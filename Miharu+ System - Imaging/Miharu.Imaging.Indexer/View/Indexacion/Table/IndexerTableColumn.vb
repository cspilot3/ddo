Imports System.Windows.Forms
Imports Miharu.Desktop.Library.Config

Namespace View.Indexacion.Table

    Public Class IndexerTableColumn

#Region " Declaraciones "

        Public Event WidthChange()

#End Region

#Region " Propiedades "

        Public Property IdCampoTabla As Integer

        Public Property Table As IndexerTable

        Private _Width As Integer = 100
        Public Property Width As Integer
            Get
                Return _Width
            End Get
            Set(ByVal value As Integer)
                _Width = value
                RaiseEvent WidthChange()
            End Set
        End Property

        Public Property HeaderText As String

        Friend Property Header As Button

        Public Property Type As DesktopConfig.CampoTipo

        Public Property MaximumLength As Short

        Public Property Usa_Decimales As Boolean
        Public Property Cantidad_Decimales As Short

        Public Property IsReadOnly As Boolean
        Public Property DefaultValue As String

        Public Property Es_Obligatorio As Boolean

        Public Property Mascara As String
        Public Property FormatoFecha As String

        Public Property Items As DataView
        Public Property DisplayMember As String
        Public Property ValueMember As String

#End Region

    End Class

End Namespace