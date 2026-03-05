Imports System.ComponentModel
Imports System.Globalization
Imports System.IO
Imports System.Threading
Imports System.Windows.Forms
Imports Slyg.Tools.CSV
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports DBArchiving
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Config
Imports BCS.Plugin.Imaging.Carpeta_Unica
Imports DBIntegration
Imports System.Dynamic
Imports DBImaging
Imports DMB = Miharu.Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl

Namespace Imaging.Carpeta_Unica.Forms.FormsCargue
    Public Class FormCargueBCS

#Region " Declaraciones "

        Private _File As Stream = Nothing
        Private _DataFile As DataTable = Nothing
        Private _DataRegistros As Integer = 0
        Private _DataColumnas As Integer = 0
        Private _EstadoProceso As Short = 0 '0 Validando, 1 Procesando.
        Private objCSV As New Slyg.Tools.CSV.CSVData
        Private objXLS As New XLSData
        Private trResultado As DesktopConfig.TypeResult
        Private _TipoCargue As DesktopConfig.TipoCargue
        Private Segundos As Integer = 0
        Private Minuto As Integer = 0
        Private Hora As Integer = 0
        Private _Plugin As CarpetaUnicaPlugin
        Dim ltLineasCamposInsertar As List(Of Object) = New List(Of Object)
        Dim _fechaProceso As Date
        Dim _fechaProcesoEjecucion As Date
        Dim ProcesaLog As New ProcesaCargueCarpetaUnica
        Dim _idCargue As Integer
        Dim fk_tipo_Proceso As Integer
        Dim fk_Tipo_Log As Integer
        Dim JornadaInt_Global As Integer
        Dim JornadaVarchar_Global As String
        Dim JornadaVarchar_DeArchivo As String
        Dim IdCargue As Integer
        Dim CargueValido As Boolean
        Dim nTempPathFile_Microfinanzas As String
        Dim ExtensionGlobal As String
        Dim passwordFile As String
        Dim ltMicrofinanzas As New List(Of String)
        Dim ltGoroVolumen As New List(Of String)
        Dim _tipoLog_ValidaDuplicados As Boolean
        Dim _contadorNroProductos_vacios As Integer = 0
        Dim _NombreProceso As String
        Dim _validaExtension As Boolean
        Dim _nombreTipoLog As String
        Dim _ValorCampoFechaApertura_ExcelActualizaciones As String


#End Region

#Region " Propiedades "

        Public Property NewFechaProceso As Integer

#End Region

