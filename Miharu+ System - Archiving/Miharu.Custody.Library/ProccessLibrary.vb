Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Library.Forms
Imports Miharu.Desktop.Library
Imports System.Windows.Forms

Public Class ProccessLibrary
    Inherits IProcessLibrary

    Private _WorkSpace As FormCustodyWorkSpace

#Region " Implementación IProcessLibrary "

    Public Overrides Property WorkSpace As IWorkspace
        Get
            Return _WorkSpace
        End Get
        Set(ByVal value As IWorkspace)
            If (value Is Nothing) Then
                _WorkSpace = Nothing
            Else
                _WorkSpace = CType(value, FormCustodyWorkSpace)
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
        Return True
    End Function

    Protected Overrides Sub ShowWorkSpace(ByVal formMain As Form)
        If (_WorkSpace Is Nothing OrElse _WorkSpace.IsDisposed) Then
            _WorkSpace = New FormCustodyWorkSpace()
            _WorkSpace.ProcessLibrary = Me
            _WorkSpace.MdiParent = formMain
        End If

        _WorkSpace.ConfigWorkspace()
        _WorkSpace.Show()        
    End Sub

#End Region

End Class
