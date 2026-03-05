Imports Miharu.Desktop.Controls.DesktopDataGridView
Imports Miharu.Desktop.Controls.DesktopComboBox

Namespace Reportes.VisorReportes.Genericos
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class Report_Estados_Operacion
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
            Dim DataGridViewCellStyle17 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle18 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle19 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle20 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Me.BaseGroupBox = New System.Windows.Forms.GroupBox()
            Me.TipoDesktopComboBox = New DesktopComboBoxControl()
            Me.CancelarButton = New System.Windows.Forms.Button()
            Me.Label4 = New System.Windows.Forms.Label()
            Me.AceptarButton = New System.Windows.Forms.Button()
            Me.EsquemaDesktopComboBox = New DesktopComboBoxControl()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.EntidadDesktopComboBox = New DesktopComboBoxControl()
            Me.ProyectoDesktopComboBox = New DesktopComboBoxControl()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.ResultadosDataGridView = New DesktopDataGridViewControl()
            Me.chkEncabezado = New System.Windows.Forms.CheckBox()
            Me.VacioRadioButton = New System.Windows.Forms.RadioButton()
            Me.PuntoComaRadioButton = New System.Windows.Forms.RadioButton()
            Me.TabuladorRadioButton = New System.Windows.Forms.RadioButton()
            Me.ComaRadioButton = New System.Windows.Forms.RadioButton()
            Me.ExportarButton = New System.Windows.Forms.Button()
            Me.SaveFileDialog = New System.Windows.Forms.SaveFileDialog()
            Me.CargandoPictureBox = New System.Windows.Forms.PictureBox()
            Me.BaseGroupBox.SuspendLayout()
            Me.GroupBox1.SuspendLayout()
            CType(Me.ResultadosDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.CargandoPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'BaseGroupBox
            '
            Me.BaseGroupBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                                            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.BaseGroupBox.Controls.Add(Me.VacioRadioButton)
            Me.BaseGroupBox.Controls.Add(Me.ExportarButton)
            Me.BaseGroupBox.Controls.Add(Me.PuntoComaRadioButton)
            Me.BaseGroupBox.Controls.Add(Me.chkEncabezado)
            Me.BaseGroupBox.Controls.Add(Me.TabuladorRadioButton)
            Me.BaseGroupBox.Controls.Add(Me.TipoDesktopComboBox)
            Me.BaseGroupBox.Controls.Add(Me.ComaRadioButton)
            Me.BaseGroupBox.Controls.Add(Me.CancelarButton)
            Me.BaseGroupBox.Controls.Add(Me.Label4)
            Me.BaseGroupBox.Controls.Add(Me.AceptarButton)
            Me.BaseGroupBox.Controls.Add(Me.EsquemaDesktopComboBox)
            Me.BaseGroupBox.Controls.Add(Me.Label3)
            Me.BaseGroupBox.Controls.Add(Me.Label2)
            Me.BaseGroupBox.Controls.Add(Me.EntidadDesktopComboBox)
            Me.BaseGroupBox.Controls.Add(Me.ProyectoDesktopComboBox)
            Me.BaseGroupBox.Controls.Add(Me.Label1)
            Me.BaseGroupBox.Location = New System.Drawing.Point(5, 12)
            Me.BaseGroupBox.Name = "BaseGroupBox"
            Me.BaseGroupBox.Size = New System.Drawing.Size(806, 126)
            Me.BaseGroupBox.TabIndex = 23
            Me.BaseGroupBox.TabStop = False
            Me.BaseGroupBox.Text = "Filtros"
            '
            'TipoDesktopComboBox
            '
            Me.TipoDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.TipoDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.TipoDesktopComboBox.DisabledEnter = False
            Me.TipoDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.TipoDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.TipoDesktopComboBox.FormattingEnabled = True
            Me.TipoDesktopComboBox.Items.AddRange(New Object() {"Operación", "Operación Detalle"})
            Me.TipoDesktopComboBox.Location = New System.Drawing.Point(430, 47)
            Me.TipoDesktopComboBox.Name = "TipoDesktopComboBox"
            Me.TipoDesktopComboBox.Size = New System.Drawing.Size(246, 21)
            Me.TipoDesktopComboBox.TabIndex = 7
            '
            'CancelarButton
            '
            Me.CancelarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CancelarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CancelarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CancelarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.Cancelar
            Me.CancelarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CancelarButton.Location = New System.Drawing.Point(712, 50)
            Me.CancelarButton.Name = "CancelarButton"
            Me.CancelarButton.Size = New System.Drawing.Size(80, 29)
            Me.CancelarButton.TabIndex = 22
            Me.CancelarButton.Text = "&Cerrar"
            Me.CancelarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CancelarButton.UseVisualStyleBackColor = True
            '
            'Label4
            '
            Me.Label4.AutoSize = True
            Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label4.Location = New System.Drawing.Point(352, 50)
            Me.Label4.Name = "Label4"
            Me.Label4.Size = New System.Drawing.Size(32, 13)
            Me.Label4.TabIndex = 6
            Me.Label4.Text = "Tipo"
            '
            'AceptarButton
            '
            Me.AceptarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.AceptarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.AceptarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.Aceptar
            Me.AceptarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.AceptarButton.Location = New System.Drawing.Point(712, 13)
            Me.AceptarButton.Name = "AceptarButton"
            Me.AceptarButton.Size = New System.Drawing.Size(80, 29)
            Me.AceptarButton.TabIndex = 21
            Me.AceptarButton.Text = "&Aceptar"
            Me.AceptarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.AceptarButton.UseVisualStyleBackColor = True
            '
            'EsquemaDesktopComboBox
            '
            Me.EsquemaDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.EsquemaDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.EsquemaDesktopComboBox.DisabledEnter = False
            Me.EsquemaDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.EsquemaDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.EsquemaDesktopComboBox.FormattingEnabled = True
            Me.EsquemaDesktopComboBox.Location = New System.Drawing.Point(87, 47)
            Me.EsquemaDesktopComboBox.Name = "EsquemaDesktopComboBox"
            Me.EsquemaDesktopComboBox.Size = New System.Drawing.Size(246, 21)
            Me.EsquemaDesktopComboBox.TabIndex = 5
            '
            'Label3
            '
            Me.Label3.AutoSize = True
            Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label3.Location = New System.Drawing.Point(10, 50)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(58, 13)
            Me.Label3.TabIndex = 4
            Me.Label3.Text = "Esquema"
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label2.Location = New System.Drawing.Point(10, 21)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(50, 13)
            Me.Label2.TabIndex = 3
            Me.Label2.Text = "Entidad"
            '
            'EntidadDesktopComboBox
            '
            Me.EntidadDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.EntidadDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.EntidadDesktopComboBox.DisabledEnter = False
            Me.EntidadDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.EntidadDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.EntidadDesktopComboBox.FormattingEnabled = True
            Me.EntidadDesktopComboBox.Location = New System.Drawing.Point(87, 18)
            Me.EntidadDesktopComboBox.Name = "EntidadDesktopComboBox"
            Me.EntidadDesktopComboBox.Size = New System.Drawing.Size(246, 21)
            Me.EntidadDesktopComboBox.TabIndex = 2
            '
            'ProyectoDesktopComboBox
            '
            Me.ProyectoDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.ProyectoDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.ProyectoDesktopComboBox.DisabledEnter = False
            Me.ProyectoDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.ProyectoDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.ProyectoDesktopComboBox.FormattingEnabled = True
            Me.ProyectoDesktopComboBox.Location = New System.Drawing.Point(430, 18)
            Me.ProyectoDesktopComboBox.Name = "ProyectoDesktopComboBox"
            Me.ProyectoDesktopComboBox.Size = New System.Drawing.Size(246, 21)
            Me.ProyectoDesktopComboBox.TabIndex = 1
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.Location = New System.Drawing.Point(352, 21)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(57, 13)
            Me.Label1.TabIndex = 0
            Me.Label1.Text = "Proyecto"
            '
            'GroupBox1
            '
            Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                                          Or System.Windows.Forms.AnchorStyles.Left) _
                                         Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GroupBox1.Controls.Add(Me.CargandoPictureBox)
            Me.GroupBox1.Controls.Add(Me.ResultadosDataGridView)
            Me.GroupBox1.Location = New System.Drawing.Point(5, 159)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(806, 391)
            Me.GroupBox1.TabIndex = 25
            Me.GroupBox1.TabStop = False
            Me.GroupBox1.Text = "Datos"
            '
            'ResultadosDataGridView
            '
            Me.ResultadosDataGridView.AllowUserToAddRows = False
            Me.ResultadosDataGridView.AllowUserToDeleteRows = False
            Me.ResultadosDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
            Me.ResultadosDataGridView.BackgroundColor = System.Drawing.Color.White
            DataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle17.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle17.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle17.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.ResultadosDataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle17
            Me.ResultadosDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            DataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle18.BackColor = System.Drawing.SystemColors.Window
            DataGridViewCellStyle18.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle18.ForeColor = System.Drawing.SystemColors.ControlText
            DataGridViewCellStyle18.SelectionBackColor = System.Drawing.Color.LightGray
            DataGridViewCellStyle18.SelectionForeColor = System.Drawing.Color.Black
            DataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
            Me.ResultadosDataGridView.DefaultCellStyle = DataGridViewCellStyle18
            Me.ResultadosDataGridView.Dock = System.Windows.Forms.DockStyle.Fill
            Me.ResultadosDataGridView.GridColor = System.Drawing.SystemColors.Control
            Me.ResultadosDataGridView.Location = New System.Drawing.Point(3, 16)
            Me.ResultadosDataGridView.MultiSelect = False
            Me.ResultadosDataGridView.Name = "ResultadosDataGridView"
            Me.ResultadosDataGridView.ReadOnly = True
            DataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
            DataGridViewCellStyle19.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle19.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle19.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle19.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle19.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle19.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.ResultadosDataGridView.RowHeadersDefaultCellStyle = DataGridViewCellStyle19
            DataGridViewCellStyle20.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle20.ForeColor = System.Drawing.Color.Black
            Me.ResultadosDataGridView.RowsDefaultCellStyle = DataGridViewCellStyle20
            Me.ResultadosDataGridView.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ResultadosDataGridView.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black
            Me.ResultadosDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.ResultadosDataGridView.Size = New System.Drawing.Size(800, 372)
            Me.ResultadosDataGridView.TabIndex = 1
            '
            'chkEncabezado
            '
            Me.chkEncabezado.AutoSize = True
            Me.chkEncabezado.Location = New System.Drawing.Point(491, 93)
            Me.chkEncabezado.Name = "chkEncabezado"
            Me.chkEncabezado.Size = New System.Drawing.Size(123, 17)
            Me.chkEncabezado.TabIndex = 15
            Me.chkEncabezado.Text = "Maneja encabezado"
            Me.chkEncabezado.UseVisualStyleBackColor = True
            '
            'VacioRadioButton
            '
            Me.VacioRadioButton.AutoSize = True
            Me.VacioRadioButton.Location = New System.Drawing.Point(309, 92)
            Me.VacioRadioButton.Name = "VacioRadioButton"
            Me.VacioRadioButton.Size = New System.Drawing.Size(63, 17)
            Me.VacioRadioButton.TabIndex = 3
            Me.VacioRadioButton.Text = "Vacío ()"
            Me.VacioRadioButton.UseVisualStyleBackColor = True
            '
            'PuntoComaRadioButton
            '
            Me.PuntoComaRadioButton.AutoSize = True
            Me.PuntoComaRadioButton.Location = New System.Drawing.Point(190, 92)
            Me.PuntoComaRadioButton.Name = "PuntoComaRadioButton"
            Me.PuntoComaRadioButton.Size = New System.Drawing.Size(103, 17)
            Me.PuntoComaRadioButton.TabIndex = 2
            Me.PuntoComaRadioButton.Text = "Punto y Coma (;)"
            Me.PuntoComaRadioButton.UseVisualStyleBackColor = True
            '
            'TabuladorRadioButton
            '
            Me.TabuladorRadioButton.AutoSize = True
            Me.TabuladorRadioButton.Location = New System.Drawing.Point(87, 92)
            Me.TabuladorRadioButton.Name = "TabuladorRadioButton"
            Me.TabuladorRadioButton.Size = New System.Drawing.Size(97, 17)
            Me.TabuladorRadioButton.TabIndex = 1
            Me.TabuladorRadioButton.Text = "Tabulador (     )"
            Me.TabuladorRadioButton.UseVisualStyleBackColor = True
            '
            'ComaRadioButton
            '
            Me.ComaRadioButton.AutoSize = True
            Me.ComaRadioButton.Checked = True
            Me.ComaRadioButton.Location = New System.Drawing.Point(13, 92)
            Me.ComaRadioButton.Name = "ComaRadioButton"
            Me.ComaRadioButton.Size = New System.Drawing.Size(64, 17)
            Me.ComaRadioButton.TabIndex = 0
            Me.ComaRadioButton.TabStop = True
            Me.ComaRadioButton.Text = "Coma (,)"
            Me.ComaRadioButton.UseVisualStyleBackColor = True
            '
            'ExportarButton
            '
            Me.ExportarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ExportarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnGuardar
            Me.ExportarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.ExportarButton.Location = New System.Drawing.Point(712, 86)
            Me.ExportarButton.Name = "ExportarButton"
            Me.ExportarButton.Size = New System.Drawing.Size(80, 28)
            Me.ExportarButton.TabIndex = 27
            Me.ExportarButton.Text = "&Exportar"
            Me.ExportarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.ExportarButton.UseVisualStyleBackColor = True
            '
            'CargandoPictureBox
            '
            Me.CargandoPictureBox.Image = Global.Miharu.Risk.Library.My.Resources.Resources.ajax_loader
            Me.CargandoPictureBox.Location = New System.Drawing.Point(355, 143)
            Me.CargandoPictureBox.Name = "CargandoPictureBox"
            Me.CargandoPictureBox.Size = New System.Drawing.Size(109, 102)
            Me.CargandoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
            Me.CargandoPictureBox.TabIndex = 26
            Me.CargandoPictureBox.TabStop = False
            Me.CargandoPictureBox.Visible = False
            '
            'Report_Estados_Operacion
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.CancelarButton
            Me.ClientSize = New System.Drawing.Size(823, 562)
            Me.Controls.Add(Me.GroupBox1)
            Me.Controls.Add(Me.BaseGroupBox)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.MinimizeBox = False
            Me.Name = "Report_Estados_Operacion"
            Me.ShowIcon = False
            Me.ShowInTaskbar = False
            Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Estados Operacion - Detalle"
            Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
            Me.BaseGroupBox.ResumeLayout(False)
            Me.BaseGroupBox.PerformLayout()
            Me.GroupBox1.ResumeLayout(False)
            CType(Me.ResultadosDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.CargandoPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents BaseGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents CancelarButton As System.Windows.Forms.Button
        Friend WithEvents AceptarButton As System.Windows.Forms.Button
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents EntidadDesktopComboBox As DesktopComboBoxControl
        Friend WithEvents ProyectoDesktopComboBox As DesktopComboBoxControl
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents EsquemaDesktopComboBox As DesktopComboBoxControl
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents TipoDesktopComboBox As DesktopComboBoxControl
        Friend WithEvents Label4 As System.Windows.Forms.Label
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents ResultadosDataGridView As DesktopDataGridViewControl
        Friend WithEvents chkEncabezado As System.Windows.Forms.CheckBox
        Friend WithEvents VacioRadioButton As System.Windows.Forms.RadioButton
        Friend WithEvents PuntoComaRadioButton As System.Windows.Forms.RadioButton
        Friend WithEvents TabuladorRadioButton As System.Windows.Forms.RadioButton
        Friend WithEvents ComaRadioButton As System.Windows.Forms.RadioButton
        Friend WithEvents ExportarButton As System.Windows.Forms.Button
        Friend WithEvents SaveFileDialog As System.Windows.Forms.SaveFileDialog
        Friend WithEvents CargandoPictureBox As System.Windows.Forms.PictureBox

    End Class
End Namespace