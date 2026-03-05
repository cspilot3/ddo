Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Custody.Library.Forms.Reportes.Despacho
Imports DBArchiving
Imports DBCore
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Config
Imports Slyg.Tools
Imports Miharu.Desktop.Controls

Namespace Forms.Despacho

    Public Class FormDespacho
        Inherits FormBase

#Region " Declaraciones "

        Dim TableGuias As DataTable
        Dim TableDestinatario As DataTable
        Dim TableGuiasFiles As DataTable
        Dim Orden As Integer = 0
        Dim guiaLocal As String = String.Empty

#End Region

#Region " Eventos "

        Private Sub ReimprimirGuiaButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ReimprimirGuiaButton.Click
            Dim Reimpresion As New FormGuiaDespachoReimpresion()
            Reimpresion.ShowDialog()
        End Sub

        Private Sub BuscarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BuscarButton.Click
            If (DevuelveSeleccionado(Me.EntidadDesktopComboBox) = False) Then
                DesktopMessageBoxControl.DesktopMessageShow("Error, debe seleccionar una Entidad!!", "Error Selección", DesktopMessageBoxControl.IconEnum.ErrorIcon, True, False)
                Return
            ElseIf (DevuelveSeleccionado(Me.ProyectoDesktopComboBox) = False) Then
                DesktopMessageBoxControl.DesktopMessageShow("Error, debe seleccionar un Proyecto!!", "Error Selección", DesktopMessageBoxControl.IconEnum.ErrorIcon, True, False)
                Return
            End If
            BuscarSolicitudes()
        End Sub

        Private Sub FormDespacho_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            CargaFiltros()
            CrearTables()
        End Sub

        Private Sub DestinatarioDesktopDataGridView_CellClick(ByVal sender As System.Object, ByVal e As DataGridViewCellEventArgs) Handles DestinatarioDesktopDataGridView.CellClick
            Dim Columna As String = DestinatarioDesktopDataGridView.Columns(e.ColumnIndex).Name
            If Columna = "CerrarGuiaColumn" Then
                Dim Row = DestinatarioDesktopDataGridView.Rows(e.RowIndex)
                Dim DestinatarioLocal As Integer = CInt(Row.Cells("ID").Value)
                Dim GuiaLocal As String = CStr(Row.Cells("Guia").Value)
                Dim SelloLocal As String = CStr(Row.Cells("Sello").Value)
                CerrarGuia(DestinatarioLocal, GuiaLocal, SelloLocal)
            End If

            Try
                Dim Destinatario_ As Integer = CInt(DestinatarioDesktopDataGridView.Rows(e.RowIndex).Cells("ID").Value)
                llenarcontroles(Destinatario_)
            Catch ex As Exception
                llenarcontroles(0)
            End Try

        End Sub

        Private Sub ItemsDesktopDataGridView_CellClick(ByVal sender As System.Object, ByVal e As DataGridViewCellEventArgs) Handles ItemsDesktopDataGridView.CellClick
            EliminarItem(e.RowIndex, e.ColumnIndex)
        End Sub

        Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AsignarGuiaButton.Click
            buscarGuia(cbarrasDesktopCBarrasControl.Text)
        End Sub

        Private Sub FormDespacho_FormClosing(ByVal sender As System.Object, ByVal e As FormClosingEventArgs) Handles MyBase.FormClosing
            If TableGuias.Rows.Count > 0 Or TableDestinatario.Rows.Count > 0 Then
                If DesktopMessageBoxControl.DesktopMessageShow("si sale todos los cambios que no se han guardado se perderan,Esta seguro que desea salir?", "Guia de despacho", DesktopMessageBoxControl.IconEnum.AdvertencyIcon) = DialogResult.OK Then
                    Dim dmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
                    dmArchiving.Connection_Open(Program.Sesion.Usuario.id)
                    dmArchiving.Transaction_Begin()

                    For Each row As DataRow In TableGuias.Rows
                        Dim SolicitudItem As New DBArchiving.SchemaCustody.TBL_Solicitud_ItemType
                        SolicitudItem.fk_Estado = EstadoEnum.En_despacho
                        dmArchiving.SchemaCustody.TBL_Solicitud_Item.DBUpdate(SolicitudItem, CInt(row("fk_Solicitud")), CShort(row("id_Item_Solicitud")))
                    Next

                    dmArchiving.Transaction_Commit()
                    dmArchiving.Connection_Close()
                Else
                    e.Cancel = True
                End If
            End If
        End Sub

        Private Sub cbarrasDesktopCBarrasControl_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles cbarrasDesktopCBarrasControl.KeyPress
            If e.KeyChar = ChrW(Keys.Enter) Then
                If String.IsNullOrWhiteSpace(guiaLocal) Then
                    e.Handled = True
                    SendKeys.Send("{TAB}")
                Else
                    buscarGuia(cbarrasDesktopCBarrasControl.Text)
                End If
            End If
        End Sub

