Imports System.IO
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopDataGridView
Imports Miharu.Desktop.Controls.DesktopReportDataGridView

Namespace DesktopReportViewer

    Public Class DesktopReportViewerControl

#Region " Declaraciones "

        Private _ReportList As New List(Of DesktopReport)
        Private DataTableReporte As DataTable
        Private NombreReporte As String
#End Region

#Region " Propiedades "

        Public ReadOnly Property ReportList As List(Of DesktopReport)
            Get
                Return Me._ReportList
            End Get
        End Property

#End Region

#Region " Eventos "

        Private Sub ReportListComboBox_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles ReportListComboBox.KeyDown
            If e.KeyCode = Keys.Enter Then
                ShowReport()
            End If
        End Sub

        Private Sub ShowButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ShowButton.Click

            ShowReport()
            ReportListComboBox.SelectedItem = 0
        End Sub

#End Region

#Region " Metodos "

        Public Sub Load_Reports()
            'ReportListComboBox.Items.Clear()
            ReportListComboBox.Items.Add("- Selecionar reporte -")
            ReportListComboBox.SelectedIndex() = 0
            ReportListComboBox.DisplayMember = "ReportName"

            For Each Reporte In Me._ReportList
                ReportListComboBox.Items.Add(Reporte)
            Next
        End Sub

        Private Sub ShowReport()

            If (ReportListComboBox.SelectedIndex > 0) Then
                Dim Reporte = CType(ReportListComboBox.SelectedItem, DesktopReport)
                ShowReport(Reporte)
            Else
                MessageBox.Show("Debe seleccionar un reporte", "Reporte", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End Sub

        Public Sub ShowReport(ByVal nReport As DesktopReport)
            nReport.Launch()
        End Sub

        Public Sub Exportar(nEncabezado As Boolean, nComma As Boolean, nTab As Boolean, nSemicolom As Boolean, nBlank As Boolean, ncodificacionArhivo As String)
            Dim GuardarFileDialog = New System.Windows.Forms.SaveFileDialog
            Dim sSeparador As String = ","
            Dim nombre As String = String.Empty

            'Generación CSV
            GuardarFileDialog.Filter = "Archivo CSV (*.csv)|*.csv"
            GuardarFileDialog.FileName = NombreReporte
            nombre = NombreReporte
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

            If GuardarFileDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                Dim Archivo As String = String.Empty

                'If DataTableReporte.Rows.Count > 0 Then
                Dim fs = CType(GuardarFileDialog.OpenFile(), FileStream)
                DataGridViewToCSV(DataTableReporte, fs, sSeparador, nEncabezado, ncodificacionArhivo)
                Archivo = "[" + fs.Name + "]"
                'End If

                'If Archivo <> String.Empty Then
                DesktopMessageBox.DesktopMessageBoxControl.DesktopMessageShow("Se exportó el reporte al archivo: " & Archivo & ".", "Reporte Exportado", Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                'End If
            End If
        End Sub

        Public Shared Sub DataGridViewToCSV(ByRef data As DataTable, ByRef archivo As FileStream, Optional ByVal separator As String = ",", Optional ByVal Encabezado As Boolean = True, Optional ByVal nEncoding As String = "")
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
                Dim finalFilas As Integer = 0
                Try
                    If Encabezado Then
                        final = 1
                        For Each dc As DataColumn In data.Columns
                            If final < data.Columns.Count Then
                                strExport += dc.ColumnName + separator
                                final += 1
                            Else
                                strExport += dc.ColumnName
                            End If
                        Next

                        sw.Write(strExport & Environment.NewLine.ToString())
                    End If
                    finalFilas = 1
                    For Each dr As DataRow In data.Rows
                        strExport = ""
                        final = 1
                        For Each dc As DataColumn In data.Columns
                            strExport += dr(dc.ColumnName).ToString()
                            If final < data.Columns.Count Then
                                strExport += separator
                            End If
                            final += 1
                        Next

                        If finalFilas < data.Rows.Count Then
                            sw.Write(strExport & Environment.NewLine.ToString())
                        Else
                            sw.Write(strExport)
                        End If
                        finalFilas += 1
                    Next
                    sw.Close()
                Catch ex As Exception
                    Throw
                End Try
            End Using
        End Sub

        Private Sub lblExportarcsv_Click(sender As Object, e As EventArgs) Handles lblExportarcsv.Click
            Dim ParametrosForm = New FormParametrosExportacion2
            If ParametrosForm.ShowDialog = System.Windows.Forms.DialogResult.OK Then
                Exportar(ParametrosForm.ManejaEncabezado, ParametrosForm.SeparadoComa, ParametrosForm.SeparadoTabulador, ParametrosForm.SeparadoPuntoyComa, ParametrosForm.SeparadoVacio, ParametrosForm.CodificacionArchivoComboBox.Text.ToString())
            End If
        End Sub
#End Region

#Region " Funciones "
        Public Sub Inicializar(ByVal nDataTableReporte As DataTable, ByVal nNombreReporte As String)
            If nDataTableReporte IsNot Nothing AndAlso nNombreReporte <> "" Then
                DataTableReporte = nDataTableReporte
                NombreReporte = nNombreReporte
                lblExportarcsv.Enabled = True
            Else
                lblExportarcsv.Enabled = False
            End If
        End Sub
#End Region

    End Class
End Namespace