Imports System.ComponentModel
Imports Miharu.Desktop.Library.Config
Imports Slyg.Tools
Imports DBImaging
Imports System.Threading
Imports Miharu.FileProvider.Library
Imports System.Data.SqlClient
Imports Slyg.Tools.Imaging
Imports System.IO
Imports System.Drawing
Imports System.Drawing.Imaging
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports DBCore

' LOL
Public Class ProcesaClientesEnLog

    Private Const THREAD_POOL_SIZE As Integer = 5

    Private _outputFolder As String
    Private _entidad As Short
    Private _proyecto As Short
    Private _nombreProyecto As String
    Private _worker As BackgroundWorker
    Private _e As DoWorkEventArgs
    Private _dataRespuesta As DataTable
    Private _exportacionesCompletas As Integer
    Private _bussyThreads As HashSet(Of Integer)
    Private _lockConcurrencia As Object

#Region " Constructor "

    Sub New(OutputFolder As String, entidad As Short, proyecto As Short, nombreProyecto As String, ByVal worker As BackgroundWorker, ByVal e As DoWorkEventArgs)
        _outputFolder = OutputFolder
        _entidad = entidad
        _proyecto = proyecto
        _nombreProyecto = nombreProyecto
        _worker = worker
        _e = e
        _dataRespuesta = Nothing
        _exportacionesCompletas = 0
        _bussyThreads = New HashSet(Of Integer)()
        _lockConcurrencia = New Object
    End Sub

#End Region

