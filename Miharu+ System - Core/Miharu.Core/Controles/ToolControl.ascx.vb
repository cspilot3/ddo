
Imports Miharu.Core.Clases

Partial Public Class ToolControl
    Inherits System.Web.UI.UserControl

    Public Event CommandAction(ByVal Action As String)

    Protected Overrides Sub OnInit(ByVal e As System.EventArgs)
        MyBase.OnInit(e)
        If Not IsPostBack Then
            AddHiddenCommandName(EnabledCommandsHiddenField, "New")
            AddHiddenCommandName(EnabledCommandsHiddenField, "Find")
            AddHiddenCommandName(EnabledCommandsHiddenField, "Edit")
            AddHiddenCommandName(EnabledCommandsHiddenField, "Delete")
            AddHiddenCommandName(EnabledCommandsHiddenField, "Save")


            'cambio realizado por simon ariza 2010-02-26 (se habilita todo el toolbox en el momento de cargar el control)

            'AddHiddenCommandName(FilterCommandsHiddenField, "New")
            'AddHiddenCommandName(FilterCommandsHiddenField, "Find")
            'AddHiddenCommandName(ListCommandsHiddenField, "New")
            'AddHiddenCommandName(ListCommandsHiddenField, "Edit")
            'AddHiddenCommandName(DetailCommandsHiddenField, "New")
            'AddHiddenCommandName(DetailCommandsHiddenField, "Save")
            'AddHiddenCommandName(DetailCommandsHiddenField, "Delete")


            AddHiddenCommandName(FilterCommandsHiddenField, "New")
            AddHiddenCommandName(FilterCommandsHiddenField, "Find")
            AddHiddenCommandName(FilterCommandsHiddenField, "Edit")
            AddHiddenCommandName(FilterCommandsHiddenField, "Delete")
            AddHiddenCommandName(FilterCommandsHiddenField, "Save")
            AddHiddenCommandName(ListCommandsHiddenField, "New")
            AddHiddenCommandName(ListCommandsHiddenField, "Find")
            AddHiddenCommandName(ListCommandsHiddenField, "Edit")
            AddHiddenCommandName(ListCommandsHiddenField, "Save")
            AddHiddenCommandName(ListCommandsHiddenField, "Delete")
            AddHiddenCommandName(DetailCommandsHiddenField, "New")
            AddHiddenCommandName(DetailCommandsHiddenField, "Find")
            AddHiddenCommandName(DetailCommandsHiddenField, "Edit")
            AddHiddenCommandName(DetailCommandsHiddenField, "Delete")
            AddHiddenCommandName(DetailCommandsHiddenField, "Save")


            'fin del cambio

            EnableAction("New")
            EnableAction("Find")
            EnableAction("Edit")
            EnableAction("Delete")
            EnableAction("Save")

        End If
    End Sub

    Public ReadOnly Property EnabledCommands() As String()
        Get
            Return EnabledCommandsHiddenField.Value.Split(","c)
        End Get
    End Property

    Public ReadOnly Property FilterCommands() As String()
        Get
            Return FilterCommandsHiddenField.Value.Split(","c)
        End Get
    End Property

    Public ReadOnly Property ListCommands() As String()
        Get
            Return ListCommandsHiddenField.Value.Split(","c)
        End Get
    End Property

    Public ReadOnly Property DetailCommands() As String()
        Get
            Return DetailCommandsHiddenField.Value.Split(","c)
        End Get
    End Property

    Public ReadOnly Property UniqueCommands() As String()
        Get
            Return UniqueCommandsHiddenField.Value.Split(","c)
        End Get
    End Property

    Public Sub AddHiddenCommandName(ByRef ContainerHiddenField As HiddenField, ByVal CommandName As String)
        Dim items = ContainerHiddenField.Value.Split(","c)
        For Each item In items
            If item = CommandName Then Return
        Next
        If (ContainerHiddenField.Value <> "") Then ContainerHiddenField.Value &= ","
        ContainerHiddenField.Value &= CommandName
    End Sub

    Public Sub RemoveHiddenCommandName(ByRef ContainerHiddenField As HiddenField, ByVal CommandName As String)
        Dim items = ContainerHiddenField.Value.Split(","c)
        ContainerHiddenField.Value = ""
        For Each item In items
            If (item <> CommandName) Then
                AddHiddenCommandName(ContainerHiddenField, item)
            End If
        Next
    End Sub

    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'If (Not IsPostBack) Then
        '    EnableAction("New")
        '    EnableAction("Edit")
        '    EnableAction("Delete")
        '    EnableAction("Find")
        '    EnableAction("Save")
        'End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Not IsPostBack) Then
            '    tblDummy.Visible = False
            '    If (MyPage.ShowToolBox) Then
            '        dtlCommandTools.Visible = True
            '        PageAction_Load()
            '    Else
            '        dtlCommandTools.Visible = False
            '    End If
        End If

    End Sub

    Public ReadOnly Property MyPage() As FormBase
        Get
            Return CType(Page, FormBase)
        End Get
    End Property

    Public ReadOnly Property ActionList() As StringCollection
        Get
            If (Session("ToolControl_ActionList") Is Nothing) Then
                Session("ToolControl_ActionList") = New StringCollection
            End If
            Return CType(Session("ToolControl_ActionList"), StringCollection)
        End Get
    End Property

    Public Function IsValidAction(ByVal action As String) As Boolean
        Return ActionList.Contains(action)
    End Function

    Public Sub Load_Tools()
        'ActionList.Clear()
        ''If (MyPage.ShowToolBox) Then
        'Dim dataController As DataController = DataFactory.CreateDefaultDataController()
        'Dim data As New DataSet

        'Dim parameters As New CmdParameterCollection
        'parameters.Add("In_COD_USUARIO", MyPage.SessionData.CurrentUser.Cod_Usuario)
        'parameters.Add("In_COD_MAPA_MODULO", MyPage.Cod_Mapa_Modulo)

        'data = dataController.ExecuteQuery("PKG_INT_CONS_MENU_ACCION.INT_CONS_MENU_ACCION", parameters)

        'dtlCommandTools.DataSource = data
        'dtlCommandTools.DataBind()

        'updTool.Update()
        'End If
    End Sub

    Public Sub Update()
        updTool.Update()
    End Sub

