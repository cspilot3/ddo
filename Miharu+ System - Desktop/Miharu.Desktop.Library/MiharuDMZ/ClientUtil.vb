Imports Newtonsoft.Json
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Namespace MiharuDMZ

    Public Class ClientUtil

        Shared Sub New()
            ' Configura el protocolo de seguridad TLS 1.2 
            System.Net.ServicePointManager.SecurityProtocol = CType(3072, System.Net.SecurityProtocolType)
        End Sub

        Public Shared Function SendDataPostRequest(ByVal JsonPayload As String, ByVal UrlJson As String) As String
            Dim requestData As PostRequestModel = New PostRequestModel()
            requestData.JsonPayload = JsonPayload
            requestData.Url = UrlJson
            Dim webServiceDataBridge As Miharu.Desktop.Library.DataBridgeService.DataBridgeService = New Miharu.Desktop.Library.DataBridgeService.DataBridgeService
            Dim serialized As String = JsonConvert.SerializeObject(requestData)
            Dim encrypted As String = CryptoUtil.encrypt(serialized)
            Dim response As String = webServiceDataBridge.SendDataPostRequest(encrypted)
            Dim decrypted As String = CryptoUtil.decrypt(response)
            Dim queryResponse As String = decrypted
            Return queryResponse
        End Function

        Public Shared Function GetFolio(ByVal command As String, ByVal queryParameters As List(Of QueryParameter), ByVal queryRequestType As QueryRequestType, ByVal queryResponseType As QueryResponseType) As QueryResponse
            Dim queryRequest As QueryRequest = New QueryRequest()
            queryRequest.queryRequestType = queryRequestType
            queryRequest.queryResponseType = queryResponseType
            queryRequest.parameters = queryParameters
            Dim webServiceDataBridge As Miharu.Desktop.Library.DataBridgeService.DataBridgeService = New Miharu.Desktop.Library.DataBridgeService.DataBridgeService
            Dim serialized As String = JsonConvert.SerializeObject(queryRequest)
            Dim encrypted As String = CryptoUtil.encrypt(serialized)
            Dim response As String = webServiceDataBridge.GetFolio(encrypted)
            Dim decrypted As String = CryptoUtil.decrypt(response)
            Dim queryResponse As QueryResponse = JsonConvert.DeserializeObject(Of QueryResponse)(decrypted)
            Return queryResponse
        End Function

        Public Shared Function GetFoliosCargueItem(ByVal command As String, ByVal queryParameters As List(Of QueryParameter), ByVal queryRequestType As QueryRequestType, ByVal queryResponseType As QueryResponseType) As QueryResponse
            Dim queryRequest As QueryRequest = New QueryRequest()
            queryRequest.queryRequestType = queryRequestType
            queryRequest.queryResponseType = queryResponseType
            queryRequest.parameters = queryParameters
            Dim webServiceDataBridge As Miharu.Desktop.Library.DataBridgeService.DataBridgeService = New Miharu.Desktop.Library.DataBridgeService.DataBridgeService
            Dim serialized As String = JsonConvert.SerializeObject(queryRequest)
            Dim encrypted As String = CryptoUtil.encrypt(serialized)
            Dim response As String = webServiceDataBridge.GetFoliosCargueItem(encrypted)
            Dim decrypted As String = CryptoUtil.decrypt(response)
            Dim queryResponse As QueryResponse = JsonConvert.DeserializeObject(Of QueryResponse)(decrypted)
            Return queryResponse
        End Function

        Public Shared Function GetFolioCargueItem(ByVal command As String, ByVal queryParameters As List(Of QueryParameter), ByVal queryRequestType As QueryRequestType, ByVal queryResponseType As QueryResponseType) As QueryResponse
            Dim queryRequest As QueryRequest = New QueryRequest()
            queryRequest.queryRequestType = queryRequestType
            queryRequest.queryResponseType = queryResponseType
            queryRequest.parameters = queryParameters
            Dim webServiceDataBridge As Miharu.Desktop.Library.DataBridgeService.DataBridgeService = New Miharu.Desktop.Library.DataBridgeService.DataBridgeService
            Dim serialized As String = JsonConvert.SerializeObject(queryRequest)
            Dim encrypted As String = CryptoUtil.encrypt(serialized)
            Dim response As String = webServiceDataBridge.GetFolioCargueItem(encrypted)
            Dim decrypted As String = CryptoUtil.decrypt(response)
            Dim queryResponse As QueryResponse = JsonConvert.DeserializeObject(Of QueryResponse)(decrypted)
            Return queryResponse
        End Function

        Public Shared Function GetFoliosFile(ByVal command As String, ByVal queryParameters As List(Of QueryParameter), ByVal queryRequestType As QueryRequestType, ByVal queryResponseType As QueryResponseType) As QueryResponse
            Dim queryRequest As QueryRequest = New QueryRequest()
            queryRequest.queryRequestType = queryRequestType
            queryRequest.queryResponseType = queryResponseType
            queryRequest.parameters = queryParameters
            Dim webServiceDataBridge As Miharu.Desktop.Library.DataBridgeService.DataBridgeService = New Miharu.Desktop.Library.DataBridgeService.DataBridgeService
            Dim serialized As String = JsonConvert.SerializeObject(queryRequest)
            Dim encrypted As String = CryptoUtil.encrypt(serialized)
            Dim response As String = webServiceDataBridge.GetFoliosFile(encrypted)
            Dim decrypted As String = CryptoUtil.decrypt(response)
            Dim queryResponse As QueryResponse = JsonConvert.DeserializeObject(Of QueryResponse)(decrypted)
            Return queryResponse
        End Function

        Public Shared Function GetFolioFile(ByVal command As String, ByVal queryParameters As List(Of QueryParameter), ByVal queryRequestType As QueryRequestType, ByVal queryResponseType As QueryResponseType) As QueryResponse
            Dim queryRequest As QueryRequest = New QueryRequest()
            queryRequest.queryRequestType = queryRequestType
            queryRequest.queryResponseType = queryResponseType
            queryRequest.parameters = queryParameters
            Dim webServiceDataBridge As Miharu.Desktop.Library.DataBridgeService.DataBridgeService = New Miharu.Desktop.Library.DataBridgeService.DataBridgeService
            Dim serialized As String = JsonConvert.SerializeObject(queryRequest)
            Dim encrypted As String = CryptoUtil.encrypt(serialized)
            Dim response As String = webServiceDataBridge.GetFolioFile(encrypted)
            Dim decrypted As String = CryptoUtil.decrypt(response)
            Dim queryResponse As QueryResponse = JsonConvert.DeserializeObject(Of QueryResponse)(decrypted)
            Return queryResponse
        End Function

        Public Shared Function ImageCount(ByVal command As String, ByVal queryParameters As List(Of QueryParameter), ByVal queryRequestType As QueryRequestType, ByVal queryResponseType As QueryResponseType) As QueryResponse
            Dim queryRequest As QueryRequest = New QueryRequest()
            queryRequest.queryRequestType = queryRequestType
            queryRequest.queryResponseType = queryResponseType
            queryRequest.parameters = queryParameters
            Dim webServiceDataBridge As Miharu.Desktop.Library.DataBridgeService.DataBridgeService = New Miharu.Desktop.Library.DataBridgeService.DataBridgeService
            Dim serialized As String = JsonConvert.SerializeObject(queryRequest)
            Dim encrypted As String = CryptoUtil.encrypt(serialized)
            Dim response As String = webServiceDataBridge.ImageCount(encrypted)
            Dim decrypted As String = CryptoUtil.decrypt(response)
            Dim queryResponse As QueryResponse = JsonConvert.DeserializeObject(Of QueryResponse)(decrypted)
            Return queryResponse
        End Function

        Public Shared Function resolver(ByVal command As String, ByVal queryParameters As List(Of QueryParameter), ByVal queryRequestType As QueryRequestType, ByVal queryResponseType As QueryResponseType) As QueryResponse
            Dim queryRequest As QueryRequest = New QueryRequest()
            queryRequest.name = command
            queryRequest.queryRequestType = queryRequestType
            queryRequest.queryResponseType = queryResponseType
            queryRequest.parameters = queryParameters
            Dim webServiceDataBridge As Miharu.Desktop.Library.DataBridgeService.DataBridgeService = New Miharu.Desktop.Library.DataBridgeService.DataBridgeService
            Dim serialized As String = JsonConvert.SerializeObject(queryRequest)
            Dim encrypted As String = CryptoUtil.encrypt(serialized)
            Dim response As String = webServiceDataBridge.DataBridge(encrypted)
            Dim decrypted As String = CryptoUtil.decrypt(response)
            Dim queryResponse As QueryResponse = JsonConvert.DeserializeObject(Of QueryResponse)(decrypted)
            Return queryResponse
        End Function

        Public Shared Function CreateImageFolioManager(ByVal command As String, ByVal queryParameters As List(Of QueryParameter), ByVal queryRequestType As QueryRequestType, ByVal queryResponseType As QueryResponseType) As QueryResponse
            Dim queryRequest As QueryRequest = New QueryRequest()
            queryRequest.queryRequestType = queryRequestType
            queryRequest.queryResponseType = queryResponseType
            queryRequest.parameters = queryParameters
            Dim webServiceDataBridge As Miharu.Desktop.Library.DataBridgeService.DataBridgeService = New Miharu.Desktop.Library.DataBridgeService.DataBridgeService
            Dim serialized As String = JsonConvert.SerializeObject(queryRequest)
            Dim encrypted As String = CryptoUtil.encrypt(serialized)
            Dim response As String = webServiceDataBridge.CreateImageFolioManager(encrypted)
            Dim decrypted As String = CryptoUtil.decrypt(response)
            Dim queryResponse As QueryResponse = JsonConvert.DeserializeObject(Of QueryResponse)(decrypted)
            Return queryResponse
        End Function

        Public Shared Function mapToTypedTable(ByVal destinationTable As DataTable, ByVal originDataTable As DataTable) As DataTable

            ' Recorre cada fila de la tabla origen
            For Each row As DataRow In originDataTable.Rows

                Dim newRow As DataRow = destinationTable.NewRow()               ' Crea una nueva fila para la tabla destino

                ' Copia los valores en las columnas correspondientes por nombre
                For Each column As DataColumn In originDataTable.Columns
                    If destinationTable.Columns.Contains(column.ColumnName) Then

                        Dim value = row(column.ColumnName)

                        If destinationTable.Columns(column.ColumnName).DataType Is GetType(Byte()) Then   ' Verifica si la columna es de tipo Byte[] y convierte si es necesario

                            If TypeOf value Is String Then                                                ' Si el valor es una cadena, intenta convertirlo de base64 a Byte[]
                                Try
                                    Dim byteArray As Byte() = Convert.FromBase64String(CType(value, String))
                                    newRow(column.ColumnName) = byteArray
                                Catch ex As FormatException
                                    newRow(column.ColumnName) = Nothing
                                End Try
                            ElseIf TypeOf value Is Byte() Then                                             ' Si ya es Byte[], simplemente asigna
                                newRow(column.ColumnName) = CType(value, Byte())
                            Else                                                                           ' Manejo del error: tipo de dato inesperado
                                newRow(column.ColumnName) = Nothing
                            End If
                        Else                                                                                ' Asigna el valor normalmente si no es un Byte[]
                            newRow(column.ColumnName) = value
                        End If
                    End If
                Next

                ' Agrega la nueva fila con los valores copiados en destinationTable
                destinationTable.Rows.Add(newRow)
            Next
            destinationTable.AcceptChanges()
            Return destinationTable
        End Function
    End Class
End Namespace

