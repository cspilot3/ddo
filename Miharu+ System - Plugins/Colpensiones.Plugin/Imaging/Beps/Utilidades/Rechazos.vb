Imports System

Namespace Imaging.Beps.Utilidades


    Public Class Rechazos

        Private IntRechazos As New Dictionary(Of Integer, String)
        Private StrRechazos As New Dictionary(Of String, Integer)

        Public Sub New()

        End Sub
        Public Sub New(int As Integer, val As String)

        End Sub

        Public Sub Add(int As Integer, val As String)
            IntRechazos.Add(int, val)
            StrRechazos.Add(val, int)
        End Sub

        Public Function Find(ByVal int As Integer) As String
            Return IntRechazos(int)
        End Function

        Public Function Find(ByVal val As String) As Integer
            Return StrRechazos(val)
        End Function
    End Class

End Namespace
