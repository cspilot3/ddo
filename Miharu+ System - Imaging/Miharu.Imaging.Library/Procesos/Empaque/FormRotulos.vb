Imports Miharu.Desktop.Controls.DesktopReportViewer
Imports Miharu.Desktop.Controls
Imports Miharu.Desktop.Library
Imports Microsoft.Reporting.WinForms
Imports System.IO
Imports DBImaging
Imports System.Data.SqlClient
Public Class FormRotulos
    Inherits FormBase
    Private caja As String
    Private IdOT As Integer
    Private tipoFuid As Integer
    Private tipoGestion As String
    Public Property Valido As Boolean = False
    Sub New(nCaja As String, Id_OT As Integer, ntipoFuid As Integer, nTipoGestion As String)
        '--==================== INICIO REQUERIMIENTO RITM0364368 =====================================
        Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
        dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
        dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
        Dim _documentosStickerDataTable As DBImaging.SchemaConfig.TBL_Documento_StickerDataTable
        _documentosStickerDataTable = dbmImaging.SchemaConfig.TBL_Documento_Sticker.DBFindByfk_Entidadfk_Proyectogenera_Sticker_Fisicofk_Documento(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, True, Nothing)
        '--==================== FIN REQUERIMIENTO RITM0364368 =====================================

        InitializeComponent()
        caja = nCaja
        IdOT = Id_OT
        tipoFuid = ntipoFuid
        tipoGestion = nTipoGestion

        '--==================== INICIO REQUERIMIENTO RITM0364368 =====================================
        ImprimirStickerCheckBox.Enabled = False
        ImprimirRotulosCheckBox.Enabled = False
        ImprimirFuidCheckBox.Enabled = False
        CheckBoxImprimirHC.Enabled = False
        GuardarRotulosCheckBox.Enabled = False
        CheckBoxGuardarHC.Enabled = False
        CheckBoxGuardarFuid.Enabled = False
        GuardarStickerCheckBox.Enabled = False
        If Program.ImagingGlobal.ProyectoImagingRow.Usa_Rotulo_de_Cajas Or Program.ImagingGlobal.ProyectoImagingRow.Usa_Rotulo_de_Carpeta Then
            ImprimirRotulosCheckBox.Enabled = True
            GuardarRotulosCheckBox.Enabled = True
        ElseIf Program.ImagingGlobal.ProyectoImagingRow.Usa_Generacion_de_Fuid Then
            ImprimirFuidCheckBox.Enabled = True
            CheckBoxGuardarFuid.Enabled = True
        ElseIf Program.ImagingGlobal.ProyectoImagingRow.Usa_Hoja_Control Then
            CheckBoxImprimirHC.Enabled = True
            CheckBoxGuardarHC.Enabled = True
        ElseIf _documentosStickerDataTable.Rows.Count > 0 Then
            ImprimirStickerCheckBox.Enabled = True
            GuardarStickerCheckBox.Enabled = True
        End If
        '--==================== FIN REQUERIMIENTO RITM0364368 =====================================
    End Sub
    Private Sub BuscarButton_Click(sender As System.Object, e As System.EventArgs) Handles BuscarButton.Click
        Dim FormularioHojacontrol As New HojaControlEmpaque
        Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
        dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
        dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
        Try
            If ImprimirStickerCheckBox.Checked Then
                Dim objRotuloCaja As New FormVisorSticker(caja, IdOT)
                objRotuloCaja.ShowDialog()
                Valido = True
            End If
            If ImprimirRotulosCheckBox.Checked Then
                Dim objRotuloCaja As New FormVisorRotuloCaja(caja, IdOT, True, tipoFuid, tipoGestion)
                objRotuloCaja.ShowDialog()
                Dim objRotuloCarpeta As New FormVisorRotuloCarpeta(caja, IdOT, True, tipoFuid, tipoGestion)
                objRotuloCarpeta.ShowDialog()
                Valido = True
            End If
            If GuardarRotulosCheckBox.Checked Then
                Dim reporte As New DesktopReportViewer1Control
                Dim objReport As New FormGuardarRotulos(reporte)
                objReport.Launch(caja, IdOT, True, False, tipoFuid, tipoGestion)
                Valido = True
            End If
            If ImprimirFuidCheckBox.Checked Then
                Dim Fuidgeneral As Boolean = False
                Dim cajaFuid = dbmImaging.SchemaProcess.PA_Genarar_Fuid.DBExecute(Program.ImagingGlobal.ProyectoImagingRow.fk_Entidad, Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto, CStr(tipoFuid), "Caja", caja, tipoGestion)
                Dim objFUID As New FormVisorFuid(cajaFuid, tipoFuid, Fuidgeneral, tipoGestion)
                objFUID.ShowDialog()
                Valido = True
            End If
            If CheckBoxImprimirHC.Checked Then
                Dim Cedulas = getConsulta_Hoja_Control_Empaque(dbmImaging, caja, Program.ImagingGlobal.ProyectoImagingRow.fk_Entidad, Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto, tipoGestion)
                ' Dim Cedulas = dbmImaging.SchemaProcess.PA_Consulta_Hoja_Control_Empaque.DBExecute(caja, "", "Cedulas", tipoGestion, Program.ImagingGlobal.ProyectoImagingRow.fk_Entidad, Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto)
                FormularioHojacontrol.GenerarEmpaqueHojaControl(Cedulas, caja, "Imprimir")
                Valido = True
            End If
            If CheckBoxGuardarHC.Checked Then
                Dim Cedulas = getConsulta_Hoja_Control_Empaque(dbmImaging, caja, Program.ImagingGlobal.ProyectoImagingRow.fk_Entidad, Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto, tipoGestion)
                ' Dim Cedulas = dbmImaging.SchemaProcess.PA_Consulta_Hoja_Control_Empaque.DBExecute(caja, "", "Cedulas", tipoGestion, Program.ImagingGlobal.ProyectoImagingRow.fk_Entidad, Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto)
                FormularioHojacontrol.GenerarEmpaqueHojaControl(Cedulas, caja, "Guardar")
                Valido = True
            End If
            If CheckBoxGuardarFuid.Checked Then
                Dim Fuidgeneral As Boolean = False
                Dim reporte As New DesktopReportViewerFUID
                Dim cajaFuid = dbmImaging.SchemaProcess.PA_Genarar_Fuid.DBExecute(Program.ImagingGlobal.ProyectoImagingRow.fk_Entidad, Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto, CStr(tipoFuid), "Caja", caja, tipoGestion)
                Dim objFUID As New FormGuardarFuid(reporte)
                objFUID.Launch(cajaFuid, tipoFuid, Fuidgeneral, tipoGestion)
                Valido = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub FormRotulos_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim validoHojaControl As Boolean
        Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
        dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
        dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
        Try
            Dim TipoFuidHistoriaLaboral = dbmImaging.SchemaConfig.TBL_Parametro_Sistema.DBGet("@SerieHistoriaLaboralUMV").Item(0).Valor_Parametro_Sistema
            If CDbl(TipoFuidHistoriaLaboral) = tipoFuid Then
                validoHojaControl = True
            Else
                validoHojaControl = False
            End If
        Catch ex As Exception
        End Try
        If Not Program.ImagingGlobal.ProyectoImagingRow.Usa_Hoja_Control Or validoHojaControl = False Then
            CheckBoxGuardarHC.Enabled = False
            CheckBoxImprimirHC.Enabled = False
        End If
        If Not Program.ImagingGlobal.ProyectoImagingRow.Usa_Rotulo_de_Cajas Or Not Program.ImagingGlobal.ProyectoImagingRow.Usa_Rotulo_de_Carpeta Then
            ImprimirRotulosCheckBox.Enabled = False
            GuardarRotulosCheckBox.Enabled = False
        End If
        If Not Program.ImagingGlobal.ProyectoImagingRow.Usa_Generacion_de_Fuid Then
            CheckBoxGuardarFuid.Enabled = False
            ImprimirFuidCheckBox.Enabled = False
        End If
    End Sub
    Private Function getConsulta_Hoja_Control_Empaque(ByVal dbmImaging As DBImagingDataBaseManager, Precinto As String, fk_Entidad As Integer, fk_Proyecto As Integer, TipoGestion As String) As DataTable
        Dim dt As DataTable = Nothing
        Try
            Dim sqlquery As String = "DECLARE @CodigoSerie VARCHAR(10) " +
                               "	,@CampoSerie INT " +
                               "	,@CampoTipoArchivo INT " +
                              " SELECT @CampoTipoArchivo = Valor_Parametro_Sistema " +
                                        " FROM Config.TBL_Parametro_Sistema " +
                              " WHERE  Nombre_Parametro_Sistema = '@CampoTipoArchivo' " +
                              " 	AND fk_Entidad = @fk_Entidad " +
                              " 	AND fk_Proyecto = @fk_Proyecto " +
                              " SELECT @CampoSerie = Valor_Parametro_Sistema " +
                              " FROM   Config.TBL_Parametro_Sistema " +
                              " WHERE  Nombre_Parametro_Sistema = '@CampoSerie' " +
                              "	AND fk_Entidad = @fk_Entidad " +
                              " 	AND fk_Proyecto = @fk_Proyecto " +
                              " SELECT @CodigoSerie = Valor_Parametro_Sistema  " +
                              " FROM Config.TBL_Parametro_Sistema  " +
                             "	WHERE Nombre_Parametro_Sistema = '@SerieHistoriaLaboralUMV' " +
                              "	AND fk_Entidad = @fk_Entidad " +
                               " AND fk_Proyecto = @fk_Proyecto " +
                              " SELECT DISTINCT Precinto.Precinto AS Cedulas " +
                              " FROM [DB_Miharu.Imaging_Core].Process.TBL_OT AS OT WITH(NOLOCK) " +
                              " INNER JOIN	[DB_Miharu.Imaging_Core].Process.TBL_Empaque AS Empaque WITH(NOLOCK) " +
                                " ON	OT.id_OT = Empaque.fk_OT				 " +
                              " INNER JOIN	[DB_Miharu.Imaging_Core].[Process].[TBL_Empaque_Data] AS EmpaqueDataSerie WITH(NOLOCK) " +
                                " ON	EmpaqueDataSerie.fk_OT = Empaque.fk_OT " +
                                " AND Empaque.id_Empaque = EmpaqueDataSerie.fk_Empaque " +
                                " AND EmpaqueDataSerie.fk_Campo = @CampoSerie " +
                                " AND EmpaqueDataSerie.Data_Campo = @CodigoSerie " +
                              " INNER JOIN	[DB_Miharu.Imaging_Core].[Process].[TBL_Empaque_Data] AS EmpaqueDataTipoArchivo WITH(NOLOCK) " +
                                " ON	EmpaqueDataTipoArchivo.fk_OT = Empaque.fk_OT " +
                                " AND EmpaqueDataTipoArchivo.fk_Empaque = Empaque.id_Empaque " +
                                " AND EmpaqueDataTipoArchivo.fk_Campo = @CampoTipoArchivo " +
                                " AND EmpaqueDataTipoArchivo.Data_Campo = @TipoArchivo " +
                              " INNER JOIN	[DB_Miharu.Imaging_Core].Process.TBL_Empaque_Contenedor AS EmpaqueContenedor WITH(NOLOCK) " +
                                " ON	Empaque.fk_OT = EmpaqueContenedor.fk_OT " +
                                " AND Empaque.id_Empaque = EmpaqueContenedor.fk_Empaque " +
                              " INNER JOIN	[DB_Miharu.Imaging_Core].Process.TBL_Contenedor AS Contenedor WITH(NOLOCK) " +
                               "	ON 	Contenedor.Token = EmpaqueContenedor.Token " +
                                " AND Contenedor.fk_OT = EmpaqueContenedor.fk_OT " +
                              " INNER JOIN	[DB_Miharu.Imaging_Core].Process.TBL_Precinto AS Precinto WITH(NOLOCK) " +
                                " ON	Precinto.fk_OT = Contenedor.fk_OT " +
                                " AND Precinto.id_Precinto = Contenedor.fk_Precinto " +
                              " WHERE		OT.fk_Entidad = @fk_Entidad " +
                               "	AND OT.fk_Proyecto = @fk_Proyecto  " +
                               "	AND OT.fk_OT_Tipo = 2 " +
                                " AND Empaque.Precinto = @Precinto "
            Dim SqlParameter = New SqlParameter() _
           {
               New SqlParameter("@Precinto", Precinto),
               New SqlParameter("@fk_Entidad", fk_Entidad),
               New SqlParameter("@fk_Proyecto", fk_Proyecto),
               New SqlParameter("@TipoArchivo", TipoGestion)
           }
            dt = ExecuteQuery(sqlquery, dbmImaging, SqlParameter)
            Return dt
        Catch ex As Exception
            MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return dt
    End Function
    Private Function ExecuteQuery(ByVal s As String, ByVal dbmImaging As DBImagingDataBaseManager, ByVal ParamArray params() As SqlParameter) As DataTable
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
End Class