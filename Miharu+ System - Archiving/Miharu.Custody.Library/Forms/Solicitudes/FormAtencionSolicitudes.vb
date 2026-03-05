Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports DBArchiving
Imports DBCore
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Config
Imports Slyg.Tools
Imports System.ComponentModel
Imports Miharu.Desktop.Controls.DesktopComboBox

Namespace Forms.Solicitudes

    Public Class FormAtencionSolicitudes
        Inherits FormBase

#Region "Declaraciones"
        Dim dtSolicitudesEncontradas As DataTable
        Dim _IdEntidad As Integer
#End Region

#Region " Enumeraciones "

        Public Enum IconoSolicitud As Integer
            ATiempo = 0
            ProximoVencer = 1
            Vencido = 2
        End Enum

#End Region

#Region " Eventos "

        Private Sub FormAtencionSolicitudes_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            CargaFiltros()
        End Sub

        Private Sub EntidadDesktopComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles EntidadDesktopComboBox.SelectedIndexChanged
            CargaFiltroProyecto(Program.Sesion.Usuario.id, CShort(EntidadDesktopComboBox.SelectedValue))
        End Sub

        Private Sub BuscarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BuscarButton.Click
            If (Not VerificaSeleccion(Me.EntidadDesktopComboBox)) Then
                DesktopMessageBoxControl.DesktopMessageShow("Error debe seleccionar una Entidad!!", "Selección", DesktopMessageBoxControl.IconEnum.ErrorIcon, True, False)
                Me.EntidadDesktopComboBox.Focus()
                Return
            ElseIf (Not VerificaSeleccion(Me.ProyectoDesktopComboBox)) Then
                DesktopMessageBoxControl.DesktopMessageShow("Error debe seleccionar un Proyecto!!", "Selección", DesktopMessageBoxControl.IconEnum.ErrorIcon, True, False)
                Me.ProyectoDesktopComboBox.Focus()
                Return
            End If

            If (Not Me.AtencionSolicitudesBackgroundWorker.IsBusy) Then
                Me.CargandoPictureBox.Visible = True
                Me._IdEntidad = CInt(Me.EntidadDesktopComboBox.SelectedValue)
                Me.SolicitudesDataGridView.DataSource = Nothing
                Me.AtencionSolicitudesBackgroundWorker.RunWorkerAsync()
            End If
        End Sub

        Private Sub FoldersDataGridView_CellClick(ByVal sender As System.Object, ByVal e As DataGridViewCellEventArgs) Handles FoldersDataGridView.CellClick
            Dim grid = CType(sender, DataGridView)
            If grid.SelectedRows.Count > 0 Then
                CargaItemsSolicitud(CInt(grid.SelectedRows(0).Cells(0).Value), CStr(grid.SelectedRows(0).Cells(1).Value))
            Else
                CargaItemsSolicitud(-1, "-1")
            End If
        End Sub

        Private Sub SolicitudesDataGridView_CellClick(ByVal sender As System.Object, ByVal e As DataGridViewCellEventArgs) Handles SolicitudesDataGridView.CellClick
            Dim grid = CType(sender, DataGridView)
            If grid.SelectedRows.Count > 0 Then
                CargaCarpetasSolicitud(CInt(grid.SelectedRows(0).Cells(0).Value))
            Else
                CargaCarpetasSolicitud(-1)
            End If
        End Sub

        Private Sub CerrarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CerrarButton.Click
            Me.Close()
        End Sub

        Private Sub SolicitudesDataGridView_RowsAdded(ByVal sender As System.Object, ByVal e As DataGridViewRowsAddedEventArgs) Handles SolicitudesDataGridView.RowsAdded
            Try
                Dim grid = CType(sender, DataGridView)
                Dim source = CType(grid.DataSource, DataTable)
                Dim image As Drawing.Image = Nothing

                If grid.RowCount = source.Rows.Count Then
                    For i = 0 To source.Rows.Count - 1
                        Select Case CInt(source.Rows(i).Item("Estado"))
                            Case IconoSolicitud.ATiempo 'A tiempo
                                image = IconosImageList.Images(IconoSolicitud.ATiempo)
                            Case IconoSolicitud.ProximoVencer 'Proxima a vencer
                                image = IconosImageList.Images(IconoSolicitud.ProximoVencer)
                            Case IconoSolicitud.Vencido 'Vencida
                                image = IconosImageList.Images(IconoSolicitud.Vencido)
                        End Select
                        grid.Rows(i).Cells(1).Value = image
                    Next
                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("ItemsSolicitudDataGridView_RowsAdded", ex)
            End Try
        End Sub

        Private Sub ItemsSolicitudDesktopData_CellDoubleClick(ByVal sender As System.Object, ByVal e As DataGridViewCellEventArgs) Handles ItemsSolicitudDesktopData.CellDoubleClick
            Dim rowItem = CType(ItemsSolicitudDesktopData.DataSource, DataTable).Rows(e.RowIndex)
            GestionaItem(rowItem)
        End Sub

        Private Sub CBarrasItemTextBox_KeyPress(ByVal sender As System.Object, ByVal e As KeyPressEventArgs) Handles cbarrasItemDesktopCBarrasControl.KeyPress
            If e.KeyChar = ChrW(Keys.Enter) Then
                e.Handled = True
                'CBarrasItemTextBox.Text = DesktopTextBoxControl.CBarrasFlash(CBarrasItemTextBox.Text)
                Dim tItems = CType(ItemsSolicitudDesktopData.DataSource, DataTable)

                For Each item As DataRow In tItems.Rows
                    If item("CBarras_File").ToString() = cbarrasItemDesktopCBarrasControl.Text Then
                        GestionaItem(item)
                        Exit For
                    End If
                Next
            End If
        End Sub

        Private Sub CBarrasFolderTextBox_KeyPress(ByVal sender As System.Object, ByVal e As KeyPressEventArgs) Handles cbarrasFolderDesktopCBarrasControl.KeyPress
            If e.KeyChar = ChrW(Keys.Enter) Then
                e.Handled = True
                'CBarrasFolderTextBox.Text = DesktopTextBoxControl.CBarrasFlash(CBarrasFolderTextBox.Text)

                If cbarrasFolderDesktopCBarrasControl.Text <> "" Then
                    If cbarrasFolderDesktopCBarrasControl.Text.Substring(0, 3) = "000" Then 'Folder
                        Dim tFolders = CType(FoldersDataGridView.DataSource, DataTable)
                        Dim i As Integer = 0
                        For Each folder As DataRow In tFolders.Rows
                            If folder("CBarras_Folder").ToString() = cbarrasFolderDesktopCBarrasControl.Text Then
                                FoldersDataGridView.Rows(i).Selected = True
                                CargaItemsSolicitud(CInt(folder("fk_Solicitud")), folder("CBarras_Folder").ToString())
                                Exit For
                            End If
                            i += 1
                        Next
                    Else 'File

                    End If

                    'Dim tItems = CType(ItemsSolicitudDesktopData.DataSource, DataTable)
                    'CBarrasFolderTextBox.SelectAll()
                    'CBarrasFolderTextBox.Focus()
                End If
            End If
        End Sub

        Private Sub AtencionSolicitudesBackgroundWorker_DoWork(sender As System.Object, e As System.ComponentModel.DoWorkEventArgs) Handles AtencionSolicitudesBackgroundWorker.DoWork
            Dim bw As BackgroundWorker = CType(sender, BackgroundWorker)
            If (bw.CancellationPending) Then
                e.Cancel = True
            Else
                BuscarSolicitudes()
            End If
        End Sub

        Private Sub AtencionSolicitudesBackgroundWorker_RunWorkerCompleted(sender As System.Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles AtencionSolicitudesBackgroundWorker.RunWorkerCompleted
            LlenarGrillaSolicitudes(Me.dtSolicitudesEncontradas)
            Me.AtencionSolicitudesBackgroundWorker.CancelAsync()
            Me.CargandoPictureBox.Visible = False
        End Sub

        Private Sub AtencionSolicitudesBackgroundWorker_ProgressChanged(sender As System.Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles AtencionSolicitudesBackgroundWorker.ProgressChanged
            Me.CargandoPictureBox.Visible = True
        End Sub

#End Region

#Region " Metodos "

        Public Sub New()
            ' Llamada necesaria para el diseñador.
            InitializeComponent()
            CheckForIllegalCrossThreadCalls = False
            ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
            cbarrasFolderDesktopCBarrasControl.Init(Program.DesktopGlobal.ConnectionStrings.Archiving)
            cbarrasItemDesktopCBarrasControl.Init(Program.DesktopGlobal.ConnectionStrings.Archiving)
        End Sub

        Public Function VerificaSeleccion(ControlCombo As DesktopComboBoxControl) As Boolean
            Dim _retorno As Boolean = False
            Try
                If (ControlCombo.SelectedIndex <> 0) Then
                    _retorno = True
                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Error Seleccion", ex)
            End Try
            Return _retorno
        End Function



        Private Sub CargaFiltros()
            Dim dmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            Try
                dmArchiving.Connection_Open(Program.Sesion.Usuario.id)
                'Carga filtro de las Entidades
                Dim tEntidades = dmArchiving.Schemadbo.CTA_Entidad_Rol_Usuario.DBFindByid_Usuario(Program.Sesion.Usuario.id).DefaultView.ToTable(True)
                Utilities.LlenarCombo(EntidadDesktopComboBox, tEntidades, tEntidades.Columns("fk_Entidad").ColumnName, tEntidades.Columns("Nombre_Entidad").ColumnName, True, "-1", "Seleccionar...")

                'Carga Filtro Proyectos
                CargaFiltroProyecto(Program.Sesion.Usuario.id, -1)

                'Carga Filtro Prioridades
                Dim tPrioridad = dmArchiving.Schemadbo.CTA_Prioridad_Rol_Usuario.DBFindByfk_Usuario(Program.Sesion.Usuario.id).DefaultView.ToTable(True)
                Utilities.LlenarCombo(PrioridadDesktopComboBox, tPrioridad, tPrioridad.Columns("id_Solicitud_Prioridad").ColumnName, tPrioridad.Columns("Nombre_Solicitud_Prioridad").ColumnName, True, "-1", "Todos...")

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaFiltros", ex)
            Finally
                dmArchiving.Connection_Close()
            End Try
        End Sub

        Private Sub CargaFiltroProyecto(ByVal fk_usuario As Integer, ByVal fk_Entidad As Short)
            Dim dmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            Try
                dmArchiving.Connection_Open(Program.Sesion.Usuario.id)
                Dim tProyectos = dmArchiving.Schemadbo.CTA_Proyecto_Rol_Usuario.DBFindByid_Usuariofk_Entidad(fk_usuario, fk_Entidad)
                Utilities.LlenarCombo(ProyectoDesktopComboBox, tProyectos, tProyectos.Columns("fk_Proyecto").ColumnName, tProyectos.Columns("Nombre_Proyecto").ColumnName, True, "-1", "Seleccionar...")
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaFiltroProyecto", ex)
            Finally
                dmArchiving.Connection_Close()
            End Try
        End Sub

        Private Sub BuscarSolicitudes()
            Dim dmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            Try
                dmArchiving.Connection_Open(Program.Sesion.Usuario.id)
                Dim nEntidad As SlygNullable(Of Integer) = Nothing
                Dim nProyecto As SlygNullable(Of Integer) = Nothing
                Dim nPrioridad As SlygNullable(Of Integer) = Nothing
                Dim nIdentificacionUsuario As SlygNullable(Of String) = Nothing

                If CInt(EntidadDesktopComboBox.SelectedValue) <> -1 Then nEntidad = CInt(EntidadDesktopComboBox.SelectedValue)
                If CInt(ProyectoDesktopComboBox.SelectedValue) <> -1 Then nProyecto = CInt(ProyectoDesktopComboBox.SelectedValue)
                If CInt(PrioridadDesktopComboBox.SelectedValue) <> -1 Then nPrioridad = CInt(PrioridadDesktopComboBox.SelectedValue)
                If IdSolicitanteDesktopTextBox.Text <> "" Then nIdentificacionUsuario = IdSolicitanteDesktopTextBox.Text

                Me.SolicitudesDataGridView.DataSource = Nothing
                Dim dItemsSolicitudes = dmArchiving.Schemadbo.PA_Obtiene_Solicitudes.DBExecute(nEntidad, nProyecto, nPrioridad, nIdentificacionUsuario, Nothing, Nothing, Nothing, "ATENCIONSOL")
                Me.dtSolicitudesEncontradas = dItemsSolicitudes

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("BuscarSolicitudes", ex)
            Finally
                dmArchiving.Connection_Close()
            End Try
        End Sub

        Private Sub LlenarGrillaSolicitudes(dItemsSolicitudes As DataTable)
            Try
                If dItemsSolicitudes.Rows.Count > 0 Then
                    SolicitudesDataGridView.AutoGenerateColumns = False
                    SolicitudesDataGridView.DataSource = dItemsSolicitudes
                    If dItemsSolicitudes.Rows.Count = 1 Then
                        CargaCarpetasSolicitud(CInt(dItemsSolicitudes.Rows(0).Item("id_Solicitud")))
                    Else
                        SolicitudesDataGridView.ClearSelection()
                    End If
                Else
                    SolicitudesDataGridView.AutoGenerateColumns = False
                    SolicitudesDataGridView.DataSource = Nothing
                    DesktopMessageBoxControl.DesktopMessageShow("No se encontraron resultados para la búsqueda realizada", "Búsqueda", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargarGrilla", ex)
            End Try
        End Sub

        Private Sub CargaCarpetasSolicitud(ByVal idSolicitud As Integer)
            Dim dmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            Try
                dmArchiving.Connection_Open(Program.Sesion.Usuario.id)
                Dim dSolicitudes = dmArchiving.Schemadbo.CTA_Folders_Solicitud.DBFindByfk_SolicitudActiva_Solicitud(idSolicitud, True)
                FoldersDataGridView.AutoGenerateColumns = False
                FoldersDataGridView.DataSource = dSolicitudes

                If dSolicitudes.Rows.Count = 1 Then
                    CargaItemsSolicitud(CInt(dSolicitudes.Rows(0).Item("fk_Solicitud")), CStr(dSolicitudes.Rows(0).Item("CBarras_Folder")))
                    cbarrasItemDesktopCBarrasControl.SelectAll()
                    cbarrasItemDesktopCBarrasControl.Focus()
                Else
                    cbarrasFolderDesktopCBarrasControl.SelectAll()
                    cbarrasFolderDesktopCBarrasControl.Focus()
                    CargaItemsSolicitud(-1, "-1")
                    FoldersDataGridView.ClearSelection()
                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaCarpetasSolicitud", ex)
            Finally
                dmArchiving.Connection_Close()
            End Try
        End Sub

        Private Sub CargaItemsSolicitud(ByVal idSolicitud As Integer, ByVal CBarrasFolder As String)
            Dim dmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            Try
                dmArchiving.Connection_Open(Program.Sesion.Usuario.id)
                Dim dSolicitudes = dmArchiving.Schemadbo.CTA_Items_Solicitud.DBFindByfk_SolicitudCBarras_Folderfk_Estado(idSolicitud, CBarrasFolder, EstadoEnum.Alistamiento_custodia)

                If dSolicitudes.Rows.Count > 0 Then
                    cbarrasItemDesktopCBarrasControl.SelectAll()
                    cbarrasItemDesktopCBarrasControl.Focus()
                End If

                ItemsSolicitudDesktopData.AutoGenerateColumns = False
                ItemsSolicitudDesktopData.DataSource = dSolicitudes
                ItemsSolicitudDesktopData.ClearSelection()

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CreaItemsSolicitud", ex)
            Finally
                dmArchiving.Connection_Close()
            End Try
        End Sub

        Private Sub GestionaItem(ByVal rowItem As DataRow)
            Dim dmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            Dim dbmCore As New DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)

            Try
                dmArchiving.Connection_Open(Program.Sesion.Usuario.id)
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                Dim tipoSolicitudLocal = CInt(rowItem("id_Solicitud_Tipo"))
                Dim Solicitud = CInt(rowItem("fk_Solicitud"))
                Dim IdItemSolicitud = CShort(rowItem("id_Item_Solicitud"))
                Dim CBarrasFile = CStr(rowItem("CBarras_File"))
                Dim CBarras_Folder = CStr(rowItem("CBarras_Folder"))

                'Folios
                Dim nFolios As Short = 0
                If tipoSolicitudLocal = DesktopConfig.SolicitudTipo.Fotocopia _
                   Or tipoSolicitudLocal = DesktopConfig.SolicitudTipo.Fax _
                   Or tipoSolicitudLocal = DesktopConfig.SolicitudTipo.Imagen Then

                    Dim tFolios As String = ""
                    InputBox.Show("Cantidad de Folios", "Por favor ingrese la cantidad de folios:", tFolios)

                    If IsNumeric(tFolios) Then
                        nFolios = CShort(tFolios)
                    Else
                        DesktopMessageBoxControl.DesktopMessageShow("La cantidad de folios debe ser numérica.", "Cantidad inválida", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                        Exit Sub
                    End If
                Else
                    Dim dtFile As DBArchiving.Schemadbo.CTA_FileDataTable
                    dtFile = dmArchiving.Schemadbo.CTA_File.DBFindByCBarras_File(CBarrasFile)

                    If dtFile.Count > 0 Then
                        nFolios = dtFile(0).Folios_File
                    End If
                End If

                'Observaciones
                Dim sObservacion As String = ""
                If tipoSolicitudLocal = DesktopConfig.SolicitudTipo.Fax _
                   Or tipoSolicitudLocal = DesktopConfig.SolicitudTipo.Imagen Then

                    'sObservacion = InputBox("Por favor ingrese los datos de envío.:", "Observaciones de envío")
                    Dim objObservaciones As New FormObservacionesSolicitud()
                    If objObservaciones.ShowDialog() = DialogResult.OK Then
                        sObservacion = objObservaciones.Fecha
                        sObservacion += objObservaciones.Destinatario
                        sObservacion += objObservaciones.RemitidaPor
                        sObservacion += objObservaciones.Observaciones
                    Else
                        Exit Sub
                    End If
                End If

                'Item Solicitud
                Dim tItemSolicitud As New DBArchiving.SchemaCustody.TBL_Solicitud_ItemType
                Dim tEstadoFile As New EstadoEnum

                tItemSolicitud.Cantidad_Folios = CShort(nFolios)
                If sObservacion <> "" Then tItemSolicitud.Observaciones = sObservacion

                'Actualiza los items de la solicitud y files de acuerdo al tipo de solicitud.
                Select Case tipoSolicitudLocal
                    Case DesktopConfig.SolicitudTipo.Prestamo
                        tItemSolicitud.fk_Estado = EstadoEnum.En_despacho
                        tEstadoFile = EstadoEnum.En_despacho

                    Case DesktopConfig.SolicitudTipo.Retiro
                        tItemSolicitud.fk_Estado = EstadoEnum.En_despacho
                        tEstadoFile = EstadoEnum.En_despacho

                    Case DesktopConfig.SolicitudTipo.Fotocopia
                        tItemSolicitud.fk_Estado = EstadoEnum.En_despacho
                        tEstadoFile = EstadoEnum.Por_Custodiar

                    Case DesktopConfig.SolicitudTipo.Fax
                        tItemSolicitud.fk_Estado = EstadoEnum.Enviado_al_cliente
                        tEstadoFile = EstadoEnum.Por_Custodiar

                    Case DesktopConfig.SolicitudTipo.Imagen
                        tItemSolicitud.fk_Estado = EstadoEnum.Enviado_al_cliente
                        tEstadoFile = EstadoEnum.Por_Custodiar

                    Case DesktopConfig.SolicitudTipo.RevisionEnBoveda
                        tItemSolicitud.fk_Estado = EstadoEnum.Custodia
                        tEstadoFile = EstadoEnum.Por_Custodiar
                End Select

                'Item Solicitud
                dmArchiving.SchemaCustody.TBL_Solicitud_Item.DBUpdate(tItemSolicitud, Solicitud, IdItemSolicitud)
                 
                'Estado File 
                Dim nModulo As Miharu.Desktop.Library.Config.DesktopConfig.Modulo = DesktopConfig.Modulo.Archiving

                Dim tblfile As DBCore.SchemaProcess.TBL_FileDataTable = dbmCore.SchemaProcess.TBL_File.DBFindByCBarras_File(CBarrasFile)
                Dim tblFileEstado As DBCore.SchemaProcess.TBL_File_EstadoDataTable = dbmCore.SchemaProcess.TBL_File_Estado.DBFindByfk_Expedientefk_Folderfk_FileModulo(tblfile(0).fk_Expediente, tblfile(0).fk_Folder, tblfile(0).id_File, 2)
                If tblFileEstado(0).Modulo = 2 Then
                    nModulo = DesktopConfig.Modulo.Imaging
                Else
                    nModulo = DesktopConfig.Modulo.Archiving
                End If

                Utilities.ActualizaEstadoFile(dmArchiving, dbmCore, CBarrasFile, Nothing, Nothing, nModulo, tEstadoFile, Program.Sesion.Usuario.id, Nothing)

                'Movimiento Facturación
                Dim FileExpediente As DBArchiving.Schemadbo.CTA_File_ExpedienteDataTable
                FileExpediente = dmArchiving.Schemadbo.CTA_File_Expediente.DBFindByCBarras_File(CBarrasFile)

                Utilities.AgregarMovimiento(dmArchiving, Program.Sesion.Entidad.id, FileExpediente(0).fk_Esquema_Facturacion, DesktopConfig.Servicio_Facturacion.Atención_Solicitudes, FileExpediente(0).fk_Entidad, FileExpediente(0).fk_Proyecto, FileExpediente(0).fk_Esquema, CInt(IIf(nFolios = 0, FileExpediente(0).Folios_File, nFolios)), Program.Sesion.Usuario.id, Program.DesktopGlobal.CentroProcesamientoRow)

                CargaItemsSolicitud(Solicitud, CBarras_Folder)
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("GestionaItem", ex)
            Finally
                dbmCore.Connection_Close()
                dmArchiving.Connection_Close()
            End Try

            cbarrasItemDesktopCBarrasControl.SelectAll()
            cbarrasItemDesktopCBarrasControl.Focus()
        End Sub
#End Region



    End Class

End Namespace