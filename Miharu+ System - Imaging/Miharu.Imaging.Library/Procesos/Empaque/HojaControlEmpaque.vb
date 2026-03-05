Imports Miharu.Desktop.Controls.DesktopReportViewer
Imports Miharu.Desktop.Controls
Imports Miharu.Desktop.Library
Imports Microsoft.Reporting.WinForms
Imports System.IO
Imports System.Drawing
Imports Slyg.Tools.Imaging
Imports System.Drawing.Imaging

Public Class HojaControlEmpaque

    Public Sub GenerarEmpaqueHojaControl(ncedulas As DataTable, ncaja As String, nopcion As String)

        Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
        Dim RutaHojaControl As String = Nothing
        Dim Reporte = New DesktopReportViewerHojaControl
        Dim crearHojaControl As New CrearHojaControl(Reporte)

        dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
        dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

        Try
            If nopcion = "Guardar" Then
                Dim DataTableHojaControl = dbmImaging.SchemaConfig.TBL_Parametro_Sistema.DBGet("@RutaHojaControl")
                For Each item As DataRow In DataTableHojaControl
                    RutaHojaControl = CStr(item("Valor_Parametro_Sistema"))
                Next
                If (Not System.IO.Directory.Exists(RutaHojaControl + "\" + ncaja)) Then
                    System.IO.Directory.CreateDirectory(RutaHojaControl + "\" + ncaja)
                End If
                crearHojaControl.ObtenerReporteTotal(ncedulas, RutaHojaControl + "\" + ncaja, nopcion)
                Process.Start(RutaHojaControl + "\" + ncaja)
            ElseIf nopcion = "Imprimir" Then
                crearHojaControl.ObtenerReporteTotal(ncedulas, "", "Imprimir")
            ElseIf nopcion = "Reimprimir" Then
                crearHojaControl.ObtenerReporteTotal(ncedulas, "", "Reimprimir")
            End If
        Catch ex As Exception

        End Try

    End Sub

End Class