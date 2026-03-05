Imports System.IO
Imports System.Text
Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Config
Imports SLYG.Tools

Namespace Forms.Solicitudes

    Public Class FormCargueSolicitudes
        Inherits FormBase

#Region " Enumeraciones "

        Enum Columnas As Integer
            Usuario_Solicitud = 0
            id_Entidad = 1
            id_Proyecto = 2
            id_Esquema = 3
            Campo1 = 4
            Tipo1 = 5
            Campo1Valor = 6
            Campo2 = 7
            Tipo2 = 8
            Campo2Valor = 9
            Campo3 = 10
            Tipo3 = 11
            Campo3Valor = 12
            Motivo = 13
            Prioridad = 14
            TipoSolicitud = 15
            Documentos = 16

            UsuarioRegistrado = 17
            Identificacion = 18
            Apellidos = 19
            Nombres = 20
            Entidad = 21
            Direccion = 22
            Departamento = 23
            Ciudad = 24
        End Enum

#End Region

#Region " Declaraciones "

        'Almacena el archivo de cargue
        Private _File As Stream = Nothing
        Private objCSV As New Slyg.Tools.CSV.CSVData
        Private objXLS As New XLSData
        'Private DocumentosSolicitados As DataTable
        Dim ResumenTable As DataTable 'Miharu_Core.Schemadbo.CTA_Busqueda_Data_SolicitudesDataTable

        Private Const colMin As Integer = 18
        Private Const colMax As Integer = 25

        Private _Separador As String = ","
        Private _NumRegistros As Integer = 0
        Dim fs As FileStream
#End Region

#Region " Metodos "

        Private Sub BuscarArchivo()
            Try
                Dim Respuesta As DialogResult
                Respuesta = ArchivoOpenFileDialog.ShowDialog()

                If Respuesta = DialogResult.OK Then
                    Try
                        ArchivoDesktopTextBox.Text = ArchivoOpenFileDialog.FileName

                        'Si el archivo es txt o csv, habilita la opción de manejar separador
                        If ArchivoOpenFileDialog.FileName.EndsWith(".txt") OrElse ArchivoOpenFileDialog.FileName.EndsWith(".csv") Then
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

        'Private Sub HabilitarControles(ByVal valor As Boolean)
        '    ArchivoDesktopTextBox.Enabled = valor
        '    BuscarArchivoButton.Enabled = valor
        '    CargarButton.Enabled = valor
        'End Sub

        Private Sub ExportarResultado()
            Try
                If ComaRadioButton.Checked Then _Separador = CChar(",")
                If TabuladorRadioButton.Checked Then _Separador = ControlChars.Tab
                If PuntoComaRadioButton.Checked Then _Separador = CChar(";")

                ArchivoSaveFileDialog.Filter = "Archivo Plano (*.csv)|*.csv"
                ArchivoSaveFileDialog.FileName = "Resultado Solicitud [" & Utilities.LimpiarCadena(Now.ToString()) & "].csv"
                ArchivoSaveFileDialog.Title = "Guardar Resultado solicitud"

                Dim resultadoLocal = ArchivoSaveFileDialog.ShowDialog()
                If resultadoLocal = DialogResult.OK Then
                    fs = CType(ArchivoSaveFileDialog.OpenFile(), FileStream)
                    Me.ExportarBackgroundWorker.RunWorkerAsync()
                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("ExportarResultado", ex)
            End Try
        End Sub

        Private Sub EscribeArchivo(ByRef archivo As FileStream)
            Try
                Dim s As New StreamWriter(archivo, System.Text.Encoding.UTF8)
                s.BaseStream.Seek(0, SeekOrigin.End)

                'Se obtien el data source de resultados
                _NumRegistros = DatosCargadosDesktopDataGridView.RowCount

                Dim iResultado As Integer = 0
                Me.ExportarBackgroundWorker.ReportProgress(iResultado)

                'Encabezado
                For Each ColumnaTitulo As DataGridViewColumn In DatosCargadosDesktopDataGridView.Columns
                    s.Write(ColumnaTitulo.Name & _Separador)
                Next
                s.Write(vbCrLf)

                ''Registros
                For Each rowEsquema As DataGridViewRow In DatosCargadosDesktopDataGridView.Rows
                    iResultado += 1
                    For Each Columna As DataGridViewCell In rowEsquema.Cells
                        s.Write(Columna.Value.ToString() & _Separador)
                    Next
                    s.Write(vbCrLf)
                    Me.ExportarBackgroundWorker.ReportProgress(iResultado)
                Next

                s.Close()
                archivo.Close()
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("EscribeArchivo", ex)

            End Try
        End Sub
#End Region

