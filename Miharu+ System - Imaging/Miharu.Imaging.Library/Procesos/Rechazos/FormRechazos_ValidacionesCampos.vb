Imports DBSecurity
Imports DBSecurity.SchemaConfig
Imports Miharu.Desktop.Controls.DesktopMessageBox

Namespace Procesos.Rechazos

    Public Class FormRechazos_ValidacionesCampos

        Private Sub RechazarButton_Click(sender As System.Object, e As System.EventArgs) Handles RechazarButton.Click
            Dim Respuesta = DesktopMessageBoxControl.DesktopMessageShow("¿Está seguro que desea generar el proceso de Rechazos?", "Rechazos", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, False)
            If (Respuesta = DialogResult.OK) Then
                Rechazar()
            End If
        End Sub

        Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
            Me.Close()
        End Sub

        Private Sub Rechazar()
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)

                dbmCore.Connection_Open(Program.Sesion.Usuario.id)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                Dim FechaProcesoDataTable = dbmImaging.SchemaProcess.TBL_Fecha_Proceso.DBFindByfk_Entidadfk_Proyectoid_fecha_proceso(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, CType(FechaProcesoDateTimePicker.Value.ToString("yyyyMMdd"), Global.Slyg.Tools.SlygNullable(Of Integer)))

                If FechaProcesoDataTable.Count > 0 Then
                    Dim Cerrada = FechaProcesoDataTable(0).Cerrado

                    If Not Cerrada Then
                        Dim RespuestaOT = dbmImaging.SchemaProcess.PA_Consultar_OT_x_Fecha_Proceso.DBExecute(Program.DesktopGlobal.PuestoTrabajoRow.fk_Entidad, Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, FechaProcesoDataTable(0).id_fecha_proceso)

                        If RespuestaOT = 1 Then
                            dbmCore.SchemaProcess.PA_Expedientes_Rechazados_ValidacionesCampos_Negativos.DBExecute(CType(FechaProcesoDateTimePicker.Value.ToString("yyyyMMdd"), Global.Slyg.Tools.SlygNullable(Of Integer)), Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto)
                            MessageBox.Show("Proceso de rechazos realizado con éxito", "Rechazos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Else
                            MessageBox.Show("No se puede realizar el proceso de Rechazos ya que hay una o más OT's pendientes de proceso.", "Rechazos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        End If
                    Else
                        MessageBox.Show("La Fecha de proceso seleccionada se encuentra cerrada.", "Rechazos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    End If
                ElseIf FechaProcesoDataTable.Count = 0 Then
                    MessageBox.Show("La fecha de proceso seleccionada no existe.", "Rechazos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If

            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

    End Class
End Namespace
