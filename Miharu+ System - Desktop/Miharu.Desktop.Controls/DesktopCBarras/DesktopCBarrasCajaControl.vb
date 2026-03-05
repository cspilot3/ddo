Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

Namespace DesktopCBarrasCajaControl
    Public Class DesktopCBarrasCajaControl
        Inherits System.Windows.Forms.TextBox

#Region "Declaraciones"
        Private _inFocus As Color = Color.LightYellow
        Private _outFocus As Color = Color.White
#End Region

#Region "Propiedades"
        Property FocusIn As Color
            Get
                Return _inFocus
            End Get
            Set(ByVal value As Color)
                _inFocus = value
            End Set
        End Property

        Property FocusOut As Color
            Get
                Return _outFocus
            End Get
            Set(ByVal value As Color)
                _outFocus = value
            End Set
        End Property

#End Region

#Region "Eventos"

        Private Sub DesktopCBarrasCajaControl_GetFocus(ByVal Sender As Object, ByVal e As System.EventArgs) Handles Me.GotFocus
            Me.BackColor = _inFocus
        End Sub

        Private Sub DesktopCBarrasCajaControl_LostFocus(ByVal Sender As Object, ByVal e As System.EventArgs) Handles Me.LostFocus
            Me.BackColor = _outFocus
            Me.Text = CBarrasFlash(Me.Text)
        End Sub

        Private Sub DesktopCBarrasCajaControl_KeyDown(ByVal Sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
            If (e.KeyCode = Keys.Enter) Then
                e.Handled = True
                SendKeys.Send("{TAB}")
            End If
        End Sub

#End Region

#Region "Metodos"

        Public Shared Function CbarrasSoloFomat(ByVal CBarras As Integer) As String
            Return String.Concat("000000000", CStr(CBarras)).Substring(String.Concat("000000000", CStr(CBarras)).Length - 9)
        End Function

        Public Shared Function CBarrasFlash(ByVal CBarrasShort As String) As String
            Try
                Dim Tipologia As String = CBarrasShort.Substring(0, 3)
                Dim CBarras As String = CBarrasShort.Substring(3)
                Return Tipologia & CbarrasSoloFomat(CInt(CBarras))
            Catch ex As Exception
                Return CBarrasShort
            End Try
        End Function

#End Region

    End Class
End Namespace
