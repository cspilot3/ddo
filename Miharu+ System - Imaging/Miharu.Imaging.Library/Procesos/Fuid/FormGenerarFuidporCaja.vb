Imports DBImaging
Imports Microsoft.Reporting.WinForms
Imports System.IO
Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports System.Drawing
Imports Slyg.Tools.Imaging
Imports Miharu.FileProvider.Library
Imports Miharu.Imaging.Indexer.View.Indexacion
Imports System.Drawing.Imaging
Imports Miharu.Desktop.Controls.DesktopReportViewer

Public Class FormGenerarFuidporCaja

    Private Sub Aceptar_Click(sender As System.Object, e As System.EventArgs) Handles Aceptar.Click

        If (TextCaja.Text = "") Then
            DesktopMessageBoxControl.DesktopMessageShow("Debe ingresar un número de caja", "Caja inválida", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
            TextCaja.Focus()
        Else
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
            dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
            Try
                Dim OT = dbmImaging.SchemaProcess.PA_Consulta_FUID_Empaque.DBExecute(TextCaja.Text)
                Dim fk_OT As Integer
                For Each row As DataRow In OT.Rows
                    fk_OT = CInt(row("fk_OT"))
                Next
                Dim DataOt = dbmImaging.SchemaProcess.TBL_OT.DBGet(fk_OT)

                If DataOt.Rows.Count > 0 Then
                    If DataOt(0).Cerrado = True Then
                        Dim Fuidgeneral As Boolean = False
                        Dim cajaFuid = dbmImaging.SchemaProcess.PA_Genarar_Fuid.DBExecute(Program.ImagingGlobal.ProyectoImagingRow.fk_Entidad, Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto, "", "Caja", TextCaja.Text, "")
                        Dim objFUID As New FormVisorFuid(cajaFuid, CInt(cajaFuid.Rows(0)(3).ToString), Fuidgeneral, "")
                        objFUID.ShowDialog()
                    Else
                        DesktopMessageBoxControl.DesktopMessageShow("La OT no ha sido totalmente procesada.", "Rotulo Carpeta", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                    End If
                End If

            Catch ex As Exception
            End Try
        End If

    End Sub

End Class