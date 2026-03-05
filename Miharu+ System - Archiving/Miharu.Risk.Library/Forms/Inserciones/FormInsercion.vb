Imports System.Windows.Forms
Imports Miharu.Desktop.Library

Namespace Forms.Inserciones

    Public Class FormInsercion
        Inherits FormBase

#Region " Propiedades "

        Public Property Tipologia As String
            Get
                Return TipologiaLabel.Text
            End Get
            Set(ByVal value As String)
                TipologiaLabel.Text = value
            End Set
        End Property

        Public Property Folios As Short
            Get
                Return CShort(FoliosTextBox.Text)
            End Get
            Set(ByVal value As Short)
                FoliosTextBox.Text = CStr(value)
            End Set
        End Property

#End Region

#Region " Eventos "

        Private Sub FormInsercion_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            FoliosTextBox.SelectAll()
            FoliosTextBox.Focus()
        End Sub

        Private Sub AceptarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AceptarButton.Click
            If (Validar()) Then
                Me.DialogResult = DialogResult.OK
                Me.Close()
            Else
                Me.DialogResult = DialogResult.None
            End If
        End Sub

        Private Sub CancelarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CancelarButton.Click
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        End Sub

#End Region

#Region " Funciones "

        Private Function Validar() As Boolean
            If (FoliosTextBox.Text = "") Then
                MessageBox.Show("Debe ingresar el número de folios", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning)

                FoliosTextBox.Focus()
                FoliosTextBox.SelectAll()

            ElseIf (Not IsNumeric(FoliosTextBox.Text)) Then
                MessageBox.Show("DEl número de folios debe ser un valor numérico", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning)

                FoliosTextBox.Focus()
                FoliosTextBox.SelectAll()

            ElseIf (Val(FoliosTextBox.Text) <= 0) Then
                MessageBox.Show("DEl número de folios debe ser un valor mayor a cero", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning)

                FoliosTextBox.Focus()
                FoliosTextBox.SelectAll()

            Else
                Return True
            End If

            FoliosTextBox.Focus()
            FoliosTextBox.SelectAll()

            Return False
        End Function

#End Region

    End Class

End Namespace