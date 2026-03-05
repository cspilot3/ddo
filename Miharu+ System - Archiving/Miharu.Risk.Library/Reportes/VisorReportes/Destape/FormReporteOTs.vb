Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library.Config
Imports DBArchiving

Namespace Reportes.VisorReportes.Destape

    Public Class FormReporteOTs
        Inherits Desktop.Library.FormBase

#Region " Eventos "

        Private Sub BuscarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BuscarButton.Click
            BuscarOts()
        End Sub

        Private Sub OTsListBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles OTsListBox.SelectedIndexChanged
            seleccionarOT()
        End Sub

#End Region

#Region " Metodos "

        Public Sub BuscarOts()
            Dim dbmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

            If Fecha1DateTimePicker.Text = "" Or Fecha2DateTimePicker.Text = "" Then
                DesktopMessageBoxControl.DesktopMessageShow("Debe filtrar por un rango de fechas", "Error en filtros", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
            Else
                Dim TableOts = dbmArchiving.Schemadbo.PA_RPT_OTs.DBExecute(Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, CDate(Fecha1DateTimePicker.Text), CDate(Fecha2DateTimePicker.Text))
                OTsListBox.DataSource = TableOts
                OTsListBox.ValueMember = "id_OT"
                OTsListBox.DisplayMember = "id_OT"
            End If

            dbmArchiving.Connection_Close()
        End Sub

        Public Sub seleccionarOT()
            Dim dbmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

            Try
                Dim OT = CInt(OTsListBox.SelectedValue)

                Dim EncabezadoOT = dbmArchiving.Schemadbo.RPT_OTs_Encabezado.DBFindByid_Otid_Entidadid_Proyecto(OT, Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto)
                Dim TotalesOT = dbmArchiving.Schemadbo.RPT_OTs_Totales.DBFindByfk_Entidadfk_Proyectoid_OT(Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, OT)
                Dim DetalleOT = dbmArchiving.Schemadbo.RPT_OTs_Detalle.DBFindByfk_Entidadfk_Proyectoid_OT(Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, OT)

                ReportViewer.LocalReport.DataSources.Clear()

                Utilities.NewDataSource(ReportViewer, "Encabezado", EncabezadoOT)
                Utilities.NewDataSource(ReportViewer, "Totales", TotalesOT)
                Utilities.NewDataSource(ReportViewer, "Detalle", DetalleOT)
                Me.ReportViewer.RefreshReport()

            Catch ex As Exception

            Finally
                dbmArchiving.Connection_Close()
            End Try
        End Sub

#End Region

    End Class

End Namespace