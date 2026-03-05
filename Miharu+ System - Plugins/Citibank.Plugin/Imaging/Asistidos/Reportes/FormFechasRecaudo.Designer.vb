<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormFechasRecaudo
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
        Me.lblFechaRecaudoInicio = New System.Windows.Forms.Label()
        Me.gbxBase = New System.Windows.Forms.GroupBox()
        Me.lblFechaRecaudoFin = New System.Windows.Forms.Label()
        Me.dtpFechaFinal = New System.Windows.Forms.DateTimePicker()
        Me.dtpFechaInicial = New System.Windows.Forms.DateTimePicker()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.btnAceptar = New System.Windows.Forms.Button()
        Me.gbxBase.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblFechaRecaudoInicio
        '
        Me.lblFechaRecaudoInicio.AutoSize = True
        Me.lblFechaRecaudoInicio.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFechaRecaudoInicio.Location = New System.Drawing.Point(15, 16)
        Me.lblFechaRecaudoInicio.Name = "lblFechaRecaudoInicio"
        Me.lblFechaRecaudoInicio.Size = New System.Drawing.Size(146, 15)
        Me.lblFechaRecaudoInicio.TabIndex = 1
        Me.lblFechaRecaudoInicio.Text = "Fecha Recaudo Inicio"
        '
        'gbxBase
        '
        Me.gbxBase.Controls.Add(Me.lblFechaRecaudoFin)
        Me.gbxBase.Controls.Add(Me.dtpFechaFinal)
        Me.gbxBase.Controls.Add(Me.lblFechaRecaudoInicio)
        Me.gbxBase.Controls.Add(Me.dtpFechaInicial)
        Me.gbxBase.Location = New System.Drawing.Point(24, 12)
        Me.gbxBase.Name = "gbxBase"
        Me.gbxBase.Size = New System.Drawing.Size(256, 122)
        Me.gbxBase.TabIndex = 18
        Me.gbxBase.TabStop = False
        '
        'lblFechaRecaudoFin
        '
        Me.lblFechaRecaudoFin.AutoSize = True
        Me.lblFechaRecaudoFin.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFechaRecaudoFin.Location = New System.Drawing.Point(15, 68)
        Me.lblFechaRecaudoFin.Name = "lblFechaRecaudoFin"
        Me.lblFechaRecaudoFin.Size = New System.Drawing.Size(131, 15)
        Me.lblFechaRecaudoFin.TabIndex = 3
        Me.lblFechaRecaudoFin.Text = "Fecha Recaudo Fin"
        '
        'dtpFechaFinal
        '
        Me.dtpFechaFinal.Location = New System.Drawing.Point(18, 87)
        Me.dtpFechaFinal.Name = "dtpFechaFinal"
        Me.dtpFechaFinal.Size = New System.Drawing.Size(218, 20)
        Me.dtpFechaFinal.TabIndex = 2
        '
        'dtpFechaInicial
        '
        Me.dtpFechaInicial.Location = New System.Drawing.Point(18, 35)
        Me.dtpFechaInicial.Name = "dtpFechaInicial"
        Me.dtpFechaInicial.Size = New System.Drawing.Size(218, 20)
        Me.dtpFechaInicial.TabIndex = 0
        '
        'btnCancelar
        '
        Me.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancelar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancelar.Location = New System.Drawing.Point(200, 140)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(80, 24)
        Me.btnCancelar.TabIndex = 20
        Me.btnCancelar.Text = "&Cancelar"
        Me.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnCancelar.UseVisualStyleBackColor = True
        '
        'btnAceptar
        '
        Me.btnAceptar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAceptar.Location = New System.Drawing.Point(101, 140)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Size = New System.Drawing.Size(80, 24)
        Me.btnAceptar.TabIndex = 19
        Me.btnAceptar.Text = "&Aceptar"
        Me.btnAceptar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnAceptar.UseVisualStyleBackColor = True
        '
        'FormFechasRecaudo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(292, 176)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.btnAceptar)
        Me.Controls.Add(Me.gbxBase)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "FormFechasRecaudo"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Filtrar Por..."
        Me.gbxBase.ResumeLayout(False)
        Me.gbxBase.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblFechaRecaudoInicio As System.Windows.Forms.Label
    Friend WithEvents gbxBase As System.Windows.Forms.GroupBox
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents btnAceptar As System.Windows.Forms.Button
    Friend WithEvents dtpFechaInicial As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblFechaRecaudoFin As System.Windows.Forms.Label
    Friend WithEvents dtpFechaFinal As System.Windows.Forms.DateTimePicker
End Class
