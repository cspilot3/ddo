Imports Miharu.Security._clases

Namespace _sitio.administracion.estructura

    Public Class centrosprocesamiento
        Inherits FormBase

#Region " Declaraciones "

        Private Const MyPathPermiso As String = "1.1.10"

        Private BaseDataTable As DBSecurity.SchemaConfig.TBL_Centro_ProcesamientoDataTable
        Private EntidadDataTable As DBSecurity.SchemaConfig.TBL_EntidadDataTable
        Private SedeDataTable As DBSecurity.SchemaConfig.TBL_SedeDataTable
        Private SedeDataTable_ As DBSecurity.SchemaConfig.CTA_Sedes_CentrosProcesamientoDataTable
        Private CentroProcesamientoDataTable As DBSecurity.SchemaConfig.TBL_Centro_ProcesamientoDataTable
        Private CalendarioDataTable As DBSecurity.SchemaConfig.TBL_CalendarioDataTable
        Private UnidadMapeoDataTable As DBSecurity.SchemaConfig.TBL_Unidad_MapeoDataTable

#End Region

#Region " Eventos "

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            If Not MyBase.ValidarNavegacion(Me.GetType().BaseType.FullName, MyPathPermiso) Then Return

            If Not Me.IsPostBack Then
                Config_Page()
                ActivarOpciones(False, False)
            Else
                Load_Data()
            End If
        End Sub

        Private Sub CentroProcesamiento_HijaClose() Handles Me.HijaClose
            Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)

            Try
                dbmSecurity.Connection_Open(MySesion.Usuario.id)

                dbmSecurity.SchemaConfig.TBL_Centro_Procesamiento.DBSaveTable(BaseDataTable)
            Catch ex As Exception
                Master.ShowAlert(ex.Message, MiharuMasterForm.MsgBoxIcon.IconError)
            Finally
                dbmSecurity.Connection_Close()
            End Try
        End Sub

        Private Sub ddlEntidad_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlEntidad.SelectedIndexChanged
            lblNombreEntidad.Text = ddlEntidad.SelectedItem.Text

            Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)

            Try
                dbmSecurity.Connection_Open(MySesion.Usuario.id)

                ShowSedes(dbmSecurity)
                ShowCalendarios(dbmSecurity)

            Catch ex As Exception
                Master.ShowAlert(ex.Message, MiharuMasterForm.MsgBoxIcon.IconError)
            Finally
                dbmSecurity.Connection_Close()
            End Try
        End Sub

        Private Sub ddlSede_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlSede.SelectedIndexChanged
            lblNombreSede.Text = ddlSede.SelectedItem.Text
            Buscar(ucFiltro.Parametro)
        End Sub

        Protected Sub ddlSedeAsig_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlSedeAsig.SelectedIndexChanged
            ShowCentros()
        End Sub

        Private Sub gvBase_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles gvBase.SelectedIndexChanged
            If gvBase.SelectedIndex >= 0 And gvBase.Rows.Count > 0 Then
                EditarRegistro()
            End If
        End Sub

        Private Sub gvBase_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles gvBase.RowDataBound
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim imgbConsulta As ImageButton = CType(e.Row.FindControl("imgbConsulta"), ImageButton)

                If Not imgbConsulta Is Nothing Then
                    imgbConsulta.CommandArgument = CStr(CType(CType(e.Row.DataItem, DataRowView).Row, DBSecurity.SchemaConfig.TBL_Centro_ProcesamientoRow).id_Centro_Procesamiento)
                End If
            End If
        End Sub

        Private Sub ibAdd_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs) Handles ibAdd.Click
            NuevoRegistro()
        End Sub

        Private Sub ibDelete_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs) Handles ibDelete.Click
            EliminarRegistro()
        End Sub

        Private Sub ibSave_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs) Handles ibSave.Click
            GuardarCambios()
        End Sub

        Private Sub ucFiltro_Click(ByVal nParametro As String) Handles ucFiltro.Click
            Buscar(nParametro)
        End Sub

        Private Sub gvUnidadMapeo_RowDeleting(ByVal sender As Object, ByVal e As GridViewDeleteEventArgs) Handles gvUnidadMapeo.RowDeleting
            DeleteUnidad(CShort(gvUnidadMapeo.Rows(e.RowIndex).DataItemIndex))
        End Sub

        Private Sub btnAddVolumen_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs) Handles btnAddUnidadMapeo.Click
            AddUnidad()
        End Sub

#End Region

