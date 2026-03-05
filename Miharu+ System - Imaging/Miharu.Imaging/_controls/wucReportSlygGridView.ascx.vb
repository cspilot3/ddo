Imports System.IO
Imports System.Text
Imports System.IO.Directory
Imports System.Data
Imports Microsoft.Office.Interop.Excel
Imports Microsoft.Office.Interop
Imports System.Xml.Serialization
Imports System.Security.Cryptography
Imports System.ComponentModel
Imports Slyg.Tools
Imports Slyg.Tools.Cryptographic
Imports Miharu.Web.Controls
Imports Miharu
Imports System.Web.UI.WebControls
Imports System.Web
Imports System.Net
Imports AjaxControlToolkit
Public Class wucReportSlygGridView
    Inherits System.Web.UI.UserControl
    Dim dat_table As System.Data.DataTable = New System.Data.DataTable
#Region " Declaraciones "
    Private ConnectionString_Core As String
    Private ConnectionString_Tools As String
    Private ConnectionString_Archiving As String
    Private Usuario As Integer
    Private IdReporte As Integer
    Private Entidad As Integer
    Private Esquema As Integer
    Private Proyecto As Integer
    Private TipoFormatoArchivo As Slyg.Tools.SlygNullable(Of Integer)
    Private CaracterSeparacion As String
    Private ManejaEncabezado As Slyg.Tools.SlygNullable(Of Boolean)
    Private idNotificacion As Slyg.Tools.SlygNullable(Of Integer)
    Private Genera_ZIP_Archivo As Slyg.Tools.SlygNullable(Of String)
    Private ClaveArchivo As Slyg.Tools.SlygNullable(Of String)
    Private Salto_linea_ As String
    Private Maneja_Encabezado As String
    Private GuardaArchivo As String
    Dim _tempPath As String = Nothing
    Dim filename As String
    Dim contarReportes As Int32
    Dim ButtonOnClickReportMas As Boolean
    Dim Contadorshowmessage As Int32
    Dim Archivo As String
    Public Property fkEsquema() As Integer
        Get
            fkEsquema = Esquema
        End Get
        Set(ByVal value As Integer)
            Esquema = value
        End Set
    End Property
    Public Property fkProyecto() As Integer
        Get
            fkProyecto = Proyecto
        End Get
        Set(ByVal value As Integer)
            Proyecto = value
        End Set
    End Property
    Public Property fkEntidad() As Integer
        Get
            fkEntidad = Esquema
        End Get
        Set(ByVal value As Integer)
            Esquema = value
        End Set
    End Property
    Public Property Titulo() As String
        Get
            Return nombreReporteLabel.Text
        End Get
        Set(ByVal value As String)
            nombreReporteLabel.Text = value
        End Set
    End Property
    Public Property InternalGridView() As Miharu.Web.Controls.SlygGridView
        Get
            Return ResultadoReporteSlygGridViewControl
        End Get
        Set(ByVal value As Miharu.Web.Controls.SlygGridView)
            ResultadoReporteSlygGridViewControl = value
        End Set
    End Property
    Public Property Id_Reporte() As Integer
        Get
            Id_Reporte = IdReporte
        End Get
        Set(ByVal Value As Integer)
            IdReporte = Value
        End Set
    End Property
    Public Property Conection_String_Core() As String
        Get
            Conection_String_Core = ConnectionString_Core
        End Get
        Set(ByVal Value As String)
            ConnectionString_Core = Value
        End Set
    End Property
    Public Property Conection_String_Tools() As String
        Get
            Conection_String_Tools = ConnectionString_Tools
        End Get
        Set(ByVal Value As String)
            ConnectionString_Tools = Value
        End Set
    End Property
    Public Property GetUsuario() As Integer
        Get
            GetUsuario = Usuario
        End Get
        Set(value As Integer)
            Usuario = value
        End Set
    End Property
    Public Property Conection_String_Archiving() As String
        Get
            Conection_String_Archiving = ConnectionString_Archiving
        End Get
        Set(ByVal Value As String)
            ConnectionString_Archiving = Value
        End Set
    End Property
    Public Property Salto_Linea() As String
        Get
            Salto_Linea = Salto_linea_
        End Get
        Set(value As String)
            Salto_linea_ = value
        End Set
    End Property
    Public Property ButtonOnClickReportMas_() As Boolean
        Get
            ButtonOnClickReportMas_ = ButtonOnClickReportMas
        End Get
        Set(ByVal Value As Boolean)
            ButtonOnClickReportMas = Value
        End Set
    End Property
    Public Property Contadorshowmessage_() As Int32
        Get
            Contadorshowmessage_ = Contadorshowmessage
        End Get
        Set(ByVal Value As Int32)
            Contadorshowmessage = Value
        End Set
    End Property
    Public Property contarReportes_() As Int32
        Get
            contarReportes_ = contarReportes
        End Get
        Set(ByVal Value As Int32)
            contarReportes = Value
        End Set
    End Property
    Public Property GuardaArchivo_() As String
        Get
            GuardaArchivo_ = GuardaArchivo
        End Get
        Set(value As String)
            GuardaArchivo = value
        End Set
    End Property
    Public Property NombreReporte() As String
        Get
            Return nombreReporteLabel.Text
        End Get
        Set(value As String)
            nombreReporteLabel.Text = value
        End Set
    End Property
    Shared Property TmpPath As String = "temp\ReportesMiharu\"
