Option Strict On

Imports Miharu.Desktop.Library.Config
Imports System.Text
Imports Miharu
Imports Miharu.Web.Controls
Imports DBCore
Imports Slyg.Data.DataBase
Imports Slyg.Data.Manager
Imports System.IO



Partial Public Class reporte2
    Inherits Imaging.FormBase

#Region " Declaraciones "


    Private _connectionString As String
    Private _basicConnectionString As String
    Private _parametros As New List(Of Object)
    Dim Id_Reporte As Integer

    Dim parametrosTable As DBCore.SchemaConfig.TBL_Reporte_ParametroDataTable
    Dim reportesTable As DBCore.SchemaConfig.TBL_ReporteDataTable

#End Region



#Region " Propiedades "



#End Region

#Region " Eventos "

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Load_Data()

    End Sub

#End Region

#Region " Procedimientos "



    Private Sub Load_Data()
        Limpieza_Variables()

        If Load_Info_Parametros() Then
            CargarParametros()
        End If

    End Sub

    Private Sub Limpieza_Variables()
        PanelParametros.Controls.Clear()
    End Sub

    Private Function Load_Info_Parametros() As Boolean

        If Me.MySesion.Pagina.Parameter("parametrosTable") Is Nothing Then

            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing

            Try

                dbmCore = New DBCore.DBCoreDataBaseManager(MyBase.ConnectionString.Core)

                dbmCore.Connection_Open(MySesion.Usuario.id)

                Id_Reporte = Get_Id_Reporte(Me.MySesion.Pagina.SecurityPath)

                reportesTable = dbmCore.SchemaConfig.TBL_Reporte.DBFindById_Reporte(Id_Reporte, 0, New DBCore.SchemaConfig.TBL_ReporteEnumList(DBCore.SchemaConfig.TBL_ReporteEnum.Nombre_Reporte, True))

                Me.MySesion.Pagina.Parameter("reportesTable") = reportesTable

                Dim conexionTable = dbmCore.SchemaConfig.TBL_Conexion.DBGet(reportesTable(0).fk_Entidad, reportesTable(0).fk_Conexion)

                _connectionString = conexionTable(0).Cadena_Conexion '& Program.DataRemoting.ToString()

                Me.MySesion.Pagina.Parameter("_connectionString") = _connectionString

                _basicConnectionString = GetBasicConnectionString(_connectionString)

                parametrosTable = dbmCore.SchemaConfig.TBL_Reporte_Parametro.DBGet(Nothing, Id_Reporte)

                Me.MySesion.Pagina.Parameter("parametrosTable") = parametrosTable

                Return True
            Catch ex As Exception
                Master.ShowAlert(ex.Message, MiharuMasterForm.MsgBoxIcon.IconError)
            Finally
                dbmCore.Connection_Close()
            End Try

            Return False

        Else
            reportesTable = CType(Me.MySesion.Pagina.Parameter("reportesTable"), DBCore.SchemaConfig.TBL_ReporteDataTable)
            parametrosTable = CType(Me.MySesion.Pagina.Parameter("parametrosTable"), DBCore.SchemaConfig.TBL_Reporte_ParametroDataTable)
            _connectionString = Me.MySesion.Pagina.Parameter("_connectionString").ToString
            _basicConnectionString = GetBasicConnectionString(_connectionString)
            Return True
        End If


    End Function

    Private Sub CargarParametros()

        Dim a = Me.MySesion

        Dim i As Integer = 0

        Dim parameter As IParameter

        For Each parametro In parametrosTable

            Select Case parametro.fk_Tipo_Parametro
                Case DesktopConfig.CampoTipo.Texto
                    parameter = New wucParameterText(parametro.Nombre_Parametro, Me.MySesion)
                    PanelParametros.Controls.Add(CType(parameter, System.Web.UI.Control))
                    _parametros.Add(parameter)

                Case DesktopConfig.CampoTipo.Numerico
                    parameter = New wucParameterNumeric(parametro.Nombre_Parametro, Me.MySesion)
                    PanelParametros.Controls.Add(CType(parameter, System.Web.UI.Control))
                    _parametros.Add(parameter)

                Case DesktopConfig.CampoTipo.Fecha
                    parameter = New wucDatePicker(parametro.Nombre_Parametro, Me.MySesion)
                    PanelParametros.Controls.Add(CType(parameter, System.Web.UI.Control))
                    _parametros.Add(parameter)

                Case DesktopConfig.CampoTipo.SiNo
                    parameter = New wucParameterCheck(parametro.Nombre_Parametro, Me.MySesion)
                    PanelParametros.Controls.Add(CType(parameter, System.Web.UI.Control))
                    _parametros.Add(parameter)

                Case DesktopConfig.CampoTipo.Lista

                    If (parametro.IsConsulta_ListaNull()) Then
                        Master.ShowAlert("El parámetro de tipo lista: " & parametro.Nombre_Parametro & ", no se ecuentra configurado", MiharuMasterForm.MsgBoxIcon.IconError)
                    Else
                        parameter = New wucParameterList(parametro.Nombre_Parametro, parametro.Consulta_Lista, parametro.Columna_Etiqueta_Lista, parametro.Columna_Valor_Lista, Me.MySesion)
                        If LoadList(CType(parameter, wucParameterList)) Then
                            PanelParametros.Controls.Add(CType(parameter, System.Web.UI.Control))
                            _parametros.Add(parameter)
                        End If

                    End If

                Case Else
                        Throw New Exception("Tipo de parámetro no válido: " & parametro.Nombre_Parametro)
            End Select

        Next

        'Se adiciona si debe realizar busqueda de archivo
        If reportesTable(0).Usa_Archivo Then
            Mostrar_Boton_Cargue_Archivo(True)
        Else
            Mostrar_Boton_Cargue_Archivo(False)
        End If

    End Sub

    Private Sub Mostrar_Boton_Cargue_Archivo(valor As Boolean)
        Button1.Visible = valor
        LblFile.Visible = valor
        If Not Session("fileupload") Is Nothing Then
            LblFile.Text = Session("fileupload").ToString
        End If
    End Sub

    Private Sub EjecutarSentencia()

        If reportesTable(0) IsNot Nothing And _parametros.Count > 0 Then
            Dim dataFile As DataTable = Nothing
            Dim titulosDataTable As DBCore.SchemaConfig.TBL_Reporte_SalidaDataTable
            Dim columnasDataTable As DBCore.SchemaConfig.TBL_Reporte_ColumnaDataTable = Nothing
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing

            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(MyBase.ConnectionString.Core)
                dbmCore.Connection_Open(MySesion.Usuario.id)

                ' Cargar los titulos de los resultados
                titulosDataTable = dbmCore.SchemaConfig.TBL_Reporte_Salida.DBGet(reportesTable(0).Id_Reporte, Nothing)

                If reportesTable(0).Usa_Archivo AndAlso reportesTable(0).Usa_Columnas_Ancho_Fijo Then
                    columnasDataTable = dbmCore.SchemaConfig.TBL_Reporte_Columna.DBGet(reportesTable(0).Id_Reporte, Nothing)
                End If
            Catch ex As Exception
                Master.ShowAlert("Error al conectarse a Core " + ex.ToString, MiharuMasterForm.MsgBoxIcon.IconError)
                Return
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            End Try


            Dim salto As Boolean = False
            Dim dbmReporte As DataBaseManager = Nothing

            Try
                dbmReporte = New DataBaseManager(_connectionString)
                dbmReporte.Connection_Open()

                Dim dataBaseType = DataBaseFactory.GetDataBaseType(_connectionString)

                'Realiza los reemplazos de los valores.
                Dim cSql = ReemplazarParametros(reportesTable(0).Consulta, "")


                ' Funcionalidad para cargar informacion a una tabla temporal
                If (reportesTable(0).Usa_Archivo) Then
                    If (dataBaseType <> dataBaseType.SqlServer) Then
                        Throw New Exception("La opción [Usa Archivo] solo es compatible con bases de datos SQL Server")
                    End If

                    Dim nFileName As String = ""

                    dataFile = CargarFile(reportesTable(0).Usa_Columnas_Ancho_Fijo, columnasDataTable, nFileName)
                    nFileName = "'" & Path.GetFileName(nFileName) & "'"
                    cSql = cSql.ToString().Replace("$FileName", nFileName)

                    If (dataFile Is Nothing) Then Return
                End If

                'Logica exclusiva para la funcionalidad de custodia de cajas
                'If ReportesTreeView.SelectedNode.Name = "2" Then
                '    cSql = cSql.ToString().Replace("@Sede", Program.DesktopGlobal.PuestoTrabajoRow.fk_Sede.ToString)
                '    cSql = cSql.ToString().Replace("@Boveda", Program.DesktopGlobal.BovedaRow.id_Boveda.ToString)
                'End If

                'Ejecuta la sentencia
                Try
                    If (reportesTable(0).Usa_Archivo) Then
                        BulkInsert.InsertDataTableReport(dataFile, dbmReporte, "#Report")
                    End If
                Catch ex As Exception
                    Throw New Exception("Error al cargar el archivo a la base de datos, la configuración es diferente al esquema del archivo seleccionado." & ex.Message)
                End Try

                ' Consulta de confirmación
                'If (Me._selectedReport.Usa_Consulta_Confirmacion) Then
                '    salto = ConsultaConfirmacion(dbmReporte)
                'End If

                ' Consulta usa exportar masivo
                If Not (reportesTable(0).Usa_Exportar_Masivo) Then
                    ExportarMasivoButton.Visible = False
                    If (salto) Then Return

                    'resultadosPanel.Controls.Clear()

                    If (reportesTable(0).Exportar_Texto_Plano) Then
                        'LeerDataReader(dbmReporte, dataBaseType, cSql)

                    Else
                        LeerDataSet(dbmReporte, dataBaseType, titulosDataTable, cSql)
                    End If
                Else
                    ExportarMasivoButton.Visible = True
                    'LeerDataSetUsaMasivo(dbmReporte, dataBaseType, titulosDataTable, cSql)
                End If

            Catch ex As Exception
                Master.ShowAlert("EjecutarSentencia" + ex.ToString, MiharuMasterForm.MsgBoxIcon.IconError)
                dbmReporte.Transaction_Rollback()
            Finally
                If (dbmReporte IsNot Nothing) Then dbmReporte.Connection_Close()
            End Try
        Else
            Master.ShowAlert("Se debe seleccionar un reporte o no hay parámetros para generar reporte.", MiharuMasterForm.MsgBoxIcon.IconWarning)
        End If
    End Sub


    Private Sub LeerDataSet(dbmReporte As DataBaseManager, dataBaseType As DataBaseType, titulosDataTable As DBCore.SchemaConfig.TBL_Reporte_SalidaDataTable, cSql As String)
        Dim resultados As DataSet = Nothing
        Try
            resultados = SqlData.ExecuteQuery(cSql, dbmReporte, dataBaseType)

            Me.wucReportSlygGridView1.NombreReporte = reportesTable(0).Nombre_Reporte

            Me.wucReportSlygGridView1.DataSource(resultados)

            Mostrar_Controles_Reporte()
        Catch ex As Exception

            Throw
        End Try

    End Sub


    Private Sub Mostrar_Controles_Reporte()
        Me.wucReportSlygGridView1.Visible = True
    End Sub

