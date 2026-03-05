Imports Miharu.Desktop.Library.Config
Imports Miharu.Security.Library.Session
Imports System.Windows.Forms
Imports Miharu.Desktop.Library.Plugins

Namespace Forms

    Public MustInherit Class IProcessLibrary

        MustOverride Property WorkSpace As IWorkspace

        Protected _PluginManager As DesktopPluginManager

        Public Event OnProyectoSelected()

        Public Event OnWorkspaceActivated()

        MustOverride Sub SetSesion(ByRef nSesion As Sesion)

        MustOverride Sub SetDesktopGlobal(ByRef nDesktopGlobal As DesktopGlobal)

        Protected MustOverride Sub ShowWorkSpace(ByVal nFormMain As Form)

        Protected MustOverride Function SelectProject() As Boolean

        Public Sub CallShowWorkSpace(ByVal nFormMain As Form)
            ShowWorkSpace(nFormMain)
        End Sub

        Public Function CallSelectProject() As Boolean
            Return SelectProject()
            'Cargar e instanciar los plugins que aplican para el proyecto seleccionado
            '_PluginManager.ChangePluingsProyecto(Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto)
        End Function


        Public Sub SetPluginManager(ByVal nPlugingManager As DesktopPluginManager)
            _PluginManager = nPlugingManager
        End Sub

    End Class
End Namespace