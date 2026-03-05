Namespace View.Indexacion

    Public Class CampoLlaveCaptura
        Public Property id As Short

        Public Property Id_Campo As Integer
        Public Property Numero_Llave As Integer
        Public Property fk_Expediente As Integer
        Public Property fk_Entidad As Integer
        Public Property fk_Proyecto As Integer
        Public Property fk_Esquema As Integer
        Public Property fk_Tipo_Llave As Integer

        Public Property Marca_Height_Campo As Integer
        Public Property Marca_Width_Campo As Integer
        Public Property Marca_X_Campo As Integer
        Public Property Marca_Y_Campo As Integer
        Public Property Usa_Marca As Boolean

        Public Property Control As IInputControl
    End Class

End Namespace