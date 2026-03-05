Imports System.Windows.Forms
Imports Miharu.Desktop.Library.Config

Namespace View.Indexacion

    Public Class TextInputControl
        Inherits InputControl
        Implements IInputControl

#Region " Declaraciones "

        Private _definicionCaptura As List(Of DefinicionCaptura)

        Private _validar As Boolean = True

#End Region

#Region " Constructores "

        Public Sub New()
            ' Llamada necesaria para el diseñador.
            InitializeComponent()

            ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
            _DefinicionCaptura = New List(Of DefinicionCaptura)

            Me.UsaTrigger = False
            Me.TriggerValues = New List(Of KeyValueItem)
        End Sub

#End Region

#Region " Implementación IIndexerControl "

        Public Overloads Property Etiqueta As String Implements IInputControl.Etiqueta
            Get
                Return MyBase.Etiqueta
            End Get
            Set(ByVal value As String)
                MyBase.Etiqueta = value
            End Set
        End Property

        Public Property Value As Object Implements IInputControl.Value
            Get
                Return ValueDesktopTextBox.Text.Trim(Chr(31))
            End Get
            Set(ByVal value As Object)
                ValueDesktopTextBox.Text = CStr(value)
            End Set
        End Property

        Public Property ValorValidacion As Object Implements IInputControl.ValorValidacion
            Get
                Return ValueDesktopTextBox.Text.Trim(Chr(31))
            End Get
            Set(ByVal value As Object)
                ValueDesktopTextBox.Text = CStr(value)
            End Set
        End Property

        Public Property ValueControl As Boolean Implements IInputControl.ValueControl

        Public Property ÏsOCRCapture As Boolean Implements IInputControl.ÏsOCRCapture

        Public Property EnableTableOCR As Boolean Implements IInputControl.EnableTableOCR


        Public Property ValueValidacionListas As Object Implements IInputControl.ValueValidacionListas
            Get
                Return ValueValidacionListasDesktopTextBox.Text.Trim(Chr(31))
            End Get
            Set(ByVal value As Object)
                ValueValidacionListasDesktopTextBox.Text = CStr(value)
            End Set
        End Property

        Public Property ValueOld1 As Object Implements IInputControl.ValueOld1
            Get
                Return ValueOld1DesktopTextBox.Text
            End Get
            Set(ByVal value As Object)
                ValueOld1DesktopTextBox.Text = CStr(value)
            End Set
        End Property

        Public Property ValueOld2 As Object Implements IInputControl.ValueOld2
            Get
                Return ValueOld2DesktopTextBox.Text
            End Get
            Set(ByVal value As Object)
                ValueOld2DesktopTextBox.Text = CStr(value)
            End Set
        End Property

        Public Property ValueOld3 As Object Implements IInputControl.ValueOld3

        Public Property NextControl As Windows.Forms.Control Implements IInputControl.NextControl

        Public Property ShowSecondControls As Boolean Implements IInputControl.ShowSecondControls
            Get
                Return CapturaOldPanel.Visible
            End Get
            Set(ByVal value As Boolean)
                CapturaOldPanel.Visible = value
            End Set
        End Property

        Public Property CampoCaptura As CampoCaptura Implements IInputControl.CampoCaptura

        Public Property CampoLlaveCaptura As CampoLlaveCaptura Implements IInputControl.CampoLlaveCaptura

        Public ReadOnly Property DefinicionCaptura As List(Of DefinicionCaptura) Implements IInputControl.DefinicionCaptura
            Get
                Return _definicionCaptura
            End Get
        End Property

        Public Property IndexerView As IIndexerView Implements IInputControl.IndexerView

        Public Property Tipo As DesktopConfig.CampoTipo Implements IInputControl.Tipo

        Public Property UsaTrigger As Boolean Implements IInputControl.UsaTrigger

        Public Property TriggerValues As List(Of KeyValueItem) Implements IInputControl.TriggerValues

        Public Property TriggerValidaciones As List(Of TriggersValidations_Items) Implements IInputControl.TriggerValidaciones

        Public Property IsVisible As Boolean Implements IInputControl.IsVisible
            Get
                Return Me.Visible
            End Get
            Set(ByVal value As Boolean)
                Me.Visible = value
            End Set
        End Property

        Public Function Validar() As Boolean Implements IInputControl.Validar
            If (Me.Visible AndAlso DefinicionCaptura.Item(0).Es_Obligatorio_Campo AndAlso Me.ValueDesktopTextBox.IsEmpty) Then
                MessageBox.Show("El campo " & Me.Etiqueta & " es obligatorio", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Warning)

                Me.ValueDesktopTextBox.SelectAll()
                Me.ValueDesktopTextBox.Focus()
                Return False
            Else
                Return Me.ValueDesktopTextBox.Validar()
            End If
        End Function

        Public Sub LoadDefinition(ByVal nDefinicionCaptura As List(Of DefinicionCaptura)) Implements IInputControl.LoadDefinition
            Me._DefinicionCaptura = nDefinicionCaptura

            Me.ValueDesktopTextBox.Mask = Me._DefinicionCaptura.Item(0).Mascara
            Me.ValueDesktopTextBox.MaxLength = Me._DefinicionCaptura.Item(0).MaximumLength
            Me.ValueDesktopTextBox.MinLength = Me._definicionCaptura.Item(0).MinimumLength
            Me.ValueDesktopTextBox.Formato = Me._definicionCaptura.Item(0).FormatoFecha
            Me.ValueDesktopTextBox.NombreCampo = Me._definicionCaptura.Item(0).Caption
        End Sub

        Public Property IsCalidad As Boolean Implements IInputControl.IsCalidad

        Public ReadOnly Property RequiereAutorizacion As Boolean Implements IInputControl.RequiereAutorizacion
            Get
                Return (Me.Value.ToString() <> Me.ValueOld1.ToString() _
                    AndAlso Me.Value.ToString() <> Me.ValueOld2.ToString() _
                    AndAlso Me.Value.ToString() <> Me.ValueOld3.ToString())
            End Get
        End Property

        Public Property ShowValidacionListasControls As Boolean Implements IInputControl.ShowValidacionListasControls
            Get
                Return CapturaValidacionListasPanel.Visible
            End Get
            Set(ByVal value As Boolean)
                CapturaValidacionListasPanel.Visible = value
            End Set
        End Property

        Public Property ShowPrimaryControls As Boolean Implements IInputControl.ShowPrimaryControls
            Get
                Return ValueDesktopTextBox.Visible
            End Get
            Set(ByVal value As Boolean)
                ValueDesktopTextBox.Visible = value
            End Set
        End Property

