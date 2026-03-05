Imports Miharu.Security._clases

Namespace _sitio.administracion.estructura

    Partial Public Class ciudades
        Inherits FormBase

#Region " Declaraciones "

        Private Const MyPathPermiso As String = "1.1.7"
        Private tblBase As DBSecurity.SchemaConfig.TBL_CiudadDataTable
        Private tblPais As DBSecurity.SchemaConfig.TBL_PaisDataTable
        Private tblRegion As DBSecurity.SchemaConfig.TBL_RegionDataTable
        Private tblCiudad As DBSecurity.SchemaConfig.TBL_CiudadDataTable

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

        Private Sub ddlPais_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlPais.SelectedIndexChanged
            lblNombrePais.Text = ddlPais.SelectedItem.Text
            Load_Region(CShort(ddlPais.SelectedItem.Value))
            Buscar(ucFiltro.Parametro)
        End Sub

        Private Sub ddlRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlRegion.SelectedIndexChanged
            lblNombreRegion.Text = ddlRegion.SelectedItem.Text
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
            tblBase = New DBSecurity.SchemaConfig.TBL_CiudadDataTable
            Me.MySesion.Pagina.Parameter("tblBase") = tblBase

            tblCiudad = New DBSecurity.SchemaConfig.TBL_CiudadDataTable
            Me.MySesion.Pagina.Parameter("tblCiudad") = tblCiudad

            tblPais = New DBSecurity.SchemaConfig.TBL_PaisDataTable
            Me.MySesion.Pagina.Parameter("tblPais") = tblPais

            tblRegion = New DBSecurity.SchemaConfig.TBL_RegionDataTable
            Me.MySesion.Pagina.Parameter("tblRegion") = tblRegion

            ' Load Paises
            Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)

            Try
                dbmSecurity.Connection_Open(MySesion.Usuario.id)   'id del usuario
                dbmSecurity.SchemaConfig.TBL_Pais.DBFill(tblPais, Nothing)

            Catch ex As Exception

                Master.ShowAlert(ex.Message, MiharuMasterForm.MsgBoxIcon.IconError)
            Finally
                dbmSecurity.Connection_Close()
            End Try

            ShowPaises()

            'Load Regiones
            Load_Region(CShort(ddlPais.SelectedItem.Value))
            lblNombrePais.Text = ddlPais.SelectedItem.Text
            lblNombreRegion.Text = ddlRegion.SelectedItem.Text
        End Sub

        Private Sub Load_Data()
            tblBase = CType(Me.MySesion.Pagina.Parameter("tblBase"), DBSecurity.SchemaConfig.TBL_CiudadDataTable)
            tblPais = CType(Me.MySesion.Pagina.Parameter("tblPais"), DBSecurity.SchemaConfig.TBL_PaisDataTable)
            tblCiudad = CType(Me.MySesion.Pagina.Parameter("tblCiudad"), DBSecurity.SchemaConfig.TBL_CiudadDataTable)
            tblRegion = CType(Me.MySesion.Pagina.Parameter("tblRegion"), DBSecurity.SchemaConfig.TBL_RegionDataTable)

        End Sub

        Private Sub Buscar(ByVal nParametro As String)
            gvBase.SelectedIndex = -1

            If nParametro <> "" And ddlPais.SelectedIndex >= 0 And ddlRegion.SelectedIndex >= 0 Then
                Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)

                Try
                    dbmSecurity.Connection_Open(MySesion.Usuario.id)
                    tblBase.Clear()
                    dbmSecurity.SchemaConfig.TBL_Ciudad.DBFillByfk_Paisfk_RegionNombre_Ciudad(tblBase, CShort(ddlPais.SelectedValue), CShort(ddlRegion.SelectedValue), nParametro)
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

            tblCiudad.Rows.Clear()

            lblCodCiudad.Text = "-1"
            txtNombre.Text = ""
            txtCodigoDANE.Text = ""
            chkPredeterminado.Checked = False
        End Sub

        Private Sub NuevoRegistro()

            If ddlPais.Text <> "" And ddlRegion.Text <> "" Then
                ClearForm()

                tcBase.ActiveTabIndex = 1

                pnlDetalle.Visible = True
                lblNombreRegion.Text = ddlRegion.SelectedItem.Text
                txtNombre.Focus()
                Me.ActivarOpciones(True, True)
            End If
        End Sub

        Private Sub EditarRegistro()
            Dim RowBase As DBSecurity.SchemaConfig.TBL_CiudadRow

            ClearForm()
            tcBase.ActiveTabIndex = 1

            pnlDetalle.Visible = True

            txtNombre.Focus()
            Me.ActivarOpciones(True, False)

            RowBase = CType(tblBase.Rows(gvBase.SelectedRow.DataItemIndex), DBSecurity.SchemaConfig.TBL_CiudadRow)

            ' Data
            lblCodCiudad.Text = CStr(RowBase.id_Ciudad)
            txtNombre.Text = RowBase.Nombre_Ciudad
            txtCodigoDANE.Text = RowBase.Codigo_DANE_Ciudad
            chkPredeterminado.Checked = RowBase.Predeterminado

            tblCiudad.Rows.Clear()
            tblCiudad.Rows.Add(RowBase.ItemArray)
            tblCiudad.AcceptChanges()
        End Sub

        Private Sub GuardarCambios()
            If Validar() Then
                Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)
                Dim RowCiudad As DBSecurity.SchemaConfig.TBL_CiudadRow
                Dim isNuevo As Boolean = False

                Try
                    dbmSecurity.Connection_Open(MySesion.Usuario.id)
                    dbmSecurity.Transaction_Begin()

                    ''dbmSecurity.DataBase.Identifier_Date_Format = Program.IdentifierDateFormat

                    If lblCodCiudad.Text = "-1" Then
                        isNuevo = True
                        lblCodCiudad.Text = CStr(dbmSecurity.SchemaConfig.TBL_Ciudad.DBNextId(CShort(ddlPais.SelectedValue), CShort(ddlRegion.SelectedValue)))

                        RowCiudad = tblCiudad.NewTBL_CiudadRow
                        RowCiudad.id_Ciudad = CShort(lblCodCiudad.Text)
                    Else
                        RowCiudad = dbmSecurity.SchemaConfig.TBL_Ciudad.DBFindByid_Ciudad(CShort(lblCodCiudad.Text))(0)
                    End If

                    Predeterminado()
                    RowCiudad.Nombre_Ciudad = txtNombre.Text
                    RowCiudad.Codigo_DANE_Ciudad = txtCodigoDANE.Text
                    RowCiudad.Predeterminado = chkPredeterminado.Checked
                    RowCiudad.fk_Usuario_Log = MySesion.Usuario.id
                    RowCiudad.fk_Pais = CShort(ddlPais.SelectedValue)
                    RowCiudad.fk_Region = CShort(ddlRegion.SelectedValue)
                    RowCiudad.Eliminado = False
                    RowCiudad.Fecha_log = Now


                    If isNuevo Then
                        tblCiudad.Rows.Add(RowCiudad)
                    End If

                    dbmSecurity.SchemaConfig.TBL_Ciudad.DBSaveTable(tblCiudad)

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
                        lblCodCiudad.Text = "-1"
                        tblCiudad.RejectChanges()
                    End If
                Finally
                    dbmSecurity.Connection_Close()
                End Try
            End If
        End Sub

        Private Sub EliminarRegistro()
            'El registro pasa a un estado Eliminado =1
            Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)
            Try
                dbmSecurity.Connection_Open(MySesion.Usuario.id)
                dbmSecurity.SchemaConfig.TBL_Ciudad.DBDelete(CShort(ddlPais.SelectedValue), CShort(ddlRegion.SelectedValue), CShort(lblCodCiudad.Text))
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

        Private Sub ShowPaises()
            ddlPais.DataSource = tblPais
            ddlPais.DataValueField = "id_Pais"
            ddlPais.DataTextField = "Nombre_Pais"
            ddlPais.DataBind()

            If Not MySesion.Usuario.PerfilManager.PuedeAcceder("1.1.5") Then ' Paises
                ddlPais.SelectedValue = CStr(MySesion.Entidad.id)
                ddlPais.Enabled = False
            End If

        End Sub

        Private Sub Load_Region(ByVal id_Pais As Short)

            ' Load Regiones
            Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)

            Try
                dbmSecurity.Connection_Open(MySesion.Usuario.id)
                tblRegion.Clear()
                dbmSecurity.SchemaConfig.TBL_Region.DBFill(tblRegion, id_Pais, Nothing)

            Catch ex As Exception

                Master.ShowAlert(ex.Message, MiharuMasterForm.MsgBoxIcon.IconError)
            Finally
                dbmSecurity.Connection_Close()
            End Try

            ShowRegiones()

        End Sub

        Private Sub ShowRegiones()
            ddlRegion.DataSource = tblRegion
            ddlRegion.DataValueField = "id_Region"
            ddlRegion.DataTextField = "Nombre_Region"
            ddlRegion.DataBind()

            If Not MySesion.Usuario.PerfilManager.PuedeAcceder("1.1.6") Then 'Regiones
                ddlRegion.SelectedValue = CStr(MySesion.Entidad.id)
                ddlRegion.Enabled = False
            End If

        End Sub

        Private Sub Predeterminado()
            '---Valida una cuidad como la prederteminada
            If chkPredeterminado.Checked Then
                'cambia de estado a la ciudad que actualmente esta como predeterminada
                Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)
                Try
                    dbmSecurity.Connection_Open(MySesion.Usuario.id)
                    dbmSecurity.SchemaConfig.PA_Ciudad_predeterminado.DBExecute(CShort(ddlRegion.SelectedValue), CShort(ddlPais.SelectedValue))

                Catch ex As Exception
                    Master.ShowAlert(ex.Message, MiharuMasterForm.MsgBoxIcon.IconError)
                Finally
                    dbmSecurity.Connection_Close()
                End Try
            End If
        End Sub
#End Region

#Region " Funciones "

        Private Function Validar() As Boolean
            Return True
        End Function

#End Region

    End Class
End Namespace