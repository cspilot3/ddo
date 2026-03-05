Imports Miharu.Desktop.Controls.DesktopTextBox

Namespace Forms.CorreccionDatos
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormCorreccionData
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormCorreccionData))
            Me.DataFlowLayoutPanel = New System.Windows.Forms.FlowLayoutPanel()
            Me.AceptarButton = New System.Windows.Forms.Button()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.DatosGroupBox = New System.Windows.Forms.GroupBox()
            Me.ValidacionesGroupBox = New System.Windows.Forms.GroupBox()
            Me.ValidacionesFlowLayoutPanel = New System.Windows.Forms.FlowLayoutPanel()
            Me.ReimpresionCBarrasButton = New System.Windows.Forms.Button()
            Me.SplitContainerData = New System.Windows.Forms.SplitContainer()
            Me.GroupBox2 = New System.Windows.Forms.GroupBox()
            Me.LlavesFlowLayoutPanel = New System.Windows.Forms.FlowLayoutPanel()
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.MontoDesktopTextBox = New DesktopTextBoxControl()
            Me.FoliosDesktopTextBox = New DesktopTextBoxControl()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.DatosGroupBox.SuspendLayout()
            Me.ValidacionesGroupBox.SuspendLayout()
            CType(Me.SplitContainerData, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SplitContainerData.Panel1.SuspendLayout()
            Me.SplitContainerData.Panel2.SuspendLayout()
            Me.SplitContainerData.SuspendLayout()
            Me.GroupBox2.SuspendLayout()
            Me.GroupBox1.SuspendLayout()
            Me.SuspendLayout()
            '
            'DataFlowLayoutPanel
            '
            Me.DataFlowLayoutPanel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                                                    Or System.Windows.Forms.AnchorStyles.Left) _
                                                   Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.DataFlowLayoutPanel.AutoScroll = True
            Me.DataFlowLayoutPanel.Location = New System.Drawing.Point(15, 33)
            Me.DataFlowLayoutPanel.Name = "DataFlowLayoutPanel"
            Me.DataFlowLayoutPanel.Size = New System.Drawing.Size(428, 214)
            Me.DataFlowLayoutPanel.TabIndex = 2
            '
            'AceptarButton
            '
            Me.AceptarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.AceptarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnGuardar
            Me.AceptarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.AceptarButton.Location = New System.Drawing.Point(576, 433)
            Me.AceptarButton.Name = "AceptarButton"
            Me.AceptarButton.Size = New System.Drawing.Size(100, 30)
            Me.AceptarButton.TabIndex = 8
            Me.AceptarButton.Text = "&Guardar"
            Me.AceptarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.AceptarButton.UseVisualStyleBackColor = True
            '
            'CerrarButton
            '
            Me.CerrarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnSalir
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(684, 433)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(100, 30)
            Me.CerrarButton.TabIndex = 9
            Me.CerrarButton.Text = "&Cerrar"
            Me.CerrarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'DatosGroupBox
            '
            Me.DatosGroupBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                                              Or System.Windows.Forms.AnchorStyles.Left) _
                                             Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.DatosGroupBox.Controls.Add(Me.DataFlowLayoutPanel)
            Me.DatosGroupBox.Location = New System.Drawing.Point(10, 137)
            Me.DatosGroupBox.Name = "DatosGroupBox"
            Me.DatosGroupBox.Size = New System.Drawing.Size(450, 253)
            Me.DatosGroupBox.TabIndex = 1
            Me.DatosGroupBox.TabStop = False
            Me.DatosGroupBox.Text = "Datos"
            '
            'ValidacionesGroupBox
            '
            Me.ValidacionesGroupBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                                                     Or System.Windows.Forms.AnchorStyles.Left) _
                                                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ValidacionesGroupBox.Controls.Add(Me.ValidacionesFlowLayoutPanel)
            Me.ValidacionesGroupBox.Location = New System.Drawing.Point(10, 114)
            Me.ValidacionesGroupBox.Name = "ValidacionesGroupBox"
            Me.ValidacionesGroupBox.Size = New System.Drawing.Size(271, 276)
            Me.ValidacionesGroupBox.TabIndex = 3
            Me.ValidacionesGroupBox.TabStop = False
            Me.ValidacionesGroupBox.Text = "Validaciones"
            '
            'ValidacionesFlowLayoutPanel
            '
            Me.ValidacionesFlowLayoutPanel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                                                            Or System.Windows.Forms.AnchorStyles.Left) _
                                                           Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ValidacionesFlowLayoutPanel.AutoScroll = True
            Me.ValidacionesFlowLayoutPanel.Location = New System.Drawing.Point(15, 33)
            Me.ValidacionesFlowLayoutPanel.Name = "ValidacionesFlowLayoutPanel"
            Me.ValidacionesFlowLayoutPanel.Size = New System.Drawing.Size(249, 237)
            Me.ValidacionesFlowLayoutPanel.TabIndex = 4
            '
            'ReimpresionCBarrasButton
            '
            Me.ReimpresionCBarrasButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ReimpresionCBarrasButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnPrinter
            Me.ReimpresionCBarrasButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.ReimpresionCBarrasButton.Location = New System.Drawing.Point(12, 433)
            Me.ReimpresionCBarrasButton.Name = "ReimpresionCBarrasButton"
            Me.ReimpresionCBarrasButton.Size = New System.Drawing.Size(239, 30)
            Me.ReimpresionCBarrasButton.TabIndex = 12
            Me.ReimpresionCBarrasButton.Text = "&Imprimir Codigo de barras Carpeta"
            Me.ReimpresionCBarrasButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.ReimpresionCBarrasButton.UseVisualStyleBackColor = True
            '
            'SplitContainerData
            '
            Me.SplitContainerData.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                                                   Or System.Windows.Forms.AnchorStyles.Left) _
                                                  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.SplitContainerData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.SplitContainerData.Location = New System.Drawing.Point(12, 12)
            Me.SplitContainerData.Name = "SplitContainerData"
            '
            'SplitContainerData.Panel1
            '
            Me.SplitContainerData.Panel1.Controls.Add(Me.GroupBox2)
            Me.SplitContainerData.Panel1.Controls.Add(Me.DatosGroupBox)
            Me.SplitContainerData.Panel1.Padding = New System.Windows.Forms.Padding(10)
            '
            'SplitContainerData.Panel2
            '
            Me.SplitContainerData.Panel2.Controls.Add(Me.GroupBox1)
            Me.SplitContainerData.Panel2.Controls.Add(Me.ValidacionesGroupBox)
            Me.SplitContainerData.Panel2.Padding = New System.Windows.Forms.Padding(10)
            Me.SplitContainerData.Size = New System.Drawing.Size(772, 402)
            Me.SplitContainerData.SplitterDistance = 475
            Me.SplitContainerData.TabIndex = 0
            '
            'GroupBox2
            '
            Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                                         Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GroupBox2.Controls.Add(Me.LlavesFlowLayoutPanel)
            Me.GroupBox2.Location = New System.Drawing.Point(10, 13)
            Me.GroupBox2.Name = "GroupBox2"
            Me.GroupBox2.Size = New System.Drawing.Size(450, 108)
            Me.GroupBox2.TabIndex = 2
            Me.GroupBox2.TabStop = False
            Me.GroupBox2.Text = "Llaves"
            '
            'LlavesFlowLayoutPanel
            '
            Me.LlavesFlowLayoutPanel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                                                      Or System.Windows.Forms.AnchorStyles.Left) _
                                                     Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.LlavesFlowLayoutPanel.AutoScroll = True
            Me.LlavesFlowLayoutPanel.Location = New System.Drawing.Point(15, 25)
            Me.LlavesFlowLayoutPanel.Name = "LlavesFlowLayoutPanel"
            Me.LlavesFlowLayoutPanel.Size = New System.Drawing.Size(428, 77)
            Me.LlavesFlowLayoutPanel.TabIndex = 3
            '
            'GroupBox1
            '
            Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                                         Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GroupBox1.Controls.Add(Me.MontoDesktopTextBox)
            Me.GroupBox1.Controls.Add(Me.FoliosDesktopTextBox)
            Me.GroupBox1.Controls.Add(Me.Label2)
            Me.GroupBox1.Controls.Add(Me.Label1)
            Me.GroupBox1.Location = New System.Drawing.Point(10, 10)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(271, 84)
            Me.GroupBox1.TabIndex = 12
            Me.GroupBox1.TabStop = False
            '
            'MontoDesktopTextBox
            '
            Me.MontoDesktopTextBox.DisabledEnter = False
            Me.MontoDesktopTextBox.DisabledTab = False
            Me.MontoDesktopTextBox.EnabledShortCuts = False
            Me.MontoDesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.MontoDesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.MontoDesktopTextBox.Location = New System.Drawing.Point(71, 50)
            Me.MontoDesktopTextBox.Name = "MontoDesktopTextBox"
            Rango1.MaxValue = CType(9223372036854775807, Long)
            Rango1.MinValue = CType(0, Long)
            Me.MontoDesktopTextBox.Rango = Rango1
            Me.MontoDesktopTextBox.ShortcutsEnabled = False
            Me.MontoDesktopTextBox.Size = New System.Drawing.Size(144, 21)
            Me.MontoDesktopTextBox.TabIndex = 13
            Me.MontoDesktopTextBox.Type = DesktopTextBoxControl.TipoTextBox.Numerico
            '
            'FoliosDesktopTextBox
            '
            Me.FoliosDesktopTextBox.DisabledEnter = False
            Me.FoliosDesktopTextBox.DisabledTab = False
            Me.FoliosDesktopTextBox.EnabledShortCuts = False
            Me.FoliosDesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.FoliosDesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.FoliosDesktopTextBox.Location = New System.Drawing.Point(71, 22)
            Me.FoliosDesktopTextBox.Name = "FoliosDesktopTextBox"
            Rango2.MaxValue = CType(2147483647, Long)
            Rango2.MinValue = CType(0, Long)
            Me.FoliosDesktopTextBox.Rango = Rango2
            Me.FoliosDesktopTextBox.ShortcutsEnabled = False
            Me.FoliosDesktopTextBox.Size = New System.Drawing.Size(144, 21)
            Me.FoliosDesktopTextBox.TabIndex = 12
            Me.FoliosDesktopTextBox.Type = DesktopTextBoxControl.TipoTextBox.Numerico
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Location = New System.Drawing.Point(17, 25)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(39, 13)
            Me.Label2.TabIndex = 10
            Me.Label2.Text = "Folios"
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(17, 53)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(43, 13)
            Me.Label1.TabIndex = 11
            Me.Label1.Text = "Monto"
            '
            'FormCorreccionData
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.AutoScroll = True
            Me.CancelButton = Me.CerrarButton
            Me.ClientSize = New System.Drawing.Size(794, 475)
            Me.Controls.Add(Me.SplitContainerData)
            Me.Controls.Add(Me.ReimpresionCBarrasButton)
            Me.Controls.Add(Me.AceptarButton)
            Me.Controls.Add(Me.CerrarButton)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormCorreccionData"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Correccion Datos y validaciones"
            Me.DatosGroupBox.ResumeLayout(False)
            Me.ValidacionesGroupBox.ResumeLayout(False)
            Me.SplitContainerData.Panel1.ResumeLayout(False)
            Me.SplitContainerData.Panel2.ResumeLayout(False)
            CType(Me.SplitContainerData, System.ComponentModel.ISupportInitialize).EndInit()
            Me.SplitContainerData.ResumeLayout(False)
            Me.GroupBox2.ResumeLayout(False)
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents DataFlowLayoutPanel As System.Windows.Forms.FlowLayoutPanel
        Friend WithEvents AceptarButton As System.Windows.Forms.Button
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents DatosGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents ValidacionesGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents ValidacionesFlowLayoutPanel As System.Windows.Forms.FlowLayoutPanel
        Friend WithEvents ReimpresionCBarrasButton As System.Windows.Forms.Button
        Friend WithEvents SplitContainerData As System.Windows.Forms.SplitContainer
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents MontoDesktopTextBox As DesktopTextBoxControl
        Friend WithEvents FoliosDesktopTextBox As DesktopTextBoxControl
        Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
        Friend WithEvents LlavesFlowLayoutPanel As System.Windows.Forms.FlowLayoutPanel
    End Class
End Namespace