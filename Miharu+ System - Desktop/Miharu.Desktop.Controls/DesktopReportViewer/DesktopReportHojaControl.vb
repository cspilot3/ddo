Imports System.Windows.Forms

Namespace DesktopReportViewer

    Public MustInherit Class DesktopReportHojaControl

#Region " Declaraciones "

        Private _ReportViewer As DesktopReportViewerHojaControl

#End Region

#Region " Propiedades "

        MustOverride ReadOnly Property ReportName() As String

        Public ReadOnly Property ReportViewer() As DesktopReportViewerHojaControl
            Get
                Return Me._ReportViewer
            End Get
        End Property

#End Region

#Region " Constructores "

        Public Sub New(ByRef nReportViewer As DesktopReportViewerHojaControl)
            _ReportViewer = nReportViewer
        End Sub

#End Region

#Region " Metodos "

        MustOverride Sub Launch(Hoja As DataTable)
        MustOverride Sub Launch(ByVal Ruta As String, ByVal Cedulas As DataTable)

#End Region

    End Class

End Namespace