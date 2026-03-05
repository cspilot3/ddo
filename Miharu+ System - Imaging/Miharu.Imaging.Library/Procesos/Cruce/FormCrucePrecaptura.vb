Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Controls
Imports System.Windows.Forms
Imports Slyg.Tools
Imports Miharu.Tools.Progress
Imports Miharu.Desktop.Library.Config
Imports System.Text
Imports Miharu.Security.Library.Session
Imports Microsoft.Reporting.WinForms
Imports Miharu.Desktop.Library

Namespace Procesos.Cruce
    Public Class FormCrucePrecaptura

#Region " Declaraciones "
        Public Property WorkSpace() As FormImagingWorkSpace
#End Region

#Region " Eventos "
        Private Sub FormCruce_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            CargarOT()
        End Sub

        Private Sub FechaProcesoDateTimePicker_ValueChanged(sender As System.Object, e As System.EventArgs) Handles FechaProcesodateTimePicker.ValueChanged
            CargarOT()
        End Sub

        Private Sub CruzarButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CruzarButton.Click
            Dim Respuesta = DesktopMessageBoxControl.DesktopMessageShow("¿Está seguro que desea generar el Cruce?", "Cruce", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, False)

            If (Respuesta = DialogResult.OK) Then
                GeneraCruce()
            End If
        End Sub

        Private Sub PrepararDataButton_Click(sender As System.Object, e As System.EventArgs) Handles PrepararDataButton.Click
            Dim Respuesta = DesktopMessageBoxControl.DesktopMessageShow("¿Está seguro que desea preparar la data?", "Preparar Data", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, False)

            If (Respuesta = DialogResult.OK) Then
                PrepararData()
            End If
        End Sub
#End Region

