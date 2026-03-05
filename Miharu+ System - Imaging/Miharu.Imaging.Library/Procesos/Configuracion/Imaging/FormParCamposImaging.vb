Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Controls
Imports Miharu.Desktop.Library
Imports DBImaging.SchemaConfig

Namespace Procesos.Configuracion.Imaging

    Public Class FormParCamposImaging
        Inherits FormBase

#Region " Declaraciones "

        Private _FileName As String = ""
        Private CamposDataTable As DBImaging.SchemaConfig.CTA_Campo_ParametrizacionDataTable

#End Region

#Region " Eventos "

        Private Sub FormParCamposImaging_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            CargaCombos()
        End Sub

        Private Sub EsquemaDesktopComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles EsquemaDesktopComboBox.SelectedIndexChanged
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                Dim Documento = dbmImaging.SchemaConfig.CTA_Documento.DBFindByfk_Entidadfk_Proyectofk_EsquemaEliminado(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, CShort(EsquemaDesktopComboBox.SelectedValue), False, 0, New DBImaging.SchemaConfig.CTA_DocumentoEnumList(DBImaging.SchemaConfig.CTA_DocumentoEnum.Nombre_Documento, True))
                Utilities.LlenarCombo(id_DocumentoDesktopComboBox, Documento, Documento.id_DocumentoColumn.ColumnName, Documento.Nombre_DocumentoColumn.ColumnName)

            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

        Private Sub id_DocumentoDesktopComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles id_DocumentoDesktopComboBox.SelectedIndexChanged
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                CamposDataTable = dbmImaging.SchemaConfig.CTA_Campo_Parametrizacion.DBFindByfk_Documentoid_CampoEliminado_Campo(CInt(id_DocumentoDesktopComboBox.SelectedValue), Nothing, False)

                CTACampoConfiguracionDataTableBindingSource.DataSource = CamposDataTable

            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

        Private Sub CamposDataGridView_CellClick(sender As System.Object, e As DataGridViewCellEventArgs) Handles CamposDataGridView.CellClick
            If (e.ColumnIndex = CamposDataGridView.Columns("AsignarMarcaColumn").Index And e.RowIndex >= 0) Then
                ConfigurarMarca(CType(CType(CamposDataGridView.Rows(e.RowIndex).DataBoundItem, DataRowView).Row, DBImaging.SchemaConfig.CTA_Campo_ParametrizacionRow))
            End If
            If (e.ColumnIndex = CamposDataGridView.Columns("AsignarCampoTrigger").Index And e.RowIndex >= 0) Then
                ConfigurarCampoTrigger(CType(CType(CamposDataGridView.Rows(e.RowIndex).DataBoundItem, DataRowView).Row, DBImaging.SchemaConfig.CTA_Campo_ParametrizacionRow))
            End If
        End Sub

        Private Sub CamposDataGridView_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles CamposDataGridView.CellContentClick
            If (e.RowIndex > -1) Then
                Dim NumCol1 As Int32
                Dim NumCol2 As Int32
                NumCol1 = CamposDataGridView.Columns("UsaCapturaDataGridViewCheckBoxColumn").Index
                NumCol2 = CamposDataGridView.Columns("UsaCapturaCorreccionMaquinaDataGridViewCheckBoxColumn").Index
                If (e.ColumnIndex = NumCol1 Or e.ColumnIndex = NumCol2) Then
                    If e.ColumnIndex = NumCol1 Then
                        If CBool(CamposDataGridView.CurrentCell.Value.ToString()) = True And _
                            CBool(CamposDataGridView.Rows(e.RowIndex).Cells(NumCol2).Value.ToString()) = True Then
                            CamposDataGridView.Rows(e.RowIndex).Cells(NumCol2).Value = False
                        End If
                    End If
                    If e.ColumnIndex = NumCol2 Then
                        If CBool(CamposDataGridView.CurrentCell.Value.ToString()) = True And _
                            CBool(CamposDataGridView.Rows(e.RowIndex).Cells(NumCol1).Value) = True Then
                            CamposDataGridView.Rows(e.RowIndex).Cells(NumCol1).Value = False
                        End If
                    End If
                End If
            End If
        End Sub

        Private Sub CamposDataGridView_CurrentCellDirtyStateChanged(sender As System.Object, e As System.EventArgs) Handles CamposDataGridView.CurrentCellDirtyStateChanged
            If (CamposDataGridView.IsCurrentCellDirty) Then
                CamposDataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit)
            End If
        End Sub

        Private Sub GuardarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles GuardarButton.Click
            GuardarCambios()
        End Sub

