Namespace View.Indexacion
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class ListValidationControl
        Inherits System.Windows.Forms.UserControl

        'UserControl reemplaza a Dispose para limpiar la lista de componentes.
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
            Dim Rango1 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ListValidationControl))
            Me.RutaGroupBox = New System.Windows.Forms.GroupBox()
            Me.SelectFolderButton = New System.Windows.Forms.Button()
            Me.RutaTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
            Me.ValidarButton = New System.Windows.Forms.Button()
            Me.RutaGroupBox.SuspendLayout()
            Me.SuspendLayout()
            '
            'RutaGroupBox
            '
            Me.RutaGroupBox.Controls.Add(Me.SelectFolderButton)
            Me.RutaGroupBox.Controls.Add(Me.RutaTextBox)
            Me.RutaGroupBox.Location = New System.Drawing.Point(3, 2)
            Me.RutaGroupBox.Name = "RutaGroupBox"
            Me.RutaGroupBox.Size = New System.Drawing.Size(289, 40)
            Me.RutaGroupBox.TabIndex = 3
            Me.RutaGroupBox.TabStop = False
            Me.RutaGroupBox.Text = "Ruta"
            '
            'SelectFolderButton
            '
            Me.SelectFolderButton.Image = Global.Miharu.Imaging.Indexer.My.Resources.Resources.folder_image
            Me.SelectFolderButton.Location = New System.Drawing.Point(255, 13)
            Me.SelectFolderButton.Name = "SelectFolderButton"
            Me.SelectFolderButton.Size = New System.Drawing.Size(27, 23)
            Me.SelectFolderButton.TabIndex = 1
            Me.SelectFolderButton.UseVisualStyleBackColor = True
            '
            'RutaTextBox
            '
            Me.RutaTextBox._Obligatorio = False
            Me.RutaTextBox._PermitePegar = False
            Me.RutaTextBox.Cantidad_Decimales = CType(0, Short)
            Me.RutaTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.RutaTextBox.DateFormat = Nothing
            Me.RutaTextBox.DisabledEnter = False
            Me.RutaTextBox.DisabledTab = False
            Me.RutaTextBox.EnabledShortCuts = False
            Me.RutaTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.RutaTextBox.FocusOut = System.Drawing.Color.White
            Me.RutaTextBox.Location = New System.Drawing.Point(6, 16)
            Me.RutaTextBox.MaskedTextBox_Property = ""
            Me.RutaTextBox.MaximumLength = CType(0, Short)
            Me.RutaTextBox.MinimumLength = CType(0, Short)
            Me.RutaTextBox.Name = "RutaTextBox"
            Me.RutaTextBox.Obligatorio = False
            Me.RutaTextBox.permitePegar = False
            Rango1.MaxValue = 9.2233720368547758E+18R
            Rango1.MinValue = 0.0R
            Me.RutaTextBox.Rango = Rango1
            Me.RutaTextBox.Size = New System.Drawing.Size(243, 20)
            Me.RutaTextBox.TabIndex = 0
            Me.RutaTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal
            Me.RutaTextBox.Usa_Decimales = False
            Me.RutaTextBox.Validos_Cantidad_Puntos = False
            '
            'ValidarButton
            '
            Me.ValidarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ValidarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ValidarButton.Image = CType(resources.GetObject("ValidarButton.Image"), System.Drawing.Image)
            Me.ValidarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.ValidarButton.Location = New System.Drawing.Point(221, 44)
            Me.ValidarButton.Name = "ValidarButton"
            Me.ValidarButton.Size = New System.Drawing.Size(71, 25)
            Me.ValidarButton.TabIndex = 4
            Me.ValidarButton.Text = "Validar"
            Me.ValidarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.ValidarButton.UseVisualStyleBackColor = True
            '
            'ListValidationControl
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.AutoSize = True
            Me.Controls.Add(Me.ValidarButton)
            Me.Controls.Add(Me.RutaGroupBox)
            Me.Name = "ListValidationControl"
            Me.Size = New System.Drawing.Size(295, 72)
            Me.RutaGroupBox.ResumeLayout(False)
            Me.RutaGroupBox.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents RutaGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents SelectFolderButton As System.Windows.Forms.Button
        Friend WithEvents RutaTextBox As Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl
        Friend WithEvents ValidarButton As System.Windows.Forms.Button

    End Class
End Namespace