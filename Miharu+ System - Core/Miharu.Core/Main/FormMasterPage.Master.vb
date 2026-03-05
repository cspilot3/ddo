Imports AjaxControlToolkit
Imports DBCore
Imports Miharu.Core.Clases

Namespace Main

    Public Class FormMasterPage
        Inherits System.Web.UI.MasterPage

        Implements IExceptionController

        Public MyPageBase As New FormBase

        Public ReadOnly Property ToolControl() As ToolControl
            Get
                Return ToolControl1
            End Get
        End Property

        Public ReadOnly Property MasterGridPanel() As UpdatePanel
            Get
                Return Me.updGrid
            End Get
        End Property

        Public ReadOnly Property MasterFilterPanel() As UpdatePanel
            Get
                Return Me.updFilter
            End Get
        End Property

        Public ReadOnly Property MasterDetailPanel() As UpdatePanel
            Get
                Return Me.updDetail
            End Get
        End Property

        Public ReadOnly Property MasterTabContainer() As TabContainer
            Get
                Return TabContainer1
            End Get
        End Property

        Public Property CurrentPanelName() As String
            Get
                Return CurrentPanelNameHidden.Value
            End Get
            Set(ByVal value As String)
                CurrentPanelNameHidden.Value = value
            End Set
        End Property


