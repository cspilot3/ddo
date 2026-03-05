Imports Miharu.FileProvider.Library
Imports Slyg.Tools.Imaging
Imports System.IO
Imports Microsoft.Reporting.WinForms
Imports BCS.Plugin.Imaging.Carpeta_Unica
Imports System.Threading
Public Class GenerarImagenTapa
#Region " Declaraciones "
    Private nTempPath As String
    Private nTipoProcesoId As Integer
    Private nNumeroHilos As Integer
    Private dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
    Private dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = Nothing
    Private _Plugin As CarpetaUnicaPlugin
    Private nFecha_Proceso As String
    Private BloqueoConcurrencia As New Object
    Public MensajeError As String = ""
#End Region
#Region " Contructores "
    Public Sub New(_dbmImaging As DBImaging.DBImagingDataBaseManager,
                   _dbmIntegration As DBIntegration.DBIntegrationDataBaseManager,
                   _nCarpetaUnicaDesktopPlugin As CarpetaUnicaPlugin,
                   _nFecha_Proceso As String,
                   _nTipoProcesoId As Integer)
        Me.nTempPath = Program.AppPath & Program.TempPath
        Me.dbmImaging = _dbmImaging
        Me.dbmIntegration = _dbmIntegration
        Me._Plugin = _nCarpetaUnicaDesktopPlugin
        Me.nFecha_Proceso = _nFecha_Proceso
        Me.nTipoProcesoId = _nTipoProcesoId
        Dim NumHilos = Me.dbmIntegration.SchemaConfig.TBL_Parametro_Sistema.DBFindByfk_Entidadfk_ProyectoNombre_Parametro_Sistema(_Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, "Numero_Hilos_Imagen_Tapa")
        If (NumHilos.Rows.Count > 0) Then
            nNumeroHilos = CInt(NumHilos.Rows(0)("Valor_Parametro_Sistema").ToString())
        Else
            nNumeroHilos = 1
        End If
    End Sub
