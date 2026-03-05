Imports System.Windows.Forms
Imports Miharu.Risk.Library.Forms
Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Library.Forms
Imports Miharu.Desktop.Library

Public Class ProcessLibrary
    Inherits IProcessLibrary

    Private _WorkSpace As FormRiskWorkSpace

    Public Sub New()

    End Sub

#Region " Implementación IProcessLibrary "

    Public Overrides Property WorkSpace As IWorkspace
        Get
            Return _WorkSpace
        End Get
        Set(ByVal value As IWorkspace)
            If (value Is Nothing) Then
                _WorkSpace = Nothing
            Else
                _WorkSpace = CType(value, FormRiskWorkSpace)
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
        Dim formSeleccionarProyecto = New FormSeleccionarProyecto()
        If formSeleccionarProyecto.ShowDialog() = DialogResult.OK Then
            Return True
        Else
            Return False
        End If
    End Function

    Protected Overrides Sub ShowWorkSpace(ByVal formMain As Form)
        If (_WorkSpace Is Nothing OrElse _WorkSpace.IsDisposed) Then
            _WorkSpace = New FormRiskWorkSpace()
            _WorkSpace.ProcessLibrary = Me
            '_WorkSpace.MdiParent = CType(formMain, IFormMDI)
            _WorkSpace.MdiParent = formMain
        End If

        _WorkSpace.ConfigWorkspace()
        _WorkSpace.Show()        
    End Sub

#End Region

End Class
