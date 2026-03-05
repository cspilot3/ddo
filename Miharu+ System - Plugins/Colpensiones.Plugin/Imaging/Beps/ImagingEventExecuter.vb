Imports Miharu.Imaging.Library.Eventos
Imports Miharu.Desktop.Library.Plugins
Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library.Config
Imports Slyg.Tools

Namespace Imaging.Beps
    Public Class ImagingEventExecuter
        Inherits EventExecuter
        Implements IEventExecuter

#Region " Declaraciones "

        Private _plugin As Plugin

#End Region

#Region " Constructores "

        Public Sub New(ByVal nPlugin As Plugin)
            Me._plugin = nPlugin
        End Sub

#End Region

#Region " Implementacion IEventExecuter "

        Public Sub AbrirFechaProceso(nidEntidadProcesamiento As Short, nidEntidad As Short, nidProyecto As Short, nidFechaProceso As Integer) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.AbrirFechaProceso

        End Sub

        Public Sub AbrirOt(nidOt As Integer) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.AbrirOt

        End Sub

        Public Sub CargarPrecinto(nidOt As Integer, nidPrecinto As Integer, nContenedorDesktop As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl, ByRef nValido As Boolean, ByRef nMsgError As String) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.CargarPrecinto

        End Sub

        Public Sub CerrarFechaProceso(nidEntidadProcesamiento As Short, nidEntidad As Short, nidProyecto As Short, nidFechaProceso As Integer) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.CerrarFechaProceso

        End Sub

        Public Sub CerrarOt(nidOt As Integer) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.CerrarOt
            Dim dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = Nothing
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing


            Try
                dbmIntegration = New DBIntegration.DBIntegrationDataBaseManager(Me._plugin.ColpensionesConnectionString)
                dbmCore = New DBCore.DBCoreDataBaseManager(Me._plugin.Manager.DesktopGlobal.ConnectionStrings.Core)
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Me._plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)


                dbmIntegration.Connection_Open(_plugin.Manager.Sesion.Usuario.id)
                dbmCore.Connection_Open(_plugin.Manager.Sesion.Usuario.id)
                dbmImaging.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                Dim ValidoOT = dbmCore.SchemaProcess.PA_Valida_Rechazos.DBExecute(_plugin.Manager.ImagingGlobal.ProyectoImagingRow.fk_Entidad, _plugin.Manager.ImagingGlobal.ProyectoImagingRow.fk_Proyecto, nidOt)

                If ValidoOT = False Then
                    Dim OTRow = New DBImaging.SchemaProcess.TBL_OTType

                    With OTRow
                        .Cerrado = False
                        .Fecha_Cierre = Nothing
                        .fk_Usuario_Cierre = Nothing
                    End With

                    dbmImaging.SchemaProcess.TBL_OT.DBUpdate(OTRow, nidOt)

                    MessageBox.Show("No se puede cerrar la OT puesto que no se ha realizado el proceso de rechazos.", "Cerrar OT", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

            Catch ex As Exception
                MessageBox.Show(ex.Message, "Cerrar OT", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Finally
                If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

        Public Sub CrearFechaProceso(nidEntidadProcesamiento As Short, nidEntidad As Short, nidProyecto As Short, nidFechaProceso As Integer) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.CrearFechaProceso

        End Sub

        Public Sub CrearOt(nidOt As Integer) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.CrearOt

        End Sub

        Public Sub Ejecutar_Cruce_En_Linea(nCampo As DBCore.SchemaImaging.CTA_Busqueda_Files_DataRow, nidExpediente As Long, nidFolder As Short, nidFile As Short) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.Ejecutar_Cruce_En_Linea

        End Sub

        Public Sub EliminarImagen(nidExpediente As Long, nidFolder As Short, nidFile As Short) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.EliminarImagen

        End Sub

        Public Sub EnviarReproceso(nidExpediente As Long, nidFolder As Short, nidFile As Short, nidVersion As Short) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.EnviarReproceso
            Dim dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = Nothing
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            Dim id_Campo As Int16
            Dim No_Formulario As String = ""

            Try
                dbmIntegration = New DBIntegration.DBIntegrationDataBaseManager(_plugin.ColpensionesConnectionString)
                dbmCore = New DBCore.DBCoreDataBaseManager(_plugin.Manager.DesktopGlobal.ConnectionStrings.Core)

                dbmIntegration.Connection_Open(_plugin.Manager.Sesion.Usuario.id)
                dbmCore.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                Dim fk_Documento = dbmCore.SchemaProcess.TBL_File.DBFindByfk_Expedientefk_Folderid_File(nidExpediente, nidFolder, nidFile)(0).fk_Documento

                Dim CamposDataTable = dbmCore.SchemaConfig.TBL_Campo.DBFindByfk_Entidadfk_Documento(_plugin.Manager.ImagingGlobal.Entidad, fk_Documento)
                Dim drowCampoFiltrado = CamposDataTable.Select("Nombre_Campo = 'No. Formulario'")

                If (drowCampoFiltrado.Count > 0) Then
                    id_Campo = CType(drowCampoFiltrado(0).Item("id_Campo").ToString(), Integer)

                    Dim FileDataDataTable = dbmCore.SchemaProcess.TBL_File_Data.DBFindByfk_Expedientefk_Folderfk_Filefk_Documentofk_Campo(nidExpediente, nidFolder, nidFile, fk_Documento, id_Campo)

                    If FileDataDataTable.Count > 0 Then
                        Dim No_FormularioDataTable = dbmIntegration.SchemaColpensionesBEPS.TBL_Expediente_No_Formulario.DBFindByfk_Expedientefk_Folder(nidExpediente, nidFolder)
                        If (No_FormularioDataTable.Count > 0) Then
                            dbmIntegration.SchemaColpensionesBEPS.TBL_Expediente_No_Formulario.DBDelete(No_FormularioDataTable(0).id_No_Formulario, No_FormularioDataTable(0).fk_OT_Tipo)
                        End If
                    End If

                End If

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Enviar Reproceso", ex)
            Finally
                If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            End Try
        End Sub

        Public Sub FinalizarActualizarDatosBusqueda(nCampo As DBCore.SchemaImaging.CTA_Busqueda_Files_DataRow, nidExpediente As Long, nidFolder As Short, nidFile As Short) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarActualizarDatosBusqueda

        End Sub

        Public Sub FinalizarCalidad(nidExpediente As Long, nidFolder As Short, nidFile As Short, nidVersion As Short) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarCalidad

        End Sub

        Public Sub FinalizarCargue(nidCargue As Integer) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarCargue

        End Sub

        Public Sub FinalizarContenedorEmpaque(nidOt As Integer, nidEmpaque As Integer, IdEmpaqueContenedor As Integer) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarContenedorEmpaque

        End Sub

        Public Sub FinalizarContenedorEmpaqueEliminar(nidOt As Integer, nToken As String) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarContenedorEmpaqueEliminar

        End Sub

        Public Sub FinalizarEliminarPrecinto(nidOt As Integer, nidPrecinto As Integer) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarEliminarPrecinto

        End Sub

        Public Sub FinalizarIndexacion(nidCargue As Integer, nidPaquete As Short) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarIndexacion
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing

            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(_plugin.Manager.DesktopGlobal.ConnectionStrings.Core)

                dbmCore.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                dbmCore.SchemaProcess.PA_Expedientes_Rechazados_Doc_Oligatorios.DBExecute(nidCargue, nidPaquete)

            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            End Try
        End Sub

        Public Sub FinalizarLoadConfig(ByRef IndexerView As Miharu.Imaging.Indexer.View.Indexacion.IIndexerView, FileCoreRow As DBCore.SchemaProcess.TBL_FileRow) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarLoadConfig

        End Sub

        Public Sub FinalizarPreCaptura(nidExpediente As Long, nidFolder As Short, nidFile As Short, nidVersion As Short) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarPreCaptura
            'Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            'Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            'Try

            '    dbmCore = New DBCore.DBCoreDataBaseManager(_plugin.Manager.DesktopGlobal.ConnectionStrings.Core)
            '    dbmImaging = New DBImaging.DBImagingDataBaseManager(_plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)


            '    dbmCore.Connection_Open(_plugin.Manager.Sesion.Usuario.id)
            '    dbmImaging.Connection_Open(_plugin.Manager.Sesion.Usuario.id)



            '    dbmImaging.Transaction_Begin(IsolationLevel.ReadUncommitted)
            '    dbmCore.Transaction_Begin()

            '    'Crea el estado del Folder en Core
            '    Dim folderEstadoDataTable = dbmCore.SchemaProcess.TBL_Folder_estado.DBFindByfk_Expedientefk_FolderModulo(nidExpediente, nidFolder, DesktopConfig.Modulo.Imaging)

            '    If folderEstadoDataTable.Count = 0 Then
            '        Dim insertEstado As New DBCore.SchemaProcess.TBL_Folder_estadoType()
            '        insertEstado.fk_Expediente = nidExpediente
            '        insertEstado.fk_Folder = nidFolder
            '        insertEstado.Modulo = DesktopConfig.Modulo.Imaging
            '        insertEstado.fk_Estado = DBCore.EstadoEnum.Indexado
            '        insertEstado.fk_Usuario = _plugin.Manager.Sesion.Usuario.id
            '        insertEstado.Fecha_Log = SlygNullable.SysDate
            '        dbmCore.SchemaProcess.TBL_Folder_estado.DBInsert(insertEstado)
            '    End If

            '    'Actualizar estado a indexado
            '    Dim updateEstado As New DBCore.SchemaProcess.TBL_File_EstadoType()
            '    updateEstado.Fecha_Log = SlygNullable.SysDate
            '    updateEstado.fk_Usuario = _plugin.Manager.Sesion.Usuario.id
            '    updateEstado.fk_Estado = DBCore.EstadoEnum.Indexado
            '    dbmCore.SchemaProcess.TBL_File_Estado.DBUpdate(updateEstado, nidExpediente, nidFolder, nidFile, DesktopConfig.Modulo.Imaging)


            '    ' Actualizar Dashboard
            '    dbmImaging.SchemaProcess.TBL_Dashboard_Capturas.DBDelete(nidExpediente, nidFolder, nidFile)

            '    dbmImaging.Transaction_Commit()
            '    dbmCore.Transaction_Commit()


            '    ' Actualizar el estado de los cargues que ya fueron procesados
            '    Dim folderImagingDataTable = dbmCore.SchemaImaging.TBL_Folder.DBGet(nidExpediente, nidFolder)

            '    Dim idCargue = folderImagingDataTable(0).fk_Cargue
            '    Dim idCarguePaquete = folderImagingDataTable(0).fk_Cargue_Paquete
            '    Try
            '        dbmImaging.Transaction_Begin(IsolationLevel.ReadUncommitted)

            '        Dim carguePaqueteFileDataTable = dbmImaging.SchemaProcess.CTA_Cargue_Paquete_Estado_File.DBFindByid_Cargueid_Cargue_Paquete(idCargue, idCarguePaquete)
            '        If (carguePaqueteFileDataTable.Count > 0) Then
            '            Dim updatePaquete = New DBImaging.SchemaProcess.TBL_Cargue_PaqueteType()
            '            updatePaquete.fk_Estado = carguePaqueteFileDataTable(0).Estado_File
            '            dbmImaging.SchemaProcess.TBL_Cargue_Paquete.DBUpdate(updatePaquete, idCargue, idCarguePaquete)

            '            Dim cargueFileDataTable = dbmImaging.SchemaProcess.CTA_Cargue_Estado.DBFindByid_Cargue(idCargue)
            '            If (cargueFileDataTable.Count > 0) Then
            '                Dim updateCargue = New DBImaging.SchemaProcess.TBL_CargueType()
            '                updateCargue.fk_Estado = cargueFileDataTable(0).Estado_File
            '                dbmImaging.SchemaProcess.TBL_Cargue.DBUpdate(updateCargue, idCargue)
            '            End If
            '        End If

            '        dbmImaging.Transaction_Commit()
            '    Catch ex As Exception
            '        If (dbmImaging IsNot Nothing) Then dbmImaging.Transaction_Rollback()
            '        Throw
            '    End Try

            'Catch ex As Exception
            '    If (dbmCore IsNot Nothing) Then dbmCore.Transaction_Rollback()
            '    If (dbmImaging IsNot Nothing) Then dbmImaging.Transaction_Rollback()

            '    DesktopMessageBoxControl.DesktopMessageShow("PreCaptura", ex)

            'Finally
            '    If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            '    If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            'End Try
        End Sub

        Public Sub FinalizarPrecinto() Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarPrecinto

        End Sub

        Public Sub FinalizarPrecintoEmpaque(nidOt As Integer, nidPrecinto As Integer) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarPrecintoEmpaque

        End Sub

        Public Sub FinalizarPrimeraCaptura(nidExpediente As Long, nidFolder As Short, nidFile As Short, nidVersion As Short) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarPrimeraCaptura

        End Sub

        Public Sub FinalizarProcesoAdicionalCaptura(nidExpediente As Long, nidFolder As Short, nidFile As Short, nidVersion As Short) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarProcesoAdicionalCaptura

        End Sub

        Public Sub FinalizarReclasificar(nidExpediente As Long, nidFolder As Short, nidFile As Short, nidVersion As Short) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarReclasificar

        End Sub

        Public Sub FinalizarRecorte(nidExpediente As Long, nidFolder As Short, nidFile As Short, nidVersion As Short) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarRecorte

        End Sub

        Public Sub FinalizarReIndexacion(nidCargue As Integer, nidPaquete As Short) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarReIndexacion
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing

            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(_plugin.Manager.DesktopGlobal.ConnectionStrings.Core)

                dbmCore.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                dbmCore.SchemaProcess.PA_Expedientes_Rechazados_Doc_Oligatorios.DBExecute(nidCargue, nidPaquete)

            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            End Try
        End Sub

        Public Sub FinalizarReproceso(nidExpediente As Long, nidFolder As Short, nidFile As Short, nidVersion As Short) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarReproceso
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing

            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(_plugin.Manager.DesktopGlobal.ConnectionStrings.Core)

                dbmCore.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                dbmCore.SchemaProcess.PA_Expedientes_Rechazados_Doc_Oligatorios_X_Expediente.DBExecute(nidExpediente, nidFolder)

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Finalizar Reproceso", ex)
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            End Try
        End Sub

        Public Sub FinalizarSegundaCaptura(nidExpediente As Long, nidFolder As Short, nidFile As Short, nidVersion As Short) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarSegundaCaptura

        End Sub

        Public Sub FinalizarTerceraCaptura(nidExpediente As Long, nidFolder As Short, nidFile As Short, nidVersion As Short) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarTerceraCaptura

        End Sub

        Public Sub FinalizarValidaciones(nidExpediente As Long, nidFolder As Short, nidFile As Short, nidVersion As Short) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarValidaciones

        End Sub

        Public Sub GuardarContenedor(nidOt As Integer, nidPrecinto As Integer, nidContenedor As Integer) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.GuardarContenedor

        End Sub

        Public Sub Reprocesar(nidExpediente As Long, nidFolder As Short, nidFile As Short) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.Reprocesar

        End Sub

        Public Sub Validar_Reprocesar(nidExpediente As Long, nidFolder As Short, nidDocumento As Integer, nidCampo As Integer, nidCampoTablaAsociada As Integer, nesCampo As Boolean, ByRef nValido As Boolean, ByRef nMsgError As String) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.Validar_Reprocesar

        End Sub

        Public Sub ValidarActualizarDatoBusqueda(nCampo As DBCore.SchemaImaging.CTA_Busqueda_Files_DataRow, nidExpediente As Long, nidFolder As Short, nidFile As Short, newValor_File_Data As Object, ByRef Result As Boolean) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.ValidarActualizarDatoBusqueda

        End Sub

        Public Sub ValidarCerrarFechaProcesoNoOT(nidEntidadProcesamiento As Short, nidEntidad As Short, nidProyecto As Short, nidFechaProceso As Integer, ByRef nValido As Boolean, ByRef nValido2 As Boolean, ByRef nMsgError As String) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.ValidarCerrarFechaProcesoNoOT
            Dim dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = Nothing
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim Valido As Boolean = True

            Try
                dbmIntegration = New DBIntegration.DBIntegrationDataBaseManager(Me._plugin.ColpensionesConnectionString)
                dbmCore = New DBCore.DBCoreDataBaseManager(Me._plugin.Manager.DesktopGlobal.ConnectionStrings.Core)
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Me._plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)


                dbmIntegration.Connection_Open(_plugin.Manager.Sesion.Usuario.id)
                dbmCore.Connection_Open(_plugin.Manager.Sesion.Usuario.id)
                dbmImaging.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                Dim OTDataTable = dbmImaging.SchemaProcess.TBL_OT.DBFindByfk_Entidadfk_Proyectofk_fecha_procesoCerrado(_plugin.Manager.ImagingGlobal.ProyectoImagingRow.fk_Entidad, _plugin.Manager.ImagingGlobal.ProyectoImagingRow.fk_Proyecto, nidFechaProceso, Nothing)

                If OTDataTable.Count > 0 Then
                    For Each OTRows In OTDataTable
                        Dim ValidoOT = dbmCore.SchemaProcess.PA_Valida_Rechazos.DBExecute(_plugin.Manager.ImagingGlobal.ProyectoImagingRow.fk_Entidad, _plugin.Manager.ImagingGlobal.ProyectoImagingRow.fk_Proyecto, OTRows.id_OT)

                        If ValidoOT = False Then
                            nValido = False
                            MessageBox.Show("No se puede cerrar la fecha de proceso puesto que no se ha realizado el proceso de rechazos para una o mas OT's", "Cerrar Fecha Proceso", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Return
                        End If
                    Next

                End If
            Catch ex As Exception
                nValido = False
                nMsgError = ex.Message
            Finally
                If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

        Public Sub ValidarEmpaque(nidOt As Integer, nidEmpaque As Short, nidEsquema As Short, nToken As String, ByRef nValido As Boolean, ByRef nMsgError As String) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.ValidarEmpaque

        End Sub

        Public Sub ValidarPrecintoEmpaque(nidOt As Integer, nidPrecinto As Integer, ndbImaging As DBImaging.DBImagingDataBaseManager, nContenedorDesktop As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl, ByRef nValido As Boolean, ByRef nMsgError As String) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.ValidarPrecintoEmpaque

        End Sub

        Public Sub ValidarSaveCalidad(campos As System.Collections.Generic.List(Of Miharu.Imaging.Indexer.View.Indexacion.CampoCaptura), fk_documento As Integer, ByRef Result As Boolean) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.ValidarSaveCalidad

        End Sub

        Public Sub ValidarSaveLabelCaptura(campos As System.Collections.Generic.List(Of Miharu.Imaging.Indexer.View.Indexacion.CampoCaptura), fk_documento As Integer, nidExpediente As Long, nidFolder As Short, nidFile As Short, TipoCaptura As String, ByRef Result As Boolean) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.ValidarSaveLabelCaptura
            Dim dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = Nothing
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            Dim id_Campo As Int16
            Dim No_Formulario As String = ""
            Dim Cedula_Ciudadano As String = ""


            Try
                dbmIntegration = New DBIntegration.DBIntegrationDataBaseManager(_plugin.ColpensionesConnectionString)
                dbmCore = New DBCore.DBCoreDataBaseManager(_plugin.Manager.DesktopGlobal.ConnectionStrings.Core)

                dbmIntegration.Connection_Open(_plugin.Manager.Sesion.Usuario.id)
                dbmCore.Connection_Open(_plugin.Manager.Sesion.Usuario.id)


                Dim CamposDataTable = dbmCore.SchemaConfig.TBL_Campo.DBFindByfk_Entidadfk_Documento(_plugin.Manager.ImagingGlobal.Entidad, fk_documento)
                Dim drowCampoFiltrado = CamposDataTable.Select("Nombre_Campo = 'No. Formulario'")

                If (drowCampoFiltrado.Count > 0) Then
                    id_Campo = CType(drowCampoFiltrado(0).Item("id_Campo").ToString(), Integer)

                    'recorre cada campo para encontrar valor de No. Formulario
                    For Each Item In campos
                        If Item.id = id_Campo Then
                            No_Formulario = Item.Control.Value
                        End If
                    Next
                    If Trim(No_Formulario) <> "" Then
                        Dim dtResult = dbmIntegration.SchemaColpensionesBEPS.PA_Expediente_No_Formulario.DBExecute(Trim(No_Formulario), _plugin.Manager.ImagingGlobal.Entidad, _plugin.Manager.ImagingGlobal.Proyecto, nidExpediente, nidFolder, TipoCaptura)
                        If dtResult.Rows.Count > 0 Then
                            If dtResult.Rows(0).Item("TipoResultado").ToString = "ERROR" Then
                                DesktopMessageBoxControl.DesktopMessageShow(dtResult.Rows(0).Item("Mensaje").ToString(), "Guardar Captura", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                                Result = False
                            End If
                        End If
                    End If
                End If

                'Numero de cedula para el tema de duplicidad de registros'
                Dim DocumentoDataTable = dbmCore.SchemaConfig.TBL_Documento.DBFindByfk_Entidadfk_Proyectoid_DocumentoEliminado(_plugin.Manager.ImagingGlobal.Entidad, _plugin.Manager.ImagingGlobal.Proyecto, fk_documento, False)
                Dim drowCampoFiltrado2 = CamposDataTable.Select("Nombre_Campo = 'CEDULA CIUDADANO'")

                If (drowCampoFiltrado2.Count > 0) Then
                    id_Campo = CType(drowCampoFiltrado2(0).Item("id_Campo").ToString(), Integer)

                    'recorre cada campo para encontrar valor de No. Formulario
                    For Each Item In campos
                        If Item.id = id_Campo Then
                            Cedula_Ciudadano = Item.Control.Value
                        End If
                    Next

                    If ((DocumentoDataTable.Count > 0) And (Trim(Cedula_Ciudadano) <> "")) Then
                        Dim No_FormularioDataTable = dbmIntegration.SchemaColpensionesBEPS.TBL_Expediente_No_Formulario.DBFindByfk_Expedientefk_Folder(nidExpediente, nidFolder)
                        If No_FormularioDataTable.Count > 0 Then
                            If ((DocumentoDataTable(0).Nombre_Documento = "Fotocopia de Cedula") Or ((No_FormularioDataTable.Count > 0) And (No_FormularioDataTable(0).IsCedula_CiudadanoNull) And (nidFile = 1))) Then

                                Dim UpdateExpediente_No_Formulario As New DBIntegration.SchemaColpensionesBEPS.TBL_Expediente_No_FormularioType()
                                UpdateExpediente_No_Formulario.Cedula_Ciudadano = Cedula_Ciudadano

                                dbmIntegration.SchemaColpensionesBEPS.TBL_Expediente_No_Formulario.DBUpdate(UpdateExpediente_No_Formulario, No_FormularioDataTable(0).id_No_Formulario, No_FormularioDataTable(0).fk_OT_Tipo)

                            End If
                        Else
                            'Insertar
                            dbmIntegration.SchemaColpensionesBEPS.PA_Insercion_Cedula_Ciudadano.DBExecute(_plugin.Manager.ImagingGlobal.Entidad, _plugin.Manager.ImagingGlobal.Proyecto, nidExpediente, nidFolder, Cedula_Ciudadano)
                        End If
                    End If
                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Validar guardar captura", ex)
                Result = False
            Finally
                If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            End Try
        End Sub

        Public Sub ValidarSavePrimeraCaptura(campos As System.Collections.Generic.List(Of Miharu.Imaging.Indexer.View.Indexacion.CampoCaptura), fk_documento As Integer, ByRef Result As Boolean) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.ValidarSavePrimeraCaptura

        End Sub

        Public Sub ValidarSaveSegundaCaptura(campos As System.Collections.Generic.List(Of Miharu.Imaging.Indexer.View.Indexacion.CampoCaptura), fk_documento As Integer, ByRef Result As Boolean) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.ValidarSaveSegundaCaptura

        End Sub

        Public Sub ValidarSaveTerceraCaptura(campos As System.Collections.Generic.List(Of Miharu.Imaging.Indexer.View.Indexacion.CampoCaptura), fk_documento As Integer, ByRef Result As Boolean) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.ValidarSaveTerceraCaptura

        End Sub

        Public Function Nombre_Imagen_Exportar(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nFk_Documento As Integer, ByVal nGrupo As Integer, ByRef nValido As Boolean, ByRef nMsgError As String) As String Implements IEventExecuter.Nombre_Imagen_Exportar

            Dim NombreArchivo As String = String.Empty
            Dim dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = Nothing

            Try
                dbmIntegration = New DBIntegration.DBIntegrationDataBaseManager(Me._plugin.ColpensionesConnectionString)

                dbmIntegration.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                NombreArchivo = dbmIntegration.SchemaColpensionesBEPS.PA_GetNombreArchivo.DBExecute(nidExpediente, nidFolder, nidFile, nFk_Documento, nGrupo)

                If NombreArchivo = String.Empty Then
                    nValido = False
                    nMsgError = "No se encontró nombre de imagen para el expediente: " & nidExpediente.ToString()
                End If

            Catch ex As Exception
                nValido = False
                Throw New Exception("Error al generar la Imagen del Expediente: (" + nidExpediente.ToString + ") Se genero el error:" + ex.Message, ex.InnerException)
            Finally
                If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
            End Try

            Return NombreArchivo

        End Function

        Public Function ExtensionImagen_Plugin(ByVal Entrada As Boolean) As String Implements IEventExecuter.ExtensionImagen_Plugin

            If Entrada Then
                Return _plugin.TipoImagenEntrada.Extension
            Else
                Return _plugin.TipoImagenSalida.Extension
            End If

        End Function

        Public Function IdFormatoImagen_Plugin(ByVal Entrada As Boolean) As Short Implements IEventExecuter.IdFormatoImagen_Plugin
            If Entrada Then
                Return _plugin.TipoImagenEntrada.idFormatoImagen
            Else
                Return _plugin.TipoImagenSalida.idFormatoImagen
            End If
        End Function

        Public Sub FinalizarCrucegenerico(nidEntidad As Short, nidProyecto As Short, nidFechaProceso As Integer, nidOT As Integer) Implements IEventExecuter.FinalizarCruceGenerico

        End Sub

        Function Nombre_Imagen_Agrupada_Exportar(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nGrupo As Integer, ByRef nValido As Boolean, ByRef nMsgError As String) As String Implements IEventExecuter.Nombre_Imagen_Agrupada_Exportar
            Dim NombreArchivo As String = String.Empty
            Dim dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = Nothing

            Try
                dbmIntegration = New DBIntegration.DBIntegrationDataBaseManager(Me._plugin.ColpensionesConnectionString)

                dbmIntegration.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                NombreArchivo = dbmIntegration.SchemaColpensionesBEPS.PA_GetNombreArchivo.DBExecute(nidExpediente, nidFolder, DBNull.Value, DBNull.Value, nGrupo)

                If NombreArchivo = String.Empty Then
                    nValido = False
                    nMsgError = "No se encontró nombre de imagen para el expediente: " & nidExpediente.ToString()
                End If

            Catch ex As Exception
                nValido = False
                Throw New Exception("Error al generar la Imagen del Expediente: (" + nidExpediente.ToString + ") Se genero el error:" + ex.Message, ex.InnerException)
            Finally
                If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
            End Try

            Return NombreArchivo
        End Function

        Public Sub FinalizarCorreccionCapturaMaquina(nidExpediente As Long, nidFolder As Short, nidFile As Short, nidVersion As Short) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarCorreccionCapturaMaquina

        End Sub

        Public Sub FinalizarPrimeraCapturaAnexo(nidAnexo As Long) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarPrimeraCapturaAnexo

        End Sub

        Public Sub FinalizarTerceraCapturaAnexo(nidAnexo As Long) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarTerceraCapturaAnexo

        End Sub

        Public Sub FinalizarPrepararDatagenerico(nidEntidad As Short, nidProyecto As Short, nidFechaProceso As Integer, nidOT As Integer) Implements IEventExecuter.FinalizarPrepararDataGenerico

        End Sub
#End Region

    End Class
End Namespace

