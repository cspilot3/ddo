Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Controls.DesktopTextBox
Imports Miharu.Desktop.Controls.DesktopComboBox
Imports DBArchiving
Imports DBArchiving.Schemadbo
Imports DBCore
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Config

Namespace Forms.MesaControlFisicos

    Public Class FormSegundaCapturarDataAdicional
        Inherits FormBase

#Region " Declaraciones "
        Private _CBarras As String
        Private _TableForm As TableLayoutPanel
        Private Campos As List(Of CamposDataAdicional)

        Dim _typeCoreFile As New DBCore.SchemaProcess.TBL_FileType
        Dim _typeArchivingFile As New SchemaRisk.TBL_FileType

        Dim tFileArchiving As DBArchiving.Schemadbo.CTA_FileDataTable
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
            Dim MeForm As New FormSegundaCapturarDataAdicional(CBarras)
            MeForm.ShowDialog()
        End Sub

#End Region

#Region " Eventos "

        Private Sub FormCapturarDataAdicional_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            Try
                setValoresCaptura()
                CreaCampos()
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("FormCapturarDataAdicional_Load", ex)
            End Try
        End Sub

        Private Sub CerrarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CerrarButton.Click
            Me.Close()
        End Sub

        Private Sub MontoDesktopTextBox_KeyPress(ByVal sender As System.Object, ByVal e As KeyPressEventArgs) Handles MontoDesktopTextBox.KeyPress
            If e.KeyChar = ChrW(Keys.Enter) Or e.KeyChar = ChrW(Keys.Tab) Then
                If CamposPanel.Controls.Count = 0 Then
                    AceptarButton.Focus()
                Else
                    Dim LayoutCampos = CType(CamposPanel.Controls(0), TableLayoutPanel)
                    For Each campo In LayoutCampos.Controls
                        Select Case campo.GetType().ToString()
                            Case "Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl"
                                CType(campo, DesktopTextBoxControl).Focus()
                                Exit For
                            Case "Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl"
                                CType(campo, DesktopComboBoxControl).Focus()
                                Exit For
                        End Select
                    Next
                End If
                e.Handled = True
            End If
        End Sub
#End Region

