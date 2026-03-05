Imports Miharu.Desktop.Controls.DesktopReportViewer
Imports Microsoft.Reporting.WinForms
Imports System.IO
Imports System.Drawing
Imports System.Drawing.Imaging
Imports Slyg.Tools.Imaging
Imports Miharu.FileProvider.Library

Public Class CrearHojaControl
    Inherits DesktopReportHojaControl

    Private FormatoHojaControl As String
    Private TipoArchivo As String

    Public Sub New(ByRef nReportViewer As DesktopReportViewerHojaControl)
        MyBase.New(nReportViewer)

    End Sub



    Public Overrides Sub Launch(Hoja As DataTable)

    End Sub

    Public Overrides Sub Launch(Ruta As String, Cedulas As DataTable)

    End Sub

    Public Overrides ReadOnly Property ReportName As String
        Get
            Return "Formato Rotulo Hoja de Control"
        End Get
    End Property

    Public Sub ObtenerReporteTotal(cedulas As DataTable, ruta As String, tipo As String)

        Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
        Dim tempPath = Program.AppPath & Program.TempPath
        Dim ByteImagen() As Byte = Nothing
        Dim manager As FileProviderManager = Nothing

        dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
        dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

        Dim servidor = dbmImaging.SchemaCore.CTA_Servidor.DBFindByfk_Entidadid_Servidor(Program.ImagingGlobal.ProyectoImagingRow.fk_Entidad_Servidor, Program.ImagingGlobal.ProyectoImagingRow.fk_Servidor)(0).ToCTA_ServidorSimpleType
        Dim centro = dbmImaging.SchemaSecurity.CTA_Centro_Procesamiento.DBFindByfk_Entidadfk_Sedeid_Centro_Procesamiento(Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede, Program.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento)(0).ToCTA_Centro_ProcesamientoSimpleType()
        manager = New FileProviderManager(servidor, centro, dbmImaging, Program.Sesion.Usuario.id)
        manager.Connect()
        Try

            For Each row As DataRow In cedulas.Rows

                Dim Cedula = row.Item("Cedulas").ToString

                If tipo = "Guardar" Then
                    Dim UltimoFile = dbmImaging.SchemaProcess.PA_Crear_Hoja_Control.DBExecute(Cedula, "UltimoFile", Program.Sesion.Usuario.id, 0, Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto)
                    Dim Hoja = dbmImaging.SchemaProcess.PA_Crear_Hoja_Control.DBExecute(Cedula, "Generar", Program.Sesion.Usuario.id, 0, Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto)
                    If (Hoja.Rows.Count > 0) Then
                        dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                        dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
                        Dim FechaElaboracion = dbmImaging.SchemaConfig.TBL_Parametro_Sistema.DBGet("@FechaElaboracionHC").Item(0).Valor_Parametro_Sistema

                        Me.ReportViewer.MainReportViewer1.Reset()
                        Me.ReportViewer.MainReportViewer1.LocalReport.ReportEmbeddedResource = "Miharu.Imaging.Library.ReportHojaControl.rdlc"
                        Me.ReportViewer.MainReportViewer1.LocalReport.DataSources.Clear()
                        Me.ReportViewer.MainReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("HojaControl", Hoja))
                        Me.ReportViewer.MainReportViewer1.LocalReport.SetParameters(New ReportParameter("FECHAELABORACION", FechaElaboracion))

                        Me.ReportViewer.MainReportViewer1.SetDisplayMode(DisplayMode.PrintLayout)
                        Me.ReportViewer.MainReportViewer1.RefreshReport()


                        If File.Exists(ruta & "\" & Cedula & ".xls") Then
                            My.Computer.FileSystem.DeleteFile(ruta & "\" & Cedula & ".xls")
                        End If
                        If File.Exists(ruta & "\" & Cedula & ".pdf") Then
                            My.Computer.FileSystem.DeleteFile(ruta & "\" & Cedula & ".pdf")
                        End If

                        Dim bytesExcel As Byte() = Me.ReportViewer.MainReportViewer1.LocalReport.Render("Excel", Nothing, Nothing, Nothing, ".xls", Nothing, Nothing)
                        Dim bytesPdf As Byte() = Me.ReportViewer.MainReportViewer1.LocalReport.Render("PDF", Nothing, Nothing, Nothing, ".pdf", Nothing, Nothing)

                        Dim writers As New MemoryStream(bytesExcel)
                        Using files = New FileStream(ruta & "\" & Cedula & "." & ".xls", FileMode.Append, FileAccess.Write)
                            writers.WriteTo(files)
                            writers.Close()
                        End Using
                        Dim writersPdf As New MemoryStream(bytesPdf)
                        Using filespdf = New FileStream(ruta & "\" & Cedula & "." & ".pdf", FileMode.Append, FileAccess.Write)
                            writersPdf.WriteTo(filespdf)
                            writersPdf.Close()
                        End Using

                        ByteImagen = Me.ReportViewer.MainReportViewer1.LocalReport.Render("Image", "<DeviceInfo><OutputFormat>TIFF</OutputFormat></DeviceInfo>", "image/tiff", Nothing, ".tiff", Nothing, Nothing)

                        Dim writer As New MemoryStream(ByteImagen)

                        Using file = New FileStream(tempPath & "\" & Cedula & Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Entrada.ToString(), FileMode.Append, FileAccess.Write)
                            writer.WriteTo(file)
                            writer.Close()
                        End Using

                        Dim PageCount As Integer
                        Dim theTIFF As Image = Image.FromFile(tempPath & "\" & Cedula & Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Entrada)
                        PageCount = theTIFF.GetFrameCount(FrameDimension.Page) * 2
                        theTIFF.Dispose()

                        dbmImaging.SchemaProcess.PA_Crear_Hoja_Control.DBExecute(Cedula, "Guardar", Program.Sesion.Usuario.id, PageCount, Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto)

                        Dim dataImage = ImageManager.GetData(tempPath & "\" & Cedula & Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Entrada)
                        Dim dataImageThumbnail = ImageManager.GetThumbnailData(tempPath & "\" & Cedula & Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Entrada, 1, 1, 60, 60)


                        manager.CreateItem(CLng(UltimoFile.Rows(0)(0)), CShort(UltimoFile.Rows(0)(1)), CShort(UltimoFile.Rows(0)(2)), 1, Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida, UltimoFile.Rows(0).Field(Of Guid)(3))

                        manager.CreateFolio(CLng(UltimoFile.Rows(0)(0)), CShort(UltimoFile.Rows(0)(1)), CShort(UltimoFile.Rows(0)(2)), 1, CShort(1), dataImage, dataImageThumbnail(0), False)

                        My.Computer.FileSystem.DeleteFile(tempPath & "\" & Cedula & Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Entrada)
                    End If
                ElseIf tipo = "Imprimir" Then
                    Dim Hoja = dbmImaging.SchemaProcess.PA_Crear_Hoja_Control.DBExecute(Cedula, "Generar", Program.Sesion.Usuario.id, 0, Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto)
                    If (Hoja.Rows.Count > 0) Then
                        dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                        dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
                        Dim FechaElaboracion = dbmImaging.SchemaConfig.TBL_Parametro_Sistema.DBGet("@FechaElaboracionHC").Item(0).Valor_Parametro_Sistema
                        Me.ReportViewer.MainReportViewer1.Reset()
                        Me.ReportViewer.MainReportViewer1.LocalReport.ReportEmbeddedResource = "Miharu.Imaging.Library.ReportHojaControl.rdlc"
                        Me.ReportViewer.MainReportViewer1.LocalReport.DataSources.Clear()
                        Me.ReportViewer.MainReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("HojaControl", Hoja))
                        Me.ReportViewer.MainReportViewer1.LocalReport.SetParameters(New ReportParameter("FECHAELABORACION", FechaElaboracion))
                        Me.ReportViewer.MainReportViewer1.SetDisplayMode(DisplayMode.PrintLayout)
                        Me.ReportViewer.MainReportViewer1.RefreshReport()

                        Dim objRotuloCaja As New FormVisorHojaControl(Hoja)
                        objRotuloCaja.ShowDialog()
                    End If
                ElseIf tipo = "Reimprimir" Then
                    Dim Hoja = dbmImaging.SchemaProcess.PA_Crear_Hoja_Control.DBExecute(Cedula, "Reimprimir", Program.Sesion.Usuario.id, 0, Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto)
                    If (Hoja.Rows.Count > 0) Then
                        dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                        dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
                        Dim FechaElaboracion = dbmImaging.SchemaConfig.TBL_Parametro_Sistema.DBGet("@FechaElaboracionHC").Item(0).Valor_Parametro_Sistema

                        Me.ReportViewer.MainReportViewer1.Reset()
                        Me.ReportViewer.MainReportViewer1.LocalReport.ReportEmbeddedResource = "Miharu.Imaging.Library.ReportHojaControl.rdlc"
                        Me.ReportViewer.MainReportViewer1.LocalReport.DataSources.Clear()
                        Me.ReportViewer.MainReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("HojaControl", Hoja))
                        Me.ReportViewer.MainReportViewer1.LocalReport.SetParameters(New ReportParameter("FECHAELABORACION", FechaElaboracion))
                        Me.ReportViewer.MainReportViewer1.SetDisplayMode(DisplayMode.PrintLayout)
                        Me.ReportViewer.MainReportViewer1.RefreshReport()

                        Dim objRotuloCaja As New FormVisorHojaControl(Hoja)
                        objRotuloCaja.ShowDialog()
                    End If
                End If
            Next row
        Catch ex As Exception
            MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            If (manager IsNot Nothing) Then manager.Disconnect()
        End Try
    End Sub

End Class
