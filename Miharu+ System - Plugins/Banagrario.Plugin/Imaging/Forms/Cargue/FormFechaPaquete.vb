Imports Miharu.Desktop.Controls.DesktopMessageBox

Namespace Imaging.Forms.Cargue

    Public Class FormFechaPaquete

#Region " Declaraciones "

        Private _PaqueteIni As Integer
        Private _PaqueteFin As Integer
        Private _plugin As New BanagrarioImagingPlugin

#End Region

#Region " Propiedades "

        Public Property Fecha() As Date
            Get
                Return FechaDateTimePicker.Value
            End Get
            Set(ByVal value As Date)
                FechaDateTimePicker.Value = value
            End Set
        End Property

        Public ReadOnly Property PaqueteIni() As Integer
            Get
                Return _PaqueteIni
            End Get
        End Property

        Public ReadOnly Property PaqueteFin() As Integer
            Get
                Return _PaqueteFin
            End Get
        End Property

#End Region

#Region " Eventos "

        Private Sub FormFechaPaquete_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            LoadConfig()
        End Sub

        Private Sub CancelarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CancelarButton.Click
            DialogResult = Windows.Forms.DialogResult.Cancel
            Me.Close()
        End Sub

        Private Sub AceptarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AceptarButton.Click
            If (Validar()) Then
                DialogResult = Windows.Forms.DialogResult.OK
                Me.Close()
            Else
                DialogResult = Windows.Forms.DialogResult.None
            End If
        End Sub

#End Region

#Region " Metodos "

        Private Sub LoadConfig()
            Dim NewDate = Now

            If (NewDate.Hour < _plugin.HoraCambioFechaProceso) Then
                NewDate = NewDate.AddDays(-1)
            End If

            FechaDateTimePicker.Value = NewDate
        End Sub

#End Region

#Region " Funciones "
        Private Function Validar() As Boolean
            Try
                _PaqueteIni = Integer.Parse(PaqueteInicialTextBox.Text)
            Catch
                DesktopMessageBoxControl.DesktopMessageShow("Debe digitar un número válido", "Paquete inválido", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                PaqueteInicialTextBox.Focus()
                Return False
            End Try

            Try
                _PaqueteFin = Integer.Parse(PaqueteFinalTextBox.Text)
            Catch
                DesktopMessageBoxControl.DesktopMessageShow("Debe digitar un número válido", "Paquete inválido", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                PaqueteFinalTextBox.Focus()
                Return False
            End Try

            If (_PaqueteIni < 0) Then
                DesktopMessageBoxControl.DesktopMessageShow("Debe digitar un número mayor que 0 (cero)", "Paquete inválido", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                PaqueteInicialTextBox.Focus()
                Return False
            End If

            If (_PaqueteFin < 0) Then
                DesktopMessageBoxControl.DesktopMessageShow("Debe digitar un número mayor que 0 (cero)", "Paquete inválido", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                PaqueteFinalTextBox.Focus()
                Return False
            End If

            If (_PaqueteIni > _PaqueteFin) Then
                DesktopMessageBoxControl.DesktopMessageShow("El número inicial debe ser mayor que el número final", "Paquete inválido", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                PaqueteInicialTextBox.Focus()
                Return False
            End If

            Return True
        End Function
#End Region

    End Class

End Namespace