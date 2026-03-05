Imports System.Text
Imports System.Windows.Forms
Imports DBArchiving
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library.Config

Namespace Forms.Facturacion

    Public Class FormHomologacionFacturacion
        Inherits Miharu.Desktop.Library.FormBase

#Region " Eventos "

        Private Sub BuscarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BuscarButton.Click
            CargaServicios()
        End Sub

        Private Sub FormHomologacionFacturacion_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            CargaEntidad()
        End Sub

        Private Sub EntidadDesktopComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles EntidadDesktopComboBox.SelectedIndexChanged
            CargaProyecto()
            CargaEntidadFacturacion()
        End Sub

        Private Sub ProyectoDesktopComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles ProyectoDesktopComboBox.SelectedIndexChanged
            CargaEsquema()
        End Sub

        Private Sub CerrarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CerrarButton.Click
            Me.Close()
        End Sub

        Private Sub EntidadFacturacionDesktopComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles EntidadFacturacionDesktopComboBox.SelectedIndexChanged
            CargaEsquema()
        End Sub

        Private Sub GuardarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles GuardarButton.Click
            GuardarCambios()
        End Sub

        Private Sub CancelarEdicionButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CancelarEdicionButton.Click
            HabilitaControles(True)
            CargaServicios()
        End Sub

        Private Sub FacturacionDesktopDataGridView_DataError(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles FacturacionDesktopDataGridView.DataError
            DesktopMessageBoxControl.DesktopMessageShow("El valor debe ser numerico", "Error en valor", Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
        End Sub

        Private Sub EditarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles EditarButton.Click
            HabilitaControles(False)
        End Sub

#End Region

#Region " Funciones "

        Public Sub HabilitaControles(ByVal Habilita As Boolean)
            FacturacionDesktopDataGridView.ReadOnly = Habilita
            BuscarButton.Enabled = Habilita
            EntidadDesktopComboBox.Enabled = Habilita
            ProyectoDesktopComboBox.Enabled = Habilita
            EsquemaDesktopComboBox.Enabled = Habilita
            EntidadFacturacionDesktopComboBox.Enabled = Habilita
            EsquemaFacturacionDesktopComboBox.Enabled = Habilita
        End Sub

#End Region

#Region " Metodos "

        Public Sub CargaServicios()
            Dim dmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dmArchiving.Connection_Open(Program.Sesion.Usuario.id)
            Dim EsquemasFacturacion = dmArchiving.Schemadbo.CTA_Facturacion_Homologacion_Codigos.DBFindByfk_Entidadfk_Proyectoid_Esquemafk_Entidad_Facturacionid_Esquema_Facturacion(CShort(EntidadDesktopComboBox.SelectedValue), CShort(ProyectoDesktopComboBox.SelectedValue), CShort(EsquemaDesktopComboBox.SelectedValue), CShort(EntidadFacturacionDesktopComboBox.SelectedValue), CShort(EsquemaFacturacionDesktopComboBox.SelectedValue))
            dmArchiving.Connection_Close()

            FacturacionDesktopDataGridView.AutoGenerateColumns = False
            FacturacionDesktopDataGridView.DataSource = EsquemasFacturacion
        End Sub

        Public Sub CargaEntidad()
            Dim dmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dmArchiving.Connection_Open(Program.Sesion.Usuario.id)
            Dim Entidad = dmArchiving.SchemaSecurity.CTA_Entidad.DBGet()
            dmArchiving.Connection_Close()

            Utilities.LlenarCombo(EntidadDesktopComboBox, Entidad, Entidad.id_EntidadColumn.ColumnName, Entidad.Nombre_EntidadColumn.ColumnName)
            CargaProyecto()
        End Sub

        Public Sub CargaProyecto()
            Dim dmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dmArchiving.Connection_Open(Program.Sesion.Usuario.id)
            Dim Proyecto = dmArchiving.Schemadbo.CTA_Proyecto.DBFindByfk_Entidad(CShort(EntidadDesktopComboBox.SelectedValue))
            dmArchiving.Connection_Close()

            Utilities.LlenarCombo(ProyectoDesktopComboBox, Proyecto, Proyecto.id_ProyectoColumn.ColumnName, Proyecto.Nombre_ProyectoColumn.ColumnName)
            CargaEsquema()
        End Sub

        Public Sub CargaEsquema()
            Dim dmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dmArchiving.Connection_Open(Program.Sesion.Usuario.id)
            Dim Esquema = dmArchiving.Schemadbo.CTA_Esquema.DBFindByfk_entidadfk_proyecto(CShort(EntidadDesktopComboBox.SelectedValue), CShort(ProyectoDesktopComboBox.SelectedValue))
            dmArchiving.Connection_Close()

            Utilities.LlenarCombo(EsquemaDesktopComboBox, Esquema, Esquema.fk_esquemaColumn.ColumnName, Esquema.Nombre_esquemaColumn.ColumnName)
        End Sub

        Public Sub CargaEntidadFacturacion()
            Dim dmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dmArchiving.Connection_Open(Program.Sesion.Usuario.id)
            Dim Esquema = dmArchiving.Schemadbo.CTA_Facturacion_Nombre_Entidad_Esquema.DBFindByfk_Entidad_Cliente(CShort(EntidadDesktopComboBox.SelectedValue))
            dmArchiving.Connection_Close()

            Utilities.LlenarCombo(EntidadFacturacionDesktopComboBox, Esquema, Esquema.fk_EntidadColumn.ColumnName, Esquema.Nombre_EntidadColumn.ColumnName)
            CargaEsquemaFacturacion()
        End Sub

        Public Sub CargaEsquemaFacturacion()
            Dim dmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dmArchiving.Connection_Open(Program.Sesion.Usuario.id)
            Dim Esquema = dmArchiving.SchemaBill.TBL_Esquema.DBFindByfk_Entidad(CShort(EntidadFacturacionDesktopComboBox.SelectedValue))
            dmArchiving.Connection_Close()

            Utilities.LlenarCombo(EsquemaFacturacionDesktopComboBox, Esquema, Esquema.id_EsquemaColumn.ColumnName, Esquema.Nombre_EsquemaColumn.ColumnName)
        End Sub

        Public Sub GuardarCambios()
            Dim Entidad = CShort(EntidadDesktopComboBox.SelectedValue)
            Dim Proyecto = CShort(ProyectoDesktopComboBox.SelectedValue)
            Dim Esquema = CShort(EsquemaDesktopComboBox.SelectedValue)
            Dim EntidadFacturacion = CShort(EntidadFacturacionDesktopComboBox.SelectedValue)
            Dim EsquemaFacturacion = CShort(EsquemaFacturacionDesktopComboBox.SelectedValue)
            Dim CadenaError As New StringBuilder
            Dim validacion As Boolean = True

            Dim dmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dmArchiving.Connection_Open(Program.Sesion.Usuario.id)

            Try
                dmArchiving.Transaction_Begin()

                dmArchiving.SchemaBill.TBL_Homologacion.DBDelete(Entidad, Proyecto, Esquema, EntidadFacturacion, EsquemaFacturacion, Nothing)
                Dim HomologacionType As New DBArchiving.SchemaBill.TBL_HomologacionType
                HomologacionType.fk_Entidad = Utilities.DShort(Entidad)
                HomologacionType.fk_Entidad_Facturacion = Utilities.DShort(EntidadFacturacion)
                HomologacionType.fk_Esquema = Utilities.DShort(Esquema)
                HomologacionType.fk_Esquema_Facturacion = Utilities.DShort(EsquemaFacturacion)
                HomologacionType.fk_Proyecto = Utilities.DShort(Proyecto)

                For Each Row As DataGridViewRow In FacturacionDesktopDataGridView.Rows
                    Dim id_Servicio = Row.Cells("id_Servicio").Value.ToString()
                    Dim Nombre_Servicio = Row.Cells("Servicio").Value.ToString()
                    Dim Clasificacion = Row.Cells("Clasificacion").Value.ToString()
                    Dim Cod_Cuenta = Row.Cells("Cod_Cuenta").Value.ToString()
                    Dim Genero = Row.Cells("Genero").Value.ToString()
                    Dim Grupo = Row.Cells("Grupo").Value.ToString()
                    Dim Producto = Row.Cells("Producto").Value.ToString()
                    Dim Producto_Detalle = Row.Cells("Producto_Detalle").Value.ToString()
                    Dim Producto_Especifico = Row.Cells("Producto_Especifico").Value.ToString()


                    If Not Clasificacion = "" _
                       And Not Cod_Cuenta = "" _
                       And Not Genero = "" _
                       And Not Grupo = "" _
                       And Not Producto = "" _
                       And Not Producto_Detalle = "" _
                       And Not Producto_Especifico = "" Then

                        HomologacionType.fk_Servicio = CInt(id_Servicio)
                        HomologacionType.Clasificacion = CInt(Clasificacion)
                        HomologacionType.Cod_Cuenta = CInt(Cod_Cuenta)
                        HomologacionType.Genero = CInt(Genero)
                        HomologacionType.Grupo = CInt(Grupo)
                        HomologacionType.Producto = CInt(Producto)
                        HomologacionType.Producto_Detalle = CInt(Producto_Detalle)
                        HomologacionType.Producto_Especifico = CInt(Producto_Especifico)

                        Dim validacionCodigos As Boolean = dmArchiving.Schemadbo.PA_Facturacion_Valida_Codigo.DBExecute(HomologacionType.Clasificacion, _
                                                                                                                        HomologacionType.Cod_Cuenta, _
                                                                                                                        HomologacionType.Genero, _
                                                                                                                        HomologacionType.Grupo, _
                                                                                                                        HomologacionType.Producto, _
                                                                                                                        HomologacionType.Producto_Detalle, _
                                                                                                                        HomologacionType.Producto_Especifico)
                        If validacionCodigos Then
                            dmArchiving.SchemaBill.TBL_Homologacion.DBInsert(HomologacionType)
                        Else
                            CadenaError.AppendLine("Los datos del Servicio [" & Nombre_Servicio & "] no coinciden con ningún registro en BRINKS.")
                            validacion = False
                        End If
                    Else
                        If Not Clasificacion = "" Or _
                           Not Cod_Cuenta = "" Or _
                           Not Genero = "" Or _
                           Not Grupo = "" Or _
                           Not Producto = "" Or _
                           Not Producto_Detalle = "" Or _
                           Not Producto_Especifico = "" Then

                            CadenaError.AppendLine("El Servicio [" & Nombre_Servicio & "] tiene los parametros incompletos por lo que no sera guardado")
                            validacion = False
                        End If
                    End If

                Next

                If validacion Then
                    DesktopMessageBoxControl.DesktopMessageShow("Codigos de homologación guardados con exito", "Codigos OK", Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                    HabilitaControles(True)
                    dmArchiving.Transaction_Commit()
                Else
                    DesktopMessageBoxControl.DesktopMessageShow(CadenaError.ToString, "Error guardando todos los codigos", Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                    dmArchiving.Transaction_Rollback()
                End If

            Catch ex As Exception
                dmArchiving.Transaction_Rollback()
                DesktopMessageBoxControl.DesktopMessageShow("GuardarCambios", ex)
            End Try

            dmArchiving.Connection_Close()
        End Sub

#End Region

    End Class
End Namespace