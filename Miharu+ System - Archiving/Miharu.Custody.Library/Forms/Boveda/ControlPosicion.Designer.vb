Namespace Forms.Boveda
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class ControlPosicion
        Inherits System.Windows.Forms.UserControl

        'UserControl overrides dispose to clean up the component list.
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
            Me.L1 = New System.Windows.Forms.Label()
            Me.L2 = New System.Windows.Forms.Label()
            Me.L3 = New System.Windows.Forms.Label()
            Me.L4 = New System.Windows.Forms.Label()
            Me.PosicionLabel = New System.Windows.Forms.Label()
            Me.CajaLabel = New System.Windows.Forms.Label()
            Me.EntidadLabel = New System.Windows.Forms.Label()
            Me.ProyectoLabel = New System.Windows.Forms.Label()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.CarpetasLabel = New System.Windows.Forms.Label()
            Me.TipoLabel = New System.Windows.Forms.Label()
            Me.PropiedadesPanel = New System.Windows.Forms.Panel()
            Me.PropiedadesPanel.SuspendLayout()
            Me.SuspendLayout()
            '
            'L1
            '
            Me.L1.AutoSize = True
            Me.L1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.L1.Location = New System.Drawing.Point(4, 6)
            Me.L1.Name = "L1"
            Me.L1.Size = New System.Drawing.Size(55, 13)
            Me.L1.TabIndex = 0
            Me.L1.Text = "Posicion"
            '
            'L2
            '
            Me.L2.AutoSize = True
            Me.L2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.L2.Location = New System.Drawing.Point(27, 24)
            Me.L2.Name = "L2"
            Me.L2.Size = New System.Drawing.Size(32, 13)
            Me.L2.TabIndex = 1
            Me.L2.Text = "Caja"
            '
            'L3
            '
            Me.L3.AutoSize = True
            Me.L3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.L3.Location = New System.Drawing.Point(9, 42)
            Me.L3.Name = "L3"
            Me.L3.Size = New System.Drawing.Size(50, 13)
            Me.L3.TabIndex = 2
            Me.L3.Text = "Entidad"
            '
            'L4
            '
            Me.L4.AutoSize = True
            Me.L4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.L4.Location = New System.Drawing.Point(2, 61)
            Me.L4.Name = "L4"
            Me.L4.Size = New System.Drawing.Size(57, 13)
            Me.L4.TabIndex = 3
            Me.L4.Text = "Proyecto"
            '
            'PosicionLabel
            '
            Me.PosicionLabel.AutoSize = True
            Me.PosicionLabel.Location = New System.Drawing.Point(60, 6)
            Me.PosicionLabel.Name = "PosicionLabel"
            Me.PosicionLabel.Size = New System.Drawing.Size(13, 13)
            Me.PosicionLabel.TabIndex = 4
            Me.PosicionLabel.Text = "_"
            '
            'CajaLabel
            '
            Me.CajaLabel.AutoSize = True
            Me.CajaLabel.Location = New System.Drawing.Point(60, 24)
            Me.CajaLabel.Name = "CajaLabel"
            Me.CajaLabel.Size = New System.Drawing.Size(13, 13)
            Me.CajaLabel.TabIndex = 5
            Me.CajaLabel.Text = "_"
            '
            'EntidadLabel
            '
            Me.EntidadLabel.AutoSize = True
            Me.EntidadLabel.Location = New System.Drawing.Point(60, 42)
            Me.EntidadLabel.Name = "EntidadLabel"
            Me.EntidadLabel.Size = New System.Drawing.Size(13, 13)
            Me.EntidadLabel.TabIndex = 6
            Me.EntidadLabel.Text = "_"
            '
            'ProyectoLabel
            '
            Me.ProyectoLabel.AutoSize = True
            Me.ProyectoLabel.Location = New System.Drawing.Point(60, 61)
            Me.ProyectoLabel.Name = "ProyectoLabel"
            Me.ProyectoLabel.Size = New System.Drawing.Size(13, 13)
            Me.ProyectoLabel.TabIndex = 7
            Me.ProyectoLabel.Text = "_"
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.ForeColor = System.Drawing.Color.White
            Me.Label1.Location = New System.Drawing.Point(-1, 5)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(57, 13)
            Me.Label1.TabIndex = 8
            Me.Label1.Text = "Carpetas"
            '
            'CarpetasLabel
            '
            Me.CarpetasLabel.AutoSize = True
            Me.CarpetasLabel.ForeColor = System.Drawing.Color.White
            Me.CarpetasLabel.Location = New System.Drawing.Point(57, 5)
            Me.CarpetasLabel.Name = "CarpetasLabel"
            Me.CarpetasLabel.Size = New System.Drawing.Size(13, 13)
            Me.CarpetasLabel.TabIndex = 9
            Me.CarpetasLabel.Text = "_"
            '
            'TipoLabel
            '
            Me.TipoLabel.AutoSize = True
            Me.TipoLabel.ForeColor = System.Drawing.Color.Black
            Me.TipoLabel.Location = New System.Drawing.Point(90, 5)
            Me.TipoLabel.Name = "TipoLabel"
            Me.TipoLabel.Size = New System.Drawing.Size(13, 13)
            Me.TipoLabel.TabIndex = 10
            Me.TipoLabel.Text = "_"
            '
            'PropiedadesPanel
            '
            Me.PropiedadesPanel.BackColor = System.Drawing.SystemColors.Control
            Me.PropiedadesPanel.Controls.Add(Me.Label1)
            Me.PropiedadesPanel.Controls.Add(Me.TipoLabel)
            Me.PropiedadesPanel.Controls.Add(Me.CarpetasLabel)
            Me.PropiedadesPanel.Location = New System.Drawing.Point(3, 78)
            Me.PropiedadesPanel.Name = "PropiedadesPanel"
            Me.PropiedadesPanel.Size = New System.Drawing.Size(153, 24)
            Me.PropiedadesPanel.TabIndex = 11
            '
            'ControlPosicion
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.SystemColors.Control
            Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Controls.Add(Me.PropiedadesPanel)
            Me.Controls.Add(Me.ProyectoLabel)
            Me.Controls.Add(Me.EntidadLabel)
            Me.Controls.Add(Me.CajaLabel)
            Me.Controls.Add(Me.PosicionLabel)
            Me.Controls.Add(Me.L4)
            Me.Controls.Add(Me.L3)
            Me.Controls.Add(Me.L2)
            Me.Controls.Add(Me.L1)
            Me.Name = "ControlPosicion"
            Me.Size = New System.Drawing.Size(159, 105)
            Me.PropiedadesPanel.ResumeLayout(False)
            Me.PropiedadesPanel.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents L1 As System.Windows.Forms.Label
        Friend WithEvents L2 As System.Windows.Forms.Label
        Friend WithEvents L3 As System.Windows.Forms.Label
        Friend WithEvents L4 As System.Windows.Forms.Label
        Friend WithEvents PosicionLabel As System.Windows.Forms.Label
        Friend WithEvents CajaLabel As System.Windows.Forms.Label
        Friend WithEvents EntidadLabel As System.Windows.Forms.Label
        Friend WithEvents ProyectoLabel As System.Windows.Forms.Label
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents CarpetasLabel As System.Windows.Forms.Label
        Friend WithEvents TipoLabel As System.Windows.Forms.Label
        Friend WithEvents PropiedadesPanel As System.Windows.Forms.Panel

    End Class
End Namespace