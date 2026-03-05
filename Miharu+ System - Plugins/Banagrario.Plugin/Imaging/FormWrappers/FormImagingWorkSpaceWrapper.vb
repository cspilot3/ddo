Imports System.Windows.Forms
Imports Banagrario.Plugin.Imaging.Controls
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library.Plugins
Imports Slyg.Tools
Imports Slyg.Tools.CSV
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Config

Namespace Imaging.FormWrappers

    Public Class FormImagingWorkSpaceWrapper

#Region " Declaraciones "

        Public Plugin As BanagrarioImagingPlugin = Nothing

        Private _banAgrarioTabPage As TabPage
        Private _correccionesTabPage As TabPage
        Private _cierreButton As Button
        Private _correccionCargueButton As Button
        Private _exportarImagenesButton As Button
        Private _seguimientoCanjeButton As Button
        Private _cargueButton As Button = Nothing

        Public WithEvents ConfiguracionBancoAgrarioToolStripMenuItem As New ToolStripMenuItem()
        Public WithEvents MatrizDocumentoToolStripMenuItem As New ToolStripMenuItem()
        Public WithEvents TipologiaCodigoTxToolStripMenuItem As New ToolStripMenuItem()
        Public WithEvents NovedadesTxToolStripMenuItem As New ToolStripMenuItem()
        Public WithEvents MesaDestapeTxToolStripMenuItem As New ToolStripMenuItem()
        Public WithEvents CambiarTipologiaToolStripMenuItem As New ToolStripMenuItem()

#End Region

#Region " Constructores "

        Public Sub New(ByVal nPlugin As BanagrarioImagingPlugin)
            Plugin = nPlugin
        End Sub

#End Region

#Region " Eventos "

        Private Sub CierreButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            Try
                Dim cierrePlugin As New Forms.Cierre.FormCierre(Me.Plugin)
                cierrePlugin.ShowDialog()

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Cierre", ex)
            End Try
        End Sub

        'Private Sub CargueCanjeButton_Click(ByVal sender As Object, ByVal e As EventArgs)
        '    Try
        '        Dim cierrePlugin As FormCargueCanje
        '        cierrePlugin = New FormCargueCanje(Me._plugin)
        '        cierrePlugin.ShowDialog()

        '    Catch ex As Exception
        '        DMB.DesktopMessageShow("Cierre", ex)
        '    End Try
        'End Sub

        Private Sub CorreccionCargueButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            Try
                Dim cierrePlugin As New Forms.Correciones.FormCorreccion(Me.Plugin)
                cierrePlugin.ShowDialog()

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Correcciones_Cargue ", ex)
            End Try
        End Sub

        Private Sub SeguimientoCanjeButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            Try
                Dim cierrePlugin As New Forms.Seguimiento.FormSeguimientoCanje(Me.Plugin)
                cierrePlugin.ShowDialog()

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Cierre", ex)
            End Try
        End Sub

        Private Sub ExportarImagenesButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            Try
                Dim exportarImagenesPlugin As New Forms.Exportar.FormExportarImagenes(Me.Plugin)
                exportarImagenesPlugin.ShowDialog()
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Cierre", ex)
            End Try
        End Sub

        Private Sub CargueButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            Cargue()
        End Sub

        'Private Sub NovedadesButton_Click(ByVal sender As Object, ByVal e As EventArgs)
        '    Try
        '        Dim cierrePlugin As FormNovedades
        '        cierrePlugin = New FormNovedades(Me._plugin)
        '        cierrePlugin.ShowDialog()

        '    Catch ex As Exception
        '        DMB.DesktopMessageShow("Novedades", ex)
        '    End Try
        'End Sub

#End Region

