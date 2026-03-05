Imports Miharu.Desktop.Forms.Facturacion
Imports Miharu.Desktop.Library.Config
Imports DMB = Miharu.Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl
Imports DBArchiving
Imports Miharu.Desktop.Library.Config.DesktopConfig
Imports Miharu.Desktop.Library

Public Class FormConsultarEsquemaFacturacion
    Inherits Miharu.Desktop.Library.FormBase

#Region " Metodos "

    Public Sub LlenarCombos()
        Dim dmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
        dmArchiving.Connection_Open(Program.Sesion.Usuario.id)
        Dim Entidades = dmArchiving.SchemaSecurity.CTA_Entidad.DBGet()
        dmArchiving.Connection_Close()

        Utilities.LlenarCombo(EntidadDesktopComboBox, Entidades, Entidades.id_EntidadColumn.ColumnName, Entidades.Nombre_EntidadColumn.ColumnName)
        LlenarEsquemas()
    End Sub

    Public Sub LlenarEsquemas()
        Dim dmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
        dmArchiving.Connection_Open(Program.Sesion.Usuario.id)
        Dim Esquema = dmArchiving.SchemaBill.TBL_Esquema.DBGet(CShort(EntidadDesktopComboBox.SelectedValue), Nothing)
        dmArchiving.Connection_Close()

        Utilities.LlenarCombo(EsquemaDesktopComboBox, Esquema, Esquema.id_EsquemaColumn.ColumnName, Esquema.Nombre_EsquemaColumn.ColumnName)
    End Sub

#End Region

#Region " Eventos "

    Private Sub BuscarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BuscarButton.Click
        If IsNothing(EntidadDesktopComboBox.SelectedValue) = True Or IsNothing(EsquemaDesktopComboBox.SelectedValue) = True Then
            DMB.DesktopMessageShow("Debe seleccionar una entidad y un esquema para consultar", "Error buscando datos", Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.IconEnum.WarningIcon, True)
        Else
            Dim EsquemaFacturacion As New FormEsquemaFacturacion(CShort(EntidadDesktopComboBox.SelectedValue), CShort(EsquemaDesktopComboBox.SelectedValue), False)
            EsquemaFacturacion.ShowDialog()
            LlenarCombos()
        End If
    End Sub

    Private Sub NuevoButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles NuevoButton.Click
        Dim EsquemaFacturacion As New FormEsquemaFacturacion(Nothing, Nothing, True)
        EsquemaFacturacion.ShowDialog()
        LlenarCombos()
    End Sub

    Private Sub CerrarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CerrarButton.Click
        Me.Close()
    End Sub

    Private Sub EntidadDesktopComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles EntidadDesktopComboBox.SelectedIndexChanged
        LlenarEsquemas()
    End Sub

    Private Sub FormConsultarEsquemaFacturacion_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
        LlenarCombos()
    End Sub

#End Region

End Class