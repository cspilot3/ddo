Namespace Controls

    Public Interface IImagingSearchControlParameters

        Event DisplayResult(ByVal nData As DataTable)

        Event BeginSearch()

        Event EndSearch()

        Sub SetFocus()

        Sub Search()

    End Interface

End Namespace