Namespace View.Indexacion

    Public Class InputControl

#Region " Constructores "

        Public Sub New()
            ' Llamada necesaria para el diseñador.
            InitializeComponent()

            ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        End Sub

#End Region

#Region " Propiedades "

        Public Property Etiqueta As String
            Get
                Return EtiquetaLabel.Text
            End Get
            Set(ByVal value As String)
                EtiquetaLabel.Text = value
            End Set
        End Property

#End Region

    End Class

End Namespace