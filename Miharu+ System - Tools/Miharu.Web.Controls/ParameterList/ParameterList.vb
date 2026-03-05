Imports System
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.ComponentModel
Imports Miharu.Web.Controls

Public Class ParameterList
    Inherits WebControl
    Implements IParameter


    Private _Label As Label = New Label
    Private _valueDropDownList As DropDownList = New DropDownList
    Private _nullCheckBox As CheckBox = New CheckBox
    Private _query As String
    Private _UpdatePanel As UpdatePanel = New UpdatePanel

#Region " Declaraciones"

    Public Event LoadList(sender As ParameterList)

#End Region


#Region " Atributos "

    'Public Function GetParameter() As Object Implements IParameter.GetParameter
    '    Return Me.Text
    'End Function

    Public Function GetStringParameter() As String Implements IParameter.GetStringParameter
        If (_nullCheckBox.Checked) Then
            Return "null"
        ElseIf (Me._valueDropDownList.SelectedIndex < 0) Then
            Return "[" & Me.ParameterName & "]"

        End If
        Dim res As String = _valueDropDownList.SelectedValue.ToString()
        Return res
    End Function

    Public ReadOnly Property ParameterType As ParameterTypeEnum Implements IParameter.ParameterType

        Get
            Return ParameterTypeEnum.Lista
        End Get

    End Property

    Public Property ParameterName As String Implements IParameter.ParameterName

#End Region

#Region " Propiedades "

    Public Property DataSource() As Object
        Get
            Return Me._valueDropDownList.DataSource
        End Get
        Set(value As Object)
            Me._valueDropDownList.DataSource = value
        End Set
    End Property

    Public Property DisplayMember() As String
        Get
            Return Me._valueDropDownList.DataTextField
        End Get
        Set(value As String)
            Me._valueDropDownList.DataTextField = value
        End Set
    End Property

    Public Property ValueMember() As String
        Get
            Return Me._valueDropDownList.DataValueField
        End Get
        Set(value As String)
            Me._valueDropDownList.DataValueField = value
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


#Region " Metodos"

    Public Sub New(NameControl As String, query As String, displayMember As String, valueMember As String)
        Me.ParameterName = NameControl
        Me.Query = query
        Me.DisplayMember = displayMember
        Me.ValueMember = valueMember
        Cargar()
    End Sub

    Protected Overloads Sub OnLoad(ByVal e As EventArgs)

        Cargar()
    End Sub

    Protected Overrides Sub Render(ByVal output As HtmlTextWriter)
        MyBase.Render(output)
    End Sub

    Private Sub Cargar()
        Me._Label.Attributes("class") = Me.CssClass
        Me._valueDropDownList.ID = "_DropDownList_" + ParameterName
        Me._Label.ID = "_Label_" + ParameterName
        Me._Label.Text = ParameterName.Substring(1)
        Me._nullCheckBox.ID = "_checkBox_" + ParameterName
        Me._nullCheckBox.Text = "NULO"
        Me._nullCheckBox.Font.Bold = True
        Dim contentTemplateContainer As Control = Me._UpdatePanel.ContentTemplateContainer
        contentTemplateContainer.Controls.Add(Me._Label)
        contentTemplateContainer.Controls.Add(Me._valueDropDownList)
        contentTemplateContainer.Controls.Add(Me._nullCheckBox)

        Me._UpdatePanel.RenderMode = UpdatePanelRenderMode.Block

        MyBase.Controls.Add(Me._UpdatePanel)
    End Sub

    Public Overrides Sub DataBind()
        Me._valueDropDownList.DataBind()
    End Sub

#End Region



End Class
