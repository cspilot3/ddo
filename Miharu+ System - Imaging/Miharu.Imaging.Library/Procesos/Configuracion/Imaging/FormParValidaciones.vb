Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Controls
Imports Miharu.Desktop.Library

Namespace Procesos.Configuracion.Imaging

    Public Class FormParValidaciones
        Inherits FormBase

#Region " Declaraciones "

        Private FileName As String = ""
        Private ValidacionesDataTable As DBImaging.SchemaConfig.CTA_Validacion_ParametrizacionDataTable
        Private CamposDataTable As DBImaging.SchemaConfig.CTA_CampoDataTable
        Private ParametrosDataTable As DBImaging.SchemaConfig.TBL_ParametroDataTable

#End Region

#Region " Eventos "

        Private Sub FormParValidacionesImaging_Load(sender As System.Object, e As EventArgs) Handles MyBase.Load
            Load_Data()
        End Sub

        Private Sub EsquemaDesktopComboBox_SelectedIndexChanged(sender As System.Object, e As EventArgs) Handles EsquemaDesktopComboBox.SelectedIndexChanged
            Cargar_Documentos()
        End Sub

        Private Sub id_DocumentoDesktopComboBox_SelectedIndexChanged(sender As System.Object, e As EventArgs) Handles id_DocumentoDesktopComboBox.SelectedIndexChanged
            Cargar_Validaciones()
        End Sub

        Private Sub ValidacionesDataGridView_CellClick(sender As System.Object, e As DataGridViewCellEventArgs) Handles ValidacionesDataGridView.CellClick
            If (e.ColumnIndex = 6 And e.RowIndex >= 0) Then ' Imagen
                Configurar_Marca(CType(CType(ValidacionesDataGridView.Rows(e.RowIndex).DataBoundItem, DataRowView).Row, DBImaging.SchemaConfig.CTA_Validacion_ParametrizacionRow))
            ElseIf (e.ColumnIndex = 16 And e.RowIndex >= 0) Then ' Validacion automática
                Configurar_Validacion(CType(CType(ValidacionesDataGridView.Rows(e.RowIndex).DataBoundItem, DataRowView).Row, DBImaging.SchemaConfig.CTA_Validacion_ParametrizacionRow))
            End If
        End Sub

        Private Sub GuardarButton_Click(sender As System.Object, e As EventArgs) Handles GuardarButton.Click
            Guardar()
        End Sub

#End Region

