Imports Miharu.Security._clases

Namespace _sitio.administracion.estructura

    Partial Public Class dependencias
        Inherits FormBase

#Region " Declaraciones "

        Private Const MyPathPermiso As String = "1.1.3"

        Private Property tblBase As DBSecurity.SchemaConfig.TBL_DependenciaDataTable
            Get
                Return CType(Me.MySesion.Pagina.Parameter("tblBase"), DBSecurity.SchemaConfig.TBL_DependenciaDataTable)
            End Get
            Set(value As DBSecurity.SchemaConfig.TBL_DependenciaDataTable)
                Me.MySesion.Pagina.Parameter("tblBase") = value
            End Set
        End Property

        Private Property tblEntidad As DBSecurity.SchemaConfig.TBL_EntidadDataTable
            Get
                Return CType(Me.MySesion.Pagina.Parameter("tblEntidad"), DBSecurity.SchemaConfig.TBL_EntidadDataTable)
            End Get
            Set(value As DBSecurity.SchemaConfig.TBL_EntidadDataTable)
                Me.MySesion.Pagina.Parameter("tblEntidad") = value
            End Set
        End Property

        Private Property tblDependencia As DBSecurity.SchemaConfig.TBL_DependenciaDataTable
            Get
                Return CType(Me.MySesion.Pagina.Parameter("tblDependencia"), DBSecurity.SchemaConfig.TBL_DependenciaDataTable)
            End Get
            Set(value As DBSecurity.SchemaConfig.TBL_DependenciaDataTable)
                Me.MySesion.Pagina.Parameter("tblDependencia") = value
            End Set
        End Property

        Private Property tblPadre As DBSecurity.SchemaConfig.TBL_DependenciaDataTable
            Get
                Return CType(Me.MySesion.Pagina.Parameter("tblPadre"), DBSecurity.SchemaConfig.TBL_DependenciaDataTable)
            End Get
            Set(value As DBSecurity.SchemaConfig.TBL_DependenciaDataTable)
                Me.MySesion.Pagina.Parameter("tblPadre") = value
            End Set
        End Property

        Private Property tblNivel As DBSecurity.SchemaConfig.TBL_NivelDataTable
            Get
                Return CType(Me.MySesion.Pagina.Parameter("tblNivel"), DBSecurity.SchemaConfig.TBL_NivelDataTable)
            End Get
            Set(value As DBSecurity.SchemaConfig.TBL_NivelDataTable)
                Me.MySesion.Pagina.Parameter("tblNivel") = value
            End Set
        End Property

        Private Property tblSedeFiltro As DBSecurity.SchemaConfig.TBL_SedeDataTable
            Get
                Return CType(Me.MySesion.Pagina.Parameter("tblSedeFiltro"), DBSecurity.SchemaConfig.TBL_SedeDataTable)
            End Get
            Set(value As DBSecurity.SchemaConfig.TBL_SedeDataTable)
                Me.MySesion.Pagina.Parameter("tblSedeFiltro") = value
            End Set
        End Property

        Private Property tblDependencia_SedeFiltro As DBSecurity.SchemaConfig.TBL_SedeDataTable
            Get
                Return CType(Me.MySesion.Pagina.Parameter("tblDependencia_SedeFiltro"), DBSecurity.SchemaConfig.TBL_SedeDataTable)
            End Get
            Set(value As DBSecurity.SchemaConfig.TBL_SedeDataTable)
                Me.MySesion.Pagina.Parameter("tblDependencia_SedeFiltro") = value
            End Set
        End Property

        Private Property tblSede_Dependencia As DBSecurity.SchemaConfig.TBL_Sede_DependenciaDataTable
            Get
                Return CType(Me.MySesion.Pagina.Parameter("tblSede_Dependencia"), DBSecurity.SchemaConfig.TBL_Sede_DependenciaDataTable)
            End Get
            Set(value As DBSecurity.SchemaConfig.TBL_Sede_DependenciaDataTable)
                Me.MySesion.Pagina.Parameter("tblSede_Dependencia") = value
            End Set
        End Property

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
            lblDepenPadreID.Text = CStr(MySesion.Pagina.Parameter("DependenciaPadreId"))
            txtDependenciaPadre.Text = CStr(MySesion.Pagina.Parameter("DependenciaPadreNombre"))
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

        Private Sub btnDependenciaPadre_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs) Handles btnDependenciaPadre.Click
            BuscarDependencia()
        End Sub

