Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Controls.DesktopTextBox
Imports Miharu.Desktop.Controls.DesktopComboBox
Imports DBArchiving
Imports DBArchiving.Schemadbo
Imports DBCore
Imports DBCore.SchemaProcess
Imports Miharu.Desktop.Library.Config

Namespace Forms.MesaControlFisicos

    Public Class FormTerceraCapturarDataAdicional

#Region " Declaraciones "
        Private _CBarras As String
        Private _TableForm As TableLayoutPanel
        Private Campos As List(Of CamposDataAdicional)

        Dim _typeCoreFile As New TBL_FileType
        Dim _typeArchivingFile As New SchemaRisk.TBL_FileType
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
            Dim MeForm As New FormTerceraCapturarDataAdicional(CBarras)
            MeForm.ShowDialog()
        End Sub

#End Region

#Region " Eventos "

        Private Sub FormTerceraCapturarDataAdicional_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            Try
                setValoresCaptura()
                CreaCampos()
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("FormTerceraCapturarDataAdicional_Load", ex)
            End Try
        End Sub

        Private Sub AceptarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AceptarButton.Click
            If ValidarData() Then
                GuardarDatos()
            Else
                DesktopMessageBoxControl.DesktopMessageShow("Existen campos que son obligatorios y no han sido diligenciados. [Campos resaltados]", "Validación de campos", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
            End If
        End Sub

        Private Sub Monto3TextBox_KeyPress(ByVal sender As System.Object, ByVal e As KeyPressEventArgs) Handles Monto3TextBox.KeyPress
            If e.KeyChar = ChrW(Keys.Enter) Or e.KeyChar = ChrW(Keys.Tab) Then
                If CamposPanel.Controls.Count = 0 Then
                    AceptarButton.Focus()
                Else
                    Dim LayoutCampos = CType(CamposPanel.Controls(0), TableLayoutPanel)
                    For Each campo In LayoutCampos.Controls
                        Select Case campo.GetType().ToString()
                            Case "Miharu.Desktop.Controls.DesktopTextBox"
                                Dim objText = CType(campo, DesktopTextBoxControl)
                                If objText.Enabled Then
                                    CType(campo, DesktopTextBoxControl).Focus()
                                    Exit For
                                End If
                        End Select
                    Next
                End If
                e.Handled = True
            End If
        End Sub

#End Region

#Region " Metodos "
        Private Sub setValoresCaptura()
            Dim dbmCore As DBCoreDataBaseManager = Nothing
            Dim dbmArchiving As DBArchivingDataBaseManager = Nothing
            Try
                'Core.File
                dbmCore = New DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                dbmArchiving = New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)
                dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

                Dim tFilecore = dbmCore.SchemaProcess.TBL_File.DBFindByCBarras_File(_CBarras)
                _typeCoreFile.fk_Expediente = tFilecore(0).fk_Expediente
                _typeCoreFile.fk_Folder = tFilecore(0).fk_Folder
                _typeCoreFile.id_File = tFilecore(0).id_File
                _typeCoreFile.fk_Documento = tFilecore(0).fk_Documento
                _typeCoreFile.Folios_File = tFilecore(0).Folios_File
                _typeCoreFile.Monto_File = tFilecore(0).Monto_File
                _typeCoreFile.CBarras_File = tFilecore(0).CBarras_File

                Dim tFileArchiving = dbmArchiving.Schemadbo.CTA_File.DBFindByCBarras_File(_CBarras)

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
                dbmArchiving.Connection_Close()
            End Try
        End Sub

        Public Sub CreaCampos()
            EntidadLabel.Text = Program.RiskGlobal.NombreEntidad
            ProyectoLabel.Text = Program.RiskGlobal.NombreProyecto
            CBarrasLabel.Text = _CBarras
            
            Dim dbmArchiving As DBArchivingDataBaseManager = Nothing
            Try
                dbmArchiving = New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
                dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

                Dim TableFile = dbmArchiving.Schemadbo.CTA_File_Detalle.DBFindByCBarras_Filefk_entidadfk_proyecto(CBarrasLabel.Text, Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto)

                If TableFile.Rows.Count = 0 Then
                    DesktopMessageBoxControl.DesktopMessageShow("El código de barras no se encuentra en la base de datos, puede ser que no tenga campos parametrizados o que no exista para el proyecto, por favor intente con otro.", "No se ha encontrado el código de barras", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                    Me.Close()
                Else
                    EsquemaLabel.Text = TableFile.Rows(0)(TableFile.fk_esquemaColumn).ToString & " - " & TableFile.Rows(0)(TableFile.Nombre_esquemaColumn).ToString
                    TipologiaLabel.Text = TableFile.Rows(0)(TableFile.id_TipologiaColumn).ToString & " - " & TableFile.Rows(0)(TableFile.Nombre_TipologiaColumn).ToString
                    CapturaLabel.Text = "Tercera Captura"

                    Try
                        'Campos segunda captura Folios y Folder
                        Dim dtDocumento = dbmArchiving.Schemadbo.CTA_File.DBFindByCBarras_File(_CBarras)
                        If dtDocumento.Count > 0 Then
                            If dtDocumento(0).Folios_Doble_Captura Or dtDocumento(0).Monto_Doble_Captura Then
                                If dtDocumento(0).Folios_Doble_Captura Then
                                    Dim dtMC = dbmArchiving.SchemaRisk.TBL_File_Data_MC.DBGet(dtDocumento(0).fk_Expediente, dtDocumento(0).fk_Folder, dtDocumento(0).id_File, dtDocumento(0).fk_Documento, CShort(-1))
                                    If dtMC.Count > 0 Then
                                        FoliosMontoGroupBox.Visible = True

                                        FoliosLabel.Visible = True
                                        Folios1TextBox.Visible = True
                                        Folios1TextBox.Text = dtDocumento(0).Folios_File.ToString()
                                        Folios2TextBox.Visible = True
                                        Folios2TextBox.Text = dtMC(0).Valor_Segunda_Captura.ToString()
                                        Folios3TextBox.Visible = True
                                    End If
                                End If
                                If dtDocumento(0).Monto_Doble_Captura Then
                                    Dim dtMC = dbmArchiving.SchemaRisk.TBL_File_Data_MC.DBGet(dtDocumento(0).fk_Expediente, dtDocumento(0).fk_Folder, dtDocumento(0).id_File, dtDocumento(0).fk_Documento, CShort(-2))
                                    If dtMC.Count > 0 Then
                                        FoliosMontoGroupBox.Visible = True

                                        MontoLabel.Visible = True
                                        Monto1TextBox.Visible = True
                                        Monto1TextBox.Text = CDec(dtDocumento(0).Monto_File).ToString()
                                        Monto2TextBox.Visible = True
                                        Monto2TextBox.Text = CDec(dtMC(0).Valor_Segunda_Captura).ToString()
                                        Monto3TextBox.Visible = True
                                    End If
                                End If
                            End If
                        End If


                        Dim TableCampos = dbmArchiving.Schemadbo.CTA_Campos_Data_TerceraCaptura.DBFindByCBarras_File(_CBarras)
                        If TableCampos.Count > 0 Then
                            Campos = New List(Of CamposDataAdicional)
                            Dim CampoControlCollection As New List(Of DesktopConfig.CrearControlesType)

                            For Each Row As DataRow In TableCampos.Rows
                                Dim campo As New CamposDataAdicional
                                Dim CampoControl As New DesktopConfig.CrearControlesType

                                campo.Id_Campo = CShort(Row(CTA_Campos_Data_TerceraCapturaEnum.id_Campo.ColumnName))
                                campo.LongitudCampo = CShort(Row(CTA_Campos_Data_TerceraCapturaEnum.Length_Campo.ColumnName))
                                campo.NombreCampo = CStr(Row(CTA_Campos_Data_TerceraCapturaEnum.Nombre_Campo.ColumnName))
                                campo.CampoObligatorio = CBool(Row(CTA_Campos_Data_TerceraCapturaEnum.Es_Obligatorio_Campo.ColumnName))
                                campo.CampoLista = CShort(Row(CTA_Campos_Data_TerceraCapturaEnum.fk_Campo_Lista.ColumnName))
                                campo.TipoCampo = CType(Row(CTA_Campos_Data_TerceraCapturaEnum.fk_Campo_Tipo.ColumnName), DesktopConfig.CampoTipo)
                                campo.Documento = CShort(Row(CTA_Campos_Data_TerceraCapturaEnum.fk_Documento.ColumnName))
                                campo.Expediente = CLng(Row(CTA_Campos_Data_TerceraCapturaEnum.fk_Expediente.ColumnName))
                                campo.File = CShort(Row(CTA_Campos_Data_TerceraCapturaEnum.id_File.ColumnName))
                                campo.Folder = CShort(Row(CTA_Campos_Data_TerceraCapturaEnum.fk_Folder.ColumnName))
                                Campos.Add(campo)

                                CampoControl.CampoLista = CShort(Row(CTA_Campos_Data_TerceraCapturaEnum.fk_Campo_Lista.ColumnName))
                                CampoControl.Label = CStr(Row(CTA_Campos_Data_TerceraCapturaEnum.Nombre_Campo.ColumnName))
                                CampoControl.MaxLength = CShort(Row(CTA_Campos_Data_TerceraCapturaEnum.Length_Campo.ColumnName))
                                CampoControl.NombreCampo = CStr(Row(CTA_Campos_Data_TerceraCapturaEnum.Nombre_Campo.ColumnName))
                                CampoControl.Tipo = CType(Row(CTA_Campos_Data_TerceraCapturaEnum.fk_Campo_Tipo.ColumnName), DesktopConfig.CampoTipo)
                                CampoControl.Width = 150
                                CampoControl.LabelWidth = 200
                                CampoControl.Existe_File_Data = CBool(Row(CTA_Campos_Data_TerceraCapturaEnum.Existe_File_Data.ColumnName))
                                CampoControl.File_Data = Row(CTA_Campos_Data_TerceraCapturaEnum.Valor_File_Data.ColumnName)
                                CampoControl.File_Data_PrimeraCaptura = Row(CTA_Campos_Data_TerceraCapturaEnum.Valor_Primera_Captura.ColumnName)
                                CampoControl.File_Data_SegundaCaptura = Row(CTA_Campos_Data_TerceraCapturaEnum.Valor_Segunda_Captura.ColumnName)
                                CampoControl.Obligatorio = CBool(Row(CTA_Campos_Data_TerceraCapturaEnum.Es_Obligatorio_Campo.ColumnName))
                                CampoControlCollection.Add(CampoControl)
                            Next

                            _TableForm = Utilities.CreaControlesTerceraCaptura(CampoControlCollection, Program.DesktopGlobal.ConnectionStrings, Program.RiskGlobal)
                            CamposPanel.Controls.Add(_TableForm)
                        End If
                    Catch ex As Exception
                        DesktopMessageBoxControl.DesktopMessageShow("CreaCampos", ex)
                        Me.Close()
                    End Try
                End If
            Catch ex As Exception
                If dbmArchiving IsNot Nothing Then dbmArchiving.Connection_Close()
                Throw
            End Try
            

        End Sub

        Private Sub GuardarDatos()
            Dim dbmCore As DBCoreDataBaseManager = Nothing
            Dim dbmArchiving As DBArchivingDataBaseManager = Nothing

            Try
                dbmCore = New DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                dbmArchiving = New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)


                dbmCore.Connection_Open(Program.Sesion.Usuario.id)
                dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

                'Campos segunda captura Folios y Folder
                Dim dtDocumento = dbmArchiving.Schemadbo.CTA_File.DBFindByCBarras_File(_CBarras)
                If dtDocumento.Count > 0 Then
                    If dtDocumento(0).Folios_Doble_Captura Or dtDocumento(0).Monto_Doble_Captura Then
                        If dtDocumento(0).Folios_Doble_Captura And Folios3TextBox.Visible Then
                            _typeCoreFile.Folios_File = CShort(IIf(Folios3TextBox.Text <> "", Folios3TextBox.Text, 0))
                            dbmArchiving.SchemaRisk.TBL_File_Data_MC.DBDelete(_typeCoreFile.fk_Expediente, _typeCoreFile.fk_Folder, _typeCoreFile.id_File, _typeCoreFile.fk_Documento, CShort(-1))
                        End If

                        If dtDocumento(0).Monto_Doble_Captura And Monto3TextBox.Visible Then
                            _typeCoreFile.Monto_File = CDec(IIf(Monto3TextBox.Text <> "", Monto3TextBox.Text, 0))
                            dbmArchiving.SchemaRisk.TBL_File_Data_MC.DBDelete(_typeCoreFile.fk_Expediente, _typeCoreFile.fk_Folder, _typeCoreFile.id_File, _typeCoreFile.fk_Documento, CShort(-2))
                        End If
                        'Guarda el registro en Core.File
                        dbmCore.Transaction_Begin()
                        dbmCore.SchemaProcess.TBL_File.DBUpdate(_typeCoreFile, _typeCoreFile.fk_Expediente, _typeCoreFile.fk_Folder, _typeCoreFile.id_File)
                        dbmCore.Transaction_Commit()
                    End If
                End If

                If Not IsNothing(Campos) Then
                    For Each Campo As CamposDataAdicional In Campos
                        Dim registro As New TBL_File_DataType
                        Dim NombreControl As String = Utilities.ConvertName(Campo.NombreCampo)
                        Dim ControlCampo As Control = Utilities.FindControl(_TableForm, NombreControl & "TerceraCaptura")

                        'Guarda el nuevo valor en Core.FileData
                        registro.fk_Expediente = Campo.Expediente
                        registro.fk_Folder = Campo.Folder
                        registro.fk_File = Campo.File
                        registro.fk_Documento = Campo.Documento
                        registro.fk_Campo = Campo.Id_Campo

                        'If Campo.TipoCampo = DesktopConfig.CampoTipo.Fecha Then
                        '    registro.Valor_File_Data = Utilities.DDate(CType(ControlCampo, DesktopTextBoxControl).Fecha)
                        'ElseIf Campo.TipoCampo = DesktopConfig.CampoTipo.Texto Then
                        '    registro.Valor_File_Data = Utilities.DStr(CType(ControlCampo, DesktopTextBoxControl).Text)
                        'ElseIf Campo.TipoCampo = DesktopConfig.CampoTipo.Numerico Then
                        '    registro.Valor_File_Data = Utilities.Dlng(CType(ControlCampo, DesktopTextBoxControl).Text)
                        'ElseIf Campo.TipoCampo = DesktopConfig.CampoTipo.Lista Then
                        '    registro.Valor_File_Data = Utilities.Dlng(CType(ControlCampo, DesktopComboBoxControl).SelectedValue)
                        'End If


                        If Campo.TipoCampo = DesktopConfig.CampoTipo.Lista Then
                            registro.Valor_File_Data = Utilities.Dlng(CType(ControlCampo, DesktopComboBoxControl).SelectedValue)
                        Else
                            registro.Valor_File_Data = CType(ControlCampo, DesktopTextBoxControl).Text
                        End If

                        registro.Conteo_File_Data = registro.Valor_File_Data.ToString().Length
                        dbmCore.SchemaProcess.TBL_File_Data.DBUpdate(registro, registro.fk_Expediente, registro.fk_Folder, registro.fk_File, registro.fk_Documento, registro.fk_Campo)

                        'Elimina los datos de segunda captura.
                        dbmArchiving.SchemaRisk.TBL_File_Data_MC.DBDelete(Campo.Expediente, Campo.Folder, Campo.File, Campo.Documento, Campo.Id_Campo)
                    Next
                End If

                'Valida que el centro de procesamiento del proyecto sea igual al centro de procesamiento del equipo
                Dim SedeCustodia As Short = -1
                If Not Program.RiskGlobal.SedeCustodia.IsNull Then SedeCustodia = CShort(Program.RiskGlobal.SedeCustodia)

                Dim Estado = DBCore.EstadoEnum.Centro_Distribucion
                If CShort(Program.RiskGlobal.EntidadCustodia) = Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad And SedeCustodia = Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede Then Estado = DBCore.EstadoEnum.Empaque

                Utilities.ActualizaEstadoFile(dbmArchiving, dbmCore, _CBarras, _typeArchivingFile.fk_OT, Nothing, DesktopConfig.Modulo.Archiving, Estado, Program.Sesion.Usuario.id, Nothing, Nothing, Nothing, Nothing)

                Me.Close()
                DesktopMessageBoxControl.DesktopMessageShow("Datos almacenados correctamente.", "Datos Guardados", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("GuardarDatos", ex)
            Finally
                If dbmCore IsNot Nothing Then dbmCore.Connection_Close()
                If dbmArchiving IsNot Nothing Then dbmArchiving.Connection_Close()
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
                        Dim ControlCampo As Control = Utilities.FindControl(_TableForm, NombreControl & "TerceraCaptura")

                        If Campo.TipoCampo <> DesktopConfig.CampoTipo.Lista Then
                            valor = CType(ControlCampo, DesktopTextBoxControl).Text
                        Else
                            valor = CStr(CType(ControlCampo, DesktopComboBoxControl).SelectedValue)
                        End If

                        If Campo.CampoObligatorio And (valor = "" Or (Slyg.Tools.DataConvert.IsNumeric(valor) AndAlso Math.Abs(Slyg.Tools.DataConvert.ToNumericD(valor, ".") - 0.0) < 0)) Then
                            CType(ControlCampo, DesktopTextBoxControl).BackColor = Drawing.Color.Yellow
                            bReturn = False
                        End If
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