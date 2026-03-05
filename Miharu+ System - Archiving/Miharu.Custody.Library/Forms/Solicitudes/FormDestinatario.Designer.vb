Imports Miharu.Desktop.Controls.DesktopCBarras
Imports Miharu.Desktop.Controls.DesktopDataGridView
Imports Miharu.Desktop.Controls.DesktopTextBox
Imports Miharu.Desktop.Controls.DesktopComboBox

Namespace Forms.Solicitudes
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormDestinatario
        Inherits System.Windows.Forms.Form

        'Form overrides dispose to clean up the component list.
        <System.Diagnostics.DebuggerNonUserCode()> _
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            Try
                If disposing AndAlso components IsNot Nothing Then
                    components.Dispose()
                End If
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub

        'Required by the Windows Form Designer
        Private components As System.ComponentModel.IContainer

        'NOTE: The following procedure is required by the Windows Form Designer
        'It can be modified using the Windows Form Designer.  
        'Do not modify it using the code editor.
        <System.Diagnostics.DebuggerStepThrough()> _
        Private Sub InitializeComponent()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormDestinatario))
            Me.PrecintoTextBox = New System.Windows.Forms.MaskedTextBox()
            Me.DireccionTextBox = New System.Windows.Forms.MaskedTextBox()
            Me.SedeTextBox = New System.Windows.Forms.MaskedTextBox()
            Me.NombresTextBox = New System.Windows.Forms.MaskedTextBox()
            Me.CancelarButton = New System.Windows.Forms.Button()
            Me.AceptarButton = New System.Windows.Forms.Button()
            Me.Label5 = New System.Windows.Forms.Label()
            Me.Label4 = New System.Windows.Forms.Label()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.SuspendLayout()
            '
            'PrecintoTextBox
            '
            resources.ApplyResources(Me.PrecintoTextBox, "PrecintoTextBox")
            Me.PrecintoTextBox.Name = "PrecintoTextBox"
            '
            'DireccionTextBox
            '
            resources.ApplyResources(Me.DireccionTextBox, "DireccionTextBox")
            Me.DireccionTextBox.Name = "DireccionTextBox"
            '
            'SedeTextBox
            '
            resources.ApplyResources(Me.SedeTextBox, "SedeTextBox")
            Me.SedeTextBox.Name = "SedeTextBox"
            '
            'NombresTextBox
            '
            resources.ApplyResources(Me.NombresTextBox, "NombresTextBox")
            Me.NombresTextBox.Name = "NombresTextBox"
            '
            'CancelarButton
            '
            Me.CancelarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CancelarButton.Image = Global.Miharu.Custody.Library.My.Resources.Resources.btnSalir1
            resources.ApplyResources(Me.CancelarButton, "CancelarButton")
            Me.CancelarButton.Name = "CancelarButton"
            Me.CancelarButton.UseVisualStyleBackColor = True
            '
            'AceptarButton
            '
            Me.AceptarButton.Image = Global.Miharu.Custody.Library.My.Resources.Resources.Aceptar
            resources.ApplyResources(Me.AceptarButton, "AceptarButton")
            Me.AceptarButton.Name = "AceptarButton"
            Me.AceptarButton.UseVisualStyleBackColor = True
            '
            'Label5
            '
            resources.ApplyResources(Me.Label5, "Label5")
            Me.Label5.Name = "Label5"
            '
            'Label4
            '
            resources.ApplyResources(Me.Label4, "Label4")
            Me.Label4.Name = "Label4"
            '
            'Label3
            '
            resources.ApplyResources(Me.Label3, "Label3")
            Me.Label3.Name = "Label3"
            '
            'Label2
            '
            resources.ApplyResources(Me.Label2, "Label2")
            Me.Label2.Name = "Label2"
            '
            'Label1
            '
            resources.ApplyResources(Me.Label1, "Label1")
            Me.Label1.BackColor = System.Drawing.SystemColors.ButtonFace
            Me.Label1.ForeColor = System.Drawing.Color.ForestGreen
            Me.Label1.Name = "Label1"
            '
            'FormDestinatario
            '
            resources.ApplyResources(Me, "$this")
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.SystemColors.Control
            Me.Controls.Add(Me.PrecintoTextBox)
            Me.Controls.Add(Me.DireccionTextBox)
            Me.Controls.Add(Me.SedeTextBox)
            Me.Controls.Add(Me.NombresTextBox)
            Me.Controls.Add(Me.CancelarButton)
            Me.Controls.Add(Me.AceptarButton)
            Me.Controls.Add(Me.Label5)
            Me.Controls.Add(Me.Label4)
            Me.Controls.Add(Me.Label3)
            Me.Controls.Add(Me.Label2)
            Me.Controls.Add(Me.Label1)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormDestinatario"
            Me.ShowInTaskbar = False
            Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents PrecintoTextBox As System.Windows.Forms.MaskedTextBox
        Friend WithEvents DireccionTextBox As System.Windows.Forms.MaskedTextBox
        Friend WithEvents SedeTextBox As System.Windows.Forms.MaskedTextBox
        Friend WithEvents NombresTextBox As System.Windows.Forms.MaskedTextBox
        Friend WithEvents CancelarButton As System.Windows.Forms.Button
        Friend WithEvents AceptarButton As System.Windows.Forms.Button
        Friend WithEvents Label5 As System.Windows.Forms.Label
        Friend WithEvents Label4 As System.Windows.Forms.Label
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents Label1 As System.Windows.Forms.Label
    End Class
End Namespace