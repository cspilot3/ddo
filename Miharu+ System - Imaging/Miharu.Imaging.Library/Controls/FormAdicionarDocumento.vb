Imports System.Linq
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Controls
Imports Miharu.Desktop.Library.Config
Imports Miharu.Tools.Progress
Imports Slyg.Tools.Imaging
Imports Miharu.FileProvider.Library

Namespace Controls

    Public Class FormAdicionarDocumento

#Region " Declaraciones "

        Private _ImagenOpenFileDialog As New OpenFileDialog()
        Private Const _MaxThumbnailWidth As Integer = 60
        Private Const _MaxThumbnailHeight As Integer = 80
        Private _EsquemaId As Short = 0
        Private _Expediente As Integer = 0
        Private _Folder As Short = 0
        Private _Locked As Boolean

#End Region

#Region " Constructores "

        Public Sub New(ByVal id_Expediente As Integer, ByVal id_Folder As Short)
            ' Llamada necesaria para el diseñador.
            InitializeComponent()
            _Expediente = id_Expediente
            _Folder = id_Folder
        End Sub

#End Region

#Region " Eventos "

        Private Sub FormAdicionarSolicitud_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            FindEsquema()
            FillCombos()
        End Sub

        Private Sub CargarImagenButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CargarImagenButton.Click
            MostrarImagen()
        End Sub

        Private Sub AdicionarImagenButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AdicionarImagenButton.Click
            If cmbFechaProceso.Items.Count = 0 Then
                DesktopMessageBoxControl.DesktopMessageShow("No se encuentran Fechas de proceso para agregar el documento", "Adicionar Documento", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                Return
            End If

            If cmbOT.Items.Count = 0 Then
                DesktopMessageBoxControl.DesktopMessageShow("No se encuentran OTs para agregar el documento", "Adicionar Documento", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                Return
            End If

            If Not _Locked Then
                _Locked = True
                Try
                    If (ValidarDocumento()) Then
                        Dim fk_Documento = CShort(cmbConfirmarTipologia.SelectedValue)
                        Dim Fecha_Proceso = CDate(cmbFechaProceso.SelectedValue)
                        Dim Ot = CInt(cmbOT.SelectedValue)

                        AdicionarDocumento(Ot, Fecha_Proceso, _Expediente, _Folder, fk_Documento)
                        Me.Close()
                    End If
                Catch ex As Exception
                    Throw
                End Try
                _Locked = False
            End If
        End Sub

#End Region

#Region " Metodos "

        Private Sub FillCombos()
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)

                dbmCore.Connection_Open(Program.Sesion.Usuario.id)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                Dim Tipologias = dbmCore.SchemaConfig.CTA_Documento.DBFindByfk_Entidadfk_Proyectofk_EsquemaEliminado(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, _EsquemaId, False)
                If Tipologias.Rows.Count > 0 Then
                    cmbConfirmarTipologia.Enabled = True
                    cmbTipologia.Enabled = True
                    Utilities.LlenarCombo(cmbTipologia, Tipologias, Tipologias.id_DocumentoColumn.ColumnName, Tipologias.Nombre_DocumentoColumn.ColumnName)
                    Utilities.LlenarCombo(cmbConfirmarTipologia, Tipologias, Tipologias.id_DocumentoColumn.ColumnName, Tipologias.Nombre_DocumentoColumn.ColumnName)

                Else
                    cmbConfirmarTipologia.DataSource = Nothing
                    cmbTipologia.DataSource = Nothing
                    cmbConfirmarTipologia.Enabled = False
                    cmbTipologia.Enabled = False
                End If

                Dim FechasProceso = dbmImaging.SchemaProcess.TBL_Fecha_Proceso.DBFindByfk_Entidadfk_ProyectoCerradofk_Entidad_Procesamiento(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, False, Program.DesktopGlobal.PuestoTrabajoRow.fk_Entidad)
                Utilities.LlenarCombo(cmbFechaProceso, FechasProceso, FechasProceso.Fecha_ProcesoColumn.ColumnName, FechasProceso.Fecha_ProcesoColumn.ColumnName)

            Catch
                Throw
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

        'Private Sub CargarOTs()
        '    Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

        '    Try
        '        dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
        '        dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

        '        Dim FechaProceso = CInt(cmbFechaProceso.SelectedValue)
        '        Dim OT = dbmImaging.SchemaProcess.TBL_OT.DBFindByfk_Entidadfk_Proyectofk_fecha_procesoCerradofk_Entidad_Procesamiento(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, FechaProceso, False, Program.DesktopGlobal.PuestoTrabajoRow.fk_Entidad)
        '        Utilities.LlenarCombo(cmbOT, OT, OT.id_OTColumn.ColumnName, OT.id_OTColumn.ColumnName)

        '    Catch
        '        Throw
        '    Finally
        '        If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
        '    End Try
        'End Sub

        Private Sub FindEsquema()
            ' Filtrar
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing

            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                Dim data = dbmCore.SchemaProcess.TBL_Expediente.DBGet(_Expediente)
                _EsquemaId = data(0).fk_Esquema
            Catch
                Throw
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            End Try
        End Sub

        Private Sub AdicionarDocumento(ByVal nIdOT As Integer, ByVal nFechaProceso As Date, ByVal nExpediente As Long, ByVal nFolder As Short, ByVal nDocumento As Short)
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim dbmArchiving As DBArchiving.DBArchivingDataBaseManager = Nothing
            Dim manager As FileProviderManager = Nothing
            Dim ProgressForm As New FormProgress

            If (_ImagenOpenFileDialog.FileNames.Length <= 0) Then
                DesktopMessageBoxControl.DesktopMessageShow("Ingrese la imágen por favor!!.", "Adicionar Items", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                Exit Sub
            End If

            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmArchiving = New DBArchiving.DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)

                dbmCore.Connection_Open(Program.Sesion.Usuario.id)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
                dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

                manager = New FileProviderManager(nExpediente, nFolder, dbmImaging, Program.Sesion.Usuario.id)
                manager.Connect()

                dbmCore.Transaction_Begin()
                dbmImaging.Transaction_Begin()
                dbmArchiving.Transaction_Begin()
                manager.TransactionBegin()

                '/*********************** Crear Cargue *************************/
                Dim CargueType As New DBImaging.SchemaProcess.TBL_CargueType()
                CargueType.fk_Entidad = Program.ImagingGlobal.Entidad
                CargueType.fk_Proyecto = Program.ImagingGlobal.Proyecto
                CargueType.fk_Estado = DBCore.EstadoEnum.Creado
                CargueType.fk_Entidad_Procesamiento = Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad
                CargueType.fk_Sede_Procesamiento_Cargue = Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede
                CargueType.fk_Centro_Procesamiento_Cargue = Program.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento
                CargueType.fk_Entidad_Servidor = Program.DesktopGlobal.ServidorImagenRow.fk_Entidad
                CargueType.fk_Servidor = Program.DesktopGlobal.ServidorImagenRow.id_Servidor
                CargueType.fk_OT = nIdOT
                CargueType.Fecha_Proceso = nFechaProceso
                CargueType.Observaciones = "CP-" & Program.DesktopGlobal.CentroProcesamientoRow.Nombre_Centro_Procesamiento
                CargueType.fk_Usuario_Log = Program.Sesion.Usuario.id

                CargueType.id_Cargue = dbmImaging.SchemaProcess.PA_Guardar_TBL_Cargue.DBExecute(
                    CargueType.fk_Entidad,
                    CargueType.fk_Proyecto,
                    CargueType.fk_Estado,
                    CargueType.fk_Entidad_Procesamiento,
                    CargueType.fk_Sede_Procesamiento_Cargue,
                    CargueType.fk_Centro_Procesamiento_Cargue,
                    CargueType.fk_Entidad_Servidor,
                    CargueType.fk_Servidor,
                    CargueType.fk_OT,
                    CargueType.Fecha_Proceso,
                    CargueType.Observaciones,
                    CargueType.fk_Usuario_Log
                    )

                '/***********************Fin Crear Cargue *************************/
                '/*********************** Crar Paquete   **************************/
                Dim PaqueteType As New DBImaging.SchemaProcess.TBL_Cargue_PaqueteType()
                Dim ruta As String = _ImagenOpenFileDialog.FileName
                PaqueteType.fk_Cargue = CargueType.id_Cargue
                PaqueteType.id_Cargue_Paquete = dbmImaging.SchemaProcess.TBL_Cargue_Paquete.DBNextId(CargueType.id_Cargue)

                PaqueteType.fk_Estado = DBCore.EstadoEnum.Indexacion

                PaqueteType.fk_Usuario_Log = Program.Sesion.Usuario.id
                PaqueteType.Fecha_Proceso = CargueType.Fecha_Cargue
                PaqueteType.Path_Cargue_Paquete = ruta
                PaqueteType.Bloqueado = False
                PaqueteType.Data_Path = ruta
                PaqueteType.fk_Sede_Procesamiento_Asignada = Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede_Asignada
                PaqueteType.fk_Centro_Procesamiento_Asignado = Program.DesktopGlobal.CentroProcesamientoRow.fk_Centro_Procesamiento_Asignado

                dbmImaging.SchemaProcess.TBL_Cargue_Paquete.DBInsert(PaqueteType)
                '/*********************** Fin Crar Paquete   ************************/
                ' Crear el File
                Dim idFile As Short = dbmCore.SchemaImaging.TBL_File.DBNextId_for_fk_File(nExpediente, nFolder, 1)
                Const idVersion As Short = CShort(1)
                Dim Folios = CShort(ImageManager.GetFolios(_ImagenOpenFileDialog.FileName))
                Dim NewImagingFile = New DBCore.SchemaImaging.TBL_FileType
                NewImagingFile.fk_Expediente = nExpediente
                NewImagingFile.fk_Folder = nFolder
                NewImagingFile.fk_File = idFile
                NewImagingFile.id_Version = idVersion
                NewImagingFile.File_Unique_Identifier = Guid.NewGuid()
                NewImagingFile.Folios_Documento_File = Folios
                NewImagingFile.Tamaño_Imagen_File = 0
                NewImagingFile.Nombre_Imagen_File = ""
                NewImagingFile.Key_Cargue_Item = ""
                NewImagingFile.Save_FileName = ""
                NewImagingFile.fk_Content_Type = Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida
                NewImagingFile.fk_Usuario_Log = Program.Sesion.Usuario.id
                NewImagingFile.Validaciones_Opcionales = False
                dbmCore.SchemaImaging.TBL_File.DBInsert(NewImagingFile)

                ' Crear File em Process
                Dim NewProcessFile = New DBCore.SchemaProcess.TBL_FileType
                NewProcessFile.fk_Expediente = nExpediente
                NewProcessFile.fk_Folder = nFolder
                NewProcessFile.id_File = idFile
                NewProcessFile.File_Unique_Identifier = Guid.NewGuid()
                NewProcessFile.fk_Documento = nDocumento
                NewProcessFile.Folios_File = Folios
                NewProcessFile.Monto_File = CDec("0.00")
                NewProcessFile.CBarras_File = ""
                dbmCore.SchemaProcess.TBL_File.DBInsert(NewProcessFile)

                ' File Imaging
                Dim FileImagingType = New DBImaging.SchemaProcess.TBL_FileType
                FileImagingType.fk_Expediente = nExpediente
                FileImagingType.fk_Folder = nFolder
                FileImagingType.fk_File = idFile
                FileImagingType.id_Version = idVersion
                FileImagingType.Actualizado = False
                FileImagingType.Reprocesado = False
                FileImagingType.fk_Documento = nDocumento
                dbmImaging.SchemaProcess.TBL_File.DBInsert(FileImagingType)


                Dim NextEstado = CType(dbmImaging.SchemaProcess.PA_Next_Estado.DBExecute(nDocumento, DBCore.EstadoEnum.Indexacion), DBCore.EstadoEnum)
                '---------------------------------------------------------------------------
                ' Crea File en File Estado
                '------------------------------------------------------------0---------------
                Dim FileEstadoType = New DBCore.SchemaProcess.TBL_File_EstadoType
                FileEstadoType.fk_Expediente = nExpediente
                FileEstadoType.fk_Folder = nFolder
                FileEstadoType.fk_File = idFile
                FileEstadoType.Modulo = DesktopConfig.Modulo.Imaging
                FileEstadoType.fk_Estado = NextEstado
                FileEstadoType.fk_Usuario = Program.Sesion.Usuario.id
                FileEstadoType.Fecha_Log = DateTime.Now

                dbmCore.SchemaProcess.TBL_File_Estado.DBInsert(FileEstadoType)

                '---------------------------------------------------------------------------
                ' Actualizar Dashboard
                '---------------------------------------------------------------------------
                If (NextEstado <> DBCore.EstadoEnum.Indexado) Then
                    Dim CapDashboardType = New DBImaging.SchemaProcess.TBL_Dashboard_CapturasType()

                    CapDashboardType.fk_Expediente = nExpediente
                    CapDashboardType.fk_Folder = nFolder
                    CapDashboardType.fk_File = idFile
                    CapDashboardType.fk_Documento = nDocumento
                    CapDashboardType.fk_Cargue = CargueType.id_Cargue
                    CapDashboardType.fk_Cargue_Paquete = PaqueteType.id_Cargue_Paquete
                    CapDashboardType.fk_Entidad_Procesamiento = CargueType.fk_Entidad_Procesamiento
                    CapDashboardType.fk_Sede_Procesamiento = Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede
                    CapDashboardType.fk_Centro_Procesamiento = Program.DesktopGlobal.CentroProcesamientoRow.fk_Centro_Procesamiento_Asignado
                    CapDashboardType.fk_Entidad = Program.ImagingGlobal.Entidad
                    CapDashboardType.fk_Proyecto = Program.ImagingGlobal.Proyecto
                    CapDashboardType.fk_Estado = NextEstado
                    CapDashboardType.fk_OT = CargueType.fk_OT
                    dbmImaging.SchemaProcess.TBL_Dashboard_Capturas.DBInsert(CapDashboardType)

                    ' Actualizar validaciones opcionales
                    Dim ValidacionesDataTable = dbmImaging.SchemaConfig.TBL_Validacion.DBFindByfk_Documentofk_Etapa_Captura(nDocumento, DBImaging.EnumEtapaCaptura.Opcional, 1, New DBImaging.SchemaConfig.TBL_ValidacionEnumList(DBImaging.SchemaConfig.TBL_ValidacionEnum.id_Validacion, True))
                    dbmImaging.SchemaProcess.TBL_Dashboard_Validaciones.DBDelete(nExpediente, nFolder, idFile)

                    If (ValidacionesDataTable.Count > 0) Then
                        Dim CargueDataTable = dbmImaging.SchemaProcess.TBL_Cargue.DBGet(CargueType.id_Cargue)
                        Dim PaqueteDataTable = dbmImaging.SchemaProcess.TBL_Cargue_Paquete.DBGet(CargueType.id_Cargue, PaqueteType.id_Cargue_Paquete)

                        Dim ValDashboardType = New DBImaging.SchemaProcess.TBL_Dashboard_ValidacionesType()
                        ValDashboardType.fk_Expediente = nExpediente
                        ValDashboardType.fk_Folder = nFolder
                        ValDashboardType.fk_File = idFile
                        ValDashboardType.fk_Documento = nDocumento
                        ValDashboardType.fk_Cargue = CargueType.id_Cargue
                        ValDashboardType.fk_Cargue_Paquete = PaqueteType.id_Cargue_Paquete
                        ValDashboardType.fk_Entidad_Procesamiento = CargueDataTable(0).fk_Entidad_Procesamiento
                        ValDashboardType.fk_Sede_Procesamiento = PaqueteDataTable(0).fk_Sede_Procesamiento_Asignada
                        ValDashboardType.fk_Centro_Procesamiento = PaqueteDataTable(0).fk_Centro_Procesamiento_Asignado
                        ValDashboardType.fk_Entidad = CargueDataTable(0).fk_Entidad
                        ValDashboardType.fk_Proyecto = CargueDataTable(0).fk_Proyecto
                        ValDashboardType.Procesado = False
                        dbmImaging.SchemaProcess.TBL_Dashboard_Validaciones.DBInsert(ValDashboardType)
                    End If
                End If
                '---------------------------------------------------------------------------
                'Crea File en Storage
                manager.CreateItem(nExpediente, nFolder, idFile, idVersion, NewImagingFile.fk_Content_Type, NewImagingFile.File_Unique_Identifier)

                Dim FolioIndex As Short = 1

                Dim Formato = Utilities.getEnumFormat(Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida)
                Dim Compresion = Utilities.getEnumCompression(CType(Program.ImagingGlobal.ProyectoImagingRow.id_Formato_Imagen_Salida, DesktopConfig.FormatoImagenEnum))

                For Folio = CShort(1) To Folios

                    Dim dataImage = ImageManager.GetFolioData(_ImagenOpenFileDialog.FileName, Folio, Formato, Compresion)
                    Dim dataImageThumbnail = ImageManager.GetThumbnailData(_ImagenOpenFileDialog.FileName, Folio, Folio, _MaxThumbnailWidth, _MaxThumbnailHeight)

                    manager.CreateFolio(nExpediente, nFolder, idFile, idVersion, Folio, dataImage, dataImageThumbnail(0), False)

                    If (Program.ImagingGlobal.ProyectoImagingRow.Aplica_Fisico) Then
                        'Actualizar estado de la carpeta en Risk
                        Dim FolderDataTable = dbmArchiving.SchemaImaging.CTA_Folder_No_Mesa.DBFindByfk_expedienteid_Folder(nExpediente, nFolder)

                        For Each FolderRow In FolderDataTable
                            ' Folder Risk
                            Dim FolderRiskType = New DBArchiving.SchemaRisk.TBL_FolderType()
                            FolderRiskType.fk_Estado = DBCore.EstadoEnum.Empaque
                            dbmArchiving.SchemaRisk.TBL_Folder.DBUpdate(FolderRiskType, FolderRow.fk_expediente, FolderRow.id_Folder, FolderRow.fk_OT)

                            ' Folder Core
                            Dim FolderEstadoCoreType = New DBCore.SchemaProcess.TBL_Folder_estadoType()
                            FolderEstadoCoreType.fk_Estado = DBCore.EstadoEnum.Empaque
                            dbmCore.SchemaProcess.TBL_Folder_estado.DBUpdate(FolderEstadoCoreType, FolderRow.fk_expediente, FolderRow.id_Folder, DesktopConfig.Modulo.Archiving)
                            dbmCore.SchemaProcess.TBL_Folder_estado.DBUpdate(FolderEstadoCoreType, FolderRow.fk_expediente, FolderRow.id_Folder, DesktopConfig.Modulo.Imaging)
                        Next
                    End If

                    Utilities.ClearMemory()
                    Application.DoEvents()
                    FolioIndex = CShort(FolioIndex + 1)
                    Application.DoEvents()
                Next


                dbmCore.Transaction_Commit()
                dbmImaging.Transaction_Commit()
                dbmArchiving.Transaction_Commit()
                manager.TransactionCommit()

                DesktopMessageBoxControl.DesktopMessageShow("Se realizó la adición de los items correctamente.", "Adicionar Items", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)

            Catch ex As Exception
                If (dbmCore IsNot Nothing) Then dbmCore.Transaction_Rollback()
                If (dbmImaging IsNot Nothing) Then dbmImaging.Transaction_Rollback()
                If (dbmArchiving IsNot Nothing) Then dbmArchiving.Transaction_Rollback()
                If (manager IsNot Nothing) Then manager.TransactionRollback()

                ProgressForm.Hide()
                Application.DoEvents()

                DesktopMessageBoxControl.DesktopMessageShow("Adicionar Imagen Solicitud", ex)
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                If (dbmArchiving IsNot Nothing) Then dbmArchiving.Connection_Close()
                If (manager IsNot Nothing) Then manager.Disconnect()
            End Try
        End Sub

        Private Sub MostrarImagen()
            Dim ProgressForm As New FormProgress
            Dim Progreso As Integer = 0

            'Se seleccionan las nuevas imágenes
            With _ImagenOpenFileDialog
                .Multiselect = True
                .Title = "Seleccionar nuevas imágenes "
                .Filter = "Imágenes (*" & Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Entrada & ")|*" & Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Entrada & ""
            End With

            Dim Respuesta = _ImagenOpenFileDialog.ShowDialog()
            If Respuesta = DialogResult.OK Then
                ProgressForm.Show()
                ProgressForm.SetProceso("Cargar")
                ProgressForm.SetAccion("Cargar Imágqmes")
                ProgressForm.SetProgreso(0)
                ProgressForm.SetMaxValue(_ImagenOpenFileDialog.FileNames.Length)
                CentralOffLineViewer.ImagePath = New String() {_ImagenOpenFileDialog.FileName}
                Progreso += 1
                ProgressForm.SetProgreso(Progreso)
            End If
            If (Not CentralOffLineViewer.ImagePath Is Nothing AndAlso CentralOffLineViewer.ImagePath.Count > 0) Then
                AdicionarImagenButton.Enabled = True
            End If

            ProgressForm.Close()
        End Sub

#End Region

#Region " Funciones "

        Private Function GetSize(ByVal Folios As Integer, ByVal Formato As Slyg.Tools.Imaging.ImageManager.EnumFormat, ByVal Compresion As Slyg.Tools.Imaging.ImageManager.EnumCompression) As Long
            Dim Peso As Long = 0
            For Folio = CShort(1) To Folios
                Dim dataImage = ImageManager.GetFolioData(_ImagenOpenFileDialog.FileName, Folio, Formato, Compresion)
                Peso = Peso + dataImage.Length
            Next
            Return Peso
        End Function

        Public Function ThumbnailCallback() As Boolean
            Return False
        End Function

        Private Function ValidarDocumento() As Boolean
            If CInt(cmbConfirmarTipologia.SelectedValue) <> CInt(cmbTipologia.SelectedValue) Then
                DesktopMessageBoxControl.DesktopMessageShow("La confirmación del Documento no coincide!!", "Validacion Documento", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                Return False
            Else
                Return True
            End If
        End Function

#End Region

    End Class

End Namespace