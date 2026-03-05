Namespace Imaging.Beps.Rechazo_Bizagi
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormCausalRechazo
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
            Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormCausalRechazo))
            Me.CausalRechazoDataGridView = New System.Windows.Forms.DataGridView()
            Me.NuevoCausalRechazoButton = New System.Windows.Forms.Button()
            Me.CerrarButton = New System.Windows.Forms.Button()
            CType(Me.CausalRechazoDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'CausalRechazoDataGridView
            '
            Me.CausalRechazoDataGridView.AllowUserToAddRows = False
            Me.CausalRechazoDataGridView.AllowUserToDeleteRows = False
            Me.CausalRechazoDataGridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CausalRechazoDataGridView.BackgroundColor = System.Drawing.Color.White
            DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
            DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
            DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.CausalRechazoDataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
            Me.CausalRechazoDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.CausalRechazoDataGridView.Location = New System.Drawing.Point(12, 12)
            Me.CausalRechazoDataGridView.MultiSelect = False
            Me.CausalRechazoDataGridView.Name = "CausalRechazoDataGridView"
            Me.CausalRechazoDataGridView.ReadOnly = True
            Me.CausalRechazoDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.CausalRechazoDataGridView.Size = New System.Drawing.Size(753, 227)
            Me.CausalRechazoDataGridView.TabIndex = 34
            '
            'NuevoCausalRechazoButton
            '
            Me.NuevoCausalRechazoButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.NuevoCausalRechazoButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.NuevoCausalRechazoButton.Image = CType(resources.GetObject("NuevoCausalRechazoButton.Image"), System.Drawing.Image)
            Me.NuevoCausalRechazoButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.NuevoCausalRechazoButton.Location = New System.Drawing.Point(12, 251)
            Me.NuevoCausalRechazoButton.Name = "NuevoCausalRechazoButton"
            Me.NuevoCausalRechazoButton.Size = New System.Drawing.Size(197, 33)
            Me.NuevoCausalRechazoButton.TabIndex = 35
            Me.NuevoCausalRechazoButton.Text = "Nueva Causal De Rechazo"
            Me.NuevoCausalRechazoButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.NuevoCausalRechazoButton.UseVisualStyleBackColor = True
            '
            'CerrarButton
            '
            Me.CerrarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CerrarButton.Image = Global.Colpensiones.Plugin.My.Resources.Resources.btnSalir
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(624, 254)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(141, 30)
            Me.CerrarButton.TabIndex = 37
            Me.CerrarButton.Text = "&Cerrar"
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'FormCausalRechazo
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(777, 299)
            Me.Controls.Add(Me.CerrarButton)
            Me.Controls.Add(Me.NuevoCausalRechazoButton)
            Me.Controls.Add(Me.CausalRechazoDataGridView)
            Me.Name = "FormCausalRechazo"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Causales Rechazo Destape"
            CType(Me.CausalRechazoDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents CausalRechazoDataGridView As System.Windows.Forms.DataGridView
        Friend WithEvents NuevoCausalRechazoButton As System.Windows.Forms.Button
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents IdRechazoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents DescripcionDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents EliminadoDataGridViewCheckBoxColumn As System.Windows.Forms.DataGridViewCheckBoxColumn
    End Class
End Namespace

