Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library.Plugins

Namespace Confinanciera.Garantias.Imaging

    Public Class Wrapper
        Public _plugin As Plugin = Nothing

        Public _ExportarButton As Button = Nothing

        Public Sub New(ByVal nPlugin As Plugin)
            _plugin = nPlugin
        End Sub

        Public Sub AplicarCambios()
            Try
                'Controles no utilizados

                'Exportar 
                _ExportarButton = PluginHelper.CloneButton(_plugin.WorkSpace.ExportarButton)
                PluginHelper.ReplaceControl(_plugin.WorkSpace.ExportarButton, _ExportarButton)
                _plugin.WorkSpace.ExportarButton.Visible = False
                AddHandler _ExportarButton.Click, AddressOf ExportarButton_Click

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("No fue posible aplicar los cambios de Banagrario al workspace, " + ex.Message, "Plugin workspace", DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
            End Try
        End Sub

        Sub ExportarButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            Try
                Dim exportarImagenesPlugin As New Forms.FormExport()
                exportarImagenesPlugin.Plugin = Me._plugin
                exportarImagenesPlugin.ShowDialog()

            Catch ex As Exception
                Miharu.Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.DesktopMessageShow(ex.Message, "Plugin workspace", Miharu.Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
            End Try
        End Sub

        'Private Sub ExportarConfinanciera(ByVal nFechaInicial As Date, ByVal nFechaFinal As Date)
        '    If Validar() Then
        '        Dim DBMImaging As New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
        '        Dim DBMCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
        '        Dim ProgressForm As New Tools.FormProgress
        '        Dim DataExportarDataTable As New SchemaExport.CTA_ConfinancieraDataTable()

        '        Try
        '            DBMImaging.Connection_Open(Program.Sesion.Usuario.id)

        '            DataExportarDataTable = DBMImaging.SchemaExport.PA_Confinanciera.DBExecute(Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, nFechaInicial, nFechaFinal)

        '        Catch ex As Exception
        '            MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        '            Return
        '        Finally
        '            DBMImaging.Connection_Close()
        '        End Try

        '        If (DataExportarDataTable.Count > 0) Then
        '            Dim Respuesta As DialogResult

        '            Respuesta = MessageBox.Show("Se encontró : " & DataExportarDataTable.Count & " imagenes pendientes por exportar" & vbCrLf & _
        '                                        "¿Desea Exportar esta informacion?", Program.AssemblyTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        '            If Respuesta = Windows.Forms.DialogResult.Yes Then
        '                Try
        '                    DBMImaging.Connection_Open(Program.Sesion.Usuario.id)
        '                    DBMCore.Connection_Open(Program.Sesion.Usuario.id)

        '                    Dim FileDataTable = DBMImaging.SchemaProcess.PA_Exportacion_Files.DBExecute(Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, nFechaInicial, nFechaFinal, Nothing, False)
        '                    Dim OutputFolder As String = CarpetaSalidaTextBox.Text.TrimEnd("\"c) & "\"
        '                    Dim Progreso As Integer = 0

        '                    ProgressForm.Show()
        '                    ProgressForm.SetProceso("Exportar")
        '                    ProgressForm.SetAccion("Obteniendo imágenes...")
        '                    ProgressForm.SetProgreso(0)
        '                    ProgressForm.SetMaxValue(FileDataTable.Count)

        '                    System.Windows.Forms.Application.DoEvents()

        '                    Dim FilesDataView As New DataView(FileDataTable)

        '                    Try : MkDir(OutputFolder & "Imagenes") : Catch : End Try

        '                    Dim ServidoresDataTable = DBMImaging.SchemaProcess.PA_Exportacion_Servidor.DBExecute(Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, nFechaInicial, nFechaFinal)

        '                    For Each RowServidor In ServidoresDataTable
        '                        ' Crear archivo físico
        '                        Dim FileProvider = getFileProvider(RowServidor.IPName_Servidor, RowServidor.Port_Servidor, RowServidor.AppName_Servidor)
        '                        Dim MsgError As String = ""
        '                        Dim Resultado As Boolean

        '                        ' Obtener los Files a transferir
        '                        FilesDataView.RowFilter = "fk_Entidad_Servidor = " & RowServidor.fk_Entidad & " AND fk_Servidor = " & RowServidor.id_Servidor

        '                        For Each ItemFile As DataRowView In FilesDataView
        '                            If ProgressForm.Cancelar Then Throw New Exception("La acción fue cancelada por el usuario")

        '                            Dim RowFile = CType(ItemFile.Row, CTA_Exportacion_FilesRow)
        '                            Dim Data() As Byte = Nothing

        '                            ' Enviar el archivo
        '                            Resultado = FileProvider.GetFile(RowFile.Path_Servidor_Volumen.TrimEnd("\"c) & "\" & RowFile.Nombre_Imagen_File, Data, MsgError)
        '                            If Not Resultado Then Throw New Exception(MsgError)

        '                            RowFile.Nombre_Imagen_File = "Imagenes\" & RowFile.CBarras_File & Path.GetExtension(RowFile.Nombre_Imagen_File)
        '                            RowFile.Path_Servidor_Volumen = ""

        '                            Dim fsOutput As New FileStream(OutputFolder & RowFile.Nombre_Imagen_File, FileMode.Create, FileAccess.Write)
        '                            fsOutput.Write(Data, 0, Data.Length)
        '                            fsOutput.Close()

        '                            Progreso += 1
        '                            ProgressForm.SetProgreso(Progreso)
        '                            System.Windows.Forms.Application.DoEvents()
        '                        Next
        '                    Next

        '                    ' Exportar data
        '                    Dim swData As New System.IO.StreamWriter(CarpetaSalidaTextBox.Text.TrimEnd("\"c) & "\" & "Export_Imagenes_" & DateTime.Now.ToString("yyyyMMdd") & ".csv", False, System.Text.Encoding.UTF8)

        '                    For Each DataExportarRow In DataExportarDataTable
        '                        Dim Nombre_Imagen_File = "E:\Imagenes\" & DataExportarRow.CBarras_File & Path.GetExtension(DataExportarRow.Nombre_Imagen_File)
        '                        swData.WriteLine(DataExportarRow.Credito & "," & _
        '                                         DataExportarRow.Cedula & "," & _
        '                                         CStr(IIf(DataExportarRow.Coodeudor = "", "", DataExportarRow.Coodeudor & ",")) & _
        '                                         Nombre_Imagen_File & "," & _
        '                                         DataExportarRow.Nombre_Documento & "," & _
        '                                         DataExportarRow.Nombre & "," & _
        '                                         DataExportarRow.Fecha & CStr(IIf(DataExportarRow.Placa <> "", ",", "")) & _
        '                                         DataExportarRow.Placa)
        '                    Next

        '                    swData.Flush()
        '                    swData.Close()

        '                    ' Marcar las imagenes como exportadas
        '                    Try
        '                        DBMCore.Transaction_Begin()

        '                        Dim ExportacionType = New TBL_ExportacionType()
        '                        ExportacionType.id_Exportacion = DBMCore.SchemaImaging.TBL_Exportacion.DBNextId()
        '                        ExportacionType.Fecha_Exportacion = SlygNullable.SysDate
        '                        ExportacionType.fk_Usuario = Program.Sesion.Usuario.id
        '                        DBMCore.SchemaImaging.TBL_Exportacion.DBInsert(ExportacionType)

        '                        Dim FileType = New TBL_FileType()
        '                        FileType.Exportado = True
        '                        FileType.fk_Exportacion = ExportacionType.id_Exportacion

        '                        For Each DataExportarRow In DataExportarDataTable
        '                            DBMCore.SchemaImaging.TBL_File.DBUpdate(FileType, DataExportarRow.fk_Expediente, DataExportarRow.fk_Folder, DataExportarRow.fk_File, DataExportarRow.id_Version)
        '                        Next

        '                        DBMCore.Transaction_Commit()
        '                    Catch ex As Exception
        '                        DBMCore.Transaction_Rollback()
        '                        Throw ex
        '                    End Try

        '                Catch ex As Exception
        '                    MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)

        '                    ProgressForm.Hide()
        '                    Application.DoEvents()

        '                    Return
        '                Finally
        '                    DBMImaging.Connection_Close()
        '                    DBMCore.Connection_Close()

        '                    ProgressForm.Close()
        '                End Try
        '            End If

        '            MessageBox.Show("La información se exportó con éxito", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Information)


        '        Else
        '            MessageBox.Show("No se encontraron registros para exportar", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '        End If
        '    End If
        'End Sub

    End Class

End Namespace