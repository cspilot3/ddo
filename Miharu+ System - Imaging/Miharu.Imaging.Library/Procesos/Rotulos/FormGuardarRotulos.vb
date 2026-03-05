Imports System.Windows.Forms
Imports Microsoft.Reporting.WinForms
Imports Miharu.Desktop.Controls.DesktopReportViewer
Imports Miharu.Desktop.Controls
Imports Miharu.Desktop.Library.Config
Imports System.Reflection
Imports DMB = Miharu.Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl
Imports System.IO
Imports System.Text
Imports System.Drawing

Public Class FormGuardarRotulos
    Inherits DesktopReport1


#Region " Propiedades "

    Friend Shared DesktopGlobal As New DesktopGlobal()
    Private _desktopReport1 As DesktopReport1

    Public Overrides ReadOnly Property ReportName As String
        Get
            Return "Formato Rotulo de Caja"
        End Get
    End Property

#End Region

#Region " Constructores "

    Public Sub New(ByRef nReportViewer As DesktopReportViewer1Control)
        MyBase.New(nReportViewer)

    End Sub
    Friend Shared ReadOnly Property AssemblyName() As String
        Get
            Return [Assembly].GetExecutingAssembly().GetName().Name
        End Get
    End Property
#End Region

#Region " Metodos "
    'Private Sub FormCartaDestinario_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
    '    Launch()
    'End Sub
    Public Overrides Sub Launch(Caja As String, OT As Integer, RotuloCarpetas As Boolean, Reportegenericofuid As Boolean, TipoFuid As Integer, TipoGestion As String)
        Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
        Try
            dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
            dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
            Dim stringGestion As String = Nothing
            Dim Rotulos = dbmImaging.SchemaProcess.PA_Rotulo_Caja.DBExecute(Caja, OT, "RotuloCaja", Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, TipoGestion, CStr(TipoFuid))

            If Rotulos.Rows(0).Item("Nomenclatura").ToString = dbmImaging.SchemaConfig.TBL_Parametro_Sistema.DBGet("@TipoGestionArchivoCentral").Item(0).Valor_Parametro_Sistema Then
                Rotulos.Columns.Add("IMAGENCBAC", GetType(Byte()))
                stringGestion = "IMAGENCBAC"
            End If

            If Rotulos.Rows(0).Item("Nomenclatura").ToString = dbmImaging.SchemaConfig.TBL_Parametro_Sistema.DBGet("@TipoGestionArchivoGestion").Item(0).Valor_Parametro_Sistema Then
                Rotulos.Columns.Add("IMAGENCBAG", GetType(Byte()))
                stringGestion = "IMAGENCBAG"

            End If

            Using barcodeImage As New BarcodeLib.Barcode
                With barcodeImage
                    .IncludeLabel = True
                End With
                For Each item As DataRow In Rotulos.Rows
                    If Not IsDBNull(item("CodigoUbicacion")) Then
                        Dim img As Byte() = ImageToByteArray(barcodeImage.Encode(BarcodeLib.TYPE.CODE128, CStr(item("CodigoUbicacion")), Color.Black, Color.White, 300, 65))
                        item(stringGestion) = img
                    End If
                Next
            End Using


            Dim InformeReportDataSource1 As New ReportDataSource("DataSet1", Rotulos)
            Me.ReportViewer.MainReportViewer1.Reset()
            Me.ReportViewer.MainReportViewer1.LocalReport.ReportEmbeddedResource = "Miharu.Imaging.Library.ReportRotulosCajas.rdlc"
            If CBool(CDbl(dbmImaging.SchemaConfig.TBL_Parametro_Sistema.DBGet("@FechaUMVCaja").Item(0).Valor_Parametro_Sistema)) Then
                Me.ReportViewer.MainReportViewer1.LocalReport.SetParameters(New ReportParameter("FECHAAPLICACION", DateTime.Now.ToString()))
            Else
                Me.ReportViewer.MainReportViewer1.LocalReport.SetParameters(New ReportParameter("FECHAAPLICACION", "Agosto de 2013"))
            End If

            Me.ReportViewer.MainReportViewer1.LocalReport.DataSources.Clear()
            Me.ReportViewer.MainReportViewer1.LocalReport.DataSources.Add(InformeReportDataSource1)
            Me.ReportViewer.MainReportViewer1.SetDisplayMode(DisplayMode.PrintLayout)
            Me.ReportViewer.MainReportViewer1.RefreshReport()
            Dim DataTableRotulos = dbmImaging.SchemaConfig.TBL_Parametro_Sistema.DBGet("@RutaRotulos")
            Dim RutaRotulos As String = Nothing
            For Each item As DataRow In DataTableRotulos
                RutaRotulos = CStr(item("Valor_Parametro_Sistema"))
            Next
            Dim OutputFolder As String = RutaRotulos.TrimEnd("\"c) & "\"
            Dim PDFMemoryStream = New MemoryStream(Me.ReportViewer.MainReportViewer1.LocalReport.Render("PDF"))
            Dim PDFMemoryStreamExcel = New MemoryStream(Me.ReportViewer.MainReportViewer1.LocalReport.Render("Excel"))
            Dim Titulo = "Rotulos"
            If Not Directory.Exists(OutputFolder & Titulo) Then
                Directory.CreateDirectory(OutputFolder & Titulo)
            End If

            Dim FileFolderName = Titulo + "\" & "Caja_n" & TipoGestion & Caja & "\"
            If (Not Directory.Exists(OutputFolder & FileFolderName)) Then
                Directory.CreateDirectory(OutputFolder & FileFolderName)
            End If
            Dim Ruta = OutputFolder & FileFolderName
            Dim NombreArchivo = "RotuloCaja_n" & TipoGestion & Caja & ".pdf"
            Dim NombreArchivoExcel = "RotuloCaja_n" & TipoGestion & Caja & ".xls"
            If File.Exists(Ruta & NombreArchivo) Then
                My.Computer.FileSystem.DeleteFile(Ruta & NombreArchivo)
            End If
            Using file = New FileStream(Ruta & NombreArchivo, FileMode.Append, FileAccess.Write)
                PDFMemoryStream.WriteTo(file)
                PDFMemoryStream.Close()
            End Using
            Using fileExcel = New FileStream(Ruta & NombreArchivoExcel, FileMode.Append, FileAccess.Write)
                PDFMemoryStreamExcel.WriteTo(fileExcel)
                PDFMemoryStreamExcel.Close()
            End Using
            MessageBox.Show("Rotulos guardados Con Exito Ruta: " & Ruta & " ", "Guardado", MessageBoxButtons.OK)
            Process.Start(Ruta)
            Process.Start(Ruta & NombreArchivo)
            Process.Start(Ruta)
            Process.Start(Ruta & NombreArchivoExcel)



            If RotuloCarpetas Then
                Dim RotulosCarpetas = dbmImaging.SchemaProcess.PA_Rotulo_Caja.DBExecute(Caja, OT, "RotuloCarpeta", Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, TipoGestion, CStr(TipoFuid))

                RotulosCarpetas.Columns.Add("IMAGENCB", GetType(Byte()))

                Using barcodeImage As New BarcodeLib.Barcode
                    With barcodeImage
                        '    .Height = 200
                        .BarWidth = 1
                        .IncludeLabel = True
                        '    .EncodedType = TYPE.CODE128
                    End With
                    For Each item As DataRow In RotulosCarpetas.Rows
                        If Not IsDBNull(item("CodigoUbicacion")) Then
                            Dim img As Byte() = ImageToByteArray(barcodeImage.Encode(BarcodeLib.TYPE.CODE128, CStr(item("CodigoUbicacion")), Color.Black, Color.White, 290, 80))
                            item("IMAGENCB") = img
                        End If
                    Next
                End Using
             
                Dim InformeReportDataSource2 As New ReportDataSource("DataSet1", RotulosCarpetas)
                Me.ReportViewer.MainReportViewer1.Reset()
                Me.ReportViewer.MainReportViewer1.LocalReport.ReportEmbeddedResource = "Miharu.Imaging.Library.ReportRotuloCarpeta.rdlc"
                If CBool(CDbl(dbmImaging.SchemaConfig.TBL_Parametro_Sistema.DBGet("@FechaUMVCarpeta").Item(0).Valor_Parametro_Sistema)) Then
                    Me.ReportViewer.MainReportViewer1.LocalReport.SetParameters(New ReportParameter("FECHAAPLICACION", DateTime.Now.ToString()))
                Else
                    Me.ReportViewer.MainReportViewer1.LocalReport.SetParameters(New ReportParameter("FECHAAPLICACION", "Abril de 2019"))
                End If
                Me.ReportViewer.MainReportViewer1.LocalReport.DataSources.Clear()
                Me.ReportViewer.MainReportViewer1.LocalReport.DataSources.Add(InformeReportDataSource2)
                Me.ReportViewer.MainReportViewer1.SetDisplayMode(DisplayMode.PrintLayout)
                Me.ReportViewer.MainReportViewer1.RefreshReport()
                If RotulosCarpetas.Rows.Count > 0 Then
                    PDFMemoryStream = New MemoryStream(Me.ReportViewer.MainReportViewer1.LocalReport.Render("PDF"))
                    PDFMemoryStreamExcel = New MemoryStream(Me.ReportViewer.MainReportViewer1.LocalReport.Render("Excel"))
                    Dim NombreCarpeta = "RotuloCarpetas_n" & TipoGestion & Caja & ".pdf"
                    Dim NombreCarpetaExcel = "RotuloCarpetas_n" & TipoGestion & Caja & ".xls"
                    If File.Exists(Ruta & NombreCarpeta) Then
                        My.Computer.FileSystem.DeleteFile(Ruta & NombreCarpeta)
                    End If
                    If File.Exists(Ruta & NombreCarpetaExcel) Then
                        My.Computer.FileSystem.DeleteFile(Ruta & NombreCarpetaExcel)
                    End If
                    Using file = New FileStream(Ruta & NombreCarpeta, FileMode.Append, FileAccess.Write)
                        PDFMemoryStream.WriteTo(file)
                        PDFMemoryStream.Close()
                    End Using
                    Using fileExcel = New FileStream(Ruta & NombreCarpetaExcel, FileMode.Append, FileAccess.Write)
                        PDFMemoryStreamExcel.WriteTo(fileExcel)
                        PDFMemoryStreamExcel.Close()
                    End Using
                    Process.Start(Ruta & NombreCarpeta)
                    Process.Start(Ruta & NombreCarpetaExcel)
                End If
            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        Finally
            If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
        End Try
    End Sub
    Public Overrides Sub Launch(Carpeta As String)
    End Sub
    Public Overrides Sub Launch(datatableDestinatario As DataTable, solicitudSeleccionada As Integer, nombres As String, direccion As String, sede As String, precinto As String)
    End Sub
    Public Overrides Sub Launch(FechaRecaudo As Integer)
    End Sub

    Public Shared Function ImageToByteArray(Image As System.Drawing.Image) As Byte()
        Using Ms = New MemoryStream()
            Image.Save(Ms, System.Drawing.Imaging.ImageFormat.Png)
            Return Ms.ToArray()
        End Using
    End Function
#End Region
End Class