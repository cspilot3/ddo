Imports System.IO
Imports Miharu.Security._clases

Namespace _sitio.administracion.estructura

    Partial Public Class entidades
        Inherits FormBase

#Region " Declaraciones "

        Private Const MyPathPermiso As String = "1.1.2"

        Private tblBase As DBSecurity.SchemaConfig.TBL_EntidadDataTable
        Private tblEntidad As DBSecurity.SchemaConfig.TBL_EntidadDataTable
        Private tblGrupoEmpresarial As DBSecurity.SchemaConfig.TBL_Grupo_EmpresarialDataTable
        'Dependencias
        Private tblDependencia As DBSecurity.SchemaConfig.TBL_DependenciaDataTable
        'wucDependencias
        Private tblSedeFiltro As DBSecurity.SchemaConfig.TBL_SedeDataTable
        Private tblDependencia_SedeFiltro As DBSecurity.SchemaConfig.TBL_SedeDataTable
        Private tblSede_Dependencia As DBSecurity.SchemaConfig.TBL_Sede_DependenciaDataTable

        Private MyDiagrama As Web.Controls.Diagrama

        Private MaxId As Short
        Private BloqueId As Integer

#End Region

#Region " Eventos "

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            If Not MyBase.ValidarNavegacion(Me.GetType().BaseType.FullName, MyPathPermiso) Then Return

            If Not Me.IsPostBack Then
                Config_Page()
                DrawDiagrama()
                DeleteTempImages()
                ActivarOpciones(False, False)
            Else
                Load_Data()
            End If
        End Sub
        Private Sub Page_HijaClose() Handles Me.HijaClose
            ShowLogo()
            DrawDiagrama()        'Dependencias
            Load_SedeFiltro()

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

        Private Sub icbEdit_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs) Handles icbEdit.Click
            EditarNodo(CShort(SelectedBloque.Value))
            lblwucSede.Text = "1"
        End Sub

        Private Sub icbDelete_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs) Handles icbDelete.Click
            EliminarNodo(CShort(SelectedBloque.Value))
            lblwucSede.Text = "0"
        End Sub

        Private Sub icbAddAsistencial_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs) Handles icbAddAsistencial.Click
            AgregarNodo(CInt(SelectedBloque.Value), Web.Controls.Diagrama.EnumRelacion.ASISTENCIAL)
            lblwucSede.Text = "0"
        End Sub

        Private Sub icbAddSubordinado_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs) Handles icbAddSubordinado.Click
            AgregarNodo(CInt(SelectedBloque.Value), Web.Controls.Diagrama.EnumRelacion.SUBORDINACION)
            lblwucSede.Text = "0"
        End Sub

        Private Sub btnDatosAceptar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDatosAceptar.Click
            ActualizarNodo(CShort(lblCodNodo.Text))
            Accept_Sedes()                   'sedes seleccionadas por dependencia creada
            lblwucSede.Text = "0"
        End Sub
        Private Sub btnDatosCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDatosCancelar.Click
            lblwucSede.Text = "0"
        End Sub

        Private Sub btnBuscarImagen_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs) Handles btnBuscarImagen.Click
            BuscarImagen()
        End Sub

#End Region

