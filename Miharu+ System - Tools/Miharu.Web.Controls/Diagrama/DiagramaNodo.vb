Imports System.Web.UI.WebControls

Public Class DiagramaNodo

#Region " Declaraciones "

    Private _Diagrama As Diagrama

    Friend _Id As Integer

    Private _Etiqueta As String
    Private _Codigo As String

    Private _Width As Integer = 160
    Private _Height As Integer = 80
    Private _X As Integer
    Private _Y As Integer
    Private _Relacion As Diagrama.EnumRelacion = Diagrama.EnumRelacion.SUBORDINACION
    Private _Padre As DiagramaNodo = Nothing

    Public ItemsSubordinados As List(Of DiagramaNodo)
    Public ItemsAsistenciales As List(Of DiagramaNodo)

#End Region

#Region " Propiedades "

    Public ReadOnly Property Id() As Integer
        Get
            Return _Id
        End Get
    End Property

    Public Property Etiqueta() As String
        Get
            Return _Etiqueta
        End Get
        Set(ByVal value As String)
            _Etiqueta = value
        End Set
    End Property
    Public Property Codigo() As String
        Get
            Return _Codigo
        End Get
        Set(ByVal value As String)
            _Codigo = value
        End Set
    End Property

    Public Property Width() As Integer
        Get
            Return _Width
        End Get
        Set(ByVal value As Integer)
            _Width = value
        End Set
    End Property
    Public Property Height() As Integer
        Get
            Return _Height
        End Get
        Set(ByVal value As Integer)
            _Height = value
        End Set
    End Property
    Public Property X() As Integer
        Get
            Return _X
        End Get
        Set(ByVal value As Integer)
            _X = value
        End Set
    End Property
    Public Property Y() As Integer
        Get
            Return _Y
        End Get
        Set(ByVal value As Integer)
            _Y = value
        End Set
    End Property
    Public Property Relacion() As Diagrama.EnumRelacion
        Get
            Return _Relacion
        End Get
        Set(ByVal value As Diagrama.EnumRelacion)
            _Relacion = value
        End Set
    End Property
    Public ReadOnly Property Padre() As DiagramaNodo
        Get
            Return _Padre
        End Get
    End Property

#End Region

