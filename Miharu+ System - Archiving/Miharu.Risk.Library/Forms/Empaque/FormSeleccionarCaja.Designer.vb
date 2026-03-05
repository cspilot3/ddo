Imports Miharu.Desktop.Controls.DesktopTextBox

Namespace Forms.Empaque
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormSeleccionarCaja
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormSeleccionarCaja))
            Me.CancelarButton = New System.Windows.Forms.Button()
            Me.AceptarButton = New System.Windows.Forms.Button()
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.LineaProcesoLabel = New System.Windows.Forms.Label()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.NuevaCajaButton = New System.Windows.Forms.Button()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.BuscarButton = New System.Windows.Forms.Button()
            Me.CajaBuscarDesktopTextBox = New DesktopTextBoxControl()
            Me.CajaListBox = New System.Windows.Forms.ListBox()
            Me.GroupBox1.SuspendLayout()
            Me.SuspendLayout()
            '
            'CancelarButton
            '
            Me.CancelarButton.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                                              Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CancelarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CancelarButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
            Me.CancelarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.Cancelar
            Me.CancelarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CancelarButton.Location = New System.Drawing.Point(235, 284)
            Me.CancelarButton.Name = "CancelarButton"
            Me.CancelarButton.Size = New System.Drawing.Size(112, 32)
            Me.CancelarButton.TabIndex = 5
            Me.CancelarButton.Text = "&Cerrar"
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
            Me.AceptarButton.Location = New System.Drawing.Point(117, 284)
            Me.AceptarButton.Name = "AceptarButton"
            Me.AceptarButton.Size = New System.Drawing.Size(112, 32)
            Me.AceptarButton.TabIndex = 4
            Me.AceptarButton.Text = "&Aceptar"
            Me.AceptarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.AceptarButton.UseVisualStyleBackColor = True
            '
            'GroupBox1
            '
            Me.GroupBox1.Controls.Add(Me.LineaProcesoLabel)
            Me.GroupBox1.Controls.Add(Me.Label2)
            Me.GroupBox1.Controls.Add(Me.NuevaCajaButton)
            Me.GroupBox1.Controls.Add(Me.Label1)
            Me.GroupBox1.Controls.Add(Me.CancelarButton)
            Me.GroupBox1.Controls.Add(Me.AceptarButton)
            Me.GroupBox1.Controls.Add(Me.BuscarButton)
            Me.GroupBox1.Controls.Add(Me.CajaBuscarDesktopTextBox)
            Me.GroupBox1.Controls.Add(Me.CajaListBox)
            Me.GroupBox1.Location = New System.Drawing.Point(3, 5)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(355, 324)
            Me.GroupBox1.TabIndex = 2
            Me.GroupBox1.TabStop = False
            Me.GroupBox1.Text = "Selección de Caja"
            '
            'LineaProcesoLabel
            '
            Me.LineaProcesoLabel.AutoSize = True
            Me.LineaProcesoLabel.Font = New System.Drawing.Font("Tahoma", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.LineaProcesoLabel.ForeColor = System.Drawing.Color.Red
            Me.LineaProcesoLabel.Location = New System.Drawing.Point(244, 17)
            Me.LineaProcesoLabel.Name = "LineaProcesoLabel"
            Me.LineaProcesoLabel.Size = New System.Drawing.Size(103, 25)
            Me.LineaProcesoLabel.TabIndex = 19
            Me.LineaProcesoLabel.Text = "0000000"
            Me.LineaProcesoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label2.Location = New System.Drawing.Point(122, 21)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(114, 14)
            Me.Label2.TabIndex = 18
            Me.Label2.Text = "Línea de Proceso:"
            '
            'NuevaCajaButton
            '
            Me.NuevaCajaButton.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                                               Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.NuevaCajaButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
            Me.NuevaCajaButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnAgregar
            Me.NuevaCajaButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.NuevaCajaButton.Location = New System.Drawing.Point(131, 78)
            Me.NuevaCajaButton.Name = "NuevaCajaButton"
            Me.NuevaCajaButton.Size = New System.Drawing.Size(121, 26)
            Me.NuevaCajaButton.TabIndex = 2
            Me.NuevaCajaButton.Text = "&Nueva Caja"
            Me.NuevaCajaButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.NuevaCajaButton.UseVisualStyleBackColor = True
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.Location = New System.Drawing.Point(17, 51)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(48, 16)
            Me.Label1.TabIndex = 0
            Me.Label1.Text = "Cajas:"
            '
            'BuscarButton
            '
            Me.BuscarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnBuscar
            Me.BuscarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.BuscarButton.Location = New System.Drawing.Point(21, 78)
            Me.BuscarButton.Name = "BuscarButton"
            Me.BuscarButton.Size = New System.Drawing.Size(104, 26)
            Me.BuscarButton.TabIndex = 1
            Me.BuscarButton.Text = "Buscar"
            Me.BuscarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.BuscarButton.UseVisualStyleBackColor = True
            '
            'CajaBuscarDesktopTextBox
            '
            Me.CajaBuscarDesktopTextBox.DisabledEnter = False
            Me.CajaBuscarDesktopTextBox.DisabledTab = False
            Me.CajaBuscarDesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.CajaBuscarDesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.CajaBuscarDesktopTextBox.Location = New System.Drawing.Point(77, 51)
            Me.CajaBuscarDesktopTextBox.Name = "CajaBuscarDesktopTextBox"
            Rango1.MaxValue = CType(2147483647, Long)
            Rango1.MinValue = CType(0, Long)
            Me.CajaBuscarDesktopTextBox.Rango = Rango1
            Me.CajaBuscarDesktopTextBox.Size = New System.Drawing.Size(270, 21)
            Me.CajaBuscarDesktopTextBox.TabIndex = 0
            Me.CajaBuscarDesktopTextBox.Type = DesktopTextBoxControl.TipoTextBox.Normal
            '
            'CajaListBox
            '
            Me.CajaListBox.FormattingEnabled = True
            Me.CajaListBox.Location = New System.Drawing.Point(21, 110)
            Me.CajaListBox.Name = "CajaListBox"
            Me.CajaListBox.Size = New System.Drawing.Size(326, 160)
            Me.CajaListBox.TabIndex = 3
            '
            'FormSeleccionarCaja
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.CancelarButton
            Me.ClientSize = New System.Drawing.Size(362, 333)
            Me.Controls.Add(Me.GroupBox1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormSeleccionarCaja"
            Me.ShowInTaskbar = False
            Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Seleccionar Caja"
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents CancelarButton As System.Windows.Forms.Button
        Friend WithEvents AceptarButton As System.Windows.Forms.Button
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents BuscarButton As System.Windows.Forms.Button
        Friend WithEvents CajaBuscarDesktopTextBox As DesktopTextBoxControl
        Friend WithEvents CajaListBox As System.Windows.Forms.ListBox
        Friend WithEvents NuevaCajaButton As System.Windows.Forms.Button
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents LineaProcesoLabel As System.Windows.Forms.Label
        Friend WithEvents Label2 As System.Windows.Forms.Label
    End Class
End Namespace