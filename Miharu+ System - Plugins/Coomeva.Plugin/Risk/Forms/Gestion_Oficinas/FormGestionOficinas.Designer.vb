<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormGestionOficinas
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormGestionOficinas))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.BtnSalir = New System.Windows.Forms.Button()
        Me.BtnEnvioCorreo = New System.Windows.Forms.Button()
        Me.FechaRecolecciondateTimePicker = New System.Windows.Forms.DateTimePicker()
        Me.FechaRecoleccionlabel = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.BtnSalir)
        Me.GroupBox1.Controls.Add(Me.BtnEnvioCorreo)
        Me.GroupBox1.Controls.Add(Me.FechaRecolecciondateTimePicker)
        Me.GroupBox1.Controls.Add(Me.FechaRecoleccionlabel)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 21)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(311, 160)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Filtro Cierre Fecha Recoleccion"
        '
        'BtnSalir
        '
        Me.BtnSalir.AccessibleDescription = ""
        Me.BtnSalir.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.BtnSalir.BackColor = System.Drawing.SystemColors.Control
        Me.BtnSalir.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSalir.Image = Global.Coomeva.Plugin.My.Resources.Resources.btnSalir
        Me.BtnSalir.Location = New System.Drawing.Point(169, 110)
        Me.BtnSalir.Name = "BtnSalir"
        Me.BtnSalir.Size = New System.Drawing.Size(100, 33)
        Me.BtnSalir.TabIndex = 27
        Me.BtnSalir.Tag = "Ctrl + S"
        Me.BtnSalir.Text = "&Salir"
        Me.BtnSalir.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.BtnSalir.UseVisualStyleBackColor = True
        '
        'BtnEnvioCorreo
        '
        Me.BtnEnvioCorreo.AccessibleDescription = ""
        Me.BtnEnvioCorreo.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.BtnEnvioCorreo.BackColor = System.Drawing.SystemColors.Control
        Me.BtnEnvioCorreo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnEnvioCorreo.Image = Global.Coomeva.Plugin.My.Resources.Resources.envio_correo
        Me.BtnEnvioCorreo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnEnvioCorreo.Location = New System.Drawing.Point(28, 110)
        Me.BtnEnvioCorreo.Name = "BtnEnvioCorreo"
        Me.BtnEnvioCorreo.Size = New System.Drawing.Size(108, 33)
        Me.BtnEnvioCorreo.TabIndex = 26
        Me.BtnEnvioCorreo.Tag = "Ctrl + C"
        Me.BtnEnvioCorreo.Text = "&Envio Correo"
        Me.BtnEnvioCorreo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnEnvioCorreo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.BtnEnvioCorreo.UseVisualStyleBackColor = True
        '
        'FechaRecolecciondateTimePicker
        '
        Me.FechaRecolecciondateTimePicker.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.FechaRecolecciondateTimePicker.CustomFormat = "yyyy/MM/dd"
        Me.FechaRecolecciondateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.FechaRecolecciondateTimePicker.Location = New System.Drawing.Point(186, 27)
        Me.FechaRecolecciondateTimePicker.Name = "FechaRecolecciondateTimePicker"
        Me.FechaRecolecciondateTimePicker.Size = New System.Drawing.Size(83, 20)
        Me.FechaRecolecciondateTimePicker.TabIndex = 25
        '
        'FechaRecoleccionlabel
        '
        Me.FechaRecoleccionlabel.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.FechaRecoleccionlabel.AutoSize = True
        Me.FechaRecoleccionlabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FechaRecoleccionlabel.Location = New System.Drawing.Point(25, 33)
        Me.FechaRecoleccionlabel.Name = "FechaRecoleccionlabel"
        Me.FechaRecoleccionlabel.Size = New System.Drawing.Size(121, 13)
        Me.FechaRecoleccionlabel.TabIndex = 24
        Me.FechaRecoleccionlabel.Text = "Fecha Recoleccion:"
        '
        'FormGestionOficinas
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(335, 189)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "FormGestionOficinas"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Envio Correo Gestión Oficinas"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Private WithEvents FechaRecolecciondateTimePicker As System.Windows.Forms.DateTimePicker
    Friend WithEvents FechaRecoleccionlabel As System.Windows.Forms.Label
    Friend WithEvents BtnEnvioCorreo As System.Windows.Forms.Button
    Friend WithEvents BtnSalir As System.Windows.Forms.Button
End Class