#End Region

#Region "Tree functions"

        Private Sub CargaFiltros()
            Dim dmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            Try
                dmArchiving.Connection_Open(Program.Sesion.Usuario.id)
                'Carga filtro de las Entidades
                Dim tEntidades = dmArchiving.Schemadbo.CTA_Entidad_Rol_Usuario.DBFindByid_Usuario(Program.Sesion.Usuario.id).DefaultView.ToTable(True)
                Utilities.LlenarCombo(EntidadDesktopComboBox, tEntidades, "fk_Entidad", "Nombre_Entidad", True, "-1", "Todos...")
                'Carga Filtro Proyectos
                CargaFiltroProyecto(Program.Sesion.Usuario.id, CShort(EntidadDesktopComboBox.SelectedValue))

                'Carga Filtro Prioridades
                Dim tPrioridad = dmArchiving.Schemadbo.CTA_Prioridad_Rol_Usuario.DBFindByfk_Usuario(Program.Sesion.Usuario.id).DefaultView.ToTable(True)
                Utilities.LlenarCombo(PrioridadDesktopComboBox, tPrioridad, "id_Solicitud_Prioridad", "Nombre_Solicitud_Prioridad", True, "-1", "Todos...")

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
                Utilities.LlenarCombo(ProyectoDesktopComboBox, tProyectos, "fk_Proyecto", "Nombre_Proyecto", True, "-1", "Todos...")
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
                Dim nEntidad As SlygNullable(Of Short) = Nothing
                Dim nProyecto As SlygNullable(Of Short) = Nothing
                Dim nPrioridad As SlygNullable(Of Byte) = Nothing
                Dim nIdentificacionUsuario As SlygNullable(Of String) = Nothing

                If CInt(EntidadDesktopComboBox.SelectedValue) <> -1 Then nEntidad = CShort(EntidadDesktopComboBox.SelectedValue)
                If CInt(ProyectoDesktopComboBox.SelectedValue) <> -1 Then nProyecto = CShort(ProyectoDesktopComboBox.SelectedValue)
                If CInt(PrioridadDesktopComboBox.SelectedValue) <> -1 Then nPrioridad = CByte(PrioridadDesktopComboBox.SelectedValue)
                If IdSolicitanteDesktopTextBox.Text <> "" Then nIdentificacionUsuario = IdSolicitanteDesktopTextBox.Text

                SolicitudesTreeView.Nodes.Clear()

                Dim Solicitudes_Despacho = dmArchiving.Schemadbo.CTA_Obtener_Solicitudes_Despacho.DBFindByfk_Entidadfk_Bovedafk_SedeEntidad_ClienteProyecto_Clientefk_Solicitud_PrioridadIdentificacion_Usuario( _
                    Nothing, _
                    Nothing, _
                    Nothing, _
                    nEntidad, _
                    nProyecto, _
                    nPrioridad, _
                    nIdentificacionUsuario)

                If Solicitudes_Despacho.Count > 0 Then
                    For Each solicitud As DataRow In Solicitudes_Despacho.Rows
                        SolicitudesTreeView.Nodes.Add("root-" & solicitud("Id_Solicitud").ToString(), solicitud("Id_Solicitud").ToString() & " - " & solicitud("Solicitante").ToString())
                        SolicitudesTreeView.CollapseAll()
                    Next
                Else
                    DesktopMessageBoxControl.DesktopMessageShow("No se encontraron resultados para la búsqueda realizada", "Búsqueda", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("BuscarSolicitudes", ex)
            Finally
                dmArchiving.Connection_Close()
            End Try
        End Sub

        Private Sub CreaCarpetasSolicitud(ByRef treeNode As TreeNode, ByVal idSolicitud As Integer)
            Dim dmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            Try
                dmArchiving.Connection_Open(Program.Sesion.Usuario.id)
                treeNode.Nodes.Clear()
                Dim SolicitudesFolder = dmArchiving.Schemadbo.CTA_Obtener_Solicitudes_Despacho_Folder.DBFindByid_Solicitud(idSolicitud)

                For Each solicitud As DBArchiving.Schemadbo.CTA_Obtener_Solicitudes_Despacho_FolderRow In SolicitudesFolder.Rows
                    treeNode.Nodes.Add("folder-" & solicitud.id_Solicitud, solicitud.CBarras_Folder)
                Next
                treeNode.Expand()

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CreaCarpetasSolicitud", ex)
            Finally
                dmArchiving.Connection_Close()
            End Try
        End Sub

        Private Sub CreaItemsSolicitud(ByRef treeNode As TreeNode, ByVal idSolicitud As Integer, ByVal CBarrasFolder As String)
            Dim dmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            Try
                dmArchiving.Connection_Open(Program.Sesion.Usuario.id)

                treeNode.Nodes.Clear()
                Dim dSolicitudes = dmArchiving.Schemadbo.CTA_Items_Solicitud.DBFindByfk_SolicitudCBarras_Folderfk_Estado(idSolicitud, CBarrasFolder, EstadoEnum.En_despacho)
                For Each solicitud In dSolicitudes
                    treeNode.Nodes.Add("file-" & solicitud.CBarras_File, solicitud.CBarras_File & "-" & solicitud.Nombre_Documento)
                Next
                treeNode.Expand()
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CreaItemsSolicitud", ex)
            Finally
                dmArchiving.Connection_Close()
            End Try
        End Sub

        Private Sub SolicitudesTreeView_AfterSelect(ByVal sender As System.Object, ByVal e As TreeViewEventArgs) Handles SolicitudesTreeView.AfterSelect
            Try
                If Not SolicitudesTreeView.SelectedNode Is Nothing Then
                    Dim idSolicitud As Integer

                    'Cuando se selecciona una Solicitud, se crean los folders de esta.
                    If SolicitudesTreeView.SelectedNode.Name.Contains("root") Then
                        idSolicitud = CInt(SolicitudesTreeView.SelectedNode.Name.Replace("root-", ""))
                        CreaCarpetasSolicitud(SolicitudesTreeView.SelectedNode, idSolicitud)
                    ElseIf SolicitudesTreeView.SelectedNode.Name.Contains("folder") Then
                        idSolicitud = CInt(SolicitudesTreeView.SelectedNode.Name.Replace("folder-", ""))
                        CreaItemsSolicitud(SolicitudesTreeView.SelectedNode, idSolicitud, SolicitudesTreeView.SelectedNode.Text)
                    ElseIf SolicitudesTreeView.SelectedNode.Name.Contains("file") Then
                        Dim File As String = SolicitudesTreeView.SelectedNode.Name.Replace("file-", "")
                        buscarGuia(File, SolicitudesTreeView.SelectedNode, idSolicitud, SolicitudesTreeView.SelectedNode.Text)
                    End If

                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("SolicitudesTreeView_AfterSelect", ex)
            End Try
        End Sub

        Private Sub CerrarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CerrarButton.Click
            Me.Close()
        End Sub

        Private Sub EntidadDesktopComboBox_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles EntidadDesktopComboBox.SelectedIndexChanged
            If (Me.EntidadDesktopComboBox.SelectedIndex <> 0) Then
                CargaFiltroProyecto(Program.Sesion.Usuario.id, CShort(Me.EntidadDesktopComboBox.SelectedValue))
            End If
        End Sub

#End Region

#Region " Metodos "

        Public Sub New()
            ' Llamada necesaria para el diseñador.
            InitializeComponent()
            ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
            cbarrasDesktopCBarrasControl.Init(Program.DesktopGlobal.ConnectionStrings.Archiving)

        End Sub

        Private Function DevuelveSeleccionado(ByVal comboParameter As DesktopComboBox.DesktopComboBoxControl) As Boolean
            Dim _retorno As Boolean = False
            Try
                If (comboParameter.SelectedIndex <> 0) Then
                    _retorno = True
                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Seleccionar Item", ex)
            End Try
            Return _retorno
        End Function

        Sub CrearTables()
            TableGuias = New DataTable
            TableGuias.Columns.Add("CBarras_File", GetType(String))
            TableGuias.Columns.Add("id_Item_Solicitud", GetType(Integer))
            TableGuias.Columns.Add("fk_Solicitud", GetType(Integer))
            TableGuias.Columns.Add("CBarras_Folder", GetType(String))
            TableGuias.Columns.Add("Nombre_solicitud_motivo", GetType(String))
            TableGuias.Columns.Add("Nombre_Solicitud_Tipo", GetType(String))
            TableGuias.Columns.Add("Nombre_Solicitud_Prioridad", GetType(String))
            TableGuias.Columns.Add("Nombre_Tipologia", GetType(String))
            TableGuias.Columns.Add("ID", GetType(Integer))
            TableGuias.Columns.Add("Orden", GetType(Integer))
            TableGuias.Columns.Add("id_Solicitud_Tipo", GetType(Integer))

            TableDestinatario = New DataTable
            TableDestinatario.Columns.Add("Nombre_Destinatario", GetType(String))
            TableDestinatario.Columns.Add("Identificacion", GetType(String))
            TableDestinatario.Columns.Add("ID", GetType(Integer))
            TableDestinatario.Columns.Add("Guia", GetType(String))
            TableDestinatario.Columns.Add("Sello", GetType(String))
            TableDestinatario.Columns.Add("CBarras_File", GetType(String))

            TableGuiasFiles = New DataTable
            TableGuiasFiles.Columns.Add("Guia", GetType(String))
            TableGuiasFiles.Columns.Add("Sello", GetType(String))
            TableGuiasFiles.Columns.Add("CBarras_File", GetType(String))
        End Sub

        Sub buscarGuia(ByVal CBarras_File As String, Optional NodoSelected As TreeNode = Nothing, Optional idSolicitud As Integer = Nothing, Optional NameNode As String = Nothing)
            Dim dmArchiving As DBArchivingDataBaseManager = Nothing

            Try
                dmArchiving = New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
                dmArchiving.Connection_Open(Program.Sesion.Usuario.id)
                Dim Datos = dmArchiving.Schemadbo.CTA_Solicitudes_Para_Guia.DBFindByCBarras_Filefk_Entidadfk_Sedefk_Boveda(CBarras_File, Nothing, Nothing, Nothing)

                If Datos.Count = 0 Then
                    If (TableDestinatario.Rows.Count > 0) Then
                        Dim EncontradoConGuia = Me.TableGuiasFiles.AsEnumerable().Where(Function(x) x("CBarras_File").ToString() = CBarras_File)

                        If (EncontradoConGuia IsNot Nothing) Then
                            DesktopMessageBoxControl.DesktopMessageShow("El codigo de barras que intenta procesar ya tiene una guia #" + EncontradoConGuia.CopyToDataTable().Rows(0)("Guia").ToString() + " asignado y un sello #" + EncontradoConGuia.CopyToDataTable().Rows(0)("Sello").ToString() + " asignado.", "Codigo de barras no encontrado", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                            Return
                        End If
                    End If
                    DesktopMessageBoxControl.DesktopMessageShow("El codigo de barras que intenta procesar no permite hacerlo debido a, (no esta en estado de Despacho, no existe o no esta en la boveda actual), por favor intente con otro.", "Codigo de barras no encontrado", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                Else
                    'almacena el usuario destinatario
                    Dim selloLocal As String = String.Empty

                    Dim viewUsuario As New DataView(TableDestinatario)
                    viewUsuario.RowFilter = Datos.IdColumn.ColumnName & "=" & Datos(0).Id
                    If viewUsuario.ToTable().Rows.Count = 0 Then
                        Dim FormGuia As New FormGuiaDespacho
                        If FormGuia.ShowDialog = DialogResult.OK And FormGuia.Guia <> "" And FormGuia.Sello <> "" Then
                            Dim rowDestinatario = TableDestinatario.NewRow
                            rowDestinatario("Nombre_Destinatario") = Datos(0).Destinatario
                            rowDestinatario("Identificacion") = Datos(0).Identificacion
                            rowDestinatario("ID") = Datos(0).Id
                            rowDestinatario("Guia") = FormGuia.Guia
                            rowDestinatario("Sello") = FormGuia.Sello
                            TableDestinatario.Rows.Add(rowDestinatario)
                            TableGuiasFiles.Rows.Add(FormGuia.Guia, FormGuia.Sello, CBarras_File)
                            guiaLocal = FormGuia.Guia
                            selloLocal = FormGuia.Sello
                        End If
                    Else
                        guiaLocal = viewUsuario.ToTable().Rows(0)("Guia").ToString()
                        selloLocal = viewUsuario.ToTable().Rows(0)("Sello").ToString()
                    End If

                    If guiaLocal <> "" And selloLocal <> "" Then
                        'Almacena los items que estan procesando actualmente
                        Dim view As New DataView(TableGuias)
                        view.RowFilter = Datos.CBarras_FileColumn.ColumnName & "='" & CBarras_File & "'"
                        If view.ToTable().Rows.Count = 0 Then
                            Dim row = TableGuias.NewRow
                            row("CBarras_File") = Datos(0).CBarras_File
                            row("id_Item_Solicitud") = Datos(0).id_Item_Solicitud
                            row("fk_Solicitud") = Datos(0).fk_Solicitud
                            row("CBarras_Folder") = Datos(0).CBarras_Folder
                            row("Nombre_solicitud_motivo") = Datos(0).Nombre_Solicitud_Motivo
                            row("Nombre_Solicitud_Tipo") = Datos(0).Nombre_solicitud_Tipo
                            row("Nombre_Solicitud_Prioridad") = Datos(0).Nombre_Solicitud_Prioridad
                            row("Nombre_Tipologia") = Datos(0).Nombre_Tipologia
                            row("ID") = Datos(0).Id
                            row("Orden") = Orden
                            row("id_Solicitud_Tipo") = Datos(0).id_solicitud_Tipo
                            Orden += 1
                            TableGuias.Rows.Add(row)
                            TableGuiasFiles.Rows.Add(guiaLocal, selloLocal, CBarras_File)
                            dmArchiving.Transaction_Begin()
                            Dim SolicitudItem As New DBArchiving.SchemaCustody.TBL_Solicitud_ItemType
                            SolicitudItem.fk_Estado = EstadoEnum.Asignado_a_Guia
                            dmArchiving.SchemaCustody.TBL_Solicitud_Item.DBUpdate(SolicitudItem, Datos(0).fk_Solicitud, Datos(0).id_Item_Solicitud)
                            dmArchiving.Transaction_Commit()
                        Else
                            DesktopMessageBoxControl.DesktopMessageShow("El codigo de barras que intenta procesar ya se encuantra procesado.", "Codigo de barras Procesado", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                        End If
                    Else
                        DesktopMessageBoxControl.DesktopMessageShow("No ha seleccionado una guia ni un sello.", "Codigo de barras Procesado", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                    End If

                    llenarcontroles(Datos(0).Id)
                    cbarrasDesktopCBarrasControl.Focus()
                    cbarrasDesktopCBarrasControl.SelectAll()
                End If

            Catch
                Throw
            Finally
                If (dmArchiving IsNot Nothing) Then dmArchiving.Connection_Close()
            End Try
        End Sub

        Sub llenarcontroles(ByVal nIdDestinatario As Integer)
            DestinatarioDesktopDataGridView.AutoGenerateColumns = False
            DestinatarioDesktopDataGridView.DataSource = TableDestinatario
            For Each row As DataGridViewRow In DestinatarioDesktopDataGridView.Rows
                If row.Cells("ID").Value.ToString() = CStr(nIdDestinatario) Then
                    row.Selected = True
                    Exit For
                End If
            Next

            ItemsDesktopDataGridView.DataSource = Nothing
            Dim viewItems As New DataView(TableGuias)
            viewItems.RowFilter = "ID = " & nIdDestinatario
            ItemsDesktopDataGridView.AutoGenerateColumns = False
            ItemsDesktopDataGridView.DataSource = viewItems.ToTable
        End Sub

        Sub EliminarItem(ByVal RowIndex As Integer, ByVal ColumnIndex As Integer)
            Dim Columna As String = ItemsDesktopDataGridView.Columns(ColumnIndex).Name

            If Columna = "Eliminar" Then
                If DesktopMessageBoxControl.DesktopMessageShow("Esta seguro que desea eliminar el item de la guia", "Eliminar Item Guia", DesktopMessageBoxControl.IconEnum.AdvertencyIcon) = DialogResult.OK Then
                    Dim dmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
                    dmArchiving.Connection_Open(Program.Sesion.Usuario.id)

                    Dim Row = ItemsDesktopDataGridView.Rows(RowIndex)
                    Dim fkSolicitud As Integer = CInt(Row.Cells("fk_Solicitud").Value)
                    Dim idItemSolicitud As Short = CShort(Row.Cells("id_Item_Solicitud").Value)
                    Dim IDLocal As Integer = CInt(Row.Cells("IDDestinatario").Value)

                    Dim SolicitudItem As New DBArchiving.SchemaCustody.TBL_Solicitud_ItemType
                    dmArchiving.Transaction_Begin()
                    SolicitudItem.fk_Estado = EstadoEnum.En_despacho
                    SolicitudItem.Orden = DBNull.Value
                    dmArchiving.SchemaCustody.TBL_Solicitud_Item.DBUpdate(SolicitudItem, fkSolicitud, idItemSolicitud)
                    dmArchiving.Transaction_Commit()

                    TableGuias.Rows.Remove(TableGuias.Select("fk_Solicitud = " & fkSolicitud & " AND id_Item_Solicitud = " & idItemSolicitud)(0))

                    Dim viewGuias As New DataView(TableGuias)
                    viewGuias.RowFilter = "ID=" & IDLocal

                    If viewGuias.ToTable().Rows.Count = 0 Then
                        TableDestinatario.Rows.Remove(TableDestinatario.Select("ID=" & IDLocal)(0))
                    End If

                    llenarcontroles(IDLocal)
                    dmArchiving.Connection_Close()

                End If
            End If
        End Sub

        Sub CerrarGuia(ByVal nIdDestinatario As Integer, ByVal nGuia As String, ByVal nSello As String)
            If DesktopMessageBoxControl.DesktopMessageShow("Esta seguro de que quiere cerrar la guia, Despues no podra agregar mas items a esta guia.", "Cerrar Guia", DesktopMessageBoxControl.IconEnum.AdvertencyIcon) = DialogResult.OK Then

                Dim dmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
                Dim dbmCore As New DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)

                dmArchiving.Connection_Open(Program.Sesion.Usuario.id)
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                Try
                    dmArchiving.Transaction_Begin()

                    'Crea la guia en base de datos
                    Dim GuiaType As New DBArchiving.SchemaCustody.TBL_Guia_DespachoType
                    GuiaType.Activa = True
                    GuiaType.Fecha_Log = SlygNullable.SysDate
                    GuiaType.fk_Estado = EstadoEnum.Enviado_a_prestamo
                    GuiaType.Guia = nGuia
                    GuiaType.id_Guia_Despacho = dmArchiving.SchemaCustody.TBL_Guia_Despacho.DBNextId
                    GuiaType.Sello = nSello
                    GuiaType.Usuario_Log = Program.Sesion.Usuario.id
                    dmArchiving.SchemaCustody.TBL_Guia_Despacho.DBInsert(GuiaType)

                    'Actualiza los estados de los items solicitud con la Nueva guia, orden de pistoleo y nuevo estado
                    Dim View As New DataView(TableGuias)
                    View.RowFilter = "ID = " & nIdDestinatario
                    For Each row As DataRow In View.ToTable().Rows
                        Dim SolicitudItem As New DBArchiving.SchemaCustody.TBL_Solicitud_ItemType
                        SolicitudItem.fk_Guia_Despacho = GuiaType.id_Guia_Despacho
                        SolicitudItem.Orden = CInt(row("Orden"))

                        Dim ModuloPrestamoDataTable = dbmCore.SchemaConfig.TBL_Parametro_Sistema.DBFindByfk_Entidadfk_ProyectoNombre_Parametro_Sistema(0, 0, "ModuloPrestamoImaging")
                        Dim ModuloPrestamo As DesktopConfig.Modulo = DesktopConfig.Modulo.Archiving

                        If ModuloPrestamoDataTable.Count = 1 Then
                            If ModuloPrestamoDataTable(0).Valor_Parametro_Sistema = "1" Then
                                ModuloPrestamo = DesktopConfig.Modulo.Imaging
                            End If
                        End If


                        ''Actualiza el estado de los documentos
                        'If CInt(row("id_Solicitud_Tipo")) = DesktopConfig.SolicitudTipo.Prestamo Then
                        '    Utilities.ActualizaEstadoFile(dmArchiving, dbmCore, row("CBarras_File").ToString(), Nothing, Nothing, DesktopConfig.Modulo.Imaging, EstadoEnum.Enviado_a_prestamo, Program.Sesion.Usuario.id, Nothing)
                        '    SolicitudItem.fk_Estado = EstadoEnum.Enviado_a_prestamo
                        'ElseIf CInt(row("id_Solicitud_Tipo")) = DesktopConfig.SolicitudTipo.Retiro Then
                        '    Utilities.ActualizaEstadoFile(dmArchiving, dbmCore, row("CBarras_File").ToString(), Nothing, Nothing, DesktopConfig.Modulo.Imaging, EstadoEnum.Enviado_al_cliente, Program.Sesion.Usuario.id, Nothing)
                        '    SolicitudItem.fk_Estado = EstadoEnum.Enviado_al_cliente
                        'End If

                        'Actualiza el estado de los documentos
                        If CInt(row("id_Solicitud_Tipo")) = DesktopConfig.SolicitudTipo.Prestamo Then
                            Utilities.ActualizaEstadoFile(dmArchiving, dbmCore, row("CBarras_File").ToString(), Nothing, Nothing, ModuloPrestamo, EstadoEnum.Enviado_a_prestamo, Program.Sesion.Usuario.id, Nothing)
                            SolicitudItem.fk_Estado = EstadoEnum.Enviado_a_prestamo
                        ElseIf CInt(row("id_Solicitud_Tipo")) = DesktopConfig.SolicitudTipo.Retiro Then
                            Utilities.ActualizaEstadoFile(dmArchiving, dbmCore, row("CBarras_File").ToString(), Nothing, Nothing, ModuloPrestamo, EstadoEnum.Enviado_al_cliente, Program.Sesion.Usuario.id, Nothing)
                            SolicitudItem.fk_Estado = EstadoEnum.Enviado_al_cliente
                        End If

                        'Actualiza el estado de los folders en custodia movimientos
                        Dim FolderCustodiaMovimiento = dbmCore.Schemadbo.CTA_Folder_En_Custodia.DBFindByCBarras_Folderfk_Expedienteid_Folder(row("CBarras_Folder").ToString(), Nothing, Nothing)
                        If FolderCustodiaMovimiento.Count = 0 Then
                            Dim Folder = dbmCore.SchemaProcess.TBL_Folder.DBFindByCBarras_Folder(row("CBarras_Folder").ToString())
                            Dim FolderMovimiento = dbmCore.SchemaCustody.TBL_Folder_Movimiento.DBFindByid_Folder_Movimientofk_Expedientefk_FolderFecha_InicialFecha_Final(Nothing, Folder(0).fk_Expediente, Folder(0).id_Folder, Nothing, DBNull.Value)

                            If FolderMovimiento.Count > 0 Then
                                Dim Movimiento As New DBCore.SchemaCustody.TBL_Folder_MovimientoType
                                Movimiento.Fecha_Final = SlygNullable.SysDate
                                dbmCore.SchemaCustody.TBL_Folder_Movimiento.DBUpdate(Movimiento, FolderMovimiento(0).id_Folder_Movimiento, FolderMovimiento(0).fk_Expediente, FolderMovimiento(0).fk_Folder)
                            End If
                        End If

                        dmArchiving.SchemaCustody.TBL_Solicitud_Item.DBUpdate(SolicitudItem, CInt(row("fk_solicitud")), CShort(row("id_Item_Solicitud")))

                        Dim nEntidad = CShort(EntidadDesktopComboBox.SelectedValue)
                        Dim nProyecto = CShort(ProyectoDesktopComboBox.SelectedValue)
                        If nEntidad > 0 And nProyecto > 0 Then

                            Dim DatatableProyecto = dmArchiving.Schemadbo.CTA_Proyecto_parametrizacion.DBFindByfk_Entidadid_Proyecto(nEntidad, nProyecto)
                            Dim Usa_control_envío_de_documentos = DatatableProyecto.Rows(0).Item("Usa_control_envío_de_documentos").ToString()
                            If CBool(Usa_control_envío_de_documentos) Then
                                'Crea el file soporte de entrega con el estado en transito
                                Dim DataTableLineaProceso = dmArchiving.Schemadbo.PA_Crea_File_Control_Documento.DBExecute(nEntidad,
                                                                                              nProyecto,
                                                                                              row("CBarras_File").ToString(),
                                                                                              Program.Sesion.Usuario.id,
                                                                                              nGuia, nSello,
                                                                                              Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad,
                                                                                              Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede,
                                                                                              Program.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento)

                                If DataTableLineaProceso.Rows.Count > 0 Then
                                    Dim Mensaje = "Se generó la linea de proceso : " & DataTableLineaProceso.Rows(0).Item("fk_Linea_Proceso").ToString()
                                    DesktopMessageBoxControl.DesktopMessageShow(Mensaje, "Devolución [Linea proceso: " & DataTableLineaProceso.Rows(0).Item("fk_Linea_Proceso").ToString() & "]", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                                End If
                            End If
                        End If
                    Next

                    'Inserta el movimiento de facturacion
                    InsertaMovimiento(dmArchiving, GuiaType.id_Guia_Despacho)

                    dmArchiving.Transaction_Commit()

                    'Actualiza variables de sesion de este proceso
                    Dim RowsGuias = TableGuias.Select("ID=" & nIdDestinatario)
                    For Each RowGuia As DataRow In RowsGuias
                        TableGuias.Rows.Remove(RowGuia)
                    Next
                    Dim RowsDestinatarios = TableDestinatario.Select("ID=" & nIdDestinatario)
                    For Each RowDest As DataRow In RowsDestinatarios
                        TableDestinatario.Rows.Remove(RowDest)
                    Next
                    llenarcontroles(nIdDestinatario)

                    DesktopMessageBoxControl.DesktopMessageShow("Se ha creado la guia con exito", "Guia OK", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)

                    'Impresion de la guia de despacho
                    ImprimirGuia(GuiaType.id_Guia_Despacho)
                Catch ex As Exception
                    dmArchiving.Transaction_Rollback()
                    DesktopMessageBoxControl.DesktopMessageShow("CerrarGuia", ex)
                Finally
                    dbmCore.Connection_Close()
                    dmArchiving.Connection_Close()
                End Try


            End If
        End Sub

        Public Sub InsertaMovimiento(ByVal dmArchiving As DBArchivingDataBaseManager, ByVal id_Guia As Integer)
            Dim EsquemaFacturacion = dmArchiving.Schemadbo.CTA_Movimiento_Guia.DBFindByfk_Guia_Despacho(id_Guia)
            If EsquemaFacturacion(0).Isfk_Esquema_FacturacionNull Then
                DesktopMessageBoxControl.DesktopMessageShow("El Esquema del proyecto no tiene un esquema de facturación parametrizado.", "Esquema de Facturación", DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
            Else
                Dim rowFactura = EsquemaFacturacion(0)
                Utilities.AgregarMovimiento(dmArchiving, rowFactura.fk_Entidad_Facturacion, rowFactura.fk_Esquema_Facturacion, DesktopConfig.Servicio_Facturacion.Despacho_Solicitudes, rowFactura.fk_Entidad, rowFactura.fk_Proyecto, rowFactura.fk_Esquema, 1, Program.Sesion.Usuario.id, Program.DesktopGlobal.CentroProcesamientoRow)
            End If
        End Sub

        Sub ImprimirGuia(ByVal Id_Guia As Integer)
            Dim dmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            Dim Table As DataTable
            Dim TableEncabezado As DataTable
            dmArchiving.Connection_Open(Program.Sesion.Usuario.id)

            If (CInt(dmArchiving.SchemaConfig.TBL_Parametro.DBGet("@ProximosVencerLetras").Rows(0).Item("Valor_Parametro")) = CInt(EntidadDesktopComboBox.SelectedValue)) Then
                Table = dmArchiving.SchemaReport.PA_Guia_Despacho_Solicitudes.DBExecute(Id_Guia)
                TableEncabezado = dmArchiving.SchemaReport.PA_Guia_Despacho_Solicitudes_Encabezado.DBExecute(Id_Guia)
            Else
                Table = dmArchiving.Schemadbo.RPT_Guia_Despacho_Solicitudes.DBFindByid_Guia_Despacho(Id_Guia)
                TableEncabezado = dmArchiving.Schemadbo.RPT_Guia_Despacho_Solicitudes_Encabezado.DBFindByid_Guia_Despacho(Id_Guia)
            End If

            dmArchiving.Connection_Close()
            Dim Reporte As New FormReporteguiaDespacho(Table, TableEncabezado, CInt(EntidadDesktopComboBox.SelectedValue))
            Reporte.ShowDialog()
        End Sub

#End Region


    End Class

End Namespace