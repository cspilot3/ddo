Imports System.Text
Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Controls
Imports DBArchiving
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Config
Imports Slyg.Tools

Namespace Forms.Destape

    Public Class FormSeleccionarPrecinto
        Inherits FormBase

#Region " Declaraciones "

        Dim TablePrecintos As DataTable

#End Region

#Region " Funciones "

        Public Sub CargaPrecintos()
            Dim dbmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)
            TablePrecintos = dbmArchiving.Schemadbo.PA_Precinto_GET.DBExecute(Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, CInt(Program.Sesion.Parameter("_idLineaProceso")), Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede)
            dbmArchiving.Connection_Close()

            PrecintosDataGridView.AutoGenerateColumns = False
            PrecintosDataGridView.DataSource = TablePrecintos
        End Sub

        Public Sub FiltraPrecintos()
            Dim view As DataView = Utilities.clonarDataTable(TablePrecintos).DefaultView
            view.RowFilter = "id_precinto like '%" & PrecintoBuscarDesktopTextBox.Text & "%'"

            PrecintosDataGridView.AutoGenerateColumns = False
            PrecintosDataGridView.DataSource = view.ToTable
        End Sub

        Public Sub SeleccionarPrecinto()
            If PrecintosDataGridView.SelectedRows.Count > 0 Then
                Program.RiskGlobal.Precinto = CStr(PrecintosDataGridView.SelectedRows(0).Cells("precinto").Value)

                Dim SeleccionarPrecintoCarpeta As New FormPrecintosCarpetas()
                SeleccionarPrecintoCarpeta.ShowDialog()
                CargaPrecintos()
            Else
                DesktopMessageBoxControl.DesktopMessageShow("Debe seleccionar un precinto para continuar.", "Seleccionar precinto", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
            End If
        End Sub

#End Region

#Region " Eventos "

        Private Sub FormSeleccionarPrecinto_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            Dim dbmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            Dim _idLineaProceso As Integer

            Try
                'Se obtiene o crea la línea de proceso.
                dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)
                AsignaLineaProceso(dbmArchiving, _idLineaProceso, Program.DesktopGlobal.CentroProcesamientoRow, Program.RiskGlobal, DBCore.EstadoEnum.Destapado, Program.Sesion.Usuario.id)
                Program.Sesion.Parameter("_idLineaProceso") = _idLineaProceso
                LineaProcesoLabel.Text = _idLineaProceso.ToString()

                CargaPrecintos()
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("FormPrecintosCarpetas_Load", ex)
            Finally
                dbmArchiving.Connection_Close()
            End Try
        End Sub

        Private Sub PrecintosDataGridView_DoubleClick(ByVal sender As System.Object, ByVal e As EventArgs) Handles PrecintosDataGridView.DoubleClick
            SeleccionarPrecinto()
        End Sub

        Private Sub BuscarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BuscarButton.Click
            FiltraPrecintos()
        End Sub

        Private Sub CerrarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CerrarButton.Click
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        End Sub

        Private Sub DestaparButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles DestaparButton.Click
            SeleccionarPrecinto()
        End Sub

        Private Sub OtBuscarDesktopTextBox_LostFocus(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles PrecintoBuscarDesktopTextBox.KeyPress
            If e.KeyChar = ChrW(Keys.Enter) Or e.KeyChar = ChrW(Keys.Tab) Then
                If PrecintoBuscarDesktopTextBox.Text = "" Then
                    PrecintosDataGridView.Focus()
                Else
                    BuscarButton.Focus()
                End If
            End If
        End Sub

        Private Sub PrecintosDataGridView_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles PrecintosDataGridView.KeyDown
            If e.KeyCode = Keys.Enter Then
                e.Handled = True
                SendKeys.Send("{TAB}")
            End If
        End Sub

        Private Sub FormSeleccionarPrecinto_FormClosing(ByVal sender As System.Object, ByVal e As FormClosingEventArgs) Handles MyBase.FormClosing
            Try
                If DesktopMessageBoxControl.DesktopMessageShow("¿Desea cerrar la línea de proceso: [" & Program.Sesion.Parameter("_idLineaProceso").ToString() & "]?", "Cerrar línea de proceso", DesktopMessageBoxControl.IconEnum.WarningIcon, False) = DialogResult.OK Then
                    CerrarLineaProceso()
                End If

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("FormSeleccionarPrecinto_FormClosing", ex)
            End Try
        End Sub
#End Region

#Region " Metodos "

        Private Sub AsignaLineaProceso(ByRef dbmArchiving As DBArchivingDataBaseManager, ByRef idLineaProceso As Integer, ByVal CentroProcesamientoRow As DBCore.SchemaSecurity.CTA_Centro_ProcesamientoSimpleType, ByVal riskGlobal As RiskGlobal, ByVal estado As DBCore.EstadoEnum, ByVal idUsuario As Integer)
            Try
                'Se consulta si existe una línea de proceso para el usuario.
                'Dim dtLineaProceso = dbmArchiving.SchemaRisk.TBL_Linea_Proceso.DBFindByfk_Entidad_Clientefk_Proyectofk_EstadoActivofk_Usuario(riskGlobal.Entidad, riskGlobal.Proyecto, estado, True, idUsuario)
                Dim dtLineaProceso = dbmArchiving.SchemaRisk.TBL_Linea_Proceso.DBFindByfk_Sede_Procesofk_Centro_Procesamientofk_Entidad_Clientefk_Proyectofk_EstadoActivofk_Usuario(CentroProcesamientoRow.fk_Sede, CentroProcesamientoRow.id_Centro_Procesamiento, riskGlobal.Entidad, riskGlobal.Proyecto, estado, True, idUsuario)

                If dtLineaProceso.Count > 0 Then
                    idLineaProceso = dtLineaProceso(0).id_Linea_Proceso
                Else
                    dbmArchiving.Transaction_Begin()

                    Dim typeLineaProceso As New SchemaRisk.TBL_Linea_ProcesoType
                    typeLineaProceso.id_Linea_Proceso = dbmArchiving.SchemaRisk.TBL_Linea_Proceso.DBNextId()
                    typeLineaProceso.fk_Entidad_Proceso = CentroProcesamientoRow.fk_Entidad
                    typeLineaProceso.fk_Sede_Proceso = CentroProcesamientoRow.fk_Sede
                    typeLineaProceso.fk_Centro_Procesamiento = CentroProcesamientoRow.id_Centro_Procesamiento
                    typeLineaProceso.fk_Entidad_Cliente = riskGlobal.Entidad
                    typeLineaProceso.fk_Proyecto = riskGlobal.Proyecto
                    typeLineaProceso.fk_Estado = estado
                    typeLineaProceso.Fecha_Creacion = SlygNullable.SysDate
                    typeLineaProceso.Activo = True
                    typeLineaProceso.fk_Usuario = idUsuario
                    typeLineaProceso.Fecha_Log = SlygNullable.SysDate
                    dbmArchiving.SchemaRisk.TBL_Linea_Proceso.DBInsert(typeLineaProceso)

                    idLineaProceso = typeLineaProceso.id_Linea_Proceso

                    dbmArchiving.Transaction_Commit()
                End If
            Catch ex As Exception
                dbmArchiving.Transaction_Rollback()
                DesktopMessageBoxControl.DesktopMessageShow("AsignaLineaProceso", ex)
            End Try
        End Sub

        Private Sub CerrarLineaProceso()
            Dim dbmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            Try
                dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

                Dim dtFilesDestape = dbmArchiving.Schemadbo.CTA_Folder_File.DBFindByfk_Linea_Proceso(CInt(Program.Sesion.Parameter("_idLineaProceso"))).DefaultView
                dtFilesDestape.RowFilter = "fk_Estado=" & CStr(DBCore.EstadoEnum.Destapado) & " OR fk_Estado_File=" & CStr(DBCore.EstadoEnum.Destapado)

                If dtFilesDestape.Count > 0 Then
                    'Crea mensaje con la relación de documentos pendientes.
                    Dim mensaje As New StringBuilder()
                    mensaje.AppendLine("CARPETA - DOCUMENTO")
                    For Each row As DataRow In dtFilesDestape.ToTable(True).Rows
                        mensaje.AppendLine(row("CBarras_Folder").ToString() & " [" & CType(row("fk_Estado"), DBCore.EstadoEnum).ToString() & "] - " & row("CBarras_File").ToString() & " [" & CType(row("fk_Estado_File"), DBCore.EstadoEnum).ToString() & "]")
                    Next

                    DesktopMessageBoxControl.DesktopMessageShow("No se puede cerrar la línea, ya que contiene items sin procesar." & vbNewLine & vbNewLine & mensaje.ToString(), "Cierre Línea Proceso", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                Else
                    Dim typeLineaProceso As New SchemaRisk.TBL_Linea_ProcesoType
                    typeLineaProceso.fk_Estado = DBCore.EstadoEnum.Mesa_de_Control
                    typeLineaProceso.Fecha_Log = SlygNullable.SysDate

                    dbmArchiving.SchemaRisk.TBL_Linea_Proceso.DBUpdate(typeLineaProceso, CInt(Program.Sesion.Parameter("_idLineaProceso")))
                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CerrarLineaProceso", ex)
            Finally
                dbmArchiving.Connection_Close()
            End Try
        End Sub
#End Region

    End Class

End Namespace