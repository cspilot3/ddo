Imports Miharu.Desktop.Controls.DesktopTextTextBox

Namespace Reportes

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormPrecinto
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
            Me.gbxBase = New System.Windows.Forms.GroupBox()
            Me.dtPrecinto = New DesktopTextTextBoxControl()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.UsuarioLabel = New System.Windows.Forms.Label()
            Me.lblFechaInicial = New System.Windows.Forms.Label()
            Me.dtpFechaInicial = New System.Windows.Forms.DateTimePicker()
            Me.dtpFechaFinal = New System.Windows.Forms.DateTimePicker()
            Me.lblFechaFinal = New System.Windows.Forms.Label()
            Me.btnCancelar = New System.Windows.Forms.Button()
            Me.btnAceptar = New System.Windows.Forms.Button()
            Me.dntOT = New DesktopTextTextBoxControl()
            Me.gbxBase.SuspendLayout()
            Me.SuspendLayout()
            '
            'gbxBase
            '
            Me.gbxBase.Controls.Add(Me.dntOT)
            Me.gbxBase.Controls.Add(Me.dtPrecinto)
            Me.gbxBase.Controls.Add(Me.Label1)
            Me.gbxBase.Controls.Add(Me.UsuarioLabel)
            Me.gbxBase.Controls.Add(Me.lblFechaInicial)
            Me.gbxBase.Controls.Add(Me.dtpFechaInicial)
            Me.gbxBase.Controls.Add(Me.dtpFechaFinal)
            Me.gbxBase.Controls.Add(Me.lblFechaFinal)
            Me.gbxBase.Location = New System.Drawing.Point(12, 12)
            Me.gbxBase.Name = "gbxBase"
            Me.gbxBase.Size = New System.Drawing.Size(256, 238)
            Me.gbxBase.TabIndex = 20
            Me.gbxBase.TabStop = False
            '
            'dtPrecinto
            '
            Me.dtPrecinto.AllowPromptAsInput = False
            Me.dtPrecinto.FocusIn = System.Drawing.Color.LightYellow
            Me.dtPrecinto.FocusOut = System.Drawing.Color.White
            Me.dtPrecinto.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.dtPrecinto.Location = New System.Drawing.Point(18, 200)
            Me.dtPrecinto.MinLength = 0
            Me.dtPrecinto.Name = "dtPrecinto"
            Me.dtPrecinto.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
            Me.dtPrecinto.ResetOnPrompt = False
            Me.dtPrecinto.ResetOnSpace = False
            Me.dtPrecinto.Size = New System.Drawing.Size(218, 20)
            Me.dtPrecinto.TabIndex = 22
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.Location = New System.Drawing.Point(15, 181)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(60, 15)
            Me.Label1.TabIndex = 21
            Me.Label1.Text = "Precinto"
            '
            'UsuarioLabel
            '
            Me.UsuarioLabel.AutoSize = True
            Me.UsuarioLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.UsuarioLabel.Location = New System.Drawing.Point(15, 126)
            Me.UsuarioLabel.Name = "UsuarioLabel"
            Me.UsuarioLabel.Size = New System.Drawing.Size(25, 15)
            Me.UsuarioLabel.TabIndex = 19
            Me.UsuarioLabel.Text = "OT"
            '
            'lblFechaInicial
            '
            Me.lblFechaInicial.AutoSize = True
            Me.lblFechaInicial.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblFechaInicial.Location = New System.Drawing.Point(15, 16)
            Me.lblFechaInicial.Name = "lblFechaInicial"
            Me.lblFechaInicial.Size = New System.Drawing.Size(145, 15)
            Me.lblFechaInicial.TabIndex = 1
            Me.lblFechaInicial.Text = "Fecha Proceso Inicial"
            '
            'dtpFechaInicial
            '
            Me.dtpFechaInicial.Location = New System.Drawing.Point(18, 35)
            Me.dtpFechaInicial.Name = "dtpFechaInicial"
            Me.dtpFechaInicial.Size = New System.Drawing.Size(218, 20)
            Me.dtpFechaInicial.TabIndex = 0
            '
            'dtpFechaFinal
            '
            Me.dtpFechaFinal.Location = New System.Drawing.Point(18, 87)
            Me.dtpFechaFinal.Name = "dtpFechaFinal"
            Me.dtpFechaFinal.Size = New System.Drawing.Size(218, 20)
            Me.dtpFechaFinal.TabIndex = 2
            '
            'lblFechaFinal
            '
            Me.lblFechaFinal.AutoSize = True
            Me.lblFechaFinal.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblFechaFinal.Location = New System.Drawing.Point(15, 68)
            Me.lblFechaFinal.Name = "lblFechaFinal"
            Me.lblFechaFinal.Size = New System.Drawing.Size(138, 15)
            Me.lblFechaFinal.TabIndex = 3
            Me.lblFechaFinal.Text = "Fecha Proceso Final"
            '
            'btnCancelar
            '
            Me.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.btnCancelar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnCancelar.Location = New System.Drawing.Point(168, 266)
            Me.btnCancelar.Name = "btnCancelar"
            Me.btnCancelar.Size = New System.Drawing.Size(80, 24)
            Me.btnCancelar.TabIndex = 19
            Me.btnCancelar.Text = "&Cancelar"
            Me.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.btnCancelar.UseVisualStyleBackColor = True
            '
            'btnAceptar
            '
            Me.btnAceptar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnAceptar.Location = New System.Drawing.Point(69, 266)
            Me.btnAceptar.Name = "btnAceptar"
            Me.btnAceptar.Size = New System.Drawing.Size(80, 24)
            Me.btnAceptar.TabIndex = 18
            Me.btnAceptar.Text = "&Aceptar"
            Me.btnAceptar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.btnAceptar.UseVisualStyleBackColor = True
            '
            'dntOT
            '
            Me.dntOT.AllowPromptAsInput = False
            Me.dntOT.FocusIn = System.Drawing.Color.LightYellow
            Me.dntOT.FocusOut = System.Drawing.Color.White
            Me.dntOT.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.dntOT.Location = New System.Drawing.Point(18, 144)
            Me.dntOT.MinLength = 0
            Me.dntOT.Name = "dntOT"
            Me.dntOT.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
            Me.dntOT.ResetOnPrompt = False
            Me.dntOT.ResetOnSpace = False
            Me.dntOT.Size = New System.Drawing.Size(218, 20)
            Me.dntOT.TabIndex = 23
            '
            'FormPrecinto
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(277, 302)
            Me.ControlBox = False
            Me.Controls.Add(Me.gbxBase)
            Me.Controls.Add(Me.btnCancelar)
            Me.Controls.Add(Me.btnAceptar)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormPrecinto"
            Me.ShowIcon = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Filtrar por ..."
            Me.gbxBase.ResumeLayout(False)
            Me.gbxBase.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents gbxBase As System.Windows.Forms.GroupBox
        Friend WithEvents UsuarioLabel As System.Windows.Forms.Label
        Friend WithEvents lblFechaInicial As System.Windows.Forms.Label
        Friend WithEvents dtpFechaInicial As System.Windows.Forms.DateTimePicker
        Friend WithEvents dtpFechaFinal As System.Windows.Forms.DateTimePicker
        Friend WithEvents lblFechaFinal As System.Windows.Forms.Label
        Friend WithEvents btnCancelar As System.Windows.Forms.Button
        Friend WithEvents btnAceptar As System.Windows.Forms.Button
        Friend WithEvents dtPrecinto As DesktopTextTextBoxControl
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents dntOT As DesktopTextTextBoxControl
    End Class

End Namespace