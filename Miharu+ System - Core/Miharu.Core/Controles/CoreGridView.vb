Public Class CoreGridView
    Inherits GridView

#Region "_   DEFINICION DE VARIABLES   _"

    Public Enum EnumClickAction
        OnClickNoEvents
        OnClickSelectedPostBack
        OnDblClickSelectedPostBack
    End Enum

    Private DivContainerControl As HtmlGenericControl

    Private PreSelectedIndexControl As HtmlInputHidden

    Private SelectedIndexControl As HtmlInputHidden

    Private IsConfigureRequiredControl As HtmlInputHidden

    Private IsInitializeRequiredControl As HtmlInputHidden

    Private ScriptControl As Literal

    Private _GridNum As Integer = 0

    Private _EnableSort As Boolean = True

    Private _ClickAction As EnumClickAction = EnumClickAction.OnDblClickSelectedPostBack

    Private _PreSelectedStyleCssClass As String = ""

    Private _OnBeginPreSelect As String = ""

    Private _OnBeginSelect As String = ""

    Private _OnEndPreSelect As String = ""

    Private _OnEndSelect As String = ""

#End Region

#Region "_   PROPIEDADES   _"

    Public Property GridNum() As Integer
        Get
            Return _GridNum
        End Get
        Set(ByVal value As Integer)
            _GridNum = value
        End Set
    End Property

    Public Property EnableSort() As Boolean
        Get
            Return _EnableSort
        End Get
        Set(ByVal value As Boolean)
            _EnableSort = value
        End Set
    End Property

    Public Property ClickAction() As EnumClickAction
        Get
            Return _ClickAction
        End Get
        Set(ByVal value As EnumClickAction)
            _ClickAction = value
        End Set
    End Property

    Private ReadOnly Property ClientGridId() As String
        Get
            Return "Grd_" & Me.ID & GridNum
        End Get
    End Property

    Private ReadOnly Property DivContainerClientControlId() As String
        Get
            Return "Grd_Div_" & Me.ID & GridNum
        End Get
    End Property

    Private ReadOnly Property PreSelectedIndexClientControlId() As String
        Get
            Return "Grd_PreSelected_" & Me.ID & GridNum
        End Get
    End Property

    Private ReadOnly Property SelectedIndexClientControlId() As String
        Get
            Return "Grd_SelectedIndex_" & Me.ID & GridNum
        End Get
    End Property

    Private ReadOnly Property IsConfigureRequiredClientControlId() As String
        Get
            Return "Grd_IsConfigureRequired_" & Me.ID & GridNum
        End Get
    End Property

    Private ReadOnly Property IsInitializeRequiredClientControlId() As String
        Get
            Return "Grd_IsInitializeRequired_" & Me.ID & GridNum
        End Get
    End Property

    Public Property PreSelectedStyleCssClass() As String
        Get
            Return _PreSelectedStyleCssClass
        End Get
        Set(ByVal value As String)
            _PreSelectedStyleCssClass = value
        End Set
    End Property

    Public Property OnBeginPreSelect() As String
        Get
            Return _OnBeginPreSelect
        End Get
        Set(ByVal value As String)
            _OnBeginPreSelect = value
        End Set
    End Property

    Public Property OnBeginSelect() As String
        Get
            Return _OnBeginSelect
        End Get
        Set(ByVal value As String)
            _OnBeginSelect = value
        End Set
    End Property

    Public Property OnEndPreSelect() As String
        Get
            Return _OnEndPreSelect
        End Get
        Set(ByVal value As String)
            _OnEndPreSelect = value
        End Set
    End Property

    Public Property OnEndSelect() As String
        Get
            Return _OnEndSelect
        End Get
        Set(ByVal value As String)
            _OnEndSelect = value
        End Set
    End Property

    Public Overrides Property SelectedIndex() As Integer
        Get
            Return MyBase.SelectedIndex
        End Get
        Set(ByVal value As Integer)
            MyBase.SelectedIndex = value
            _IsConfigureRequired = True
        End Set
    End Property

    Public Property PreSelectedIndex() As Integer
        Get
            Return _PreSelectedIndex
        End Get
        Set(ByVal value As Integer)
            _PreSelectedIndex = value
            _IsConfigureRequired = True
        End Set
    End Property

    Private _PreSelectedIndex As Integer = -1

    Private _IsConfigureRequired As Boolean = False

    Private _IsInitializeRequired As Boolean = False

#End Region

