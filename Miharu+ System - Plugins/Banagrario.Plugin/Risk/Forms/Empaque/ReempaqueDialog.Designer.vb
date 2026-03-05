Imports Miharu.Desktop.Controls.DesktopComboBox
Imports Miharu.Desktop.Controls.DesktopTextBox
Imports Miharu.Desktop.Controls.Utils

Namespace Risk.Forms.Empaque

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class ReempaqueDialog
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
            Dim Rango2 As Rango = New Rango()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ReempaqueDialog))
            Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
            Me.Aceptar_Button = New System.Windows.Forms.Button()
            Me.Cancel_Button = New System.Windows.Forms.Button()
            Me.Contenedor1DesktopTextBox = New DesktopTextBoxControl()
            Me.Contenedor2DesktopTextBox = New DesktopTextBoxControl()
            Me.Label10 = New System.Windows.Forms.Label()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.Panel1 = New System.Windows.Forms.Panel()
            Me.lblTipoContenedor = New System.Windows.Forms.Label()
            Me.TipoContOKControl = New OkControl()
            Me.ContenedorOkControl = New OkControl()
            Me.TipoCont1DesktopComboBox = New DesktopComboBoxControl()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.TipoCont2ComboBox = New DesktopComboBoxControl()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.TableLayoutPanel1.SuspendLayout()
            Me.Panel1.SuspendLayout()
            Me.SuspendLayout()
            '
            'TableLayoutPanel1
            '
            Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.TableLayoutPanel1.ColumnCount = 2
            Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            Me.TableLayoutPanel1.Controls.Add(Me.Aceptar_Button, 0, 0)
            Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
            Me.TableLayoutPanel1.Location = New System.Drawing.Point(360, 174)
            Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
            Me.TableLayoutPanel1.RowCount = 1
            Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
            Me.TableLayoutPanel1.TabIndex = 0
            '
            'Aceptar_Button
            '
            Me.Aceptar_Button.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.Aceptar_Button.Location = New System.Drawing.Point(3, 3)
            Me.Aceptar_Button.Name = "Aceptar_Button"
            Me.Aceptar_Button.Size = New System.Drawing.Size(67, 23)
            Me.Aceptar_Button.TabIndex = 0
            Me.Aceptar_Button.Text = "Aceptar"
            '
            'Cancel_Button
            '
            Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
            Me.Cancel_Button.Name = "Cancel_Button"
            Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
            Me.Cancel_Button.TabIndex = 1
            Me.Cancel_Button.Text = "Cancel"
            '
            'Contenedor1DesktopTextBox
            '
            Me.Contenedor1DesktopTextBox.Cantidad_Decimales = CType(0, Short)
            Me.Contenedor1DesktopTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.Contenedor1DesktopTextBox.DisabledEnter = False
            Me.Contenedor1DesktopTextBox.DisabledTab = False
            Me.Contenedor1DesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.Contenedor1DesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.Contenedor1DesktopTextBox.Location = New System.Drawing.Point(248, 23)
            Me.Contenedor1DesktopTextBox.MaximumLength = CType(0, Short)
            Me.Contenedor1DesktopTextBox.MaxLength = 0
            Me.Contenedor1DesktopTextBox.MinimumLength = CType(0, Short)
            Me.Contenedor1DesktopTextBox.Name = "Contenedor1DesktopTextBox"
            Rango1.MaxValue = 1.7976931348623157E+308R
            Rango1.MinValue = 0.0R
            Me.Contenedor1DesktopTextBox.Rango = Rango1
            Me.Contenedor1DesktopTextBox.ShortcutsEnabled = False
            Me.Contenedor1DesktopTextBox.Size = New System.Drawing.Size(230, 20)
            Me.Contenedor1DesktopTextBox.TabIndex = 0
            Me.Contenedor1DesktopTextBox.Type = DesktopTextBoxControl.TipoTextBox.Normal
            Me.Contenedor1DesktopTextBox.Usa_Decimales = False
            Me.Contenedor1DesktopTextBox.Validos_Cantidad_Puntos = False
            '
            'Contenedor2DesktopTextBox
            '
            Me.Contenedor2DesktopTextBox.Cantidad_Decimales = CType(0, Short)
            Me.Contenedor2DesktopTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.Contenedor2DesktopTextBox.DisabledEnter = False
            Me.Contenedor2DesktopTextBox.DisabledTab = False
            Me.Contenedor2DesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.Contenedor2DesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.Contenedor2DesktopTextBox.Location = New System.Drawing.Point(248, 51)
            Me.Contenedor2DesktopTextBox.MaximumLength = CType(0, Short)
            Me.Contenedor2DesktopTextBox.MaxLength = 0
            Me.Contenedor2DesktopTextBox.MinimumLength = CType(0, Short)
            Me.Contenedor2DesktopTextBox.Name = "Contenedor2DesktopTextBox"
            Rango2.MaxValue = 1.7976931348623157E+308R
            Rango2.MinValue = 0.0R
            Me.Contenedor2DesktopTextBox.Rango = Rango2
            Me.Contenedor2DesktopTextBox.ShortcutsEnabled = False
            Me.Contenedor2DesktopTextBox.Size = New System.Drawing.Size(230, 20)
            Me.Contenedor2DesktopTextBox.TabIndex = 1
            Me.Contenedor2DesktopTextBox.Type = DesktopTextBoxControl.TipoTextBox.Normal
            Me.Contenedor2DesktopTextBox.Usa_Decimales = False
            Me.Contenedor2DesktopTextBox.Validos_Cantidad_Puntos = False
            '
            'Label10
            '
            Me.Label10.AutoSize = True
            Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label10.Location = New System.Drawing.Point(3, 25)
            Me.Label10.Name = "Label10"
            Me.Label10.Size = New System.Drawing.Size(146, 14)
            Me.Label10.TabIndex = 0
            Me.Label10.Text = "Nuevo No Contenedor:"
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.Location = New System.Drawing.Point(3, 53)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(218, 14)
            Me.Label1.TabIndex = 6
            Me.Label1.Text = "Confirmar Nuevo No Contenendor:"
            '
            'Panel1
            '
            Me.Panel1.Controls.Add(Me.lblTipoContenedor)
            Me.Panel1.Controls.Add(Me.TipoContOKControl)
            Me.Panel1.Controls.Add(Me.ContenedorOkControl)
            Me.Panel1.Controls.Add(Me.TipoCont1DesktopComboBox)
            Me.Panel1.Controls.Add(Me.Label3)
            Me.Panel1.Controls.Add(Me.TipoCont2ComboBox)
            Me.Panel1.Controls.Add(Me.Label2)
            Me.Panel1.Controls.Add(Me.Label10)
            Me.Panel1.Controls.Add(Me.Contenedor2DesktopTextBox)
            Me.Panel1.Controls.Add(Me.Label1)
            Me.Panel1.Controls.Add(Me.Contenedor1DesktopTextBox)
            Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
            Me.Panel1.Location = New System.Drawing.Point(0, 0)
            Me.Panel1.Name = "Panel1"
            Me.Panel1.Size = New System.Drawing.Size(518, 162)
            Me.Panel1.TabIndex = 0
            '
            'lblTipoContenedor
            '
            Me.lblTipoContenedor.BackColor = System.Drawing.SystemColors.ButtonHighlight
            Me.lblTipoContenedor.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblTipoContenedor.Location = New System.Drawing.Point(249, 82)
            Me.lblTipoContenedor.Name = "lblTipoContenedor"
            Me.lblTipoContenedor.Size = New System.Drawing.Size(209, 15)
            Me.lblTipoContenedor.TabIndex = 3
            Me.lblTipoContenedor.Text = "*************"
            Me.lblTipoContenedor.Visible = False
            '
            'TipoContOKControl
            '
            Me.TipoContOKControl.BackgroundImage = CType(resources.GetObject("TipoContOKControl.BackgroundImage"), System.Drawing.Image)
            Me.TipoContOKControl.Location = New System.Drawing.Point(491, 108)
            Me.TipoContOKControl.Name = "TipoContOKControl"
            Me.TipoContOKControl.OK = Microsoft.VisualBasic.TriState.[True]
            Me.TipoContOKControl.Size = New System.Drawing.Size(12, 12)
            Me.TipoContOKControl.TabIndex = 17
            Me.TipoContOKControl.TabStop = False
            Me.TipoContOKControl.Text = "OkControl2"
            Me.TipoContOKControl.Visible = False
            '
            'ContenedorOkControl
            '
            Me.ContenedorOkControl.BackgroundImage = CType(resources.GetObject("ContenedorOkControl.BackgroundImage"), System.Drawing.Image)
            Me.ContenedorOkControl.Location = New System.Drawing.Point(491, 55)
            Me.ContenedorOkControl.Name = "ContenedorOkControl"
            Me.ContenedorOkControl.OK = Microsoft.VisualBasic.TriState.[True]
            Me.ContenedorOkControl.Size = New System.Drawing.Size(12, 12)
            Me.ContenedorOkControl.TabIndex = 16
            Me.ContenedorOkControl.TabStop = False
            Me.ContenedorOkControl.Text = "OkControl2"
            Me.ContenedorOkControl.Visible = False
            '
            'TipoCont1DesktopComboBox
            '
            Me.TipoCont1DesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.TipoCont1DesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.TipoCont1DesktopComboBox.DisabledEnter = True
            Me.TipoCont1DesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.TipoCont1DesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.TipoCont1DesktopComboBox.FormattingEnabled = True
            Me.TipoCont1DesktopComboBox.Location = New System.Drawing.Point(248, 79)
            Me.TipoCont1DesktopComboBox.Name = "TipoCont1DesktopComboBox"
            Me.TipoCont1DesktopComboBox.Size = New System.Drawing.Size(230, 21)
            Me.TipoCont1DesktopComboBox.TabIndex = 2
            '
            'Label3
            '
            Me.Label3.AutoSize = True
            Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label3.Location = New System.Drawing.Point(3, 108)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(178, 14)
            Me.Label3.TabIndex = 9
            Me.Label3.Text = "Confirmar Tipo Contenedor:"
            '
            'TipoCont2ComboBox
            '
            Me.TipoCont2ComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.TipoCont2ComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.TipoCont2ComboBox.DisabledEnter = True
            Me.TipoCont2ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.TipoCont2ComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.TipoCont2ComboBox.FormattingEnabled = True
            Me.TipoCont2ComboBox.Location = New System.Drawing.Point(248, 106)
            Me.TipoCont2ComboBox.Name = "TipoCont2ComboBox"
            Me.TipoCont2ComboBox.Size = New System.Drawing.Size(230, 21)
            Me.TipoCont2ComboBox.TabIndex = 3
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label2.Location = New System.Drawing.Point(3, 81)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(114, 14)
            Me.Label2.TabIndex = 7
            Me.Label2.Text = "Tipo Contenedor:"
            '
            'ReempaqueDialog
            '
            Me.AcceptButton = Me.Aceptar_Button
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.Cancel_Button
            Me.ClientSize = New System.Drawing.Size(518, 215)
            Me.Controls.Add(Me.Panel1)
            Me.Controls.Add(Me.TableLayoutPanel1)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "ReempaqueDialog"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Cambiar Sticker"
            Me.TableLayoutPanel1.ResumeLayout(False)
            Me.Panel1.ResumeLayout(False)
            Me.Panel1.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
        Friend WithEvents Aceptar_Button As System.Windows.Forms.Button
        Friend WithEvents Cancel_Button As System.Windows.Forms.Button
        Friend WithEvents Contenedor1DesktopTextBox As DesktopTextBoxControl
        Friend WithEvents Contenedor2DesktopTextBox As DesktopTextBoxControl
        Friend WithEvents Label10 As System.Windows.Forms.Label
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents Panel1 As System.Windows.Forms.Panel
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents TipoCont2ComboBox As DesktopComboBoxControl
        Friend WithEvents TipoContOKControl As OkControl
        Friend WithEvents ContenedorOkControl As OkControl
        Friend WithEvents lblTipoContenedor As System.Windows.Forms.Label
        Friend WithEvents TipoCont1DesktopComboBox As DesktopComboBoxControl

    End Class
End Namespace