Imports Miharu.Core.Clases
Imports Miharu

Public Class PopupBase
    Inherits FormBase

    Public Event PopupPageBase_Load(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Overrides ReadOnly Property RequiredValidUser() As Boolean
        Get
            Return False
        End Get
    End Property

    Public Property PopupGridData() As DataTable
        Get
            Return CType(Session("BusquedaGridData"), DataTable)
        End Get
        Set(ByVal value As DataTable)
            Session("BusquedaGridData") = value
        End Set
    End Property


    Public ReadOnly Property MyPopupMasterpage() As PopupMasterPage
        Get
            Return CType(Me.Master, PopupMasterPage)
        End Get
    End Property

    Public Sub CloseWindow(ByVal result As Boolean)
        PopupGridData = Nothing
        Try
            CType(Me.Master, PopupMasterPage).CloseWindow(result)
        Catch 'ex As Exception
        End Try
    End Sub

    Public Sub ShowMessage(ByVal Message As String)
        Try
            CType(Me.Master, PopupMasterPage).ShowMessage(Message)
        Catch 'ex As Exception
        End Try
    End Sub

    Public Overrides ReadOnly Property ClearPageData() As Boolean
        Get
            Return False
        End Get
    End Property

    Public Sub ParentRedirect(ByVal relativeUrl As String, ByVal paramsUrl As String)
        MyPopupMasterpage.ParentRedirect(relativeUrl, paramsUrl)
    End Sub
End Class
