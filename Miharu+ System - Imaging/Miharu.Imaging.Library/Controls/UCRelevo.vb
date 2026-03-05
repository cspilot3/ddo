Imports System.Drawing
Imports System.IO
Imports System.Linq
Imports System.Linq.Expressions
Imports System.Text.RegularExpressions
Imports System.Web.UI.WebControls.Expressions
Imports DBCore
Imports DBImaging
Imports Microsoft.Reporting.WinForms
Imports Miharu.Desktop.Controls
Imports Miharu.Desktop.Controls.DesktopReportViewer
Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Library.MiharuDMZ
Imports RestSharp.Extensions
Imports RestSharp.Validation

Namespace Controls
    Public Class UCRelevo

#Region " Declaraciones "
        Public Workspace As FormImagingWorkSpace

        Private regexOnceDigitosDespuesDe8020 As String = "(?<=8020)\d{11}"
        Private regexSoloOnceDigitos As String = "^\d{11}$"

        Private _referenciaEncontradaDataTable As DBImaging.SchemaProcess.CTA_Get_Registros_RelevoDataTable
        Private _registrosDataTable As DBImaging.SchemaProcess.CTA_Get_Registros_RelevoDataTable
        Private _registrosTotalDataTable As DBImaging.SchemaProcess.CTA_Get_Registros_Relevo_TotalDataTable

        Private WorkSpaceImagingReportViewerControl As DesktopReportViewer1Control

        Private lastTabPage As TabPage
        Public Ingreso As Boolean = True

#End Region

#Region " Constructores "

        Public Sub New()
            InitializeComponent()
            _referenciaEncontradaDataTable = New DBImaging.SchemaProcess.CTA_Get_Registros_RelevoDataTable()
            _registrosDataTable = New DBImaging.SchemaProcess.CTA_Get_Registros_RelevoDataTable()

            WorkSpaceImagingReportViewerControl = New DesktopReportViewer1Control()
        End Sub

#End Region

