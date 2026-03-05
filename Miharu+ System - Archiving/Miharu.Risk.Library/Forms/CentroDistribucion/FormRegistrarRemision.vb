Imports System.Text
Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Controls
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Config
Imports Miharu.Risk.Library.Forms.Reportes.CentroDistribucion
Imports System.Linq
Imports Slyg.Tools
Imports DMB = Miharu.Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl
Imports DBArchiving
Imports DBCore

Namespace Forms.CentroDistribucion

    Public Class FormRegistrarRemision
        Inherits FormBase


#Region "Declaraciones"

        Private _Remision As Long
        Private _LineaProceso As Integer
        Private _Resultado As New DataSet
        Private _ModoRegistroORecepcionRemision As New Boolean
        Private _Caja As Integer
        Private _CerrarManual As Boolean = False

#End Region

#Region "Propiedades"

        Public Property Remision As Long
            Get
                Return _Remision
            End Get
            Set(value As Long)
                _Remision = value
            End Set
        End Property

        Public Property LineaProceso As Integer
            Get
                Return _LineaProceso
            End Get
            Set(value As Integer)
                _LineaProceso = value
            End Set
        End Property

        Public Property ModoRegistroORecepcionRemision As Boolean
            Get
                Return _ModoRegistroORecepcionRemision
            End Get
            Set(value As Boolean)
                _ModoRegistroORecepcionRemision = value
            End Set
        End Property

        Public Property Caja As Integer
            Get
                Return _Caja
            End Get
            Set(value As Integer)
                _Caja = value
            End Set
        End Property
#End Region

#Region "Constructores"

        Public Sub New()
            InitializeComponent()
        End Sub

#End Region

#Region "Eventos"

        Private Sub FormRegistrarRemision_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            If _ModoRegistroORecepcionRemision = True Then
                btnSacarCarpeta.Visible = True
                btnCerrar.Visible = False
            Else
                btnSacarCarpeta.Visible = False
                btnCerrar.Visible = True
                LineaProceso = CInt(Program.Sesion.Parameter("_idLineaProceso"))
            End If
            AlimentarContadoresDocumentos(_ModoRegistroORecepcionRemision)
            gridRemision.AutoGenerateColumns = False
        End Sub

        Private Sub txtCBarras1_Leave(sender As System.Object, e As System.EventArgs) Handles txtCBarras1.Leave

            'If ValidarTextBox(txtCBarras1.Text) Then
            '    ConsultarDocumentosRemision(_ModoRegistroORecepcionRemision)
            'Else
            '    txtCBarras1.Text = ""
            'End If

        End Sub

        Private Sub btnSacarCarpeta_Click(sender As System.Object, e As System.EventArgs) Handles btnSacarCarpeta.Click

            Dim conteodocs = From reg As DataGridViewRow In gridRemision.Rows.Cast(Of DataGridViewRow)()
                                            Where (reg.Cells("EnRemision").Value.ToString.Contains("1"))
                                            Select reg.Index
            If conteodocs.Count > 0 Then
                If DMB.DesktopMessageShow("¿Desea retirar todos los documentos de la remisión?", "Remision", DMB.IconEnum.WarningIcon, False) = DialogResult.OK Then
                    If RetirarCarpetaDeBD() = True Then
                        DMB.DesktopMessageShow("El total de documentos fue retirado con exito", "Retiro Exitoso", DMB.IconEnum.SuccessfullIcon, False)
                    End If
                    ConsultarDocumentosRemision(_ModoRegistroORecepcionRemision)
                    AlimentarContadoresDocumentos(_ModoRegistroORecepcionRemision)
                End If
            Else

            End If
        End Sub

        Private Sub btnCerrar_Click(sender As System.Object, e As System.EventArgs) Handles btnCerrar.Click
            AlimentarContadoresDocumentos(_ModoRegistroORecepcionRemision)
            Me.Close()
        End Sub

        Public Sub FormRegistrarRemision_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
            If Cierre() = False Then
                e.Cancel = True
            End If
        End Sub

        Private Sub txtCBarras1_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtCBarras1.KeyPress
            If e.KeyChar = ChrW(Keys.Enter) Then
                If ValidarTextBox(txtCBarras1.Text) Then
                    ConsultarDocumentosRemision(_ModoRegistroORecepcionRemision)
                Else
                    txtCBarras1.Text = ""
                End If
                SendKeys.Send("{TAB}")
            End If
        End Sub

        Private Sub txtCBarras2_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtCBarras2.KeyPress
            If e.KeyChar = ChrW(Keys.Enter) Then
                If ValidarTextBox(txtCBarras2.Text) Then
                    BuscarCBarrasEnLista()
                    If ModoRegistroORecepcionRemision = True Then
                        If InsertarDocs() = True Then
                            AlimentarContadoresDocumentos(_ModoRegistroORecepcionRemision)
                            LimpiarFormularios()
                        End If
                    Else
                        If ActualizarDocs() = True Then
                            AlimentarContadoresDocumentos(_ModoRegistroORecepcionRemision)
                            LimpiarFormularios()
                        End If
                    End If
                Else
                    txtCBarras2.Text = ""
                End If
            End If
        End Sub

