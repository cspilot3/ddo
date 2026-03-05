Public Class FormLogin

#Region " Propiedades "

    Public ReadOnly Property Login() As String
        Get
            Return Me.LoginTextBox.Text
        End Get
    End Property

    Public ReadOnly Property Password() As String
        Get
            Return Me.PasswordTextBox.Text
        End Get
    End Property

#End Region

#Region " Eventos "

    Private Sub FormLogin_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
        SelectText()

#If DEBUG Then
        Try
            If (System.IO.File.Exists(Program.AppPath & "user.txt")) Then
                Dim UserFile = New System.IO.StreamReader(Program.AppPath & "user.txt", System.Text.Encoding.UTF7)

                Me.LoginTextBox.Text = UserFile.ReadLine()
                Me.PasswordTextBox.Text = UserFile.ReadLine()

                Me.AceptarButton.Focus()
            End If
        Catch
        End Try
#End If
    End Sub

    Private Sub AceptarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AceptarButton.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Hide()
    End Sub

    Private Sub CancelarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CancelarButton.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Hide()
    End Sub

#End Region

#Region " Metodos "

    Public Sub SelectText()
        LoginTextBox.Focus()
        LoginTextBox.SelectAll()
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Me.PasswordTextBox.UseSystemPasswordChar = Not Me.PasswordTextBox.UseSystemPasswordChar
    End Sub


#End Region

End Class