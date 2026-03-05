Imports Miharu.Security.Library.SecurityServiceReference
Imports Miharu.Security.Library.WebService
Imports Miharu.Security._clases
Imports Slyg.Tools.Cryptographic

Namespace _sitio.administracion.seguridad

    Partial Public Class usuarios
        Inherits FormBase

#Region " Declaraciones "

        Private Const MyPathPermiso As String = "1.2.4"

        Private tblBase As DBSecurity.SchemaSecurity.TBL_UsuarioDataTable
        Private tblEntidad As DBSecurity.SchemaConfig.TBL_EntidadDataTable
        Private tblDependencia As DBSecurity.SchemaConfig.TBL_DependenciaDataTable
        Private tblPerfiles As DBSecurity.SchemaSecurity.TBL_PerfilDataTable
        Private tblRoles As DBSecurity.SchemaSecurity.TBL_RolDataTable
        Private tblEsquema As DBSecurity.SchemaSecurity.TBL_Esquema_SeguridadDataTable
        Private tblUsuario As DBSecurity.SchemaSecurity.TBL_UsuarioDataTable
        Private tblUsuarioPerfiles As DBSecurity.SchemaSecurity.TBL_Usuario_PerfilesDataTable
        Private tblUsuarioRoles As DBSecurity.SchemaSecurity.TBL_Usuario_RolesDataTable

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
            lblCodJefe.Text = CStr(MySesion.Pagina.Parameter("JefeId"))
            txtJefe.Text = CStr(MySesion.Pagina.Parameter("JefeNombre"))

            ibDeleteJefe.Visible = (lblCodJefe.Text <> "-1")
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

        Private Sub icbJefe_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs) Handles icbJefe.Click
            BuscarJefe()
        End Sub

        Private Sub ibDeleteJefe_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs) Handles ibDeleteJefe.Click
            EliminarJefe()
        End Sub

        Private Sub ucFiltro_Click(ByVal nParametro As String) Handles ucFiltro.Click
            Buscar(nParametro)
        End Sub

        Private Sub PasswordAceptarButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles PasswordAceptarButton.Click
            ModalPopupPassword.Hide()
            AsignarPassword()
        End Sub

#End Region

