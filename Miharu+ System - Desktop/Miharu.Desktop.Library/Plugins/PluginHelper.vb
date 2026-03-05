Imports System.Windows.Forms
Imports System.Configuration

Namespace Plugins

    Public Class PluginHelper

        Public Shared Function CloneButton(ByVal nOriginalButton As Button) As Button

            Dim clone As New Button()
            clone.Anchor = nOriginalButton.Anchor
            clone.BackColor = nOriginalButton.BackColor
            clone.Enabled = nOriginalButton.Enabled
            clone.FlatStyle = nOriginalButton.FlatStyle
            clone.Font = nOriginalButton.Font
            clone.Image = nOriginalButton.Image
            clone.ImageAlign = nOriginalButton.ImageAlign
            clone.Location = nOriginalButton.Location
            clone.Name = nOriginalButton.Name
            clone.Size = nOriginalButton.Size
            clone.TabIndex = nOriginalButton.TabIndex
            clone.Text = nOriginalButton.Text
            clone.TextAlign = nOriginalButton.TextAlign
            clone.UseVisualStyleBackColor = nOriginalButton.UseVisualStyleBackColor
            clone.Visible = nOriginalButton.Visible
            Return clone
        End Function

        Public Shared Sub ReplaceControl(ByVal nOriginalControl As Control, ByVal nNewControl As Control)

            Dim nContainer As Control = nOriginalControl.Parent
            'nContainer.Controls.Remove(nOriginalControl);

            nOriginalControl.Visible = False
            nOriginalControl.Enabled = False

            nContainer.Controls.Add(nNewControl)
        End Sub

        Public Shared Sub DisableControl(ByVal nControl As Control)
            nControl.Visible = False
            nControl.Enabled = False
        End Sub

        Public Shared Sub ControlarEventoCerrarVentanaTeclaEscape(ByVal nForm As Form)
            AgregarEventosContenedor(nForm)
        End Sub

        Private Shared Sub AgregarEventosContenedor(ByVal nControl As Control)
            If (Not nControl Is Nothing) Then
                AddHandler nControl.KeyDown, AddressOf Controlador_KeyDown

                If (Not nControl.Controls Is Nothing) Then
                    For Each ctr As Control In nControl.Controls
                        AgregarEventosContenedor(ctr)
                    Next
                End If
            End If

        End Sub

        Private Shared Sub Controlador_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
            If (e.KeyCode = Keys.Escape) Then
                If (MessageBox.Show("¿Desea cerrar la ventana?", "Cerrar ventana", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) = DialogResult.Yes) Then
                    Dim frm = DirectCast(sender, Control).FindForm()
                    frm.DialogResult = DialogResult.Cancel
                    frm.Close()
                End If
            End If
        End Sub

        Public Shared Sub InicializarConfiguracion(nPropertyObject As Object, nConfigurationPrefix As String)
            Dim props = nPropertyObject.GetType.GetProperties()
            For Each prop In props
                If (prop.CanWrite()) Then
                    Try
                        Dim stringValue = ConfigurationManager.AppSettings.Get(nConfigurationPrefix & prop.Name)

                        If (Not stringValue Is Nothing AndAlso stringValue.ToString().Trim() <> "") Then
                            Dim val = Convert.ChangeType(stringValue, prop.PropertyType)
                            prop.SetValue(nPropertyObject, val, Nothing)
                        End If
                    Catch : End Try
                End If
            Next
        End Sub

    End Class

End Namespace