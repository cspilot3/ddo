Imports System.Windows.Forms
Imports System.Drawing.Imaging
Imports Miharu.Desktop.Controls.BarCode
Imports DBIntegration
Imports DBIntegration.SchemaConfig
Imports DBCore
Imports System.IO
Imports Microsoft.Reporting.WinForms
Imports DBImaging
Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports DBIntegration.SchemaSantander

Namespace Embargos.Forms.FormGenerarCartas
    Public Class FormGenerarCartas

        Private _plugin As EmbargosImagingPlugin

        Public Sub New(nPlugin As EmbargosImagingPlugin)
            _plugin = nPlugin

            InitializeComponent()
        End Sub


        Private Sub GenerarCartas()
            Dim dbmIntegration As DBIntegrationDataBaseManager = Nothing
            Dim dbmCore As DBCoreDataBaseManager = Nothing
            Dim dbmImaging As DBImagingDataBaseManager = Nothing

            Try
                If Validar() Then
                    dbmIntegration = New DBIntegrationDataBaseManager(_plugin.SantanderConnectionString)
                    dbmCore = New DBCoreDataBaseManager(_plugin.Manager.DesktopGlobal.ConnectionStrings.Core)
                    dbmImaging = New DBImagingDataBaseManager(_plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)
                    dbmIntegration.Connection_Open(_plugin.Manager.Sesion.Usuario.id)
                    dbmCore.Connection_Open(_plugin.Manager.Sesion.Usuario.id)
                    dbmImaging.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                    Dim ImagenesDataTable = dbmIntegration.SchemaProcess.PA_Get_Imagenes.DBExecute(_plugin.Manager.ImagingGlobal.Entidad, _plugin.Manager.ImagingGlobal.Proyecto)
                    Dim FormatoDataTable = dbmIntegration.SchemaConfig.TBL_Formato.DBGet(_plugin.Manager.ImagingGlobal.Entidad, _plugin.Manager.ImagingGlobal.Proyecto, Nothing)

                    Dim ExpedientesDataTable = dbmIntegration.SchemaSantander.CTA_Expediente_Folder_File.DBFindByid_OTid_Santander_DocumentoSede_Impresion(CInt(OTDesktopComboBox.SelectedValue), 12, CStr(_plugin.Manager.DesktopGlobal.PuestoTrabajoRow.fk_Sede))
                    If _plugin.Manager.DesktopGlobal.PuestoTrabajoRow.fk_Sede = 3 Then
                        dbmIntegration.SchemaSantander.CTA_Expediente_Folder_File.DBFillByid_OTid_Santander_DocumentoSede_Impresion(ExpedientesDataTable, CInt(OTDesktopComboBox.SelectedValue), 12, "0")
                    End If

                    Dim ListadoCBarras As New CTA_Listado_CBarras_DistribucionDataTable()
                    Dim ListadoCBarrasNo As New CTA_Listado_CBarras_DistribucionDataTable()

                    Dim CTAImagenesDataSource = New ReportDataSource("CTA_ImagenesDataSet", ImagenesDataTable)

                    For Each ExpedientesRow In ExpedientesDataTable

                        Dim CBarrasRow = ListadoCBarras.NewCTA_Listado_CBarras_DistribucionRow()

                        For Each tblFormatoRow As TBL_FormatoRow In FormatoDataTable

                            If CInt(ExpedientesRow.Valor_File_Data) = tblFormatoRow.Id_Formato Then

                                Dim CartaFormateadaRow = tblFormatoRow.ToTBL_FormatoSimpleType

                                'Formateo de Partes
                                Dim FormatoParametrosDataTable = dbmIntegration.SchemaProcess.PA_Formato_Parametros.DBExecute(ExpedientesRow.fk_Expediente, ExpedientesRow.fk_Folder, 1, CInt(ExpedientesRow.Valor_File_Data))

                                CBarrasRow.Numero_Unico = ""

                                For Each tblFormatoParametrosRow In FormatoParametrosDataTable

                                    Select Case tblFormatoParametrosRow.fk_parametro_tipo
                                        Case 1
                                            CartaFormateadaRow.Destinatario = CartaFormateadaRow.Destinatario.Replace(tblFormatoParametrosRow.parametro, tblFormatoParametrosRow.Valor_Parametro)
                                            If tblFormatoParametrosRow.Es_Numero_Unico Then
                                                CBarrasRow.Numero_Unico = tblFormatoParametrosRow.Valor_Parametro.Replace("<b>", "").Replace("</b>", "")
                                            End If
                                        Case 2
                                            CartaFormateadaRow.Asunto = CartaFormateadaRow.Asunto.Replace(tblFormatoParametrosRow.parametro, tblFormatoParametrosRow.Valor_Parametro)
                                            If tblFormatoParametrosRow.Es_Numero_Unico Then
                                                CBarrasRow.Numero_Unico = tblFormatoParametrosRow.Valor_Parametro.Replace("<b>", "").Replace("</b>", "")
                                            End If
                                        Case 3
                                            CartaFormateadaRow.Cuerpo = CartaFormateadaRow.Cuerpo.Replace(tblFormatoParametrosRow.parametro, tblFormatoParametrosRow.Valor_Parametro)
                                            If tblFormatoParametrosRow.Es_Numero_Unico Then
                                                CBarrasRow.Numero_Unico = tblFormatoParametrosRow.Valor_Parametro.Replace("<b>", "").Replace("</b>", "")
                                            End If
                                        Case 4
                                            CartaFormateadaRow.Firma = CartaFormateadaRow.Firma.Replace(tblFormatoParametrosRow.parametro, tblFormatoParametrosRow.Valor_Parametro)
                                            If tblFormatoParametrosRow.Es_Numero_Unico Then
                                                CBarrasRow.Numero_Unico = tblFormatoParametrosRow.Valor_Parametro.Replace("<b>", "").Replace("</b>", "")
                                            End If
                                    End Select

                                Next

                                'Dim FormatoDT = Utilities.ClonarDataTable(FormatoDataTable)
                                Dim FormatoDT = FormatoDataTable.Clone

                                Dim Fdtr As DataRow = FormatoDT.NewRow
                                Fdtr("Fk_Entidad") = CartaFormateadaRow.Fk_Entidad
                                Fdtr("Fk_Proyecto") = CartaFormateadaRow.Fk_Proyecto
                                Fdtr("Id_Formato") = CartaFormateadaRow.Id_Formato
                                Fdtr("Nombre_Formato") = CartaFormateadaRow.Nombre_Formato
                                Fdtr("fk_Ente_Coactivo") = CartaFormateadaRow.fk_Ente_Coactivo
                                Fdtr("Descripción") = CartaFormateadaRow.Descripción
                                Fdtr("Destinatario") = CartaFormateadaRow.Destinatario
                                Fdtr("Asunto") = CartaFormateadaRow.Asunto
                                Fdtr("Cuerpo") = CartaFormateadaRow.Cuerpo
                                Fdtr("Firma") = CartaFormateadaRow.Firma

                                'FormatoDT.Rows.Add(CartaFormateadaRow)
                                FormatoDT.Rows.Add(Fdtr)

                                Dim CodigoBarrasDataTable As New CTA_Imagen_CBarras_ReportDataTable()

                                Dim CodigoBarrasDataRow = CodigoBarrasDataTable.NewCTA_Imagen_CBarras_ReportRow()

                                With CodigoBarrasDataRow
                                    Dim CodigoBarras As New BarCodeControl()

                                    CodigoBarras.BarCode = ExpedientesRow.CBarras_File
                                    CodigoBarras.BarCodeHeight = 50
                                    CodigoBarras.BarCodeType = BarCodeTypeType.EAN128
                                    CodigoBarras.Align = AlignType.Center
                                    CodigoBarras.Width = 200
                                    CodigoBarras.Height = 40
                                    CodigoBarras.Weight = BarCodeWeight.Small
                                    CodigoBarras.ShowHeader = False
                                    CodigoBarras.FooterLinesClear()
                                    CodigoBarras.Update()

                                    .ImagenCbarras = CodigoBarras.GetImage(ImageFormat.Jpeg)
                                    CBarrasRow.ImagenCBarras = CodigoBarras.GetImage(ImageFormat.Jpeg)
                                End With

                                CodigoBarrasDataTable.AddCTA_Imagen_CBarras_ReportRow(CodigoBarrasDataRow)

                                Dim TBLFormatoDataSource = New ReportDataSource("TBL_FormatoDataSet", FormatoDT)
                                Dim CodigoBarrasDataSource = New ReportDataSource("ImagenCBarrasDataSet", CType(CodigoBarrasDataTable, DataTable))

                                ReportViewer1.LocalReport.ReportEmbeddedResource = "Santander.Plugin.Report_GenerarCartas.rdlc"
                                ReportViewer1.LocalReport.DataSources.Clear()
                                ReportViewer1.LocalReport.DataSources.Add(CTAImagenesDataSource)
                                ReportViewer1.LocalReport.DataSources.Add(TBLFormatoDataSource)
                                ReportViewer1.LocalReport.DataSources.Add(CodigoBarrasDataSource)
                                Dim Parametros = New ReportParameter()
                                Parametros = New ReportParameter("Cbarras", ExpedientesRow.CBarras_File)
                                ReportViewer1.LocalReport.SetParameters(Parametros)

                                ReportViewer1.RefreshReport()

                                Dim PDFMemoryStream = New MemoryStream(ReportViewer1.LocalReport.Render("PDF"))

                                Dim Prefijo As String

                                If ExpedientesRow.Sede_Impresion = 0 Then
                                    Prefijo = "\No_Cubrimiento - "
                                    Dim Listad = ListadoCBarrasNo.NewCTA_Listado_CBarras_DistribucionRow()

                                    If Not String.IsNullOrEmpty(CBarrasRow.Numero_Unico) Then
                                        Listad.Numero_Unico = CBarrasRow.Numero_Unico
                                    Else
                                        Listad.Numero_Unico = "N/A"
                                    End If
                                    Listad.CBarras_File = ExpedientesRow.CBarras_File
                                    Listad.ImagenCBarras = CBarrasRow.ImagenCBarras
                                    ListadoCBarrasNo.AddCTA_Listado_CBarras_DistribucionRow(Listad)
                                Else
                                    Dim Listad = ListadoCBarras.NewCTA_Listado_CBarras_DistribucionRow()

                                    If Not String.IsNullOrEmpty(CBarrasRow.Numero_Unico) Then
                                        Listad.Numero_Unico = CBarrasRow.Numero_Unico
                                    Else
                                        Listad.Numero_Unico = "N/A"
                                    End If
                                    Listad.CBarras_File = ExpedientesRow.CBarras_File
                                    Listad.ImagenCBarras = CBarrasRow.ImagenCBarras
                                    Prefijo = "\"
                                    ListadoCBarras.AddCTA_Listado_CBarras_DistribucionRow(Listad)
                                End If

                                Using File = New FileStream(RutaTextBox.Text & Prefijo & ExpedientesRow.CBarras_File & ".pdf", FileMode.Append, FileAccess.Write)
                                    PDFMemoryStream.WriteTo(File)
                                    PDFMemoryStream.Close()
                                End Using

                                Utilities.ActualizaEstadoFileImaging(dbmImaging, dbmCore, ExpedientesRow.CBarras_File, DesktopConfig.Modulo.Imaging, 42, _plugin.Manager.Sesion.Usuario.id)

                            End If

                        Next

                    Next

                    Dim CodigoBarrasListadoDataSource = New ReportDataSource("ImagenCBarrasDataSet", CType(ListadoCBarras, DataTable))

                    ReportViewer1.Clear()
                    ReportViewer1.LocalReport.ReportEmbeddedResource = "Santander.Plugin.Report_DistribucionCartas.rdlc"
                    ReportViewer1.LocalReport.DataSources.Clear()
                    ReportViewer1.LocalReport.DataSources.Add(CTAImagenesDataSource)
                    ReportViewer1.LocalReport.DataSources.Add(CodigoBarrasListadoDataSource)
                    ReportViewer1.RefreshReport()

                    Dim PDFMemoryStreamListado = New MemoryStream(ReportViewer1.LocalReport.Render("PDF"))

                    Using File = New FileStream(RutaTextBox.Text & "\Listado Cartas Impresas.pdf", FileMode.Append, FileAccess.Write)
                        PDFMemoryStreamListado.WriteTo(File)
                        PDFMemoryStreamListado.Close()
                    End Using

                    Dim CodigoBarrasListadoNoDataSource = New ReportDataSource("ImagenCBarrasDataSet", CType(ListadoCBarrasNo, DataTable))

                    ReportViewer1.Clear()
                    ReportViewer1.LocalReport.ReportEmbeddedResource = "Santander.Plugin.Report_DistribucionCartasNo.rdlc"
                    ReportViewer1.LocalReport.DataSources.Clear()
                    ReportViewer1.LocalReport.DataSources.Add(CTAImagenesDataSource)
                    ReportViewer1.LocalReport.DataSources.Add(CodigoBarrasListadoNoDataSource)
                    ReportViewer1.RefreshReport()

                    Dim PDFMemoryStreamListadoNo = New MemoryStream(ReportViewer1.LocalReport.Render("PDF"))

                    Using File = New FileStream(RutaTextBox.Text & "\Listado Cartas Impresas No Cubrimiento.pdf", FileMode.Append, FileAccess.Write)
                        PDFMemoryStreamListadoNo.WriteTo(File)
                        PDFMemoryStreamListadoNo.Close()
                    End Using

                    MessageBox.Show("Cartas generadas correctamente", "GenerarCartas", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.RutaTextBox.Text = ""

                End If
            Catch ex As Exception
                MessageBox.Show("Error : " & ex.Message, "GenerarCartas", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If dbmIntegration IsNot Nothing Then dbmIntegration.Connection_Close()
                If dbmCore IsNot Nothing Then dbmCore.Connection_Close()
                If dbmImaging IsNot Nothing Then dbmImaging.Connection_Close()
            End Try
        End Sub

        Private Sub FormGenerarCartas_Load(sender As Object, e As EventArgs) Handles MyBase.Load

            ReportViewer1.RefreshReport()

            CargarOT()

        End Sub

        Private Sub CargarOT()
            OTDesktopComboBox.DataSource = Nothing
            OTDesktopComboBox.SelectedIndex = -1


            Dim dbmImaging As DBImagingDataBaseManager = Nothing

            Try
                dbmImaging = New DBImagingDataBaseManager(_plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                Dim OTDataTable = dbmImaging.SchemaProcess.TBL_OT.DBFindByfk_Entidadfk_Proyectofk_fecha_procesoCerradofk_Entidad_Procesamiento(_plugin.Manager.ImagingGlobal.ProyectoImagingRow.fk_Entidad, _plugin.Manager.ImagingGlobal.ProyectoImagingRow.fk_Proyecto, CInt(FechaProcesoPicker.Value.ToString("yyyyMMdd")), True, _plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad)

                If OTDataTable.Count > 0 Then
                    For Each OTRow In OTDataTable
                        Utilities.LlenarCombo(OTDesktopComboBox, OTDataTable, OTDataTable.id_OTColumn.ColumnName, OTDataTable.id_OTColumn.ColumnName)
                    Next
                End If

            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try



        End Sub

        Private Sub FechaProcesoPicker_ValueChanged(sender As Object, e As EventArgs) Handles FechaProcesoPicker.ValueChanged
            CargarOT()
        End Sub

#Region " Funciones "

        Private Function Validar() As Boolean
            If (RutaTextBox.Text = "") Then
                DesktopMessageBoxControl.DesktopMessageShow("Debe seleccionar un directorio válido", "Directorio inválido", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                RutaTextBox.Focus()

            ElseIf (Not Directory.Exists(Me.RutaTextBox.Text)) Then
                DesktopMessageBoxControl.DesktopMessageShow("Debe seleccionar un directorio válido", "Directorio inválido", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                RutaTextBox.Focus()
                RutaTextBox.SelectAll()
            ElseIf OTDesktopComboBox.SelectedIndex = -1 Then
                Throw New Exception("No hay OT's para la fecha de proceso seleccionada")
            Else
                Return True
            End If

            Return False
        End Function
#End Region

        Private Sub GenerarButton_Click(sender As Object, e As EventArgs) Handles GenerarButton.Click
            GenerarCartas()
        End Sub

        Private Sub SelectFolderButton_Click(sender As Object, e As EventArgs) Handles SelectFolderButton.Click
            SelectFolderPath()
        End Sub

        Private Sub SelectFolderPath()
            Dim LectorFolderBrowserDialog = New FolderBrowserDialog()
            Dim Respuesta As DialogResult

            LectorFolderBrowserDialog.SelectedPath = RutaTextBox.Text
            LectorFolderBrowserDialog.ShowNewFolderButton = False
            LectorFolderBrowserDialog.Description = "Seleccione la carpeta"

            Respuesta = LectorFolderBrowserDialog.ShowDialog()

            If (Respuesta = DialogResult.OK) Then
                RutaTextBox.Text = LectorFolderBrowserDialog.SelectedPath
            End If
        End Sub
    End Class
End Namespace