#End Region
#Region " Funciones "
    Private Function GenerarImagen(ByVal NombreArchivo As String, ByVal nAnexo As Long) As String
        Dim ErrorMensaje As String = ""
        Try
            Dim Datos = dbmIntegration.SchemaBCSCarpetaUnica.PA_Reporte_Imagenes_Tapa.DBExecute(nAnexo, Me.nFecha_Proceso, _Plugin.Manager.Sesion.Usuario.id, nTipoProcesoId)
            If Datos.Rows.Count > 0 Then
                ErrorMensaje = Datos.Rows(0)("Error")
                If ErrorMensaje.Equals("") Then
                    Dim viewer As ReportViewer = New ReportViewer()
                    Dim rds As ReportDataSource = New ReportDataSource("DataSet1", Datos)
                    Dim warnings As Warning() = Nothing
                    Dim streamIds As String() = Nothing
                    Dim mimeType As String = String.Empty
                    Dim encoding As String = String.Empty
                    Dim extension As String = String.Empty
                    Dim filetype As String = String.Empty
                    viewer.Reset()
                    viewer.LocalReport.ReportEmbeddedResource = "BCS.Plugin.ReporteGeneraImagen.rdlc"
                    viewer.LocalReport.DataSources.Clear()
                    viewer.LocalReport.DataSources.Add(rds)
                    Dim bytes As Byte() = viewer.LocalReport.Render("Image", Nothing, mimeType, encoding, extension, streamIds, warnings)
                    Dim fs As FileStream = New FileStream(NombreArchivo, FileMode.OpenOrCreate)
                    fs.Write(bytes, 0, bytes.Length)
                    fs.Close()
                End If
            End If
        Catch ex As Exception
            ErrorMensaje = ErrorMensaje & ex.Message
        End Try
        Return ErrorMensaje
    End Function
    Public Function IndexarFolio() As String
        Dim Mensaje As String = ""
        Dim MensajeError As String = ""
        Dim FileName As String = ""
        Dim manager As FileProviderManager = Nothing
        Dim idFolio As Short = 0
        Dim idAnexo As Long = 0
        Try
            Dim resultadoDataTable = Me.dbmIntegration.SchemaBCSCarpetaUnica.PA_Get_Anexo_Tapa.DBExecute(nFecha_Proceso, _Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, _Plugin.Manager.Sesion.Usuario.id)
            If resultadoDataTable.Rows.Count > 0 Then
                For Each ItemCrear As DataRow In resultadoDataTable.Rows
                    Dim imagen() As Byte = Nothing
                    Dim thumbnail() As Byte = Nothing
                    FileName = nTempPath & Guid.NewGuid().ToString() & _Plugin.Manager.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida
                    idAnexo = CLng(ItemCrear.Item("fk_Anexo"))
                    idFolio = CLng(ItemCrear.Item("id_Anexo_Record_Folio"))
                    manager = New FileProviderManager(idAnexo, dbmImaging, _Plugin.Manager.Sesion.Usuario.id)
                    manager.Connect()
                    Mensaje = GenerarImagen(FileName, idAnexo)
                    If Mensaje.Equals("") Then
                        Dim dataImage = ImageManager.GetData(FileName)
                        Dim dataImageThumbnail = ImageManager.GetThumbnailData(FileName, 1, 1, 60, 60)
                        manager.CreateItem(idAnexo, _Plugin.Manager.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida)
                        manager.CreateFolio(idAnexo, idFolio + 1, dataImage, dataImageThumbnail(0), True)
                    End If
                    If File.Exists(FileName) Then
                        My.Computer.FileSystem.DeleteFile(FileName)
                    End If
                    manager.Disconnect()
                    MensajeError = MensajeError & Mensaje
                Next
            End If
        Catch ex As Exception
            Dim estadoLogReporte As New DBIntegration.SchemaBCSCarpetaUnica.TBL_Log_Reporte_AnexoType()
            estadoLogReporte.fk_Anexo = idFolio
            estadoLogReporte.fk_Fecha_Proceso = Me.nFecha_Proceso
            estadoLogReporte.fk_Usuario = _Plugin.Manager.Sesion.Usuario.id
            estadoLogReporte.generado = 0
            dbmIntegration.SchemaBCSCarpetaUnica.TBL_Log_Reporte_Anexo.DBUpdate(estadoLogReporte, 0)
            MensajeError = ex.Message
        Finally
            If (manager IsNot Nothing) Then manager.Disconnect()
        End Try
        Return MensajeError
    End Function
    Public Function IndexarFolioHilos() As String
        Dim Mensaje As String = ""
        Dim FileName As String = ""
        Dim manager As FileProviderManager = Nothing
        Dim idFolio As Short = 0
        Dim idAnexo As Long = 0
        Try
            Dim resultadoDataTable = Me.dbmIntegration.SchemaBCSCarpetaUnica.PA_Get_Anexo_Tapa.DBExecute(nFecha_Proceso, _Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, _Plugin.Manager.Sesion.Usuario.id)
            If resultadoDataTable.Rows.Count > 0 Then
                For Each ItemCrear As DataRow In resultadoDataTable.Rows
                    Dim imagen() As Byte = Nothing
                    Dim thumbnail() As Byte = Nothing
                    Dim procesador As New ProcesadorHilosImagen
                    FileName = nTempPath & Guid.NewGuid().ToString() & _Plugin.Manager.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida
                    idAnexo = CLng(ItemCrear.Item("fk_Anexo"))
                    idFolio = CLng(ItemCrear.Item("id_Anexo_Record_Folio"))
                    procesador.generarImagen = Me
                    Dim ArrayParameters As ArrayList = New ArrayList
                    ArrayParameters.Add(manager)
                    ArrayParameters.Add(FileName)
                    ArrayParameters.Add(idAnexo)
                    ArrayParameters.Add(idFolio)
                    procesador.AgregarHilo(ArrayParameters, nNumeroHilos)
                    While (procesador.TerminoHilos = False)
                        System.Threading.Thread.Sleep(100)
                    End While
                Next
            End If
        Catch ex As Exception
            MensajeError = MensajeError & ex.Message
        Finally
            If (manager IsNot Nothing) Then manager.Disconnect()
        End Try
        Return MensajeError
    End Function
    Public Function GenerarReporte() As String
        Dim MensajeError As String = ""
        Dim Datos = dbmIntegration.SchemaBCSCarpetaUnica.PA_Reporte_Anexos_Tapa.DBExecute(Me.nFecha_Proceso,
                                                                                          _Plugin.Manager.ImagingGlobal.Entidad,
                                                                                          _Plugin.Manager.ImagingGlobal.Proyecto,
                                                                                          _Plugin.Manager.Sesion.Usuario.id,
                                                                                           nTipoProcesoId)
        If Datos.Rows.Count > 0 Then
            MensajeError = Datos.Rows(0)("Error")
        End If
        Return MensajeError
    End Function
