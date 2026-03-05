Imports Miharu
Imports Miharu.Web.Controls

Public Class wucDatePicker
    Inherits WebControl
    Implements IParameter

#Region " Declaraciones "
    Private _Format_Date As String = "yyyy-MM-dd"

    Private _UpdatePanel As UpdatePanel = New UpdatePanel

    Private MySesion As Miharu.Security.Library.Session.Sesion

#End Region

#Region " Atributos "

    Public Function GetStringParameter() As String Implements IParameter.GetStringParameter
        If (_nullCheckBox.Checked) Then
            Return "null"
        End If

        Dim res As String = "'" & String.Format("{0:yyyy-MM-dd}", _valueTextBox.Text) & "'"
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

    Public Sub New()

    End Sub

    Public Sub New(NameControl As String, ByRef sesion As Miharu.Security.Library.Session.Sesion)
        Me.ParameterName = NameControl
        Me.MySesion = sesion
    End Sub
#End Region

#Region " Procedimientos "

    Protected Overrides Sub Render(ByVal output As HtmlTextWriter)
        MyBase.Render(output)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Cargar()
    End Sub

    Private Sub Cargar()
        Me._nullCheckBox = New CheckBox()
        Me._nullCheckBox.CssClass = "CheckBox"
        Me._nullCheckBox.ID = "_nullCheckBox_" + ParameterName
        Me._nullCheckBox.Text = "Nulo"

        Me._label = New Label()
        Me._label.CssClass = "Label"
        Me._label.ID = "_label_" + ParameterName
        Me._label.Text = ParameterName.Substring(1)

        Me._ImageButton = New ImageButton
        Me._ImageButton.ID = "_ImageButton_" + ParameterName
        Me._ImageButton.CssClass = "Button"
        Me._ImageButton.ImageUrl = "~/_images/menu/Calendario.png"

        Me._calendar = New Calendar()
        Me._calendar.CssClass = "Calendar"
        Me._calendar.ID = "_calendar_" + ParameterName
        Me._calendar.Visible = EvaluarCalendar()

        Me._valueTextBox = New TextBox()
        Me._valueTextBox.CssClass = "TextBox"
        Me._valueTextBox.ID = "_valueTextBox_" + ParameterName
        Me._valueTextBox.Enabled = False
        Me._valueTextBox.Text = EvaluarFecha()

        Dim contentTemplateContainer As Control = Me._UpdatePanel.ContentTemplateContainer
        contentTemplateContainer.Controls.Add(Me._label)
        contentTemplateContainer.Controls.Add(Me._valueTextBox)
        contentTemplateContainer.Controls.Add(Me._ImageButton)
        contentTemplateContainer.Controls.Add(Me._nullCheckBox)
        contentTemplateContainer.Controls.Add(Me._calendar)

        Me._UpdatePanel.RenderMode = UpdatePanelRenderMode.Block

        MyBase.Controls.Add(Me._UpdatePanel)

    End Sub

    Private Sub OcultarCalendar()
        _calendar.Visible = False
        Me.MySesion.Pagina.Parameter("_calendar_visible_" + ParameterName.ToString) = _calendar.Visible
        Me._valueTextBox.Text = EvaluarFecha()
    End Sub
#End Region

#Region " Eventos "

    Protected Sub _ImageButton_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles _ImageButton.Click
        Procesar_Calendario()
    End Sub

    Private Sub Procesar_Calendario()
        If _calendar.Visible Then
            _calendar.Visible = False
        Else
            _calendar.SelectedDate = CDate(_valueTextBox.Text)
            _calendar.Visible = True
        End If
        Me.MySesion.Pagina.Parameter("_calendar_visible_" + ParameterName.ToString) = _calendar.Visible
    End Sub

#End Region

#Region " Funciones "

    Protected Sub _calendar_SelectionChanged(sender As Object, e As EventArgs) Handles _calendar.SelectionChanged
        _valueTextBox.Text = _calendar.SelectedDate.Date.ToString(_Format_Date)
        OcultarCalendar()
    End Sub

    Private Function EvaluarCalendar() As Boolean

        If Me.MySesion.Pagina.Parameter("_calendar_visible_" + ParameterName.ToString) Is Nothing Then
            Me.MySesion.Pagina.Parameter("_calendar_visible_" + ParameterName.ToString) = False
            _calendar.SelectedDate = Date.Today
        Else
            Return CType(Me.MySesion.Pagina.Parameter("_calendar_visible_" + ParameterName.ToString), Boolean)
        End If

    End Function

    Private Function EvaluarFecha() As String
        Return _calendar.SelectedDate.ToString(_Format_Date)
    End Function

#End Region

End Class