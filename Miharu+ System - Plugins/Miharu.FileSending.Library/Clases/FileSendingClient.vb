Imports System.Runtime.Remoting.Channels.Tcp
Imports System.Runtime.Remoting.Channels
Imports System.IO
Imports System.Threading
Imports System.Windows.Forms

Namespace Clases

    Public Class FileSendingClient

#Region " Declaraciones "

        Private _Servidor As FileSendingServer
        Private _IPName As String
        Private _Port As Integer
        Private _AppName As String
        Private _WorkingFolder As String
        Private _PackageSize As Integer
        Private _FechaMovimiento As DateTime
        Private _Fecha_Recaudo As String
        Private _TipoMovimiento As Short

        Private _Identificadores As New List(Of String)
        Private _Cola As New Dictionary(Of String, FileSendingDefinitionClient)
        Private _Detener As Boolean

        Public Event TransferBegin(ByVal sender As Object, ByVal Identificador As String)
        Public Event TransferProcess(ByVal sender As Object, ByVal Identificador As String, ByVal Avance As Single)
        Public Event TransferCompleted(ByVal sender As Object, ByVal Identificador As String)
        Public Event TransferError(ByVal sender As Object, ByVal Identificador As String, ByVal Mensaje As String)

#End Region

#Region " Propiedades "

        Public ReadOnly Property IPName As String
            Get
                Return _IPName
            End Get
        End Property

        Public ReadOnly Property Port As Integer
            Get
                Return _Port
            End Get
        End Property

        Public ReadOnly Property AppName As String
            Get
                Return _AppName
            End Get
        End Property

        Public ReadOnly Property WorkingFolder As String
            Get
                Return _WorkingFolder
            End Get
        End Property

        Private ReadOnly Property PackageSize As Integer
            Get
                Return _PackageSize
            End Get
        End Property

#End Region

