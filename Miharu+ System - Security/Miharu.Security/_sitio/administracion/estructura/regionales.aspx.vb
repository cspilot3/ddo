Imports Miharu.Security._clases

Namespace _sitio.administracion.estructura

    Partial Public Class regionales
        Inherits FormBase

#Region " Declaraciones "

        Private Const MyPathPermiso As String = "1.1.4"

        Private tblBase As DBSecurity.SchemaConfig.TBL_RegionalDataTable
        Private tblEntidad As DBSecurity.SchemaConfig.TBL_EntidadDataTable
        Private tblRgional As DBSecurity.SchemaConfig.TBL_RegionalDataTable


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
            tblBase = New DBSecurity.SchemaConfig.TBL_RegionalDataTable
            Me.MySesion.Pagina.Parameter("tblBase") = tblBase

            tblEntidad = New DBSecurity.SchemaConfig.TBL_EntidadDataTable
            Me.MySesion.Pagina.Parameter("tblEntidad") = tblEntidad

            tblRgional = New DBSecurity.SchemaConfig.TBL_RegionalDataTable
            Me.MySesion.Pagina.Parameter("tblRgional") = tblRgional


            ' Load Entidades
            Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)

            Try
                dbmSecurity.Connection_Open(MySesion.Usuario.id)   'id del usuario
                tblEntidad = dbmSecurity.SchemaConfig.TBL_Entidad.DBFindByNombre_Entidad(Nothing)

            Catch ex As Exception
                Master.ShowAlert(ex.Message, MiharuMasterForm.MsgBoxIcon.IconError)
            Finally
                dbmSecurity.Connection_Close()
            End Try

            ShowEntidades()


        End Sub

        Private Sub Load_Data()
            tblBase = CType(Me.MySesion.Pagina.Parameter("tblBase"), DBSecurity.SchemaConfig.TBL_RegionalDataTable)
            tblEntidad = CType(Me.MySesion.Pagina.Parameter("tblEntidad"), DBSecurity.SchemaConfig.TBL_EntidadDataTable)
            tblRgional = CType(Me.MySesion.Pagina.Parameter("tblRgional"), DBSecurity.SchemaConfig.TBL_RegionalDataTable)
        End Sub

        Private Sub Buscar(ByVal nParametro As String)
            gvBase.SelectedIndex = -1

            tblBase.Rows.Clear()

            If nParametro <> "" And ddlEntidad.SelectedIndex >= 0 Then
                Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)

                Try
                    dbmSecurity.Connection_Open(MySesion.Usuario.id)
                    dbmSecurity.SchemaConfig.TBL_Regional.DBFillByfk_EntidadNombre_Regional(tblBase, CShort(ddlEntidad.SelectedValue), nParametro)
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

            tblRgional.Rows.Clear()

            lblNombreEntidad.Text = ddlEntidad.SelectedItem.Text

            lblCodRegional.Text = "-1"
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

            Dim RowBase As DBSecurity.SchemaConfig.TBL_RegionalRow

            ClearForm()
            tcBase.ActiveTabIndex = 1

            pnlDetalle.Visible = True


            txtNombre.Focus()
            Me.ActivarOpciones(True, False)

            'RowBase = CType(tblBase.Rows(gvBase.SelectedRow.DataItemIndex), DBSecurity.SchemaConfig.TBL_RgionalRow)
            RowBase = CType(tblBase.Rows(gvBase.SelectedRow.DataItemIndex), DBSecurity.SchemaConfig.TBL_RegionalRow)
            ' Data
            lblCodRegional.Text = CStr(RowBase.id_Regional)
            txtNombre.Text = RowBase.Nombre_Regional
            txtCodigo.Text = CStr(RowBase.id_Regional)

            tblRgional.Rows.Clear()
            tblRgional.Rows.Add(RowBase.ItemArray)
            tblRgional.AcceptChanges()


        End Sub

        Private Sub GuardarCambios()
            If Validar() Then
                Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)
                Dim RowRegional As DBSecurity.SchemaConfig.TBL_RegionalRow
                Dim isNuevo As Boolean = False

                Try
                    dbmSecurity.Connection_Open(MySesion.Usuario.id)
                    dbmSecurity.Transaction_Begin()

                    'dbmSecurity.DataBase.Identifier_Date_Format = Program.IdentifierDateFormat

                    If lblCodRegional.Text = "-1" Then
                        isNuevo = True
                        'captura el siguiente id_regional
                        lblCodRegional.Text = CStr(dbmSecurity.SchemaConfig.TBL_Regional.DBNextId(CShort(ddlEntidad.SelectedValue)))

                        RowRegional = tblRgional.NewTBL_RegionalRow
                        RowRegional.fk_Entidad = CShort(ddlEntidad.SelectedValue)
                        RowRegional.id_Regional = CShort(lblCodRegional.Text)
                    Else
                        RowRegional = tblRgional.FindByid_Regionalfk_Entidad(CShort(lblCodRegional.Text), CShort(ddlEntidad.SelectedValue))
                    End If

                    RowRegional.Nombre_Regional = txtNombre.Text        'nombre de la regional
                    RowRegional.fk_Usuario_Log = MySesion.Usuario.id    'id del usuario 
                    RowRegional.Eliminado = False
                    RowRegional.Fecha_log = Now

                    If isNuevo Then
                        tblRgional.Rows.Add(RowRegional)
                    End If

                    dbmSecurity.SchemaConfig.TBL_Regional.DBSaveTable(tblRgional)

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
                        lblCodRegional.Text = "-1"
                        tblRgional.RejectChanges()
                    End If
                Finally
                    dbmSecurity.Connection_Close()
                End Try
            End If
        End Sub

        Private Sub EliminarRegistro()
            Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)
            Try
                dbmSecurity.SchemaConfig.TBL_Regional.DBDelete(CShort(ddlEntidad.SelectedValue), CShort(lblCodRegional.Text))
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
            Return True
        End Function

#End Region

    End Class

End Namespace