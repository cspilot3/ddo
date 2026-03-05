
Imports Banagrario.Plugin.Imaging.FormWrappers
Imports Miharu.Desktop.Library
Imports Miharu.Imaging.Library
Imports DBSecurity
Imports DBAgrario
Imports Miharu.Desktop.Library.Plugins
Imports EventExecuterImaging = Banagrario.Plugin.Imaging.FormWrappers.EventExecuterImaging

Namespace Imaging

    Public Class BanagrarioImagingPlugin
        Inherits MarshalByRefObject
        Implements IDesktopPlugin

#Region " Declaraciones "

        Private _EventExecuter As New EventExecuterImaging(Me)

        Private _Manager As DesktopPluginManager

        Private _WorkSpace As FormImagingWorkSpace

        Private _BancoAgrarioConnectionString As String

        Private _ToolsConnectionString As String

        Private _Wrapper As FormImagingWorkSpaceWrapper

        Private _HoraCambioFechaProceso As Integer

#End Region

#Region " Propidades "

        Public ReadOnly Property Manager() As DesktopPluginManager
            Get
                Return Me._Manager
            End Get
        End Property

        Public ReadOnly Property WorkSpace() As FormImagingWorkSpace
            Get
                Return Me._WorkSpace
            End Get
        End Property

        Public ReadOnly Property BancoAgrarioConnectionString() As String
            Get
                Return Me._BancoAgrarioConnectionString
            End Get
        End Property

        Public ReadOnly Property ToolsConnectionString() As String
            Get
                Return Me._ToolsConnectionString
            End Get
        End Property

        Public ReadOnly Property Wrapper As FormImagingWorkSpaceWrapper
            Get
                Return Me._Wrapper
            End Get
        End Property

        Public ReadOnly Property HoraCambioFechaProceso As Integer
            Get
                Return Me._HoraCambioFechaProceso
            End Get
        End Property
#End Region

#Region " Implementacion IDesktopPlugin "

        Public Function IsValidPlugin(ByVal nProcessType As ProcessLibraryType, ByVal nEntidad As Integer, ByVal nProyecto As Integer) As Boolean Implements IDesktopPlugin.IsValidPlugin
            Program.InicializarConfiguracion()

            If (nProcessType <> ProcessLibraryType.Imaging) Then Return False
            If (nEntidad <> Program.BanagrarioEntidadId) Then Return False
            If (nProyecto <> Program.BanagrarioProyectoImagingId) Then Return False
            Return True
        End Function

        Public Sub Initialize(ByVal nManager As DesktopPluginManager) Implements IDesktopPlugin.Initialize
            Me._Manager = nManager
            Me._WorkSpace = CType(nManager.WorkSpace, FormImagingWorkSpace)
            DBAgrarioDataBaseManager.IdentifierDateFormat = nManager.DesktopGlobal.IdentifierDateFormat

            'Obtener las cadenas de conexión a la base de datos
            Dim dbmSecurity As New DBSecurityDataBaseManager(Me.Manager.DesktopGlobal.ConnectionStrings.Security)

            Try
                dbmSecurity.Connection_Open(1) ' System

                Dim ModuloDataTable = dbmSecurity.SchemaSecurity.TBL_Modulo.DBGet(Nothing)

                If (ModuloDataTable.Count > 0) Then
                    For Each modulo In ModuloDataTable
                        Select Case modulo.id_Modulo
                            Case 13
                                Me._BancoAgrarioConnectionString = modulo.ConnectionString
                            Case 3
                                Me._ToolsConnectionString = modulo.ConnectionString
                        End Select
                    Next
                Else
                    Throw New Exception("No se pudo cargar la cadena de conexión para el módulo: " & Program.ModuloId.ToString())
                End If
            Finally
                dbmSecurity.Connection_Close()
            End Try

            'Obtener la hora en que cambia la fecha de proceso
            If Me._BancoAgrarioConnectionString <> "" Then
                Dim dbmBancoAgrario As New DBAgrarioDataBaseManager(Me._BancoAgrarioConnectionString)

                Try
                    dbmBancoAgrario.Connection_Open(1) ' System

                    Dim hora = dbmBancoAgrario.SchemaProcess.PA_Get_Hora_Cambio_Fecha_Proceso.DBExecute()
                    Me._HoraCambioFechaProceso = hora
                Finally
                    dbmBancoAgrario.Connection_Close()
                End Try
            Else
                Throw New Exception("No existe una cadena de conexión para el módulo Banco Agrario")
            End If

        End Sub

        Public Sub Activate() Implements IDesktopPlugin.Activate
            Try
                Me._Wrapper = New FormImagingWorkSpaceWrapper(Me)
                Wrapper.AplicarCambios()
            Catch ex As Exception
                Windows.Forms.MessageBox.Show("Error al activar el plugin " + GetName() + ", " + ex.Message)
            End Try
        End Sub

        Public Function GetCode() As String Implements IDesktopPlugin.GetCode

            Return "BanagrarioPlugin"
        End Function

        Public Function GetName() As String Implements IDesktopPlugin.GetName
            Return "Libreria especializada para Banagrario "
        End Function

        Public Sub Close() Implements IDesktopPlugin.Close
            Wrapper.DeshacerCambios()
            Me._Wrapper = Nothing
        End Sub

        Public Overrides Function InitializeLifetimeService() As Object Implements IDesktopPlugin.InitializeLifetimeService
            Return Nothing
        End Function

        Public ReadOnly Property EventExecuter As EventExecuter Implements IDesktopPlugin.EventExecuter
            Get
                Return Me._EventExecuter
            End Get
        End Property

#End Region

    End Class
End Namespace