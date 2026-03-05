Imports System.Drawing
Imports System.Linq.Expressions
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar
Imports Miharu.Desktop.Controls.DesktopReportViewer
Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Library.Config.DesktopConfig
Imports Miharu.Desktop.Library.MiharuDMZ
Imports Miharu.Imaging.Library.Procesos
Imports Slyg
Imports DBCore


Public Class FormActas
    Dim reporte As New DesktopReportViewer1Control
    Public TipoRelevo As String
    Dim Ingreso As Boolean = True
    Private Sub FormActas_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        CargarFechasRecaudo()
        CargarCiald()

        ObtenerActas()
        If cbxFechaRecaudo.SelectedValue Is Nothing Then
            Exit Sub
        End If


        '  ObtenerTotalesActas()
        Ingreso = False
        PnActas.Controls.Add(reporte)
        PnActas.Controls(0).Dock = DockStyle.Fill
    End Sub

    Private Sub CbxFechaRecaudo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbxFechaRecaudo.SelectedIndexChanged
        If Ingreso = False Then
            ObtenerActas()
            '  ObtenerTotalesActas()

            PnActas.Controls.Add(reporte)
            PnActas.Controls(0).Dock = DockStyle.Fill
        End If
    End Sub

    Private Sub CbxCiald_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbxCiald.SelectedIndexChanged
        If Ingreso = False Then
            ObtenerActas()
        End If
    End Sub

    Private Sub ObtenerActas()
        If TipoRelevo <> "LOCAL" And Ingreso = True Then
            Exit Sub
        End If
        Dim DtActas As New DataTable
        Dim queryResponse As QueryResponse = ClientUtil.resolver("[DB_Miharu.Imaging_Core].[Process].[PA_Get_Registros_Total_Actas]", New List(Of QueryParameter) From {
                New QueryParameter With {.name = "fk_Entidad", .value = Program.ImagingGlobal.Entidad.ToString()},
                New QueryParameter With {.name = "fk_Proyecto", .value = Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto.ToString()},
                New QueryParameter With {.name = "fk_Fecha_Recaudo", .value = cbxFechaRecaudo.SelectedValue.ToString()},
                New QueryParameter With {.name = "Ciald", .value = cbxCiald.SelectedValue.ToString()}
                }, QueryRequestType.StoredProcedure, QueryResponseType.Table)

        DtActas = queryResponse.dataTable

        cbxActas.DataSource = Nothing
        If DtActas.Rows.Count > 0 Then
            Try
                'Dim nuevaFila As DataRow = DtActas.NewRow()
                'nuevaFila("ID") = -1
                'nuevaFila("id_Acta") = "--TODAS--"
                'DtActas.Rows.InsertAt(nuevaFila, 0)

                cbxActas.DisplayMember = "id_Acta"
                cbxActas.ValueMember = "ID"

                cbxActas.DataSource = DtActas

                cbxActas.Refresh()
            Catch ex As Exception
                MsgBox(Err.Description)
            End Try

        Else
            If Ingreso = False Then
                MessageBox.Show("No hay actas generadas en el Ciald seleccionado.", "Actas", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End If
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
            Exit Sub
        End If
    End Sub

    Private Sub CargarCiald()
        'Dejar en pantalla el ciald correspondiente al local y deshabilitar combo
        If TipoRelevo = "LOCAL" Then
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
        Else
            Dim cialdDataTable As DBSecurity.SchemaConfig.TBL_SedeDataTable

            Dim queryResponse As QueryResponse = ClientUtil.resolver("[DB_Miharu.Security_Core].Config.TBL_Sede", New List(Of QueryParameter) From {
                    New QueryParameter With {.name = "fk_Entidad", .value = Program.ImagingGlobal.Entidad.ToString()}
                    }, QueryRequestType.Table, QueryResponseType.Table)

            cialdDataTable = CType(ClientUtil.mapToTypedTable(New DBSecurity.SchemaConfig.TBL_SedeDataTable(), queryResponse.dataTable), DBSecurity.SchemaConfig.TBL_SedeDataTable)

            cbxCiald.DisplayMember = "Nombre_Sede"
            cbxCiald.ValueMember = "id_Sede"

            Utilities.Llenarcombo(cbxCiald, cialdDataTable, DBSecurity.SchemaConfig.TBL_SedeEnum.id_Sede.ColumnName, DBSecurity.SchemaConfig.TBL_SedeEnum.Nombre_Sede.ColumnName, True, "-1", "--TODOS--")
        End If

        cbxCiald.Refresh()

    End Sub

    Private Sub BtnGenerar_Click(sender As Object, e As EventArgs) Handles BtnGenerar.Click

        If Trim(Me.cbxFechaRecaudo.Text) = "" Then
            MsgBox("Debes seleccionar una fecha de recaudo", MsgBoxStyle.Information, "Dato pendiente.")
            Exit Sub
        End If

        If cbxActas.DataSource Is Nothing Then
            MsgBox("No hay acta seleccionada", MsgBoxStyle.Information, "Dato pendiente.")
            Exit Sub
        End If
        If Trim(cbxCiald.Text) = "--TODOS--" Then
            MsgBox("No hay un ciald seleccionado", MsgBoxStyle.Information, "Dato pendiente.")
            Exit Sub
        End If
        ''Dim DtLogos As DBCore.SchemaProcess.CTA_Imagenes_Por_DocumentoDataTable = Cargar_Imagenes()
        'If Trim(Me.cbxActas.Text) = "--TODAS--" Then
        '    For i As Integer = 0 To cbxActas.Items.Count - 1
        '        Dim RowView As DataRowView = DirectCast(cbxActas.Items(i), DataRowView)
        '        If Trim(RowView("ID").ToString()) <> "-1" Then
        '            GenerarReporte_Acta(CInt(RowView("id_Acta")))
        '        End If
        '    Next
        'Else
        GenerarReporte_Acta(CInt(cbxActas.SelectedValue))
        'End If

    End Sub
    Private Sub GenerarReporte_Acta(NumActa As Integer)
        Try
            Dim FechaRecaudo As Integer = CInt(cbxFechaRecaudo.SelectedValue)
            ' Dim reporte As New DesktopReportViewer1Control
            Dim CodigoCiald As Integer
            Dim NombreCiald As String

            If TipoRelevo = "LOCAL" Then
                CodigoCiald = CInt(Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede)
                NombreCiald = Program.DesktopGlobal.CentroProcesamientoRow.Nombre_Sede
            Else
                CodigoCiald = CInt(cbxCiald.SelectedValue)
                NombreCiald = cbxCiald.Text
            End If

            Dim queryResponse As QueryResponse = ClientUtil.resolver("[DB_Miharu.Imaging_Core].[Process].[PA_Get_Registros_Relevo_ConActa]", New List(Of QueryParameter) From {
                 New QueryParameter With {.name = "fk_Entidad", .value = Program.ImagingGlobal.Entidad.ToString()},
                 New QueryParameter With {.name = "fk_Proyecto", .value = Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto.ToString()},
                 New QueryParameter With {.name = "fk_Fecha_Recaudo", .value = FechaRecaudo.ToString()},
                 New QueryParameter With {.name = "Ciald", .value = CodigoCiald.ToString()},
                 New QueryParameter With {.name = "fk_Serie_Documental", .value = CInt("1").ToString()},
                 New QueryParameter With {.name = "Estado_CialdLocal", .value = CInt(EstadoEnum.Relevo_Ciald_Local).ToString()},
                 New QueryParameter With {.name = "fk_Acta", .value = NumActa.ToString()}
                 }, QueryRequestType.StoredProcedure, QueryResponseType.Table)

            If queryResponse.dataTable Is Nothing OrElse Not queryResponse.dataTable.Rows.Count > 0 Then
                MsgBox("No existen registros para generar este reporte.. Verifique.", MsgBoxStyle.Information, "Reporte Acta.")
                Exit Sub
            End If

            Dim objReport As New Report_Acta_CialdLocal(reporte) With {
                .DTActas = queryResponse.dataTable,
                .CodigoCiald = CodigoCiald,
                .NombreCiald = Nombreciald}

            objReport.Launch(CInt(cbxFechaRecaudo.SelectedValue))
            ' Crear una ventana emergente para mostrarlo
            ' Mostrar el control dentro del formulario
            reporte.Dock = DockStyle.Fill
            'PnActas.Controls.Add(reporte)
        Catch
            MsgBox(Err.Description)
        End Try

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

        Catch ex As Exception
            MessageBox.Show("Error: " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally

        End Try
    End Function


End Class