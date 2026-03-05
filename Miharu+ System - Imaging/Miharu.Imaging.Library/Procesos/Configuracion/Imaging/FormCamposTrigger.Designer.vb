
Namespace Procesos.Configuracion.Imaging
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormCamposTrigger
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
            Me.components = New System.ComponentModel.Container()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormCamposTrigger))
            Me.Label1 = New System.Windows.Forms.Label()
            Me.ListaBindingSource = New System.Windows.Forms.BindingSource(Me.components)
            Me.TriggerDataSet = New DBImaging.Esquemas.xsdTrigger()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.CamposListBox = New System.Windows.Forms.ListBox()
            Me.MostrarBindingSource = New System.Windows.Forms.BindingSource(Me.components)
            Me.EtapaBindingSource = New System.Windows.Forms.BindingSource(Me.components)
            Me.AñadirCamposListBox = New System.Windows.Forms.ListBox()
            Me.OcultarBindingSource = New System.Windows.Forms.BindingSource(Me.components)
            Me.AñadirButton = New System.Windows.Forms.Button()
            Me.AñadirTodosButton = New System.Windows.Forms.Button()
            Me.QuitarTodosButton = New System.Windows.Forms.Button()
            Me.QuitarButton = New System.Windows.Forms.Button()
            Me.lblMostrar = New System.Windows.Forms.Label()
            Me.lblOcultar = New System.Windows.Forms.Label()
            Me.CancelarButton = New System.Windows.Forms.Button()
            Me.AceptarButton = New System.Windows.Forms.Button()
            Me.Label7 = New System.Windows.Forms.Label()
            Me.EtapaComboBox = New System.Windows.Forms.ComboBox()
            Me.NombreCampoTextBox = New System.Windows.Forms.TextBox()
            Me.TBL_EtapaBindingSource = New System.Windows.Forms.BindingSource(Me.components)
            Me.OperadorComboBox = New System.Windows.Forms.ComboBox()
            Me.Label4 = New System.Windows.Forms.Label()
            Me.ValorTextBox = New System.Windows.Forms.TextBox()
            Me.ValorComboBox = New System.Windows.Forms.ComboBox()
            Me.Ocultar1_BindingSource = New System.Windows.Forms.BindingSource(Me.components)
            Me.MostrarValidacionBindingSource = New System.Windows.Forms.BindingSource(Me.components)
            CType(Me.ListaBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.TriggerDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.MostrarBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.EtapaBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.OcultarBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.TBL_EtapaBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.Ocultar1_BindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.MostrarValidacionBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.Location = New System.Drawing.Point(14, 15)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(49, 13)
            Me.Label1.TabIndex = 38
            Me.Label1.Text = "Campo "
            '
            'ListaBindingSource
            '
            Me.ListaBindingSource.AllowNew = False
            Me.ListaBindingSource.DataMember = "Lista"
            Me.ListaBindingSource.DataSource = Me.TriggerDataSet
            Me.ListaBindingSource.Sort = "Nombre_Lista"
            '
            'TriggerDataSet
            '
            Me.TriggerDataSet.DataSetName = "xsdTrigger"
            Me.TriggerDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label2.Location = New System.Drawing.Point(14, 42)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(36, 13)
            Me.Label2.TabIndex = 40
            Me.Label2.Text = "Valor"
            '
            'CamposListBox
            '
            Me.CamposListBox.DataSource = Me.MostrarBindingSource
            Me.CamposListBox.DisplayMember = "Nombre_Campo"
            Me.CamposListBox.FormattingEnabled = True
            Me.CamposListBox.Location = New System.Drawing.Point(17, 146)
            Me.CamposListBox.Name = "CamposListBox"
            Me.CamposListBox.Size = New System.Drawing.Size(179, 225)
            Me.CamposListBox.TabIndex = 42
            Me.CamposListBox.ValueMember = "id_Campo"
            '
            'MostrarBindingSource
            '
            Me.MostrarBindingSource.AllowNew = False
            Me.MostrarBindingSource.DataMember = "Campo"
            Me.MostrarBindingSource.DataSource = Me.TriggerDataSet
            Me.MostrarBindingSource.Filter = "Ocultar=False"
            Me.MostrarBindingSource.Sort = "Nombre_Campo"
            '
            'EtapaBindingSource
            '
            Me.EtapaBindingSource.DataMember = "FK_Lista_Etapa"
            Me.EtapaBindingSource.DataSource = Me.ListaBindingSource
            Me.EtapaBindingSource.Sort = "Nombre_Etapa"
            '
            'AñadirCamposListBox
            '
            Me.AñadirCamposListBox.DataSource = Me.OcultarBindingSource
            Me.AñadirCamposListBox.DisplayMember = "Nombre_Campo"
            Me.AñadirCamposListBox.FormattingEnabled = True
            Me.AñadirCamposListBox.Location = New System.Drawing.Point(244, 146)
            Me.AñadirCamposListBox.Name = "AñadirCamposListBox"
            Me.AñadirCamposListBox.Size = New System.Drawing.Size(179, 225)
            Me.AñadirCamposListBox.TabIndex = 43
            Me.AñadirCamposListBox.ValueMember = "id_Campo"
            '
            'OcultarBindingSource
            '
            Me.OcultarBindingSource.DataMember = "FK_Etapa_CampoTrigger"
            Me.OcultarBindingSource.DataSource = Me.EtapaBindingSource
            Me.OcultarBindingSource.Sort = "Nombre_Campo"
            '
            'AñadirButton
            '
            Me.AñadirButton.Location = New System.Drawing.Point(202, 146)
            Me.AñadirButton.Name = "AñadirButton"
            Me.AñadirButton.Size = New System.Drawing.Size(36, 23)
            Me.AñadirButton.TabIndex = 44
            Me.AñadirButton.Text = ">"
            Me.AñadirButton.UseVisualStyleBackColor = True
            '
            'AñadirTodosButton
            '
            Me.AñadirTodosButton.Location = New System.Drawing.Point(202, 175)
            Me.AñadirTodosButton.Name = "AñadirTodosButton"
            Me.AñadirTodosButton.Size = New System.Drawing.Size(36, 23)
            Me.AñadirTodosButton.TabIndex = 45
            Me.AñadirTodosButton.Text = ">>"
            Me.AñadirTodosButton.UseVisualStyleBackColor = True
            '
            'QuitarTodosButton
            '
            Me.QuitarTodosButton.Location = New System.Drawing.Point(202, 348)
            Me.QuitarTodosButton.Name = "QuitarTodosButton"
            Me.QuitarTodosButton.Size = New System.Drawing.Size(36, 23)
            Me.QuitarTodosButton.TabIndex = 46
            Me.QuitarTodosButton.Text = "<<"
            Me.QuitarTodosButton.UseVisualStyleBackColor = True
            '
            'QuitarButton
            '
            Me.QuitarButton.Location = New System.Drawing.Point(202, 319)
            Me.QuitarButton.Name = "QuitarButton"
            Me.QuitarButton.Size = New System.Drawing.Size(36, 23)
            Me.QuitarButton.TabIndex = 47
            Me.QuitarButton.Text = "<"
            Me.QuitarButton.UseVisualStyleBackColor = True
            '
            'lblMostrar
            '
            Me.lblMostrar.AutoSize = True
            Me.lblMostrar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblMostrar.Location = New System.Drawing.Point(14, 127)
            Me.lblMostrar.Name = "lblMostrar"
            Me.lblMostrar.Size = New System.Drawing.Size(108, 13)
            Me.lblMostrar.TabIndex = 48
            Me.lblMostrar.Text = "Campos a Mostrar"
            '
            'lblOcultar
            '
            Me.lblOcultar.AutoSize = True
            Me.lblOcultar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblOcultar.Location = New System.Drawing.Point(241, 127)
            Me.lblOcultar.Name = "lblOcultar"
            Me.lblOcultar.Size = New System.Drawing.Size(107, 13)
            Me.lblOcultar.TabIndex = 49
            Me.lblOcultar.Text = "Campos a Ocultar"
            '
            'CancelarButton
            '
            Me.CancelarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CancelarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CancelarButton.Image = CType(resources.GetObject("CancelarButton.Image"), System.Drawing.Image)
            Me.CancelarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.CancelarButton.Location = New System.Drawing.Point(343, 377)
            Me.CancelarButton.Name = "CancelarButton"
            Me.CancelarButton.Size = New System.Drawing.Size(80, 24)
            Me.CancelarButton.TabIndex = 51
            Me.CancelarButton.Text = "&Cancelar"
            Me.CancelarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'AceptarButton
            '
            Me.AceptarButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.AceptarButton.Image = CType(resources.GetObject("AceptarButton.Image"), System.Drawing.Image)
            Me.AceptarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.AceptarButton.Location = New System.Drawing.Point(257, 377)
            Me.AceptarButton.Name = "AceptarButton"
            Me.AceptarButton.Size = New System.Drawing.Size(80, 23)
            Me.AceptarButton.TabIndex = 50
            Me.AceptarButton.Text = "&Aceptar"
            Me.AceptarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'Label7
            '
            Me.Label7.AutoSize = True
            Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label7.Location = New System.Drawing.Point(14, 71)
            Me.Label7.Name = "Label7"
            Me.Label7.Size = New System.Drawing.Size(40, 13)
            Me.Label7.TabIndex = 53
            Me.Label7.Text = "Etapa"
            '
            'EtapaComboBox
            '
            Me.EtapaComboBox.DataSource = Me.EtapaBindingSource
            Me.EtapaComboBox.DisplayMember = "Nombre_Etapa"
            Me.EtapaComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.EtapaComboBox.FormattingEnabled = True
            Me.EtapaComboBox.Location = New System.Drawing.Point(138, 68)
            Me.EtapaComboBox.Name = "EtapaComboBox"
            Me.EtapaComboBox.Size = New System.Drawing.Size(285, 21)
            Me.EtapaComboBox.TabIndex = 54
            Me.EtapaComboBox.ValueMember = "id_Etapa"
            '
            'NombreCampoTextBox
            '
            Me.NombreCampoTextBox.Location = New System.Drawing.Point(138, 12)
            Me.NombreCampoTextBox.Name = "NombreCampoTextBox"
            Me.NombreCampoTextBox.ReadOnly = True
            Me.NombreCampoTextBox.Size = New System.Drawing.Size(285, 20)
            Me.NombreCampoTextBox.TabIndex = 55
            '
            'TBL_EtapaBindingSource
            '
            Me.TBL_EtapaBindingSource.AllowNew = False
            Me.TBL_EtapaBindingSource.DataMember = "TBL_Etapa"
            Me.TBL_EtapaBindingSource.DataSource = Me.TriggerDataSet
            Me.TBL_EtapaBindingSource.Sort = "Nombre_Etapa"
            '
            'OperadorComboBox
            '
            Me.OperadorComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.OperadorComboBox.FormattingEnabled = True
            Me.OperadorComboBox.Items.AddRange(New Object() {"=", "<", "<=", ">", ">=", "<>"})
            Me.OperadorComboBox.Location = New System.Drawing.Point(138, 98)
            Me.OperadorComboBox.Name = "OperadorComboBox"
            Me.OperadorComboBox.Size = New System.Drawing.Size(110, 21)
            Me.OperadorComboBox.TabIndex = 57
            '
            'Label4
            '
            Me.Label4.AutoSize = True
            Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label4.Location = New System.Drawing.Point(14, 101)
            Me.Label4.Name = "Label4"
            Me.Label4.Size = New System.Drawing.Size(122, 13)
            Me.Label4.TabIndex = 56
            Me.Label4.Text = "Operador Validación"
            '
            'ValorTextBox
            '
            Me.ValorTextBox.BackColor = System.Drawing.Color.White
            Me.ValorTextBox.Location = New System.Drawing.Point(138, 39)
            Me.ValorTextBox.Name = "ValorTextBox"
            Me.ValorTextBox.Size = New System.Drawing.Size(285, 20)
            Me.ValorTextBox.TabIndex = 58
            '
            'ValorComboBox
            '
            Me.ValorComboBox.DataSource = Me.ListaBindingSource
            Me.ValorComboBox.DisplayMember = "Nombre_Lista"
            Me.ValorComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.ValorComboBox.FormattingEnabled = True
            Me.ValorComboBox.Location = New System.Drawing.Point(138, 38)
            Me.ValorComboBox.Name = "ValorComboBox"
            Me.ValorComboBox.Size = New System.Drawing.Size(285, 21)
            Me.ValorComboBox.TabIndex = 59
            Me.ValorComboBox.ValueMember = "id_Lista"
            '
            'Ocultar1_BindingSource
            '
            Me.Ocultar1_BindingSource.AllowNew = False
            Me.Ocultar1_BindingSource.DataMember = "TBL_CampoTrigger"
            Me.Ocultar1_BindingSource.DataSource = Me.TriggerDataSet
            Me.Ocultar1_BindingSource.Filter = "Ocultar=True"
            Me.Ocultar1_BindingSource.Sort = "Nombre_Campo"
            '
            'MostrarValidacionBindingSource
            '
            Me.MostrarValidacionBindingSource.AllowNew = False
            Me.MostrarValidacionBindingSource.DataMember = "Validacion"
            Me.MostrarValidacionBindingSource.DataSource = Me.TriggerDataSet
            Me.MostrarValidacionBindingSource.Filter = "Ocultar=False"
            Me.MostrarValidacionBindingSource.Sort = "Pregunta_Validacion"
            '
            'FormCamposTrigger
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(435, 410)
            Me.Controls.Add(Me.ValorComboBox)
            Me.Controls.Add(Me.ValorTextBox)
            Me.Controls.Add(Me.OperadorComboBox)
            Me.Controls.Add(Me.Label4)
            Me.Controls.Add(Me.NombreCampoTextBox)
            Me.Controls.Add(Me.EtapaComboBox)
            Me.Controls.Add(Me.Label7)
            Me.Controls.Add(Me.CancelarButton)
            Me.Controls.Add(Me.AceptarButton)
            Me.Controls.Add(Me.lblOcultar)
            Me.Controls.Add(Me.lblMostrar)
            Me.Controls.Add(Me.CamposListBox)
            Me.Controls.Add(Me.QuitarButton)
            Me.Controls.Add(Me.AñadirCamposListBox)
            Me.Controls.Add(Me.QuitarTodosButton)
            Me.Controls.Add(Me.Label2)
            Me.Controls.Add(Me.AñadirButton)
            Me.Controls.Add(Me.AñadirTodosButton)
            Me.Controls.Add(Me.Label1)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FormCamposTrigger"
            Me.ShowIcon = False
            Me.Text = "Campos Trigger"
            CType(Me.ListaBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.TriggerDataSet, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.MostrarBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.EtapaBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.OcultarBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.TBL_EtapaBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.Ocultar1_BindingSource, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.MostrarValidacionBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents CamposListBox As System.Windows.Forms.ListBox
        Friend WithEvents AñadirCamposListBox As System.Windows.Forms.ListBox
        Friend WithEvents AñadirButton As System.Windows.Forms.Button
        Friend WithEvents AñadirTodosButton As System.Windows.Forms.Button
        Friend WithEvents QuitarTodosButton As System.Windows.Forms.Button
        Friend WithEvents QuitarButton As System.Windows.Forms.Button
        Friend WithEvents lblMostrar As System.Windows.Forms.Label
        Friend WithEvents lblOcultar As System.Windows.Forms.Label
        Friend WithEvents CancelarButton As System.Windows.Forms.Button
        Friend WithEvents AceptarButton As System.Windows.Forms.Button
        Friend WithEvents Label7 As System.Windows.Forms.Label
        Friend WithEvents EtapaComboBox As System.Windows.Forms.ComboBox
        Friend WithEvents NombreCampoTextBox As System.Windows.Forms.TextBox
        Friend WithEvents TriggerDataSet As DBImaging.Esquemas.xsdTrigger
        Friend WithEvents MostrarBindingSource As System.Windows.Forms.BindingSource
        Friend WithEvents OcultarBindingSource As System.Windows.Forms.BindingSource
        Friend WithEvents ListaBindingSource As System.Windows.Forms.BindingSource
        Friend WithEvents EtapaBindingSource As System.Windows.Forms.BindingSource
        Friend WithEvents TBL_EtapaBindingSource As System.Windows.Forms.BindingSource
        Friend WithEvents OperadorComboBox As System.Windows.Forms.ComboBox
        Friend WithEvents Label4 As System.Windows.Forms.Label
        Friend WithEvents ValorTextBox As System.Windows.Forms.TextBox
        Friend WithEvents ValorComboBox As System.Windows.Forms.ComboBox
        Friend WithEvents Ocultar1_BindingSource As System.Windows.Forms.BindingSource
        Friend WithEvents MostrarValidacionBindingSource As System.Windows.Forms.BindingSource
    End Class
End Namespace