Imports Miharu.Desktop.Library.Config

Namespace Forms.Parametrizacion

    Public Class FormParametrizacionCampos

#Region " Declaraciones "

        Dim cargaEntidad As Boolean
        Dim cargaProyecto As Boolean
        Dim cargaEsquema As Boolean

#End Region

#Region "Eventos"

        Private Sub FormParametrizacionCampos_Load(sender As System.Object, e As EventArgs) Handles MyBase.Load
            CargarEntidades()
        End Sub

        Private Sub EntidadComboBox_SelectedIndexChanged(sender As System.Object, e As EventArgs) Handles EntidadComboBox.SelectedIndexChanged
            CargarProyectos()
        End Sub

        Private Sub ProyectoComboBox_SelectedIndexChanged(sender As System.Object, e As EventArgs) Handles ProyectoComboBox.SelectedIndexChanged
            CargarEsquemas()
        End Sub

        Private Sub EsquemaComboBox_SelectedIndexChanged(sender As System.Object, e As EventArgs) Handles EsquemaComboBox.SelectedIndexChanged
            CargarDocumentos()
        End Sub

        Private Sub BuscarButton_Click(sender As System.Object, e As EventArgs)
            Buscar()
        End Sub

        Private Sub CamposDataGridView_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles CamposDataGridView.CellDoubleClick
            Try
                Dim Row = CamposDataGridView.Rows(e.RowIndex)

                Dim agregarCampos = New FormNuevoCampo
                With agregarCampos
                    .Entidad = Row.Value(Of Short)("fk_Entidad")
                    .Documento = Row.Value(Of Integer)("fk_Documento")
                    .Id_Campo = Row.Value(Of Short)("id_Campo")
                    .fk_Campo_Tipo = Row.Value(Of Byte)("fk_Campo_Tipo")
                    .Es_Campo_Busqueda = Row.Value(Of Boolean)("Es_Campo_Busqueda")
                    .Nombre_Campo = Row.Value("Nombre_Campo")
                    .Es_Campo_Folder = Row.Value(Of Boolean)("Es_Campo_Folder")
                    .Es_Obligatorio_Campo = Row.Value(Of Boolean)("Es_Obligatorio_Campo")
                    .Length_Min_Campo = Row.Value("Length_Min_Campo")
                    .Es_Exportable = Row.Value(Of Boolean)("Es_Exportable")
                    .Eliminado_Campo = Row.Value(Of Boolean)("Eliminado_Campo")
                    .Usa_Decimales = Row.Value(Of Boolean)("Usa_Decimales")
                    .Caracter_Decimal = Row.Value("Caracter_Decimal")
                    .Body_Query = Row.Value("Body_Query")
                    .Validar_Registros = Row.Value(Of Boolean)("Validar_Registros")
                    .Validar_Totales = Row.Value(Of Boolean)("Validar_Totales")
                    .Valor_por_Defecto = Row.Value("Valor_por_Defecto")
                    .fk_Campo_Busqueda = Row.Value(Of Short)("fk_Campo_Busqueda")
                    .Length_Campo = Row.Value(Of Short)("Length_Campo")
                    .Cantidad_Decimales = Row.Value(Of Short)("Cantidad_Decimales")
                    .fk_Campo_Lista = Row.Value(Of Short)("fk_Campo_Lista")
                    .Campo_Control_Duplicado = Row.Value(Of Boolean)("Campo_Control_Duplicado")
                End With

                agregarCampos.ShowDialog()
                Buscar()

            Catch
                Throw
            End Try

        End Sub

        Private Sub GuardarButton_Click(sender As System.Object, e As EventArgs) Handles GuardarButton.Click
            Dim NuevoCampoForm = New FormNuevoCampo()
            NuevoCampoForm.Entidad = CShort(EntidadComboBox.SelectedValue)
            NuevoCampoForm.Documento = CInt(DocumentoComboBox.SelectedValue)
            NuevoCampoForm.ShowDialog()
            Buscar()
        End Sub

        Private Sub DocumentoComboBox_SelectedIndexChanged(sender As System.Object, e As EventArgs) Handles DocumentoComboBox.SelectedIndexChanged
            Buscar()
        End Sub
        

#End Region