#Region " Eventos "
        Private Sub UCRelevo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            'If Not Me.DesignMode Then
            '    cargarFechasRecaudo()
            '    cargarCiald()
            'End If
            InicializateControls()

            Dim parentTabControl As TabControl = FindParentTabControl(Me)
            If parentTabControl IsNot Nothing Then
                AddHandler parentTabControl.Deselecting, AddressOf TabControl_Deselecting
                lastTabPage = parentTabControl.SelectedTab ' Inicializar con la pestaña actual
            End If
        End Sub

        'Private Sub cbxCiald_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbxCiald.SelectedIndexChanged
        '    cargarOficinas()
        'End Sub

        Private Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click

            If cbxFechaRecaudo.SelectedValue IsNot Nothing Then
                ObtenerRegistrosRelevo()
                _referenciaEncontradaDataTable = New DBImaging.SchemaProcess.CTA_Get_Registros_RelevoDataTable()
                MostrarRegistrosEnGrilla()

                ObtenerRegistrosRelevoTotal()
                MostrarRegistrosEnGrillaTotal()

                HabilitarBotonGenerarActaSiHayRegistros()
            Else
                MsgBox("No hay fechas de recaudo disponibles para seleccionar. Por favor, verifique la configuración del sistema o comuníquese con el área encargada.", MsgBoxStyle.Information, "Reporte Acta.")
            End If
        End Sub

        Private Sub LoginReferenciaTextBox_KeyDown(sender As Object, e As KeyEventArgs) Handles LoginReferenciaTextBox.KeyDown

            If e.KeyCode = Keys.Enter Then
                e.SuppressKeyPress = True
                ProcesarReferenciaIngresada(LoginReferenciaTextBox.Text.Trim())
            End If

        End Sub

        Private Sub BtnGenerarActa_Click(sender As Object, e As EventArgs) Handles BtnGenerarActa.Click
            Dim reporte As New DesktopReportViewer1Control
            Dim CodigoCiald = CInt(Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede)
            Dim NombreCiald As String = Program.DesktopGlobal.CentroProcesamientoRow.Nombre_Sede

            Dim queryResponseIni As QueryResponse = ClientUtil.resolver("[DB_Miharu.Imaging_Core].[Process].[PA_Get_Registros_Relevo_Acta]", New List(Of QueryParameter) From {
                    New QueryParameter With {.name = "fk_Entidad", .value = Program.ImagingGlobal.Entidad.ToString()},
                    New QueryParameter With {.name = "fk_Proyecto", .value = Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto.ToString()},
                    New QueryParameter With {.name = "fk_Fecha_Recaudo", .value = cbxFechaRecaudo.SelectedValue.ToString()},
                    New QueryParameter With {.name = "Ciald", .value = CodigoCiald.ToString()},
                    New QueryParameter With {.name = "fk_Serie_Documental", .value = CInt("1").ToString()},
                    New QueryParameter With {.name = "Estado_CialdLocal", .value = CInt(EstadoEnum.Relevo_Ciald_Local).ToString()},
                    New QueryParameter With {.name = "fk_Usuario", .value = Program.Sesion.Usuario.id.ToString()}
                    }, QueryRequestType.StoredProcedure, QueryResponseType.Table)

            If queryResponseIni.dataTable Is Nothing OrElse Not queryResponseIni.dataTable.Rows.Count > 0 Then
                MsgBox("No existen registros para generar este reporte.. Verifique.", MsgBoxStyle.Information, "Reporte Acta.")
                Exit Sub
            End If

            Dim objReport As New Report_Acta_CialdLocal(reporte) With {
                .DTActas = queryResponseIni.dataTable,
                .CodigoCiald = CodigoCiald,
                .NombreCiald = NombreCiald}

            objReport.Launch(CInt(cbxFechaRecaudo.SelectedValue))

            ' Crear una ventana emergente para mostrarlo
            Dim popup As New Form()
            popup.Text = "Vista previa de Acta Ciald"
            popup.Size = New Size(1024, 768)
            popup.StartPosition = FormStartPosition.CenterScreen
            popup.FormBorderStyle = FormBorderStyle.SizableToolWindow ' Opcional
            popup.ShowIcon = False

            ' Mostrar el control dentro del formulario
            reporte.Dock = DockStyle.Fill
            popup.Controls.Add(reporte)

            ' Mostrar como ventana emergente
            popup.ShowDialog()

            cargarFechasRecaudo()
            DGVRelevo.Rows.Clear()
            DGVRelevoTotal.Rows.Clear()

        End Sub

#End Region

