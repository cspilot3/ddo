Imports Miharu.Security._clases

Namespace _sitio.administracion.estructura

    Partial Public Class sedes
        Inherits FormBase

#Region " Declaraciones "

        Private Const MyPathPermiso As String = "1.1.8"

        Private tblBase As DBSecurity.SchemaConfig.TBL_SedeDataTable
        Private tblEntidad As DBSecurity.SchemaConfig.TBL_EntidadDataTable
        Private tblSede As DBSecurity.SchemaConfig.TBL_SedeDataTable
        Private tblPais As DBSecurity.SchemaConfig.TBL_PaisDataTable
        Private tblRegion As DBSecurity.SchemaConfig.TBL_RegionDataTable
        Private tblRegional As DBSecurity.SchemaConfig.TBL_RegionalDataTable
        Private tblCiudad As DBSecurity.SchemaConfig.TBL_CiudadDataTable
        Private tblDependenciaFiltro As DBSecurity.SchemaConfig.TBL_DependenciaDataTable
        Private tblSede_DependenciaFiltro As DBSecurity.SchemaConfig.TBL_DependenciaDataTable
        Private tblSede_Dependencia As DBSecurity.SchemaConfig.TBL_Sede_DependenciaDataTable

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
            Load_Regional()                                        'Carga las regionales por entidad
            Buscar(ucFiltro.Parametro)
        End Sub

        Private Sub ddlPais_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlPais.SelectedIndexChanged
            'carga las regiones
            Load_Region(CShort(ddlPais.SelectedValue))
        End Sub

        Private Sub ddlRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlRegion.SelectedIndexChanged
            'carga las ciudades
            Load_Ciudad(CShort(ddlRegion.SelectedValue))
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

            tblBase = New DBSecurity.SchemaConfig.TBL_SedeDataTable
            Me.MySesion.Pagina.Parameter("tblBase") = tblBase

            tblEntidad = New DBSecurity.SchemaConfig.TBL_EntidadDataTable
            Me.MySesion.Pagina.Parameter("tblEntidad") = tblEntidad

            tblSede = New DBSecurity.SchemaConfig.TBL_SedeDataTable
            Me.MySesion.Pagina.Parameter("tblSede") = tblSede

            tblPais = New DBSecurity.SchemaConfig.TBL_PaisDataTable
            Me.MySesion.Pagina.Parameter("tblPais") = tblPais

            tblRegion = New DBSecurity.SchemaConfig.TBL_RegionDataTable
            Me.MySesion.Pagina.Parameter("tblRegion") = tblRegion

            tblCiudad = New DBSecurity.SchemaConfig.TBL_CiudadDataTable
            Me.MySesion.Pagina.Parameter("tblCiudad") = tblCiudad

            tblRegional = New DBSecurity.SchemaConfig.TBL_RegionalDataTable
            Me.MySesion.Pagina.Parameter("tblRegional") = tblRegional

            tblRegional = New DBSecurity.SchemaConfig.TBL_RegionalDataTable
            Me.MySesion.Pagina.Parameter("tblRegional") = tblRegional

            tblDependenciaFiltro = New DBSecurity.SchemaConfig.TBL_DependenciaDataTable
            Me.MySesion.Pagina.Parameter("tblDependenciaFiltro") = tblDependenciaFiltro

            tblSede_DependenciaFiltro = New DBSecurity.SchemaConfig.TBL_DependenciaDataTable
            Me.MySesion.Pagina.Parameter("tblSede_DependenciaFiltro") = tblSede_Dependencia

            tblSede_Dependencia = New DBSecurity.SchemaConfig.TBL_Sede_DependenciaDataTable
            Me.MySesion.Pagina.Parameter("tblSede_Dependencia") = tblSede_Dependencia

            ' Load Entidades--Pais
            Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)

            Try
                dbmSecurity.Connection_Open(MySesion.Usuario.id)

                tblEntidad = dbmSecurity.SchemaConfig.TBL_Entidad.DBFindByNombre_Entidad(Nothing)
                tblPais = dbmSecurity.SchemaConfig.TBL_Pais.DBFindByid_Pais(Nothing)

            Catch ex As Exception
                Master.ShowAlert(ex.Message, MiharuMasterForm.MsgBoxIcon.IconError)
            Finally
                dbmSecurity.Connection_Close()
            End Try

            ShowEntidades()
            ShowPais()
            Load_Regional()
            Load_Region(CShort(ddlPais.SelectedValue))
            Load_Ciudad(CShort(ddlRegion.SelectedValue))
            '-------------------------------------------
            Load_DependenciaFiltro()

        End Sub

        Private Sub Load_Data()
            tblBase = CType(Me.MySesion.Pagina.Parameter("tblBase"), DBSecurity.SchemaConfig.TBL_SedeDataTable)
            tblEntidad = CType(Me.MySesion.Pagina.Parameter("tblEntidad"), DBSecurity.SchemaConfig.TBL_EntidadDataTable)
            tblSede = CType(Me.MySesion.Pagina.Parameter("tblSede"), DBSecurity.SchemaConfig.TBL_SedeDataTable)
            tblPais = CType(Me.MySesion.Pagina.Parameter("tblPais"), DBSecurity.SchemaConfig.TBL_PaisDataTable)
            tblRegion = CType(Me.MySesion.Pagina.Parameter("tblRegion"), DBSecurity.SchemaConfig.TBL_RegionDataTable)
            tblCiudad = CType(Me.MySesion.Pagina.Parameter("tblCiudad"), DBSecurity.SchemaConfig.TBL_CiudadDataTable)
            tblRegional = CType(Me.MySesion.Pagina.Parameter("tblRegional"), DBSecurity.SchemaConfig.TBL_RegionalDataTable)
            tblDependenciaFiltro = CType(Me.MySesion.Pagina.Parameter("tblDependenciaFiltro"), DBSecurity.SchemaConfig.TBL_DependenciaDataTable)
            tblSede_DependenciaFiltro = CType(Me.MySesion.Pagina.Parameter("tblSede_DependenciaFiltro"), DBSecurity.SchemaConfig.TBL_DependenciaDataTable)
            tblSede_Dependencia = CType(Me.MySesion.Pagina.Parameter("tblSede_Dependencia"), DBSecurity.SchemaConfig.TBL_Sede_DependenciaDataTable)
        End Sub

        Private Sub Load_Region(ByVal id_pais As Short)
            Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)
            Try
                dbmSecurity.Connection_Open(MySesion.Usuario.id)
                tblRegion.Clear()
                dbmSecurity.SchemaConfig.TBL_Region.DBFillByfk_PaisNombre_Region(tblRegion, id_pais, Nothing)
                ShowRegion()
                ddlCiudad.Items.Clear()
            Catch ex As Exception
                Master.ShowAlert(ex.Message, MiharuMasterForm.MsgBoxIcon.IconError)
            Finally
                dbmSecurity.Connection_Close()
            End Try
        End Sub

        Private Sub Load_Ciudad(ByVal id_region As Short)
            Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)
            Try
                dbmSecurity.Connection_Open(MySesion.Usuario.id)
                tblCiudad.Clear()
                dbmSecurity.SchemaConfig.TBL_Ciudad.DBFillByfk_Paisfk_RegionNombre_Ciudad(tblCiudad, CShort(ddlPais.SelectedValue), id_region, Nothing)
                ShowCiudads()
            Catch ex As Exception
                Master.ShowAlert(ex.Message, MiharuMasterForm.MsgBoxIcon.IconError)
            Finally
                dbmSecurity.Connection_Close()
            End Try
        End Sub

        Private Sub Load_Regional()
            Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)
            Try
                dbmSecurity.Connection_Open(MySesion.Usuario.id)
                tblRegional.Clear()
                dbmSecurity.SchemaConfig.TBL_Regional.DBFillByfk_EntidadNombre_Regional(tblRegional, CShort(ddlEntidad.SelectedValue), Nothing)
                ShowRegional()
            Catch ex As Exception
                Master.ShowAlert(ex.Message, MiharuMasterForm.MsgBoxIcon.IconError)
            Finally
                dbmSecurity.Connection_Close()
            End Try


        End Sub

        Private Sub Load_DependenciaFiltro()
            Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)
            Try
                dbmSecurity.Connection_Open(MySesion.Usuario.id)
                tblDependenciaFiltro.Clear()
                dbmSecurity.SchemaConfig.TBL_Dependencia.DBFillByfk_Entidadid_Dependencia(tblDependenciaFiltro, CShort(0), Nothing)
                'carga las dependencias en el web control
                Delete_Columns(tblDependenciaFiltro)
                wucDependencias.tbl_in = tblDependenciaFiltro
                wucDependencias.tbl_in_aux = tblDependenciaFiltro
                tcDetalle.Enabled = False

            Catch ex As Exception
                Master.ShowAlert(ex.Message, MiharuMasterForm.MsgBoxIcon.IconError)
            Finally
                dbmSecurity.Connection_Close()
            End Try

        End Sub

        Private Sub New_DependenciaFiltro()
            Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)
            Try
                tblDependenciaFiltro = New DBSecurity.SchemaConfig.TBL_DependenciaDataTable
                dbmSecurity.Connection_Open(MySesion.Usuario.id)

                tblDependenciaFiltro = dbmSecurity.SchemaConfig.TBL_Dependencia.DBFindByfk_EntidadNombre_Dependencia(CShort(ddlEntidad.SelectedValue), Nothing)
                'carga las dependencias en el web control
                Delete_Columns(tblDependenciaFiltro)
                wucDependencias.Set_Data(tblDependenciaFiltro, Nothing)

            Catch ex As Exception
                Master.ShowAlert(ex.Message, MiharuMasterForm.MsgBoxIcon.IconError)
            Finally
                dbmSecurity.Connection_Close()
            End Try

        End Sub

        Private Sub Edit_DependenciaFiltro()
            '
            Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)
            Try
                tblDependenciaFiltro = New DBSecurity.SchemaConfig.TBL_DependenciaDataTable
                tblSede_DependenciaFiltro = New DBSecurity.SchemaConfig.TBL_DependenciaDataTable
                dbmSecurity.Connection_Open(MySesion.Usuario.id)

                tblDependenciaFiltro = dbmSecurity.SchemaConfig.PA_dependencias_sede.DBExecute(CShort(lblCodSede.Text), CShort(ddlEntidad.SelectedValue), False)
                tblSede_DependenciaFiltro = dbmSecurity.SchemaConfig.PA_dependencias_sede.DBExecute(CShort(lblCodSede.Text), CShort(ddlEntidad.SelectedValue), True)

                'carga las dependencias en el web control
                Delete_Columns(tblDependenciaFiltro)
                Delete_Columns(tblSede_DependenciaFiltro)
                If tblSede_DependenciaFiltro.Rows.Count <> 0 Then
                    wucDependencias.Set_Data(tblDependenciaFiltro, tblSede_DependenciaFiltro)
                Else
                    wucDependencias.Set_Data(tblDependenciaFiltro, Nothing)

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
                    dbmSecurity.SchemaConfig.TBL_Sede.DBFillByfk_EntidadNombre_Sede(tblBase, CShort(ddlEntidad.SelectedValue), nParametro)
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
        End Sub

        Private Sub Visualizar_Resultados()
            gvBase.DataSource = tblBase
            gvBase.DataBind()
        End Sub

        Private Sub ClearForm()
            tblSede.Rows.Clear()


            lblNombreEntidad.Text = ddlEntidad.SelectedItem.Text

            lblCodSede.Text = "-1"

            ddlPais.SelectedIndex = -1
            ddlRegion.SelectedIndex = -1
            ddlRegional.SelectedIndex = -1
            ddlCiudad.SelectedIndex = -1
            '----------------------------
            txtCodigo.Text = ""
            txtNombre.Text = ""
            txtCentroCosto.Text = ""
            txtClasificacion.Text = ""
            txtDireccion.Text = ""
            txtTelefono.Text = ""
            txtUbicacion.Text = ""
            '------------------------------

        End Sub

        Private Sub NuevoRegistro()
            ClearForm()
            tcBase.ActiveTabIndex = 1
            tcDetalle.Enabled = True
            tcDetalle.ActiveTabIndex = 0

            pnlDetalle.Visible = True

            txtNombre.Focus()
            Me.ActivarOpciones(True, True)
            'carga las dependencias
            New_DependenciaFiltro()
        End Sub

        Private Sub EditarRegistro()
            Try
                Dim RowBase As DBSecurity.SchemaConfig.TBL_SedeRow

                ClearForm()
                tcBase.ActiveTabIndex = 1
                tcDetalle.Enabled = True
                tcDetalle.ActiveTabIndex = 0

                pnlDetalle.Visible = True

                txtNombre.Focus()
                Me.ActivarOpciones(True, False)

                RowBase = CType(tblBase.Rows(gvBase.SelectedRow.DataItemIndex), DBSecurity.SchemaConfig.TBL_SedeRow)

                ' Data
                lblCodSede.Text = CStr(RowBase.id_Sede)
                txtNombre.Text = RowBase.Nombre_Sede
                ddlCiudad.SelectedValue = CStr(RowBase.fk_Ciudad)
                ddlPais.SelectedValue = CStr(RowBase.fk_Pais)
                ddlRegion.SelectedValue = CStr(RowBase.fk_Region)
                ddlRegional.SelectedValue = CStr(RowBase.fk_Regional)
                txtCodigo.Text = RowBase.Codigo_Sede
                txtCentroCosto.Text = RowBase.Centro_Costo
                txtClasificacion.Text = RowBase.Clasificacion
                txtDireccion.Text = RowBase.Direccion_Sede
                txtTelefono.Text = RowBase.Telefono_Sede
                txtUbicacion.Text = RowBase.Ubicacion
                tblSede.Rows.Clear()
                tblSede.Rows.Add(RowBase.ItemArray)
                tblSede.AcceptChanges()
                'load Dependencias
                Edit_DependenciaFiltro()
            Catch ex As Exception
                Master.ShowAlert(ex.Message, MiharuMasterForm.MsgBoxIcon.IconError)
            End Try

        End Sub

        Private Sub GuardarCambios()
            If Validar() Then

                Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)
                Dim RowSede As DBSecurity.SchemaConfig.TBL_SedeRow
                Dim isNuevo As Boolean = False

                Try
                    dbmSecurity.Connection_Open(MySesion.Usuario.id)
                    dbmSecurity.Transaction_Begin()

                    'dbmSecurity.DataBase.Identifier_Date_Format = Program.IdentifierDateFormat

                    If lblCodSede.Text = "-1" Then
                        isNuevo = True
                        lblCodSede.Text = CStr(dbmSecurity.SchemaConfig.TBL_Sede.DBNextId(CShort(ddlEntidad.SelectedValue)))

                        RowSede = tblSede.NewTBL_SedeRow
                        RowSede.fk_Entidad = CShort(ddlEntidad.SelectedValue)
                        RowSede.id_Sede = CShort(lblCodSede.Text)
                    Else
                        RowSede = tblSede.FindByfk_Entidadid_Sede(CShort(ddlEntidad.SelectedValue), CShort(lblCodSede.Text))
                    End If

                    RowSede.Nombre_Sede = txtNombre.Text
                    RowSede.fk_Ciudad = CShort(ddlCiudad.SelectedValue)
                    RowSede.Codigo_Sede = txtCodigo.Text
                    RowSede.fk_Pais = CShort(ddlPais.SelectedValue)
                    RowSede.fk_Region = CShort(ddlRegion.SelectedValue)
                    RowSede.fk_Regional = CShort(ddlRegional.SelectedValue)
                    RowSede.Centro_Costo = txtCentroCosto.Text
                    RowSede.Clasificacion = txtClasificacion.Text
                    RowSede.Direccion_Sede = txtDireccion.Text
                    RowSede.Ubicacion = txtUbicacion.Text
                    RowSede.Telefono_Sede = txtTelefono.Text
                    RowSede.fk_Usuario_Log = MySesion.Usuario.id
                    RowSede.Eliminado = False
                    RowSede.Fecha_log = Now

                    If isNuevo Then
                        tblSede.Rows.Add(RowSede)
                    End If

                    dbmSecurity.SchemaConfig.TBL_Sede.DBSaveTable(tblSede)

                    'Save Dependencias

                    Dim tbl As DataTable = wucDependencias.tbl_out

                    dbmSecurity.SchemaConfig.TBL_Sede_Dependencia.DBDelete(CShort(ddlEntidad.SelectedValue), CShort(lblCodSede.Text), Nothing)

                    For i As Integer = 0 To tbl.Rows.Count - 1
                        Dim RowSede_Dependencia As DBSecurity.SchemaConfig.TBL_Sede_DependenciaRow
                        RowSede_Dependencia = tblSede_Dependencia.NewTBL_Sede_DependenciaRow
                        RowSede_Dependencia.fk_Entidad = CShort(tbl.Rows(i).Item("fk"))         'fk entidad
                        RowSede_Dependencia.fk_Sede = CShort(lblCodSede.Text)                   'fk_Sede
                        RowSede_Dependencia.fk_Dependencia = CShort(tbl.Rows(i).Item("id"))     'fk_Dependencia
                        RowSede_Dependencia.fk_Usuario_Log = MySesion.Usuario.id
                        RowSede_Dependencia.Eliminado = False
                        RowSede_Dependencia.Fecha_log = Now
                        tblSede_Dependencia.Rows.Add(RowSede_Dependencia)
                    Next

                    dbmSecurity.SchemaConfig.TBL_Sede_Dependencia.DBSaveTable(tblSede_Dependencia)

                    dbmSecurity.Transaction_Commit()

                    Me.Master.ShowAlert("Los datos se almacenaron correctamente", MiharuMasterForm.MsgBoxIcon.IconInformation)

                    Buscar(ucFiltro.Parametro)

                    ActivarOpciones(True, False)
                    pnlDetalle.Visible = True
                    tcBase.ActiveTabIndex = 1
                    tcDetalle.ActiveTabIndex = 0

                Catch ex As Exception
                    dbmSecurity.Transaction_Rollback()
                    Master.ShowAlert(ex.Message, MiharuMasterForm.MsgBoxIcon.IconError)

                    If isNuevo Then
                        lblCodSede.Text = "-1"
                        tblSede.RejectChanges()
                    End If
                Finally
                    dbmSecurity.Connection_Close()
                End Try
            End If
        End Sub

        Private Sub EliminarRegistro()
            Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)
            Try
                dbmSecurity.SchemaConfig.TBL_Sede.DBDelete(CShort(ddlEntidad.SelectedValue), CShort(lblCodSede.Text))
                'delete dependencias Eliminado=1 
                Dim tbl As DataTable = wucDependencias.tbl_out
                For i As Integer = 0 To tbl.Rows.Count - 1
                    dbmSecurity.SchemaConfig.TBL_Sede_Dependencia.DBDelete(CShort(ddlEntidad.SelectedValue), _
                                                                           CShort(lblCodSede.Text), _
                                                                           CShort(tbl.Rows(i).Item("id")))
                Next

                Me.Master.ShowAlert("El registro se eliminó exitosamente", MiharuMasterForm.MsgBoxIcon.IconInformation)

                ClearForm()
                tcBase.ActiveTabIndex = 0
                Buscar(ucFiltro.Parametro)
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
        'Autor_Lady
        Private Sub ShowPais()
            ddlPais.DataSource = tblPais
            ddlPais.DataValueField = "id_Pais"
            ddlPais.DataTextField = "Nombre_Pais"
            ddlPais.DataBind()
        End Sub

        Private Sub ShowRegion()
            ddlRegion.DataSource = tblRegion
            ddlRegion.DataValueField = "id_Region"
            ddlRegion.DataTextField = "Nombre_Region"
            ddlRegion.DataBind()
        End Sub

        Private Sub ShowCiudads()
            ddlCiudad.DataSource = tblCiudad
            ddlCiudad.DataValueField = "id_Ciudad"
            ddlCiudad.DataTextField = "Nombre_Ciudad"
            ddlCiudad.DataBind()
        End Sub

        Private Sub ShowRegional()
            ddlRegional.DataSource = tblRegional
            ddlRegional.DataValueField = "id_Regional"
            ddlRegional.DataTextField = "Nombre_Regional"
            ddlRegional.DataBind()
        End Sub

