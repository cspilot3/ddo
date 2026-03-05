Imports Miharu.Desktop.Controls.DesktopDataGridView

Namespace Forms.CentroDistribucion
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormRemisionCiudades
        Inherits Miharu.Desktop.Library.FormBase

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
            Me.GridRemisiones = New System.Windows.Forms.DataGridView()
            Me.gbxRemisiones = New System.Windows.Forms.GroupBox()
            Me.btnCrearRemision = New System.Windows.Forms.Button()
            Me.btnAceptar = New System.Windows.Forms.Button()
            Me.btnCerrarRemision = New System.Windows.Forms.Button()
            CType(Me.GridRemisiones, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.gbxRemisiones.SuspendLayout()
            Me.SuspendLayout()
            '
            'GridRemisiones
            '
            Me.GridRemisiones.AllowUserToAddRows = False
            Me.GridRemisiones.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.GridRemisiones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.GridRemisiones.Location = New System.Drawing.Point(7, 19)
            Me.GridRemisiones.Name = "GridRemisiones"
            Me.GridRemisiones.ReadOnly = True
            Me.GridRemisiones.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.GridRemisiones.Size = New System.Drawing.Size(745, 150)
            Me.GridRemisiones.TabIndex = 0
            '
            'gbxRemisiones
            '
            Me.gbxRemisiones.Controls.Add(Me.GridRemisiones)
            Me.gbxRemisiones.Location = New System.Drawing.Point(14, 12)
            Me.gbxRemisiones.Name = "gbxRemisiones"
            Me.gbxRemisiones.Size = New System.Drawing.Size(759, 179)
            Me.gbxRemisiones.TabIndex = 1
            Me.gbxRemisiones.TabStop = False
            Me.gbxRemisiones.Text = "Remisiones"
            '
            'btnCrearRemision
            '
            Me.btnCrearRemision.Location = New System.Drawing.Point(14, 211)
            Me.btnCrearRemision.Name = "btnCrearRemision"
            Me.btnCrearRemision.Size = New System.Drawing.Size(121, 23)
            Me.btnCrearRemision.TabIndex = 2
            Me.btnCrearRemision.Text = "Crear Remisión"
            Me.btnCrearRemision.UseVisualStyleBackColor = True
            '
            'btnAceptar
            '
            Me.btnAceptar.Image = Global.Miharu.Risk.Library.My.Resources.Resources.Aceptar
            Me.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnAceptar.Location = New System.Drawing.Point(157, 211)
            Me.btnAceptar.Name = "btnAceptar"
            Me.btnAceptar.Size = New System.Drawing.Size(87, 23)
            Me.btnAceptar.TabIndex = 3
            Me.btnAceptar.Text = "&Aceptar"
            Me.btnAceptar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.btnAceptar.UseVisualStyleBackColor = True
            '
            'btnCerrarRemision
            '
            Me.btnCerrarRemision.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnPrinter
            Me.btnCerrarRemision.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.btnCerrarRemision.Location = New System.Drawing.Point(528, 211)
            Me.btnCerrarRemision.Name = "btnCerrarRemision"
            Me.btnCerrarRemision.Size = New System.Drawing.Size(127, 23)
            Me.btnCerrarRemision.TabIndex = 4
            Me.btnCerrarRemision.Text = "&Cerrar Remisión"
            Me.btnCerrarRemision.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnCerrarRemision.UseVisualStyleBackColor = True
            '
            'FormRemisionCiudades
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(787, 245)
            Me.Controls.Add(Me.btnCerrarRemision)
            Me.Controls.Add(Me.btnAceptar)
            Me.Controls.Add(Me.btnCrearRemision)
            Me.Controls.Add(Me.gbxRemisiones)
            Me.Name = "FormRemisionCiudades"
            Me.Text = "Bandeja Distribución"
            CType(Me.GridRemisiones, System.ComponentModel.ISupportInitialize).EndInit()
            Me.gbxRemisiones.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents GridRemisiones As System.Windows.Forms.DataGridView
        Friend WithEvents gbxRemisiones As System.Windows.Forms.GroupBox
        Friend WithEvents btnCrearRemision As System.Windows.Forms.Button
        Friend WithEvents btnAceptar As System.Windows.Forms.Button
        Friend WithEvents btnCerrarRemision As System.Windows.Forms.Button
    End Class
End Namespace

