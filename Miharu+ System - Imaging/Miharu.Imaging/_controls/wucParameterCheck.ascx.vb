Imports Miharu
Imports Miharu.Web.Controls

Public Class wucParameterCheck
    Inherits WebControl
    Implements IParameter


#Region " Declaraciones"

    Private _UpdatePanel As UpdatePanel = New UpdatePanel

    Private MySesion As Miharu.Security.Library.Session.Sesion

#End Region

#Region " Atributos "

    Public Function GetStringParameter() As String Implements IParameter.GetStringParameter
        If (nullCheckBox.Checked) Then
            Return "null"
        End If

        Return CStr(IIf(valueCheckBox.Checked, "1", "0"))

    End Function


    Public ReadOnly Property ParameterType As ParameterTypeEnum Implements IParameter.ParameterType

        Get
            Return ParameterTypeEnum.SiNo
        End Get

    End Property

    Public Property ParameterName As String Implements IParameter.ParameterName

    Public Sub New()

    End Sub

    Public Sub New(NameControl As String, ByRef sesion As Miharu.Security.Library.Session.Sesion)
        Me.ParameterName = NameControl
        Me.MySesion = sesion
    End Sub
#End Region


#Region " Procedimientos "

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Cargar()
    End Sub


    Private Sub Cargar()
        Me.nullCheckBox = New CheckBox()
        Me.nullCheckBox.CssClass = "CheckBox"
        Me.nullCheckBox.ID = "nullCheckBox_" + ParameterName
        Me.nullCheckBox.Text = "Nulo"

        Me.Label1 = New Label()
        Me.Label1.CssClass = "Label"
        Me.Label1.ID = "label_" + ParameterName
        Me.Label1.Text = ParameterName.Substring(1)

        Me.valueCheckBox = New CheckBox()
        Me.valueCheckBox.CssClass = "CheckBox"
        Me.valueCheckBox.ID = "valueCheckBox_" + ParameterName
        Me.valueCheckBox.Text = ""


        Dim contentTemplateContainer As Control = Me._UpdatePanel.ContentTemplateContainer
        contentTemplateContainer.Controls.Add(Me.Label1)
        contentTemplateContainer.Controls.Add(Me.valueCheckBox)
        contentTemplateContainer.Controls.Add(Me.nullCheckBox)

        Me._UpdatePanel.RenderMode = UpdatePanelRenderMode.Block

        MyBase.Controls.Add(Me._UpdatePanel)

    End Sub

#End Region



End Class