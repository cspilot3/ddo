Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopComboBox
Imports Miharu.Desktop.Library.Config

Namespace View.Indexacion

    Public Class ValidationControl
        Inherits InputControl
        Implements IInputControl

#Region " Constructor "

        Public Sub New()

            ' This call is required by the designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            ValidacionDesktopComboBox.AutoCompleteMode = AutoCompleteMode.None

            ValidacionDesktopComboBox.ValueMember = "Value"
            ValidacionDesktopComboBox.DisplayMember = "Text"

            ValidacionDesktopComboBox.Items.Add(New Utilities.ListItem("-1", "..."))
            ValidacionDesktopComboBox.Items.Add(New Utilities.ListItem("1", "Si"))
            ValidacionDesktopComboBox.Items.Add(New Utilities.ListItem("0", "No"))
            ValidacionDesktopComboBox.SelectedIndex = 0
        End Sub

#End Region

#Region " Declaraciones "

        Private _UsaMotivo As Boolean = False
        Private _EsObligatorio As Boolean = False
        Private _definicionCaptura As List(Of DefinicionCaptura)

#End Region

#Region " Propiedades "

        Public Property UsaMotivo As Boolean
            Get
                Return _UsaMotivo
            End Get
            Set(ByVal value As Boolean)
                _UsaMotivo = value
            End Set
        End Property

        Public Property EsObligatorio As Boolean
            Get
                Return _EsObligatorio
            End Get
            Set(ByVal value As Boolean)
                _EsObligatorio = value
            End Set
        End Property

        Public Property DataSource As Object
            Get
                Return MotivoDesktopComboBox.DataSource
            End Get
            Set(ByVal value As Object)
                MotivoDesktopComboBox.DataSource = value
            End Set
        End Property

        Public Property ValueMember As String
            Get
                Return MotivoDesktopComboBox.ValueMember
            End Get
            Set(ByVal value As String)
                MotivoDesktopComboBox.ValueMember = value
            End Set
        End Property

        Public Property DisplayMember As String
            Get
                Return MotivoDesktopComboBox.DisplayMember
            End Get
            Set(ByVal value As String)
                MotivoDesktopComboBox.DisplayMember = value
            End Set
        End Property


        Public Overloads Property Etiqueta As String Implements IInputControl.Etiqueta
            Get
                Return ValidacionLabel.Text
            End Get
            Set(ByVal value As String)
                ValidacionLabel.Text = value
                Me.ToolTip.SetToolTip(Me, value)
            End Set
        End Property

        Public Property Value As Object Implements IInputControl.Value

        Public Property ValueValidacionListas As Object Implements IInputControl.ValueValidacionListas

        Public Property ValueControl As Boolean Implements IInputControl.ValueControl
            Get
                Return (CType(Me.ValidacionDesktopComboBox.SelectedItem, Utilities.ListItem).Value = "1")
            End Get
            Set(ByVal value As Boolean)
                ValidacionDesktopComboBox.SelectedValue = CInt(IIf(value, 1, 0))
            End Set
        End Property

        Public Property ÏsOCRCapture As Boolean Implements IInputControl.ÏsOCRCapture

        Public Property EnableTableOCR As Boolean Implements IInputControl.EnableTableOCR

        Public ReadOnly Property Motivo As String
            Get
                Try
                    Return CStr(MotivoDesktopComboBox.SelectedValue)
                Catch ex As Exception
                    Return ""
                End Try
            End Get
        End Property

        Public Property NextControl As Windows.Forms.Control Implements IInputControl.NextControl

        Public Property ValidacionCaptura As ValidacionCaptura

        Public Property IndexerView As IIndexerView Implements IInputControl.IndexerView

        Public Property ValueOld1 As Object Implements IInputControl.ValueOld1

        Public Property ValueOld2 As Object Implements IInputControl.ValueOld2

        Public Property ValueOld3 As Object Implements IInputControl.ValueOld3

        Public Property Tipo As DesktopConfig.CampoTipo Implements IInputControl.Tipo

        Public ReadOnly Property RequiereAutorizacion As Boolean Implements IInputControl.RequiereAutorizacion
            Get
                Return (Me.Value.ToString() <> Me.ValueOld1.ToString() _
                    AndAlso Me.Value.ToString() <> Me.ValueOld2.ToString() _
                    AndAlso Me.Value.ToString() <> Me.ValueOld3.ToString())
            End Get
        End Property

        Public Property ShowSecondControls As Boolean Implements IInputControl.ShowSecondControls

        Public Property CampoCaptura As CampoCaptura Implements IInputControl.CampoCaptura

        Public Property CampoLlaveCaptura As CampoLlaveCaptura Implements IInputControl.CampoLlaveCaptura

        Public Property TriggerValidaciones As List(Of TriggersValidations_Items) Implements IInputControl.TriggerValidaciones


        Public Property IsVisible As Boolean Implements IInputControl.IsVisible
            Get
                Return Me.Visible
            End Get
            Set(ByVal value As Boolean)
                Me.Visible = value
            End Set
        End Property

        Public ReadOnly Property DefinicionCaptura As List(Of DefinicionCaptura) Implements IInputControl.DefinicionCaptura
            Get
                Return _definicionCaptura
            End Get
        End Property

        Public Property IsCalidad As Boolean Implements IInputControl.IsCalidad

        Public Property UsaTrigger As Boolean Implements IInputControl.UsaTrigger

        Public Property TriggerValues As List(Of KeyValueItem) Implements IInputControl.TriggerValues

        Public Sub LoadDefinition(ByVal nDefinicionCaptura As List(Of DefinicionCaptura)) Implements IInputControl.LoadDefinition
            Me._DefinicionCaptura = nDefinicionCaptura

        End Sub

        Public Property ValorValidacion As Object Implements IInputControl.ValorValidacion

        Public Property ShowValidacionListasControls As Boolean Implements IInputControl.ShowValidacionListasControls
            Get
                Return True
            End Get
            Set(ByVal value As Boolean)
                Me.Visible = value
            End Set
        End Property

        Public Property ShowPrimaryControls As Boolean Implements IInputControl.ShowPrimaryControls
            Get
                Return True
            End Get
            Set(ByVal value As Boolean)
                Me.Visible = value
            End Set
        End Property

