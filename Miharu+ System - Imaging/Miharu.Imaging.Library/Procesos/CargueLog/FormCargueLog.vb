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
Imports System.Dynamic
Imports DBImaging
Imports DMB = Miharu.Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl
Imports System.Linq
Imports System.Xml
Imports System.Xml.Serialization
Imports System.Text.RegularExpressions

Namespace Procesos.CargueLog
    Public Class FormCargueLog
        Inherits FormBase

#Region " Declaraciones "

        Private _File As Stream = Nothing
        Private _DataFile As DataTable = Nothing
        Private _DataRegistros As Integer = 0
        Private _DataColumnas As Integer = 0
        Private _EstadoProceso As Short = 0 '0 Validando, 1 Procesando.
        Private objCSV As New Slyg.Tools.CSV.CSVData
        Private objXLS As New XLSData
        Private trResultado As DesktopConfig.TypeResult
        'Private _TipoCargue As DesktopConfig.TipoCargue
        Private Segundos As Integer = 0
        Private Minuto As Integer = 0
        Private Hora As Integer = 0
        Private TiposLogDataTable As DBImaging.SchemaConfig.TBL_Tipos_LogDataTable
        Private TiposLogrow As DBImaging.SchemaConfig.TBL_Tipos_LogRow
        Private validaArchivoCargue As Boolean
        Private SaltoPrimasLineas As Integer
        Private ExtensionGlobal As String = ""
        Private _fechaProceso As DateTime
        Private passwordFile As String = ""
        Private Usa_Fecha_Recaudo As Boolean = False

#End Region

#Region " Constructor "

        Sub New()
            ' Sub New(ByVal TipoCargue As DesktopConfig.TipoCargue)
            ' This call is required by the designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            '_TipoCargue = TipoCargue
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
            Dim dbmImaging As New DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
            Try
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                TiposLogDataTable = dbmImaging.SchemaConfig.TBL_Tipos_Log.DBFindByfk_Entidadfk_ProyectoEliminado(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, False)
                Utilities.Llenarcombo_(DesktopComboBoxControlTiposLog, TiposLogDataTable, TiposLogDataTable.id_Tipo_logColumn.ColumnName, TiposLogDataTable.Nombre_Tipo_LogColumn.ColumnName, True)

                If (TiposLogDataTable.Count > 0) Then
                    If TiposLogDataTable.Columns.Contains("Usa_Fecha_Recaudo") Then
                        Usa_Fecha_Recaudo = Convert.ToBoolean(TiposLogDataTable.Rows(0).Item("Usa_Fecha_Recaudo"))
                    End If

                    If (Usa_Fecha_Recaudo) Then
                        dtpFechaRecaudo.Visible = True
                        lblFechaRecaudo.Visible = True
                    End If
                End If

                AddHandler DesktopComboBoxControlTiposLog.SelectedIndexChanged, New EventHandler(AddressOf DesktopComboBoxControlTiposLog_SelectedIndexChanged)

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaDatos", ex)
            Finally
                dbmImaging.Connection_Close()
            End Try
        End Sub

#End Region

