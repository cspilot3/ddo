Imports System
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.ComponentModel

Public Class ParameterFileUpload
    Inherits WebControl
    Implements IParameter
    'NOTA. El control opera pero para el entorno Miharu.Imaging no cumple las condiciones 
    '20210618

#Region " Declaraciones "
    Private _Label As Label = New Label
    Private _nullCheckBox As CheckBox = New CheckBox
    Private _valueTextBox As TextBox = New TextBox
    Private _UpdatePanel As UpdatePanel = New UpdatePanel


#End Region

    Public Function GetStringParameter() As String Implements IParameter.GetStringParameter

        If (_nullCheckBox.Checked) Then
            Return ""
        End If
        If _valueTextBox.Text.Length = 0 Then
            Return ""
        Else

            Dim respuesta As String = _valueTextBox.Text
            If respuesta.Length = 0 Then
                Return ""
            Else
                Return respuesta
            End If
        End If


    End Function

    Public Property ParameterName As String Implements IParameter.ParameterName


    Public ReadOnly Property ParameterType As ParameterTypeEnum Implements IParameter.ParameterType
        Get
            Return ParameterTypeEnum.Texto
        End Get
    End Property


#Region " Metodos "

    Public Sub New(NameControl As String, val As Object)
        Me.ParameterName = NameControl
        If Not val Is Nothing Then
            _valueTextBox.Text = CType(val, String)
        End If
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
        Me._Label.ID = "_Label_" + ParameterName
        Me._Label.Text = "Archivo: "
        Me._nullCheckBox.ID = "_checkBox_" + ParameterName
        Me._nullCheckBox.Text = "NULO"
        Me._nullCheckBox.Font.Bold = True
        Me._nullCheckBox.Visible = False

        Me._valueTextBox.ID = "_textBox_" + ParameterName
        Dim contentTemplateContainer As Control = Me._UpdatePanel.ContentTemplateContainer
        contentTemplateContainer.Controls.Add(Me._Label)
        contentTemplateContainer.Controls.Add(Me._nullCheckBox)
        contentTemplateContainer.Controls.Add(Me._valueTextBox)
        Me._UpdatePanel.RenderMode = UpdatePanelRenderMode.Block
        Me._UpdatePanel.EnableViewState = True

        MyBase.Controls.Add(Me._UpdatePanel)

    End Sub



#End Region

End Class
