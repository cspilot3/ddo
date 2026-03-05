Imports System.Text
Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopDataGridView
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Controls.DesktopTextBox
Imports Miharu.Desktop.Controls.DesktopCheckBox
Imports Miharu.Desktop.Controls.DesktopComboBox
Imports Miharu.Desktop.Controls
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Config
Imports DBArchiving
Imports Slyg.Tools

Namespace Forms.MesaControlFisicos

    Public Class FormPrimeraCapturarDataAdicionalDevolucion
        Inherits FormBase

#Region " Declaraciones "

        Private _CBarras As String
        Private _TableFormValidaciones As TableLayoutPanel
        Private _TableForm As TableLayoutPanel
        Private Campos As New List(Of CamposDataAdicional)
        Private CamposV As New List(Of CamposValidaciones)

        Dim _typeCoreFile As New DBCore.SchemaProcess.TBL_FileType
        Dim _typeArchivingFile As New DBArchiving.SchemaRisk.TBL_FileType

        Dim tFileArchiving As DBArchiving.Schemadbo.CTA_FileDataTable

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
            Public Valor_File_Data As Object
        End Structure

        Structure CamposValidaciones
            Public Documento As Integer
            Public Id_Validacion As Short
            Public Pregunta As String
            Public Obligatorio As Boolean
            Public Subsanable As Boolean
            Public Respuesta As Boolean
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
            Dim MeForm As New FormPrimeraCapturarDataAdicionalDevolucion(CBarras)
            MeForm.ShowDialog()
        End Sub

#End Region

