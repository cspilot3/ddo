Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library.Plugins
Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Library

Namespace Imaging.ConciliacionRecaudos
    Public Class FormImagingkWorkSpaceWrapperCR

#Region " Declaraciones "
        Public _plugin As ConciliacionRecaudosPlugin = Nothing
#End Region


#Region " Constructores "
        Public Sub New(ByVal nPlugin As ConciliacionRecaudosPlugin)
            _plugin = nPlugin
        End Sub
#End Region

        
#Region " Metodos "
        Public Sub AplicarCambios()
            Try

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("No fue posible aplicar los cambios de E-pago Conciliacion Recaudos al workspace, " + ex.Message, "Plugin workspace", DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
            End Try
        End Sub

        Public Sub DeshacerCambios()
            Try

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("No fue posible deshacer los cambios de E-pago Conciliacion Recaudos al workspace, " + ex.Message, "Plugin workspace", DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
            End Try
        End Sub
#End Region
        

    End Class
End Namespace

