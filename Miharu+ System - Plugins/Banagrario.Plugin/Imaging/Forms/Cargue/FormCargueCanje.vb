Imports System.IO
Imports DBAgrario
Imports DBCore
Imports DBCore.SchemaProcess
Imports DBImaging
Imports DBStorage
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library.Config
Imports Slyg.Tools.Imaging

Namespace Imaging.Forms.Cargue

    Public Class FormCargueCanje

#Region " Declaraciones "

        Public Const MaxThumbnailWidth As Integer = 60
        Public Const MaxThumbnailHeight As Integer = 80

        Private Const fk_Entidad_Cliente As Short = 9 'Banagrario
        Private Const fk_Proyecto As Short = 2
        Private Const fk_Esquema As Short = 3

        Private Const fk_TRD As Short = 1
        Private Const fk_TRD_Serie As Short = 1
        Private Const fk_TRD_Subserie As Short = 1

        Private Const id_Folder As Short = 1 'SOLO EXISTE UN FOLDER POR EXPEDIENTE
        Private Const id_File As Short = 1 'SOLO EXISTE UN FILE POR FOLDER
        Private Const fk_Documento_Canje As Integer = 1700

        Private Const fk_Campo_Correlativo As Short = 1
        Private Const fk_Campo_Ruta As Short = 2
        Private Const fk_Campo_Cuenta As Short = 3
        Private Const fk_Campo_Serie As Short = 4
        Private Const fk_Campo_Valor As Short = 5
        Private Const fk_Campo_BancoDestino As Short = 6

        Private Extension_Formato_Imagen As String = ""

        'Private Const fk_Formato_Salida As Short = 3
        'Private Const Nombre_Formato_Imagen As String = "jpg"

        Private FechaProceso As Date = Nothing
        Private FechaMovimiento As Date = Nothing

        Private _Plugin As BanagrarioImagingPlugin

        Private LineaActual As Integer = 0
        Private ColumnaActual As Integer = 0
        Private ErroresCargue As New List(Of String)
        Private AdventenciasCargue As New List(Of String)

        Public Shared Property SeparadorDecimal As String = ""

        Private EsCancelar As Boolean = False

        Private FinalizadoCorrectamente As Boolean = False

        Private EsReporteException As Boolean = False
        Private ReporteException As Exception = Nothing
        Private Const ReporteMessage As String = ""
        Private Const ReporteMessageIcon As DesktopMessageBoxControl.IconEnum = DesktopMessageBoxControl.IconEnum.ErrorIcon

        Private CanjesGuardados As Integer = 0
        Private CanjesSinGuardar As Integer = 0

        Private id_Cargue_guardado As Integer = 0

        Private TextoEstado As String = ""
        Private MaximoValor As Integer = 0
        Private ValorActual As Integer = 0

#End Region

#Region " Constructores "

        Public Sub New(ByVal nBanagrarioDesktopPlugin As BanagrarioImagingPlugin)
            InitializeComponent()

            _Plugin = nBanagrarioDesktopPlugin

        End Sub

#End Region

