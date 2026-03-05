Imports System.Windows.Forms
Imports Miharu.Desktop.Controls
Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Library.Forms
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Plugins
Imports DMB = Miharu.Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl
Imports System.Text
Imports Miharu.Desktop.Controls.DesktopReportViewer

Public Class FormVisorFuid
    Inherits FormBase
    Implements IWorkspace
#Region " Declaraciones "

    'Esquema Búsqueda
    Private _ProcessLibrary As ProcessLibrary
    Private _ReportList As New List(Of DesktopReportFUID)
    Private ReportListDestiantario As New System.Windows.Forms.ComboBox
    Private _desktopDataGridViewControl As DesktopDataGridView.DesktopDataGridViewControl

#End Region

#Region " Propiedades "
    Private reporteFUID As DataTable
    Private tipoFuid As Integer
    Private Fuidgeneral As Boolean
    Private tipoGestion As String

    Sub New(nreporteFUID As DataTable, ntipoFuid As Integer, nFuidgeneral As Boolean, ntipoGestion As String)
        InitializeComponent()
        reporteFUID = nreporteFUID
        tipoFuid = ntipoFuid
        Fuidgeneral = nFuidgeneral
        tipoGestion = ntipoGestion
    End Sub


    Public ReadOnly Property ReportList As List(Of DesktopReportFUID)
        Get
            Return Me._ReportList
        End Get
    End Property
#End Region

#Region " Implementación IWorkspace"

    Public Property ProcessLibrary As IProcessLibrary Implements IWorkspace.ProcessLibrary
        Get
            Return _ProcessLibrary
        End Get
        Set(ByVal value As IProcessLibrary)
            _ProcessLibrary = CType(value, ProcessLibrary)
        End Set
    End Property

    Public Sub ConfigWorkspace() Implements IWorkspace.ConfigWorkspace
        Try
            DBArchiving.DBArchivingDataBaseManager.IdentifierDateFormat = Program.DesktopGlobal.IdentifierDateFormat
            'Se carga el título
            WorkSpaceViewerControl.ReportList.Clear()
            WorkSpaceViewerControl.ReportList.Add(New FormReportFuid(WorkSpaceViewerControl))
        Catch ex As Exception
            DMB.DesktopMessageShow("CargaPermisos", ex)
        End Try
    End Sub

#End Region

    Private Sub FormVisorDestinatario_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            ReportListDestiantario.SelectedItem = 0
            WorkSpaceViewerControl.ReportList.Clear()
            WorkSpaceViewerControl.ReportList.Add(New FormReportFuid(WorkSpaceViewerControl))
            Me._ReportList = WorkSpaceViewerControl.ReportList
            Dim FUID As DesktopReportFUID = Nothing
            For Each Reporte In Me._ReportList
                FUID = CType(Reporte, DesktopReportFUID)
            Next
            Me.WorkSpaceViewerControl.ShowReport(FUID, reporteFUID, tipoFuid, Fuidgeneral, tipoGestion)
        Catch ex As Exception
            MessageBox.Show("Error al generar Fuid", "Carpeta", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class