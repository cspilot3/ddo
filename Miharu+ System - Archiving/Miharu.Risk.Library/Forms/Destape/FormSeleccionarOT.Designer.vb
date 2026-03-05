Imports Miharu.Desktop.Controls.DesktopTextBox

Namespace Forms.Destape
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormSeleccionarOT
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
            Dim Rango1 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormSeleccionarOT))
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.OTDataGridView = New System.Windows.Forms.DataGridView()
            Me.id_OT = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Fecha_OT = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.CerrarOTButton = New System.Windows.Forms.Button()
            Me.CancelarButton = New System.Windows.Forms.Button()
            Me.AceptarButton = New System.Windows.Forms.Button()
            Me.BuscarButton = New System.Windows.Forms.Button()
            Me.OtBuscarDesktopTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
            Me.GroupBox1.SuspendLayout()
            CType(Me.OTDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'GroupBox1
            '
            Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GroupBox1.Controls.Add(Me.OTDataGridView)
            Me.GroupBox1.Controls.Add(Me.CerrarOTButton)
            Me.GroupBox1.Controls.Add(Me.CancelarButton)
            Me.GroupBox1.Controls.Add(Me.AceptarButton)
            Me.GroupBox1.Controls.Add(Me.BuscarButton)
            Me.GroupBox1.Controls.Add(Me.OtBuscarDesktopTextBox)
            Me.GroupBox1.Location = New System.Drawing.Point(2, 3)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(333, 301)
            Me.GroupBox1.TabIndex = 1
            Me.GroupBox1.TabStop = False
            Me.GroupBox1.Text = "Ordenes de Trabajo"
            '
            'OTDataGridView
            '
            Me.OTDataGridView.AllowUserToAddRows = False
            Me.OTDataGridView.AllowUserToDeleteRows = False
            Me.OTDataGridView.AllowUserToResizeColumns = False
            Me.OTDataGridView.AllowUserToResizeRows = False
            Me.OTDataGridView.BackgroundColor = System.Drawing.Color.White
            Me.OTDataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
            Me.OTDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
            Me.OTDataGridView.ColumnHeadersVisible = False
            Me.OTDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.id_OT, Me.Fecha_OT})
            Me.OTDataGridView.Location = New System.Drawing.Point(10, 56)
            Me.OTDataGridView.MultiSelect = False
            Me.OTDataGridView.Name = "OTDataGridView"
            Me.OTDataGridView.ReadOnly = True
            Me.OTDataGridView.RowHeadersVisible = False
            Me.OTDataGridView.RowHeadersWidth = 4
            Me.OTDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.OTDataGridView.Size = New System.Drawing.Size(311, 186)
            Me.OTDataGridView.TabIndex = 6
            '
            'id_OT
            '
            Me.id_OT.DataPropertyName = "id_OT"
            Me.id_OT.HeaderText = "OT"
            Me.id_OT.Name = "id_OT"
            Me.id_OT.ReadOnly = True
            Me.id_OT.Width = 237
            '
            'Fecha_OT
            '
            Me.Fecha_OT.DataPropertyName = "Fecha_OT"
            Me.Fecha_OT.HeaderText = "Fecha_OTColumn"
            Me.Fecha_OT.Name = "Fecha_OT"
            Me.Fecha_OT.ReadOnly = True
            Me.Fecha_OT.Visible = False
            '
            'CerrarOTButton
            '
            Me.CerrarOTButton.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CerrarOTButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
            Me.CerrarOTButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnSalir1
            Me.CerrarOTButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarOTButton.Location = New System.Drawing.Point(11, 262)
            Me.CerrarOTButton.Name = "CerrarOTButton"
            Me.CerrarOTButton.Size = New System.Drawing.Size(102, 32)
            Me.CerrarOTButton.TabIndex = 5
            Me.CerrarOTButton.Text = "&Cerrar OT"
            Me.CerrarOTButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CerrarOTButton.UseVisualStyleBackColor = True
            '
            'CancelarButton
            '
            Me.CancelarButton.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CancelarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CancelarButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
            Me.CancelarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnSalir
            Me.CancelarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CancelarButton.Location = New System.Drawing.Point(241, 262)
            Me.CancelarButton.Name = "CancelarButton"
            Me.CancelarButton.Size = New System.Drawing.Size(75, 32)
            Me.CancelarButton.TabIndex = 4
            Me.CancelarButton.Text = "&Cerrar"
            Me.CancelarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CancelarButton.UseVisualStyleBackColor = True
            '
            'AceptarButton
            '
            Me.AceptarButton.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.AceptarButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
            Me.AceptarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.Aceptar
            Me.AceptarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.AceptarButton.Location = New System.Drawing.Point(152, 262)
            Me.AceptarButton.Name = "AceptarButton"
            Me.AceptarButton.Size = New System.Drawing.Size(78, 32)
            Me.AceptarButton.TabIndex = 3
            Me.AceptarButton.Text = "&Aceptar"
            Me.AceptarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.AceptarButton.UseVisualStyleBackColor = True
            '
            'BuscarButton
            '
            Me.BuscarButton.Image = Global.Miharu.Risk.Library.My.Resources.Resources.btnBuscar
            Me.BuscarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.BuscarButton.Location = New System.Drawing.Point(241, 29)
            Me.BuscarButton.Name = "BuscarButton"
            Me.BuscarButton.Size = New System.Drawing.Size(80, 24)
            Me.BuscarButton.TabIndex = 1
            Me.BuscarButton.Text = "Buscar"
            Me.BuscarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.BuscarButton.UseVisualStyleBackColor = True
            '
            'OtBuscarDesktopTextBox
            '
            Me.OtBuscarDesktopTextBox._Obligatorio = False
            Me.OtBuscarDesktopTextBox._PermitePegar = False
            Me.OtBuscarDesktopTextBox.Cantidad_Decimales = CType(2, Short)
            Me.OtBuscarDesktopTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.OtBuscarDesktopTextBox.DateFormat = Nothing
            Me.OtBuscarDesktopTextBox.DisabledEnter = True
            Me.OtBuscarDesktopTextBox.DisabledTab = False
            Me.OtBuscarDesktopTextBox.EnabledShortCuts = False
            Me.OtBuscarDesktopTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.OtBuscarDesktopTextBox.FocusOut = System.Drawing.Color.White
            Me.OtBuscarDesktopTextBox.Location = New System.Drawing.Point(10, 29)
            Me.OtBuscarDesktopTextBox.MaskedTextBox_Property = ""
            Me.OtBuscarDesktopTextBox.MaximumLength = CType(0, Short)
            Me.OtBuscarDesktopTextBox.MinimumLength = CType(0, Short)
            Me.OtBuscarDesktopTextBox.Name = "OtBuscarDesktopTextBox"
            Me.OtBuscarDesktopTextBox.Obligatorio = False
            Me.OtBuscarDesktopTextBox.permitePegar = False
            Rango1.MaxValue = 2147483647.0R
            Rango1.MinValue = 0.0R
            Me.OtBuscarDesktopTextBox.Rango = Rango1
            Me.OtBuscarDesktopTextBox.Size = New System.Drawing.Size(225, 21)
            Me.OtBuscarDesktopTextBox.TabIndex = 0
            Me.OtBuscarDesktopTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal
            Me.OtBuscarDesktopTextBox.Usa_Decimales = False
            Me.OtBuscarDesktopTextBox.Validos_Cantidad_Puntos = False
            '
            'FormSeleccionarOT
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.CancelarButton
            Me.ClientSize = New System.Drawing.Size(339, 308)
            Me.Controls.Add(Me.GroupBox1)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormSeleccionarOT"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "OTs"
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            CType(Me.OTDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents OtBuscarDesktopTextBox As DesktopTextBoxControl
        Friend WithEvents BuscarButton As System.Windows.Forms.Button
        Friend WithEvents CancelarButton As System.Windows.Forms.Button
        Friend WithEvents AceptarButton As System.Windows.Forms.Button
        Friend WithEvents CerrarOTButton As System.Windows.Forms.Button
        Friend WithEvents OTDataGridView As System.Windows.Forms.DataGridView
        Friend WithEvents id_OT As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Fecha_OT As System.Windows.Forms.DataGridViewTextBoxColumn
    End Class
End Namespace