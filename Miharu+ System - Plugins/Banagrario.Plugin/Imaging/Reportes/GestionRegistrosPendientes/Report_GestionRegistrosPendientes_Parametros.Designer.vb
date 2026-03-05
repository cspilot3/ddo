Imports Miharu.Desktop.Controls.DesktopComboBox

Namespace Imaging.Reportes.GestionRegistrosPendientes
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class Report_GestionRegistrosPendientes_Parametros
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
            Me.OficinaDesktopComboBox = New DesktopComboBoxControl()
            Me.OficinaLabel = New System.Windows.Forms.Label()
            Me.COBDesktopComboBox = New DesktopComboBoxControl()
            Me.COBLabel = New System.Windows.Forms.Label()
            Me.RegionalDesktopComboBox = New DesktopComboBoxControl()
            Me.RegionalLabel = New System.Windows.Forms.Label()
            Me.lblFechaInicial = New System.Windows.Forms.Label()
            Me.dtpFechaInicial = New System.Windows.Forms.DateTimePicker()
            Me.btnCancelar = New System.Windows.Forms.Button()
            Me.btnAceptar = New System.Windows.Forms.Button()
            Me.gbxBase.SuspendLayout()
            Me.SuspendLayout()
            '
            'gbxBase
            '
            Me.gbxBase.Controls.Add(Me.OficinaDesktopComboBox)
            Me.gbxBase.Controls.Add(Me.OficinaLabel)
            Me.gbxBase.Controls.Add(Me.COBDesktopComboBox)
            Me.gbxBase.Controls.Add(Me.COBLabel)
            Me.gbxBase.Controls.Add(Me.RegionalDesktopComboBox)
            Me.gbxBase.Controls.Add(Me.RegionalLabel)
            Me.gbxBase.Controls.Add(Me.lblFechaInicial)
            Me.gbxBase.Controls.Add(Me.dtpFechaInicial)
            Me.gbxBase.Location = New System.Drawing.Point(12, 12)
            Me.gbxBase.Name = "gbxBase"
            Me.gbxBase.Size = New System.Drawing.Size(276, 246)
            Me.gbxBase.TabIndex = 17
            Me.gbxBase.TabStop = False
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
            Me.OficinaDesktopComboBox.Location = New System.Drawing.Point(21, 144)
            Me.OficinaDesktopComboBox.Name = "OficinaDesktopComboBox"
            Me.OficinaDesktopComboBox.Size = New System.Drawing.Size(235, 21)
            Me.OficinaDesktopComboBox.TabIndex = 35
            '
            'OficinaLabel
            '
            Me.OficinaLabel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                                             Or System.Windows.Forms.AnchorStyles.Left) _
                                            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.OficinaLabel.AutoSize = True
            Me.OficinaLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.OficinaLabel.Location = New System.Drawing.Point(21, 122)
            Me.OficinaLabel.Name = "OficinaLabel"
            Me.OficinaLabel.Size = New System.Drawing.Size(51, 13)
            Me.OficinaLabel.TabIndex = 34
            Me.OficinaLabel.Text = "Oficina:"
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
            Me.COBDesktopComboBox.Location = New System.Drawing.Point(21, 88)
            Me.COBDesktopComboBox.Name = "COBDesktopComboBox"
            Me.COBDesktopComboBox.Size = New System.Drawing.Size(235, 21)
            Me.COBDesktopComboBox.TabIndex = 33
            '
            'COBLabel
            '
            Me.COBLabel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                                         Or System.Windows.Forms.AnchorStyles.Left) _
                                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.COBLabel.AutoSize = True
            Me.COBLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.COBLabel.Location = New System.Drawing.Point(18, 66)
            Me.COBLabel.Name = "COBLabel"
            Me.COBLabel.Size = New System.Drawing.Size(36, 13)
            Me.COBLabel.TabIndex = 32
            Me.COBLabel.Text = "COB:"
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
            Me.RegionalDesktopComboBox.Location = New System.Drawing.Point(21, 32)
            Me.RegionalDesktopComboBox.Name = "RegionalDesktopComboBox"
            Me.RegionalDesktopComboBox.Size = New System.Drawing.Size(235, 21)
            Me.RegionalDesktopComboBox.TabIndex = 31
            '
            'RegionalLabel
            '
            Me.RegionalLabel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                                              Or System.Windows.Forms.AnchorStyles.Left) _
                                             Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.RegionalLabel.AutoSize = True
            Me.RegionalLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.RegionalLabel.Location = New System.Drawing.Point(18, 16)
            Me.RegionalLabel.Name = "RegionalLabel"
            Me.RegionalLabel.Size = New System.Drawing.Size(61, 13)
            Me.RegionalLabel.TabIndex = 30
            Me.RegionalLabel.Text = "Regional:"
            '
            'lblFechaInicial
            '
            Me.lblFechaInicial.AutoSize = True
            Me.lblFechaInicial.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblFechaInicial.Location = New System.Drawing.Point(21, 182)
            Me.lblFechaInicial.Name = "lblFechaInicial"
            Me.lblFechaInicial.Size = New System.Drawing.Size(89, 15)
            Me.lblFechaInicial.TabIndex = 1
            Me.lblFechaInicial.Text = "Fecha Inicial"
            '
            'dtpFechaInicial
            '
            Me.dtpFechaInicial.Location = New System.Drawing.Point(24, 201)
            Me.dtpFechaInicial.Name = "dtpFechaInicial"
            Me.dtpFechaInicial.Size = New System.Drawing.Size(232, 20)
            Me.dtpFechaInicial.TabIndex = 0
            '
            'btnCancelar
            '
            Me.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.btnCancelar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnCancelar.Location = New System.Drawing.Point(208, 264)
            Me.btnCancelar.Name = "btnCancelar"
            Me.btnCancelar.Size = New System.Drawing.Size(80, 24)
            Me.btnCancelar.TabIndex = 16
            Me.btnCancelar.Text = "&Cancelar"
            Me.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.btnCancelar.UseVisualStyleBackColor = True
            '
            'btnAceptar
            '
            Me.btnAceptar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnAceptar.Location = New System.Drawing.Point(109, 264)
            Me.btnAceptar.Name = "btnAceptar"
            Me.btnAceptar.Size = New System.Drawing.Size(80, 24)
            Me.btnAceptar.TabIndex = 15
            Me.btnAceptar.Text = "&Aceptar"
            Me.btnAceptar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.btnAceptar.UseVisualStyleBackColor = True
            '
            'Report_GestionRegistrosPendientes_Parametros
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(309, 298)
            Me.ControlBox = False
            Me.Controls.Add(Me.gbxBase)
            Me.Controls.Add(Me.btnCancelar)
            Me.Controls.Add(Me.btnAceptar)
            Me.Name = "Report_GestionRegistrosPendientes_Parametros"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Filtrar por ..."
            Me.gbxBase.ResumeLayout(False)
            Me.gbxBase.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents gbxBase As System.Windows.Forms.GroupBox
        Friend WithEvents lblFechaInicial As System.Windows.Forms.Label
        Friend WithEvents dtpFechaInicial As System.Windows.Forms.DateTimePicker
        Friend WithEvents btnCancelar As System.Windows.Forms.Button
        Friend WithEvents btnAceptar As System.Windows.Forms.Button
        Friend WithEvents OficinaDesktopComboBox As DesktopComboBoxControl
        Friend WithEvents OficinaLabel As System.Windows.Forms.Label
        Friend WithEvents COBDesktopComboBox As DesktopComboBoxControl
        Friend WithEvents COBLabel As System.Windows.Forms.Label
        Friend WithEvents RegionalDesktopComboBox As DesktopComboBoxControl
        Friend WithEvents RegionalLabel As System.Windows.Forms.Label
    End Class
End Namespace