#End Region

#Region "Metodos"

        Private Sub AlimentarContadoresDocumentos(ByVal Tipo As Boolean)
            Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
            Dim Contadores As DataTable
            Try
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)
                Contadores = dbmCore.SchemaProcess.PA_Remision_Conteo_Docs.DBExecute(_Remision, Tipo)
                lblNumeroCarpetas.Text = Contadores.Rows(0).Item("NumeroCarpetas").ToString()
                lblNumeroDocs.Text = Contadores.Rows(0).Item("NumeroDocumentos").ToString()
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CBarrasDocumento", ex)
            Finally
                If dbmCore IsNot Nothing Then dbmCore.Connection_Close()
            End Try
        End Sub

        Private Function ValidarTextBox(Texto As String) As Boolean
            If Texto <> "" And IsNumeric(Texto) And Texto.Length = 12 Then
                Return True
            Else
                DMB.DesktopMessageShow("Valor digitado invalido", "Dato Incorrecto", DMB.IconEnum.WarningIcon, False)
                Return False
            End If
        End Function

        Private Sub ConsultarDocumentosRemision(ByVal Modo As Boolean)
            Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
            Dim CBarras As String = txtCBarras1.Text
            Try
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)
                _Resultado.Tables.Clear()

                If CBarras.Substring(0, 3) = "000" Then
                    _Resultado.Tables.Add(dbmCore.SchemaProcess.PA_Documentos_Remision.DBExecute(CBarras, "0", _LineaProceso, _Remision, ModoRegistroORecepcionRemision))
                Else
                    _Resultado.Tables.Add(dbmCore.SchemaProcess.PA_Documentos_Remision.DBExecute("0", CBarras, _LineaProceso, _Remision, ModoRegistroORecepcionRemision))
                    If _Resultado.Tables(0).Rows.Count > 0 Then
                        txtCBarras1.Text = _Resultado.Tables(0).Rows(0).ItemArray(5).ToString
                    End If

                End If

                gridRemision.DataSource = _Resultado.Tables(0)
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CBarrasDocumento", ex)
            Finally
                If dbmCore IsNot Nothing Then dbmCore.Connection_Close()
            End Try
        End Sub

        Private Sub BuscarCBarrasEnLista()

            Dim existe As Boolean = False
            Dim valor As String = txtCBarras2.Text

            Dim busqueda = From reg As DataGridViewRow In gridRemision.Rows.Cast(Of DataGridViewRow)()
                           Where reg.Cells("CBarras_File").Value.ToString.Equals(valor)
                           Select reg.Index

            If busqueda.Count > 0 Then

                If Not CBool(gridRemision.Rows(busqueda.Single).Cells("EnRemision").Value) Then
                    gridRemision.Rows(busqueda.Single).Cells("EnRemision").Value = True
                    txtCBarras2.Clear()
                    existe = True
                Else
                    DesktopMessageBoxControl.DesktopMessageShow("El documento ya fue ingresado", "Descuelgue Solicitudes", DesktopMessageBoxControl.IconEnum.WarningIcon)
                    txtCBarras2.Clear()
                End If
            End If

            If Not existe Then
                DesktopMessageBoxControl.DesktopMessageShow("Codigo de Barras no encontrado.", "Descuelgue Solicitudes", DesktopMessageBoxControl.IconEnum.WarningIcon)
            End If
            txtCBarras2.Focus()
        End Sub

        Private Function InsertarDocs() As Boolean

            Dim Grabado As Boolean = False
            Dim conteodocs = From reg As DataGridViewRow In gridRemision.Rows.Cast(Of DataGridViewRow)()
                                             Where (reg.Cells("EnRemision").Value.ToString.Contains("0"))
                                             Select reg.Index

            If conteodocs.Count = 0 Then

                Dim n_OT As Integer
                Dim nExpediente As Long
                Dim nFolder As Short
                Dim nFile As Short
                Dim nUsuario As Integer
                Dim nEstado As Short
                Dim nModulo As Byte
                Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                Dim dbmArchiving As New DBArchiving.DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)

                Try
                    dbmCore.Connection_Open(Program.Sesion.Usuario.id)
                    dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)


                    For i As Integer = 0 To gridRemision.Rows.Count - 1 Step 1

                        Dim Existe = dbmCore.SchemaProcess.PA_Documentos_Remision.DBExecute("0", gridRemision.Rows(i).Cells("CBarras_File").Value.ToString, _LineaProceso, _Remision, ModoRegistroORecepcionRemision)

                        Dim registro = From reg As DataGridViewRow In gridRemision.Rows.Cast(Of DataGridViewRow)()
                                             Where (reg.Cells("CBarras_File").Value.ToString.Contains(gridRemision.Rows(i).Cells("CBarras_File").Value.ToString))
                                             Select reg.Cells("EnRemision").Value

                        If CBool(registro(0)) Then
                            Dim ActualizaFolder As Boolean = True
                            n_OT = CType(gridRemision.Rows(i).Cells("ColumnOT").Value, Integer)
                            nExpediente = CType(gridRemision.Rows(i).Cells("ColumnExpediente").Value, Long)
                            nFolder = CType(gridRemision.Rows(i).Cells("ColumnFolder").Value, Short)
                            nFile = CType(gridRemision.Rows(i).Cells("ColumnFile").Value, Short)
                            nUsuario = Program.Sesion.Usuario.id
                            nEstado = DBCore.EstadoEnum.Asignado_a_Remision
                            nModulo = DesktopConfig.Modulo.Archiving


                            Dim tRemisionItem As New DBCore.SchemaProcess.TBL_Remision_ItemType
                            tRemisionItem.fk_Remision_Caja = _Remision
                            tRemisionItem.fk_Expediente = nExpediente
                            tRemisionItem.fk_Folder = nFolder
                            tRemisionItem.fk_File = nFile
                            tRemisionItem.fk_Usuario_Remite = nUsuario
                            tRemisionItem.Fecha_Remite = SlygNullable.SysDate
                            dbmCore.SchemaProcess.TBL_Remision_Item.DBInsert(tRemisionItem)

                            Dim tEstadoFile As New DBCore.SchemaProcess.TBL_File_EstadoType
                            tEstadoFile.fk_Estado = nEstado
                            tEstadoFile.fk_Usuario = Program.Sesion.Usuario.id
                            tEstadoFile.Fecha_Log = SlygNullable.SysDate
                            dbmCore.SchemaProcess.TBL_File_Estado.DBUpdate(tEstadoFile, nExpediente, nFolder, nFile, nModulo)

                            Dim tFileArchiving As New DBArchiving.SchemaRisk.TBL_FileType
                            tFileArchiving.fk_Estado = nEstado
                            dbmArchiving.SchemaRisk.TBL_File.DBUpdate(tFileArchiving, n_OT, nFolder, nFile, nExpediente)

                            If CType(gridRemision.Rows(i).Cells("ColumnRegistroTipo").Value, Short) = 1 Then
                                If ActualizaFolder Then

                                    Dim tFolderArchiving As New DBArchiving.SchemaRisk.TBL_FolderType
                                    tFolderArchiving.fk_Estado = nEstado
                                    dbmArchiving.SchemaRisk.TBL_Folder.DBUpdate(tFolderArchiving, nExpediente, nFolder, n_OT)
                                    ActualizaFolder = False

                                End If
                            ElseIf CType(gridRemision.Rows(i).Cells("ColumnRegistroTipo").Value, Short) = 3 Then

                                If ActualizaFolder Then

                                    Dim tEstadoFolder As New DBCore.SchemaProcess.TBL_Folder_estadoType
                                    tEstadoFolder.fk_Estado = nEstado
                                    tEstadoFolder.fk_Usuario = Program.Sesion.Usuario.id
                                    tEstadoFolder.Fecha_Log = SlygNullable.SysDate
                                    dbmCore.SchemaProcess.TBL_Folder_estado.DBUpdate(tEstadoFolder, nExpediente, nFolder, nModulo)
                                    ActualizaFolder = False

                                    Dim tFolderArchiving As New DBArchiving.SchemaRisk.TBL_FolderType
                                    tFolderArchiving.fk_Estado = nEstado
                                    dbmArchiving.SchemaRisk.TBL_Folder.DBUpdate(tFolderArchiving, nExpediente, nFolder, n_OT)

                                End If
                            End If

                        End If

                    Next i

                    DMB.DesktopMessageShow("El total de documentos fue ingresados con exito", "Grabación Exitosa", DMB.IconEnum.SuccessfullIcon, False)
                    Grabado = True
                    txtCBarras1.Focus()
                Catch ex As Exception
                    DesktopMessageBoxControl.DesktopMessageShow("CBarrasDocumento", ex)
                Finally
                    If dbmCore IsNot Nothing Then dbmCore.Connection_Close()
                    If dbmArchiving IsNot Nothing Then dbmArchiving.Connection_Close()
                End Try
            End If
            Return Grabado
        End Function

        Private Function ActualizarDocs() As Boolean
            Dim Grabado As Boolean = False
            Dim conteodocs = From reg As DataGridViewRow In gridRemision.Rows.Cast(Of DataGridViewRow)()
                                             Where (reg.Cells("EnRemision").Value.ToString.Contains("0"))
                                             Select reg.Index

            If conteodocs.Count = 0 Or _CerrarManual Then

                Dim n_OT As Integer
                Dim nExpediente As Long
                Dim nFolder As Short
                Dim nFile As Short
                Dim nUsuario As Integer
                Dim nEstado As Short
                Dim nModulo As Byte
                Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                Dim dbmArchiving As New DBArchiving.DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)

                Try
                    dbmCore.Connection_Open(Program.Sesion.Usuario.id)
                    dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)
                    Dim ActualizaFolder As Boolean = True

                    For i As Integer = 0 To gridRemision.Rows.Count - 1 Step 1

                        If CType(gridRemision.Rows(i).Cells("EnRemision").Value, Short) = 1 Then

                            n_OT = CType(gridRemision.Rows(i).Cells("ColumnOT").Value, Integer)
                            nExpediente = CType(gridRemision.Rows(i).Cells("ColumnExpediente").Value, Long)
                            nFolder = CType(gridRemision.Rows(i).Cells("ColumnFolder").Value, Short)
                            nFile = CType(gridRemision.Rows(i).Cells("ColumnFile").Value, Short)
                            nUsuario = Program.Sesion.Usuario.id
                            nEstado = DBCore.EstadoEnum.Empaque
                            nModulo = DesktopConfig.Modulo.Archiving

                            Dim tRemisionItem As New DBCore.SchemaProcess.TBL_Remision_ItemType

                            tRemisionItem.fk_Usuario_Recibe = nUsuario
                            tRemisionItem.Fecha_Recibe = SlygNullable.SysDate
                            dbmCore.SchemaProcess.TBL_Remision_Item.DBUpdate(tRemisionItem, _Remision, nExpediente, nFolder, nFile)

                            If CType(gridRemision.Rows(i).Cells("ColumnRegistroTipo").Value, Short) = 1 Then
                                If ActualizaFolder Then

                                    Dim tFolderArchiving As New DBArchiving.SchemaRisk.TBL_FolderType
                                    tFolderArchiving.fk_Estado = DBCore.EstadoEnum.Custodia
                                    dbmArchiving.SchemaRisk.TBL_Folder.DBUpdate(tFolderArchiving, nExpediente, nFolder, n_OT)

                                    Dim tFileArchiving As New DBArchiving.SchemaRisk.TBL_FileType
                                    tFileArchiving.fk_Estado = DBCore.EstadoEnum.Custodia
                                    dbmArchiving.SchemaRisk.TBL_File.DBUpdate(tFileArchiving, n_OT, nFolder, nFile, nExpediente)

                                    ActualizaFolder = False

                                End If

                                Dim tEstadoFile As New DBCore.SchemaProcess.TBL_File_EstadoType
                                tEstadoFile.fk_Estado = DBCore.EstadoEnum.Custodia
                                tEstadoFile.fk_Usuario = Program.Sesion.Usuario.id
                                tEstadoFile.Fecha_Log = SlygNullable.SysDate
                                dbmCore.SchemaProcess.TBL_File_Estado.DBUpdate(tEstadoFile, nExpediente, nFolder, nFile, nModulo)

                            ElseIf CType(gridRemision.Rows(i).Cells("ColumnRegistroTipo").Value, Short) = 3 Then

                                If ActualizaFolder Then
                                    Dim tEstadoFolder As New DBCore.SchemaProcess.TBL_Folder_estadoType
                                    tEstadoFolder.fk_Estado = nEstado
                                    tEstadoFolder.fk_Usuario = Program.Sesion.Usuario.id
                                    tEstadoFolder.Fecha_Log = SlygNullable.SysDate
                                    dbmCore.SchemaProcess.TBL_Folder_estado.DBUpdate(tEstadoFolder, nExpediente, nFolder, nModulo)

                                    Dim tFolderArchiving As New DBArchiving.SchemaRisk.TBL_FolderType
                                    tFolderArchiving.fk_Estado = nEstado
                                    dbmArchiving.SchemaRisk.TBL_Folder.DBUpdate(tFolderArchiving, nExpediente, nFolder, n_OT)

                                    ActualizaFolder = False

                                End If

                                Dim tFileArchiving As New DBArchiving.SchemaRisk.TBL_FileType
                                tFileArchiving.fk_Estado = nEstado
                                dbmArchiving.SchemaRisk.TBL_File.DBUpdate(tFileArchiving, n_OT, nFolder, nFile, nExpediente)

                                Dim tEstadoFile As New DBCore.SchemaProcess.TBL_File_EstadoType
                                tEstadoFile.fk_Estado = nEstado
                                tEstadoFile.fk_Usuario = Program.Sesion.Usuario.id
                                tEstadoFile.Fecha_Log = SlygNullable.SysDate
                                dbmCore.SchemaProcess.TBL_File_Estado.DBUpdate(tEstadoFile, nExpediente, nFolder, nFile, nModulo)

                            End If
                        End If
                    Next i

                    Dim Resultados As DataTable = dbmArchiving.Schemadbo.PA_Empaca_Folder_Caja_Final.DBExecute(txtCBarras1.Text, Program.RiskGlobal.ID_CajaCustodia, Program.RiskGlobal.Folder_Tipo, Program.Sesion.Usuario.id, Program.RiskGlobal.Usa_Empaque_Adicion)

                    DMB.DesktopMessageShow("Los documentos fueron recibidos con exito", "Grabación Exitosa", DMB.IconEnum.SuccessfullIcon, False)
                    Grabado = True
                    txtCBarras1.Focus()
                Catch ex As Exception
                    DesktopMessageBoxControl.DesktopMessageShow("CBarrasDocumento", ex)
                Finally
                    If dbmCore IsNot Nothing Then dbmCore.Connection_Close()
                    If dbmArchiving IsNot Nothing Then dbmArchiving.Connection_Close()
                End Try
            End If
            Return Grabado
        End Function

        Private Function RetirarCarpetaDeBD() As Boolean
            Dim conteodocs = From reg As DataGridViewRow In gridRemision.Rows.Cast(Of DataGridViewRow)()
                                 Where (reg.Cells("EnRemision").Value.ToString.Contains("0"))
                                 Select reg.Index
            If conteodocs.Count = 0 Then

                Dim n_OT As Integer
                Dim nExpediente As Long
                Dim nFolder As Short
                Dim nFile As Short
                Dim nEstado As Short
                Dim nModulo As Byte
                Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                Dim dbmArchiving As New DBArchiving.DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)

                Try
                    dbmCore.Connection_Open(Program.Sesion.Usuario.id)
                    dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

                    For i As Integer = 0 To gridRemision.Rows.Count - 1 Step 1

                        n_OT = CType(gridRemision.Rows(i).Cells("ColumnOT").Value, Integer)
                        nExpediente = CType(gridRemision.Rows(i).Cells("ColumnExpediente").Value, Long)
                        nFolder = CType(gridRemision.Rows(i).Cells("ColumnFolder").Value, Short)
                        nFile = CType(gridRemision.Rows(i).Cells("ColumnFile").Value, Short)
                        nEstado = DBCore.EstadoEnum.Centro_Distribucion
                        nModulo = DesktopConfig.Modulo.Archiving

                        Dim tRemisionItem As New DBCore.SchemaProcess.TBL_Remision_ItemType
                        dbmCore.SchemaProcess.TBL_Remision_Item.DBDelete(_Remision, nExpediente, nFolder, nFile)

                        Dim tEstadoFile As New DBCore.SchemaProcess.TBL_File_EstadoType
                        tEstadoFile.fk_Estado = nEstado
                        dbmCore.SchemaProcess.TBL_File_Estado.DBUpdate(tEstadoFile, nExpediente, nFolder, nFile, nModulo)

                        Dim tFileArchiving As New DBArchiving.SchemaRisk.TBL_FileType
                        tFileArchiving.fk_Estado = nEstado
                        dbmArchiving.SchemaRisk.TBL_File.DBUpdate(tFileArchiving, n_OT, nFolder, nFile, nExpediente)

                        If CType(gridRemision.Rows(i).Cells("ColumnRegistroTipo").Value, Short) = 1 Then

                            Dim tEstadoFolder As New DBCore.SchemaProcess.TBL_Folder_estadoType
                            tEstadoFolder.fk_Estado = nEstado
                            dbmCore.SchemaProcess.TBL_Folder_estado.DBUpdate(tEstadoFolder, nExpediente, nFolder, nModulo)

                        ElseIf CType(gridRemision.Rows(i).Cells("ColumnRegistroTipo").Value, Short) = 3 Then

                            Dim tEstadoFolder As New DBCore.SchemaProcess.TBL_Folder_estadoType
                            tEstadoFolder.fk_Estado = nEstado
                            dbmCore.SchemaProcess.TBL_Folder_estado.DBUpdate(tEstadoFolder, nExpediente, nFolder, nModulo)

                            Dim tFolderArchiving As New DBArchiving.SchemaRisk.TBL_FolderType
                            tFolderArchiving.fk_Estado = nEstado
                            dbmArchiving.SchemaRisk.TBL_Folder.DBUpdate(tFolderArchiving, nExpediente, nFolder, n_OT)
                        End If

                    Next i

                Catch ex As Exception
                    DesktopMessageBoxControl.DesktopMessageShow("CBarrasDocumento", ex)
                Finally
                    If dbmCore IsNot Nothing Then dbmCore.Connection_Close()
                    If dbmArchiving IsNot Nothing Then dbmArchiving.Connection_Close()
                End Try
            End If

            Return True
        End Function

        Private Function Cierre() As Boolean

            Dim conteodocs = From reg As DataGridViewRow In gridRemision.Rows.Cast(Of DataGridViewRow)()
                                             Where (reg.Cells("EnRemision").Value.ToString.Contains("0"))
                                             Select reg.Index

            If conteodocs.Count > 0 Then
                If DMB.DesktopMessageShow("No fueron recibidos todos los documentos, al cerrar la remisión seran reasignados a la sede de procesamiento \n ¿ Desea continuar ?", "Documentos Faltantes", DMB.IconEnum.WarningIcon, False) = DialogResult.OK Then
                    _CerrarManual = True
                    ActualizarDocs()
                    CerrarLinea()
                    Return True
                Else
                    Return False
                End If
            Else
                CerrarLinea()
                Return True
            End If

        End Function

        Private Sub LimpiarFormularios()
            gridRemision.DataSource = Nothing
            txtCBarras1.Text = ""
            txtCBarras2.Text = ""
        End Sub

        Private Sub CerrarLinea()
            Dim Estado As Integer

            'If _ModoRegistroORecepcionRemision Then
            'Else
            '    Estado = DBCore.EstadoEnum.Empaque
            'End If
            If ModoRegistroORecepcionRemision Then
                Dim dbmArchiving As New DBArchiving.DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)

                Estado = DBCore.EstadoEnum.Centro_Distribucion

                Try
                    dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

                    Dim dtFilesLineaProceso = dbmArchiving.Schemadbo.CTA_File_CentroDistribucion.DBFindByfk_Linea_Proceso(_LineaProceso)
                    Dim viewFilesLineaProceso As DataView = dtFilesLineaProceso.DefaultView



                    viewFilesLineaProceso.RowFilter = "fk_estado=" & Estado
                    viewFilesLineaProceso.Sort = "CBarras_Folder ASC, CBarras_File ASC"

                    If viewFilesLineaProceso.Count > 0 Then
                        'Crea mensaje con la relación de documentos pendientes.
                        Dim mensaje As New StringBuilder()
                        mensaje.AppendLine("CARPETA - DOCUMENTO")
                        For Each row As DataRow In viewFilesLineaProceso.ToTable(True).Rows
                            mensaje.AppendLine(row("CBarras_Folder").ToString() & " - " & row("CBarras_File").ToString())
                        Next

                        DesktopMessageBoxControl.DesktopMessageShow("No se puede cerrar la línea de proceso, ya que aún existen items pendientes de procesar ó carpetas sin cerrar." & vbNewLine & vbNewLine & mensaje.ToString(), "Cierre de línea", DesktopMessageBoxControl.IconEnum.WarningIcon, True)

                    Else
                        If DesktopMessageBoxControl.DesktopMessageShow("¿Desea cerrar la línea de proceso: [" & _LineaProceso.ToString() & "]?", "Cerrar línea de proceso", DesktopMessageBoxControl.IconEnum.WarningIcon) = DialogResult.OK Then
                            Estado = DBCore.EstadoEnum.Asignado_a_Remision
                            Dim typeLineaProceso As New SchemaRisk.TBL_Linea_ProcesoType
                            typeLineaProceso.fk_Estado = CType(Estado, SlygNullable(Of Short))
                            typeLineaProceso.Fecha_Log = SlygNullable.SysDate

                            dbmArchiving.Transaction_Begin()
                            dbmArchiving.SchemaRisk.TBL_Linea_Proceso.DBUpdate(typeLineaProceso, _LineaProceso)
                            dbmArchiving.Transaction_Commit()
                        End If
                    End If


                Catch ex As Exception
                    dbmArchiving.Transaction_Rollback()
                    DesktopMessageBoxControl.DesktopMessageShow("CerrarLineaProceso", ex)
                Finally
                    dbmArchiving.Connection_Close()
                End Try
            End If
        End Sub

#End Region

    End Class
End Namespace
