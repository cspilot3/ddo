Imports System.Windows.Forms
Imports Microsoft.Reporting.WinForms
Imports Miharu.Desktop.Controls.DesktopReportViewer
Imports Miharu.Desktop.Controls
Imports Miharu.Desktop.Library.Config
Imports System.Reflection
Imports DMB = Miharu.Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl
Imports System.IO
Imports BarcodeLib
Imports System.Drawing


Public Class FormReportRotuloCaja
    Inherits DesktopReport1
#Region " Propiedades "

    Friend Shared DesktopGlobal As New DesktopGlobal()
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
        Dim stringGestion As String = Nothing

        Try
            dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
            dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
            Dim Rotulos = dbmImaging.SchemaProcess.PA_Rotulo_Caja.DBExecute(Caja, OT, "RotuloCaja", Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, TipoGestion, CStr(TipoFuid))
            If Rotulos.Rows.Count > 0 Then



                If Not String.IsNullOrEmpty(Rotulos.Rows(0).Item("Nomenclatura").ToString) Then
                    If Rotulos.Rows(0).Item("Nomenclatura").ToString = dbmImaging.SchemaConfig.TBL_Parametro_Sistema.DBGet("@TipoGestionArchivoCentral").Item(0).Valor_Parametro_Sistema Then
                        Rotulos.Columns.Add("IMAGENCBAC", GetType(Byte()))
                        stringGestion = "IMAGENCBAC"
                    End If
                End If
                If Not String.IsNullOrEmpty(Rotulos.Rows(0).Item("Nomenclatura").ToString) Then
                    If Rotulos.Rows(0).Item("Nomenclatura").ToString = dbmImaging.SchemaConfig.TBL_Parametro_Sistema.DBGet("@TipoGestionArchivoGestion").Item(0).Valor_Parametro_Sistema Then
                        Rotulos.Columns.Add("IMAGENCBAG", GetType(Byte()))
                        stringGestion = "IMAGENCBAG"

                    End If
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
                Dim deviceInfo = "<DeviceInfo><OutputFormat>PDF</OutputFormat> <PageWidth>21cm</PageWidth>  <PageHeight>29.7cm</PageHeight> <MarginTop>0.5cm</MarginTop>  <MarginLeft>1.1cm</MarginLeft> <MarginRight>0.5cm</MarginRight> <MarginBottom>0.5cm</MarginBottom></DeviceInfo>"
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
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
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
    Public Overrides Sub Launch(Carpeta As String)
    End Sub
    Public Overrides Sub Launch(datatableDestinatario As DataTable, solicitudSeleccionada As Integer, nombres As String, direccion As String, sede As String, precinto As String)
    End Sub

    Public Overrides Sub Launch(FechaRecaudo As Integer)
    End Sub

#End Region
End Class