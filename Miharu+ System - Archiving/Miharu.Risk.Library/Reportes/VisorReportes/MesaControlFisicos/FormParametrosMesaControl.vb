Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Controls
Imports DBArchiving

Namespace Reportes.VisorReportes.MesaControlFisicos

    Public Class FormParametrosMesaControl
        Inherits Desktop.Library.FormBase

#Region " Eventos "

        Private Sub FormReporteReprocesos_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            CargarCombos()
        End Sub

        Private Sub ClienteDesktopComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles ClienteDesktopComboBox.SelectedIndexChanged
            Dim Archiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            Archiving.Connection_Open(Program.Sesion.Usuario.id)

            Try
                Dim Proyecto = Archiving.Schemadbo.CTA_Proyecto.DBFindByfk_Entidad(CShort(ClienteDesktopComboBox.SelectedValue))
                ProyectoDesktopComboBox.DataSource = Proyecto
                ProyectoDesktopComboBox.ValueMember = Proyecto.id_ProyectoColumn.ColumnName
                ProyectoDesktopComboBox.DisplayMember = Proyecto.Nombre_ProyectoColumn.ColumnName

                Dim Esquema = Archiving.Schemadbo.CTA_Esquema.DBFindByfk_entidadfk_proyecto(CShort(ClienteDesktopComboBox.SelectedValue), CShort(ProyectoDesktopComboBox.SelectedValue))
                EsquemaDesktopComboBox.DataSource = Esquema
                EsquemaDesktopComboBox.ValueMember = Esquema.fk_esquemaColumn.ColumnName
                EsquemaDesktopComboBox.DisplayMember = Esquema.Nombre_esquemaColumn.ColumnName
            Catch : End Try

            Archiving.Connection_Close()
        End Sub

        Private Sub ProyectoDesktopComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles ProyectoDesktopComboBox.SelectedIndexChanged
            Dim Archiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            Archiving.Connection_Open(Program.Sesion.Usuario.id)

            Try
                Dim Esquema = Archiving.Schemadbo.CTA_Esquema.DBFindByfk_entidadfk_proyecto(CShort(ClienteDesktopComboBox.SelectedValue), CShort(ProyectoDesktopComboBox.SelectedValue))
                EsquemaDesktopComboBox.DataSource = Esquema
                EsquemaDesktopComboBox.ValueMember = Esquema.fk_esquemaColumn.ColumnName
                EsquemaDesktopComboBox.DisplayMember = Esquema.Nombre_esquemaColumn.ColumnName
            Catch : End Try

            Archiving.Connection_Close()
        End Sub

        Private Sub BuscarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BuscarButton.Click
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        End Sub
#End Region

#Region " Metodos "

        Public Sub CargarCombos()
            Try
                Dim Archiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
                Archiving.Connection_Open(Program.Sesion.Usuario.id)

                Dim Entidad = Archiving.SchemaSecurity.CTA_Entidad.DBGet()
                ClienteDesktopComboBox.DataSource = Entidad
                ClienteDesktopComboBox.ValueMember = Entidad.id_EntidadColumn.ColumnName
                ClienteDesktopComboBox.DisplayMember = Entidad.Nombre_EntidadColumn.ColumnName

                Dim Proyecto = Archiving.Schemadbo.CTA_Proyecto.DBFindByfk_Entidad(CShort(ClienteDesktopComboBox.SelectedValue))
                ProyectoDesktopComboBox.DataSource = Proyecto
                ProyectoDesktopComboBox.ValueMember = Proyecto.id_ProyectoColumn.ColumnName
                ProyectoDesktopComboBox.DisplayMember = Proyecto.Nombre_ProyectoColumn.ColumnName

                Dim Esquema = Archiving.Schemadbo.CTA_Esquema.DBFindByfk_entidadfk_proyecto(CShort(ClienteDesktopComboBox.SelectedValue), CShort(ProyectoDesktopComboBox.SelectedValue))
                EsquemaDesktopComboBox.DataSource = Esquema
                EsquemaDesktopComboBox.ValueMember = Esquema.fk_esquemaColumn.ColumnName
                EsquemaDesktopComboBox.DisplayMember = Esquema.Nombre_esquemaColumn.ColumnName

                Archiving.Connection_Close()
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargarCombos", ex)
            End Try
        End Sub
#End Region

    End Class

End Namespace