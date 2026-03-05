Namespace View.Comun

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormAccesosRapidos
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormAccesosRapidos))
            Me.AceptarButton = New System.Windows.Forms.Button
            Me.picicono = New System.Windows.Forms.PictureBox
            Me.lblAcceso = New System.Windows.Forms.Label
            Me.lvAcceso = New System.Windows.Forms.ListView
            Me.chId = New System.Windows.Forms.ColumnHeader
            Me.chAcceso = New System.Windows.Forms.ColumnHeader
            Me.chDescripcion = New System.Windows.Forms.ColumnHeader
            CType(Me.picicono, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'AceptarButton
            '
            Me.AceptarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.AceptarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.AceptarButton.Image = Global.Miharu.Imaging.Indexer.My.Resources.Resources.tick
            Me.AceptarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.AceptarButton.Location = New System.Drawing.Point(452, 327)
            Me.AceptarButton.Name = "AceptarButton"
            Me.AceptarButton.Size = New System.Drawing.Size(72, 24)
            Me.AceptarButton.TabIndex = 9
            Me.AceptarButton.Text = "&Aceptar"
            Me.AceptarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'picicono
            '
            Me.picicono.Image = Global.Miharu.Imaging.Indexer.My.Resources.Resources.lightning_go
            Me.picicono.Location = New System.Drawing.Point(12, 12)
            Me.picicono.Name = "picicono"
            Me.picicono.Size = New System.Drawing.Size(16, 16)
            Me.picicono.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
            Me.picicono.TabIndex = 12
            Me.picicono.TabStop = False
            '
            'lblAcceso
            '
            Me.lblAcceso.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblAcceso.Location = New System.Drawing.Point(34, 12)
            Me.lblAcceso.Name = "lblAcceso"
            Me.lblAcceso.Size = New System.Drawing.Size(128, 16)
            Me.lblAcceso.TabIndex = 10
            Me.lblAcceso.Text = "Accesos"
            '
            'lvAcceso
            '
            Me.lvAcceso.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.chId, Me.chAcceso, Me.chDescripcion})
            Me.lvAcceso.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lvAcceso.FullRowSelect = True
            Me.lvAcceso.HideSelection = False
            Me.lvAcceso.Location = New System.Drawing.Point(9, 34)
            Me.lvAcceso.MultiSelect = False
            Me.lvAcceso.Name = "lvAcceso"
            Me.lvAcceso.Size = New System.Drawing.Size(533, 287)
            Me.lvAcceso.TabIndex = 11
            Me.lvAcceso.UseCompatibleStateImageBehavior = False
            Me.lvAcceso.View = System.Windows.Forms.View.Details
            '
            'chId
            '
            Me.chId.Text = ""
            Me.chId.Width = 30
            '
            'chAcceso
            '
            Me.chAcceso.Text = "Acceso"
            Me.chAcceso.Width = 150
            '
            'chDescripcion
            '
            Me.chDescripcion.Text = "Descripción"
            Me.chDescripcion.Width = 320
            '
            'FormAccesosRapidos
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(554, 363)
            Me.Controls.Add(Me.picicono)
            Me.Controls.Add(Me.lblAcceso)
            Me.Controls.Add(Me.lvAcceso)
            Me.Controls.Add(Me.AceptarButton)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormAccesosRapidos"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Teclas de accesos rapido"
            CType(Me.picicono, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents AceptarButton As System.Windows.Forms.Button
        Friend WithEvents picicono As System.Windows.Forms.PictureBox
        Friend WithEvents lblAcceso As System.Windows.Forms.Label
        Friend WithEvents lvAcceso As System.Windows.Forms.ListView
        Friend WithEvents chId As System.Windows.Forms.ColumnHeader
        Friend WithEvents chAcceso As System.Windows.Forms.ColumnHeader
        Friend WithEvents chDescripcion As System.Windows.Forms.ColumnHeader
    End Class

End Namespace
