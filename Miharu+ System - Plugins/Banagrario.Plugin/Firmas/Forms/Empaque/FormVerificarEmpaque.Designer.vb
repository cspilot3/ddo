Namespace Firmas.Forms.Empaque
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormVerificarEmpaque
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormVerificarEmpaque))
            Me.CancelarButton = New System.Windows.Forms.Button()
            Me.FechaProcesoPicker = New System.Windows.Forms.DateTimePicker()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.TxtCodBar = New System.Windows.Forms.TextBox()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.LbMensaje = New System.Windows.Forms.Label()
            Me.LbCodBar = New System.Windows.Forms.Label()
            Me.Panel1 = New System.Windows.Forms.Panel()
            Me.Panel2 = New System.Windows.Forms.Panel()
            Me.Panel1.SuspendLayout()
            Me.Panel2.SuspendLayout()
            Me.SuspendLayout()
            '
            'CancelarButton
            '
            Me.CancelarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CancelarButton.Image = CType(resources.GetObject("CancelarButton.Image"), System.Drawing.Image)
            Me.CancelarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CancelarButton.Location = New System.Drawing.Point(417, 266)
            Me.CancelarButton.Name = "CancelarButton"
            Me.CancelarButton.Size = New System.Drawing.Size(80, 23)
            Me.CancelarButton.TabIndex = 4
            Me.CancelarButton.Text = "&Cerrar"
            Me.CancelarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'FechaProcesoPicker
            '
            Me.FechaProcesoPicker.Location = New System.Drawing.Point(16, 27)
            Me.FechaProcesoPicker.Name = "FechaProcesoPicker"
            Me.FechaProcesoPicker.Size = New System.Drawing.Size(214, 20)
            Me.FechaProcesoPicker.TabIndex = 35
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.Location = New System.Drawing.Point(15, 9)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(110, 13)
            Me.Label1.TabIndex = 38
            Me.Label1.Text = "Fecha de Proceso"
            '
            'TxtCodBar
            '
            Me.TxtCodBar.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.TxtCodBar.Location = New System.Drawing.Point(16, 72)
            Me.TxtCodBar.Name = "TxtCodBar"
            Me.TxtCodBar.Size = New System.Drawing.Size(449, 26)
            Me.TxtCodBar.TabIndex = 39
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label2.Location = New System.Drawing.Point(15, 54)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(103, 13)
            Me.Label2.TabIndex = 40
            Me.Label2.Text = "Código de barras"
            '
            'LbMensaje
            '
            Me.LbMensaje.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.LbMensaje.ForeColor = System.Drawing.Color.Black
            Me.LbMensaje.Location = New System.Drawing.Point(13, 37)
            Me.LbMensaje.Name = "LbMensaje"
            Me.LbMensaje.Size = New System.Drawing.Size(456, 81)
            Me.LbMensaje.TabIndex = 41
            Me.LbMensaje.Text = "Mensaje"
            Me.LbMensaje.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'LbCodBar
            '
            Me.LbCodBar.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.LbCodBar.Location = New System.Drawing.Point(13, 14)
            Me.LbCodBar.Name = "LbCodBar"
            Me.LbCodBar.Size = New System.Drawing.Size(456, 23)
            Me.LbCodBar.TabIndex = 42
            Me.LbCodBar.Text = "Codigo de Barras"
            Me.LbCodBar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'Panel1
            '
            Me.Panel1.BackColor = System.Drawing.SystemColors.ActiveCaption
            Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Panel1.Controls.Add(Me.LbCodBar)
            Me.Panel1.Controls.Add(Me.LbMensaje)
            Me.Panel1.Location = New System.Drawing.Point(12, 126)
            Me.Panel1.Name = "Panel1"
            Me.Panel1.Size = New System.Drawing.Size(482, 134)
            Me.Panel1.TabIndex = 43
            '
            'Panel2
            '
            Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Panel2.Controls.Add(Me.Label1)
            Me.Panel2.Controls.Add(Me.FechaProcesoPicker)
            Me.Panel2.Controls.Add(Me.Label2)
            Me.Panel2.Controls.Add(Me.TxtCodBar)
            Me.Panel2.Location = New System.Drawing.Point(12, 12)
            Me.Panel2.Name = "Panel2"
            Me.Panel2.Size = New System.Drawing.Size(482, 108)
            Me.Panel2.TabIndex = 44
            '
            'FormVerificarEmpaque
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.CancelarButton
            Me.ClientSize = New System.Drawing.Size(509, 301)
            Me.Controls.Add(Me.Panel2)
            Me.Controls.Add(Me.Panel1)
            Me.Controls.Add(Me.CancelarButton)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormVerificarEmpaque"
            Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Verificar tipo de empaque"
            Me.Panel1.ResumeLayout(False)
            Me.Panel2.ResumeLayout(False)
            Me.Panel2.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents CancelarButton As System.Windows.Forms.Button
        Friend WithEvents FechaProcesoPicker As System.Windows.Forms.DateTimePicker
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents TxtCodBar As System.Windows.Forms.TextBox
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents LbMensaje As System.Windows.Forms.Label
        Friend WithEvents LbCodBar As System.Windows.Forms.Label
        Friend WithEvents Panel1 As System.Windows.Forms.Panel
        Friend WithEvents Panel2 As System.Windows.Forms.Panel
    End Class
End Namespace