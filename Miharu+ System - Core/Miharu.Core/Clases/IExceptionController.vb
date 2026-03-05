
Public Interface IExceptionController
    Sub ShowMessage(ByRef nMessage As String, ByVal nMsgBoxIcon As MsgBoxIcon, Optional ByRef nTitle As String = "")
    Function getLogPath() As String
End Interface

Public Enum MsgBoxIcon As Byte
    IconInformation = 1
    IconWarning = 2
    IconError = 3
    IconConfirmation = 4
End Enum