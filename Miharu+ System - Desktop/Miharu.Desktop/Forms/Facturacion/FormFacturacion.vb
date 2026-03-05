Imports System.Windows.Forms
Imports DBArchiving
Imports DBArchiving.SchemaBill
Imports DBArchiving.Schemadbo
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Slyg.Tools

Namespace Forms.Facturacion

    Public Class FormFacturacion
        Inherits Library.FormBase

#Region " Eventos "
        Private Sub FormFacturacion_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            CargarFiltros()
            CargaFacturacion()
        End Sub

        Private Sub CerrarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CerrarButton.Click
            Me.Close()
        End Sub

        Private Sub GenerarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles GenerarButton.Click
            If DesktopMessageBoxControl.DesktopMessageShow("¿Está seguro que desea generar la facturación para el rango de fecha [" & FechaInicialDateTimePicker.Value.ToShortDateString() & "] - [" & FechaFinalDateTimePicker.Value.ToShortDateString() & "]?", "Generar Facturación", DesktopMessageBoxControl.IconEnum.WarningIcon, False) = DialogResult.OK Then
                If ValidaGeneracion() Then
                    GeneraFacturacion()
                End If
            End If
            CargaFacturacion()
        End Sub

        Private Sub EditarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles EditarButton.Click
            EditarFacturacion()
        End Sub

        Private Sub EditarToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles EditarToolStripMenuItem.Click
            EditarFacturacion()
        End Sub

        Private Sub ReporteButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ReporteButton.Click
            ReporteFacturacion()
        End Sub

        Private Sub TransmitirButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles TransmitirButton.Click
            If DesktopMessageBoxControl.DesktopMessageShow("¿Esta seguro que desea transmitir la facturación?", "Transmisión de Facturación", DesktopMessageBoxControl.IconEnum.WarningIcon) = DialogResult.OK Then
                EjecutarTransmision()
            End If
        End Sub

        Private Sub ReporteFacturaciónToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ReporteFacturaciónToolStripMenuItem.Click
            ReporteFacturacion()
        End Sub

        Private Sub FacturacionDataGridView_CellMouseDown(ByVal sender As System.Object, ByVal e As DataGridViewCellMouseEventArgs) Handles FacturacionDataGridView.CellMouseDown
            If e.Button = MouseButtons.Right Then
                FacturacionDataGridView.Rows(e.RowIndex).Selected = True
                FacturacionDataGridView.ContextMenuStrip = ContextMenuStrip
            End If
        End Sub

        Private Sub FacturacionDataGridView_CellDoubleClick(ByVal sender As System.Object, ByVal e As DataGridViewCellEventArgs) Handles FacturacionDataGridView.CellDoubleClick
            EditarFacturacion()
        End Sub
#End Region

