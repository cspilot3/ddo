Imports System.Text
Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Config
Imports Miharu.Risk.Library.Forms.LineaProceso
Imports Slyg.Tools

Namespace Forms.Empaque

    Public Class FormSeleccionarCajaCancelados
        Inherits FormBase

#Region " Declaraciones "
        Public Es_Empaque_Nuevo As Boolean = False
        Private cajas As DBCore.SchemaCustody.TBL_CajaDataTable
#End Region

#Region " Metodos "

        Private Sub CrearNuevaCaja()
            Dim Caja As New FormCrearCaja()
            Caja.ShowDialog()

            LlenarCajas()
        End Sub

        Private Sub LlenarCajas()
            Dim dmcore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
            dmcore.Connection_Open(Program.Sesion.Usuario.id)

            cajas = dmcore.SchemaCustody.TBL_Caja.DBFindByfk_Entidadfk_SedeEs_Procesofk_Entidad_Clientefk_Proyecto_Clientefk_Estado(Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede, False, Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, DBCore.EstadoEnum.Empaque)
            CajaListBox.DataSource = cajas
            CajaListBox.ValueMember = cajas.id_CajaColumn.ColumnName
            CajaListBox.DisplayMember = cajas.Codigo_CajaColumn.ColumnName

            dmcore.Connection_Close()
        End Sub

        Private Sub SeleccionarCaja()
            If CajaListBox.SelectedIndex < 0 Then
                DesktopMessageBoxControl.DesktopMessageShow("Debe seleccionar una caja para continuar con el proceso", "Error seleccionando caja", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
            Else
                Program.RiskGlobal.ID_CajaCustodia = CInt(CajaListBox.SelectedValue)
                Program.RiskGlobal.CBarras_CajaCustodia = CajaListBox.Text

                If Es_Empaque_Nuevo Then
                    Dim EmpaqueCaja As New FormEmpaqueCaja()
                    EmpaqueCaja.CBarras_Caja = CajaListBox.Text
                    EmpaqueCaja.ShowDialog()
                Else
                    Dim Empaque As New FormEmpaqueCancelados()
                    Empaque.ShowDialog()
                End If

                LlenarCajas()
            End If
        End Sub

        Private Sub FiltrarCaja()
            Dim Filtro As New DataView(cajas)

            If CajaBuscarDesktopTextBox.Text <> "" Then
                Filtro.RowFilter = cajas.Codigo_CajaColumn.ColumnName & "='" & CajaBuscarDesktopTextBox.Text & "'"
            End If

            CajaListBox.DataSource = Filtro.ToTable()
            CajaListBox.ValueMember = cajas.id_CajaColumn.ColumnName
            CajaListBox.DisplayMember = cajas.Codigo_CajaColumn.ColumnName
        End Sub

        Private Sub CerrarLineaProceso()
            Dim dbmArchiving As New DBArchiving.DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            Const Estado As DBCore.EstadoEnum = DBCore.EstadoEnum.Empaque

            Try
                dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

                'Si la línea no tiene asociado ningún file se elimina, de lo contrario la procesa.
                Dim dtFilesLineaProceso = dbmArchiving.Schemadbo.CTA_Folder_File.DBFindByfk_Registro_Tipofk_Linea_Proceso(DesktopConfig.RegistroTipo.Nuevo, CInt(Program.Sesion.Parameter("_idLineaProceso")))
                dbmArchiving.Schemadbo.CTA_Folder_File.DBFillByfk_Registro_Tipofk_Linea_Proceso(dtFilesLineaProceso, DesktopConfig.RegistroTipo.Adicion, CInt(Program.Sesion.Parameter("_idLineaProceso")))
                dbmArchiving.Schemadbo.CTA_Folder_File.DBFillByfk_Registro_Tipofk_Linea_Proceso(dtFilesLineaProceso, DesktopConfig.RegistroTipo.Devolucion, CInt(Program.Sesion.Parameter("_idLineaProceso")))

                If dtFilesLineaProceso.Count = 0 Then
                    Try : dbmArchiving.SchemaRisk.TBL_Linea_Proceso.DBDelete(CInt(Program.Sesion.Parameter("_idLineaProceso")))
                    Catch : End Try
                    Program.Sesion.Parameter("_idLineaProceso") = Nothing
                Else
                    If DesktopMessageBoxControl.DesktopMessageShow("¿Desea cerrar la línea de proceso: [" & Program.Sesion.Parameter("_idLineaProceso").ToString() & "]?", "Cerrar línea de proceso", DesktopMessageBoxControl.IconEnum.WarningIcon) = DialogResult.OK Then

                        'Para realizar el cierre no debe existir ningún item en el mismo estado (Empaque)
                        Dim viewFilesLineaProceso As DataView = dtFilesLineaProceso.DefaultView
                        viewFilesLineaProceso.RowFilter = "fk_estado=" & CStr(Estado) & " OR fk_Estado_File=" & CStr(Estado) & " OR fk_Estado=" & CStr(DBCore.EstadoEnum.Enviado_a_custodia) & " OR fk_Estado_File=" & CStr(DBCore.EstadoEnum.Enviado_a_custodia)

                        If viewFilesLineaProceso.Count > 0 Then
                            Dim strMensaje As New StringBuilder
                            For Each row As DataRow In viewFilesLineaProceso.ToTable(True, "CBarras_Folder", "CBarras_File", "fk_estado", "fk_Estado_File").Rows
                                Dim EstadoFolder = CType(row("fk_Estado").ToString(), DBCore.EstadoEnum)
                                Dim EstadoFile = CType(row("fk_Estado_File").ToString(), DBCore.EstadoEnum)
                                strMensaje.AppendLine(row("CBarras_Folder").ToString() & "  [" & EstadoFolder.ToString() & "] - " & row("CBarras_File").ToString() & "  [" & EstadoFile.ToString() & "]")
                            Next

                            DesktopMessageBoxControl.DesktopMessageShow("No se puede cerrar la línea de proceso, ya que aún existen items pendientes de empacar o cajas sin cerrar:" & vbNewLine & vbNewLine & strMensaje.ToString(), "Cierre de línea", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                        Else
                            CambiarLineaProceso(DBCore.EstadoEnum.Empacado)
                        End If
                    End If
                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CerrarLineaProceso", ex)
            Finally
                dbmArchiving.Connection_Close()
            End Try
        End Sub

        Private Sub CambiarLineaProceso(ByVal Estado As DBCore.EstadoEnum)
            Dim dbmArchiving As New DBArchiving.DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            Try
                dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

                Dim typeLineaProceso As New DBArchiving.SchemaRisk.TBL_Linea_ProcesoType
                typeLineaProceso.fk_Estado = Estado
                typeLineaProceso.Fecha_Log = SlygNullable.SysDate
                typeLineaProceso.Activo = False

                dbmArchiving.SchemaRisk.TBL_Linea_Proceso.DBUpdate(typeLineaProceso, CInt(Program.Sesion.Parameter("_idLineaProceso")))
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CerrarLineaProceso", ex)
            Finally
                dbmArchiving.Connection_Close()
            End Try
        End Sub
#End Region

#Region " Eventos "

        Private Sub CajaListBox_DoubleClick(ByVal sender As System.Object, ByVal e As EventArgs) Handles CajaListBox.DoubleClick
            SeleccionarCaja()
        End Sub

        Private Sub CancelarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CancelarButton.Click
            Me.Close()
        End Sub

        Private Sub NuevaCaja_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles NuevaCajaButton.Click
            CrearNuevaCaja()
        End Sub

        Private Sub FormSeleccionarCaja_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            Try
                Dim formLineaProceso As New FormSeleccionarLineaProceso()
                Dim Respuesta As DialogResult = formLineaProceso.ShowDialog()

                If Respuesta = DialogResult.OK Then
                    LineaProcesoLabel.Text = Program.Sesion.Parameter("_idLineaProceso").ToString()

                    LlenarCajas()
                ElseIf Respuesta = DialogResult.No Then
                    'No se puede trabajar sin línea de proceso.
                    Me.Close()
                End If

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Cargar Caja", ex)
            End Try
        End Sub

        Private Sub AceptarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AceptarButton.Click
            SeleccionarCaja()
        End Sub

        Private Sub DesktopTextBox_LostFocus(ByVal sender As Object, ByVal e As EventArgs) Handles CajaBuscarDesktopTextBox.LostFocus
            If CajaBuscarDesktopTextBox.Text = "" Then
                NuevaCajaButton.Focus()
            Else
                BuscarButton.Focus()
            End If
        End Sub

        Private Sub BuscarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BuscarButton.Click
            FiltrarCaja()
        End Sub

        Private Sub FormSeleccionarCaja_FormClosing(ByVal sender As System.Object, ByVal e As FormClosingEventArgs) Handles MyBase.FormClosing
            'If Not Es_Empaque_Nuevo Then 
            CerrarLineaProceso()
        End Sub
#End Region

    End Class

End Namespace