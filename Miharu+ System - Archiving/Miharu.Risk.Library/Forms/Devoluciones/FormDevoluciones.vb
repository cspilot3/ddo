Imports System.Drawing
Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopDataGridView
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports DBArchiving
Imports DBArchiving.Schemadbo
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Config
Imports Miharu.Risk.Library.Forms.Reportes.Devoluciones
Imports Slyg.Tools

Namespace Forms.Devoluciones

    Public Class FormDevoluciones
        Inherits FormBase

#Region " Eventos "

        Private Sub FormDevoluciones_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            CargaControles()
        End Sub

        Private Sub BuscarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BuscarButton.Click
            If OTTextBox.Text = "" Then
                DesktopMessageBoxControl.DesktopMessageShow("Debe Ingresar OT para realizar la búsqueda", "Búsqueda de Registros", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                OTTextBox.Focus()
                Exit Sub
            End If

            If Not IsNumeric(PrecintoComboBox.SelectedValue.ToString) Then
                DesktopMessageBoxControl.DesktopMessageShow("Debe Seleccionar un Precinto Valido para realizar la búsqueda", "Búsqueda de Registros", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                PrecintoComboBox.Focus()
                Exit Sub
            End If


            Select Case CInt(PrecintoComboBox.SelectedValue)
                Case 0, -1
                    DesktopMessageBoxControl.DesktopMessageShow("Debe Seleccionar un Precinto para realizar la búsqueda", "Búsqueda de Registros", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                    PrecintoComboBox.Focus()
                    Exit Sub

            End Select


            BuscarReprocesos()
            CreaCheckAll()
        End Sub

        Private Sub CerrarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CerrarButton.Click
            Me.Close()
        End Sub

        Private Sub ReprocesosDataGridView_CellClick(ByVal sender As System.Object, ByVal e As DataGridViewCellEventArgs) Handles ReprocesosDataGridView.CellClick
            Try
                If e.RowIndex > -1 Then 'Si no es la fila de encabezados
                    Dim objCheck = CType(sender, DesktopDataGridViewControl).Rows(e.RowIndex).Cells(ReprocesosDataGridView.Columns("R").Index)

                    If CBool(objCheck.Value) Then
                        objCheck.Value = False
                    Else
                        objCheck.Value = True
                    End If
                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("ReprocesosDataGridView_CellClick", ex)
            End Try
        End Sub

        Private Sub ckBox_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
            Try
                Dim checked As Boolean = CType(sender, CheckBox).Checked
                ReprocesosDataGridView.ClearSelection()
                For i = 0 To Me.ReprocesosDataGridView.RowCount - 1
                    ReprocesosDataGridView.Rows(i).Cells(ReprocesosDataGridView.Columns("R").Index).Value = checked
                Next
                ReprocesosDataGridView.EndEdit()
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("ckBox_CheckedChanged", ex)
            End Try
        End Sub

        Private Sub InformeButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles InformeButton.Click
            If ValidaSeleccionItems() Then
                ImprimirInforme()
            Else
                DesktopMessageBoxControl.DesktopMessageShow("No se ha seleccionado ningún elemento.", "Selección de Items", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
            End If
        End Sub

        Private Sub GuiaButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles GuiaButton.Click
            If ValidaSeleccionItems() Then
                If DesktopMessageBoxControl.DesktopMessageShow("¿Está seguro que desea Devolverlos elementos seleccionados?", "Devolver items", DesktopMessageBoxControl.IconEnum.WarningIcon, False) = DialogResult.OK Then
                    ImprimirGuia()
                End If
            Else
                DesktopMessageBoxControl.DesktopMessageShow("No se ha seleccionado ningún elemento.", "Selección de Items", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
            End If
        End Sub

        Private Sub DevolucionButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles DevolucionButton.Click
            If ValidaSeleccionItems() Then
                If DesktopMessageBoxControl.DesktopMessageShow("¿Está seguro que desea trasladar los elementos seleccionados a Destape?", "Trasladar a Destape", DesktopMessageBoxControl.IconEnum.WarningIcon, False) = DialogResult.OK Then
                    CambiaEstadoElemento(DBCore.EstadoEnum.Cargado)
                End If
            Else
                DesktopMessageBoxControl.DesktopMessageShow("No se ha seleccionado ningún elemento.", "Selección de Items", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
            End If
        End Sub

        Private Sub MesaControlButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles MesaControlButton.Click
            If ValidaSeleccionItems() Then
                If DesktopMessageBoxControl.DesktopMessageShow("¿Está seguro que desea trasladar los elementos seleccionados a Mesa de Control?", "Trasladar a Mesa de Control", DesktopMessageBoxControl.IconEnum.WarningIcon, False) = DialogResult.OK Then
                    CambiaEstadoElemento(DBCore.EstadoEnum.Mesa_de_Control)
                End If
            Else
                DesktopMessageBoxControl.DesktopMessageShow("No se ha seleccionado ningún elemento.", "Selección de Items", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
            End If
        End Sub

        Private Sub EmpaqueButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles EmpaqueButton.Click
            If ValidaSeleccionItems() Then
                If DesktopMessageBoxControl.DesktopMessageShow("¿Está seguro que desea trasladar los elementos seleccionados a Empaque?", "Trasladar a Empaque", DesktopMessageBoxControl.IconEnum.WarningIcon, False) = DialogResult.OK Then
                    CambiaEstadoElemento(DBCore.EstadoEnum.Empaque)
                End If
            Else
                DesktopMessageBoxControl.DesktopMessageShow("No se ha seleccionado ningún elemento.", "Selección de Items", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
            End If
        End Sub

        Private Sub ReprocesosDataGridView_SizeChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles ReprocesosDataGridView.SizeChanged
            If Not IsNothing(ReprocesosDataGridView.Columns("R")) Then
                CreaCheckAll()
            End If
        End Sub

        Private Sub OTTextBox_Leave(sender As Object, e As System.EventArgs) Handles OTTextBox.Leave
            CargaPrecintos()
        End Sub

#End Region

#Region " Metodos "

        Private Sub CargaControles()
            Dim dbmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            Try
                dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)
                Dim dtMotivo = dbmArchiving.SchemaRisk.TBL_Reproceso_Motivo.DBGet(Nothing)
                Utilities.LlenarCombo(MotivoComboBox, dtMotivo, dtMotivo.Id_Reproceso_MotivoColumn.ColumnName, dtMotivo.Nombre_Reproceso_MotivoColumn.ColumnName, True, "-1", "Todos...")
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaControles", ex)
            Finally
                dbmArchiving.Connection_Close()
            End Try
        End Sub

        Private Sub CargaPrecintos()
            If OTTextBox.Text <> "" Then
                Dim dbmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
                Try
                    dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

                    Dim Dtprecinto = dbmArchiving.SchemaRisk.PA_GET_Precinto_Reproceso.DBExecute(CLng(OTTextBox.Text))

                    Utilities.LlenarCombo(PrecintoComboBox, Dtprecinto, "id_Precinto", "id_Precinto", True, "-1", "Todos...")

                Catch ex As Exception

                Finally

                    If Not dbmArchiving Is Nothing Then dbmArchiving.Connection_Close()
                End Try
            End If
            
        End Sub

        Private Sub CreaCheckAll()
            'Se elimina si ya existe
            If ReprocesosDataGridView.Controls.Count >= 3 Then
                Me.ReprocesosDataGridView.Controls.Remove(ReprocesosDataGridView.Controls(2))
            End If

            'Control para seleccionar todos los elementos de la grilla
            Dim ckBox As New CheckBox()
            Dim rect As Rectangle = Me.ReprocesosDataGridView.GetCellDisplayRectangle(ReprocesosDataGridView.Columns("R").Index, -1, True)
            ckBox.Size = New Size(18, 18)
            'Para ajustar la posición donde se pinta
            rect.X += 3
            ckBox.Location = rect.Location
            ckBox.BackColor = Color.Transparent
            AddHandler ckBox.CheckedChanged, AddressOf ckBox_CheckedChanged
            Me.ReprocesosDataGridView.Controls.Add(ckBox)
        End Sub

        Private Sub BuscarReprocesos()
            Dim dbmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            Try
                dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

                Dim numOT As Slyg.Tools.SlygNullable(Of Integer) = Nothing
                Dim numPrecinto As Slyg.Tools.SlygNullable(Of Integer) = Nothing
                Dim numMotivo As Slyg.Tools.SlygNullable(Of Integer) = Nothing

                If OTTextBox.Text <> "" Then numOT = CInt(OTTextBox.Text)
                If PrecintoComboBox.Text <> "" Then numPrecinto = CInt(PrecintoComboBox.Text)
                If CStr(MotivoComboBox.SelectedValue) <> "-1" Then numMotivo = CInt(MotivoComboBox.SelectedValue)

                Dim dtReprocesos = dbmArchiving.Schemadbo.PA_Obtiene_Items_Reproceso.DBExecute(Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, DBCore.EstadoEnum.Reproceso, numOT, numPrecinto, numMotivo)
                If dtReprocesos.Rows.Count > 0 Then
                    ReprocesosDataGridView.AutoGenerateColumns = False
                    ReprocesosDataGridView.DataSource = dtReprocesos
                Else
                    ReprocesosDataGridView.AutoGenerateColumns = False
                    ReprocesosDataGridView.DataSource = Nothing
                    DesktopMessageBoxControl.DesktopMessageShow("No se encontraron registros para esta búsqueda.", "Búsqueda de Registros", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                End If

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("BuscarReprocesos", ex)
            Finally
                dbmArchiving.Connection_Close()
            End Try
        End Sub

        Private Sub ImprimirGuia()
            Try
                Dim listRow As New List(Of String)
                Dim listFolder As New List(Of String)

                For Each row As DataGridViewRow In ReprocesosDataGridView.Rows
                    If CBool(row.Cells(ReprocesosDataGridView.Columns("R").Index).Value) Then
                        listRow.Add(row.Cells(ReprocesosDataGridView.Columns("CBarras").Index).Value.ToString())
                        If Not listFolder.Contains(row.Cells(ReprocesosDataGridView.Columns("CBarras_Folder").Index).Value.ToString()) Then
                            listFolder.Add(row.Cells(ReprocesosDataGridView.Columns("CBarras_Folder").Index).Value.ToString())
                        End If
                    End If
                Next
                Dim FormImprimirGuia As New FormImprimirDevolucion(listRow, PrecintoComboBox.Text)
                FormImprimirGuia.ShowDialog()

                Dim dbmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
                Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)

                For Each carpeta In listFolder
                    Try

                        dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)
                        dbmArchiving.Transaction_Begin()

                        dbmArchiving.Schemadbo.PA_Actualiza_Folder_Reproceso.DBExecute(carpeta, PrecintoComboBox.Text, Program.Sesion.Usuario.id)
                        dbmArchiving.Transaction_Commit()

                    Catch ex As Exception
                        dbmArchiving.Transaction_Rollback()
                    Finally
                        dbmArchiving.Connection_Close()
                    End Try
                Next

                BuscarReprocesos()
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("ImprimirGuia", ex)
            End Try
        End Sub

        Private Sub ImprimirInforme()
            Dim dbmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            Try
                dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

                Dim listRow As New List(Of String)
                For Each row As DataGridViewRow In ReprocesosDataGridView.Rows
                    If CBool(row.Cells(ReprocesosDataGridView.Columns("R").Index).Value) Then
                        listRow.Add(row.Cells(ReprocesosDataGridView.Columns("CBarras").Index).Value.ToString())
                    End If
                Next

                'Muestra el reporte
                Dim itemsReporte = New xsdDevoluciones

                'Los items de la devolución
                For Each item In listRow
                    Dim orderItems As New RPT_Items_DevolucionEnumList
                    orderItems.Add(RPT_Items_DevolucionEnum.Llave1, True)
                    orderItems.Add(RPT_Items_DevolucionEnum.Llave2, True)
                    orderItems.Add(RPT_Items_DevolucionEnum.Llave3, True)
                    Dim ItemsDevolucion = dbmArchiving.Schemadbo.RPT_Items_Devolucion.DBFindByCBarras_File(item, 0, orderItems)

                    For Each ItemDev In ItemsDevolucion
                        Dim fila = itemsReporte.Items.NewItemsRow()
                        fila.Elemento = ItemDev.Nombre_Documento
                        fila.CBarras = ItemDev.CBarras_File
                        fila.Llave1 = CStr(ItemDev.Llave1)
                        If (Not ItemDev.Llave2 Is DBNull.Value) Then fila.Llave2 = CStr(ItemDev.Llave2)
                        If (Not ItemDev.Llave3 Is DBNull.Value) Then fila.Llave3 = CStr(ItemDev.Llave3)
                        fila.Folios = CStr(ItemDev.Folios_File)
                        fila.NombreLLave1 = CStr(ItemDev.NombreLlave1)
                        If (Not ItemDev.NombreLlave2 Is DBNull.Value) Then fila.NombreLlave2 = CStr(ItemDev.NombreLlave2)
                        If (Not ItemDev.NombreLlave3 Is DBNull.Value) Then fila.NombreLlave3 = CStr(ItemDev.NombreLlave3)
                        fila.MotivoDevolucion = ItemDev.Nombre_Reproceso_Motivo

                        If Not ItemDev.IsPregunta_ValidacionNull Then
                            fila.Pregunta_Validacion = ItemDev.Pregunta_Validacion
                        Else
                            fila.Pregunta_Validacion = Nothing
                        End If

                        If Not ItemDev.IsRespuestaNull Then
                            fila.Respuesta = ItemDev.Respuesta
                        Else
                            fila.Respuesta = Nothing
                        End If

                        'Se filtran las respuestas negativas
                        If ItemDev.IsRespuestaNull Then
                            itemsReporte.Items.Rows.Add(fila)
                        ElseIf Not ItemDev.Respuesta Then
                            itemsReporte.Items.Rows.Add(fila)
                        End If
                    Next
                Next

                'Los datos del Reproceso

                Dim dtDataPunto = dbmArchiving.Schemadbo.CTA_Puntos_Proyecto.DBFindByid_Precinto(PrecintoComboBox.Text)

                Dim filaReproceso = itemsReporte.Reproceso.NewReprocesoRow()
                filaReproceso.Guia = ""
                filaReproceso.Sello = ""

                If Not dtDataPunto Is DBNull.Value Then
                    filaReproceso.UsuarioDestino = dtDataPunto(0).Responsable
                    filaReproceso.EntidadDestino = dtDataPunto(0).Nombre_Punto
                Else
                    filaReproceso.UsuarioDestino = Program.RiskGlobal.ProyectoRow.Responsable_Proyecto.ToUpper()
                    filaReproceso.EntidadDestino = Program.RiskGlobal.ProyectoRow.Nombre_Entidad_Responsable.ToUpper()
                End If

                'Dim UsuarioFirma As New DBArchiving.SchemaSecurity.CTA_UsuarioDataTable
                'If Not Program.DesktopGlobal.CentroProcesamientoRow.Isfk_Usuario_ResponsableNull Then
                Dim UsuarioFirma = dbmArchiving.SchemaSecurity.CTA_Usuario.DBFindByid_Usuario(Program.DesktopGlobal.CentroProcesamientoRow.fk_Usuario_Responsable)
                'End If

                If UsuarioFirma.Count > 0 Then
                    filaReproceso.UsuarioRemite = UsuarioFirma(0).Nombres.ToUpper()
                    If Not UsuarioFirma(0).Isfk_CargoNull Then filaReproceso.CargoRemite = UsuarioFirma(0).Nombre_Cargo.ToUpper()
                    filaReproceso.DependenciaRemite = UsuarioFirma(0).Nombre_Dependencia.ToUpper()
                End If

                Dim CiudadRemite = dbmArchiving.SchemaSecurity.CTA_Sede.DBFindByfk_Entidadid_Sede(Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede)
                If CiudadRemite.Count > 0 Then
                    filaReproceso.CiudadRemite = CiudadRemite(0).Nombre_Ciudad
                End If
                itemsReporte.Reproceso.Rows.Add(filaReproceso)


                'Se muestra el informe
                Dim sTituloReporte As String = Now.Year.ToString() & Now.Month.ToString() & Now.Day.ToString() & "-Informe"
                Dim FormImpresionDevolucion As New FormImpresionDevolucion(itemsReporte, "Miharu.Risk.Library.InformeItemsReproceso.rdlc", sTituloReporte, Nothing)
                FormImpresionDevolucion.ShowDialog()

                BuscarReprocesos()
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("ImprimirInforme", ex)
            Finally
                dbmArchiving.Connection_Close()
            End Try
        End Sub

        Private Sub CambiaEstadoElemento(ByVal estado As DBCore.EstadoEnum)
            Dim numNoValidos As Integer = 0
            Dim _idLineaProceso As Integer
            Dim _OTReproceso As Integer

            'If ValidarOT(estado, _OTReproceso) Then


            Dim dbmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)

            Try
                _OTReproceso = CInt(OTTextBox.Text)

                dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

                Select Case estado
                    Case DBCore.EstadoEnum.Cargado, DBCore.EstadoEnum.Mesa_de_Control

                        For Each row As DataGridViewRow In ReprocesosDataGridView.Rows

                            If CBool(row.Cells(ReprocesosDataGridView.Columns("R").Index).Value) Then
                                Dim CBarrasLocal As String = row.Cells(ReprocesosDataGridView.Columns("CBarras").Index).Value.ToString()
                                Dim dtFile = dbmArchiving.Schemadbo.CTA_File.DBFindByCBarras_File(CBarrasLocal)
                                'Dim dtFileFolder = dbmArchiving.Schemadbo.CTA_Folder_File.DBFindByCBarras_Filefk_OT_File(CBarrasLocal, dtFile(0).fk_OT)

                                If dtFile(0).fk_Reproceso_Motivo = DesktopConfig.MotivoReproceso.FilesSobrantes Or dtFile(0).fk_Reproceso_Motivo = DesktopConfig.MotivoReproceso.FaltantesObligatorios Then

                                    AsignaLineaProceso(dbmArchiving, _idLineaProceso, Program.DesktopGlobal.CentroProcesamientoRow, Program.RiskGlobal, estado, Program.Sesion.Usuario.id)
                                    Dim data = dbmArchiving.Schemadbo.PA_Reproceso_Reasignar.DBExecute(CBarrasLocal, _idLineaProceso, _OTReproceso, estado)
                                    If CInt(data.Rows(0)("Tipo")) <> 1 Then
                                        numNoValidos += 1
                                        DesktopMessageBoxControl.DesktopMessageShow(data.Rows(0)("MENSAJE").ToString(), "Error", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                                    End If

                                ElseIf dtFile(0).fk_Reproceso_Motivo = DesktopConfig.MotivoReproceso.ValidacionesObligatorias Or dtFile(0).fk_Reproceso_Motivo = DesktopConfig.MotivoReproceso.ValidacionesDestapeObligatorias Then

                                    AsignaLineaProceso(dbmArchiving, _idLineaProceso, Program.DesktopGlobal.CentroProcesamientoRow, Program.RiskGlobal, DBCore.EstadoEnum.Mesa_de_Control, Program.Sesion.Usuario.id)
                                    Dim data = dbmArchiving.Schemadbo.PA_Reproceso_Reasignar.DBExecute(CBarrasLocal, _idLineaProceso, _OTReproceso, DBCore.EstadoEnum.Mesa_de_Control)
                                    If CInt(data.Rows(0)("Tipo")) <> 1 Then
                                        numNoValidos += 1
                                        DesktopMessageBoxControl.DesktopMessageShow(data.Rows(0)("MENSAJE").ToString(), "Error", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                                    End If

                                Else
                                    numNoValidos += 1
                                End If

                            End If

                        Next

                    Case DBCore.EstadoEnum.Empaque
                        AsignaLineaProceso(dbmArchiving, _idLineaProceso, Program.DesktopGlobal.CentroProcesamientoRow, Program.RiskGlobal, estado, Program.Sesion.Usuario.id)

                        For Each row As DataGridViewRow In ReprocesosDataGridView.Rows
                            If CBool(row.Cells(ReprocesosDataGridView.Columns("R").Index).Value) Then
                                Dim CBarrasLocal As String = row.Cells(ReprocesosDataGridView.Columns("CBarras").Index).Value.ToString()
                                Dim dtFile = dbmArchiving.Schemadbo.CTA_File.DBFindByCBarras_File(CBarrasLocal)

                                'Dim dtFileFolder = dbmArchiving.Schemadbo.CTA_Folder_File.DBFindByfk_OTCBarras_File(dtFile(0).fk_OT, CBarrasLocal)

                                If dtFile(0).fk_Reproceso_Motivo = DesktopConfig.MotivoReproceso.ValidacionesObligatorias Or dtFile(0).fk_Reproceso_Motivo = DesktopConfig.MotivoReproceso.ValidacionesDestapeObligatorias Then

                                    Dim data = dbmArchiving.Schemadbo.PA_Reproceso_Reasignar.DBExecute(CBarrasLocal, _idLineaProceso, _OTReproceso, estado)
                                    If CInt(data.Rows(0)("Tipo")) <> 1 Then
                                        numNoValidos += 1
                                        DesktopMessageBoxControl.DesktopMessageShow(data.Rows(0)("MENSAJE").ToString(), "Error", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                                    End If

                                Else
                                    numNoValidos += 1
                                End If
                            End If
                        Next
                End Select

                If numNoValidos > 0 Then
                    DesktopMessageBoxControl.DesktopMessageShow("No se procesaron [" & numNoValidos.ToString & "] documentos, ya que no soportan el estado: " & estado.ToString().Replace("_", " "), "No procesados", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                Else
                    DesktopMessageBoxControl.DesktopMessageShow("Se procesaron los documentos seleccionados, con el estado " & estado.ToString().Replace("_", " ") & vbNewLine & vbNewLine & "La nueva Línea de proceso es: " & _idLineaProceso.ToString(), "Procesados [Línea de Proceso: " & _idLineaProceso.ToString() & "]", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                End If
                BuscarReprocesos()

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CambiaEstadoElemento", ex)
            Finally
                dbmArchiving.Connection_Close()
            End Try

            'End If

        End Sub

        Private Sub AsignaLineaProceso(ByRef dbmArchiving As DBArchivingDataBaseManager, ByRef idLineaProceso As Integer, ByVal CentroProcesamientoRow As DBCore.SchemaSecurity.CTA_Centro_ProcesamientoSimpleType, ByVal riskGlobal As RiskGlobal, ByVal estado As DBCore.EstadoEnum, ByVal idUsuario As Integer)
            Try
                'Se consulta si existe una línea de proceso para el usuario.
                Dim dtLineaProceso = dbmArchiving.SchemaRisk.TBL_Linea_Proceso.DBFindByfk_Sede_Procesofk_Centro_Procesamientofk_Entidad_Clientefk_Proyectofk_EstadoActivofk_Usuario(CentroProcesamientoRow.fk_Sede, CentroProcesamientoRow.id_Centro_Procesamiento, riskGlobal.Entidad, riskGlobal.Proyecto, estado, True, idUsuario)
                Dim Transaccion As Boolean = False

                If dtLineaProceso.Count > 0 Then
                    idLineaProceso = dtLineaProceso(0).id_Linea_Proceso
                Else
                    If dbmArchiving.DataBase.ExistsTransaction Then
                        Transaccion = True
                    End If

                    If Not Transaccion Then dbmArchiving.Transaction_Begin()

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

                    If Not Transaccion Then dbmArchiving.Transaction_Commit()

                End If
            Catch ex As Exception
                dbmArchiving.Transaction_Rollback()
                DesktopMessageBoxControl.DesktopMessageShow("AsignaLineaProceso", ex)
            End Try
        End Sub

#End Region

#Region " Funciones "

        Private Function ValidarOT(ByVal estado As DBCore.EstadoEnum, ByRef mOT As Integer) As Boolean

            If estado <> DBCore.EstadoEnum.Cargado Then

                Dim OTList As New List(Of Integer)

                For Each row As DataGridViewRow In ReprocesosDataGridView.Rows
                    If CBool(row.Cells(ReprocesosDataGridView.Columns("R").Index).Value) Then
                        Dim OTSeleccionada = CInt(row.Cells(ReprocesosDataGridView.Columns("OT").Index).Value)
                        OTList.Add(OTSeleccionada)
                    End If
                Next

                Dim FormOt As New FormSeleccionOTs(OTList)
                If FormOt.ShowDialog() = DialogResult.OK Then
                    mOT = FormOt.OTSelect
                    Return True
                Else
                    Return False
                End If

            Else
                Return True
            End If

        End Function

        Private Function ValidaSeleccionItems() As Boolean
            Dim bReturn As Boolean = False
            Dim nSelecionados As Integer = 0
            Try
                For Each row As DataGridViewRow In ReprocesosDataGridView.Rows
                    If CBool(row.Cells(ReprocesosDataGridView.Columns("R").Index).Value) = True Then
                        nSelecionados += 1
                    End If
                Next

                If nSelecionados > 0 Then bReturn = True
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("ValidaSeleccionItems", ex)
            End Try
            Return bReturn
        End Function

#End Region


    End Class

End Namespace