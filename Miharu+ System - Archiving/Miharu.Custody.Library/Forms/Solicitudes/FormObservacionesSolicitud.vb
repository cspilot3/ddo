Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Controls
Imports Miharu.Desktop.Library

Namespace Forms.Solicitudes

    Public Class FormObservacionesSolicitud
        Inherits FormBase

#Region " Declaraciones "

        Private _Fecha As String
        Private _Destinatario As String
        Private _RemitidaPor As String
        Private _Observaciones As String

#End Region

#Region " Propiedades "

        Public ReadOnly Property Fecha As String
            Get
                Return _Fecha
            End Get
        End Property

        Public ReadOnly Property Destinatario As String
            Get
                Return _Destinatario
            End Get
        End Property

        Public ReadOnly Property RemitidaPor As String
            Get
                Return _RemitidaPor
            End Get
        End Property

        Public ReadOnly Property Observaciones As String
            Get
                Return _Observaciones
            End Get
        End Property

#End Region

#Region " Eventos "

        Private Sub AceptarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AceptarButton.Click
            Guardar()
        End Sub

        Private Sub CancelarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CancelarButton.Click
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
            Me.Close()
        End Sub

#End Region

#Region " Metodos "

        Private Sub Guardar()
            Try
                If DestinatarioTextBox.Text <> "" And RemitidaPorTextBox.Text <> "" And ObservacionesTextBox.Text <> "" Then
                    _Fecha = FechaLabel.Text & FechaDateTimePicker.Text & vbNewLine
                    _Destinatario = DestinatarioLabel.Text & DestinatarioTextBox.Text & vbNewLine
                    _RemitidaPor = RemitidaPorLabel.Text & RemitidaPorTextBox.Text & vbNewLine
                    _Observaciones = ObservacionesLabel.Text & ObservacionesTextBox.Text & vbNewLine
                    Me.DialogResult = Windows.Forms.DialogResult.OK
                    Me.Close()
                Else
                    DesktopMessageBoxControl.DesktopMessageShow("Debe diligenciar todos los campos", "Campos Obligatorios", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                    Me.DialogResult = Windows.Forms.DialogResult.Cancel
                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Guardar", ex)
            End Try
        End Sub

#End Region

    End Class

End Namespace