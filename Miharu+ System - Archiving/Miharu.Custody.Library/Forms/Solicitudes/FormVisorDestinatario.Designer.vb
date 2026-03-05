<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormVisorDestinatario
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
        Me.components = New System.ComponentModel.Container()
        Me.RPT_Guia_Despacho_Solicitudes_EncabezadoDataTableBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.RPT_Guia_Despacho_SolicitudesDataTableBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.WorkSpaceViewerControl = New Miharu.Desktop.Controls.DesktopReportViewer.DesktopReportViewer1Control()
        CType(Me.RPT_Guia_Despacho_Solicitudes_EncabezadoDataTableBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RPT_Guia_Despacho_SolicitudesDataTableBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RPT_Guia_Despacho_Solicitudes_EncabezadoDataTableBindingSource
        '
        Me.RPT_Guia_Despacho_Solicitudes_EncabezadoDataTableBindingSource.DataSource = GetType(DBArchiving.Schemadbo.RPT_Guia_Despacho_Solicitudes_EncabezadoDataTable)
        '
        'RPT_Guia_Despacho_SolicitudesDataTableBindingSource
        '
        Me.RPT_Guia_Despacho_SolicitudesDataTableBindingSource.DataSource = GetType(DBArchiving.Schemadbo.RPT_Guia_Despacho_SolicitudesDataTable)
        '
        'WorkSpaceViewerControl
        '
        Me.WorkSpaceViewerControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WorkSpaceViewerControl.Location = New System.Drawing.Point(0, 0)
        Me.WorkSpaceViewerControl.Name = "WorkSpaceViewerControl"
        Me.WorkSpaceViewerControl.Size = New System.Drawing.Size(686, 470)
        Me.WorkSpaceViewerControl.TabIndex = 21
        '
        'FormVisorDestinatario
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(686, 470)
        Me.Controls.Add(Me.WorkSpaceViewerControl)
        Me.Name = "FormVisorDestinatario"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Salida documentos de custodia"
        CType(Me.RPT_Guia_Despacho_Solicitudes_EncabezadoDataTableBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RPT_Guia_Despacho_SolicitudesDataTableBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RPT_Guia_Despacho_Solicitudes_EncabezadoDataTableBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents RPT_Guia_Despacho_SolicitudesDataTableBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents WorkSpaceViewerControl As Miharu.Desktop.Controls.DesktopReportViewer.DesktopReportViewer1Control
End Class
