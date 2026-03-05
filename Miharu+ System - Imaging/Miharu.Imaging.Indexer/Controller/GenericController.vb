Imports System.IO
Imports Miharu.Imaging.Indexer.View.Indexacion
Imports Miharu.Desktop.Library.Config
Imports Slyg.Tools
Imports DMB = Miharu.Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl
Imports Slyg.Tools.Imaging
Imports System.Windows.Forms
Imports Miharu.FileProvider.Library

Namespace Controller

    Public MustInherit Class GenericController
        Inherits MarshalByRefObject
        Implements IController

#Region " Declaraciones "

        Private _eventManager As New Events.EventManager

        Public Const MaxThumbnailWidth As Integer = 60
        Public Const MaxThumbnailHeight As Integer = 80

        Public _IndexerView As View.IView

        Protected _CurrentFolderIndex As Integer
        Protected _CurrentDocumentFileIndex As Integer
        Protected _CurrentFolioIndex As Integer
        Protected _CurrentImageIndex As Integer

        Protected _Folders As New List(Of Generic.Folder)

        Protected _TempPath As String

        Protected EsquemaDataTable As DBCore.SchemaConfig.TBL_EsquemaDataTable
        Protected DocumentoDataTable As DBImaging.SchemaConfig.CTA_DocumentoDataTable
        Protected DocumentoIndexacionDataTable As DBImaging.SchemaConfig.CTA_Documento_IndexacionDataTable

        Protected _Campos As New List(Of CampoCaptura)
        Protected _CamposLlave As New List(Of CampoLlaveCaptura)
        Protected _GetValueFileData As String
        Protected _Validaciones As New List(Of ValidacionCaptura)
        Protected _ValidacionListas As New ListValidationControl

        Public _Usa_OCR_Captura As Boolean

        Protected _ImageCount As Integer

        Protected _Ciclo As Integer = 0

        Protected Delegate Sub InicializarFolioDelegate(ByRef nFileProvider As FileProviderManager, ByVal nFolio As Integer)
        Protected Delegate Sub ShowMessageDelegate(ByVal nTitle As String, ByVal nEx As Exception)

#End Region

