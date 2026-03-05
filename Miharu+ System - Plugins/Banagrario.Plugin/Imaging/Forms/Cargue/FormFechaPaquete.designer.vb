Namespace Imaging.Forms.Cargue
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormFechaPaquete
        Inherits System.Windows.Forms.Form

        'Form reemplaza a Dispose para limpiar la lista de componentes.
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

        'Requerido por el Diseñador de Windows Forms
        Private components As System.ComponentModel.IContainer

        'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
        'Se puede modificar usando el Diseñador de Windows Forms.  
        'No lo modifique con el editor de código.
        <System.Diagnostics.DebuggerStepThrough()> _
        Private Sub InitializeComponent()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormFechaPaquete))
            Me.PaqueteGroupBox = New System.Windows.Forms.GroupBox()
            Me.PaqueteFinalTextBox = New System.Windows.Forms.TextBox()
            Me.PaqueteInicialTextBox = New System.Windows.Forms.TextBox()
            Me.DesdeLabel = New System.Windows.Forms.Label()
            Me.HastaLabel = New System.Windows.Forms.Label()
            Me.FechaGroupBox = New System.Windows.Forms.GroupBox()
            Me.FechaDateTimePicker = New System.Windows.Forms.DateTimePicker()
            Me.CancelarButton = New System.Windows.Forms.Button()
            Me.AceptarButton = New System.Windows.Forms.Button()
            Me.PaqueteGroupBox.SuspendLayout()
            Me.FechaGroupBox.SuspendLayout()
            Me.SuspendLayout()
            '
            'PaqueteGroupBox
            '
            Me.PaqueteGroupBox.Controls.Add(Me.PaqueteFinalTextBox)
            Me.PaqueteGroupBox.Controls.Add(Me.PaqueteInicialTextBox)
            Me.PaqueteGroupBox.Controls.Add(Me.DesdeLabel)
            Me.PaqueteGroupBox.Controls.Add(Me.HastaLabel)
            Me.PaqueteGroupBox.Location = New System.Drawing.Point(14, 82)
            Me.PaqueteGroupBox.Name = "PaqueteGroupBox"
            Me.PaqueteGroupBox.Size = New System.Drawing.Size(296, 100)
            Me.PaqueteGroupBox.TabIndex = 14
            Me.PaqueteGroupBox.TabStop = False
            Me.PaqueteGroupBox.Text = "Paquetes"
            '
            'PaqueteFinalTextBox
            '
            Me.PaqueteFinalTextBox.Location = New System.Drawing.Point(96, 56)
            Me.PaqueteFinalTextBox.Name = "PaqueteFinalTextBox"
            Me.PaqueteFinalTextBox.Size = New System.Drawing.Size(184, 20)
            Me.PaqueteFinalTextBox.TabIndex = 5
            Me.PaqueteFinalTextBox.Text = "99999"
            '
            'PaqueteInicialTextBox
            '
            Me.PaqueteInicialTextBox.Location = New System.Drawing.Point(96, 24)
            Me.PaqueteInicialTextBox.Name = "PaqueteInicialTextBox"
            Me.PaqueteInicialTextBox.Size = New System.Drawing.Size(184, 20)
            Me.PaqueteInicialTextBox.TabIndex = 4
            Me.PaqueteInicialTextBox.Text = "0"
            '
            'DesdeLabel
            '
            Me.DesdeLabel.Location = New System.Drawing.Point(13, 24)
            Me.DesdeLabel.Name = "DesdeLabel"
            Me.DesdeLabel.Size = New System.Drawing.Size(64, 16)
            Me.DesdeLabel.TabIndex = 2
            Me.DesdeLabel.Text = "Desde"
            '
            'HastaLabel
            '
            Me.HastaLabel.Location = New System.Drawing.Point(13, 56)
            Me.HastaLabel.Name = "HastaLabel"
            Me.HastaLabel.Size = New System.Drawing.Size(56, 16)
            Me.HastaLabel.TabIndex = 3
            Me.HastaLabel.Text = "Hasta"
            '
            'FechaGroupBox
            '
            Me.FechaGroupBox.Controls.Add(Me.FechaDateTimePicker)
            Me.FechaGroupBox.Location = New System.Drawing.Point(14, 12)
            Me.FechaGroupBox.Name = "FechaGroupBox"
            Me.FechaGroupBox.Size = New System.Drawing.Size(296, 64)
            Me.FechaGroupBox.TabIndex = 13
            Me.FechaGroupBox.TabStop = False
            Me.FechaGroupBox.Text = "Fecha"
            '
            'FechaDateTimePicker
            '
            Me.FechaDateTimePicker.Location = New System.Drawing.Point(16, 23)
            Me.FechaDateTimePicker.Name = "FechaDateTimePicker"
            Me.FechaDateTimePicker.Size = New System.Drawing.Size(264, 20)
            Me.FechaDateTimePicker.TabIndex = 0
            '
            'CancelarButton
            '
            Me.CancelarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CancelarButton.Image = CType(resources.GetObject("CancelarButton.Image"), System.Drawing.Image)
            Me.CancelarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CancelarButton.Location = New System.Drawing.Point(230, 188)
            Me.CancelarButton.Name = "CancelarButton"
            Me.CancelarButton.Size = New System.Drawing.Size(80, 23)
            Me.CancelarButton.TabIndex = 12
            Me.CancelarButton.Text = "&Cancelar"
            Me.CancelarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'AceptarButton
            '
            Me.AceptarButton.Image = CType(resources.GetObject("AceptarButton.Image"), System.Drawing.Image)
            Me.AceptarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.AceptarButton.Location = New System.Drawing.Point(134, 188)
            Me.AceptarButton.Name = "AceptarButton"
            Me.AceptarButton.Size = New System.Drawing.Size(80, 23)
            Me.AceptarButton.TabIndex = 11
            Me.AceptarButton.Text = "&Aceptar"
            Me.AceptarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'FormFechaPaquete
            '
            Me.AcceptButton = Me.AceptarButton
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.CancelarButton
            Me.ClientSize = New System.Drawing.Size(322, 222)
            Me.Controls.Add(Me.PaqueteGroupBox)
            Me.Controls.Add(Me.FechaGroupBox)
            Me.Controls.Add(Me.CancelarButton)
            Me.Controls.Add(Me.AceptarButton)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormFechaPaquete"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Fecha"
            Me.PaqueteGroupBox.ResumeLayout(False)
            Me.PaqueteGroupBox.PerformLayout()
            Me.FechaGroupBox.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents PaqueteGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents PaqueteFinalTextBox As System.Windows.Forms.TextBox
        Friend WithEvents PaqueteInicialTextBox As System.Windows.Forms.TextBox
        Friend WithEvents DesdeLabel As System.Windows.Forms.Label
        Friend WithEvents HastaLabel As System.Windows.Forms.Label
        Friend WithEvents FechaGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents FechaDateTimePicker As System.Windows.Forms.DateTimePicker
        Friend WithEvents CancelarButton As System.Windows.Forms.Button
        Friend WithEvents AceptarButton As System.Windows.Forms.Button
    End Class
End Namespace