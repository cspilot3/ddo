Imports System.ComponentModel
Imports System.Globalization
Imports System.IO
Imports System.Threading
Imports System.Windows.Forms
Imports Slyg.Tools.CSV
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports DBArchiving
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Config
Imports System.Linq
Imports System.Configuration
Imports System.Data.SqlClient

Namespace Forms.Cargue

    Public Class FormCargueClienteEXterno
        Inherits FormBase

#Region " Declaraciones "

        Private _File As Stream = Nothing
        Private _DataFile As DataTable = Nothing
        Private _DataRegistros As Integer = 0
        Private _DataColumnas As Integer = 0
        Private _EstadoProceso As Short = 0 '0 Validando, 1 Procesando.
        Private objCSV As New Slyg.Tools.CSV.CSVData
        Private objXLS As New XLSData
        Private trResultado As DesktopConfig.TypeResult
        Private _TipoCargue As DesktopConfig.TipoCargue
        Private Segundos As Integer = 0
        Private Minuto As Integer = 0
        Private Hora As Integer = 0
        Private TiposLogDataTable As DBArchiving.SchemaConfig.TBL_Tipos_LogDataTable
        Private TiposLogrow As DBArchiving.SchemaConfig.TBL_Tipos_LogRow
        Private validaArchivoCargue As Boolean
        Private SaltoPrimasLineas As Integer
        Private ExtensionGlobal As String = ""

#End Region

#Region " Constructor "
        Private _appSettings As String

        Sub New(ByVal TipoCargue As DesktopConfig.TipoCargue)
            ' This call is required by the designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            _TipoCargue = TipoCargue
        End Sub

#End Region

#Region " Metodos "

        Private Property AppSettings(p1 As String) As String
            Get
                Return _appSettings
            End Get
            Set(value As String)
                _appSettings = value
            End Set
        End Property

        Private Sub BuscarArchivo()
            Segundos = 0
            Minuto = 0
            Hora = 0

            Try
                Dim Respuesta = ArchivoOpenFileDialog.ShowDialog()

                If Respuesta = DialogResult.OK Then
                    Try
                        ArchivoDesktopTextBox.Text = ArchivoOpenFileDialog.FileName

                        'Si el archivo es txt o csv, habilita la opción de manejar separador
                        If ArchivoOpenFileDialog.FileName.ToLower().EndsWith(".txt") OrElse ArchivoOpenFileDialog.FileName.ToLower().EndsWith(".csv") Then
                            OpcionesSeparadorGroupBox.Enabled = True
                        Else
                            OpcionesSeparadorGroupBox.Enabled = False
                        End If

                        _File = ArchivoOpenFileDialog.OpenFile()
                    Catch ex As Exception
                        DesktopMessageBoxControl.DesktopMessageShow("BuscarArchivo", ex)
                    Finally
                        If _File IsNot Nothing Then
                            _File.Close()
                        End If
                    End Try

                ElseIf Respuesta = DialogResult.Cancel Then
                    OpcionesSeparadorGroupBox.Enabled = False
                    ArchivoDesktopTextBox.Text = ""
                    _File = Nothing
                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("BuscarArchivo", ex)
            End Try
        End Sub

        Private Sub HabilitarControles(ByVal valor As Boolean)
            ArchivoDesktopTextBox.Enabled = valor
            BuscarArchivoButton.Enabled = valor
            CargarButton.Enabled = valor
        End Sub

        Private Sub CargaDatos()
            Dim dbmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            Try
                dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

                TiposLogDataTable = dbmArchiving.SchemaConfig.TBL_Tipos_Log.DBFindByfk_Entidadfk_ProyectoEliminado(Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, False)
                Utilities.LlenarCombo(DesktopComboBoxControlTiposLog, TiposLogDataTable, TiposLogDataTable.id_Tipo_logColumn.ColumnName, TiposLogDataTable.Nombre_Tipo_LogColumn.ColumnName, True)

                Dim EsquemasFac = dbmArchiving.Schemadbo.CTA_Esquema_x_Facturacion.DBFindByfk_Entidad_Cliente(Program.RiskGlobal.Entidad)
                Utilities.LlenarCombo(EsquemaFacturacionComboBox, EsquemasFac, EsquemasFac.IDColumn.ColumnName, EsquemasFac.ValorColumn.ColumnName, True)
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaDatos", ex)
            Finally
                dbmArchiving.Connection_Close()
            End Try
        End Sub

#End Region

