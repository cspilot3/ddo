Imports System
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Imaging

<ToolboxData("<{0}:ImageChangingButton runat=server></{0}:ImageChangingButton>")> _
Public Class ImageChangingButton
    Inherits System.Web.UI.WebControls.ImageButton

#Region " Declaraciones "

    Private _ChangingImageUrl As String = ""
    Private _DisableImageUrl As String = ""

#End Region

#Region " Constructores "

    Public Sub New()
        MyBase.CausesValidation = False

        Me.BorderWidth = New Unit(0, UnitType.Pixel)
        Me.BorderStyle = WebControls.BorderStyle.None
    End Sub

#End Region

#Region " Propiedades "

    <Bindable(True), _
    Category("Behavior"), _
    Browsable(True), _
    Description("Dirección URL de la imagen a visualizar cuando el mause se posa sobre el control")> _
    Public Property ChangingImageUrl() As String
        Get
            Return _ChangingImageUrl
        End Get
        Set(ByVal value As String)
            _ChangingImageUrl = value
        End Set
    End Property
    <Bindable(True), _
        Category("Behavior"), _
        Browsable(True), _
        Description("Dirección URL de la imagen a visualizar el control se encuentra deshabilitado")> _
    Public Property DisableImageUrl() As String
        Get
            Return _DisableImageUrl
        End Get
        Set(ByVal value As String)
            _DisableImageUrl = value
        End Set
    End Property

#End Region

#Region " Metodos "

    Protected Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
    End Sub

    Protected Overrides Sub Render(ByVal output As System.Web.UI.HtmlTextWriter)
        If Me.Enabled Then
            If Me.ChangingImageUrl <> "" Then
                output.AddAttribute("onmouseover", "this.src='" + ResolveClientUrl(Me.ChangingImageUrl) + "';")
                output.AddAttribute("onmouseout", "this.src='" + ResolveClientUrl(Me.ImageUrl) + "';")
            Else
                Me.BorderWidth = New Unit(1, UnitType.Pixel)
                Me.BorderStyle = WebControls.BorderStyle.Solid
                Me.BorderColor = Color.White
                Me.BackColor = Color.White

                Dim strColor As String = Me.BackColor.Name

                If (strColor.ToUpper()(0) = "F"c And strColor.ToUpper()(1) = "F"c) Then
                    strColor = "#" & strColor.Substring(2, 6)
                End If

                output.AddAttribute("onmouseover", "this.style.border='1px solid #808080'")
                output.AddAttribute("onmouseout", "this.style.border='1px solid " & strColor & "'")
            End If

            MyBase.Render(output)
        ElseIf Me.DisableImageUrl <> "" Then
            Dim TempImage As String

            TempImage = Me.ImageUrl
            Me.ImageUrl = Me.DisableImageUrl

            MyBase.Render(output)

            Me.ImageUrl = TempImage
        Else
            Me.BorderWidth = New Unit(1, UnitType.Pixel)
            Me.BorderStyle = WebControls.BorderStyle.Solid
            Me.BorderColor = Color.White
            Me.BackColor = Color.White

            MyBase.Render(output)
            If (Me.DesignMode = False) Then
                Dim Imagen As New Bitmap(Page.Server.MapPath(Me.ImageUrl))

                output.Write("<div title='" & Me.ToolTip & "' style='width: " & Imagen.Width & "px; height: " & Imagen.Height & "px; background-color: WHITE; filter: alpha(opacity=80);" & _
                                "opacity: 0.8; -moz-opacity: 0.7; opacity: 0.8; position: relative; z-index: 100; top: -" & Imagen.Height & "px; left: 0px;'></div>")
            End If
        End If
    End Sub

#End Region

End Class