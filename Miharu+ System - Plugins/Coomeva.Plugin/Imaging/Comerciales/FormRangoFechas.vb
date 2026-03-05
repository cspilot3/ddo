Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library.Config
Imports System.IO
Namespace Imaging.Comerciales
    Public Class FormRangoFechas
#Region " Declaraciones "
        Public _plugin As Plugin = Nothing
        Public Property Fecha_Recaudo As DateTime
        Public Property Fecha_Recaudo2 As DateTime
        Public Property Matrices As Boolean
        Public Property RutaGeneral As String
        Public Property _Tipo As String
        Public Property _nombreReporte As String
        Public Property _tipoReporte As String
#End Region
#Region " Propiedades "
        Public Property FechaInicial() As Date
            Get
                'Return dtpFechaInicial.Value.Date.AddHours(16)
                Return dtpFechaInicial.Value.Date
            End Get
            Set(ByVal value As Date)
                dtpFechaInicial.Value = value
            End Set
        End Property
        Public Property FechaFinal() As Date
            Get
                'Return dtpFechaFinal.Value.Date.AddHours(40).AddMilliseconds(-10)
                Return dtpFechaFinal.Value.Date.AddHours(24).AddMilliseconds(-10)
            End Get
            Set(ByVal value As Date)
                dtpFechaFinal.Value = value
            End Set
        End Property
#End Region
#Region " Constructor "
        Public Sub New(ByVal nPlugin As Plugin)
            InitializeComponent()
            Me._plugin = nPlugin
            Me.lblRuta.Visible = True
            Me.RutaTextBox.Visible = True
            Me.SelectFolderButton.Visible = True
        End Sub
#End Region
#Region " Eventos "
        Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnCancelar.Click
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
            Me.Close()
        End Sub
        Private Sub FormRangoFechas_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
        End Sub
        Private Sub btnAceptar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAceptar.Click
            Dim dtReporte As DataTable = New DataTable()
            Dim dbmIntegration As New DBIntegration.DBIntegrationDataBaseManager(_plugin.CoomevaConnectionString)
            Dim strFechaInicio As String
            Dim strFechaFin As String
            Dim strFechaArchivo As String
            Try
                If Validar() Then
                    Me.DialogResult = System.Windows.Forms.DialogResult.OK
                    Me.Fecha_Recaudo = Me.dtpFechaInicial.Value
                    Me.Fecha_Recaudo2 = Me.dtpFechaFinal.Value
                    Me.RutaGeneral = Me.RutaTextBox.Text
                    strFechaInicio = Fecha_Recaudo.ToString("yyyyMMdd")
                    strFechaFin = Fecha_Recaudo2.ToString("yyyyMMdd")
                    strFechaArchivo = Fecha_Recaudo.ToString("dd-MM-yyyy")
                    dtReporte.Clear()
                    dbmIntegration.Connection_Open(_plugin.Manager.Sesion.Usuario.id)
                    dtReporte = dbmIntegration.SchemaCoomeva.PA_Reporte_Duplicados_Log.DBExecute(_plugin.Manager.ImagingGlobal.Entidad, _plugin.Manager.ImagingGlobal.Proyecto, strFechaInicio)
                    If dtReporte.Rows.Count() > 0 Then
                        _nombreReporte = String.Format(dbmIntegration.SchemaCoomeva.TBL_Config_Reporte.DBFindByNombre_Reporte("Registros_duplicados").FirstOrDefault().Formato_Reporte, DateTime.Parse(strFechaArchivo))
                        _tipoReporte = dbmIntegration.SchemaCoomeva.TBL_Config_Reporte.DBFindByNombre_Reporte("Registros_duplicados").FirstOrDefault().Extension_Salida
                        If Genera_ReporteExcel(RutaGeneral, _tipoReporte, "\" & _nombreReporte, dtReporte, "Hoja1") Then
                            MessageBox.Show("!Reporte Generado con Exito!")
                        End If
                        Me.Close()
                    Else
                        MessageBox.Show("!No se encontraron datos¡")
                    End If
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.[Error])
            End Try
        End Sub
        Private Sub SelectFolderButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SelectFolderButton.Click
            SelectFolderPath()
        End Sub
        Private Sub RutaTextBox_Click(ByVal sender As Object, ByVal e As EventArgs)
            SelectFolderPath()
        End Sub
        Protected Overrides Function ProcessCmdKey(ByRef msg As Message, ByVal keyData As Keys) As Boolean
            If keyData = Keys.Escape Then
                Me.Close()
                Return True
            End If
            Return MyBase.ProcessCmdKey(msg, keyData)
        End Function
        Private Sub frmFechaRecaudo_Load(ByVal sender As Object, ByVal e As EventArgs)
            Me.dtpFechaInicial.MaxDate = DateTime.Now
        End Sub
