Namespace Procesos.Empaque
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormCierreCaja
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
            Me.gbCierreCaja = New System.Windows.Forms.GroupBox()
            Me.btnSalir = New System.Windows.Forms.Button()
            Me.btnCerrar = New System.Windows.Forms.Button()
            Me.dgvCajas = New System.Windows.Forms.DataGridView()
            Me.btnBuscarCaja = New System.Windows.Forms.Button()
            Me.txtCaja = New System.Windows.Forms.TextBox()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.gbCierreCaja.SuspendLayout()
            CType(Me.dgvCajas, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'gbCierreCaja
            '
            Me.gbCierreCaja.Controls.Add(Me.btnSalir)
            Me.gbCierreCaja.Controls.Add(Me.btnCerrar)
            Me.gbCierreCaja.Controls.Add(Me.dgvCajas)
            Me.gbCierreCaja.Controls.Add(Me.btnBuscarCaja)
            Me.gbCierreCaja.Controls.Add(Me.txtCaja)
            Me.gbCierreCaja.Controls.Add(Me.Label1)
            Me.gbCierreCaja.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.gbCierreCaja.Location = New System.Drawing.Point(12, 9)
            Me.gbCierreCaja.Name = "gbCierreCaja"
            Me.gbCierreCaja.Size = New System.Drawing.Size(765, 238)
            Me.gbCierreCaja.TabIndex = 5
            Me.gbCierreCaja.TabStop = False
            Me.gbCierreCaja.Text = "Cerrar Caja"
            '
            'btnSalir
            '
            Me.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnSalir.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.btnSalir
            Me.btnSalir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnSalir.Location = New System.Drawing.Point(446, 188)
            Me.btnSalir.Name = "btnSalir"
            Me.btnSalir.Size = New System.Drawing.Size(99, 38)
            Me.btnSalir.TabIndex = 5
            Me.btnSalir.Text = "Salir"
            Me.btnSalir.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.btnSalir.UseVisualStyleBackColor = True
            '
            'btnCerrar
            '
            Me.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnCerrar.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.Aceptar
            Me.btnCerrar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnCerrar.Location = New System.Drawing.Point(255, 188)
            Me.btnCerrar.Name = "btnCerrar"
            Me.btnCerrar.Size = New System.Drawing.Size(118, 38)
            Me.btnCerrar.TabIndex = 3
            Me.btnCerrar.Text = "Cerrar Caja"
            Me.btnCerrar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.btnCerrar.UseVisualStyleBackColor = True
            '
            'dgvCajas
            '
            Me.dgvCajas.AllowUserToAddRows = False
            Me.dgvCajas.AllowUserToDeleteRows = False
            Me.dgvCajas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
            Me.dgvCajas.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells
            Me.dgvCajas.Location = New System.Drawing.Point(20, 84)
            Me.dgvCajas.Name = "dgvCajas"
            Me.dgvCajas.ReadOnly = True
            Me.dgvCajas.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
            Me.dgvCajas.Size = New System.Drawing.Size(725, 98)
            Me.dgvCajas.TabIndex = 2
            '
            'btnBuscarCaja
            '
            Me.btnBuscarCaja.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnBuscarCaja.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.zoom
            Me.btnBuscarCaja.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnBuscarCaja.Location = New System.Drawing.Point(288, 16)
            Me.btnBuscarCaja.Name = "btnBuscarCaja"
            Me.btnBuscarCaja.Size = New System.Drawing.Size(126, 42)
            Me.btnBuscarCaja.TabIndex = 1
            Me.btnBuscarCaja.Text = "Buscar Caja"
            Me.btnBuscarCaja.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.btnBuscarCaja.UseVisualStyleBackColor = True
            '
            'txtCaja
            '
            Me.txtCaja.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.txtCaja.Location = New System.Drawing.Point(77, 28)
            Me.txtCaja.Name = "txtCaja"
            Me.txtCaja.Size = New System.Drawing.Size(186, 20)
            Me.txtCaja.TabIndex = 0
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(17, 31)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(40, 13)
            Me.Label1.TabIndex = 4
            Me.Label1.Text = "Caja: "
            '
            'FormCierreCaja
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(791, 264)
            Me.Controls.Add(Me.gbCierreCaja)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.Name = "FormCierreCaja"
            Me.ShowIcon = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Empaque - Cierre de Cajas"
            Me.gbCierreCaja.ResumeLayout(False)
            Me.gbCierreCaja.PerformLayout()
            CType(Me.dgvCajas, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents gbCierreCaja As System.Windows.Forms.GroupBox
        Friend WithEvents dgvCajas As System.Windows.Forms.DataGridView
        Friend WithEvents btnBuscarCaja As System.Windows.Forms.Button
        Friend WithEvents txtCaja As System.Windows.Forms.TextBox
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents btnCerrar As System.Windows.Forms.Button
        Friend WithEvents btnSalir As System.Windows.Forms.Button
    End Class

End Namespace


