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

Namespace Imaging.Carpeta_Unica.Forms.Empaque
    Public Class FormActualizacionEmpaque

#Region " Declaraciones "

        Private _Plugin As CarpetaUnicaPlugin
        Private _TipoProcesoDataTable As DBIntegration.SchemaBCSCarpetaUnica.TBL_Tipo_ProcesoDataTable

#End Region

#Region " Contructores "

        Public Sub New(ByVal nCarpetaUnicaDesktopPlugin As CarpetaUnicaPlugin)
            InitializeComponent()

            _Plugin = nCarpetaUnicaDesktopPlugin
            CargaTablas()
        End Sub

#End Region

#Region " Eventos "

        Private Sub FormActualizacionEmpaque_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            CargaTipoProceso()
        End Sub

        Private Sub ActualizarButton_Click(sender As System.Object, e As System.EventArgs) Handles ActualizarButton.Click
            Dim Respuesta = DesktopMessageBoxControl.DesktopMessageShow("¿Está seguro que desea generar la actualización?", "Actualización Empaque", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, False)

            If (Respuesta = DialogResult.OK) Then
                ActualizarEmpaque()
            End If
        End Sub

        Private Sub SalirButton_Click(sender As System.Object, e As System.EventArgs) Handles SalirButton.Click
            Me.Close()
        End Sub

#End Region

