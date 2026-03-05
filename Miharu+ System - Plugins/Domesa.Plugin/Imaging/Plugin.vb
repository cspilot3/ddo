Imports Miharu.Desktop.Library.Plugins
Imports Miharu.Desktop.Library
Imports Miharu.Imaging.Library
Imports System.Windows.Forms

Namespace Imaging

    Public Class Plugin
        Implements IDesktopPlugin

#Region " Declaraciones "

        Private _EventExecuter As New ImagingEventExecuter(Me)

        Private _Manager As DesktopPluginManager

        Private _WorkSpace As FormImagingWorkSpace

        Private _DomesaConnectionString As String

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

        Public ReadOnly Property DomesaConnectionString() As String
            Get
                Return Me._DomesaConnectionString
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
            If (nEntidad <> Program.DomesaEntidadId) Then Return False
            If (nProyecto <> Program.DomesaProyectoImagenesId) Then Return False
            Return True
        End Function

        Public Sub Initialize(ByVal nManager As DesktopPluginManager) Implements IDesktopPlugin.Initialize
            Me._Manager = nManager
            Me._WorkSpace = CType(nManager.WorkSpace, FormImagingWorkSpace)

            Dim dbmSecurity As DBSecurity.DBSecurityDataBaseManager = Nothing
            Try
                dbmSecurity = New DBSecurity.DBSecurityDataBaseManager(Me.Manager.DesktopGlobal.ConnectionStrings.Security)
                dbmSecurity.Connection_Open(1) ' System

                Dim ModuloDataTable = dbmSecurity.SchemaSecurity.TBL_Modulo.DBGet(Program.ModuloId)

                If (ModuloDataTable.Count > 0) Then
                    Me._DomesaConnectionString = ModuloDataTable(0).ConnectionString
                Else
                    Throw New Exception("No se pudo cargar la cadena de conexión para el módulo: " & Program.ModuloId.ToString())
                End If
            Catch
                Throw
            Finally
                If (dbmSecurity IsNot Nothing) Then dbmSecurity.Connection_Close()
            End Try
        End Sub

        Public Sub Activate() Implements IDesktopPlugin.Activate
            Try
                Me._Wrapper = New Wrapper(Me)
                Me._Wrapper.AplicarCambios()
            Catch ex As Exception
                MessageBox.Show("Error al activar el plugin " + GetName() + ", " + ex.Message)
            End Try
        End Sub

        Public Function GetCode() As String Implements IDesktopPlugin.GetCode
            Return "DomesaPluginImaging"
        End Function

        Public Function GetName() As String Implements IDesktopPlugin.GetName
            Return "Libreria especializada para el cliente Domesa - Imágenes"
        End Function

        Public Sub Close() Implements IDesktopPlugin.Close
            Me._Wrapper.DeshacerCambios()
            Me._Wrapper = Nothing
        End Sub

        Public Function InitializeLifetimeService() As Object Implements IDesktopPlugin.InitializeLifetimeService
            Return Nothing
        End Function

        Public ReadOnly Property EventExecuter() As EventExecuter Implements IDesktopPlugin.EventExecuter
            Get
                Return Me._EventExecuter
            End Get
        End Property

#End Region

    End Class

End Namespace