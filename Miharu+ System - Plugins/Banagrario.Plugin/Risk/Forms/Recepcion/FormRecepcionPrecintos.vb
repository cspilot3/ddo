Imports System.Windows.Forms
Imports DBAgrario
Imports DBAgrario.SchemaArchiving
Imports DBAgrario.SchemaProcess
Imports DBArchiving
Imports DBArchiving.SchemaRisk
Imports DBSecurity
Imports DBSecurity.SchemaConfig
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Config
Imports SLYG.Tools

Namespace Risk.Forms.Recepcion

    Public Class FormRecepcionPrecintos

#Region " Declaraciones "

        Private _plugin As BanagrarioRiskPlugin

        Private _otsData As CTA_Risk_OTDataTable

        Private _valorPrecintoActual As String = ""

        Private _otComboBoxIgnoreSelectedIndexChange As Boolean = False
        Private _oficinaComboBoxIgnoreSelectedIndexChange As Boolean = False
        Private _precintosDataGridViewEditing As Boolean = False

#End Region

#Region " Constructores "

        Public Sub New(ByVal nBanagrarioDesktopPlugin As BanagrarioRiskPlugin)
            InitializeComponent()

            _plugin = nBanagrarioDesktopPlugin
        End Sub

#End Region

#Region " Eventos "

        Private Sub FormRecepcionPrecintos_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            'CargarOtsFecha()
            LlenarCombos()
        End Sub

        'Private Sub FinalizarRecepcionarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs)
        '    FinalizarRecepcionar()
        'End Sub

        Private Sub CerrarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CerrarButton.Click
            Me.Close()
        End Sub

        Private Sub OTComboBox_KeyDown(ByVal sender As System.Object, ByVal e As KeyEventArgs) Handles OTComboBox.KeyDown
            If e.KeyCode = Keys.Enter Then
                e.Handled = True
                SendKeys.Send("{TAB}")
            End If
        End Sub

        Private Sub OTComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles OTComboBox.SelectedIndexChanged
            If (_otComboBoxIgnoreSelectedIndexChange = False) Then
                OficinaComboBox.SelectedIndex = -1
                MostrarPrecintosOt()
            End If
        End Sub

        Private Sub OficinaComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles OficinaComboBox.SelectedIndexChanged
            If _oficinaComboBoxIgnoreSelectedIndexChange = False Then
                If OficinaComboBox.SelectedIndex <> -1 Then
                    MostrarPrecintosOf()
                Else
                    MostrarPrecintosOt()
                End If

            End If
        End Sub

        Private Sub PrecintosDataGridView_CellBeginEdit(ByVal sender As System.Object, ByVal e As DataGridViewCellCancelEventArgs) Handles PrecintosDataGridView.CellBeginEdit
            Dim val = PrecintosDataGridView(e.ColumnIndex, e.RowIndex).Value
            If (val Is DBNull.Value) Then
                _valorPrecintoActual = ""

            Else
                If (Not ValidarEdicionPrecinto()) Then
                    e.Cancel = True
                End If
                _valorPrecintoActual = val.ToString()
            End If

            _precintosDataGridViewEditing = Not e.Cancel
        End Sub

        Private Sub PrecintosDataGridView_CellEndEdit(ByVal sender As System.Object, ByVal e As DataGridViewCellEventArgs) Handles PrecintosDataGridView.CellEndEdit
            Dim nuevoValor = PrecintosDataGridView(e.ColumnIndex, e.RowIndex).Value
            If (nuevoValor Is DBNull.Value) Then
                Return
            End If

            If (CStr(nuevoValor) <> _valorPrecintoActual) Then
                GuardarPrecinto(_valorPrecintoActual, CStr(nuevoValor), e.RowIndex)
            End If

            _precintosDataGridViewEditing = False
        End Sub

        Private Sub FormRecepcionPrecintos_KeyDown(ByVal sender As System.Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
            If (e.KeyCode = Keys.Escape) Then
                DialogResult = DialogResult.Cancel
                Close()
            End If
        End Sub

        Private Sub PrecintosDataGridView_CellValidating(ByVal sender As System.Object, ByVal e As DataGridViewCellValidatingEventArgs) Handles PrecintosDataGridView.CellValidating
            If (_precintosDataGridViewEditing) Then
                Dim nuevoValor = e.FormattedValue
                If (nuevoValor Is DBNull.Value) Then
                    Return
                End If

                If (CStr(nuevoValor) <> _valorPrecintoActual) Then
                    e.Cancel = Not ValidarPrecinto(CStr(nuevoValor))
                End If
            End If
        End Sub

        Private Sub EliminarPrecintoButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles EliminarPrecintoButton.Click
            EliminarPrecinto()
        End Sub

        Private Sub ReporteButton_Click(sender As System.Object, e As EventArgs) Handles ReporteButton.Click
            Dim dbmAgrario As DBAgrarioDataBaseManager = Nothing
            Try
                dbmAgrario = New DBAgrarioDataBaseManager(Me._plugin.BancoAgrarioConnectionString)
                'dbmAgrario.DataBase.Identifier_Date_Format = _plugin.Manager.DesktopGlobal.IdentifierDateFormat
                dbmAgrario.Connection_Open(_plugin.Manager.Sesion.Usuario.id)
                Dim precintosData As New DataTable
                Dim x As DataTable
                precintosData.Columns.Add("Ciudad")
                precintosData.Columns.Add("Nombre_Oficina")
                precintosData.Columns.Add("id_Precinto")
                precintosData.Columns.Add("Fecha_Recepcion")
                precintosData.Columns.Add("OT")
                For i As Integer = 0 To PrecintosDataGridView.RowCount - 1
                    If ((Not PrecintosDataGridView.Rows(i).Cells(0).Value Is Nothing) And (Not PrecintosDataGridView.Rows(i).Cells(0).Value Is "") And (Not PrecintosDataGridView.Rows(i).Cells(0).Value Is DBNull.Value)) Then
                        Dim row As DataRow = precintosData.NewRow
                        x = dbmAgrario.SchemaReport.PA_Destape_Buscar.DBExecute(CStr(PrecintosDataGridView.Rows(i).Cells(0).Value), DBNull.Value, DBNull.Value, DBNull.Value)

                        row("Ciudad") = x.Rows(0)("Ciudad").ToString
                        row("Nombre_Oficina") = x.Rows(0)("Nombre_Oficina").ToString
                        row("id_Precinto") = x.Rows(0)("id_Precinto").ToString
                        row("Fecha_Recepcion") = x.Rows(0)("Fecha_Recepcion").ToString
                        row("OT") = CStr(DirectCast(OTComboBox.SelectedItem, DataRowView).Row(CTA_Risk_OTEnum.Fecha_Proceso.ColumnName))
                        precintosData.Rows.Add(row)
                    End If
                Next
                dbmAgrario.Connection_Close()
                Dim fReporte As New ReporteRecepcionPrecintos(_plugin, precintosData)
                fReporte.ShowDialog()

            Catch ex As Exception
                Try : dbmAgrario.Connection_Close() : Catch : End Try
                DesktopMessageBoxControl.DesktopMessageShow("ReportePrecintos", ex)
            End Try
        End Sub

#End Region

#Region " Funciones "

        Public Sub LlenarCombos()
            Dim dmSecurity As DBSecurityDataBaseManager = Nothing

            Try

                dmSecurity = New DBSecurityDataBaseManager(_plugin.Manager.DesktopGlobal.ConnectionStrings.Security)
                'dmSecurity.DataBase.Identifier_Date_Format = _plugin.Manager.DesktopGlobal.IdentifierDateFormat
                dmSecurity.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                Dim sedesData = dmSecurity.SchemaConfig.CTA_Sede.DBFindByfk_Entidadid_Sede(_plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, Nothing, 0, New CTA_SedeEnumList(CTA_SedeEnum.Nombre_Ciudad_Sede, True))
                Utilities.LlenarCombo(SedeComboBox, sedesData, CTA_SedeEnum.id_Sede.ColumnName, CTA_SedeEnum.Nombre_Ciudad_Sede.ColumnName)

                For Each item In SedeComboBox.Items
                    Dim row = DirectCast(item, DataRowView).Row
                    If (row(CTA_SedeEnum.id_Sede.ColumnName).ToString() = _plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Sede.ToString()) Then
                        SedeComboBox.SelectedItem = item
                        Exit For
                    End If
                Next

                SedeComboBox.Enabled = False
                'TODO: Agregar control de cordinador Bogota

                CargarOtsFecha()

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("GenerarOT", ex)
            Finally
                Try : dmSecurity.Connection_Close() : Catch : End Try
            End Try
        End Sub

        Private Sub CargarOtsFecha()
            If (SedeComboBox.SelectedIndex = -1) Then
                Return
            End If

            Dim dbmAgrario As DBAgrarioDataBaseManager = Nothing

            Try
                OTComboBox.Items.Clear()
                OTComboBox.SelectedItem = Nothing

                dbmAgrario = New DBAgrarioDataBaseManager(Me._plugin.BancoAgrarioConnectionString)
                'dbmAgrario.DataBase.Identifier_Date_Format = _plugin.Manager.DesktopGlobal.IdentifierDateFormat
                dbmAgrario.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                Dim entidadProcesamientoId = _plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad
                Dim sedeProcesamientoId = CShort(SedeComboBox.SelectedValue)

                _otsData = dbmAgrario.SchemaProcess.PA_OT_Get.DBExecute(entidadProcesamientoId, sedeProcesamientoId, Program.OTDiasAtras)
                Dim otsList As New ListItemDataTable(CTA_Risk_OTEnum.Fecha_Proceso.ColumnName, CTA_Risk_OTEnum.id_OT.ColumnName)
                For Each row In _otsData
                    otsList.Add(row.Fecha_Proceso.ToString("yyyy-MM-dd") + " OT(" + row.id_OT.ToString() + "-" + GetNombreEstado(row.fk_Estado) + ")", CStr(row.id_OT))
                Next
                _otComboBoxIgnoreSelectedIndexChange = True
                Utilities.LlenarCombo(OTComboBox, otsList, CTA_Risk_OTEnum.id_OT.ColumnName, CTA_Risk_OTEnum.Fecha_Proceso.ColumnName)

                For Each item In OTComboBox.Items
                    Dim fechaOt As String = CStr(DirectCast(item, DataRowView).Row(CTA_Risk_OTEnum.Fecha_Proceso.ColumnName))
                    If (fechaOt.IndexOf(Now.ToString("yyyy-MM-dd")) > -1) Then
                        OTComboBox.SelectedItem = item
                    End If
                Next
                _otComboBoxIgnoreSelectedIndexChange = False

                CargarOficinas()

            Catch ex As Exception
                MostrarMensajeValidacion("No fue posible cargar las OTS, " + ex.Message)
            Finally
                Try : dbmAgrario.Connection_Close() : Catch : End Try
            End Try
        End Sub

        Private Sub CargarOficinas()
            Dim dbmAgrario As DBAgrarioDataBaseManager = Nothing

            Try

                dbmAgrario = New DBAgrarioDataBaseManager(Me._plugin.BancoAgrarioConnectionString)
                'dbmAgrario.DataBase.Identifier_Date_Format = _plugin.Manager.DesktopGlobal.IdentifierDateFormat
                dbmAgrario.Connection_Open(_plugin.Manager.Sesion.Usuario.id)
                Dim oficinaDataTable As New DBAgrario.SchemaConfig.TBL_OficinaDataTable

                'Dim EntidadProcesamiento As Short = _plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad

                Dim OficinaData = dbmAgrario.SchemaProcess.CTA_Oficina_Concatenacion.DBFindByid_COB(Convert.ToInt16(SedeComboBox.SelectedValue), 0, New CTA_Oficina_ConcatenacionEnumList(CTA_Oficina_ConcatenacionEnum.id_Oficina, True))
                Dim OfiView = OficinaData.DefaultView

                Utilities.LlenarCombo(OficinaComboBox, OfiView.ToTable(), CTA_Oficina_ConcatenacionEnum.id_Oficina.ColumnName, CTA_Oficina_ConcatenacionEnum.Nombre_Oficina.ColumnName)
                OficinaComboBox.SelectedIndex = -1

                MostrarPrecintosOT()

            Catch ex As Exception
                MostrarMensajeValidacion("No fue posible cargar las oficinas, " + ex.Message)
            Finally
                Try : dbmAgrario.Connection_Close() : Catch : End Try
            End Try
        End Sub

        Private Sub MostrarPrecintosOt()
            If (OTComboBox.SelectedItem Is Nothing) Then
                PrecintosDataGridView.Enabled = False
                Return
            End If

            Dim otId = DirectCast(OTComboBox.SelectedItem, DataRowView).Row(CTA_Risk_OTEnum.id_OT.ColumnName)
            Dim ot = DirectCast(_otsData.Select(CTA_Risk_OTEnum.id_OT.ColumnName + "=" + otId.ToString()), CTA_Risk_OTRow())(0)

            Dim dmarchiving As DBArchivingDataBaseManager = Nothing
            Try
                dmarchiving = New DBArchivingDataBaseManager(_plugin.Manager.DesktopGlobal.ConnectionStrings.Archiving)
                'dmarchiving.DataBase.Identifier_Date_Format = _plugin.Manager.DesktopGlobal.IdentifierDateFormat
                dmarchiving.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                Dim precintosData = dmarchiving.SchemaRisk.TBL_Precinto.DBFindByfk_OT(ot.id_OT)
                PluginsTables.Precintos.Rows.Clear()
                For Each p In precintosData
                    PluginsTables.Precintos.AddPrecintosRow(p.id_Precinto)
                Next
                PluginsTables.Precintos.AcceptChanges()
                AutoNumberRowsForGridView(PrecintosDataGridView)
                dmarchiving.Connection_Close()

                PrecintosDataGridView.Enabled = (ot.fk_Estado <> DBCore.EstadoEnum.Cerrado)

                If (PluginsTables.Precintos.Count > 0) Then
                    PrecintosDataGridView.ClearSelection()
                    PrecintosDataGridView(0, PluginsTables.Precintos.Count).Selected = True
                End If

            Catch ex As Exception
                Try : dmarchiving.Connection_Close() : Catch : End Try

                DesktopMessageBoxControl.DesktopMessageShow("MostrarPrecintosOT", ex)
            End Try
        End Sub

        Private Sub MostrarPrecintosOf()
            If (OficinaComboBox.SelectedItem Is Nothing) Then
                PrecintosDataGridView.Enabled = False
                Return
            End If

            Dim dbmAgrario As DBAgrarioDataBaseManager = Nothing
            Try
                dbmAgrario = New DBAgrarioDataBaseManager(Me._plugin.BancoAgrarioConnectionString)
                'dbmAgrario.DataBase.Identifier_Date_Format = _plugin.Manager.DesktopGlobal.IdentifierDateFormat
                dbmAgrario.Connection_Open(_plugin.Manager.Sesion.Usuario.id)
                Dim precintosData As DataTable
                precintosData = dbmAgrario.SchemaReport.PA_Destape_Buscar.DBExecute(DBNull.Value, Convert.ToInt32(OficinaComboBox.SelectedValue), Convert.ToInt32(SedeComboBox.SelectedValue), CStr(DirectCast(OTComboBox.SelectedItem, DataRowView).Row(CTA_Risk_OTEnum.Fecha_Proceso.ColumnName)))

                PluginsTables.Precintos.Rows.Clear()
                For Each p As DataRow In precintosData.Rows
                    PluginsTables.Precintos.AddPrecintosRow(p.Item("id_Precinto").ToString)
                Next
                PluginsTables.Precintos.AcceptChanges()
                AutoNumberRowsForGridView(PrecintosDataGridView)
                dbmAgrario.Connection_Close()

                PrecintosDataGridView.Enabled = True

                If (PluginsTables.Precintos.Count > 0) Then
                    PrecintosDataGridView.ClearSelection()
                    PrecintosDataGridView(0, PluginsTables.Precintos.Count).Selected = True
                End If

            Catch ex As Exception
                Try : dbmAgrario.Connection_Close() : Catch : End Try

                DesktopMessageBoxControl.DesktopMessageShow("MostrarPrecintosOF", ex)
            End Try
        End Sub

        Private Function ValidarEdicionPrecinto() As Boolean
            Dim dmarchiving As DBArchivingDataBaseManager = Nothing

            Try
                If (OTComboBox.SelectedItem Is Nothing) Then
                    'PrecintosDataGridView.Rows.Remove(PrecintosDataGridView(nColumnIndex, nRowIndex).OwningRow)
                    Throw New Exception("Primero se debe seleccionar una fecha de proceso")
                End If

                'Dim OTId = DirectCast(OTComboBox.SelectedItem, System.Data.DataRowView).Row(CTA_Risk_OTEnum.id_OT.ColumnName)
                'Dim OT = DirectCast(_OTS_Data.Select(CTA_Risk_OTEnum.id_OT.ColumnName + "=" + OTId.ToString()), CTA_Risk_OTRow())(0)

                dmarchiving = New DBArchivingDataBaseManager(_plugin.Manager.DesktopGlobal.ConnectionStrings.Archiving)
                'dmarchiving.DataBase.Identifier_Date_Format = _plugin.Manager.DesktopGlobal.IdentifierDateFormat
                dmarchiving.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                Dim foldersData = dmarchiving.SchemaRisk.TBL_Folder.DBFindByfk_Precinto(_valorPrecintoActual)

                If (foldersData.Count > 0) Then
                    If (Not _plugin.Manager.Sesion.Usuario.PerfilManager.PuedeEditar(Permisos.Imaging.Proceso.Captura.Reprocesos)) Then
                        DesktopMessageBoxControl.DesktopMessageShow("Para realizar la modificacion del precinto se requiere autorizacion", "Autorizacion", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                    End If
                End If

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Recepcion de precintos", ex)
                Return False
            Finally
                Try : dmarchiving.Connection_Close() : Catch : End Try
            End Try

            Return True
        End Function

        Private Function ValidarPrecinto(ByVal nPrecinto As String) As Boolean
            Dim dbmArchiving As DBArchivingDataBaseManager = Nothing

            Try
                If (OTComboBox.SelectedItem Is Nothing) Then
                    Throw New Exception("Primero se debe seleccionar una fecha de proceso")
                End If

                If (nPrecinto.Length < Program.PrecintoMinCaracteres) Then
                    Throw New Exception("El número de precinto no puede ser menor a " + Program.PrecintoMinCaracteres.ToString() + " digitos")
                End If

                If (nPrecinto.Length > Program.PrecintoMaxCaracteres) Then
                    Throw New Exception("El número de precinto no puede ser mayor a " + Program.PrecintoMaxCaracteres.ToString() + " digitos")
                End If

                dbmArchiving = New DBArchivingDataBaseManager(_plugin.Manager.DesktopGlobal.ConnectionStrings.Archiving)
                'dbmArchiving.DataBase.Identifier_Date_Format = _plugin.Manager.DesktopGlobal.IdentifierDateFormat
                dbmArchiving.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                Dim precintosExistentes = dbmArchiving.SchemaRisk.TBL_Precinto.DBGet(_plugin.Manager.RiskGlobal.Entidad, _plugin.Manager.RiskGlobal.Proyecto, nPrecinto)

                If (precintosExistentes.Count > 0) Then
                    Throw New Exception("Ya existe un precinto cargado con el numero ingresado " + nPrecinto)
                End If

                Dim foldersData = dbmArchiving.SchemaRisk.TBL_Folder.DBFindByfk_Precinto(_valorPrecintoActual)

                If (foldersData.Count > 0) Then
                    If (Not Utilities.ValidarPermiso(Permisos.Imaging.Proceso.Control.Autorizaciones, "Plugin", "Autorizar cambio de numero de precinto de " + _valorPrecintoActual + " a " + nPrecinto, _plugin.Manager.Sesion.Usuario, _plugin.Manager.DesktopGlobal.SecurityServiceURL, _plugin.Manager.DesktopGlobal.ClientIPAddress)) Then
                        Throw New Exception("No se permitio el cambio del número de precinto " + _valorPrecintoActual + " a " + nPrecinto)
                    End If
                End If

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Recepcion de precintos", ex)
                Return False
            Finally
                Try : dbmArchiving.Connection_Close() : Catch : End Try
            End Try

            Return True
        End Function

        Private Sub GuardarPrecinto(ByVal nPrecintoOriginal As String, ByVal nPrecinto As String, ByVal nRowIndex As Integer)

            Dim otId = DirectCast(OTComboBox.SelectedItem, DataRowView).Row(CTA_Risk_OTEnum.id_OT.ColumnName)
            Dim ot = DirectCast(_otsData.Select(CTA_Risk_OTEnum.id_OT.ColumnName + "=" + otId.ToString()), CTA_Risk_OTRow())(0)

            Dim dmarchiving As DBArchivingDataBaseManager = Nothing
            Try
                dmarchiving = New DBArchivingDataBaseManager(_plugin.Manager.DesktopGlobal.ConnectionStrings.Archiving)
                'dmarchiving.DataBase.Identifier_Date_Format = _plugin.Manager.DesktopGlobal.IdentifierDateFormat
                dmarchiving.Connection_Open(_plugin.Manager.Sesion.Usuario.id)
                dmarchiving.Transaction_Begin()

                If (OficinaComboBox.SelectedIndex = -1) Then
                    PrecintosDataGridView(0, nRowIndex).Value = ""
                    MostrarMensajeValidacion("Debe seleccionar oficina para registrar el precinto")
                    Return
                End If

                Dim precintosExistentes = dmarchiving.SchemaRisk.TBL_Precinto.DBGet(_plugin.Manager.RiskGlobal.Entidad, _plugin.Manager.RiskGlobal.Proyecto, nPrecinto)
                If (precintosExistentes.Count > 0) Then
                    PrecintosDataGridView(1, nRowIndex).Value = nPrecintoOriginal
                    Throw New Exception("Ya existe un precinto cargado con el numero ingresado " + nPrecinto)
                End If

                Dim objPrecinto As TBL_PrecintoType = Nothing
                If (Not nPrecintoOriginal Is Nothing AndAlso nPrecintoOriginal <> "") Then
                    Dim precintoReemplazar = dmarchiving.SchemaRisk.TBL_Precinto.DBGet(_plugin.Manager.RiskGlobal.Entidad, _plugin.Manager.RiskGlobal.Proyecto, nPrecintoOriginal)
                    If (precintoReemplazar.Count > 0) Then
                        objPrecinto = precintoReemplazar(0).ToTBL_PrecintoType
                    End If
                End If

                If (objPrecinto Is Nothing) Then
                    objPrecinto = New TBL_PrecintoType()
                    objPrecinto.Fecha_Precinto = SlygNullable.SysDate
                    objPrecinto.Destapado = False
                    objPrecinto.fk_Entidad = ot.fk_Entidad
                    objPrecinto.fk_OT = ot.id_OT
                    objPrecinto.id_Precinto = nPrecinto
                    objPrecinto.fk_Usuario_Log = _plugin.Manager.Sesion.Usuario.id
                    'objPrecinto.fk_Ciudad = Convert.ToInt16(SedeComboBox.SelectedValue)
                    'objPrecinto.fk_Oficina = CInt(OficinaComboBox.SelectedValue.ToString())
                    dmarchiving.SchemaRisk.TBL_Precinto.DBInsert(objPrecinto)
                Else
                    objPrecinto.id_Precinto = nPrecinto
                    objPrecinto.fk_Usuario_Log = _plugin.Manager.Sesion.Usuario.id
                    'objPrecinto.fk_Ciudad = SedeComboBox.SelectedValue
                    'objPrecinto.fk_Oficina = OficinaComboBox.SelectedValue
                    dmarchiving.SchemaRisk.TBL_Precinto.DBUpdate(objPrecinto, _plugin.Manager.RiskGlobal.Entidad, _plugin.Manager.RiskGlobal.Proyecto, nPrecintoOriginal)
                End If

                dmarchiving.Transaction_Commit()

            Catch ex As Exception
                Try : dmarchiving.Transaction_Rollback() : Catch : End Try
                DesktopMessageBoxControl.DesktopMessageShow("Recepcion de precintos", ex)
            Finally
                Try : dmarchiving.Connection_Close() : Catch : End Try
            End Try

        End Sub

        Private Sub EliminarPrecinto()
            Try
                If (PrecintosDataGridView.SelectedRows.Count = 0) Then
                    MessageBox.Show("No se ha seleccionado ningún precinto para eliminar, puede hacer click sobre la primera columna para seleccionar algún precinto", "Recepción", MessageBoxButtons.OK)
                    Return
                End If

                Dim precintoEliminado As Boolean = False

                Dim row = DirectCast(PrecintosDataGridView.SelectedRows(0).DataBoundItem, DataRowView).Row
                Dim precinto = row(0).ToString()

                Dim dbmArchiving As DBArchivingDataBaseManager = Nothing

                Try
                    dbmArchiving = New DBArchivingDataBaseManager(_plugin.Manager.DesktopGlobal.ConnectionStrings.Archiving)
                    'dbmArchiving.DataBase.Identifier_Date_Format = _plugin.Manager.DesktopGlobal.IdentifierDateFormat
                    dbmArchiving.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                    Dim foldersData = dbmArchiving.SchemaRisk.TBL_Folder.DBFindByfk_Precinto(precinto)

                    If (foldersData.Count > 0) Then
                        MessageBox.Show("No se permite eliminar un precinto al que ya se le ha realizado el proceso de destape de contenedores", "Recepción", MessageBoxButtons.OK)
                        Return
                    Else
                        If (Not Utilities.ValidarPermiso(Permisos.Imaging.Proceso.Control.Autorizaciones, "Plugin", "Autorizar, eliminar de precinto " + precinto, _plugin.Manager.Sesion.Usuario, _plugin.Manager.DesktopGlobal.SecurityServiceURL, _plugin.Manager.DesktopGlobal.ClientIPAddress)) Then
                            MessageBox.Show("No se permitió eliminar el precinto " + precinto, "Recepción", MessageBoxButtons.OK)
                            Return
                        End If
                    End If

                    dbmArchiving.SchemaRisk.TBL_Precinto.DBDelete(_plugin.Manager.RiskGlobal.Entidad, _plugin.Manager.RiskGlobal.Proyecto, precinto)
                    precintoEliminado = True

                Catch ex As Exception
                    DesktopMessageBoxControl.DesktopMessageShow("Recepcion de precintos", ex)
                Finally
                    Try : dbmArchiving.Connection_Close() : Catch : End Try
                End Try


                If (precintoEliminado) Then
                    'MostrarPrecintosOT()
                    MostrarPrecintosOF()
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End Sub

        'Private Sub FinalizarRecepcionar()
        '    'Try
        '    '    Throw New NotImplementedException("FinalizarRecepcionar")
        '    'Catch ex As Exception
        '    '    DesktopMessageBox.DesktopMessageShow("Se generó el siguiente error: " + ex.Message, "Error", DesktopMessageBox.IconEnum.ErrorIcon, True)
        '    'End Try
        'End Sub

        Public Function MostrarMensajeValidacion(ByVal mensaje As String) As Boolean
            DesktopMessageBoxControl.DesktopMessageShow(mensaje, "GenerarOT", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
            Return False
        End Function

        Private Function GetNombreEstado(ByVal fkEstado As Short) As String
            Dim estado = DirectCast(fkEstado, DBCore.EstadoEnum)
            Return estado.ToString()
        End Function

        Public Sub AutoNumberRowsForGridView(ByVal dataGridView As DataGridView)
            If dataGridView IsNot Nothing Then
                Dim count As Integer = 0
                While (count <= (dataGridView.Rows.Count - 2))
                    dataGridView.Rows(count).HeaderCell.Value = String.Format((count + 1).ToString(), "0")
                    count += 1
                End While

                dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
            End If

        End Sub
#End Region

    End Class

End Namespace