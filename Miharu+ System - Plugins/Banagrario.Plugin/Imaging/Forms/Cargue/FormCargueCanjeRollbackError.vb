Namespace Imaging.Forms.Cargue

    Public Class FormCargueCanjeRollbackError

        Private _erroresCargue As List(Of String)

        Sub New(ByVal ErroresCargue As List(Of String))
            InitializeComponent()

            _erroresCargue = ErroresCargue
        End Sub

        Private Sub FormCargueCanjeDetalle_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            For Each s In _erroresCargue
                Errores_TextBox.AppendText(s & vbCrLf & vbCrLf)
            Next
        End Sub

        Private Sub CerrarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CerrarButton.Click
            DialogResult = Windows.Forms.DialogResult.Cancel
            Close()
        End Sub

    End Class

End Namespace