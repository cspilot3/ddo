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
Imports Slyg.Tools.Imaging.ImageManager

Namespace Reportes.Ciald

    Public Class Report_AnexoF88
        Inherits DesktopReport1

#Region " Propiedades "

        Overrides ReadOnly Property ReportName As String
            Get
                Return "Contenedor"
            End Get
        End Property

#End Region

        Public DtLogos As New DataTable
        Public DtRegistrosF88 As New DataTable
        Public TotalRegistro As Integer
        Public idDocumentAnexo88 As Integer
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
            'Dim NumeroMedioMagnetico = DtParametrosF11.Rows(0).Item("NumeroEnvio").ToString()
            'Dim ParamCodigoEntidad = DtParametrosF11.Rows(0).Item("CodigoEntidad").ToString()
            Dim rutaArchivoPDF As String = String.Empty

            Dim NumeroMedioMagnetico = DtRegistrosF88.Rows(0).Item("NumMedioMagnetico").ToString()
            Dim CodigoEntidad = DtRegistrosF88.Rows(0).Item("CodigoSucursal").ToString()
            Dim fk_File = CInt(DtRegistrosF88.Rows(0).Item("fk_File"))
            Dim fk_Folder = CInt(DtRegistrosF88.Rows(0).Item("fk_Folder"))
            Dim ValorInsertarFileData = DtRegistrosF88.Rows(0).Item("ValorInsertarFileData").ToString()

            Try
                Dim DtRowFirma As DataRow = DtLogos.Select("fk_Tipo_Imagen='2'")(0)
                Dim DtUnFirma As DataTable = DtLogos.Clone()
                DtUnFirma.ImportRow(DtRowFirma)

                Dim ReportDataFirma As New ReportDataSource("DtFirma", CType(DtUnFirma, DataTable))
                Dim ReportDataF88 As New ReportDataSource("DtAnexoF88", CType(DtRegistrosF88, DataTable))

                Dim ParamTotalRegistros = New ReportParameter("TotalRegistros", TotalRegistro.ToString())

                Me.ReportViewer.MainReportViewer1.Reset()
                Me.ReportViewer.MainReportViewer1.LocalReport.ReportEmbeddedResource = "Miharu.Imaging.Library.Report_AnexoF88.rdlc"
                Me.ReportViewer.MainReportViewer1.LocalReport.SetParameters(ParamTotalRegistros)
                Me.ReportViewer.MainReportViewer1.LocalReport.DataSources.Clear()
                Me.ReportViewer.MainReportViewer1.LocalReport.DataSources.Add(ReportDataF88)
                Me.ReportViewer.MainReportViewer1.LocalReport.DataSources.Add(ReportDataFirma)
                Me.ReportViewer.MainReportViewer1.RefreshReport()
                Me.ReportViewer.MainReportViewer1.Show()

                Dim imagingCreator As New ImagingFolioCreator(NumeroMedioMagnetico, CodigoEntidad)

                ' Nombre Archivo PDF del anexo
                Dim nameFilePDF As String = $"AnexoF88_{CodigoEntidad}{NumeroMedioMagnetico}.pdf"

                ' Ruta del arcivo PDF del anexo
                rutaArchivoPDF = imagingCreator.RenderReportToTempPdf(Me.ReportViewer.MainReportViewer1, nameFilePDF)
                If String.IsNullOrEmpty(rutaArchivoPDF) Then
                    Throw New ApplicationException("No fue posible generar y almacenar el PDF temporalmente.")
                End If

                Dim FileProcesado = imagingCreator.ProcessPdfExpedientStorage(rutaArchivoPDF, fkExpedienteAsociado, 1, fk_File, idDocumentAnexo88, False)

                If (fk_File = 0) Then

                    ' Insertar Campos de Anexo f88
                    Dim queryResponseParam As QueryResponse = ClientUtil.resolver("[DB_Miharu.Imaging_Core].[Process].[PA_Insert_Campos_Anexos]", New List(Of QueryParameter) From {
                                      New QueryParameter With {.name = "fk_Entidad", .value = Program.ImagingGlobal.Entidad.ToString()},
                                      New QueryParameter With {.name = "fk_Proyecto", .value = Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto.ToString()},
                                      New QueryParameter With {.name = "fk_Documento", .value = idDocumentAnexo88.ToString()},
                                      New QueryParameter With {.name = "fk_Expediente", .value = fkExpedienteAsociado.ToString},
                                      New QueryParameter With {.name = "fk_Folder", .value = 1.ToString},
                                      New QueryParameter With {.name = "fk_File", .value = FileProcesado.ToString},
                                      New QueryParameter With {.name = "ValorInsertarFileData", .value = ValorInsertarFileData.ToString}
                                    }, QueryRequestType.StoredProcedure, QueryResponseType.NonQuery)

                    If queryResponseParam.error IsNot Nothing Then
                        Throw New ApplicationException("Error insertando Campos del Anexo F88: " + queryResponseParam.error)
                    End If
                End If

            Catch ex As Exception
                MsgBox(Err.Description)
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