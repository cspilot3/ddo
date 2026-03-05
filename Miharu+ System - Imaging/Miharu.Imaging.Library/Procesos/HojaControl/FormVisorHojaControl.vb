Imports System.Windows.Forms
Imports Miharu.Desktop.Controls
Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Library.Forms
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Plugins
Imports DMB = Miharu.Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl
Imports System.Text
Imports Miharu.Desktop.Controls.DesktopReportViewer

Public Class FormVisorHojaControl
    Inherits FormBase
    Implements IWorkspace
#Region " Declaraciones "

    'Esquema Búsqueda
    Private _ProcessLibrary As ProcessLibrary
    Private _xsdBusqueda As New xsdBusqueda
    Public _ToolsConnectionString As String

#End Region

#Region " Declaraciones "

    Private _ReportList As New List(Of DesktopReportHojaControl)
    Private ReportListDestiantario As New System.Windows.Forms.ComboBox
    Private _desktopDataGridViewControl As DesktopDataGridView.DesktopDataGridViewControl

#End Region

#Region " Propiedades "
    Private _hoja As DataTable

    Sub New(Hoja As DataTable)
        ' TODO: Complete member initialization 
        InitializeComponent()
        _hoja = Hoja
    End Sub

    Public ReadOnly Property ReportList As List(Of DesktopReportHojaControl)
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
            WorkSpaceViewerControl.ReportList.Add(New FormReportHojaControl(WorkSpaceViewerControl))
        Catch ex As Exception
            DMB.DesktopMessageShow("CargaPermisos", ex)
        End Try
    End Sub

#End Region

    Private Sub FormVisorDestinatario_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            ReportListDestiantario.SelectedItem = 0
            WorkSpaceViewerControl.ReportList.Clear()
            WorkSpaceViewerControl.ReportList.Add(New FormReportHojaControl(WorkSpaceViewerControl))
            Me._ReportList = WorkSpaceViewerControl.ReportList
            Dim HojaControl As DesktopReportHojaControl = Nothing
            For Each Reporte In Me._ReportList
                HojaControl = CType(Reporte, DesktopReportHojaControl)
            Next
            Me.WorkSpaceViewerControl.ShowReport(HojaControl, _hoja)
        Catch ex As Exception
            MessageBox.Show("Error al generar Hoja Control", "Carpeta", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class