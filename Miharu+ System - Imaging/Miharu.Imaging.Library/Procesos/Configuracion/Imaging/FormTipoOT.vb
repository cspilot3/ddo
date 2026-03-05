Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Controls
Imports Miharu.Desktop

Namespace Procesos.Configuracion.Imaging

    Public Class FormTipoOT
        Inherits Desktop.Library.FormBase

#Region " Eventos "

        Private Sub FormValidaciones_Load(sender As System.Object, e As EventArgs) Handles MyBase.Load
            CargarDatos()
        End Sub

        Private Sub AddTipoOT_Click(sender As System.Object, e As EventArgs) Handles AddTipoOT.Click
            Try
                Dim Entidad = Program.ImagingGlobal.Entidad
                Dim Proyecto = Program.ImagingGlobal.Proyecto

                Dim Agregar As New FormNuevoTipoOT(Entidad, Proyecto)
                If Agregar.ShowDialog() = DialogResult.OK Then CargarDatos()

            Catch
                DesktopMessageBoxControl.DesktopMessageShow("Por favor valide que ha seleccionado [Entidad y Proyecto]", "Tipo OT", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
            End Try
        End Sub

        Private Sub TipoOTDataGridView_DoubleClick(sender As System.Object, e As EventArgs) Handles TipoOTDataGridView.DoubleClick
            Dim row = TipoOTDataGridView.SelectedRows(0)
            Dim idOTTipo = row.Value(Of Short)("id_OT_Tipo")
            Dim fkEntidad = row.Value(Of Short)("fk_Entidad")
            Dim fkProyecto = row.Value(Of Short)("fk_Proyecto")
            Dim Nombre_OT_Tipo = row.Value("Nombre")
            Dim Descripcion_OT_Tipo = row.Value("Descripcion")
            Dim Eliminado = row.Value(Of Boolean)("Eliminado")

            Dim Agregar As New FormNuevoTipoOT(idOTTipo, fkEntidad, fkProyecto, Nombre_OT_Tipo, Descripcion_OT_Tipo, Eliminado)
            If Agregar.ShowDialog() = DialogResult.OK Then CargarDatos()
        End Sub

        Private Sub TipoOTDataGridView_CellContentClick(sender As System.Object, e As DataGridViewCellEventArgs)
            If (e.ColumnIndex = 0) AndAlso DesktopMessageBoxControl.DesktopMessageShow("¿Esta seguro de eliminar el Tipo de OT?", "Eliminar", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, False) = DialogResult.OK Then
                EliminarTipoOT(e.RowIndex)
            End If
        End Sub

#End Region

#Region " Metodos "

        Public Sub EliminarTipoOT(Index As Integer)
            Dim dmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dmImaging.Connection_Open(Program.Sesion.Usuario.id)
                dmImaging.Transaction_Begin()

                Dim Row = TipoOTDataGridView.Rows(Index)
                Dim idOTTipo = Row.Value(Of Short)("id_OT_Tipo")
                Dim fkEntidad = Row.Value(Of Short)("fk_Entidad")
                Dim fkProyecto = Row.Value(Of Short)("fk_Proyecto")
                dmImaging.SchemaProcess.TBL_OT_Tipo.DBDelete(fkEntidad, fkProyecto, idOTTipo)

                dmImaging.Transaction_Commit()
                CargarDatos()

                DesktopMessageBoxControl.DesktopMessageShow("Tipo de OT eliminada con exito.", "Eliminar", DesktopMessageBoxControl.IconEnum.SuccessfullIcon)

            Catch
                If dmImaging IsNot Nothing Then dmImaging.Transaction_Rollback()
                DesktopMessageBoxControl.DesktopMessageShow("No ha sido posible eliminar el tipo de OT, por favor valide que ninguna OT contenga este tipo.", "Eliminar", DesktopMessageBoxControl.IconEnum.WarningIcon)
            Finally
                If dmImaging IsNot Nothing Then dmImaging.Connection_Close()
            End Try
        End Sub

        Public Sub CargarDatos()
            Dim dmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dmImaging.Connection_Open(Program.Sesion.Usuario.id)

                Dim TipoOT = dmImaging.SchemaProcess.TBL_OT_Tipo.DBGet(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, Nothing)
                If TipoOT.Rows.Count = 0 Then
                    DesktopMessageBoxControl.DesktopMessageShow("No se han encontrado Tipos OT para los filtros realizados.", "Tipo OT", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                End If

                TipoOTDataGridView.AutoGenerateColumns = False
                TipoOTDataGridView.DataSource = TipoOT

            Catch
                Throw
            Finally
                If dmImaging IsNot Nothing Then dmImaging.Connection_Close()
            End Try
        End Sub

#End Region

    End Class

End Namespace