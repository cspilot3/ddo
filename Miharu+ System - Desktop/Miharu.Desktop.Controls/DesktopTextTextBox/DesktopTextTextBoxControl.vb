Imports System.Drawing
Imports System.Windows.Forms
Imports System.Text.RegularExpressions

Namespace DesktopTextTextBox

    Public Class DesktopTextTextBoxControl
        Inherits MaskedTextBox

#Region " Propiedades "

        Public Property FocusIn As Color

        Public Property FocusOut As Color

        Public Property MinLength As Integer

        Public Overrides Property MaxLength As Integer

        Public Property Formato As String

        Public Property NombreCampo As String

        Public ReadOnly Property IsEmpty As Boolean
            Get
                Return (Me.Text = "")
            End Get
        End Property

#End Region

#Region " Constructores "

        Public Sub New()
            MyBase.New()

            'El Diseñador de componentes requiere esta llamada.
            InitializeComponent()

            'Me.RejectInputOnFirstFailure = True
        End Sub

#End Region

#Region " Eventos "

        Private Sub DesktopTextBox_GotFocus(ByVal sender As Object, ByVal e As EventArgs) Handles Me.GotFocus
            If (Not Me.ReadOnly) Then
                Me.BackColor = Me.FocusIn

                Me.SelectionStart = 0

                If (Me.IsEmpty) Then
                    Me.SelectionLength = 0
                Else
                    Me.SelectionLength = Me.Text.Length
                End If
            End If
        End Sub

        Private Sub DesktopTextBox_LostFocus(ByVal sender As Object, ByVal e As EventArgs) Handles Me.LostFocus
            'Validar()
        End Sub

        Private Sub DesktopTextBox_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles Me.KeyDown
            If (e.Control) Then
                If (e.KeyCode = Keys.C Or e.KeyCode = Keys.V Or e.KeyCode = Keys.X Or e.KeyCode = Keys.Left Or e.KeyCode = Keys.Right Or e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Or e.KeyCode = Keys.Subtract Or e.KeyCode = Keys.Add) Then
                    e.SuppressKeyPress = True
                    Return
                End If
            ElseIf (e.Shift) Then
                If (e.KeyCode = Keys.Delete Or e.KeyCode = Keys.Insert) Then
                    e.SuppressKeyPress = True
                    Return
                End If
            Else
                Select Case e.KeyCode
                    Case Keys.Left, Keys.Right, Keys.Back, Keys.Delete, Keys.Home, Keys.End
                        Return

                    Case Else
                        If (Me.MaxLength > 0 And Me.Text.Length >= Me.MaxLength) Then
                            e.SuppressKeyPress = True
                        End If

                End Select
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

            Me.ShortcutsEnabled = True
            Me.FocusIn = Color.LightYellow
            Me.FocusOut = Color.White
            Me.MinLength = 0
            Me.AllowPromptAsInput = False
            Me.Font = New Font("Verdana", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0)
            Me.PromptChar = " "c
            Me.ResetOnPrompt = False
            Me.ResetOnSpace = False
            Me.Formato = ""
            Me.NombreCampo = ""

            Me.ResumeLayout(False)
        End Sub

#End Region

#Region " Funciones "

        Public Function Validar() As Boolean
            'PATCH: Ignora las validaciones si no ocurrio ningun cambio        
            If (Not Me.ReadOnly) Then
                Me.BackColor = Me.FocusOut

                If (Me.MinLength <> 0 AndAlso Me.Text.Length < Me.MinLength) Then
                    MessageBox.Show("La longitud mínima del campo [" + NombreCampo + "] es [" & Me.MinLength & "]", "Longitud no válida", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Me.SelectAll()
                    Me.Focus()
                    Return False
                End If


                If (Me.Formato <> "") Then
                    Dim check As New Regex(Me.Formato)

                    If Not (check.IsMatch(Me.Text)) Then
                        MessageBox.Show("El valor del campo [" + NombreCampo + "] no es válido", "Formato no válido", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Me.SelectAll()
                        Me.Focus()
                        Return False
                    End If
                End If
            End If

            Return True
        End Function

#End Region

    End Class

End Namespace