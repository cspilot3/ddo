Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library
Imports System.Threading
Imports System.Windows.Forms
Imports Miharu.FileProvider.Library

Namespace Firmas.Controller.Indexer.Capturas.Reproceso

    Public Class ReProcessDemandaController_Firmas
        Inherits ReProcessController_Firmas

#Region " Declaraciones "

        Private _sede As Slyg.Tools.SlygNullable(Of Short)
        Private _centroProcesamiento As Slyg.Tools.SlygNullable(Of Short)
        Private _motivoReproceso As Short

#End Region

#Region " Implementación IIndexerController "

        Public Overrides Function NextIndexingElement(ByVal ot As Integer, nEstado As DBCore.EstadoEnum, nIdDocumento As Slyg.Tools.SlygNullable(Of Integer)) As Boolean
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
                Dim bloqueoDatatable = dbmImaging.SchemaProcess.PA_Dashboard_Capturas_get.DBExecute(Me.IndexerDesktopGlobal.SesionID)
                If bloqueoDatatable.Count > 0 Then
                    For Each BloqueoRow In bloqueoDatatable
                        dbmImaging.SchemaProcess.PA_Dashboard_Desbloquear_Capturas.DBExecute(BloqueoRow.fk_Expediente, BloqueoRow.fk_Folder, BloqueoRow.fk_File)
                    Next
                End If

                ' Calcular siguiente imagen disponible
                Dim bloqueados As Boolean = dbmImaging.SchemaProcess.PA_Bloqueo_Reproceso_Next.DBExecute(ot,
                                                                                                         Me.IndexerDesktopGlobal.CentroProcesamientoRow.fk_Entidad,
                                                                                                         Me._sede,
                                                                                                         Me._centroProcesamiento,
                                                                                                         Me._motivoReproceso,
                                                                                                         Me.IndexerSesion.Usuario.id,
                                                                                                         Me.IndexerDesktopGlobal.SesionID,
                                                                                                         Me.IndexerDesktopGlobal.PCName)
                If (Not bloqueados) Then Return False

                bloqueoDatatable = dbmImaging.SchemaProcess.PA_Dashboard_Capturas_get.DBExecute(Me.IndexerDesktopGlobal.SesionID)
                If bloqueoDatatable.Count > 0 Then
                    Dim fileImagingDataTable = dbmCore.SchemaImaging.TBL_File.DBGet(bloqueoDatatable(0).fk_Expediente, bloqueoDatatable(0).fk_Folder, bloqueoDatatable(0).fk_File, Nothing)

                    If (fileImagingDataTable.Count = 0) Then
                        Throw New Exception("No se encontró data asociada al Ex: " & bloqueoDatatable(0).fk_Expediente & " Fo: " & bloqueoDatatable(0).fk_Folder & " Fi: " & bloqueoDatatable(0).fk_File & " del Dashboard")
                    End If
                    FileImagingRow = fileImagingDataTable(fileImagingDataTable.Count - 1)

                    Dim fileCoreDataTable = dbmCore.SchemaProcess.TBL_File.DBGet(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File)
                    FileCoreRow = fileCoreDataTable(0)

                    Dim expedienteDataTable = dbmCore.SchemaProcess.TBL_Expediente.DBGet(FileImagingRow.fk_Expediente)
                    ExpedienteRow = expedienteDataTable(0)

                    Dim folderImagingDataTable = dbmCore.SchemaImaging.TBL_Folder.DBGet(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder)

                    _idCargue = folderImagingDataTable(0).fk_Cargue
                    _idCarguePaquete = folderImagingDataTable(0).fk_Cargue_Paquete


                    ' Leer el listado de imagenes
                    manager = New FileProviderManager(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, dbmImaging, Me.IndexerSesion.Usuario.id)
                    manager.Connect()

                    If FileImagingRow.Es_Anexo Then
                        _ImageCount = manager.GetFolios(FileImagingRow.fk_Anexo)
                    Else
                        _ImageCount = manager.GetFolios(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File, FileImagingRow.id_Version)
                    End If

                    View.SetTitle(Me.ProcessName & " [Ex: " & FileCoreRow.fk_Expediente & " - Fo: " & FileCoreRow.fk_Folder & " - Fi: " & FileCoreRow.id_File & "]")

                    IndexerView.Information = ""
                    Dim llavesDataTable = dbmCore.SchemaProcess.CTA_Expediente_LLave.DBFindByid_Expediente(FileCoreRow.fk_Expediente)
                    For Each Llave In llavesDataTable
                        IndexerView.Information &= "[" & Llave.Nombre_Proyecto_Llave & "]: " & Llave.Valor_Llave.ToString() & vbCrLf
                    Next

                    AnexosDataTable = dbmCore.SchemaImaging.CTA_Documentos.DBFindByfk_Expedientefk_Folder(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder)

                    LoadConfig(dbmCore, dbmImaging, ExpedienteRow.fk_Entidad, ExpedienteRow.fk_Proyecto, ExpedienteRow.fk_Esquema)

                    ' Definir si se puede cambiar el esquema
                    Dim totalFiles = dbmCore.SchemaImaging.PA_Get_Files_No_Anexo.DBExecute(ExpedienteRow.id_Expediente)

                    Me.IndexerView.Esquema_Enabled = (totalFiles = 1)
                Else
                    Return False
                End If
            Catch ex As Exception
                Throw

            Finally
                If (manager IsNot Nothing) Then manager.Disconnect()
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            End Try

            ' Liberar la memoria
            Me.Clear()


            Application.DoEvents()

            _CurrentFolderIndex = 0
            _CurrentDocumentFileIndex = 0
            _CurrentFolioIndex = 0
            _CurrentImageIndex = 0

            If (_ImageCount = 0) Then
                Dim mensaje = "No se encontraron folios asociados a al File, por favor comuniquese con el administrador del sistema." & vbCrLf
                mensaje &= "[Expediente]: " & FileCoreRow.fk_Expediente & vbCrLf
                mensaje &= "[Folder]: " & FileCoreRow.fk_Folder & vbCrLf
                mensaje &= "[File]: " & FileCoreRow.id_File & vbCrLf

                Throw New Exception(mensaje)
            End If

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
            DesktopTrace.Trace(traceMessage, DesktopTrace.CategoryEnum.Performance, 1, 0, TraceEventType.Information, "[" & Me.ProcessName & "][NextIndexingElement]")
            '-------------------------------------------------------------------------------------------------------------

            Return True
        End Function

#End Region

#Region " Metodos "

        Public Overloads Sub SetData(nSede As Slyg.Tools.SlygNullable(Of Short), nCentroProcesamiento As Slyg.Tools.SlygNullable(Of Short), nMotivoReproceso As Short)
            Me._sede = nSede
            Me._centroProcesamiento = nCentroProcesamiento
            Me._motivoReproceso = nMotivoReproceso
        End Sub

#End Region

    End Class

End Namespace