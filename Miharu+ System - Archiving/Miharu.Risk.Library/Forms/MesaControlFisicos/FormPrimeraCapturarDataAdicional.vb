Imports System.Text
Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopDataGridView
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Controls.DesktopTextBox
Imports Miharu.Desktop.Controls.DesktopComboBox
Imports Miharu.Desktop.Controls.DesktopCheckBox
Imports Miharu.Desktop.Controls
Imports DBArchiving
Imports DBArchiving.Schemadbo
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Config

Namespace Forms.MesaControlFisicos

    Public Class FormPrimeraCapturarDataAdicional
        Inherits FormBase

#Region " Declaraciones "

        Private _CBarras As String
        Private _TableFormValidaciones As TableLayoutPanel
        Private _TableForm As TableLayoutPanel
        Private _Campos As New List(Of CamposDataAdicional)
        Private _Validaciones As New List(Of CamposValidaciones)
        'Private _TipoCaptura As DesktopConfig.TipoCaptura
        Private _typeCoreFile As New DBCore.SchemaProcess.TBL_FileType
        Private _typeArchivingFile As New SchemaRisk.TBL_FileType
        Private _tFileDetalle As CTA_File_Detalle_2DataTable
        Private _tFileDocumento As CTA_DocumentoDataTable
        Private _TriggersDataTable As DBArchiving.SchemaConfig.TBL_Campo_TriggerDataTable '= nDBMImaging.SchemaConfig.TBL_Campo_Trigger.DBGet(DBImaging.EnumEtapaCaptura.Captura, FileCoreRow.fk_Documento, Nothing, Nothing, Nothing, Nothing)
        Private _TriggersValidacionesDataTable As DBArchiving.SchemaConfig.TBL_Campo_Trigger_ValidacionDataTable 'nDBMImaging.SchemaConfig.TBL_Campo_Trigger_Validacion.DBGet(DBImaging.EnumEtapaCaptura.Captura, FileCoreRow.fk_Documento, Nothing, Not


#End Region

#Region " Estructuras "

        Structure CamposDataAdicional
            Public NombreCampo As String
            Public Id_Campo As Short
            Public LongitudCampo As Short
            Public CampoObligatorio As Boolean
            Public CampoLista As Short
            Public TipoCampo As DesktopConfig.CampoTipo
            Public Documento As Integer
            Public Expediente As Long
            Public File As Short
            Public Folder As Short
            Public fk_Entidad As Short
            Public fk_Documento As Integer
            Public fk_Esquema As Short
            Public Tiene_Datos As Boolean
        End Structure

        Structure CamposValidaciones
            Public Documento As Integer
            Public Id_Validacion As Short
            Public Pregunta As String
            Public Obligatorio As Boolean
            Public Subsanable As Boolean
        End Structure

#End Region

#Region " Constructor "

        Sub New(ByVal CBarras As String)
            ' This call is required by the designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            _CBarras = CBarras
        End Sub

        Public Shared Sub Captura(ByVal CBarras As String)
            Dim MeForm As New FormPrimeraCapturarDataAdicional(CBarras)
            MeForm.ShowDialog()
        End Sub

#End Region


