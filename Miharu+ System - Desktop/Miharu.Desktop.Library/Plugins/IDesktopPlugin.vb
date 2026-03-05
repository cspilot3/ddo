Namespace Plugins

    Public Interface IDesktopPlugin

        Function IsValidPlugin(ByVal nProcessType As ProcessLibraryType, ByVal nEntidad As Integer, ByVal nProyecto As Integer) As Boolean

        Function GetName() As String

        Function GetCode() As String

        Sub Initialize(ByVal nManager As DesktopPluginManager)

        Sub Activate()

        Sub Close()

        Function InitializeLifetimeService() As Object

        ReadOnly Property EventExecuter() As EventExecuter

    End Interface

End Namespace