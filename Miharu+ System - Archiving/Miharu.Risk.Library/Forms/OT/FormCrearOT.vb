Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library.Config
Imports DBArchiving
Imports Miharu.Desktop.Library
Imports Slyg.Tools

Namespace Forms.OT

    Public Class FormCrearOT
        Inherits FormBase

#Region " Declaraciones "
        Private _Cerrar As Short
#End Region

#Region " Metodos "

        Public Sub llenarCombos()
            Dim dbmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

            Dim EsquemasFacturacion = dbmArchiving.Schemadbo.CTA_Esquema_x_Facturacion.DBFindByfk_Entidad_Cliente(Program.RiskGlobal.Entidad)
            Utilities.LlenarCombo(EsquemaComboBox, EsquemasFacturacion, EsquemasFacturacion.IDColumn.ColumnName, EsquemasFacturacion.ValorColumn.ColumnName)

            dbmArchiving.Connection_Close()
        End Sub

        Public Sub GuardarOT()
            If IsNothing(EsquemaComboBox.SelectedValue) Then
                DesktopMessageBoxControl.DesktopMessageShow("Debe selecciona un esquema de facturacion para crear la OT", "Error creando OT", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
            Else

                Dim EntidadFacturacion As Short = CShort(Split(EsquemaComboBox.SelectedValue.ToString(), "-")(0))
                Dim EsquemaFacturacion As Short = CShort(Split(EsquemaComboBox.SelectedValue.ToString(), "-")(1))

                Dim dbmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)

                Try
                    dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)
                    dbmArchiving.Transaction_Begin()

                    Dim NextOt As New SchemaRisk.TBL_OTType
                    NextOt.Fecha_OT = SlygNullable.SysDate
                    NextOt.fk_Cargue = Nothing
                    NextOt.fk_Entidad = Program.RiskGlobal.Entidad
                    If Program.RiskGlobal.ProyectoRow.usa_cargue_parcial Then
                        NextOt.fk_Estado = DBCore.EstadoEnum.Cargado
                    Else
                        NextOt.fk_Estado = DBCore.EstadoEnum.Creado
                    End If
                    NextOt.fk_Proyecto = Program.RiskGlobal.Proyecto
                    NextOt.fk_Usuario = Program.Sesion.Usuario.id
                    NextOt.id_OT = dbmArchiving.SchemaRisk.TBL_OT.DBNextId
                    NextOt.fk_Entidad_Procesamiento = EntidadFacturacion
                    NextOt.fk_Esquema_Facturacion = EsquemaFacturacion
                    NextOt.fk_Sede_Procesamiento = Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede
                    NextOt.Fecha_Proceso = FechaOperacionDateTimePicker.Value

                    dbmArchiving.SchemaRisk.TBL_OT.DBInsert(NextOt)
                    dbmArchiving.Transaction_Commit()
                    DesktopMessageBoxControl.DesktopMessageShow("Se ha generado la Ot   ( " & CStr(NextOt.id_OT) & " ).", "OT Generada", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)

                Catch ex As Exception
                    dbmArchiving.Transaction_Rollback()
                    DesktopMessageBoxControl.DesktopMessageShow("GenerarOT", ex)
                Finally
                    dbmArchiving.Connection_Close()
                End Try

            End If
        End Sub

        Public Function ValidarFechaProcesamiento() As Boolean
            If FechaOperacionDateTimePicker.Value > Now Then
                Return False
            Else
                Return True
            End If

        End Function

        Public Sub MostrarMensajeValidacion(ByVal mensaje As String)
            If DesktopMessageBoxControl.DesktopMessageShow(mensaje, "GenerarOT", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True) = MsgBoxResult.Ok Then
                FechaOperacionDateTimePicker.Focus()
            End If
        End Sub

#End Region

#Region " Eventos "

        Private Sub CerrarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CerrarButton.Click
            Me.Close()
        End Sub

        Private Sub FormCrearOT_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            llenarCombos()
        End Sub

        Private Sub FormCrearOT_FormClosing(ByVal sender As System.Object, ByVal e As Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
            If (_Cerrar = 0) Or (_Cerrar = -1) Then
                e.Cancel = False
            Else
                e.Cancel = True
            End If
        End Sub

        Private Sub AceptarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AceptarButton.Click
            ' Validar que la fecha de operación no sea mayor a la actual
            If Not ValidarFechaProcesamiento() Then
                MostrarMensajeValidacion("La Fecha de Operación no puede ser mayor a la fecha actual")
                _Cerrar = 1
            Else
                GuardarOT()
                _Cerrar = 0
            End If
        End Sub

#End Region

    End Class

End Namespace