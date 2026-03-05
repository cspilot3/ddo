Imports System.Text
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Drawing

Public Class Bloque
    Inherits System.Web.UI.WebControls.Panel

#Region " Declaraciones "

    Private _X As Integer
    Private _Y As Integer
    Private _Etiqueta As String
    Private _Codigo As String

#End Region

#Region " Propiedades "

    Public Property X() As Integer
        Get
            Return _X
        End Get
        Set(ByVal value As Integer)
            _X = value
        End Set
    End Property
    Public Property Y() As Integer
        Get
            Return _Y
        End Get
        Set(ByVal value As Integer)
            _Y = value
        End Set
    End Property
    Public Property Etiqueta() As String
        Get
            Return _Etiqueta
        End Get
        Set(ByVal value As String)
            _Etiqueta = value
        End Set
    End Property
    Public Property Codigo() As String
        Get
            Return _Codigo
        End Get
        Set(ByVal value As String)
            _Codigo = value
        End Set
    End Property

#End Region

#Region " Metodos "

    Public Sub New()
        Me.BorderStyle = BorderStyle.Solid
        Me.BorderColor = Drawing.Color.Black
        Me.BorderWidth = New Unit(1, UnitType.Pixel)
    End Sub
    Protected Overrides Sub CreateChildControls()
        Dim Sb As New StringBuilder("")

        Sb.AppendLine("<table style=""width: " & Me.Width.Value & "px; height: " & Me.Height.Value & "px;"">")
        Sb.AppendLine("    <tr>")
        Sb.AppendLine("        <td align='center'>")
        Sb.AppendLine("            <b>" & _Etiqueta & "</b>")
        Sb.AppendLine("        </td>")
        Sb.AppendLine("    </tr>")
        Sb.AppendLine("    <tr>")
        Sb.AppendLine("        <td align='center'>")
        Sb.AppendLine("            " & _Codigo)
        Sb.AppendLine("        </td>")
        Sb.AppendLine("    </tr>")
        Sb.AppendLine("</table>")

        Controls.Add(New LiteralControl(Sb.ToString))

        MyBase.CreateChildControls()
    End Sub
    Protected Overrides Sub OnPreRender(ByVal e As System.EventArgs)
        Me.Attributes("onclick") = "SelectBloque(this);"
        'Me.Style("overflow") = "auto"
        Me.Style("position") = "absolute"
        Me.Style("top") = _Y & "px"
        Me.Style("left") = _X & "px"
        Me.Style("cursor") = "pointer"

        MyBase.OnPreRender(e)
    End Sub

#End Region

End Class
