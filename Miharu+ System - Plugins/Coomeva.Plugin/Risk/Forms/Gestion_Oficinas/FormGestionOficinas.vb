Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Coomeva.Plugin.Risk.FormWrappers
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Config
Imports Slyg.Tools.Progress

Public Class FormGestionOficinas

#Region " Declaraciones "

    Private _Plugin As CoomevaRiskPlugin
    Private _TipoCargue As DesktopConfig.TipoCargue

#End Region

#Region " Constructor "

    Sub New(ByVal nCoomevaDesktopPlugin As CoomevaRiskPlugin)
        ' This call is required by the designer.
        InitializeComponent()
        _Plugin = nCoomevaDesktopPlugin
    End Sub

#End Region

    Private Sub ValidaEnvioCorreoOficinas()
        Dim ClsEnvio_Correo = New EnvioCorreo(_Plugin)
        Dim dbmArchiving As New DBArchiving.DBArchivingDataBaseManager(_Plugin.CoomevaConnectionString)

        Try
            Me.Enabled = False
            Me.Cursor = Cursors.WaitCursor

            dbmArchiving.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

            Dim ProcesoDataTable = dbmArchiving.SchemaRisk.TBL_Control_Cargue_Reporte.DBFindByFecha_RecoleccionCerrado(CDate(FechaRecolecciondateTimePicker.Value.ToString).ToShortDateString, 1)

            If (ProcesoDataTable.Count = 0) Then
                MessageBox.Show("No se encuentra información asociada a la Fecha de Recolección seleccionada o la fecha de recolección no ha sido cerrada.", "Correo Gestion Oficinas", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            'realiza el envio de correo masivo a oficinas
            ClsEnvio_Correo.EnviarCorreo(CDate(FechaRecolecciondateTimePicker.Value.ToString).ToShortDateString)

        Catch ex As Exception
            Application.DoEvents()
            DesktopMessageBoxControl.DesktopMessageShow("Envio Correo - Gestión Oficinas", ex)
        Finally
            dbmArchiving.Connection_Close()
            Me.Enabled = True
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub BtnSalir_Click(sender As System.Object, e As System.EventArgs) Handles BtnSalir.Click
        Me.Close()
    End Sub

    Private Sub BtnEnvioCorreo_Click(sender As System.Object, e As System.EventArgs) Handles BtnEnvioCorreo.Click
        Dim Respuesta = DesktopMessageBoxControl.DesktopMessageShow("¿Desea enviar el correo de Gestión de Oficinas?", "Gestión de Oficinas", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, False)

        If (Respuesta = DialogResult.OK) Then
            ValidaEnvioCorreoOficinas()
        End If
    End Sub
End Class