<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormConsultarEsquemaFacturacion
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormConsultarEsquemaFacturacion))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.EntidadDesktopComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.EsquemaDesktopComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
        Me.NuevoButton = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.BuscarButton = New System.Windows.Forms.Button()
        Me.CerrarButton = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(17, 29)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(133, 13)
        Me.Label1.TabIndex = 36
        Me.Label1.Text = "Entidad de facturación"
        '
        'EntidadDesktopComboBox
        '
        Me.EntidadDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.EntidadDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.EntidadDesktopComboBox.DisabledEnter = False
        Me.EntidadDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.EntidadDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.EntidadDesktopComboBox.FormattingEnabled = True
        Me.EntidadDesktopComboBox.Location = New System.Drawing.Point(160, 26)
        Me.EntidadDesktopComboBox.Name = "EntidadDesktopComboBox"
        Me.EntidadDesktopComboBox.Size = New System.Drawing.Size(310, 21)
        Me.EntidadDesktopComboBox.TabIndex = 35
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Green
        Me.Label2.Location = New System.Drawing.Point(12, 19)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(56, 16)
        Me.Label2.TabIndex = 37
        Me.Label2.Text = "Entidad"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(17, 61)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(125, 13)
        Me.Label3.TabIndex = 38
        Me.Label3.Text = "Esquema facturación"
        '
        'EsquemaDesktopComboBox
        '
        Me.EsquemaDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.EsquemaDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.EsquemaDesktopComboBox.DisabledEnter = False
        Me.EsquemaDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.EsquemaDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.EsquemaDesktopComboBox.FormattingEnabled = True
        Me.EsquemaDesktopComboBox.Location = New System.Drawing.Point(160, 58)
        Me.EsquemaDesktopComboBox.Name = "EsquemaDesktopComboBox"
        Me.EsquemaDesktopComboBox.Size = New System.Drawing.Size(310, 21)
        Me.EsquemaDesktopComboBox.TabIndex = 39
        '
        'NuevoButton
        '
        Me.NuevoButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NuevoButton.Image = Global.Miharu.Desktop.My.Resources.Resources.btnAgregar
        Me.NuevoButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.NuevoButton.Location = New System.Drawing.Point(189, 97)
        Me.NuevoButton.Name = "NuevoButton"
        Me.NuevoButton.Size = New System.Drawing.Size(89, 28)
        Me.NuevoButton.TabIndex = 45
        Me.NuevoButton.Text = "&Nuevo"
        Me.NuevoButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.NuevoButton.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.CerrarButton)
        Me.GroupBox1.Controls.Add(Me.EntidadDesktopComboBox)
        Me.GroupBox1.Controls.Add(Me.NuevoButton)
        Me.GroupBox1.Controls.Add(Me.BuscarButton)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.EsquemaDesktopComboBox)
        Me.GroupBox1.Location = New System.Drawing.Point(15, 38)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(489, 144)
        Me.GroupBox1.TabIndex = 47
        Me.GroupBox1.TabStop = False
        '
        'BuscarButton
        '
        Me.BuscarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BuscarButton.Image = Global.Miharu.Desktop.My.Resources.Resources.btnBuscar
        Me.BuscarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BuscarButton.Location = New System.Drawing.Point(284, 97)
        Me.BuscarButton.Name = "BuscarButton"
        Me.BuscarButton.Size = New System.Drawing.Size(92, 28)
        Me.BuscarButton.TabIndex = 44
        Me.BuscarButton.Text = "&Buscar"
        Me.BuscarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BuscarButton.UseVisualStyleBackColor = True
        '
        'CerrarButton
        '
        Me.CerrarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.CerrarButton.Image = Global.Miharu.Desktop.My.Resources.Resources.btnSalir
        Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CerrarButton.Location = New System.Drawing.Point(382, 97)
        Me.CerrarButton.Name = "CerrarButton"
        Me.CerrarButton.Size = New System.Drawing.Size(89, 28)
        Me.CerrarButton.TabIndex = 46
        Me.CerrarButton.Text = "&Cerrar"
        Me.CerrarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CerrarButton.UseVisualStyleBackColor = True
        '
        'FormConsultarEsquemaFacturacion
        '
        Me.AcceptButton = Me.BuscarButton
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.CerrarButton
        Me.ClientSize = New System.Drawing.Size(517, 196)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormConsultarEsquemaFacturacion"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Consulta de Esquemas de facturación"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents EntidadDesktopComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents EsquemaDesktopComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
    Friend WithEvents BuscarButton As System.Windows.Forms.Button
    Friend WithEvents NuevoButton As System.Windows.Forms.Button
    Friend WithEvents CerrarButton As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
End Class
