Imports System.Globalization
Imports System.IO
Imports System.ServiceProcess
Imports System.Text.RegularExpressions
Imports System.Threading
Imports System.Windows.Forms
Imports System.Xml
Imports System.Xml.Serialization
Imports DBImaging
Imports DBImaging.SchemaConfig
Imports Microsoft.VisualBasic.Logging
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Config
Imports Miharu.Security.Library.Session
Imports Miharu.Security.Library.WebService

Namespace Servicio
    Public Class CargueLogService

#Region " Declaraciones "
        Private Detener As Boolean

        Private _File As Stream = Nothing
        Private _DataFile As DataTable = Nothing
        Private _DataRegistros As Integer = 0
        Private _DataColumnas As Integer = 0
        Private _EstadoProceso As Short = 0 '0 Validando, 1 Procesando.
        Private objCSV As New Slyg.Tools.CSV.CSVData
        Private objXLS As New XLSData
        Private trResultado As DesktopConfig.TypeResult
        Private Segundos As Integer = 0
        Private Minuto As Integer = 0
        Private Hora As Integer = 0
        Private TiposLogDataTable As DBImaging.SchemaConfig.TBL_Tipos_LogDataTable
        Private validaArchivoCargue As Boolean
        Private SaltoPrimasLineas As Integer
        Private ExtensionGlobal As String = ""
        Private _fechaProceso As DateTime
        Private passwordFile As String = ""
        Private Usa_Fecha_Recaudo As Boolean = False
#End Region

#Region " Metodos reemplazados "
        Protected Overrides Sub OnStart(ByVal args() As String)
            ' Agregue el código aquí para iniciar el servicio. Este método debería poner
            ' en movimiento los elementos para que el servicio pueda funcionar.
            IniciarServicio()
        End Sub

        Protected Overrides Sub OnStop()
            ' Agregue el código aquí para realizar cualquier anulación necesaria para detener el servicio.
            DetenerServicio()
        End Sub
#End Region

#Region " Metodos "

        Private Sub LoadConfig()
            ' Leer la configuración
            If (File.Exists(Program.AppDataPath + CargueLogConfig.ConfigFileName)) Then
                Program.Config = CargueLogConfig.Deserialize(Program.AppDataPath)
            End If
        End Sub

        Public Sub IniciarServicio()
            WriteErrorLog("Funcion Iniciar Servicio")

            Try
                Dim WebService As SecurityWebService

                LoadConfig()

                WebService = New SecurityWebService(Program.Config.SecurityWebServiceURL, "")
                WriteErrorLog(Program.Config.SecurityWebServiceURL)

#If Not DEBUG Then
            ' Validar que la versión corresponda
            Dim VersionApp As String = WebService.getAssemblyVersion(Program.AssemblyName)

            If Not VersionApp = Program.AssemblyVersion Then
                WriteErrorLog("La versión del aplicativo no corresponde a la registrada en la base de datos," & vbCrLf & vbCrLf & _
                                "Versión registrada: [" & VersionApp & "]" & vbCrLf & _
                                "Versión ejecutable: [" & Program.AssemblyVersion & "]")

                Me.Stop()

                Return
            End If