#Region " Metodos "

        Private Sub Config_Page()
            tblBase = New DBSecurity.SchemaSecurity.TBL_UsuarioDataTable
            Me.MySesion.Pagina.Parameter("tblBase") = tblBase

            tblEntidad = New DBSecurity.SchemaConfig.TBL_EntidadDataTable
            Me.MySesion.Pagina.Parameter("tblEntidad") = tblEntidad

            tblEsquema = New DBSecurity.SchemaSecurity.TBL_Esquema_SeguridadDataTable
            Me.MySesion.Pagina.Parameter("tblEsquema") = tblEsquema

            tblDependencia = New DBSecurity.SchemaConfig.TBL_DependenciaDataTable
            Me.MySesion.Pagina.Parameter("tblDependencia") = tblDependencia

            tblUsuario = New DBSecurity.SchemaSecurity.TBL_UsuarioDataTable
            Me.MySesion.Pagina.Parameter("tblUsuario") = tblUsuario

            tblUsuarioPerfiles = New DBSecurity.SchemaSecurity.TBL_Usuario_PerfilesDataTable
            Me.MySesion.Pagina.Parameter("tblUsuarioPerfiles") = tblUsuarioPerfiles

            tblUsuarioRoles = New DBSecurity.SchemaSecurity.TBL_Usuario_RolesDataTable
            Me.MySesion.Pagina.Parameter("tblUsuarioRoles") = tblUsuarioRoles

            Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)

            Try
                dbmSecurity.Connection_Open(MySesion.Usuario.id)

                ' Entidades
                If MySesion.Usuario.isRoot Then
                    tblEntidad = dbmSecurity.SchemaConfig.TBL_Entidad.DBFindByid_Entidad(CShort(0))
                End If

                tblEntidad = dbmSecurity.SchemaConfig.TBL_Entidad.DBFindByNombre_Entidad(Nothing)

                ' Perfiles
                If MySesion.Usuario.PerfilManager.PuedeAcceder("1.2.2") Then
                    tblPerfiles = dbmSecurity.SchemaSecurity.TBL_Perfil.DBGet(Nothing)
                Else
                    tblPerfiles = dbmSecurity.SchemaSecurity.PA_Perfil_find_Usuario.DBExecute(MySesion.Usuario.id)
                End If

                'Roles
                If MySesion.Usuario.PerfilManager.PuedeAcceder("1.2.7") Then
                    tblRoles = dbmSecurity.SchemaSecurity.TBL_Rol.DBGet(Nothing)
                Else
                    'TODO: CRear Procedimiento de Roles: tblRoles = dbmSecurity.SchemaSecurity.PA_Perfil_find_Usuario.DBExecute(MySesion.Usuario.id)
                End If


                Me.MySesion.Pagina.Parameter("tblPerfiles") = tblPerfiles
                Me.MySesion.Pagina.Parameter("tblRoles") = tblRoles

            Catch ex As Exception
                Master.ShowAlert(ex.Message, MiharuMasterForm.MsgBoxIcon.IconError)
            Finally
                dbmSecurity.Connection_Close()
            End Try

            ShowEntidades()
            ShowPerfiles()
            ShowRoles()
        End Sub

        Private Sub Load_Data()
            tblBase = CType(Me.MySesion.Pagina.Parameter("tblBase"), DBSecurity.SchemaSecurity.TBL_UsuarioDataTable)
            tblEntidad = CType(Me.MySesion.Pagina.Parameter("tblEntidad"), DBSecurity.SchemaConfig.TBL_EntidadDataTable)
            tblEsquema = CType(Me.MySesion.Pagina.Parameter("tblEsquema"), DBSecurity.SchemaSecurity.TBL_Esquema_SeguridadDataTable)
            tblDependencia = CType(Me.MySesion.Pagina.Parameter("tblDependencia"), DBSecurity.SchemaConfig.TBL_DependenciaDataTable)
            tblPerfiles = CType(Me.MySesion.Pagina.Parameter("tblPerfiles"), DBSecurity.SchemaSecurity.TBL_PerfilDataTable)
            tblRoles = CType(Me.MySesion.Pagina.Parameter("tblRoles"), DBSecurity.SchemaSecurity.TBL_RolDataTable)
            tblUsuario = CType(Me.MySesion.Pagina.Parameter("tblUsuario"), DBSecurity.SchemaSecurity.TBL_UsuarioDataTable)
            tblUsuarioPerfiles = CType(Me.MySesion.Pagina.Parameter("tblUsuarioPerfiles"), DBSecurity.SchemaSecurity.TBL_Usuario_PerfilesDataTable)
            tblUsuarioRoles = CType(Me.MySesion.Pagina.Parameter("tblUsuarioRoles"), DBSecurity.SchemaSecurity.TBL_Usuario_RolesDataTable)
        End Sub

        Private Sub Buscar(ByVal nParametro As String)
            gvBase.SelectedIndex = -1

            If nParametro <> "" And ddlEntidad.SelectedIndex >= 0 Then
                Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)

                Try
                    dbmSecurity.Connection_Open(MySesion.Usuario.id)
                    tblBase.Clear()
                    dbmSecurity.SchemaSecurity.TBL_Usuario.DBFillByfk_EntidadApellidos_Usuario(tblBase, CShort(ddlEntidad.SelectedValue), nParametro)
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

        Private Sub ClearForm(ByRef nDBMSecurity As DBSecurity.DBSecurityDataBaseManager)
            tblUsuario.Rows.Clear()
            tblUsuarioPerfiles.Rows.Clear()
            tblUsuarioRoles.Rows.Clear()

            lblNombreEntidad.Text = ddlEntidad.SelectedItem.Text

            btnAsignarPassword.Visible = False

            ' Dependencias
            tblDependencia.Clear()
            nDBMSecurity.SchemaConfig.TBL_Dependencia.DBFillByfk_Entidadid_Dependencia(tblDependencia, CShort(ddlEntidad.SelectedValue), Nothing)

            ' Esquemas
            tblEsquema.Clear()
            nDBMSecurity.SchemaSecurity.TBL_Esquema_Seguridad.DBFillByfk_EntidadNombre_Esquema_Seguridad(tblEsquema, CShort(ddlEntidad.SelectedValue), Nothing)

            txtLogin.Text = ""
            txtNombres.Text = ""
            txtApellidos.Text = ""
            txtIdentificacion.Text = ""
            txtEmail.Text = ""
            txtDireccion.Text = ""
            txtTelefono.Text = ""

            txtObservaciones.Text = ""
            chkActivo.Checked = True
            chkCambio.Checked = True

            txtJefe.Text = ""
            lblCodJefe.Text = "-1"

            tcDetalle.ActiveTabIndex = 0

            If ddlEsquemaSeguridad.Items.Count > 0 Then ddlEsquemaSeguridad.SelectedIndex = 0
            If ddlDependencia.Items.Count > 0 Then ddlDependencia.SelectedIndex = 0

            ShowDependencia()
        End Sub

        Private Sub NuevoRegistro()
            lblCodUsuario.Text = "-1"
            lblCodJefe.Text = ""

            Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)

            Try
                dbmSecurity.Connection_Open(MySesion.Usuario.id)

                ClearForm(dbmSecurity)

            Catch ex As Exception
                Master.ShowAlert(ex.Message, MiharuMasterForm.MsgBoxIcon.IconError)
            Finally
                dbmSecurity.Connection_Close()
            End Try

            tcBase.ActiveTabIndex = 1

            pnlDetalle.Visible = True

            ShowEsquemaSeguridad()
            ShowPerfiles()
            ShowRoles()

            txtLogin.Focus()
            Me.ActivarOpciones(True, True)
        End Sub

        Private Sub EditarRegistro()
            Dim RowBase As DBSecurity.SchemaSecurity.TBL_UsuarioRow

            RowBase = CType(tblBase.Rows(gvBase.SelectedRow.DataItemIndex), DBSecurity.SchemaSecurity.TBL_UsuarioRow)
            lblCodUsuario.Text = CStr(RowBase.id_Usuario)

            Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)

            Try
                dbmSecurity.Connection_Open(MySesion.Usuario.id)

                ClearForm(dbmSecurity)

                ' Data
                txtLogin.Text = RowBase.Login_Usuario
                txtNombres.Text = RowBase.Nombres_Usuario
                txtApellidos.Text = RowBase.Apellidos_Usuario
                txtIdentificacion.Text = RowBase.Identificacion_Usuario
                txtEmail.Text = RowBase.Email_Usuario
                txtDireccion.Text = RowBase.Direccion_Usuario
                txtTelefono.Text = RowBase.Telefono_Usuario
                chkActivo.Checked = RowBase.Usuario_Activo
                chkCambio.Checked = RowBase.Solicitar_Cambio_Password


                Try : ddlEsquemaSeguridad.SelectedValue = CStr(RowBase.fk_Esquema_Seguridad) : Catch : End Try

                If Not RowBase.Isfk_DependenciaNull Then
                    ddlDependencia.SelectedValue = CStr(RowBase.fk_Dependencia)
                Else
                    If ddlDependencia.Items.Count > 0 Then ddlDependencia.SelectedIndex = 0
                End If

                tblUsuario.Rows.Clear()
                tblUsuario.Rows.Add(RowBase.ItemArray)
                tblUsuario.AcceptChanges()

                ' Perfiles usuario
                dbmSecurity.SchemaSecurity.TBL_Usuario_Perfiles.DBFillByfk_Usuario(tblUsuarioPerfiles, CInt(lblCodUsuario.Text))

                If RowBase.Isfk_Usuario_JefeNull Then
                    txtJefe.Text = ""
                    lblCodJefe.Text = "-1"
                Else
                    txtJefe.Text = dbmSecurity.SchemaSecurity.TBL_Usuario.DBFindByfk_Entidadid_Usuario(RowBase.fk_Entidad, RowBase.fk_Usuario_Jefe)(0).Apellidos_Usuario.ToString() + ", " + dbmSecurity.SchemaSecurity.TBL_Usuario.DBFindByfk_Entidadid_Usuario(RowBase.fk_Entidad, RowBase.fk_Usuario_Jefe)(0).Nombres_Usuario.ToString()
                    lblCodJefe.Text = CStr(RowBase.fk_Usuario_Jefe)
                End If

                'Roles de Usuario
                dbmSecurity.SchemaSecurity.TBL_Usuario_Roles.DBFillByfk_Usuario(tblUsuarioRoles, CInt(lblCodUsuario.Text))

            Catch ex As Exception
                Master.ShowAlert(ex.Message, MiharuMasterForm.MsgBoxIcon.IconError)
            Finally
                dbmSecurity.Connection_Close()
            End Try

            tcBase.ActiveTabIndex = 1

            pnlDetalle.Visible = True

            btnAsignarPassword.Visible = True

            txtLogin.Focus()
            Me.ActivarOpciones(True, False)

            ShowEsquemaSeguridad()
            ShowPerfiles()
            ShowRoles()
        End Sub

        Private Sub GuardarCambios()
            Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)
            Dim RowUsuario As DBSecurity.SchemaSecurity.TBL_UsuarioRow
            Dim isNuevo As Boolean = False

            Try
                dbmSecurity.Connection_Open(MySesion.Usuario.id)
                'dbmSecurity.DataBase.Identifier_Date_Format = Program.IdentifierDateFormat

                dbmSecurity.Transaction_Begin()

                If Validar(dbmSecurity) Then
                    If lblCodUsuario.Text = "-1" Then
                        isNuevo = True
                        lblCodUsuario.Text = CStr(dbmSecurity.SchemaSecurity.TBL_Usuario.DBNextId())

                        RowUsuario = tblUsuario.NewTBL_UsuarioRow

                        RowUsuario.fk_Entidad = CShort(ddlEntidad.SelectedValue)
                        RowUsuario.id_Usuario = CInt(lblCodUsuario.Text)
                        RowUsuario.Fecha_Asignacion_Password = Now
                        RowUsuario.Password_Usuario = Crypto.HASH.Encrypt(Now.ToString(), "$|yg" & Format(Now, "hhmmss"), 100)
                    Else
                        RowUsuario = tblUsuario.FindByid_Usuario(CInt(lblCodUsuario.Text))
                    End If

                    UpdatePerfiles()
                    UpdateRoles()

                    RowUsuario.Nombres_Usuario = txtNombres.Text
                    RowUsuario.Apellidos_Usuario = txtApellidos.Text
                    RowUsuario.Identificacion_Usuario = txtIdentificacion.Text
                    RowUsuario.Login_Usuario = txtLogin.Text
                    RowUsuario.Email_Usuario = txtEmail.Text
                    RowUsuario.Direccion_Usuario = txtDireccion.Text
                    RowUsuario.Telefono_Usuario = txtTelefono.Text
                    RowUsuario.Solicitar_Cambio_Password = chkCambio.Checked
                    RowUsuario.Usuario_Activo = chkActivo.Checked
                    RowUsuario.fk_Esquema_Seguridad = CShort(ddlEsquemaSeguridad.SelectedValue)
                    RowUsuario.fk_Dependencia = CShort(ddlDependencia.SelectedValue)
                    RowUsuario.fk_Usuario_Log = MySesion.Usuario.id
                    RowUsuario.Eliminado_Usuario = False
                    RowUsuario.Fecha_log = Now
                    RowUsuario.Logeado = False

                    RowUsuario.Observaciones = txtObservaciones.Text

                    If lblCodJefe.Text = "-1" Then
                        RowUsuario.Setfk_Usuario_JefeNull()
                    Else
                        RowUsuario.fk_Usuario_Jefe = CInt(lblCodJefe.Text)
                    End If

                    If isNuevo Then
                        RowUsuario.LDAP = False
                        tblUsuario.Rows.Add(RowUsuario)
                    End If

                    dbmSecurity.SchemaSecurity.TBL_Usuario.DBSaveTable(tblUsuario)

                    dbmSecurity.SchemaSecurity.TBL_Usuario_Perfiles.DBDelete(RowUsuario.id_Usuario, Nothing)
                    dbmSecurity.SchemaSecurity.TBL_Usuario_Perfiles.DBSaveTable(tblUsuarioPerfiles)

                    dbmSecurity.SchemaSecurity.TBL_Usuario_Roles.DBDelete(RowUsuario.id_Usuario, Nothing)
                    dbmSecurity.SchemaSecurity.TBL_Usuario_Roles.DBSaveTable(tblUsuarioRoles)

                    If RowUsuario.Usuario_Activo Then
                        dbmSecurity.SchemaSecurity.TBL_Conexiones_Usuario.DBDelete(RowUsuario.id_Usuario, Nothing)
                    End If

                    dbmSecurity.Transaction_Commit()

                    Me.Master.ShowAlert("Los datos se almacenaron correctamente", MiharuMasterForm.MsgBoxIcon.IconInformation)

                    Buscar(ucFiltro.Parametro)

                    btnAsignarPassword.Visible = True

                    ActivarOpciones(True, False)
                    pnlDetalle.Visible = True
                    tcBase.ActiveTabIndex = 1

                End If

            Catch ex As Exception
                dbmSecurity.Transaction_Rollback()

                Master.ShowAlert(ex.Message + "------" + ex.StackTrace, MiharuMasterForm.MsgBoxIcon.IconError)

                If isNuevo Then
                    lblCodUsuario.Text = "-1"
                    tblUsuario.RejectChanges()
                End If
            Finally
                dbmSecurity.Connection_Close()
            End Try

        End Sub

        Private Sub EliminarRegistro()
            Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)
            Try
                dbmSecurity.Connection_Open(MySesion.Usuario.id)

                dbmSecurity.SchemaSecurity.TBL_Usuario.DBDelete(CInt(lblCodUsuario.Text))

                ClearForm(dbmSecurity)

                tcBase.ActiveTabIndex = 0
                Buscar(ucFiltro.Parametro)

                Me.Master.ShowAlert("El registro se eliminó exitosamente", MiharuMasterForm.MsgBoxIcon.IconInformation)

                tcBase.ActiveTabIndex = 0
                pnlDetalle.Visible = False
                Me.ActivarOpciones(False, False)
                lblCodUsuario.Text = "-1"

            Catch ex As Exception
                Master.ShowAlert(ex.Message, MiharuMasterForm.MsgBoxIcon.IconError)
            Finally
                dbmSecurity.Connection_Close()
            End Try
        End Sub

        Private Sub ActivarOpciones(ByVal nActivo As Boolean, ByVal nIsNew As Boolean)
            ibSave.Visible = nActivo
            divSave.Style("display") = CStr(IIf(ibSave.Visible, "inline", "none"))

            If ddlEntidad.SelectedValue = "0" Then ' SLYG
                ibDelete.Visible = False
                divDelete.Style("display") = "none"

                divDeleteJefe.Visible = False
                ibDeleteJefe.Visible = False


                txtLogin.Enabled = False
                txtNombres.Enabled = False
                txtApellidos.Enabled = False
                txtIdentificacion.Enabled = False
                txtEmail.Enabled = False
                txtDireccion.Enabled = False
                txtTelefono.Enabled = False
                ddlDependencia.Enabled = False
                txtObservaciones.Enabled = False


                chkActivo.Enabled = False
                chkCambio.Enabled = False

                icbJefe.Enabled = False

            Else
                ibSave.Visible = nActivo
                divSave.Style("display") = CStr(IIf(ibSave.Visible, "inline", "none"))

                ibDelete.Visible = nActivo And Not nIsNew
                divDelete.Style("display") = CStr(IIf(ibDelete.Visible, "inline", "none"))

                divDeleteJefe.Visible = (lblCodJefe.Text <> "-1")
                ibDeleteJefe.Visible = (lblCodJefe.Text <> "-1")

                txtLogin.Enabled = True
                txtNombres.Enabled = True
                txtApellidos.Enabled = True
                txtIdentificacion.Enabled = True
                txtEmail.Enabled = True
                txtDireccion.Enabled = True
                txtTelefono.Enabled = True
                ddlDependencia.Enabled = True
                txtObservaciones.Enabled = True

                chkActivo.Enabled = True
                chkCambio.Enabled = True

                icbJefe.Enabled = True
            End If
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

        Private Sub ShowPerfiles()
            Dim RowPerfil As DBSecurity.SchemaSecurity.TBL_PerfilRow
            Dim RowUsuarioPerfil As DBSecurity.SchemaSecurity.TBL_Usuario_PerfilesRow
            Dim activoCheckBox As CheckBox

            gvPerfiles.DataSource = tblPerfiles
            gvPerfiles.DataBind()

            For Each RowItem As GridViewRow In gvPerfiles.Rows
                RowPerfil = CType(tblPerfiles.Rows(RowItem.DataItemIndex), DBSecurity.SchemaSecurity.TBL_PerfilRow)

                RowUsuarioPerfil = tblUsuarioPerfiles.FindByfk_Usuariofk_Perfil(CInt(lblCodUsuario.Text), RowPerfil.id_Perfil)

                If Not RowUsuarioPerfil Is Nothing Then
                    activoCheckBox = CType(RowItem.Cells(2).FindControl("chkPerfil"), CheckBox)
                    activoCheckBox.Checked = True
                End If
            Next
        End Sub

        Private Sub ShowRoles()
            Dim RowRol As DBSecurity.SchemaSecurity.TBL_RolRow
            Dim RowUsuarioRoles As DBSecurity.SchemaSecurity.TBL_Usuario_RolesRow
            Dim activoCheckBox As CheckBox

            gvRoles.DataSource = tblRoles
            gvRoles.DataBind()

            For Each RowItem As GridViewRow In gvRoles.Rows
                RowRol = CType(tblRoles.Rows(RowItem.DataItemIndex), DBSecurity.SchemaSecurity.TBL_RolRow)

                RowUsuarioRoles = tblUsuarioRoles.FindByfk_Usuariofk_Rol(CInt(lblCodUsuario.Text), RowRol.id_Rol)

                If Not RowUsuarioRoles Is Nothing Then
                    activoCheckBox = CType(RowItem.Cells(2).FindControl("chkRol"), CheckBox)
                    activoCheckBox.Checked = True
                End If
            Next
        End Sub

        Private Sub UpdatePerfiles()
            Dim RowPerfil As DBSecurity.SchemaSecurity.TBL_PerfilRow
            Dim RowUsuarioPerfil As DBSecurity.SchemaSecurity.TBL_Usuario_PerfilesRow
            Dim activoCheckBox As CheckBox

            tblUsuarioPerfiles.Rows.Clear()

            For Each RowItem As GridViewRow In gvPerfiles.Rows
                activoCheckBox = CType(RowItem.Cells(2).FindControl("chkPerfil"), CheckBox)

                If activoCheckBox.Checked Then
                    RowPerfil = CType(tblPerfiles.Rows(RowItem.DataItemIndex), DBSecurity.SchemaSecurity.TBL_PerfilRow)

                    RowUsuarioPerfil = tblUsuarioPerfiles.NewTBL_Usuario_PerfilesRow
                    RowUsuarioPerfil.fk_Usuario = CInt(lblCodUsuario.Text)
                    RowUsuarioPerfil.fk_Perfil = RowPerfil.id_Perfil
                    RowUsuarioPerfil.fk_Usuario_Log = MySesion.Usuario.id
                    RowUsuarioPerfil.Fecha_Log = Now

                    tblUsuarioPerfiles.Rows.Add(RowUsuarioPerfil)
                End If
            Next
        End Sub

        Private Sub UpdateRoles()
            Dim RowRol As DBSecurity.SchemaSecurity.TBL_RolRow
            Dim RowUsuarioRol As DBSecurity.SchemaSecurity.TBL_Usuario_RolesRow
            Dim activoCheckBox As CheckBox

            tblUsuarioRoles.Rows.Clear()

            For Each RowItem As GridViewRow In gvRoles.Rows
                activoCheckBox = CType(RowItem.Cells(2).FindControl("chkRol"), CheckBox)

                If activoCheckBox.Checked Then
                    RowRol = CType(tblRoles.Rows(RowItem.DataItemIndex), DBSecurity.SchemaSecurity.TBL_RolRow)

                    RowUsuarioRol = tblUsuarioRoles.NewTBL_Usuario_RolesRow

                    RowUsuarioRol.fk_Usuario = CInt(lblCodUsuario.Text)
                    RowUsuarioRol.fk_Rol = RowRol.id_Rol
                    RowUsuarioRol.fk_Usuario_Log = MySesion.Usuario.id
                    RowUsuarioRol.Fecha_Log = Now

                    tblUsuarioRoles.Rows.Add(RowUsuarioRol)
                End If
            Next
        End Sub

        Private Sub AsignarPassword()
            Dim Login = txtLogin.Text
            Dim nOldPassword = txtPassword1.Text
            Dim nNewPassword = txtPassword2.Text

            If nOldPassword <> nNewPassword Then
                Master.ShowAlert("Las contraseñas ingresadas no coinciden", MiharuMasterForm.MsgBoxIcon.IconWarning)
            Else
                Dim WebService As New SecurityWebService(Program.SecurityWebServiceURL, Me.MySesion.ClientIPAddress)

                Try
                    Dim nMsgError As String = String.Empty
                    WebService.CrearCanalSeguro()
                    WebService.setUser(Me.MySesion.Usuario.Login, Me.MySesion.Usuario.Password)
                    Dim Respuesta = WebService.ChangePassword(Login, nNewPassword, nMsgError)

                    Select Case Respuesta
                        Case EnumValidateUser.INVALIDO_PASSWORD
                            Master.ShowAlert("Contraseña no inválida", MiharuMasterForm.MsgBoxIcon.IconWarning)

                        Case EnumValidateUser.ERROR_PASSWORD
                            Master.ShowAlert("La contraseña no tiene la complejidad requerida por las políticas de seguridad del sistema", MiharuMasterForm.MsgBoxIcon.IconWarning)

                        Case EnumValidateUser.VALIDO
                            Master.ShowAlert("Contraseña se cambio exitosamente", MiharuMasterForm.MsgBoxIcon.IconInformation)

                    End Select
                Catch ex As Exception
                    Master.ShowAlert(ex.Message, MiharuMasterForm.MsgBoxIcon.IconError)

                    Return
                End Try
            End If
        End Sub

        Private Sub ShowEsquemaSeguridad()
            ddlEsquemaSeguridad.DataSource = tblEsquema
            ddlEsquemaSeguridad.DataValueField = "id_Esquema_Seguridad"
            ddlEsquemaSeguridad.DataTextField = "Nombre_Esquema_Seguridad"
            ddlEsquemaSeguridad.DataBind()
        End Sub

        Private Sub ShowDependencia()
            ddlDependencia.DataSource = tblDependencia
            ddlDependencia.DataValueField = "id_Dependencia"
            ddlDependencia.DataTextField = "Nombre_Dependencia"
            ddlDependencia.DataBind()
        End Sub

        Private Sub BuscarJefe()
            MySesion.Pagina.Parameter("JefeId") = CInt(lblCodJefe.Text)
            MySesion.Pagina.Parameter("JefeNombre") = txtJefe.Text
            MySesion.Pagina.Parameter("EntidadId") = CShort(ddlEntidad.SelectedValue)

            Master.ShowDialog("administracion/seguridad/p_usuariojefe.aspx", "UsuarioJefe", "Usuario jefe", "730", "460", "100", "100", False)
        End Sub

        Private Sub EliminarJefe()
            lblCodJefe.Text = "-1"
            txtJefe.Text = ""

            ibDeleteJefe.Visible = False
        End Sub

#End Region

#Region " Funciones "

        Private Function Validar(ByRef nDBMSecurity As DBSecurity.DBSecurityDataBaseManager) As Boolean
            If Usuario_Validar_Login(nDBMSecurity, txtLogin.Text, CInt(lblCodUsuario.Text)) Then
                Return True
            Else
                Master.ShowAlert("Ya existe un usuario con el Login: " & txtLogin.Text, MiharuMasterForm.MsgBoxIcon.IconInformation)

                Return False
            End If
        End Function

        Public Function Usuario_Validar_Login(ByRef nDBMSecurity As DBSecurity.DBSecurityDataBaseManager, ByVal nLogin As String, ByVal nUsuario As Integer) As Boolean
            Dim UsuarioDatatTable = nDBMSecurity.SchemaSecurity.TBL_Usuario.DBFindByLogin_Usuario(nLogin)

            If UsuarioDatatTable.Count = 0 Then
                Return True
            Else
                Return (UsuarioDatatTable(0).id_Usuario = nUsuario)
            End If
        End Function

#End Region

    End Class

End Namespace