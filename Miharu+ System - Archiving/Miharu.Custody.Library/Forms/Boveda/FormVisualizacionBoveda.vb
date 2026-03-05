Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Controls
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Config

Namespace Forms.Boveda

    Public Class FormVisualizacionBoveda
        Inherits FormBase

#Region " Eventos "

        Private Sub FormVisualizacionBoveda_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            CargaDatos()
        End Sub

        Private Sub BovedaComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles BovedaComboBox.SelectedIndexChanged
            CargaSeccion()
        End Sub

        Private Sub SeccionComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles SeccionComboBox.SelectedIndexChanged
            CargaEstante()
        End Sub

        Private Sub CerrarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CerrarButton.Click
            Me.Close()
        End Sub

        Private Sub EstantesDataGridView_CellClick(ByVal sender As System.Object, ByVal e As DataGridViewCellEventArgs) Handles EstantesDataGridView.CellClick
            If e.ColumnIndex = 0 Then
                Dim objEstante As New FormVisualizacionEstante2(Program.DesktopGlobal.BovedaRow.fk_Entidad, Program.DesktopGlobal.BovedaRow.fk_Sede, CShort(BovedaComboBox.SelectedValue), CShort(SeccionComboBox.SelectedValue), CShort(EstantesDataGridView.Rows(e.RowIndex).Cells(1).Value))
                objEstante.ShowDialog()
            End If
        End Sub

#End Region

#Region " Metodos "

        Private Sub CargaDatos()
            Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
            Try
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                'Dim Boveda = dbmCore.SchemaCustody.TBL_Boveda.DBFindByfk_Entidadfk_Sede(Program.DesktopGlobal.BovedaRow.fk_Entidad, Program.DesktopGlobal.BovedaRow.fk_Sede)
                Dim Boveda = dbmCore.SchemaCustody.TBL_Boveda.DBGet(Nothing, Nothing, Nothing)

                If Boveda.Count > 0 Then
                    Dim BovedaSeccion = dbmCore.SchemaCustody.TBL_Boveda_Seccion.DBGet(Nothing, Nothing, Nothing, Nothing)
                    'Dim BovedaSeccion = dbmCore.SchemaCustody.TBL_Boveda_Seccion.DBFindByfk_Entidadfk_Sedefk_Boveda(Program.DesktopGlobal.BovedaRow.fk_Entidad, Program.DesktopGlobal.BovedaRow.fk_Sede, Nothing)
                    Dim BovedaEstante = dbmCore.SchemaCustody.TBL_Boveda_Estante.DBGet(Nothing, Nothing, Nothing, Nothing, Nothing)
                    'Dim BovedaEstante = dbmCore.SchemaCustody.TBL_Boveda_Estante.DBFindByfk_Entidadfk_Sedefk_Bovedafk_Boveda_Seccion(Program.DesktopGlobal.BovedaRow.fk_Entidad, Program.DesktopGlobal.BovedaRow.fk_Sede, Nothing, Nothing)

                    BovedaDataSet.Tables.Add(Boveda)        '0: Bóveda
                    BovedaDataSet.Tables.Add(BovedaSeccion) '1: Sección
                    BovedaDataSet.Tables.Add(BovedaEstante) '2: Estante

                    Utilities.LlenarCombo(BovedaComboBox, BovedaDataSet.Tables(0), "id_Boveda", "Nombre_Boveda", True, "-1", "Todos...")
                Else
                    DesktopMessageBoxControl.DesktopMessageShow("No hay bovedas, favor crear las bovedas", "Cargar Bovedas", DesktopMessageBoxControl.IconEnum.ErrorIcon, True, False)
                End If

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaDatos", ex)
            Finally
                dbmCore.Connection_Close()
            End Try
        End Sub

        Private Sub CargaSeccion()
            Try
                Dim viewSeccion = BovedaDataSet.Tables(1).DefaultView
                viewSeccion.RowFilter = "fk_Boveda=" & BovedaComboBox.SelectedValue.ToString()
                Utilities.LlenarCombo(SeccionComboBox, viewSeccion.ToTable(), "id_Boveda_Seccion", "Nombre_Boveda_Seccion", True, "-1", "Todos...")
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaSeccion", ex)
            End Try
        End Sub

        Private Sub CargaEstante()
            Try
                Dim viewEstante = BovedaDataSet.Tables(2).DefaultView
                viewEstante.RowFilter = "fk_Boveda=" & BovedaComboBox.SelectedValue.ToString() & " AND fk_Boveda_Seccion=" & SeccionComboBox.SelectedValue.ToString()

                EstantesDataGridView.AutoGenerateColumns = False
                EstantesDataGridView.DataSource = viewEstante.ToTable()
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaEstante", ex)
            End Try
        End Sub

#End Region

    End Class

End Namespace