Imports System.Windows.Forms
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Config

Namespace Forms.CBarras

    Public Class FormImprimirCBarras
        Inherits FormBase

#Region " Declaraciones "

        Structure DatosCore
            Public Existe As Boolean
            Public Expediente As String
            Public Folder As String
        End Structure

        Dim _TableCBarras As DataTable

#End Region

#Region " Constructor "
        Sub New(ByVal TableCBarras As DataTable)
            ' This call is required by the designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            _TableCBarras = TableCBarras
        End Sub
#End Region

#Region " Eventos "

        Private Sub ImprimirButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ImprimirButton.Click
            Utilities.IniciarImpresion(Program.DesktopGlobal.ConnectionStrings, Program.Sesion, _TableCBarras, CBarrasProgressBar)
            Me.DialogResult = DialogResult.OK
        End Sub

        Private Sub CerrarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CerrarButton.Click
            Me.Close()
            Me.DialogResult = DialogResult.Cancel
        End Sub
#End Region

    End Class

End Namespace