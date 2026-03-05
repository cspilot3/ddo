Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Controls
Imports DBCore
Imports Miharu.Desktop.Library

Namespace Forms.Recepcion

    Public Class FormRemisiones
        Inherits FormBase

#Region " Eventos "

        Private Sub FormRecepcionRemisiones_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            CargaRemisiones()
        End Sub

        Private Sub RecibirButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles RecibirButton.Click
            If RemisionesListBox.SelectedIndex > -1 Then
                RecibirRemision(CInt(RemisionesListBox.SelectedValue))
            Else
                DesktopMessageBoxControl.DesktopMessageShow("Debe seleccionar una remisión.", "Seleccionar Remisión", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
            End If
        End Sub

        Private Sub CerrarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CerrarButton.Click
            Me.Close()
        End Sub

#End Region

#Region " Metodos "
        Private Sub CargaRemisiones()
            Dim dbmCore As New DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
            Try
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)
                If Not IsNothing(Program.DesktopGlobal.BovedaRow) Then
                    Dim dtRemisiones = dbmCore.SchemaProcess.TBL_Remision_Caja.DBFindByfk_Entidadfk_Sedefk_Bovedafk_Estado(CShort(Program.DesktopGlobal.BovedaRow.fk_Entidad), Program.DesktopGlobal.BovedaRow.fk_Sede, Program.DesktopGlobal.BovedaRow.id_Boveda, EstadoEnum.Enviado_a_custodia)
                    RemisionesListBox.DataSource = dtRemisiones
                    RemisionesListBox.ValueMember = dtRemisiones.Id_Remision_CajaColumn.ColumnName
                    RemisionesListBox.DisplayMember = dtRemisiones.Id_Remision_CajaColumn.ColumnName
                Else
                    DesktopMessageBoxControl.DesktopMessageShow("No existe bóveda para procesar.", "Bóveda inexistente", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaRemisiones", ex)
            Finally
                dbmCore.Connection_Close()
            End Try
        End Sub

        Private Sub RecibirRemision(ByVal idRemision As Long)
            Try
                Dim objRecibir As New FormRecibirRemision(idRemision)
                objRecibir.ShowDialog()
                CargaRemisiones()
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("RecibirRemision", ex)
            End Try
        End Sub
#End Region

    End Class

End Namespace