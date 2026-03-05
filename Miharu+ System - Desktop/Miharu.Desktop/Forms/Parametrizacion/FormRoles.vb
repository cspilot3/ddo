Imports DBCore.SchemaSecurity

Namespace Forms.Parametrizacion

    Public Class FormRoles

#Region " Eventos "

        Private Sub FormRoles_Load(sender As System.Object, e As EventArgs) Handles MyBase.Load
            CargarDatos()
        End Sub

        Private Sub rolesDataGridView_SelectionChanged(sender As System.Object, e As EventArgs) Handles rolesDataGridView.SelectionChanged
            ActivarOpciones()
        End Sub

        Private Sub closeButton_Click(sender As System.Object, e As EventArgs) Handles closeButton.Click
            Me.Close()
        End Sub

        Private Sub saveButton_Click(sender As System.Object, e As EventArgs) Handles saveButton.Click
            Guardar()
        End Sub

        Private Sub deleteButton_Click(sender As System.Object, e As EventArgs) Handles deleteButton.Click
            EliminarEsquema()
        End Sub

        Private Sub addButton_Click(sender As System.Object, e As EventArgs) Handles addButton.Click
            AgregarEsquema()
        End Sub

#End Region

#Region " Metodos "

        Private Sub CargarDatos()
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing

            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                dbmCore.SchemaSecurity.CTA_Rol.DBFill(rolesDataSet.Rol, 0, New CTA_RolEnumList(CTA_RolEnum.Nombre_Rol, True))
                dbmCore.SchemaSecurity.CTA_Rol_Esquema.DBFill(rolesDataSet.Esquema)
                dbmCore.SchemaSecurity.CTA_Rol_Documento.DBFill(rolesDataSet.Documento)
                dbmCore.SchemaSecurity.CTA_Rol_Categoria.DBFill(rolesDataSet.Categoria)

            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            End Try
        End Sub

        Private Sub AgregarEsquema()
            Dim f = New FormSeleccionarEsquema()
            Dim result = f.ShowDialog()

            If (result = DialogResult.OK) Then
                Dim rolRow = CType(CType(Me.rolesDataGridView.CurrentRow.DataBoundItem, DataRowView).Row, FormRolesDataSet.RolRow)

                If (rolesDataSet.Esquema.FindByfk_Rolid_Entidadid_Proyectoid_Esquema(rolRow.id_Rol, f.IdEntidad, f.IdProyecto, f.IdEsquema) Is Nothing) Then
                    rolesDataSet.Esquema.AddEsquemaRow(rolRow, f.IdEntidad, f.NombreEntidad, f.IdProyecto, f.NombreProyecto, f.IdEsquema, f.NombreEsquema)

                    Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
                    Try
                        dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                        dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                        Dim documentos = dbmCore.SchemaConfig.TBL_Documento.DBFindByfk_Entidadfk_Proyectofk_Esquema(f.IdEntidad, f.IdProyecto, f.IdEsquema)
                        Dim categorias = dbmCore.SchemaConfig.TBL_Categoria.DBGet(f.IdEntidad, Nothing)

                        For Each row In documentos
                            rolesDataSet.Documento.AddDocumentoRow(rolRow.id_Rol, f.IdEntidad, f.IdProyecto, f.IdEsquema, row.id_Documento, row.Nombre_Documento, False, False, False, False)
                        Next

                        For Each row In categorias
                            rolesDataSet.Categoria.AddCategoriaRow(rolRow.id_Rol, f.IdEntidad, f.IdProyecto, f.IdEsquema, row.id_Categoria, row.Nombre_Categoria, False)
                        Next

                    Catch ex As Exception
                        MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Finally
                        If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                    End Try

                    rolesDataSet.AcceptChanges()
                End If
            End If            
        End Sub

        Private Sub EliminarEsquema()
            Dim result = MessageBox.Show("¿Está seguro que desea eliminar la configuración del esquema seleccionado?", Program.AssemblyTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            If (result = DialogResult.Yes) Then
                Dim esquemaRow = CType(CType(Me.esquemasDataGridView.CurrentRow.DataBoundItem, DataRowView).Row, FormRolesDataSet.EsquemaRow)

                esquemaRow.Delete()
                rolesDataSet.AcceptChanges()
            End If
        End Sub

        Private Sub Guardar()
            Me.Cursor = Cursors.WaitCursor
            Me.Enabled = False

            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing

            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                dbmCore.Transaction_Begin()

                dbmCore.DataBase.BeginBulkQuery()
                
                dbmCore.SchemaSecurity.TBL_Rol_Documento.DBDelete(Nothing, Nothing, Nothing, Nothing, Nothing)
                dbmCore.SchemaSecurity.TBL_Rol_Categoria.DBDelete(Nothing, Nothing, Nothing, Nothing, Nothing)
                dbmCore.SchemaSecurity.TBL_Rol_Esquema.DBDelete(Nothing, Nothing, Nothing, Nothing)

                For Each row In rolesDataSet.Esquema
                    Dim newRow = New TBL_Rol_EsquemaType()

                    newRow.fk_Rol = row.fk_Rol
                    newRow.fk_Entidad = row.id_Entidad
                    newRow.fk_Proyecto = row.id_Proyecto
                    newRow.fk_Esquema = row.id_Esquema

                    dbmCore.SchemaSecurity.TBL_Rol_Esquema.DBInsert(newRow)
                Next

                For Each row In rolesDataSet.Documento                    
                    If (row.Ver_Registro Or row.Ver_Data Or row.Ver_Imagen Or row.Descargar) Then
                        Dim newRow = New TBL_Rol_DocumentoType()
                        newRow.fk_Rol = row.fk_Rol
                        newRow.fk_Entidad = row.fk_Entidad
                        newRow.fk_Proyecto = row.fk_Proyecto
                        newRow.fk_Esquema = row.fk_Esquema
                        newRow.fk_Documento = row.id_Documento
                        newRow.Ver_Registro = row.Ver_Registro
                        newRow.Ver_Imagen = row.Ver_Imagen
                        newRow.Ver_Data = row.Ver_Data
                        newRow.Descargar = row.Descargar
                        dbmCore.SchemaSecurity.TBL_Rol_Documento.DBInsert(newRow)
                    End If
                Next

                For Each row In rolesDataSet.Categoria
                    Dim newRow = New TBL_Rol_CategoriaType()
                    If (row.Seleccionado) Then
                        newRow.fk_Rol = row.fk_Rol
                        newRow.fk_Entidad = row.fk_Entidad
                        newRow.fk_Proyecto = row.fk_Proyecto
                        newRow.fk_Esquema = row.fk_Esquema
                        newRow.fk_Categoria = row.id_Categoria
                        dbmCore.SchemaSecurity.TBL_Rol_Categoria.DBInsert(newRow)
                    End If
                Next

                While dbmCore.DataBase.BulkQueryLines > 0
                    dbmCore.DataBase.SendBulkQuery(1000)
                End While

                dbmCore.DataBase.EndBulkQuery()
                dbmCore.Transaction_Commit()

                MessageBox.Show("Los datos se almacenaron correctamente", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)

            Catch ex As Exception
                dbmCore.Transaction_Rollback()
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                Me.Cursor = Cursors.Default
                Me.Enabled = True

                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            End Try
        End Sub

        Private Sub ActivarOpciones()
            Me.addButton.Enabled = Me.rolesDataGridView.CurrentRow IsNot Nothing
            Me.deleteButton.Enabled = Me.rolesDataGridView.CurrentRow IsNot Nothing
        End Sub
#End Region

    End Class

End Namespace