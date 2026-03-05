Imports Miharu.Desktop.Controls.DesktopTextBox

Namespace View.Indexacion.Table

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class IndexerTable
        Inherits System.Windows.Forms.UserControl

        'UserControl reemplaza a Dispose para limpiar la lista de componentes.
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

        'Requerido por el Diseñador de Windows Forms
        Private components As System.ComponentModel.IContainer

        'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
        'Se puede modificar usando el Diseñador de Windows Forms.  
        'No lo modifique con el editor de código.
        <System.Diagnostics.DebuggerStepThrough()> _
        Private Sub InitializeComponent()
            Dim Rango1 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
            Dim Rango2 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
            Me.UpPanel = New System.Windows.Forms.Panel()
            Me.HeaderButton = New System.Windows.Forms.Button()
            Me.ColumnHeaderFlowLayoutPanel = New System.Windows.Forms.FlowLayoutPanel()
            Me.BodyFlowLayoutPanel = New System.Windows.Forms.FlowLayoutPanel()
            Me.RowHeaderFlowLayoutPanel = New System.Windows.Forms.FlowLayoutPanel()
            Me.LeftPanel = New System.Windows.Forms.Panel()
            Me.ControlPanel = New System.Windows.Forms.Panel()
            Me.PanelControlValor = New System.Windows.Forms.Panel()
            Me.unLockButton = New System.Windows.Forms.Button()
            Me.controlValorDesktopTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
            Me.ControlValorLabel = New System.Windows.Forms.Label()
            Me.PanelControlRegistros = New System.Windows.Forms.Panel()
            Me.controlRegistrosDesktopTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
            Me.ControlRegistrosLabel = New System.Windows.Forms.Label()
            Me.UpPanel.SuspendLayout()
            Me.LeftPanel.SuspendLayout()
            Me.ControlPanel.SuspendLayout()
            Me.PanelControlValor.SuspendLayout()
            Me.PanelControlRegistros.SuspendLayout()
            Me.SuspendLayout()
            '
            'UpPanel
            '
            Me.UpPanel.BackColor = System.Drawing.SystemColors.Control
            Me.UpPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.UpPanel.Controls.Add(Me.HeaderButton)
            Me.UpPanel.Controls.Add(Me.ColumnHeaderFlowLayoutPanel)
            Me.UpPanel.Dock = System.Windows.Forms.DockStyle.Top
            Me.UpPanel.Location = New System.Drawing.Point(0, 33)
            Me.UpPanel.Margin = New System.Windows.Forms.Padding(0)
            Me.UpPanel.Name = "UpPanel"
            Me.UpPanel.Size = New System.Drawing.Size(430, 25)
            Me.UpPanel.TabIndex = 0
            '
            'HeaderButton
            '
            Me.HeaderButton.BackColor = System.Drawing.SystemColors.Control
            Me.HeaderButton.Dock = System.Windows.Forms.DockStyle.Left
            Me.HeaderButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.HeaderButton.Location = New System.Drawing.Point(0, 0)
            Me.HeaderButton.Margin = New System.Windows.Forms.Padding(0)
            Me.HeaderButton.Name = "HeaderButton"
            Me.HeaderButton.Size = New System.Drawing.Size(50, 23)
            Me.HeaderButton.TabIndex = 0
            Me.HeaderButton.UseVisualStyleBackColor = False
            '
            'ColumnHeaderFlowLayoutPanel
            '
            Me.ColumnHeaderFlowLayoutPanel.Location = New System.Drawing.Point(50, 0)
            Me.ColumnHeaderFlowLayoutPanel.Margin = New System.Windows.Forms.Padding(0)
            Me.ColumnHeaderFlowLayoutPanel.Name = "ColumnHeaderFlowLayoutPanel"
            Me.ColumnHeaderFlowLayoutPanel.Size = New System.Drawing.Size(200, 25)
            Me.ColumnHeaderFlowLayoutPanel.TabIndex = 1
            Me.ColumnHeaderFlowLayoutPanel.WrapContents = False
            '
            'BodyFlowLayoutPanel
            '
            Me.BodyFlowLayoutPanel.AutoScroll = True
            Me.BodyFlowLayoutPanel.BackColor = System.Drawing.SystemColors.ControlDark
            Me.BodyFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill
            Me.BodyFlowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
            Me.BodyFlowLayoutPanel.Location = New System.Drawing.Point(50, 58)
            Me.BodyFlowLayoutPanel.Margin = New System.Windows.Forms.Padding(0)
            Me.BodyFlowLayoutPanel.Name = "BodyFlowLayoutPanel"
            Me.BodyFlowLayoutPanel.Size = New System.Drawing.Size(380, 143)
            Me.BodyFlowLayoutPanel.TabIndex = 1
            '
            'RowHeaderFlowLayoutPanel
            '
            Me.RowHeaderFlowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
            Me.RowHeaderFlowLayoutPanel.Location = New System.Drawing.Point(0, 0)
            Me.RowHeaderFlowLayoutPanel.Margin = New System.Windows.Forms.Padding(0)
            Me.RowHeaderFlowLayoutPanel.Name = "RowHeaderFlowLayoutPanel"
            Me.RowHeaderFlowLayoutPanel.Size = New System.Drawing.Size(50, 104)
            Me.RowHeaderFlowLayoutPanel.TabIndex = 0
            Me.RowHeaderFlowLayoutPanel.WrapContents = False
            '
            'LeftPanel
            '
            Me.LeftPanel.BackColor = System.Drawing.SystemColors.Control
            Me.LeftPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.LeftPanel.Controls.Add(Me.RowHeaderFlowLayoutPanel)
            Me.LeftPanel.Dock = System.Windows.Forms.DockStyle.Left
            Me.LeftPanel.Location = New System.Drawing.Point(0, 58)
            Me.LeftPanel.Margin = New System.Windows.Forms.Padding(0)
            Me.LeftPanel.Name = "LeftPanel"
            Me.LeftPanel.Size = New System.Drawing.Size(50, 143)
            Me.LeftPanel.TabIndex = 5
            '
            'ControlPanel
            '
            Me.ControlPanel.Controls.Add(Me.unLockButton)
            Me.ControlPanel.Controls.Add(Me.PanelControlValor)
            Me.ControlPanel.Controls.Add(Me.PanelControlRegistros)
            Me.ControlPanel.Dock = System.Windows.Forms.DockStyle.Top
            Me.ControlPanel.Location = New System.Drawing.Point(0, 0)
            Me.ControlPanel.Name = "ControlPanel"
            Me.ControlPanel.Size = New System.Drawing.Size(430, 33)
            Me.ControlPanel.TabIndex = 0
            '
            'PanelControlValor
            '
            Me.PanelControlValor.Controls.Add(Me.controlValorDesktopTextBox)
            Me.PanelControlValor.Controls.Add(Me.ControlValorLabel)
            Me.PanelControlValor.Dock = System.Windows.Forms.DockStyle.Left
            Me.PanelControlValor.Location = New System.Drawing.Point(153, 0)
            Me.PanelControlValor.Name = "PanelControlValor"
            Me.PanelControlValor.Size = New System.Drawing.Size(221, 33)
            Me.PanelControlValor.TabIndex = 1
            '
            'unLockButton
            '
            Me.unLockButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.unLockButton.Image = Global.Miharu.Imaging.Indexer.My.Resources.Resources.lock_open
            Me.unLockButton.Location = New System.Drawing.Point(397, 4)
            Me.unLockButton.Name = "unLockButton"
            Me.unLockButton.Size = New System.Drawing.Size(30, 23)
            Me.unLockButton.TabIndex = 2
            Me.unLockButton.UseVisualStyleBackColor = True
            '
            'controlValorDesktopTextBox
            '
            Me.controlValorDesktopTextBox.BackColor = System.Drawing.Color.LightGray
            Me.controlValorDesktopTextBox.Cantidad_Decimales = CType(2, Short)
            Me.controlValorDesktopTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(46)
            Me.controlValorDesktopTextBox.DateFormat = Nothing
            Me.controlValorDesktopTextBox.DisabledEnter = True
            Me.controlValorDesktopTextBox.DisabledTab = True
            Me.controlValorDesktopTextBox.EnabledShortCuts = False
            Me.controlValorDesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.controlValorDesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.controlValorDesktopTextBox.Location = New System.Drawing.Point(81, 4)
            Me.controlValorDesktopTextBox.MaskedTextBox_Property = ""
            Me.controlValorDesktopTextBox.MaximumLength = CType(0, Short)
            Me.controlValorDesktopTextBox.MinimumLength = CType(1, Short)
            Me.controlValorDesktopTextBox.Name = "controlValorDesktopTextBox"
            Rango1.MaxValue = 999999999999.0R
            Rango1.MinValue = 0.0R
            Me.controlValorDesktopTextBox.Rango = Rango1
            Me.controlValorDesktopTextBox.ReadOnly = True
            Me.controlValorDesktopTextBox.Size = New System.Drawing.Size(131, 20)
            Me.controlValorDesktopTextBox.TabIndex = 1
            Me.controlValorDesktopTextBox.Text = "0.00"
            Me.controlValorDesktopTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Numerico
            Me.controlValorDesktopTextBox.Usa_Decimales = True
            Me.controlValorDesktopTextBox.Validos_Cantidad_Puntos = False
            '
            'ControlValorLabel
            '
            Me.ControlValorLabel.AutoSize = True
            Me.ControlValorLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ControlValorLabel.Location = New System.Drawing.Point(6, 8)
            Me.ControlValorLabel.Name = "ControlValorLabel"
            Me.ControlValorLabel.Size = New System.Drawing.Size(69, 13)
            Me.ControlValorLabel.TabIndex = 0
            Me.ControlValorLabel.Text = "Valor Total"
            '
            'PanelControlRegistros
            '
            Me.PanelControlRegistros.Controls.Add(Me.controlRegistrosDesktopTextBox)
            Me.PanelControlRegistros.Controls.Add(Me.ControlRegistrosLabel)
            Me.PanelControlRegistros.Dock = System.Windows.Forms.DockStyle.Left
            Me.PanelControlRegistros.Location = New System.Drawing.Point(0, 0)
            Me.PanelControlRegistros.Name = "PanelControlRegistros"
            Me.PanelControlRegistros.Size = New System.Drawing.Size(153, 33)
            Me.PanelControlRegistros.TabIndex = 0
            '
            'controlRegistrosDesktopTextBox
            '
            Me.controlRegistrosDesktopTextBox.BackColor = System.Drawing.Color.LightGray
            Me.controlRegistrosDesktopTextBox.Cantidad_Decimales = CType(0, Short)
            Me.controlRegistrosDesktopTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.controlRegistrosDesktopTextBox.DateFormat = Nothing
            Me.controlRegistrosDesktopTextBox.DisabledEnter = True
            Me.controlRegistrosDesktopTextBox.DisabledTab = True
            Me.controlRegistrosDesktopTextBox.EnabledShortCuts = False
            Me.controlRegistrosDesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.controlRegistrosDesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.controlRegistrosDesktopTextBox.Location = New System.Drawing.Point(70, 4)
            Me.controlRegistrosDesktopTextBox.MaskedTextBox_Property = ""
            Me.controlRegistrosDesktopTextBox.MaximumLength = CType(0, Short)
            Me.controlRegistrosDesktopTextBox.MinimumLength = CType(1, Short)
            Me.controlRegistrosDesktopTextBox.Name = "controlRegistrosDesktopTextBox"
            Rango2.MaxValue = 1000000.0R
            Rango2.MinValue = 0.0R
            Me.controlRegistrosDesktopTextBox.Rango = Rango2
            Me.controlRegistrosDesktopTextBox.ReadOnly = True
            Me.controlRegistrosDesktopTextBox.Size = New System.Drawing.Size(78, 20)
            Me.controlRegistrosDesktopTextBox.TabIndex = 0
            Me.controlRegistrosDesktopTextBox.Text = "0"
            Me.controlRegistrosDesktopTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Numerico
            Me.controlRegistrosDesktopTextBox.Usa_Decimales = False
            Me.controlRegistrosDesktopTextBox.Validos_Cantidad_Puntos = False
            '
            'ControlRegistrosLabel
            '
            Me.ControlRegistrosLabel.AutoSize = True
            Me.ControlRegistrosLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ControlRegistrosLabel.Location = New System.Drawing.Point(3, 8)
            Me.ControlRegistrosLabel.Name = "ControlRegistrosLabel"
            Me.ControlRegistrosLabel.Size = New System.Drawing.Size(60, 13)
            Me.ControlRegistrosLabel.TabIndex = 1
            Me.ControlRegistrosLabel.Text = "Registros"
            '
            'IndexerTable
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Controls.Add(Me.BodyFlowLayoutPanel)
            Me.Controls.Add(Me.LeftPanel)
            Me.Controls.Add(Me.UpPanel)
            Me.Controls.Add(Me.ControlPanel)
            Me.Margin = New System.Windows.Forms.Padding(0)
            Me.Name = "IndexerTable"
            Me.Size = New System.Drawing.Size(430, 201)
            Me.UpPanel.ResumeLayout(False)
            Me.LeftPanel.ResumeLayout(False)
            Me.ControlPanel.ResumeLayout(False)
            Me.PanelControlValor.ResumeLayout(False)
            Me.PanelControlValor.PerformLayout()
            Me.PanelControlRegistros.ResumeLayout(False)
            Me.PanelControlRegistros.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents UpPanel As System.Windows.Forms.Panel
        Friend WithEvents HeaderButton As System.Windows.Forms.Button
        Friend WithEvents BodyFlowLayoutPanel As System.Windows.Forms.FlowLayoutPanel
        Friend WithEvents ColumnHeaderFlowLayoutPanel As System.Windows.Forms.FlowLayoutPanel
        Friend WithEvents RowHeaderFlowLayoutPanel As System.Windows.Forms.FlowLayoutPanel
        Friend WithEvents LeftPanel As System.Windows.Forms.Panel
        Friend WithEvents ControlPanel As System.Windows.Forms.Panel
        Friend WithEvents ControlValorLabel As System.Windows.Forms.Label
        Friend WithEvents ControlRegistrosLabel As System.Windows.Forms.Label
        Friend WithEvents controlValorDesktopTextBox As DesktopTextBoxControl
        Friend WithEvents controlRegistrosDesktopTextBox As DesktopTextBoxControl
        Friend WithEvents PanelControlValor As System.Windows.Forms.Panel
        Friend WithEvents PanelControlRegistros As System.Windows.Forms.Panel
        Friend WithEvents unLockButton As System.Windows.Forms.Button

    End Class

End Namespace