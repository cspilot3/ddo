Namespace View.Indexacion.Table

    Public Class IndexerTableCellCollection
        Implements IEnumerable(Of IndexerTableCell)

#Region " Declaraciones "

        Private _Items As List(Of IndexerTableCell)

#End Region

#Region " Constructores "

        Public Sub New()
            _Items = New List(Of IndexerTableCell)
        End Sub

#End Region

#Region " Propiedades "

        Public ReadOnly Property Count As Integer
            Get
                Return _Items.Count
            End Get
        End Property

        Public ReadOnly Property IsReadOnly As Boolean
            Get
                Return False
            End Get
        End Property

        Default Public ReadOnly Property Item(ByVal index As Integer) As IndexerTableCell
            Get
                Return _Items.Item(index)
            End Get
        End Property

#End Region

#Region " Metodos "

        Public Sub Add(ByVal item As IndexerTableCell)
            _Items.Add(item)
        End Sub

        Public Sub Clear()
            _Items.Clear()
        End Sub

        Public Sub CopyTo(ByVal array() As IndexerTableCell, ByVal arrayIndex As Integer)
            _Items.CopyTo(array, arrayIndex)
        End Sub

#End Region

#Region " Funciones "

        Public Function Contains(ByVal item As IndexerTableCell) As Boolean
            Return _Items.Contains(item)
        End Function

        Public Function IndexOf(ByVal item As IndexerTableCell) As Integer
            Return _Items.IndexOf(item)
        End Function

        Public Function Remove() As IndexerTableCell
            If (_Items.Count = 0) Then Throw New Exception("No hay elementos para remover")

            Dim Value = _Items(_Items.Count - 1)
            _Items.Remove(Value)

            Return Value
        End Function

#End Region

#Region " Implement IEnumerable "

        Public Function GetEnumerator() As IEnumerator(Of IndexerTableCell) Implements IEnumerable(Of IndexerTableCell).GetEnumerator
            Return _Items.GetEnumerator()
        End Function

        Public Function GetEnumerator1() As IEnumerator Implements IEnumerable.GetEnumerator
            Return _Items.GetEnumerator()
        End Function

#End Region

    End Class

End Namespace
