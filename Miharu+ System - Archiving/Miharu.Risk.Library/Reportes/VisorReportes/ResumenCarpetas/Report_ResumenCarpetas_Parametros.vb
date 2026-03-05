Namespace Reportes.VisorReportes.ResumenCarpetas

    Public Class Report_ResumenCarpetas_Parametros

#Region " Propiedades "

        Public Property OT() As Integer
            Get
                Try
                    Return CInt(OTTextBox.Text)
                Catch ex As Exception
                    MsgBox("Numero de OT no valido", MsgBoxStyle.Critical, "Error")
                End Try
                Return CInt(False)
            End Get
            Set(ByVal value As Integer)
                OTTextBox.Text = value.ToString
            End Set
        End Property

#End Region

#Region " Constructores "

        Public Sub New()
            ' This call is required by the designer.
            InitializeComponent()
        End Sub

#End Region

#Region " Eventos "

        Private Sub CancelarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CancelarButton.Click
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
            Me.Close()
        End Sub

        Private Sub AceptarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AceptarButton.Click
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        End Sub

#End Region

#Region " Metodos "


#End Region

    End Class

End Namespace