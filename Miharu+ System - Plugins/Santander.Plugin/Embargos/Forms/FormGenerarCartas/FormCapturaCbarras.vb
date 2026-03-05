Imports System.Windows.Forms
Imports DBCore
Imports System.Drawing


Namespace Embargos.Forms.FormGenerarCartas.FormCapturaCbarras
    Public Class FormCapturaCbarras

        Private _plugin As EmbargosImagingPlugin

        Public Sub New(nPlugin As EmbargosImagingPlugin)
            _plugin = nPlugin
            InitializeComponent()
        End Sub

        Private Sub CbarrasCapturaTextBox_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles CbarrasCapturaTextBox.KeyPress
            If e.KeyChar = Convert.ToChar(13) Then
                Dim dbmCore As DBCoreDataBaseManager = Nothing
                Try
                    dbmCore = New DBCoreDataBaseManager(_plugin.Manager.DesktopGlobal.ConnectionStrings.Core)
                    dbmCore.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                    Dim fileCore = dbmCore.SchemaProcess.TBL_File.DBFindByCBarras_File(CbarrasCapturaTextBox.Text)

                    If fileCore.Rows.Count = 0 Then
                        MessageBox.Show("No se encuentra un documento con el código de barras " & CbarrasCapturaTextBox.Text)
                    Else
                        DialogResult = DialogResult.OK
                        Close()
                    End If

                Catch ex As Exception
                    MessageBox.Show("Error : " & ex.Message, "GenerarCartas", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    If dbmCore IsNot Nothing Then dbmCore.Connection_Close()
                End Try

            End If
        End Sub

        Private Sub AceptarButton_Click(sender As System.Object, e As System.EventArgs) Handles AceptarButton.Click
            DialogResult = DialogResult.OK
            Close()
        End Sub

        Private Sub FormCapturaCbarras_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            pnlImage.Height = picImage.Image.Height
            pnlImage.Width = picImage.Image.Width
            pnlMarcoDibujo.AutoScrollPosition = New Point(pnlImage.Width - CInt(pnlImage.Width * 0.47), pnlImage.Height - CInt(pnlImage.Height * 0.47))

        End Sub
    End Class
End Namespace
