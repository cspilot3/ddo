Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Controls
Imports Miharu.Desktop.Library.Config

Namespace Procesos.Configuracion.Imaging

    Public Class FormEmpaqueCampo
        Inherits Desktop.Library.FormBase

#Region " Declaraciones "

        Private _empaqueCampoDataTable As DBImaging.SchemaConfig.TBL_Empaque_CampoDataTable

#End Region

#Region " Eventos "

        Private Sub FormempaqueCampoLoad(sender As System.Object, e As EventArgs) Handles MyBase.Load
            CargarDatos()
        End Sub

        Private Sub GuardarButtonClick(sender As System.Object, e As EventArgs) Handles GuardarButton.Click
            Dim agregarCampos = New FormAgregarEmpaqueCampo
            agregarCampos.ShowDialog()
            CargarDatos()
        End Sub

        Private Sub CerrarButton_Click(sender As System.Object, e As EventArgs) Handles CerrarButton.Click
            Me.Close()
        End Sub

        Private Sub Button1Click(sender As System.Object, e As EventArgs) Handles Button1.Click
            GuardarCambios()
        End Sub

        Private Sub CamposDataGridView_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles CamposDataGridView.CellDoubleClick
            Try
                Dim agregarCampos = New FormAgregarempaqueCampo
                Dim Row = CamposDataGridView.Rows(e.RowIndex)

                With agregarCampos
                    .id_CampoTextBox.Text = Row.Cells("id_Campo").Value.ToString()
                    .Nombre_Campo_TextBox.Text = Row.Cells("Nombre_Campo").Value.ToString()
                    .CampoTipoTextBox.Text = Row.Cells("fk_Campo_Tipo").Value.ToString()
                    .CampoListaTextBox.Text = Row.Cells("fk_Campo_Lista").Value.ToString()
                    .Obligatorio_CheckBox.Checked = CBool(Row.Cells("Es_Obligatorio_Campo").Value)
                    .Longitud_Campo_TextBox.Text = Row.Cells("Length_Campo").Value.ToString()
                    .Longitud_Minima_TextBox.Text = Row.Cells("Length_Min_Campo").Value.ToString()
                    .Usa_Decimales_CheckBox.Checked = CBool(Row.Cells("Usa_Decimales").Value)
                    .Cantidad_Decimales_TextBox.Text = Row.Cells("Cantidad_Decimales").Value.ToString()
                    .EliminadoCheckBox.Checked = CBool(Row.Cells("Eliminado").Value)
                End With

                agregarCampos.ShowDialog()
                CargarDatos()

            Catch
                Throw
            End Try
        End Sub

#End Region

#Region " Metodos "

        Private Sub CargarListas()
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing

            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                'CampoTipo
                Dim campoTipoColumn As DataGridViewComboBoxColumn = CType(CamposDataGridView.Columns("fk_Campo_Tipo"), DataGridViewComboBoxColumn)
                campoTipoColumn.DataSource = dbmCore.SchemaConfig.TBL_Campo_Tipo.DBGet(Nothing)
                campoTipoColumn.DisplayMember = "Nombre_Campo_Tipo"
                campoTipoColumn.ValueMember = "id_Campo_Tipo"

                'CampoLista
                Dim campoListaColumn As DataGridViewComboBoxColumn = CType(CamposDataGridView.Columns("fk_Campo_Lista"), DataGridViewComboBoxColumn)
                campoListaColumn.DataSource = dbmCore.SchemaConfig.TBL_Campo_Lista.DBGet(Program.ImagingGlobal.Entidad, Nothing)
                campoListaColumn.DisplayMember = "Nombre_Campo_Lista"
                campoListaColumn.ValueMember = "id_Campo_Lista"

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

                dbmImaging.SchemaConfig.TBL_Empaque_Campo.DBSaveTable(_empaqueCampoDataTable)

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

        Private Sub CargarDatos()
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                CargarListas()
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                _empaqueCampoDataTable = dbmImaging.SchemaConfig.TBL_Empaque_Campo.DBGet(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, Nothing)
                CamposDataGridView.AutoGenerateColumns = False
                CamposDataGridView.DataSource = _empaqueCampoDataTable

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargarParametros", ex)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

#End Region

    End Class

End Namespace