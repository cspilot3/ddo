Imports DMB = Miharu.Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl
Imports Miharu.Desktop.Library.Config

Namespace Procesos.Destape
    Public Class FormFechaProceso
        Inherits Form

#Region " Declaraciones "

        Private _fechaProcesoData As DataTable

#End Region

#Region " Propiedades"

        Public Property WorkSpace() As FormImagingWorkSpace

#End Region

#Region " Eventos "

        Private Sub CrearFechaProcesoButton_Click(sender As System.Object, e As EventArgs) Handles CrearFechaProcesoButton.Click
            Dim crearFecha = New FormCrearFechaProceso

            If crearFecha.ShowDialog() = Windows.Forms.DialogResult.OK Then
                BuscarFechas()
                Me.WorkSpace.EventManager.CrearFechaProceso(Program.DesktopGlobal.PuestoTrabajoRow.fk_Entidad, Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, crearFecha.NewFechaProceso)
            End If
        End Sub

        Private Sub BuscarButton_Click(sender As System.Object, e As EventArgs) Handles BuscarButton.Click
            BuscarFechas()
        End Sub

        Private Sub FormFechaProceso_Load(sender As System.Object, e As EventArgs) Handles MyBase.Load
            FechaFinalDateTimePicker.Value = DateTime.Now
            FechaInicialDateTimePicker.Value = FechaFinalDateTimePicker.Value.AddMonths(-1)
        End Sub

        Private Sub CerrarFechaButton_Click(sender As System.Object, e As EventArgs) Handles CerrarFechaButton.Click
            If (FechaProcesoDataGridView.SelectedCells.Count > 0) Then

                Dim CerrarFecha As Boolean = False
                Dim Row = FechaProcesoDataGridView.Rows(FechaProcesoDataGridView.SelectedCells(0).RowIndex)
                Dim fechaCierre = Row.Value(Of Integer)("id_Fecha_Proceso")

                If (MessageBox.Show("Esta seguro?", "Cierre Fecha", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes) Then
                    Dim dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)

                    Try
                        ' Evento al plugin para verificar si se puede cerrar la fecha de proceso - casos especiales diferentes a OT
                        Dim valido As Boolean = True
                        Dim valido2 As Boolean = True
                        Dim msgError As String = ""
                        Me.WorkSpace.EventManager.ValidarCerrarFechaProcesoNoOT(Program.DesktopGlobal.PuestoTrabajoRow.fk_Entidad, Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, fechaCierre, valido, valido2, msgError)


                        If Not (valido) Then
                            CerrarFecha = False
                        ElseIf Not (valido2) Then
                            If (MessageBox.Show(msgError + ". Desea cerrar la fecha de proceso de todos modos?", "Cierre Fecha", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes) Then
                                CerrarFecha = True
                            Else
                                CerrarFecha = False
                                Throw New Exception(msgError)
                            End If
                        Else
                            CerrarFecha = True
                        End If
                        If (CerrarFecha) Then
                            dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                            ''--=============== INICIO REQUERIMIENTO RITM0229763 ===========-
                            'Dim ValidaPrepararDataCruce As String
                            'If Program.ImagingGlobal.ProyectoImagingRow.Usa_Cruce_Generico Then
                            '    ValidaPrepararDataCruce = dbmImaging.SchemaProcess.PA_Get_Prepara_Data_Cruce.DBExecute(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, fechaCierre, 0, 4)
                            '    If (ValidaPrepararDataCruce <> "") Then
                            '        MessageBox.Show("¡" & ValidaPrepararDataCruce & "!", "Cruce", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            '        Return
                            '    End If
                            'End If
                            ''--=============== FIN REQUERIMIENTO RITM0229763 ===========-

                            dbmImaging.SchemaProcess.PA_Cerrar_OT_x_Fecha_Proceso.DBExecute(Program.DesktopGlobal.PuestoTrabajoRow.fk_Entidad, Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, fechaCierre, Program.Sesion.Usuario.id)
                            Dim OTAbiertasData = dbmImaging.SchemaProcess.PA_OTs_Abiertas.DBExecute(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, fechaCierre)

                            If (OTAbiertasData.Rows.Count > 0) Then
                                MessageBox.Show("No se puede cerrar la fecha ya que no se lograron cerrar una o mas OTs", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Else
                                Dim fechaCerrarType = New DBImaging.SchemaProcess.TBL_Fecha_ProcesoType
                                fechaCerrarType.Cerrado = True
                                fechaCerrarType.fk_Usuario_Cierre = Program.Sesion.Usuario.id
                                fechaCerrarType.Fecha_Cierre = DateTime.Now
                                dbmImaging.SchemaProcess.TBL_Fecha_Proceso.DBUpdate(fechaCerrarType, Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, fechaCierre, Program.DesktopGlobal.PuestoTrabajoRow.fk_Entidad)

                                If Program.ImagingGlobal.ProyectoImagingRow.Notificacion_Cierre_FechaProceso Then
                                    If DMB.DesktopMessageShow("Desea enviar el correo con el informe de publicación?", "Cerrar OT", DMB.IconEnum.AdvertencyIcon) = DialogResult.OK Then
                                        Try
                                            dbmImaging.SchemaProcess.PA_Notificacion_CierreFechaProceso.DBExecute(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, fechaCierre, Program.Sesion.Usuario.id)
                                        Catch ex As Exception
                                            DMB.DesktopMessageShow(ex.Message, "Cierre Fecha", DMB.IconEnum.AdvertencyIcon, True)
                                        End Try

                                    End If
                                End If

                                ' Evento al plugin despues de cerrar fecha de proceso
                                Me.WorkSpace.EventManager.CerrarFechaProceso(Program.DesktopGlobal.PuestoTrabajoRow.fk_Entidad, Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, fechaCierre)
                            End If
                        End If
                    Catch ex As Exception
                        DMB.DesktopMessageShow("Cierre Fecha", ex)
                    Finally
                        If dbmImaging Is Nothing Then dbmImaging.Connection_Close()
                    End Try
                End If
            End If

            BuscarFechas()
        End Sub

#End Region

#Region " Metodos "

        Private Sub BuscarFechas()
            Dim dbmImaging As New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)

            Try
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                Dim Entidad = Program.ImagingGlobal.Entidad
                Dim Proyecto = Program.ImagingGlobal.Proyecto
                Dim FechaInicial As DateTime = New Date(FechaInicialDateTimePicker.Value.Year, FechaInicialDateTimePicker.Value.Month, FechaInicialDateTimePicker.Value.Day)
                Dim FechaFinal As DateTime = New Date(FechaFinalDateTimePicker.Value.Year, FechaFinalDateTimePicker.Value.Month, FechaFinalDateTimePicker.Value.Day, 23, 59, 59, 900)
                _fechaProcesoData = dbmImaging.SchemaProcess.PA_Obtiene_Fecha_Proceso.DBExecute(Entidad, Proyecto, FechaInicial, FechaFinal, Program.DesktopGlobal.PuestoTrabajoRow.fk_Entidad)

            Catch ex As Exception
                DMB.DesktopMessageShow("Buscar Fechas Proceso", ex)
            Finally
                If dbmImaging Is Nothing Then dbmImaging.Connection_Close()
            End Try

            FechaProcesoDataGridView.DataSource = _fechaProcesoData
            FechaProcesoDataGridView.Refresh()
        End Sub

#End Region

    End Class
End Namespace