#End Region

#Region " Funciones "

    Private Function CargarFile(nUsaColumnasAnchoFijo As Boolean, nColumnas As DBCore.SchemaConfig.TBL_Reporte_ColumnaDataTable, ByRef nFileName As String) As DataTable
        Try

            Dim archivoCargue As String = Server.MapPath("~/_temporal/") + Session("fileupload").ToString
            'Dim archivoCargue As New OpenFileDialog()
            'archivoCargue.Title = "Por favor seleccione el archivo a cargar"
            'archivoCargue.Filter = "Archivos de texto plano|*.csv; *.txt; *.*"

            'If (archivoCargue.ShowDialog() = DialogResult.OK) Then
            Dim csvData As New DBCore.CSVData() 'Slyg.Tools.CSV.CSVData()
            Dim caracter As Char

            csvData.LinesToJump = reportesTable(0).Filas_Omitir

            If (nUsaColumnasAnchoFijo) Then
                If (nColumnas.Count = 0) Then
                    Throw New Exception("No se encuentra la configuración para el manejo del archivo, falta la definición de columnas, por favor comuniquese con el administrador para que la configure.")
                End If

                Dim definiciones = New List(Of DBCore.CSVLoadColumnDefinition)

                For Each column In nColumnas
                    definiciones.Add(New DBCore.CSVLoadColumnDefinition(column.Nombre_Columna, column.Inicio_Columna, column.Longitud_Columna))
                Next

                csvData.HasHeader = reportesTable(0).Maneja_Encabezado
                csvData.LoadCSV(archivoCargue, definiciones)
            Else
                If (reportesTable(0).IsCaracter_SeparadoNull() Or reportesTable(0).IsIdentificador_TextoNull()) Then
                    Throw New Exception("No se encuentra la configuración para el manejo del archivo, por favor comuniquese con el administrador para que la configure.")
                End If

                Select Case reportesTable(0).Caracter_Separado
                    Case ";"
                        caracter = ";"c
                    Case ","
                        caracter = Chr(44)
                    Case "TAB"
                        caracter = Chr(9)
                    Case " "
                        caracter = Chr(32)
                    Case Else
                        caracter = Chr(0)
                End Select

                csvData.LoadCSV(archivoCargue, reportesTable(0).Maneja_Encabezado, caracter, reportesTable(0).Identificador_Texto)
            End If

            nFileName = archivoCargue

            Return csvData.DataTable.ToDataTable()

        Catch ex As Exception
            Master.ShowAlert("Cargar Archivo " + ex.Message, MiharuMasterForm.MsgBoxIcon.IconError)
            Return Nothing
        End Try
    End Function

    Private Function Get_Id_Reporte(SecurityPathValue As String) As Integer
        Try
            Dim val As String() = SecurityPathValue.Split(CChar("."))
            Return CInt(val(2))
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Private Shared Function GetBasicConnectionString(nConnectionString As String) As String
        Dim partes = nConnectionString.Split(";"c)
        Dim result As String = ""

        For Each parte In partes
            If (Not parte.ToUpper().StartsWith("SLYGPROVIDER")) Then
                result &= parte & ";"
            End If
        Next

        Return result
    End Function

    Private Function ReemplazarParametros(nSql As String, nToIgnore As String) As String
        Try

            Dim WordCancelados As String = Mid(nSql, 1, 46)
            Dim ProcesoCancelados As Boolean

            If (WordCancelados = "EXEC [Report].[PA_Ejecutar_Cancelados_Informe]") Then
                ProcesoCancelados = True
            End If

            If ProcesoCancelados = True Then
                nSql = nSql.ToString().Replace("@UsuarioAccion", "'" + MySesion.Usuario.Login + "'")
            End If
            Dim i = 0
            While i < PanelParametros.Controls.Count
                Dim parameterControl = PanelParametros.Controls(i).Controls


                Dim parameter = CType(_parametros(i), IParameter)

                If (parameter.ParameterName <> nToIgnore) Then
                    nSql = nSql.ToString().Replace(parameter.ParameterName, parameter.GetStringParameter())
                End If
                i = i + 1
            End While

            Return nSql
        Catch ex As Exception

        End Try

        Return nSql
    End Function


    Private Function Validar() As Boolean

        Dim i As Integer = 0

        Dim PName As String = ""

        Try
            While i < PanelParametros.Controls.Count

                Dim parameter = CType(_parametros(i), IParameter)
                PName = parameter.ParameterName
                If parameter.GetStringParameter().Length = 0 Then

                    Return False

                End If
                i = i + 1
            End While

            If reportesTable(0).Usa_Archivo Then
                If Session("fileupload") Is Nothing Then
                    Return False
                End If
            End If

        Catch ex As Exception
            Master.ShowAlert("Falta seleccionar valor en : " + PName, MiharuMasterForm.MsgBoxIcon.IconError)
            Return False
        End Try

        Return True

    End Function

    Private Function LoadList(ByRef sender As wucParameterList) As Boolean

        Dim cnn As SqlClient.SqlConnection = Nothing

        Try
            cnn = New SqlClient.SqlConnection(_basicConnectionString)
            cnn.Open()

            Dim sql = ReemplazarParametros(sender.Query, sender.ParameterName)
            Dim da = New SqlClient.SqlDataAdapter(sql, cnn)
            Dim ds As DataSet = New DataSet()
            da.Fill(ds)

            Dim resultTable = New DataTable()

            da.Fill(resultTable)

            sender.DataSource = ds
            sender.DataBind()

            Return True
        Catch ex As Exception
            Master.ShowAlert("LoadList " + ex.ToString, MiharuMasterForm.MsgBoxIcon.IconError)
            Return False
        Finally
            If (cnn IsNot Nothing) Then cnn.Close()
        End Try
    End Function

    Protected Sub ImageButton1_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        If Validar() Then
            EjecutarSentencia()
        Else
            Master.ShowAlert("Debe diligenciar los valores para la consulta", MiharuMasterForm.MsgBoxIcon.IconError)
        End If
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Master.ShowDialog("../_sitio/fileupload/FileUpload.aspx", "Upload File", "Titulo", "400", "300", "400", "400", True)
    End Sub

