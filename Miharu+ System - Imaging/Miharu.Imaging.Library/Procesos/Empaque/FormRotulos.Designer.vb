<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormRotulos
    Inherits Miharu.Desktop.Library.FormBase

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
        Me.CheckBoxImprimirHC = New System.Windows.Forms.CheckBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.CheckBoxGuardarFuid = New System.Windows.Forms.CheckBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.CheckBoxGuardarHC = New System.Windows.Forms.CheckBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.ImprimirFuidCheckBox = New System.Windows.Forms.CheckBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.GuardarRotulosCheckBox = New System.Windows.Forms.CheckBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ImprimirRotulosCheckBox = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.BuscarButton = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.ImprimirStickerCheckBox = New System.Windows.Forms.CheckBox()
        Me.GuardarStickerCheckBox = New System.Windows.Forms.CheckBox()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.GuardarStickerCheckBox)
        Me.GroupBox1.Controls.Add(Me.ImprimirStickerCheckBox)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.CheckBoxImprimirHC)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.CheckBoxGuardarFuid)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.CheckBoxGuardarHC)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.ImprimirFuidCheckBox)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.GuardarRotulosCheckBox)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.ImprimirRotulosCheckBox)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.BuscarButton)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(5, 21)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(467, 130)
        Me.GroupBox1.TabIndex = 15
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Seleccione:"
        '
        'CheckBoxImprimirHC
        '
        Me.CheckBoxImprimirHC.AutoSize = True
        Me.CheckBoxImprimirHC.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBoxImprimirHC.Location = New System.Drawing.Point(328, 22)
        Me.CheckBoxImprimirHC.Name = "CheckBoxImprimirHC"
        Me.CheckBoxImprimirHC.Size = New System.Drawing.Size(15, 14)
        Me.CheckBoxImprimirHC.TabIndex = 87
        Me.CheckBoxImprimirHC.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(206, 23)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(118, 13)
        Me.Label6.TabIndex = 86
        Me.Label6.Text = "Imprimir Hoja de Control"
        '
        'CheckBoxGuardarFuid
        '
        Me.CheckBoxGuardarFuid.AutoSize = True
        Me.CheckBoxGuardarFuid.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBoxGuardarFuid.Location = New System.Drawing.Point(187, 61)
        Me.CheckBoxGuardarFuid.Name = "CheckBoxGuardarFuid"
        Me.CheckBoxGuardarFuid.Size = New System.Drawing.Size(15, 14)
        Me.CheckBoxGuardarFuid.TabIndex = 85
        Me.CheckBoxGuardarFuid.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(116, 61)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(68, 13)
        Me.Label5.TabIndex = 84
        Me.Label5.Text = "Guardar Fuid"
        '
        'CheckBoxGuardarHC
        '
        Me.CheckBoxGuardarHC.AutoSize = True
        Me.CheckBoxGuardarHC.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBoxGuardarHC.Location = New System.Drawing.Point(329, 61)
        Me.CheckBoxGuardarHC.Name = "CheckBoxGuardarHC"
        Me.CheckBoxGuardarHC.Size = New System.Drawing.Size(15, 14)
        Me.CheckBoxGuardarHC.TabIndex = 83
        Me.CheckBoxGuardarHC.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(205, 61)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(121, 13)
        Me.Label4.TabIndex = 82
        Me.Label4.Text = "Guardar Hoja de Control"
        '
        'ImprimirFuidCheckBox
        '
        Me.ImprimirFuidCheckBox.AutoSize = True
        Me.ImprimirFuidCheckBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ImprimirFuidCheckBox.Location = New System.Drawing.Point(187, 20)
        Me.ImprimirFuidCheckBox.Name = "ImprimirFuidCheckBox"
        Me.ImprimirFuidCheckBox.Size = New System.Drawing.Size(15, 14)
        Me.ImprimirFuidCheckBox.TabIndex = 81
        Me.ImprimirFuidCheckBox.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(119, 21)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(65, 13)
        Me.Label3.TabIndex = 80
        Me.Label3.Text = "Imprimir Fuid"
        '
        'GuardarRotulosCheckBox
        '
        Me.GuardarRotulosCheckBox.AutoSize = True
        Me.GuardarRotulosCheckBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GuardarRotulosCheckBox.Location = New System.Drawing.Point(91, 61)
        Me.GuardarRotulosCheckBox.Name = "GuardarRotulosCheckBox"
        Me.GuardarRotulosCheckBox.Size = New System.Drawing.Size(15, 14)
        Me.GuardarRotulosCheckBox.TabIndex = 79
        Me.GuardarRotulosCheckBox.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(9, 61)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(79, 13)
        Me.Label2.TabIndex = 78
        Me.Label2.Text = "Guardar rótulos"
        '
        'ImprimirRotulosCheckBox
        '
        Me.ImprimirRotulosCheckBox.AutoSize = True
        Me.ImprimirRotulosCheckBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ImprimirRotulosCheckBox.Location = New System.Drawing.Point(91, 22)
        Me.ImprimirRotulosCheckBox.Name = "ImprimirRotulosCheckBox"
        Me.ImprimirRotulosCheckBox.Size = New System.Drawing.Size(15, 14)
        Me.ImprimirRotulosCheckBox.TabIndex = 77
        Me.ImprimirRotulosCheckBox.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(13, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(76, 13)
        Me.Label1.TabIndex = 76
        Me.Label1.Text = "Imprimir rótulos"
        '
        'BuscarButton
        '
        Me.BuscarButton.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.BuscarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BuscarButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.replace_image
        Me.BuscarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BuscarButton.Location = New System.Drawing.Point(162, 94)
        Me.BuscarButton.Name = "BuscarButton"
        Me.BuscarButton.Size = New System.Drawing.Size(141, 30)
        Me.BuscarButton.TabIndex = 75
        Me.BuscarButton.Text = "&Generar"
        Me.BuscarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BuscarButton.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(357, 23)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(78, 13)
        Me.Label7.TabIndex = 88
        Me.Label7.Text = "Imprimir Sticker"
        '
        'Label8
        '
        Me.Label8.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(354, 61)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(81, 13)
        Me.Label8.TabIndex = 89
        Me.Label8.Text = "Guardar Sticker"
        '
        'ImprimirStickerCheckBox
        '
        Me.ImprimirStickerCheckBox.AutoSize = True
        Me.ImprimirStickerCheckBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ImprimirStickerCheckBox.Location = New System.Drawing.Point(438, 23)
        Me.ImprimirStickerCheckBox.Name = "ImprimirStickerCheckBox"
        Me.ImprimirStickerCheckBox.Size = New System.Drawing.Size(15, 14)
        Me.ImprimirStickerCheckBox.TabIndex = 90
        Me.ImprimirStickerCheckBox.UseVisualStyleBackColor = True
        '
        'GuardarStickerCheckBox
        '
        Me.GuardarStickerCheckBox.AutoSize = True
        Me.GuardarStickerCheckBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GuardarStickerCheckBox.Location = New System.Drawing.Point(438, 60)
        Me.GuardarStickerCheckBox.Name = "GuardarStickerCheckBox"
        Me.GuardarStickerCheckBox.Size = New System.Drawing.Size(15, 14)
        Me.GuardarStickerCheckBox.TabIndex = 91
        Me.GuardarStickerCheckBox.UseVisualStyleBackColor = True
        '
        'FormRotulos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(484, 163)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "FormRotulos"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "FormRótulos"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents CheckBoxImprimirHC As System.Windows.Forms.CheckBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents CheckBoxGuardarFuid As System.Windows.Forms.CheckBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents CheckBoxGuardarHC As System.Windows.Forms.CheckBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ImprimirFuidCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents GuardarRotulosCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ImprimirRotulosCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents BuscarButton As System.Windows.Forms.Button
    Friend WithEvents ImprimirStickerCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents GuardarStickerCheckBox As System.Windows.Forms.CheckBox
End Class
