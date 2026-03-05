Public Class SessionData
    Private _IsValidUser As Boolean = False
    Private _CurrentUser As New Usuario

    Public Property IsValidUser() As Boolean
        Get
            Return _IsValidUser
        End Get
        Set(ByVal value As Boolean)
            _IsValidUser = value
        End Set
    End Property

    Public Property CurrentUser() As Usuario
        Get
            Return _CurrentUser
        End Get
        Set(ByVal value As Usuario)
            _CurrentUser = value
        End Set
    End Property
End Class