#End Region



    Protected Sub ExportarMasivoButton_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ExportarMasivoButton.Click

        'Dim resultados As DataSet = Nothing
        'Dim contador As Int32
        'Dim dbmReporte As DataBaseManager = Nothing
        'Dim GuardaArchivo As String
        ''Dim DesktopControl = New DesktopReportDataGridViewControl
        'Dim ManejaEncabezado As String
        'Dim SeparadoComa As String
        'Dim SeparadoTabulador As String
        'Dim SeparadoPuntoyComa As String
        'Dim SeparadoVacio As String
        'Dim FormatExcel As String
        'Dim FormatTexto As String

        'Try
        '    dbmReporte = New DataBaseManager(_connectionString)
        '    dbmReporte.Connection_Open()

        '    Dim dataBaseType = DataBaseFactory.GetDataBaseType(_connectionString)
        '    Dim cSql = ReemplazarParametros(Me._selectedReport.Consulta, "")
        '    'dbmReporte.Transaction_Begin()

        '    'resultados.Tables.Add(dbmReporte.DataBase.ExecuteQueryGet(cSql))
        '    resultados = SqlData.ExecuteQuery(cSql, dbmReporte, dataBaseType)

        '    'dbmReporte.Transaction_Commit()
        'Catch ex As Exception
        '    'dbmReporte.Transaction_Rollback()
        '    Throw
        'End Try

        'For i = resultados.Tables.Count To 1 Step -1
        '    Dim tabla As DataTable

        '    tabla = resultados.Tables(i - 1)
        '    Dim nuevaGrilla = New DesktopReportDataGridViewControl

        '    nuevaGrilla.Conection_String_Core = Program.DesktopGlobal.ConnectionStrings.Core
        '    nuevaGrilla.Conection_String_Tools = Program.DesktopGlobal.ConnectionStrings.Tools

        '    nuevaGrilla.Titulo = tabla.Rows.Item(0).ItemArray(0).ToString()

        '    If resultados.Tables.Count = 1 Then
        '        nuevaGrilla.Dock = DockStyle.Fill
        '    Else
        '        nuevaGrilla.Dock = DockStyle.Top
        '    End If
        '    Dim index As Short
        '    index = CShort(resultados.Tables.IndexOf(tabla) + 1)

        '    nuevaGrilla.Id_Reporte = Me._selectedReport.Id_Reporte
        '    If Not Me._selectedReport Is Nothing Then
        '        nuevaGrilla.Salto_Linea = Me._SelectedTipoSalidaReporte.Codigo_Salto_Linea
        '    Else
        '        nuevaGrilla.Salto_Linea = ""
        '    End If

        '    nuevaGrilla.InternalGridView.DataSource = tabla
        '    nuevaGrilla.ButtonOnClickReportMas_ = True
        '    nuevaGrilla.Contadorshowmessage_ = contador
        '    nuevaGrilla.contarReportes_ = index

        '    nuevaGrilla.InternalGridView.DataSource = tabla
        '    Dim ParametrosForm = New FormParametrosExportacion
        '    If contador = 0 Then
        '        If ParametrosForm.ShowDialog = System.Windows.Forms.DialogResult.OK Then
        '            nuevaGrilla.Exportar(Convert.ToBoolean(ParametrosForm.ManejaEncabezado), Convert.ToBoolean(ParametrosForm.SeparadoComa),
        '                                 Convert.ToBoolean(ParametrosForm.SeparadoTabulador), Convert.ToBoolean(ParametrosForm.SeparadoPuntoyComa),
        '                                 Convert.ToBoolean(ParametrosForm.SeparadoVacio), Convert.ToBoolean(ParametrosForm.FormatExcel),
        '                                 Convert.ToBoolean(ParametrosForm.FormatTexto), nuevaGrilla.Salto_Linea)
        '            ManejaEncabezado = ParametrosForm.ManejaEncabezado
        '            SeparadoComa = ParametrosForm.SeparadoComa
        '            SeparadoTabulador = ParametrosForm.SeparadoTabulador
        '            SeparadoPuntoyComa = ParametrosForm.SeparadoPuntoyComa
        '            SeparadoVacio = ParametrosForm.SeparadoVacio
        '            FormatExcel = ParametrosForm.FormatExcel
        '            FormatTexto = ParametrosForm.FormatTexto
        '            GuardaArchivo = nuevaGrilla.GuardaArchivo_
        '        End If
        '    Else
        '        nuevaGrilla.GuardaArchivo_ = GuardaArchivo
        '        nuevaGrilla.Exportar(Convert.ToBoolean(ManejaEncabezado), Convert.ToBoolean(SeparadoComa),
        '                             Convert.ToBoolean(SeparadoTabulador), Convert.ToBoolean(SeparadoPuntoyComa),
        '                             Convert.ToBoolean(SeparadoVacio), Convert.ToBoolean(FormatExcel),
        '                             Convert.ToBoolean(FormatTexto), nuevaGrilla.Salto_Linea)
        '    End If
        '    contador = contador + 1

        'Next

        'Me.Cursor = Cursors.Default



    End Sub


End Class