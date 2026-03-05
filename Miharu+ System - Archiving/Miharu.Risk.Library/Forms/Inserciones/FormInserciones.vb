Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports DBArchiving
Imports DBArchiving.SchemaRisk
Imports DBCore.SchemaProcess
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Config
Imports Miharu.Risk.Library.Forms.CBarras
Imports Slyg.Tools

Namespace Forms.Inserciones

    Public Class FormInserciones
        Inherits FormBase

#Region " Declaraciones "

        Private CTAExpedienteLLaveDataTable As New CTA_Expediente_LLaveDataTable()
        Private CTAFileDataTable As New CTA_FileDataTable()
        Private idDocumento As Integer

#End Region

#Region " Propiedades "

        Public Property Folder As DesktopConfig.Folder

#End Region

#Region " Eventos "

        Private Sub CerrarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CerrarButton.Click
            Me.Close()
        End Sub

        Private Sub InsertarDocumentoButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles InsertarDocumentoButton.Click
            InsertarDocumento()
        End Sub

        Private Sub InsertarFolderButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles InsertarFolderButton.Click
            InsertarFolder()
        End Sub

#End Region

#Region " Metodos "

        Public Sub LoadData()
            CBarrasLabel.Text = Me.Folder.CBarras_Folder

            Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)

            Try
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                CTAExpedienteLLaveDataTable = dbmCore.SchemaProcess.CTA_Expediente_LLave.DBFindByid_Expediente(Me.Folder.fk_Expediente)
                CTAFileDataTable = dbmCore.SchemaProcess.CTA_File.DBFindByfk_Expedientefk_Folderid_File(Me.Folder.fk_Expediente, Me.Folder.id_Folder, Nothing)

                Dim DocumentoTable = dbmCore.SchemaConfig.TBL_Documento.DBFindByfk_Entidadfk_Proyectofk_Esquemafk_TipologiaEliminado(Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, Folder.fk_Esquema, CShort(1), False) 'Anexos

                If (DocumentoTable.Count > 0) Then
                    idDocumento = DocumentoTable(0).id_Documento
                    InsertarFolderButton.Enabled = True
                Else
                    InsertarFolderButton.Enabled = False
                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("LoadData", ex)
            Finally
                dbmCore.Connection_Close()
            End Try

            CTAExpedienteLLaveDataTableBindingSource.DataSource = CTAExpedienteLLaveDataTable
            CTAFileDataTableBindingSource.DataSource = CTAFileDataTable

        End Sub

        Private Sub InsertarDocumento()
            Dim DocumentoRow = CType(CType(DocumentosDataGridView.CurrentRow.DataBoundItem, DataRowView).Row, CTA_FileRow)

            Dim f = New FormInsercion()
            f.Tipologia = DocumentoRow.Nombre_Tipologia

            Dim Respuesta = f.ShowDialog()

            If (Respuesta = DialogResult.OK) Then
                Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                Dim DBMrchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)

                Try
                    dbmCore.Connection_Open(Program.Sesion.Usuario.id)
                    DBMrchiving.Connection_Open(Program.Sesion.Usuario.id)

                    dbmCore.Transaction_Begin()
                    DBMrchiving.Transaction_Begin()

                    ' Actualizar los folios
                    Dim FileType As New DBCore.SchemaProcess.TBL_FileType()
                    FileType.Folios_File = DocumentoRow.Folios_File + f.Folios
                    dbmCore.SchemaProcess.TBL_File.DBUpdate(FileType, DocumentoRow.fk_Expediente, DocumentoRow.fk_Folder, DocumentoRow.id_File)

                    ' Reportar la inserción
                    Dim InsercionType As New TBL_InsercionesType()
                    InsercionType.fk_Expediente = DocumentoRow.fk_Expediente
                    InsercionType.fk_Folder = DocumentoRow.fk_Folder
                    InsercionType.fk_File = DocumentoRow.id_File
                    InsercionType.id_Insercion = DBMrchiving.SchemaRisk.TBL_Inserciones.DBNextId_for_id_Insercion(DocumentoRow.fk_Expediente, DocumentoRow.fk_Folder, DocumentoRow.id_File)
                    InsercionType.Folios = f.Folios
                    InsercionType.Fecha_Insercion = SlygNullable.SysDate
                    InsercionType.fk_Usuario_Log = Program.Sesion.Usuario.id
                    DBMrchiving.SchemaRisk.TBL_Inserciones.DBInsert(InsercionType)

                    ' Actualizar el estado del File para dejarlo en entrada de boveda
                    Dim FileEstadoType As New TBL_File_EstadoType()
                    FileEstadoType.fk_Estado = DBCore.EstadoEnum.Por_Custodiar
                    dbmCore.SchemaProcess.TBL_File_Estado.DBUpdate(FileEstadoType, DocumentoRow.fk_Expediente, DocumentoRow.fk_Folder, DocumentoRow.id_File, DesktopConfig.Modulo.Archiving)

                    CTAFileDataTable = dbmCore.SchemaProcess.CTA_File.DBFindByfk_Expedientefk_Folderid_File(Me.Folder.fk_Expediente, Me.Folder.id_Folder, Nothing)
                    CTAFileDataTableBindingSource.DataSource = CTAFileDataTable

                    dbmCore.Transaction_Commit()
                    DBMrchiving.Transaction_Commit()

                    DocumentoRow.Folios_File = f.Folios

                    MessageBox.Show("Se insertó con éxito " & f.Folios & " nuevo(s) folio(s) al documento: " & f.Tipologia & " con código de barras: " & DocumentoRow.CBarras_File, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)

                Catch ex As Exception
                    dbmCore.Transaction_Rollback()
                    DBMrchiving.Transaction_Rollback()

                    DesktopMessageBoxControl.DesktopMessageShow("Insertar", ex)
                Finally
                    dbmCore.Connection_Close()
                    DBMrchiving.Connection_Close()
                End Try
            End If
        End Sub

        Private Sub InsertarFolder()
            Dim f = New FormInsercion()
            f.Tipologia = "Anexos"

            Dim Respuesta = f.ShowDialog()

            If (Respuesta = DialogResult.OK) Then
                Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                Dim DBMrchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
                Dim idFile As Short

                Try
                    dbmCore.Connection_Open(Program.Sesion.Usuario.id)
                    DBMrchiving.Connection_Open(Program.Sesion.Usuario.id)

                    dbmCore.Transaction_Begin()
                    DBMrchiving.Transaction_Begin()

                    ' Crear el File Anexos si no existe
                    Dim FileTable = dbmCore.SchemaProcess.TBL_File.DBFindByfk_Expedientefk_Folderfk_Documento(Folder.fk_Expediente, Folder.id_Folder, 1) ' Anexos
                    Dim Imprimir As Boolean = False

                    If (FileTable.Count = 0) Then
                        Imprimir = True
                        Dim FileType As New DBCore.SchemaProcess.TBL_FileType()

                        idFile = Utilities.InsertaFileCore(dbmCore, DBMrchiving, Folder.fk_Expediente, Folder.id_Folder, idDocumento, Program.RiskGlobal)

                        FileTable = dbmCore.SchemaProcess.TBL_File.DBGet(Folder.fk_Expediente, Folder.id_Folder, idFile) ' Anexos

                        ' Actualizar los folios                    
                        FileType.Folios_File = f.Folios
                        dbmCore.SchemaProcess.TBL_File.DBUpdate(FileType, FileTable(0).fk_Expediente, FileTable(0).fk_Folder, FileTable(0).id_File)

                        ' Insertar el estado
                        dbmCore.SchemaProcess.TBL_File_Estado.DBInsert(FileTable(0).fk_Expediente, FileTable(0).fk_Folder, FileTable(0).id_File, DesktopConfig.Modulo.Archiving, DBCore.EstadoEnum.Por_Custodiar, Program.Sesion.Usuario.id, SlygNullable.SysDate)
                    Else
                        ' Actualizar los folios
                        Dim FileType As New DBCore.SchemaProcess.TBL_FileType()
                        FileType.Folios_File = FileTable(0).Folios_File + f.Folios
                        dbmCore.SchemaProcess.TBL_File.DBUpdate(FileType, FileTable(0).fk_Expediente, FileTable(0).fk_Folder, FileTable(0).id_File)

                        ' Actualizar el estado del File para dejarlo en entrada de boveda
                        Dim FileEstadoType As New TBL_File_EstadoType()
                        FileEstadoType.fk_Estado = DBCore.EstadoEnum.Por_Custodiar
                        dbmCore.SchemaProcess.TBL_File_Estado.DBUpdate(FileEstadoType, FileTable(0).fk_Expediente, FileTable(0).fk_Folder, FileTable(0).id_File, DesktopConfig.Modulo.Archiving)

                        dbmCore.Transaction_Commit()
                        DBMrchiving.Transaction_Commit()
                    End If

                    ' Reportar la inserción
                    Dim InsercionType As New TBL_InsercionesType()
                    InsercionType.fk_Expediente = FileTable(0).fk_Expediente
                    InsercionType.fk_Folder = FileTable(0).fk_Folder
                    InsercionType.fk_File = FileTable(0).id_File
                    InsercionType.id_Insercion = DBMrchiving.SchemaRisk.TBL_Inserciones.DBNextId_for_id_Insercion(FileTable(0).fk_Expediente, FileTable(0).fk_Folder, FileTable(0).id_File)
                    InsercionType.Folios = f.Folios
                    InsercionType.Fecha_Insercion = SlygNullable.SysDate
                    InsercionType.fk_Usuario_Log = Program.Sesion.Usuario.id
                    DBMrchiving.SchemaRisk.TBL_Inserciones.DBInsert(InsercionType)

                    CTAFileDataTable = dbmCore.SchemaProcess.CTA_File.DBFindByfk_Expedientefk_Folderid_File(Me.Folder.fk_Expediente, Me.Folder.id_Folder, Nothing)
                    CTAFileDataTableBindingSource.DataSource = CTAFileDataTable

                    MessageBox.Show("Se insertó con éxito " & f.Folios & " nuevo(s) folio(s) al documento: " & f.Tipologia & " con código de barras: " & FileTable(0).CBarras_File, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)

                    CTAFileDataTable = dbmCore.SchemaProcess.CTA_File.DBFindByfk_Expedientefk_Folderid_File(Me.Folder.fk_Expediente, Me.Folder.id_Folder, Nothing)
                    CTAFileDataTableBindingSource.DataSource = CTAFileDataTable

                    dbmCore.Transaction_Commit()
                    DBMrchiving.Transaction_Commit()

                    If (Imprimir) Then
                        'Imprimir = False
                        Dim Formimpresion As New FormCBarrasFolderFile
                        Formimpresion.CBarras = FileTable(0).CBarras_File
                        Formimpresion.ShowDialog()
                    End If

                Catch ex As Exception
                    dbmCore.Transaction_Rollback()
                    DBMrchiving.Transaction_Rollback()

                    DesktopMessageBoxControl.DesktopMessageShow("Insertar", ex)
                Finally
                    dbmCore.Connection_Close()
                    DBMrchiving.Connection_Close()
                End Try
            End If
        End Sub

#End Region

    End Class

End Namespace