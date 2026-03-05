Imports System.ComponentModel
Imports System.Drawing
Imports System.Globalization
Imports System.Windows.Forms

Namespace DesktopTextBox

    Public Class DesktopTextBoxControl
        Inherits MaskedTextBox

#Region " Declaraciones "

        Dim _DisabledTab As Boolean = False
        Dim _DisabledEnter As Boolean = False

        Private Formating As Boolean = False

        Private IsDecimal As Boolean = False
        Private Const DecimalSeparador As Char = "."
        Private Const ThousandSeparador As Char = ","

        Private _inFocus As Color = Color.LightYellow
        Private _outFocus As Color = Color.White
        Private _tipo As TipoTextBox = TipoTextBox.Normal

        Private _Rango As New Rango()
        
        Private _MinimumLength As Short
        Private _MaximumLength As Short

        Private _Fk_Documento As Short
        Private _Fk_Validacion As Short
        Private _Fk_Campo As Short

        Private _Validar As Boolean = True
        Public Property _Obligatorio As Boolean = False

        Private _LastValidationMessage As String = ""
        Private _LastValidationMessageTime As DateTime = Now
        Private _LastValidationMessageCount As Int16 = 0
        
        Public Property DateFormat As String
        Public Property _PermitePegar As Boolean = False


#End Region

#Region " Enumeraciones "

        Enum TipoTextBox
            Normal
            Numerico
            Fecha
        End Enum

#End Region

#Region " Propiedades "

        Property EnabledShortCuts() As Boolean

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

        Property Type As TipoTextBox
            Get
                Return _tipo
            End Get
            Set(ByVal value As TipoTextBox)
                _tipo = value
            End Set
        End Property

        Public Property DisabledTab As Boolean
            Get
                Return _DisabledTab
            End Get
            Set(ByVal value As Boolean)
                _DisabledTab = value
            End Set
        End Property

        Public Property DisabledEnter As Boolean
            Get
                Return _DisabledEnter
            End Get
            Set(ByVal value As Boolean)
                _DisabledEnter = value
            End Set
        End Property

        <TypeConverter(GetType(ExpandableObjectConverter))> _
        Public Property Rango() As Rango
            Get
                Return _Rango
            End Get
            Set(ByVal value As Rango)
                _Rango = value
            End Set
        End Property

        Public ReadOnly Property Fecha As Object
            Get
                If Type = TipoTextBox.Fecha And Me.Text <> "" Then
                    'Me.Text
                    Dim Anio As String
                    Dim Mes As String
                    Dim Dia As String

                    Anio = Me.Text.Replace("/", "").Substring(0, 4)
                    Mes = Me.Text.Replace("/", "").Substring(4, 2)
                    Dia = Me.Text.Replace("/", "").Substring(6, 2)

                    Return CDate(Anio & "/" & Mes & "/" & Dia)

                Else
                    Return Nothing
                End If
            End Get
        End Property

        Public Property MinimumLength As Short
            Get
                Return _MinimumLength
            End Get
            Set(ByVal value As Short)
                _MinimumLength = value
            End Set
        End Property

        Public Property MaximumLength As Short
            Get
                Return _MaximumLength
            End Get
            Set(ByVal value As Short)
                _MaximumLength = value
                Me.MaxLength = _MaximumLength
            End Set
        End Property

        Public Property MaskedTextBox_Property As String
            Get
                Return Me.Mask
            End Get
            Set(ByVal value As String)
                Me.Mask = value
            End Set

        End Property

        Public Property Obligatorio As Boolean
            Get
                Return Me._Obligatorio
            End Get
            Set(value As Boolean)
                Me._Obligatorio = value
            End Set
        End Property

        Public Property Usa_Decimales As Boolean

        Public Property Validos_Cantidad_Puntos As Boolean

        Public Property Caracter_Decimal As Char

        Public Property Cantidad_Decimales As Short

        Public Property permitePegar As Boolean
            Get
                Return _PermitePegar
            End Get
            Set(value As Boolean)
                _PermitePegar = value
            End Set
        End Property

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
                Return _Fk_Validacion
            End Get
            Set(value As Integer)
                _Fk_Validacion = value
            End Set
        End Property

#End Region

