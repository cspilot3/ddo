Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Controls
Imports DBArchiving
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Config

Namespace Forms.Parametrizacion.Solicitudes

    Public Class FormMotivoXRol
        Inherits FormBase

#Region " Eventos "

        Private Sub FormMotivoXRol_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            LlenarCombos()
        End Sub

        Private Sub rolesDesktopComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles rolesDesktopComboBox.SelectedIndexChanged
            LlenarMotivos()
        End Sub

        Private Sub CerrarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CerrarButton.Click
            Me.Close()
        End Sub

        Private Sub guardarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles guardarButton.Click
            Guardar()
        End Sub

#End Region

#Region " Metodos "

        Public Sub LlenarCombos()
            Dim dmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dmArchiving.Connection_Open(Program.Sesion.Usuario.id)
            Dim Roles = dmArchiving.SchemaSecurity.CTA_Rol.DBGet()
            Utilities.LlenarCombo(rolesDesktopComboBox, Roles, Roles.id_RolColumn.ColumnName, Roles.Nombre_RolColumn.ColumnName)
            dmArchiving.Connection_Close()
        End Sub

        Public Sub LlenarMotivos()
            Dim dmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dmArchiving.Connection_Open(Program.Sesion.Usuario.id)
            Dim Motivos = dmArchiving.Schemadbo.PA_Solicitud_Motivo_Rol.DBExecute(CShort(rolesDesktopComboBox.SelectedValue))
            MotivosDesktopDataGridView.DataSource = Motivos
            dmArchiving.Connection_Close()
        End Sub

        Public Sub Guardar()
            Dim dmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dmArchiving.Connection_Open(Program.Sesion.Usuario.id)
            Dim id_Rol As Integer = CInt(rolesDesktopComboBox.SelectedValue)

            Try
                dmArchiving.Transaction_Begin()

                dmArchiving.SchemaCustody.TBL_Solicitud_Motivo_x_Rol.DBDelete(id_Rol, Nothing)
                For Each RowData As DataGridViewRow In MotivosDesktopDataGridView.Rows
                    If Not IsDBNull(RowData.Cells("Aplica").Value) Then
                        If CBool(RowData.Cells("Aplica").Value) Then
                            Dim id_Motivo As Short = CShort(RowData.Cells("ID").Value)
                            dmArchiving.SchemaCustody.TBL_Solicitud_Motivo_x_Rol.DBInsert(id_Rol, id_Motivo)
                        End If
                    End If
                Next

                dmArchiving.Transaction_Commit()
                DesktopMessageBoxControl.DesktopMessageShow("Los datos se han guardado con exito", "Motivos por rol OK", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
            Catch ex As Exception
                dmArchiving.Transaction_Rollback()
                DesktopMessageBoxControl.DesktopMessageShow("Guardar", ex)
            End Try

            dmArchiving.Connection_Close()
        End Sub

#End Region

    End Class

End Namespace