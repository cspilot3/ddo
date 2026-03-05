Imports Miharu.Security._clases

Namespace _sitio.administracion.estructura

    Partial Public Class puestotrabajo
        Inherits FormBase

#Region " Declaraciones "

        Private Const MyPathPermiso As String = "1.1.9"

        Private tblBase As DBSecurity.SchemaConfig.TBL_Puesto_TrabajoDataTable
        Private tblEntidad As DBSecurity.SchemaConfig.TBL_EntidadDataTable
        Private tblSede As DBSecurity.SchemaConfig.TBL_SedeDataTable
        Private tblCentroProcesamiento As DBSecurity.SchemaConfig.TBL_Centro_ProcesamientoDataTable
        Private tblPuestoTrabajo As DBSecurity.SchemaConfig.TBL_Puesto_TrabajoDataTable

        Private CentroAnterior As String
#End Region

#Region " Eventos "

        Private Sub CentroProcesamiento_HijaClose() Handles Me.HijaClose
            Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)

            Try
                dbmSecurity.Connection_Open()

                dbmSecurity.SchemaConfig.TBL_Puesto_Trabajo.DBSaveTable(tblBase)

            Catch ex As Exception
                Master.ShowAlert(ex.Message, MiharuMasterForm.MsgBoxIcon.IconError)
            Finally
                dbmSecurity.Connection_Close()
            End Try
        End Sub

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            If Not MyBase.ValidarNavegacion(Me.GetType().BaseType.FullName, MyPathPermiso) Then Return

            If Not Me.IsPostBack Then
                Config_Page()
                ActivarOpciones(False, False)
            Else
                Load_Data()
            End If
        End Sub

        Private Sub ddlEntidad_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlEntidad.SelectedIndexChanged
            lblNombreEntidad.Text = ddlEntidad.SelectedItem.Text

            Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)

            Try
                dbmSecurity.Connection_Open(1)
                ShowSedes(dbmSecurity)

            Catch ex As Exception
                Master.ShowAlert(ex.Message, MiharuMasterForm.MsgBoxIcon.IconError)
            Finally
                dbmSecurity.Connection_Close()
            End Try
        End Sub

        Private Sub ddlSede_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlSede.SelectedIndexChanged
            lblNombreSede.Text = ddlSede.SelectedItem.Text
            ShowCentros()
            Buscar(ucFiltro.Parametro)
        End Sub

        Private Sub gvBase_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles gvBase.SelectedIndexChanged
            If gvBase.SelectedIndex >= 0 And gvBase.Rows.Count > 0 Then
                EditarRegistro()
            End If
        End Sub

        Private Sub gvBase_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles gvBase.RowDataBound
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim RowPuestoTrabajo = CType(CType(e.Row.DataItem, DataRowView).Row, DBSecurity.SchemaConfig.TBL_Puesto_TrabajoRow)
                Dim RowCentroProcesamiento = tblCentroProcesamiento.FindByfk_Entidadfk_Sedeid_Centro_Procesamiento(RowPuestoTrabajo.fk_Entidad, RowPuestoTrabajo.fk_Sede, RowPuestoTrabajo.fk_Centro_Procesamiento)

                e.Row.Cells(1).Text = RowCentroProcesamiento.Nombre_Centro_Procesamiento
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

#End Region