#Region " Métodos "
        Private Sub GeneraCruce()
            If Validar() Then
                Dim dbmImaging As New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                Dim Mensaje As String = ""
                Dim ProgressForm As New Slyg.Tools.Progress.FormProgress()

                Try
                    Me.Enabled = False
                    Me.Cursor = Cursors.WaitCursor

                    dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                    Dim ProcesoDataTable As DBImaging.SchemaProcess.TBL_Fecha_ProcesoDataTable
                    ProcesoDataTable = dbmImaging.SchemaProcess.TBL_Fecha_Proceso.DBFindByfk_Entidadfk_Proyectoid_fecha_proceso(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, CInt(FechaProcesodateTimePicker.Value.ToString("yyyyMMdd")))

                    Dim RegistrosPendientes = dbmImaging.SchemaProcess.PA_Validacion_Registros_Precaptura_Fecha_Proceso.DBExecute(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, CInt(Me.CargueTextBox.Text), CInt(FechaProcesodateTimePicker.Value.ToString("yyyyMMdd")))

                    If (RegistrosPendientes = 0) Then
                        MessageBox.Show("No se puede realizar la preparación de data puesto que la OT, Cargue y/o Fecha de Proceso seleccionada aun tiene registros pendientes por procesar.", "Preparar Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Return
                    End If

                    If (ProcesoDataTable.Count = 1) Then
                        If (ProcesoDataTable(0).Cerrado = False) Then
                            'Iniciar proceso                
                            ProgressForm.Process = ""
                            ProgressForm.Action = ""
                            ProgressForm.ValueProcess = 0
                            ProgressForm.ValueAction = 0
                            ProgressForm.MaxValueProcess = 1
                            ProgressForm.MaxValueAction = 1

                            ProgressForm.Show()

                            ProgressForm.Action = "Cruce"
                            ProgressForm.ValueAction = 1
                            Application.DoEvents()

                            Dim Resultado = dbmImaging.SchemaProcess.PA_Cruce_Generico.DBExecute(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, CInt(FechaProcesodateTimePicker.Value.ToString("yyyyMMdd")), CInt(IIf(OTDesktopComboBox.SelectedValue.Equals("--TODOS--"), -1, OTDesktopComboBox.SelectedValue)), Program.Sesion.Usuario.id)

                            If (Resultado.ToString() = "Cruce Finalizado.") Then
                                Me.WorkSpace.EventManager.FinalizarCruceGenerico(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, CInt(FechaProcesodateTimePicker.Value.ToString("yyyyMMdd")), CInt(IIf(OTDesktopComboBox.SelectedValue.Equals("--TODOS--"), -1, OTDesktopComboBox.SelectedValue)))
                            End If

                            MessageBox.Show(Resultado.ToString(), "Cruce", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Else
                            MessageBox.Show("La Fecha de Proceso seleccionada ya fue cerrada.", "Cruce", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        End If
                    Else
                        MessageBox.Show("No se encuentra información asociada a la Fecha de Proceso seleccionada.", "Cruce", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    End If
                Catch ex As Exception
                    ProgressForm.Hide()
                    Application.DoEvents()
                    DesktopMessageBoxControl.DesktopMessageShow("GeneraCruce", ex)
                Finally
                    ProgressForm.Visible = False
                    ProgressForm.Close()

                    If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()

                    Me.Enabled = True
                    Me.Cursor = Cursors.Default
                End Try
            End If
        End Sub

        Private Sub PrepararData()
            If Validar() Then
                Dim dbmImaging As New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                Dim Mensaje As String = ""
                Dim ProgressForm As New Slyg.Tools.Progress.FormProgress()

                Try
                    Me.Enabled = False
                    Me.Cursor = Cursors.WaitCursor

                    dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                    Dim ProcesoDataTable As DBImaging.SchemaProcess.TBL_Fecha_ProcesoDataTable
                    ProcesoDataTable = dbmImaging.SchemaProcess.TBL_Fecha_Proceso.DBFindByfk_Entidadfk_Proyectoid_fecha_proceso(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, CInt(FechaProcesodateTimePicker.Value.ToString("yyyyMMdd")))

                    Dim RegistrosPendientes = dbmImaging.SchemaProcess.PA_Validacion_Registros_Precaptura_Fecha_Proceso.DBExecute(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, CInt(Me.CargueTextBox.Text), CInt(FechaProcesodateTimePicker.Value.ToString("yyyyMMdd")))

                    If (RegistrosPendientes = 0) Then
                        MessageBox.Show("No se puede realizar la preparación de data puesto que la OT, Cargue y/o Fecha de Proceso seleccionada aun tiene registros pendientes por procesar.", "Preparar Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Return
                    End If

                    If (ProcesoDataTable.Count = 1) Then
                        If (ProcesoDataTable(0).Cerrado = False) Then
                            'Iniciar proceso                
                            ProgressForm.Process = ""
                            ProgressForm.Action = ""
                            ProgressForm.ValueProcess = 0
                            ProgressForm.ValueAction = 0
                            ProgressForm.MaxValueProcess = 1
                            ProgressForm.MaxValueAction = 1

                            ProgressForm.Show()

                            ProgressForm.Action = "Preparación Data"
                            ProgressForm.ValueAction = 1
                            Application.DoEvents()

                            Dim Resultado = dbmImaging.SchemaProcess.PA_Preparar_Data.DBExecute(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, CInt(FechaProcesodateTimePicker.Value.ToString("yyyyMMdd")), CInt(IIf(OTDesktopComboBox.SelectedValue.Equals("--TODOS--"), -1, OTDesktopComboBox.SelectedValue)))

                            MessageBox.Show(Resultado.ToString(), "Preparar Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Else
                            MessageBox.Show("La Fecha de Proceso seleccionada ya fue cerrada.", "Preparar Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        End If
                    Else
                        MessageBox.Show("No se encuentra información asociada a la Fecha de Proceso seleccionada.", "Preparar Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    End If
                Catch ex As Exception
                    ProgressForm.Hide()
                    Application.DoEvents()
                    DesktopMessageBoxControl.DesktopMessageShow("Preparar Data", ex)
                Finally
                    ProgressForm.Visible = False
                    ProgressForm.Close()

                    If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()

                    Me.Enabled = True
                    Me.Cursor = Cursors.Default
                End Try
            End If
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
#End Region

#Region " Funciones "
        Private Function Validar() As Boolean
            If OTDesktopComboBox.Items.Count <= 0 Then
                MessageBox.Show("La Fecha de Proceso seleccionada no contiene OT(s)", "Cruce", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return False
            End If

            If (Not DataConvert.IsNumeric(Me.CargueTextBox.Text)) Then
                MessageBox.Show("El cargue debe ser un valor numérico", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If

            Return True
        End Function
#End Region

    End Class
End Namespace
