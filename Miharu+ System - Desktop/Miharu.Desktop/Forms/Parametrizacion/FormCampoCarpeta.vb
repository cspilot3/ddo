Imports Miharu.Desktop.Library.Config

Namespace Forms.Parametrizacion

    Public Class FormCampoCarpeta

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
            DocumentoComboBox.SelectedIndex = -1
            Buscar()
        End Sub

        Private Sub BuscarButton_Click(sender As System.Object, e As EventArgs)
            Buscar()
        End Sub

        Private Sub CamposDataGridView_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles CamposDataGridView.CellDoubleClick
            Try
                Dim Row = CamposDataGridView.Rows(e.RowIndex)

                Dim agregarCampos = New FormNuevoCampoCarpeta
                With agregarCampos
                    .Entidad = Row.Value(Of Short)("fk_Entidad")
                    .Proyecto = Row.Value(Of Short)("fk_Proyecto")
                    .Esquema = Row.Value(Of Short)("fk_Esquema")
                    .Documento = Row.Value(Of Integer)("fk_Documento")
                    .Id_Campo = Row.Value(Of Short)("id_Campo")
                    .fk_Campo_Tipo = Row.Value(Of Byte)("fk_Campo_Tipo")
                    .Nombre_Campo = Row.Value("Nombre_Campo")
                    .Precaptura = Row.Value(Of Boolean)("Es_Campo_Indexacion")
                    .PrimeraCaptura = Row.Value(Of Boolean)("Usa_Captura")
                    .UsaDobleCaptura = Row.Value(Of Boolean)("Usa_Doble_Captura")
                    .Eliminado_Campo = Row.Value(Of Boolean)("Eliminado_Campo")
                    .Length_Min_Campo = Row.Value("Length_Min_Campo")
                    .Length_Campo = Row.Value(Of Short)("Length_Campo")
                    .Usa_Marca = Row.Value(Of Boolean)("Usa_Marca")
                    .Marca_X_Campo = Row.Value(Of Byte)("Marca_X_Campo")
                    .Marca_Y_Campo = Row.Value(Of Byte)("Marca_Y_Campo")
                    .Marca_Width_Campo = Row.Value(Of Byte)("Marca_Width_Campo")
                    .Marca_Height_Campo = Row.Value(Of Byte)("Marca_Height_Campo")
                    .fk_Tipo_Llave = Row.Value(Of Byte)("fk_Tipo_Llave")
                End With

                agregarCampos.ShowDialog()
                Buscar()

            Catch ex As Exception
                Throw New Exception("Error en la carga del control")
            End Try

        End Sub

        Private Sub GuardarButton_Click(sender As System.Object, e As EventArgs) Handles GuardarButton.Click
            Dim NuevoCampoLlaveForm = New FormNuevoCampoCarpeta()
            NuevoCampoLlaveForm.Entidad = CShort(EntidadComboBox.SelectedValue)
            NuevoCampoLlaveForm.Proyecto = CShort(ProyectoComboBox.SelectedValue)
            NuevoCampoLlaveForm.Esquema = CShort(EsquemaComboBox.SelectedValue)
            NuevoCampoLlaveForm.Id_Campo = -1

            Try
                Select Case Convert.ToInt16(DocumentoComboBox.SelectedValue)
                    Case -1, Nothing
                        Throw New Exception("Debe seleccionar un Tipo de Documento")
                End Select
            Catch ex As Exception
                Throw New Exception("Debe seleccionar un Tipo de Documento")
            End Try

            NuevoCampoLlaveForm.Documento = CInt(DocumentoComboBox.SelectedValue)
            NuevoCampoLlaveForm.ShowDialog()
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

                If CType(EntidadComboBox.SelectedValue, Short) > 0 And CType(EsquemaComboBox.SelectedValue, Short) > 0 And CType(ProyectoComboBox.SelectedValue, Integer) > 0 Then
                    Dim camposDataTable = dbmCore.SchemaConfig.PA_Busqueda_Campos_Llave.DBExecute(CType(EntidadComboBox.SelectedValue, Short), CType(EsquemaComboBox.SelectedValue, Short), CType(ProyectoComboBox.SelectedValue, Integer), CType(DocumentoComboBox.SelectedValue, Integer))
                    CamposDataGridView.DataSource = camposDataTable

                End If

            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            End Try

        End Sub

#End Region
    End Class

End Namespace
