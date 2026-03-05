Imports System.Windows.Forms
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Plugins
Imports Miharu.Imaging.Library

Namespace Imaging

    Public Class Plugin
        Inherits MarshalByRefObject
        Implements IDesktopPlugin

#Region " Declaraciones "

        Private _EventExecuter As New EventExecuterImaging(Me)

        Private _Manager As DesktopPluginManager

        Private _WorkSpace As FormImagingWorkSpace

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

        Public ReadOnly Property Wrapper As Wrapper
            Get
                Return Me._Wrapper
            End Get
        End Property

#End Region

#Region " Implementacion IDesktopPlugin "

        Public Function IsValidPlugin(ByVal nProcessType As ProcessLibraryType, ByVal nEntidad As Integer, ByVal nProyecto As Integer) As Boolean Implements IDesktopPlugin.IsValidPlugin
            Program.InicializarConfiguracion()

            If (nProcessType <> ProcessLibraryType.Imaging) Then Return False
            If (nEntidad <> Program.AccionesValoresEntidadId) Then Return False
            If (nProyecto <> Program.AccionesValoresProyectoImagingId) Then Return False
            Return True
        End Function

        Public Sub Initialize(ByVal nManager As DesktopPluginManager) Implements IDesktopPlugin.Initialize
            Me._Manager = nManager
            Me._WorkSpace = CType(nManager.WorkSpace, FormImagingWorkSpace)
        End Sub

        Public Sub Activate() Implements IDesktopPlugin.Activate
            Try
                Me._Wrapper = New Wrapper(Me)
                Me.Wrapper.AplicarCambios()
            Catch ex As Exception
                MessageBox.Show("Error al activar el plugin " + GetName() + ", " + ex.Message)
            End Try
        End Sub

        Public Function GetCode() As String Implements IDesktopPlugin.GetCode

            Return "BanagrarioPlugin"
        End Function

        Public Function GetName() As String Implements IDesktopPlugin.GetName
            Return "Libreria especializada para Banagrario "
        End Function

        Public Sub Close() Implements IDesktopPlugin.Close

        End Sub

        Public Overrides Function InitializeLifetimeService() As Object Implements IDesktopPlugin.InitializeLifetimeService
            Return Nothing
        End Function

        Public ReadOnly Property EventExecuter As Miharu.Desktop.Library.Plugins.EventExecuter Implements IDesktopPlugin.EventExecuter
            Get
                Return Me._EventExecuter
            End Get
        End Property

#End Region

    End Class
End Namespace