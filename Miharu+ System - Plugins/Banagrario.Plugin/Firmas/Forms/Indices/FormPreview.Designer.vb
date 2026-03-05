Namespace Firmas.Forms.Indices
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormPreview
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormPreview))
            Me.Panel1 = New System.Windows.Forms.Panel()
            Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
            Me.cbarrasNewTextBox = New System.Windows.Forms.TextBox()
            Me.cbarrasOldTextBox = New System.Windows.Forms.TextBox()
            Me.Label4 = New System.Windows.Forms.Label()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.errorTextBox = New System.Windows.Forms.TextBox()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.rutaTextBox = New System.Windows.Forms.TextBox()
            Me.fileNameTextBox = New System.Windows.Forms.TextBox()
            Me.Panel2 = New System.Windows.Forms.Panel()
            Me.tipoComboBox = New System.Windows.Forms.ComboBox()
            Me.okButton = New System.Windows.Forms.Button()
            Me.exitButton = New System.Windows.Forms.Button()
            Me.imagePictureBox = New System.Windows.Forms.PictureBox()
            Me.Panel1.SuspendLayout()
            Me.TableLayoutPanel1.SuspendLayout()
            Me.Panel2.SuspendLayout()
            CType(Me.imagePictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'Panel1
            '
            Me.Panel1.Controls.Add(Me.TableLayoutPanel1)
            Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
            Me.Panel1.Location = New System.Drawing.Point(0, 0)
            Me.Panel1.Name = "Panel1"
            Me.Panel1.Size = New System.Drawing.Size(904, 100)
            Me.Panel1.TabIndex = 0
            '
            'TableLayoutPanel1
            '
            Me.TableLayoutPanel1.ColumnCount = 5
            Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
            Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
            Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
            Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            Me.TableLayoutPanel1.Controls.Add(Me.cbarrasNewTextBox, 4, 1)
            Me.TableLayoutPanel1.Controls.Add(Me.cbarrasOldTextBox, 4, 0)
            Me.TableLayoutPanel1.Controls.Add(Me.Label4, 3, 0)
            Me.TableLayoutPanel1.Controls.Add(Me.Label1, 0, 0)
            Me.TableLayoutPanel1.Controls.Add(Me.errorTextBox, 1, 2)
            Me.TableLayoutPanel1.Controls.Add(Me.Label2, 0, 1)
            Me.TableLayoutPanel1.Controls.Add(Me.Label3, 0, 2)
            Me.TableLayoutPanel1.Controls.Add(Me.rutaTextBox, 1, 0)
            Me.TableLayoutPanel1.Controls.Add(Me.fileNameTextBox, 1, 1)
            Me.TableLayoutPanel1.Controls.Add(Me.Panel2, 4, 2)
            Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
            Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
            Me.TableLayoutPanel1.RowCount = 4
            Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
            Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
            Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
            Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.TableLayoutPanel1.Size = New System.Drawing.Size(904, 100)
            Me.TableLayoutPanel1.TabIndex = 6
            '
            'cbarrasNewTextBox
            '
            Me.cbarrasNewTextBox.Dock = System.Windows.Forms.DockStyle.Fill
            Me.cbarrasNewTextBox.Location = New System.Drawing.Point(565, 33)
            Me.cbarrasNewTextBox.Name = "cbarrasNewTextBox"
            Me.cbarrasNewTextBox.Size = New System.Drawing.Size(336, 20)
            Me.cbarrasNewTextBox.TabIndex = 8
            '
            'cbarrasOldTextBox
            '
            Me.cbarrasOldTextBox.Dock = System.Windows.Forms.DockStyle.Fill
            Me.cbarrasOldTextBox.Location = New System.Drawing.Point(565, 3)
            Me.cbarrasOldTextBox.Name = "cbarrasOldTextBox"
            Me.cbarrasOldTextBox.ReadOnly = True
            Me.cbarrasOldTextBox.Size = New System.Drawing.Size(336, 20)
            Me.cbarrasOldTextBox.TabIndex = 7
            '
            'Label4
            '
            Me.Label4.Dock = System.Windows.Forms.DockStyle.Fill
            Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label4.Location = New System.Drawing.Point(465, 0)
            Me.Label4.Name = "Label4"
            Me.Label4.Size = New System.Drawing.Size(94, 30)
            Me.Label4.TabIndex = 6
            Me.Label4.Text = "CBarras"
            Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'Label1
            '
            Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.Location = New System.Drawing.Point(3, 0)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(74, 30)
            Me.Label1.TabIndex = 0
            Me.Label1.Text = "Ruta"
            Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'errorTextBox
            '
            Me.TableLayoutPanel1.SetColumnSpan(Me.errorTextBox, 3)
            Me.errorTextBox.Dock = System.Windows.Forms.DockStyle.Fill
            Me.errorTextBox.ForeColor = System.Drawing.Color.Red
            Me.errorTextBox.Location = New System.Drawing.Point(83, 63)
            Me.errorTextBox.Name = "errorTextBox"
            Me.errorTextBox.ReadOnly = True
            Me.errorTextBox.Size = New System.Drawing.Size(476, 20)
            Me.errorTextBox.TabIndex = 5
            '
            'Label2
            '
            Me.Label2.Dock = System.Windows.Forms.DockStyle.Fill
            Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label2.Location = New System.Drawing.Point(3, 30)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(74, 30)
            Me.Label2.TabIndex = 2
            Me.Label2.Text = "Imagen"
            Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'Label3
            '
            Me.Label3.Dock = System.Windows.Forms.DockStyle.Fill
            Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label3.Location = New System.Drawing.Point(3, 60)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(74, 30)
            Me.Label3.TabIndex = 4
            Me.Label3.Text = "Error"
            Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'rutaTextBox
            '
            Me.rutaTextBox.Dock = System.Windows.Forms.DockStyle.Fill
            Me.rutaTextBox.Location = New System.Drawing.Point(83, 3)
            Me.rutaTextBox.Name = "rutaTextBox"
            Me.rutaTextBox.ReadOnly = True
            Me.rutaTextBox.Size = New System.Drawing.Size(336, 20)
            Me.rutaTextBox.TabIndex = 1
            '
            'fileNameTextBox
            '
            Me.fileNameTextBox.Dock = System.Windows.Forms.DockStyle.Fill
            Me.fileNameTextBox.Location = New System.Drawing.Point(83, 33)
            Me.fileNameTextBox.Name = "fileNameTextBox"
            Me.fileNameTextBox.ReadOnly = True
            Me.fileNameTextBox.Size = New System.Drawing.Size(336, 20)
            Me.fileNameTextBox.TabIndex = 3
            '
            'Panel2
            '
            Me.Panel2.Controls.Add(Me.tipoComboBox)
            Me.Panel2.Controls.Add(Me.okButton)
            Me.Panel2.Controls.Add(Me.exitButton)
            Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
            Me.Panel2.Location = New System.Drawing.Point(565, 63)
            Me.Panel2.Name = "Panel2"
            Me.Panel2.Size = New System.Drawing.Size(336, 24)
            Me.Panel2.TabIndex = 9
            '
            'tipoComboBox
            '
            Me.tipoComboBox.Dock = System.Windows.Forms.DockStyle.Fill
            Me.tipoComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.tipoComboBox.FormattingEnabled = True
            Me.tipoComboBox.Items.AddRange(New Object() {"- Seleccione el tipo -", "Ilegible", "No identificado", "Tapa", "Tarjeta de firmas", "Sin Cod. Barras", "Diligenciada Manual"})
            Me.tipoComboBox.Location = New System.Drawing.Point(0, 0)
            Me.tipoComboBox.Name = "tipoComboBox"
            Me.tipoComboBox.Size = New System.Drawing.Size(206, 21)
            Me.tipoComboBox.TabIndex = 2
            '
            'okButton
            '
            Me.okButton.Dock = System.Windows.Forms.DockStyle.Right
            Me.okButton.Image = Global.Banagrario.Plugin.My.Resources.Resources.Aceptar
            Me.okButton.Location = New System.Drawing.Point(206, 0)
            Me.okButton.Name = "okButton"
            Me.okButton.Size = New System.Drawing.Size(65, 24)
            Me.okButton.TabIndex = 1
            Me.okButton.UseVisualStyleBackColor = True
            '
            'exitButton
            '
            Me.exitButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.exitButton.Dock = System.Windows.Forms.DockStyle.Right
            Me.exitButton.Image = Global.Banagrario.Plugin.My.Resources.Resources.Cancelar
            Me.exitButton.Location = New System.Drawing.Point(271, 0)
            Me.exitButton.Name = "exitButton"
            Me.exitButton.Size = New System.Drawing.Size(65, 24)
            Me.exitButton.TabIndex = 0
            Me.exitButton.UseVisualStyleBackColor = True
            '
            'imagePictureBox
            '
            Me.imagePictureBox.Dock = System.Windows.Forms.DockStyle.Fill
            Me.imagePictureBox.Location = New System.Drawing.Point(0, 100)
            Me.imagePictureBox.Name = "imagePictureBox"
            Me.imagePictureBox.Size = New System.Drawing.Size(904, 285)
            Me.imagePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
            Me.imagePictureBox.TabIndex = 1
            Me.imagePictureBox.TabStop = False
            '
            'FormPreview
            '
            Me.AcceptButton = Me.okButton
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.exitButton
            Me.ClientSize = New System.Drawing.Size(904, 385)
            Me.Controls.Add(Me.imagePictureBox)
            Me.Controls.Add(Me.Panel1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "FormPreview"
            Me.Text = "Calificar"
            Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
            Me.Panel1.ResumeLayout(False)
            Me.TableLayoutPanel1.ResumeLayout(False)
            Me.TableLayoutPanel1.PerformLayout()
            Me.Panel2.ResumeLayout(False)
            CType(Me.imagePictureBox, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents Panel1 As System.Windows.Forms.Panel
        Friend WithEvents errorTextBox As System.Windows.Forms.TextBox
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents fileNameTextBox As System.Windows.Forms.TextBox
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents rutaTextBox As System.Windows.Forms.TextBox
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents imagePictureBox As System.Windows.Forms.PictureBox
        Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
        Friend WithEvents Label4 As System.Windows.Forms.Label
        Friend WithEvents cbarrasNewTextBox As System.Windows.Forms.TextBox
        Friend WithEvents cbarrasOldTextBox As System.Windows.Forms.TextBox
        Friend WithEvents Panel2 As System.Windows.Forms.Panel
        Friend WithEvents tipoComboBox As System.Windows.Forms.ComboBox
        Friend WithEvents okButton As System.Windows.Forms.Button
        Friend WithEvents exitButton As System.Windows.Forms.Button
    End Class
End Namespace