#Region " Funciones "

        Private Sub CargarArchivo()
            Dim _DataFile As DataTable
            Dim _Validacion As DataTable
            Dim _Cargue As DataTable

            'Se valida el tipo de archivo, si es CSV,TXT o XLS
            If OpcionesSeparadorGroupBox.Enabled Then 'Es plano CSV o TXT
                'Se obtiene el separador
                If ComaRadioButton.Checked Then objCSV.Separator = CChar(",")
                If TabuladorRadioButton.Checked Then objCSV.Separator = ControlChars.Tab.ToCharArray()(0)
                If PuntoComaRadioButton.Checked Then objCSV.Separator = CChar(";")

                'Se realiza el cargue del archivo en un datatable para luego validarlo.
                objCSV.LoadCSV(CType(_File, FileStream).Name, chkEncabezado.Checked)
                _DataFile = objCSV.DataTable.ToDataTable()
            Else 'Es XLS
                Dim dsHojas As DataSet = objXLS.ImportExcelXLS(CType(_File, FileStream).Name, chkEncabezado.Checked)
                _DataFile = dsHojas.Tables(0)
            End If
            
            Dim dmArchiving As New DBArchiving.DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)

            Try
                dmArchiving.Connection_Open(Program.Sesion.Usuario.id)
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                BulkInsert.InsertDataTable(_DataFile, dmArchiving, "#Validacion")

                'ValidaCargue
                _Validacion = dmArchiving.Schemadbo.PA_Validar_Cargue_Solicitudes_Masivas.DBExecute()
                Dim cadena As New StringBuilder
                For Each row As DataRow In _Validacion.Rows
                    cadena.AppendLine(row(0).ToString())
                Next

                If cadena.ToString().Length > 0 Then
                    Dim Errores As New FormResultadoValidacion(cadena)
                    Errores.ShowDialog()
                    Exit Sub
                End If

                'Insertar Solicitudes
                _Cargue = dmArchiving.Schemadbo.PA_Inserta_Solicitudes_Masivas.DBExecute(Program.Sesion.Usuario.id)

                If _Cargue.Rows.Count > 0 Then
                    DesktopMessageBoxControl.DesktopMessageShow("Se han creado las solicitudes masivas con éxito.", "Solicitudes Creadas", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                    DatosCargadosDesktopDataGridView.AutoGenerateColumns = False
                    If DatosCargadosDesktopDataGridView.Rows.Count > 0 Then
                        DatosCargadosDesktopDataGridView.Rows.Clear()
                    End If
                    DatosCargadosDesktopDataGridView.DataSource = _Cargue
                Else
                    DesktopMessageBoxControl.DesktopMessageShow("No se logro procesar ninguna solicitud. Por favor verifique la información del archivo de cargue.", "Solicitudes No creadas", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                End If

                'If ValidaCargue(_DataFile) Then
                '    Dim countDocumentos As Integer = 0

                '    For Each RowData As DataRow In _DataFile.Rows
                '        dmArchiving.Transaction_Begin()
                '        dbmCore.Transaction_Begin()

                '        'SOLICITUD
                '        Dim Solicitud As New DBArchiving.SchemaCustody.TBL_SolicitudType
                '        Solicitud.Bloqueada = False
                '        Solicitud.Fecha_Solicitud = SlygNullable.SysDate

                '        'USUARIO LOG
                '        Dim Usuario = dmArchiving.SchemaSecurity.CTA_Usuario.DBFindByIdentificacion_Usuario(RowData(Columnas.Usuario_Solicitud).ToString())
                '        Solicitud.fk_Usuario = Usuario(0).id_Usuario

                '        'USUARIO DESTINO
                '        If CStr(RowData(Columnas.UsuarioRegistrado)) = "0" Then
                '            Solicitud.fk_Solicitante = InsertaUsuario(dmArchiving, RowData)
                '            Solicitud.fk_Usuario_Destino = DBNull.Value
                '        Else
                '            Solicitud.fk_Usuario_Destino = InsertaUsuario(dmArchiving, RowData)
                '            Solicitud.fk_Solicitante = DBNull.Value
                '        End If

                '        Solicitud.id_Solicitud = dmArchiving.SchemaCustody.TBL_Solicitud.DBNextId()

                '        dmArchiving.SchemaCustody.TBL_Solicitud.DBInsert(Solicitud)

                '        'Documentos
                '        Dim Documentos = getConsultaWhere(dmArchiving, RowData)
                '        Dim Consecutivo As Short = 1

                '        For Each RowDocumento As DataRow In Documentos.Rows

                '            'Crea el item solicitud
                '            Dim SolicitudItem As New DBArchiving.SchemaCustody.TBL_Solicitud_ItemType
                '            SolicitudItem.Activa_Solicitud = True
                '            SolicitudItem.Cantidad_Folios = CShort(0)
                '            SolicitudItem.Fecha_Accion = SlygNullable.SysDate
                '            SolicitudItem.fk_Estado = DBCore.EstadoEnum.Bandeja_salida_boveda
                '            SolicitudItem.fk_Solicitud_Motivo = CShort(RowData(Columnas.Motivo))
                '            SolicitudItem.fk_Solicitud_Prioridad = CByte(RowData(Columnas.Prioridad))
                '            SolicitudItem.fk_Solicitud_Tipo = CByte(RowData(Columnas.TipoSolicitud))
                '            SolicitudItem.fk_Solicitud = Solicitud.id_Solicitud
                '            SolicitudItem.fk_Expediente = CLng(RowDocumento("fk_Expediente"))
                '            SolicitudItem.fk_File = CShort(RowDocumento("id_File"))
                '            SolicitudItem.fk_Folder = CShort(RowDocumento("fk_Folder"))
                '            SolicitudItem.id_Item_Solicitud = Consecutivo
                '            dmArchiving.SchemaCustody.TBL_Solicitud_Item.DBInsert(SolicitudItem)

                '            'Actualiza el estado del file
                '            Dim File As New DBCore.SchemaProcess.TBL_File_EstadoType
                '            File.fk_Estado = DBCore.EstadoEnum.Bandeja_salida_boveda
                '            dbmCore.SchemaProcess.TBL_File_Estado.DBUpdate(File, CLng(RowDocumento("fk_Expediente")), CShort(RowDocumento("fk_Folder")), CShort(RowDocumento("id_File")), DesktopConfig.Modulo.Archiving)

                '            Consecutivo = CShort(Consecutivo + 1)
                '            countDocumentos += 1
                '        Next

                '        dbmCore.Transaction_Commit()
                '        dmArchiving.Transaction_Commit()
                '    Next

                '    If countDocumentos > 0 Then
                '        DesktopMessageBoxControl.DesktopMessageShow("Se han creado las solicitudes masivas con éxito.", "Solicitudes Creadas", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                '        DatosCargadosDesktopDataGridView.AutoGenerateColumns = False
                '        If DatosCargadosDesktopDataGridView.Rows.Count > 0 Then
                '            DatosCargadosDesktopDataGridView.Rows.Clear()
                '        End If
                '        DatosCargadosDesktopDataGridView.DataSource = ResumenTable
                '    Else
                '        DesktopMessageBoxControl.DesktopMessageShow("No se logro procesar ninguna solicitud. Por favor verifique la información del archivo de cargue.", "Solicitudes No creadas", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                '    End If
                'End If
            Catch ex As Exception
                dbmCore.Transaction_Rollback()
                dmArchiving.Transaction_Rollback()
                DesktopMessageBoxControl.DesktopMessageShow("CargarArchivo", ex)
            Finally
                dmArchiving.Connection_Close()
                dbmCore.Connection_Close()
            End Try
        End Sub

        Public Function InsertaUsuario(ByVal dmArchiving As DBArchiving.DBArchivingDataBaseManager, ByVal RowData As DataRow) As Integer
            Dim Id_Usuario As Integer

            'Si no es un usuario registrado
            If CStr(RowData(Columnas.UsuarioRegistrado)) = "0" Then
                Dim Solicitantes As New DBArchiving.SchemaCustody.TBL_Solicitud_SolicitanteType
                Solicitantes.Apellidos_Solicitante = RowData(Columnas.Apellidos).ToString()
                Solicitantes.Ciudad_Solicitante = RowData(Columnas.Ciudad).ToString()
                Solicitantes.Departamento_solicitante = RowData(Columnas.Departamento).ToString()
                Solicitantes.Direccion_Solicitante = RowData(Columnas.Direccion).ToString()
                Solicitantes.Identificacion_Solicitante = RowData(Columnas.Identificacion).ToString()
                Solicitantes.Nombre_Entidad_Solicitante = RowData(Columnas.Entidad).ToString()
                Solicitantes.Nombres_Solicitante = RowData(Columnas.Nombres).ToString()
                Solicitantes.id_Solicitante = dmArchiving.SchemaCustody.TBL_Solicitud_Solicitante.DBNextId()
                Id_Usuario = Solicitantes.id_Solicitante
                dmArchiving.SchemaCustody.TBL_Solicitud_Solicitante.DBInsert(Solicitantes)
            Else
                Dim Usuario = dmArchiving.SchemaSecurity.CTA_Usuario.DBFindByIdentificacion_Usuario(RowData(Columnas.Identificacion).ToString())
                Id_Usuario = Usuario(0).id_Usuario
            End If

            Return Id_Usuario
        End Function

        'Public Function getConsultaWhere(ByVal dmArchiving As DBArchiving.DBArchivingDataBaseManager, ByVal RowData As DataRow) As DataTable
        '    Dim SQL As New StringBuilder()

        '    SQL.Append(" AND fk_Entidad = " & RowData(Columnas.id_Entidad).ToString())

        '    If Not IsDBNull(RowData(Columnas.id_Proyecto)) Then
        '        SQL.Append(" AND fk_Proyecto = " & RowData(Columnas.id_Proyecto).ToString())
        '    End If

        '    If Not IsDBNull(RowData(Columnas.id_Esquema)) Then
        '        SQL.Append(" AND fk_Esquema = " & RowData(Columnas.id_Esquema).ToString())
        '    End If

        '    SQL.Append(vbCrLf & " AND (CHARINDEX ('<fk_Campo_Tipo>" & RowData(Columnas.Tipo1).ToString() & "</fk_Campo_Tipo>" _
        '               & "<fk_Campo_Busqueda>" & RowData(Columnas.Campo1).ToString() & "</fk_Campo_Busqueda>" _
        '               & "<Valor_File_Data>" & RowData(Columnas.Campo1Valor).ToString() & "</Valor_File_Data>',CampoBusqueda) > 0) ")

        '    If Not IsDBNull(RowData(Columnas.Campo2)) Then
        '        SQL.Append(vbCrLf & " AND (CHARINDEX ('<fk_Campo_Tipo>" & RowData(Columnas.Tipo2).ToString() & "</fk_Campo_Tipo>" _
        '                   & "<fk_Campo_Busqueda>" & RowData(Columnas.Campo2).ToString() & "</fk_Campo_Busqueda>" _
        '                   & "<Valor_File_Data>" & RowData(Columnas.Campo2Valor).ToString() & "</Valor_File_Data>',CampoBusqueda) > 0) ")
        '    End If

        '    If Not IsDBNull(RowData(Columnas.Campo3)) Then
        '        SQL.Append(vbCrLf & " AND (CHARINDEX ('<fk_Campo_Tipo>" & RowData(Columnas.Tipo3).ToString() & "</fk_Campo_Tipo>" _
        '                   & "<fk_Campo_Busqueda>" & RowData(Columnas.Campo3).ToString() & "</fk_Campo_Busqueda>" _
        '                   & "<Valor_File_Data>" & RowData(Columnas.Campo3Valor).ToString() & "</Valor_File_Data>',CampoBusqueda) > 0) ")
        '    End If



        '    Dim Table = dmArchiving.Schemadbo.PA_Busqueda_Data_Solicitudes_Masivas.DBExecute(RowData(Columnas.Documentos).ToString(), SQL.ToString(), CShort(RowData(Columnas.Motivo)), CByte(RowData(Columnas.Prioridad)), CByte(RowData(Columnas.TipoSolicitud)))
        '    Dim view As New DataView(Table)
        '    view.RowFilter = "fk_Estado = " & DBCore.EstadoEnum.Custodia
        '    dmArchiving.Schemadbo.PA_Busqueda_Data_Solicitudes_Masivas.DBExecute(ResumenTable, RowData(Columnas.Documentos).ToString(), SQL.ToString(), CShort(RowData(Columnas.Motivo)), CByte(RowData(Columnas.Prioridad)), CByte(RowData(Columnas.TipoSolicitud)))

        '    'For Each Row As DataRow In Table.Rows
        '    '    Dim RowDocumento = DocumentosSolicitados.NewRow
        '    '    RowDocumento("Codigo File") = Row("CBarras_File").ToString()
        '    '    DocumentosSolicitados.Rows.Add(RowDocumento)
        '    'Next

        '    Return Table
        'End Function

        Public Function ValidaCargue(ByVal Data As DataTable) As Boolean
            Dim Validacion As Boolean = True

            Dim CadenaError As New StringBuilder

            Dim dmArchiving As New DBArchiving.DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
            dmArchiving.Connection_Open(Program.Sesion.Usuario.id)
            dbmCore.Connection_Open(Program.Sesion.Usuario.id)


            Dim entidadLocal = dmArchiving.SchemaSecurity.CTA_Entidad.DBGet()
            Dim proyectoLocal = dmArchiving.SchemaConfig.TBL_Proyecto.DBGet(Nothing, Nothing)
            Dim esquemaLocal = dmArchiving.SchemaConfig.TBL_Esquema.DBGet(Nothing, Nothing, Nothing)
            Dim CampoBusqueda = dbmCore.SchemaConfig.TBL_Campo_Busqueda_Entidad.DBGet(Nothing, Nothing, Nothing)

            dbmCore.Connection_Close()
            dmArchiving.Connection_Close()

            'Valida numero de columnas minimas
            If Data.Columns.Count < colMin + 1 Or Data.Columns.Count > colMax Then
                Validacion = False
                CadenaError.AppendLine("El número de columnas mínimas del archivo de cargue es " & colMin.ToString() & " y las maximas " & colMax.ToString() & ".")
            Else
                If Data.Rows.Count = 0 Then
                    Validacion = False
                    CadenaError.AppendLine("El archivo cargado no contiene ningún registro.")
                End If

                'VALIDACION POR CADA REGISTRO
                Dim Count As Integer = 1
                For Each RowData As DataRow In Data.Rows

                    Dim id_Entidad As String = ""
                    Dim id_Proyecto As String = ""
                    Dim id_Esquema As String = ""
                    Dim UsuarioPeticion As String = ""

                    'valida  -  USUARIO PETICION  -
                    ValidaUsuario(Count, dmArchiving, RowData(Columnas.Usuario_Solicitud), CadenaError, UsuarioPeticion)

                    'Valida  -  ENTIDAD  -  
                    ValidaEntidad(Count, entidadLocal, RowData(Columnas.id_Entidad), CadenaError, id_Entidad)

                    'Valida  -  PROYECTO  -
                    ValidaProyecto(Count, proyectoLocal, id_Entidad, RowData(Columnas.id_Proyecto), CadenaError, id_Proyecto)

                    'valida  -  ESQUEMA  -
                    ValidaEsquema(Count, esquemaLocal, id_Entidad, id_Proyecto, RowData(Columnas.id_Esquema), CadenaError, id_Esquema)

                    'valida  -  CAMPO1  -  VALOR CAMPO1  -
                    ValidaCamposBusqueda(Count, CampoBusqueda, id_Entidad, RowData(Columnas.Campo1), RowData(Columnas.Tipo1), RowData(Columnas.Campo1Valor), CadenaError, True, "Campo busqueda 1")

                    'valida  -  CAMPO2  -  VALOR CAMPO2  -
                    ValidaCamposBusqueda(Count, CampoBusqueda, id_Entidad, RowData(Columnas.Campo2), RowData(Columnas.Tipo2), RowData(Columnas.Campo2Valor), CadenaError, False, "Campo busqueda 2")

                    'valida  -  CAMPO3  -  VALOR CAMPO3  -
                    ValidaCamposBusqueda(Count, CampoBusqueda, id_Entidad, RowData(Columnas.Campo3), RowData(Columnas.Tipo3), RowData(Columnas.Campo3Valor), CadenaError, False, "Campo busqueda 3")

                    'valida  -  MOTIVO SOLICITUD  -
                    ValidaMotivoSolicitud(Count, dmArchiving, UsuarioPeticion, RowData(Columnas.Motivo), CadenaError)

                    'valida  -  PRIORIDAD SOLICITUD  -
                    ValidaPrioridadSolicitud(Count, dmArchiving, UsuarioPeticion, RowData(Columnas.Prioridad), CadenaError)

                    'valida  -  USUARIO REGISTRADO  -
                    ValidaEsUsuarioRegistrado(Count, dmArchiving, RowData(Columnas.UsuarioRegistrado), CadenaError, RowData(Columnas.Identificacion), RowData)

                    'valida  -  TIPO SOLICITUD  -  DOCUMENTOS
                    ValidaTipoSolicitud(Count, dmArchiving, UsuarioPeticion, RowData(Columnas.TipoSolicitud), RowData(Columnas.Documentos), CadenaError)

                    Count += 1
                Next
            End If

            If CadenaError.ToString().Length > 0 Then
                Validacion = False
                Dim Errores As New FormResultadoValidacion(CadenaError)
                Errores.ShowDialog()
                'DMB.DesktopMessageShow(CadenaError.ToString, "", IconEnum.AdvertencyIcon, True)
            End If

            Return Validacion
        End Function

        Public Sub ValidaUsuario(ByVal Num_Registro As Integer, ByVal dmarchiving As DBArchiving.DBArchivingDataBaseManager, ByVal Usuario_ As Object, ByRef CadenaError As StringBuilder, ByRef id_Usuario As String)
            Try
                Dim UsuarioPeticion As String = CStr(Usuario_)

                dmarchiving.Connection_Open(Program.Sesion.Usuario.id)
                Dim Usuario = dmarchiving.SchemaSecurity.CTA_Usuario.DBFindByIdentificacion_Usuario(UsuarioPeticion.ToString())
                dmarchiving.Connection_Close()

                If Usuario.Count = 0 Then
                    CadenaError.AppendLine("Linea " & Num_Registro & " - El usuario [" & UsuarioPeticion & "] no se encuentra en la base de datos de usuarios.")
                Else
                    id_Usuario = CStr(Usuario(0).id_Usuario)
                End If
            Catch ex As Exception
                If IsDBNull(Usuario_) Then
                    CadenaError.AppendLine("Linea " & Num_Registro & " - El usuario de petición es obligatorio.")
                End If
            End Try
        End Sub

        Public Sub ValidaEntidad(ByVal Num_Registro As Integer, ByVal DataEntidad As DBArchiving.SchemaSecurity.CTA_EntidadDataTable, ByRef id_Entidad As Object, ByRef CadenaError As StringBuilder, ByRef id_Entidad_ As String)
            If Not IsDBNull(id_Entidad) Then
                Try
                    If Not id_Entidad.ToString = "" Then
                        id_Entidad_ = CStr(CShort(id_Entidad))
                        Dim view As New DataView(DataEntidad)
                        view.RowFilter = DataEntidad.id_EntidadColumn.ColumnName & "=" & id_Entidad.ToString

                        If view.ToTable(True).Rows.Count = 0 Then
                            CadenaError.AppendLine("Linea " & Num_Registro & " - La entidad [" & id_Entidad.ToString & "] no se encuentra en la base de datos.")
                        End If
                    Else
                        CadenaError.AppendLine("Linea " & Num_Registro & " - La entidad es obligatoria.")
                    End If
                Catch ex As Exception
                    CadenaError.AppendLine("Linea " & Num_Registro & " - La entidad [" & id_Entidad.ToString & "] no es de tipo númerico.")
                End Try
            Else
                CadenaError.AppendLine("Linea " & Num_Registro & " - La entidad es obligatoria.")
            End If

        End Sub

        Public Sub ValidaProyecto(ByVal Num_Registro As Integer, ByVal DataProyecto As DBArchiving.SchemaConfig.TBL_ProyectoDataTable, ByVal id_Entidad As String, ByRef id_Proyecto As Object, ByRef CadenaError As StringBuilder, ByRef id_Proyecto_ As String)
            If id_Entidad <> "" Then
                Try
                    If IsDBNull(id_Proyecto) = False Then
                        If id_Proyecto.ToString <> "" Then
                            id_Proyecto_ = CStr(CShort(id_Proyecto))

                            Dim view As New DataView(DataProyecto)
                            view.RowFilter = DataProyecto.fk_EntidadColumn.ColumnName & "=" & id_Entidad & " AND " & DataProyecto.fk_ProyectoColumn.ColumnName & "=" & id_Proyecto.ToString

                            If view.ToTable(True).Rows.Count = 0 Then
                                CadenaError.AppendLine("Linea " & Num_Registro & " - El proyecto [" & id_Proyecto.ToString & "] no se encuentra en la base de datos asociada a la entidad " & id_Entidad & ".")
                            End If
                        End If
                    End If
                Catch ex As Exception
                    CadenaError.AppendLine("Linea " & Num_Registro & " - El proyecto [" & id_Proyecto.ToString & "] no es de tipo númerico.")
                End Try
            End If
        End Sub

        Public Sub ValidaEsquema(ByVal Num_Registro As Integer, ByVal DataEsquema As DBArchiving.SchemaConfig.TBL_EsquemaDataTable, ByVal id_Entidad As String, ByVal id_Proyecto As String, ByRef id_Esquema As Object, ByRef CadenaError As StringBuilder, ByRef id_Esquema_ As String)
            If id_Entidad <> "" And id_Proyecto <> "" Then
                Try
                    If IsDBNull(id_Esquema) = False Then
                        If id_Esquema.ToString <> "" Then
                            id_Esquema_ = CStr(CShort(id_Esquema))

                            Dim view As New DataView(DataEsquema)
                            view.RowFilter = DataEsquema.fk_EntidadColumn.ColumnName & "=" & id_Entidad & " AND " & DataEsquema.fk_ProyectoColumn.ColumnName & "=" & id_Proyecto & " AND " & DataEsquema.fk_EsquemaColumn.ColumnName & "=" & id_Esquema.ToString

                            If view.ToTable(True).Rows.Count = 0 Then
                                CadenaError.AppendLine("Linea " & Num_Registro & " - El esquema [" & id_Esquema.ToString & "] no se encuentra en la base de datos asociada a la entidad [" & id_Entidad & "] y al proyecto [" & id_Proyecto & "].")
                            End If
                        End If
                    End If

                Catch ex As Exception
                    CadenaError.AppendLine("Linea " & Num_Registro & " - El esquema [" & id_Esquema.ToString & "] no es de tipo númerico.")
                End Try
            End If
        End Sub

        Public Sub ValidaCamposBusqueda(ByVal Num_Registro As Integer, ByVal CampoBusqueda As DBCore.SchemaConfig.TBL_Campo_Busqueda_EntidadDataTable, ByVal id_Entidad As String, ByVal Campo As Object, ByVal nTipo As Object, ByVal Valor As Object, ByRef CadenaError As StringBuilder, ByVal Obligatorio As Boolean, ByVal NombreCampo As String)
            If id_Entidad <> "" Then
                Try
                    If (Not IsDBNull(Campo)) And (Not IsDBNull(nTipo)) Then
                        If Campo.ToString() <> "" And nTipo.ToString() <> "" Then
                            Campo = CStr(CShort(Campo))
                            nTipo = CStr(CShort(nTipo))

                            Dim view As New DataView(CampoBusqueda)
                            view.RowFilter = CampoBusqueda.fk_EntidadColumn.ColumnName & "=" & id_Entidad & " AND " & CampoBusqueda.fk_Campo_BusquedaColumn.ColumnName & "=" & Campo.ToString() & " AND " & CampoBusqueda.fk_Campo_TipoColumn.ColumnName & "=" & nTipo.ToString()

                            If view.ToTable(True).Rows.Count = 0 And Obligatorio = True Then
                                CadenaError.AppendLine("Linea " & Num_Registro & " - El " & NombreCampo & " [" & Campo.ToString() & "] no se encuentra en la base de datos asociada a la entidad [" & id_Entidad & "].")
                            Else
                                Dim TipoDato As Short = CShort(view.ToTable(True).Rows(0)("fk_Campo_Tipo"))

                                If Not IsDBNull(Valor) Then
                                    If Valor.ToString() <> "" Then

                                        If TipoDato = DesktopConfig.CampoTipo.Fecha Then
                                            If Not IsDate(Valor) Then
                                                CadenaError.AppendLine("Linea " & Num_Registro & " - El Valor [" & Valor.ToString() & "] del " & NombreCampo & " [" & Campo.ToString() & "] debe ser de tipo Fecha.")
                                            End If
                                        ElseIf TipoDato = DesktopConfig.CampoTipo.Numerico Then
                                            If Not IsNumeric(Valor) Then
                                                CadenaError.AppendLine("Linea " & Num_Registro & " - El Valor [" & Valor.ToString() & "] del " & NombreCampo & " [" & Campo.ToString() & "] debe ser de tipo númerico.")
                                            End If
                                        ElseIf TipoDato = DesktopConfig.CampoTipo.SiNo Then
                                            If Valor.ToString() <> "1" And Valor.ToString() <> "0" Then
                                                CadenaError.AppendLine("Linea " & Num_Registro & " - El Valor [" & Valor.ToString() & "] del " & NombreCampo & " [" & Campo.ToString() & "] debe ser de tipo booleano  [1-Si],[0-No].")
                                            End If
                                        End If
                                    Else
                                        If Obligatorio Then
                                            CadenaError.AppendLine("Linea " & Num_Registro & " - El Valor del " & NombreCampo & " no puede ser vacio.")
                                        End If
                                    End If
                                Else
                                    If Obligatorio Then
                                        CadenaError.AppendLine("Linea " & Num_Registro & " - El Valor del " & NombreCampo & " no puede ser vacio.")
                                    End If
                                End If
                            End If
                        Else
                            If Obligatorio Then
                                CadenaError.AppendLine("Linea " & Num_Registro & " - El " & NombreCampo & " es obligatorio.")
                            End If
                        End If
                    Else
                        If Obligatorio Then
                            CadenaError.AppendLine("Linea " & Num_Registro & " - El " & NombreCampo & " es obligatorio.")
                        End If
                    End If

                Catch ex As Exception
                    CadenaError.AppendLine("Linea " & Num_Registro & " - El " & NombreCampo & " [" & Campo.ToString() & "] no es de tipo númerico.")
                End Try
            End If

        End Sub

        Public Sub ValidaMotivoSolicitud(ByVal Num_Registro As Integer, ByVal dmarchiving As DBArchiving.DBArchivingDataBaseManager, ByVal Usuario As String, ByVal nMotivo As Object, ByRef CadenaError As StringBuilder)
            If Not IsDBNull(nMotivo) Then
                If Not nMotivo.ToString() = "" Then
                    If Not Usuario = "" Then
                        Try
                            Dim Motivo_ = CShort(nMotivo)
                            dmarchiving.Connection_Open(Program.Sesion.Usuario.id)
                            Dim MotivosUsuario = dmarchiving.Schemadbo.CTA_Solicitud_Motivo_x_Usuario.DBFindByfk_Usuariofk_Solicitud_Motivo(CInt(Usuario), Motivo_)
                            dmarchiving.Connection_Close()

                            If MotivosUsuario.Count = 0 Then
                                CadenaError.AppendLine("Linea " & Num_Registro & " - El Motivo [" & nMotivo.ToString() & "] no existe o el usuario que hace la peticion no tiene privilegios para usar este.")
                            End If
                        Catch ex As Exception
                            CadenaError.AppendLine("Linea " & Num_Registro & " - El Motivo [" & nMotivo.ToString() & "] no es de tipo númerico.")
                        End Try
                    End If
                Else
                    CadenaError.AppendLine("Linea " & Num_Registro & " - El Motivo de la solicitud es obligatorio.")
                End If
            Else
                CadenaError.AppendLine("Linea " & Num_Registro & " - El Motivo de la solicitud es obligatorio.")
            End If
        End Sub

        Public Sub ValidaPrioridadSolicitud(ByVal Num_Registro As Integer, ByVal dmarchiving As DBArchiving.DBArchivingDataBaseManager, ByVal Usuario As String, ByVal nPrioridad As Object, ByRef CadenaError As StringBuilder)
            If Not IsDBNull(nPrioridad) Then
                If Not nPrioridad.ToString() = "" Then
                    If Not Usuario = "" Then
                        Try
                            Dim Prioridad_ = CByte(nPrioridad)
                            dmarchiving.Connection_Open(Program.Sesion.Usuario.id)
                            Dim PrioridadUsuario = dmarchiving.Schemadbo.CTA_Solicitud_Prioridad_x_Usuario.DBFindByfk_Usuariofk_Solicitud_Prioridad(CInt(Usuario), Prioridad_)
                            dmarchiving.Connection_Close()

                            If PrioridadUsuario.Count = 0 Then
                                CadenaError.AppendLine("Linea " & Num_Registro & " - La Prioridad [" & nPrioridad.ToString() & "] no existe o el usuario que hace la peticion no tiene privilegios para usar este.")
                            End If
                        Catch ex As Exception
                            CadenaError.AppendLine("Linea " & Num_Registro & " - La Prioridad [" & nPrioridad.ToString() & "] no es de tipo númerico.")
                        End Try
                    End If
                Else
                    CadenaError.AppendLine("Linea " & Num_Registro & " - La Prioridad de la solicitud es obligatorio.")
                End If
            Else
                CadenaError.AppendLine("Linea " & Num_Registro & " - La Prioridad de la solicitud es obligatorio.")
            End If
        End Sub

        Public Sub ValidaTipoSolicitud(ByVal Num_Registro As Integer, ByVal dmarchiving As DBArchiving.DBArchivingDataBaseManager, ByVal Usuario As String, ByVal TipoSolicitud As Object, ByVal Documentos As Object, ByRef CadenaError As StringBuilder)
            Dim validacionInterna As Boolean = False

            If Usuario <> "" Then

                'valida tipo solicitud NULO
                If Not IsDBNull(TipoSolicitud) Then
                    If TipoSolicitud.ToString <> "" Then
                        validacionInterna = True
                    Else
                        CadenaError.AppendLine("Linea " & Num_Registro & " - El tipo de solicitud es obligatorio.")
                    End If
                Else
                    CadenaError.AppendLine("Linea " & Num_Registro & " - El tipo de solicitud es obligatorio.")
                End If

                'valida documentos NULOS
                If Not IsDBNull(Documentos) Then
                    If Documentos.ToString <> "" Then
                        If validacionInterna Then validacionInterna = True
                    Else
                        CadenaError.AppendLine("Linea " & Num_Registro & " - La columna de documentos es obligatoria.")
                    End If
                Else
                    CadenaError.AppendLine("Linea " & Num_Registro & " - La columna de documentos es obligatoria.")
                End If


                If validacionInterna Then
                    Try
                        Dim SolicitudTipo = CByte(TipoSolicitud)

                        If IsNumeric(Documentos.ToString().Replace(",", "")) Then
                            Dim ListaDocumentos = Split(Documentos.ToString(), ",")

                            dmarchiving.Connection_Open(Program.Sesion.Usuario.id)
                            For Each Doc As String In ListaDocumentos
                                Dim tipoLocal = dmarchiving.Schemadbo.CTA_Solicitud_Tipo_x_Usuario.DBFindByfk_Usuariofk_Solicitud_Tipofk_Documento(CInt(Usuario), SolicitudTipo, CInt(Doc))

                                If tipoLocal.Count = 0 Then
                                    CadenaError.AppendLine("Linea " & Num_Registro & " - Es posible que el documento [" & Doc & "] no exista o que el usuario no tenga permisos el tipo de solicitud [" & SolicitudTipo & "] sobre el documento.")
                                End If
                            Next
                            dmarchiving.Connection_Close()
                        Else
                            CadenaError.AppendLine("Linea " & Num_Registro & " - Hay un problema con los documentos.")
                        End If
                    Catch ex As Exception
                        CadenaError.AppendLine("Linea " & Num_Registro & " - El tipo de solicitud no es numerico.")
                    End Try
                End If

            End If
        End Sub

        Public Sub ValidaEsUsuarioRegistrado(ByVal Num_Registro As Integer, ByVal dmarchiving As DBArchiving.DBArchivingDataBaseManager, ByVal EsUsuarioRegistrado As Object, ByRef CadenaError As StringBuilder, ByVal Cedula As Object, ByVal RowData As DataRow)
            If Not IsDBNull(EsUsuarioRegistrado) Then
                If Not EsUsuarioRegistrado.ToString() = "" Then
                    If Not EsUsuarioRegistrado.ToString = "0" And Not EsUsuarioRegistrado.ToString = "1" Then
                        CadenaError.AppendLine("Linea " & Num_Registro & " - La columna de Es usuario registrado no es de tipo booleano [1-Si],[0-No].")
                    Else
                        'Si el usuario es un tercero no registrado
                        If EsUsuarioRegistrado.ToString = "0" Then

                            If RowData.Table.Columns.Count < 25 Then
                                CadenaError.AppendLine("Linea " & Num_Registro & " - El usuario no registrado no puede llevar nulos las columnas [Cedula - Apellidos - Nombres - Nombre Entidad - Direccion - Deptartamento - Ciudad].")
                            Else
                                If IsDBNull(Cedula) _
                                   Or IsDBNull(RowData(Columnas.Apellidos)) _
                                   Or IsDBNull(RowData(Columnas.Nombres)) _
                                   Or IsDBNull(RowData(Columnas.Entidad)) _
                                   Or IsDBNull(RowData(Columnas.Direccion)) _
                                   Or IsDBNull(RowData(Columnas.Departamento)) _
                                   Or IsDBNull(RowData(Columnas.Ciudad)) Then

                                    CadenaError.AppendLine("Linea " & Num_Registro & " - El usuario no registrado no puede llevar nulos las columnas [Cedula - Apellidos - Nombres - Nombre Entidad - Direccion - Deptartamento - Ciudad].")
                                End If
                            End If
                        Else 'si el usuario es registrado
                            If Not IsDBNull(Cedula) Then
                                If Cedula.ToString <> "" Then
                                    dmarchiving.Connection_Open(Program.Sesion.Usuario.id)
                                    Dim Usuario = dmarchiving.SchemaSecurity.CTA_Usuario.DBFindByIdentificacion_Usuario(Cedula.ToString())
                                    dmarchiving.Connection_Close()

                                    If Usuario.Count = 0 Then
                                        CadenaError.AppendLine("Linea " & Num_Registro & " - El usuario [" & Cedula.ToString() & "] no se encuentra en la base de datos de usuarios.")
                                    End If

                                Else
                                    CadenaError.AppendLine("Linea " & Num_Registro & " - El usuario destinatario es obligatorio.")
                                End If
                            Else
                                CadenaError.AppendLine("Linea " & Num_Registro & " - El usuario de destinatario es obligatorio.")
                            End If

                        End If

                    End If
                Else
                    CadenaError.AppendLine("Linea " & Num_Registro & " - La columna de Es usuario registrado es obligatoria.")
                End If
            Else
                CadenaError.AppendLine("Linea " & Num_Registro & " - La columna de Es usuario registrado es obligatoria.")
            End If
        End Sub

