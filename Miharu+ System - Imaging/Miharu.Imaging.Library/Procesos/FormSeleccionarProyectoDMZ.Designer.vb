Imports Miharu.Desktop.Controls.DesktopDataGridView
Imports Miharu.Desktop.Controls.DesktopComboBox

Namespace Procesos
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormSeleccionarProyectoDMZ
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
            Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormSeleccionarProyectoDMZ))
            Me.Label1 = New System.Windows.Forms.Label()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.CancelarButton = New System.Windows.Forms.Button()
            Me.AceptarButton = New System.Windows.Forms.Button()
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.EntidadComboBox = New DesktopComboBoxControl()
            Me.ProyectoDataGridView = New DesktopDataGridViewControl()
            Me.Id_Proyecto = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Nombre_Proyecto = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.GroupBox1.SuspendLayout()
            CType(Me.ProyectoDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
            Me.Label1.Location = New System.Drawing.Point(9, 16)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(94, 13)
            Me.Label1.TabIndex = 1
            Me.Label1.Text = "Entidad Cliente:"
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
            Me.Label2.Location = New System.Drawing.Point(9, 69)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(61, 13)
            Me.Label2.TabIndex = 2
            Me.Label2.Text = "Proyecto:"
            '
            'CancelarButton
            '
            Me.CancelarButton.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CancelarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CancelarButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
            Me.CancelarButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.CancelarVen
            Me.CancelarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CancelarButton.Location = New System.Drawing.Point(270, 303)
            Me.CancelarButton.Name = "CancelarButton"
            Me.CancelarButton.Size = New System.Drawing.Size(88, 32)
            Me.CancelarButton.TabIndex = 4
            Me.CancelarButton.Text = "&Cancelar"
            Me.CancelarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.CancelarButton.UseVisualStyleBackColor = True
            '
            'AceptarButton
            '
            Me.AceptarButton.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.AceptarButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
            Me.AceptarButton.Image = Global.Miharu.Imaging.Library.My.Resources.Resources.Aceptar
            Me.AceptarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.AceptarButton.Location = New System.Drawing.Point(178, 303)
            Me.AceptarButton.Name = "AceptarButton"
            Me.AceptarButton.Size = New System.Drawing.Size(86, 32)
            Me.AceptarButton.TabIndex = 2
            Me.AceptarButton.Text = "&Aceptar"
            Me.AceptarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.AceptarButton.UseVisualStyleBackColor = True
            '
            'GroupBox1
            '
            Me.GroupBox1.Controls.Add(Me.EntidadComboBox)
            Me.GroupBox1.Controls.Add(Me.ProyectoDataGridView)
            Me.GroupBox1.Controls.Add(Me.Label2)
            Me.GroupBox1.Controls.Add(Me.Label1)
            Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(346, 285)
            Me.GroupBox1.TabIndex = 0
            Me.GroupBox1.TabStop = False
            '
            'EntidadComboBox
            '
            Me.EntidadComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.EntidadComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.EntidadComboBox.DisabledEnter = False
            Me.EntidadComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.EntidadComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.EntidadComboBox.FormattingEnabled = True
            Me.EntidadComboBox.Location = New System.Drawing.Point(12, 32)
            Me.EntidadComboBox.Name = "EntidadComboBox"
            Me.EntidadComboBox.Size = New System.Drawing.Size(317, 21)
            Me.EntidadComboBox.TabIndex = 0
            '
            'ProyectoDataGridView
            '
            Me.ProyectoDataGridView.AllowUserToAddRows = False
            Me.ProyectoDataGridView.AllowUserToDeleteRows = False
            Me.ProyectoDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
            Me.ProyectoDataGridView.BackgroundColor = System.Drawing.Color.White
            DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.DimGray
            DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.ProyectoDataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
            Me.ProyectoDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.ProyectoDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Id_Proyecto, Me.Nombre_Proyecto})
            DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
            DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
            DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightGray
            DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
            DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
            Me.ProyectoDataGridView.DefaultCellStyle = DataGridViewCellStyle2
            Me.ProyectoDataGridView.GridColor = System.Drawing.SystemColors.Control
            Me.ProyectoDataGridView.Location = New System.Drawing.Point(12, 88)
            Me.ProyectoDataGridView.MultiSelect = False
            Me.ProyectoDataGridView.Name = "ProyectoDataGridView"
            Me.ProyectoDataGridView.ReadOnly = True
            DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.LightGray
            DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.ProyectoDataGridView.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
            Me.ProyectoDataGridView.RowHeadersWidth = 20
            Me.ProyectoDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.ProyectoDataGridView.Size = New System.Drawing.Size(317, 181)
            Me.ProyectoDataGridView.TabIndex = 5
            '
            'Id_Proyecto
            '
            Me.Id_Proyecto.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
            Me.Id_Proyecto.DataPropertyName = "Id_Proyecto"
            Me.Id_Proyecto.HeaderText = "Id"
            Me.Id_Proyecto.Name = "Id_Proyecto"
            Me.Id_Proyecto.ReadOnly = True
            Me.Id_Proyecto.Width = 41
            '
            'Nombre_Proyecto
            '
            Me.Nombre_Proyecto.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.Nombre_Proyecto.DataPropertyName = "Nombre_Proyecto"
            Me.Nombre_Proyecto.HeaderText = "Nombre"
            Me.Nombre_Proyecto.Name = "Nombre_Proyecto"
            Me.Nombre_Proyecto.ReadOnly = True
            '
            'FormSeleccionarProyecto
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.SystemColors.Control
            Me.CancelButton = Me.CancelarButton
            Me.ClientSize = New System.Drawing.Size(370, 345)
            Me.Controls.Add(Me.GroupBox1)
            Me.Controls.Add(Me.CancelarButton)
            Me.Controls.Add(Me.AceptarButton)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormSeleccionarProyecto"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Proyecto"
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            CType(Me.ProyectoDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents AceptarButton As System.Windows.Forms.Button
        Friend WithEvents CancelarButton As System.Windows.Forms.Button
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents ProyectoDataGridView As DesktopDataGridViewControl
        Friend WithEvents Id_Proyecto As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents Nombre_Proyecto As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents EntidadComboBox As DesktopComboBoxControl
    End Class
End Namespace