Imports Miharu.Desktop.Library.Config
Imports System.Windows.Forms
Imports DBImaging.SchemaProcess

Namespace Controller

    Public Interface IController

        Function DocumentosCaptura(ByVal nEsquema As Short) As CTA_Documentos_ValidacionesDataTable
        Function Indexar() As DialogResult
        Function InitializeLifetimeService() As Object
        Function MotivosReproceso() As List(Of Slyg.Tools.Item)
        Function NextFolio(ByRef nUpdateInfo As Boolean) As Boolean
        Function NextIndexingElement(ByVal ot As Integer, ByVal nEstado As DBCore.EstadoEnum, ByVal nIdDocumento As Slyg.Tools.SlygNullable(Of Integer)) As Boolean
        Function PreviousFolio(ByRef nUpdateInfo As Boolean) As Boolean
        Function Reproceso(ByVal nMotivo As Short) As Boolean
        Function Save() As Boolean
        Function SetCurrentFolio(ByVal nFolio As Generic.Folio, ByRef nUpdateInfo As Boolean) As Boolean
        Function ShowDocumentosCaptura() As DialogResult
        Property DocumentoCaptura As Slyg.Tools.SlygNullable(Of Integer)
        Property IndexerDesktopGlobal As DesktopGlobal
        Property IndexerImagingGlobal As ImagingGlobal
        Property IndexerSesion As Security.Library.Session.Sesion
        ReadOnly Property Cargado As Boolean
        ReadOnly Property Ciclo As Integer
        ReadOnly Property IsOCRUsed As Boolean
        ReadOnly Property CurrentDocumentFile As Generic.DocumentFile
        ReadOnly Property CurrentDocumentFileIndex As Integer
        ReadOnly Property CurrentFolder As Generic.Folder
        ReadOnly Property CurrentFolderIndex As Integer
        ReadOnly Property CurrentFolio As Generic.Folio
        ReadOnly Property CurrentFolioIndex As Integer
        ReadOnly Property CurrentImage As Slyg.Tools.Imaging.FreeImageAPI.FreeImageBitmap
        ReadOnly Property CurrentImageIndex As Integer
        ReadOnly Property EventManager As Events.EventManager
        ReadOnly Property Folders As List(Of Generic.Folder)
        ReadOnly Property Folios As Integer
        ReadOnly Property ImageCount As Integer
        ReadOnly Property PreguntarSalida As Boolean
        ReadOnly Property View As View.IView
        Sub Dispose()
        Sub Inicializar(ByVal nTempPath As String, ByVal nIndexerSesion As Security.Library.Session.Sesion, ByVal nIndexerDesktopGlobal As DesktopGlobal, ByVal nIndexerImagingGlobal As ImagingGlobal)
        Sub Unlock()

    End Interface

End Namespace