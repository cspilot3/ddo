Public Class Mapeo

#Region " Declaraciones "

    Private Declare Function WNetAddConnection2 Lib "mpr.dll" Alias "WNetAddConnection2A" (ByRef lpNetResource As NETRESOURCE, ByVal lpPassword As String, ByVal lpUserName As String, ByVal dwFlags As Integer) As Integer
    Private Declare Function WNetCancelConnection2 Lib "mpr.dll" Alias "WNetCancelConnection2A" (ByVal lpName As String, ByVal dwFlags As Integer, ByVal fForce As Integer) As Integer

    Private Structure NETRESOURCE
        Dim dwScope As Integer
        Dim dwType As Integer
        Dim dwDisplayType As Integer
        Dim dwUsage As Integer
        Dim lpLocalName As String
        Dim lpRemoteName As String
        Dim lpComment As String
        Dim lpProvider As String
    End Structure

    ' The following includes all the constants defined for NETRESOURCE,
    ' not just the ones used in this example.
    Public Enum EnumNET_RESOURCE As Integer
        CONNECT_UPDATE_PROFILE = &H1S
        RESOURCETYPE_DISK = &H1S
        RESOURCETYPE_PRINT = &H2S
        RESOURCETYPE_ANY = &H0S
        RESOURCE_CONNECTED = &H1S
        RESOURCE_REMEMBERED = &H3S
        RESOURCE_GLOBALNET = &H2S
        RESOURCEDISPLAYTYPE_DOMAIN = &H1S
        RESOURCEDISPLAYTYPE_GENERIC = &H0S
        RESOURCEDISPLAYTYPE_SERVER = &H2S
        RESOURCEDISPLAYTYPE_SHARE = &H3S
        RESOURCEUSAGE_CONNECTABLE = &H1S
        RESOURCEUSAGE_CONTAINER = &H2S
    End Enum

    Public Enum EnumNET_ERROR As Integer
        NO_ERROR = 0
        ERROR_ACCESS_DENIED = 5
        ERROR_ALREADY_ASSIGNED = 85
        ERROR_BAD_DEV_TYPE = 66
        ERROR_BAD_DEVICE = 1200
        ERROR_BAD_NET_NAME = 67
        ERROR_BAD_PROFILE = 1206
        ERROR_BAD_PROVIDER = 1204
        ERROR_BUSY = 170
        ERROR_CANCELLED = 1223
        ERROR_CANNOT_OPEN_PROFILE = 1205
        ERROR_DEVICE_ALREADY_REMEMBERED = 1202
        ERROR_EXTENDED_ERROR = 1208
        ERROR_INVALID_PASSWORD = 86
        ERROR_NO_NET_OR_BAD_PATH = 1203
    End Enum

    Private _Unidad As String
    Private _Carpeta As String
    Private _Usuario As String
    Private _Contraseña As String

#End Region

#Region " Declaraciones "

    Public ReadOnly Property Unidad() As String
        Get
            Return _Unidad
        End Get
    End Property
    Public ReadOnly Property Carpeta() As String
        Get
            Return _Carpeta
        End Get
    End Property
    Public ReadOnly Property Usuario() As String
        Get
            Return _Usuario
        End Get
    End Property
    Public ReadOnly Property Contraseña() As String
        Get
            Return _Contraseña
        End Get
    End Property

#End Region

#Region " Metodos "

    Public Sub New(ByVal nUnidad As String, ByVal nCarpeta As String)
        _Unidad = nUnidad
        _Carpeta = nCarpeta
        _Usuario = Nothing
        _Contraseña = Nothing
    End Sub
    Public Sub New(ByVal nUnidad As String, ByVal nCarpeta As String, ByVal nUsuario As String, ByVal nContraseña As String)
        _Unidad = nUnidad
        _Carpeta = nCarpeta
        _Usuario = nUsuario
        _Contraseña = nContraseña
    End Sub

#End Region

#Region " Funciones "

    Public Function Conectar() As EnumNET_ERROR
        Dim NetR As NETRESOURCE = Nothing

        NetR.dwScope = EnumNET_RESOURCE.RESOURCE_GLOBALNET
        NetR.dwType = EnumNET_RESOURCE.RESOURCETYPE_DISK
        NetR.dwDisplayType = EnumNET_RESOURCE.RESOURCEDISPLAYTYPE_SHARE
        NetR.dwUsage = EnumNET_RESOURCE.RESOURCEUSAGE_CONNECTABLE
        NetR.lpLocalName = _Unidad ' If undefined, Connect with no device
        NetR.lpRemoteName = _Carpeta ' Your valid share
        'NetR.lpComment = "Optional Comment"
        'NetR.lpProvider =    ' Leave this undefined

        ' If the UserName and Password arguments are NULL, the user context
        ' for the process provides the default user name.
        Return CType(WNetAddConnection2(NetR, _Contraseña, _Usuario, EnumNET_RESOURCE.CONNECT_UPDATE_PROFILE), EnumNET_ERROR)
    End Function
    Public Function Desconectar() As EnumNET_ERROR
        Return CType(WNetCancelConnection2(_Unidad, EnumNET_RESOURCE.CONNECT_UPDATE_PROFILE, CInt(False)), EnumNET_ERROR)
    End Function
    Public Function MensajeError(ByVal nError As EnumNET_ERROR) As String
        Select Case nError
            Case EnumNET_ERROR.NO_ERROR
                Return "NO_ERROR"

            Case EnumNET_ERROR.ERROR_ACCESS_DENIED
                Return "ERROR_ACCESS_DENIED"

            Case EnumNET_ERROR.ERROR_ALREADY_ASSIGNED
                Return "ERROR ALREADY ASSIGNED"

            Case EnumNET_ERROR.ERROR_BAD_DEV_TYPE
                Return "ERROR BAD DEV TYPE"

            Case EnumNET_ERROR.ERROR_BAD_DEVICE
                Return "ERROR BAD DEVICE"

            Case EnumNET_ERROR.ERROR_BAD_NET_NAME
                Return "ERROR BAD NET NAME"

            Case EnumNET_ERROR.ERROR_BAD_PROFILE
                Return "ERROR BAD PROFILE"

            Case EnumNET_ERROR.ERROR_BAD_PROVIDER
                Return "ERROR BAD PROVIDER"

            Case EnumNET_ERROR.ERROR_BUSY
                Return "ERROR BUSY"

            Case EnumNET_ERROR.ERROR_CANCELLED
                Return "ERROR CANCELLED"

            Case EnumNET_ERROR.ERROR_CANNOT_OPEN_PROFILE
                Return "ERROR CANNOT OPEN PROFILE"

            Case EnumNET_ERROR.ERROR_DEVICE_ALREADY_REMEMBERED
                Return "ERROR DEVICE ALREADY REMEMBERED"

            Case EnumNET_ERROR.ERROR_EXTENDED_ERROR
                Return "ERROR EXTENDED ERROR"

            Case EnumNET_ERROR.ERROR_INVALID_PASSWORD
                Return "ERROR INVALID PASSWORD"

            Case EnumNET_ERROR.ERROR_NO_NET_OR_BAD_PATH
                Return "ERROR NO NET OR BAD PATH"

            Case Else
                Return ""

        End Select
    End Function

#End Region

End Class