#End Region

#Region " Metodos "

        Private Sub Config_Page()
            tblBase = New DBSecurity.SchemaConfig.TBL_DependenciaDataTable
            tblEntidad = New DBSecurity.SchemaConfig.TBL_EntidadDataTable
            tblDependencia = New DBSecurity.SchemaConfig.TBL_DependenciaDataTable
            tblPadre = New DBSecurity.SchemaConfig.TBL_DependenciaDataTable
            tblNivel = New DBSecurity.SchemaConfig.TBL_NivelDataTable
            tblSedeFiltro = New DBSecurity.SchemaConfig.TBL_SedeDataTable
            tblDependencia_SedeFiltro = New DBSecurity.SchemaConfig.TBL_SedeDataTable
            tblSede_Dependencia = New DBSecurity.SchemaConfig.TBL_Sede_DependenciaDataTable

            ' Load Entidades-Nivel
            Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)

            Try
                dbmSecurity.Connection_Open(MySesion.Usuario.id)

                tblEntidad = dbmSecurity.SchemaConfig.TBL_Entidad.DBFindByNombre_Entidad(Nothing)
                dbmSecurity.SchemaConfig.TBL_Nivel.DBFill(tblNivel, Nothing)

            Catch ex As Exception
                Master.ShowAlert(ex.Message, MiharuMasterForm.MsgBoxIcon.IconError)
            Finally
                dbmSecurity.Connection_Close()
            End Try

            ShowEntidades()
            ShowNivel()
            '-------------------------------------------
            Load_SedeFiltro()

        End Sub

        Private Sub Load_Data()

        End Sub

        Private Sub Load_SedeFiltro()
            Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)
            Try
                dbmSecurity.Connection_Open(MySesion.Usuario.id)
                tblSedeFiltro.Clear()
                dbmSecurity.SchemaConfig.TBL_Sede_Dependencia.DBFill(tblSedeFiltro, CShort(0), Nothing, Nothing)
                'carga las sedes en el web control
                Delete_Columns(tblSedeFiltro)
                wucDependencias.tbl_in = tblSedeFiltro
                wucDependencias.tbl_in_aux = tblSedeFiltro
                tcDetalle.Enabled = False

            Catch ex As Exception
                Master.ShowAlert(ex.Message, MiharuMasterForm.MsgBoxIcon.IconError)
            Finally
                dbmSecurity.Connection_Close()
            End Try

        End Sub

        Private Sub New_SedeFiltro()
            Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)
            Try
                tblDependencia_SedeFiltro = New DBSecurity.SchemaConfig.TBL_SedeDataTable
                dbmSecurity.Connection_Open(MySesion.Usuario.id)

                dbmSecurity.SchemaConfig.TBL_Sede_Dependencia.DBFill(tblDependencia_SedeFiltro, CShort(ddlEntidad.SelectedValue), Nothing, Nothing)
                'carga las dependencias en el web control
                Delete_Columns(tblDependencia_SedeFiltro)
                wucDependencias.Set_Data(tblDependencia_SedeFiltro, Nothing)

            Catch ex As Exception
                Master.ShowAlert(ex.Message, MiharuMasterForm.MsgBoxIcon.IconError)
            Finally
                dbmSecurity.Connection_Close()
            End Try

        End Sub

        Private Sub Edit_SedeFiltro()
            '
            Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)
            Try
                tblSedeFiltro = New DBSecurity.SchemaConfig.TBL_SedeDataTable
                tblDependencia_SedeFiltro = New DBSecurity.SchemaConfig.TBL_SedeDataTable
                dbmSecurity.Connection_Open(MySesion.Usuario.id)

                tblDependencia_SedeFiltro.Clear()
                dbmSecurity.SchemaConfig.TBL_Sede.DBFill(tblDependencia_SedeFiltro, CShort(ddlEntidad.SelectedValue), CShort(lblCodDependencia.Text))
                dbmSecurity.SchemaConfig.TBL_Sede.DBFill(tblSedeFiltro, CShort(ddlEntidad.SelectedValue), CShort(lblCodDependencia.Text))

                'carga las dependencias en el web control
                Delete_Columns(tblSedeFiltro)
                Delete_Columns(tblDependencia_SedeFiltro)
                If tblDependencia_SedeFiltro.Rows.Count <> 0 Then
                    wucDependencias.Set_Data(tblSedeFiltro, tblDependencia_SedeFiltro)
                Else
                    wucDependencias.Set_Data(tblSedeFiltro, Nothing)

                End If
            Catch ex As Exception
                Master.ShowAlert(ex.Message, MiharuMasterForm.MsgBoxIcon.IconError)
            Finally
                dbmSecurity.Connection_Close()
            End Try
        End Sub

        Private Sub Buscar(ByVal nParametro As String)
            gvBase.SelectedIndex = -1

            If nParametro <> "" And ddlEntidad.SelectedIndex >= 0 Then
                Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)

                Try
                    dbmSecurity.Connection_Open(MySesion.Usuario.id)
                    tblBase.Clear()
                    dbmSecurity.SchemaConfig.TBL_Dependencia.DBFillByfk_EntidadNombre_Dependencia(tblBase, CShort(ddlEntidad.SelectedValue), nParametro)
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
            tblDependencia.Rows.Clear()

            lblNombreEntidad.Text = ddlEntidad.SelectedItem.Text

            lblCodDependencia.Text = "-1"
            lblDepenPadreID.Text = "0"
            txtNombre.Text = ""
            txtCodigo.Text = ""
            txtCentroCosto.Text = ""
            txtDependenciaPadre.Text = ""
            txtDireccion.Text = ""
            txtTelefono.Text = ""
            txtUbicacion.Text = ""
        End Sub

        Private Sub NuevoRegistro()
            ClearForm()
            tcBase.ActiveTabIndex = 1
            tcDetalle.Enabled = True
            tcDetalle.ActiveTabIndex = 0
            pnlDetalle.Visible = True

            txtNombre.Focus()
            chkActivo.Checked = True
            chkActivo.Enabled = False

            Me.ActivarOpciones(True, True)
            'carga las sedes
            New_SedeFiltro()
        End Sub

        Private Sub EditarRegistro()
            Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)
            Dim RowBase As DBSecurity.SchemaConfig.TBL_DependenciaRow
            Try

                ClearForm()
                tcBase.ActiveTabIndex = 1
                tcDetalle.ActiveTabIndex = 0

                pnlDetalle.Visible = True
                tcDetalle.Enabled = True

                txtNombre.Focus()
                Me.ActivarOpciones(True, False)

                RowBase = CType(tblBase.Rows(gvBase.SelectedRow.DataItemIndex), DBSecurity.SchemaConfig.TBL_DependenciaRow)

                ' Data
                lblCodDependencia.Text = CStr(RowBase.id_Dependencia)
                txtNombre.Text = RowBase.Nombre_Dependencia
                txtCodigo.Text = RowBase.Codigo_Dependencia
                ' New Data
                txtCentroCosto.Text = RowBase.Centro_Costo
                txtDireccion.Text = RowBase.Direccion_Dependencia
                txtTelefono.Text = RowBase.Telefono_Dependencia
                txtUbicacion.Text = RowBase.Ubicacion_Dependencia
                chkActivo.Checked = RowBase.Activo
                chkActivo.Enabled = True
                ddlNivel.SelectedValue = CStr(RowBase.fk_Nivel)
                'Padre
                dbmSecurity.Connection_Open(MySesion.Usuario.id)
                If RowBase.fk_Padre <> 0 Then
                    tblPadre.Clear()
                    dbmSecurity.SchemaConfig.TBL_Dependencia.DBFillByfk_Entidadid_Dependencia(tblPadre, CShort(ddlEntidad.SelectedValue), CShort(RowBase.fk_Padre))
                    If tblPadre.Rows.Count > 0 Then
                        txtDependenciaPadre.Text = tblPadre.Rows(0).Item("Nombre_Dependencia").ToString
                        lblDepenPadreID.Text = tblPadre.Rows(0).Item("id_Dependencia").ToString
                    End If
                End If

                '---------
                tblDependencia.Rows.Clear()
                tblDependencia.Rows.Add(RowBase.ItemArray)
                tblDependencia.AcceptChanges()

                'load Dependencias
                Edit_SedeFiltro()
            Catch ex As Exception
                Master.ShowAlert(ex.Message, MiharuMasterForm.MsgBoxIcon.IconError)
            Finally
                dbmSecurity.Connection_Close()
            End Try

        End Sub

        Private Sub GuardarCambios()
            If Validar() Then
                Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)
                Dim RowDependencia As DBSecurity.SchemaConfig.TBL_DependenciaRow
                Dim isNuevo As Boolean = False

                Try
                    dbmSecurity.Connection_Open(MySesion.Usuario.id)
                    dbmSecurity.Transaction_Begin()

                    'dbmSecurity.DataBase.Identifier_Date_Format = Program.IdentifierDateFormat

                    If lblCodDependencia.Text = "-1" Then
                        isNuevo = True
                        lblCodDependencia.Text = CStr(dbmSecurity.SchemaConfig.TBL_Dependencia.DBNextId(CShort(ddlEntidad.SelectedValue)))

                        RowDependencia = tblDependencia.NewTBL_DependenciaRow
                        RowDependencia.fk_Entidad = CShort(ddlEntidad.SelectedValue)
                        RowDependencia.id_Dependencia = CShort(lblCodDependencia.Text)

                        If lblCodDependencia.Text = "1" Then                              'Cuando es la dependencia Cabeza o General
                            RowDependencia.fk_Nivel = 1
                            RowDependencia.fk_Padre = 0
                        End If

                    Else
                        RowDependencia = tblDependencia.FindByfk_Entidadid_Dependencia(CShort(ddlEntidad.SelectedValue), CShort(lblCodDependencia.Text))
                    End If

                    If lblDepenPadreID.Text = "-1" And lblCodDependencia.Text <> "1" Then 'Cuando no le asignan dependencias padres hereda de la cabeza o general
                        RowDependencia.fk_Padre = 1
                    End If

                    RowDependencia.Nombre_Dependencia = txtNombre.Text
                    RowDependencia.Codigo_Dependencia = txtCodigo.Text
                    'RowDependencia.fk_Entidad_Log = MySesion.Entidad.id
                    RowDependencia.fk_Usuario_Log = MySesion.Usuario.id
                    RowDependencia.fk_Padre = CShort(lblDepenPadreID.Text)
                    RowDependencia.Activo = chkActivo.Checked
                    RowDependencia.Direccion_Dependencia = txtDireccion.Text
                    RowDependencia.Centro_Costo = txtCentroCosto.Text
                    RowDependencia.Ubicacion_Dependencia = txtUbicacion.Text
                    RowDependencia.Telefono_Dependencia = txtTelefono.Text
                    RowDependencia.Eliminado = False
                    RowDependencia.Fecha_log = Now
                    RowDependencia.fk_Nivel = CType(ddlNivel.SelectedValue, Byte)                   'fk_Nivel

                    If isNuevo Then
                        tblDependencia.Rows.Add(RowDependencia)
                    End If


                    dbmSecurity.SchemaConfig.TBL_Dependencia.DBSaveTable(tblDependencia)

                    'Save Sedes
                    'valida->cuando no existen sedes retorna una fila cero
                    Dim tbl As DataTable = wucDependencias.tbl_out
                    If Not tbl Is Nothing Then
                        If tbl.Rows(0).Item("fk").ToString <> "0" Then
                            dbmSecurity.SchemaConfig.TBL_Sede_Dependencia.DBDelete(CShort(ddlEntidad.SelectedValue), Nothing, CShort(lblCodDependencia.Text))
                            For i As Integer = 0 To tbl.Rows.Count - 1
                                Dim RowSede_Dependencia As DBSecurity.SchemaConfig.TBL_Sede_DependenciaRow
                                RowSede_Dependencia = tblSede_Dependencia.NewTBL_Sede_DependenciaRow
                                RowSede_Dependencia.fk_Entidad = CShort(tbl.Rows(i).Item("fk"))         'fk entidad
                                RowSede_Dependencia.fk_Sede = CShort(tbl.Rows(i).Item("id"))            'fk_Sede
                                RowSede_Dependencia.fk_Dependencia = CShort(lblCodDependencia.Text)     'fk_Dependencia
                                RowSede_Dependencia.fk_Usuario_Log = MySesion.Usuario.id
                                RowSede_Dependencia.Eliminado = False
                                RowSede_Dependencia.Fecha_log = Now
                                tblSede_Dependencia.Rows.Add(RowSede_Dependencia)
                            Next

                            dbmSecurity.SchemaConfig.TBL_Sede_Dependencia.DBSaveTable(tblSede_Dependencia)
                        End If
                    End If
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
                        lblCodDependencia.Text = "-1"
                        tblDependencia.RejectChanges()
                    End If
                Finally
                    dbmSecurity.Connection_Close()
                End Try
            End If
        End Sub

        Private Sub EliminarRegistro()
            Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)
            Try

                If lblCodDependencia.Text <> "1" And lblCodDependencia.Text <> "-1" Then  '<> Dependencia Raiz
                    dbmSecurity.SchemaConfig.TBL_Dependencia.DBDelete(CShort(ddlEntidad.SelectedValue), CShort(lblCodDependencia.Text))
                    'delete sedes Eliminado=1 
                    Dim tbl As DataTable = wucDependencias.tbl_out
                    For i As Integer = 0 To tbl.Rows.Count - 1
                        dbmSecurity.SchemaConfig.TBL_Sede_Dependencia.DBDelete(CShort(ddlEntidad.SelectedValue), _
                                                                               CShort(tbl.Rows(i).Item("id")), _
                                                                               CShort(lblCodDependencia.Text))
                    Next
                    ClearForm()

                    Buscar(ucFiltro.Parametro)

                    Me.Master.ShowAlert("El registro se eliminó exitosamente", MiharuMasterForm.MsgBoxIcon.IconInformation)

                    tcBase.ActiveTabIndex = 0
                    tcDetalle.ActiveTabIndex = 0
                    pnlDetalle.Visible = False
                    Me.ActivarOpciones(False, False)
                Else
                    Me.Master.ShowAlert("No se puede eliminar la dependencia raíz", MiharuMasterForm.MsgBoxIcon.IconInformation)
                End If

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

        Private Sub ShowNivel()
            ddlNivel.DataSource = tblNivel
            ddlNivel.DataValueField = "id_Nivel"
            ddlNivel.DataTextField = "Nombre_Nivel"
            ddlNivel.DataBind()
        End Sub

        Private Sub BuscarDependencia()
            MySesion.Pagina.Parameter("DependenciaPadreId") = CInt(lblDepenPadreID.Text)
            MySesion.Pagina.Parameter("DependenciaPadreNombre") = txtDependenciaPadre.Text
            MySesion.Pagina.Parameter("EntidadId") = CShort(ddlEntidad.SelectedValue)
            Master.ShowDialog("administracion/estructura/p_dependencia_padre.aspx", "PopUpFile", "Dependencia Padre", "530", "300", "200", "200", False)
        End Sub
