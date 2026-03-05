Imports System.Windows.Forms
Imports Banagrario.Plugin.Firmas.Forms.Exportar
Imports Banagrario.Plugin.Firmas.Forms.CargueLog
Imports Banagrario.Plugin.Firmas.Reportes
Imports Banagrario.Plugin.Firmas.Forms.Empaque
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library.Plugins
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Config
Imports Banagrario.Plugin.Firmas
Imports System.Reflection

Namespace Firmas.FormWrappers

    Public Class FormImagingWorkSpaceWrapper_Firmas

#Region " Declaraciones "

        Private _eventManager As Miharu.Imaging.Library.Eventos.EventManager

        Public _plugin As FirmasImagingPlugin = Nothing

        Public _ExportarButton As Button = Nothing
        Public _ReporcesosButton As Button = Nothing

        Public WithEvents ConfiguracionPluginToolStripMenuItem As New ToolStripMenuItem()
        Public WithEvents CargueLogToolStripMenuItem As New ToolStripMenuItem()
        Public WithEvents ExcluidosCruceToolStripMenuItem As New ToolStripMenuItem()
        Public WithEvents ContingenciaToolStripMenuItem As New ToolStripMenuItem()
        Public WithEvents PreparacionDataToolStripMenuItem As New ToolStripMenuItem()
        Public WithEvents VerificarEmpaqueToolStripMenuItem As New ToolStripMenuItem()
        Public WithEvents EditCausaRechazoToolStripMenuItem As New ToolStripMenuItem()
        Public WithEvents ValidarTarjetaToolStripMenuItem As New ToolStripMenuItem()
        Public WithEvents IndexCreatorToolStripMenuItem As New ToolStripMenuItem()

#End Region

#Region " Propiedades "

        Public ReadOnly Property EventManager As Miharu.Imaging.Library.Eventos.EventManager
            Get
                Return Me._eventManager
            End Get
        End Property

#End Region

#Region " Constructores "

        Public Sub New(ByVal nPlugin As FirmasImagingPlugin)
            _plugin = nPlugin
        End Sub

#End Region

#Region " Eventos "

        Private Sub exportarButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim ExportarForm As New FormExportar(_plugin)
            ExportarForm.ShowDialog()
        End Sub

        'Se desmonta CC9491 modulo de reprocesos de Firmas desarrollo no finalizado.
        'Private Sub reprocesosButton_Click(ByVal sender As Object, ByVal e As EventArgs)

        '    If (Not _ReporcesosButton.Visible Or Not _ReporcesosButton.Enabled) Then Return
        '    If (Not Miharu.Imaging.Library.LaunchUtil.ValidarAcceso(Miharu.Imaging.Library.LaunchUtil.EtapaEnum.Reprocesos)) Then Return

        '    Try
        '        Dim seleccionOt = New Miharu.Imaging.Library.Procesos.OT.FormSeleccionarOTCaptura(DBCore.EstadoEnum.Reproceso)
        '        If seleccionOt.ShowDialog <> DialogResult.OK Then Return

        '        Dim formReprocesos = New Firmas.Forms.Reprocesos.FormReprocesos(_plugin)
        '        formReprocesos.OT = seleccionOt.OT
        '        formReprocesos.ShowDialog()
        '    Catch ex As Exception
        '        DesktopMessageBoxControl.DesktopMessageShow("Reprocesos", ex)
        '    End Try
        'End Sub

#End Region

