Imports Miharu.Desktop.Controls.DesktopMessageBox

Namespace Imaging.Beps.CBarras


    Public Class CbarrasForm

#Region " Declaraciones "

        Private _Plugin As Plugin

#End Region

#Region " Contructores "

        Public Sub New(ByVal nPlugin As Plugin)
            InitializeComponent()

            _Plugin = nPlugin
        End Sub

#End Region
        Private Sub btnbuscar_Click(sender As System.Object, e As System.EventArgs) Handles btnbuscar.Click
            Dim dbmIntegration As New DBIntegration.DBIntegrationDataBaseManager(Me._Plugin.ColpensionesConnectionString)

            Try
                dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow(ex.ToString(), "fallo", DesktopMessageBoxControl.IconEnum.ErrorIcon)
            Finally
                If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
            End Try
        End Sub
    End Class
End Namespace