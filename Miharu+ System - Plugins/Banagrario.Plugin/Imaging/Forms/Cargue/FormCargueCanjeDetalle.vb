Namespace Imaging.Forms.Cargue

    Public Class FormCargueCanjeDetalle

        Private _erroresCargue As List(Of String)
        Private _adventenciasCargue As List(Of String)

        Sub New(ByVal ErroresCargue As List(Of String), ByVal AdventenciasCargue As List(Of String))
            InitializeComponent()

            _erroresCargue = ErroresCargue
            _adventenciasCargue = AdventenciasCargue
        End Sub

        Private Sub FormCargueCanjeDetalle_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            For Each s In _erroresCargue
                Errores_TextBox.AppendText(s & vbCrLf & vbCrLf)
            Next

            For Each s In _adventenciasCargue
                Advertencias_TextBox.AppendText(s & vbCrLf)
            Next

            If (_erroresCargue.Count > 0) Then
                AceptarButton.Enabled = False
            End If
        End Sub

        Private Sub AceptarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AceptarButton.Click
            DialogResult = Windows.Forms.DialogResult.OK
            Close()
        End Sub

        Private Sub CerrarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CerrarButton.Click
            DialogResult = Windows.Forms.DialogResult.Cancel
            Close()
        End Sub

    End Class

End Namespace