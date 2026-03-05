Imports System.Text
Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports DBArchiving
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Config
Imports Slyg.Tools

Namespace Forms.Destape

    Public Class FormDocumentos
        Inherits FormBase

#Region " Declaraciones "

        'Private _Folder As DesktopConfig.Folder
        Private _TipoFile As DesktopConfig.TipoFileCargue
        Private _RegistroTipo As DesktopConfig.RegistroTipo
        Private _TableDocumentosxCarpeta As Schemadbo.CTA_Carpeta_documentosDataTable
        Private _idLineaProceso As SlygNullable(Of Integer) = Nothing

        Private _nfk_Expediente As Long
        Private _nfk_Folder As Short
        Private _nfk_OT As Integer
        Private _nfk_Esquema As Short
        Private _nCBarras_Folder As String
        Private PermiteDevolucion As DesktopConfig.Devolucion
#End Region

#Region " Constructor "

        Sub New(ByVal nfk_Expediente As Long, ByVal nfk_Folder As Short, ByVal nfk_OT As Integer, ByVal nfk_Esquema As Short, ByVal nCBarras_Folder As String, ByVal TipoFile As DesktopConfig.TipoFileCargue, ByVal RegistroTipo As DesktopConfig.RegistroTipo)
            ' This call is required by the designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            cbarrasDesktopCBarrasControl.Init(Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, Program.DesktopGlobal.ConnectionStrings.Archiving)

            _nfk_Expediente = nfk_Expediente
            _nfk_Folder = nfk_Folder
            _nfk_OT = nfk_OT
            _nfk_Esquema = nfk_Esquema
            _nCBarras_Folder = nCBarras_Folder

            _TipoFile = TipoFile
            _RegistroTipo = RegistroTipo
        End Sub

#End Region