#Region " Metodos "

        Private Sub Config_Page()
            tblBase = New DBSecurity.SchemaConfig.TBL_Puesto_TrabajoDataTable
            Me.MySesion.Pagina.Parameter("tblBase") = tblBase

            tblEntidad = New DBSecurity.SchemaConfig.TBL_EntidadDataTable
            Me.MySesion.Pagina.Parameter("tblEntidad") = tblEntidad

            tblSede = New DBSecurity.SchemaConfig.TBL_SedeDataTable
            Me.MySesion.Pagina.Parameter("tblProyecto") = tblSede

            tblCentroProcesamiento = New DBSecurity.SchemaConfig.TBL_Centro_ProcesamientoDataTable
            Me.MySesion.Pagina.Parameter("tblCentroProcesamiento") = tblCentroProcesamiento

            tblPuestoTrabajo = New DBSecurity.SchemaConfig.TBL_Puesto_TrabajoDataTable
            Me.MySesion.Pagina.Parameter("tblPuestoTrabajo") = tblPuestoTrabajo

            Me.MySesion.Pagina.Parameter("CentroAnterior") = CentroAnterior

            ' Load Entidades
            Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)

            Try
                dbmSecurity.Connection_Open(1)

                tblEntidad.Clear()
                dbmSecurity.SchemaConfig.TBL_Entidad.DBFill(tblEntidad, Nothing)

                ShowEntidades()
                ShowSedes(dbmSecurity)
                ShowCentros()

            Catch ex As Exception
                Master.ShowAlert(ex.Message, MiharuMasterForm.MsgBoxIcon.IconError)
            Finally
                dbmSecurity.Connection_Close()
            End Try
        End Sub

        Private Sub Load_Data()
            tblBase = CType(Me.MySesion.Pagina.Parameter("tblBase"), DBSecurity.SchemaConfig.TBL_Puesto_TrabajoDataTable)
            tblEntidad = CType(Me.MySesion.Pagina.Parameter("tblEntidad"), DBSecurity.SchemaConfig.TBL_EntidadDataTable)
            tblSede = CType(Me.MySesion.Pagina.Parameter("tblSede"), DBSecurity.SchemaConfig.TBL_SedeDataTable)
            tblCentroProcesamiento = CType(Me.MySesion.Pagina.Parameter("tblCentroProcesamiento"), DBSecurity.SchemaConfig.TBL_Centro_ProcesamientoDataTable)
            tblPuestoTrabajo = CType(Me.MySesion.Pagina.Parameter("tblPuestoTrabajo"), DBSecurity.SchemaConfig.TBL_Puesto_TrabajoDataTable)
            CentroAnterior = CType(Me.MySesion.Pagina.Parameter("CentroAnterior"), String)
        End Sub

        Private Sub Buscar(ByVal nParametro As String)
            gvBase.SelectedIndex = -1

            If nParametro <> "" And ddlEntidad.SelectedIndex >= 0 And ddlSede.SelectedIndex >= 0 Then
                Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)

                Try
                    dbmSecurity.Connection_Open(1)

                    tblBase.Clear()
                    dbmSecurity.SchemaConfig.TBL_Puesto_Trabajo.DBFillByfk_Entidadfk_SedePC_Name(tblBase, CShort(ddlEntidad.SelectedValue), CShort(ddlSede.SelectedValue), nParametro)

                Catch ex As Exception
                    Master.ShowAlert(ex.Message, MiharuMasterForm.MsgBoxIcon.IconError)
                Finally
                    dbmSecurity.Connection_Close()
                End Try
            Else
                tblBase.Rows.Clear()
            End If

            ActivarOpciones(False, False)
            Visualizar_Resultados()
            pnlDetalle.Visible = False
        End Sub

        Private Sub Visualizar_Resultados()
            gvBase.DataSource = tblBase
            gvBase.DataBind()
        End Sub

        Private Sub ClearForm()

            ShowCentros()
            tblPuestoTrabajo.Rows.Clear()

            lblNombreEntidad.Text = ddlEntidad.SelectedItem.Text
            lblNombreSede.Text = ddlSede.SelectedItem.Text
            ddlCentro.SelectedIndex = -1

            lblCodPuestoTrabajo.Text = "-1"
            txtPCName.Text = ""
            txtIPName.Text = ""

        End Sub

        Private Sub NuevoRegistro()
            ClearForm()
            tcBase.ActiveTabIndex = 1

            pnlDetalle.Visible = True

            ddlCentro.Focus()
            Me.ActivarOpciones(True, True)
        End Sub

        Private Sub EditarRegistro()
            Dim RowBase As DBSecurity.SchemaConfig.TBL_Puesto_TrabajoRow

            ClearForm()

            tcBase.ActiveTabIndex = 1

            pnlDetalle.Visible = True

            ddlCentro.Focus()
            Me.ActivarOpciones(True, False)

            RowBase = CType(tblBase.Rows(gvBase.SelectedRow.DataItemIndex), DBSecurity.SchemaConfig.TBL_Puesto_TrabajoRow)

            ' Data
            ddlCentro.SelectedValue = RowBase.fk_Centro_Procesamiento.ToString()
            Me.MySesion.Pagina.Parameter("CentroAnterior") = RowBase.fk_Centro_Procesamiento.ToString()
            lblCodPuestoTrabajo.Text = CStr(RowBase.id_Puesto_Trabajo)
            txtPCName.Text = RowBase.PC_Name
            txtIPName.Text = RowBase.IP_Address
            txtDescripción.Text = RowBase.Descripcion_Puesto_Trabajo
            cbxTieneCamara.Checked = RowBase.TieneCamara
            txtCodigoCamara.Text = RowBase.Codigo_Camara

            tblPuestoTrabajo.Rows.Clear()
            tblPuestoTrabajo.Rows.Add(RowBase.ItemArray)
            tblPuestoTrabajo.AcceptChanges()
        End Sub

        Private Sub GuardarCambios()
            Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)
            Dim RowPuestoTrabajo As DBSecurity.SchemaConfig.TBL_Puesto_TrabajoRow
            Dim isNuevo As Boolean = False

            Try
                dbmSecurity.Connection_Open(1)
                dbmSecurity.Transaction_Begin()

                If lblCodPuestoTrabajo.Text = "-1" Then
                    isNuevo = True
                    lblCodPuestoTrabajo.Text = CStr(dbmSecurity.SchemaConfig.TBL_Puesto_Trabajo.DBNextId(CShort(ddlEntidad.SelectedValue), CShort(ddlSede.SelectedValue), CShort(ddlCentro.SelectedValue)))

                    RowPuestoTrabajo = tblPuestoTrabajo.NewTBL_Puesto_TrabajoRow
                    RowPuestoTrabajo.fk_Entidad = CShort(ddlEntidad.SelectedValue)
                    RowPuestoTrabajo.fk_Sede = CShort(ddlSede.SelectedValue)
                    RowPuestoTrabajo.fk_Centro_Procesamiento = CShort(ddlCentro.SelectedValue)

                Else
                    RowPuestoTrabajo = tblPuestoTrabajo.FindByfk_Entidadfk_Sedefk_Centro_Procesamientoid_Puesto_Trabajo(CShort(ddlEntidad.SelectedValue), CShort(ddlSede.SelectedValue), CShort(CentroAnterior), CShort(lblCodPuestoTrabajo.Text))
                    If CShort(CentroAnterior) <> CShort(ddlCentro.SelectedValue) Then
                        lblCodPuestoTrabajo.Text = CStr(dbmSecurity.SchemaConfig.TBL_Puesto_Trabajo.DBNextId(CShort(ddlEntidad.SelectedValue), CShort(ddlSede.SelectedValue), CShort(ddlCentro.SelectedValue)))
                    End If
                End If

                RowPuestoTrabajo.fk_Centro_Procesamiento = CShort(ddlCentro.SelectedValue)
                RowPuestoTrabajo.id_Puesto_Trabajo = CShort(lblCodPuestoTrabajo.Text)
                RowPuestoTrabajo.PC_Name = txtPCName.Text
                RowPuestoTrabajo.IP_Address = txtIPName.Text
                RowPuestoTrabajo.Descripcion_Puesto_Trabajo = txtDescripción.Text
                RowPuestoTrabajo.TieneCamara = cbxTieneCamara.Checked
                If cbxTieneCamara.Checked Then
                    If (txtCodigoCamara.Text.Trim = "") Then
                        Throw New Exception("El Código de la Cámara")
                    Else
                        RowPuestoTrabajo.Codigo_Camara = txtCodigoCamara.Text
                    End If
                Else
                    RowPuestoTrabajo.Codigo_Camara = " "
                End If


                If isNuevo Then
                    tblPuestoTrabajo.Rows.Add(RowPuestoTrabajo)
                End If

                dbmSecurity.SchemaConfig.TBL_Puesto_Trabajo.DBSaveTable(tblPuestoTrabajo)

                dbmSecurity.Transaction_Commit()

                Me.Master.ShowAlert("Los datos se almacenaron correctamente", MiharuMasterForm.MsgBoxIcon.IconInformation)

                Buscar(ucFiltro.Parametro)

                ActivarOpciones(True, False)
                pnlDetalle.Visible = True
                tcBase.ActiveTabIndex = 1

            Catch ex As Exception
                dbmSecurity.Transaction_Rollback()
                Master.ShowAlert(ex.Message, MiharuMasterForm.MsgBoxIcon.IconError)

                If isNuevo Then
                    lblCodPuestoTrabajo.Text = "-1"
                    tblPuestoTrabajo.RejectChanges()
                End If
            Finally
                dbmSecurity.Connection_Close()
            End Try
        End Sub

        Private Sub EliminarRegistro()
            Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)
            Dim RowPuestoTrabajo As DBSecurity.SchemaConfig.TBL_Puesto_TrabajoRow

            RowPuestoTrabajo = tblPuestoTrabajo.FindByfk_Entidadfk_Sedefk_Centro_Procesamientoid_Puesto_Trabajo(CShort(ddlEntidad.SelectedValue), CShort(ddlSede.SelectedValue), CShort(ddlCentro.SelectedValue), CShort(lblCodPuestoTrabajo.Text))
            RowPuestoTrabajo.Delete()

            Try
                dbmSecurity.Connection_Open(1)
                dbmSecurity.SchemaConfig.TBL_Puesto_Trabajo.DBSaveTable(tblPuestoTrabajo)

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
            ddlEntidad.DataSource = tblEntidad
            ddlEntidad.DataValueField = "id_Entidad"
            ddlEntidad.DataTextField = "Nombre_Entidad"
            ddlEntidad.DataBind()

            If Not MySesion.Usuario.PerfilManager.PuedeAcceder("3.2.1") Then ' Entidades
                ddlEntidad.SelectedValue = CStr(MySesion.Entidad.id)
                ddlEntidad.Enabled = False
            End If
        End Sub

        Private Sub ShowSedes(ByRef nDBMSecurity As DBSecurity.DBSecurityDataBaseManager)
            tblSede.Clear()

            If ddlEntidad.SelectedIndex >= 0 Then
                tblSede.Clear()
                nDBMSecurity.SchemaConfig.TBL_Sede.DBFillByfk_EntidadNombre_Sede(tblSede, CShort(ddlEntidad.SelectedValue), Nothing)
            End If

            ddlSede.DataSource = tblSede
            ddlSede.DataValueField = "id_Sede"
            ddlSede.DataTextField = "Nombre_Sede"
            ddlSede.DataBind()

            ShowCentros()
        End Sub

        Private Sub ShowCentros()
            tblCentroProcesamiento.Clear()

            If ddlSede.SelectedIndex >= 0 Then
                Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)

                Try
                    dbmSecurity.Connection_Open(1)
                    tblCentroProcesamiento.Clear()
                    dbmSecurity.SchemaConfig.TBL_Centro_Procesamiento.DBFill(tblCentroProcesamiento, CShort(ddlEntidad.SelectedValue), CShort(ddlSede.SelectedValue), Nothing)
                Catch ex As Exception
                    Master.ShowAlert(ex.Message, MiharuMasterForm.MsgBoxIcon.IconError)
                Finally
                    dbmSecurity.Connection_Close()
                End Try

            End If

            ddlCentro.DataSource = tblCentroProcesamiento
            ddlCentro.DataValueField = "id_Centro_Procesamiento"
            ddlCentro.DataTextField = "Nombre_Centro_Procesamiento"
            ddlCentro.DataBind()

        End Sub

#End Region

    End Class

End Namespace