#Region " Metodos "

        Private Sub CargaTablas()
            Dim dbmIntegration As New DBIntegration.DBIntegrationDataBaseManager(Me._Plugin.CajaSocialConnectionString)
            Try

                dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                _TipoProcesoDataTable = dbmIntegration.SchemaBCSCarpetaUnica.TBL_Tipo_Proceso.DBGet(Nothing)

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaTablas", ex)
            Finally
                If (dbmIntegration IsNot Nothing) Then
                    dbmIntegration.Connection_Close()
                End If
            End Try
        End Sub

        Private Sub CargaTipoProceso()
            Dim dbmIntegration As New DBIntegration.DBIntegrationDataBaseManager(Me._Plugin.CajaSocialConnectionString)

            Try
                dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                _TipoProcesoDataTable = dbmIntegration.SchemaBCSCarpetaUnica.TBL_Tipo_Proceso.DBFindByActivoAplica_Tipo_Proceso(True, True)

                If _TipoProcesoDataTable.Count > 0 Then
                    Utilities.LlenarCombo(ProcesoDesktopComboBox, _TipoProcesoDataTable, DBIntegration.SchemaBCSCarpetaUnica.TBL_Tipo_ProcesoEnum.id_Tipo_Proceso.ColumnName, DBIntegration.SchemaBCSCarpetaUnica.TBL_Tipo_ProcesoEnum.Nombre_Tipo_Proceso.ColumnName, True, "-1", "--TODOS--")
                Else
                    MessageBox.Show("El proyecto no tiene procesos activos, por favor validar", "CargaFiltros", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaFiltros", ex)
            End Try
        End Sub

        Private Sub ActualizarEmpaque()
            Dim dbmIntegration As New DBIntegration.DBIntegrationDataBaseManager(Me._Plugin.CajaSocialConnectionString)
            Dim dbmImaging As New DBImaging.DBImagingDataBaseManager(Me._Plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)
            Dim ProgressForm As New Slyg.Tools.Progress.FormProgress()

            Try
                Me.Enabled = False
                Me.Cursor = Cursors.WaitCursor

                dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
                dbmImaging.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                Dim ProcesoDataTable As DBImaging.SchemaProcess.TBL_Fecha_ProcesoDataTable
                Dim Procesos As DBIntegration.SchemaBCSCarpetaUnica.TBL_Tipo_ProcesoDataTable
                Dim ProcesosDictionary As New Dictionary(Of Integer, String)

                ProcesoDataTable = dbmImaging.SchemaProcess.TBL_Fecha_Proceso.DBFindByfk_Entidadfk_Proyectoid_fecha_proceso(_Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, FechaProcesodateTimePicker.Value.ToString("yyyyMMdd"))

                If (ProcesoDataTable.Count = 1) Then
                    If (ProcesoDataTable(0).Cerrado = False) Then
                        Dim OTDataTable = dbmImaging.SchemaProcess.TBL_OT.DBFindByfk_Entidadfk_Proyectofk_fecha_procesoCerrado(_Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, FechaProcesodateTimePicker.Value.ToString("yyyyMMdd"), Nothing)
                        If OTDataTable.Count > 0 Then

                            Dim OT = OTDataTable(0).id_OT

                            If (Integer.Parse(ProcesoDesktopComboBox.SelectedValue.ToString()) = -1) Then
                                Procesos = dbmIntegration.SchemaBCSCarpetaUnica.TBL_Tipo_Proceso.DBFindByActivoAplica_Tipo_Proceso(1, 1)

                                'Se adicional los procesos
                                For Each proceso In Procesos
                                    ProcesosDictionary.Add(proceso.id_Tipo_Proceso, proceso.Nombre_Tipo_Proceso)
                                Next

                                'Iniciar proceso                
                                ProgressForm.Process = ""
                                ProgressForm.Action = ""
                                ProgressForm.ValueProcess = 0
                                ProgressForm.ValueAction = 0
                                ProgressForm.MaxValueProcess = ProcesosDictionary.Count
                                ProgressForm.MaxValueAction = 1

                                ProgressForm.Show()

                                Dim i As Integer = 0
                                Dim msg As String = ""
                                Dim Usa_Cruce_En_Linea As Boolean = 0

                                For Each Item In ProcesosDictionary
                                    ProgressForm.Process = Item.Value
                                    ProgressForm.Action = "Ejecutando Actualización Empaque"
                                    ProgressForm.ValueAction = 1
                                    Application.DoEvents()


                                    'logica de busqueda de empaques
                                    Dim EmpaqueDataTable = dbmImaging.SchemaProcess.TBL_Empaque_Data.DBFindByfk_OTfk_Entidadfk_ProyectoData_Campo(OTDataTable(0).id_OT, _Plugin.Manager.ImagingGlobal.ProyectoImagingRow.fk_Entidad, _Plugin.Manager.ImagingGlobal.ProyectoImagingRow.fk_Proyecto, Item.Value)
                                    If EmpaqueDataTable.Count > 0 Then
                                        Dim Empaque As DBImaging.SchemaProcess.TBL_Empaque_DataRow
                                        For Each Empaque In EmpaqueDataTable
                                            dbmIntegration.SchemaBCSCarpetaUnica.PA_Actualizar_Empaque.DBExecute(OTDataTable(0).id_OT, Empaque.fk_Empaque, _Plugin.Manager.ImagingGlobal.ProyectoImagingRow.fk_Entidad, _Plugin.Manager.ImagingGlobal.ProyectoImagingRow.fk_Proyecto)
                                        Next
                                    End If
                                    i += 1
                                    ProgressForm.ValueProcess = i
                                Next
                                MessageBox.Show("Proceso Finalizado.", "Actualización Empaque", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Else
                                'Iniciar proceso 
                                ProgressForm.Process = ""
                                ProgressForm.Action = ""
                                ProgressForm.ValueProcess = 0
                                ProgressForm.ValueAction = 0
                                ProgressForm.MaxValueProcess = 1
                                ProgressForm.MaxValueAction = 1

                                ProgressForm.Show()

                                ProgressForm.Process = ProcesoDesktopComboBox.Text.ToString()
                                ProgressForm.Action = "Ejecutando Actualización Empaque"
                                ProgressForm.ValueAction = 1
                                Application.DoEvents()

                                'logica de busqueda de empaques
                                Dim EmpaqueDataTable = dbmImaging.SchemaProcess.TBL_Empaque_Data.DBFindByfk_OTfk_Entidadfk_ProyectoData_Campo(OTDataTable(0).id_OT, _Plugin.Manager.ImagingGlobal.ProyectoImagingRow.fk_Entidad, _Plugin.Manager.ImagingGlobal.ProyectoImagingRow.fk_Proyecto, ProcesoDesktopComboBox.Text.ToString())
                                If EmpaqueDataTable.Count > 0 Then
                                    Dim Empaque As DBImaging.SchemaProcess.TBL_Empaque_DataRow
                                    For Each Empaque In EmpaqueDataTable
                                        dbmIntegration.SchemaBCSCarpetaUnica.PA_Actualizar_Empaque.DBExecute(OTDataTable(0).id_OT, Empaque.fk_Empaque, _Plugin.Manager.ImagingGlobal.ProyectoImagingRow.fk_Entidad, _Plugin.Manager.ImagingGlobal.ProyectoImagingRow.fk_Proyecto)
                                    Next
                                    MessageBox.Show("Proceso Finalizado.", "Actualización Empaque", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                End If

                            End If
                        Else
                            MessageBox.Show("La OT de la Fecha de Proceso seleccionada ya fue cerrada.", "Actualización Empaque", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        End If
                    Else
                        MessageBox.Show("La Fecha de Proceso seleccionada ya fue cerrada.", "Actualización Empaque", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    End If
                Else
                    MessageBox.Show("No se encuentra información asociada a la Fecha de Proceso seleccionada.", "Actualización Empaque", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
            Catch ex As Exception
                ProgressForm.Hide()
                Application.DoEvents()
                DesktopMessageBoxControl.DesktopMessageShow("Actualización Empaque", ex)
            Finally
                ProgressForm.Visible = False
                ProgressForm.Close()

                If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()

                Me.Enabled = True
                Me.Cursor = Cursors.Default
            End Try
        End Sub

#End Region

        
        
    End Class
End Namespace