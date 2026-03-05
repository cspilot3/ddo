Imports System.Web.Services.Description
Imports Microsoft.Reporting.WinForms
Imports Miharu.Desktop.Controls.DesktopReportViewer

Namespace Reportes.Ciald

    Public Class Report_CialdCentral
        Inherits DesktopReport1

#Region " Propiedades "

        Overrides ReadOnly Property ReportName As String
            Get
                Return "Contenedor"
            End Get
        End Property

#End Region

        Public DtRegistrosCialCentral As New DBImaging.SchemaProcess.CTA_Get_Registros_Relevo_CentralDataTable

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

            ' Dim recursos = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceNames()

            'Dim warnings(0) As Warning
            'Dim streamids(0) As String
            'Dim mimeType As String = ""
            'Dim encoding As String = ""
            'Dim extension As String = ""
            ' Exporta el reporte al formato XLSX
            'Dim bytes As Byte() = Me.ReportViewer.MainReportViewer.LocalReport.Render("EXCELOPENXML", Nothing, mimeType, encoding, extension, streamids, warnings)

            'Dim filePath As String = "C:\ReporteExportado.xlsx"
            'System.IO.File.WriteAllBytes(filePath, bytes)

            Dim ReportDataSource As New ReportDataSource("DSRelevoCentral", CType(DtRegistrosCialCentral, DataTable))
            Try
                'Me.ReportViewer.MainReportViewer.LocalReport.ListRenderingExtensions()

                'For Each extension As RenderingExtension In Me.ReportViewer.MainReportViewer.LocalReport.ListRenderingExtensions()
                '    If extension.Name = "WORD" OrElse extension.Name = "WORDOPENXML" Then
                '        Dim fi As Reflection.FieldInfo = extension.GetType().GetField("m_isVisible", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic)
                '        'If fi IsNot Nothing Then
                '        '    fi.SetValue(extension, False)
                '        'End If
                '        fi?.SetValue(extension, False)
                '    End If
                'Next

                Me.ReportViewer.MainReportViewer1.Reset()
                'Me.ReportViewer.MainReportViewer.LocalReport.Render("EXCELOPENXML", Nothing, mimeType, encoding, extension, streamids, warnings)
                Me.ReportViewer.MainReportViewer1.LocalReport.ReportEmbeddedResource = "Miharu.Imaging.Library.Report_CialdCentral.rdlc"
                Me.ReportViewer.MainReportViewer1.LocalReport.DataSources.Clear()
                Me.ReportViewer.MainReportViewer1.LocalReport.DataSources.Add(ReportDataSource)
                Me.ReportViewer.MainReportViewer1.RefreshReport()
                Me.ReportViewer.MainReportViewer1.Show()
            Catch ex As Exception
                MsgBox(Err.Description)
            End Try


        End Sub

#End Region

    End Class

End Namespace