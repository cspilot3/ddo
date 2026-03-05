Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports DBSecurity

Namespace Forms

    Public Class FormDesbloquear_Usuarios

#Region " Eventos "
        Private Sub FormDesbloquear_Usuarios_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            GeneraReporteDesbloquearUsuario()
        End Sub

        Private Sub BtnDesbloquearUsr_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BtnDesbloquearUsr.Click
            DesbloquearUsuario()
        End Sub

#End Region

#Region " Metodos "
        Private Sub GeneraReporteDesbloquearUsuario()
            Dim dbmSecurity As New DBSecurityDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Security)
            Try
                dbmSecurity.Connection_Open(Program.Sesion.Usuario.id)
                Dim dtUsersLogueados = dbmSecurity.SchemaSecurity.CTA_Usuarios_Logueados.DBGet()
                DesbloquearUserDataGridView.DataSource = dtUsersLogueados

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Generar_Grilla_Desbloquear Usuario", ex)
            Finally
                dbmSecurity.Connection_Close()
            End Try
        End Sub
        Private Sub DesbloquearUsuario()
            Dim dbmSecurity As New DBSecurityDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Security)
            Try
                dbmSecurity.Connection_Open(Program.Sesion.Usuario.id)
                Dim Id_usr = dbmSecurity.SchemaSecurity.TBL_Usuario.DBFindByid_Usuario(ValidaUserDesbloquear())
                Dim usuarioType = Id_usr(0).ToTBL_UsuarioType()
                usuarioType.Logeado = False
                usuarioType.Logeo_IP = Nothing
                dbmSecurity.SchemaSecurity.TBL_Usuario.DBUpdate(usuarioType, usuarioType.id_Usuario)
                MessageBox.Show("Se ha desbloqueado el usuario con exito.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                GeneraReporteDesbloquearUsuario()
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Desbloquear Usuario", ex)
            Finally
                dbmSecurity.Connection_Close()
            End Try
        End Sub

#End Region

#Region " Funciones "
        Private Function ValidaUserDesbloquear() As Integer
            Dim id_usuariodesbloquear As Integer
            For Each row As DataGridViewRow In DesbloquearUserDataGridView.Rows
                Dim marcado As Boolean = Convert.ToBoolean(row.Cells("Column_Desbloquear").Value)
                If marcado Then
                    id_usuariodesbloquear = Convert.ToInt32(row.Cells(1).Value)
                End If
            Next

            Return id_usuariodesbloquear
        End Function
#End Region

    End Class
End Namespace