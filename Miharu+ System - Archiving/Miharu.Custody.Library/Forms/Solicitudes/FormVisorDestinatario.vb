Imports System.Windows.Forms
Imports Miharu.Desktop.Controls
Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Library.Forms
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Plugins
Imports DMB = Miharu.Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl
Imports System.Text
Imports Miharu.Desktop.Controls.DesktopReportViewer
Imports Miharu.Custody.Library.Forms.Solicitudes.FormCartaDestinario

Public Class FormVisorDestinatario
    Inherits FormBase
    Implements IWorkspace
#Region " Declaraciones "

    'Esquema Búsqueda
    Private _ProcessLibrary As ProccessLibrary
    Private _xsdBusqueda As New xsdBusqueda
    Public _ToolsConnectionString As String

#End Region

#Region " Declaraciones "

    Private _ReportList As New List(Of DesktopReport1)
    Private ReportListDestiantario As New System.Windows.Forms.ComboBox
    Private _desktopDataGridViewControl As DesktopDataGridView.DesktopDataGridViewControl

#End Region

#Region " Propiedades "
    Private _nombres As String
    Private _sede As String
    Private _direccion As String
    Private _precinto As String
    Private _datatableDestinatario As DataTable
    Private _solicitudSeleccionada As Integer


    Sub New(DatatableDestinatario As DataTable, SolicitudSeleccionada As Integer, Nombres As String, Sede As String, Direccion As String, Precinto As String)
        ' TODO: Complete member initialization 
        InitializeComponent()
        _datatableDestinatario = DatatableDestinatario
        _solicitudSeleccionada = SolicitudSeleccionada
        _nombres = Nombres
        _sede = Sede
        _direccion = Direccion
        _precinto = Precinto
    End Sub

    Public ReadOnly Property ReportList As List(Of DesktopReport1)
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
            _ProcessLibrary = CType(value, ProccessLibrary)
        End Set
    End Property

    Public Sub ConfigWorkspace() Implements IWorkspace.ConfigWorkspace
        Try
            DBArchiving.DBArchivingDataBaseManager.IdentifierDateFormat = Program.DesktopGlobal.IdentifierDateFormat
            'Se carga el título
            WorkSpaceViewerControl.ReportList.Clear()
            WorkSpaceViewerControl.ReportList.Add(New FormCartaDestinario(WorkSpaceViewerControl))
        Catch ex As Exception
            DMB.DesktopMessageShow("CargaPermisos", ex)
        End Try
    End Sub

#End Region

    Private Sub FormVisorDestinatario_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            ReportListDestiantario.SelectedItem = 0
            WorkSpaceViewerControl.ReportList.Clear()
            WorkSpaceViewerControl.ReportList.Add(New FormCartaDestinario(WorkSpaceViewerControl))
            Me._ReportList = WorkSpaceViewerControl.ReportList
            Dim CartaDestinatario As DesktopReport1
            For Each Reporte In Me._ReportList
                CartaDestinatario = CType(Reporte, DesktopReport1)
            Next
            Me.WorkSpaceViewerControl.ShowReport(CartaDestinatario, _datatableDestinatario, _solicitudSeleccionada, _nombres, _direccion, _sede, _precinto)
        Catch ex As Exception
            MessageBox.Show("Error al generar carta destinatario", "Destinatario", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
End Class