#Region " Funciones "
        Private Function SubString(ByVal CamposLog As DataTable, ByVal File As Stream) As DataTable

            Dim Lee = New System.IO.StreamReader(CType(File, FileStream).Name)
            Dim Data = New DataTable()

            For Each x As DataRow In CamposLog.Rows
                Data.Columns.Add(x.Item("Descripcion").ToString())
            Next

            Dim texto = ""
            Dim numLinea = 0
            If CInt(TiposLogrow.Salto_Primeras_Lineas) = 0 Then
                texto = Lee.ReadLine()
                numLinea = numLinea + 1
            Else
                For index As Integer = 0 To CInt(TiposLogrow.Salto_Primeras_Lineas) ' - 1
                    texto = Lee.ReadLine()
                    numLinea = numLinea + 1
                Next
            End If

            Dim longitud_Fija = 0

            If Not IsDBNull(TiposLogrow.Longitud_Fija) Then
                longitud_Fija = CInt(TiposLogrow.Longitud_Fija)
            End If

            While texto IsNot Nothing
                Dim lenTexto = texto.Length
                Dim Valida = True
                If (TiposLogrow.Identificador_Linea <> "" Or Not IsDBNull(TiposLogrow.Identificador_Linea)) Then
                    Dim lon = TiposLogrow.Identificador_Linea.Length
                    If (lenTexto > 0) Then
                        If (texto.Substring(0, lon) = TiposLogrow.Identificador_Linea) Then
                            Valida = True
                        Else
                            Valida = False
                            texto = Lee.ReadLine()
                        End If
                    Else
                        Valida = False
                        texto = Lee.ReadLine()
                    End If
                End If

                If (Valida) Then
                    If (longitud_Fija <> 0 And lenTexto = longitud_Fija) Or (longitud_Fija = 0) Then
                        Dim row = Data.NewRow
                        Try
                            For Each x As DataRow In CamposLog.Rows
                                row.Item(x.Item("Descripcion").ToString()) = "" + texto.Substring(CInt(x.Item("Length_Inicia")), CInt(x.Item("Length_Fijo")))
                            Next
                            Data.Rows.Add(row)
                        Catch
                            Throw New Exception("Error en la linea " + numLinea.ToString() + " por favor revisar el log y volver a intentar")
                        End Try
                        texto = Lee.ReadLine()
                        numLinea = numLinea + 1
                    Else
                        Throw New Exception("Error en la linea " + numLinea.ToString() + " por favor revisar el log y volver a intentar")
                    End If
                End If

            End While

            Dim saltosUltimas = ""

            If Not IsDBNull(TiposLogrow.Salto_Ultimas_Lineas) Then
                saltosUltimas = TiposLogrow.Salto_Ultimas_Lineas
            End If

            For index As Integer = 0 To CInt(saltosUltimas) - 1
                Data.Rows.RemoveAt(Data.Rows.Count - 1)
            Next

            Return Data
        End Function

        Private Function CargarArchivo(ByVal NombreArchivo As String, ByVal Entidad As Short, ByVal Esquema As Short) As DesktopConfig.TypeResult
            Dim trReturn As New DesktopConfig.TypeResult
            trReturn.Result = True
            Dim _Data As DataTable = New DataTable

            Dim CamposLog As DataTable = New DataTable

            Dim CamposLLenos As Boolean = False
            Dim contadorValoresEncontrados As Integer = 0
            Dim ltCamposLog As List(Of String) = New List(Of String)
            Try
                Dim dbmArchiving As DBArchivingDataBaseManager = Nothing

                dbmArchiving = New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
                dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

                If OpcionesSeparadorGroupBox.Enabled Then 'Es plano CSV o TXT
                    'Se obtiene el separador
                    If ComaRadioButton.Checked Then objCSV.Separator = CChar(",")
                    If TabuladorRadioButton.Checked Then objCSV.Separator = ControlChars.Tab
                    If PuntoComaRadioButton.Checked Then objCSV.Separator = CChar(";")

                    'Se realiza el cargue del archivo en un datatable para luego validarlo.
                    objCSV.LoadCSV(CType(_File, FileStream).Name, chkEncabezado.Checked)
                    _Data = objCSV.DataTable.ToDataTable()
                Else 'Es XLS
                    Dim dsHojas As DataSet = objXLS.ImportExcelXLS(CType(_File, FileStream).Name, chkEncabezado.Checked)
                    _Data = dsHojas.Tables(0)
                End If

                Dim objProcesaCargue As ProcesaCargueClienteExterno = New ProcesaCargueClienteExterno()
                Dim trRespuestaCargue As New DesktopConfig.TypeResult

                Dim lisMsgError = New List(Of String)

                Dim CamposTipoLogDataTable = dbmArchiving.SchemaConfig.TBL_Campos_Tipo_log.DBFindByfk_Tipo_logfk_Entidadfk_ProyectoEliminado(TiposLogrow.id_Tipo_log, Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, False)
                Dim CamposDataLog = dbmArchiving.SchemaProcess.Get_Tabla_Log.DBExecute(Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto)

                _DataFile = New DataTable()

                For Each itemCampo As DataColumn In _Data.Columns
                    _DataFile.Columns.Add(itemCampo.ColumnName)
                Next

                If _Data.Rows.Count > 0 Then
                    For index As Integer = 0 To _Data.Rows.Count - 1
                        For Each itemCampo As DataColumn In _Data.Columns
                            Dim valorCampo = _Data.Rows(index)(itemCampo).ToString()
                            If itemCampo.ToString().Contains("Column") Then
                                Dim campoBuscar = itemCampo.ToString()
                                Dim ax_encontrado = CamposTipoLogDataTable.Where(Function(x) x.Nombre_Campo = campoBuscar.ToString()).ToList()
                                Dim CampoTipoLogRow As DBArchiving.SchemaConfig.TBL_Campos_Tipo_logRow = Nothing

                                If ax_encontrado.Count > 0 Then
                                    Dim valorDescripcion As String
                                    CampoTipoLogRow = ax_encontrado.First()
                                    valorCampo = _Data.Rows(index)(CampoTipoLogRow.Nombre_Campo).ToString()
                                    valorDescripcion = CampoTipoLogRow.Descripcion.ToString()

                                    Dim campoObligatorio = CampoTipoLogRow.Campo_Obligatorio
                                    Dim CampoObligatorioApellido As Boolean

                                    If ((valorDescripcion = "Identificador_Clase_Persona") And (valorCampo = "1")) Then
                                        CampoObligatorioApellido = True
                                    End If

                                    'Validar si el campo está vacío y si es requerido
                                    If (String.IsNullOrEmpty(valorCampo)) Then

                                        campoObligatorio = CampoTipoLogRow.Campo_Obligatorio

                                        If (valorDescripcion = "Primer_Apellido") Then
                                            If (CampoObligatorioApellido) Then
                                                campoObligatorio = CampoObligatorioApellido
                                            Else
                                                campoObligatorio = CampoTipoLogRow.Campo_Obligatorio
                                            End If
                                        End If

                                        If (campoObligatorio) Then
                                            lisMsgError.Add("Línea " & index + 1 & " : El campo " & CampoTipoLogRow.Descripcion & " es obligatorio")
                                        End If
                                    Else
                                        'Validar tamaño del campo
                                        Dim campoTamano = CInt(CampoTipoLogRow.Length_Fijo)
                                        If (valorCampo.Length > campoTamano) Then
                                            lisMsgError.Add("Línea " & index + 1 & " : El tamaño del campo " & CampoTipoLogRow.Descripcion & " es mayor al requerido")
                                        Else
                                            Dim caracterValido As String
                                            Dim AplicaTipoRepresentanteLegal As Boolean = False

                                            If ((valorDescripcion = "Identificador_Rol_Persona") And (valorCampo = "9")) Then
                                                AplicaTipoRepresentanteLegal = True
                                            End If

                                            If (valorDescripcion = "Tipo_Representante_Legal") Then
                                                If (AplicaTipoRepresentanteLegal) Then
                                                    caracterValido = CampoTipoLogRow.Caracteres_Validos
                                                Else
                                                    caracterValido = "0, "
                                                End If
                                            End If

                                            'Validar caracteres válidos
                                            If (Not (String.IsNullOrEmpty(CampoTipoLogRow.Caracteres_Validos))) Then
                                                Dim Listacaracteresvalidos() As String
                                                caracterValido = CampoTipoLogRow.Caracteres_Validos
                                                Dim contCoincide As Integer = 0
                                                Listacaracteresvalidos = caracterValido.Split(CChar(";"))

                                                For i As Integer = 0 To Listacaracteresvalidos.Count - 1
                                                    If (valorCampo = Listacaracteresvalidos(i)) Then
                                                        contCoincide = contCoincide + 1
                                                    End If
                                                Next
                                                If (contCoincide = 0) Then
                                                    lisMsgError.Add("Línea " & index + 1 & " : El valor del campo " & CampoTipoLogRow.Descripcion & " no coincide con el requerido")
                                                End If

                                                If ((valorDescripcion = "Fecha_Desembolso") Or (valorDescripcion = "Fecha_Firma_Fisico")) Then
                                                    'Formato Fecha
                                                End If
                                            End If
                                            ltCamposLog.Add(valorCampo)
                                        End If
                                    End If
                                Else
                                    ltCamposLog.Add(valorCampo)
                                End If
                            End If
                        Next
                        _DataFile.Rows.Add(ltCamposLog.ToArray())
                        '_DataFile.Rows(index)("fk_Entidad") = Program.RiskGlobal.Entidad
                        '_DataFile.Rows(index)("fk_Proyecto") = Program.RiskGlobal.Proyecto
                        '_DataFile.Rows(index)("fk_Tipo_Log") = TiposLogrow.id_Tipo_log

                        ltCamposLog.Clear()
                    Next
                    Program.Sesion.Parameter("_ErroresEstructuraLog") = lisMsgError
                End If

                _DataRegistros = _DataFile.Rows.Count
                _DataColumnas = _DataFile.Columns.Count

                If (lisMsgError.Count = 0) Then

                    If _DataRegistros > 0 And _DataColumnas > 1 Then
                        CargueBackgroundWorker.ReportProgress(0)

                        If _TipoCargue = DesktopConfig.TipoCargue.Universal Then
                            trRespuestaCargue = objProcesaCargue.ProcesaUniversal(_DataFile, NombreArchivo, Me.CargueBackgroundWorker)
                        ElseIf _TipoCargue = DesktopConfig.TipoCargue.Parcial Then
                            trRespuestaCargue = objProcesaCargue.ProcesaParcial(_DataFile, NombreArchivo, Entidad, Esquema, Me.CargueBackgroundWorker)
                        ElseIf _TipoCargue = DesktopConfig.TipoCargue.Actualizacion Then
                            trRespuestaCargue = objProcesaCargue.ProcesaActualizacion(_DataFile, NombreArchivo, Entidad, Esquema, Me.CargueBackgroundWorker)
                        ElseIf _TipoCargue = DesktopConfig.TipoCargue.Deceval Then
                            trRespuestaCargue = objProcesaCargue.ProcesaDeceval(_DataFile, NombreArchivo, Entidad, Esquema, Me.CargueBackgroundWorker)
                        End If

                        If trRespuestaCargue.Result Then
                            trReturn = trRespuestaCargue
                        Else
                            trReturn.Result = False
                            trReturn.Parameters = trRespuestaCargue.Parameters
                            trReturn.Resumen = trRespuestaCargue.Resumen
                        End If
                    Else
                        trReturn.Result = False
                        lisMsgError.Add("- Archivo no válido. Por favor verifique lo siguiente:")
                        lisMsgError.Add("- 1. Que el archivo contenga datos.")
                        lisMsgError.Add("- 2. El carácter de separación sea el adecuado.")
                        trReturn.Parameters = lisMsgError
                    End If
                Else
                    Dim bRegistroValido As Boolean
                    trReturn.Resumen = New DesktopConfig.ValidacionCargue() With {.NoValido = 1}

                    trReturn.Parameters = lisMsgError
                    objProcesaCargue.ReportaError("Error en la estructura del log", lisMsgError, trReturn, bRegistroValido)
                    trReturn.Result = False
                End If

            Catch ex As Exception
                Dim lisMsgError = New List(Of String)
                lisMsgError.Add("- Error: " & ex.Message)
                trReturn.Result = False
                trReturn.Parameters = lisMsgError
            End Try

            Return trReturn
        End Function

        Private Function getconfig() As DataTable
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            Dim ListProyectos As New List(Of String)
            Dim dt As DataTable = Nothing
            Dim dv As DataView = Nothing
            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                Dim conn As New SqlConnection
                conn.ConnectionString = (dbmCore.DataBase.ConnectionString)
                Dim sqlquery As String = " SELECT [fk_Esquema],[fk_Tipologia] FROM [DB_Miharu.Core].[Config].[TBL_Documento]WHERE (fk_entidad = @fk_entidad) AND fk_Proyecto = @fk_proyecto "
                Dim SqlParameter = New SqlParameter() _
                {
                    New SqlParameter("@fk_entidad", Program.RiskGlobal.Entidad),
                    New SqlParameter("@fk_proyecto", Program.RiskGlobal.Proyecto)
                                }
                dt = ExecuteQuery(sqlquery, conn, SqlParameter)
                Return dt

            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            End Try
            Return dt
        End Function

        Private Function ValidaFormatoCorrecto_Log(IdTipoLog As Integer, NombreArchivo As String, RowTipoLog As DBArchiving.SchemaConfig.TBL_Tipos_LogRow) As Boolean
            Dim retorno As Boolean = True
            Dim NombreArchivo_Aux As String = ""
            If (TiposLogrow.Valida_Extension And NombreArchivo.Contains(".")) Then
                NombreArchivo_Aux = NombreArchivo.Remove(NombreArchivo.LastIndexOf("."), NombreArchivo.Length - NombreArchivo.LastIndexOf("."))
            Else
                NombreArchivo_Aux = NombreArchivo
            End If

            'Try
            '    If (RowTipoLog IsNot Nothing) Then
            '        ValidacionesSplit(retorno, TiposLogrow.Validaciones_ArchivoPlano.Split(";"c), TiposLogrow.LengthValidaciones_ArchivoPlano.Split(";"c), NombreArchivo_Aux)
            '    End If
            'Catch generatedExceptionName As Exception

            '    Throw
            'End Try
            Return retorno
        End Function

        Private Function ValidacionesSplit(ByRef retorno As Boolean, StrValidacionColumn As String(), StrLengthValidaciones As String(), NombreArchivo_Aux As String) As Object
            Try
                If (StrValidacionColumn.Count() > 0) Then
                    For index As Integer = 0 To StrValidacionColumn.Count() - 1
                        Dim AuxItemValidacion As String = Nothing
                        If (StrValidacionColumn(index).ToString().Contains("FECHA")) Then
                            AuxItemValidacion = StrValidacionColumn(index).ToString()
                        Else
                            AuxItemValidacion = StrValidacionColumn(index).ToString().ToUpper()
                        End If
                        Dim LengthItemValidacion As String() = StrLengthValidaciones(index).Split("-"c)
                        Dim lengthInicio As Integer = CInt(LengthItemValidacion(0).ToString())
                        Dim lengthFinal As Integer = CInt(LengthItemValidacion(1).ToString())
                        Dim auxArchivoVal As String = Nothing

                        If (AuxItemValidacion.Contains("*")) Then
                            Dim OrAnd = AuxItemValidacion.Split("*"c)
                            If (OrAnd.Contains("OR") And Not OrAnd.Contains("AND")) Then

                                OrAnd = OrAnd.Where(Function(x) Not x.Contains("OR")).ToArray()

                                For Each itemOr_loopVariable In OrAnd
                                    auxArchivoVal = NombreArchivo_Aux.Substring(lengthInicio, (lengthFinal - lengthInicio)).ToUpper()
                                    If (auxArchivoVal = itemOr_loopVariable.ToUpper()) Then
                                        retorno = True
                                    End If
                                Next
                            End If
                        ElseIf (AuxItemValidacion.Contains("<FECHA>")) Then
                            Dim dtAux As New DateTime()
                            auxArchivoVal = NombreArchivo_Aux.Substring(lengthInicio, (lengthFinal - lengthInicio))

                            AuxItemValidacion = AuxItemValidacion.Replace("<FECHA>", "")
                            retorno = DateTime.TryParseExact(auxArchivoVal, AuxItemValidacion, Nothing, DateTimeStyles.None, dtAux)


                            If (retorno = False) Then
                                ' TODO: might not be correct. Was : Exit For
                                Exit For
                            End If
                        ElseIf AuxItemValidacion.Contains("<OFICINA>") Then
                            AuxItemValidacion = AuxItemValidacion.Replace("<FECHA>", "")

                            Continue For
                        ElseIf AuxItemValidacion.Contains("<NUMERO>") Then
                            AuxItemValidacion = AuxItemValidacion.Replace("<NUMERO>", "")

                            Continue For
                        ElseIf AuxItemValidacion.Contains("<ANIO>") Then
                            AuxItemValidacion = AuxItemValidacion.Replace("<ANIO>", "")
                            Dim anios = Enumerable.Range(1900, DateTime.Now.Year)
                            Dim encontrado = anios.Where(Function(x) x.ToString() = AuxItemValidacion)

                            If encontrado.Count() = 0 Then
                                retorno = False
                            End If
                        Else
                            auxArchivoVal = NombreArchivo_Aux.Substring(lengthInicio, (lengthFinal - lengthInicio)).ToUpper()
                            If (auxArchivoVal <> AuxItemValidacion) Then
                                retorno = False
                                ' TODO: might not be correct. Was : Exit For
                                Exit For
                            End If
                        End If
                    Next
                End If
            Catch ex As Exception
                retorno = False
            End Try

            Return retorno
        End Function

        Private Sub ValidarExiste_ArchivoCargue(NombreArchivo As String, dbmArchiving As DBArchivingDataBaseManager)
            Try

                Dim Cargue_valido = dbmArchiving.SchemaConfig.TBL_Cargue.DBFindByfk_Entidadfk_Proyectofk_Tipo_LogArchivo_CargueValido(Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, TiposLogrow.id_Tipo_log, NombreArchivo, True)

                If (Cargue_valido.[Select]("Archivo_Cargue = '" + NombreArchivo + "'").Count() > 0) AndAlso Me.validaArchivoCargue = False Then
                    DesktopMessageBoxControl.DesktopMessageShow("El nombre de archivo que estan intentando cargar ya se encuentra registrado con un Cargue Valido.", "Archivo de Cargue existente", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                    Return
                End If

                If (Cargue_valido.Count > 0) AndAlso Me.validaArchivoCargue Then
                    DesktopMessageBoxControl.DesktopMessageShow("El nombre de archivo que estan intentando cargar ya se encuentra registrado con un Cargue Valido.", "Archivo de Cargue existente", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                    _File = Nothing
                    ArchivoDesktopTextBox.Text = ""
                    Return
                Else

                    Me.CargandoPictureBox.Visible = True
                    Timer1.Enabled = True
                    HabilitarControles(False)
                    CheckForIllegalCrossThreadCalls = False
                    Me.CargueBackgroundWorker.RunWorkerAsync(NombreArchivo)
                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("ValidarExiste ArchivoCargue", ex)
            End Try
        End Sub
#End Region

#Region " Eventos "

        Private Sub FormCargue_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
            If Me.CargueBackgroundWorker.IsBusy Then
                Me.CargueBackgroundWorker.CancelAsync()
            End If
        End Sub

        Private Sub FormCargue_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
            Thread.CurrentThread.CurrentCulture = New CultureInfo("es-MX")

            Try
                'Cambia Titulo
                Me.Text = "Cargue " & _TipoCargue.ToString()
                CargaDatos()

                If _TipoCargue = DesktopConfig.TipoCargue.Deceval Then
                    OpcionesSeparadorGroupBox.Enabled = False
                    chkEncabezado.Enabled = False
                    EsquemaFacLabel.Enabled = False
                    EsquemaFacturacionComboBox.Enabled = False
                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("FormCargue_Load", ex)
            End Try
        End Sub

        Private Sub BuscarArchivoButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BuscarArchivoButton.Click
            BuscarArchivo()
        End Sub

        Private Sub ArchivoDesktopTextBox_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ArchivoDesktopTextBox.Click
            BuscarArchivo()
        End Sub

        Private Sub CargarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CargarButton.Click
            Timer1.Enabled = True

            Me.CargueBackgroundWorker.WorkerSupportsCancellation = True

            If Me.ArchivoDesktopTextBox.Text = "" Then
                DesktopMessageBoxControl.DesktopMessageShow("Error al cargar Log, Archivo a cargar Vacio!!", "ERROR", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                Return
            End If

            Dim strSeleccionado As String = Me.DesktopComboBoxControlTiposLog.Text
            Dim validaExtension As Boolean = False
            Dim Extension As String = ""
            Dim ExtensionBD As String = ""

            Dim dbmArchiving = New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            Try
                dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

                If Not IsNothing(_File) Then
                    Dim NombreArchivo As String = Path.GetFileName(CType(_File, FileStream).Name)
                    Dim Entidad As String = CStr(Program.RiskGlobal.Entidad)
                    Dim Esquema As String = CStr(Program.RiskGlobal.Proyecto)

                    If _TipoCargue <> DesktopConfig.TipoCargue.Deceval Then
                        If CStr(EsquemaFacturacionComboBox.SelectedValue) <> "-1" Then
                            Dim arrEsquema = EsquemaFacturacionComboBox.SelectedValue.ToString().Split(CChar("-"))
                            Entidad = CStr(arrEsquema(0))
                            Esquema = CStr(arrEsquema(1))
                        Else
                            DesktopMessageBoxControl.DesktopMessageShow("Debe seleccionar un esquema de facturación.", "Archivo de cargue", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                        End If
                    End If

                    'Valida si el nombre del archivo ya fué cargado.
                    Dim dtCargues = dbmArchiving.SchemaRisk.TBL_Cargue.DBFindByfk_Entidadfk_ProyectoArchivo_Cargue(Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, NombreArchivo)
                    If dtCargues.Count > 0 Then
                        DesktopMessageBoxControl.DesktopMessageShow("El nombre de archivo que estan intentando cargar ya se encuentra registrado, por favor seleccione otro archivo de cargue.", "Archivo de Cargue existente", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                        _File = Nothing
                        ArchivoDesktopTextBox.Text = ""
                    Else
                        HabilitarControles(False)
                        Me.CargueBackgroundWorker.RunWorkerAsync(NombreArchivo & "-" & Entidad & "-" & Esquema)
                    End If
                Else
                    DesktopMessageBoxControl.DesktopMessageShow("Debe seleccionar un archivo de cargue.", "Archivo de cargue", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                End If

                If (_File IsNot Nothing) Then

                    Dim NombreArchivo As String = Path.GetFileName(DirectCast(_File, FileStream).Name)

                    validaExtension = TiposLogrow.Valida_Extension
                    ExtensionBD = TiposLogrow.Extension_Archivo
                    validaArchivoCargue = TiposLogrow.Valida_ArchivoCargue

                    If Not String.IsNullOrEmpty(TiposLogrow.Salto_Primeras_Lineas.ToString()) Then
                        Me.SaltoPrimasLineas = Convert.ToInt32(TiposLogrow.Salto_Primeras_Lineas.ToString())
                    End If


                    If validaExtension Then
                        Extension = NombreArchivo.Substring(NombreArchivo.LastIndexOf("."))

                        If Extension.ToString() = "" Then
                            DesktopMessageBoxControl.DesktopMessageShow("La extensión del archivo no es correcta para este Tipo de Log, por favor seleccione otro archivo de cargue.", "Error de Extensión", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                            Return
                        Else
                            Dim StrSplit As String() = ExtensionBD.Split("|"c)
                            Dim encontradoSplit = StrSplit.Where(Function(x) x.ToString().ToUpper() = Extension.ToUpper()).[Select](Function(x) x.ToString())

                            If encontradoSplit.Count() = 0 Then
                                DesktopMessageBoxControl.DesktopMessageShow("La extensión del archivo no es correcta para este Tipo de Log, por favor seleccione otro archivo de cargue.", "Error de Extensión", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                                Return
                            End If

                            Me.ExtensionGlobal = Extension
                        End If
                    End If


                    If ValidaFormatoCorrecto_Log(TiposLogrow.id_Tipo_log, NombreArchivo, TiposLogrow) = False Then
                        DesktopMessageBoxControl.DesktopMessageShow("El formato del archivo no es correcto, por favor verifique y cargue nuevamente!!", "Format Archivo de cargue", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                        Return
                    End If

                    ValidarExiste_ArchivoCargue(NombreArchivo, dbmArchiving)
                End If

                'Timer1.Enabled = False

            Catch ex As Exception
                'Timer1.Enabled = False
                DesktopMessageBoxControl.DesktopMessageShow("CargarButton_Click", ex)
            Finally
                dbmArchiving.Connection_Close()
            End Try

        End Sub

        Private Sub CerrarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CerrarButton.Click
            If CargueBackgroundWorker.IsBusy Then
                Dim resultado = DesktopMessageBoxControl.DesktopMessageShow("En este momento se esta procesando un cargue de archivo, ¿Desea cancelar la operación?", "Cancelación de cargue", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, False)
                If resultado = DialogResult.OK Then
                    Me.CargueBackgroundWorker.CancelAsync()
                    Me.Close()
                End If
            Else
                Me.Close()
            End If
        End Sub

        Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As EventArgs) Handles Timer1.Tick
            If Timer1.Enabled Then
                Segundos += 1

                If Segundos = 60 Then
                    Segundos = 0
                    Minuto += 1
                End If

                If Minuto = 60 Then
                    Minuto = 0
                    Hora += 1
                End If

                TiempoLabel.Text = String.Format("{0:00}", Hora) + ":" + String.Format("{0:00}", Minuto) + ":" + String.Format("{0:00}", Segundos)
            End If
        End Sub

        Private Sub DesktopComboBoxControlTiposLog_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles DesktopComboBoxControlTiposLog.SelectedIndexChanged
            Dim x As String
            x = DesktopComboBoxControlTiposLog.SelectedValue.ToString()
            If x <> "-1" Then
                TiposLogrow = CType(TiposLogDataTable.Select("id_Tipo_Log = " + DesktopComboBoxControlTiposLog.SelectedValue.ToString())(0), DBArchiving.SchemaConfig.TBL_Tipos_LogRow)
            End If
        End Sub

#Region "BackgroundWorker"

        Private Sub CargueBackgroundWorker_DoWork(ByVal sender As System.Object, ByVal e As DoWorkEventArgs) Handles CargueBackgroundWorker.DoWork
            Dim worker As BackgroundWorker = CType(sender, BackgroundWorker)
            If worker.CancellationPending Then e.Cancel = True

            'Se obtiene entidad, esquema
            Dim Parametros = e.Argument.ToString().Split(CChar("-"))
            Dim NombreArchivo As String = ""

            If Parametros.Length > 3 Then
                'Dim Nombre = Parametros.Length - 3

                For i As Integer = 0 To Parametros.Length - 3 Step 1
                    NombreArchivo = Parametros(i)
                Next

            Else
                NombreArchivo = Parametros(0)
            End If

            Dim Entidad = CShort(Parametros(Parametros.Length - 2))
            Dim Esquema = CShort(Parametros(Parametros.Length - 1))
            trResultado = CargarArchivo(NombreArchivo, Entidad, Esquema)
            Me.CargueBackgroundWorker.ReportProgress(CargueProgressBar.Maximum)
        End Sub

        Private Sub CargueBackgroundWorker_ProgressChanged(ByVal sender As System.Object, ByVal e As ProgressChangedEventArgs) Handles CargueBackgroundWorker.ProgressChanged
            If e.ProgressPercentage = 0 Then
                CargandoPictureBox.Visible = True

                CargueProgressBar.Minimum = 0
                CargueProgressBar.Maximum = _DataRegistros
                TotalRegistrosLabel.Text = _DataRegistros.ToString()

                If _EstadoProceso = 0 Then
                    ProcesadosTituloLabel.Text = "Validados:"
                ElseIf _EstadoProceso = 1 Then
                    ProcesadosTituloLabel.Text = "Procesados:"
                End If
                _EstadoProceso = CShort(_EstadoProceso + 1)

            Else
                'Inicio Proceso
                CargueProgressBar.Value = e.ProgressPercentage
                ProcesadosLabel.Text = e.ProgressPercentage.ToString()
            End If
        End Sub

        Private Sub CargueBackgroundWorker_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As RunWorkerCompletedEventArgs) Handles CargueBackgroundWorker.RunWorkerCompleted
            CargandoPictureBox.Visible = False
            Timer1.Enabled = False

            ArchivoDesktopTextBox.Text = ""
            _File = Nothing

            If trResultado.Result Then
                If Not IsNothing(trResultado.OT) Then
                    If Not trResultado.OT.IsNull Then
                        DesktopMessageBoxControl.DesktopMessageShow("Se generó el cargue con OT: " & trResultado.OT.ToString(), "Cargue Parcial [OT: " & trResultado.OT.ToString() & "]", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                    End If
                End If

                DatosCargadosDesktopDataGridView.DataSource = _DataFile
                CargueProgressBar.Value = CargueProgressBar.Maximum
                DesktopMessageBoxControl.DesktopMessageShow("Datos cargados éxitosamente", "Cargue de datos", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
            Else
                If Not IsNothing(trResultado.Parameters) Then
                    Dim myError As New FormResultadoValidacion(trResultado)
                    CargueBackgroundWorker.Dispose()
                    myError.ShowDialog()
                End If
            End If

            _File = Nothing
        End Sub

        Private Function ExecuteQuery(ByVal s As String, ByVal condb As SqlConnection, ByVal ParamArray params() As SqlParameter) As DataTable
            Dim dt As DataTable = Nothing
            Using da As New System.Data.SqlClient.SqlDataAdapter(s, condb)
                dt = New DataTable
                If params.Length > 0 Then
                    da.SelectCommand.Parameters.AddRange(params)
                End If
                If da.SelectCommand.Connection.State <> ConnectionState.Open Then da.SelectCommand.Connection.Open()
                da.Fill(dt)
            End Using
            Return dt
        End Function

#End Region

#End Region
    End Class

End Namespace