#Region "_   CONTROL DE ACCIONES   _"
    Private Sub Config_CommandAction(ByVal Action As String)
        'Select Case Action
        '    Case "New"
        '        PageAction_New()
        '    Case "Find"
        '        PageAction_Find()
        '    Case "Edit"
        '        PageAction_Edit()
        '    Case "Delete"
        '        PageAction_Delete()
        '    Case "Save"
        '        PageAction_Save()
        'End Select
    End Sub

    Private Sub PageAction_Load()

        EnableAction("New")
        EnableAction("Find")
        DisableAction("Delete")
        DisableAction("Save")
        DisableAction("Edit")
    End Sub

    'Private Sub PageAction_New()
    '    DisableAction("Delete")
    '    EnableAction("Save")
    'End Sub

    'Private Sub PageAction_Find()
    '    DisableAction("Delete")
    '    DisableAction("Save")
    '    DisableAction("Edit")
    'End Sub

    'Private Sub PageAction_Edit()
    '    EnableAction("Delete")
    '    EnableAction("Save")
    'End Sub

    'Private Sub PageAction_Delete()
    '    DisableAction("Edit")
    '    DisableAction("Delete")
    '    DisableAction("Save")
    'End Sub

    'Private Sub PageAction_Save()
    '    DisableAction("Edit")
    '    DisableAction("Delete")
    '    DisableAction("Save")
    'End Sub
