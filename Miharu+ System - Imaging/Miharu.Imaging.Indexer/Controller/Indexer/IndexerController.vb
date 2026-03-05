Imports System.IO
Imports Miharu.Imaging.Indexer.View.Indexacion
Imports Miharu.Desktop.Library.Config
Imports Slyg.Tools
Imports Slyg.Tools.Imaging

Namespace Controller.Indexer

    Public MustInherit Class IndexerController
        Inherits GenericController
        Implements IIndexerController

#Region " Implementación IIndexerController "

        Public ReadOnly Property IndexerView As IIndexerView Implements IIndexerController.IndexerView
            Get
                Return CType(_IndexerView, IIndexerView)
            End Get
        End Property

        Public MustOverride ReadOnly Property AllowNewFile As Boolean Implements IIndexerController.AllowNewFile

        Public MustOverride ReadOnly Property AllowNewFolder As Boolean Implements IIndexerController.AllowNewFolder

        Public MustOverride ReadOnly Property ShowSecondControls As Boolean Implements IIndexerController.ShowSecondControls

        Public Property ReemplazarImagen As Boolean Implements IIndexerController.ReemplazarImagen

        Public MustOverride Function NewDocumentFile() As Boolean Implements IIndexerController.NewDocumentFile

        Public MustOverride Function NewFolder() As Boolean Implements IIndexerController.NewFolder

        Public MustOverride Function AddFolio() As Boolean Implements IIndexerController.AddFolio

        Public MustOverride Function DeleteFolio() As Boolean Implements IIndexerController.DeleteFolio

        Public MustOverride Function DesindexarFolio() As Boolean Implements IIndexerController.DesindexarFolio

        Public MustOverride Function Campos(ByVal idDocumento As Integer) As List(Of CampoCaptura) Implements IIndexerController.Campos

        Public MustOverride Function CamposLlave(ByVal idDocumento As Integer) As List(Of CampoLlaveCaptura) Implements IIndexerController.CamposLlave

        Public MustOverride Function GetValueFileData(ByVal idCampo As Integer) As String Implements IIndexerController.GetValueFileData

        Public MustOverride Function GetValueLlaveData() As DBCore.SchemaProcess.TBL_Expediente_Llave_LineaRow Implements IIndexerController.GetValueLlaveData

        Public MustOverride Function Validaciones(ByVal idDocumento As Integer) As List(Of ValidacionCaptura) Implements IIndexerController.Validaciones

        Public MustOverride Function Validar() As Boolean Implements IIndexerController.Validar

        Public MustOverride Function ValidacionListas() As Boolean Implements IIndexerController.ValidacionListas

        Public MustOverride Sub Move(ByVal nSourceFolio As Generic.Folio, ByVal nTargetFolio As Generic.Folio, ByVal nTargetIndex As Integer) Implements IIndexerController.Move

        Public MustOverride Sub AutoIndexar(ByVal nDocumento As Integer, ByVal nFolios As Integer, ByVal nModo As ModoAutoIndexarEnum) Implements IIndexerController.AutoIndexar

        Public Overridable Function Anexos() As List(Of Item) Implements IIndexerController.Anexos
            Return New List(Of Item)
        End Function

        Public Overridable Function LoadAnexo(ByVal idAnexo As Integer) As IEnumerable(Of String) Implements IIndexerController.LoadAnexo
            Return Nothing
        End Function

#End Region

#Region " Implementacion IController "

        Public Overrides Function Reproceso(ByVal nMotivo As Short) As Boolean
            Throw New NotImplementedException()
        End Function

#End Region
        
#Region " Metodos "

        Protected Overrides Sub Clear()
            Try
                If (_IndexerView IsNot Nothing) Then View.Clear()
            Catch : End Try

            For Each Item In _Folders
                Try
                    Item.Dispose()
                Catch : End Try
            Next

            _Folders.Clear()

            BorrarTemporal()
        End Sub

        Protected Sub SaveFolio()
            If (ReemplazarImagen) Then
                Dim formato = Utilities.getEnumFormat(Me.IndexerImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida)
                Dim compresion = Utilities.getEnumCompression(CType(Me.IndexerImagingGlobal.ProyectoImagingRow.id_Formato_Imagen_Salida, DesktopConfig.FormatoImagenEnum))

                CurrentFolio.FileName = Path.GetDirectoryName(CurrentFolio.FileName).TrimEnd("\"c) & "\" & Path.GetFileNameWithoutExtension(CurrentFolio.FileName) & "(1)" & Path.GetExtension(CurrentFolio.FileName)
                ImageManager.Save(Me.View.Image, CurrentFolio.FileName, "", formato, compresion, False, _TempPath)

                Dim thumbnailData = ImageManager.GetThumbnailData(CurrentFolio.FileName, 1, 1, MaxThumbnailWidth, MaxThumbnailHeight)
                CurrentFolio.ThumbnailImage = New FreeImageAPI.FreeImageBitmap(New MemoryStream(thumbnailData(0)))
                CurrentFolio.Updated = True
            End If
        End Sub

#End Region

#Region " Funciones "

        'Public Function ThumbnailCallback() As Boolean
        '    Return False
        'End Function

#End Region

    End Class

End Namespace