#Region " Metódos "

        Private Sub TabControl_Deselecting(sender As Object, e As TabControlCancelEventArgs)
            If Not ShouldAllowTabChange(sender, CType(sender, TabControl).SelectedTab) Then
                e.Cancel = True
            Else
                InicializateControls()
            End If
        End Sub

        Private Function ShouldAllowTabChange(sender As Object, nextTab As TabPage) As Boolean
            Dim tabControl As TabControl = CType(sender, TabControl)
            Dim currentTab As TabPage = tabControl.SelectedTab

            If currentTab.Controls.Contains(Me) Then

                Dim respuesta As DialogResult = MessageBox.Show(
                                "Está a punto de salir de esta página." & Environment.NewLine &
                                "Si tiene información pendiente, por favor guárdela antes de continuar." & Environment.NewLine &
                                "¿Desea salir de esta página?",
                                "Confirmación de salida",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

                If respuesta = DialogResult.No Then
                    Return False
                Else
                End If
            End If

            Return True
        End Function

        Private Sub InicializateControls()
            Ingreso = True
            cargarFechasRecaudo()
            cargarCiald()
            Ingreso = False
            If DGVRelevo IsNot Nothing Then
                DGVRelevo.DataSource = Nothing
                DGVRelevo.Rows.Clear()
                DGVRelevo.Refresh()
            End If

            If DGVRelevoTotal IsNot Nothing Then
                DGVRelevoTotal.DataSource = Nothing
                DGVRelevoTotal.Rows.Clear()
                DGVRelevoTotal.Refresh()
            End If
        End Sub

        Private Sub HabilitarBotonGenerarActaSiHayRegistros()
            BtnGenerarActa.Enabled = DGVRelevo.Rows.Count > 0
        End Sub

        Private Sub cargarCiald()

            cbxCiald.DisplayMember = "Nombre_Sede"
            cbxCiald.ValueMember = "id_Sede"

            Dim dtSedes As New DataTable()
            dtSedes.Columns.Add("id_Sede", GetType(Integer))
            dtSedes.Columns.Add("Nombre_Sede", GetType(String))

            dtSedes.Rows.Add(Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede, Program.DesktopGlobal.CentroProcesamientoRow.Nombre_Sede)

            cbxCiald.DataSource = dtSedes
            cbxCiald.SelectedIndex = 0
            cbxCiald.Enabled = False

            cbxCiald.Refresh()
            cargarOficinas()
        End Sub

        Private Sub cargarOficinas()
            Dim oficinaDataTable As DBSecurity.SchemaConfig.CTA_CentrosProcesamiento_ComboBoxDataTable

            Dim queryResponse As QueryResponse = ClientUtil.resolver("[DB_Miharu.Security_Core].Config.CTA_CentrosProcesamiento_ComboBox", New List(Of QueryParameter) From {
                New QueryParameter With {.name = "fk_Entidad", .value = Program.ImagingGlobal.Entidad.ToString()},
                New QueryParameter With {.name = "fk_Sede", .value = cbxCiald.SelectedValue.ToString()}
                }, QueryRequestType.Table, QueryResponseType.Table)

            oficinaDataTable = CType(ClientUtil.mapToTypedTable(New DBSecurity.SchemaConfig.CTA_CentrosProcesamiento_ComboBoxDataTable(), queryResponse.dataTable), DBSecurity.SchemaConfig.CTA_CentrosProcesamiento_ComboBoxDataTable)

            Utilities.Llenarcombo(OficinasDesktopComboBox, oficinaDataTable, DBSecurity.SchemaConfig.CTA_CentrosProcesamiento_ComboBoxEnum.Codigo_Centro.ColumnName, DBSecurity.SchemaConfig.CTA_CentrosProcesamiento_ComboBoxEnum.Centro.ColumnName, True, "-1", "--TODOS--")

            OficinasDesktopComboBox.Refresh()
        End Sub

        Private Sub cargarFechasRecaudo()

            Dim fechasRecaudoDataTable As DBImaging.SchemaProcess.TBL_Fecha_RecaudoDataTable

            Dim queryResponse As QueryResponse = ClientUtil.resolver("[DB_Miharu.Imaging_Core].Process.TBL_Fecha_Recaudo", New List(Of QueryParameter) From {
                New QueryParameter With {.name = "fk_Entidad", .value = Program.ImagingGlobal.Entidad.ToString()},
                New QueryParameter With {.name = "fk_Proyecto", .value = Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto.ToString()},
                New QueryParameter With {.name = "Cerrado", .value = "0".ToString()}
                }, QueryRequestType.Table, QueryResponseType.Table)

            fechasRecaudoDataTable = CType(ClientUtil.mapToTypedTable(New DBImaging.SchemaProcess.TBL_Fecha_RecaudoDataTable(), queryResponse.dataTable), DBImaging.SchemaProcess.TBL_Fecha_RecaudoDataTable)

            cbxFechaRecaudo.DataSource = Nothing
            If fechasRecaudoDataTable.Rows.Count > 0 Then
                cbxFechaRecaudo.Items.Clear()
                cbxFechaRecaudo.DisplayMember = "id_Fecha_Recaudo"
                cbxFechaRecaudo.ValueMember = "id_Fecha_Recaudo"

                cbxFechaRecaudo.DataSource = fechasRecaudoDataTable

                cbxFechaRecaudo.Refresh()
            Else
                MessageBox.Show("No hay fechas de recaudo abiertas", "Relevo", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End Sub

        ''' <summary>
        ''' Procesa la referencia ingresada por el usuario: valida, busca en la grilla,
        ''' actualiza su estado mediante procedimiento almacenado y la agrega al listado de referencias encontradas.
        ''' </summary>
        ''' <param name="texto">Texto ingresado por el usuario que puede contener una referencia válida.</param>
        Private Sub ProcesarReferenciaIngresada(texto As String)

            Try
                Dim referenciaValida = ExtraerReferenciaValidaDesdeTexto(texto)

                If String.IsNullOrEmpty(referenciaValida) Then
                    MostrarMensajeReferenciaNoEncontrada()
                    Exit Sub
                End If

                Dim esRelevado As Boolean
                Dim referenciaEncontradadataTable = BuscarReferenciaEnGrilla(referenciaValida, esRelevado)

                If referenciaEncontradadataTable Is Nothing OrElse referenciaEncontradadataTable.Count = 0 Then

                    If esRelevado = True Then
                        MessageBox.Show("La referencia ingresada ya se encuentra relevada en el sistema. Por favor, verifica que sea correcta.", "Referencia Relevada Previamente", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Exit Sub
                    End If

                    MostrarMensajeReferenciaNoEncontrada()
                    Exit Sub
                End If

                Dim queryResponse As QueryResponse = ClientUtil.resolver("[DB_Miharu.Imaging_Core].[Process].[PA_Update_Registros_Relevo]", New List(Of QueryParameter) From {
                                                        New QueryParameter With {.name = "fk_Entidad", .value = Program.ImagingGlobal.Entidad.ToString()},
                                                        New QueryParameter With {.name = "fk_Proyecto", .value = Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto.ToString()},
                                                        New QueryParameter With {.name = "fk_Fecha_Recaudo", .value = cbxFechaRecaudo.SelectedValue.ToString()},
                                                        New QueryParameter With {.name = "Oficina", .value = OficinasDesktopComboBox.SelectedValue.ToString()},
                                                        New QueryParameter With {.name = "Numero_Referencia", .value = referenciaValida.ToString()},
                                                        New QueryParameter With {.name = "Estado_Relevo_CialLocal", .value = CInt(EstadoEnum.Relevo_Ciald_Local).ToString()},
                                                        New QueryParameter With {.name = "fk_Usuario", .value = Program.Sesion.Usuario.id.ToString()}
                                                        }, QueryRequestType.StoredProcedure, QueryResponseType.Table)

                If queryResponse.dataTable.Rows.Count > 0 Then
                    Dim registrosProcesados As Integer = Convert.ToInt32(queryResponse.dataTable.Rows(0)("RegistrosProcesados"))
                    Dim mensajeResultado As String = queryResponse.dataTable.Rows(0)("Mensaje").ToString()

                    If registrosProcesados > 0 Then
                        AgregarAReferenciasEncontradas(referenciaEncontradadataTable)
                        MessageBox.Show("Se realizo el cruce de la referencia.", "Referencia encontrada", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        LoginReferenciaTextBox.Text = ""
                        LoginReferenciaTextBox.Focus()
                    ElseIf Not String.IsNullOrWhiteSpace(mensajeResultado) Then
                        Throw New Exception("No se pudo procesar la referencia")
                    End If
                Else
                    MostrarMensajeReferenciaNoEncontrada()
                    Exit Sub
                End If

                MostrarRegistrosEnGrilla()

            Catch ex As Exception
                MessageBox.Show("Error al relevar la referencia, por favor comunicarse con el administrador", "Error Relevando Referencia", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        ''' <summary>
        ''' Copia los registros validados desde una tabla temporal a la tabla de referencias encontradas.
        ''' </summary>
        Private Sub AgregarAReferenciasEncontradas(tablaTemporal As DBImaging.SchemaProcess.CTA_Get_Registros_RelevoDataTable)

            ' Eliminar referencias del DataTable principal
            EliminarReferenciasEncontradas(tablaTemporal)

            For Each fila As DBImaging.SchemaProcess.CTA_Get_Registros_RelevoRow In tablaTemporal
                Dim nuevaFila = _referenciaEncontradaDataTable.NewCTA_Get_Registros_RelevoRow()

                nuevaFila.NumeroReferencia = fila.NumeroReferencia
                nuevaFila.TipoMovimiento = fila.TipoMovimiento
                nuevaFila.Ciald = fila.Ciald
                nuevaFila.NombreOficina = fila.NombreOficina
                nuevaFila.CodigoOficinaDian = fila.CodigoOficinaDian
                nuevaFila.CodigoOficinaBanco = fila.CodigoOficinaBanco
                nuevaFila.Estado = fila.Estado

                _referenciaEncontradaDataTable.AddCTA_Get_Registros_RelevoRow(nuevaFila)
            Next
        End Sub

        ''' <summary>
        ''' Elimina del DataTable principal las referencias que ya fueron encontradas.
        ''' </summary>
        Private Sub EliminarReferenciasEncontradas(tablaTemporal As DBImaging.SchemaProcess.CTA_Get_Registros_RelevoDataTable)
            Dim referenciasAMover = tablaTemporal.Select(Function(f) f.NumeroReferencia).ToHashSet()

            For i As Integer = _registrosDataTable.Rows.Count - 1 To 0 Step -1
                Dim fila = _registrosDataTable(i)
                If referenciasAMover.Contains(fila.NumeroReferencia) Then
                    _registrosDataTable.Rows.RemoveAt(i)
                End If
            Next
        End Sub

        ''' <summary>
        ''' Busca en la grilla una fila que coincida con el número de referencia especificado
        ''' y devuelve una tabla con los registros encontrados.
        ''' </summary>
        ''' <param name="referenciaValida">Referencia a buscar en la columna "NumeroReferencia" del DataGridView.</param>
        ''' <returns>Tabla con los registros que coinciden con la referencia.</returns>
        Private Function BuscarReferenciaEnGrilla(referenciaValida As String, ByRef esRelevado As Boolean) As DBImaging.SchemaProcess.CTA_Get_Registros_RelevoDataTable
            Dim resultado = New DBImaging.SchemaProcess.CTA_Get_Registros_RelevoDataTable()

            ' Buscar coincidencia en la columna "Numero de Referencia"
            For Each fila As DataGridViewRow In DGVRelevo.Rows

                If Not fila.IsNewRow AndAlso fila.Cells("NumeroReferencia").Value?.ToString().Trim() = referenciaValida Then

                    Dim estado As String = fila.Cells("Estado").Value?.ToString().Trim().ToUpperInvariant()

                    If estado.ToUpper() <> EstadoProcesoRelevo.RelevoCialdLocal.ToUpper() Then

                        esRelevado = False
                        resultado.AddCTA_Get_Registros_RelevoRow(CLng(fila.Cells("NumeroReferencia").Value),
                                            fila.Cells("TipoMovimiento").Value?.ToString(),
                                            fila.Cells("Ciald").Value?.ToString(),
                                            fila.Cells("NombreOficina").Value?.ToString(),
                                            fila.Cells("CodigoOficinaDian").Value?.ToString(),
                                            fila.Cells("CodigoOficinaBanco").Value?.ToString(),
                                            EstadoProcesoRelevo.RelevoCialdLocal
                                        )
                    Else
                        esRelevado = True
                    End If

                End If
            Next

            Return resultado
        End Function

        ''' <summary>
        ''' Consulta los registros de relevo desde la base de datos usando parámetros seleccionados 
        ''' (entidad, proyecto, fecha de recaudo, oficina y serie documental), y carga el resultado en _registrosDataTable.
        ''' </summary>
        Private Sub ObtenerRegistrosRelevo()


            Dim queryResponse As QueryResponse = ClientUtil.resolver("[DB_Miharu.Imaging_Core].Process.PA_Get_Registros_Relevo", New List(Of QueryParameter) From {
                New QueryParameter With {.name = "fk_Entidad", .value = Program.ImagingGlobal.Entidad.ToString()},
                New QueryParameter With {.name = "fk_Proyecto", .value = Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto.ToString()},
                New QueryParameter With {.name = "fk_Fecha_Recaudo", .value = cbxFechaRecaudo.SelectedValue.ToString()},
                New QueryParameter With {.name = "Ciald", .value = cbxCiald.SelectedValue.ToString()},
                New QueryParameter With {.name = "Oficina", .value = OficinasDesktopComboBox.SelectedValue.ToString()},
                New QueryParameter With {.name = "fk_Serie_Documental", .value = "1".ToString()},
                New QueryParameter With {.name = "Estado_CialdLocal", .value = CInt(EstadoEnum.Relevo_Ciald_Local).ToString()}
                }, QueryRequestType.StoredProcedure, QueryResponseType.Table)

            _registrosDataTable = CType(ClientUtil.mapToTypedTable(New DBImaging.SchemaProcess.CTA_Get_Registros_RelevoDataTable(), queryResponse.dataTable), DBImaging.SchemaProcess.CTA_Get_Registros_RelevoDataTable)
        End Sub

        Private Sub ObtenerRegistrosRelevoTotal()
            Dim queryResponse As QueryResponse = ClientUtil.resolver("[DB_Miharu.Imaging_Core].Process.PA_Get_Registros_Relevo_Total", New List(Of QueryParameter) From {
                New QueryParameter With {.name = "fk_Entidad", .value = Program.ImagingGlobal.Entidad.ToString()},
                New QueryParameter With {.name = "fk_Proyecto", .value = Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto.ToString()},
                New QueryParameter With {.name = "fk_Fecha_Recaudo", .value = cbxFechaRecaudo.SelectedValue.ToString()},
                New QueryParameter With {.name = "Ciald", .value = cbxCiald.SelectedValue.ToString()},
                New QueryParameter With {.name = "Oficina", .value = OficinasDesktopComboBox.SelectedValue.ToString()},
                New QueryParameter With {.name = "fk_Serie_Documental", .value = "1".ToString()},
                New QueryParameter With {.name = "Estado_CialdLocal", .value = CInt(EstadoEnum.Relevo_Ciald_Local).ToString()}
                }, QueryRequestType.StoredProcedure, QueryResponseType.Table)

            _registrosTotalDataTable = CType(ClientUtil.mapToTypedTable(New DBImaging.SchemaProcess.CTA_Get_Registros_Relevo_TotalDataTable(), queryResponse.dataTable), DBImaging.SchemaProcess.CTA_Get_Registros_Relevo_TotalDataTable)
        End Sub

        ''' <summary>
        ''' Limpia y actualiza el contenido del DataGridView con los registros obtenidos. 
        ''' Muestra los registros en color según su origen y notifica si no hay datos para mostrar.
        ''' </summary>
        Private Sub MostrarRegistrosEnGrilla()

            DGVRelevo.Rows.Clear()

            If _registrosDataTable.Rows.Count > 0 Then
                Dim registrosDataTableOrdenada = ObtenerRegistrosOrdenadosPorEstado(_registrosDataTable)
                AgregarFilasAlDataGridView(registrosDataTableOrdenada)
            End If

            If _referenciaEncontradaDataTable.Rows.Count > 0 Then
                AgregarFilasAlDataGridView(_referenciaEncontradaDataTable, Color.Black)
            End If

            If _registrosDataTable.Rows.Count = 0 AndAlso _referenciaEncontradaDataTable.Rows.Count = 0 Then
                DGVRelevo.DataSource = Nothing
                MessageBox.Show("No hay registros pendientes por relevar", "Relevo", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If

            DGVRelevo.Refresh()
        End Sub

        Private Sub MostrarRegistrosEnGrillaTotal()

            DGVRelevoTotal.Rows.Clear()

            ' Separar registros
            Dim registrosConPendientesList = _registrosTotalDataTable.Where(Function(row) row.RegistrosPendientesorRelevar <> 0).ToList()
            Dim registrosSinPendientesList = _registrosTotalDataTable.Where(Function(row) row.RegistrosPendientesorRelevar = 0).ToList()

            ' Crear nuevas tablas del mismo tipo
            Dim registrosConPendientesTable As New DBImaging.SchemaProcess.CTA_Get_Registros_Relevo_TotalDataTable()
            Dim registrosSinPendientesTable As New DBImaging.SchemaProcess.CTA_Get_Registros_Relevo_TotalDataTable()

            ' Importar registros a las tablas
            For Each row In registrosConPendientesList
                registrosConPendientesTable.ImportRow(row)
            Next

            For Each row In registrosSinPendientesList
                registrosSinPendientesTable.ImportRow(row)
            Next

            AgregarFilasAlDataGridViewTotal(registrosConPendientesTable, Color.Red)
            AgregarFilasAlDataGridViewTotal(registrosSinPendientesTable, Color.Black)

            DGVRelevoTotal.Refresh()

        End Sub


        ''' <summary>
        ''' Agrega filas al DataGridView DGVRelevo a partir de una tabla de registros,
        ''' asignando los valores de cada campo y aplicando el color de texto especificado a las celdas.
        ''' </summary>
        ''' <param name="tabla">Tabla de registros tipo CTA_Get_Registros_RelevoDataTable.</param>
        ''' <param name="colorTexto">Color a aplicar al texto de todas las celdas de la fila.</param>
        Private Sub AgregarFilasAlDataGridView(ByVal tabla As DBImaging.SchemaProcess.CTA_Get_Registros_RelevoDataTable, ByVal colorTexto As Color)
            For Each row As DBImaging.SchemaProcess.CTA_Get_Registros_RelevoRow In tabla
                Dim index As Integer = DGVRelevo.Rows.Add()
                Dim nuevaFila As DataGridViewRow = DGVRelevo.Rows(index)

                nuevaFila.Cells("NumeroReferencia").Value = row.NumeroReferencia
                nuevaFila.Cells("TipoMovimiento").Value = row.TipoMovimiento
                nuevaFila.Cells("Ciald").Value = row.Ciald
                nuevaFila.Cells("NombreOficina").Value = row.NombreOficina
                nuevaFila.Cells("CodigoOficinaDian").Value = row.CodigoOficinaDian
                nuevaFila.Cells("CodigoOficinaBanco").Value = row.CodigoOficinaBanco
                nuevaFila.Cells("Estado").Value = row.Estado

                ' Aplicar color a todas las celdas
                For Each celda As DataGridViewCell In nuevaFila.Cells
                    celda.Style.ForeColor = colorTexto
                Next
            Next
        End Sub

        Private Sub AgregarFilasAlDataGridViewTotal(ByVal tabla As DBImaging.SchemaProcess.CTA_Get_Registros_Relevo_TotalDataTable, ByVal colorTexto As Color)
            For Each row As DBImaging.SchemaProcess.CTA_Get_Registros_Relevo_TotalRow In tabla
                Dim index As Integer = DGVRelevoTotal.Rows.Add()
                Dim nuevaFila As DataGridViewRow = DGVRelevoTotal.Rows(index)

                nuevaFila.Cells("FechaRecaudo").Value = row.FechaRecaudo
                nuevaFila.Cells("NombreOficinaTotal").Value = row.NombreOficina
                nuevaFila.Cells("RegistrosPorOficina").Value = row.RegistrosPorOficina
                nuevaFila.Cells("RegistroRelevados").Value = row.RegistroRelevados
                nuevaFila.Cells("RegistrosPendientesorRelevar").Value = row.RegistrosPendientesorRelevar

                ' Aplicar color a todas las celdas
                For Each celda As DataGridViewCell In nuevaFila.Cells
                    celda.Style.ForeColor = colorTexto
                Next
            Next
        End Sub

        Private Sub AgregarFilasAlDataGridView(ByVal tabla As DBImaging.SchemaProcess.CTA_Get_Registros_RelevoDataTable)
            For Each row As DBImaging.SchemaProcess.CTA_Get_Registros_RelevoRow In tabla
                Dim index As Integer = DGVRelevo.Rows.Add()
                Dim nuevaFila As DataGridViewRow = DGVRelevo.Rows(index)

                nuevaFila.Cells("NumeroReferencia").Value = row.NumeroReferencia
                nuevaFila.Cells("TipoMovimiento").Value = row.TipoMovimiento
                nuevaFila.Cells("Ciald").Value = row.Ciald
                nuevaFila.Cells("NombreOficina").Value = row.NombreOficina
                nuevaFila.Cells("CodigoOficinaDian").Value = row.CodigoOficinaDian
                nuevaFila.Cells("CodigoOficinaBanco").Value = row.CodigoOficinaBanco
                nuevaFila.Cells("Estado").Value = row.Estado

                Dim colorTexto As Color = Color.Black

                If row.Estado = EstadoProcesoRelevo.PendienteRelevar Then
                    colorTexto = Color.Red
                End If

                ' Aplicar color a todas las celdas
                For Each celda As DataGridViewCell In nuevaFila.Cells
                    celda.Style.ForeColor = colorTexto
                Next
            Next
        End Sub

        Private Sub MostrarMensajeReferenciaNoEncontrada()
            MessageBox.Show("No se encontró una coincidencia para la referencia ingresada. Por favor, verifica que sea correcta.", "Referencia no encontrada", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Sub

#End Region


#Region " Funciones "

        ''' <summary>
        ''' Ordena las filas de la tabla de registros por estado, colocando primero las que están "Pendiente Relevar".
        ''' Devuelve una nueva instancia de la tabla con el mismo esquema y las filas ordenadas.
        ''' </summary>
        ''' <param name="tablaOriginal">Tabla de registros sin ordenar.</param>
        ''' <returns>Tabla ordenada con los registros "Pendiente Relevar" al inicio.</returns>
        Private Function ObtenerRegistrosOrdenadosPorEstado(ByVal tablaOriginal As DBImaging.SchemaProcess.CTA_Get_Registros_RelevoDataTable) As DBImaging.SchemaProcess.CTA_Get_Registros_RelevoDataTable
            Dim tablaOrdenada As New DBImaging.SchemaProcess.CTA_Get_Registros_RelevoDataTable()

            Dim filasOrdenadas = From fila In tablaOriginal
                                 Order By If(fila.Estado = EstadoProcesoRelevo.PendienteRelevar, 0, 1)
                                 Select fila

            For Each fila In filasOrdenadas
                tablaOrdenada.ImportRow(fila)
            Next

            Return tablaOrdenada
        End Function

        ''' <summary>
        ''' Intenta extraer el primer número de 11 dígitos que comienza con 8020 desde un texto dado.
        ''' </summary>
        ''' <param name="input">Cadena escaneada o digitada.</param>
        ''' <returns>Referencia extraída si es válida; de lo contrario, una cadena vacía.</returns>
        Private Function ExtraerReferenciaDesdeTexto(input As String, regexPattern As String) As String
            If String.IsNullOrWhiteSpace(input) OrElse String.IsNullOrWhiteSpace(regexPattern) Then
                Return String.Empty
            End If

            Dim match As Match = Regex.Match(input.Trim(), regexPattern)
            If match.Success Then
                Return match.Value
            End If

            Return String.Empty
        End Function

        ''' <summary>
        ''' Valida y extrae una referencia desde un texto ingresado (manual o escaneado), y verifica que existan datos en la grilla superior.
        ''' </summary>
        ''' <param name="referencia">Texto de entrada que puede contener o ser la referencia.</param>
        ''' <returns>La referencia extraída (11 dígitos válidos) si todo es correcto; de lo contrario, cadena vacía.</returns>
        Private Function ExtraerReferenciaValidaDesdeTexto(ByVal referencia As String) As String

            ' Intentar extraer referencia desde texto largo (escaneado)
            Dim referenciaExtraida = ExtraerReferenciaDesdeTexto(referencia, regexOnceDigitosDespuesDe8020)

            ' Si no se encuentra en texto largo, validar si el texto completo son solo los 11 dígitos válidos
            If referenciaExtraida Is String.Empty Then
                referenciaExtraida = ExtraerReferenciaDesdeTexto(referencia, regexSoloOnceDigitos)
            End If

            Return referenciaExtraida
        End Function

        Private Sub BtnConsultaActas_Click(sender As Object, e As EventArgs) Handles BtnConsultaActas.Click
            Dim FrmActas As New FormActas
            FrmActas.TipoRelevo = "LOCAL"
            FrmActas.ShowDialog()
        End Sub

        Private Function FindParentTabControl(ctrl As Control) As TabControl
            Dim parent = ctrl.Parent
            While parent IsNot Nothing
                If TypeOf parent Is TabControl Then
                    Return CType(parent, TabControl)
                End If
                parent = parent.Parent
            End While
            Return Nothing
        End Function


#End Region
    End Class
End Namespace