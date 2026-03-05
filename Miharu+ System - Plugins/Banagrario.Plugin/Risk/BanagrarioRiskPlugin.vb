Imports Banagrario.Plugin.Risk.FormWrappers
Imports DBSecurity
Imports System.Windows.Forms
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Plugins
Imports Miharu.Risk.Library

Namespace Risk

    Public Class BanagrarioRiskPlugin
        Inherits MarshalByRefObject
        Implements IDesktopPlugin

#Region " Declaraciones "

        Private _EventExecuter As New EventExecuterRisk(Me)

        Private _Manager As DesktopPluginManager

        Private _WorkSpace As FormRiskWorkSpace

        Private _BancoAgrarioConnectionString As String

#End Region

#Region " Propiedades "

        Public ReadOnly Property Manager() As DesktopPluginManager
            Get
                Return Me._Manager
            End Get
        End Property

        Public ReadOnly Property WorkSpace() As FormRiskWorkSpace
            Get
                Return Me._WorkSpace
            End Get
        End Property

        Public ReadOnly Property BancoAgrarioConnectionString() As String
            Get
                Return Me._BancoAgrarioConnectionString
            End Get
        End Property

#End Region

#Region " Implementacion IDesktopPlugin "

        Public Function IsValidPlugin(ByVal nProcessType As ProcessLibraryType, ByVal nEntidad As Integer, ByVal nProyecto As Integer) As Boolean Implements IDesktopPlugin.IsValidPlugin
            Program.InicializarConfiguracion()

            If (nProcessType <> ProcessLibraryType.Risk) Then Return False
            If (nEntidad <> Program.BanagrarioEntidadId) Then Return False
            If (nProyecto <> Program.BanagrarioProyectoRiskId) Then Return False
            Return True
        End Function

        Public Sub Initialize(ByVal nManager As DesktopPluginManager) Implements IDesktopPlugin.Initialize
            Me._Manager = nManager
            Me._WorkSpace = CType(nManager.WorkSpace, FormRiskWorkSpace)

            Dim dbmSecurity As DBSecurityDataBaseManager = Nothing

            Try
                dbmSecurity = New DBSecurityDataBaseManager(Me.Manager.DesktopGlobal.ConnectionStrings.Security)
                dbmSecurity.Connection_Open(1) ' System

                Dim ModuloDataTable = dbmSecurity.SchemaSecurity.TBL_Modulo.DBGet(Program.ModuloId)

                If (ModuloDataTable.Count > 0) Then
                    Me._BancoAgrarioConnectionString = ModuloDataTable(0).ConnectionString
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
                Dim frwWrapper As New FormRiskWorkSpaceWrapper(Me)
                frwWrapper.AplicarCambios()
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

        Public ReadOnly Property EventExecuter As Miharu.Desktop.Library.Plugins.EventExecuter Implements Miharu.Desktop.Library.Plugins.IDesktopPlugin.EventExecuter
            Get
                Return Me._EventExecuter
            End Get
        End Property

#End Region

    End Class

End Namespace