Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports System.IO
Imports Miharu.FileProvider.Library
Imports System.Drawing
Imports System.Data.SqlClient
Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Library
Imports DBImaging.SchemaProcess
Imports DBImaging
Imports System.Windows.Forms
Imports Miharu.Tools.Progress
Imports Slyg.Tools.Imaging
Imports Slyg.Tools

Public Class FormCargueCredivalores

#Region " Propiedades "
    Private Entidad As Object
    Private Proyecto As Object
    Private NombreProyecto As String
    Private DataTableExpedientes As DataTable
    Private EsCargueLog As Boolean

    Sub New(nEntidad As Short, nProyecto As Short, nNombreProyecto As String)
        ' TODO: Complete member initialization
        Entidad = nEntidad
        Proyecto = nProyecto
        NombreProyecto = nNombreProyecto

        InitializeComponent()
    End Sub

    Property SelectedPath As String
        Get
            Return CarpetaSalidaTextBox.Text.TrimEnd("\"c) & "\"
        End Get
        Set(ByVal value As String)
            CarpetaSalidaTextBox.Text = value
        End Set
    End Property
#End Region
    Private Function Validar() As Boolean
        If (CarpetaSalidaTextBox.Text = "") Then
            DesktopMessageBoxControl.DesktopMessageShow("Debe seleccionar un directorio válido", "Directorio inválido", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
            CarpetaSalidaTextBox.Focus()
        Else
            Return True
        End If
        Return False
    End Function
#Region " Metodos "
    Private Sub SelectFolderButton_Click(sender As System.Object, e As System.EventArgs) Handles SelectFolderButton.Click
        SelectFolderPath()
    End Sub
    Private Sub SelectFolderPath()
        Dim LectorFolderBrowserDialog = New OpenFileDialog()

        LectorFolderBrowserDialog.InitialDirectory = "c:\\"
        LectorFolderBrowserDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"
        LectorFolderBrowserDialog.FilterIndex = 2

        LectorFolderBrowserDialog.RestoreDirectory = True
        If (LectorFolderBrowserDialog.ShowDialog() = DialogResult.OK) Then
            CarpetaSalidaTextBox.Text = LectorFolderBrowserDialog.FileName
        End If
    End Sub
    Private Sub CargarLogCredivalores()
        If Validar() Then
            Dim OutputFolder = CarpetaSalidaTextBox.Text.TrimEnd("\"c)
            Try
                Dim sr = New StreamReader(OutputFolder)
                Dim linea As String
                Dim lineas As List(Of String) = New List(Of String)
                Dim lineaActual As String()

                Dim columnnames As New List(Of String)
                columnnames.Add("Carpeta")
                'columnnames.Add("Llave_01")
                'columnnames.Add("Llave_02")

                Dim dt As DataTable = Nothing
                While Not sr.EndOfStream
                    linea = sr.ReadLine()
                    lineas.Add(linea)
                End While
                dt = New DataTable()

                For Each col As String In columnnames
                    dt.Columns.Add(col)
                Next
                Dim Cuenta As Integer = 1
                Dim TableLogs As DataTable = Nothing
                Dim RowLogs As DataRow = Nothing
                TableLogs = New DataTable("Tbl_Logs")

                Dim ColumnCarpeta As DataColumn = New DataColumn("Carpeta")
                ColumnCarpeta.DataType = System.Type.GetType("System.String")

                'Dim ColumnLlave_01 As DataColumn = New DataColumn("Llave_01")
                'ColumnLlave_01.DataType = System.Type.GetType("System.String")

                'Dim ColumnLlave_02 As DataColumn = New DataColumn("Llave_02")
                'ColumnLlave_02.DataType = System.Type.GetType("System.String")

                'Dim fk_Entidad As DataColumn = New DataColumn("fk_Entidad")
                'fk_Entidad.DataType = System.Type.GetType("System.Int32")

                'Dim fk_Proyecto As DataColumn = New DataColumn("fk_Proyecto")
                'fk_Proyecto.DataType = System.Type.GetType("System.Int32")
                TableLogs.Columns.Add(ColumnCarpeta)

                'TableLogs.Columns.Add(fk_Entidad)
                'TableLogs.Columns.Add(fk_Proyecto)
                'TableLogs.Columns.Add(ColumnLlave_01)
                'TableLogs.Columns.Add(ColumnLlave_02)
                For p As Integer = 1 To lineas.Count - 1
                    lineaActual = lineas(p).Split(Convert.ToChar(vbTab))
                    ProgressBarReemplazar.Visible = True
                    Dim Porcentaje = (Cuenta / lineas.Count()) * 100

                    Dim row As DataRow = dt.NewRow()
                    row.ItemArray = lineaActual
                    Dim Carpeta As String = ""
                    'Dim Llave_01 As String = ""
                    'Dim Llave_02 As String = ""

                    If (IsDBNull(row.Item("Carpeta"))) Then
                        Carpeta = ""
                    Else
                        Carpeta = CStr(row.Item("Carpeta"))
                    End If

                    'If (IsDBNull(row.Item("Llave_01"))) Then
                    '    Llave_01 = ""
                    'Else
                    '    Llave_01 = CStr(row.Item("Llave_01"))
                    'End If
                    'If (IsDBNull(row.Item("Llave_02"))) Then
                    '    Llave_02 = ""
                    'Else
                    '    Llave_02 = CStr(row.Item("Llave_02"))
                    'End If
                    Try
                        RowLogs = TableLogs.NewRow()
                        'RowLogs.Item("fk_Entidad") = Entidad
                        'RowLogs.Item("fk_Proyecto") = Proyecto
                        If (String.IsNullOrEmpty(CStr(row.Item("Carpeta")))) Then
                            Carpeta = ""
                        Else
                            RowLogs.Item("Carpeta") = Carpeta
                        End If
                        'If (String.IsNullOrEmpty(CStr(row.Item("Llave_01")))) Then
                        '    Llave_01 = ""
                        'Else
                        '    RowLogs.Item("Llave_01") = Llave_01
                        'End If
                        'If (IsDBNull(row.Item("Llave_02"))) Then
                        '    Llave_02 = ""
                        'Else
                        '    RowLogs.Item("Llave_02") = Llave_02
                        'End If
                        TableLogs.Rows.Add(RowLogs)

                        ProgressBarReemplazar.Value = CInt(Porcentaje)
                        Dim percent As Integer = CInt(((CDbl(ProgressBarReemplazar.Value) / CDbl(ProgressBarReemplazar.Maximum)) * 100))
                        ProgressBarReemplazar.CreateGraphics().DrawString(percent.ToString() & "%", New Font("Arial", CSng(8.25), FontStyle.Regular), Brushes.White, New PointF(CSng(ProgressBarReemplazar.Width / 2 - 10), CSng(ProgressBarReemplazar.Height / 2 - 7)))
                        Cuenta = Cuenta + 1
                    Catch ex As Exception
                        Dim Mensaje As String = CStr(Carpeta) & vbTab & "Imagen :  NO"
                        'CStr(Llave_01) & vbTab & CStr(Llave_02) & vbTab & "Imagen :  NO"
                        log(CarpetaSalidaTextBox.Text.TrimEnd("\"c) & "\", "ErroresEX:" & ex.Message, Mensaje)
                    End Try
                Next
                sr.Close()
                Dim ResultTableLogs = TableLogs


                Dim Respuesta As DialogResult
                Respuesta = MessageBox.Show("Log cargado con éxito. " & vbCrLf & _
                                                "¿Desea Exportar esta información?", Program.AssemblyTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                ProgressBarReemplazar.Visible = False
                If Respuesta = DialogResult.Yes Then
                    Dim EscargueLog = True
                    Dim objCargueCredivalores As New Procesos.Exportar.FormExportCredivalores(CShort(Entidad), CShort(Proyecto), NombreProyecto, ResultTableLogs, EscargueLog)
                    objCargueCredivalores.ShowDialog()
                End If
            Catch ex As Exception
                log(CarpetaSalidaTextBox.Text.TrimEnd("\"c) & "\", "ErroresEX:" & ex.Message, ex.Message)
            End Try
        End If
    End Sub
    Private Sub AceptarButton_Click(sender As System.Object, e As System.EventArgs) Handles AceptarButton.Click
        CargarLogCredivalores()
    End Sub
    Public Shared Sub log(ByVal logFileDirectory As String, ByVal filenamePrefix As String, ByVal message As String)

        If Not Directory.Exists(logFileDirectory) Then
            Directory.CreateDirectory(logFileDirectory)
        End If

        Dim filepath As String = logFileDirectory & filenamePrefix & ".txt"
        Dim logMessage As String = message

        If Not File.Exists(filepath) Then

            Using sw As StreamWriter = File.CreateText(filepath)
                sw.WriteLine(logMessage)
            End Using
        Else

            Using sw As StreamWriter = File.AppendText(filepath)
                sw.WriteLine(logMessage)
            End Using
        End If
    End Sub
    Private Function InserTemporal(txtdatatable As DataTable, ByVal dbmImaging As DBImagingDataBaseManager) As Boolean
        Dim Valido As Boolean = False
        Const tabla As String = "##TempTableLogs"
        BulkInsert.InsertDataTableTemp(txtdatatable, dbmImaging, tabla)
        Valido = True
        Return Valido
    End Function
#End Region

End Class