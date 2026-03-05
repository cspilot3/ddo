Imports System
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.ComponentModel
Imports Miharu.Web.Controls

Public Class ParameterCheck
    Inherits WebControl
    Implements IParameter

#Region " Declaraciones "

    Private _Label As Label = New Label
    Private _valueCheckBox As CheckBox = New CheckBox
    Private _nullCheckBox As CheckBox = New CheckBox

    Private _UpdatePanel As UpdatePanel = New UpdatePanel

#End Region

#Region " Atributos "

    'Public Function GetParameter() As Object Implements IParameter.GetParameter
    '    Return Me.Text
    'End Function

    Public Function GetStringParameter() As String Implements IParameter.GetStringParameter
        If (_nullCheckBox.Checked) Then
            Return "null"
        End If

        Return CStr(IIf(_valueCheckBox.Checked, "1", "0"))
    End Function

    Public ReadOnly Property ParameterType As ParameterTypeEnum Implements IParameter.ParameterType

        Get
            Return ParameterTypeEnum.SiNo
        End Get

    End Property

    Public Property ParameterName As String Implements IParameter.ParameterName

#End Region

#Region " Metodos"

    Public Sub New(NameControl As String)
        Me.ParameterName = NameControl
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
        Me._valueCheckBox.ID = "_CheckBox_" + ParameterName
        Me._Label.ID = "_Label_" + ParameterName
        Me._Label.Text = ParameterName.Substring(1)
        Me._nullCheckBox.ID = "_checkBox_" + ParameterName
        Me._nullCheckBox.Text = "NULO"
        Me._nullCheckBox.Font.Bold = True
        Dim contentTemplateContainer As Control = Me._UpdatePanel.ContentTemplateContainer
        contentTemplateContainer.Controls.Add(Me._Label)
        contentTemplateContainer.Controls.Add(Me._valueCheckBox)
        contentTemplateContainer.Controls.Add(Me._nullCheckBox)

        Me._UpdatePanel.RenderMode = UpdatePanelRenderMode.Block

        MyBase.Controls.Add(Me._UpdatePanel)
    End Sub

#End Region


End Class
