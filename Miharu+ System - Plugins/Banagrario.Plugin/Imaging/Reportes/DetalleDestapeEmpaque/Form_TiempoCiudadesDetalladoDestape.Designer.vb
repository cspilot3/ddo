Namespace Imaging.Reportes.DetalleDestapeEmpaque
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormTiempoCiudadesDetalladoDestape
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
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.ContenedorComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.UsuarioComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.ReporteComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.btnCancelar = New System.Windows.Forms.Button()
            Me.btnAceptar = New System.Windows.Forms.Button()
            Me.GroupBox1.SuspendLayout()
            Me.SuspendLayout()
            '
            'GroupBox1
            '
            Me.GroupBox1.Controls.Add(Me.ContenedorComboBox)
            Me.GroupBox1.Controls.Add(Me.UsuarioComboBox)
            Me.GroupBox1.Controls.Add(Me.ReporteComboBox)
            Me.GroupBox1.Controls.Add(Me.Label3)
            Me.GroupBox1.Controls.Add(Me.Label2)
            Me.GroupBox1.Controls.Add(Me.Label1)
            Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(180, 176)
            Me.GroupBox1.TabIndex = 0
            Me.GroupBox1.TabStop = False
            '
            'ContenedorComboBox
            '
            Me.ContenedorComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.ContenedorComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.ContenedorComboBox.DisabledEnter = False
            Me.ContenedorComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.ContenedorComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.ContenedorComboBox.FormattingEnabled = True
            Me.ContenedorComboBox.Location = New System.Drawing.Point(9, 140)
            Me.ContenedorComboBox.Name = "ContenedorComboBox"
            Me.ContenedorComboBox.Size = New System.Drawing.Size(158, 21)
            Me.ContenedorComboBox.TabIndex = 8
            '
            'UsuarioComboBox
            '
            Me.UsuarioComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.UsuarioComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.UsuarioComboBox.DisabledEnter = False
            Me.UsuarioComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.UsuarioComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.UsuarioComboBox.FormattingEnabled = True
            Me.UsuarioComboBox.Location = New System.Drawing.Point(9, 83)
            Me.UsuarioComboBox.Name = "UsuarioComboBox"
            Me.UsuarioComboBox.Size = New System.Drawing.Size(158, 21)
            Me.UsuarioComboBox.TabIndex = 7
            '
            'ReporteComboBox
            '
            Me.ReporteComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.ReporteComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.ReporteComboBox.DisabledEnter = False
            Me.ReporteComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.ReporteComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.ReporteComboBox.FormattingEnabled = True
            Me.ReporteComboBox.Items.AddRange(New Object() {"Destape", "Empaque"})
            Me.ReporteComboBox.Location = New System.Drawing.Point(10, 32)
            Me.ReporteComboBox.Name = "ReporteComboBox"
            Me.ReporteComboBox.Size = New System.Drawing.Size(158, 21)
            Me.ReporteComboBox.TabIndex = 6
            '
            'Label3
            '
            Me.Label3.AutoSize = True
            Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label3.Location = New System.Drawing.Point(6, 124)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(72, 13)
            Me.Label3.TabIndex = 2
            Me.Label3.Text = "Contenedor"
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label2.Location = New System.Drawing.Point(6, 67)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(50, 13)
            Me.Label2.TabIndex = 1
            Me.Label2.Text = "Usuario"
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.Location = New System.Drawing.Point(6, 16)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(52, 13)
            Me.Label1.TabIndex = 0
            Me.Label1.Text = "Reporte"
            '
            'btnCancelar
            '
            Me.btnCancelar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnCancelar.Location = New System.Drawing.Point(105, 194)
            Me.btnCancelar.Name = "btnCancelar"
            Me.btnCancelar.Size = New System.Drawing.Size(75, 23)
            Me.btnCancelar.TabIndex = 10
            Me.btnCancelar.Text = "&Cancelar"
            Me.btnCancelar.UseVisualStyleBackColor = True
            '
            'btnAceptar
            '
            Me.btnAceptar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnAceptar.Location = New System.Drawing.Point(21, 194)
            Me.btnAceptar.Name = "btnAceptar"
            Me.btnAceptar.Size = New System.Drawing.Size(75, 23)
            Me.btnAceptar.TabIndex = 9
            Me.btnAceptar.Text = "&Aceptar"
            Me.btnAceptar.UseVisualStyleBackColor = True
            '
            'Form_TiempoCiudadesDetalladoDestape
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(203, 228)
            Me.Controls.Add(Me.btnCancelar)
            Me.Controls.Add(Me.GroupBox1)
            Me.Controls.Add(Me.btnAceptar)
            Me.Name = "FormTiempoCiudadesDetalladoDestape"
            Me.Text = "Filtro"
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents ContenedorComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents UsuarioComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents ReporteComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents btnCancelar As System.Windows.Forms.Button
        Friend WithEvents btnAceptar As System.Windows.Forms.Button
    End Class
End Namespace

