Imports System.Windows.Forms
Imports System.IO
Imports BarcodeLib.BarcodeReader
Imports System.Drawing
Imports DBAgrario.SchemaAudit

Namespace Firmas.Forms.Indices

    Public Class FormIndexGenerator

#Region " Declaraciones "

        Private oficinasDataView As DataView
        Private codTransaccionDataView As DataView

        Private _plugin As FirmasImagingPlugin

        Private Class Record
            Public Property FileName() As String
            Public Property CBarras_Leido() As String
            Public Property CBarras_Capturado() As String
            Public Property Message() As String            

            Public Sub New(nFileName As String)
                Me.FileName = nFileName
            End Sub

            Function getLine() As String
                Return """" & Me.FileName & """" & vbTab & """" & CStr(IIf(Me.CBarras_Capturado <> "", Me.CBarras_Capturado, Me.CBarras_Leido)) & """"
            End Function

        End Class

        Private Class Formater
            Private Property RTFControl() As RichTextBox

            Public Sub New(nRTFControl As RichTextBox)
                Me.RTFControl = nRTFControl
                Me.RTFControl.Text = ""
            End Sub

            Public Sub NewLine()
                Me.RTFControl.Text &= vbCrLf
                GotoEnd()
            End Sub

            Private Sub AddText(nText As String, nColor As Color, nBold As Boolean)
                Dim startPos = Me.RTFControl.TextLength
                Me.RTFControl.AppendText(nText)
                Dim endPos = Me.RTFControl.TextLength

                Me.RTFControl.Select(startPos, endPos - startPos)

                If nBold Then
                    Me.RTFControl.SelectionFont = New Font(Me.RTFControl.Font, FontStyle.Bold)
                Else
                    Me.RTFControl.SelectionFont = New Font(Me.RTFControl.Font, FontStyle.Regular)
                End If

                Me.RTFControl.SelectionColor = nColor

                Me.RTFControl.SelectionLength = 0

                GotoEnd()
            End Sub

            Public Sub AddLine(nText As String, nColor As Color, nBold As Boolean)
                AddText(nText, nColor, nBold)
                NewLine()
            End Sub

            Private Sub GotoEnd()
                Me.RTFControl.SelectionStart = Me.RTFControl.Text.Length
                Me.RTFControl.ScrollToCaret()
            End Sub
        End Class

#End Region

#Region " Eventos "

        Private Sub folderButton_Click(sender As System.Object, e As EventArgs) Handles folderButton.Click
            Me.pathTextBox.Text = Select_Folder(Me.pathTextBox.Text)
        End Sub

        Private Sub processButton_Click(sender As System.Object, e As EventArgs) Handles processButton.Click
            mainGroupBox.Enabled = False
            Procesar()
            mainGroupBox.Enabled = True
        End Sub

#End Region

