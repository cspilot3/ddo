Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Text
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports AjaxControlToolkit

Public Class CoreDateBox
    Inherits TextBox

    Private oMEE As MaskedEditExtender
    'Private oMEV As MaskedEditValidator

    Private _DateFormat As String = "yyyy-MM-dd"
    Private _IsRequired As Boolean = False

    Public Property IsRequired() As Boolean
        Get
            Return _IsRequired
        End Get
        Set(ByVal value As Boolean)
            _IsRequired = value
        End Set
    End Property

    Public Property DateFormat() As String
        Get
            Return _DateFormat
        End Get
        Set(ByVal value As String)
            _DateFormat = value
        End Set
    End Property

    Public ReadOnly Property MyMaskedEditExtender() As MaskedEditExtender
        Get
            Return oMEE
        End Get
    End Property

    'Public ReadOnly Property MyMaskedEditValidator() As MaskedEditValidator
    '    Get
    '        Return oMEV
    '    End Get
    'End Property

    Public ReadOnly Property MaskedEditExtenderID() As String
        Get
            Return "MEE_" & Me.ID
        End Get
    End Property

    Public ReadOnly Property MaskedEditValidatorID() As String
        Get
            Return "Validator_" & Me.ID
        End Get
    End Property


    <Bindable(True), DefaultValue("*")> _
    Property ErrorMessage() As String
        Get
            Return CStr(ViewState(Me.ID & "ErrorMessage"))
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "ErrorMessage") = value
        End Set
    End Property

    Protected Overrides Sub OnInit(ByVal e As System.EventArgs)
        Me.BuildControls()
    End Sub

    Protected Overrides Sub Render(ByVal writer As System.Web.UI.HtmlTextWriter)
        MyBase.Render(writer)
        writer.Write("&nbsp;")
        If (Not DesignMode) Then
            oMEE.RenderControl(writer)
            'oMEV.RenderControl(writer)
        End If
    End Sub

    Protected Sub BuildControls()

        oMEE = New MaskedEditExtender
        oMEE.ID = MaskedEditExtenderID
        oMEE.MaskType = MaskedEditType.Date
        oMEE.Mask = "9999/99/99"
        oMEE.AutoComplete = False
        oMEE.CultureName = "en-CA"
        oMEE.MessageValidatorTip = True
        oMEE.TargetControlID = Me.ID
        Me.Controls.Add(oMEE)

        'oMEV = New MaskedEditValidator
        'oMEV.ID = MaskedEditValidatorID
        'oMEV.ControlExtender = oMEE.ID
        'oMEV.ControlToValidate = Me.ID
        'oMEV.Display = ValidatorDisplay.None
        'oMEV.IsValidEmpty = Not _IsRequired
        'oMEV.EmptyValueMessage = "Fecha invalida"
        'oMEV.ErrorMessage = "Fecha invalida"
        'oMEV.ToolTip = "Digite la fecha"
        'oMEV.Text = "Fecha invalida"
        'oMEV.TooltipMessage = "Digite la fecha"
        'oMEV.ValidationGroup = Me.ValidationGroup ' "Save"
        'Me.Controls.Add(oMEV)

    End Sub

    Public Function ToDateString() As String
        If (Text.Trim() = "") Then
            Return ""
        Else
            Try
                Dim d As DateTime = Convert.ToDateTime(Text)
                Return d.ToString(DateFormat)
            Catch ex As Exception
                Return ""
            End Try
        End If
    End Function
End Class
