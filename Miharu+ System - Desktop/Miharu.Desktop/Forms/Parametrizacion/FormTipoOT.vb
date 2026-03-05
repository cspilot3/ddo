Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Controls

Namespace Forms.Parametrizacion

    Public Class FormTipoOT
        Inherits Library.FormBase

#Region " Eventos "

        Private Sub FormValidaciones_Load(sender As System.Object, e As EventArgs) Handles MyBase.Load
            llenarCombos()
        End Sub

        Private Sub FiltrarButton_Click(sender As System.Object, e As EventArgs) Handles FiltrarButton.Click
            CargarDatos()
        End Sub

        Private Sub Button2_Click(sender As System.Object, e As EventArgs) Handles AddTipoOT.Click
            Dim Agregar As New FormNuevoTipoOT()
            If Agregar.ShowDialog() = DialogResult.OK Then CargarDatos()
        End Sub

        Private Sub TipoOTDataGridView_CellContentDoubleClick(sender As System.Object, e As DataGridViewCellEventArgs) Handles TipoOTDataGridView.CellContentDoubleClick
            Dim Row = TipoOTDataGridView.Rows(e.RowIndex)
            Dim idOTTipo = CShort(Row.Cells("id_OT_Tipo").Value)
            Dim fkEntidad = CShort(Row.Cells("fk_Entidad").Value)
            Dim fkProyecto = CShort(Row.Cells("fk_Proyecto").Value)
            Dim Nombre_OT_Tipo = Row.Cells("Nombre").Value.ToString
            Dim Descripcion_OT_Tipo = Row.Cells("Descripcion").Value.ToString

            Dim Agregar As New FormNuevoTipoOT(idOTTipo, fkEntidad, fkProyecto, Nombre_OT_Tipo, Descripcion_OT_Tipo)
            If Agregar.ShowDialog() = DialogResult.OK Then CargarDatos()
        End Sub

        Private Sub EntidadDesktopComboBox_SelectedIndexChanged(sender As System.Object, e As EventArgs) Handles EntidadDesktopComboBox.SelectedIndexChanged
            llenarProyectos()
        End Sub

        Private Sub TipoOTDataGridView_CellContentClick(sender As System.Object, e As DataGridViewCellEventArgs) Handles TipoOTDataGridView.CellContentClick
            If (e.ColumnIndex = 0) AndAlso DesktopMessageBox.DesktopMessageBoxControl.DesktopMessageShow("¿Esta seguro de eliminar el Tipo de OT?", "Eliminar", Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.IconEnum.AdvertencyIcon, False) = DialogResult.OK Then
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
                Dim idOTTipo = CShort(Row.Cells("id_OT_Tipo").Value)
                Dim fkEntidad = CShort(Row.Cells("fk_Entidad").Value)
                Dim fkProyecto = CShort(Row.Cells("fk_Proyecto").Value)
                dmImaging.SchemaProcess.TBL_OT_Tipo.DBDelete(fkEntidad, fkProyecto, idOTTipo)

                dmImaging.Transaction_Commit()

                DesktopMessageBox.DesktopMessageBoxControl.DesktopMessageShow("Tipo de OT eliminada con exito.", "Eliminar", DesktopMessageBox.DesktopMessageBoxControl.IconEnum.SuccessfullIcon)

            Catch
                dmImaging.Transaction_Rollback()
                DesktopMessageBox.DesktopMessageBoxControl.DesktopMessageShow("No ha sido posible eliminar el tipo de OT, por favor valide que ninguna OT contenga este tipo.", "Eliminar", DesktopMessageBox.DesktopMessageBoxControl.IconEnum.WarningIcon)
            Finally
                If dmImaging IsNot Nothing Then dmImaging.Connection_Close()
            End Try
        End Sub

        Public Sub CargarDatos()
            Dim dmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dmImaging.Connection_Open(Program.Sesion.Usuario.id)

                Dim TipoOT = dmImaging.SchemaProcess.TBL_OT_Tipo.DBGet(CShort(EntidadDesktopComboBox.SelectedValue), CShort(ProyectoComboBox.SelectedValue), Nothing)

                If TipoOT.Rows.Count = 0 Then
                    DesktopMessageBox.DesktopMessageBoxControl.DesktopMessageShow("No se han encontrado Tipos OT para los filtros realizados.", "Tipo OT", DesktopMessageBox.DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                End If

                TipoOTDataGridView.AutoGenerateColumns = False
                TipoOTDataGridView.DataSource = TipoOT

            Catch
                Throw
            Finally
                If dmImaging IsNot Nothing Then dmImaging.Connection_Close()
            End Try
        End Sub

        Public Sub llenarCombos()
            Dim dmCore As DBCore.DBCoreDataBaseManager = Nothing

            Try
                dmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                dmCore.Connection_Open(Program.Sesion.Usuario.id)

                Dim Entidad = dmCore.SchemaSecurity.CTA_Entidad.DBGet()
                EntidadDesktopComboBox.Fill(Entidad, Entidad.id_EntidadColumn, Entidad.Nombre_EntidadColumn)
                EntidadDesktopComboBox.SelectedIndex = 0

            Catch
                Throw
            Finally
                If dmCore IsNot Nothing Then dmCore.Connection_Close()
                llenarProyectos()
            End Try
        End Sub

        Public Sub llenarProyectos()
            Dim dmCore As DBCore.DBCoreDataBaseManager = Nothing

            Try
                dmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                dmCore.Connection_Open(Program.Sesion.Usuario.id)
                Dim Proyecto = dmCore.SchemaConfig.TBL_Proyecto.DBGet(CShort(EntidadDesktopComboBox.SelectedValue), Nothing)
                ProyectoComboBox.Fill(Proyecto, Proyecto.id_ProyectoColumn, Proyecto.Nombre_ProyectoColumn)
                ProyectoComboBox.SelectedIndex = 0

            Catch
                Throw
            Finally
                If dmCore IsNot Nothing Then dmCore.Connection_Close()
            End Try
        End Sub

#End Region

    End Class

End Namespace