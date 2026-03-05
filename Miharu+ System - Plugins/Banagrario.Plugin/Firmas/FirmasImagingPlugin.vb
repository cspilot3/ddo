Imports Banagrario.Plugin.Firmas.FormWrappers
Imports Miharu.Desktop.Library
Imports Miharu.Imaging.Library
Imports DBSecurity
Imports DBAgrario
Imports Miharu.Desktop.Library.Plugins
Imports EventExecuterFirmasImaging = Banagrario.Plugin.Firmas.FormWrappers.EventExecuterFirmasImaging

Namespace Firmas

    Public Class FirmasImagingPlugin
        Inherits MarshalByRefObject
        Implements IDesktopPlugin

#Region " Declaraciones "

        Private _eventExecuter As New EventExecuterFirmasImaging(Me)

        Private _manager As DesktopPluginManager

        Private _workSpace As FormImagingWorkSpace

        Private _bancoAgrarioConnectionString As String

        Private _toolsConnectionString As String

        Private _wrapper As FormImagingWorkSpaceWrapper_Firmas

        Private _horaCambioFechaProceso As Integer

#End Region

#Region " Propiedades "

        Public ReadOnly Property Manager() As DesktopPluginManager
            Get
                Return Me._manager
            End Get
        End Property

        Public ReadOnly Property WorkSpace() As FormImagingWorkSpace
            Get
                Return Me._workSpace
            End Get
        End Property

        Public ReadOnly Property BancoAgrarioConnectionString() As String
            Get
                Return Me._bancoAgrarioConnectionString
            End Get
        End Property

        Public ReadOnly Property ToolsConnectionString() As String
            Get
                Return Me._toolsConnectionString
            End Get
        End Property

        Public ReadOnly Property Wrapper As FormImagingWorkSpaceWrapper_Firmas
            Get
                Return Me._wrapper
            End Get
        End Property

        Public ReadOnly Property HoraCambioFechaProceso As Integer
            Get
                Return Me._horaCambioFechaProceso
            End Get
        End Property
#End Region

#Region " Implementacion IDesktopPlugin "

        Public Function IsValidPlugin(ByVal nProcessType As ProcessLibraryType, ByVal nEntidad As Integer, ByVal nProyecto As Integer) As Boolean Implements IDesktopPlugin.IsValidPlugin
            Program.InicializarConfiguracion()

            If (nProcessType <> ProcessLibraryType.Imaging) Then Return False
            If (nEntidad <> Program.BanagrarioEntidadId) Then Return False
            If (nProyecto <> Program.FirmasProyectoImagingId) Then Return False
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

                Dim moduloDataTable = dbmSecurity.SchemaSecurity.TBL_Modulo.DBGet(Nothing)

                If (moduloDataTable.Count > 0) Then
                    For Each modulo In moduloDataTable
                        Select Case modulo.id_Modulo
                            Case 13
                                Me._bancoAgrarioConnectionString = modulo.ConnectionString
                            Case 3
                                Me._toolsConnectionString = modulo.ConnectionString
                        End Select
                    Next
                Else
                    Throw New Exception("No se pudo cargar la cadena de conexión para el módulo: " & Program.ModuloId.ToString())
                End If
            Finally
                dbmSecurity.Connection_Close()
            End Try

            'Obtener la hora en que cambia la fecha de proceso
            If Me._bancoAgrarioConnectionString <> "" Then
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
                Me._wrapper = New FormImagingWorkSpaceWrapper_Firmas(Me)
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
                Return Me._eventExecuter
            End Get
        End Property

#End Region

    End Class

End Namespace