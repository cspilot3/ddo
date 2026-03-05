Imports System.Threading
Imports Miharu.SofTracTransfer.Servicio

Public Class ProcesadorHilos

    Dim NumHilos As Integer = 1
    Dim ListaHilos As New List(Of Thread)
    Public Shared objLock = New Object()

    Public servicio As SofTracService

    Public Sub AgregarHilo(nTransferenciarow As DBSofTrac.SchemaProcess.CTA_Registros_TransferenciaRow)

        If TieneHiloslibres() = False Then
            Do
                Thread.Sleep(100)
                If TieneHiloslibres() Then
                    Exit Do
                End If
            Loop
        End If

        Dim Threads As New System.Threading.Thread(AddressOf servicio.ProcesoPrincipalHilo)
        Threads.Start(nTransferenciarow)

        SyncLock objLock
            ListaHilos.Add(Threads)
        End SyncLock

        'Dim x As New System.Threading.Thread(AddressOf servicio.ProcesoPrincipalHilo)
        'x.Start(nTransferenciarow)
        'ListaHilos.Add(x)

    End Sub

    Public Function TieneHiloslibres() As Boolean
        SyncLock objLock
            Dim ListaHilosBorrar As New List(Of Thread)
            For Each hilo In ListaHilos
                If hilo.ThreadState = ThreadState.Stopped Then
                    ListaHilosBorrar.Add(hilo)
                End If
            Next
            For Each hilo In ListaHilosBorrar
                ListaHilos.Remove(hilo)
            Next
        End SyncLock
        If ListaHilos.Count < NumHilos Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function TerminoHilos() As Boolean

        For Each hilo In ListaHilos
            If hilo.ThreadState <> ThreadState.Stopped Then
                Return False
            End If
        Next

        Return True

    End Function


End Class