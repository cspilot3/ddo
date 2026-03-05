Imports DBAgrario

Public Class ReportManager

#Region " Constructores "

    Public Sub New()

    End Sub

#End Region

#Region " Metodos "

    Public Sub GenerarTodos(ByVal path As String)
        'Crear la estructura de directorios donde se almacena los reportes
        If Not (System.IO.Directory.Exists(path & "\")) Then
            System.IO.Directory.CreateDirectory(path & "\")
        End If
        If Not (System.IO.Directory.Exists(path & "\" & Date.Now.ToString("yyyyMMdd") & "\")) Then
            System.IO.Directory.CreateDirectory(path & "\" & Date.Now.ToString("yyyyMMdd") & "\")
        End If

        'TODO: LLAMAR LA GENERACION DE LOS PDF
        ''LLAMAR LA GENERACION DE LOS PDF
        GenerarArchivoPrueba(path & "\" & Date.Now.ToString("yyyyMMdd") & "\")
    End Sub

    Protected Sub GenerarArchivoPrueba(ByVal path As String)
        Dim dbmBanagrario As New DBAgrarioDataBaseManager(Program.ConnectionStrings.PunteoAgrario)
        Try
            dbmBanagrario.Connection_Open(1)
            Dim LogData As New SchemaProcess.TBL_ArchivoDataTable
            LogData = dbmBanagrario.SchemaProcess.TBL_Archivo.DBFindByNombre_ArchivoArchivo_Valido("\\10.64.64.52\sourcesafedata$\temp\Logs\ArchivoACargar20101228PED", False)
            Dim tbl As DataTable = LogData
            Dim generarPdf As PDFManager = New PDFManager(tbl)

            generarPdf.GenerarPdf(path & "ArchivosCargados.pdf")
            dbmBanagrario.Connection_Close()
        Catch ex As Exception

        End Try
    End Sub

#End Region

End Class
