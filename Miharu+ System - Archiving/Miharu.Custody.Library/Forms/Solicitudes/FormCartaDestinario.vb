Imports System.Windows.Forms
Imports Microsoft.Reporting.WinForms
Imports Miharu.Desktop.Controls.DesktopReportViewer
Imports Miharu.Desktop.Controls
Imports Miharu.Desktop.Library.Config
Imports System.Reflection
Imports DMB = Miharu.Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl
'Namespace Reportes.VisorReportes.UniversalvsParcial
'C:\Proyectos\Miharu+ System - Plugins\Miharu+ System - Archiving\Miharu.Custody.Library\Forms\Solicitudes\FormDestinatario.vb
Namespace Forms.Solicitudes.FormCartaDestinario


    Public Class FormCartaDestinario
        Inherits DesktopReport1
#Region " Propiedades "

        Friend Shared DesktopGlobal As New DesktopGlobal()
        Public Overrides ReadOnly Property ReportName As String
            Get
                Return "Reporte Universal vs Parcial"
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
        Public Overrides Sub Launch()
        End Sub
        Public Overrides Sub Launch(datatableDestinatario As DataTable, solicitudSeleccionada As Integer, nombres As String, direccion As String, sede As String, precinto As String)
            Dim dbmSecurity As DBSecurity.DBSecurityDataBaseManager = Nothing
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            Dim sedeRow As DBSecurity.SchemaConfig.TBL_SedeRow
            Dim puestoTrabajoDataTable As DBSecurity.SchemaConfig.TBL_Puesto_TrabajoDataTable
            Try
                dbmSecurity = New DBSecurity.DBSecurityDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Security)
                dbmSecurity.Connection_Open(Program.Sesion.Usuario.id)
                puestoTrabajoDataTable = dbmSecurity.SchemaConfig.TBL_Puesto_Trabajo.DBFindByPC_Name(Environment.MachineName)
                If (puestoTrabajoDataTable.Count = 0) Then
                    MessageBox.Show("El equipo actual [" & Environment.MachineName & "] no se encuentra registrado como un puesto de trabajo de la entidad [" & Program.Sesion.Entidad.Nombre & "], por favor comuniquese con el administrador del sistema", AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If
                DesktopGlobal.PuestoTrabajoRow = puestoTrabajoDataTable(0).ToTBL_Puesto_TrabajoSimpleType
                Dim sedeDataTable = dbmSecurity.SchemaConfig.TBL_Sede.DBGet(puestoTrabajoDataTable(0).fk_Entidad, puestoTrabajoDataTable(0).fk_Sede)
                sedeRow = sedeDataTable(0)
                Dim Fecha As DateTime = Now
                Dim InformeReportDataSource1 As New ReportDataSource("Destinatario", datatableDestinatario)
                Me.ReportViewer.MainReportViewer1.Reset()
                Me.ReportViewer.MainReportViewer1.LocalReport.ReportEmbeddedResource = "Miharu.Custody.Library.CartaDestinatario.rdlc"
                Me.ReportViewer.MainReportViewer1.LocalReport.DataSources.Clear()
                Me.ReportViewer.MainReportViewer1.LocalReport.DataSources.Add(InformeReportDataSource1)
                Me.ReportViewer.MainReportViewer1.LocalReport.SetParameters(New ReportParameter("Fecha_Inicio", CStr(Fecha)))
                Me.ReportViewer.MainReportViewer1.LocalReport.SetParameters(New ReportParameter("Ciudad", sedeRow.Nombre_Sede))
                Me.ReportViewer.MainReportViewer1.LocalReport.SetParameters(New ReportParameter("Solicitud", CStr(solicitudSeleccionada)))
                Me.ReportViewer.MainReportViewer1.LocalReport.SetParameters(New ReportParameter("Nombres", nombres))
                Me.ReportViewer.MainReportViewer1.LocalReport.SetParameters(New ReportParameter("Sede", sede))
                Me.ReportViewer.MainReportViewer1.LocalReport.SetParameters(New ReportParameter("Direccion", direccion))
                Me.ReportViewer.MainReportViewer1.LocalReport.SetParameters(New ReportParameter("Usuario", CStr(Program.Sesion.Usuario.Nombres + " " + CStr(Program.Sesion.Usuario.Apellidos))))
                Me.ReportViewer.MainReportViewer1.RefreshReport()
            Catch ex As Exception
                DMB.DesktopMessageShow("Carta Destino", ex)
            Finally
                If (dbmSecurity IsNot Nothing) Then dbmSecurity.Connection_Close()
            End Try
        End Sub

#End Region
    End Class
End Namespace