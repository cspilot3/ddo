Imports System.Data

#Region " Security "

<System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://slyg.com.co/miharu/PR_ExistKey/")> _
Public Class PR_ExistKey

    <System.Xml.Serialization.XmlAttribute()> _
    Public Result As Boolean

    <System.Xml.Serialization.XmlAttribute()> _
    Public Message As String

    <System.Xml.Serialization.XmlAttribute()> _
    Public Encontrado As Boolean

    <System.Xml.Serialization.XmlAttribute()> _
    Public Identificador As String

    <System.Xml.Serialization.XmlAttribute()> _
    Public Folios As Short

    <System.Xml.Serialization.XmlAttribute()> _
    Public Size As Long

    <System.Xml.Serialization.XmlAttribute()> _
    Public Tipo As String

End Class

<System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://slyg.com.co/miharu/PR_ExistKeyC/")> _
Public Class PR_ExistKeyC

    <System.Xml.Serialization.XmlAttribute()> _
    Public Result As Boolean

    <System.Xml.Serialization.XmlAttribute()> _
    Public Message As String

    <System.Xml.Serialization.XmlAttribute()> _
    Public Encontrado As Boolean

    <System.Xml.Serialization.XmlAttribute()> _
    Public Identificador As String

    <System.Xml.Serialization.XmlAttribute()> _
    Public Folios As Short

    <System.Xml.Serialization.XmlAttribute()> _
    Public Size As Long

    <System.Xml.Serialization.XmlAttribute()> _
    Public Tipo As String

    <System.Xml.Serialization.XmlAttribute()> _
    Public Ruta As String

End Class

<System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://slyg.com.co/miharu/PR_ServerConfig/")> _
Public Class PR_ServerConfig

    <System.Xml.Serialization.XmlAttribute()> _
    Public Result As Boolean

    <System.Xml.Serialization.XmlAttribute()> _
    Public Message As String

    <System.Xml.Serialization.XmlAttribute()> _
    Public Servidor As String

    <System.Xml.Serialization.XmlAttribute()> _
    Public AppName As String

    <System.Xml.Serialization.XmlAttribute()> _
    Public Puerto As Integer

    <System.Xml.Serialization.XmlAttribute()> _
    Public Path As String

    <System.Xml.Serialization.XmlAttribute()> _
    Public SaveFileName As String

End Class

<System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://slyg.com.co/miharu/PR_InsertFolder/")> _
Public Class PR_InsertFolder

    <System.Xml.Serialization.XmlAttribute()> _
    Public Result As Boolean

    <System.Xml.Serialization.XmlAttribute()> _
    Public Message As String

    <System.Xml.Serialization.XmlAttribute()> _
    Public idFolder As Long

End Class

<System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://slyg.com.co/miharu/PR_InsertFile/")> _
Public Class PR_InsertFile

    <System.Xml.Serialization.XmlAttribute()> _
    Public Result As Boolean

    <System.Xml.Serialization.XmlAttribute()> _
    Public Message As String

    <System.Xml.Serialization.XmlAttribute()> _
    Public idFile As Short

    <System.Xml.Serialization.XmlAttribute()> _
    Public Token As String

End Class

<System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://slyg.com.co/miharu/PR_getConfigEsquema/")> _
Public Class PR_getConfigEsquema

    Public Structure Documento
        Public id As Short
        Public Nombre As String
    End Structure

    <System.Xml.Serialization.XmlAttribute()> _
    Public Result As Boolean

    <System.Xml.Serialization.XmlAttribute()> _
    Public Message As String

    <System.Xml.Serialization.XmlAttribute()> _
    Public Esquema As String

    <System.Xml.Serialization.XmlElement()> _
    Public Documentos As List(Of Documento)

End Class

<System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://slyg.com.co/miharu/PR_getConfigDocumento/")> _
Public Class PR_getConfigDocumento

    Public Structure Campo
        Public id As Integer
        Public Nombre As String
        Public Tipo As String
        Public EsBusqueda As Boolean
        Public idBusqueda As Integer
        Public NombreBusqueda As String
    End Structure

    <System.Xml.Serialization.XmlAttribute()> _
    Public Result As Boolean

    <System.Xml.Serialization.XmlAttribute()> _
    Public Message As String

    <System.Xml.Serialization.XmlAttribute()> _
    Public Documento As String

    <System.Xml.Serialization.XmlElement()> _
    Public Campos As List(Of Campo)

End Class

<System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://slyg.com.co/miharu/PR_FindImage/")> _
Public Class PR_FindImage

    Public Structure Imagen
        Public Entidad As Short
        Public Proyecto As Short
        Public Esquema As Short
        Public Folder As Long
        Public File As Short
        Public Version As Short
        Public Key As String
    End Structure

    <System.Xml.Serialization.XmlAttribute()> _
    Public Result As Boolean

    <System.Xml.Serialization.XmlAttribute()> _
    Public Message As String

    <System.Xml.Serialization.XmlAttribute()> _
    Public Campo As String

    <System.Xml.Serialization.XmlElement()> _
    Public Imagenes As List(Of Imagen)

End Class

#End Region