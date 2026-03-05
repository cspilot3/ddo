Imports System
Imports System.IO
Imports System.Threading
Imports DBCore.SchemaImaging
Imports Slyg.Tools
Imports Miharu.FileProvider.Library
Imports Miharu.Security.Library.WebService

Namespace Servicio

    Public Class ImagingFileTransferService

#Region " Declaraciones "

        Private _detener As Boolean
        Private newThread As Thread

#End Region

#Region " Metodos reemplazados "

        Protected Overrides Sub OnStart(ByVal args() As String)
            IniciarServicio()
        End Sub

        Protected Overrides Sub OnStop()
            DetenerServicio()
        End Sub

#End Region

#Region " Metodos "

        Private Sub LoadConfig()
            ' Leer la configuración
            If (File.Exists(Program.AppDataPath + ImagingFileTransferConfig.ConfigFileName)) Then
                Program.Config = ImagingFileTransferConfig.Deserialize(Program.AppDataPath)
            End If
        End Sub

        Public Sub IniciarServicio()
            Try
                Dim webService As SecurityWebService

                LoadConfig()

                webService = New SecurityWebService(Program.Config.SecurityWebServiceURL, "")

#If Not Debug Then
                ' Validar que la versión corresponda
                Dim versionApp As String = webService.getAssemblyVersion(Program.AssemblyName)

                If Not versionApp = Program.AssemblyVersion Then
                    WriteErrorLog("La versión del aplicativo no corresponde a la registrada en la base de datos," & vbCrLf & vbCrLf & _
                                    "Versión registrada: [" & versionApp & "]" & vbCrLf & _
                                    "Versión ejecutable: [" & Program.AssemblyVersion & "]")

                    Me.Stop()

                    Return
                End If