#Region " Metodos "

        Private Sub Config_Page()
            tblBase = New DBSecurity.SchemaConfig.TBL_EntidadDataTable
            Me.MySesion.Pagina.Parameter("tblBase") = tblBase

            tblEntidad = New DBSecurity.SchemaConfig.TBL_EntidadDataTable
            Me.MySesion.Pagina.Parameter("tblEntidad") = tblEntidad

            tblGrupoEmpresarial = New DBSecurity.SchemaConfig.TBL_Grupo_EmpresarialDataTable
            Me.MySesion.Pagina.Parameter("tblGrupoEmpresarial") = tblGrupoEmpresarial

            Const FileName As String = "templogo.bmp"

            Me.MySesion.Pagina.Parameter("TempFileNameURL") = "~/_images/logo_cliente/" & FileName
            Me.MySesion.Pagina.Parameter("TempFileNamePath") = Server.MapPath(CStr(Me.MySesion.Pagina.Parameter("TempFileNameURL")))

            ' Load grupos empresariales
            Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)

            Try
                dbmSecurity.Connection_Open(MySesion.Usuario.id)

                If MySesion.Usuario.isRoot Then
                    tblGrupoEmpresarial = dbmSecurity.SchemaConfig.TBL_Grupo_Empresarial.DBGet(Nothing)
                Else
                    tblGrupoEmpresarial = dbmSecurity.SchemaConfig.TBL_Grupo_Empresarial.DBGet(MySesion.Entidad.idGrupo)
                End If
            Catch ex As Exception
                Master.ShowAlert(ex.Message, MiharuMasterForm.MsgBoxIcon.IconError)
            Finally
                dbmSecurity.Connection_Close()
            End Try

            ShowGrupos()

            'Dependencias--
            tblDependencia = New DBSecurity.SchemaConfig.TBL_DependenciaDataTable
            Me.MySesion.Pagina.Parameter("tblDependencia") = tblDependencia

            'wucDependencias--
            tblSedeFiltro = New DBSecurity.SchemaConfig.TBL_SedeDataTable
            Me.MySesion.Pagina.Parameter("tblSedeFiltro") = tblSedeFiltro

            tblDependencia_SedeFiltro = New DBSecurity.SchemaConfig.TBL_SedeDataTable
            Me.MySesion.Pagina.Parameter("tblDependencia_SedeFiltro") = tblDependencia_SedeFiltro

            tblSede_Dependencia = New DBSecurity.SchemaConfig.TBL_Sede_DependenciaDataTable
            Me.MySesion.Pagina.Parameter("tblSede_Dependencia") = tblSede_Dependencia

            MyDiagrama = New Web.Controls.Diagrama
            Me.MySesion.Pagina.Parameter("Diagrama") = MyDiagrama


            MaxId = 0
            Me.MySesion.Pagina.Parameter("MaxId") = MaxId

            BloqueId = 0
            Me.MySesion.Pagina.Parameter("BloqueId") = BloqueId
            '-------------------------------------------
            Load_SedeFiltro()
        End Sub
        Private Sub Load_Data()

            tblBase = CType(Me.MySesion.Pagina.Parameter("tblBase"), DBSecurity.SchemaConfig.TBL_EntidadDataTable)
            tblEntidad = CType(Me.MySesion.Pagina.Parameter("tblEntidad"), DBSecurity.SchemaConfig.TBL_EntidadDataTable)
            tblGrupoEmpresarial = CType(Me.MySesion.Pagina.Parameter("tblGrupoEmpresarial"), DBSecurity.SchemaConfig.TBL_Grupo_EmpresarialDataTable)
            'Dependencias---
            tblDependencia = CType(Me.MySesion.Pagina.Parameter("tblDependencia"), DBSecurity.SchemaConfig.TBL_DependenciaDataTable)
            'wucDependencias
            tblSedeFiltro = CType(Me.MySesion.Pagina.Parameter("tblSedeFiltro"), DBSecurity.SchemaConfig.TBL_SedeDataTable)
            tblDependencia_SedeFiltro = CType(Me.MySesion.Pagina.Parameter("tblDependencia_SedeFiltro"), DBSecurity.SchemaConfig.TBL_SedeDataTable)
            tblSede_Dependencia = CType(Me.MySesion.Pagina.Parameter("tblSede_Dependencia"), DBSecurity.SchemaConfig.TBL_Sede_DependenciaDataTable)

            MyDiagrama = CType(Me.MySesion.Pagina.Parameter("Diagrama"), Web.Controls.Diagrama)

            MaxId = CShort(Me.MySesion.Pagina.Parameter("MaxId"))
            BloqueId = CInt(Me.MySesion.Pagina.Parameter("BloqueId"))
            ' lblwucSede.Text = "0"

        End Sub
        Private Sub Page_LoadComplete(ByVal sender As Object, ByVal e As EventArgs) Handles Me.LoadComplete
            If lblwucSede.Text = "1" Then
                DrawDiagrama()
                ModalPopupDatos.Show()
            End If
        End Sub
        Private Sub Buscar(ByVal nParametro As String)
            gvBase.SelectedIndex = -1

            If nParametro <> "" Then
                Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)

                Try
                    dbmSecurity.Connection_Open(MySesion.Usuario.id)
                    tblBase.Clear()
                    dbmSecurity.SchemaConfig.TBL_Entidad.DBFillByNombre_Entidad(tblBase, nParametro)

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
            ' pnlDependencia.Visible = False
        End Sub
        Private Sub Visualizar_Resultados()
            gvBase.DataSource = tblBase
            gvBase.DataBind()
        End Sub
        Private Sub ClearForm()
            tblEntidad.Rows.Clear()
            tblDependencia.Rows.Clear()

            lblCodEntidad.Text = "-1"
            ddlGrupo.SelectedIndex = -1
            txtNombre.Text = ""
            txtCodigo.Text = ""
            txtNIT.Text = ""
            txtContacto.Text = ""
            txtTelefono.Text = ""
        End Sub
        Private Sub NuevoRegistro()
            ClearForm()
            DeleteTempImages()
            ShowLogo()

            tcBase.ActiveTabIndex = 1
            tcDetalle.ActiveTabIndex = 0

            pnlDetalle.Visible = True
            'pnlDependencia.Visible = True

            txtNombre.Focus()
            chkActivo.Checked = True
            chkActivo.Enabled = False
            lblwucSede.Text = "0"
            lblnewNodo.Text = "0"
            Me.ActivarOpciones(True, True)
            'si es nuevo dibuja nodo raiz--
            Nueva_Dependencia(-1)
            InsertItems()
            DrawDiagrama()
            '-------------------

        End Sub
        Private Sub EditarRegistro()
            Dim RowBase As DBSecurity.SchemaConfig.TBL_EntidadRow
            Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)
            Try
                ClearForm()
                tcBase.ActiveTabIndex = 1

                pnlDetalle.Visible = True
                txtNombre.Focus()
                Me.ActivarOpciones(True, False)

                RowBase = CType(tblBase.Rows(gvBase.SelectedRow.DataItemIndex), DBSecurity.SchemaConfig.TBL_EntidadRow)

                ' Data
                lblCodEntidad.Text = CStr(RowBase.id_Entidad)
                ddlGrupo.SelectedValue = CStr(RowBase.fk_Grupo_Empresarial)
                txtNombre.Text = RowBase.Nombre_Entidad
                txtCodigo.Text = RowBase.Codigo_Entidad
                txtNIT.Text = RowBase.NIT_Entidad
                txtContacto.Text = RowBase.Contacto_Entidad
                txtTelefono.Text = RowBase.Telefono_Entidad
                chkActivo.Checked = RowBase.Activo

                tblEntidad.Rows.Clear()
                tblEntidad.Rows.Add(RowBase.ItemArray)
                tblEntidad.AcceptChanges()

                ' Logo
                Dim FileName As String = "~/_images/logo_cliente/entidad-" & lblCodEntidad.Text & ".bmp"

                If File.Exists(Server.MapPath(FileName)) Then
                    File.Delete(CStr(Me.MySesion.Pagina.Parameter("TempFileNamePath")))
                    File.Copy(Server.MapPath(FileName), CStr(Me.MySesion.Pagina.Parameter("TempFileNamePath")))
                End If

                ShowLogo()
                'Dependencias----Cargar estructura
                dbmSecurity.Connection_Open(MySesion.Usuario.id)

                Dim ordenar As New DBSecurity.SchemaConfig.TBL_DependenciaEnumList(DBSecurity.SchemaConfig.TBL_DependenciaEnum.fk_Padre, True)
                tblDependencia = dbmSecurity.SchemaConfig.TBL_Dependencia.DBGet(RowBase.id_Entidad, Nothing, 0, ordenar)

                If tblDependencia.Rows.Count = 0 Then
                    Nueva_Dependencia(RowBase.id_Entidad)
                End If

                InsertItems()
                DrawDiagrama()
                'nombre entidad
                lblNombreEntidad.Text = txtNombre.Text


            Catch ex As Exception
                Master.ShowAlert(ex.Message, MiharuMasterForm.MsgBoxIcon.IconError)
            Finally
                dbmSecurity.Connection_Close()
            End Try
        End Sub
        Private Sub GuardarCambios()
            If Validar() Then
                Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)
                Dim RowEntidad As DBSecurity.SchemaConfig.TBL_EntidadRow
                Dim isNuevo As Boolean = False

                Try
                    dbmSecurity.Connection_Open(MySesion.Usuario.id)
                    dbmSecurity.Transaction_Begin()
                    'dbmSecurity.SchemaConfig.()
                    'dbmSecurity.DataBase.Identifier_Date_Format = Program.IdentifierDateFormat

                    If lblCodEntidad.Text = "-1" Then
                        isNuevo = True
                        lblCodEntidad.Text = CStr(dbmSecurity.SchemaConfig.TBL_Entidad.DBNextId)

                        RowEntidad = tblEntidad.NewTBL_EntidadRow
                        RowEntidad.id_Entidad = CShort(lblCodEntidad.Text)
                        '-Si es una nueva entidad actualiza el id de la nueva entidad en las dependencias
                        For i As Integer = 0 To tblDependencia.Rows.Count - 1
                            tblDependencia.Rows(i).Item("fk_Entidad") = lblCodEntidad.Text
                        Next
                        '----
                    Else
                        RowEntidad = tblEntidad.FindByid_Entidad(CShort(lblCodEntidad.Text))
                    End If

                    RowEntidad.fk_Grupo_Empresarial = CShort(ddlGrupo.SelectedValue)
                    RowEntidad.Nombre_Entidad = txtNombre.Text
                    RowEntidad.Codigo_Entidad = txtCodigo.Text
                    RowEntidad.NIT_Entidad = txtNIT.Text
                    RowEntidad.Contacto_Entidad = txtContacto.Text
                    RowEntidad.Telefono_Entidad = txtTelefono.Text
                    RowEntidad.Activo = chkActivo.Checked
                    'RowEntidad.fk_Entidad_Log = MySesion.Entidad.id
                    RowEntidad.fk_Usuario_Log = MySesion.Usuario.id
                    RowEntidad.Eliminado = False
                    RowEntidad.Fecha_log = Now

                    If isNuevo Then
                        tblEntidad.Rows.Add(RowEntidad)
                    End If
                    'Entidad---
                    dbmSecurity.SchemaConfig.TBL_Entidad.DBSaveTable(tblEntidad)

                    ' Logo
                    Dim FileName As String = Server.MapPath("~/_images/logo_cliente/entidad-" & lblCodEntidad.Text & ".bmp")

                    If File.Exists(CStr(Me.MySesion.Pagina.Parameter("TempFileNamePath"))) Then
                        If File.Exists(FileName) Then File.Delete(FileName)
                        File.Copy(CStr(Me.MySesion.Pagina.Parameter("TempFileNamePath")), FileName)
                        File.Delete(CStr(Me.MySesion.Pagina.Parameter("TempFileNamePath")))
                    End If

                    'Dependencias----
                    dbmSecurity.SchemaConfig.TBL_Dependencia.DBSaveTable(tblDependencia)
                    'Save Sedes
                    If Not isNuevo Then
                        'vDependencia() = Guarda las dependencias que fueron agregadas o editadas
                        Dim vDependencia() As DataRow = tblSede_Dependencia.Select("fk_Entidad = " & lblCodEntidad.Text)
                        For i As Integer = 0 To vDependencia.Length - 1
                            dbmSecurity.SchemaConfig.TBL_Sede_Dependencia.DBDelete(CShort(lblCodEntidad.Text), CShort(vDependencia(i).Item("fk_Sede")), CShort(vDependencia(i).Item("fk_Dependencia")))
                        Next
                        ''valida->cuando no existen sedes retorna una fila cero
                        'ElseIf tblSede_Dependencia.Rows(0).Item("fk_Entidad").ToString <> "0" Then
                        '    dbmSecurity.SchemaConfig.TBL_Sede_Dependencia.DBSaveTable(tblSede_Dependencia)
                    End If
                    dbmSecurity.Transaction_Commit()

                    Me.Master.ShowAlert("Los datos se almacenaron correctamente", MiharuMasterForm.MsgBoxIcon.IconInformation)

                    Buscar(ucFiltro.Parametro)

                    ActivarOpciones(True, False)
                    pnlDetalle.Visible = True
                    'pnlDependencia.Visible = True
                    tcBase.ActiveTabIndex = 1


                Catch ex As Exception
                    dbmSecurity.Transaction_Rollback()
                    Master.ShowAlert(ex.Message, MiharuMasterForm.MsgBoxIcon.IconError)

                    If isNuevo Then
                        lblCodEntidad.Text = "-1"
                        tblEntidad.RejectChanges()
                    End If
                Finally
                    dbmSecurity.Connection_Close()
                End Try
            End If
            'Dependecias
            DrawDiagrama()
        End Sub
        Private Sub EliminarRegistro()
            Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)

            Try
                dbmSecurity.Connection_Open(MySesion.Usuario.id)

                'Dependencias
                tblDependencia = dbmSecurity.SchemaConfig.TBL_Dependencia.DBGet(CShort(lblCodEntidad.Text), Nothing)

                For j As Integer = 0 To tblDependencia.Rows.Count - 1
                    dbmSecurity.SchemaConfig.TBL_Dependencia.DBDelete(CShort(lblCodEntidad.Text), CShort(tblDependencia.Rows(j).Item("id_Dependencia")))
                    'Sede_Dependencia
                    dbmSecurity.SchemaConfig.TBL_Sede_Dependencia.DBDelete(CShort(lblCodEntidad.Text), Nothing, CShort(tblDependencia.Rows(j).Item("id_Dependencia")))
                Next

                'Entidad
                dbmSecurity.SchemaConfig.TBL_Entidad.DBDelete(CShort(lblCodEntidad.Text))

                tcBase.ActiveTabIndex = 0
                Buscar(ucFiltro.Parametro)

                Me.Master.ShowAlert("El registro se eliminó exitosamente", MiharuMasterForm.MsgBoxIcon.IconInformation)

                ClearForm()

                pnlDetalle.Visible = False
                'pnlDependencia.Visible = False
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
        Private Sub DeleteTempImages()
            Dim FileNames() As String
            Dim FileName As String

            Try
                FileNames = Directory.GetFiles(Server.MapPath("~/_images/logo_cliente"), "templogo*")

                For Each FileName In FileNames
                    Try : File.Delete(FileName) : Catch : End Try
                Next
            Catch

            End Try

        End Sub
        Private Sub BuscarImagen()
            Master.ShowDialog("administracion/estructura/p_endtidades_logo.aspx", "PopUpFile", "Cargar Logo", "580", "200", "100", "100", False)
        End Sub
        Private Sub ShowLogo()
            ' Logo
            Dim FileName As String = "~/_images/logo_cliente/templogo-" & Format(Now, "hhmmss") & ".bmp"

            If File.Exists(CStr(Me.MySesion.Pagina.Parameter("TempFileNamePath"))) Then

                File.Copy(CStr(Me.MySesion.Pagina.Parameter("TempFileNamePath")), Server.MapPath(FileName))
            End If

            imgLogo.ImageUrl = FileName
        End Sub
        Private Sub ShowGrupos()
            ddlGrupo.DataSource = tblGrupoEmpresarial
            ddlGrupo.DataValueField = "id_Grupo_Empresarial"
            ddlGrupo.DataTextField = "Nombre_Grupo_Empresarial"
            ddlGrupo.DataBind()
        End Sub
        'Dependencias---------------------
        Private Sub DrawDiagrama()
            Dim Ancho As Integer
            Dim Bloques As List(Of Web.Controls.Bloque)
            Dim Lineas As List(Of Web.Controls.Linea)

            Me.pnlBase.Controls.Clear()

            Ancho = MyDiagrama.getAncho

            If Ancho < pnlBase.Width.Value Then
                MyDiagrama.ConfigPosiciones(CInt(pnlBase.Width.Value / 2), 0)
            Else
                MyDiagrama.ConfigPosiciones(CInt(Ancho / 2), 0)
            End If

            Bloques = MyDiagrama.getBloques()


            For Each B As Web.Controls.Bloque In Bloques
                B.EnableViewState = False
                Me.pnlBase.Controls.Add(B)
            Next

            Lineas = MyDiagrama.getLineas()


            For Each L As Web.Controls.Linea In Lineas
                L.EnableViewState = False
                Me.pnlBase.Controls.Add(L)
            Next
        End Sub
        Private Sub InsertItems()
            Dim Nodo As Web.Controls.DiagramaNodo

            MyDiagrama.Clear()

            For Each RowDependencia As DBSecurity.SchemaConfig.TBL_DependenciaRow In tblDependencia.Rows
                If MaxId < RowDependencia.id_Dependencia Then MaxId = RowDependencia.id_Dependencia

                If RowDependencia.RowState <> DataRowState.Deleted Then
                    Nodo = MyDiagrama.NewNodo
                    Nodo.Etiqueta = RowDependencia.Nombre_Dependencia
                    Nodo.Codigo = RowDependencia.Codigo_Dependencia

                    If RowDependencia.fk_Padre = 0 Then
                        MyDiagrama.Add(RowDependencia.id_Dependencia, Nodo)
                    Else
                        MyDiagrama.Add(RowDependencia.id_Dependencia, Nodo, RowDependencia.fk_Padre, CType(RowDependencia.fk_Nivel, Web.Controls.Diagrama.EnumRelacion))
                    End If
                End If
            Next

            Me.MySesion.Pagina.Parameter("MaxId") = MaxId

        End Sub
        Private Sub AgregarNodo(ByVal nPadre As Integer, ByVal nTipo As Web.Controls.Diagrama.EnumRelacion)
            If MyDiagrama.getNodo(nPadre).Relacion = Web.Controls.Diagrama.EnumRelacion.ASISTENCIAL Then
                Master.ShowAlert("No se puede agregar nodos a una nodo asistencial", MiharuMasterForm.MsgBoxIcon.IconWarning)
            Else
                Dim RowDependencia As DBSecurity.SchemaConfig.TBL_DependenciaRow

                MaxId += CShort(1)
                Me.MySesion.Pagina.Parameter("MaxId") = MaxId

                RowDependencia = tblDependencia.NewTBL_DependenciaRow

                RowDependencia.fk_Entidad = CShort(lblCodEntidad.Text)
                RowDependencia.id_Dependencia = MaxId
                RowDependencia.fk_Padre = CShort(nPadre)
                RowDependencia.Nombre_Dependencia = ""
                RowDependencia.Codigo_Dependencia = ""
                RowDependencia.Centro_Costo = ""
                RowDependencia.Direccion_Dependencia = ""
                RowDependencia.Telefono_Dependencia = ""
                RowDependencia.Ubicacion_Dependencia = ""
                RowDependencia.Centro_Costo = ""
                lblnewNodo.Text = "1"
                RowDependencia.Activo = True
                RowDependencia.Eliminado = False
                RowDependencia.fk_Nivel = CByte(nTipo)
                'RowDependencia.fk_Entidad_Log = MySesion.Entidad.id
                RowDependencia.fk_Usuario_Log = MySesion.Usuario.id
                RowDependencia.Fecha_log = Now

                tblDependencia.Rows.Add(RowDependencia)

                Dim Nodo As Web.Controls.DiagramaNodo

                Nodo = MyDiagrama.NewNodo
                Nodo.Etiqueta = RowDependencia.Nombre_Dependencia
                Nodo.Codigo = RowDependencia.Codigo_Dependencia

                MyDiagrama.Add(RowDependencia.id_Dependencia, Nodo, RowDependencia.fk_Padre, nTipo)
            End If
            DrawDiagrama()
        End Sub
        Private Sub EditarNodo(ByVal nId As Short)
            Dim RowDependencia As DBSecurity.SchemaConfig.TBL_DependenciaRow = tblDependencia.FindByfk_Entidadid_Dependencia(CShort(lblCodEntidad.Text), nId)

            lblCodNodo.Text = CStr(RowDependencia.id_Dependencia)
            txtNombreDependencia.Text = RowDependencia.Nombre_Dependencia
            txtCodDependencia.Text = RowDependencia.Codigo_Dependencia
            txtCentroCosto.Text = RowDependencia.Centro_Costo
            txtDireccion.Text = RowDependencia.Direccion_Dependencia
            txtTelefonoDep.Text = RowDependencia.Telefono_Dependencia
            txtUbicacion.Text = RowDependencia.Ubicacion_Dependencia
            chk_ActivoDep.Checked = RowDependencia.Activo
            'wucSedes----------------------------------------
            If lblnewNodo.Text = "1" Then
                New_SedeFiltro()
            Else
                Edit_SedeFiltro()
            End If
            '------------------------------------------------
            txtNombre.Focus()
            ModalPopupDatos.Show()
            lblnewNodo.Text = "0"
            DrawDiagrama()

        End Sub
        Private Sub EliminarNodo(ByVal nId As Short)
            If tblDependencia.Rows.Count > 1 Then
                Dim RowDependencia As DBSecurity.SchemaConfig.TBL_DependenciaRow

                RowDependencia = tblDependencia.FindByfk_Entidadid_Dependencia(CShort(lblCodEntidad.Text), nId)

                RowDependencia.fk_Usuario_Log = MySesion.Usuario.id
                RowDependencia.Eliminado = True                           'Cambia el estado ha eliminado
                RowDependencia.Fecha_log = Now

                MyDiagrama.Remove(nId)
            Else
                Master.ShowAlert("No se puede eliminar el nodo raíz", MiharuMasterForm.MsgBoxIcon.IconWarning)
            End If

            DrawDiagrama()
        End Sub
        Private Sub ActualizarNodo(ByVal nId As Short)
            Dim RowDependencia As DBSecurity.SchemaConfig.TBL_DependenciaRow = tblDependencia.FindByfk_Entidadid_Dependencia(CShort(lblCodEntidad.Text), nId)
            Dim Nodo As Web.Controls.DiagramaNodo = MyDiagrama.getNodo(nId)

            Nodo.Codigo = txtCodDependencia.Text
            Nodo.Etiqueta = txtNombreDependencia.Text

            RowDependencia.id_Dependencia = CShort(lblCodNodo.Text)
            RowDependencia.Nombre_Dependencia = txtNombreDependencia.Text
            RowDependencia.Codigo_Dependencia = txtCodDependencia.Text
            RowDependencia.Centro_Costo = txtCentroCosto.Text
            RowDependencia.Direccion_Dependencia = txtDireccion.Text
            RowDependencia.Telefono_Dependencia = txtTelefonoDep.Text
            RowDependencia.Ubicacion_Dependencia = txtUbicacion.Text
            RowDependencia.fk_Usuario_Log = MySesion.Usuario.id
            RowDependencia.Eliminado = False
            RowDependencia.Fecha_log = Now
            RowDependencia.Activo = chk_ActivoDep.Checked

            ModalPopupDatos.Hide()

            DrawDiagrama()

        End Sub
        Private Sub Nueva_Dependencia(ByVal id_Entidad As Short)
            Dim RowDependencia As DBSecurity.SchemaConfig.TBL_DependenciaRow

            RowDependencia = tblDependencia.NewTBL_DependenciaRow

            RowDependencia.fk_Entidad = id_Entidad
            RowDependencia.id_Dependencia = 1
            RowDependencia.fk_Padre = 0
            RowDependencia.fk_Nivel = 1
            RowDependencia.Nombre_Dependencia = "Gerencia"
            RowDependencia.Codigo_Dependencia = "001"
            RowDependencia.Direccion_Dependencia = ""
            RowDependencia.Telefono_Dependencia = ""
            RowDependencia.Ubicacion_Dependencia = ""
            RowDependencia.Centro_Costo = ""
            RowDependencia.Activo = True
            RowDependencia.Eliminado = False
            RowDependencia.Fecha_log = Now
            'RowDependencia.fk_Entidad_Log = MySesion.Entidad.id
            RowDependencia.fk_Usuario_Log = MySesion.Usuario.id

            tblDependencia.Rows.Add(RowDependencia)

        End Sub
        'Popup wucDependencias-----------
        Private Sub Load_SedeFiltro()
            Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)
            Try
                dbmSecurity.Connection_Open(MySesion.Usuario.id)
                tblSedeFiltro.Clear()
                dbmSecurity.SchemaConfig.TBL_Sede_Dependencia.DBFill(tblSedeFiltro, CShort(0), Nothing, Nothing)
                'dbmSecurity.SchemaConfig.Sede_DependenciaSedeget(tblSedeFiltro, CShort(0), Nothing, True)
                'carga las sedes en el web control
                Delete_Columns(tblSedeFiltro)
                wucDependencias.tbl_in = tblSedeFiltro
                wucDependencias.tbl_in_aux = tblSedeFiltro
                'tcDetalle.Enabled = False
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

                tblDependencia_SedeFiltro = dbmSecurity.SchemaConfig.TBL_Sede.DBGet(CShort(lblCodEntidad.Text), Nothing)
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

                tblDependencia_SedeFiltro = dbmSecurity.SchemaConfig.TBL_Sede.DBGet(CShort(lblCodEntidad.Text), CShort(lblCodNodo.Text))
                tblSedeFiltro = dbmSecurity.SchemaConfig.TBL_Sede.DBGet(CShort(lblCodEntidad.Text), CShort(lblCodNodo.Text))

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
        Private Sub Accept_Sedes()
            Dim tbl As DataTable = wucDependencias.tbl_out
            For i As Integer = 0 To tbl.Rows.Count - 1
                Dim RowSede_Dependencia As DBSecurity.SchemaConfig.TBL_Sede_DependenciaRow
                RowSede_Dependencia = tblSede_Dependencia.NewTBL_Sede_DependenciaRow
                RowSede_Dependencia.fk_Entidad = CShort(tbl.Rows(i).Item("fk"))         'fk entidad
                RowSede_Dependencia.fk_Sede = CShort(tbl.Rows(i).Item("id"))            'fk_Sede
                RowSede_Dependencia.fk_Dependencia = CShort(lblCodNodo.Text)            'fk_Dependencia
                RowSede_Dependencia.fk_Usuario_Log = MySesion.Usuario.id
                RowSede_Dependencia.Eliminado = False
                RowSede_Dependencia.Fecha_log = Now
                tblSede_Dependencia.Rows.Add(RowSede_Dependencia)
            Next
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