Imports DBCore
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Controls

Namespace Procesos.Cargue

    Public Class FormDesbloquear

#Region " Propiedades "

        Property FechaProceso As Integer

#End Region

#Region " Eventos "

        Private Sub FormDesbloquear_Load(sender As System.Object, e As EventArgs) Handles MyBase.Load
            Me.FechaProcesoLabel.Text = Me.FechaProceso.ToString()
            Buscar()
        End Sub

        Private Sub CerrarButton_Click(sender As System.Object, e As EventArgs) Handles CerrarButton.Click
            Me.Close()
        End Sub

        Private Sub DesbloquearButton_Click(sender As System.Object, e As EventArgs) Handles DesbloquearButton.Click
            Desbloquear()
        End Sub

        Private Sub BuscarButton_Click(sender As System.Object, e As EventArgs) Handles BuscarButton.Click
            Buscar()
        End Sub

#End Region

#Region " Metodos "

        Private Sub Buscar()
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)

                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                Dim resultData = dbmImaging.SchemaProcess.PA_Dashboard_Desbloquear_get.DBExecute(Program.ImagingGlobal.ProyectoImagingRow.fk_Entidad, Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto, Me.FechaProceso)

                If (resultData.Count = 0 OrElse resultData(0).Total = 0) Then
                    Me.IndexacionTextBox.Text = "0"
                    Me.OCRCapturaTextBox.Text = "0"
                    Me.PreCapturaTextBox.Text = "0"
                    Me.PrimeraCapturaTextBox.Text = "0"
                    Me.SegundaCapturaTextBox.Text = "0"
                    Me.TerceraCapturaTextBox.Text = "0"
                    Me.CalidadCapturaTextBox.Text = "0"
                    Me.RecorteTextBox.Text = "0"
                    Me.CalidadRecorteTextBox.Text = "0"
                    Me.ValidacionesTextBox.Text = "0"
                    Me.ValidacionListasTextBox.Text = "0"

                    DesbloquearButton.Enabled = False
                Else
                    Me.IndexacionTextBox.Text = resultData(0).Indexacion.ToString("#,##0")
                    Me.OCRCapturaTextBox.Text = resultData(0).OCRCaptura.ToString("#,##0")
                    Me.PreCapturaTextBox.Text = resultData(0).Precaptura.ToString("#,##0")
                    Me.PrimeraCapturaTextBox.Text = resultData(0).PrimeraCaptura.ToString("#,##0")
                    Me.SegundaCapturaTextBox.Text = resultData(0).SegundaCaptura.ToString("#,##0")
                    Me.TerceraCapturaTextBox.Text = resultData(0).terceraCaptura.ToString("#,##0")
                    Me.CalidadCapturaTextBox.Text = resultData(0).CalidadCaptura.ToString("#,##0")
                    Me.RecorteTextBox.Text = resultData(0).Recorte.ToString("#,##0")
                    Me.CalidadRecorteTextBox.Text = resultData(0).CalidadRecorte.ToString("#,##0")
                    Me.ValidacionesTextBox.Text = resultData(0).Validaciones.ToString("#,##0")
                    Me.ValidacionListasTextBox.Text = resultData(0).ValidacionListas.ToString("#,##0")

                    DesbloquearButton.Enabled = True
                End If

                Me.IndexacionCheckBox.Enabled = Not Me.IndexacionTextBox.Text = "0"
                Me.OCRCapturaCheckBox.Enabled = Not Me.OCRCapturaTextBox.Text = "0"
                Me.PreCapturaCheckBox.Enabled = Not Me.PreCapturaTextBox.Text = "0"
                Me.PrimeraCapturaCheckBox.Enabled = Not Me.PrimeraCapturaTextBox.Text = "0"
                Me.SegundaCapturaCheckBox.Enabled = Not Me.SegundaCapturaTextBox.Text = "0"
                Me.TerceraCapturaCheckBox.Enabled = Not Me.TerceraCapturaTextBox.Text = "0"
                Me.CalidadCapturaCheckBox.Enabled = Not Me.CalidadCapturaTextBox.Text = "0"
                Me.RecorteCheckBox.Enabled = Not Me.RecorteTextBox.Text = "0"
                Me.CalidadRecorteCheckBox.Enabled = Not Me.CalidadRecorteTextBox.Text = "0"
                Me.ValidacionesCheckBox.Enabled = Not Me.ValidacionesTextBox.Text = "0"
                Me.ValidacionListasCheckBox.Enabled = Not Me.ValidacionListasTextBox.Text = "0"

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Buscar", ex)
                Return
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

        Private Sub Desbloquear()
            Dim total As Integer = 0
            If (Me.IndexacionCheckBox.Checked) Then total += 1
            If (Me.OCRCapturaCheckBox.Checked) Then total += 1
            If (Me.PreCapturaCheckBox.Checked) Then total += 1
            If (Me.PrimeraCapturaCheckBox.Checked) Then total += 1
            If (Me.SegundaCapturaCheckBox.Checked) Then total += 1
            If (Me.TerceraCapturaCheckBox.Checked) Then total += 1
            If (Me.CalidadCapturaCheckBox.Checked) Then total += 1
            If (Me.RecorteCheckBox.Checked) Then total += 1
            If (Me.CalidadRecorteCheckBox.Checked) Then total += 1
            If (Me.ValidacionesCheckBox.Checked) Then total += 1
            If (Me.ValidacionListasCheckBox.Checked) Then total += 1

            If (total = 0) Then
                MessageBox.Show("Debe seleccionar al menos una etapa con registros para a desbloquear", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If (MessageBox.Show("Está seguro que desea desbloquear?", Program.AssemblyName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes) Then
                Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

                Try
                    dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)

                    dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                    If (Me.IndexacionCheckBox.Checked) Then dbmImaging.SchemaProcess.PA_Dashboard_Desbloquear_Paquetes_Masivo.DBExecute(Program.ImagingGlobal.ProyectoImagingRow.fk_Entidad, Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto, Me.FechaProceso)
                    If (Me.OCRCapturaCheckBox.Checked) Then dbmImaging.SchemaProcess.PA_Dashboard_Desbloquear_Capturas_Masivo.DBExecute(Program.ImagingGlobal.ProyectoImagingRow.fk_Entidad, Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto, Me.FechaProceso, EstadoEnum.OCR_Captura)
                    If (Me.PreCapturaCheckBox.Checked) Then dbmImaging.SchemaProcess.PA_Dashboard_Desbloquear_Capturas_Masivo.DBExecute(Program.ImagingGlobal.ProyectoImagingRow.fk_Entidad, Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto, Me.FechaProceso, EstadoEnum.Pre_Captura)
                    If (Me.PrimeraCapturaCheckBox.Checked) Then dbmImaging.SchemaProcess.PA_Dashboard_Desbloquear_Capturas_Masivo.DBExecute(Program.ImagingGlobal.ProyectoImagingRow.fk_Entidad, Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto, Me.FechaProceso, EstadoEnum.Captura)
                    If (Me.SegundaCapturaCheckBox.Checked) Then dbmImaging.SchemaProcess.PA_Dashboard_Desbloquear_Capturas_Masivo.DBExecute(Program.ImagingGlobal.ProyectoImagingRow.fk_Entidad, Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto, Me.FechaProceso, EstadoEnum.Segunda_Captura)
                    If (Me.TerceraCapturaCheckBox.Checked) Then dbmImaging.SchemaProcess.PA_Dashboard_Desbloquear_Capturas_Masivo.DBExecute(Program.ImagingGlobal.ProyectoImagingRow.fk_Entidad, Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto, Me.FechaProceso, EstadoEnum.Tercera_Captura)
                    If (Me.CalidadCapturaCheckBox.Checked) Then dbmImaging.SchemaProcess.PA_Dashboard_Desbloquear_Capturas_Masivo.DBExecute(Program.ImagingGlobal.ProyectoImagingRow.fk_Entidad, Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto, Me.FechaProceso, EstadoEnum.Calidad_Captura)
                    If (Me.RecorteCheckBox.Checked) Then dbmImaging.SchemaProcess.PA_Dashboard_Desbloquear_Capturas_Masivo.DBExecute(Program.ImagingGlobal.ProyectoImagingRow.fk_Entidad, Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto, Me.FechaProceso, EstadoEnum.Recorte)
                    If (Me.CalidadRecorteCheckBox.Checked) Then dbmImaging.SchemaProcess.PA_Dashboard_Desbloquear_Capturas_Masivo.DBExecute(Program.ImagingGlobal.ProyectoImagingRow.fk_Entidad, Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto, Me.FechaProceso, EstadoEnum.Calidad_Recorte)
                    If (Me.ValidacionesCheckBox.Checked) Then dbmImaging.SchemaProcess.PA_Dashboard_Desbloquear_Validaciones_Masivo.DBExecute(Program.ImagingGlobal.ProyectoImagingRow.fk_Entidad, Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto, Me.FechaProceso)
                    If (Me.ValidacionListasCheckBox.Checked) Then dbmImaging.SchemaProcess.PA_Dashboard_Desbloquear_Validacion_Listas_Masivo.DBExecute(Program.ImagingGlobal.ProyectoImagingRow.fk_Entidad, Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto, Me.FechaProceso)


                Catch ex As Exception
                    DesktopMessageBoxControl.DesktopMessageShow("Desbloquear", ex)
                    Return
                Finally
                    If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                End Try

                MessageBox.Show("El proceso de desbloqueo se realizó exitosamente", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Information)

                Buscar()

            End If
        End Sub

#End Region

    End Class

End Namespace