#Region " Metodos "

        Public Sub Load_Data()
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                Me.TBL_Etapa_CapturaDataTableBindingSource.DataSource = dbmImaging.SchemaConfig.TBL_Etapa_Captura.DBGet(Nothing)
                Me.TBL_Modo_Respuesta_AutomaticaDataTableBindingSource.DataSource = dbmImaging.SchemaConfig.TBL_Modo_Respuesta_Automatica.DBGet(Nothing)

                Me.ParametrosDataTable = dbmImaging.SchemaConfig.TBL_Parametro.DBGet(Nothing, 0, New DBImaging.SchemaConfig.TBL_ParametroEnumList(DBImaging.SchemaConfig.TBL_ParametroEnum.Nombre_Parametro, True))
            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (Not dbmImaging Is Nothing) Then dbmImaging.Connection_Close()
            End Try

            Dim Esquema As New DataView(Program.ImagingGlobal.Esquemas)
            Esquema.RowFilter = Program.ImagingGlobal.Esquemas.fk_EntidadColumn.ColumnName & "=" & Program.ImagingGlobal.Entidad _
                                & " AND " & Program.ImagingGlobal.Esquemas.fk_ProyectoColumn.ColumnName & "=" & Program.ImagingGlobal.Proyecto
            Utilities.LlenarCombo(EsquemaDesktopComboBox, Esquema.ToTable(), Program.ImagingGlobal.Esquemas.id_EsquemaColumn, Program.ImagingGlobal.Esquemas.Nombre_EsquemaColumn)
        End Sub

        Private Sub Cargar_Documentos()
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                Dim Documento = dbmImaging.SchemaCore.CTA_Documento.DBFindByfk_Entidadfk_Proyectofk_EsquemaEliminado(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, CShort(EsquemaDesktopComboBox.SelectedValue), False, 0, New DBImaging.SchemaCore.CTA_DocumentoEnumList(DBImaging.SchemaCore.CTA_DocumentoEnum.Nombre_Documento, True))
                Utilities.LlenarCombo(id_DocumentoDesktopComboBox, Documento, Documento.id_DocumentoColumn.ColumnName, Documento.Nombre_DocumentoColumn.ColumnName)
            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (Not dbmImaging Is Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

        Private Sub Cargar_Validaciones()
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                Me.ValidacionesDataTable = dbmImaging.SchemaConfig.CTA_Validacion_Parametrizacion.DBFindByfk_Documento(CInt(id_DocumentoDesktopComboBox.SelectedValue))
                CTA_Validacion_ConfiguracionDataTableBindingSource.DataSource = Me.ValidacionesDataTable

                Me.CamposDataTable = dbmImaging.SchemaConfig.CTA_Campo.DBFindByfk_DocumentoEliminado_Campo(CInt(id_DocumentoDesktopComboBox.SelectedValue), False, 0, New DBImaging.SchemaConfig.CTA_CampoEnumList(DBImaging.SchemaConfig.CTA_CampoEnum.Nombre_Campo, True))

            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (Not dbmImaging Is Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

        Private Sub Configurar_Marca(ByRef nFila As DBImaging.SchemaConfig.CTA_Validacion_ParametrizacionRow)
            Dim f = New FormConfigCampo()
            Dim Cargado As Boolean

            f.X = nFila.Marca_X_Campo
            f.Y = nFila.Marca_Y_Campo
            f.W = nFila.Marca_Width_Campo
            f.H = nFila.Marca_Height_Campo

            If (FileName <> "") Then
                Cargado = f.Cargar(FileName)
            Else
                Cargado = f.Cargar()
            End If

            If (Cargado) Then
                If (f.ShowDialog() = DialogResult.OK) Then
                    FileName = f.FileName

                    nFila.Marca_X_Campo = CByte(f.X)
                    nFila.Marca_Y_Campo = CByte(f.Y)
                    nFila.Marca_Width_Campo = CByte(f.W)
                    nFila.Marca_Height_Campo = CByte(f.H)
                End If
            End If
        End Sub

        Private Sub Configurar_Validacion(ByRef nFila As DBImaging.SchemaConfig.CTA_Validacion_ParametrizacionRow)
            Dim Modo = CType(nFila.fk_Modo_Respuesta_Automatica, DBImaging.EnumModoRespuestaAutomatica)
            If (Modo <> DBImaging.EnumModoRespuestaAutomatica.No_aplica) Then
                Try
                    Dim ConfigForm = ParValidacionesFactory.Create(Modo)

                    ConfigForm.setData(Me.CamposDataTable, Me.ParametrosDataTable)

                    ConfigForm.Respuesta = nFila.Respuesta
                    ConfigForm.Campo_1 = nFila.fk_Campo_1
                    ConfigForm.Campo_2 = nFila.fk_Campo_2
                    ConfigForm.Valor_Comparacion = nFila.Valor_Comparacion
                    ConfigForm.Operador_Comparacion = nFila.Operador_Comparacion

                    Dim Respuesta = CType(ConfigForm, Form).ShowDialog()

                    If (Respuesta = DialogResult.OK) Then
                        nFila.Respuesta = ConfigForm.Respuesta
                        nFila.fk_Campo_1 = ConfigForm.Campo_1
                        nFila.fk_Campo_2 = ConfigForm.Campo_2
                        nFila.Valor_Comparacion = ConfigForm.Valor_Comparacion
                        nFila.Operador_Comparacion = ConfigForm.Operador_Comparacion
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        End Sub

        Private Sub Guardar()
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

                    For Each Fila In ValidacionesDataTable
                        Dim Registro As New DBImaging.SchemaConfig.TBL_ValidacionType

                        Registro.fk_Documento = Fila.fk_Documento
                        Registro.id_Validacion = Fila.id_Validacion

                        Registro.Usa_Marca = Fila.Usa_Marca
                        If (Registro.Usa_Marca) Then
                            Registro.Marca_Height_Campo = Fila.Marca_Height_Campo
                            Registro.Marca_Width_Campo = Fila.Marca_Width_Campo
                            Registro.Marca_X_Campo = Fila.Marca_X_Campo
                            Registro.Marca_Y_Campo = Fila.Marca_Y_Campo
                        Else
                            Registro.Marca_Height_Campo = 0
                            Registro.Marca_Width_Campo = 0
                            Registro.Marca_X_Campo = 0
                            Registro.Marca_Y_Campo = 0
                        End If

                        Registro.Orden_Validacion = Fila.Orden_Validacion
                        Registro.fk_Etapa_Captura = Fila.fk_Etapa_Captura
                        Registro.fk_Modo_Respuesta_Automatica = Fila.fk_Modo_Respuesta_Automatica

                        Select Case CType(Fila.fk_Modo_Respuesta_Automatica, DBImaging.EnumModoRespuestaAutomatica)
                            Case DBImaging.EnumModoRespuestaAutomatica.Comparacion_Campo
                                Registro.Respuesta = False
                                Registro.fk_Campo_1 = Fila.fk_Campo_1
                                Registro.fk_Campo_2 = Fila.fk_Campo_2
                                Registro.Valor_Comparacion = DBNull.Value
                                Registro.Operador_Comparacion = Fila.Operador_Comparacion

                            Case DBImaging.EnumModoRespuestaAutomatica.Comparacion_Constante
                                Registro.Respuesta = False
                                Registro.fk_Campo_1 = Fila.fk_Campo_1
                                Registro.fk_Campo_2 = DBNull.Value
                                Registro.Valor_Comparacion = Fila.Valor_Comparacion
                                Registro.Operador_Comparacion = Fila.Operador_Comparacion

                            Case DBImaging.EnumModoRespuestaAutomatica.Comparacion_Parametro
                                Registro.Respuesta = False
                                Registro.fk_Campo_1 = Fila.fk_Campo_1
                                Registro.fk_Campo_2 = DBNull.Value
                                Registro.Valor_Comparacion = Fila.Valor_Comparacion
                                Registro.Operador_Comparacion = Fila.Operador_Comparacion

                            Case DBImaging.EnumModoRespuestaAutomatica.Constante
                                Registro.Respuesta = Fila.Respuesta
                                Registro.fk_Campo_1 = DBNull.Value
                                Registro.fk_Campo_2 = DBNull.Value
                                Registro.Valor_Comparacion = DBNull.Value
                                Registro.Operador_Comparacion = DBNull.Value

                            Case DBImaging.EnumModoRespuestaAutomatica.No_aplica
                                Registro.Respuesta = False
                                Registro.fk_Campo_1 = DBNull.Value
                                Registro.fk_Campo_2 = DBNull.Value
                                Registro.Valor_Comparacion = DBNull.Value
                                Registro.Operador_Comparacion = DBNull.Value
                        End Select

                        Registro.fk_Usuario_Log = Program.Sesion.Usuario.id
                        Registro.Fecha_Log = DateTime.Now

                        If (dbmImaging.SchemaConfig.TBL_Validacion.DBGet(Registro.fk_Documento, Registro.id_Validacion).Count = 0) Then
                            dbmImaging.SchemaConfig.TBL_Validacion.DBInsert(Registro)
                        Else
                            dbmImaging.SchemaConfig.TBL_Validacion.DBUpdate(Registro, Registro.fk_Documento, Registro.id_Validacion)
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

#End Region

#Region " Funciones "

        Public Function Validar() As Boolean
            Return True
        End Function

#End Region

    End Class
End Namespace