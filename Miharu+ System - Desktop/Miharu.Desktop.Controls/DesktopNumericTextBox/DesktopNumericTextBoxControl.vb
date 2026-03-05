Imports System.Drawing
Imports System.Windows.Forms

Namespace DesktopNumericTextBox

    Public Class DesktopNumericTextBoxControl
        Inherits TextBox

#Region " Declaraciones "

        Private Const DecimalSeparador As Char = "."

        Private Const ThousandSeparador As Char = ","

        Private _isDecimal As Boolean = False

        Private _formating As Boolean = False

        Public Property NombreCampo As String

#End Region

#Region " Propiedades "

        Public Property FocusIn As Color

        Public Property FocusOut As Color

        Public Property UsaDecimales As Boolean

        Public Property CantidadDecimales As Short

        Public Shadows Property Text As String
            Get
                Return MyBase.Text
            End Get
            Set(ByVal value As String)
                Dim newValue = value.Replace(ThousandSeparador, DecimalSeparador)

                Dim splitValue = newValue.Split(DecimalSeparador)

                newValue = ""
                For i = 0 To splitValue.Length - 2
                    newValue &= splitValue(i)
                Next

                If (splitValue.Length > 1) Then
                    newValue &= DecimalSeparador
                End If

                newValue &= splitValue(splitValue.Length - 1)

                newValue = newValue.TrimStart("0"c)

                If (newValue = "") Then
                    newValue = "0"
                ElseIf (newValue.StartsWith(DecimalSeparador)) Then
                    newValue = "0" & newValue
                End If

                MyBase.Text = newValue
            End Set
        End Property

        Public Property UsaRango As Boolean

        Public Property MinValue() As Double

        Public Property MaxValue() As Double

        Public ReadOnly Property IsEmpty As Boolean
            Get
                ' ReSharper disable once CompareOfFloatsByEqualityOperator
                Return (DataToNumericD() = 0.0)
            End Get
        End Property

        Public ReadOnly Property ValidChars As String
            Get
                Return "0123456789.," & Convert.ToChar(8) & Convert.ToChar(127)
            End Get
        End Property

#End Region

#Region " Constructores "

        Public Sub New()
            InitializeComponent()
            Me.NombreCampo = ""
        End Sub

#End Region

