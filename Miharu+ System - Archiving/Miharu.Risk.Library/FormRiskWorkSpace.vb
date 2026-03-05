Imports System.Windows.Forms
Imports Miharu.Desktop.Controls
Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Library.Forms
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Plugins
Imports DMB = Miharu.Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl
Imports System.Text

Public Class FormRiskWorkSpace
    Inherits FormBase
    Implements IWorkspace

#Region " Declaraciones "

    'Esquema Búsqueda
    Private _ProcessLibrary As ProcessLibrary
    Private _xsdBusqueda As New xsdBusqueda
    Public _ToolsConnectionString As String

#End Region

#Region " Implementación IWorkspace"

    Public Property ProcessLibrary As IProcessLibrary Implements IWorkspace.ProcessLibrary
        Get
            Return _ProcessLibrary
        End Get
        Set(ByVal value As IProcessLibrary)
            _ProcessLibrary = CType(value, ProcessLibrary)
        End Set
    End Property

    Public Sub ConfigWorkspace() Implements IWorkspace.ConfigWorkspace

        Dim dmArchiving As New DBArchiving.DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
        Try
            dmArchiving.Connection_Open(Program.Sesion.Usuario.id)
            DBArchiving.DBArchivingDataBaseManager.IdentifierDateFormat = Program.DesktopGlobal.IdentifierDateFormat
            'Se carga el título
            Me.Text = "Entidad: " & Program.RiskGlobal.ProyectoRow.Nombre_Entidad_Responsable & ", Proyecto: " & Program.RiskGlobal.ProyectoRow.Nombre_Proyecto & ""
            lblEntidad.Text = Program.RiskGlobal.ProyectoRow.Nombre_Entidad_Responsable
            lblProyecto.Text = Program.RiskGlobal.ProyectoRow.Nombre_Proyecto

            'SE HABILITAN CONTROLES SEGUN PERFIL USUARIO


            'Proceso
            If Not Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Risk.Proceso.Path) Then MainTabControl.TabPages.Remove(ProcesoTabPage)

            'CargueDecevalButton.Enabled = Program.RiskGlobal.ProyectoRow.Usa_Proyecto_Deceval
            cargueUniversalButton.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Risk.Proceso.Cargue.Universal)
            cargueParcialButton.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Risk.Proceso.Cargue.Parcial)
            actualizarButton.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Risk.Proceso.Cargue.Actualizacion)

            otButton.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Risk.Proceso.OT)
            recepcionButton.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Risk.Proceso.Recepcion)
            actualizarButton.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Risk.Proceso.Actualizar)
            destapeButton.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Risk.Proceso.Destape)
            insercionesButton.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Risk.Proceso.Inserciones)
            empaqueButton.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Risk.Proceso.Empaque)
            ButtonCancelados.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Risk.Proceso.Empaque)
            EmpaqueEndososButton.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Risk.Proceso.Empaque)

            If (Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Risk.Proceso.Centro_Distribucion) And Program.RiskGlobal.UsaCentroDistribucion) Then
                centroDistribucionButton.Enabled = True
                CentroDistribucionMesaButton.Enabled = True
            End If
            devolucionesButton.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Risk.Proceso.Devoluciones)

            'Mesa control Fisicos
            If Not Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Risk.Mesa_Control.Path) Then MainTabControl.TabPages.Remove(FisicosTabPage)
            mesaControlFisicoButton.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Risk.Mesa_Control.Path)
            FisicoPrimeraCapturaButton.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Risk.Mesa_Control.Primera_Captura)
            FisicoSegundaCapturaButton.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Risk.Mesa_Control.Segunda_Captura)
            FisicoTerceraCapturaButton.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Risk.Mesa_Control.Tercera_Captura)

            'Busqueda
            If Not Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Risk.Busqueda) Then MainTabControl.TabPages.Remove(BusquedaTabPage)

            'Reportes
            ReportesToolStripMenuItem.Visible = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Risk.Reportes)
            If Not Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Risk.Reportes) Then MainTabControl.TabPages.Remove(InformesTabPage)

            'Parametrizacion
            configuracionToolStripMenuItem.Visible = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Risk.Configuracion)

            'Estructuras
            EstrucutrasDeCargueToolStripMenuItem.Visible = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Risk.Estructura)

            'Menu de Codigos de barras
            CodigosDeBarrasToolStripMenuItem1.Visible = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Risk.Mesa_Control.Path)

            'Correción de Datos
            RemoveFolderButton.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Risk.Eliminacion)
            RemoveFileButton.Enabled = RemoveFolderButton.Enabled
            EditDataButton.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Risk.Correccion_Data)
            FoldersDataGridView.AllowUserToDeleteRows = RemoveFolderButton.Enabled
            FilesDataGridView.AllowUserToDeleteRows = RemoveFolderButton.Enabled

            'Custodia
            custodiaButton.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeAcceder(Permisos.Custody)

            'Valida si la entidad es DECEVAL para habilitar el menú -- Oswaldo Ibarra -- 04/02/2020
            If Program.RiskGlobal.ProyectoRow.Usa_Proyecto_Deceval = False Then MainTabControl.TabPages.Remove(ProyectoDecevalTabPage)

            'Procesos Especiales
            ProcesosEspecialesToolStripMenuItem.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeAcceder(Permisos.Proceso_Especiales.Path)
            ValidacionesDinámicasToolStripMenuItem.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeAcceder(Permisos.Proceso_Especiales.Validaciones_Dinamicas)

            WorkSpaceRiskReportViewerControl.ReportList.Clear()
            WorkSpaceRiskReportViewerControl.ReportList.Add(New Reportes.VisorReportes.Genericos.Report_Estados_Operacion_Launch(WorkSpaceRiskReportViewerControl))
            WorkSpaceRiskReportViewerControl.ReportList.Add(New Reportes.VisorReportes.Destape.OTs(WorkSpaceRiskReportViewerControl))
            WorkSpaceRiskReportViewerControl.ReportList.Add(New Reportes.VisorReportes.MesaControlFisicos.EstadosOT(WorkSpaceRiskReportViewerControl))
            WorkSpaceRiskReportViewerControl.ReportList.Add(New Reportes.VisorReportes.MesaControlFisicos.Reproceso(WorkSpaceRiskReportViewerControl))
            WorkSpaceRiskReportViewerControl.ReportList.Add(New Reportes.VisorReportes.ParametrizacionProyectos.Report_ParametrizacionProyectos(WorkSpaceRiskReportViewerControl))
            WorkSpaceRiskReportViewerControl.ReportList.Add(New Reportes.VisorReportes.DestapeOT.Report_DestapeOT(WorkSpaceRiskReportViewerControl))
            WorkSpaceRiskReportViewerControl.ReportList.Add(New Reportes.VisorReportes.ResumenPrecintosOT.Report_ResumenPrecintosOT(WorkSpaceRiskReportViewerControl))
            WorkSpaceRiskReportViewerControl.ReportList.Add(New Reportes.VisorReportes.ResumenOT.Report_ResumenOT(WorkSpaceRiskReportViewerControl))
            WorkSpaceRiskReportViewerControl.ReportList.Add(New Reportes.VisorReportes.SeguimientoOT.Report_SeguimientoOT(WorkSpaceRiskReportViewerControl))
            WorkSpaceRiskReportViewerControl.ReportList.Add(New Reportes.VisorReportes.Validaciones.Report_Validaciones(WorkSpaceRiskReportViewerControl))
            WorkSpaceRiskReportViewerControl.ReportList.Add(New Reportes.VisorReportes.DocumentosSinCustodia.Report_DocumentosSinCustodia(WorkSpaceRiskReportViewerControl))
            WorkSpaceRiskReportViewerControl.ReportList.Add(New Reportes.VisorReportes.ResumenCarpetas.Report_ResumenCarpetas(WorkSpaceRiskReportViewerControl))
            WorkSpaceRiskReportViewerControl.ReportList.Add(New Reportes.VisorReportes.ResumenDocumentos.Report_ResumenDocumentos(WorkSpaceRiskReportViewerControl))
            WorkSpaceRiskReportViewerControl.ReportList.Add(New Reportes.VisorReportes.ImagenesProcesadas.Report_ImagenesProcesadas(WorkSpaceRiskReportViewerControl))
            WorkSpaceRiskReportViewerControl.ReportList.Add(New Reportes.VisorReportes.ImagenesSinProcesar.Report_ImagenesSinProcesar(WorkSpaceRiskReportViewerControl))
            WorkSpaceRiskReportViewerControl.ReportList.Add(New Reportes.VisorReportes.EmpaqueNivelCaja.Report_EmpaqueNivelCaja(WorkSpaceRiskReportViewerControl))
            WorkSpaceRiskReportViewerControl.ReportList.Add(New Reportes.VisorReportes.EmpaqueCaja.Report_EmpaqueCaja(WorkSpaceRiskReportViewerControl))
            WorkSpaceRiskReportViewerControl.ReportList.Add(New Reportes.VisorReportes.UniversalvsParcial.Report_UniversalvsParcial(WorkSpaceRiskReportViewerControl))

            ConfigurarProyecto()
        Catch ex As Exception
            DMB.DesktopMessageShow("CargaPermisos", ex)
        End Try
        dmArchiving.Connection_Close()

    End Sub

