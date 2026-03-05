Imports Miharu.Desktop.Controls.DesktopComboBox

Namespace Procesos.Cargue
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormReasignarCargue
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormReasignarCargue))
            Me.GroupBoxProcesamiento = New System.Windows.Forms.GroupBox()
            Me.AsignarCargueButton = New System.Windows.Forms.Button()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.CentroProcesamientoDesktopComboBox = New DesktopComboBoxControl()
            Me.SedeProcesamientoDesktopComboBox = New DesktopComboBoxControl()
            Me.CentroDeProcesamientoLabel = New System.Windows.Forms.Label()
            Me.LabelSedeProcesamiento = New System.Windows.Forms.Label()
            Me.GroupBoxProcesamiento.SuspendLayout()
            Me.SuspendLayout()
            '
            'GroupBoxProcesamiento
            '
            Me.GroupBoxProcesamiento.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                                                      Or System.Windows.Forms.AnchorStyles.Left) _
                                                     Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GroupBoxProcesamiento.Controls.Add(Me.AsignarCargueButton)
            Me.GroupBoxProcesamiento.Controls.Add(Me.CerrarButton)
            Me.GroupBoxProcesamiento.Controls.Add(Me.CentroProcesamientoDesktopComboBox)
            Me.GroupBoxProcesamiento.Controls.Add(Me.SedeProcesamientoDesktopComboBox)
            Me.GroupBoxProcesamiento.Controls.Add(Me.CentroDeProcesamientoLabel)
            Me.GroupBoxProcesamiento.Controls.Add(Me.LabelSedeProcesamiento)
            Me.GroupBoxProcesamiento.Location = New System.Drawing.Point(12, 12)
            Me.GroupBoxProcesamiento.Name = "GroupBoxProcesamiento"
            Me.GroupBoxProcesamiento.Size = New System.Drawing.Size(426, 149)
            Me.GroupBoxProcesamiento.TabIndex = 0
            Me.GroupBoxProcesamiento.TabStop = False
            Me.GroupBoxProcesamiento.Text = "Asignar a..."
            '
            'AsignarCargueButton
            '
            Me.AsignarCargueButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.AsignarCargueButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.refresh
            Me.AsignarCargueButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.AsignarCargueButton.Location = New System.Drawing.Point(219, 110)
            Me.AsignarCargueButton.Name = "AsignarCargueButton"
            Me.AsignarCargueButton.Size = New System.Drawing.Size(89, 33)
            Me.AsignarCargueButton.TabIndex = 29
            Me.AsignarCargueButton.Text = "Asignar"
            Me.AsignarCargueButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.AsignarCargueButton.UseVisualStyleBackColor = True
            '
            'CerrarButton
            '
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CerrarButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.cancelar
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(314, 110)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(88, 33)
            Me.CerrarButton.TabIndex = 28
            Me.CerrarButton.Text = "Cerrar"
            Me.CerrarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'CentroProcesamientoDesktopComboBox
            '
            Me.CentroProcesamientoDesktopComboBox.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.CentroProcesamientoDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.CentroProcesamientoDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.CentroProcesamientoDesktopComboBox.DisabledEnter = False
            Me.CentroProcesamientoDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.CentroProcesamientoDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.CentroProcesamientoDesktopComboBox.FormattingEnabled = True
            Me.CentroProcesamientoDesktopComboBox.Location = New System.Drawing.Point(186, 70)
            Me.CentroProcesamientoDesktopComboBox.Name = "CentroProcesamientoDesktopComboBox"
            Me.CentroProcesamientoDesktopComboBox.Size = New System.Drawing.Size(216, 21)
            Me.CentroProcesamientoDesktopComboBox.TabIndex = 27
            '
            'SedeProcesamientoDesktopComboBox
            '
            Me.SedeProcesamientoDesktopComboBox.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.SedeProcesamientoDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.SedeProcesamientoDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.SedeProcesamientoDesktopComboBox.DisabledEnter = False
            Me.SedeProcesamientoDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.SedeProcesamientoDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.SedeProcesamientoDesktopComboBox.FormattingEnabled = True
            Me.SedeProcesamientoDesktopComboBox.Location = New System.Drawing.Point(186, 33)
            Me.SedeProcesamientoDesktopComboBox.Name = "SedeProcesamientoDesktopComboBox"
            Me.SedeProcesamientoDesktopComboBox.Size = New System.Drawing.Size(216, 21)
            Me.SedeProcesamientoDesktopComboBox.TabIndex = 26
            '
            'CentroDeProcesamientoLabel
            '
            Me.CentroDeProcesamientoLabel.AutoSize = True
            Me.CentroDeProcesamientoLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CentroDeProcesamientoLabel.Location = New System.Drawing.Point(18, 68)
            Me.CentroDeProcesamientoLabel.Name = "CentroDeProcesamientoLabel"
            Me.CentroDeProcesamientoLabel.Size = New System.Drawing.Size(153, 13)
            Me.CentroDeProcesamientoLabel.TabIndex = 1
            Me.CentroDeProcesamientoLabel.Text = "Centro de Procesamiento:"
            '
            'LabelSedeProcesamiento
            '
            Me.LabelSedeProcesamiento.AutoSize = True
            Me.LabelSedeProcesamiento.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.LabelSedeProcesamiento.Location = New System.Drawing.Point(18, 31)
            Me.LabelSedeProcesamiento.Name = "LabelSedeProcesamiento"
            Me.LabelSedeProcesamiento.Size = New System.Drawing.Size(145, 13)
            Me.LabelSedeProcesamiento.TabIndex = 0
            Me.LabelSedeProcesamiento.Text = "Sede de Procesamiento:"
            '
            'FormReasignarCargue
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.CerrarButton
            Me.ClientSize = New System.Drawing.Size(451, 173)
            Me.Controls.Add(Me.GroupBoxProcesamiento)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormReasignarCargue"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Reasignación de Cargues"
            Me.GroupBoxProcesamiento.ResumeLayout(False)
            Me.GroupBoxProcesamiento.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents GroupBoxProcesamiento As System.Windows.Forms.GroupBox
        Friend WithEvents LabelSedeProcesamiento As System.Windows.Forms.Label
        Friend WithEvents CentroDeProcesamientoLabel As System.Windows.Forms.Label
        Friend WithEvents CentroProcesamientoDesktopComboBox As DesktopComboBoxControl
        Friend WithEvents SedeProcesamientoDesktopComboBox As DesktopComboBoxControl
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents AsignarCargueButton As System.Windows.Forms.Button
    End Class
End Namespace