#Region " Constructor "

        Public Sub New(ByVal nCajaSocialImaginPlugin As CarpetaUnicaPlugin)
            InitializeComponent()
            _Plugin = nCajaSocialImaginPlugin
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
            ArchivoDesktopTextBox.Enabled = valor
            BuscarArchivoButton.Enabled = valor
            CargarButton.Enabled = valor
        End Sub

        Private Sub CargaDatos()
            Dim dbmIntegration As New DBIntegrationDataBaseManager(Me._Plugin.CajaSocialConnectionString)
            Try
                dbmIntegration.Connection_Open(Me._Plugin.Manager.Sesion.Usuario.id)
                Dim Procesos = dbmIntegration.SchemaBCSCarpetaUnica.TBL_Tipo_Proceso.DBFindByUsa_Cargue_logActivo(True, True)

                If Procesos IsNot Nothing Then
                    If Procesos.Rows.Count > 0 Then
                        DesktopComboBoxControlProcesos.DisplayMember = Procesos.Nombre_Tipo_ProcesoColumn.ToString()
                        DesktopComboBoxControlProcesos.ValueMember = Procesos.id_Tipo_ProcesoColumn.ToString()
                        DesktopComboBoxControlProcesos.DataSource = Procesos
                    End If
                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaDatos", ex)
            Finally
                dbmIntegration.Connection_Close()
            End Try
        End Sub

        Private Sub CargaTipoJornadaProceso()
            Dim dbmIntegration As New DBIntegrationDataBaseManager(Me._Plugin.CajaSocialConnectionString)
            Try
                dbmIntegration.Connection_Open(Me._Plugin.Manager.Sesion.Usuario.id)
                Dim Jornadas = dbmIntegration.SchemaBCSCarpetaUnica.TBL_Tipo_Jornada.DBGet(Nothing)
                Dim TipoJornadaProcesoView = Jornadas.DefaultView
                TipoJornadaProcesoView.RowFilter = "fk_Tipo_Proceso = " + DesktopComboBoxControlProcesos.SelectedValue.ToString()

                If (TipoJornadaProcesoView IsNot Nothing) Then
                    If TipoJornadaProcesoView.ToTable().Rows.Count > 0 Then
                        Dim jornadas_aux = TipoJornadaProcesoView.ToTable()
                        DesktopComboBoxControlJornada.DisplayMember = "Nombre_Tipo_Jornada"
                        DesktopComboBoxControlJornada.ValueMember = "id_Tipo_Jornada"
                        DesktopComboBoxControlJornada.DataSource = jornadas_aux
                        DesktopComboBoxControlJornada.Enabled = True
                    Else
                        DesktopComboBoxControlJornada.DataSource = Nothing
                        DesktopComboBoxControlJornada.Enabled = False
                    End If
                End If

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaJornada", ex)
            Finally
                dbmIntegration.Connection_Close()
            End Try
        End Sub

        Private Sub CargaTiposLog()
            Dim dbmIntegration As New DBIntegrationDataBaseManager(Me._Plugin.CajaSocialConnectionString)
            Try
                dbmIntegration.Connection_Open(Me._Plugin.Manager.Sesion.Usuario.id)
                Dim TiposLog = dbmIntegration.SchemaConfig.TBL_Tipos_Log.DBGet(Nothing, _Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto)
                Dim TiposLogView = TiposLog.DefaultView
                Dim jornadaInt As Integer
                Dim TipoProceso = DesktopComboBoxControlProcesos.Text.ToUpper().TrimEnd().TrimStart().ToUpper()

                If (TipoProceso = "MICROFINANZAS") Then
                    Me.OpcionesSeparadorGroupBox.Enabled = True
                    Me.TabuladorRadioButton.Checked = True
                    Me.chkEncabezado.Checked = False
                ElseIf (TipoProceso = "ACUSES DE RECIBIDO" Or TipoProceso = "FATCA" Or TipoProceso = "AINCO" Or TipoProceso = "PRODUCTOS CRUZADOS" Or TipoProceso = "CONSTRUCTOR") Then
                    Me.chkEncabezado.Checked = True
                    Me.OpcionesSeparadorGroupBox.Enabled = False
                    Me.TabuladorRadioButton.Checked = False
                ElseIf (TipoProceso = "GORO VOLUMEN") Then
                    Me.TabuladorRadioButton.Checked = True
                    Me.chkEncabezado.Checked = False
                    Me.OpcionesSeparadorGroupBox.Enabled = True
                ElseIf (TipoProceso = "CREDITOS DE CONSUMO" Or TipoProceso = "CREDITOS ROTATIVOS") Then
                    Me.OpcionesSeparadorGroupBox.Enabled = True
                    Me.PuntoComaRadioButton.Checked = True
                    Me.chkEncabezado.Checked = False
                    Me.TabuladorRadioButton.Checked = False
                Else
                    Me.OpcionesSeparadorGroupBox.Enabled = False
                End If

                If (DesktopComboBoxControlJornada.Items.Count > 0) Then
                    If (DesktopComboBoxControlJornada.Text.ToUpper() = "NORMAL") Then
                        jornadaInt = 1
                    Else
                        jornadaInt = 2
                    End If
                    TiposLogView.RowFilter = "fk_Tipo_Proceso = " + DesktopComboBoxControlProcesos.SelectedValue.ToString() + " AND fk_Tipo_Jornada=" + jornadaInt.ToString() + " AND Visible = 1"
                Else
                    TiposLogView.RowFilter = "fk_Tipo_Proceso = " + DesktopComboBoxControlProcesos.SelectedValue.ToString() + " AND Visible = 1"
                End If

                If (TiposLogView IsNot Nothing) Then
                    If TiposLogView.ToTable().Rows.Count > 0 Then
                        Dim tiposlog_aux = TiposLogView.ToTable()
                        DesktopComboBoxControlTiposLog.DisplayMember = "Nombre_Tipo_Log"
                        DesktopComboBoxControlTiposLog.ValueMember = "id_Tipo_Log"
                        DesktopComboBoxControlTiposLog.DataSource = tiposlog_aux
                    End If
                End If

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaJornada", ex)
            Finally
                dbmIntegration.Connection_Close()
            End Try
        End Sub

        Private Sub ValidarExiste_ArchivoCargue(NombreArchivo As String, dbmIntegration As DBIntegrationDataBaseManager)
            Try
                If (dbmIntegration.SchemaConfig.TBL_Cargue.DBFindByArchivo_CargueValido(NombreArchivo, True).Rows.Count > 0) Then
                    DesktopMessageBoxControl.DesktopMessageShow("El nombre de archivo que estan intentando cargar ya se encuentra registrado con un Cargue Valido.", "Archivo de Cargue existente", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                    Exit Sub
                End If

                Dim fechaProceso = Integer.Parse(Me.dtpFechaProceso.Value.ToString("yyyyMMdd"))
                Dim dtCargues = dbmIntegration.SchemaConfig.TBL_Cargue.DBFindByfk_Entidadfk_Proyectofk_Tipo_LogArchivo_CargueValidoFecha_Proceso(_Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, Me.fk_Tipo_Log, NombreArchivo, True, Nothing)
                If dtCargues.Count > 0 Then
                    DesktopMessageBoxControl.DesktopMessageShow("El nombre de archivo que estan intentando cargar ya se encuentra registrado, por favor seleccione otro archivo de cargue.", "Archivo de Cargue existente", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                    _File = Nothing
                    ArchivoDesktopTextBox.Text = ""
                Else
                    If (VerificarFechaProceso() And ValidarFechaProcesoEjecucion()) Then

                        If (Path.GetExtension(CType(_File, FileStream).Name).ToLower().StartsWith(".xl")) Then
                            If (MessageBox.Show("¿Este archivo maneja contraseña?", "Archivo con Password", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes) Then
                                Me.passwordFile = InputBox("Digite la contraseña: ", "Password", "")
                            Else
                                Me.passwordFile = ""
                            End If
                        End If

                        Me.CargandoPictureBox.Visible = True
                        Timer1.Enabled = True
                        HabilitarControles(False)
                        Me.CargueBackgroundWorker.RunWorkerAsync(NombreArchivo & "-" & _Plugin.Manager.ImagingGlobal.Entidad.ToString() + "-" + _Plugin.Manager.ImagingGlobal.Proyecto.ToString() + "-" + Me.fk_Tipo_Log.ToString())
                    End If
                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("ValidarExiste ArchivoCargue", ex)
            End Try
        End Sub


        Private Sub CargaDataTable_ConSeparador(ByRef Data As DataTable, Optional ByVal PathFileName As String = "")
            'Se obtiene el separador
            If ComaRadioButton.Checked Then objCSV.Separator = CChar(",")
            If TabuladorRadioButton.Checked Then objCSV.Separator = ControlChars.Tab
            If PuntoComaRadioButton.Checked Then objCSV.Separator = CChar(";")

            'Se realiza el cargue del archivo en un datatable para luego validarlo.
            objCSV.LoadCSV(PathFileName, chkEncabezado.Checked)
            Data = objCSV.DataTable.ToDataTable()

        End Sub

        Private Sub CargaArchivoExcel(Optional ByVal UsarReferencia As Boolean = Nothing, Optional ByRef dtRef As DataTable = Nothing, Optional ByRef dsref As DataSet = Nothing)
            Dim nTempPath = CarpetaUnicaPlugin.AppPath() & CarpetaUnicaPlugin.TempPath
            If (UsarReferencia) Then
                If (dtRef IsNot Nothing) Then
                    dtRef = objXLS.ReadExcelIntoDataTable(CType(_File, FileStream).Name, nTempPath, chkEncabezado.Checked, Me.passwordFile)
                ElseIf (dsref IsNot Nothing) Then
                    objXLS.ReadExcelIntoDataTable(CType(_File, FileStream).Name, nTempPath, chkEncabezado.Checked, Me.passwordFile, Nothing, dsref)
                End If

            Else
                _DataFile = objXLS.ReadExcelIntoDataTable(CType(_File, FileStream).Name, nTempPath, chkEncabezado.Checked, Me.passwordFile)
            End If

        End Sub

#End Region

#Region " Funciones "

        Private Function ValidaFormatoCorrecto_Log(ByVal IdTipoLog As Integer, ByVal NombreArchivo As String, ByVal RowTipoLog As DBIntegration.SchemaConfig.TBL_Tipos_LogRow)
            Dim retorno As Boolean = True
            Dim NombreArchivo_Aux As String = ""
            If (Me._validaExtension And NombreArchivo.Contains(".")) Then
                NombreArchivo_Aux = NombreArchivo.Remove(NombreArchivo.IndexOf("."), NombreArchivo.Length - NombreArchivo.IndexOf("."))
            Else
                NombreArchivo_Aux = NombreArchivo
            End If

            Dim dateVariable As Date
            Dim NombreProceso = Me.DesktopComboBoxControlProcesos.Text.ToUpper().TrimEnd().TrimStart()

            Select Case (NombreProceso)
                Case "PASIVO"

                    Dim PrimeraValidacion_Aux = NombreArchivo_Aux.Substring(0, 6)
                    If (PrimeraValidacion_Aux.ToUpper() <> "CTACOR") Then
                        Return False
                    End If

                    Dim NombreArchivo_reducido = NombreArchivo_Aux.Remove(0, 6)
                    NombreArchivo_reducido = NombreArchivo_reducido.Remove(NombreArchivo_reducido.Length - 1, 1)
                    If (NombreArchivo_reducido IsNot Nothing) Then
                        Dim fechaExtraida = NombreArchivo_reducido

                        If (Date.TryParseExact(fechaExtraida, "yymmdd", Nothing, DateTimeStyles.None, dateVariable) = False) Then
                            Return False
                        End If
                    Else
                        Return False
                    End If
                Case "DOE"
                    Dim PrimeraValidacion_Aux = NombreArchivo_Aux.Substring(0, 8)
                    If (PrimeraValidacion_Aux.ToUpper() <> "BCSSPL18") Then
                        Return False
                    End If

                    Dim NombreArchivo_reducido = NombreArchivo_Aux.Remove(0, 8)
                    If (NombreArchivo_reducido IsNot Nothing) Then
                        Dim fechaExtraida = NombreArchivo_reducido

                        If (Date.TryParseExact(fechaExtraida, "ddmmyyyy", Nothing, DateTimeStyles.None, dateVariable) = False) Then
                            Return False
                        End If
                    Else
                        Return False
                    End If
                Case "MICROFINANZAS"
                    Dim PrimeraValidacion_Aux = NombreArchivo_Aux.Substring(0, 7)
                    If (PrimeraValidacion_Aux.ToUpper() <> "DESBCS_") Then
                        Return False
                    End If

                    Dim NombreArchivo_reducido = NombreArchivo_Aux.Remove(0, 7)
                    If (NombreArchivo_reducido IsNot Nothing) Then
                        Dim fechaExtraida = NombreArchivo_reducido

                        If (Date.TryParseExact(fechaExtraida, "ddmmyyyy", Nothing, DateTimeStyles.None, dateVariable) = False) Then
                            Return False
                        End If
                    Else
                        Return False
                    End If
                Case "ACUSES DE RECIBIDO"
                    Dim PrimeraValidacion_Aux = NombreArchivo_Aux.Substring(0, 7)
                    If (PrimeraValidacion_Aux.ToUpper() <> "BCS_AR_") Then
                        Return False
                    End If

                    Dim NombreArchivo_reducido = NombreArchivo_Aux.Remove(0, 7)
                    If (NombreArchivo_reducido IsNot Nothing) Then
                        Dim fechaExtraida = NombreArchivo_reducido


                        If (Date.TryParseExact(fechaExtraida, "ddmmyyyy", Nothing, DateTimeStyles.None, dateVariable) = False) Then
                            Return False
                        End If
                    Else
                        Return False
                    End If
                Case "GORO VOLUMEN"
                    Dim primeraValidacion = NombreArchivo_Aux.Substring(0, 9)
                    If (primeraValidacion.ToString().ToUpper() <> "APERTURAS") Then
                        Return False
                    End If

                    Dim SegundaValidacion = NombreArchivo_Aux.Substring(9, 2)
                    Dim intVariable As Integer
                    If (Integer.TryParse(SegundaValidacion, intVariable) = False) Then
                        Return False
                    End If

                    Dim TerceraValidacion = NombreArchivo_Aux.Substring(11, NombreArchivo_Aux.Length - 11)
                    Dim strMeses() As String = DateTimeFormatInfo.CurrentInfo.MonthNames
                    Dim MesEncontrado = strMeses.Where(Function(x) x.ToString().ToUpper() = TerceraValidacion.ToString().ToUpper()).Select(Function(x) x.ToString())
                    If (MesEncontrado Is Nothing) Then
                        Return False
                    End If
                Case "FATCA"
                    Dim primeraValidacion = NombreArchivo_Aux.Substring(0, 6)
                    If (primeraValidacion.ToString().ToUpper() <> "FATCA_") Then
                        Return False
                    End If

                    Dim SegundaValidacion = NombreArchivo_Aux.Substring(primeraValidacion.Length, 8)
                    If (Date.TryParseExact(SegundaValidacion, "yyyyMMdd", Nothing, DateTimeStyles.None, dateVariable) = False) Then
                        Return False
                    End If
                Case Else
                    If (RowTipoLog IsNot Nothing) Then
                        ValidacionesSplit(retorno, RowTipoLog.Validaciones_ArchivoPlano.Split("|"), RowTipoLog.LengthValidaciones_ArchivoPlano.Split("|"), NombreArchivo_Aux)
                    End If
            End Select

            Return retorno
        End Function

        Private Function ValidacionesSplit(ByRef retorno As Boolean, StrValidacionColumn() As String, StrLengthValidaciones() As String, NombreArchivo_Aux As String)
            Try
                If (StrValidacionColumn.Count > 0) Then
                    For index = 0 To StrValidacionColumn.Count - 1
                        Dim AuxItemValidacion As String
                        If (StrValidacionColumn(index).ToString().Contains("FECHA")) Then
                            AuxItemValidacion = StrValidacionColumn(index).ToString()
                        Else
                            AuxItemValidacion = StrValidacionColumn(index).ToString().ToUpper()
                        End If
                        Dim LengthItemValidacion = StrLengthValidaciones(index).Split("-")
                        Dim lengthInicio = CInt(LengthItemValidacion(0).ToString())
                        Dim lengthFinal = CInt(LengthItemValidacion(1).ToString())
                        Dim auxArchivoVal As String

                        If (AuxItemValidacion.Contains("*")) Then
                            Dim OrAnd = AuxItemValidacion.Split("*")
                            If (OrAnd.Contains("OR") And Not OrAnd.Contains("AND")) Then
                                OrAnd = OrAnd.Where(Function(x) Not x.Contains("OR")).ToArray()
                                For Each itemOr In OrAnd
                                    auxArchivoVal = NombreArchivo_Aux.Substring(lengthInicio, (lengthFinal - lengthInicio)).ToUpper()
                                    If (auxArchivoVal = itemOr.ToUpper()) Then
                                        retorno = True
                                    End If
                                Next
                            End If
                        ElseIf (AuxItemValidacion.Contains("<FECHA>")) Then
                            Dim dtAux As New DateTime
                            auxArchivoVal = NombreArchivo_Aux.Substring(lengthInicio, (lengthFinal - lengthInicio))
                           
                            AuxItemValidacion = AuxItemValidacion.Replace("<FECHA>", "")
                            retorno = DateTime.TryParseExact(auxArchivoVal, AuxItemValidacion, Nothing, DateTimeStyles.None, dtAux)

                            If (Me._nombreTipoLog = "CONSTRUCTOR EXCEL") Then
                                Me._ValorCampoFechaApertura_ExcelActualizaciones = dtAux.ToString("yyyyMMdd")
                            End If

                            If (retorno = False) Then
                                Exit For
                            End If
                        Else
                            auxArchivoVal = NombreArchivo_Aux.Substring(lengthInicio, (lengthFinal - lengthInicio)).ToUpper()
                            If (auxArchivoVal <> AuxItemValidacion) Then
                                retorno = False
                                Exit For
                            End If
                        End If
                    Next
                End If
            Catch ex As Exception
                retorno = False
            End Try

            Return retorno
        End Function

        Private Function ValidarTipoLog() As Boolean
            Dim retorno As Boolean = True
            Try
                If (DesktopComboBoxControlTiposLog.SelectedIndex = 0) Then
                    DesktopMessageBoxControl.DesktopMessageShow("Debe seleccionar un Tipo de Log.", "Seleccionar Tipo Log", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                    DesktopComboBoxControlTiposLog.Focus()
                    retorno = False
                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("ValidarTipoLog", ex)
            End Try
            Return retorno
        End Function

        Private Function ValidarJornada(NombreArchivo As String, dbmIntegration As DBIntegrationDataBaseManager) As Boolean
            Dim retorno As Boolean = True

            Try
                Dim JornadaVarchar = Me.DesktopComboBoxControlJornada.Text
                Dim NombreArchivo_Aux = NombreArchivo.Remove(NombreArchivo.IndexOf("."), NombreArchivo.Length - NombreArchivo.IndexOf("."))
                Dim JornadaVarchar_DeArchivo = NombreArchivo_Aux.Substring(NombreArchivo_Aux.Length - 1, 1)
                Me.JornadaInt_Global = CInt(Me.DesktopComboBoxControlJornada.SelectedValue)
                Me.JornadaVarchar_Global = JornadaVarchar
                Me.JornadaVarchar_DeArchivo = JornadaVarchar_DeArchivo

                If (JornadaVarchar.ToUpper() = "NORMAL") Then
                    If (JornadaVarchar_DeArchivo.ToUpper() = "A") Then
                        DesktopMessageBoxControl.DesktopMessageShow("Error de coincidencia en la jornada de Archivo y jornada seleccionada, verifique la información.", "Error de Jornada", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                        Me.DesktopComboBoxControlJornada.Focus()
                        Return False
                    End If
                ElseIf (JornadaVarchar.ToUpper() = "ADICIONAL") Then
                    If (JornadaVarchar_DeArchivo = "N") Then
                        DesktopMessageBoxControl.DesktopMessageShow("Error de coincidencia en la jornada de Archivo y jornada seleccionada, verifique la información.", "Error de Jornada", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                        Me.DesktopComboBoxControlJornada.Focus()
                        Return False
                    End If
                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("ValidarJornada", ex)
            End Try
            Return retorno
        End Function

        Private Function ValidarExtension(Extension As String, dbmIntegration As DBIntegrationDataBaseManager) As Boolean
            Dim retorno As Boolean = True
            Try
                If (Extension = "") Then
                    DesktopMessageBoxControl.DesktopMessageShow("La extensión del archivo no es correcta para este Tipo de Log, por favor seleccione otro archivo de cargue.", "Error de Extensión", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                    Return False
                Else
                    Dim EncontradoExtension = dbmIntegration.SchemaConfig.TBL_Tipos_Log.DBFindByfk_Entidadfk_Proyectofk_Tipo_Procesoid_Tipo_log(_Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, fk_tipo_Proceso, Me.fk_Tipo_Log)

                    If (EncontradoExtension.Count > 0) Then
                        Dim extensionFile As String = EncontradoExtension.Rows(0).Item("Extension_Archivo").ToString()
                        Dim extensionFileSplit = extensionFile.Split("|")

                        Dim encontradoCru = extensionFileSplit.Where(Function(x) x.ToUpper().Trim() = Extension.ToString().ToUpper().Trim())
                        If encontradoCru Is Nothing Then
                            DesktopMessageBoxControl.DesktopMessageShow("La extensión del archivo no es correcta para este Tipo de Log, por favor seleccione otro archivo de cargue.", "Error de Extensión", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                            Return False
                        End If
                    End If
                End If
                
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("ValidarExtension", ex)
            End Try
            Return retorno
        End Function

        Private Function Cargue_CTACORaammdd(ByVal NombreArchivo As String, ByVal Entidad As Short, ByVal Proyecto As Short, ByVal IdTipo_log As Integer) As DesktopConfig.TypeResult
            Dim trReturn As New DesktopConfig.TypeResult
            Dim dbmIntregation As New DBIntegrationDataBaseManager(_Plugin.CajaSocialConnectionString)
            trReturn.Result = True
            Dim lisMsgError = New List(Of String)
            Dim contiene01 As Boolean = False
            Try
                Dim contador As Integer = 1
                'Dim archivoTXT = File.ReadAllText(CType(_File, FileStream).Name, System.Text.Encoding.Default)
                If (CType(_File, FileStream).Name IsNot Nothing) Then
                    Dim lenghtInicio As Integer = 0

                    'TipoLog CTACORaammdd
                    Using st As StreamReader = New StreamReader(CType(_File, FileStream).Name, System.Text.Encoding.Default)
                        While (Not st.EndOfStream())
                            Dim Linea = st.ReadLine().ToString()

                            If (Linea.Contains("/01")) Then
                                contiene01 = True
                                Dim indexContains = Linea.LastIndexOf("/01")
                                Linea = Linea.Replace("/01", "")
                            Else
                                contiene01 = False
                            End If

                            If (Linea.Length <> 153 And contiene01 = False) Then
                                lisMsgError.Add("La linea #" + contador.ToString() + " del archivo esta errada debe tener 153 caracteres, Error de Cargue Log")
                            End If

                            If (Linea.Length < 153 And contiene01) Then
                                Linea = Linea.Insert(43, "   ")
                                If (Linea.Length <> 153) Then
                                    lisMsgError.Add("La linea #" + contador.ToString() + " del archivo esta errada debe tener 153 caracteres, Error de Cargue Log")
                                End If
                            End If

                            lenghtInicio = 0
                            Dim RegistroInsertar As Object = New ExpandoObject()
                            Dim lenghCompletoLinea = Linea.Length

                            Dim FechaApertura = Linea.Substring(0, 8)
                            lenghtInicio = FechaApertura.Length
                            RegistroInsertar.FechaApertura = FechaApertura

                            Dim Filler = Linea.Substring(lenghtInicio, 1)
                            lenghtInicio = lenghtInicio + Filler.Length
                            RegistroInsertar.Filler = Filler

                            Dim Codigo_Oficina = Linea.Substring(lenghtInicio, 4)
                            lenghtInicio = lenghtInicio + Codigo_Oficina.Length
                            RegistroInsertar.Codigo_Oficina = Codigo_Oficina

                            Dim Nombre_Oficina = Linea.Substring(lenghtInicio, 30)
                            lenghtInicio = lenghtInicio + Nombre_Oficina.Length
                            RegistroInsertar.Nombre_Oficina = Nombre_Oficina

                            Dim Numero_Producto = Linea.Substring(lenghtInicio, 20)

                            lenghtInicio = lenghtInicio + Numero_Producto.Length
                            RegistroInsertar.Numero_Producto = Numero_Producto

                            Dim Tipo_Identificacion = Linea.Substring(lenghtInicio, 2)
                            lenghtInicio = lenghtInicio + Tipo_Identificacion.Length
                            RegistroInsertar.Tipo_Identificacion = Tipo_Identificacion

                            Dim Numero_Identificacion = Linea.Substring(lenghtInicio, 18)
                            lenghtInicio = lenghtInicio + Numero_Identificacion.Length
                            RegistroInsertar.Numero_Identificacion = Numero_Identificacion

                            Dim Datos_Generales_Titular = Linea.Substring(lenghtInicio, 40)
                            lenghtInicio = lenghtInicio + Datos_Generales_Titular.Length
                            RegistroInsertar.Datos_Generales_Titular = Datos_Generales_Titular

                            Dim Codigo_Oficina_2 = Linea.Substring(lenghtInicio, 4)
                            lenghtInicio = lenghtInicio + Codigo_Oficina_2.Length
                            RegistroInsertar.Codigo_Oficina_2 = Codigo_Oficina_2

                            Dim Fecha_Sipla = Linea.Substring(lenghtInicio, 8)
                            lenghtInicio = lenghtInicio + Fecha_Sipla.Length
                            RegistroInsertar.Fecha_Sipla = Fecha_Sipla

                            Dim Execion_GMF = Linea.Substring(lenghtInicio, 1)
                            lenghtInicio = lenghtInicio + Execion_GMF.Length
                            RegistroInsertar.Execion_GMF = Execion_GMF

                            ' 'S(tiene cupo)' or 'N(no tiene cupo)'
                            Dim Indicador_Sobregiro = Linea.Substring(lenghtInicio, 1)
                            lenghtInicio = lenghtInicio + Indicador_Sobregiro.Length
                            RegistroInsertar.Indicador_Sobregiro = Indicador_Sobregiro

                            Dim Usuario_De_Creacion = Linea.Substring(lenghtInicio, 8)
                            lenghtInicio = lenghtInicio + Usuario_De_Creacion.Length
                            RegistroInsertar.Usuario_De_Creacion = Usuario_De_Creacion

                            Dim Origen_canal = Linea.Substring(lenghtInicio, 4)
                            lenghtInicio = lenghtInicio + Origen_canal.Length
                            RegistroInsertar.Origen_canal = Origen_canal

                            Dim Codigo_De_Subledger = Linea.Substring(lenghtInicio, 4)
                            lenghtInicio = lenghtInicio + Codigo_De_Subledger.Length
                            RegistroInsertar.Codigo_De_Subledger = Codigo_De_Subledger

                            If (contiene01) Then
                                RegistroInsertar.NumProductoOriginal = Numero_Producto.ToString() + "/01"
                            Else
                                RegistroInsertar.NumProductoOriginal = Numero_Producto
                            End If

                            ltLineasCamposInsertar.Add(RegistroInsertar)
                            contador += 1
                        End While
                    End Using
                    trReturn.Parameters = lisMsgError

                    If (trReturn.Parameters.Count > 0) Then
                        trReturn.Result = False
                    Else
                        _DataFile = New DataTable()
                        dbmIntregation.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
                        Dim CamposPorTipoLog = dbmIntregation.SchemaConfig.TBL_Campos_Tipo_log.DBGet(IdTipo_log, Entidad, Proyecto, Nothing, Nothing)
                        CargueBackgroundWorker.ReportProgress(0)

                        For Each itemCampo In CamposPorTipoLog
                            _DataFile.Columns.Add(itemCampo.Nombre_Campo)
                        Next

                        For index = 0 To ltLineasCamposInsertar.Count - 1
                            Dim Cod = ltLineasCamposInsertar(index).FechaApertura
                            Dim Cod1 = ltLineasCamposInsertar(index).Filler
                            Dim Cod2 = ltLineasCamposInsertar(index).Codigo_Oficina
                            Dim Cod3 = ltLineasCamposInsertar(index).Nombre_Oficina
                            Dim Cod4 = ltLineasCamposInsertar(index).Numero_Producto
                            Dim Cod5 = ltLineasCamposInsertar(index).Tipo_Identificacion
                            Dim Cod6 = ltLineasCamposInsertar(index).Numero_Identificacion
                            Dim Cod7 = ltLineasCamposInsertar(index).Datos_Generales_Titular
                            Dim Cod8 = ltLineasCamposInsertar(index).Codigo_Oficina_2
                            Dim Cod9 = ltLineasCamposInsertar(index).Fecha_Sipla
                            Dim Cod10 = ltLineasCamposInsertar(index).Execion_GMF
                            Dim Cod11 = ltLineasCamposInsertar(index).Indicador_Sobregiro
                            Dim Cod12 = ltLineasCamposInsertar(index).Usuario_De_Creacion
                            Dim Cod13 = ltLineasCamposInsertar(index).Origen_canal
                            Dim Cod14 = ltLineasCamposInsertar(index).Codigo_De_Subledger
                            Dim Cod15 = ltLineasCamposInsertar(index).NumProductoOriginal

                            _DataFile.Rows.Add(Cod, Cod1, Cod2, Cod3, Cod4, Cod5, Cod6, Cod7, Cod8, Cod9, Cod10, Cod11, Cod12, Cod13, Cod14, Cod15)
                        Next

                        Me.ltLineasCamposInsertar.Clear()

                        _DataRegistros = _DataFile.Rows.Count
                        _DataColumnas = _DataFile.Columns.Count

                        'If _DataRegistros > 0 And _DataColumnas > 1 Then
                            Dim Data_Log As New DBIntegration.SchemaLoad.TBL_Log_DataType
                            Data_Log.Fecha_Proceso = Me._fechaProceso.ToString("yyyyMMdd")
                            Data_Log.fk_Tipo_Log = Me.fk_Tipo_Log
                            Data_Log.fk_Tipo_Proceso = Me.fk_tipo_Proceso

                            Dim ProcesoEncontrado = dbmIntregation.SchemaBCSCarpetaUnica.TBL_Tipo_Proceso.DBGet(Me.fk_tipo_Proceso)
                            Dim ProcesoSigla_Aux As String = ""
                            If (ProcesoEncontrado.Rows.Count > 0) Then
                                ProcesoSigla_Aux = ProcesoEncontrado.Rows(0)("Sigla_Proceso")
                            End If
                            Data_Log.Proceso_Sigla = ProcesoSigla_Aux
                            Data_Log.Jornada = Me.JornadaVarchar_Global
                        Return ProcesaLog.ProcesaLogCajaSocial(_DataFile, Path.GetFileName(CType(_File, FileStream).Name), Me.CargueBackgroundWorker, Me._Plugin, Me._idCargue, Data_Log, Me._tipoLog_ValidaDuplicados, _NombreProceso, Me._fechaProcesoEjecucion.ToString("yyyyMMdd"))
                        'Else
                        'trReturn.Result = False
                        'lisMsgError.Add("- Archivo no válido. Por favor verifique lo siguiente:")
                        'lisMsgError.Add("- 1. Que el archivo contenga datos.")
                        ''lisMsgError.Add("- 2. El carácter de separación sea el adecuado.")
                        'trReturn.Parameters = lisMsgError
                        'End If
                    End If
                End If
            Catch ex As Exception
                lisMsgError.Add("- Error: " & ex.Message)
                trReturn.Result = False
                trReturn.Parameters = lisMsgError
            Finally
                dbmIntregation.Connection_Close()
            End Try

            Return trReturn
        End Function

        Public Function Cargue_DOE(ByVal NombreArchivo As String, ByVal Entidad As Short, ByVal Proyecto As Short, ByVal IdTipo_log As Integer) As DesktopConfig.TypeResult
            Dim trReturn As New DesktopConfig.TypeResult
            Dim dbmIntregation As New DBIntegrationDataBaseManager(_Plugin.CajaSocialConnectionString)
            trReturn.Result = True
            Dim lisMsgError = New List(Of String)
            _DataFile = New DataTable()
            dbmIntregation.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
            Dim CamposPorTipoLog = dbmIntregation.SchemaConfig.TBL_Campos_Tipo_log.DBGet(IdTipo_log, Entidad, Proyecto, Nothing, Nothing)
            Dim NumLineaFinal As Integer = 0

            _DataFile.Columns.Add("No Producto") 'No. Producto

            For Each itemCampo In CamposPorTipoLog
                _DataFile.Columns.Add(itemCampo.Nombre_Campo)
            Next

            Try
                Dim contador As Integer = 1
                Dim LineaFinal_Aux As String = ""
                If (CType(_File, FileStream).Name IsNot Nothing) Then
                    Dim lenghtInicio As Integer = 0

                    'TipoLog DOE
                    Using st As StreamReader = New StreamReader(CType(_File, FileStream).Name, System.Text.Encoding.Default)
                        While (Not st.EndOfStream())
                            LineaFinal_Aux = st.ReadLine().ToString()
                            NumLineaFinal += 1
                        End While
                    End Using

                    Using st As StreamReader = New StreamReader(CType(_File, FileStream).Name, System.Text.Encoding.Default)
                        While (Not st.EndOfStream())

                            If (contador = NumLineaFinal) Then
                                If (LineaFinal_Aux.StartsWith("*") Or LineaFinal_Aux.Length > 390) Then
                                    Exit While
                                End If
                            End If

                            Dim Linea = st.ReadLine().ToString()

                            If (Linea.Length <> 390) Then
                                If (Linea.Length <> 350) Then
                                    lisMsgError.Add("La linea #" + contador.ToString() + " del archivo esta errada debe tener 390 caracteres, Error de Cargue Log")
                                End If
                            End If

                            If (Linea.Length = 350) Then
                                'posicion 35
                                Dim linea_inicial = Linea.Substring(0, 51)
                                Dim linea_final = Linea.Substring(51, Linea.Length - 51)
                                Dim StringSpace As New String(" ", 40)
                                linea_inicial = linea_inicial + StringSpace
                                Linea = linea_inicial + linea_final
                            End If

                            'NO. PRODUCTO
                            Dim Cod1 = ""

                            'Fecha Transacción
                            Dim Cod2 = Linea.Substring(16, 10)

                            'Tipo identificación del cliente
                            Dim Cod3 = Linea.Substring(33, 2)

                            'Identificación del Cliente
                            Dim Cod4 = Linea.Substring(35, 16)

                            'Nombre cliente
                            Dim Cod5 = Linea.Substring(51, 40)

                            'DOE No
                            Dim Cod6 = Linea.Substring(0, 16)

                            'Oficina origen
                            Dim Cod7 = Linea.Substring(26, 7)

                            'Exento
                            Dim Cod8 = Linea.Substring(91, 2)

                            'Transacción
                            Dim Cod9 = Linea.Substring(93, 30)

                            'Naturaleza
                            Dim Cod10 = Linea.Substring(141, 7)

                            'No. Cuenta Afectada
                            Dim cod11 = Linea.Substring(123, 18)

                            'Valor Efectivo débito o crédito
                            Dim Cod12 = Linea.Substring(148, 20)

                            'Identificación realiza transacción
                            Dim Cod13 = Linea.Substring(170, 16)

                            'Tipo Identificación realiza transacción
                            Dim Cod14 = Linea.Substring(168, 2)

                            'Primer Apellido RT
                            Dim Cod15 = Linea.Substring(186, 40)

                            'Segundo Apellido RT
                            Dim Cod16 = Linea.Substring(226, 40)

                            'Primer Nombre RT
                            Dim Cod17 = Linea.Substring(266, 40)

                            'Otros Nombres RT
                            Dim Cod18 = Linea.Substring(306, 40)

                            'No de Transacción Alliance
                            Dim Cod19 = Linea.Substring(346, 21)

                            'TXDEALREFERENCE
                            Dim Cod20 = Linea.Substring(367, 23)
                            contador += 1

                            _DataFile.Rows.Add(Cod1, Cod2, Cod3, Cod4, Cod5, Cod6, Cod7, Cod8, Cod9, Cod10, cod11, Cod12, Cod13, Cod14, Cod15, Cod16, Cod17, Cod18, Cod19, Cod20)
                        End While
                    End Using

                    _DataRegistros = _DataFile.Rows.Count
                    _DataColumnas = _DataFile.Columns.Count

                    'If _DataRegistros > 0 And _DataColumnas > 1 Then
                    Dim Data_Log As New DBIntegration.SchemaLoad.TBL_Log_DataType
                    Data_Log.Fecha_Proceso = Me._fechaProceso.ToString("yyyyMMdd")
                    Data_Log.fk_Tipo_Log = Me.fk_Tipo_Log
                    Data_Log.fk_Tipo_Proceso = Me.fk_tipo_Proceso
                    Dim ProcesoEncontrado = dbmIntregation.SchemaBCSCarpetaUnica.TBL_Tipo_Proceso.DBGet(Me.fk_tipo_Proceso)
                    Dim ProcesoSigla_Aux As String = ""
                    If (ProcesoEncontrado.Rows.Count > 0) Then
                        ProcesoSigla_Aux = ProcesoEncontrado.Rows(0)("Sigla_Proceso")
                    End If
                    Data_Log.Proceso_Sigla = ProcesoSigla_Aux
                    Data_Log.Jornada = Me.JornadaVarchar_Global

                    Return ProcesaLog.ProcesaLogCajaSocial(_DataFile, Path.GetFileName(CType(_File, FileStream).Name), Me.CargueBackgroundWorker, Me._Plugin, Me._idCargue, Data_Log, Me._tipoLog_ValidaDuplicados, _NombreProceso, Me._fechaProcesoEjecucion.ToString("yyyyMMdd"))
                    'Else
                    '    trReturn.Result = False
                    '    lisMsgError.Add("- Archivo no válido. Por favor verifique lo siguiente:")
                    '    lisMsgError.Add("- 1. Que el archivo contenga datos.")
                    '    'lisMsgError.Add("- 2. El carácter de separación sea el adecuado.")
                    '    trReturn.Parameters = lisMsgError
                    'End If
                End If
            Catch ex As Exception
                lisMsgError.Add("- Error: " & ex.Message)
                trReturn.Result = False
                trReturn.Parameters = lisMsgError
            Finally
                dbmIntregation.Connection_Close()
            End Try

            Return trReturn
        End Function

        Private Function Cargue_Microfinanzas(ByVal NombreArchivo As String, ByVal Entidad As Short, ByVal Proyecto As Short, ByVal IdTipo_log As Integer) As DesktopConfig.TypeResult
            Dim trReturn As New DesktopConfig.TypeResult
            Dim dbmIntregation As New DBIntegrationDataBaseManager(_Plugin.CajaSocialConnectionString)
            dbmIntregation.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
            trReturn.Result = True
            Dim lisMsgError = New List(Of String)
            Dim nTempPath = CarpetaUnicaPlugin.AppPath() & CarpetaUnicaPlugin.TempPath
            Me.nTempPathFile_Microfinanzas = Path.GetDirectoryName(Me.ArchivoOpenFileDialog.FileName) + "\\file_temporal_" + Date.Now.Ticks.ToString() + ".txt"
            Dim ltLineasValidas As List(Of String) = New List(Of String)
            Dim dt As DataTable = New DataTable


            Try
                Using st As StreamReader = New StreamReader(CType(_File, FileStream).Name, System.Text.Encoding.Default)
                    While (Not st.EndOfStream())
                        Dim Linea = st.ReadLine().ToString()
                        If (Linea <> "") Then
                            ltLineasValidas.Add(Linea)
                        End If
                    End While
                End Using

                Me.ltMicrofinanzas = ltLineasValidas

                Using file As New System.IO.StreamWriter(Me.nTempPathFile_Microfinanzas, True)
                    Me.ltMicrofinanzas = Me.ltMicrofinanzas.Where(Function(x) x.Trim() <> Nothing).ToList()

                    For Each item In Me.ltMicrofinanzas
                        file.WriteLine(item)
                    Next
                End Using

                CargaDataTable_ConSeparador(dt, Me.nTempPathFile_Microfinanzas)

                Dim colAdicional = "Column" + (dt.Columns.Count).ToString()
                dt.Columns.Add(colAdicional)

                Dim dtFinal As New DataTable
                For Each itemColumn As DataColumn In dt.Columns
                    dtFinal.Columns.Add(itemColumn.ColumnName)
                Next

                If (dt.Rows.Count > 0) Then
                    For Each itemRow As DataRow In dt.Rows
                        Dim NoPro = itemRow("Column5")

                        If (Not String.IsNullOrEmpty(NoPro.ToString())) Then
                            Dim longVariable As Long

                            Dim valoresPartidos_1() As String = NoPro.Split(" ")

                            If (Long.TryParse(valoresPartidos_1(0), longVariable)) Then
                                Dim valorColumNumCredito = NoPro
                                Dim valoresPartidos() As String = valorColumNumCredito.Split(" ")

                                If (valoresPartidos.Length > 0) Then
                                    Dim NumCredito As String = ""
                                    Dim TipoID As String = ""

                                    If (valoresPartidos.Length = 1) Then
                                        NumCredito = valoresPartidos(0)
                                        itemRow(dt.Columns.Count - 2) = NumCredito
                                    End If

                                    If (valoresPartidos.Length = 2) Then
                                        NumCredito = valoresPartidos(0)
                                        TipoID = valoresPartidos(1)

                                        itemRow(dt.Columns.Count - 2) = NumCredito
                                        itemRow(dt.Columns.Count - 1) = TipoID
                                    End If
                                    dtFinal.Rows.Add(itemRow.ItemArray())
                                End If
                            Else
                                '_contadorNroProductos_vacios += 1
                            End If
                        Else
                            '_contadorNroProductos_vacios += 1
                        End If
                    Next
                End If

                Try
                    If (File.Exists(Me.nTempPathFile_Microfinanzas)) Then
                        File.Delete(Me.nTempPathFile_Microfinanzas)
                    End If
                Catch ex As Exception
                    If (File.Exists(Me.nTempPathFile_Microfinanzas)) Then
                        File.WriteAllText(Me.nTempPathFile_Microfinanzas, String.Empty)
                    End If
                End Try

                _DataRegistros = dtFinal.Rows.Count
                _DataColumnas = dtFinal.Columns.Count

                Me._DataFile = dtFinal
                Dim Data_Log As New DBIntegration.SchemaLoad.TBL_Log_DataType
                Data_Log.Fecha_Proceso = Me._fechaProceso.ToString("yyyyMMdd")
                Data_Log.fk_Tipo_Log = Me.fk_Tipo_Log
                Data_Log.fk_Tipo_Proceso = Me.fk_tipo_Proceso
                Dim ProcesoEncontrado = dbmIntregation.SchemaBCSCarpetaUnica.TBL_Tipo_Proceso.DBGet(Me.fk_tipo_Proceso)
                Dim ProcesoSigla_Aux As String = ""
                If (ProcesoEncontrado.Rows.Count > 0) Then
                    ProcesoSigla_Aux = ProcesoEncontrado.Rows(0)("Sigla_Proceso")
                End If
                Data_Log.Proceso_Sigla = ProcesoSigla_Aux
                Data_Log.Jornada = Me.JornadaVarchar_Global
                Return ProcesaLog.ProcesaLogCajaSocial(_DataFile, Path.GetFileName(CType(_File, FileStream).Name), Me.CargueBackgroundWorker, Me._Plugin, Me._idCargue, Data_Log, Me._tipoLog_ValidaDuplicados, _NombreProceso, Me._fechaProcesoEjecucion.ToString("yyyyMMdd"))

            Catch ex As Exception
                Try
                    If (File.Exists(Me.nTempPathFile_Microfinanzas)) Then
                        File.Delete(Me.nTempPathFile_Microfinanzas)
                    End If
                Catch ex_2 As Exception
                    If (File.Exists(Me.nTempPathFile_Microfinanzas)) Then
                        File.WriteAllText(Me.nTempPathFile_Microfinanzas, String.Empty)
                    End If
                End Try
            End Try

            Return trReturn
        End Function

        Private Function Cargue_AINCO_NOV(ByVal NombreArchivo As String, ByVal Entidad As Short, ByVal Proyecto As Short, ByVal IdTipo_log As Integer) As DesktopConfig.TypeResult
            Dim trReturn As New DesktopConfig.TypeResult
            Dim dbmIntregation As New DBIntegrationDataBaseManager(_Plugin.CajaSocialConnectionString)
            trReturn.Result = True
            Dim lisMsgError = New List(Of String)
            dbmIntregation.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

            'Se valida el tipo de archivo, si es CSV,TXT o XLS
            CargaArchivoExcel()
            KillSpecificProcess("EXCEL")

            _DataRegistros = _DataFile.Rows.Count
            _DataColumnas = _DataFile.Columns.Count

            Dim Data_Log As New DBIntegration.SchemaLoad.TBL_Log_DataType
            Data_Log.Fecha_Proceso = Me._fechaProceso.ToString("yyyyMMdd")
            Data_Log.fk_Tipo_Log = Me.fk_Tipo_Log
            Data_Log.fk_Tipo_Proceso = Me.fk_tipo_Proceso
            Dim ProcesoEncontrado = dbmIntregation.SchemaBCSCarpetaUnica.TBL_Tipo_Proceso.DBGet(Me.fk_tipo_Proceso)
            Dim ProcesoSigla_Aux As String = ""
            If (ProcesoEncontrado.Rows.Count > 0) Then
                ProcesoSigla_Aux = ProcesoEncontrado.Rows(0)("Sigla_Proceso")
            End If
            Data_Log.Proceso_Sigla = ProcesoSigla_Aux
            Data_Log.Jornada = Me.JornadaVarchar_Global
            Return ProcesaLog.ProcesaLogCajaSocial(_DataFile, Path.GetFileName(CType(_File, FileStream).Name), Me.CargueBackgroundWorker, Me._Plugin, Me._idCargue, Data_Log, Me._tipoLog_ValidaDuplicados, _NombreProceso, Me._fechaProcesoEjecucion.ToString("yyyyMMdd"))


            Return trReturn
        End Function

        Private Function Cargue_AcuseRecibido(ByVal NombreArchivo As String, ByVal Entidad As Short, ByVal Proyecto As Short, ByVal IdTipo_log As Integer) As DesktopConfig.TypeResult
            Dim trReturn As New DesktopConfig.TypeResult
            Dim dbmIntregation As New DBIntegrationDataBaseManager(_Plugin.CajaSocialConnectionString)
            trReturn.Result = True
            Dim lisMsgError = New List(Of String)
            dbmIntregation.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

            'Se valida el tipo de archivo, si es CSV,TXT o XLS
            CargaArchivoExcel()
            KillSpecificProcess("EXCEL")

            _DataRegistros = _DataFile.Rows.Count
            _DataColumnas = _DataFile.Columns.Count

            'If _DataRegistros > 0 And _DataColumnas > 1 Then
            Dim Data_Log As New DBIntegration.SchemaLoad.TBL_Log_DataType
            Data_Log.Fecha_Proceso = Me._fechaProceso.ToString("yyyyMMdd")
            Data_Log.fk_Tipo_Log = Me.fk_Tipo_Log
            Data_Log.fk_Tipo_Proceso = Me.fk_tipo_Proceso
            Dim ProcesoEncontrado = dbmIntregation.SchemaBCSCarpetaUnica.TBL_Tipo_Proceso.DBGet(Me.fk_tipo_Proceso)
            Dim ProcesoSigla_Aux As String = ""
            If (ProcesoEncontrado.Rows.Count > 0) Then
                ProcesoSigla_Aux = ProcesoEncontrado.Rows(0)("Sigla_Proceso")
            End If
            Data_Log.Proceso_Sigla = ProcesoSigla_Aux
            Data_Log.Jornada = Me.JornadaVarchar_Global
            Return ProcesaLog.ProcesaLogCajaSocial(_DataFile, Path.GetFileName(CType(_File, FileStream).Name), Me.CargueBackgroundWorker, Me._Plugin, Me._idCargue, Data_Log, Me._tipoLog_ValidaDuplicados, _NombreProceso, Me._fechaProcesoEjecucion.ToString("yyyyMMdd"))
            'Else
            'trReturn.Result = False
            'lisMsgError.Add("- Archivo no válido. Por favor verifique lo siguiente:")
            'lisMsgError.Add("- 1. Que el archivo contenga datos.")
            ''lisMsgError.Add("- 2. El carácter de separación sea el adecuado.")
            'trReturn.Parameters = lisMsgError
            'End If



            Return trReturn
        End Function

        Private Sub KillSpecificProcess(processName As String)
            Dim processes = From p In Process.GetProcessesByName(processName) Select p

            For Each process__1 In processes
                process__1.Kill()
            Next
        End Sub

        Private Function Cargue_FATCA(ByVal NombreArchivo As String, ByVal Entidad As Short, ByVal Proyecto As Short, ByVal IdTipo_log As Integer) As DesktopConfig.TypeResult
            Dim trReturn As New DesktopConfig.TypeResult
            Dim dbmIntregation As New DBIntegrationDataBaseManager(_Plugin.CajaSocialConnectionString)
            trReturn.Result = True
            Dim lisMsgError = New List(Of String)
            dbmIntregation.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

            'Se valida el tipo de archivo, si es CSV,TXT o XLS
            CargaArchivoExcel()
            KillSpecificProcess("EXCEL")

            _DataRegistros = _DataFile.Rows.Count
            _DataColumnas = _DataFile.Columns.Count

            Dim Data_Log As New DBIntegration.SchemaLoad.TBL_Log_DataType
            Data_Log.Fecha_Proceso = Me._fechaProceso.ToString("yyyyMMdd")
            Data_Log.fk_Tipo_Log = Me.fk_Tipo_Log
            Data_Log.fk_Tipo_Proceso = Me.fk_tipo_Proceso
            Dim ProcesoEncontrado = dbmIntregation.SchemaBCSCarpetaUnica.TBL_Tipo_Proceso.DBGet(Me.fk_tipo_Proceso)
            Dim ProcesoSigla_Aux As String = ""
            If (ProcesoEncontrado.Rows.Count > 0) Then
                ProcesoSigla_Aux = ProcesoEncontrado.Rows(0)("Sigla_Proceso")
            End If
            Data_Log.Proceso_Sigla = ProcesoSigla_Aux
            Data_Log.Jornada = Me.JornadaVarchar_Global
            Return ProcesaLog.ProcesaLogCajaSocial(_DataFile, Path.GetFileName(CType(_File, FileStream).Name), Me.CargueBackgroundWorker, Me._Plugin, Me._idCargue, _
                                                   Data_Log, Me._tipoLog_ValidaDuplicados, _NombreProceso, Me._fechaProcesoEjecucion.ToString("yyyyMMdd"))



            Return trReturn
        End Function

        Private Function Cargue_GoroVoluen(ByVal NombreArchivo As String, ByVal Entidad As Short, ByVal Proyecto As Short, ByVal IdTipo_log As Integer) As DesktopConfig.TypeResult
            Dim trReturn As New DesktopConfig.TypeResult
            Dim dbmIntregation As New DBIntegrationDataBaseManager(_Plugin.CajaSocialConnectionString)
            trReturn.Result = True
            Dim lisMsgError = New List(Of String)
            dbmIntregation.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
            Dim ltLineasValidas As List(Of String) = New List(Of String)
            Dim dt As DataTable = New DataTable
            Dim PathTemporaly = Path.GetDirectoryName(Me.ArchivoOpenFileDialog.FileName) + "\\file_temporal_" + Date.Now.Ticks.ToString() + ".txt"
            Dim contadorvacio As Integer = 1

            Dim CamposPorTipoLog = dbmIntregation.SchemaConfig.TBL_Campos_Tipo_log.DBGet(IdTipo_log, Entidad, Proyecto, Nothing, Nothing).OrderBy(Function(x) x("Campo_Log_Data")).CopyToDataTable()
            For Each itemCampo In CamposPorTipoLog.Rows
                dt.Columns.Add(itemCampo("Nombre_Campo"))
            Next


            Using st As StreamReader = New StreamReader(CType(_File, FileStream).Name, System.Text.Encoding.Default)
                While (Not st.EndOfStream())
                    Dim Linea = st.ReadLine().ToString()

                    If (contadorvacio > 2) Then
                        If (Linea <> "") Then

                            If (Linea = Microsoft.VisualBasic.vbCrLf) Then
                                Dim joijoij = 2
                            End If
                            Try
                                ltLineasValidas.Add(Linea.Substring(117, 20))
                                ltLineasValidas.Add(Linea.Substring(71, 23))
                                ltLineasValidas.Add(Linea.Substring(182, 21))
                                ltLineasValidas.Add(Linea.Substring(160, 21))
                                ltLineasValidas.Add("") '5 "" nombre
                                ltLineasValidas.Add(Linea.Substring(22, 15))
                                ltLineasValidas.Add(Linea.Substring(38, 15))
                                ltLineasValidas.Add(Linea.Substring(54, 7))
                                ltLineasValidas.Add(Linea.Substring(62, 8))
                                ltLineasValidas.Add(Linea.Substring(95, 7))
                                ltLineasValidas.Add(Linea.Substring(103, 13))
                                ltLineasValidas.Add(Linea.Substring(138, 21))
                                ltLineasValidas.Add(Linea.Substring(204, 23))
                                ltLineasValidas.Add(Linea.Substring(228, 20))
                                ltLineasValidas.Add(Linea.Substring(249, 6))
                                ltLineasValidas.Add(Linea.Substring(256, 60))
                                ltLineasValidas.Add(Linea.Substring(0, 21))
                            Catch ex As Exception
                            End Try
                            
                            If (ltLineasValidas.Count > 0) Then
                                dt.Rows.Add(ltLineasValidas.ToArray())
                            End If

                            ltLineasValidas = New List(Of String)
                        End If
                    ElseIf (contadorvacio = 5297) Then
                        Dim condd = 1
                    End If

                    contadorvacio += 1

                End While
            End Using

            Dim con = 1

           _DataRegistros = dt.Rows.Count
            _DataColumnas = dt.Columns.Count

            'If _DataRegistros > 0 And _DataColumnas > 1 Then
            Me._DataFile = dt
            Dim Data_Log As New DBIntegration.SchemaLoad.TBL_Log_DataType
            Data_Log.Fecha_Proceso = Me._fechaProceso.ToString("yyyyMMdd")
            Data_Log.fk_Tipo_Log = Me.fk_Tipo_Log
            Data_Log.fk_Tipo_Proceso = Me.fk_tipo_Proceso
            Dim ProcesoEncontrado = dbmIntregation.SchemaBCSCarpetaUnica.TBL_Tipo_Proceso.DBGet(Me.fk_tipo_Proceso)
            Dim ProcesoSigla_Aux As String = ""
            If (ProcesoEncontrado.Rows.Count > 0) Then
                ProcesoSigla_Aux = ProcesoEncontrado.Rows(0)("Sigla_Proceso")
            End If
            Data_Log.Proceso_Sigla = ProcesoSigla_Aux
            Data_Log.Jornada = Me.JornadaVarchar_Global
            Return ProcesaLog.ProcesaLogCajaSocial(_DataFile, Path.GetFileName(CType(_File, FileStream).Name), Me.CargueBackgroundWorker, Me._Plugin, Me._idCargue, Data_Log, _
                                                   Me._tipoLog_ValidaDuplicados, _NombreProceso, Me._fechaProcesoEjecucion.ToString("yyyyMMdd"))



            Return trReturn
        End Function

        Private Function Cargue_DINAMICO(ByVal NombreArchivo As String, ByVal Entidad As Short, ByVal Proyecto As Short, ByVal IdTipo_log As Integer) As DesktopConfig.TypeResult
            Dim trReturn As New DesktopConfig.TypeResult
            Dim dbmIntregation As New DBIntegrationDataBaseManager(_Plugin.CajaSocialConnectionString)
            trReturn.Result = True
            Dim lisMsgError = New List(Of String)
            Dim contiene01 As Boolean = False
            Dim LengthInicio As Integer = 0
            Dim CamposListos As Boolean = False
            Dim dt As New DataTable
            Dim SaltoPrimeraLinea As Boolean = False
            Dim CamposLLenos As Boolean = False
            Dim ltCamposLog As New List(Of String)
            Dim contadorValoresEncontrados As Integer = 0
            Dim SepararCampo As Boolean = False

            Try
                Dim contador As Integer = 0
                'Se crean Columnas para dataTable Final
                _DataFile = New DataTable()
                dbmIntregation.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
                Dim CamposPorTipoLog As DBIntegration.SchemaConfig.TBL_Campos_Tipo_logDataTable = dbmIntregation.SchemaConfig.TBL_Campos_Tipo_log.DBFindByCampo_Adicionalfk_Tipo_logfk_Entidadfk_ProyectoDescripcion(False, IdTipo_log, Entidad, Proyecto, Nothing)

                Dim ColumnsAdicionalesLogData = dbmIntregation.SchemaLoad.TBL_Log_Data.DBGet(0).Columns
                CargueBackgroundWorker.ReportProgress(0)
                For Each itemCampo As DataColumn In ColumnsAdicionalesLogData
                    _DataFile.Columns.Add(itemCampo.ColumnName)
                Next
                _DataFile.Columns.RemoveAt(0)
                Dim ProcesoEncontrado = dbmIntregation.SchemaBCSCarpetaUnica.TBL_Tipo_Proceso.DBGet(Me.fk_tipo_Proceso)
                Dim ProcesoSigla_Aux As String = ""
                If (ProcesoEncontrado.Rows.Count > 0) Then
                    ProcesoSigla_Aux = ProcesoEncontrado.Rows(0)("Sigla_Proceso")
                End If

                Dim TipoLogDataTable = dbmIntregation.SchemaConfig.TBL_Tipos_Log.DBGet(Me.fk_Tipo_Log, Me._Plugin.Manager.ImagingGlobal.Entidad, Me._Plugin.Manager.ImagingGlobal.Proyecto)
                If (TipoLogDataTable.Rows.Count > 0) Then
                    SaltoPrimeraLinea = CBool(TipoLogDataTable.Rows(0)("ManejaEncabezado"))
                End If

                Dim MaxNumCamposLog_BCS As Integer

                Dim MaxNumCamposLog = dbmIntregation.SchemaConfig.TBL_Parametro_Sistema.DBFindByfk_Entidadfk_ProyectoNombre_Parametro_Sistema(_Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, "MaxNumero_CamposLog_BCS")
                If (MaxNumCamposLog.Rows.Count > 0) Then
                    MaxNumCamposLog_BCS = CInt(MaxNumCamposLog.Rows(0)("Valor_Parametro_Sistema").ToString())
                End If

                If (Me.ExtensionGlobal = "") Then
                    Me.ExtensionGlobal = "NO TIENE"
                End If

                'Se leen valores del archivo plano
                If (Me.ExtensionGlobal.ToString().ToLower().Contains(".xls")) Then
                    '.EXCEL

                    Dim dtRef As New DataTable
                    CargaArchivoExcel(True, dtRef)
                    Dim contadorRows As Integer = 0

                    If (dtRef.Rows.Count > 0) Then
                        For index = 0 To dtRef.Rows.Count - 1
                            For Each itemColumCampo In ColumnsAdicionalesLogData
                                If (itemColumCampo.ToString().ToUpper() <> "ID_LOG_DATA") Then
                                    If (itemColumCampo.ToString().ToUpper().Contains("CAMPO_")) Then
                                        Dim campoBuscar = itemColumCampo.ToString()
                                        Dim ax_encontrado = CamposPorTipoLog.Where(Function(x) x.Nombre_Campo = campoBuscar.ToString())
                                        Dim campoEncontrado As DBIntegration.SchemaConfig.TBL_Campos_Tipo_logRow = Nothing

                                        If (ax_encontrado.Count > 0) Then
                                            campoEncontrado = ax_encontrado.First()
                                            ltCamposLog.Add(dtRef.Rows(index)(campoEncontrado.Descripcion).ToString())
                                        Else
                                            ltCamposLog.Add("")
                                        End If
                                    Else
                                        ltCamposLog.Add("")
                                    End If
                                End If
                            Next
                            _DataFile.Rows.Add(ltCamposLog.ToArray())
                            _DataFile.Rows(contador)("fk_Entidad") = Me._Plugin.Manager.ImagingGlobal.Entidad
                            _DataFile.Rows(contador)("fk_Proyecto") = Me._Plugin.Manager.ImagingGlobal.Proyecto
                            _DataFile.Rows(contador)("fk_Tipo_Log") = Me.fk_Tipo_Log
                            If chk_CargaLogsFechasAnteriores.Checked = True Then
                                _DataFile.Rows(contador)("Fecha_Proceso") = Me.dtpFechaEjecucion.Value.ToString("yyyyMMdd") 'Fecha de proceso anterior
                            Else
                                _DataFile.Rows(contador)("Fecha_Proceso") = Me.dtpFechaProceso.Value.ToString("yyyyMMdd")
                            End If
                            _DataFile.Rows(contador)("fk_Tipo_Proceso") = Me.fk_tipo_Proceso
                            _DataFile.Rows(contador)("Proceso_Sigla") = ProcesoSigla_Aux
                            _DataFile.Rows(contador)("Cruce_Tipo_Tx") = "0"
                            contador += 1
                            ltCamposLog.Clear()
                        Next
                    End If

                Else
                    '.TXT
                    If (CType(_File, FileStream).Name IsNot Nothing) Then
                        Dim lenghtInicio As Integer = 0
                        Using st As StreamReader = New StreamReader(CType(_File, FileStream).Name, System.Text.Encoding.Default)
                            While (Not st.EndOfStream())
                                Dim Linea = st.ReadLine().ToString()
                                If (SaltoPrimeraLinea) Then
                                    SaltoPrimeraLinea = False
                                    Linea = st.ReadLine()
                                End If

                                If (Linea IsNot Nothing) Then
                                    lenghtInicio = 0
                                    Dim RegistroInsertar As Object = New ExpandoObject()
                                    Dim contadorInterno As Integer = 0
                                    For Each itemColumCampo In ColumnsAdicionalesLogData
                                        If (itemColumCampo.ToString().ToUpper() <> "ID_LOG_DATA") Then
                                            If (itemColumCampo.ToString().ToUpper().Contains("CAMPO_")) Then
                                                If (Not CamposLLenos) Then
                                                    If (TipoLogDataTable.Rows.Count > 0) Then
                                                        If (Not IsDBNull(TipoLogDataTable.Rows(0)("Separador"))) Then
                                                            Dim Separador = TipoLogDataTable.Rows(0)("Separador")
                                                            If (Separador <> "") Then
                                                                If (Separador = "TAB") Then
                                                                    Dim LineaSplitTAB = Linea.Split(vbTab)
                                                                    CamposLLenos = True
                                                                    For index = 0 To MaxNumCamposLog_BCS - 1
                                                                        Dim idBuscar = index + 1
                                                                        Dim idCampo_Encontrado = CamposPorTipoLog.Where(Function(x) x.Campo_Log_Data = idBuscar)

                                                                        If (idCampo_Encontrado.ToList().Count > 0) Then
                                                                            If (contadorValoresEncontrados < LineaSplitTAB.Length) Then
                                                                                ltCamposLog.Add(LineaSplitTAB(contadorValoresEncontrados))
                                                                            End If
                                                                            contadorValoresEncontrados += 1
                                                                        Else
                                                                            ltCamposLog.Add("")
                                                                        End If
                                                                    Next
                                                                    contadorValoresEncontrados = 0
                                                                ElseIf (Separador = ";") Then
                                                                    Dim LineaSplitPuntoComa = Linea.Split(";")
                                                                    CamposLLenos = True

                                                                    For index = 0 To MaxNumCamposLog_BCS - 1
                                                                        Dim idBuscar = index + 1
                                                                        Dim idCampo_Encontrado = CamposPorTipoLog.Where(Function(x) x.Campo_Log_Data = idBuscar)

                                                                        If (idCampo_Encontrado.ToList().Count > 0) Then
                                                                            ltCamposLog.Add(LineaSplitPuntoComa(CInt(idCampo_Encontrado.FirstOrDefault().id_Campo_Log - 1)))
                                                                        Else
                                                                            ltCamposLog.Add("")
                                                                        End If
                                                                    Next

                                                                    contadorValoresEncontrados = 0
                                                                End If
                                                            End If
                                                        Else
                                                            Dim campoBuscar = itemColumCampo.ToString()
                                                            Dim ax_encontrado = CamposPorTipoLog.Where(Function(x) x.Nombre_Campo = campoBuscar.ToString())
                                                            Dim campoEncontrado As DBIntegration.SchemaConfig.TBL_Campos_Tipo_logRow = Nothing

                                                            If (ax_encontrado.Count > 0) Then
                                                                campoEncontrado = ax_encontrado.First()
                                                                If Not campoEncontrado.IsLength_IniciaNull() And Not campoEncontrado.IsLength_TerminaNull() Then
                                                                    ltCamposLog.Add(Linea.Substring(CInt(campoEncontrado.Length_Inicia), CInt(campoEncontrado.Length_Fijo)))
                                                                End If
                                                            Else
                                                                ltCamposLog.Add("")
                                                            End If
                                                        End If
                                                    End If
                                                End If
                                            Else
                                                ltCamposLog.Add("")
                                            End If
                                        End If
                                    Next
                                    CamposLLenos = False
                                    _DataFile.Rows.Add(ltCamposLog.ToArray())
                                    If (Me._NombreProceso = "CASH MANAGEMENT") Then
                                        Dim campos_adicional = dbmIntregation.SchemaConfig.TBL_Campos_Tipo_log.DBFindByCampo_Adicionalfk_Tipo_logfk_Entidadfk_ProyectoDescripcion(True, IdTipo_log, Entidad, Proyecto, "TIPOID").ToList()
                                        Dim campo_separar = dbmIntregation.SchemaConfig.TBL_Campos_Tipo_log.DBFindByCampo_Adicionalfk_Tipo_logfk_Entidadfk_ProyectoDescripcion(False, IdTipo_log, Entidad, Proyecto, "NIT").ToList()

                                        For index = 0 To campos_adicional.Count - 1
                                            Dim aux_campo = campos_adicional(index)
                                            Dim lengthInciaCampoAdicional = CInt(aux_campo.Length_Inicia)
                                            Dim lengthTerminaCampoAdicional = CInt(aux_campo.Length_Termina)

                                            Dim lengthInciaCampoSeparar = CInt(campo_separar.FirstOrDefault().Length_Inicia)
                                            Dim lengthTerminaCampoSeparar = campo_separar.FirstOrDefault().Length_Termina

                                            If (lengthTerminaCampoSeparar <> "LENGTH") Then
                                                lengthTerminaCampoSeparar = CInt(lengthTerminaCampoSeparar)
                                            Else
                                                lengthTerminaCampoSeparar = _DataFile.Rows(contador)(campo_separar.FirstOrDefault().Nombre_Campo).ToString().Length - lengthInciaCampoSeparar
                                            End If

                                            _DataFile.Rows(contador)(aux_campo.Nombre_Campo) = _DataFile.Rows(contador)(campo_separar.FirstOrDefault().Nombre_Campo).ToString().Substring(lengthInciaCampoAdicional, lengthTerminaCampoAdicional)
                                            _DataFile.Rows(contador)(campo_separar.FirstOrDefault().Nombre_Campo) = _DataFile.Rows(contador)(campo_separar.FirstOrDefault().Nombre_Campo).ToString().Substring(lengthInciaCampoSeparar, lengthTerminaCampoSeparar)
                                        Next
                                    ElseIf (_NombreProceso = "CARPETA EMPRESARIAL") Then
                                        Dim CampoTipoIDEmpresarial = dbmIntregation.SchemaConfig.TBL_Parametro_Sistema.DBFindByfk_Entidadfk_ProyectoNombre_Parametro_Sistema(_Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, "TipoID_Empresarial_Log_BCS")

                                        If (CampoTipoIDEmpresarial.Rows.Count > 0) Then
                                            Dim campoTipoIdEmpresarialSplit = CampoTipoIDEmpresarial.Rows(0)("Valor_Parametro_Sistema").ToString().Split("|")
                                            _DataFile.Rows(contador)(campoTipoIdEmpresarialSplit(0)) = campoTipoIdEmpresarialSplit(1)
                                        End If
                                    End If
                                    _DataFile.Rows(contador)("fk_Entidad") = Me._Plugin.Manager.ImagingGlobal.Entidad
                                    _DataFile.Rows(contador)("fk_Proyecto") = Me._Plugin.Manager.ImagingGlobal.Proyecto
                                    _DataFile.Rows(contador)("fk_Tipo_Log") = Me.fk_Tipo_Log
                                    If chk_CargaLogsFechasAnteriores.Checked = True Then
                                        _DataFile.Rows(contador)("Fecha_Proceso") = Me.dtpFechaEjecucion.Value.ToString("yyyyMMdd") 'Fecha de proceso anterior
                                    Else
                                        _DataFile.Rows(contador)("Fecha_Proceso") = Me.dtpFechaProceso.Value.ToString("yyyyMMdd")
                                    End If
                                    _DataFile.Rows(contador)("fk_Tipo_Proceso") = Me.fk_tipo_Proceso
                                    _DataFile.Rows(contador)("Proceso_Sigla") = ProcesoSigla_Aux
                                    _DataFile.Rows(contador)("Cruce_Tipo_Tx") = "0"
                                    _DataFile.Rows(contador)("Cruzado_Multiproducto") = "0"
                                    contador += 1
                                    ltCamposLog.Clear()
                                End If
                            End While
                        End Using
                    End If
                End If
                contador = 0
                _DataRegistros = _DataFile.Rows.Count
                _DataColumnas = _DataFile.Columns.Count

                Dim Data_Log As New DBIntegration.SchemaLoad.TBL_Log_DataType
                Data_Log.Fecha_Proceso = Me._fechaProceso.ToString("yyyyMMdd")
                Data_Log.fk_Tipo_Log = Me.fk_Tipo_Log
                Data_Log.fk_Tipo_Proceso = Me.fk_tipo_Proceso
                Data_Log.Proceso_Sigla = ProcesoSigla_Aux
                Data_Log.Jornada = Me.JornadaVarchar_Global
                KillSpecificProcess("EXCEL")
                Return ProcesaLog.ProcesaLogCajaSocial(_DataFile, Path.GetFileName(CType(_File, FileStream).Name), Me.CargueBackgroundWorker, Me._Plugin, Me._idCargue, Data_Log, _
                                                       Me._tipoLog_ValidaDuplicados, _NombreProceso, Me._fechaProcesoEjecucion.ToString("yyyyMMdd"))
               
            Catch ex As Exception
                lisMsgError.Add("- Error: " & ex.Message)
                trReturn.Result = False
                trReturn.Parameters = lisMsgError
            Finally
                dbmIntregation.Connection_Close()
                KillSpecificProcess("EXCEL")
                Dim ltColumnsDelete As New List(Of String)
                For Each itemColumn As DataColumn In _DataFile.Columns
                    If (Not itemColumn.ColumnName.ToUpper().Contains("CAMPO_")) Then
                        ltColumnsDelete.Add(itemColumn.ColumnName)
                    End If
                Next
                For Each itemColumnDelete In ltColumnsDelete
                    _DataFile.Columns.Remove(itemColumnDelete)
                Next
            End Try

            Return trReturn
        End Function

        Private Function Cargue_CONSTRUCTOR_EXCEL(ByVal NombreArchivo As String, ByVal Entidad As Short, ByVal Proyecto As Short, ByVal IdTipo_log As Integer) As DesktopConfig.TypeResult
            Dim trReturn As New DesktopConfig.TypeResult
            Dim dbmIntregation As New DBIntegrationDataBaseManager(_Plugin.CajaSocialConnectionString)
            trReturn.Result = True
            Dim lisMsgError = New List(Of String)
            Dim contiene01 As Boolean = False
            Dim LengthInicio As Integer = 0
            Dim CamposListos As Boolean = False
            Dim dt As New DataTable
            Dim SaltoPrimeraLinea As Boolean = False
            Dim CamposLLenos As Boolean = False
            Dim ltCamposLog As New List(Of String)
            Dim contadorValoresEncontrados As Integer = 0
            Dim SepararCampo As Boolean = False
            Dim dtProductosNuevos As New DataTable
            Dim dtProductosAntiguos As New DataTable

            Try
                Dim contador As Integer = 0
                'Se crean Columnas para dataTable Final
                _DataFile = New DataTable()
                dbmIntregation.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                Dim ColumnsAdicionalesLogData = dbmIntregation.SchemaLoad.TBL_Log_Data.DBGet(0).Columns
                CargueBackgroundWorker.ReportProgress(0)
                For Each itemCampo As DataColumn In ColumnsAdicionalesLogData
                    _DataFile.Columns.Add(itemCampo.ColumnName)
                Next
                _DataFile.Columns.RemoveAt(0)
                For Each itemCampo As DataColumn In ColumnsAdicionalesLogData
                    dtProductosNuevos.Columns.Add(itemCampo.ColumnName)
                Next
                dtProductosNuevos.Columns.RemoveAt(0)

                For Each itemCampo As DataColumn In ColumnsAdicionalesLogData
                    dtProductosAntiguos.Columns.Add(itemCampo.ColumnName)
                Next
                dtProductosAntiguos.Columns.RemoveAt(0)

                Dim ProcesoEncontrado = dbmIntregation.SchemaBCSCarpetaUnica.TBL_Tipo_Proceso.DBGet(Me.fk_tipo_Proceso)
                Dim ProcesoSigla_Aux As String = ""
                If (ProcesoEncontrado.Rows.Count > 0) Then
                    ProcesoSigla_Aux = ProcesoEncontrado.Rows(0)("Sigla_Proceso")
                End If

                Dim TipoLogDataTable = dbmIntregation.SchemaConfig.TBL_Tipos_Log.DBGet(Me.fk_Tipo_Log, Me._Plugin.Manager.ImagingGlobal.Entidad, Me._Plugin.Manager.ImagingGlobal.Proyecto)
                If (TipoLogDataTable.Rows.Count > 0) Then
                    SaltoPrimeraLinea = CBool(TipoLogDataTable.Rows(0)("ManejaEncabezado"))
                End If

                Dim MaxNumCamposLog_BCS As Integer

                Dim MaxNumCamposLog = dbmIntregation.SchemaConfig.TBL_Parametro_Sistema.DBFindByfk_Entidadfk_ProyectoNombre_Parametro_Sistema(_Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, "MaxNumero_CamposLog_BCS")
                If (MaxNumCamposLog.Rows.Count > 0) Then
                    MaxNumCamposLog_BCS = CInt(MaxNumCamposLog.Rows(0)("Valor_Parametro_Sistema").ToString())
                End If

                If (Me.ExtensionGlobal = "") Then
                    Me.ExtensionGlobal = "NO TIENE"
                End If

                'Se leen valores del EXCEL
                Dim dsRef As New DataSet
                Dim id_Tipo_log_aux As Integer
                CargaArchivoExcel(True, Nothing, dsRef)
                Dim contadorRows As Integer = 0
                Dim TipoLog As DBIntegration.SchemaConfig.TBL_Tipos_LogDataTable = Nothing
                Dim CamposPorTipoLog_aux As DBIntegration.SchemaConfig.TBL_Campos_Tipo_logDataTable = Nothing

                For Each itemdt As DataTable In dsRef.Tables
                    For Each column As DataColumn In itemdt.Columns
                        column.ColumnName.TrimEnd()
                        column.ColumnName.TrimStart()
                    Next
                Next


                    Dim dtCampoReporte = dbmIntregation.SchemaBCSCarpetaUnica.TBL_Config_Campo_Reporte.DBFindByNombre_Campo_Reporte("Nro_Producto")
                    Dim idCamporReporte_NroProducto As Integer = 0

                    If (dtCampoReporte.Rows.Count > 0) Then
                        idCamporReporte_NroProducto = CInt(dtCampoReporte.Rows(0)("id_Campo_Reporte").ToString())
                    End If


                    If (dsRef.Tables.Count > 0) Then
                        For index = 0 To dsRef.Tables.Count - 1
                            Dim table = dsRef.Tables(index)

                            If (table.Rows.Count > 0) Then
                                If (table.TableName.ToUpper() = "EXP NUEVOS" Or table.TableName.ToUpper().Contains("EXP NUEVOS")) Then
                                    TipoLog = dbmIntregation.SchemaConfig.TBL_Tipos_Log.DBFindByNombre_Tipo_Logfk_Entidadfk_Proyecto("CONSTRUCTOR EXCEL", Me._Plugin.Manager.ImagingGlobal.Entidad, Me._Plugin.Manager.ImagingGlobal.Proyecto)
                                    If (TipoLog.Rows.Count > 0) Then
                                        id_Tipo_log_aux = CInt(TipoLog.Rows(0)("id_Tipo_Log"))
                                        CamposPorTipoLog_aux = dbmIntregation.SchemaConfig.TBL_Campos_Tipo_log.DBFindByCampo_Adicionalfk_Tipo_logfk_Entidadfk_ProyectoDescripcion(False, id_Tipo_log_aux, Entidad, Proyecto, Nothing)
                                    End If
                                Else
                                    TipoLog = dbmIntregation.SchemaConfig.TBL_Tipos_Log.DBFindByNombre_Tipo_Logfk_Entidadfk_Proyecto("CONSTRUCTOR EXCEL-ACTUALIZACIONES", Me._Plugin.Manager.ImagingGlobal.Entidad, Me._Plugin.Manager.ImagingGlobal.Proyecto)
                                    If (TipoLog.Rows.Count > 0) Then
                                        id_Tipo_log_aux = CInt(TipoLog.Rows(0)("id_Tipo_Log"))
                                        CamposPorTipoLog_aux = dbmIntregation.SchemaConfig.TBL_Campos_Tipo_log.DBFindByCampo_Adicionalfk_Tipo_logfk_Entidadfk_ProyectoDescripcion(False, id_Tipo_log_aux, Entidad, Proyecto, Nothing)
                                    End If
                                End If

                                ''Llenado
                                For index_aux = 0 To table.Rows.Count - 1
                                    For Each itemColumCampo In ColumnsAdicionalesLogData
                                        If (itemColumCampo.ToString().ToUpper() <> "ID_LOG_DATA") Then
                                            If (itemColumCampo.ToString().ToUpper().Contains("CAMPO_")) Then
                                                Dim campoBuscar = itemColumCampo.ToString()
                                                Dim ax_encontrado = CamposPorTipoLog_aux.Where(Function(x) x.Nombre_Campo.ToUpper() = campoBuscar.ToString().ToUpper())
                                                Dim campoEncontrado As DBIntegration.SchemaConfig.TBL_Campos_Tipo_logRow = Nothing

                                                If (ax_encontrado.Count > 0) Then
                                                    campoEncontrado = ax_encontrado.First()

                                                    If (Not campoEncontrado.Isfk_BCS_CU_Campo_ReporteNull) Then
                                                        If campoEncontrado.fk_BCS_CU_Campo_Reporte = idCamporReporte_NroProducto Then
                                                            Dim valor = table.Rows(index_aux)(campoEncontrado.Descripcion).ToString()

                                                            If (valor = "") Then
                                                                Dim ltError As List(Of String) = New List(Of String)
                                                                ltError.Add("Error al cargar Excel constructor: Hay un Nro de Producto vacio en la Hoja " + table.TableName)
                                                            trResultado.Result = False
                                                            trResultado.Parameters = ltError
                                                            Return trResultado
                                                            End If

                                                        End If
                                                    End If


                                                    If (Me._nombreTipoLog = "CONSTRUCTOR EXCEL" And campoEncontrado.Descripcion = "Fecha" And table.TableName <> "EXP NUEVOS" And (Not table.TableName.ToUpper().Contains("EXP NUEVOS"))) Then
                                                        ltCamposLog.Add(Me._ValorCampoFechaApertura_ExcelActualizaciones)
                                                        Continue For
                                                    End If
                                                ltCamposLog.Add(table.Rows(index_aux)(campoEncontrado.Descripcion).ToString())
                                                Else
                                                    ltCamposLog.Add("")
                                                End If
                                            Else
                                                ltCamposLog.Add("")
                                            End If
                                        End If
                                Next


                                    _DataFile.Rows.Add(ltCamposLog.ToArray())
                                    _DataFile.Rows(contador)("fk_Entidad") = Me._Plugin.Manager.ImagingGlobal.Entidad
                                    _DataFile.Rows(contador)("fk_Proyecto") = Me._Plugin.Manager.ImagingGlobal.Proyecto
                                    _DataFile.Rows(contador)("fk_Tipo_Log") = CInt(TipoLog.Rows(0)("id_Tipo_Log"))
                                    If chk_CargaLogsFechasAnteriores.Checked = True Then
                                        _DataFile.Rows(contador)("Fecha_Proceso") = Me.dtpFechaEjecucion.Value.ToString("yyyyMMdd") 'Fecha de proceso anterior
                                    Else
                                        _DataFile.Rows(contador)("Fecha_Proceso") = Me.dtpFechaProceso.Value.ToString("yyyyMMdd")
                                    End If
                                    _DataFile.Rows(contador)("fk_Tipo_Proceso") = CInt(TipoLog.Rows(0)("fk_Tipo_Proceso"))
                                    _DataFile.Rows(contador)("Proceso_Sigla") = ProcesoSigla_Aux
                                    _DataFile.Rows(contador)("Cruce_Tipo_Tx") = "0"
                                    contador += 1
                                    ltCamposLog.Clear()
                                Next

                                contador = 0


                                If (table.TableName.ToUpper() = "EXP NUEVOS" Or table.TableName.ToUpper().Contains("EXP NUEVOS")) Then
                                    dtProductosNuevos.Merge(_DataFile)
                                Else
                                    dtProductosAntiguos.Merge(_DataFile)
                                End If

                                _DataFile.Clear()
                                If (_DataFile.Columns.Contains("REGISTRO")) Then
                                    _DataFile.Columns.Remove("REGISTRO")
                                End If
                            End If
                        Next

                        Dim Data_Log As New DBIntegration.SchemaLoad.TBL_Log_DataType
                        If (dtProductosAntiguos.Rows.Count > 0) Then
                            Data_Log.Fecha_Proceso = Me._fechaProceso.ToString("yyyyMMdd")
                            Data_Log.fk_Tipo_Log = CInt(dtProductosAntiguos.Rows(0)("fk_Tipo_Log"))
                            Data_Log.fk_Tipo_Proceso = CInt(dtProductosAntiguos.Rows(0)("fk_Tipo_Proceso"))
                            Data_Log.Proceso_Sigla = ProcesoSigla_Aux
                            Data_Log.Jornada = Me.JornadaVarchar_Global
                            ProcesaLog.ProcesaLogCajaSocial(dtProductosAntiguos, Path.GetFileName(CType(_File, FileStream).Name), Me.CargueBackgroundWorker, Me._Plugin, Me._idCargue, Data_Log, _
                                                                     Me._tipoLog_ValidaDuplicados, _NombreProceso, Me._fechaProcesoEjecucion.ToString("yyyyMMdd"))
                        End If

                        If (dtProductosNuevos.Rows.Count > 0) Then
                            Data_Log.Fecha_Proceso = Me._fechaProceso.ToString("yyyyMMdd")
                            Data_Log.fk_Tipo_Log = CInt(dtProductosNuevos.Rows(0)("fk_Tipo_Log"))
                            Data_Log.fk_Tipo_Proceso = CInt(dtProductosNuevos.Rows(0)("fk_Tipo_Proceso"))
                            Data_Log.Proceso_Sigla = ProcesoSigla_Aux
                            Data_Log.Jornada = Me.JornadaVarchar_Global
                            ProcesaLog.ProcesaLogCajaSocial(dtProductosNuevos, Path.GetFileName(CType(_File, FileStream).Name), Me.CargueBackgroundWorker, Me._Plugin, Me._idCargue, Data_Log, _
                                                                     Me._tipoLog_ValidaDuplicados, _NombreProceso, Me._fechaProcesoEjecucion.ToString("yyyyMMdd"))
                        End If

                        _DataRegistros = dtProductosAntiguos.Rows.Count + dtProductosNuevos.Rows.Count
                        _DataColumnas = dtProductosNuevos.Columns.Count + dtProductosNuevos.Columns.Count
                    KillSpecificProcess("EXCEL")

                    End If
            Catch ex As Exception
                lisMsgError.Add("- Error: " & ex.Message)
                trReturn.Result = False
                trReturn.Parameters = lisMsgError
                KillSpecificProcess("EXCEL")
            Finally
                dbmIntregation.Connection_Close()
                _DataFile.Merge(dtProductosAntiguos)

                Dim ltColumnsDelete As New List(Of String)
                For Each itemColumn As DataColumn In _DataFile.Columns
                    If (Not itemColumn.ColumnName.ToUpper().Contains("CAMPO_")) Then
                        ltColumnsDelete.Add(itemColumn.ColumnName)
                    End If
                Next
                For Each itemColumnDelete In ltColumnsDelete
                    _DataFile.Columns.Remove(itemColumnDelete)
                Next
            End Try

            Return trReturn
        End Function

        Public Function VerificarFechaProceso() As Boolean
            Dim retorno As Boolean = True
            Dim ControlCargue As New DataTable
            Dim ControlProceso As New DataTable
            Dim fechaProcesoDataTable As New DataTable

            _fechaProceso = Me.dtpFechaProceso.Value
            If (_fechaProceso.Date > Now.Date) Then
                DesktopMessageBoxControl.DesktopMessageShow("Error, la fecha de proceso no debe ser superior al dia de hoy", "Error de Fecha proceso", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                Return False
            End If
            Dim DBMIMaging As New DBImagingDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)
            Dim dbmIntregation As New DBIntegrationDataBaseManager(_Plugin.CajaSocialConnectionString)

            Try
                DBMIMaging.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
                dbmIntregation.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
                Dim fechaProceso = Integer.Parse(_fechaProceso.ToString("yyyyMMdd"))

                If chk_CargaLogsFechasAnteriores.Checked = True Then
                    'fechas de proceso anterior
                    fechaProcesoDataTable = DBMIMaging.SchemaProcess.TBL_Fecha_Proceso.DBGet(_Plugin.Manager.ImagingGlobal.ProyectoImagingRow.fk_Entidad, _Plugin.Manager.ImagingGlobal.ProyectoImagingRow.fk_Proyecto, Integer.Parse(Me.dtpFechaEjecucion.Value.ToString("yyyyMMdd")), Nothing)
                Else
                    fechaProcesoDataTable = DBMIMaging.SchemaProcess.TBL_Fecha_Proceso.DBGet(_Plugin.Manager.ImagingGlobal.ProyectoImagingRow.fk_Entidad, _Plugin.Manager.ImagingGlobal.ProyectoImagingRow.fk_Proyecto, fechaProceso, Nothing)
                End If

                If fechaProcesoDataTable.Rows.Count = 0 Then
                    DesktopMessageBoxControl.DesktopMessageShow("Error, no existe la fecha de proceso Seleccionada", "Error de Fecha proceso", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                    Return False
                Else

                    If chk_CargaLogsFechasAnteriores.Checked = True Then
                        'fechas de proceso anterior
                        ControlCargue = dbmIntregation.SchemaBCSCarpetaUnica.TBL_Control_Cargue_Logs.DBFindByfk_Tipo_ProcesoFecha_Proceso(Me.fk_tipo_Proceso, Integer.Parse(Me.dtpFechaEjecucion.Value.ToString("yyyyMMdd")))
                        ControlProceso = dbmIntregation.SchemaBCSCarpetaUnica.TBL_Control_Proceso.DBFindByfk_Tipo_ProcesoFecha_Proceso(Me.fk_tipo_Proceso, Integer.Parse(Me.dtpFechaEjecucion.Value.ToString("yyyyMMdd")))
                    Else
                        ControlCargue = dbmIntregation.SchemaBCSCarpetaUnica.TBL_Control_Cargue_Logs.DBFindByfk_Tipo_ProcesoFecha_Proceso(Me.fk_tipo_Proceso, fechaProceso)
                        ControlProceso = dbmIntregation.SchemaBCSCarpetaUnica.TBL_Control_Proceso.DBFindByfk_Tipo_ProcesoFecha_Proceso(Me.fk_tipo_Proceso, fechaProceso)
                    End If

                    If chk_CargaLogsFechasAnteriores.Checked = True Then
                        'verifica carga de logs fechas anteriores a fecha de ejecucion.
                        If ControlCargue.Rows.Count > 0 And ControlProceso.Rows.Count > 0 Then
                            'verifica que la cantidad de logs cargados sea menor a la cantidad que se deben cargar y cargue completo aun no este completo
                            If Not (Convert.ToBoolean(ControlProceso.Rows(0)("Cargue_Completo"))) Then
                                If (Convert.ToBoolean(fechaProcesoDataTable.Rows(0)("Cerrado"))) Then
                                    If ((Convert.ToInt32(ControlCargue.Rows(0)("Cantidad_Logs_Cargados")) < Convert.ToInt32(ControlCargue.Rows(0)("Cantidad_Logs_Cargar")))) Then
                                        Return True
                                    Else
                                        DesktopMessageBoxControl.DesktopMessageShow("No es posible realizar mas carga de Log para este tipo de proceso en la fecha seleccionada!", "Carga de Log", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                                        Return False
                                    End If
                                ElseIf (Convert.ToBoolean(fechaProcesoDataTable.Rows(0)("Cerrado")) = False) Then
                                    DesktopMessageBoxControl.DesktopMessageShow("La fecha de proceso no ha sido cerrada, no es posible cargar Log a través de esta opción!", "Carga de Log", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                                    Return False
                                End If
                            Else
                                DesktopMessageBoxControl.DesktopMessageShow("No es posible realizar mas carga de Log para este tipo de proceso en la fecha seleccionada!", "Cargue Completo..", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                                Return False
                            End If
                        Else
                            DesktopMessageBoxControl.DesktopMessageShow("Error no se encontro registros de control de cargue para este tipo de proceso y fecha de proceso", "Error carga Logs", DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
                            Return False
                        End If
                    Else
                        If (Convert.ToBoolean(fechaProcesoDataTable.Rows(0)("Cerrado"))) Then
                            DesktopMessageBoxControl.DesktopMessageShow("La fecha proceso seleccionada no se encuentra abierta", "Error Cargue", DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
                            Return False
                        ElseIf (Convert.ToBoolean(fechaProcesoDataTable.Rows(0)("Cerrado")) = False) Then
                            If ControlCargue.Rows.Count > 0 And ControlProceso.Rows.Count > 0 Then
                                If Not (Convert.ToBoolean(ControlProceso.Rows(0)("Cargue_Completo"))) Then
                                    If ((Convert.ToInt32(ControlCargue.Rows(0)("Cantidad_Logs_Cargados")) < Convert.ToInt32(ControlCargue.Rows(0)("Cantidad_Logs_Cargar")))) Then
                                        Return True
                                    Else
                                        DesktopMessageBoxControl.DesktopMessageShow("No es posible realizar mas carga de Log para este tipo de proceso en la fecha seleccionada!", "Cargue Completo..", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                                        Return False
                                    End If
                                Else
                                    DesktopMessageBoxControl.DesktopMessageShow("No es posible realizar mas carga de Log para este tipo de proceso en la fecha seleccionada!", "Cargue Completo..", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                                    Return False
                                End If
                            Else
                                'realiza primer cargue
                                Return True
                            End If
                        End If
                    End If

                End If

                Return retorno
            Catch ex As Exception
                DMB.DesktopMessageShow("Crear Fecha de Proceso", ex)
                DialogResult = Windows.Forms.DialogResult.Cancel
            Finally
                DBMIMaging.Connection_Close()
                dbmIntregation.Connection_Close()
            End Try

            Me.DialogResult = Windows.Forms.DialogResult.OK
            Return retorno
        End Function

        Public Function ValidarFechaProcesoEjecucion() As Boolean
            Dim retorno As Boolean = True

            If Me.chk_CargaLogsFechasAnteriores.Checked = True Then
                _fechaProceso = Me.dtpFechaEjecucion.Value
                _fechaProcesoEjecucion = Me.dtpFechaProceso.Value
            Else
                _fechaProceso = Me.dtpFechaProceso.Value
                _fechaProcesoEjecucion = Me.dtpFechaEjecucion.Value
            End If
            
            If (_fechaProcesoEjecucion.Date < _fechaProceso.Date) Then
                DesktopMessageBoxControl.DesktopMessageShow("Error, la fecha de proceso ejecucion no debe ser menor a la fecha de proceso", "Error de Fecha proceso Ejecucion", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                Return False
            End If
            Dim DBMIMaging As New DBImagingDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)
            Dim dbmIntregation As New DBIntegrationDataBaseManager(_Plugin.CajaSocialConnectionString)

            Try
                DBMIMaging.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
                dbmIntregation.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
                Dim fecha_ProcesoEjecucion = Integer.Parse(_fechaProcesoEjecucion.ToString("yyyyMMdd"))
                Dim fechaProcesoEjecucionDataTable = DBMIMaging.SchemaProcess.TBL_Fecha_Proceso.DBGet(_Plugin.Manager.ImagingGlobal.ProyectoImagingRow.fk_Entidad, _Plugin.Manager.ImagingGlobal.ProyectoImagingRow.fk_Proyecto, fecha_ProcesoEjecucion, Nothing)
                If fechaProcesoEjecucionDataTable.Rows.Count = 0 Then
                    DesktopMessageBoxControl.DesktopMessageShow("Error, no existe la fecha de proceso de ejecucion seleccionada", "Error de Fecha proceso Ejecucion", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                    Return False
                Else
                    If (Convert.ToBoolean(fechaProcesoEjecucionDataTable.Rows(0)("Cerrado"))) Then
                        DesktopMessageBoxControl.DesktopMessageShow("Error, la fecha de proceso de ejecucion se encuentra cerrada", "Error de Fecha proceso Ejecucion", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                        Return False
                    Else
                        'Fecha ejecucion valida
                        Return True
                    End If
                End If

                Return retorno
            Catch ex As Exception
                DMB.DesktopMessageShow("Fecha de Proceso Ejecucion", ex)
                DialogResult = Windows.Forms.DialogResult.Cancel
            Finally
                DBMIMaging.Connection_Close()
                dbmIntregation.Connection_Close()
            End Try

            Me.DialogResult = Windows.Forms.DialogResult.OK
            Return retorno
        End Function

        Public Shared Function ToDataTable(Of T)(data As IList(Of Object)) As DataTable
            Dim props As PropertyDescriptorCollection = TypeDescriptor.GetProperties(GetType(Object))
            Dim table As New DataTable()
            For i As Integer = 0 To data.Count - 1
                Dim prop As PropertyDescriptor = props(i)
                table.Columns.Add(prop.Name, prop.PropertyType)
            Next
            Dim values As Object() = New Object(props.Count - 1) {}
            For Each item As Object In data
                For i As Integer = 0 To values.Length - 1
                    values(i) = props(i).GetValue(item)
                Next
                table.Rows.Add(values)
            Next
            Return table
        End Function

        Private Function CargarArchivo(ByVal NombreArchivo As String, ByVal Entidad As Short, ByVal Esquema As Short, ByVal Proyecto As Short, ByVal IdTipo_log As Integer, ByVal Nombre_Proceso As String) As DesktopConfig.TypeResult
            Dim trReturn As New DesktopConfig.TypeResult
            trReturn.Result = True
            Dim listErrores As List(Of String) = New List(Of String)
            Try
                'Procesa Log por TipoLog
                Select Case (_NombreProceso)
                    Case "PASIVO"
                        Return Cargue_CTACORaammdd(NombreArchivo, Entidad, Proyecto, IdTipo_log)
                    Case "DOE"
                        Return Cargue_DOE(NombreArchivo, Entidad, Proyecto, IdTipo_log)
                    Case "MICROFINANZAS"
                        Return Cargue_Microfinanzas(NombreArchivo, Entidad, Proyecto, IdTipo_log)
                    Case "ACUSES DE RECIBIDO"
                        Return Cargue_AcuseRecibido(NombreArchivo, Entidad, Proyecto, IdTipo_log)
                    Case "GORO VOLUMEN"
                        Return Cargue_GoroVoluen(NombreArchivo, Entidad, Proyecto, IdTipo_log)
                    Case "FATCA"
                        Return Cargue_FATCA(NombreArchivo, Entidad, Proyecto, IdTipo_log)
                    Case "AINCO"
                        Return Cargue_AINCO_NOV(NombreArchivo, Entidad, Proyecto, IdTipo_log)
                    Case "CONSTRUCTOR"
                        If (Me._nombreTipoLog.ToUpper() = "CONSTRUCTOR EXCEL" Or Me._nombreTipoLog.ToUpper() = "CONSTRUCTOR EXCEL-ACTUALIZACIONES") Then
                            Return Cargue_CONSTRUCTOR_EXCEL(NombreArchivo, Entidad, Proyecto, IdTipo_log)
                        Else
                            Return Cargue_DINAMICO(NombreArchivo, Entidad, Proyecto, IdTipo_log)
                        End If
                    Case Else
                        Return Cargue_DINAMICO(NombreArchivo, Entidad, Proyecto, IdTipo_log)
                End Select

            Catch ex As Exception
                trReturn.Result = False
                listErrores.Add(ex.Message)
                trReturn.Parameters = listErrores

                If (_NombreProceso = "AINCO" Or _NombreProceso = "ACUSES DE RECIBIDO" Or _NombreProceso = "FATCA") Then
                    KillSpecificProcess("EXCEL")
                End If
            End Try

            Return trReturn
        End Function

#End Region

#Region " Eventos "

        Private Sub DesktopComboBoxControlProcesos_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles DesktopComboBoxControlProcesos.SelectedIndexChanged
            CargaTipoJornadaProceso()
            CargaTiposLog()
        End Sub

        Private Sub FormCargue_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
            If Me.CargueBackgroundWorker.IsBusy Then
                Me.CargueBackgroundWorker.CancelAsync()
            End If
        End Sub

        Private Sub FormCargue_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            Try
                Me.dtpFechaProceso.MaxDate = Date.Today
                CargaDatos()
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("FormCargue_Load", ex)
            End Try
        End Sub

        Private Sub BuscarArchivoButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BuscarArchivoButton.Click
            BuscarArchivo()
        End Sub

        Private Sub ArchivoDesktopTextBox_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ArchivoDesktopTextBox.Click
            BuscarArchivo()
        End Sub

        Private Sub CargarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CargarButton.Click
            Dim IdTipo_Log = CShort(Me.DesktopComboBoxControlTiposLog.SelectedValue)
            Me.CargueBackgroundWorker.WorkerSupportsCancellation = True
            Me._NombreProceso = Me.DesktopComboBoxControlProcesos.Text

            Dim dbmIntegration As New DBIntegrationDataBaseManager(Me._Plugin.CajaSocialConnectionString)
            Try
                dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
                If Not IsNothing(_File) Then
                    Dim NombreArchivo As String = Path.GetFileName(CType(_File, FileStream).Name)
                    Dim fk_Tipo_Proceso = CShort(Me.DesktopComboBoxControlProcesos.SelectedValue)
                    Me.fk_tipo_Proceso = fk_Tipo_Proceso
                    Me.fk_Tipo_Log = IdTipo_Log
                    Dim Extension As String = ""
                    Dim TipoLogType = dbmIntegration.SchemaConfig.TBL_Tipos_Log.DBGet(IdTipo_Log, CInt(_Plugin.Manager.ImagingGlobal.Entidad), CInt(_Plugin.Manager.ImagingGlobal.Proyecto))
                    Dim RowTipoLog As DBIntegration.SchemaConfig.TBL_Tipos_LogRow = TipoLogType.Rows(0)
                    Me._validaExtension = CBool(TipoLogType.Rows(0)("Valida_Extension"))
                    Me._nombreTipoLog = Me.DesktopComboBoxControlTiposLog.Text

                    If (TipoLogType.Rows.Count > 0) Then
                        Me.chkEncabezado.Checked = CType(TipoLogType.Rows(0), DBIntegration.SchemaConfig.TBL_Tipos_LogRow).ManejaEncabezado

                        Me._tipoLog_ValidaDuplicados = CBool(TipoLogType.Rows(0)("Valida_Duplicados"))
                        If ValidaFormatoCorrecto_Log(IdTipo_Log, NombreArchivo, RowTipoLog) = False Then
                            DesktopMessageBoxControl.DesktopMessageShow("El formato del archivo no es correcto, por favor verifique y cargue nuevamente!!", "Format Archivo de cargue", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                            Return
                        End If
                        If Me._validaExtension Then
                            Extension = NombreArchivo.Substring(NombreArchivo.IndexOf("."))
                            Me.ExtensionGlobal = Extension
                            If Not ValidarExtension(Extension, dbmIntegration) Then
                                Return
                            End If
                        Else
                            Me.ExtensionGlobal = ""
                        End If
                        If (CBool(TipoLogType.Rows(0)("Valida_Jornada"))) Then
                            If Not ValidarJornada(NombreArchivo, dbmIntegration) Then
                                Return
                            End If
                        End If

                        ValidarExiste_ArchivoCargue(NombreArchivo, dbmIntegration)
                    Else
                        DesktopMessageBoxControl.DesktopMessageShow("Error Tipo Log No se Encuentra en Base de datos!!", "Error Tipo No Encontrado", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                        Return
                    End If
                Else
                    DesktopMessageBoxControl.DesktopMessageShow("Debe seleccionar un archivo de cargue", "Archivo de cargue", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                    Return
                End If



            Catch ex As Exception
                HabilitarControles(True)
                DesktopMessageBoxControl.DesktopMessageShow("CargarButton_Click", ex)
            Finally
                dbmIntegration.Connection_Close()
            End Try
        End Sub

        Private Sub CerrarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CerrarButton.Click
            If CargueBackgroundWorker.IsBusy Then
                Dim resultado = DesktopMessageBoxControl.DesktopMessageShow("En este momento se esta procesando un cargue de archivo, ¿Desea cancelar la operación?", "Cancelación de cargue", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, False)
                If resultado = DialogResult.OK Then
                    Me.CargueBackgroundWorker.CancelAsync()
                    Me.Close()
                End If
            Else
                Me.Close()
            End If
        End Sub

        Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As EventArgs) Handles Timer1.Tick
            If Timer1.Enabled Then
                Segundos += 1

                If Segundos = 60 Then
                    Segundos = 0
                    Minuto += 1
                End If

                If Minuto = 60 Then
                    Minuto = 0
                    Hora += 1
                End If

                TiempoLabel.Text = String.Format("{0:00}", Hora) + ":" + String.Format("{0:00}", Minuto) + ":" + String.Format("{0:00}", Segundos)
            End If
        End Sub

        Private Sub dtpFechaProceso_ValueChanged(sender As System.Object, e As System.EventArgs) Handles dtpFechaProceso.ValueChanged
            Dim FechaProcesoSeleccionada = Me.dtpFechaProceso.Value
            If chk_CargaLogsFechasAnteriores.Checked = False Then
                Me.dtpFechaEjecucion.Value = Me.dtpFechaProceso.Value
            End If
        End Sub

        Private Sub DesktopComboBoxControlJornada_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles DesktopComboBoxControlJornada.SelectedIndexChanged
            CargaTiposLog()
        End Sub

        Private Sub chk_CargaLogsFechasAnteriores_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chk_CargaLogsFechasAnteriores.CheckedChanged
            If chk_CargaLogsFechasAnteriores.Checked = True Then
                Me.dtpFechaEjecucion.Enabled = True
            Else
                Me.dtpFechaEjecucion.Enabled = False
                Me.dtpFechaEjecucion.Value = dtpFechaProceso.Value
            End If
        End Sub

#End Region

#Region "BackgroundWorker"

        Private Sub CargueBackgroundWorker_DoWork(ByVal sender As System.Object, ByVal e As DoWorkEventArgs) Handles CargueBackgroundWorker.DoWork
            Dim worker As BackgroundWorker = CType(sender, BackgroundWorker)
            If worker.CancellationPending Then e.Cancel = True

            'Se obtiene entidad, esquema
            Dim Parametros = e.Argument.ToString().Split(CChar("-"))
            Dim NombreArchivo As String = Parametros(0)

            Dim Entidad = CShort(Parametros(1))
            Dim Proyecto = CShort(Parametros(2))
            Dim IdTipoLog = CInt(Parametros(3))
            trResultado = CargarArchivo(NombreArchivo, Entidad, 0, Proyecto, IdTipoLog, Me._NombreProceso)
            If (trResultado.Result) Then
                CargueValido = True
            Else
                CargueValido = False
            End If
            Me.CargueBackgroundWorker.ReportProgress(CargueProgressBar.Maximum)
        End Sub

        Private Sub CargueBackgroundWorker_ProgressChanged(ByVal sender As System.Object, ByVal e As ProgressChangedEventArgs) Handles CargueBackgroundWorker.ProgressChanged
            If e.ProgressPercentage = 0 Then
                CargandoPictureBox.Visible = True
                CargueProgressBar.Maximum = _DataRegistros
                TotalRegistrosLabel.Text = _DataRegistros.ToString()

                If _EstadoProceso = 0 Then
                    ProcesadosTituloLabel.Text = "Validados:"
                ElseIf _EstadoProceso = 1 Then
                    ProcesadosTituloLabel.Text = "Procesados:"
                End If
                _EstadoProceso = CShort(_EstadoProceso + 1)

            Else
                'Inicio Proceso
                If (e.ProgressPercentage < 100) Then
                    CargueProgressBar.Value = e.ProgressPercentage
                End If

                ProcesadosLabel.Text = e.ProgressPercentage.ToString()
            End If
        End Sub

        Private Sub CargueBackgroundWorker_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As RunWorkerCompletedEventArgs) Handles CargueBackgroundWorker.RunWorkerCompleted
            CargandoPictureBox.Visible = False
            Timer1.Enabled = False
            Dim dbmIntegration As New DBIntegrationDataBaseManager(Me._Plugin.CajaSocialConnectionString)
            dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
            ArchivoDesktopTextBox.Text = ""

            Try
                If Me.CargueValido Then
                    DatosCargadosDesktopDataGridView.DataSource = _DataFile
                    CargueProgressBar.Value = CargueProgressBar.Maximum
                    Dim strMsjFinal = "Datos cargados éxitosamente"
                    If (Me._contadorNroProductos_vacios > 0) Then
                        strMsjFinal = strMsjFinal + Environment.NewLine + "'NOTA: Para este cargue hubieron " + Me._contadorNroProductos_vacios.ToString() + " Nro de Producto vacios'"
                        Me._contadorNroProductos_vacios = 0
                    End If
                    DesktopMessageBoxControl.DesktopMessageShow(strMsjFinal, "Cargue de datos", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                    HabilitarControles(True)
                    TiempoLabel.Text = "00:00:00"
                    CargueProgressBar.Value = 0
                    DatosCargadosDesktopDataGridView.DataSource = Nothing
                    _DataFile = Nothing
                    TotalRegistrosLabel.Text = "0"
                    ProcesadosLabel.Text = "0"
                    Me._DataRegistros = 0
                    Me._DataColumnas = 0
                Else
                    If Not IsNothing(trResultado.Parameters) Then
                        Dim Cargue_Log_Detalle = New DBIntegration.SchemaConfig.TBL_Cargue_Log_DetalleType
                        Cargue_Log_Detalle.fk_Cargue = Me._idCargue
                        Cargue_Log_Detalle.Fecha_Cargue = Nothing
                        Cargue_Log_Detalle.fk_Usuario_Log = _Plugin.Manager.Sesion.Usuario.id
                        Dim var = dbmIntegration.SchemaConfig.TBL_Cargue.DBGet(Me._idCargue)
                        If (var.Rows.Count > 0) Then
                            Cargue_Log_Detalle.Fecha_Cargue = CDate(var.Rows(0)("Fecha_Log"))
                        Else
                            Cargue_Log_Detalle.Fecha_Cargue = Nothing
                        End If

                        For Each itemError In trResultado.Parameters
                            Cargue_Log_Detalle.Descripcion_Error = itemError
                            dbmIntegration.SchemaConfig.TBL_Cargue_Log_Detalle.DBInsert(Cargue_Log_Detalle)
                        Next
                        Dim myError As New FormResultadoValidacion(trResultado)
                        CargueBackgroundWorker.Dispose()
                        myError.ShowDialog()
                        HabilitarControles(True)
                        TiempoLabel.Text = "00:00:00"
                        CargueProgressBar.Value = 0
                        DatosCargadosDesktopDataGridView.DataSource = Nothing
                        _DataFile = Nothing
                        TotalRegistrosLabel.Text = "0"
                        ProcesadosLabel.Text = "0"
                        Me._DataRegistros = 0
                        Me._DataColumnas = 0
                    End If
                End If
                _File = Nothing
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Insertar Cargue (Evento:CargueBackgroundWorker_RunWorkerCompleted)  ", ex)
            Finally
                dbmIntegration.Connection_Close()
            End Try
        End Sub
#End Region

#Region "BackgroundTemporal"

        Private Sub BackgroundTemporal_DoWork(sender As System.Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundTemporal.DoWork
            Dim bw = CType(sender, BackgroundWorker)
            If bw.CancellationPending Then
                e.Cancel = True
            Else
                Using file As New System.IO.StreamWriter(Me.nTempPathFile_Microfinanzas, True)
                    Me.ltMicrofinanzas = Me.ltMicrofinanzas.Where(Function(x) x.Trim() <> Nothing).ToList()

                    For Each item In Me.ltMicrofinanzas
                        file.WriteLine(item)
                    Next
                End Using
            End If
        End Sub

        Private Sub BackgroundTemporal_RunWorkerCompleted(sender As System.Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundTemporal.RunWorkerCompleted
            Me.BackgroundTemporal.CancelAsync()
            Me.BackgroundTemporal.Dispose()
        End Sub

#End Region

    End Class

End Namespace