#End Region
#Region " Funciones "
        Private Function Validar() As Boolean
            Dim result As Boolean = True
            If Me.Matrices OrElse Me.SelectFolderButton.Visible = True Then
                If String.IsNullOrEmpty(RutaTextBox.Text) Then
                    DesktopMessageBoxControl.DesktopMessageShow("Debe Seleccionar Un directorio", "Validar", DesktopMessageBoxControl.IconEnum.AdvertencyIcon)
                    result = False
                End If
                If Not ValidarRuta() Then
                    result = False
                End If
            End If
            Return result
        End Function
        Private Function ValidarRuta() As Boolean
            If (String.IsNullOrEmpty(RutaTextBox.Text)) Then
                DesktopMessageBoxControl.DesktopMessageShow("Debe seleccionar un directorio válido", "Directorio inválido", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                RutaTextBox.Focus()
            ElseIf (Not Directory.Exists(RutaTextBox.Text)) Then
                DesktopMessageBoxControl.DesktopMessageShow("Debe seleccionar un directorio válido", "Directorio inválido", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                RutaTextBox.Focus()
                RutaTextBox.SelectAll()
            Else
                Return True
            End If
            Return False
        End Function
        Public Function Genera_Reporte_Plano(ByVal RutaDir As String, ByVal Extension As String, ByVal NombreReporte As String, ByVal DtFinal As DataTable) As Boolean
            Dim retorno As Boolean = True
            Try
                Dim archivo = RutaDir & "\" & NombreReporte & Extension
                If System.IO.File.Exists(archivo) Then System.IO.File.Delete(archivo)
                Using fileStream = System.IO.File.Create(archivo)
                    Dim writer As StreamWriter = New StreamWriter(fileStream)
                    For j As Integer = 0 To DtFinal.Rows.Count - 1
                        For i As Integer = 0 To DtFinal.Columns.Count - 1
                            writer.Write(DtFinal.Rows(j)(i).ToString())
                        Next
                        writer.WriteLine("")
                    Next
                    fileStream.Flush()
                    writer.Close()
                End Using
            Catch e As Exception
                MessageBox.Show(e.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.[Error])
                Return False
            End Try
            Return retorno
        End Function
        Private Function Genera_ReporteExcel(ByVal RutaDir As String, ByVal Extension As String, ByVal NombreReporte As String, ByVal DtFinal As DataTable, ByVal nombreHojaExcel As String) As Boolean
            Dim retorno As Boolean = True
            Try
                If (DtFinal Is Nothing Or DtFinal.Rows.Count = 0) Then
                    DtFinal = New DataTable
                    DtFinal.Columns.Add(" ")
                    Dim ItemsDinamicos As New List(Of String)
                    For Each row In DtFinal.Columns
                        ItemsDinamicos.Add(" ")
                    Next
                    DtFinal.Rows.Add(ItemsDinamicos.ToArray())
                End If
                'Creae an Excel application instance
                Dim excelApp As New Microsoft.Office.Interop.Excel.Application()
                Dim excel As Microsoft.Office.Interop.Excel.Application
                Dim worKbooK As Microsoft.Office.Interop.Excel.Workbook
                Dim worKsheeT As Microsoft.Office.Interop.Excel.Worksheet

                excel = New Microsoft.Office.Interop.Excel.Application()
                excel.Visible = False
                excel.DisplayAlerts = False
                worKbooK = excel.Workbooks.Add(Type.Missing)

                worKsheeT = DirectCast(worKbooK.ActiveSheet, Microsoft.Office.Interop.Excel.Worksheet)
                worKsheeT.Name = nombreHojaExcel
                worKsheeT.Cells.NumberFormat = "@"
                worKsheeT.Columns.NumberFormat = "@"
                worKsheeT.Rows.NumberFormat = "@"

                For i As Integer = 1 To DtFinal.Columns.Count
                    worKsheeT.Cells(1, i) = DtFinal.Columns(i - 1).ColumnName
                Next

                Dim dataMatriz = New Object(DtFinal.Rows.Count, DtFinal.Columns.Count - 1) {}
                Dim ContadorProgress As Integer = 2


                For j As Integer = 0 To DtFinal.Rows.Count - 1
                    For i As Integer = 0 To DtFinal.Columns.Count - 1
                        If ContadorProgress < 100 Then
                            'bw.ReportProgress(ContadorProgress)
                        End If

                        dataMatriz(j, i) = DtFinal.Rows(j)(i).ToString()
                        ContadorProgress += 2
                    Next
                Next
                ContadorProgress = 2

                worKsheeT.Cells.NumberFormat = "@"
                worKsheeT.Columns.NumberFormat = "@"
                worKsheeT.Rows.NumberFormat = "@"
                Dim startCell = CType(worKsheeT.Cells(2, 1), Microsoft.Office.Interop.Excel.Range)
                startCell.NumberFormat = "@"
                Dim endCell = CType(worKsheeT.Cells(DtFinal.Rows.Count + 1, DtFinal.Columns.Count), Microsoft.Office.Interop.Excel.Range)
                endCell.NumberFormat = "@"
                Dim writeRange = worKsheeT.Range(startCell, endCell)
                writeRange.NumberFormat = "@"
                writeRange.Value2 = dataMatriz

                worKbooK.SaveAs(RutaDir.Replace("\\", "\") + NombreReporte + Extension)
                worKbooK.Close()
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(worKsheeT)
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(worKbooK)
                excel.Quit()
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(excel)

            Catch e As Exception
                MessageBox.Show(e.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.[Error])
                Return False
            End Try
            Return retorno
        End Function
#End Region
#Region " Metodos "
        Private Sub SelectFolderPath()
            Dim LectorFolderBrowserDialog As FolderBrowserDialog = New FolderBrowserDialog()
            Dim Respuesta As DialogResult = Nothing
            LectorFolderBrowserDialog.SelectedPath = RutaTextBox.Text
            LectorFolderBrowserDialog.ShowNewFolderButton = False
            LectorFolderBrowserDialog.Description = "Seleccione la carpeta"
            Respuesta = LectorFolderBrowserDialog.ShowDialog()
            If (Respuesta = DialogResult.OK) Then
                RutaTextBox.Text = LectorFolderBrowserDialog.SelectedPath
            End If
        End Sub
#End Region
    End Class
End Namespace