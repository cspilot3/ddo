Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Controls
Imports DBArchiving
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Config

Namespace Forms.Parametrizacion.Solicitudes

    Public Class Formtipoxrol
        Inherits FormBase

#Region " Eventos "

        Private Sub FormPrioridadXRol_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            LlenarCombos()
        End Sub

        Private Sub CerrarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CerrarButton.Click
            Me.Close()
        End Sub

        Private Sub guardarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles guardarButton.Click
            Guardar()
        End Sub

        Private Sub EntidadDesktopComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles EntidadDesktopComboBox.SelectedIndexChanged
            LlenarProyectos()
            LlenarDocumentos()
        End Sub

        Private Sub ProyectoDesktopComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles ProyectoDesktopComboBox.SelectedIndexChanged
            LlenarEsquemas()
            LlenarDocumentos()
        End Sub

        Private Sub rolesDesktopComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles rolesDesktopComboBox.SelectedIndexChanged
            LlenarDocumentos()
        End Sub

        Private Sub TipoDesktopComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles TipoDesktopComboBox.SelectedIndexChanged
            LlenarDocumentos()
        End Sub

        Private Sub EsquemaDesktopComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles EsquemaDesktopComboBox.SelectedIndexChanged
            LlenarDocumentos()
        End Sub

#End Region

#Region " Metodos "

        Public Sub LlenarCombos()
            Dim dmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dmArchiving.Connection_Open(Program.Sesion.Usuario.id)
            Dim Roles = dmArchiving.SchemaSecurity.CTA_Rol.DBGet()
            Dim Entidades = dmArchiving.SchemaSecurity.CTA_Entidad.DBGet()
            Dim tipos = dmArchiving.Schemadbo.CTA_solicutd_Tipo_Descripcion.DBGet()
            Utilities.LlenarCombo(rolesDesktopComboBox, Roles, Roles.id_RolColumn.ColumnName, Roles.Nombre_RolColumn.ColumnName)
            Utilities.LlenarCombo(EntidadDesktopComboBox, Entidades, Entidades.id_EntidadColumn.ColumnName, Entidades.Nombre_EntidadColumn.ColumnName)
            Utilities.LlenarCombo(TipoDesktopComboBox, tipos, tipos.id_Solicitud_TipoColumn.ColumnName, tipos.NombreColumn.ColumnName)
            dmArchiving.Connection_Close()
        End Sub

        Public Sub LlenarProyectos()
            Dim dmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dmArchiving.Connection_Open(Program.Sesion.Usuario.id)
            Dim Proyectos = dmArchiving.Schemadbo.CTA_Proyecto.DBFindByfk_Entidad(CShort(EntidadDesktopComboBox.SelectedValue))
            Utilities.LlenarCombo(ProyectoDesktopComboBox, Proyectos, Proyectos.id_ProyectoColumn.ColumnName, Proyectos.Nombre_ProyectoColumn.ColumnName)
            dmArchiving.Connection_Close()
        End Sub

        Public Sub LlenarEsquemas()
            Dim dmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dmArchiving.Connection_Open(Program.Sesion.Usuario.id)
            Dim Esquemas = dmArchiving.Schemadbo.CTA_Esquema.DBFindByfk_entidadfk_proyecto(CShort(EntidadDesktopComboBox.SelectedValue), CShort(ProyectoDesktopComboBox.SelectedValue))
            Utilities.LlenarCombo(EsquemaDesktopComboBox, Esquemas, Esquemas.fk_esquemaColumn.ColumnName, Esquemas.Nombre_esquemaColumn.ColumnName)
            dmArchiving.Connection_Close()
        End Sub

        Public Sub LlenarDocumentos()
            Dim dmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dmArchiving.Connection_Open(Program.Sesion.Usuario.id)
            Dim Documentos = dmArchiving.Schemadbo.PA_Solicitud_Tipo_Rol.DBExecute(CShort(rolesDesktopComboBox.SelectedValue), _
                                                                                   CShort(EntidadDesktopComboBox.SelectedValue), _
                                                                                   CShort(ProyectoDesktopComboBox.SelectedValue), _
                                                                                   CShort(EsquemaDesktopComboBox.SelectedValue), _
                                                                                   CByte(TipoDesktopComboBox.SelectedValue))
            DocumentosDesktopDataGridView.DataSource = Documentos
            dmArchiving.Connection_Close()
        End Sub

        Public Sub Guardar()
            Dim dmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dmArchiving.Connection_Open(Program.Sesion.Usuario.id)
            Dim id_Rol As Integer = CInt(rolesDesktopComboBox.SelectedValue)
            Dim fk_Solicitud_Tipo As Byte = CByte(TipoDesktopComboBox.SelectedValue)

            Try
                dmArchiving.Transaction_Begin()

                'dmArchiving.SchemaCustody.TBL_Solicitud_Tipo_x_Rol.DBDelete(id_Rol, fk_Solicitud_Tipo, Nothing)
                'Elimina Los documentos asociados antes de asignar los nuevos.
                For Each RowData As DataGridViewRow In DocumentosDesktopDataGridView.Rows
                    dmArchiving.SchemaCustody.TBL_Solicitud_Tipo_x_Rol.DBDelete(id_Rol, fk_Solicitud_Tipo, CInt(RowData.Cells("ID").Value))
                Next

                For Each RowData As DataGridViewRow In DocumentosDesktopDataGridView.Rows
                    If Not IsDBNull(RowData.Cells("Aplica").Value) Then
                        If CBool(RowData.Cells("Aplica").Value) Then
                            Dim id_Documento As Integer = CInt(RowData.Cells("ID").Value)
                            dmArchiving.SchemaCustody.TBL_Solicitud_Tipo_x_Rol.DBInsert(id_Rol, fk_Solicitud_Tipo, id_Documento)
                        End If
                    End If
                Next

                dmArchiving.Transaction_Commit()
                DesktopMessageBoxControl.DesktopMessageShow("Los datos se han guardado con éxito.", "Documentos por Rol", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
            Catch ex As Exception
                dmArchiving.Transaction_Rollback()
                DesktopMessageBoxControl.DesktopMessageShow("Guardar", ex)
            End Try

            dmArchiving.Connection_Close()
        End Sub

#End Region

    End Class

End Namespace