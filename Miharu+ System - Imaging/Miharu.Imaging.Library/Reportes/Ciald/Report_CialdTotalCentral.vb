Imports Microsoft.Reporting.WinForms
Imports Miharu.Desktop.Controls.DesktopReportViewer

Namespace Reportes.Ciald

    Public Class Report_CialdTotalCentral
        Inherits DesktopReport1

#Region " Propiedades "

        Overrides ReadOnly Property ReportName As String
            Get
                Return "Contenedor"
            End Get
        End Property

#End Region

        Public DtRegTotalCentral As New DataTable

#Region " Constructores "

        Public Sub New(ByRef nReportViewer As DesktopReportViewer1Control)
            MyBase.New(nReportViewer)
        End Sub

#End Region

#Region " Metodos "

        ' Método requerido #1
        Public Overrides Sub Launch(datatableDestinatario As DataTable, solicitudSeleccionada As Integer, nombres As String, direccion As String, sede As String, precinto As String)
            Throw New NotImplementedException("Este método no está implementado en Report_Acta_CialdLocal")
        End Sub

        ' Método requerido #2
        Public Overrides Sub Launch(Carpeta As String)
            Throw New NotImplementedException("Este método no está implementado en Report_Acta_CialdLocal")
        End Sub

        ' Método requerido #3
        Public Overrides Sub Launch(Caja As String, OT As Integer, RotuloCarpetas As Boolean, Reportegenericofuid As Boolean, TipoFuid As Integer, TipoGestion As String)
            Throw New NotImplementedException("Este método no está implementado en Report_Acta_CialdLocal")
        End Sub

        Public Overrides Sub Launch(FechaRecaudo As Integer)
            'Dim recursos = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceNames()

            Dim DataTotalSource As New ReportDataSource("DSRelevoTotalCentral", CType(DtRegTotalCentral, DataTable))
            Try
                Me.ReportViewer.MainReportViewer1.Reset()
                'Me.ReportViewer.MainReportViewer.LocalReport.Render("ExcelOpenXML", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                Me.ReportViewer.MainReportViewer1.LocalReport.ReportEmbeddedResource = "Miharu.Imaging.Library.Report_CialdTotalCentral.rdlc"
                Me.ReportViewer.MainReportViewer1.LocalReport.DataSources.Clear()
                Me.ReportViewer.MainReportViewer1.LocalReport.DataSources.Add(DataTotalSource)
                Me.ReportViewer.MainReportViewer1.RefreshReport()
                Me.ReportViewer.MainReportViewer1.Show()
            Catch ex As Exception
                MsgBox(Err.Description)
            End Try


        End Sub

#End Region

    End Class

End Namespace