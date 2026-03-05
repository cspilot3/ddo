Imports System.Text
Imports System.Reflection
Imports System.IO
Imports System.Windows.Forms
Imports Miharu.Desktop.Library.Config
Imports Miharu.Security.Library.Session

Namespace Plugins

    Public Class DesktopPluginManager

#Region " Declaraciones "

        Private _plugins As New List(Of IDesktopPlugin)
        Private _pluginsPath As String
        Private _workSpace As Form

#End Region

#Region " Propiedades "
        Public Property Sesion() As Sesion

        Public Property RiskGlobal() As RiskGlobal

        Public Property ImagingGlobal() As ImagingGlobal

        Public Property DesktopGlobal() As DesktopGlobal

        Public ReadOnly Property Plugins() As List(Of IDesktopPlugin)
            Get
                Return _Plugins
            End Get
        End Property

        Public ReadOnly Property PluginsPath() As String
            Get
                Return _PluginsPath
            End Get
        End Property

        Public ReadOnly Property WorkSpace() As Form
            Get
                Return _WorkSpace
            End Get
        End Property


#End Region

#Region " Constructores "

        Public Sub New(ByVal nPluginsPath As String, ByVal nWorkSpace As Form, ByVal nSession As Sesion)
            _PluginsPath = nPluginsPath
            _WorkSpace = nWorkSpace
            '_DesktopConfig = nDesktopConfig
            '_ComplementsMenu = nComplementsMenu
            Sesion = nSession
        End Sub

#End Region

#Region " Metodos "

        Public Sub LoadPlugins(ByVal processType As ProcessLibraryType, ByVal nEntidad As Integer, ByVal nProyecto As Integer)
            Try
                ClosePlugins()

                Dim mpath As String = _PluginsPath

                Const fileExtension As String = "*.dll"
                Dim iFilter As New TypeFilter(AddressOf InterfaceFilter)


                If (Directory.Exists(mpath)) Then
                    Dim files() As String = Directory.GetFiles(mpath, fileExtension)
                    If (files.Length > 0) Then

                        For Each sfile As String In files
                            Dim assemb As [Assembly] = [Assembly].LoadFile(sfile)
                            Dim types() As Type
                            Try
                                types = assemb.GetTypes()
                            Catch ex As ReflectionTypeLoadException
                                Dim innerEx As String = ""
                                For Each lEx In ex.LoaderExceptions
                                    innerEx += lEx.Message + ","
                                Next
                                Throw New Exception("Libreria contiene errores, " + ex.Message + ", " + innerEx + ", Intente actualizar la libreria para solucionar el problema", ex)
                            Catch ex As Exception
                                Throw New Exception("Libreria contiene errores, " + ex.Message + ", Intente actualizar la libreria para solucionar el problema", ex)
                            End Try

                            For Each oType As Type In types
                                Try
                                    If (oType.IsClass) Then
                                        Dim interfaces() As Type = oType.FindInterfaces(iFilter, GetType(IDesktopPlugin).FullName)

                                        If (interfaces.Length > 0) Then
                                            Dim pluginInstance As IDesktopPlugin = assemb.CreateInstance(oType.FullName)
                                            If (pluginInstance.IsValidPlugin(processType, nEntidad, nProyecto)) Then
                                                pluginInstance.Initialize(Me)
                                                Plugins.Add(pluginInstance)
                                            End If
                                        End If
                                    End If
                                Catch ex As Exception
                                    Throw New Exception("No fue posible instanciar el plugin: " & sfile & ", " & ex.Message, ex)
                                End Try
                            Next
                        Next
                    End If
                End If

                'If (Not _ComplementsMenu Is Nothing) Then
                '    _ComplementsMenu.Visible = (_ComplementsMenu.MenuItems.Count > 0)
                'End If
            Catch ex As Exception
                Throw New Exception("No fue posible cargar los plugins, " + ex.Message, ex)
            End Try

        End Sub

        Public Sub Activateplugins()
            For Each plugin In Plugins
                Try
                    plugin.Activate()
                Catch ex As Exception
                    Throw New Exception("Error al activar el plugin " + plugin.GetName() + ", " + ex.Message, ex)
                End Try
            Next

        End Sub

        Public Sub ClosePlugins()
            Dim errors As New StringBuilder()

            For Each plugin In Plugins
                Try
                    plugin.Close()

                Catch ex As Exception
                    errors.AppendLine("Plugin " & plugin.GetName & " Error:" & ex.Message & "; ")
                End Try
            Next

            Try
                Plugins.Clear()
            Catch ex As Exception
                errors.AppendLine(" ," & ex.Message & "; ")
            End Try

            If (errors.Length > 0) Then
                Throw New Exception("Ocurrio un error al cerrar los plugins cargados, " & errors.ToString())
            End If
        End Sub

#End Region

#Region " Funciones "

        Public Shared Function InterfaceFilter(ByVal typeObj As Type, ByVal criteriaObj As [Object]) As Boolean
            If typeObj.ToString() = criteriaObj.ToString() Then
                Return True
            Else
                Return False
            End If
        End Function 'MyInterfaceFilter 

#End Region

    End Class

End Namespace