#Region " Funciones "

    Public Function ProcesaCargue(ByVal data As DataTable) As DesktopConfig.TypeResultDataDate
        Dim trRetrun As New DesktopConfig.TypeResultDataDate
        trRetrun.Result = True

        ' 1) Limpiar llaves de repetidas y ordenarlas para procesarlas, crear el diccionario con los agrupados por llaves y guardar el indice en el que se presentaran en la tabla respuesta
        Dim view = New DataView(data)
        view.Sort = "Llave_01 ASC, Llave_02 ASC"
        _dataRespuesta = view.ToTable("SortedUniqueData", True, "Llave_01", "Llave_02")
        Dim cacheInstancias As New Dictionary(Of String, InstanciaAProcesarClinetesEnLog)
        For i As Integer = 0 To _dataRespuesta.Rows.Count - 1
            Dim actualRow = _dataRespuesta.Rows(i)
            Dim actualLlave_01 As String = actualRow.Item("Llave_01").ToString()
            Dim actualLlave_02 As String = actualRow.Item("Llave_02").ToString()
            Dim nuevaInstancia As InstanciaAProcesarClinetesEnLog = New InstanciaAProcesarClinetesEnLog(Me, _outputFolder, actualLlave_01, actualLlave_02, i)
            Dim llaveCache As String = nuevaInstancia.getSumaDeLlaves()
            cacheInstancias.Add(llaveCache, nuevaInstancia)
        Next

        ' 2) Recuperar files asociados a las llaves
        Dim files As DataTable = Nothing
        Dim dbmImaging = New DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
        Dim dbmCore = New DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
        Try
            dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
            dbmCore.Connection_Open(Program.Sesion.Usuario.id)
            BulkInsert.InsertDataTable(_dataRespuesta, dbmCore, "#TempLlaves") 'Esta instruccion crea la columna REGISTRO
            _dataRespuesta.Columns.Add("Files")
            _dataRespuesta.Columns.Add("Folios_Esperados")
            _dataRespuesta.Columns.Add("Folios_Recuperables")
            _dataRespuesta.Columns.Add("Folios_Errados")
            _dataRespuesta.Columns.Add("Crea_pdf")
            _dataRespuesta.Columns("REGISTRO").SetOrdinal(0)
            files = getFilesExportacion(dbmCore)
        Catch ex As Exception
            MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            trRetrun.Result = False
            'trRetrun.Parameters = ex.ge
            Return trRetrun
        Finally
            If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
        End Try

        ' 3) Poblar el diccionario con los files de cada archivo (Llaves)
        Dim filesUnArchivo As DataTable = createEmptyTable(files.Columns)
        Dim agrupadoLlave_01 As String = ""
        Dim agrupadoLlave_02 As String = ""
        Dim ltCampos As List(Of String) = New List(Of String)
        For i As Integer = 0 To files.Rows.Count - 1
            Dim actualRow = files.Rows(i)
            Dim actualLlave_01 As String = actualRow.Item("Llave_01").ToString()
            Dim actualLlave_02 As String = actualRow.Item("Llave_02").ToString()

            If Not String.Equals(agrupadoLlave_01, actualLlave_01) Or Not String.Equals(agrupadoLlave_02, actualLlave_02) Then
                If filesUnArchivo.Rows.Count > 0 Then
                    Dim sumaDeLlaves As String = agrupadoLlave_01 '+ "_" + agrupadoLlave_02
                    Dim instancia As InstanciaAProcesarClinetesEnLog = cacheInstancias(sumaDeLlaves)
                    instancia.setFilesUnArchivo(filesUnArchivo)
                    cacheInstancias(sumaDeLlaves) = instancia
                End If
                agrupadoLlave_01 = actualLlave_01
                agrupadoLlave_02 = actualLlave_02
                filesUnArchivo = createEmptyTable(files.Columns)
            End If

            For Each itemCampo As DataColumn In files.Columns
                Dim valorCampo = actualRow.Item(itemCampo.ColumnName).ToString()
                ltCampos.Add(valorCampo)
            Next
            filesUnArchivo.Rows.Add(ltCampos.ToArray())
            ltCampos.Clear()
        Next
        If filesUnArchivo.Rows.Count > 0 Then
            Dim sumaDeLlaves As String = agrupadoLlave_01 '+ "_" + agrupadoLlave_02
            Dim instancia As InstanciaAProcesarClinetesEnLog = cacheInstancias(sumaDeLlaves)
            instancia.setFilesUnArchivo(filesUnArchivo)
            cacheInstancias(sumaDeLlaves) = instancia
        End If

        ' 4) Recorrer la tabla respuesta e ir procesando las instancias con ayuda de un ThreadPool
        ' https://docs.microsoft.com/en-us/dotnet/api/system.threading.threadpool.queueuserworkitem?view=netframework-4.8
        ' 4.1) Declarar las propiedades que manejan el ThreadPool
        Dim workerThreads As Integer
        Dim portThreads As Integer
        ThreadPool.GetMaxThreads(workerThreads, portThreads)
        Dim unfinishedThreads As HashSet(Of Integer) = New HashSet(Of Integer)()
        Dim cts As CancellationTokenSource = New CancellationTokenSource()

        ' 4.2) Dar tamaño al threadPool y encolar los Threads
        ThreadPool.SetMaxThreads(THREAD_POOL_SIZE, THREAD_POOL_SIZE)
        For i As Integer = 0 To _dataRespuesta.Rows.Count - 1
            Dim actualRow = _dataRespuesta.Rows(i)
            Dim actualLlave_01 As String = actualRow.Item("Llave_01").ToString()
            Dim actualLlave_02 As String = actualRow.Item("Llave_02").ToString()
            Dim sumaDeLlaves As String = actualLlave_01 '+ "_" + actualLlave_02
            Dim instancia As InstanciaAProcesarClinetesEnLog = cacheInstancias(sumaDeLlaves)
            _bussyThreads.Add(i)
            instancia.setCancelationTokenSource(cts)
            ThreadPool.QueueUserWorkItem(AddressOf instancia.ThreadPoolCallback, i)
        Next

        ' 4.3) Esperar la finalizacion o cancelacion del ThreadPool
        Dim esperarThreads As Boolean = True
        While esperarThreads
            SyncLock _lockConcurrencia
                If _worker.CancellationPending Then
                    cts.Cancel()
                    _e.Cancel = True
                Else
                    If _bussyThreads.Count = 0 Then
                        esperarThreads = False
                    End If
                End If
            End SyncLock
            Thread.Sleep(1000)
        End While

        ' 4.4) Devolver el ThreadPool al valor preio a la ejecucion y retornar la respuesta
        ThreadPool.SetMaxThreads(workerThreads, portThreads)
        trRetrun.tablaResultados = _dataRespuesta
        Return trRetrun
    End Function

    Private Function createEmptyTable(dataColumnCollection As DataColumnCollection) As DataTable
        Dim filesUnArchivo = New DataTable()
        For Each itemCampo As DataColumn In dataColumnCollection
            filesUnArchivo.Columns.Add(itemCampo.ColumnName)
        Next
        Return filesUnArchivo
    End Function

    Private Function getFilesExportacion(ByVal dbmCore As DBCoreDataBaseManager) As DataTable
        'Throw New System.Exception("JMGG Exception.")
        Dim dt As DataTable = Nothing
        Dim sqlquery As String = "SELECT DISTINCT " +
            "    Ll.Campo_Empaque AS Llave_01, '' AS Llave_02, " +
            "    I.fk_Expediente, I.fk_Folder, I.fk_File AS id_File, I.id_Version, " +
            "    I.fk_Entidad_Servidor,  " +
            "    I.fk_Servidor,  " +
            "    Ll.fk_Entidad, " +
            "    Ll.fk_Proyecto " +
            "FROM #TempLlaves Tll " +
            "    INNER JOIN [DB_Miharu.Core].[Process].TBL_Expediente_Llave_Linea AS Ll WITH(NOLOCK)" +
            "        ON Ll.Campo_Empaque = Tll.Llave_01 " +
            "    INNER JOIN [DB_Miharu.Core].[Imaging].[TBL_File] AS I WITH(NOLOCK) " +
            "        ON I.fk_Expediente = Ll.fk_Expediente " +
            "WHERE	Ll.fk_Entidad = @fk_entidad " +
            "        AND Ll.fk_Proyecto = @fk_proyecto " +
            "ORDER BY I.fk_Expediente, I.fk_Folder, I.fk_File ASC"

        sqlquery = sqlquery.Replace("@fk_entidad", CStr(_entidad))
        sqlquery = sqlquery.Replace("@fk_proyecto", CStr(_proyecto))

        ' dt = ExecuteQuery(sqlquery, dbmImaging)

        dt = dbmCore.SchemaImaging.PA_Busqueda_get_Llaves_Log.DBExecute(_entidad, _proyecto)

        Return dt
    End Function

    Private Function ExecuteQuery(ByVal s As String, ByVal dbmImaging As DBImagingDataBaseManager, ByVal ParamArray params() As SqlParameter) As DataTable
        Dim dt As DataTable = Nothing
        Using da As New System.Data.SqlClient.SqlDataAdapter(s, dbmImaging.DataBase.ConnectionString)
            dt = New DataTable
            If params.Length > 0 Then
                da.SelectCommand.Parameters.AddRange(params)
            End If
            da.SelectCommand.CommandTimeout = 86400
            da.Fill(dt)
        End Using
        Return dt
    End Function
#End Region

#Region " Metodos "

    Public Sub reportarRespuesta(indexEnTablaRta As Integer, filesSize As Integer, folios_Esperados As Integer, folios_Recuperables As Integer, folios_Errados As Integer, creaPdf As Boolean, threadIndex As Integer)
        SyncLock _lockConcurrencia
            _dataRespuesta.Rows(indexEnTablaRta)("Files") = filesSize
            _dataRespuesta.Rows(indexEnTablaRta)("Folios_Esperados") = folios_Esperados
            _dataRespuesta.Rows(indexEnTablaRta)("Folios_Recuperables") = folios_Recuperables
            _dataRespuesta.Rows(indexEnTablaRta)("Folios_Errados") = folios_Errados
            _dataRespuesta.Rows(indexEnTablaRta)("Crea_pdf") = creaPdf

            _exportacionesCompletas += 1
            _worker.ReportProgress(_exportacionesCompletas)

            _bussyThreads.Remove(threadIndex)
        End SyncLock
    End Sub

#End Region

End Class