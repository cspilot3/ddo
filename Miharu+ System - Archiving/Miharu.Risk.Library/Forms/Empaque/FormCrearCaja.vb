Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports DBArchiving
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Config
Imports Slyg.Tools

Namespace Forms.Empaque

    Public Class FormCrearCaja
        Inherits FormBase

#Region " Metodos "

        Function valida() As Boolean
            Dim validacion As Boolean = True

            Dim dbmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

            Dim UltimoCbarrasCaja = dbmArchiving.SchemaConfig.Tbl_Secuencia.DBGet(DesktopConfig.Consecutivo.Cajas)(0).Secuencia

            If CBarrasDesktopTextBox.Text = "" Then
                DesktopMessageBoxControl.DesktopMessageShow("Debe digitar un codigo de barras", "Codigo de barras vacio", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                validacion = False
            End If

            If CBarrasDesktopTextBox.Text.Substring(0, 3) <> "999" Then
                DesktopMessageBoxControl.DesktopMessageShow("El codigo de barras debe iniciar en '999' para las cajas.", "Error imprimiendo CBarras", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                validacion = False
            End If

            If CInt(CBarrasDesktopTextBox.Text.Substring(4)) > UltimoCbarrasCaja Then
                DesktopMessageBoxControl.DesktopMessageShow("El codigo de barras no puede ser mayor al consecutivo de base de datos.", "Error imprimiendo CBarras", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                validacion = False
            End If

            dbmArchiving.Connection_Close()

            Return validacion
        End Function

        Private Sub AgregarCaja()
            If valida() Then

                Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                Dim Caja = dbmCore.SchemaCustody.TBL_Caja.DBFindByCodigo_Caja(CBarrasDesktopTextBox.Text)

                If Caja.Count = 0 Then
                    Try
                        dbmCore.Transaction_Begin()

                        Dim Registro As New DBCore.SchemaCustody.TBL_CajaType
                        Registro.Codigo_Caja = CBarrasDesktopTextBox.Text
                        Registro.Es_Proceso = False
                        Registro.fk_Caja_Tipo = CShort(TipoCajaDesktopComboBox.SelectedValue)
                        Registro.fk_Entidad = Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad
                        Registro.fk_Entidad_Cliente = Program.RiskGlobal.Entidad
                        Registro.fk_Estado = DBCore.EstadoEnum.Empaque
                        Registro.fk_Proyecto_Cliente = Program.RiskGlobal.Proyecto
                        Registro.fk_Sede = Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede
                        Registro.id_Caja = dbmCore.SchemaCustody.TBL_Caja.DBNextId()
                        Registro.Fecha_Creacion = SlygNullable.SysDate

                        dbmCore.SchemaCustody.TBL_Caja.DBInsert(Registro)
                        dbmCore.Transaction_Commit()
                        DesktopMessageBoxControl.DesktopMessageShow("Datos Guardados con exito", "Caja OK", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                        LimpiarFormulario()

                    Catch ex As Exception
                        dbmCore.Transaction_Rollback()
                        CBarrasDesktopTextBox.Focus()
                        DesktopMessageBoxControl.DesktopMessageShow("AgregarCaja", ex)
                    End Try
                Else
                    DesktopMessageBoxControl.DesktopMessageShow("La caja que intenta crear ya existe con el codigo de barras digitado, por favor pruebe otro", "Caja existente", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                End If

                dbmCore.Connection_Close()
            End If

            CBarrasDesktopTextBox.Focus()
            CBarrasDesktopTextBox.SelectAll()

        End Sub

        Private Sub CargaCombos()
            Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
            dbmCore.Connection_Open(Program.Sesion.Usuario.id)

            Dim CajaTipo = dbmCore.SchemaCustody.TBL_Caja_Tipo.DBGet(Nothing)
            Utilities.LlenarCombo(TipoCajaDesktopComboBox, CajaTipo, CajaTipo.id_Caja_TipoColumn.ColumnName, CajaTipo.Nombre_Caja_TipoColumn.ColumnName)

            TipoCajaDesktopComboBox.SelectedValue = Program.RiskGlobal.CajaXDefecto

            dbmCore.Connection_Close()
        End Sub

        Public Sub LimpiarFormulario()
            TipoCajaDesktopComboBox.SelectedIndex = -1
            CBarrasDesktopTextBox.Text = ""
            CBarrasDesktopTextBox.Focus()
        End Sub

#End Region

#Region " Eventos "

        Private Sub CancelarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CancelarButton.Click
            Me.Close()
        End Sub

        Private Sub AceptarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AceptarButton.Click
            AgregarCaja()
            TipoCajaDesktopComboBox.SelectedValue = Program.RiskGlobal.CajaXDefecto
        End Sub

        Private Sub FormCrearCaja_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            CargaCombos()
        End Sub

#End Region

    End Class

End Namespace