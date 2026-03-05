Imports Miharu.Desktop.Controls.DesktopComboBox

Namespace Risk.Forms.Recepcion

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormRecepcionPrecintos
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
            Me.components = New System.ComponentModel.Container()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.OficinaComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.ReporteButton = New System.Windows.Forms.Button()
            Me.EliminarPrecintoButton = New System.Windows.Forms.Button()
            Me.Label4 = New System.Windows.Forms.Label()
            Me.SedeComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.PrecintosDataGridView = New System.Windows.Forms.DataGridView()
            Me.CodigoPrecintoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.PrecintosBindingSource = New System.Windows.Forms.BindingSource(Me.components)
            Me.PluginsTables = New Banagrario.Plugin.PluginsTables()
            Me.OTComboBox = New Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.GroupBox1.SuspendLayout()
            CType(Me.PrecintosDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.PrecintosBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.PluginsTables, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'CerrarButton
            '
            Me.CerrarButton.CausesValidation = False
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Image = Global.Banagrario.Plugin.My.Resources.Resources.btnSalir
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(165, 448)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(92, 30)
            Me.CerrarButton.TabIndex = 4
            Me.CerrarButton.Text = "     &Cerrar"
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'GroupBox1
            '
            Me.GroupBox1.Controls.Add(Me.OficinaComboBox)
            Me.GroupBox1.Controls.Add(Me.Label3)
            Me.GroupBox1.Controls.Add(Me.ReporteButton)
            Me.GroupBox1.Controls.Add(Me.EliminarPrecintoButton)
            Me.GroupBox1.Controls.Add(Me.Label4)
            Me.GroupBox1.Controls.Add(Me.SedeComboBox)
            Me.GroupBox1.Controls.Add(Me.PrecintosDataGridView)
            Me.GroupBox1.Controls.Add(Me.OTComboBox)
            Me.GroupBox1.Controls.Add(Me.CerrarButton)
            Me.GroupBox1.Controls.Add(Me.Label2)
            Me.GroupBox1.Controls.Add(Me.Label1)
            Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(338, 492)
            Me.GroupBox1.TabIndex = 1
            Me.GroupBox1.TabStop = False
            '
            'OficinaComboBox
            '
            Me.OficinaComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.OficinaComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.OficinaComboBox.DisabledEnter = True
            Me.OficinaComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.OficinaComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.OficinaComboBox.FormattingEnabled = True
            Me.OficinaComboBox.Location = New System.Drawing.Point(90, 96)
            Me.OficinaComboBox.Name = "OficinaComboBox"
            Me.OficinaComboBox.Size = New System.Drawing.Size(230, 21)
            Me.OficinaComboBox.TabIndex = 58
            '
            'Label3
            '
            Me.Label3.AutoSize = True
            Me.Label3.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label3.Location = New System.Drawing.Point(19, 94)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(65, 19)
            Me.Label3.TabIndex = 57
            Me.Label3.Text = "Oficina"
            '
            'ReporteButton
            '
            Me.ReporteButton.Image = Global.Banagrario.Plugin.My.Resources.Resources.btnInsert
            Me.ReporteButton.Location = New System.Drawing.Point(277, 444)
            Me.ReporteButton.Name = "ReporteButton"
            Me.ReporteButton.Size = New System.Drawing.Size(43, 38)
            Me.ReporteButton.TabIndex = 56
            Me.ReporteButton.UseVisualStyleBackColor = True
            '
            'EliminarPrecintoButton
            '
            Me.EliminarPrecintoButton.CausesValidation = False
            Me.EliminarPrecintoButton.Image = Global.Banagrario.Plugin.My.Resources.Resources.Cancelar
            Me.EliminarPrecintoButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.EliminarPrecintoButton.Location = New System.Drawing.Point(21, 448)
            Me.EliminarPrecintoButton.Name = "EliminarPrecintoButton"
            Me.EliminarPrecintoButton.Size = New System.Drawing.Size(138, 30)
            Me.EliminarPrecintoButton.TabIndex = 55
            Me.EliminarPrecintoButton.Text = "     &Eliminar precinto"
            Me.EliminarPrecintoButton.UseVisualStyleBackColor = True
            '
            'Label4
            '
            Me.Label4.AutoSize = True
            Me.Label4.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label4.Location = New System.Drawing.Point(19, 22)
            Me.Label4.Name = "Label4"
            Me.Label4.Size = New System.Drawing.Size(65, 19)
            Me.Label4.TabIndex = 54
            Me.Label4.Text = "Ciudad"
            '
            'SedeComboBox
            '
            Me.SedeComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.SedeComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.SedeComboBox.DisabledEnter = False
            Me.SedeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.SedeComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.SedeComboBox.FormattingEnabled = True
            Me.SedeComboBox.Location = New System.Drawing.Point(91, 20)
            Me.SedeComboBox.Name = "SedeComboBox"
            Me.SedeComboBox.Size = New System.Drawing.Size(230, 21)
            Me.SedeComboBox.TabIndex = 52
            '
            'PrecintosDataGridView
            '
            Me.PrecintosDataGridView.AllowUserToDeleteRows = False
            Me.PrecintosDataGridView.AutoGenerateColumns = False
            Me.PrecintosDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.PrecintosDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.CodigoPrecintoDataGridViewTextBoxColumn})
            Me.PrecintosDataGridView.DataSource = Me.PrecintosBindingSource
            Me.PrecintosDataGridView.Location = New System.Drawing.Point(21, 152)
            Me.PrecintosDataGridView.MultiSelect = False
            Me.PrecintosDataGridView.Name = "PrecintosDataGridView"
            Me.PrecintosDataGridView.Size = New System.Drawing.Size(300, 286)
            Me.PrecintosDataGridView.TabIndex = 2
            '
            'CodigoPrecintoDataGridViewTextBoxColumn
            '
            Me.CodigoPrecintoDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.CodigoPrecintoDataGridViewTextBoxColumn.DataPropertyName = "Codigo_Precinto"
            Me.CodigoPrecintoDataGridViewTextBoxColumn.HeaderText = "Codigo"
            Me.CodigoPrecintoDataGridViewTextBoxColumn.Name = "CodigoPrecintoDataGridViewTextBoxColumn"
            '
            'PrecintosBindingSource
            '
            Me.PrecintosBindingSource.DataMember = "Precintos"
            Me.PrecintosBindingSource.DataSource = Me.PluginsTables
            '
            'PluginsTables
            '
            Me.PluginsTables.DataSetName = "PluginsTables"
            Me.PluginsTables.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
            '
            'OTComboBox
            '
            Me.OTComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.OTComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.OTComboBox.DisabledEnter = True
            Me.OTComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.OTComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.OTComboBox.FormattingEnabled = True
            Me.OTComboBox.Location = New System.Drawing.Point(91, 59)
            Me.OTComboBox.Name = "OTComboBox"
            Me.OTComboBox.Size = New System.Drawing.Size(230, 21)
            Me.OTComboBox.TabIndex = 1
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label2.Location = New System.Drawing.Point(19, 130)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(85, 19)
            Me.Label2.TabIndex = 1
            Me.Label2.Text = "Precintos"
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.Location = New System.Drawing.Point(19, 57)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(56, 19)
            Me.Label1.TabIndex = 0
            Me.Label1.Text = "Fecha"
            '
            'FormRecepcionPrecintos
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CausesValidation = False
            Me.ClientSize = New System.Drawing.Size(364, 516)
            Me.Controls.Add(Me.GroupBox1)
            Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.KeyPreview = True
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormRecepcionPrecintos"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Recepcion de unidades de carga sellada"
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            CType(Me.PrecintosDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.PrecintosBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.PluginsTables, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents OTComboBox As DesktopComboBoxControl
        Friend WithEvents PrecintosDataGridView As System.Windows.Forms.DataGridView
        Friend WithEvents CodigoPrecintoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents PrecintosBindingSource As System.Windows.Forms.BindingSource
        Friend WithEvents PluginsTables As Banagrario.Plugin.PluginsTables
        Friend WithEvents Label4 As System.Windows.Forms.Label
        Friend WithEvents SedeComboBox As DesktopComboBoxControl
        Friend WithEvents EliminarPrecintoButton As System.Windows.Forms.Button
        Friend WithEvents ReporteButton As System.Windows.Forms.Button
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents OficinaComboBox As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl
    End Class
End Namespace