#Region " Constructores "

        'Public Sub New(ByVal nIPName As String, ByVal nPort As Integer, ByVal nAppName As String, ByVal nWorkingFolder As String, ByVal nPackageSize As Integer, nFechaMovimiento As DateTime, nTipoMovimiento As Short)
        '    _IPName = nIPName
        '    _Port = nPort
        '    _AppName = nAppName

        '    _WorkingFolder = nWorkingFolder.TrimEnd("\"c) + "\"
        '    _PackageSize = nPackageSize

        '    _FechaMovimiento = nFechaMovimiento
        '    _TipoMovimiento = nTipoMovimiento

        '    _Servidor = getFileSendingServer(_IPName, _Port, _AppName)

        '    If (Not Directory.Exists(_WorkingFolder)) Then
        '        Directory.CreateDirectory(_WorkingFolder)
        '    End If

        '    ' Iniciar servicio
        '    _Detener = False
        '    Dim Hilo As New Thread(AddressOf Proceso)
        '    Hilo.Start()
        'End Sub


        'Este es el new de ddo
        Public Sub New(ByVal nIPName As String, ByVal nPort As Integer, ByVal nAppName As String, ByVal nWorkingFolder As String, ByVal nPackageSize As Integer, nFechaMovimiento As String, nUsuario As Integer)
            _IPName = nIPName
            _Port = nPort
            _AppName = nAppName
            _WorkingFolder = nWorkingFolder.TrimEnd("\"c) + "\"
            _PackageSize = nPackageSize

            _Fecha_Recaudo = nFechaMovimiento

            _Servidor = getFileSendingServer(_IPName, _Port, _AppName)

            If (Not Directory.Exists(_WorkingFolder)) Then
                Directory.CreateDirectory(_WorkingFolder)
            End If

            ' Iniciar servicio
            _Detener = False
            Dim Hilo As New Thread(AddressOf ProcesoG)
            Hilo.Start()
        End Sub

        Public Sub New(ByVal nIPName As String, ByVal nPort As Integer, ByVal nAppName As String, ByVal nWorkingFolder As String, ByVal nPackageSize As Integer)
            _IPName = nIPName
            _Port = nPort
            _AppName = nAppName
            _WorkingFolder = nWorkingFolder.TrimEnd("\"c) + "\"
            _PackageSize = nPackageSize
            
            _Servidor = getFileSendingServer(_IPName, _Port, _AppName)

            If (Not Directory.Exists(_WorkingFolder)) Then
                Directory.CreateDirectory(_WorkingFolder)
            End If

            ' Iniciar servicio
            _Detener = False
            Dim Hilo As New Thread(AddressOf ProcesoG)
            Hilo.Start()
        End Sub

        Protected Overrides Sub Finalize()
            _Detener = True
        End Sub

#End Region

#Region " Metodos "

        Public Sub Detener()
            _Detener = True
        End Sub

        'Private Sub Proceso()
        '    Do
        '        If (_Identificadores.Count > 0) Then
        '            Dim Definition = _Cola.Item(_Identificadores(0))
        '            Dim LastPart As Integer = -1
        '            Dim Message As String = ""

        '            Try
        '                Definition.Estado = ""
        '                Definition.Message = ""

        '                RaiseEvent TransferBegin(Me, Definition.ID)
        '                Dim idLog As Integer = 0

        '                If (_Servidor.SendFile(Definition, LastPart, Definition.Oficina, Definition.Fecha_Movimiento, Definition.Tipo_Movimiento, Definition.Usuario, Message, idLog)) Then
        '                    Definition.id_Log = idLog

        '                    Using Archivo As New FileStream(_WorkingFolder & Definition.ID + ".source", FileMode.Open, FileAccess.Read)
        '                        Dim Inicio As Integer
        '                        Dim Longitud As Integer
        '                        Dim Paso As Integer = 0
        '                        Dim MaxPasos As Integer = CInt(Definition.FilePackages / 100)

        '                        For i As Integer = LastPart + 1 To Definition.FilePackages - 1
        '                            If (Definition.Cancel) Then
        '                                Exit For
        '                            End If

        '                            Inicio = Definition.PackageSize * i
        '                            Longitud = CInt(IIf(Inicio + Definition.PackageSize < Definition.FileSize - 1, Definition.PackageSize, Definition.FileSize - 1 - Inicio + Definition.PackageSize))
        '                            Dim Data(Longitud - 1) As Byte

        '                            Archivo.Position = Inicio
        '                            Archivo.Read(Data, 0, Longitud)

        '                            If (_Servidor.SendPart(Definition.ID, Data, i, Message)) Then
        '                                Definition.LastSentPart = i
        '                                Paso += 1
        '                                If (Paso >= MaxPasos) Then
        '                                    RaiseEvent TransferProcess(Me, Definition.ID, Definition.getAvance())
        '                                    Application.DoEvents()
        '                                    Paso = 0
        '                                End If
        '                            Else
        '                                Throw New Exception(Message)
        '                            End If
        '                        Next

        '                        If (Definition.Cancel) Then
        '                            _Servidor.Cancel(Definition.ID, Message)
        '                        Else
        '                            RaiseEvent TransferCompleted(Me, Definition.ID)
        '                        End If

        '                        _Identificadores.RemoveAt(0)
        '                        _Cola.Remove(Definition.ID)

        '                        Archivo.Close()
        '                        Archivo.Dispose()
        '                    End Using
        '                Else
        '                    Throw New Exception(Message)
        '                End If
        '            Catch ex As Exception
        '                Definition.Estado = "Error"
        '                Definition.Message = ex.Message
        '                RaiseEvent TransferError(Me, Definition.ID, ex.Message)
        '            End Try
        '        End If

        '        Thread.Sleep(3000)
        '    Loop While Not _Detener
        'End Sub

        Private Sub ProcesoG()
            Do
                If (_Identificadores.Count > 0) Then
                    Dim Definition = _Cola.Item(_Identificadores(0))
                    Dim LastPart As Integer = -1
                    Dim Message As String = ""

                    Try
                        Definition.Estado = ""
                        Definition.Message = ""

                        RaiseEvent TransferBegin(Me, Definition.ID)
                        Dim idLog As Integer = 0

                        If (_Servidor.SendFile(Definition, LastPart, Definition.Usuario, Message, idLog)) Then
                            Definition.id_Log = idLog

                            Using Archivo As New FileStream(_WorkingFolder & Definition.ID + ".source", FileMode.Open, FileAccess.Read)
                                Dim Inicio As Integer
                                Dim Longitud As Integer
                                Dim Paso As Integer = 0
                                Dim MaxPasos As Integer = CInt(Definition.FilePackages / 100)

                                For i As Integer = LastPart + 1 To Definition.FilePackages - 1
                                    If (Definition.Cancel) Then
                                        Exit For
                                    End If

                                    Inicio = Definition.PackageSize * i
                                    Longitud = CInt(IIf(Inicio + Definition.PackageSize < Definition.FileSize - 1, Definition.PackageSize, Definition.FileSize - 1 - Inicio + Definition.PackageSize))
                                    Dim Data(Longitud - 1) As Byte

                                    Archivo.Position = Inicio
                                    Archivo.Read(Data, 0, Longitud)

                                    If (_Servidor.SendPart(Definition.ID, Data, i, Message)) Then
                                        Definition.LastSentPart = i
                                        Paso += 1
                                        If (Paso >= MaxPasos) Then
                                            RaiseEvent TransferProcess(Me, Definition.ID, Definition.getAvance())
                                            Application.DoEvents()
                                            Paso = 0
                                        End If
                                    Else
                                        Throw New Exception(Message)
                                    End If
                                Next

                                If (Definition.Cancel) Then
                                    _Servidor.Cancel(Definition.ID, Message)
                                Else
                                    RaiseEvent TransferCompleted(Me, Definition.ID)
                                End If

                                _Identificadores.RemoveAt(0)
                                _Cola.Remove(Definition.ID)

                            End Using
                        Else
                            Throw New Exception(Message)
                        End If
                    Catch ex As Exception
                        Definition.Estado = "Error"
                        Definition.Message = ex.Message
                        RaiseEvent TransferError(Me, Definition.ID, ex.Message)
                    End Try
                End If

                Thread.Sleep(1000)
            Loop While Not _Detener
        End Sub

        Public Sub Cancelar(ByVal nIdentificador As String)
            If (_Cola.ContainsKey(nIdentificador)) Then
                _Cola(nIdentificador).Cancel = True
            End If
        End Sub

#End Region

#Region " Funciones "

        Public Function Transmitir(ByVal nFileName As String, ByVal nOficina As Integer, ByVal nUsuario As Integer) As String
            Return Publicar(nFileName, nOficina, nUsuario).ID.ToString()
        End Function

        Public Function TransmitirDDO(ByVal nFileName As String, ByVal nOficina As Integer, ByVal nUsuario As Integer, ByVal nFechaMovimiento As String) As String
            Return PublicarDDO(nFileName, nOficina, nUsuario, nFechaMovimiento).ID.ToString()
        End Function

        Public Function Transmitir(ByVal nFileName As String, ByVal nUsuario As Integer) As String
            Return Publicar(nFileName, nUsuario).ID.ToString()
        End Function

        'Public Sub Cargar(ByVal nEntidadCliente As Short, ByVal nProyecto As Short, ByVal nEsquema As Short, ByVal nFolder As String, ByVal nPaqueteName As String, ByVal nObservaciones As String, ByVal nEntidadProcesamiento As Short, ByVal nSedeProcesamiento As Short, ByVal nCentroProcesamiento As Short, nIDLog As Integer, ByVal nOficina As Integer, ByVal nUser As Integer, ByVal nEstado As Short, ByRef nCargueID As Integer, ByRef nFolios As Short)
        '    Dim Message As String = ""

        '    If (Not _Servidor.Cargar(nEntidadCliente, nProyecto, nEsquema, nFolder, nPaqueteName, nObservaciones, nEntidadProcesamiento, nSedeProcesamiento, nCentroProcesamiento, nIDLog, nOficina, nUser, nEstado, nCargueID, nFolios, Message)) Then
        '        Throw New Exception(Message)
        '    End If
        'End Sub

        Public Sub Cargar(ByVal nEntidadCliente As Short, ByVal nProyecto As Short, ByVal nEsquema As Short, ByVal nFolder As String, ByVal nPaqueteName As String, ByVal nObservaciones As String, ByVal nEntidadProcesamiento As Short, ByVal nSedeProcesamiento As Short, ByVal nCentroProcesamiento As Short, nIDLog As Integer, ByVal nUser As Integer, ByVal nEstado As Short, ByVal nContenedor As String, ByRef nCargueID As Integer, ByRef nFolios As Short)
            Dim Message As String = ""

            If (Not _Servidor.Cargar(nEntidadCliente, nProyecto, nEsquema, nFolder, nPaqueteName, nObservaciones, nEntidadProcesamiento, nSedeProcesamiento, nCentroProcesamiento, nIDLog, nUser, nEstado, nContenedor, nCargueID, nFolios, Message)) Then
                Throw New Exception(Message)
            End If
        End Sub


        Public Sub CargarDDO(ByVal nEntidadCliente As Short, ByVal nProyecto As Short, ByVal nEsquema As Short, ByVal nFolder As String, ByVal nPaqueteName As String, ByVal nObservaciones As String, ByVal nEntidadProcesamiento As Short, ByVal nSedeProcesamiento As Short, ByVal nCentroProcesamiento As Short, nIDLog As Integer, ByVal nUser As Integer, ByVal nEstado As Short, ByVal nContenedor As String, ByRef nCargueID As Integer, ByRef nFolios As Short, ByVal nCampos As String, ByVal ExtImgSalida As String)
            Dim Message As String = ""

            If (Not _Servidor.CargarDDO(nEntidadCliente, nProyecto, nEsquema, nFolder, nPaqueteName, nObservaciones, nEntidadProcesamiento, nSedeProcesamiento, nCentroProcesamiento, nIDLog, nUser, nEstado, nContenedor, nCargueID, nFolios, nCampos, ExtImgSalida, Message)) Then
                Throw New Exception(Message)
            End If
        End Sub


        Public Sub Unzip(ByVal nFileName As String)
            Dim Message As String = ""

            If (Not _Servidor.Unzip(nFileName, Message)) Then
                Throw New Exception(Message)
            End If
        End Sub

        Private Function Publicar(ByVal nFileName As String, ByVal nOficina As Integer, ByVal nUsuario As Integer) As FileSendingDefinitionClient
            ' Validar que exista el archivo
            If (Not File.Exists(nFileName)) Then
                Throw New Exception("El archivo " + nFileName + " no existe")
            End If

            ' Leer el archivo
            Dim Archivo As New FileStream(nFileName, FileMode.Open, FileAccess.Read)

            ' Crear la definición        
            Dim Definition As New FileSendingDefinitionClient(Path.GetFileName(nFileName), Guid.NewGuid().ToString(), Archivo.Length, Me.PackageSize, nOficina, _FechaMovimiento, _TipoMovimiento, nUsuario)


            ' Almacenar la definición
            FileSendingDefinitionClient.Serialize(Definition, _WorkingFolder & Definition.ID & ".definition")

            ' Crear el archivo en el repositorio
            File.Copy(nFileName, _WorkingFolder & Definition.ID + ".source")

            _Identificadores.Add(Definition.ID)
            _Cola.Add(Definition.ID, Definition)

            Archivo.Close()
            Archivo.Dispose()

            Return Definition
        End Function

        Private Function PublicarDDO(ByVal nFileName As String, ByVal nOficina As Integer, ByVal nUsuario As Integer, _FechaMovimiento As String) As FileSendingDefinitionClient
            ' Validar que exista el archivo
            If (Not File.Exists(nFileName)) Then
                Throw New Exception("El archivo " + nFileName + " no existe")
            End If

            ' Leer el archivo
            Dim Archivo As New FileStream(nFileName, FileMode.Open, FileAccess.Read)

            ' Crear la definición        
            Dim Definition As New FileSendingDefinitionClient(Path.GetFileName(nFileName), Guid.NewGuid().ToString(), Archivo.Length, Me.PackageSize, nOficina, _FechaMovimiento, nUsuario)


            ' Almacenar la definición
            FileSendingDefinitionClient.Serialize(Definition, _WorkingFolder & Definition.ID & ".definition")

            ' Crear el archivo en el repositorio
            File.Copy(nFileName, _WorkingFolder & Definition.ID + ".source")

            _Identificadores.Add(Definition.ID)
            _Cola.Add(Definition.ID, Definition)

            Archivo.Close()
            Archivo.Dispose()

            Return Definition
        End Function

        Private Function Publicar(ByVal nFileName As String, ByVal nUsuario As Integer) As FileSendingDefinitionClient
            ' Validar que exista el archivo
            If (Not File.Exists(nFileName)) Then
                Throw New Exception("El archivo " + nFileName + " no existe")
            End If

            ' Leer el archivo
            Dim Archivo As New FileStream(nFileName, FileMode.Open, FileAccess.Read)

            ' Crear la definición        
            Dim Definition As New FileSendingDefinitionClient(Path.GetFileName(nFileName), Guid.NewGuid().ToString(), Archivo.Length, Me.PackageSize, nUsuario)


            ' Almacenar la definición
            FileSendingDefinitionClient.Serialize(Definition, _WorkingFolder & Definition.ID & ".definition")

            ' Crear el archivo en el repositorio
            File.Copy(nFileName, _WorkingFolder & Definition.ID + ".source")

            _Identificadores.Add(Definition.ID)
            _Cola.Add(Definition.ID, Definition)

            Archivo.Close()
            Archivo.Dispose()

            Return Definition
        End Function

        Private Function getFileSendingServer(ByVal nServidor As String, ByVal nPuerto As Integer, ByVal nAppName As String) As FileSendingServer
            Dim Channel As TcpChannel

            Try
                Channel = New TcpChannel(0)
                ChannelServices.RegisterChannel(Channel, False)
            Catch : End Try

            Return CType(Activator.GetObject(GetType(FileSendingServer), "tcp://" & nServidor & ":" & nPuerto & "/" & nAppName), FileSendingServer)
        End Function

        Public Function getDefinition(Identificador As String) As FileSendingDefinitionClient
            Return _Cola(Identificador)
        End Function

#End Region

    End Class

End Namespace