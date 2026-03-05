Imports System.IO
Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library.Config
Imports System.Linq
Imports System.ComponentModel
Imports DBImaging

Namespace Forms.Solicitudes

    Public Class FormDescuelgueDocumentos

        Dim Flag As Boolean
        Dim _IdEntidad As Short
        Dim dtSolicitudesEncontradas As DataTable
        Dim SolicitudSeleccionada, SolicitudesDescolgarCount, ContadorDescuelgue, TotalDocumentosParaDescolgar As Integer

        Sub New(idEntidadLocal As Short)
            _IdEntidad = idEntidadLocal
            InitializeComponent()
            ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
            cbarrasDesktopCBarrasControl.Init(Program.DesktopGlobal.ConnectionStrings.Archiving)
        End Sub


#Region " Eventos "

        Private Sub FormDescuelgueDocumentos_Load(sender As System.Object, e As EventArgs) Handles MyBase.Load
            Dim dmCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
            Try
                dmCore.Connection_Open(Program.Sesion.Usuario.id)
                Dim EntidadEncontrada = dmCore.SchemaSecurity.CTA_Entidad().DBFindByid_Entidad(_IdEntidad).Rows(0)("Nombre_Entidad").ToString()
                Me.lblEntidadEncontrada.Text = EntidadEncontrada
                Flag = True

                CargarFiltroSolicitudes(CShort(Me._IdEntidad), 1500)
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Error al cargar Formulario", ex)
            Finally
                dmCore.Connection_Close()
            End Try
        End Sub

        Private Sub CerrarButton_Click(sender As System.Object, e As EventArgs) Handles CerrarButton.Click
            Me.Close()
        End Sub

        Private Sub EntidadDesktopComboBox_SelectedIndexChanged(sender As System.Object, e As EventArgs)
            'CargarFiltroSolicitudes(CShort(EntidadDesktopComboBox.SelectedValue), 1500)
        End Sub

        Private Sub BuscarButton_Click(sender As System.Object, e As EventArgs) Handles BuscarButton.Click
            Me.CargandoPictureBox.Visible = True
            Me.SolicitudesDataGridView.DataSource = Nothing
            Me.SolicitudSeleccionada = CInt(Me.SolicitudesDesktopComboBox.SelectedValue)
            If (Not Me.DescuelgueBackgroundWorker.IsBusy) Then
                Me.DescuelgueBackgroundWorker.RunWorkerAsync("TareaBuscaSolicitudItems")
            End If

        End Sub

        Private Sub txtCbarrasDoc_KeyPress(sender As System.Object, e As KeyPressEventArgs) Handles cbarrasDesktopCBarrasControl.KeyPress
            If e.KeyChar = ChrW(Keys.Enter) Then
                e.Handled = True

                Dim existe As Boolean = False
                Dim valor As String = cbarrasDesktopCBarrasControl.Text

                Dim busqueda =
                    From reg As DataGridViewRow In SolicitudesDataGridView.Rows.Cast(Of DataGridViewRow)()
                     Where reg.Cells("CBarras_File").Value.ToString() = valor
                     Select reg.Index

                If busqueda.Count > 0 Then
                    existe = True
                    If Not CBool(SolicitudesDataGridView.Rows(busqueda.Single).Cells("R").Value) Then
                        SolicitudesDataGridView.Rows(busqueda.Single).Cells("R").Value = True
                        cbarrasDesktopCBarrasControl.Clear()
                    Else
                        DesktopMessageBoxControl.DesktopMessageShow("El documento ya fue ingresado", "Descuelgue Solicitudes", DesktopMessageBoxControl.IconEnum.WarningIcon)
                        cbarrasDesktopCBarrasControl.Clear()
                    End If
                End If

                If Not existe Then
                    DesktopMessageBoxControl.DesktopMessageShow("Codigo de Barras no encontrado.", "Descuelgue Solicitudes", DesktopMessageBoxControl.IconEnum.WarningIcon)
                End If
                cbarrasDesktopCBarrasControl.Focus()
            End If
        End Sub

        Private Sub btnExportar_Click(sender As System.Object, e As EventArgs) Handles btnExportar.Click
            Exportar()
        End Sub

        Private Sub btnDescuelgue_Click(sender As System.Object, e As EventArgs) Handles btnDescuelgue.Click
            If SolicitudesDataGridView.RowCount > 0 Then
                If (Not DescuelgueBackgroundWorker_2.IsBusy) Then
                    Me.pbDescolgando.Visible = True
                    Me.lblDescolgando.Visible = True
                    Me.DescuelgueBackgroundWorker_2.RunWorkerAsync()
                End If
            Else
                DesktopMessageBoxControl.DesktopMessageShow("No existen documentos para descolgar.", "No existen registros", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
            End If
        End Sub

        Private Sub DescuelgueBackgroundWorker_DoWork(sender As System.Object, e As System.ComponentModel.DoWorkEventArgs) Handles DescuelgueBackgroundWorker.DoWork
            Dim bw As BackgroundWorker = CType(sender, BackgroundWorker)
            If (bw.CancellationPending) Then
                e.Cancel = True
                Return
            Else
                BuscarDocumentos()
            End If
        End Sub

        Private Sub DescuelgueBackgroundWorker_RunWorkerCompleted(sender As System.Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles DescuelgueBackgroundWorker.RunWorkerCompleted
            Me.DescuelgueBackgroundWorker.CancelAsync()
            Me.DescuelgueBackgroundWorker.Dispose()
            Me.CargandoPictureBox.Visible = False
            SolicitudesDataGridView.AutoGenerateColumns = False
            SolicitudesDataGridView.DataSource = Me.dtSolicitudesEncontradas
        End Sub

        Private Sub DescuelgueBackgroundWorker_2_DoWork(sender As System.Object, e As System.ComponentModel.DoWorkEventArgs) Handles DescuelgueBackgroundWorker_2.DoWork
            Dim bw As BackgroundWorker = CType(sender, BackgroundWorker)
            If (bw.CancellationPending) Then e.Cancel = True
            Dim i As Integer = 0
            Dim CantidadSeleccionadas = Me.SolicitudesDataGridView.Rows.Cast(Of DataGridViewRow).Where(Function(x) CType(x.Cells("R").Value, Boolean) = True).Count
            For Each item As DataGridViewRow In SolicitudesDataGridView.Rows
                If CBool(item.Cells("R").Value) Then
                    GestionarItem(item)
                    i = i + 1
                    Me.ContadorDescuelgue += 1
                    Me.DescuelgueBackgroundWorker_2.ReportProgress(Me.ContadorDescuelgue, "Descolgando " + i.ToString() + " de " + CantidadSeleccionadas.ToString() + " Documentos...")
                End If
            Next
            Me.SolicitudesDescolgarCount = i
            Me.DescuelgueBackgroundWorker_2.ReportProgress(100, "Descuelgue Completado.")
            Threading.Thread.Sleep(1000)
        End Sub

        Private Sub DescuelgueBackgroundWorker_2_ProgressChanged(sender As System.Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles DescuelgueBackgroundWorker_2.ProgressChanged
            Me.pbDescolgando.Value = e.ProgressPercentage
            Me.lblDescolgando.Text = e.UserState.ToString()
        End Sub

        Private Sub DescuelgueBackgroundWorker_2_RunWorkerCompleted(sender As System.Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles DescuelgueBackgroundWorker_2.RunWorkerCompleted
            Me.DescuelgueBackgroundWorker_2.CancelAsync()
            Me.pbDescolgando.Visible = False
            Me.lblDescolgando.Visible = False
            If Me.SolicitudesDescolgarCount > 0 Then
                DesktopMessageBoxControl.DesktopMessageShow("Se realizo el descuelgue de " & Me.SolicitudesDescolgarCount.ToString & " documentos", "Proceso ejecutado exitosamente", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)

                Me.CargandoPictureBox.Visible = True
                Me.SolicitudesDataGridView.DataSource = Nothing
                Me.SolicitudSeleccionada = CInt(Me.SolicitudesDesktopComboBox.SelectedValue)
                If (Not Me.DescuelgueBackgroundWorker.IsBusy) Then
                    Me.DescuelgueBackgroundWorker.RunWorkerAsync()
                End If
                CargarFiltroSolicitudes(Me._IdEntidad, 1500)
            Else
                DesktopMessageBoxControl.DesktopMessageShow("No existen documentos para descolgar.", "No existen registros", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
            End If
        End Sub

        Private Sub btnImprimirCB_Click(sender As System.Object, e As System.EventArgs) Handles btnImprimirCB.Click
            If SolicitudesDataGridView.Rows.Count > 0 Then
                Dim dmImaging As New DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dmImaging.Connection_Open(Program.Sesion.Usuario.id)
                Dim TableCBarras = dmImaging.SchemaProcess.PA_CBarras_Impresion.DBExecute(Nothing, Nothing, CInt(Me.SolicitudesDesktopComboBox.SelectedValue), "")
                dmImaging.Connection_Close()

                Dim impresion As New Forms.CBarras.FormImprimirCBarras(TableCBarras)
                impresion.ShowDialog()
            Else
                DesktopMessageBoxControl.DesktopMessageShow("No hay registros en la grilla para imprimir", "Imprimir CB", DesktopMessageBoxControl.IconEnum.ErrorIcon, True, False)
            End If
        End Sub
#End Region

#Region " Metodos "

        Public Sub New()
            ' Llamada necesaria para el diseñador.
            InitializeComponent()
        End Sub

        Private Sub CargaFiltroEntidad()
            Dim dmArchiving As New DBArchiving.DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            Try
                dmArchiving.Connection_Open(Program.Sesion.Usuario.id)
                'Carga filtro de las Entidades
                Flag = False
                Dim tEntidades = dmArchiving.Schemadbo.CTA_Entidad_Rol_Usuario.DBFindByid_Usuario(Program.Sesion.Usuario.id).DefaultView.ToTable(True)
                'Utilities.LlenarCombo(EntidadDesktopComboBox, tEntidades, tEntidades.Columns("fk_Entidad").ColumnName, tEntidades.Columns("Nombre_Entidad").ColumnName, True, "-1", "Seleccione...")
                'CargarFiltroSolicitudes(CShort(EntidadDesktopComboBox.SelectedValue), 1500)
                Flag = True
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaFiltroEntidad", ex)
            Finally
                dmArchiving.Connection_Close()
            End Try
        End Sub

        Private Sub CargarFiltroSolicitudes(ByVal Fk_entidad As Short, ByVal Fk_Estado As Short)
            If Flag Then
                Dim dmArchiving As DBArchiving.DBArchivingDataBaseManager = Nothing
                Try
                    dmArchiving = New DBArchiving.DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
                    dmArchiving.Connection_Open(Program.Sesion.Usuario.id)

                    'consulta que trae las solicitudes que tienen documentos asociados en estado 1500(Alistamiento Custodia)
                    Dim dtSolicitudes = dmArchiving.Schemadbo.PA_Consulta_Solicitudes_entidad.DBExecute(Fk_entidad, Fk_Estado)
                    If (dtSolicitudes.Rows.Count > 0) Then
                        dtSolicitudes = dtSolicitudes.AsEnumerable().OrderBy(Function(x) x("fk_Solicitud")).CopyToDataTable()
                        SolicitudesDesktopComboBox.DataSource = dtSolicitudes
                        SolicitudesDesktopComboBox.DisplayMember = "fk_Solicitud"
                        SolicitudesDesktopComboBox.ValueMember = "fk_Solicitud"
                    End If

                Catch ex As Exception
                    DesktopMessageBoxControl.DesktopMessageShow("CargarFiltroSolicitudes", ex)
                Finally
                    If dmArchiving IsNot Nothing Then dmArchiving.Connection_Close()
                End Try
            End If

        End Sub

        Private Sub BuscarDocumentos()
            Dim dbmArchiving As New DBArchiving.DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            Try
                dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)
                Dim dtDocumentosSolicitudes As DataTable = dbmArchiving.Schemadbo.PA_Consulta_Solicitudes_Descuelgue.DBExecute(CInt(Me._IdEntidad), CInt(Me.SolicitudSeleccionada))
                Me.dtSolicitudesEncontradas = dtDocumentosSolicitudes
                If (Not DescuelgueBackgroundWorker.IsBusy) Then
                    SolicitudesDataGridView.AutoGenerateColumns = False
                    SolicitudesDataGridView.DataSource = dtDocumentosSolicitudes
                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("BuscarDocumentos", ex)
            Finally
                dbmArchiving.Connection_Close()
            End Try
        End Sub

        Private Sub Exportar()

            If SolicitudesDataGridView.RowCount > 0 Then
                Try
                    SaveFileDialog.Filter = "Archivo CSV (*.csv)|*.csv"
                    SaveFileDialog.FileName = "Archivo" & ".csv"
                    SaveFileDialog.Title = "Guardar Archivo"

                    Dim Resultado = SaveFileDialog.ShowDialog()
                    If Resultado = DialogResult.OK Then

                        Dim Archivo As String = ""

                        If SolicitudesDataGridView.RowCount > 0 Then
                            Dim fs = CType(SaveFileDialog.OpenFile(), FileStream)
                            SolicitudesDataGridView.Columns.Remove("R")
                            Utilities.DataGridViewToCsv(SolicitudesDataGridView, fs, vbTab, True)
                            Dim column As New DataGridViewCheckBoxColumn
                            column.Name = "R"
                            column.HeaderText = "R"
                            SolicitudesDataGridView.Columns.Add(column)
                            Archivo = "[" + fs.Name + "]"
                        End If

                        DesktopMessageBoxControl.DesktopMessageShow("Se exporto el archivo: " & Archivo & ".", "Reporte Exportado", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                    End If

                Catch ex As Exception
                    DesktopMessageBoxControl.DesktopMessageShow("Exportar", ex)
                End Try
            Else
                DesktopMessageBoxControl.DesktopMessageShow("No existen registros para exportar.", "No existen registros", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
            End If

        End Sub

        Private Sub GestionarItem(item As DataGridViewRow)
            Dim dmArchiving As New DBArchiving.DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)

            Try
                dmArchiving.Connection_Open(Program.Sesion.Usuario.id)
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                Dim CBarrasFile = item.Cells("CBarras_File").Value.ToString()
                Dim idItemSolicitud = item.Cells("id_Item_Solicitud").Value.ToString()

                Dim nModulo As Miharu.Desktop.Library.Config.DesktopConfig.Modulo
                nModulo = DesktopConfig.Modulo.Archiving

                Dim ProcesoCustodiaDataTable = dbmCore.SchemaConfig.TBL_Parametro_Sistema.DBFindByfk_Entidadfk_ProyectoNombre_Parametro_Sistema(0, 0, "ProcesoCustodia")

                If ProcesoCustodiaDataTable.Count = 1 Then
                    If ProcesoCustodiaDataTable(0).Valor_Parametro_Sistema = "1" Then
                        nModulo = DesktopConfig.Modulo.Imaging
                    End If
                End If

                'If CBarrasFile.Contains("-") Then
                '    nModulo = DesktopConfig.Modulo.Imaging
                'Else
                '    nModulo = DesktopConfig.Modulo.Archiving
                'End If

                Utilities.ActualizaEstadoFile(dmArchiving, dbmCore, CBarrasFile, Nothing, Nothing, nModulo, DBCore.EstadoEnum.Alistamiento_custodia, Program.Sesion.Usuario.id, Nothing)

                Dim tItemSolicitud As New DBArchiving.SchemaCustody.TBL_Solicitud_ItemType
                tItemSolicitud.fk_Estado = DBCore.EstadoEnum.Alistamiento_custodia
                dmArchiving.SchemaCustody.TBL_Solicitud_Item.DBUpdate(tItemSolicitud, Me.SolicitudSeleccionada, CShort(idItemSolicitud))

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("GestionarItem", ex)
            End Try

        End Sub

#End Region
    End Class

End Namespace