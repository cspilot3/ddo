Imports Miharu.Desktop.Library.Config

Namespace View.Indexacion

    Public Class DefinicionCaptura
        Public Property id As Integer

        Public Property Caption As String
        Public Property Es_Obligatorio_Campo As Boolean

        Public Property MaximumLength As Short
        Public Property MinimumLength As Short
        Public Property Usa_Decimales As Boolean
        Public Property Caracter_Decimal As Char
        Public Property Cantidad_Decimales As Short
        Public Property IsReadOnly As Boolean
        Public Property DefaultValue As String

        Public Property Type As DesktopConfig.CampoTipo

        Public Property FormatoFecha As String
        Public Property Mascara As String

        Public Property Items As DataView
        Public Property ValueMember As String
        Public Property DisplayMember As String

    End Class

End Namespace