#Region " Eventos "

        Private Sub FormCapturarDataAdicional_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            Try
                setValoresCaptura()
                CreaCampos()
                crearValidaciones()
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("FormCapturarDataAdicional_Load", ex)
            End Try
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
                        End Select
                    Next
                End If

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

        Private Sub setValoresCaptura()
            Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
            Dim dbmArchiving As New DBArchiving.DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            Try
                'Core.File
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                Dim tFilecore = dbmCore.SchemaProcess.TBL_File.DBFindByCBarras_File(_CBarras)
                _typeCoreFile.fk_Expediente = tFilecore(0).fk_Expediente
                _typeCoreFile.fk_Folder = tFilecore(0).fk_Folder
                _typeCoreFile.id_File = tFilecore(0).id_File
                _typeCoreFile.fk_Documento = tFilecore(0).fk_Documento
                _typeCoreFile.Folios_File = tFilecore(0).Folios_File
                _typeCoreFile.Monto_File = tFilecore(0).Monto_File
                _typeCoreFile.CBarras_File = tFilecore(0).CBarras_File

                dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)
                tFileArchiving = dbmArchiving.Schemadbo.CTA_File.DBFindByCBarras_Filefk_Estado(_CBarras, DBCore.EstadoEnum.Mesa_de_Control)
                _typeArchivingFile.fk_OT = tFileArchiving(0).fk_OT
                _typeArchivingFile.fk_Folder = tFileArchiving(0).fk_Folder
                _typeArchivingFile.id_File = tFileArchiving(0).id_File
                _typeArchivingFile.fk_expediente = tFileArchiving(0).fk_Expediente
                _typeArchivingFile.fk_Estado = tFileArchiving(0).fk_Estado
                _typeArchivingFile.fk_Caja_Proceso = tFileArchiving(0).fk_Caja_Proceso
                _typeArchivingFile.fk_Registro_Tipo = tFileArchiving(0).fk_Registro_Tipo
                _typeArchivingFile.Es_sobrante = tFileArchiving(0).Es_sobrante
                _typeArchivingFile.Impreso = tFileArchiving(0).Impreso

                _TriggersDataTable = dbmArchiving.SchemaConfig.TBL_Campo_Trigger.DBGet(DBArchiving.EnumEtapaCaptura.Mesa_Control, CShort(tFileArchiving(0).fk_Documento), Nothing, Nothing, Nothing, Nothing)
                _TriggersValidacionesDataTable = dbmArchiving.SchemaConfig.TBL_Campo_Trigger_Validacion.DBGet(DBArchiving.EnumEtapaCaptura.Mesa_Control, CShort(tFileArchiving(0).fk_Documento), Nothing, Nothing, Nothing)

                dbmArchiving.Connection_Close()

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("setValoresCaptura", ex)
            Finally
                dbmCore.Connection_Close()
            End Try
        End Sub

        Public Sub CreaCampos()
            EntidadLabel.Text = Program.RiskGlobal.NombreEntidad
            ProyectoLabel.Text = Program.RiskGlobal.NombreProyecto
            CBarrasLabel.Text = _CBarras

            Dim dbmArchiving As New DBArchiving.DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

            Dim TableFile = dbmArchiving.Schemadbo.CTA_File_Detalle.DBFindByCBarras_Filefk_entidadfk_proyectofk_Estado(_CBarras, Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, DBCore.EstadoEnum.Mesa_de_Control)

            If TableFile.Rows.Count = 0 Then
                DesktopMessageBoxControl.DesktopMessageShow("El código de barras no se encuentra en la base de datos, puede ser que no tenga campos parametrizados o que no exista para el proyecto, por favor intente con otro.", "No se ha encontrado el código de barras", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                Me.Close()
            Else
                EsquemaLabel.Text = TableFile.Rows(0)(TableFile.fk_esquemaColumn).ToString & " - " & TableFile.Rows(0)(TableFile.Nombre_esquemaColumn).ToString
                TipologiaLabel.Text = TableFile.Rows(0)(TableFile.id_TipologiaColumn).ToString & " - " & TableFile.Rows(0)(TableFile.Nombre_TipologiaColumn).ToString
                CapturaLabel.Text = "Primera Captura"

                Try
                    Dim TableCampos = dbmArchiving.Schemadbo.CTA_Campos_Documentos.DBFindByUsa_CapturaUsa_Doble_CapturaCBarras_Filefk_Estadofk_Entidadfk_Proyecto(True, Nothing, _CBarras, DBCore.EstadoEnum.Mesa_de_Control, Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto)

                    If TableCampos.Count > 0 Then
                        Dim TableCamposFilter = TableCampos.DefaultView.ToTable(True, "Nombre_Campo", "Id_Campo", "fk_Campo_Tipo", "fk_Campo_Lista", "Es_Obligatorio_Campo", "Length_Campo", "Existe_File_Data", "Usa_Captura", "Usa_Doble_Captura", "CBarras_File", "CBarras_Folder", "fk_Estado", "fk_Entidad", "fk_Proyecto", "fk_Documento", "fk_Expediente", "id_File", "fk_Folder", "Valor_File_Data", "fk_Esquema", "Folios_Doble_Captura", "Monto_Doble_Captura")

                        Campos = New List(Of CamposDataAdicional)
                        Dim CampoControlCollection As New List(Of DesktopConfig.CrearControlesType)

                        For Each Row As DataRow In TableCamposFilter.Rows
                            Dim campo As New CamposDataAdicional
                            Dim CampoControl As New DesktopConfig.CrearControlesType

                            campo.Id_Campo = CShort(Row(DBArchiving.Schemadbo.CTA_Campos_DocumentosEnum.id_Campo.ColumnName))
                            campo.LongitudCampo = CShort(Row(DBArchiving.Schemadbo.CTA_Campos_DocumentosEnum.Length_Campo.ColumnName))
                            campo.NombreCampo = CStr(Row(DBArchiving.Schemadbo.CTA_Campos_DocumentosEnum.Nombre_Campo.ColumnName))
                            campo.CampoObligatorio = CBool(Row(DBArchiving.Schemadbo.CTA_Campos_DocumentosEnum.Es_Obligatorio_Campo.ColumnName))
                            campo.CampoLista = CShort(Row(DBArchiving.Schemadbo.CTA_Campos_DocumentosEnum.fk_Campo_Lista.ColumnName))
                            campo.TipoCampo = CType(Row(DBArchiving.Schemadbo.CTA_Campos_DocumentosEnum.fk_Campo_Tipo.ColumnName), DesktopConfig.CampoTipo)
                            campo.Documento = CShort(Row(DBArchiving.Schemadbo.CTA_Campos_DocumentosEnum.fk_Documento.ColumnName))
                            campo.Expediente = CLng(Row(DBArchiving.Schemadbo.CTA_Campos_DocumentosEnum.fk_Expediente.ColumnName))
                            campo.File = CShort(Row(DBArchiving.Schemadbo.CTA_Campos_DocumentosEnum.id_File.ColumnName))
                            campo.Folder = CShort(Row(DBArchiving.Schemadbo.CTA_Campos_DocumentosEnum.fk_Folder.ColumnName))
                            'Para Tabla Asociada
                            campo.fk_Entidad = Program.RiskGlobal.Entidad
                            campo.fk_Documento = CShort(Row(DBArchiving.Schemadbo.CTA_Campos_DocumentosEnum.fk_Documento.ColumnName))
                            campo.fk_Esquema = CShort(Row(DBArchiving.Schemadbo.CTA_Campos_DocumentosEnum.fk_Esquema.ColumnName))
                            campo.Valor_File_Data = Row(DBArchiving.Schemadbo.CTA_Campos_DocumentosEnum.Valor_File_Data.ColumnName)
                            Campos.Add(campo)

                            CampoControl.CampoLista = CShort(Row(DBArchiving.Schemadbo.CTA_Campos_DocumentosEnum.fk_Campo_Lista.ColumnName))
                            CampoControl.Label = CStr(Row(DBArchiving.Schemadbo.CTA_Campos_DocumentosEnum.Nombre_Campo.ColumnName))
                            CampoControl.MaxLength = CShort(Row(DBArchiving.Schemadbo.CTA_Campos_DocumentosEnum.Length_Campo.ColumnName))
                            CampoControl.NombreCampo = CStr(Row(DBArchiving.Schemadbo.CTA_Campos_DocumentosEnum.Nombre_Campo.ColumnName))
                            CampoControl.Tipo = CType(Row(DBArchiving.Schemadbo.CTA_Campos_DocumentosEnum.fk_Campo_Tipo.ColumnName), DesktopConfig.CampoTipo)
                            CampoControl.Width = 200
                            CampoControl.LabelWidth = 200
                            CampoControl.Existe_File_Data = CBool(Row(DBArchiving.Schemadbo.CTA_Campos_DocumentosEnum.Existe_File_Data.ColumnName))
                            CampoControl.File_Data = Row(DBArchiving.Schemadbo.CTA_Campos_DocumentosEnum.Valor_File_Data.ColumnName)
                            'Para Tabla asociada
                            CampoControl.fk_Entidad = Program.RiskGlobal.Entidad
                            CampoControl.fk_Documento = CShort(Row(DBArchiving.Schemadbo.CTA_Campos_DocumentosEnum.fk_Documento.ColumnName))
                            CampoControl.fk_Campo = CShort(Row(DBArchiving.Schemadbo.CTA_Campos_DocumentosEnum.id_Campo.ColumnName))
                            CampoControl.Obligatorio = CBool(Row(DBArchiving.Schemadbo.CTA_Campos_DocumentosEnum.Es_Obligatorio_Campo.ColumnName))

                            CampoControlCollection.Add(CampoControl)
                        Next

                        _TableForm = Utilities.CreaControles(dbmArchiving, CampoControlCollection, Program.DesktopGlobal.ConnectionStrings, Program.RiskGlobal, False, CamposPanel.Width)
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
            End If

            dbmArchiving.Connection_Close()
        End Sub

        Public Sub crearValidaciones()
            Dim dbmArchiving As New DBArchiving.DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

            Try
                Dim CBarrasDocTable = dbmArchiving.Schemadbo.CTA_Folder_File.DBFindByfk_EstadoCBarras_File(DBCore.EstadoEnum.Mesa_de_Control, _CBarras)
                _TableFormValidaciones = New TableLayoutPanel()

                Dim CampoControl As New DesktopConfig.CrearControlesType
                Dim CampoControlCollection As New List(Of DesktopConfig.CrearControlesType)
                'Dim TableValidaciones = dbmArchiving.SchemaCore.CTA_Validacion.DBFindByfk_Documento(CInt(CBarrasDocTable.Rows(0)(CBarrasDocTable.fk_DocumentoColumn)))
                Dim TableValidaciones = dbmArchiving.SchemaCore.CTA_Validacion_Respuesta.DBFindByfk_Documentofk_Expedientefk_Folderfk_File(CBarrasDocTable(0).fk_Documento, CBarrasDocTable(0).fk_Expediente, CBarrasDocTable(0).id_Folder, CBarrasDocTable(0).id_File)

                CamposV = New List(Of CamposValidaciones)

                'For Each Validacion As DBArchiving.SchemaCore.CTA_ValidacionRow In TableValidaciones.Rows
                For Each Validacion As DBArchiving.SchemaCore.CTA_Validacion_RespuestaRow In TableValidaciones.Rows
                    Dim campo As New CamposValidaciones
                    campo.Id_Validacion = Validacion.id_Validacion
                    campo.Documento = Validacion.fk_Documento
                    campo.Pregunta = Validacion.Pregunta_Validacion
                    campo.Obligatorio = Validacion.Obligatorio
                    campo.Subsanable = Validacion.Es_Subsanable
                    campo.Respuesta = Validacion.Respuesta
                    CamposV.Add(campo)

                    CampoControl.Label = Validacion.Pregunta_Validacion
                    CampoControl.NombreCampo = Validacion.Pregunta_Validacion
                    CampoControl.Tipo = DesktopConfig.CampoTipo.SiNo
                    CampoControl.LabelWidth = 200
                    CampoControlCollection.Add(CampoControl)
                    CampoControl.fk_Documento = Validacion.fk_Documento
                    CampoControl.fk_Validacion = Validacion.id_Validacion

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
            Dim dbmArchiving As New DBArchiving.DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)

            Dim nInconsistencias As Integer = 0

            Try
                dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                'Guarda el registro en Core.File
                dbmCore.Transaction_Begin()

                If CInt(_typeCoreFile.Folios_File) <> CInt(IIf(FoliosDesktopTextBox.Text <> "", FoliosDesktopTextBox.Text, 0)) Then
                    dbmArchiving.Transaction_Begin()
                    Dim tInconsistencia As New DBArchiving.SchemaRisk.TBL_InconsistenciaType
                    tInconsistencia.fk_OT = _typeArchivingFile.fk_OT
                    tInconsistencia.fk_expediente = _typeArchivingFile.fk_expediente
                    tInconsistencia.fk_folder = _typeArchivingFile.fk_Folder
                    tInconsistencia.fk_file = _typeArchivingFile.id_File
                    tInconsistencia.id_inconsistencia = dbmArchiving.SchemaRisk.TBL_Inconsistencia.DBNextId(_typeArchivingFile.fk_OT, _typeArchivingFile.fk_expediente, _typeArchivingFile.fk_Folder, _typeArchivingFile.id_File)
                    tInconsistencia.Valor_Anterior = _typeCoreFile.Folios_File
                    tInconsistencia.Valor_Nuevo = CInt(IIf(FoliosDesktopTextBox.Text <> "", FoliosDesktopTextBox.Text, 0))
                    tInconsistencia.fk_Tipo_Inconsistencia = DesktopConfig.Tipo_Inconsistencia.Campo_Inconsistente
                    dbmArchiving.SchemaRisk.TBL_Inconsistencia.DBInsert(tInconsistencia)
                    dbmArchiving.Transaction_Commit()

                    nInconsistencias += 1
                End If

                If _typeCoreFile.Monto_File.Value <> CDec(IIf(MontoDesktopTextBox.Text <> "", MontoDesktopTextBox.Text, 0)) Then
                    dbmArchiving.Transaction_Begin()
                    Dim tInconsistencia As New DBArchiving.SchemaRisk.TBL_InconsistenciaType
                    tInconsistencia.fk_OT = _typeArchivingFile.fk_OT
                    tInconsistencia.fk_expediente = _typeArchivingFile.fk_expediente
                    tInconsistencia.fk_folder = _typeArchivingFile.fk_Folder
                    tInconsistencia.fk_file = _typeArchivingFile.id_File
                    tInconsistencia.id_inconsistencia = dbmArchiving.SchemaRisk.TBL_Inconsistencia.DBNextId(_typeArchivingFile.fk_OT, _typeArchivingFile.fk_expediente, _typeArchivingFile.fk_Folder, _typeArchivingFile.id_File)
                    tInconsistencia.Valor_Anterior = _typeCoreFile.Monto_File
                    tInconsistencia.Valor_Nuevo = CDec(IIf(MontoDesktopTextBox.Text <> "", MontoDesktopTextBox.Text, 0))
                    tInconsistencia.fk_Tipo_Inconsistencia = DesktopConfig.Tipo_Inconsistencia.Campo_Inconsistente
                    dbmArchiving.SchemaRisk.TBL_Inconsistencia.DBInsert(tInconsistencia)
                    dbmArchiving.Transaction_Commit()

                    nInconsistencias += 1
                End If

                _typeCoreFile.Folios_File = CShort(IIf(FoliosDesktopTextBox.Text <> "", FoliosDesktopTextBox.Text, 0))
                _typeCoreFile.Monto_File = CDec(IIf(MontoDesktopTextBox.Text <> "", MontoDesktopTextBox.Text, 0))
                dbmCore.SchemaProcess.TBL_File.DBUpdate(_typeCoreFile, _typeCoreFile.fk_Expediente, _typeCoreFile.fk_Folder, _typeCoreFile.id_File)

                'Facturación Caractéres Campos
                Utilities.AgregarMovimiento(dbmArchiving, Program.Sesion.Entidad.id, tFileArchiving(0).fk_Esquema_Facturacion, DesktopConfig.Servicio_Facturacion.Primera_Captura_Devolucion_Caracter, Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, tFileArchiving(0).fk_Esquema, _typeCoreFile.Folios_File.ToString().Length, Program.Sesion.Usuario.id, Program.DesktopGlobal.CentroProcesamientoRow)
                Utilities.AgregarMovimiento(dbmArchiving, Program.Sesion.Entidad.id, tFileArchiving(0).fk_Esquema_Facturacion, DesktopConfig.Servicio_Facturacion.Primera_Captura_Devolucion_Caracter, Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, tFileArchiving(0).fk_Esquema, _typeCoreFile.Monto_File.ToString().Length, Program.Sesion.Usuario.id, Program.DesktopGlobal.CentroProcesamientoRow)

                'Guarda la captura de datos en Core.File_Data
                Dim registro As New DBCore.SchemaProcess.TBL_File_DataType
                Dim numeroCampos As Integer = 2 'Inicia en dos apar tener en cuenta Folios y Monto

                For Each Campo As CamposDataAdicional In Campos
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

                            If IsDBNull(Campo.Valor_File_Data) Then
                                Campo.Valor_File_Data = Nothing
                            End If

                            Select Case Campo.TipoCampo
                                Case DesktopConfig.CampoTipo.Fecha
                                    If CDate(Campo.Valor_File_Data) <> Utilities.DDate(CType(ControlCampo, DesktopTextBoxControl).Fecha) Then
                                        dbmArchiving.Transaction_Begin()
                                        Dim tInconsistencia As New DBArchiving.SchemaRisk.TBL_InconsistenciaType
                                        tInconsistencia.fk_OT = _typeArchivingFile.fk_OT
                                        tInconsistencia.fk_expediente = _typeArchivingFile.fk_expediente
                                        tInconsistencia.fk_folder = _typeArchivingFile.fk_Folder
                                        tInconsistencia.fk_file = _typeArchivingFile.id_File
                                        tInconsistencia.id_inconsistencia = dbmArchiving.SchemaRisk.TBL_Inconsistencia.DBNextId(_typeArchivingFile.fk_OT, _typeArchivingFile.fk_expediente, _typeArchivingFile.fk_Folder, _typeArchivingFile.id_File)
                                        tInconsistencia.Valor_Anterior = CDate(Campo.Valor_File_Data)
                                        tInconsistencia.Valor_Nuevo = Utilities.DDate(CType(ControlCampo, DesktopTextBoxControl).Fecha)
                                        tInconsistencia.fk_Tipo_Inconsistencia = DesktopConfig.Tipo_Inconsistencia.Campo_Inconsistente
                                        dbmArchiving.SchemaRisk.TBL_Inconsistencia.DBInsert(tInconsistencia)
                                        dbmArchiving.Transaction_Commit()

                                        nInconsistencias += 1
                                    End If
                                    registro.Valor_File_Data = Utilities.DDate(CType(ControlCampo, DesktopTextBoxControl).Fecha)

                                Case DesktopConfig.CampoTipo.Texto
                                    If CStr(Campo.Valor_File_Data) <> Utilities.DStr(CType(ControlCampo, DesktopTextBoxControl).Text) Then
                                        dbmArchiving.Transaction_Begin()
                                        Dim tInconsistencia As New DBArchiving.SchemaRisk.TBL_InconsistenciaType
                                        tInconsistencia.fk_OT = _typeArchivingFile.fk_OT
                                        tInconsistencia.fk_expediente = _typeArchivingFile.fk_expediente
                                        tInconsistencia.fk_folder = _typeArchivingFile.fk_Folder
                                        tInconsistencia.fk_file = _typeArchivingFile.id_File
                                        tInconsistencia.id_inconsistencia = dbmArchiving.SchemaRisk.TBL_Inconsistencia.DBNextId(_typeArchivingFile.fk_OT, _typeArchivingFile.fk_expediente, _typeArchivingFile.fk_Folder, _typeArchivingFile.id_File)
                                        tInconsistencia.Valor_Anterior = CStr(Campo.Valor_File_Data)
                                        tInconsistencia.Valor_Nuevo = Utilities.DStr(CType(ControlCampo, DesktopTextBoxControl).Text)
                                        tInconsistencia.fk_Tipo_Inconsistencia = DesktopConfig.Tipo_Inconsistencia.Campo_Inconsistente
                                        dbmArchiving.SchemaRisk.TBL_Inconsistencia.DBInsert(tInconsistencia)
                                        dbmArchiving.Transaction_Commit()

                                        nInconsistencias += 1
                                    End If
                                    registro.Valor_File_Data = Utilities.DStr(CType(ControlCampo, DesktopTextBoxControl).Text)

                                Case DesktopConfig.CampoTipo.Numerico
                                    If Not CInt(Campo.Valor_File_Data).Equals(Utilities.DInt(CType(ControlCampo, DesktopTextBoxControl).Text)) Then
                                        dbmArchiving.Transaction_Begin()
                                        Dim tInconsistencia As New DBArchiving.SchemaRisk.TBL_InconsistenciaType
                                        tInconsistencia.fk_OT = _typeArchivingFile.fk_OT
                                        tInconsistencia.fk_expediente = _typeArchivingFile.fk_expediente
                                        tInconsistencia.fk_folder = _typeArchivingFile.fk_Folder
                                        tInconsistencia.fk_file = _typeArchivingFile.id_File
                                        tInconsistencia.id_inconsistencia = dbmArchiving.SchemaRisk.TBL_Inconsistencia.DBNextId(_typeArchivingFile.fk_OT, _typeArchivingFile.fk_expediente, _typeArchivingFile.fk_Folder, _typeArchivingFile.id_File)
                                        tInconsistencia.Valor_Anterior = CInt(Campo.Valor_File_Data)
                                        tInconsistencia.Valor_Nuevo = Utilities.DInt(CType(ControlCampo, DesktopTextBoxControl).Text)
                                        tInconsistencia.fk_Tipo_Inconsistencia = DesktopConfig.Tipo_Inconsistencia.Campo_Inconsistente
                                        dbmArchiving.SchemaRisk.TBL_Inconsistencia.DBInsert(tInconsistencia)
                                        dbmArchiving.Transaction_Commit()

                                        nInconsistencias += 1
                                    End If
                                    registro.Valor_File_Data = Utilities.DInt(CType(ControlCampo, DesktopTextBoxControl).Text)

                            End Select

                            registro.Conteo_File_Data = registro.Valor_File_Data.ToString().Length
                            dbmCore.SchemaProcess.TBL_File_Data.DBUpdate(registro, registro.fk_Expediente, registro.fk_Folder, registro.fk_File, registro.fk_Documento, registro.fk_Campo)

                            'Facturación Caracteres Campos
                            Utilities.AgregarMovimiento(dbmArchiving, Program.Sesion.Entidad.id, tFileArchiving(0).fk_Esquema_Facturacion, DesktopConfig.Servicio_Facturacion.Primera_Captura_Devolucion_Caracter, Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, tFileArchiving(0).fk_Esquema, registro.Conteo_File_Data, Program.Sesion.Usuario.id, Program.DesktopGlobal.CentroProcesamientoRow)
                        End If

                    End If
                Next
                dbmCore.Transaction_Commit()

                'Facturación Campos Documento
                Utilities.AgregarMovimiento(dbmArchiving, Program.Sesion.Entidad.id, tFileArchiving(0).fk_Esquema_Facturacion, DesktopConfig.Servicio_Facturacion.Primera_Captura_Devolucion_Campo, Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, tFileArchiving(0).fk_Esquema, numeroCampos, Program.Sesion.Usuario.id, Program.DesktopGlobal.CentroProcesamientoRow)

                'Guardar las validaciones, si alguna que sea obligatoria no se cumple, se devuelve.
                Const bFileDevuelto As Boolean = False
                Dim bFolderSubsanable As Boolean = False
                Dim validacionNegativa As Boolean = False
                Dim registroValidacion As New DBCore.SchemaProcess.TBL_File_ValidacionType
                'Const numeroValidaciones As Integer = 0
                For Each validacion In CamposV
                    Dim NombreControl As String = Utilities.ConvertName(validacion.Pregunta)
                    Dim ControlCampo As Control = Utilities.FindControl(_TableFormValidaciones, NombreControl)

                    registroValidacion.fk_Expediente = _typeCoreFile.fk_Expediente
                    registroValidacion.fk_Folder = _typeCoreFile.fk_Folder
                    registroValidacion.fk_File = _typeCoreFile.id_File
                    registroValidacion.fk_Validacion = validacion.Id_Validacion
                    registroValidacion.Respuesta = CType(ControlCampo, DesktopCheckBox.DesktopCheckBoxControl).Checked

                    If validacion.Respuesta <> CType(ControlCampo, DesktopCheckBox.DesktopCheckBoxControl).Checked Then
                        dbmArchiving.Transaction_Begin()
                        Dim tInconsistencia As New DBArchiving.SchemaRisk.TBL_InconsistenciaType
                        tInconsistencia.fk_OT = _typeArchivingFile.fk_OT
                        tInconsistencia.fk_expediente = _typeArchivingFile.fk_expediente
                        tInconsistencia.fk_folder = _typeArchivingFile.fk_Folder
                        tInconsistencia.fk_file = _typeArchivingFile.id_File
                        tInconsistencia.id_inconsistencia = dbmArchiving.SchemaRisk.TBL_Inconsistencia.DBNextId(_typeArchivingFile.fk_OT, _typeArchivingFile.fk_expediente, _typeArchivingFile.fk_Folder, _typeArchivingFile.id_File)
                        tInconsistencia.Valor_Anterior = validacion.Respuesta
                        tInconsistencia.Valor_Nuevo = CType(ControlCampo, DesktopCheckBox.DesktopCheckBoxControl).Checked
                        tInconsistencia.fk_Tipo_Inconsistencia = DesktopConfig.Tipo_Inconsistencia.Validacion_Inconsitente
                        dbmArchiving.SchemaRisk.TBL_Inconsistencia.DBInsert(tInconsistencia)
                        dbmArchiving.Transaction_Commit()

                        nInconsistencias += 1
                    End If

                    If validacion.Subsanable And Not bFolderSubsanable Then bFolderSubsanable = True
                    If Not CType(ControlCampo, DesktopCheckBox.DesktopCheckBoxControl).Checked And Not validacionNegativa Then validacionNegativa = True

                    dbmCore.SchemaProcess.TBL_File_Validacion.DBUpdate(registroValidacion, registroValidacion.fk_Expediente, registroValidacion.fk_Folder, registroValidacion.fk_File, registroValidacion.fk_Validacion, Nothing)
                Next

                'Facturación Validaciones
                'If numeroValidaciones > 0 Then
                '    Utilities.AgregarMovimiento(dbmArchiving, Program.Sesion.Entidad.id, tFileArchiving(0).fk_Esquema_Facturacion, DesktopConfig.Servicio_Facturacion.Primera_Captura_Validacion_Devolucion, Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, tFileArchiving(0).fk_Esquema, Now, numeroValidaciones, Program.Sesion.Usuario.id, Program.DesktopGlobal.CentroProcesamientoRow)
                'End If

                'Se envía el file a Empaque
                If Not bFileDevuelto Then

                    'Valida que el centro de procesamiento del proyecto sea igual al centro de procesamiento del equipo
                    Dim SedeCustodia As Short = -1
                    If Not Program.RiskGlobal.SedeCustodia.IsNull Then SedeCustodia = CShort(Program.RiskGlobal.SedeCustodia)

                    Dim Estado = DBCore.EstadoEnum.Centro_Distribucion
                    If CShort(Program.RiskGlobal.EntidadCustodia) = Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad And SedeCustodia = Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede Then Estado = DBCore.EstadoEnum.Empaque

                    Utilities.ActualizaEstadoFile(dbmArchiving, dbmCore, _CBarras, _typeArchivingFile.fk_OT, Nothing, DesktopConfig.Modulo.Archiving, Estado, Program.Sesion.Usuario.id, Nothing, Nothing, Nothing, Nothing)
                End If

                ' Marca el folder como no subsanable para impedir el proceso de Endoso con manejo de fondeadores
                If validacionNegativa And Not bFolderSubsanable Then
                    Dim RiskFolder As New DBArchiving.SchemaRisk.TBL_FolderType
                    RiskFolder.Impide_Endoso = True
                    dbmArchiving.SchemaRisk.TBL_Folder.DBUpdate(RiskFolder, _typeCoreFile.fk_Expediente, _typeCoreFile.fk_Folder, _typeArchivingFile.fk_OT)
                End If

                Me.Close()
                If nInconsistencias = 0 Then
                    DesktopMessageBoxControl.DesktopMessageShow("Datos almacenados correctamente.", "Datos Guardados", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                Else
                    DesktopMessageBoxControl.DesktopMessageShow("Datos almacenados con [" + nInconsistencias.ToString() + "] inconsistencias.", "[" + nInconsistencias.ToString() + "] Datos Inconsistentes", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                End If
            Catch ex As Exception
                dbmArchiving.Transaction_Rollback()
                dbmCore.Transaction_Rollback()
                DesktopMessageBoxControl.DesktopMessageShow("GuardarDatos", ex)
            Finally
                dbmArchiving.Connection_Close()
                dbmCore.Connection_Close()
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

        Public Function ValidarTipoCaptura(ByVal Table As DBArchiving.Schemadbo.CTA_Campos_DocumentosDataTable) As DataTable
            Dim TableData As DataTable
            Dim viewPrimeraCaptura As New DataView(Table)
            'Dim viewSegundaCaptura As New DataView(Table)
            'Dim viewTerceraCaptura As New DataView(Table)

            viewPrimeraCaptura.RowFilter = Table.fk_EstadoColumn.ColumnName & "=30" & " and " & Table.Existe_File_DataColumn.ColumnName & "=0 "
            'viewSegundaCaptura.RowFilter = Table.fk_estadoColumn.ColumnName & "=33 and " & Table.Usa_Doble_CapturaColumn.ColumnName & "=1"
            'viewTerceraCaptura.RowFilter = Table.fk_estadoColumn.ColumnName & "=37 and " & Table.Usa_Doble_CapturaColumn.ColumnName & "=1"

            'Primera captura
            If viewPrimeraCaptura.ToTable().Rows.Count > 0 Then
                TableData = viewPrimeraCaptura.ToTable

                'ElseIf viewSegundaCaptura.ToTable().Rows.Count > 0 Then 'Segunda captura
                '    CapturaLabel.Text = "Segunda Captura"
                '    TableData = viewSegundaCaptura.ToTable()

                'ElseIf viewTerceraCaptura.ToTable().Rows.Count > 0 Then 'Tercera captura
                '    CapturaLabel.Text = "Tercera Captura"
                '    TableData = viewTerceraCaptura.ToTable

            Else 'No se puede capturar
                Throw New Exception("El Documento que intenta procesar no esta en ningun estado que permita hacer mesa de control.")
            End If

            Return TableData
        End Function

        Public Function ValidarData() As Boolean
            Dim dbmArchiving As New DBArchiving.DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            Dim bReturn As Boolean = True
            Try
                For Each Campo As CamposDataAdicional In Campos
                    Dim valor As String
                    Dim NombreControl As String = Utilities.ConvertName(Campo.NombreCampo)
                    Dim ControlCampo As Control = Utilities.FindControl(_TableForm, NombreControl)

                    If Campo.TipoCampo = DesktopConfig.CampoTipo.Texto Or Campo.TipoCampo = DesktopConfig.CampoTipo.Fecha Or Campo.TipoCampo = DesktopConfig.CampoTipo.Numerico Then
                        valor = CType(ControlCampo, DesktopTextBoxControl).Text
                        If (Campo.CampoObligatorio And (valor = "" Or (Slyg.Tools.DataConvert.IsNumeric(valor) AndAlso Slyg.Tools.DataConvert.ToNumericD(valor, ".") = 0.0))) Then
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