Imports System.Windows.Forms
Imports System.IO
Imports Slyg.Data.DataBase
Imports Slyg.Data.Manager
Imports Miharu.Desktop.Controls.DesktopReportDataGridView
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Config
Imports System.Linq
Imports Miharu.Desktop.Library.Plugins

Namespace Forms.Reportes

    Public Class FormVisorReportes
        Inherits FormBase

#Region "Declaraciones"

        Private _selectedReport2 As DBCore.SchemaConfig.TBL_ReporteRow
        Private _selectedReport As DBCore.SchemaConfig.CTA_Reportes_Por_RolesPermiso_Usuario_CategoriaRow
        Private _SelectedTipoSalidaReporte As DBCore.SchemaConfig.TBL_Reporte_Tipo_SalidaRow
        Private _connectionString As String
        Private _basicConnectionString As String
        Private _Manager As DesktopPluginManager

#End Region

#Region "Enumeraciones"

        Public Enum IconoReportes As Integer
            Categoria = 0
            Reporte = 1
        End Enum

#End Region

        Public ReadOnly Property Manager() As DesktopPluginManager
            Get
                Return Me._Manager
            End Get
        End Property

#Region "Eventos"

        Private Sub FormVisorReportes_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

            CargarCategorias()
        End Sub

        Private Sub ReportesTreeView_AfterSelect(ByVal sender As Object, ByVal e As TreeViewEventArgs) Handles ReportesTreeView.AfterSelect
            If Not ReportesTreeView.SelectedNode Is Nothing Then
                If ReportesTreeView.SelectedNode.Name.StartsWith("C-") Then
                    CargarReportes(ReportesTreeView.SelectedNode)
                Else
                    '_selectedReport = CType(ReportesTreeView.SelectedNode.Tag, DBCore.SchemaConfig.TBL_ReporteRow)
                    _selectedReport = CType(ReportesTreeView.SelectedNode.Tag, DBCore.SchemaConfig.CTA_Reportes_Por_RolesPermiso_Usuario_CategoriaRow)
                    nombreReporteLabel.Text = _selectedReport.Nombre_Reporte
                    CargarParametros()
                End If
                ReportesTreeView.SelectedNode.Expand()
            End If
        End Sub

        Private Sub EjecutarButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ejecutarButton.Click
            Me.Cursor = Cursors.AppStarting
            EjecutarSentencia()
            Me.Cursor = Cursors.Default
        End Sub

        Private Sub ExportarMasivoButton_Click(sender As System.Object, e As System.EventArgs) Handles ExportarMasivoButton.Click

            Me.Cursor = Cursors.AppStarting
            Dim resultados As DataSet = Nothing
            Dim contador As Int32
            Dim dbmReporte As DataBaseManager = Nothing
            Dim GuardaArchivo As String
            Dim DesktopControl = New DesktopReportDataGridViewControl
            Dim ManejaEncabezado As String
            Dim SeparadoComa As String
            Dim SeparadoTabulador As String
            Dim SeparadoPuntoyComa As String
            Dim SeparadoVacio As String
            Dim FormatExcel As String
            Dim FormatTexto As String

            Try
                dbmReporte = New DataBaseManager(_connectionString)
                dbmReporte.Connection_Open()

                Dim dataBaseType = DataBaseFactory.GetDataBaseType(_connectionString)
                Dim cSql = ReemplazarParametros(Me._selectedReport.Consulta, "")
                'dbmReporte.Transaction_Begin()

                'resultados.Tables.Add(dbmReporte.DataBase.ExecuteQueryGet(cSql))
                resultados = SqlData.ExecuteQuery(cSql, dbmReporte, dataBaseType)

                'dbmReporte.Transaction_Commit()
            Catch ex As Exception
                'dbmReporte.Transaction_Rollback()
                Throw
            End Try

            For i = resultados.Tables.Count To 1 Step -1
                Dim tabla As DataTable

                tabla = resultados.Tables(i - 1)
                Dim nuevaGrilla = New DesktopReportDataGridViewControl

                nuevaGrilla.Conection_String_Core = Program.DesktopGlobal.ConnectionStrings.Core
                nuevaGrilla.Conection_String_Tools = Program.DesktopGlobal.ConnectionStrings.Tools

                nuevaGrilla.Titulo = tabla.Rows.Item(0).ItemArray(0).ToString()

                If resultados.Tables.Count = 1 Then
                    nuevaGrilla.Dock = DockStyle.Fill
                Else
                    nuevaGrilla.Dock = DockStyle.Top
                End If
                Dim index As Short
                index = CShort(resultados.Tables.IndexOf(tabla) + 1)

                nuevaGrilla.Id_Reporte = Me._selectedReport.Id_Reporte
                If Not Me._selectedReport Is Nothing Then
                    nuevaGrilla.Salto_Linea = Me._SelectedTipoSalidaReporte.Codigo_Salto_Linea
                Else
                    nuevaGrilla.Salto_Linea = ""
                End If

                If Not _selectedReport.Muestra_Columna_Nombre_Reporte_Masivo Then
                    Dim tabla2 As DataTable = Nothing
                    tabla2 = tabla.Copy
                    tabla2.Columns.Remove(tabla2.Columns(0))
                    tabla2.AcceptChanges()

                    nuevaGrilla.InternalGridView.DataSource = tabla2
                Else
                    nuevaGrilla.InternalGridView.DataSource = tabla
                End If

                nuevaGrilla.ButtonOnClickReportMas_ = True
                nuevaGrilla.Contadorshowmessage_ = contador
                nuevaGrilla.contarReportes_ = index

                Dim ParametrosForm = New FormParametrosExportacion
                If contador = 0 Then
                    If ParametrosForm.ShowDialog = System.Windows.Forms.DialogResult.OK Then
                        nuevaGrilla.Exportar(Convert.ToBoolean(ParametrosForm.ManejaEncabezado), Convert.ToBoolean(ParametrosForm.SeparadoComa),
                                             Convert.ToBoolean(ParametrosForm.SeparadoTabulador), Convert.ToBoolean(ParametrosForm.SeparadoPuntoyComa),
                                             Convert.ToBoolean(ParametrosForm.SeparadoVacio), Convert.ToBoolean(ParametrosForm.FormatExcel),
                                             Convert.ToBoolean(ParametrosForm.FormatTexto), nuevaGrilla.Salto_Linea)
                        ManejaEncabezado = ParametrosForm.ManejaEncabezado
                        SeparadoComa = ParametrosForm.SeparadoComa
                        SeparadoTabulador = ParametrosForm.SeparadoTabulador
                        SeparadoPuntoyComa = ParametrosForm.SeparadoPuntoyComa
                        SeparadoVacio = ParametrosForm.SeparadoVacio
                        FormatExcel = ParametrosForm.FormatExcel
                        FormatTexto = ParametrosForm.FormatTexto
                        GuardaArchivo = nuevaGrilla.GuardaArchivo_
                    End If
                Else
                    nuevaGrilla.GuardaArchivo_ = GuardaArchivo
                    nuevaGrilla.Exportar(Convert.ToBoolean(ManejaEncabezado), Convert.ToBoolean(SeparadoComa),
                                         Convert.ToBoolean(SeparadoTabulador), Convert.ToBoolean(SeparadoPuntoyComa),
                                         Convert.ToBoolean(SeparadoVacio), Convert.ToBoolean(FormatExcel),
                                         Convert.ToBoolean(FormatTexto), nuevaGrilla.Salto_Linea)
                End If
                contador = contador + 1

            Next

            Me.Cursor = Cursors.Default

        End Sub

        Private Sub LoadList(sender As Controls.ParameterList)
            Dim cnn As SqlClient.SqlConnection = Nothing

            Try
                cnn = New SqlClient.SqlConnection(_basicConnectionString)
                cnn.Open()

                Dim sql = ReemplazarParametros(sender.Query, sender.ParameterName)
                Dim da = New SqlClient.SqlDataAdapter(sql, cnn)
                Dim resultTable = New DataTable()

                da.Fill(resultTable)

                sender.DataSource = resultTable

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("LoadList", ex)
            Finally
                If (cnn IsNot Nothing) Then cnn.Close()
            End Try
        End Sub

