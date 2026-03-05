Namespace View.Indexacion.Table

    Public Class IndexerTableColumnCollection
        Implements IEnumerable(Of IndexerTableColumn)

#Region " Declaraciones "

        Private _Items As List(Of IndexerTableColumn)

        Public Event AddColumnEvent(ByRef Column As IndexerTableColumn)
        Public Event ClearColumnsEvent()
        Public Event RemoveColumnEvent(ByRef Column As IndexerTableColumn)

#End Region

#Region " Constructores "

        Public Sub New()
            _Items = New List(Of IndexerTableColumn)
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

        Default Public ReadOnly Property Item(ByVal index As Integer) As IndexerTableColumn
            Get
                Return _Items.Item(index)
            End Get
        End Property

#End Region

#Region " Metodos "

        Public Sub Add(ByVal nItem As IndexerTableColumn)
            _Items.Add(nItem)
            RaiseEvent AddColumnEvent(nItem)
        End Sub

        Public Sub Clear()
            _Items.Clear()
            RaiseEvent ClearColumnsEvent()
        End Sub

        Public Sub CopyTo(ByVal array() As IndexerTableColumn, ByVal arrayIndex As Integer)
            _Items.CopyTo(array, arrayIndex)
        End Sub

#End Region

#Region " Funciones "

        Public Function Contains(ByVal nItem As IndexerTableColumn) As Boolean
            Return _Items.Contains(nItem)
        End Function

        Public Function IndexOf(ByVal nItem As IndexerTableColumn) As Integer
            Return _Items.IndexOf(nItem)
        End Function

        Public Function Remove() As IndexerTableColumn
            If (_Items.Count = 0) Then Throw New Exception("No hay elementos para remover")

            Dim Value = _Items(_Items.Count - 1)
            _Items.Remove(Value)

            RaiseEvent RemoveColumnEvent(Value)

            Return Value
        End Function

#End Region

#Region " Implement IEnumerable "

        Public Function GetEnumerator() As IEnumerator(Of IndexerTableColumn) Implements IEnumerable(Of IndexerTableColumn).GetEnumerator
            Return _Items.GetEnumerator()
        End Function

        Public Function GetEnumerator1() As IEnumerator Implements IEnumerable.GetEnumerator
            Return _Items.GetEnumerator()
        End Function

#End Region

    End Class

End Namespace
