Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Controls

Namespace Procesos.Cargue

    Public Class FormEstadoCargue

#Region " Eventos "

        Private Sub FormEstadoCargue_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
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

            If MessageBox.Show("Está seguro que desea desbloquear?", Program.AssemblyName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                Desbloquear()
            End If

        End Sub

        Private Sub ReasignarCargueButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ReasignarCargueButton.Click
            ReasignarPaquete()
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

                    Dim FechaInicial As New DateTime(FechaInicialDateTimePicker.Value.Year, FechaInicialDateTimePicker.Value.Month, FechaInicialDateTimePicker.Value.Day)
                    Dim FechaFinal As New DateTime(FechaFinalDateTimePicker.Value.Year, FechaFinalDateTimePicker.Value.Month, FechaFinalDateTimePicker.Value.Day, 23, 59, 59)

                    Dim CargueSeguimientoDataTable = New DBImaging.SchemaProcess.CTA_Cargue_SeguimientoDataTable()
                    CargueSeguimientoDataTable.id_CargueColumn.DataType = GetType(String)
                    CargueSeguimientoDataTable.id_Cargue_PaqueteColumn.DataType = GetType(String)
                    CargueSeguimientoDataTable.Fecha_CargueColumn.DataType = GetType(String)
                    CargueSeguimientoDataTable.Fecha_ProcesoColumn.DataType = GetType(String)

                    Dim Data As DBImaging.SchemaProcess.CTA_Cargue_SeguimientoDataTable
                    If (Program.ImagingGlobal.ProyectoImagingRow.Usa_Paquete_x_Imagen) Then
                        Data = dbmImaging.SchemaProcess.PA_Cargue_Seguimiento_Agrupado_get.DBExecute(Program.ImagingGlobal.ProyectoImagingRow.fk_Entidad, Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto, FechaInicial, FechaFinal, CarguesActivosCheckBox.Checked)
                    Else
                        Data = dbmImaging.SchemaProcess.PA_Cargue_Seguimiento_get.DBExecute(Program.ImagingGlobal.ProyectoImagingRow.fk_Entidad, Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto, FechaInicial, FechaFinal, CarguesActivosCheckBox.Checked)
                    End If

                    ' Calcular totales
                    Dim TotalesRow = CargueSeguimientoDataTable.NewCTA_Cargue_SeguimientoRow()

                    TotalesRow.fk_Entidad = -1
                    TotalesRow.fk_Proyecto = -1
                    TotalesRow.id_Cargue = 0
                    TotalesRow.id_Cargue_Paquete = 0
                    TotalesRow.id_Estado = 0
                    TotalesRow.Nombre_Estado = "TOTAL ->"
                    TotalesRow.Fecha_Cargue = Now
                    TotalesRow.Fecha_Proceso = Now
                    TotalesRow.Observaciones = ""
                    TotalesRow.Nombre_Sede = ""
                    TotalesRow.Nombre_Centro_Procesamiento = ""
                    TotalesRow.Data_Path = ""
                    TotalesRow.Key_1 = ""
                    TotalesRow.Key_2 = ""
                    TotalesRow.Key_3 = ""

                    TotalesRow.Paquetes = 0
                    TotalesRow.Items = 0
                    TotalesRow.Expedientes = 0
                    TotalesRow.Files = 0
                    TotalesRow.Indexacion = 0
                    TotalesRow.Retenido = 0
                    TotalesRow.Pre_Captura = 0
                    TotalesRow.Captura = 0
                    TotalesRow.Segunda_Captura = 0
                    TotalesRow.Tercera_Captura = 0
                    TotalesRow.Reproceso = 0
                    TotalesRow.Calidad = 0
                    TotalesRow.Indexado = 0
                    TotalesRow.Validaciones_Totales = 0
                    TotalesRow.Validaciones_Pendientes = 0
                    TotalesRow.Validacion_Listas = 0

                    TotalesRow.OCR_Captura = 0
                    TotalesRow.OCR_Indexacion = 0

                    For Each Fila In Data
                        If (Program.ImagingGlobal.ProyectoImagingRow.Usa_Paquete_x_Imagen) Then
                            TotalesRow.Paquetes += Fila.Paquetes
                        Else
                            TotalesRow.Paquetes += 1
                        End If

                        TotalesRow.Items += Fila.Items
                        TotalesRow.Expedientes += Fila.Expedientes
                        TotalesRow.Files += Fila.Files
                        TotalesRow.Indexacion += Fila.Indexacion
                        TotalesRow.Retenido += Fila.Retenido
                        TotalesRow.Pre_Captura += Fila.Pre_Captura
                        TotalesRow.Captura += Fila.Captura
                        TotalesRow.Reproceso += Fila.Reproceso
                        TotalesRow.Segunda_Captura += Fila.Segunda_Captura
                        TotalesRow.Tercera_Captura += Fila.Tercera_Captura
                        TotalesRow.Calidad += Fila.Calidad
                        TotalesRow.Indexado += Fila.Indexado
                        TotalesRow.Validaciones_Totales += Fila.Validaciones_Totales
                        TotalesRow.Validaciones_Pendientes += Fila.Validaciones_Pendientes
                        TotalesRow.Validacion_Listas += Fila.Validacion_Listas

                        TotalesRow.OCR_Captura += Fila.OCR_Captura
                        TotalesRow.OCR_Indexacion += Fila.OCR_Indexacion

                        CargueSeguimientoDataTable.Rows.Add(Fila.ItemArray)
                    Next

                    CargueSeguimientoDataTable.AddCTA_Cargue_SeguimientoRow(TotalesRow)

                    DatosDataGridView.DataSource = CargueSeguimientoDataTable
                    DatosDataGridView.ClearSelection()

                    ' Borrar data invalida de totales
                    Dim PaqueteRow = DatosDataGridView.Rows(DatosDataGridView.Rows.Count - 1)
                    PaqueteRow.Cells(Columnid_Cargue.Index).Value = ""
                    PaqueteRow.Cells(Columnid_Cargue_Paquete.Index).Value = ""
                    PaqueteRow.Cells(ColumnFecha_Cargue.Index).Value = ""
                    PaqueteRow.Cells(ColumnFecha_Proceso.Index).Value = ""

                    PaqueteRow.DefaultCellStyle.BackColor = Drawing.Color.LightYellow
                    PaqueteRow.DefaultCellStyle.ForeColor = Drawing.Color.Red
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
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)

                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                dbmImaging.SchemaProcess.PA_Dashboard_Desbloquear.DBExecute(Nothing, Nothing)
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Desbloquear", ex)
                Return
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try

            MessageBox.Show("El proceso de desbloqueo se realizó exitosamente", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Sub

        'Private Sub ReasignarCargue()
        '    Try
        '        If (DatosDataGridView.SelectedRows.Count > 0) Then
        '            Dim PaquetesList As New List(Of FormReasignarCargue.CarguePaquete)

        '            For Each PaqueteRow As DataGridViewRow In DatosDataGridView.SelectedRows()
        '                PaquetesList.Add(New FormReasignarCargue.CarguePaquete(CInt(PaqueteRow.Cells(Columnid_Cargue.Index).Value), CShort(PaqueteRow.Cells(Columnid_Cargue_Paquete.Index).Value)))
        '            Next

        '            Dim formReasignarCargue As New FormReasignarCargue()
        '            formReasignarCargue.Paquetes = PaquetesList
        '            formReasignarCargue.ShowDialog()

        '            Application.DoEvents()
        '            Buscar()
        '        Else
        '            DesktopMessageBox.DesktopMessageShow("No existen cargues seleccionados.", "Selección de Cargues", Desktop.Controls.DesktopMessageBox.IconEnum.WarningIcon, True)
        '        End If
        '    Catch ex As Exception
        '        DesktopMessageBox.DesktopMessageShow("ReasignarCargue", ex)
        '    End Try
        'End Sub

        Private Sub LoadConfig()
            Dim NewDate = Now

            If (NewDate.Hour < 9) Then
                NewDate = NewDate.AddDays(-1)
            End If

            FechaInicialDateTimePicker.Value = NewDate

            ' Ocultar columnas opcionales        
            'Me.ColumnData_Path.Visible = Program.ImagingGlobal.ProyectoImagingRow.Seguimiento_Show_Col_Data_Path
            'Me.ColumnFecha_Cargue.Visible = Program.ImagingGlobal.ProyectoImagingRow.Seguimiento_Show_Col_Fecha_Cargue
            'Me.ColumnFecha_Proceso.Visible = Program.ImagingGlobal.ProyectoImagingRow.Seguimiento_Show_Col_Fecha_Proceso
            'Me.ColumnFiles.Visible = Program.ImagingGlobal.ProyectoImagingRow.Seguimiento_Show_Col_Files
            'Me.Columnid_Cargue.Visible = Program.ImagingGlobal.ProyectoImagingRow.Seguimiento_Show_Col_id_Cargue
            Me.Columnid_Cargue_Paquete.Visible = Program.ImagingGlobal.ProyectoImagingRow.Seguimiento_Show_Col_fk_Paquete And Not Program.ImagingGlobal.ProyectoImagingRow.Usa_Paquete_x_Imagen
            Me.ColumnIndexacion.Visible = Program.ImagingGlobal.ProyectoImagingRow.Seguimiento_Show_Col_Indexacion
            Me.ColumnIndexado.Visible = Program.ImagingGlobal.ProyectoImagingRow.Seguimiento_Show_Col_Indexado
            Me.ColumnItems.Visible = Program.ImagingGlobal.ProyectoImagingRow.Seguimiento_Show_Col_Items
            Me.ColumnKey_1.Visible = Program.ImagingGlobal.ProyectoImagingRow.Seguimiento_Show_Col_Key01
            Me.ColumnKey_2.Visible = Program.ImagingGlobal.ProyectoImagingRow.Seguimiento_Show_Col_Key02
            Me.ColumnKey_3.Visible = Program.ImagingGlobal.ProyectoImagingRow.Seguimiento_Show_Col_Key03
            'Me.ColumnNombre_Centro_Procesamiento.Visible = Program.ImagingGlobal.ProyectoImagingRow.Seguimiento_Show_Col_Centro_Procesamiento
            'Me.ColumnNombre_Estado.Visible = Program.ImagingGlobal.ProyectoImagingRow.Seguimiento_Show_Col_Estado
            'Me.ColumnNombre_Sede.Visible = Program.ImagingGlobal.ProyectoImagingRow.Seguimiento_Show_Col_Sede
            Me.ColumnObservaciones.Visible = Program.ImagingGlobal.ProyectoImagingRow.Seguimiento_Show_Col_Observaciones
            Me.ColumnPaquetes.Visible = Program.ImagingGlobal.ProyectoImagingRow.Seguimiento_Show_Col_Paquetes
            Me.ColumnPre_Captura.Visible = Program.ImagingGlobal.ProyectoImagingRow.Seguimiento_Show_Col_PreCaptura
            Me.ColumnPrimera_Captura.Visible = Program.ImagingGlobal.ProyectoImagingRow.Seguimiento_Show_Col_PrimeraCaptura
            Me.ColumnSegunda_Captura.Visible = Program.ImagingGlobal.ProyectoImagingRow.Seguimiento_Show_Col_SegundaCaptura
            Me.ColumnTercera_Captura.Visible = Program.ImagingGlobal.ProyectoImagingRow.Seguimiento_Show_Col_TerceraCaptura
            Me.ColumnCalidad.Visible = Program.ImagingGlobal.ProyectoImagingRow.Seguimiento_Show_Col_Calidad
            Me.ColumnValidaciones_Pendientes.Visible = Program.ImagingGlobal.ProyectoImagingRow.Seguimiento_Show_Col_Validaciones_Pendientes
            Me.ColumnValidaciones_Totales.Visible = Program.ImagingGlobal.ProyectoImagingRow.Seguimiento_Show_Col_Validaciones_Totales
            Me.ColumnRetenido.Visible = Program.ImagingGlobal.ProyectoImagingRow.Seguimiento_Show_Col_Retenido
            Me.ColumnReproceso.Visible = Program.ImagingGlobal.ProyectoImagingRow.Seguimiento_Show_Col_Reproceso
            Me.ColumnValidacion_Listas.Visible = Program.ImagingGlobal.ProyectoImagingRow.Seguimiento_Show_Col_Validacion_Listas
            Me.OCR_Captura.Visible = Program.ImagingGlobal.ProyectoImagingRow.Seguimiento_Show_Col_OCRCaptura
            Me.OCR_Indexacion.Visible = Program.ImagingGlobal.ProyectoImagingRow.Seguimiento_Show_Col_OCRIndexacion

            ' Titulo de las columna
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing

            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)

                dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                Dim LlavesDataTable = dbmCore.SchemaConfig.TBL_Proyecto_Llave.DBGet(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, Nothing, 3, New DBCore.SchemaConfig.TBL_Proyecto_LlaveEnumList(DBCore.SchemaConfig.TBL_Proyecto_LlaveEnum.id_Proyecto_Llave, True))


                If (LlavesDataTable.Count > 0) Then
                    ColumnKey_1.HeaderText = LlavesDataTable(0).Nombre_Proyecto_Llave
                Else
                    ColumnKey_1.Visible = False
                End If

                If (LlavesDataTable.Count > 1) Then
                    ColumnKey_2.HeaderText = LlavesDataTable(1).Nombre_Proyecto_Llave
                Else
                    ColumnKey_2.Visible = False
                End If

                If (LlavesDataTable.Count > 2) Then
                    ColumnKey_3.HeaderText = LlavesDataTable(2).Nombre_Proyecto_Llave
                Else
                    ColumnKey_3.Visible = False
                End If

            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            End Try

        End Sub

        Private Sub ReasignarPaquete()
            Try
                Dim PaquetesList As New List(Of FormReasignarCargue.CarguePaquete)

                For Each PaqueteRow As DataGridViewRow In DatosDataGridView.SelectedRows()
                    Dim Cargue = CInt(PaqueteRow.Cells(Columnid_Cargue.Index).Value)

                    If (Cargue <> 0) Then
                        Dim Paquete = CShort(PaqueteRow.Cells(Columnid_Cargue_Paquete.Index).Value)
                        PaquetesList.Add(New FormReasignarCargue.CarguePaquete(Cargue, Paquete))
                    End If
                Next

                If (PaquetesList.Count > 0) Then
                    Dim formReasignarCargue As New FormReasignarCargue()
                    formReasignarCargue.Paquetes = PaquetesList
                    Dim Respuesta = formReasignarCargue.ShowDialog()

                    Application.DoEvents()

                    If (Respuesta = Windows.Forms.DialogResult.OK) Then Buscar()
                Else
                    DesktopMessageBoxControl.DesktopMessageShow("No existen paquetes seleccionados.", "Selección de Paquetes", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("ReasignarPaquete", ex)
            End Try
        End Sub

#End Region

#Region " Funciones "

        Private Function Validar() As Boolean
            Dim FechaInicial = New DateTime(FechaInicialDateTimePicker.Value.Year, FechaInicialDateTimePicker.Value.Month, FechaInicialDateTimePicker.Value.Day)
            Dim FechaFinal = New DateTime(FechaFinalDateTimePicker.Value.Year, FechaFinalDateTimePicker.Value.Month, FechaFinalDateTimePicker.Value.Day)

            If (FechaInicial > FechaFinal) Then
                MessageBox.Show("La fecha final no puede ser inferior a la fecha inicial", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                Return True
            End If

            Return False
        End Function

#End Region

    End Class
End Namespace