#End Region
#Region " Metodos "
    Public Function Exportar(nEncabezado As Boolean, _
                             nComma As Boolean, _
                             nTab As Boolean, _
                             nSemicolom As Boolean, _
                             nBlank As Boolean,
                             Excel As Boolean, _
                             Texto As Boolean, _
                             Optional Salto_Linea As String = "") As Integer
        Dim D_FileName As String
        Dim Opcion As Integer = 0
        If ResultadoReporteSlygGridViewControl.Rows.Count > 0 Then
            Dim sSeparador As String = ","
            Dim RutaFinal As String = ""
            Dim nombre As String = String.Empty
            Try
                If Excel Then
                    Opcion = 1
                    Session("OpcionExportacion") = "1"
                    'D_FileName = Titulo + " " + DateTime.Now.ToString("yyyyMMdd_HH_mm") + ".xlsx"
                    D_FileName = Titulo + " " + DateTime.Now.ToString("yyyyMMdd_HH_mm") + ".xls"
                    nombre = D_FileName
                    If ButtonOnClickReportMas_ Then
                        'Export XLSX
                        If Contadorshowmessage_ = 0 Then
                            'ExportDataGridView_To_Excel(CType(ViewState("dat_table"), System.Data.DataTable), D_FileName, True)
                            ExportDataGridView_To_Excel(CType(ViewState("dat_table"), System.Data.DataTable), nombre)
                            filename = _tempPath
                            GuardaArchivo_ = filename.Replace(nombre, "")
                        Else
                            D_FileName = GuardaArchivo_ + nombre
                            'ExportDataGridView_To_Excel(CType(ViewState("dat_table"), System.Data.DataTable), D_FileName, True)
                            ExportDataGridView_To_Excel(CType(ViewState("dat_table"), System.Data.DataTable), nombre)
                        End If
                    Else
                        'ExportDataGridView_To_Excel(CType(ViewState("dat_table"), System.Data.DataTable), D_FileName, True)
                        ExportDataGridView_To_Excel(CType(ViewState("dat_table"), System.Data.DataTable), nombre)
                    End If
                ElseIf Texto Then
                    Opcion = 1
                    Session("OpcionExportacion") = "2"
                    'Se obtiene el separador seleccionado
                    If nComma Then
                        sSeparador = ","
                    ElseIf nTab Then
                        sSeparador = vbTab
                    ElseIf nSemicolom Then
                        sSeparador = ";"
                    ElseIf nBlank Then
                        sSeparador = ""
                    End If
                    'Generación Texto (TXT)
                    nombre = Titulo + ".txt"
                    Dim rutanombre = GuardaArchivo_ + nombre
                    If ButtonOnClickReportMas_ Then
                        If Contadorshowmessage_ = 0 Then
                            If ResultadoReporteSlygGridViewControl.Rows.Count > 0 Then
                                Dim fs As FileStream = New FileStream(rutanombre, FileMode.Create)
                                'ExportSlygGridViewToCSV(CType(ViewState("dat_table"), System.Data.DataTable), fs, sSeparador, nEncabezado, Salto_Linea)
                                ExportSlygGridViewToCSV(CType(ViewState("dat_table"), System.Data.DataTable), nombre, sSeparador, nEncabezado, Salto_Linea)
                                Me._tempPath = fs.Name
                                Archivo = "[" + fs.Name + "]"
                                filename = fs.Name
                                GuardaArchivo_ = filename.Replace(nombre, "")
                            End If
                        Else
                            If ResultadoReporteSlygGridViewControl.Rows.Count > 0 Then
                                Dim fs As FileStream = _
                                    New FileStream(rutanombre, FileMode.Create)
                                'ExportSlygGridViewToCSV(CType(ViewState("dat_table"), System.Data.DataTable), fs, "", False, Salto_Linea)
                                ExportSlygGridViewToCSV(CType(ViewState("dat_table"), System.Data.DataTable), nombre, sSeparador, nEncabezado, Salto_Linea)
                                Me._tempPath = fs.Name
                                Archivo = "[" + fs.Name + "]"
                            End If
                        End If
                    Else
                        Dim Archivo As String = String.Empty
                        If ResultadoReporteSlygGridViewControl.Rows.Count > 0 Then
                            Dim fs As FileStream = New FileStream(rutanombre, FileMode.Create)
                            'ExportSlygGridViewToCSV(CType(ViewState("dat_table"), System.Data.DataTable), fs, sSeparador, nEncabezado, Salto_Linea)
                            ExportSlygGridViewToCSV(CType(ViewState("dat_table"), System.Data.DataTable), nombre, sSeparador, nEncabezado, Salto_Linea)
                            Me._tempPath = fs.Name
                            Archivo = "[" + fs.Name + "]"
                        End If
                        If Archivo <> String.Empty Then
                            'MensajeLabel.Text = "Se exporto el reporte al archivo: " & Archivo & "."
                            MensajeLabel.Text = "¡Se exporto el reporte al archivo!"
                        End If
                    End If
                Else
                    'Generación CSV
                    nombre = Titulo + ".csv"
                    'Se obtiene el separador seleccionado
                    If nComma Then
                        sSeparador = ","
                    ElseIf nTab Then
                        sSeparador = vbTab
                    ElseIf nSemicolom Then
                        sSeparador = ";"
                    ElseIf nBlank Then
                        sSeparador = ""
                    End If
                    If ButtonOnClickReportMas_ Then
                        If Contadorshowmessage_ = 0 Then

                            If ResultadoReporteSlygGridViewControl.Rows.Count > 0 Then
                                Dim fs As FileStream = New FileStream(nombre, FileMode.CreateNew)
                                ExportSlygGridViewToCSV(CType(ViewState("dat_table"), System.Data.DataTable), fs, sSeparador, nEncabezado, Salto_Linea)
                                Me._tempPath = fs.Name
                                Archivo = "[" + fs.Name + "]"
                                filename = fs.Name
                                GuardaArchivo_ = filename.Replace(nombre, "")
                            End If
                        Else
                            Dim rutanombre = GuardaArchivo_ + nombre
                            Dim fs As FileStream = _
                               New FileStream(rutanombre, FileMode.Create)
                            If ResultadoReporteSlygGridViewControl.Rows.Count > 0 Then
                                ExportSlygGridViewToCSV(CType(ViewState("dat_table"), System.Data.DataTable), fs, sSeparador, nEncabezado, Salto_Linea)
                                Me._tempPath = fs.Name
                                Archivo = "[" + fs.Name + "]"
                                If contarReportes = 1 Then
                                    MensajeLabel.Text = "Archivos CSV han sido generados correctamente en la ruta:" & GuardaArchivo_ & "."
                                End If
                            End If
                        End If
                    Else
                        Dim Archivo As String = String.Empty
                        If ResultadoReporteSlygGridViewControl.Rows.Count > 0 Then
                            Dim fs As FileStream = New FileStream(Archivo, FileMode.CreateNew)
                            ExportSlygGridViewToCSV(CType(ViewState("dat_table"), System.Data.DataTable), fs, sSeparador, nEncabezado, Salto_Linea)
                            Me._tempPath = fs.Name
                            Archivo = "[" + fs.Name + "]"
                        End If

                        If Archivo <> String.Empty Then
                            MensajeLabel.Text = "Se exporto el reporte al archivo: " & Archivo & "."
                        End If
                    End If
                End If
            Catch ex As Exception
                MensajeLabel.Text = "Exportar " + ex.ToString
            End Try
        Else
            MensajeLabel.Text = "No existen registros para exportar."
        End If
        Return Opcion
    End Function
    Public Sub ExportSlygGridViewToCSV(ByRef data As System.Data.DataTable, ByRef archivo As String, Optional ByVal separator As String = ",", Optional ByVal Encabezado As Boolean = True, Optional ByVal SaltoLinea As String = "")
        System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US")
        Dim byteArray() As Byte
        Dim ms As MemoryStream = New MemoryStream()
        Session("NombreArchivoExportacion") = archivo
        Using sw As New StreamWriter(ms)
            Dim strExport As String = ""
            Dim final As Integer = 0
            Try
                If Encabezado Then
                    final = 1
                    'Export the Columns to Header excel file
                    For Each dcol In data.Columns
                        If final < data.Columns.Count Then
                            strExport += CType(dcol, System.Data.DataColumn).ColumnName + separator
                            final += 1
                        Else
                            strExport += CType(dcol, System.Data.DataColumn).ColumnName
                        End If
                    Next
                    If SaltoLinea = "CR LF" Or SaltoLinea = "" Then
                        sw.Write(strExport & Environment.NewLine.ToString())
                    ElseIf SaltoLinea = "LF" Then
                        sw.Write(strExport & vbLf)
                    End If

                End If
                Dim Cols As Integer = data.Columns.Count
                Dim rows As Integer = data.Rows.Count
                Dim linea(rows, Cols) As String
                Dim c_Rows As Integer = -1
                Dim c_Cells As Integer
                For Each row In data.Rows
                    c_Rows = c_Rows + 1
                    c_Cells = -1
                    Dim R As System.Data.DataRow = CType(row, System.Data.DataRow)
                    strExport = ""
                    While c_Cells < Cols - 1
                        c_Cells = c_Cells + 1
                        Dim valor As String = R(c_Cells).ToString
                        'linea(c_Rows, c_Cells) = valor
                        If valor IsNot Nothing Then
                            strExport += valor
                        Else
                            strExport += ""
                        End If
                        If c_Cells < Cols - 1 Then
                            strExport += separator
                        End If
                    End While
                    If SaltoLinea = "CR LF" Or SaltoLinea = "" Then
                        sw.Write(strExport & Environment.NewLine.ToString())
                    ElseIf SaltoLinea = "LF" Then
                        sw.Write(strExport & vbLf)
                    End If
                Next
                sw.Flush()
                byteArray = ms.GetBuffer()
                Session("byteArray") = byteArray
                Session("byteArrayLength") = byteArray.Length
            Catch ex As Exception
                Throw
            End Try
        End Using
    End Sub
    Public Shared Sub ExportSlygGridViewToCSV(ByRef data As System.Data.DataTable, ByRef archivo As FileStream, Optional ByVal separator As String = ",", Optional ByVal Encabezado As Boolean = True, Optional ByVal SaltoLinea As String = "")
        System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US")
        Using sw As New StreamWriter(archivo, Encoding.UTF8)
            Dim strExport As String = ""
            Dim final As Integer = 0
            Try
                If Encabezado Then
                    final = 1
                    'Export the Columns to Header excel file
                    For Each dcol In data.Columns
                        If final < data.Columns.Count Then
                            strExport += CType(dcol, System.Data.DataColumn).ColumnName + separator
                            final += 1
                        Else
                            strExport += CType(dcol, System.Data.DataColumn).ColumnName
                        End If
                    Next
                    If SaltoLinea = "CR LF" Or SaltoLinea = "" Then
                        sw.Write(strExport & Environment.NewLine.ToString())
                    ElseIf SaltoLinea = "LF" Then
                        sw.Write(strExport & vbLf)
                    End If
                End If
                Dim Cols As Integer = data.Columns.Count
                Dim rows As Integer = data.Rows.Count
                Dim linea(rows, Cols) As String
                Dim c_Rows As Integer = -1
                Dim c_Cells As Integer
                For Each row In data.Rows
                    c_Rows = c_Rows + 1
                    c_Cells = -1
                    Dim R As System.Data.DataRow = CType(row, System.Data.DataRow)
                    strExport = ""
                    While c_Cells < Cols - 1
                        c_Cells = c_Cells + 1
                        Dim valor As String = R(c_Cells).ToString
                        'linea(c_Rows, c_Cells) = valor
                        If valor IsNot Nothing Then
                            strExport += valor
                        Else
                            strExport += ""
                        End If
                        If c_Cells < Cols - 1 Then
                            strExport += separator
                        End If
                    End While
                    If SaltoLinea = "CR LF" Or SaltoLinea = "" Then
                        sw.Write(strExport & Environment.NewLine.ToString())
                    ElseIf SaltoLinea = "LF" Then
                        sw.Write(strExport & vbLf)
                    End If
                Next
                sw.Close()
            Catch ex As Exception
                Throw
            End Try
        End Using
    End Sub
    Public Sub ExportDataSet_To_Excel(Data As System.Data.DataSet, pathFile As String, Optional ByVal Msg As Boolean = True)
        Dim DataTableMain As New System.Data.DataTable
        Try
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US")
            Dim oExcel As Excel.Application
            Dim oBook As Excel.Workbook
            Dim oSheet As Excel.Worksheet
            Dim oRange As Microsoft.Office.Interop.Excel.Range
            Dim rows As Integer = Data.Tables(0).Rows.Count + 1
            Dim Cols As Integer = Data.Tables(0).Columns.Count

            oExcel = CType(CreateObject("Excel.Application"), Excel.Application)
            oBook = oExcel.Workbooks.Add(Type.Missing)
            ' Crea nueva hoja y asigna nombre
            oSheet = CType(oBook.Worksheets(1), Excel.Worksheet)
            oSheet.Name = Titulo

            DataTableMain = Data.Tables(0)
            Dim colIndex As Integer = 0
            Dim rowIndex As Integer = 0

            'Export the Columns to Header excel file
            For Each dc As Data.DataColumn In DataTableMain.Columns
                colIndex = colIndex + 1
                oSheet.Cells(1, colIndex) = dc.ColumnName
            Next

            'Propiedades Header
            oRange = oSheet.Range(oSheet.Cells(1, 1), oSheet.Cells(1, Cols))
            With oRange
                .Columns.AutoFit()
                .Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                .Borders.Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin
                .Borders.ColorIndex = Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic
                .Font.Bold = True
                .Interior.Color = RGB(128, 128, 128)
                '.Font.ColorIndex = 2 ' BLANCO
            End With

            'Agrega contenido en arreglo para insertar a excel
            Dim linea(rows, Cols) As String
            Dim c_Rows As Integer = -1
            Dim c_Cells As Integer

            For Each item As DataRow In DataTableMain.Rows
                c_Rows = c_Rows + 1
                c_Cells = -1
                For Each value As Object In item.ItemArray
                    c_Cells = c_Cells + 1
                    linea(c_Rows, c_Cells) = value.ToString
                Next
            Next

            'Inserta contenido de detalles a excel de acuerdo al rango establecido
            oRange = oSheet.Range(oSheet.Cells(2, 1), oSheet.Cells(rows, Cols))
            With oRange
                .Value = linea
            End With

            'Propiedades Detalles
            oRange = oSheet.Range(oSheet.Cells(2, 1), oSheet.Cells(rows, Cols))
            With oRange
                .Columns.AutoFit()
                .Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                .Borders.Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin
                .Borders.ColorIndex = Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic
                .Font.Bold = False
            End With

            'AutoAjusta las columnas
            oSheet.Columns.AutoFit()
            'Save file in final path in format (.xlsx)
            oBook.SaveAs(pathFile, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing)

            'Release the objects
            ReleaseObject(oSheet)
            oBook.Close(False, Type.Missing, Type.Missing)
            ReleaseObject(oBook)
            oExcel.Quit()
            ReleaseObject(oExcel)
            GC.Collect()

            If Msg Then
                'MensajeLabel.Text = "Archivo Excel ha sido generado correctamente en la ruta: " & pathFile & "."
                MensajeLabel.Text = "¡Archivo Excel ha sido generado correctamente!"
            End If

        Catch ex As Exception
            MensajeLabel.Text = "Error al exportar a Excel" + " " + ex.ToString
        End Try

    End Sub
    Public Sub ExportDataGridView_To_Excel(ByVal data As System.Data.DataTable, ByVal pathFile As String)
        Try
            Session("DatosReporte") = data
            Session("NombreArchivoExportacion") = pathFile
            If ButtonOnClickReportMas_ And contarReportes = 1 Then
                MensajeLabel.Text = "¡Archivos Excel han sido generados correctamente en la ruta!"
            End If
        Catch ex As Exception
            MensajeLabel.Text = "Error al exportar a Excel" + " " + ex.ToString()
        End Try
    End Sub
    Public Sub ExportDataGridView_To_Excel(ByVal data As System.Data.DataTable, pathFile As String, ByVal Msg As Boolean)

        Try
            'Dim ruta = Directory.GetCurrentDirectory
            Dim ruta = Server.MapPath("~/_temporal/")
            Dim FileName As String = Path.Combine(ruta, pathFile)

            If File.Exists(FileName) Then
                File.Delete(FileName)
            End If

            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US")
            Dim oExcel As Excel.Application
            Dim oBook As Excel.Workbook
            Dim oSheet As Excel.Worksheet
            Dim objRango As Microsoft.Office.Interop.Excel.Range
            Dim rows As Integer = data.Rows.Count + 1
            Dim Cols As Integer = data.Columns.Count

            oExcel = CType(CreateObject("Excel.Application"), Excel.Application)
            oBook = oExcel.Workbooks.Add(Type.Missing)
            ' Crea nueva hoja y asigna nombre
            oSheet = CType(oBook.Worksheets(1), Excel.Worksheet)
            If Titulo.Length > 20 Then
                oSheet.Name = Titulo.Substring(0, 20)
            Else
                oSheet.Name = Titulo
            End If

            Dim colIndex As Integer = 0
            Dim rowIndex As Integer = 0

            'Export the Columns to Header excel file
            For Each dcol In data.Columns
                colIndex = colIndex + 1
                oSheet.Cells(1, colIndex) = CType(dcol, System.Data.DataColumn).ColumnName
            Next

            'Propiedades Header
            objRango = oSheet.Range(oSheet.Cells(1, 1), oSheet.Cells(1, Cols))
            With objRango
                .Columns.AutoFit()
                .Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                .Borders.Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin
                .Borders.ColorIndex = Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic
                .Font.Bold = True
                .Interior.Color = RGB(128, 128, 128)
                '.Font.ColorIndex = 2 ' BLANCO
            End With

            'Agrega contenido en arreglo para insertar a excel
            Dim linea(rows, Cols) As String
            Dim c_Rows As Integer = -1
            Dim c_Cells As Integer

            For Each row In data.Rows
                c_Rows = c_Rows + 1
                c_Cells = -1
                Dim R As System.Data.DataRow = CType(row, System.Data.DataRow)
                While c_Cells < Cols - 1
                    c_Cells = c_Cells + 1
                    Dim valor As String = R(c_Cells).ToString
                    linea(c_Rows, c_Cells) = valor
                End While
            Next

            'Inserta contenido de detalles a excel de acuerdo al rango establecido
            objRango = oSheet.Range(oSheet.Cells(2, 1), oSheet.Cells(rows, Cols))
            With objRango
                .Value = linea
            End With

            'Propiedades Detalles
            objRango = oSheet.Range(oSheet.Cells(2, 1), oSheet.Cells(rows, Cols))
            With objRango
                .Columns.AutoFit()
                .Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                .Borders.Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin
                .Borders.ColorIndex = Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic
                .Font.Bold = False
            End With

            'AutoAjusta las columnas
            oSheet.Columns.AutoFit()
            'Save file in final path in format (.xlsx)

            oBook.SaveAs(FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing)
            _tempPath = FileName
            'Release the objects
            ReleaseObject(oSheet)
            oBook.Close(False, Type.Missing, Type.Missing)
            ReleaseObject(oBook)
            oExcel.Quit()
            ReleaseObject(oExcel)
            GC.Collect()

            If Msg Then
                If ButtonOnClickReportMas_ And contarReportes = 1 Then
                    MensajeLabel.Text = "Archivos Excel han sido generados correctamente en la ruta: " & GuardaArchivo_ & "."
                End If
            Else
                MensajeLabel.Text = "Archivo Excel ha sido generado correctamente en la ruta: " & pathFile & "."
            End If

        Catch ex As Exception
            MensajeLabel.Text = "Error al exportar a Excel" + " " + ex.ToString()
        End Try

    End Sub
    Protected Sub Download_File(ByVal Opcion As Integer)
        Try
            Dim url As String = "window.open('../../_temporal/" + Path.GetFileName(Me._tempPath) + "','_blank','toolbar=no,scrollbars=no,resizable=no,top=500,left=500,width=300,height=300');"
            Dim sUrl As String = "../../_controls/DescargaArchivoExcel.aspx"
            Dim script = String.Format("window.open('" & sUrl & "',null,'toolbar=0,scrollbars=1,location=0,statusbar=0,menubar=0,resizable=1,width=400,height=300,left=550,top=400');")
            If Opcion = 1 Then
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "popup", script, True)
            Else
                ToolkitScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenWindow", url, True)
            End If
        Catch ex As Exception
            MensajeLabel.Text = ex.ToString
        End Try

    End Sub
    Private Sub ReleaseObject(ByVal o As Object)
        Try
            While (System.Runtime.InteropServices.Marshal.ReleaseComObject(o) > 0)
            End While
        Catch
        Finally
            o = Nothing
        End Try
    End Sub

    Public Sub LoadConfigReport()
        Dim dbmCore As New DBCore.DBCoreDataBaseManager(Me.ConnectionString_Core)

        Try
            dbmCore.Connection_Open(1)
            Dim Usa_Exportar_Masivo As Boolean
            Dim TablaReporte = dbmCore.SchemaConfig.TBL_Reporte.DBGet(IdReporte)
            For Each UsaMasivo In TablaReporte
                If UsaMasivo.IsUsa_Exportar_MasivoNull Then
                    Usa_Exportar_Masivo = False
                    If TablaReporte.Rows.Count > 0 Then
                        Try
                            'Formato de generacion de archivo
                            TipoFormatoArchivo = TablaReporte(0).fk_Formato
                        Catch
                            TipoFormatoArchivo = 0
                        End Try
                        Try
                            'Caracter de separacion (CSV)
                            CaracterSeparacion = TablaReporte(0).Caracter_Separado
                        Catch
                            CaracterSeparacion = ","
                        End Try
                        Try
                            'Maneja encabezado, plano CSV
                            ManejaEncabezado = TablaReporte(0).Maneja_Encabezado
                        Catch
                            ManejaEncabezado = True
                        End Try
                        Try
                            'ID de notificacion de correo
                            idNotificacion = TablaReporte(0).fk_notificacion
                        Catch
                            idNotificacion = 0
                        End Try
                        Try
                            'Indica si el archivo generado debe ir Zipiado
                            Genera_ZIP_Archivo = CType(TablaReporte(0).Genera_ZIP_Reporte, String)
                        Catch
                            Genera_ZIP_Archivo = "0"
                        End Try
                        Try
                            Dim clave As Byte()
                            'Indica clave de encripcion del zip generado
                            If CType(Genera_ZIP_Archivo, Boolean) Then
                                'Clave binario
                                clave = TablaReporte(0).Clave_Reporte
                                ClaveArchivo = Decrypt(clave)
                            Else
                                ClaveArchivo = Nothing
                            End If
                        Catch
                            ClaveArchivo = Nothing
                        End Try
                    End If
                Else

                    Usa_Exportar_Masivo = UsaMasivo.Usa_Exportar_Masivo
                    If Usa_Exportar_Masivo Then
                        Me.ExportarImageButton.Visible = False
                        If UsaMasivo.Usa_Exportar_Masivo Then
                            If TablaReporte.Rows.Count > 0 Then
                                Try
                                    'Formato de generacion de archivo
                                    TipoFormatoArchivo = 0
                                Catch
                                    TipoFormatoArchivo = 0
                                End Try
                                Try
                                    'Caracter de separacion (CSV)
                                    CaracterSeparacion = "0"

                                Catch
                                    CaracterSeparacion = ","
                                End Try
                                Try
                                    'Maneja encabezado, plano CSV
                                    ManejaEncabezado = False
                                Catch
                                    ManejaEncabezado = True
                                End Try
                                Try
                                    'ID de notificacion de correo
                                    idNotificacion = 0
                                Catch
                                    idNotificacion = 0
                                End Try
                                Try
                                    'Indica si el archivo generado debe ir Zipiado
                                    Genera_ZIP_Archivo = "0"
                                Catch
                                    Genera_ZIP_Archivo = "0"
                                End Try
                                Try
                                    Dim clave As Byte()
                                    'Indica clave de encripcion del zip generado
                                    If CType(Genera_ZIP_Archivo, Boolean) Then
                                        'Clave binario
                                        clave = TablaReporte(0).Clave_Reporte
                                        ClaveArchivo = Decrypt(clave)
                                    Else
                                        ClaveArchivo = Nothing
                                    End If
                                Catch
                                    ClaveArchivo = Nothing
                                End Try
                            End If

                        End If
                    End If
                End If
            Next
        Catch ex As Exception
            MensajeLabel.Text = "Error al conectarse a Core" + " " + ex.ToString()
            Return
        Finally
            If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
        End Try
    End Sub
    Public Function GetFile(ByVal FileName As String) As Byte()
        Dim fsInput As New FileStream(FileName, FileMode.Open, FileAccess.Read)
        Dim Longitud As Integer = CInt(fsInput.Length - 1)
        Dim Data(Longitud) As Byte
        fsInput.Read(Data, 0, Data.Length)
        fsInput.Close()
        Return Data
    End Function
    Private Sub BorrarTemporal(ByVal TempPath As String)
        Try
            Dim objDirectoryInfo = New DirectoryInfo(TempPath)
            Dim fileInfoArray As FileInfo() = objDirectoryInfo.GetFiles()
            Dim objFileInfo As FileInfo
            For Each objFileInfo In fileInfoArray
                objFileInfo.Delete()
            Next objFileInfo
            objDirectoryInfo.Delete(True)
        Catch ex As Exception
        End Try
    End Sub
    Public Shared Function Encrypt(ByVal nData As String) As Byte()
        Return Crypto.DPAPI.Encrypt(nData, MemoryProtectionScope.CrossProcess, 51)
    End Function
    Public Shared Function Decrypt(ByVal nData() As Byte) As String
        Return Crypto.DPAPI.Decrypt(nData, MemoryProtectionScope.CrossProcess, 51)
    End Function
