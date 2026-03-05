Imports System.IO
Imports Miharu.Desktop.Controls.DesktopDataGridView
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Library
Imports System.Windows.Forms
Imports System.Drawing
Imports Slyg.Tools.Imaging
Imports Miharu.FileProvider.Library
Imports Slyg.Tools
Imports Miharu.Imaging.Indexer
Imports Miharu.Imaging.Indexer.Events

Namespace Procesos.Reproceso

    Public Class FormReprocesos
        Inherits FormBase


#Region " Declaraciones "

        Const MaxThumbnailWidth As Integer = 60
        Const MaxThumbnailHeight As Integer = 80

        Private WorkSpace As FormImagingWorkSpace

        Private Class FileIndex
            Public Property Expediente As Long
            Public Property Folder As Short
            Public Property File As Short

            Public Sub New(nExpediente As Long, nFolder As Short, nFile As Short)
                Me.Expediente = nExpediente
                Me.Folder = nFolder
                Me.File = nFile
            End Sub
        End Class

#End Region

#Region " Constructores "

        Private Sub New()
            ' Llamada necesaria para el diseñador.
            InitializeComponent()

            ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        End Sub

        Public Sub New(ByRef nWorkSpace As FormImagingWorkSpace)
            Me.New()

            Me.WorkSpace = nWorkSpace
        End Sub

#End Region

#Region " Propiedades "

        Public OT As Integer

#End Region

