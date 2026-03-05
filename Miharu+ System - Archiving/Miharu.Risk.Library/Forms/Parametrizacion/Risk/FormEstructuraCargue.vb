Imports System.ComponentModel
Imports System.IO
Imports System.Text
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports DBArchiving
Imports DBArchiving.Schemadbo
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Config

Namespace Forms.Parametrizacion.Risk

    Public Class FormEstructuraCargue
        Inherits FormBase

#Region " Declaraciones "

        Private _Separador As String = ","
        Private Const LenLinea As Integer = 100
        Private Const charFillPrimario As Char = CChar("=")
        Private Const charFillSecundario As Char = CChar("-")
        Dim fs As FileStream
        Private _NumEsquemas As Integer = 0

#End Region

#Region " Eventos "
        Private Sub GenerarArchivoButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles GenerarArchivoButton.Click
            GenerarArchivo()
        End Sub

        Private Sub CerrarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CerrarButton.Click
            Me.Close()
        End Sub
#End Region

#Region " Metodos "

        Private Sub GenerarArchivo()
            Try
                If ComaRadioButton.Checked Then _Separador = CChar(",")
                If TabuladorRadioButton.Checked Then _Separador = ControlChars.Tab
                If PuntoComaRadioButton.Checked Then _Separador = CChar(";")

                GuardarArchivoSaveFileDialog.Filter = "Archivo Plano (*.txt)|*.txt"
                GuardarArchivoSaveFileDialog.FileName = "Estructura de cargue [" & Program.RiskGlobal.NombreEntidad & "][" & Program.RiskGlobal.NombreProyecto & "].txt"
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

            Try
                dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

                Dim s As New StreamWriter(archivo, System.Text.Encoding.UTF8)
                s.BaseStream.Seek(0, SeekOrigin.End)

                'Se recorren los esquemas del proyecto seleccionado.
                Dim dtEsquemas = dbmArchiving.Schemadbo.CTA_Esquema.DBFindByfk_entidadfk_proyecto(Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto)
                _NumEsquemas = dtEsquemas.Rows.Count

                Dim iEsquema As Integer = 0
                Me.GenerarArchivoBackgroundWorker.ReportProgress(iEsquema)
                For Each rowEsquema In dtEsquemas
                    iEsquema += 1
                    s.Write(vbCrLf & vbCrLf)
                    s.Write(FormateaLinea("ESQUEMA " & rowEsquema.Nombre_esquema.ToUpper(), 1))
                    s.Write(vbCrLf)

                    CreaEstructura(s, 0, rowEsquema) 'Universal
                    CreaEstructura(s, 1, rowEsquema) 'Folders
                    CreaEstructura(s, 2, rowEsquema) 'Files
                    Me.GenerarArchivoBackgroundWorker.ReportProgress(iEsquema)
                Next

                s.Close()
                archivo.Close()
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("EscribeArchivo", ex)
            Finally
                dbmArchiving.Connection_Close()
            End Try
        End Sub

        Private Sub CreaEstructura(ByRef s As StreamWriter, ByVal tipo As Short, ByVal rowEsquema As CTA_EsquemaRow)
            Dim dbmArchiving = New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)

            Try
                dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)


                Select Case tipo
                    Case 0 'Universal
                        Dim LineaUno As New StringBuilder
                        Dim LineaDos As New StringBuilder

                        s.Write(vbCrLf & vbCrLf)
                        s.Write(FormateaLinea("UNIVERSAL", 2))
                        s.Write(vbCrLf)

                        'Esquema
                        LineaUno.Append("Esquema" & _Separador)
                        LineaDos.Append(rowEsquema.fk_esquema.ToString() & _Separador)

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

                        LineaUno = New StringBuilder(FormateaLinea(LineaUno.ToString(), 3))
                        LineaDos = New StringBuilder(FormateaLinea(LineaDos.ToString(), 3))

                        LineaUno.Append(vbCrLf)
                        LineaDos.Append(vbCrLf)
                        s.Write(LineaUno.ToString())
                        s.Write(LineaDos.ToString())

                    Case 1 'Folders
                        Dim LineaUno As New StringBuilder
                        Dim LineaDos As New StringBuilder

                        s.Write(vbCrLf & vbCrLf)
                        s.Write(FormateaLinea("FOLDERS", 2))
                        s.Write(vbCrLf)

                        'Esquema
                        LineaUno.Append("Esquema" & _Separador)
                        LineaDos.Append(rowEsquema.fk_esquema.ToString() & _Separador)

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

                        'Tipología
                        LineaUno.Append("Tipología" & _Separador)
                        LineaDos.Append("0" & _Separador)

                        'CBarras
                        LineaUno.Append("[CBarras]" & _Separador)
                        LineaDos.Append("Para Devoluciones" & _Separador)

                        'Clase
                        LineaUno.Append("Clase" & _Separador)
                        LineaDos.Append("N,A,D" & _Separador)

                        'Data Adicional
                        Dim dtDataAdicional = dbmArchiving.Schemadbo.PA_Campos_DataAdicional.DBExecute(Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, rowEsquema.fk_esquema, Nothing, Nothing, True)

                        For Each rowDataAdicionalFile As DataRow In dtDataAdicional.Rows
                            LineaUno.Append(rowDataAdicionalFile.Item("Nombre_Campo").ToString & _Separador)
                            LineaDos.Append(rowDataAdicionalFile.Item("Nombre_Campo_Tipo").ToString() & "(" & rowDataAdicionalFile.Item("Length_Campo").ToString() & ")" & _Separador)
                        Next


                        LineaUno = New StringBuilder(FormateaLinea(LineaUno.ToString(), 3))
                        LineaDos = New StringBuilder(FormateaLinea(LineaDos.ToString(), 3))

                        LineaUno.Append(vbCrLf)
                        LineaDos.Append(vbCrLf)
                        s.Write(LineaUno.ToString())
                        s.Write(LineaDos.ToString())

                    Case 2 'Files
                        Dim dtTipologias = dbmArchiving.Schemadbo.CTA_Tipologias_Proyecto_Esquema.DBFindByfk_Esquemafk_Proyectofk_Entidad(rowEsquema.fk_esquema, rowEsquema.fk_proyecto, rowEsquema.fk_entidad)

                        For Each rowTipologia In dtTipologias
                            Dim LineaUno As New StringBuilder
                            Dim LineaDos As New StringBuilder

                            s.Write(vbCrLf & vbCrLf)
                            s.Write(FormateaLinea("TIPOLOGÍA " & rowTipologia.Nombre_Tipologia.ToUpper(), 2))
                            s.Write(vbCrLf)

                            'Esquema
                            LineaUno.Append("Esquema" & _Separador)
                            LineaDos.Append(rowEsquema.fk_esquema.ToString() & _Separador)

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

                            'Tipología
                            LineaUno.Append("Tipología" & _Separador)
                            LineaDos.Append(rowTipologia.id_Tipologia & _Separador)

                            'CBarras
                            LineaUno.Append("[CBarras]" & _Separador)
                            LineaDos.Append("Para Devoluciones" & _Separador)

                            'Clase
                            LineaUno.Append("Clase" & _Separador)
                            LineaDos.Append("N,A,D" & _Separador)

                            'Data Adicional
                            Dim dtDataAdicional = dbmArchiving.Schemadbo.PA_Campos_DataAdicional.DBExecute(Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, rowEsquema.fk_esquema, rowTipologia.id_Tipologia, True, False)

                            For Each rowDataAdicionalFile As DataRow In dtDataAdicional.Rows
                                LineaUno.Append(rowDataAdicionalFile.Item("Nombre_Campo").ToString & _Separador)
                                LineaDos.Append(rowDataAdicionalFile.Item("Nombre_Campo_Tipo").ToString() & "(" & rowDataAdicionalFile.Item("Length_Campo").ToString() & ")" & _Separador)
                            Next


                            LineaUno = New StringBuilder(FormateaLinea(LineaUno.ToString(), 3))
                            LineaDos = New StringBuilder(FormateaLinea(LineaDos.ToString(), 3))

                            LineaUno.Append(vbCrLf)
                            LineaDos.Append(vbCrLf)
                            s.Write(LineaUno.ToString())
                            s.Write(LineaDos.ToString())
                        Next
                End Select

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("EscribeArchivo", ex)
            Finally
                dbmArchiving.Connection_Close()
            End Try
        End Sub