#Region " Eventos "

        Private Sub FormCargueCanje_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            Try
                SeparadorDecimal = "."
                Dim val = Double.Parse("10.5")
                If (val <> 10.5) Then
                    SeparadorDecimal = ","
                End If

                'fk_Entidad_Proceso = _Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad

                If (Now.Hour < 6) Then
                    FechaProcDateTimePicker.Value = Now.AddDays(-1)
                Else
                    FechaProcDateTimePicker.Value = Now
                End If
                FechaMovDateTimePicker.Value = FechaProcDateTimePicker.Value.AddDays(-1)

            Catch ex As Exception
                SeparadorDecimal = ","
            End Try
        End Sub

        Private Sub SeleccionarArchivo_Button_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles SeleccionarArchivo_Button.Click
            Try
                OpenFileDialog.InitialDirectory = Path.GetFullPath(ArchivoCargue_TextBox.Text)
            Catch
            End Try

            Try
                If (OpenFileDialog.ShowDialog() = Windows.Forms.DialogResult.OK) Then
                    ArchivoCargue_TextBox.Text = OpenFileDialog.FileName
                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow(ex.Message, "Error", DesktopMessageBoxControl.IconEnum.ErrorIcon)
            End Try
        End Sub

        Private Sub CargarJpg_Button_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CargarJpg_Button.Click
            FiltroGroupBox.Enabled = False
            Cancelar_Button.Visible = True
            Cancelar_Button.Enabled = True
            Cancelar_Button.Text = "Cancelar"
            EsCancelar = False

            Cargar_BackgroundWorker.RunWorkerAsync("jpg")
        End Sub

        Private Sub CargarTif_Button_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CargarTif_Button.Click
            FiltroGroupBox.Enabled = False
            Cancelar_Button.Visible = True
            Cancelar_Button.Enabled = True
            Cancelar_Button.Text = "Cancelar"
            EsCancelar = False

            Cargar_BackgroundWorker.RunWorkerAsync("tif")
        End Sub

        Private Sub Cargar_BackgroundWorker_DoWork(ByVal sender As System.Object, ByVal e As ComponentModel.DoWorkEventArgs) Handles Cargar_BackgroundWorker.DoWork
            CargarYGuardarCanje(e.Argument.ToString())
        End Sub

        Private Sub Cargar_BackgroundWorker_ProgressChanged(ByVal sender As System.Object, ByVal e As ComponentModel.ProgressChangedEventArgs) Handles Cargar_BackgroundWorker.ProgressChanged
            If (EsReporteException) Then
                If (Not ReporteException Is Nothing) Then
                    DesktopMessageBoxControl.DesktopMessageShow("Cargue", ReporteException)
                Else
                    DesktopMessageBoxControl.DesktopMessageShow(ReporteMessage, "Cargue", ReporteMessageIcon)
                End If
                EsReporteException = False
            Else
                EstadoCargue_ProgressBar.Maximum = MaximoValor
                EstadoCargue_ProgressBar.Value = ValorActual
                Estado_Cargue_Label.Text = TextoEstado
            End If
        End Sub

        Private Sub Cargar_BackgroundWorker_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As ComponentModel.RunWorkerCompletedEventArgs) Handles Cargar_BackgroundWorker.RunWorkerCompleted
            FiltroGroupBox.Enabled = True
            Cancelar_Button.Visible = False
            EstadoCargue_ProgressBar.Value = 0
            Estado_Cargue_Label.Text = "Proceso finalizado"

            If (ErroresCargue.Count > 0) Then
                Dim dlg As New FormCargueCanjeRollbackError(ErroresCargue)
                dlg.Show()
            Else
                If (FinalizadoCorrectamente) Then
                    DesktopMessageBoxControl.DesktopMessageShow("Cargue realizado con exito, Se guardaron (" & CStr(CanjesGuardados) & ") canjes y (" & CStr(CanjesSinGuardar) & ") no fueron cargados", "Cargue", DesktopMessageBoxControl.IconEnum.SuccessfullIcon)
                Else
                    DesktopMessageBoxControl.DesktopMessageShow("Cargue No Realizado", "Cargue", DesktopMessageBoxControl.IconEnum.ErrorIcon)
                End If

            End If
        End Sub

        Private Sub Cancelar_Button_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles Cancelar_Button.Click
            EsCancelar = True
            Cancelar_Button.Text = "Cancelando..."
            Cancelar_Button.Enabled = False
        End Sub
#End Region

