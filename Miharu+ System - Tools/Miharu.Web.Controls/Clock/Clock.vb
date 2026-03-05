Imports System
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.ComponentModel

<ToolboxData("<{0}:Clock runat=server></{0}:Clock>")> _
Public Class Clock
    Inherits WebControl

#Region " Declaraciones "

    ' Fields
    Private _Interval As Integer = 60000
    Private _Format As String = "yyyy-MM-dd HH:mm"

    Private _Label As Label = New Label
    Private _Timer As Timer = New Timer
    Private _UpdatePanel As UpdatePanel = New UpdatePanel

    Public Event OnDateChanged(ByRef curDate As DateTime)

#End Region

#Region " Propiedades "

    Public Property Interval() As Integer
        Get
            Return _Interval
        End Get
        Set(ByVal value As Integer)
            _Interval = value
        End Set
    End Property
    Public Property Format() As String
        Get
            Return _Format
        End Get
        Set(ByVal value As String)
            _Format = value
        End Set
    End Property

#End Region

#Region " Metodos "

    Protected Overrides Sub OnLoad(ByVal e As EventArgs)
        Dim curDate As DateTime = DateTime.Now

        RaiseEvent OnDateChanged(curDate)

        Me._Timer.ID = Me.ID & "_tiker"
        Me._Label.ID = Me.ID & "_l"
        Me._Label.Attributes("class") = Me.CssClass

        Dim contentTemplateContainer As Control = Me._UpdatePanel.ContentTemplateContainer
        contentTemplateContainer.Controls.Add(Me._Label)
        contentTemplateContainer.Controls.Add(Me._Timer)

        Dim item As New AsyncPostBackTrigger
        item.ControlID = Me._Timer.ID
        item.EventName = "Tick"

        Me._UpdatePanel.Triggers.Add(item)
        Me._UpdatePanel.ChildrenAsTriggers = True
        Me._Label.Text = curDate.ToString(_Format)
        Me._Timer.Interval = _Interval

        Me._UpdatePanel.RenderMode = UpdatePanelRenderMode.Block

        MyBase.Controls.Add(Me._UpdatePanel)
    End Sub
    Protected Overrides Sub Render(ByVal output As HtmlTextWriter)
        MyBase.Render(output)
    End Sub

#End Region

End Class