#Region "BackgroundWorker"
        Private Sub GenerarArchivoBackgroundWorker_DoWork(ByVal sender As System.Object, ByVal e As DoWorkEventArgs) Handles GenerarArchivoBackgroundWorker.DoWork
            EscribeArchivo(fs)
        End Sub

        Private Sub GenerarArchivoBackgroundWorker_ProgressChanged(ByVal sender As System.Object, ByVal e As ProgressChangedEventArgs) Handles GenerarArchivoBackgroundWorker.ProgressChanged
            If e.ProgressPercentage = 0 Then
                Me.GeneraArchivoProgressBar.Maximum = _NumEsquemas
            End If
            Me.GeneraArchivoProgressBar.Value = e.ProgressPercentage
        End Sub

        Private Sub GenerarArchivoBackgroundWorker_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As RunWorkerCompletedEventArgs) Handles GenerarArchivoBackgroundWorker.RunWorkerCompleted
            DesktopMessageBoxControl.DesktopMessageShow("Archivo generado correctamente en " & GuardarArchivoSaveFileDialog.FileName, "Generación archivo", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
            Try
                If AbrirArchivoCheckBox.Checked Then
                    Process.Start("notepad.exe", GuardarArchivoSaveFileDialog.FileName)
                End If
            Catch : End Try
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
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("FormateaLinea", ex)
            End Try
            Return bReturn
        End Function
#End Region

    End Class

End Namespace