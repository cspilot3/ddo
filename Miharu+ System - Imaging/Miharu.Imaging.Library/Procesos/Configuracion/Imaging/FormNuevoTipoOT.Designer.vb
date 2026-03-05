Imports Miharu.Desktop.Controls.DesktopTextBox

Namespace Procesos.Configuracion.Imaging
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormNuevoTipoOT
        Inherits Miharu.Desktop.Library.FormBase

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
            Dim Rango5 As Rango = New Rango()
            Dim Rango6 As Rango = New Rango()
            Me.GuardarButton = New System.Windows.Forms.Button()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.Label10 = New System.Windows.Forms.Label()
            Me.DescripcionReporteDesktopTextBox = New DesktopTextBoxControl()
            Me.Label4 = New System.Windows.Forms.Label()
            Me.NombreDesktopTextBox = New DesktopTextBoxControl()
            Me.Label11 = New System.Windows.Forms.Label()
            Me.EliminadoCheckBox = New System.Windows.Forms.CheckBox()
            Me.SuspendLayout()
            '
            'GuardarButton
            '
            Me.GuardarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GuardarButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.btnGuardar
            Me.GuardarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.GuardarButton.Location = New System.Drawing.Point(276, 91)
            Me.GuardarButton.Name = "GuardarButton"
            Me.GuardarButton.Size = New System.Drawing.Size(88, 30)
            Me.GuardarButton.TabIndex = 98
            Me.GuardarButton.Text = "&Guardar"
            Me.GuardarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.GuardarButton.UseVisualStyleBackColor = True
            '
            'CerrarButton
            '
            Me.CerrarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.cancel
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(370, 91)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(83, 30)
            Me.CerrarButton.TabIndex = 97
            Me.CerrarButton.Text = "&Cerrar"
            Me.CerrarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'Label10
            '
            Me.Label10.AutoSize = True
            Me.Label10.Location = New System.Drawing.Point(15, 41)
            Me.Label10.Name = "Label10"
            Me.Label10.Size = New System.Drawing.Size(72, 13)
            Me.Label10.TabIndex = 96
            Me.Label10.Text = "Descripción"
            '
            'DescripcionReporteDesktopTextBox
            '
            Me.DescripcionReporteDesktopTextBox.Cantidad_Decimales = CType(0, Short)
            Me.DescripcionReporteDesktopTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.DescripcionReporteDesktopTextBox.DateFormat = Nothing
            Me.DescripcionReporteDesktopTextBox.DisabledEnter = False
            Me.DescripcionReporteDesktopTextBox.DisabledTab = False
            Me.DescripcionReporteDesktopTextBox.EnabledShortCuts = False
            Me.DescripcionReporteDesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.DescripcionReporteDesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.DescripcionReporteDesktopTextBox.Location = New System.Drawing.Point(107, 38)
            Me.DescripcionReporteDesktopTextBox.MaskedTextBox_Property = ""
            Me.DescripcionReporteDesktopTextBox.MaximumLength = CType(0, Short)
            Me.DescripcionReporteDesktopTextBox.MinimumLength = CType(0, Short)
            Me.DescripcionReporteDesktopTextBox.Name = "DescripcionReporteDesktopTextBox"
            Rango5.MaxValue = 2147483647.0R
            Rango5.MinValue = 0.0R
            Me.DescripcionReporteDesktopTextBox.Rango = Rango5
            Me.DescripcionReporteDesktopTextBox.Size = New System.Drawing.Size(346, 21)
            Me.DescripcionReporteDesktopTextBox.TabIndex = 91
            Me.DescripcionReporteDesktopTextBox.Type = DesktopTextBoxControl.TipoTextBox.Normal
            Me.DescripcionReporteDesktopTextBox.Usa_Decimales = False
            Me.DescripcionReporteDesktopTextBox.Validos_Cantidad_Puntos = False
            '
            'Label4
            '
            Me.Label4.AutoSize = True
            Me.Label4.Location = New System.Drawing.Point(15, 15)
            Me.Label4.Name = "Label4"
            Me.Label4.Size = New System.Drawing.Size(51, 13)
            Me.Label4.TabIndex = 86
            Me.Label4.Text = "Nombre"
            '
            'NombreDesktopTextBox
            '
            Me.NombreDesktopTextBox.Cantidad_Decimales = CType(0, Short)
            Me.NombreDesktopTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.NombreDesktopTextBox.DateFormat = Nothing
            Me.NombreDesktopTextBox.DisabledEnter = False
            Me.NombreDesktopTextBox.DisabledTab = False
            Me.NombreDesktopTextBox.EnabledShortCuts = False
            Me.NombreDesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.NombreDesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.NombreDesktopTextBox.Location = New System.Drawing.Point(107, 12)
            Me.NombreDesktopTextBox.MaskedTextBox_Property = ""
            Me.NombreDesktopTextBox.MaximumLength = CType(100, Short)
            Me.NombreDesktopTextBox.MinimumLength = CType(0, Short)
            Me.NombreDesktopTextBox.Name = "NombreDesktopTextBox"
            Rango6.MaxValue = 2147483647.0R
            Rango6.MinValue = 0.0R
            Me.NombreDesktopTextBox.Rango = Rango6
            Me.NombreDesktopTextBox.Size = New System.Drawing.Size(346, 21)
            Me.NombreDesktopTextBox.TabIndex = 85
            Me.NombreDesktopTextBox.Type = DesktopTextBoxControl.TipoTextBox.Normal
            Me.NombreDesktopTextBox.Usa_Decimales = False
            Me.NombreDesktopTextBox.Validos_Cantidad_Puntos = False
            '
            'Label11
            '
            Me.Label11.AutoSize = True
            Me.Label11.Location = New System.Drawing.Point(15, 68)
            Me.Label11.Name = "Label11"
            Me.Label11.Size = New System.Drawing.Size(61, 13)
            Me.Label11.TabIndex = 100
            Me.Label11.Text = "Eliminado"
            '
            'EliminadoCheckBox
            '
            Me.EliminadoCheckBox.AutoSize = True
            Me.EliminadoCheckBox.Location = New System.Drawing.Point(107, 67)
            Me.EliminadoCheckBox.Name = "EliminadoCheckBox"
            Me.EliminadoCheckBox.Size = New System.Drawing.Size(15, 14)
            Me.EliminadoCheckBox.TabIndex = 99
            Me.EliminadoCheckBox.UseVisualStyleBackColor = True
            '
            'FormNuevoTipoOT
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(462, 133)
            Me.Controls.Add(Me.Label11)
            Me.Controls.Add(Me.EliminadoCheckBox)
            Me.Controls.Add(Me.GuardarButton)
            Me.Controls.Add(Me.CerrarButton)
            Me.Controls.Add(Me.Label10)
            Me.Controls.Add(Me.DescripcionReporteDesktopTextBox)
            Me.Controls.Add(Me.Label4)
            Me.Controls.Add(Me.NombreDesktopTextBox)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormNuevoTipoOT"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Nuevo Tipo OT"
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents GuardarButton As System.Windows.Forms.Button
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents Label10 As System.Windows.Forms.Label
        Friend WithEvents DescripcionReporteDesktopTextBox As DesktopTextBoxControl
        Friend WithEvents Label4 As System.Windows.Forms.Label
        Friend WithEvents NombreDesktopTextBox As DesktopTextBoxControl
        Friend WithEvents Label11 As System.Windows.Forms.Label
        Friend WithEvents EliminadoCheckBox As System.Windows.Forms.CheckBox
    End Class
End Namespace