Imports DBSecurity
Imports DBSecurity.SchemaConfig
Imports Miharu.Desktop.Controls.DesktopMessageBox

Namespace Imaging.Multiactiva.Procesos.Rechazos

    Public Class FormRechazos_ValidacionesCampos
#Region " Declaraciones "

        Private _Plugin As Plugin

#End Region
#Region " Constructores "

        Public Sub New(ByVal nPlugin As Plugin)
            InitializeComponent()

            _Plugin = nPlugin
        End Sub

#End Region

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
            'Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = Nothing

            Try
                'dbmCore = New DBCore.DBCoreDataBaseManager(Me._Plugin.Manager.DesktopGlobal.ConnectionStrings.Core)
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Me._Plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)
                dbmIntegration = New DBIntegration.DBIntegrationDataBaseManager(Me._Plugin.CoomevaConnectionString)

                'dbmCore.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
                dbmImaging.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
                dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                Dim FechaProcesoDataTable = dbmImaging.SchemaProcess.TBL_Fecha_Proceso.DBFindByfk_Entidadfk_Proyectoid_fecha_proceso(_Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, CType(FechaProcesoDateTimePicker.Value.ToString("yyyyMMdd"), Global.Slyg.Tools.SlygNullable(Of Integer)))

                If FechaProcesoDataTable.Count > 0 Then
                    Dim Cerrada = FechaProcesoDataTable(0).Cerrado

                    If Not Cerrada Then
                        Dim RespuestaOT = dbmImaging.SchemaProcess.PA_Consultar_OT_x_Fecha_Proceso.DBExecute(Me._Plugin.Manager.DesktopGlobal.PuestoTrabajoRow.fk_Entidad, _Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, FechaProcesoDataTable(0).id_fecha_proceso)

                        If RespuestaOT = 1 Then
                            dbmIntegration.SchemaCoomeva.PA_Multiactiva_Exp_Rechazados_ValidaCampos_Negativos.DBExecute(CType(FechaProcesoDateTimePicker.Value.ToString("yyyyMMdd"), Global.Slyg.Tools.SlygNullable(Of Integer)), _Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto)
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
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
            End Try
        End Sub

    End Class
End Namespace
