Imports System.Drawing
Imports System.IO
Imports System.Linq
Imports System.Reflection
Imports System.Runtime.Remoting.Messaging
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
    Public Class UCRelevoCentralizador

#Region " Declaraciones "
        Public Workspace As FormImagingWorkSpace

        Private regexOnceDigitosDespuesDe8020 As String = "(?<=8020)\d{11}"
        Private regexSoloOnceDigitos As String = "^\d{11}$"

        Private _registrosDataTable As DBImaging.SchemaProcess.CTA_Get_Registros_Relevo_CentralDataTable
        Private _TotalesDataTable As New DataTable

        Private Ingreso As Boolean = True
        Private lastTabPage As TabPage
        Private ignoreChange As Boolean = False
#End Region

#Region " Constructores "

        Public Sub New()
            InitializeComponent()
            _registrosDataTable = New DBImaging.SchemaProcess.CTA_Get_Registros_Relevo_CentralDataTable()
        End Sub



#End Region

#Region " Eventos "
        Private Sub UCRelevoCentralizador_Load(sender As Object, e As EventArgs) Handles MyBase.Load

            InicializateControls()

            Dim parentTabControl As TabControl = FindParentTabControl(Me)
            If parentTabControl IsNot Nothing Then
                AddHandler parentTabControl.Deselecting, AddressOf TabControl_Deselecting
                lastTabPage = parentTabControl.SelectedTab ' Inicializar con la pestaña actual
            End If
        End Sub

        Private Sub CbxCiald_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbxCiald.SelectedIndexChanged
            CargarOficinas()
            If Ingreso = False Then
                ObtenerActas()
            End If
        End Sub

        Private Sub BtnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
            ObtenerRegistrosRelevo()
            MostrarRegistrosEnGrilla()

            ObtenerTotalesxCiald()
            MostrarRegistrosEnGrillaTotal()
        End Sub

        Private Sub LoginReferenciaTextBox_KeyDown(sender As Object, e As KeyEventArgs) Handles LoginReferenciaTextBox.KeyDown

            If e.KeyCode = Keys.Enter Then
                e.SuppressKeyPress = True
                ProcesarReferenciaIngresada(LoginReferenciaTextBox.Text.Trim())
            End If

        End Sub


#End Region