#Region " Eventos "

        Private Sub FormCapturarDataAdicional_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            CBarrasLabel.Text = _CBarras
            Dim dbmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            Try
                dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)
                _tFileDetalle = dbmArchiving.Schemadbo.CTA_File_Detalle_2.DBFindByCBarras_Filefk_entidadfk_proyecto(CBarrasLabel.Text, Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto)
                _TriggersDataTable = dbmArchiving.SchemaConfig.TBL_Campo_Trigger.DBGet(DBArchiving.EnumEtapaCaptura.Mesa_Control, CShort(_tFileDetalle(0).fk_Documento), Nothing, Nothing, Nothing, Nothing)
                _TriggersValidacionesDataTable = dbmArchiving.SchemaConfig.TBL_Campo_Trigger_Validacion.DBGet(DBArchiving.EnumEtapaCaptura.Mesa_Control, CShort(_tFileDetalle(0).fk_Documento), Nothing, Nothing, Nothing)
                dbmArchiving.Connection_Close()

                If _tFileDetalle.Rows.Count > 0 Then
                    ConfiguraFoliosMonto()
                    CreaCampos()
                    crearValidaciones()

                Else
                    DesktopMessageBoxControl.DesktopMessageShow("El código de barras no se encuentra en la base de datos, puede ser que no tenga campos parametrizados o que no exista para el proyecto, por favor intente con otro.", "No se ha encontrado el código de barras", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                    Me.Close()
                End If

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("FormCapturarDataAdicional_Load", ex)
            End Try
            FoliosDesktopTextBox.Focus()
        End Sub

        Private Sub CerrarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CerrarButton.Click
            Me.Close()
        End Sub

        Private Sub AceptarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AceptarButton.Click
            If ValidarData() Then
                GuardarDatos()
            Else
                DesktopMessageBoxControl.DesktopMessageShow("Existen campos que son obligatorios y no han sido diligenciados. [Campos resaltados].", "Validación de campos", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
            End If

        End Sub

        Private Sub MontoDesktopTextBox_KeyPress(ByVal sender As System.Object, ByVal e As KeyPressEventArgs) Handles MontoDesktopTextBox.KeyPress
            If e.KeyChar = ChrW(Keys.Enter) Or e.KeyChar = ChrW(Keys.Tab) Then
                BuscaSiguienteControl()
                e.Handled = True
            End If
        End Sub

        Private Sub SplitContainer1_GotFocus(ByVal sender As System.Object, ByVal e As EventArgs) Handles SplitContainer1.GotFocus
            If ValidacionesPanel.Controls.Count = 0 Then
                AceptarButton.Focus()
            Else
                Dim LayoutCampos = CType(ValidacionesPanel.Controls(0), TableLayoutPanel)
                For Each campo In LayoutCampos.Controls
                    Select Case campo.GetType().ToString()
                        Case "Miharu.Desktop.Controls.DesktopCheckBox.DesktopCheckBoxControl"
                            CType(campo, CheckBox).Focus()
                            Exit For
                    End Select
                Next
            End If
        End Sub

        Public Sub control_trigger(sender As Object, e As EventArgs)

            Select Case sender.GetType()
                Case GetType(DesktopTextBoxControl)
                    Dim Ctrl As DesktopTextBoxControl
                    Ctrl = CType(sender, DesktopTextBoxControl)
                    Dim fk_Documento As Integer
                    fk_Documento = Ctrl.fk_Documento

                    Dim fk_Campo As Integer
                    fk_Campo = Ctrl.fk_Campo


                    CamposTirgger(fk_Documento, fk_Campo, Ctrl.Text)

                Case GetType(DesktopComboBoxControl)
                    Dim Ctrl As DesktopComboBoxControl
                    Ctrl = CType(sender, DesktopComboBoxControl)
                    Dim fk_Documento As Integer
                    fk_Documento = Ctrl.fk_Documento

                    Dim fk_Campo As Integer
                    fk_Campo = Ctrl.fk_Campo


                    CamposTirgger(fk_Documento, fk_Campo, Ctrl.SelectedValue.ToString)

            End Select


        End Sub

#End Region


#Region " Metodos "

        Public Sub CreaCampos()
            EntidadLabel.Text = Program.RiskGlobal.NombreEntidad
            ProyectoLabel.Text = Program.RiskGlobal.NombreProyecto

            Dim dbmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

            EsquemaLabel.Text = _tFileDetalle.Rows(0)(_tFileDetalle.fk_esquemaColumn).ToString & " - " & _tFileDetalle.Rows(0)(_tFileDetalle.Nombre_esquemaColumn).ToString
            TipologiaLabel.Text = _tFileDetalle.Rows(0)(_tFileDetalle.id_TipologiaColumn).ToString & " - " & _tFileDetalle.Rows(0)(_tFileDetalle.Nombre_TipologiaColumn).ToString
            CapturaLabel.Text = "Primera Captura"
            Try
                Dim TableCampos = Program.RiskGlobal.DatosMesa_DBFindfkDocumento(_tFileDetalle(0).fk_Documento, True)
                If TableCampos.Rows.Count > 0 Then

                    _Campos = New List(Of CamposDataAdicional)
                    Dim CampoControlCollection As New List(Of DesktopConfig.CrearControlesType)

                    For Each Row As DataRow In TableCampos.Rows
                        Dim campo As New CamposDataAdicional
                        Dim CampoControl As New DesktopConfig.CrearControlesType

                        'Configuracion del campo
                        campo.Id_Campo = CShort(Row(CTA_Campos_Documentos_MesaEnum.id_Campo.ColumnName))
                        campo.LongitudCampo = CShort(Row(CTA_Campos_Documentos_MesaEnum.Length_Campo.ColumnName))
                        campo.NombreCampo = CStr(Row(CTA_Campos_Documentos_MesaEnum.Nombre_Campo.ColumnName))
                        campo.CampoObligatorio = CBool(Row(CTA_Campos_Documentos_MesaEnum.Es_Obligatorio_Campo.ColumnName))
                        campo.CampoLista = CShort(Row(CTA_Campos_Documentos_MesaEnum.fk_Campo_Lista.ColumnName))
                        campo.TipoCampo = CType(Row(CTA_Campos_Documentos_MesaEnum.fk_Campo_Tipo.ColumnName), DesktopConfig.CampoTipo)
                        campo.Documento = CShort(Row(CTA_Campos_Documentos_MesaEnum.fk_Documento.ColumnName))

                        'Informacion del documento
                        campo.Expediente = _tFileDetalle(0).fk_Expediente
                        campo.Folder = _tFileDetalle(0).fk_Folder
                        campo.File = _tFileDetalle(0).id_File

                        'Para Tabla Asociada
                        campo.fk_Entidad = Program.RiskGlobal.Entidad
                        campo.fk_Documento = CShort(Row(CTA_Campos_Documentos_MesaEnum.fk_Documento.ColumnName))
                        campo.fk_Esquema = CShort(Row(CTA_Campos_Documentos_MesaEnum.fk_Esquema.ColumnName))

                        'configuracion Control
                        CampoControl.CampoLista = CShort(Row(CTA_Campos_Documentos_MesaEnum.fk_Campo_Lista.ColumnName))
                        CampoControl.Label = CStr(Row(CTA_Campos_Documentos_MesaEnum.Nombre_Campo.ColumnName))
                        CampoControl.MaxLength = CShort(Row(CTA_Campos_Documentos_MesaEnum.Length_Campo.ColumnName))
                        CampoControl.NombreCampo = CStr(Row(CTA_Campos_Documentos_MesaEnum.Nombre_Campo.ColumnName))
                        CampoControl.Tipo = CType(Row(CTA_Campos_Documentos_MesaEnum.fk_Campo_Tipo.ColumnName), DesktopConfig.CampoTipo)
                        CampoControl.Width = 200
                        CampoControl.LabelWidth = 200
                        CampoControl.Obligatorio = CBool(Row(CTA_Campos_Documentos_MesaEnum.Es_Obligatorio_Campo.ColumnName))

                        'Data
                        Dim File_Data = dbmArchiving.SchemaCore.CTA_File_Data.DBFindByfk_Expedientefk_Folderfk_Filefk_Documentofk_Campo(_tFileDetalle(0).fk_Expediente, _tFileDetalle(0).fk_Folder, _tFileDetalle(0).id_File, _tFileDetalle(0).fk_Documento, CShort(Row(CTA_Campos_Documentos_MesaEnum.id_Campo.ColumnName)))
                        If File_Data.Rows.Count > 0 Then

                            If File_Data(0).IsValor_File_DataNull Then
                                CampoControl.File_Data = ""
                            Else
                                CampoControl.File_Data = File_Data(0).Valor_File_Data
                            End If

                            CampoControl.Existe_File_Data = True
                            campo.Tiene_Datos = True

                        Else
                            CampoControl.Existe_File_Data = False
                            CampoControl.File_Data = DBNull.Value
                            campo.Tiene_Datos = False
                        End If

                        'Para Tabla asociada
                        CampoControl.fk_Entidad = Program.RiskGlobal.Entidad
                        CampoControl.fk_Documento = CShort(Row(CTA_Campos_Documentos_MesaEnum.fk_Documento.ColumnName))
                        CampoControl.fk_Campo = CShort(Row(CTA_Campos_Documentos_MesaEnum.id_Campo.ColumnName))

                        _Campos.Add(campo)
                        CampoControlCollection.Add(CampoControl)

                    Next

                    _TableForm = Utilities.CreaControles(dbmArchiving, CampoControlCollection, Program.DesktopGlobal.ConnectionStrings, Program.RiskGlobal, True, CamposPanel.Width)
                    CamposPanel.Controls.Add(_TableForm)

                    For Each Ctrl As Control In CamposPanel.Controls

                        Select Case Ctrl.GetType()
                            Case GetType(TableLayoutPanel)
                                For Each C As Control In Ctrl.Controls
                                    Select Case C.GetType()
                                        Case GetType(DesktopTextBoxControl)
                                            AddHandler C.Leave, AddressOf control_trigger
                                        Case GetType(DesktopComboBoxControl)
                                            AddHandler C.Leave, AddressOf control_trigger
                                            AddHandler CType(C, DesktopComboBoxControl).SelectedIndexChanged, AddressOf control_trigger
                                    End Select
                                Next
                        End Select

                    Next

                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CrearEstructuraCampos", ex)
                Me.Close()
            End Try

            dbmArchiving.Connection_Close()
        End Sub

        Public Sub crearValidaciones()
            Dim dbmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

            Try
                _TableFormValidaciones = New TableLayoutPanel()

                Dim CampoControl As New DesktopConfig.CrearControlesType
                Dim CampoControlCollection As New List(Of DesktopConfig.CrearControlesType)
                Dim TableValidaciones = Program.RiskGlobal.ValidacionesMesa_DBFindfkDocumento(_tFileDetalle(0).fk_Documento)

                _Validaciones = New List(Of CamposValidaciones)
                For Each Validacion As DataRow In TableValidaciones.Rows
                    Dim campo As New CamposValidaciones
                    campo.Id_Validacion = CShort(Validacion("id_Validacion"))
                    campo.Documento = CInt(Validacion("fk_Documento"))
                    campo.Pregunta = CStr(Validacion("Pregunta_Validacion"))
                    campo.Obligatorio = CBool(Validacion("Obligatorio"))
                    campo.Subsanable = CBool(Validacion("Es_Subsanable"))
                    _Validaciones.Add(campo)

                    CampoControl.Label = CStr(Validacion("Pregunta_Validacion"))
                    CampoControl.NombreCampo = CStr(Validacion("Pregunta_Validacion"))
                    CampoControl.Tipo = DesktopConfig.CampoTipo.SiNo
                    CampoControl.LabelWidth = 600
                    CampoControl.fk_Documento = CInt(Validacion("fk_Documento"))
                    CampoControl.fk_Validacion = CShort(Validacion("id_Validacion"))
                    CampoControlCollection.Add(CampoControl)
                Next

                If CampoControlCollection.Count > 0 Then
                    _TableFormValidaciones = Utilities.CreaControles(dbmArchiving, CampoControlCollection, Program.DesktopGlobal.ConnectionStrings, Program.RiskGlobal, False, ValidacionesPanel.Width, True, True)
                    ValidacionesPanel.Controls.Add(_TableFormValidaciones)
                End If

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CrearEstructuraCampos", ex)
                Me.Close()
            End Try

            dbmArchiving.Connection_Close()
        End Sub

        Private Sub GuardarDatos()
            Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
            Dim dbmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)

            Try
                dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)
                dbmCore.Transaction_Begin()
                dbmArchiving.Transaction_Begin()


                'Variables
                Dim FacturaPrimeraCapturaCaracter As Integer = 0
                Dim bFileDevuelto As Boolean = False
                Dim bFolderSubsanable As Boolean = False
                Dim validacionNegativa As Boolean = False
                Dim registroValidacion As New DBCore.SchemaProcess.TBL_File_ValidacionType


                'Monto y Folios
                _typeCoreFile.Folios_File = CShort(IIf(FoliosDesktopTextBox.Text <> "", FoliosDesktopTextBox.Text, 0))
                _typeCoreFile.Monto_File = CDec(IIf(MontoDesktopTextBox.Text <> "", MontoDesktopTextBox.Text, 0))
                FacturaPrimeraCapturaCaracter += _typeCoreFile.Folios_File.ToString().Length
                FacturaPrimeraCapturaCaracter += _typeCoreFile.Monto_File.ToString().Length
                dbmCore.SchemaProcess.TBL_File.DBUpdate(_typeCoreFile, _tFileDetalle(0).fk_Expediente, _tFileDetalle(0).fk_Folder, _tFileDetalle(0).id_File)


                'Guarda la captura
                Dim registro As New DBCore.SchemaProcess.TBL_File_DataType
                Dim registroTablaAsociada As New DBCore.SchemaProcess.TBL_File_Data_AsociadaType
                Dim numeroCampos As Integer = 2 'Inicia en dos apar tener en cuenta Folios y Monto

                For Each Campo As CamposDataAdicional In _Campos
                    Dim NombreControl As String = Utilities.ConvertName(Campo.NombreCampo)
                    Dim ControlCampo As Control = Utilities.FindControl(_TableForm, NombreControl)

                    If Campo.TipoCampo = DesktopConfig.CampoTipo.Texto Or Campo.TipoCampo = DesktopConfig.CampoTipo.Fecha Or Campo.TipoCampo = DesktopConfig.CampoTipo.Numerico Then
                        If CType(ControlCampo, DesktopTextBoxControl).Enabled Then
                            numeroCampos += 1
                            registro.fk_Campo = Campo.Id_Campo
                            registro.fk_Documento = Campo.Documento
                            registro.fk_Expediente = Campo.Expediente
                            registro.fk_File = Campo.File
                            registro.fk_Folder = Campo.Folder

                            registro.Valor_File_Data = CType(ControlCampo, DesktopTextBoxControl).Text

                            registro.Conteo_File_Data = registro.Valor_File_Data.ToString().Length

                            If Campo.Tiene_Datos Then : dbmCore.SchemaProcess.TBL_File_Data.DBUpdate(registro, registro.fk_Expediente, registro.fk_Folder, registro.fk_File, registro.fk_Documento, registro.fk_Campo)
                            Else : dbmCore.SchemaProcess.TBL_File_Data.DBInsert(registro) : End If

                            FacturaPrimeraCapturaCaracter += registro.Valor_File_Data.ToString().Length
                        End If

                    ElseIf Campo.TipoCampo = DesktopConfig.CampoTipo.Lista Then
                        If CType(ControlCampo, DesktopComboBoxControl).Enabled Then
                            numeroCampos += 1
                            registro.fk_Campo = Campo.Id_Campo
                            registro.fk_Documento = Campo.Documento
                            registro.fk_Expediente = Campo.Expediente
                            registro.fk_File = Campo.File
                            registro.fk_Folder = Campo.Folder
                            registro.Valor_File_Data = Utilities.DStr(CType(ControlCampo, DesktopComboBoxControl).SelectedValue)
                            registro.Conteo_File_Data = registro.Valor_File_Data.ToString().Length

                            If Campo.Tiene_Datos Then : dbmCore.SchemaProcess.TBL_File_Data.DBUpdate(registro, registro.fk_Expediente, registro.fk_Folder, registro.fk_File, registro.fk_Documento, registro.fk_Campo)
                            Else : dbmCore.SchemaProcess.TBL_File_Data.DBInsert(registro) : End If

                            FacturaPrimeraCapturaCaracter += registro.Valor_File_Data.ToString().Length
                        End If

                    ElseIf Campo.TipoCampo = DesktopConfig.CampoTipo.TablaAsociada Then
                        Dim dtTablaAsociada = dbmArchiving.SchemaCore.CTA_Tabla_Asociada.DBFindByfk_Entidadfk_Documentofk_Campo(Campo.fk_Entidad, Campo.fk_Documento, Campo.Id_Campo)

                        Dim arrIndex(dtTablaAsociada.Rows.Count - 1) As Integer
                        Dim arrCampoTabla(dtTablaAsociada.Rows.Count - 1) As Integer

                        For i = 0 To dtTablaAsociada.Rows.Count - 1
                            arrIndex(i) = dtTablaAsociada(i).fk_Campo_Tipo
                            arrCampoTabla(i) = dtTablaAsociada(i).id_Campo_Tabla
                        Next

                        Dim idAsociada As Short = 0

                        For Each fila As DataGridViewRow In CType(ControlCampo, DesktopDataGridViewControl).Rows
                            For celda = 0 To fila.Cells.Count - 1
                                If Not IsNothing(fila.Cells(celda).Value) Then
                                    numeroCampos += 1
                                    idAsociada = CShort(idAsociada + 1)
                                    registroTablaAsociada.fk_Expediente = Campo.Expediente
                                    registroTablaAsociada.fk_Folder = Campo.Folder
                                    registroTablaAsociada.fk_File = Campo.File
                                    registroTablaAsociada.fk_Documento = Campo.fk_Documento
                                    registroTablaAsociada.fk_Campo = Campo.Id_Campo
                                    registroTablaAsociada.fk_Campo_Tabla = CShort(arrCampoTabla(celda))
                                    registroTablaAsociada.id_File_Data_Asociada = idAsociada
                                    registroTablaAsociada.Conteo_File_Data = fila.Cells(celda).Value.ToString.Length

                                    Select Case arrIndex(celda)
                                        Case DesktopConfig.CampoTipo.Texto : registroTablaAsociada.Valor_File_Data = CStr(fila.Cells(celda).Value)
                                        Case DesktopConfig.CampoTipo.Numerico : registroTablaAsociada.Valor_File_Data = CStr(fila.Cells(celda).Value)
                                        Case DesktopConfig.CampoTipo.Fecha : registroTablaAsociada.Valor_File_Data = CStr(fila.Cells(celda).Value)
                                        Case DesktopConfig.CampoTipo.SiNo : registroTablaAsociada.Valor_File_Data = CBool(fila.Cells(celda).Value)
                                        Case DesktopConfig.CampoTipo.Lista : registroTablaAsociada.Valor_File_Data = CLng(fila.Cells(celda).Value)
                                    End Select

                                    dbmCore.SchemaProcess.TBL_File_Data_Asociada.DBInsert(registroTablaAsociada)
                                    FacturaPrimeraCapturaCaracter += fila.Cells(celda).Value.ToString.Length
                                End If
                            Next
                        Next
                    End If
                Next


                'Guardar las validaciones, si alguna que sea obligatoria no se cumple, se devuelve.
                For Each validacion In _Validaciones
                    registroValidacion.fk_Expediente = _tFileDetalle(0).fk_Expediente
                    registroValidacion.fk_Folder = _tFileDetalle(0).fk_Folder
                    registroValidacion.fk_File = _tFileDetalle(0).id_File
                    registroValidacion.fk_Validacion = validacion.Id_Validacion
                    registroValidacion.Respuesta = CType(Utilities.FindControl(_TableFormValidaciones, Utilities.ConvertName(validacion.Pregunta)), DesktopCheckBox.DesktopCheckBoxControl).Checked
                    registroValidacion.fk_Documento = _tFileDetalle(0).fk_Documento

                    Dim Validaciones = dbmCore.SchemaProcess.TBL_File_Validacion.DBGet(registroValidacion.fk_Expediente, registroValidacion.fk_Folder, registroValidacion.fk_File, registroValidacion.fk_Validacion, Nothing).Rows.Count
                    If Validaciones = 0 Then
                        dbmCore.SchemaProcess.TBL_File_Validacion.DBInsert(registroValidacion)
                    Else
                        dbmCore.SchemaProcess.TBL_File_Validacion.DBUpdate(registroValidacion, registroValidacion.fk_Expediente, registroValidacion.fk_Folder, registroValidacion.fk_File, registroValidacion.fk_Validacion, Nothing)
                    End If

                    If (Not CBool(registroValidacion.Respuesta)) And validacion.Obligatorio Then bFileDevuelto = True
                    If validacion.Subsanable And Not bFolderSubsanable And Not CBool(registroValidacion.Respuesta) Then bFolderSubsanable = True
                    If Not CBool(registroValidacion.Respuesta) And Not validacionNegativa Then validacionNegativa = True
                Next

                'Cambia estado del file dependiendo del tipo de captura
                'Actualiza el estado del documento
                If bFileDevuelto Or bFolderSubsanable Then

                    'If (Program.RiskGlobal.ProyectoRow.Devolucion_Carpeta) Then
                    '    Dim TipoDocumento = dbmArchiving.Schemadbo.CTA_Documento.DBFindByfk_Documento(registroValidacion.fk_Documento)
                    '    If (TipoDocumento(0).Es_Obligatorio) Then
                    '        Dim TableFolder As DBArchiving.Schemadbo.CTA_Folder_FileDataTable = Nothing

                    '        TableFolder = dbmArchiving.Schemadbo.CTA_Folder_File.DBFindByCBarras_File(_CBarras)
                    '        'TableFolder = dbmArchiving.Schemadbo.CTA_Folder_File.DBFindByCBarras_Folder(_CBarras)
                    '        If (TableFolder(0).fk_Registro_Tipo = 1) Then
                    '            Utilities.ActualizaEstadoFile(dbmArchiving, dbmCore, _CBarras, Nothing, Nothing, DesktopConfig.Modulo.Archiving, DBCore.EstadoEnum.Reproceso, Program.Sesion.Usuario.id, Nothing, DesktopConfig.MotivoReproceso.ValidacionesObligatorias, Nothing, CInt(Program.Sesion.Parameter("_idLineaProceso")))
                    '        Else
                    '            Utilities.ActualizaEstadoFileCarpeta(dbmArchiving, dbmCore, _CBarras, Nothing, Nothing, DesktopConfig.Modulo.Archiving, DBCore.EstadoEnum.Reproceso, Program.Sesion.Usuario.id, Nothing, DesktopConfig.MotivoReproceso.ValidacionesObligatorias, Nothing, CInt(Program.Sesion.Parameter("_idLineaProceso")))
                    '        End If

                    '    Else
                    '        Utilities.ActualizaEstadoFile(dbmArchiving, dbmCore, _CBarras, Nothing, Nothing, DesktopConfig.Modulo.Archiving, DBCore.EstadoEnum.Reproceso, Program.Sesion.Usuario.id, Nothing, DesktopConfig.MotivoReproceso.ValidacionesObligatorias, Nothing, CInt(Program.Sesion.Parameter("_idLineaProceso")))
                    '    End If
                    'Else
                    '    Utilities.ActualizaEstadoFile(dbmArchiving, dbmCore, _CBarras, Nothing, Nothing, DesktopConfig.Modulo.Archiving, DBCore.EstadoEnum.Reproceso, Program.Sesion.Usuario.id, Nothing, DesktopConfig.MotivoReproceso.ValidacionesObligatorias, Nothing, CInt(Program.Sesion.Parameter("_idLineaProceso")))
                    'End If

                    Utilities.ActualizaEstadoFile(dbmArchiving, dbmCore, _CBarras, Nothing, Nothing, DesktopConfig.Modulo.Archiving, DBCore.EstadoEnum.Reproceso, Program.Sesion.Usuario.id, Nothing, DesktopConfig.MotivoReproceso.ValidacionesObligatorias, Nothing, CInt(Program.Sesion.Parameter("_idLineaProceso")))
                Else
                    Dim camposDocumento = dbmArchiving.Schemadbo.CTA_Campos_Documentos.DBFindByCBarras_Filefk_Entidadfk_ProyectoUsa_CapturaUsa_Doble_CapturaEs_Campo_Destape(CBarrasLabel.Text, Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, Nothing, True, False)
                    Dim viewPrimeraCaptura = camposDocumento.DefaultView
                    viewPrimeraCaptura.RowFilter = camposDocumento.fk_EstadoColumn.ColumnName & "=" & CStr(DBCore.EstadoEnum.Mesa_de_Control)

                    If (viewPrimeraCaptura.ToTable().Rows.Count = 0) And (Not _tFileDetalle(0).Folios_Doble_Captura) And (Not _tFileDetalle(0).Monto_Doble_Captura) Then

                        'Valida que el centro de procesamiento del proyecto sea igual al centro de procesamiento del equipo
                        Dim SedeCustodia As Short = -1
                        If Not Program.RiskGlobal.SedeCustodia.IsNull Then SedeCustodia = CShort(Program.RiskGlobal.SedeCustodia)

                        Dim Estado = DBCore.EstadoEnum.Centro_Distribucion
                        If CShort(Program.RiskGlobal.EntidadCustodia) = Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad And SedeCustodia = Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede Then Estado = DBCore.EstadoEnum.Empaque

                        Utilities.ActualizaEstadoFile(dbmArchiving, dbmCore, _CBarras, _typeArchivingFile.fk_OT, Nothing, DesktopConfig.Modulo.Archiving, Estado, Program.Sesion.Usuario.id, Nothing, Nothing, Nothing, Nothing)
                    Else
                        Utilities.ActualizaEstadoFile(dbmArchiving, dbmCore, _CBarras, _typeArchivingFile.fk_OT, Nothing, DesktopConfig.Modulo.Archiving, DBCore.EstadoEnum.Segunda_Captura, Program.Sesion.Usuario.id, Nothing, Nothing, Nothing, Nothing)
                    End If

                    ' Marca el folder como no subsanable para impedir el proceso de Endoso con manejo de fondeadores
                    If validacionNegativa And Not bFolderSubsanable Then
                        Dim RiskFolder As New DBArchiving.SchemaRisk.TBL_FolderType
                        RiskFolder.Impide_Endoso = True
                        dbmArchiving.SchemaRisk.TBL_Folder.DBUpdate(RiskFolder, _tFileDetalle(0).fk_Expediente, _tFileDetalle(0).fk_Folder, _typeArchivingFile.fk_OT)
                    End If

                End If


                    'Facturación Campos Documento, cantidad caracteres y validaciones
                    Utilities.AgregarMovimiento(dbmArchiving, Program.Sesion.Entidad.id, _tFileDetalle(0).fk_Esquema_Facturacion, DesktopConfig.Servicio_Facturacion.Primera_Captura_Caracter, Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, _tFileDetalle(0).fk_esquema, FacturaPrimeraCapturaCaracter, Program.Sesion.Usuario.id, Program.DesktopGlobal.CentroProcesamientoRow)
                    Utilities.AgregarMovimiento(dbmArchiving, Program.Sesion.Entidad.id, _tFileDetalle(0).fk_Esquema_Facturacion, DesktopConfig.Servicio_Facturacion.Primera_Captura_Campo, Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, _tFileDetalle(0).fk_esquema, numeroCampos, Program.Sesion.Usuario.id, Program.DesktopGlobal.CentroProcesamientoRow)
                    Utilities.AgregarMovimiento(dbmArchiving, Program.Sesion.Entidad.id, _tFileDetalle(0).fk_Esquema_Facturacion, DesktopConfig.Servicio_Facturacion.Primera_Captura_Validacion, Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, _tFileDetalle(0).fk_esquema, _Validaciones.Count, Program.Sesion.Usuario.id, Program.DesktopGlobal.CentroProcesamientoRow)


                    dbmCore.Transaction_Commit()
                    dbmArchiving.Transaction_Commit()

                    Me.Close()

            Catch ex As Exception
                Try : dbmCore.Transaction_Rollback() : Catch : End Try
                Try : dbmArchiving.Transaction_Rollback() : Catch : End Try
                DesktopMessageBoxControl.DesktopMessageShow("GuardarDatos", ex)

            Finally
                dbmArchiving.Connection_Close()
                dbmCore.Connection_Close()
            End Try

        End Sub

        Private Sub ConfiguraFoliosMonto()
            Try
                If (Program.RiskGlobal.ProyectoRow.Captura_Folios_Destape) And (Program.RiskGlobal.ProyectoRow.Captura_Monto_Destape) Then
                    FoliosMontoGroupBox.Visible = False
                    BuscaSiguienteControl()
                Else
                    If Program.RiskGlobal.ProyectoRow.Captura_Folios_Destape Then
                        FoliosLabel.Visible = False
                        FoliosDesktopTextBox.Visible = False
                        MontoLabel.Focus()
                    End If

                    If Program.RiskGlobal.ProyectoRow.Captura_Monto_Destape Then
                        MontoLabel.Visible = False
                        MontoDesktopTextBox.Visible = False
                        BuscaSiguienteControl()
                    End If
                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("ConfiguraFoliosMonto", ex)
            End Try
        End Sub

        Private Sub BuscaSiguienteControl()
            Try
                If CamposPanel.Controls.Count = 0 Then
                    If ValidacionesPanel.Controls.Count = 0 Then
                        AceptarButton.Focus()
                    Else
                        Dim LayoutCampos = CType(ValidacionesPanel.Controls(0), TableLayoutPanel)
                        For Each campo In LayoutCampos.Controls
                            Select Case campo.GetType().ToString()
                                Case "Miharu.Desktop.Controls.DesktopCheckBox.DesktopCheckBoxControl"
                                    CType(campo, CheckBox).Focus()
                                    Exit For
                            End Select
                        Next
                    End If
                Else
                    Dim LayoutCampos = CType(CamposPanel.Controls(0), TableLayoutPanel)
                    For Each campo In LayoutCampos.Controls
                        Select Case campo.GetType().ToString()
                            Case "Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl"
                                CType(campo, DesktopTextBoxControl).Focus()
                                Exit For

                            Case "Miharu.Desktop.Controls.DesktopDataGridView.DesktopDataGridViewControl"
                                CType(campo, DesktopDataGridViewControl).Focus()
                                CType(campo, DesktopDataGridViewControl).Rows(0).Cells(0).Selected = True
                                Exit For

                            Case "Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl"
                                CType(campo, DesktopComboBoxControl).Focus()
                                Exit For
                        End Select
                    Next
                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("BuscaSiguienteControl", ex)
            End Try
        End Sub

        Private Sub CamposTirgger(ByVal fk_Documento As Integer, ByVal fk_Campo As Integer, ByVal valor As String)
            Dim Tbl_TriggersDataTable = _TriggersDataTable.Select("fk_Documento = " & fk_Documento & "AND fk_Campo_Trigger = " & fk_Campo)
            Dim Tbl_TriggersValidacionesDataTable = _TriggersValidacionesDataTable.Select("fk_Documento = " & fk_Documento & "AND fk_Campo_Trigger = " & fk_Campo)

            If Tbl_TriggersDataTable.Length > 0 Then

                For Each Trigger As DBArchiving.SchemaConfig.TBL_Campo_TriggerRow In Tbl_TriggersDataTable
                    MostrarCampoTrigger(Trigger.fk_Documento, Trigger.fk_Campo_Ocultar, 0, CBool(IIf(valor = Trigger.Valor, False, True)))
                Next

            End If

            If Tbl_TriggersValidacionesDataTable.Length > 0 Then
                For Each Trigger As DBArchiving.SchemaConfig.TBL_Campo_Trigger_ValidacionRow In Tbl_TriggersValidacionesDataTable

                    Select Case Trigger.Operador_Validacion
                        Case "="
                            MostrarCampoTrigger(Trigger.fk_Documento, 0, Trigger.fk_Validacion_Ocultar, CBool(IIf(valor = Trigger.Valor, False, True)))
                        Case "<"
                            MostrarCampoTrigger(Trigger.fk_Documento, 0, Trigger.fk_Validacion_Ocultar, CBool(IIf(valor < Trigger.Valor, False, True)))
                        Case ">"
                            MostrarCampoTrigger(Trigger.fk_Documento, 0, Trigger.fk_Validacion_Ocultar, CBool(IIf(valor > Trigger.Valor, False, True)))
                        Case "<="
                            MostrarCampoTrigger(Trigger.fk_Documento, 0, Trigger.fk_Validacion_Ocultar, CBool(IIf(valor <= Trigger.Valor, False, True)))
                        Case ">="
                            MostrarCampoTrigger(Trigger.fk_Documento, 0, Trigger.fk_Validacion_Ocultar, CBool(IIf(valor >= Trigger.Valor, False, True)))
                        Case "<>"
                            MostrarCampoTrigger(Trigger.fk_Documento, 0, Trigger.fk_Validacion_Ocultar, CBool(IIf(valor <> Trigger.Valor, False, True)))
                        Case Else
                            MostrarCampoTrigger(Trigger.fk_Documento, 0, Trigger.fk_Validacion_Ocultar, True)
                    End Select
                Next
            End If


        End Sub

        Private Sub MostrarCampoTrigger(ByVal fk_Documento As Integer, ByVal fk_Campo As Integer, ByVal fk_Validacion As Integer, ByVal Visible As Boolean)

            For Each panel As Panel In CamposPanel.Controls

                For Each Objeto In panel.Controls
                    Select Case Objeto.GetType()
                        Case GetType(DesktopTextBoxControl)
                            Dim Ctrl As DesktopTextBoxControl = CType(Objeto, DesktopTextBoxControl)
                            If Ctrl.fk_Documento = fk_Documento And Ctrl.fk_Campo = fk_Campo Then
                                Ctrl.Visible = Visible
                            End If

                        Case GetType(DesktopComboBoxControl)
                            Dim Ctrl As DesktopComboBoxControl = CType(Objeto, DesktopComboBoxControl)
                            If Ctrl.fk_Documento = fk_Documento And Ctrl.fk_Campo = fk_Campo Then
                                Ctrl.Visible = Visible
                            End If

                        Case GetType(DesktopCheckBoxControl)
                            Dim Ctrl As DesktopCheckBoxControl = CType(Objeto, DesktopCheckBoxControl)
                            If Ctrl.fk_Documento = fk_Documento And Ctrl.fk_Campo = fk_Campo Then
                                Ctrl.Visible = Visible
                            End If
                    End Select
                Next



            Next


            For Each panel As Panel In ValidacionesPanel.Controls

                For Each Objeto In panel.Controls
                    Select Case Objeto.GetType()
                        Case GetType(DesktopTextBoxControl)
                            Dim Ctrl As DesktopTextBoxControl = CType(Objeto, DesktopTextBoxControl)
                            If Ctrl.fk_Documento = fk_Documento And Ctrl.fk_Validacion = fk_Validacion Then
                                Ctrl.Visible = Visible
                            End If

                        Case GetType(DesktopComboBoxControl)
                            Dim Ctrl As DesktopComboBoxControl = CType(Objeto, DesktopComboBoxControl)
                            If Ctrl.fk_Documento = fk_Documento And Ctrl.fk_Validacion = fk_Validacion Then
                                Ctrl.Visible = Visible
                            End If

                        Case GetType(DesktopCheckBoxControl)
                            Dim Ctrl As DesktopCheckBoxControl = CType(Objeto, DesktopCheckBoxControl)
                            If Ctrl.fk_Documento = fk_Documento And Ctrl.fk_Validacion = fk_Validacion Then
                                Ctrl.Visible = Visible
                            End If
                    End Select
                Next


            Next
        End Sub


