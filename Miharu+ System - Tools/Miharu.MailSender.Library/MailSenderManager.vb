Imports System.Net.Mail
Imports System.Text.RegularExpressions
Imports Newtonsoft.Json
Imports System.Net
Imports System.IO
Imports System.Text

Public Class MailSenderManager

    Public Class EmailData
        Public Property Subject As String
        Public Property From As String
        Public Property Template As TemplateData
        'Public Property PreviewText As String
        Public Property ReplyTo As String
        'Public Property Parameters As List(Of ParameterData)
        Public Property Attachments As List(Of AttachmentData)
        Public Property Recipients As List(Of RecipientData)
    End Class

    Public Class TemplateData
        Public Property Type As String
        Public Property Value As String
    End Class

    Public Class ParameterData
        Public Property Name As String
        Public Property Type As String
        Public Property Value As String
    End Class

    Public Class AttachmentData
        Public Property Path As String
        Public Property Filename As String
        'Public Property Action As String
        'Public Property Password As String
    End Class

    Public Class RecipientData
        Public Property [To] As String
    End Class

    ' Clases para deserializar la respuesta de la API
    Public Class ApiResponse
        Public Property status As String
        Public Property description As String
        Public Property data As ResponseData
    End Class

    Public Class ResponseData
        Public Property deliveryId As String
    End Class

#Region " Declaraciones "

    ' Cliente Correo
    Private _Client As SmtpClient

    ' Parametros base
    Private _FromMailAddress As String
    Private _FromMailDisplay As String

    Private _SMTPServerAddress As String
    Private _Port As Integer
    Private _User As String
    Private _Password As String
    Private _EnabledSSL As Boolean

    Private _URLAPIMasiv As String
    Private _UserAPIMasiv As String
    Private _PasswordAPIMasiv As String
    Private _AditionalEmailAdress As String

#End Region

#Region " Propiedades "

    Public Property FromMailAddress() As String
        Get
            Return _FromMailAddress
        End Get
        Set(ByVal value As String)
            _FromMailAddress = value
        End Set
    End Property
    Public Property FromMailDisplay() As String
        Get
            Return _FromMailDisplay
        End Get
        Set(ByVal value As String)
            _FromMailDisplay = value
        End Set
    End Property

    Public Property SMTPServerAddress() As String
        Get
            Return _SMTPServerAddress
        End Get
        Set(ByVal value As String)
            _SMTPServerAddress = value
        End Set
    End Property
    Public Property Port() As Integer
        Get
            Return _Port
        End Get
        Set(ByVal value As Integer)
            _Port = value
        End Set
    End Property
    Public Property User() As String
        Get
            Return _User
        End Get
        Set(ByVal value As String)
            _User = value
        End Set
    End Property
    Public Property Password() As String
        Get
            Return _Password
        End Get
        Set(ByVal value As String)
            _Password = value
        End Set
    End Property
    Public Property EnabledSSL() As Boolean
        Get
            Return _EnabledSSL
        End Get
        Set(ByVal value As Boolean)
            _EnabledSSL = value
        End Set
    End Property

    Public Property URLAPIMasiv() As String
        Get
            Return _URLAPIMasiv
        End Get
        Set(ByVal value As String)
            _URLAPIMasiv = value
        End Set
    End Property

    Public Property UserAPIMasiv() As String
        Get
            Return _UserAPIMasiv
        End Get
        Set(ByVal value As String)
            _UserAPIMasiv = value
        End Set
    End Property

    Public Property PasswordAPIMasiv() As String
        Get
            Return _PasswordAPIMasiv
        End Get
        Set(ByVal value As String)
            _PasswordAPIMasiv = value
        End Set
    End Property

    Public Property AditionalEmailAdress() As String
        Get
            Return _AditionalEmailAdress
        End Get
        Set(ByVal value As String)
            _AditionalEmailAdress = value
        End Set
    End Property

#End Region