#End If

                WebService.CrearCanalSeguro()
                WebService.setUser(Program.Config.User, CargueLogConfig.Decrypt(Program.Config.Password))
                Program.ConnectionStrings = CargueLogConfig.getCadenasConexion(WebService)

                If Program.ConnectionStrings.Security = "" Then
                    WriteErrorLog("No se pudo obtener la cadena de conexión a la base de datos Security")
                    Me.Stop()

                    Return
                End If

                If Program.ConnectionStrings.Imaging = "" Then
                    WriteErrorLog("No se pudo obtener la cadena de conexión a la base de datos Imaging")
                    Me.Stop()

                    Return
                End If

                Dim NewThread As New Thread(AddressOf Proceso)

                Detener = False
                NewThread.Start()

            Catch ex As Exception
                WriteErrorLog("Error IniciarServicio ex: " & ex.Message & " " & ex.ToString())
                Me.Stop()
            End Try
        End Sub

        Public Sub DetenerServicio()
            Detener = True
        End Sub

        Private Sub Proceso()
            Dim dbmSecurity As DBSecurity.DBSecurityDataBaseManager = Nothing
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dbmSecurity = New DBSecurity.DBSecurityDataBaseManager(Program.ConnectionStrings.Security)
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.ConnectionStrings.Imaging)

                While Not Detener
                    If Detener Then Return

                    Try
                        dbmSecurity.Connection_Open(1)
                        dbmImaging.Connection_Open(1)

                        Dim CalendarioDataTable = dbmSecurity.SchemaConfig.TBL_Calendario.DBFindByfk_EntidadNombre_Calendario(1, "CargueLog")

                        If CalendarioDataTable.Rows.Count > 0 Then
                            Dim habil = dbmSecurity.SchemaConfig.PA_Es_Hora_Habil.DBExecute(CalendarioDataTable(0).fk_Entidad, CalendarioDataTable(0).id_Calendario)

                            If (habil) Then
                                Dim CargueLogDatatable As DBImaging.SchemaConfig.TBL_Tipos_LogDataTable

                                CargueLogDatatable = dbmImaging.SchemaConfig.TBL_Tipos_Log.DBFindByEliminadoUsa_Cargue_Automatico(0, 1)

                                If CargueLogDatatable.Rows.Count > 0 Then
                                    CargarLogs(dbmImaging, CargueLogDatatable)
                                Else
                                    WriteErrorLog("No hay logs para cargue automático para ejecutar")
                                End If
                            Else
                                WriteErrorLog("No es una hora habil para ejecutar el proceso")
                            End If
                        Else
                            WriteErrorLog("No existe el calendario del servicio de cargue de log")
                        End If
                    Catch ex As Exception
                        WriteErrorLog("Error Proceso ex: " & ex.ToString())
                    Finally
                        If (dbmSecurity IsNot Nothing) Then dbmSecurity.Connection_Close()
                        If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                    End Try

                    If Detener Then Return

                    Thread.Sleep(Program.Config.Intervalo) ' Esperar n segundos antes de continuar
                End While
            Catch ex As Exception
                WriteErrorLog("Error Proceso ex: " & ex.ToString())
            Finally
                If (dbmSecurity IsNot Nothing) Then dbmSecurity.Connection_Close()
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

        Private Sub CargarLogs(dbmImaging As DBImagingDataBaseManager, cargueLogDatatable As TBL_Tipos_LogDataTable)
            Dim RutaCargados As String = ""
            For Each log In cargueLogDatatable

                If log.Ruta.ToString() <> "" Then
                    Dim FileNames As String()

                    If log.Extension_Archivo.ToString() <> "" Then
                        FileNames = Directory.GetFiles(log.Ruta.ToString(), "*" & log.Extension_Archivo)
                    Else
                        FileNames = Directory.GetFiles(log.Ruta.ToString())
                    End If

                    For Each nfile In FileNames
                        Dim NombreArchivo As String = Path.GetFileName(nfile)
                        _File = File.OpenRead(nfile)

                        'Si archivo cumple con las caracteristicas del nombre continua
                        If ValidaFormatoCorrecto_Log(log.id_Tipo_log, NombreArchivo, log) Then
                            validaArchivoCargue = log.Valida_ArchivoCargue

                            If Not String.IsNullOrEmpty(log.Salto_Primeras_Lineas.ToString()) Then
                                Me.SaltoPrimasLineas = Convert.ToInt32(log.Salto_Primeras_Lineas.ToString())
                            End If

                            If ValidarExiste_ArchivoCargue(NombreArchivo, dbmImaging, log) Then
                                'ajustar esquema
                                trResultado = CargarArchivo(NombreArchivo, log.fk_Entidad, 1, dbmImaging, log)

                                _File.Close()
                                _File.Dispose()

                                If trResultado.Result Then

                                    If log.Usa_Proceso_Posterior AndAlso Not log.Isconsulta_Proceso_PosteriorNull Then
                                        If log.consulta_Proceso_Posterior <> "" Then
                                            Dim cSql As String = log.consulta_Proceso_Posterior
                                            cSql = cSql.Replace("@fk_Cargue", trResultado.Cargue)
                                            SqlData.ExecuteQuery(cSql, dbmImaging)
                                        End If
                                    End If

                                    RutaCargados = log.Ruta & "\Cargados"
                                    If Not Directory.Exists(RutaCargados) Then
                                        Directory.CreateDirectory(RutaCargados)
                                    End If

                                    File.Move(nfile, RutaCargados & "\" & NombreArchivo)
                                Else
                                    WriteErrorLog("Error cargue log: " & log.Nombre_Tipo_Log.ToString() & ", NombreArchivo: " & Path.GetFileName(nfile) & ", error:" & trResultado.Resumen.ToString())
                                End If
                            Else
                                _File.Close()
                                _File.Dispose()
                                WriteErrorLog("Archivo ya fue cargado previamente Tipo Log: " & log.Nombre_Tipo_Log.ToString() & ", NombreArchivo: " & Path.GetFileName(nfile))
                            End If
                        Else
                            _File.Close()
                            _File.Dispose()
                            WriteErrorLog("Nombre de archivo no es valido Tipo Log: " & log.Nombre_Tipo_Log.ToString() & ", NombreArchivo: " & Path.GetFileName(nfile))
                        End If
                    Next
                Else
                    WriteErrorLog("No existe ruta para cargar log: " & log.Nombre_Tipo_Log.ToString())
                End If
            Next
        End Sub

        Private Sub WriteErrorLog(ByVal nMessage As String)
            'SyncLock objectLock
            Try
                Dim sw As New StreamWriter(Program.AppDataPath & "log_" & Now.ToString("yyyyMMdd") & ".txt", True)

                sw.WriteLine("--------------------------------------------------------------")
                sw.WriteLine(Now.ToString("yyyy-MM-dd HH:mm:ss"))
                sw.WriteLine("Mensaje: " & nMessage)
                sw.WriteLine("--------------------------------------------------------------")
                sw.WriteLine("")

                sw.Flush()
                sw.Close()
            Catch ex As Exception
                Try : JWriteLog(ex.ToString(), EventLogEntryType.Error) : Catch : End Try
            End Try
            Windows.Forms.Application.DoEvents()

            'End SyncLock
        End Sub

        Public Sub JWriteLog(ByVal mensaje As String, ByVal tipo As EventLogEntryType)

            If Not EventLog.SourceExists("MiharuCargueLogDDOService") Then
                EventLog.CreateEventSource("MiharuCargueLogDDOService", "Application")
            End If

            Dim eventLog1 As EventLog = New EventLog()
            eventLog1.Source = "MiharuCargueLogDDOService"
            eventLog1.WriteEntry(mensaje, tipo)

        End Sub

#End Region

#Region " Funciones "

        Private Function CargarArchivo(ByVal NombreArchivo As String, ByVal Entidad As Short, ByVal Esquema As Short, ByVal dbmImaging As DBImaging.DBImagingDataBaseManager, nlog As DBImaging.SchemaConfig.TBL_Tipos_LogRow) As DesktopConfig.TypeResult
            Dim trReturn As New DesktopConfig.TypeResult
            trReturn.Result = True
            Dim _Data As DataTable = New DataTable

            Dim CamposLog As DataTable = New DataTable

            Dim CamposLLenos As Boolean = False
            Dim contadorValoresEncontrados As Integer = 0
            Dim ltCamposLog As List(Of String) = New List(Of String)
            Try
                If ((nlog.Extension_Archivo.ToUpper() = ".XLS") Or (nlog.Extension_Archivo.ToUpper() = ".XLSX")) Then
                    Dim dsHojas As DataSet = objXLS.ImportExcelXLS(CType(_File, FileStream).Name, nlog.ManejaEncabezado)
                    _Data = dsHojas.Tables(0)
                ElseIf nlog.Extension_Archivo.ToUpper() = ".XML" Then
                    Try
                        Dim ManejaNodoPrincipalLog As Boolean = nlog.Maneja_Nodo_Principal_XML
                        Dim NombreNodoPrincipalLog As String = nlog.Nombre_Nodo_Principal_XML
                        Dim ManejaNodoCabeceraLog As Boolean = nlog.Maneja_Nodo_Cabecera_XML
                        Dim NombreNodoCabeceraLog As String = nlog.Nombre_Nodo_Cabecera_XML
                        Dim NombreNodoListaRegistrosLog As String = nlog.Nombre_Nodo_Lista_RegistrosXML

                        'Creamos el "Document"
                        Dim xmlDocument = New XmlDocument()

                        'Cargamos el archivo
                        xmlDocument.Load(CType(_File, FileStream).Name)

                        'Obtenemos los campos del log
                        CamposLog = dbmImaging.SchemaConfig.TBL_Campos_Tipo_log.DBFindByfk_Tipo_logfk_Entidadfk_ProyectoEliminado(nlog.id_Tipo_log, nlog.fk_Entidad, nlog.fk_Proyecto, False)

                        Dim listaTagsCabecera As List(Of String) = New List(Of String)
                        Dim listaTagsRegistros As List(Of String) = New List(Of String)

                        'Se crean las columnas de la tabla Data y se identifican los campos segun clasificacion de cabecera o registro
                        For Each x As DataRow In CamposLog.Rows
                            If (_Data.Columns.Contains(x.Item("Nombre_Tag_Campo").ToString()) = False) Then
                                _Data.Columns.Add(x.Item("Nombre_Tag_Campo").ToString())
                            End If

                            Dim Is_Tag_Cabecera As Boolean = CBool(x.Item("Is_Tag_Cabecera"))
                            If (Is_Tag_Cabecera = True) Then
                                If (listaTagsCabecera.Contains(x.Item("Nombre_Tag_Campo").ToString()) = False) Then
                                    listaTagsCabecera.Add(x.Item("Nombre_Tag_Campo").ToString)
                                End If
                            Else
                                If (listaTagsRegistros.Contains(x.Item("Nombre_Tag_Campo").ToString()) = False) Then
                                    listaTagsRegistros.Add(x.Item("Nombre_Tag_Campo").ToString)
                                End If
                            End If
                        Next

                        Dim nombreNodoPrincipal As String = ""
                        Dim nombreSingleNodoCabecera As String = ""
                        Dim nombreSingleNodoRegistros As String = ""

                        If (ManejaNodoPrincipalLog = True) Then
                            nombreNodoPrincipal = NombreNodoPrincipalLog
                        End If

                        Dim listDataCabecera As List(Of String) = New List(Of String)

                        If (ManejaNodoCabeceraLog = True) Then

                            nombreSingleNodoCabecera = "/" & nombreNodoPrincipal & "/" & NombreNodoCabeceraLog

                            Dim nodoCabecera As XmlNode

                            'Obtenemos los hijos del nodo cabecera
                            nodoCabecera = xmlDocument.SelectSingleNode(nombreSingleNodoCabecera.Replace("//", "/"))

                            For i As Integer = 0 To listaTagsCabecera.Count - 1
                                For Each node As XmlNode In nodoCabecera.ChildNodes
                                    If (node.Name.ToString.ToLower = listaTagsCabecera(i).ToString.ToLower) Then
                                        listDataCabecera.Add(node.InnerText.ToString)
                                        Exit For
                                    End If
                                Next
                            Next
                        End If

                        Dim nodoRegList As XmlNodeList
                        'Obtenemos la lista de los nodos "caja"
                        nombreSingleNodoRegistros = "/" & nombreNodoPrincipal & "/" & NombreNodoListaRegistrosLog
                        nodoRegList = xmlDocument.SelectNodes(nombreSingleNodoRegistros.Replace("//", "/"))

                        Dim validacionEntidad As Boolean = True

                        'Iniciamos el ciclo de lectura Registros
                        For Each nodeReg As XmlNode In nodoRegList
                            Dim listDataRegistro As List(Of String) = CopyList(listDataCabecera)

                            For i As Integer = 0 To listaTagsRegistros.Count - 1
                                For Each attribute As XmlNode In nodeReg.Attributes
                                    If (attribute.Name.ToString.ToLower = listaTagsRegistros(i).ToString.ToLower) Then
                                        listDataRegistro.Add(attribute.InnerText.ToString)

                                        'Valido si los registros del log, correponden a la entidad seleccionada
                                        If (nlog.Valida_Entidad And attribute.Name.ToString.ToLower = nlog.Valida_Entidad_Contra_Campo.ToLower) Then

                                            If attribute.InnerText.ToString <> nlog.Valida_Entidad_Codigo Then
                                                MessageBox.Show("El Log que se está intentando cargar no corresponde al banco seleccionado", "Banco del log diferente al seleccionado")
                                                validacionEntidad = False
                                                Exit For
                                            End If
                                        End If

                                        Exit For
                                    End If
                                Next

                                If validacionEntidad = False Then
                                    Exit For
                                End If
                            Next

                            If validacionEntidad = False Then
                                Exit For
                            End If

                            _Data.Rows.Add(listDataRegistro.ToArray)
                        Next

                        If validacionEntidad = False Then
                            trReturn.Result = False
                            Dim lisMsgError = New List(Of String)
                            lisMsgError.Add("- Archivo no válido. Por favor verifique lo siguiente:")
                            lisMsgError.Add("- El Log que se está intentando cargar no corresponde a la Entidad seleccionada.")
                            trReturn.Parameters = lisMsgError
                            Return trReturn
                        End If
                    Catch ex As Exception
                        Console.Write(ex.ToString())
                    End Try
                Else
                    If (nlog.Extension_Archivo.ToUpper() = ".TXT" Or nlog.Extension_Archivo.ToUpper() = ".DAT" Or nlog.Extension_Archivo.ToUpper() = ".RTA") And (nlog.Separador = "") Then
                        CamposLog = dbmImaging.SchemaConfig.TBL_Campos_Tipo_log.DBFindByfk_Tipo_logfk_Entidadfk_ProyectoEliminado(nlog.id_Tipo_log, nlog.fk_Entidad, nlog.fk_Proyecto, False)
                        _Data = SubString(CamposLog, _File, nlog)
                    Else
                        objCSV.Separator = CChar(IIf(nlog.Separador = "TAB", ControlChars.Tab, nlog.Separador))
                        'Se realiza el cargue del archivo en un datatable para luego validarlo.


                        objCSV.LoadCSV(CType(_File, FileStream).Name, nlog.ManejaEncabezado)
                        _Data = objCSV.DataTable.ToDataTable()
                    End If
                End If


                Dim objProcesaCargue As ProcesaCargueLog = New ProcesaCargueLog()
                Dim trRespuestaCargue As New DesktopConfig.TypeResult

                Dim CamposTipoLogDataTable = dbmImaging.SchemaConfig.TBL_Campos_Tipo_log.DBFindByfk_Tipo_logfk_Entidadfk_ProyectoEliminado(nlog.id_Tipo_log, nlog.fk_Entidad, nlog.fk_Proyecto, False)
                Dim CamposDataLog = dbmImaging.SchemaProcess.Get_Tabla_Log.DBExecute(nlog.fk_Entidad, nlog.fk_Proyecto)

                _DataFile = New DataTable()

                For Each itemCampo As DataColumn In CamposDataLog.Columns
                    _DataFile.Columns.Add(itemCampo.ColumnName)
                Next

                If _Data.Rows.Count > 0 Then
                    For index As Integer = 0 To _Data.Rows.Count - 1
                        For Each itemCampo As DataColumn In CamposDataLog.Columns
                            If itemCampo.ToString().ToUpper.Contains("CAMPO_") Or itemCampo.ToString().ToUpper.Contains("FECHA_RECAUDO") Then
                                Dim campoBuscar = itemCampo.ToString()
                                Dim ax_encontrado = CamposTipoLogDataTable.Where(Function(x) x.Nombre_Campo = campoBuscar.ToString()).ToList()
                                Dim CampoTipoLogRow As DBImaging.SchemaConfig.TBL_Campos_Tipo_logRow = Nothing

                                If ax_encontrado.Count > 0 Then
                                    CampoTipoLogRow = ax_encontrado.First()
                                    Dim valorCampo = _Data.Rows(index)(CampoTipoLogRow.Descripcion).ToString()
                                    ltCamposLog.Add(valorCampo)
                                Else
                                    ltCamposLog.Add("")
                                End If
                            Else
                                ltCamposLog.Add("")
                            End If
                        Next
                        _DataFile.Rows.Add(ltCamposLog.ToArray())
                        _DataFile.Rows(index)("fk_Entidad") = nlog.fk_Entidad
                        _DataFile.Rows(index)("fk_Proyecto") = nlog.fk_Proyecto
                        _DataFile.Rows(index)("fk_Tipo_Log") = nlog.id_Tipo_log
                        _DataFile.Rows(index)("Fecha_Proceso") = Date.Now.ToString("yyyyMMdd") ' dtpFechaProceso.Value.ToString("yyyyMMdd")
                        _DataFile.Rows(index)("fk_Tipo_Registro_Log") = 3 'detalle

                        'validar
                        'If (Usa_Fecha_Recaudo) Then
                        '    _DataFile.Rows(index)("Fecha_Recaudo") = dtpFechaRecaudo.Value.ToString("yyyy/MM/dd")
                        'End If

                        ltCamposLog.Clear()
                    Next

                End If

                _DataRegistros = _DataFile.Rows.Count
                _DataColumnas = _DataFile.Columns.Count

                If _DataRegistros > 0 And _DataColumnas > 1 Then
                    'CargueBackgroundWorker.ReportProgress(0)

                    trRespuestaCargue = objProcesaCargue.ProcesaCargue(_DataFile, NombreArchivo, nlog.id_Tipo_log, nlog.Valida_Duplicados, Date.Now.ToString("yyyyMMdd"), nlog)

                    If trRespuestaCargue.Result Then
                        trReturn = trRespuestaCargue
                    Else
                        trReturn.Result = False
                        trReturn.Parameters = trRespuestaCargue.Parameters
                        trReturn.Resumen = trRespuestaCargue.Resumen
                    End If
                Else
                    trReturn.Result = False
                    Dim lisMsgError = New List(Of String)
                    lisMsgError.Add("- Archivo no válido. Por favor verifique lo siguiente:")
                    lisMsgError.Add("- 1. Que el archivo contenga datos.")
                    lisMsgError.Add("- 2. El carácter de separación sea el adecuado.")
                    trReturn.Parameters = lisMsgError
                End If
            Catch ex As Exception
                Dim lisMsgError = New List(Of String)
                lisMsgError.Add("- Error: " & ex.Message)
                trReturn.Result = False
                trReturn.Parameters = lisMsgError
            End Try

            Return trReturn
        End Function

        Private Function SubString(ByVal CamposLog As DataTable, ByVal File As Stream, nlog As DBImaging.SchemaConfig.TBL_Tipos_LogRow) As DataTable
            Using Lee = New System.IO.StreamReader(CType(_File, FileStream).Name)
                Dim Data = New DataTable()

                For Each x As DataRow In CamposLog.Rows
                    Data.Columns.Add(x.Item("Descripcion").ToString())
                Next

                Dim texto = ""
                Dim numLinea = 0
                If CInt(nlog.Salto_Primeras_Lineas) = 0 Then
                    texto = Lee.ReadLine()
                    numLinea = numLinea + 1
                Else
                    For index As Integer = 0 To CInt(nlog.Salto_Primeras_Lineas) ' - 1
                        texto = Lee.ReadLine()
                        numLinea = numLinea + 1
                    Next
                End If

                Dim longitud_Fija = 0

                If Not IsDBNull(nlog.Longitud_Fija) Then
                    longitud_Fija = CInt(nlog.Longitud_Fija)
                End If

                While texto IsNot Nothing
                    Dim lenTexto = texto.Length
                    Dim Valida = True
                    If (nlog.Identificador_Linea <> "" Or Not IsDBNull(nlog.Identificador_Linea)) Then
                        Dim lon = nlog.Identificador_Linea.Length
                        If (lenTexto > 0) Then
                            If (texto.Substring(0, lon) = nlog.Identificador_Linea) Then
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
                        If (longitud_Fija <> 0 And lenTexto = longitud_Fija) Or (longitud_Fija = 0) Or (Not nlog.IsidentificadorLineaDetalleNull()) Then
                            Dim row = Data.NewRow
                            Try
                                'TODO
                                If (Not nlog.IsidentificadorLineaDetalleNull()) Then
                                    If texto.Substring(0, nlog.identificadorLineaDetalle.Length) = nlog.identificadorLineaDetalle Then
                                        For Each x As DataRow In CamposLog.Rows
                                            row.Item(x.Item("Descripcion").ToString()) = "" + texto.Substring(CInt(x.Item("Length_Inicia")), CInt(x.Item("Length_Fijo")))
                                        Next
                                        Data.Rows.Add(row)
                                    End If
                                Else
                                    For Each x As DataRow In CamposLog.Rows
                                        row.Item(x.Item("Descripcion").ToString()) = "" + texto.Substring(CInt(x.Item("Length_Inicia")), CInt(x.Item("Length_Fijo")))
                                    Next
                                    Data.Rows.Add(row)
                                End If
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

                If Not IsDBNull(nlog.Salto_Ultimas_Lineas) Then
                    saltosUltimas = nlog.Salto_Ultimas_Lineas
                End If

                For index As Integer = 0 To CInt(saltosUltimas) - 1
                    Data.Rows.RemoveAt(Data.Rows.Count - 1)
                Next

                Return Data
            End Using
        End Function

        Public Function CopyList(Of T)(oldList As List(Of T)) As List(Of T)

            'Serialize
            Dim xmlString As String = ""
            Dim string_writer As New StringWriter
            Dim xml_serializer As New XmlSerializer(GetType(List(Of T)))
            xml_serializer.Serialize(string_writer, oldList)
            xmlString = string_writer.ToString()

            'Deserialize
            Dim string_reader As New StringReader(xmlString)
            Dim newList As List(Of T)
            newList = DirectCast(xml_serializer.Deserialize(string_reader), List(Of T))
            string_reader.Close()

            Return newList
        End Function

        Private Function ValidarExiste_ArchivoCargue(NombreArchivo As String, dbmImaging As DBImagingDataBaseManager, nlog As DBImaging.SchemaConfig.TBL_Tipos_LogRow) As Boolean
            Dim retorno As Boolean = True
            Try
                Dim Cargue_valido = dbmImaging.SchemaConfig.TBL_Cargue.DBFindByfk_Entidadfk_Proyectofk_Tipo_LogArchivo_CargueValido(nlog.fk_Entidad, nlog.fk_Proyecto, nlog.id_Tipo_log, NombreArchivo, True)

                If Cargue_valido.Rows.Count > 0 Then
                    WriteErrorLog("Archivo cargado: " & NombreArchivo.ToString())
                    retorno = False
                End If
            Catch ex As Exception
                WriteErrorLog("ValidarExiste ArchivoCargue, nombre archivo: " & NombreArchivo.ToString() & ", error: " & ex.ToString())
            End Try
            Return retorno
        End Function

        Private Function ValidaFormatoCorrecto_Log(IdTipoLog As Integer, NombreArchivo As String, RowTipoLog As DBImaging.SchemaConfig.TBL_Tipos_LogRow) As Boolean
            Dim retorno As Boolean = True
            Dim NombreArchivo_Aux As String = ""
            If (RowTipoLog.Valida_Extension And NombreArchivo.Contains(".")) Then
                NombreArchivo_Aux = NombreArchivo.Remove(NombreArchivo.LastIndexOf("."), NombreArchivo.Length - NombreArchivo.LastIndexOf("."))
            Else
                NombreArchivo_Aux = NombreArchivo
            End If

            Try
                If (RowTipoLog IsNot Nothing) Then
                    ValidacionesSplit(retorno, RowTipoLog.Validaciones_ArchivoPlano.Split("|"c), RowTipoLog.LengthValidaciones_ArchivoPlano.Split("|"c), NombreArchivo_Aux, RowTipoLog.Usa_Nombre_Archivo_Exacto)
                End If
            Catch generatedExceptionName As Exception

                Throw
            End Try
            Return retorno
        End Function

        Private Function ValidacionesSplit(ByRef retorno As Boolean, StrValidacionColumn As String(), StrLengthValidaciones As String(), NombreArchivo_Aux As String, UsaNombreArchivoExacto As Boolean) As Object
            Dim NombreArchivoValidado As String = String.Empty
            Try
                If (StrValidacionColumn.Count() > 0) Then
                    For index As Integer = 0 To StrValidacionColumn.Count() - 1
                        Dim AuxItemValidacion As String = Nothing
                        If (StrValidacionColumn(index).ToString().Contains("FECHA") Or StrValidacionColumn(index).ToString().Contains("CORTE") Or StrValidacionColumn(index).ToString().Contains("CONSECUTIVO")) Then
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
                                        NombreArchivoValidado = NombreArchivoValidado + auxArchivoVal
                                    End If
                                Next
                            End If
                        ElseIf (AuxItemValidacion.Contains("<FECHA>")) Then
                            Dim dtAux As New DateTime()
                            auxArchivoVal = NombreArchivo_Aux.Substring(lengthInicio, (lengthFinal - lengthInicio))

                            AuxItemValidacion = AuxItemValidacion.Replace("<FECHA>", "")
                            retorno = DateTime.TryParseExact(auxArchivoVal, AuxItemValidacion, Nothing, DateTimeStyles.None, dtAux)

                            NombreArchivoValidado = NombreArchivoValidado + auxArchivoVal

                            If (retorno = False) Then
                                ' TODO: might not be correct. Was : Exit For
                                Exit For
                            End If
                        ElseIf AuxItemValidacion.Contains("<OFICINA>") Then
                            AuxItemValidacion = AuxItemValidacion.Replace("<OFICINA", "")

                            Continue For
                        ElseIf AuxItemValidacion.Contains("<NUMERO>") Then
                            AuxItemValidacion = AuxItemValidacion.Replace("<NUMERO>", "")

                            Continue For
                        ElseIf AuxItemValidacion.Contains("<CORTE>") Then
                            auxArchivoVal = NombreArchivo_Aux.Substring(lengthInicio, (lengthFinal - lengthInicio)).ToUpper()
                            AuxItemValidacion = AuxItemValidacion.Replace("<CORTE>", "")

                            Dim check As New Regex(AuxItemValidacion)

                            retorno = check.IsMatch(auxArchivoVal)
                            NombreArchivoValidado = NombreArchivoValidado + auxArchivoVal

                            If (retorno = False) Then
                                ' TODO: might not be correct. Was : Exit For
                                Exit For
                            End If
                        ElseIf AuxItemValidacion.Contains("<ANIO>") Then
                            AuxItemValidacion = AuxItemValidacion.Replace("<ANIO>", "")
                            Dim anios = Enumerable.Range(1900, DateTime.Now.Year)
                            Dim encontrado = anios.Where(Function(x) x.ToString() = AuxItemValidacion)

                            If encontrado.Count() = 0 Then
                                retorno = False
                            End If
                        ElseIf AuxItemValidacion.Contains("<CONCEPTO>") Then 'IMPUESTOS ASISTIDOS 
                            AuxItemValidacion = AuxItemValidacion.Replace("<CONCEPTO>", "")
                            Continue For
                        ElseIf AuxItemValidacion.Contains("<FORMATO>") Then 'IMPUESTOS ASISTIDOS 
                            AuxItemValidacion = AuxItemValidacion.Replace("<FORMATO>", "")
                            Continue For
                        ElseIf AuxItemValidacion.Contains("<VERSION>") Then 'IMPUESTOS ASISTIDOS 
                            AuxItemValidacion = AuxItemValidacion.Replace("<VERSION>", "")
                            Continue For
                        ElseIf AuxItemValidacion.Contains("<AÑO>") Then 'IMPUESTOS ASISTIDOS 
                            AuxItemValidacion = AuxItemValidacion.Replace("<AÑO>", "")
                            Continue For
                        ElseIf AuxItemValidacion.Contains("<CONSECUTIVO>") Then 'IMPUESTOS ASISTIDOS 
                            auxArchivoVal = NombreArchivo_Aux.Substring(lengthInicio, (lengthFinal - lengthInicio)).ToUpper()
                            AuxItemValidacion = AuxItemValidacion.Replace("<CONSECUTIVO>", "")

                            Dim check As New Regex(AuxItemValidacion)

                            retorno = check.IsMatch(auxArchivoVal)

                            NombreArchivoValidado = NombreArchivoValidado + auxArchivoVal

                            If (retorno = False) Then
                                ' TODO: might not be correct. Was : Exit For
                                Exit For
                            End If
                        Else
                            auxArchivoVal = NombreArchivo_Aux.Substring(lengthInicio, (lengthFinal - lengthInicio)).ToUpper()

                            NombreArchivoValidado = NombreArchivoValidado + auxArchivoVal

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

            If retorno Then
                If ((NombreArchivoValidado.ToUpper <> NombreArchivo_Aux.ToUpper) And (UsaNombreArchivoExacto)) Then
                    retorno = False
                End If
            End If

            Return retorno
        End Function

#End Region

    End Class
End Namespace