#End Region

#Region " Eventos "

    Private Sub FormRiskWorkSpace_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        CargaDatosBusqueda()
        Me.WindowState = FormWindowState.Maximized

        'Cargar los plugins
        Try
            If (Not Program.PluginManager Is Nothing) Then
                Program.PluginManager.ClosePlugins()
            End If

            Program.PluginManager = New DesktopPluginManager(
                Program.AppPath.TrimEnd("\"c) + "\Plugins",
                Me,
                Program.Sesion)

            Program.PluginManager.RiskGlobal = Program.RiskGlobal
            Program.PluginManager.DesktopGlobal = Program.DesktopGlobal

            Program.PluginManager.LoadPlugins(ProcessLibraryType.Risk, Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto)

            Program.PluginManager.Activateplugins()
            Me.WorkSpaceRiskReportViewerControl.Load_Reports()
        Catch ex As Exception
            MessageBox.Show("Error al cargar los plugins, " + ex.Message)
        End Try
    End Sub

    'Accesos directos
    Private Sub FormMain_KeyDown(ByVal sender As System.Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown

        If e.KeyCode = Keys.F2 And e.Control Then
            If FisicoPrimeraCapturaButton.Enabled Then
                Dim formPistolearCarpeta As New Forms.MesaControlFisicos.FormPistolearCarpeta(DesktopConfig.TipoCaptura.Primera_Captura)
                formPistolearCarpeta.ShowDialog()
            End If
        ElseIf e.KeyCode = Keys.F3 And e.Control Then
            If FisicoSegundaCapturaButton.Enabled Then
                Dim formPistolearCarpeta As New Forms.MesaControlFisicos.FormPistolearCarpeta(DesktopConfig.TipoCaptura.Segunda_Captura)
                formPistolearCarpeta.ShowDialog()
            End If
        ElseIf e.KeyCode = Keys.F4 And e.Control Then
            If FisicoTerceraCapturaButton.Enabled Then
                Dim formPistolearCarpeta As New Forms.MesaControlFisicos.FormPistolearCarpeta(DesktopConfig.TipoCaptura.Tercera_Captura)
                formPistolearCarpeta.ShowDialog()
            End If

        ElseIf e.KeyCode = Keys.F2 Then
            If recepcionButton.Enabled Then SeleccionarRecepcion()
        ElseIf e.KeyCode = Keys.F3 Then
            If destapeButton.Enabled Then SeleccionarOT()
        ElseIf e.KeyCode = Keys.F4 Then
            If mesaControlFisicoButton.Enabled Then MainTabControl.SelectedIndex = 1
        ElseIf e.KeyCode = Keys.F5 Then
            If mesaControlImagenesButton.Enabled Then MainTabControl.SelectedIndex = 2
        ElseIf e.KeyCode = Keys.F6 Then
            If empaqueButton.Enabled Then
                Dim formseleccionacaja As New Forms.Empaque.FormSeleccionarCaja()
                formseleccionacaja.ShowDialog()
            End If
        ElseIf e.KeyCode = Keys.F7 Then
            If centroDistribucionButton.Enabled Then CentroDistribucion()
        ElseIf e.KeyCode = Keys.F8 Then
            If devolucionesButton.Enabled Then SeleccionarDevoluciones()
        ElseIf e.KeyCode = Keys.F9 Then
            If cerrarOTButton.Enabled Then CerrarOT()
        ElseIf e.KeyCode = Keys.F11 Then
            MainTabControl.SelectedIndex = MainTabControl.TabPages.Count - 2
        ElseIf e.KeyCode = Keys.F12 Then
            MainTabControl.SelectedIndex = MainTabControl.TabPages.Count - 1
        End If


    End Sub

    Private Sub EditDataButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles EditDataButton.Click
        Try

            Dim nFile = CStr(FilesDataGridView.SelectedRows(0).Cells("CBarras_File").Value)
            If nFile <> "" Then
                Dim EditaData As New Forms.CorreccionDatos.FormCorreccionData(nFile)
                EditaData.ShowDialog()
            Else
                DMB.DesktopMessageShow("Esta Operacion no se Puede Realizar con Registros Faltantes De Cargue Version 2.0", "Correccion Datos", DMB.IconEnum.WarningIcon, True)
            End If

        Catch : End Try
    End Sub

    Private Sub Empaque2Button_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles Empaque2Button.Click
        Dim formseleccionacaja As New Forms.Empaque.FormSeleccionarCaja()
        formseleccionacaja.ShowDialog()
    End Sub

    Private Sub OTButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles otButton.Click
        GenerarOT()
    End Sub

    Private Sub CerrarOTButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles cerrarOTButton.Click
        CerrarOT()
    End Sub

    Private Sub FormRiskWorkSpace_FormClosed(ByVal sender As Object, ByVal e As FormClosedEventArgs) Handles Me.FormClosed
        Me.ProcessLibrary.WorkSpace = Nothing
    End Sub

    Private Sub RecepcionButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles recepcionButton.Click
        SeleccionarRecepcion()
    End Sub

    Private Sub CargueDecevalButton_Click(sender As System.Object, e As System.EventArgs) Handles CargueDecevalButton.Click
        SeleccionarCargueDeceval()
    End Sub

    Private Sub CargueUniversalButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles cargueUniversalButton.Click
        SeleccionarCargueUniversal()
    End Sub

    Private Sub CargueParcialButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles cargueParcialButton.Click
        SeleccionarCargueParcial()
    End Sub

    Private Sub CarpetasYDocumentosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CarpetasYDocumentosToolStripMenuItem.Click
        Dim formImprimirCbarrasFIleFolder As New Forms.CBarras.FormCBarrasFolderFile()
        formImprimirCbarrasFIleFolder.ShowDialog()
    End Sub

    Private Sub ValidacionesDinámicasToolStripMenuItem_Click(sender As Object, e As System.EventArgs) Handles ValidacionesDinámicasToolStripMenuItem.Click
        Dim FormValidacionesDinamicas As New Forms.ValidacionesDinamicas.FormValidacionesDinamicas()
        FormValidacionesDinamicas.ShowDialog()
    End Sub

    Private Sub CajasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CajasToolStripMenuItem.Click
        Dim formImprimirCbarrascaja As New Forms.CBarras.FormCBarrasCajas()
        formImprimirCbarrascaja.ShowDialog()
    End Sub

    Private Sub ActualizarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles actualizarButton.Click
        SeleccionarCargueActualizacion()
    End Sub

    Private Sub DestapeButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles destapeButton.Click
        SeleccionarOT()
    End Sub

    Private Sub DocumentosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles DocumentosToolStripMenuItem.Click
        Dim Documento As New Forms.Parametrizacion.Risk.FormParDocumentos()
        Documento.ShowDialog()
    End Sub

    Private Sub CamposToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CamposToolStripMenuItem.Click
        Dim Campo As New Forms.Parametrizacion.Risk.FormParCampos()
        Campo.ShowDialog()
    End Sub

    Private Sub EsquemasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles EsquemasToolStripMenuItem.Click
        Dim esquemaForm As New Forms.Parametrizacion.Risk.FormParEsquema()
        esquemaForm.ShowDialog()
    End Sub

    Private Sub CargueToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CargueToolStripMenuItem.Click
        Dim Estructura As New Forms.Parametrizacion.Risk.FormEstructuraCargue()
        Estructura.ShowDialog()
    End Sub

    Private Sub ActualizaciónToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ActualizaciónToolStripMenuItem.Click
        Dim EstructuraActualizacion As New Forms.Parametrizacion.Risk.FormEstructuraCargueActualizacion()
        EstructuraActualizacion.ShowDialog()
    End Sub

    'Private Sub EstructurasCargueToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs)Handles EstructurasCargueToolStripMenuItem
    '    Dim Estructura As New FormEstructuraCargue()
    '    Estructura.ShowDialog()
    'End Sub

    'Private Sub EstreucturasActualizaciónToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs)
    '    Dim EstructuraActualizacion As New FormEstructuraCargueActualizacion()
    '    EstructuraActualizacion.ShowDialog()
    'End Sub

    Private Sub OTsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles OTsToolStripMenuItem.Click
        Dim ReporteOt As New Reportes.VisorReportes.Destape.FormReporteOTs()
        ReporteOt.ShowDialog()
    End Sub

    Private Sub MesaControlFisicoButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles mesaControlFisicoButton.Click
        MainTabControl.SelectedIndex = 1
    End Sub

    Private Sub MesaControlImagenesButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles mesaControlImagenesButton.Click
        MainTabControl.SelectedIndex = 2
    End Sub

    Private Sub FisicoPrimeraCapturaButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles FisicoPrimeraCapturaButton.Click
        Dim formPistolearCarpeta As New Forms.MesaControlFisicos.FormPistolearCarpeta(DesktopConfig.TipoCaptura.Primera_Captura)
        formPistolearCarpeta.ShowDialog()
    End Sub

    Private Sub FisicoSegundaCapturaButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles FisicoSegundaCapturaButton.Click
        Dim formPistolearCarpeta As New Forms.MesaControlFisicos.FormPistolearCarpeta(DesktopConfig.TipoCaptura.Segunda_Captura)
        formPistolearCarpeta.ShowDialog()
    End Sub

    Private Sub FisicoTerceraCapturaButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles FisicoTerceraCapturaButton.Click
        Dim formPistolearCarpeta As New Forms.MesaControlFisicos.FormPistolearCarpeta(DesktopConfig.TipoCaptura.Tercera_Captura)
        formPistolearCarpeta.ShowDialog()
    End Sub

    Private Sub ReprocesosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ReprocesosToolStripMenuItem.Click
        Dim Reporte As New Forms.Reportes.MesaControlFisicos.FormReporteReprocesos()
        Reporte.ShowDialog()
    End Sub

    Private Sub EstadosOTToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles EstadosOTToolStripMenuItem.Click
        Dim Reporte As New Forms.Reportes.MesaControlFisicos.FormEstadoOT()
        Reporte.ShowDialog()
    End Sub

    Private Sub DevolucionesButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles devolucionesButton.Click
        SeleccionarDevoluciones()
    End Sub

    Private Sub DevolucionesMCButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles DevolucionesMCButton.Click
        SeleccionarDevoluciones()
    End Sub

    Private Sub UniversalVSParcialToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles UniversalVSParcialToolStripMenuItem.Click
        Dim FormReporte As New Forms.Reportes.Destape.FormReporteUniversalVsParcial()
        FormReporte.ShowDialog()
    End Sub

    Private Sub InsercionesButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles insercionesButton.Click
        Inserciones()
    End Sub

    Private Sub ProcesamientoLightToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ProcesamientoLightToolStripMenuItem.Click
        Dim FormCBarras As New Forms.ProcesamientoLight.Form_ImpresionCBarras_PL()
        FormCBarras.ShowDialog()
    End Sub


    Private Sub EmpaqueButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles empaqueButton.Click
        Dim formseleccionacaja As New Forms.Empaque.FormSeleccionarCaja()
        formseleccionacaja.ShowDialog()
    End Sub


    Private Sub CentroDistribucionButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles centroDistribucionButton.Click
        CentroDistribucion()
    End Sub

    Private Sub CentroDistribucionMesaButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CentroDistribucionMesaButton.Click
        CentroDistribucion()
    End Sub

    'Búsqueda
    Private Sub CampoRadioButton_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles CampoRadioButton.CheckedChanged
        CambiarTipoBusqueda()
    End Sub

    Private Sub CBarrasRadioButton_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles CBarrasRadioButton.CheckedChanged
        CambiarTipoBusqueda()
    End Sub

    Private Sub BuscarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BuscarButton.Click
        buscar()
    End Sub

    Private Sub FoldersDataGridView_RowEnter(ByVal sender As System.Object, ByVal e As DataGridViewCellEventArgs) Handles FoldersDataGridView.RowEnter
        Try
            Dim GrillaFolder = CType(sender, DataGridView)
            If GrillaFolder.SelectedRows.Count > 0 Then
                EstadosDataGridView.DataSource = Nothing
                Dim rowSelected = GrillaFolder.SelectedRows(0)
                If Not IsNothing(rowSelected.Cells("CBarras_Folder").Value) Then
                    CargaFiles(rowSelected.Cells("CBarras_Folder").Value.ToString())
                End If
            End If

        Catch : End Try
    End Sub

    Private Sub FoldersDataGridView_CellClick(ByVal sender As System.Object, ByVal e As DataGridViewCellEventArgs) Handles FoldersDataGridView.CellClick
        Try
            Dim GrillaFolder = CType(sender, DataGridView)
            If GrillaFolder.SelectedRows.Count > 0 Then
                Dim rowSelected = GrillaFolder.SelectedRows(0)
                CargaFiles(rowSelected.Cells("CBarras_Folder").Value.ToString())
            End If

        Catch : End Try
    End Sub

    Private Sub FilesDataGridView_RowEnter(ByVal sender As System.Object, ByVal e As DataGridViewCellEventArgs) Handles FilesDataGridView.RowEnter
        Try
            Dim GrillaFile = CType(sender, DataGridView)
            If GrillaFile.SelectedRows.Count > 0 Then
                Dim rowSelected = GrillaFile.SelectedRows(0)
                If Not IsNothing(rowSelected.Cells("CBarras_File").Value) Or Not IsNothing(rowSelected.Cells("fk_Log_cargue").Value) Or Not IsNothing(rowSelected.Cells("fk_Documento").Value) Then
                    CargaFilesData(rowSelected.Cells("CBarrasFolder").Value.ToString(), rowSelected.Cells("CBarras_File").Value.ToString(), rowSelected.Cells("fk_Documento").Value.ToString(), rowSelected.Cells("fk_Log_cargue").Value.ToString())
                End If
            End If
        Catch : End Try
    End Sub

    Private Sub FilesDataGridView_CellClick(ByVal sender As System.Object, ByVal e As DataGridViewCellEventArgs) Handles FilesDataGridView.CellClick
        Try
            Dim GrillaFile = CType(sender, DataGridView)
            If GrillaFile.SelectedRows.Count > 0 Then
                Dim rowSelected = GrillaFile.SelectedRows(0)
                CargaFilesData(rowSelected.Cells("CBarrasFolder").Value.ToString(), rowSelected.Cells("CBarras_File").Value.ToString(), rowSelected.Cells("fk_Documento").Value.ToString(), rowSelected.Cells("fk_Log_Cargue").Value.ToString())
            End If

        Catch : End Try
    End Sub

    Private Sub RemoveFolderButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles RemoveFolderButton.Click
        Try
            If FoldersDataGridView.SelectedRows.Count > 0 Then
                Dim row = FoldersDataGridView.SelectedRows(0)
                If DMB.DesktopMessageShow("¿Desea eliminar la carpeta [" & row.Cells("CBarras_Folder").Value.ToString() & "]?", "Eliminación de Carpeta", DMB.IconEnum.WarningIcon, False) = DialogResult.OK Then
                    If EliminarFolder(row.Cells("CBarras_Folder").Value.ToString()) Then
                        FoldersDataGridView.Rows.Remove(FoldersDataGridView.SelectedRows(0))
                    End If
                End If
            Else
                DMB.DesktopMessageShow("Debe seleccionar una carpeta para eliminarla.", "Eliminación de carpeta", DMB.IconEnum.WarningIcon, True)
            End If
        Catch : End Try
    End Sub

    Private Sub RemoveFileButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles RemoveFileButton.Click
        Try
            If FilesDataGridView.SelectedRows.Count > 0 Then
                Dim row = FilesDataGridView.SelectedRows(0)
                If DMB.DesktopMessageShow("¿Desea eliminar el documento [" & row.Cells("CBarras_File").Value.ToString() & "]?", "Eliminación de documento", DMB.IconEnum.WarningIcon, False) = DialogResult.OK Then
                    If EliminarFile(row.Cells("CBarras_File").Value.ToString()) Then
                        FilesDataGridView.Rows.Remove(FilesDataGridView.SelectedRows(0))
                    End If
                End If
            Else
                DMB.DesktopMessageShow("Debe seleccionar un documento para eliminarlo.", "Eliminación de documento", DMB.IconEnum.WarningIcon, True)
            End If
        Catch : End Try
    End Sub

    Private Sub FoldersDataGridView_CellDoubleClick(ByVal sender As System.Object, ByVal e As DataGridViewCellEventArgs) Handles FoldersDataGridView.CellDoubleClick
        Try
            MostrarHistorial(CType(sender, DesktopDataGridView.DesktopDataGridViewControl).SelectedRows(0).Cells(0).Value.ToString(), 0)
        Catch : End Try
    End Sub

    Private Sub FilesDataGridView_CellDoubleClick(ByVal sender As System.Object, ByVal e As DataGridViewCellEventArgs) Handles FilesDataGridView.CellDoubleClick
        Try
            MostrarHistorial(CType(sender, DesktopDataGridView.DesktopDataGridViewControl).SelectedRows(0).Cells(0).Value.ToString(), 1)
        Catch : End Try
    End Sub

    Private Sub CerrarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CerrarButton.Click
        DetallePanel.Visible = False
        DetalleDataGridView.DataSource = Nothing
    End Sub

    Private Sub CerraDataButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CerraDataButton.Click
        DataPanel.Visible = False
    End Sub

    Private Sub TablaAsociadaButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles TablaAsociadaButton.Click
        DataPanel.Visible = True
    End Sub

    Private Sub CambiaDocumentoButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CambiaDocumentoButton.Click
        Dim nFile = CStr(FilesDataGridView.SelectedRows(0).Cells("CBarras_File").Value)
        Dim Documento As New Forms.CorreccionDatos.FormCambiaDocumento(nFile)
        Documento.ShowDialog()
    End Sub

    Private Sub cbarrasDesktopCBarrasControl_KeyDown(sender As System.Object, e As KeyEventArgs) Handles cbarrasDesktopCBarrasControl.KeyDown
        If (e.KeyCode = Keys.Enter) Then
            BuscarButton.Focus()
        End If
    End Sub

    'Accesos Directos
    Private Sub FormRiskWorkSpace_KeyDown(ByVal sender As System.Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
        If ((e.KeyCode = Keys.U) AndAlso (e.Modifiers = (Keys.Control))) Then
            SeleccionarCargueUniversal()
        End If

        If ((e.KeyCode = Keys.P) AndAlso (e.Modifiers = (Keys.Control))) Then
            SeleccionarCargueParcial()
        End If

        If ((e.KeyCode = Keys.D) AndAlso (e.Modifiers = (Keys.Control))) Then
            SeleccionarOT()
        End If

        If ((e.KeyCode = Keys.M) AndAlso (e.Modifiers = (Keys.Control))) Then
            MainTabControl.SelectedIndex = 1
        End If

        If ((e.KeyCode = Keys.W) AndAlso (e.Modifiers = (Keys.Control))) Then
            MainTabControl.SelectedIndex = 0
        End If

        If ((e.KeyCode = Keys.I) AndAlso (e.Modifiers = (Keys.Control))) Then
            MainTabControl.SelectedIndex = 2
        End If

        If ((e.KeyCode = Keys.R) AndAlso (e.Modifiers = (Keys.Control))) Then
            SeleccionarRecepcion()
        End If

        If ((e.KeyCode = Keys.E) AndAlso (e.Modifiers = (Keys.Control))) Then
            Dim formseleccionacaja As New Forms.Empaque.FormSeleccionarCaja()
            formseleccionacaja.ShowDialog()
        End If

        If ((e.KeyCode = Keys.C) AndAlso (e.Modifiers = (Keys.Control))) Then
            CentroDistribucion()
        End If

        If e.KeyCode = Keys.Escape Then
            If DetallePanel.Visible Then DetallePanel.Visible = False
            If DataPanel.Visible Then DataPanel.Visible = False
        End If
    End Sub

    Private Sub EmpaqueEndososButton_Click(sender As System.Object, e As EventArgs) Handles EmpaqueEndososButton.Click
        Dim formseleccionacaja As New Forms.Empaque.FormSeleccionarCaja()
        formseleccionacaja.Es_Empaque_Nuevo = True
        formseleccionacaja.ShowDialog()
    End Sub

#End Region

#Region " Metodos "

    Public Sub New()
        ' Llamada necesaria para el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().        
    End Sub

    Private Sub SeleccionarRecepcion()
        Try
            Dim formSeleccionarRecepcion = New Forms.Recepcion.FormRecepcionPrecintos()
            formSeleccionarRecepcion.ShowDialog()

        Catch ex As Exception
            DMB.DesktopMessageShow("SeleccionarRecepcion", ex)
        End Try
    End Sub

    Private Sub SeleccionarOT()
        Try
            Dim SeleccionarOT = New Forms.Destape.FormSeleccionarOT()

            'Si selecciona una Ot y da aceptar
            If SeleccionarOT.ShowDialog() = DialogResult.OK Then
                SeleccionarPrecinto()
            End If
        Catch ex As Exception
            DMB.DesktopMessageShow("SeleccionarOT", ex)
        End Try
    End Sub

    Private Sub CerrarOT()
        Dim SeleccionarPrecinto = New Forms.OT.FormCerrarOt()
        SeleccionarPrecinto.ShowDialog()
    End Sub

    Public Sub SeleccionarPrecinto()
        Dim SeleccionarPrecinto = New Forms.Destape.FormSeleccionarPrecinto()
        SeleccionarPrecinto.ShowDialog()
    End Sub

    Private Sub SeleccionarCargueDeceval()
        Dim formSeleccionarCargue = New Forms.CargueLog.FormCargueLog(DesktopConfig.TipoCargue.Deceval)
        formSeleccionarCargue.ShowDialog()
    End Sub

    Private Sub SeleccionarCargueUniversal()
        Dim formSeleccionarCargue = New Forms.Cargue.FormCargue(DesktopConfig.TipoCargue.Universal)
        formSeleccionarCargue.ShowDialog()
    End Sub

    Private Sub SeleccionarCargueParcial()
        Dim formSeleccionarCargue = New Forms.Cargue.FormCargue(DesktopConfig.TipoCargue.Parcial)
        formSeleccionarCargue.ShowDialog()
    End Sub

    Private Sub SeleccionarCargueActualizacion()
        Dim formSeleccionarCargue = New Forms.Cargue.FormCargue(DesktopConfig.TipoCargue.Actualizacion)
        formSeleccionarCargue.ShowDialog()
    End Sub

    Private Sub ConfigurarProyecto()
        Try
            'Se ocultan las opciones que no se manejan en el proyecto
            If Not Program.RiskGlobal.CargueUniversal Then
                cargueUniversalButton.Visible = False
                cargueUniversal_OTFlecha.Visible = False
            End If

            otButton.Visible = Not Program.RiskGlobal.CargueParcial
            cerrarOTButton.Visible = Not Program.RiskGlobal.CargueParcial
            cerrarOTButton.Enabled = Not Program.RiskGlobal.CargueParcial
            ot_CerrarOTFlecha.Visible = Not Program.RiskGlobal.CargueParcial
            cargueParcialButton.Visible = Program.RiskGlobal.CargueParcial

            cbarrasDesktopCBarrasControl.Init(Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, Program.DesktopGlobal.ConnectionStrings.Archiving)

        Catch ex As Exception
            DMB.DesktopMessageShow("ConfigurarProyecto", ex)
        End Try
    End Sub

    Private Sub GenerarOT()
        Dim OtForm As New Forms.OT.FormCrearOT
        OtForm.ShowDialog()
    End Sub

    Private Sub Inserciones()
        Dim f As New Forms.Inserciones.FormLlavesInsercion()

        f.ShowDialog()
    End Sub

    ' Imagenes
    'Private Sub CargueImagenes()
    '    Dim LineaProceso As Integer = 0
    '    Dim Respuesta As DialogResult

    '    If (Program.RiskGlobal.Usa_Mesa_Control_Imagenes) Then
    '        Dim formLineaProceso As New FormSeleccionarLineaProceso(DesktopConfig.TipoCaptura.Primera_Captura)
    '        Respuesta = formLineaProceso.ShowDialog()

    '        LineaProceso = formLineaProceso.LineaProceso
    '    Else
    '        Respuesta = DialogResult.OK
    '    End If

    '    If Respuesta = DialogResult.OK Then
    '        Dim NewCargue As New Cargue(LineaProceso)

    '        If NewCargue.Run() Then
    '            Dim CargueImagingForm As New FormImagingCargue()

    '            CargueImagingForm.setData(NewCargue, LineaProceso)
    '            CargueImagingForm.ShowDialog()

    '        End If
    '    End If
    'End Sub

    'Private Sub IndexarImagenes()
    '    Try
    '        CrearCarpeta(Program.AppPath & Program.TempPath)

    '        Do
    '            Dim CargueSelectorForm As New FormImagingCargueSelector(RiskIndexerController.EnumCaptura.Primera)
    '            Dim Respuesta As DialogResult

    '            Respuesta = CargueSelectorForm.ShowDialog()

    '            If Respuesta = DialogResult.OK Then
    '                Try
    '                    Dim Controlador As IIndexerController

    '                    Controlador = New RiskIndexerController(Program.RiskGlobal.Entidad, _
    '                                                            Program.RiskGlobal.Proyecto, _
    '                                                            Program.Sesion.Usuario.id, _
    '                                                            CargueSelectorForm.Cargue, _
    '                                                            Program.DesktopGlobal.ConnectionStrings.Core, _
    '                                                            Program.DesktopGlobal.ConnectionStrings.Imaging, _
    '                                                            Program.DesktopGlobal.ConnectionStrings.Archiving, _
    '                                                            Program.AppPath & Program.TempPath, _
    '                                                            RiskIndexerController.EnumCaptura.Primera, _
    '                                                           Program.RiskGlobal.ProyectoImagingRow.Output_Folder, _
    '                                                           Program.RiskGlobal.ProyectoImagingRow.fk_Formato_Salida, _
    '                                                           Program.RiskGlobal.ProyectoImagingRow.fk_Entidad_Servidor, _
    '                                                           Program.RiskGlobal.ProyectoImagingRow.fk_Servidor, _
    '                                                           Program.RiskGlobal.ProyectoImagingRow.fk_Servidor_Volumen)
    '                    Dim Procesados As Integer = 1

    '                    Do
    '                        If Controlador.NextIndexingElement() Then

    '                            Try
    '                                Respuesta = Controlador.Indexar()

    '                            Catch ex As Exception
    '                                Controlador.Dispose()
    '                                Throw ex
    '                            End Try

    '                            If Respuesta <> DialogResult.Yes Then
    '                                Exit Do
    '                            Else
    '                                Procesados += 1
    '                            End If
    '                        Else
    '                            MessageBox.Show("El cargue seleccionado no tiene más ítems pendientes por indexar", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Information)

    '                            Exit Do
    '                        End If
    '                    Loop

    '                    Controlador.Dispose()
    '                Catch ex As Exception
    '                    DMB.DesktopMessageShow("IndexarImagenes", ex)

    '                End Try
    '            Else
    '                Exit Do
    '            End If
    '        Loop
    '    Catch ex As Exception
    '        DMB.DesktopMessageShow("IndexarImagenes", ex)

    '    End Try
    'End Sub

    'Private Sub SegundaCapturaImagenes()
    '    Try
    '        CrearCarpeta(Program.AppPath & Program.TempPath)

    '        Do
    '            Dim CargueSelectorForm As New FormImagingCargueSelector(RiskIndexerController.EnumCaptura.Segunda)
    '            Dim Respuesta As DialogResult

    '            Respuesta = CargueSelectorForm.ShowDialog()

    '            If Respuesta = DialogResult.OK Then
    '                Try
    '                    Dim Controlador As IIndexerController

    '                    Controlador = New RiskIndexerController(Program.RiskGlobal.Entidad, _
    '                                                            Program.RiskGlobal.Proyecto, _
    '                                                            Program.Sesion.Usuario.id, _
    '                                                            CargueSelectorForm.Cargue, _
    '                                                            Program.DesktopGlobal.ConnectionStrings.Core, _
    '                                                            Program.DesktopGlobal.ConnectionStrings.Imaging, _
    '                                                            Program.DesktopGlobal.ConnectionStrings.Archiving, _
    '                                                            Program.AppPath & Program.TempPath, _
    '                                                            RiskIndexerController.EnumCaptura.Segunda, _
    '                                                           Program.RiskGlobal.ProyectoImagingRow.Output_Folder, _
    '                                                           Program.RiskGlobal.ProyectoImagingRow.fk_Formato_Salida, _
    '                                                           Program.RiskGlobal.ProyectoImagingRow.fk_Entidad_Servidor, _
    '                                                           Program.RiskGlobal.ProyectoImagingRow.fk_Servidor, _
    '                                                           Program.RiskGlobal.ProyectoImagingRow.fk_Servidor_Volumen)
    '                    Dim Procesados As Integer = 1

    '                    Do
    '                        If Controlador.NextIndexingElement() Then

    '                            Try
    '                                Respuesta = Controlador.Indexar()

    '                            Catch ex As Exception
    '                                Controlador.Dispose()
    '                                Throw ex
    '                            End Try

    '                            If Respuesta <> DialogResult.Yes Then
    '                                Exit Do
    '                            Else
    '                                Procesados += 1
    '                            End If
    '                        Else
    '                            MessageBox.Show("El cargue seleccionado no tiene más ítems pendientes por indexar", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Information)

    '                            Exit Do
    '                        End If
    '                    Loop

    '                    Controlador.Dispose()
    '                Catch ex As Exception
    '                    DMB.DesktopMessageShow("IndexarImagenes", ex)

    '                End Try
    '            Else
    '                Exit Do
    '            End If
    '        Loop
    '    Catch ex As Exception
    '        DMB.DesktopMessageShow("IndexarImagenes", ex)

    '    End Try
    'End Sub

    'Private Sub TerceraCapturaImagenes()
    '    Try
    '        CrearCarpeta(Program.AppPath & Program.TempPath)

    '        Do
    '            Dim CargueSelectorForm As New FormImagingCargueSelector(RiskIndexerController.EnumCaptura.Tercera)
    '            Dim Respuesta As DialogResult

    '            Respuesta = CargueSelectorForm.ShowDialog()

    '            If Respuesta = DialogResult.OK Then
    '                Try
    '                    Dim Controlador As IIndexerController

    '                    Controlador = New RiskIndexerController(Program.RiskGlobal.Entidad, _
    '                                                            Program.RiskGlobal.Proyecto, _
    '                                                            Program.Sesion.Usuario.id, _
    '                                                            CargueSelectorForm.Cargue, _
    '                                                            Program.DesktopGlobal.ConnectionStrings.Core, _
    '                                                            Program.DesktopGlobal.ConnectionStrings.Imaging, _
    '                                                            Program.DesktopGlobal.ConnectionStrings.Archiving, _
    '                                                            Program.AppPath & Program.TempPath, _
    '                                                            RiskIndexerController.EnumCaptura.Tercera, _
    '                                                           Program.RiskGlobal.ProyectoImagingRow.Output_Folder, _
    '                                                           Program.RiskGlobal.ProyectoImagingRow.fk_Formato_Salida, _
    '                                                           Program.RiskGlobal.ProyectoImagingRow.fk_Entidad_Servidor, _
    '                                                           Program.RiskGlobal.ProyectoImagingRow.fk_Servidor, _
    '                                                           Program.RiskGlobal.ProyectoImagingRow.fk_Servidor_Volumen)
    '                    Dim Procesados As Integer = 1

    '                    Do
    '                        If Controlador.NextIndexingElement() Then

    '                            Try
    '                                Respuesta = Controlador.Indexar()

    '                            Catch ex As Exception
    '                                Controlador.Dispose()
    '                                Throw ex
    '                            End Try

    '                            If Respuesta <> DialogResult.Yes Then
    '                                Exit Do
    '                            Else
    '                                Procesados += 1
    '                            End If
    '                        Else
    '                            MessageBox.Show("El cargue seleccionado no tiene más ítems pendientes por indexar", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Information)

    '                            Exit Do
    '                        End If
    '                    Loop

    '                    Controlador.Dispose()
    '                Catch ex As Exception
    '                    DMB.DesktopMessageShow("IndexarImagenes", ex)

    '                End Try
    '            Else
    '                Exit Do
    '            End If
    '        Loop
    '    Catch ex As Exception
    '        DMB.DesktopMessageShow("IndexarImagenes", ex)

    '    End Try
    'End Sub

    'Private Sub ReprocesarImagenes()
    '    Dim f As New FormImagingReprocesos()
    '    f.ShowDialog()
    'End Sub

    'Private Sub PublicarImagenes()

    'End Sub

    'Private Sub ExportarImagenes()
    '    Dim f As New FormExport()
    '    f.ShowDialog()
    'End Sub

    Private Sub CentroDistribucion()
        Dim FormRemisionCiudades As New Forms.CentroDistribucion.FormRemisionCiudades
        FormRemisionCiudades.ShowDialog()
        'Dim FormBandejaDistribucion As New Forms.CentroDistribucion.FormBandejaDistribucion
        'FormBandejaDistribucion.ShowDialog()
    End Sub

    Private Sub SeleccionarDevoluciones()
        Dim FormDevoluciones As New Forms.Devoluciones.FormDevoluciones()
        FormDevoluciones.ShowDialog()
    End Sub

    'Private Sub CrearCarpeta(ByVal NewPath As String)
    '    If (Not System.IO.Directory.Exists(NewPath)) Then
    '        System.IO.Directory.CreateDirectory(NewPath)
    '    End If
    'End Sub

    'Búsqueda

    Public Sub buscar()
        Try
            'Se crea el filtro de acuerdo al tipo de búsqueda.
            Dim arrCampo() As String
            Dim campoTipo As Byte = Nothing
            Dim idCampo As String = String.Empty
            Dim fkCampoBusqueda As Byte = Nothing

            Dim sFiltroValores As New StringBuilder
            Dim sFiltroLlaves As New StringBuilder
            Dim sFiltroCbarras As New StringBuilder

            'Valida las opciones de búsqueda
            If CampoRadioButton.Checked Then
                If IsNothing(CondicionComboBox.SelectedItem) Or ValorBusquedaTextBox.Text = "" Then
                    DMB.DesktopMessageShow("Se debe seleccionar una condición y un valor de búsqueda.", "Búsqueda incorrecta", DMB.IconEnum.WarningIcon, True)
                    Exit Sub
                End If
                arrCampo = CampoComboBox.SelectedValue.ToString().Split(CChar("-"))

                campoTipo = CByte(arrCampo(0))
                idCampo = arrCampo(1)
                fkCampoBusqueda = CByte(idCampo)

            ElseIf CBarrasRadioButton.Checked Then
                If cbarrasDesktopCBarrasControl.Text = "" Then
                    DMB.DesktopMessageShow("Se debe ingresar un código de barras.", "Búsqueda incorrecta", DMB.IconEnum.WarningIcon, True)
                    Exit Sub
                End If
            End If

            Dim condicion As String = ""
            If Not IsNothing(CondicionComboBox.SelectedItem) Then
                condicion = CondicionComboBox.SelectedItem.ToString()
            End If

            Dim valorBusqueda As String

            Dim Valor As String
            Dim Llave As String
            'Dim Campo As Object = Nothing

            If CampoRadioButton.Checked Then
                valorBusqueda = ValorBusquedaTextBox.Text
                sFiltroValores.Append(" WHERE ")

                Select Case campoTipo
                    Case DesktopConfig.CampoTipo.Fecha
                        Valor = "[Process].Fn_getFecha(Valor_File_Data) = " & "CONVERT(DATETIME, '" & Format(CDate(valorBusqueda), "yyyy-MM-dd") & " 23:59:00', 102)"
                        Llave = "[Process].Fn_getFecha(Valor_Llave) = " & "CONVERT(DATETIME, '" & Format(CDate(valorBusqueda), "yyyy-MM-dd") & " 23:59:00', 102)"

                    Case Else
                        If IsNumeric(valorBusqueda) Then
                            Valor = "Valor_File_Data " & condicion & " " & valorBusqueda
                        Else
                            Valor = "Valor_File_Data = '" & valorBusqueda & "'"
                        End If

                        Llave = "CAST(Valor_Llave AS VARCHAR(500)) = '" & valorBusqueda & "'"
                End Select

                sFiltroValores.Append(vbCrLf & "((fk_Campo_Tipo = " & campoTipo & ") AND (fk_Campo_Busqueda = " & idCampo & ") AND (" & Valor & "))")
                sFiltroLlaves.Append(" WHERE (" & Llave & ")")

            ElseIf CBarrasRadioButton.Checked Then
                valorBusqueda = cbarrasDesktopCBarrasControl.Text
                sFiltroCbarras.Append(valorBusqueda)
            End If

            CargaBusqueda(sFiltroValores.ToString(), sFiltroLlaves.ToString, sFiltroCbarras.ToString, campoTipo, fkCampoBusqueda)
        Catch ex As Exception
            DMB.DesktopMessageShow("BuscarButton_Click", ex)
        End Try
    End Sub

    Private Sub CambiarTipoBusqueda()
        If CampoRadioButton.Checked Then
            CampoPanel.Visible = True
            CBarrasPanel.Visible = False
        ElseIf CBarrasRadioButton.Checked Then
            CampoPanel.Visible = False
            CBarrasPanel.Visible = True
        End If
    End Sub

    Private Sub CargaDatosBusqueda()
        Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
        Try
            dbmCore.Connection_Open(Program.Sesion.Usuario.id)

            Dim CamposBusqueda = dbmCore.Schemadbo.PA_Campos_Busqueda_Rol_F11.DBExecute(Program.Sesion.Usuario.id, Program.RiskGlobal.Entidad)
            For Each row In CamposBusqueda
                Dim rowBusqueda = _xsdBusqueda.Tables("CamposBusqueda").NewRow()
                rowBusqueda("fk_Campo_Tipo") = row.fk_Campo_Tipo
                rowBusqueda("Id_Campo_Busqueda") = row.id_Campo_Busqueda
                rowBusqueda("Nombre_Campo_Busqueda") = row.Nombre_Campo_Busqueda
                rowBusqueda("Id") = row.fk_Campo_Tipo & "-" & row.id_Campo_Busqueda
                _xsdBusqueda.Tables("CamposBusqueda").Rows.Add(rowBusqueda)
            Next
            _xsdBusqueda.AcceptChanges()

            'Carga el combo de campos
            CampoComboBox.DataSource = _xsdBusqueda.CamposBusqueda
            CampoComboBox.ValueMember = _xsdBusqueda.CamposBusqueda.IdColumn.ColumnName
            CampoComboBox.DisplayMember = _xsdBusqueda.CamposBusqueda.Nombre_Campo_BusquedaColumn.ColumnName

            'Carga las condiciones
            CondicionComboBox.Items.Add("=")
            CondicionComboBox.Items.Add(">")
            CondicionComboBox.Items.Add("<")

        Catch ex As Exception
            DMB.DesktopMessageShow("CargaDatosBusqueda", ex)
        Finally
            dbmCore.Connection_Close()
        End Try
    End Sub

    Private Sub CargaBusqueda(ByVal SentenciaValor As String, ByVal SentenciaLlaves As String, ByVal SentenciaCBarras As String, ByVal fk_Campo_Tipo As Byte, ByVal fk_Campo_Busqueda As Byte)
        Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
        Try
            dbmCore.Connection_Open(Program.Sesion.Usuario.id)

            'Limpia las grillas de files y data
            FilesDataGridView.DataSource = Nothing
            EstadosDataGridView.DataSource = Nothing
            DataDataGridView.DataSource = Nothing

            'Carga datos de los folders
            Dim FoldersDataTable = dbmCore.Schemadbo.PA_Buscar_F11.DBExecute(CShort(Program.Sesion.Usuario.id), SentenciaCBarras, SentenciaValor, SentenciaLlaves, fk_Campo_Tipo, fk_Campo_Busqueda, Program.RiskGlobal.Entidad, ValorBusquedaTextBox.Text)

            If FoldersDataTable.Rows.Count > 0 Then
                FoldersDataTable = FoldersDataTable.DefaultView.ToTable(True, "CBarras_Folder", "Nombre_Entidad", "Nombre_Proyecto", "Nombre_Esquema", "Data_1", "Data_2", "Data_3", "CBarras_1", "CBarras_2", "CBarras_3", "Nombre_Estado", "id_Estado", "Llaves")
                FoldersDataGridView.AutoGenerateColumns = False
                FoldersDataGridView.DataSource = FoldersDataTable
                FoldersDataGridView.ClearSelection()
            Else
                DMB.DesktopMessageShow("No se encontraron regitros para la búsqueda realizada.", "Resultado de Búsqueda", DMB.IconEnum.WarningIcon, True)
                FoldersDataGridView.DataSource = Nothing
                FoldersDataGridView.ClearSelection()
            End If
        Catch ex As Exception
            DMB.DesktopMessageShow("CargaBusqueda", ex)
        Finally
            dbmCore.Connection_Close()
        End Try
    End Sub

    Public Sub CargaFiles(ByVal nCBarras_Folder As String)
        Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
        Try
            dbmCore.Connection_Open(Program.Sesion.Usuario.id)

            If FoldersDataGridView.SelectedRows.Count > 0 Then
                'Limpia las grillas de files y data
                FilesDataGridView.DataSource = Nothing
                DataDataGridView.DataSource = Nothing
                Dim llaves As String = Convert.ToString(FoldersDataGridView.CurrentRow.Cells("Llaves").Value)
                Dim FileDataTable = dbmCore.SchemaProcess.PA_Folder_File_Busqueda_F11.DBExecute(nCBarras_Folder, Program.Sesion.Usuario.id, llaves, Nothing).DefaultView.ToTable(True, "CBarras_File", "Monto_File", "Folios_File", "Nombre_Tipologia", "CBarras_Folder", "fk_Documento", "Fk_Log_Cargue")

                FilesDataGridView.AutoGenerateColumns = False
                FilesDataGridView.DataSource = FileDataTable
                'EstadosDataGridView.Columns("CBarras_File").Visible = False
                FilesDataGridView.ClearSelection()
            End If
        Catch ex As Exception
            DMB.DesktopMessageShow("CargaFiles", ex)
        Finally
            dbmCore.Connection_Close()
        End Try
    End Sub

    Public Sub CargaFilesData(ByVal nCBarras_Folder As String, ByVal nCBarras_File As String, ByVal fk_Documento As String, ByVal fk_Log_Cargue As String)

        Dim _fk_Log_Cargue As Long = Nothing
        Dim _fk_Documento As Integer = Nothing

        If fk_Documento.Length > 0 Then
            _fk_Documento = Integer.Parse(fk_Documento)
        End If
        If fk_Log_Cargue.Length > 0 Then
            _fk_Log_Cargue = Long.Parse(fk_Log_Cargue)
        End If


        Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
        Try
            dbmCore.Connection_Open(Program.Sesion.Usuario.id)

            'Limpia las grillas de data
            DataDataGridView.DataSource = Nothing
            EstadosDataGridView.DataSource = Nothing


            'Muestra los estados del file
            Dim localEstadoFile = dbmCore.SchemaProcess.PA_File_Estado_Modulo.DBExecute(nCBarras_Folder, nCBarras_File, _fk_Log_Cargue, _fk_Documento)
            EstadosDataGridView.AutoGenerateColumns = False
            EstadosDataGridView.DataSource = localEstadoFile
            EstadosDataGridView.ClearSelection()

            'Visualiza la data del documento
            Dim FileDataTable = dbmCore.Schemadbo.PA_File_Data_Busqueda_F11.DBExecute(nCBarras_File, Program.Sesion.Usuario.id, _fk_Log_Cargue)

            Dim view As New DataView(FileDataTable)
            view.Sort = "CONSULTA"
            view.RowFilter = "TIPO in (1,2)"
            DataDataGridView.AutoGenerateColumns = False
            DataDataGridView.DataSource = view.ToTable(True, "Valor_File_Data", "Nombre_Campo")
            DataDataGridView.ClearSelection()

            'Valida si contiene data asociada para generar visor.
            If FileDataTable.Count > 0 Then
                Dim rowData = FileDataTable(0)
                Dim TablaAsociada = dbmCore.Schemadbo.CTA_File_Data_Asociada.DBFindByfk_Expedientefk_Folderfk_Filefk_Documento(rowData.fk_Expediente, rowData.fk_Folder, rowData.id_File, rowData.id_Documento)
                If TablaAsociada.Count > 0 Then
                    Dim camposTablas As Integer = 0
                    'Columnas de la tabla asociada
                    Dim dtColumnas = TablaAsociada.DefaultView.ToTable(True, "fk_Campo_Tabla", "Nombre_Campo")
                    Dim tableData As New DataTable()

                    For Each columna As DataRow In dtColumnas.Rows
                        tableData.Columns.Add(columna("Nombre_Campo").ToString())
                        camposTablas += 1
                    Next

                    Dim contCol As Integer = 0
                    Dim fila As DataRow = Nothing
                    For Each row In TablaAsociada
                        If contCol = 0 Then
                            fila = tableData.NewRow()
                        End If

                        If contCol <= camposTablas - 1 Then
                            fila(contCol) = row.Valor_File_Data.ToString()
                            contCol += 1
                        End If

                        If contCol = camposTablas Then
                            contCol = 0
                            tableData.Rows.Add(fila)
                        End If
                    Next
                    DataAsociadaDataGridView.DataSource = tableData
                    TablaAsociadaButton.Visible = True
                Else
                    TablaAsociadaButton.Visible = False
                End If
            End If



        Catch ex As Exception
            DMB.DesktopMessageShow("CargaFilesData", ex)
        Finally
            dbmCore.Connection_Close()
        End Try
    End Sub

    Private Sub MostrarHistorial(ByVal nCBarras As String, ByVal Tipo As Integer)
        'Tipo:    0: Carpeta
        '         1: File

        Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
        Dim dbmArchiving As New DBArchiving.DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
        Try
            dbmCore.Connection_Open(Program.Sesion.Usuario.id)
            dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)
            Dim llaves As String = Convert.ToString(FoldersDataGridView.CurrentRow.Cells("Llaves").Value)
            DetalleDataGridView.AutoGenerateColumns = False
            Select Case Tipo
                Case 0
                    Dim order As New DBCore.Schemadbo.CTA_Folder_LogEnumList
                    order.Add(DBCore.Schemadbo.CTA_Folder_LogEnum.fecha_log, False)
                    DetalleDataGridView.DataSource = dbmCore.Schemadbo.PA_Historial_Folder_F11.DBExecute(nCBarras, llaves)
                    'DetalleDataGridView.DataSource = dbmCore.Schemadbo.CTA_Folder_Log.DBFindByCBarras(nCBarras, 0, order)
                Case 1
                    Dim order As New DBArchiving.Schemadbo.CTA_File_LogEnumList
                    order.Add(DBArchiving.Schemadbo.CTA_File_LogEnum.Fecha_Log, False)
                    Dim Documento = Convert.ToInt32(FilesDataGridView.CurrentRow.Cells("fk_Documento").Value)
                    DetalleDataGridView.DataSource = dbmCore.Schemadbo.PA_Historial_File_F11.DBExecute(llaves, Documento, nCBarras)
                    'DetalleDataGridView.DataSource = dbmArchiving.Schemadbo.CTA_File_Log.DBFindByCBarras(nCBarras, 0, order)
            End Select

            'Se limpia la caja custodia y proceso cuándo el estado es diferente a empacado.
            'For Each fila As DataGridViewRow In DetalleDataGridView.Rows
            '    If fila.Cells("Estado_Log").Value.ToString() <> DBCore.EstadoEnum.Empacado.ToString() And fila.Cells("Usuario").Value.ToString() <> "Estado Actual" Then
            '        fila.Cells("CajaCustodia").Value = ""
            '    End If

            '    If Utilities.ObtieneEstadobyNombre(fila.Cells("Estado_Log").Value.ToString()) >= DBCore.EstadoEnum.Empacado Then
            '        fila.Cells("CajaProceso").Value = ""
            '    End If
            'Next

            DetallePanel.Visible = True
        Catch ex As Exception
            DMB.DesktopMessageShow("MostrarHistorial", ex)
        Finally
            dbmArchiving.Connection_Close()
            dbmCore.Connection_Close()
        End Try
    End Sub

#End Region

#Region " Funciones "

    Public Function ConCargue() As Boolean
        Return Program.RiskGlobal.CargueParcial
    End Function

    Private Function EliminarFolder(ByVal nCBarras_Folder As String) As Boolean
        Dim dbmArchiving As New DBArchiving.DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
        Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
        Dim bReturn As Boolean = False
        Try
            dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)
            dbmCore.Connection_Open(Program.Sesion.Usuario.id)

            'Valida que el folder se pueda eliminar Estado<1000-Por custodiar
            Dim dtFolder = dbmArchiving.Schemadbo.CTA_Folder.DBFindByCBarras_Folder(nCBarras_Folder)(0)

            'Elimina el Folder en Archiving
            dbmArchiving.SchemaRisk.TBL_Folder.DBDelete(dtFolder.fk_Expediente, dtFolder.id_Folder, Nothing)

            'Elimina todos los estados del Folder en Core
            dbmCore.SchemaProcess.TBL_Folder_estado.DBDelete(dtFolder.fk_Expediente, dtFolder.id_Folder, DesktopConfig.Modulo.Archiving)

            'elimina el folder core cuando no existe ninigun estado en otro modulo
            Dim ExisteEstadofolder = dbmCore.SchemaProcess.TBL_Folder_estado.DBGet(dtFolder.fk_Expediente, dtFolder.id_Folder, Nothing)
            If ExisteEstadofolder.Count = 0 Then
                dbmCore.SchemaProcess.TBL_Expediente.DBDelete(dtFolder.fk_Expediente)
            End If

            bReturn = True

        Catch ex As Exception
            DMB.DesktopMessageShow("EliminarFolder", ex)
        Finally
            dbmCore.Connection_Close()
            dbmArchiving.Connection_Close()
        End Try
        Return bReturn
    End Function

    Private Function EliminarFile(ByVal nCBarras_File As String) As Boolean
        Dim dbmArchiving As New DBArchiving.DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
        Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
        Dim bReturn As Boolean = False
        Try
            dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)
            dbmCore.Connection_Open(Program.Sesion.Usuario.id)

            'Valida que el file se pueda eliminar Estado<1000-Por custodiar
            Dim dtFile = dbmArchiving.Schemadbo.CTA_File.DBFindByCBarras_File(nCBarras_File)(0)

            'Elimina el File en Archiving
            dbmArchiving.SchemaRisk.TBL_File.DBDelete(dtFile.fk_OT, dtFile.fk_Folder, dtFile.id_File, dtFile.fk_Expediente)

            'Elimina los estados del File en Core
            dbmCore.SchemaProcess.TBL_File_Estado.DBDelete(dtFile.fk_Expediente, dtFile.fk_Folder, dtFile.id_File, DesktopConfig.Modulo.Archiving)

            'Elimina el File si no existe ningun otro estado en otro modulo
            Dim ExisteEstadofile = dbmCore.SchemaProcess.TBL_File_Estado.DBGet(dtFile.fk_Expediente, dtFile.fk_Folder, dtFile.id_File, Nothing)
            If ExisteEstadofile.Count = 0 Then
                dbmCore.SchemaProcess.TBL_File.DBDelete(dtFile.fk_Expediente, dtFile.fk_Folder, dtFile.id_File)
            End If

            bReturn = True

        Catch ex As Exception
            DMB.DesktopMessageShow("EliminarFile", ex)
        Finally
            dbmCore.Connection_Close()
            dbmArchiving.Connection_Close()
        End Try
        Return bReturn
    End Function

#End Region



    Private Sub PrevalidarButton_Click(sender As System.Object, e As System.EventArgs) Handles PrevalidarButton.Click

    End Sub

    Private Sub ButtonCancelados_Click(sender As System.Object, e As System.EventArgs) Handles ButtonCancelados.Click
        Dim formseleccionacaja As New Forms.Empaque.FormSeleccionarCajaCancelados()
        formseleccionacaja.ShowDialog()
    End Sub

    Private Sub CargueLogToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs)
        SeleccionarCargueDeceval()
    End Sub

    Private Sub btnCargueLogDeceval_Click(sender As System.Object, e As System.EventArgs) Handles btnCargueLogDeceval.Click
        SeleccionarCargueDeceval()
    End Sub

    Private Sub btnMesaControlDeceval_Click(sender As System.Object, e As System.EventArgs) Handles btnMesaControlDeceval.Click
        Dim formPistolearCarpetaDeceval As New Forms.MesaControlFisicos.FormPistolearCarpetaDeceval(DesktopConfig.TipoCaptura.Primera_Captura)
        formPistolearCarpetaDeceval.ShowDialog()
    End Sub
End Class