#Region " Eventos "

        Private Sub DesktopTextBox_GotFocus(ByVal sender As Object, ByVal e As EventArgs) Handles Me.GotFocus
            If (Not Me.ReadOnly And Me.Enabled) Then
                ' ReSharper disable once CompareOfFloatsByEqualityOperator
                If (CDbl(GetValue()) = 0.0) Then
                    MyBase.Text = String.Empty
                End If
            End If

            If (Not Me.ReadOnly) Then
                Me.BackColor = Me.FocusIn
                MoveCursor()
            End If
        End Sub

        Private Sub DesktopNumericTextBox_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Click
            MoveCursor()
        End Sub

        Private Sub DesktopNumericTextBox_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles Me.KeyDown
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

            If (MyBase.Text = "") Then
                _isDecimal = False
            End If

            MoveCursor()

            If (e.KeyCode = Keys.Delete) Then
                MyBase.Text = String.Empty
            ElseIf (e.KeyCode = Keys.Multiply Or e.KeyValue = 187) Then
                e.SuppressKeyPress = True

                Dim valor = MyBase.Text.TrimStart("-")

                If (valor.Length > 0 And Not (valor.StartsWith("0") Or _isDecimal)) Then
                    MyBase.Text += "000"
                End If
            ElseIf ((e.KeyValue >= 48 And e.KeyValue <= 57) Or (e.KeyValue >= 96 And e.KeyValue <= 105)) Then
                If (_isDecimal) Then
                    Dim parts = MyBase.Text.Split(DecimalSeparador)
                    Try
                        If (parts(1).Length >= Me.CantidadDecimales) Then
                            e.SuppressKeyPress = True
                        End If
                    Catch ex As Exception

                    End Try

                ElseIf (MyBase.Text.StartsWith("0") And Not _isDecimal And (e.KeyValue <> 190 Or e.KeyValue <> 110)) Then
                    e.SuppressKeyPress = True
                ElseIf (MyBase.Text.StartsWith("-0") And Not _isDecimal And (e.KeyValue <> 190 Or e.KeyValue <> 110)) Then
                    e.SuppressKeyPress = True
                End If

            ElseIf ((e.KeyValue = 190 Or e.KeyValue = 110) And Not _isDecimal And UsaDecimales And Me.CantidadDecimales > 0 And MyBase.Text.Length > 0) Then
                If (MyBase.Text.StartsWith("-") And MyBase.Text.Length = 1) Then
                    e.SuppressKeyPress = True
                Else
                    _isDecimal = True
                End If
            ElseIf (e.KeyCode = Keys.Back) Then
                If (MyBase.Text.EndsWith(DecimalSeparador.ToString())) Then
                    _isDecimal = False
                End If
            ElseIf ((e.KeyValue = 189 Or e.KeyCode = Keys.Subtract) And MyBase.Text.Length = 0) Then
                e.SuppressKeyPress = False
            ElseIf (e.KeyValue <> Keys.Tab) Then
                e.SuppressKeyPress = True
            End If
        End Sub

        Private Sub DesktopNumericTextBox_Leave(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Leave
            Formatear(True)
        End Sub

        Private Sub DesktopTextBox_LostFocus(ByVal sender As Object, ByVal e As EventArgs) Handles Me.LostFocus
            'Validar()
        End Sub

        Private Sub DesktopNumericTextBox_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Me.TextChanged
            Formatear(False)
        End Sub

        Private Sub DesktopNumericTextBox_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles Me.KeyPress
            e.Handled = Not Me.ValidChars.Contains(e.KeyChar)
        End Sub

#End Region

#Region " Metodos "

        Private Sub InitializeComponent()
            Me.SuspendLayout()
            '
            'DesktopTextBox
            '
            Me.ShortcutsEnabled = True
            Me.TextAlign = HorizontalAlignment.Right

            Me.FocusIn = Color.LightYellow
            Me.FocusOut = Color.White
            Me.UsaRango = False
            Me.Font = New Font("Verdana", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0)

            Me.ResumeLayout(False)
        End Sub

        Public Sub Formatear(ByVal nForceFormat As Boolean)
            If (Not _formating And (MyBase.Text <> "" Or nForceFormat)) Then
                _formating = True
                Dim isSigned As Boolean = MyBase.Text.StartsWith("-")

                Dim parts = MyBase.Text.TrimStart("-").Replace(ThousandSeparador.ToString(), "").Split(DecimalSeparador)

                Dim formatedText = String.Empty

                For i As Integer = parts(0).Length - 1 To 0 Step -1
                    If formatedText.Length >= 3 And (parts(0).Length - 1 - i) Mod 3 = 0 Then
                        formatedText = ThousandSeparador + formatedText
                    End If

                    formatedText = parts(0)(i) + formatedText
                Next

                If (parts.Length > 1) Then
                    If (nForceFormat) Then
                        formatedText += DecimalSeparador + parts(1).PadRight(Me.CantidadDecimales, "0").Substring(0, Me.CantidadDecimales)
                    ElseIf (parts(1).Length > Me.CantidadDecimales) Then
                        formatedText += DecimalSeparador + parts(1).Substring(0, Me.CantidadDecimales)
                    Else
                        formatedText += DecimalSeparador + parts(1)
                    End If
                ElseIf (nForceFormat) Then
                    If (formatedText = "") Then
                        If (Me.UsaDecimales) Then
                            formatedText = "0" + DecimalSeparador.ToString().PadRight(Me.CantidadDecimales + 1, "0")
                        Else
                            formatedText = "0"
                        End If
                    ElseIf (Me.UsaDecimales) Then
                        formatedText += DecimalSeparador.ToString().PadRight(Me.CantidadDecimales + 1, "0")
                    End If
                End If

                If (isSigned) Then
                    formatedText = "-" + formatedText
                End If

                MyBase.Text = formatedText
                MoveCursor()

                _formating = False
            End If

        End Sub

        Private Sub MoveCursor()
            Me.SelectionStart = MyBase.Text.Length
            Me.SelectionLength = 0
        End Sub

        Public Function GetString()
            Return MyBase.Text.Replace(ThousandSeparador, "")
        End Function

        Public Function GetValue() As Object
            If (UsaDecimales) Then
                Return DataToNumericD()
            Else
                Return DataToNumericL()
            End If
        End Function

        Private Function DataToNumericD() As Decimal
            If (MyBase.Text = "") Then
                Return 0
            Else
                Dim valor As String = MyBase.Text.Replace(ThousandSeparador, "").Replace(DecimalSeparador, getPuntoFlotante())
                Dim resultado As Decimal

                If (Decimal.TryParse(valor, resultado)) Then
                    Return resultado
                Else
                    Return 0
                End If
            End If
        End Function

        Private Function DataToNumericL() As Long
            If (MyBase.Text = "") Then
                Return 0
            Else
                Dim valor As String = MyBase.Text.Replace(ThousandSeparador, "")
                Dim resultado As Long

                If (Long.TryParse(valor, resultado)) Then
                    Return resultado
                Else
                    Return 0
                End If
            End If
        End Function

        Public Shared Function GetPuntoFlotante() As String
            Dim a = Double.Parse("0.5")
            If (a > 1) Then
                Return ","
            Else
                Return "."
            End If
        End Function

#End Region

#Region " Funciones "

        Public Function Validar() As Boolean
            If (Me.UsaRango) Then
                Dim valor = DataToNumericD()
                If (valor < Me.MinValue Or valor > Me.MaxValue) Then
                    MessageBox.Show("El valor capturado del campo [" + NombreCampo + "] debe estar entre: " & Me.MinValue & " y " & Me.MaxValue, "Validar", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Me.Focus()
                    Return False
                End If
            End If

            Return True
        End Function

#End Region

    End Class

End Namespace