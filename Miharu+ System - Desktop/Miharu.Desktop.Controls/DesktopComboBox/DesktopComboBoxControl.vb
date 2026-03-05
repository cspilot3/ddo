Imports System.Windows.Forms

Namespace DesktopComboBox

    Public Class DesktopComboBoxControl
        Inherits ComboBox

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


#Region " Constructor "

        Public Sub New()
            ' This call is required by the designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            Me.AutoCompleteMode = Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.AutoCompleteSource = Windows.Forms.AutoCompleteSource.ListItems
            Me.FlatStyle = Windows.Forms.FlatStyle.Popup
            Me.DropDownStyle = ComboBoxStyle.DropDownList
        End Sub

#End Region

#Region " Eventos "

        Property DisabledEnter As Boolean

        Private Sub cboEmbarque_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles Me.KeyDown
            If (Not DisabledEnter) Then
                If e.KeyCode = Keys.Enter Then
                    e.Handled = True
                    SendKeys.Send("{TAB}")
                End If
            End If
        End Sub

#End Region

    End Class

End Namespace