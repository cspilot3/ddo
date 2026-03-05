Imports System.IO
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports System.Windows.Forms
Imports Slyg.Tools
Imports System.Drawing
Imports Miharu.Imaging.Indexer.View.Indexacion
Imports iTextSharp
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.pdf.parser
Imports DBImaging

Namespace ValidacionListas.Controls

    Public Class ListValidationControl
        Implements IListValidationControl

#Region " Declaraciones"
        Private Class Record
            Public Property FileName() As String

            Public Sub New(nFileName As String)
                Me.FileName = nFileName
            End Sub


        End Class

        Public _ValidacionListasController As ValidacionListas.Forms.Validaciones.ValidacionListasController

        Public _RespuestaFactiva As String
        Public _RespuestaContraloria As String
        Public _RespuestaProcuraduria As String

        Public _Ruta As String

#End Region

#Region " Propiedades "

        Property SelectedPath As String
            Get
                Return RutaTextBox.Text.TrimEnd("\"c) & "\"
            End Get
            Set(ByVal value As String)
                RutaTextBox.Text = value
            End Set
        End Property

#End Region

#Region " Implementacion IListValidationControl"

        Private Sub ValidarArchivos() Implements IListValidationControl.ValidarArchivos

            If Validar() Then
                Dim CedulaCapturada As String = _ValidacionListasController.CedulaCapturada
                Dim NombreCapturado As String = _ValidacionListasController.NombreCapturado.Replace(" ", "")
                Dim _NombreCapturado As String = _ValidacionListasController.NombreCapturado
                Dim Procuraduria As String = _ValidacionListasController.ValidarProcuraduria

                Dim CedulaLeida As String = ""
                Dim records = New List(Of Record)
                Dim fileNames = Directory.GetFiles(Me.RutaTextBox.Text)
                Dim msg As String = ""
                _Ruta = Me.RutaTextBox.Text

                Dim ArchivoProcuraduria As Boolean = False
                Dim ArchivoFactivaCedula As Boolean = False
                Dim ArchivoFactivaNombre As Boolean = False
                Dim ArchivoContraloriaCedula As Boolean = False
                Dim ArchivoContraloriaNombre As Boolean = False

                Dim j As Integer = 0
                For i = 0 To fileNames.Count - 1

                    Dim record = New Record(Path.GetFileName(fileNames(i)).ToUpper())

                    CedulaLeida = record.FileName.Substring(0, record.FileName.IndexOf("_"))
                    If CedulaLeida <> "" Then

                        Dim extension = Path.GetExtension(record.FileName).ToUpper()
                        If (extension <> ".PDF") Then

                            msg = msg & record.FileName & "; extension no valida." & vbCrLf
                        Else
                            If _ValidacionListasController.CedulaCapturada <> CedulaLeida Then

                                msg = msg & record.FileName & "; cedula no válida." & vbCrLf

                            Else
                                'Factiva Cedula
                                If record.FileName = CedulaCapturada & "_F_C" & extension Then
                                    If CedulaLeida <> "1" Then
                                        ArchivoFactivaCedula = True
                                        If Not (ReadPdfFile(fileNames(i), CedulaCapturada)) Then

                                            msg = msg & "Archivo " & record.FileName & "; no corresponde con la cedula a validar." & vbCrLf

                                        End If
                                    Else
                                        msg = msg & "Archivo " & record.FileName & "; no válido." & vbCrLf
                                    End If
                                End If
                                'Contraloria Cedula
                                If record.FileName = CedulaCapturada & "_C_C" & extension Then
                                    If CedulaLeida <> "1" Then
                                        ArchivoContraloriaCedula = True
                                        Dim Pasaportenumerico = LimpiarPasaporteContraloria(CedulaCapturada)
                                        If Not (ReadPdfFile(fileNames(i), Pasaportenumerico)) Then

                                            msg = msg & "Archivo " & record.FileName & "; no corresponde con la cedula a validar." & vbCrLf

                                        End If
                                    Else
                                        msg = msg & "Archivo " & record.FileName & "; no válido." & vbCrLf
                                    End If
                                End If

                                If (Procuraduria = "SI") Then
                                    If record.FileName = CedulaCapturada & "_P" & extension Then
                                        If CedulaLeida <> "1" Then
                                            ArchivoProcuraduria = True
                                        Else
                                            msg = msg & "Archivo " & record.FileName & "; no válido." & vbCrLf
                                        End If
                                    End If
                                Else
                                    If record.FileName = CedulaCapturada & "_P" & extension Then
                                        msg = msg & "Archivo " & record.FileName & "; no válido." & vbCrLf
                                    End If
                                End If

                                'Factiva Nombre
                                If record.FileName = CedulaCapturada & "_F_N" & extension Then
                                    ArchivoFactivaNombre = True
                                    If Not (ReadPdfFile(fileNames(i), NombreCapturado)) Then
                                        msg = msg & "Archivo " & record.FileName & "; no corresponde con el nombre a validar." & vbCrLf
                                    End If
                                End If

                                'Contraloria Nombre
                                If record.FileName = CedulaCapturada & "_C_N" & extension Then
                                    ArchivoContraloriaNombre = True
                                    Dim NombreLimpio = LimpiarNombreContraloria(_NombreCapturado)
                                    If Not (ReadPdfFile(fileNames(i), NombreLimpio)) Then
                                        msg = msg & "Archivo " & record.FileName & "; no corresponde con el nombre a validar." & vbCrLf
                                    End If

                                End If

                                _ValidacionListasController.AdicionarFolio(fileNames(i), j, j)
                                j += 1
                            End If

                        End If

                    End If

                Next

                'Validacion de existencia de archivos de requeridos
                If (CedulaLeida <> "") Then
                    If (CedulaLeida <> "1") Then
                        If (Procuraduria = "SI") Then
                            If Not (ArchivoProcuraduria) Then
                                msg = msg & "Falta archivo de Procuraduria." & vbCrLf
                            End If
                        End If

                        If Not (ArchivoFactivaCedula) Then
                            msg = msg & "Falta archivo de Factiva por Cedula." & vbCrLf
                        End If

                        If Not (ArchivoContraloriaCedula) Then
                            msg = msg & "Falta archivo de Contraloria por Cedula." & vbCrLf
                        End If
                    End If


                    If Not (ArchivoFactivaNombre) Then
                        msg = msg & "Falta archivo de Factiva por Nombre." & vbCrLf
                    End If

                    If Not (ArchivoContraloriaNombre) Then
                        msg = msg & "Falta archivo de Contraloria por Nombre." & vbCrLf
                    End If
                End If



                'Si errores no hay
                If msg = "" Then
                    _RespuestaFactiva = ""
                    _RespuestaContraloria = ""
                    _RespuestaProcuraduria = ""

                    Dim dbmImaging As DBImagingDataBaseManager = Nothing

                    Try
                        dbmImaging = New DBImagingDataBaseManager(Miharu.Imaging.Library.Program.DesktopGlobal.ConnectionStrings.Imaging)
                        dbmImaging.Connection_Open(Miharu.Imaging.Library.Program.Sesion.Usuario.id)

                        Dim ParametroSistemaDataTable = dbmImaging.SchemaConfig.TBL_Parametro_Sistema.DBGet(Nothing)
                        Dim TextoFactivaNegativo As String = ""
                        Dim TextoFactivaPositivo As String = ""
                        Dim TextoEsFactiva As String = ""
                        Dim TextoContraloriaNegativo As String = ""
                        Dim TextoProcuraduriaNegativo1 As String = ""
                        Dim TextoProcuraduriaNegativo2 As String = ""
                        Dim TextoEsContraloria As String = ""
                        Dim TextoEsProcuraduria As String = ""

                        If ParametroSistemaDataTable.Count > 0 Then

                            For Each ParametroRow In ParametroSistemaDataTable

                                Select Case ParametroRow.Nombre_Parametro_Sistema

                                    Case "@FactivaNegativo"
                                        TextoFactivaNegativo = Replace(ParametroRow.Valor_Parametro_Sistema, " ", "")
                                    Case "@FactivaPositivo"
                                        TextoFactivaPositivo = Replace(ParametroRow.Valor_Parametro_Sistema, " ", "")
                                    Case "@ContraloriaNegativo"
                                        TextoContraloriaNegativo = Replace(ParametroRow.Valor_Parametro_Sistema, " ", "")
                                    Case "@ProcuraduriaNegativo1"
                                        TextoProcuraduriaNegativo1 = Replace(ParametroRow.Valor_Parametro_Sistema, " ", "")
                                    Case "@ProcuraduriaNegativo2"
                                        TextoProcuraduriaNegativo2 = Replace(ParametroRow.Valor_Parametro_Sistema, " ", "")
                                    Case "@EsFactiva"
                                        TextoEsFactiva = Replace(ParametroRow.Valor_Parametro_Sistema, " ", "")
                                    Case "@EsContraloria"
                                        TextoEsContraloria = Replace(ParametroRow.Valor_Parametro_Sistema, " ", "")
                                    Case "@EsProcuraduria"
                                        TextoEsProcuraduria = Replace(ParametroRow.Valor_Parametro_Sistema, " ", "")
                                End Select

                            Next

                            For Each fileName In fileNames
                                Dim record = New Record(Path.GetFileName(fileName).ToUpper())
                                Dim extension = Path.GetExtension(record.FileName).ToUpper()

                                'FACTIVA
                                If ((record.FileName = CedulaCapturada & "_F_C" & extension) Or (record.FileName = CedulaCapturada & "_F_N" & extension)) Then
                                    If (ReadPdfFile(fileName, TextoFactivaNegativo)) Then
                                        _RespuestaFactiva = "NEGATIVO"
                                    ElseIf (ReadPdfFile(fileName, TextoFactivaPositivo)) Then
                                        _RespuestaFactiva = "POSITIVO"
                                    ElseIf (ReadPdfFile(fileName, TextoEsFactiva)) Then
                                        _RespuestaFactiva = "MULTIPLE"
                                    Else
                                        MessageBox.Show("No se pudo determinar el estado del cliente en el documento, por favor valide el archivo", "Validación Archivos", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                                        Return
                                    End If
                                End If


                                'CONTRALORIA CEDULA
                                If record.FileName = CedulaCapturada & "_C_C" & extension Then
                                    If (ReadPdfFile(fileName, TextoEsContraloria)) Then
                                        Dim Pasaportenumerico = LimpiarPasaporteContraloria(CedulaCapturada)
                                        Dim TextoContraloriaNegativo_C = TextoContraloriaNegativo.Replace("@Dato", Pasaportenumerico)
                                        If (ReadPdfFile(fileName, TextoContraloriaNegativo_C)) Then
                                            _RespuestaContraloria = "NEGATIVO"
                                        Else

                                            _RespuestaContraloria = "POSITIVO"
                                        End If
                                    Else
                                        MessageBox.Show("Aparentemente el archivo no pertenece a la Contraloría, por favor valide el archivo", "Validación Archivos", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                                        Return
                                    End If

                                End If

                                    'CONTRALORIA NOMBRE
                                If record.FileName = CedulaCapturada & "_C_N" & extension Then
                                    If (ReadPdfFile(fileName, TextoEsContraloria)) Then
                                        Dim NombreLimpio = LimpiarNombreContraloria(_NombreCapturado)
                                        Dim TextoContraloriaNegativo_N = TextoContraloriaNegativo.Replace("@Dato", NombreLimpio)
                                        If (ReadPdfFile(fileName, TextoContraloriaNegativo_N)) Then
                                            _RespuestaContraloria = "NEGATIVO"
                                        Else
                                            _RespuestaContraloria = "POSITIVO"
                                        End If
                                    Else
                                        MessageBox.Show("Aparentemente el archivo no pertenece a la Contraloría, por favor valide el archivo", "Validación Archivos", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                                        Return
                                    End If
                                End If

                                    'PROCURADURIA
                                    If (Procuraduria = "SI") Then
                                        If record.FileName = CedulaCapturada & "_P" & extension Then
                                            If (ReadPdfFile(fileName, TextoEsProcuraduria)) Then
                                                If (ReadPdfFile(fileName, TextoProcuraduriaNegativo1)) Then
                                                    _RespuestaProcuraduria = "NEGATIVO"
                                                ElseIf (ReadPdfFile(fileName, TextoProcuraduriaNegativo2)) Then
                                                    _RespuestaProcuraduria = "NEGATIVO"
                                                Else
                                                    _RespuestaProcuraduria = "POSITIVO"
                                                End If
                                            Else
                                                MessageBox.Show("Aparentemente el archivo no pertenece a la Procuraduría, por favor valide el archivo", "Validación Archivos", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                                                Return
                                            End If
                                        End If
                                    Else
                                        _RespuestaProcuraduria = "NO APLICA"
                                    End If


                            Next

                            _ValidacionListasController.RespuestaFactiva = _RespuestaFactiva
                            _ValidacionListasController.RespuestaContraloria = _RespuestaContraloria
                            _ValidacionListasController.RespuestaProcuraduria = _RespuestaProcuraduria

                            _ValidacionListasController.IndexerView.ShowSaveButton(True)
                        End If

                    Catch ex As Exception
                        DesktopMessageBoxControl.DesktopMessageShow("Validacion Respuestas PDF", ex)
                    Finally
                        If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                    End Try


                Else
                    MessageBox.Show(msg.ToString(), "Validación Archivos", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                End If

                _ValidacionListasController._IndexerView.ShowImagen(False)

            End If

        End Sub

        Public Sub SetFocus() Implements IListValidationControl.SetFocus
            RutaTextBox.Focus()
            RutaTextBox.SelectAll()
        End Sub

        'Public Sub Save(ByVal nfk_Expediente As Integer, ByVal nfk_Folder As Integer, ByVal nfk_File As Integer, ByVal nid_Version As Integer) Implements IListValidationControl.Save


        'End Sub

#End Region

#Region " Eventos "

        Private Sub SelectFolderButton_Click(sender As System.Object, e As System.EventArgs) Handles SelectFolderButton.Click
            SelectFolderPath()
        End Sub

        Private Sub ValidarButton_Click(sender As System.Object, e As System.EventArgs) Handles ValidarButton.Click
            ValidarArchivos()
        End Sub

        Private Sub ListValidationControl_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            SetFocus()
        End Sub

#End Region

#Region " Metodos "

        Private Sub SelectFolderPath()
            Dim LectorFolderBrowserDialog = New FolderBrowserDialog()
            Dim Respuesta As DialogResult

            LectorFolderBrowserDialog.SelectedPath = RutaTextBox.Text
            LectorFolderBrowserDialog.ShowNewFolderButton = False
            LectorFolderBrowserDialog.Description = "Seleccione la carpeta"

            Respuesta = LectorFolderBrowserDialog.ShowDialog()

            If (Respuesta = DialogResult.OK) Then
                RutaTextBox.Text = LectorFolderBrowserDialog.SelectedPath
            End If
        End Sub

        'Public Function save(ByVal nfk_Expediente As Long, ByVal nfk_Folder As Short, ByVal nfk_File As Short) As Boolean



        '    _ValidacionListasController.Save_Validacion_Listas_Respuesta(nfk_Expediente, nfk_Folder, nfk_File, _RespuestaFactiva, _RespuestaContraloria, _RespuestaProcuraduria)


        '    'dbmSantander.SchemaSantander.PA_Insercion_Validacion_Listas_Respuestas.DBExecute(nfk_Expediente, nfk_Folder, nfk_File, RespuestaFactiva, RespuestaContraloria, RespuestaProcuraduria)




        '    Return True
        'End Function


#End Region

#Region " Funciones "

        Private Function Validar() As Boolean
            If (RutaTextBox.Text = "") Then
                DesktopMessageBoxControl.DesktopMessageShow("Debe seleccionar un directorio válido", "Directorio inválido", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                RutaTextBox.Focus()

            ElseIf (Not Directory.Exists(Me.SelectedPath)) Then
                DesktopMessageBoxControl.DesktopMessageShow("Debe seleccionar un directorio válido", "Directorio inválido", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                RutaTextBox.Focus()
                RutaTextBox.SelectAll()
            Else
                Return True
            End If

            Return False
        End Function

        Public Function ReadPdfFile(fileName As String, searthText As String) As Boolean
            If searthText <> "" Then

                Dim pages As List(Of Integer) = New List(Of Integer)()
                If File.Exists(fileName) Then
                    Dim pdfReader As New PdfReader(fileName)
                    For page As Integer = 1 To pdfReader.NumberOfPages
                        Dim strategy As ITextExtractionStrategy = New SimpleTextExtractionStrategy()
                        Dim currentPageText As String = PdfTextExtractor.GetTextFromPage(pdfReader, page, strategy)
                        currentPageText = currentPageText.Replace(" ", "")
                        currentPageText = currentPageText.Replace(vbCrLf, "")
                        currentPageText = currentPageText.Replace(vbLf, "")
                        If currentPageText.Contains(searthText) Then
                            pages.Add(page)

                        End If

                    Next
                    pdfReader.Close()

                End If
                If pages.Count = 0 Then
                    Return False
                Else
                    Return True
                End If
            Else
                Throw New Exception("No se encontró un parametro para validacion")
            End If
        End Function

        Public Function BorrarArchivos() As Boolean

            Try
                Dim fileNames = Directory.GetFiles(_Ruta)

                For Each fileName In fileNames

                    My.Computer.FileSystem.DeleteFile(fileName)

                Next
            Catch ex As Exception
                MessageBox.Show("Algunos archivos no se pudieron borrar", "Eliminación Archivos Cargados", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            End Try
         

            Return True

        End Function

        Public Function LimpiarPasaporteContraloria(Pasaporte As String) As String
            Dim pasaporteNumerico As String = String.Empty

            For i As Integer = 0 To Len(Pasaporte) - 1 Step 1
                If IsNumeric(Pasaporte.Substring(i, 1)) Then
                    pasaporteNumerico += Pasaporte.Substring(i, 1)
                End If
            Next
            Return pasaportenumerico
        End Function

        Public Function LimpiarNombreContraloria(Nombre As String) As String
            Dim nombreLimpio As String = String.Empty
            If Nombre.Length > 50 Then
                Nombre = Nombre.Substring(0, 50)
            End If

            For i As Integer = 0 To Len(Nombre) - 1 Step 1
                If Char.IsLetter(Nombre.Substring(i, 1)) Then
                    nombreLimpio += Nombre.Substring(i, 1)
                End If
            Next

            Return nombreLimpio
        End Function


#End Region


    End Class

End Namespace
