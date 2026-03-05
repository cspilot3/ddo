Imports Miharu
Imports Miharu.Web.Controls

Public Class wucParameterList
    Inherits WebControl
    Implements IParameter

#Region " Declaraciones"

    Private MySesion As Miharu.Security.Library.Session.Sesion

    Public Event LoadList(sender As ParameterList)

    Private _query As String
    Private _UpdatePanel As UpdatePanel = New UpdatePanel

#End Region

#Region " Atributos "

    Public Function GetStringParameter() As String Implements IParameter.GetStringParameter
        If (nullCheckBox.Checked) Then
            Return "null"
        ElseIf (valueDropDownList.SelectedIndex < 0) Then
            Return "[" & Me.ParameterName & "]"

        End If
        Dim res As String = _valueDropDownList.SelectedValue.ToString()
        Return res

    End Function


    Public ReadOnly Property ParameterType As ParameterTypeEnum Implements IParameter.ParameterType

        Get
            Return ParameterTypeEnum.Texto
        End Get

    End Property

    Public Property ParameterName As String Implements IParameter.ParameterName

    Public Sub New()

    End Sub

    'Public Sub New(NameControl As String, ByRef sesion As Miharu.Security.Library.Session.Sesion)
    '    Me.ParameterName = NameControl
    '    Me.MySesion = sesion
    'End Sub


#End Region
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Cargar()

    End Sub

#Region " Propiedades "

    Public Property DataSource() As Object
        Get
            Return Me.valueDropDownList.DataSource
        End Get
        Set(value As Object)
            Me.valueDropDownList.DataSource = value
        End Set
    End Property

    Public Property DisplayMember() As String
        Get
            Return Me.valueDropDownList.DataTextField
        End Get
        Set(value As String)
            Me.valueDropDownList.DataTextField = value
        End Set
    End Property

    Public Property ValueMember() As String
        Get
            Return Me.valueDropDownList.DataValueField
        End Get
        Set(value As String)
            Me.valueDropDownList.DataValueField = value
        End Set
    End Property

    Public Property Query() As String
        Get
            Return _query
        End Get
        Set(value As String)
            _query = value
        End Set
    End Property

#End Region


#Region " Procedimientos "

    Public Sub New(NameControl As String, query As String, displayMember As String, valueMember As String, sesion As Miharu.Security.Library.Session.Sesion)
        Me.ParameterName = NameControl
        MySesion = sesion
        Cargar()
        Me.Query = query
        Me.DisplayMember = displayMember
        Me.ValueMember = valueMember


    End Sub

    Protected Overloads Sub OnLoad(ByVal e As EventArgs)

        'Cargar()

    End Sub

    Protected Overrides Sub Render(ByVal output As HtmlTextWriter)
        MyBase.Render(output)
    End Sub

    Private Sub Cargar()
        Me.Label = New Label()
        Me.Label.Attributes("class") = Me.CssClass
        Me.Label.ID = "_Label_" + ParameterName
        Me.Label.Text = ParameterName.Substring(1)
        Me.Label.CssClass = "Label"

        Me.valueDropDownList = New DropDownList()
        Me.valueDropDownList.ID = "_valueDropDownList_" + ParameterName
        Me.valueDropDownList.CssClass = "DropDownList"

        Me.nullCheckBox = New CheckBox()
        Me.nullCheckBox.ID = "_nullCheckBox_" + ParameterName
        Me.nullCheckBox.Text = "NULO"
        Me.nullCheckBox.Font.Bold = True
        Me.nullCheckBox.CssClass = "CheckBox"

        Dim contentTemplateContainer As Control = Me._UpdatePanel.ContentTemplateContainer
        contentTemplateContainer.Controls.Add(Me._Label)
        contentTemplateContainer.Controls.Add(Me._valueDropDownList)
        contentTemplateContainer.Controls.Add(Me._nullCheckBox)

        Me._UpdatePanel.RenderMode = UpdatePanelRenderMode.Block

        MyBase.Controls.Add(Me._UpdatePanel)
    End Sub

    Public Overrides Sub DataBind()
        Me.valueDropDownList.DataBind()
    End Sub

#End Region



End Class