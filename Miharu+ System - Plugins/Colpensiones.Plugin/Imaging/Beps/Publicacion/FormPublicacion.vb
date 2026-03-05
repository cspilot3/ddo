Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports System.Windows.Forms
Imports Miharu.Desktop.Library.Config

Namespace Imaging.Beps.Publicacion
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

        Private Sub FormPublicacion_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            CargarOT()
        End Sub

        Private Sub FechaProcesoDateTimePicker_ValueChanged(sender As System.Object, e As System.EventArgs) Handles FechaProcesoDateTimePicker.ValueChanged
            CargarOT()
        End Sub
#End Region

#Region " Metodos "

        Private Sub Publicar()
            Dim dbmIntegration As New DBIntegration.DBIntegrationDataBaseManager(Me._Plugin.ColpensionesConnectionString)
            Dim dbmImaging As New DBImaging.DBImagingDataBaseManager(Me._Plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)

            Try
                dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
                dbmImaging.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                If Validar() Then
                    If OTDesktopComboBox.SelectedIndex = 0 Then
                        Dim OTData = dbmImaging.SchemaProcess.TBL_OT.DBFindByfk_Entidadfk_Proyectofk_fecha_procesoCerrado(Me._Plugin.Manager.ImagingGlobal.Entidad, Me._Plugin.Manager.ImagingGlobal.Proyecto, FechaProcesoDateTimePicker.Value.ToString("yyyyMMdd"), True)

                        For Each ot As DBImaging.SchemaProcess.TBL_OTRow In OTData
                            dbmIntegration.SchemaColpensionesBEPS.PA_Publicar_Report_Inventario.DBExecute(_Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, ot.id_OT)
                        Next
                    Else
                        dbmIntegration.SchemaColpensionesBEPS.PA_Publicar_Report_Inventario.DBExecute(_Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, CInt(OTDesktopComboBox.SelectedValue))
                    End If

                    MessageBox.Show("Publicación Exitosa", "Publicación", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Publicación", ex)
            Finally
                If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

        Private Sub CargarOT()
            Dim dbmIntegration As New DBIntegration.DBIntegrationDataBaseManager(Me._Plugin.ColpensionesConnectionString)

            Try
                dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                Dim OTDataTable = dbmIntegration.SchemaColpensionesBEPS.PA_Get_OT_Publicar.DBExecute(_Plugin.Manager.ImagingGlobal.ProyectoImagingRow.fk_Entidad, _Plugin.Manager.ImagingGlobal.ProyectoImagingRow.fk_Proyecto, FechaProcesoDateTimePicker.Value.ToString("yyyyMMdd"))
                OTDesktopComboBox.DataSource = Nothing

                If OTDataTable.Count > 0 Then
                    Utilities.LlenarCombo(OTDesktopComboBox, OTDataTable, DBImaging.SchemaProcess.TBL_OTEnum.id_OT.ColumnName, DBImaging.SchemaProcess.TBL_OTEnum.id_OT.ColumnName, True, "-1", "--TODOS--")
                End If

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Cargar OT", ex)
            Finally
                If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
            End Try
        End Sub
#End Region

#Region " Funciones "
        Private Function Validar() As Boolean

            If OTDesktopComboBox.SelectedIndex = -1 Then
                MessageBox.Show("Debe seleccionar una OT", "Publicación", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return False
            End If

            Return True

        End Function
#End Region
    End Class
End Namespace
