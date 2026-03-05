Imports System.Drawing
Imports System.Linq
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar
Imports Miharu.Desktop.Controls.DesktopReportViewer
Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Library.MiharuDMZ
Imports Miharu.Imaging.Library.Procesos
Imports Miharu.Imaging.Library.Procesos.Exportar
Imports Slyg.Tools.Progress

Public Class FormCentralizador

#Region " Declaraciones "
    Private dtMediosMagneticos As DBImaging.SchemaProcess.CTA_Expediente_Medio_MagneticoDataTable
    Private ProgressForm As New FormProgress()

    Public Sub New()

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().

    End Sub

#End Region

#Region " Constructores "

    Private Sub FormCentralizador_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CargarFechasRecaudo()
        ObtenerMediosMagneticos()
        ObtenerTotalesxCiald()
    End Sub

#End Region

    Private Sub ObtenerMediosMagneticos()

        Dim queryResponseMediosMagneticos As QueryResponse = ClientUtil.resolver("[DB_Miharu.Imaging_Core].[Process].[PA_Get_MediosMagneticos_Anexos]", New List(Of QueryParameter) From {
                New QueryParameter With {.name = "fk_Entidad", .value = Program.ImagingGlobal.Entidad.ToString()},
                New QueryParameter With {.name = "fk_Proyecto", .value = Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto.ToString()},
                New QueryParameter With {.name = "fk_Fecha_Recaudo", .value = cbxFechaRecaudo.SelectedValue.ToString()}
                }, QueryRequestType.StoredProcedure, QueryResponseType.Table)

        dtMediosMagneticos = CType(ClientUtil.mapToTypedTable(New DBImaging.SchemaProcess.CTA_Expediente_Medio_MagneticoDataTable(), queryResponseMediosMagneticos.dataTable), DBImaging.SchemaProcess.CTA_Expediente_Medio_MagneticoDataTable)

        If dtMediosMagneticos.Rows.Count > 0 Then
            'Utilities.Llenarcombo(cbxMedioMagnetico, dtMediosMagneticos, DBImaging.SchemaProcess.CTA_Expediente_Medio_MagneticoEnum.MedioMagnetico.ColumnName, DBImaging.SchemaProcess.CTA_Expediente_Medio_MagneticoEnum.MedioMagnetico.ColumnName, True, "-1", "--TODOS--")
            ''Utilities.Llenarcombo(OficinasDesktopComboBox, oficinaDataTable, DBSecurity.SchemaConfig.TBL_Centro_ProcesamientoEnum.Codigo_Centro.ColumnName, DBSecurity.SchemaConfig.TBL_Centro_ProcesamientoEnum.Nombre_Centro_Procesamiento.ColumnName, True, "-1", "--TODOS--")
            'cbxMedioMagnetico.Refresh()

            cbxMedioMagnetico.DataSource = Nothing
            Dim dtMedios As DataTable = dtMediosMagneticos.Clone
            dtMedios.Columns.Add("IDn", GetType(String))

            For Each row As DataRow In dtMediosMagneticos.Rows
                Dim newRow As DataRow = dtMedios.NewRow()
                newRow.ItemArray = DirectCast(row.ItemArray.Clone(), Object())
                newRow("IDn") = row("MedioMagnetico")
                dtMedios.Rows.Add(newRow)
            Next

            Dim nuevaFila As DataRow = dtMedios.NewRow()
            nuevaFila("IDn") = -1
            nuevaFila("MedioMagnetico") = "--TODOS--"
            dtMedios.Rows.InsertAt(nuevaFila, 0)

            cbxMedioMagnetico.DisplayMember = "MedioMagnetico"
            cbxMedioMagnetico.ValueMember = "IDn"

            cbxMedioMagnetico.DataSource = dtMedios

            cbxMedioMagnetico.Refresh()

        Else
            MessageBox.Show("No hay medios magneticos cargados", "Medios magneticos", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub CargarFechasRecaudo()

        Dim fechasRecaudoDataTable As DBImaging.SchemaProcess.TBL_Fecha_RecaudoDataTable
        Dim queryResponse As QueryResponse = ClientUtil.resolver("[DB_Miharu.Imaging_Core].Process.TBL_Fecha_Recaudo", New List(Of QueryParameter) From {
                New QueryParameter With {.name = "fk_Entidad", .value = Program.ImagingGlobal.Entidad.ToString()},
                New QueryParameter With {.name = "fk_Proyecto", .value = Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto.ToString()},
                New QueryParameter With {.name = "Cerrado", .value = "0".ToString()}
                }, QueryRequestType.Table, QueryResponseType.Table)

        fechasRecaudoDataTable = CType(ClientUtil.mapToTypedTable(New DBImaging.SchemaProcess.TBL_Fecha_RecaudoDataTable(), queryResponse.dataTable), DBImaging.SchemaProcess.TBL_Fecha_RecaudoDataTable)
        cbxFechaRecaudo.DataSource = Nothing
        If fechasRecaudoDataTable.Rows.Count > 0 Then
            cbxFechaRecaudo.DisplayMember = "id_Fecha_Recaudo"
            cbxFechaRecaudo.ValueMember = "id_Fecha_Recaudo"

            cbxFechaRecaudo.DataSource = fechasRecaudoDataTable

            cbxFechaRecaudo.Refresh()
        Else
            MessageBox.Show("No hay fechas de recaudo abiertas", "Relevo", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub ObtenerTotalesxCiald()
        If cbxMedioMagnetico.Items.Count = 0 Then
            MsgBox("Debes cargar primero los medios magnéticos para poder generar anexos...", MsgBoxStyle.Information, "Proceso pendiente.")
            Exit Sub
        End If

        Dim queryResponse As QueryResponse = ClientUtil.resolver("[DB_Miharu.Imaging_Core].[Process].[PA_Get_Registros_Relevo_Total_Anexos]", New List(Of QueryParameter) From {
                New QueryParameter With {.name = "fk_Entidad", .value = Program.ImagingGlobal.Entidad.ToString()},
                New QueryParameter With {.name = "fk_Proyecto", .value = Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto.ToString()},
                New QueryParameter With {.name = "fk_Fecha_Recaudo", .value = cbxFechaRecaudo.SelectedValue.ToString()},
                New QueryParameter With {.name = "MedioMagnetico", .value = cbxMedioMagnetico.SelectedValue.ToString()}
                }, QueryRequestType.StoredProcedure, QueryResponseType.Table)

        DGVTotalCiald.Rows.Clear()
        AgregarFilasAlDataGridViewTotal(queryResponse.dataTable, Color.Black)
        DGVTotalCiald.Refresh()
    End Sub
    Private Sub AgregarFilasAlDataGridViewTotal(ByVal tabla As DataTable, ByVal colorTexto As Color)
        For Each row As DataRow In tabla.Rows
            Dim index As Integer = DGVTotalCiald.Rows.Add()
            Dim nuevaFila As DataGridViewRow = DGVTotalCiald.Rows(index)

            nuevaFila.Cells("FechaRecaudo").Value = row("FechaRecaudo")
            nuevaFila.Cells("CialdTot").Value = row("CialdTot")
            nuevaFila.Cells("RegistrosCiald").Value = row("RegistrosCiald")
            nuevaFila.Cells("Relevados").Value = row("Relevados")
            nuevaFila.Cells("Centralizados").Value = row("Centralizados")
            nuevaFila.Cells("Faltantes").Value = row("Faltantes")

            ' Aplicar color a todas las celdas
            For Each celda As DataGridViewCell In nuevaFila.Cells
                celda.Style.ForeColor = colorTexto
            Next
        Next
    End Sub

    Private Sub BtnGenerar_Click(sender As Object, e As EventArgs) Handles BtnGenerar.Click

        If cbxMedioMagnetico.Items.Count = 0 Then
            MsgBox("Debes cargar primero los medios magnéticos para poder generar anexos...", MsgBoxStyle.Information, "Proceso pendiente.")
            Exit Sub
        End If

        Dim idDocumentAnexo11 As Integer
        Dim idDocumentAnexo5 As Integer
        Dim idDocumentAnexo6 As Integer
        Dim idDocumentAnexo88 As Integer
        Dim idDocumentAceptaMedio As Integer
        Dim TotReport As Integer = 0
        If AnexoF11.Checked Then TotReport += 1
        If Anexo5.Checked Then TotReport += 1
        If Anexo6.Checked Then TotReport += 1
        If EntregaDenuncios.Checked Then TotReport += 1
        If AceptacionMedios.Checked Then TotReport += 1

        If Trim(Me.cbxFechaRecaudo.Text) = "" Then
                            MsgBox("Debes seleccionar una fecha de recaudo..", MsgBoxStyle.Information, "Dato pendiente.")
                            Exit Sub
                        End If

                        If Not (AnexoF11.Checked Or Anexo5.Checked Or Anexo6.Checked Or EntregaDenuncios.Checked Or AceptacionMedios.Checked) Then
            MsgBox("Debe seleccionar al menos un tipo de anexo antes de continuar.", MsgBoxStyle.Exclamation, "Advertencia")
            Exit Sub
        End If

        ' Trae los Ids de los documentos de anexo 
        Dim queryResponseParametersSystem As QueryResponse = ClientUtil.resolver("[DB_Miharu.Core].[Config].[TBL_Parametro_Sistema]", New List(Of QueryParameter) From {
                                                                New QueryParameter With {.name = "fk_Entidad", .value = Program.ImagingGlobal.Entidad.ToString()},
                                                                New QueryParameter With {.name = "fk_Proyecto", .value = Program.ImagingGlobal.Proyecto.ToString()}
                                                                }, QueryRequestType.Table, QueryResponseType.Table)

        Dim dtParametrosDocumentos = CType(ClientUtil.mapToTypedTable(New DBCore.SchemaConfig.TBL_Parametro_SistemaDataTable(), queryResponseParametersSystem.dataTable), DBCore.SchemaConfig.TBL_Parametro_SistemaDataTable)
        If dtParametrosDocumentos Is Nothing OrElse dtParametrosDocumentos.Count = 0 Then
            MsgBox("No hay documentos tipo Anexo configurados para este proyecto. Por favor, proceda a configurarlos en la tabla TBL_Parametro_Sistema.", MsgBoxStyle.Exclamation, "Advertencia")
            Exit Sub
        End If

        ' Trae las imagenes
        Dim DtLogos As DBCore.SchemaProcess.CTA_Imagenes_Por_DocumentoDataTable = Cargar_Imagenes()
        If dtParametrosDocumentos Is Nothing OrElse dtParametrosDocumentos.Count = 0 Then
            MsgBox("No hay Imagenes, logos o Firmas configuradas para los anexos seleccionados. Por favor, proceda a configurarlos.", MsgBoxStyle.Exclamation, "Advertencia")
            Exit Sub
        End If
        Try
            'Iniciar proceso                
            ProgressForm.Process = "Generación de reportes"
            ProgressForm.Action = "Generando Anexo 5"
            ProgressForm.ValueProcess = 0
            ProgressForm.ValueAction = 0
            ProgressForm.MaxValueAction = 0
            ProgressForm.TopMost = True
            ProgressForm.Show()
            ProgressForm.MaxValueProcess = TotReport
            Dim Trep As Integer = 0
            ' Recorre cada registro de Medio Magnetico Asociado
            For Each expedienteMedio As DBImaging.SchemaProcess.CTA_Expediente_Medio_MagneticoRow In dtMediosMagneticos
                Dim medioMagnetico = expedienteMedio.MedioMagnetico
                Dim fkExpedienteAsociado = expedienteMedio.id

                If AnexoF11.Checked = True Then
                    ProgressForm.Action = "Generando Anexo F11"
                    Trep += 1
                    ProgressForm.ValueProcess = Trep
                    Application.DoEvents()
                    idDocumentAnexo11 = ObtenerIdDocumento(dtParametrosDocumentos, "DOCUMENTOIDANEXO11")
                    If (idDocumentAnexo11 = 0) Then
                        MsgBox($"Advertencia: No se encontró el documento ANEXO11 configurado para este proyecto. Por favor, proceda a configurarlo.", MsgBoxStyle.Exclamation, "Advertencia")
                        Exit Sub
                    End If
                    GenerarReporte_F11(medioMagnetico, fkExpedienteAsociado, DtLogos, idDocumentAnexo11)
                End If

                If Anexo5.Checked = True Then
                    ProgressForm.Action = "Generando Anexo 5"
                    Trep += 1
                    ProgressForm.ValueProcess = Trep
                    Application.DoEvents()
                    idDocumentAnexo5 = ObtenerIdDocumento(dtParametrosDocumentos, "DOCUMENTOIDANEXO5")
                    If (idDocumentAnexo5 = 0) Then
                        MsgBox($"Advertencia: No se encontró el documento ANEXO5 configurado para este proyecto. Por favor, proceda a configurarlo.", MsgBoxStyle.Exclamation, "Advertencia")
                        Exit Sub
                    End If
                    GenerarReporte_A5(medioMagnetico, fkExpedienteAsociado, DtLogos, idDocumentAnexo5, TotReport, Trep)
                End If
                If Anexo6.Checked = True Then
                    ProgressForm.Action = "Generando Anexo 6"
                    Trep += 1
                    ProgressForm.ValueProcess = Trep
                    Application.DoEvents()
                    idDocumentAnexo6 = ObtenerIdDocumento(dtParametrosDocumentos, "DOCUMENTOIDANEXO6")
                    If (idDocumentAnexo6 = 0) Then
                        MsgBox($"Advertencia: No se encontró el documento ANEXO6 configurado para este proyecto. Por favor, proceda a configurarlo.", MsgBoxStyle.Exclamation, "Advertencia")
                        Exit Sub
                    End If
                    GenerarReporte_A6(medioMagnetico, fkExpedienteAsociado, DtLogos, idDocumentAnexo6, TotReport, Trep)
                End If
                If EntregaDenuncios.Checked = True Then
                    ProgressForm.Action = "Generando entrega denuncios"
                    Trep += 1
                    ProgressForm.ValueProcess = Trep
                    Application.DoEvents()
                    idDocumentAnexo88 = ObtenerIdDocumento(dtParametrosDocumentos, "DOCUMENTOIDANEXO88")
                    If (idDocumentAnexo88 = 0) Then
                        MsgBox($"Advertencia: No se encontró el documento ANEXO88 configurado para este proyecto. Por favor, proceda a configurarlo.", MsgBoxStyle.Exclamation, "Advertencia")
                        Exit Sub
                    End If
                    GenerarReporte_F88(medioMagnetico, fkExpedienteAsociado, DtLogos, idDocumentAnexo88)
                End If

                If AceptacionMedios.Checked = True Then
                    ProgressForm.Action = "Generando Acta de medios"
                    Trep += 1
                    ProgressForm.ValueProcess = Trep
                    Application.DoEvents()
                    idDocumentAceptaMedio = ObtenerIdDocumento(dtParametrosDocumentos, "DOCUMENTOIDACEPTAMEDIOS")
                    If (idDocumentAceptaMedio = 0) Then
                        MsgBox("Advertencia: No se encontró el documento Aceptacion de medios configurado para este proyecto. Por favor, proceda a configurarlo.", MsgBoxStyle.Exclamation, "Advertencia")
                        Exit Sub
                    End If
                    GenerarReporte_AceptaMedio(medioMagnetico, fkExpedienteAsociado, idDocumentAceptaMedio)
                End If
            Next
            ProgressForm.Visible = False
            ProgressForm.Hide()
        Catch ex As Exception
            ProgressForm.Visible = False
            ProgressForm.Hide()
            MsgBox("Error generando Anexos.. Comuniquese con el administrador.")
        End Try
    End Sub

    Private Sub GenerarReporte_AceptaMedio(MedioMag As String, fkExpedienteAsociado As Long, idDocumentACeptaMedio As Integer)
        Try
            Dim FechaRecaudo As Integer = CInt(cbxFechaRecaudo.SelectedValue)
            Dim Version As Short = 1
            Dim Folder As Short = 1
            Dim EsAnexo As Boolean = False
            Dim fk_Anexo As Long = 0
            Dim Folios As Short = 0
            Dim File As Short = 0

            Dim queryResponseFile As QueryResponse = ClientUtil.resolver("[DB_Miharu.Core].[Process].[PA_Get_File_Data]", New List(Of QueryParameter) From {
                  New QueryParameter With {.name = "id_Expediente", .value = fkExpedienteAsociado.ToString()},
                  New QueryParameter With {.name = "id_Folder", .value = Folder.ToString()},
                  New QueryParameter With {.name = "id_Documento", .value = idDocumentACeptaMedio.ToString()}}, QueryRequestType.StoredProcedure, QueryResponseType.Table)

            If Not queryResponseFile.dataTable.Rows.Count > 0 Then
                MsgBox("No se encuentra información para cargar.")
            End If
            File = CShort(queryResponseFile.dataTable.Rows(0).Item("id_File"))
            Folios = CShort(queryResponseFile.dataTable.Rows(0).Item("Folios_File"))

            Dim queryResponseFoliosFile As QueryResponse = ClientUtil.GetFoliosFile("", New List(Of QueryParameter) From {
                 New QueryParameter With {.name = "fk_Expediente", .value = fkExpedienteAsociado.ToString()},
                 New QueryParameter With {.name = "fk_Folder", .value = Folder.ToString()},
                 New QueryParameter With {.name = "fk_File", .value = File.ToString()},
                 New QueryParameter With {.name = "fk_Version", .value = Version.ToString()},
                 New QueryParameter With {.name = "es_Anexo", .value = EsAnexo.ToString()},
                 New QueryParameter With {.name = "fk_Anexo", .value = fk_Anexo.ToString()}
                      }, QueryRequestType.Table, QueryResponseType.Scalar)
            Folios = CShort(queryResponseFoliosFile.scalar)

            Dim TotalFolios As New DBStorage.SchemaImaging.TBL_File_FolioDataTable

            If (Folios > 0) Then
                For folio = CShort(1) To Folios
                    Dim folioFileDataTable As DBStorage.SchemaImaging.TBL_File_FolioDataTable = Nothing
                    Dim queryResponseFolioFile As QueryResponse = ClientUtil.GetFolioFile("", New List(Of QueryParameter) From {
                                 New QueryParameter With {.name = "fk_Expediente", .value = fkExpedienteAsociado.ToString()},
                                 New QueryParameter With {.name = "fk_Folder", .value = Folder.ToString()},
                                 New QueryParameter With {.name = "fk_File", .value = File.ToString()},
                                 New QueryParameter With {.name = "fk_Version", .value = Version.ToString()},
                                 New QueryParameter With {.name = "es_Anexo", .value = EsAnexo.ToString()},
                                 New QueryParameter With {.name = "fk_Anexo", .value = fk_Anexo.ToString()},
                                 New QueryParameter With {.name = "folio", .value = Folios.ToString()}
                                                                }, QueryRequestType.Table, QueryResponseType.Table)

                    folioFileDataTable = CType(ClientUtil.mapToTypedTable(New DBStorage.SchemaImaging.TBL_File_FolioDataTable(), queryResponseFolioFile.dataTable), DBStorage.SchemaImaging.TBL_File_FolioDataTable)

                    For Each row As DataRow In folioFileDataTable.Rows
                        TotalFolios.ImportRow(row)
                    Next
                Next
                'Contultar tabla de registros F11
                Dim miVisorAceptaMedios As New DesktopReportViewer1Control()
                Dim ReportAceptaMedios As New Reportes.Ciald.Report_AceptaMedios(miVisorAceptaMedios) With {
                          .DtAceptaMedios = TotalFolios}

                ReportAceptaMedios.Launch(FechaRecaudo)
                Dim PopupAceptaMedios As New Form()
                PopupAceptaMedios.Text = "Reporte Aceptación de Medios"
                PopupAceptaMedios.Size = New Size(800, 550)
                PopupAceptaMedios.StartPosition = FormStartPosition.CenterScreen
                PopupAceptaMedios.FormBorderStyle = FormBorderStyle.SizableToolWindow
                PopupAceptaMedios.ShowIcon = False

                miVisorAceptaMedios.Dock = DockStyle.Fill
                PopupAceptaMedios.Controls.Add(miVisorAceptaMedios)
                ' Mostrar como ventana emergente
                PopupAceptaMedios.Show()
            Else
                MsgBox("No se encontró imagen asociado.")
                Exit Sub
            End If
        Catch ex As Exception
            MsgBox("Error al generar el reporte de acta de medios, por favor comunicarse con el administrador")
        End Try

    End Sub
    Private Sub GenerarReporte_F11(MedioMag As String, fkExpedienteAsociado As Integer, DtLogos As DataTable, idDocumentAnexo11 As Integer)
        Try
            Dim FechaRecaudo As Integer = CInt(cbxFechaRecaudo.SelectedValue)

            Dim queryResponseParam As QueryResponse = ClientUtil.resolver("[DB_Miharu.Imaging_Core].[Process].[PA_Get_Parametros_AnexoF11]", New List(Of QueryParameter) From {
                  New QueryParameter With {.name = "fk_Entidad", .value = Program.ImagingGlobal.Entidad.ToString()},
                  New QueryParameter With {.name = "fk_Proyecto", .value = Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto.ToString()},
                  New QueryParameter With {.name = "fk_Fecha_Recaudo", .value = FechaRecaudo.ToString()},
                  New QueryParameter With {.name = "MedioMagnetico", .value = MedioMag.ToString},
                  New QueryParameter With {.name = "fk_Expediente", .value = fkExpedienteAsociado.ToString},
                  New QueryParameter With {.name = "fk_Documento", .value = idDocumentAnexo11.ToString}
                }, QueryRequestType.StoredProcedure, QueryResponseType.Table)

            'Dim DtRowF11 As DataRow() = DtLogos.Select("fk_Documento='2'")
            Dim DtRowF11 As DataRow() = DtLogos.Select("fk_Documento = '" & idDocumentAnexo11.ToString() & "'")

            Dim queryResponseRegistros As QueryResponse = ClientUtil.resolver("[DB_Miharu.Imaging_Core].[Process].[PA_Get_Registros_AnexoF11]", New List(Of QueryParameter) From {
                  New QueryParameter With {.name = "fk_Entidad", .value = Program.ImagingGlobal.Entidad.ToString()},
                  New QueryParameter With {.name = "fk_Proyecto", .value = Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto.ToString()},
                  New QueryParameter With {.name = "fk_Fecha_Recaudo", .value = FechaRecaudo.ToString()},
                  New QueryParameter With {.name = "MedioMagnetico", .value = MedioMag.ToString}}, QueryRequestType.StoredProcedure, QueryResponseType.Table)

            'Contultar tabla de registros F11
            Dim miVisorF11 As New DesktopReportViewer1Control()
            Dim ReportF11 As New Reportes.Ciald.Report_AnexoF11(miVisorF11) With {
                   .idDocumentAnexo11 = idDocumentAnexo11,
                   .fkExpedienteAsociado = fkExpedienteAsociado,
                   .DtLogos = DtRowF11.CopyToDataTable,
                   .DtParametrosF11 = queryResponseParam.dataTable,
                   .DtRegistrosF11 = queryResponseRegistros.dataTable}

            ReportF11.Launch(FechaRecaudo)
            Dim PopupF11 As New Form()
            PopupF11.Text = "Reporte F11"
            PopupF11.Size = New Size(830, 550)
            PopupF11.StartPosition = FormStartPosition.CenterScreen
            PopupF11.FormBorderStyle = FormBorderStyle.SizableToolWindow
            PopupF11.ShowIcon = False

            miVisorF11.Dock = DockStyle.Fill
            PopupF11.Controls.Add(miVisorF11)
            ' Mostrar como ventana emergente
            PopupF11.Show()
        Catch
            MsgBox("Error al generar los anexos F11, por favor comunicarse con el administrador")
        End Try

    End Sub
    Private Sub GenerarReporte_F88(MedioMag As String, fkExpedienteAsociado As Integer, DtLogos As DataTable, idDocumentAnexo88 As Integer)
        Try
            Dim FechaRecaudo As Integer = CInt(cbxFechaRecaudo.SelectedValue)
            Dim DtRowF88 As DataRow() = DtLogos.Select("fk_Documento = '" & idDocumentAnexo88.ToString() & "'")

            Dim queryResponseRegistros As QueryResponse = ClientUtil.resolver("[DB_Miharu.Imaging_Core].[Process].[PA_Get_Parametros_AnexoF88]", New List(Of QueryParameter) From {
                  New QueryParameter With {.name = "fk_Entidad", .value = Program.ImagingGlobal.Entidad.ToString()},
                  New QueryParameter With {.name = "fk_Proyecto", .value = Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto.ToString()},
                  New QueryParameter With {.name = "fk_Fecha_Recaudo", .value = FechaRecaudo.ToString()},
                  New QueryParameter With {.name = "MedioMagnetico", .value = MedioMag.ToString},
                  New QueryParameter With {.name = "fk_Expediente", .value = fkExpedienteAsociado.ToString},
                  New QueryParameter With {.name = "fk_Documento", .value = idDocumentAnexo88.ToString}
                }, QueryRequestType.StoredProcedure, QueryResponseType.Table)

            Dim TotalRegistros As Integer = 0
            If queryResponseRegistros.dataTable Is Nothing Then
                MsgBox("No existen registros para generar reporte F88. Verifique.")
                Exit Sub
            End If
            If Not queryResponseRegistros.dataTable.Rows.Count > 0 Then
                MsgBox("No existen registros para generar reporte F88. Verifique.")
                Exit Sub
            End If
            TotalRegistros = queryResponseRegistros.dataTable.Rows.Count
            'Contultar tabla de registros F88
            Dim miVisorF88 As New DesktopReportViewer1Control()
            Dim ReportF88 As New Reportes.Ciald.Report_AnexoF88(miVisorF88) With {
                   .idDocumentAnexo88 = idDocumentAnexo88,
                   .fkExpedienteAsociado = fkExpedienteAsociado,
                   .DtLogos = DtRowF88.CopyToDataTable,
                   .DtRegistrosF88 = queryResponseRegistros.dataTable,
                   .TotalRegistro = TotalRegistros}

            ReportF88.Launch(FechaRecaudo)
            Dim PopupF88 As New Form()
            PopupF88.Text = "Reporte Entrega Denuncios Autorizados"
            PopupF88.Size = New Size(840, 550)
            PopupF88.StartPosition = FormStartPosition.CenterScreen
            PopupF88.FormBorderStyle = FormBorderStyle.SizableToolWindow
            PopupF88.ShowIcon = False

            miVisorF88.Dock = DockStyle.Fill
            PopupF88.Controls.Add(miVisorF88)
            ' Mostrar como ventana emergente
            PopupF88.Show()
        Catch
            MsgBox("Error al generar los anexos de Entrega Denuncios Autorizados, por favor comunicarse con el administrador")
        End Try

    End Sub
    Private Sub GenerarReporte_A5(MedioMag As String, fkExpedienteAsociado As Integer, DtLogos As DataTable, idDocumentAnexo5 As Integer, TotReport As Integer, TotRep As Integer)
        Dim FechaRecaudo As Integer = CInt(cbxFechaRecaudo.SelectedValue)
        Try
            Dim queryResponseAnexo5 As QueryResponse = ClientUtil.resolver("[DB_Miharu.Imaging_Core].[Process].[PA_Get_Parametros_Anexo5]", New List(Of QueryParameter) From {
                     New QueryParameter With {.name = "fk_Entidad", .value = Program.ImagingGlobal.Entidad.ToString()},
                     New QueryParameter With {.name = "fk_Proyecto", .value = Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto.ToString()},
                     New QueryParameter With {.name = "fk_Fecha_Recaudo", .value = FechaRecaudo.ToString()},
                     New QueryParameter With {.name = "MedioMagnetico", .value = MedioMag.ToString()},
                     New QueryParameter With {.name = "fk_Expediente", .value = fkExpedienteAsociado.ToString},
                     New QueryParameter With {.name = "fk_Documento", .value = idDocumentAnexo5.ToString}
                    }, QueryRequestType.StoredProcedure, QueryResponseType.Table)

            'Dim DtRowA5 As DataRow() = DtLogos.Select("fk_Documento='2'")

            If Not queryResponseAnexo5.dataTable.Rows.Count > 0 Then
                MsgBox("No existen registros para generar reporte Anexo 5. Verifique.")
                Exit Sub
            End If

            Dim DtRowA5 As DataRow() = DtLogos.Select("fk_Documento = '" & idDocumentAnexo5.ToString() & "'")

            Dim miVisorA5 As New DesktopReportViewer1Control()
            Dim ReportA5 As New Reportes.Ciald.Report_Anexo5(miVisorA5) With {
                            .idDocumentAnexo5 = idDocumentAnexo5,
                            .fkExpedienteAsociado = fkExpedienteAsociado,
                            .NumeroMedioMagnetico = MedioMag,
                            .ParamCodigoEntidad = Program.ImagingGlobal.Entidad.ToString(),
                            .DtLogos = DtRowA5.CopyToDataTable,
                            .DtRegistrosA5 = queryResponseAnexo5.dataTable,
                            .TotReport = TotReport,
                            .TotRep = TotRep
                            }

            ReportA5.Launch(FechaRecaudo)
            Dim PopupA5 As New Form()
            PopupA5.Text = "Reporte ANEXO 5"
            PopupA5.Size = New Size(820, 650)
            PopupA5.StartPosition = FormStartPosition.CenterScreen
            PopupA5.FormBorderStyle = FormBorderStyle.SizableToolWindow
            PopupA5.ShowIcon = False

            miVisorA5.Dock = DockStyle.Fill
            PopupA5.Controls.Add(miVisorA5)
            ' Mostrar como ventana emergente
            PopupA5.Show()
        Catch
            MsgBox("Error al generar los anexos 5, por favor comunicarse con el administrador")
        End Try

    End Sub
    Private Sub GenerarReporte_A6(MedioMag As String, fkExpedienteAsociado As Integer, DtLogos As DataTable, idDocumentAnexo6 As Integer, TotReport As Integer, TotRep As Integer)
        Dim FechaRecaudo As Integer = CInt(cbxFechaRecaudo.SelectedValue)
        Try
            'Dim DtRowA6 As DataRow() = DtLogos.Select("fk_Documento='3'")
            Dim DtRowA6 As DataRow() = DtLogos.Select("fk_Documento = '" & idDocumentAnexo6.ToString() & "'")

            Dim queryResponseAnexo6 As QueryResponse = ClientUtil.resolver("[DB_Miharu.Imaging_Core].[Process].[PA_Get_Parametros_Anexo6]", New List(Of QueryParameter) From {
                   New QueryParameter With {.name = "fk_Entidad", .value = Program.ImagingGlobal.Entidad.ToString()},
                   New QueryParameter With {.name = "fk_Proyecto", .value = Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto.ToString()},
                   New QueryParameter With {.name = "fk_Fecha_Recaudo", .value = FechaRecaudo.ToString()},
                   New QueryParameter With {.name = "MedioMagnetico", .value = MedioMag.ToString()},
                   New QueryParameter With {.name = "fk_Expediente", .value = fkExpedienteAsociado.ToString},
                   New QueryParameter With {.name = "fk_Documento", .value = idDocumentAnexo6.ToString}
                 }, QueryRequestType.StoredProcedure, QueryResponseType.Table)

            If Not queryResponseAnexo6.dataTable.Rows.Count > 0 Then
                MsgBox("No existen registros para generar reporte Anexo 6. Verifique.")
                Exit Sub
            End If

            Dim miVisorA6 As New DesktopReportViewer1Control()
            Dim ReportA6 As New Reportes.Ciald.Report_Anexo6(miVisorA6) With {
                                        .NumeroMedioMagnetico = MedioMag.ToString(),
                                        .idDocumentAnexo6 = idDocumentAnexo6,
                                        .fkExpedienteAsociado = fkExpedienteAsociado,
                                        .ParamCodigoEntidad = Program.ImagingGlobal.Entidad.ToString(),
                                        .DtRegistrosA6 = queryResponseAnexo6.dataTable,
                                        .DtLogos = DtRowA6.CopyToDataTable,
                                        .TotReport = TotReport,
                                        .TotRep = TotRep}

            ReportA6.Launch(FechaRecaudo)
            Dim PopupA6 As New Form()
            PopupA6.Text = "Reporte ANEXO 6"
            PopupA6.Size = New Size(850, 650)
            PopupA6.StartPosition = FormStartPosition.CenterScreen
            PopupA6.FormBorderStyle = FormBorderStyle.SizableToolWindow
            PopupA6.ShowIcon = False

            miVisorA6.Dock = DockStyle.Fill
            PopupA6.Controls.Add(miVisorA6)
            ' Mostrar como ventana emergente
            PopupA6.Show()
        Catch
            MsgBox("Error al generar los anexos 6, por favor comunicarse con el administrador")
        End Try
    End Sub
    Private Function Cargar_Imagenes() As DBCore.SchemaProcess.CTA_Imagenes_Por_DocumentoDataTable

        Try
            Dim imagenesDataTable As DBCore.SchemaProcess.CTA_Imagenes_Por_DocumentoDataTable

            Dim queryResponse As QueryResponse = ClientUtil.resolver("[DB_Miharu.Core].[Process].[CTA_Imagenes_Por_Documento]", New List(Of QueryParameter) From {
                                                    New QueryParameter With {.name = "Fk_Entidad", .value = Program.ImagingGlobal.Entidad.ToString()},
                                                    New QueryParameter With {.name = "Fk_Proyecto", .value = Program.ImagingGlobal.Proyecto.ToString()},
                                                    New QueryParameter With {.name = "fk_Esquema", .value = CInt(1).ToString}
                                                    }, QueryRequestType.Table, QueryResponseType.Table)

            imagenesDataTable = CType(ClientUtil.mapToTypedTable(New DBCore.SchemaProcess.CTA_Imagenes_Por_DocumentoDataTable(), queryResponse.dataTable), DBCore.SchemaProcess.CTA_Imagenes_Por_DocumentoDataTable)

            Return imagenesDataTable

        Catch ex As Exception
            MessageBox.Show("Error: " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally

        End Try
    End Function
    Private Sub BtnBuscar_Click(sender As Object, e As EventArgs) Handles BtnBuscar.Click
        ObtenerTotalesxCiald()
    End Sub
    Private Function ObtenerIdDocumento(dtParametros As DBCore.SchemaConfig.TBL_Parametro_SistemaDataTable, nombreDocumento As String) As Integer

        Dim rowData = dtParametros.FirstOrDefault(Function(row) row.Nombre_Parametro_Sistema.Trim().ToUpper() = nombreDocumento.Trim().ToUpper())
        If rowData IsNot Nothing Then
            Return CInt(rowData.Valor_Parametro_Sistema)
        Else
            Return 0
        End If
    End Function


    ''' <summary>
    ''' Obtiene la tabla de expediente asociado al medio magnético especificado para la entidad y proyecto activos.
    ''' </summary>
    ''' <returns>Devuelve un objeto DBCore.SchemaProcess.TBL_File_DataDataTable con la información del expediente.</returns>
    Public Function GetFileDataByMedioMagnetico(_NumeroMedioMagnetico As Integer) As DBCore.SchemaProcess.TBL_File_DataDataTable

        ' Buscar el Expediente correspondiente al Medio Magentico
        Dim queryResponse As QueryResponse = ClientUtil.resolver("[DB_Miharu.Core].[Process].[PA_Get_FileData_MediosMagneticos]", New List(Of QueryParameter) From {
                                                        New QueryParameter With {.name = "fk_Entidad", .value = Program.ImagingGlobal.Entidad.ToString()},
                                                        New QueryParameter With {.name = "fk_Proyecto", .value = Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto.ToString()},
                                                        New QueryParameter With {.name = "fk_Numero_Medio_Magnetico", .value = _NumeroMedioMagnetico.ToString()}
                                                        }, QueryRequestType.StoredProcedure, QueryResponseType.Table)

        Dim fileDataTable = CType(ClientUtil.mapToTypedTable(New DBCore.SchemaProcess.TBL_File_DataDataTable(), queryResponse.dataTable), DBCore.SchemaProcess.TBL_File_DataDataTable)

        Return fileDataTable
    End Function

    Private Sub cbxFechaRecaudo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbxFechaRecaudo.SelectedIndexChanged
        ObtenerMediosMagneticos()
        ObtenerTotalesxCiald()
    End Sub
End Class