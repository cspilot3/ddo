Imports System.Drawing
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Controls
Imports DBAgrario
Imports DBAgrario.SchemaConfig
Imports DBAgrario.SchemaCore
Imports Ionic.Zip
Imports Miharu.Desktop.Library.Config
Imports Slyg.Tools.Progress

Namespace Imaging.Forms.Exportar

    Public Class FormExportarImagenes

#Region " Declaraciones "
        Private _Plugin As BanagrarioImagingPlugin

        Private _EsquemaDataTable As CTA_Config_EsquemaDataTable
        Private _TransaccionDataTable As CTA_Config_DocumentoDataTable

        Private _RegionalDataTable As TBL_RegionalDataTable
        Private _COBDataTable As TBL_COBDataTable
        Private _OficinaDataTable As TBL_OficinaDataTable

        Private _ConveniosDictionary As New Dictionary(Of Integer, String)

        Dim formProgress As New FormProgress 'Slyg.Tools.FormProgress()
#End Region

#Region " Constructores "

        Public Sub New(ByVal nBanagrarioDesktopPlugin As BanagrarioImagingPlugin)

            ' This call is required by the designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            _Plugin = nBanagrarioDesktopPlugin

            CargaTablas()
        End Sub

#End Region

#Region " Eventos "

        Private Sub FormExportarImagenes_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            CargaEsquema()
            CargaRegional()
        End Sub

        Private Sub EsquemaDesktopComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles EsquemaDesktopComboBox.SelectedIndexChanged
            CargarTransaccion()
            CargarTraficoPanel()
        End Sub

        Private Sub RegionalDesktopComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles RegionalDesktopComboBox.SelectedIndexChanged
            CargaCOB()
        End Sub

        Private Sub COBDesktopComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles COBDesktopComboBox.SelectedIndexChanged
            CargaOficina()
        End Sub

        Private Sub SeleccionarRutaButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles SeleccionarRutaButton.Click
            If RutaExportacionFolderBrowserDialog.ShowDialog() = DialogResult.OK Then
                RutaExportacionDesktopTextBox.Text = RutaExportacionFolderBrowserDialog.SelectedPath
            End If
        End Sub

        Private Sub ExportarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ExportarButton.Click
            If ValidarRuta() Then
                If TipoExportacionTabControl.SelectedIndex = 0 Then 'Fecha de Proceso
                    If EsquemaDesktopComboBox.SelectedValue.ToString = "2" Then     'Convenios
                        ExportarConvenios()
                    Else    'Transacciones
                        ExportarImagenes()
                    End If
                Else 'Cargues
                    ExportarCargues()
                End If

            Else
                DesktopMessageBoxControl.DesktopMessageShow("El directorio debe estar vacío.", "Ruta no válida", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
            End If
        End Sub

#End Region

#Region " Metodos "

        Private Sub CargaTablas()

            Dim dmBanAgrario As New DBAgrarioDataBaseManager(Me._Plugin.BancoAgrarioConnectionString)
            Try

                dmBanAgrario.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                _EsquemaDataTable = dmBanAgrario.SchemaCore.CTA_Config_Esquema.DBFindByfk_Entidadfk_Proyectoid_Esquema(Program.BanagrarioEntidadId, Program.BanagrarioProyectoImagingId, Nothing)
                _TransaccionDataTable = dmBanAgrario.SchemaCore.CTA_Config_Documento.DBFindByfk_Entidadfk_Proyectofk_Esquema(Program.BanagrarioEntidadId, Nothing, Nothing)

                _RegionalDataTable = dmBanAgrario.SchemaConfig.TBL_Regional.DBGet(Nothing)
                _COBDataTable = dmBanAgrario.SchemaConfig.TBL_COB.DBGet(Nothing)
                _OficinaDataTable = dmBanAgrario.SchemaConfig.TBL_Oficina.DBGet(Nothing)

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaTablas", ex)
            Finally
                dmBanAgrario.Connection_Close()
            End Try
        End Sub

        Private Sub CargaEsquema()
            Try
                Utilities.LlenarCombo(EsquemaDesktopComboBox, _EsquemaDataTable, CTA_Config_EsquemaEnum.id_Esquema.ColumnName, CTA_Config_EsquemaEnum.Nombre_Esquema.ColumnName, True, "-1", "--TODOS--")
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaFiltros", ex)
            End Try
        End Sub

        Private Sub CargarTransaccion()
            Try
                Dim TransaccionView = _TransaccionDataTable.DefaultView
                TransaccionView.RowFilter = "fk_Esquema = " + EsquemaDesktopComboBox.SelectedValue.ToString()
                Utilities.LlenarCombo(TransaccionDesktopComboBox, TransaccionView.ToTable(), CTA_Config_DocumentoEnum.id_Documento.ColumnName, CTA_Config_DocumentoEnum.Nombre_Documento.ColumnName, True, "-1", "--TODOS--")
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargarTransaccion", ex)
            End Try
        End Sub

        Private Sub CargaRegional()
            Try
                Utilities.LlenarCombo(RegionalDesktopComboBox, _RegionalDataTable, TBL_RegionalEnum.id_Regional.ColumnName, TBL_RegionalEnum.Nombre_Regional.ColumnName, True, "-1", "--TODOS--")
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaFiltros", ex)
            End Try
        End Sub

        Private Sub CargaCOB()
            Try
                Dim COBView = _COBDataTable.DefaultView
                COBView.RowFilter = "fk_Regional = " + RegionalDesktopComboBox.SelectedValue.ToString()

                Utilities.LlenarCombo(COBDesktopComboBox, COBView.ToTable(), TBL_COBEnum.id_COB.ColumnName, TBL_COBEnum.Nombre_COB.ColumnName, True, "-1", "--TODOS--")
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaCOB", ex)
            End Try
        End Sub

        Private Sub CargaOficina()
            Try
                Dim OficinaView = _OficinaDataTable.DefaultView
                OficinaView.RowFilter = "fk_COB = " + COBDesktopComboBox.SelectedValue.ToString()

                Utilities.LlenarCombo(OficinaDesktopComboBox, OficinaView.ToTable(), TBL_OficinaEnum.id_Oficina.ColumnName, TBL_OficinaEnum.Nombre_Oficina.ColumnName, True, "-1", "--TODOS--")

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaOficina", ex)
            End Try
        End Sub

        Private Sub ExportarImagenes()
            Dim dmBanAgrario As New DBAgrarioDataBaseManager(Me._Plugin.BancoAgrarioConnectionString)

            Try
                dmBanAgrario.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                Dim ImagenesDataTable = dmBanAgrario.SchemaProcess.PA_Exportar_Imagenes.DBExecute(FechaProcesodateTimePicker.Value.ToString("yyyy/MM/dd"), CInt(TransaccionDesktopComboBox.SelectedValue), CShort(RegionalDesktopComboBox.SelectedValue), CShort(COBDesktopComboBox.SelectedValue), CInt(OficinaDesktopComboBox.SelectedValue), CShort(EsquemaDesktopComboBox.SelectedValue), DBNull.Value)
                Dim countProcess As Integer = 0

                If ImagenesDataTable.Count > 0 Then
                    Dim FileNames As New List(Of String)

                    formProgress.ValueProcess = countProcess
                    formProgress.MaxValueProcess = ImagenesDataTable.Count

                    formProgress.Show()

                    For Each imagen In ImagenesDataTable
                        Dim Nombre_File_Exportar = LimpiarCadena(imagen.Nombre_Tx) & "_" & imagen.Fecha_Movimiento.Replace("/", "") & "_" & imagen.Consecutivo & "_" & imagen.File_Unique_Identifier.ToString()
                        Bytes_Imagen_Disk(imagen.File_Unique_Identifier.ToString(), Nombre_File_Exportar, FileNames)

                        countProcess = countProcess + 1
                        formProgress.ValueProcess = countProcess
                        formProgress.Process = "Imagen: " & countProcess
                        Application.DoEvents()
                    Next

                    'Genera el ZIP
                    If FileNames.Count > 0 And ComprimirDesktopCheckBox.Checked Then
                        Dim ZipFileName As String = RutaExportacionDesktopTextBox.Text & "\Imagenes_" & CStr(FechaProcesodateTimePicker.Value.Month).PadLeft(2, "0"c) & CStr(FechaProcesodateTimePicker.Value.Day).PadLeft(2, "0"c) & CStr(FechaProcesodateTimePicker.Value.Year).PadLeft(4, "0"c) & "_" & CStr(Now.Hour).PadLeft(2, "0"c) & CStr(Now.Minute).PadLeft(2, "0"c) & ".zip"

                        Using zip As ZipFile = New ZipFile()
                            zip.AddFiles(FileNames, False, "")
                            zip.Save(ZipFileName)
                        End Using

                        For Each archivo In FileNames
                            File.Delete(archivo)
                        Next
                    End If

                    formProgress.Hide()

                    DesktopMessageBoxControl.DesktopMessageShow("Se realizó la exportación con éxito de " & countProcess & " transacciones.", "Resultado Exportación", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                Else
                    DesktopMessageBoxControl.DesktopMessageShow("No se encontraron imágenes para el filtro seleccionado.", "Búsqueda de Imágenes", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                End If

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("ExportarImagenes", ex)
            Finally
                dmBanAgrario.Connection_Close()
            End Try

        End Sub

        Private Sub ExportarConvenios()
            Dim dmBanAgrario As New DBAgrarioDataBaseManager(Me._Plugin.BancoAgrarioConnectionString)
            Dim Trafico As Integer = 0

            Try
                dmBanAgrario.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)


                'Validacion de Tx con o sin Tráfico
                If (ConTraficoDesktopCheckBox.Checked = True And SinTraficoDesktopCheckBox.Checked = True) Or (ConTraficoDesktopCheckBox.Checked = False And SinTraficoDesktopCheckBox.Checked = False) Then
                    Trafico = 3
                ElseIf ConTraficoDesktopCheckBox.Checked = True Then
                    Trafico = 2
                ElseIf SinTraficoDesktopCheckBox.Checked = True Then
                    Trafico = 1
                End If

                'Se agregan al diccionario los convenios.
                _ConveniosDictionary.Clear()
                If TransaccionDesktopComboBox.SelectedValue.ToString() = "-1" Then  'TODOS

                    Dim TransaccionView = _TransaccionDataTable.DefaultView
                    TransaccionView.RowFilter = "fk_Esquema = " + EsquemaDesktopComboBox.SelectedValue.ToString()

                    For Each convenio As DataRow In TransaccionView.ToTable().Rows
                        _ConveniosDictionary.Add(CInt(convenio("id_Documento")), CStr(convenio("Nombre_Documento")))
                    Next

                Else    'Convenio único

                    'Validacion de Tx con o sin Tráfico
                    If Trafico = 2 Then
                        'Validar si Tx tiene trafico
                        If ValidarTraficoTx(dmBanAgrario) = "True" Then
                            _ConveniosDictionary.Add(CInt(TransaccionDesktopComboBox.SelectedValue), TransaccionDesktopComboBox.SelectedText)
                        Else
                            DesktopMessageBoxControl.DesktopMessageShow("La Transacción que desea exportar no tiene tráfico, por favor verifique", "Exportar transacciones con tráfico", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                            Exit Sub
                        End If
                    ElseIf Trafico = 1 Then
                        'Validar si Tx no tiene tráfico
                        If ValidarTraficoTx(dmBanAgrario) = "False" Then
                            _ConveniosDictionary.Add(CInt(TransaccionDesktopComboBox.SelectedValue), TransaccionDesktopComboBox.SelectedText)
                        Else
                            DesktopMessageBoxControl.DesktopMessageShow("La Transacción que desea exportar tiene tráfico, por favor verifique", "Exportar transacciones sin tráfico", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                            Exit Sub
                        End If
                    Else
                        _ConveniosDictionary.Add(CInt(TransaccionDesktopComboBox.SelectedValue), TransaccionDesktopComboBox.SelectedText)
                    End If


                End If

                'Se recorren los convenios a exportar.
                formProgress.Process = ""
                formProgress.Action = ""
                formProgress.ValueProcess = 0
                formProgress.ValueAction = 0
                formProgress.MaxValueProcess = _ConveniosDictionary.Count
                formProgress.MaxValueAction = 8

                formProgress.Show()

                Dim i As Integer = 0
                Dim ImagenesDataTable = dmBanAgrario.SchemaProcess.PA_Exportar_Imagenes.DBExecute(FechaProcesodateTimePicker.Value.ToString("yyyy/MM/dd"), CInt(TransaccionDesktopComboBox.SelectedValue), CShort(RegionalDesktopComboBox.SelectedValue), CShort(COBDesktopComboBox.SelectedValue), CInt(OficinaDesktopComboBox.SelectedValue), CShort(EsquemaDesktopComboBox.SelectedValue), Trafico)

                For Each convenio In _ConveniosDictionary

                    formProgress.Process = convenio.Value

                    Dim ImagenesView = ImagenesDataTable.DefaultView
                    ImagenesView.RowFilter = "fk_Documento = " & convenio.Key

                    If ImagenesView.Count > 0 Then
                        Dim cons As Integer = 1
                        Dim codigoConvenio As String = Nothing
                        Dim FileNames As New List(Of String)

                        For Each imagen As DataRow In ImagenesView.ToTable().Rows

                            Dim Nombre_File_Exportar = "trafidoc_" & _
                                                       CStr(FechaProcesodateTimePicker.Value.Month).PadLeft(2, "0"c) & _
                                                       CStr(FechaProcesodateTimePicker.Value.Day).PadLeft(2, "0"c) & _
                                                       CStr(FechaProcesodateTimePicker.Value.Year).PadLeft(4, "0"c) & "_" & _
                                                       CStr(FechaProcesodateTimePicker.Value.Hour) & _
                                                       CStr(FechaProcesodateTimePicker.Value.Minute) & _
                                                       "_con" & CStr(imagen("Nombre_Tx")).Substring(1, 5) & _
                                                       "_" & CStr(cons).PadLeft(3, "0"c)
                            '"_" & CStr(cons).PadLeft(4, "0"c)


                            Bytes_Imagen_Disk(imagen("File_Unique_Identifier").ToString(), Nombre_File_Exportar, FileNames)
                            cons += 1
                            If IsNothing(codigoConvenio) Then codigoConvenio = CStr(imagen("Nombre_Tx")).Substring(1, 5)
                        Next

                        'Genera el ZIP
                        If FileNames.Count > 0 And ComprimirDesktopCheckBox.Checked Then
                            Dim ZipFileName As String = RutaExportacionDesktopTextBox.Text & _
                                                        "\trafidoc_" & _
                                                        CStr(FechaProcesodateTimePicker.Value.Month).PadLeft(2, "0"c) & _
                                                        CStr(FechaProcesodateTimePicker.Value.Day).PadLeft(2, "0"c) & _
                                                        CStr(FechaProcesodateTimePicker.Value.Year).PadLeft(4, "0"c) & "_" & _
                                                        CStr(FechaProcesodateTimePicker.Value.Hour) & _
                                                        CStr(FechaProcesodateTimePicker.Value.Minute) & _
                                                        "_con" & codigoConvenio & ".zip"
                            'CStr(Now.Hour).PadLeft(2, "0"c) & _
                            'CStr(Now.Minute).PadLeft(2, "0"c) & _


                            Using zip As ZipFile = New ZipFile()
                                zip.AddFiles(FileNames, False, "")
                                zip.Save(ZipFileName)
                            End Using

                            For Each archivo In FileNames
                                File.Delete(archivo)
                            Next
                        End If

                    End If

                    i += 1
                    Application.DoEvents()
                    formProgress.ValueProcess = i
                Next
                formProgress.Hide()

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("ExportarConvenios", ex)
            Finally
                dmBanAgrario.Connection_Close()
            End Try
        End Sub

        Private Sub ExportarCargues()
            Dim dmBanAgrario As New DBAgrarioDataBaseManager(Me._Plugin.BancoAgrarioConnectionString)

            Try
                dmBanAgrario.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                Dim CarguesDataTable = dmBanAgrario.SchemaProcess.PA_Exportar_Cargues.DBExecute(FechaInicioDateTimePicker.Value.ToString("yyyy/MM/dd"), FechaFinDateTimePicker.Value.ToString("yyyy/MM/dd"), CShort(RegionalDesktopComboBox.SelectedValue), CShort(COBDesktopComboBox.SelectedValue), CInt(OficinaDesktopComboBox.SelectedValue))
                Dim countProcess As Integer = 0

                If CarguesDataTable.Count > 0 Then
                    formProgress.ValueProcess = countProcess
                    formProgress.MaxValueProcess = CarguesDataTable.Count
                    formProgress.Show()

                    For Each cargue_actual In CarguesDataTable

                        Dim NombreFolder As String = CStr(cargue_actual.id_Oficina).PadLeft(4, "0"c) & cargue_actual.Fecha_Movimiento.Replace("/", "") & "_" & CStr(cargue_actual.fk_Cargue) & "\"

                        formProgress.ValueProcess = countProcess
                        formProgress.Process = "Cargue: " & cargue_actual.fk_Cargue & " - " & NombreFolder
                        Application.DoEvents()


                        Dim ImagenesDataTable = dmBanAgrario.SchemaProcess.CTA_Exportar_Imagenes_x_Cargue.DBFindByfk_Cargue(cargue_actual.fk_Cargue)

                        For Each imagen In ImagenesDataTable
                            Dim Nombre_File_Exportar = NombreFolder & CStr(imagen.fk_Expediente).PadLeft(10, "0"c) & "_FP" & cargue_actual.Fecha_Proceso.Replace("/", "") & "_FM" & cargue_actual.Fecha_Movimiento.Replace("/", "")
                            Bytes_Imagen_Disk(imagen.File_Unique_Identifier.ToString(), Nombre_File_Exportar)
                        Next

                        countProcess = countProcess + 1
                    Next

                    formProgress.Hide()

                    DesktopMessageBoxControl.DesktopMessageShow("Se realizó la exportación con éxito de " & countProcess & " cargues.", "Resultado Exportación", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                Else
                    DesktopMessageBoxControl.DesktopMessageShow("No se encontraron imágenes para el filtro seleccionado.", "Búsqueda de Imágenes", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("ExportarConvenios", ex)
            Finally
                dmBanAgrario.Connection_Close()
            End Try
        End Sub

        Sub Bytes_Imagen_Disk(ByVal guid As String, ByVal nombreExportar As String, Optional ByRef Files As List(Of String) = Nothing)
            Dim dmBanAgrario As New DBAgrarioDataBaseManager(Me._Plugin.BancoAgrarioConnectionString)
            Dim countAction As Integer = 0

            Try
                dmBanAgrario.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                Dim nImagenDataTable As DataTable = dmBanAgrario.SchemaProcess.PA_Imagenes_Visualizar.DBExecute(guid)

                formProgress.ValueAction = countAction
                formProgress.MaxValueAction = nImagenDataTable.Rows.Count

                For Each imageRow As DataRow In nImagenDataTable.Rows

                    Dim imageBinary As Image = New Bitmap(New MemoryStream(CType(imageRow("Image_Binary"), Byte())))

                    Dim folioImage As String = imageRow("id_File_Record_Folio").ToString()
                    Dim nameImeage As String

                    If nImagenDataTable.Rows.Count = 1 Then
                        nameImeage = RutaExportacionDesktopTextBox.Text & "\" & nombreExportar & ".jpg"
                    Else
                        'nameImeage = RutaExportacionDesktopTextBox.Text & "\" & nombreExportar & "_" & folioImage & ".jpg"
                        nameImeage = RutaExportacionDesktopTextBox.Text & "\" & nombreExportar & "_" & CStr(folioImage).PadLeft(2, "0"c) & ".jpg"
                    End If

                    'Valida que exista el directorio de almacenamiento
                    If Not Directory.Exists(Path.GetDirectoryName(nameImeage)) Then Directory.CreateDirectory(Path.GetDirectoryName(nameImeage))

                    'Escribe la imagen en disco
                    imageBinary.Save(nameImeage)

                    'Agrega el elemento a la lista
                    If Not IsNothing(Files) Then Files.Add(nameImeage)

                    imageBinary.Dispose()

                    countAction = countAction + 1
                    formProgress.ValueAction = countAction
                    formProgress.Action = "Transacción: " & imageRow("File_Unique_Identifier").ToString() & " - Folio: " & countAction
                    Application.DoEvents()
                Next
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Bytes_Imagen_Disk", ex)
            Finally
                dmBanAgrario.Connection_Close()
            End Try

        End Sub

        Private Sub CargarTraficoPanel()
            Try
                If EsquemaDesktopComboBox.SelectedValue.ToString = "2" Then

                    TraficoPanel.Visible = True

                End If

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargarTraficoPanel ", ex)
            End Try
        End Sub

#End Region

#Region " Funciones "

        Private Function ValidarRuta() As Boolean
            Try
                If Not RutaExportacionDesktopTextBox.Text = "" Then
                    If (Directory.GetDirectories(RutaExportacionDesktopTextBox.Text).Length = 0 And Directory.GetFiles(RutaExportacionDesktopTextBox.Text).Length = 0) Then
                        Return True
                    End If
                Else
                    Return False
                End If

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("ValidarRuta", ex)
            End Try

            Return False
        End Function

        Private Function LimpiarCadena(ByVal nCadena As String) As String
            Try
                Dim regexSearch As String = New String(Path.GetInvalidFileNameChars()) & New String(Path.GetInvalidPathChars())
                Dim regex As Regex = New Regex(String.Format("[{0}]", regex.Escape(regexSearch)))
                Return regex.Replace(nCadena, "-")

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("LimpiarCadena", ex)
            End Try
            Return nCadena
        End Function

        Private Function ValidarTraficoTx(ByRef ndmBanAgrario As DBAgrarioDataBaseManager) As String

            Dim TxTieneTrafico As String = ""

            Try

                Dim DocumentoTraficoDataTable = ndmBanAgrario.SchemaConfig.TBL_Documento_Trafico.DBFindByfk_Documento(CShort(TransaccionDesktopComboBox.SelectedValue.ToString()))

                If DocumentoTraficoDataTable.Count > 0 Then


                    TxTieneTrafico = DocumentoTraficoDataTable.Rows(0)("tiene_Trafico").ToString()

                End If

            Catch ex As Exception

                DesktopMessageBoxControl.DesktopMessageShow("TxTieneTráfico ", ex)

            End Try

            Return TxTieneTrafico


        End Function

#End Region

    End Class
End Namespace