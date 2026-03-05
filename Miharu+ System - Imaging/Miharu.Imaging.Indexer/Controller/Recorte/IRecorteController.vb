Namespace Controller.Recorte

    Public Interface IRecorteController

        Function GetRecortes() As DBCore.SchemaImaging.TBL_File_RecorteDataTable

        Property CurrentRecorteIndex() As Integer

    End Interface

End Namespace