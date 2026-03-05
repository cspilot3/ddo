Imports Miharu.Desktop.Controls.DesktopCBarrasCajaControl
Imports Miharu.Desktop.Controls.DesktopTextBox
Imports Miharu.Desktop.Controls.DesktopComboBox

Namespace Forms.Empaque
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormCrearCaja
        Inherits Miharu.Desktop.Library.FormBase

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
            Dim Rango1 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormCrearCaja))
            Me.CancelarButton = New System.Windows.Forms.Button()
            Me.AceptarButton = New System.Windows.Forms.Button()
            Me.CBarrasDesktopTextBox = New DesktopCBarrasCajaControl()
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.TipoCajaDesktopComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.GroupBox1.SuspendLayout()
            Me.SuspendLayout()
            '
            'CancelarButton
            '
            Me.CancelarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CancelarButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
            Me.CancelarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.Cancelar
            Me.CancelarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CancelarButton.Location = New System.Drawing.Point(222, 90)
            Me.CancelarButton.Name = "CancelarButton"
            Me.CancelarButton.Size = New System.Drawing.Size(93, 32)
            Me.CancelarButton.TabIndex = 3
            Me.CancelarButton.Text = "&Cerrar"
            Me.CancelarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CancelarButton.UseVisualStyleBackColor = True
            '
            'AceptarButton
            '
            Me.AceptarButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
            Me.AceptarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.Aceptar
            Me.AceptarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.AceptarButton.Location = New System.Drawing.Point(123, 90)
            Me.AceptarButton.Name = "AceptarButton"
            Me.AceptarButton.Size = New System.Drawing.Size(93, 32)
            Me.AceptarButton.TabIndex = 2
            Me.AceptarButton.Text = "&Aceptar"
            Me.AceptarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.AceptarButton.UseVisualStyleBackColor = True
            '
            'CBarrasDesktopTextBox
            '
            Me.CBarrasDesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.CBarrasDesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.CBarrasDesktopTextBox.Location = New System.Drawing.Point(114, 24)
            Me.CBarrasDesktopTextBox.Name = "CBarrasDesktopTextBox"
            Me.CBarrasDesktopTextBox.Size = New System.Drawing.Size(201, 21)
            Me.CBarrasDesktopTextBox.TabIndex = 0
            '
            'GroupBox1
            '
            Me.GroupBox1.Controls.Add(Me.Label3)
            Me.GroupBox1.Controls.Add(Me.Label2)
            Me.GroupBox1.Controls.Add(Me.TipoCajaDesktopComboBox)
            Me.GroupBox1.Controls.Add(Me.CancelarButton)
            Me.GroupBox1.Controls.Add(Me.AceptarButton)
            Me.GroupBox1.Controls.Add(Me.CBarrasDesktopTextBox)
            Me.GroupBox1.Location = New System.Drawing.Point(9, 9)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(331, 134)
            Me.GroupBox1.TabIndex = 9
            Me.GroupBox1.TabStop = False
            Me.GroupBox1.Text = "Datos de la Caja"
            '
            'Label3
            '
            Me.Label3.AutoSize = True
            Me.Label3.Location = New System.Drawing.Point(6, 60)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(62, 13)
            Me.Label3.TabIndex = 31
            Me.Label3.Text = "Tipo Caja:"
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Location = New System.Drawing.Point(6, 27)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(105, 13)
            Me.Label2.TabIndex = 30
            Me.Label2.Text = "Código de barras:"
            '
            'TipoCajaDesktopComboBox
            '
            Me.TipoCajaDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.TipoCajaDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.TipoCajaDesktopComboBox.DisabledEnter = False
            Me.TipoCajaDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.TipoCajaDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.TipoCajaDesktopComboBox.FormattingEnabled = True
            Me.TipoCajaDesktopComboBox.Location = New System.Drawing.Point(114, 52)
            Me.TipoCajaDesktopComboBox.Name = "TipoCajaDesktopComboBox"
            Me.TipoCajaDesktopComboBox.Size = New System.Drawing.Size(201, 21)
            Me.TipoCajaDesktopComboBox.TabIndex = 1
            '
            'FormCrearCaja
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.CancelarButton
            Me.ClientSize = New System.Drawing.Size(352, 153)
            Me.Controls.Add(Me.GroupBox1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormCrearCaja"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Crear Caja"
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents CancelarButton As System.Windows.Forms.Button
        Friend WithEvents AceptarButton As System.Windows.Forms.Button
        Friend WithEvents CBarrasDesktopTextBox As DesktopCBarrasCajaControl
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents TipoCajaDesktopComboBox As DesktopComboBoxControl
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents Label2 As System.Windows.Forms.Label
    End Class
End Namespace