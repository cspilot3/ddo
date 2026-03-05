Imports System.Windows.Forms
Imports Miharu.Imaging.Indexer.View.Indexacion
Imports Miharu.Desktop.Library.Config

Namespace View

    Public Class KeyValueItem

        Public Sub New(ByVal nKey As String, ByVal nValue As Short, ByVal nItem As Short)
            Key = nKey
            Value = nValue
            Item = nItem
        End Sub

        Public Property Key As String
        Public Property Value As Short
        Public Property Item As Short
    End Class

    Public Class TriggersValidations_Items

        Public Sub New(ByVal id_ValidacionOcultar As Short, ByVal Valor As String, ByVal id_Campo As Short, ByVal OperadorValidacion As String)
            _idValidacionOcultar = id_ValidacionOcultar
            _Valor = Valor
            _idCampo = id_Campo
            _OperadorValidacion = OperadorValidacion
        End Sub

        Public Property _idValidacionOcultar As Short
        Public Property _Valor As String
        Public Property _idCampo As Short
        Public Property _OperadorValidacion As String
    End Class

    Public Interface IInputControl
        Property Etiqueta As String


        Property Value As Object
        Property ValorValidacion As Object
        Property ValueControl As Boolean
        Property ValueValidacionListas As Object

        Property ValueOld1 As Object
        Property ValueOld2 As Object
        Property ValueOld3 As Object

        Property IsCalidad As Boolean
        Property ÏsOCRCapture As Boolean
        Property EnableTableOCR As Boolean

        Property NextControl As Control

        Property ShowPrimaryControls As Boolean
        Property ShowSecondControls As Boolean
        Property ShowValidacionListasControls As Boolean

        Property CampoCaptura As CampoCaptura
        Property CampoLlaveCaptura As CampoLlaveCaptura
        ReadOnly Property DefinicionCaptura As List(Of DefinicionCaptura)

        Property IndexerView As IIndexerView

        Property Tipo As DesktopConfig.CampoTipo

        Property UsaTrigger As Boolean

        Property TriggerValues As List(Of KeyValueItem)

        Property TriggerValidaciones As List(Of TriggersValidations_Items)

        Property IsVisible As Boolean

        Sub LoadDefinition(ByVal nDefinicionCaptura As List(Of DefinicionCaptura))

        Function Validar() As Boolean

        ReadOnly Property RequiereAutorizacion As Boolean

    End Interface

End Namespace