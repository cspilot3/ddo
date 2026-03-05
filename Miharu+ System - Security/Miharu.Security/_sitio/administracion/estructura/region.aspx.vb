Imports Miharu.Security._clases

Namespace _sitio.administracion.estructura

    Partial Public Class region
        Inherits FormBase

#Region " Declaraciones "

        Private Const MyPathPermiso As String = "1.1.6"

        Private tblBase As DBSecurity.SchemaConfig.TBL_RegionDataTable
        Private tblPais As DBSecurity.SchemaConfig.TBL_PaisDataTable
        Private tblRegion As DBSecurity.SchemaConfig.TBL_RegionDataTable


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
            tblBase = New DBSecurity.SchemaConfig.TBL_RegionDataTable
            Me.MySesion.Pagina.Parameter("tblBase") = tblBase

            tblPais = New DBSecurity.SchemaConfig.TBL_PaisDataTable
            Me.MySesion.Pagina.Parameter("tblPais") = tblPais

            tblRegion = New DBSecurity.SchemaConfig.TBL_RegionDataTable
            Me.MySesion.Pagina.Parameter("tblRegion") = tblRegion


            ' Load Paises
            Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)

            Try
                dbmSecurity.Connection_Open(MySesion.Usuario.id)   'id del usuario

                tblPais = dbmSecurity.SchemaConfig.TBL_Pais.DBFindByid_Pais(Nothing)

            Catch ex As Exception
                Master.ShowAlert(ex.Message, MiharuMasterForm.MsgBoxIcon.IconError)
            Finally
                dbmSecurity.Connection_Close()
            End Try

            ShowPaises()


        End Sub

        Private Sub Load_Data()
            tblBase = CType(Me.MySesion.Pagina.Parameter("tblBase"), DBSecurity.SchemaConfig.TBL_RegionDataTable)
            tblPais = CType(Me.MySesion.Pagina.Parameter("tblPais"), DBSecurity.SchemaConfig.TBL_PaisDataTable)
            tblRegion = CType(Me.MySesion.Pagina.Parameter("tblRegion"), DBSecurity.SchemaConfig.TBL_RegionDataTable)
        End Sub

        Private Sub Buscar(ByVal nParametro As String)
            gvBase.SelectedIndex = -1

            If nParametro <> "" And ddlPais.SelectedIndex >= 0 Then
                Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)

                Try
                    dbmSecurity.Connection_Open(MySesion.Usuario.id)
                    tblBase.Clear()
                    dbmSecurity.SchemaConfig.TBL_Region.DBFillByfk_PaisNombre_Region(tblBase, CShort(ddlPais.SelectedValue), nParametro)
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
            tblRegion.Rows.Clear()

            lblNombrePais.Text = ddlPais.SelectedItem.Text

            lblCodRegion.Text = "-1"
            txtNombre.Text = ""
            txtCodigo.Text = ""
        End Sub

        Private Sub NuevoRegistro()
            ClearForm()
            tcBase.ActiveTabIndex = 1

            pnlDetalle.Visible = True

            txtNombre.Focus()
            Me.ActivarOpciones(True, True)
        End Sub

        Private Sub EditarRegistro()

            Dim RowBase As DBSecurity.SchemaConfig.TBL_RegionRow

            ClearForm()
            tcBase.ActiveTabIndex = 1

            pnlDetalle.Visible = True


            txtNombre.Focus()
            Me.ActivarOpciones(True, False)

            RowBase = CType(tblBase.Rows(gvBase.SelectedRow.DataItemIndex), DBSecurity.SchemaConfig.TBL_RegionRow)
            ' Data
            lblCodRegion.Text = CStr(RowBase.id_Region)
            txtNombre.Text = RowBase.Nombre_Region
            txtCodigo.Text = CStr(RowBase.id_Region)

            tblRegion.Rows.Clear()
            tblRegion.Rows.Add(RowBase.ItemArray)
            tblRegion.AcceptChanges()


        End Sub

        Private Sub GuardarCambios()
            If Validar() Then
                Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)
                Dim RowRegion As DBSecurity.SchemaConfig.TBL_RegionRow
                Dim isNuevo As Boolean = False

                Try
                    dbmSecurity.Connection_Open(MySesion.Usuario.id)
                    dbmSecurity.Transaction_Begin()

                    'dbmSecurity.DataBase.Identifier_Date_Format = Program.IdentifierDateFormat

                    If lblCodRegion.Text = "-1" Then
                        isNuevo = True
                        'captura el siguiente id_regional
                        lblCodRegion.Text = CStr(dbmSecurity.SchemaConfig.TBL_Region.DBNextId(CShort(ddlPais.SelectedValue)))

                        RowRegion = tblRegion.NewTBL_RegionRow
                        RowRegion.fk_Pais = CShort(ddlPais.SelectedValue)
                        RowRegion.id_Region = CShort(lblCodRegion.Text)
                    Else
                        RowRegion = tblRegion.FindByid_Regionfk_Pais(CShort(ddlPais.SelectedValue), CShort(lblCodRegion.Text))
                    End If

                    RowRegion.Nombre_Region = txtNombre.Text          'nombre de la regional
                    RowRegion.fk_Usuario_Log = MySesion.Usuario.id    'id del usuario 
                    RowRegion.Eliminado = False
                    RowRegion.Fecha_log = Now

                    If isNuevo Then
                        tblRegion.Rows.Add(RowRegion)
                    End If

                    dbmSecurity.SchemaConfig.TBL_Region.DBSaveTable(tblRegion)

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
                        lblCodRegion.Text = "-1"
                        tblRegion.RejectChanges()
                    End If
                Finally
                    dbmSecurity.Connection_Close()
                End Try
            End If
        End Sub

        Private Sub EliminarRegistro()
            Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)
            Try
                dbmSecurity.SchemaConfig.TBL_Region.DBDelete(CShort(ddlPais.SelectedValue), CShort(lblCodRegion.Text))
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

#End Region

#Region " Funciones "

        Private Function Validar() As Boolean
            Return True
        End Function

#End Region

    End Class

End Namespace