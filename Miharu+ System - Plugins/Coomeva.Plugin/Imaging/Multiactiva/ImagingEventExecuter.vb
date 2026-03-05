Imports Miharu.Imaging.Library.Eventos
Imports Miharu.Desktop.Library.Plugins
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library.Config
Imports Slyg.Tools

Namespace Imaging.Multiactiva

    Public Class ImagingEventExecuter
        Inherits EventExecuter
        Implements IEventExecuter

#Region " Declaraciones "

        ' ReSharper disable once NotAccessedField.Local
        Private _plugin As Plugin

#End Region

#Region " Constructores "

        Public Sub New(ByVal nPlugin As Plugin)
            Me._plugin = nPlugin
        End Sub

#End Region

#Region " Implementacion IEventExecuter "

        Public Sub EliminarImagen(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short) Implements IEventExecuter.EliminarImagen

        End Sub

        Public Sub FinalizarCargue(ByVal nidCargue As Integer) Implements IEventExecuter.FinalizarCargue

        End Sub

        Public Sub FinalizarIndexacion(ByVal nidCargue As Integer, ByVal nidPaquete As Short) Implements IEventExecuter.FinalizarIndexacion
            Dim dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = Nothing

            Try
                dbmIntegration = New DBIntegration.DBIntegrationDataBaseManager(_plugin.CoomevaConnectionString)

                dbmIntegration.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                dbmIntegration.SchemaCoomeva.PA_Multiactiva_Exp_Rechazados_Doc_Obligatorios.DBExecute(nidCargue, nidPaquete)

            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
            End Try
        End Sub

        Public Sub FinalizarReIndexacion(nidCargue As Integer, nidPaquete As Short) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarReIndexacion

        End Sub

        Public Sub ValidarSavePrimeraCaptura(ByVal campos As List(Of Miharu.Imaging.Indexer.View.Indexacion.CampoCaptura), ByVal fk_documento As Integer, ByRef Result As Boolean) Implements IEventExecuter.ValidarSavePrimeraCaptura

        End Sub

        Public Sub ValidarSaveSegundaCaptura(ByVal campos As List(Of Miharu.Imaging.Indexer.View.Indexacion.CampoCaptura), ByVal fk_documento As Integer, ByRef Result As Boolean) Implements IEventExecuter.ValidarSaveSegundaCaptura

        End Sub

        Public Sub ValidarSaveTerceraCaptura(ByVal campos As List(Of Miharu.Imaging.Indexer.View.Indexacion.CampoCaptura), ByVal fk_documento As Integer, ByRef Result As Boolean) Implements IEventExecuter.ValidarSaveTerceraCaptura

        End Sub

        Public Sub ValidarSaveCalidad(ByVal campos As List(Of Miharu.Imaging.Indexer.View.Indexacion.CampoCaptura), ByVal fk_documento As Integer, ByRef Result As Boolean) Implements IEventExecuter.ValidarSaveCalidad

        End Sub

        Public Sub FinalizarPrecaptura(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short) Implements IEventExecuter.FinalizarPreCaptura
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try

                dbmCore = New DBCore.DBCoreDataBaseManager(_plugin.Manager.DesktopGlobal.ConnectionStrings.Core)
                dbmImaging = New DBImaging.DBImagingDataBaseManager(_plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)


                dbmCore.Connection_Open(_plugin.Manager.Sesion.Usuario.id)
                dbmImaging.Connection_Open(_plugin.Manager.Sesion.Usuario.id)



                dbmImaging.Transaction_Begin(IsolationLevel.ReadUncommitted)
                dbmCore.Transaction_Begin()

                'Crea el estado del Folder en Core
                Dim folderEstadoDataTable = dbmCore.SchemaProcess.TBL_Folder_estado.DBFindByfk_Expedientefk_FolderModulo(nidExpediente, nidFolder, DesktopConfig.Modulo.Imaging)

                If folderEstadoDataTable.Count = 0 Then
                    Dim insertEstado As New DBCore.SchemaProcess.TBL_Folder_estadoType()
                    insertEstado.fk_Expediente = nidExpediente
                    insertEstado.fk_Folder = nidFolder
                    insertEstado.Modulo = DesktopConfig.Modulo.Imaging
                    insertEstado.fk_Estado = DBCore.EstadoEnum.Indexado
                    insertEstado.fk_Usuario = _plugin.Manager.Sesion.Usuario.id
                    insertEstado.Fecha_Log = SlygNullable.SysDate
                    dbmCore.SchemaProcess.TBL_Folder_estado.DBInsert(insertEstado)
                End If

                'Actualizar estado a indexado
                Dim updateEstado As New DBCore.SchemaProcess.TBL_File_EstadoType()
                updateEstado.Fecha_Log = SlygNullable.SysDate
                updateEstado.fk_Usuario = _plugin.Manager.Sesion.Usuario.id
                updateEstado.fk_Estado = DBCore.EstadoEnum.Indexado
                dbmCore.SchemaProcess.TBL_File_Estado.DBUpdate(updateEstado, nidExpediente, nidFolder, nidFile, DesktopConfig.Modulo.Imaging)


                ' Actualizar Dashboard
                dbmImaging.SchemaProcess.TBL_Dashboard_Capturas.DBDelete(nidExpediente, nidFolder, nidFile)

                dbmImaging.Transaction_Commit()
                dbmCore.Transaction_Commit()


                ' Actualizar el estado de los cargues que ya fueron procesados
                Dim folderImagingDataTable = dbmCore.SchemaImaging.TBL_Folder.DBGet(nidExpediente, nidFolder)

                Dim idCargue = folderImagingDataTable(0).fk_Cargue
                Dim idCarguePaquete = folderImagingDataTable(0).fk_Cargue_Paquete
                Try
                    dbmImaging.Transaction_Begin(IsolationLevel.ReadUncommitted)

                    Dim carguePaqueteFileDataTable = dbmImaging.SchemaProcess.CTA_Cargue_Paquete_Estado_File.DBFindByid_Cargueid_Cargue_Paquete(idCargue, idCarguePaquete)
                    If (carguePaqueteFileDataTable.Count > 0) Then
                        Dim updatePaquete = New DBImaging.SchemaProcess.TBL_Cargue_PaqueteType()
                        updatePaquete.fk_Estado = carguePaqueteFileDataTable(0).Estado_File
                        dbmImaging.SchemaProcess.TBL_Cargue_Paquete.DBUpdate(updatePaquete, idCargue, idCarguePaquete)

                        Dim cargueFileDataTable = dbmImaging.SchemaProcess.CTA_Cargue_Estado.DBFindByid_Cargue(idCargue)
                        If (cargueFileDataTable.Count > 0) Then
                            Dim updateCargue = New DBImaging.SchemaProcess.TBL_CargueType()
                            updateCargue.fk_Estado = cargueFileDataTable(0).Estado_File
                            dbmImaging.SchemaProcess.TBL_Cargue.DBUpdate(updateCargue, idCargue)
                        End If
                    End If

                    dbmImaging.Transaction_Commit()
                Catch ex As Exception
                    If (dbmImaging IsNot Nothing) Then dbmImaging.Transaction_Rollback()
                    Throw
                End Try

            Catch ex As Exception
                If (dbmCore IsNot Nothing) Then dbmCore.Transaction_Rollback()
                If (dbmImaging IsNot Nothing) Then dbmImaging.Transaction_Rollback()

                DesktopMessageBoxControl.DesktopMessageShow("PreCaptura", ex)

            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

        Public Sub FinalizarPrimeraCaptura(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short) Implements IEventExecuter.FinalizarPrimeraCaptura

        End Sub

        Public Sub FinalizarSegundaCaptura(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short) Implements IEventExecuter.FinalizarSegundaCaptura

        End Sub

        Public Sub FinalizarTerceraCaptura(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short) Implements IEventExecuter.FinalizarTerceraCaptura

        End Sub

        Public Sub FinalizarCalidad(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short) Implements IEventExecuter.FinalizarCalidad

        End Sub

        Public Sub FinalizarValidaciones(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short) Implements IEventExecuter.FinalizarValidaciones

        End Sub

        Public Sub FinalizarReclasificar(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short) Implements IEventExecuter.FinalizarReclasificar

        End Sub

        Public Sub EnviarReproceso(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short) Implements IEventExecuter.EnviarReproceso

        End Sub

        Public Sub FinalizarReproceso(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short) Implements IEventExecuter.FinalizarReproceso

        End Sub

        Public Sub AbrirFechaProceso(ByVal nidEntidadProcesamiento As Short, ByVal nidEntidad As Short, ByVal nidProyecto As Short, ByVal nidFechaProceso As Integer) Implements IEventExecuter.AbrirFechaProceso

        End Sub

        Public Sub AbrirOt(ByVal nidOt As Integer) Implements IEventExecuter.AbrirOt

        End Sub

        Public Sub ValidarCerrarFechaProcesoNoOT(nidEntidadProcesamiento As Short, nidEntidad As Short, nidProyecto As Short, nidFechaProceso As Integer, ByRef nValido As Boolean, ByRef nValido2 As Boolean, ByRef nMsgError As String) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.ValidarCerrarFechaProcesoNoOT

        End Sub

        Public Sub CerrarFechaProceso(ByVal nidEntidadProcesamiento As Short, ByVal nidEntidad As Short, ByVal nidProyecto As Short, ByVal nidFechaProceso As Integer) Implements IEventExecuter.CerrarFechaProceso

        End Sub

        Public Sub CerrarOt(ByVal nidOt As Integer) Implements IEventExecuter.CerrarOt

        End Sub

        Public Sub CrearFechaProceso(ByVal nidEntidadProcesamiento As Short, ByVal nidEntidad As Short, ByVal nidProyecto As Short, ByVal nidFechaProceso As Integer) Implements IEventExecuter.CrearFechaProceso

        End Sub

        Public Sub CrearOt(ByVal nidOt As Integer) Implements IEventExecuter.CrearOt

        End Sub

        Public Sub FinalizarRecorte(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short) Implements IEventExecuter.FinalizarRecorte

        End Sub

        Public Sub FinalizarPrecinto() Implements IEventExecuter.FinalizarPrecinto

        End Sub

        Public Sub ValidarEmpaque(ByVal nidOt As Integer, ByVal nidEmpaque As Short, ByVal nidEsquema As Short, ByVal nToken As String, ByRef nValido As Boolean, ByRef nMsgError As String) Implements IEventExecuter.ValidarEmpaque
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim dbmCoomeva As DBIntegration.DBIntegrationDataBaseManager = Nothing
            Dim Fecha_Proceso As Integer

            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(_plugin.Manager.DesktopGlobal.ConnectionStrings.Core)
                dbmImaging = New DBImaging.DBImagingDataBaseManager(_plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)
                dbmCoomeva = New DBIntegration.DBIntegrationDataBaseManager(_plugin.CoomevaConnectionString)

                dbmCore.Connection_Open(_plugin.Manager.Sesion.Usuario.id)
                dbmImaging.Connection_Open(_plugin.Manager.Sesion.Usuario.id)
                dbmCoomeva.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                ' Validar si ya se realizó el cierre
                Dim OTDataTable = dbmImaging.SchemaProcess.TBL_OT.DBFindByfk_Entidadfk_Proyectoid_OT(_plugin.Manager.ImagingGlobal.Entidad, _plugin.Manager.ImagingGlobal.Proyecto, nidOt)

                Fecha_Proceso = CInt(OTDataTable(0).fk_fecha_proceso)

                Dim Validos = dbmCore.SchemaProcess.PA_Existe_Generacion_Rechazos.DBExecute(Fecha_Proceso, _plugin.Manager.ImagingGlobal.Entidad, _plugin.Manager.ImagingGlobal.Proyecto)

                If (Validos = 0) Then Throw New Exception("No se puede realizar el empaque de tarjetas de credito si no se ha realizado el proceso de generar rechazos")

                Dim valida = dbmCoomeva.SchemaCoomeva.PA_Multiactiva_Empaque_Validar_Token.DBExecute(nidOt, nidEmpaque, nToken)

                If (CInt(valida(0)(0)) = 0) Then
                    Throw New Exception("La Cedula: " & nToken & " no coincide con el tipo de empaque, " & valida(0)(1))
                ElseIf (CInt(valida(0)(0)) = -1) Then
                    Throw New Exception("La Cedula: " & nToken & " no pertenece a la OT actual,")
                End If

            Catch ex As Exception
                nValido = False
                nMsgError = ex.Message
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

        Public Sub FinalizarProcesoAdicionalCaptura(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short) Implements IEventExecuter.FinalizarProcesoAdicionalCaptura

        End Sub

        Public Sub GuardarContenedor(ByVal nidOt As Integer, ByVal nidPrecinto As Integer, ByVal nidContenedor As Integer) Implements IEventExecuter.GuardarContenedor

        End Sub

        Public Sub ValidarPrecintoEmpaque(ByVal nidOt As Integer, ByVal nidEmpaque As Integer, ByVal ndbImaging As DBImaging.DBImagingDataBaseManager, ByVal nContenedorDesktop As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl, ByRef nValido As Boolean, ByRef nMsgError As String) Implements IEventExecuter.ValidarPrecintoEmpaque

        End Sub

        Public Sub FinalizarPrecintoEmpaque(ByVal nidOt As Integer, ByVal nidEmpaque As Integer) Implements IEventExecuter.FinalizarPrecintoEmpaque

        End Sub

        Public Sub CargarPrecinto(ByVal nidOt As Integer, ByVal nidPrecinto As Integer, ByVal nContenedorDesktop As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl, ByRef nValido As Boolean, ByRef nMsgError As String) Implements IEventExecuter.CargarPrecinto

        End Sub

        Public Sub FinalizarEliminarPrecinto(ByVal nidOt As Integer, ByVal nidEmpaque As Integer) Implements IEventExecuter.FinalizarEliminarPrecinto

        End Sub

        Public Sub ValidarSaveLabelCaptura(campos As System.Collections.Generic.List(Of Miharu.Imaging.Indexer.View.Indexacion.CampoCaptura), fk_documento As Integer, ByVal fk_Expediente As Long, ByVal fk_Folder As Short, ByVal fk_File As Short, ByVal TipoCaptura As String, ByRef Result As Boolean) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.ValidarSaveLabelCaptura

        End Sub

        Public Sub ValidarActualizarDatoBusqueda(ByVal nCampo As DBCore.SchemaImaging.CTA_Busqueda_Files_DataRow, ByVal fk_Expediente As Long, ByVal fk_Folder As Short, ByVal fk_File As Short, ByVal newValor_File_Data As Object, ByRef Result As Boolean) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.ValidarActualizarDatoBusqueda
            Dim dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = Nothing
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                If (nCampo.Nombre_Campo.ToUpper() = "EL CLIENTE ACEPTA CUPO?") Then
                    dbmIntegration = New DBIntegration.DBIntegrationDataBaseManager(_plugin.CoomevaConnectionString)
                    dbmCore = New DBCore.DBCoreDataBaseManager(_plugin.Manager.DesktopGlobal.ConnectionStrings.Core)
                    dbmImaging = New DBImaging.DBImagingDataBaseManager(Me._plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)


                    dbmIntegration.Connection_Open(_plugin.Manager.Sesion.Usuario.id)
                    dbmCore.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                    Dim resultValidacion = dbmIntegration.SchemaCoomeva.PA_CambiaPrimeraCaptura_ValidacionTriggrer.DBExecute(fk_Expediente, fk_Folder, fk_File, Me._plugin.Manager.ImagingGlobal.Entidad, Me._plugin.Manager.ImagingGlobal.Proyecto, Me._plugin.Manager.Sesion.Usuario.id)

                    If (resultValidacion.Rows.Count > 0) Then
                        If (resultValidacion.Rows(0)("MSJ_RETURN") = "CAMBIA ESTADO") Then
                            If (DesktopMessageBoxControl.DesktopMessageShow("Desea Cambiar la Respuesta a este campo?, Si su respuesta es 'Aceptar' este documento sera enviado a Primera Captura", "Editar Campo", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, False) = DialogResult.OK) Then
                                'Result = False
                            Else
                                'Devuelve a estado anterior
                                Dim estado_anterior = Convert.ToInt32(resultValidacion.Rows(0)("Estado_Anterior"))
                                Dim updateEstado As New DBCore.SchemaProcess.TBL_File_EstadoType()
                                updateEstado.fk_Estado = estado_anterior
                                dbmCore.SchemaProcess.TBL_File_Estado.DBUpdate(updateEstado, fk_Expediente, fk_Folder, fk_File, DesktopConfig.Modulo.Imaging)
                                'Result = True
                            End If
                        End If
                    End If
                End If
                
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Error Validación Busqueda", ex.Message, DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
            Finally
                If (dbmIntegration IsNot Nothing) Then
                    dbmIntegration.Connection_Close()
                End If

                If (dbmCore IsNot Nothing) Then
                    dbmCore.Connection_Close()
                End If
            End Try
        End Sub

        Public Sub FinalizarActualizarDatosBusqueda(ByVal nCampo As DBCore.SchemaImaging.CTA_Busqueda_Files_DataRow, ByVal fk_Expediente As Long, ByVal fk_Folder As Short, ByVal fk_File As Short) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarActualizarDatosBusqueda

        End Sub

        Public Sub Ejecutar_Cruce_En_Linea(ByVal nCampo As DBCore.SchemaImaging.CTA_Busqueda_Files_DataRow, ByVal fk_Expediente As Long, ByVal fk_Folder As Short, ByVal fk_File As Short) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.Ejecutar_Cruce_En_Linea

        End Sub

        Public Sub FinalizarLoadConfig(ByRef IndexerView As Miharu.Imaging.Indexer.View.Indexacion.IIndexerView, ByVal FileCoreRow As DBCore.SchemaProcess.TBL_FileRow) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarLoadConfig

        End Sub

        Public Sub FinalizarContenedorEmpaque(ByVal nidOt As Integer, ByVal nidEmpaque As Integer, ByVal IdEmpaqueContenedor As Integer) Implements IEventExecuter.FinalizarContenedorEmpaque

        End Sub

        Public Sub FinalizarContenedorEmpaqueEliminar(ByVal nidOt As Integer, ByVal ntoken As String) Implements IEventExecuter.FinalizarContenedorEmpaqueEliminar

        End Sub

        Public Sub Reprocesar(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short) Implements IEventExecuter.Reprocesar
        End Sub

        Public Sub Validar_Reprocesar(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidDocumento As Integer, ByVal nidCampo As Integer, ByVal nidCampoTablaAsociada As Integer, ByVal nesCampo As Boolean, ByRef nValido As Boolean, ByRef nMsgError As String) Implements IEventExecuter.Validar_Reprocesar

        End Sub

        Public Function Nombre_Imagen_Exportar(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nFk_Documento As Integer, ByVal nGrupo As Integer, ByRef nValido As Boolean, ByRef nMsgError As String) As String Implements IEventExecuter.Nombre_Imagen_Exportar
            Return String.Empty
        End Function

        Public Function ExtensionImagen_Plugin(ByVal Entrada As Boolean) As String Implements IEventExecuter.ExtensionImagen_Plugin
            Return String.Empty
        End Function

        Public Function IdFormatoImagen_Plugin(ByVal Entrada As Boolean) As Short Implements IEventExecuter.IdFormatoImagen_Plugin
            Return -1
        End Function

        Public Sub FinalizarCrucegenerico(nidEntidad As Short, nidProyecto As Short, nidFechaProceso As Integer, nidOT As Integer) Implements IEventExecuter.FinalizarCruceGenerico

        End Sub

        Function Nombre_Imagen_Agrupada_Exportar(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nGrupo As Integer, ByRef nValido As Boolean, ByRef nMsgError As String) As String Implements IEventExecuter.Nombre_Imagen_Agrupada_Exportar
            Return String.Empty
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