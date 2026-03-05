Imports Miharu.Desktop.Controls.DesktopDataGridView

Namespace Forms.Despacho
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormGuiaDespachoReimpresion
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
            Dim ReportDataSource1 As Microsoft.Reporting.WinForms.ReportDataSource = New Microsoft.Reporting.WinForms.ReportDataSource()
            Dim ReportDataSource2 As Microsoft.Reporting.WinForms.ReportDataSource = New Microsoft.Reporting.WinForms.ReportDataSource()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormGuiaDespachoReimpresion))
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.BuscarButton = New System.Windows.Forms.Button()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.Fecha_FinalDateTimePicker = New System.Windows.Forms.DateTimePicker()
            Me.Fecha_InicialDateTimePicker = New System.Windows.Forms.DateTimePicker()
            Me.ImprimirButton = New System.Windows.Forms.Button()
            Me.OrdenRadioButton = New System.Windows.Forms.RadioButton()
            Me.GrupoRadioButton = New System.Windows.Forms.RadioButton()
            Me.GuiasDesktopDataGridView = New DesktopDataGridViewControl()
            Me.id_Guia_Despacho = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Guia = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Sello = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Fecha = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.GroupBox2 = New System.Windows.Forms.GroupBox()
            Me.CorrecionDatosButton = New System.Windows.Forms.Button()
            Me.ReportViewer = New Microsoft.Reporting.WinForms.ReportViewer()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
            Me.GroupBox1.SuspendLayout()
            CType(Me.GuiasDesktopDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.GroupBox2.SuspendLayout()
            CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SplitContainer1.Panel1.SuspendLayout()
            Me.SplitContainer1.Panel2.SuspendLayout()
            Me.SplitContainer1.SuspendLayout()
            Me.SuspendLayout()
            '
            'GroupBox1
            '
            Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                                         Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GroupBox1.Controls.Add(Me.BuscarButton)
            Me.GroupBox1.Controls.Add(Me.Label2)
            Me.GroupBox1.Controls.Add(Me.Label1)
            Me.GroupBox1.Controls.Add(Me.Fecha_FinalDateTimePicker)
            Me.GroupBox1.Controls.Add(Me.Fecha_InicialDateTimePicker)
            Me.GroupBox1.Location = New System.Drawing.Point(14, 25)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(291, 149)
            Me.GroupBox1.TabIndex = 0
            Me.GroupBox1.TabStop = False
            Me.GroupBox1.Text = "Filtro"
            '
            'BuscarButton
            '
            Me.BuscarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.BuscarButton.Image = Global.Miharu.Custody.Library.My.Resources.Resources.btnBuscar
            Me.BuscarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.BuscarButton.Location = New System.Drawing.Point(186, 99)
            Me.BuscarButton.Name = "BuscarButton"
            Me.BuscarButton.Size = New System.Drawing.Size(92, 30)
            Me.BuscarButton.TabIndex = 20
            Me.BuscarButton.Text = "&Buscar"
            Me.BuscarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.BuscarButton.UseVisualStyleBackColor = True
            '
            'Label2
            '
            Me.Label2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                                       Or System.Windows.Forms.AnchorStyles.Left) _
                                      Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Label2.AutoSize = True
            Me.Label2.Location = New System.Drawing.Point(24, 60)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(69, 13)
            Me.Label2.TabIndex = 2
            Me.Label2.Text = "Fecha Final"
            '
            'Label1
            '
            Me.Label1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                                       Or System.Windows.Forms.AnchorStyles.Left) _
                                      Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(24, 33)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(77, 13)
            Me.Label1.TabIndex = 1
            Me.Label1.Text = "Fecha Inicial"
            '
            'Fecha_FinalDateTimePicker
            '
            Me.Fecha_FinalDateTimePicker.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                                                         Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Fecha_FinalDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
            Me.Fecha_FinalDateTimePicker.Location = New System.Drawing.Point(121, 60)
            Me.Fecha_FinalDateTimePicker.Name = "Fecha_FinalDateTimePicker"
            Me.Fecha_FinalDateTimePicker.Size = New System.Drawing.Size(157, 21)
            Me.Fecha_FinalDateTimePicker.TabIndex = 1
            '
            'Fecha_InicialDateTimePicker
            '
            Me.Fecha_InicialDateTimePicker.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                                                           Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Fecha_InicialDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
            Me.Fecha_InicialDateTimePicker.Location = New System.Drawing.Point(121, 29)
            Me.Fecha_InicialDateTimePicker.Name = "Fecha_InicialDateTimePicker"
            Me.Fecha_InicialDateTimePicker.Size = New System.Drawing.Size(157, 21)
            Me.Fecha_InicialDateTimePicker.TabIndex = 0
            Me.Fecha_InicialDateTimePicker.Value = New Date(2010, 1, 1, 0, 0, 0, 0)
            '
            'ImprimirButton
            '
            Me.ImprimirButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.ImprimirButton.Image = Global.Miharu.Custody.Library.My.Resources.Resources.btnPrinter
            Me.ImprimirButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.ImprimirButton.Location = New System.Drawing.Point(176, 263)
            Me.ImprimirButton.Name = "ImprimirButton"
            Me.ImprimirButton.Size = New System.Drawing.Size(92, 30)
            Me.ImprimirButton.TabIndex = 16
            Me.ImprimirButton.Text = "&Imprimir"
            Me.ImprimirButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.ImprimirButton.UseVisualStyleBackColor = True
            '
            'OrdenRadioButton
            '
            Me.OrdenRadioButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.OrdenRadioButton.AutoSize = True
            Me.OrdenRadioButton.Location = New System.Drawing.Point(12, 232)
            Me.OrdenRadioButton.Name = "OrdenRadioButton"
            Me.OrdenRadioButton.Size = New System.Drawing.Size(188, 17)
            Me.OrdenRadioButton.TabIndex = 17
            Me.OrdenRadioButton.Text = "Imprimir en orden de punteo"
            Me.OrdenRadioButton.UseVisualStyleBackColor = True
            '
            'GrupoRadioButton
            '
            Me.GrupoRadioButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.GrupoRadioButton.AutoSize = True
            Me.GrupoRadioButton.Checked = True
            Me.GrupoRadioButton.Location = New System.Drawing.Point(12, 205)
            Me.GrupoRadioButton.Name = "GrupoRadioButton"
            Me.GrupoRadioButton.Size = New System.Drawing.Size(177, 17)
            Me.GrupoRadioButton.TabIndex = 18
            Me.GrupoRadioButton.TabStop = True
            Me.GrupoRadioButton.Text = "Imprimir con agrupaciones"
            Me.GrupoRadioButton.UseVisualStyleBackColor = True
            '
            'GuiasDesktopDataGridView
            '
            Me.GuiasDesktopDataGridView.AllowUserToAddRows = False
            Me.GuiasDesktopDataGridView.AllowUserToDeleteRows = False
            Me.GuiasDesktopDataGridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                                                         Or System.Windows.Forms.AnchorStyles.Left) _
                                                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GuiasDesktopDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
            Me.GuiasDesktopDataGridView.BackgroundColor = System.Drawing.Color.White
            DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.GuiasDesktopDataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
            Me.GuiasDesktopDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.GuiasDesktopDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.id_Guia_Despacho, Me.Guia, Me.Sello, Me.Fecha})
            DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
            DataGridViewCellStyle2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
            DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightGray
            DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
            DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
            Me.GuiasDesktopDataGridView.DefaultCellStyle = DataGridViewCellStyle2
            Me.GuiasDesktopDataGridView.GridColor = System.Drawing.SystemColors.Control
            Me.GuiasDesktopDataGridView.Location = New System.Drawing.Point(12, 28)
            Me.GuiasDesktopDataGridView.MultiSelect = False
            Me.GuiasDesktopDataGridView.Name = "GuiasDesktopDataGridView"
            Me.GuiasDesktopDataGridView.ReadOnly = True
            DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.GuiasDesktopDataGridView.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
            Me.GuiasDesktopDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.GuiasDesktopDataGridView.Size = New System.Drawing.Size(266, 159)
            Me.GuiasDesktopDataGridView.TabIndex = 19
            '
            'id_Guia_Despacho
            '
            Me.id_Guia_Despacho.DataPropertyName = "id_Guia_Despacho"
            Me.id_Guia_Despacho.HeaderText = "id_Guia_Despacho"
            Me.id_Guia_Despacho.Name = "id_Guia_Despacho"
            Me.id_Guia_Despacho.ReadOnly = True
            Me.id_Guia_Despacho.Visible = False
            Me.id_Guia_Despacho.Width = 136
            '
            'Guia
            '
            Me.Guia.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
            Me.Guia.DataPropertyName = "Guia"
            Me.Guia.HeaderText = "Guia"
            Me.Guia.Name = "Guia"
            Me.Guia.ReadOnly = True
            Me.Guia.Width = 57
            '
            'Sello
            '
            Me.Sello.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
            Me.Sello.DataPropertyName = "Sello"
            Me.Sello.HeaderText = "Sello"
            Me.Sello.Name = "Sello"
            Me.Sello.ReadOnly = True
            Me.Sello.Width = 59
            '
            'Fecha
            '
            Me.Fecha.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.Fecha.DataPropertyName = "Fecha_Log"
            Me.Fecha.HeaderText = "Fecha"
            Me.Fecha.Name = "Fecha"
            Me.Fecha.ReadOnly = True
            '
            'GroupBox2
            '
            Me.GroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                                          Or System.Windows.Forms.AnchorStyles.Left) _
                                         Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GroupBox2.Controls.Add(Me.CorrecionDatosButton)
            Me.GroupBox2.Controls.Add(Me.GuiasDesktopDataGridView)
            Me.GroupBox2.Controls.Add(Me.ImprimirButton)
            Me.GroupBox2.Controls.Add(Me.GrupoRadioButton)
            Me.GroupBox2.Controls.Add(Me.OrdenRadioButton)
            Me.GroupBox2.Location = New System.Drawing.Point(14, 196)
            Me.GroupBox2.Name = "GroupBox2"
            Me.GroupBox2.Size = New System.Drawing.Size(291, 308)
            Me.GroupBox2.TabIndex = 20
            Me.GroupBox2.TabStop = False
            Me.GroupBox2.Text = "Resultados de la busqueda"
            '
            'CorrecionDatosButton
            '
            Me.CorrecionDatosButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.CorrecionDatosButton.Image = Global.Miharu.Custody.Library.My.Resources.Resources.btnSalir
            Me.CorrecionDatosButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CorrecionDatosButton.Location = New System.Drawing.Point(12, 263)
            Me.CorrecionDatosButton.Name = "CorrecionDatosButton"
            Me.CorrecionDatosButton.Size = New System.Drawing.Size(158, 30)
            Me.CorrecionDatosButton.TabIndex = 20
            Me.CorrecionDatosButton.Text = "&Corregir Guia o Sello"
            Me.CorrecionDatosButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CorrecionDatosButton.UseVisualStyleBackColor = True
            '
            'ReportViewer
            '
            Me.ReportViewer.Dock = System.Windows.Forms.DockStyle.Fill
            ReportDataSource1.Name = "GuiaDespachoEncabezado"
            ReportDataSource1.Value = Nothing
            ReportDataSource2.Name = "GuiaDespacho"
            ReportDataSource2.Value = Nothing
            Me.ReportViewer.LocalReport.DataSources.Add(ReportDataSource1)
            Me.ReportViewer.LocalReport.DataSources.Add(ReportDataSource2)
            Me.ReportViewer.LocalReport.ReportEmbeddedResource = "Miharu.Custody.Library.GuiaDespacho.rdlc"
            Me.ReportViewer.Location = New System.Drawing.Point(5, 5)
            Me.ReportViewer.Name = "ReportViewer"
            Me.ReportViewer.Size = New System.Drawing.Size(428, 499)
            Me.ReportViewer.TabIndex = 21
            '
            'CerrarButton
            '
            Me.CerrarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CerrarButton.Image = Global.Miharu.Custody.Library.My.Resources.Resources.btnSalir1
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(692, 536)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(88, 30)
            Me.CerrarButton.TabIndex = 21
            Me.CerrarButton.Text = "&Cerrar"
            Me.CerrarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'SplitContainer1
            '
            Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                                                Or System.Windows.Forms.AnchorStyles.Left) _
                                               Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.SplitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
            Me.SplitContainer1.Location = New System.Drawing.Point(12, 12)
            Me.SplitContainer1.Name = "SplitContainer1"
            '
            'SplitContainer1.Panel1
            '
            Me.SplitContainer1.Panel1.Controls.Add(Me.GroupBox2)
            Me.SplitContainer1.Panel1.Controls.Add(Me.GroupBox1)
            '
            'SplitContainer1.Panel2
            '
            Me.SplitContainer1.Panel2.Controls.Add(Me.ReportViewer)
            Me.SplitContainer1.Panel2.Padding = New System.Windows.Forms.Padding(5)
            Me.SplitContainer1.Size = New System.Drawing.Size(768, 513)
            Me.SplitContainer1.SplitterDistance = 322
            Me.SplitContainer1.TabIndex = 22
            '
            'FormGuiaDespachoReimpresion
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(792, 573)
            Me.Controls.Add(Me.SplitContainer1)
            Me.Controls.Add(Me.CerrarButton)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "FormGuiaDespachoReimpresion"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Reimpresion de guias de despacho"
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            CType(Me.GuiasDesktopDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.GroupBox2.ResumeLayout(False)
            Me.GroupBox2.PerformLayout()
            Me.SplitContainer1.Panel1.ResumeLayout(False)
            Me.SplitContainer1.Panel2.ResumeLayout(False)
            CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.SplitContainer1.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents Fecha_FinalDateTimePicker As System.Windows.Forms.DateTimePicker
        Friend WithEvents Fecha_InicialDateTimePicker As System.Windows.Forms.DateTimePicker
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents ImprimirButton As System.Windows.Forms.Button
        Friend WithEvents BuscarButton As System.Windows.Forms.Button
        Friend WithEvents OrdenRadioButton As System.Windows.Forms.RadioButton
        Friend WithEvents GrupoRadioButton As System.Windows.Forms.RadioButton
        Friend WithEvents GuiasDesktopDataGridView As DesktopDataGridViewControl
        Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
        Friend WithEvents ReportViewer As Microsoft.Reporting.WinForms.ReportViewer
        Friend WithEvents CorrecionDatosButton As System.Windows.Forms.Button
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
        Friend WithEvents id_Guia_Despacho As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Guia As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Sello As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Fecha As System.Windows.Forms.DataGridViewTextBoxColumn
    End Class
End Namespace