Imports Miharu.Desktop.Controls.DesktopMessageBox

Namespace Imaging.Tarjeta_Credito.Procesos.Publicacion


    Public Class FormPublicacion

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
        Private Sub AceptarButton_Click(sender As System.Object, e As System.EventArgs) Handles AceptarButton.Click
            Dim Respuesta = DesktopMessageBoxControl.DesktopMessageShow("¿Está seguro que desea generar la publicación?", "Publicación", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, False)

            If (Respuesta = DialogResult.OK) Then
                Publicar()
            End If
        End Sub

        Private Sub CerrarButton_Click(sender As System.Object, e As System.EventArgs) Handles CerrarButton.Click
            Me.Close()
        End Sub

#End Region

       
#Region " Metodos "

        Private Sub Publicar()
            Dim dbmIntegration As New DBIntegration.DBIntegrationDataBaseManager(Me._Plugin.CoomevaConnectionString)
            Dim dbmImaging As New DBImaging.DBImagingDataBaseManager(Me._Plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)

            Try
                dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
                dbmImaging.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                Dim ProcesoDataTable As DBImaging.SchemaProcess.TBL_Fecha_ProcesoDataTable
                Dim OTPendientesProceso As Boolean = False

                ProcesoDataTable = dbmImaging.SchemaProcess.TBL_Fecha_Proceso.DBFindByfk_Entidadfk_Proyectoid_fecha_proceso(_Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, FechaProcesoDateTimePicker.Value.ToString("yyyyMMdd"))

                If (ProcesoDataTable.Count = 1) Then
                    If (ProcesoDataTable(0).Cerrado = False) Then
                        Dim OTDataTable = dbmImaging.SchemaProcess.TBL_OT.DBFindByfk_Entidadfk_Proyectofk_fecha_procesoCerrado(_Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, FechaProcesoDateTimePicker.Value.ToString("yyyyMMdd"), Nothing)
                        If OTDataTable.Count > 0 Then
                            For Each ot As DBImaging.SchemaProcess.TBL_OTRow In OTDataTable
                                Dim Respuesta = dbmImaging.SchemaProcess.PA_Validar_Documentos_Pendientes_OT.DBExecute(ot.id_OT, _Plugin.Manager.Sesion.Usuario.id)

                                If Respuesta = 1 Then
                                    OTPendientesProceso = True
                                End If
                            Next

                            If (OTPendientesProceso) Then
                                MessageBox.Show("La Fecha de Proceso seleccionada tiene OT's con documentos pendientes de proceso.", "Publicación", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Else
                                Dim OTData = dbmImaging.SchemaProcess.TBL_OT.DBFindByfk_Entidadfk_Proyectofk_fecha_procesoCerradofk_Entidad_Procesamiento(Me._Plugin.Manager.ImagingGlobal.Entidad, Me._Plugin.Manager.ImagingGlobal.Proyecto, FechaProcesoDateTimePicker.Value.ToString("yyyyMMdd"), Nothing, Me._Plugin.Manager.DesktopGlobal.PuestoTrabajoRow.fk_Entidad)

                                For Each ot As DBImaging.SchemaProcess.TBL_OTRow In OTData
                                    dbmIntegration.SchemaCoomeva.PA_PublicarData.DBExecute(ot.id_OT)
                                Next

                                MessageBox.Show("Publicación Exitosa", "Pubicación", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            End If
                        Else
                            MessageBox.Show("No se encuentra OTs asociadas a la Fecha de Proceso seleccionada.", "Publicación", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        End If
                    Else
                        MessageBox.Show("La Fecha de Proceso seleccionada ya fue cerrada.", "Publicación", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    End If
                Else
                    MessageBox.Show("No se encuentra información asociada a la Fecha de Proceso seleccionada.", "Publicación", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Publicación", ex)
            Finally
                If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub
#End Region
       
    End Class
End Namespace