#Region " Metodos "

        Private Sub Config_Page()
            BaseDataTable = New DBSecurity.SchemaConfig.TBL_Centro_ProcesamientoDataTable
            Me.MySesion.Pagina.Parameter("BaseDataTable") = BaseDataTable

            EntidadDataTable = New DBSecurity.SchemaConfig.TBL_EntidadDataTable
            Me.MySesion.Pagina.Parameter("EntidadDataTable") = EntidadDataTable

            SedeDataTable = New DBSecurity.SchemaConfig.TBL_SedeDataTable
            Me.MySesion.Pagina.Parameter("SedeDataTable") = SedeDataTable

            SedeDataTable_ = New DBSecurity.SchemaConfig.CTA_Sedes_CentrosProcesamientoDataTable
            Me.MySesion.Pagina.Parameter("SedeDataTable_") = SedeDataTable_

            CentroProcesamientoDataTable = New DBSecurity.SchemaConfig.TBL_Centro_ProcesamientoDataTable
            Me.MySesion.Pagina.Parameter("CentroProcesamientoDataTable") = CentroProcesamientoDataTable

            CalendarioDataTable = New DBSecurity.SchemaConfig.TBL_CalendarioDataTable
            Me.MySesion.Pagina.Parameter("CalendarioDataTable") = CalendarioDataTable

            UnidadMapeoDataTable = New DBSecurity.SchemaConfig.TBL_Unidad_MapeoDataTable
            Me.MySesion.Pagina.Parameter("UnidadMapeoDataTable") = UnidadMapeoDataTable

            ' Load Entidades
            Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)

            Try
                dbmSecurity.Connection_Open(MySesion.Usuario.id)

                EntidadDataTable.Clear()
                dbmSecurity.SchemaConfig.TBL_Entidad.DBFillByNombre_Entidad(EntidadDataTable, Nothing)

                ShowEntidades()
                ShowSedes(dbmSecurity)
                ShowSedesAsignadas(dbmSecurity)
                ShowCalendarios(dbmSecurity)
                ShowCentros()

            Catch ex As Exception
                Master.ShowAlert(ex.Message, MiharuMasterForm.MsgBoxIcon.IconError)
            Finally
                dbmSecurity.Connection_Close()
            End Try
        End Sub

        Private Sub Load_Data()
            BaseDataTable = CType(Me.MySesion.Pagina.Parameter("BaseDataTable"), DBSecurity.SchemaConfig.TBL_Centro_ProcesamientoDataTable)
            EntidadDataTable = CType(Me.MySesion.Pagina.Parameter("EntidadDataTable"), DBSecurity.SchemaConfig.TBL_EntidadDataTable)
            SedeDataTable = CType(Me.MySesion.Pagina.Parameter("SedeDataTable"), DBSecurity.SchemaConfig.TBL_SedeDataTable)
            CentroProcesamientoDataTable = CType(Me.MySesion.Pagina.Parameter("CentroProcesamientoDataTable"), DBSecurity.SchemaConfig.TBL_Centro_ProcesamientoDataTable)
            CalendarioDataTable = CType(Me.MySesion.Pagina.Parameter("CalendarioDataTable"), DBSecurity.SchemaConfig.TBL_CalendarioDataTable)
            UnidadMapeoDataTable = CType(Me.MySesion.Pagina.Parameter("UnidadMapeoDataTable"), DBSecurity.SchemaConfig.TBL_Unidad_MapeoDataTable)
        End Sub

        Private Sub Buscar(ByVal nParametro As String)
            gvBase.SelectedIndex = -1

            If nParametro <> "" And ddlEntidad.SelectedIndex >= 0 And ddlSede.SelectedIndex >= 0 Then
                Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)

                Try
                    dbmSecurity.Connection_Open(MySesion.Usuario.id)
                    BaseDataTable.Clear()
                    dbmSecurity.SchemaConfig.TBL_Centro_Procesamiento.DBFillByfk_Entidadfk_SedeNombre_Centro_Procesamiento(BaseDataTable, CShort(ddlEntidad.SelectedValue), CShort(ddlSede.SelectedValue), nParametro)
                Catch ex As Exception
                    Master.ShowAlert(ex.Message, MiharuMasterForm.MsgBoxIcon.IconError)
                Finally
                    dbmSecurity.Connection_Close()
                End Try
            Else
                BaseDataTable.Rows.Clear()
            End If

            ActivarOpciones(False, False)
            Visualizar_Resultados()
            pnlDetalle.Visible = False
        End Sub

        Private Sub Visualizar_Resultados()
            gvBase.DataSource = BaseDataTable
            gvBase.DataBind()
        End Sub

        Private Sub ClearForm()
            CentroProcesamientoDataTable.Rows.Clear()

            lblNombreEntidad.Text = ddlEntidad.SelectedItem.Text
            lblNombreSede.Text = ddlSede.SelectedItem.Text
            lblCodCentroProcesamiento.Text = "-1"
            txtNombre.Text = ""
            txtPCName.Text = ""
            txtIPName.Text = ""
            txtPathIpServer.Text = ""
            txtPortServer.Text = ""
            txtNameAppServer.Text = ""
            txtServerLocalPath.Text = ""
            ddlCalendario.SelectedIndex = -1
            ddlSedeAsig.SelectedIndex = -1
            ddlCentroProcAsig.SelectedIndex = -1
            UnidadMapeoDataTable.Rows.Clear()
        End Sub

        Private Sub NuevoRegistro()
            ClearForm()
            tcBase.ActiveTabIndex = 1

            pnlDetalle.Visible = True

            txtNombre.Focus()
            UnidadMapeoDataTable.Clear()
            ShowUnidades()
            ShowCentros()
            ValidarExisteCentroProcesamiento(CShort(ddlEntidad.SelectedValue), CShort(ddlSede.SelectedValue))

            Me.ActivarOpciones(True, True)
        End Sub

        Private Sub EditarRegistro()
            Dim RowBase As DBSecurity.SchemaConfig.TBL_Centro_ProcesamientoRow
            Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)
            dbmSecurity.Connection_Open(1)
            Try

                ClearForm()
                tcBase.ActiveTabIndex = 1

                pnlDetalle.Visible = True

                txtNombre.Focus()
                Me.ActivarOpciones(True, False)

                RowBase = CType(BaseDataTable.Rows(gvBase.SelectedRow.DataItemIndex), DBSecurity.SchemaConfig.TBL_Centro_ProcesamientoRow)

                ' Data
                lblCodCentroProcesamiento.Text = CStr(RowBase.id_Centro_Procesamiento)
                txtNombre.Text = RowBase.Nombre_Centro_Procesamiento
                txtPCName.Text = RowBase.PC_Name
                txtIPName.Text = RowBase.IP_Address
                txtServerLocalPath.Text = RowBase.Server_Local_Path
                txtNameAppServer.Text = RowBase.AppName_Servidor
                txtPathIpServer.Text = RowBase.IPName_Servidor
                txtPortServer.Text = CStr(RowBase.Port_Servidor)


                ddlCalendario.SelectedValue = CStr(RowBase.fk_Calendario)


                ''Sede Asignada y Centro de Procesamiento Asignados 

                ShowSedesAsignadas(dbmSecurity)
                ValidarExisteCentroProcesamiento(CShort(ddlEntidad.SelectedValue), CShort(ddlSede.SelectedValue))
                ddlSedeAsig.SelectedValue = CStr(RowBase.fk_Sede_Asignada)
                ShowCentros()
                ddlCentroProcAsig.SelectedValue = CStr(RowBase.fk_Centro_Procesamiento_Asignado)


                If (RowBase.Usa_Cache_Local = True) Then
                    rbtnsi_Cache.Checked = True
                Else
                    rbtnno_Cache.Checked = True
                End If

                If (RowBase.Usa_Cargue_Local = True) Then
                    rbtnsi_Cargue.Checked = True
                Else
                    rbtnsi_Cargue.Checked = True
                End If

                rbtnUsaCacheRecurSI.Checked = RowBase.Usa_Cache_Local_Recursiva
                rbtnUsaCacheRecurNO.Checked = Not RowBase.Usa_Cache_Local_Recursiva

                CentroProcesamientoDataTable.Rows.Clear()
                CentroProcesamientoDataTable.Rows.Add(RowBase.ItemArray)
                CentroProcesamientoDataTable.AcceptChanges()

                ' Unidades de Mapeo

                UnidadMapeoDataTable.Clear()
                dbmSecurity.SchemaConfig.TBL_Unidad_Mapeo.DBFill(UnidadMapeoDataTable, RowBase.fk_Entidad, RowBase.fk_Sede, RowBase.id_Centro_Procesamiento, Nothing)

                ShowUnidades()

                divAddUnidadMapeo.Visible = True

            Catch ex As Exception
                Master.ShowAlert(ex.Message, MiharuMasterForm.MsgBoxIcon.IconError)

            Finally
                dbmSecurity.Connection_Close()

            End Try
        End Sub

        Private Sub GuardarCambios()
            If Validar() Then
                Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)
                Dim RowCentroProcesamiento As DBSecurity.SchemaConfig.TBL_Centro_ProcesamientoRow
                Dim isNuevo As Boolean = False

                Try
                    dbmSecurity.Connection_Open(MySesion.Usuario.id)
                    dbmSecurity.Transaction_Begin()

                    If lblCodCentroProcesamiento.Text = "-1" Then
                        isNuevo = True
                        lblCodCentroProcesamiento.Text = CStr(dbmSecurity.SchemaConfig.TBL_Centro_Procesamiento.DBNextId_for_id_Centro_Procesamiento(CShort(ddlEntidad.SelectedValue), CShort(ddlSede.SelectedValue)))
                        RowCentroProcesamiento = CentroProcesamientoDataTable.NewTBL_Centro_ProcesamientoRow
                        RowCentroProcesamiento.fk_Entidad = CShort(ddlEntidad.SelectedValue)
                        RowCentroProcesamiento.fk_Sede = CShort(ddlSede.SelectedValue)
                        RowCentroProcesamiento.id_Centro_Procesamiento = CShort(lblCodCentroProcesamiento.Text)
                    Else
                        RowCentroProcesamiento = CentroProcesamientoDataTable.FindByfk_Entidadfk_Sedeid_Centro_Procesamiento(CShort(ddlEntidad.SelectedValue), CShort(ddlSede.SelectedValue), CShort(lblCodCentroProcesamiento.Text))
                    End If

                    RowCentroProcesamiento.Nombre_Centro_Procesamiento = txtNombre.Text
                    RowCentroProcesamiento.PC_Name = txtPCName.Text
                    RowCentroProcesamiento.IP_Address = txtIPName.Text
                    RowCentroProcesamiento.Server_Local_Path = txtServerLocalPath.Text
                    RowCentroProcesamiento.fk_Calendario = CShort(ddlCalendario.SelectedValue)
                    RowCentroProcesamiento.AppName_Servidor = txtNameAppServer.Text
                    RowCentroProcesamiento.IPName_Servidor = txtPathIpServer.Text
                    RowCentroProcesamiento.Port_Servidor = CInt(txtPortServer.Text)

                    ''Valida existe el centro de procesamiento 

                    If checkboxDefaultCentro.Checked Then
                        RowCentroProcesamiento.fk_Sede_Asignada = CShort(ddlSede.SelectedValue)
                        RowCentroProcesamiento.fk_Centro_Procesamiento_Asignado = 1
                    Else
                        RowCentroProcesamiento.fk_Sede_Asignada = CShort(ddlSedeAsig.SelectedValue)
                        RowCentroProcesamiento.fk_Centro_Procesamiento_Asignado = CShort(ddlCentroProcAsig.SelectedValue)
                    End If

                    RowCentroProcesamiento.Usa_Cache_Local = rbtnsi_Cache.Checked
                    RowCentroProcesamiento.Usa_Cargue_Local = rbtnsi_Cache.Checked
                    RowCentroProcesamiento.Usa_Cache_Local_Recursiva = rbtnUsaCacheRecurSI.Checked

                    If isNuevo Then
                        'Insertar los datos del Centro de Procesamiento
                        CentroProcesamientoDataTable.Rows.Add(RowCentroProcesamiento)
                        dbmSecurity.SchemaConfig.TBL_Centro_Procesamiento.DBInsert(RowCentroProcesamiento)
                    Else
                        'Actualizar los datos del Centro de Procesamiento
                        Dim CentroP = GenerarTBL_Centro_ProcesamientoTipada(RowCentroProcesamiento)

                        dbmSecurity.SchemaConfig.TBL_Centro_Procesamiento.DBUpdate(CentroP, RowCentroProcesamiento.fk_Entidad, RowCentroProcesamiento.fk_Sede, RowCentroProcesamiento.id_Centro_Procesamiento)
                    End If

                    'Eliminar las Unidades de Mapeo Existentes
                    dbmSecurity.SchemaConfig.TBL_Unidad_Mapeo.DBDelete(CShort(ddlEntidad.SelectedValue), CShort(ddlSede.SelectedValue), CShort(lblCodCentroProcesamiento.Text), Nothing)

                    Dim RowUnidadMapeo As DBSecurity.SchemaConfig.TBL_Unidad_MapeoRow

                    For Each Fila As GridViewRow In gvUnidadMapeo.Rows
                        RowUnidadMapeo = CType(UnidadMapeoDataTable.Rows(Fila.DataItemIndex), DBSecurity.SchemaConfig.TBL_Unidad_MapeoRow)

                        Dim ExisteUnidadMapeo = dbmSecurity.SchemaConfig.TBL_Unidad_Mapeo.DBGet(RowUnidadMapeo.fk_Entidad, RowUnidadMapeo.fk_Sede, RowUnidadMapeo.fk_Centro_Procesamiento, RowUnidadMapeo.id_Unidad_Mapeo)

                        If ExisteUnidadMapeo.Rows.Count > 0 Then
                            'Actualizar los datos de la Unidad de Mapeo
                            Dim Unidad = GenerarTBL_Unidad_MapeoTipada(RowUnidadMapeo)
                            dbmSecurity.SchemaConfig.TBL_Unidad_Mapeo.DBUpdate(Unidad, RowUnidadMapeo.fk_Entidad, RowUnidadMapeo.fk_Sede, RowUnidadMapeo.fk_Centro_Procesamiento, _
                                                                               RowUnidadMapeo.id_Unidad_Mapeo)

                        Else
                            'Crear la Unidad de Mapeo
                            dbmSecurity.SchemaConfig.TBL_Unidad_Mapeo.DBInsert(RowUnidadMapeo)
                        End If
                    Next

                    dbmSecurity.Transaction_Commit()

                    Me.Master.ShowAlert("Los datos se almacenaron correctamente", MiharuMasterForm.MsgBoxIcon.IconInformation)

                    Buscar(ucFiltro.Parametro)

                    ActivarOpciones(True, False)
                    ClearForm()
                    tcBase.ActiveTabIndex = 0

                    pnlDetalle.Visible = False
                    Me.ActivarOpciones(False, False)

                Catch ex As Exception
                    dbmSecurity.Transaction_Rollback()
                    If ex.Message = "Centro procesamiento Asignado Erroneo" Then
                        Me.Master.ShowAlert("Seleccione por Favor un una Sede Asignada que tenga un centro de procesamiento", MiharuMasterForm.MsgBoxIcon.IconInformation)
                    Else
                        Master.ShowAlert(ex.Message, MiharuMasterForm.MsgBoxIcon.IconError)
                    End If

                    If isNuevo Then
                        lblCodCentroProcesamiento.Text = "-1"
                        CentroProcesamientoDataTable.RejectChanges()
                    End If
                Finally
                    dbmSecurity.Connection_Close()
                End Try
            End If
        End Sub

        Private Function GenerarTBL_Centro_ProcesamientoTipada(ByVal RowCentroProcesamiento As DBSecurity.SchemaConfig.TBL_Centro_ProcesamientoRow) As DBSecurity.SchemaConfig.TBL_Centro_ProcesamientoType
            Dim CentroP As New DBSecurity.SchemaConfig.TBL_Centro_ProcesamientoType

            CentroP.fk_Entidad = RowCentroProcesamiento.fk_Entidad
            CentroP.fk_Sede = RowCentroProcesamiento.fk_Sede
            CentroP.id_Centro_Procesamiento = RowCentroProcesamiento.id_Centro_Procesamiento
            CentroP.Nombre_Centro_Procesamiento = RowCentroProcesamiento.Nombre_Centro_Procesamiento
            CentroP.PC_Name = RowCentroProcesamiento.PC_Name
            CentroP.IP_Address = RowCentroProcesamiento.IP_Address
            CentroP.fk_Calendario = RowCentroProcesamiento.fk_Calendario
            CentroP.IPName_Servidor = RowCentroProcesamiento.IPName_Servidor
            CentroP.AppName_Servidor = RowCentroProcesamiento.AppName_Servidor
            CentroP.Port_Servidor = RowCentroProcesamiento.Port_Servidor
            CentroP.fk_Sede_Asignada = RowCentroProcesamiento.fk_Sede_Asignada
            CentroP.fk_Centro_Procesamiento_Asignado = RowCentroProcesamiento.fk_Centro_Procesamiento_Asignado
            CentroP.Usa_Cache_Local = RowCentroProcesamiento.Usa_Cache_Local
            CentroP.Usa_Cache_Local_Recursiva = RowCentroProcesamiento.Usa_Cache_Local_Recursiva
            CentroP.Usa_Cargue_Local = RowCentroProcesamiento.Usa_Cargue_Local
            If Not RowCentroProcesamiento.Isfk_Usuario_ResponsableNull Then
                CentroP.fk_Usuario_Responsable = RowCentroProcesamiento.fk_Usuario_Responsable
            Else
                CentroP.fk_Usuario_Responsable = Nothing
            End If
            CentroP.Server_Local_Path = RowCentroProcesamiento.Server_Local_Path

            Return CentroP
        End Function

        Private Function GenerarTBL_Unidad_MapeoTipada(ByVal RowUnidadMapeo As DBSecurity.SchemaConfig.TBL_Unidad_MapeoRow) As DBSecurity.SchemaConfig.TBL_Unidad_MapeoType
            Dim Unidad As New DBSecurity.SchemaConfig.TBL_Unidad_MapeoType

            Unidad.fk_Entidad = RowUnidadMapeo.fk_Entidad
            Unidad.fk_Sede = RowUnidadMapeo.fk_Sede
            Unidad.fk_Centro_Procesamiento = RowUnidadMapeo.fk_Centro_Procesamiento
            Unidad.Carpeta_Unidad_Mapeo = RowUnidadMapeo.Carpeta_Unidad_Mapeo
            Unidad.Nombre_Unidad_Mapeo = RowUnidadMapeo.Nombre_Unidad_Mapeo
            Unidad.User_Unidad_Mapeo = RowUnidadMapeo.User_Unidad_Mapeo
            Unidad.Password_Unidad_Mapeo = RowUnidadMapeo.Password_Unidad_Mapeo
            Unidad.Activa_Unidad_Mapeo = RowUnidadMapeo.Activa_Unidad_Mapeo
            Unidad.Usar_Usuario_Contexto = RowUnidadMapeo.Usar_Usuario_Contexto

            Return Unidad
        End Function

        Private Sub EliminarRegistro()
            Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)
            Dim RowCentroProcesamiento As DBSecurity.SchemaConfig.TBL_Centro_ProcesamientoRow

            RowCentroProcesamiento = CentroProcesamientoDataTable.FindByfk_Entidadfk_Sedeid_Centro_Procesamiento(CShort(ddlEntidad.SelectedValue), CShort(ddlSede.SelectedValue), CShort(lblCodCentroProcesamiento.Text))
            RowCentroProcesamiento.Delete()

            UnidadMapeoDataTable.Rows.Clear()

            Try
                DBSecurity.DBSecurityDataBaseManager.IdentifierDateFormat = Program.IdentifierDateFormat
                dbmSecurity.Connection_Open(MySesion.Usuario.id)
                dbmSecurity.SchemaConfig.TBL_Centro_Procesamiento.DBSaveTable(CentroProcesamientoDataTable)

                tcBase.ActiveTabIndex = 0
                Buscar(ucFiltro.Parametro)

                Me.Master.ShowAlert("El registro se eliminó exitosamente", MiharuMasterForm.MsgBoxIcon.IconInformation)

                ClearForm()
                tcBase.ActiveTabIndex = 0

                pnlDetalle.Visible = False
                Me.ActivarOpciones(False, False)

            Catch ex As Exception
                Master.ShowAlert(ex.Message, MiharuMasterForm.MsgBoxIcon.IconError)
            Finally
                dbmSecurity.Connection_Close()
            End Try

        End Sub

        Private Sub ActivarOpciones(ByVal nActivo As Boolean, ByVal nIsNew As Boolean)
            ibSave.Visible = nActivo
            divSave.Style("display") = CStr(IIf(ibSave.Visible, "inline", "none"))

            ibDelete.Visible = nActivo And Not nIsNew
            divDelete.Style("display") = CStr(IIf(ibDelete.Visible, "inline", "none"))
        End Sub

        Private Sub ShowEntidades()
            ddlEntidad.DataSource = EntidadDataTable
            ddlEntidad.DataValueField = "id_Entidad"
            ddlEntidad.DataTextField = "Nombre_Entidad"
            ddlEntidad.DataBind()

            If Not MySesion.Usuario.PerfilManager.PuedeAcceder("3.2.1") Then ' Entidades
                ddlEntidad.SelectedValue = CStr(MySesion.Entidad.id)
                ddlEntidad.Enabled = False
            End If
        End Sub

        Private Sub ShowSedes(ByRef nDBMSecurity As DBSecurity.DBSecurityDataBaseManager)
            SedeDataTable.Clear()

            If ddlEntidad.SelectedIndex >= 0 Then
                SedeDataTable.Clear()
                nDBMSecurity.SchemaConfig.TBL_Sede.DBFill(SedeDataTable, Short.Parse(ddlEntidad.SelectedValue), Nothing, Nothing, Nothing)
            End If

            ddlSede.DataSource = SedeDataTable
            ddlSede.DataValueField = "id_Sede"
            ddlSede.DataTextField = "Nombre_Sede"
            ddlSede.DataBind()
        End Sub

        Private Sub ShowCalendarios(ByRef nDBMSecurity As DBSecurity.DBSecurityDataBaseManager)

            CalendarioDataTable.Clear()
            nDBMSecurity.SchemaConfig.TBL_Calendario.DBFill(CalendarioDataTable, CShort(ddlEntidad.SelectedValue), Nothing)
            ddlCalendario.DataSource = CalendarioDataTable
            ddlCalendario.DataValueField = "id_Calendario"
            ddlCalendario.DataTextField = "Nombre_Calendario"
            ddlCalendario.DataBind()
        End Sub

        Private Sub ShowUnidades()
            Dim RowUnidadMapeo As DBSecurity.SchemaConfig.TBL_Unidad_MapeoRow
            Dim Texto As TextBox
            Dim Check As CheckBox

            gvUnidadMapeo.DataSource = UnidadMapeoDataTable
            gvUnidadMapeo.DataBind()

            For Each Fila As GridViewRow In gvUnidadMapeo.Rows
                RowUnidadMapeo = CType(UnidadMapeoDataTable.Rows(Fila.DataItemIndex), DBSecurity.SchemaConfig.TBL_Unidad_MapeoRow)

                Texto = CType(Fila.FindControl("txtNombreUnidadMapeo"), TextBox)
                Texto.Text = RowUnidadMapeo.Nombre_Unidad_Mapeo

                Texto = CType(Fila.FindControl("txtCarpetaUnidadMapeo"), TextBox)
                Texto.Text = RowUnidadMapeo.Carpeta_Unidad_Mapeo

                Texto = CType(Fila.FindControl("txtUnidadMapeo"), TextBox)
                Texto.Text = CStr(RowUnidadMapeo.Unidad_Mapeo)

                Texto = CType(Fila.FindControl("txtUsuarioUnidadMapeo"), TextBox)
                Texto.Text = CStr(RowUnidadMapeo.User_Unidad_Mapeo)

                Texto = CType(Fila.FindControl("txtPasswordUnidadMapeo"), TextBox)
                Texto.Text = CStr(RowUnidadMapeo.Password_Unidad_Mapeo)

                Check = CType(Fila.FindControl("chkActivaUnidadMapeo"), CheckBox)
                Check.Checked = RowUnidadMapeo.Activa_Unidad_Mapeo

                Check = CType(Fila.FindControl("chkUsarUsuarioContexto"), CheckBox)
                Check.Checked = RowUnidadMapeo.Usar_Usuario_Contexto
            Next
        End Sub

        Private Sub UpdateUnidades()
            Dim RowUnidadMapeo As DBSecurity.SchemaConfig.TBL_Unidad_MapeoRow
            Dim Texto As TextBox
            Dim Check As CheckBox

            For Each Fila As GridViewRow In gvUnidadMapeo.Rows
                RowUnidadMapeo = CType(UnidadMapeoDataTable.Rows(Fila.DataItemIndex), DBSecurity.SchemaConfig.TBL_Unidad_MapeoRow)

                Texto = CType(Fila.FindControl("txtNombreUnidadMapeo"), TextBox)
                RowUnidadMapeo.Nombre_Unidad_Mapeo = Texto.Text

                Texto = CType(Fila.FindControl("txtCarpetaUnidadMapeo"), TextBox)
                RowUnidadMapeo.Carpeta_Unidad_Mapeo = Texto.Text

                Texto = CType(Fila.FindControl("txtUnidadMapeo"), TextBox)
                RowUnidadMapeo.Unidad_Mapeo = Texto.Text

                Texto = CType(Fila.FindControl("txtUsuarioUnidadMapeo"), TextBox)
                RowUnidadMapeo.User_Unidad_Mapeo = Texto.Text

                Texto = CType(Fila.FindControl("txtPasswordUnidadMapeo"), TextBox)
                RowUnidadMapeo.Password_Unidad_Mapeo = Texto.Text

                Check = CType(Fila.FindControl("chkActivaUnidadMapeo"), CheckBox)
                RowUnidadMapeo.Activa_Unidad_Mapeo = Check.Checked

                Check = CType(Fila.FindControl("chkUsarUsuarioContexto"), CheckBox)
                RowUnidadMapeo.Usar_Usuario_Contexto = Check.Checked
            Next
        End Sub

        Private Sub AddUnidad()
            UpdateUnidades()

            Dim RowItem As DBSecurity.SchemaConfig.TBL_Unidad_MapeoRow

            RowItem = UnidadMapeoDataTable.NewTBL_Unidad_MapeoRow

            RowItem.fk_Entidad = CShort(ddlEntidad.SelectedValue)
            RowItem.fk_Sede = CShort(ddlSede.SelectedValue)
            RowItem.fk_Centro_Procesamiento = CShort(lblCodCentroProcesamiento.Text)

            If UnidadMapeoDataTable.Count = 0 Then
                RowItem.id_Unidad_Mapeo = 1
            Else
                RowItem.id_Unidad_Mapeo = CByte(CType(UnidadMapeoDataTable.Rows(UnidadMapeoDataTable.Rows.Count - 1), DBSecurity.SchemaConfig.TBL_Unidad_MapeoRow).id_Unidad_Mapeo + 1)
            End If

            RowItem.Nombre_Unidad_Mapeo = ""
            RowItem.Carpeta_Unidad_Mapeo = ""
            RowItem.Unidad_Mapeo = ""
            RowItem.User_Unidad_Mapeo = ""
            RowItem.Password_Unidad_Mapeo = ""
            RowItem.Activa_Unidad_Mapeo = False
            RowItem.Usar_Usuario_Contexto = False

            UnidadMapeoDataTable.Rows.Add(RowItem)

            ShowUnidades()
        End Sub

        Private Sub DeleteUnidad(ByVal nIndex As Short)
            UpdateUnidades()

            Dim RowItem As DBSecurity.SchemaConfig.TBL_Unidad_MapeoRow

            RowItem = CType(UnidadMapeoDataTable.Rows(nIndex), DBSecurity.SchemaConfig.TBL_Unidad_MapeoRow)
            RowItem.Delete()

            UnidadMapeoDataTable.AcceptChanges()

            ShowUnidades()

        End Sub

        Private Sub ShowCentros()
            Dim tblCentroProcesamiento As New DBSecurity.SchemaConfig.TBL_Centro_ProcesamientoDataTable()
            If ddlSedeAsig.SelectedIndex >= 0 Then
                Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)

                Try
                    dbmSecurity.Connection_Open(1)
                    tblCentroProcesamiento.Clear()
                    dbmSecurity.SchemaConfig.TBL_Centro_Procesamiento.DBFill(tblCentroProcesamiento, CShort(ddlEntidad.SelectedValue), CShort(ddlSedeAsig.SelectedValue), Nothing)
                Catch ex As Exception
                    Master.ShowAlert(ex.Message, MiharuMasterForm.MsgBoxIcon.IconError)
                Finally
                    dbmSecurity.Connection_Close()
                End Try

            End If

            ddlCentroProcAsig.DataSource = tblCentroProcesamiento
            ddlCentroProcAsig.DataValueField = "id_Centro_Procesamiento"
            ddlCentroProcAsig.DataTextField = "Nombre_Centro_Procesamiento"
            ddlCentroProcAsig.DataBind()

        End Sub

        Private Sub ShowSedesAsignadas(ByRef nDBMSecurity As DBSecurity.DBSecurityDataBaseManager)
            If ddlEntidad.SelectedIndex >= 0 Then
                SedeDataTable_ = nDBMSecurity.SchemaConfig.CTA_Sedes_CentrosProcesamiento.DBFindByfk_Entidad(Short.Parse(ddlEntidad.SelectedValue))
            End If
            ddlSedeAsig.DataSource = SedeDataTable_
            ddlSedeAsig.DataValueField = "id_Sede"
            ddlSedeAsig.DataTextField = "Nombre_Sede"
            ddlSedeAsig.DataBind()


        End Sub
