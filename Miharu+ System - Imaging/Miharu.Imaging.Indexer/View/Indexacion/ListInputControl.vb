Imports System.Windows.Forms
Imports Miharu.Desktop.Library.Config

Namespace View.Indexacion

    Public Class ListInputControl
        Inherits InputControl
        Implements IInputControl

#Region " Declaraciones "

        Private _tipo As DesktopConfig.CampoTipo = DesktopConfig.CampoTipo.Lista
        Private _definicionCaptura As List(Of DefinicionCaptura)

#End Region

#Region " Constructores "

        Public Sub New()
            ' Llamada necesaria para el diseñador.
            InitializeComponent()

            ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
            _DefinicionCaptura = New List(Of DefinicionCaptura)

            Me.UsaTrigger = False
            Me.TriggerValues = New List(Of KeyValueItem)
            Me.TriggerValidaciones = New List(Of TriggersValidations_Items)
        End Sub

#End Region

#Region " Implementación IIndexerControl "

        Public Overloads Property Etiqueta As String Implements IInputControl.Etiqueta
            Get
                Return MyBase.Etiqueta
            End Get
            Set(ByVal valor As String)
                MyBase.Etiqueta = valor
            End Set
        End Property

        Public Property Value As Object Implements IInputControl.Value
            Get
                If (ValueDesktopComboBox.SelectedIndex >= 0) Then
                    If (_tipo = DesktopConfig.CampoTipo.SiNo) Then
                        Return (ValueDesktopComboBox.SelectedIndex = 0)
                    Else
                        Return ValueDesktopComboBox.SelectedValue
                    End If
                Else
                    Return ""
                End If
            End Get
            Set(ByVal valor As Object)
                If (_tipo = DesktopConfig.CampoTipo.SiNo) Then
                    If (CBool(valor)) Then
                        ValueDesktopComboBox.SelectedIndex = 0
                    Else
                        ValueDesktopComboBox.SelectedIndex = 1
                    End If
                Else
                    ValueDesktopComboBox.SelectedValue = valor
                End If
            End Set
        End Property

        Public Property ValorValidacion As Object Implements IInputControl.ValorValidacion
            Get
                If (ValueDesktopComboBox.SelectedIndex >= 0) Then
                    If (_Tipo = DesktopConfig.CampoTipo.SiNo) Then
                        Return (ValueDesktopComboBox.SelectedIndex = 0)
                    Else
                        Return ValueDesktopComboBox.SelectedValue
                    End If
                Else
                    Return ""
                End If
            End Get
            Set(ByVal valor As Object)
                If (_Tipo = DesktopConfig.CampoTipo.SiNo) Then
                    If (CBool(valor)) Then
                        ValueDesktopComboBox.SelectedIndex = 0
                    Else
                        ValueDesktopComboBox.SelectedIndex = 1
                    End If
                Else
                    ValueDesktopComboBox.SelectedValue = valor
                End If
            End Set
        End Property

        Public Property ValueControl As Boolean Implements IInputControl.ValueControl

        Public Property ÏsOCRCapture As Boolean Implements IInputControl.ÏsOCRCapture

        Public Property EnableTableOCR As Boolean Implements IInputControl.EnableTableOCR


        Public Property ValueValidacionListas As Object Implements IInputControl.ValueValidacionListas
            Get
                If (ValueValidacionListasDesktopComboBox.SelectedIndex >= 0) Then
                    If (_tipo = DesktopConfig.CampoTipo.SiNo) Then
                        Return (ValueValidacionListasDesktopComboBox.SelectedIndex = 0)
                    Else
                        Return ValueValidacionListasDesktopComboBox.SelectedValue
                    End If
                Else
                    Return ""
                End If
            End Get
            Set(ByVal valor As Object)
                If (_tipo = DesktopConfig.CampoTipo.SiNo) Then
                    If (CBool(valor)) Then
                        ValueValidacionListasDesktopComboBox.SelectedIndex = 0
                    Else
                        ValueValidacionListasDesktopComboBox.SelectedIndex = 1
                    End If
                Else
                    ValueValidacionListasDesktopComboBox.SelectedValue = valor
                End If
            End Set
        End Property

        Public Property ValueOld1 As Object Implements IInputControl.ValueOld1
            Get
                If (ValueOld1DesktopComboBox.SelectedIndex >= 0) Then
                    Return ValueOld1DesktopComboBox.SelectedValue
                Else
                    Return ""
                End If
            End Get
            Set(ByVal valor As Object)
                ValueOld1DesktopComboBox.SelectedValue = valor
            End Set
        End Property

        Public Property ValueOld2 As Object Implements IInputControl.ValueOld2
            Get
                If (ValueOld2DesktopComboBox.SelectedIndex >= 0) Then
                    Return ValueOld2DesktopComboBox.SelectedValue
                Else
                    Return ""
                End If
            End Get
            Set(ByVal valor As Object)
                ValueOld2DesktopComboBox.SelectedValue = valor
            End Set
        End Property

        Public Property ValueOld3 As Object Implements IInputControl.ValueOld3

        Public Property NextControl As Control Implements IInputControl.NextControl

        Public Property ShowSecondControls As Boolean Implements IInputControl.ShowSecondControls
            Get
                Return CapturaOldPanel.Visible
            End Get
            Set(ByVal valor As Boolean)
                CapturaOldPanel.Visible = valor
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
            Get
                Return _tipo
            End Get
            Set(ByVal valor As DesktopConfig.CampoTipo)
                Select Case valor
                    Case DesktopConfig.CampoTipo.Lista
                    Case DesktopConfig.CampoTipo.ListaEnlazada
                    Case DesktopConfig.CampoTipo.SiNo

                    Case Else
                        Throw New Exception("Tipo de dato no soportado por el control " + valor.ToString())

                End Select

                _tipo = valor

            End Set

        End Property

        Public Property UsaTrigger As Boolean Implements IInputControl.UsaTrigger

        Public Property TriggerValues As List(Of KeyValueItem) Implements IInputControl.TriggerValues

        Public Property TriggerValidaciones As List(Of TriggersValidations_Items) Implements IInputControl.TriggerValidaciones

        Public Property IsVisible As Boolean Implements IInputControl.IsVisible
            Get
                Return Me.Visible
            End Get
            Set(ByVal valor As Boolean)
                Me.Visible = valor
            End Set
        End Property

        Public Function Validar() As Boolean Implements IInputControl.Validar
            Select Case Tipo
                Case DesktopConfig.CampoTipo.Lista
                    If (Me.ValueDesktopComboBox.SelectedIndex < 0 And Me.DefinicionCaptura.Item(0).Es_Obligatorio_Campo) Then
                        MessageBox.Show("Debe seleccionar un valor para el campo " & Me.Etiqueta, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Warning)

                        Me.ValueDesktopComboBox.SelectAll()
                        Me.ValueDesktopComboBox.Focus()

                        Return False
                    End If

            End Select

            Return True
        End Function

        Public Sub LoadDefinition(ByVal nDefinicionCaptura As List(Of DefinicionCaptura)) Implements IInputControl.LoadDefinition
            _definicionCaptura = nDefinicionCaptura
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
                Return ValueDesktopComboBox.Visible
            End Get
            Set(ByVal value As Boolean)
                ValueDesktopComboBox.Visible = value
            End Set
        End Property
