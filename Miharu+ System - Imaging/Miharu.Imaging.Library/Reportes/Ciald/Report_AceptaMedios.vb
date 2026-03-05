Imports System.Web.Services.Description
Imports Microsoft.Reporting.WinForms
Imports Miharu.Desktop.Controls.DesktopReportViewer
Imports Slyg.Tools.Imaging

Namespace Reportes.Ciald

    Public Class Report_AceptaMedios
        Inherits DesktopReport1

#Region " Propiedades "

        Overrides ReadOnly Property ReportName As String
            Get
                Return "Contenedor"
            End Get
        End Property

#End Region

        Public DtAceptaMedios As New DataTable

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

            ' Exporta el reporte al formato XLSX
            'Dim bytes As Byte() = Me.ReportViewer.MainReportViewer.LocalReport.Render("EXCELOPENXML", Nothing, mimeType, encoding, extension, streamids, warnings)

            Dim DsAceptarMedio As New DS_AceptaMedio
            Dim Row2 As DataRow
            For i = 0 To DtAceptaMedios.Rows.Count - 1
                Row2 = DsAceptarMedio.Medios.NewMediosRow
                Row2("Imagen") = DtAceptaMedios.Rows(i).Item("Image_Binary")
                Row2("MiniImagen") = DtAceptaMedios.Rows(i).Item("Thumbnail_Binary")
                DsAceptarMedio.Medios.AddMediosRow(CType(Row2, DS_AceptaMedio.MediosRow))
            Next

            Dim ReportDataAM As New ReportDataSource("DsAceptacionMedios", CType(DsAceptarMedio.Medios, DataTable))

            Dim rutaArchivoPDF As String = String.Empty

            Try
                'Me.ReportViewer.MainReportViewer.LocalReport.ListRenderingExtensions()

                Me.ReportViewer.MainReportViewer1.Reset()
                Me.ReportViewer.MainReportViewer1.LocalReport.DataSources.Clear()
                Me.ReportViewer.MainReportViewer1.LocalReport.ReportEmbeddedResource = "Miharu.Imaging.Library.Report_AceptaMedios.rdlc"
                'Me.ReportViewer.MainReportViewer1.SetDisplayMode(DisplayMode.PrintLayout)
                Me.ReportViewer.MainReportViewer1.LocalReport.DataSources.Add(ReportDataAM)
                Me.ReportViewer.MainReportViewer1.RefreshReport()
                Me.ReportViewer.MainReportViewer1.Show()

            Catch ex As Exception
                MsgBox(Err.Description)
            Finally
                If Not String.IsNullOrEmpty(rutaArchivoPDF) AndAlso System.IO.File.Exists(rutaArchivoPDF) Then
                    System.IO.File.Delete(rutaArchivoPDF)
                End If
            End Try


        End Sub

#End Region

    End Class



End Namespace