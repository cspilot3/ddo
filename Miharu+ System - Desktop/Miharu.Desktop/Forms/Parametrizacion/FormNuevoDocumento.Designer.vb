Namespace Forms.Parametrizacion
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormNuevoDocumento
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormNuevoDocumento))
            Me.Label1 = New System.Windows.Forms.Label()
            Me.EntidadTextBox = New System.Windows.Forms.TextBox()
            Me.ProyectoTextBox = New System.Windows.Forms.TextBox()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.EsquemaTextBox = New System.Windows.Forms.TextBox()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.IdDocumentoTextBox = New System.Windows.Forms.TextBox()
            Me.Label4 = New System.Windows.Forms.Label()
            Me.NombreDocumentoTextBox = New System.Windows.Forms.TextBox()
            Me.Label5 = New System.Windows.Forms.Label()
            Me.Label6 = New System.Windows.Forms.Label()
            Me.Label7 = New System.Windows.Forms.Label()
            Me.CancelarButton = New System.Windows.Forms.Button()
            Me.AceptarButton = New System.Windows.Forms.Button()
            Me.TipologiaComboBox = New System.Windows.Forms.ComboBox()
            Me.DocumentoGrupoComboBox = New System.Windows.Forms.ComboBox()
            Me.SuspendLayout()
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.Location = New System.Drawing.Point(12, 15)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(50, 13)
            Me.Label1.TabIndex = 24
            Me.Label1.Text = "Entidad"
            '
            'EntidadTextBox
            '
            Me.EntidadTextBox.Enabled = False
            Me.EntidadTextBox.Location = New System.Drawing.Point(136, 12)
            Me.EntidadTextBox.Name = "EntidadTextBox"
            Me.EntidadTextBox.ReadOnly = True
            Me.EntidadTextBox.Size = New System.Drawing.Size(283, 20)
            Me.EntidadTextBox.TabIndex = 25
            '
            'ProyectoTextBox
            '
            Me.ProyectoTextBox.Enabled = False
            Me.ProyectoTextBox.Location = New System.Drawing.Point(136, 38)
            Me.ProyectoTextBox.Name = "ProyectoTextBox"
            Me.ProyectoTextBox.ReadOnly = True
            Me.ProyectoTextBox.Size = New System.Drawing.Size(283, 20)
            Me.ProyectoTextBox.TabIndex = 27
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label2.Location = New System.Drawing.Point(12, 41)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(57, 13)
            Me.Label2.TabIndex = 26
            Me.Label2.Text = "Proyecto"
            '
            'EsquemaTextBox
            '
            Me.EsquemaTextBox.Enabled = False
            Me.EsquemaTextBox.Location = New System.Drawing.Point(136, 64)
            Me.EsquemaTextBox.Name = "EsquemaTextBox"
            Me.EsquemaTextBox.ReadOnly = True
            Me.EsquemaTextBox.Size = New System.Drawing.Size(283, 20)
            Me.EsquemaTextBox.TabIndex = 29
            '
            'Label3
            '
            Me.Label3.AutoSize = True
            Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label3.Location = New System.Drawing.Point(12, 67)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(58, 13)
            Me.Label3.TabIndex = 28
            Me.Label3.Text = "Esquema"
            '
            'IdDocumentoTextBox
            '
            Me.IdDocumentoTextBox.Enabled = False
            Me.IdDocumentoTextBox.Location = New System.Drawing.Point(136, 90)
            Me.IdDocumentoTextBox.Name = "IdDocumentoTextBox"
            Me.IdDocumentoTextBox.ReadOnly = True
            Me.IdDocumentoTextBox.Size = New System.Drawing.Size(283, 20)
            Me.IdDocumentoTextBox.TabIndex = 31
            '
            'Label4
            '
            Me.Label4.AutoSize = True
            Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label4.Location = New System.Drawing.Point(12, 93)
            Me.Label4.Name = "Label4"
            Me.Label4.Size = New System.Drawing.Size(86, 13)
            Me.Label4.TabIndex = 30
            Me.Label4.Text = "Id Documento"
            '
            'NombreDocumentoTextBox
            '
            Me.NombreDocumentoTextBox.Location = New System.Drawing.Point(136, 116)
            Me.NombreDocumentoTextBox.MaxLength = 150
            Me.NombreDocumentoTextBox.Name = "NombreDocumentoTextBox"
            Me.NombreDocumentoTextBox.Size = New System.Drawing.Size(283, 20)
            Me.NombreDocumentoTextBox.TabIndex = 33
            '
            'Label5
            '
            Me.Label5.AutoSize = True
            Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label5.Location = New System.Drawing.Point(12, 119)
            Me.Label5.Name = "Label5"
            Me.Label5.Size = New System.Drawing.Size(118, 13)
            Me.Label5.TabIndex = 32
            Me.Label5.Text = "Nombre Documento"
            '
            'Label6
            '
            Me.Label6.AutoSize = True
            Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label6.Location = New System.Drawing.Point(12, 145)
            Me.Label6.Name = "Label6"
            Me.Label6.Size = New System.Drawing.Size(61, 13)
            Me.Label6.TabIndex = 34
            Me.Label6.Text = "Tipología"
            '
            'Label7
            '
            Me.Label7.AutoSize = True
            Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label7.Location = New System.Drawing.Point(12, 171)
            Me.Label7.Name = "Label7"
            Me.Label7.Size = New System.Drawing.Size(109, 13)
            Me.Label7.TabIndex = 36
            Me.Label7.Text = "Documento Grupo"
            '
            'CancelarButton
            '
            Me.CancelarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CancelarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CancelarButton.Image = CType(resources.GetObject("CancelarButton.Image"), System.Drawing.Image)
            Me.CancelarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CancelarButton.Location = New System.Drawing.Point(339, 196)
            Me.CancelarButton.Name = "CancelarButton"
            Me.CancelarButton.Size = New System.Drawing.Size(80, 24)
            Me.CancelarButton.TabIndex = 37
            Me.CancelarButton.Text = "&Cancelar"
            Me.CancelarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'AceptarButton
            '
            Me.AceptarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.AceptarButton.Image = CType(resources.GetObject("AceptarButton.Image"), System.Drawing.Image)
            Me.AceptarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.AceptarButton.Location = New System.Drawing.Point(253, 196)
            Me.AceptarButton.Name = "AceptarButton"
            Me.AceptarButton.Size = New System.Drawing.Size(80, 23)
            Me.AceptarButton.TabIndex = 36
            Me.AceptarButton.Text = "&Aceptar"
            Me.AceptarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'TipologiaComboBox
            '
            Me.TipologiaComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.TipologiaComboBox.FormattingEnabled = True
            Me.TipologiaComboBox.Location = New System.Drawing.Point(136, 142)
            Me.TipologiaComboBox.Name = "TipologiaComboBox"
            Me.TipologiaComboBox.Size = New System.Drawing.Size(283, 21)
            Me.TipologiaComboBox.TabIndex = 34
            '
            'DocumentoGrupoComboBox
            '
            Me.DocumentoGrupoComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.DocumentoGrupoComboBox.FormattingEnabled = True
            Me.DocumentoGrupoComboBox.Location = New System.Drawing.Point(136, 169)
            Me.DocumentoGrupoComboBox.Name = "DocumentoGrupoComboBox"
            Me.DocumentoGrupoComboBox.Size = New System.Drawing.Size(283, 21)
            Me.DocumentoGrupoComboBox.TabIndex = 35
            '
            'FormNuevoDocumento
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(431, 232)
            Me.Controls.Add(Me.DocumentoGrupoComboBox)
            Me.Controls.Add(Me.TipologiaComboBox)
            Me.Controls.Add(Me.CancelarButton)
            Me.Controls.Add(Me.AceptarButton)
            Me.Controls.Add(Me.Label7)
            Me.Controls.Add(Me.Label6)
            Me.Controls.Add(Me.NombreDocumentoTextBox)
            Me.Controls.Add(Me.Label5)
            Me.Controls.Add(Me.IdDocumentoTextBox)
            Me.Controls.Add(Me.Label4)
            Me.Controls.Add(Me.EsquemaTextBox)
            Me.Controls.Add(Me.Label3)
            Me.Controls.Add(Me.ProyectoTextBox)
            Me.Controls.Add(Me.Label2)
            Me.Controls.Add(Me.EntidadTextBox)
            Me.Controls.Add(Me.Label1)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormNuevoDocumento"
            Me.ShowIcon = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Nuevo Documento"
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents EntidadTextBox As System.Windows.Forms.TextBox
        Friend WithEvents ProyectoTextBox As System.Windows.Forms.TextBox
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents EsquemaTextBox As System.Windows.Forms.TextBox
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents IdDocumentoTextBox As System.Windows.Forms.TextBox
        Friend WithEvents Label4 As System.Windows.Forms.Label
        Friend WithEvents NombreDocumentoTextBox As System.Windows.Forms.TextBox
        Friend WithEvents Label5 As System.Windows.Forms.Label
        Friend WithEvents Label6 As System.Windows.Forms.Label
        Friend WithEvents Label7 As System.Windows.Forms.Label
        Friend WithEvents CancelarButton As System.Windows.Forms.Button
        Friend WithEvents AceptarButton As System.Windows.Forms.Button
        Friend WithEvents TipologiaComboBox As System.Windows.Forms.ComboBox
        Friend WithEvents DocumentoGrupoComboBox As System.Windows.Forms.ComboBox
    End Class
End Namespace