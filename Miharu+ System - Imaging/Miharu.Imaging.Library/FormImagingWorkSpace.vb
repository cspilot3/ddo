Imports System.Linq
Imports System.Reflection
Imports System.Web.UI.WebControls
Imports System.Windows.Forms
Imports DBImaging
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library
Imports Miharu.Imaging.Indexer
Imports Miharu.Imaging.Library.Procesos
Imports Miharu.Imaging.Library.Procesos.CargueLog
Imports Miharu.Imaging.Library.Procesos.ValidacionesDinamicas
Imports ClientUtil = Miharu.Desktop.Library.MiharuDMZ.ClientUtil
Imports QueryParameter = Miharu.Desktop.Library.MiharuDMZ.QueryParameter
Imports QueryRequestType = Miharu.Desktop.Library.MiharuDMZ.QueryRequestType
Imports QueryResponse = Miharu.Desktop.Library.MiharuDMZ.QueryResponse
Imports QueryResponseType = Miharu.Desktop.Library.MiharuDMZ.QueryResponseType

Public Class FormImagingWorkSpace
    Inherits FormBase
    Implements IWorkspace

#Region " Declaraciones "

    Private _eventManager As Eventos.EventManager

    Private _processLibrary As ProcessLibrary

#End Region

#Region " Propiedades "

    Public ReadOnly Property EventManager As Eventos.EventManager
        Get
            Return Me._eventManager
        End Get
    End Property

#End Region

