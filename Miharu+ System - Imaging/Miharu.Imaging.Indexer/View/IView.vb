Imports System.Windows.Forms
Imports Slyg.Tools.Imaging

Namespace View

    Public Interface IView

        Function ShowDialog() As DialogResult
        Property Image As FreeImageAPI.FreeImageBitmap
        Property Unlock As Boolean
        ReadOnly Property Controller As Controller.IController
        ReadOnly Property ThumbnailHeight As Integer
        ReadOnly Property ThumbnailHelper As DropThumbnailHelper
        ReadOnly Property ThumbnailPanel As FlowLayoutPanel
        ReadOnly Property ThumbnailWidth As Integer
        ReadOnly Property ViewClosing As Boolean
        Sub ActivarControles(ByVal nActivo As Boolean)
        Sub Clear()
        Sub ScrollThumbnail()
        Sub SetController(nController As Object)
        Sub SetTitle(ByVal title As String)
        Sub Thumbnail_Click(ByVal sender As System.Object, ByVal e As EventArgs)
        Sub Thumbnail_MouseEnter(ByVal sender As System.Object, ByVal e As EventArgs)
        Sub Thumbnail_MouseLeave(ByVal sender As System.Object, ByVal e As EventArgs)
        Sub UpdateAvance()
        Sub UpdateNombreImagen()

        Sub ShowImagen(ByVal UpdateInfo As Boolean)

        Sub NextFolio()
        Function PreviousFolio() As Boolean
        Sub ShowImagenRuta(ByVal path As String)


    End Interface

End Namespace