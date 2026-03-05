Imports System.Runtime.ConstrainedExecution
Imports System.Web.Services.Description
Imports Microsoft.Reporting.WinForms
Imports Microsoft.Win32
Imports Miharu.Desktop.Controls.DesktopReportViewer
Imports Miharu.Desktop.Library.MiharuDMZ
Imports Slyg.Tools.Imaging
Imports Slyg.Tools.Progress

Namespace Reportes.Ciald

    Public Class Report_Anexo5
        Inherits DesktopReport1

#Region " Propiedades "

        Overrides ReadOnly Property ReportName As String
            Get
                Return "Contenedor"
            End Get
        End Property

#End Region

        Public DtRegistrosA5 As New DataTable
        Public DtLogos As New DataTable
        Public NumeroMedioMagnetico As String
        Public ParamCodigoEntidad As String
        Public idDocumentAnexo5 As Integer
        Public fkExpedienteAsociado As Integer

        Public TotReport As Integer
        Public TotRep As Integer
        Private ProgressForm As New FormProgress()

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
            Dim DtRowEncabezado As DataRow = DtLogos.Select("fk_Tipo_Imagen='1'")(0)
            Dim DtRowFirma As DataRow = DtLogos.Select("fk_Tipo_Imagen='2'")(0)
            Dim DtRowPie As DataRow = DtLogos.Select("fk_Tipo_Imagen='3'")(0)

            Dim DtUnEncabezado As DataTable = DtLogos.Clone()
            Dim DtUnFirma As DataTable = DtLogos.Clone()
            Dim DtUnPie As DataTable = DtLogos.Clone()

            DtUnEncabezado.ImportRow(DtRowEncabezado)
            DtUnFirma.ImportRow(DtRowFirma)
            DtUnPie.ImportRow(DtRowPie)

            Dim ReportDataEnca As New ReportDataSource("DtEncabezado", CType(DtUnEncabezado, DataTable))
            Dim ReportDataFirma As New ReportDataSource("DtFirma", CType(DtUnFirma, DataTable))
            Dim ReportDataPie As New ReportDataSource("DtPiePagina", CType(DtUnPie, DataTable))

            Dim ReportDataA5 As New ReportDataSource("DtAnexo5", CType(DtRegistrosA5, DataTable))

            Dim rutaArchivoPDF As String = String.Empty

            Try
                Me.ReportViewer.MainReportViewer1.Reset()
                Me.ReportViewer.MainReportViewer1.LocalReport.ReportEmbeddedResource = "Miharu.Imaging.Library.Report_Anexo5.rdlc"
                Me.ReportViewer.MainReportViewer1.SetDisplayMode(DisplayMode.PrintLayout)
                Me.ReportViewer.MainReportViewer1.LocalReport.DataSources.Clear()
                Me.ReportViewer.MainReportViewer1.LocalReport.DataSources.Add(ReportDataA5)
                Me.ReportViewer.MainReportViewer1.LocalReport.DataSources.Add(ReportDataEnca)
                Me.ReportViewer.MainReportViewer1.LocalReport.DataSources.Add(ReportDataFirma)
                Me.ReportViewer.MainReportViewer1.LocalReport.DataSources.Add(ReportDataPie)
                Me.ReportViewer.MainReportViewer1.RefreshReport()
                Me.ReportViewer.MainReportViewer1.Show()

                Dim imagingCreator As New ImagingFolioCreator(NumeroMedioMagnetico, ParamCodigoEntidad)

                ' Nombre Archivo PDF del anexo
                Dim nameFilePDF As String = $"Anexo5_{ParamCodigoEntidad}{NumeroMedioMagnetico}.pdf"

                ' Ruta del arcivo PDF del anexo
                rutaArchivoPDF = imagingCreator.RenderReportToTempPdf(Me.ReportViewer.MainReportViewer1, nameFilePDF)
                If String.IsNullOrEmpty(rutaArchivoPDF) Then
                    Throw New ApplicationException("No fue posible generar y almacenar el PDF temporalmente.")
                End If

                Dim totalFoliosImage = Convert.ToInt16(ImageManager.GetFolios(rutaArchivoPDF))

                If DtRegistrosA5.Rows.Count <> totalFoliosImage Then
                    Throw New ApplicationException("Error al generar los expedientes: la cantidad de registros creados no coincide con el número de folios procesados. Verifique los datos e intente nuevamente.")
                End If
                Dim folioActual As Integer = 0

                'Iniciar proceso                
                ProgressForm.Process = "Generación de reportes"
                ProgressForm.Action = "Generando Anexo 5"
                ProgressForm.ValueProcess = CInt((TotRep * 100) / TotReport)
                ProgressForm.ValueAction = 0
                ProgressForm.TopMost = True
                ProgressForm.MaxValueAction = DtRegistrosA5.Rows.Count
                ProgressForm.Show()
                ProgressForm.MaxValueProcess = TotReport

                For Each RowRegistrosA5 As DataRow In DtRegistrosA5.Rows
                    Dim fk_Folder = CInt(RowRegistrosA5.Item("fk_Folder"))
                    Dim fk_File = CInt(RowRegistrosA5.Item("fk_File"))
                    Dim Numero_Referencia = RowRegistrosA5.Item("NumeroReferencia").ToString().Trim()
                    Dim ValorInsertarFileData = RowRegistrosA5.Item("ValorInsertarFileData").ToString()
                    folioActual += 1
                    ProgressForm.ValueAction = folioActual
                    Application.DoEvents()

                    Dim FileProcesado = imagingCreator.ProcessPdfExpedientStorage(rutaArchivoPDF, fkExpedienteAsociado, 1, fk_File, idDocumentAnexo5, True, folioActual)

                    If (fk_File = 0) Then
                        ' Insertar Campos de Anexo 5
                        Dim queryResponseParam As QueryResponse = ClientUtil.resolver("[DB_Miharu.Imaging_Core].[Process].[PA_Insert_Campos_Anexos]", New List(Of QueryParameter) From {
                                          New QueryParameter With {.name = "fk_Entidad", .value = Program.ImagingGlobal.Entidad.ToString()},
                                          New QueryParameter With {.name = "fk_Proyecto", .value = Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto.ToString()},
                                          New QueryParameter With {.name = "fk_Documento", .value = idDocumentAnexo5.ToString()},
                                          New QueryParameter With {.name = "fk_Expediente", .value = fkExpedienteAsociado.ToString()},
                                          New QueryParameter With {.name = "fk_Folder", .value = 1.ToString()},
                                          New QueryParameter With {.name = "fk_File", .value = FileProcesado.ToString()},
                                          New QueryParameter With {.name = "ValorInsertarFileData", .value = ValorInsertarFileData}
                                        }, QueryRequestType.StoredProcedure, QueryResponseType.NonQuery)

                        If queryResponseParam.error IsNot Nothing Then
                            Throw New ApplicationException("Error insertando Campos del Anexo 5: " + queryResponseParam.error)
                        End If
                    End If
                Next
                ProgressForm.Visible = False
                ProgressForm.Hide()

                ' Marca Eliminar los files que no se encuentran y previamente se habian creado
                Dim queryResponseAnexo5 As QueryResponse = ClientUtil.resolver("[DB_Miharu.Imaging_Core].[Process].[PA_Marcar_Eliminar_Files_Anexo5]", New List(Of QueryParameter) From {
                             New QueryParameter With {.name = "fk_Entidad", .value = Program.ImagingGlobal.Entidad.ToString()},
                             New QueryParameter With {.name = "fk_Proyecto", .value = Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto.ToString()},
                             New QueryParameter With {.name = "fk_Fecha_Recaudo", .value = FechaRecaudo.ToString()},
                             New QueryParameter With {.name = "MedioMagnetico", .value = NumeroMedioMagnetico.ToString()},
                             New QueryParameter With {.name = "fk_Expediente", .value = fkExpedienteAsociado.ToString},
                             New QueryParameter With {.name = "fk_Documento", .value = idDocumentAnexo5.ToString}
                            }, QueryRequestType.StoredProcedure, QueryResponseType.Table)

            Catch ex As Exception
                Throw
                ProgressForm.Visible = False
                ProgressForm.Hide()
            Finally
                If Not String.IsNullOrEmpty(rutaArchivoPDF) AndAlso System.IO.File.Exists(rutaArchivoPDF) Then
                    System.IO.File.Delete(rutaArchivoPDF)
                End If
            End Try
        End Sub

#End Region

    End Class

End Namespace