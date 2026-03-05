Imports System.Text
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Drawing

Public Class Linea
    Inherits System.Web.UI.WebControls.Panel

#Region " Declaraciones "

    Private _X1 As Integer
    Private _Y1 As Integer
    Private _X2 As Integer
    Private _Y2 As Integer

#End Region

#Region " Propiedades "

    Public Property X1() As Integer
        Get
            Return _X1
        End Get
        Set(ByVal value As Integer)
            _X1 = value
        End Set
    End Property
    Public Property Y1() As Integer
        Get
            Return _Y1
        End Get
        Set(ByVal value As Integer)
            _Y1 = value
        End Set
    End Property
    Public Property X2() As Integer
        Get
            Return _X2
        End Get
        Set(ByVal value As Integer)
            _X2 = value
        End Set
    End Property
    Public Property Y2() As Integer
        Get
            Return _Y2
        End Get
        Set(ByVal value As Integer)
            _Y2 = value
        End Set
    End Property

#End Region

#Region " Metodos "

    Public Sub New()
        Me.BorderStyle = BorderStyle.Solid
        Me.BorderColor = Drawing.Color.Black
        Me.BorderWidth = New Unit(2, UnitType.Pixel)
    End Sub
    Protected Overrides Sub OnPreRender(ByVal e As System.EventArgs)
        Me.Style("position") = "absolute"
        Me.Style("top") = _Y1 & "px"
        Me.Style("left") = _X1 & "px"

        If _X1 = _X2 Then ' Vertical
            Me.Width = New Unit(0, UnitType.Pixel)
            Me.Height = New Unit(_Y2 - _Y1, UnitType.Pixel)
        Else ' Horizontal
            Me.Width = New Unit(_X2 - _X1, UnitType.Pixel)
            Me.Height = New Unit(0, UnitType.Pixel)
        End If

        MyBase.OnPreRender(e)
    End Sub

#End Region

End Class
