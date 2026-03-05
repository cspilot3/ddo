Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Controls
Imports Miharu.Desktop.Library.Config

Namespace Procesos.Configuracion.Imaging

    Public Class FormEmpaqueContenedorCampo
        Inherits Desktop.Library.FormBase

#Region " Declaraciones "

        Private _contenedorCampoDataTable As DBImaging.SchemaConfig.TBL_Empaque_Contenedor_CampoDataTable

#End Region

#Region " Eventos "

        Private Sub FormEmpaqueContenedorCampoLoad(sender As System.Object, e As EventArgs) Handles MyBase.Load
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)

                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
                _contenedorCampoDataTable = dbmImaging.SchemaConfig.TBL_Empaque_Contenedor_Campo.DBGet(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, Nothing)

                CamposDataGridView.AutoGenerateColumns = False
                CamposDataGridView.DataSource = _contenedorCampoDataTable

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargarParametros", ex)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

        Private Sub GuardarButtonClick(sender As System.Object, e As EventArgs) Handles GuardarButton.Click
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)

                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
                _contenedorCampoDataTable = dbmImaging.SchemaConfig.TBL_Empaque_Contenedor_Campo.DBFindByfk_Entidadfk_ProyectoNombre_CampoEliminado(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, Nothing, False)

                If _contenedorCampoDataTable.Count <= 0 Then
                    Dim agregarCampos = New FormAgregarEmpaqueContenedorCampo
                    agregarCampos.ShowDialog()
                    FormEmpaqueContenedorCampoLoad(sender, e)
                Else
                    DesktopMessageBoxControl.DesktopMessageShow("No se permite mas de un campo empaque", "Campos Empaque", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargarParametros", ex)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
            
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

                Dim agregarCampos = New FormAgregarEmpaqueContenedorCampo
                With agregarCampos
                    .id_CampoTextBox.Text = Row.Value("id_Campo")
                    .Nombre_Campo_TextBox.Text = Row.Value("Nombre_Campo")
                    .EliminadoCheckBox.Checked = Row.Value(Of Boolean)("Eliminado")
                End With

                agregarCampos.ShowDialog()
                FormEmpaqueContenedorCampoLoad(sender, e)
            Catch ex As Exception

            End Try

        End Sub

#End Region

#Region " Metodos "

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