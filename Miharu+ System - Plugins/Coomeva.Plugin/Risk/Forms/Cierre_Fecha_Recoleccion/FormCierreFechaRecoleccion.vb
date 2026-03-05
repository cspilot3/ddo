Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Coomeva.Plugin.Risk.FormWrappers
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Config
Imports Slyg.Tools.Progress

Public Class FormCierreFechaRecoleccion

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


    Private Sub GeneraCierre()
        Dim dbmArchiving As New DBArchiving.DBArchivingDataBaseManager(_Plugin.CoomevaConnectionString)

        Try
            Me.Enabled = False
            Me.Cursor = Cursors.WaitCursor

            dbmArchiving.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

            Dim ProcesoDataTable = dbmArchiving.SchemaRisk.TBL_Control_Cargue_Reporte.DBFindByFecha_Recoleccion(CDate(FechaRecolecciondateTimePicker.Value.ToString).ToShortDateString)
            Dim Msg As String = ""

            If (ProcesoDataTable.Count = 0) Then
                MessageBox.Show("No se encuentra información asociada a la Fecha de Recolección seleccionada", "Cierre Fecha Recolección", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            Else
                If ProcesoDataTable(0).Isfk_CargueNull() Then
                    MessageBox.Show("La Fecha de Recolección seleccionada no ha sido cargada, por favor verifique", "Cierre Fecha Recolección", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return
                End If
            End If

                Dim OTPendientes = dbmArchiving.Schemadbo.PA_Validar_OT_Pendientes.DBExecute(FechaRecolecciondateTimePicker.Value.ToString("yyyy/MM/dd"))

                If (OTPendientes.Rows.Count() > 0) Then


                    If OTPendientes.Rows(0).Item("OT") <> "" Then
                        Msg = "La Fecha de Recolección seleccionada tiene las siguientes ot's pendientes por cerrar: " & OTPendientes.Rows(0).Item("OT")
                    End If

                    If OTPendientes.Rows(0).Item("Lineas") <> "" Then
                        If Msg <> "" Then
                            Msg = Msg & " y las siguientes lineas pendientes de proceso: " & OTPendientes.Rows(0).Item("Lineas")
                        Else
                            Msg = "La Fecha de Recolección seleccionada tiene las siguientes Lineas pendientes de proceso: " & OTPendientes.Rows(0).Item("Lineas")
                        End If

                    End If
                End If

                If Msg <> "" Then
                    MessageBox.Show(Msg & ". Por favor verifique e intente nuevamente.", "Cierre Fecha Recolección", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return
                Else
                    Dim Respuesta = dbmArchiving.SchemaProcess.PA_Cerrar_Fecha_Recoleccion.DBExecute(FechaRecolecciondateTimePicker.Value.ToString("yyyy/MM/dd"), _Plugin.Manager.Sesion.Usuario.id, _Plugin.Manager.RiskGlobal.Entidad, _Plugin.Manager.RiskGlobal.Proyecto, 1, _Plugin.Manager.RiskGlobal.Usa_Tabla_Fisico)

                    If Respuesta Then
                        MessageBox.Show("La fecha de recolección seleccionada ya se encuentra cerrada, por favor revise", "Cierre Fecha Recolección", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        MessageBox.Show("Proceso finalizado con éxito", "Cierre Fecha Recolección", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                End If


        Catch ex As Exception
            Application.DoEvents()
            DesktopMessageBoxControl.DesktopMessageShow("GeneraCierre", ex)
        Finally
            dbmArchiving.Connection_Close()
            Me.Enabled = True
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub BtnSalir_Click(sender As System.Object, e As System.EventArgs) Handles BtnSalir.Click
        Me.Close()
    End Sub

    Private Sub BtnCierre_Click(sender As System.Object, e As System.EventArgs) Handles BtnCierre.Click
        Dim Respuesta = DesktopMessageBoxControl.DesktopMessageShow("¿Está seguro que desea generar el Cierre de la Fecha de Recolección?", "Cierre Fecha Recolección", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, False)

        If (Respuesta = DialogResult.OK) Then
            GeneraCierre()
        End If
    End Sub
End Class