#Region " Metodos "

        Private Sub CargarYGuardarCanje(ByVal nExtensionImagenOrigen As String)
            Try
                FinalizadoCorrectamente = False

                If (ArchivoCargue_TextBox.Text.Trim = "") Then Throw New Exception("Primero debe seleccionar un archivo")
                FechaMovimiento = New DateTime(FechaMovDateTimePicker.Value.Year, FechaMovDateTimePicker.Value.Month, FechaMovDateTimePicker.Value.Day)
                FechaProceso = New DateTime(FechaProcDateTimePicker.Value.Year, FechaProcDateTimePicker.Value.Month, FechaProcDateTimePicker.Value.Day)

                If (FechaMovimiento.Subtract(FechaProceso).Milliseconds > 0) Then
                    Throw New Exception("La fecha de movimiento no puede ser mayor a la fecha de proceso")
                End If

                If (Not File.Exists(ArchivoCargue_TextBox.Text)) Then Throw New Exception("El archivo seleccionado no existe")

                Dim canjeData = ValidarYPrepararCargue(ArchivoCargue_TextBox.Text, nExtensionImagenOrigen)

                If (AdventenciasCargue.Count > 0 Or ErroresCargue.Count > 0) Then
                    Dim dlg As New FormCargueCanjeDetalle(ErroresCargue, AdventenciasCargue)
                    If (dlg.ShowDialog() = Windows.Forms.DialogResult.OK) Then
                        GuardarCargue(canjeData)
                    End If
                Else
                    GuardarCargue(canjeData)
                End If

            Catch ex As Exception
                ReporteBackgroudException(ex)
            End Try
        End Sub

        Private Sub ReporteBackgroudException(ByVal ex As Exception)
            EsReporteException = True
            ReporteException = ex
            Cargar_BackgroundWorker.ReportProgress(1)
        End Sub

        'Private Sub ReporteBackgroudException(ByVal nMessage As String, ByVal nIconEnum As DesktopMessageBox.IconEnum)
        '    EsReporteException = True
        '    ReporteException = Nothing
        '    ReporteMessage = nMessage
        '    ReporteMessageIcon = nIconEnum
        '    Cargar_BackgroundWorker.ReportProgress(1)
        '    EsReporteException = False
        'End Sub

        Private Function ReadCanjeInfo(ByRef nLine As String) As CanjeInfo
            Dim canje As New CanjeInfo

            Try
                canje.Correlativo = ReadLinePart(nLine, 0, 8)
                canje.Ruta = ReadLinePart(nLine, 9, 7)
                canje.Cuenta = ReadLinePart(nLine, 25, 14)
                canje.Serie = ReadLinePart(nLine, 40, 8)
                canje.Valor = ReadLinePart(nLine, 49, 14)
                canje.BancoDestino = ReadLinePart(nLine, 64, 1)
            Catch ex As Exception
                Throw New Exception("Error al leer los valores de la linea de canje, " & ex.Message, ex)
            End Try

            Return canje
        End Function

        Private Function ParseCanjeInfo(ByRef nCanjeInfo As CanjeInfo) As Boolean
            Try
                nCanjeInfo.Correlativo = nCanjeInfo.Correlativo.Trim()
                If (nCanjeInfo.Correlativo = "") Then Throw New Exception("El campo correlativo no puede ser vacio")

                If (Convert.ToInt32(nCanjeInfo.Correlativo) >= 500000) Then Return False

                nCanjeInfo.Ruta = nCanjeInfo.Ruta.TrimStart("0"c)
                If (nCanjeInfo.Ruta = "") Then Throw New Exception("El campo ruta no puede ser vacio o ceros")

                nCanjeInfo.Oficina = nCanjeInfo.Cuenta.Substring(3, 4)

                nCanjeInfo.Cuenta = nCanjeInfo.Cuenta.Substring(2, 12)
                'If (nCanjeInfo.Cuenta = "") Then Throw New Exception("El campo cuenta no puede ser vacio o ceros")

                nCanjeInfo.Serie = nCanjeInfo.Serie.Substring(1, 7)
                'If (nCanjeInfo.Serie = "") Then Throw New Exception("El campo serie no puede ser vacio o ceros")

                nCanjeInfo.Valor = nCanjeInfo.Valor.TrimStart("0"c)
                'If (nCanjeInfo.Valor = "") Then Throw New Exception("El campo valor no puede ser vacio o ceros")
                If (nCanjeInfo.Valor = "") Then
                    nCanjeInfo.Valor = "0"
                ElseIf (nCanjeInfo.Valor.Length < 3) Then
                    nCanjeInfo.Valor = "0" & SeparadorDecimal & String.Format("{0:00}", Convert.ToInt32(nCanjeInfo.Valor))
                Else
                    nCanjeInfo.Valor = nCanjeInfo.Valor.Substring(0, nCanjeInfo.Valor.Length - 2) & SeparadorDecimal & nCanjeInfo.Valor.Substring(nCanjeInfo.Valor.Length - 2, 2)
                End If

                nCanjeInfo.BancoDestino = nCanjeInfo.BancoDestino.Trim()

                Return True
            Catch ex As Exception
                Throw New Exception("Error al analizar los valores de la linea de canje, " & ex.Message, ex)
            End Try
        End Function

        Public Function ReadLine(ByRef nReader As StreamReader) As String
            LineaActual += 1
            Dim line = nReader.ReadLine()
            ValorActual += line.Length
            Cargar_BackgroundWorker.ReportProgress(1)
            Return line
        End Function

        Public Function ReadLinePart(ByRef nLine As String, ByVal nStartIndex As Integer, ByVal nLength As Integer) As String
            ColumnaActual = nStartIndex
            Return nLine.Substring(ColumnaActual, nLength)
        End Function

        Private Function ValidarYPrepararCargue(ByVal nArchivoCargue As String, ByVal nExtensionImagenOrigen As String) As List(Of CanjeInfo)
            Dim canjeData As New List(Of CanjeInfo)
            Dim line As String

            Try
                LineaActual = 0
                ErroresCargue.Clear()
                AdventenciasCargue.Clear()

                Using reader As New StreamReader(nArchivoCargue)
                    Cargar_BackgroundWorker.ReportProgress(1)

                    TextoEstado = "Leyendo el archivo y las imagenes"
                    MaximoValor = CInt(reader.BaseStream.Length)
                    ValorActual = 0
                    Cargar_BackgroundWorker.ReportProgress(1)

                    ReadLine(reader)

                    While (Not reader.EndOfStream)
                        If (EsCancelar) Then Throw New Exception("Operacion cancelada por el usuario")

                        line = ""
                        Try
                            line = ReadLine(reader)
                            If (line.Trim() <> "") Then

                                Dim canje = ReadCanjeInfo(line)

                                If (ParseCanjeInfo(canje)) Then
                                    BuscarImagenesCanje(canje, nArchivoCargue, nExtensionImagenOrigen)
                                    If (canje.Imagenes.Count > 0) Then
                                        canjeData.Add(canje)
                                    Else
                                        ErroresCargue.Add("Error: Linea " & LineaActual.ToString & ", no se encontro la imagen asociada al registro, " & line)
                                    End If

                                Else
                                    AdventenciasCargue.Add("Adventencia: Linea " & LineaActual.ToString & ", Se ignora la linea >= 500000, " & line)
                                End If
                            Else
                                AdventenciasCargue.Add("Adventencia: Linea " & LineaActual.ToString & ", La linea esta vacia, " & line)
                            End If

                        Catch ex As Exception
                            ErroresCargue.Add("Error: Linea " & LineaActual.ToString & ", " & ex.Message & ", " & line)
                        End Try

                    End While
                End Using
            Catch ex As Exception
                Throw New Exception("No fue posible leer el archivo, " & ex.Message)
            End Try

            ValorActual = 0
            TextoEstado = "Lectura realizada"
            Cargar_BackgroundWorker.ReportProgress(1)

            Return canjeData
        End Function

        Private Sub GuardarCargue(ByRef nCanjeData As List(Of CanjeInfo))
            ErroresCargue.Clear()

            Dim dbmCore As DBCoreDataBaseManager = Nothing
            Dim dbStorage As DBStorageDataBaseManager = Nothing
            Dim dbmAgrario As DBAgrarioDataBaseManager = Nothing
            Dim dbImg As DBImagingDataBaseManager = Nothing

            Try

                'INICIALIZAR LOS CONTROLADORES DE BASE DE DATOS
                dbmAgrario = Create_Banco_Agrario_DataBaseManager()
                ''dbmAgrario.DataBase.Identifier_Date_Format = _Plugin.Manager.DesktopGlobal.IdentifierDateFormat
                dbmAgrario.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                dbmCore = Create_Core_DataBaseManager()
                ''dbmCore.DataBase.Identifier_Date_Format = _Plugin.Manager.DesktopGlobal.IdentifierDateFormat
                dbmCore.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
                'dbmCore.Transaction_Begin()

                dbImg = Create_Imaging_DataBaseManager()
                'dbImg.DataBase.Identifier_Date_Format = _Plugin.Manager.DesktopGlobal.IdentifierDateFormat
                dbImg.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
                'dbImg.Transaction_Begin()

                'formatoImagen = GetEnumFormat(fk_Formato_Salida, Nombre_Formato_Imagen)
                Extension_Formato_Imagen = "jpg"

                dbStorage = Create_ImagingStorage_DataBaseManager(_Plugin.Manager.DesktopGlobal.ServidorImagenRow.ConnectionString_Servidor)
                'dbStorage.DataBase.Identifier_Date_Format = _Plugin.Manager.DesktopGlobal.IdentifierDateFormat
                dbStorage.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
                'dbImag.Transaction_Begin()

                MaximoValor = nCanjeData.Count
                ValorActual = 0
                TextoEstado = "Indexando y guardando las imagenes"
                Cargar_BackgroundWorker.ReportProgress(1)

                CanjesGuardados = 0
                CanjesSinGuardar = 0

                GuardarImagingCargueYPaquete(dbImg)

                For i = 0 To nCanjeData.Count - 1
                    If (EsCancelar) Then Throw New Exception("Operacion cancelada por el usuario")

                    'Validar canje
                    Dim canjesCargue = dbmAgrario.SchemaProcess.PA_Consulta_Archivo_Detalle_Canje.DBExecute(
                        FechaMovimiento.ToString("yyyy/MM/dd"),
                        nCanjeData(i).Serie,
                        nCanjeData(i).Cuenta)

                    If (canjesCargue > 0) Then
                        GuardarTransaccion(dbmCore, dbStorage, dbmAgrario, nCanjeData(i))
                        CanjesGuardados += 1
                    Else
                        CanjesSinGuardar += 1
                    End If

                    ValorActual += 1
                    Cargar_BackgroundWorker.ReportProgress(1)
                Next

                ValorActual = 0
                TextoEstado = "Cargue realizado"

                FinalizadoCorrectamente = True
            Catch ex As Exception
                ReversarTransacciones(dbmCore, dbImg, dbStorage, nCanjeData)

                ReporteBackgroudException(ex)
            Finally
                If Not dbmCore Is Nothing Then Try : dbmCore.Connection_Close() : Catch : End Try
                If Not dbStorage Is Nothing Then Try : dbStorage.Connection_Close() : Catch : End Try
                If Not dbmAgrario Is Nothing Then Try : dbmAgrario.Connection_Close() : Catch : End Try
            End Try
        End Sub

        Private Sub GuardarImagingCargueYPaquete(ByVal dbImg As DBImagingDataBaseManager)
            Dim fk_Entidad_Procesamiento = _Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad

            Dim fk_Entidad_Servidor As Short = _Plugin.Manager.DesktopGlobal.ServidorImagenRow.fk_Entidad
            Dim fk_Servidor As Short = _Plugin.Manager.DesktopGlobal.ServidorImagenRow.id_Servidor
            Const fk_OT As Integer = 1
            Dim fk_Sede_Procesamiento_Cargue As Short = _Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Sede
            Dim fk_Centro_Procesamiento_Cargue As Short = _Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento
            Dim nombreCentroProcesamiento = "CP_" & _Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.Nombre_Centro_Procesamiento


            'GUARDAR CARGUE
            id_Cargue_guardado = dbImg.SchemaProcess.PA_Guardar_TBL_Cargue.DBExecute(
                nfk_Entidad:=fk_Entidad_Cliente,
                nfk_Proyecto:=fk_Proyecto,
                nfk_Estado:=EstadoEnum.Indexado,
                nfk_Entidad_Procesamiento:=fk_Entidad_Procesamiento,
                nfk_Sede_Procesamiento_Cargue:=fk_Sede_Procesamiento_Cargue,
                nfk_Centro_Procesamiento_Cargue:=fk_Centro_Procesamiento_Cargue,
                nFecha_Proceso:=FechaProceso,
                nfk_Entidad_Servidor:=fk_Entidad_Servidor,
                nfk_Servidor:=fk_Servidor,
                nObservaciones:=nombreCentroProcesamiento,
                nfk_Usuario_Log:=_Plugin.Manager.Sesion.Usuario.id,
                nfk_OT:=fk_OT)

            'GUARDAR PAQUETE
            Dim paquete As New DBImaging.SchemaProcess.TBL_Cargue_PaqueteType
            paquete.fk_Cargue = id_Cargue_guardado
            paquete.id_Cargue_Paquete = CShort(1)
            paquete.Fecha_Proceso = FechaProceso
            paquete.fk_Estado = EstadoEnum.Indexado
            paquete.Path_Cargue_Paquete = ""
            paquete.fk_Usuario_Log = _Plugin.Manager.Sesion.Usuario.id
            paquete.fk_Sede_Procesamiento_Asignada = _Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Sede_Asignada
            paquete.fk_Centro_Procesamiento_Asignado = _Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Centro_Procesamiento_Asignado

            dbImg.SchemaProcess.TBL_Cargue_Paquete.DBInsert(paquete)

        End Sub

        Private Sub ReversarTransacciones(ByRef nDbCore As DBCoreDataBaseManager, ByVal dbImg As DBImagingDataBaseManager, ByRef nDbStorage As DBStorageDataBaseManager, ByRef nCanjeData As List(Of CanjeInfo))
            If (id_Cargue_guardado <> 0) Then
                dbImg.SchemaProcess.TBL_Cargue.DBDelete(id_Cargue_guardado)
            End If
            For Each canje In nCanjeData
                Try
                    If (canje.Indexado) Then
                        nDbCore.SchemaProcess.TBL_Expediente.DBDelete(canje.fk_Expediente)
                        nDbStorage.SchemaImaging.TBL_File.DBDelete(canje.fk_Expediente, Nothing, Nothing, Nothing)
                    End If

                Catch ex As Exception
                    ErroresCargue.Add("Expediente no se pudo eliminar, " & canje.fk_Expediente.ToString & " ," & ex.Message)
                End Try
            Next
        End Sub

        Private Sub GuardarTransaccion(ByRef nDbCore As DBCoreDataBaseManager, ByRef nDbStorage As DBStorageDataBaseManager, ByRef nDbBanagrario As DBAgrarioDataBaseManager, ByRef nCanje As CanjeInfo)
            Try
                'GUARDAR EXPEDIENTE EN CORE PROCESS
                Dim coreFolder As TBL_FolderType

                Dim id_Expediente = nDbCore.SchemaProcess.PA_Insertar_Expediente.DBExecute(fk_Entidad_Cliente, fk_Proyecto, fk_Esquema, fk_TRD, fk_TRD_Serie, fk_TRD_Subserie, 0)

                nCanje.fk_Expediente = id_Expediente

                'GUARDAR FOLDER
                coreFolder = New TBL_FolderType()
                coreFolder.fk_Expediente = id_Expediente
                coreFolder.id_Folder = id_Folder
                coreFolder.CBarras_Folder = id_Folder.ToString
                'nuevoFolder.Fecha_Inicial = DateTime.Parse("2011-01-01")
                'nuevoFolder.Fecha_Final = DateTime.Parse("2013-01-01")

                nDbCore.SchemaProcess.TBL_Folder.DBInsert(coreFolder)

                'GUARDAR LAS LLAVES DEL EXPEDIENTE
                Dim llave As New TBL_Expediente_LlaveType()
                llave.fk_Expediente = id_Expediente

                llave.fk_proyecto_Llave = CShort(1)
                llave.fk_campo_tipo = CByte(5) 'Oficina
                llave.Valor_Llave = CType(nCanje.Oficina, Int32)
                nDbCore.SchemaProcess.TBL_Expediente_Llave.DBInsert(llave)

                llave.fk_proyecto_Llave = CShort(2)
                llave.fk_campo_tipo = CByte(3) 'Fecha
                llave.Valor_Llave = FechaMovimiento
                nDbCore.SchemaProcess.TBL_Expediente_Llave.DBInsert(llave)

                'GUARDAR EL ESTADO DEL FOLDER
                Dim coreFolderEstado As New TBL_Folder_estadoType()

                coreFolderEstado.fk_Expediente = id_Expediente
                coreFolderEstado.fk_Folder = id_Folder
                coreFolderEstado.Modulo = DesktopConfig.Modulo.Imaging
                coreFolderEstado.fk_Estado = EstadoEnum.Indexado
                coreFolderEstado.fk_Usuario = _Plugin.Manager.Sesion.Usuario.id
                coreFolderEstado.Fecha_Log = DateTime.Now

                nDbCore.SchemaProcess.TBL_Folder_estado.DBInsert(coreFolderEstado)

                'GUARDAR EL FOLDER EN CORE IMAGING
                Dim imgFolder As New DBCore.SchemaImaging.TBL_FolderType()
                imgFolder.fk_Expediente = id_Expediente
                imgFolder.fk_Folder = id_Folder
                imgFolder.fk_Entidad_Servidor = _Plugin.Manager.DesktopGlobal.ServidorImagenRow.fk_Entidad
                imgFolder.fk_Servidor = _Plugin.Manager.DesktopGlobal.ServidorImagenRow.id_Servidor
                imgFolder.Fecha_Creacion = Now
                imgFolder.Fecha_Transferencia = Nothing
                imgFolder.En_Transferencia = False
                imgFolder.fk_Entidad_Servidor_Transferencia = Nothing
                imgFolder.fk_Servidor_Transferencia = Nothing
                imgFolder.fk_Cargue = id_Cargue_guardado
                imgFolder.fk_Cargue_Paquete = CShort(1)
                nDbCore.SchemaImaging.TBL_Folder.DBInsert(imgFolder)

                'GUARDAR FILE EN CORE
                Dim coreFile As New TBL_FileType

                coreFile.fk_Expediente = id_Expediente
                coreFile.fk_Folder = coreFolder.id_Folder
                coreFile.id_File = CShort(1)
                coreFile.fk_Documento = fk_Documento_Canje
                coreFile.File_Unique_Identifier = Guid.NewGuid()
                coreFile.CBarras_File = Convert.ToString(coreFile.id_File)

                coreFile.Folios_File = CType(nCanje.Imagenes.Count, Short)
                coreFile.Monto_File = 0

                nDbCore.SchemaProcess.TBL_File.DBInsert(coreFile)

                'GUARDAR EL FILE EN CORE IMAGING
                Dim imgFile As New DBCore.SchemaImaging.TBL_FileType()
                imgFile.fk_Expediente = id_Expediente
                imgFile.fk_Folder = id_Folder
                imgFile.fk_File = id_File
                imgFile.id_Version = CShort(1)
                imgFile.File_Unique_Identifier = Guid.NewGuid
                imgFile.Folios_Documento_File = CType(nCanje.Imagenes.Count, Short)
                imgFile.Tamaño_Imagen_File = CalcularTamanoFolios(nCanje.Imagenes)
                imgFile.Nombre_Imagen_File = ""
                imgFile.Key_Cargue_Item = ""
                imgFile.Save_FileName = ""
                imgFile.fk_Content_Type = ".jpg"
                imgFile.fk_Usuario_Log = _Plugin.Manager.Sesion.Usuario.id
                imgFile.Validaciones_Opcionales = False
                nDbCore.SchemaImaging.TBL_File.DBInsert(imgFile)

                'GUARDAR EL ESTADO DEL ARCHIVO
                Dim coreFileEstado As New TBL_File_EstadoType()
                coreFileEstado.fk_Expediente = id_Expediente
                coreFileEstado.fk_Folder = coreFolder.id_Folder
                coreFileEstado.fk_File = coreFile.id_File
                coreFileEstado.Modulo = DesktopConfig.Modulo.Imaging
                coreFileEstado.fk_Estado = EstadoEnum.Indexado
                coreFileEstado.fk_Usuario = _Plugin.Manager.Sesion.Usuario.id
                coreFileEstado.Fecha_Log = DateTime.Now

                nDbCore.SchemaProcess.TBL_File_Estado.DBInsert(coreFileEstado)

                'GUARDAR LOS CAMPOS DEL DOCUMENTO

                'GUARDAR FILE DATA
                Dim coreFileData As New TBL_File_DataType

                coreFileData.fk_Expediente = id_Expediente
                coreFileData.fk_Folder = coreFolder.id_Folder
                coreFileData.fk_File = coreFile.id_File
                coreFileData.fk_Documento = fk_Documento_Canje

                coreFileData.fk_Campo = fk_Campo_Correlativo
                coreFileData.Valor_File_Data = nCanje.Correlativo
                coreFileData.Conteo_File_Data = nCanje.Correlativo.Length
                nDbCore.SchemaProcess.TBL_File_Data.DBInsert(coreFileData)

                coreFileData.fk_Campo = fk_Campo_Ruta
                coreFileData.Valor_File_Data = nCanje.Ruta
                coreFileData.Conteo_File_Data = nCanje.Ruta.Length
                nDbCore.SchemaProcess.TBL_File_Data.DBInsert(coreFileData)

                coreFileData.fk_Campo = fk_Campo_Cuenta
                coreFileData.Valor_File_Data = nCanje.Cuenta
                coreFileData.Conteo_File_Data = nCanje.Cuenta.Length
                nDbCore.SchemaProcess.TBL_File_Data.DBInsert(coreFileData)

                coreFileData.fk_Campo = fk_Campo_Serie
                coreFileData.Valor_File_Data = CType(nCanje.Serie, Int64)
                coreFileData.Conteo_File_Data = nCanje.Serie.Length
                nDbCore.SchemaProcess.TBL_File_Data.DBInsert(coreFileData)

                coreFileData.fk_Campo = fk_Campo_Valor
                coreFileData.Valor_File_Data = Convert.ToDecimal(nCanje.Valor)
                coreFileData.Conteo_File_Data = nCanje.Valor.Length
                nDbCore.SchemaProcess.TBL_File_Data.DBInsert(coreFileData)

                coreFileData.fk_Campo = fk_Campo_BancoDestino
                coreFileData.Valor_File_Data = nCanje.BancoDestino
                coreFileData.Conteo_File_Data = nCanje.BancoDestino.Length
                nDbCore.SchemaProcess.TBL_File_Data.DBInsert(coreFileData)

                'GUARDAR LAS IMAGENES

                Dim imagFile = New DBStorage.SchemaImaging.TBL_FileType()
                imagFile.File_Unique_Identifier = coreFile.File_Unique_Identifier
                imagFile.fk_Content_Type = Extension_Formato_Imagen
                imagFile.fk_Expediente = id_Expediente
                imagFile.fk_Folder = coreFolder.id_Folder
                imagFile.fk_File = coreFile.id_File
                imagFile.id_Version = CShort(1)

                nDbStorage.SchemaImaging.TBL_File.DBInsert(imagFile)

                For i = 0 To nCanje.Imagenes.Count - 1
                    Dim imagFileFolio = New DBStorage.SchemaImaging.TBL_File_FolioType()
                    imagFileFolio.fk_Expediente = imagFile.fk_Expediente
                    imagFileFolio.fk_File = imagFile.fk_File
                    imagFileFolio.fk_Folder = imagFile.fk_Folder
                    imagFileFolio.fk_Version = imagFile.id_Version
                    imagFileFolio.id_File_Record_Folio = CType(i + 1, Short)

                    imagFileFolio.Image_Binary = nCanje.Imagenes(i)
                    imagFileFolio.Thumbnail_Binary = nCanje.ImagenesMini(i)

                    nDbStorage.SchemaImaging.TBL_File_Folio.DBInsert(imagFileFolio)
                Next

                nDbBanagrario.SchemaProcess.PA_Preparar_Data_File_Sin_Salida.DBExecute(id_Expediente, id_Folder, id_File)

                nCanje.Indexado = True
            Catch ex As Exception
                Throw New Exception("Error al guardar la transaccion, " & ex.Message, ex)
            End Try
        End Sub

        Public Function Create_Banco_Agrario_DataBaseManager() As DBAgrarioDataBaseManager
            Try
                Return New DBAgrarioDataBaseManager(Me._Plugin.BancoAgrarioConnectionString)
            Catch ex As Exception
                Throw New Exception("No fue posible crear la conexion a la base de datos de banco agrario, " & ex.Message, ex)
            End Try
        End Function

        Public Function Create_Core_DataBaseManager() As DBCoreDataBaseManager
            Try
                Return New DBCoreDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Core)
            Catch ex As Exception
                Throw New Exception("No fue posible crear la conexion a la base de datos de core, " & ex.Message, ex)
            End Try
        End Function

        Public Function Create_Imaging_DataBaseManager() As DBImagingDataBaseManager
            Try
                Return New DBImagingDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)
            Catch ex As Exception
                Throw New Exception("No fue posible crear la conexion a la base de datos de imaging, " & ex.Message, ex)
            End Try
        End Function

        Public Function Create_ImagingStorage_DataBaseManager(ByVal nConnectionString As String) As DBStorageDataBaseManager
            Try
                Return New DBStorageDataBaseManager(nConnectionString)
            Catch ex As Exception
                Throw New Exception("No fue posible crear la conexion a la base de datos imaging storage, " & ex.Message, ex)
            End Try
        End Function

        'Private Function GetEnumFormat(ByVal nDataBaseFormat As Short, ByVal nDataBaseFormatName As String) As ImageManager.EnumFormat
        '    Select Case nDataBaseFormat
        '        Case 1 : Return ImageManager.EnumFormat.BMP
        '        Case 2 : Return ImageManager.EnumFormat.GIF
        '        Case 3 : Return ImageManager.EnumFormat.JPEG
        '        Case 4 : Return ImageManager.EnumFormat.PDF
        '        Case 5 : Return ImageManager.EnumFormat.PNG
        '        Case 6 : Return ImageManager.EnumFormat.TIFF 'TIFF Color (Tagged Image File Format)
        '        Case 7 : Return ImageManager.EnumFormat.TIFF 'Bitonal (Tagged Image File Format)	.tif
        '    End Select
        '    Throw New Exception("Formato de imagen no permitido, " & nDataBaseFormat.ToString() & " " & nDataBaseFormatName)
        'End Function

        Private Function GetTiffBuffer(ByRef nImage As FreeImageAPI.FreeImageBitmap) As Byte()
            Using ms As New MemoryStream
                nImage.Save(ms, FreeImageAPI.FREE_IMAGE_FORMAT.FIF_JPEG)
                Return ms.GetBuffer()
            End Using
        End Function

        Private Function GetMiniTiffBuffer(ByRef nImage As FreeImageAPI.FreeImageBitmap) As Byte()
            Using ms As New MemoryStream
                Dim thumb = ImageManager.GetThumbnailBitmap(nImage, 1, MaxThumbnailWidth, MaxThumbnailHeight)
                thumb.Save(ms, FreeImageAPI.FREE_IMAGE_FORMAT.FIF_JPEG)
                Return ms.GetBuffer()
            End Using
        End Function

        Private Sub BuscarImagenesCanje(ByRef nCanje As CanjeInfo, ByVal nArchivoCargue As String, ByVal nExtensionImagenOrigen As String)
            Dim correlativo = String.Format("{0:000000}", Convert.ToInt32(nCanje.Correlativo))

            Dim ruta = Path.GetDirectoryName(nArchivoCargue) & "\" & correlativo(0) & "\" + correlativo(1) & "\" + correlativo(2) & "\"

            If (nExtensionImagenOrigen = "jpg") Then
                Dim fileNameB = ruta & "B" & correlativo & ".jpg"
                Dim fileNameF = ruta & "F" & correlativo & ".jpg"

                If (File.Exists(fileNameB)) Then
                    Dim fileBitmap = New FreeImageAPI.FreeImageBitmap(fileNameB)
                    nCanje.Imagenes.Add(GetTiffBuffer(fileBitmap))
                    nCanje.ImagenesMini.Add(GetMiniTiffBuffer(fileBitmap))
                End If

                If (File.Exists(fileNameF)) Then
                    Dim fileBitmap = New FreeImageAPI.FreeImageBitmap(fileNameF)
                    nCanje.Imagenes.Add(GetTiffBuffer(fileBitmap))
                    nCanje.ImagenesMini.Add(GetMiniTiffBuffer(fileBitmap))
                End If

            ElseIf (nExtensionImagenOrigen = "tif") Then
                Dim fileName = ruta & correlativo & ".tif"
                If (File.Exists(fileName)) Then
                    Dim fileBitmap = New FreeImageAPI.FreeImageBitmap(fileName)

                    Dim folios = ImageManager.GetFolios(fileBitmap)

                    If (folios > 0) Then
                        Dim img = ImageManager.GetFolioBitmap(fileBitmap, 1)

                        nCanje.Imagenes.Add(GetTiffBuffer(img))
                        nCanje.ImagenesMini.Add(GetMiniTiffBuffer(img))
                    End If

                    If (folios > 1) Then
                        Dim img = ImageManager.GetFolioBitmap(fileBitmap, 2)

                        nCanje.Imagenes.Add(GetTiffBuffer(img))
                        nCanje.ImagenesMini.Add(GetMiniTiffBuffer(img))
                    End If
                End If
            End If

        End Sub

        Private Function CalcularTamanoFolios(ByVal Imagenes As List(Of Byte())) As Long
            Dim tamano As Long = 0
            Try
                For Each img In Imagenes
                    tamano += CType(img.Length, Long)
                Next
            Catch
            End Try
            Return tamano
        End Function

#End Region

    End Class

End Namespace