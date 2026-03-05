Imports System
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.ComponentModel

Public Class ParameterDate
    Inherits WebControl
    Implements IParameter

#Region " Declaraciones "

    Private _Format As String = "yyyy-MM-dd"

    Private _Label As Label = New Label
    Private _calendar As Calendar = New Calendar
    Private _text As TextBox = New TextBox
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
        Dim res As String = "'" & String.Format("{0:yyyy-MM-dd}", _calendar.SelectedDate) & "'"
        If String.Compare("'0001-01-01'", res) = 0 Then
            Return ""
        Else
            Return res
        End If

    End Function

    Public ReadOnly Property ParameterType As ParameterTypeEnum Implements IParameter.ParameterType

        Get
            Return ParameterTypeEnum.Fecha
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
        Me._calendar.ID = "_calendar_" + ParameterName
        Me._Label.ID = "_Label_" + ParameterName
        Me._Label.Text = ParameterName.Substring(1)
        Me._nullCheckBox.ID = "_checkBox_" + ParameterName
        Me._nullCheckBox.Text = "NULO"
        Me._nullCheckBox.Font.Bold = True
        Dim contentTemplateContainer As Control = Me._UpdatePanel.ContentTemplateContainer
        contentTemplateContainer.Controls.Add(Me._Label)
        contentTemplateContainer.Controls.Add(Me._calendar)
        contentTemplateContainer.Controls.Add(Me._nullCheckBox)

        Me._UpdatePanel.RenderMode = UpdatePanelRenderMode.Block

        MyBase.Controls.Add(Me._UpdatePanel)
    End Sub

#End Region

End Class