#End Region

#Region " Funciones "

        Private Function Validar() As Boolean
            Return True
        End Function
        Private Sub Delete_Columns(ByVal tbl As DBSecurity.SchemaConfig.TBL_SedeDataTable)
            'Autor_Lady
            tbl.Columns.Remove(tbl.Direccion_SedeColumn.ColumnName)
            tbl.Columns.Remove(tbl.Centro_CostoColumn.ColumnName)
            tbl.Columns.Remove(tbl.fk_PaisColumn.ColumnName)
            tbl.Columns.Remove(tbl.fk_RegionalColumn.ColumnName)
            tbl.Columns.Remove(tbl.fk_RegionColumn.ColumnName)
            tbl.Columns.Remove(tbl.fk_CiudadColumn.ColumnName)
            tbl.Columns.Remove(tbl.Fecha_logColumn.ColumnName)
            tbl.Columns.Remove(tbl.fk_Usuario_LogColumn.ColumnName)
            tbl.Columns.Remove(tbl.Telefono_SedeColumn.ColumnName)
            tbl.Columns.Remove(tbl.EliminadoColumn.ColumnName)
            tbl.Columns.Remove(tbl.UbicacionColumn.ColumnName)
            tbl.Columns.Remove(tbl.ClasificacionColumn.ColumnName)
            tbl.Columns("Codigo_Sede").ColumnName = "Codigo"
            tbl.Columns("Nombre_Sede").ColumnName = "Nombre"
            tbl.Columns("id_Sede").ColumnName = "id"
            tbl.Columns("fk_Entidad").ColumnName = "fk"            
        End Sub
#End Region

    End Class
End Namespace