#End Region

#Region " Eventos "

        Private Sub ValidationControl_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            ValidacionDesktopComboBox.DisabledEnter = True
            MotivoDesktopComboBox.DisabledEnter = True

            If Not IsNothing(ValidacionDesktopComboBox.SelectedValue) Then
                Try
                    MotivoDesktopComboBox.Visible = Not CBool(IIf(ValidacionDesktopComboBox.SelectedValue.ToString() = "1", True, False))
                Catch : End Try
            Else
                Try
                    MotivoDesktopComboBox.Visible = False
                Catch : End Try
            End If
        End Sub

        Private Sub TextInputControl_Enter(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Enter
            If MotivoDesktopComboBox.Visible Then
                MotivoDesktopComboBox.Focus()
            Else
                ValidacionDesktopComboBox.Focus()
            End If

            IndexerView.HideGridControl()
        End Sub

        Private Sub ValidacionDesktopComboBox_Enter(ByVal sender As Object, ByVal e As EventArgs) Handles ValidacionDesktopComboBox.Enter
            Me.IndexerView.SelectedValidationControl = Me
        End Sub

        Private Sub ValidacionDesktopComboBox_Leave(ByVal sender As Object, ByVal e As EventArgs) Handles ValidacionDesktopComboBox.Leave
            If MotivoDesktopComboBox.Visible Then
                MotivoDesktopComboBox.Focus()
            End If

            Me.IndexerView.SelectedValidationControl = Nothing
        End Sub

        Private Sub ValidacionDesktopComboBox_KeyUp(ByVal sender As System.Object, ByVal e As KeyEventArgs) Handles ValidacionDesktopComboBox.KeyUp
            If (e.KeyCode = Keys.Enter) Then
                If MotivoDesktopComboBox.Visible Then
                    MotivoDesktopComboBox.Focus()
                Else
                    If (Me.NextControl IsNot Nothing) Then Me.NextControl.Focus()
                End If

            ElseIf e.KeyCode = Keys.NumPad1 Or e.KeyCode = Keys.D1 Or e.KeyCode = Keys.S Then
                CType(sender, DesktopComboBoxControl).SelectedIndex = 1
                CType(sender, DesktopComboBoxControl).Select()

                If MotivoDesktopComboBox.Visible Then
                    MotivoDesktopComboBox.Focus()
                Else
                    If (Me.NextControl IsNot Nothing) Then Me.NextControl.Focus()
                End If

            ElseIf e.KeyCode = Keys.NumPad2 Or e.KeyCode = Keys.D2 Or e.KeyCode = Keys.N Then
                CType(sender, DesktopComboBoxControl).SelectedIndex = 2
                CType(sender, DesktopComboBoxControl).Select()

                If MotivoDesktopComboBox.Visible Then
                    MotivoDesktopComboBox.Focus()
                Else
                    If (Me.NextControl IsNot Nothing) Then Me.NextControl.Focus()
                End If
            End If

        End Sub

        Private Sub MotivoDesktopComboBox_KeyUp(ByVal sender As System.Object, ByVal e As KeyEventArgs) Handles MotivoDesktopComboBox.KeyUp
            If (e.KeyCode = Keys.Enter) Then
                If (Me.NextControl IsNot Nothing) Then Me.NextControl.Focus()
            End If
        End Sub

        Private Sub ValidacionDesktopComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles ValidacionDesktopComboBox.SelectedIndexChanged
            Try
                If UsaMotivo Then
                    Dim Valor = CBool(IIf(ValidacionDesktopComboBox.Text.ToUpper() = "SI", True, False))
                    MotivoDesktopComboBox.Visible = Not Valor
                    MotivoLabel.Visible = Not Valor
                    MotivoDesktopComboBox.SelectedIndex = 0

                    If MotivoDesktopComboBox.Visible Then
                        Me.Height = 60
                    Else
                        Me.Height = 30
                    End If
                Else
                    MotivoLabel.Visible = False
                    MotivoDesktopComboBox.Visible = False
                    Me.Height = 30
                End If
            Catch
                MotivoLabel.Visible = False
                Me.Height = 30
                MotivoDesktopComboBox.Visible = False
            End Try
        End Sub

#End Region

#Region " Funciones "

        Public Function Validar() As Boolean Implements IInputControl.Validar
            Dim valida As Boolean = True

            If CInt(CType(Me.ValidacionDesktopComboBox.SelectedItem, Utilities.ListItem).Value) = -1 And EsObligatorio And IsVisible Then
                MessageBox.Show("Debe seleccionar un valor para la validación " & Etiqueta, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                ValidacionDesktopComboBox.Focus()
                valida = False
            End If

            If UsaMotivo And ValueControl() = False Then
                Try
                    If CStr(MotivoDesktopComboBox.SelectedValue) = "-1" Or CStr(MotivoDesktopComboBox.SelectedValue) = "" Then
                        MessageBox.Show("El motivo del campo [" & Me.Etiqueta & "] es obligatorio cuando la seleccion es No.", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        valida = False
                    End If
                Catch : End Try
            End If

            Return valida
        End Function

#End Region

    End Class

End Namespace