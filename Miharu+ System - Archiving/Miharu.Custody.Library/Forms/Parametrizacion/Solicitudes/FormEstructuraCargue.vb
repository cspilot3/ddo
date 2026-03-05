Imports System.IO
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Controls
Imports DBArchiving
Imports Miharu.Desktop.Library

Namespace Forms.Parametrizacion.Solicitudes

    Public Class FormEstructuraCargue
        Inherits FormBase

#Region " Declaraciones "

        Private _Separador As String = ","
        Dim fs As FileStream

#End Region

#Region " Eventos "
        Private Sub GenerarArchivoButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles GenerarArchivoButton.Click
            GenerarArchivo()
        End Sub

        Private Sub CerrarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CerrarButton.Click
            Me.Close()
        End Sub
#End Region

#Region " Metodos "

        Private Sub GenerarArchivo()
            Try
                If ComaRadioButton.Checked Then _Separador = CChar(",")
                If TabuladorRadioButton.Checked Then _Separador = ControlChars.Tab
                If PuntoComaRadioButton.Checked Then _Separador = CChar(";")

                GuardarArchivoSaveFileDialog.Filter = "Archivo Plano (*.txt)|*.txt"
                GuardarArchivoSaveFileDialog.FileName = "Estructura de cargue [solicitudes Masivas].txt"
                GuardarArchivoSaveFileDialog.Title = "Guardar Estructura de Archivo"

                Dim Resultado = GuardarArchivoSaveFileDialog.ShowDialog()
                If Resultado = Windows.Forms.DialogResult.OK Then
                    fs = CType(GuardarArchivoSaveFileDialog.OpenFile(), FileStream)
                    Me.GenerarArchivoBackgroundWorker.RunWorkerAsync()
                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("GenerarArchivo", ex)
            End Try
        End Sub

        Private Sub EscribeArchivo(ByRef archivo As FileStream)
            Dim dmArchiving = New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)

            Try
                dmArchiving.Connection_Open(Program.Sesion.Usuario.id)

                Dim s As New StreamWriter(archivo, System.Text.Encoding.UTF8)
                s.BaseStream.Seek(0, SeekOrigin.End)
                s.Write("[Usuario Solicitud]" & _Separador)
                s.Write("[Entidad Documento]" & _Separador)
                s.Write("[Proyecto Documento]" & _Separador)
                s.Write("[Esquema]" & _Separador)
                s.Write("[Campo Busqueda 1]" & _Separador)
                s.Write("[Tipo Dato 1]" & _Separador)
                s.Write("[Valor 1]" & _Separador)
                s.Write("[Campo Busqueda 2]" & _Separador)
                s.Write("[Tipo Dato 2]" & _Separador)
                s.Write("[Valor 2]" & _Separador)
                s.Write("[Campo Busqueda 3]" & _Separador)
                s.Write("[Tipo Dato 3]" & _Separador)
                s.Write("[Valor 3]" & _Separador)
                s.Write("[Motivo Solicitud]" & _Separador)
                s.Write("[Prioridad Solicitud]" & _Separador)
                s.Write("[Tipo Solicitud]" & _Separador)
                s.Write("[Documentos Solicitud]" & _Separador)
                s.Write("[Es Usuario Registrado]" & _Separador)
                s.Write("[Identificacion Destinatario]" & _Separador)
                s.Write("[Apellidos]" & _Separador)
                s.Write("[Nombres]" & _Separador)
                s.Write("[Nombre Entidad]" & _Separador)
                s.Write("[Direccion]" & _Separador)
                s.Write("[Departamento]" & _Separador)
                s.Write("[Ciudad]")

                s.WriteLine("")
                s.Write("[Texto(15)]" & _Separador)
                s.Write("[Númerico]" & _Separador)
                s.Write("[Numerico - Opcional]" & _Separador)
                s.Write("[Numerico - Opcional]" & _Separador)
                s.Write("[Numerico]" & _Separador)
                s.Write("[Numerico]" & _Separador)
                s.Write("[Texto]" & _Separador)
                s.Write("[Numerico - Opcional]" & _Separador)
                s.Write("[Numerico - Opcional]" & _Separador)
                s.Write("[Texto - Opcional]" & _Separador)
                s.Write("[Numerico - Opcional]" & _Separador)
                s.Write("[Numerico - Opcional]" & _Separador)
                s.Write("[Texto - Opcional]" & _Separador)
                s.Write("[Numerico]" & _Separador)
                s.Write("[Numerico]" & _Separador)
                s.Write("[Numerico]" & _Separador)
                s.Write("[ID Documento separado por coma]" & _Separador)
                s.Write("[Booleano (0-No 1-Si)]" & _Separador)
                s.Write("[Texto(15)]" & _Separador)
                s.Write("[Texto(50)]" & _Separador)
                s.Write("[Texto(50)]" & _Separador)
                s.Write("[Texto(50)]" & _Separador)
                s.Write("[Texto(100)]" & _Separador)
                s.Write("[Texto(50)]" & _Separador)
                s.Write("[Texto(50)]")

                Me.GenerarArchivoBackgroundWorker.ReportProgress(100)

                s.Close()
                archivo.Close()
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("EscribeArchivo", ex)
            Finally
                dmArchiving.Connection_Close()
            End Try
        End Sub

#Region "BackgroundWorker"

        Private Sub GenerarArchivoBackgroundWorker_DoWork(ByVal sender As System.Object, ByVal e As ComponentModel.DoWorkEventArgs) Handles GenerarArchivoBackgroundWorker.DoWork
            EscribeArchivo(fs)
        End Sub

        Private Sub GenerarArchivoBackgroundWorker_ProgressChanged(ByVal sender As System.Object, ByVal e As ComponentModel.ProgressChangedEventArgs) Handles GenerarArchivoBackgroundWorker.ProgressChanged
            If e.ProgressPercentage = 0 Then
                Me.GeneraArchivoProgressBar.Maximum = 1
            End If
            Me.GeneraArchivoProgressBar.Value = e.ProgressPercentage
        End Sub

        Private Sub GenerarArchivoBackgroundWorker_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As ComponentModel.RunWorkerCompletedEventArgs) Handles GenerarArchivoBackgroundWorker.RunWorkerCompleted
            DesktopMessageBoxControl.DesktopMessageShow("Archivo generado correctamente en " & GuardarArchivoSaveFileDialog.FileName, "Generación archivo", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
        End Sub

#End Region

#End Region

    End Class

End Namespace