#Region " Eventos "

        Private Sub DesktopTextBox_Enter(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Enter
            If (Not Me.ReadOnly And Me.Enabled And Me.Type = TipoTextBox.Numerico) Then
                If (Me.Text = "0.00" Or Me.Text = "0" Or Me.Text = ".00") Then
                    Me.Text = String.Empty
                End If
            End If
        End Sub

        Private Sub DesktopTextBox_HandleCreated(ByVal sender As Object, ByVal e As EventArgs) Handles Me.HandleCreated
            InitializeComponent()
            If Me._tipo = TipoTextBox.Fecha Then
                Me.Width = 75
            ElseIf Me._tipo = TipoTextBox.Numerico Then
                Me.Rango = _Rango
            End If
        End Sub

        Private Sub DesktopTextBox_GotFocus(ByVal sender As Object, ByVal e As EventArgs) Handles Me.GotFocus
            If (Not Me.ReadOnly And Me.Enabled And Me.Type = TipoTextBox.Numerico) Then
                If (Me.Text = "0.00" Or Me.Text = "0" Or Me.Text = ".00") Then
                    Me.Text = String.Empty
                End If
            End If

            If (Not Me.ReadOnly) Then
                Me.BackColor = Me._inFocus
                Me.MoveCursor()
            End If
        End Sub

        Private Sub DesktopTextBox_LostFocus(ByVal sender As Object, ByVal e As EventArgs) Handles Me.LostFocus
            'If (Me.Text = _LastValue) Then Return
            If (Not Me.ReadOnly And Me.Enabled And Type = TipoTextBox.Numerico) Then
                If (Me.Text = "") Then
                    Me.Text = Me.Rango.MinValue
                    Me.SelectAll()
                End If
            End If

            Validar()
        End Sub

        Private Sub DesktopTextBox_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Click
            MoveCursor()
        End Sub

        Private Sub DesktopTextBox_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles Me.KeyDown
            If (e.Control) Then
                If (e.KeyCode = Keys.X) Then
                    e.SuppressKeyPress = True
                    Return
                ElseIf ((e.KeyCode = Keys.C Or e.KeyCode = Keys.V) And Not _PermitePegar) Then
                    e.SuppressKeyPress = True
                    Return
                End If
            ElseIf (e.Shift) Then
                If (e.KeyCode = Keys.Delete Or e.KeyCode = Keys.Insert) Then
                    e.SuppressKeyPress = True
                    Return
                End If
            End If

            If (Me.Text = "") Then
                IsDecimal = False
            End If

            If (Type = TipoTextBox.Numerico) Then
                MoveCursor()

                If (e.KeyCode = Keys.Delete) Then
                    Me.Text = String.Empty
                ElseIf (e.KeyCode = Keys.Multiply Or e.KeyValue = 187) Then
                    e.SuppressKeyPress = True

                    Dim valor = Me.Text.TrimStart("-")

                    If (valor.Length > 0 And Not (valor.StartsWith("0") Or IsDecimal)) Then
                        Me.Text += "000"
                    End If
                ElseIf ((e.KeyValue >= 48 And e.KeyValue <= 57) Or (e.KeyValue >= 96 And e.KeyValue <= 105)) Then
                    If (IsDecimal) Then
                        Dim Parts = Me.Text.Split(DecimalSeparador)
                        Try
                            If (Parts(1).Length >= Cantidad_Decimales) Then
                                e.SuppressKeyPress = True
                            End If
                        Catch ex As Exception

                        End Try

                    ElseIf (Me.Text.StartsWith("0") And Not IsDecimal And (e.KeyValue <> 190 Or e.KeyValue <> 110)) Then
                        e.SuppressKeyPress = True
                    ElseIf (Me.Text.StartsWith("-0") And Not IsDecimal And (e.KeyValue <> 190 Or e.KeyValue <> 110)) Then
                        e.SuppressKeyPress = True
                    End If

                ElseIf ((e.KeyValue = 190 Or e.KeyValue = 110) And Not IsDecimal And Usa_Decimales And Cantidad_Decimales > 0 And Me.Text.Length > 0) Then
                    If (Me.Text.StartsWith("-") And Me.Text.Length = 1) Then
                        e.SuppressKeyPress = True
                    Else
                        IsDecimal = True
                    End If
                ElseIf (e.KeyCode = Keys.Back) Then
                    If (Me.Text.EndsWith(DecimalSeparador.ToString())) Then
                        IsDecimal = False
                    End If
                ElseIf ((e.KeyValue = 189 Or e.KeyCode = Keys.Subtract) And Me.Text.Length = 0) Then
                    e.SuppressKeyPress = False
                ElseIf (e.KeyValue <> Keys.Tab And e.KeyValue <> Keys.Enter) Then
                    e.SuppressKeyPress = True
                End If
            End If
        End Sub

        Private Sub DesktopTextBox_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles Me.KeyPress
            'Validacion numerica
            If Type = TipoTextBox.Numerico Then
                Dim KeyAscii As Short = CShort(Asc(e.KeyChar))
                KeyAscii = CShort(SoloNumeros(KeyAscii))
                If KeyAscii = 0 Then
                    e.Handled = True
                End If

                'validacion de fechas
            ElseIf Type = TipoTextBox.Fecha Then
                Dim KeyAscii As Short = CShort(Asc(e.KeyChar))
                KeyAscii = CShort(SoloFecha(KeyAscii))
                If KeyAscii = 0 Then
                    e.Handled = True
                End If
                Me.MaxLength = 10
            End If

            'Validaciones botones Enter y Tab tienen eventos permitidos.
            If Not DisabledEnter Then
                If e.KeyChar = ChrW(Keys.Enter) Then
                    e.Handled = True
                    SendKeys.Send("{TAB}")
                End If
            End If

            If DisabledTab Then
                If e.KeyChar = ChrW(Keys.Tab) Then
                    e.Handled = False
                End If
            End If

            If Not _Validar Then _Validar = True
        End Sub

        Private Sub DesktopTextBox_Leave(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Leave
            If (Type = TipoTextBox.Numerico) Then
                If (Usa_Decimales) Then
                    Formatear(Me.ReadOnly)
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

        Private Sub DesktopNumericTextBox_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Me.TextChanged
            If Type = TipoTextBox.Numerico Then
                If (Usa_Decimales = True) Then
                    Formatear(False)
                End If
            End If

            If (Me.MaximumLength > 0 AndAlso Me.Text.Length >= Me.MaximumLength) Then
                Me.Text = Me.Text.Substring(0, Me.MaximumLength)

                If (Usa_Decimales = True) Then
                    Formatear(False)
                Else
                    MoveCursor()
                End If
            End If
        End Sub

        Private Sub DesktopTextBox_KeyUp(ByVal sender As Object, ByVal e As KeyEventArgs) Handles Me.KeyUp
            'If Usa_Decimales = True Then
            '    If (e.KeyCode = Keys.Multiply Or e.KeyValue = 187) Then
            '        e.SuppressKeyPress = True
            '        Me.Text += "000"
            '    End If
            '    Me.Text = Puntos(Me.Text)
            '    Me.Select(Me.Text.Length, 0)
            'End If


        End Sub

#End Region

#Region " Metodos "

        Public Sub New()
            Cantidad_Decimales = 2
        End Sub

        Private Sub InitializeComponent()
            Me.SuspendLayout()
            '
            'DesktopTextBox
            '
            Me.ShortcutsEnabled = True
            Me.ResumeLayout(False)
        End Sub

#End Region

#Region " Funciones "

        Public Shared Function TipologiaFomat(ByVal Tipologia As Integer) As String
            Return String.Concat("000", CStr(Tipologia)).Substring(String.Concat("000", CStr(Tipologia)).Length - 3)
        End Function

        'PATCH: Muestra el mismo mensaje de validacion solo una vez, para evitar que se quede en un ciclo infitino mostrando el mismo mensaje y no deja hacer nada mas
        Public Sub ShowMessage(nText As String, nCaption As String, nButtons As MessageBoxButtons, nIcon As MessageBoxIcon)
            If (nText <> _LastValidationMessage) Then _LastValidationMessageCount = 0
            _LastValidationMessageCount = _LastValidationMessageCount + 1

            If (Now.Subtract(_LastValidationMessageTime).Seconds > 5) Then _LastValidationMessageCount = 1
            If (_LastValidationMessageCount > 1) Then Return

            _LastValidationMessage = nText
            _LastValidationMessageTime = Now
            MessageBox.Show(nText, nCaption, nButtons, nIcon)
        End Sub

        Public Function Validar() As Boolean
            'PATCH: Ignora las validaciones si no ocurrio ningun cambio        
            If (Not Me.ReadOnly) Then
                Me.BackColor = Me._outFocus

                'Texto
                Select Case Me.Type
                    Case TipoTextBox.Normal
                        If (Me.MinimumLength <> 0 AndAlso Me.Text.Length < Me.MinimumLength) Then
                            ShowMessage("La longitud mínima es [" & Me.MinimumLength & "]", "Longitud no válida", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Me.SelectAll()
                            Me.Focus()
                            Return False
                        End If

                    Case TipoTextBox.Fecha 'Fechas
                        Dim Formato

                        If (Me.Mask <> "") Then
                            Formato = Me.DateFormat
                        Else
                            Formato = "yyyy/MM/dd"
                        End If

                        Dim FechaLocal As DateTime

                        If (Not DateTime.TryParseExact(Me.Text, Formato, Nothing, DateTimeStyles.None, FechaLocal) And _Obligatorio) Then
                            ShowMessage("Fecha inválida. Por favor verifique el formato. (Formato: " & Formato & ")", "Error en fecha", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Me.SelectAll()
                            Me.Focus()
                            Return False
                        End If

                    Case TipoTextBox.Numerico
                        If _Validar Then
                            If Me.Text.Contains("-") Then
                                Me.Text = Me.Text.Replace("-", "")
                                Me.Text = "-" & Me.Text

                                If Me.Text = "-" Then
                                    Me.Text = ""
                                    Me.SelectAll()
                                    Me.Focus()
                                End If
                            End If

                            'Valida que el valor ingresado corresponda al rango.
                            If (Me.Text <> "") AndAlso (CDbl(Me.Text.Replace(",", "")) < Me.Rango.MinValue Or CDbl(Me.Text.Replace(",", "")) > Me.Rango.MaxValue) Then
                                ShowMessage("No se encuentra en el rango [" + Me.Rango.MinValue.ToString() + "," + Me.Rango.MaxValue.ToString() + "]", "Valor no permitido", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                Me.SelectAll()
                                Me.Focus()
                                _Validar = False
                                Return False
                            End If

                            'Valida la longitud del campo.
                            If (Me.MinimumLength <> 0 AndAlso Me.Text.Length < Me.MinimumLength) Then
                                ShowMessage("La longitud mínima es [" & Me.MinimumLength.ToString() & "]", "Longitud no válida", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                Me.SelectAll()
                                Me.Focus()
                                _Validar = False
                                Return False

                            ElseIf Me.Usa_Decimales Then 'Valida los decimales.
                                If Me.Text.Length > 0 Then
                                    'Si maneja decimales y no se capturaron, se completa en valor automáticamente.
                                    If Me.Text.IndexOf(Me.Caracter_Decimal) = -1 Then
                                        Me.Text = Me.Text & Me.Caracter_Decimal & String.Empty.PadRight(Me.Cantidad_Decimales, "0")
                                    Else
                                        Dim cantidad_puntos, recorrido, total As Integer
                                        Dim conteo, punto As String
                                        punto = "."
                                        cantidad_puntos = Text.Length
                                        For recorrido = 1 To cantidad_puntos
                                            conteo = Mid(Me.Text, recorrido, 1)
                                            If conteo = punto Then
                                                total = total + 1
                                            End If
                                        Next

                                        'El caracter decimal debe esta ubicado en la posición [Me.Cantidad_Decimales]-1, de lo contrario no es un valor válido.
                                        If Me.Text = Me.Caracter_Decimal OrElse ((Me.Text.Length - (Me.Cantidad_Decimales + 1)) < 0) OrElse (Me.Text.Substring(Me.Text.Length - (Me.Cantidad_Decimales + 1), 1)) <> Me.Caracter_Decimal Then
                                            If total > 1 Then
                                                ShowMessage("El valor decimal ingresado no es válido, se debe ingresar [" + Me.Cantidad_Decimales.ToString() + "] valores decimales y Se ha encontrado mas de un indicador decimal recuerde que solo puede utilizar una sola vez el punto para indicar el valor decimal", "Valor inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                                Me.SelectAll()
                                                Me.Focus()
                                                _Validar = False
                                                Validos_Cantidad_Puntos = False
                                                Return False
                                            Else
                                                ShowMessage("El valor decimal ingresado no es válido, se debe ingresar [" + Me.Cantidad_Decimales.ToString() + "] valores decimales.", "Valor inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                                Me.SelectAll()
                                                Me.Focus()
                                                _Validar = False
                                                Validos_Cantidad_Puntos = False
                                                Return False
                                            End If
                                        End If

                                        If total > 1 Then
                                            ShowMessage("Se ha encontrado mas de un indicador decimal recuerde que solo puede utilizar una sola vez el punto para indicar el valor decimal ", "Valor inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                            Me.SelectAll()
                                            Me.Focus()
                                            _Validar = False
                                            Validos_Cantidad_Puntos = False
                                            Return False
                                        End If
                                        Return True

                                    End If
                                    Return False
                                End If
                                Return False
                            End If
                        Else
                            Me.SelectAll()
                            Me.Focus()
                        End If
                        Return False
                End Select

                Return True
            End If

            _LastValidationMessage = ""
            Return True
        End Function

        'Public Shared Function CbarrasSoloFomat(ByVal CBarras As Integer) As String
        '    Return String.Concat("000000000", CStr(CBarras)).Substring(String.Concat("000000000", CStr(CBarras)).Length - 9)
        'End Function

        'Public Shared Function CBarrasFlash(ByVal CBarrasShort As String) As String
        '    Try
        '        Dim Tipologia As String = CBarrasShort.Substring(0, 4)
        '        Dim CBarras As String = CBarrasShort.Substring(4)
        '        Return Tipologia & CbarrasSoloFomat(CInt(CBarras))
        '    Catch ex As Exception
        '        Return CBarrasShort
        '    End Try
        'End Function

        Function SoloNumeros(ByVal Keyascii As Short) As Short
            Dim CadenaCaracteres As String = "1234567890-"
            If Me.Usa_Decimales Then CadenaCaracteres += Me.Caracter_Decimal

            If InStr(CadenaCaracteres, Chr(Keyascii)) = 0 Then
                SoloNumeros = 0
            Else
                SoloNumeros = Keyascii
            End If

            Select Case Keyascii
                Case 8
                    SoloNumeros = Keyascii
                Case 13
                    SoloNumeros = Keyascii
            End Select
        End Function

        Function SoloFecha(ByVal Keyascii As Short) As Short
            If InStr("1234567890/", Chr(Keyascii)) = 0 Then
                SoloFecha = 0
            Else
                SoloFecha = Keyascii
            End If
            Select Case Keyascii
                Case 8
                    SoloFecha = Keyascii
                Case 13
                    SoloFecha = Keyascii
            End Select

        End Function

        Private Sub MoveCursor()
            Me.SelectionStart = Me.Text.Length
            Me.SelectionLength = 0

        End Sub

        Private Sub Formatear(ByVal nForceFormat As Boolean)
            If Not Formating Then
                Formating = True
                Dim IsSigned As Boolean = Me.Text.StartsWith("-")

                Dim Parts = Me.Text.TrimStart("-").Replace(ThousandSeparador.ToString(), "").Split(DecimalSeparador)

                Dim FormatedText = String.Empty

                For i As Integer = Parts(0).Length - 1 To 0 Step -1
                    If FormatedText.Length >= 3 And (Parts(0).Length - 1 - i) Mod 3 = 0 Then
                        FormatedText = ThousandSeparador + FormatedText
                    End If

                    FormatedText = Parts(0)(i) + FormatedText
                Next

                If (Parts.Length > 1) Then
                    If (nForceFormat) Then
                        FormatedText += DecimalSeparador + Parts(1).PadRight(Cantidad_Decimales, "0").Substring(0, Cantidad_Decimales)
                    ElseIf (Parts(1).Length > Cantidad_Decimales) Then
                        FormatedText += DecimalSeparador + Parts(1).Substring(0, Cantidad_Decimales)
                    Else
                        FormatedText += DecimalSeparador + Parts(1)
                    End If
                ElseIf (nForceFormat) Then
                    If (FormatedText = "") Then
                        FormatedText = "0" + DecimalSeparador.ToString().PadRight(Cantidad_Decimales + 1, "0")
                    Else
                        FormatedText += DecimalSeparador.ToString().PadRight(Cantidad_Decimales + 1, "0")
                    End If
                End If

                If (IsSigned) Then
                    FormatedText = "-" + FormatedText
                End If

                Me.Text = FormatedText
                MoveCursor()
                Formating = False

            End If
        End Sub

        Public Function getValues(ByVal Valor As String) As String

            Valor = Valor.Replace(",", "")
            Return Valor

        End Function

#End Region

    End Class

#Region " Class Rango "

    Public Class Rango

        Private _MinValue As Double
        Private _MaxValue As Double

        Public Sub New()
            _MinValue = 0
            _MaxValue = Double.MaxValue
        End Sub

        Public Sub New(ByVal nMin As Double, ByVal nMax As Double)
            _MinValue = nMin
            _MaxValue = nMax
        End Sub

        Property MinValue() As Double
            Get
                Return _MinValue
            End Get
            Set(ByVal value As Double)
                _MinValue = value
            End Set
        End Property

        Property MaxValue() As Double
            Get
                Return _MaxValue
            End Get
            Set(ByVal value As Double)
                _MaxValue = value
            End Set
        End Property

    End Class

#End Region

End Namespace