#End Region
#Region " Metodos "
    Public Sub ProcesoHilos(ByVal objectArray As Object)
        Dim ArraListParameters As ArrayList = objectArray
        Dim manager As FileProviderManager = ArraListParameters(0)
        Dim FileName As String = ArraListParameters(1)
        Dim idAnexo As Long = CLng(ArraListParameters(2))
        Dim idFolio As Long = CLng(ArraListParameters(3))
        Dim Mensaje As String = ""
        Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
        Try
            dbmCore = New DBCore.DBCoreDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Core)
            dbmCore.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
            Mensaje = GenerarImagen(FileName, idAnexo)
            If Mensaje.Equals("") Then
                Dim dataImage = ImageManager.GetData(FileName)
                Dim dataImageThumbnail = ImageManager.GetThumbnailData(FileName, 1, 1, 60, 60)
                Dim estadoAnexo As New DBCore.SchemaImaging.TBL_AnexoType
                manager = New FileProviderManager(idAnexo, dbmImaging, _Plugin.Manager.Sesion.Usuario.id)
                manager.Connect()
                manager.CreateItem(idAnexo, _Plugin.Manager.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida)
                manager.CreateFolio(idAnexo, idFolio + 1, dataImage, dataImageThumbnail(0), True)
                estadoAnexo.Folios_Documento_File = idFolio + 1
                dbmCore.SchemaImaging.TBL_Anexo.DBUpdate(estadoAnexo, idAnexo)
            Else
                MensajeError = MensajeError & Mensaje
            End If
            If File.Exists(FileName) Then
                My.Computer.FileSystem.DeleteFile(FileName)
            End If
            manager.Disconnect()
        Catch ex As Exception
            manager.Disconnect()
            dbmCore.Connection_Close()
            Dim estadoLogReporte As New DBIntegration.SchemaBCSCarpetaUnica.TBL_Log_Reporte_AnexoType()
            estadoLogReporte.fk_Anexo = idAnexo
            estadoLogReporte.fk_Fecha_Proceso = Me.nFecha_Proceso
            estadoLogReporte.fk_Usuario = _Plugin.Manager.Sesion.Usuario.id
            estadoLogReporte.generado = 0
            dbmIntegration.SchemaBCSCarpetaUnica.TBL_Log_Reporte_Anexo.DBUpdate(estadoLogReporte, 0)
            MensajeError = MensajeError & ex.Message
        End Try
    End Sub
#End Region
End Class
#Region " Implementación de hilos "
Public Class ProcesadorHilosImagen
    Dim NumHilos As Integer
    Dim ListaHilos As New List(Of Thread)
    Public generarImagen As GenerarImagenTapa
    Public Shared objLock = New Object()
    Public Sub AgregarHilo(ByVal ArrayParameters As ArrayList, ByVal nNumeroHilos As Integer)
        Me.NumHilos = nNumeroHilos
        If TieneHiloslibres() = False Then
            Do
                Thread.Sleep(1000)
                If TieneHiloslibres() Then
                    Exit Do
                End If
            Loop
        End If

        Dim Threads As New System.Threading.Thread(AddressOf generarImagen.ProcesoHilos)
        Threads.Start(ArrayParameters)

        SyncLock objLock
            ListaHilos.Add(Threads)
        End SyncLock
    End Sub
    Public Function TieneHiloslibres() As Boolean
        SyncLock objLock
            Dim ListaHilosBorrar As New List(Of Thread)
            For Each hilo In ListaHilos
                If hilo.ThreadState = ThreadState.Stopped Then
                    ListaHilosBorrar.Add(hilo)
                End If
            Next
            For Each hilo In ListaHilosBorrar
                ListaHilos.Remove(hilo)
            Next
        End SyncLock
        If ListaHilos.Count < NumHilos Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function TerminoHilos() As Boolean
        SyncLock objLock
            For Each hilo In ListaHilos
                If hilo.ThreadState <> ThreadState.Stopped Then
                    Return False
                End If
            Next
        End SyncLock

        Return True
    End Function
End Class
#End Region