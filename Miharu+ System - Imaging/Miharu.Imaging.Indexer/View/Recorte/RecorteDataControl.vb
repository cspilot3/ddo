Imports DBCore.SchemaImaging
Imports System.Drawing

Namespace View.Recorte

    Public Class RecorteDataControl
        Implements IDataControl

#Region " Eventos "

        Public Event OnDataButtonClick(ByVal sender As RecorteDataControl)
        Public Event OnDeleteButtonClick(ByVal sender As RecorteDataControl)

        Private Sub DataButton_Click(sender As System.Object, e As EventArgs) Handles DataButton.Click
            RaiseEvent OnDataButtonClick(Me)
        End Sub

        Private Sub Eliminar_Click(sender As System.Object, e As EventArgs) Handles DeleteButton.Click
            RaiseEvent OnDeleteButtonClick(Me)

            Me.Selector = Nothing
        End Sub

#End Region

#Region " Implementacion IDataControl "

        Public Property id As Short Implements IDataControl.id

        Public Property Label As String Implements IDataControl.Label
            Get
                Return DataButton.Text
            End Get
            Set(value As String)
                DataButton.Text = value
            End Set
        End Property

        Private _Selected As Boolean
        Public Property Selected As Boolean Implements IDataControl.Selected
            Get
                Return _Selected
            End Get
            Set(value As Boolean)
                _Selected = value

                If (_Selected) Then
                    DataButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                Else
                    DataButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                End If
            End Set
        End Property

        Private _Selector As Selector
        Public Property Selector As Selector Implements IDataControl.Selector
            Get
                Return _Selector
            End Get
            Set(value As Selector)
                DeleteButton.Visible = (value IsNot Nothing AndAlso Not Me.Data.Bloqueado)

                If (Me.Data.Bloqueado) Then
                    DataButton.ForeColor = Color.Green
                Else
                    DataButton.ForeColor = Color.Black
                End If

                _Selector = value
            End Set
        End Property

        Public Property Data As TBL_File_RecorteRow Implements IDataControl.Data

#End Region

    End Class

End Namespace