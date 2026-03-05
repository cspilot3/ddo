Imports Miharu.Desktop.Controls.DesktopTextBox
Imports Miharu.Desktop.Controls.DesktopComboBox

Namespace Forms.Reportes.MesaControlFisicos
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormEstadoOT
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormEstadoOT))
            Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.OTDesktopTextBox = New DesktopTextBoxControl()
            Me.ProyectoDesktopComboBox = New DesktopComboBoxControl()
            Me.EsquemaDesktopComboBox = New DesktopComboBoxControl()
            Me.ClienteDesktopComboBox = New DesktopComboBoxControl()
            Me.Label6 = New System.Windows.Forms.Label()
            Me.Label5 = New System.Windows.Forms.Label()
            Me.Label4 = New System.Windows.Forms.Label()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.BuscarButton = New System.Windows.Forms.Button()
            Me.ReportViewer = New Microsoft.Reporting.WinForms.ReportViewer()
            CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SplitContainer1.Panel1.SuspendLayout()
            Me.SplitContainer1.Panel2.SuspendLayout()
            Me.SplitContainer1.SuspendLayout()
            Me.GroupBox1.SuspendLayout()
            Me.SuspendLayout()
            '
            'SplitContainer1
            '
            Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
            Me.SplitContainer1.Name = "SplitContainer1"
            Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
            '
            'SplitContainer1.Panel1
            '
            Me.SplitContainer1.Panel1.Controls.Add(Me.GroupBox1)
            '
            'SplitContainer1.Panel2
            '
            Me.SplitContainer1.Panel2.Controls.Add(Me.ReportViewer)
            Me.SplitContainer1.Size = New System.Drawing.Size(792, 573)
            Me.SplitContainer1.SplitterDistance = 139
            Me.SplitContainer1.TabIndex = 0
            '
            'GroupBox1
            '
            Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                                          Or System.Windows.Forms.AnchorStyles.Left) _
                                         Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GroupBox1.Controls.Add(Me.OTDesktopTextBox)
            Me.GroupBox1.Controls.Add(Me.ProyectoDesktopComboBox)
            Me.GroupBox1.Controls.Add(Me.EsquemaDesktopComboBox)
            Me.GroupBox1.Controls.Add(Me.ClienteDesktopComboBox)
            Me.GroupBox1.Controls.Add(Me.Label6)
            Me.GroupBox1.Controls.Add(Me.Label5)
            Me.GroupBox1.Controls.Add(Me.Label4)
            Me.GroupBox1.Controls.Add(Me.Label1)
            Me.GroupBox1.Controls.Add(Me.BuscarButton)
            Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.GroupBox1.Location = New System.Drawing.Point(27, 12)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(738, 110)
            Me.GroupBox1.TabIndex = 8
            Me.GroupBox1.TabStop = False
            Me.GroupBox1.Text = "Filtros"
            '
            'OTDesktopTextBox
            '
            Me.OTDesktopTextBox.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.OTDesktopTextBox.DisabledEnter = False
            Me.OTDesktopTextBox.DisabledTab = False
            Me.OTDesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.OTDesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.OTDesktopTextBox.Location = New System.Drawing.Point(488, 62)
            Me.OTDesktopTextBox.Name = "OTDesktopTextBox"
            Rango1.MaxValue = 2147483647
            Rango1.MinValue = 0
            Me.OTDesktopTextBox.Rango = Rango1
            Me.OTDesktopTextBox.Size = New System.Drawing.Size(97, 20)
            Me.OTDesktopTextBox.TabIndex = 15
            Me.OTDesktopTextBox.Type = DesktopTextBoxControl.TipoTextBox.Numerico
            '
            'ProyectoDesktopComboBox
            '
            Me.ProyectoDesktopComboBox.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.ProyectoDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.ProyectoDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.ProyectoDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.ProyectoDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.ProyectoDesktopComboBox.FormattingEnabled = True
            Me.ProyectoDesktopComboBox.Location = New System.Drawing.Point(488, 29)
            Me.ProyectoDesktopComboBox.Name = "ProyectoDesktopComboBox"
            Me.ProyectoDesktopComboBox.Size = New System.Drawing.Size(220, 21)
            Me.ProyectoDesktopComboBox.TabIndex = 14
            '
            'EsquemaDesktopComboBox
            '
            Me.EsquemaDesktopComboBox.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.EsquemaDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.EsquemaDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.EsquemaDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.EsquemaDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.EsquemaDesktopComboBox.FormattingEnabled = True
            Me.EsquemaDesktopComboBox.Location = New System.Drawing.Point(136, 58)
            Me.EsquemaDesktopComboBox.Name = "EsquemaDesktopComboBox"
            Me.EsquemaDesktopComboBox.Size = New System.Drawing.Size(223, 21)
            Me.EsquemaDesktopComboBox.TabIndex = 13
            '
            'ClienteDesktopComboBox
            '
            Me.ClienteDesktopComboBox.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.ClienteDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.ClienteDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.ClienteDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.ClienteDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.ClienteDesktopComboBox.FormattingEnabled = True
            Me.ClienteDesktopComboBox.Location = New System.Drawing.Point(136, 29)
            Me.ClienteDesktopComboBox.Name = "ClienteDesktopComboBox"
            Me.ClienteDesktopComboBox.Size = New System.Drawing.Size(223, 21)
            Me.ClienteDesktopComboBox.TabIndex = 12
            '
            'Label6
            '
            Me.Label6.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.Label6.AutoSize = True
            Me.Label6.Location = New System.Drawing.Point(406, 62)
            Me.Label6.Name = "Label6"
            Me.Label6.Size = New System.Drawing.Size(24, 13)
            Me.Label6.TabIndex = 11
            Me.Label6.Text = "OT"
            '
            'Label5
            '
            Me.Label5.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.Label5.AutoSize = True
            Me.Label5.Location = New System.Drawing.Point(406, 32)
            Me.Label5.Name = "Label5"
            Me.Label5.Size = New System.Drawing.Size(57, 13)
            Me.Label5.TabIndex = 10
            Me.Label5.Text = "Proyecto"
            '
            'Label4
            '
            Me.Label4.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.Label4.AutoSize = True
            Me.Label4.Location = New System.Drawing.Point(30, 61)
            Me.Label4.Name = "Label4"
            Me.Label4.Size = New System.Drawing.Size(58, 13)
            Me.Label4.TabIndex = 9
            Me.Label4.Text = "Esquema"
            '
            'Label1
            '
            Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(30, 32)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(46, 13)
            Me.Label1.TabIndex = 8
            Me.Label1.Text = "Cliente"
            '
            'BuscarButton
            '
            Me.BuscarButton.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.BuscarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnBuscar2
            Me.BuscarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.BuscarButton.Location = New System.Drawing.Point(623, 58)
            Me.BuscarButton.Name = "BuscarButton"
            Me.BuscarButton.Size = New System.Drawing.Size(85, 30)
            Me.BuscarButton.TabIndex = 3
            Me.BuscarButton.Text = "&Buscar"
            Me.BuscarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.BuscarButton.UseVisualStyleBackColor = True
            '
            'ReportViewer
            '
            Me.ReportViewer.Dock = System.Windows.Forms.DockStyle.Fill
            Me.ReportViewer.LocalReport.ReportEmbeddedResource = "Miharu.Risk.Library.EstadosOT.rdlc"
            Me.ReportViewer.Location = New System.Drawing.Point(0, 0)
            Me.ReportViewer.Name = "ReportViewer"
            Me.ReportViewer.Size = New System.Drawing.Size(792, 430)
            Me.ReportViewer.TabIndex = 0
            '
            'FormEstadoOT
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.SystemColors.Control
            Me.ClientSize = New System.Drawing.Size(792, 573)
            Me.Controls.Add(Me.SplitContainer1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "FormEstadoOT"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Estados de OT"
            Me.SplitContainer1.Panel1.ResumeLayout(False)
            Me.SplitContainer1.Panel2.ResumeLayout(False)
            CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.SplitContainer1.ResumeLayout(False)
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents OTDesktopTextBox As DesktopTextBoxControl
        Friend WithEvents ProyectoDesktopComboBox As DesktopComboBoxControl
        Friend WithEvents EsquemaDesktopComboBox As DesktopComboBoxControl
        Friend WithEvents ClienteDesktopComboBox As DesktopComboBoxControl
        Friend WithEvents Label6 As System.Windows.Forms.Label
        Friend WithEvents Label5 As System.Windows.Forms.Label
        Friend WithEvents Label4 As System.Windows.Forms.Label
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents BuscarButton As System.Windows.Forms.Button
        Friend WithEvents ReportViewer As Microsoft.Reporting.WinForms.ReportViewer
    End Class
End Namespace