#Region " Metodos "

    Public Sub New()
        _Client = New SmtpClient()
    End Sub

    Public Sub SendMailTracking(ByRef _DBMMailSender As DBTools.DBToolsDataBaseManager, ByRef DBCore As DBCore.DBCoreDataBaseManager, ByVal RowQueue As DBTools.SchemaMail.TBL_QueueRow, ByVal nAdjuntos() As String)

        Dim nEmailFrom As String = RowQueue.EmailFrom                                              ' Email Remitente
        Dim nEmailFromDisplay As String = RowQueue.EmailFromDisplay                                ' Alias Remitente
        Dim nEmailAddress As String = RowQueue.EmailAddress_Queue + ";" + _AditionalEmailAdress    ' Emails Destinatario
        Dim nCC As String = RowQueue.CC_Queue                                                      ' Emails Copia Destinatario
        Dim nCCO As String = RowQueue.CCO_Queue                                                    ' Email Copia Oculta Destinatario
        Dim nSubject As String = RowQueue.Subject_Queue                                            ' Asunto Email 
        Dim nMensaje As String = RowQueue.Message_Queue                                            ' Mensaje-Cuerpo del Email

        Try
            Dim MailAddressList() As String
            Dim MailRecipientList As List(Of RecipientData) = New List(Of RecipientData)()

            ' Destinatario
            MailAddressList = nEmailAddress.Split(";"c)
            For Each eMailAddress In MailAddressList
                If (eMailAddress.Trim() <> "") Then
                    MailRecipientList.Add(New RecipientData With {.To = eMailAddress.Trim()})
                End If
            Next

            ' Copia
            MailAddressList = nCC.Split(";"c)
            For Each eMailAddress In MailAddressList
                If (eMailAddress.Trim() <> "") Then
                    MailRecipientList.Add(New RecipientData With {.To = eMailAddress.Trim()})
                End If
            Next

            ' Copia Oculta
            MailAddressList = nCCO.Split(";"c)
            For Each eMailAddress In MailAddressList
                If (eMailAddress.Trim() <> "") Then
                    MailRecipientList.Add(New RecipientData With {.To = eMailAddress.Trim()})
                End If
            Next

            ' Crear la lista de adjuntos
            Dim attachments As New List(Of AttachmentData)
            For Each adjunto As String In nAdjuntos
                Dim base64String As String = ConvertToBase64(adjunto)   ' Convierte el archivo a Base64
                attachments.Add(New AttachmentData With {
                    .Path = "data:application/pdf;base64," + base64String,
                    .Filename = Path.GetFileName(adjunto)
                })
            Next

            Dim valueMensaje As String = "<div style='text-align: left;'>" + nMensaje + "</div>"
            Dim valueFrom As String = If(nEmailFromDisplay IsNot Nothing, nEmailFromDisplay + "<" + nEmailFrom + ">", nEmailFrom)

            ' Crear los datos de email
            Dim emailData As New EmailData With {
                .Subject = nSubject,
                .From = valueFrom,
                .Template = New TemplateData With {
                    .Type = "text/html",
                    .Value = valueMensaje
                },
                .ReplyTo = nEmailFrom,
                .Attachments = attachments,
                .Recipients = MailRecipientList
            }

            ' Convertir el objeto de datos a JSON
            Dim jsonData As String = JsonConvert.SerializeObject(emailData)

            Dim urlTableOCR As String = _URLAPIMasiv
            Dim responseText As String = SendDataPostRequest(jsonData, urlTableOCR, _UserAPIMasiv, _PasswordAPIMasiv)

            ' Manejar la respuesta de la API
            Dim response As ApiResponse = JsonConvert.DeserializeObject(Of ApiResponse)(responseText)

            If response.status = "OK" Then

                Dim deliveryId As String = response.data.deliveryId
                If Not String.IsNullOrWhiteSpace(deliveryId) Then

                    Dim dataTBLTrackingMailDataTable = _DBMMailSender.SchemaMail.TBL_Tracking_Mail.DBFindByfk_Queuefk_EntidadIsActive(RowQueue.id_Queue, RowQueue.fk_Entidad, True)
                    If dataTBLTrackingMailDataTable.Count > 0 Then
                        Dim dataRowTBLTrackingMailDataTable = dataTBLTrackingMailDataTable(0)

                        Dim dataTBLTrackingMail As DBTools.SchemaMail.TBL_Tracking_MailType = New DBTools.SchemaMail.TBL_Tracking_MailType
                        dataTBLTrackingMail.Id_Delivery = deliveryId
                        dataTBLTrackingMail.Fecha_Envio = DateTime.Now
                        _DBMMailSender.SchemaMail.TBL_Tracking_Mail.DBUpdate(dataTBLTrackingMail, dataRowTBLTrackingMailDataTable.id_Tracking_Mail)

                        ' Se coloca modulo 2: Imagenes siempre
                        Dim dataTBLFileEstado = DBCore.SchemaProcess.TBL_File_Estado.DBFindByfk_Expedientefk_Folderfk_FileModulo(dataRowTBLTrackingMailDataTable.fk_Expediente, dataRowTBLTrackingMailDataTable.fk_Folder, CShort(dataRowTBLTrackingMailDataTable.fk_File), 2)
                        If dataTBLFileEstado.Count > 0 Then
                            Dim dataRowTBLFileEstado = dataTBLFileEstado(0)

                            If dataRowTBLFileEstado.fk_Estado <> 38 Then
                                Dim dataTableFileEstadoType = New DBCore.SchemaProcess.TBL_File_EstadoType
                                dataTableFileEstadoType.fk_Estado = 38 'Indexado
                                dataTableFileEstadoType.Fecha_Log = DateTime.Now
                                dataTableFileEstadoType.fk_Usuario = 2 'usuario sistema
                                DBCore.SchemaProcess.TBL_File_Estado.DBUpdate(dataTableFileEstadoType, dataRowTBLFileEstado.fk_Expediente, dataRowTBLFileEstado.fk_Folder, dataRowTBLFileEstado.fk_File, dataRowTBLFileEstado.Modulo)
                            End If
                        End If

                    Else
                        Dim dataTBLTrackingMailType = New DBTools.SchemaMail.TBL_Tracking_MailType
                        dataTBLTrackingMailType.id_Tracking_Mail = Guid.NewGuid()
                        dataTBLTrackingMailType.fk_Queue = RowQueue.id_Queue
                        dataTBLTrackingMailType.Id_Delivery = deliveryId
                        dataTBLTrackingMailType.fk_Entidad = RowQueue.fk_Entidad
                        dataTBLTrackingMailType.fk_Proyecto = 0
                        dataTBLTrackingMailType.fk_Expediente = 0
                        dataTBLTrackingMailType.fk_Folder = 0
                        dataTBLTrackingMailType.fk_File = 0
                        dataTBLTrackingMailType.fk_Usuario = RowQueue.fk_Usuario
                        dataTBLTrackingMailType.Fecha_Log = DateTime.Now
                        dataTBLTrackingMailType.EmailAddress_Queue = RowQueue.EmailAddress_Queue
                        dataTBLTrackingMailType.CC_Queue = RowQueue.CC_Queue
                        dataTBLTrackingMailType.CCO_Queue = RowQueue.CCO_Queue
                        dataTBLTrackingMailType.Subject_Queue = RowQueue.Subject_Queue
                        dataTBLTrackingMailType.Message_Queue = RowQueue.Message_Queue
                        dataTBLTrackingMailType.Attach_Queue = RowQueue.Attach_Queue
                        dataTBLTrackingMailType.AttachName_Queue = RowQueue.AttachName_Queue
                        dataTBLTrackingMailType.EmailFrom = RowQueue.EmailFrom
                        dataTBLTrackingMailType.EmailFromDisplay = RowQueue.EmailFromDisplay
                        dataTBLTrackingMailType.Fecha_Envio = RowQueue.Fecha_Queue
                        dataTBLTrackingMailType.fk_Estado_Correo = 2
                        dataTBLTrackingMailType.Detalle_Envio = "Email Enviado"
                        dataTBLTrackingMailType.IsActive = True
                        _DBMMailSender.SchemaMail.TBL_Tracking_Mail.DBInsert(dataTBLTrackingMailType)
                    End If
                End If
            Else
                Dim fullErrorSend As String = " Data: " + response.data.ToString() + " Descripcion: " + response.description.ToString() + " Status: " + response.status.ToString()
                Throw New ApplicationException(fullErrorSend)
            End If

        Catch ex As Exception
            Throw
        End Try

    End Sub

    Public Sub SendMail(ByVal nEmailFrom As String, ByVal nEmailFromDisplay As String, ByVal nEmailAddress As String, ByVal nCC As String, ByVal nCCO As String, ByVal nSubject As String, ByVal nMensaje As String, ByVal nAdjuntos() As String)
        ' Configurar parametros para el mensaje de correo
        Using NewMailMessage As New MailMessage()

            If (nEmailFrom <> "" And nEmailFromDisplay <> "") Then
                NewMailMessage.From = New MailAddress(nEmailFrom, nEmailFromDisplay)
            Else
                NewMailMessage.From = New MailAddress(_FromMailAddress, _FromMailDisplay)

            End If

            Dim MailAddressList() As String

            ' Destinatario
            MailAddressList = nEmailAddress.Split(";"c)
            For Each eMailAddress In MailAddressList
                If (eMailAddress.Trim() <> "") Then NewMailMessage.To.Add(New MailAddress(eMailAddress.Trim()))
            Next

            ' Copia
            MailAddressList = nCC.Split(";"c)
            For Each eMailAddress In MailAddressList
                If (eMailAddress.Trim() <> "") Then NewMailMessage.CC.Add(New MailAddress(eMailAddress.Trim()))
            Next

            ' Copia oculta
            MailAddressList = nCCO.Split(";"c)
            For Each eMailAddress In MailAddressList
                If (eMailAddress.Trim() <> "") Then NewMailMessage.Bcc.Add(New MailAddress(eMailAddress.Trim()))
            Next

            NewMailMessage.Subject = nSubject
            NewMailMessage.Body = nMensaje
            NewMailMessage.IsBodyHtml = True

            If Not nAdjuntos Is Nothing Then
                For Each Adjunto As String In nAdjuntos
                    ' adjuntamos un archivo en disco                    
                    Dim attachment As New Attachment(Adjunto)

                    ' configurar algunos valores opcionales
                    attachment.Name = Path.GetFileName(Adjunto)

                    ' adjuntar el archivo al mensaje
                    NewMailMessage.Attachments.Add(attachment)
                Next
            End If

            InitializeClientProperties()

            ' Enviar el mensaje
            _Client.Send(NewMailMessage)

            ' ---------------------------------------
            ' LIBERAR RECURSOS MANUALMENTE
            ' ---------------------------------------
            'For i = 0 To NewMailMessage.Attachments.Count - 1
            '    NewMailMessage.Attachments(i).Dispose()
            'Next

            'NewMailMessage.Attachments.Dispose()

            'NewMailMessage.Dispose()
            ' ---------------------------------------
        End Using
    End Sub

    Private Sub InitializeClientProperties()
        _Client.Host = _SMTPServerAddress
        _Client.Port = _Port
        _Client.DeliveryMethod = SmtpDeliveryMethod.Network
        _Client.Credentials = New Net.NetworkCredential(_User, _Password)
        _Client.EnableSsl = _EnabledSSL
    End Sub