#End Region

#Region " Metodos "

        Public Sub CargaCombos()
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                Dim Esquema As New DataView(Program.ImagingGlobal.Esquemas)
                Esquema.RowFilter = Program.ImagingGlobal.Esquemas.fk_EntidadColumn.ColumnName & "=" & Program.ImagingGlobal.Entidad _
                                    & " AND " & Program.ImagingGlobal.Esquemas.fk_ProyectoColumn.ColumnName & "=" & Program.ImagingGlobal.Proyecto

                Utilities.LlenarCombo(EsquemaDesktopComboBox, Esquema.ToTable(), Program.ImagingGlobal.Esquemas.id_EsquemaColumn, Program.ImagingGlobal.Esquemas.Nombre_EsquemaColumn)

            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

        Public Sub GuardarCambios()
            If Validar() Then
                Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
                Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing

                Try
                    dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                    dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                    'dbmImaging.DataBase.Identifier_Date_Format = Program.DesktopGlobal.IdentifierDateFormat

                    dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
                    dbmImaging.Transaction_Begin()

                    dbmCore.Connection_Open(Program.Sesion.Usuario.id)
                    dbmCore.Transaction_Begin()

                    For Each Fila In CamposDataTable
                        Dim Registro As New DBImaging.SchemaConfig.TBL_CampoType
                        Dim RegistroCore As New DBCore.SchemaConfig.TBL_CampoType

                        Registro.Es_Campo_Cargue = Fila.Es_Campo_Cargue
                        Registro.Columna_Cargue_Campo = Fila.Columna_Cargue_Campo

                        Registro.Expresion_Regular_Campo = ""
                        Registro.fk_Documento = Fila.fk_Documento
                        RegistroCore.fk_Documento = Fila.fk_Documento
                        Registro.id_Campo = Fila.id_Campo
                        RegistroCore.id_Campo = Fila.id_Campo

                        Registro.Usa_Marca = Fila.Usa_Marca

                        If (Registro.Usa_Marca) Then
                            Registro.Marca_Height_Campo = Fila.Marca_Height_Campo
                            Registro.Marca_Width_Campo = Fila.Marca_Width_Campo
                            Registro.Marca_X_Campo = Fila.Marca_X_Campo
                            Registro.Marca_Y_Campo = Fila.Marca_Y_Campo
                        Else
                            Registro.Marca_Height_Campo = CByte(0)
                            Registro.Marca_Width_Campo = CByte(0)
                            Registro.Marca_X_Campo = CByte(0)
                            Registro.Marca_Y_Campo = CByte(0)
                        End If

                        Registro.Usa_Captura = Fila.Usa_Captura
                        Registro.Usa_Doble_Captura = ((Fila.Usa_Doble_Captura And Fila.Usa_Captura) Or (Fila.Usa_Doble_Captura And Fila.Usa_Captura_Correccion_Maquina))
                        Registro.Es_Campo_Indexacion = Fila.Es_Campo_Indexacion
                        Registro.Usa_Captura_Proceso_Adicional = Fila.Usa_Captura_Proceso_Adicional
                        Registro.Usa_Captura_Correccion_Maquina = Fila.Usa_Captura_Correccion_Maquina


                        If (Fila.IsNull("Orden_Campo")) Then
                            Registro.Orden_Campo = CShort(0)
                        Else
                            Registro.Orden_Campo = Fila.Orden_Campo
                        End If

                        If (Fila.IsNull("Mascara")) Then
                            Registro.Mascara = ""
                        Else
                            Registro.Mascara = Fila.Mascara
                        End If

                        If (Fila.IsNull("Formato")) Then
                            Registro.Formato = ""
                        Else
                            Registro.Formato = Fila.Formato
                        End If

                        If (Fila.Tabla_Min_Registros >= 0 And Fila.Tabla_Max_Registros >= Fila.Tabla_Min_Registros) Then
                            Registro.Tabla_Min_Registros = Fila.Tabla_Min_Registros
                            Registro.Tabla_Max_Registros = Fila.Tabla_Max_Registros
                        Else
                            Throw New Exception("El mínimo y/o el máximo de registros no cumplen con los requisitos para poder guardar el campo. El mínimo debe mayor o igual a cero (0) y menor o igual a el máximo")
                        End If

                        RegistroCore.Es_Obligatorio_Campo = Fila.Es_Obligatorio_Campo

                        Registro.Usa_Trigger = Fila.Usa_Trigger

                        Registro.fk_Usuario_Log = Program.Sesion.Usuario.id
                        Registro.Fecha_Log = DateTime.Now

                        If (dbmImaging.SchemaConfig.TBL_Campo.DBGet(Registro.fk_Documento, Registro.id_Campo).Count = 0) Then
                            dbmImaging.SchemaConfig.TBL_Campo.DBInsert(Registro)
                        Else
                            dbmImaging.SchemaConfig.TBL_Campo.DBUpdate(Registro, Registro.fk_Documento, Registro.id_Campo)
                            dbmCore.SchemaConfig.TBL_Campo.DBUpdate(RegistroCore, RegistroCore.fk_Documento, RegistroCore.id_Campo)
                        End If
                    Next

                    dbmImaging.Transaction_Commit()
                    dbmCore.Transaction_Commit()

                    DesktopMessageBoxControl.DesktopMessageShow("Se han guardado los datos con éxito.", "Campo Imaging", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                Catch ex As Exception
                    If (dbmImaging IsNot Nothing) Then dbmImaging.Transaction_Rollback()
                    If (dbmCore IsNot Nothing) Then dbmCore.Transaction_Rollback()

                    DesktopMessageBoxControl.DesktopMessageShow("Hubo problemas al guardar los datos, por favor comuniquese con el administrador" & vbCrLf & vbCrLf & ex.Message, "Problemas en Campo Imaging", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                Finally
                    If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                    If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                End Try
            End If
        End Sub

        Private Sub ConfigurarMarca(nFilaCampo As DBImaging.SchemaConfig.CTA_Campo_ParametrizacionRow)
            If (Validar()) Then
                Dim f = New FormConfigCampo()
                Dim Cargado As Boolean

                f.X = nFilaCampo.Marca_X_Campo
                f.Y = nFilaCampo.Marca_Y_Campo
                f.W = nFilaCampo.Marca_Width_Campo
                f.H = nFilaCampo.Marca_Height_Campo

                If (_FileName <> "") Then
                    Cargado = f.Cargar(_FileName)
                Else
                    Cargado = f.Cargar()
                End If

                If (Cargado) Then
                    If (f.ShowDialog() = DialogResult.OK) Then
                        _FileName = f.FileName

                        nFilaCampo.Marca_X_Campo = CByte(f.X)
                        nFilaCampo.Marca_Y_Campo = CByte(f.Y)
                        nFilaCampo.Marca_Width_Campo = CByte(f.W)
                        nFilaCampo.Marca_Height_Campo = CByte(f.H)
                    End If
                End If
            End If
        End Sub

        Private Sub ConfigurarCampoTrigger(ByVal ctaCampoParametrizacionRow As CTA_Campo_ParametrizacionRow)
            Dim fCampoTrigger As New FormCamposTrigger()
            fCampoTrigger.CampoRow = ctaCampoParametrizacionRow

            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                With ctaCampoParametrizacionRow
                    Dim campoCoreRow = dbmCore.SchemaConfig.TBL_Campo.DBFindByfk_Entidadfk_Documentoid_Campo(.fk_Entidad, .fk_Documento, .id_Campo)

                    If campoCoreRow.Rows.Count > 0 Then
                        Dim campoRow = CType(campoCoreRow.Rows(0), DBCore.SchemaConfig.TBL_CampoRow)
                        'If (Not campoRow.Isfk_Campo_ListaNull) AndAlso (.Usa_Trigger) Then
                        If (.Usa_Trigger) Then
                            If fCampoTrigger.ShowDialog() = Windows.Forms.DialogResult.OK Then
                                MessageBox.Show("Campos trigger parametrizados con éxito", "Campos Trigger", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        End If
                    End If
                End With

            Catch
                Throw
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            End Try
        End Sub

#End Region

#Region " Funciones "

        Public Function Validar() As Boolean
            'Dim Validacion As Boolean = True
            'Dim sbErrores As New StringBuilder

            'If Validacion = False Then
            '    DesktopMessageBox.DesktopMessageShow(sbErrores.ToString(), "Error de data", Desktop.Controls.DesktopMessageBox.IconEnum.ErrorIcon, True)
            'End If

            Return True
        End Function

#End Region


    End Class
End Namespace