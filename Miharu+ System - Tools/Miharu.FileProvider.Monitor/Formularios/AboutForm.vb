Public Class AboutForm
    Inherits System.Windows.Forms.Form

#Region " Código generado por el Diseñador de Windows Forms "

    Public Sub New()
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()

    End Sub

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    Friend WithEvents lblProductName As System.Windows.Forms.Label
    Friend WithEvents lblVersion As System.Windows.Forms.Label
    Friend WithEvents lblCopyright As System.Windows.Forms.Label
    Friend WithEvents lblCompanyName As System.Windows.Forms.Label
    Friend WithEvents txtBoxDescription As System.Windows.Forms.TextBox
    Friend WithEvents btnAceptar As System.Windows.Forms.Button
    Friend WithEvents pnlSeparador As System.Windows.Forms.Panel
    Friend WithEvents pi As System.Windows.Forms.PictureBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AboutForm))
        Me.lblProductName = New System.Windows.Forms.Label
        Me.pi = New System.Windows.Forms.PictureBox
        Me.lblVersion = New System.Windows.Forms.Label
        Me.lblCopyright = New System.Windows.Forms.Label
        Me.lblCompanyName = New System.Windows.Forms.Label
        Me.txtBoxDescription = New System.Windows.Forms.TextBox
        Me.btnAceptar = New System.Windows.Forms.Button
        Me.pnlSeparador = New System.Windows.Forms.Panel
        CType(Me.pi, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblProductName
        '
        Me.lblProductName.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblProductName.Location = New System.Drawing.Point(12, 9)
        Me.lblProductName.Name = "lblProductName"
        Me.lblProductName.Size = New System.Drawing.Size(224, 23)
        Me.lblProductName.TabIndex = 0
        Me.lblProductName.Text = "lblProductName"
        '
        'pi
        '
        Me.pi.Image = Global.Miharu.FileProvider.Monitor.My.Resources.Resources.FileProviderMonitor
        Me.pi.Location = New System.Drawing.Point(238, 41)
        Me.pi.Name = "pi"
        Me.pi.Size = New System.Drawing.Size(32, 32)
        Me.pi.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.pi.TabIndex = 1
        Me.pi.TabStop = False
        '
        'lblVersion
        '
        Me.lblVersion.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVersion.Location = New System.Drawing.Point(12, 41)
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.Size = New System.Drawing.Size(224, 16)
        Me.lblVersion.TabIndex = 2
        Me.lblVersion.Text = "lblVersion"
        '
        'lblCopyright
        '
        Me.lblCopyright.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCopyright.Location = New System.Drawing.Point(12, 65)
        Me.lblCopyright.Name = "lblCopyright"
        Me.lblCopyright.Size = New System.Drawing.Size(224, 16)
        Me.lblCopyright.TabIndex = 3
        Me.lblCopyright.Text = "lblCopyright"
        '
        'lblCompanyName
        '
        Me.lblCompanyName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCompanyName.Location = New System.Drawing.Point(12, 89)
        Me.lblCompanyName.Name = "lblCompanyName"
        Me.lblCompanyName.Size = New System.Drawing.Size(224, 16)
        Me.lblCompanyName.TabIndex = 4
        Me.lblCompanyName.Text = "lblCompanyName"
        '
        'txtBoxDescription
        '
        Me.txtBoxDescription.BackColor = System.Drawing.Color.White
        Me.txtBoxDescription.Location = New System.Drawing.Point(12, 121)
        Me.txtBoxDescription.Multiline = True
        Me.txtBoxDescription.Name = "txtBoxDescription"
        Me.txtBoxDescription.ReadOnly = True
        Me.txtBoxDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtBoxDescription.Size = New System.Drawing.Size(258, 64)
        Me.txtBoxDescription.TabIndex = 5
        Me.txtBoxDescription.Text = "txtBoxDescription"
        '
        'btnAceptar
        '
        Me.btnAceptar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnAceptar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAceptar.Image = Global.Miharu.FileProvider.Monitor.My.Resources.Resources.Aceptar
        Me.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAceptar.Location = New System.Drawing.Point(198, 211)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Size = New System.Drawing.Size(72, 24)
        Me.btnAceptar.TabIndex = 0
        Me.btnAceptar.Text = "&Aceptar"
        Me.btnAceptar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlSeparador
        '
        Me.pnlSeparador.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlSeparador.Location = New System.Drawing.Point(4, 193)
        Me.pnlSeparador.Name = "pnlSeparador"
        Me.pnlSeparador.Size = New System.Drawing.Size(275, 4)
        Me.pnlSeparador.TabIndex = 7
        '
        'AboutForm
        '
        Me.AcceptButton = Me.btnAceptar
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(282, 247)
        Me.Controls.Add(Me.pnlSeparador)
        Me.Controls.Add(Me.btnAceptar)
        Me.Controls.Add(Me.txtBoxDescription)
        Me.Controls.Add(Me.lblCompanyName)
        Me.Controls.Add(Me.lblCopyright)
        Me.Controls.Add(Me.lblVersion)
        Me.Controls.Add(Me.lblProductName)
        Me.Controls.Add(Me.pi)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "AboutForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        CType(Me.pi, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private Sub classFormAbout_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ShowData()
    End Sub

    Private Sub ShowData()
        Me.Text = String.Format("Acerca de {0}", Program.AssemblyTitle)

        Me.lblProductName.Text = Program.AssemblyProduct
        Me.lblVersion.Text = String.Format("Versión {0}", Program.AssemblyVersion)
        Me.lblCopyright.Text = Program.AssemblyCopyright
        Me.lblCompanyName.Text = Program.AssemblyCompany
        Me.txtBoxDescription.Text = Program.AssemblyDescription
    End Sub

End Class
