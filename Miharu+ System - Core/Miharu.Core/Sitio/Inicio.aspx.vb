Imports Miharu.Core.Clases
Imports Miharu.Core.Main

Namespace Sitio

    Partial Public Class Inicio
        Inherits FormBase

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            BodyType = BodyType.Unique
            MyMasterPage.ToolControl.DisableAction("Find")
            MyMasterPage.ToolControl.DisableAction("New")
            MyMasterPage.ToolControl.DisableAction("Save")
            MyMasterPage.ToolControl.DisableAction("Edit")
            MyMasterPage.ToolControl.DisableAction("Delete")
            'MyMasterPage.ToolControl.DisableAction("Export")
        End Sub
    End Class
End Namespace