#End Region

#Region " Funciones "

        Public Function ValidarData() As Boolean
            Dim dbmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            Dim bReturn As Boolean = True
            Try
                For Each Campo As CamposDataAdicional In _Campos
                    Dim valor As String
                    Dim NombreControl As String = Utilities.ConvertName(Campo.NombreCampo)
                    Dim ControlCampo As Control = Utilities.FindControl(_TableForm, NombreControl)

                    If Campo.TipoCampo = DesktopConfig.CampoTipo.Texto Or Campo.TipoCampo = DesktopConfig.CampoTipo.Fecha Or Campo.TipoCampo = DesktopConfig.CampoTipo.Numerico Then
                        valor = CType(ControlCampo, DesktopTextBoxControl).Text
                        If (Campo.CampoObligatorio And (valor = "" Or (Slyg.Tools.DataConvert.IsNumeric(valor) AndAlso Math.Abs(Slyg.Tools.DataConvert.ToNumericD(valor, ".") - 0.0) < 0))) Then
                            CType(ControlCampo, DesktopTextBoxControl).BackColor = Drawing.Color.Yellow
                            bReturn = False
                        End If

                    ElseIf Campo.TipoCampo = DesktopConfig.CampoTipo.TablaAsociada Then
                        dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)
                        Dim dtTablaAsociada = dbmArchiving.SchemaCore.CTA_Tabla_Asociada.DBFindByfk_Entidadfk_Documentofk_Campo(Campo.fk_Entidad, Campo.fk_Documento, Campo.Id_Campo)
                        dbmArchiving.Connection_Close()

                        Dim bTipoDatoValido As Boolean = True
                        Dim sbErrores As New StringBuilder


                        Dim arrIndex(dtTablaAsociada.Rows.Count - 1) As Integer
                        For i = 0 To dtTablaAsociada.Rows.Count - 1
                            arrIndex(i) = dtTablaAsociada(i).fk_Campo_Tipo
                        Next

                        For Each fila As DataGridViewRow In CType(ControlCampo, DesktopDataGridViewControl).Rows
                            For celda = 0 To fila.Cells.Count - 1
                                If Not IsNothing(fila.Cells(celda).Value) Then
                                    Select Case arrIndex(celda)
                                        Case DesktopConfig.CampoTipo.Texto
                                            bTipoDatoValido = (fila.Cells(celda).Value.ToString() <> "")
                                            If Not bTipoDatoValido Then sbErrores.AppendLine("Fila: " & (fila.Index + 1).ToString() & ", Columna: " & (celda + 1).ToString() & ": Se esperaba tipo TEXTO.")

                                        Case DesktopConfig.CampoTipo.Numerico
                                            bTipoDatoValido = IsNumeric(fila.Cells(celda).Value)
                                            If Not bTipoDatoValido Then sbErrores.AppendLine("Fila: " & (fila.Index + 1).ToString() & ", Columna: " & (celda + 1).ToString() & ": Se esperaba tipo NÚMERO.")

                                        Case DesktopConfig.CampoTipo.Fecha
                                            Dim arrDate = fila.Cells(celda).Value.ToString().Split(CChar(" "))
                                            bTipoDatoValido = IsDate(arrDate(0))
                                            If Not bTipoDatoValido Then sbErrores.AppendLine("Fila: " & (fila.Index + 1).ToString() & ", Columna: " & (celda + 1).ToString() & ": Se esperaba tipo FECHA.")
                                    End Select
                                End If
                            Next
                        Next
                        If Not bTipoDatoValido Or sbErrores.ToString() <> "" Then
                            bReturn = bTipoDatoValido
                            DesktopMessageBoxControl.DesktopMessageShow("Por favor corregir los siguientes errores:" & vbCrLf & sbErrores.ToString(), "Data Asociada", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                        End If
                    End If
                Next

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("ValidarData", ex)
            End Try
            Return bReturn
        End Function

#End Region

    End Class
End Namespace