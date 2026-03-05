Public Class NameObjectCollection
    Inherits NameObjectCollectionBase

    Default Public Property Item(ByVal name As String) As Object
        Get
            Return MyBase.BaseGet(name)
        End Get
        Set(ByVal value As Object)
            Try
                MyBase.BaseSet(name, value)
            Catch
                MyBase.BaseAdd(name, value)
            End Try
        End Set
    End Property
End Class