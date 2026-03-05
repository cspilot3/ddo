Imports System.Windows.Forms

Namespace Validation

    Public Class ValidationControl

#Region " Constructor "

        Public Sub New()

            ' This call is required by the designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            ValidacionDesktopComboBox.AutoCompleteMode = AutoCompleteMode.None

            ValidacionDesktopComboBox.ValueMember = "Value"
            ValidacionDesktopComboBox.DisplayMember = "Text"

            ValidacionDesktopComboBox.Items.Add(New ListItem("-1", "..."))
            ValidacionDesktopComboBox.Items.Add(New ListItem("1", "Si"))
            ValidacionDesktopComboBox.Items.Add(New ListItem("0", "No"))
            ValidacionDesktopComboBox.SelectedIndex = 0
        End Sub

#End Region

#Region " Propiedades "

        Public Property UsaMotivo As Boolean
            Get
                Return MotivoDesktopComboBox.Visible
            End Get
            Set(ByVal valor As Boolean)
                MotivoDesktopComboBox.Visible = Not valor
            End Set
        End Property

        Public Property DataSource As Object
            Get
                Return MotivoDesktopComboBox.DataSource
            End Get
            Set(ByVal valor As Object)
                MotivoDesktopComboBox.DataSource = valor
            End Set
        End Property

        Public Property ValueMemberMotivo As String
            Get
                Return MotivoDesktopComboBox.ValueMember
            End Get
            Set(ByVal valor As String)
                MotivoDesktopComboBox.ValueMember = valor
            End Set
        End Property

        Public Property DisplayMemberMotivo As String
            Get
                Return MotivoDesktopComboBox.DisplayMember
            End Get
            Set(ByVal valor As String)
                MotivoDesktopComboBox.DisplayMember = valor
            End Set
        End Property

        Public Property Etiqueta As String
            Get
                Return ValidacionLabel.Text
            End Get
            Set(ByVal valor As String)
                ValidacionLabel.Text = valor
                Me.ToolTip.SetToolTip(Me, valor)
            End Set
        End Property

        Public Property Value As Boolean
            Get
                Return (CType(Me.ValidacionDesktopComboBox.SelectedItem, ListItem).Value = "1")
            End Get
            Set(ByVal valor As Boolean)
                ValidacionDesktopComboBox.SelectedValue = CInt(IIf(valor, 1, 0))
            End Set
        End Property

        Public ReadOnly Property ValueMember As Integer
            Get
                Return CInt(CType(Me.ValidacionDesktopComboBox.SelectedItem, ListItem).Value)
            End Get
        End Property

        Public ReadOnly Property Motivo As String
            Get
                Return CStr(MotivoDesktopComboBox.SelectedValue)
            End Get
        End Property

        Public Property NombreControl As String
            Get
                Return Me.Name
            End Get
            Set(ByVal valor As String)
                Me.Name = valor
            End Set
        End Property

        Public Property NextControl As Control

        Public Property CampoCaptura As ValidacionCaptura

        'Public Property IndexerView As IIndexerView

        Public Property BackColorCombo As Drawing.Color
            Get
                Return Me.ValidacionDesktopComboBox.BackColor
            End Get
            Set(ByVal valor As Drawing.Color)
                Me.ValidacionDesktopComboBox.BackColor = valor
            End Set
        End Property

#End Region

#Region " Eventos "

        Private Sub ValidationControl_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            ValidacionDesktopComboBox.DisabledEnter = True

            If Not IsNothing(ValidacionDesktopComboBox.SelectedValue) Then
                MotivoDesktopComboBox.Visible = Not CBool(IIf(ValidacionDesktopComboBox.SelectedValue.ToString() = "1", True, False))
            Else
                MotivoDesktopComboBox.Visible = False
            End If
        End Sub

        Private Sub TextInputControl_Enter(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Enter
            ValidacionDesktopComboBox.Focus()
        End Sub

        Private Sub ValidacionDesktopComboBox_KeyDown(ByVal sender As System.Object, ByVal e As KeyEventArgs) Handles ValidacionDesktopComboBox.KeyDown

            If (e.KeyCode = Keys.Enter) Then
                If (Me.NextControl IsNot Nothing) Then Me.NextControl.Focus()

            ElseIf e.KeyCode = Keys.NumPad1 Or e.KeyCode = Keys.D1 Or e.KeyCode = Keys.S Then
                CType(sender, DesktopComboBox.DesktopComboBoxControl).SelectedIndex = 1
                CType(sender, DesktopComboBox.DesktopComboBoxControl).Select()

                If (Me.NextControl IsNot Nothing) Then Me.NextControl.Focus()

            ElseIf e.KeyCode = Keys.NumPad2 Or e.KeyCode = Keys.D2 Or e.KeyCode = Keys.N Then
                CType(sender, DesktopComboBox.DesktopComboBoxControl).SelectedIndex = 2
                CType(sender, DesktopComboBox.DesktopComboBoxControl).Select()
                If (Me.NextControl IsNot Nothing) Then Me.NextControl.Focus()
            End If

        End Sub

        Private Sub ValidacionDesktopComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles ValidacionDesktopComboBox.SelectedIndexChanged
            If Not IsNothing(ValidacionDesktopComboBox.SelectedValue) Then
                Dim Valor = CBool(IIf(ValidacionDesktopComboBox.SelectedValue.ToString() = "1", True, False))
                MotivoDesktopComboBox.Visible = Not Valor
                If Valor = True Then MotivoDesktopComboBox.SelectedIndex = 0
            Else
                MotivoDesktopComboBox.Visible = False
            End If
        End Sub

#End Region

#Region " Funciones "

        Public Function Validar() As Boolean
            Dim valida As Boolean = True

            If CInt(CType(Me.ValidacionDesktopComboBox.SelectedItem, ListItem).Value) = -1 Then
                MessageBox.Show("Debe seleccionar un valor para la validación " & Etiqueta, "Valor Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                ValidacionDesktopComboBox.Focus()
                valida = False
            End If

            If UsaMotivo And Value = False Then
                If CStr(MotivoDesktopComboBox.SelectedValue) = "-1" Or CStr(MotivoDesktopComboBox.SelectedValue) = "" Then
                    MessageBox.Show("El motivo del campo [" & Me.Etiqueta & "] es obligatorio cuando la seleccion es No.", "Valor Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    valida = False
                End If
            End If

            Return valida
        End Function

#End Region

    End Class

#Region "Clase Auxiliar"

    Class ListItem
        Private m_value As String
        Public Property Value() As String
            Get
                Return m_value
            End Get
            Set(ByVal valor As String)
                m_value = valor
            End Set
        End Property

        Private m_text As String
        Public Property Text() As String
            Get
                Return m_text
            End Get
            Set(ByVal valor As String)
                m_text = valor
            End Set
        End Property

        Public Sub New(ByVal valor As String, ByVal text As String)
            m_value = valor
            m_text = text
        End Sub
    End Class

#End Region

End Namespace