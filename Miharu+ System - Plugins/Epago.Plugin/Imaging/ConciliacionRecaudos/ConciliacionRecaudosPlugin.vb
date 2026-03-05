Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Plugins
Imports Miharu.Imaging.Library
Imports System.Windows.Forms

Namespace Imaging.ConciliacionRecaudos
    Public Class ConciliacionRecaudosPlugin
        Implements IDesktopPlugin


#Region " Declaraciones "

        Public Const ModuloId As Short = 31

        Public Const ConfigurationPrefix = "PluginEpago_"

        Private _EventExecuter As New EventExecuterCR(Me)

        Private _Manager As DesktopPluginManager

        Private _WorkSpace As FormImagingWorkSpace

        Private _EpagoConnectionString As String

        Private _ToolsConnectionString As String

        Private _Wrapper As FormImagingkWorkSpaceWrapperCR

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

        Public ReadOnly Property EpagoConnectionString() As String
            Get
                Return Me._EpagoConnectionString
            End Get
        End Property

        Public ReadOnly Property ToolsConnectionString() As String
            Get
                Return Me._ToolsConnectionString
            End Get
        End Property

        Public ReadOnly Property Wrapper As FormImagingkWorkSpaceWrapperCR
            Get
                Return Me._Wrapper
            End Get
        End Property

        ' Propiedades configurables
        Public Shared Property Imaging_EntidadId() As Short = 29
        Public Shared Property Imaging_ConciliacionRecaudos_ProyectoId() As Short = 2

#End Region

#Region " Implementacion IDesktopPlugin "

        Public Function IsValidPlugin(ByVal nProcessType As ProcessLibraryType, ByVal nEntidad As Integer, ByVal nProyecto As Integer) As Boolean Implements IDesktopPlugin.IsValidPlugin
            PluginHelper.InicializarConfiguracion(Me, ConfigurationPrefix)

            If (nProcessType <> ProcessLibraryType.Imaging) Then Return False
            If (nEntidad <> Imaging_EntidadId) Then Return False
            If (nProyecto <> Imaging_ConciliacionRecaudos_ProyectoId) Then Return False
            Return True
        End Function

        Public Sub Initialize(ByVal nManager As DesktopPluginManager) Implements IDesktopPlugin.Initialize
            Me._Manager = nManager
            Me._WorkSpace = CType(nManager.WorkSpace, FormImagingWorkSpace)

            Dim dbmSecurity As DBSecurity.DBSecurityDataBaseManager = Nothing

            Try
                dbmSecurity = New DBSecurity.DBSecurityDataBaseManager(Me.Manager.DesktopGlobal.ConnectionStrings.Security)
                dbmSecurity.Connection_Open(1) ' System

                Dim ModuloDataTable = dbmSecurity.SchemaSecurity.TBL_Modulo.DBGet(Nothing)

                If (ModuloDataTable.Count > 0) Then
                    For Each modulo In ModuloDataTable
                        Select Case modulo.id_Modulo
                            Case 31
                                Me._EpagoConnectionString = modulo.ConnectionString
                            Case 3
                                Me._ToolsConnectionString = modulo.ConnectionString
                        End Select
                    Next



                Else
                    Throw New Exception("No se pudo cargar la cadena de conexión para el módulo: " & ModuloId.ToString())
                End If
            Catch
                Throw
            Finally
                If (dbmSecurity IsNot Nothing) Then dbmSecurity.Connection_Close()
            End Try
        End Sub

        Public Sub Activate() Implements IDesktopPlugin.Activate
            Try
                Me._Wrapper = New FormImagingkWorkSpaceWrapperCR(Me)
                Me._Wrapper.AplicarCambios()
            Catch ex As Exception
                MessageBox.Show("Error al activar el plugin " + GetName() + ", " + ex.Message)
            End Try
        End Sub

        Public Function GetCode() As String Implements IDesktopPlugin.GetCode

            Return "EpagoPlugin"
        End Function

        Public Function GetName() As String Implements IDesktopPlugin.GetName
            Return "Libreria especializada para Epago "
        End Function

        Public Sub Close() Implements IDesktopPlugin.Close
        End Sub

        Public Function InitializeLifetimeService() As Object Implements IDesktopPlugin.InitializeLifetimeService
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