#End If

                webService.CrearCanalSeguro()
                webService.setUser(Program.Config.User, ImagingFileTransferConfig.Decrypt(Program.Config.Password))
                Program.ConnectionStrings = ImagingFileTransferConfig.getCadenasConexion(webService)

                If Program.ConnectionStrings.Core = "" Then
                    WriteErrorLog("No se pudo obtener la cadena de conexión a la base de datos Core")
                    Me.Stop()

                    Return
                End If

                If Program.ConnectionStrings.Security = "" Then
                    WriteErrorLog("No se pudo obtener la cadena de conexión a la base de datos Security")
                    Me.Stop()

                    Return
                End If

                'Dim newThread As New Thread(AddressOf Proceso)
                newThread = New Thread(AddressOf Proceso)

                _detener = False
                newThread.Start()

            Catch ex As Exception
                EventLog.WriteEntry(ex.Message, EventLogEntryType.Error)
                Me.Stop()
            End Try
        End Sub

        Public Sub DetenerServicio()
            _detener = True
            Try
                newThread.Abort()
            Catch ex As Exception

            End Try

            Me.Stop()
        End Sub

        Private Sub Proceso()
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim dbmSecurity As DBSecurity.DBSecurityDataBaseManager = Nothing
            Dim managerOrigen As FileProviderManager = Nothing


            While True

                Try
                    dbmCore = New DBCore.DBCoreDataBaseManager(Program.ConnectionStrings.Core)
                    dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.ConnectionStrings.Imaging)
                    dbmSecurity = New DBSecurity.DBSecurityDataBaseManager(Program.ConnectionStrings.Security)

                    ' Cargar la configuración del servidor                
                    dbmImaging.Connection_Open(2) ' Service
                    Dim servidorDataTable = dbmImaging.SchemaCore.CTA_Servidor.DBFindByIPName_Servidor(Environment.MachineName)
                    dbmImaging.Connection_Close()

                    Dim esServidor = (servidorDataTable.Rows.Count > 0)

                    If Not esServidor Then
                        Throw New Exception("No se encontró parametrización para el servicio")
                    End If

                    Dim servidorOrigenRow = servidorDataTable(0)
                    managerOrigen = New FileProviderManager(servidorOrigenRow.ToCTA_ServidorSimpleType(), dbmImaging, 2)

                    ' Proceso
                    While Not _detener
                        ' Validar si se detuvo el servicio
                        If (_detener) Then Return

                        Try
                            dbmSecurity.Connection_Open(2) ' Service

                            ' Preguntar si es una hora habil
                            If dbmSecurity.SchemaConfig.PA_Es_Hora_Habil.DBExecute(servidorOrigenRow.fk_Entidad, servidorOrigenRow.fk_Calendario) Then
                                Dim salir As Boolean = False

                                dbmCore.Connection_Open(2) ' Service
                                dbmImaging.Connection_Open(2) ' Service
                                managerOrigen.Connect() ' Service

                                ' Leer los servidores a donde transferir
                                Dim servidorTransferenciaDataTable = dbmCore.SchemaImaging.CTA_Transferencias_Servidor_File.DBFindByfk_Entidad_Servidorfk_Servidor(servidorOrigenRow.fk_Entidad, servidorOrigenRow.id_Servidor)

                                Dim fk_Expediente As Long = 0
                                Dim fk_Folder As Short = 0
                                ' Recorrer los servidores
                                For Each ServidorDestinoRow In servidorTransferenciaDataTable
                                    Dim managerDestino As FileProviderManager = Nothing

                                    Try
                                        Dim servidorDestino = dbmImaging.SchemaCore.CTA_Servidor.DBFindByfk_Entidadid_Servidor(ServidorDestinoRow.fk_Entidad_Servidor_Transferencia, ServidorDestinoRow.fk_Servidor_Transferencia)

                                        managerDestino = New FileProviderManager(servidorDestino(0).ToCTA_ServidorSimpleType(), dbmImaging, 2)
                                        managerDestino.Connect()

                                        ' Obtener los Files a transferir
                                        Dim filesDataTable = dbmCore.SchemaImaging.TBL_File.DBFindByfk_Entidad_Servidorfk_ServidorEn_Transferenciafk_Entidad_Servidor_Transferenciafk_Servidor_Transferencia(servidorOrigenRow.fk_Entidad, servidorOrigenRow.id_Servidor, True, ServidorDestinoRow.fk_Entidad_Servidor_Transferencia, ServidorDestinoRow.fk_Servidor_Transferencia)

                                        ' Solo se realiza el proceso si los servidores son distintos
                                        If (servidorOrigenRow.fk_Entidad <> ServidorDestinoRow.fk_Entidad_Servidor_Transferencia Or servidorOrigenRow.id_Servidor <> ServidorDestinoRow.fk_Servidor_Transferencia) Then
                                            ' Mover la imagenes
                                            For Each FileRow In filesDataTable
                                                Try
                                                    If Not FileRow.Es_Anexo Then
                                                        ' Crear el file en el destino
                                                        managerDestino.CreateItem(FileRow.fk_Expediente, FileRow.fk_Folder, FileRow.fk_File, FileRow.id_Version, FileRow.fk_Content_Type, FileRow.File_Unique_Identifier)

                                                        ' Obtener los Folios a transferir                            
                                                        Dim folios = managerOrigen.GetFolios(FileRow.fk_Expediente, FileRow.fk_Folder, FileRow.fk_File, FileRow.id_Version)
                                                        Dim Movido As Integer = 0
                                                        ' Mover la imagenes
                                                        For folio = 1 To folios
                                                            If (managerOrigen.ExistFolio(FileRow.fk_Expediente, FileRow.fk_Folder, FileRow.fk_File, FileRow.id_Version, CShort(folio))) Then
                                                                'If (Not managerDestino.ExistFolio(FileRow.fk_Expediente, FileRow.fk_Folder, FileRow.fk_File, FileRow.id_Version, CShort(folio))) Then
                                                                ' Crear el Folio en el destino
                                                                Dim imagen As Byte() = Nothing
                                                                Dim thumbnail As Byte() = Nothing

                                                                managerOrigen.GetFolio(FileRow.fk_Expediente, FileRow.fk_Folder, FileRow.fk_File, FileRow.id_Version, CShort(folio), imagen, thumbnail)
                                                                managerDestino.CreateFolio(FileRow.fk_Expediente, FileRow.fk_Folder, FileRow.fk_File, FileRow.id_Version, CShort(folio), imagen, thumbnail, False)
                                                                Movido = Movido + 1
                                                                'End If
                                                            End If
                                                        Next

                                                        ' Borrar File/ actualizar tbl_File
                                                        If Movido = folios Then
                                                            Dim fileType As New TBL_FileType
                                                            fileType.fk_Entidad_Servidor = ServidorDestinoRow.fk_Entidad_Servidor_Transferencia
                                                            fileType.fk_Servidor = ServidorDestinoRow.fk_Servidor_Transferencia
                                                            fileType.fk_Entidad_Servidor_Transferencia = DBNull.Value
                                                            fileType.fk_Servidor_Transferencia = DBNull.Value
                                                            fileType.En_Transferencia = False
                                                            fileType.Fecha_Transferencia = SlygNullable.SysDate
                                                            dbmCore.SchemaImaging.TBL_File.DBUpdate(fileType, FileRow.fk_Expediente, FileRow.fk_Folder, FileRow.fk_File, FileRow.id_Version)
                                                            managerOrigen.DeleteItem(FileRow.fk_Expediente, FileRow.fk_Folder, FileRow.fk_File, FileRow.id_Version)
                                                        End If
                                                    Else 'Anexos
                                                        ' Crear el file en el destino
                                                        managerDestino.CreateItem(FileRow.fk_Anexo, FileRow.fk_Content_Type)

                                                        ' Obtener los Folios a transferir                            
                                                        Dim folios = managerOrigen.GetFolios(FileRow.fk_Anexo)
                                                        Dim Movido As Integer = 0
                                                        ' Mover la imagenes
                                                        For folio = 1 To folios
                                                            If (managerOrigen.ExistFolio(FileRow.fk_Anexo, CShort(folio))) Then
                                                                If (Not managerDestino.ExistFolio(FileRow.fk_Anexo, CShort(folio))) Then
                                                                    ' Crear el Folio en el destino
                                                                    Dim imagen As Byte() = Nothing
                                                                    Dim thumbnail As Byte() = Nothing

                                                                    managerOrigen.GetFolio(FileRow.fk_Anexo, CShort(folio), imagen, thumbnail)
                                                                    managerDestino.CreateFolio(FileRow.fk_Anexo, CShort(folio), imagen, thumbnail, False)
                                                                    Movido = Movido + 1
                                                                End If
                                                            End If
                                                        Next

                                                        ' Borrar Anexo/Actualizar Anexo
                                                        If Movido = folios Then
                                                            Dim anexoType As New TBL_AnexoType
                                                            anexoType.fk_Entidad_Servidor = ServidorDestinoRow.fk_Entidad_Servidor_Transferencia
                                                            anexoType.fk_Servidor = ServidorDestinoRow.fk_Servidor_Transferencia
                                                            anexoType.fk_Entidad_Servidor_Transferencia = DBNull.Value
                                                            anexoType.fk_Servidor_Transferencia = DBNull.Value
                                                            anexoType.En_Transferencia = False
                                                            anexoType.Fecha_Transferencia = SlygNullable.SysDate
                                                            dbmCore.SchemaImaging.TBL_Anexo.DBUpdate(anexoType, FileRow.fk_Anexo)
                                                            managerOrigen.DeleteItem(FileRow.fk_Anexo)
                                                        End If
                                                    End If

                                                    If fk_Expediente <> FileRow.fk_Expediente Or fk_Folder <> FileRow.fk_Folder Then
                                                        ' Actualizar data
                                                        Dim folderType = New TBL_FolderType()
                                                        folderType.fk_Entidad_Servidor = ServidorDestinoRow.fk_Entidad_Servidor_Transferencia
                                                        folderType.fk_Servidor = ServidorDestinoRow.fk_Servidor_Transferencia
                                                        folderType.fk_Entidad_Servidor_Transferencia = DBNull.Value
                                                        folderType.fk_Servidor_Transferencia = DBNull.Value
                                                        folderType.En_Transferencia = False
                                                        folderType.Fecha_Transferencia = SlygNullable.SysDate

                                                        dbmCore.SchemaImaging.TBL_Folder.DBUpdate(folderType, FileRow.fk_Expediente, FileRow.fk_Folder)

                                                        fk_Expediente = FileRow.fk_Expediente
                                                        fk_Folder = FileRow.fk_Folder
                                                    End If
                                                Catch ex As Exception
                                                    'Throw
                                                    WriteErrorLog(ex)
                                                End Try
                                            Next
                                        End If

                                        '' Actualizar data
                                        'Dim folderType = New TBL_FolderType()
                                        'folderType.fk_Entidad_Servidor = ServidorDestinoRow.fk_Entidad_Servidor_Transferencia
                                        'folderType.fk_Servidor = ServidorDestinoRow.fk_Servidor_Transferencia
                                        'folderType.fk_Entidad_Servidor_Transferencia = DBNull.Value
                                        'folderType.fk_Servidor_Transferencia = DBNull.Value
                                        'folderType.En_Transferencia = False
                                        'folderType.Fecha_Transferencia = SlygNullable.SysDate

                                        'dbmCore.SchemaImaging.TBL_Folder.DBUpdate(folderType, fk_Expediente, fk_Folder)

                                        Windows.Forms.Application.DoEvents()
                                    Catch ex As Exception
                                        'Throw
                                        WriteErrorLog(ex)
                                    Finally
                                        If (managerDestino IsNot Nothing) Then managerDestino.Disconnect()
                                    End Try

                                    Windows.Forms.Application.DoEvents()

                                    If salir Then Exit For
                                Next
                            End If
                        Catch ex As Exception
                            'Throw
                            WriteErrorLog(ex)
                        Finally
                            If (managerOrigen IsNot Nothing) Then managerOrigen.Disconnect()
                            If (dbmSecurity IsNot Nothing) Then dbmSecurity.Connection_Close()
                            If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                            If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                        End Try

                        If _detener Then Return

                        Thread.Sleep(Program.Config.Intervalo) ' Esperar n segundos antes de continuar
                    End While

                Catch ex As Exception
                    WriteErrorLog(ex)
                Finally
                    If (managerOrigen IsNot Nothing) Then managerOrigen.Disconnect()
                    If (dbmSecurity IsNot Nothing) Then dbmSecurity.Connection_Close()
                    If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                    If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                End Try
                Thread.Sleep(Program.Config.Intervalo) ' Esperar n segundos antes de continuar
            End While
            'Me.Stop()
        End Sub

        Private Sub WriteErrorLog(ByVal nMessage As String)
            Try
                Dim sw As New StreamWriter(Program.AppDataPath & "log-" & Date.Now.ToString("yyyyMMdd") & ".txt", True)

                sw.WriteLine("--------------------------------------------------------------")
                sw.WriteLine(Now.ToString("yyyy-MM-dd HH:mm:ss"))
                sw.WriteLine("Mensaje: " & nMessage)
                sw.WriteLine("--------------------------------------------------------------")
                sw.WriteLine("")

                sw.Flush()
                sw.Close()
            Catch ex As Exception
                Try : EventLog.WriteEntry(ex.Message, EventLogEntryType.Error) : Catch : End Try
            End Try

            Windows.Forms.Application.DoEvents()
        End Sub

        Private Sub WriteErrorLog(ByVal nEx As Exception)
            Try
                Dim sw As New StreamWriter(Program.AppDataPath & "log-" & Date.Now.ToString("yyyyMMdd") & ".txt", True)

                sw.WriteLine("--------------------------------------------------------------")
                sw.WriteLine(Now.ToString("yyyy-MM-dd HH:mm:ss"))
                sw.WriteLine("Mensaje: " & nEx.Message)
                sw.WriteLine("--------------------------------------------------------------")
                sw.WriteLine("Traza:")
                sw.WriteLine(nEx.StackTrace)
                sw.WriteLine("--------------------------------------------------------------")
                sw.WriteLine("")

                sw.Flush()
                sw.Close()
            Catch ex As Exception
                Try : EventLog.WriteEntry(ex.Message, EventLogEntryType.Error) : Catch : End Try
            End Try

            Windows.Forms.Application.DoEvents()
        End Sub

#End Region

    End Class

End Namespace