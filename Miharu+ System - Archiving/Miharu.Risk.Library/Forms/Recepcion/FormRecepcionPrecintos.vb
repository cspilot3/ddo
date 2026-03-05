Imports System.Windows.Forms
Imports DBArchiving
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library

Namespace Forms.Recepcion

    Public Class FormRecepcionPrecintos
        Inherits FormBase

#Region " Funciones "

        Private Sub NuevaRecepcion()
            Try
                Dim formSeleccionarRecepcion = New FormPrecinto()
                formSeleccionarRecepcion.ShowDialog()
                CargarPrecintosFecha()
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Se generó el siguiente error: " + ex.Message, "Error", DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
            End Try
        End Sub

        Private Sub CargarPrecintosFecha()
            Dim dbmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

            If Not Program.RiskGlobal.Usa_Validacion_Destape Then
                PrecintosListBox.DataSource = dbmArchiving.Schemadbo.PA_Precinto_Fecha.DBExecute(Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, FechaDateTimePicker.Value.ToString("yyyy/MM/dd"), Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede)
                PrecintosListBox.ValueMember = "id_precinto"
                PrecintosListBox.DisplayMember = "id_precinto"
            Else
                PrecintosListBox.DataSource = dbmArchiving.SchemaRisk.TBL_Precinto.DBFindByFecha_Precintofk_EntidadFK_Sede_Procesamiento(CDate(CDate(FechaDateTimePicker.Text).ToShortDateString), Program.RiskGlobal.Entidad, Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede)
                PrecintosListBox.ValueMember = "id_precinto"
                PrecintosListBox.DisplayMember = "id_precinto"
            End If
            

            dbmArchiving.Connection_Close()
        End Sub

#End Region

#Region " Eventos "

        Private Sub RecepcionarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles RecepcionarButton.Click
            NuevaRecepcion()
            CerrarButton.Focus()
        End Sub

        Private Sub FechaDateTimePicker_ValueChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles FechaDateTimePicker.ValueChanged
            CargarPrecintosFecha()
        End Sub

        Private Sub CerrarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CerrarButton.Click
            Me.Close()
        End Sub

        Private Sub FormRecepcionPrecintos_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            CargarPrecintosFecha()
        End Sub

        Private Sub Fecha_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles FechaDateTimePicker.KeyDown
            If e.KeyCode = Keys.Enter Then
                e.Handled = True
                SendKeys.Send("{TAB}")
            End If
        End Sub

        Private Sub PrecintosComboBox_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles PrecintosListBox.KeyDown
            If e.KeyCode = Keys.Enter Then
                e.Handled = True
                SendKeys.Send("{TAB}")
            End If
        End Sub

#End Region

    End Class

End Namespace