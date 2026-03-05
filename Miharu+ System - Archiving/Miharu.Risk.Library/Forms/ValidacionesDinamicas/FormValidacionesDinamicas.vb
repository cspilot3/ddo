Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports System.Windows.Forms
Imports DBArchiving

Namespace Forms.ValidacionesDinamicas
    Public Class FormValidacionesDinamicas


        Private Sub BtnCancelar_Click(sender As System.Object, e As System.EventArgs) Handles BtnCancelar.Click
            Me.Close()
        End Sub

        Private Sub BtnAceptar_Click(sender As System.Object, e As System.EventArgs) Handles BtnAceptar.Click
            If DesktopMessageBoxControl.DesktopMessageShow("¿Está seguro que desea realizar el proceso de validaciones dinamicas?", "Validaciones Dinamicas", DesktopMessageBoxControl.IconEnum.WarningIcon, False) = DialogResult.OK Then
                ValidacionesDinamicas()
            End If
        End Sub

        Private Sub ValidacionesDinamicas()
            Dim dbmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)

            Try
                dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)
                dbmArchiving.Transaction_Begin()

                Dim OTDataTable = dbmArchiving.SchemaProcess.PA_Validar_OT.DBExecute(CInt(OTTextBox.Text), DBCore.EstadoEnum.Empaque)

                If OTDataTable.ToString() = "OK" Then
                    Dim Respuesta1 = dbmArchiving.SchemaProcess.PA_Validaciones_Dinamicas_Documentos_Obligatorios.DBExecute(CInt(OTTextBox.Text), Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, Program.Sesion.Usuario.id)

                    If Respuesta1.ToString() = "OK" Then
                        Dim Respuesta2 = dbmArchiving.SchemaProcess.PA_Validaciones_Dinamicas_Novedades.DBExecute(CInt(OTTextBox.Text), Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, Program.Sesion.Usuario.id)
                        If Respuesta2.ToString() = "OK" Then
                            DesktopMessageBoxControl.DesktopMessageShow("Proceso realizado con éxito", "Validaciones Dinámicas", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True, False)
                        Else
                            DesktopMessageBoxControl.DesktopMessageShow(Respuesta2.ToString(), "Validaciones Dinámicas Novedades", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True, False)
                        End If
                    Else
                        DesktopMessageBoxControl.DesktopMessageShow(Respuesta1.ToString(), "Validaciones Dinámicas Doc Obligatorios", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True, False)
                    End If
                Else
                    DesktopMessageBoxControl.DesktopMessageShow(OTDataTable.ToString(), "Validaciones Dinámicas", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True, False)
                End If

                dbmArchiving.Transaction_Commit()
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("ValidacionesDinamicas", ex)
                dbmArchiving.Transaction_Rollback()
            Finally
                dbmArchiving.Connection_Close()
            End Try
        End Sub

    End Class
End Namespace