#Region " Metodos "

        Public Sub DocumentosFolder()
            Dim dbmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)
            DocumentosDesktopDataGridView.DataSource = Nothing
            DocumentosDesktopDataGridView.Refresh()

            _TableDocumentosxCarpeta = dbmArchiving.Schemadbo.CTA_Carpeta_documentos.DBFindByfk_Folderfk_OTfk_expedientefk_entidadfk_proyecto(_nfk_Folder, Nothing, _nfk_Expediente, Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto)
            DocumentosDesktopDataGridView.AutoGenerateColumns = False
            DocumentosDesktopDataGridView.DataSource = _TableDocumentosxCarpeta
            DocumentosDesktopDataGridView.Refresh()
            dbmArchiving.Connection_Close()
        End Sub

        Public Sub CargaCombos()
            Dim viewTipologias As New DataView(Program.RiskGlobal.Tipologias)
            viewTipologias.RowFilter = Program.RiskGlobal.Tipologias.fk_esquemaColumn.ColumnName & "=" & _nfk_Esquema
            viewTipologias.Sort = Program.RiskGlobal.Tipologias.id_tipologiaColumn.ColumnName
            Utilities.LlenarCombo(TipologiaComboBox, viewTipologias.ToTable(), Program.RiskGlobal.Tipologias.id_documentoColumn, Program.RiskGlobal.Tipologias.nombre_tipologiaColumn)
        End Sub

        Public Sub Procesar_Documento(Optional ByVal CBarrasFile As String = "")
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            Dim dbmArchiving As DBArchivingDataBaseManager = Nothing

            'VALIDA SI ES UNA DEVOLUCION
            Dim EsDevolucion = CBarrasFile <> ""
            Dim _nRegistroTipo = _RegistroTipo
            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                dbmArchiving = New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)

                dbmCore.Connection_Open(Program.Sesion.Usuario.id)
                dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

                'CARGA DOCUMENTOS DISPONIBLES PARA PROCESAR
                Dim Faltante As Boolean = False
                Dim ExisteCargueLog As Boolean = False
                Dim DocumentosDisponibles As DesktopConfig.FilesEstadoCargue
                If Not EsDevolucion Then
                    If _TipoFile = DesktopConfig.TipoFileCargue.SinCargue Then
                        DocumentosDisponibles = Utilities.CargueFilesDisponibles(dbmArchiving, Program.RiskGlobal, _nfk_Esquema, _nfk_Folder, _nfk_Expediente, CShort(TipologiaComboBox.SelectedValue), Nothing, _nfk_OT)
                    Else
                        DocumentosDisponibles = Utilities.CargueFilesDisponibles(dbmArchiving, Program.RiskGlobal, _nfk_Esquema, _nfk_Folder, _nfk_Expediente, CShort(TipologiaComboBox.SelectedValue), DBCore.EstadoEnum.Cargado, _nfk_OT)
                        If DocumentosDisponibles.FilesDisponibles = False Then
                            DocumentosDisponibles = Utilities.CargueFilesDisponibles(dbmArchiving, Program.RiskGlobal, _nfk_Esquema, _nfk_Folder, _nfk_Expediente, CShort(TipologiaComboBox.SelectedValue), DBCore.EstadoEnum.Faltante, _nfk_OT)
                            Faltante = True
                        End If
                    End If
                    If DocumentosDisponibles.FilesDisponibles = False And Not Program.RiskGlobal.Usa_Validacion_Destape Then
                        ExisteCargueLog = dbmArchiving.SchemaProcess.PA_Validacion_Log_Sobrantes.DBExecute(Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, _nfk_Esquema, Program.RiskGlobal.LLaves, CShort(TipologiaComboBox.SelectedValue))
                    End If
                Else
                    _nRegistroTipo = DesktopConfig.RegistroTipo.Devolucion
                    DocumentosDisponibles = Utilities.CargueFilesDisponibles(dbmArchiving, Program.RiskGlobal, CBarrasFile)
                    If DocumentosDisponibles.FilesDisponibles = False And Not Program.RiskGlobal.Usa_Validacion_Destape Then
                        ExisteCargueLog = dbmArchiving.SchemaProcess.PA_Validacion_Log_Sobrantes_Devolucion.DBExecute(Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, _nfk_Esquema, Program.RiskGlobal.LLaves, CBarrasFile)
                    End If
                End If

                'SI HAY DOCUMENTOS DISPONIBLES PARA PROCESAR
                If DocumentosDisponibles.FilesDisponibles = True Then
                    Try
                        If Not Faltante Then
                            Utilities.ActualizaEstadoFile(dbmArchiving, dbmCore, DocumentosDisponibles.Cbarras, DocumentosDisponibles.Ot, Program.RiskGlobal.Precinto, DesktopConfig.Modulo.Archiving, DBCore.EstadoEnum.Destapado, Program.Sesion.Usuario.id, Program.RiskGlobal.CajaProceso, Nothing, Nothing, _idLineaProceso, _nRegistroTipo.ToString, _nfk_OT, Program.RiskGlobal.Usa_Tabla_Fisico, Faltante)
                        Else
                            Utilities.ActualizaEstadoFile(dbmArchiving, dbmCore, DocumentosDisponibles.Cbarras, DocumentosDisponibles.Ot, Program.RiskGlobal.Precinto, DesktopConfig.Modulo.Archiving, DBCore.EstadoEnum.Destapado, Program.Sesion.Usuario.id, Program.RiskGlobal.CajaProceso, _nfk_OT, _idLineaProceso, _nRegistroTipo.ToString, Program.RiskGlobal.Usa_Tabla_Fisico, Faltante)
                        End If
                    Catch
                        Throw
                    End Try

                    DocumentosFolder()
                    TipologiaComboBox.Focus()
                Else
                    'SI NO HAY DOCUMENTOS DISPONIBLES PARA PROCESAR

                    Dim id_Documento As Integer = CInt(TipologiaComboBox.SelectedValue)
                    Dim MotivoReproceso As DesktopConfig.MotivoReproceso = DesktopConfig.MotivoReproceso.N_A
                    Dim Sobrante As Boolean = False
                    Dim nEstado = DBCore.EstadoEnum.Destapado
                    Dim ValidaMensajes As Boolean = True


                    'Detecta la clase de tipo de registro   A-D-N    - "Si es devolucion valida si permite devolucion"

                    Dim CarpetaDataTable = dbmArchiving.SchemaRisk.TBL_Folder.DBGet(_nfk_Expediente, _nfk_Folder, _nfk_OT)

                    Dim _Registro_Tipo_Carpeta As Integer = 1

                    If CarpetaDataTable.Rows.Count > 0 Then
                        _Registro_Tipo_Carpeta = CarpetaDataTable(0).fk_Registro_Tipo
                    End If

                    Dim Clase As DesktopConfig.RegistroTipo = _nRegistroTipo
                    'If _RegistroTipo = DesktopConfig.RegistroTipo.Devolucion Then Clase = DesktopConfig.RegistroTipo.Adicion
                    If _nRegistroTipo = DesktopConfig.RegistroTipo.Nuevo And _Registro_Tipo_Carpeta = 1 Then
                        Clase = DesktopConfig.RegistroTipo.Adicion
                    End If

                    If EsDevolucion Then
                        Clase = DesktopConfig.RegistroTipo.Devolucion
                        PermiteDevolucion = Utilities.PermiteDevolucion(dbmArchiving, CBarrasFile)
                        If Not PermiteDevolucion.PermiteDevolucion Then
                            DesktopMessageBoxControl.DesktopMessageShow("El documento no permite devoluciones porque no esta en un estado de prestamo.", "Documento no permite devoluciones", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                            ValidaMensajes = False
                            DocumentosDisponibles.AceptaSobrantes = True
                        End If
                    End If

                    'Valida si es un sobrante o un reproceso, si es sin cargue omite este paso
                    If (Not _TipoFile = DesktopConfig.TipoFileCargue.SinCargue) And ValidaMensajes = True Then
                        If DocumentosDisponibles.AceptaSobrantes Then
                            Sobrante = True
                            If (Program.RiskGlobal.Usa_Cargue_Carpeta = False And Program.RiskGlobal.Usa_Validacion_Destape) Or (Program.RiskGlobal.Usa_Cargue_Carpeta = False And Not Program.RiskGlobal.Usa_Validacion_Destape And ExisteCargueLog = False) Then
                                If Not DesktopMessageBoxControl.DesktopMessageShow("El documento no se encuentra en el archivo de cargue.  ¿Desea Crearlo como Sobrante? ", "Sobrante", DesktopMessageBoxControl.IconEnum.WarningIcon, False) = DialogResult.OK Then
                                    ValidaMensajes = False
                                End If
                            End If
                        Else
                            MotivoReproceso = DesktopConfig.MotivoReproceso.FilesSobrantes
                            nEstado = DBCore.EstadoEnum.Reproceso
                            If Not DesktopMessageBoxControl.DesktopMessageShow("El esquema seleccionado no permite sobrantes.  ¿Desea Enviar el documento a Reproceso?", "Reproceso", DesktopMessageBoxControl.IconEnum.WarningIcon, False) = DialogResult.OK Then
                                ValidaMensajes = False
                            End If
                        End If
                    End If

                    'Si acepta las pautas inserta el Documento en Core y Archiving
                    If ValidaMensajes = True Then
                        Try
                            dbmArchiving.Transaction_Begin()
                            dbmCore.Transaction_Begin()

                            'Crea File para Nuevos y adiciones
                            If Clase <> DesktopConfig.RegistroTipo.Devolucion Then
                                dbmArchiving.Schemadbo.PA_Destape_Inserta_File_Nuevo.DBExecute(_nfk_Expediente, _nfk_Folder, _nfk_OT, id_Documento, Clase, Program.Sesion.Usuario.id, _idLineaProceso, Program.RiskGlobal.CajaProceso, Program.RiskGlobal.Usa_Tabla_Fisico, Program.RiskGlobal.Precinto)
                            Else
                                'Crea y actualiza File para Devoluciones
                                Dim FileCore = dbmCore.SchemaProcess.TBL_File.DBFindByCBarras_File(CBarrasFile)
                                'Utilities.InsertaFileArchiving(dbmArchiving, dbmCore, FileCore(0).id_File, _nfk_Folder, _nfk_Expediente, MotivoReproceso, Sobrante, Clase, _nfk_OT, Program.RiskGlobal.CajaProceso, nEstado, False, Program.Sesion.Usuario.id, _idLineaProceso)
                                Dim FileRisk = dbmArchiving.SchemaRisk.TBL_File.DBFindByfk_Folderid_Filefk_expediente(_nfk_Folder, FileCore(0).id_File, _nfk_Expediente)
                                Utilities.ActualizaEstadoFileDevolucion(dbmArchiving, dbmCore, CBarrasFile, FileRisk(0).fk_OT, Program.RiskGlobal.Precinto, DesktopConfig.Modulo.Archiving, nEstado, Program.Sesion.Usuario.id, Program.RiskGlobal.CajaProceso, _nfk_OT, _idLineaProceso, _nRegistroTipo.ToString(), Program.RiskGlobal.Usa_Tabla_Fisico, Sobrante, Faltante)

                            End If

                            'If Not dbmCore.DataBase.ExistsTransaction Then dbmCore.Transaction_Begin()
                            InsertaFolderCustodiaMovimiento(dbmCore)

                            dbmCore.Transaction_Commit()
                            dbmArchiving.Transaction_Commit()
                        Catch
                            dbmCore.Transaction_Rollback()
                            dbmArchiving.Transaction_Rollback()
                            Throw
                        End Try

                        DocumentosFolder()
                    End If
                End If
            Catch ex As Exception
                If ex.Message.IndexOf("UK_TBL_File", StringComparison.Ordinal) >= 0 Then
                    If dbmArchiving IsNot Nothing Then dbmArchiving.Schemadbo.PA_Actualiza_Consecutivo_CBarras.DBExecute()
                    DesktopMessageBoxControl.DesktopMessageShow("Han ocurrido problemas al crear el documento, por favor intente nuevamente.", "Error procesando documento", DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
                Else
                    DesktopMessageBoxControl.DesktopMessageShow("Proceso_Adiciones_Nuevos", ex)
                End If

            Finally
                If dbmCore IsNot Nothing Then dbmCore.Connection_Close()
                If dbmArchiving IsNot Nothing Then dbmArchiving.Connection_Close()
            End Try

            If CBarrasFile = "" Then
                TipologiaComboBox.Focus()
            Else
                cbarrasDesktopCBarrasControl.Focus()
                cbarrasDesktopCBarrasControl.SelectAll()
            End If
        End Sub

        Public Sub InsertaFolderCustodiaMovimiento(ByVal dbmCore As DBCore.DBCoreDataBaseManager)
            'Busca que la carpeta no tenga la fecha de salida
            Dim FolderCustodiaMovimiento = dbmCore.SchemaCustody.TBL_Folder_Movimiento.DBFindByid_Folder_Movimientofk_Expedientefk_FolderFecha_InicialFecha_Final(Nothing, _nfk_Expediente, _nfk_Folder, Nothing, DBNull.Value)

            'Si no encuentra el folder que cumpla es filtro entonces lo crea
            If FolderCustodiaMovimiento.Count = 0 Then
                Dim FolderType As New DBCore.SchemaCustody.TBL_Folder_MovimientoType
                FolderType.Fecha_Inicial = SlygNullable.SysDate
                FolderType.fk_Expediente = _nfk_Expediente
                FolderType.fk_Folder = _nfk_Folder
                FolderType.id_Folder_Movimiento = dbmCore.SchemaCustody.TBL_Folder_Movimiento.DBNextId(_nfk_Expediente, _nfk_Folder)
                FolderType.Fecha_Final = DBNull.Value

                dbmCore.SchemaCustody.TBL_Folder_Movimiento.DBInsert(FolderType)
            End If
        End Sub

        Public Sub EliminarFile(ByVal nCBarras As String, ByVal tipologia As Short, ByVal ArchivoCargado As Boolean, ByVal ot As Integer, ByVal expediente As Long, ByVal folder As Short, ByVal file As Short)
            Dim dbmArchiving As DBArchivingDataBaseManager = Nothing
            Dim dmcore As DBCore.DBCoreDataBaseManager = Nothing

            Try

                dbmArchiving = New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
                dmcore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)

                dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)
                dmcore.Connection_Open(Program.Sesion.Usuario.id)

                dbmArchiving.Transaction_Begin()
                dmcore.Transaction_Begin()

                Dim FilesNoCargadosView As New DataView(_TableDocumentosxCarpeta)
                FilesNoCargadosView.RowFilter = _TableDocumentosxCarpeta.CargadoColumn.ColumnName & "=0 and " & _TableDocumentosxCarpeta.id_TipologiaColumn.ColumnName & "=" & CStr(tipologia)
                Dim Devolucion As Boolean

                If FilesNoCargadosView.Count > 0 Then
                    Dim FileSeleccionado As New DataView(FilesNoCargadosView.ToTable())
                    FileSeleccionado.RowFilter = _TableDocumentosxCarpeta.CBarrasColumn.ColumnName & "='" & nCBarras & "'"


                    Dim tabla = FileSeleccionado.ToTable()
                    If tabla.Rows.Count > 0 Then
                        Dim row = tabla.Rows(0)
                        'si existe el documento seleccionado en estado no cargado entonces le hace el proceso
                        'CBarras = CStr(row(_TableDocumentosxCarpeta.CBarrasColumn.ColumnName))
                        ot = CInt(row(_TableDocumentosxCarpeta.fk_OTColumn.ColumnName))
                        expediente = CLng(row(_TableDocumentosxCarpeta.fk_expedienteColumn.ColumnName))
                        folder = CShort(row(_TableDocumentosxCarpeta.fk_FolderColumn.ColumnName))
                        ArchivoCargado = CBool(row(_TableDocumentosxCarpeta.CargadoColumn.ColumnName))
                        file = CShort(row(_TableDocumentosxCarpeta.id_fileColumn.ColumnName))
                        Devolucion = CShort(row(_TableDocumentosxCarpeta.ProcesoColumn.ColumnName).ToString().Substring(0, 1)) = DesktopConfig.RegistroTipo.Devolucion

                    Else
                        Dim row = FilesNoCargadosView.ToTable.Rows(0)
                        'si no existe el documento seleccionaod selecciona el primero que cargado y de la misma tipologia
                        'CBarras = CStr(row(_TableDocumentosxCarpeta.CBarrasColumn.ColumnName))
                        ot = CInt(row(_TableDocumentosxCarpeta.fk_OTColumn.ColumnName))
                        expediente = CLng(row(_TableDocumentosxCarpeta.fk_expedienteColumn.ColumnName))
                        folder = CShort(row(_TableDocumentosxCarpeta.fk_FolderColumn.ColumnName))
                        ArchivoCargado = CBool(row(_TableDocumentosxCarpeta.CargadoColumn.ColumnName))
                        file = CShort(row(_TableDocumentosxCarpeta.id_fileColumn.ColumnName))
                        Devolucion = CShort(row(_TableDocumentosxCarpeta.ProcesoColumn.ColumnName).ToString().Substring(0, 1)) = DesktopConfig.RegistroTipo.Devolucion
                    End If
                End If

                If Not ArchivoCargado And Not Devolucion Then
                    'Eliminar
                    dbmArchiving.SchemaRisk.TBL_File.DBDelete(ot, folder, file, expediente)

                    If dbmArchiving.SchemaRisk.TBL_File.DBGet(Nothing, folder, file, expediente).Count = 0 Then
                        dmcore.SchemaProcess.TBL_File.DBDelete(expediente, folder, file)
                    End If
                ElseIf Not ArchivoCargado And Devolucion Then
                    Dim registro As New SchemaRisk.TBL_FileType
                    registro.fk_Estado = DBCore.EstadoEnum.Enviado_a_prestamo
                    registro.fk_Linea_Proceso = PermiteDevolucion.LineaProceso
                    registro.fk_OT = PermiteDevolucion.OT
                    registro.Impreso = False
                    dbmArchiving.SchemaRisk.TBL_File.DBUpdate(registro, ot, folder, file, expediente)
                    dmcore.SchemaProcess.TBL_File_Estado.DBUpdate(Nothing, Nothing, Nothing, Nothing, DBCore.EstadoEnum.Enviado_a_prestamo, Nothing, Nothing, expediente, folder, file, DesktopConfig.Modulo.Archiving)
                Else
                    'Actualizar estados
                    Dim registro As New SchemaRisk.TBL_FileType
                    registro.fk_Estado = DBCore.EstadoEnum.Cargado
                    registro.fk_Linea_Proceso = DBNull.Value
                    registro.Impreso = False
                    dbmArchiving.SchemaRisk.TBL_File.DBUpdate(registro, ot, folder, file, expediente)
                    dmcore.SchemaProcess.TBL_File_Estado.DBUpdate(Nothing, Nothing, Nothing, Nothing, DBCore.EstadoEnum.Cargado, Nothing, Nothing, expediente, folder, file, DesktopConfig.Modulo.Archiving)
                End If

                If Program.RiskGlobal.Usa_Tabla_Fisico Then
                    'Reversar Cruce Fisicos
                    dbmArchiving.Schemaprocess.PA_Cruce_Reversar.DBExecute(expediente, folder, file)
                End If


                dmcore.Transaction_Commit()
                dbmArchiving.Transaction_Commit()

            Catch ex As Exception
                If dmcore IsNot Nothing Then dmcore.Transaction_Rollback()
                If dbmArchiving IsNot Nothing Then dbmArchiving.Transaction_Rollback()
            Finally
                If dmcore IsNot Nothing Then dbmArchiving.Connection_Close()
                If dbmArchiving IsNot Nothing Then dmcore.Connection_Close()
            End Try

            DocumentosFolder()
        End Sub

        Public Sub ImprimirCBarras()
            Dim dbmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

            Dim TableCBarras = dbmArchiving.Schemadbo.PA_CBarras_Impresion.DBExecute(Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, "", _nCBarras_Folder)
            If TableCBarras.Rows.Count > 0 Then
                If DesktopMessageBoxControl.DesktopMessageShow("Desea imprimir los códigos de barras", "Impresión Códigos de barras", DesktopMessageBoxControl.IconEnum.AdvertencyIcon) = DialogResult.OK Then
                    Utilities.IniciarImpresion(Program.DesktopGlobal.ConnectionStrings, Program.Sesion, TableCBarras)
                End If
            End If
            dbmArchiving.Connection_Close()
        End Sub

