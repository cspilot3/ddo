Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports System.Windows.Forms

Namespace Imaging.Estiba

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FormCreacionEstibas
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
            Me.MainGroupBox = New System.Windows.Forms.GroupBox()
            Me.btnsalir = New System.Windows.Forms.Button()
            Me.btnguardarEstiba = New System.Windows.Forms.Button()
            Me.Panel2 = New System.Windows.Forms.Panel()
            Me.TipoArchivoComboBox = New System.Windows.Forms.ComboBox()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.SerieTextBox = New System.Windows.Forms.TextBox()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.tbnumerocaja = New System.Windows.Forms.TextBox()
            Me.btnAgregarCaja = New System.Windows.Forms.Button()
            Me.EstibasDataGridView = New System.Windows.Forms.DataGridView()
            Me.MainGroupBox.SuspendLayout()
            Me.Panel2.SuspendLayout()
            CType(Me.EstibasDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'MainGroupBox
            '
            Me.MainGroupBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.MainGroupBox.Controls.Add(Me.btnsalir)
            Me.MainGroupBox.Controls.Add(Me.btnguardarEstiba)
            Me.MainGroupBox.Controls.Add(Me.Panel2)
            Me.MainGroupBox.Controls.Add(Me.EstibasDataGridView)
            Me.MainGroupBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.MainGroupBox.Location = New System.Drawing.Point(9, 9)
            Me.MainGroupBox.Name = "MainGroupBox"
            Me.MainGroupBox.Size = New System.Drawing.Size(647, 518)
            Me.MainGroupBox.TabIndex = 8
            Me.MainGroupBox.TabStop = False
            Me.MainGroupBox.Text = "Cerrar Caja"
            '
            'btnsalir
            '
            Me.btnsalir.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnsalir.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnsalir.Image = Global.UMV.Plugin.My.Resources.Resources.btnSalir
            Me.btnsalir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnsalir.Location = New System.Drawing.Point(371, 468)
            Me.btnsalir.Name = "btnsalir"
            Me.btnsalir.Size = New System.Drawing.Size(103, 37)
            Me.btnsalir.TabIndex = 22
            Me.btnsalir.Text = "Salir"
            Me.btnsalir.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.btnsalir.UseVisualStyleBackColor = True
            '
            'btnguardarEstiba
            '
            Me.btnguardarEstiba.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnguardarEstiba.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnguardarEstiba.Image = Global.UMV.Plugin.My.Resources.Resources.Close_folder
            Me.btnguardarEstiba.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnguardarEstiba.Location = New System.Drawing.Point(203, 468)
            Me.btnguardarEstiba.Name = "btnguardarEstiba"
            Me.btnguardarEstiba.Size = New System.Drawing.Size(133, 37)
            Me.btnguardarEstiba.TabIndex = 21
            Me.btnguardarEstiba.Text = "Guardar Estiba"
            Me.btnguardarEstiba.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.btnguardarEstiba.UseVisualStyleBackColor = True
            '
            'Panel2
            '
            Me.Panel2.Controls.Add(Me.TipoArchivoComboBox)
            Me.Panel2.Controls.Add(Me.Label3)
            Me.Panel2.Controls.Add(Me.SerieTextBox)
            Me.Panel2.Controls.Add(Me.Label2)
            Me.Panel2.Controls.Add(Me.Label1)
            Me.Panel2.Controls.Add(Me.tbnumerocaja)
            Me.Panel2.Controls.Add(Me.btnAgregarCaja)
            Me.Panel2.Location = New System.Drawing.Point(6, 19)
            Me.Panel2.Name = "Panel2"
            Me.Panel2.Size = New System.Drawing.Size(625, 107)
            Me.Panel2.TabIndex = 20
            '
            'TipoArchivoComboBox
            '
            Me.TipoArchivoComboBox.FormattingEnabled = True
            Me.TipoArchivoComboBox.ItemHeight = 13
            Me.TipoArchivoComboBox.Items.AddRange(New Object() {"AG", "AC"})
            Me.TipoArchivoComboBox.Location = New System.Drawing.Point(104, 54)
            Me.TipoArchivoComboBox.Name = "TipoArchivoComboBox"
            Me.TipoArchivoComboBox.Size = New System.Drawing.Size(121, 21)
            Me.TipoArchivoComboBox.TabIndex = 1
            '
            'Label3
            '
            Me.Label3.AutoSize = True
            Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label3.Location = New System.Drawing.Point(3, 81)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(45, 15)
            Me.Label3.TabIndex = 10
            Me.Label3.Text = "Serie:"
            '
            'SerieTextBox
            '
            Me.SerieTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.SerieTextBox.Location = New System.Drawing.Point(104, 81)
            Me.SerieTextBox.Name = "SerieTextBox"
            Me.SerieTextBox.Size = New System.Drawing.Size(165, 20)
            Me.SerieTextBox.TabIndex = 2
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label2.Location = New System.Drawing.Point(3, 55)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(89, 15)
            Me.Label2.TabIndex = 8
            Me.Label2.Text = "Tipo Archivo:"
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.Location = New System.Drawing.Point(3, 29)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(40, 15)
            Me.Label1.TabIndex = 5
            Me.Label1.Text = "Caja:"
            '
            'tbnumerocaja
            '
            Me.tbnumerocaja.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.tbnumerocaja.Location = New System.Drawing.Point(104, 28)
            Me.tbnumerocaja.Name = "tbnumerocaja"
            Me.tbnumerocaja.Size = New System.Drawing.Size(306, 20)
            Me.tbnumerocaja.TabIndex = 0
            '
            'btnAgregarCaja
            '
            Me.btnAgregarCaja.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnAgregarCaja.Image = Global.UMV.Plugin.My.Resources.Resources.btnAgregar
            Me.btnAgregarCaja.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.btnAgregarCaja.Location = New System.Drawing.Point(457, 24)
            Me.btnAgregarCaja.Name = "btnAgregarCaja"
            Me.btnAgregarCaja.Size = New System.Drawing.Size(106, 30)
            Me.btnAgregarCaja.TabIndex = 7
            Me.btnAgregarCaja.Text = "Agregar Caja"
            Me.btnAgregarCaja.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnAgregarCaja.UseVisualStyleBackColor = True
            '
            'EstibasDataGridView
            '
            Me.EstibasDataGridView.AllowUserToAddRows = False
            Me.EstibasDataGridView.AllowUserToDeleteRows = False
            Me.EstibasDataGridView.AllowUserToResizeColumns = False
            Me.EstibasDataGridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.EstibasDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.EstibasDataGridView.Location = New System.Drawing.Point(15, 144)
            Me.EstibasDataGridView.MultiSelect = False
            Me.EstibasDataGridView.Name = "EstibasDataGridView"
            Me.EstibasDataGridView.RowHeadersWidth = 10
            Me.EstibasDataGridView.RowTemplate.Height = 60
            Me.EstibasDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.EstibasDataGridView.Size = New System.Drawing.Size(616, 318)
            Me.EstibasDataGridView.TabIndex = 16
            '
            'FormCreacionEstibas
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(660, 549)
            Me.Controls.Add(Me.MainGroupBox)
            Me.Name = "FormCreacionEstibas"
            Me.Text = "Creación de Estibas"
            Me.MainGroupBox.ResumeLayout(False)
            Me.Panel2.ResumeLayout(False)
            Me.Panel2.PerformLayout()
            CType(Me.EstibasDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents MainGroupBox As System.Windows.Forms.GroupBox
        Private WithEvents Panel2 As System.Windows.Forms.Panel
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents tbnumerocaja As System.Windows.Forms.TextBox
        Friend WithEvents btnAgregarCaja As System.Windows.Forms.Button
        Friend WithEvents EstibasDataGridView As System.Windows.Forms.DataGridView
        Friend WithEvents btnsalir As System.Windows.Forms.Button
        Friend WithEvents btnguardarEstiba As System.Windows.Forms.Button
        Friend WithEvents TipoArchivoComboBox As System.Windows.Forms.ComboBox
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents SerieTextBox As System.Windows.Forms.TextBox
        Friend WithEvents Label2 As System.Windows.Forms.Label
    End Class

End Namespace

