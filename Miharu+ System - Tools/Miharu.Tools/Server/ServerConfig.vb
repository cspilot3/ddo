Public Class ServerConfig

#Region " Declaraciones "

    Private _Conexion_Fallida_IP_Max As Short = 0
    Private _Conexion_Fallida_User_Max As Short = 0

#End Region

#Region " Propiedades "

    Public Property Conexion_Fallida_IP_Max() As Short
        Get
            Return _Conexion_Fallida_IP_Max
        End Get
        Set(ByVal value As Short)
            _Conexion_Fallida_IP_Max = value
        End Set
    End Property
    Public Property Conexion_Fallida_User_Max() As Short
        Get
            Return _Conexion_Fallida_User_Max
        End Get
        Set(ByVal value As Short)
            _Conexion_Fallida_User_Max = value
        End Set
    End Property

#End Region

End Class
