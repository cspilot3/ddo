Imports System.Drawing
Imports System.Windows.Forms

Namespace DesktopCheckBox

    Public Class DesktopCheckBoxControl
        Inherits CheckBox

#Region "Declaraciones"
        Private _Fk_Documento As Short
        Private _Fk_Campo As Short
        Private _fk_Validacion As Short

#End Region

#Region "Propiedades"
        Public Property fk_Documento As Integer
            Get
                Return _Fk_Documento
            End Get
            Set(value As Integer)
                _Fk_Documento = value
            End Set
        End Property

        Public Property fk_Campo As Integer
            Get
                Return _Fk_Campo
            End Get
            Set(value As Integer)
                _Fk_Campo = value
            End Set
        End Property

        Public Property fk_Validacion As Integer
            Get
                Return _fk_Validacion
            End Get
            Set(value As Integer)
                _fk_Validacion = value
            End Set
        End Property
#End Region

#Region " Constructores "

        Public Sub New()
            ' This call is required by the designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            FocusIn = Color.LightYellow
            FocusOut = Color.Gainsboro

            DisabledEnter = False
            DisableSpaceBar = False
            EnabledD1 = False

        End Sub

#End Region

#Region " Eventos "

        Private Sub DesktopCheckBox_GotFocus(ByVal sender As Object, ByVal e As EventArgs) Handles Me.GotFocus
            Me.BackColor = FocusIn
        End Sub

        Private Sub DesktopCheckBox_LostFocus(ByVal sender As Object, ByVal e As EventArgs) Handles Me.LostFocus
            Me.BackColor = FocusOut
        End Sub

        Private Sub DesktopCheckBox_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles Me.KeyPress
            If Not DisabledEnter Then
                If e.KeyChar = ChrW(Keys.Enter) Then
                    e.Handled = True
                    SendKeys.Send("{TAB}")
                End If
            End If

            If DisableSpaceBar Then
                If e.KeyChar = ChrW(Keys.Space) Then
                    e.Handled = True
                    SendKeys.Send("{BACKSPACE}")
                End If
            End If

            If EnabledD1 Then
                If e.KeyChar = ChrW(Keys.D1) Then
                    Select Case Me.Checked
                        Case True
                            Me.Checked = False
                        Case False
                            Me.Checked = True
                    End Select
                End If
            End If
        End Sub


#End Region

#Region " Propiedades "

        Public Property DisabledEnter As Boolean

        Property FocusIn As Color

        Property FocusOut As Color

        Public Property DisableSpaceBar As Boolean

        Public Property EnabledD1 As Boolean

#End Region


    End Class

End Namespace