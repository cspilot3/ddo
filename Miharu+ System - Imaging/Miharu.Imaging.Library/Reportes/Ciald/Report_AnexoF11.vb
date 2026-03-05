Imports System.IO
Imports System.Linq
Imports System.Text
Imports System.Web.Services.Description
Imports System.Web.UI.WebControls.Expressions
Imports Microsoft.Reporting.WinForms
Imports Miharu.Desktop.Controls.DesktopReportViewer
Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Library.MiharuDMZ
Imports Miharu.Imaging.Indexer.Generic
Imports Slyg.Tools.Imaging
Imports Slyg.Tools.Imaging.FreeImageAPI
Imports Slyg.Tools.Imaging.ImageManager

Namespace Reportes.Ciald

    Public Class Report_AnexoF11
        Inherits DesktopReport1

#Region " Propiedades "

        Overrides ReadOnly Property ReportName As String
            Get
                Return "Contenedor"
            End Get
        End Property

#End Region

        Public DtLogos As New DataTable
        Public DtParametrosF11 As New DataTable
        Public DtRegistrosF11 As New DataTable
        Public idDocumentAnexo11 As Integer
        Public fkExpedienteAsociado As Integer

        Private MaxThumbnailWidth As Integer = 60
        Private MaxThumbnailHeight As Integer = 80

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

            Dim NumeroMedioMagnetico = DtParametrosF11.Rows(0).Item("NumeroEnvio").ToString()
            Dim CodigoEntidad = DtParametrosF11.Rows(0).Item("CodigoEntidad").ToString()
            Dim ValorTotal = DtParametrosF11.Rows(0).Item("ValorTotalFormularios").ToString()
            Dim CantidadFormularios = DtParametrosF11.Rows(0).Item("CantidadFormularios").ToString()
            Dim fk_Folder = CInt(DtParametrosF11.Rows(0).Item("fk_Folder"))
            Dim fk_File = CInt(DtParametrosF11.Rows(0).Item("fk_File"))
            Dim NombreResponsable = DtParametrosF11.Rows(0).Item("NombreResponsable").ToString()
            Dim ValorInsertarFileData = DtParametrosF11.Rows(0).Item("ValoresInsertFileData").ToString()


            Dim Parameter_NomEntidad = New ReportParameter("NomEntidad", DtParametrosF11.Rows(0).Item("NombreEntidad").ToString())
            Dim Parameter_CodigoEntidad = New ReportParameter("CodigoEntidad", CodigoEntidad)
            Dim Parameter_NumeroEnvio = New ReportParameter("NumeroEnvio", NumeroMedioMagnetico)
            Dim Parameter_CantidadFormularios = New ReportParameter("CantidadFormularios", CantidadFormularios)
            Dim Parameter_ValorTotal = New ReportParameter("ValorTotal", ValorTotal)
            Dim Parameter_FechaRec = New ReportParameter("FechaRecaudo", DtParametrosF11.Rows(0).Item("FechaRecaudo").ToString())
            Dim Parameter_NombreResponsable = New ReportParameter("NombreResponsable", NombreResponsable)


            Dim rutaArchivoPDF As String = String.Empty

            Try
                Dim DtRowEncabezado As DataRow = DtLogos.Select("fk_Tipo_Imagen='1'")(0)
                Dim DtRowFirma As DataRow = DtLogos.Select("fk_Tipo_Imagen='2'")(0)
                Dim DtRowPie As DataRow = DtLogos.Select("fk_Tipo_Imagen='3'")(0)

                Dim DtUnEncabezado As DataTable = DtLogos.Clone()
                Dim DtUnFirma As DataTable = DtLogos.Clone()
                Dim DtUnPie As DataTable = DtLogos.Clone()

                DtUnEncabezado.ImportRow(DtRowEncabezado)
                DtUnFirma.ImportRow(DtRowFirma)
                DtUnPie.ImportRow(DtRowPie)

                Dim ReportDataEnca As New ReportDataSource("DtLogoEncabezado", CType(DtUnEncabezado, DataTable))
                Dim ReportDataFirma As New ReportDataSource("DtFirma", CType(DtUnFirma, DataTable))
                Dim ReportDataPie As New ReportDataSource("DtPiePagina", CType(DtUnPie, DataTable))

                Dim ReportDataF11 As New ReportDataSource("dtAnexoF11", CType(DtRegistrosF11, DataTable))

                Me.ReportViewer.MainReportViewer1.Reset()
                Me.ReportViewer.MainReportViewer1.LocalReport.ReportEmbeddedResource = "Miharu.Imaging.Library.Report_AnexoF11.rdlc"
                Me.ReportViewer.MainReportViewer1.LocalReport.SetParameters(Parameter_NomEntidad)
                Me.ReportViewer.MainReportViewer1.LocalReport.SetParameters(Parameter_CodigoEntidad)
                Me.ReportViewer.MainReportViewer1.LocalReport.SetParameters(Parameter_NumeroEnvio)
                Me.ReportViewer.MainReportViewer1.LocalReport.SetParameters(Parameter_CantidadFormularios)
                Me.ReportViewer.MainReportViewer1.LocalReport.SetParameters(Parameter_ValorTotal)
                Me.ReportViewer.MainReportViewer1.LocalReport.SetParameters(Parameter_FechaRec)
                Me.ReportViewer.MainReportViewer1.LocalReport.SetParameters(Parameter_NombreResponsable)
                'Me.ReportViewer.MainReportViewer.SetDisplayMode(DisplayMode.PrintLayout)
                Me.ReportViewer.MainReportViewer1.LocalReport.DataSources.Clear()
                Me.ReportViewer.MainReportViewer1.LocalReport.DataSources.Add(ReportDataF11)
                Me.ReportViewer.MainReportViewer1.LocalReport.DataSources.Add(ReportDataEnca)
                Me.ReportViewer.MainReportViewer1.LocalReport.DataSources.Add(ReportDataFirma)
                Me.ReportViewer.MainReportViewer1.LocalReport.DataSources.Add(ReportDataPie)
                Me.ReportViewer.MainReportViewer1.RefreshReport()
                Me.ReportViewer.MainReportViewer1.Show()

                Dim imagingCreator As New ImagingFolioCreator(NumeroMedioMagnetico, CodigoEntidad)

                ' Nombre Archivo PDF del anexo
                Dim nameFilePDF As String = $"AnexoF11_{CodigoEntidad}{NumeroMedioMagnetico}.pdf"

                ' Ruta del arcivo PDF del anexo
                rutaArchivoPDF = imagingCreator.RenderReportToTempPdf(Me.ReportViewer.MainReportViewer1, nameFilePDF)
                If String.IsNullOrEmpty(rutaArchivoPDF) Then
                    Throw New ApplicationException("No fue posible generar y almacenar el PDF temporalmente.")
                End If

                Dim FileProcesado = imagingCreator.ProcessPdfExpedientStorage(rutaArchivoPDF, fkExpedienteAsociado, 1, fk_File, idDocumentAnexo11, False)

                If (fk_File = 0) Then

                    ' Insertar Campos de Anexo 11
                    Dim queryResponseParam As QueryResponse = ClientUtil.resolver("[DB_Miharu.Imaging_Core].[Process].[PA_Insert_Campos_AnexoF11]", New List(Of QueryParameter) From {
                                      New QueryParameter With {.name = "fk_Entidad", .value = Program.ImagingGlobal.Entidad.ToString()},
                                      New QueryParameter With {.name = "fk_Proyecto", .value = Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto.ToString()},
                                      New QueryParameter With {.name = "fk_Documento", .value = idDocumentAnexo11.ToString()},
                                      New QueryParameter With {.name = "fk_Expediente", .value = fkExpedienteAsociado.ToString},
                                      New QueryParameter With {.name = "fk_Folder", .value = 1.ToString},
                                      New QueryParameter With {.name = "fk_File", .value = FileProcesado.ToString},
                                      New QueryParameter With {.name = "ValorInsertarFileData", .value = ValorInsertarFileData.ToString}
                                    }, QueryRequestType.StoredProcedure, QueryResponseType.NonQuery)

                    If queryResponseParam.error IsNot Nothing Then
                        Throw New ApplicationException("Error insertando Campos del Anexo 11: " + queryResponseParam.error)
                    End If
                End If

            Catch ex As Exception
                Throw
            Finally
                If Not String.IsNullOrEmpty(rutaArchivoPDF) AndAlso System.IO.File.Exists(rutaArchivoPDF) Then
                    System.IO.File.Delete(rutaArchivoPDF)
                End If
            End Try


        End Sub

#End Region

#Region " Funciones "

#End Region


    End Class

End Namespace