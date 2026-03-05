Imports Citibank.Plugin.Imaging.Embargos
Imports DBImaging
Imports Miharu.Desktop.Library.Config
Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports System.IO
Imports Microsoft.Reporting.WinForms
Imports DBIntegration
Imports DBCore
Imports DBIntegration.SchemaSantander
Imports DBIntegration.SchemaConfig
Imports Miharu.Desktop.Controls.BarCode
Imports System.Drawing.Imaging
Imports Slyg.Tools
Imports Slyg.Tools.Imaging
Imports Miharu.FileProvider.Library

Namespace Embargos.Forms
    Public Class FormGenerarCartas
        Private _plugin As EmbargosImagingPlugin

        Public Sub New(nPlugin As EmbargosImagingPlugin)
            _plugin = nPlugin

            InitializeComponent()
        End Sub

#Region " Metodos "
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

        Private Sub GenerarCartas()
            Dim dbmIntegration As DBIntegrationDataBaseManager = Nothing
            Dim dbmCore As DBCoreDataBaseManager = Nothing
            Dim dbmImaging As DBImagingDataBaseManager = Nothing
            Dim manager As FileProviderManager = Nothing

            Try
                If Validar() Then
                    dbmIntegration = New DBIntegrationDataBaseManager(_plugin.IntegrationConnectionString)
                    dbmCore = New DBCoreDataBaseManager(_plugin.Manager.DesktopGlobal.ConnectionStrings.Core)
                    dbmImaging = New DBImagingDataBaseManager(_plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)
                    dbmIntegration.Connection_Open(_plugin.Manager.Sesion.Usuario.id)
                    dbmCore.Connection_Open(_plugin.Manager.Sesion.Usuario.id)
                    dbmImaging.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                    Dim ImagenesDataTable = dbmIntegration.SchemaProcess.PA_Get_Imagenes.DBExecute(_plugin.Manager.ImagingGlobal.Entidad, _plugin.Manager.ImagingGlobal.Proyecto)
                    Dim FormatoDataTable = dbmIntegration.SchemaConfig.TBL_Formato.DBGet(_plugin.Manager.ImagingGlobal.Entidad, _plugin.Manager.ImagingGlobal.Proyecto, Nothing)

                    Dim ExpedientesDataTable = dbmIntegration.SchemaCITIBANK.PA_Get_Expedientes_Carta_Respueta.DBExecute(CInt(OTDesktopComboBox.SelectedValue), _plugin.Manager.ImagingGlobal.Entidad, _plugin.Manager.ImagingGlobal.Proyecto, CStr(_plugin.Manager.DesktopGlobal.PuestoTrabajoRow.fk_Sede))

                    If ExpedientesDataTable.Rows.Count > 0 Then

                        Dim ListadoCBarras As New CTA_Listado_CBarras_DistribucionDataTable()
                        Dim ListadoCBarrasNo As New CTA_Listado_CBarras_DistribucionDataTable()

                        Dim CTAImagenesDataSource = New ReportDataSource("CTA_ImagenesDataSet", ImagenesDataTable)

                        Dim formato = Utilities.GetEnumFormat(_plugin.Manager.ImagingGlobal.ProyectoImagingRow.id_Formato_Imagen_Entrada)
                        Dim compresion = Utilities.GetEnumCompression(CType(_plugin.Manager.ImagingGlobal.ProyectoImagingRow.id_Formato_Imagen_Salida, DesktopConfig.FormatoImagenEnum))
                        Const MaxThumbnailWidth As Integer = 60
                        Const MaxThumbnailHeight As Integer = 80

                        Dim servidor = dbmImaging.SchemaCore.CTA_Servidor.DBFindByfk_Entidadid_Servidor(_plugin.Manager.DesktopGlobal.ServidorImagenRow.fk_Entidad, _plugin.Manager.DesktopGlobal.ServidorImagenRow.id_Servidor)(0).ToCTA_ServidorSimpleType
                        Dim centro = dbmImaging.SchemaSecurity.CTA_Centro_Procesamiento.DBFindByfk_Entidadfk_Sedeid_Centro_Procesamiento(_plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, _plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Sede, _plugin.Manager.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento)(0).ToCTA_Centro_ProcesamientoSimpleType()

                        manager = New FileProviderManager(servidor, centro, dbmImaging, _plugin.Manager.Sesion.Usuario.id)
                        manager.Connect()

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

                                    Dim cultures As System.Globalization.CultureInfo = System.Globalization.CultureInfo.CreateSpecificCulture("es-ES")
                                    Dim FechaGeneracion As String = Date.Now.ToString("D", cultures)

                                    CartaFormateadaRow.Destinatario = CartaFormateadaRow.Destinatario.Replace("@FechaGeneracion", FechaGeneracion)

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

                                        .ImagenCBarras = CodigoBarras.GetImage(ImageFormat.Jpeg)
                                        CBarrasRow.ImagenCBarras = CodigoBarras.GetImage(ImageFormat.Jpeg)
                                    End With

                                    CodigoBarrasDataTable.AddCTA_Imagen_CBarras_ReportRow(CodigoBarrasDataRow)

                                    Dim TBLFormatoDataSource = New ReportDataSource("TBL_FormatoDataSet", FormatoDT)
                                    Dim CodigoBarrasDataSource = New ReportDataSource("ImagenCBarrasDataSet", CType(CodigoBarrasDataTable, DataTable))

                                    ReportViewer1.LocalReport.ReportEmbeddedResource = "Citibank.Plugin.Report_GenerarCartas.rdlc"
                                    ReportViewer1.LocalReport.DataSources.Clear()
                                    ReportViewer1.LocalReport.DataSources.Add(CTAImagenesDataSource)
                                    ReportViewer1.LocalReport.DataSources.Add(TBLFormatoDataSource)
                                    ReportViewer1.LocalReport.DataSources.Add(CodigoBarrasDataSource)
                                    Dim Parametros = New ReportParameter()
                                    Parametros = New ReportParameter("Cbarras", ExpedientesRow.CBarras_File)
                                    ReportViewer1.LocalReport.SetParameters(Parametros)

                                    ReportViewer1.RefreshReport()


                                    Dim FileName As String = ""
                                    'Dim Imagen() As Byte = Nothing
                                    'Dim Thumbnail() As Byte = Nothing
                                    Dim pdfContent() As Byte = Nothing
                                    'Dim identificador = Guid.NewGuid()
                                    Dim identificador = Guid.Parse(ExpedientesRow.File_Unique_Identifier)

                                    'FileName = Program.AppPath & Program.TempPath & identificador.ToString() & ".tiff"
                                    'Imagen = ReportViewer1.LocalReport.Render("Image")
                                    FileName = Program.AppPath & Program.TempPath & identificador.ToString() & ".pdf"
                                    pdfContent = ReportViewer1.LocalReport.Render("PDF")

                                    Using fs = New FileStream(FileName, FileMode.Create)
                                        'fs.Write(Imagen, 0, Imagen.Length)
                                        fs.Write(pdfContent, 0, pdfContent.Length)
                                        fs.Close()
                                    End Using

                                    Dim leerFolio As Boolean = True
                                    Dim Folios As Integer = 1
                                    Dim FolioBitmap = ImageManager.GetFolioBitmap(FileName, Folios)

                                    dbmCore.Transaction_Begin()
                                    manager.TransactionBegin()

                                    For folio As Short = 1 To Folios
                                        Dim dataImage = ImageManager.GetFolioData(FolioBitmap, folio, 1, formato, compresion)
                                        Dim dataImageThumbnail = ImageManager.GetThumbnailData(FileName, folio, folio, MaxThumbnailWidth, MaxThumbnailHeight)

                                        If Folios = 1 Then
                                            manager.CreateItem(ExpedientesRow.fk_Expediente, ExpedientesRow.fk_Folder, ExpedientesRow.id_File, 1, _plugin.Manager.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida, identificador)

                                            Dim fileImgType As New DBCore.SchemaImaging.TBL_FileType()
                                            fileImgType.fk_Expediente = ExpedientesRow.fk_Expediente
                                            fileImgType.fk_Folder = ExpedientesRow.fk_Folder
                                            fileImgType.fk_File = ExpedientesRow.id_File
                                            fileImgType.id_Version = 1
                                            fileImgType.File_Unique_Identifier = identificador
                                            fileImgType.Folios_Documento_File = Folios
                                            fileImgType.Tamaño_Imagen_File = 0
                                            fileImgType.Nombre_Imagen_File = ""
                                            fileImgType.Key_Cargue_Item = ""
                                            fileImgType.Save_FileName = ""
                                            fileImgType.fk_Content_Type = _plugin.Manager.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida
                                            fileImgType.fk_Usuario_Log = _plugin.Manager.Sesion.Usuario.id
                                            fileImgType.Validaciones_Opcionales = False
                                            fileImgType.Es_Anexo = False
                                            fileImgType.fk_Anexo = Nothing
                                            fileImgType.fk_Entidad_Servidor = _plugin.Manager.DesktopGlobal.ServidorImagenRow.fk_Entidad
                                            fileImgType.fk_Servidor = _plugin.Manager.DesktopGlobal.ServidorImagenRow.id_Servidor
                                            fileImgType.Fecha_Creacion = SlygNullable.SysDate
                                            fileImgType.Fecha_Transferencia = Nothing
                                            fileImgType.En_Transferencia = False
                                            fileImgType.fk_Entidad_Servidor_Transferencia = Nothing
                                            fileImgType.fk_Servidor_Transferencia = Nothing
                                            dbmCore.SchemaImaging.TBL_File.DBInsert(fileImgType)
                                        End If

                                        manager.CreateFolio(ExpedientesRow.fk_Expediente, ExpedientesRow.fk_Folder, ExpedientesRow.id_File, 1, folio, dataImage, dataImageThumbnail(0), False)
                                    Next
                                    FolioBitmap.Dispose()

                                    dbmCore.Transaction_Commit()
                                    manager.TransactionCommit()

                                    Utilities.ActualizaEstadoFileImaging(dbmImaging, dbmCore, ExpedientesRow.CBarras_File, DesktopConfig.Modulo.Imaging, 42, _plugin.Manager.Sesion.Usuario.id)
                                End If
                            Next
                        Next

                        MessageBox.Show("Cartas generadas correctamente", "GenerarCartas", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        MessageBox.Show("No hay cartas para generar", "GenerarCartas", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                End If
            Catch ex As Exception
                MessageBox.Show("Error : " & ex.Message, "GenerarCartas", MessageBoxButtons.OK, MessageBoxIcon.Error)
                If (dbmCore IsNot Nothing) Then dbmCore.Transaction_Rollback()
                If (manager IsNot Nothing) Then manager.TransactionRollback()
            Finally
                If dbmIntegration IsNot Nothing Then dbmIntegration.Connection_Close()
                If dbmCore IsNot Nothing Then dbmCore.Connection_Close()
                If dbmImaging IsNot Nothing Then dbmImaging.Connection_Close()
                If manager IsNot Nothing Then manager.Disconnect()
            End Try
        End Sub
#End Region

#Region " Funciones "
        Private Function Validar() As Boolean
            If OTDesktopComboBox.SelectedIndex = -1 Then
                Throw New Exception("No hay OT's para la fecha de proceso seleccionada")
            Else
                Return True
            End If

            Return False
        End Function
#End Region

#Region " Eventos "
        Private Sub FormGenerarCartas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            ReportViewer1.RefreshReport()
            CargarOT()
        End Sub

        Private Sub FechaProcesoPicker_ValueChanged(sender As Object, e As EventArgs) Handles FechaProcesoPicker.ValueChanged
            CargarOT()
        End Sub

        Private Sub GenerarButton_Click(sender As Object, e As EventArgs) Handles GenerarButton.Click
            GenerarCartas()
        End Sub
#End Region

    End Class
End Namespace
