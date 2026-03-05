Imports Miharu.Desktop.Controls.DesktopComboBox
Imports Miharu.Imaging.OffLineViewer.Library.Visor

Namespace Controls
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormAdicionarDocumento
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormAdicionarDocumento))
            Me.cmbTipologia = New DesktopComboBoxControl()
            Me.cmbConfirmarTipologia = New DesktopComboBoxControl()
            Me.gbEncabezado = New System.Windows.Forms.GroupBox()
            Me.AdicionarImagenButton = New System.Windows.Forms.Button()
            Me.CargarImagenButton = New System.Windows.Forms.Button()
            Me.lblConfirmarDocumento = New System.Windows.Forms.Label()
            Me.lblDocumento = New System.Windows.Forms.Label()
            Me.gbSolicitud = New System.Windows.Forms.GroupBox()
            Me.CentralOffLineViewer = New ImageViewer()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.cmbFechaProceso = New DesktopComboBoxControl()
            Me.cmbOT = New DesktopComboBoxControl()
            Me.gbEncabezado.SuspendLayout()
            Me.gbSolicitud.SuspendLayout()
            Me.SuspendLayout()
            '
            'cmbTipologia
            '
            Me.cmbTipologia.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.cmbTipologia.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.cmbTipologia.DisabledEnter = False
            Me.cmbTipologia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cmbTipologia.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.cmbTipologia.FormattingEnabled = True
            Me.cmbTipologia.Location = New System.Drawing.Point(147, 70)
            Me.cmbTipologia.Name = "cmbTipologia"
            Me.cmbTipologia.Size = New System.Drawing.Size(314, 21)
            Me.cmbTipologia.TabIndex = 0
            '
            'cmbConfirmarTipologia
            '
            Me.cmbConfirmarTipologia.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.cmbConfirmarTipologia.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.cmbConfirmarTipologia.DisabledEnter = False
            Me.cmbConfirmarTipologia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cmbConfirmarTipologia.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.cmbConfirmarTipologia.FormattingEnabled = True
            Me.cmbConfirmarTipologia.Location = New System.Drawing.Point(147, 99)
            Me.cmbConfirmarTipologia.Name = "cmbConfirmarTipologia"
            Me.cmbConfirmarTipologia.Size = New System.Drawing.Size(314, 21)
            Me.cmbConfirmarTipologia.TabIndex = 1
            '
            'gbEncabezado
            '
            Me.gbEncabezado.Controls.Add(Me.Label1)
            Me.gbEncabezado.Controls.Add(Me.Label2)
            Me.gbEncabezado.Controls.Add(Me.cmbFechaProceso)
            Me.gbEncabezado.Controls.Add(Me.cmbOT)
            Me.gbEncabezado.Controls.Add(Me.AdicionarImagenButton)
            Me.gbEncabezado.Controls.Add(Me.CargarImagenButton)
            Me.gbEncabezado.Controls.Add(Me.lblConfirmarDocumento)
            Me.gbEncabezado.Controls.Add(Me.lblDocumento)
            Me.gbEncabezado.Controls.Add(Me.cmbTipologia)
            Me.gbEncabezado.Controls.Add(Me.cmbConfirmarTipologia)
            Me.gbEncabezado.Location = New System.Drawing.Point(12, 12)
            Me.gbEncabezado.Name = "gbEncabezado"
            Me.gbEncabezado.Size = New System.Drawing.Size(778, 149)
            Me.gbEncabezado.TabIndex = 2
            Me.gbEncabezado.TabStop = False
            '
            'AdicionarImagenButton
            '
            Me.AdicionarImagenButton.BackColor = System.Drawing.SystemColors.Control
            Me.AdicionarImagenButton.FlatAppearance.BorderSize = 0
            Me.AdicionarImagenButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
            Me.AdicionarImagenButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
            Me.AdicionarImagenButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.sheet_add
            Me.AdicionarImagenButton.Location = New System.Drawing.Point(538, 91)
            Me.AdicionarImagenButton.Name = "AdicionarImagenButton"
            Me.AdicionarImagenButton.Size = New System.Drawing.Size(34, 34)
            Me.AdicionarImagenButton.TabIndex = 10
            Me.AdicionarImagenButton.UseVisualStyleBackColor = False
            '
            'CargarImagenButton
            '
            Me.CargarImagenButton.BackColor = System.Drawing.SystemColors.Control
            Me.CargarImagenButton.FlatAppearance.BorderSize = 0
            Me.CargarImagenButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
            Me.CargarImagenButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
            Me.CargarImagenButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.replace_image
            Me.CargarImagenButton.Location = New System.Drawing.Point(498, 91)
            Me.CargarImagenButton.Name = "CargarImagenButton"
            Me.CargarImagenButton.Size = New System.Drawing.Size(34, 34)
            Me.CargarImagenButton.TabIndex = 9
            Me.CargarImagenButton.UseVisualStyleBackColor = False
            '
            'lblConfirmarDocumento
            '
            Me.lblConfirmarDocumento.AutoSize = True
            Me.lblConfirmarDocumento.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblConfirmarDocumento.Location = New System.Drawing.Point(6, 102)
            Me.lblConfirmarDocumento.Name = "lblConfirmarDocumento"
            Me.lblConfirmarDocumento.Size = New System.Drawing.Size(128, 13)
            Me.lblConfirmarDocumento.TabIndex = 3
            Me.lblConfirmarDocumento.Text = "Confirmar Documento"
            '
            'lblDocumento
            '
            Me.lblDocumento.AutoSize = True
            Me.lblDocumento.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblDocumento.Location = New System.Drawing.Point(6, 73)
            Me.lblDocumento.Name = "lblDocumento"
            Me.lblDocumento.Size = New System.Drawing.Size(71, 13)
            Me.lblDocumento.TabIndex = 2
            Me.lblDocumento.Text = "Documento"
            '
            'gbSolicitud
            '
            Me.gbSolicitud.Controls.Add(Me.CentralOffLineViewer)
            Me.gbSolicitud.Location = New System.Drawing.Point(12, 167)
            Me.gbSolicitud.Name = "gbSolicitud"
            Me.gbSolicitud.Size = New System.Drawing.Size(778, 383)
            Me.gbSolicitud.TabIndex = 3
            Me.gbSolicitud.TabStop = False
            '
            'CentralOffLineViewer
            '
            Me.CentralOffLineViewer.Dock = System.Windows.Forms.DockStyle.Fill
            Me.CentralOffLineViewer.ImagePath = New List(Of String)
            Me.CentralOffLineViewer.Location = New System.Drawing.Point(3, 16)
            Me.CentralOffLineViewer.Name = "CentralOffLineViewer"
            Me.CentralOffLineViewer.SelectedPage = 1
            Me.CentralOffLineViewer.Size = New System.Drawing.Size(772, 364)
            Me.CentralOffLineViewer.TabIndex = 22
            Me.CentralOffLineViewer.Zoom = CType(100, Short)
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.Location = New System.Drawing.Point(6, 45)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(24, 13)
            Me.Label1.TabIndex = 14
            Me.Label1.Text = "OT"
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label2.Location = New System.Drawing.Point(6, 16)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(109, 13)
            Me.Label2.TabIndex = 13
            Me.Label2.Text = "Fecha de proceso"
            '
            'cmbFechaProceso
            '
            Me.cmbFechaProceso.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.cmbFechaProceso.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.cmbFechaProceso.DisabledEnter = False
            Me.cmbFechaProceso.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cmbFechaProceso.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.cmbFechaProceso.FormattingEnabled = True
            Me.cmbFechaProceso.Location = New System.Drawing.Point(147, 13)
            Me.cmbFechaProceso.Name = "cmbFechaProceso"
            Me.cmbFechaProceso.Size = New System.Drawing.Size(314, 21)
            Me.cmbFechaProceso.TabIndex = 11
            '
            'cmbOT
            '
            Me.cmbOT.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.cmbOT.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.cmbOT.DisabledEnter = False
            Me.cmbOT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cmbOT.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.cmbOT.FormattingEnabled = True
            Me.cmbOT.Location = New System.Drawing.Point(147, 42)
            Me.cmbOT.Name = "cmbOT"
            Me.cmbOT.Size = New System.Drawing.Size(314, 21)
            Me.cmbOT.TabIndex = 12
            '
            'FormAdicionarDocumento
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(802, 563)
            Me.Controls.Add(Me.gbSolicitud)
            Me.Controls.Add(Me.gbEncabezado)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "FormAdicionarDocumento"
            Me.Text = "Adicionar Documento"
            Me.gbEncabezado.ResumeLayout(False)
            Me.gbEncabezado.PerformLayout()
            Me.gbSolicitud.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents cmbTipologia As DesktopComboBoxControl
        Friend WithEvents cmbConfirmarTipologia As DesktopComboBoxControl
        Friend WithEvents gbEncabezado As System.Windows.Forms.GroupBox
        Friend WithEvents lblConfirmarDocumento As System.Windows.Forms.Label
        Friend WithEvents lblDocumento As System.Windows.Forms.Label
        Friend WithEvents gbSolicitud As System.Windows.Forms.GroupBox
        Friend WithEvents CargarImagenButton As System.Windows.Forms.Button
        Friend WithEvents AdicionarImagenButton As System.Windows.Forms.Button
        Friend WithEvents CentralOffLineViewer As ImageViewer
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents cmbFechaProceso As DesktopComboBoxControl
        Friend WithEvents cmbOT As DesktopComboBoxControl
    End Class
End Namespace