
Public Class CanjeInfo
    'Public RegistroValido As Boolean = True

    Public Correlativo As String = ""
    Public Ruta As String = ""
    Public Cuenta As String = ""
    Public Serie As String = ""
    Public Valor As String = ""
    Public BancoDestino As String = ""

    Public Oficina As String = ""

    Public Indexado As Boolean = False
    Public fk_Expediente As Long = 0

    Public Imagenes As New List(Of Byte())
    Public ImagenesMini As New List(Of Byte())
End Class
