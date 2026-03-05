Imports System.Text
Imports System.Windows.Forms

Namespace DesktopValidacion

    Public Class DesktopValidacionControl

#Region " Propiedades "

        Public Property UsaMotivo As Boolean
            Get
                Return MotivoComboBox.Visible
            End Get
            Set(ByVal valor As Boolean)
                MotivoComboBox.Visible = Not valor
            End Set
        End Property

        Public Property DataSource As Object
            Get
                Return MotivoComboBox.DataSource
            End Get
            Set(ByVal valor As Object)
                MotivoComboBox.DataSource = valor
            End Set
        End Property

        Public Property ValueMember As String
            Get
                Return MotivoComboBox.ValueMember
            End Get
            Set(ByVal valor As String)
                MotivoComboBox.ValueMember = valor
            End Set
        End Property

        Public Property DisplayMember As String
            Get
                Return MotivoComboBox.DisplayMember
            End Get
            Set(ByVal valor As String)
                MotivoComboBox.DisplayMember = valor
            End Set
        End Property

        Public Property Caption As String
            Get
                Return CaptionLabel.Text
            End Get
            Set(ByVal valor As String)
                CaptionLabel.Text = valor
            End Set
        End Property

        Public Property Value As Boolean
            Get
                Dim Valor As Boolean
                Valor = CBool(IIf(SeleccionComboBox.SelectedValue.ToString() = 0, True, False))
                Return Valor
            End Get
            Set(ByVal valor As Boolean)
                SeleccionComboBox.SelectedValue = IIf(valor, "SI", "NO")
            End Set
        End Property

        Public ReadOnly Property Motivo As String
            Get
                Return CStr(MotivoComboBox.SelectedValue)
            End Get
        End Property

        Public Property Id As Short

        Public Property EsObligatorio As Boolean = False

        Public Property CadenaError As StringBuilder

        Property DisabledEnter As Boolean

#End Region

#Region " Constructor "

        Public Sub New()
            ' This call is required by the designer.
            InitializeComponent()

            CadenaError = New StringBuilder

            ' Add any initialization after the InitializeComponent() call.
            Dim Data As New DataTable
            Data.Columns.Add("value", GetType(Short))
            Data.Columns.Add("text", GetType(String))
            Dim Row = Data.NewRow() : Row("value") = -1 : Row("text") = "-" : Data.Rows.Add(Row)
            Row = Data.NewRow() : Row("value") = 0 : Row("text") = "No" : Data.Rows.Add(Row)
            Row = Data.NewRow() : Row("value") = 1 : Row("text") = "Si" : Data.Rows.Add(Row)
            SeleccionComboBox.DataSource = Data
            SeleccionComboBox.ValueMember = "value"
            SeleccionComboBox.DisplayMember = "text"
        End Sub

#End Region

#Region " Funciones "

        Public Function Validar() As Boolean
            Dim Valida As Boolean = True
            CadenaError.Clear()

            If CStr(SeleccionComboBox.SelectedValue) = "-1" Or CStr(SeleccionComboBox.SelectedValue) = "" Then
                Valida = False
                CadenaError.AppendLine("Debe seleccionar una opcion en el campo [" & Me.Caption & "].")
            End If

            If UsaMotivo And Value = False Then
                If CStr(MotivoComboBox.SelectedValue) = "-1" Or CStr(MotivoComboBox.SelectedValue) = "" Then
                    Valida = False
                    CadenaError.AppendLine("El motivo del campo [" & Me.Caption & "] es obligatorio cuando la seleccion es NO.")
                End If
            End If

            Return Valida
        End Function

#End Region

#Region " Eventos "

        Private Sub DesktopValidacion_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles Me.KeyDown
            If (Not DisabledEnter) Then
                If e.KeyCode = Keys.Enter Then
                    e.Handled = True
                    SendKeys.Send("{TAB}")
                End If
            End If
        End Sub

        Private Sub SeleccionComboBox_SelectedValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles SeleccionComboBox.SelectedValueChanged
            MotivoComboBox.Visible = Not Value
        End Sub

        Private Sub DesktopValidacion_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            MotivoComboBox.Visible = Not Value
            MotivoComboBox.SelectedIndex = 0
        End Sub

#End Region

    End Class

End Namespace