#End Region

#Region " Eventos "

        Private Sub ValueDesktopTextBox_Enter(ByVal sender As System.Object, ByVal e As EventArgs) Handles ValueDesktopTextBox.Enter
            Me.IndexerView.SelectedInputControl = Me
        End Sub

        Private Sub ValueDesktopTextBox_Leave(ByVal sender As System.Object, ByVal e As EventArgs) Handles ValueDesktopTextBox.Leave
            If (Me.IsCalidad) Then ValidarCalidad()
            Me.IndexerView.SelectedInputControl = Nothing
            If (Me.UsaTrigger) Then
                Trigger_Validaciones()
            End If
        End Sub

        Private Sub TextInputControl_Enter(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Enter
            ValueDesktopTextBox.Focus()
        End Sub

        Private Sub ValueDesktopTextBox_KeyUp(ByVal sender As System.Object, ByVal e As KeyEventArgs) Handles ValueDesktopTextBox.KeyUp

            If (e.KeyCode = Keys.Enter) Then

                If (Me.UsaTrigger) Then
                    Trigger_Validaciones()
                End If

                If (Me.NextControl IsNot Nothing) Then
                    Dim visibleNextControl As Control = Me.NextControl
                    Dim contador As Integer = 0

                    While Not visibleNextControl.Visible And contador < 1000
                        If (visibleNextControl.GetType().BaseType = GetType(InputControl)) Then
                            visibleNextControl = CType(visibleNextControl, IInputControl).NextControl
                        End If
                        contador += 1
                    End While

                    visibleNextControl.Focus()
                End If
            End If
        End Sub

        Private Sub ValueDesktopTextBox_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles ValueDesktopTextBox.KeyPress
            If Not _Validar Then _Validar = True
        End Sub

#End Region

#Region " Metodos "

        Private Sub Trigger_Validaciones()

            ' Mostrar todas las validaciones afectadas 
            Dim OperadorValidacion As String = ""

            For Each TriggerValue In TriggerValidaciones
                For Each Validacion In Me.IndexerView.Validaciones
                    If (TriggerValue._idValidacionOcultar = Validacion.id) Then
                        Validacion.Control.Visible = True
                    End If
                Next
            Next

            ' Ocultar los controles de validaciones configurados
            For Each TriggerValue In TriggerValidaciones
                OperadorValidacion = TriggerValue._OperadorValidacion
                For Each Validacion In Me.IndexerView.Validaciones
                    If (TriggerValue._idValidacionOcultar = Validacion.id) Then
                        Select Case OperadorValidacion
                            Case "="
                                If (Me.Value.ToString() = TriggerValue._Valor) Then
                                    'oculta control validacion
                                    Validacion.Control.Visible = False
                                End If
                            Case "<"
                                If (Me.Value.ToString() < TriggerValue._Valor) Then
                                    'oculta control validacion
                                    Validacion.Control.Visible = False
                                End If
                            Case ">"
                                If (Me.Value.ToString() > TriggerValue._Valor) Then
                                    'oculta control validacion
                                    Validacion.Control.Visible = False
                                End If
                            Case "<="
                                If (Me.Value.ToString() <= TriggerValue._Valor) Then
                                    'oculta control validacion
                                    Validacion.Control.Visible = False
                                End If
                            Case ">="
                                If (Me.Value.ToString() >= TriggerValue._Valor) Then
                                    'oculta control validacion
                                    Validacion.Control.Visible = False
                                End If
                        End Select

                    End If
                Next
            Next
        End Sub

#End Region

#Region " Funciones "

        Private Sub ValidarCalidad()
            If (Me.RequiereAutorizacion) Then
                Me.ValueDesktopTextBox.ForeColor = Drawing.Color.Red
            Else
                Me.ValueDesktopTextBox.ForeColor = Drawing.Color.Black
            End If
        End Sub

#End Region

    End Class

End Namespace
