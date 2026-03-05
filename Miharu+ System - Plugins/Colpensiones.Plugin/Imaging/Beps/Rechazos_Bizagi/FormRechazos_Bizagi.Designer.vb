Namespace Imaging.Beps.Rechazo_Bizagi
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormRechazos_Bizagi
        Inherits System.Windows.Forms.Form

        'Form reemplaza a Dispose para limpiar la lista de componentes.
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
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.txtNo_Formulario = New System.Windows.Forms.TextBox()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.GroupBoxRechazosBizagi = New System.Windows.Forms.GroupBox()
            Me.CheckedListBoxRechazos = New System.Windows.Forms.CheckedListBox()
            Me.Btn_Cerrar = New System.Windows.Forms.Button()
            Me.RechazarButton = New System.Windows.Forms.Button()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.FechaProcesoDateTimePicker = New System.Windows.Forms.DateTimePicker()
            Me.OTDesktopComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.lblOT = New System.Windows.Forms.Label()
            Me.GroupBox1.SuspendLayout()
            Me.GroupBoxRechazosBizagi.SuspendLayout()
            Me.SuspendLayout()
            '
            'GroupBox1
            '
            Me.GroupBox1.Controls.Add(Me.OTDesktopComboBox)
            Me.GroupBox1.Controls.Add(Me.lblOT)
            Me.GroupBox1.Controls.Add(Me.txtNo_Formulario)
            Me.GroupBox1.Controls.Add(Me.Label2)
            Me.GroupBox1.Controls.Add(Me.GroupBoxRechazosBizagi)
            Me.GroupBox1.Controls.Add(Me.Btn_Cerrar)
            Me.GroupBox1.Controls.Add(Me.RechazarButton)
            Me.GroupBox1.Controls.Add(Me.Label1)
            Me.GroupBox1.Controls.Add(Me.FechaProcesoDateTimePicker)
            Me.GroupBox1.Location = New System.Drawing.Point(12, 23)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(452, 406)
            Me.GroupBox1.TabIndex = 1
            Me.GroupBox1.TabStop = False
            Me.GroupBox1.Text = "Filtros de Rechazo Bizagi"
            '
            'txtNo_Formulario
            '
            Me.txtNo_Formulario.Location = New System.Drawing.Point(127, 86)
            Me.txtNo_Formulario.Name = "txtNo_Formulario"
            Me.txtNo_Formulario.Size = New System.Drawing.Size(200, 20)
            Me.txtNo_Formulario.TabIndex = 3
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label2.Location = New System.Drawing.Point(16, 86)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(93, 13)
            Me.Label2.TabIndex = 16
            Me.Label2.Text = "No. Formulario:"
            '
            'GroupBoxRechazosBizagi
            '
            Me.GroupBoxRechazosBizagi.Controls.Add(Me.CheckedListBoxRechazos)
            Me.GroupBoxRechazosBizagi.Location = New System.Drawing.Point(19, 122)
            Me.GroupBoxRechazosBizagi.Name = "GroupBoxRechazosBizagi"
            Me.GroupBoxRechazosBizagi.Size = New System.Drawing.Size(405, 216)
            Me.GroupBoxRechazosBizagi.TabIndex = 15
            Me.GroupBoxRechazosBizagi.TabStop = False
            Me.GroupBoxRechazosBizagi.Text = "Causales Rechazo Bizagi"
            '
            'CheckedListBoxRechazos
            '
            Me.CheckedListBoxRechazos.FormattingEnabled = True
            Me.CheckedListBoxRechazos.Location = New System.Drawing.Point(7, 19)
            Me.CheckedListBoxRechazos.Name = "CheckedListBoxRechazos"
            Me.CheckedListBoxRechazos.Size = New System.Drawing.Size(377, 184)
            Me.CheckedListBoxRechazos.TabIndex = 4
            '
            'Btn_Cerrar
            '
            Me.Btn_Cerrar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Btn_Cerrar.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.Btn_Cerrar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Btn_Cerrar.Image = Global.Colpensiones.Plugin.My.Resources.Resources.btnSalir
            Me.Btn_Cerrar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.Btn_Cerrar.Location = New System.Drawing.Point(312, 344)
            Me.Btn_Cerrar.Name = "Btn_Cerrar"
            Me.Btn_Cerrar.Size = New System.Drawing.Size(91, 33)
            Me.Btn_Cerrar.TabIndex = 6
            Me.Btn_Cerrar.Text = "Cerrar"
            Me.Btn_Cerrar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.Btn_Cerrar.UseVisualStyleBackColor = True
            '
            'RechazarButton
            '
            Me.RechazarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.RechazarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.RechazarButton.Image = Global.Colpensiones.Plugin.My.Resources.Resources.Process_Accept
            Me.RechazarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.RechazarButton.Location = New System.Drawing.Point(141, 344)
            Me.RechazarButton.Name = "RechazarButton"
            Me.RechazarButton.Size = New System.Drawing.Size(102, 33)
            Me.RechazarButton.TabIndex = 5
            Me.RechazarButton.Text = "Rechazar"
            Me.RechazarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.RechazarButton.UseVisualStyleBackColor = True
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.Location = New System.Drawing.Point(16, 29)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(96, 13)
            Me.Label1.TabIndex = 9
            Me.Label1.Text = "Fecha Proceso:"
            '
            'FechaProcesoDateTimePicker
            '
            Me.FechaProcesoDateTimePicker.Location = New System.Drawing.Point(127, 23)
            Me.FechaProcesoDateTimePicker.Name = "FechaProcesoDateTimePicker"
            Me.FechaProcesoDateTimePicker.Size = New System.Drawing.Size(200, 20)
            Me.FechaProcesoDateTimePicker.TabIndex = 1
            '
            'OTDesktopComboBox
            '
            Me.OTDesktopComboBox.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.OTDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.OTDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.OTDesktopComboBox.DisabledEnter = False
            Me.OTDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.OTDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.OTDesktopComboBox.FormattingEnabled = True
            Me.OTDesktopComboBox.Location = New System.Drawing.Point(127, 56)
            Me.OTDesktopComboBox.Name = "OTDesktopComboBox"
            Me.OTDesktopComboBox.Size = New System.Drawing.Size(200, 21)
            Me.OTDesktopComboBox.TabIndex = 2
            '
            'lblOT
            '
            Me.lblOT.AutoSize = True
            Me.lblOT.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblOT.Location = New System.Drawing.Point(16, 56)
            Me.lblOT.Name = "lblOT"
            Me.lblOT.Size = New System.Drawing.Size(24, 13)
            Me.lblOT.TabIndex = 29
            Me.lblOT.Text = "OT"
            '
            'FormRechazos_Bizagi
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(476, 441)
            Me.ControlBox = False
            Me.Controls.Add(Me.GroupBox1)
            Me.Name = "FormRechazos_Bizagi"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Rechazos Bizagi"
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            Me.GroupBoxRechazosBizagi.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents txtNo_Formulario As System.Windows.Forms.TextBox
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents GroupBoxRechazosBizagi As System.Windows.Forms.GroupBox
        Friend WithEvents Btn_Cerrar As System.Windows.Forms.Button
        Friend WithEvents RechazarButton As System.Windows.Forms.Button
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents FechaProcesoDateTimePicker As System.Windows.Forms.DateTimePicker
        Friend WithEvents CheckedListBoxRechazos As System.Windows.Forms.CheckedListBox
        Friend WithEvents OTDesktopComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents lblOT As System.Windows.Forms.Label
    End Class
End Namespace