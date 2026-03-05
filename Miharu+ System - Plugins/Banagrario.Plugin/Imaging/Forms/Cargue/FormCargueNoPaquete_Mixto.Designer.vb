Imports Miharu.Desktop.Controls.DesktopTextBox
Imports Miharu.Imaging.Indexer.View.Indexacion

Namespace Imaging.Forms.Cargue
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormCargueNoPaqueteMixto
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
            Dim Rango1 As Rango = New Rango()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormCargueNoPaqueteMixto))
            Me.RutaGroupBox = New System.Windows.Forms.GroupBox()
            Me.SelectFolderButton = New System.Windows.Forms.Button()
            Me.RutaTextBox = New DesktopTextBoxControl()
            Me.CancelarButton = New System.Windows.Forms.Button()
            Me.AceptarButton = New System.Windows.Forms.Button()
            Me.FechaGroupBox = New System.Windows.Forms.GroupBox()
            Me.ComboBoxOficinas = New ComboBoxControl()
            Me.CodigoOficinaLabel = New System.Windows.Forms.Label()
            Me.FechaProcesoLabel = New System.Windows.Forms.Label()
            Me.FechaMovimientoLabel = New System.Windows.Forms.Label()
            Me.FechaMovimientoPicker = New System.Windows.Forms.DateTimePicker()
            Me.FechaProcesoPicker = New System.Windows.Forms.DateTimePicker()
            Me.Oficinas_ComboBox = New System.Windows.Forms.ComboBox()
            Me.RutaGroupBox.SuspendLayout()
            Me.FechaGroupBox.SuspendLayout()
            Me.SuspendLayout()
            '
            'RutaGroupBox
            '
            Me.RutaGroupBox.Controls.Add(Me.SelectFolderButton)
            Me.RutaGroupBox.Controls.Add(Me.RutaTextBox)
            Me.RutaGroupBox.Location = New System.Drawing.Point(12, 292)
            Me.RutaGroupBox.Name = "RutaGroupBox"
            Me.RutaGroupBox.Size = New System.Drawing.Size(355, 64)
            Me.RutaGroupBox.TabIndex = 1
            Me.RutaGroupBox.TabStop = False
            Me.RutaGroupBox.Text = "Ruta"
            '
            'SelectFolderButton
            '
            Me.SelectFolderButton.Image = Global.Banagrario.Plugin.My.Resources.Resources.folder_image
            Me.SelectFolderButton.Location = New System.Drawing.Point(305, 23)
            Me.SelectFolderButton.Name = "SelectFolderButton"
            Me.SelectFolderButton.Size = New System.Drawing.Size(27, 23)
            Me.SelectFolderButton.TabIndex = 1
            Me.SelectFolderButton.UseVisualStyleBackColor = True
            '
            'RutaTextBox
            '
            Me.RutaTextBox.Cantidad_Decimales = CType(0, Short)
            Me.RutaTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.RutaTextBox.DisabledEnter = False
            Me.RutaTextBox.DisabledTab = False
            Me.RutaTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.RutaTextBox.FocusOut = System.Drawing.Color.White
            Me.RutaTextBox.Location = New System.Drawing.Point(16, 25)
            Me.RutaTextBox.MaximumLength = CType(0, Short)
            Me.RutaTextBox.MaxLength = 0
            Me.RutaTextBox.MinimumLength = CType(0, Short)
            Me.RutaTextBox.Name = "RutaTextBox"
            Rango1.MaxValue = 9.2233720368547758E+18R
            Rango1.MinValue = 0.0R
            Me.RutaTextBox.Rango = Rango1
            Me.RutaTextBox.ShortcutsEnabled = False
            Me.RutaTextBox.Size = New System.Drawing.Size(280, 20)
            Me.RutaTextBox.TabIndex = 0
            Me.RutaTextBox.Type = DesktopTextBoxControl.TipoTextBox.Normal
            Me.RutaTextBox.Usa_Decimales = False
            '
            'CancelarButton
            '
            Me.CancelarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CancelarButton.Image = CType(resources.GetObject("CancelarButton.Image"), System.Drawing.Image)
            Me.CancelarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CancelarButton.Location = New System.Drawing.Point(264, 372)
            Me.CancelarButton.Name = "CancelarButton"
            Me.CancelarButton.Size = New System.Drawing.Size(80, 23)
            Me.CancelarButton.TabIndex = 3
            Me.CancelarButton.Text = "&Cancelar"
            Me.CancelarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'AceptarButton
            '
            Me.AceptarButton.Image = CType(resources.GetObject("AceptarButton.Image"), System.Drawing.Image)
            Me.AceptarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.AceptarButton.Location = New System.Drawing.Point(158, 372)
            Me.AceptarButton.Name = "AceptarButton"
            Me.AceptarButton.Size = New System.Drawing.Size(80, 23)
            Me.AceptarButton.TabIndex = 2
            Me.AceptarButton.Text = "&Aceptar"
            Me.AceptarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'FechaGroupBox
            '
            Me.FechaGroupBox.Controls.Add(Me.ComboBoxOficinas)
            Me.FechaGroupBox.Controls.Add(Me.CodigoOficinaLabel)
            Me.FechaGroupBox.Controls.Add(Me.FechaProcesoLabel)
            Me.FechaGroupBox.Controls.Add(Me.FechaMovimientoLabel)
            Me.FechaGroupBox.Controls.Add(Me.FechaMovimientoPicker)
            Me.FechaGroupBox.Controls.Add(Me.FechaProcesoPicker)
            Me.FechaGroupBox.Controls.Add(Me.Oficinas_ComboBox)
            Me.FechaGroupBox.Location = New System.Drawing.Point(12, 4)
            Me.FechaGroupBox.Name = "FechaGroupBox"
            Me.FechaGroupBox.Size = New System.Drawing.Size(355, 282)
            Me.FechaGroupBox.TabIndex = 14
            Me.FechaGroupBox.TabStop = False
            '
            'ComboBoxOficinas
            '
            Me.ComboBoxOficinas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.ComboBoxOficinas.Location = New System.Drawing.Point(19, 151)
            Me.ComboBoxOficinas.Name = "ComboBoxOficinas"
            Me.ComboBoxOficinas.Size = New System.Drawing.Size(313, 131)
            Me.ComboBoxOficinas.TabIndex = 15
            Me.ComboBoxOficinas.Visible = False
            '
            'CodigoOficinaLabel
            '
            Me.CodigoOficinaLabel.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.CodigoOficinaLabel.AutoSize = True
            Me.CodigoOficinaLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CodigoOficinaLabel.Location = New System.Drawing.Point(16, 118)
            Me.CodigoOficinaLabel.Name = "CodigoOficinaLabel"
            Me.CodigoOficinaLabel.Size = New System.Drawing.Size(138, 13)
            Me.CodigoOficinaLabel.TabIndex = 35
            Me.CodigoOficinaLabel.Text = "Ofícina de movimiento:"
            '
            'FechaProcesoLabel
            '
            Me.FechaProcesoLabel.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.FechaProcesoLabel.AutoSize = True
            Me.FechaProcesoLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.FechaProcesoLabel.Location = New System.Drawing.Point(16, 16)
            Me.FechaProcesoLabel.Name = "FechaProcesoLabel"
            Me.FechaProcesoLabel.Size = New System.Drawing.Size(117, 13)
            Me.FechaProcesoLabel.TabIndex = 33
            Me.FechaProcesoLabel.Text = "Fecha del Proceso:"
            '
            'FechaMovimientoLabel
            '
            Me.FechaMovimientoLabel.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.FechaMovimientoLabel.AutoSize = True
            Me.FechaMovimientoLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.FechaMovimientoLabel.Location = New System.Drawing.Point(16, 57)
            Me.FechaMovimientoLabel.Name = "FechaMovimientoLabel"
            Me.FechaMovimientoLabel.Size = New System.Drawing.Size(135, 13)
            Me.FechaMovimientoLabel.TabIndex = 32
            Me.FechaMovimientoLabel.Text = "Fecha del Movimiento:"
            '
            'FechaMovimientoPicker
            '
            Me.FechaMovimientoPicker.Location = New System.Drawing.Point(16, 73)
            Me.FechaMovimientoPicker.Name = "FechaMovimientoPicker"
            Me.FechaMovimientoPicker.Size = New System.Drawing.Size(316, 20)
            Me.FechaMovimientoPicker.TabIndex = 1
            '
            'FechaProcesoPicker
            '
            Me.FechaProcesoPicker.Location = New System.Drawing.Point(16, 34)
            Me.FechaProcesoPicker.Name = "FechaProcesoPicker"
            Me.FechaProcesoPicker.Size = New System.Drawing.Size(316, 20)
            Me.FechaProcesoPicker.TabIndex = 0
            '
            'Oficinas_ComboBox
            '
            Me.Oficinas_ComboBox.FormattingEnabled = True
            Me.Oficinas_ComboBox.Location = New System.Drawing.Point(19, 174)
            Me.Oficinas_ComboBox.Name = "Oficinas_ComboBox"
            Me.Oficinas_ComboBox.Size = New System.Drawing.Size(313, 21)
            Me.Oficinas_ComboBox.TabIndex = 18
            '
            'FormCargueNoPaquete
            '
            Me.AcceptButton = Me.AceptarButton
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.CancelarButton
            Me.ClientSize = New System.Drawing.Size(379, 407)
            Me.Controls.Add(Me.FechaGroupBox)
            Me.Controls.Add(Me.RutaGroupBox)
            Me.Controls.Add(Me.CancelarButton)
            Me.Controls.Add(Me.AceptarButton)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormCargueNoPaquete"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Cargue"
            Me.RutaGroupBox.ResumeLayout(False)
            Me.RutaGroupBox.PerformLayout()
            Me.FechaGroupBox.ResumeLayout(False)
            Me.FechaGroupBox.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents RutaGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents CancelarButton As System.Windows.Forms.Button
        Friend WithEvents AceptarButton As System.Windows.Forms.Button
        Friend WithEvents SelectFolderButton As System.Windows.Forms.Button
        Friend WithEvents RutaTextBox As DesktopTextBoxControl
        Friend WithEvents FechaGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents FechaProcesoPicker As System.Windows.Forms.DateTimePicker
        Friend WithEvents FechaMovimientoPicker As System.Windows.Forms.DateTimePicker
        Friend WithEvents FechaProcesoLabel As System.Windows.Forms.Label
        Friend WithEvents FechaMovimientoLabel As System.Windows.Forms.Label
        Friend WithEvents CodigoOficinaLabel As System.Windows.Forms.Label
        Friend WithEvents ComboBoxOficinas As ComboBoxControl
        Friend WithEvents Oficinas_ComboBox As System.Windows.Forms.ComboBox
    End Class
End Namespace