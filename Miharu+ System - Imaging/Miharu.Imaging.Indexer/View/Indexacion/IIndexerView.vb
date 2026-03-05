Namespace View.Indexacion

    Public Enum ModoAutoIndexarEnum
        Folder
        Documento
    End Enum

    Public Interface IIndexerView

        Property Esquema_DataSource As DataView
        Property Esquema_Enabled As Boolean
        Property Esquema_Index As Integer
        Property Esquema_Value As Short
        Property Information As String
        Property SelectedInputControl As IInputControl
        Property SelectedValidationControl As ValidationControl
        Property TipoDocumental_DataSource As DataView
        Property TipoDocumental_Enabled As Boolean
        Property TipoDocumental_Index As Integer
        Property TipoDocumental_Value As Nullable(Of Integer)
        ReadOnly Property AnexoCompleto As Boolean
        ReadOnly Property Campos As List(Of CampoCaptura)
        ReadOnly Property CamposLlave As List(Of CampoLlaveCaptura)
        ReadOnly Property Validaciones As List(Of ValidacionCaptura)
        ReadOnly Property ValidacionListas As Boolean
        ReadOnly Property CancelProcess As Boolean
        ReadOnly Property Esquema_Text As String
        ReadOnly Property GridControl As Table.IndexerTable
        ReadOnly Property IndexerController As Controller.Indexer.IIndexerController
        ReadOnly Property RequiereAutorizacion As Boolean
        ReadOnly Property TipoDocumental_Text As String
        Sub Esquema_Refresh()
        Sub HideGridControl()

        Sub ShowAddFolioButton(ByVal nShow As Boolean)
        Sub ShowAutoIndexar(ByVal nShow As Boolean)
        Sub ShowDataPanel(ByVal nShow As Boolean)
        Sub ShowCamposLlavePanel(ByVal nShow As Boolean)
        Sub ShowDeleteFolioButton(ByVal nShow As Boolean)
        Sub ShowDesindexarFolioButton(ByVal nShow As Boolean)
        Sub ShowGridControl(ByVal TableControl As TableInputControl)
        Sub ShowInformationPanel(ByVal nShow As Boolean)
        Sub ShowNewFileButton(ByVal nShow As Boolean)
        Sub ShowNewFolderButton(ByVal nShow As Boolean)
        Sub ShowNextButton(ByVal nShow As Boolean)
        Sub ShowReprocesoButton(ByVal nShow As Boolean)
        Sub ShowSaveButton(ByVal nShow As Boolean)
        Sub ShowToolTipData(ByVal nShow As Boolean)
        Sub ShowValidationsPanel(ByVal nShow As Boolean)
        Sub TipoDocumental_Refresh()
        Sub ShowValidationsListasPanel(ByVal nShow As Boolean)
        Sub SetSearchControl(ByVal nSearchControl As IListValidationControl)

    End Interface

End Namespace