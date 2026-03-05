Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Text.RegularExpressions
Imports System.Windows.Forms
Imports Ghostscript.NET
Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Library.MiharuDMZ

Imports Word = Microsoft.Office.Interop.Word


Module Mod_Digitaliza
    Public SucursalIngreso As String
    Public RutaImagSCan As String
    Public Lote As String
    Public IdRImg As Integer
    Public ParamTipologia As Esquema
    Public LotesLeidos(0) As Esquema
    Public LoteExportar As Esquema
    Public DtEsquemas As DataTable
    Public DtCampos As DataTable
    Public Exportando As Boolean
    Public TiempoInactividad As Integer
    Public Reintentos As Integer
    Public Jornada As String
    Public NumLabel As String
    Public FechaMvto As Date
    Public LogActivo As String
    Public RutaLogError As String

    Public Structure Esquema
        Dim idEntidad As Integer
        Dim idProyecto As Integer
        Dim idEsquema As Integer
        Dim idSerieDctal As Integer
        Dim idOficina As Integer
        Dim EsquemaNombre As String
        Dim TamPapel As Integer
        Dim ResolucionX As Integer
        Dim ResolucionY As Integer
        Dim FormatoSalida As String
        Dim Contraste As Integer
        Dim TipoImagen As Integer
        Dim Duplex As Integer
        Dim Usuario As String
        Dim RutaLote As String
        Dim Numlote As String
        Dim CamposCaptura As String
        Dim FechaProceso As Date
        Dim Imagenes As Integer
        Dim NumImgen As Integer
        Dim Estado As String
        Dim HoraInactivo As String
        Dim TamanoHojaBlanca As Long
        Dim IntentoExportar As Integer
        Dim ErrorExportar As String
    End Structure

    Public Structure ImagenEscaneada
        Dim IdImg As Integer
        Dim Exportada As Integer '0,1
        Dim NomImagen As String
    End Structure

    Public Function ParsearExtensiones(cadena As String) As List(Of String)
        ' Soporta separadores: | ; ,
        If String.IsNullOrWhiteSpace(cadena) Then Return New List(Of String)

        ' Split por cualquiera de los separadores y quita vacíos
        Dim partes = Regex.Split(cadena, "[\|\;,]+")
        Dim lista As New List(Of String)

        For Each p In partes
            Dim ext = p.Trim()

            If String.IsNullOrEmpty(ext) Then Continue For

            ' Quitar prefijos "*." o "." si vienen
            If ext.StartsWith("*.") Then
                ext = ext.Substring(2)
            ElseIf ext.StartsWith(".") Then
                ext = ext.Substring(1)
            End If

            ' Normalizar a minúsculas
            ext = ext.ToLowerInvariant()

            ' Validación básica: solo letras/números (si quieres permitir cosas como "tiff" o "jpeg")
            ' Puedes aflojarla si necesitas otras extensiones con guiones etc.
            If Regex.IsMatch(ext, "^[a-z0-9]+$") Then
                lista.Add(ext)
            End If
        Next

        ' Quitar duplicados preservando orden
        Dim sinDuplicados = lista.Distinct().ToList()
        Return sinDuplicados
    End Function

    Public Function ConstruirSetExtensionesPermitidas(listaExt As List(Of String)) As HashSet(Of String)
        Dim hs As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
        For Each ext In listaExt
            hs.Add("." & ext) ' ej: ".jpg"
        Next
        Return hs
    End Function

    Public Function EsExtensionPermitida(rutaArchivo As String, extensionesPermitidas As HashSet(Of String)) As Boolean
        If String.IsNullOrWhiteSpace(rutaArchivo) Then Return False
        Dim ext = Path.GetExtension(rutaArchivo) ' Devuelve ".jpg" o "" si no tiene
        Return extensionesPermitidas.Contains(ext)
    End Function

    Public Function ConstruirFiltroOpenFileDialog(listaExt As List(Of String), Optional titulo As String = "Archivos permitidos") As String
        If listaExt Is Nothing OrElse listaExt.Count = 0 Then
            ' Si no hay lista, mostrar todos
            Return "Todos|*.*"
        End If

        ' Parte 1: grupo combinado (todos los permitidos)
        ' *.jpg;*.tif;*.png;*.pdf
        Dim comodines = listaExt.Select(Function(e) "*." & e)
        Dim combinado = String.Join(";", comodines)

        ' Parte 2: grupos por tipo (opcional, útil para que el usuario filtre por uno en particular)
        ' "JPG|*.jpg|TIF|*.tif|PNG|*.png|PDF|*.pdf"
        Dim gruposIndividuales As New List(Of String)
        For Each e In listaExt
            gruposIndividuales.Add(e.ToUpperInvariant() & "|*." & e)
        Next

        ' Ensamblar filtro completo: Combinado | individuales | Todos
        ' Ej: "Archivos permitidos|*.jpg;*.tif;*.png;*.pdf|JPG|*.jpg|TIF|*.tif|PNG|*.png|PDF|*.pdf|Todos|*.*"
        Dim filtro = $"{titulo}|{combinado}"
        If gruposIndividuales.Count > 0 Then
            filtro &= "|" & String.Join("|", gruposIndividuales)
        End If
        filtro &= "|Todos|*.*"

        Return filtro
    End Function


    Public Function ConvertDocxToPdf(inputPath As String) As String
        Dim OutputPdf As String = ""

        OutputPdf = Path.Combine(System.IO.Path.GetTempPath(), Path.GetFileNameWithoutExtension(inputPath) & ".pdf")
        'OutputPdf = Path.Combine(Path.GetDirectoryName(inputPath), Path.GetFileNameWithoutExtension(inputPath) & ".pdf")
        'System.IO.Path.GetTempPath()
        Dim app As Word.Application = Nothing
        Dim doc As Word.Document = Nothing
        Try
            app = New Word.Application() With {
              .Visible = False}
            ' Abre el documento en modo lectura
            doc = app.Documents.Open(inputPath, ReadOnly:=True, Visible:=False)
            ' --- Opción recomendada: ExportAsFixedFormat (PDF/XPS) ---
            doc.ExportAsFixedFormat(
                OutputFileName:=OutputPdf,
                ExportFormat:=Word.WdExportFormat.wdExportFormatPDF,
                OpenAfterExport:=False,
                OptimizeFor:=Word.WdExportOptimizeFor.wdExportOptimizeForPrint,
                Range:=Word.WdExportRange.wdExportAllDocument)

            ' --- Alternativa equivalente: SaveAs2 con formato PDF ---
            'doc.SaveAs2(FileName:=outputPdf, FileFormat:=Word.WdSaveFormat.wdFormatPDF)
            Return OutputPdf
        Catch ex As Exception
            Return ""
        Finally
            ' Cierre y liberación COM en el orden correcto:
            If doc IsNot Nothing Then
                doc.Close(SaveChanges:=False)
                Marshal.FinalReleaseComObject(doc)
                doc = Nothing
            End If
            If app IsNot Nothing Then
                app.Quit()
                Marshal.FinalReleaseComObject(app)
                app = Nothing
            End If

            ' Recolección para acelerar la liberación de RCWs
            GC.Collect()
            GC.WaitForPendingFinalizers()
            GC.Collect()
            GC.WaitForPendingFinalizers()
        End Try
    End Function



End Module
