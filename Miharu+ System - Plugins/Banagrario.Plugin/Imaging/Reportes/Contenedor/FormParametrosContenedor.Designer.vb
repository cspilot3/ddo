Imports Miharu.Desktop.Controls.DesktopComboBox

Namespace Imaging.Reportes.Contenedor

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormParametrosContenedor
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
            Me.gbxBase = New System.Windows.Forms.GroupBox()
            Me.COBDesktopComboBox = New DesktopComboBoxControl()
            Me.COBLabel = New System.Windows.Forms.Label()
            Me.OficinaDesktopComboBox = New DesktopComboBoxControl()
            Me.OficinaLabel = New System.Windows.Forms.Label()
            Me.RegionalDesktopComboBox = New DesktopComboBoxControl()
            Me.RegionalLabel = New System.Windows.Forms.Label()
            Me.dtpFechaMovimiento = New System.Windows.Forms.DateTimePicker()
            Me.lblFechaMovimiento = New System.Windows.Forms.Label()
            Me.dtpFechaProceso = New System.Windows.Forms.DateTimePicker()
            Me.lblFechaProceso = New System.Windows.Forms.Label()
            Me.btnCancelar = New System.Windows.Forms.Button()
            Me.btnAceptar = New System.Windows.Forms.Button()
            Me.CkbxFechaProceso = New System.Windows.Forms.CheckBox()
            Me.CkbxFechaMovimiento = New System.Windows.Forms.CheckBox()
            Me.gbxBase.SuspendLayout()
            Me.SuspendLayout()
            '
            'gbxBase
            '
            Me.gbxBase.Controls.Add(Me.CkbxFechaMovimiento)
            Me.gbxBase.Controls.Add(Me.CkbxFechaProceso)
            Me.gbxBase.Controls.Add(Me.COBDesktopComboBox)
            Me.gbxBase.Controls.Add(Me.COBLabel)
            Me.gbxBase.Controls.Add(Me.OficinaDesktopComboBox)
            Me.gbxBase.Controls.Add(Me.OficinaLabel)
            Me.gbxBase.Controls.Add(Me.RegionalDesktopComboBox)
            Me.gbxBase.Controls.Add(Me.RegionalLabel)
            Me.gbxBase.Controls.Add(Me.dtpFechaMovimiento)
            Me.gbxBase.Controls.Add(Me.lblFechaMovimiento)
            Me.gbxBase.Controls.Add(Me.dtpFechaProceso)
            Me.gbxBase.Controls.Add(Me.lblFechaProceso)
            Me.gbxBase.Location = New System.Drawing.Point(12, 12)
            Me.gbxBase.Name = "gbxBase"
            Me.gbxBase.Size = New System.Drawing.Size(260, 278)
            Me.gbxBase.TabIndex = 1
            Me.gbxBase.TabStop = False
            '
            'COBDesktopComboBox
            '
            Me.COBDesktopComboBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                                                   Or System.Windows.Forms.AnchorStyles.Left) _
                                                  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.COBDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.COBDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.COBDesktopComboBox.DisabledEnter = False
            Me.COBDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.COBDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.COBDesktopComboBox.FormattingEnabled = True
            Me.COBDesktopComboBox.Location = New System.Drawing.Point(19, 186)
            Me.COBDesktopComboBox.Name = "COBDesktopComboBox"
            Me.COBDesktopComboBox.Size = New System.Drawing.Size(235, 21)
            Me.COBDesktopComboBox.TabIndex = 41
            '
            'COBLabel
            '
            Me.COBLabel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                                         Or System.Windows.Forms.AnchorStyles.Left) _
                                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.COBLabel.AutoSize = True
            Me.COBLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.COBLabel.Location = New System.Drawing.Point(16, 167)
            Me.COBLabel.Name = "COBLabel"
            Me.COBLabel.Size = New System.Drawing.Size(36, 13)
            Me.COBLabel.TabIndex = 40
            Me.COBLabel.Text = "COB:"
            '
            'OficinaDesktopComboBox
            '
            Me.OficinaDesktopComboBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                                                       Or System.Windows.Forms.AnchorStyles.Left) _
                                                      Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.OficinaDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.OficinaDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.OficinaDesktopComboBox.DisabledEnter = False
            Me.OficinaDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.OficinaDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.OficinaDesktopComboBox.FormattingEnabled = True
            Me.OficinaDesktopComboBox.Location = New System.Drawing.Point(15, 244)
            Me.OficinaDesktopComboBox.Name = "OficinaDesktopComboBox"
            Me.OficinaDesktopComboBox.Size = New System.Drawing.Size(235, 21)
            Me.OficinaDesktopComboBox.TabIndex = 39
            '
            'OficinaLabel
            '
            Me.OficinaLabel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                                             Or System.Windows.Forms.AnchorStyles.Left) _
                                            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.OficinaLabel.AutoSize = True
            Me.OficinaLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.OficinaLabel.Location = New System.Drawing.Point(15, 222)
            Me.OficinaLabel.Name = "OficinaLabel"
            Me.OficinaLabel.Size = New System.Drawing.Size(51, 13)
            Me.OficinaLabel.TabIndex = 38
            Me.OficinaLabel.Text = "Oficina:"
            '
            'RegionalDesktopComboBox
            '
            Me.RegionalDesktopComboBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                                                        Or System.Windows.Forms.AnchorStyles.Left) _
                                                       Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.RegionalDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.RegionalDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.RegionalDesktopComboBox.DisabledEnter = False
            Me.RegionalDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.RegionalDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.RegionalDesktopComboBox.FormattingEnabled = True
            Me.RegionalDesktopComboBox.Location = New System.Drawing.Point(19, 139)
            Me.RegionalDesktopComboBox.Name = "RegionalDesktopComboBox"
            Me.RegionalDesktopComboBox.Size = New System.Drawing.Size(235, 21)
            Me.RegionalDesktopComboBox.TabIndex = 37
            '
            'RegionalLabel
            '
            Me.RegionalLabel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                                              Or System.Windows.Forms.AnchorStyles.Left) _
                                             Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.RegionalLabel.AutoSize = True
            Me.RegionalLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.RegionalLabel.Location = New System.Drawing.Point(16, 121)
            Me.RegionalLabel.Name = "RegionalLabel"
            Me.RegionalLabel.Size = New System.Drawing.Size(61, 13)
            Me.RegionalLabel.TabIndex = 36
            Me.RegionalLabel.Text = "Regional:"
            '
            'dtpFechaMovimiento
            '
            Me.dtpFechaMovimiento.Location = New System.Drawing.Point(18, 88)
            Me.dtpFechaMovimiento.Name = "dtpFechaMovimiento"
            Me.dtpFechaMovimiento.Size = New System.Drawing.Size(233, 20)
            Me.dtpFechaMovimiento.TabIndex = 3
            '
            'lblFechaMovimiento
            '
            Me.lblFechaMovimiento.AutoSize = True
            Me.lblFechaMovimiento.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblFechaMovimiento.Location = New System.Drawing.Point(40, 72)
            Me.lblFechaMovimiento.Name = "lblFechaMovimiento"
            Me.lblFechaMovimiento.Size = New System.Drawing.Size(118, 13)
            Me.lblFechaMovimiento.TabIndex = 2
            Me.lblFechaMovimiento.Text = "Fecha Movimiento: "
            '
            'dtpFechaProceso
            '
            Me.dtpFechaProceso.Location = New System.Drawing.Point(18, 36)
            Me.dtpFechaProceso.Name = "dtpFechaProceso"
            Me.dtpFechaProceso.Size = New System.Drawing.Size(233, 20)
            Me.dtpFechaProceso.TabIndex = 1
            '
            'lblFechaProceso
            '
            Me.lblFechaProceso.AutoSize = True
            Me.lblFechaProceso.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblFechaProceso.Location = New System.Drawing.Point(40, 17)
            Me.lblFechaProceso.Name = "lblFechaProceso"
            Me.lblFechaProceso.Size = New System.Drawing.Size(100, 13)
            Me.lblFechaProceso.TabIndex = 0
            Me.lblFechaProceso.Text = "Fecha Proceso: "
            '
            'btnCancelar
            '
            Me.btnCancelar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnCancelar.Location = New System.Drawing.Point(168, 296)
            Me.btnCancelar.Name = "btnCancelar"
            Me.btnCancelar.Size = New System.Drawing.Size(75, 23)
            Me.btnCancelar.TabIndex = 4
            Me.btnCancelar.Text = "&Cancelar"
            Me.btnCancelar.UseVisualStyleBackColor = True
            '
            'btnAceptar
            '
            Me.btnAceptar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnAceptar.Location = New System.Drawing.Point(27, 296)
            Me.btnAceptar.Name = "btnAceptar"
            Me.btnAceptar.Size = New System.Drawing.Size(75, 23)
            Me.btnAceptar.TabIndex = 3
            Me.btnAceptar.Text = "&Aceptar"
            Me.btnAceptar.UseVisualStyleBackColor = True
            '
            'CkbxFechaProceso
            '
            Me.CkbxFechaProceso.AutoSize = True
            Me.CkbxFechaProceso.Checked = True
            Me.CkbxFechaProceso.CheckState = System.Windows.Forms.CheckState.Checked
            Me.CkbxFechaProceso.Location = New System.Drawing.Point(19, 17)
            Me.CkbxFechaProceso.Name = "CkbxFechaProceso"
            Me.CkbxFechaProceso.Size = New System.Drawing.Size(15, 14)
            Me.CkbxFechaProceso.TabIndex = 42
            Me.CkbxFechaProceso.UseVisualStyleBackColor = True
            '
            'CkbxFechaMovimiento
            '
            Me.CkbxFechaMovimiento.AutoSize = True
            Me.CkbxFechaMovimiento.Location = New System.Drawing.Point(19, 72)
            Me.CkbxFechaMovimiento.Name = "CkbxFechaMovimiento"
            Me.CkbxFechaMovimiento.Size = New System.Drawing.Size(15, 14)
            Me.CkbxFechaMovimiento.TabIndex = 43
            Me.CkbxFechaMovimiento.UseVisualStyleBackColor = True
            '
            'FormParametrosContenedor
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(284, 329)
            Me.Controls.Add(Me.btnCancelar)
            Me.Controls.Add(Me.btnAceptar)
            Me.Controls.Add(Me.gbxBase)
            Me.Name = "FormParametrosContenedor"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Filtrar por..."
            Me.gbxBase.ResumeLayout(False)
            Me.gbxBase.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents gbxBase As System.Windows.Forms.GroupBox
        Friend WithEvents dtpFechaProceso As System.Windows.Forms.DateTimePicker
        Friend WithEvents lblFechaProceso As System.Windows.Forms.Label
        Friend WithEvents btnCancelar As System.Windows.Forms.Button
        Friend WithEvents btnAceptar As System.Windows.Forms.Button
        Friend WithEvents dtpFechaMovimiento As System.Windows.Forms.DateTimePicker
        Friend WithEvents lblFechaMovimiento As System.Windows.Forms.Label
        Friend WithEvents OficinaDesktopComboBox As DesktopComboBoxControl
        Friend WithEvents OficinaLabel As System.Windows.Forms.Label
        Friend WithEvents RegionalDesktopComboBox As DesktopComboBoxControl
        Friend WithEvents RegionalLabel As System.Windows.Forms.Label
        Friend WithEvents COBDesktopComboBox As DesktopComboBoxControl
        Friend WithEvents COBLabel As System.Windows.Forms.Label
        Friend WithEvents CkbxFechaMovimiento As System.Windows.Forms.CheckBox
        Friend WithEvents CkbxFechaProceso As System.Windows.Forms.CheckBox
    End Class
End Namespace