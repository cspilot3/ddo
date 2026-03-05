Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Controls
Imports Miharu.Desktop.Library.Config

Namespace Procesos.Configuracion.Imaging

    Public Class FormContenedorCampo
        Inherits Desktop.Library.FormBase

#Region " Declaraciones "

        Private _contenedorCampoDataTable As DBImaging.SchemaConfig.TBL_Contenedor_CampoDataTable

#End Region

#Region " Eventos "

        Private Sub FormContenedorCampoLoad(sender As System.Object, e As EventArgs) Handles MyBase.Load
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Try
                CargarListas()
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)

                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
                _contenedorCampoDataTable = dbmImaging.SchemaConfig.TBL_Contenedor_Campo.DBGet(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, Nothing)

                CamposDataGridView.AutoGenerateColumns = False
                CamposDataGridView.DataSource = _contenedorCampoDataTable

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargarParametros", ex)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

        Private Sub GuardarButtonClick(sender As System.Object, e As EventArgs) Handles GuardarButton.Click
            Dim agregarCampos = New FormAgregarContenedorCampo
            agregarCampos.ShowDialog()
            FormContenedorCampoLoad(sender, e)
        End Sub

        Private Sub CerrarButton_Click(sender As System.Object, e As EventArgs) Handles CerrarButton.Click
            Me.Close()
        End Sub

        Private Sub Button1Click(sender As System.Object, e As EventArgs) Handles Button1.Click
            GuardarCambios()
        End Sub

        Private Sub CamposDataGridView_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles CamposDataGridView.CellDoubleClick
            Try
                Dim Row = CamposDataGridView.Rows(e.RowIndex)

                Dim agregarCampos = New FormAgregarContenedorCampo
                With agregarCampos
                    .id_CampoTextBox.Text = Row.Value("id_Campo")
                    .Nombre_Campo_TextBox.Text = Row.Value("Nombre_Campo")
                    .CampoTipoTextBox.Text = Row.Value("fk_Campo_Tipo")
                    .CampoListaTextBox.Text = Row.Value("fk_Campo_Lista")
                    .Obligatorio_CheckBox.Checked = Row.Value(Of Boolean)("Es_Obligatorio_Campo")
                    .Longitud_Campo_TextBox.Text = Row.Value("Length_Campo")
                    .Longitud_Minima_TextBox.Text = Row.Value("Length_Min_Campo")
                    .Usa_Decimales_CheckBox.Checked = Row.Value(Of Boolean)("Usa_Decimales")
                    .Cantidad_Decimales_TextBox.Text = Row.Value("Cantidad_Decimales")
                    .EliminadoCheckBox.Checked = Row.Value(Of Boolean)("Eliminado")
                    .OrdenCampo_TextBox.Text = Row.Value("Orden")
                End With

                agregarCampos.ShowDialog()
                FormContenedorCampoLoad(sender, e)
            Catch ex As Exception

            End Try

        End Sub

#End Region

#Region " Metodos "

        Private Sub CargarListas()
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing

            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                Dim CampoTipo = dbmCore.SchemaConfig.TBL_Campo_Tipo.DBGet(Nothing)
                Dim CampoLista = dbmCore.SchemaConfig.TBL_Campo_Lista.DBGet(Program.ImagingGlobal.Entidad, Nothing)

                'CampoTipo
                Dim campoTipoColumn As DataGridViewComboBoxColumn = CType(CamposDataGridView.Columns("fk_Campo_Tipo"), DataGridViewComboBoxColumn)
                campoTipoColumn.DataSource = CampoTipo
                campoTipoColumn.DisplayMember = CampoTipo.Nombre_Campo_TipoColumn.ColumnName
                campoTipoColumn.ValueMember = CampoTipo.id_Campo_TipoColumn.ColumnName

                'CampoLista
                Dim campoListaColumn As DataGridViewComboBoxColumn = CType(CamposDataGridView.Columns("fk_Campo_Lista"), DataGridViewComboBoxColumn)
                campoListaColumn.DataSource = CampoLista
                campoListaColumn.DisplayMember = CampoLista.Nombre_Campo_ListaColumn.ColumnName
                campoListaColumn.ValueMember = CampoLista.id_Campo_ListaColumn.ColumnName

            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            End Try
        End Sub

        Private Sub GuardarCambios()
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
                dbmImaging.Transaction_Begin()

                dbmImaging.SchemaConfig.TBL_Contenedor_Campo.DBSaveTable(_contenedorCampoDataTable)

                dbmImaging.Transaction_Commit()
                DesktopMessageBoxControl.DesktopMessageShow("Se han guardado los datos con éxito.", "Campo Imaging", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                CamposDataGridView.Refresh()

            Catch ex As Exception
                If (dbmImaging IsNot Nothing) Then dbmImaging.Transaction_Rollback()
                DesktopMessageBoxControl.DesktopMessageShow("Hubo problemas al guardar los datos, por favor comuniquese con el administrador" & vbCrLf & vbCrLf & ex.Message, "Problemas en Campo Imaging", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

#End Region

    End Class

End Namespace