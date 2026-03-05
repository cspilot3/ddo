Imports Miharu.Desktop.Library.Config

Namespace Forms.Parametrizacion

    Public Class FormCampoLista

#Region " Eventos "

        Private Sub FormCampoLista_Load(sender As System.Object, e As EventArgs) Handles MyBase.Load
            CargarEntidades()
        End Sub
        Private Sub NuevoCampoListaButton_Click(sender As System.Object, e As EventArgs) Handles NuevoCampoListaButton.Click
            Dim fNuevoCampoLista As New FormNuevoCampoLista()
            fNuevoCampoLista.IdEntidad = CShort(EntidadComboBox.SelectedValue)
            fNuevoCampoLista.IdCampoLista = 0
            If fNuevoCampoLista.ShowDialog() = Windows.Forms.DialogResult.OK Then
                BuscarCamposLista()
            End If

        End Sub
        Private Sub CampoListaDataGridView_DoubleClick(sender As System.Object, e As EventArgs) Handles CampoListaDataGridView.DoubleClick
            Dim fEditarCampoLista As New FormNuevoCampoLista()
            If CampoListaDataGridView.SelectedRows.Count > 0 Then
                Dim SelectedRow = CampoListaDataGridView.SelectedRows(0)
                fEditarCampoLista.IdEntidad = CShort(EntidadComboBox.SelectedValue)
                fEditarCampoLista.IdCampoLista = CShort(SelectedRow.Cells("id_Campo_Lista").Value)
                fEditarCampoLista.NombreCampoLista = SelectedRow.Cells("Nombre_Campo_Lista").Value.ToString
            End If
            If fEditarCampoLista.ShowDialog() = Windows.Forms.DialogResult.OK Then
                BuscarCamposLista()
            End If
        End Sub
        Private Sub EliminarCampoListaButton_Click(sender As System.Object, e As EventArgs) Handles EliminarCampoListaButton.Click

            If MessageBox.Show("Esta Seguro?", "Borrar Campo Lista", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)

                Try
                    dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                    Dim Entidad = CShort(EntidadComboBox.SelectedValue)
                    Dim idCampoLista = CShort(CampoListaDataGridView.SelectedRows(0).Cells("id_Campo_LIsta").Value)
                    dbmCore.SchemaConfig.TBL_Campo_Lista.DBDelete(Entidad, idCampoLista)

                Catch ex As Exception
                    Throw New Exception(ex.Message)
                Finally
                    dbmCore.Connection_Close()
                End Try
                BuscarCamposLista()
            End If
        End Sub
        Private Sub EntidadComboBox_SelectedIndexChanged(sender As System.Object, e As EventArgs) Handles EntidadComboBox.SelectedIndexChanged
            BuscarCamposLista()
        End Sub

#End Region

#Region " Metodos "

        Private Sub CargarEntidades()

            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing

            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                Dim DocumentoDataTable = dbmCore.SchemaSecurity.CTA_Entidad.DBGet()
                EntidadComboBox.Fill(DocumentoDataTable, DocumentoDataTable.id_EntidadColumn, DocumentoDataTable.Nombre_EntidadColumn)

            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                dbmCore.Connection_Close()
            End Try
        End Sub
        Private Sub BuscarCamposLista()
            Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)

            Try
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                Dim Entidad = CShort(EntidadComboBox.SelectedValue)
                Dim CampoListaDataTable As DataTable = dbmCore.SchemaConfig.TBL_Campo_Lista.DBFindByfk_Entidad(Entidad)

                CampoListaDataGridView.DataSource = CampoListaDataTable

            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                dbmCore.Connection_Close()
            End Try
        End Sub

#End Region

    End Class
End Namespace