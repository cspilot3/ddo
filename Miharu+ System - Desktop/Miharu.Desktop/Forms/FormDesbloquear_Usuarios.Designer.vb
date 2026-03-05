Namespace Forms
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormDesbloquear_Usuarios
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
            Me.DesbloquearUserDataGridView = New System.Windows.Forms.DataGridView()
            Me.Column_Desbloquear = New System.Windows.Forms.DataGridViewCheckBoxColumn()
            Me.BtnDesbloquearUsr = New System.Windows.Forms.Button()
            CType(Me.DesbloquearUserDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'DesbloquearUserDataGridView
            '
            Me.DesbloquearUserDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.DesbloquearUserDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column_Desbloquear})
            Me.DesbloquearUserDataGridView.Location = New System.Drawing.Point(12, 31)
            Me.DesbloquearUserDataGridView.Name = "DesbloquearUserDataGridView"
            Me.DesbloquearUserDataGridView.Size = New System.Drawing.Size(643, 269)
            Me.DesbloquearUserDataGridView.TabIndex = 0
            '
            'Column_Desbloquear
            '
            Me.Column_Desbloquear.HeaderText = ""
            Me.Column_Desbloquear.Name = "Column_Desbloquear"
            Me.Column_Desbloquear.Width = 40
            '
            'BtnDesbloquearUsr
            '
            Me.BtnDesbloquearUsr.Location = New System.Drawing.Point(545, 319)
            Me.BtnDesbloquearUsr.Name = "BtnDesbloquearUsr"
            Me.BtnDesbloquearUsr.Size = New System.Drawing.Size(110, 28)
            Me.BtnDesbloquearUsr.TabIndex = 1
            Me.BtnDesbloquearUsr.Text = "Desbloquear"
            Me.BtnDesbloquearUsr.UseVisualStyleBackColor = True
            '
            'FormDesbloquear_Usuarios
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(667, 359)
            Me.Controls.Add(Me.BtnDesbloquearUsr)
            Me.Controls.Add(Me.DesbloquearUserDataGridView)
            Me.Name = "FormDesbloquear_Usuarios"
            Me.Text = "Desbloquear Usuario"
            CType(Me.DesbloquearUserDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents DesbloquearUserDataGridView As System.Windows.Forms.DataGridView
        Friend WithEvents BtnDesbloquearUsr As System.Windows.Forms.Button
        Friend WithEvents Column_Desbloquear As System.Windows.Forms.DataGridViewCheckBoxColumn
    End Class
End Namespace