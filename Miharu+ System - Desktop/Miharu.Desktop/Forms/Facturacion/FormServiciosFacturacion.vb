Imports System.Text
Imports DBArchiving
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library.Config

Namespace Forms.Facturacion

    Public Class FormServiciosFacturacion
        Inherits Miharu.Desktop.Library.FormBase

#Region " Metodos "

        Public Sub llenarCombos()
            Dim dmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dmArchiving.Connection_Open(Program.Sesion.Usuario.id)
            Dim Servicios = dmArchiving.SchemaBill.TBL_Servicio.DBGet(Nothing)
            dmArchiving.Connection_Close()

            Utilities.LlenarCombo(ServicioConsultaDesktopComboBox, Servicios, Servicios.id_ServicioColumn.ColumnName, Servicios.Nombre_ServicioColumn.ColumnName, False, Servicios.Nombre_ServicioColumn.ColumnName)
            Utilities.LlenarCombo(fk_Servicio_Padre_DesktopComboBox, Servicios, Servicios.id_ServicioColumn.ColumnName, Servicios.Nombre_ServicioColumn.ColumnName, True, Servicios.Nombre_ServicioColumn.ColumnName)
        End Sub

        Public Sub GuardarCambios()
            If Validaciones() Then
                Dim dmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
                dmArchiving.Connection_Open(Program.Sesion.Usuario.id)

                Try
                    Dim ServicioType As New SchemaBill.TBL_ServicioType
                    ServicioType.Codigo_Servicio = Codigo_ServicioDesktopTextBox.Text
                    ServicioType.Descripcion_Servicio = Descripcion_ServicioDesktopTextBox.Text
                    ServicioType.fk_Servicio_Padre = Utilities.DShort(fk_Servicio_Padre_DesktopComboBox.SelectedValue)
                    ServicioType.Nombre_Servicio = Nombre_ServicioDesktopTextBox.Text

                    dmArchiving.Transaction_Begin()
                    If id_ServicioDesktopTextBox.Text = "" Then
                        ServicioType.id_Servicio = dmArchiving.SchemaBill.TBL_Servicio.DBNextId
                        dmArchiving.SchemaBill.TBL_Servicio.DBInsert(ServicioType)
                        id_ServicioDesktopTextBox.Text = CStr(ServicioType.id_Servicio)
                    Else
                        ServicioType.id_Servicio = CShort(id_ServicioDesktopTextBox.Text)
                        dmArchiving.SchemaBill.TBL_Servicio.DBUpdate(ServicioType, ServicioType.id_Servicio)
                    End If
                    dmArchiving.Transaction_Commit()

                    DesktopMessageBoxControl.DesktopMessageShow("Se ha guardado el servicio satisfactoriamente", "Servicio OK", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                Catch ex As Exception
                    dmArchiving.Transaction_Rollback()
                    DesktopMessageBoxControl.DesktopMessageShow("GuardarCambios", ex)
                Finally
                    dmArchiving.Connection_Close()
                End Try

                llenarCombos()
            End If
        End Sub

#End Region

#Region " Eventos "

        Private Sub ServicioConsultaDesktopComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles ServicioConsultaDesktopComboBox.SelectedIndexChanged
            Consultar()
        End Sub

        Private Sub FormServiciosFacturacion_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            llenarCombos()
        End Sub

        Private Sub CerrarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CerrarButton.Click
            Me.Close()
        End Sub

        Private Sub GuardarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles GuardarButton.Click
            GuardarCambios()
        End Sub

#End Region

#Region " Funciones "

        Private Sub Consultar()
            Dim dmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dmArchiving.Connection_Open(Program.Sesion.Usuario.id)
            Dim Servicios = dmArchiving.SchemaBill.TBL_Servicio.DBGet(CShort(ServicioConsultaDesktopComboBox.SelectedValue))
            dmArchiving.Connection_Close()

            Try
                If Servicios(0).Isfk_Servicio_PadreNull Then
                    fk_Servicio_Padre_DesktopComboBox.SelectedValue = "-1"
                Else
                    fk_Servicio_Padre_DesktopComboBox.SelectedValue = Servicios(0).fk_Servicio_Padre
                End If

                id_ServicioDesktopTextBox.Text = CStr(Servicios(0).id_Servicio)
                Nombre_ServicioDesktopTextBox.Text = Servicios(0).Nombre_Servicio
                Descripcion_ServicioDesktopTextBox.Text = Servicios(0).Descripcion_Servicio
                Codigo_ServicioDesktopTextBox.Text = Servicios(0).Codigo_Servicio
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("ConsultarButton_Click", ex)
            End Try
        End Sub

        Public Function Validaciones() As Boolean
            Dim valida As Boolean = True
            Dim cadena As New StringBuilder

            If Nombre_ServicioDesktopTextBox.Text = "" Then
                valida = False
                cadena.AppendLine("El Nombre del servicio es obligatorio.")
            End If

            If Descripcion_ServicioDesktopTextBox.Text = "" Then
                valida = False
                cadena.AppendLine("La descripción del servicio es obligatoria.")
            End If

            If Codigo_ServicioDesktopTextBox.Text = "" Then
                valida = False
                cadena.AppendLine("El codigo del servicio es obligatorio.")
            End If

            If valida = False Then
                DesktopMessageBoxControl.DesktopMessageShow(cadena.ToString(), "Error guardando servicio", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
            End If

            Return valida
        End Function

#End Region

    End Class
End Namespace