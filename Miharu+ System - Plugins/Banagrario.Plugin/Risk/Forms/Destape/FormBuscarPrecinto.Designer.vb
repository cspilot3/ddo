Imports Miharu.Desktop.Controls.DesktopTextBox
Imports Miharu.Desktop.Controls.DesktopComboBox

Namespace Risk.Forms.Destape
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormBuscarPrecinto
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
            Dim Rango2 As Rango = New Rango()
            Me.PrecintoGroupBox = New System.Windows.Forms.GroupBox()
            Me.OficinaPanel = New System.Windows.Forms.Panel()
            Me.NombreOficina1ComboBox = New DesktopComboBoxControl()
            Me.CodOficina1ComboBox = New DesktopComboBoxControl()
            Me.Label6 = New System.Windows.Forms.Label()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.PrecintoTextBox = New DesktopTextBoxControl()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.BotonesGroupBox = New System.Windows.Forms.GroupBox()
            Me.BotonesPanel = New System.Windows.Forms.Panel()
            Me.InformeButton = New System.Windows.Forms.Button()
            Me.BuscarButton = New System.Windows.Forms.Button()
            Me.CerrarButton = New System.Windows.Forms.Button()
            Me.ContenedoresGroupBox = New System.Windows.Forms.GroupBox()
            Me.Panel2 = New System.Windows.Forms.Panel()
            Me.ContenedoresDataGridView = New System.Windows.Forms.DataGridView()
            Me.CiudadDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.CodigoOficinaDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.NombreOficinaDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.FechaProcesoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.IdPrecintoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ContenedoresDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.FechaRecepcionDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.FechaDestapeDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.UsuarioRecepcionDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.UsuarioDestapeDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.BanAgrarioData = New DBAgrario.ProcessDataSet()
            Me.Panel1 = New System.Windows.Forms.Panel()
            Me.Label4 = New System.Windows.Forms.Label()
            Me.PrecintoGroupBox.SuspendLayout()
            Me.OficinaPanel.SuspendLayout()
            Me.BotonesGroupBox.SuspendLayout()
            Me.BotonesPanel.SuspendLayout()
            Me.ContenedoresGroupBox.SuspendLayout()
            Me.Panel2.SuspendLayout()
            CType(Me.ContenedoresDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.BanAgrarioData, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.Panel1.SuspendLayout()
            Me.SuspendLayout()
            '
            'PrecintoGroupBox
            '
            Me.PrecintoGroupBox.Controls.Add(Me.OficinaPanel)
            Me.PrecintoGroupBox.Controls.Add(Me.PrecintoTextBox)
            Me.PrecintoGroupBox.Controls.Add(Me.Label1)
            Me.PrecintoGroupBox.Dock = System.Windows.Forms.DockStyle.Top
            Me.PrecintoGroupBox.Location = New System.Drawing.Point(0, 0)
            Me.PrecintoGroupBox.Name = "PrecintoGroupBox"
            Me.PrecintoGroupBox.Size = New System.Drawing.Size(880, 50)
            Me.PrecintoGroupBox.TabIndex = 2
            Me.PrecintoGroupBox.TabStop = False
            '
            'OficinaPanel
            '
            Me.OficinaPanel.Controls.Add(Me.NombreOficina1ComboBox)
            Me.OficinaPanel.Controls.Add(Me.CodOficina1ComboBox)
            Me.OficinaPanel.Controls.Add(Me.Label6)
            Me.OficinaPanel.Controls.Add(Me.Label2)
            Me.OficinaPanel.Location = New System.Drawing.Point(310, 15)
            Me.OficinaPanel.Name = "OficinaPanel"
            Me.OficinaPanel.Size = New System.Drawing.Size(543, 24)
            Me.OficinaPanel.TabIndex = 7
            '
            'NombreOficina1ComboBox
            '
            Me.NombreOficina1ComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.NombreOficina1ComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.NombreOficina1ComboBox.DisabledEnter = True
            Me.NombreOficina1ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.NombreOficina1ComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.NombreOficina1ComboBox.FormattingEnabled = True
            Me.NombreOficina1ComboBox.Location = New System.Drawing.Point(377, 2)
            Me.NombreOficina1ComboBox.Name = "NombreOficina1ComboBox"
            Me.NombreOficina1ComboBox.Size = New System.Drawing.Size(160, 21)
            Me.NombreOficina1ComboBox.TabIndex = 10
            '
            'CodOficina1ComboBox
            '
            Me.CodOficina1ComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.CodOficina1ComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            Me.CodOficina1ComboBox.DisabledEnter = True
            Me.CodOficina1ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.CodOficina1ComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.CodOficina1ComboBox.FormattingEnabled = True
            Me.CodOficina1ComboBox.Location = New System.Drawing.Point(121, 2)
            Me.CodOficina1ComboBox.Name = "CodOficina1ComboBox"
            Me.CodOficina1ComboBox.Size = New System.Drawing.Size(160, 21)
            Me.CodOficina1ComboBox.TabIndex = 9
            '
            'Label6
            '
            Me.Label6.AutoSize = True
            Me.Label6.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label6.Location = New System.Drawing.Point(293, 4)
            Me.Label6.Name = "Label6"
            Me.Label6.Size = New System.Drawing.Size(71, 19)
            Me.Label6.TabIndex = 30
            Me.Label6.Text = "Oficina:"
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label2.Location = New System.Drawing.Point(9, 4)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(107, 19)
            Me.Label2.TabIndex = 8
            Me.Label2.Text = "Cod Oficina:"
            '
            'PrecintoTextBox
            '
            Me.PrecintoTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                                               Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.PrecintoTextBox.Cantidad_Decimales = CType(0, Short)
            Me.PrecintoTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
            Me.PrecintoTextBox.DisabledEnter = True
            Me.PrecintoTextBox.DisabledTab = False
            Me.PrecintoTextBox.FocusIn = System.Drawing.Color.LightYellow
            Me.PrecintoTextBox.FocusOut = System.Drawing.Color.White
            Me.PrecintoTextBox.Location = New System.Drawing.Point(92, 15)
            Me.PrecintoTextBox.MaximumLength = CType(0, Short)
            Me.PrecintoTextBox.MaxLength = 0
            Me.PrecintoTextBox.MinimumLength = CType(0, Short)
            Me.PrecintoTextBox.Name = "PrecintoTextBox"
            Rango2.MaxValue = 2147483647.0R
            Rango2.MinValue = 0.0R
            Me.PrecintoTextBox.Rango = Rango2
            Me.PrecintoTextBox.ShortcutsEnabled = False
            Me.PrecintoTextBox.Size = New System.Drawing.Size(216, 21)
            Me.PrecintoTextBox.TabIndex = 6
            Me.PrecintoTextBox.Type = DesktopTextBoxControl.TipoTextBox.Numerico
            Me.PrecintoTextBox.Usa_Decimales = False
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.Location = New System.Drawing.Point(8, 15)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(83, 19)
            Me.Label1.TabIndex = 5
            Me.Label1.Text = "Precinto:"
            '
            'BotonesGroupBox
            '
            Me.BotonesGroupBox.Controls.Add(Me.BotonesPanel)
            Me.BotonesGroupBox.Controls.Add(Me.CerrarButton)
            Me.BotonesGroupBox.Dock = System.Windows.Forms.DockStyle.Top
            Me.BotonesGroupBox.Location = New System.Drawing.Point(0, 50)
            Me.BotonesGroupBox.Name = "BotonesGroupBox"
            Me.BotonesGroupBox.Size = New System.Drawing.Size(880, 46)
            Me.BotonesGroupBox.TabIndex = 4
            Me.BotonesGroupBox.TabStop = False
            '
            'BotonesPanel
            '
            Me.BotonesPanel.Controls.Add(Me.InformeButton)
            Me.BotonesPanel.Controls.Add(Me.BuscarButton)
            Me.BotonesPanel.Location = New System.Drawing.Point(6, 16)
            Me.BotonesPanel.Name = "BotonesPanel"
            Me.BotonesPanel.Size = New System.Drawing.Size(727, 30)
            Me.BotonesPanel.TabIndex = 31
            '
            'InformeButton
            '
            Me.InformeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.InformeButton.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
            Me.InformeButton.Image = Global.Banagrario.Plugin.My.Resources.Resources.Edit
            Me.InformeButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.InformeButton.Location = New System.Drawing.Point(553, -2)
            Me.InformeButton.Name = "InformeButton"
            Me.InformeButton.Size = New System.Drawing.Size(134, 32)
            Me.InformeButton.TabIndex = 35
            Me.InformeButton.Text = "&Generar Reporte"
            Me.InformeButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.InformeButton.UseVisualStyleBackColor = True
            '
            'BuscarButton
            '
            Me.BuscarButton.Image = Global.Banagrario.Plugin.My.Resources.Resources.btnBuscar
            Me.BuscarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.BuscarButton.Location = New System.Drawing.Point(4, 0)
            Me.BuscarButton.Name = "BuscarButton"
            Me.BuscarButton.Size = New System.Drawing.Size(101, 30)
            Me.BuscarButton.TabIndex = 34
            Me.BuscarButton.Text = "   &Buscar"
            Me.BuscarButton.UseVisualStyleBackColor = True
            '
            'CerrarButton
            '
            Me.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CerrarButton.Image = Global.Banagrario.Plugin.My.Resources.Resources.btnSalir1
            Me.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CerrarButton.Location = New System.Drawing.Point(739, 16)
            Me.CerrarButton.Name = "CerrarButton"
            Me.CerrarButton.Size = New System.Drawing.Size(107, 30)
            Me.CerrarButton.TabIndex = 35
            Me.CerrarButton.Text = "     &Cerrar"
            Me.CerrarButton.UseVisualStyleBackColor = True
            '
            'ContenedoresGroupBox
            '
            Me.ContenedoresGroupBox.Controls.Add(Me.Panel2)
            Me.ContenedoresGroupBox.Controls.Add(Me.Panel1)
            Me.ContenedoresGroupBox.Dock = System.Windows.Forms.DockStyle.Fill
            Me.ContenedoresGroupBox.Location = New System.Drawing.Point(0, 96)
            Me.ContenedoresGroupBox.Name = "ContenedoresGroupBox"
            Me.ContenedoresGroupBox.Size = New System.Drawing.Size(880, 270)
            Me.ContenedoresGroupBox.TabIndex = 5
            Me.ContenedoresGroupBox.TabStop = False
            '
            'Panel2
            '
            Me.Panel2.Controls.Add(Me.ContenedoresDataGridView)
            Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
            Me.Panel2.Location = New System.Drawing.Point(3, 37)
            Me.Panel2.Name = "Panel2"
            Me.Panel2.Size = New System.Drawing.Size(874, 230)
            Me.Panel2.TabIndex = 17
            '
            'ContenedoresDataGridView
            '
            Me.ContenedoresDataGridView.AllowUserToAddRows = False
            Me.ContenedoresDataGridView.AllowUserToDeleteRows = False
            Me.ContenedoresDataGridView.AutoGenerateColumns = False
            Me.ContenedoresDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.ContenedoresDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.CiudadDataGridViewTextBoxColumn, Me.CodigoOficinaDataGridViewTextBoxColumn, Me.NombreOficinaDataGridViewTextBoxColumn, Me.FechaProcesoDataGridViewTextBoxColumn, Me.IdPrecintoDataGridViewTextBoxColumn, Me.ContenedoresDataGridViewTextBoxColumn, Me.FechaRecepcionDataGridViewTextBoxColumn, Me.FechaDestapeDataGridViewTextBoxColumn, Me.UsuarioRecepcionDataGridViewTextBoxColumn, Me.UsuarioDestapeDataGridViewTextBoxColumn})
            Me.ContenedoresDataGridView.DataMember = "CTA_Destape_Buscar"
            Me.ContenedoresDataGridView.DataSource = Me.BanAgrarioData
            Me.ContenedoresDataGridView.Dock = System.Windows.Forms.DockStyle.Fill
            Me.ContenedoresDataGridView.Location = New System.Drawing.Point(0, 0)
            Me.ContenedoresDataGridView.Name = "ContenedoresDataGridView"
            Me.ContenedoresDataGridView.ReadOnly = True
            Me.ContenedoresDataGridView.Size = New System.Drawing.Size(874, 230)
            Me.ContenedoresDataGridView.TabIndex = 37
            '
            'CiudadDataGridViewTextBoxColumn
            '
            Me.CiudadDataGridViewTextBoxColumn.DataPropertyName = "Ciudad"
            Me.CiudadDataGridViewTextBoxColumn.HeaderText = "Ciudad"
            Me.CiudadDataGridViewTextBoxColumn.Name = "CiudadDataGridViewTextBoxColumn"
            Me.CiudadDataGridViewTextBoxColumn.ReadOnly = True
            '
            'CodigoOficinaDataGridViewTextBoxColumn
            '
            Me.CodigoOficinaDataGridViewTextBoxColumn.DataPropertyName = "Codigo_Oficina"
            Me.CodigoOficinaDataGridViewTextBoxColumn.HeaderText = "Cod Oficina"
            Me.CodigoOficinaDataGridViewTextBoxColumn.Name = "CodigoOficinaDataGridViewTextBoxColumn"
            Me.CodigoOficinaDataGridViewTextBoxColumn.ReadOnly = True
            '
            'NombreOficinaDataGridViewTextBoxColumn
            '
            Me.NombreOficinaDataGridViewTextBoxColumn.DataPropertyName = "Nombre_Oficina"
            Me.NombreOficinaDataGridViewTextBoxColumn.HeaderText = "Oficina"
            Me.NombreOficinaDataGridViewTextBoxColumn.Name = "NombreOficinaDataGridViewTextBoxColumn"
            Me.NombreOficinaDataGridViewTextBoxColumn.ReadOnly = True
            '
            'FechaProcesoDataGridViewTextBoxColumn
            '
            Me.FechaProcesoDataGridViewTextBoxColumn.DataPropertyName = "Fecha_Proceso"
            Me.FechaProcesoDataGridViewTextBoxColumn.HeaderText = "Fecha Proceso"
            Me.FechaProcesoDataGridViewTextBoxColumn.Name = "FechaProcesoDataGridViewTextBoxColumn"
            Me.FechaProcesoDataGridViewTextBoxColumn.ReadOnly = True
            '
            'IdPrecintoDataGridViewTextBoxColumn
            '
            Me.IdPrecintoDataGridViewTextBoxColumn.DataPropertyName = "id_Precinto"
            Me.IdPrecintoDataGridViewTextBoxColumn.HeaderText = "Precinto"
            Me.IdPrecintoDataGridViewTextBoxColumn.Name = "IdPrecintoDataGridViewTextBoxColumn"
            Me.IdPrecintoDataGridViewTextBoxColumn.ReadOnly = True
            '
            'ContenedoresDataGridViewTextBoxColumn
            '
            Me.ContenedoresDataGridViewTextBoxColumn.DataPropertyName = "Contenedores"
            Me.ContenedoresDataGridViewTextBoxColumn.HeaderText = "Cantidad Contenedores"
            Me.ContenedoresDataGridViewTextBoxColumn.Name = "ContenedoresDataGridViewTextBoxColumn"
            Me.ContenedoresDataGridViewTextBoxColumn.ReadOnly = True
            '
            'FechaRecepcionDataGridViewTextBoxColumn
            '
            Me.FechaRecepcionDataGridViewTextBoxColumn.DataPropertyName = "Fecha_Recepcion"
            Me.FechaRecepcionDataGridViewTextBoxColumn.HeaderText = "Fecha y Hora Recepcion"
            Me.FechaRecepcionDataGridViewTextBoxColumn.Name = "FechaRecepcionDataGridViewTextBoxColumn"
            Me.FechaRecepcionDataGridViewTextBoxColumn.ReadOnly = True
            '
            'FechaDestapeDataGridViewTextBoxColumn
            '
            Me.FechaDestapeDataGridViewTextBoxColumn.DataPropertyName = "Fecha_Destape"
            Me.FechaDestapeDataGridViewTextBoxColumn.HeaderText = "Fecha y Hora Destape"
            Me.FechaDestapeDataGridViewTextBoxColumn.Name = "FechaDestapeDataGridViewTextBoxColumn"
            Me.FechaDestapeDataGridViewTextBoxColumn.ReadOnly = True
            '
            'UsuarioRecepcionDataGridViewTextBoxColumn
            '
            Me.UsuarioRecepcionDataGridViewTextBoxColumn.DataPropertyName = "Usuario_Recepcion"
            Me.UsuarioRecepcionDataGridViewTextBoxColumn.HeaderText = "Recibido por"
            Me.UsuarioRecepcionDataGridViewTextBoxColumn.Name = "UsuarioRecepcionDataGridViewTextBoxColumn"
            Me.UsuarioRecepcionDataGridViewTextBoxColumn.ReadOnly = True
            '
            'UsuarioDestapeDataGridViewTextBoxColumn
            '
            Me.UsuarioDestapeDataGridViewTextBoxColumn.DataPropertyName = "Usuario_Destape"
            Me.UsuarioDestapeDataGridViewTextBoxColumn.HeaderText = "Destapado por"
            Me.UsuarioDestapeDataGridViewTextBoxColumn.Name = "UsuarioDestapeDataGridViewTextBoxColumn"
            Me.UsuarioDestapeDataGridViewTextBoxColumn.ReadOnly = True
            '
            'BanAgrarioData
            '
            Me.BanAgrarioData.DataSetName = "NewDataSet"
            '
            'Panel1
            '
            Me.Panel1.Controls.Add(Me.Label4)
            Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
            Me.Panel1.Location = New System.Drawing.Point(3, 17)
            Me.Panel1.Name = "Panel1"
            Me.Panel1.Size = New System.Drawing.Size(874, 20)
            Me.Panel1.TabIndex = 16
            '
            'Label4
            '
            Me.Label4.AutoSize = True
            Me.Label4.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label4.Location = New System.Drawing.Point(3, 0)
            Me.Label4.Name = "Label4"
            Me.Label4.Size = New System.Drawing.Size(190, 19)
            Me.Label4.TabIndex = 36
            Me.Label4.Text = "Precintos encontrados"
            '
            'FormBuscarPrecinto
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(880, 366)
            Me.Controls.Add(Me.ContenedoresGroupBox)
            Me.Controls.Add(Me.BotonesGroupBox)
            Me.Controls.Add(Me.PrecintoGroupBox)
            Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.Name = "FormBuscarPrecinto"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Buscar Precinto"
            Me.PrecintoGroupBox.ResumeLayout(False)
            Me.PrecintoGroupBox.PerformLayout()
            Me.OficinaPanel.ResumeLayout(False)
            Me.OficinaPanel.PerformLayout()
            Me.BotonesGroupBox.ResumeLayout(False)
            Me.BotonesPanel.ResumeLayout(False)
            Me.ContenedoresGroupBox.ResumeLayout(False)
            Me.Panel2.ResumeLayout(False)
            CType(Me.ContenedoresDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.BanAgrarioData, System.ComponentModel.ISupportInitialize).EndInit()
            Me.Panel1.ResumeLayout(False)
            Me.Panel1.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents PrecintoGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents OficinaPanel As System.Windows.Forms.Panel
        Friend WithEvents NombreOficina1ComboBox As DesktopComboBoxControl
        Friend WithEvents CodOficina1ComboBox As DesktopComboBoxControl
        Friend WithEvents Label6 As System.Windows.Forms.Label
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents PrecintoTextBox As DesktopTextBoxControl
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents BanAgrarioData As DBAgrario.ProcessDataSet
        Friend WithEvents BotonesGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents BotonesPanel As System.Windows.Forms.Panel
        Friend WithEvents BuscarButton As System.Windows.Forms.Button
        Friend WithEvents CerrarButton As System.Windows.Forms.Button
        Friend WithEvents ContenedoresGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents Panel2 As System.Windows.Forms.Panel
        Friend WithEvents ContenedoresDataGridView As System.Windows.Forms.DataGridView
        Friend WithEvents Panel1 As System.Windows.Forms.Panel
        Friend WithEvents Label4 As System.Windows.Forms.Label
        Friend WithEvents CiudadDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents CodigoOficinaDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents NombreOficinaDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents FechaProcesoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents IdPrecintoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ContenedoresDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents FechaRecepcionDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents FechaDestapeDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents UsuarioRecepcionDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents UsuarioDestapeDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents InformeButton As System.Windows.Forms.Button
    End Class
End Namespace