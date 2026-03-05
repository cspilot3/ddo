Imports Miharu.Desktop.Controls.DesktopReportViewer

Namespace Reportes.VisorReportes.Genericos

    Public Class Report_Estados_Operacion_Launch
        Inherits DesktopReport

#Region " Propiedades "

        Overrides ReadOnly Property ReportName As String
            Get
                Return "Estados Operación (Detalle)"
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
            Dim Reportes As New Report_Estados_Operacion
            Reportes.ShowDialog()
        End Sub

#End Region

    End Class

End Namespace