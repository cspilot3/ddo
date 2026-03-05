Imports DBCore.SchemaConfig
Imports DBCore.SchemaSecurity
Imports Miharu.Desktop.Library.Config

Namespace Forms.Parametrizacion

    Public Class FormCategoria

#Region " Eventos "

        Private Sub FormCategoria_Load(sender As System.Object, e As EventArgs) Handles MyBase.Load
            CargarEntidades()
        End Sub

        Private Sub entidadComboBox_SelectedIndexChanged(sender As System.Object, e As EventArgs) Handles entidadComboBox.SelectedIndexChanged
            CargarCategorias()
        End Sub

        Private Sub categoriaDataGridView_SelectionChanged(sender As System.Object, e As EventArgs) Handles categoriaDataGridView.SelectionChanged
            editButton.Enabled = (categoriaDataGridView.CurrentRow IsNot Nothing)
        End Sub

        Private Sub addButton_Click(sender As System.Object, e As EventArgs) Handles addButton.Click
            AgregarCategoria()
        End Sub

        Private Sub editButton_Click(sender As System.Object, e As EventArgs) Handles editButton.Click
            EditarCategoria()
        End Sub

        Private Sub closeButton_Click(sender As System.Object, e As EventArgs) Handles closeButton.Click
            Me.Close()
        End Sub

#End Region

#Region " Metodos "

        Private Sub CargarEntidades()
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing

            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)

                dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                Dim EntidadDataTable = dbmCore.SchemaSecurity.CTA_Entidad.DBGet(0, New CTA_EntidadEnumList(CTA_EntidadEnum.Nombre_Entidad, True))
                entidadComboBox.Fill(EntidadDataTable, EntidadDataTable.id_EntidadColumn, EntidadDataTable.Nombre_EntidadColumn)

            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            End Try

            CargarCategorias()
        End Sub

        Private Sub CargarCategorias()
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing

            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                Dim idEntidad = CType(entidadComboBox.SelectedValue, Short)
                Dim categoriaDataTable = dbmCore.SchemaConfig.TBL_Categoria.DBGet(idEntidad, Nothing, 0, New TBL_CategoriaEnumList(TBL_CategoriaEnum.Nombre_Categoria, True))
                categoriaDataGridView.DataSource = categoriaDataTable

            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            End Try
        End Sub

        Private Sub AgregarCategoria()
            Dim categoria As String
            Dim result = Slyg.Tools.InputBox.Show("Nombre de la Categoría", "Ingrese el nombre de la nueva Categoría", categoria)
            If (result <> DialogResult.OK) Then Return
            If (categoria = "") Then
                MessageBox.Show("Debe ingresar el nombre de la Categoría", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing

            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)

                dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                Dim idEntidad = CType(entidadComboBox.SelectedValue, Short)
                Dim idCategoria = dbmCore.SchemaConfig.TBL_Categoria.DBNextId_for_id_Categoria(idEntidad)
                dbmCore.SchemaConfig.TBL_Categoria.DBInsert(idEntidad, idCategoria, categoria)

            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            End Try

            CargarCategorias()
        End Sub

        Private Sub EditarCategoria()
            Dim row = CType(CType(categoriaDataGridView.CurrentRow.DataBoundItem, DataRowView).Row, TBL_CategoriaRow)
            Dim categoria = row.Nombre_Categoria
            Dim result = Slyg.Tools.InputBox.Show("Nombre de la Categoría", "Ingrese el nombre de la nueva Categoría", categoria)
            If (result <> DialogResult.OK) Then Return
            If (categoria = "") Then
                MessageBox.Show("Debe ingresar el nombre de la Categoría", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing

            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)

                dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                Dim idEntidad = CType(entidadComboBox.SelectedValue, Short)
                dbmCore.SchemaConfig.TBL_Categoria.DBUpdate(Nothing, Nothing, categoria, idEntidad, row.id_Categoria)

            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            End Try

            CargarCategorias()
        End Sub

#End Region

    End Class

End Namespace