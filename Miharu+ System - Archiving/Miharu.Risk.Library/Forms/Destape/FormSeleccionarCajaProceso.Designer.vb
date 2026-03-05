Imports Miharu.Desktop.Controls.DesktopTextBox

Namespace Forms.Destape
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormSeleccionarCajaProceso
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormSeleccionarCajaProceso))
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.CancelarButton = New System.Windows.Forms.Button()
            Me.AceptarButton = New System.Windows.Forms.Button()
            Me.BuscarButton = New System.Windows.Forms.Button()
            Me.CajaBuscarDesktopTextBox = New DesktopTextBoxControl()
            Me.cajaListBox = New System.Windows.Forms.ListBox()
            Me.GroupBox1.SuspendLayout()
            Me.SuspendLayout()
            '
            'GroupBox1
            '
            Me.GroupBox1.Controls.Add(Me.CancelarButton)
            Me.GroupBox1.Controls.Add(Me.AceptarButton)
            Me.GroupBox1.Controls.Add(Me.BuscarButton)
            Me.GroupBox1.Controls.Add(Me.CajaBuscarDesktopTextBox)
            Me.GroupBox1.Controls.Add(Me.cajaListBox)
            Me.GroupBox1.Location = New System.Drawing.Point(3, 3)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(334, 298)
            Me.GroupBox1.TabIndex = 2
            Me.GroupBox1.TabStop = False
            Me.GroupBox1.Text = "Cajas de Proceso"
            '
            'CancelarButton
            '
            Me.CancelarButton.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                                              Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CancelarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CancelarButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
            Me.CancelarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.Cancelar
            Me.CancelarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CancelarButton.Location = New System.Drawing.Point(220, 259)
            Me.CancelarButton.Name = "CancelarButton"
            Me.CancelarButton.Size = New System.Drawing.Size(91, 32)
            Me.CancelarButton.TabIndex = 4
            Me.CancelarButton.Text = "&Cancelar"
            Me.CancelarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CancelarButton.UseVisualStyleBackColor = True
            '
            'AceptarButton
            '
            Me.AceptarButton.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                                             Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.AceptarButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
            Me.AceptarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.Aceptar
            Me.AceptarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.AceptarButton.Location = New System.Drawing.Point(123, 260)
            Me.AceptarButton.Name = "AceptarButton"
            Me.AceptarButton.Size = New System.Drawing.Size(91, 32)
            Me.AceptarButton.TabIndex = 3
            Me.AceptarButton.Text = "&Aceptar"
            Me.AceptarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.AceptarButton.UseVisualStyleBackColor = True
            '
            'BuscarButton
            '
            Me.BuscarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnBuscar
            Me.BuscarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.BuscarButton.Location = New System.Drawing.Point(235, 25)
            Me.BuscarButton.Name = "BuscarButton"
            Me.BuscarButton.Size = New System.Drawing.Size(76, 24)
            Me.BuscarButton.TabIndex = 1
            Me.BuscarButton.Text = "Buscar"
            Me.BuscarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.BuscarButton.UseVisualStyleBackColor = True
            '
            'CajaBuscarDesktopTextBox
            '
            Me.CajaBuscarDesktopTextBox.DisabledEnter = True
            Me.CajaBuscarDesktopTextBox.DisabledTab = False
            Me.CajaBuscarDesktopTextBox.EnabledShortCuts = False
            Me.CajaBuscarDesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.CajaBuscarDesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.CajaBuscarDesktopTextBox.Location = New System.Drawing.Point(12, 25)
            Me.CajaBuscarDesktopTextBox.Name = "CajaBuscarDesktopTextBox"
            Rango1.MaxValue = CType(2147483647, Long)
            Rango1.MinValue = CType(0, Long)
            Me.CajaBuscarDesktopTextBox.Rango = Rango1
            Me.CajaBuscarDesktopTextBox.ShortcutsEnabled = False
            Me.CajaBuscarDesktopTextBox.Size = New System.Drawing.Size(217, 21)
            Me.CajaBuscarDesktopTextBox.TabIndex = 0
            Me.CajaBuscarDesktopTextBox.Type = DesktopTextBoxControl.TipoTextBox.Normal
            '
            'cajaListBox
            '
            Me.cajaListBox.FormattingEnabled = True
            Me.cajaListBox.Location = New System.Drawing.Point(12, 53)
            Me.cajaListBox.Name = "cajaListBox"
            Me.cajaListBox.Size = New System.Drawing.Size(299, 199)
            Me.cajaListBox.TabIndex = 2
            '
            'FormSeleccionarCajaProceso
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.CancelarButton
            Me.ClientSize = New System.Drawing.Size(342, 306)
            Me.Controls.Add(Me.GroupBox1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "FormSeleccionarCajaProceso"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Caja de Proceso"
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents CancelarButton As System.Windows.Forms.Button
        Friend WithEvents AceptarButton As System.Windows.Forms.Button
        Friend WithEvents BuscarButton As System.Windows.Forms.Button
        Friend WithEvents CajaBuscarDesktopTextBox As DesktopTextBoxControl
        Friend WithEvents cajaListBox As System.Windows.Forms.ListBox
    End Class
End Namespace