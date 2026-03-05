Namespace Firmas.Forms.Indices
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormIndexGenerator
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormIndexGenerator))
            Me.mainGroupBox = New System.Windows.Forms.GroupBox()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.pathTextBox = New System.Windows.Forms.TextBox()
            Me.logRichTextBox = New System.Windows.Forms.RichTextBox()
            Me.mainProgressBar = New System.Windows.Forms.ProgressBar()
            Me.processButton = New System.Windows.Forms.Button()
            Me.folderButton = New System.Windows.Forms.Button()
            Me.mainGroupBox.SuspendLayout()
            Me.SuspendLayout()
            '
            'mainGroupBox
            '
            Me.mainGroupBox.Controls.Add(Me.Label1)
            Me.mainGroupBox.Controls.Add(Me.processButton)
            Me.mainGroupBox.Controls.Add(Me.folderButton)
            Me.mainGroupBox.Controls.Add(Me.pathTextBox)
            Me.mainGroupBox.Dock = System.Windows.Forms.DockStyle.Top
            Me.mainGroupBox.Location = New System.Drawing.Point(5, 5)
            Me.mainGroupBox.Name = "mainGroupBox"
            Me.mainGroupBox.Size = New System.Drawing.Size(683, 80)
            Me.mainGroupBox.TabIndex = 0
            Me.mainGroupBox.TabStop = False
            Me.mainGroupBox.Text = "Parámetros"
            '
            'Label1
            '
            Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.Location = New System.Drawing.Point(7, 20)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(100, 16)
            Me.Label1.TabIndex = 3
            Me.Label1.Text = "Ruta"
            '
            'pathTextBox
            '
            Me.pathTextBox.Location = New System.Drawing.Point(6, 39)
            Me.pathTextBox.Name = "pathTextBox"
            Me.pathTextBox.Size = New System.Drawing.Size(455, 20)
            Me.pathTextBox.TabIndex = 0
            '
            'logRichTextBox
            '
            Me.logRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill
            Me.logRichTextBox.Location = New System.Drawing.Point(5, 85)
            Me.logRichTextBox.Name = "logRichTextBox"
            Me.logRichTextBox.ReadOnly = True
            Me.logRichTextBox.Size = New System.Drawing.Size(683, 278)
            Me.logRichTextBox.TabIndex = 1
            Me.logRichTextBox.Text = ""
            '
            'mainProgressBar
            '
            Me.mainProgressBar.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.mainProgressBar.Location = New System.Drawing.Point(5, 363)
            Me.mainProgressBar.Name = "mainProgressBar"
            Me.mainProgressBar.Size = New System.Drawing.Size(683, 23)
            Me.mainProgressBar.TabIndex = 5
            '
            'processButton
            '
            Me.processButton.Image = Global.Banagrario.Plugin.My.Resources.Resources.Aceptar
            Me.processButton.Location = New System.Drawing.Point(523, 25)
            Me.processButton.Name = "processButton"
            Me.processButton.Size = New System.Drawing.Size(36, 34)
            Me.processButton.TabIndex = 2
            Me.processButton.UseVisualStyleBackColor = True
            '
            'folderButton
            '
            Me.folderButton.Image = Global.Banagrario.Plugin.My.Resources.Resources.folder_add2
            Me.folderButton.Location = New System.Drawing.Point(481, 25)
            Me.folderButton.Name = "folderButton"
            Me.folderButton.Size = New System.Drawing.Size(36, 34)
            Me.folderButton.TabIndex = 1
            Me.folderButton.UseVisualStyleBackColor = True
            '
            'FormIndexGenerator
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(693, 391)
            Me.Controls.Add(Me.logRichTextBox)
            Me.Controls.Add(Me.mainProgressBar)
            Me.Controls.Add(Me.mainGroupBox)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "FormIndexGenerator"
            Me.Padding = New System.Windows.Forms.Padding(5)
            Me.Text = "Index Generator"
            Me.mainGroupBox.ResumeLayout(False)
            Me.mainGroupBox.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents mainGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents logRichTextBox As System.Windows.Forms.RichTextBox
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents processButton As System.Windows.Forms.Button
        Friend WithEvents folderButton As System.Windows.Forms.Button
        Friend WithEvents pathTextBox As System.Windows.Forms.TextBox
        Friend WithEvents mainProgressBar As System.Windows.Forms.ProgressBar
    End Class
End Namespace