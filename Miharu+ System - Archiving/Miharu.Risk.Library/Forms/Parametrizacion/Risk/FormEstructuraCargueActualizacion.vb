Imports System.ComponentModel
Imports System.IO
Imports System.Text
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Controls
Imports DBArchiving
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Config

Namespace Forms.Parametrizacion.Risk

    Public Class FormEstructuraCargueActualizacion
        Inherits FormBase

#Region " Declaraciones "

        Private _Separador As String = ","
        Private Const LenLinea As Integer = 100
        Private Const charFillPrimario As Char = CChar("=")
        Private Const charFillSecundario As Char = CChar("-")
        Dim fs As FileStream
        Private _NumCampos As Integer = 0

        Private _dsProyectoEsquema As New DataSet()
        Dim _viewDocumento As DataView
        Private _DocumentoID As Short
        Private _DocumentoName As String

#End Region

#Region " Eventos "

        Private Sub FormEstructuraCargueActualizacion_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            CargaFiltros()
        End Sub

        Private Sub GenerarArchivoButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles GenerarArchivoButton.Click
            _DocumentoID = CShort(DocumentoComboBox.SelectedValue)
            _DocumentoName = CStr(DocumentoComboBox.Text)
            GenerarArchivo()
        End Sub

        Private Sub CerrarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CerrarButton.Click
            Me.Close()
        End Sub

        Private Sub EsquemaComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles EsquemaComboBox.SelectedIndexChanged
            Try
                _viewDocumento = _dsProyectoEsquema.Tables(0).DefaultView
                _viewDocumento.RowFilter = "fk_Esquema=" & EsquemaComboBox.SelectedValue.ToString()

                Utilities.LlenarCombo(DocumentoComboBox, _viewDocumento.ToTable(), "id_Documento", "Nombre_Documento")
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("EsquemaComboBox_SelectedIndexChanged", ex)
            End Try
        End Sub

        Private Sub DocumentoComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles DocumentoComboBox.SelectedIndexChanged
            _viewDocumento.RowFilter = "fk_Esquema=" & EsquemaComboBox.SelectedValue.ToString() & " AND id_Documento=" & DocumentoComboBox.SelectedValue.ToString()
        End Sub

#End Region

#Region " Metodos "

        Private Sub GenerarArchivo()
            Try
                If ComaRadioButton.Checked Then _Separador = CChar(",")
                If TabuladorRadioButton.Checked Then _Separador = ControlChars.Tab
                If PuntoComaRadioButton.Checked Then _Separador = CChar(";")

                GuardarArchivoSaveFileDialog.Filter = "Archivo Plano (*.txt)|*.txt"
                GuardarArchivoSaveFileDialog.FileName = "Estructura de cargue actualización [" & Program.RiskGlobal.NombreEntidad & "][" & Program.RiskGlobal.NombreProyecto & "][" & EsquemaComboBox.Text & "][" & DocumentoComboBox.Text & "].txt"
                GuardarArchivoSaveFileDialog.Title = "Guardar Estructura de Archivo"

                Dim Resultado = GuardarArchivoSaveFileDialog.ShowDialog()
                If Resultado = Windows.Forms.DialogResult.OK Then
                    fs = CType(GuardarArchivoSaveFileDialog.OpenFile(), FileStream)
                    Me.GenerarArchivoBackgroundWorker.RunWorkerAsync()
                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("GenerarArchivo", ex)
            End Try
        End Sub

        Private Sub EscribeArchivo(ByRef archivo As FileStream)
            Dim dbmArchiving = New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            Dim LineaUno As New StringBuilder
            Dim LineaDos As New StringBuilder

            Try
                dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

                'Se recorren los campos del documento seleccionado.
                Dim dtCampos = dbmArchiving.Schemadbo.CTA_Campo.DBFindByfk_Entidadfk_DocumentoEs_Campo_Actualizacion(Program.RiskGlobal.Entidad, _DocumentoID, True)
                _NumCampos = dtCampos.Rows.Count

                If _NumCampos > 0 Then

                    Dim s As New StreamWriter(archivo, System.Text.Encoding.UTF8)
                    s.BaseStream.Seek(0, SeekOrigin.End)

                    s.Write(vbCrLf & vbCrLf)
                    s.Write(FormateaLinea("DOCUMENTO " & _DocumentoName.ToUpper(), 1))
                    s.Write(vbCrLf)

                    'Tipología
                    LineaUno.Append("Documento" & _Separador)
                    LineaDos.Append(_viewDocumento.ToTable().Rows(0)("id_documento").ToString() & _Separador)

                    'Llaves
                    For Each llave In Program.RiskGlobal.LLavesProyecto
                        Dim strTipo As String = ""
                        Select Case llave.Tipo
                            Case DesktopConfig.CampoTipo.Texto
                                strTipo = DesktopConfig.CampoTipo.Texto.ToString()
                            Case DesktopConfig.CampoTipo.Numerico
                                strTipo = DesktopConfig.CampoTipo.Numerico.ToString()
                            Case DesktopConfig.CampoTipo.Fecha
                                strTipo = DesktopConfig.CampoTipo.Fecha.ToString()
                        End Select
                        LineaUno.Append(llave.Nombre & _Separador)
                        LineaDos.Append(strTipo & _Separador)
                    Next

                    'Campos
                    Dim iCampo As Integer = 0
                    Me.GenerarArchivoBackgroundWorker.ReportProgress(iCampo)
                    For Each rowCampos In dtCampos
                        iCampo += 1

                        LineaUno.Append(rowCampos("Nombre_Campo").ToString() & _Separador)
                        LineaDos.Append(rowCampos("Nombre_Campo_Tipo").ToString() & _Separador)

                        Me.GenerarArchivoBackgroundWorker.ReportProgress(iCampo)
                    Next

                    LineaUno = New StringBuilder(FormateaLinea(LineaUno.ToString(), 3))
                    LineaDos = New StringBuilder(FormateaLinea(LineaDos.ToString(), 3))

                    LineaUno.Append(vbCrLf)
                    LineaDos.Append(vbCrLf)

                    s.Write(LineaUno.ToString())
                    s.Write(LineaDos.ToString())

                    s.Close()
                    archivo.Close()
                Else
                    Dim FileToDelete As String
                    FileToDelete = archivo.Name
                    archivo.Close()
                    archivo = Nothing
                    If File.Exists(FileToDelete) = True Then
                        File.Delete(FileToDelete)
                    End If

                    Throw New Exception("No existen campos cargue para este documento.")
                End If

            Catch
                Throw
            Finally
                dbmArchiving.Connection_Close()
            End Try
        End Sub

        Private Sub CargaFiltros()
            Dim dbmArchiving = New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            Try
                dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

                Dim dtEsquemas = dbmArchiving.SchemaCore.CTA_Esquema.DBFindByfk_Entidadfk_Proyecto(Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto)
                Dim dtDocumentos = dbmArchiving.SchemaCore.CTA_Documento.DBFindByfk_Entidadfk_Proyectofk_Esquema(Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, Nothing)
                _dsProyectoEsquema.Tables.Add(dtDocumentos)

                Utilities.LlenarCombo(EsquemaComboBox, dtEsquemas, dtEsquemas.id_EsquemaColumn.ColumnName, dtEsquemas.Nombre_EsquemaColumn.ColumnName)
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaFiltros", ex)
            Finally
                dbmArchiving.Connection_Close()
            End Try
        End Sub

