Imports Miharu.Imaging.Indexer.Controller

Namespace Generic

    Public Class Folder
        Implements IList(Of DocumentFile)

#Region " Enumeraciones "

        Public Enum FolderModoEnum
            Normal
            Anexo
        End Enum

#End Region

#Region " Declaraciones "

        Private _Items As New List(Of DocumentFile)
        Private _Parent As IController
        Private _Panel As New Windows.Forms.FlowLayoutPanel
        Private _Label As New Windows.Forms.Label
        Private _Modo As FolderModoEnum

#End Region

#Region " Propiedades "

        Public ReadOnly Property Parent() As IController
            Get
                Return _Parent
            End Get
        End Property

        Default Public ReadOnly Property DocumentFile(ByVal index As Integer) As DocumentFile
            Get
                If (_Items.Count = 0) Then
                    Return Nothing
                Else
                    Return _Items.Item(index)
                End If
            End Get
        End Property

        Public ReadOnly Property Folios() As Integer
            Get
                Dim Total As Integer = 0

                For Each ItemDocumentFile As DocumentFile In _Items
                    Total += ItemDocumentFile.Count
                Next

                Return Total
            End Get
        End Property

        Public ReadOnly Property Panel() As Windows.Forms.Panel
            Get
                Return _Panel
            End Get
        End Property

        Public ReadOnly Property Index() As Integer
            Get
                Return _Parent.Folders.IndexOf(Me)
            End Get
        End Property

        Public Property LLaves As List(Of Campo)

        Public Property FolderRow As DBCore.SchemaProcess.TBL_FolderRow

        Public Property Esquema As Short

        Public Property IdAnexo As Integer

        Public Property idExpediente As Long

        Public Property Modo As FolderModoEnum
            Get
                Return _Modo
            End Get
            Set(value As FolderModoEnum)
                _Modo = value

                _Panel.BackColor = CType(IIf(_Modo = FolderModoEnum.Normal, Drawing.Color.Khaki, Drawing.Color.Purple), Drawing.Color)
            End Set
        End Property

#End Region

#Region " Constructores "

        Public Sub New(ByRef nParent As IController)
            _Parent = nParent

            _Panel.AutoSize = True
            _Panel.AutoScroll = False
            _Panel.BorderStyle = Windows.Forms.BorderStyle.FixedSingle
            _Panel.AutoSizeMode = Windows.Forms.AutoSizeMode.GrowAndShrink
            _Panel.Margin = New Windows.Forms.Padding(2, 2, 3, 2)

            _Label.BackColor = Drawing.Color.SteelBlue
            _Label.BorderStyle = Windows.Forms.BorderStyle.FixedSingle
            _Label.Font = New Drawing.Font("Microsoft Sans Serif", 8.25!, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CType(0, Byte))
            _Label.ForeColor = Drawing.Color.White
            _Label.Image = My.Resources.Resources.folder_image
            _Label.ImageAlign = Drawing.ContentAlignment.TopCenter
            _Label.Margin = New Windows.Forms.Padding(4, 4, 2, 4)
            _Label.Size = New Drawing.Size(25, 63)
            _Label.Text = CStr(Parent.Folders.Count + 1)
            _Label.TextAlign = Drawing.ContentAlignment.MiddleCenter

            _Panel.Controls.Add(_Label)

            Me.Modo = FolderModoEnum.Normal

            LLaves = New List(Of Campo)
        End Sub

#End Region

#Region " Metodos "

        Sub Dispose()
            _Panel.Dispose()
            _Label.Dispose()

            For Each Documento In _Items
                Documento.Dispose()
            Next

            _Items.Clear()
        End Sub

#End Region

#Region " Funciones "

        Public Function NewDocumentFile() As DocumentFile
            Return New DocumentFile(Me)
        End Function

        Public Function getCampo(ByVal nIdDocumento As Integer, ByVal nIdCampo As Short) As Campo
            For Each DocumentItem In _Items
                If (DocumentItem.TipoDocumento = nIdDocumento) Then
                    Return DocumentItem.getCampo(nIdCampo)
                End If
            Next

            Return Nothing
        End Function

#End Region

#Region " Implements IList(Of DocumentFile) "

        Private Property Item(ByVal index As Integer) As DocumentFile Implements IList(Of DocumentFile).Item
            Get
                Return _Items.Item(index)
            End Get
            Set(ByVal value As DocumentFile)
                _Items.Item(index) = value
            End Set
        End Property

        Public ReadOnly Property Count() As Integer Implements ICollection(Of DocumentFile).Count
            Get
                Return _Items.Count
            End Get
        End Property

        Public ReadOnly Property IsReadOnly() As Boolean Implements ICollection(Of DocumentFile).IsReadOnly
            Get
                Return False
            End Get
        End Property

        Public Sub Insert(ByVal index As Integer, ByVal item As DocumentFile) Implements IList(Of DocumentFile).Insert
            If item.Parent.Equals(Me) Then
                _Items.Insert(index, item)
            Else
                Throw New Exception("El Documento no pertenece a este folder")
            End If
        End Sub

        Public Sub Add(ByVal item As DocumentFile) Implements ICollection(Of DocumentFile).Add
            If item.Parent.Equals(Me) Then
                _Items.Add(item)
                _Panel.Controls.Add(item.Panel)
            Else
                Throw New Exception("El DocumentFile no pertenece a este proyecto")
            End If
        End Sub

        Public Sub Clear() Implements ICollection(Of DocumentFile).Clear
            _Items.Clear()
            _Panel.Controls.Clear()
        End Sub

        Public Sub CopyTo(ByVal array() As DocumentFile, ByVal arrayIndex As Integer) Implements ICollection(Of DocumentFile).CopyTo
            _Items.CopyTo(array, arrayIndex)
        End Sub

        Private Sub RemoveAt(ByVal index As Integer) Implements IList(Of DocumentFile).RemoveAt
            _Items.RemoveAt(index)
        End Sub

        Public Function Contains(ByVal item As DocumentFile) As Boolean Implements ICollection(Of DocumentFile).Contains
            Return _Items.Contains(item)
        End Function

        Public Function Remove(ByVal item As DocumentFile) As Boolean Implements ICollection(Of DocumentFile).Remove
            _Panel.Controls.Remove(item.Panel)
            Return _Items.Remove(item)
        End Function

        Public Function IndexOf(ByVal item As DocumentFile) As Integer Implements IList(Of DocumentFile).IndexOf
            Return _Items.IndexOf(item)
        End Function

        Public Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
            Return _Items.GetEnumerator
        End Function

        Public Function GetEnumeratorDocumentFile() As IEnumerator(Of DocumentFile) Implements IEnumerable(Of DocumentFile).GetEnumerator
            Return _Items.GetEnumerator
        End Function

#End Region

    End Class

End Namespace