Namespace Generic

    Public Class DocumentFile
        Implements IList(Of Folio)

#Region " Declaraciones "

        Private _Items As New List(Of Folio)
        Private _Parent As Folder
        Private _TipoDocumento As Integer
        Private _NombreTipoDocumento As String

        Private _Panel As New Windows.Forms.FlowLayoutPanel

        Public Campos As New List(Of Campo)
        Public Validaciones As New List(Of Validacion)

#End Region

#Region " Propiedades "

        Public ReadOnly Property Parent() As Folder
            Get
                Return _Parent
            End Get
        End Property

        Default Public ReadOnly Property Folio(ByVal index As Integer) As Folio
            Get
                Return _Items.Item(index)
            End Get
        End Property

        Public ReadOnly Property Folios() As Integer
            Get
                Return _Items.Count
            End Get
        End Property

        Public Property TipoDocumento() As Integer
            Get
                Return _TipoDocumento
            End Get
            Set(ByVal value As Integer)
                _TipoDocumento = value
            End Set
        End Property

        Public Property NombreTipoDocumento() As String
            Get
                Return _NombreTipoDocumento
            End Get
            Set(ByVal value As String)
                _NombreTipoDocumento = value
            End Set
        End Property

        Public ReadOnly Property Panel() As Windows.Forms.Panel
            Get
                Return _Panel
            End Get
        End Property

        Public Property OutputFileName() As String

        Public ReadOnly Property Index() As Integer
            Get
                Return _Parent.IndexOf(Me)
            End Get
        End Property

#End Region

#Region " Metodos "

        Public Sub New(ByRef nParent As Folder)
            _Parent = nParent

            _Panel.AutoSize = True
            _Panel.AutoScroll = False
            _Panel.BackColor = Drawing.SystemColors.Control
            _Panel.BorderStyle = Windows.Forms.BorderStyle.FixedSingle
            _Panel.AutoSizeMode = Windows.Forms.AutoSizeMode.GrowAndShrink
            _Panel.Margin = New Windows.Forms.Padding(4)
            _Panel.Dock = Windows.Forms.DockStyle.Left
        End Sub

        Sub Dispose()
            _Panel.Dispose()

            For Each Pagina In _Items
                Pagina.Dispose()
            Next

            Campos.Clear()
            _Items.Clear()
        End Sub

#End Region

#Region " Funciones "

        Public Function NewFolio(ByVal nAcceptClick As Boolean) As Folio
            Dim FolioNew = New Folio(Me, nAcceptClick)

            Return FolioNew
        End Function

        Function getCampo(ByVal nIdCampo As Short) As Campo
            For Each CampoItem In Campos
                If (CampoItem.id = nIdCampo) Then
                    Return CampoItem
                End If
            Next

            Return Nothing
        End Function

#End Region

#Region " Implements IList(Of Folio) "

        Private Property Item(ByVal index As Integer) As Folio Implements IList(Of Folio).Item
            Get
                Return _Items.Item(index)
            End Get
            Set(ByVal value As Folio)
                _Items.Item(index) = value
            End Set
        End Property

        Public ReadOnly Property Count() As Integer Implements ICollection(Of Folio).Count
            Get
                Return _Items.Count
            End Get
        End Property

        Public ReadOnly Property IsReadOnly() As Boolean Implements ICollection(Of Folio).IsReadOnly
            Get
                Return False
            End Get
        End Property

        Public Sub Insert(ByVal index As Integer, ByVal item As Folio) Implements IList(Of Folio).Insert
            If item.Parent.Equals(Me) Then
                _Items.Insert(index, item)
                _Panel.Controls.Add(item.Picture)
                _Panel.Controls.SetChildIndex(item.Picture, index)
            Else
                Throw New Exception("El folio no pertenece a este Documento")
            End If
        End Sub

        Public Sub Add(ByVal item As Folio) Implements ICollection(Of Folio).Add
            If item.Parent.Equals(Me) Then
                _Items.Add(item)
                item.setIndex(_Parent.Parent.Folios - 1)
                _Panel.Controls.Add(item.Picture)
            Else
                Throw New Exception("El folio no pertenece a este Documento")
            End If
        End Sub

        Public Sub Clear() Implements ICollection(Of Folio).Clear
            _Items.Clear()
            _Panel.Controls.Clear()
        End Sub

        Public Sub CopyTo(ByVal array() As Folio, ByVal arrayIndex As Integer) Implements ICollection(Of Folio).CopyTo
            _Items.CopyTo(array, arrayIndex)
        End Sub

        Private Sub RemoveAt(ByVal index As Integer) Implements IList(Of Folio).RemoveAt
            _Items.RemoveAt(index)
        End Sub

        Public Function Contains(ByVal item As Folio) As Boolean Implements ICollection(Of Folio).Contains
            Return _Items.Contains(item)
        End Function

        Public Function Remove(ByVal item As Folio) As Boolean Implements ICollection(Of Folio).Remove
            If item.Picture IsNot Nothing Then _Panel.Controls.Remove(item.Picture)
            Return _Items.Remove(item)
        End Function

        Public Function IndexOf(ByVal item As Folio) As Integer Implements IList(Of Folio).IndexOf
            Return _Items.IndexOf(item)
        End Function

        Public Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
            Return _Items.GetEnumerator
        End Function

        Public Function GetEnumeratorFolio() As IEnumerator(Of Folio) Implements IEnumerable(Of Folio).GetEnumerator
            Return _Items.GetEnumerator
        End Function

#End Region

    End Class

End Namespace