#End Region

    Protected Sub imgNew_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgNew.Click
        RaiseEvent CommandAction("New")
    End Sub

    Protected Sub imgFind_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgFind.Click
        RaiseEvent CommandAction("Find")
    End Sub

    Protected Sub imgDelete_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgDelete.Click
        RaiseEvent CommandAction("Delete")
    End Sub

    Protected Sub imgEdit_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgEdit.Click
        RaiseEvent CommandAction("Edit")
    End Sub

    Protected Sub imgSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSave.Click
        RaiseEvent CommandAction("Save")
    End Sub

    Public Sub AddCommand(ByVal CommandName As String, ByVal ImageUrl As String, Optional ByVal isFilterTab As Boolean = False, Optional ByVal isListTab As Boolean = False, Optional ByVal isDetailTab As Boolean = True)
        Dim tdControl As New HtmlTableCell("td")
        tdControl.ID = "tdControl" & CommandName
        trControl.Controls.Add(tdControl)

        Dim divAction As New HtmlGenericControl("div")
        divAction.ID = "divAction" & CommandName
        divAction.Attributes("class") = "ToolDiv"
        divAction.Attributes.Add("ActionName", CommandName)
        tdControl.Controls.Add(divAction)

        Dim img As New ImageButton()
        img.ID = "img" & CommandName
        img.CommandName = CommandName
        img.CommandArgument = CommandName
        img.ImageUrl = ImageUrl
        img.Attributes("onclick") = ImageArgumentHiddenField.ClientID & ".value = '" & CommandName & "';__doPostBack('ctl00$MasterTool$ToolControl1$ImageCommandLinkButton','')"
        divAction.Controls.Add(img)

        Dim divDisabled As New HtmlGenericControl("div")
        divDisabled.ID = "divDisabled" & CommandName
        divDisabled.Attributes("class") = "ToolDivInDisabled"
        divDisabled.Style("display") = "none"
        divAction.Controls.Add(divDisabled)

        AddHiddenCommandName(EnabledCommandsHiddenField, CommandName)
        If isFilterTab Then AddHiddenCommandName(FilterCommandsHiddenField, CommandName)
        If isListTab Then AddHiddenCommandName(ListCommandsHiddenField, CommandName)
        If isDetailTab Then AddHiddenCommandName(DetailCommandsHiddenField, CommandName)
    End Sub

    Protected Sub ImageCommandLinkButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ImageCommandLinkButton.Click
        RaiseEvent CommandAction(ImageArgumentHiddenField.Value)
    End Sub

    Public Sub DisableVisible(ByVal CommandName As String)
        Dim tdControl As HtmlTableCell = CType(trControl.FindControl("tdControl" & CommandName), HtmlTableCell)
        Dim divAction As HtmlGenericControl = CType(tdControl.FindControl("divAction" & CommandName), HtmlGenericControl)
        Dim img As ImageButton = CType(divAction.FindControl("img" & CommandName), ImageButton)
        Dim divDisabled As HtmlGenericControl = CType(divAction.FindControl("divDisabled" & CommandName), HtmlGenericControl)

        img.Enabled = False
        img.Attributes("visibility") = "hidden"
        divDisabled.Style("display") = "inline-block"

        RemoveHiddenCommandName(EnabledCommandsHiddenField, CommandName)

        ' cambio realizado por simon ariza 26-02-2010 (se habilitan las opciones de bloqueo del menu de filtro lista y detalle)

        RemoveHiddenCommandName(FilterCommandsHiddenField, CommandName)
        RemoveHiddenCommandName(ListCommandsHiddenField, CommandName)
        RemoveHiddenCommandName(DetailCommandsHiddenField, CommandName)

        'cambio Lady Cifuentes  4-06-2010
        tdControl.Visible = False
        Update()
        ' fin del cambio
    End Sub

    Public Sub DisableAction(ByVal CommandName As String)
        Dim tdControl As HtmlTableCell = CType(trControl.FindControl("tdControl" & CommandName), HtmlTableCell)
        Dim divAction As HtmlGenericControl = CType(tdControl.FindControl("divAction" & CommandName), HtmlGenericControl)
        Dim img As ImageButton = CType(divAction.FindControl("img" & CommandName), ImageButton)
        Dim divDisabled As HtmlGenericControl = CType(divAction.FindControl("divDisabled" & CommandName), HtmlGenericControl)

        img.Enabled = False

        divDisabled.Style("display") = "inline-block"

        RemoveHiddenCommandName(EnabledCommandsHiddenField, CommandName)

        ' cambio realizado por simon ariza 26-02-2010 (se habilitan las opciones de bloqueo del menu de filtro lista y detalle)

        RemoveHiddenCommandName(FilterCommandsHiddenField, CommandName)
        RemoveHiddenCommandName(ListCommandsHiddenField, CommandName)
        RemoveHiddenCommandName(DetailCommandsHiddenField, CommandName)

        ' fin del cambio
    End Sub

    Public Sub EnableAction(ByVal CommandName As String)
        Dim tdControl As HtmlTableCell = CType(trControl.FindControl("tdControl" & CommandName), HtmlTableCell)
        Dim divAction As HtmlGenericControl = CType(tdControl.FindControl("divAction" & CommandName), HtmlGenericControl)
        Dim img As ImageButton = CType(divAction.FindControl("img" & CommandName), ImageButton)
        Dim divDisabled As HtmlGenericControl = CType(divAction.FindControl("divDisabled" & CommandName), HtmlGenericControl)

        img.Enabled = True
        divDisabled.Style("display") = "none"

        AddHiddenCommandName(EnabledCommandsHiddenField, CommandName)
    End Sub
End Class
