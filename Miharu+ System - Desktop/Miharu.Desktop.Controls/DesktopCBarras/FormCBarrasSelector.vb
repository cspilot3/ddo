Imports System.Windows.Forms

Namespace DesktopCBarras

    Public Class FormCBarrasSelector

        Public Sub LoadData(nData As DBArchiving.SchemaPLight.CTA_Homologacion_CBarrasDataTable)
            Me.selectorDataGridView.DataSource = nData
        End Sub

        Public ReadOnly Property Index() As Integer
            Get
                Return Me.selectorDataGridView.CurrentRow.Index
            End Get
        End Property


        Private Sub selectorDataGridView_CellDoubleClick(sender As Object, e As Windows.Forms.DataGridViewCellEventArgs) Handles selectorDataGridView.CellDoubleClick
            Me.DialogResult = DialogResult.OK
            Me.Close()
        End Sub

    End Class

End Namespace