#Region " Implementación IWorkspace "

    Public Property ProcessLibrary As Forms.IProcessLibrary Implements IWorkspace.ProcessLibrary
        Get
            Return _processLibrary
        End Get
        Set(ByVal value As Forms.IProcessLibrary)
            _processLibrary = CType(value, ProcessLibrary)
        End Set
    End Property

    Public Sub ConfigWorkspace() Implements IWorkspace.ConfigWorkspace
        Try
            ' Inicializar el formato de fecha
            DBImaging.DBImagingDataBaseManager.IdentifierDateFormat = Program.DesktopGlobal.IdentifierDateFormat
            DBCore.DBCoreDataBaseManager.IdentifierDateFormat = Program.DesktopGlobal.IdentifierDateFormat

            ''Se carga el título
            Me.Text = Program.ImagingGlobal.NombreEntidad & " - " & Program.ImagingGlobal.ProyectoImagingRow.Nombre_Proyecto & ""

            'SE HABILITAN CONTROLES SEGUN PERFIL USUARIO

            ' Proceso
            If Not Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Imaging.Proceso.Path) Then MainTabControl.TabPages.Remove(ProcesoTabPage)

            ' Proceso - Control
            ProcesoToolStripMenuItem.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Imaging.Proceso.Control.Path)
            SeguimientoCargueToolStripMenuItem.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Imaging.Proceso.Control.Seguimiento)
            SeguimientoCargueButton.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Imaging.Proceso.Control.Seguimiento)
            FechaProcesoButton.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Imaging.Proceso.Control.Fecha_de_proceso)
            OTButton.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Imaging.Proceso.Control.OT)
            AccesosToolStripMenuItem.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Imaging.Proceso.Control.Acceso)
            LogoFirmaToolStripMenuItem.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Imaging.Proceso.Control.Acceso)
            ProcesosEspecialesToolStripMenuItem.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Proceso_Especiales.Path)
            ProcesoCierreToolStripMenuItem.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Proceso_Generico.Path)

            If Not Program.ImagingGlobal.ProyectoImagingRow.Usa_Exportacion_Validos Then
                RechazosToolStripMenuItem.Enabled = False
            End If

            If Not Program.ImagingGlobal.ProyectoImagingRow.Usa_Cruce_Generico Then
                CierreToolStripMenuItem.Enabled = False
            End If

            If Not Program.ImagingGlobal.ProyectoImagingRow.Usa_Cargue_Log_Generico Then
                CargueLogToolStripMenuItem.Enabled = False
            End If
            If Not Program.ImagingGlobal.ProyectoImagingRow.Usa_Rotulo_de_Carpeta Then
                GenerarRotulosToolStripMenuItem.Enabled = False
            End If
            If Not Program.ImagingGlobal.ProyectoImagingRow.Usa_Rotulo_de_Cajas Then
                GenerarRotulosCajaToolStripMenuItem.Enabled = False
            End If
            If Not Program.ImagingGlobal.ProyectoImagingRow.Usa_Hoja_Control Then
                GenerarHojaDeControlToolStripMenuItem.Enabled = False
            End If
            If Not Program.ImagingGlobal.ProyectoImagingRow.Usa_Generacion_de_Fuid Then
                GenerarFuidToolStripMenuItem.Enabled = False
                GenerarFuidPorCajaToolStripMenuItem.Enabled = False
            End If
            'RITM0364368: RF-02  Generacion de estampado de sticker en imagen
            If Not Program.ImagingGlobal.ProyectoImagingRow.Usa_Genera_Sticker Then
                GenerarEstampadoToolStripMenuItem.Enabled = False
            End If

            ' Proceso - Físicos         
            DestapeButton.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Imaging.Proceso.Fisicos.Destape)
            EmpaqueButton.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Imaging.Proceso.Fisicos.Empaque)

            ' Proceso - Indexación
            CargueButton.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Imaging.Proceso.Indexacion.Cargue)
            IndexarButton.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Imaging.Proceso.Indexacion.Indexar)

            ' Proceso - Captura
            PreCapturaButton.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Imaging.Proceso.Captura.Precaptura)
            ReprocesosButton.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Imaging.Proceso.Captura.Reprocesos)
            CapturaButton.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Imaging.Proceso.Captura.Primera_captura)
            SegundaCapturaButton.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Imaging.Proceso.Captura.Segunda_captura)
            TerceraCapturaButton.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Imaging.Proceso.Captura.Tercera_captura)
            CalidadCapturaButton.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Imaging.Proceso.Captura.Calidad_captura)
            ValidacionesOpcionalesButton.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Imaging.Proceso.Captura.Validaciones_opcionales)
            CorreccionMaquinaButton.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Imaging.Proceso.Captura.Correccion_maquina_captura)
            ProcesoAdicionalCapturaButton.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Imaging.Proceso.Captura.Adicional_captura)

            ' Proceso - Recortes
            RecortesButton.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Imaging.Proceso.Recortes.Recorte)
            CalidadRecortesButton.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Imaging.Proceso.Recortes.Calidad_recorte)

            ' Proceso - Exportar
            ExportarButton.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Imaging.Proceso.Exportar)
            ExportarCredivaloresButton.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Imaging.Proceso.Exportar)
            ExportarCredivaloresButton.Visible = False 'Se oculta para realizar la publicacion
            CredivaloresToolStripMenuItem.Visible = False

            'Correos
            SeguimientoCorreoButton.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Imaging.Proceso.Correos)
            EnviarCorreoButton.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Imaging.Proceso.Correos)

            ' Búsqueda
            If Not Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Imaging.Busqueda) Then MainTabControl.TabPages.Remove(BusquedaTabPage)

            ' Búsqueda Lote
            If Not Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.BusquedaLote) Then MainTabControl.TabPages.Remove(BusquedaLoteTabPage)

            ' Relevo
            If Not Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Relevo) Then MainTabControl.TabPages.Remove(RelevoTabPage)

            ' RelevoCentralizador
            If Not Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.RelevoCentralizador) Then MainTabControl.TabPages.Remove(RelevoCentralTabPage)

            ' Digitalización
            If Not Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Digitalizacion) Then
                MainTabControl.TabPages.Remove(DigitalizacionTabPage)
            Else
                Miharu.DDO.Program.ImagingGlobal = Program.ImagingGlobal

                Miharu.DDO.Program._desktopGlobal = Program.DesktopGlobal

                Miharu.DDO.Program.MiharuSession = Program.Sesion
            End If

            ' Reportes
            If Not Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Imaging.Informes) Then
                MainTabControl.TabPages.Remove(InformesTabPage)
            Else
                WorkSpaceImagingReportViewerControl.ReportList.Clear()

                WorkSpaceImagingReportViewerControl.ReportList.Add(New Reportes.ControlLotes.Report_ControlLotesEfectividad(WorkSpaceImagingReportViewerControl))
                WorkSpaceImagingReportViewerControl.ReportList.Add(New Reportes.ControlLotes.Report_ControlLotesFaltantes(WorkSpaceImagingReportViewerControl))
                WorkSpaceImagingReportViewerControl.ReportList.Add(New Reportes.ControlLotes.Report_ControlLotesSobrantes(WorkSpaceImagingReportViewerControl))

                WorkSpaceImagingReportViewerControl.ReportList.Add(New Reportes.ControlLotes.Report_ControlLotesPD(WorkSpaceImagingReportViewerControl))
                WorkSpaceImagingReportViewerControl.ReportList.Add(New Reportes.ControlLotes.Report_ControlLotesAD(WorkSpaceImagingReportViewerControl))
                WorkSpaceImagingReportViewerControl.ReportList.Add(New Reportes.ControlLotes.Report_ControlLotesFileNet(WorkSpaceImagingReportViewerControl))
                WorkSpaceImagingReportViewerControl.ReportList.Add(New Reportes.ControlLotes.Report_ControlLotesMarcacion(WorkSpaceImagingReportViewerControl))
                WorkSpaceImagingReportViewerControl.ReportList.Add(New Reportes.ControlLotes.Report_Volumenes(WorkSpaceImagingReportViewerControl))
                WorkSpaceImagingReportViewerControl.ReportList.Add(New Reportes.Filenet.Report_Filenet_Documento(WorkSpaceImagingReportViewerControl))

                WorkSpaceImagingReportViewerControl.ReportList.Add(New Reportes.ProductividadGeneral.Report_ProductividadGeneral(WorkSpaceImagingReportViewerControl))
                WorkSpaceImagingReportViewerControl.ReportList.Add(New Reportes.ProductividadDetallado.Report_ProductividadDetallado(WorkSpaceImagingReportViewerControl))
                WorkSpaceImagingReportViewerControl.ReportList.Add(New Reportes.DestapeContenedor.Report_DestapeContenedor(WorkSpaceImagingReportViewerControl))
                WorkSpaceImagingReportViewerControl.ReportList.Add(New Reportes.ControlProceso.Report_ControlProceso(WorkSpaceImagingReportViewerControl))
                If (Not Program.ImagingGlobal.ProyectoImagingRow.Usa_Destape_Contenedor) Then WorkSpaceImagingReportViewerControl.ReportList.Add(New Report_DestapeContenedorDetallado(WorkSpaceImagingReportViewerControl))
                WorkSpaceImagingReportViewerControl.ReportList.Add(New Reportes.Destape.Report_Destape(WorkSpaceImagingReportViewerControl))
                If (Not Program.ImagingGlobal.ProyectoImagingRow.Usa_Destape_Contenedor) Then WorkSpaceImagingReportViewerControl.ReportList.Add(New Report_DestapeDetallado(WorkSpaceImagingReportViewerControl))
                WorkSpaceImagingReportViewerControl.ReportList.Add(New Reportes.Empaque.Report_Empaque(WorkSpaceImagingReportViewerControl))
                If (Not Program.ImagingGlobal.ProyectoImagingRow.Usa_Destape_Contenedor) Then WorkSpaceImagingReportViewerControl.ReportList.Add(New Reportes.EmpaqueDetallado.Report_EmpaqueDetallado(WorkSpaceImagingReportViewerControl))
                WorkSpaceImagingReportViewerControl.ReportList.Add(New Reportes.ImagenesExportadas.ReportImagenesExportadas(WorkSpaceImagingReportViewerControl))
                WorkSpaceImagingReportViewerControl.ReportList.Add(New Reportes.ValidacionesDinamicas.Report_DocumentosObligatoriosFaltantes(WorkSpaceImagingReportViewerControl))
                WorkSpaceImagingReportViewerControl.ReportList.Add(New Reportes.ValidacionesDinamicas.Report_ValidacionCampos(WorkSpaceImagingReportViewerControl))
            End If

            ' Configuración Imaging
            ConfiguracionModuloToolStripMenuItem.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Imaging.Configuracion.Path)

            If (Program.DesktopGlobal.CentroProcesamientoRow.Usa_Cache_Local) Then
                Me.UsaCacheLocalLabel.Text = "Usa caché local"
                Me.UsaCacheLocalLabel.ForeColor = Drawing.Color.Green
            Else
                Me.UsaCacheLocalLabel.Text = "No usa caché local"
                Me.UsaCacheLocalLabel.ForeColor = Drawing.Color.Red
            End If

            If (Program.ImagingGlobal.UsaDominioExterno) Then
                Me.UsaDominioExternoLabel.Text = "Usa dominio externo"
            Else
                Me.UsaDominioExternoLabel.Text = "No usa dominio externo"
            End If

        Catch ex As Exception
            DesktopMessageBoxControl.DesktopMessageShow("CargaPermisos", ex)
        End Try
    End Sub

#End Region

