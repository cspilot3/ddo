Imports System.Windows.Forms
Imports Miharu.Imaging.Library
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Plugins

Namespace Confinanciera.Garantias.Imaging

    Public Class Plugin
        Inherits MarshalByRefObject
        Implements IDesktopPlugin

#Region " Declaraciones "

        Const ConfigurationPrefix = "PluginImagingConfinancieraGarantias_"

        Const IdEntidad As Short = 17
        Const IdProyecto As Short = 1

        Private _EventExecuter As New ImagingEventExecuter(Me)

        Private _Manager As DesktopPluginManager

        Private _WorkSpace As FormImagingWorkSpace

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

#End Region

#Region " Implementacion IDesktopPlugin "

        Public Function IsValidPlugin(ByVal nProcessType As ProcessLibraryType, ByVal nEntidad As Integer, ByVal nProyecto As Integer) As Boolean Implements IDesktopPlugin.IsValidPlugin
            PluginHelper.InicializarConfiguracion(Me, ConfigurationPrefix)

            If (nProcessType <> ProcessLibraryType.Imaging) Then Return False
            If (nEntidad <> IdEntidad) Then Return False
            If (nProyecto <> IdProyecto) Then Return False
            Return True
        End Function

        Public Sub Initialize(ByVal nManager As DesktopPluginManager) Implements IDesktopPlugin.Initialize
            Me._Manager = nManager
            Me._WorkSpace = CType(nManager.WorkSpace, FormImagingWorkSpace)

            'Dim dbmSecurity As DBSecurity.DBSecurityDataBaseManager = Nothing

            'Try
            '    dbmSecurity = New DBSecurity.DBSecurityDataBaseManager(Me.Manager.DesktopGlobal.ConnectionStrings.Security)
            '    dbmSecurity.Connection_Open(1) ' System

            '    Dim ModuloDataTable = dbmSecurity.SchemaSecurity.TBL_Modulo.DBGet(IdModulo)

            '    If (ModuloDataTable.Count > 0) Then
            '        Me._BancoAgrarioConnectionString = ModuloDataTable(0).ConnectionString
            '    Else
            '        Throw New Exception("No se pudo cargar la cadena de conexión para el módulo: " & Program.ModuloId.ToString())
            '    End If
            'Catch
            '    Throw
            'Finally
            '    If (dbmSecurity IsNot Nothing) Then dbmSecurity.Connection_Close()
            'End Try
        End Sub

        Public Sub Activate() Implements IDesktopPlugin.Activate
            Try
                Dim frwWrapper As New Wrapper(Me)
                frwWrapper.AplicarCambios()
            Catch ex As Exception
                MessageBox.Show("Error al activar el plugin " + GetName() + ", " + ex.Message)
            End Try
        End Sub

        Public Function GetCode() As String Implements IDesktopPlugin.GetCode
            Return "PluginRiskConfinanciera"
        End Function

        Public Function GetName() As String Implements IDesktopPlugin.GetName
            Return "Plugin para el módulo Risk del cliente Confinanciera"
        End Function

        Public Sub Close() Implements IDesktopPlugin.Close
        End Sub

        Public Overrides Function InitializeLifetimeService() As Object Implements IDesktopPlugin.InitializeLifetimeService
            Return Nothing
        End Function

        Public ReadOnly Property EventExecuter As Miharu.Desktop.Library.Plugins.EventExecuter Implements Miharu.Desktop.Library.Plugins.IDesktopPlugin.EventExecuter
            Get
                Return Me._EventExecuter
            End Get
        End Property

#End Region

    End Class

End Namespace