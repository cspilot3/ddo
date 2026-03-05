Imports Miharu.Desktop.Controls.DesktopCBarras
Imports Miharu.Desktop.Controls.DesktopTextBox
Imports Miharu.Desktop.Controls.DesktopComboBox
Imports Miharu.Desktop.Library

Namespace Forms.ProcesamientoLight

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class Form_ImpresionCBarras_PL
        Inherits FormBase

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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_ImpresionCBarras_PL))
            Me.Label1 = New System.Windows.Forms.Label()
            Me.CBarrasDesktopTextBox = New DesktopTextBoxControl()
            Me.CBarrar_PLDesktopTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.ImprimirButton = New System.Windows.Forms.Button()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.Panel1 = New System.Windows.Forms.Panel()
            Me.Panel2 = New System.Windows.Forms.Panel()
            Me.cbarrasBarCodeControl = New Miharu.Desktop.Controls.BarCode.BarCodeControl()
            Me.BuscarButton = New System.Windows.Forms.Button()
            Me.EntidadDesktopComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.ProyectoDesktopComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.EsquemaDesktopComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.Label4 = New System.Windows.Forms.Label()
            Me.Label5 = New System.Windows.Forms.Label()
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.Panel1.SuspendLayout()
            Me.Panel2.SuspendLayout()
            Me.GroupBox1.SuspendLayout()
            Me.SuspendLayout()
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(12, 18)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(221, 13)
            Me.Label1.TabIndex = 34
            Me.Label1.Text = "Codigo de Barras Procesamiento Light"
            '
            'CBarrasDesktopTextBox
            '            
            Me.CBarrasDesktopTextBox.Enabled = False
            Me.CBarrasDesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.CBarrasDesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.CBarrasDesktopTextBox.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CBarrasDesktopTextBox.Location = New System.Drawing.Point(369, 43)
            Me.CBarrasDesktopTextBox.MaxLength = 0
            Me.CBarrasDesktopTextBox.Name = "CBarrasDesktopTextBox"
            Me.CBarrasDesktopTextBox.ShortcutsEnabled = False
            Me.CBarrasDesktopTextBox.Size = New System.Drawing.Size(333, 20)
            Me.CBarrasDesktopTextBox.TabIndex = 10
            '
            'CBarrar_PLDesktopTextBox
            '
            Me.CBarrar_PLDesktopTextBox.Cantidad_Decimales = CType(2, Short)
            Me.CBarrar_PLDesktopTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.CBarrar_PLDesktopTextBox.DateFormat = Nothing
            Me.CBarrar_PLDesktopTextBox.DisabledEnter = False
            Me.CBarrar_PLDesktopTextBox.DisabledTab = False
            Me.CBarrar_PLDesktopTextBox.EnabledShortCuts = False
            Me.CBarrar_PLDesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.CBarrar_PLDesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.CBarrar_PLDesktopTextBox.Location = New System.Drawing.Point(15, 42)
            Me.CBarrar_PLDesktopTextBox.MaskedTextBox_Property = ""
            Me.CBarrar_PLDesktopTextBox.MaximumLength = CType(0, Short)
            Me.CBarrar_PLDesktopTextBox.MinimumLength = CType(0, Short)
            Me.CBarrar_PLDesktopTextBox.Name = "CBarrar_PLDesktopTextBox"
            Rango1.MaxValue = 9.2233720368547758E+18R
            Rango1.MinValue = 0.0R
            Me.CBarrar_PLDesktopTextBox.Rango = Rango1
            Me.CBarrar_PLDesktopTextBox.Size = New System.Drawing.Size(333, 21)
            Me.CBarrar_PLDesktopTextBox.TabIndex = 0
            Me.CBarrar_PLDesktopTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Numerico
            Me.CBarrar_PLDesktopTextBox.Usa_Decimales = False
            Me.CBarrar_PLDesktopTextBox.Validos_Cantidad_Puntos = False
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Location = New System.Drawing.Point(366, 18)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(139, 13)
            Me.Label2.TabIndex = 39
            Me.Label2.Text = "Codigo de Barras [Risk]"
            '
            'ImprimirButton
            '
            Me.ImprimirButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnPrinter
            Me.ImprimirButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.ImprimirButton.Location = New System.Drawing.Point(148, 237)
            Me.ImprimirButton.Name = "ImprimirButton"
            Me.ImprimirButton.Size = New System.Drawing.Size(97, 30)
            Me.ImprimirButton.TabIndex = 3
            Me.ImprimirButton.Text = "&Imprimir"
            Me.ImprimirButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.ImprimirButton.UseVisualStyleBackColor = True
            '
            'CerrarButton
            '
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnSalir
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(251, 237)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(97, 30)
            Me.CerrarButton.TabIndex = 4
            Me.CerrarButton.Text = "&Cerrar"
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'Panel1
            '
            Me.Panel1.BackColor = System.Drawing.SystemColors.Desktop
            Me.Panel1.Controls.Add(Me.Panel2)
            Me.Panel1.Location = New System.Drawing.Point(369, 78)
            Me.Panel1.Name = "Panel1"
            Me.Panel1.Size = New System.Drawing.Size(336, 189)
            Me.Panel1.TabIndex = 42
            '
            'Panel2
            '
            Me.Panel2.BackColor = System.Drawing.SystemColors.Control
            Me.Panel2.Controls.Add(Me.cbarrasBarCodeControl)
            Me.Panel2.Location = New System.Drawing.Point(10, 11)
            Me.Panel2.Name = "Panel2"
            Me.Panel2.Size = New System.Drawing.Size(318, 167)
            Me.Panel2.TabIndex = 2
            '
            'cbarrasBarCodeControl
            '
            Me.cbarrasBarCodeControl.Align = Miharu.Desktop.Controls.BarCode.AlignType.Center
            Me.cbarrasBarCodeControl.AutoPrint = False
            Me.cbarrasBarCodeControl.BackColor = System.Drawing.SystemColors.Window
            Me.cbarrasBarCodeControl.BarCode = "1234567890"
            Me.cbarrasBarCodeControl.BarCodeHeight = 30
            Me.cbarrasBarCodeControl.BarCodePrintDocument = Nothing
            Me.cbarrasBarCodeControl.BarCodeType = Miharu.Desktop.Controls.BarCode.BarCodeTypeType.EAN128
            Me.cbarrasBarCodeControl.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.cbarrasBarCodeControl.FooterColumns = 2
            Me.cbarrasBarCodeControl.FooterFont = New System.Drawing.Font("Tahoma", 7.0!)
            Me.cbarrasBarCodeControl.FooterLinesString = ""
            Me.cbarrasBarCodeControl.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 18.0!)
            Me.cbarrasBarCodeControl.HeaderText = "PYC"
            Me.cbarrasBarCodeControl.LeftMargin = 1
            Me.cbarrasBarCodeControl.Location = New System.Drawing.Point(8, 8)
            Me.cbarrasBarCodeControl.Name = "cbarrasBarCodeControl"
            Me.cbarrasBarCodeControl.Padding = New System.Windows.Forms.Padding(1)
            Me.cbarrasBarCodeControl.ShowFooter = True
            Me.cbarrasBarCodeControl.ShowHeader = True
            Me.cbarrasBarCodeControl.Size = New System.Drawing.Size(302, 150)
            Me.cbarrasBarCodeControl.TabIndex = 0
            Me.cbarrasBarCodeControl.TopMargin = 1
            Me.cbarrasBarCodeControl.Weight = Miharu.Desktop.Controls.BarCode.BarCodeWeight.Small
            '
            'BuscarButton
            '
            Me.BuscarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnBuscar
            Me.BuscarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.BuscarButton.Location = New System.Drawing.Point(223, 69)
            Me.BuscarButton.Name = "BuscarButton"
            Me.BuscarButton.Size = New System.Drawing.Size(125, 30)
            Me.BuscarButton.TabIndex = 1
            Me.BuscarButton.Text = "Buscar en Risk"
            Me.BuscarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.BuscarButton.UseVisualStyleBackColor = True
            '
            'EntidadDesktopComboBox
            '
            Me.EntidadDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.EntidadDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.EntidadDesktopComboBox.DisabledEnter = False
            Me.EntidadDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.EntidadDesktopComboBox.Enabled = False
            Me.EntidadDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.EntidadDesktopComboBox.FormattingEnabled = True
            Me.EntidadDesktopComboBox.Location = New System.Drawing.Point(95, 28)
            Me.EntidadDesktopComboBox.Name = "EntidadDesktopComboBox"
            Me.EntidadDesktopComboBox.Size = New System.Drawing.Size(228, 21)
            Me.EntidadDesktopComboBox.TabIndex = 1
            '
            'ProyectoDesktopComboBox
            '
            Me.ProyectoDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.ProyectoDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.ProyectoDesktopComboBox.DisabledEnter = False
            Me.ProyectoDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.ProyectoDesktopComboBox.Enabled = False
            Me.ProyectoDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.ProyectoDesktopComboBox.FormattingEnabled = True
            Me.ProyectoDesktopComboBox.Location = New System.Drawing.Point(95, 55)
            Me.ProyectoDesktopComboBox.Name = "ProyectoDesktopComboBox"
            Me.ProyectoDesktopComboBox.Size = New System.Drawing.Size(229, 21)
            Me.ProyectoDesktopComboBox.TabIndex = 2
            '
            'EsquemaDesktopComboBox
            '
            Me.EsquemaDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.EsquemaDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.EsquemaDesktopComboBox.DisabledEnter = False
            Me.EsquemaDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.EsquemaDesktopComboBox.Enabled = False
            Me.EsquemaDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.EsquemaDesktopComboBox.FormattingEnabled = True
            Me.EsquemaDesktopComboBox.Location = New System.Drawing.Point(95, 82)
            Me.EsquemaDesktopComboBox.Name = "EsquemaDesktopComboBox"
            Me.EsquemaDesktopComboBox.Size = New System.Drawing.Size(228, 21)
            Me.EsquemaDesktopComboBox.TabIndex = 3
            '
            'Label3
            '
            Me.Label3.AutoSize = True
            Me.Label3.Location = New System.Drawing.Point(31, 31)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(49, 13)
            Me.Label3.TabIndex = 46
            Me.Label3.Text = "Entidad"
            '
            'Label4
            '
            Me.Label4.AutoSize = True
            Me.Label4.Location = New System.Drawing.Point(31, 58)
            Me.Label4.Name = "Label4"
            Me.Label4.Size = New System.Drawing.Size(58, 13)
            Me.Label4.TabIndex = 47
            Me.Label4.Text = "Proyecto"
            '
            'Label5
            '
            Me.Label5.AutoSize = True
            Me.Label5.Location = New System.Drawing.Point(31, 85)
            Me.Label5.Name = "Label5"
            Me.Label5.Size = New System.Drawing.Size(58, 13)
            Me.Label5.TabIndex = 48
            Me.Label5.Text = "Esquema"
            '
            'GroupBox1
            '
            Me.GroupBox1.Controls.Add(Me.Label3)
            Me.GroupBox1.Controls.Add(Me.Label5)
            Me.GroupBox1.Controls.Add(Me.EntidadDesktopComboBox)
            Me.GroupBox1.Controls.Add(Me.Label4)
            Me.GroupBox1.Controls.Add(Me.ProyectoDesktopComboBox)
            Me.GroupBox1.Controls.Add(Me.EsquemaDesktopComboBox)
            Me.GroupBox1.Location = New System.Drawing.Point(12, 108)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(336, 115)
            Me.GroupBox1.TabIndex = 2
            Me.GroupBox1.TabStop = False
            Me.GroupBox1.Text = "Detalle"
            '
            'Form_ImpresionCBarras_PL
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.CerrarButton
            Me.ClientSize = New System.Drawing.Size(720, 279)
            Me.Controls.Add(Me.GroupBox1)
            Me.Controls.Add(Me.BuscarButton)
            Me.Controls.Add(Me.Panel1)
            Me.Controls.Add(Me.ImprimirButton)
            Me.Controls.Add(Me.CerrarButton)
            Me.Controls.Add(Me.Label2)
            Me.Controls.Add(Me.CBarrar_PLDesktopTextBox)
            Me.Controls.Add(Me.CBarrasDesktopTextBox)
            Me.Controls.Add(Me.Label1)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "Form_ImpresionCBarras_PL"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Impresion Codigo Barras PL"
            Me.Panel1.ResumeLayout(False)
            Me.Panel2.ResumeLayout(False)
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents CBarrasDesktopTextBox As DesktopTextBoxControl
        Friend WithEvents CBarrar_PLDesktopTextBox As DesktopTextBoxControl
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents ImprimirButton As System.Windows.Forms.Button
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents Panel1 As System.Windows.Forms.Panel
        Friend WithEvents Panel2 As System.Windows.Forms.Panel
        Friend WithEvents cbarrasBarCodeControl As Miharu.Desktop.Controls.BarCode.BarCodeControl
        Friend WithEvents BuscarButton As System.Windows.Forms.Button
        Friend WithEvents EntidadDesktopComboBox As DesktopComboBoxControl
        Friend WithEvents ProyectoDesktopComboBox As DesktopComboBoxControl
        Friend WithEvents EsquemaDesktopComboBox As DesktopComboBoxControl
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents Label4 As System.Windows.Forms.Label
        Friend WithEvents Label5 As System.Windows.Forms.Label
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    End Class
End Namespace