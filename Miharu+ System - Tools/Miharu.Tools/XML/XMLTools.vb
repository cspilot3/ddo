Public Class XMLTools

    Public Shared Function ParceXML(ByVal nTexto As String) As String
        nTexto = Replace(nTexto, "'", "&apos;")
        nTexto = Replace(nTexto, Chr(34), "&quot")
        nTexto = Replace(nTexto, ">", "&gt;")
        nTexto = Replace(nTexto, "<", "&lt;")
        nTexto = Replace(nTexto, "&", "&amp;")

        Return nTexto
    End Function

End Class
