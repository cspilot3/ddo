Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Library
Imports System.IO
Imports System.ComponentModel
Imports System.Windows.Forms
Imports Slyg.Tools.CSV
Imports System.Threading

Namespace Forms.Risk
    Public Class FormAdmonPuntos
        Inherits Library.FormBase

#Region " Declaraciones "

        Private _File As Stream = Nothing
        Private _DataFile As DataTable = Nothing
        Private _DataRegistros As Integer = 0
        Private _DataColumnas As Integer = 0
        'Private _EstadoProceso As Short = 0 '0 Validando, 1 Procesando.
        Private objCSV As New Slyg.Tools.CSV.CSVData
        Private objXLS As New XLSData
        Private trResultado As DesktopConfig.TypeResult
        'Private _TipoCargue As DesktopConfig.TipoCargue
        Private Segundos As Integer = 0
        Private Minuto As Integer = 0
        Private Hora As Integer = 0


        Private HiloActivarControles As Thread = Nothing

#End Region

#Region " Funciones "
        Public Sub CargarCombos()
            Dim dmImaging As New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
            dmImaging.Connection_Open(Program.Sesion.Usuario.id)


            Dim Entidad = dmImaging.SchemaSecurity.CTA_Entidad.DBGet(0, New DBImaging.SchemaSecurity.CTA_EntidadEnumList(DBImaging.SchemaSecurity.CTA_EntidadEnum.Nombre_Entidad, True))
            Utilities.LlenarCombo(fk_entidadDesktopComboBox, Entidad, Entidad.id_EntidadColumn.ColumnName, Entidad.Nombre_EntidadColumn.ColumnName)

            Dim Proyecto = dmImaging.SchemaCore.CTA_Proyecto.DBFindByfk_Entidad(CShort(fk_entidadDesktopComboBox.SelectedValue))
            Utilities.LlenarCombo(fk_proyectoDesktopComboBox, Proyecto, Proyecto.id_ProyectoColumn.ColumnName, Proyecto.Nombre_ProyectoColumn.ColumnName)


            dmImaging.Connection_Close()
        End Sub
#End Region

