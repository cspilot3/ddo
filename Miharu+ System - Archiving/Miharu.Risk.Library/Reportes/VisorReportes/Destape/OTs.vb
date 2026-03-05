
Imports Miharu.Desktop.Controls.DesktopReportViewer

Namespace Reportes.VisorReportes.Destape

    Public Class OTs
        Inherits DesktopReport

#Region " Propiedades "

        Overrides ReadOnly Property ReportName As String
            Get
                Return "OTs"
            End Get
        End Property
#End Region

#Region " Constructores "

        Public Sub New(ByRef nReportViewer As DesktopReportViewerControl)
            MyBase.New(nReportViewer)

        End Sub

#End Region

#Region " Metodos "

        Public Overrides Sub Launch()
            Dim Ots As New FormReporteOTs
            Ots.ShowDialog()

        End Sub

#End Region

    End Class

End Namespace