#End Region

#Region " Eventos "

        Private Sub FormCargueSolicitudes_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            'DocumentosSolicitados = New DataTable
            'DocumentosSolicitados.Columns.Add("Codigo File", GetType(String))

            ResumenTable = New DataTable 'DB_Miharu_Core.Schemadbo.CTA_Busqueda_Data_SolicitudesDataTable
        End Sub

        Private Sub BuscarArchivoButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BuscarArchivoButton.Click
            BuscarArchivo()
        End Sub

        Private Sub ArchivoDesktopTextBox_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ArchivoDesktopTextBox.Click
            BuscarArchivo()
        End Sub

        Private Sub CargarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CargarButton.Click
            If File.Exists(ArchivoDesktopTextBox.Text) Then
                'Dim NombreArchivo As String = Path.GetFileName(CType(_File, FileStream).Name)
                CargarArchivo()
            Else
                DesktopMessageBoxControl.DesktopMessageShow("El archivo seleccionado no existe, por favor seleccione otro.", "Eror Cargando archivo", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
            End If
        End Sub

        Private Sub ExportarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ExportarButton.Click
            ExportarResultado()
        End Sub
#End Region

#Region " BackgroundWorker "

        Private Sub ExportarBackgroundWorker_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles ExportarBackgroundWorker.DoWork
            EscribeArchivo(fs)
        End Sub

        Private Sub ExportarBackgroundWorker_ProgressChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles ExportarBackgroundWorker.ProgressChanged
            If e.ProgressPercentage = 0 Then
                Me.CargueProgressBar.Maximum = _NumRegistros
            End If
            Me.CargueProgressBar.Value = e.ProgressPercentage
        End Sub

        Private Sub ExportarBackgroundWorker_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles ExportarBackgroundWorker.RunWorkerCompleted
            DesktopMessageBoxControl.DesktopMessageShow("Archivo generado correctamente en " & ArchivoSaveFileDialog.FileName, "Generación archivo", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
            Try
                Process.Start("notepad.exe", ArchivoSaveFileDialog.FileName)
            Catch : End Try
        End Sub
#End Region

    End Class

End Namespace