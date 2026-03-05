Imports System.IO
Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopDataGridView
Imports System.Text
Imports System.IO.Directory
Imports System.Data
Imports Microsoft.Office.Interop.Excel
Imports Microsoft.Office.Interop
Imports Ionic.Zip
Imports System.Xml.Serialization
Imports System.Security.Cryptography
Imports System.ComponentModel
Imports Slyg.Tools
Imports Slyg.Tools.Cryptographic


Namespace DesktopReportDataGridView
    Public Class DesktopReportDataGridViewControl

#Region " Declaraciones "

        Private ConnectionString_Core As String
        Private ConnectionString_Tools As String
        Private IdReporte As Integer
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




        Public Property Titulo() As String
            Get
                Return EtiquetaToolStripLabel.Text
            End Get
            Set(ByVal value As String)
                EtiquetaToolStripLabel.Text = value
            End Set
        End Property

        Public Property InternalGridView() As DesktopDataGridView.DesktopDataGridViewControl
            Get
                Return ResultadoReporteDesktopDataGridViewControl
            End Get
            Set(ByVal value As DesktopDataGridView.DesktopDataGridViewControl)
                ResultadoReporteDesktopDataGridViewControl = value
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

        Public Shared ReadOnly Property AppPath As String
            Get
                Return System.Windows.Forms.Application.StartupPath.TrimEnd("\"c) + "\"
            End Get
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



        Shared Property TmpPath As String = "temp\ReportesMiharu\"

#End Region

#Region " Metodos "

        Public Sub Exportar(nEncabezado As Boolean, nComma As Boolean, nTab As Boolean, nSemicolom As Boolean, nBlank As Boolean, Excel As Boolean, Texto As Boolean, ncodificacionArhivo As String, Optional Salto_Linea As String = "")
            If ResultadoReporteDesktopDataGridViewControl.RowCount > 0 Then
                Dim GuardarFileDialog = New System.Windows.Forms.SaveFileDialog
                Dim sSeparador As String = ","
                Dim RutaFinal As String = ""
                Dim nombre As String = String.Empty


                Try

                    If Excel Then

                        'Generación Excel
                        GuardarFileDialog.Filter = "Archivo Excel (*.xlsx)|*.xlsx"
                        GuardarFileDialog.FileName = Titulo + ".xlsx"
                        nombre = Titulo + ".xlsx"
                        GuardarFileDialog.Title = "Guardar Reporte"

                        If ButtonOnClickReportMas_ Then
                            'Export XLSX
                            If Contadorshowmessage_ = 0 Then
                                If GuardarFileDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                                    'Export XLSX
                                    ExportDataGridView_To_Excel(ResultadoReporteDesktopDataGridViewControl, GuardarFileDialog.FileName)
                                    filename = _tempPath
                                    GuardaArchivo_ = filename.Replace(nombre, "")
                                End If
                            Else
                                GuardarFileDialog.FileName = GuardaArchivo_ + nombre
                                ExportDataGridView_To_Excel(ResultadoReporteDesktopDataGridViewControl, GuardarFileDialog.FileName)

                            End If
                        Else
                            If GuardarFileDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                                'Export XLSX
                                ExportDataGridView_To_Excel(ResultadoReporteDesktopDataGridViewControl, GuardarFileDialog.FileName)
                            End If
                        End If
                    ElseIf Texto Then

                        'Generación Texto (TXT)
                        GuardarFileDialog.Filter = "Archivo TXT (*.txt)|*.txt"
                        GuardarFileDialog.FileName = Titulo + ".txt"
                        nombre = Titulo + ".txt"
                        GuardarFileDialog.Title = "Guardar Reporte"
                        Dim rutanombre = GuardaArchivo_ + nombre
                        If ButtonOnClickReportMas_ Then
                            If Contadorshowmessage_ = 0 Then
                                If GuardarFileDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                                    If ResultadoReporteDesktopDataGridViewControl.RowCount > 0 Then
                                        Dim fs = CType(GuardarFileDialog.OpenFile(), FileStream)
                                        DataGridViewToCSV(ResultadoReporteDesktopDataGridViewControl, fs, "", False, Salto_Linea, ncodificacionArhivo)
                                        Archivo = "[" + fs.Name + "]"
                                        filename = fs.Name
                                        GuardaArchivo_ = filename.Replace(nombre, "")

                                    End If
                                End If
                            Else

                                If ResultadoReporteDesktopDataGridViewControl.RowCount > 0 Then
                                    Dim fs As FileStream = _
                                        New FileStream(rutanombre, FileMode.Create)
                                    DataGridViewToCSV(ResultadoReporteDesktopDataGridViewControl, fs, "", False, Salto_Linea, ncodificacionArhivo)
                                    Archivo = "[" + fs.Name + "]"
                                    If contarReportes = 1 Then
                                        DesktopMessageBox.DesktopMessageBoxControl.DesktopMessageShow("Archivos texto han sido generados correctamente en la ruta:" & GuardaArchivo_ & ".", "Reporte Exportado", Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                                    End If
                                End If
                            End If

                        Else
                            If GuardarFileDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then

                                Dim Archivo As String = String.Empty

                                If ResultadoReporteDesktopDataGridViewControl.RowCount > 0 Then
                                    Dim fs = CType(GuardarFileDialog.OpenFile(), FileStream)
                                    DataGridViewToCSV(ResultadoReporteDesktopDataGridViewControl, fs, "", False, Salto_Linea, ncodificacionArhivo)
                                    Archivo = "[" + fs.Name + "]"
                                End If

                                If Archivo <> String.Empty Then
                                    DesktopMessageBox.DesktopMessageBoxControl.DesktopMessageShow("Se exporto el reporte al archivo: " & Archivo & ".", "Reporte Exportado", Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                                End If
                            End If
                        End If
                    Else
                        'Generación CSV
                        GuardarFileDialog.Filter = "Archivo CSV (*.csv)|*.csv"
                        GuardarFileDialog.FileName = Titulo + ".csv"
                        nombre = Titulo + ".csv"
                        GuardarFileDialog.Title = "Guardar Reporte"

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
                                If GuardarFileDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then

                                    If ResultadoReporteDesktopDataGridViewControl.RowCount > 0 Then
                                        Dim fs = CType(GuardarFileDialog.OpenFile(), FileStream)
                                        DataGridViewToCSV(ResultadoReporteDesktopDataGridViewControl, fs, sSeparador, nEncabezado, Salto_Linea, ncodificacionArhivo)
                                        Archivo = "[" + fs.Name + "]"
                                        filename = fs.Name
                                        GuardaArchivo_ = filename.Replace(nombre, "")
                                    End If
                                End If
                            Else

                                Dim rutanombre = GuardaArchivo_ + nombre
                                Dim fs As FileStream = _
                                   New FileStream(rutanombre, FileMode.Create)
                                If ResultadoReporteDesktopDataGridViewControl.RowCount > 0 Then
                                    DataGridViewToCSV(ResultadoReporteDesktopDataGridViewControl, fs, sSeparador, nEncabezado, Salto_Linea, ncodificacionArhivo)
                                    Archivo = "[" + fs.Name + "]"
                                    If contarReportes = 1 Then
                                        DesktopMessageBox.DesktopMessageBoxControl.DesktopMessageShow("Archivos CSV han sido generados correctamente en la ruta:" & GuardaArchivo_ & ".", "Reporte Exportado", Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                                    End If
                                End If
                            End If
                        Else
                            If GuardarFileDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then

                                Dim Archivo As String = String.Empty

                                If ResultadoReporteDesktopDataGridViewControl.RowCount > 0 Then
                                    Dim fs = CType(GuardarFileDialog.OpenFile(), FileStream)
                                    DataGridViewToCSV(ResultadoReporteDesktopDataGridViewControl, fs, sSeparador, nEncabezado, Salto_Linea, ncodificacionArhivo)
                                    Archivo = "[" + fs.Name + "]"
                                End If

                                If Archivo <> String.Empty Then
                                    DesktopMessageBox.DesktopMessageBoxControl.DesktopMessageShow("Se exporto el reporte al archivo: " & Archivo & ".", "Reporte Exportado", Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                                End If
                            End If
                        End If
                    End If
                Catch ex As Exception
                    DesktopMessageBox.DesktopMessageBoxControl.DesktopMessageShow("Exportar", ex)
                End Try
            Else
                DesktopMessageBox.DesktopMessageBoxControl.DesktopMessageShow("No existen registros para exportar.", "No existen registros", Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
            End If
        End Sub

        Public Shared Sub DataGridViewToCSV(ByRef data As DesktopDataGridViewControl, ByRef archivo As FileStream, Optional ByVal separator As String = ",", Optional ByVal Encabezado As Boolean = True, Optional ByVal SaltoLinea As String = "", Optional ByVal nEncoding As String = "")
            Dim Cod As System.Text.Encoding

            Select Case nEncoding
                Case "UTF8"
                    Cod = New System.Text.UTF8Encoding(False)
                Case "ANSI"
                    Cod = Encoding.GetEncoding("Windows-1252")
                Case Else
                    Cod = System.Text.Encoding.Default
            End Select

            Using sw As New StreamWriter(archivo, Cod)
                Dim strExport As String = ""
                Dim final As Integer = 0
                Try
                    If Encabezado Then
                        final = 1
                        For Each dc As DataGridViewColumn In data.Columns
                            If final < data.ColumnCount Then
                                strExport += dc.Name + separator
                                final += 1
                            Else
                                strExport += dc.Name
                            End If
                        Next
                        If SaltoLinea = "CR LF" Or SaltoLinea = "" Then
                            sw.Write(strExport & Environment.NewLine.ToString())
                        ElseIf SaltoLinea = "LF" Then
                            sw.Write(strExport & vbLf)
                        End If


                    End If
                    For Each dr As DataGridViewRow In data.Rows
                        strExport = ""
                        final = 1
                        For Each dc As DataGridViewCell In dr.Cells
                            If dc.Value IsNot Nothing Then
                                strExport += dc.Value.ToString()
                            Else
                                strExport += ""
                            End If
                            If final < dr.Cells.Count Then
                                strExport += separator
                            End If
                            final += 1
                        Next
                        'strExport = strExport.Substring(0, strExport.Length - 3) & Environment.NewLine.ToString()
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

                oExcel = CreateObject("Excel.Application")
                oBook = oExcel.Workbooks.Add(Type.Missing)
                ' Crea nueva hoja y asigna nombre
                oSheet = oBook.Worksheets(1)
                oSheet.Name = Titulo

                DataTableMain = Data.Tables(0)
                Dim colIndex As Integer = 0
                Dim rowIndex As Integer = 0

                'Export the Columns to Header excel file
                For Each dc In DataTableMain.Columns
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
                Dim c_Rows As Long = -1
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
                    DesktopMessageBox.DesktopMessageBoxControl.DesktopMessageShow("Archivo Excel ha sido generado correctamente en la ruta: " & pathFile & ".", "Reporte Exportado", Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                End If

            Catch ex As Exception
                DesktopMessageBox.DesktopMessageBoxControl.DesktopMessageShow("Error al exportar a Excel", ex)
            End Try

        End Sub

        Public Sub ExportDataGridView_To_Excel(ByRef data As DesktopDataGridViewControl, pathFile As String, Optional ByVal Msg As Boolean = True)

            Try

                System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US")
                Dim oExcel As Excel.Application
                Dim oBook As Excel.Workbook
                Dim oSheet As Excel.Worksheet
                Dim objRango As Microsoft.Office.Interop.Excel.Range
                Dim rows As Integer = data.Rows.Count + 1
                Dim Cols As Integer = data.Columns.Count

                oExcel = CreateObject("Excel.Application")
                oBook = oExcel.Workbooks.Add(Type.Missing)
                ' Crea nueva hoja y asigna nombre
                oSheet = oBook.Worksheets(1)
                If Titulo.Length > 20 Then
                    oSheet.Name = Titulo.Substring(0, 20)
                Else
                    oSheet.Name = Titulo
                End If

                Dim colIndex As Integer = 0
                Dim rowIndex As Integer = 0

                'Export the Columns to Header excel file
                For Each dcol As DataGridViewColumn In data.Columns
                    colIndex = colIndex + 1
                    oSheet.Cells(1, colIndex) = dcol.Name
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
                Dim c_Rows As Long = -1
                Dim c_Cells As Integer

                For Each item As DataGridViewRow In data.Rows
                    c_Rows = c_Rows + 1
                    c_Cells = -1
                    For Each d_col As DataGridViewCell In item.Cells
                        c_Cells = c_Cells + 1
                        linea(c_Rows, c_Cells) = d_col.Value.ToString()
                    Next
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
                oBook.SaveAs(pathFile, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing)
                _tempPath = pathFile
                'Release the objects
                ReleaseObject(oSheet)
                oBook.Close(False, Type.Missing, Type.Missing)
                ReleaseObject(oBook)
                oExcel.Quit()
                ReleaseObject(oExcel)
                GC.Collect()

                If Msg Then
                    If ButtonOnClickReportMas_ And contarReportes = 1 Then
                        DesktopMessageBox.DesktopMessageBoxControl.DesktopMessageShow("Archivos Excel han sido generados correctamente en la ruta: " & GuardaArchivo_ & ".", "Reporte Exportado", Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                    End If
                Else
                    DesktopMessageBox.DesktopMessageBoxControl.DesktopMessageShow("Archivo Excel ha sido generado correctamente en la ruta: " & pathFile & ".", "Reporte Exportado", Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)

                End If

            Catch ex As Exception
                DesktopMessageBox.DesktopMessageBoxControl.DesktopMessageShow("Error al exportar a Excel", ex)
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
                                Genera_ZIP_Archivo = TablaReporte(0).Genera_ZIP_Reporte
                            Catch
                                Genera_ZIP_Archivo = False
                            End Try
                            Try
                                Dim clave As Byte()
                                'Indica clave de encripcion del zip generado
                                If Genera_ZIP_Archivo Then
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
                            Me.ExportarToolStripButton.Visible = False
                            If UsaMasivo.Usa_Exportar_Masivo Then
                                If TablaReporte.Rows.Count > 0 Then
                                    Try
                                        'Formato de generacion de archivo
                                        TipoFormatoArchivo = False
                                    Catch
                                        TipoFormatoArchivo = 0
                                    End Try
                                    Try
                                        'Caracter de separacion (CSV)
                                        CaracterSeparacion = False

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
                                        idNotificacion = False
                                    Catch
                                        idNotificacion = 0
                                    End Try
                                    Try
                                        'Indica si el archivo generado debe ir Zipiado
                                        Genera_ZIP_Archivo = False
                                    Catch
                                        Genera_ZIP_Archivo = False
                                    End Try
                                    Try
                                        Dim clave As Byte()
                                        'Indica clave de encripcion del zip generado
                                        If Genera_ZIP_Archivo Then
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
                DesktopMessageBox.DesktopMessageBoxControl.DesktopMessageShow("Error al conectarse a Core", ex)
                Return
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            End Try
        End Sub

        Private Function CreateZIPFile(ByVal FilePath As String, Optional ByVal EncryptPassword As String = Nothing) As String

            Dim ArchivoZip As String
            filename = Titulo & ".zip"
            Dim files() As String
            ArchivoZip = _tempPath & filename
            Dim Zip As New Ionic.Zip.ZipFile(ArchivoZip)
            If EncryptPassword <> Nothing And EncryptPassword <> "" Then
                'asigna clave a reporte
                Zip.Password = EncryptPassword
            End If

            files = Directory.GetFiles(_tempPath)
            Zip.AddFiles(files, False, "")
            Zip.Save()

            Return ArchivoZip
        End Function

        Public Function GetFile(ByVal FileName As String) As Byte()
            Dim fsInput As New FileStream(FileName, FileMode.Open, FileAccess.Read)
            Dim Longitud As Integer = CInt(fsInput.Length - 1)
            Dim Data(Longitud) As Byte

            fsInput.Read(Data, 0, Data.Length)
            fsInput.Close()

            Return Data
        End Function

        Public Sub EnviarCorreo()

            Dim attach() As Byte = Nothing
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing

            dbmCore = New DBCore.DBCoreDataBaseManager(Me.ConnectionString_Core)
            dbmCore.Connection_Open(1)

            '------------------Validacion listas para enviar correos--------------------
            Dim NotificacionDataTable = dbmCore.SchemaConfig.TBL_Notificacion.DBGet(idNotificacion)
            If NotificacionDataTable.Count = 1 Then
                Dim NotificacionListasDataTable = dbmCore.SchemaConfig.TBL_Notificacion_Lista.DBGet(NotificacionDataTable(0).id_Notificacion, 1)
                If NotificacionListasDataTable.Count = 1 Then
                    Dim CorreoDatatable = dbmCore.SchemaConfig.PA_Busqueda_Parametros_Correo.DBExecute(NotificacionDataTable(0).id_Notificacion, NotificacionListasDataTable(0).id_Notificacion_Lista)
                    If CorreoDatatable.Count = 1 Then
                        Dim Mensaje As String = ""
                        Dim EnvioCorreo As Boolean = False

                        Mensaje = CorreoDatatable(0).SALUDO & CorreoDatatable(0).CUERPO & CorreoDatatable(0).FIRMA

                        ' Crear archivo adjunto
                        Dim mimeType As String = ""
                        Dim encoding As String = ""
                        Dim fileNameExtension As String = ""
                        Dim streamids As String() = Nothing

                        Try
                            If Genera_ZIP_Archivo Then
                                attach = GetFile(CreateZIPFile(_tempPath, ClaveArchivo))
                            Else
                                attach = GetFile(_tempPath & filename)
                            End If
                        Catch
                        End Try

                        'realiza agendamiento de mail para envio
                        If SendMail(CorreoDatatable(0).CORREOS, "", "", CorreoDatatable(0).ASUNTO, Mensaje, filename, attach) Then
                            DesktopMessageBox.DesktopMessageBoxControl.DesktopMessageShow("El reporte ha sido enviado exitosamente.", "Envio correo - Reporte", Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                        Else
                            DesktopMessageBox.DesktopMessageBoxControl.DesktopMessageShow("Se ha presentado un error al generar y enviar el reporte", "Envio correo - Reporte", Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
                        End If

                    End If
                Else
                    DesktopMessageBox.DesktopMessageBoxControl.DesktopMessageShow("Error de configuración de los parámetros de listas de envió de correo, Por favor verifique con el administrador.", "Configuración parametros correo", Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
                End If
            Else
                DesktopMessageBox.DesktopMessageBoxControl.DesktopMessageShow("Error de configuración de los parámetros de envío de correo, Por favor verifique con el administrador.", "Configuración parametros correo", Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
            End If
            'Borra los archivos generados en la carpeta temporal.
            BorrarTemporal(_tempPath)
        End Sub

        Private Function SendMail(ByVal MailTo As String, ByVal MailCC As String, ByVal MailCCO As String, ByVal Subject As String, ByVal Message As String, nAttachName As String, nAttach As Byte()) As Boolean
            Dim DBMTools As DBTools.DBToolsDataBaseManager = Nothing
            Dim SendMailExitoso As Boolean = False

            Try
                DBMTools = New DBTools.DBToolsDataBaseManager(Me.ConnectionString_Tools)
                DBMTools.Connection_Open()

                DBMTools.InsertMail(2, 1, MailTo, MailCC, MailCCO, Subject, Message, nAttachName, nAttach)
                SendMailExitoso = True
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Envio de Correo", MessageBoxButtons.OK, MessageBoxIcon.Error)
                SendMailExitoso = False
            Finally
                DBMTools.Connection_Close()
            End Try
            Return SendMailExitoso
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

#Region " Eventos "

        Public Sub DesktopReportDataGridViewControl_Load(sender As Object, e As System.EventArgs) Handles Me.Load

            'carga la configuracion del reporte.
            LoadConfigReport()
            If CInt(idNotificacion) > 0 And CInt(TipoFormatoArchivo) <> 0 Then
                Me.EnvioCorreoToolStripButton.Visible = True
            End If

        End Sub

        Private Sub ExportarToolStripButton_Click(sender As System.Object, e As System.EventArgs) Handles ExportarToolStripButton.Click
            Dim ParametrosForm = New FormParametrosExportacion()
            If ParametrosForm.ShowDialog = System.Windows.Forms.DialogResult.OK Then
                Exportar(ParametrosForm.ManejaEncabezado, ParametrosForm.SeparadoComa, ParametrosForm.SeparadoTabulador, ParametrosForm.SeparadoPuntoyComa, ParametrosForm.SeparadoVacio, ParametrosForm.FormatExcel, ParametrosForm.FormatTexto, ParametrosForm.CodificacionArchivoComboBox.Text.ToString(), Salto_linea_)
            End If
        End Sub

        Private Sub EnvioCorreoToolStripButton_Click(sender As System.Object, e As System.EventArgs) Handles EnvioCorreoToolStripButton.Click
            Dim Respuesta = DesktopMessageBox.DesktopMessageBoxControl.DesktopMessageShow("¿Esta seguro que desea hacer el envio del reporte via e-mail", "Envio correo - Reportes", Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.IconEnum.AdvertencyIcon, False)
            If (Respuesta = DialogResult.OK) Then
                Dim fs As FileStream

                'Ruta temporal para almacenar reporte.
                _tempPath = AppPath & TmpPath & Guid.NewGuid().ToString() & "\"

                If (Not Directory.Exists(_tempPath)) Then
                    Directory.CreateDirectory(_tempPath)
                End If

                Select Case TipoFormatoArchivo
                    Case "1" 'Genera Plano
                        filename = Titulo & ".txt"
                        fs = File.Create(_tempPath & filename)
                        DataGridViewToCSV(ResultadoReporteDesktopDataGridViewControl, fs, vbTab, True)
                    Case "2" 'Genera CSV
                        filename = Titulo & ".csv"
                        fs = File.Create(_tempPath & filename)
                        DataGridViewToCSV(ResultadoReporteDesktopDataGridViewControl, fs, CaracterSeparacion, True)
                    Case "3" 'Genera Excel
                        filename = Titulo & ".xlsx"
                        ExportDataGridView_To_Excel(ResultadoReporteDesktopDataGridViewControl, _tempPath & filename, False)
                End Select

                'Realiza envio de correo
                EnviarCorreo()
            End If

        End Sub

#End Region

    End Class
End Namespace