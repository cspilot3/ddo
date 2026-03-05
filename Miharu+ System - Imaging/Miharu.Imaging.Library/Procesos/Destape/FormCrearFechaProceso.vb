Imports DBImaging
Imports DMB = Miharu.Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl

Namespace Procesos.Destape
    Public Class FormCrearFechaProceso
        Inherits Form

#Region " Declaraciones "

        Private _fechaProceso As DateTime

#End Region

#Region " Propiedades "

        Public Property NewFechaProceso As Integer

#End Region

#Region " Eventos "

        Private Sub AceptarButtonClick(sender As System.Object, e As EventArgs) Handles AceptarButton.Click
            _fechaProceso = FechaInicialDateTimePicker.Value
            Dim DBMIMaging As New DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)

            Try
                DBMIMaging.Connection_Open(Program.Sesion.Usuario.id)

                Dim fechaProceso = Integer.Parse(_fechaProceso.ToString("yyyyMMdd"))
                Dim fechaProcesoDataTable = DBMIMaging.SchemaProcess.TBL_Fecha_Proceso.DBGet(Program.ImagingGlobal.ProyectoImagingRow.fk_Entidad, Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto, fechaProceso, Program.DesktopGlobal.PuestoTrabajoRow.fk_Entidad)

                If fechaProcesoDataTable.Rows.Count = 0 Then

                    Dim fechaProcesoNueva = New SchemaProcess.TBL_Fecha_ProcesoType
                    With fechaProcesoNueva
                        .fk_Entidad = Program.ImagingGlobal.ProyectoImagingRow.fk_Entidad
                        .fk_Proyecto = Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto
                        .id_fecha_proceso = Integer.Parse(_fechaProceso.ToString("yyyyMMdd"))
                        .Fecha_Proceso = _fechaProceso
                        .fk_Entidad_Procesamiento = Program.DesktopGlobal.PuestoTrabajoRow.fk_Entidad
                        .fk_Usuario_Apertura = Program.Sesion.Usuario.id
                        .Fecha_Apertura = DateTime.Now
                        .Cerrado = False
                    End With
                    DBMIMaging.SchemaProcess.TBL_Fecha_Proceso.DBInsert(fechaProcesoNueva)

                    Me.NewFechaProceso = fechaProcesoNueva.id_fecha_proceso.Value

                Else
                    MessageBox.Show("La fecha ya se encuentra en el sistema", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If

            Catch ex As Exception
                DMB.DesktopMessageShow("Crear Fecha de Proceso", ex)
                DialogResult = Windows.Forms.DialogResult.Cancel
            Finally
                DBMIMaging.Connection_Close()
            End Try

            Me.DialogResult = Windows.Forms.DialogResult.OK
        End Sub

        Private Sub CancelarButtonClick(sender As System.Object, e As EventArgs) Handles CancelarButton.Click
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
        End Sub

        Private Sub FormCrearFechaProceso_Load(sender As System.Object, e As EventArgs) Handles MyBase.Load
            FechaInicialDateTimePicker.Value = DateTime.Now
            FechaInicialDateTimePicker.MaxDate = DateTime.Now
        End Sub

#End Region

    End Class

End Namespace