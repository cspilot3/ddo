Namespace View.Indexacion.Table

    Public Class IndexerTableRowCollection
        Implements IEnumerable(Of IndexerTableRow)

#Region " Declaraciones "

        Private _Items As List(Of IndexerTableRow)

        Public Event AddRowEvent(ByRef Row As IndexerTableRow)
        Public Event ClearRowsEvent()
        Public Event RemoveRowEvent(ByRef Row As IndexerTableRow)

#End Region

#Region " Constructores "

        Public Sub New()
            _Items = New List(Of IndexerTableRow)
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

        Default Public ReadOnly Property Item(ByVal index As Integer) As IndexerTableRow
            Get
                Return _Items.Item(index)
            End Get
        End Property

#End Region

#Region " Metodos "

        Public Sub Add(ByVal nItem As IndexerTableRow)
            _Items.Add(nItem)
            RaiseEvent AddRowEvent(nItem)
        End Sub

        Public Sub Clear()
            _Items.Clear()
            RaiseEvent ClearRowsEvent()
        End Sub

        Public Sub CopyTo(ByVal array() As IndexerTableRow, ByVal arrayIndex As Integer)
            _Items.CopyTo(array, arrayIndex)
        End Sub

#End Region

#Region " Funciones "

        Public Function Contains(ByVal nItem As IndexerTableRow) As Boolean
            Return _Items.Contains(nItem)
        End Function

        Public Function IndexOf(ByVal nItem As IndexerTableRow) As Integer
            Return _Items.IndexOf(nItem)
        End Function

        Public Function Remove() As IndexerTableRow
            If (_Items.Count = 0) Then Throw New Exception("No hay elementos para remover")

            Dim Value = _Items(_Items.Count - 1)
            _Items.Remove(Value)

            RaiseEvent RemoveRowEvent(Value)

            Return Value
        End Function

        Public Function RemoveIndex(ByVal rowIndex As Integer) As IndexerTableRow
            If (_Items.Count = 0) Then Throw New Exception("No hay elementos para remover")

            Dim Value = _Items(rowIndex)
            _Items.Remove(Value)

            RaiseEvent RemoveRowEvent(Value)

            Return Value
        End Function

#End Region

#Region " Implement IEnumerable "

        Public Function GetEnumerator() As IEnumerator(Of IndexerTableRow) Implements IEnumerable(Of IndexerTableRow).GetEnumerator
            Return _Items.GetEnumerator()
        End Function

        Public Function GetEnumerator1() As IEnumerator Implements IEnumerable.GetEnumerator
            Return _Items.GetEnumerator()
        End Function

#End Region

    End Class

End Namespace