#End Region

#Region " Eventos "

        Private Sub FormDocumentos_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            'Se valida que exista línea de proceso
            If Not IsNothing(Program.Sesion.Parameter("_idLineaProceso")) Then

                Select Case _RegistroTipo
                    Case DesktopConfig.RegistroTipo.Adicion
                        ClaseLabel.Text = "Adicion"
                        ClaseLabel.ForeColor = Drawing.Color.DarkRed

                    Case (DesktopConfig.RegistroTipo.Nuevo)
                        ClaseLabel.Text = "Nuevo"
                        ClaseLabel.ForeColor = Drawing.Color.SeaGreen

                    Case DesktopConfig.RegistroTipo.Devolucion
                        ClaseLabel.Text = "Devolucion"
                        ClaseLabel.ForeColor = Drawing.Color.DarkBlue

                End Select

                _idLineaProceso = CInt(Program.Sesion.Parameter("_idLineaProceso"))
                CargaCombos()
                DocumentosFolder()
            Else
                DesktopMessageBoxControl.DesktopMessageShow("No existe una línea de proceso.", "Línea de proceso", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                Me.Close()
            End If
        End Sub

        Private Sub CerrarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CerrarButton.Click
            Dim dbmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

            Dim Eliminado As Boolean = dbmArchiving.Schemadbo.PA_Destape_Elimina_Folder.DBExecute(_nfk_Expediente, _nfk_Folder, _nfk_OT)
            If Not Eliminado Then
                'Mensaje de los documentos obligatorios que no han llegado
                Dim DocFaltantes = dbmArchiving.Schemadbo.PA_Destape_Documentos_Obligatorios.DBExecute(_nCBarras_Folder)
                Dim DocFaltantesString As New StringBuilder
                Dim strMensajeDocumentos As String = ""

                For Each Row As DataRow In DocFaltantes.Rows
                    If CBool(Row("Es_Obligatorio")) Then
                        DocFaltantesString.AppendLine("<br><font color='red'>" & Row("Nombre_Tipologia").ToString() & "</font>")
                    Else
                        DocFaltantesString.AppendLine("<br>" & Row("Nombre_Tipologia").ToString().ToUpper())
                    End If
                Next

                If DocFaltantes.Rows.Count > 0 Then
                    strMensajeDocumentos = "Existen documentos faltantes, los cuáles podrían causar reprocesos." & vbNewLine
                    strMensajeDocumentos += DocFaltantesString.ToString()
                End If

                If DesktopMessageBoxControl.DesktopMessageShow(New StringBuilder("¿Está seguro que desea cerrar la carpeta?" & vbNewLine & vbNewLine & strMensajeDocumentos), "Cerrar carpeta", DesktopMessageBoxControl.IconEnum.WarningIcon, False) = DialogResult.OK Then
                    ImprimirCBarras()
                    Me.Close()
                End If

            Else
                Me.Close()
            End If

            dbmArchiving.Connection_Close()
        End Sub

        Private Sub AceptarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AgregarButton.Click
            If cbarrasDesktopCBarrasControl.Text = "" And CStr(TipologiaComboBox.SelectedValue) = "" Then
                DesktopMessageBoxControl.DesktopMessageShow("Debe seleccionar una tipología o digitar un código de barras", "Error al procesar", DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
                cbarrasDesktopCBarrasControl.Focus()
            Else
                If cbarrasDesktopCBarrasControl.Text <> "" Then
                    Procesar_Documento(cbarrasDesktopCBarrasControl.Text)
                Else
                    Procesar_Documento()
                End If

                TipologiaComboBox.SelectedIndex = -1
            End If
        End Sub

        Private Sub CBarrasDesktopTextBox_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles cbarrasDesktopCBarrasControl.KeyPress
            If e.KeyChar = ChrW(Keys.Enter) Or e.KeyChar = ChrW(Keys.Tab) Then
                If cbarrasDesktopCBarrasControl.Text = "" Then
                    TipologiaComboBox.Focus()
                Else
                    AgregarButton.Focus()
                End If
            End If
        End Sub

        Private Sub TipologiaComboBox_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles TipologiaComboBox.KeyDown
            If e.KeyCode = Keys.Enter Then
                If CStr(TipologiaComboBox.SelectedValue) = "" Then
                    CerrarButton.Focus()
                Else
                    e.Handled = True
                    SendKeys.Send("{TAB}")
                End If
            End If
        End Sub

        Private Sub DocumentosDesktopDataGridView_UserDeletingRow(ByVal sender As System.Object, ByVal e As DataGridViewRowCancelEventArgs) Handles DocumentosDesktopDataGridView.UserDeletingRow
            Try
                If DesktopMessageBoxControl.DesktopMessageShow("Esta seguro de eliminar el documento", "Eliminar documento", DesktopMessageBoxControl.IconEnum.AdvertencyIcon) = DialogResult.OK Then
                    Dim indice As Integer = e.Row.Index

                    Dim CBarrasLocal As String = CStr(DocumentosDesktopDataGridView.Rows(indice).Cells("CBarras").Value)
                    Dim CargadoLocal As Boolean = CBool(DocumentosDesktopDataGridView.Rows(indice).Cells("cargado").Value)
                    Dim ot As Integer = CInt(DocumentosDesktopDataGridView.Rows(indice).Cells("fk_ot").Value)
                    Dim expediente As Long = CLng(DocumentosDesktopDataGridView.Rows(indice).Cells("fk_expediente").Value)
                    Dim folder As Short = CShort(DocumentosDesktopDataGridView.Rows(indice).Cells("fk_folder").Value)
                    Dim file As Short = CShort(DocumentosDesktopDataGridView.Rows(indice).Cells("id_file").Value)
                    Dim tipologia As Short = CShort(DocumentosDesktopDataGridView.Rows(indice).Cells("id_tipologia").Value)

                    If CShort(DocumentosDesktopDataGridView.Rows(indice).Cells("id_estado").Value) = DBCore.EstadoEnum.Cargado Then
                        DesktopMessageBoxControl.DesktopMessageShow("El documento no se puede eliminar ya que este ha sido cargado desde un archivo", "Problemas eliminando documento", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                        e.Cancel = True
                    ElseIf CargadoLocal = True Then
                        e.Cancel = True
                        EliminarFile(CBarrasLocal, tipologia, CargadoLocal, ot, expediente, folder, file)
                    Else
                        EliminarFile(CBarrasLocal, tipologia, CargadoLocal, ot, expediente, folder, file)
                    End If

                    e.Cancel = True
                Else
                    e.Cancel = True
                End If

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("DocumentosDesktopDataGridView_UserDeletingRow", ex)
            End Try
        End Sub

#End Region

    End Class

End Namespace