#End Region
#Region " PROCEDIMIENTOS "
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Cargar_Opciones()
        End If
    End Sub
    Private Sub Cargar_Opciones()
        Cargar_Separador()
        Cargar_Formato()
    End Sub
    Private Sub Cargar_Separador()
        SeparadorDropDownList.Items.Clear()
        SeparadorDropDownList.Items.Add(New System.Web.UI.WebControls.ListItem("Coma (,)", "Coma"))
        SeparadorDropDownList.Items.Add(New System.Web.UI.WebControls.ListItem("Punto y Coma (;)", "PuntoyComa"))
        SeparadorDropDownList.Items.Add(New System.Web.UI.WebControls.ListItem("Tabulador ([Tab])", "Tabulador"))
        SeparadorDropDownList.Items.Add(New System.Web.UI.WebControls.ListItem("Vacio ()", "Vacio"))
        SeparadorDropDownList.SelectedIndex = 0
    End Sub
    Private Sub Cargar_Formato()
        FormatoDropDownList.Items.Clear()
        'FormatoDropDownList.Items.Add(New System.Web.UI.WebControls.ListItem("Excel (.xlsx)", "Excel"))
        FormatoDropDownList.Items.Add(New System.Web.UI.WebControls.ListItem("Excel (.xls)", "Excel"))
        FormatoDropDownList.Items.Add(New System.Web.UI.WebControls.ListItem("Texto (.txt)", "Texto"))
        FormatoDropDownList.SelectedIndex = 0
    End Sub
    Public Sub DataSource(ByVal DataSource As System.Data.DataSet)
        For Each tabla In DataSource.Tables
            'Dim Cols As Integer = CType(tabla, System.Data.DataTable).Columns.Count
            dat_table = CType(tabla, System.Data.DataTable)
        Next
        ResultadoReporteSlygGridViewControl.DataSource = DataSource
        ResultadoReporteSlygGridViewControl.Visible = True
        ResultadoReporteSlygGridViewControl.DataBind()
        ExportarImageButton.Visible = True
        ViewState("dat_table") = dat_table
    End Sub
    Protected Sub ExportarImageButton_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ExportarImageButton.Click
        'Acá lo que hace es bajar el archivo
        Dim ManejaEncabezado As Boolean = False
        Dim Separador_Coma As Boolean = False
        Dim Separador_Tabulador As Boolean = False
        Dim Separador_PuntoyComa As Boolean = False
        Dim Separador_Vacio As Boolean = False
        Dim Format_Excel As Boolean = False
        Dim Format_Texto As Boolean = False
        Dim Opcion As Integer = 0
        ManejaEncabezado = EncabezadoCheckBox.Checked
        Select Case SeparadorDropDownList.SelectedItem.Value
            Case "Coma"
                Separador_Coma = True
            Case "PuntoyComa"
                Separador_PuntoyComa = True
            Case "Tabulador"
                Separador_Tabulador = True
            Case "Vacio"
                Separador_Vacio = True
        End Select
        Select Case FormatoDropDownList.SelectedItem.Value
            Case "Excel"
                Format_Excel = True
            Case "Texto"
                Format_Texto = True
        End Select
        Opcion = Exportar(ManejaEncabezado, Separador_Coma, Separador_Tabulador, Separador_PuntoyComa, Separador_Vacio, Format_Excel, Format_Texto, Salto_linea_)
        Download_File(Opcion)
    End Sub
#End Region
End Class