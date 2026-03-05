Imports System.Drawing
Imports System.Globalization
Imports System.Windows.Forms

Namespace DesktopDateTimeTextBox

    Public Class DesktopDateTimeTextBoxControl
        Inherits MaskedTextBox

#Region " Propiedades "

        Property FocusIn As Color

        Property FocusOut As Color

        Public Property DateFormat As String
        Public Property NombreCampo As String

        Public ReadOnly Property IsEmpty As Boolean
            Get
                Return (Me.Text = "")
            End Get
        End Property

#End Region

#Region " Constructores "

        Public Sub New()
            InitializeComponent()
        End Sub

#End Region

#Region " Eventos "

        Private Sub DesktopTextBox_GotFocus(ByVal sender As Object, ByVal e As EventArgs) Handles Me.GotFocus
            If (Not Me.ReadOnly) Then
                Me.BackColor = Me.FocusIn

                If (Me.IsEmpty) Then
                    Me.SelectionLength = 0
                    Me.SelectionStart = 0
                Else
                    Me.SelectAll()
                End If
            End If
        End Sub

        Private Sub DesktopTextBox_LostFocus(ByVal sender As Object, ByVal e As EventArgs) Handles Me.LostFocus
            'Validar()
        End Sub

        Private Sub DesktopTextBox_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles Me.KeyDown
            If (e.Control) Then
                If (e.KeyCode = Keys.C Or e.KeyCode = Keys.V Or e.KeyCode = Keys.X Or e.KeyCode = Keys.Left Or e.KeyCode = Keys.Right Or e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up) Then
                    e.SuppressKeyPress = True
                    Return
                End If
            ElseIf (e.Shift) Then
                If (e.KeyCode = Keys.Delete Or e.KeyCode = Keys.Insert) Then
                    e.SuppressKeyPress = True
                    Return
                End If
            End If
        End Sub

        Private Sub DesktopTextBox_MouseDown(ByVal sender As System.Object, ByVal e As MouseEventArgs) Handles MyBase.MouseDown
            If e.Button = MouseButtons.Right Then
                Exit Sub
            End If
        End Sub

        Private Sub DesktopTextBox_ReadOnlyChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Me.ReadOnlyChanged
            If (Me.ReadOnly) Then
                Me.BackColor = Color.LightGray
            End If
        End Sub

#End Region

#Region " Metodos "

        Private Sub InitializeComponent()
            Me.SuspendLayout()

            Me.FocusIn = Color.LightYellow
            Me.FocusOut = Color.White
            Me.ShortcutsEnabled = True
            Me.DateFormat = "yyyy/MM/dd"
            Me.Mask = "9999/99/99"
            Me.Font = New Font("Verdana", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0)
            Me.ResetOnPrompt = False
            Me.ResetOnSpace = False
            Me.NombreCampo = ""

            Me.ResumeLayout(False)
        End Sub

#End Region

#Region " Funciones "

        Public Function Validar() As Boolean
            If (Not Me.ReadOnly AndAlso Not Me.IsEmpty) Then
                Me.BackColor = Me.FocusOut

                Dim Formato

                If (Me.DateFormat <> "") Then
                    Formato = Me.DateFormat
                Else
                    Formato = "yyyy/MM/dd"
                End If

                Dim Fecha As DateTime

                If (Not DateTime.TryParseExact(Me.Text, Formato, Nothing, DateTimeStyles.None, Fecha)) Then
                    MessageBox.Show("Fecha inválida [" + NombreCampo + "] ,por favor verifique el formato. (Formato: " & Formato & ")", "Error en fecha", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Me.SelectAll()
                    Me.Focus()
                    Return False
                End If

                Return True
            End If

            Return True
        End Function

        Public Function getValue() As DateTime
            Dim Formato

            If (Me.Mask <> "") Then
                Formato = Me.DateFormat
            Else
                Formato = "yyyy/MM/dd"
            End If

            Return DateTime.ParseExact(Me.Text, Formato, Nothing, DateTimeStyles.None)
        End Function
#End Region

    End Class

End Namespace