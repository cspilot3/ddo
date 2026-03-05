Namespace Progress
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormProgress
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormProgress))
            Me.gbxBase = New System.Windows.Forms.GroupBox()
            Me.lblProgreso = New System.Windows.Forms.Label()
            Me.lblAccion = New System.Windows.Forms.Label()
            Me.lblProceso = New System.Windows.Forms.Label()
            Me.pgbContador = New System.Windows.Forms.ProgressBar()
            Me.btnCancelar = New System.Windows.Forms.Button()
            Me.gbxBase.SuspendLayout()
            Me.SuspendLayout()
            '
            'gbxBase
            '
            Me.gbxBase.Controls.Add(Me.lblProgreso)
            Me.gbxBase.Controls.Add(Me.lblAccion)
            Me.gbxBase.Controls.Add(Me.lblProceso)
            Me.gbxBase.Controls.Add(Me.pgbContador)
            Me.gbxBase.Location = New System.Drawing.Point(12, 10)
            Me.gbxBase.Name = "gbxBase"
            Me.gbxBase.Size = New System.Drawing.Size(360, 122)
            Me.gbxBase.TabIndex = 3
            Me.gbxBase.TabStop = False
            '
            'lblProgreso
            '
            Me.lblProgreso.BackColor = System.Drawing.Color.Transparent
            Me.lblProgreso.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblProgreso.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
            Me.lblProgreso.Location = New System.Drawing.Point(159, 85)
            Me.lblProgreso.Name = "lblProgreso"
            Me.lblProgreso.Size = New System.Drawing.Size(40, 15)
            Me.lblProgreso.TabIndex = 6
            Me.lblProgreso.Text = "100%"
            Me.lblProgreso.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'lblAccion
            '
            Me.lblAccion.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblAccion.Location = New System.Drawing.Point(16, 40)
            Me.lblAccion.Name = "lblAccion"
            Me.lblAccion.Size = New System.Drawing.Size(328, 32)
            Me.lblAccion.TabIndex = 5
            Me.lblAccion.Text = "Acción"
            '
            'lblProceso
            '
            Me.lblProceso.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblProceso.Location = New System.Drawing.Point(16, 13)
            Me.lblProceso.Name = "lblProceso"
            Me.lblProceso.Size = New System.Drawing.Size(328, 24)
            Me.lblProceso.TabIndex = 4
            Me.lblProceso.Text = "Proceso"
            '
            'pgbContador
            '
            Me.pgbContador.Location = New System.Drawing.Point(16, 78)
            Me.pgbContador.MarqueeAnimationSpeed = 30
            Me.pgbContador.Name = "pgbContador"
            Me.pgbContador.Size = New System.Drawing.Size(328, 30)
            Me.pgbContador.TabIndex = 0
            Me.pgbContador.Value = 100
            '
            'btnCancelar
            '
            Me.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.btnCancelar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnCancelar.Image = CType(resources.GetObject("btnCancelar.Image"), System.Drawing.Image)
            Me.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnCancelar.Location = New System.Drawing.Point(156, 140)
            Me.btnCancelar.Name = "btnCancelar"
            Me.btnCancelar.Size = New System.Drawing.Size(72, 24)
            Me.btnCancelar.TabIndex = 2
            Me.btnCancelar.Text = "&Cancelar"
            Me.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'FormProgress
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.btnCancelar
            Me.ClientSize = New System.Drawing.Size(384, 170)
            Me.ControlBox = False
            Me.Controls.Add(Me.gbxBase)
            Me.Controls.Add(Me.btnCancelar)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormProgress"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Progreso"
            Me.TopMost = True
            Me.gbxBase.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents gbxBase As System.Windows.Forms.GroupBox
        Friend WithEvents lblProgreso As System.Windows.Forms.Label
        Friend WithEvents lblAccion As System.Windows.Forms.Label
        Friend WithEvents lblProceso As System.Windows.Forms.Label
        Friend WithEvents pgbContador As System.Windows.Forms.ProgressBar
        Friend WithEvents btnCancelar As System.Windows.Forms.Button
    End Class
End Namespace