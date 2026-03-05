Imports System.Drawing
Imports Slyg.Tools.Imaging

Namespace Generic

    Public Class Folio

#Region " Declaraciones "

        Private _Parent As DocumentFile
        Private _GlobalIndex As Integer = -1
        Private _ThumbnailImage As New Windows.Forms.PictureBox
        Private Const PictureWidth As Integer = 40
        Private Const PictureHeight As Integer = 55

#End Region

#Region " Constructores "

        Public Sub New(ByRef nParent As DocumentFile, ByVal nAcceptClick As Boolean)
            _Parent = nParent

            _ThumbnailImage.BackColor = Color.White
            _ThumbnailImage.BorderStyle = Windows.Forms.BorderStyle.FixedSingle

            _ThumbnailImage.Size = New Size(PictureWidth, PictureHeight)
            _ThumbnailImage.TabStop = False
            _ThumbnailImage.SizeMode = Windows.Forms.PictureBoxSizeMode.Zoom ' StretchImage
            _ThumbnailImage.Margin = New Windows.Forms.Padding(3)
            _ThumbnailImage.Cursor = Windows.Forms.Cursors.Hand

            AddHandler _ThumbnailImage.MouseEnter, AddressOf Me.Parent.Parent.Parent.View.Thumbnail_MouseEnter
            AddHandler _ThumbnailImage.MouseLeave, AddressOf Me.Parent.Parent.Parent.View.Thumbnail_MouseLeave

            If (nAcceptClick) Then
                AddHandler _ThumbnailImage.Click, AddressOf Me.Parent.Parent.Parent.View.Thumbnail_Click
            End If

            If (Me.Parent.Parent.Parent.View.ThumbnailHelper IsNot Nothing) Then
                Me.Parent.Parent.Parent.View.ThumbnailHelper.AddPictureHandlers(_ThumbnailImage)
            End If

            _ThumbnailImage.Tag = Me

            Me.Lineas = New List(Of Single)
        End Sub

#End Region

#Region " Propiedades "

        Public ReadOnly Property Parent() As DocumentFile
            Get
                Return _Parent
            End Get
        End Property

        Public Property FileName() As String

        Public Property ThumbnailImage() As FreeImageAPI.FreeImageBitmap
            Get
                If (_ThumbnailImage.Image Is Nothing) Then
                    Return Nothing
                Else
                    Return CType(_ThumbnailImage.Image, FreeImageAPI.FreeImageBitmap)
                End If
            End Get
            Set(ByVal value As FreeImageAPI.FreeImageBitmap)
                Try
                    _ThumbnailImage.Image = CType(ImageManager.GetThumbnailBitmap(value, 1, Me.Parent.Parent.Parent.View.ThumbnailWidth, Me.Parent.Parent.Parent.View.ThumbnailHeight), Bitmap)

                    If (value.Width / PictureWidth) > (value.Height / PictureHeight) Then
                        _ThumbnailImage.Size = New Size(PictureWidth, CInt(value.Height / (value.Width / PictureWidth)))
                    Else
                        _ThumbnailImage.Size = New Size(CInt(value.Width / (value.Height / PictureHeight)), PictureHeight)
                    End If
                Catch : End Try
            End Set
        End Property

        Public ReadOnly Property Picture() As Windows.Forms.PictureBox
            Get
                Return _ThumbnailImage
            End Get
        End Property

        Public ReadOnly Property GlobalIndex() As Integer
            Get
                Return _GlobalIndex
            End Get
        End Property

        Public ReadOnly Property Index() As Integer
            Get
                Return _Parent.IndexOf(Me)
            End Get
        End Property

        Public Property Updated() As Boolean

        Public Property Cargue() As Integer
        Public Property Cargue_Paquete() As Short
        Public Property Cargue_Item() As Integer
        Public Property Id_Item_Folio() As Short
        Public Property NombreImagen() As String

        Public Property Lineas As List(Of Single)

#End Region

#Region " Metodos "

        Public Sub Move(ByRef nNewParent As DocumentFile)
            _Parent.Remove(Me)
            _Parent = nNewParent
            nNewParent.Add(Me)
        End Sub

        Public Sub Move(ByRef nNewParent As DocumentFile, ByVal nIndex As Integer)
            _Parent.Remove(Me)
            _Parent = nNewParent
            nNewParent.Insert(nIndex, Me)
        End Sub

        Public Sub setIndex(ByVal nGlobalIndex As Integer)
            _GlobalIndex = nGlobalIndex
        End Sub

        Sub Dispose()
            _ThumbnailImage.Dispose()
        End Sub

        Sub Unselect()
            _ThumbnailImage.BorderStyle = Windows.Forms.BorderStyle.FixedSingle
        End Sub

        Sub [Select]()
            _ThumbnailImage.BorderStyle = Windows.Forms.BorderStyle.Fixed3D
        End Sub

#End Region

    End Class

End Namespace
