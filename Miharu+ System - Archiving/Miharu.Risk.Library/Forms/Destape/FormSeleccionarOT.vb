Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports DBArchiving
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Config

Namespace Forms.Destape

    Public Class FormSeleccionarOT
        Inherits FormBase

#Region " Declaraciones "
        Dim TableOT As DataTable
#End Region

#Region " Funciones "

        Private Sub CerrarOT()
            If OTDataGridView.SelectedRows().Count = 0 Then
                DesktopMessageBoxControl.DesktopMessageShow("Debe seleccionar una OT para continuar", "Seleccion OT", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
            Else
                If DesktopMessageBoxControl.DesktopMessageShow("Esta seguro de cerrar la OT " & CStr(OTDataGridView.SelectedRows(0).Cells(SchemaRisk.TBL_OTEnum.id_OT.ColumnName).Value) & "?", "Cerrar OT", DesktopMessageBoxControl.IconEnum.AdvertencyIcon) = DialogResult.OK Then
                    Dim dbmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)

                    Try
                        dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)
                        dbmArchiving.Transaction_Begin()
                        dbmArchiving.Schemadbo.PA_Cerrar_OT.DBExecute(CInt(OTDataGridView.SelectedRows(0).Cells(SchemaRisk.TBL_OTEnum.id_OT.ColumnName).Value), Program.Sesion.Usuario.id, DesktopConfig.Modulo.Archiving, DBCore.EstadoEnum.Mesa_de_Control)
                        dbmArchiving.Transaction_Commit()
                    Catch ex As Exception
                        dbmArchiving.Transaction_Rollback()
                    Finally
                        dbmArchiving.Connection_Close()
                    End Try
                End If

                LlenarOTS()
            End If
        End Sub

        Public Sub LlenarOTS()
            Dim dbmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)

            Try
                dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)
                dbmArchiving.Transaction_Begin()
                dbmArchiving.Schemadbo.PA_Actualizar_OTs.DBExecute()
                dbmArchiving.Transaction_Commit()
            Catch ex As Exception
                dbmArchiving.Transaction_Rollback()
            Finally
                dbmArchiving.Connection_Close()
            End Try

            dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)
            If Program.RiskGlobal.ProyectoRow.usa_cargue_parcial Then
                TableOT = dbmArchiving.SchemaRisk.TBL_OT.DBFindByfk_Estadofk_Entidadfk_Proyectofk_Sede_Procesamiento(DBCore.EstadoEnum.Cargado, _
                                                                                                                     Program.RiskGlobal.Entidad, _
                                                                                                                     Program.RiskGlobal.Proyecto, _
                                                                                                                     Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede)
            Else
                TableOT = dbmArchiving.SchemaRisk.TBL_OT.DBFindByfk_Estadofk_Entidadfk_Proyectofk_Sede_Procesamiento(DBCore.EstadoEnum.Creado, _
                                                                                                                     Program.RiskGlobal.Entidad, _
                                                                                                                     Program.RiskGlobal.Proyecto, _
                                                                                                                     Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede)
            End If

            dbmArchiving.Connection_Close()

            OTDataGridView.AutoGenerateColumns = False
            OTDataGridView.DataSource = TableOT
            For Each OTdataRow As DataGridViewRow In OTDataGridView.Rows
                Dim DiasDiferencia = Now - CDate(OTdataRow.Cells(SchemaRisk.TBL_OTEnum.Fecha_OT.ColumnName).Value)
                If (DiasDiferencia.Days > Program.RiskGlobal.ProyectoRow.Dias_Vencimiento And Program.RiskGlobal.ProyectoRow.Dias_Vencimiento <> 0) Then
                    OTdataRow.DefaultCellStyle.ForeColor = Drawing.Color.Red
                    OTdataRow.DefaultCellStyle.SelectionForeColor = Drawing.Color.Red
                Else
                    OTdataRow.DefaultCellStyle.ForeColor = Drawing.Color.Black
                    OTdataRow.DefaultCellStyle.SelectionForeColor = Drawing.Color.Black
                End If
            Next
            OTDataGridView.ClearSelection()
        End Sub

        Public Sub LlenarOTSFiltro()
            Dim view As DataView = Utilities.ClonarDataTable(TableOT).DefaultView
            Dim filtro As String = "id_Ot LIKE '%" & OtBuscarDesktopTextBox.Text & "%'"
            view.RowFilter = filtro

            OTDataGridView.DataSource = view.ToTable

            'OtListBox.DataSource = view.ToTable
            'OtListBox.ValueMember = "id_OT"
            'OtListBox.DisplayMember = "id_OT"
        End Sub

        Public Sub SeleccionarOt()
            If OTDataGridView.SelectedRows().Count = 0 Then
                DesktopMessageBoxControl.DesktopMessageShow("Debe seleccionar una OT para continuar", "Seleccion OT", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
            Else
                Program.RiskGlobal.OT = CInt(OTDataGridView.SelectedRows(0).Cells(SchemaRisk.TBL_OTEnum.id_OT.ColumnName).Value)

                Me.DialogResult = DialogResult.OK
                Me.Close()
            End If
        End Sub

#End Region

#Region " Eventos "

        Private Sub CerrarOTButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CerrarOTButton.Click
            CerrarOT()
        End Sub
        
        Private Sub OTDataGridView_CellDoubleClick(sender As System.Object, e As DataGridViewCellEventArgs) Handles OTDataGridView.CellDoubleClick
            SeleccionarOt()
        End Sub

        Private Sub FormSeleccionarOT_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            LlenarOTS()
        End Sub

        Private Sub BuscarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BuscarButton.Click
            LlenarOTSFiltro()
        End Sub

        Private Sub AceptarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AceptarButton.Click
            SeleccionarOt()
        End Sub

        Private Sub CancelarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CancelarButton.Click
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        End Sub

        'Private Sub OtListBox_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
        '    If e.KeyCode = Keys.Enter Then
        '        e.Handled = True
        '        SendKeys.Send("{TAB}")
        '    End If
        'End Sub

        Private Sub OtBuscarDesktopTextBox_LostFocus(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles OtBuscarDesktopTextBox.KeyPress
            If e.KeyChar = ChrW(Keys.Enter) Or e.KeyChar = ChrW(Keys.Tab) Then
                If OtBuscarDesktopTextBox.Text = "" Then
                    OTDataGridView.Focus()
                Else
                    BuscarButton.Focus()
                End If
            End If
        End Sub

#End Region

    End Class

End Namespace