#Region " Implementación IController "

        Protected Property IndexerSesion As Security.Library.Session.Sesion Implements IController.IndexerSesion

        Protected Property IndexerDesktopGlobal As DesktopGlobal Implements IController.IndexerDesktopGlobal

        Protected Property IndexerImagingGlobal As ImagingGlobal Implements IController.IndexerImagingGlobal

        Public ReadOnly Property EventManager As Events.EventManager Implements IController.EventManager
            Get
                Return Me._eventManager
            End Get
        End Property

        Public MustOverride Sub Inicializar(ByVal nTempPath As String, ByVal nIndexerSesion As Security.Library.Session.Sesion, ByVal nIndexerDesktopGlobal As DesktopGlobal, ByVal nIndexerImagingGlobal As ImagingGlobal) Implements IController.Inicializar

        Public ReadOnly Property View As View.IView Implements IController.View
            Get
                Return Me._IndexerView
            End Get
        End Property

        Public MustOverride ReadOnly Property Cargado As Boolean Implements IController.Cargado

        Public ReadOnly Property IsOCRUsed As Boolean Implements IController.IsOCRUsed
            Get
                Return Me.IndexerImagingGlobal.ProyectoImagingRow.Usa_OCR_Captura
            End Get
        End Property

        Public ReadOnly Property CurrentFolderIndex As Integer Implements IController.CurrentFolderIndex
            Get
                Return _CurrentFolderIndex
            End Get
        End Property

        Public ReadOnly Property CurrentFolder As Generic.Folder Implements IController.CurrentFolder
            Get
                If (_Folders.Count = 0 Or _CurrentFolderIndex = -1) Then
                    Return Nothing
                ElseIf (_CurrentFolderIndex >= _Folders.Count) Then
                    Return Nothing
                Else
                    Return _Folders(_CurrentFolderIndex)
                End If
            End Get
        End Property

        Public ReadOnly Property CurrentDocumentFileIndex As Integer Implements IController.CurrentDocumentFileIndex
            Get
                Return _CurrentDocumentFileIndex
            End Get
        End Property

        Public ReadOnly Property CurrentDocumentFile As Generic.DocumentFile Implements IController.CurrentDocumentFile
            Get
                If (CurrentFolder Is Nothing Or _CurrentDocumentFileIndex = -1) Then
                    Return Nothing
                ElseIf (_CurrentDocumentFileIndex >= CurrentFolder.Count) Then
                    Return Nothing
                Else
                    Return CurrentFolder.DocumentFile(_CurrentDocumentFileIndex)
                End If
            End Get
        End Property

        Public ReadOnly Property CurrentFolioIndex As Integer Implements IController.CurrentFolioIndex
            Get
                Return _CurrentFolioIndex
            End Get
        End Property

        Public ReadOnly Property CurrentFolio As Generic.Folio Implements IController.CurrentFolio
            Get
                If (CurrentDocumentFile Is Nothing Or _CurrentFolioIndex = -1) Then
                    Return Nothing
                ElseIf (_CurrentFolioIndex >= CurrentDocumentFile.Count) Then
                    Return Nothing
                Else
                    Return CurrentDocumentFile.Folio(_CurrentFolioIndex)
                End If
            End Get
        End Property

        Public ReadOnly Property CurrentImageIndex As Integer Implements IController.CurrentImageIndex
            Get
                Return _CurrentImageIndex
            End Get
        End Property

        Public ReadOnly Property CurrentImage As FreeImageAPI.FreeImageBitmap Implements IController.CurrentImage
            Get
                If CurrentFolio.FileName.ToLower.EndsWith(".pdf") Then
                    Return ImageManager.GetFolioBitmap(CurrentFolio.FileName, 1)
                Else
                    Return New FreeImageAPI.FreeImageBitmap(CurrentFolio.FileName)
                End If

            End Get
        End Property

        Public ReadOnly Property Folders As List(Of Generic.Folder) Implements IController.Folders
            Get
                Return _Folders
            End Get
        End Property

        Public ReadOnly Property ImageCount As Integer Implements IController.ImageCount
            Get
                Return _ImageCount
            End Get
        End Property

        Public ReadOnly Property PreguntarSalida As Boolean Implements IController.PreguntarSalida
            Get
                Return (CurrentImageIndex > 0)
            End Get
        End Property

        Public ReadOnly Property Folios() As Integer Implements IController.Folios
            Get
                Dim total As Integer = 0

                For Each itemFolder As Generic.Folder In _Folders
                    total += itemFolder.Folios
                Next

                Return total
            End Get
        End Property

        Public ReadOnly Property Ciclo As Integer Implements IController.Ciclo
            Get
                Return _Ciclo
            End Get
        End Property

        Public MustOverride Function Save() As Boolean Implements IController.Save

        Public MustOverride Function NextFolio(ByRef nUpdateInfo As Boolean) As Boolean Implements IController.NextFolio

        Public MustOverride Function PreviousFolio(ByRef nUpdateInfo As Boolean) As Boolean Implements IController.PreviousFolio

        Public MustOverride Sub Unlock() Implements IController.Unlock

        Public Shadows Sub Dispose() Implements IController.Dispose
            Clear()

            Try
                If (Me.View IsNot Nothing) Then CType(Me.View, Form).Dispose()
            Catch : End Try
        End Sub

        Public MustOverride Function NextIndexingElement(ByVal ot As Integer, ByVal nEstado As DBCore.EstadoEnum, ByVal nIdDocumento As SlygNullable(Of Integer)) As Boolean Implements IController.NextIndexingElement

        Public MustOverride Function Indexar() As DialogResult Implements IController.Indexar

        Public MustOverride Function SetCurrentFolio(ByVal value As Generic.Folio, ByRef nUpdateInfo As Boolean) As Boolean Implements IController.SetCurrentFolio

        Public MustOverride Function Reproceso(ByVal nMotivo As Short) As Boolean Implements IController.Reproceso

        Public MustOverride Function MotivosReproceso() As List(Of Item) Implements IController.MotivosReproceso

        Public Overrides Function InitializeLifetimeService() As Object Implements IController.InitializeLifetimeService
            'Dim lease As ILease = CType(MyBase.InitializeLifetimeService(), ILease)

            'If lease.CurrentState = LeaseState.Initial Then
            '    lease.InitialLeaseTime = TimeSpan.Zero
            'End If

            'Return lease
            Return Nothing
        End Function

        Public Property DocumentoCaptura As SlygNullable(Of Integer) Implements IController.DocumentoCaptura

        Public Overridable Function DocumentosCaptura(ByVal nEsquema As Short) As DBImaging.SchemaProcess.CTA_Documentos_ValidacionesDataTable Implements IController.DocumentosCaptura
            Return New DBImaging.SchemaProcess.CTA_Documentos_ValidacionesDataTable()
        End Function

        Public Overridable Function ShowDocumentosCaptura() As DialogResult Implements IController.ShowDocumentosCaptura
            Return DialogResult.OK
        End Function

#End Region

#Region " Metodos "

        Protected Sub BorrarTemporal()
            Dim temporales() As String = Directory.GetFiles(_TempPath, "*")

            For Each Temporal In temporales
                Try : File.Delete(Temporal) : Catch : End Try
            Next
        End Sub

        Protected Sub ShowMessage(ByVal nTitle As String, ByVal nEx As Exception)
            If (CType(Me.View, Form).InvokeRequired) Then
                Dim myDelegate As ShowMessageDelegate

                myDelegate = AddressOf ShowMessage
                CType(Me.View, Form).Invoke(myDelegate, New Object() {nTitle, nEx})
            Else
                DMB.DesktopMessageShow(nTitle, nEx)
            End If
        End Sub

        Protected MustOverride Sub Clear()

#End Region

#Region " Funciones "

        Protected Function ClonarColumna(ByVal nColumna As DataColumn) As DataColumn
            Dim newColumn As New DataColumn()

            newColumn.ColumnName = nColumna.ColumnName
            newColumn.Caption = nColumna.Caption
            newColumn.DataType = nColumna.DataType
            newColumn.MaxLength = nColumna.MaxLength

            Return newColumn
        End Function

        'Public Function ThumbnailCallback() As Boolean
        '    Return False
        'End Function

#End Region

    End Class

End Namespace