#End Region

#Region "Métodos"

        Private Sub CargarCategorias()
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing

            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                Dim categoriasTable2 = dbmCore.SchemaConfig.CTA_Categoria_Reportes.DBFindByfk_Entidad(Program.Sesion.Entidad.id, 0, New DBCore.SchemaConfig.CTA_Categoria_ReportesEnumList(DBCore.SchemaConfig.CTA_Categoria_ReportesEnum.Nombre_Categoria_Reporte, True))

                Dim categoriasTable = dbmCore.SchemaConfig.CTA_Categoria_Reportes_2.DBFindByfk_Usuario(Program.Sesion.Usuario.id, 0, New DBCore.SchemaConfig.CTA_Categoria_Reportes_2EnumList(DBCore.SchemaConfig.CTA_Categoria_Reportes_2Enum.Nombre_Categoria_Reporte, True))
             
                For Each categoria In categoriasTable
                    Dim newNode = ReportesTreeView.Nodes.Add("C-" & categoria.Id_Categoria_Reporte, categoria.Nombre_Categoria_Reporte, IconoReportes.Categoria, IconoReportes.Categoria)
                    newNode.Tag = categoria.Id_Categoria_Reporte

                    ReportesTreeView.CollapseAll()
                Next

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargarReportes", ex)
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            End Try
        End Sub

        Private Sub CargarReportes(ByVal nCategoriaNode As TreeNode)
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing

            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                nombreReporteLabel.Text = ""

                Dim idCategoria = CShort(nCategoriaNode.Tag)

                nCategoriaNode.Nodes.Clear()

                Dim reportesTable2 = dbmCore.SchemaConfig.TBL_Reporte.DBFindByfk_Categoria_Reporte(idCategoria, 0, New DBCore.SchemaConfig.TBL_ReporteEnumList(DBCore.SchemaConfig.TBL_ReporteEnum.Nombre_Reporte, True))

                Dim reportesTable = dbmCore.SchemaConfig.CTA_Reportes_Por_RolesPermiso_Usuario_Categoria.DBFindByfk_UsuarioId_Categoria_Reporte(Program.Sesion.Usuario.id, idCategoria, 0, New DBCore.SchemaConfig.CTA_Reportes_Por_RolesPermiso_Usuario_CategoriaEnumList(DBCore.SchemaConfig.CTA_Reportes_Por_RolesPermiso_Usuario_CategoriaEnum.Nombre_Reporte, True))

                'Dim reportesTable = dbmCore.SchemaConfig.TBL_Reporte.DBFindByfk_Categoria_Reporte(idCategoria, 0, New DBCore.SchemaConfig.TBL_ReporteEnumList(DBCore.SchemaConfig.TBL_ReporteEnum.Nombre_Reporte, True))

                For Each reporte In reportesTable
                    Dim newNode = nCategoriaNode.Nodes.Add("R-" & reporte.Id_Reporte, reporte.Nombre_Reporte, IconoReportes.Reporte, IconoReportes.Reporte)
                    newNode.Tag = reporte
                Next
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CrearItemsReporte", ex)
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            End Try
        End Sub

        Private Sub CargarParametros()
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing

            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                ' Recuperar la cadena de conexión
                Dim conexionTable = dbmCore.SchemaConfig.TBL_Conexion.DBGet(Me._selectedReport.fk_Entidad, Me._selectedReport.fk_Conexion)

                _connectionString = conexionTable(0).Cadena_Conexion
                _basicConnectionString = GetBasicConnectionString(_connectionString)

                Dim parametrosTable = dbmCore.SchemaConfig.TBL_Reporte_Parametro.DBGet(Nothing, Me._selectedReport.Id_Reporte)

                camposPanel.Controls.Clear()

                Dim tableLayoutPanelControles As New TableLayoutPanel()
                tableLayoutPanelControles.Name = "controlesTableLayoutPanel"
                tableLayoutPanelControles.ColumnCount = 2
                tableLayoutPanelControles.RowCount = parametrosTable.Count

                Dim i As Integer = 0
                For Each parametro In parametrosTable
                    'Crea el Label del campo
                    Dim parametroLabel As New Label
                    parametroLabel.Name = "lbl_" & parametro.Nombre_Parametro
                    parametroLabel.Text = parametro.Etiqueta_Parametro
                    parametroLabel.AutoSize = False
                    parametroLabel.Dock = DockStyle.Fill
                    parametroLabel.TextAlign = ContentAlignment.TopLeft

                    Dim parameter As IParameter

                    'Crea la caja de texto
                    Select Case parametro.fk_Tipo_Parametro
                        Case DesktopConfig.CampoTipo.Texto
                            parameter = New Controls.ParameterText()

                        Case DesktopConfig.CampoTipo.Numerico
                            parameter = New Controls.ParameterNumeric()

                        Case DesktopConfig.CampoTipo.Fecha
                            parameter = New Controls.ParameterDate()

                        Case DesktopConfig.CampoTipo.SiNo
                            parameter = New Controls.ParameterCheck()

                        Case DesktopConfig.CampoTipo.Lista
                            parameter = New Controls.ParameterList()

                            If (parametro.IsConsulta_ListaNull()) Then
                                Throw New Exception("El parámetro de tipo lista: " & parametro.Nombre_Parametro & ", no se ecuentra configurado")
                            End If

                            Dim lista = CType(parameter, Controls.ParameterList)

                            lista.Query = parametro.Consulta_Lista
                            lista.DisplayMember = parametro.Columna_Etiqueta_Lista
                            lista.ValueMember = parametro.Columna_Valor_Lista

                            AddHandler lista.LoadList, AddressOf LoadList

                        Case Else
                            Throw New Exception("Tipo de parámetro no válido: " & parametro.Nombre_Parametro)

                    End Select

                    parameter.ParameterName = parametro.Nombre_Parametro

                    Dim parameterControl = CType(parameter, UserControl)
                    parameterControl.AutoSizeMode = AutoSizeMode.GrowAndShrink
                    parameterControl.Dock = DockStyle.Fill

                    tableLayoutPanelControles.Controls.Add(parametroLabel, 0, i)
                    tableLayoutPanelControles.Controls.Add(parameterControl, 1, i)

                    i += 1
                Next

                tableLayoutPanelControles.Dock = DockStyle.Fill
                tableLayoutPanelControles.AutoSizeMode = AutoSizeMode.GrowAndShrink
                tableLayoutPanelControles.AutoScroll = True
                camposPanel.Controls.Add(tableLayoutPanelControles)

                Dim TipoSalidaReporteDataTable As DBCore.SchemaConfig.TBL_Reporte_Tipo_SalidaDataTable

                TipoSalidaReporteDataTable = dbmCore.SchemaConfig.TBL_Reporte_Tipo_Salida.DBFindByid_Reporte_Tipo_Salida(_selectedReport.fk_Reporte_Tipo_Salida)

                If TipoSalidaReporteDataTable.Rows.Count > 0 Then
                    _SelectedTipoSalidaReporte = CType(TipoSalidaReporteDataTable.Rows(0), DBCore.SchemaConfig.TBL_Reporte_Tipo_SalidaRow)
                Else
                    _SelectedTipoSalidaReporte = Nothing
                End If



            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CrearCampos", ex)
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            End Try
        End Sub

        Private Sub EjecutarSentencia()
            If (_selectedReport IsNot Nothing) Then
                Dim dataFile As DataTable = Nothing
                Dim titulosDataTable As DBCore.SchemaConfig.TBL_Reporte_SalidaDataTable
                Dim columnasDataTable As DBCore.SchemaConfig.TBL_Reporte_ColumnaDataTable = Nothing
                Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing

                Try
                    dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                    dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                    ' Cargar los titulos de los resultados
                    titulosDataTable = dbmCore.SchemaConfig.TBL_Reporte_Salida.DBGet(Me._selectedReport.Id_Reporte, Nothing)

                    If (_selectedReport.Usa_Archivo AndAlso Me._selectedReport.Usa_Columnas_Ancho_Fijo) Then
                        columnasDataTable = dbmCore.SchemaConfig.TBL_Reporte_Columna.DBGet(Me._selectedReport.Id_Reporte, Nothing)
                    End If
                Catch ex As Exception
                    DesktopMessageBoxControl.DesktopMessageShow("Error al conectarse a Core", ex)
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
                    Dim cSql = ReemplazarParametros(Me._selectedReport.Consulta, "")


                    ' Funcionalidad para cargar informacion a una tabla temporal
                    If (_selectedReport.Usa_Archivo) Then
                        If (dataBaseType <> dataBaseType.SqlServer) Then
                            Throw New Exception("La opción [Usa Archivo] solo es compatible con bases de datos SQL Server")
                        End If

                        Dim nFileName As String = ""

                        dataFile = CargarFile(Me._selectedReport.Usa_Columnas_Ancho_Fijo, columnasDataTable, nFileName)
                        nFileName = "'" & Path.GetFileName(nFileName) & "'"
                        cSql = cSql.ToString().Replace("$FileName", nFileName)

                        If (dataFile Is Nothing) Then Return
                    End If

                    'Logica exclusiva para la funcionalidad de custodia de cajas
                    If ReportesTreeView.SelectedNode.Name = "2" Then
                        cSql = cSql.ToString().Replace("@Sede", Program.DesktopGlobal.PuestoTrabajoRow.fk_Sede.ToString)
                        cSql = cSql.ToString().Replace("@Boveda", Program.DesktopGlobal.BovedaRow.id_Boveda.ToString)
                    End If

                    'Ejecuta la sentencia
                    Try
                        If (Me._selectedReport.Usa_Archivo) Then
                            BulkInsert.InsertDataTableReport(dataFile, dbmReporte, "#Report")
                        End If
                    Catch ex As Exception
                        Throw New Exception("Error al cargar el archivo a la base de datos, la configuración es diferente al esquema del archivo seleccionado." & ex.Message)
                    End Try

                    ' Consulta de confirmación
                    If (Me._selectedReport.Usa_Consulta_Confirmacion) Then
                        salto = ConsultaConfirmacion(dbmReporte)
                    End If

                    ' Consulta usa exportar masivo
                    If Not (_selectedReport.Usa_Exportar_Masivo) Then
                        ExportarMasivoButton.Visible = False
                        If (salto) Then Return

                        resultadosPanel.Controls.Clear()

                        If (Me._selectedReport.Exportar_Texto_Plano) Then
                            LeerDataReader(dbmReporte, dataBaseType, cSql)
                        Else
                            LeerDataSet(dbmReporte, dataBaseType, titulosDataTable, cSql)
                        End If
                    Else
                        ExportarMasivoButton.Visible = True
                        LeerDataSetUsaMasivo(dbmReporte, dataBaseType, titulosDataTable, cSql)
                    End If

                Catch ex As Exception
                    DesktopMessageBoxControl.DesktopMessageShow("EjecutarSentencia", ex)
                    dbmReporte.Transaction_Rollback()
                Finally
                    If (dbmReporte IsNot Nothing) Then dbmReporte.Connection_Close()
                End Try
            Else
                DesktopMessageBoxControl.DesktopMessageShow("Se debe seleccionar un reporte para obtener los resultados.", "Selección de Reporte", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
            End If
        End Sub

        Private Sub LeerDataSetUsaMasivo(dbmReporte As DataBaseManager, dataBaseType As DataBaseType, titulosDataTable As DBCore.SchemaConfig.TBL_Reporte_SalidaDataTable, cSql As String)
            Dim resultados As DataSet = Nothing
            Try
                resultados = SqlData.ExecuteQuery(cSql, dbmReporte, dataBaseType)
            Catch ex As Exception
                Throw
            End Try

            If resultados.Tables.Count > 0 Then
                For i = resultados.Tables.Count To 1 Step -1
                    Dim tabla As DataTable

                    tabla = resultados.Tables(i - 1)
                    If tabla.Rows.Count() > 0 Then
                        Dim nuevaGrilla = New DesktopReportDataGridViewControl
                        nuevaGrilla.Conection_String_Core = Program.DesktopGlobal.ConnectionStrings.Core
                        nuevaGrilla.Conection_String_Tools = Program.DesktopGlobal.ConnectionStrings.Tools


                        If resultados.Tables.Count = 1 Then
                            nuevaGrilla.Dock = DockStyle.Fill
                        Else
                            nuevaGrilla.Dock = DockStyle.Top
                        End If
                        Dim index As Short
                        index = CShort(resultados.Tables.IndexOf(tabla) + 1)

                        Dim titulo = titulosDataTable.Select("Id_Reporte_Salida = '" & tabla.Rows.Item(0).ItemArray(0).ToString() & "'")

                        nuevaGrilla.Titulo = tabla.Rows.Item(0).ItemArray(0).ToString()
                        nuevaGrilla.Id_Reporte = Me._selectedReport.Id_Reporte
                        If Not Me._selectedReport Is Nothing Then
                            nuevaGrilla.Salto_Linea = Me._SelectedTipoSalidaReporte.Codigo_Salto_Linea
                        Else
                            nuevaGrilla.Salto_Linea = ""
                        End If

                        If Not _selectedReport.Muestra_Columna_Nombre_Reporte_Masivo Then
                            Dim tabla2 As DataTable = Nothing
                            tabla2 = tabla.Copy
                            tabla2.Columns.Remove(tabla2.Columns(0))
                            tabla2.AcceptChanges()

                            nuevaGrilla.InternalGridView.DataSource = tabla2
                        Else
                            nuevaGrilla.InternalGridView.DataSource = tabla
                        End If

                        nuevaGrilla.ButtonOnClickReportMas_ = False
                        resultadosPanel.Controls.Add(nuevaGrilla)

                    Else
                        ExportarMasivoButton.Visible = False
                        DesktopMessageBoxControl.DesktopMessageShow("No hay registros para la fecha seleccionada.", "Selección de Reporte", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                    End If

                Next
            Else
                ExportarMasivoButton.Visible = False
                DesktopMessageBoxControl.DesktopMessageShow("No hay registros para la fecha seleccionada.", "Selección de Reporte", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
            End If


        End Sub

        Private Sub LeerDataSet(dbmReporte As DataBaseManager, dataBaseType As DataBaseType, titulosDataTable As DBCore.SchemaConfig.TBL_Reporte_SalidaDataTable, cSql As String)
            Dim resultados As DataSet = Nothing
            Try
                'dbmReporte.Transaction_Begin()

                'resultados.Tables.Add(dbmReporte.DataBase.ExecuteQueryGet(cSql))
                resultados = SqlData.ExecuteQuery(cSql, dbmReporte, dataBaseType)

                'dbmReporte.Transaction_Commit()
            Catch ex As Exception
                'dbmReporte.Transaction_Rollback()
                Throw
            End Try

            For i = resultados.Tables.Count To 1 Step -1
                Dim tabla As DataTable

                tabla = resultados.Tables(i - 1)
                Dim nuevaGrilla = New DesktopReportDataGridViewControl
                nuevaGrilla.Conection_String_Core = Program.DesktopGlobal.ConnectionStrings.Core
                nuevaGrilla.Conection_String_Tools = Program.DesktopGlobal.ConnectionStrings.Tools


                If resultados.Tables.Count = 1 Then
                    nuevaGrilla.Dock = DockStyle.Fill
                Else
                    nuevaGrilla.Dock = DockStyle.Top
                End If
                Dim index As Short
                index = CShort(resultados.Tables.IndexOf(tabla) + 1)

                Dim titulo = titulosDataTable.Select("Id_Reporte_Salida = '" & index & "'")
                If (titulo.Length > 0) Then
                    Dim tituloRow = CType(titulo(0), DBCore.SchemaConfig.TBL_Reporte_SalidaRow)
                    nuevaGrilla.Titulo = FormatearNombreSalidaArchivo(tituloRow.Titulo_Salida)
                Else
                    nuevaGrilla.Titulo = "Tabla - " + CStr(index)
                End If

                nuevaGrilla.Id_Reporte = Me._selectedReport.Id_Reporte
                If Not Me._selectedReport Is Nothing Then
                    nuevaGrilla.Salto_Linea = Me._SelectedTipoSalidaReporte.Codigo_Salto_Linea
                Else
                    nuevaGrilla.Salto_Linea = ""
                End If

                nuevaGrilla.InternalGridView.DataSource = tabla
                resultadosPanel.Controls.Add(nuevaGrilla)
            Next
        End Sub

        Private Sub LeerDataReader(dbmReporte As DataBaseManager, dataBaseType As DataBaseType, cSql As String)
            Dim selector = New SaveFileDialog()
            selector.Filter = "Archivo de texto plano delimitado (*.csv)|*.csv"

            Dim respuesta = selector.ShowDialog()

            If (respuesta <> DialogResult.OK) Then Return

            Dim fileName = selector.FileName

            dbmReporte.Transaction_Begin()

            Dim resultados = SqlData.ExecuteReader(cSql, dbmReporte, dataBaseType)

            Using archivo = New StreamWriter(fileName)
                Dim columnas = resultados.GetSchemaTable()

                ' Crear los encabezados
                For col = 0 To columnas.Rows.Count - 1
                    If (col > 0) Then archivo.Write(vbTab)
                    archivo.Write(columnas.Rows(col)("ColumnName").ToString())
                Next
                archivo.WriteLine()

                ' Escribir la data
                While (resultados.Read())
                    For col = 0 To columnas.Rows.Count - 1
                        If (col > 0) Then archivo.Write(vbTab)
                        archivo.Write(resultados.GetString(col))
                    Next
                    archivo.WriteLine()
                End While
            End Using

            resultados.Close()

            dbmReporte.Transaction_Commit()

            MessageBox.Show("El reporte se exportó exitosamente", "Reportes", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Sub

        Private Function CargarFile(nUsaColumnasAnchoFijo As Boolean, nColumnas As DBCore.SchemaConfig.TBL_Reporte_ColumnaDataTable, ByRef nFileName As String) As DataTable
            Try
                Dim archivoCargue As New OpenFileDialog()
                archivoCargue.Title = "Por favor seleccione el archivo a cargar"
                archivoCargue.Filter = "Archivos de texto plano|*.csv; *.txt; *.*"

                If (archivoCargue.ShowDialog() = DialogResult.OK) Then
                    Dim csvData As New DBCore.CSVData() 'Slyg.Tools.CSV.CSVData()
                    Dim caracter As Char

                    csvData.LinesToJump = Me._selectedReport.Filas_Omitir

                    If (nUsaColumnasAnchoFijo) Then
                        If (nColumnas.Count = 0) Then
                            Throw New Exception("No se encuentra la configuración para el manejo del archivo, falta la definición de columnas, por favor comuniquese con el administrador para que la configure.")
                        End If

                        Dim definiciones = New List(Of DBCore.CSVLoadColumnDefinition)

                        For Each column In nColumnas
                            definiciones.Add(New DBCore.CSVLoadColumnDefinition(column.Nombre_Columna, column.Inicio_Columna, column.Longitud_Columna))
                        Next

                        csvData.HasHeader = Me._selectedReport.Maneja_Encabezado
                        csvData.LoadCSV(archivoCargue.FileName, definiciones)
                    Else
                        If (Me._selectedReport.IsCaracter_SeparadoNull() Or Me._selectedReport.IsIdentificador_TextoNull()) Then
                            Throw New Exception("No se encuentra la configuración para el manejo del archivo, por favor comuniquese con el administrador para que la configure.")
                        End If

                        Select Case Me._selectedReport.Caracter_Separado
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

                        csvData.LoadCSV(archivoCargue.FileName, Me._selectedReport.Maneja_Encabezado, caracter, Me._selectedReport.Identificador_Texto)
                    End If

                    nFileName = archivoCargue.FileName

                    Return csvData.DataTable.ToDataTable()
                Else
                    Return Nothing
                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Cargar Archivo", ex)
                Return Nothing
            End Try
        End Function

        Private Function ReemplazarParametros(nSql As String, nToIgnore As String) As String
            Dim WordCancelados As String = Mid(nSql, 1, 46)
            Dim ProcesoCancelados As Boolean

            If (WordCancelados = "EXEC [Report].[PA_Ejecutar_Cancelados_Informe]") Then
                ProcesoCancelados = True
            End If

            If ProcesoCancelados = True Then
                nSql = nSql.ToString().Replace("@UsuarioAccion", "'" + Program.Sesion.Usuario.Login + "'")
            End If

            For Each parameterControl As Control In camposPanel.Controls("ControlesTableLayoutPanel").Controls
                If (parameterControl.GetType().GetInterfaces().Contains(GetType(IParameter))) Then
                    Dim parameter = CType(parameterControl, IParameter)

                    If (parameter.ParameterName <> nToIgnore) Then
                        nSql = nSql.ToString().Replace(parameter.ParameterName, parameter.GetStringParameter())
                    End If
                End If
            Next
            Return nSql
        End Function

        Private Function ConsultaConfirmacion(dbmReporte As DataBaseManager) As Boolean
            If (Not Me._selectedReport.IsConsulta_ConfirmacionNull() AndAlso Me._selectedReport.Consulta_Confirmacion <> "") Then
                Dim consultaConfirmacionForm As New FormConfirmacionReporte(Program.DesktopGlobal.ConnectionStrings.Core, Program.DesktopGlobal.ConnectionStrings.Tools)

                Dim cSqlC = ReemplazarParametros(Me._selectedReport.Consulta_Confirmacion, "")

                consultaConfirmacionForm.Consulta = cSqlC
                consultaConfirmacionForm.Conexion = dbmReporte

                If (Not consultaConfirmacionForm.ShowDialog = DialogResult.OK) Then
                    Return True
                ElseIf (consultaConfirmacionForm.ResultadosDataGridView.InternalGridView.RowCount = 0) Then
                    DesktopMessageBoxControl.DesktopMessageShow("Se enco tro algún error en el archivo, no se puede continuar", "Consulta Confirmación", DesktopMessageBoxControl.IconEnum.AdvertencyIcon)
                    Return True
                End If
            Else
                DesktopMessageBoxControl.DesktopMessageShow("Aunque esta configurado para que haya consulta de confirmación, no hay ninguna consulta para evaluar. Por favor contacte a su administrador para que se configure correctamente.", "Consulta Confirmación", DesktopMessageBoxControl.IconEnum.AdvertencyIcon)
                Return True
            End If

            Return False
        End Function

#End Region

#Region "Funciones"

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

        Private Function FormatearNombreSalidaArchivo(FileName As String) As String

            Dim result As String = ""
            Dim Fecha As Date = Date.Now

            Dim strArr() As String
            Dim count As Integer

            strArr = FileName.Split(CChar("#"))
            For count = 0 To strArr.Length - 1
                Select Case strArr(count)
                    Case "yMMdd"
                        result = result & strArr(count).ToString.Replace(strArr(count), Fecha.ToString("yMMdd").ToString)
                    Case "yyMMdd"
                        result = result & strArr(count).ToString.Replace(strArr(count), Fecha.ToString("yyMMdd").ToString)
                    Case "hhmmss"
                        result = result & strArr(count).ToString.Replace(strArr(count), Fecha.ToString("HHmmss").ToString)
                    Case "yyyy/MM/dd"
                        result = result & strArr(count).ToString.Replace(strArr(count), Fecha.ToString("yyyy/MM/dd").ToString)
                    Case "dd/MM/yyyy"
                        result = result & strArr(count).ToString.Replace(strArr(count), Fecha.ToString("dd/MM/yyyy").ToString)
                    Case "yyyyMMdd"
                        result = result & strArr(count).ToString.Replace(strArr(count), Fecha.ToString("yyyyMMdd").ToString)
                    Case Else
                        result = result & strArr(count)
                End Select
            Next
            Return result

        End Function

#End Region

    End Class

End Namespace