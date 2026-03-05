Imports System.Windows.Forms
Imports Microsoft.Reporting.WinForms
Imports Miharu.Desktop.Controls.DesktopReportViewer
Imports Miharu.Desktop.Controls
Imports Miharu.Desktop.Library.Config
Imports System.Reflection
Imports DMB = Miharu.Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl
Imports BarcodeLib
Imports System.Text
Imports System.IO
Imports System.Drawing

Public Class FormReportRotuloCarpeta
    Inherits DesktopReport1

#Region " Propiedades "

    Friend Shared DesktopGlobal As New DesktopGlobal()
    Public Overrides ReadOnly Property ReportName As String
        Get
            Return "Formato Rotulo de Carpeta"
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
    Public Overrides Sub Launch(Carpeta As String)
    End Sub
    Public Overrides Sub Launch(datatableDestinatario As DataTable, solicitudSeleccionada As Integer, nombres As String, direccion As String, sede As String, precinto As String)
    End Sub
    Public Overrides Sub Launch(FechaRecaudo As Integer)
    End Sub

    Public Overrides Sub Launch(Carpeta As String, OT As Integer, RotuloCarpetas As Boolean, Reportegenericofuid As Boolean, TipoFuid As Integer, TipoGestion As String)
        Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
        Try
            dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
            dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
            If RotuloCarpetas Then
                Dim RotulosCarpetas = dbmImaging.SchemaProcess.PA_Rotulo_Caja.DBExecute(Carpeta, OT, "RotuloCarpeta", Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, TipoGestion, CStr(TipoFuid))
                If RotulosCarpetas.Rows.Count > 0 Then
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
                    'Me.ReportViewer.MainReportViewer1.RefreshReport()
                End If

            Else
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
                Dim Rotulos = dbmImaging.SchemaProcess.PA_Rotulo_Carpeta.DBExecute("", CType(Carpeta, Global.Slyg.Tools.SlygNullable(Of Integer)), "RotuloCarpeta")
                If Rotulos.Rows.Count > 0 Then
                    Rotulos.Columns.Add("IMAGENCB", GetType(Byte()))

                    Using barcodeImage As New BarcodeLib.Barcode
                        With barcodeImage
                            '    .Height = 200
                            .BarWidth = 1
                            .IncludeLabel = True
                            '    .EncodedType = TYPE.CODE128
                        End With
                        For Each item As DataRow In Rotulos.Rows
                            Dim img As Byte() = ImageToByteArray(barcodeImage.Encode(BarcodeLib.TYPE.CODE128, CStr(item("CodigoUbicacion")), Color.Black, Color.White, 290, 80))
                            item("IMAGENCB") = img
                        Next
                    End Using
                    Dim InformeReportDataSource1 As New ReportDataSource("DataSet1", Rotulos)
                    Me.ReportViewer.MainReportViewer1.Reset()
                    Me.ReportViewer.MainReportViewer1.LocalReport.ReportEmbeddedResource = "Miharu.Imaging.Library.ReportRotuloCarpeta.rdlc"
                    Me.ReportViewer.MainReportViewer1.LocalReport.DataSources.Clear()
                    Me.ReportViewer.MainReportViewer1.LocalReport.DataSources.Add(InformeReportDataSource1)
                    Me.ReportViewer.MainReportViewer1.SetDisplayMode(DisplayMode.PrintLayout)
                    Me.ReportViewer.MainReportViewer1.RefreshReport()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        Finally
            If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
        End Try
    End Sub

    Public Shared Function ImageToByteArray(Image As System.Drawing.Image) As Byte()
        Using Ms = New MemoryStream()
            Image.Save(Ms, System.Drawing.Imaging.ImageFormat.Png)
            Return Ms.ToArray()
        End Using
    End Function

#End Region

End Class