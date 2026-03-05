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
Imports Miharu.Desktop.Library.MiharuDMZ
Imports DBCore

Public Class Report_Acta_CialdLocal
    Inherits DesktopReport1


#Region " Propiedades "

    Friend Shared DesktopGlobal As New DesktopGlobal()
    Private _desktopReport1 As DesktopReport1

    Public DTActas As DataTable
    Public CodigoCiald As Integer
    Public NombreCiald As String

    Public Overrides ReadOnly Property ReportName As String
        Get
            Return "Formato Rotulo de Acta Ciald"
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
        Try
            'Dim CodigoCiald = CInt(Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede)
            'Dim NombreCiald = Program.DesktopGlobal.CentroProcesamientoRow.Nombre_Sede
            Dim fechaRecaudoValor As String = ""
            Dim RegistrosRelevados As Integer = 0
            Dim ResponseDatatable As New DataTable
            Dim Id_Acta As Integer = 0

            fechaRecaudoValor = DTActas.Rows(0)("FechaRecaudo").ToString()
            Id_Acta = CInt(DTActas.Rows(0)("idActa"))
            RegistrosRelevados = DTActas.Rows.Count
            ResponseDatatable = DTActas

            Dim parametroFechaRecaudo = New ReportParameter("FechaRecaudo", fechaRecaudoValor.ToString())
            Dim parametroCodigoCiald = New ReportParameter("CodigoCiald", CodigoCiald.ToString())
            Dim parametroRegistroRelevados = New ReportParameter("RegistrosRelevados", RegistrosRelevados.ToString())
            Dim parametroNombreCiald = New ReportParameter("NombreCiald", NombreCiald.ToString())
            Dim parametroIdActa = New ReportParameter("IdActa", Id_Acta.ToString())


            Me.ReportViewer.MainReportViewer1.Reset()
            Me.ReportViewer.MainReportViewer1.LocalReport.ReportEmbeddedResource = "Miharu.Imaging.Library.Report_Acta_CialdLocal.rdlc"
            Me.ReportViewer.MainReportViewer1.LocalReport.SetParameters(parametroCodigoCiald)
            Me.ReportViewer.MainReportViewer1.LocalReport.SetParameters(parametroFechaRecaudo)
            Me.ReportViewer.MainReportViewer1.LocalReport.SetParameters(parametroRegistroRelevados)
            Me.ReportViewer.MainReportViewer1.LocalReport.SetParameters(parametroNombreCiald)
            Me.ReportViewer.MainReportViewer1.LocalReport.SetParameters(parametroIdActa)

            Dim InformeReportDataSource1 As New ReportDataSource("dsActaRelevo", ResponseDatatable)

            Me.ReportViewer.MainReportViewer1.LocalReport.DataSources.Clear()
            Me.ReportViewer.MainReportViewer1.LocalReport.DataSources.Add(InformeReportDataSource1)
            Me.ReportViewer.MainReportViewer1.SetDisplayMode(DisplayMode.PrintLayout)
            Me.ReportViewer.MainReportViewer1.RefreshReport()

        Catch ex As Exception
            MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        Finally
            'If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
        End Try
    End Sub

#End Region
End Class