#Region " Metodos "

    Friend Sub New(ByVal nDiagrama As Diagrama)
        _Diagrama = nDiagrama

        ItemsSubordinados = New List(Of DiagramaNodo)
        ItemsAsistenciales = New List(Of DiagramaNodo)

    End Sub

    Public Sub AddItem(ByVal item As DiagramaNodo, ByVal Tipo As Diagrama.EnumRelacion) ', ByVal nId As String)
        item._Padre = Me
        item.Relacion = Tipo

        Select Case Tipo
            Case Diagrama.EnumRelacion.ASISTENCIAL
                Me.ItemsAsistenciales.Add(item)

            Case Else ' Diagrama.EnumRelacion.SUBORDINACION                
                Me.ItemsSubordinados.Add(item)

        End Select
    End Sub
    Public Sub RemoveItem(ByVal item As DiagramaNodo)
        Select Case item.Relacion
            Case Diagrama.EnumRelacion.ASISTENCIAL
                Me.ItemsAsistenciales.Remove(item)

            Case Else ' Diagrama.EnumRelacion.SUBORDINACION
                Me.ItemsSubordinados.Remove(item)

        End Select
    End Sub
    Public Sub Delete()
        Select Case Me.Relacion
            Case Diagrama.EnumRelacion.ASISTENCIAL
                Padre.ItemsAsistenciales.Remove(Me)

            Case Else ' Diagrama.EnumRelacion.SUBORDINACION
                Padre.ItemsSubordinados.Remove(Me)

        End Select

        _Padre = Nothing
    End Sub
    Public Sub ConfigPosiciones(ByVal Xini As Integer, ByVal Yini As Integer)
        If Me.Relacion = Diagrama.EnumRelacion.ASISTENCIAL Then
            Me.X = Xini - _Diagrama.EspacioAX - Me.Width
            Me.Y = CInt(Yini - (Me.Width / 2))
        Else
            Dim AnchoTotal As Integer
            Dim Ancho As Integer
            Dim Corrimiento As Integer
            Dim PuntoInferior As Integer

            Me.X = CInt(Xini - (Me.Width / 2))
            Me.Y = Yini + _Diagrama.EspacioY

            PuntoInferior = Me.Y + Me._Height + _Diagrama.EspacioY

            For Each s As DiagramaNodo In ItemsAsistenciales
                PuntoInferior = CInt(PuntoInferior + _Diagrama.EspacioY + (s.Height / 2))
                s.ConfigPosiciones(Xini, PuntoInferior)
                PuntoInferior = CInt(PuntoInferior + (s.Height / 2))
            Next

            If ItemsSubordinados.Count = 1 Then
                ItemsSubordinados.Item(0).ConfigPosiciones(Xini, PuntoInferior)
            Else
                AnchoTotal = getAncho()
                Corrimiento = CInt(Xini - (AnchoTotal / 2))

                For Each s As DiagramaNodo In ItemsSubordinados
                    Ancho = s.getAncho()

                    s.ConfigPosiciones(CInt(Corrimiento + (Ancho / 2)), PuntoInferior)
                    Corrimiento += Ancho
                Next
            End If
        End If
    End Sub
    Public Sub getLista(ByVal Lista As List(Of DiagramaNodo))
        Lista.Add(Me)

        For Each Nodo In ItemsAsistenciales
            Nodo.getLista(Lista)
        Next

        For Each Nodo In ItemsSubordinados
            Nodo.getLista(Lista)
        Next
    End Sub
    Public Sub getBloques(ByVal Lista As List(Of Bloque))
        Lista.Add(Me.getBloque)

        For Each Nodo In ItemsAsistenciales
            Nodo.getBloques(Lista)
        Next

        For Each Nodo In ItemsSubordinados
            Nodo.getBloques(Lista)
        Next
    End Sub
    Public Sub getLineas(ByVal Lista As List(Of Linea))
        Dim Line As Linea

        If Not Me.Padre Is Nothing Then
            Line = New Linea

            If _Relacion = Diagrama.EnumRelacion.ASISTENCIAL Then
                Line.X1 = Me.X + Me._Width + 2
                Line.X2 = Line.X1 + _Diagrama.EspacioAX - 3
                Line.Y1 = CInt(Me.Y + (Me.Height / 2))
                Line.Y2 = Line.Y1
            Else
                Line.X1 = CInt(Me.X + (Me.Width / 2))
                Line.X2 = Line.X1
                Line.Y1 = Me.Y - _Diagrama.EspacioY
                Line.Y2 = Me.Y - 3
            End If

            Lista.Add(Line)
        End If


        If ItemsSubordinados.Count = 0 Then
            If ItemsAsistenciales.Count > 0 Then
                Line = New Linea

                Line.X1 = CInt(Me.X + (Me.Width / 2))
                Line.X2 = Line.X1
                Line.Y1 = Me.Y + Me.Height + 2
                Line.Y2 = CInt(ItemsAsistenciales(ItemsAsistenciales.Count - 1).Y + (ItemsAsistenciales(ItemsAsistenciales.Count - 1).Height / 2))

                Lista.Add(Line)
            End If
        ElseIf ItemsSubordinados.Count = 1 Then
            Line = New Linea

            Line.X1 = CInt(Me.X + (Me.Width / 2))
            Line.X2 = Line.X1
            Line.Y1 = Me.Y + Me.Height + 2
            Line.Y2 = ItemsSubordinados(0).Y - _Diagrama.EspacioY

            Lista.Add(Line)
        Else
            Line = New Linea

            Line.X1 = CInt(Me.X + (Me.Width / 2))
            Line.X2 = Line.X1
            Line.Y1 = Me.Y + Me.Height + 2
            Line.Y2 = ItemsSubordinados(0).Y - _Diagrama.EspacioY

            Lista.Add(Line)

            Line = New Linea

            Line.X1 = CInt(ItemsSubordinados(0).X + (ItemsSubordinados(0)._Width / 2))
            Line.X2 = CInt(ItemsSubordinados(ItemsSubordinados.Count - 1).X + (ItemsSubordinados(ItemsSubordinados.Count - 1)._Width / 2))
            Line.Y1 = ItemsSubordinados(0).Y - _Diagrama.EspacioY
            Line.Y2 = Line.Y1

            Lista.Add(Line)
        End If

        For Each Nodo In ItemsAsistenciales
            Nodo.getLineas(Lista)
        Next

        For Each Nodo In ItemsSubordinados
            Nodo.getLineas(Lista)
        Next
    End Sub

#End Region

#Region " Funciones "

    Public Function getAncho() As Integer
        Dim Ancho As Integer
        Dim AnchoTemp As Integer

        Ancho = Me.Width + (2 * _Diagrama.EspacioX)

        AnchoTemp = 0
        For Each s As DiagramaNodo In ItemsAsistenciales
            If AnchoTemp < s.Width Then AnchoTemp = s.Width
        Next

        If ItemsAsistenciales.Count > 0 Then AnchoTemp = (AnchoTemp * 2) + (_Diagrama.EspacioAX * 2)
        If Ancho < AnchoTemp Then Ancho = AnchoTemp

        AnchoTemp = 0
        For Each s As DiagramaNodo In ItemsSubordinados
            AnchoTemp += s.getAncho()
        Next

        If Ancho < AnchoTemp Then Ancho = AnchoTemp

        Return Ancho
    End Function
    Private Function getBloque() As Bloque
        Dim MyBloque As New Bloque

        MyBloque.X = _X
        MyBloque.Y = _Y
        MyBloque.Width = New Unit(_Width, UnitType.Pixel)
        MyBloque.Height = New Unit(_Height, UnitType.Pixel)
        MyBloque.Etiqueta = _Etiqueta

        If _Padre Is Nothing Then
            MyBloque.BackColor = Drawing.Color.Salmon
        ElseIf _Relacion = Diagrama.EnumRelacion.ASISTENCIAL Then
            MyBloque.BackColor = Drawing.Color.LightGray
        Else
            MyBloque.BackColor = Drawing.Color.Honeydew
        End If

        'MyBloque.Identificador = Me._Id
        MyBloque.ToolTip = CStr(Me._Id)
        MyBloque.Codigo = Me._Codigo

        Return MyBloque
    End Function

#End Region

End Class