#Region " Metodos "
        Private Sub CargarFiltros()
            Try
                'Fechas
                FechaInicialDateTimePicker.Value = Now.AddMonths(-1)
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargarFiltros", ex)
            End Try
        End Sub

        Private Sub CargaFacturacion()
            Dim dmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            Try
                dmArchiving.Connection_Open(Program.Sesion.Usuario.id)

                Dim enumFacturacion As New CTA_FacturacionEnumList
                enumFacturacion.Add(CTA_FacturacionEnum.Fecha_Facturacion, True)

                Dim dtFacturacion = dmArchiving.Schemadbo.CTA_Facturacion.DBGet(0, enumFacturacion)

                FacturacionDataGridView.AutoGenerateColumns = False
                FacturacionDataGridView.DataSource = dtFacturacion
                FacturacionDataGridView.ClearSelection()

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaFacturacion", ex)
            Finally
                dmArchiving.Connection_Close()
            End Try
        End Sub

        Private Sub GeneraFacturacion()
            Dim dmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            Try
                dmArchiving.Connection_Open(Program.Sesion.Usuario.id)

                'Se obtienen los movimientos para el rango de fechas seleccionados
                Dim Inicial As New Date(FechaInicialDateTimePicker.Value.Year, FechaInicialDateTimePicker.Value.Month, FechaInicialDateTimePicker.Value.Day, 0, 0, 0)
                Dim Final As New Date(FechaFinalDateTimePicker.Value.Year, FechaFinalDateTimePicker.Value.Month, FechaFinalDateTimePicker.Value.Day, 23, 59, 59)

                Dim dtMovimientos = dmArchiving.Schemadbo.PA_Obtiene_Movimientos_Rango.DBExecute(Inicial, Final)
                If dtMovimientos.Count > 0 Then
                    dmArchiving.Transaction_Begin()

                    'Genera la facturación
                    Dim tFacturacion As New TBL_FacturacionType
                    tFacturacion.fk_Entidad = Program.Sesion.Entidad.id
                    tFacturacion.id_Facturacion = dmArchiving.SchemaBill.TBL_Facturacion.DBNextId_for_id_Facturacion(Program.Sesion.Entidad.id)
                    tFacturacion.Fecha_Facturacion = SlygNullable.SysDate
                    tFacturacion.fk_Usuario_Log = Program.Sesion.Usuario.id
                    dmArchiving.SchemaBill.TBL_Facturacion.DBInsert(tFacturacion)

                    'Actualiza los movimientos seleccionados a Facturados.
                    dmArchiving.Schemadbo.PA_Actualiza_Movimientos_Rango.DBExecute(Inicial, Final, tFacturacion.id_Facturacion)

                    'Genera el detalle de la factura
                    For Each movimiento In dtMovimientos
                        Dim tFacturacionDetalle As New TBL_Facturacion_DetalleType
                        tFacturacionDetalle.fk_Entidad = Program.Sesion.Entidad.id
                        tFacturacionDetalle.fk_Facturacion = tFacturacion.id_Facturacion
                        tFacturacionDetalle.id_Facturacion_Detalle = dmArchiving.SchemaBill.TBL_Facturacion_Detalle.DBNextId(Program.Sesion.Entidad.id, tFacturacion.id_Facturacion)
                        tFacturacionDetalle.fk_Sede = movimiento.fk_Sede
                        tFacturacionDetalle.fk_Centro_Procesamiento = movimiento.fk_Centro_Procesamiento
                        tFacturacionDetalle.fk_Esquema = movimiento.fk_Esquema
                        tFacturacionDetalle.fk_Servicio = movimiento.fk_Servicio
                        tFacturacionDetalle.fk_Entidad_Cliente = movimiento.fk_Entidad_Cliente
                        tFacturacionDetalle.fk_Proyecto_Cliente = movimiento.fk_Proyecto_Cliente
                        tFacturacionDetalle.fk_Esquema_Cliente = movimiento.fk_Esquema_Cliente
                        tFacturacionDetalle.Fecha_Movimiento = movimiento.Fecha_Movimiento
                        tFacturacionDetalle.Cantidad_Movimiento = movimiento.Cantidad_Movimiento
                        tFacturacionDetalle.fk_Usuario_Log = movimiento.fk_Usuario_Log
                        tFacturacionDetalle.Tipo_Registro = "I"
                        tFacturacionDetalle.Transmitido = False
                        dmArchiving.SchemaBill.TBL_Facturacion_Detalle.DBInsert(tFacturacionDetalle)
                    Next

                    dmArchiving.Transaction_Commit()

                    DesktopMessageBoxControl.DesktopMessageShow("Se generó la facturación para el rango de fechas satisfactoriamente.", "Facturación Creada", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                Else
                    DesktopMessageBoxControl.DesktopMessageShow("No existen movimientos para este rango de fechas.", "No existe Movimientos", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                End If
            Catch ex As Exception
                dmArchiving.Transaction_Rollback()
                DesktopMessageBoxControl.DesktopMessageShow("GeneraFacturacion", ex)
            Finally
                dmArchiving.Connection_Close()
            End Try
        End Sub

        Private Sub EditarFacturacion()
            Try
                Dim registroFacturacion = FacturacionDataGridView.SelectedRows
                Dim sourceFacturacion = CType(FacturacionDataGridView.DataSource, DataTable)

                If registroFacturacion.Count <> 0 Then
                    Dim filaFacturacion = registroFacturacion(0)
                    Dim FormEditar As New FormEditarFacturacion(CShort(sourceFacturacion.Rows(filaFacturacion.Index).Item("id_Entidad")), CShort(sourceFacturacion.Rows(filaFacturacion.Index).Item("id_Facturacion")))
                    FormEditar.ShowDialog()
                    CargaFacturacion()
                Else
                    DesktopMessageBoxControl.DesktopMessageShow("No existen registros selecionados", "Selección de Registro", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("EditarFacturacion", ex)
            End Try
        End Sub

        Private Sub ReporteFacturacion()
            Dim dmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            Try
                Dim registroFacturacion = FacturacionDataGridView.SelectedRows
                Dim sourceFacturacion = CType(FacturacionDataGridView.DataSource, DataTable)

                dmArchiving.Connection_Open(Program.Sesion.Usuario.id)

                If registroFacturacion.Count <> 0 Then
                    Dim filaFacturacion = registroFacturacion(0)
                    Dim dtFacturacion = dmArchiving.Schemadbo.RPT_Facturacion_Detalle.DBFindByfk_Facturacion(CShort(sourceFacturacion.Rows(filaFacturacion.Index).Item("id_Facturacion")))

                    Dim FormReporte As New FormReporteFacturacion(dtFacturacion)
                    FormReporte.ShowDialog()
                Else
                    DesktopMessageBoxControl.DesktopMessageShow("No existen registros selecionados", "Selección de Registro", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("ReporteFacturacion", ex)
            Finally
                dmArchiving.Connection_Close()
            End Try
        End Sub

        Private Sub EjecutarTransmision()
            Dim dmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            Try
                dmArchiving.Connection_Open(Program.Sesion.Usuario.id)
                dmArchiving.DataBase.ExecuteNonQuery("EXEC msdb.dbo.sp_start_job @job_name = 'Facturacion Archiving'")

                DesktopMessageBoxControl.DesktopMessageShow("Se ejecuto el proceso de transmisión", "Transmisión", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("EjecutarTransmision", ex)
            Finally
                dmArchiving.Connection_Close()
            End Try
        End Sub
#End Region

#Region " Funciones "
        Private Function ValidaGeneracion() As Boolean
            Dim bReturn As Boolean = True
            Try
                If FechaFinalDateTimePicker.Value < FechaInicialDateTimePicker.Value Then
                    DesktopMessageBoxControl.DesktopMessageShow("La fecha inicial debe ser inferior a la fecha final", "Fechas incorrectas", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                    bReturn = False
                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("ValidaGeneracion", ex)
            End Try
            Return bReturn
        End Function
#End Region

    End Class

End Namespace