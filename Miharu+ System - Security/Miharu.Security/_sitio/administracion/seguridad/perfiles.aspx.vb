Imports DBSecurity.Esquemas
Imports DBSecurity.SchemaSecurity
Imports Miharu.Security._clases

Namespace _sitio.administracion.seguridad

    Partial Public Class perfiles
        Inherits FormBase

#Region " Declaraciones "

        Private Const MyPathPermiso As String = "1.2.2"

        Private tblBase As DBSecurity.SchemaSecurity.TBL_PerfilDataTable
        Private tblPerfil As DBSecurity.SchemaSecurity.TBL_PerfilDataTable
        Private tblPerfilPermisos As DBSecurity.SchemaSecurity.TBL_Perfil_PermisosDataTable
        Private tblCadena As xsdSecurity.TBL_CadenaDataTable

        Private tblModulo As DBSecurity.SchemaSecurity.TBL_ModuloDataTable
        Private tblModuloMapa As DBSecurity.SchemaSecurity.TBL_Modulo_MapaDataTable


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

        Private Sub AsignarAccionesButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles AsignarAccionesButton.Click
            If lblCodPerfil.Text = "-1" Then   'Nuevo
                LoadAcciones_New()
            Else                               'Editar
                LoadAcciones_Edit()
            End If
            tcDetalle.ActiveTabIndex = 1
            tpAcciones.Enabled = True
            tpAcciones.HeaderText = "Acciones"
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
            tblBase = New DBSecurity.SchemaSecurity.TBL_PerfilDataTable
            Me.MySesion.Pagina.Parameter("tblBase") = tblBase

            tblPerfil = New DBSecurity.SchemaSecurity.TBL_PerfilDataTable
            Me.MySesion.Pagina.Parameter("tblPerfil") = tblPerfil

            tblPerfilPermisos = New DBSecurity.SchemaSecurity.TBL_Perfil_PermisosDataTable
            Me.MySesion.Pagina.Parameter("tblPerfilPermisos") = tblPerfilPermisos

            tblCadena = New xsdSecurity.TBL_CadenaDataTable
            Me.MySesion.Pagina.Parameter("tblCadena") = tblCadena

            tblModulo = New DBSecurity.SchemaSecurity.TBL_ModuloDataTable
            Me.MySesion.Pagina.Parameter("tblModulo") = tblModulo

            tblModuloMapa = New DBSecurity.SchemaSecurity.TBL_Modulo_MapaDataTable
            Me.MySesion.Pagina.Parameter("tblModuloMapa") = tblModuloMapa

            ' Load modulos
            Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)

            Try
                dbmSecurity.Connection_Open(MySesion.Usuario.id)

                dbmSecurity.SchemaSecurity.TBL_Modulo.DBFillByEnsamblado_Modulo(tblModulo, Nothing)
                dbmSecurity.SchemaSecurity.TBL_Modulo_Mapa.DBFill(tblModuloMapa, Nothing, Nothing)

            Catch ex As Exception
                Master.ShowAlert(ex.Message, MiharuMasterForm.MsgBoxIcon.IconError)
            Finally
                dbmSecurity.Connection_Close()
            End Try

            ShowArbol()
        End Sub

        Private Sub Load_Data()
            tblBase = CType(Me.MySesion.Pagina.Parameter("tblBase"), DBSecurity.SchemaSecurity.TBL_PerfilDataTable)
            tblPerfil = CType(Me.MySesion.Pagina.Parameter("tblPerfil"), DBSecurity.SchemaSecurity.TBL_PerfilDataTable)
            tblPerfilPermisos = CType(Me.MySesion.Pagina.Parameter("tblPerfilPermisos"), DBSecurity.SchemaSecurity.TBL_Perfil_PermisosDataTable)
            tblCadena = CType(Me.MySesion.Pagina.Parameter("tblCadena"), xsdSecurity.TBL_CadenaDataTable)

            tblModulo = CType(Me.MySesion.Pagina.Parameter("tblModulo"), DBSecurity.SchemaSecurity.TBL_ModuloDataTable)
            tblModuloMapa = CType(Me.MySesion.Pagina.Parameter("tblModuloMapa"), DBSecurity.SchemaSecurity.TBL_Modulo_MapaDataTable)
        End Sub

        Private Sub Buscar(ByVal nParametro As String)
            gvBase.SelectedIndex = -1

            If nParametro <> "" Then
                Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)

                Try
                    dbmSecurity.Connection_Open(MySesion.Usuario.id)
                    dbmSecurity.SchemaSecurity.TBL_Perfil.DBFillByNombre_Perfil(tblBase, nParametro)
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
            visible_controls(False)
        End Sub

        Private Sub Visualizar_Resultados()
            gvBase.DataSource = tblBase
            gvBase.DataBind()
        End Sub

        Private Sub ClearForm()
            tblPerfil.Rows.Clear()

            lblCodPerfil.Text = "-1"
            txtNombre.Text = ""
            txtDescripcion.Text = ""
            tblPerfilPermisos.Rows.Clear()

            UnselectArbol()
            'ShowCadenas()
        End Sub

        Private Sub NuevoRegistro()
            ClearForm()

            tcBase.ActiveTabIndex = 1
            tcDetalle.ActiveTabIndex = 0

            visible_controls(True)

            txtNombre.Focus()
            Me.ActivarOpciones(True, True)
        End Sub

        Private Sub EditarRegistro()
            Dim RowBase As DBSecurity.SchemaSecurity.TBL_PerfilRow

            tcBase.ActiveTabIndex = 1
            tcDetalle.ActiveTabIndex = 0

            visible_controls(True)

            txtNombre.Focus()

            RowBase = CType(tblBase.Rows(gvBase.SelectedRow.DataItemIndex), DBSecurity.SchemaSecurity.TBL_PerfilRow)

            ' Data
            lblCodPerfil.Text = CStr(RowBase.id_Perfil)
            txtNombre.Text = RowBase.Nombre_Perfil
            txtDescripcion.Text = RowBase.Descripcion_Perfil

            tblPerfil.Rows.Clear()
            tblPerfil.Rows.Add(RowBase.ItemArray)
            tblPerfil.AcceptChanges()

            ' Permisos
            Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)

            Try
                dbmSecurity.Connection_Open(MySesion.Usuario.id)
                tblPerfilPermisos.Clear()
                dbmSecurity.SchemaSecurity.TBL_Perfil_Permisos.DBFillByfk_Perfilfk_Modulo(tblPerfilPermisos, RowBase.id_Perfil, Nothing)

            Catch ex As Exception
                Master.ShowAlert(ex.Message, MiharuMasterForm.MsgBoxIcon.IconError)
            Finally
                dbmSecurity.Connection_Close()
            End Try

            'Acciones
            tpAcciones.Enabled = True
            tpAcciones.HeaderText = "Acciones"

            Me.ActivarOpciones(True, False)

            SelectArbol()
            'ShowCadenas()
        End Sub

        Private Sub GuardarCambios()
            If Validar() Then
                Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)
                Dim RowPerfil As DBSecurity.SchemaSecurity.TBL_PerfilRow
                Dim isNuevo As Boolean = False

                Try
                    dbmSecurity.Connection_Open(MySesion.Usuario.id)
                    dbmSecurity.Transaction_Begin()
                    ActualizarPermisos()
                    'dbmSecurity.DataBase.Identifier_Date_Format = Program.IdentifierDateFormat

                    If lblCodPerfil.Text = "-1" Then
                        isNuevo = True
                        lblCodPerfil.Text = CStr(dbmSecurity.SchemaSecurity.TBL_Perfil.DBNextId)

                        RowPerfil = tblPerfil.NewTBL_PerfilRow
                        RowPerfil.id_Perfil = CShort(lblCodPerfil.Text)

                        For Each RowPerfilPermisos As DBSecurity.SchemaSecurity.TBL_Perfil_PermisosRow In tblPerfilPermisos.Rows
                            RowPerfilPermisos.fk_Perfil = RowPerfil.id_Perfil
                            RowPerfilPermisos.Eliminado = False
                            RowPerfilPermisos.Fecha_log = Now
                        Next
                    Else
                        RowPerfil = tblPerfil.FindByid_Perfil(CShort(lblCodPerfil.Text))
                    End If

                    RowPerfil.Nombre_Perfil = txtNombre.Text
                    RowPerfil.Descripcion_Perfil = txtDescripcion.Text
                    RowPerfil.fk_Usuario_Log = MySesion.Usuario.id
                    RowPerfil.Eliminado = False
                    RowPerfil.Fecha_log = Now

                    If isNuevo Then
                        tblPerfil.Rows.Add(RowPerfil)
                    End If

                    dbmSecurity.SchemaSecurity.TBL_Perfil_Permisos.DBDelete(RowPerfil.id_Perfil, Nothing)   'Elimina los permisos por perfil
                    dbmSecurity.SchemaSecurity.TBL_Perfil.DBSaveTable(tblPerfil)
                    dbmSecurity.SchemaSecurity.TBL_Perfil_Permisos.DBSaveTable(tblPerfilPermisos)       'Guarda los permisos por perfil

                    dbmSecurity.Transaction_Commit()

                    Me.Master.ShowAlert("Los datos se almacenaron correctamente", MiharuMasterForm.MsgBoxIcon.IconInformation)

                    Buscar(ucFiltro.Parametro)

                    ActivarOpciones(True, False)
                    visible_controls(True)
                    tcBase.ActiveTabIndex = 1
                    tcDetalle.ActiveTabIndex = 0

                Catch ex As Exception
                    dbmSecurity.Transaction_Rollback()
                    Master.ShowAlert(ex.Message, MiharuMasterForm.MsgBoxIcon.IconError)

                    If isNuevo Then
                        lblCodPerfil.Text = "-1"
                        tblPerfil.RejectChanges()
                    End If
                Finally
                    dbmSecurity.Connection_Close()
                End Try
            End If

            'ShowCadenas()

        End Sub

        Private Sub EliminarRegistro()
            'Actualiza el campo Eliminado=1 de las tablas Perfil y Perfil_Permisos
            Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)

            Try
                dbmSecurity.SchemaSecurity.TBL_Perfil.DBDelete(CShort(lblCodPerfil.Text))

                Me.Master.ShowAlert("El registro se eliminó exitosamente", MiharuMasterForm.MsgBoxIcon.IconInformation)

                ClearForm()
                tcBase.ActiveTabIndex = 0
                tcDetalle.ActiveTabIndex = 0
                Buscar(ucFiltro.Parametro)
                visible_controls(False)
                Me.ActivarOpciones(False, False)
                lblCodPerfil.Text = "-1"

            Catch ex As Exception
                Master.ShowAlert(ex.Message, MiharuMasterForm.MsgBoxIcon.IconError)
            Finally
                dbmSecurity.Connection_Close()
            End Try
        End Sub

        Private Sub ActivarOpciones(ByVal nActivo As Boolean, ByVal nIsNew As Boolean)
            ibSave.Visible = lblCodPerfil.Text <> "0" And nActivo
            divSave.Style("display") = CStr(IIf(ibSave.Visible, "inline", "none"))

            ibDelete.Visible = lblCodPerfil.Text <> "0" And nActivo And Not nIsNew
            divDelete.Style("display") = CStr(IIf(ibDelete.Visible, "inline", "none"))

            If lblCodPerfil.Text <> "0" Then
                txtNombre.Enabled = True
                txtNombre.CssClass = "Textbox"

                txtDescripcion.Enabled = True
                txtDescripcion.CssClass = "Textbox"
            Else
                txtNombre.Enabled = False
                txtNombre.CssClass = "TextboxDisable"

                txtDescripcion.Enabled = False
                txtDescripcion.CssClass = "TextboxDisable"
            End If
        End Sub

        Private Sub ShowArbol()
            tvPermisos.Nodes.Clear()

            For Each RowModulo As DBSecurity.SchemaSecurity.TBL_ModuloRow In tblModulo.Rows
                Dim NewNodo As New TreeNode(RowModulo.Nombre_Modulo, "M-" & RowModulo.id_Modulo, "~/_images/menu/Modulos.png")

                NewNodo.SelectAction = TreeNodeSelectAction.Expand
                NewNodo.ShowCheckBox = False

                tvPermisos.Nodes.Add(NewNodo)
                InsertarNodos(NewNodo, RowModulo.id_Modulo, 0)
            Next

            tvPermisos.CollapseAll()
        End Sub

        Private Sub InsertarNodos(ByRef nNodo As TreeNode, ByVal nModulo As Short, ByVal nNodoPadre As Integer)
            Dim Nodos() As DBSecurity.SchemaSecurity.TBL_Modulo_MapaRow

            Nodos = CType(tblModuloMapa.Select("fk_Modulo = " & nModulo & " AND Nodo_Padre " & CStr(IIf(nNodoPadre = 0, " IS NULL", "= " & CStr(nNodoPadre)))), DBSecurity.SchemaSecurity.TBL_Modulo_MapaRow())

            For Each RowNodo As DBSecurity.SchemaSecurity.TBL_Modulo_MapaRow In Nodos
                Dim NewNodo As New TreeNode(RowNodo.Titulo_Nodo, "M-" & nModulo & "-" & RowNodo.id_Nodo) ', "~/_images/menu/Parametros.png")

                NewNodo.SelectAction = TreeNodeSelectAction.Expand
                NewNodo.ShowCheckBox = True

                nNodo.ChildNodes.Add(NewNodo)
                InsertarNodos(NewNodo, nModulo, RowNodo.id_Nodo)
            Next
        End Sub

        Private Sub SelectArbol()
            UnselectArbol()

            For Each RowPermiso As DBSecurity.SchemaSecurity.TBL_Perfil_PermisosRow In tblPerfilPermisos.Rows
                Dim RowNodo As DBSecurity.SchemaSecurity.TBL_Modulo_MapaRow

                RowNodo = tblModuloMapa.FindByfk_Moduloid_Nodo(RowPermiso.fk_Modulo, RowPermiso.fk_Nodo)

                CheckNode(tvPermisos.Nodes, "M-" & RowNodo.fk_Modulo & "-" & RowNodo.id_Nodo)
            Next
        End Sub

        Private Sub CheckNode(ByRef nNodes As TreeNodeCollection, ByVal nValue As String)
            For Each Nodo As TreeNode In nNodes
                If Nodo.Value = nValue Then
                    Nodo.Checked = True
                    ExpandNodo(Nodo)
                Else
                    CheckNode(Nodo.ChildNodes, nValue)
                End If
            Next
        End Sub

        Private Sub ExpandNodo(ByRef nNodo As TreeNode)
            nNodo.Expand()

            If Not nNodo.Parent Is Nothing Then
                ExpandNodo(nNodo.Parent)
            End If
        End Sub

        Private Sub UnselectArbol()
            While tvPermisos.CheckedNodes.Count > 0
                tvPermisos.CheckedNodes.Item(0).Checked = False
            End While

            tvPermisos.CollapseAll()
        End Sub

        Private Sub ActualizarPermisos()
            Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)
            Dim id As Short = 0

            tblPerfilPermisos.Rows.Clear()

            Dim chkConsultar As CheckBox
            Dim chkAgregar As CheckBox
            Dim chkEditar As CheckBox
            Dim chkEliminar As CheckBox
            Dim chkExportar As CheckBox
            Dim chkImprimir As CheckBox
            Dim i As Int32 = 0

            dbmSecurity.Connection_Open(MySesion.Usuario.id)

            For Each Nodo As TreeNode In tvPermisos.CheckedNodes

                If Nodo.Checked Then
                    Dim RowPerfilPermisos As DBSecurity.SchemaSecurity.TBL_Perfil_PermisosRow
                    Dim RowNodo As DBSecurity.SchemaSecurity.TBL_Modulo_MapaRow
                    Dim Partes() As String = Nodo.Value.Split("-"c)

                    RowNodo = tblModuloMapa.FindByfk_Moduloid_Nodo(CShort(Partes(1)), CShort(Partes(2)))


                    id += CShort(1)

                    RowPerfilPermisos = tblPerfilPermisos.NewTBL_Perfil_PermisosRow

                    RowPerfilPermisos.fk_Perfil = CShort(lblCodPerfil.Text)
                    RowPerfilPermisos.id_Perfil_Permisos = id
                    RowPerfilPermisos.fk_Modulo = RowNodo.fk_Modulo
                    RowPerfilPermisos.fk_Nodo = RowNodo.id_Nodo
                    RowPerfilPermisos.fk_Usuario_Log = MySesion.Usuario.id

                    If Nodo.ChildNodes.Count = 0 Then
                        RowPerfilPermisos.Cadena_Permiso = RowNodo.Path_Nodo
                    Else
                        RowPerfilPermisos.Cadena_Permiso = RowNodo.Path_Nodo & ".*"
                    End If
                    'Campos de chequeo----
                    Dim Fila As GridViewRow
                    If i <= gvCadenas.Rows.Count Then
                        Fila = gvCadenas.Rows(i)
                        chkConsultar = CType(Fila.FindControl("chkConsultar"), CheckBox)
                        RowPerfilPermisos.Consultar = chkConsultar.Checked
                        chkAgregar = CType(Fila.FindControl("chkAgregar"), CheckBox)
                        RowPerfilPermisos.Agregar = chkAgregar.Checked
                        chkEditar = CType(Fila.FindControl("chkEditar"), CheckBox)
                        RowPerfilPermisos.Editar = chkEditar.Checked
                        chkEliminar = CType(Fila.FindControl("chkEliminar"), CheckBox)
                        RowPerfilPermisos.Eliminar = chkEliminar.Checked
                        chkExportar = CType(Fila.FindControl("chkExportar"), CheckBox)
                        RowPerfilPermisos.Exportar = chkExportar.Checked
                        chkImprimir = CType(Fila.FindControl("chkImprimir"), CheckBox)
                        RowPerfilPermisos.Imprimir = chkImprimir.Checked
                    End If

                    RowPerfilPermisos.Eliminado = False
                    RowPerfilPermisos.Fecha_log = Now
                    i = i + 1
                    tblPerfilPermisos.Rows.Add(RowPerfilPermisos)
                End If
            Next

            dbmSecurity.Connection_Close()
        End Sub

        Private Sub LoadAcciones_New()
            'Permite cargar los permisos en el grid de Acciones cuando es nuevo registro
            tblCadena.Rows.Clear()

            For Each Nodo As TreeNode In tvPermisos.CheckedNodes
                If Nodo.Checked Then
                    Dim RowNodo As TBL_Modulo_MapaRow
                    Dim RowModulo As TBL_ModuloRow
                    Dim Partes() As String = Nodo.Value.Split("-"c)
                    Dim RowCadena As xsdSecurity.TBL_CadenaRow

                    RowNodo = tblModuloMapa.FindByfk_Moduloid_Nodo(CShort(Partes(1)), CShort(Partes(2)))
                    RowModulo = tblModulo.FindByid_Modulo(RowNodo.fk_Modulo)

                    RowCadena = tblCadena.NewTBL_CadenaRow

                    RowCadena.Modulo = RowModulo.Nombre_Modulo
                    RowCadena.Seccion = RowNodo.Titulo_Nodo

                    If Nodo.ChildNodes.Count = 0 Then
                        RowCadena.Permiso = RowNodo.Path_Nodo
                    Else
                        RowCadena.Permiso = RowNodo.Path_Nodo & ".*"
                    End If
                    tblCadena.Rows.Add(RowCadena)
                End If
            Next

            gvCadenas.DataSource = tblCadena
            gvCadenas.DataBind()

        End Sub

        Private Sub LoadAcciones_Edit()
            LoadAcciones_New()
            clear_gvCadenas()

            'Verifica si el modulo chequeado ya existe mantiene los datos de los checkbox en esa fila 
            Try
                Dim RowPermiso As TBL_Perfil_PermisosRow
                Dim RowNodo As TBL_Modulo_MapaRow
                'Dim RowModulo As TBL_ModuloRow

                Dim i As Short = 0
                For Each Nodo As TreeNode In tvPermisos.CheckedNodes
                    If Nodo.Checked Then
                        Dim Partes() As String = Nodo.Value.Split("-"c)
                        RowNodo = tblModuloMapa.FindByfk_Moduloid_Nodo(CShort(Partes(1)), CShort(Partes(2)))
                        'RowModulo = tblModulo.FindByid_Modulo(RowNodo.fk_Modulo)

                        For Each RowPermiso In tblPerfilPermisos.Rows
                            If (RowPermiso.fk_Modulo = RowNodo.fk_Modulo) And (RowPermiso.fk_Nodo = RowNodo.id_Nodo) Then
                                '-----------Cargo los datos de las columnas checkbox------------ 
                                Dim chkConsultar As CheckBox
                                Dim chkAgregar As CheckBox
                                Dim chkEditar As CheckBox
                                Dim chkEliminar As CheckBox
                                Dim chkExportar As CheckBox
                                Dim Fila As GridViewRow = gvCadenas.Rows(i)
                                chkConsultar = CType(Fila.FindControl("chkConsultar"), CheckBox)
                                chkConsultar.Checked = RowPermiso.Consultar
                                chkAgregar = CType(Fila.FindControl("chkAgregar"), CheckBox)
                                chkAgregar.Checked = RowPermiso.Agregar
                                chkEditar = CType(Fila.FindControl("chkEditar"), CheckBox)
                                chkEditar.Checked = RowPermiso.Editar
                                chkEliminar = CType(Fila.FindControl("chkEliminar"), CheckBox)
                                chkEliminar.Checked = RowPermiso.Eliminar
                                chkExportar = CType(Fila.FindControl("chkExportar"), CheckBox)
                                chkExportar.Checked = RowPermiso.Exportar
                                i = CShort(i + 1)
                                Exit For
                            End If
                        Next
                    End If
                Next
            Catch ex As Exception
            End Try
        End Sub

        Private Sub clear_gvCadenas()
            '-----------Cargo los datos de las columnas checkbox------------
            Dim chkConsultar As CheckBox
            Dim chkAgregar As CheckBox
            Dim chkEditar As CheckBox
            Dim chkEliminar As CheckBox
            Dim chkExportar As CheckBox

            For Each Fila As GridViewRow In gvCadenas.Rows

                chkConsultar = CType(Fila.FindControl("chkConsultar"), CheckBox)
                chkConsultar.Checked = False
                chkAgregar = CType(Fila.FindControl("chkAgregar"), CheckBox)
                chkAgregar.Checked = False
                chkEditar = CType(Fila.FindControl("chkEditar"), CheckBox)
                chkEditar.Checked = False
                chkEliminar = CType(Fila.FindControl("chkEliminar"), CheckBox)
                chkEliminar.Checked = False
                chkExportar = CType(Fila.FindControl("chkExportar"), CheckBox)
                chkExportar.Checked = False

            Next
        End Sub

        Private Sub visible_controls(ByVal dato As Boolean)
            pnlDatos.Visible = dato
            tvPermisos.Visible = dato
            AsignarAccionesButton.Visible = dato
        End Sub

#End Region

#Region " Funciones "

        Private Function Validar() As Boolean
            Return True
        End Function

#End Region

    End Class

End Namespace