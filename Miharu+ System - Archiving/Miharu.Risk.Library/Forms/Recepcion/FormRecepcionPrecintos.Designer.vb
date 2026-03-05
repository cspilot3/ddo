Imports Miharu.Desktop.Library

Namespace Forms.Recepcion

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormRecepcionPrecintos
        Inherits FormBase

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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormRecepcionPrecintos))
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.RecepcionarButton = New System.Windows.Forms.Button()
            Me.PrecintosListBox = New System.Windows.Forms.ListBox()
            Me.FechaDateTimePicker = New System.Windows.Forms.DateTimePicker()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.GroupBox1.SuspendLayout()
            Me.SuspendLayout()
            '
            'GroupBox1
            '
            Me.GroupBox1.Controls.Add(Me.CerrarButton)
            Me.GroupBox1.Controls.Add(Me.RecepcionarButton)
            Me.GroupBox1.Controls.Add(Me.PrecintosListBox)
            Me.GroupBox1.Controls.Add(Me.FechaDateTimePicker)
            Me.GroupBox1.Controls.Add(Me.Label2)
            Me.GroupBox1.Controls.Add(Me.Label1)
            Me.GroupBox1.Location = New System.Drawing.Point(3, 1)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(319, 308)
            Me.GroupBox1.TabIndex = 0
            Me.GroupBox1.TabStop = False
            '
            'CerrarButton
            '
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnSalir
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(205, 268)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(106, 30)
            Me.CerrarButton.TabIndex = 3
            Me.CerrarButton.Text = "&Cerrar"
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'RecepcionarButton
            '
            Me.RecepcionarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnRecepcion
            Me.RecepcionarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.RecepcionarButton.Location = New System.Drawing.Point(93, 268)
            Me.RecepcionarButton.Name = "RecepcionarButton"
            Me.RecepcionarButton.Size = New System.Drawing.Size(106, 30)
            Me.RecepcionarButton.TabIndex = 2
            Me.RecepcionarButton.Text = "&Recepcionar"
            Me.RecepcionarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.RecepcionarButton.UseVisualStyleBackColor = True
            '
            'PrecintosListBox
            '
            Me.PrecintosListBox.FormattingEnabled = True
            Me.PrecintosListBox.Location = New System.Drawing.Point(13, 92)
            Me.PrecintosListBox.Name = "PrecintosListBox"
            Me.PrecintosListBox.Size = New System.Drawing.Size(298, 160)
            Me.PrecintosListBox.TabIndex = 1
            '
            'FechaDateTimePicker
            '
            Me.FechaDateTimePicker.Location = New System.Drawing.Point(9, 38)
            Me.FechaDateTimePicker.Name = "FechaDateTimePicker"
            Me.FechaDateTimePicker.Size = New System.Drawing.Size(298, 21)
            Me.FechaDateTimePicker.TabIndex = 0
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label2.Location = New System.Drawing.Point(9, 70)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(85, 19)
            Me.Label2.TabIndex = 1
            Me.Label2.Text = "Precintos"
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.Location = New System.Drawing.Point(9, 16)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(56, 19)
            Me.Label1.TabIndex = 0
            Me.Label1.Text = "Fecha"
            '
            'FormRecepcionPrecintos
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.SystemColors.Control
            Me.CancelButton = Me.CerrarButton
            Me.ClientSize = New System.Drawing.Size(329, 315)
            Me.Controls.Add(Me.GroupBox1)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormRecepcionPrecintos"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Recepcion de precintos"
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents RecepcionarButton As System.Windows.Forms.Button
        Friend WithEvents PrecintosListBox As System.Windows.Forms.ListBox
        Friend WithEvents FechaDateTimePicker As System.Windows.Forms.DateTimePicker
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents Label1 As System.Windows.Forms.Label
    End Class
End Namespace