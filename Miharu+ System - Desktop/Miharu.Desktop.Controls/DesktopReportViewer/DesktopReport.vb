Namespace DesktopReportViewer

    Public MustInherit Class DesktopReport

#Region " Declaraciones "

        Private _ReportViewer As DesktopReportViewerControl

#End Region

#Region " Propiedades "

        MustOverride ReadOnly Property ReportName() As String

        Public ReadOnly Property ReportViewer() As DesktopReportViewerControl
            Get
                Return Me._ReportViewer
            End Get
        End Property

#End Region

#Region " Constructores "

        Public Sub New(ByRef nReportViewer As DesktopReportViewerControl)
            _ReportViewer = nReportViewer
        End Sub

#End Region

#Region " Metodos "

        MustOverride Sub Launch()

#End Region

    End Class

End Namespace