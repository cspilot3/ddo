Namespace View.Indexacion

    Public Class FormAutoIndexar

#Region " Propiedades "

        Public Property Esquema As String
            Get
                Return Me.EsquemaTextBox.Text
            End Get
            Set(value As String)
                Me.EsquemaTextBox.Text = value
            End Set
        End Property

        Public Property Documento As String
            Get
                Return Me.DocumentoTextBox.Text
            End Get
            Set(value As String)
                Me.DocumentoTextBox.Text = value
            End Set
        End Property

        Public ReadOnly Property Folios As Integer
            Get
                Return CInt(Me.FoliosNumericUpDown.Value)
            End Get
        End Property

        Public Property MaxFolios As Integer
            Get
                Return CInt(Me.FoliosNumericUpDown.Maximum)
            End Get
            Set(value As Integer)
                Me.FoliosNumericUpDown.Maximum = value
            End Set
        End Property

        Public ReadOnly Property Modo As ModoAutoIndexarEnum
            Get
                If (Me.FolderRadioButton.Checked) Then
                    Return ModoAutoIndexarEnum.Folder
                Else
                    Return ModoAutoIndexarEnum.Documento
                End If
            End Get
        End Property

#End Region

#Region " Eventos "

        Private Sub CancelarButton_Click(sender As System.Object, e As EventArgs) Handles CancelarButton.Click
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
        End Sub

        Private Sub AceptarButton_Click(sender As System.Object, e As EventArgs) Handles AceptarButton.Click
            If (Validar()) Then
                Me.DialogResult = Windows.Forms.DialogResult.OK
            Else
                Me.DialogResult = Windows.Forms.DialogResult.None
            End If
        End Sub

#End Region

#Region " Funciones "

        Private Function Validar() As Boolean
            Return True
        End Function

#End Region

    End Class

End Namespace