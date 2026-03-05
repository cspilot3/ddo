Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.IO
Imports System.Text
Imports System.Data
Imports System.Data.OleDb
Imports System.Xml
Imports System.Web
Imports Microsoft.Office.Interop.Excel
Imports Microsoft.Office.Interop
Imports System.Reflection
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Linq

Public Class XLSData

#Region " Load XLS "

    Public Function ReadExcelIntoDataTable(ByVal FileName As String, ByVal nPathTempFileName As String, ByVal hasHeaders As Boolean, Optional ByVal Password As String = "", Optional ByRef dtref As System.Data.DataTable = Nothing, Optional ByRef dsref As DataSet = Nothing) As System.Data.DataTable
        Dim RetVal As New System.Data.DataTable
        Dim RetValDS As New System.Data.DataSet
        Dim contador As Integer = 0
        Dim Extension = System.IO.Path.GetExtension(FileName)
        Dim MissingValue = System.Reflection.Missing.Value
        Dim varFileNameString As String = Date.Now.Ticks.ToString() + Extension

        Dim app As Microsoft.Office.Interop.Excel.Application = New Microsoft.Office.Interop.Excel.Application()
        Dim workBook As Microsoft.Office.Interop.Excel.Workbook

        If (Extension.ToUpper() = ".XLSX") Then
            If (Password = "") Then
                nPathTempFileName = FileName
                workBook = app.Workbooks.Open(FileName)
            Else
                workBook = app.Workbooks.Open(FileName, MissingValue, MissingValue, MissingValue, Password:=Password, WriteResPassword:=Password)
            End If

        Else
            If (Password = "") Then
                workBook = app.Workbooks.Open(FileName)
            Else
                workBook = app.Workbooks.Open(FileName, 0, True, 5, Password, Password, True, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", False, False, 0, True, 1, 0)
            End If

        End If

        Dim workSheet As Microsoft.Office.Interop.Excel.Worksheet = workBook.ActiveSheet
        If (Password <> "") Then
            workBook.Unprotect(Password)
        End If

        If (Not File.Exists(nPathTempFileName)) Then
            nPathTempFileName = nPathTempFileName + varFileNameString
            workBook.SaveAs(nPathTempFileName, MissingValue, Password:="", WriteResPassword:="")
        End If

        RetValDS = ImportExcelXLS(nPathTempFileName, hasHeaders)

        If (dtref IsNot Nothing) Then
            If (RetValDS.Tables.Count > 0) Then
                dtref = RetValDS.Tables(0)
            End If
        ElseIf (dsref IsNot Nothing) Then
            If (RetValDS.Tables.Count > 0) Then
                dsref = RetValDS
            End If
        End If

        If (RetValDS.Tables.Count > 0) Then
            RetVal = RetValDS.Tables(0)
        End If

        workBook.Close(True, nPathTempFileName, False)
        System.Runtime.InteropServices.Marshal.FinalReleaseComObject(workSheet)
        System.Runtime.InteropServices.Marshal.FinalReleaseComObject(workBook)
        app.Quit()
        System.Runtime.InteropServices.Marshal.FinalReleaseComObject(app)

        If (Password <> "") Then
            If (File.Exists(nPathTempFileName)) Then
                File.Delete(nPathTempFileName)
            End If
        End If

        KillSpecificExcelFileProcess(varFileNameString)

        Return RetVal

    End Function

    Private Sub KillSpecificExcelFileProcess(excelFileName As String)
        Dim processes = From p In Process.GetProcessesByName("EXCEL") Select p

        For Each process__1 In processes
            If process__1.MainWindowTitle = Convert.ToString("Microsoft Excel - ") & excelFileName Then
                process__1.Kill()
            End If
        Next
    End Sub

    Public Function VerificaFilasVacias_ExcelWorkSheet(RangeValues As Object, ByVal MaxColum As Integer, ByVal rowIndex As Integer) As Boolean
        Dim contadorOK As Integer = 0
        Dim contadorErrado As Integer = 0
        Dim retorno As Boolean = True

        For Each itemValor In RangeValues
            If (contadorOK = MaxColum) Then
                Exit For
            End If

            If (contadorErrado = MaxColum) Then
                Return False
            End If

            If (itemValor Is Nothing) Then
                contadorErrado += 1
            End If

            contadorOK += 1
        Next

        Return retorno
    End Function


    Public Function ImportExcelXLS(ByVal FileName As String, ByVal hasHeaders As Boolean) As DataSet
        Dim HDR As String
        If hasHeaders Then HDR = "Yes" Else HDR = "No"
        Dim Extension = System.IO.Path.GetExtension(FileName)
        Dim strConn As String = ""

        Select Case (Extension.ToLower())
            Case ".xls"
                strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source='" & FileName & "';Extended Properties='Excel 8.0;HDR=" + HDR + ";'"
            Case ".xlsx"
                strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + FileName + "';Extended Properties='Excel 12.0 Xml;HDR=" + HDR + "';"
        End Select
        Dim output As New DataSet
        Dim conn As OleDbConnection = New OleDbConnection(strConn)

        With conn
            conn.Open()
            Dim dt As System.Data.DataTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, New Object() {Nothing, Nothing, Nothing, "TABLE"})
            Dim oda As OleDbDataAdapter
            For Each row As DataRow In dt.Rows
                Dim sheet As String = row("TABLE_NAME").ToString()

                If (Not sheet.ToUpper().Contains("PRINT_AREA") And Not sheet.ToUpper().Contains("PRINT_TITLES")) Then
                    Dim cmd As OleDbCommand = New OleDbCommand("SELECT * FROM [" & sheet & "]", conn)
                    cmd.CommandType = CommandType.Text

                    Dim outputTable As System.Data.DataTable = New System.Data.DataTable(sheet)
                    output.Tables.Add(outputTable)
                    oda = New OleDbDataAdapter(cmd)
                    oda.Fill(outputTable)

                    If (outputTable.Columns.Count > 0) Then
                        For Each itemColumn As DataColumn In outputTable.Columns
                            If (itemColumn.ColumnName.Trim() <> "") Then
                                itemColumn.ColumnName = itemColumn.ColumnName.Trim()
                            End If
                        Next
                    End If
                End If
                
            Next
            Return output
        End With
    End Function
#End Region

#Region " Save XLS "
    Public Shared Sub saveExcelXLS(ByVal DataTable As System.Data.DataTable, outputFilePath As String)
        Dim app As Microsoft.Office.Interop.Excel.Application = New Microsoft.Office.Interop.Excel.Application()
        Dim workbook As Excel.Workbook = app.Workbooks.Add()
        Dim worksheet As Worksheet = workbook.ActiveSheet

        Dim colIndex As Integer = 0
        Dim rowIndex As Integer = 0
        For Each dc In DataTable.Columns
            colIndex = colIndex + 1
            app.Cells(1, colIndex) = dc.ColumnName
        Next
        For Each actualRow In DataTable.Rows
            rowIndex = rowIndex + 1
            colIndex = 0
            For Each dc In DataTable.Columns
                colIndex = colIndex + 1
                app.Cells(rowIndex + 1, colIndex) = actualRow.Item(dc.ColumnName)
            Next
        Next

        worksheet.UsedRange.Columns.AutoFit()
        workbook.SaveAs(outputFilePath)
        app.Quit()
    End Sub
#End Region


End Class