Imports Miharu.Desktop.Controls.DesktopTextBox
Imports Miharu.Desktop.Controls.DesktopComboBox

Namespace Forms.CentroDistribucion
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormCrearRemision
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
            Dim Rango1 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
            Dim Rango2 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
            Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim Rango3 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormCrearRemision))
            Me.RemisionGroupBox = New System.Windows.Forms.GroupBox()
            Me.BovedaComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.ExternoPanel = New System.Windows.Forms.Panel()
            Me.ResponsableDesktopTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.DireccionEnvioDesktopTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.ExternoDesktopCheckBox = New Miharu.Desktop.Controls.DesktopCheckBox.DesktopCheckBoxControl()
            Me.CajasRemicionGroupBox = New System.Windows.Forms.GroupBox()
            Me.IngresarCajaButton = New System.Windows.Forms.Button()
            Me.CajasDataGridView = New System.Windows.Forms.DataGridView()
            Me.Id_Caja = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Cantidad = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.CBarras = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.CBarrasDesktopTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.CrearRemisionButton = New System.Windows.Forms.Button()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.EliminarRemisionButton = New System.Windows.Forms.Button()
            Me.RemisionGroupBox.SuspendLayout()
            Me.ExternoPanel.SuspendLayout()
            Me.CajasRemicionGroupBox.SuspendLayout()
            CType(Me.CajasDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'RemisionGroupBox
            '
            Me.RemisionGroupBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.RemisionGroupBox.Controls.Add(Me.BovedaComboBox)
            Me.RemisionGroupBox.Controls.Add(Me.ExternoPanel)
            Me.RemisionGroupBox.Controls.Add(Me.ExternoDesktopCheckBox)
            Me.RemisionGroupBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.RemisionGroupBox.Location = New System.Drawing.Point(12, 4)
            Me.RemisionGroupBox.Name = "RemisionGroupBox"
            Me.RemisionGroupBox.Size = New System.Drawing.Size(424, 105)
            Me.RemisionGroupBox.TabIndex = 0
            Me.RemisionGroupBox.TabStop = False
            Me.RemisionGroupBox.Text = "Remisión"
            '
            'BovedaComboBox
            '
            Me.BovedaComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.BovedaComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.BovedaComboBox.DisabledEnter = False
            Me.BovedaComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.BovedaComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.BovedaComboBox.FormattingEnabled = True
            Me.BovedaComboBox.ItemHeight = 13
            Me.BovedaComboBox.Location = New System.Drawing.Point(150, 19)
            Me.BovedaComboBox.Name = "BovedaComboBox"
            Me.BovedaComboBox.Size = New System.Drawing.Size(257, 21)
            Me.BovedaComboBox.TabIndex = 5
            '
            'ExternoPanel
            '
            Me.ExternoPanel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ExternoPanel.Controls.Add(Me.ResponsableDesktopTextBox)
            Me.ExternoPanel.Controls.Add(Me.Label2)
            Me.ExternoPanel.Controls.Add(Me.DireccionEnvioDesktopTextBox)
            Me.ExternoPanel.Controls.Add(Me.Label1)
            Me.ExternoPanel.Location = New System.Drawing.Point(10, 43)
            Me.ExternoPanel.Name = "ExternoPanel"
            Me.ExternoPanel.Size = New System.Drawing.Size(406, 54)
            Me.ExternoPanel.TabIndex = 4
            Me.ExternoPanel.Visible = False
            '
            'ResponsableDesktopTextBox
            '
            Me.ResponsableDesktopTextBox.Cantidad_Decimales = CType(2, Short)
            Me.ResponsableDesktopTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.ResponsableDesktopTextBox.DateFormat = Nothing
            Me.ResponsableDesktopTextBox.DisabledEnter = False
            Me.ResponsableDesktopTextBox.DisabledTab = False
            Me.ResponsableDesktopTextBox.EnabledShortCuts = False
            Me.ResponsableDesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.ResponsableDesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.ResponsableDesktopTextBox.Location = New System.Drawing.Point(140, 30)
            Me.ResponsableDesktopTextBox.MaskedTextBox_Property = ""
            Me.ResponsableDesktopTextBox.MaximumLength = CType(0, Short)
            Me.ResponsableDesktopTextBox.MinimumLength = CType(0, Short)
            Me.ResponsableDesktopTextBox.Name = "ResponsableDesktopTextBox"
            Rango1.MaxValue = 2147483647.0R
            Rango1.MinValue = 0.0R
            Me.ResponsableDesktopTextBox.Rango = Rango1
            Me.ResponsableDesktopTextBox.Size = New System.Drawing.Size(249, 20)
            Me.ResponsableDesktopTextBox.TabIndex = 3
            Me.ResponsableDesktopTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal
            Me.ResponsableDesktopTextBox.Usa_Decimales = False
            Me.ResponsableDesktopTextBox.Validos_Cantidad_Puntos = False
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Location = New System.Drawing.Point(-2, 33)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(84, 13)
            Me.Label2.TabIndex = 9
            Me.Label2.Text = "Responsable:"
            '
            'DireccionEnvioDesktopTextBox
            '
            Me.DireccionEnvioDesktopTextBox.Cantidad_Decimales = CType(2, Short)
            Me.DireccionEnvioDesktopTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.DireccionEnvioDesktopTextBox.DateFormat = Nothing
            Me.DireccionEnvioDesktopTextBox.DisabledEnter = False
            Me.DireccionEnvioDesktopTextBox.DisabledTab = False
            Me.DireccionEnvioDesktopTextBox.EnabledShortCuts = False
            Me.DireccionEnvioDesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.DireccionEnvioDesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.DireccionEnvioDesktopTextBox.Location = New System.Drawing.Point(140, 4)
            Me.DireccionEnvioDesktopTextBox.MaskedTextBox_Property = ""
            Me.DireccionEnvioDesktopTextBox.MaximumLength = CType(0, Short)
            Me.DireccionEnvioDesktopTextBox.MinimumLength = CType(0, Short)
            Me.DireccionEnvioDesktopTextBox.Name = "DireccionEnvioDesktopTextBox"
            Rango2.MaxValue = 2147483647.0R
            Rango2.MinValue = 0.0R
            Me.DireccionEnvioDesktopTextBox.Rango = Rango2
            Me.DireccionEnvioDesktopTextBox.Size = New System.Drawing.Size(249, 20)
            Me.DireccionEnvioDesktopTextBox.TabIndex = 2
            Me.DireccionEnvioDesktopTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal
            Me.DireccionEnvioDesktopTextBox.Usa_Decimales = False
            Me.DireccionEnvioDesktopTextBox.Validos_Cantidad_Puntos = False
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(-2, 7)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(103, 13)
            Me.Label1.TabIndex = 7
            Me.Label1.Text = "Dirección Envío:"
            '
            'ExternoDesktopCheckBox
            '
            Me.ExternoDesktopCheckBox.AutoSize = True
            Me.ExternoDesktopCheckBox.DisabledEnter = False
            Me.ExternoDesktopCheckBox.FocusIn = System.Drawing.Color.LightYellow
            Me.ExternoDesktopCheckBox.FocusOut = System.Drawing.SystemColors.Control
            Me.ExternoDesktopCheckBox.Location = New System.Drawing.Point(10, 19)
            Me.ExternoDesktopCheckBox.Name = "ExternoDesktopCheckBox"
            Me.ExternoDesktopCheckBox.Size = New System.Drawing.Size(69, 17)
            Me.ExternoDesktopCheckBox.TabIndex = 0
            Me.ExternoDesktopCheckBox.Text = "Externo"
            Me.ExternoDesktopCheckBox.UseVisualStyleBackColor = True
            '
            'CajasRemicionGroupBox
            '
            Me.CajasRemicionGroupBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CajasRemicionGroupBox.Controls.Add(Me.IngresarCajaButton)
            Me.CajasRemicionGroupBox.Controls.Add(Me.CajasDataGridView)
            Me.CajasRemicionGroupBox.Controls.Add(Me.CBarrasDesktopTextBox)
            Me.CajasRemicionGroupBox.Controls.Add(Me.Label3)
            Me.CajasRemicionGroupBox.Enabled = False
            Me.CajasRemicionGroupBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CajasRemicionGroupBox.Location = New System.Drawing.Point(12, 159)
            Me.CajasRemicionGroupBox.Name = "CajasRemicionGroupBox"
            Me.CajasRemicionGroupBox.Size = New System.Drawing.Size(424, 262)
            Me.CajasRemicionGroupBox.TabIndex = 1
            Me.CajasRemicionGroupBox.TabStop = False
            Me.CajasRemicionGroupBox.Text = "Cajas de la remisión"
            '
            'IngresarCajaButton
            '
            Me.IngresarCajaButton.BackgroundImage = Global.Miharu.Risk.Library.My.Resources.Resources.btnAgregar
            Me.IngresarCajaButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
            Me.IngresarCajaButton.Location = New System.Drawing.Point(306, 24)
            Me.IngresarCajaButton.Name = "IngresarCajaButton"
            Me.IngresarCajaButton.Size = New System.Drawing.Size(101, 23)
            Me.IngresarCajaButton.TabIndex = 5
            Me.IngresarCajaButton.Text = "&Agregar Caja"
            Me.IngresarCajaButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.IngresarCajaButton.UseVisualStyleBackColor = True
            '
            'CajasDataGridView
            '
            Me.CajasDataGridView.AllowUserToAddRows = False
            Me.CajasDataGridView.AllowUserToOrderColumns = True
            Me.CajasDataGridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CajasDataGridView.BackgroundColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
            DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.LightGray
            DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.CajasDataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
            Me.CajasDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.CajasDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Id_Caja, Me.Cantidad, Me.CBarras})
            Me.CajasDataGridView.GridColor = System.Drawing.SystemColors.Control
            Me.CajasDataGridView.Location = New System.Drawing.Point(11, 52)
            Me.CajasDataGridView.Name = "CajasDataGridView"
            Me.CajasDataGridView.ReadOnly = True
            DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightGray
            DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.CajasDataGridView.RowHeadersDefaultCellStyle = DataGridViewCellStyle2
            DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
            DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.LightGray
            DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black
            Me.CajasDataGridView.RowsDefaultCellStyle = DataGridViewCellStyle3
            Me.CajasDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.CajasDataGridView.Size = New System.Drawing.Size(396, 191)
            Me.CajasDataGridView.TabIndex = 2
            '
            'Id_Caja
            '
            Me.Id_Caja.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.Id_Caja.DataPropertyName = "fk_Caja"
            Me.Id_Caja.HeaderText = "Id Caja"
            Me.Id_Caja.Name = "Id_Caja"
            Me.Id_Caja.ReadOnly = True
            '
            'Cantidad
            '
            Me.Cantidad.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.Cantidad.DataPropertyName = "Cantidad_Folders"
            Me.Cantidad.HeaderText = "Cantidad"
            Me.Cantidad.Name = "Cantidad"
            Me.Cantidad.ReadOnly = True
            '
            'CBarras
            '
            Me.CBarras.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.CBarras.DataPropertyName = "Codigo_Caja"
            Me.CBarras.HeaderText = "C. Barras"
            Me.CBarras.Name = "CBarras"
            Me.CBarras.ReadOnly = True
            '
            'CBarrasDesktopTextBox
            '
            Me.CBarrasDesktopTextBox.Cantidad_Decimales = CType(2, Short)
            Me.CBarrasDesktopTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.CBarrasDesktopTextBox.DateFormat = Nothing
            Me.CBarrasDesktopTextBox.DisabledEnter = False
            Me.CBarrasDesktopTextBox.DisabledTab = False
            Me.CBarrasDesktopTextBox.EnabledShortCuts = False
            Me.CBarrasDesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.CBarrasDesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.CBarrasDesktopTextBox.Location = New System.Drawing.Point(149, 26)
            Me.CBarrasDesktopTextBox.MaskedTextBox_Property = ""
            Me.CBarrasDesktopTextBox.MaximumLength = CType(0, Short)
            Me.CBarrasDesktopTextBox.MinimumLength = CType(0, Short)
            Me.CBarrasDesktopTextBox.Name = "CBarrasDesktopTextBox"
            Rango3.MaxValue = 1.7976931348623157E+308R
            Rango3.MinValue = 0.0R
            Me.CBarrasDesktopTextBox.Rango = Rango3
            Me.CBarrasDesktopTextBox.Size = New System.Drawing.Size(151, 20)
            Me.CBarrasDesktopTextBox.TabIndex = 4
            Me.CBarrasDesktopTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Numerico
            Me.CBarrasDesktopTextBox.Usa_Decimales = False
            Me.CBarrasDesktopTextBox.Validos_Cantidad_Puntos = False
            '
            'Label3
            '
            Me.Label3.AutoSize = True
            Me.Label3.Location = New System.Drawing.Point(6, 26)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(108, 13)
            Me.Label3.TabIndex = 0
            Me.Label3.Text = "Código de Barras:"
            '
            'CrearRemisionButton
            '
            Me.CrearRemisionButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CrearRemisionButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnGuardar
            Me.CrearRemisionButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CrearRemisionButton.Location = New System.Drawing.Point(300, 115)
            Me.CrearRemisionButton.Name = "CrearRemisionButton"
            Me.CrearRemisionButton.Size = New System.Drawing.Size(136, 23)
            Me.CrearRemisionButton.TabIndex = 2
            Me.CrearRemisionButton.Text = "&Guardar Remisión"
            Me.CrearRemisionButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CrearRemisionButton.UseVisualStyleBackColor = True
            '
            'CerrarButton
            '
            Me.CerrarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnSalir
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(360, 427)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(75, 23)
            Me.CerrarButton.TabIndex = 3
            Me.CerrarButton.Text = "&Cerrar"
            Me.CerrarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'EliminarRemisionButton
            '
            Me.EliminarRemisionButton.Enabled = False
            Me.EliminarRemisionButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.Cancelar
            Me.EliminarRemisionButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.EliminarRemisionButton.Location = New System.Drawing.Point(161, 115)
            Me.EliminarRemisionButton.Name = "EliminarRemisionButton"
            Me.EliminarRemisionButton.Size = New System.Drawing.Size(134, 23)
            Me.EliminarRemisionButton.TabIndex = 4
            Me.EliminarRemisionButton.Text = "&Eliminar Remisión"
            Me.EliminarRemisionButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.EliminarRemisionButton.UseVisualStyleBackColor = True
            '
            'FormCrearRemision
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.SystemColors.Control
            Me.CancelButton = Me.CerrarButton
            Me.ClientSize = New System.Drawing.Size(448, 457)
            Me.Controls.Add(Me.EliminarRemisionButton)
            Me.Controls.Add(Me.CerrarButton)
            Me.Controls.Add(Me.CrearRemisionButton)
            Me.Controls.Add(Me.CajasRemicionGroupBox)
            Me.Controls.Add(Me.RemisionGroupBox)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "FormCrearRemision"
            Me.ShowInTaskbar = False
            Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Crear Remisión"
            Me.RemisionGroupBox.ResumeLayout(False)
            Me.RemisionGroupBox.PerformLayout()
            Me.ExternoPanel.ResumeLayout(False)
            Me.ExternoPanel.PerformLayout()
            Me.CajasRemicionGroupBox.ResumeLayout(False)
            Me.CajasRemicionGroupBox.PerformLayout()
            CType(Me.CajasDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents RemisionGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents ExternoDesktopCheckBox As Miharu.Desktop.Controls.DesktopCheckBox.DesktopCheckBoxControl
        Friend WithEvents ExternoPanel As System.Windows.Forms.Panel
        Friend WithEvents ResponsableDesktopTextBox As Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents DireccionEnvioDesktopTextBox As Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents CajasRemicionGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents CBarrasDesktopTextBox As DesktopTextboxControl
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents CajasDataGridView As System.Windows.Forms.DataGridView
        Friend WithEvents IngresarCajaButton As System.Windows.Forms.Button
        Friend WithEvents BovedaComboBox As DesktopComboBoxControl
        Friend WithEvents CrearRemisionButton As System.Windows.Forms.Button
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents Id_Caja As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Cantidad As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents CBarras As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents EliminarRemisionButton As System.Windows.Forms.Button
    End Class
End Namespace