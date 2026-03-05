Imports System.Threading
Imports Miharu.SFTPTransfer.Servicio

Public Class ProcesadorHilos
    Dim NumHilos As Integer = 5
    Dim ListaHilos As New List(Of Thread)
    Public Shared objLock = New Object()

    Public servicio As SFTPTransferService
    'Public servicio As PruebaServicio

    Public Sub AgregarHilo()
        If TieneHiloslibres() = False Then
            Do
                Thread.Sleep(1000)
                If TieneHiloslibres() Then
                    Exit Do
                End If
            Loop
        End If
        Dim Threads As New System.Threading.Thread(AddressOf servicio.ProcesoPrincipalHilo)
        Threads.Start()
        SyncLock objLock
            ListaHilos.Add(Threads)
        End SyncLock
    End Sub
    'Funcion para usar la instancia del procesador hilos con el metodo de mover los archivos a Synergetics
    Public Sub AgregarHiloArchivos()
        If TieneHiloslibres() = False Then
            Do
                Thread.Sleep(1000)
                If TieneHiloslibres() Then
                    Exit Do
                End If
            Loop
        End If
        Dim Threads As New System.Threading.Thread(AddressOf servicio.ProcesoHiloArchivos)
        Threads.Start()

        SyncLock objLock
            ListaHilos.Add(Threads)
        End SyncLock
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

        SyncLock objLock
            For Each hilo In ListaHilos
                If hilo.ThreadState <> ThreadState.Stopped Then

                    Return False

                End If
            Next
        End SyncLock

        Return True

    End Function
End Class