Imports Miharu.Desktop.Controls.DesktopTextBox

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormAutorizacion
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim Rango1 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
        Dim Rango2 As Miharu.Desktop.Controls.DesktopTextBox.Rango = New Miharu.Desktop.Controls.DesktopTextBox.Rango()
        Me.TextoAutorizacionTextBox = New System.Windows.Forms.TextBox()
        Me.AceptarButton = New System.Windows.Forms.Button()
        Me.CancelarButton = New System.Windows.Forms.Button()
        Me.lblLogin = New System.Windows.Forms.Label()
        Me.lblPassword = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DataGridProcesosAutoriza = New System.Windows.Forms.DataGridView()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ObservacionTexBox = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.PasswordTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
        Me.LoginTextBox = New Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl()
        Me.id_Control_Proceso = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.fk_Tipo_Proceso = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nombre_Tipo_Proceso = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Fecha_Proceso = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Fecha_Proceso_Ejecucion = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Cargue_Completo = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Cantidad_Logs_Cargar = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Cantidad_Logs_Cargados = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.DataGridProcesosAutoriza, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TextoAutorizacionTextBox
        '
        Me.TextoAutorizacionTextBox.Location = New System.Drawing.Point(14, 36)
        Me.TextoAutorizacionTextBox.Multiline = True
        Me.TextoAutorizacionTextBox.Name = "TextoAutorizacionTextBox"
        Me.TextoAutorizacionTextBox.ReadOnly = True
        Me.TextoAutorizacionTextBox.Size = New System.Drawing.Size(562, 69)
        Me.TextoAutorizacionTextBox.TabIndex = 1
        Me.TextoAutorizacionTextBox.TabStop = False
        '
        'AceptarButton
        '
        Me.AceptarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.AceptarButton.Location = New System.Drawing.Point(384, 496)
        Me.AceptarButton.Name = "AceptarButton"
        Me.AceptarButton.Size = New System.Drawing.Size(87, 23)
        Me.AceptarButton.TabIndex = 6
        Me.AceptarButton.Text = "     Aceptar"
        Me.AceptarButton.UseVisualStyleBackColor = True
        '
        'CancelarButton
        '
        Me.CancelarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.CancelarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CancelarButton.Location = New System.Drawing.Point(485, 496)
        Me.CancelarButton.Name = "CancelarButton"
        Me.CancelarButton.Size = New System.Drawing.Size(87, 23)
        Me.CancelarButton.TabIndex = 7
        Me.CancelarButton.Text = "    Cancelar"
        Me.CancelarButton.UseVisualStyleBackColor = True
        '
        'lblLogin
        '
        Me.lblLogin.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLogin.Location = New System.Drawing.Point(11, 251)
        Me.lblLogin.Name = "lblLogin"
        Me.lblLogin.Size = New System.Drawing.Size(136, 16)
        Me.lblLogin.TabIndex = 2
        Me.lblLogin.Text = "Usuario:"
        '
        'lblPassword
        '
        Me.lblPassword.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPassword.Location = New System.Drawing.Point(11, 431)
        Me.lblPassword.Name = "lblPassword"
        Me.lblPassword.Size = New System.Drawing.Size(136, 16)
        Me.lblPassword.TabIndex = 4
        Me.lblPassword.Text = "Contraseña:"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(136, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Accion a realizar"
        '
        'DataGridProcesosAutoriza
        '
        Me.DataGridProcesosAutoriza.AllowUserToAddRows = False
        Me.DataGridProcesosAutoriza.AllowUserToDeleteRows = False
        Me.DataGridProcesosAutoriza.AllowUserToOrderColumns = True
        Me.DataGridProcesosAutoriza.AllowUserToResizeRows = False
        Me.DataGridProcesosAutoriza.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridProcesosAutoriza.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridProcesosAutoriza.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridProcesosAutoriza.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridProcesosAutoriza.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.id_Control_Proceso, Me.fk_Tipo_Proceso, Me.Nombre_Tipo_Proceso, Me.Fecha_Proceso, Me.Fecha_Proceso_Ejecucion, Me.Cargue_Completo, Me.Cantidad_Logs_Cargar, Me.Cantidad_Logs_Cargados})
        Me.DataGridProcesosAutoriza.Location = New System.Drawing.Point(13, 132)
        Me.DataGridProcesosAutoriza.Name = "DataGridProcesosAutoriza"
        Me.DataGridProcesosAutoriza.Size = New System.Drawing.Size(563, 150)
        Me.DataGridProcesosAutoriza.TabIndex = 8
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 377)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(136, 16)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "Usuario:"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(12, 110)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(296, 16)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "Seleccione los procesos que desea autorizar"
        '
        'ObservacionTexBox
        '
        Me.ObservacionTexBox.Font = New System.Drawing.Font("Tahoma", 11.25!)
        Me.ObservacionTexBox.Location = New System.Drawing.Point(13, 310)
        Me.ObservacionTexBox.Multiline = True
        Me.ObservacionTexBox.Name = "ObservacionTexBox"
        Me.ObservacionTexBox.Size = New System.Drawing.Size(563, 53)
        Me.ObservacionTexBox.TabIndex = 12
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(12, 291)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(136, 16)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "Observacion:"
        '
        'PasswordTextBox
        '
        Me.PasswordTextBox._Obligatorio = False
        Me.PasswordTextBox._PermitePegar = False
        Me.PasswordTextBox.Cantidad_Decimales = CType(0, Short)
        Me.PasswordTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
        Me.PasswordTextBox.DateFormat = Nothing
        Me.PasswordTextBox.DisabledEnter = False
        Me.PasswordTextBox.DisabledTab = False
        Me.PasswordTextBox.EnabledShortCuts = False
        Me.PasswordTextBox.FocusIn = System.Drawing.Color.LightYellow
        Me.PasswordTextBox.FocusOut = System.Drawing.Color.White
        Me.PasswordTextBox.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold)
        Me.PasswordTextBox.Location = New System.Drawing.Point(14, 451)
        Me.PasswordTextBox.MaskedTextBox_Property = ""
        Me.PasswordTextBox.MaximumLength = CType(0, Short)
        Me.PasswordTextBox.MinimumLength = CType(0, Short)
        Me.PasswordTextBox.Name = "PasswordTextBox"
        Me.PasswordTextBox.Obligatorio = False
        Me.PasswordTextBox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.PasswordTextBox.permitePegar = False
        Rango1.MaxValue = 2147483647.0R
        Rango1.MinValue = 0.0R
        Me.PasswordTextBox.Rango = Rango1
        Me.PasswordTextBox.Size = New System.Drawing.Size(563, 26)
        Me.PasswordTextBox.TabIndex = 5
        Me.PasswordTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal
        Me.PasswordTextBox.Usa_Decimales = False
        Me.PasswordTextBox.Validos_Cantidad_Puntos = False
        '
        'LoginTextBox
        '
        Me.LoginTextBox._Obligatorio = False
        Me.LoginTextBox._PermitePegar = False
        Me.LoginTextBox.Cantidad_Decimales = CType(0, Short)
        Me.LoginTextBox.Caracter_Decimal = Global.Microsoft.VisualBasic.ChrW(0)
        Me.LoginTextBox.DateFormat = Nothing
        Me.LoginTextBox.DisabledEnter = False
        Me.LoginTextBox.DisabledTab = False
        Me.LoginTextBox.EnabledShortCuts = False
        Me.LoginTextBox.FocusIn = System.Drawing.Color.LightYellow
        Me.LoginTextBox.FocusOut = System.Drawing.Color.White
        Me.LoginTextBox.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold)
        Me.LoginTextBox.Location = New System.Drawing.Point(14, 398)
        Me.LoginTextBox.MaskedTextBox_Property = ""
        Me.LoginTextBox.MaximumLength = CType(0, Short)
        Me.LoginTextBox.MinimumLength = CType(0, Short)
        Me.LoginTextBox.Name = "LoginTextBox"
        Me.LoginTextBox.Obligatorio = False
        Me.LoginTextBox.permitePegar = False
        Rango2.MaxValue = 2147483647.0R
        Rango2.MinValue = 0.0R
        Me.LoginTextBox.Rango = Rango2
        Me.LoginTextBox.Size = New System.Drawing.Size(563, 26)
        Me.LoginTextBox.TabIndex = 3
        Me.LoginTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal
        Me.LoginTextBox.Usa_Decimales = False
        Me.LoginTextBox.Validos_Cantidad_Puntos = False
        '
        'id_Control_Proceso
        '
        Me.id_Control_Proceso.DataPropertyName = "id_Control_Proceso"
        Me.id_Control_Proceso.HeaderText = "id_Control_Proceso"
        Me.id_Control_Proceso.Name = "id_Control_Proceso"
        Me.id_Control_Proceso.Visible = False
        '
        'fk_Tipo_Proceso
        '
        Me.fk_Tipo_Proceso.DataPropertyName = "fk_Tipo_Proceso"
        Me.fk_Tipo_Proceso.HeaderText = "fk_Tipo_Proceso"
        Me.fk_Tipo_Proceso.Name = "fk_Tipo_Proceso"
        Me.fk_Tipo_Proceso.Visible = False
        '
        'Nombre_Tipo_Proceso
        '
        Me.Nombre_Tipo_Proceso.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Nombre_Tipo_Proceso.DataPropertyName = "Nombre_Tipo_Proceso"
        Me.Nombre_Tipo_Proceso.HeaderText = "Nombre Tipo Proceso"
        Me.Nombre_Tipo_Proceso.MinimumWidth = 200
        Me.Nombre_Tipo_Proceso.Name = "Nombre_Tipo_Proceso"
        Me.Nombre_Tipo_Proceso.ReadOnly = True
        Me.Nombre_Tipo_Proceso.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        'Fecha_Proceso
        '
        Me.Fecha_Proceso.DataPropertyName = "Fecha_Proceso"
        Me.Fecha_Proceso.HeaderText = "Fecha Proceso"
        Me.Fecha_Proceso.MinimumWidth = 70
        Me.Fecha_Proceso.Name = "Fecha_Proceso"
        Me.Fecha_Proceso.ReadOnly = True
        '
        'Fecha_Proceso_Ejecucion
        '
        Me.Fecha_Proceso_Ejecucion.DataPropertyName = "Fecha_Proceso_Ejecucion"
        Me.Fecha_Proceso_Ejecucion.HeaderText = "Fecha Proceso Ejecucion"
        Me.Fecha_Proceso_Ejecucion.Name = "Fecha_Proceso_Ejecucion"
        Me.Fecha_Proceso_Ejecucion.ReadOnly = True
        Me.Fecha_Proceso_Ejecucion.Visible = False
        '
        'Cargue_Completo
        '
        Me.Cargue_Completo.DataPropertyName = "Cargue_Completo"
        Me.Cargue_Completo.HeaderText = "Cargue Completo"
        Me.Cargue_Completo.Name = "Cargue_Completo"
        Me.Cargue_Completo.ReadOnly = True
        Me.Cargue_Completo.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Cargue_Completo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.Cargue_Completo.Visible = False
        '
        'Cantidad_Logs_Cargar
        '
        Me.Cantidad_Logs_Cargar.DataPropertyName = "Cantidad_Logs_Cargar"
        Me.Cantidad_Logs_Cargar.HeaderText = "Logs Cargar"
        Me.Cantidad_Logs_Cargar.Name = "Cantidad_Logs_Cargar"
        Me.Cantidad_Logs_Cargar.ReadOnly = True
        '
        'Cantidad_Logs_Cargados
        '
        Me.Cantidad_Logs_Cargados.DataPropertyName = "Cantidad_Logs_Cargados"
        Me.Cantidad_Logs_Cargados.HeaderText = "Logs Cargados"
        Me.Cantidad_Logs_Cargados.Name = "Cantidad_Logs_Cargados"
        Me.Cantidad_Logs_Cargados.ReadOnly = True
        '
        'FormAutorizacion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.CancelarButton
        Me.ClientSize = New System.Drawing.Size(588, 531)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.ObservacionTexBox)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.DataGridProcesosAutoriza)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PasswordTextBox)
        Me.Controls.Add(Me.LoginTextBox)
        Me.Controls.Add(Me.lblLogin)
        Me.Controls.Add(Me.lblPassword)
        Me.Controls.Add(Me.CancelarButton)
        Me.Controls.Add(Me.AceptarButton)
        Me.Controls.Add(Me.TextoAutorizacionTextBox)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormAutorizacion"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Autorizar"
        CType(Me.DataGridProcesosAutoriza, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TextoAutorizacionTextBox As System.Windows.Forms.TextBox
    Friend WithEvents AceptarButton As System.Windows.Forms.Button
    Friend WithEvents CancelarButton As System.Windows.Forms.Button
    Friend WithEvents PasswordTextBox As DesktopTextBoxControl
    Friend WithEvents LoginTextBox As DesktopTextBoxControl
    Friend WithEvents lblLogin As System.Windows.Forms.Label
    Friend WithEvents lblPassword As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents DataGridProcesosAutoriza As System.Windows.Forms.DataGridView
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ObservacionTexBox As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents id_Control_Proceso As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents fk_Tipo_Proceso As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Nombre_Tipo_Proceso As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Fecha_Proceso As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Fecha_Proceso_Ejecucion As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Cargue_Completo As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Cantidad_Logs_Cargar As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Cantidad_Logs_Cargados As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
