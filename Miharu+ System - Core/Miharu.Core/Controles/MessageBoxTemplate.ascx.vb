Imports AjaxControlToolkit

Public Class MessageBoxTemplate
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public ReadOnly Property MsgBoxTitulo1() As Label
        Get
            Return MsgBoxTitulo
        End Get
    End Property

    Public ReadOnly Property MsgBoxMensaje1() As Label
        Get
            Return MsgBoxMensaje
        End Get
    End Property

    Public ReadOnly Property MsgBoxIcono1() As Image
        Get
            Return MsgBoxIcono
        End Get
    End Property

    Public ReadOnly Property pnlPopUpBarra1() As Panel
        Get
            Return pnlPopUpBarra
        End Get
    End Property

    'Public ReadOnly Property CrearPopUp() As HtmlInputHidden
    '    Get
    '        Return CrearPopUp1
    '    End Get
    'End Property

    Public ReadOnly Property MsgBoxPopUp1() As ModalPopupExtender
        Get
            Return MsgBoxPopUp
        End Get
    End Property
End Class