#Region "_   FUNCIONES Y PROCEDIMIENTOS   _"

    Public Sub SelectPreselectedIndex()
        If (Not DesignMode) Then
            Try
                If (Integer.Parse(PreSelectedIndexControl.Value) > -1) Then
                    SelectedIndex = CInt(PreSelectedIndexControl.Value)
                    OnSelectedIndexChanged(Nothing)
                End If

            Catch 'ex As Exception
            End Try
        End If
    End Sub

    Public Sub New()
        MyBase.CssClass = "yui-datatable-theme"
        MyBase.RowStyle.CssClass = "nor-data-row"
        MyBase.AlternatingRowStyle.CssClass = "alt-data-row"
        MyBase.EditRowStyle.CssClass = "row-edit"
        MyBase.SelectedRowStyle.CssClass = "row-Select"
        MyBase.PagerStyle.CssClass = "pager-stl"
        PreSelectedStyleCssClass = "row-PreSelect"
    End Sub

    Protected Overrides Sub OnInit(ByVal e As System.EventArgs)
        MyBase.OnInit(e)
    End Sub

    Protected Overrides Sub LoadControlState(ByVal savedState As Object)
        MyBase.LoadControlState(savedState)

        _PreSelectedIndex = CInt(GetControlStateValue(PreSelectedIndexClientControlId, "-1"))
        SelectedIndex = CInt(GetControlStateValue(SelectedIndexClientControlId, "-1"))
        _IsConfigureRequired = Boolean.Parse(GetControlStateValue(IsConfigureRequiredClientControlId, "False"))
        _IsInitializeRequired = Boolean.Parse(GetControlStateValue(IsInitializeRequiredClientControlId, "False"))

    End Sub

    Protected Overrides Function SaveControlState() As Object
        Try

            PreSelectedIndexControl.Value = CStr(_PreSelectedIndex)
            SelectedIndexControl.Value = CStr(SelectedIndex)
            IsConfigureRequiredControl.Value = CStr(_IsConfigureRequired)
            IsInitializeRequiredControl.Value = CStr(_IsInitializeRequired)
            ScriptControl.Text = GetClientScript()
        Catch
        End Try
        Return MyBase.SaveControlState()
    End Function

    Protected Overrides Sub CreateChildControls()
        MyBase.CreateChildControls()
        Me.CreateHidenControls()
        Me.RegisterHidenControls()
    End Sub

    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
        MyBase.OnLoad(e)
    End Sub

    Protected Overrides Sub OnPreRender(ByVal e As System.EventArgs)
        Page.ClientScript.RegisterStartupScript(GetType(String), ClientGridId & "_Initialize", "Sys.Application.add_load( function(sender, args) {{" & ClientGridId & ".Configure(sender, args)" & "}});", True)
        MyBase.OnPreRender(e)
        'JMRC 01
        'EnsureChildControls()
        'PreSelectedIndexControl.Value = _PreSelectedIndex.ToString()
        'SelectedIndexControl.Value = CStr(SelectedIndex)
        'IsConfigureRequiredControl.Value = CStr(_IsConfigureRequired)
        'IsInitializeRequiredControl.Value = CStr(_IsInitializeRequired)

    End Sub

    Protected Overrides Sub Render(ByVal writer As System.Web.UI.HtmlTextWriter)
        If (Not DesignMode) Then
            Try
                MyBase.Render(writer)
            Catch ex As Exception
            End Try
            Try
                DivContainerControl.RenderControl(writer)
                'GridViewPreSelectedIndex.RenderControl(writer)
                'GridViewSelectedIndex.RenderControl(writer)
                'GridViewScript.RenderControl(writer)
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Function GetControlStateValue(ByVal ControlID As String, Optional ByVal DefaultValue As String = "") As String
        'If (Page Is Nothing Or Page.Request Is Nothing Or Page.Request.Form Is Nothing) Then Return DefaultValue
        For i As Integer = 0 To Page.Request.Form.Keys.Count - 1
            If (Not Page.Request.Form.Keys(i) Is Nothing) Then
                If (Page.Request.Form.Keys(i).IndexOf(ControlID, 0) >= 0) Then
                    Return Page.Request.Form(i).Split(","c)(0)
                End If
            End If
        Next
        Return DefaultValue
    End Function

    Protected Sub CreateHidenControls()
        Try
            If (Not DesignMode) Then
                DivContainerControl = New HtmlGenericControl("div")
                PreSelectedIndexControl = New HtmlInputHidden()
                SelectedIndexControl = New HtmlInputHidden()
                IsConfigureRequiredControl = New HtmlInputHidden()
                IsInitializeRequiredControl = New HtmlInputHidden()
                ScriptControl = New Literal
            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub RegisterHidenControls()
        Try
            If (Not DesignMode) Then
                MyBase.Controls.Add(DivContainerControl)

                PreSelectedIndexControl.ID = PreSelectedIndexClientControlId
                PreSelectedIndexControl.Value = CStr(_PreSelectedIndex)
                DivContainerControl.Controls.Add(PreSelectedIndexControl)

                SelectedIndexControl.ID = SelectedIndexClientControlId
                SelectedIndexControl.Value = CStr(SelectedIndex)
                DivContainerControl.Controls.Add(SelectedIndexControl)

                IsConfigureRequiredControl.ID = IsConfigureRequiredClientControlId
                IsConfigureRequiredControl.Value = CStr(_IsConfigureRequired)
                DivContainerControl.Controls.Add(IsConfigureRequiredControl)

                IsInitializeRequiredControl.ID = IsInitializeRequiredClientControlId
                IsInitializeRequiredControl.Value = CStr(_IsInitializeRequired)
                DivContainerControl.Controls.Add(IsInitializeRequiredControl)

                DivContainerControl.Controls.Add(ScriptControl)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Property DataSource() As Object
        Get
            Return MyBase.DataSource
        End Get
        Set(ByVal value As Object)
            MyBase.DataSource = value
            _IsInitializeRequired = True
        End Set
    End Property

    Protected Overrides Sub OnRowDataBound(ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        If (Not DesignMode) Then

            If (_EnableSort) Then
                If (e.Row.RowType = DataControlRowType.Header) Then
                    For i As Integer = 0 To e.Row.Cells.Count - 1
                        e.Row.Cells(i).Attributes("onclick") = ClientGridId & ".SortColumn( " & i & " );"
                    Next
                End If
            End If

            If (e.Row.RowType = DataControlRowType.DataRow) Then
                e.Row.Attributes("onclick") = ClientGridId & ".PreSelectedIndexChanged" & "( this, " & e.Row.RowIndex & ")"
                e.Row.Attributes("ondblclick") = ClientGridId & ".SelectedIndexChanged" & "( this, " & e.Row.RowIndex & ")"
                e.Row.Attributes("ServerIndex") = CStr(e.Row.RowIndex)
            End If

            MyBase.OnRowDataBound(e)
        End If
    End Sub

    Protected Overrides Sub OnSelectedIndexChanged(ByVal e As System.EventArgs)
        MyBase.OnSelectedIndexChanged(e)
    End Sub

    Public Function GetClientScript() As String
        Dim writer As New StringBuilder("")

        Dim isPreSelectedDoPostBack As String = IIf(_ClickAction = EnumClickAction.OnClickSelectedPostBack, "true", "false").ToString()
        Dim isSelectedDoPostBack As String = IIf(_ClickAction = EnumClickAction.OnDblClickSelectedPostBack, "true", "false").ToString()

        writer.Append("<script type='text/javascript'>" & vbCrLf)
        writer.Append("    var " & ClientGridId & " = ERGridView.CreateNew('" & Me.ClientID & "' , '" & Me.UniqueID & "' , " & GridNum & " , '" & PreSelectedIndexControl.ClientID & "', '" & SelectedIndexControl.ClientID & "', '" & IsConfigureRequiredControl.ClientID & "' , '" & IsInitializeRequiredControl.ClientID & "' , '" & RowStyle.CssClass & "' , '" & AlternatingRowStyle.CssClass & "' , '" & PreSelectedStyleCssClass & "' , '" & SelectedRowStyle.CssClass & "' , " & isPreSelectedDoPostBack & " , " & isSelectedDoPostBack & " );" & vbCrLf)

        If _OnBeginPreSelect <> "" Then
            writer.Append(ClientGridId & ".OnBeginPreSelect = " & _OnBeginPreSelect & ";" & vbCrLf)
        End If
        If _OnBeginSelect <> "" Then
            writer.Append(ClientGridId & ".OnBeginSelect = " & _OnBeginSelect & ";" & vbCrLf)
        End If
        If _OnEndPreSelect <> "" Then
            writer.Append(ClientGridId & ".OnEndPreSelect = " & _OnEndPreSelect & ";" & vbCrLf)
        End If
        If _OnEndSelect <> "" Then
            writer.Append(ClientGridId & ".OnEndSelect = " & _OnEndSelect & ";" & vbCrLf)
        End If

        'writer.Append(ClientGridId & ".Initialize();//" & Page.IsPostBack & vbCrLf)

        writer.Append("</script>" & vbCrLf)
        Return writer.ToString
    End Function


#End Region

End Class