#Region " Metódos "

        Private Sub InicializateControls()
            Ingreso = True
            CargarFechasRecaudo()
            CargarCiald()
            ObtenerActas()
            Ingreso = False
            If DGVRelevo IsNot Nothing Then
                DGVRelevo.DataSource = Nothing
                DGVRelevo.Rows.Clear()
                DGVRelevo.Refresh()
            End If

            If DGVTotalCiald IsNot Nothing Then
                DGVTotalCiald.DataSource = Nothing
                DGVTotalCiald.Rows.Clear()
                DGVTotalCiald.Refresh()
            End If
        End Sub

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

        Private Sub TabControl_SelectedIndexChanged(sender As Object, e As EventArgs)
            Dim tabControl As TabControl = CType(sender, TabControl)
            Dim currentTab As TabPage = tabControl.SelectedTab

            ' Prevenir bucles infinitos al forzar cambio de pestaña
            If ignoreChange Then
                ignoreChange = False
                Return
            End If

            ' Verificar si salimos de la pestaña actual que contiene este UserControl
            If lastTabPage IsNot Nothing AndAlso lastTabPage.Controls.Contains(Me) AndAlso lastTabPage IsNot currentTab Then

                Dim respuesta As DialogResult = MessageBox.Show(
            "Está a punto de salir de esta página." & Environment.NewLine &
            "Si tiene información pendiente, por favor guárdela antes de continuar." & Environment.NewLine &
            "¿Desea salir de esta página?",
            "Confirmación de salida",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning
        )

                If respuesta = DialogResult.No Then
                    ' Cancelar cambio de pestaña devolviéndolo a la anterior
                    ignoreChange = True
                    tabControl.SelectedTab = lastTabPage
                    Return
                End If

            End If

            ' Actualizar la pestaña actual para el siguiente cambio
            lastTabPage = currentTab

            ' Opcional: recargar controles al volver a la página
            If currentTab.Controls.Contains(Me) Then
                InicializateControls()
            End If

        End Sub

        Private Sub CargarCiald()

            Dim cialdDataTable As DBSecurity.SchemaConfig.TBL_SedeDataTable

            Dim queryResponse As QueryResponse = ClientUtil.resolver("[DB_Miharu.Security_Core].Config.TBL_Sede", New List(Of QueryParameter) From {
                New QueryParameter With {.name = "fk_Entidad", .value = Program.ImagingGlobal.Entidad.ToString()}
                }, QueryRequestType.Table, QueryResponseType.Table)

            cialdDataTable = CType(ClientUtil.mapToTypedTable(New DBSecurity.SchemaConfig.TBL_SedeDataTable(), queryResponse.dataTable), DBSecurity.SchemaConfig.TBL_SedeDataTable)

            cbxCiald.DisplayMember = "Nombre_Sede"
            cbxCiald.ValueMember = "id_Sede"

            Utilities.Llenarcombo(cbxCiald, cialdDataTable, DBSecurity.SchemaConfig.TBL_SedeEnum.id_Sede.ColumnName, DBSecurity.SchemaConfig.TBL_SedeEnum.Nombre_Sede.ColumnName, True, "-1", "--TODOS--")


            cbxCiald.Refresh()

            CargarOficinas()
        End Sub

        Private Sub CargarOficinas()
            Dim oficinaDataTable As DBSecurity.SchemaConfig.CTA_CentrosProcesamiento_ComboBoxDataTable

            Dim queryResponse As QueryResponse = ClientUtil.resolver("[DB_Miharu.Security_Core].Config.PA_CentrosProcesamiento_ComboBox", New List(Of QueryParameter) From {
                New QueryParameter With {.name = "fk_Entidad", .value = Program.ImagingGlobal.Entidad.ToString()},
                New QueryParameter With {.name = "fk_Sede", .value = cbxCiald.SelectedValue.ToString()}
                }, QueryRequestType.StoredProcedure, QueryResponseType.Table)

            oficinaDataTable = CType(ClientUtil.mapToTypedTable(New DBSecurity.SchemaConfig.CTA_CentrosProcesamiento_ComboBoxDataTable(), queryResponse.dataTable), DBSecurity.SchemaConfig.CTA_CentrosProcesamiento_ComboBoxDataTable)

            Utilities.Llenarcombo(OficinasDesktopComboBox, oficinaDataTable, DBSecurity.SchemaConfig.CTA_CentrosProcesamiento_ComboBoxEnum.Codigo_Centro.ColumnName, DBSecurity.SchemaConfig.CTA_CentrosProcesamiento_ComboBoxEnum.Centro.ColumnName, True, "-1", "--TODOS--")

            OficinasDesktopComboBox.Refresh()
        End Sub

        Private Sub CargarFechasRecaudo()

            Dim fechasRecaudoDataTable As DBImaging.SchemaProcess.TBL_Fecha_RecaudoDataTable

            Dim queryResponse As QueryResponse = ClientUtil.resolver("[DB_Miharu.Imaging_Core].Process.TBL_Fecha_Recaudo", New List(Of QueryParameter) From {
                New QueryParameter With {.name = "fk_Entidad", .value = Program.ImagingGlobal.Entidad.ToString()},
                New QueryParameter With {.name = "fk_Proyecto", .value = Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto.ToString()},
                New QueryParameter With {.name = "Cerrado", .value = "0".ToString()}
                }, QueryRequestType.Table, QueryResponseType.Table)

            fechasRecaudoDataTable = CType(ClientUtil.mapToTypedTable(New DBImaging.SchemaProcess.TBL_Fecha_RecaudoDataTable(), queryResponse.dataTable), DBImaging.SchemaProcess.TBL_Fecha_RecaudoDataTable)
            cbxFechaRecaudo.DataSource = Nothing
            If fechasRecaudoDataTable.Rows.Count > 0 Then
                cbxFechaRecaudo.DisplayMember = "id_Fecha_Recaudo"
                cbxFechaRecaudo.ValueMember = "id_Fecha_Recaudo"

                cbxFechaRecaudo.DataSource = fechasRecaudoDataTable

                cbxFechaRecaudo.Refresh()
            Else
                MessageBox.Show("No hay fechas de recaudo abiertas", "Relevo", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End Sub

        Private Sub ObtenerActas()

            Dim DtActas As New DataTable
            'If cbxCiald.SelectedValue Is Nothing Then cbxCiald.SelectedItem = 0
            Dim queryResponse As QueryResponse = ClientUtil.resolver("[DB_Miharu.Imaging_Core].[Process].[PA_Get_Registros_Total_Actas]", New List(Of QueryParameter) From {
                New QueryParameter With {.name = "fk_Entidad", .value = Program.ImagingGlobal.Entidad.ToString()},
                New QueryParameter With {.name = "fk_Proyecto", .value = Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto.ToString()},
                New QueryParameter With {.name = "fk_Fecha_Recaudo", .value = cbxFechaRecaudo.SelectedValue.ToString()},
                New QueryParameter With {.name = "Ciald", .value = cbxCiald.SelectedValue.ToString()}
                }, QueryRequestType.StoredProcedure, QueryResponseType.Table)

            DtActas = queryResponse.dataTable

            Dim dt As New DataTable("MiTabla")
            dt.Columns.Add("ID", GetType(Integer))
            dt.Columns.Add("id_Acta", GetType(String))

            For Each row As DataRow In DtActas.Rows
                Dim id As Integer = If(IsDBNull(row("ID")), 0, CInt(row("ID")))
                Dim idActa As String = If(IsDBNull(row("id_Acta")), "", row("id_Acta").ToString())
                dt.Rows.Add(id, idActa)
            Next
            cbxActas.DataSource = Nothing
            Utilities.Llenarcombo(cbxActas, dt, "ID", "id_Acta", True, "-1", "--TODOS--")
        End Sub



        ''' <summary>
        ''' Procesa la referencia ingresada por el usuario: valida, busca en la grilla,
        ''' actualiza su estado mediante procedimiento almacenado y la agrega al listado de referencias encontradas.
        ''' </summary>
        ''' <param name="texto">Texto ingresado por el usuario que puede contener una referencia válida.</param>
        Private Sub ProcesarReferenciaIngresada(texto As String)

            Dim referenciaValida = ExtraerReferenciaValidaDesdeTexto(texto)

            If String.IsNullOrEmpty(referenciaValida) Then
                MostrarMensajeReferenciaNoEncontrada()
                Exit Sub
            End If

            Dim referenciaEncontradadataTable = BuscarReferenciaEnGrilla(referenciaValida)

            If referenciaEncontradadataTable Is Nothing OrElse referenciaEncontradadataTable.Count = 0 Then
                MostrarMensajeReferenciaNoEncontrada()
                Exit Sub
            End If
            If CInt(referenciaEncontradadataTable.Rows(0).Item("fk_Estado_Relevo").ToString) = 6001 Then
                MsgBox("Este registro ya fue marcado como Centralizado, no es posible cambiar su estado de nuevo", MsgBoxStyle.Critical, "Cuidado..")
                Exit Sub
            End If

            Dim Estado As Integer
            Estado = CInt(referenciaEncontradadataTable.Rows(0).Item("fk_Estado_Relevo").ToString)
            If Estado = 6002 Then Estado = 0
            Dim queryResponse As QueryResponse = ClientUtil.resolver("[DB_Miharu.Imaging_Core].[Process].[PA_Update_Registros_Relevo_Central]", New List(Of QueryParameter) From {
                        New QueryParameter With {.name = "fk_Entidad", .value = Program.ImagingGlobal.Entidad.ToString()},
                        New QueryParameter With {.name = "fk_Proyecto", .value = Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto.ToString()},
                        New QueryParameter With {.name = "fk_Fecha_Recaudo", .value = cbxFechaRecaudo.SelectedValue.ToString()},
                        New QueryParameter With {.name = "Oficina", .value = OficinasDesktopComboBox.SelectedValue.ToString()},
                        New QueryParameter With {.name = "Numero_Referencia", .value = referenciaValida.ToString()},
                        New QueryParameter With {.name = "Estado_Relevo_CialCentral", .value = Estado.ToString()},
                        New QueryParameter With {.name = "fk_Usuario", .value = Program.Sesion.Usuario.id.ToString()}
                                                    }, QueryRequestType.StoredProcedure, QueryResponseType.Table)

            If queryResponse.dataTable.Rows.Count > 0 Then
                Dim registrosProcesados As Integer = Convert.ToInt32(queryResponse.dataTable.Rows(0)("RegistrosProcesados"))
                Dim mensajeResultado As String = queryResponse.dataTable.Rows(0)("Mensaje").ToString()

                If registrosProcesados > 0 Then
                    ObtenerRegistrosRelevo()
                    MostrarRegistrosEnGrilla()

                    ObtenerTotalesxCiald()
                    MostrarRegistrosEnGrillaTotal()
                    MessageBox.Show("Se realizo el cruce de la referencia.", "Referencia encontrada", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    LoginReferenciaTextBox.Text = ""
                ElseIf Not String.IsNullOrWhiteSpace(mensajeResultado) Then
                    ' Hubo algún problema, mostrar mensaje informativo
                    MessageBox.Show("No se pudo procesar la referencia: " & mensajeResultado, "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If
            Else
                MostrarMensajeReferenciaNoEncontrada()
                Exit Sub
            End If

        End Sub


        ''' <summary>
        ''' Busca en la grilla una fila que coincida con el número de referencia especificado
        ''' y devuelve una tabla con los registros encontrados.
        ''' </summary>
        ''' <param name="referenciaValida">Referencia a buscar en la columna "NumeroReferencia" del DataGridView.</param>
        ''' <returns>Tabla con los registros que coinciden con la referencia.</returns>
        Private Function BuscarReferenciaEnGrilla(referenciaValida As String) As DBImaging.SchemaProcess.CTA_Get_Registros_Relevo_CentralDataTable
            Dim resultado = New DBImaging.SchemaProcess.CTA_Get_Registros_Relevo_CentralDataTable()

            ' Buscar coincidencia en la columna "Numero de Referencia"
            For Each fila As DataGridViewRow In DGVRelevo.Rows

                If Not fila.IsNewRow AndAlso fila.Cells("NumeroReferencia").Value?.ToString().Trim() = referenciaValida Then
                    resultado.AddCTA_Get_Registros_Relevo_CentralRow(CLng(fila.Cells("id_Log_Data").Value),
                                                                CLng(fila.Cells("NumeroReferencia").Value),
                                                                fila.Cells("IDTipoMvto").Value?.ToString(),
                                                                fila.Cells("TipoMovimiento").Value?.ToString(),
                                                                fila.Cells("CodigoCiald").Value?.ToString(),
                                                                fila.Cells("Ciald").Value?.ToString(),
                                                                fila.Cells("NombreOficina").Value?.ToString(),
                                                                fila.Cells("CodigoOficinaDian").Value?.ToString(),
                                                                fila.Cells("CodigoOficinaBanco").Value?.ToString(),
                                                                fila.Cells("Monto").Value?.ToString(),
                                                                EstadoProcesoRelevo.RelevoCialdCentralizador,
                                                                fila.Cells("fk_Estado_Relevo").Value?.ToString(),
                                                                fila.Cells("FechaProceso").Value?.ToString()
                                                            )
                End If
            Next

            Return resultado
        End Function

        ''' <summary>
        ''' Consulta los registros de relevo desde la base de datos usando parámetros seleccionados 
        ''' (entidad, proyecto, fecha de recaudo, oficina y serie documental), y carga el resultado en _registrosDataTable.
        ''' </summary>
        Private Sub ObtenerRegistrosRelevo()

            Dim queryResponse As QueryResponse = ClientUtil.resolver("[DB_Miharu.Imaging_Core].Process.PA_Get_Registros_Relevo_Centralizador", New List(Of QueryParameter) From {
                New QueryParameter With {.name = "fk_Entidad", .value = Program.ImagingGlobal.Entidad.ToString()},
                New QueryParameter With {.name = "fk_Proyecto", .value = Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto.ToString()},
                New QueryParameter With {.name = "fk_Fecha_Recaudo", .value = cbxFechaRecaudo.SelectedValue.ToString()},
                New QueryParameter With {.name = "Ciald", .value = cbxCiald.SelectedValue.ToString()},
                New QueryParameter With {.name = "Oficina", .value = OficinasDesktopComboBox.SelectedValue.ToString()},
                New QueryParameter With {.name = "fk_Serie_Documental", .value = "1".ToString()},
                New QueryParameter With {.name = "fk_Acta", .value = cbxActas.SelectedValue.ToString()}
                }, QueryRequestType.StoredProcedure, QueryResponseType.Table)

            _registrosDataTable = CType(ClientUtil.mapToTypedTable(New DBImaging.SchemaProcess.CTA_Get_Registros_Relevo_CentralDataTable(), queryResponse.dataTable), DBImaging.SchemaProcess.CTA_Get_Registros_Relevo_CentralDataTable)
        End Sub


        ''' <summary>
        ''' Consulta los registros de relevo desde la base de datos usando parámetros seleccionados 
        ''' (entidad, proyecto, fecha de recaudo, oficina y serie documental), y carga el resultado en _registrosDataTable.
        ''' </summary>
        Private Sub ObtenerTotalesxCiald()

            Dim queryResponse As QueryResponse = ClientUtil.resolver("[DB_Miharu.Imaging_Core].[Process].[PA_Get_Registros_Relevo_Total_Centralizador]", New List(Of QueryParameter) From {
                New QueryParameter With {.name = "fk_Entidad", .value = Program.ImagingGlobal.Entidad.ToString()},
                New QueryParameter With {.name = "fk_Proyecto", .value = Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto.ToString()},
                New QueryParameter With {.name = "fk_Fecha_Recaudo", .value = cbxFechaRecaudo.SelectedValue.ToString()}
                }, QueryRequestType.StoredProcedure, QueryResponseType.Table)

            _TotalesDataTable = queryResponse.dataTable
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

            Dim _RegistrosEncontrados As New DBImaging.SchemaProcess.CTA_Get_Registros_Relevo_CentralDataTable
            If _RegistrosEncontrados.Rows.Count > 0 Then
                AgregarFilasAlDataGridView(_RegistrosEncontrados, Color.Black)
            End If

            If _registrosDataTable.Rows.Count = 0 AndAlso _RegistrosEncontrados.Rows.Count = 0 Then
                DGVRelevo.DataSource = Nothing
                MessageBox.Show("No hay registros pendientes por relevar", "Relevo Central", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
            DGVRelevo.Refresh()
        End Sub

        Private Sub MostrarRegistrosEnGrillaTotal()
            DGVTotalCiald.Rows.Clear()
            AgregarFilasAlDataGridViewTotal(_TotalesDataTable, Color.Black)
            DGVTotalCiald.Refresh()
        End Sub


        ''' <summary>
        ''' Agrega filas al DataGridView DGVRelevo a partir de una tabla de registros,
        ''' asignando los valores de cada campo y aplicando el color de texto especificado a las celdas.
        ''' </summary>
        ''' <param name="tabla">Tabla de registros tipo CTA_Get_Registros_RelevoDataTable.</param>
        ''' <param name="colorTexto">Color a aplicar al texto de todas las celdas de la fila.</param>

        Private Sub AgregarFilasAlDataGridView(ByVal tabla As DBImaging.SchemaProcess.CTA_Get_Registros_Relevo_CentralDataTable, ByVal colorTexto As Color)

            For Each row As DBImaging.SchemaProcess.CTA_Get_Registros_Relevo_CentralRow In tabla
                Dim index As Integer = DGVRelevo.Rows.Add()
                Dim nuevaFila As DataGridViewRow = DGVRelevo.Rows(index)
                nuevaFila.Cells("id_Log_Data").Value = row.id_Log_Data
                nuevaFila.Cells("NumeroReferencia").Value = row.NumeroReferencia
                nuevaFila.Cells("IDTipoMvto").Value = row.IDTipoMvto
                nuevaFila.Cells("TipoMovimiento").Value = row.TipoMovimiento
                nuevaFila.Cells("CodigoCiald").Value = row.CodigoCiald
                nuevaFila.Cells("Ciald").Value = row.Ciald
                nuevaFila.Cells("NombreOficina").Value = row.NombreOficina
                nuevaFila.Cells("CodigoOficinaDian").Value = row.CodigoOficinaDian
                nuevaFila.Cells("CodigoOficinaBanco").Value = row.CodigoOficinaBanco
                nuevaFila.Cells("Monto").Value = row.Monto
                nuevaFila.Cells("Estado").Value = row.Estado
                nuevaFila.Cells("fk_Estado_Relevo").Value = row.fk_Estado_Relevo
                nuevaFila.Cells("FechaProceso").Value = row.FechaProceso
                ' Aplicar color a todas las celdas
                For Each celda As DataGridViewCell In nuevaFila.Cells
                    celda.Style.ForeColor = colorTexto
                Next
            Next
        End Sub
        Private Sub AgregarFilasAlDataGridView(ByVal tabla As DBImaging.SchemaProcess.CTA_Get_Registros_Relevo_CentralDataTable)
            For Each row As DBImaging.SchemaProcess.CTA_Get_Registros_Relevo_CentralRow In tabla
                Dim index As Integer = DGVRelevo.Rows.Add()
                Dim nuevaFila As DataGridViewRow = DGVRelevo.Rows(index)
                nuevaFila.Cells("id_Log_Data").Value = row.id_Log_Data
                nuevaFila.Cells("NumeroReferencia").Value = row.NumeroReferencia
                nuevaFila.Cells("IDTipoMvto").Value = row.IDTipoMvto
                nuevaFila.Cells("TipoMovimiento").Value = row.TipoMovimiento
                nuevaFila.Cells("CodigoCiald").Value = row.CodigoCiald
                nuevaFila.Cells("Ciald").Value = row.Ciald
                nuevaFila.Cells("NombreOficina").Value = row.NombreOficina
                nuevaFila.Cells("CodigoOficinaDian").Value = row.CodigoOficinaDian
                nuevaFila.Cells("CodigoOficinaBanco").Value = row.CodigoOficinaBanco
                nuevaFila.Cells("Monto").Value = row.Monto
                nuevaFila.Cells("Estado").Value = row.Estado
                nuevaFila.Cells("fk_Estado_Relevo").Value = row.fk_Estado_Relevo
                nuevaFila.Cells("FechaProceso").Value = row.FechaProceso

                Dim colorTexto As Color = Color.Black
                Select Case CInt(row.fk_Estado_Relevo)
                    Case 0
                        colorTexto = Color.Red
                        nuevaFila.Cells("BtnCambioEstado").Value = ""
                        nuevaFila.Cells("BtnCambioEstado").Tag = "Deshabilitado"
                        nuevaFila.Cells("BtnCambioEstado").Style.ForeColor = Color.Green
                        nuevaFila.Cells("BtnCambioEstado").Style.BackColor = Color.LightGray
                    Case 6000
                        colorTexto = Color.Blue
                        nuevaFila.Cells("BtnCambioEstado").Value = "Pasar a Faltante"
                        nuevaFila.Cells("BtnCambioEstado").Tag = "habilitado"
                    Case 6001
                        colorTexto = Color.Black
                        nuevaFila.Cells("BtnCambioEstado").Value = "Pasar a Faltante"
                        nuevaFila.Cells("BtnCambioEstado").Tag = "habilitado"
                    Case 6002
                        colorTexto = Color.Red
                        nuevaFila.Cells("BtnCambioEstado").Value = ""
                        nuevaFila.Cells("BtnCambioEstado").Tag = "Deshabilitado"
                        nuevaFila.Cells("BtnCambioEstado").Style.ForeColor = Color.Gray
                        nuevaFila.Cells("BtnCambioEstado").Style.BackColor = Color.LightGray
                End Select
                ' Aplicar color a todas las celdas
                For Each celda As DataGridViewCell In nuevaFila.Cells
                    celda.Style.ForeColor = colorTexto
                Next
            Next

        End Sub

        Private Sub AgregarFilasAlDataGridViewTotal(ByVal tabla As DataTable, ByVal colorTexto As Color)
            For Each row As DataRow In tabla.Rows
                Dim index As Integer = DGVTotalCiald.Rows.Add()
                Dim nuevaFila As DataGridViewRow = DGVTotalCiald.Rows(index)

                nuevaFila.Cells("FechaRecaudo").Value = row("FechaRecaudo")
                nuevaFila.Cells("CialdTot").Value = row("CialdTot")
                nuevaFila.Cells("RegistrosCiald").Value = row("RegistrosCiald")
                nuevaFila.Cells("Relevados").Value = row("Relevados")
                nuevaFila.Cells("Centralizados").Value = row("Centralizados")
                nuevaFila.Cells("Faltantes").Value = row("Faltantes")

                ' Aplicar color a todas las celdas
                For Each celda As DataGridViewCell In nuevaFila.Cells
                    celda.Style.ForeColor = colorTexto
                Next
            Next
        End Sub


        Private Sub MostrarMensajeReferenciaNoEncontrada()
            MessageBox.Show("La referencia ingresada no cruzó.", "Referencia no encontrada", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Sub

#End Region


#Region " Funciones "

        ''' <summary>
        ''' Ordena las filas de la tabla de registros por estado, colocando primero las que están "Pendiente Relevar".
        ''' Devuelve una nueva instancia de la tabla con el mismo esquema y las filas ordenadas.
        ''' </summary>
        ''' <param name="tablaOriginal">Tabla de registros sin ordenar.</param>
        ''' <returns>Tabla ordenada con los registros "Pendiente Relevar" al inicio.</returns>
        Private Function ObtenerRegistrosOrdenadosPorEstado(ByVal tablaOriginal As DBImaging.SchemaProcess.CTA_Get_Registros_Relevo_CentralDataTable) As DBImaging.SchemaProcess.CTA_Get_Registros_Relevo_CentralDataTable
            Dim tablaOrdenada As New DBImaging.SchemaProcess.CTA_Get_Registros_Relevo_CentralDataTable()

            ' Asegúrate de que no existe ya una columna auxiliar
            If Not tablaOriginal.Columns.Contains("Nivel") Then
                tablaOriginal.Columns.Add("Nivel", GetType(Integer))
            End If

            ' Asignar orden personalizado
            For Each row As DataRow In tablaOriginal.Rows
                Select Case CInt(row("fk_Estado_Relevo"))
                    Case 0 : row("Nivel") = 0
                    Case 6002 : row("Nivel") = 1
                    Case 6000 : row("Nivel") = 2
                    Case 6001 : row("Nivel") = 3
                    Case Else : row("Nivel") = 1000 + CInt(row("fk_Estado_Relevo"))
                End Select
            Next

            ' Ordenar por la columna auxiliar
            Dim dv As New DataView(tablaOriginal) With {
              .Sort = "Nivel ASC"}

            tablaOriginal.Columns.Remove("Nivel")

            ' Mostrar en la grilla
            'DGVRelevo.DataSource = tablaOriginal
            Return tablaOriginal

            'Dim filasOrdenadas = From fila In tablaOriginal
            '                     Order By If(fila.Estado = "Faltante", 0, 1)
            '                     Select fila
            'For Each fila In filasOrdenadas
            '    tablaOrdenada.ImportRow(fila)
            'Next
            'Return tablaOrdenada
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

        Private Sub BtnReporte_Click(sender As Object, e As EventArgs) Handles BtnReporte.Click
            Dim miVisorCentral As New DesktopReportViewer1Control()

            Dim Reporte As New Reportes.Ciald.Report_Bean(miVisorCentral)

            Reporte.Launch(CInt(Me.cbxFechaRecaudo.SelectedValue))
            Dim popup As New Form()
            popup.Text = "Reporte Bean"
            popup.Size = New Size(1348, 559)
            popup.StartPosition = FormStartPosition.CenterScreen
            popup.FormBorderStyle = FormBorderStyle.FixedToolWindow
            popup.ShowIcon = False

            miVisorCentral.Dock = DockStyle.Fill
            popup.Controls.Add(miVisorCentral)
            popup.Show()
        End Sub

        Private Sub DGVRelevo_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGVRelevo.CellContentClick
            ' Verifica que la celda clickeada es de tipo botón
            If TypeOf DGVRelevo.Columns(e.ColumnIndex) Is DataGridViewButtonColumn Then
                ' Obtiene datos de la fila
                Dim fila As DataGridViewRow = DGVRelevo.Rows(e.RowIndex)
                Dim ValorEstado As String = fila.Cells("fk_Estado_Relevo").Value.ToString()
                Select Case ValorEstado
                    Case "0", "6002"
                        Exit Sub
                    Case "6000"
                        If MsgBox("Esta seguro de cambiar el estado actual a Faltante.", CType(MsgBoxStyle.Information + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, Global.Microsoft.VisualBasic.MsgBoxStyle), "CAMBIO DE ESTADO.") = MsgBoxResult.No Then
                            Exit Sub
                        Else
                            CambiarEstado(e.RowIndex)
                        End If
                    Case "6001"
                        If MsgBox("Esta seguro de cambiar el estado actual a Faltante.", CType(MsgBoxStyle.Information + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, Global.Microsoft.VisualBasic.MsgBoxStyle), "CAMBIO DE ESTADO.") = MsgBoxResult.No Then
                            Exit Sub
                        Else
                            CambiarEstado(e.RowIndex)
                        End If

                End Select

                'MessageBox.Show("Se presionó el botón: " & e.RowIndex & ", ID: " & ValorEstado)
            End If

        End Sub

        Private Sub CambiarEstado(IdDataGrid As Integer)
            Dim fila As DataGridViewRow = DGVRelevo.Rows(IdDataGrid)
            Dim referenciaValida As String = fila.Cells("NumeroReferencia").Value.ToString()
            Dim referenciaEncontradadataTable = BuscarReferenciaEnGrilla(referenciaValida)

            If referenciaEncontradadataTable Is Nothing OrElse referenciaEncontradadataTable.Count = 0 Then
                MostrarMensajeReferenciaNoEncontrada()
                Exit Sub
            End If
            Dim EstadoNew = 6002
            Dim queryResponse As QueryResponse = ClientUtil.resolver("[DB_Miharu.Imaging_Core].[Process].[PA_Update_Registros_Relevo_Central]", New List(Of QueryParameter) From {
                                        New QueryParameter With {.name = "fk_Entidad", .value = Program.ImagingGlobal.Entidad.ToString()},
                                        New QueryParameter With {.name = "fk_Proyecto", .value = Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto.ToString()},
                                        New QueryParameter With {.name = "fk_Fecha_Recaudo", .value = cbxFechaRecaudo.SelectedValue.ToString()},
                                        New QueryParameter With {.name = "Oficina", .value = OficinasDesktopComboBox.SelectedValue.ToString()},
                                        New QueryParameter With {.name = "Numero_Referencia", .value = fila.Cells("NumeroReferencia").Value.ToString()},
                                        New QueryParameter With {.name = "Estado_Relevo_CialCentral", .value = EstadoNew.ToString()},
                                        New QueryParameter With {.name = "fk_Usuario", .value = Program.Sesion.Usuario.id.ToString()}
                                        }, QueryRequestType.StoredProcedure, QueryResponseType.Table)

            If queryResponse.dataTable.Rows.Count > 0 Then
                Dim registrosProcesados As Integer = Convert.ToInt32(queryResponse.dataTable.Rows(0)("RegistrosProcesados"))
                Dim mensajeResultado As String = queryResponse.dataTable.Rows(0)("Mensaje").ToString()

                If registrosProcesados > 0 Then
                    ObtenerRegistrosRelevo()
                    MostrarRegistrosEnGrilla()

                    ObtenerTotalesxCiald()
                    MostrarRegistrosEnGrillaTotal()

                    LoginReferenciaTextBox.Text = ""

                    MessageBox.Show("Se realizo el cambio de estado de la referencia.", "Cambio de estado.", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    LoginReferenciaTextBox.Text = ""
                ElseIf Not String.IsNullOrWhiteSpace(mensajeResultado) Then
                    ' Hubo algún problema, mostrar mensaje informativo
                    MessageBox.Show("No se pudo procesar la referencia: " & mensajeResultado, "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If
            Else
                MostrarMensajeReferenciaNoEncontrada()
                Exit Sub
            End If

            'MostrarRegistrosEnGrilla()

        End Sub

        Private Sub BtnAnexos_Click(sender As Object, e As EventArgs) Handles BtnAnexos.Click
            Dim FrmAnexos As New FormCentralizador
            FrmAnexos.ShowDialog()

        End Sub

        Private Function Cargar_Imagenes() As DBCore.SchemaProcess.CTA_Imagenes_Por_DocumentoDataTable

            Try
                Dim imagenesDataTable As DBCore.SchemaProcess.CTA_Imagenes_Por_DocumentoDataTable

                Dim queryResponse As QueryResponse = ClientUtil.resolver("[DB_Miharu.Core].[Process].[CTA_Imagenes_Por_Documento]", New List(Of QueryParameter) From {
                                                    New QueryParameter With {.name = "Fk_Entidad", .value = Program.ImagingGlobal.Entidad.ToString()},
                                                    New QueryParameter With {.name = "Fk_Proyecto", .value = Program.ImagingGlobal.Proyecto.ToString()},
                                                    New QueryParameter With {.name = "fk_Esquema", .value = CInt(1).ToString}
                                                    }, QueryRequestType.Table, QueryResponseType.Table)

                imagenesDataTable = CType(ClientUtil.mapToTypedTable(New DBCore.SchemaProcess.CTA_Imagenes_Por_DocumentoDataTable(), queryResponse.dataTable), DBCore.SchemaProcess.CTA_Imagenes_Por_DocumentoDataTable)

                Return imagenesDataTable
                'If imagenesDataTable Is Nothing OrElse imagenesDataTable.Rows.Count = 0 Then Exit Sub

                'For Each imagenRow As DBCore.SchemaProcess.CTA_Imagenes_Por_DocumentoRow In imagenesDataTable
                '    ' Imagen Vacia, continua for
                '    If imagenRow.IsImage_BinaryNull() Then Continue For

                '    Dim imageBytes As Byte() = CType(imagenRow.Image_Binary, Byte())
                '    Dim bitmap As New Bitmap(New MemoryStream(imageBytes))

                '    Select Case imagenRow.Fk_Tipo_Imagen
                '        Case TipoImagenLogo.LogoEncabezado
                '            HeaderLogoImageTextBox.Text = String.Empty
                '            LogoImagePictureBox.Image = bitmap
                '            LogoImagePictureBox.Refresh()

                '        Case TipoImagenLogo.Firma
                '            SignatureImageTextBox.Text = String.Empty
                '            SignatureImagePictureBox.Image = bitmap
                '            SignatureImagePictureBox.Refresh()

                '        Case TipoImagenLogo.LogoPiePagina
                '            FooterLogoImageTextBox.Text = String.Empty
                '            FooterImagePictureBox.Image = bitmap
                '            FooterImagePictureBox.Refresh()
                '    End Select
                'Next

            Catch ex As Exception
                MessageBox.Show("Error: " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return Nothing
            Finally

            End Try
        End Function

        '' Función utilitaria para recorrer la jerarquía de controles y encontrar el TabControl que contiene al UserControl.
        'Private Function FindParentTabControl(ctrl As Control) As TabControl
        '    Dim parent = ctrl.Parent
        '    While parent IsNot Nothing
        '        If TypeOf parent Is TabControl Then
        '            Return CType(parent, TabControl)
        '        End If
        '        parent = parent.Parent
        '    End While
        '    Return Nothing
        'End Function

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

        Private Sub BtnConsultaActas_Click(sender As Object, e As EventArgs) Handles BtnConsultaActas.Click
            Dim FrmActas As New FormActas
            FrmActas.TipoRelevo = "CENTRAL"
            FrmActas.ShowDialog()
        End Sub

        Private Sub cbxFechaRecaudo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbxFechaRecaudo.SelectedIndexChanged
            If Ingreso = False Then
                ObtenerActas()
            End If
        End Sub




#End Region
    End Class
End Namespace