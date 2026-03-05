Public Class WebService

#Region " Declaraciones "

    Private _WebServiceURL As String
    Private _ServicioWebImaging As ImagingServiceReference.ImagingServiceSoapClient

#End Region

#Region " Propiedades "

    Public ReadOnly Property WebServiceURL() As String
        Get
            Return _WebServiceURL
        End Get
    End Property

#End Region

#Region " Metodos "

    Public Sub New(ByVal nWebServiceURL As String)
        Try
            _WebServiceURL = nWebServiceURL
            _ServicioWebImaging = GetImagingServiceReference(_WebServiceURL)
        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
        End Try
    End Sub

#End Region

#Region " Funciones "

    Public Shared Function GetImagingServiceReference(ByVal nWebServiceURL As String) As ImagingServiceReference.ImagingServiceSoapClient
        Dim binding As New System.ServiceModel.BasicHttpBinding
        Dim baseAddress As New System.ServiceModel.EndpointAddress(nWebServiceURL)
        Dim ws As ImagingServiceReference.ImagingServiceSoapClient

        binding.Name = "binding1"

        ws = New ImagingServiceReference.ImagingServiceSoapClient(binding, baseAddress)

        Return ws
    End Function

    Public Function ExistsKeyA(ByVal nEntidad As Short, ByVal nProyecto As Short, ByVal nEsquema As Short, ByVal nKey As String) As ImagingServiceReference.PR_ExistKey
        Dim Respuesta As ImagingServiceReference.PR_ExistKey

        Respuesta = _ServicioWebImaging.ExistsKeyA(nEntidad, nProyecto, nEsquema, nKey)

        Return Respuesta
    End Function
    Public Function ExistsKeyB(ByVal nEntidad As Short, ByVal nProyecto As Short, ByVal nEsquema As Short, ByVal nDocumento As Integer, ByVal nKey As String) As ImagingServiceReference.PR_ExistKey
        Dim Respuesta As ImagingServiceReference.PR_ExistKey

        Respuesta = _ServicioWebImaging.ExistsKeyB(nEntidad, nProyecto, nEsquema, nDocumento, nKey)

        Return Respuesta
    End Function

    'Public Function ServerConfig(ByVal nToken As String) As ImagingServiceReference.PR_ServerConfig
    '    Dim Respuesta As ImagingServiceReference.PR_ServerConfig

    '    Respuesta = _ServicioWebImaging.ServerConfig(nToken)

    '    Return Respuesta
    'End Function

    'Public Function FindImage(ByVal nEntidad As Short, ByVal nProyecto As Short, ByVal nEsquema As Short, ByVal nTipoCampo As Byte, ByVal nCampoBusqueda As Short, ByVal nValor As String) As ImagingServiceReference.PR_FindImage
    '    Dim Respuesta As ImagingServiceReference.PR_FindImage

    '    Respuesta = _ServicioWebImaging.FindImage(nEntidad, nProyecto, nEsquema, nTipoCampo, nCampoBusqueda, nValor)

    '    Return Respuesta
    'End Function

#End Region

End Class
