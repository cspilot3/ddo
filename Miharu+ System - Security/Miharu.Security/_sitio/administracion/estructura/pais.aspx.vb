Imports Miharu.Security._clases

Namespace _sitio.administracion.estructura

    Partial Public Class pais
        Inherits FormBase

#Region " Declaraciones "

        Private Const MyPathPermiso As String = "1.1.5"

        Private tblBase As DBSecurity.SchemaConfig.TBL_PaisDataTable
        Private tblPais As DBSecurity.SchemaConfig.TBL_PaisDataTable

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
            tblBase = New DBSecurity.SchemaConfig.TBL_PaisDataTable
            Me.MySesion.Pagina.Parameter("tblBase") = tblBase

            tblPais = New DBSecurity.SchemaConfig.TBL_PaisDataTable
            Me.MySesion.Pagina.Parameter("tblPais") = tblPais
        End Sub

        Private Sub Load_Data()
            tblBase = CType(Me.MySesion.Pagina.Parameter("tblBase"), DBSecurity.SchemaConfig.TBL_PaisDataTable)
            tblPais = CType(Me.MySesion.Pagina.Parameter("tblPais"), DBSecurity.SchemaConfig.TBL_PaisDataTable)
        End Sub

        Private Sub Buscar(ByVal nParametro As String)
            gvBase.SelectedIndex = -1

            If nParametro <> "" Then
                Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)

                Try
                    dbmSecurity.Connection_Open(MySesion.Usuario.id)
                    dbmSecurity.SchemaConfig.TBL_Pais.DBFillByNombre_Pais(tblBase, nParametro)
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
            tblPais.Rows.Clear()

            lblCodPais.Text = "-1"
            txtCodigo.Text = ""
            txtNombre.Text = ""
        End Sub

        Private Sub NuevoRegistro()
            ClearForm()

            tcBase.ActiveTabIndex = 1

            pnlDetalle.Visible = True

            txtNombre.Focus()
            Me.ActivarOpciones(True, True)
        End Sub

        Private Sub EditarRegistro()
            Dim RowBase As DBSecurity.SchemaConfig.TBL_PaisRow

            ClearForm()
            tcBase.ActiveTabIndex = 1

            pnlDetalle.Visible = True

            txtNombre.Focus()
            Me.ActivarOpciones(True, False)

            RowBase = CType(tblBase.Rows(gvBase.SelectedRow.DataItemIndex), DBSecurity.SchemaConfig.TBL_PaisRow)

            ' Data
            lblCodPais.Text = CStr(RowBase.id_Pais)
            txtNombre.Text = RowBase.Nombre_Pais
            txtCodigo.Text = CStr(RowBase.id_Pais)

            tblPais.Rows.Clear()
            tblPais.Rows.Add(RowBase.ItemArray)
            tblPais.AcceptChanges()
        End Sub

        Private Sub GuardarCambios()
            If Validar() Then
                Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)
                Dim RowPais As DBSecurity.SchemaConfig.TBL_PaisRow
                Dim isNuevo As Boolean = False

                Try
                    dbmSecurity.Connection_Open(MySesion.Usuario.id)
                    dbmSecurity.Transaction_Begin()

                    'dbmSecurity.DataBase.Identifier_Date_Format = Program.IdentifierDateFormat

                    If lblCodPais.Text = "-1" Then
                        isNuevo = True
                        lblCodPais.Text = CStr(dbmSecurity.SchemaConfig.TBL_Pais.DBNextId)

                        RowPais = tblPais.NewTBL_PaisRow
                        RowPais.id_Pais = CShort(lblCodPais.Text)
                    Else
                        RowPais = tblPais.FindByid_Pais(CShort(lblCodPais.Text))
                    End If

                    RowPais.Nombre_Pais = txtNombre.Text
                    RowPais.fk_Usuario_Log = MySesion.Usuario.id
                    RowPais.Eliminado = False
                    RowPais.Fecha_log = Now

                    If isNuevo Then
                        tblPais.Rows.Add(RowPais)
                    End If

                    dbmSecurity.SchemaConfig.TBL_Pais.DBSaveTable(tblPais)

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
                        lblCodPais.Text = "-1"
                        tblPais.RejectChanges()
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
                dbmSecurity.SchemaConfig.TBL_Pais.DBDelete(CShort(lblCodPais.Text))
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