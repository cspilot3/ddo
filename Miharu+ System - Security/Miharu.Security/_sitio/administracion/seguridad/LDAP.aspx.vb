Imports Miharu.Security._clases
Imports SLYG.Tools

Namespace _sitio.administracion.seguridad

    Partial Public Class LDAP
        Inherits FormBase

#Region " Declaraciones "

        Private Const MyPathPermiso As String = "1.2.8"

        Private BaseDataTable As DBSecurity.SchemaSecurity.CTA_LDAPDataTable

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

        Private Sub EntidadDropDownList_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles EntidadDropDownList.SelectedIndexChanged
            Me.NombreEntidadLabel.Text = Me.EntidadDropDownList.SelectedItem.Text
            Buscar(Me.ucFiltro.Parametro)
        End Sub

        Private Sub BaseGridView_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles BaseGridView.SelectedIndexChanged
            If (Me.BaseGridView.SelectedIndex >= 0 And Me.BaseGridView.Rows.Count > 0) Then
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
            Me.BaseDataTable = New DBSecurity.SchemaSecurity.CTA_LDAPDataTable
            Me.MySesion.Pagina.Parameter("BaseDataTable") = Me.BaseDataTable

            ' Load Entidades
            Dim dbmSecurity As DBSecurity.DBSecurityDataBaseManager = Nothing

            Try
                dbmSecurity = New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)

                dbmSecurity.Connection_Open(MySesion.Usuario.id)
                Dim EntidadDatatable = dbmSecurity.SchemaConfig.TBL_Entidad.DBGet(Nothing, 0, New DBSecurity.SchemaConfig.TBL_EntidadEnumList(DBSecurity.SchemaConfig.TBL_EntidadEnum.Nombre_Entidad, True))
                Dim PerfilDatatable = dbmSecurity.SchemaSecurity.TBL_Perfil.DBGet(Nothing, 0, New DBSecurity.SchemaSecurity.TBL_PerfilEnumList(DBSecurity.SchemaSecurity.TBL_PerfilEnum.Nombre_Perfil, True))

                ShowEntidades(EntidadDatatable)
                ShowPerfiles(PerfilDatatable)
            Catch ex As Exception
                Master.ShowAlert(ex.Message, MiharuMasterForm.MsgBoxIcon.IconError)
            Finally
                If (dbmSecurity IsNot Nothing) Then dbmSecurity.Connection_Close()
            End Try
        End Sub

        Private Sub Load_Data()
            Me.BaseDataTable = CType(Me.MySesion.Pagina.Parameter("BaseDataTable"), DBSecurity.SchemaSecurity.CTA_LDAPDataTable)
        End Sub

        Private Sub Buscar(ByVal nParametro As String)
            Me.BaseGridView.SelectedIndex = -1

            If nParametro <> "" And Me.EntidadDropDownList.SelectedIndex >= 0 Then
                Dim dbmSecurity As DBSecurity.DBSecurityDataBaseManager = Nothing

                Try
                    dbmSecurity = New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)

                    dbmSecurity.Connection_Open(MySesion.Usuario.id)
                    BaseDataTable.Clear()
                    dbmSecurity.SchemaSecurity.CTA_LDAP.DBFillByfk_EntidadGrupo_LDAP(BaseDataTable, CShort(Me.EntidadDropDownList.SelectedValue), nParametro)
                Catch ex As Exception
                    Master.ShowAlert(ex.Message, MiharuMasterForm.MsgBoxIcon.IconError)
                Finally
                    If (dbmSecurity IsNot Nothing) Then dbmSecurity.Connection_Close()
                End Try
            Else
                BaseDataTable.Rows.Clear()
            End If

            ActivarOpciones(False, False)
            Visualizar_Resultados()
            DetallePanel.Visible = False
        End Sub

        Private Sub Visualizar_Resultados()
            Me.BaseGridView.DataSource = Me.BaseDataTable
            Me.BaseGridView.DataBind()
        End Sub

        Private Sub ClearForm()
            Me.NombreEntidadLabel.Text = Me.EntidadDropDownList.SelectedItem.Text
            Me.CodLDAPLabel.Text = "-1"
            Me.GrupoTextBox.Text = ""
            Me.PerfilDropDownList.SelectedIndex = 0
        End Sub

        Private Sub NuevoRegistro()
            ClearForm()
            Me.BaseTabContainer.ActiveTabIndex = 1

            Me.DetallePanel.Visible = True
            Me.NombreEntidadLabel.Focus()
            Me.ActivarOpciones(True, True)
        End Sub

        Private Sub EditarRegistro()
            ClearForm()
            Me.BaseTabContainer.ActiveTabIndex = 1

            Me.DetallePanel.Visible = True

            Me.GrupoTextBox.Focus()
            Me.ActivarOpciones(True, False)

            Dim RowBase = CType(BaseDataTable.Rows(Me.BaseGridView.SelectedRow.DataItemIndex), DBSecurity.SchemaSecurity.CTA_LDAPRow)

            ' Data
            Me.CodLDAPLabel.Text = CStr(RowBase.id_LDAP)
            Me.GrupoTextBox.Text = RowBase.Grupo_LDAP

            Me.PerfilDropDownList.SelectedValue = CStr(RowBase.fk_Perfil)

            'For i As Integer = 0 To Me.PerfilDropDownList.Items.Count - 1
            '    If (Me.PerfilDropDownList.Items(i).Value = RowBase.fk_Perfil.ToString()) Then
            '        Me.PerfilDropDownList.Items(i).Selected = True
            '        Exit For
            '    End If
            'Next
        End Sub

        Private Sub GuardarCambios()
            If (Validar()) Then
                Dim dbmSecurity As DBSecurity.DBSecurityDataBaseManager = Nothing
                Dim isNuevo As Boolean = False

                Try
                    dbmSecurity = New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)

                    dbmSecurity.Connection_Open(MySesion.Usuario.id)
                    dbmSecurity.Transaction_Begin()
                    'dbmSecurity.DataBase.Identifier_Date_Format = Program.IdentifierDateFormat

                    Dim LDAPType = New DBSecurity.SchemaSecurity.TBL_LDAPType()

                    If (CodLDAPLabel.Text = "-1") Then
                        isNuevo = True
                        CodLDAPLabel.Text = CStr(dbmSecurity.SchemaSecurity.TBL_LDAP.DBNextId(CShort(Me.EntidadDropDownList.SelectedValue)))
                    End If

                    LDAPType.fk_Entidad = CShort(Me.EntidadDropDownList.SelectedValue)
                    LDAPType.id_LDAP = CShort(CodLDAPLabel.Text)
                    LDAPType.Grupo_LDAP = Me.GrupoTextBox.Text
                    LDAPType.fk_Perfil = CShort(Me.PerfilDropDownList.SelectedValue)

                    LDAPType.fk_Usuario_Log = MySesion.Usuario.id
                    LDAPType.Fecha_Log = SlygNullable.SysDate

                    If (isNuevo) Then
                        dbmSecurity.SchemaSecurity.TBL_LDAP.DBInsert(LDAPType)
                    Else
                        dbmSecurity.SchemaSecurity.TBL_LDAP.DBUpdate(LDAPType, LDAPType.fk_Entidad, LDAPType.id_LDAP)
                    End If

                    dbmSecurity.Transaction_Commit()

                    Me.Master.ShowAlert("Los datos se almacenaron correctamente", MiharuMasterForm.MsgBoxIcon.IconInformation)

                    Buscar(ucFiltro.Parametro)

                    ActivarOpciones(True, False)
                    Me.DetallePanel.Visible = True
                    Me.BaseTabContainer.ActiveTabIndex = 1

                Catch ex As Exception
                    If (dbmSecurity IsNot Nothing) Then dbmSecurity.Transaction_Rollback()
                    Master.ShowAlert(ex.Message, MiharuMasterForm.MsgBoxIcon.IconError)

                    If (isNuevo) Then
                        CodLDAPLabel.Text = "-1"
                    End If
                Finally
                    If (dbmSecurity IsNot Nothing) Then dbmSecurity.Connection_Close()
                End Try
            End If
        End Sub

        Private Sub EliminarRegistro()
            Dim dbmSecurity As DBSecurity.DBSecurityDataBaseManager = Nothing

            Try
                dbmSecurity = New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)

                dbmSecurity.Connection_Open(MySesion.Usuario.id)
                dbmSecurity.SchemaSecurity.TBL_LDAP.DBDelete(CShort(Me.EntidadDropDownList.SelectedValue), CShort(Me.CodLDAPLabel.Text))

                Me.BaseTabContainer.ActiveTabIndex = 0
                Buscar(ucFiltro.Parametro)

                Me.Master.ShowAlert("El registro se eliminó exitosamente", MiharuMasterForm.MsgBoxIcon.IconInformation)

                ClearForm()
                Me.BaseTabContainer.ActiveTabIndex = 0

                Me.DetallePanel.Visible = False
                Me.ActivarOpciones(False, False)

            Catch ex As Exception
                Master.ShowAlert(ex.Message, MiharuMasterForm.MsgBoxIcon.IconError)
            Finally
                If (dbmSecurity IsNot Nothing) Then dbmSecurity.Connection_Close()
            End Try
        End Sub

        Private Sub ActivarOpciones(ByVal nActivo As Boolean, ByVal nIsNew As Boolean)
            ibSave.Visible = nActivo
            divSave.Style("display") = CStr(IIf(ibSave.Visible, "inline", "none"))

            ibDelete.Visible = nActivo And Not nIsNew
            divDelete.Style("display") = CStr(IIf(ibDelete.Visible, "inline", "none"))
        End Sub

        Private Sub ShowEntidades(ByVal nEntidadDatatable As DBSecurity.SchemaConfig.TBL_EntidadDataTable)
            Me.EntidadDropDownList.DataSource = nEntidadDatatable
            Me.EntidadDropDownList.DataValueField = "id_Entidad"
            Me.EntidadDropDownList.DataTextField = "Nombre_Entidad"
            Me.EntidadDropDownList.DataBind()

            If Not MySesion.Usuario.PerfilManager.PuedeAcceder("1.1.2") Then ' Entidades
                Me.EntidadDropDownList.SelectedValue = CStr(MySesion.Entidad.id)
                Me.EntidadDropDownList.Enabled = False
            End If
        End Sub

        Private Sub ShowPerfiles(ByVal nPerfilDatatable As DBSecurity.SchemaSecurity.TBL_PerfilDataTable)
            Me.PerfilDropDownList.Items.Clear()
            Me.PerfilDropDownList.Items.Add(New ListItem("- Seleccionar un perfil -", "-1"))

            For Each PerfilRow In nPerfilDatatable
                Me.PerfilDropDownList.Items.Add(New ListItem(PerfilRow.Nombre_Perfil, PerfilRow.id_Perfil.ToString()))
            Next
        End Sub

