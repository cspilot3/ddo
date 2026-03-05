Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Controls
Imports System.Windows.Forms
Imports Slyg.Tools
Imports Miharu.Desktop.Library.Config
Imports System.Text
Imports Miharu.Security.Library.Session
Imports Miharu.Desktop.Library

Namespace Imaging.Carpeta_Unica.Forms.Soporte.Eliminacion_Cargue


    Public Class FormEliminacionCargue

#Region " Declaraciones "

        Private _Plugin As CarpetaUnicaPlugin
#End Region


#Region " Constructores "

        Public Sub New(ByVal nCarpetaUnicaDesktopPlugin As CarpetaUnicaPlugin)
            InitializeComponent()

            _Plugin = nCarpetaUnicaDesktopPlugin
        End Sub

#End Region

#Region " Eventos "
        Private Sub BtnAceptar_Click(sender As System.Object, e As System.EventArgs) Handles BtnAceptar.Click
            If Validar() Then
                Dim Respuesta = DesktopMessageBoxControl.DesktopMessageShow("¿Está seguro que desea eliminar el cargue y paquete digitado?", "Elimininación Cargue - Paquete", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, False)

                If (Respuesta = DialogResult.OK) Then
                    EliminarCargue()
                End If
            End If
           
        End Sub

        Private Sub btnSalir_Click(sender As System.Object, e As System.EventArgs) Handles btnSalir.Click
            Me.Close()
        End Sub
#End Region

#Region " Metodos "
        Private Sub EliminarCargue()
            Dim dbmIntegration As New DBIntegration.DBIntegrationDataBaseManager(Me._Plugin.CajaSocialConnectionString)

            Try
                dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                Dim Respuesta = dbmIntegration.SchemaBCSCarpetaUnica.PA_Eliminacion_Cargue.DBExecute(_Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, CInt(txtidCargue.Text.ToString()), CInt(txtidCarguePaquete.Text.ToString()), txtMotivo.Text.ToString(), _Plugin.Manager.Sesion.Usuario.id)

                If Respuesta.ToString() <> "" Then
                    MessageBox.Show(Respuesta.ToString(), "Eliminación Cargue - Paquete", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Else
                    MessageBox.Show("Cargue - Paquete eliminado con exito", "Eliminación Cargue - Paquete", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                    'Limpiar vbles
                    txtidCargue.Text = ""
                    txtidCarguePaquete.Text = ""
                    txtMotivo.Text = ""
                End If

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("EliminaciónCarguePaquete", ex)
            Finally
                If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
            End Try
        End Sub

        Private Function Validar() As Boolean
            If txtidCargue.Text = "" Then
                MessageBox.Show("Debe digitar un número de cargue", "Eliminación Cargue", MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtidCargue.Focus()
            ElseIf Not IsNumeric(txtidCargue.Text) Then
                MessageBox.Show("El número de cargue debe ser númerico", "Eliminación Cargue", MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtidCargue.Focus()
            ElseIf txtidCarguePaquete.Text = "" Then
                MessageBox.Show("Debe digitar un número de paquete", "Eliminación Cargue", MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtidCarguePaquete.Focus()
            ElseIf Not IsNumeric(txtidCarguePaquete.Text) Then
                MessageBox.Show("El número de paquete debe ser númerico", "Eliminación Cargue", MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtidCarguePaquete.Focus()
            ElseIf txtMotivo.Text = "" Then
                MessageBox.Show("Debe digitar el motivo de la eliminación del cargue - paquete", "Eliminación Cargue", MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtMotivo.Focus()
            Else
                Return True
            End If

            Return False
        End Function
#End Region

    End Class

End Namespace