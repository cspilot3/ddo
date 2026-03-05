Imports Miharu.Desktop.Library.Plugins
Imports Miharu.Imaging.Library
Imports Miharu.Desktop.Library

Namespace Imaging.Comerciales
    Public Class Plugin
        Implements IDesktopPlugin

#Region " Declaraciones "

        Public Const ModuloId As Short = 24

        Public Const ConfigurationPrefix = "PluginCoomeva_"

        Private _EventExecuter As New ImagingEventExecuter(Me)

        Private _Manager As DesktopPluginManager

        Private _WorkSpace As FormImagingWorkSpace

        Private _CoomevaConnectionString As String

        Private _Wrapper As Wrapper

#End Region

#Region " Propiedades "

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

        Public ReadOnly Property CoomevaConnectionString() As String
            Get
                Return Me._CoomevaConnectionString
            End Get
        End Property

        Public ReadOnly Property Wrapper As Wrapper
            Get
                Return Me._Wrapper
            End Get
        End Property

        ' Propiedades configurables
        Public Shared Property Imagenes_EntidadId() As Short = 12
        Public Shared Property Imagenes_Comerciales_ProyectoId() As Short = 6

#End Region

#Region " Implementacion IDesktopPlugin "
        Public Sub Activate() Implements Miharu.Desktop.Library.Plugins.IDesktopPlugin.Activate
            Try
                Me._Wrapper = New Wrapper(Me)
                Me._Wrapper.AplicarCambios()
            Catch ex As Exception
                MessageBox.Show("Error al activar el plugin " + GetName() + ", " + ex.Message)
            End Try
        End Sub

        Public Sub Close() Implements Miharu.Desktop.Library.Plugins.IDesktopPlugin.Close
            Me._Wrapper.DeshacerCambios()
            Me._Wrapper = Nothing
        End Sub

        Public ReadOnly Property EventExecuter As Miharu.Desktop.Library.Plugins.EventExecuter Implements Miharu.Desktop.Library.Plugins.IDesktopPlugin.EventExecuter
            Get
                Return Me._EventExecuter
            End Get
        End Property

        Public Function GetCode() As String Implements Miharu.Desktop.Library.Plugins.IDesktopPlugin.GetCode
            Return "CoomevaPluginImaging"
        End Function

        Public Function GetName() As String Implements Miharu.Desktop.Library.Plugins.IDesktopPlugin.GetName
            Return "Libreria especializada para el cliente Coomeva - Imágenes - Comerciales"
        End Function

        Public Sub Initialize(nManager As Miharu.Desktop.Library.Plugins.DesktopPluginManager) Implements Miharu.Desktop.Library.Plugins.IDesktopPlugin.Initialize
            Me._Manager = nManager
            Me._WorkSpace = CType(nManager.WorkSpace, FormImagingWorkSpace)

            Dim dbmSecurity As DBSecurity.DBSecurityDataBaseManager = Nothing
            Try
                dbmSecurity = New DBSecurity.DBSecurityDataBaseManager(Me.Manager.DesktopGlobal.ConnectionStrings.Security)
                dbmSecurity.Connection_Open(1) ' System

                Dim ModuloDataTable = dbmSecurity.SchemaSecurity.TBL_Modulo.DBGet(ModuloId)

                If (ModuloDataTable.Count > 0) Then
                    Me._CoomevaConnectionString = ModuloDataTable(0).ConnectionString
                Else
                    Throw New Exception("No se pudo cargar la cadena de conexión para el módulo: " & ModuloId.ToString())
                End If
            Catch
                Throw
            Finally
                If (dbmSecurity IsNot Nothing) Then dbmSecurity.Connection_Close()
            End Try
        End Sub

        Public Function InitializeLifetimeService() As Object Implements Miharu.Desktop.Library.Plugins.IDesktopPlugin.InitializeLifetimeService
            Return Nothing
        End Function

        Public Function IsValidPlugin(nProcessType As Miharu.Desktop.Library.ProcessLibraryType, nEntidad As Integer, nProyecto As Integer) As Boolean Implements Miharu.Desktop.Library.Plugins.IDesktopPlugin.IsValidPlugin
            PluginHelper.InicializarConfiguracion(Me, ConfigurationPrefix)
            Imagenes_EntidadId = 12
            If (nProcessType <> ProcessLibraryType.Imaging) Then Return False
            If (nEntidad <> Imagenes_EntidadId) Then Return False
            If (nProyecto <> Imagenes_Comerciales_ProyectoId) Then Return False
            Return True
        End Function
#End Region

    End Class
End Namespace

