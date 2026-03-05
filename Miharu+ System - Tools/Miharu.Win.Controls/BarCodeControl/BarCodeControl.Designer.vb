Namespace BarCode

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class BarCodeControl
        Inherits System.Windows.Forms.UserControl

        'UserControl reemplaza a Dispose para limpiar la lista de componentes.
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

        'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
        'Puede modificarse utilizando el Diseñador de Windows Forms. 
        'No lo modifique con el editor de código.
        <System.Diagnostics.DebuggerStepThrough()> _
        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container
            Me.BarCodePrintDocument = New System.Drawing.Printing.PrintDocument
            Me.MainPanel = New System.Windows.Forms.Panel
            Me.MainContextMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
            Me.ImprimirToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
            Me.MainContextMenuStrip.SuspendLayout()
            Me.SuspendLayout()
            '
            'BarCodePrintDocument
            '
            '
            'MainPanel
            '
            Me.MainPanel.BackColor = System.Drawing.SystemColors.Window
            Me.MainPanel.ContextMenuStrip = Me.MainContextMenuStrip
            Me.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill
            Me.MainPanel.Location = New System.Drawing.Point(0, 0)
            Me.MainPanel.Name = "MainPanel"
            Me.MainPanel.Size = New System.Drawing.Size(272, 168)
            Me.MainPanel.TabIndex = 1
            '
            'MainContextMenuStrip
            '
            Me.MainContextMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ImprimirToolStripMenuItem})
            Me.MainContextMenuStrip.Name = "MainContextMenuStrip"
            Me.MainContextMenuStrip.Size = New System.Drawing.Size(130, 26)
            '
            'ImprimirToolStripMenuItem
            '
            Me.ImprimirToolStripMenuItem.Image = Global.Miharu.Win.Controls.My.Resources.Resources.printer
            Me.ImprimirToolStripMenuItem.Name = "ImprimirToolStripMenuItem"
            Me.ImprimirToolStripMenuItem.Size = New System.Drawing.Size(129, 22)
            Me.ImprimirToolStripMenuItem.Text = "Imprimir..."
            '
            'BarCodeControl
            '
            Me.BackColor = System.Drawing.SystemColors.Window
            Me.Controls.Add(Me.MainPanel)
            Me.Name = "BarCodeControl"
            Me.Size = New System.Drawing.Size(272, 168)
            Me.MainContextMenuStrip.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub
        Public WithEvents BarCodePrintDocument As System.Drawing.Printing.PrintDocument
        Friend WithEvents MainPanel As System.Windows.Forms.Panel
        Friend WithEvents MainContextMenuStrip As System.Windows.Forms.ContextMenuStrip
        Friend WithEvents ImprimirToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

    End Class

End Namespace