#End Region

#Region " Eventos "

        Private Sub ListInputControl_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            ValueDesktopComboBox.DisabledEnter = True
            Trigger()
            Trigger_Validaciones()
        End Sub

        Private Sub ValueDesktopComboBox_Enter(ByVal sender As System.Object, ByVal e As EventArgs) Handles ValueDesktopComboBox.Enter
            Me.IndexerView.SelectedInputControl = Me
        End Sub

        Private Sub ValueDesktopComboBox_Leave(ByVal sender As System.Object, ByVal e As EventArgs) Handles ValueDesktopComboBox.Leave
            If (Me.IsCalidad) Then ValidarCalidad()
            Me.IndexerView.SelectedInputControl = Nothing
        End Sub

        Private Sub ListInputControl_Enter(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Enter
            ValueDesktopComboBox.Focus()
        End Sub

        Private Sub ValueDesktopComboBox_KeyUp(ByVal sender As System.Object, ByVal e As KeyEventArgs) Handles ValueDesktopComboBox.KeyUp
            If (e.KeyCode = Keys.Enter) Then


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

        Private Sub ValueDesktopComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles ValueDesktopComboBox.SelectedIndexChanged
            Trigger()
            Trigger_Validaciones()
        End Sub

#End Region

#Region " Metodos "

        Private Sub Trigger()
            If (Me.UsaTrigger) Then
                '' Hace visibles todos los controles
                'For Each Campo In Me.IndexerView.Campos
                '    If (Not Campo.Control.IsVisible) Then
                '        Campo.Control.IsVisible = True
                '    End If

                '    If (Campo.Control.GetType() = GetType(TableInputControl)) Then
                '        Dim Tabla = CType(Campo.Control, TableInputControl)

                '        For Each Definicion In Tabla.DefinicionCaptura
                '            Definicion.IsReadOnly = False
                '        Next
                '    End If
                'Next

                ' Mostrar todos los campos afectados                
                For Each TriggerValue In TriggerValues
                    For Each Campo In Me.IndexerView.Campos
                        If (TriggerValue.Value = Campo.id) Then
                            If (Campo.Control.GetType() = GetType(TableInputControl)) Then
                                Dim tabla = CType(Campo.Control, TableInputControl)

                                For Each Definicion In tabla.DefinicionCaptura
                                    If (Definicion.id = TriggerValue.Item) Then
                                        Definicion.IsReadOnly = False
                                    End If
                                Next
                            Else
                                Campo.Control.IsVisible = True
                            End If
                        End If
                    Next
                Next

                ' Ocultar los controles configurados
                For Each TriggerValue In TriggerValues
                    If (Me.Value.ToString() = TriggerValue.Key) Then
                        For Each Campo In Me.IndexerView.Campos
                            If (TriggerValue.Value = Campo.id) Then
                                If (Campo.Control.GetType() = GetType(TableInputControl)) Then
                                    If TriggerValue.Item = -1 Then
                                        Campo.Control.IsVisible = False
                                    Else
                                        Dim tabla = CType(Campo.Control, TableInputControl)

                                        For Each Definicion In tabla.DefinicionCaptura
                                            If (Definicion.id = TriggerValue.Item) Then
                                                Definicion.IsReadOnly = True
                                            End If
                                        Next
                                    End If
                                Else
                                    Campo.Control.IsVisible = False
                                End If
                            End If
                        Next
                    End If
                Next
            End If
        End Sub

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
                Me.ValueDesktopComboBox.ForeColor = Drawing.Color.Red
            Else
                Me.ValueDesktopComboBox.ForeColor = Drawing.Color.Black
            End If
        End Sub

#End Region
    End Class

End Namespace