#End Region

#Region " Funciones "

        Private Function Validar() As Boolean
            If ddlCalendario.SelectedIndex = -1 Then
                Master.ShowAlert("Se debe seleccionar el calendario de transferencia de archivos", MiharuMasterForm.MsgBoxIcon.IconError)
                ddlCalendario.Focus()
            Else
                Return True
            End If

            Return False
        End Function

        Private Sub ValidarExisteCentroProcesamiento(ByVal nfk_Entidad As Short, ByVal nfk_Sede As Short)
            Dim CentroProcesamientoTable As New DBSecurity.SchemaConfig.TBL_Centro_ProcesamientoDataTable()
            Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)
            Try
                dbmSecurity.Connection_Open(1)
                CentroProcesamientoTable.Clear()
                CentroProcesamientoTable = dbmSecurity.SchemaConfig.TBL_Centro_Procesamiento.DBFindByfk_Entidadfk_Sede(nfk_Entidad, nfk_Sede)
                If CentroProcesamientoTable.Rows.Count = 0 Then
                    checkboxDefaultCentro.Visible = True
                    checkboxDefaultCentro.Checked = True
                    lblDefault.Visible = True
                    ddlSedeAsig.Visible = False
                    ddlCentroProcAsig.Visible = False
                    lblSedeAsignada.Visible = False
                    lblCentroAsignado.Visible = False
                    Return
                Else
                    checkboxDefaultCentro.Visible = False
                    checkboxDefaultCentro.Checked = False
                    lblDefault.Visible = False
                    ddlSedeAsig.Visible = True
                    ddlCentroProcAsig.Visible = True
                    lblSedeAsignada.Visible = True
                    lblCentroAsignado.Visible = True
                    Return
                End If
            Catch ex As Exception
                Master.ShowAlert("A ocurrido un error al intentar validar si existe el centro de procesamiento para la sede elegida", MiharuMasterForm.MsgBoxIcon.IconError)
            Finally
                dbmSecurity.Connection_Close()
            End Try

        End Sub

#End Region

    End Class
End Namespace