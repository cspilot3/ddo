Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Plugins
Imports Miharu.Imaging.Library
Imports System.Windows.Forms
Namespace Imaging.UMV
    Public Class UMVPlugin
        Implements IDesktopPlugin

#Region " Declaraciones "

        Public Const ModuloId As Short = 30

        Public Const ConfigurationPrefix = "PluginUMV_"

        Private _EventExecuter As New EventExecuterUMV(Me)

        Private _Manager As DesktopPluginManager

        Private _WorkSpace As FormImagingWorkSpace

        Private _UMVConnectionString As String

        Private _ToolsConnectionString As String

        Private _SofTracConnectionString As String

        Private _Wrapper As ImagingWorkSpaceWrapperUMV

        Public Const TempPath As String = "temp\"

        Friend Shared ReadOnly Property AppPath() As String
            Get
                Return Windows.Forms.Application.StartupPath.TrimEnd("\"c) + "\"
            End Get
        End Property

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

        Public ReadOnly Property UMVConnectionString() As String
            Get
                Return Me._UMVConnectionString
            End Get
        End Property

        Public ReadOnly Property ToolsConnectionString() As String
            Get
                Return Me._ToolsConnectionString
            End Get
        End Property

        Public ReadOnly Property SofTracConnectionString() As String
            Get
                Return Me._SofTracConnectionString
            End Get
        End Property

        Public ReadOnly Property Wrapper As ImagingWorkSpaceWrapperUMV
            Get
                Return Me._Wrapper
            End Get
        End Property

        ' Propiedades configurables
        Public Shared Property Imaging_EntidadId() As Short = 39
        Public Shared Property Imaging_UMV_ProyectoId() As Short = 1

#End Region

#Region " Implementacion IDesktopPlugin "

        Public Function IsValidPlugin(nProcessType As Miharu.Desktop.Library.ProcessLibraryType, nEntidad As Integer, nProyecto As Integer) As Boolean Implements Miharu.Desktop.Library.Plugins.IDesktopPlugin.IsValidPlugin
            PluginHelper.InicializarConfiguracion(Me, ConfigurationPrefix)

            If (nProcessType <> ProcessLibraryType.Imaging) Then Return False
            If (nEntidad <> Imaging_EntidadId) Then Return False
            If (nProyecto <> Imaging_UMV_ProyectoId) Then Return False
            Return True
        End Function

        Public Sub Initialize(nManager As Miharu.Desktop.Library.Plugins.DesktopPluginManager) Implements Miharu.Desktop.Library.Plugins.IDesktopPlugin.Initialize
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
                            Case 30
                                Me._UMVConnectionString = modulo.ConnectionString
                            Case 3
                                Me._ToolsConnectionString = modulo.ConnectionString
                            Case 35
                                Me._SofTracConnectionString = modulo.ConnectionString
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

        Public Sub Activate() Implements Miharu.Desktop.Library.Plugins.IDesktopPlugin.Activate
            Try
                Me._Wrapper = New ImagingWorkSpaceWrapperUMV(Me)
                Me._Wrapper.AplicarCambios()
            Catch ex As Exception
                MessageBox.Show("Error al activar el plugin " + GetName() + ", " + ex.Message)
            End Try
        End Sub

        Public Function GetCode() As String Implements Miharu.Desktop.Library.Plugins.IDesktopPlugin.GetCode
            Return "Unidad_De_Mantenimiento_VialPlugin"
        End Function

        Public Function GetName() As String Implements Miharu.Desktop.Library.Plugins.IDesktopPlugin.GetName
            Return "Libreria especializada para UMV"
        End Function

        Public Sub Close() Implements Miharu.Desktop.Library.Plugins.IDesktopPlugin.Close
        End Sub

        Public Function InitializeLifetimeService() As Object Implements Miharu.Desktop.Library.Plugins.IDesktopPlugin.InitializeLifetimeService
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