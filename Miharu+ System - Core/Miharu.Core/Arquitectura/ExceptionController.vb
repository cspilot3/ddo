Imports System.IO
Imports System.Web.UI

Public Class ExceptionController
    Private _Controller As IExceptionController = Nothing

    Public Sub Initialize(ByRef nGUI As IExceptionController)
        _Controller = nGUI
    End Sub

    Public Sub Register(ByVal ex As Exception, Optional ByVal nShowMessage As Boolean = False, Optional ByVal nIcon As MsgBoxIcon = MsgBoxIcon.IconError, Optional ByRef aditionalInfo As String = "", Optional ByVal nTitle As String = "")
        If (nShowMessage) Then
            ShowMessage(ex.Message, nIcon, aditionalInfo, nTitle)
        End If

        WriteLog(ex, aditionalInfo)
    End Sub

    Public Sub Register(ByVal nMessage As String, Optional ByVal nShowMessage As Boolean = False, Optional ByVal nIcon As MsgBoxIcon = MsgBoxIcon.IconError, Optional ByRef aditionalInfo As String = "", Optional ByVal nTitle As String = "")
        If (nShowMessage) Then
            ShowMessage(nMessage, nIcon, aditionalInfo, nTitle)
        End If

        WriteLog(nMessage, aditionalInfo)
    End Sub

    Public Sub ShowMessage(ByVal nMessage As String, Optional ByVal nIcon As MsgBoxIcon = MsgBoxIcon.IconError, Optional ByRef aditionalInfo As String = "", Optional ByVal nTitle As String = "")
        If (_Controller IsNot Nothing) Then
            _Controller.ShowMessage(nMessage & CStr(IIf(aditionalInfo = "", "", " " & aditionalInfo)), nIcon, nTitle)
        End If
    End Sub

    Private Sub WriteLog(ByVal ex As Exception, Optional ByVal aditionalInfo As String = "")
        If (_Controller IsNot Nothing) Then
            'Lock(1)

            'Dim _page As System.Web.UI.Page
            '_page.Server.MapPath()

            Dim filePath = _Controller.getLogPath ' "C:\Inetpub\wwwroot\Web_CMI\Xml\logs\"
            Dim fileName = "log_" & DateTime.Now.ToString("yyyy-MM-dd") & ".txt"

            Dim fs As New FileStream(filePath & fileName, FileMode.Append)

            Dim sb As New StringBuilder
            sb.Append("..." & vbCrLf)
            sb.Append(DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") & vbCrLf)
            sb.Append(ex.Message & vbCrLf)
            sb.Append(ex.StackTrace & vbCrLf)
            sb.Append(aditionalInfo & vbCrLf)

            Dim buffer() As Byte = Encoding.ASCII.GetBytes(sb.ToString)

            fs.Write(buffer, 0, buffer.Length)
            fs.Flush()
            fs.Close()

            'Unlock(1)
        End If
    End Sub

    Private Sub WriteLog(ByVal text As String, Optional ByVal aditionalInfo As String = "")
        If (_Controller IsNot Nothing) Then
            'Lock(1)

            Dim filePath = _Controller.getLogPath & "\" ' "C:\Inetpub\wwwroot\Web_CMI\Xml\logs\"
            Dim fileName = "log_" & DateTime.Now.ToString("yyyy-MM-dd") & ".txt"

            Dim fs As New FileStream(filePath & fileName, FileMode.Append)

            Dim sb As New StringBuilder
            sb.Append("..." & vbCrLf)
            sb.Append(DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") & vbCrLf)
            sb.Append(text & vbCrLf)
            sb.Append(aditionalInfo & vbCrLf)

            Dim buffer() As Byte = Encoding.ASCII.GetBytes(sb.ToString)

            fs.Write(buffer, 0, buffer.Length)
            fs.Flush()
            fs.Close()

            'Unlock(1)

        End If
    End Sub
End Class
