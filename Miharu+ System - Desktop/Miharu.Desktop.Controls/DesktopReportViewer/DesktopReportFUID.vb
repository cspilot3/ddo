Imports System.Windows.Forms

Namespace DesktopReportViewer

    Public MustInherit Class DesktopReportFUID

#Region " Declaraciones "

        Private _ReportViewer As DesktopReportViewerFUID

#End Region

#Region " Propiedades "

        MustOverride ReadOnly Property ReportName() As String

        Public ReadOnly Property ReportViewer() As DesktopReportViewerFUID
            Get
                Return Me._ReportViewer
            End Get
        End Property

#End Region

#Region " Constructores "

        Public Sub New(ByRef nReportViewer As DesktopReportViewerFUID)
            _ReportViewer = nReportViewer
        End Sub

#End Region

#Region " Metodos "

        MustOverride Sub Launch(reportFuid As DataTable, tipoFuid As Integer, Fuidgeneral As Boolean, tipoGestion As String)

#End Region

    End Class

End Namespace