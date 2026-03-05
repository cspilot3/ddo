Imports System.Windows.Forms
Imports Miharu.Desktop.Controls
Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Library.Forms
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Plugins
Imports DMB = Miharu.Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl
Imports System.Text
Imports Miharu.Desktop.Controls.DesktopReportViewer

Public Class FormVisorRotuloCarpeta
    Inherits FormBase
    Implements IWorkspace
#Region " Declaraciones "

    'Esquema Búsqueda
    Private _ProcessLibrary As ProcessLibrary
    Private _xsdBusqueda As New xsdBusqueda
    Public _ToolsConnectionString As String

#End Region

#Region " Declaraciones "

    Private _ReportList As New List(Of DesktopReport1)
    Private ReportListDestiantario As New System.Windows.Forms.ComboBox
    Private _desktopDataGridViewControl As DesktopDataGridView.DesktopDataGridViewControl

#End Region

#Region " Propiedades "
    Private _carpeta As String
    Private _rotuloCarpetas As Boolean
    Private _caja As String
    Private _id_OT As Integer
    Private _tipoFuid As Integer
    Private _tipoGestion As String



    Sub New(caja As String, Id_OT As Integer, RotuloCarpetas As Boolean, TipoFuid As Integer, TipoGestion As String)
        ' TODO: Complete member initialization 
        InitializeComponent()
        _tipoFuid = TipoFuid
        _caja = caja
        _id_OT = Id_OT
        _rotuloCarpetas = RotuloCarpetas
        _tipoGestion = TipoGestion
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
            _ProcessLibrary = CType(value, ProcessLibrary)
        End Set
    End Property

    Public Sub ConfigWorkspace() Implements IWorkspace.ConfigWorkspace
        Try
            DBArchiving.DBArchivingDataBaseManager.IdentifierDateFormat = Program.DesktopGlobal.IdentifierDateFormat
            'Se carga el título
            WorkSpaceViewerControl.ReportList.Clear()
            WorkSpaceViewerControl.ReportList.Add(New FormReportRotuloCarpeta(WorkSpaceViewerControl))
        Catch ex As Exception
            DMB.DesktopMessageShow("CargaPermisos", ex)
        End Try
    End Sub

#End Region

    Private Sub FormVisorDestinatario_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            ReportListDestiantario.SelectedItem = 0
            WorkSpaceViewerControl.ReportList.Clear()
            WorkSpaceViewerControl.ReportList.Add(New FormReportRotuloCarpeta(WorkSpaceViewerControl))
            Me._ReportList = WorkSpaceViewerControl.ReportList
            Dim RotuloCarpeta As DesktopReport1 = Nothing
            For Each Reporte In Me._ReportList
                RotuloCarpeta = CType(Reporte, DesktopReport1)
            Next
            If _rotuloCarpetas Then
                Me.WorkSpaceViewerControl.ShowReport(RotuloCarpeta, CStr(_caja), _id_OT, _rotuloCarpetas, False, _tipoFuid, _tipoGestion)
            Else
                Me.WorkSpaceViewerControl.ShowReport(RotuloCarpeta, _caja, _id_OT, _rotuloCarpetas, False, _tipoFuid, _tipoGestion)
            End If

            Me.WorkSpaceViewerControl.ShowReport(RotuloCarpeta, _caja)
        Catch ex As Exception
            MessageBox.Show("Error al generar Rutulo", "Carpeta", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class