#Region " Metodos "

        Public Sub AplicarCambios()
            Try

                'Menu de Configuraciones
                ConfiguracionPluginToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {CargueLogToolStripMenuItem, ExcluidosCruceToolStripMenuItem, ContingenciaToolStripMenuItem, PreparacionDataToolStripMenuItem, VerificarEmpaqueToolStripMenuItem, EditCausaRechazoToolStripMenuItem, ValidarTarjetaToolStripMenuItem, IndexCreatorToolStripMenuItem})
                ConfiguracionPluginToolStripMenuItem.Name = "ConfiguracionPluginToolStripMenuItem"
                ConfiguracionPluginToolStripMenuItem.Size = New Drawing.Size(193, 22)
                ConfiguracionPluginToolStripMenuItem.Text = "Proceso Firmas ..."
                ConfiguracionPluginToolStripMenuItem.Visible = _plugin.Manager.Sesion.Usuario.PerfilManager.PuedeAcceder(Permisos.Imaging.Firmas.Path)

                'Configuracion Matriz Documento
                CargueLogToolStripMenuItem.Name = "CargueLogToolStripMenuItem"
                CargueLogToolStripMenuItem.Size = New Drawing.Size(193, 22)
                CargueLogToolStripMenuItem.Text = "Cargue Log ..."
                CargueLogToolStripMenuItem.Visible = _plugin.Manager.Sesion.Usuario.PerfilManager.PuedeAcceder(Permisos.Imaging.Firmas.FirmasCoordinador.Cargue_Log)

                'Menú Transacciones Excluidas del cruce
                ExcluidosCruceToolStripMenuItem.Name = "ExcluidosCruceToolStripMenuItem"
                ExcluidosCruceToolStripMenuItem.Size = New Drawing.Size(193, 22)
                ExcluidosCruceToolStripMenuItem.Text = "Excluidos Cruce ..."
                ExcluidosCruceToolStripMenuItem.Visible = _plugin.Manager.Sesion.Usuario.PerfilManager.PuedeAcceder(Permisos.Imaging.Firmas.FirmasCoordinador.Excluidos_Cruce)

                'Menú separación de tarjetas excluidas para el empaque
                ContingenciaToolStripMenuItem.Name = "ContingenciaToolStripMenuItem"
                ContingenciaToolStripMenuItem.Size = New Drawing.Size(193, 22)
                ContingenciaToolStripMenuItem.Text = "Marcar Contingencias..."
                ContingenciaToolStripMenuItem.Visible = _plugin.Manager.Sesion.Usuario.PerfilManager.PuedeAcceder(Permisos.Imaging.Firmas.FirmasCoordinador.Marcar_Contigencias)

                'Menú Preparacion de Data
                PreparacionDataToolStripMenuItem.Name = "PreparacionDataToolStripMenuItem"
                PreparacionDataToolStripMenuItem.Size = New Drawing.Size(193, 22)
                PreparacionDataToolStripMenuItem.Text = "Preparar Data..."
                PreparacionDataToolStripMenuItem.Visible = _plugin.Manager.Sesion.Usuario.PerfilManager.PuedeAcceder(Permisos.Imaging.Firmas.FirmasCoordinador.Preparar_Data)

                'Menú de edición causales de rechazo
                EditCausaRechazoToolStripMenuItem.Name = "EditarCausalRechazoToolStripMenuItem"
                EditCausaRechazoToolStripMenuItem.Size = New Drawing.Size(193, 22)
                EditCausaRechazoToolStripMenuItem.Text = "Causales De Rechazo..."
                EditCausaRechazoToolStripMenuItem.Visible = _plugin.Manager.Sesion.Usuario.PerfilManager.PuedeAcceder(Permisos.Imaging.Firmas.FirmasCoordinador.Causales_Rechazo)

                'Menú separación de tarjetas excluidas para el empaque
                VerificarEmpaqueToolStripMenuItem.Name = "VerificarEmpaqueToolStripMenuItem"
                VerificarEmpaqueToolStripMenuItem.Size = New Drawing.Size(193, 22)
                VerificarEmpaqueToolStripMenuItem.Text = "Verificar Empaque..."
                VerificarEmpaqueToolStripMenuItem.Visible = _plugin.Manager.Sesion.Usuario.PerfilManager.PuedeAcceder(Permisos.Imaging.Firmas.FirmasFuncionario.Verificar_Empaque)

                'Menú separación de tarjetas excluidas para el empaque
                ValidarTarjetaToolStripMenuItem.Name = "ValidarTarjetaToolStripMenuItem"
                ValidarTarjetaToolStripMenuItem.Size = New Drawing.Size(193, 22)
                ValidarTarjetaToolStripMenuItem.Text = "Validar Tarjetas de Firmas..."
                ValidarTarjetaToolStripMenuItem.Visible = _plugin.Manager.Sesion.Usuario.PerfilManager.PuedeAcceder(Permisos.Imaging.Firmas.FirmasFuncionario.Validar_Tarjetas)

                'Menú creación de indice
                IndexCreatorToolStripMenuItem.Name = "IndexCreatorToolStripMenuItem"
                IndexCreatorToolStripMenuItem.Size = New Drawing.Size(193, 22)
                IndexCreatorToolStripMenuItem.Text = "Crear Archivo .DAT..."
                IndexCreatorToolStripMenuItem.Visible = _plugin.Manager.Sesion.Usuario.PerfilManager.PuedeAcceder(Permisos.Imaging.Firmas.FirmasFuncionario.Crear_Dat)


                _plugin.WorkSpace.MainMenuStrip.Items.AddRange(New ToolStripItem() {ConfiguracionPluginToolStripMenuItem})

                'Reemplazo exportacion
                _ExportarButton = PluginHelper.CloneButton(_plugin.WorkSpace.ExportarButton)
                PluginHelper.ReplaceControl(_plugin.WorkSpace.ExportarButton, _ExportarButton)
                _plugin.WorkSpace.ExportarButton.Visible = False
                AddHandler _ExportarButton.Click, AddressOf exportarButton_Click




                'Se desmonta CC9491 modulo de reprocesos de Firmas desarrollo no finalizado.
                'Reemplazo reproceso
                '_ReporcesosButton = PluginHelper.CloneButton(_plugin.WorkSpace.ReprocesosButton)
                'PluginHelper.ReplaceControl(_plugin.WorkSpace.ReprocesosButton, _ReporcesosButton)
                '_plugin.WorkSpace.ReprocesosButton.Visible = False
                'AddHandler _ReporcesosButton.Click, AddressOf reprocesosButton_Click

                'Reportes Plugin
                '_plugin.WorkSpace.WorkSpaceImagingReportViewerControl.ReportList.Add(New Report_DocumentosIlegibles(_plugin.WorkSpace.WorkSpaceImagingReportViewerControl, _plugin))
                _plugin.WorkSpace.WorkSpaceImagingReportViewerControl.ReportList.Add(New InformeFaltantes.Report_Faltantes(_plugin.WorkSpace.WorkSpaceImagingReportViewerControl, _plugin))
                _plugin.WorkSpace.WorkSpaceImagingReportViewerControl.ReportList.Add(New InformeSobrantes.Report_Sobrantes(_plugin.WorkSpace.WorkSpaceImagingReportViewerControl, _plugin))
                _plugin.WorkSpace.WorkSpaceImagingReportViewerControl.ReportList.Add(New InformesProcesados.Report_Procesados(_plugin.WorkSpace.WorkSpaceImagingReportViewerControl, _plugin))
                _plugin.WorkSpace.WorkSpaceImagingReportViewerControl.ReportList.Add(New InformeRechazados.Report_Rechazados(_plugin.WorkSpace.WorkSpaceImagingReportViewerControl, _plugin))
                _plugin.WorkSpace.WorkSpaceImagingReportViewerControl.ReportList.Add(New InformeResumenCargue.Report_Resumen_Cargue(_plugin.WorkSpace.WorkSpaceImagingReportViewerControl, _plugin))

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("No fue posible aplicar los cambios de Banagrario Firmas al workspace, " + ex.Message, "Plugin workspace", DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
            End Try
        End Sub

        Public Sub DeshacerCambios()

        End Sub

        Private Sub CargueLogToolStripMenuItem_Click(sender As System.Object, e As EventArgs) Handles CargueLogToolStripMenuItem.Click
            Dim CargueLogForm As New FormCargueLog(_plugin)
            CargueLogForm.ShowDialog()
        End Sub

        Private Sub ExcluidosCruceToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ExcluidosCruceToolStripMenuItem.Click
            Dim ExluidosCruceForm As New FormExcluidoCruce(_plugin)
            ExluidosCruceForm.ShowDialog()
        End Sub

        Private Sub PreparacionDataToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles PreparacionDataToolStripMenuItem.Click
            Dim PrepararDataForm As New Forms.PrepararData.FormPrepararData(_plugin)
            PrepararDataForm.ShowDialog()
        End Sub

        Private Sub VerificarEmpaqueToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles VerificarEmpaqueToolStripMenuItem.Click
            Dim VerificarEmpaqueForm As New FormVerificarEmpaque(_plugin)
            VerificarEmpaqueForm.ShowDialog()
        End Sub

        Private Sub EditarCausalRechazoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles EditCausaRechazoToolStripMenuItem.Click
            Dim EditarCausalRechazoToolStripMenuItem As New Forms.Destape.FormCausalRechazo(_plugin)
            EditarCausalRechazoToolStripMenuItem.ShowDialog()
        End Sub

        Private Sub ValidarTarjetaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ValidarTarjetaToolStripMenuItem.Click
            Dim ValidarTarjetasForm As New Forms.Destape.FormValidarTarjetas(_plugin)
            ValidarTarjetasForm.ShowDialog()
        End Sub

        Private Sub ContingenciaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ContingenciaToolStripMenuItem.Click
            Dim ContingenciaForm As New Forms.CargueLog.FormContingencia(_plugin)
            ContingenciaForm.ShowDialog()
        End Sub

        Private Sub IndexCreatorToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles IndexCreatorToolStripMenuItem.Click
            Dim IndexGeneratorForm As New Forms.Indices.FormIndexGenerator(_plugin)
            IndexGeneratorForm.ShowDialog()
        End Sub
#End Region

#Region " Funciones"


#End Region

    End Class

End Namespace


Public Class LaunchUtil_Firmas



#Region " Declaraciones "

    Private Shared _ejecutando As Boolean = False
    Private Shared _idDocumento As Slyg.Tools.SlygNullable(Of Integer)
    Private Shared _idOt As Integer

    Public Shared _plugin As FirmasImagingPlugin = Nothing

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
    End Enum

#End Region

#Region "Constructores"
    Sub New(nplugin As FirmasImagingPlugin)
        _plugin = nplugin
    End Sub

#End Region

#Region " Metodos "

    Public Sub Indexar(ot As Slyg.Tools.SlygNullable(Of Integer), ByVal nController As Type, ByVal nEstado As DBCore.EstadoEnum, ByVal nAcceso As EtapaEnum, ByRef nWorkSpace As Miharu.Imaging.Library.FormImagingWorkSpace, ByRef nButton As Button, ByVal nDemanda As Boolean)
        Indexar(ot, nController, nEstado, nAcceso, nWorkSpace, nButton, nDemanda, "", Nothing)
    End Sub

    Public Sub Indexar(ot As Slyg.Tools.SlygNullable(Of Integer), ByVal nController As Type, ByVal nEstado As DBCore.EstadoEnum, ByVal nAcceso As EtapaEnum, ByRef nWorkSpace As Miharu.Imaging.Library.FormImagingWorkSpace, ByRef nButton As Button, ByVal nDemanda As Boolean, ByVal nSetDataMetod As String, ByVal nParameters As Object())


        Try
            If (Not nButton.Visible Or Not nButton.Enabled) Then Return
            If (Not ValidarAcceso(nAcceso)) Then Return

            nWorkSpace.Cursor = Cursors.AppStarting
            Application.DoEvents()

            If (ot Is Nothing) Then
                Dim seleccionOt As Miharu.Imaging.Library.Procesos.OT.FormSeleccionarOT

                If (nEstado = DBCore.EstadoEnum.Indexado) Then
                    seleccionOt = New Miharu.Imaging.Library.Procesos.OT.FormSeleccionarOTValidacion()
                ElseIf (nEstado = DBCore.EstadoEnum.Indexacion) Then
                    seleccionOt = New Miharu.Imaging.Library.Procesos.OT.FormSeleccionarOTIndexacion()
                ElseIf (nEstado = DBCore.EstadoEnum.Proceso_Adicional) Then
                    seleccionOt = New Miharu.Imaging.Library.Procesos.OT.FormSeleccionarOTValidacionListas()
                Else
                    seleccionOt = New Miharu.Imaging.Library.Procesos.OT.FormSeleccionarOTCaptura(nEstado)
                End If

                If seleccionOt.ShowDialog <> DialogResult.OK Then Return
                _idOt = seleccionOt.OT
            Else
                _idOt = ot
            End If

            Do
                Dim newDomain As AppDomain = Nothing
                Dim newController As Miharu.Imaging.Indexer.Controller.IController = Nothing

                Try
                    ' Crear el objeto en el nuevo dominio
                    'If ((Program.ImagingGlobal.UsaDominioExterno)) Then
                    If ((_plugin.Manager.ImagingGlobal.ProyectoImagingRow.Usa_Dominio_Externo)) Then
                        newDomain = CreateDommain()
                    Else
                        newController = CType(Activator.CreateInstance(nController), Miharu.Imaging.Indexer.Controller.IController)
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

    Private Shared Function LaunchIndexar(ByRef newController As Miharu.Imaging.Indexer.Controller.IController, ByRef nWorkSpace As Miharu.Imaging.Library.FormImagingWorkSpace, ByVal nSetDataMetod As String, ByVal nParameters As Object(), ByVal nEstado As DBCore.EstadoEnum, ByVal nDemanda As Boolean) As Boolean
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

            ' Inicializar el controlador
            newController.Inicializar(Program.AppPath & Program.TempPath, _plugin.Manager.Sesion, _plugin.Manager.DesktopGlobal, _plugin.Manager.ImagingGlobal)

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

            End If
        End Try

        Return True
    End Function

#End Region

#Region " Funciones "

    Public Shared Function ValidarAcceso(ByVal nEtapa As EtapaEnum) As Boolean
        Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
        Dim respuesta As Boolean

        Try
            dbmImaging = New DBImaging.DBImagingDataBaseManager(_plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)
            dbmImaging.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

            Dim accesosDataTable = dbmImaging.SchemaConfig.TBL_Accesos.DBGet(_plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, _plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Sede, _plugin.Manager.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento, _plugin.Manager.ImagingGlobal.ProyectoImagingRow.fk_Entidad, _plugin.Manager.ImagingGlobal.ProyectoImagingRow.fk_Proyecto)

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
        Finally
            If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
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