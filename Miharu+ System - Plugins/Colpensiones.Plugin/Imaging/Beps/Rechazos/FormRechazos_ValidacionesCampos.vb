Imports DBSecurity
Imports DBSecurity.SchemaConfig
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports System.Windows.Forms
Imports Miharu.Desktop.Library.Config

Namespace Imaging.Beps.Rechazos

    Public Class FormRechazos_ValidacionesCampos

#Region " Declaraciones "

        Private _Plugin As Plugin

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

        Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
            Me.Close()
        End Sub

        Private Sub FormRechazos_ValidacionesCampos_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            CargarOT()
        End Sub

        Private Sub FechaProcesoDateTimePicker_ValueChanged(sender As System.Object, e As System.EventArgs) Handles FechaProcesoDateTimePicker.ValueChanged
            CargarOT()
        End Sub

#End Region

#Region " Metodos "
        Private Sub Rechazar()
            Dim dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = Nothing
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dbmIntegration = New DBIntegration.DBIntegrationDataBaseManager(Me._Plugin.ColpensionesConnectionString)
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Me._Plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)

                dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
                dbmImaging.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                If Validar() Then
                    If OTDesktopComboBox.SelectedIndex = 0 Then
                        Dim RespuestaOT = dbmImaging.SchemaProcess.PA_Consultar_OT_x_Fecha_Proceso.DBExecute(_Plugin.Manager.DesktopGlobal.PuestoTrabajoRow.fk_Entidad, _Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, FechaProcesoDateTimePicker.Value.ToString("yyyyMMdd"))
                        If RespuestaOT = 1 Then
                            Dim OTData = dbmImaging.SchemaProcess.TBL_OT.DBFindByfk_Entidadfk_Proyectofk_fecha_procesoCerrado(Me._Plugin.Manager.ImagingGlobal.Entidad, Me._Plugin.Manager.ImagingGlobal.Proyecto, FechaProcesoDateTimePicker.Value.ToString("yyyyMMdd"), False)
                            For Each ot As DBImaging.SchemaProcess.TBL_OTRow In OTData
                                dbmIntegration.SchemaColpensionesBEPS.PA_Expedientes_Rechazados_ValidacionesCampos_Negativos.DBExecute(CType(FechaProcesoDateTimePicker.Value.ToString("yyyyMMdd"), Global.Slyg.Tools.SlygNullable(Of Integer)), _Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, ot.id_OT)
                            Next
                            MessageBox.Show("Proceso de rechazo realizado con éxito.", "Rechazos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Else
                            MessageBox.Show("No se puede realizar el proceso de Rechazos ya que hay una o más OT's con registros pendientes de proceso.", "Rechazos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        End If
                    Else
                        Dim RespuestaOT = dbmImaging.SchemaProcess.PA_Consultar_Cerrar_OT.DBExecute(CInt(OTDesktopComboBox.SelectedValue), _Plugin.Manager.Sesion.Usuario.id)
                        If RespuestaOT = 1 Then
                            dbmIntegration.SchemaColpensionesBEPS.PA_Expedientes_Rechazados_ValidacionesCampos_Negativos.DBExecute(CType(FechaProcesoDateTimePicker.Value.ToString("yyyyMMdd"), Global.Slyg.Tools.SlygNullable(Of Integer)), _Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, CInt(OTDesktopComboBox.SelectedValue))
                            MessageBox.Show("Proceso de rechazo realizado con éxito.", "Rechazos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Else
                            MessageBox.Show("No se puede realizar el proceso de Rechazos ya que la OT tiene registros pendientes de proceso.", "Rechazos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        End If
                    End If
                End If

            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

        Private Sub CargarOT()
            Dim dbmImaging As New DBImaging.DBImagingDataBaseManager(Me._Plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)

            Try
                dbmImaging.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                Dim OTDataTable = dbmImaging.SchemaProcess.TBL_OT.DBFindByfk_Entidadfk_Proyectofk_fecha_procesoCerrado(_Plugin.Manager.ImagingGlobal.ProyectoImagingRow.fk_Entidad, _Plugin.Manager.ImagingGlobal.ProyectoImagingRow.fk_Proyecto, FechaProcesoDateTimePicker.Value.ToString("yyyyMMdd"), False)
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

            If OTDesktopComboBox.SelectedIndex = -1 Then
                MessageBox.Show("Debe seleccionar una OT", "Rechazos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return False
            End If

            Return True

        End Function
#End Region

        
    End Class
End Namespace
