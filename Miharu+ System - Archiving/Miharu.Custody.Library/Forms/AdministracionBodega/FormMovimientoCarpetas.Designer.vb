Namespace Forms.AdministracionBodega
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormMovimientoCarpetas
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
            Me.TituloLabel = New System.Windows.Forms.Label()
            Me.Panel1 = New System.Windows.Forms.Panel()
            Me.Label13 = New System.Windows.Forms.Label()
            Me.ProfundidadLabelOrigen = New System.Windows.Forms.Label()
            Me.ColumnaLabelOrigen = New System.Windows.Forms.Label()
            Me.FilaLabelOrigen = New System.Windows.Forms.Label()
            Me.EstanteLabelOrigen = New System.Windows.Forms.Label()
            Me.SeccionLabelOrigen = New System.Windows.Forms.Label()
            Me.Label8 = New System.Windows.Forms.Label()
            Me.Label7 = New System.Windows.Forms.Label()
            Me.Label10 = New System.Windows.Forms.Label()
            Me.Label11 = New System.Windows.Forms.Label()
            Me.Label12 = New System.Windows.Forms.Label()
            Me.FindButton = New System.Windows.Forms.Button()
            Me.CajaInicialTextBox = New System.Windows.Forms.TextBox()
            Me.DetallePanel = New System.Windows.Forms.Panel()
            Me.Label14 = New System.Windows.Forms.Label()
            Me.Label9 = New System.Windows.Forms.Label()
            Me.FindFolderButton = New System.Windows.Forms.Button()
            Me.cbarrasDesktopCBarrasControl = New System.Windows.Forms.TextBox()
            Me.CajaLabel = New System.Windows.Forms.Label()
            Me.ProfundidadLabel = New System.Windows.Forms.Label()
            Me.ColumnaLabel = New System.Windows.Forms.Label()
            Me.FilaLabel = New System.Windows.Forms.Label()
            Me.EstanteLabel = New System.Windows.Forms.Label()
            Me.SeccionLabel = New System.Windows.Forms.Label()
            Me.Label6 = New System.Windows.Forms.Label()
            Me.Label5 = New System.Windows.Forms.Label()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.Label4 = New System.Windows.Forms.Label()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.FindCajaButton = New System.Windows.Forms.Button()
            Me.CajaFinalTextBox = New System.Windows.Forms.TextBox()
            Me.ChkMantenerCajaOrigenCajaDestino = New System.Windows.Forms.CheckBox()
            Me.Panel1.SuspendLayout()
            Me.DetallePanel.SuspendLayout()
            Me.SuspendLayout()
            '
            'TituloLabel
            '
            Me.TituloLabel.Dock = System.Windows.Forms.DockStyle.Top
            Me.TituloLabel.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
            Me.TituloLabel.ForeColor = System.Drawing.Color.Black
            Me.TituloLabel.Location = New System.Drawing.Point(0, 0)
            Me.TituloLabel.Name = "TituloLabel"
            Me.TituloLabel.Size = New System.Drawing.Size(366, 20)
            Me.TituloLabel.TabIndex = 3
            Me.TituloLabel.Text = "Movimientos - Carpetas"
            Me.TituloLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter
            '
            'Panel1
            '
            Me.Panel1.BackColor = System.Drawing.Color.White
            Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
            Me.Panel1.Controls.Add(Me.Label13)
            Me.Panel1.Controls.Add(Me.ProfundidadLabelOrigen)
            Me.Panel1.Controls.Add(Me.ColumnaLabelOrigen)
            Me.Panel1.Controls.Add(Me.FilaLabelOrigen)
            Me.Panel1.Controls.Add(Me.EstanteLabelOrigen)
            Me.Panel1.Controls.Add(Me.SeccionLabelOrigen)
            Me.Panel1.Controls.Add(Me.Label8)
            Me.Panel1.Controls.Add(Me.Label7)
            Me.Panel1.Controls.Add(Me.Label10)
            Me.Panel1.Controls.Add(Me.Label11)
            Me.Panel1.Controls.Add(Me.Label12)
            Me.Panel1.Controls.Add(Me.FindButton)
            Me.Panel1.Controls.Add(Me.CajaInicialTextBox)
            Me.Panel1.Location = New System.Drawing.Point(0, 20)
            Me.Panel1.Name = "Panel1"
            Me.Panel1.Size = New System.Drawing.Size(362, 155)
            Me.Panel1.TabIndex = 4
            '
            'Label13
            '
            Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label13.ForeColor = System.Drawing.Color.Black
            Me.Label13.Location = New System.Drawing.Point(132, 10)
            Me.Label13.Name = "Label13"
            Me.Label13.Size = New System.Drawing.Size(84, 15)
            Me.Label13.TabIndex = 50
            Me.Label13.Text = "Origen:"
            '
            'ProfundidadLabelOrigen
            '
            Me.ProfundidadLabelOrigen.Location = New System.Drawing.Point(109, 130)
            Me.ProfundidadLabelOrigen.Name = "ProfundidadLabelOrigen"
            Me.ProfundidadLabelOrigen.Size = New System.Drawing.Size(84, 15)
            Me.ProfundidadLabelOrigen.TabIndex = 49
            '
            'ColumnaLabelOrigen
            '
            Me.ColumnaLabelOrigen.Location = New System.Drawing.Point(270, 100)
            Me.ColumnaLabelOrigen.Name = "ColumnaLabelOrigen"
            Me.ColumnaLabelOrigen.Size = New System.Drawing.Size(84, 15)
            Me.ColumnaLabelOrigen.TabIndex = 48
            '
            'FilaLabelOrigen
            '
            Me.FilaLabelOrigen.Location = New System.Drawing.Point(83, 100)
            Me.FilaLabelOrigen.Name = "FilaLabelOrigen"
            Me.FilaLabelOrigen.Size = New System.Drawing.Size(84, 15)
            Me.FilaLabelOrigen.TabIndex = 47
            '
            'EstanteLabelOrigen
            '
            Me.EstanteLabelOrigen.Location = New System.Drawing.Point(268, 70)
            Me.EstanteLabelOrigen.Name = "EstanteLabelOrigen"
            Me.EstanteLabelOrigen.Size = New System.Drawing.Size(84, 15)
            Me.EstanteLabelOrigen.TabIndex = 46
            '
            'SeccionLabelOrigen
            '
            Me.SeccionLabelOrigen.Location = New System.Drawing.Point(83, 70)
            Me.SeccionLabelOrigen.Name = "SeccionLabelOrigen"
            Me.SeccionLabelOrigen.Size = New System.Drawing.Size(84, 15)
            Me.SeccionLabelOrigen.TabIndex = 45
            '
            'Label8
            '
            Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label8.ForeColor = System.Drawing.Color.Black
            Me.Label8.Location = New System.Drawing.Point(30, 130)
            Me.Label8.Name = "Label8"
            Me.Label8.Size = New System.Drawing.Size(84, 15)
            Me.Label8.TabIndex = 40
            Me.Label8.Text = "Profundidad:"
            '
            'Label7
            '
            Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label7.ForeColor = System.Drawing.Color.Black
            Me.Label7.Location = New System.Drawing.Point(207, 100)
            Me.Label7.Name = "Label7"
            Me.Label7.Size = New System.Drawing.Size(84, 15)
            Me.Label7.TabIndex = 41
            Me.Label7.Text = "Columna:"
            '
            'Label10
            '
            Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label10.ForeColor = System.Drawing.Color.Black
            Me.Label10.Location = New System.Drawing.Point(30, 100)
            Me.Label10.Name = "Label10"
            Me.Label10.Size = New System.Drawing.Size(84, 15)
            Me.Label10.TabIndex = 42
            Me.Label10.Text = "Fila:"
            '
            'Label11
            '
            Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label11.ForeColor = System.Drawing.Color.Black
            Me.Label11.Location = New System.Drawing.Point(207, 70)
            Me.Label11.Name = "Label11"
            Me.Label11.Size = New System.Drawing.Size(84, 15)
            Me.Label11.TabIndex = 43
            Me.Label11.Text = "Estante:"
            '
            'Label12
            '
            Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label12.ForeColor = System.Drawing.Color.Black
            Me.Label12.Location = New System.Drawing.Point(30, 70)
            Me.Label12.Name = "Label12"
            Me.Label12.Size = New System.Drawing.Size(84, 15)
            Me.Label12.TabIndex = 44
            Me.Label12.Text = "Seccion:"
            '
            'FindButton
            '
            Me.FindButton.Image = Global.Miharu.Custody.Library.My.Resources.Resources.btnBuscar
            Me.FindButton.Location = New System.Drawing.Point(246, 28)
            Me.FindButton.Name = "FindButton"
            Me.FindButton.Size = New System.Drawing.Size(45, 23)
            Me.FindButton.TabIndex = 1
            '
            'CajaInicialTextBox
            '
            Me.CajaInicialTextBox.Location = New System.Drawing.Point(60, 28)
            Me.CajaInicialTextBox.Name = "CajaInicialTextBox"
            Me.CajaInicialTextBox.Size = New System.Drawing.Size(177, 20)
            Me.CajaInicialTextBox.TabIndex = 0
            '
            'DetallePanel
            '
            Me.DetallePanel.BackColor = System.Drawing.Color.White
            Me.DetallePanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
            Me.DetallePanel.Controls.Add(Me.ChkMantenerCajaOrigenCajaDestino)
            Me.DetallePanel.Controls.Add(Me.Label14)
            Me.DetallePanel.Controls.Add(Me.Label9)
            Me.DetallePanel.Controls.Add(Me.FindFolderButton)
            Me.DetallePanel.Controls.Add(Me.cbarrasDesktopCBarrasControl)
            Me.DetallePanel.Controls.Add(Me.CajaLabel)
            Me.DetallePanel.Controls.Add(Me.ProfundidadLabel)
            Me.DetallePanel.Controls.Add(Me.ColumnaLabel)
            Me.DetallePanel.Controls.Add(Me.FilaLabel)
            Me.DetallePanel.Controls.Add(Me.EstanteLabel)
            Me.DetallePanel.Controls.Add(Me.SeccionLabel)
            Me.DetallePanel.Controls.Add(Me.Label6)
            Me.DetallePanel.Controls.Add(Me.Label5)
            Me.DetallePanel.Controls.Add(Me.Label3)
            Me.DetallePanel.Controls.Add(Me.Label4)
            Me.DetallePanel.Controls.Add(Me.Label2)
            Me.DetallePanel.Controls.Add(Me.Label1)
            Me.DetallePanel.Controls.Add(Me.FindCajaButton)
            Me.DetallePanel.Controls.Add(Me.CajaFinalTextBox)
            Me.DetallePanel.Location = New System.Drawing.Point(0, 183)
            Me.DetallePanel.Name = "DetallePanel"
            Me.DetallePanel.Size = New System.Drawing.Size(362, 267)
            Me.DetallePanel.TabIndex = 5
            '
            'Label14
            '
            Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label14.ForeColor = System.Drawing.Color.Black
            Me.Label14.Location = New System.Drawing.Point(137, 17)
            Me.Label14.Name = "Label14"
            Me.Label14.Size = New System.Drawing.Size(84, 15)
            Me.Label14.TabIndex = 31
            Me.Label14.Text = "Destino:"
            '
            'Label9
            '
            Me.Label9.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
            Me.Label9.ForeColor = System.Drawing.Color.Black
            Me.Label9.Location = New System.Drawing.Point(60, 221)
            Me.Label9.Name = "Label9"
            Me.Label9.Size = New System.Drawing.Size(74, 13)
            Me.Label9.TabIndex = 0
            Me.Label9.Text = "Folder"
            '
            'FindFolderButton
            '
            Me.FindFolderButton.Image = Global.Miharu.Custody.Library.My.Resources.Resources.btnBuscar
            Me.FindFolderButton.Location = New System.Drawing.Point(251, 235)
            Me.FindFolderButton.Name = "FindFolderButton"
            Me.FindFolderButton.Size = New System.Drawing.Size(45, 23)
            Me.FindFolderButton.TabIndex = 3
            '
            'cbarrasDesktopCBarrasControl
            '
            Me.cbarrasDesktopCBarrasControl.Location = New System.Drawing.Point(60, 235)
            Me.cbarrasDesktopCBarrasControl.Name = "cbarrasDesktopCBarrasControl"
            Me.cbarrasDesktopCBarrasControl.Size = New System.Drawing.Size(177, 20)
            Me.cbarrasDesktopCBarrasControl.TabIndex = 2
            '
            'CajaLabel
            '
            Me.CajaLabel.Location = New System.Drawing.Point(97, 193)
            Me.CajaLabel.Name = "CajaLabel"
            Me.CajaLabel.Size = New System.Drawing.Size(151, 15)
            Me.CajaLabel.TabIndex = 4
            '
            'ProfundidadLabel
            '
            Me.ProfundidadLabel.Location = New System.Drawing.Point(108, 166)
            Me.ProfundidadLabel.Name = "ProfundidadLabel"
            Me.ProfundidadLabel.Size = New System.Drawing.Size(84, 15)
            Me.ProfundidadLabel.TabIndex = 5
            '
            'ColumnaLabel
            '
            Me.ColumnaLabel.Location = New System.Drawing.Point(274, 137)
            Me.ColumnaLabel.Name = "ColumnaLabel"
            Me.ColumnaLabel.Size = New System.Drawing.Size(84, 15)
            Me.ColumnaLabel.TabIndex = 6
            '
            'FilaLabel
            '
            Me.FilaLabel.Location = New System.Drawing.Point(80, 137)
            Me.FilaLabel.Name = "FilaLabel"
            Me.FilaLabel.Size = New System.Drawing.Size(84, 15)
            Me.FilaLabel.TabIndex = 7
            '
            'EstanteLabel
            '
            Me.EstanteLabel.Location = New System.Drawing.Point(274, 107)
            Me.EstanteLabel.Name = "EstanteLabel"
            Me.EstanteLabel.Size = New System.Drawing.Size(84, 15)
            Me.EstanteLabel.TabIndex = 8
            '
            'SeccionLabel
            '
            Me.SeccionLabel.Location = New System.Drawing.Point(84, 107)
            Me.SeccionLabel.Name = "SeccionLabel"
            Me.SeccionLabel.Size = New System.Drawing.Size(84, 15)
            Me.SeccionLabel.TabIndex = 9
            '
            'Label6
            '
            Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label6.ForeColor = System.Drawing.Color.Black
            Me.Label6.Location = New System.Drawing.Point(30, 193)
            Me.Label6.Name = "Label6"
            Me.Label6.Size = New System.Drawing.Size(84, 15)
            Me.Label6.TabIndex = 10
            Me.Label6.Text = "Caja:"
            '
            'Label5
            '
            Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label5.ForeColor = System.Drawing.Color.Black
            Me.Label5.Location = New System.Drawing.Point(30, 166)
            Me.Label5.Name = "Label5"
            Me.Label5.Size = New System.Drawing.Size(84, 15)
            Me.Label5.TabIndex = 11
            Me.Label5.Text = "Profundidad:"
            '
            'Label3
            '
            Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label3.ForeColor = System.Drawing.Color.Black
            Me.Label3.Location = New System.Drawing.Point(207, 137)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(84, 15)
            Me.Label3.TabIndex = 12
            Me.Label3.Text = "Columna:"
            '
            'Label4
            '
            Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label4.ForeColor = System.Drawing.Color.Black
            Me.Label4.Location = New System.Drawing.Point(30, 137)
            Me.Label4.Name = "Label4"
            Me.Label4.Size = New System.Drawing.Size(84, 15)
            Me.Label4.TabIndex = 13
            Me.Label4.Text = "Fila:"
            '
            'Label2
            '
            Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label2.ForeColor = System.Drawing.Color.Black
            Me.Label2.Location = New System.Drawing.Point(207, 107)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(84, 15)
            Me.Label2.TabIndex = 14
            Me.Label2.Text = "Estante:"
            '
            'Label1
            '
            Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.ForeColor = System.Drawing.Color.Black
            Me.Label1.Location = New System.Drawing.Point(30, 107)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(84, 15)
            Me.Label1.TabIndex = 15
            Me.Label1.Text = "Seccion:"
            '
            'FindCajaButton
            '
            Me.FindCajaButton.Image = Global.Miharu.Custody.Library.My.Resources.Resources.btnBuscar
            Me.FindCajaButton.Location = New System.Drawing.Point(251, 35)
            Me.FindCajaButton.Name = "FindCajaButton"
            Me.FindCajaButton.Size = New System.Drawing.Size(45, 23)
            Me.FindCajaButton.TabIndex = 1
            '
            'CajaFinalTextBox
            '
            Me.CajaFinalTextBox.Location = New System.Drawing.Point(56, 35)
            Me.CajaFinalTextBox.Name = "CajaFinalTextBox"
            Me.CajaFinalTextBox.Size = New System.Drawing.Size(177, 20)
            Me.CajaFinalTextBox.TabIndex = 0
            '
            'ChkMantenerCajaOrigenCajaDestino
            '
            Me.ChkMantenerCajaOrigenCajaDestino.AutoSize = True
            Me.ChkMantenerCajaOrigenCajaDestino.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ChkMantenerCajaOrigenCajaDestino.Location = New System.Drawing.Point(33, 64)
            Me.ChkMantenerCajaOrigenCajaDestino.Name = "ChkMantenerCajaOrigenCajaDestino"
            Me.ChkMantenerCajaOrigenCajaDestino.Size = New System.Drawing.Size(229, 17)
            Me.ChkMantenerCajaOrigenCajaDestino.TabIndex = 32
            Me.ChkMantenerCajaOrigenCajaDestino.Text = "Mantener caja origen y caja destino"
            Me.ChkMantenerCajaOrigenCajaDestino.UseVisualStyleBackColor = True
            '
            'FormMovimientoCarpetas
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(366, 452)
            Me.Controls.Add(Me.DetallePanel)
            Me.Controls.Add(Me.Panel1)
            Me.Controls.Add(Me.TituloLabel)
            Me.Name = "FormMovimientoCarpetas"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Administración Bodega"
            Me.Panel1.ResumeLayout(False)
            Me.Panel1.PerformLayout()
            Me.DetallePanel.ResumeLayout(False)
            Me.DetallePanel.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Private WithEvents TituloLabel As System.Windows.Forms.Label
        Private WithEvents Panel1 As System.Windows.Forms.Panel
        Private WithEvents FindButton As System.Windows.Forms.Button
        Private WithEvents CajaInicialTextBox As System.Windows.Forms.TextBox
        Private WithEvents DetallePanel As System.Windows.Forms.Panel
        Private WithEvents Label9 As System.Windows.Forms.Label
        Private WithEvents FindFolderButton As System.Windows.Forms.Button
        Private WithEvents cbarrasDesktopCBarrasControl As System.Windows.Forms.TextBox
        Private WithEvents CajaLabel As System.Windows.Forms.Label
        Private WithEvents ProfundidadLabel As System.Windows.Forms.Label
        Private WithEvents ColumnaLabel As System.Windows.Forms.Label
        Private WithEvents FilaLabel As System.Windows.Forms.Label
        Private WithEvents EstanteLabel As System.Windows.Forms.Label
        Private WithEvents SeccionLabel As System.Windows.Forms.Label
        Private WithEvents Label6 As System.Windows.Forms.Label
        Private WithEvents Label5 As System.Windows.Forms.Label
        Private WithEvents Label3 As System.Windows.Forms.Label
        Private WithEvents Label4 As System.Windows.Forms.Label
        Private WithEvents Label2 As System.Windows.Forms.Label
        Private WithEvents Label1 As System.Windows.Forms.Label
        Private WithEvents FindCajaButton As System.Windows.Forms.Button
        Private WithEvents CajaFinalTextBox As System.Windows.Forms.TextBox
        Private WithEvents ProfundidadLabelOrigen As System.Windows.Forms.Label
        Private WithEvents ColumnaLabelOrigen As System.Windows.Forms.Label
        Private WithEvents FilaLabelOrigen As System.Windows.Forms.Label
        Private WithEvents EstanteLabelOrigen As System.Windows.Forms.Label
        Private WithEvents SeccionLabelOrigen As System.Windows.Forms.Label
        Private WithEvents Label8 As System.Windows.Forms.Label
        Private WithEvents Label7 As System.Windows.Forms.Label
        Private WithEvents Label10 As System.Windows.Forms.Label
        Private WithEvents Label11 As System.Windows.Forms.Label
        Private WithEvents Label12 As System.Windows.Forms.Label
        Private WithEvents Label13 As System.Windows.Forms.Label
        Private WithEvents Label14 As System.Windows.Forms.Label
        Friend WithEvents ChkMantenerCajaOrigenCajaDestino As System.Windows.Forms.CheckBox
    End Class
End Namespace