#End Region

#Region " Funciones "

        Private Function Validar() As Boolean
            If (Me.PerfilDropDownList.SelectedIndex = 0) Then
                Me.Master.ShowAlert("Debe seleccionar el Perfil", MiharuMasterForm.MsgBoxIcon.IconError)
                Me.PerfilDropDownList.Focus()
            Else
                Dim dbmSecurity As DBSecurity.DBSecurityDataBaseManager = Nothing

                Try
                    dbmSecurity = New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)

                    dbmSecurity.Connection_Open(MySesion.Usuario.id)

                    Dim Entidad = CShort(Me.EntidadDropDownList.SelectedValue)
                    Dim Perfil = CShort(Me.PerfilDropDownList.SelectedValue)
                    Dim Grupo = Me.GrupoTextBox.Text

                    Dim LDAPPerfilDataTable = dbmSecurity.SchemaSecurity.TBL_LDAP.DBFindByfk_Entidadfk_Perfil(Entidad, Perfil)
                    Dim LDAPGrupoDataTable = dbmSecurity.SchemaSecurity.TBL_LDAP.DBFindByfk_EntidadGrupo_LDAP(Entidad, Grupo)

                    ' Validar que el perfil no se repita
                    If (LDAPPerfilDataTable.Count > 0) Then
                        If (CodLDAPLabel.Text = "-1" OrElse LDAPPerfilDataTable(0).id_LDAP.ToString() <> CodLDAPLabel.Text) Then
                            Me.Master.ShowAlert("No se pueden crear dos registros con el mismo Perfil", MiharuMasterForm.MsgBoxIcon.IconError)
                            Return False
                        End If
                    End If

                    If (LDAPGrupoDataTable.Count > 0) Then
                        If (CodLDAPLabel.Text = "-1" OrElse LDAPGrupoDataTable(0).id_LDAP.ToString() <> CodLDAPLabel.Text) Then
                            Me.Master.ShowAlert("No se pueden crear dos registros con el mismo Grupo", MiharuMasterForm.MsgBoxIcon.IconError)
                            Return False
                        End If
                    End If

                    Return True
                Catch ex As Exception
                    Master.ShowAlert(ex.Message, MiharuMasterForm.MsgBoxIcon.IconError)

                Finally
                    If (dbmSecurity IsNot Nothing) Then dbmSecurity.Connection_Close()
                End Try
            End If

            Return False
        End Function

#End Region

    End Class

End Namespace