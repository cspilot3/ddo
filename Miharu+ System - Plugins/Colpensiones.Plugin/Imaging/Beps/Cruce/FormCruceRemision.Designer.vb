Namespace Imaging.Beps.Cruce


    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormCruceRemisiones
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormCruceRemisiones))
            Me.CruceButton = New System.Windows.Forms.Button()
            Me.GroupBox2 = New System.Windows.Forms.GroupBox()
            Me.lblRegistros = New System.Windows.Forms.Label()
            Me.Cierrebtn = New System.Windows.Forms.Button()
            Me.GroupBox2.SuspendLayout()
            Me.SuspendLayout()
            '
            'CruceButton
            '
            Me.CruceButton.Image = CType(resources.GetObject("CruceButton.Image"), System.Drawing.Image)
            Me.CruceButton.Location = New System.Drawing.Point(45, 31)
            Me.CruceButton.Name = "CruceButton"
            Me.CruceButton.Size = New System.Drawing.Size(70, 60)
            Me.CruceButton.TabIndex = 0
            Me.CruceButton.Text = "Cruzar"
            Me.CruceButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
            Me.CruceButton.UseVisualStyleBackColor = True
            '
            'GroupBox2
            '
            Me.GroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GroupBox2.Controls.Add(Me.lblRegistros)
            Me.GroupBox2.Controls.Add(Me.Cierrebtn)
            Me.GroupBox2.Controls.Add(Me.CruceButton)
            Me.GroupBox2.Location = New System.Drawing.Point(12, 12)
            Me.GroupBox2.Name = "GroupBox2"
            Me.GroupBox2.Size = New System.Drawing.Size(294, 150)
            Me.GroupBox2.TabIndex = 1
            Me.GroupBox2.TabStop = False
            Me.GroupBox2.Text = "Filtro Cruce Remision"
            '
            'lblRegistros
            '
            Me.lblRegistros.AutoSize = True
            Me.lblRegistros.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblRegistros.Location = New System.Drawing.Point(42, 127)
            Me.lblRegistros.Name = "lblRegistros"
            Me.lblRegistros.Size = New System.Drawing.Size(186, 13)
            Me.lblRegistros.TabIndex = 4
            Me.lblRegistros.Text = "Registros posibles por cruzar: 0"
            '
            'Cierrebtn
            '
            Me.Cierrebtn.Image = CType(resources.GetObject("Cierrebtn.Image"), System.Drawing.Image)
            Me.Cierrebtn.Location = New System.Drawing.Point(176, 31)
            Me.Cierrebtn.Name = "Cierrebtn"
            Me.Cierrebtn.Size = New System.Drawing.Size(70, 60)
            Me.Cierrebtn.TabIndex = 1
            Me.Cierrebtn.Text = "Cerrar"
            Me.Cierrebtn.TextAlign = System.Drawing.ContentAlignment.BottomCenter
            Me.Cierrebtn.UseVisualStyleBackColor = True
            '
            'FormCruceRemisiones
            '
            Me.ClientSize = New System.Drawing.Size(318, 174)
            Me.ControlBox = False
            Me.Controls.Add(Me.GroupBox2)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormCruceRemisiones"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Cruce Remisión"
            Me.GroupBox2.ResumeLayout(False)
            Me.GroupBox2.PerformLayout()
            Me.ResumeLayout(False)

        End Sub

        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents FechaComboBoxControl As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
        Friend WithEvents lblFechaProceso As System.Windows.Forms.Label
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents CruzarButton As System.Windows.Forms.Button
        Friend WithEvents CruceButton As System.Windows.Forms.Button
        Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
        Friend WithEvents Cierrebtn As System.Windows.Forms.Button
        Friend WithEvents lblRegistros As System.Windows.Forms.Label
    End Class

End Namespace
