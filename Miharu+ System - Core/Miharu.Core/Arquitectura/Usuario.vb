Public Enum TipoUsuario
    NN
    COR
    OPE
    NAL
End Enum

Public Class Usuario
    Private _Cod_Usuario As Integer = -1
    Private _Opd_cedula As String = ""
    Private _Nombre As String = "Anonimo"
    Private _Login As String = ""
    Private _Contraseña As String = ""
    Private _Email As String = ""
    Private _Solicitar_cambio_clave As Boolean = True
    Private _Fecha_Activacion As New DateTime
    Private _Fecha_Final As New DateTime
    Private _Fecha_Inicio_Sesion As New DateTime
    Private _Activo As Boolean = False
    Private _Fk_Sep_Sedo_Sede_Domesa As String = ""
    Private _Cc1_Codigo As String = ""
    Private _Cc1_Nombre As String = ""
    Private _Cod_Tipo_Usuario As TipoUsuario = TipoUsuario.NN
    Private _Sep_Sedo_Nombre As String = ""
    Private _Num_Historial As String = ""

    Public Property Cod_Usuario() As Integer
        Get
            Return _Cod_Usuario
        End Get
        Set(ByVal value As Integer)
            _Cod_Usuario = value
        End Set
    End Property

    Public Property Opd_cedula() As String
        Get
            Return _Opd_cedula
        End Get
        Set(ByVal value As String)
            _Opd_cedula = value
        End Set
    End Property

    Public Property Nombre() As String
        Get
            Return _Nombre
        End Get
        Set(ByVal value As String)
            _Nombre = value
        End Set
    End Property

    Public Property Login() As String
        Get
            Return _Login
        End Get
        Set(ByVal value As String)
            _Login = value
        End Set
    End Property

    Public Property Contraseña() As String
        Get
            Return _Contraseña
        End Get
        Set(ByVal value As String)
            _Contraseña = value
        End Set
    End Property

    Public Property Email() As String
        Get
            Return _Email
        End Get
        Set(ByVal value As String)
            _Email = value
        End Set
    End Property

    Public Property Solicitar_cambio_clave() As Boolean
        Get
            Return _Solicitar_cambio_clave
        End Get
        Set(ByVal value As Boolean)
            _Solicitar_cambio_clave = value
        End Set
    End Property

    Public Property Fecha_Activacion() As DateTime
        Get
            Return _Fecha_Activacion
        End Get
        Set(ByVal value As DateTime)
            _Fecha_Activacion = value
        End Set
    End Property

    Public Property Fecha_Final() As DateTime
        Get
            Return _Fecha_Final
        End Get
        Set(ByVal value As DateTime)
            _Fecha_Final = value
        End Set
    End Property

    Public Property Fecha_Inicio_Sesion() As DateTime
        Get
            Return _Fecha_Inicio_Sesion
        End Get
        Set(ByVal value As DateTime)
            _Fecha_Inicio_Sesion = value
        End Set
    End Property

    Public Property Activo() As Boolean
        Get
            Return _Activo
        End Get
        Set(ByVal value As Boolean)
            _Activo = value
        End Set
    End Property

    Public Property Fk_Sep_Sedo_Sede_Domesa() As String
        Get
            Return _Fk_Sep_Sedo_Sede_Domesa
        End Get
        Set(ByVal value As String)
            _Fk_Sep_Sedo_Sede_Domesa = value
        End Set
    End Property

    Public Property Cc1_Codigo() As String
        Get
            Return _Cc1_Codigo
        End Get
        Set(ByVal value As String)
            _Cc1_Codigo = value
        End Set
    End Property

    Public Property Cc1_Nombre() As String
        Get
            Return _Cc1_Nombre
        End Get
        Set(ByVal value As String)
            _Cc1_Nombre = value
        End Set
    End Property

    Public Property Cod_Tipo_Usuario() As TipoUsuario
        Get
            Return _Cod_Tipo_Usuario
        End Get
        Set(ByVal value As TipoUsuario)
            _Cod_Tipo_Usuario = value
        End Set
    End Property

    Public Property Sep_Sedo_Nombre() As String
        Get
            Return _Sep_Sedo_Nombre
        End Get
        Set(ByVal value As String)
            _Sep_Sedo_Nombre = value
        End Set
    End Property

    Public Property Num_Historial() As String
        Get
            Return _Num_Historial
        End Get
        Set(ByVal value As String)
            _Num_Historial = value
        End Set
    End Property
End Class
