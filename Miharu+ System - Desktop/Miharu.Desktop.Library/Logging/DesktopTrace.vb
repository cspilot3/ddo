Imports Microsoft.Practices.EnterpriseLibrary.Logging

Public Class DesktopTrace

    Public Class CategoryEnum
        Private _EnumName As String

        Public Shared ReadOnly CriticalError As New CategoryEnum("CriticalError")
        Public Shared ReadOnly Warning As New CategoryEnum("Warning")
        Public Shared ReadOnly Performance As New CategoryEnum("Performance")
        Public Shared ReadOnly Information As New CategoryEnum("Information")

        Private Sub New(nEnumName As String)
            _EnumName = nEnumName
        End Sub

        Public ReadOnly Property EnumName As String
            Get
                Return _EnumName
            End Get
        End Property
    End Class

    Public Shared Sub Trace(nMessage As String, nCategory As CategoryEnum, nPriority As Integer, nEventId As Integer, nTraceEventType As TraceEventType, nTitle As String)
        Trace(nMessage, nCategory, nPriority, nEventId, nTraceEventType, nTitle, Nothing)
    End Sub

    Public Shared Sub Trace(nMessage As String, nCategory As CategoryEnum, nPriority As Integer, nEventId As Integer, nTraceEventType As TraceEventType, nTitle As String, nParameters As Dictionary(Of String, Object))
        Logger.Write(New LogEntry(nMessage, nCategory.EnumName, nPriority, nEventId, nTraceEventType, nTitle, nParameters))
    End Sub

End Class