#Region " Eventos "

    Private Sub FormImagingWorkSpace_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
        Me.WorkSpaceImagingSearchControl.Workspace = Me

        Miharu.DDO.Program.ImagingGlobal = Program.ImagingGlobal

        Miharu.DDO.Program._desktopGlobal = Program.DesktopGlobal

        Miharu.DDO.Program.MiharuSession = Program.Sesion

        Me.WindowState = FormWindowState.Maximized

        'Cargar los plugins
        Try
            If (Not Program.PluginManager Is Nothing) Then
                Program.PluginManager.ClosePlugins()
            End If

            Program.PluginManager = New Plugins.DesktopPluginManager(
                Program.AppPath.TrimEnd("\"c) + "\Plugins",
                Me,
                Program.Sesion)

            Program.PluginManager.ImagingGlobal = Program.ImagingGlobal
            Program.PluginManager.DesktopGlobal = Program.DesktopGlobal

            Program.PluginManager.LoadPlugins(ProcessLibraryType.Imaging, Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto)

            Program.PluginManager.Activateplugins()

            Dim eventManagerList = (From plugin In Program.PluginManager.Plugins Select CType(plugin.EventExecuter, Eventos.IEventExecuter)).ToList()

            Me._eventManager = New Eventos.EventManager(eventManagerList)

            Me.WorkSpaceImagingReportViewerControl.Load_Reports()

        Catch ex As Exception
            MessageBox.Show("Error al cargar los plugins, " + ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'Menus
    Private Sub CamposToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CamposToolStripMenuItem.Click
        Dim parcampoImaging As New Procesos.Configuracion.Imaging.FormParCamposImaging
        parcampoImaging.ShowDialog()
    End Sub

    Private Sub AccesosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AccesosToolStripMenuItem.Click
        Dim accesosForm As New Procesos.Configuracion.Imaging.FormAccesos
        accesosForm.ShowDialog()
    End Sub

    Private Sub LogoFirmaToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles LogoFirmaToolStripMenuItem.Click
        Dim LogoFirmaForm As New Procesos.Configuracion.Imaging.FormConfigImagenes
        LogoFirmaForm.ShowDialog()
    End Sub

    Private Sub RechazosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RechazosToolStripMenuItem.Click
        Dim FormRechazos As New Procesos.Rechazos.FormRechazos_ValidacionesCampos
        FormRechazos.ShowDialog()
    End Sub

    Private Sub CargueLogToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CargueLogToolStripMenuItem.Click
        Dim CargueLogForm As New Procesos.CargueLog.FormCargueLog
        CargueLogForm.ShowDialog()
    End Sub

    Private Sub GenerarRotulosToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles GenerarRotulosToolStripMenuItem.Click
        Dim RotuloCarpetaForm As New FormRotuloCarpeta
        RotuloCarpetaForm.ShowDialog()
    End Sub

    Private Sub CierreToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CierreToolStripMenuItem.Click
        Try
            Dim CruceForm As New Procesos.Cruce.FormCruce
            CruceForm.WorkSpace = Me
            CruceForm.ShowDialog()

        Catch ex As Exception
            DesktopMessageBoxControl.DesktopMessageShow("Cruce", ex)
        End Try
    End Sub

    'Indexación
    Private Sub FormImagingWorkSpace_KeyDown(ByVal sender As System.Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
        KeyDownAction(e)
    End Sub

    Private Sub CargueButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CargueButton.Click
        Cargue()
    End Sub

    Private Sub SeguimientoCargueButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles SeguimientoCargueButton.Click
        Seguimiento()
    End Sub

    Private Sub SeguimientoCorreoButton_Click(sender As System.Object, e As System.EventArgs) Handles SeguimientoCorreoButton.Click
        SeguimientoCorreo()
    End Sub

    Private Sub IndexarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles IndexarButton.Click
        Indexacion()
    End Sub

    Private Sub PreCapturaButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles PreCapturaButton.Click
        PreCaptura()
    End Sub

    Private Sub CapturaButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CapturaButton.Click
        PrimeraCaptura()
    End Sub

    Private Sub SegundaCapturaButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles SegundaCapturaButton.Click
        SegundaCaptura()
    End Sub

    Private Sub TerceraCapturaButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles TerceraCapturaButton.Click
        TerceraCaptura()
    End Sub

    Private Sub CalidadCapturaButton_Click(sender As System.Object, e As EventArgs) Handles CalidadCapturaButton.Click
        CalidadCaptura()
    End Sub

    Private Sub ExportarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ExportarButton.Click
        Exportar()
    End Sub
    'Private Sub ExportarCredivaloresButton_Click(sender As System.Object, e As System.EventArgs) Handles ExportarCredivaloresButton.Click
    '    ExportarCredivalores()
    'End Sub
    Private Sub ReprocesosButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ReprocesosButton.Click
        Reprocesos()
    End Sub

    Private Sub RecortesButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles RecortesButton.Click
        Recortar()
    End Sub

    Private Sub CalidadRecortesButton_Click(sender As System.Object, e As EventArgs) Handles CalidadRecortesButton.Click
        CalidadRecortes()
    End Sub

    Private Sub ValidacionesOPcionalesButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ValidacionesOpcionalesButton.Click
        ValidacionesOpcionales()
    End Sub

    Private Sub EnviarCorreoButton_Click(sender As System.Object, e As System.EventArgs) Handles EnviarCorreoButton.Click
        EnviarCorreos()
    End Sub

    Private Sub KeyDownAction(ByVal e As KeyEventArgs)
        Select Case e.KeyCode
            Case Keys.F2 : Cargue()
            Case Keys.F3 : Indexacion()
            Case Keys.F4 : Reprocesos()
            Case Keys.F5 : PreCaptura()
            Case Keys.F6 : PrimeraCaptura()
            Case Keys.F7 : SegundaCaptura()
            Case Keys.F8 : TerceraCaptura()
            Case Keys.F9 : Exportar()
            Case Keys.F10 : CargueSeguimiento()
            Case Keys.F11 : ValidacionesOpcionales()
        End Select
    End Sub

    Private Sub DocumentosToolStripMenuItem_Click(sender As System.Object, e As EventArgs) Handles DocumentosToolStripMenuItem.Click
        Dim documentoForm As New Procesos.Configuracion.Imaging.FormParDocumentos()
        documentoForm.ShowDialog()
    End Sub

    Private Sub TipoOTToolStripMenuItem1_Click(sender As System.Object, e As EventArgs) Handles TipoOTToolStripMenuItem1.Click
        Dim parTipoOtImagingForm As New Procesos.Configuracion.Imaging.FormTipoOT
        parTipoOtImagingForm.ShowDialog()
    End Sub

    Private Sub ValidacionesToolStripMenuItem_Click(sender As System.Object, e As EventArgs) Handles ValidacionesToolStripMenuItem.Click
        Dim parValidacionImagingForm As New Procesos.Configuracion.Imaging.FormParValidaciones
        parValidacionImagingForm.ShowDialog()
    End Sub

    Private Sub PrecintoCamposToolStripMenuItem1_Click(sender As System.Object, e As EventArgs) Handles PrecintoCamposToolStripMenuItem1.Click
        Dim parPrecintoCampoImagingForm As New Procesos.Configuracion.Imaging.FormPrecintoCampo
        parPrecintoCampoImagingForm.ShowDialog()
    End Sub

    Private Sub EmpaqueCamposToolStripMenuItem_Click(sender As System.Object, e As EventArgs) Handles EmpaqueCamposToolStripMenuItem.Click
        Dim parEmpaqueCampoImagingForm As New Procesos.Configuracion.Imaging.FormEmpaqueCampo
        parEmpaqueCampoImagingForm.ShowDialog()
    End Sub

    Private Sub ContenedorCamposToolStripMenuItem1_Click(sender As System.Object, e As EventArgs) Handles ContenedorCamposToolStripMenuItem1.Click
        Dim parContenedorCampoImagingForm As New Procesos.Configuracion.Imaging.FormContenedorCampo
        parContenedorCampoImagingForm.ShowDialog()
    End Sub

    Private Sub GeneralesToolStripMenuItem_Click(sender As System.Object, e As EventArgs) Handles GeneralesToolStripMenuItem.Click
        Dim parametrosForm As New Procesos.Configuracion.Imaging.FormParametros
        parametrosForm.ShowDialog()
    End Sub

    Private Sub OTButton_Click(sender As System.Object, e As EventArgs) Handles OTButton.Click
        Ot()
    End Sub

    Private Sub FechaProcesoButton_Click(sender As System.Object, e As EventArgs) Handles FechaProcesoButton.Click
        FechaProceso()
    End Sub

    Private Sub EmpaqueButton_Click(sender As System.Object, e As EventArgs) Handles EmpaqueButton.Click
        Empaque()
    End Sub

    Private Sub DestapeButton_Click(sender As System.Object, e As EventArgs) Handles DestapeButton.Click
        Destape()
    End Sub

    Private Sub ExportaPDFToolStripMenuItem_Click(sender As System.Object, e As EventArgs) Handles ExportaPDFToolStripMenuItem.Click
        ExportaPDF()
    End Sub

    Private Sub SeguimientoCargueToolStripMenuItem_Click(sender As System.Object, e As EventArgs) Handles SeguimientoCargueToolStripMenuItem.Click
        CargueSeguimiento()
    End Sub

    Private Sub TablasAsociadasToolStripMenuItem_Click(sender As System.Object, e As EventArgs) Handles TablasAsociadasToolStripMenuItem.Click
        Dim parTablaAsociadaForm As New Procesos.Configuracion.Imaging.FormParTablaAsociadaImaging()
        parTablaAsociadaForm.ShowDialog()
    End Sub

    Private Sub ProcesoAdicionalCapturaButton_Click(sender As System.Object, e As System.EventArgs) Handles ProcesoAdicionalCapturaButton.Click
        ProcesoAdicionalCaptura()
    End Sub

    Private Sub CorreccionMaquinaButton_Click(sender As System.Object, e As System.EventArgs) Handles CorreccionMaquinaButton.Click
        If (Program.ImagingGlobal.ProyectoImagingRow.Usa_Correccion_Captura_Maquina) Then
            CorreccionCapturaMaquina()
        Else
            MessageBox.Show("El proyecto no usa corrección de captura máquina", "Corrección Captura Máquina", MessageBoxButtons.OK)
        End If
    End Sub

    Private Sub ValidacionesDinámicasToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ValidacionesDinámicasToolStripMenuItem.Click
        Dim f = New FormValidacionesDinamicas()
        f.ShowDialog()
    End Sub
#End Region

#Region " Metodos "

    Private Sub Cargue()
        If (Not CargueButton.Visible Or Not CargueButton.Enabled) Then Return
        If (Not LaunchUtil.ValidarAcceso(LaunchUtil.EtapaEnum.Cargue)) Then Return

        Try
            Dim seleccionOt = New Procesos.OT.FormSeleccionarOTCargue()
            If seleccionOt.ShowDialog <> DialogResult.OK Then Return

            Dim newCargue As Procesos.Cargue.CargueBase

            If (Program.ImagingGlobal.ProyectoImagingRow.Aplica_Fisico) Then
                newCargue = New Procesos.Cargue.CargueRisk(seleccionOt.FechaProceso, seleccionOt.OT, Me.EventManager)
            Else
                newCargue = New Procesos.Cargue.CargueImaging(seleccionOt.FechaProceso, seleccionOt.OT, Me.EventManager)
            End If

            newCargue.Run()

        Catch ex As Exception
            DesktopMessageBoxControl.DesktopMessageShow("Cargue", ex)
        End Try
    End Sub

    Private Sub CargueSeguimiento()
        If (Not SeguimientoCargueButton.Visible Or Not SeguimientoCargueButton.Enabled) Then Return

        Try
            Dim f As New Procesos.Cargue.FormEstadoCargue()
            f.ShowDialog()
        Catch ex As Exception
            DesktopMessageBoxControl.DesktopMessageShow("Seguimiento", ex)
        End Try
    End Sub

    Private Sub ExportaPDF()
        'If (Not SeguimientoCargueButton.Visible Or Not SeguimientoCargueButton.Enabled) Then Return

        Try
            Dim f As New Procesos.Exportar.FormExportPDF()
            f.ShowDialog()
        Catch ex As Exception
            DesktopMessageBoxControl.DesktopMessageShow("Seguimiento", ex)
        End Try
    End Sub

    Private Sub Seguimiento()
        If (Not SeguimientoCargueButton.Visible Or Not SeguimientoCargueButton.Enabled) Then Return

        Try
            Dim f As New Procesos.Cargue.FormSeguimiento()
            f.ShowDialog()
        Catch ex As Exception
            DesktopMessageBoxControl.DesktopMessageShow("Seguimiento", ex)
        End Try
    End Sub

    Private Sub SeguimientoCorreo()
        If (Not SeguimientoCorreoButton.Visible Or Not SeguimientoCorreoButton.Enabled) Then Return

        Try
            Dim f As New Procesos.Correos.FormSeguimientoCorreo()
            f.ShowDialog()
        Catch ex As Exception
            DesktopMessageBoxControl.DesktopMessageShow("Seguimiento Correos", ex)
        End Try
    End Sub

    ' Indexacion
    Private Sub Indexacion()
        LaunchUtil.Indexar(Nothing, GetType(Controller.Indexer.Indexacion.IndexacionController), DBCore.EstadoEnum.Indexacion, LaunchUtil.EtapaEnum.Indexacion, Me, Me.IndexarButton, True)
    End Sub

    Private Sub PreCaptura()
        If Program.Sesion.IsExternal Then
            LaunchUtil.Indexar(Nothing, GetType(Controller.Indexer.Capturas.Captura.PreCapturaControllerDMZ), DBCore.EstadoEnum.Pre_Captura, LaunchUtil.EtapaEnum.PreCaptura, Me, Me.PreCapturaButton, True)
        Else
            LaunchUtil.Indexar(Nothing, GetType(Controller.Indexer.Capturas.Captura.PreCapturaController), DBCore.EstadoEnum.Pre_Captura, LaunchUtil.EtapaEnum.PreCaptura, Me, Me.PreCapturaButton, True)
        End If
    End Sub

    Private Sub PrimeraCaptura()
        LaunchUtil.Indexar(Nothing, GetType(Controller.Indexer.Capturas.Captura.PrimeraCapturaController), DBCore.EstadoEnum.Captura, LaunchUtil.EtapaEnum.PrimeraCaptura, Me, Me.CapturaButton, True)
    End Sub

    Private Sub SegundaCaptura()
        LaunchUtil.Indexar(Nothing, GetType(Controller.Indexer.Capturas.Captura.SegundaCapturaController), DBCore.EstadoEnum.Segunda_Captura, LaunchUtil.EtapaEnum.SegundaCaptura, Me, Me.SegundaCapturaButton, True)
    End Sub

    Private Sub TerceraCaptura()
        LaunchUtil.Indexar(Nothing, GetType(Controller.Indexer.Capturas.Captura.TerceraCapturaController), DBCore.EstadoEnum.Tercera_Captura, LaunchUtil.EtapaEnum.TerceraCaptura, Me, Me.TerceraCapturaButton, True)
    End Sub

    Private Sub CalidadCaptura()
        LaunchUtil.Indexar(Nothing, GetType(Controller.Indexer.Capturas.Captura.CalidadCapturaController), DBCore.EstadoEnum.Calidad_Captura, LaunchUtil.EtapaEnum.CalidadCaptura, Me, Me.CalidadCapturaButton, True)
    End Sub

    Private Sub CalidadRecortes()
        LaunchUtil.Indexar(Nothing, GetType(Controller.Recorte.CalidadRecorteController), DBCore.EstadoEnum.Calidad_Recorte, LaunchUtil.EtapaEnum.CalidadRecorte, Me, Me.CalidadRecortesButton, True)
    End Sub


    Private Sub ValidacionesOpcionales()
        LaunchUtil.Indexar(Nothing, GetType(Controller.Indexer.Capturas.Validacion.ValidacionesOpcionalesController), DBCore.EstadoEnum.Indexado, LaunchUtil.EtapaEnum.ValidacionesOpcionales, Me, Me.ValidacionesOpcionalesButton, True)
    End Sub

    'Reprocesos
    Private Sub Reprocesos()
        If (Not ReprocesosButton.Visible Or Not ReprocesosButton.Enabled) Then Return
        If (Not LaunchUtil.ValidarAcceso(LaunchUtil.EtapaEnum.Reprocesos)) Then Return

        Try
            Dim seleccionOt = New Procesos.OT.FormSeleccionarOTCaptura(DBCore.EstadoEnum.Reproceso)
            If seleccionOt.ShowDialog <> DialogResult.OK Then Return

            Dim formReprocesos = New Procesos.Reproceso.FormReprocesos(Me)
            formReprocesos.OT = seleccionOt.OT
            formReprocesos.ShowDialog()
        Catch ex As Exception
            DesktopMessageBoxControl.DesktopMessageShow("Reprocesos", ex)
        End Try
    End Sub

    'Recortar
    Private Sub Recortar()
        LaunchUtil.Indexar(Nothing, GetType(Controller.Recorte.RecorteController), DBCore.EstadoEnum.Recorte, LaunchUtil.EtapaEnum.Recorte, Me, Me.RecortesButton, True)
    End Sub

    'Exportar
    Private Sub Exportar()
        If (Not ExportarButton.Visible Or Not ExportarButton.Enabled) Then Return

        Try
            Dim formExportar = New Procesos.Exportar.FormExport()
            formExportar.EventManager = Me._eventManager
            formExportar.ShowDialog()
        Catch ex As Exception
            DesktopMessageBoxControl.DesktopMessageShow("Exportar", ex)
        End Try

    End Sub
    'Public Sub ExportarCredivalores()
    '    If (Not ExportarCredivaloresButton.Visible Or Not ExportarCredivaloresButton.Enabled) Then Return
    '    Try
    '        Dim FormExportCredivalores = New Procesos.Exportar.FormExportCredivalores()
    '        FormExportCredivalores.EventManager = Me._eventManager
    '        FormExportCredivalores.ShowDialog()
    '    Catch ex As Exception
    '        DesktopMessageBoxControl.DesktopMessageShow("Exportar", ex)
    '    End Try

    'End Sub

    'Destape
    Private Sub FechaProceso()
        If (Not FechaProcesoButton.Visible Or Not FechaProcesoButton.Enabled) Then Return

        Try
            Dim newFechaProceso As New Procesos.Destape.FormFechaProceso()
            newFechaProceso.WorkSpace = Me
            newFechaProceso.ShowDialog()

        Catch ex As Exception
            DesktopMessageBoxControl.DesktopMessageShow("FechaProceso", ex)
        End Try
    End Sub

    Private Sub Ot()
        If (Not OTButton.Visible Or Not OTButton.Enabled) Then Return

        Try
            Dim newOt = New Procesos.Destape.FormOT
            newOt.WorkSpace = Me
            newOt.ShowDialog()

        Catch ex As Exception
            DesktopMessageBoxControl.DesktopMessageShow("OT", ex)
        End Try
    End Sub

    Private Sub Destape()
        If Program.DesktopGlobal.PuestoTrabajoRow.TieneCamara Then
            If (Not DestapeButton.Visible Or Not DestapeButton.Enabled) Then Return

            Try
                Dim newDestape = New Procesos.Destape.FormPreDestape
                newDestape.EventManager = Me._eventManager
                newDestape.ShowDialog()

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Destape", ex)
            End Try
        Else
            Dim ex As New Exception("No se puede realizar esta operación en este puesto de trabajo (No esta configurado para tener camara)")
            DesktopMessageBoxControl.DesktopMessageShow("Destape", ex)
        End If
    End Sub

    Private Sub Empaque()
        If (Not EmpaqueButton.Visible Or Not EmpaqueButton.Enabled) Then Return

        Try
            Dim newEmpaque = New Procesos.Empaque.FormPreEmpaque
            newEmpaque.EventManager = Me._eventManager
            newEmpaque.ShowDialog()

        Catch ex As Exception
            DesktopMessageBoxControl.DesktopMessageShow("Empaque", ex)
        End Try
    End Sub

    Private Sub ProcesoAdicionalCaptura()
        LaunchUtil.Indexar(Nothing, GetType(Controller.Indexer.Capturas.Captura.ProcesoAdicionalCapturaController), DBCore.EstadoEnum.Proceso_Adicional, LaunchUtil.EtapaEnum.ProcesoAdicional, Me, Me.ProcesoAdicionalCapturaButton, True)
    End Sub

    Private Sub CorreccionCapturaMaquina()
        LaunchUtil.Indexar(Nothing, GetType(Controller.Indexer.Capturas.Captura.CorreccionCapturaMaquinaController), DBCore.EstadoEnum.Correccion_Maquina, LaunchUtil.EtapaEnum.CorreccionMaquina, Me, Me.CorreccionMaquinaButton, True)
    End Sub
#End Region

    Private Sub GenerarHojaDeControlToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles GenerarHojaDeControlToolStripMenuItem.Click
        Dim HojaDeControlForm As New FormGenerarHojadeControl
        HojaDeControlForm.ShowDialog()
    End Sub

    Private Sub GenerarRotulosCajaToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles GenerarRotulosCajaToolStripMenuItem.Click
        Dim RotuloCajaForm As New FormRotuloCaja
        RotuloCajaForm.ShowDialog()
    End Sub

    Private Sub GenerarFuidToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles GenerarFuidToolStripMenuItem.Click
        Dim objFiud As New FormGenerarFuid()
        objFiud.ShowDialog()
    End Sub

    Private Sub GenerarFuidPorCajaToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles GenerarFuidPorCajaToolStripMenuItem.Click
        Dim objFiud As New FormGenerarFuidporCaja()
        objFiud.ShowDialog()
    End Sub

    Private Sub ReemplazarImagenesToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ReemplazarImagenesToolStripMenuItem.Click
        Dim objFiud As New ReemplazarImagenesForm()
        objFiud.ShowDialog()
    End Sub

    Private Sub CredivaloresToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles CredivaloresToolStripMenuItem.Click
        'Dim objFiud As New Form1()
        'objFiud.ShowDialog()
    End Sub

    Private Sub LogCargaToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles LogCargaToolStripMenuItem.Click
        Dim objCargaLog As New FormCargueDescarga()
        objCargaLog.ShowDialog()
    End Sub

    Private Sub GenerarEstampadoToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles GenerarEstampadoToolStripMenuItem.Click
        Try
            Dim EstampadoForm As New Procesos.GenerarSticker.FormGeneraSticker
            EstampadoForm.WorkSpace = Me
            EstampadoForm.ShowDialog()
        Catch ex As Exception
            DesktopMessageBoxControl.DesktopMessageShow("Estampado", ex)
        End Try
    End Sub

    Private Sub EnviarCorreos()
        If (Not EnviarCorreoButton.Visible Or Not EnviarCorreoButton.Enabled) Then Return

        Try
            Dim f As New Procesos.Correos.FormEnviarCorreo()
            f.ShowDialog()
        Catch ex As Exception
            DesktopMessageBoxControl.DesktopMessageShow("Enviar Correos", ex)
        End Try
    End Sub


End Class

Public Class LaunchUtil

#Region " Declaraciones "

    Private Shared _ejecutando As Boolean = False
    Private Shared _idDocumento As Slyg.Tools.SlygNullable(Of Integer)
    Private Shared _idOt As Integer

#End Region

#Region " Enumeraciones "

    Public Enum EtapaEnum
        Cargue
        Indexacion
        Reprocesos
        ValidacionesOpcionales
        PreCaptura
        PrimeraCaptura
        SegundaCaptura
        TerceraCaptura
        CalidadCaptura
        Recorte
        CalidadRecorte
        ValidacionListas
        ProcesoAdicional
        CorreccionMaquina
    End Enum

#End Region

#Region " Metodos "

    Public Shared Sub Indexar(ot As Slyg.Tools.SlygNullable(Of Integer), ByVal nController As Type, ByVal nEstado As DBCore.EstadoEnum, ByVal nAcceso As EtapaEnum, ByRef nWorkSpace As FormImagingWorkSpace, ByRef nButton As System.Windows.Forms.Button, ByVal nDemanda As Boolean)
        Indexar(ot, nController, nEstado, nAcceso, nWorkSpace, nButton, nDemanda, "", Nothing)
    End Sub

    Public Shared Sub Indexar(ot As Slyg.Tools.SlygNullable(Of Integer), ByVal nController As Type, ByVal nEstado As DBCore.EstadoEnum, ByVal nAcceso As EtapaEnum, ByRef nWorkSpace As FormImagingWorkSpace, ByRef nButton As System.Windows.Forms.Button, ByVal nDemanda As Boolean, ByVal nSetDataMetod As String, ByVal nParameters As Object())
        If (_ejecutando) Then Return
        _ejecutando = True

        _idDocumento = -1

        Try
            If (Not nButton.Visible Or Not nButton.Enabled) Then Return
            If (Not ValidarAcceso(nAcceso)) Then Return

            nWorkSpace.Cursor = Cursors.AppStarting
            Application.DoEvents()

            If (ot Is Nothing) Then
                Dim seleccionOt As Procesos.OT.FormSeleccionarOT

                If (nEstado = DBCore.EstadoEnum.Indexado) Then
                    seleccionOt = New Procesos.OT.FormSeleccionarOTValidacion()
                ElseIf (nEstado = DBCore.EstadoEnum.Indexacion) Then
                    seleccionOt = New Procesos.OT.FormSeleccionarOTIndexacion()
                ElseIf (nEstado = DBCore.EstadoEnum.Proceso_Adicional) Then
                    seleccionOt = New Procesos.OT.FormSeleccionarOTValidacionListas()
                Else
                    If Program.Sesion.IsExternal Then
                        seleccionOt = New Procesos.OT.FormSeleccionarOTCapturaDMZ(nEstado)
                    Else
                        seleccionOt = New Procesos.OT.FormSeleccionarOTCaptura(nEstado)
                    End If
                    'seleccionOt = New Procesos.OT.FormSeleccionarOTCaptura(nEstado)
                End If

                If seleccionOt.ShowDialog <> DialogResult.OK Then Return
                _idOt = seleccionOt.OT
            Else
                _idOt = ot
            End If

            Do
                Dim newDomain As AppDomain = Nothing
                Dim newController As Controller.IController = Nothing

                Try
                    ' Crear el objeto en el nuevo dominio
                    'If ((Program.ImagingGlobal.UsaDominioExterno)) Then
                    If ((Program.ImagingGlobal.ProyectoImagingRow.Usa_Dominio_Externo)) Then
                        newDomain = CreateDommain()
                        newController = CType(newDomain.CreateInstanceAndUnwrap(nController.Assembly.FullName, nController.FullName), Controller.IController)
                    Else
                        newController = CType(Activator.CreateInstance(nController), Controller.IController)
                    End If

                    If (Not LaunchIndexar(newController, nWorkSpace, nSetDataMetod, nParameters, nEstado, nDemanda)) Then
                        Return
                    End If
                Catch ex As Exception
                    Throw
                Finally
                    If (newController IsNot Nothing) Then
                        newController.Dispose()
                    End If

                    If (newDomain IsNot Nothing) Then
                        ' Descargar el dominio secundario
                        Try : AppDomain.Unload(newDomain) : Catch : End Try
                    End If

                    Application.DoEvents()
                End Try
            Loop While True 'Not Salir
        Catch ex As Exception
            DesktopMessageBoxControl.DesktopMessageShow("Indexacion", ex)
        Finally
            nWorkSpace.Cursor = Cursors.Default
            _ejecutando = False
        End Try
    End Sub

    Private Shared Function LaunchIndexar(ByRef newController As Controller.IController, ByRef nWorkSpace As FormImagingWorkSpace, ByVal nSetDataMetod As String, ByVal nParameters As Object(), ByVal nEstado As DBCore.EstadoEnum, ByVal nDemanda As Boolean) As Boolean
        Try
            ' Enlazar los eventos                   
            AddHandler newController.EventManager.OnValidarPermiso, AddressOf nWorkSpace.EventManager.ValidarPermiso
            AddHandler newController.EventManager.OnFinalizarIndexacion, AddressOf nWorkSpace.EventManager.FinalizarIndexacion
            AddHandler newController.EventManager.OnFinalizarReIndexacion, AddressOf nWorkSpace.EventManager.FinalizarReIndexacion
            AddHandler newController.EventManager.OnValidarSavePrimeraCaptura, AddressOf nWorkSpace.EventManager.ValidarSavePrimeraCaptura
            AddHandler newController.EventManager.OnValidarSaveSegundaCaptura, AddressOf nWorkSpace.EventManager.ValidarSaveSegundaCaptura
            AddHandler newController.EventManager.OnValidarSaveTerceraCaptura, AddressOf nWorkSpace.EventManager.ValidarSaveTerceraCaptura
            AddHandler newController.EventManager.OnValidarSaveCalidad, AddressOf nWorkSpace.EventManager.ValidarSaveCalidad
            AddHandler newController.EventManager.OnFinalizarPreCaptura, AddressOf nWorkSpace.EventManager.FinalizarPreCaptura
            AddHandler newController.EventManager.OnFinalizarPrimeraCaptura, AddressOf nWorkSpace.EventManager.FinalizarPrimeraCaptura
            AddHandler newController.EventManager.OnFinalizarSegundaCaptura, AddressOf nWorkSpace.EventManager.FinalizarSegundaCaptura
            AddHandler newController.EventManager.OnFinalizarTerceraCaptura, AddressOf nWorkSpace.EventManager.FinalizarTerceraCaptura
            AddHandler newController.EventManager.OnFinalizarCalidad, AddressOf nWorkSpace.EventManager.FinalizarCalidad
            AddHandler newController.EventManager.OnFinalizarValidaciones, AddressOf nWorkSpace.EventManager.FinalizarValidaciones
            AddHandler newController.EventManager.OnEnviarReproceso, AddressOf nWorkSpace.EventManager.EnviarReproceso
            AddHandler newController.EventManager.OnFinalizarReclasificar, AddressOf nWorkSpace.EventManager.FinalizarReclasificar
            AddHandler newController.EventManager.OnFinalizarRecorte, AddressOf nWorkSpace.EventManager.FinalizarRecorte
            AddHandler newController.EventManager.OnEliminarImagen, AddressOf nWorkSpace.EventManager.EliminarImagen
            AddHandler newController.EventManager.OnFinalizarProcesoAdicionalCaptura, AddressOf nWorkSpace.EventManager.FinalizarProcesoAdicionalCaptura
            AddHandler newController.EventManager.OnValidarSaveLabelCaptura, AddressOf nWorkSpace.EventManager.ValidarSaveLabelCaptura
            AddHandler newController.EventManager.OnFinalizarLoadConfig, AddressOf nWorkSpace.EventManager.FinalizarLoadConfig
            AddHandler newController.EventManager.OnFinalizarPrimeraCapturaAnexo, AddressOf nWorkSpace.EventManager.FinalizarPrimeraCapturaAnexo
            AddHandler newController.EventManager.OnFinalizarTerceraCapturaAnexo, AddressOf nWorkSpace.EventManager.FinalizarTerceraCapturaAnexo

            ' Inicializar el controlador
            newController.Inicializar(Program.AppPath & Program.TempPath, Program.Sesion, Program.DesktopGlobal, Program.ImagingGlobal)

            ' Llamar el delegado para configuración especial de data
            If (nSetDataMetod <> "") Then
                newController.GetType().InvokeMember(nSetDataMetod, BindingFlags.InvokeMethod, Nothing, newController, nParameters)
            End If

            If (_idDocumento IsNot Nothing AndAlso _idDocumento.Value = -1) Then
                If (newController.ShowDocumentosCaptura() = DialogResult.OK) Then
                    _idDocumento = newController.DocumentoCaptura
                Else
                    Return False
                End If
            End If

            If (newController.NextIndexingElement(_idOt, nEstado, _idDocumento)) Then
                Dim respuesta = newController.Indexar()

                If (respuesta <> DialogResult.Yes) Or (Not nDemanda) Then Return False
            Else
                MessageBox.Show("No se encontraron más elementos para procesar", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return False
            End If

        Catch ex As Exception
            Throw
        Finally
            ' Invalidar el proxy del controlador
            If (newController IsNot Nothing) Then
                ' Enlazar los eventos   
                RemoveHandler newController.EventManager.OnValidarPermiso, AddressOf nWorkSpace.EventManager.ValidarPermiso
                RemoveHandler newController.EventManager.OnFinalizarIndexacion, AddressOf nWorkSpace.EventManager.FinalizarIndexacion
                RemoveHandler newController.EventManager.OnFinalizarReIndexacion, AddressOf nWorkSpace.EventManager.FinalizarReIndexacion
                RemoveHandler newController.EventManager.OnValidarSavePrimeraCaptura, AddressOf nWorkSpace.EventManager.ValidarSavePrimeraCaptura
                RemoveHandler newController.EventManager.OnValidarSaveSegundaCaptura, AddressOf nWorkSpace.EventManager.ValidarSaveSegundaCaptura
                RemoveHandler newController.EventManager.OnValidarSaveTerceraCaptura, AddressOf nWorkSpace.EventManager.ValidarSaveTerceraCaptura
                RemoveHandler newController.EventManager.OnValidarSaveCalidad, AddressOf nWorkSpace.EventManager.ValidarSaveCalidad
                RemoveHandler newController.EventManager.OnFinalizarPreCaptura, AddressOf nWorkSpace.EventManager.FinalizarPreCaptura
                RemoveHandler newController.EventManager.OnFinalizarPrimeraCaptura, AddressOf nWorkSpace.EventManager.FinalizarPrimeraCaptura
                RemoveHandler newController.EventManager.OnFinalizarSegundaCaptura, AddressOf nWorkSpace.EventManager.FinalizarSegundaCaptura
                RemoveHandler newController.EventManager.OnFinalizarTerceraCaptura, AddressOf nWorkSpace.EventManager.FinalizarTerceraCaptura
                RemoveHandler newController.EventManager.OnFinalizarCalidad, AddressOf nWorkSpace.EventManager.FinalizarCalidad
                RemoveHandler newController.EventManager.OnFinalizarValidaciones, AddressOf nWorkSpace.EventManager.FinalizarValidaciones
                RemoveHandler newController.EventManager.OnEnviarReproceso, AddressOf nWorkSpace.EventManager.EnviarReproceso
                RemoveHandler newController.EventManager.OnFinalizarReclasificar, AddressOf nWorkSpace.EventManager.FinalizarReclasificar
                RemoveHandler newController.EventManager.OnFinalizarRecorte, AddressOf nWorkSpace.EventManager.FinalizarRecorte
                RemoveHandler newController.EventManager.OnEliminarImagen, AddressOf nWorkSpace.EventManager.EliminarImagen
                RemoveHandler newController.EventManager.OnFinalizarPrimeraCapturaAnexo, AddressOf nWorkSpace.EventManager.FinalizarPrimeraCapturaAnexo
                RemoveHandler newController.EventManager.OnFinalizarTerceraCapturaAnexo, AddressOf nWorkSpace.EventManager.FinalizarTerceraCapturaAnexo

            End If
        End Try

        Return True
    End Function

#End Region

#Region " Funciones "

    Public Shared Function ValidarAcceso(ByVal nEtapa As EtapaEnum) As Boolean
        'Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
        Dim respuesta As Boolean

        Dim accesosDataTable As DBImaging.SchemaConfig.TBL_AccesosDataTable = Nothing
        Try
            If Program.Sesion.IsExternal Then
                'Dim accesosDataTable = dbmImaging.SchemaConfig.TBL_Accesos.DBGet(Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede, Program.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento, Program.ImagingGlobal.ProyectoImagingRow.fk_Entidad, Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto)
                Dim queryResponseAccesosDataTable As QueryResponse = ClientUtil.resolver("[DB_Miharu.Imaging_Core].Config.TBL_Accesos", New List(Of QueryParameter) From {
                                                                 New QueryParameter With {.name = "fk_Entidad", .value = Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad.ToString()},
                                                                 New QueryParameter With {.name = "fk_Sede", .value = Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede.ToString()},
                                                                 New QueryParameter With {.name = "fk_Centro_Procesamiento", .value = Program.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento.ToString()},
                                                                 New QueryParameter With {.name = "fk_Entidad_Cliente", .value = Program.ImagingGlobal.ProyectoImagingRow.fk_Entidad.ToString()},
                                                                 New QueryParameter With {.name = "fk_Proyecto", .value = Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto.ToString()}
                                                             }, QueryRequestType.Table, QueryResponseType.Table)
                accesosDataTable = CType(ClientUtil.mapToTypedTable(New DBImaging.SchemaConfig.TBL_AccesosDataTable(), queryResponseAccesosDataTable.dataTable), DBImaging.SchemaConfig.TBL_AccesosDataTable)
            Else
                Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
                Try
                    dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                    dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                    accesosDataTable = dbmImaging.SchemaConfig.TBL_Accesos.DBGet(Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede, Program.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento, Program.ImagingGlobal.ProyectoImagingRow.fk_Entidad, Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto)
                Catch ex As Exception
                    Throw
                Finally
                    If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                End Try
            End If

            If (accesosDataTable.Count > 0) Then
                Dim accesosRow = accesosDataTable(0)

                Select Case nEtapa
                    Case EtapaEnum.Cargue
                        respuesta = accesosRow.Cargue
                    Case EtapaEnum.Indexacion
                        respuesta = accesosRow.Indexacion
                    Case EtapaEnum.PreCaptura
                        respuesta = accesosRow.Pre_Captura
                    Case EtapaEnum.PrimeraCaptura
                        respuesta = accesosRow.Primera_Captura
                    Case EtapaEnum.Reprocesos
                        respuesta = accesosRow.Reprocesos
                    Case EtapaEnum.SegundaCaptura
                        respuesta = accesosRow.Segunda_Captura
                    Case EtapaEnum.TerceraCaptura
                        respuesta = accesosRow.Tercera_Captura
                    Case EtapaEnum.ValidacionesOpcionales
                        respuesta = accesosRow.Validaciones_Opcionales
                    Case EtapaEnum.CalidadCaptura
                        respuesta = accesosRow.Calidad_Captura
                    Case EtapaEnum.Recorte
                        respuesta = accesosRow.Recorte
                    Case EtapaEnum.CalidadRecorte
                        respuesta = accesosRow.Calidad_Recorte
                    Case EtapaEnum.ValidacionListas
                        respuesta = accesosRow.Validacion_Listas
                    Case EtapaEnum.ProcesoAdicional
                        respuesta = accesosRow.Validacion_Listas
                    Case EtapaEnum.CorreccionMaquina
                        respuesta = accesosRow.Correccion_Maquina
                End Select

                If (Not respuesta) Then
                    MessageBox.Show("No hay autorización del coordinador para ingresar a esta opción", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
            Else
                respuesta = True
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)

            respuesta = False
        End Try

        Return respuesta
    End Function

    Private Shared Function CreateDommain() As AppDomain
        ' Crear las configuraciones del nuevo AppDomain.
        Dim ads = New AppDomainSetup()
        ads.ApplicationBase = Environment.CurrentDirectory
        ads.DisallowBindingRedirects = False
        ads.DisallowCodeDownload = True
        ads.ConfigurationFile = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile

        ' Crear el nuevo AppDomain.
        Return AppDomain.CreateDomain("Domain-" & Guid.NewGuid().ToString(), Nothing, ads)
    End Function

#End Region

End Class