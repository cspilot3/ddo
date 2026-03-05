Imports Miharu.Security._clases

Namespace _sitio.administracion.seguridad

    Partial Public Class modulos
        Inherits FormBase

#Region " Declaraciones "

        Private Const MyPathPermiso As String = "1.2.1"

        Private tblBase As DBSecurity.SchemaSecurity.TBL_ModuloDataTable
        Private tblModulo As DBSecurity.SchemaSecurity.TBL_ModuloDataTable

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
            tblBase = New DBSecurity.SchemaSecurity.TBL_ModuloDataTable
            Me.MySesion.Pagina.Parameter("tblBase") = tblBase

            tblModulo = New DBSecurity.SchemaSecurity.TBL_ModuloDataTable
            Me.MySesion.Pagina.Parameter("tblModulo") = tblModulo
        End Sub

        Private Sub Load_Data()
            tblBase = CType(Me.MySesion.Pagina.Parameter("tblBase"), DBSecurity.SchemaSecurity.TBL_ModuloDataTable)
            tblModulo = CType(Me.MySesion.Pagina.Parameter("tblModulo"), DBSecurity.SchemaSecurity.TBL_ModuloDataTable)
        End Sub

        Private Sub Buscar(ByVal nParametro As String)
            gvBase.SelectedIndex = -1

            If nParametro <> "" Then
                Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)

                Try
                    dbmSecurity.Connection_Open(MySesion.Usuario.id)
                    tblBase.Clear()
                    dbmSecurity.SchemaSecurity.TBL_Modulo.DBFillByEnsamblado_Modulo(tblBase, nParametro)
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
            tblModulo.Rows.Clear()

            lblCodModulo.Text = "-1"
            txtNombre.Text = ""
            chkPermisosHeredados.Checked = True
            txtEnsamblado.Text = ""
            CadenaTextBox.Text = ""
        End Sub

        Private Sub NuevoRegistro()
            ClearForm()

            tcBase.ActiveTabIndex = 1

            pnlDetalle.Visible = True

            txtNombre.Focus()
            Me.ActivarOpciones(True, True)
        End Sub

        Private Sub EditarRegistro()
            Dim RowBase As DBSecurity.SchemaSecurity.TBL_ModuloRow

            ClearForm()
            tcBase.ActiveTabIndex = 1

            pnlDetalle.Visible = True

            txtNombre.Focus()
            Me.ActivarOpciones(True, False)

            RowBase = CType(tblBase.Rows(gvBase.SelectedRow.DataItemIndex), DBSecurity.SchemaSecurity.TBL_ModuloRow)

            ' Data
            lblCodModulo.Text = CStr(RowBase.id_Modulo)
            txtNombre.Text = RowBase.Nombre_Modulo
            chkPermisosHeredados.Checked = RowBase.Usa_Permisos_Heredados
            txtEnsamblado.Text = RowBase.Ensamblado_Modulo
            CadenaTextBox.Text = RowBase.ConnectionString

            tblModulo.Rows.Clear()
            tblModulo.Rows.Add(RowBase.ItemArray)
            tblModulo.AcceptChanges()
        End Sub

        Private Sub GuardarCambios()
            If Validar() Then
                Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)
                Dim RowModulo As DBSecurity.SchemaSecurity.TBL_ModuloRow
                Dim isNuevo As Boolean = False

                Try
                    dbmSecurity.Connection_Open(MySesion.Usuario.id)
                    dbmSecurity.Transaction_Begin()
                    'dbmSecurity.DataBase.Identifier_Date_Format = Program.IdentifierDateFormat

                    If lblCodModulo.Text = "-1" Then
                        isNuevo = True
                        lblCodModulo.Text = CStr(dbmSecurity.SchemaSecurity.TBL_Modulo.DBNextId)

                        RowModulo = tblModulo.NewTBL_ModuloRow
                        RowModulo.id_Modulo = CShort(lblCodModulo.Text)
                    Else
                        RowModulo = tblModulo.FindByid_Modulo(CShort(lblCodModulo.Text))
                    End If

                    RowModulo.Nombre_Modulo = txtNombre.Text
                    RowModulo.Usa_Permisos_Heredados = chkPermisosHeredados.Checked
                    RowModulo.Ensamblado_Modulo = txtEnsamblado.Text
                    RowModulo.ConnectionString = CadenaTextBox.Text
                    RowModulo.Eliminado = False
                    RowModulo.Fecha_log = Now
                    RowModulo.fk_Usuario_Log = MySesion.Usuario.id

                    If isNuevo Then
                        tblModulo.Rows.Add(RowModulo)
                    End If

                    dbmSecurity.SchemaSecurity.TBL_Modulo.DBSaveTable(tblModulo)

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
                        lblCodModulo.Text = "-1"
                        tblModulo.RejectChanges()
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
                dbmSecurity.SchemaSecurity.TBL_Modulo.DBDelete(CShort(lblCodModulo.Text))

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

#End Region

#Region " Funciones "

        Private Function Validar() As Boolean
            Return True
        End Function

#End Region

    End Class

End Namespace