#Region " Funciones "

        Private Function SubString(ByVal CamposLog As DataTable, ByVal File As Stream) As DataTable

            Dim Lee = New System.IO.StreamReader(CType(File, FileStream).Name)
            Dim Data = New DataTable()

            For Each x As DataRow In CamposLog.Rows
                Data.Columns.Add(x.Item("Descripcion").ToString())
            Next

            Dim texto = ""
            Dim numLinea = 0
            If CInt(TiposLogrow.Salto_Primeras_Lineas) = 0 Then
                texto = Lee.ReadLine()
                numLinea = numLinea + 1
            Else
                For index As Integer = 0 To CInt(TiposLogrow.Salto_Primeras_Lineas) ' - 1
                    texto = Lee.ReadLine()
                    numLinea = numLinea + 1
                Next
            End If

            Dim longitud_Fija = 0

            If Not IsDBNull(TiposLogrow.Longitud_Fija) Then
                longitud_Fija = CInt(TiposLogrow.Longitud_Fija)
            End If

            While texto IsNot Nothing
                Dim lenTexto = texto.Length
                Dim Valida = True
                If (TiposLogrow.Identificador_Linea <> "" Or Not IsDBNull(TiposLogrow.Identificador_Linea)) Then
                    Dim lon = TiposLogrow.Identificador_Linea.Length
                    If (lenTexto > 0) Then
                        If (texto.Substring(0, lon) = TiposLogrow.Identificador_Linea) Then
                            Valida = True
                        Else
                            Valida = False
                            texto = Lee.ReadLine()
                        End If
                    Else
                        Valida = False
                        texto = Lee.ReadLine()
                    End If
                End If

                If (Valida) Then
                    If (longitud_Fija <> 0 And lenTexto = longitud_Fija) Or (longitud_Fija = 0) Or (Not TiposLogrow.IsidentificadorLineaDetalleNull()) Then
                        Dim row = Data.NewRow
                        Try
                            'TODO
                            If (Not TiposLogrow.IsidentificadorLineaDetalleNull()) Then
                                If texto.Substring(0, TiposLogrow.identificadorLineaDetalle.Length) = TiposLogrow.identificadorLineaDetalle Then
                                    For Each x As DataRow In CamposLog.Rows
                                        row.Item(x.Item("Descripcion").ToString()) = "" + texto.Substring(CInt(x.Item("Length_Inicia")), CInt(x.Item("Length_Fijo")))
                                    Next
                                    Data.Rows.Add(row)
                                End If
                            Else
                                For Each x As DataRow In CamposLog.Rows
                                    row.Item(x.Item("Descripcion").ToString()) = "" + texto.Substring(CInt(x.Item("Length_Inicia")), CInt(x.Item("Length_Fijo")))
                                Next
                                Data.Rows.Add(row)
                            End If
                        Catch
                            Throw New Exception("Error en la linea " + numLinea.ToString() + " por favor revisar el log y volver a intentar")
                        End Try
                        texto = Lee.ReadLine()
                        numLinea = numLinea + 1
                    Else
                        Throw New Exception("Error en la linea " + numLinea.ToString() + " por favor revisar el log y volver a intentar")
                    End If
                End If

            End While

            Dim saltosUltimas = ""

            If Not IsDBNull(TiposLogrow.Salto_Ultimas_Lineas) Then
                saltosUltimas = TiposLogrow.Salto_Ultimas_Lineas
            End If

            For index As Integer = 0 To CInt(saltosUltimas) - 1
                Data.Rows.RemoveAt(Data.Rows.Count - 1)
            Next

            Return Data
        End Function

        Private Function CargarArchivo(ByVal NombreArchivo As String, ByVal Entidad As Short, ByVal Esquema As Short) As DesktopConfig.TypeResult
            Dim trReturn As New DesktopConfig.TypeResult
            trReturn.Result = True
            Dim _Data As DataTable = New DataTable

            Dim CamposLog As DataTable = New DataTable

            Dim CamposLLenos As Boolean = False
            Dim contadorValoresEncontrados As Integer = 0
            Dim ltCamposLog As List(Of String) = New List(Of String)
            Try
                Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

                dbmImaging = New DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                If ((ExtensionGlobal.ToUpper() = ".XLS") Or (ExtensionGlobal.ToUpper() = ".XLSX")) Then
                    Dim dsHojas As DataSet = objXLS.ImportExcelXLS(CType(_File, FileStream).Name, TiposLogrow.ManejaEncabezado)
                    _Data = dsHojas.Tables(0)
                ElseIf ExtensionGlobal.ToUpper() = ".XML" Then
                    Try
                        Dim ManejaNodoPrincipalLog As Boolean = TiposLogrow.Maneja_Nodo_Principal_XML
                        Dim NombreNodoPrincipalLog As String = TiposLogrow.Nombre_Nodo_Principal_XML
                        Dim ManejaNodoCabeceraLog As Boolean = TiposLogrow.Maneja_Nodo_Cabecera_XML
                        Dim NombreNodoCabeceraLog As String = TiposLogrow.Nombre_Nodo_Cabecera_XML
                        Dim NombreNodoListaRegistrosLog As String = TiposLogrow.Nombre_Nodo_Lista_RegistrosXML

                        'Creamos el "Document"
                        Dim xmlDocument = New XmlDocument()

                        'Cargamos el archivo
                        xmlDocument.Load(CType(_File, FileStream).Name)

                        'Obtenemos los campos del log
                        CamposLog = dbmImaging.SchemaConfig.TBL_Campos_Tipo_log.DBFindByfk_Tipo_logfk_Entidadfk_ProyectoEliminado(CInt(DesktopComboBoxControlTiposLog.SelectedValue), Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, False)

                        Dim listaTagsCabecera As List(Of String) = New List(Of String)
                        Dim listaTagsRegistros As List(Of String) = New List(Of String)

                        'Se crean las columnas de la tabla Data y se identifican los campos segun clasificacion de cabecera o registro
                        For Each x As DataRow In CamposLog.Rows
                            If (_Data.Columns.Contains(x.Item("Nombre_Tag_Campo").ToString()) = False) Then
                                _Data.Columns.Add(x.Item("Nombre_Tag_Campo").ToString())
                            End If

                            Dim Is_Tag_Cabecera As Boolean = CBool(x.Item("Is_Tag_Cabecera"))
                            If (Is_Tag_Cabecera = True) Then
                                If (listaTagsCabecera.Contains(x.Item("Nombre_Tag_Campo").ToString()) = False) Then
                                    listaTagsCabecera.Add(x.Item("Nombre_Tag_Campo").ToString)
                                End If
                            Else
                                If (listaTagsRegistros.Contains(x.Item("Nombre_Tag_Campo").ToString()) = False) Then
                                    listaTagsRegistros.Add(x.Item("Nombre_Tag_Campo").ToString)
                                End If
                            End If
                        Next

                        Dim nombreNodoPrincipal As String = ""
                        Dim nombreSingleNodoCabecera As String = ""
                        Dim nombreSingleNodoRegistros As String = ""

                        If (ManejaNodoPrincipalLog = True) Then
                            nombreNodoPrincipal = NombreNodoPrincipalLog
                        End If

                        Dim listDataCabecera As List(Of String) = New List(Of String)

                        If (ManejaNodoCabeceraLog = True) Then

                            nombreSingleNodoCabecera = "/" & nombreNodoPrincipal & "/" & NombreNodoCabeceraLog

                            Dim nodoCabecera As XmlNode

                            'Obtenemos los hijos del nodo cabecera
                            nodoCabecera = xmlDocument.SelectSingleNode(nombreSingleNodoCabecera.Replace("//", "/"))

                            For i As Integer = 0 To listaTagsCabecera.Count - 1
                                For Each node As XmlNode In nodoCabecera.ChildNodes
                                    If (node.Name.ToString.ToLower = listaTagsCabecera(i).ToString.ToLower) Then
                                        listDataCabecera.Add(node.InnerText.ToString)
                                        Exit For
                                    End If
                                Next
                            Next
                        End If

                        Dim nodoRegList As XmlNodeList
                        'Obtenemos la lista de los nodos "caja"
                        nombreSingleNodoRegistros = "/" & nombreNodoPrincipal & "/" & NombreNodoListaRegistrosLog
                        nodoRegList = xmlDocument.SelectNodes(nombreSingleNodoRegistros.Replace("//", "/"))

                        Dim validacionEntidad As Boolean = True

                        'Iniciamos el ciclo de lectura Registros
                        For Each nodeReg As XmlNode In nodoRegList
                            Dim listDataRegistro As List(Of String) = CopyList(listDataCabecera)

                            For i As Integer = 0 To listaTagsRegistros.Count - 1
                                For Each attribute As XmlNode In nodeReg.Attributes
                                    If (attribute.Name.ToString.ToLower = listaTagsRegistros(i).ToString.ToLower) Then
                                        listDataRegistro.Add(attribute.InnerText.ToString)

                                        'Valido si los registros del log, correponden a la entidad seleccionada
                                        If (TiposLogrow.Valida_Entidad And attribute.Name.ToString.ToLower = TiposLogrow.Valida_Entidad_Contra_Campo.ToLower) Then

                                            If attribute.InnerText.ToString <> TiposLogrow.Valida_Entidad_Codigo Then
                                                MessageBox.Show("El Log que se está intentando cargar no corresponde al banco seleccionado", "Banco del log diferente al seleccionado")
                                                validacionEntidad = False
                                                Exit For
                                            End If
                                        End If

                                        Exit For
                                    End If
                                Next

                                If validacionEntidad = False Then
                                    Exit For
                                End If
                            Next

                            If validacionEntidad = False Then
                                Exit For
                            End If

                            _Data.Rows.Add(listDataRegistro.ToArray)
                        Next

                        If validacionEntidad = False Then
                            trReturn.Result = False
                            Dim lisMsgError = New List(Of String)
                            lisMsgError.Add("- Archivo no válido. Por favor verifique lo siguiente:")
                            lisMsgError.Add("- El Log que se está intentando cargar no corresponde a la Entidad seleccionada.")
                            trReturn.Parameters = lisMsgError
                            Return trReturn
                        End If
                    Catch ex As Exception
                        Console.Write(ex.ToString())
                    End Try
                Else
                    If (ExtensionGlobal.ToUpper() = ".TXT" Or ExtensionGlobal.ToUpper() = ".DAT" Or ExtensionGlobal.ToUpper() = ".RTA") And (TiposLogrow.Separador = "") Then
                        CamposLog = dbmImaging.SchemaConfig.TBL_Campos_Tipo_log.DBFindByfk_Tipo_logfk_Entidadfk_ProyectoEliminado(CInt(DesktopComboBoxControlTiposLog.SelectedValue), Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, False)
                        _Data = SubString(CamposLog, _File)
                    Else
                        objCSV.Separator = CChar(IIf(TiposLogrow.Separador = "TAB", ControlChars.Tab, TiposLogrow.Separador))
                        'Se realiza el cargue del archivo en un datatable para luego validarlo.
                        objCSV.LoadCSV(CType(_File, FileStream).Name, TiposLogrow.ManejaEncabezado)
                        _Data = objCSV.DataTable.ToDataTable()
                    End If
                End If


                Dim objProcesaCargue As ProcesaCargueLog = New ProcesaCargueLog()
                Dim trRespuestaCargue As New DesktopConfig.TypeResult

                Dim CamposTipoLogDataTable = dbmImaging.SchemaConfig.TBL_Campos_Tipo_log.DBFindByfk_Tipo_logfk_Entidadfk_ProyectoEliminado(TiposLogrow.id_Tipo_log, Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, False)
                Dim CamposDataLog = dbmImaging.SchemaProcess.Get_Tabla_Log.DBExecute(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto)

                _DataFile = New DataTable()

                For Each itemCampo As DataColumn In CamposDataLog.Columns
                    _DataFile.Columns.Add(itemCampo.ColumnName)
                Next

                If _Data.Rows.Count > 0 Then
                    For index As Integer = 0 To _Data.Rows.Count - 1
                        For Each itemCampo As DataColumn In CamposDataLog.Columns
                            If itemCampo.ToString().ToUpper.Contains("CAMPO_") Or itemCampo.ToString().ToUpper.Contains("FECHA_RECAUDO") Then
                                Dim campoBuscar = itemCampo.ToString()
                                Dim ax_encontrado = CamposTipoLogDataTable.Where(Function(x) x.Nombre_Campo = campoBuscar.ToString()).ToList()
                                Dim CampoTipoLogRow As DBImaging.SchemaConfig.TBL_Campos_Tipo_logRow = Nothing

                                If ax_encontrado.Count > 0 Then
                                    CampoTipoLogRow = ax_encontrado.First()
                                    Dim valorCampo = _Data.Rows(index)(CampoTipoLogRow.Descripcion).ToString()
                                    ltCamposLog.Add(valorCampo)
                                Else
                                    ltCamposLog.Add("")
                                End If
                            Else
                                ltCamposLog.Add("")
                            End If
                        Next
                        _DataFile.Rows.Add(ltCamposLog.ToArray())
                        _DataFile.Rows(index)("fk_Entidad") = Program.ImagingGlobal.Entidad
                        _DataFile.Rows(index)("fk_Proyecto") = Program.ImagingGlobal.Proyecto
                        _DataFile.Rows(index)("fk_Tipo_Log") = TiposLogrow.id_Tipo_log
                        _DataFile.Rows(index)("Fecha_Proceso") = dtpFechaProceso.Value.ToString("yyyyMMdd")
                        _DataFile.Rows(index)("fk_Tipo_Registro_Log") = 3 'detalle

                        If (Usa_Fecha_Recaudo) Then
                            _DataFile.Rows(index)("Fecha_Recaudo") = dtpFechaRecaudo.Value.ToString("yyyy/MM/dd")
                        End If

                        ltCamposLog.Clear()
                    Next

                End If

                _DataRegistros = _DataFile.Rows.Count
                _DataColumnas = _DataFile.Columns.Count

                If _DataRegistros > 0 And _DataColumnas > 1 Then
                    CargueBackgroundWorker.ReportProgress(0)

                    trRespuestaCargue = objProcesaCargue.ProcesaCargue(_DataFile, NombreArchivo, TiposLogrow.id_Tipo_log, TiposLogrow.Valida_Duplicados, CInt(dtpFechaProceso.Value.ToString("yyyyMMdd")), Me.CargueBackgroundWorker)

                    If trResultado.Result Then
                        If TiposLogrow.Usa_Proceso_Posterior AndAlso Not TiposLogrow.Isconsulta_Proceso_PosteriorNull Then
                            If TiposLogrow.consulta_Proceso_Posterior <> "" Then
                                Dim cSql As String = TiposLogrow.consulta_Proceso_Posterior
                                cSql = cSql.Replace("@fk_Cargue", CStr(trResultado.Cargue))
                                SqlData.ExecuteQuery(cSql, dbmImaging)
                            End If
                        End If
                    Else
                        trReturn.Result = False
                        trReturn.Parameters = trRespuestaCargue.Parameters
                        trReturn.Resumen = trRespuestaCargue.Resumen
                    End If

                    'If trRespuestaCargue.Result Then
                    '    trReturn = trRespuestaCargue
                    'Else
                    '    trReturn.Result = False
                    '    trReturn.Parameters = trRespuestaCargue.Parameters
                    '    trReturn.Resumen = trRespuestaCargue.Resumen
                    'End If
                Else
                    trReturn.Result = False
                    Dim lisMsgError = New List(Of String)
                    lisMsgError.Add("- Archivo no válido. Por favor verifique lo siguiente:")
                    lisMsgError.Add("- 1. Que el archivo contenga datos.")
                    lisMsgError.Add("- 2. El carácter de separación sea el adecuado.")
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

        Private Function ValidaFormatoCorrecto_Log(IdTipoLog As Integer, NombreArchivo As String, RowTipoLog As DBImaging.SchemaConfig.TBL_Tipos_LogRow) As Boolean
            Dim retorno As Boolean = True
            Dim NombreArchivo_Aux As String = ""
            If (TiposLogrow.Valida_Extension And NombreArchivo.Contains(".")) Then
                NombreArchivo_Aux = NombreArchivo.Remove(NombreArchivo.LastIndexOf("."), NombreArchivo.Length - NombreArchivo.LastIndexOf("."))
            Else
                NombreArchivo_Aux = NombreArchivo
            End If

            Try
                If (RowTipoLog IsNot Nothing) Then
                    ValidacionesSplit(retorno, TiposLogrow.Validaciones_ArchivoPlano.Split("|"c), TiposLogrow.LengthValidaciones_ArchivoPlano.Split("|"c), NombreArchivo_Aux)
                End If
            Catch generatedExceptionName As Exception

                Throw
            End Try
            Return retorno
        End Function

        Public Function CopyList(Of T)(oldList As List(Of T)) As List(Of T)

            'Serialize
            Dim xmlString As String = ""
            Dim string_writer As New StringWriter
            Dim xml_serializer As New XmlSerializer(GetType(List(Of T)))
            xml_serializer.Serialize(string_writer, oldList)
            xmlString = string_writer.ToString()

            'Deserialize
            Dim string_reader As New StringReader(xmlString)
            Dim newList As List(Of T)
            newList = DirectCast(xml_serializer.Deserialize(string_reader), List(Of T))
            string_reader.Close()

            Return newList
        End Function

        Private Function ValidacionesSplit(ByRef retorno As Boolean, StrValidacionColumn As String(), StrLengthValidaciones As String(), NombreArchivo_Aux As String) As Object
            Try
                If (StrValidacionColumn.Count() > 0) Then
                    For index As Integer = 0 To StrValidacionColumn.Count() - 1
                        Dim AuxItemValidacion As String = Nothing
                        If (StrValidacionColumn(index).ToString().Contains("FECHA") Or StrValidacionColumn(index).ToString().Contains("CORTE") Or StrValidacionColumn(index).ToString().Contains("CONSECUTIVO")) Then
                            AuxItemValidacion = StrValidacionColumn(index).ToString()
                        Else
                            AuxItemValidacion = StrValidacionColumn(index).ToString().ToUpper()
                        End If
                        Dim LengthItemValidacion As String() = StrLengthValidaciones(index).Split("-"c)
                        Dim lengthInicio As Integer = CInt(LengthItemValidacion(0).ToString())
                        Dim lengthFinal As Integer = CInt(LengthItemValidacion(1).ToString())
                        Dim auxArchivoVal As String = Nothing

                        If (AuxItemValidacion.Contains("*")) Then
                            Dim OrAnd = AuxItemValidacion.Split("*"c)
                            If (OrAnd.Contains("OR") And Not OrAnd.Contains("AND")) Then

                                OrAnd = OrAnd.Where(Function(x) Not x.Contains("OR")).ToArray()

                                For Each itemOr_loopVariable In OrAnd
                                    auxArchivoVal = NombreArchivo_Aux.Substring(lengthInicio, (lengthFinal - lengthInicio)).ToUpper()
                                    If (auxArchivoVal = itemOr_loopVariable.ToUpper()) Then
                                        retorno = True
                                    End If
                                Next
                            End If
                        ElseIf (AuxItemValidacion.Contains("<FECHA>")) Then
                            Dim dtAux As New DateTime()
                            auxArchivoVal = NombreArchivo_Aux.Substring(lengthInicio, (lengthFinal - lengthInicio))

                            AuxItemValidacion = AuxItemValidacion.Replace("<FECHA>", "")
                            retorno = DateTime.TryParseExact(auxArchivoVal, AuxItemValidacion, Nothing, DateTimeStyles.None, dtAux)


                            If (retorno = False) Then
                                ' TODO: might not be correct. Was : Exit For
                                Exit For
                            End If
                        ElseIf AuxItemValidacion.Contains("<OFICINA>") Then
                            AuxItemValidacion = AuxItemValidacion.Replace("<OFICINA", "")

                            Continue For
                        ElseIf AuxItemValidacion.Contains("<NUMERO>") Then
                            AuxItemValidacion = AuxItemValidacion.Replace("<NUMERO>", "")

                            Continue For
                        ElseIf AuxItemValidacion.Contains("<CORTE>") Then
                            auxArchivoVal = NombreArchivo_Aux.Substring(lengthInicio, (lengthFinal - lengthInicio)).ToUpper()
                            AuxItemValidacion = AuxItemValidacion.Replace("<CORTE>", "")

                            Dim check As New Regex(AuxItemValidacion)

                            retorno = check.IsMatch(auxArchivoVal)

                            If (retorno = False) Then
                                ' TODO: might not be correct. Was : Exit For
                                Exit For
                            End If
                        ElseIf AuxItemValidacion.Contains("<ANIO>") Then
                            AuxItemValidacion = AuxItemValidacion.Replace("<ANIO>", "")
                            Dim anios = Enumerable.Range(1900, DateTime.Now.Year)
                            Dim encontrado = anios.Where(Function(x) x.ToString() = AuxItemValidacion)

                            If encontrado.Count() = 0 Then
                                retorno = False
                            End If
                        ElseIf AuxItemValidacion.Contains("<CONCEPTO>") Then 'IMPUESTOS ASISTIDOS 
                            AuxItemValidacion = AuxItemValidacion.Replace("<CONCEPTO>", "")
                            Continue For
                        ElseIf AuxItemValidacion.Contains("<FORMATO>") Then 'IMPUESTOS ASISTIDOS 
                            AuxItemValidacion = AuxItemValidacion.Replace("<FORMATO>", "")
                            Continue For
                        ElseIf AuxItemValidacion.Contains("<VERSION>") Then 'IMPUESTOS ASISTIDOS 
                            AuxItemValidacion = AuxItemValidacion.Replace("<VERSION>", "")
                            Continue For
                        ElseIf AuxItemValidacion.Contains("<AÑO>") Then 'IMPUESTOS ASISTIDOS 
                            AuxItemValidacion = AuxItemValidacion.Replace("<AÑO>", "")
                            Continue For
                        ElseIf AuxItemValidacion.Contains("<CONSECUTIVO>") Then 'IMPUESTOS ASISTIDOS 
                            auxArchivoVal = NombreArchivo_Aux.Substring(lengthInicio, (lengthFinal - lengthInicio)).ToUpper()
                            AuxItemValidacion = AuxItemValidacion.Replace("<CONSECUTIVO>", "")

                            Dim check As New Regex(AuxItemValidacion)

                            retorno = check.IsMatch(auxArchivoVal)

                            If (retorno = False) Then
                                ' TODO: might not be correct. Was : Exit For
                                Exit For
                            End If
                        Else
                            auxArchivoVal = NombreArchivo_Aux.Substring(lengthInicio, (lengthFinal - lengthInicio)).ToUpper()
                            If (auxArchivoVal <> AuxItemValidacion) Then
                                retorno = False
                                ' TODO: might not be correct. Was : Exit For
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

        Private Sub ValidarExiste_ArchivoCargue(NombreArchivo As String, dbmImaging As DBImagingDataBaseManager)
            Try

                Dim Cargue_valido = dbmImaging.SchemaConfig.TBL_Cargue.DBFindByfk_Entidadfk_Proyectofk_Tipo_LogArchivo_CargueValido(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, TiposLogrow.id_Tipo_log, NombreArchivo, True)
                Dim fechaProceso = Me.dtpFechaProceso.Value.ToString("yyyyMMdd")

                If (Cargue_valido.[Select]("Fecha_Proceso = '" + fechaProceso + "'").Count() > 0) AndAlso Me.validaArchivoCargue = False Then
                    DesktopMessageBoxControl.DesktopMessageShow("El nombre de archivo que estan intentando cargar ya se encuentra registrado con un Cargue Valido.", "Archivo de Cargue existente", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                    Return
                End If

                If (Cargue_valido.Count > 0) AndAlso Me.validaArchivoCargue Then
                    DesktopMessageBoxControl.DesktopMessageShow("El nombre de archivo que estan intentando cargar ya se encuentra registrado con un Cargue Valido.", "Archivo de Cargue existente", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                    _File = Nothing
                    ArchivoDesktopTextBox.Text = ""
                    Return
                Else

                    If VerificarFechaProceso() Then
                        If (Path.GetExtension(DirectCast(_File, FileStream).Name).ToLower().StartsWith(".xl")) Then
                            If (MessageBox.Show("¿Este archivo maneja contraseña?", "Archivo con Password", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = System.Windows.Forms.DialogResult.Yes) Then
                                Me.passwordFile = Microsoft.VisualBasic.Interaction.InputBox("Digite la contraseña: ", "Password", "")
                            Else
                                Me.passwordFile = ""
                            End If
                        End If

                        Me.CargandoPictureBox.Visible = True
                        Timer1.Enabled = True
                        HabilitarControles(False)
                        CheckForIllegalCrossThreadCalls = False
                        Me.CargueBackgroundWorker.RunWorkerAsync(NombreArchivo)
                    End If
                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("ValidarExiste ArchivoCargue", ex)
            End Try
        End Sub

        Public Function VerificarFechaProceso() As Boolean
            Dim retorno As Boolean = True
            Dim ControlCargue As New DataTable()
            Dim ControlProceso As New DataTable()
            Dim fechaProcesoDataTable As New DataTable()

            _fechaProceso = Me.dtpFechaProceso.Value


            Dim dbmImaging As New DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)

            Try
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
                Dim fechaProceso As Integer = Integer.Parse(_fechaProceso.ToString("yyyyMMdd"))


                fechaProcesoDataTable = dbmImaging.SchemaProcess.TBL_Fecha_Proceso.DBGet(Program.ImagingGlobal.ProyectoImagingRow.fk_Entidad, Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto, fechaProceso, Nothing)

                If fechaProcesoDataTable.Rows.Count = 0 Then
                    DesktopMessageBoxControl.DesktopMessageShow("Error, no existe la fecha de proceso Seleccionada", "Error de Fecha proceso", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                    retorno = False
                ElseIf (Convert.ToBoolean(fechaProcesoDataTable.Rows(0)("Cerrado")) = True) Then
                    DesktopMessageBoxControl.DesktopMessageShow("La fecha de proceso ha sido cerrada, no es posible cargar Log a través de esta opción!", "Carga de Log", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                    retorno = False
                End If

                Return retorno
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Crear Fecha de Proceso", ex)
                DialogResult = System.Windows.Forms.DialogResult.Cancel
            Finally
                dbmImaging.Connection_Close()
            End Try

            Me.DialogResult = System.Windows.Forms.DialogResult.OK
            Return retorno
        End Function

#End Region

#Region " Eventos "

        Private Sub FormCargue_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
            If Me.CargueBackgroundWorker.IsBusy Then
                Me.CargueBackgroundWorker.CancelAsync()
            End If
        End Sub

        Private Sub FormCargue_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
            Thread.CurrentThread.CurrentCulture = New CultureInfo("es-MX")

            Try
                'Cambia Titulo
                Me.Text = "Cargue " '& _TipoCargue.ToString()
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
            Timer1.Enabled = True

            Me.CargueBackgroundWorker.WorkerSupportsCancellation = True

            Dim dbmImaging = New DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
            dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

            If Me.ArchivoDesktopTextBox.Text = "" Then
                DesktopMessageBoxControl.DesktopMessageShow("Error al cargar Log, Archivo a cargar Vacio!!", "ERROR", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                Return
            End If

            Dim strSeleccionado As String = Me.DesktopComboBoxControlTiposLog.Text
            Dim validaExtension As Boolean = False
            Dim Extension As String = ""
            Dim ExtensionBD As String = ""

            Try

                If Me._File IsNot Nothing Then
                    Dim NombreArchivo As String = Path.GetFileName(DirectCast(_File, FileStream).Name)

                    If TiposLogrow Is Nothing Then
                        DesktopMessageBoxControl.DesktopMessageShow("Debe seleccionar el tipo Log", "Error al seleccionar Tipo Log", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                        Return
                    End If

                    If (_File IsNot Nothing) Then

                        validaExtension = TiposLogrow.Valida_Extension
                        ExtensionBD = TiposLogrow.Extension_Archivo
                        validaArchivoCargue = TiposLogrow.Valida_ArchivoCargue

                        If Not String.IsNullOrEmpty(TiposLogrow.Salto_Primeras_Lineas.ToString()) Then
                            Me.SaltoPrimasLineas = Convert.ToInt32(TiposLogrow.Salto_Primeras_Lineas.ToString())
                        End If


                        If validaExtension Then
                            Extension = NombreArchivo.Substring(NombreArchivo.LastIndexOf("."))

                            If Extension.ToString() = "" Then
                                DesktopMessageBoxControl.DesktopMessageShow("La extensión del archivo no es correcta para este Tipo de Log, por favor seleccione otro archivo de cargue.", "Error de Extensión", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                                Return
                            Else
                                Dim StrSplit As String() = ExtensionBD.Split("|"c)
                                Dim encontradoSplit = StrSplit.Where(Function(x) x.ToString().ToUpper() = Extension.ToUpper()).[Select](Function(x) x.ToString())

                                If encontradoSplit.Count() = 0 Then
                                    DesktopMessageBoxControl.DesktopMessageShow("La extensión del archivo no es correcta para este Tipo de Log, por favor seleccione otro archivo de cargue.", "Error de Extensión", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                                    Return
                                End If

                                Me.ExtensionGlobal = Extension
                            End If
                        End If


                        If ValidaFormatoCorrecto_Log(TiposLogrow.id_Tipo_log, NombreArchivo, TiposLogrow) = False Then
                            DesktopMessageBoxControl.DesktopMessageShow("El formato del archivo no es correcto, por favor verifique y cargue nuevamente!!", "Format Archivo de cargue", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                            Return
                        End If

                        ValidarExiste_ArchivoCargue(NombreArchivo, dbmImaging)
                    End If
                Else
                    DesktopMessageBoxControl.DesktopMessageShow("Error al cargar Log, intentelo nuevamente!!", "ERROR", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                    Me._File = Nothing
                    Me.ArchivoDesktopTextBox.Text = ""
                    Return
                End If

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargarButton_Click", ex)
            Finally
                Timer1.Enabled = False
                dbmImaging.Connection_Close()
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

        'Private Sub DesktopComboBoxControlTiposLog_SelectedIndexChanged(sender As Object, e As System.EventArgs)
        '    'Dim x As Object
        '    'x = DesktopComboBoxControlTiposLog.SelectedIndex
        '    'x = DesktopComboBoxControlTiposLog.SelectedValue
        '    TiposLogrow = CType(TiposLogDataTable.Select("id_Tipo_Log = " + DesktopComboBoxControlTiposLog.SelectedValue.ToString())(0), DBImaging.SchemaConfig.TBL_Tipos_LogRow)
        'End Sub

        Private Sub DesktopComboBoxControlTiposLog_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) 'Handles DesktopComboBoxControlTiposLog.SelectedIndexChanged
            Dim x As String
            x = DesktopComboBoxControlTiposLog.SelectedValue.ToString()
            If x <> "-1" Then
                TiposLogrow = CType(TiposLogDataTable.Select("id_Tipo_Log = " + DesktopComboBoxControlTiposLog.SelectedValue.ToString())(0), DBImaging.SchemaConfig.TBL_Tipos_LogRow)
            End If
        End Sub

#End Region

#Region "BackgroundWorker"

        Private Sub CargueBackgroundWorker_DoWork(ByVal sender As System.Object, ByVal e As DoWorkEventArgs) Handles CargueBackgroundWorker.DoWork
            Dim worker As BackgroundWorker = CType(sender, BackgroundWorker)
            If worker.CancellationPending Then e.Cancel = True

            Dim nombreArchivo As String = ""
            Dim entidad = CShort(Program.ImagingGlobal.Entidad)
            Dim esquema = CShort(Program.ImagingGlobal.Proyecto)
            nombreArchivo = e.Argument.ToString()
            trResultado = CargarArchivo(nombreArchivo, entidad, esquema)
            Me.CargueBackgroundWorker.ReportProgress(CargueProgressBar.Maximum)
        End Sub

        Private Sub CargueBackgroundWorker_ProgressChanged(ByVal sender As System.Object, ByVal e As ProgressChangedEventArgs) Handles CargueBackgroundWorker.ProgressChanged
            If e.ProgressPercentage = 0 Then
                CargandoPictureBox.Visible = True

                CargueProgressBar.Minimum = 0
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
                CargueProgressBar.Value = e.ProgressPercentage
                ProcesadosLabel.Text = e.ProgressPercentage.ToString()
            End If
        End Sub

        Private Sub CargueBackgroundWorker_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As RunWorkerCompletedEventArgs) Handles CargueBackgroundWorker.RunWorkerCompleted
            CargandoPictureBox.Visible = False
            Timer1.Enabled = False

            ArchivoDesktopTextBox.Text = ""
            _File = Nothing

            If trResultado.Result Then
                If Not IsNothing(trResultado.OT) Then
                    If Not trResultado.OT.IsNull Then
                        DesktopMessageBoxControl.DesktopMessageShow("Se generó el cargue con OT: " & trResultado.OT.ToString(), "Cargue Parcial [OT: " & trResultado.OT.ToString() & "]", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                    End If
                End If

                DatosCargadosDesktopDataGridView.DataSource = _DataFile
                CargueProgressBar.Value = CargueProgressBar.Maximum
                DesktopMessageBoxControl.DesktopMessageShow("Datos cargados éxitosamente", "Cargue de datos", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
            Else
                If Not IsNothing(trResultado.Parameters) Then
                    Dim myError As New FormResultadoValidacion(trResultado)
                    CargueBackgroundWorker.Dispose()
                    myError.ShowDialog()
                End If
            End If

            _File = Nothing
        End Sub

#End Region

    End Class

End Namespace

