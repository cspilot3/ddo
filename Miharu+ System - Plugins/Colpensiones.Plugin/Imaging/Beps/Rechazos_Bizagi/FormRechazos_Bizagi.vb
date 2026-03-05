Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports System.Windows.Forms
Imports Miharu.Desktop.Library.Config

Namespace Imaging.Beps.Rechazo_Bizagi
    Public Class FormRechazos_Bizagi

#Region " Declaraciones "

        Private _Plugin As Plugin
        Dim CausalesRechazoDataTable As DBIntegration.SchemaColpensionesBEPS.TBL_Rechazos_BizagiDataTable

#End Region

#Region " Contructores "

        Public Sub New(ByVal nPlugin As Plugin)
            InitializeComponent()

            _Plugin = nPlugin
        End Sub

#End Region

#Region " Eventos "
        Private Sub RechazarButton_Click(sender As System.Object, e As System.EventArgs) Handles RechazarButton.Click
            Dim Respuesta = DesktopMessageBoxControl.DesktopMessageShow("¿Está seguro que desea generar el proceso de Rechazos?", "Rechazos", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, False)
            If (Respuesta = DialogResult.OK) Then
                Rechazar()
            End If
        End Sub

        Private Sub Btn_Cerrar_Click(sender As System.Object, e As System.EventArgs) Handles Btn_Cerrar.Click
            Me.Close()
        End Sub

        Private Sub FormRechazos_Bizagi_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            CargarRechazos()
            CargarOT()
        End Sub

        Private Sub FechaProcesoDateTimePicker_ValueChanged(sender As System.Object, e As System.EventArgs) Handles FechaProcesoDateTimePicker.ValueChanged
            CargarOT()
        End Sub
#End Region

#Region " Funciones "
        Private Function Validar() As Boolean

            If OTDesktopComboBox.SelectedIndex = 0 Then
                MessageBox.Show("Debe seleccionar una OT", "Rechazos Bizagi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return False
            ElseIf Me.txtNo_Formulario.Text = "" Then
                MessageBox.Show("Debe digitar un No. Formulario", "Rechazos Bizagi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return False
            ElseIf CheckedListBoxRechazos.CheckedIndices.Count = 0 Then
                MessageBox.Show("Debe seleccionar al menos una causal de rechazo", "Rechazos Bizagi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return False
            End If

            Return True

        End Function
#End Region

#Region " Metodos "
        Private Sub Rechazar()
            Dim dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = Nothing
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Dim RechazadosGrabados As Integer = 0
            Dim IndexChecked As Integer
            Dim fkRechazos As String = ""

            Try
                dbmIntegration = New DBIntegration.DBIntegrationDataBaseManager(Me._Plugin.ColpensionesConnectionString)
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Me._Plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)

                dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
                dbmImaging.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                If (Validar()) Then
                    Dim TBL_Report_InventarioDataTable = dbmIntegration.SchemaColpensionesBEPS.TBL_Report_Inventario.DBFindByfk_OTFecha_Proceso(CInt(OTDesktopComboBox.SelectedValue), CType(FechaProcesoDateTimePicker.Value.ToString("yyyyMMdd"), Global.Slyg.Tools.SlygNullable(Of Integer)))
                    If TBL_Report_InventarioDataTable.Count > 0 Then

                        Dim TBL_Report_InventarioRegistroDataTable = dbmIntegration.SchemaColpensionesBEPS.TBL_Report_Inventario.DBFindByfk_OTFecha_ProcesoNo_Formulario(CInt(OTDesktopComboBox.SelectedValue), CType(FechaProcesoDateTimePicker.Value.ToString("yyyyMMdd"), Global.Slyg.Tools.SlygNullable(Of Integer)), txtNo_Formulario.Text)
                        If TBL_Report_InventarioRegistroDataTable.Count > 0 Then
                            For Each IndexChecked In CheckedListBoxRechazos.CheckedIndices

                                Dim Prb As String = CheckedListBoxRechazos.GetItemChecked(IndexChecked)

                                dbmIntegration.SchemaColpensionesBEPS.PA_Guardar_Registro_Rechazado_Bizagi.DBExecute(txtNo_Formulario.Text,
                                                                                                                     CType(FechaProcesoDateTimePicker.Value.ToString("yyyyMMdd"), Global.Slyg.Tools.SlygNullable(Of Integer)),
                                                                                                                     CInt(OTDesktopComboBox.SelectedValue),
                                                                                                                     Int16.Parse(CausalesRechazoDataTable.Rows(IndexChecked)(0)),
                                                                                                                     _Plugin.Manager.Sesion.Usuario.id)
                                RechazadosGrabados += 1

                            Next

                            dbmIntegration.SchemaColpensionesBEPS.PA_Actualizar_Rechazos_Bizagi_Report_Inventario.DBExecute(txtNo_Formulario.Text,
                                                                                     CType(FechaProcesoDateTimePicker.Value.ToString("yyyyMMdd"), Global.Slyg.Tools.SlygNullable(Of Integer)),
                                                                                     CInt(OTDesktopComboBox.SelectedValue))

                            If RechazadosGrabados > 0 Then
                                MessageBox.Show("Registro guardado con éxito", "Rechazos Bizagi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                LimpiarControles()
                            End If

                        Else
                            MessageBox.Show("El número de formulario no es válido o no pertenece a la OT y/o fecha de proceso", "Rechazos Bizagi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        End If
                    Else
                        MessageBox.Show("La Fecha de proceso seleccionada no ha sido publicada.", "Rechazos Bizagi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    End If
                End If

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Rechazos Bizagi", ex)
            Finally
                If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

        Private Sub CargarRechazos()
            Dim dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = Nothing

            Try
                dbmIntegration = New DBIntegration.DBIntegrationDataBaseManager(Me._Plugin.ColpensionesConnectionString)
                dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                CausalesRechazoDataTable = dbmIntegration.SchemaColpensionesBEPS.TBL_Rechazos_Bizagi.DBFindByEliminado(False)

                CheckedListBoxRechazos.DataSource = CausalesRechazoDataTable
                CheckedListBoxRechazos.DisplayMember = "Descripcion_Rechazo"
                CheckedListBoxRechazos.ValueMember = "id_Rechazo"

            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
            End Try
        End Sub

        Private Sub LimpiarControles()
            Me.txtNo_Formulario.Text = ""
            For i = 0 To CausalesRechazoDataTable.Rows.Count - 1
                CheckedListBoxRechazos.SetItemChecked(i, False)
            Next
        End Sub

        Private Sub CargarOT()
            Dim dbmImaging As New DBImaging.DBImagingDataBaseManager(Me._Plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)

            Try
                dbmImaging.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                Dim OTDataTable = dbmImaging.SchemaProcess.TBL_OT.DBFindByfk_Entidadfk_Proyectofk_fecha_procesoCerrado(_Plugin.Manager.ImagingGlobal.ProyectoImagingRow.fk_Entidad, _Plugin.Manager.ImagingGlobal.ProyectoImagingRow.fk_Proyecto, FechaProcesoDateTimePicker.Value.ToString("yyyyMMdd"), True)

                If OTDataTable.Count > 0 Then
                    Utilities.LlenarCombo(OTDesktopComboBox, OTDataTable, DBImaging.SchemaProcess.TBL_OTEnum.id_OT.ColumnName, DBImaging.SchemaProcess.TBL_OTEnum.id_OT.ColumnName, True)
                End If

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Cargar OT", ex)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub
#End Region
    End Class
End Namespace