#End Region

#Region " Funciones "

    Function ConvertToBase64(filePath As String) As String
        ' Usa un bloque Using para garantizar que el archivo se cierra correctamente
        Using fileStream As New FileStream(filePath, FileMode.Open, FileAccess.Read)
            Using memoryStream As New MemoryStream()
                fileStream.CopyTo(memoryStream) ' Copia el contenido del FileStream al MemoryStream
                Dim fileBytes As Byte() = memoryStream.ToArray() ' Obtiene los bytes del MemoryStream
                Return Convert.ToBase64String(fileBytes) ' Convierte a Base64
            End Using
        End Using
    End Function


    Private Function SendDataPostRequest(jsonPayload As String, url As String, username As String, password As String) As String

        Try
            ' Forzar el uso de TLS 1.2.
            ServicePointManager.SecurityProtocol = CType(3072, SecurityProtocolType)

            ' Crea la solicitud HTTP con la URL especificada.
            Dim request As HttpWebRequest = CType(WebRequest.Create(url), HttpWebRequest)
            request.Method = "POST"
            request.ContentType = "application/json"

            ' Deshabilita el uso de proxy.
            request.Proxy = Nothing

            ' Agrega la cabecera Authorization para la autenticación básica.
            Dim authInfo As String = Convert.ToBase64String(Encoding.UTF8.GetBytes(username & ":" & password))
            request.Headers.Add("Authorization", "Basic " & authInfo)

            ' Agrega la cabecera Accept-Encoding para indicar la aceptación de compresión.
            request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate, br")

            ' Escribe el JSON en el cuerpo de la solicitud.
            Using streamWriter As New StreamWriter(request.GetRequestStream())
                streamWriter.Write(jsonPayload)
            End Using

            ' Obtiene y devuelve la respuesta.
            Using response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
                If response.StatusCode = HttpStatusCode.OK Then
                    Using reader As New StreamReader(response.GetResponseStream())
                        Return reader.ReadToEnd()
                    End Using

                End If
            End Using

        Catch ex As WebException
            Dim response As HttpWebResponse = CType(ex.Response, HttpWebResponse)
            Dim errorMessage As String = "Error:" & ex.Message

            If response IsNot Nothing Then
                errorMessage += "Status Code:" & response.StatusCode
                Using reader As New StreamReader(response.GetResponseStream())
                    errorMessage += "Response:" & reader.ReadToEnd()
                End Using
            End If

            Throw New ApplicationException("Error al enviar la solicitud: " & errorMessage)

        Catch ex As Exception
            Throw New ApplicationException("Excepción general al enviar la solicitud:" + ex.Message)
        End Try

        ' Si no se pudo obtener una respuesta, se devuelve una cadena vacía.
        Return String.Empty
    End Function

    Public Shared Function ValidateMail(ByVal nMail As String) As Boolean
        Dim l_reg As New Regex("^(([^<;>;()[\]\\.,;:\s@\""]+(\.[^<;>;()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$")

        Return l_reg.IsMatch(nMail)
    End Function

#End Region

End Class