#Region " Metodos "

        Public Sub New(nPlugin As FirmasImagingPlugin)
            InitializeComponent()

            _plugin = nPlugin
        End Sub

        Private Sub Procesar()
            If (Not Validar()) Then Return

            Dim log = New Formater(Me.logRichTextBox)
            Dim records = New List(Of Record)
            Dim fileNames = Directory.GetFiles(Me.pathTextBox.Text)

            mainProgressBar.Minimum = 0
            mainProgressBar.Value = 0
            mainProgressBar.Maximum = fileNames.Length

            Dim dbmAgrario As DBAgrario.DBAgrarioDataBaseManager = Nothing
            Try
                dbmAgrario = New DBAgrario.DBAgrarioDataBaseManager(_plugin.BancoAgrarioConnectionString)
                dbmAgrario.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                Dim oficinaDataTable = dbmAgrario.SchemaConfig.TBL_Oficina.DBGet(Nothing)
                oficinasDataView = New DataView(oficinaDataTable)

                Dim codTransaccionDataTable = dbmAgrario.SchemaFirmas.TBL_Transaccion.DBGet(Nothing)
                codTransaccionDataView = New DataView(codTransaccionDataTable)

            Catch ex As Exception
                log.AddLine("-----------------------------------------", Color.Red, False)
                log.AddLine("...", Color.Red, False)
                log.AddLine("Error:", Color.Red, False)
                log.AddLine(ex.Message, Color.Red, False)
                log.AddLine("-----------------------------------------", Color.Red, False)
                log.AddLine(ex.StackTrace, Color.Red, False)

                Return
            Finally
                If (dbmAgrario IsNot Nothing) Then dbmAgrario.Connection_Close()
            End Try

            Try
                log.NewLine()
                log.AddLine("-----------------------------------------", Color.Red, False)
                log.AddLine("Iniciando proceso..", Color.Black, False)
                log.AddLine("Ruta: " + Me.pathTextBox.Text, Color.Black, False)
                log.AddLine("-----------------------------------------", Color.Red, False)
                log.NewLine()

                For Each fileName In fileNames
                    Dim record = New Record(Path.GetFileName(fileName))
                    Dim msg As String = ""

                    Dim extension = Path.GetExtension(fileName).ToUpper()
                    If (extension <> ".JPG" And extension <> ".JPEG" And extension <> ".TIF") Then
                        log.AddLine("Omitir archivo: " + record.FileName, Color.Black, False)
                        Application.DoEvents()
                        mainProgressBar.Value += 1
                        Continue For
                    End If

                    log.AddLine("Validando archivo: " + record.FileName + "...", Color.Black, False)

                    Dim valido As Boolean = False
                    Dim Codigos = BarcodeReader.read(fileName, BarcodeReader.CODE128)

                    If (Codigos.Length > 0) Then
                        record.CBarras_Leido = Codigos(0)

                        If (Codigos.Length = 1) Then
                            ' Validar dígito de verificación
                            Dim digito1 As Integer = -1
                            Dim newCodigo As String = ""
                            Dim longitud As Integer = record.CBarras_Leido.Length
                            Dim oficina = String.Empty
                            Dim fecha = String.Empty
                            Dim codTransaccion = String.Empty

                            Select Case longitud
                                Case 44 ' Judiciales 
                                    ExtraerDigitoYCodigo(record.CBarras_Leido, 14, newCodigo, digito1)
                                    oficina = record.CBarras_Leido.Substring(13, 4)
                                    fecha = record.CBarras_Leido.Substring(32, 8)
                                    codTransaccion = record.CBarras_Leido.Substring(17, 5)

                                Case 38 ' Clientes
                                    ExtraerDigitoYCodigo(record.CBarras_Leido, 13, newCodigo, digito1)
                                    oficina = "9000"
                                    fecha = record.CBarras_Leido.Substring(28, 8)
                                    codTransaccion = record.CBarras_Leido.Substring(14, 3)

                                Case 28 ' Funcionarios
                                    ExtraerDigitoYCodigo(record.CBarras_Leido, 10, newCodigo, digito1)
                                    oficina = record.CBarras_Leido.Substring(11, 4)
                                    fecha = record.CBarras_Leido.Substring(20, 8)
                                    codTransaccion = record.CBarras_Leido.Substring(15, 5)

                                Case 12 ' Tapa
                                    digito1 = -2

                            End Select

                            If (digito1 >= 0) Then
                                Dim digito2 = FMDigitoVerif(newCodigo, longitud)

                                If (digito1 = digito2) Then

                                    If (ValidarOficina(oficina, msg)) Then
                                        If (ValidarFecha(fecha, msg)) Then
                                            If (ValidarCodTransaccion(codTransaccion, msg)) Then
                                                valido = True
                                            End If
                                        End If
                                    End If
                                Else
                                    msg = "Dígito de verificación inválido"
                                End If

                            ElseIf (digito1 = -1) Then
                                msg = "El código de barras inválido, la longitud no es válida"
                            ElseIf (digito1 = -2) Then
                                valido = True
                            End If
                        Else
                            msg = "Se detectó mas de un código de barras"
                        End If
                    Else
                        msg = "No se pudo leer el código de barras"
                    End If

                    record.Message = msg

                    If (Not valido) Then
                        log.AddLine(msg, Color.Red, False)
                        log.AddLine("Solicitando código manual...", Color.Black, False)

                        Dim previewForm = New FormPreview()

                        previewForm.rutaTextBox.Text = Me.pathTextBox.Text
                        previewForm.fileNameTextBox.Text = record.FileName
                        previewForm.errorTextBox.Text = msg
                        previewForm.cbarrasOldTextBox.Text = record.CBarras_Leido
                        previewForm.imagePictureBox.Image = New Bitmap(fileName)

                        Dim result = previewForm.ShowDialog()

                        If (result = DialogResult.OK) Then
                            record.CBarras_Capturado = previewForm.cbarrasNewTextBox.Text
                        Else
                            log.AddLine("-----------------------------------------", Color.Red, False)
                            log.AddLine("...", Color.Red, False)
                            log.AddLine("El proceso fue cancelado por el usuario", Color.Red, False)

                            Return
                        End If
                    Else
                        log.AddLine("Válido", Color.Green, False)
                    End If

                    records.Add(record)

                    log.NewLine()

                    Application.DoEvents()
                    mainProgressBar.Value += 1
                Next

                Try
                    dbmAgrario.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                    Dim idLog = dbmAgrario.SchemaAudit.PA_Firmas_Log_DAT_insert.DBExecute(Me.pathTextBox.Text, _plugin.Manager.Sesion.Usuario.id)

                    For i As Short = 0 To CShort(records.Count - 1)
                        Dim record = records(i)

                        dbmAgrario.SchemaAudit.TBL_Firmas_Log_DAT_Detalle.DBInsert(New TBL_Firmas_Log_DAT_DetalleType() With {
                                                                        .fk_Firmas_Log_DAT = idLog,
                                                                        .id_Firmas_Log_DAT_Detalle = i,
                                                                        .CBarras_Leido = record.CBarras_Leido,
                                                                        .CBarras_Capturado = record.CBarras_Capturado,
                                                                        .Mensaje_Error = record.Message
                                                                            })
                    Next

                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                Finally
                    If dbmAgrario IsNot Nothing Then dbmAgrario.Connection_Close()
                End Try

                Dim outputFilename = Me.pathTextBox.Text.TrimEnd("\"c) & "\index.dat"
                Using writer = New StreamWriter(outputFilename, False)
                    For Each record In records
                        writer.WriteLine(record.getLine())
                    Next

                    writer.Close()
                End Using



                log.AddLine("-----------------------------------------", Color.Green, False)
                log.AddLine("...", Color.Green, False)
                log.AddLine("El proceso se realizó con éxito", Color.Green, False)
                log.AddLine("Se generó el archivo:", Color.Green, False)
                log.AddLine(outputFilename, Color.Black, False)

                MessageBox.Show("El proceso se realizó con éxito", "Generar DAT", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Catch ex As Exception
                log.AddLine("-----------------------------------------", Color.Red, False)
                log.AddLine("...", Color.Red, False)
                log.AddLine("Error:", Color.Red, False)
                log.AddLine(ex.Message, Color.Red, False)
                log.AddLine("-----------------------------------------", Color.Red, False)
                log.AddLine(ex.StackTrace, Color.Red, False)
            End Try
        End Sub

#End Region

#Region " Funciones "

        Private Function Select_Folder(nPath As String) As String
            Dim selector = New FolderBrowserDialog

            selector.SelectedPath = nPath

            If (selector.ShowDialog() = DialogResult.OK) Then
                Return selector.SelectedPath
            Else
                Return nPath
            End If
        End Function

        Private Function Validar() As Boolean

            If (Me.pathTextBox.Text = "") Then
                MessageBox.Show("Debe seleccionar la ruta de cargue", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Me.pathTextBox.Focus()
            ElseIf (Not Directory.Exists(Me.pathTextBox.Text)) Then
                MessageBox.Show("La ruta de cargue no es válida", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Me.pathTextBox.Focus()
                Me.pathTextBox.SelectAll()
            Else
                Return True
            End If

            Return False
        End Function

        Private Function ValidarOficina(nOficina As String, ByRef nMessage As String) As Boolean
            If (Not Slyg.Tools.DataConvert.IsNumeric(nOficina)) Then
                nMessage = "Código de oficina no válido: " & nOficina
                Return False
            End If

            oficinasDataView.RowFilter = "id_Oficina = " & nOficina

            If (oficinasDataView.Count = 0) Then
                nMessage = "La oficina no existe: " & nOficina
                Return False
            End If

            Dim oficinaDataRow = CType(oficinasDataView(0).Row, DBAgrario.SchemaConfig.TBL_OficinaRow)
            If (Not oficinaDataRow.Activa) Then
                nMessage = "La oficina no se encuentra activa: " & nOficina
                Return False
            End If

            Return True
        End Function

        Private Function ValidarFecha(nFecha As String, ByRef nMessage As String) As Boolean
            Dim year = nFecha.Substring(0, 4)
            Dim month = nFecha.Substring(4, 2)
            Dim day = nFecha.Substring(6, 2)

            If (Not Slyg.Tools.DataConvert.IsDate(year & "/" & month & "/" & day, Slyg.Tools.DataConvert.EnumDateFormat.yyyyMMdd, "/"c)) Then
                nMessage = "La fecha no es válida: " & nFecha
                Return False
            End If

            Dim FechaCB = New DateTime(year, month, day)
            Dim FechaHoy = New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)

            If FechaCB > FechaHoy Then
                nMessage = "La Fecha de Movimiento no puede ser mayor al día de hoy"
                Return False
            End If

            Return True
        End Function

        Private Function ValidarCodTransaccion(nCodTransaccion As String, ByRef nMessage As String) As Boolean
            If (Not Slyg.Tools.DataConvert.IsNumeric(nCodTransaccion)) Then
                nMessage = "Código de transacción no es válido: " & nCodTransaccion
                Return False
            End If

            codTransaccionDataView.RowFilter = "Codigo_Transaccion = " & nCodTransaccion

            If (codTransaccionDataView.Count = 0) Then
                nMessage = "El código de transacción no existe: " & nCodTransaccion
                Return False
            End If

            Return True
        End Function

        Private Sub ExtraerDigitoYCodigo(ByVal nCBarras As String, ByVal nPosicion As Integer, ByRef nNewCBarras As String, ByRef nDigito As Integer)
            nNewCBarras = nCBarras.Substring(0, nPosicion) & "0" & nCBarras.Substring(nPosicion + 1, nCBarras.Length - nPosicion - 1)
            nDigito = CInt(nCBarras.Substring(nPosicion, 1))
        End Sub

        ''' <summary>
        ''' Método suministrado por el banco para validar el código de barras
        ''' </summary>
        ''' <param name="COdigoBarra">Código de barras a calcular dígito de verificación</param>
        ''' <param name="valor">Longitúd del código de barras a calcular</param>
        ''' <returns>Digito de verificación</returns>
        ''' <remarks></remarks>
        ''' 
        Function FMDigitoVerif(COdigoBarra As String, valor As Integer) As Integer
            Const VGTablaDigito As String = "716759534743413729231917130703"
            Dim VTSuma = 0
            Dim VTNumero As Integer
            Dim i As Integer
            Dim VTN As Integer
            Dim VTConstante As Integer
            For i = Len(COdigoBarra) To 1 Step -1
                VTNumero = CInt(Mid(COdigoBarra, i, 1))
                VTN = i + valor - Len(COdigoBarra)
                VTConstante = CInt(Val(Mid(VGTablaDigito, 2 * VTN, 1)) + 10 * Val(Mid(VGTablaDigito, 2 * VTN - 1, 1)))
                VTSuma = VTSuma + VTNumero% * VTConstante
            Next i%

            Dim VTDig = VTSuma Mod 11
            If VTDig > 1 Then
                VTDig = 11 - VTDig
            End If

            FMDigitoVerif = VTDig
        End Function

#End Region

    End Class

End Namespace