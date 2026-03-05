Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports System.Windows.Forms
Imports DBArchiving
Imports Miharu.Desktop.Library.Config

Namespace Procesos.ValidacionesDinamicas
    Public Class FormValidacionesDinamicas


        Private Sub BtnCancelar_Click(sender As System.Object, e As System.EventArgs) Handles BtnCancelar.Click
            Me.Close()
        End Sub

        Private Sub BtnAceptar_Click(sender As System.Object, e As System.EventArgs) Handles BtnAceptar.Click
            If DesktopMessageBoxControl.DesktopMessageShow("¿Está seguro que desea realizar el proceso de validaciones dinamicas?", "Validaciones Dinamicas", DesktopMessageBoxControl.IconEnum.WarningIcon, False) = DialogResult.OK Then
                ValidacionesDinamicas()
            End If
        End Sub

        Private Sub FormCruce_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            CargarOT()
        End Sub

        Private Sub ValidacionesDinamicas()
            Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
            Dim dbmImaging As New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
            Try
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                dbmCore.Transaction_Begin()

                Dim RegistrosPendientes As Short

                 RegistrosPendientes = dbmImaging.SchemaProcess.PA_Validacion_Registros_Precaptura_Fecha_Proceso.DBExecute(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, CInt(FechaProcesodateTimePicker.Value.ToString("yyyyMMdd")))

                If (RegistrosPendientes = 0) Then
                    MessageBox.Show("No se puede realizar la preparación de data puesto que la OT, Cargue y/o Fecha de Proceso seleccionada aun tiene registros pendientes por procesar.", "Validaciones Dinámicas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Return

                End If

                If OTDesktopComboBox.SelectedValue.ToString() = "--TODOS--" Then
                    MessageBox.Show("Seleccione una OT valida.", "Validaciones Dinámicas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Return
                End If

                Dim Respuesta1 = dbmCore.SchemaProcess.PA_Validaciones_Dinamicas_Documentos_Obligatorios.DBExecute(CInt(OTDesktopComboBox.SelectedValue), Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, Program.Sesion.Usuario.id)

                If Respuesta1.ToString() = "OK" Then
                    Dim Respuesta2 = dbmCore.SchemaProcess.PA_Validaciones_Dinamicas_Novedades.DBExecute(CInt(OTDesktopComboBox.SelectedValue), Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, Program.Sesion.Usuario.id)
                    If Respuesta2.ToString() = "OK" Then
                        DesktopMessageBoxControl.DesktopMessageShow("Proceso realizado con éxito", "Validaciones Dinámicas", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True, False)
                    Else
                        DesktopMessageBoxControl.DesktopMessageShow(Respuesta2.ToString(), "Validaciones Dinámicas Novedades", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True, False)
                    End If
                Else
                    DesktopMessageBoxControl.DesktopMessageShow(Respuesta1.ToString(), "Validaciones Dinámicas Doc Obligatorios", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True, False)
                End If

                dbmCore.Transaction_Commit()
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("ValidacionesDinamicas", ex)
                dbmCore.Transaction_Rollback()
            Finally
                dbmCore.Connection_Close()
            End Try
        End Sub

        Private Sub CargarOT()
            Dim dbmImaging As New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
            Try
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                Dim OTDataTable = dbmImaging.SchemaProcess.TBL_OT.DBFindByfk_Entidadfk_Proyectofk_fecha_procesoCerrado(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, CType(FechaProcesodateTimePicker.Value.ToString("yyyyMMdd"), Global.Slyg.Tools.SlygNullable(Of Integer)), False)

                OTDesktopComboBox.DataSource = Nothing

                If OTDataTable.Count > 0 Then
                    Utilities.LlenarCombo(OTDesktopComboBox, OTDataTable, DBImaging.SchemaProcess.TBL_OTEnum.id_OT.ColumnName, DBImaging.SchemaProcess.TBL_OTEnum.id_OT.ColumnName, True, "-1", "--TODOS--")
                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Cargar OT", ex)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

        Private Sub FechaProcesodateTimePicker_ValueChanged(sender As System.Object, e As System.EventArgs) Handles FechaProcesodateTimePicker.ValueChanged
            CargarOT()
        End Sub
    End Class
End Namespace