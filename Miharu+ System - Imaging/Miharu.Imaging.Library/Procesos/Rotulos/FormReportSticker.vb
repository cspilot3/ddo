Imports System.Windows.Forms
Imports Microsoft.Reporting.WinForms
Imports Miharu.Desktop.Controls.DesktopReportViewer
Imports Miharu.Desktop.Controls
Imports Miharu.Desktop.Library.Config
Imports System.Reflection
Imports DMB = Miharu.Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl
Imports System.IO
Imports BarcodeLib
Imports System.Xml
Imports System.Drawing
Imports System.Data.SqlClient
Public Class FormReportSticker
    Inherits DesktopReport1
#Region " Propiedades "
    Friend Shared DesktopGlobal As New DesktopGlobal()
    Public Overrides ReadOnly Property ReportName As String
        Get
            Return "Formato Sticker"
        End Get
    End Property
#End Region
#Region " Constructores "
    Public Sub New(ByRef nReportViewer As DesktopReportViewer1Control)
        MyBase.New(nReportViewer)
    End Sub
    Friend Shared ReadOnly Property AssemblyName() As String
        Get
            Return [Assembly].GetExecutingAssembly().GetName().Name
        End Get
    End Property
#End Region
#Region " Metodos "
    Public Overrides Sub Launch(Caja As String, OT As Integer, RotuloCarpetas As Boolean, Reportegenericofuid As Boolean, TipoFuid As Integer, TipoGestion As String)
        Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
        Dim stringGestion As String = Nothing
        Dim idFolder, idFile, idFolio, id_Version, idDocumento As Short
        Dim idExpediente As Long
        Dim dt As New DataTable
        Try
            dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
            dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

            ' CONSULTA TABLA PARAMETRICA PARA GENERAR STICKER
            Dim _documentosStickerDataTable As DBImaging.SchemaConfig.TBL_Documento_StickerDataTable
            _documentosStickerDataTable = dbmImaging.SchemaConfig.TBL_Documento_Sticker.DBFindByfk_Entidadfk_Proyectogenera_Sticker_Fisicofk_Documento(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, True, Nothing)

            If _documentosStickerDataTable.Rows.Count > 0 Then
                'CONSULTA PARAMETROS EN FORMATO XML
                Dim docXML As XmlDocument = New XmlDocument()
                docXML.LoadXml(_documentosStickerDataTable(0).parametro_Reporte)
                Dim nodes As XmlNodeList = docXML.SelectNodes("/Configuracion/Sticker")

                With dt
                    .Columns.Add(New DataColumn("PosicionX", GetType(String)))
                    .Columns.Add(New DataColumn("PosicionY", GetType(String)))
                    .Columns.Add(New DataColumn("CodigoBarras", GetType(String)))
                    .Columns.Add(New DataColumn("NombreParametrosPrincipal", GetType(String)))
                    .Columns.Add(New DataColumn("ValorParametrosPrincipal", GetType(String)))
                    .Columns.Add(New DataColumn("NombreParametrosSticker", GetType(String)))
                    .Columns.Add(New DataColumn("ValorParametrosSticker", GetType(String)))
                End With

                For Each n1 As XmlNode In docXML.DocumentElement.ChildNodes
                    If n1.HasChildNodes Then
                        Dim row As DataRow
                        row = dt.NewRow
                        For Each n2 As XmlNode In n1.ChildNodes
                            For Each n3 As XmlNode In n2.ChildNodes
                                row(n2.Name) = n3.InnerText
                            Next
                        Next
                        dt.Rows.Add(row)
                    End If
                Next
            End If

            Dim resultadoDataTable = DatosParametros(dbmImaging,
                                                     _documentosStickerDataTable(0).sqlPrincipal,
                                                     dt.Rows(0).Item("NombreParametrosPrincipal").ToString(),
                                                   dt.Rows(0).Item("ValorParametrosPrincipal").ToString(),
                                                   Caja, Nothing, Nothing, Nothing, Nothing)
            If resultadoDataTable.Rows.Count > 0 Then
                Try
                    For Each ItemCrear As DataRow In resultadoDataTable.Rows
                        Try
                            If ItemCrear.Item("fk_Expediente").ToString() <> "" Then
                                idExpediente = CLng(ItemCrear.Item("fk_Expediente"))
                            Else
                                idExpediente = 0
                            End If
                        Catch ex As Exception
                            idExpediente = 0
                        End Try
                        Try
                            If ItemCrear.Item("fk_Folder").ToString() <> "" Then
                                idFolder = CShort(ItemCrear.Item("fk_Folder"))
                            Else
                                idFolder = 0
                            End If
                        Catch ex As Exception
                            idFolder = 0
                        End Try
                        Try
                            If ItemCrear.Item("fk_File").ToString() <> "" Then
                                idFile = CShort(ItemCrear.Item("fk_File"))
                            Else
                                idFile = 0
                            End If
                        Catch ex As Exception
                            idFile = 0
                        End Try
                        Try
                            If ItemCrear.Item("id_File_Record_Folio").ToString() <> "" Then
                                idFolio = CShort(CInt(ItemCrear.Item("id_File_Record_Folio")))
                            Else
                                idFolio = 0
                            End If
                        Catch ex As Exception
                            idFolio = 0
                        End Try
                        Try
                            If ItemCrear.Item("fk_Version").ToString() <> "" Then
                                id_Version = CShort(ItemCrear.Item("fk_Version"))
                            Else
                                id_Version = 0
                            End If
                        Catch ex As Exception
                            id_Version = 0
                        End Try
                        Try
                            If ItemCrear.Item("id_Documento").ToString() <> "" Then
                                idDocumento = CShort(ItemCrear.Item("id_Documento"))
                            Else
                                idDocumento = 0
                            End If
                        Catch ex As Exception
                            idDocumento = 0
                        End Try

                        GenerarSticker(dbmImaging,
                                       _documentosStickerDataTable,
                                       dt,
                                       CShort(Program.ImagingGlobal.Entidad),
                                       CShort(Program.ImagingGlobal.Proyecto),
                                       Caja,
                                       idDocumento,
                                       idExpediente,
                                       idFolder,
                                       idFile)

                    Next
                Catch ex As Exception
                End Try
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
        End Try
    End Sub
    Public Overrides Sub Launch(Carpeta As String)
    End Sub
    Public Overrides Sub Launch(datatableDestinatario As DataTable, solicitudSeleccionada As Integer, nombres As String, direccion As String, sede As String, precinto As String)
    End Sub
    Public Overrides Sub Launch(FechaRecaudo As Integer)
    End Sub
    Private Sub GenerarSticker(ByVal dbmImaging As DBImaging.DBImagingDataBaseManager,
                                    ByVal _documentosStickerDataTable As DBImaging.SchemaConfig.TBL_Documento_StickerDataTable,
                                    ByVal dt As DataTable,
                                    ByVal nEntidad As Short,
                                    ByVal nProyecto As Short,
                                    ByVal nCaja As String,
                                    ByVal nDocumento As Long,
                                    ByVal nExpediente As Long,
                                    ByVal nFolder As Short,
                                    ByVal nFile As Short)
        Dim Extension_Formato_Imagen_Salida As String = String.Empty
        Dim ImagenSticker As Image = Nothing
        Dim Datos = DatosParametros(dbmImaging,
                               _documentosStickerDataTable(0).sqlSticker,
                               dt.Rows(0).Item("NombreParametrosSticker").ToString(),
                               dt.Rows(0).Item("ValorParametrosSticker").ToString(), nCaja, nDocumento, nExpediente, nFolder, nFile)
        Datos.Columns.Add("IMAGEN", GetType(Byte()))
        Dim barcodeImage As BarcodeLib.Barcode = New BarcodeLib.Barcode()
        barcodeImage.IncludeLabel = False
        For Each item As DataRow In Datos.Rows
            Try
                Dim img As Byte() = Nothing
                Select Case dt.Rows(0).Item("CodigoBarras").ToString()
                    Case "CODE39Extended"
                        img = ImageToByteArray(barcodeImage.Encode(BarcodeLib.TYPE.CODE39Extended, item("CodigoBarras").ToString(), Color.Black, Color.White, 200, 25))
                    Case "CODE39"
                        img = ImageToByteArray(barcodeImage.Encode(BarcodeLib.TYPE.CODE39, item("CodigoBarras").ToString(), Color.Black, Color.White, 200, 25))
                    Case "CODE128"
                        img = ImageToByteArray(barcodeImage.Encode(BarcodeLib.TYPE.CODE128, item("CodigoBarras").ToString(), Color.Black, Color.White, 200, 25))
                    Case "EAN13"
                        img = ImageToByteArray(barcodeImage.Encode(BarcodeLib.TYPE.EAN13, item("CodigoBarras").ToString(), Color.Black, Color.White, 200, 25))
                End Select
                item("IMAGEN") = img
            Catch ex As Exception
            End Try
        Next
        Datos.AcceptChanges()
        If Datos.Rows.Count > 0 Then
            Dim InformeReportDataSource1 As New ReportDataSource("DataSet1", Datos)
            Me.ReportViewer.MainReportViewer1.Reset()
            Me.ReportViewer.MainReportViewer1.LocalReport.ReportEmbeddedResource = "Miharu.Imaging.Library." & _documentosStickerDataTable(0).nombre_RDLC & ".rdlc"
            Me.ReportViewer.MainReportViewer1.LocalReport.DataSources.Clear()
            Me.ReportViewer.MainReportViewer1.LocalReport.DataSources.Add(InformeReportDataSource1)
            Me.ReportViewer.MainReportViewer1.SetDisplayMode(DisplayMode.PrintLayout)
            Me.ReportViewer.MainReportViewer1.RefreshReport()
        End If
    End Sub
    Private Function DatosParametros(ByVal dbmImaging As DBImaging.DBImagingDataBaseManager,
                                  ByVal sqlquery As String,
                                  ByVal nombreParametros As String,
                                  ByVal valorParametros As String,
                                  ByVal nCaja As String,
                                  ByVal nDocumento As Long,
                                  ByVal nExpediente As Long,
                                  ByVal nFolder As Short,
                                  ByVal nFile As Short) As DataTable
        Dim dt As DataTable = Nothing
        Try
            Dim parametros As String()
            Dim valores As String()
            Dim SqlParameter() As SqlParameter
            parametros = nombreParametros.Split(","c)
            valores = valorParametros.Split(","c)
            ReDim SqlParameter(parametros.Length - 1)
            For i As Integer = 0 To UBound(parametros, 1)
                Select Case parametros(i)
                    Case "fkCaja"
                        SqlParameter(i) = New SqlParameter("@" + parametros(i) + "", nCaja)
                    Case "fkExpediente"
                        SqlParameter(i) = New SqlParameter("@" + parametros(i) + "", nExpediente)
                    Case "fkFolder"
                        SqlParameter(i) = New SqlParameter("@" + parametros(i) + "", nFolder)
                    Case "fkFile"
                        SqlParameter(i) = New SqlParameter("@" + parametros(i) + "", nFile)
                    Case "fkDocumento"
                        SqlParameter(i) = New SqlParameter("@" + parametros(i) + "", nDocumento)
                    Case Else
                        SqlParameter(i) = New SqlParameter("@" + parametros(i) + "", valores(i))
                End Select
            Next i
            dt = ExecuteQuery(sqlquery, dbmImaging, SqlParameter)
            Return dt
        Catch ex As Exception
        End Try
        Return dt
    End Function
    Private Function ExecuteQuery(ByVal s As String,
                                  ByVal dbmImaging As DBImaging.DBImagingDataBaseManager,
                                  ByVal ParamArray params() As SqlParameter) As DataTable
        Dim dt As DataTable = Nothing
        Using da As New System.Data.SqlClient.SqlDataAdapter(s, dbmImaging.DataBase.ConnectionString)
            Try
                dt = New DataTable
                If params.Length > 0 Then
                    da.SelectCommand.Parameters.AddRange(params)
                End If
                da.SelectCommand.CommandTimeout = 86400
                da.Fill(dt)
                Return dt
            Catch ex As SqlException
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
            Return dt
        End Using
    End Function
    Public Shared Function ImageToByteArray(ByVal Image As System.Drawing.Image) As Byte()
        Using Ms = New MemoryStream()
            Image.Save(Ms, System.Drawing.Imaging.ImageFormat.Png)
            Return Ms.ToArray()
        End Using
    End Function
    Public Shared Function ByteArrayToImage(ByVal Bit As Byte()) As System.Drawing.Image
        Using Ms = New MemoryStream(Bit)
            Return Image.FromStream(Ms)
        End Using
    End Function
#End Region
End Class