#Region " Metodos "

        Public Sub AplicarCambios()
            Try
                ' Nuevos controles
                Me._banAgrarioTabPage = New TabPage()
                Me._correccionesTabPage = New TabPage()
                Me._cierreButton = New Button()
                Me._correccionCargueButton = New Button()
                Me._seguimientoCanjeButton = New Button()
                Me._exportarImagenesButton = New Button()
                'Me.NovedadesButton = New Button()

                ' TAB BANAGRARIO
                Me._banAgrarioTabPage.Controls.Add(Me._cierreButton)
                Me._banAgrarioTabPage.Controls.Add(Me._seguimientoCanjeButton)
                Me._banAgrarioTabPage.Controls.Add(Me._exportarImagenesButton)
                'Me.BanAgrarioTabPage.Controls.Add(Me.NovedadesButton)

                Me._banAgrarioTabPage.Location = New Drawing.Point(4, 26)
                Me._banAgrarioTabPage.Name = "BanAgrarioTabPage"
                Me._banAgrarioTabPage.Padding = New Padding(3)
                Me._banAgrarioTabPage.Size = New Drawing.Size(981, 362)
                Me._banAgrarioTabPage.TabIndex = 3
                Me._banAgrarioTabPage.Text = "Banco Agrario"
                Me._banAgrarioTabPage.UseVisualStyleBackColor = True

                ' TAB CORRECCIONES

                Me._correccionesTabPage.Controls.Add(Me._correccionCargueButton)

                Me._correccionesTabPage.Location = New Drawing.Point(4, 26)
                Me._correccionesTabPage.Name = "CorreccionesTabPage"
                Me._correccionesTabPage.Padding = New Padding(3)
                Me._correccionesTabPage.Size = New Drawing.Size(981, 362)
                Me._correccionesTabPage.TabIndex = 4
                Me._correccionesTabPage.Text = "Correcciones"
                Me._correccionesTabPage.UseVisualStyleBackColor = True


                ' Botón de Cierre
                Me._cierreButton.AccessibleDescription = ""
                Me._cierreButton.Anchor = AnchorStyles.None
                Me._cierreButton.BackColor = Drawing.Color.LightSteelBlue
                Me._cierreButton.Enabled = False
                Me._cierreButton.FlatStyle = FlatStyle.Flat
                Me._cierreButton.Font = New Drawing.Font("Tahoma", 8.25!, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CType(0, Byte))
                Me._cierreButton.Image = My.Resources.Resources.Process_Accept
                Me._cierreButton.ImageAlign = Drawing.ContentAlignment.TopCenter
                Me._cierreButton.Location = New Drawing.Point(122, 136)
                Me._cierreButton.Name = "CierreButton"
                Me._cierreButton.Size = New Drawing.Size(113, 60)
                Me._cierreButton.TabIndex = 20
                Me._cierreButton.Tag = "Ctrl + C"
                Me._cierreButton.Text = "&Cierre"
                Me._cierreButton.TextAlign = Drawing.ContentAlignment.BottomCenter
                Me._cierreButton.UseVisualStyleBackColor = False

                AddHandler Me._cierreButton.Click, AddressOf CierreButton_Click

                'Botón Corrección cargue
                Me._correccionCargueButton.AccessibleDescription = ""
                Me._correccionCargueButton.Anchor = AnchorStyles.None
                Me._correccionCargueButton.BackColor = Drawing.Color.LightSteelBlue
                Me._correccionCargueButton.Enabled = False
                Me._correccionCargueButton.FlatStyle = FlatStyle.Flat
                Me._correccionCargueButton.Font = New Drawing.Font("Tahoma", 8.0!, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CType(0, Byte))
                Me._correccionCargueButton.Image = My.Resources.Corregir_
                Me._correccionCargueButton.ImageAlign = Drawing.ContentAlignment.TopCenter
                Me._correccionCargueButton.Location = New Drawing.Point(122, 136)
                Me._correccionCargueButton.Name = "CorreccionCargueButton"
                Me._correccionCargueButton.Size = New Drawing.Size(123, 60)
                Me._correccionCargueButton.TabIndex = 23
                Me._correccionCargueButton.Tag = "Ctrl + R"
                Me._correccionCargueButton.Text = "&Mód. Correcciones"
                Me._correccionCargueButton.TextAlign = Drawing.ContentAlignment.BottomCenter
                Me._correccionCargueButton.UseVisualStyleBackColor = False

                AddHandler Me._correccionCargueButton.Click, AddressOf CorreccionCargueButton_Click

                ' Botón de seguimiento canje
                Me._seguimientoCanjeButton.AccessibleDescription = ""
                Me._seguimientoCanjeButton.Anchor = AnchorStyles.None
                Me._seguimientoCanjeButton.BackColor = Drawing.Color.LightSteelBlue
                Me._seguimientoCanjeButton.Enabled = False
                Me._seguimientoCanjeButton.FlatStyle = FlatStyle.Flat
                Me._seguimientoCanjeButton.Font = New Drawing.Font("Tahoma", 8.25!, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CType(0, Byte))
                Me._seguimientoCanjeButton.Image = My.Resources.Resources.btnRecepcion
                Me._seguimientoCanjeButton.ImageAlign = Drawing.ContentAlignment.TopCenter
                Me._seguimientoCanjeButton.Location = New Drawing.Point(522, 136)
                Me._seguimientoCanjeButton.Name = "SeguimientoCanjeButton"
                Me._seguimientoCanjeButton.Size = New Drawing.Size(113, 60)
                Me._seguimientoCanjeButton.TabIndex = 21
                Me._seguimientoCanjeButton.Tag = "Ctrl + S"
                Me._seguimientoCanjeButton.Text = "&Seguimiento Canje"
                Me._seguimientoCanjeButton.TextAlign = Drawing.ContentAlignment.BottomCenter
                Me._seguimientoCanjeButton.UseVisualStyleBackColor = False

                AddHandler Me._seguimientoCanjeButton.Click, AddressOf SeguimientoCanjeButton_Click

                ' BotónExportar Imagen
                Me._exportarImagenesButton.AccessibleDescription = ""
                Me._exportarImagenesButton.Anchor = AnchorStyles.None
                Me._exportarImagenesButton.BackColor = Drawing.Color.LightSteelBlue
                Me._exportarImagenesButton.Enabled = False
                Me._exportarImagenesButton.FlatStyle = FlatStyle.Flat
                Me._exportarImagenesButton.Font = New Drawing.Font("Tahoma", 8.25!, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CType(0, Byte))
                Me._exportarImagenesButton.Image = My.Resources.Resources.export_image
                Me._exportarImagenesButton.ImageAlign = Drawing.ContentAlignment.TopCenter
                Me._exportarImagenesButton.Location = New Drawing.Point(322, 136)
                Me._exportarImagenesButton.Name = "ExportarImagenesButton"
                Me._exportarImagenesButton.Size = New Drawing.Size(123, 60)
                Me._exportarImagenesButton.TabIndex = 22
                Me._exportarImagenesButton.Tag = "Ctrl + E"
                Me._exportarImagenesButton.Text = "&Exportar Imagenes"
                Me._exportarImagenesButton.TextAlign = Drawing.ContentAlignment.BottomCenter
                Me._exportarImagenesButton.UseVisualStyleBackColor = False

                AddHandler Me._exportarImagenesButton.Click, AddressOf ExportarImagenesButton_Click

                ''Boton Novedades
                'Me.NovedadesButton.AccessibleDescription = ""
                'Me.NovedadesButton.Anchor = AnchorStyles.None
                'Me.NovedadesButton.BackColor = Drawing.Color.LightSteelBlue
                'Me.NovedadesButton.Enabled = False
                'Me.NovedadesButton.FlatStyle = FlatStyle.Flat
                'Me.NovedadesButton.Font = New Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                'Me.NovedadesButton.Image = Global.Banagrario.Plugin.My.Resources.Resources.export_image
                'Me.NovedadesButton.ImageAlign = Drawing.ContentAlignment.TopCenter
                'Me.NovedadesButton.Location = New Drawing.Point(922, 136)
                'Me.NovedadesButton.Name = "NovedadesButton"
                'Me.NovedadesButton.Size = New Drawing.Size(123, 60)
                'Me.NovedadesButton.TabIndex = 21
                'Me.NovedadesButton.Tag = "Ctrl + N"
                'Me.NovedadesButton.Text = "&Novedades Destape"
                'Me.NovedadesButton.TextAlign = Drawing.ContentAlignment.BottomCenter
                'Me.NovedadesButton.UseVisualStyleBackColor = False

                'AddHandler Me.NovedadesButton.Click, AddressOf NovedadesButton_Click


                Me._cierreButton.Enabled = Plugin.Manager.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Imaging.Proceso.Control.OT)
                Me._correccionCargueButton.Enabled = Plugin.Manager.Sesion.Usuario.PerfilManager.PuedeAcceder(Permisos.Imaging.Proceso.Control.Autorizaciones)
                Me._seguimientoCanjeButton.Enabled = Plugin.Manager.Sesion.Usuario.PerfilManager.PuedeAcceder(Permisos.Imaging.Proceso.Control.Seguimiento)
                Me._exportarImagenesButton.Enabled = Plugin.Manager.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Imaging.Proceso.Exportar)

                Plugin.WorkSpace.MainTabControl.TabPages.Add(Me._banAgrarioTabPage)
                Plugin.WorkSpace.MainTabControl.TabPages.Add(Me._correccionesTabPage)


                'Menu plugin banagrario

                ConfiguracionBancoAgrarioToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {MatrizDocumentoToolStripMenuItem, TipologiaCodigoTxToolStripMenuItem, NovedadesTxToolStripMenuItem, MesaDestapeTxToolStripMenuItem, CambiarTipologiaToolStripMenuItem})
                ConfiguracionBancoAgrarioToolStripMenuItem.Name = "ConfiguracionBancoAgrarioToolStripMenuItem"
                ConfiguracionBancoAgrarioToolStripMenuItem.Size = New Drawing.Size(193, 22)
                ConfiguracionBancoAgrarioToolStripMenuItem.Text = "Configuración Banco Agrario ..."

                'Configuracion Matriz Documento
                MatrizDocumentoToolStripMenuItem.Name = "MatrizDocumentoToolStripMenuItem"
                MatrizDocumentoToolStripMenuItem.Size = New Drawing.Size(193, 22)
                MatrizDocumentoToolStripMenuItem.Text = "Matriz Documento..."

                'Configuracion Matriz Documento
                TipologiaCodigoTxToolStripMenuItem.Name = "TipologiaCodigoTxToolStripMenuItem"
                TipologiaCodigoTxToolStripMenuItem.Size = New Drawing.Size(193, 22)
                TipologiaCodigoTxToolStripMenuItem.Text = "Tipologia - Codigo TX..."

                'COnfiguracion Novedades
                NovedadesTxToolStripMenuItem.Name = "NovedadesTxToolStripMenuItem"
                NovedadesTxToolStripMenuItem.Size = New Drawing.Size(193, 22)
                NovedadesTxToolStripMenuItem.Text = "Novedades..."

                'Configuración Mesa Destape
                MesaDestapeTxToolStripMenuItem.Name = "MesaDestapeTxToolStripMenuItem"
                MesaDestapeTxToolStripMenuItem.Size = New Drawing.Size(193, 22)
                MesaDestapeTxToolStripMenuItem.Text = "Mesa Destape.."

                'Calificaciones Masivas
                CambiarTipologiaToolStripMenuItem.Name = "CambiarTipologiaToolStripMenuItem"
                CambiarTipologiaToolStripMenuItem.Size = New Drawing.Size(193, 22)
                CambiarTipologiaToolStripMenuItem.Text = "Calificaciones Masivas..."


                ' Ocultar opciones
                Plugin.WorkSpace.OTButton.Enabled = False
                Plugin.WorkSpace.FechaProcesoButton.Enabled = False
                Plugin.WorkSpace.DestapeButton.Enabled = False
                Plugin.WorkSpace.EmpaqueButton.Enabled = False

                ConfiguracionBancoAgrarioToolStripMenuItem.Enabled = Plugin.Manager.Sesion.Usuario.PerfilManager.PuedeConsultar(Program.PermisoMenuConfiguracionBanagrario)
                Plugin.WorkSpace.MainMenuStrip.Items.AddRange(New ToolStripItem() {ConfiguracionBancoAgrarioToolStripMenuItem})

                ' SearchControl
                Dim searchControl = New PluginSearchControlParameters(Plugin)
                Plugin.WorkSpace.WorkSpaceImagingSearchControl.SetSearchControl(searchControl)

                'Reportes Plugin
                Plugin.WorkSpace.WorkSpaceImagingReportViewerControl.ReportList.Add(New Reportes.DocumentosIlegibles.Report_DocumentosIlegibles(Plugin.WorkSpace.WorkSpaceImagingReportViewerControl, Plugin))
                '_plugin.WorkSpace.WorkSpaceImagingReportViewerControl.ReportList.Add(New Report_Faltantes(_plugin.WorkSpace.WorkSpaceImagingReportViewerControl, _plugin))
                Plugin.WorkSpace.WorkSpaceImagingReportViewerControl.ReportList.Add(New Report_ProductividadGeneral(Plugin.WorkSpace.WorkSpaceImagingReportViewerControl, Plugin))
                Plugin.WorkSpace.WorkSpaceImagingReportViewerControl.ReportList.Add(New Report_ProductividadDetallado(Plugin.WorkSpace.WorkSpaceImagingReportViewerControl, Plugin))
                Plugin.WorkSpace.WorkSpaceImagingReportViewerControl.ReportList.Add(New Reportes.GestionRegistrosPendientes.Report_GestionRegistrosPendientes(Plugin.WorkSpace.WorkSpaceImagingReportViewerControl, Plugin))
                Plugin.WorkSpace.WorkSpaceImagingReportViewerControl.ReportList.Add(New Reportes.GestionCorreccionLLaves.ReportGestionCorreccionLLaves(Plugin.WorkSpace.WorkSpaceImagingReportViewerControl, Plugin))
                Plugin.WorkSpace.WorkSpaceImagingReportViewerControl.ReportList.Add(New Reportes.Contenedor.Report_ConsolidadoContenedor(Plugin.WorkSpace.WorkSpaceImagingReportViewerControl, Plugin))
                Plugin.WorkSpace.WorkSpaceImagingReportViewerControl.ReportList.Add(New Reportes.Novedades.Report_NovedadesContenedor(Plugin.WorkSpace.WorkSpaceImagingReportViewerControl, Plugin))
                Plugin.WorkSpace.WorkSpaceImagingReportViewerControl.ReportList.Add(New Reportes.TiempoCiudadesConsolidado.ReportTiempoCiudadesConsolidado(Plugin.WorkSpace.WorkSpaceImagingReportViewerControl, Plugin))
                Plugin.WorkSpace.WorkSpaceImagingReportViewerControl.ReportList.Add(New Reportes.DetalleDestapeEmpaque.ReportTiempoCiudadesDetalladoDestape(Plugin.WorkSpace.WorkSpaceImagingReportViewerControl, Plugin))
                Plugin.WorkSpace.WorkSpaceImagingReportViewerControl.ReportList.Add(New Reportes.ControlIndexacion.ReportControlIndexacion(Plugin.WorkSpace.WorkSpaceImagingReportViewerControl, Plugin))
                Plugin.WorkSpace.WorkSpaceImagingReportViewerControl.ReportList.Add(New Reportes.Seguimiento.ReportSeguimientoProcesoDiario(Plugin.WorkSpace.WorkSpaceImagingReportViewerControl, Plugin))

                ' Reemplazar cargue
                PluginHelper.DisableControl(Plugin.WorkSpace.CargueButton)
                Me._cargueButton = PluginHelper.CloneButton(Plugin.WorkSpace.CargueButton)
                PluginHelper.ReplaceControl(Plugin.WorkSpace.CargueButton, _cargueButton)
                _cargueButton.Visible = True
                _cargueButton.Enabled = Plugin.Manager.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Imaging.Proceso.Indexacion.Cargue)
                AddHandler Me._cargueButton.Click, AddressOf CargueButton_Click
                PluginHelper.DisableControl(Plugin.WorkSpace.CargueButton)
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("No fue posible aplicar los cambios de Banagrario al workspace, " + ex.Message, "Plugin workspace", DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
            End Try
        End Sub

        Public Sub DeshacerCambios()

        End Sub

        Private Sub Cargue()
            If (Not _cargueButton.Visible Or Not _cargueButton.Enabled) Then Return

            Try
                If Plugin.Manager.Sesion.Usuario.PerfilManager.Permisos.Item(0).Cadena = Permisos.Imaging.Proceso.Especiales Then
                    Dim newCargue = New PluginCargueMixtoController(Plugin)
                    newCargue.Run()
                Else
                    Dim newCargue = New PluginCargueController(Plugin)
                    newCargue.Run()
                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Cargue", ex)
            End Try
        End Sub

        Private Sub MatrizDocumentoToolStripMenuItem_Click(sender As System.Object, e As EventArgs) Handles MatrizDocumentoToolStripMenuItem.Click
            Dim matrizDocumentoForm As New Forms.Parametrización.FormMatrizDocumento(Plugin)
            matrizDocumentoForm.ShowDialog()
        End Sub

        Private Sub TipologiaCodigoTxToolStripMenuItem_Click(sender As System.Object, e As EventArgs) Handles TipologiaCodigoTxToolStripMenuItem.Click
            Dim tipologiaTxForm As New Forms.Parametrización.Tipologia_CodigoTx(Plugin)
            tipologiaTxForm.ShowDialog()
        End Sub

        Private Sub NovedadesTxToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles NovedadesTxToolStripMenuItem.Click
            Dim novedadesForm As New Forms.Parametrización.FormNovedades(Plugin)
            novedadesForm.ShowDialog()
        End Sub

        Private Sub MesaDestapeTxToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles MesaDestapeTxToolStripMenuItem.Click
            Dim mesaDestapeForm As New Forms.Parametrización.FormMesaDestape(Plugin)
            mesaDestapeForm.ShowDialog()
        End Sub

        Private Sub CambiarTipologiaToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CambiarTipologiaToolStripMenuItem.Click
            Dim dbmBancoAgrario As DBAgrario.DBAgrarioDataBaseManager = Nothing
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                Dim selector = New OpenFileDialog()

                selector.Filter = "Log actualización (*.txt)|*.txt"

                Dim result = selector.ShowDialog()
                If (result = DialogResult.OK) Then
                    Dim lector = New Slyg.Tools.CSV.CSVData("|", "", True)
                    lector.LoadCSV(selector.FileName)

                    If (lector.DataTable.Columns.Count <> 2) Then Throw New Exception("El archivo no tiene una estructura correcta, debe tener dos columnas")
                    If (lector.DataTable.Columns(0).ColumnName <> "fk_Core_Index") Then Throw New Exception("El archivo no tiene una estructura correcta, la primera columna debe ser fk_Core_Index")
                    If (lector.DataTable.Columns(1).ColumnName <> "fk_Documento") Then Throw New Exception("El archivo no tiene una estructura correcta, la segunda columna debe ser fk_Documento")
                    If (lector.DataTable.Rows.Count = 0) Then Throw New Exception("El archivo no contiene registros para procesar")

                    result = MessageBox.Show("Se encontraron " & lector.DataTable.Rows.Count.ToString("#,##0") & " para procesar, ¿Desea continuar?", "Cambiar Tipología", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                    If (result = DialogResult.Yes) Then
                        dbmBancoAgrario = New DBAgrario.DBAgrarioDataBaseManager(Plugin.BancoAgrarioConnectionString)
                        dbmCore = New DBCore.DBCoreDataBaseManager(Plugin.Manager.DesktopGlobal.ConnectionStrings.Core)
                        dbmImaging = New DBImaging.DBImagingDataBaseManager(Plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)

                        dbmCore.Connection_Open(Plugin.Manager.Sesion.Usuario.id)
                        dbmImaging.Connection_Open(Plugin.Manager.Sesion.Usuario.id)

                        dbmBancoAgrario.Transaction_Begin()
                        dbmCore.Transaction_Begin()
                        dbmImaging.Transaction_Begin()

                        Dim i = 1
                        For Each row As CSVRow In lector.DataTable.Rows
                            i += 1

                            Dim coreIndexS = row.Item(0).ToString()
                            If (Not Slyg.Tools.DataConvert.IsNumeric(coreIndexS)) Then Throw New Exception("Error en la fila: " & i & ", fk_Core_Index debe ser numérico")

                            Dim documentoS = row.Item(1).ToString()
                            If (Not Slyg.Tools.DataConvert.IsNumeric(documentoS)) Then Throw New Exception("Error en la fila: " & i & ", fk_Documento debe ser numérico")

                            Dim idCoreIndex = CLng(coreIndexS)
                            Dim idDocumento = CInt(documentoS)

                            Dim coreIndexTable = dbmBancoAgrario.SchemaProcess.TBL_Core_Index.DBGet(idCoreIndex)
                            If (coreIndexTable.Count = 0) Then
                                Throw New Exception("Error en la fila: " & i & ", el fk_Core_Index: " & idCoreIndex & " no se encuentra registrado en el sistema")
                            End If

                            Dim expediente As Long = coreIndexTable(0).fk_Expediente
                            Dim folder As Short = coreIndexTable(0).fk_Folder
                            Dim file As Short = CShort(coreIndexTable(0).fk_File)

                            Dim documentoTable = dbmCore.SchemaConfig.TBL_Documento.DBGet(idDocumento)
                            If (documentoTable.Count = 0) Then
                                Throw New Exception("Error en la fila: " & i & ", el fk_Documento: " & idDocumento & " no se encuentra registrado en el sistema")
                            End If

                            Dim fileType = New DBCore.SchemaProcess.TBL_FileType() With {.fk_Documento = idDocumento}
                            dbmCore.SchemaProcess.TBL_File.DBUpdate(fileType, expediente, folder, file)

                            ' Estado
                            Dim fileEstadoType = New DBCore.SchemaProcess.TBL_File_EstadoType() With {.Fecha_Log = SlygNullable.SysDate, .fk_Usuario = Plugin.Manager.Sesion.Usuario.id, .fk_Estado = CShort(DBCore.EstadoEnum.Captura)}
                            dbmCore.SchemaProcess.TBL_File_Estado.DBUpdate(fileEstadoType, expediente, folder, file, CByte(DesktopConfig.Modulo.Imaging))

                            ' Registrar Tx en Reproceso
                            dbmBancoAgrario.SchemaProcess.PA_Registrar_Reproceso.DBExecute(expediente, folder, file, Plugin.Manager.Sesion.Usuario.id)

                            ' Borrar data
                            dbmCore.SchemaProcess.TBL_File_Data.DBDelete(expediente, folder, file, Nothing, Nothing)
                            dbmCore.SchemaProcess.TBL_File_Data_Asociada.DBDelete(expediente, folder, file, Nothing, Nothing, Nothing, Nothing)
                            dbmCore.SchemaProcess.TBL_File_Validacion.DBDelete(expediente, folder, file, Nothing, fileType.fk_Documento)

                            ' Solicita nuevamente validaciones opcionales
                            Dim imagingFileType = New DBCore.SchemaImaging.TBL_FileType() With {.Validaciones_Opcionales = False}
                            dbmCore.SchemaImaging.TBL_File.DBUpdate(imagingFileType, expediente, folder, file, Nothing)

                            '---------------------------------------------------------------------------
                            ' Actualizar Dashboard
                            '---------------------------------------------------------------------------
                            Dim folderDataTable = dbmCore.SchemaImaging.TBL_Folder.DBGet(expediente, folder)
                            Dim cargueDataTable = dbmImaging.SchemaProcess.TBL_Cargue.DBGet(folderDataTable(0).fk_Cargue)
                            Dim paqueteDataTable = dbmImaging.SchemaProcess.TBL_Cargue_Paquete.DBGet(folderDataTable(0).fk_Cargue, folderDataTable(0).fk_Cargue_Paquete)

                            ' Actualizar capturas
                            dbmImaging.SchemaProcess.TBL_Dashboard_Capturas.DBDelete(expediente, folder, file)

                            Dim capDashboardType = New DBImaging.SchemaProcess.TBL_Dashboard_CapturasType() With {.fk_Expediente = expediente, .fk_Folder = folder, .fk_File = file, .fk_Documento = idDocumento, .fk_Cargue = folderDataTable(0).fk_Cargue, .fk_Cargue_Paquete = folderDataTable(0).fk_Cargue_Paquete, .fk_Entidad_Procesamiento = cargueDataTable(0).fk_Entidad_Procesamiento, .fk_Sede_Procesamiento = paqueteDataTable(0).fk_Sede_Procesamiento_Asignada, .fk_Centro_Procesamiento = paqueteDataTable(0).fk_Centro_Procesamiento_Asignado, .fk_Entidad = cargueDataTable(0).fk_Entidad, .fk_Proyecto = cargueDataTable(0).fk_Proyecto, .fk_Estado = CShort(DBCore.EstadoEnum.Captura), .fk_OT = cargueDataTable(0).fk_OT}

                            dbmImaging.SchemaProcess.TBL_Dashboard_Capturas.DBInsert(capDashboardType)

                            ' Actualizar validaciones opcionales
                            Dim validacionesDataTable = dbmImaging.SchemaConfig.CTA_Validacion.DBFindByfk_Documentofk_Etapa_Captura(idDocumento, CByte(DBImaging.EnumEtapaCaptura.Opcional), 1, New DBImaging.SchemaConfig.CTA_ValidacionEnumList(DBImaging.SchemaConfig.CTA_ValidacionEnum.id_Validacion, True))

                            If (validacionesDataTable.Count > 0) Then
                                dbmImaging.SchemaProcess.TBL_Dashboard_Validaciones.DBDelete(expediente, folder, file)

                                Dim valDashboardType = New DBImaging.SchemaProcess.TBL_Dashboard_ValidacionesType() With {.fk_Expediente = expediente, .fk_Folder = folder, .fk_File = file, .fk_Documento = idDocumento, .fk_Cargue = folderDataTable(0).fk_Cargue, .fk_Cargue_Paquete = folderDataTable(0).fk_Cargue_Paquete, .fk_Entidad_Procesamiento = cargueDataTable(0).fk_Entidad_Procesamiento, .fk_Sede_Procesamiento = paqueteDataTable(0).fk_Sede_Procesamiento_Asignada, .fk_Centro_Procesamiento = paqueteDataTable(0).fk_Centro_Procesamiento_Asignado, .fk_Entidad = cargueDataTable(0).fk_Entidad, .fk_Proyecto = cargueDataTable(0).fk_Proyecto, .Procesado = False}

                                dbmImaging.SchemaProcess.TBL_Dashboard_Validaciones.DBInsert(valDashboardType)
                            End If
                            '---------------------------------------------------------------------------

                            dbmImaging.SchemaProcess.TBL_File_Data_MC.DBDelete(expediente, folder, file, Nothing, Nothing, Nothing)
                            dbmImaging.SchemaProcess.TBL_File_Data_MC_Asociada.DBDelete(expediente, folder, file, Nothing, Nothing, Nothing, Nothing, Nothing)
                        Next

                        dbmBancoAgrario.Transaction_Commit()
                        dbmCore.Transaction_Commit()
                        dbmImaging.Transaction_Commit()

                        MessageBox.Show("Proceso finalizado con éxito", "Calificaciones Masivas", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                End If
            Catch ex As Exception
                If (dbmBancoAgrario IsNot Nothing) Then dbmBancoAgrario.Transaction_Rollback()
                If (dbmCore IsNot Nothing) Then dbmCore.Transaction_Rollback()
                If (dbmImaging IsNot Nothing) Then dbmImaging.Transaction_Rollback()

                MessageBox.Show(ex.Message, "Calificaciones Masivas", MessageBoxButtons.OK, MessageBoxIcon.Error)

            Finally
                If (dbmBancoAgrario IsNot Nothing) Then dbmBancoAgrario.Connection_Close()
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

#End Region

    End Class

End Namespace