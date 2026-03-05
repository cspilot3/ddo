Imports Miharu.Desktop.Library.Config

Namespace Forms.Parametrizacion
    Public Class FormDocumento

#Region " Declaraciones "

        Dim cargaEntidad As Boolean
        Dim cargaProyecto As Boolean
        Dim cargaDocumentos As Boolean

#End Region

#Region " Eventos "

        Private Sub FormDocumento_Load(sender As System.Object, e As EventArgs) Handles MyBase.Load
            CargarEntidades()
        End Sub

        Private Sub EntidadComboBox_SelectedIndexChanged(sender As System.Object, e As EventArgs) Handles EntidadComboBox.SelectedIndexChanged
            CargarProyectos()
        End Sub

        Private Sub ProyectoComboBox_SelectedIndexChanged(sender As System.Object, e As EventArgs) Handles ProyectoComboBox.SelectedIndexChanged
            CargarEsquemas()
        End Sub

        Private Sub BuscarButton_Click(sender As System.Object, e As EventArgs) Handles BuscarButton.Click
            BuscarDocumentos()
        End Sub

        Private Sub CrearOTButton_Click(sender As System.Object, e As EventArgs) Handles NuevoDocumentoButton.Click
            Dim fNuevoDocumento As New FormNuevoDocumento
            With fNuevoDocumento
                .fkEntidad = CShort(EntidadComboBox.SelectedValue)
                .fkProyecto = CShort(ProyectoComboBox.SelectedValue)
                .fkEsquema = CShort(EsquemaComboBox.SelectedValue)
                .Entidad = EntidadComboBox.Text
                .Proyecto = ProyectoComboBox.Text
                .Esquema = EsquemaComboBox.Text
            End With

            If fNuevoDocumento.ShowDialog() = DialogResult.OK Then
                BuscarDocumentos()
            End If
        End Sub

        Private Sub DocumentoDataGridView_CellDoubleClick(sender As System.Object, e As DataGridViewCellEventArgs) Handles DocumentoDataGridView.CellDoubleClick
            Dim Row = DocumentoDataGridView.Rows(DocumentoDataGridView.SelectedCells(0).RowIndex)

            Dim fNuevoDocumento As New FormNuevoDocumento
            With fNuevoDocumento
                .fkEntidad = CShort(EntidadComboBox.SelectedValue)
                .fkProyecto = CShort(ProyectoComboBox.SelectedValue)
                .fkEsquema = CShort(EsquemaComboBox.SelectedValue)
                .Entidad = EntidadComboBox.Text
                .Proyecto = ProyectoComboBox.Text
                .Esquema = EsquemaComboBox.Text
                .IdDocumento = Row.Value("id_Documento")
                .NombreDocumento = Row.Value("Nombre_Documento")
                .FkTipologia = Row.Value(Of Short)("fk_Tipologia")
                .DocumentoGrupo = Row.Value(Of Short)("fk_Documento_Grupo")
            End With

            If fNuevoDocumento.ShowDialog() = DialogResult.OK Then
                BuscarDocumentos()
            End If
        End Sub

        Private Sub EliminarButton_Click(sender As System.Object, e As EventArgs) Handles EliminarButton.Click
            Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)

            Try
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)
                Dim DocumentoType As New DBCore.SchemaConfig.TBL_DocumentoType

                Dim Row = DocumentoDataGridView.Rows(DocumentoDataGridView.SelectedCells(0).RowIndex)
                Dim EliminadoCampo = Row.Value(Of Boolean)("Eliminado")
                Dim idDocumento = Row.Value(Of Integer)("id_Documento")

                DocumentoType.Eliminado = Not EliminadoCampo
                dbmCore.SchemaConfig.TBL_Documento.DBUpdate(DocumentoType, idDocumento)

                If DocumentoType.Eliminado Then
                    MessageBox.Show("Documento eliminado con éxito.", "Documentos", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    MessageBox.Show("Documento habilitado con éxito.", "Documentos", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

                BuscarDocumentos()
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try

        End Sub

        Private Sub DocumentoDataGridView_SelectionChanged(sender As System.Object, e As EventArgs) Handles DocumentoDataGridView.SelectionChanged
            If Not cargaDocumentos Then
                Dim Row = DocumentoDataGridView.Rows(DocumentoDataGridView.SelectedCells(0).RowIndex)

                If Row IsNot Nothing Then
                    If Row.Value(Of Boolean)("Eliminado") Then
                        EliminarButton.Text = "Habilitar Documento"
                    Else
                        EliminarButton.Text = "Inhabilitar Documento"
                    End If
                End If
            End If
        End Sub

#End Region

#Region " Metodos "

        Private Sub CargarEntidades()
            Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)

            Try
                cargaEntidad = True
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                Dim Orden As New DBCore.SchemaSecurity.CTA_EntidadEnumList
                Orden.Add(DBCore.SchemaSecurity.CTA_EntidadEnum.Nombre_Entidad, True)
                Dim DocumentoDataTable = dbmCore.SchemaSecurity.CTA_Entidad.DBGet(0, Orden)
                EntidadComboBox.Fill(DocumentoDataTable, DocumentoDataTable.id_EntidadColumn, DocumentoDataTable.Nombre_EntidadColumn)

                cargaEntidad = False
            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                dbmCore.Connection_Close()
                CargarProyectos()
            End Try


        End Sub

        Private Sub CargarProyectos()
            Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)

            Try
                If (Not cargaEntidad) Then
                    cargaProyecto = True
                    dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                    Dim Entidad = CShort(EntidadComboBox.SelectedValue)
                    Dim ProyectoDataTable = dbmCore.SchemaConfig.TBL_Proyecto.DBGet(Entidad, Nothing)
                    ProyectoComboBox.Fill(ProyectoDataTable, ProyectoDataTable.id_ProyectoColumn, ProyectoDataTable.Nombre_ProyectoColumn)

                    cargaProyecto = False
                End If
            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                dbmCore.Connection_Close()
                CargarEsquemas()
            End Try
        End Sub

        Private Sub CargarEsquemas()
            Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)

            Try
                If Not cargaProyecto And Not cargaEntidad Then
                    dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                    Dim Entidad = CShort(EntidadComboBox.SelectedValue)
                    Dim Proyecto = CShort(ProyectoComboBox.SelectedValue)
                    Dim EsquemaDataTable = dbmCore.SchemaConfig.TBL_Esquema.DBGet(Entidad, Proyecto, Nothing)
                    EsquemaComboBox.Fill(EsquemaDataTable, EsquemaDataTable.id_EsquemaColumn, EsquemaDataTable.Nombre_EsquemaColumn)

                End If
            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                dbmCore.Connection_Close()
            End Try
        End Sub

        Private Sub BuscarDocumentos()
            Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)

            Try
                cargaDocumentos = True
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                Dim Entidad = CShort(EntidadComboBox.SelectedValue)
                Dim Proyecto = CShort(ProyectoComboBox.SelectedValue)
                Dim Esquema = CShort(EsquemaComboBox.SelectedValue)
                Dim DocumentoDataTable As DataTable = dbmCore.SchemaConfig.CTA_Documento.DBFindByfk_Entidadfk_Proyectofk_EsquemaEliminado(Entidad, Proyecto, Esquema, Nothing)

                DocumentoDataGridView.DataSource = DocumentoDataTable
                cargaDocumentos = False

            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                dbmCore.Connection_Close()
            End Try
        End Sub

#End Region

    End Class
End Namespace