#Region " Eventos "

        Private Sub FormReprocesos_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            CargaPermisos()
            CargaControles()
        End Sub

        Private Sub BuscarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BuscarButton.Click
            BuscarReprocesos()
            CreaCheckAll()
        End Sub

        Private Sub ckBox_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
            Try
                Dim checked As Boolean = CType(sender, CheckBox).Checked
                ReprocesosDataGridView.ClearSelection()
                For i = 0 To Me.ReprocesosDataGridView.RowCount - 1
                    ReprocesosDataGridView.Rows(i).Cells(ReprocesosDataGridView.Columns("R").Index).Value = checked
                Next
                ReprocesosDataGridView.EndEdit()
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("ckBox_CheckedChanged", ex)
            End Try
        End Sub

        Private Sub FormReprocesos_SizeChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.SizeChanged
            If Not IsNothing(ReprocesosDataGridView.Columns("R")) Then
                CreaCheckAll()
            End If
        End Sub

        Private Sub ReemplazarImagenButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ReemplazarImagenButton.Click
            If ValidaSeleccionItems() Then
                If ReprocesosDataGridView.SelectedRows.Count = 1 Then
                    ReemplazarItems()
                Else
                    DesktopMessageBoxControl.DesktopMessageShow("Solamente se puede procesar una imágen a la vez.", "Reemplazo de Items", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                End If
            Else
                DesktopMessageBoxControl.DesktopMessageShow("No se ha seleccionado ningún elemento.", "Selección de Items", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
            End If
        End Sub

        Private Sub EliminaActualizacionButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles EliminaActualizacionButton.Click
            If ValidaSeleccionItems() Then
                If DesktopMessageBoxControl.DesktopMessageShow("¿Está seguro que desea Eliminar la actualización de los elementos seleccionados?", "Eliminar Actualización Item", DesktopMessageBoxControl.IconEnum.WarningIcon, False) = DialogResult.OK Then
                    EliminaActualizacionImagenes()
                End If
            Else
                DesktopMessageBoxControl.DesktopMessageShow("No se ha seleccionado ningún elemento.", "Selección de Items", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
            End If
        End Sub

        Private Sub EliminarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles EliminarButton.Click
            If ValidaSeleccionItems() Then
                If DesktopMessageBoxControl.DesktopMessageShow("¿Está seguro que desea Eliminar los elementos seleccionados?", "Eliminar Item", DesktopMessageBoxControl.IconEnum.WarningIcon, False) = DialogResult.OK Then
                    EliminaImagenes()
                End If
            Else
                DesktopMessageBoxControl.DesktopMessageShow("No se ha seleccionado ningún elemento.", "Selección de Items", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
            End If
        End Sub

        Private Sub CerrarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CerrarButton.Click
            Me.Close()
        End Sub

        Private Sub ReprocesosDataGridView_CellContentClick(ByVal sender As System.Object, ByVal e As DataGridViewCellEventArgs) Handles ReprocesosDataGridView.CellContentClick
            Try
                If e.RowIndex > -1 Then 'Si no es la fila de encabezados
                    Dim objCheck = CType(sender, DesktopDataGridViewControl).Rows(e.RowIndex).Cells(ReprocesosDataGridView.Columns("R").Index)

                    If CBool(objCheck.Value) Then
                        objCheck.Value = False
                    Else
                        objCheck.Value = True
                    End If
                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("ReprocesosDataGridView_CellClick", ex)
            End Try
        End Sub

        Private Sub ReprocesosDataGridView_CellContentDoubleClick(ByVal sender As System.Object, ByVal e As DataGridViewCellEventArgs) Handles ReprocesosDataGridView.CellDoubleClick
            Try
                If e.RowIndex > -1 Then 'Si no es la fila de encabezados
                    Dim rowData = CType(ReprocesosDataGridView.Rows(e.RowIndex).DataBoundItem, DataRowView).Row
                    VisualizaImagen(rowData)
                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("ReprocesosDataGridView_CellContentDoubleClick", ex)
            End Try
        End Sub

        Private Sub SedeoComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles SedeComboBox.SelectedIndexChanged
            Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Security)
            dbmSecurity.Connection_Open(Program.Sesion.Usuario.id)

            Dim CentroDataTable = dbmSecurity.SchemaConfig.TBL_Centro_Procesamiento.DBFindByfk_Entidadfk_Sede(Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, CShort(SedeComboBox.SelectedValue))

            If CStr(SedeComboBox.SelectedValue) = "-1" Then

                CentroComboBox.Enabled = False
                Utilities.LlenarCombo(CentroComboBox, CentroDataTable, CentroDataTable.id_Centro_ProcesamientoColumn.ColumnName, CentroDataTable.Nombre_Centro_ProcesamientoColumn.ColumnName, True, "-1", "Todos...")

            Else
                CentroComboBox.Enabled = True

                Utilities.LlenarCombo(CentroComboBox, CentroDataTable, CentroDataTable.id_Centro_ProcesamientoColumn.ColumnName, CentroDataTable.Nombre_Centro_ProcesamientoColumn.ColumnName, True, "-1", "Todos...")
                dbmSecurity.Connection_Close()
            End If


        End Sub

        Private Sub ProcesarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ProcesarButton.Click
            If ValidaSeleccionItems() Then
                If ReprocesosDataGridView.SelectedRows.Count = 1 Then
                    Me.Cursor = Cursors.AppStarting
                    ProcesarItem()
                    Me.Cursor = Cursors.Default
                Else
                    DesktopMessageBoxControl.DesktopMessageShow("Solamente se puede procesar una imágen a la vez.", "Reproceso de Items", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                End If
            Else
                DesktopMessageBoxControl.DesktopMessageShow("No se ha seleccionado ningún elemento.", "Selección de Items", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
            End If
        End Sub

        Private Sub ProcesarDemandaButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ProcesarDemandaButton.Click
            ProcesarDemanda()
        End Sub

        Private Sub reIndexarButton_Click(sender As System.Object, e As EventArgs) Handles reIndexarButton.Click
            ReIndexar()
        End Sub

#End Region

#Region " Metodos "

        Private Sub BuscarReprocesos()
            Me.Enabled = False
            Me.Cursor = Cursors.WaitCursor

            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)

                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                Dim numMotivo As SlygNullable(Of Short) = Nothing
                Dim numSede As SlygNullable(Of Short) = Nothing
                Dim numCentro As SlygNullable(Of Short) = Nothing

                If CStr(MotivoComboBox.SelectedValue) <> "-1" Then numMotivo = CShort(MotivoComboBox.SelectedValue)
                If CStr(SedeComboBox.SelectedValue) <> "-1" Then numSede = CShort(SedeComboBox.SelectedValue)
                If CStr(CentroComboBox.SelectedValue) <> "-1" Then numCentro = CShort(CentroComboBox.SelectedValue)

                Dim ReprocesosDataTable = dbmImaging.SchemaProcess.PA_Obtiene_Items_Reproceso.DBExecute(Program.ImagingGlobal.ProyectoImagingRow.fk_Entidad, _
                                                                                                        Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto, _
                                                                                                        Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, _
                                                                                                        numSede, _
                                                                                                        numCentro, _
                                                                                                        numMotivo, _
                                                                                                        Me.OT)

                If ReprocesosDataTable.Rows.Count > 0 Then
                    ReprocesosDataGridView.AutoGenerateColumns = False
                    ReprocesosDataGridView.DataSource = ReprocesosDataTable

                    AutoNumberRowsForGridView(ReprocesosDataGridView)
                Else
                    ReprocesosDataGridView.AutoGenerateColumns = False
                    ReprocesosDataGridView.DataSource = Nothing
                    DesktopMessageBoxControl.DesktopMessageShow("No se encontraron registros para esta búsqueda.", "Búsqueda de Registros", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                End If

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("BuscarReprocesos", ex)
            Finally
                Me.Enabled = True
                Me.Cursor = Cursors.Default

                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

        Private Sub CreaCheckAll()
            'Se elimina si ya existe
            If ReprocesosDataGridView.Controls.Count >= 3 Then
                Me.ReprocesosDataGridView.Controls.Remove(ReprocesosDataGridView.Controls(2))
            End If

            'Control para seleccionar todos los elementos de la grilla
            If ReprocesosDataGridView.RowCount > 0 Then
                Dim ckBox As New CheckBox()
                Dim rect As Rectangle = Me.ReprocesosDataGridView.GetCellDisplayRectangle(ReprocesosDataGridView.Columns("R").Index, -1, True)
                ckBox.Size = New Size(18, 18)
                'Para ajustar la posición donde se pinta
                rect.X += 3
                ckBox.Location = rect.Location
                ckBox.BackColor = Color.Transparent
                AddHandler ckBox.CheckedChanged, AddressOf ckBox_CheckedChanged
                Me.ReprocesosDataGridView.Controls.Add(ckBox)
            End If
        End Sub

        Private Sub CargaControles()
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim dbmSecurity As DBSecurity.DBSecurityDataBaseManager = Nothing

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmSecurity = New DBSecurity.DBSecurityDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Security)

                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
                dbmSecurity.Connection_Open(Program.Sesion.Usuario.id)

                Dim MotivoDataTable = dbmImaging.SchemaProcess.TBL_Reproceso_Motivo.DBGet(Nothing)

                Dim SedeDataTable = dbmSecurity.SchemaConfig.TBL_Sede.DBGet(Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, Nothing)

                Dim CentroDataTable = dbmSecurity.SchemaConfig.TBL_Centro_Procesamiento.DBGet(Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede, Nothing)
                Utilities.LlenarCombo(MotivoComboBox, MotivoDataTable, MotivoDataTable.Id_Reproceso_MotivoColumn.ColumnName, MotivoDataTable.Nombre_Reproceso_MotivoColumn.ColumnName, True, "-1", "Todos...")

                Utilities.LlenarCombo(SedeComboBox, SedeDataTable, SedeDataTable.id_SedeColumn.ColumnName, SedeDataTable.Nombre_SedeColumn.ColumnName, True, "-1", "Todos...")
                SedeComboBox.SelectedValue = Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede
                Utilities.LlenarCombo(CentroComboBox, CentroDataTable, CentroDataTable.id_Centro_ProcesamientoColumn.ColumnName, CentroDataTable.Nombre_Centro_ProcesamientoColumn.ColumnName, True, "-1", "Todos...")
                CentroComboBox.SelectedValue = Program.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaControles", ex)

            Finally
                If (dbmSecurity IsNot Nothing) Then dbmSecurity.Connection_Close()
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

        Private Sub EliminaImagenes()
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim dbmCore As DBCore.DBCoreDataBaseManager
            Dim manager As FileProviderManager = Nothing

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)

                'dbmCore.LinkDataBaseManager(dbmImaging)

                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
                'dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                Dim servidor = dbmImaging.SchemaCore.CTA_Servidor.DBFindByfk_Entidadid_Servidor(Program.DesktopGlobal.ServidorImagenRow.fk_Entidad, Program.DesktopGlobal.ServidorImagenRow.id_Servidor)(0).ToCTA_ServidorSimpleType()
                Dim centro = dbmImaging.SchemaSecurity.CTA_Centro_Procesamiento.DBFindByfk_Entidadfk_Sedeid_Centro_Procesamiento(Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede, Program.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento)(0).ToCTA_Centro_ProcesamientoSimpleType()
                manager = New FileProviderManager(servidor, centro, dbmImaging, Program.Sesion.Usuario.id)
                manager.Connect()

                dbmImaging.Transaction_Begin()
                'dbmCore.Transaction_Begin()

                WorkSpace.EventManager.MessageEliminados = ""

                For Each row As DataGridViewRow In ReprocesosDataGridView.Rows
                    If CBool(row.Cells(ReprocesosDataGridView.Columns("R").Index).Value) Then
                        Dim rowData = CType(row.DataBoundItem, DataRowView).Row

                        Dim idExpediente = CLng(rowData("fk_Expediente"))
                        Dim idFolder = CShort(rowData("fk_Folder"))
                        Dim idFile = CShort(rowData("fk_File"))

                        'Se llama el evento Para marcar imagenes como eliminadas
                        Me.WorkSpace.EventManager.EliminarImagen(idExpediente, idFolder, idFile)

                        ' Se elimina de  las tablas TBL_Dashboard_Capturas -TBL_Dashboard_Validaciones
                        dbmImaging.SchemaProcess.TBL_Dashboard_Capturas.DBDelete(idExpediente, idFolder, idFile)
                        dbmImaging.SchemaProcess.TBL_Dashboard_Validaciones.DBDelete(idExpediente, idFolder, idFile)

                        ' Se actualiza en la tabla de Core El estado del file en el modulo de Imagenes
                        Dim newFilesEstadoType As New DBCore.SchemaProcess.TBL_File_EstadoType()
                        newFilesEstadoType.fk_Estado = DBCore.EstadoEnum.Eliminado
                        newFilesEstadoType.fk_Usuario = Program.Sesion.Usuario.id
                        newFilesEstadoType.Fecha_Log = SlygNullable.SysDate

                        dbmCore.SchemaProcess.TBL_File_Estado.DBUpdate(newFilesEstadoType, idExpediente, idFolder, idFile, DesktopConfig.Modulo.Imaging)

                        'Dim VersionesDataTable = dbmCore.SchemaImaging.TBL_File.DBGet(idExpediente, idFolder, idFile, Nothing)
                        'For Each tblFileRow In VersionesDataTable
                        '    manager.DeleteItem(idExpediente, idFolder, idFile, tblFileRow.id_Version)
                        'Next

                        Me.WorkSpace.EventManager.FinalizarReproceso(idExpediente, idFolder, idFile, 1)
                    End If
                Next

                If (Me.WorkSpace.EventManager.MessageEliminados <> "") Then
                    DesktopMessageBoxControl.DesktopMessageShow("No pudieron Eliminar las transacciones con los siguientes id_Core_Index pues ya se encuentran Facturados en el sistema :  " + Me.WorkSpace.EventManager.MessageEliminados, "Eliminación de Imagenes", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                End If

                dbmImaging.Transaction_Commit()
                'dbmCore.Transaction_Commit()
                manager.TransactionCommit()

                BuscarReprocesos()
            Catch ex As Exception
                'If (dbmCore IsNot Nothing) Then dbmCore.Transaction_Rollback()
                If (dbmImaging IsNot Nothing) Then dbmImaging.Transaction_Rollback()

                DesktopMessageBoxControl.DesktopMessageShow("EliminaImagenes", ex)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                'If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                If (manager IsNot Nothing) Then manager.Disconnect()
            End Try
        End Sub

        Private Sub VisualizaImagen(ByVal rowData As DataRow)
            Try
                'Se obtienen los datos del file para crear la imagen

                Dim Expediente = CLng(rowData("fk_Expediente"))
                Dim Folder = CShort(rowData("fk_Folder"))
                Dim File = CShort(rowData("fk_File"))

                Me.Cursor = Cursors.AppStarting
                Dim formVisor As New FormVisorImagen(Expediente, Folder, File)
                formVisor.ShowDialog()
                Me.Cursor = Cursors.Default

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("VisualizaImagen", ex)
            End Try
        End Sub

        Private Sub ReemplazarItems()
            Dim dbmCore As DBCore.DBCoreDataBaseManager
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim manager As FileProviderManager = Nothing

            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)

                dbmCore.Connection_Open(Program.Sesion.Usuario.id)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                Dim servidor = dbmImaging.SchemaCore.CTA_Servidor.DBFindByfk_Entidadid_Servidor(Program.DesktopGlobal.ServidorImagenRow.fk_Entidad, Program.DesktopGlobal.ServidorImagenRow.id_Servidor)(0).ToCTA_ServidorSimpleType()
                Dim centro = dbmImaging.SchemaSecurity.CTA_Centro_Procesamiento.DBFindByfk_Entidadfk_Sedeid_Centro_Procesamiento(Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede, Program.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento)(0).ToCTA_Centro_ProcesamientoSimpleType()
                manager = New FileProviderManager(servidor, centro, dbmImaging, Program.Sesion.Usuario.id)
                manager.Connect()

                'Se seleccionan las nuevas imágenes
                Dim ImagenOpenFileDialog = New OpenFileDialog() With {
                    .Multiselect = False,
                    .Title = "Seleccionar la nueva imagen",
                    .Filter = "Imágenes (*" & Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Entrada & ")|*" & Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Entrada & ""
                }

                Dim Respuesta = ImagenOpenFileDialog.ShowDialog()
                If Respuesta = DialogResult.OK Then
                    manager.TransactionBegin()
                    dbmImaging.Transaction_Begin()

                    Dim rowData = CType(ReprocesosDataGridView.SelectedRows(0).DataBoundItem, DataRowView).Row

                    Dim idExpediente = CLng(rowData("fk_Expediente"))
                    Dim idFolder = CShort(rowData("fk_Folder"))
                    Dim idFile = CShort(rowData("fk_File"))

                    Dim DashBoardType As New DBImaging.SchemaProcess.TBL_Dashboard_CapturasType()
                    DashBoardType.Actualizado = True
                    dbmImaging.SchemaProcess.TBL_Dashboard_Capturas.DBUpdate(DashBoardType, idExpediente, idFolder, idFile)

                    Dim FileImgType As New DBCore.SchemaImaging.TBL_FileType()
                    FileImgType.fk_Expediente = idExpediente
                    FileImgType.fk_Folder = idFolder
                    FileImgType.fk_File = idFile
                    FileImgType.id_Version = dbmImaging.SchemaProcess.TBL_File.DBNextId_for_id_Version(idExpediente, idFolder, idFile)
                    FileImgType.File_Unique_Identifier = Guid.NewGuid()
                    FileImgType.Folios_Documento_File = CShort(ImageManager.GetFolios(ImagenOpenFileDialog.FileName))
                    FileImgType.Tamaño_Imagen_File = 0
                    FileImgType.Nombre_Imagen_File = Path.GetFileName(ImagenOpenFileDialog.FileName)
                    FileImgType.Key_Cargue_Item = FileImgType.Nombre_Imagen_File
                    FileImgType.Save_FileName = FileImgType.Nombre_Imagen_File
                    FileImgType.fk_Content_Type = Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida
                    FileImgType.fk_Usuario_Log = Program.Sesion.Usuario.id
                    FileImgType.Validaciones_Opcionales = False
                    FileImgType.Es_Anexo = False
                    FileImgType.fk_Anexo = Nothing
                    FileImgType.fk_Entidad_Servidor = Program.DesktopGlobal.ServidorImagenRow.fk_Entidad
                    FileImgType.fk_Servidor = Program.DesktopGlobal.ServidorImagenRow.id_Servidor
                    FileImgType.Fecha_Creacion = SlygNullable.SysDate
                    FileImgType.Fecha_Transferencia = Nothing
                    dbmCore.SchemaImaging.TBL_File.DBInsert(FileImgType)

                    'Crea la nueva versión del file e inserta la nueva imágen.
                    Dim FileCoreType As New DBImaging.SchemaProcess.TBL_FileType
                    FileCoreType.fk_Expediente = idExpediente
                    FileCoreType.fk_Folder = idFolder
                    FileCoreType.fk_File = idFile
                    FileCoreType.id_Version = FileImgType.id_Version
                    dbmImaging.SchemaProcess.TBL_File.DBInsert(FileCoreType)

                    manager.CreateItem(idExpediente, idFolder, idFile, FileImgType.id_Version, Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida, FileImgType.File_Unique_Identifier)

                    Dim Formato = Utilities.GetEnumFormat(Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida)
                    Dim Compresion = Utilities.GetEnumCompression(CType(Program.ImagingGlobal.ProyectoImagingRow.id_Formato_Imagen_Salida, DesktopConfig.FormatoImagenEnum))

                    'Se crean los folios del file.
                    For nFolioImage = 1 To FileImgType.Folios_Documento_File
                        Dim Image_Binary = ImageManager.GetFolioData(ImagenOpenFileDialog.FileName, nFolioImage, Formato, Compresion)
                        Dim Thumbnail_Binary = ImageManager.GetThumbnailData(ImagenOpenFileDialog.FileName, nFolioImage, nFolioImage, MaxThumbnailWidth, MaxThumbnailHeight)

                        manager.CreateFolio(idExpediente, idFolder, idFile, FileImgType.id_Version, CShort(nFolioImage), Image_Binary, Thumbnail_Binary(0), False)
                    Next

                    dbmImaging.Transaction_Commit()
                    manager.TransactionCommit()

                End If

                BuscarReprocesos()
            Catch ex As Exception
                If (dbmImaging IsNot Nothing) Then dbmImaging.Transaction_Rollback()
                If (manager IsNot Nothing) Then manager.TransactionRollback()

                Application.DoEvents()

                DesktopMessageBoxControl.DesktopMessageShow("ReemplazarItems", ex)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                If (manager IsNot Nothing) Then manager.Disconnect()

                DesktopMessageBoxControl.DesktopMessageShow("Se realizó la actualización de los items correctamente.", "Reemplazo de Items", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
            End Try
        End Sub


        Private Sub EliminaActualizacionImagenes()
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)

                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
                dbmImaging.Transaction_Begin()

                For Each row As DataGridViewRow In ReprocesosDataGridView.Rows
                    If CBool(row.Cells(ReprocesosDataGridView.Columns("R").Index).Value) Then
                        Dim rowData = CType(row.DataBoundItem, DataRowView).Row

                        Dim Expediente = CLng(rowData("fk_Expediente"))
                        Dim Folder = CShort(rowData("fk_Folder"))
                        Dim File = CShort(rowData("fk_File"))

                        'Se actualiza el File de Imaging, establece Actualizado en False
                        Dim DashBoardType As New DBImaging.SchemaProcess.TBL_Dashboard_CapturasType()
                        DashBoardType.Actualizado = False
                        dbmImaging.SchemaProcess.TBL_Dashboard_Capturas.DBUpdate(DashBoardType, Expediente, Folder, File)
                    End If
                Next
                dbmImaging.Transaction_Commit()

                BuscarReprocesos()
            Catch ex As Exception
                If (dbmImaging IsNot Nothing) Then dbmImaging.Transaction_Rollback()
                DesktopMessageBoxControl.DesktopMessageShow("EliminaActualizacionImagenes", ex)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

        Public Sub AutoNumberRowsForGridView(ByVal dataGridView As DataGridView)
            If dataGridView IsNot Nothing Then
                Dim count As Integer = 0
                While (count <= (dataGridView.Rows.Count - 1))
                    dataGridView.Rows(count).HeaderCell.Value = String.Format((count + 1).ToString(), "0")
                    count += 1
                End While

                dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
            End If

        End Sub

        Private Sub CargaPermisos()
            Try
                ''Se habilitan los botones de acuerdo al perfil del usuario
                SedeComboBox.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Imaging.Proceso.Captura.Reprocesos)
                CentroComboBox.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Imaging.Proceso.Captura.Reprocesos)
                SedeProcesaminetoL.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Imaging.Proceso.Captura.Reprocesos)
                CentroProcesamientoL.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Imaging.Proceso.Captura.Reprocesos)

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaPermisos", ex)
            End Try
        End Sub

        Private Sub ProcesarItem()
            If (ValidaSeleccionItems()) Then
                If (ReprocesosDataGridView.SelectedRows.Count = 1) Then
                    Dim Expediente = CLng(ReprocesosDataGridView.SelectedRows(0).Cells("fk_Expediente").Value)
                    Dim Folder = CShort(ReprocesosDataGridView.SelectedRows(0).Cells("fk_Folder").Value)
                    Dim File = CShort(ReprocesosDataGridView.SelectedRows(0).Cells("fk_File").Value)

                    'Controlador.SetData(Expediente, Folder, File)
                    Dim Parameters = New Object() {Expediente, Folder, File}

                    LaunchUtil.Indexar(OT, GetType(Controller.Indexer.Capturas.Reproceso.ReProcessController), DBCore.EstadoEnum.Reproceso, LaunchUtil.EtapaEnum.Reprocesos, Me.WorkSpace, Me.ProcesarButton, False, "SetData", Parameters)

                    Me.WorkSpace.EventManager.FinalizarReproceso(Expediente, Folder, File, 1)

                    BuscarReprocesos()
                Else
                    DesktopMessageBoxControl.DesktopMessageShow("Solamente se puede procesar una imágen a la vez.", "Reproceso de Items", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                End If
            Else
                DesktopMessageBoxControl.DesktopMessageShow("No se ha seleccionado ningún elemento.", "Selección de Items", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
            End If
        End Sub

        Private Sub ProcesarDemanda()
            If (CStr(MotivoComboBox.SelectedValue) <> "-1") Then
                Dim numMotivo As Short
                Dim numSede As SlygNullable(Of Short) = Nothing
                Dim numCentro As SlygNullable(Of Short) = Nothing

                numMotivo = CShort(MotivoComboBox.SelectedValue)
                If CStr(Me.SedeComboBox.SelectedValue) <> "-1" Then numSede = CShort(SedeComboBox.SelectedValue)
                If CStr(Me.CentroComboBox.SelectedValue) <> "-1" Then numCentro = CShort(CentroComboBox.SelectedValue)

                'Controlador.SetData(numSede, numCentro, numMotivo)
                Dim Parameters = New Object() {numSede, numCentro, numMotivo}

                LaunchUtil.Indexar(OT, GetType(Controller.Indexer.Capturas.Reproceso.ReProcessDemandaController), DBCore.EstadoEnum.Reproceso, LaunchUtil.EtapaEnum.Reprocesos, Me.WorkSpace, Me.ProcesarDemandaButton, True, "SetData", Parameters)

                If (ReprocesosDataGridView.RowCount > 0) Then
                    BuscarReprocesos()
                End If
            Else
                MessageBox.Show("Para realizar la reclasificación por demanda se debe seleccionar un motivo de reproceso", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End Sub

        Private Sub ReIndexar()
            Dim listaFiles = New List(Of FileIndex)

            For Each row As DataGridViewRow In ReprocesosDataGridView.Rows
                If CBool(row.Cells(ReprocesosDataGridView.Columns("R").Index).Value) Then
                    Dim rowData = CType(row.DataBoundItem, DataRowView).Row

                    Dim idExpediente = CLng(rowData("fk_Expediente"))
                    Dim idFolder = CShort(rowData("fk_Folder"))
                    Dim idFile = CShort(rowData("fk_File"))

                    listaFiles.Add(New FileIndex(idExpediente, idFolder, idFile))
                End If
            Next

            If (listaFiles.Count = 0) Then
                MessageBox.Show("Debe seleccionar almenos un registro para Re Indexar", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)

                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                ' Liberar los registros asignados anteriores
                Dim BloqueoDatatable = dbmImaging.SchemaProcess.PA_Dashboard_Capturas_get.DBExecute(Program.DesktopGlobal.SesionId)
                If BloqueoDatatable.Count > 0 Then
                    For Each BloqueoRow In BloqueoDatatable
                        dbmImaging.SchemaProcess.PA_Dashboard_Desbloquear_Capturas.DBExecute(BloqueoRow.fk_Expediente, BloqueoRow.fk_Folder, BloqueoRow.fk_File)
                    Next
                End If

                dbmImaging.Transaction_Begin()

                ' Validar si los Registros están disponibles
                Dim cargue As Integer = -1
                Dim paquete As Short = -1
                For Each listaFile In listaFiles
                    Dim dashboardTable = dbmImaging.SchemaProcess.TBL_Dashboard_Capturas.DBGet(listaFile.Expediente, listaFile.Folder, listaFile.File)

                    If (dashboardTable.Count = 0) Then
                        Throw New Exception("El registro no se encuentra disponible para Reproceso. Ex: " & listaFile.Expediente & ", Fo: " & listaFile.Folder & ", Fi: " & listaFile.File)
                    End If

                    Dim dashboardRow = dashboardTable(0)
                    If (Not dashboardRow.IsSesionNull() AndAlso dashboardRow.Sesion.ToString() <> Program.DesktopGlobal.SesionId.ToString()) Then
                        Throw New Exception("El registro está asignado a otro usuario. Ex: " & listaFile.Expediente & ", Fo: " & listaFile.Folder & ", Fi: " & listaFile.File)
                    End If

                    If (cargue = -1) Then
                        cargue = dashboardRow.fk_Cargue
                        paquete = dashboardRow.fk_Cargue_Paquete
                    End If

                    If (cargue <> dashboardRow.fk_Cargue Or paquete <> dashboardRow.fk_Cargue_Paquete) Then
                        Throw New Exception("Solo se pueden Reindexar Documentos del mismo Cargue y Paquete")
                    End If

                    Dim dashboardType As New DBImaging.SchemaProcess.TBL_Dashboard_CapturasType()

                    dashboardType.fk_Usuario_log = Program.Sesion.Usuario.id
                    dashboardType.Sesion = Program.DesktopGlobal.SesionId
                    dashboardType.PCName = Program.DesktopGlobal.PcName

                    dbmImaging.SchemaProcess.TBL_Dashboard_Capturas.DBUpdate(dashboardType, listaFile.Expediente, listaFile.Folder, listaFile.File)
                Next

                dbmImaging.Transaction_Commit()
            Catch ex As Exception
                If (dbmImaging IsNot Nothing) Then dbmImaging.Transaction_Rollback()
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try

            LaunchUtil.Indexar(OT, GetType(Controller.Indexer.Indexacion.ReIndexacionController), DBCore.EstadoEnum.Reproceso, LaunchUtil.EtapaEnum.Reprocesos, Me.WorkSpace, Me.reIndexarButton, False)

            BuscarReprocesos()
        End Sub

#End Region

#Region " Funciones "


        'Private Function GetSize(ByVal FileName As String, ByVal folios As Short, ByVal Formato As Slyg.Tools.Imaging.ImageManager.EnumFormat, ByVal Compresion As Slyg.Tools.Imaging.ImageManager.EnumCompression) As Long

        '    Dim Peso As Long = 0
        '    For nFolioImage = 1 To folios
        '        Dim Image_Binary = ImageManager.GetFolioData(FileName, nFolioImage, Formato, Compresion)
        '        Peso = Peso + Image_Binary.Length
        '    Next

        '    Return Peso

        'End Function

        Private Function ValidaSeleccionItems() As Boolean
            Dim bReturn As Boolean = False
            Dim nSelecionados As Integer = 0
            Try
                For Each row As DataGridViewRow In ReprocesosDataGridView.Rows
                    If CBool(row.Cells(ReprocesosDataGridView.Columns("R").Index).Value) = True Then
                        nSelecionados += 1
                    End If
                Next

                If nSelecionados > 0 Then bReturn = True
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("ValidaSeleccionItems", ex)
            End Try
            Return bReturn
        End Function

#End Region

    End Class

End Namespace