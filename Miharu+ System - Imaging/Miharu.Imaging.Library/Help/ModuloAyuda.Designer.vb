Namespace Help
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class ModuloAyuda
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ModuloAyuda))
            Me.pbImagen = New System.Windows.Forms.PictureBox()
            Me.txtDescripcion = New System.Windows.Forms.TextBox()
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.FindListBox = New System.Windows.Forms.ListBox()
            Me.FindTextBox = New System.Windows.Forms.TextBox()
            Me.GroupBox2 = New System.Windows.Forms.GroupBox()
            Me.GroupBox3 = New System.Windows.Forms.GroupBox()
            Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
            CType(Me.pbImagen, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.GroupBox1.SuspendLayout()
            Me.GroupBox2.SuspendLayout()
            Me.GroupBox3.SuspendLayout()
            CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SplitContainer1.Panel1.SuspendLayout()
            Me.SplitContainer1.Panel2.SuspendLayout()
            Me.SplitContainer1.SuspendLayout()
            Me.SuspendLayout()
            '
            'pbImagen
            '
            Me.pbImagen.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
            Me.pbImagen.Dock = System.Windows.Forms.DockStyle.Fill
            Me.pbImagen.Location = New System.Drawing.Point(5, 18)
            Me.pbImagen.Name = "pbImagen"
            Me.pbImagen.Size = New System.Drawing.Size(481, 557)
            Me.pbImagen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
            Me.pbImagen.TabIndex = 2
            Me.pbImagen.TabStop = False
            '
            'txtDescripcion
            '
            Me.txtDescripcion.Dock = System.Windows.Forms.DockStyle.Fill
            Me.txtDescripcion.Location = New System.Drawing.Point(5, 18)
            Me.txtDescripcion.Multiline = True
            Me.txtDescripcion.Name = "txtDescripcion"
            Me.txtDescripcion.Size = New System.Drawing.Size(235, 367)
            Me.txtDescripcion.TabIndex = 0
            '
            'GroupBox1
            '
            Me.GroupBox1.Controls.Add(Me.FindListBox)
            Me.GroupBox1.Controls.Add(Me.FindTextBox)
            Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
            Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Padding = New System.Windows.Forms.Padding(5)
            Me.GroupBox1.Size = New System.Drawing.Size(245, 190)
            Me.GroupBox1.TabIndex = 0
            Me.GroupBox1.TabStop = False
            Me.GroupBox1.Text = "Busqueda"
            '
            'FindListBox
            '
            Me.FindListBox.Dock = System.Windows.Forms.DockStyle.Fill
            Me.FindListBox.FormattingEnabled = True
            Me.FindListBox.Location = New System.Drawing.Point(5, 38)
            Me.FindListBox.Name = "FindListBox"
            Me.FindListBox.Size = New System.Drawing.Size(235, 147)
            Me.FindListBox.TabIndex = 1
            '
            'FindTextBox
            '
            Me.FindTextBox.Dock = System.Windows.Forms.DockStyle.Top
            Me.FindTextBox.Location = New System.Drawing.Point(5, 18)
            Me.FindTextBox.Name = "FindTextBox"
            Me.FindTextBox.Size = New System.Drawing.Size(235, 20)
            Me.FindTextBox.TabIndex = 0
            '
            'GroupBox2
            '
            Me.GroupBox2.Controls.Add(Me.txtDescripcion)
            Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
            Me.GroupBox2.Location = New System.Drawing.Point(0, 190)
            Me.GroupBox2.Name = "GroupBox2"
            Me.GroupBox2.Padding = New System.Windows.Forms.Padding(5)
            Me.GroupBox2.Size = New System.Drawing.Size(245, 390)
            Me.GroupBox2.TabIndex = 2
            Me.GroupBox2.TabStop = False
            Me.GroupBox2.Text = "Observación"
            '
            'GroupBox3
            '
            Me.GroupBox3.Controls.Add(Me.pbImagen)
            Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Fill
            Me.GroupBox3.Location = New System.Drawing.Point(0, 0)
            Me.GroupBox3.Name = "GroupBox3"
            Me.GroupBox3.Padding = New System.Windows.Forms.Padding(5)
            Me.GroupBox3.Size = New System.Drawing.Size(491, 580)
            Me.GroupBox3.TabIndex = 1
            Me.GroupBox3.TabStop = False
            Me.GroupBox3.Text = "Imagen"
            '
            'SplitContainer1
            '
            Me.SplitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.SplitContainer1.Location = New System.Drawing.Point(10, 10)
            Me.SplitContainer1.Name = "SplitContainer1"
            '
            'SplitContainer1.Panel1
            '
            Me.SplitContainer1.Panel1.Controls.Add(Me.GroupBox2)
            Me.SplitContainer1.Panel1.Controls.Add(Me.GroupBox1)
            '
            'SplitContainer1.Panel2
            '
            Me.SplitContainer1.Panel2.Controls.Add(Me.GroupBox3)
            Me.SplitContainer1.Size = New System.Drawing.Size(745, 582)
            Me.SplitContainer1.SplitterDistance = 247
            Me.SplitContainer1.SplitterWidth = 5
            Me.SplitContainer1.TabIndex = 3
            '
            'ModuloAyuda
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(765, 602)
            Me.Controls.Add(Me.SplitContainer1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "ModuloAyuda"
            Me.Padding = New System.Windows.Forms.Padding(10)
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Ayuda"
            Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
            CType(Me.pbImagen, System.ComponentModel.ISupportInitialize).EndInit()
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            Me.GroupBox2.ResumeLayout(False)
            Me.GroupBox2.PerformLayout()
            Me.GroupBox3.ResumeLayout(False)
            Me.SplitContainer1.Panel1.ResumeLayout(False)
            Me.SplitContainer1.Panel2.ResumeLayout(False)
            CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.SplitContainer1.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents pbImagen As System.Windows.Forms.PictureBox
        Friend WithEvents txtDescripcion As System.Windows.Forms.TextBox
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
        Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
        Friend WithEvents FindListBox As System.Windows.Forms.ListBox
        Friend WithEvents FindTextBox As System.Windows.Forms.TextBox
        Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    End Class
End Namespace