Namespace Forms.Parametrizacion
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormNuevoCampoLista
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormNuevoCampoLista))
            Me.Label1 = New System.Windows.Forms.Label()
            Me.EntidadTextBox = New System.Windows.Forms.TextBox()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.IdCampoListaTextBox = New System.Windows.Forms.TextBox()
            Me.Label = New System.Windows.Forms.Label()
            Me.NombreCampoListaTextBox = New System.Windows.Forms.TextBox()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.CancelarButton = New System.Windows.Forms.Button()
            Me.AceptarButton = New System.Windows.Forms.Button()
            Me.CampoListaItemDataGridView = New System.Windows.Forms.DataGridView()
            Me.ColumnEtiqueta_Campo_Lista_Item = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnValor_Campo_Lista_Item = New System.Windows.Forms.DataGridViewTextBoxColumn()
            CType(Me.CampoListaItemDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.Location = New System.Drawing.Point(12, 17)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(50, 13)
            Me.Label1.TabIndex = 43
            Me.Label1.Text = "Entidad"
            '
            'EntidadTextBox
            '
            Me.EntidadTextBox.Enabled = False
            Me.EntidadTextBox.Location = New System.Drawing.Point(141, 14)
            Me.EntidadTextBox.Name = "EntidadTextBox"
            Me.EntidadTextBox.Size = New System.Drawing.Size(215, 20)
            Me.EntidadTextBox.TabIndex = 44
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label2.Location = New System.Drawing.Point(12, 43)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(91, 13)
            Me.Label2.TabIndex = 45
            Me.Label2.Text = "Id Campo Lista"
            '
            'IdCampoListaTextBox
            '
            Me.IdCampoListaTextBox.Enabled = False
            Me.IdCampoListaTextBox.Location = New System.Drawing.Point(141, 40)
            Me.IdCampoListaTextBox.Name = "IdCampoListaTextBox"
            Me.IdCampoListaTextBox.Size = New System.Drawing.Size(215, 20)
            Me.IdCampoListaTextBox.TabIndex = 46
            '
            'Label
            '
            Me.Label.AutoSize = True
            Me.Label.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label.Location = New System.Drawing.Point(12, 69)
            Me.Label.Name = "Label"
            Me.Label.Size = New System.Drawing.Size(123, 13)
            Me.Label.TabIndex = 45
            Me.Label.Text = "Nombre Campo Lista"
            '
            'NombreCampoListaTextBox
            '
            Me.NombreCampoListaTextBox.Location = New System.Drawing.Point(141, 66)
            Me.NombreCampoListaTextBox.Name = "NombreCampoListaTextBox"
            Me.NombreCampoListaTextBox.Size = New System.Drawing.Size(215, 20)
            Me.NombreCampoListaTextBox.TabIndex = 46
            '
            'Label3
            '
            Me.Label3.AutoSize = True
            Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label3.Location = New System.Drawing.Point(12, 94)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(110, 13)
            Me.Label3.TabIndex = 50
            Me.Label3.Text = "Items Campo Lista"
            '
            'CancelarButton
            '
            Me.CancelarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CancelarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CancelarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CancelarButton.Image = CType(resources.GetObject("CancelarButton.Image"), System.Drawing.Image)
            Me.CancelarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CancelarButton.Location = New System.Drawing.Point(280, 308)
            Me.CancelarButton.Name = "CancelarButton"
            Me.CancelarButton.Size = New System.Drawing.Size(80, 24)
            Me.CancelarButton.TabIndex = 52
            Me.CancelarButton.Text = "&Cancelar"
            Me.CancelarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'AceptarButton
            '
            Me.AceptarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.AceptarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.AceptarButton.Image = CType(resources.GetObject("AceptarButton.Image"), System.Drawing.Image)
            Me.AceptarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.AceptarButton.Location = New System.Drawing.Point(194, 308)
            Me.AceptarButton.Name = "AceptarButton"
            Me.AceptarButton.Size = New System.Drawing.Size(80, 23)
            Me.AceptarButton.TabIndex = 51
            Me.AceptarButton.Text = "&Aceptar"
            Me.AceptarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'CampoListaItemDataGridView
            '
            Me.CampoListaItemDataGridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CampoListaItemDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.CampoListaItemDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ColumnEtiqueta_Campo_Lista_Item, Me.ColumnValor_Campo_Lista_Item})
            Me.CampoListaItemDataGridView.Location = New System.Drawing.Point(12, 110)
            Me.CampoListaItemDataGridView.Name = "CampoListaItemDataGridView"
            Me.CampoListaItemDataGridView.Size = New System.Drawing.Size(344, 192)
            Me.CampoListaItemDataGridView.TabIndex = 53
            '
            'ColumnEtiqueta_Campo_Lista_Item
            '
            Me.ColumnEtiqueta_Campo_Lista_Item.DataPropertyName = "Etiqueta_Campo_Lista_Item"
            Me.ColumnEtiqueta_Campo_Lista_Item.HeaderText = "Etiqueta"
            Me.ColumnEtiqueta_Campo_Lista_Item.Name = "ColumnEtiqueta_Campo_Lista_Item"
            Me.ColumnEtiqueta_Campo_Lista_Item.Width = 200
            '
            'ColumnValor_Campo_Lista_Item
            '
            Me.ColumnValor_Campo_Lista_Item.DataPropertyName = "Valor_Campo_Lista_Item"
            Me.ColumnValor_Campo_Lista_Item.HeaderText = "Valor"
            Me.ColumnValor_Campo_Lista_Item.Name = "ColumnValor_Campo_Lista_Item"
            '
            'FormNuevoCampoLista
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(372, 342)
            Me.Controls.Add(Me.CampoListaItemDataGridView)
            Me.Controls.Add(Me.CancelarButton)
            Me.Controls.Add(Me.AceptarButton)
            Me.Controls.Add(Me.Label3)
            Me.Controls.Add(Me.NombreCampoListaTextBox)
            Me.Controls.Add(Me.IdCampoListaTextBox)
            Me.Controls.Add(Me.Label)
            Me.Controls.Add(Me.Label2)
            Me.Controls.Add(Me.EntidadTextBox)
            Me.Controls.Add(Me.Label1)
            Me.Name = "FormNuevoCampoLista"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Nuevo Campo Lista"
            CType(Me.CampoListaItemDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents EntidadTextBox As System.Windows.Forms.TextBox
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents IdCampoListaTextBox As System.Windows.Forms.TextBox
        Friend WithEvents Label As System.Windows.Forms.Label
        Friend WithEvents NombreCampoListaTextBox As System.Windows.Forms.TextBox
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents CancelarButton As System.Windows.Forms.Button
        Friend WithEvents AceptarButton As System.Windows.Forms.Button
        Friend WithEvents CampoListaItemDataGridView As System.Windows.Forms.DataGridView
        Friend WithEvents ColumnEtiqueta_Campo_Lista_Item As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnValor_Campo_Lista_Item As System.Windows.Forms.DataGridViewTextBoxColumn
    End Class
End Namespace