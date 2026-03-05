Namespace DesktopReportViewer

    Public MustInherit Class DesktopReport1

#Region " Declaraciones "

        Private _ReportViewer As DesktopReportViewer1Control

#End Region

#Region " Propiedades "

        MustOverride ReadOnly Property ReportName() As String

        Public ReadOnly Property ReportViewer() As DesktopReportViewer1Control
            Get
                Return Me._ReportViewer
            End Get
        End Property

#End Region

#Region " Constructores "

        Public Sub New(ByRef nReportViewer As DesktopReportViewer1Control)
            _ReportViewer = nReportViewer
        End Sub

#End Region

#Region " Metodos "

        MustOverride Sub Launch(datatableDestinatario As DataTable, solicitudSeleccionada As Integer, nombres As String, direccion As String, sede As String, precinto As String)
        MustOverride Sub Launch(Carpeta As String)
        MustOverride Sub Launch(Caja As String, OT As Integer, RotuloCarpetas As Boolean, Reportegenericofuid As Boolean, TipoFuid As Integer, TipoGestion As String)
        MustOverride Sub Launch(FechaRecaudo As Integer)
#End Region



    End Class

End Namespace