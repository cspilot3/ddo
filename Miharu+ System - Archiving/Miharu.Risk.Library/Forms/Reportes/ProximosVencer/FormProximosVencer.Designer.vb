<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormProximosVencer
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
        Me.Label6 = New System.Windows.Forms.Label()
        Me.UsuarioSolicitanteProximoVencer = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.BuscarButton = New System.Windows.Forms.Button()
        Me.EsquemaProximoVencer = New System.Windows.Forms.ComboBox()
        Me.ProyectoProximoVencer = New System.Windows.Forms.ComboBox()
        Me.EntidadProximoVencer = New System.Windows.Forms.ComboBox()
        Me.ResultadosPanel = New System.Windows.Forms.Panel()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.UsuarioSolicitanteProximoVencer)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.BuscarButton)
        Me.GroupBox1.Controls.Add(Me.EsquemaProximoVencer)
        Me.GroupBox1.Controls.Add(Me.ProyectoProximoVencer)
        Me.GroupBox1.Controls.Add(Me.EntidadProximoVencer)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(782, 102)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Filtros"
        '
        'Label6
        '
        Me.Label6.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(475, 26)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(129, 15)
        Me.Label6.TabIndex = 27
        Me.Label6.Text = "Usuario Solicitante"
        '
        'UsuarioSolicitanteProximoVencer
        '
        Me.UsuarioSolicitanteProximoVencer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.UsuarioSolicitanteProximoVencer.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.UsuarioSolicitanteProximoVencer.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsuarioSolicitanteProximoVencer.FormattingEnabled = True
        Me.UsuarioSolicitanteProximoVencer.Location = New System.Drawing.Point(478, 50)
        Me.UsuarioSolicitanteProximoVencer.Name = "UsuarioSolicitanteProximoVencer"
        Me.UsuarioSolicitanteProximoVencer.Size = New System.Drawing.Size(191, 21)
        Me.UsuarioSolicitanteProximoVencer.TabIndex = 26
        '
        'Label3
        '
        Me.Label3.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(318, 26)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(67, 15)
        Me.Label3.TabIndex = 24
        Me.Label3.Text = "Esquema"
        '
        'Label2
        '
        Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(171, 26)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(62, 15)
        Me.Label2.TabIndex = 23
        Me.Label2.Text = "Proyecto"
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(18, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(56, 15)
        Me.Label1.TabIndex = 22
        Me.Label1.Text = "Entidad"
        '
        'BuscarButton
        '
        Me.BuscarButton.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.BuscarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.BuscarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnBuscar2
        Me.BuscarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BuscarButton.Location = New System.Drawing.Point(691, 44)
        Me.BuscarButton.Name = "BuscarButton"
        Me.BuscarButton.Size = New System.Drawing.Size(85, 30)
        Me.BuscarButton.TabIndex = 21
        Me.BuscarButton.Text = "Buscar"
        Me.BuscarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BuscarButton.UseVisualStyleBackColor = True
        '
        'EsquemaProximoVencer
        '
        Me.EsquemaProximoVencer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.EsquemaProximoVencer.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.EsquemaProximoVencer.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EsquemaProximoVencer.FormattingEnabled = True
        Me.EsquemaProximoVencer.Location = New System.Drawing.Point(321, 50)
        Me.EsquemaProximoVencer.Name = "EsquemaProximoVencer"
        Me.EsquemaProximoVencer.Size = New System.Drawing.Size(151, 21)
        Me.EsquemaProximoVencer.TabIndex = 19
        '
        'ProyectoProximoVencer
        '
        Me.ProyectoProximoVencer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ProyectoProximoVencer.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ProyectoProximoVencer.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ProyectoProximoVencer.FormattingEnabled = True
        Me.ProyectoProximoVencer.Location = New System.Drawing.Point(174, 50)
        Me.ProyectoProximoVencer.Name = "ProyectoProximoVencer"
        Me.ProyectoProximoVencer.Size = New System.Drawing.Size(141, 21)
        Me.ProyectoProximoVencer.TabIndex = 18
        '
        'EntidadProximoVencer
        '
        Me.EntidadProximoVencer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.EntidadProximoVencer.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.EntidadProximoVencer.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EntidadProximoVencer.FormattingEnabled = True
        Me.EntidadProximoVencer.Location = New System.Drawing.Point(21, 50)
        Me.EntidadProximoVencer.Name = "EntidadProximoVencer"
        Me.EntidadProximoVencer.Size = New System.Drawing.Size(147, 21)
        Me.EntidadProximoVencer.TabIndex = 17
        '
        'ResultadosPanel
        '
        Me.ResultadosPanel.Location = New System.Drawing.Point(12, 120)
        Me.ResultadosPanel.Name = "ResultadosPanel"
        Me.ResultadosPanel.Size = New System.Drawing.Size(782, 189)
        Me.ResultadosPanel.TabIndex = 2
        '
        'FormProximosVencer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(818, 322)
        Me.Controls.Add(Me.ResultadosPanel)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "FormProximosVencer"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Proximos a vencer"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents ResultadosPanel As System.Windows.Forms.Panel
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents UsuarioSolicitanteProximoVencer As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents BuscarButton As System.Windows.Forms.Button
    Friend WithEvents EsquemaProximoVencer As System.Windows.Forms.ComboBox
    Friend WithEvents ProyectoProximoVencer As System.Windows.Forms.ComboBox
    Friend WithEvents EntidadProximoVencer As System.Windows.Forms.ComboBox
End Class
