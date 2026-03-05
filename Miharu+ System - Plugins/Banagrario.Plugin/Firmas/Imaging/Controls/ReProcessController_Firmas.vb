Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.FileProvider.Library
Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Library
Imports System.Threading
Imports System.Windows.Forms
Imports Slyg.Tools


Namespace Firmas.Controller.Indexer.Capturas.Reproceso

    Public Class ReProcessController_Firmas
        Inherits Miharu.Imaging.Indexer.Controller.Indexer.Capturas.CapturasController 'CapturasController


#Region " Declaraciones "

        Dim _idExpediente As Long
        Dim _idFolder As Short
        Dim _idFile As Short

        Private _sede As Slyg.Tools.SlygNullable(Of Short)
        Private _centroProcesamiento As Slyg.Tools.SlygNullable(Of Short)
        Private _motivoReproceso As Short

        Public _Plugin As FirmasImagingPlugin

        Public _AgrarioConnectionString As String
        Public _ToolsConnectionString As String

        Public _CamposReproceso As DBAgrario.SchemaFirmas.TBL_Campo_ReprocesoDataTable

        Private oficinasDataView As DataView
        Private codTransaccionDataView As DataView
        'Private idDocumento_actual As String

#End Region

#Region " Propiedades "

        Protected Overrides ReadOnly Property ProcessName As String
            Get
                Return "Reproceso"
            End Get
        End Property

#End Region