#Region " Metodos "

        Private Sub CargarEntidades()
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing

            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)

                cargaEntidad = True
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                Dim EntidadDataTable = dbmCore.SchemaSecurity.CTA_Entidad.DBGet()
                EntidadComboBox.Fill(EntidadDataTable, EntidadDataTable.id_EntidadColumn, EntidadDataTable.Nombre_EntidadColumn)

                cargaEntidad = False
            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            End Try

            CargarProyectos()
        End Sub

        Private Sub CargarProyectos()
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing

            Try
                If (Not cargaEntidad) Then
                    dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)

                    cargaProyecto = True
                    dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                    Dim Entidad = CShort(EntidadComboBox.SelectedValue)
                    Dim ProyectoDataTable = dbmCore.SchemaConfig.TBL_Proyecto.DBGet(Entidad, Nothing)
                    ProyectoComboBox.Fill(ProyectoDataTable, ProyectoDataTable.id_ProyectoColumn, ProyectoDataTable.Nombre_ProyectoColumn)

                    cargaProyecto = False
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                CargarEsquemas()
            End Try
        End Sub

        Private Sub CargarEsquemas()
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing

            Try
                If Not cargaProyecto And Not cargaEntidad Then
                    dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)

                    cargaEsquema = True
                    dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                    Dim Entidad = CShort(EntidadComboBox.SelectedValue)
                    Dim Proyecto = CShort(ProyectoComboBox.SelectedValue)

                    Dim EsquemaDataTable = dbmCore.SchemaConfig.TBL_Esquema.DBGet(Entidad, Proyecto, Nothing)
                    EsquemaComboBox.Fill(EsquemaDataTable, EsquemaDataTable.id_EsquemaColumn, EsquemaDataTable.Nombre_EsquemaColumn)

                    cargaEsquema = False
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                CargarDocumentos()
            End Try
        End Sub

        Private Sub CargarDocumentos()
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing

            Try
                If Not cargaProyecto And Not cargaEntidad And Not cargaEsquema Then
                    dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)

                    dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                    Dim Entidad = CShort(EntidadComboBox.SelectedValue)
                    Dim Proyecto = CShort(ProyectoComboBox.SelectedValue)
                    Dim Esquema = CShort(EsquemaComboBox.SelectedValue)

                    Dim DocumentoDataTable = dbmCore.SchemaConfig.CTA_Documento.DBFindByfk_Entidadfk_Proyectofk_EsquemaEliminado(Entidad, Proyecto, Esquema, False)
                    DocumentoComboBox.Fill(DocumentoDataTable, DocumentoDataTable.id_DocumentoColumn, DocumentoDataTable.Nombre_DocumentoColumn)

                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            End Try
        End Sub

        Private Sub Buscar()
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing

            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                Dim camposDataTable = dbmCore.SchemaConfig.PA_Busqueda_Campos.DBExecute(CType(EntidadComboBox.SelectedValue, Short), CType(EsquemaComboBox.SelectedValue, Short), CType(ProyectoComboBox.SelectedValue, Integer), CType(DocumentoComboBox.SelectedValue, Integer))
                CamposDataGridView.DataSource = camposDataTable

            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            End Try

        End Sub

#End Region

        Private Sub ConfigurarTablaAsociada(tblCampoRow As DataRow)
            Dim fTablaAsociada As New FormTablaAsociada()

            Dim fk_Entidad = CShort(tblCampoRow("fk_Entidad"))
            Dim fk_Documento = CShort(tblCampoRow("fk_Documento"))
            Dim id_Campo = CShort(tblCampoRow("id_Campo"))

            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                With tblCampoRow
                    Dim campoCoreRow = dbmCore.SchemaConfig.TBL_Campo.DBFindByfk_Entidadfk_Documentoid_Campo(fk_Entidad, fk_Documento, id_Campo)

                    If campoCoreRow.Rows.Count > 0 Then
                        Dim campoRow = CType(campoCoreRow.Rows(0), DBCore.SchemaConfig.TBL_CampoRow)
                        If (CShort(tblCampoRow("fk_Campo_Tipo")) = 9) Then

                            fTablaAsociada.NombreEntidad = EntidadComboBox.Text
                            fTablaAsociada.EntidadId = campoRow.fk_Entidad
                            fTablaAsociada.NombreDocumento = DocumentoComboBox.Text
                            fTablaAsociada.DocumentoId = campoRow.fk_Documento
                            fTablaAsociada.NombreCampo = tblCampoRow("Nombre_Campo").ToString
                            fTablaAsociada.CampoId = CShort(tblCampoRow("id_Campo"))

                            If fTablaAsociada.ShowDialog() = Windows.Forms.DialogResult.OK Then
                                MessageBox.Show("Tabla Asociada parametrizada con éxito", "Tabla Asociada", MessageBoxButtons.OK, MessageBoxIcon.Information)
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

        Private Sub CamposDataGridView_CellClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles CamposDataGridView.CellClick
            If (e.ColumnIndex = 0 And e.RowIndex >= 0) Then
                ConfigurarTablaAsociada(CType(CamposDataGridView.Rows(e.RowIndex).DataBoundItem, DataRowView).Row)
            End If
        End Sub
    End Class

End Namespace