#Region " PROCEDIMIENTOS "

        Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Try
                If Not IsNothing(MyPageBase.MySesion.Pagina.Parameter("pagename")) Then
                    lblnamepage.Text = CStr(MyPageBase.MySesion.Pagina.Parameter("pagename"))
                End If

                If (Not cssStyles Is Nothing) Then
                    cssStyles.Attributes("href") = ResolveUrl("~/_styles/Styles.css")
                    cssDefault.Attributes("href") = ResolveUrl("~/_styles/Default.css")
                    cssModalPopUp.Attributes("href") = ResolveUrl("~/_styles/ModalPopUp/StyleSheetModalPopUp.css")
                    cssTabpanel.Attributes("href") = ResolveUrl("~/_styles/Tabpanel/TabpanelStyles.css")
                    cssMarco.Attributes("href") = ResolveUrl("~/_styles/Marco/StyleSheetMaster.css")
                End If

                If Not IsPostBack Then
                    lrtMasterScripts.Text = "<script src='" & ResolveUrl("~/_js/ConfirmBoxTemplate.js") & "' type='text/javascript'></script>"
                    lrtMasterScripts.Text &= "<script src='" & ResolveUrl("~/_js/CmiGridView.js") & "' type='text/javascript'></script>"

                    lrtMasterScripts.Text &= "<script src='" & ResolveUrl("~/_js/windows/javascripts/prototype.js") & "' type='text/javascript'></script>"
                    lrtMasterScripts.Text &= "<script src='" & ResolveUrl("~/_js/windows/javascripts/window.js") & "' type='text/javascript'></script>"
                    lrtMasterScripts.Text &= "<script src='" & ResolveUrl("~/_js/windows/javascripts/window_ext.js") & "' type='text/javascript'></script>"
                    lrtMasterScripts.Text &= "<script src='" & ResolveUrl("~/_js/windows/javascripts/effects.js") & "' type='text/javascript'></script>"
                    lrtMasterScripts.Text &= "<script src='" & ResolveUrl("~/_js/windows/javascripts/debug.js") & "' type='text/javascript'></script>"

                    'debe ser la ultima en cargarse
                    lrtMasterScripts.Text &= "<script src='" & ResolveUrl("~/_js/FormMasterPage.js") & "' type='text/javascript'></script>"


                    If (MyPageBase.BodyType = BodyType.Tabs) Then
                        divBodyTabs.Visible = True
                        divBodyUnique.Visible = False

                        TabContainer1.Width = Nothing
                        TabContainer1.ActiveTabIndex = 0
                    Else
                        divBodyTabs.Visible = False
                        divBodyUnique.Visible = True
                    End If
                    Load_Entidades()
                End If
            Catch ex As Exception

            End Try
        End Sub

        Private Sub Load_Entidades()
            Dim dm As DBCoreDataBaseManager
            dm = New DBCoreDataBaseManager(MyPageBase.ConnectionString.Core)
            Try
                dm.Connection_Open(MyPageBase.MySesion.Usuario.id)
                LabelEntidad.Text = MyPageBase.MySesion.Entidad.Nombre
                MyPageBase.MySesion.Parameter("id_entidad") = MyPageBase.MySesion.Entidad.id
            Catch ex As Exception
                dm.Connection_Close()
            Finally
                dm.Connection_Close()
            End Try
        End Sub
        Public Sub SetPageBase(ByRef mPageBase As FormBase)
            MyPageBase = mPageBase
        End Sub

        Public Sub ShowMessage(ByRef nMessage As String, ByVal nMsgBoxIcon As MsgBoxIcon, Optional ByRef nTitle As String = "") Implements IExceptionController.ShowMessage
            MessageBoxTemplate1.MsgBoxTitulo1.Text = nTitle
            MessageBoxTemplate1.MsgBoxMensaje1.Text = nMessage

            Select Case nMsgBoxIcon
                Case MsgBoxIcon.IconError
                    MessageBoxTemplate1.MsgBoxIcono1.ImageUrl = MainMasterPage.IconError

                Case MsgBoxIcon.IconInformation
                    MessageBoxTemplate1.MsgBoxIcono1.ImageUrl = MainMasterPage.IconInformation

                Case MsgBoxIcon.IconWarning
                    MessageBoxTemplate1.MsgBoxIcono1.ImageUrl = MainMasterPage.IconWarning

            End Select

            MessageBoxTemplate1.MsgBoxPopUp1.Show()
        End Sub

        Public Sub ShowConfirm()
            'Me.ConfirmBoxTemplate1.ConfirmText
        End Sub

        Public Function getLogPath() As String Implements IExceptionController.getLogPath
            Return Server.MapPath("~").TrimEnd("\"c) & "\Logs"
        End Function

        Public Sub ShowPopUp(ByVal WindowParams As String)
            AddEndRequest("ModalPopup", WindowParams)
        End Sub

        Public Sub ShowDialog(ByVal WindowParams As String)
            AddEndRequest("Unlock", "true")
            AddEndRequest("ShowDialog", WindowParams)
        End Sub

        Public Sub Alert(ByVal text As String)
            AddEndRequest("Alert", text)
        End Sub

        Public Sub SetActiveTabIndex(ByVal tab As FormBase.MasterTabType)
            Select Case tab
                Case FormBase.MasterTabType.Filter
                    updFilter.Update()
                Case FormBase.MasterTabType.Grid
                    updGrid.Update()
                Case FormBase.MasterTabType.Detail
                    updDetail.Update()
            End Select

            MasterTabContainer.ActiveTabIndex = Convert.ToInt32(tab)
            AddEndRequest("SetActiveTabIndex", CStr(tab))
        End Sub

        Public Function GetActiveTabIndex() As FormBase.MasterTabType
            Return CType([Enum].Parse(GetType(FormBase.MasterTabType), CStr(MasterTabContainer.TabIndex)), FormBase.MasterTabType)
        End Function

        Public Sub SelectText(ByVal ctl As TextBox)
            ctl.Attributes.Add("onfocus", ctl.ClientID + ".select();")
        End Sub

        Public Sub AddEndRequest(ByVal Action As String, ByVal Message As String)
            If (EndRequestAction.Value <> "") Then
                EndRequestAction.Value &= "|"
                EndRequestMessage.Value &= "|"
            End If
            EndRequestAction.Value &= Action
            EndRequestMessage.Value &= Message
        End Sub

        Private Sub ToolControl1_CommandAction(ByVal Action As String) Handles ToolControl1.CommandAction
            MyPageBase.RaiseCommandAction(Action)
        End Sub

        Protected Sub btnModalClosed_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnModalClosed.Click
            MyPageBase.RaiseModalClosed("")
        End Sub
#End Region

    End Class

    Public Enum BodyType
        Unique
        Tabs
    End Enum
End Namespace