Namespace Controller.Indexer

    Public Interface IIndexerController
        Function AddFolio() As Boolean
        Function Anexos() As List(Of Slyg.Tools.Item)
        Function Campos(ByVal idDocumento As Integer) As List(Of View.Indexacion.CampoCaptura)
        Function CamposLlave(ByVal idDocumento As Integer) As List(Of View.Indexacion.CampoLlaveCaptura)
        Function GetValueFileData(ByVal idCampo As Integer) As String
        Function DeleteFolio() As Boolean
        Function DesindexarFolio() As Boolean
        Function LoadAnexo(ByVal idAnexo As Integer) As IEnumerable(Of String)
        Function NewDocumentFile() As Boolean
        Function NewFolder() As Boolean
        Function Validaciones(ByVal idDocumento As Integer) As List(Of View.Indexacion.ValidacionCaptura)
        Function Validar() As Boolean
        Function ValidacionListas() As Boolean
        Property ReemplazarImagen As Boolean
        ReadOnly Property AllowNewFile As Boolean
        ReadOnly Property AllowNewFolder As Boolean
        ReadOnly Property IndexerView As View.Indexacion.IIndexerView
        ReadOnly Property ShowSecondControls As Boolean
        Sub AutoIndexar(ByVal nDocumento As Integer, ByVal nFolios As Integer, ByVal nModo As View.Indexacion.ModoAutoIndexarEnum)
        Sub Move(ByVal nSourceFolio As Generic.Folio, ByVal nTargetFolio As Generic.Folio, ByVal nTargetIndex As Integer)
        Function GetValueLlaveData() As DBCore.SchemaProcess.TBL_Expediente_Llave_LineaRow

    End Interface

End Namespace