#Region "BackgroundWorker"

        Private Sub GenerarArchivoBackgroundWorker_DoWork(ByVal sender As System.Object, ByVal e As DoWorkEventArgs) Handles GenerarArchivoBackgroundWorker.DoWork
            Try
                EscribeArchivo(fs)
            Catch ex As Exception
                Me.GenerarArchivoBackgroundWorker.CancelAsync()
            End Try
        End Sub

        Private Sub GenerarArchivoBackgroundWorker_ProgressChanged(ByVal sender As System.Object, ByVal e As ProgressChangedEventArgs) Handles GenerarArchivoBackgroundWorker.ProgressChanged
            If e.ProgressPercentage = 0 Then
                Me.GeneraArchivoProgressBar.Maximum = _NumCampos
            End If
            Me.GeneraArchivoProgressBar.Value = e.ProgressPercentage
        End Sub

        Private Sub GenerarArchivoBackgroundWorker_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As RunWorkerCompletedEventArgs) Handles GenerarArchivoBackgroundWorker.RunWorkerCompleted
            If Not IsNothing(fs) Then
                DesktopMessageBoxControl.DesktopMessageShow("Archivo generado correctamente en " & GuardarArchivoSaveFileDialog.FileName, "Generación archivo", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)

                Try
                    If AbrirArchivoCheckBox.Checked Then
                        Process.Start("notepad.exe", GuardarArchivoSaveFileDialog.FileName)
                    End If
                Catch : End Try
            Else
                DesktopMessageBoxControl.DesktopMessageShow("Es posible que el Esquema no tenga documentos con campos de tipo cargue.", "Generación archivo", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
            End If
        End Sub

#End Region

#End Region

#Region " Funciones "

        Private Function FormateaLinea(ByVal texto As String, ByVal tipoFormato As Short) As String
            Dim bReturn As String = ""
            Dim LengthTexto As Integer
            Dim LengthFill As Integer
            Dim strTexto As String

            Try
                If texto.Length Mod 2 <> 0 Then
                    strTexto = texto & " "
                Else
                    strTexto = texto
                End If

                LengthTexto = strTexto.Length
                LengthFill = CInt((LenLinea - LengthTexto) / 2)

                Select Case tipoFormato
                    Case 1 'Titulo Primario
                        For i = 0 To LenLinea - 1
                            bReturn &= charFillPrimario
                        Next
                        bReturn &= vbCrLf
                        For i = 0 To LengthFill - 1
                            bReturn &= charFillPrimario
                        Next
                        bReturn &= strTexto
                        For i = 0 To LengthFill - 1
                            bReturn &= charFillPrimario
                        Next
                        bReturn &= vbCrLf
                        For i = 0 To LenLinea - 1
                            bReturn &= charFillPrimario
                        Next

                    Case 2 'Titulo Secundario
                        For i = 0 To LengthFill - 1
                            bReturn &= charFillSecundario
                        Next
                        bReturn &= strTexto
                        For i = 0 To LengthFill - 1
                            bReturn &= charFillSecundario
                        Next

                    Case 3 'Limpia Linea
                        bReturn = texto.Trim().Substring(0, texto.Trim().Length - 1)

                End Select
            Catch
                Throw
            End Try
            Return bReturn
        End Function

#End Region

    End Class

End Namespace