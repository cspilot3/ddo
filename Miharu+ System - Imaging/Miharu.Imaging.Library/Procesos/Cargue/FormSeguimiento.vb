Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Controls

Namespace Procesos.Cargue

    Public Class FormSeguimiento

#Region " Eventos "

        Private Sub FormSeguimiento_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            DesbloquearButton.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Imaging.Proceso.Control.Acceso)

            LoadConfig()
        End Sub

        Private Sub BuscarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BuscarButton.Click
            Buscar()
        End Sub

        Private Sub CerrarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CerrarButton.Click
            Me.Close()
        End Sub

        Private Sub DesbloquearButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles DesbloquearButton.Click
            Desbloquear()
        End Sub

        Private Sub ReasignarCargueButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ReasignarCargueButton.Click
            Reasignar()
        End Sub

#End Region

#Region " Metodos "

        Private Sub Buscar()
            If (Validar()) Then
                Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

                Try
                    dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)

                    dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                    Me.Cursor = Cursors.AppStarting

                    Dim Fecha_Proceso = CInt(FechaProcesoComboBox.SelectedValue)

                    Dim CargueSeguimientoDataTable = New DBImaging.SchemaProcess.CTA_SeguimientoDataTable()

                    Dim Data As DBImaging.SchemaProcess.CTA_SeguimientoDataTable

                    Data = dbmImaging.SchemaProcess.PA_Seguimiento_get.DBExecute(Program.DesktopGlobal.PuestoTrabajoRow.fk_Entidad, Program.ImagingGlobal.ProyectoImagingRow.fk_Entidad, Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto, Fecha_Proceso)

                    ' Calcular totales
                    Dim TotalesRow = CargueSeguimientoDataTable.NewCTA_SeguimientoRow()


                    TotalesRow.idSedeCargue = -1
                    TotalesRow.SedeCargue = ""
                    TotalesRow.idCentroCargue = -1
                    TotalesRow.CentroCargue = ""
                    TotalesRow.id_OT = 0
                    TotalesRow.Nombre_OT_Tipo = ""
                    TotalesRow.idSedeAsignado = 0
                    TotalesRow.SedeAsignado = ""
                    TotalesRow.idCentroAsignado = 0
                    TotalesRow.CentroAsignado = ""

                    TotalesRow.Precintos = 0
                    TotalesRow.Contenedores = 0
                    TotalesRow.Contenedores_Cargados = 0
                    TotalesRow.Contenedores_Empacados = 0
                    TotalesRow.Documentos = 0
                    TotalesRow.Documentos_Cargados = 0
                    TotalesRow.Documentos_Empacados = 0
                    TotalesRow.Expedientes = 0
                    TotalesRow.Files = 0
                    TotalesRow.Paquetes = 0
                    TotalesRow.OCR_Indexacion = 0
                    TotalesRow.Indexacion = 0
                    TotalesRow.Retenido = 0
                    TotalesRow.Pre_Captura = 0
                    TotalesRow.Captura = 0
                    TotalesRow.Segunda_Captura = 0
                    TotalesRow.Tercera_Captura = 0
                    TotalesRow.Calidad = 0
                    TotalesRow.Indexado = 0
                    TotalesRow.Reproceso = 0
                    TotalesRow.Recorte = 0
                    TotalesRow.Calidad_Recorte = 0
                    TotalesRow.Validaciones = 0
                    TotalesRow.Validacion_Listas = 0
                    TotalesRow.Correccion_Captura = 0
                    TotalesRow.OCR_Captura = 0

                    For Each Fila In Data

                        Fila.Indexado = Fila.Files - (Fila.Reproceso + Fila.Pre_Captura + Fila.Retenido + Fila.Captura + Fila.Segunda_Captura + Fila.Tercera_Captura + Fila.Calidad + Fila.Recorte + Fila.Calidad_Recorte + Fila.Validacion_Listas + Fila.Correccion_Captura + Fila.OCR_Captura)

                        TotalesRow.Precintos += Fila.Precintos
                        TotalesRow.Contenedores += Fila.Contenedores
                        TotalesRow.Contenedores_Cargados += Fila.Contenedores_Cargados
                        TotalesRow.Contenedores_Empacados += Fila.Contenedores_Empacados
                        TotalesRow.Documentos += Fila.Documentos
                        TotalesRow.Documentos_Cargados += Fila.Documentos_Cargados
                        TotalesRow.Documentos_Empacados += Fila.Documentos_Empacados
                        TotalesRow.Expedientes += Fila.Expedientes
                        TotalesRow.Files += Fila.Files
                        TotalesRow.Paquetes += Fila.Paquetes
                        TotalesRow.OCR_Indexacion += Fila.OCR_Indexacion
                        TotalesRow.Indexacion += Fila.Indexacion
                        TotalesRow.Retenido += Fila.Retenido
                        TotalesRow.OCR_Captura += Fila.OCR_Captura
                        TotalesRow.Pre_Captura += Fila.Pre_Captura
                        TotalesRow.Captura += Fila.Captura
                        TotalesRow.Segunda_Captura += Fila.Segunda_Captura
                        TotalesRow.Tercera_Captura += Fila.Tercera_Captura
                        TotalesRow.Calidad += Fila.Calidad
                        TotalesRow.Reproceso += Fila.Reproceso
                        TotalesRow.Recorte += Fila.Recorte
                        TotalesRow.Calidad_Recorte += Fila.Calidad_Recorte
                        TotalesRow.Indexado += Fila.Indexado
                        TotalesRow.Validaciones += Fila.Validaciones
                        TotalesRow.Validacion_Listas += Fila.Validacion_Listas
                        TotalesRow.Correccion_Captura += Fila.Correccion_Captura

                        CargueSeguimientoDataTable.Rows.Add(Fila.ItemArray)
                    Next

                    CargueSeguimientoDataTable.AddCTA_SeguimientoRow(TotalesRow)

                    DatosDataGridView.AutoGenerateColumns = False
                    DatosDataGridView.DataSource = CargueSeguimientoDataTable
                    DatosDataGridView.ClearSelection()

                    ' Borrar data invalida de totales
                    Dim PaqueteRow = DatosDataGridView.Rows(DatosDataGridView.Rows.Count - 1)
                    PaqueteRow.Cells(ColumnNombre_Sede.Index).Value = ""
                    PaqueteRow.Cells(ColumnNombre_Centro_Procesamiento.Index).Value = ""

                    PaqueteRow.DefaultCellStyle.BackColor = Drawing.Color.LightYellow
                    PaqueteRow.DefaultCellStyle.ForeColor = Drawing.Color.Red
                    PaqueteRow.Cells(Columnid_OT.Index).Style.ForeColor = Drawing.Color.LightYellow
                    PaqueteRow.DefaultCellStyle.Font = New Drawing.Font("Microsoft Sans Serif", 9, Drawing.FontStyle.Bold)

                    MessageBox.Show("Se encontraron " & CargueSeguimientoDataTable.Count - 1 & " cargues para el rango de fechas seleccionado", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)

                Catch ex As Exception
                    MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                End Try
            End If

            Me.Cursor = Cursors.Default
        End Sub

        Private Sub Desbloquear()
            Dim newForm = New FormDesbloquear()
            Dim Fecha_Proceso = CInt(FechaProcesoComboBox.SelectedValue)

            newForm.FechaProceso = Fecha_Proceso

            newForm.ShowDialog()
        End Sub

        Private Sub LoadConfig()

            CargarFechas()

            ' Ocultar columnas opcionales        
            'Me.ColumnData_Path.Visible = Program.ImagingGlobal.ProyectoImagingRow.Seguimiento_Show_Col_Data_Path
            'Me.ColumnFecha_Cargue.Visible = Program.ImagingGlobal.ProyectoImagingRow.Seguimiento_Show_Col_Fecha_Cargue
            'Me.ColumnFecha_Proceso.Visible = Program.ImagingGlobal.ProyectoImagingRow.Seguimiento_Show_Col_Fecha_Proceso
            'Me.ColumnFiles.Visible = Program.ImagingGlobal.ProyectoImagingRow.Seguimiento_Show_Col_Files
            'Me.Columnid_Cargue.Visible = Program.ImagingGlobal.ProyectoImagingRow.Seguimiento_Show_Col_id_Cargue
            'Me.ColumnNombre_Centro_Procesamiento.Visible = Program.ImagingGlobal.ProyectoImagingRow.Seguimiento_Show_Col_Centro_Procesamiento
            'Me.ColumnNombre_Estado.Visible = Program.ImagingGlobal.ProyectoImagingRow.Seguimiento_Show_Col_Estado
            'Me.ColumnNombre_Sede.Visible = Program.ImagingGlobal.ProyectoImagingRow.Seguimiento_Show_Col_Sede

            ColumnPaquetes.Visible = Program.ImagingGlobal.ProyectoImagingRow.Seguimiento_Show_Col_Paquetes
            ColumnOCRIndexacion.Visible = Program.ImagingGlobal.ProyectoImagingRow.Seguimiento_Show_Col_OCRIndexacion
            ColumnIndexacion.Visible = Program.ImagingGlobal.ProyectoImagingRow.Seguimiento_Show_Col_Indexacion
            ColumnRetenido.Visible = Program.ImagingGlobal.ProyectoImagingRow.Seguimiento_Show_Col_Retenido
            ColumnOCRCaptura.Visible = Program.ImagingGlobal.ProyectoImagingRow.Seguimiento_Show_Col_OCRCaptura
            ColumnPreCaptura.Visible = Program.ImagingGlobal.ProyectoImagingRow.Seguimiento_Show_Col_PreCaptura
            ColumnCaptura.Visible = Program.ImagingGlobal.ProyectoImagingRow.Seguimiento_Show_Col_PrimeraCaptura
            ColumnSegundaCaptura.Visible = Program.ImagingGlobal.ProyectoImagingRow.Seguimiento_Show_Col_SegundaCaptura
            ColumnTerceraCaptura.Visible = Program.ImagingGlobal.ProyectoImagingRow.Seguimiento_Show_Col_TerceraCaptura
            ColumnCalidad.Visible = Program.ImagingGlobal.ProyectoImagingRow.Seguimiento_Show_Col_Calidad
            ColumnReproceso.Visible = Program.ImagingGlobal.ProyectoImagingRow.Seguimiento_Show_Col_Reproceso
            ColumnValidaciones.Visible = Program.ImagingGlobal.ProyectoImagingRow.Seguimiento_Show_Col_Validaciones_Pendientes
            ColumnRecortes.Visible = Program.ImagingGlobal.ProyectoImagingRow.Seguimiento_Show_Col_Recortes
            ColumnCalidadRecorte.Visible = Program.ImagingGlobal.ProyectoImagingRow.Seguimiento_Show_Col_Calidad_Recortes
            ColumnValidacionListas.Visible = Program.ImagingGlobal.ProyectoImagingRow.Seguimiento_Show_Col_Validacion_Listas
            ColumnCorreccionCaptura.Visible = Program.ImagingGlobal.ProyectoImagingRow.Seguimiento_Show_Col_CorreccionCaptura

            ' Titulo de las columna
            
        End Sub

        Private Sub Reasignar()
            If (DatosDataGridView.SelectedRows.Count > 0) Then
                Try
                    Dim SeguimientoSeleccionado = CType(CType(DatosDataGridView.CurrentRow.DataBoundItem, DataRowView).Row, DBImaging.SchemaProcess.CTA_SeguimientoRow)

                    Dim FormReasignarPaqueteCantidad As New FormReasignarPaqueteCantidad()
                    FormReasignarPaqueteCantidad.ConfigForm(SeguimientoSeleccionado)

                    Dim Respuesta = FormReasignarPaqueteCantidad.ShowDialog()

                    Application.DoEvents()

                    If (Respuesta = Windows.Forms.DialogResult.OK) Then Buscar()

                Catch ex As Exception
                    DesktopMessageBoxControl.DesktopMessageShow("ReasignarPaquete", ex)
                End Try
            End If
        End Sub

        Private Sub CargarFechas()
            Dim dbmImaging As New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)

            Try
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                Dim fechaProcesoData = dbmImaging.SchemaProcess.TBL_Fecha_Proceso.DBFindByfk_Entidadfk_ProyectoCerradofk_Entidad_Procesamiento(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, False, Program.DesktopGlobal.PuestoTrabajoRow.fk_Entidad, 0, New DBImaging.SchemaProcess.TBL_Fecha_ProcesoEnumList(DBImaging.SchemaProcess.TBL_Fecha_ProcesoEnum.Fecha_Proceso, False))

                FechaProcesoComboBox.DataSource = fechaProcesoData
                FechaProcesoComboBox.DisplayMember = "Fecha_Proceso"
                FechaProcesoComboBox.ValueMember = "id_Fecha_Proceso"

                FechaProcesoComboBox.Refresh()
            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                dbmImaging.Connection_Close()
            End Try
        End Sub


#End Region

#Region " Funciones "

        Private Function Validar() As Boolean
            Return True
        End Function

#End Region

    End Class
End Namespace