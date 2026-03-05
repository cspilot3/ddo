Imports Miharu.Desktop.Controls.DesktopDataGridView
Imports Miharu.Desktop.Controls.DesktopTextBox

Namespace Forms.Destape
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormSeleccionarPrecinto
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
            Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim Rango1 As Rango = New Rango()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormSeleccionarPrecinto))
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.LineaProcesoLabel = New System.Windows.Forms.Label()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.DestaparButton = New System.Windows.Forms.Button()
            Me.PrecintosDataGridView = New DesktopDataGridViewControl()
            Me.Precinto = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Carpetas = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.CarpetasAdicion = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.BuscarButton = New System.Windows.Forms.Button()
            Me.PrecintoBuscarDesktopTextBox = New DesktopTextBoxControl()
            Me.GroupBox1.SuspendLayout()
            CType(Me.PrecintosDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'GroupBox1
            '
            Me.GroupBox1.Controls.Add(Me.LineaProcesoLabel)
            Me.GroupBox1.Controls.Add(Me.Label1)
            Me.GroupBox1.Controls.Add(Me.CerrarButton)
            Me.GroupBox1.Controls.Add(Me.DestaparButton)
            Me.GroupBox1.Controls.Add(Me.PrecintosDataGridView)
            Me.GroupBox1.Controls.Add(Me.BuscarButton)
            Me.GroupBox1.Controls.Add(Me.PrecintoBuscarDesktopTextBox)
            Me.GroupBox1.Location = New System.Drawing.Point(3, 2)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(338, 300)
            Me.GroupBox1.TabIndex = 2
            Me.GroupBox1.TabStop = False
            Me.GroupBox1.Text = "Precintos"
            '
            'LineaProcesoLabel
            '
            Me.LineaProcesoLabel.AutoSize = True
            Me.LineaProcesoLabel.Font = New System.Drawing.Font("Tahoma", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.LineaProcesoLabel.ForeColor = System.Drawing.Color.Red
            Me.LineaProcesoLabel.Location = New System.Drawing.Point(226, 14)
            Me.LineaProcesoLabel.Name = "LineaProcesoLabel"
            Me.LineaProcesoLabel.Size = New System.Drawing.Size(103, 25)
            Me.LineaProcesoLabel.TabIndex = 14
            Me.LineaProcesoLabel.Text = "0000000"
            Me.LineaProcesoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.Location = New System.Drawing.Point(109, 20)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(114, 14)
            Me.Label1.TabIndex = 13
            Me.Label1.Text = "Línea de Proceso:"
            '
            'CerrarButton
            '
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnSalir
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(235, 264)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(94, 30)
            Me.CerrarButton.TabIndex = 12
            Me.CerrarButton.Text = "Cerrar"
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'DestaparButton
            '
            Me.DestaparButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnDestape
            Me.DestaparButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.DestaparButton.Location = New System.Drawing.Point(135, 264)
            Me.DestaparButton.Name = "DestaparButton"
            Me.DestaparButton.Size = New System.Drawing.Size(94, 30)
            Me.DestaparButton.TabIndex = 11
            Me.DestaparButton.Text = "&Destapar"
            Me.DestaparButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.DestaparButton.UseVisualStyleBackColor = True
            '
            'PrecintosDataGridView
            '
            Me.PrecintosDataGridView.AllowUserToAddRows = False
            Me.PrecintosDataGridView.AllowUserToDeleteRows = False
            Me.PrecintosDataGridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                                                      Or System.Windows.Forms.AnchorStyles.Left) _
                                                     Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.PrecintosDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
            Me.PrecintosDataGridView.BackgroundColor = System.Drawing.Color.White
            DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.PrecintosDataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
            Me.PrecintosDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.PrecintosDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Precinto, Me.Carpetas, Me.CarpetasAdicion})
            DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
            DataGridViewCellStyle2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
            DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightGray
            DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
            DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
            Me.PrecintosDataGridView.DefaultCellStyle = DataGridViewCellStyle2
            Me.PrecintosDataGridView.GridColor = System.Drawing.SystemColors.Control
            Me.PrecintosDataGridView.Location = New System.Drawing.Point(9, 82)
            Me.PrecintosDataGridView.MultiSelect = False
            Me.PrecintosDataGridView.Name = "PrecintosDataGridView"
            Me.PrecintosDataGridView.ReadOnly = True
            DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.PrecintosDataGridView.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
            Me.PrecintosDataGridView.RowHeadersVisible = False
            Me.PrecintosDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.PrecintosDataGridView.Size = New System.Drawing.Size(320, 176)
            Me.PrecintosDataGridView.TabIndex = 10
            '
            'Precinto
            '
            Me.Precinto.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.Precinto.DataPropertyName = "id_precinto"
            Me.Precinto.HeaderText = "Precinto"
            Me.Precinto.Name = "Precinto"
            Me.Precinto.ReadOnly = True
            '
            'Carpetas
            '
            Me.Carpetas.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.Carpetas.DataPropertyName = "carpetas"
            Me.Carpetas.HeaderText = "Carpetas N"
            Me.Carpetas.Name = "Carpetas"
            Me.Carpetas.ReadOnly = True
            '
            'CarpetasAdicion
            '
            Me.CarpetasAdicion.DataPropertyName = "CarpetasAdicion"
            Me.CarpetasAdicion.HeaderText = "Carpetas A"
            Me.CarpetasAdicion.Name = "CarpetasAdicion"
            Me.CarpetasAdicion.ReadOnly = True
            Me.CarpetasAdicion.Width = 94
            '
            'BuscarButton
            '
            Me.BuscarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.BuscarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnBuscar
            Me.BuscarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.BuscarButton.Location = New System.Drawing.Point(249, 45)
            Me.BuscarButton.Name = "BuscarButton"
            Me.BuscarButton.Size = New System.Drawing.Size(80, 24)
            Me.BuscarButton.TabIndex = 7
            Me.BuscarButton.Text = "Buscar"
            Me.BuscarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.BuscarButton.UseVisualStyleBackColor = True
            '
            'PrecintoBuscarDesktopTextBox
            '
            Me.PrecintoBuscarDesktopTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                                                            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.PrecintoBuscarDesktopTextBox.DisabledEnter = True
            Me.PrecintoBuscarDesktopTextBox.DisabledTab = False
            Me.PrecintoBuscarDesktopTextBox.EnabledShortCuts = False
            Me.PrecintoBuscarDesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.PrecintoBuscarDesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.PrecintoBuscarDesktopTextBox.Location = New System.Drawing.Point(9, 45)
            Me.PrecintoBuscarDesktopTextBox.Name = "PrecintoBuscarDesktopTextBox"
            Rango1.MaxValue = CType(2147483647, Long)
            Rango1.MinValue = CType(0, Long)
            Me.PrecintoBuscarDesktopTextBox.Rango = Rango1
            Me.PrecintoBuscarDesktopTextBox.ShortcutsEnabled = False
            Me.PrecintoBuscarDesktopTextBox.Size = New System.Drawing.Size(234, 21)
            Me.PrecintoBuscarDesktopTextBox.TabIndex = 6
            Me.PrecintoBuscarDesktopTextBox.Type = DesktopTextBoxControl.TipoTextBox.Normal
            '
            'FormSeleccionarPrecinto
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.CerrarButton
            Me.ClientSize = New System.Drawing.Size(344, 308)
            Me.Controls.Add(Me.GroupBox1)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormSeleccionarPrecinto"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Destape"
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            CType(Me.PrecintosDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents BuscarButton As System.Windows.Forms.Button
        Friend WithEvents PrecintoBuscarDesktopTextBox As DesktopTextBoxControl
        Friend WithEvents PrecintosDataGridView As DesktopDataGridViewControl
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents DestaparButton As System.Windows.Forms.Button
        Friend WithEvents LineaProcesoLabel As System.Windows.Forms.Label
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents Precinto As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Carpetas As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents CarpetasAdicion As System.Windows.Forms.DataGridViewTextBoxColumn
    End Class
End Namespace