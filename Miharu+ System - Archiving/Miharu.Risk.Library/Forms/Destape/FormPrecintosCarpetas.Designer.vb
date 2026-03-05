Imports Miharu.Desktop.Controls.DesktopDataGridView

Namespace Forms.Destape
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormPrecintosCarpetas
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
            Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormPrecintosCarpetas))
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.SplitContainer = New System.Windows.Forms.SplitContainer()
            Me.CarpetasDesktopDataGridView = New DesktopDataGridViewControl()
            Me.Label4 = New System.Windows.Forms.Label()
            Me.ImprimirButton = New System.Windows.Forms.Button()
            Me.DocumentosDesktopDataGridView = New DesktopDataGridViewControl()
            Me.CerrarPrecintoButton = New System.Windows.Forms.Button()
            Me.Label5 = New System.Windows.Forms.Label()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.DestaparButton = New System.Windows.Forms.Button()
            Me.SeleccionarCajaButton = New System.Windows.Forms.Button()
            Me.CajaProcesoLabel = New System.Windows.Forms.Label()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.PrecintoLabel = New System.Windows.Forms.Label()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.FolderCBarras = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.DocEstado = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fk_precinto = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fk_estado = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fk_expediente = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.id_folder = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.fk_ot = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.CBarras = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Tipo = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Estado = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.CProceso = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.GroupBox1.SuspendLayout()
            CType(Me.SplitContainer, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SplitContainer.Panel1.SuspendLayout()
            Me.SplitContainer.Panel2.SuspendLayout()
            Me.SplitContainer.SuspendLayout()
            CType(Me.CarpetasDesktopDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.DocumentosDesktopDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'GroupBox1
            '
            Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                                          Or System.Windows.Forms.AnchorStyles.Left) _
                                         Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GroupBox1.Controls.Add(Me.SplitContainer)
            Me.GroupBox1.Controls.Add(Me.SeleccionarCajaButton)
            Me.GroupBox1.Controls.Add(Me.CajaProcesoLabel)
            Me.GroupBox1.Controls.Add(Me.Label3)
            Me.GroupBox1.Controls.Add(Me.PrecintoLabel)
            Me.GroupBox1.Controls.Add(Me.Label1)
            Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(765, 552)
            Me.GroupBox1.TabIndex = 3
            Me.GroupBox1.TabStop = False
            '
            'SplitContainer
            '
            Me.SplitContainer.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                                               Or System.Windows.Forms.AnchorStyles.Left) _
                                              Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.SplitContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
            Me.SplitContainer.Location = New System.Drawing.Point(6, 50)
            Me.SplitContainer.Name = "SplitContainer"
            Me.SplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal
            '
            'SplitContainer.Panel1
            '
            Me.SplitContainer.Panel1.Controls.Add(Me.CarpetasDesktopDataGridView)
            Me.SplitContainer.Panel1.Controls.Add(Me.Label4)
            Me.SplitContainer.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.SplitContainer.Panel1MinSize = 200
            '
            'SplitContainer.Panel2
            '
            Me.SplitContainer.Panel2.Controls.Add(Me.ImprimirButton)
            Me.SplitContainer.Panel2.Controls.Add(Me.DocumentosDesktopDataGridView)
            Me.SplitContainer.Panel2.Controls.Add(Me.CerrarPrecintoButton)
            Me.SplitContainer.Panel2.Controls.Add(Me.Label5)
            Me.SplitContainer.Panel2.Controls.Add(Me.CerrarButton)
            Me.SplitContainer.Panel2.Controls.Add(Me.DestaparButton)
            Me.SplitContainer.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.SplitContainer.Panel2MinSize = 205
            Me.SplitContainer.Size = New System.Drawing.Size(753, 496)
            Me.SplitContainer.SplitterDistance = 241
            Me.SplitContainer.TabIndex = 19
            '
            'CarpetasDesktopDataGridView
            '
            Me.CarpetasDesktopDataGridView.AllowUserToAddRows = False
            Me.CarpetasDesktopDataGridView.AllowUserToDeleteRows = False
            Me.CarpetasDesktopDataGridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                                                            Or System.Windows.Forms.AnchorStyles.Left) _
                                                           Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CarpetasDesktopDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
            Me.CarpetasDesktopDataGridView.BackgroundColor = System.Drawing.Color.White
            DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.CarpetasDesktopDataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
            Me.CarpetasDesktopDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.CarpetasDesktopDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.FolderCBarras, Me.DocEstado, Me.fk_precinto, Me.fk_estado, Me.fk_expediente, Me.id_folder, Me.fk_ot})
            DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
            DataGridViewCellStyle3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
            DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.LightGray
            DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black
            DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
            Me.CarpetasDesktopDataGridView.DefaultCellStyle = DataGridViewCellStyle3
            Me.CarpetasDesktopDataGridView.GridColor = System.Drawing.SystemColors.Control
            Me.CarpetasDesktopDataGridView.Location = New System.Drawing.Point(16, 30)
            Me.CarpetasDesktopDataGridView.MultiSelect = False
            Me.CarpetasDesktopDataGridView.Name = "CarpetasDesktopDataGridView"
            Me.CarpetasDesktopDataGridView.ReadOnly = True
            DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.CarpetasDesktopDataGridView.RowHeadersDefaultCellStyle = DataGridViewCellStyle4
            Me.CarpetasDesktopDataGridView.RowHeadersVisible = False
            Me.CarpetasDesktopDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.CarpetasDesktopDataGridView.Size = New System.Drawing.Size(715, 189)
            Me.CarpetasDesktopDataGridView.TabIndex = 10
            '
            'Label4
            '
            Me.Label4.AutoSize = True
            Me.Label4.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label4.Location = New System.Drawing.Point(12, 8)
            Me.Label4.Name = "Label4"
            Me.Label4.Size = New System.Drawing.Size(82, 19)
            Me.Label4.TabIndex = 15
            Me.Label4.Text = "Carpetas"
            '
            'ImprimirButton
            '
            Me.ImprimirButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ImprimirButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnPrinter
            Me.ImprimirButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.ImprimirButton.Location = New System.Drawing.Point(599, 105)
            Me.ImprimirButton.Name = "ImprimirButton"
            Me.ImprimirButton.Size = New System.Drawing.Size(132, 30)
            Me.ImprimirButton.TabIndex = 19
            Me.ImprimirButton.Text = "&Imprimir"
            Me.ImprimirButton.UseVisualStyleBackColor = True
            '
            'DocumentosDesktopDataGridView
            '
            Me.DocumentosDesktopDataGridView.AllowUserToAddRows = False
            Me.DocumentosDesktopDataGridView.AllowUserToDeleteRows = False
            Me.DocumentosDesktopDataGridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                                                              Or System.Windows.Forms.AnchorStyles.Left) _
                                                             Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.DocumentosDesktopDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
            Me.DocumentosDesktopDataGridView.BackgroundColor = System.Drawing.Color.White
            DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.DocumentosDesktopDataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle5
            Me.DocumentosDesktopDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.DocumentosDesktopDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.CBarras, Me.Tipo, Me.Estado, Me.CProceso})
            DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window
            DataGridViewCellStyle6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText
            DataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.LightGray
            DataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black
            DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
            Me.DocumentosDesktopDataGridView.DefaultCellStyle = DataGridViewCellStyle6
            Me.DocumentosDesktopDataGridView.GridColor = System.Drawing.SystemColors.Control
            Me.DocumentosDesktopDataGridView.Location = New System.Drawing.Point(16, 46)
            Me.DocumentosDesktopDataGridView.MultiSelect = False
            Me.DocumentosDesktopDataGridView.Name = "DocumentosDesktopDataGridView"
            Me.DocumentosDesktopDataGridView.ReadOnly = True
            DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle7.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.DocumentosDesktopDataGridView.RowHeadersDefaultCellStyle = DataGridViewCellStyle7
            Me.DocumentosDesktopDataGridView.RowHeadersVisible = False
            Me.DocumentosDesktopDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.DocumentosDesktopDataGridView.Size = New System.Drawing.Size(571, 186)
            Me.DocumentosDesktopDataGridView.TabIndex = 16
            '
            'CerrarPrecintoButton
            '
            Me.CerrarPrecintoButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CerrarPrecintoButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnFolderGo
            Me.CerrarPrecintoButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarPrecintoButton.Location = New System.Drawing.Point(600, 141)
            Me.CerrarPrecintoButton.Name = "CerrarPrecintoButton"
            Me.CerrarPrecintoButton.Size = New System.Drawing.Size(132, 30)
            Me.CerrarPrecintoButton.TabIndex = 18
            Me.CerrarPrecintoButton.Text = "Cerrar &Precinto"
            Me.CerrarPrecintoButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CerrarPrecintoButton.UseVisualStyleBackColor = True
            '
            'Label5
            '
            Me.Label5.AutoSize = True
            Me.Label5.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label5.Location = New System.Drawing.Point(12, 14)
            Me.Label5.Name = "Label5"
            Me.Label5.Size = New System.Drawing.Size(109, 19)
            Me.Label5.TabIndex = 17
            Me.Label5.Text = "Documentos"
            '
            'CerrarButton
            '
            Me.CerrarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnSalir
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(599, 202)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(133, 30)
            Me.CerrarButton.TabIndex = 12
            Me.CerrarButton.Text = "&Cerrar      "
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'DestaparButton
            '
            Me.DestaparButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.DestaparButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnDestape
            Me.DestaparButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.DestaparButton.Location = New System.Drawing.Point(600, 70)
            Me.DestaparButton.Name = "DestaparButton"
            Me.DestaparButton.Size = New System.Drawing.Size(132, 30)
            Me.DestaparButton.TabIndex = 11
            Me.DestaparButton.Text = "&Destapar"
            Me.DestaparButton.UseVisualStyleBackColor = True
            '
            'SeleccionarCajaButton
            '
            Me.SeleccionarCajaButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.SeleccionarCajaButton.FlatAppearance.BorderSize = 0
            Me.SeleccionarCajaButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnBuscar2
            Me.SeleccionarCajaButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.SeleccionarCajaButton.Location = New System.Drawing.Point(334, 20)
            Me.SeleccionarCajaButton.Name = "SeleccionarCajaButton"
            Me.SeleccionarCajaButton.Size = New System.Drawing.Size(95, 24)
            Me.SeleccionarCajaButton.TabIndex = 14
            Me.SeleccionarCajaButton.Text = "&Seleccionar"
            Me.SeleccionarCajaButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.SeleccionarCajaButton.UseVisualStyleBackColor = True
            '
            'CajaProcesoLabel
            '
            Me.CajaProcesoLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CajaProcesoLabel.AutoSize = True
            Me.CajaProcesoLabel.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CajaProcesoLabel.ForeColor = System.Drawing.Color.SeaGreen
            Me.CajaProcesoLabel.Location = New System.Drawing.Point(502, 21)
            Me.CajaProcesoLabel.Name = "CajaProcesoLabel"
            Me.CajaProcesoLabel.Size = New System.Drawing.Size(0, 19)
            Me.CajaProcesoLabel.TabIndex = 13
            '
            'Label3
            '
            Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Label3.AutoSize = True
            Me.Label3.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label3.Location = New System.Drawing.Point(444, 21)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(52, 19)
            Me.Label3.TabIndex = 12
            Me.Label3.Text = "Caja:"
            '
            'PrecintoLabel
            '
            Me.PrecintoLabel.AutoSize = True
            Me.PrecintoLabel.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.PrecintoLabel.ForeColor = System.Drawing.Color.SeaGreen
            Me.PrecintoLabel.Location = New System.Drawing.Point(106, 17)
            Me.PrecintoLabel.Name = "PrecintoLabel"
            Me.PrecintoLabel.Size = New System.Drawing.Size(79, 19)
            Me.PrecintoLabel.TabIndex = 11
            Me.PrecintoLabel.Text = "0000000"
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.Location = New System.Drawing.Point(17, 16)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(83, 19)
            Me.Label1.TabIndex = 0
            Me.Label1.Text = "Precinto:"
            '
            'Label2
            '
            Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.Label2.AutoSize = True
            Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label2.Location = New System.Drawing.Point(15, 572)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(573, 13)
            Me.Label2.TabIndex = 18
            Me.Label2.Text = "Para reimprimir codigos de barras por favor de [Doble Click] Sobre el codigo de b" & _
                             "arras en {Carpetas} o {Documentos}."
            '
            'FolderCBarras
            '
            Me.FolderCBarras.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.FolderCBarras.DataPropertyName = "CBarras_folder"
            DataGridViewCellStyle2.NullValue = Nothing
            Me.FolderCBarras.DefaultCellStyle = DataGridViewCellStyle2
            Me.FolderCBarras.HeaderText = "Codigo de Barras"
            Me.FolderCBarras.Name = "FolderCBarras"
            Me.FolderCBarras.ReadOnly = True
            '
            'DocEstado
            '
            Me.DocEstado.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.DocEstado.DataPropertyName = "estado"
            Me.DocEstado.HeaderText = "Estado"
            Me.DocEstado.Name = "DocEstado"
            Me.DocEstado.ReadOnly = True
            '
            'fk_precinto
            '
            Me.fk_precinto.DataPropertyName = "fk_precinto"
            Me.fk_precinto.HeaderText = "fk_precinto"
            Me.fk_precinto.Name = "fk_precinto"
            Me.fk_precinto.ReadOnly = True
            Me.fk_precinto.Visible = False
            Me.fk_precinto.Width = 97
            '
            'fk_estado
            '
            Me.fk_estado.DataPropertyName = "fk_estado"
            Me.fk_estado.HeaderText = "fk_estado"
            Me.fk_estado.Name = "fk_estado"
            Me.fk_estado.ReadOnly = True
            Me.fk_estado.Visible = False
            Me.fk_estado.Width = 89
            '
            'fk_expediente
            '
            Me.fk_expediente.DataPropertyName = "fk_expediente"
            Me.fk_expediente.HeaderText = "fk_expediente"
            Me.fk_expediente.Name = "fk_expediente"
            Me.fk_expediente.ReadOnly = True
            Me.fk_expediente.Visible = False
            Me.fk_expediente.Width = 114
            '
            'id_folder
            '
            Me.id_folder.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.id_folder.DataPropertyName = "id_folder"
            Me.id_folder.HeaderText = "id_folder"
            Me.id_folder.Name = "id_folder"
            Me.id_folder.ReadOnly = True
            Me.id_folder.Visible = False
            '
            'fk_ot
            '
            Me.fk_ot.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.fk_ot.DataPropertyName = "fk_ot"
            Me.fk_ot.HeaderText = "fk_ot"
            Me.fk_ot.Name = "fk_ot"
            Me.fk_ot.ReadOnly = True
            Me.fk_ot.Visible = False
            '
            'CBarras
            '
            Me.CBarras.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
            Me.CBarras.DataPropertyName = "CBarras"
            Me.CBarras.HeaderText = "Codigo de Barras"
            Me.CBarras.Name = "CBarras"
            Me.CBarras.ReadOnly = True
            Me.CBarras.Width = 116
            '
            'Tipo
            '
            Me.Tipo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.Tipo.DataPropertyName = "Tipo"
            Me.Tipo.HeaderText = "Tipologia"
            Me.Tipo.Name = "Tipo"
            Me.Tipo.ReadOnly = True
            '
            'Estado
            '
            Me.Estado.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.Estado.DataPropertyName = "Estado"
            Me.Estado.HeaderText = "Estado"
            Me.Estado.Name = "Estado"
            Me.Estado.ReadOnly = True
            '
            'CProceso
            '
            Me.CProceso.DataPropertyName = "Proceso"
            Me.CProceso.HeaderText = "Tipo"
            Me.CProceso.Name = "CProceso"
            Me.CProceso.ReadOnly = True
            Me.CProceso.Width = 56
            '
            'FormPrecintosCarpetas
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.CerrarButton
            Me.ClientSize = New System.Drawing.Size(792, 594)
            Me.Controls.Add(Me.Label2)
            Me.Controls.Add(Me.GroupBox1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "FormPrecintosCarpetas"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Destape"
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            Me.SplitContainer.Panel1.ResumeLayout(False)
            Me.SplitContainer.Panel1.PerformLayout()
            Me.SplitContainer.Panel2.ResumeLayout(False)
            Me.SplitContainer.Panel2.PerformLayout()
            CType(Me.SplitContainer, System.ComponentModel.ISupportInitialize).EndInit()
            Me.SplitContainer.ResumeLayout(False)
            CType(Me.CarpetasDesktopDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.DocumentosDesktopDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents DestaparButton As System.Windows.Forms.Button
        Friend WithEvents CarpetasDesktopDataGridView As DesktopDataGridViewControl
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents PrecintoLabel As System.Windows.Forms.Label
        Friend WithEvents SeleccionarCajaButton As System.Windows.Forms.Button
        Friend WithEvents CajaProcesoLabel As System.Windows.Forms.Label
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents Label4 As System.Windows.Forms.Label
        Friend WithEvents CerrarPrecintoButton As System.Windows.Forms.Button
        Friend WithEvents Label5 As System.Windows.Forms.Label
        Friend WithEvents DocumentosDesktopDataGridView As DesktopDataGridViewControl
        Friend WithEvents SplitContainer As System.Windows.Forms.SplitContainer
        Friend WithEvents ImprimirButton As System.Windows.Forms.Button
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents FolderCBarras As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents DocEstado As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents fk_precinto As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents fk_estado As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents fk_expediente As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents id_folder As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents fk_ot As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents CBarras As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Tipo As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Estado As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents CProceso As System.Windows.Forms.DataGridViewTextBoxColumn
    End Class
End Namespace