#Region " Implementación IIndexerController "

        Public Overrides Sub Inicializar(ByVal nTempPath As String, nIndexerSesion As Miharu.Security.Library.Session.Sesion, nIndexerDesktopGlobal As DesktopGlobal, nIndexerImagingGlobal As ImagingGlobal)
            MyBase.Inicializar(nTempPath, nIndexerSesion, nIndexerDesktopGlobal, nIndexerImagingGlobal)

            IndexerView.ShowDesindexarFolioButton(False)
            IndexerView.ShowSaveButton(True)
            IndexerView.ShowAddFolioButton(False)
            IndexerView.ShowDeleteFolioButton(False)
            IndexerView.ShowNewFolderButton(False)
            IndexerView.ShowNewFileButton(False)
            IndexerView.ShowNextButton(True)
            IndexerView.ShowReprocesoButton(False)

            IndexerView.ShowInformationPanel(Me.IndexerImagingGlobal.ProyectoImagingRow.Show_Information)
            IndexerView.ShowDataPanel(True)
            IndexerView.ShowValidationsPanel(False)
            IndexerView.ShowValidationsListasPanel(False)
            IndexerView.ShowToolTipData(False)
            IndexerView.ShowAutoIndexar(False)

            IndexerView.Esquema_Enabled = True
            IndexerView.TipoDocumental_Enabled = True

        End Sub

        Public Overrides ReadOnly Property ShowSecondControls As Boolean
            Get
                Return True
            End Get
        End Property

        Public Overrides Function AddFolio() As Boolean
            Throw New NotImplementedException()
        End Function

        Public Overrides Function DeleteFolio() As Boolean
            'CurrentDocumentFile.Remove(CurrentFolio)

            'If (_CurrentFolioIndex >= CurrentDocumentFile.Count) Then
            '    _CurrentFolioIndex -= 1
            'End If

            'SetCurrentFolio(CurrentFolio)

            '_InderxerView.ShowDeleteFolioButton(Folios > 1 And _Captura = EnumCaptura.Primera)
            Throw New NotImplementedException()
        End Function

        Public Function _Validar() As Boolean
            If CInt(Me.IndexerView.TipoDocumental_Value) <> FileCoreRow.fk_Documento Then

                Return Validar()

            End If
            Return True

        End Function

        Public Overrides Function Save() As Boolean


            If (_Validar()) Then

                Try

                    Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
                    Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
                    Dim dbmAgrario As DBAgrario.DBAgrarioDataBaseManager = Nothing
                    Dim tipodocumentonuevoDatarow As DBAgrario.SchemaFirmas.TBL_DocumentoRow


                    Dim fechaInicioProceso = Now

                    Try
                        dbmCore = New DBCore.DBCoreDataBaseManager(Me.IndexerDesktopGlobal.ConnectionStrings.Core)
                        dbmImaging = New DBImaging.DBImagingDataBaseManager(Me.IndexerDesktopGlobal.ConnectionStrings.Imaging)
                        dbmAgrario = New DBAgrario.DBAgrarioDataBaseManager(_AgrarioConnectionString)

                        dbmAgrario.Connection_Open(Me.IndexerSesion.Usuario.id)
                        dbmAgrario.Transaction_Begin(IsolationLevel.ReadUncommitted)

                        Dim CBarras As String = Nothing
                        Dim cuenta As String = Nothing
                        Dim sticker As String = Nothing

                        tipodocumentonuevoDatarow = dbmAgrario.SchemaFirmas.TBL_Documento.DBFindByfk_Documento_Core(CInt(Me.IndexerView.TipoDocumental_Value)).Rows(0)

                        'validaciones campos
                        If _Campos.Count > 0 Then

                            For Each campo In _Campos
                                Select Case campo.Control.Etiqueta.ToUpper
                                    Case "CUENTA"
                                        cuenta = campo.Control.Value.ToString

                                    Case "STICKER"
                                        sticker = campo.Control.Value.ToString

                                    Case "CB"
                                        CBarras = campo.Control.Value.ToString
                                    Case Else

                                        If campo.Control.Etiqueta.ToUpper.Contains("BARRAS") Then
                                            CBarras = campo.Control.Value.ToString

                                        End If
                                End Select
                            Next

                            'Dim campos_diligenciados As String = ""

                            'If Not CBarras Is Nothing Then
                            '    campos_diligenciados = "CB/"
                            'Else
                            '    campos_diligenciados = "/"
                            'End If
                            'If Not cuenta Is Nothing Then
                            '    campos_diligenciados = campos_diligenciados & "CUENTA/"
                            'Else
                            '    campos_diligenciados = "/"
                            'End If

                            'If Not sticker Is Nothing Then
                            '    campos_diligenciados = campos_diligenciados & "STICKER"
                            'Else
                            '    campos_diligenciados = ""
                            'End If

                            'Dim splitcampos() As String

                            'splitcampos = campos_diligenciados.Split("/")
                            

                            If Not CBarras Is Nothing Or Not cuenta Is Nothing Or Not sticker Is Nothing Then

                                Dim msg As String = ""
                                Dim longitud As Integer


                                Dim reprocesosrow As DBAgrario.SchemaFirmas.TBL_Data_Temporal_ReprocesosRow

                                Dim updateTemporalReproceso As New DBAgrario.SchemaFirmas.TBL_Data_Temporal_ReprocesosType

                                updateTemporalReproceso.fk_Documento = CInt(Me.IndexerView.TipoDocumental_Value)

                                reprocesosrow = dbmAgrario.SchemaFirmas.TBL_Data_Temporal_Reprocesos.DBFindByfk_Expedientefk_Folderfk_File(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File).Rows(0)

                                If reprocesosrow.id = 0 Then

                                    MsgBox("No se puede guardar debido a que no existe registro en tabla temporal de reproceso")
                                    Return False
                                End If



                                If Not sticker Is Nothing Then

                                    Select Case tipodocumentonuevoDatarow.id_Documento

                                        Case 4 'Ilegible, sin Codigo de Barras, Diligenciada Manualmente
                                            If Not sticker.Length = 12 Then
                                                MsgBox("Sticker Inválido")
                                                Return False
                                            End If
                                        Case Else
                                            MsgBox("Documento no permitido para ingresar sticker")
                                            Return False
                                    End Select

                                    updateTemporalReproceso.CBarras = sticker
                                    dbmAgrario.SchemaFirmas.TBL_Data_Temporal_Reprocesos.DBUpdate(updateTemporalReproceso, CInt(reprocesosrow(0)))
                                End If


                                If Not cuenta Is Nothing Then

                                    Select Case tipodocumentonuevoDatarow.id_Documento
                                        Case 7, 8, 9
                                            If cuenta.Length <> 12 Then
                                                MsgBox("Numero de Cuenta Inválido")
                                                Return False
                                            End If


                                    End Select

                                    updateTemporalReproceso.CBarras = cuenta
                                    updateTemporalReproceso.Numero_Cuenta = cuenta
                                    dbmAgrario.SchemaFirmas.TBL_Data_Temporal_Reprocesos.DBUpdate(updateTemporalReproceso, reprocesosrow.id)
                                End If

                                If Not CBarras Is Nothing Then
                                    If CBarras <> "" Then


                                        Dim oficinaDataTable = dbmAgrario.SchemaConfig.TBL_Oficina.DBGet(Nothing)
                                        oficinasDataView = New DataView(oficinaDataTable)

                                        Dim codTransaccionDataTable = dbmAgrario.SchemaFirmas.TBL_Transaccion.DBGet(Nothing)
                                        codTransaccionDataView = New DataView(codTransaccionDataTable)

                                        Dim valido As Boolean = False

                                        ' Validar dígito de verificación
                                        Dim digito1 As Integer = -1
                                        Dim newCodigo As String = ""

                                        Dim oficina = String.Empty
                                        Dim fecha = String.Empty
                                        Dim codTransaccion = String.Empty

                                        longitud = CBarras.Length


                                        Select Case longitud
                                            Case 44 ' Judiciales 
                                                If tipodocumentonuevoDatarow.id_Documento <> 2 Then
                                                    MsgBox("Codigo de Barras no corresponde a tarjeta de Firmas Seleccionada")
                                                    Return False
                                                End If
                                                ExtraerDigitoYCodigo(CBarras, 14, newCodigo, digito1)
                                                oficina = CBarras.Substring(13, 4)
                                                fecha = CBarras.Substring(32, 8)
                                                codTransaccion = CBarras.Substring(17, 5)


                                            Case 38 ' Clientes
                                                If tipodocumentonuevoDatarow.id_Documento <> 1 Then
                                                    MsgBox("Codigo de Barras no corresponde a tarjeta de Firmas Seleccionada")
                                                    Return False
                                                End If
                                                ExtraerDigitoYCodigo(CBarras, 13, newCodigo, digito1)
                                                oficina = "9000"
                                                fecha = CBarras.Substring(28, 8)
                                                codTransaccion = CBarras.Substring(14, 3)

                                            Case 28 ' Funcionarios

                                                If tipodocumentonuevoDatarow.id_Documento <> 3 Then
                                                    MsgBox("Codigo de Barras no corresponde a tarjeta de Firmas Seleccionada")
                                                    Return False
                                                End If
                                                ExtraerDigitoYCodigo(CBarras, 10, newCodigo, digito1)
                                                oficina = CBarras.Substring(11, 4)
                                                fecha = CBarras.Substring(20, 8)
                                                codTransaccion = CBarras.Substring(15, 5)

                                            Case 12 ' Tapa
                                                digito1 = -2

                                        End Select

                                        If (digito1 >= 0) Then
                                            Dim digito2 = FMDigitoVerif(newCodigo, longitud)

                                            If (digito1 = digito2) Then

                                                If (ValidarOficina(oficina, msg)) Then
                                                    If (ValidarFecha(fecha, msg)) Then
                                                        If (ValidarCodTransaccion(codTransaccion, msg)) Then
                                                            valido = True
                                                        End If
                                                    End If
                                                End If
                                            Else
                                                MsgBox("Dígito de verificación inválido")
                                                Return False
                                            End If
                                        End If


                                        updateTemporalReproceso.CBarras = CBarras
                                        dbmAgrario.SchemaFirmas.TBL_Data_Temporal_Reprocesos.DBUpdate(updateTemporalReproceso, CInt(reprocesosrow(0)))
                                    End If
                                End If

                            End If



                        End If
                        dbmAgrario.Transaction_Commit()
                        ' Insertar datos
                        dbmCore.Connection_Open(Me.IndexerSesion.Usuario.id)
                        dbmImaging.Connection_Open(Me.IndexerSesion.Usuario.id)

                        '--------------------------------------------------------------------------------------
                        ' REPORTAR DUPLICADOS
                        '--------------------------------------------------------------------------------------
                        Try
                            Dim fileBloqueadoDataTable = dbmImaging.SchemaProcess.TBL_Dashboard_Capturas.DBGet(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File)

                            If (fileBloqueadoDataTable(0).fk_Usuario_log <> Me.IndexerSesion.Usuario.id) Then
                                dbmImaging.SchemaAudit.PA_Inserta_Seguimiento_Imagenes.DBExecute(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, DBCore.EstadoEnum.Reproceso, Me.IndexerSesion.Usuario.id, Me.IndexerDesktopGlobal.PcName)
                            End If

                        Catch : End Try
                        '--------------------------------------------------------------------------------------

                        dbmCore.Transaction_Begin(IsolationLevel.ReadUncommitted)
                        dbmImaging.Transaction_Begin(IsolationLevel.ReadUncommitted)

                        'Actualizar tipo documental
                        Dim updateFileCore As New DBCore.SchemaProcess.TBL_FileType()
                        updateFileCore.fk_Documento = CInt(Me.IndexerView.TipoDocumental_Value)
                        dbmCore.SchemaProcess.TBL_File.DBUpdate(updateFileCore, FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File)

                        'Actualizar el esquema del Expediente
                        'TODO: Falta la implementación para cuando hay mas de un File
                        If (Me.IndexerView.Esquema_Value <> ExpedienteRow.fk_Esquema) Then
                            Dim updateExpediente As New DBCore.SchemaProcess.TBL_ExpedienteType()
                            updateExpediente.fk_Esquema = Me.IndexerView.Esquema_Value
                            dbmCore.SchemaProcess.TBL_Expediente.DBUpdate(updateExpediente, FileCoreRow.fk_Expediente)

                            Dim anexosFileDataTable = dbmCore.SchemaImaging.TBL_File.DBFindByfk_ExpedienteEs_Anexo(FileCoreRow.fk_Expediente, True)

                            If (anexosFileDataTable.Count > 0) Then
                                Dim anexoDataTable = DocumentoIndexacionDataTable.Select("fk_Esquema = " & Me.IndexerView.Esquema_Value & " AND Es_Anexo = 1")

                                If (anexoDataTable Is Nothing OrElse anexoDataTable.Length = 0) Then
                                    MessageBox.Show("El esquema: " & Me.IndexerView.Esquema_Value & " no tiene definido un documento como anexo", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)

                                    Return False
                                End If

                                updateFileCore = New DBCore.SchemaProcess.TBL_FileType()
                                updateFileCore.fk_Documento = CType(anexoDataTable(0), DBImaging.SchemaConfig.CTA_Documento_IndexacionRow).id_Documento
                                For Each AnexoRow In anexosFileDataTable
                                    dbmCore.SchemaProcess.TBL_File.DBUpdate(updateFileCore, AnexoRow.fk_Expediente, AnexoRow.fk_Folder, AnexoRow.fk_File)
                                Next
                            End If
                        End If


                        'Actualiza el File de Imaging, para remover el reproceso
                        Dim fileImaging As New DBImaging.SchemaProcess.TBL_FileType
                        fileImaging.fk_Documento = updateFileCore.fk_Documento
                        fileImaging.Fecha_Reproceso = SlygNullable.SysDate
                        dbmImaging.SchemaProcess.TBL_File.DBUpdate(fileImaging, FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, Nothing)

                        'Eliminar los datos de la mesa de control
                        dbmImaging.SchemaProcess.TBL_File_Data_MC_Asociada.DBDelete(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, Nothing, Nothing, Nothing, Nothing, Nothing)
                        dbmImaging.SchemaProcess.TBL_File_Data_MC.DBDelete(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, Nothing, Nothing, Nothing)

                        'Elimar los datos de la captura
                        dbmCore.SchemaProcess.TBL_File_Data_Asociada.DBDelete(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, Nothing, Nothing, Nothing, Nothing)
                        dbmCore.SchemaProcess.TBL_File_Data.DBDelete(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, Nothing, Nothing)

                        ' Actualizar estados
                        Dim nextEstado As DBCore.EstadoEnum = CType(dbmImaging.SchemaProcess.PA_Next_Estado.DBExecute(updateFileCore.fk_Documento, DBCore.EstadoEnum.Indexacion), DBCore.EstadoEnum)

                        Select Case tipodocumentonuevoDatarow.id_Documento
                            Case 4, 5 'Tapa, Documento No identificado
                                nextEstado = DBCore.EstadoEnum.Captura
                        End Select

                        ' Preparar los recortes
                        If (nextEstado = DBCore.EstadoEnum.Recorte) Then
                            If Not (Me.IndexerImagingGlobal.ProyectoImagingRow.Usa_Recortes AndAlso dbmImaging.SchemaProcess.PA_Preparar_Recortes.DBExecute(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, FileImagingRow.id_Version)) Then
                                nextEstado = DBCore.EstadoEnum.Indexado
                            End If
                        End If


                        Dim updateEstado As New DBCore.SchemaProcess.TBL_File_EstadoType()
                        updateEstado.Fecha_Log = SlygNullable.SysDate
                        updateEstado.fk_Usuario = Me.IndexerSesion.Usuario.id
                        updateEstado.fk_Estado = nextEstado
                        dbmCore.SchemaProcess.TBL_File_Estado.DBUpdate(updateEstado, FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, DesktopConfig.Modulo.Imaging)

                        '---------------------------------------------------------------------------
                        ' Actualizar Dashboard
                        '---------------------------------------------------------------------------
                        If (nextEstado = DBCore.EstadoEnum.Indexado) Then
                            dbmImaging.SchemaProcess.TBL_Dashboard_Capturas.DBDelete(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File)
                        Else
                            Dim capDashboardType = New DBImaging.SchemaProcess.TBL_Dashboard_CapturasType()
                            capDashboardType.fk_Documento = updateFileCore.fk_Documento
                            capDashboardType.fk_Usuario_log = DBNull.Value
                            capDashboardType.Sesion = DBNull.Value
                            capDashboardType.fk_Estado = nextEstado
                            capDashboardType.fk_Reproceso_Motivo = DBNull.Value
                            dbmImaging.SchemaProcess.TBL_Dashboard_Capturas.DBUpdate(capDashboardType, FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File)
                        End If

                        ' Actualizar validaciones opcionales
                        dbmImaging.SchemaProcess.TBL_Dashboard_Validaciones.DBDelete(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File)
                        If (dbmImaging.SchemaConfig.CTA_Validacion.DBFindByfk_Documentofk_Etapa_Captura(updateFileCore.fk_Documento, DBImaging.EnumEtapaCaptura.Opcional, 1, New DBImaging.SchemaConfig.CTA_ValidacionEnumList(DBImaging.SchemaConfig.CTA_ValidacionEnum.id_Validacion, True)).Count > 0) Then
                            'Obtener el Cargue/Paquete
                            Dim cargueImgDataTable = dbmCore.SchemaImaging.TBL_Folder.DBGet(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder)

                            'Obtener el detalle de Asignación del Cargue/Paquete
                            Dim cargueDatatable = dbmImaging.SchemaProcess.TBL_Cargue.DBGet(cargueImgDataTable(0).fk_Cargue)
                            Dim carguePaqueteDatatable = dbmImaging.SchemaProcess.TBL_Cargue_Paquete.DBGet(cargueImgDataTable(0).fk_Cargue, cargueImgDataTable(0).fk_Cargue_Paquete)

                            Dim capDashboardType = New DBImaging.SchemaProcess.TBL_Dashboard_ValidacionesType()
                            capDashboardType.fk_Expediente = FileCoreRow.fk_Expediente
                            capDashboardType.fk_Folder = FileCoreRow.fk_Folder
                            capDashboardType.fk_File = FileCoreRow.id_File
                            capDashboardType.fk_Documento = updateFileCore.fk_Documento
                            capDashboardType.fk_Cargue = cargueImgDataTable(0).fk_Cargue
                            capDashboardType.fk_Cargue_Paquete = carguePaqueteDatatable(0).id_Cargue_Paquete
                            capDashboardType.fk_Entidad_Procesamiento = cargueDatatable(0).fk_Entidad_Procesamiento
                            capDashboardType.fk_Sede_Procesamiento = carguePaqueteDatatable(0).fk_Sede_Procesamiento_Asignada
                            capDashboardType.fk_Centro_Procesamiento = carguePaqueteDatatable(0).fk_Centro_Procesamiento_Asignado
                            capDashboardType.fk_Entidad = cargueDatatable(0).fk_Entidad
                            capDashboardType.fk_Proyecto = cargueDatatable(0).fk_Proyecto
                            capDashboardType.Procesado = False

                            dbmImaging.SchemaProcess.TBL_Dashboard_Validaciones.DBInsert(capDashboardType)
                        End If
                        '---------------------------------------------------------------------------

                        dbmCore.Transaction_Commit()
                        dbmImaging.Transaction_Commit()

                        ' Actualizar el estado de los cargues que ya fueron procesados
                        Try
                            dbmImaging.Transaction_Begin(IsolationLevel.ReadUncommitted)

                            Dim carguePaqueteFileDataTable = dbmImaging.SchemaProcess.CTA_Cargue_Paquete_Estado_File.DBFindByid_Cargueid_Cargue_Paquete(_idCargue, _idCarguePaquete)
                            If (carguePaqueteFileDataTable.Count > 0) Then
                                Dim updatePaquete = New DBImaging.SchemaProcess.TBL_Cargue_PaqueteType()
                                updatePaquete.fk_Estado = carguePaqueteFileDataTable(0).Estado_File
                                dbmImaging.SchemaProcess.TBL_Cargue_Paquete.DBUpdate(updatePaquete, _idCargue, _idCarguePaquete)

                                Dim cargueFileDataTable = dbmImaging.SchemaProcess.CTA_Cargue_Estado.DBFindByid_Cargue(_idCargue)
                                If (cargueFileDataTable.Count > 0) Then
                                    Dim updateCargue = New DBImaging.SchemaProcess.TBL_CargueType()
                                    updateCargue.fk_Estado = cargueFileDataTable(0).Estado_File
                                    dbmImaging.SchemaProcess.TBL_Cargue.DBUpdate(updateCargue, _idCargue)
                                End If
                            End If

                            dbmImaging.Transaction_Commit()
                        Catch ex As Exception
                            If (dbmImaging IsNot Nothing) Then dbmImaging.Transaction_Rollback()
                            Throw
                        End Try


                        'EventManager.FinalizarReclasificar(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, FileImagingRow.id_Version)
                        Try

                            dbmAgrario.SchemaFirmas.PA_Cargar_Data_Reproceso.DBExecute(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File)


                        Catch ex As Exception
                            Throw New Exception(ex.Message)
                        End Try


                    Catch ex As Exception
                        If (dbmCore IsNot Nothing) Then dbmCore.Transaction_Rollback()
                        If (dbmImaging IsNot Nothing) Then dbmImaging.Transaction_Rollback()
                        If (dbmAgrario IsNot Nothing) Then dbmAgrario.Transaction_Rollback()

                        DesktopMessageBoxControl.DesktopMessageShow("Publicar", ex)

                        Return False
                    Finally
                        If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                        If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                        If (dbmAgrario IsNot Nothing) Then dbmAgrario.Connection_Close()
                    End Try


                    'Dim evmgr As New Miharu.Imaging.Library.Eventos.EventManager(Plugin.Firmas.Controller.Indexer.)



                    '-------------------------------------------------------------------------------------------------------------
                    ' LOGGING - PERFORMANCE
                    '-------------------------------------------------------------------------------------------------------------
                    Dim fechaFinProceso = Now
                    Dim traceMessage As String = ""
                    traceMessage &= "Duración:" & vbTab & (fechaInicioProceso - fechaFinProceso).TotalMilliseconds & vbTab
                    traceMessage &= "Expediente:" & vbTab & FileCoreRow.fk_Expediente & vbTab
                    traceMessage &= "Folder:" & vbTab & FileCoreRow.fk_Folder & vbTab
                    traceMessage &= "File:" & vbTab & FileCoreRow.id_File & vbTab
                    DesktopTrace.Trace(traceMessage, DesktopTrace.CategoryEnum.Performance, 1, 0, TraceEventType.Information, "[Reprocesos][Publicar]")
                    '-------------------------------------------------------------------------------------------------------------

                Catch ex As Exception
                    DesktopMessageBoxControl.DesktopMessageShow("Publicar", ex)

                    Return False
                End Try

                Return True
            End If

            Return False
        End Function

        Public Overrides Function ValidacionListas() As Boolean
            Dim _ValidacionListas As Boolean

            _ValidacionListas = False

            Return _ValidacionListas
        End Function

        Public Overrides Function Campos(ByVal idDocumento As Integer) As List(Of Miharu.Imaging.Indexer.View.Indexacion.CampoCaptura)
            Me._Campos.Clear()

            Dim dbmAgrario As DBAgrario.DBAgrarioDataBaseManager = Nothing

            dbmAgrario = New DBAgrario.DBAgrarioDataBaseManager(_AgrarioConnectionString)


            ' Insertar datos
            dbmAgrario.Connection_Open(Me.IndexerSesion.Usuario.id)

            Dim CamposReprocesoDT As New DBAgrario.SchemaFirmas.TBL_Campo_ReprocesoDataTable

            'CamposReprocesoDT = dbmAgrario.SchemaFirmas.TBL_Campo_Reproceso.DBFindByfk_Documento_Core(idDocumento)
            CamposReprocesoDT = dbmAgrario.SchemaFirmas.PA_Consulta_Campo_Reproceso.DBExecute(FileCoreRow.fk_Documento, idDocumento)

            For Each CampoRow As DBAgrario.SchemaFirmas.TBL_Campo_ReprocesoRow In CamposReprocesoDT

                Dim ControlCaptura As Miharu.Imaging.Indexer.View.IInputControl
                Dim CampoCaptura As New Miharu.Imaging.Indexer.View.Indexacion.CampoCaptura()
                Dim ListDefinicionCaptura As New List(Of Miharu.Imaging.Indexer.View.Indexacion.DefinicionCaptura)

                ' Control
                Select Case CType(CampoRow.fk_Tipo_Campo, DesktopConfig.CampoTipo)
                    Case DesktopConfig.CampoTipo.Texto
                        Dim DefinicionCaptura = New Miharu.Imaging.Indexer.View.Indexacion.DefinicionCaptura()
                        ControlCaptura = New Miharu.Imaging.Indexer.View.Indexacion.TextInputControl()

                        DefinicionCaptura.Type = DesktopConfig.CampoTipo.Texto
                        'DefinicionCaptura.Mascara = CampoRow.Mascara
                        'DefinicionCaptura.FormatoFecha = CampoRow.Formato
                        DefinicionCaptura.Es_Obligatorio_Campo = CampoRow.Es_Obligatorio
                        DefinicionCaptura.MaximumLength = CampoRow.Length_Campo
                        DefinicionCaptura.MinimumLength = CampoRow.Length_Min_Campo

                        ListDefinicionCaptura.Add(DefinicionCaptura)
                        ControlCaptura.LoadDefinition(ListDefinicionCaptura)

                        'If (CampoRow.Valor_por_Defecto <> "") Then
                        '    ControlCaptura.Value = CampoRow.Valor_por_Defecto
                        'End If

                    Case DesktopConfig.CampoTipo.Numerico
                        Dim DefinicionCaptura = New Miharu.Imaging.Indexer.View.Indexacion.DefinicionCaptura()
                        ControlCaptura = New Miharu.Imaging.Indexer.View.Indexacion.TextInputNumericControl()

                        DefinicionCaptura.Type = DesktopConfig.CampoTipo.Numerico
                        DefinicionCaptura.Es_Obligatorio_Campo = CampoRow.Es_Obligatorio
                        DefinicionCaptura.MaximumLength = CampoRow.Length_Campo
                        DefinicionCaptura.MinimumLength = CampoRow.Length_Min_Campo
                        'DefinicionCaptura.Usa_Decimales = CampoRow.Usa_Decimales

                        'If CampoRow.Usa_Decimales Then
                        '    DefinicionCaptura.Caracter_Decimal = CChar(CampoRow.Caracter_Decimal)
                        '    DefinicionCaptura.Cantidad_Decimales = CampoRow.Cantidad_Decimales
                        'End If

                        ListDefinicionCaptura.Add(DefinicionCaptura)
                        ControlCaptura.LoadDefinition(ListDefinicionCaptura)

                        'If (CampoRow.Valor_por_Defecto <> "") Then
                        '    ControlCaptura.Value = CampoRow.Valor_por_Defecto
                        'End If

                    Case DesktopConfig.CampoTipo.Fecha
                        Dim DefinicionCaptura = New Miharu.Imaging.Indexer.View.Indexacion.DefinicionCaptura()
                        ControlCaptura = New Miharu.Imaging.Indexer.View.Indexacion.TextInputDateTimeControl()

                        DefinicionCaptura.Type = DesktopConfig.CampoTipo.Fecha
                        'DefinicionCaptura.Mascara = CampoRow.Mascara
                        'DefinicionCaptura.FormatoFecha = CampoRow.Formato
                        DefinicionCaptura.Es_Obligatorio_Campo = CampoRow.Es_Obligatorio

                        ListDefinicionCaptura.Add(DefinicionCaptura)
                        ControlCaptura.LoadDefinition(ListDefinicionCaptura)

                        'If (CampoRow.Valor_por_Defecto <> "") Then
                        '    ControlCaptura.Value = CampoRow.Valor_por_Defecto
                        'End If

                    Case DesktopConfig.CampoTipo.Lista
                        Dim DefinicionCaptura = New Miharu.Imaging.Indexer.View.Indexacion.DefinicionCaptura()
                        ControlCaptura = New Miharu.Imaging.Indexer.View.Indexacion.ListInputControl()

                        DefinicionCaptura.Type = DesktopConfig.CampoTipo.Lista
                        DefinicionCaptura.Es_Obligatorio_Campo = CampoRow.Es_Obligatorio

                        ' Datos de la lista
                        If (Not CampoRow.Isfk_Campo_ListaNull()) Then
                            Dim dtvLista As New DataView(ListaItemsDataTable)

                            dtvLista.RowFilter = "fk_Campo_Lista = " & CampoRow.fk_Campo_Lista

                            Dim ListControl = CType(ControlCaptura, Miharu.Imaging.Indexer.View.Indexacion.ListInputControl)
                            ListControl.ValueDesktopComboBox.ValueMember = "Valor_Campo_Lista_Item"
                            ListControl.ValueDesktopComboBox.DisplayMember = "Etiqueta_Campo_Lista_Item"
                            ListControl.ValueDesktopComboBox.DataSource = dtvLista
                            ListControl.ValueDesktopComboBox.Refresh()
                        End If

                        ListDefinicionCaptura.Add(DefinicionCaptura)
                        ControlCaptura.LoadDefinition(ListDefinicionCaptura)

                    Case DesktopConfig.CampoTipo.SiNo
                        Dim DefinicionCaptura = New Miharu.Imaging.Indexer.View.Indexacion.DefinicionCaptura()
                        ControlCaptura = New Miharu.Imaging.Indexer.View.Indexacion.ListInputControl()

                        DefinicionCaptura.Type = DesktopConfig.CampoTipo.SiNo
                        DefinicionCaptura.Es_Obligatorio_Campo = CampoRow.Es_Obligatorio

                        Dim ListControl = CType(ControlCaptura, Miharu.Imaging.Indexer.View.Indexacion.ListInputControl)
                        ListControl.ValueDesktopComboBox.DropDownStyle = ComboBoxStyle.DropDownList
                        ListControl.ValueDesktopComboBox.Items.Add("Si")
                        ListControl.ValueDesktopComboBox.Items.Add("No")
                        ListControl.ValueDesktopComboBox.SelectedIndex = 0

                        ListDefinicionCaptura.Add(DefinicionCaptura)
                        ControlCaptura.LoadDefinition(ListDefinicionCaptura)

                End Select

                ControlCaptura.ShowSecondControls = False
                ControlCaptura.Tipo = CType(CampoRow.fk_Tipo_Campo, DesktopConfig.CampoTipo)
                ControlCaptura.Etiqueta = CampoRow.Descripcion_Campo
                ControlCaptura.CampoCaptura = CampoCaptura
                ControlCaptura.ShowSecondControls = False
                ControlCaptura.ShowPrimaryControls = True
                ControlCaptura.ShowValidacionListasControls = False

                CampoCaptura.id = CampoRow.id_Campo_Reproceso
                CampoCaptura.Control = ControlCaptura
                'CampoCaptura.Marca_Height_Campo = CampoRow.Marca_Height
                'CampoCaptura.Marca_Width_Campo = CampoRow.Marca_Width_Campo
                'CampoCaptura.Marca_X_Campo = CampoRow.Marca_X_Campo
                'CampoCaptura.Marca_Y_Campo = CampoRow.Marca_Y_Campo
                'CampoCaptura.Usa_Marca = CampoRow.Usa_Marca

                _Campos.Add(CampoCaptura)

            Next


            Return _Campos
        End Function

        Public Overrides Function GetValueFileData(idCampo As Integer) As String
            Return Nothing
        End Function

        Public Overrides Function GetValueLlaveData() As DBCore.SchemaProcess.TBL_Expediente_Llave_LineaRow
            Return Nothing
        End Function

        Public Overrides Function Validaciones(ByVal idDocumento As Integer) As List(Of Miharu.Imaging.Indexer.View.Indexacion.ValidacionCaptura)
            Me._Validaciones.Clear()
            Return _Validaciones
        End Function

        Public Overrides Function NextIndexingElement(ByVal ot As Integer, nEstado As DBCore.EstadoEnum, nIdDocumento As SlygNullable(Of Integer)) As Boolean
            Dim manager As FileProviderManager = Nothing
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing

            Dim fechaInicioProceso = Now

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Me.IndexerDesktopGlobal.ConnectionStrings.Imaging)
                dbmCore = New DBCore.DBCoreDataBaseManager(Me.IndexerDesktopGlobal.ConnectionStrings.Core)

                dbmImaging.Connection_Open(Me.IndexerSesion.Usuario.id)
                dbmCore.Connection_Open(Me.IndexerSesion.Usuario.id)

                ' Mirar si existe un registro previamente asignado
                Dim bloqueoDatatable = dbmImaging.SchemaProcess.TBL_Dashboard_Capturas.DBFindBySesionfk_OT(Me.IndexerDesktopGlobal.SesionId, ot)
                If bloqueoDatatable.Count > 0 Then
                    For Each BloqueoRow In bloqueoDatatable
                        dbmImaging.SchemaProcess.PA_Dashboard_Desbloquear_Capturas.DBExecute(BloqueoRow.fk_Expediente, BloqueoRow.fk_Folder, BloqueoRow.fk_File)
                    Next
                End If

                Dim fileCoreDataTable = dbmCore.SchemaProcess.TBL_File.DBGet(Me._idExpediente, Me._idFolder, Me._idFile)
                If (fileCoreDataTable.Count = 0) Then
                    Throw New Exception("No se encontró data asociada al Ex: " & bloqueoDatatable(0).fk_Expediente & " Fo: " & bloqueoDatatable(0).fk_Folder & " Fi: " & bloqueoDatatable(0).fk_File & " del Dashboard")
                End If
                FileCoreRow = fileCoreDataTable(0)

                bloqueoDatatable = dbmImaging.SchemaProcess.TBL_Dashboard_Capturas.DBGet(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File)
                If (bloqueoDatatable.Count > 0 AndAlso Not bloqueoDatatable(0).IsSesionNull AndAlso bloqueoDatatable(0).Sesion <> Me.IndexerDesktopGlobal.SesionId) Then
                    MessageBox.Show("El registro se encuentra bloqueado por el usuario del equipo: " & bloqueoDatatable(0).PCName, "Registro bloqueado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Return False
                End If

                dbmCore.Transaction_Begin()
                dbmImaging.Transaction_Begin()

                ' Bloquear el item
                Dim bloqueoType As New DBImaging.SchemaProcess.TBL_Dashboard_CapturasType()
                bloqueoType.fk_Usuario_log = Me.IndexerSesion.Usuario.id
                bloqueoType.Sesion = Me.IndexerDesktopGlobal.SesionId
                bloqueoType.PCName = Me.IndexerDesktopGlobal.PcName

                dbmImaging.SchemaProcess.TBL_Dashboard_Capturas.DBUpdate(bloqueoType, FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File)

                Dim fileImagingDataTable = dbmCore.SchemaImaging.TBL_File.DBGet(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, Nothing)
                FileImagingRow = fileImagingDataTable(fileImagingDataTable.Count - 1)

                Dim fileType As New DBCore.SchemaImaging.TBL_FileType()
                fileType.fk_Usuario_Log = Me.IndexerSesion.Usuario.id
                dbmCore.SchemaImaging.TBL_File.DBUpdate(fileType, FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, FileImagingRow.id_Version)

                Dim expedienteDataTable = dbmCore.SchemaProcess.TBL_Expediente.DBGet(FileCoreRow.fk_Expediente)
                ExpedienteRow = expedienteDataTable(0)

                Dim folderImagingDataTable = dbmCore.SchemaImaging.TBL_Folder.DBGet(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder)

                _idCargue = folderImagingDataTable(0).fk_Cargue
                _idCarguePaquete = folderImagingDataTable(0).fk_Cargue_Paquete

                ' Leer el listado de imagenes
                manager = New FileProviderManager(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, dbmImaging, Me.IndexerSesion.Usuario.id)
                manager.Connect()

                If FileImagingRow.Es_Anexo Then
                    _ImageCount = manager.GetFolios(FileImagingRow.fk_Anexo)
                Else
                    _ImageCount = manager.GetFolios(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, FileImagingRow.id_Version)
                End If

                View.SetTitle("Reproceso [Ex: " & FileCoreRow.fk_Expediente & " - Fo: " & FileCoreRow.fk_Folder & " - Fi: " & FileCoreRow.id_File & " - Ver: " & FileImagingRow.id_Version & "]")

                IndexerView.Information = ""
                Dim llavesDataTable = dbmCore.SchemaProcess.CTA_Expediente_LLave.DBFindByid_Expediente(FileCoreRow.fk_Expediente)
                For Each Llave In llavesDataTable
                    IndexerView.Information &= "[" & Llave.Nombre_Proyecto_Llave & "]: " & Llave.Valor_Llave.ToString() & vbCrLf
                Next

                ' Anexos del documento
                AnexosDataTable = dbmCore.SchemaImaging.CTA_Documentos.DBFindByfk_Expedientefk_Folder(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder)

                LoadConfig(dbmCore, dbmImaging, ExpedienteRow.fk_Entidad, ExpedienteRow.fk_Proyecto, ExpedienteRow.fk_Esquema)

                ' Definir si se puede cambiar el esquema
                Dim totalFiles = dbmCore.SchemaImaging.PA_Get_Files_No_Anexo.DBExecute(ExpedienteRow.id_Expediente)

                Me.IndexerView.Esquema_Enabled = (totalFiles = 1)


                dbmImaging.Transaction_Commit()
                dbmCore.Transaction_Commit()

            Catch ex As Exception
                If (dbmCore IsNot Nothing) Then dbmCore.Transaction_Rollback()
                If (dbmImaging IsNot Nothing) Then dbmImaging.Transaction_Rollback()

                Throw

            Finally
                If (manager IsNot Nothing) Then manager.Disconnect()
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            End Try

            ' Liberar la memoria
            For Each Item In _Folders
                Item.Dispose()
            Next

            ' Liberar la memoria
            Me.Clear()


            Application.DoEvents()

            _CurrentFolderIndex = 0
            _CurrentDocumentFileIndex = 0
            _CurrentFolioIndex = 0
            _CurrentImageIndex = 0

            ' Crear data inicial
            Dim newFolder = New Miharu.Imaging.Indexer.Generic.Folder(Me)
            Folders.Add(newFolder)

            Dim newDocumento = newFolder.NewDocumentFile()
            newFolder.Add(newDocumento)

            For i = 0 To _ImageCount - 1
                Dim newFolio = newDocumento.NewFolio(True)
                newDocumento.Add(newFolio)
            Next

            InicializarFolio(_CurrentFolioIndex)

            View.ThumbnailPanel.Controls.Add(newFolder.Panel)

            Try
                Dim hilo As New Thread(AddressOf InicializarFolios)
                hilo.Start()
            Catch ext As ThreadStateException
                MessageBox.Show("Error: " + ext.Message, "Error en Thread", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("NextIndexingElement", ex)
            End Try

            '-------------------------------------------------------------------------------------------------------------
            ' LOGGING - PERFORMANCE
            '-------------------------------------------------------------------------------------------------------------
            Dim fechaFinProceso = Now
            Dim traceMessage As String = ""
            traceMessage &= "Duración:" & vbTab & (fechaInicioProceso - fechaFinProceso).TotalMilliseconds & vbTab
            traceMessage &= "Expediente:" & vbTab & FileCoreRow.fk_Expediente & vbTab
            traceMessage &= "Folder:" & vbTab & FileCoreRow.fk_Folder & vbTab
            traceMessage &= "File:" & vbTab & FileCoreRow.id_File & vbTab
            DesktopTrace.Trace(traceMessage, DesktopTrace.CategoryEnum.Performance, 1, 0, TraceEventType.Information, "[Reproceso][NextIndexingElement]")
            '-------------------------------------------------------------------------------------------------------------

            Return True
        End Function

        Public Overrides Function Indexar() As DialogResult
            View.Unlock = True

            '_InderxerView.Esquema_Enabled = True
            IndexerView.Esquema_Refresh()
            IndexerView.Esquema_Index = CInt(IIf(IndexerView.Esquema_DataSource.Count = 1, 0, -1))
            IndexerView.Esquema_Value = ExpedienteRow.fk_Esquema

            IndexerView.TipoDocumental_Enabled = True
            IndexerView.TipoDocumental_Refresh()
            IndexerView.TipoDocumental_Index = CInt(IIf(IndexerView.TipoDocumental_DataSource.Count = 1, 0, -1))
            IndexerView.TipoDocumental_Value = FileCoreRow.fk_Documento

            CurrentFolder.Esquema = IndexerView.Esquema_Value
            CurrentDocumentFile.TipoDocumento = IndexerView.TipoDocumental_Value.Value
            CurrentDocumentFile.NombreTipoDocumento = IndexerView.TipoDocumental_Text

            _Ciclo += 1

            View.ActivarControles(True)
            Return View.ShowDialog()
        End Function

        Public Overrides Function Reproceso(ByVal nMotivo As Short) As Boolean
            Throw New NotImplementedException()
        End Function

#End Region

#Region " Metodos "

        Protected Overrides Sub LoadConfig(ByRef dbmCore As DBCore.DBCoreDataBaseManager, ByRef dbmImaging As DBImaging.DBImagingDataBaseManager, ByVal nEntidad As Short, ByVal nProyecto As Short, ByVal nEsquema As Short)

            CargarCadenasConexion()

            EsquemaDataTable = dbmCore.SchemaConfig.TBL_Esquema.DBGet(nEntidad, nProyecto, Nothing)
            'DocumentoDataTable = dbmImaging.SchemaConfig.CTA_Documento.DBFindByfk_Entidadfk_Proyectofk_EsquemaEliminado(nEntidad, nProyecto, Nothing, False)
            DocumentoIndexacionDataTable = dbmImaging.SchemaConfig.CTA_Documento_Indexacion.DBFindByfk_Entidadfk_Proyectofk_EsquemaEliminado(nEntidad, nProyecto, Nothing, False)
            IndexerView.Esquema_DataSource = New DataView(EsquemaDataTable)
            IndexerView.Esquema_Value = ExpedienteRow.fk_Esquema

            Dim Orden = New DBImaging.SchemaConfig.CTA_CampoEnumList()
            Me.CamposDataTable = dbmImaging.SchemaConfig.CTA_Campo.DBFindByfk_Entidadfk_Documentofk_Proyectoid_EsquemaUsa_CapturaEs_Campo_IndexacionEliminado_Campo(Me.IndexerImagingGlobal.ProyectoImagingRow.fk_Entidad, FileCoreRow.fk_Documento, Me.IndexerImagingGlobal.ProyectoImagingRow.fk_Proyecto, nEsquema, True, Nothing, False, 0, Orden)


            'IndexerView.TipoDocumental_DataSource = New DataView(DocumentoDataTable)
            IndexerView.TipoDocumental_DataSource = New DataView(DocumentoIndexacionDataTable)

        End Sub

        Public Sub SetData(ByVal nExpediente As Long, ByVal nFolder As Short, ByVal nFile As Short)
            Me._idExpediente = nExpediente
            Me._idFolder = nFolder
            Me._idFile = nFile
        End Sub

        Public Sub SetData(nSede As Slyg.Tools.SlygNullable(Of Short), nCentroProcesamiento As Slyg.Tools.SlygNullable(Of Short), nMotivoReproceso As Short)
            Me._sede = nSede
            Me._centroProcesamiento = nCentroProcesamiento
            Me._motivoReproceso = nMotivoReproceso
        End Sub

        Private Sub CargarCadenasConexion()
            Dim dbmSecurity As DBSecurity.DBSecurityDataBaseManager = Nothing

            Try
                dbmSecurity = New DBSecurity.DBSecurityDataBaseManager(Me.IndexerDesktopGlobal.ConnectionStrings.Security)
                dbmSecurity.Connection_Open(1)
                Dim ModuloDataTable = dbmSecurity.SchemaSecurity.TBL_Modulo.DBGet(Nothing)

                If (ModuloDataTable.Count > 0) Then
                    For Each modulo In ModuloDataTable
                        Select Case modulo.id_Modulo
                            Case 13
                                Me._AgrarioConnectionString = modulo.ConnectionString
                        End Select
                    Next
                Else
                    Throw New Exception("No se pudo cargar la cadena de conexión para el módulo: " & Program.ModuloId.ToString())
                End If
            Finally
                dbmSecurity.Connection_Close()
            End Try
        End Sub



        Private Sub consultarCampos()

        End Sub


#End Region


#Region "Funciones"

        Private Sub ExtraerDigitoYCodigo(ByVal nCBarras As String, ByVal nPosicion As Integer, ByRef nNewCBarras As String, ByRef nDigito As Integer)
            nNewCBarras = nCBarras.Substring(0, nPosicion) & "0" & nCBarras.Substring(nPosicion + 1, nCBarras.Length - nPosicion - 1)
            nDigito = CInt(nCBarras.Substring(nPosicion, 1))
        End Sub

        Function FMDigitoVerif(COdigoBarra As String, valor As Integer) As Integer
            Const VGTablaDigito As String = "716759534743413729231917130703"
            Dim VTSuma = 0
            Dim VTNumero As Integer
            Dim i As Integer
            Dim VTN As Integer
            Dim VTConstante As Integer
            For i = Len(COdigoBarra) To 1 Step -1
                VTNumero = CInt(Mid(COdigoBarra, i, 1))
                VTN = i + valor - Len(COdigoBarra)
                VTConstante = CInt(Val(Mid(VGTablaDigito, 2 * VTN, 1)) + 10 * Val(Mid(VGTablaDigito, 2 * VTN - 1, 1)))
                VTSuma = VTSuma + VTNumero% * VTConstante
            Next i%

            Dim VTDig = VTSuma Mod 11
            If VTDig > 1 Then
                VTDig = 11 - VTDig
            End If

            FMDigitoVerif = VTDig
        End Function


        Private Function ValidarOficina(nOficina As String, ByRef nMessage As String) As Boolean
            If (Not Slyg.Tools.DataConvert.IsNumeric(nOficina)) Then
                nMessage = "Código de oficina no válido: " & nOficina
                Return False
            End If

            oficinasDataView.RowFilter = "id_Oficina = " & nOficina

            If (oficinasDataView.Count = 0) Then
                nMessage = "La oficina no existe: " & nOficina
                Return False
            End If

            Dim oficinaDataRow = CType(oficinasDataView(0).Row, DBAgrario.SchemaConfig.TBL_OficinaRow)
            If (Not oficinaDataRow.Activa) Then
                nMessage = "La oficina no se encuentra activa: " & nOficina
                Return False
            End If

            Return True
        End Function

        Private Function ValidarFecha(nFecha As String, ByRef nMessage As String) As Boolean
            Dim year = nFecha.Substring(0, 4)
            Dim month = nFecha.Substring(4, 2)
            Dim day = nFecha.Substring(6, 2)

            If (Not Slyg.Tools.DataConvert.IsDate(year & "/" & month & "/" & day, Slyg.Tools.DataConvert.EnumDateFormat.yyyyMMdd, "/"c)) Then
                nMessage = "La fecha no es válida: " & nFecha
                Return False
            End If

            Dim FechaCB = New DateTime(year, month, day)
            Dim FechaHoy = New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)

            If FechaCB > FechaHoy Then
                nMessage = "La Fecha de Movimiento no puede ser mayor al día de hoy"
                Return False
            End If

            Return True
        End Function

        Private Function ValidarCodTransaccion(nCodTransaccion As String, ByRef nMessage As String) As Boolean
            If (Not Slyg.Tools.DataConvert.IsNumeric(nCodTransaccion)) Then
                nMessage = "Código de transacción no es válido: " & nCodTransaccion
                Return False
            End If

            codTransaccionDataView.RowFilter = "Codigo_Transaccion = " & nCodTransaccion

            If (codTransaccionDataView.Count = 0) Then
                nMessage = "El código de transacción no existe: " & nCodTransaccion
                Return False
            End If

            Return True
        End Function

#End Region
    End Class

End Namespace