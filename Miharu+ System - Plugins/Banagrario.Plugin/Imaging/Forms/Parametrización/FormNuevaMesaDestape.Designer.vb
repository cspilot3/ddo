Imports Miharu.Desktop.Controls.DesktopComboBox

Namespace Imaging.Forms.Parametrización

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormNuevaMesaDestape
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormNuevaMesaDestape))
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.CheckBoxActivo = New System.Windows.Forms.CheckBox()
            Me.lblActivo = New System.Windows.Forms.Label()
            Me.PCNameTextBox = New System.Windows.Forms.TextBox()
            Me.lblPCName = New System.Windows.Forms.Label()
            Me.SedeDesktopComboBox = New DesktopComboBoxControl()
            Me.lblSede = New System.Windows.Forms.Label()
            Me.CentroProcDesktopComboBox = New DesktopComboBoxControl()
            Me.lblCentroProcesamiento = New System.Windows.Forms.Label()
            Me.CancelarButton = New System.Windows.Forms.Button()
            Me.AceptarButton = New System.Windows.Forms.Button()
            Me.GroupBox1.SuspendLayout()
            Me.SuspendLayout()
            '
            'GroupBox1
            '
            Me.GroupBox1.Controls.Add(Me.CheckBoxActivo)
            Me.GroupBox1.Controls.Add(Me.lblActivo)
            Me.GroupBox1.Controls.Add(Me.PCNameTextBox)
            Me.GroupBox1.Controls.Add(Me.lblPCName)
            Me.GroupBox1.Controls.Add(Me.SedeDesktopComboBox)
            Me.GroupBox1.Controls.Add(Me.lblSede)
            Me.GroupBox1.Controls.Add(Me.CentroProcDesktopComboBox)
            Me.GroupBox1.Controls.Add(Me.lblCentroProcesamiento)
            Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(291, 202)
            Me.GroupBox1.TabIndex = 0
            Me.GroupBox1.TabStop = False
            '
            'CheckBoxActivo
            '
            Me.CheckBoxActivo.AutoSize = True
            Me.CheckBoxActivo.Checked = True
            Me.CheckBoxActivo.CheckState = System.Windows.Forms.CheckState.Checked
            Me.CheckBoxActivo.Location = New System.Drawing.Point(87, 165)
            Me.CheckBoxActivo.Name = "CheckBoxActivo"
            Me.CheckBoxActivo.Size = New System.Drawing.Size(15, 14)
            Me.CheckBoxActivo.TabIndex = 51
            Me.CheckBoxActivo.UseVisualStyleBackColor = True
            '
            'lblActivo
            '
            Me.lblActivo.AutoSize = True
            Me.lblActivo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblActivo.Location = New System.Drawing.Point(16, 165)
            Me.lblActivo.Name = "lblActivo"
            Me.lblActivo.Size = New System.Drawing.Size(43, 13)
            Me.lblActivo.TabIndex = 50
            Me.lblActivo.Text = "Activa"
            '
            'PCNameTextBox
            '
            Me.PCNameTextBox.Location = New System.Drawing.Point(19, 129)
            Me.PCNameTextBox.Name = "PCNameTextBox"
            Me.PCNameTextBox.Size = New System.Drawing.Size(235, 20)
            Me.PCNameTextBox.TabIndex = 49
            '
            'lblPCName
            '
            Me.lblPCName.AutoSize = True
            Me.lblPCName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblPCName.Location = New System.Drawing.Point(16, 113)
            Me.lblPCName.Name = "lblPCName"
            Me.lblPCName.Size = New System.Drawing.Size(63, 13)
            Me.lblPCName.TabIndex = 48
            Me.lblPCName.Text = "PC Name:"
            '
            'SedeDesktopComboBox
            '
            Me.SedeDesktopComboBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                                                    Or System.Windows.Forms.AnchorStyles.Left) _
                                                   Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.SedeDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.SedeDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.SedeDesktopComboBox.DisabledEnter = False
            Me.SedeDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.SedeDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.SedeDesktopComboBox.FormattingEnabled = True
            Me.SedeDesktopComboBox.Location = New System.Drawing.Point(19, 31)
            Me.SedeDesktopComboBox.Name = "SedeDesktopComboBox"
            Me.SedeDesktopComboBox.Size = New System.Drawing.Size(235, 21)
            Me.SedeDesktopComboBox.TabIndex = 47
            '
            'lblSede
            '
            Me.lblSede.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                                        Or System.Windows.Forms.AnchorStyles.Left) _
                                       Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lblSede.AutoSize = True
            Me.lblSede.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblSede.Location = New System.Drawing.Point(16, 15)
            Me.lblSede.Name = "lblSede"
            Me.lblSede.Size = New System.Drawing.Size(40, 13)
            Me.lblSede.TabIndex = 46
            Me.lblSede.Text = "Sede:"
            '
            'CentroProcDesktopComboBox
            '
            Me.CentroProcDesktopComboBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                                                          Or System.Windows.Forms.AnchorStyles.Left) _
                                                         Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CentroProcDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.CentroProcDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.CentroProcDesktopComboBox.DisabledEnter = False
            Me.CentroProcDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.CentroProcDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.CentroProcDesktopComboBox.FormattingEnabled = True
            Me.CentroProcDesktopComboBox.Location = New System.Drawing.Point(19, 82)
            Me.CentroProcDesktopComboBox.Name = "CentroProcDesktopComboBox"
            Me.CentroProcDesktopComboBox.Size = New System.Drawing.Size(235, 21)
            Me.CentroProcDesktopComboBox.TabIndex = 45
            '
            'lblCentroProcesamiento
            '
            Me.lblCentroProcesamiento.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                                                       Or System.Windows.Forms.AnchorStyles.Left) _
                                                      Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lblCentroProcesamiento.AutoSize = True
            Me.lblCentroProcesamiento.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblCentroProcesamiento.Location = New System.Drawing.Point(16, 63)
            Me.lblCentroProcesamiento.Name = "lblCentroProcesamiento"
            Me.lblCentroProcesamiento.Size = New System.Drawing.Size(135, 13)
            Me.lblCentroProcesamiento.TabIndex = 44
            Me.lblCentroProcesamiento.Text = "Centro Procesamiento:"
            '
            'CancelarButton
            '
            Me.CancelarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CancelarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CancelarButton.Image = CType(resources.GetObject("CancelarButton.Image"), System.Drawing.Image)
            Me.CancelarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CancelarButton.Location = New System.Drawing.Point(223, 237)
            Me.CancelarButton.Name = "CancelarButton"
            Me.CancelarButton.Size = New System.Drawing.Size(80, 24)
            Me.CancelarButton.TabIndex = 43
            Me.CancelarButton.Text = "&Cancelar"
            Me.CancelarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'AceptarButton
            '
            Me.AceptarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.AceptarButton.Image = CType(resources.GetObject("AceptarButton.Image"), System.Drawing.Image)
            Me.AceptarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.AceptarButton.Location = New System.Drawing.Point(99, 237)
            Me.AceptarButton.Name = "AceptarButton"
            Me.AceptarButton.Size = New System.Drawing.Size(80, 23)
            Me.AceptarButton.TabIndex = 42
            Me.AceptarButton.Text = "&Aceptar"
            Me.AceptarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'FormNuevaMesaDestape
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(318, 273)
            Me.Controls.Add(Me.CancelarButton)
            Me.Controls.Add(Me.AceptarButton)
            Me.Controls.Add(Me.GroupBox1)
            Me.Name = "FormNuevaMesaDestape"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Agregar Mesa Destape"
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents SedeDesktopComboBox As DesktopComboBoxControl
        Friend WithEvents lblSede As System.Windows.Forms.Label
        Friend WithEvents CentroProcDesktopComboBox As DesktopComboBoxControl
        Friend WithEvents lblCentroProcesamiento As System.Windows.Forms.Label
        Friend WithEvents CheckBoxActivo As System.Windows.Forms.CheckBox
        Friend WithEvents lblActivo As System.Windows.Forms.Label
        Friend WithEvents PCNameTextBox As System.Windows.Forms.TextBox
        Friend WithEvents lblPCName As System.Windows.Forms.Label
        Friend WithEvents CancelarButton As System.Windows.Forms.Button
        Friend WithEvents AceptarButton As System.Windows.Forms.Button
    End Class
End Namespace