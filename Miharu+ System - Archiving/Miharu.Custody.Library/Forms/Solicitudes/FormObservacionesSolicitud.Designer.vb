Imports Miharu.Desktop.Controls.DesktopTextBox

Namespace Forms.Solicitudes
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormObservacionesSolicitud
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
            Dim Rango1 As Rango = New Rango()
            Dim Rango2 As Rango = New Rango()
            Dim Rango3 As Rango = New Rango()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormObservacionesSolicitud))
            Me.ObservacionesGroupBox = New System.Windows.Forms.GroupBox()
            Me.CancelarButton = New System.Windows.Forms.Button()
            Me.AceptarButton = New System.Windows.Forms.Button()
            Me.ObservacionesTextBox = New DesktopTextBoxControl()
            Me.ObservacionesLabel = New System.Windows.Forms.Label()
            Me.RemitidaPorTextBox = New DesktopTextBoxControl()
            Me.RemitidaPorLabel = New System.Windows.Forms.Label()
            Me.DestinatarioTextBox = New DesktopTextBoxControl()
            Me.DestinatarioLabel = New System.Windows.Forms.Label()
            Me.FechaDateTimePicker = New System.Windows.Forms.DateTimePicker()
            Me.FechaLabel = New System.Windows.Forms.Label()
            Me.ObservacionesGroupBox.SuspendLayout()
            Me.SuspendLayout()
            '
            'ObservacionesGroupBox
            '
            Me.ObservacionesGroupBox.Controls.Add(Me.CancelarButton)
            Me.ObservacionesGroupBox.Controls.Add(Me.AceptarButton)
            Me.ObservacionesGroupBox.Controls.Add(Me.ObservacionesTextBox)
            Me.ObservacionesGroupBox.Controls.Add(Me.ObservacionesLabel)
            Me.ObservacionesGroupBox.Controls.Add(Me.RemitidaPorTextBox)
            Me.ObservacionesGroupBox.Controls.Add(Me.RemitidaPorLabel)
            Me.ObservacionesGroupBox.Controls.Add(Me.DestinatarioTextBox)
            Me.ObservacionesGroupBox.Controls.Add(Me.DestinatarioLabel)
            Me.ObservacionesGroupBox.Controls.Add(Me.FechaDateTimePicker)
            Me.ObservacionesGroupBox.Controls.Add(Me.FechaLabel)
            Me.ObservacionesGroupBox.Location = New System.Drawing.Point(13, 5)
            Me.ObservacionesGroupBox.Name = "ObservacionesGroupBox"
            Me.ObservacionesGroupBox.Size = New System.Drawing.Size(437, 244)
            Me.ObservacionesGroupBox.TabIndex = 0
            Me.ObservacionesGroupBox.TabStop = False
            Me.ObservacionesGroupBox.Text = "Observaciones"
            '
            'CancelarButton
            '
            Me.CancelarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CancelarButton.Image = Global.Miharu.Custody.Library.My.Resources.Resources.btnSalir1
            Me.CancelarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CancelarButton.Location = New System.Drawing.Point(345, 209)
            Me.CancelarButton.Name = "CancelarButton"
            Me.CancelarButton.Size = New System.Drawing.Size(86, 29)
            Me.CancelarButton.TabIndex = 9
            Me.CancelarButton.Text = "&Cancelar"
            Me.CancelarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CancelarButton.UseVisualStyleBackColor = True
            '
            'AceptarButton
            '
            Me.AceptarButton.Image = Global.Miharu.Custody.Library.My.Resources.Resources.Aceptar
            Me.AceptarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.AceptarButton.Location = New System.Drawing.Point(257, 209)
            Me.AceptarButton.Name = "AceptarButton"
            Me.AceptarButton.Size = New System.Drawing.Size(82, 29)
            Me.AceptarButton.TabIndex = 8
            Me.AceptarButton.Text = "&Aceptar"
            Me.AceptarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.AceptarButton.UseVisualStyleBackColor = True
            '
            'ObservacionesTextBox
            '
            Me.ObservacionesTextBox.DisabledEnter = False
            Me.ObservacionesTextBox.DisabledTab = False
            Me.ObservacionesTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.ObservacionesTextBox.FocusOut = System.Drawing.Color.White
            Me.ObservacionesTextBox.Location = New System.Drawing.Point(192, 118)
            Me.ObservacionesTextBox.Multiline = True
            Me.ObservacionesTextBox.Name = "ObservacionesTextBox"
            Rango1.MaxValue = CType(9223372036854775807, Long)
            Rango1.MinValue = CType(0, Long)
            Me.ObservacionesTextBox.Rango = Rango1
            Me.ObservacionesTextBox.ShortcutsEnabled = False
            Me.ObservacionesTextBox.Size = New System.Drawing.Size(239, 85)
            Me.ObservacionesTextBox.TabIndex = 7
            Me.ObservacionesTextBox.Type = DesktopTextBoxControl.TipoTextBox.Normal
            '
            'ObservacionesLabel
            '
            Me.ObservacionesLabel.AutoSize = True
            Me.ObservacionesLabel.Location = New System.Drawing.Point(10, 121)
            Me.ObservacionesLabel.Name = "ObservacionesLabel"
            Me.ObservacionesLabel.Size = New System.Drawing.Size(96, 13)
            Me.ObservacionesLabel.TabIndex = 6
            Me.ObservacionesLabel.Text = "Observaciones: "
            '
            'RemitidaPorTextBox
            '
            Me.RemitidaPorTextBox.DisabledEnter = False
            Me.RemitidaPorTextBox.DisabledTab = False
            Me.RemitidaPorTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.RemitidaPorTextBox.FocusOut = System.Drawing.Color.White
            Me.RemitidaPorTextBox.Location = New System.Drawing.Point(192, 83)
            Me.RemitidaPorTextBox.Name = "RemitidaPorTextBox"
            Rango2.MaxValue = CType(9223372036854775807, Long)
            Rango2.MinValue = CType(0, Long)
            Me.RemitidaPorTextBox.Rango = Rango2
            Me.RemitidaPorTextBox.ShortcutsEnabled = False
            Me.RemitidaPorTextBox.Size = New System.Drawing.Size(239, 21)
            Me.RemitidaPorTextBox.TabIndex = 5
            Me.RemitidaPorTextBox.Type = DesktopTextBoxControl.TipoTextBox.Normal
            '
            'RemitidaPorLabel
            '
            Me.RemitidaPorLabel.AutoSize = True
            Me.RemitidaPorLabel.Location = New System.Drawing.Point(10, 86)
            Me.RemitidaPorLabel.Name = "RemitidaPorLabel"
            Me.RemitidaPorLabel.Size = New System.Drawing.Size(137, 13)
            Me.RemitidaPorLabel.TabIndex = 4
            Me.RemitidaPorLabel.Text = "Solicitud Remitida por: "
            '
            'DestinatarioTextBox
            '
            Me.DestinatarioTextBox.DisabledEnter = False
            Me.DestinatarioTextBox.DisabledTab = False
            Me.DestinatarioTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.DestinatarioTextBox.FocusOut = System.Drawing.Color.White
            Me.DestinatarioTextBox.Location = New System.Drawing.Point(192, 49)
            Me.DestinatarioTextBox.Name = "DestinatarioTextBox"
            Rango3.MaxValue = CType(9223372036854775807, Long)
            Rango3.MinValue = CType(0, Long)
            Me.DestinatarioTextBox.Rango = Rango3
            Me.DestinatarioTextBox.ShortcutsEnabled = False
            Me.DestinatarioTextBox.Size = New System.Drawing.Size(239, 21)
            Me.DestinatarioTextBox.TabIndex = 3
            Me.DestinatarioTextBox.Type = DesktopTextBoxControl.TipoTextBox.Normal
            '
            'DestinatarioLabel
            '
            Me.DestinatarioLabel.AutoSize = True
            Me.DestinatarioLabel.Location = New System.Drawing.Point(7, 52)
            Me.DestinatarioLabel.Name = "DestinatarioLabel"
            Me.DestinatarioLabel.Size = New System.Drawing.Size(150, 13)
            Me.DestinatarioLabel.TabIndex = 2
            Me.DestinatarioLabel.Text = "Nombre del Destinatario: "
            '
            'FechaDateTimePicker
            '
            Me.FechaDateTimePicker.Location = New System.Drawing.Point(192, 17)
            Me.FechaDateTimePicker.Name = "FechaDateTimePicker"
            Me.FechaDateTimePicker.Size = New System.Drawing.Size(239, 21)
            Me.FechaDateTimePicker.TabIndex = 1
            '
            'FechaLabel
            '
            Me.FechaLabel.AutoSize = True
            Me.FechaLabel.Location = New System.Drawing.Point(7, 21)
            Me.FechaLabel.Name = "FechaLabel"
            Me.FechaLabel.Size = New System.Drawing.Size(179, 13)
            Me.FechaLabel.TabIndex = 0
            Me.FechaLabel.Text = "Fecha atención de la Solicitud: "
            '
            'FormObservacionesSolicitud
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.CancelarButton
            Me.ClientSize = New System.Drawing.Size(462, 261)
            Me.Controls.Add(Me.ObservacionesGroupBox)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormObservacionesSolicitud"
            Me.ShowInTaskbar = False
            Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Observaciones Solicitud"
            Me.ObservacionesGroupBox.ResumeLayout(False)
            Me.ObservacionesGroupBox.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents ObservacionesGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents FechaLabel As System.Windows.Forms.Label
        Friend WithEvents FechaDateTimePicker As System.Windows.Forms.DateTimePicker
        Friend WithEvents DestinatarioTextBox As DesktopTextBoxControl
        Friend WithEvents DestinatarioLabel As System.Windows.Forms.Label
        Friend WithEvents RemitidaPorLabel As System.Windows.Forms.Label
        Friend WithEvents ObservacionesTextBox As DesktopTextBoxControl
        Friend WithEvents ObservacionesLabel As System.Windows.Forms.Label
        Friend WithEvents RemitidaPorTextBox As DesktopTextBoxControl
        Friend WithEvents CancelarButton As System.Windows.Forms.Button
        Friend WithEvents AceptarButton As System.Windows.Forms.Button
    End Class
End Namespace