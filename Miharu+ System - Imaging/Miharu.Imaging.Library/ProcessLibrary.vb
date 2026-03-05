Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Library.Forms
Imports Miharu.Desktop.Library
Imports Miharu.Imaging.Library.Procesos

Public Class ProcessLibrary
    Inherits IProcessLibrary

    Private _WorkSpace As FormImagingWorkSpace

#Region " Implementación IProcessLibrary "

    Public Overrides Property WorkSpace As IWorkspace
        Get
            Return _WorkSpace
        End Get
        Set(ByVal value As IWorkspace)
            If (value Is Nothing) Then
                _WorkSpace = Nothing
            Else
                _WorkSpace = CType(value, FormImagingWorkSpace)
            End If
        End Set
    End Property

    Public Overrides Sub SetDesktopGlobal(ByRef nDesktopGlobal As DesktopGlobal)
        Program.DesktopGlobal = nDesktopGlobal
    End Sub

    Public Overrides Sub SetSesion(ByRef nSesion As Security.Library.Session.Sesion)
        Program.Sesion = nSesion
    End Sub

    Protected Overrides Function SelectProject() As Boolean

        Dim formSeleccionarProyecto = New Form
        If Program.Sesion.IsExternal Then
            formSeleccionarProyecto = New FormSeleccionarProyectoDMZ()
        Else
            formSeleccionarProyecto = New FormSeleccionarProyecto()
        End If

        'Dim formSeleccionarProyecto = New FormSeleccionarProyecto()
        If formSeleccionarProyecto.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Return True
        Else
            Return False
        End If
    End Function

    Protected Overrides Sub ShowWorkSpace(ByVal formMain As System.Windows.Forms.Form)
        Try
            If (_WorkSpace Is Nothing OrElse _WorkSpace.IsDisposed) Then
                _WorkSpace = New FormImagingWorkSpace()
                _WorkSpace.ProcessLibrary = Me
                _WorkSpace.MdiParent = formMain
            End If

            _WorkSpace.ConfigWorkspace()
            _WorkSpace.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

End Class
