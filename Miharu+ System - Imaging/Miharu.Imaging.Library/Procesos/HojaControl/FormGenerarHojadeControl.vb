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

Public Class FormGenerarHojadeControl

    Private operacion As String

    Private Sub FormGenerarHojadeControl_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Generar.Checked = True
        Reimprimir.Checked = False
    End Sub

    Private Sub Generar_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles Generar.CheckedChanged
        Reimprimir.Checked = Generar.Checked = False
    End Sub

    Private Sub Reimprimir_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles Reimprimir.CheckedChanged
        Generar.Checked = Reimprimir.Checked = False
    End Sub

    Private Sub Aceptar_Click(sender As System.Object, e As System.EventArgs) Handles Aceptar.Click

        If (Cedula.Text = "") Then
            DesktopMessageBoxControl.DesktopMessageShow("Debe ingresar un número de cédula", "Cédula inválida", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
            Cedula.Focus()
        Else
            If (Generar.Checked = True) Then
                operacion = "Generar"
            ElseIf (Reimprimir.Checked = True) Then

                operacion = "Reimprimir"
            End If
            Dim objCarpeta As New FormPrecintoHojaControl(Cedula.Text, operacion)
            objCarpeta.ShowDialog()
        End If
    End Sub

End Class