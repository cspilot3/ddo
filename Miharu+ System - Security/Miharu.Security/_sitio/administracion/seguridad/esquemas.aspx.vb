Imports Miharu.Security._clases

Namespace _sitio.administracion.seguridad

    Partial Public Class esquemas
        Inherits FormBase

#Region " Declaraciones "

        Private Const MyPathPermiso As String = "1.2.3"

        Private tblBase As DBSecurity.SchemaSecurity.TBL_Esquema_SeguridadDataTable
        Private tblEntidad As DBSecurity.SchemaConfig.TBL_EntidadDataTable
        Private tblEsquema As DBSecurity.SchemaSecurity.TBL_Esquema_SeguridadDataTable

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
        Private Sub Page_HijaClose() Handles Me.HijaClose

        End Sub

        Private Sub ddlEntidad_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlEntidad.SelectedIndexChanged
            lblNombreEntidad.Text = ddlEntidad.SelectedItem.Text
            Buscar(ucFiltro.Parametro)
        End Sub

        Private Sub gvBase_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles gvBase.SelectedIndexChanged
            If gvBase.SelectedIndex >= 0 And gvBase.Rows.Count > 0 Then
                EditarRegistro()
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
            tblBase = New DBSecurity.SchemaSecurity.TBL_Esquema_SeguridadDataTable
            Me.MySesion.Pagina.Parameter("tblBase") = tblBase

            tblEntidad = New DBSecurity.SchemaConfig.TBL_EntidadDataTable
            Me.MySesion.Pagina.Parameter("tblEntidad") = tblEntidad

            tblEsquema = New DBSecurity.SchemaSecurity.TBL_Esquema_SeguridadDataTable
            Me.MySesion.Pagina.Parameter("tblEsquema") = tblEsquema

            ' Load Entidades
            Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)

            Try
                dbmSecurity.Connection_Open(MySesion.Usuario.id)
                tblEntidad = dbmSecurity.SchemaConfig.TBL_Entidad.DBFindByNombre_Entidad(Nothing)

            Catch ex As Exception
                Master.ShowAlert(ex.Message, MiharuMasterForm.MsgBoxIcon.IconError)
            Finally
                dbmSecurity.Connection_Close()
            End Try

            ShowEntidades()

        End Sub

        Private Sub Load_Data()
            tblBase = CType(Me.MySesion.Pagina.Parameter("tblBase"), DBSecurity.SchemaSecurity.TBL_Esquema_SeguridadDataTable)
            tblEntidad = CType(Me.MySesion.Pagina.Parameter("tblEntidad"), DBSecurity.SchemaConfig.TBL_EntidadDataTable)
            tblEsquema = CType(Me.MySesion.Pagina.Parameter("tblEsquema"), DBSecurity.SchemaSecurity.TBL_Esquema_SeguridadDataTable)
        End Sub

        Private Sub Buscar(ByVal nParametro As String)
            gvBase.SelectedIndex = -1

            If nParametro <> "" And ddlEntidad.SelectedIndex >= 0 Then
                Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)

                Try
                    dbmSecurity.Connection_Open(MySesion.Usuario.id)
                    tblBase.Clear()
                    dbmSecurity.SchemaSecurity.TBL_Esquema_Seguridad.DBFillByfk_EntidadNombre_Esquema_Seguridad(tblBase, CShort(ddlEntidad.SelectedValue), nParametro)
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
            tblEsquema.Rows.Clear()

            lblNombreEntidad.Text = ddlEntidad.SelectedItem.Text

            lblCodEsquema.Text = "-1"
            txtNombre.Text = ""
            txtLongitud.Text = ""
            txtEspeciales.Text = ""
            txtMayusculas.Text = ""
            txtMinusculas.Text = ""
            txtNumeros.Text = ""
            txtHistorial.Text = ""
            chkCambioContraseña.Checked = True
            txtDias.Text = ""
        End Sub

        Private Sub NuevoRegistro()
            ClearForm()
            tcBase.ActiveTabIndex = 1

            pnlDetalle.Visible = True

            txtNombre.Focus()
            Me.ActivarOpciones(True, True)
        End Sub

        Private Sub EditarRegistro()
            Dim RowBase As DBSecurity.SchemaSecurity.TBL_Esquema_SeguridadRow

            ClearForm()
            tcBase.ActiveTabIndex = 1

            pnlDetalle.Visible = True

            txtNombre.Focus()
            Me.ActivarOpciones(True, False)

            RowBase = CType(tblBase.Rows(gvBase.SelectedRow.DataItemIndex), DBSecurity.SchemaSecurity.TBL_Esquema_SeguridadRow)

            ' Data
            lblCodEsquema.Text = CStr(RowBase.id_Esquema_Seguridad)
            txtNombre.Text = RowBase.Nombre_Esquema_Seguridad
            txtLongitud.Text = CStr(RowBase.Min_Longitud)
            txtEspeciales.Text = CStr(RowBase.Min_Especiales)
            txtMayusculas.Text = CStr(RowBase.Min_Mayusculas)
            txtMinusculas.Text = CStr(RowBase.Min_Minusculas)
            txtNumeros.Text = CStr(RowBase.Min_Numeros)
            txtHistorial.Text = CStr(RowBase.Num_Historial)
            chkCambioContraseña.Checked = RowBase.Cambiar_Password
            txtDias.Text = CStr(RowBase.Dias_Cambio_Password)

            tblEsquema.Rows.Clear()
            tblEsquema.Rows.Add(RowBase.ItemArray)
            tblEsquema.AcceptChanges()
        End Sub

        Private Sub GuardarCambios()
            If Validar() Then
                Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)
                Dim RowEsquema As DBSecurity.SchemaSecurity.TBL_Esquema_SeguridadRow
                Dim isNuevo As Boolean = False

                Try
                    dbmSecurity.Connection_Open(MySesion.Usuario.id)
                    dbmSecurity.Transaction_Begin()
                    'dbmSecurity.DataBase.Identifier_Date_Format = Program.IdentifierDateFormat

                    If lblCodEsquema.Text = "-1" Then
                        isNuevo = True
                        lblCodEsquema.Text = CStr(dbmSecurity.SchemaSecurity.TBL_Esquema_Seguridad.DBNextId_for_fk_Entidad(CShort(ddlEntidad.SelectedValue)))

                        RowEsquema = tblEsquema.NewTBL_Esquema_SeguridadRow

                        RowEsquema.fk_Entidad = CShort(ddlEntidad.SelectedValue)
                        RowEsquema.id_Esquema_Seguridad = CShort(lblCodEsquema.Text)
                    Else
                        RowEsquema = tblEsquema.FindByfk_Entidadid_Esquema_Seguridad(CShort(ddlEntidad.SelectedValue), CShort(lblCodEsquema.Text))
                    End If

                    RowEsquema.Nombre_Esquema_Seguridad = txtNombre.Text
                    RowEsquema.Min_Longitud = CByte(txtLongitud.Text)
                    RowEsquema.Min_Especiales = CByte(txtEspeciales.Text)
                    RowEsquema.Min_Mayusculas = CByte(txtMayusculas.Text)
                    RowEsquema.Min_Minusculas = CByte(txtMinusculas.Text)
                    RowEsquema.Min_Numeros = CByte(txtNumeros.Text)
                    RowEsquema.Num_Historial = CByte(txtHistorial.Text)
                    RowEsquema.Cambiar_Password = chkCambioContraseña.Checked
                    RowEsquema.Dias_Cambio_Password = CShort(txtDias.Text)
                    RowEsquema.fk_Entidad_Log = MySesion.Entidad.id
                    RowEsquema.fk_Usuario_Log = MySesion.Usuario.id
                    RowEsquema.Eliminado = False
                    RowEsquema.Fecha_log = Now



                    If isNuevo Then
                        tblEsquema.Rows.Add(RowEsquema)
                    End If

                    dbmSecurity.SchemaSecurity.TBL_Esquema_Seguridad.DBSaveTable(tblEsquema)

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
                        lblCodEsquema.Text = "-1"
                        tblEsquema.RejectChanges()
                    End If
                Finally
                    dbmSecurity.Connection_Close()
                End Try
            End If
        End Sub

        Private Sub EliminarRegistro()
            Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)

            Try
                dbmSecurity.Connection_Open(MySesion.Usuario.id)
                dbmSecurity.SchemaSecurity.TBL_Esquema_Seguridad.DBDelete(CShort(ddlEntidad.SelectedValue), CShort(lblCodEsquema.Text))

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

            If Not MySesion.Usuario.PerfilManager.PuedeAcceder("1.1.2") Then ' Entidades
                ddlEntidad.SelectedValue = CStr(MySesion.Entidad.id)
                ddlEntidad.Enabled = False
            End If
        End Sub

#End Region

#Region " Funciones "

        Private Function Validar() As Boolean
            If txtNombre.Text = "" Then
                Me.Master.ShowAlert("Debe ingresar el Nombre del Esquema", MiharuMasterForm.MsgBoxIcon.IconError)
                txtNombre.Focus()
                Me.SelectText(txtNombre)

            Else
                Return True

            End If

            Return False
        End Function

#End Region

    End Class

End Namespace