#End Region

#Region " Funciones "

        Private Function Validar() As Boolean
            'validar la seleccion del pais,region y regional
            If ddlPais.SelectedValue = "" Or ddlRegion.SelectedValue = "" Or ddlRegional.SelectedValue = "" Then
                Me.Master.ShowAlert("Verifique que haya seleccionado Pais,Región y Regional", MiharuMasterForm.MsgBoxIcon.IconWarning)
                Return False
            Else

                Return True
            End If

        End Function

        Private Sub Delete_Columns(ByVal tbl As DBSecurity.SchemaConfig.TBL_DependenciaDataTable)
            'Autor_Lady
            tbl.Columns.Remove(tbl.ActivoColumn.ColumnName)
            tbl.Columns.Remove(tbl.Centro_CostoColumn.ColumnName)
            'tbl.Columns.Remove(tbl.fk_Entidad_LogColumn.ColumnName)
            tbl.Columns.Remove(tbl.fk_NivelColumn.ColumnName)
            tbl.Columns.Remove(tbl.Fecha_logColumn.ColumnName)
            tbl.Columns.Remove(tbl.fk_Usuario_LogColumn.ColumnName)
            tbl.Columns.Remove(tbl.fk_PadreColumn.ColumnName)
            tbl.Columns.Remove(tbl.EliminadoColumn.ColumnName)
            tbl.Columns.Remove(tbl.Direccion_DependenciaColumn.ColumnName)
            tbl.Columns.Remove(tbl.Ubicacion_DependenciaColumn.ColumnName)
            tbl.Columns.Remove(tbl.Telefono_DependenciaColumn.ColumnName)
            tbl.Columns("Codigo_Dependencia").ColumnName = "Codigo"
            tbl.Columns("Nombre_Dependencia").ColumnName = "Nombre"
            tbl.Columns("id_Dependencia").ColumnName = "id"
            tbl.Columns("fk_Entidad").ColumnName = "fk"            
        End Sub

#End Region

    End Class

End Namespace