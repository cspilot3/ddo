Public Class Diagrama

#Region " Declaraciones "

    Public Const _EspacioX As Integer = 20
    Public Const _EspacioY As Integer = 30
    Public Const _EspacioAX As Integer = 50

    Public Enum EnumRelacion
        SUBORDINACION = 1
        ASISTENCIAL = 2
    End Enum

    Private _Items As List(Of DiagramaNodo)
    Private _Nodos As Dictionary(Of Integer, DiagramaNodo)

#End Region

#Region " Propiedades "

    Public ReadOnly Property EspacioX() As Integer
        Get
            Return _EspacioX
        End Get
    End Property
    Public ReadOnly Property EspacioY() As Integer
        Get
            Return _EspacioY
        End Get
    End Property
    Public ReadOnly Property EspacioAX() As Integer
        Get
            Return _EspacioAX
        End Get
    End Property

#End Region

#Region " Metodos "

    Public Sub New()
        _Items = New List(Of DiagramaNodo)
        _Nodos = New Dictionary(Of Integer, DiagramaNodo)
    End Sub

    Public Sub ConfigPosiciones(ByVal Xini As Integer, ByVal Yini As Integer)
        Dim AnchoTotal As Integer
        Dim Ancho As Integer
        Dim Corrimiento As Integer

        AnchoTotal = getAncho()

        Corrimiento = CInt(Xini - (AnchoTotal / 2))

        For Each s As DiagramaNodo In _Items
            Ancho = s.getAncho()

            s.ConfigPosiciones(CInt(Corrimiento + (Ancho / 2)), Yini)
            Corrimiento += Ancho
        Next
    End Sub

    Public Sub Clear()
        _Items.Clear()
        _Nodos.Clear()
    End Sub
    Public Sub Add(ByVal nKey As Integer, ByVal nItem As DiagramaNodo)
        nItem.Relacion = EnumRelacion.SUBORDINACION
        nItem._Id = nKey
        _Items.Add(nItem)
        _Nodos.Add(nItem.Id, nItem)
    End Sub
    Public Sub Add(ByVal nKey As Integer, ByVal nItem As DiagramaNodo, ByVal nKeyPadre As Integer, ByVal nTipo As EnumRelacion)
        nItem._Id = nKey
        _Nodos.Item(nKeyPadre).AddItem(nItem, nTipo)
        _Nodos.Add(nItem.Id, nItem)
    End Sub
    Public Sub Remove(ByVal nKey As Integer)
        Dim ItemRemove As DiagramaNodo = _Nodos.Item(nKey)

        'ItemRemove.Padre.RemoveItem(ItemRemove)
        ItemRemove.Delete()
        _Nodos.Remove(nKey)

    End Sub

#End Region

#Region " Funciones "

    Public Function NewNodo() As DiagramaNodo
        Return New DiagramaNodo(Me)
    End Function
    Public Function getNodo(ByVal nKey As Integer) As DiagramaNodo
        Return _Nodos.Item(nKey)
    End Function

    Public Function getAncho() As Integer
        Dim Ancho As Integer

        Ancho = 0
        For Each s As DiagramaNodo In _Items
            Ancho += s.getAncho()
        Next

        Return Ancho
    End Function
    Public Function getLista() As List(Of DiagramaNodo)
        Dim Lista As New List(Of DiagramaNodo)

        For Each Nodo In _Items
            Nodo.getLista(Lista)
        Next

        Return Lista
    End Function

    Public Function getBloques() As List(Of Bloque)
        Dim Lista As New List(Of Bloque)

        For Each Nodo In _Items
            Nodo.getBloques(Lista)
        Next

        Return Lista
    End Function
    Public Function getLineas() As List(Of Linea)
        Dim Lista As New List(Of Linea)

        For Each Nodo In _Items
            Nodo.getLineas(Lista)
        Next

        Return Lista
    End Function

#End Region

End Class