#Region " Eventos "
        Private Sub FormAdmonPuntos_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            CargarCombos()
        End Sub

        Private Sub fk_entidadDesktopComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles fk_entidadDesktopComboBox.SelectedIndexChanged
            Dim dmImaging As New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
            dmImaging.Connection_Open(Program.Sesion.Usuario.id)

            Dim Proyecto = dmImaging.SchemaCore.CTA_Proyecto.DBFindByfk_Entidad(CShort(fk_entidadDesktopComboBox.SelectedValue))
            Utilities.LlenarCombo(fk_proyectoDesktopComboBox, Proyecto, Proyecto.id_ProyectoColumn.ColumnName, Proyecto.Nombre_ProyectoColumn.ColumnName)

            dmImaging.Connection_Close()
        End Sub


        Private Sub BuscarPunto_Click(sender As System.Object, e As System.EventArgs) Handles BuscarPunto.Click
            Dim dbmArchiving As New DBArchiving.DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

            Try

                Dim PuntoDatatable = dbmArchiving.SchemaRisk.TBL_Puntos_Cliente.DBFindByFk_EntidadFk_Proyecto(CInt(fk_entidadDesktopComboBox.SelectedValue), CInt(fk_proyectoDesktopComboBox.SelectedValue))
                PuntosClienteDataGridView.DataSource = PuntoDatatable

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Buscar Punto", ex)
            Finally
                If Not dbmArchiving Is Nothing Then dbmArchiving.Connection_Close()
            End Try

        End Sub

        Private Sub BuscarArchivoButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BuscarArchivoButton.Click
            BuscarArchivo()
        End Sub

        Private Sub ArchivoDesktopTextBox_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ArchivoDesktopTextBox.Click
            BuscarArchivo()
        End Sub


        Private Sub CargarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CargarButton.Click

            'Me.CargueBackgroundWorker.WorkerSupportsCancellation = True

            Dim dbmArchiving = New DBArchiving.DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            Try
                dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

                If Not IsNothing(_File) Then
                    'Valida si el nombre del archivo ya fué cargado.
                    Dim NombreArchivo As String = Path.GetFileName(CType(_File, FileStream).Name)


                    HabilitarControles(False)
                    'Me.CargueBackgroundWorker.RunWorkerAsync(NombreArchivo & "-" & fk_entidadDesktopComboBox.SelectedValue.ToString & _
                    '"-" & fk_proyectoDesktopComboBox.SelectedValue.ToString)
                    Dim Respuesta = CargarArchivo(NombreArchivo, CInt(fk_entidadDesktopComboBox.SelectedValue), CInt(fk_proyectoDesktopComboBox.SelectedValue))

                    If Not Respuesta.Result Then
                        Dim msg As String = ""
                        For Each ln In Respuesta.Parameters
                            msg = msg & vbCrLf & ln.ToString
                        Next
                        DesktopMessageBoxControl.DesktopMessageShow(msg, "Archivo de Cargue", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                    Else
                        DesktopMessageBoxControl.DesktopMessageShow("Cargue Finalizado", "Archivo de Cargue", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                    End If



                    HabilitarControles(True)
                Else
                    DesktopMessageBoxControl.DesktopMessageShow("Debe seleccionar un archivo de cargue.", "Archivo de cargue", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                End If

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargarButton_Click", ex)
            Finally
                dbmArchiving.Connection_Close()
            End Try

        End Sub


#End Region

#Region " Metodos "

        Private Sub BuscarArchivo()
            Segundos = 0
            Minuto = 0
            Hora = 0

            Try
                Dim Respuesta = ArchivoOpenFileDialog.ShowDialog()

                If Respuesta = DialogResult.OK Then
                    Try
                        ArchivoDesktopTextBox.Text = ArchivoOpenFileDialog.FileName

                        'Si el archivo es txt o csv, habilita la opción de manejar separador
                        If ArchivoOpenFileDialog.FileName.ToLower().EndsWith(".txt") OrElse ArchivoOpenFileDialog.FileName.ToLower().EndsWith(".csv") Then
                            OpcionesSeparadorGroupBox.Enabled = True
                        Else
                            OpcionesSeparadorGroupBox.Enabled = False
                        End If

                        _File = ArchivoOpenFileDialog.OpenFile()
                    Catch ex As Exception
                        DesktopMessageBoxControl.DesktopMessageShow("BuscarArchivo", ex)
                    Finally
                        If _File IsNot Nothing Then
                            _File.Close()
                        End If
                    End Try

                ElseIf Respuesta = DialogResult.Cancel Then
                    OpcionesSeparadorGroupBox.Enabled = False
                    ArchivoDesktopTextBox.Text = ""
                    _File = Nothing
                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("BuscarArchivo", ex)
            End Try
        End Sub

        Private Sub HabilitarControles(ByVal valor As Boolean)
            Me.ArchivoDesktopTextBox.Enabled = valor
            Me.BuscarArchivoButton.Enabled = valor
            Me.CargarButton.Enabled = valor
        End Sub

        Private Sub HabilitarControles()
            HabilitarControles(True)
        End Sub

#End Region

#Region " Funciones "

        Private Function CargarArchivo(ByVal NombreArchivo As String, ByVal Entidad As Integer, ByVal Proyecto As Integer) As DesktopConfig.TypeResult
            Dim trReturn As New DesktopConfig.TypeResult
            trReturn.Result = True
            Try
                'Se valida el tipo de archivo, si es CSV,TXT o XLS
                If OpcionesSeparadorGroupBox.Enabled Then 'Es plano CSV o TXT
                    'Se obtiene el separador
                    If ComaRadioButton.Checked Then objCSV.Separator = CChar(",")
                    If TabuladorRadioButton.Checked Then objCSV.Separator = ControlChars.Tab
                    If PuntoComaRadioButton.Checked Then objCSV.Separator = CChar(";")

                    'Se realiza el cargue del archivo en un datatable para luego validarlo.
                    objCSV.LoadCSV(CType(_File, FileStream).Name, chkEncabezado.Checked)
                    _DataFile = objCSV.DataTable.ToDataTable()
                Else 'Es XLS
                    Dim dsHojas As DataSet = objXLS.ImportExcelXLS(CType(_File, FileStream).Name, chkEncabezado.Checked)
                    _DataFile = dsHojas.Tables(0)
                End If

                Dim trRespuestaCargue As New DesktopConfig.TypeResult

                _DataRegistros = _DataFile.Rows.Count
                _DataColumnas = _DataFile.Columns.Count

                If _DataRegistros > 0 And _DataColumnas = 8 Then
                    CargueBackgroundWorker.ReportProgress(0)

                    trRespuestaCargue = ProcesarCargue(_DataFile, NombreArchivo, Entidad, Proyecto)

                    If trRespuestaCargue.Result Then
                        trReturn = trRespuestaCargue
                    Else
                        trReturn.Result = False
                        trReturn.Parameters = trRespuestaCargue.Parameters
                        trReturn.Resumen = trRespuestaCargue.Resumen
                    End If
                Else
                    trReturn.Result = False
                    Dim lisMsgError = New List(Of String)
                    lisMsgError.Add("- Archivo no válido. Por favor verifique lo siguiente:")
                    lisMsgError.Add("- 1. Que el archivo contenga datos.")
                    lisMsgError.Add("- 2. El carácter de separación sea el adecuado.")
                    lisMsgError.Add("- 3. La cantidad de columnas debe ser correcta.")
                    trReturn.Parameters = lisMsgError

                End If
            Catch ex As Exception
                Dim lisMsgError = New List(Of String)
                lisMsgError.Add("- Error: " & ex.Message)
                trReturn.Result = False
                trReturn.Parameters = lisMsgError
            End Try

            Return trReturn
        End Function


        Public Function ProcesarCargue(ByVal data As DataTable, ByVal fileName As String, ByVal Entidad As Integer, ByVal Proyecto As Integer) As DesktopConfig.TypeResult
            Dim dbmArchiving As DBArchiving.DBArchivingDataBaseManager = Nothing
            Dim trRetrun As New DesktopConfig.TypeResult

            Try
                dbmArchiving = New DBArchiving.DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
                dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

                Const tabla As String = "#Puntos"
                BulkInsert.InsertDataTable(data, dbmArchiving, tabla)

                '1.
                dbmArchiving.Transaction_Begin()
                'Valida e inserta los datos cargados
                Dim ResultCargue = dbmArchiving.SchemaRisk.PA_Puntos_Cliente.DBExecute(Entidad, Proyecto)

                If ResultCargue.Rows.Count > 0 Then
                    Dim Actualizados = ResultCargue.Rows(0)(0).ToString
                    Dim Insertados = ResultCargue.Rows(0)(1).ToString

                    TotalRegistrosLabel.Text = Convert.ToString(CInt(Actualizados) + CInt(Insertados))
                    ActualizadosLabel.Text = Actualizados
                    InsertadosLabel.Text = Insertados

                    trRetrun.Result = True
                Else
                    MsgBox("Hubo un error al procesar el archivo, verifique su estructura e intentelo nuevamente")
                End If

                dbmArchiving.Transaction_Commit()

            Catch
                '4.
                dbmArchiving.Transaction_Rollback()
                Throw
            Finally
                If (dbmArchiving IsNot Nothing) Then dbmArchiving.Connection_Close()
            End Try

            Return trRetrun
        End Function

#End Region

#Region "BackgroundWorker"

        Private Sub CargueBackgroundWorker_DoWork(ByVal sender As System.Object, ByVal e As DoWorkEventArgs) Handles CargueBackgroundWorker.DoWork
            Dim worker As BackgroundWorker = CType(sender, BackgroundWorker)
            If worker.CancellationPending Then e.Cancel = True

            'Se obtiene entidad, proyecto
            Dim Parametros = e.Argument.ToString().Split(CChar("-"))
            Dim NombreArchivo As String = ""

            If Parametros.Length > 3 Then
                'Dim Nombre = Parametros.Length - 3

                For i As Integer = 0 To Parametros.Length - 3 Step 1
                    NombreArchivo = NombreArchivo & " - " & Parametros(i)
                Next

            Else
                NombreArchivo = Parametros(0)
            End If

            Dim Entidad = CShort(Parametros(Parametros.Length - 2))
            Dim Proyecto = CShort(Parametros(Parametros.Length - 1))

            trResultado = CargarArchivo(NombreArchivo, Entidad, Proyecto)
            If Not trResultado.Result Then

                Dim msg As String = ""
                For Each ln In trResultado.Parameters
                    msg = msg & vbCrLf & ln.ToString
                Next
                MessageBox.Show(msg)
            End If




        End Sub

        


#End Region



    End Class
End Namespace