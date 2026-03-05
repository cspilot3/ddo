Imports System.Windows.Forms
Imports Miharu.Desktop.Controls
Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Library.Forms
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Plugins
Imports DMB = Miharu.Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl
Imports System.Text
Imports Miharu.Desktop.Controls.DesktopReportViewer
Public Class FormVisorSticker
    Inherits FormBase
    Implements IWorkspace
#Region " Declaraciones "
    'Esquema Búsqueda
    Private _ProcessLibrary As ProcessLibrary
    Private _ReportList As New List(Of DesktopReport1)
    Private ReportListDestiantario As New System.Windows.Forms.ComboBox
    Private nrocaja As String
    Private idOT As Integer
#End Region
#Region " Propiedades "
    Sub New(Caja As String, Id_OT As Integer)
        ' TODO: Complete member initialization 
        InitializeComponent()
        nrocaja = Caja
        idOT = Id_OT
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
            WorkSpaceViewerControl.ReportList.Add(New FormReportSticker(WorkSpaceViewerControl))
        Catch ex As Exception
            DMB.DesktopMessageShow("CargaPermisos", ex)
        End Try
    End Sub
    Private Sub FormVisorDestinatario_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            ReportListDestiantario.SelectedItem = 0
            WorkSpaceViewerControl.ReportList.Clear()
            WorkSpaceViewerControl.ReportList.Add(New FormReportSticker(WorkSpaceViewerControl))
            Me._ReportList = WorkSpaceViewerControl.ReportList
            Dim RotuloCarpeta As DesktopReport1 = Nothing
            For Each Reporte In Me._ReportList
                RotuloCarpeta = CType(Reporte, DesktopReport1)
            Next
            Me.WorkSpaceViewerControl.ShowReport(RotuloCarpeta, CStr(nrocaja), idOT, False, False, Nothing, Nothing)
        Catch ex As Exception
            MessageBox.Show("Error al generar Rutulo", "Caja", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region
End Class