Imports Miharu.Desktop.Controls.DesktopComboBox

Namespace Imaging.Forms.Parametrización

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormMatrizDocumento
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
            Me.EsquemaDesktopComboBox = New DesktopComboBoxControl()
            Me.lblEsquema = New System.Windows.Forms.Label()
            Me.LblDocumento = New System.Windows.Forms.Label()
            Me.DocumentoDesktopComboBox = New DesktopComboBoxControl()
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.DataGridViewMatriz = New System.Windows.Forms.DataGridView()
            Me.EliminarButton = New System.Windows.Forms.Button()
            Me.AñadirButton = New System.Windows.Forms.Button()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.GroupBox1.SuspendLayout()
            CType(Me.DataGridViewMatriz, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'EsquemaDesktopComboBox
            '
            Me.EsquemaDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.EsquemaDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.EsquemaDesktopComboBox.DisabledEnter = False
            Me.EsquemaDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.EsquemaDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.EsquemaDesktopComboBox.FormattingEnabled = True
            Me.EsquemaDesktopComboBox.Location = New System.Drawing.Point(90, 19)
            Me.EsquemaDesktopComboBox.Name = "EsquemaDesktopComboBox"
            Me.EsquemaDesktopComboBox.Size = New System.Drawing.Size(279, 21)
            Me.EsquemaDesktopComboBox.TabIndex = 0
            '
            'lblEsquema
            '
            Me.lblEsquema.AutoSize = True
            Me.lblEsquema.Location = New System.Drawing.Point(7, 27)
            Me.lblEsquema.Name = "lblEsquema"
            Me.lblEsquema.Size = New System.Drawing.Size(51, 13)
            Me.lblEsquema.TabIndex = 1
            Me.lblEsquema.Text = "Esquema"
            '
            'LblDocumento
            '
            Me.LblDocumento.AutoSize = True
            Me.LblDocumento.Location = New System.Drawing.Point(7, 60)
            Me.LblDocumento.Name = "LblDocumento"
            Me.LblDocumento.Size = New System.Drawing.Size(62, 13)
            Me.LblDocumento.TabIndex = 2
            Me.LblDocumento.Text = "Documento"
            '
            'DocumentoDesktopComboBox
            '
            Me.DocumentoDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.DocumentoDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.DocumentoDesktopComboBox.DisabledEnter = False
            Me.DocumentoDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.DocumentoDesktopComboBox.Enabled = False
            Me.DocumentoDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.DocumentoDesktopComboBox.FormattingEnabled = True
            Me.DocumentoDesktopComboBox.Location = New System.Drawing.Point(90, 60)
            Me.DocumentoDesktopComboBox.Name = "DocumentoDesktopComboBox"
            Me.DocumentoDesktopComboBox.Size = New System.Drawing.Size(279, 21)
            Me.DocumentoDesktopComboBox.TabIndex = 3
            '
            'GroupBox1
            '
            Me.GroupBox1.Controls.Add(Me.DocumentoDesktopComboBox)
            Me.GroupBox1.Controls.Add(Me.LblDocumento)
            Me.GroupBox1.Controls.Add(Me.lblEsquema)
            Me.GroupBox1.Controls.Add(Me.EsquemaDesktopComboBox)
            Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(391, 95)
            Me.GroupBox1.TabIndex = 4
            Me.GroupBox1.TabStop = False
            '
            'DataGridViewMatriz
            '
            Me.DataGridViewMatriz.AllowUserToAddRows = False
            Me.DataGridViewMatriz.AllowUserToDeleteRows = False
            Me.DataGridViewMatriz.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.DataGridViewMatriz.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.DataGridViewMatriz.Location = New System.Drawing.Point(12, 113)
            Me.DataGridViewMatriz.MultiSelect = False
            Me.DataGridViewMatriz.Name = "DataGridViewMatriz"
            Me.DataGridViewMatriz.ReadOnly = True
            Me.DataGridViewMatriz.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.DataGridViewMatriz.Size = New System.Drawing.Size(756, 259)
            Me.DataGridViewMatriz.TabIndex = 5
            '
            'EliminarButton
            '
            Me.EliminarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.EliminarButton.Image = Global.Banagrario.Plugin.My.Resources.Resources.Cancelar
            Me.EliminarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.EliminarButton.Location = New System.Drawing.Point(108, 378)
            Me.EliminarButton.Name = "EliminarButton"
            Me.EliminarButton.Size = New System.Drawing.Size(90, 30)
            Me.EliminarButton.TabIndex = 37
            Me.EliminarButton.Text = "&Eliminar"
            Me.EliminarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.EliminarButton.UseVisualStyleBackColor = True
            '
            'AñadirButton
            '
            Me.AñadirButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.AñadirButton.Image = Global.Banagrario.Plugin.My.Resources.Resources.btnRecepcion
            Me.AñadirButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.AñadirButton.Location = New System.Drawing.Point(12, 378)
            Me.AñadirButton.Name = "AñadirButton"
            Me.AñadirButton.Size = New System.Drawing.Size(90, 30)
            Me.AñadirButton.TabIndex = 36
            Me.AñadirButton.Text = "&Añadir"
            Me.AñadirButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.AñadirButton.UseVisualStyleBackColor = True
            '
            'CerrarButton
            '
            Me.CerrarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Image = Global.Banagrario.Plugin.My.Resources.Resources.btnSalir
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(678, 378)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(90, 30)
            Me.CerrarButton.TabIndex = 35
            Me.CerrarButton.Text = "&Cerrar"
            Me.CerrarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'FormMatriz_Documento
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(780, 420)
            Me.Controls.Add(Me.EliminarButton)
            Me.Controls.Add(Me.AñadirButton)
            Me.Controls.Add(Me.CerrarButton)
            Me.Controls.Add(Me.DataGridViewMatriz)
            Me.Controls.Add(Me.GroupBox1)
            Me.Name = "FormMatrizDocumento"
            Me.Text = "Parametrización Matriz Documento"
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            CType(Me.DataGridViewMatriz, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents EsquemaDesktopComboBox As DesktopComboBoxControl
        Friend WithEvents lblEsquema As System.Windows.Forms.Label
        Friend WithEvents LblDocumento As System.Windows.Forms.Label
        Friend WithEvents DocumentoDesktopComboBox As DesktopComboBoxControl
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents DataGridViewMatriz As System.Windows.Forms.DataGridView
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents EliminarButton As System.Windows.Forms.Button
        Friend WithEvents AñadirButton As System.Windows.Forms.Button
    End Class
End Namespace