#Region " Metodos "

        Private Sub setValoresCaptura()
            Dim dbmCore As New DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
            Dim dbmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
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
                tFileArchiving = dbmArchiving.Schemadbo.CTA_File.DBFindByCBarras_File(_CBarras)
                dbmArchiving.Connection_Close()

                _typeArchivingFile.fk_OT = tFileArchiving(0).fk_OT
                _typeArchivingFile.fk_Folder = tFileArchiving(0).fk_Folder
                _typeArchivingFile.id_File = tFileArchiving(0).id_File
                _typeArchivingFile.fk_expediente = tFileArchiving(0).fk_Expediente
                _typeArchivingFile.fk_Estado = tFileArchiving(0).fk_Estado
                If Not tFileArchiving(0).Isfk_Caja_ProcesoNull Then _typeArchivingFile.fk_Caja_Proceso = tFileArchiving(0).fk_Caja_Proceso
                _typeArchivingFile.fk_Registro_Tipo = tFileArchiving(0).fk_Registro_Tipo
                _typeArchivingFile.Es_sobrante = tFileArchiving(0).Es_sobrante
                _typeArchivingFile.Impreso = tFileArchiving(0).Impreso

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

            Dim dbmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

            Dim TableFile = dbmArchiving.Schemadbo.CTA_File_Detalle.DBFindByCBarras_Filefk_entidadfk_proyecto(CBarrasLabel.Text, Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto)

            If TableFile.Rows.Count = 0 Then
                DesktopMessageBoxControl.DesktopMessageShow("El código de barras no se encuentra en la base de datos, puede ser que no tenga campos parametrizados o que no exista para el proyecto, por favor intente con otro.", "No se ha encontrado el código de barras", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                Me.Close()
            Else
                EsquemaLabel.Text = TableFile.Rows(0)(TableFile.fk_esquemaColumn).ToString & " - " & TableFile.Rows(0)(TableFile.Nombre_esquemaColumn).ToString
                TipologiaLabel.Text = TableFile.Rows(0)(TableFile.id_TipologiaColumn).ToString & " - " & TableFile.Rows(0)(TableFile.Nombre_TipologiaColumn).ToString
                CapturaLabel.Text = "Segunda Captura"

                Try
                    'Campos segunda captura Folios y Folder
                    Dim dtDocumento = dbmArchiving.SchemaConfig.TBL_Documento.DBFindByfk_Entidadfk_Proyectofk_Esquemafk_Documento(tFileArchiving(0).fk_Entidad, Program.RiskGlobal.Proyecto, tFileArchiving(0).fk_Esquema, tFileArchiving(0).fk_Documento)
                    If dtDocumento.Count > 0 Then
                        If dtDocumento(0).Folios_Doble_Captura Or dtDocumento(0).Monto_Doble_Captura Then
                            FoliosMontoGroupBox.Visible = True
                            If dtDocumento(0).Folios_Doble_Captura Then
                                FoliosLabel.Visible = True
                                FoliosDesktopTextBox.Visible = True
                                FoliosDesktopTextBox.Focus()
                            End If
                            If dtDocumento(0).Monto_Doble_Captura Then
                                MontoLabel.Visible = True
                                MontoDesktopTextBox.Visible = True
                            End If
                        End If
                    End If

                    Dim camposDocumento = dbmArchiving.Schemadbo.CTA_Campos_Documentos.DBFindByCBarras_Filefk_Entidadfk_ProyectoUsa_CapturaUsa_Doble_CapturaEs_Campo_Destape(CBarrasLabel.Text, Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, Nothing, True, False)
                    If camposDocumento.Count > 0 Then

                        Dim TableCampos As DataTable = camposDocumento
                        Dim TableCamposFilter = TableCampos.DefaultView.ToTable(True, "Nombre_Campo", "Id_Campo", "fk_Campo_Tipo", "fk_Campo_Lista", "Es_Obligatorio_Campo", "Length_Campo", "Existe_File_Data", "Usa_Captura", "Usa_Doble_Captura", "CBarras_File", "CBarras_Folder", "fk_Estado", "fk_Entidad", "fk_Proyecto", "fk_Documento", "fk_Expediente", "id_File", "fk_Folder", "Valor_File_Data", "fk_Esquema", "Folios_Doble_Captura", "Monto_Doble_Captura")

                        Campos = New List(Of CamposDataAdicional)
                        Dim CampoControlCollection As New List(Of DesktopConfig.CrearControlesType)

                        For Each Row As DataRow In TableCamposFilter.Rows
                            Dim campo As New CamposDataAdicional
                            Dim CampoControl As New DesktopConfig.CrearControlesType

                            campo.Id_Campo = CShort(Row(CTA_Campos_DocumentosEnum.id_Campo.ColumnName))
                            campo.LongitudCampo = CShort(Row(CTA_Campos_DocumentosEnum.Length_Campo.ColumnName))
                            campo.NombreCampo = CStr(Row(CTA_Campos_DocumentosEnum.Nombre_Campo.ColumnName))
                            campo.CampoObligatorio = CBool(Row(CTA_Campos_DocumentosEnum.Es_Obligatorio_Campo.ColumnName))
                            campo.CampoLista = CShort(Row(CTA_Campos_DocumentosEnum.fk_Campo_Lista.ColumnName))
                            campo.TipoCampo = CType(Row(CTA_Campos_DocumentosEnum.fk_Campo_Tipo.ColumnName), DesktopConfig.CampoTipo)
                            campo.Documento = CShort(Row(CTA_Campos_DocumentosEnum.fk_Documento.ColumnName))
                            campo.Expediente = CLng(Row(CTA_Campos_DocumentosEnum.fk_Expediente.ColumnName))
                            campo.File = CShort(Row(CTA_Campos_DocumentosEnum.id_File.ColumnName))
                            campo.Folder = CShort(Row(CTA_Campos_DocumentosEnum.fk_Folder.ColumnName))
                            Campos.Add(campo)

                            CampoControl.CampoLista = CShort(Row(CTA_Campos_DocumentosEnum.fk_Campo_Lista.ColumnName))
                            CampoControl.Label = CStr(Row(CTA_Campos_DocumentosEnum.Nombre_Campo.ColumnName))
                            CampoControl.MaxLength = CShort(Row(CTA_Campos_DocumentosEnum.Length_Campo.ColumnName))
                            CampoControl.NombreCampo = CStr(Row(CTA_Campos_DocumentosEnum.Nombre_Campo.ColumnName))
                            CampoControl.Tipo = CType(Row(CTA_Campos_DocumentosEnum.fk_Campo_Tipo.ColumnName), DesktopConfig.CampoTipo)
                            CampoControl.Width = 200
                            CampoControl.LabelWidth = 200
                            CampoControl.Existe_File_Data = CBool(Row(CTA_Campos_DocumentosEnum.Existe_File_Data.ColumnName))
                            CampoControl.File_Data = Row(CTA_Campos_DocumentosEnum.Valor_File_Data.ColumnName)
                            CampoControl.Obligatorio = CBool(Row(CTA_Campos_DocumentosEnum.Es_Obligatorio_Campo.ColumnName))
                            CampoControlCollection.Add(CampoControl)
                        Next

                        _TableForm = Utilities.CreaControles(dbmArchiving, CampoControlCollection, Program.DesktopGlobal.ConnectionStrings, Program.RiskGlobal, False, CamposPanel.Width)
                        CamposPanel.Controls.Add(_TableForm)
                    End If
                Catch ex As Exception
                    DesktopMessageBoxControl.DesktopMessageShow("CrearEstructuraCampos", ex)
                    Me.Close()
                End Try
            End If

            dbmArchiving.Connection_Close()
        End Sub

        Private Sub AceptarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AceptarButton.Click
            If ValidarData() Then
                GuardarDatos()
            Else
                DesktopMessageBoxControl.DesktopMessageShow("Existen campos que son obligatorios y no han sido diligenciados. [Campos resaltados]", "Validación de campos", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
            End If
        End Sub

        Private Sub GuardarDatos()
            Dim dbmCore As New DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
            Dim dbmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            Dim contInvalidos As Integer = 0
            Dim _typeArchivingFileDataMC As New SchemaRisk.TBL_File_Data_MCType
            Try
                dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                'Valida doble captura para Folios y Monto
                'Calcula si se debe capturar doble monto y folios
                'Folios = -1
                'Monto = -2
                Dim numeroCampos As Integer = 0
                Dim dtDocumento = dbmArchiving.Schemadbo.CTA_File.DBFindByCBarras_File(_CBarras)
                If dtDocumento.Count > 0 Then
                    If dtDocumento(0).Folios_Doble_Captura Then
                        If dtDocumento(0).Folios_File <> CShort(IIf(FoliosDesktopTextBox.Text <> "", FoliosDesktopTextBox.Text, 0)) Then
                            _typeArchivingFileDataMC.fk_Campo = CShort(-1)
                            _typeArchivingFileDataMC.Valor_Primera_Captura = dtDocumento(0).Folios_File
                            _typeArchivingFileDataMC.Valor_Segunda_Captura = CShort(IIf(FoliosDesktopTextBox.Text <> "", FoliosDesktopTextBox.Text, 0))
                            _typeArchivingFileDataMC.fk_Expediente = dtDocumento(0).fk_Expediente
                            _typeArchivingFileDataMC.fk_Folder = dtDocumento(0).fk_Folder
                            _typeArchivingFileDataMC.fk_File = dtDocumento(0).id_File
                            _typeArchivingFileDataMC.fk_Documento = dtDocumento(0).fk_Documento

                            'Elimina la data temporal si existe
                            dbmArchiving.SchemaRisk.TBL_File_Data_MC.DBDelete(_typeArchivingFileDataMC.fk_Expediente, _typeArchivingFileDataMC.fk_Folder, _typeArchivingFileDataMC.fk_File, _typeArchivingFileDataMC.fk_Documento, _typeArchivingFileDataMC.fk_Campo)

                            dbmArchiving.SchemaRisk.TBL_File_Data_MC.DBInsert(_typeArchivingFileDataMC)
                            contInvalidos += 1
                            numeroCampos += 1

                            'Facturación Caractéres Campos
                            Utilities.AgregarMovimiento(dbmArchiving, Program.Sesion.Entidad.id, tFileArchiving(0).fk_Esquema_Facturacion, DesktopConfig.Servicio_Facturacion.Segunda_Captura_Caracter, Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, tFileArchiving(0).fk_Esquema, _typeArchivingFileDataMC.Valor_Segunda_Captura.ToString().Length, Program.Sesion.Usuario.id, Program.DesktopGlobal.CentroProcesamientoRow)
                        End If
                    End If

                    If dtDocumento(0).Monto_Doble_Captura Then
                        If dtDocumento(0).Monto_File <> CDec(IIf(MontoDesktopTextBox.Text <> "", MontoDesktopTextBox.Text, 0)) Then
                            _typeArchivingFileDataMC.fk_Campo = CShort(-2)
                            _typeArchivingFileDataMC.Valor_Primera_Captura = dtDocumento(0).Monto_File
                            _typeArchivingFileDataMC.Valor_Segunda_Captura = CDec(IIf(MontoDesktopTextBox.Text <> "", MontoDesktopTextBox.Text, 0))
                            _typeArchivingFileDataMC.fk_Expediente = dtDocumento(0).fk_Expediente
                            _typeArchivingFileDataMC.fk_Folder = dtDocumento(0).fk_Folder
                            _typeArchivingFileDataMC.fk_File = dtDocumento(0).id_File
                            _typeArchivingFileDataMC.fk_Documento = dtDocumento(0).fk_Documento

                            'Elimina la data temporal si existe
                            dbmArchiving.SchemaRisk.TBL_File_Data_MC.DBDelete(_typeArchivingFileDataMC.fk_Expediente, _typeArchivingFileDataMC.fk_Folder, _typeArchivingFileDataMC.fk_File, _typeArchivingFileDataMC.fk_Documento, _typeArchivingFileDataMC.fk_Campo)

                            dbmArchiving.SchemaRisk.TBL_File_Data_MC.DBInsert(_typeArchivingFileDataMC)
                            contInvalidos += 1
                            numeroCampos += 1
                            'Facturación Caractéres Campos
                            Utilities.AgregarMovimiento(dbmArchiving, Program.Sesion.Entidad.id, tFileArchiving(0).fk_Esquema_Facturacion, DesktopConfig.Servicio_Facturacion.Segunda_Captura_Caracter, Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, tFileArchiving(0).fk_Esquema, _typeArchivingFileDataMC.Valor_Segunda_Captura.ToString().Length, Program.Sesion.Usuario.id, Program.DesktopGlobal.CentroProcesamientoRow)
                        End If
                    End If
                End If

                'Valida los datos diligenciados contra la base de datos
                If Not IsNothing(Campos) Then
                    For Each Campo As CamposDataAdicional In Campos
                        Dim registro As New SchemaRisk.TBL_File_Data_MCType
                        Dim NombreControl As String = Utilities.ConvertName(Campo.NombreCampo)
                        Dim ControlCampo As Control = Utilities.FindControl(_TableForm, NombreControl)

                        Dim dtDataCampo = dbmArchiving.Schemadbo.CTA_Campo_Data.DBFindByCBarras_Filefk_CampoUsa_Doble_Captura(_CBarras, Campo.Id_Campo, True)
                        If dtDataCampo.Count > 0 Then
                            Dim dtData = dtDataCampo.Rows(0)
                            Dim sValorData As String = ""

                            'If Campo.TipoCampo = DesktopConfig.CampoTipo.Fecha Then
                            '    'La fecha viene en formato 01-12-2014 12:00:00, se debe procesar como yyyy/MM/dd -- 2010/12/01
                            '    Try : sValorData = DateTime.Parse(dtData("Valor_File_Data").ToString.Substring(0, 10)).ToString("yyyy/MM/dd") : Catch : End Try
                            'Else
                            '    sValorData = dtData("Valor_File_Data").ToString()
                            'End If

                            sValorData = dtData("Valor_File_Data").ToString()

                            'Si los campos son iguales se actualiza el estado a Empaque=40,
                            'de lo contrario almacena la segunda captura y lo pasa a Tercera Captura=35
                            If Campo.TipoCampo = DesktopConfig.CampoTipo.Texto Or Campo.TipoCampo = DesktopConfig.CampoTipo.Fecha Or Campo.TipoCampo = DesktopConfig.CampoTipo.Numerico Then
                                If sValorData.ToLower <> CType(ControlCampo, DesktopTextBoxControl).Text.ToLower Then
                                    contInvalidos += 1
                                    numeroCampos += 1

                                    registro.fk_Expediente = Campo.Expediente
                                    registro.fk_Folder = Campo.Folder
                                    registro.fk_File = Campo.File
                                    registro.fk_Documento = Campo.Documento
                                    registro.fk_Campo = Campo.Id_Campo
                                    registro.Valor_Primera_Captura = dtData("Valor_File_Data")

                                    'If Campo.TipoCampo = DesktopConfig.CampoTipo.Fecha Then
                                    '    registro.Valor_Segunda_Captura = Utilities.DDate(CType(ControlCampo, DesktopTextBoxControl).Fecha)
                                    'ElseIf Campo.TipoCampo = DesktopConfig.CampoTipo.Texto Then
                                    '    registro.Valor_Segunda_Captura = Utilities.DStr(CType(ControlCampo, DesktopTextBoxControl).Text)
                                    'ElseIf Campo.TipoCampo = DesktopConfig.CampoTipo.Numerico Then
                                    '    registro.Valor_Segunda_Captura = Utilities.Dlng(CType(ControlCampo, DesktopTextBoxControl).Text)
                                    'End If

                                    registro.Valor_Segunda_Captura = CType(ControlCampo, DesktopTextBoxControl).Text

                                    If dbmArchiving.SchemaRisk.TBL_File_Data_MC.DBGet(registro.fk_Expediente, registro.fk_Folder, registro.fk_File, registro.fk_Documento, registro.fk_Campo).Rows.Count > 0 Then
                                        dbmArchiving.SchemaRisk.TBL_File_Data_MC.DBUpdate(registro, registro.fk_Expediente, registro.fk_Folder, registro.fk_File, registro.fk_Documento, registro.fk_Campo)
                                    Else
                                        dbmArchiving.SchemaRisk.TBL_File_Data_MC.DBInsert(registro)
                                    End If

                                    'Facturación Caractéres Campos
                                    Utilities.AgregarMovimiento(dbmArchiving, Program.Sesion.Entidad.id, tFileArchiving(0).fk_Esquema_Facturacion, DesktopConfig.Servicio_Facturacion.Segunda_Captura_Caracter, Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, tFileArchiving(0).fk_Esquema, registro.Valor_Segunda_Captura.ToString().Length, Program.Sesion.Usuario.id, Program.DesktopGlobal.CentroProcesamientoRow)
                                End If

                            Else
                                If sValorData.ToLower <> CType(ControlCampo, DesktopComboBoxControl).SelectedValue.ToString.ToLower Then
                                    contInvalidos += 1
                                    numeroCampos += 1

                                    registro.fk_Expediente = Campo.Expediente
                                    registro.fk_Folder = Campo.Folder
                                    registro.fk_File = Campo.File
                                    registro.fk_Documento = Campo.Documento
                                    registro.fk_Campo = Campo.Id_Campo
                                    registro.Valor_Primera_Captura = dtData("Valor_File_Data")
                                    registro.Valor_Segunda_Captura = Utilities.DStr(CType(ControlCampo, DesktopComboBoxControl).SelectedValue)

                                    If dbmArchiving.SchemaRisk.TBL_File_Data_MC.DBGet(registro.fk_Expediente, registro.fk_Folder, registro.fk_File, registro.fk_Documento, registro.fk_Campo).Rows.Count > 0 Then
                                        dbmArchiving.SchemaRisk.TBL_File_Data_MC.DBUpdate(registro, registro.fk_Expediente, registro.fk_Folder, registro.fk_File, registro.fk_Documento, registro.fk_Campo)
                                    Else
                                        dbmArchiving.SchemaRisk.TBL_File_Data_MC.DBInsert(registro)
                                    End If

                                    'Facturación Caractéres Campos
                                    Utilities.AgregarMovimiento(dbmArchiving, Program.Sesion.Entidad.id, tFileArchiving(0).fk_Esquema_Facturacion, DesktopConfig.Servicio_Facturacion.Segunda_Captura_Caracter, Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, tFileArchiving(0).fk_Esquema, registro.Valor_Segunda_Captura.ToString().Length, Program.Sesion.Usuario.id, Program.DesktopGlobal.CentroProcesamientoRow)
                                End If
                            End If

                        End If
                    Next

                    'Facturación Campos Documento
                    Utilities.AgregarMovimiento(dbmArchiving, Program.Sesion.Entidad.id, tFileArchiving(0).fk_Esquema_Facturacion, DesktopConfig.Servicio_Facturacion.Segunda_Captura_Campo, Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, tFileArchiving(0).fk_Esquema, numeroCampos, Program.Sesion.Usuario.id, Program.DesktopGlobal.CentroProcesamientoRow)
                End If


                If contInvalidos = 0 Then 'Actualiza a empaque=40
                    'Valida que el centro de procesamiento del proyecto sea igual al centro de procesamiento del equipo
                    Dim SedeCustodia As Short = -1
                    If Not Program.RiskGlobal.SedeCustodia.IsNull Then SedeCustodia = CShort(Program.RiskGlobal.SedeCustodia)

                    Dim Estado = DBCore.EstadoEnum.Centro_Distribucion
                    If CShort(Program.RiskGlobal.EntidadCustodia) = Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad And SedeCustodia = Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede Then Estado = DBCore.EstadoEnum.Empaque

                    Utilities.ActualizaEstadoFile(dbmArchiving, dbmCore, _CBarras, _typeArchivingFile.fk_OT, Nothing, DesktopConfig.Modulo.Archiving, Estado, Program.Sesion.Usuario.id, Nothing, Nothing, Nothing, Nothing)
                Else 'Actualiza a tercera captura=35
                    Utilities.ActualizaEstadoFile(dbmArchiving, dbmCore, _CBarras, _typeArchivingFile.fk_OT, Nothing, DesktopConfig.Modulo.Archiving, EstadoEnum.Tercera_Captura, Program.Sesion.Usuario.id, Nothing, Nothing, Nothing, Nothing)
                End If

                Me.Close()
                DesktopMessageBoxControl.DesktopMessageShow("Datos almacenados correctamente.", "Datos Guardados", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("GuardarDatos", ex)
            Finally
                dbmCore.Connection_Close()
                dbmArchiving.Connection_Close()
            End Try
        End Sub
#End Region

#Region " Funciones "
        Public Function ValidarData() As Boolean
            Dim bReturn As Boolean = True
            Try
                If Not IsNothing(Campos) Then
                    For Each Campo As CamposDataAdicional In Campos
                        Dim valor As String
                        Dim NombreControl As String = Utilities.ConvertName(Campo.NombreCampo)
                        Dim ControlCampo As Control = Utilities.FindControl(_TableForm, NombreControl)

                        Select Case Campo.TipoCampo
                            Case DesktopConfig.CampoTipo.Texto, DesktopConfig.CampoTipo.Fecha, DesktopConfig.CampoTipo.Numerico
                                valor = CType(ControlCampo, DesktopTextBoxControl).Text
                                If (Campo.CampoObligatorio And (valor = "" Or (Slyg.Tools.DataConvert.IsNumeric(valor) AndAlso Math.Abs(Slyg.Tools.DataConvert.ToNumericD(valor, ".") - 0.0) < 0))) Then
                                    CType(ControlCampo, DesktopTextBoxControl).BackColor = Drawing.Color.Yellow
                                    bReturn = False
                                End If
                        End Select

                    Next
                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("ValidarData", ex)
            End Try
            Return bReturn
        End Function
#End Region

    End Class

End Namespace