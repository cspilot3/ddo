Imports System.Web.Services
Imports System.ComponentModel
Imports Newtonsoft.Json

' Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la siguiente línea.
' <System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://slyg.com.co/miharu/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class ImagingExplorerService
    Inherits Services.WebService

#Region " Servidor "

    <WebMethod()> _
    Public Overloads Function ExistsKeyA(ByVal nEntidad As Short, ByVal nProyecto As Short, ByVal nEsquema As Short, ByVal nKey As String) As PR_ExistKey
        Return ExistsKeyB(nEntidad, nProyecto, nEsquema, -1, nKey)
    End Function

    <WebMethod()> _
    Public Overloads Function ExistsKeyB(ByVal nEntidad As Short, ByVal nProyecto As Short, ByVal nEsquema As Short, ByVal nDocumento As Integer, ByVal nKey As String) As PR_ExistKey
        Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.CoreConnectionString)
        Dim Respuesta As New PR_ExistKey
        Dim documento As Slyg.Tools.SlygNullable(Of Integer)

        If (nDocumento = -1) Then
            documento = Nothing
        Else
            documento = nDocumento
        End If

        Try
            dbmCore.Connection_Open(1) ' System

            Dim FileKeyDataTable = dbmCore.SchemaImaging.PA_File_Key.DBExecute(nEntidad, nProyecto, nEsquema, documento, nKey)

            Respuesta.Result = True
            Respuesta.Message = ""

            If FileKeyDataTable.Rows.Count > 0 Then
                Respuesta.Encontrado = True
                Respuesta.Identificador = FileKeyDataTable(0).File_Unique_Identifier.ToString()
                Respuesta.Folios = FileKeyDataTable(0).Folios_Documento_File
                Respuesta.Size = FileKeyDataTable(0).Tamaño_Imagen_File
                Respuesta.Tipo = FileKeyDataTable(0).fk_Content_Type
            Else
                Respuesta.Encontrado = False
            End If

        Catch ex As Exception
            Respuesta.Result = False
            Respuesta.Message = ex.Message
        Finally
            dbmCore.Connection_Close() ' System
        End Try

        Return Respuesta
    End Function

    <WebMethod()> _
    Public Function dataBridge(ByVal nrequest As String) As String
        Dim queryRequest As QueryRequest = JsonConvert.DeserializeObject(Of QueryRequest)(CryptoUtil.decrypt(nrequest))
        Return CryptoUtil.encrypt(JsonConvert.SerializeObject(New ProcessQuery().execute(queryRequest)))
    End Function

#End Region

End Class