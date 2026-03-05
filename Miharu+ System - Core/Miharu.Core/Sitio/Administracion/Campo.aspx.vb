Imports Miharu.Core.Clases
Imports DBCore
Imports Slyg.Tools

Namespace Sitio.Administracion

    Public Class Campo
        Inherits FormBase

#Region "Declaraciones"
        Private Const MyPathPermiso As String = "1.4.4"
        Public nEntidad As Short
        Dim container As Object
        Public Esquema As String = "config"
        Public Tabla As String = "TBL_Campo"

        Private EntidadDateTable As SchemaSecurity.CTA_EntidadDataTable
        Private ProyectoDateTable As SchemaConfig.TBL_ProyectoDataTable
        Private EsquemaDateTable As SchemaConfig.TBL_EsquemaDataTable
        Private DocumentoDateTable As SchemaConfig.TBL_DocumentoDataTable
        Private Campo_TipoDataTable As SchemaConfig.TBL_Campo_TipoDataTable
        Private Campo_ListaDataTable As SchemaConfig.TBL_Campo_ListaDataTable
        Private Campo_BusquedaDataTable As SchemaConfig.TBL_Campo_BusquedaDataTable
#End Region

#Region "Eventos"

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            container = pnlDetalle
            nEntidad = MySesion.Entidad.id

            If Not Me.IsPostBack Then
                Config_Page()
            Else
                Load_Data()
            End If
        End Sub

        Protected Sub TablaAsodiadaImageButton_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles TablaAsodiadaImageButton.Click
            If fk_Campo_Tipo.SelectedValue = "9" Then
                Dim parameter As New ParameterCollection
                parameter.Add("fk_Entidad", fk_Entidad.SelectedValue)
                parameter.Add("fk_Documento", fk_Documento.SelectedValue)
                parameter.Add("fk_Campo", id_Campo.Text)
                GlobalParameterCollection = parameter

                OpenModalDialog("Save", "../sitio/Administracion/P_Tabla_Asociada.aspx", "msgTablaAsociada", "Campos Tabla Asociada", 800, 500)
            Else
                MyMasterPage.ShowMessage("El campo debe ser de tipo Tabla asociada para configurarle campos", MsgBoxIcon.IconWarning)
            End If
        End Sub

        Private Sub ddlEntidad_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEntidad.SelectedIndexChanged
            Load_Proyectos(CShort(ddlEntidad.SelectedValue), False)
            Load_Esquemas(CShort(ddlProyecto.SelectedValue), CShort(ddlEntidad.SelectedValue), False)
            Load_Documentos(CShort(ddlEntidad.SelectedValue), CShort(ddlProyecto.SelectedValue), CShort(ddlEsquema.SelectedValue), False)
        End Sub

        Private Sub ddlProyecto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlProyecto.SelectedIndexChanged
            Load_Esquemas(CShort(ddlProyecto.SelectedValue), CShort(ddlEntidad.SelectedValue), False)
        End Sub

        Private Sub ddlEsquema_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEsquema.SelectedIndexChanged
            Load_Documentos(CShort(ddlEntidad.SelectedValue), CShort(ddlProyecto.SelectedValue), CShort(ddlEsquema.SelectedValue), False)
        End Sub

        Protected Sub Es_Campo_Busqueda_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Es_Campo_Busqueda.CheckedChanged
            Try
                If Es_Campo_Busqueda.Checked Then
                    OpenModalDialog("Save", "../sitio/Administracion/p_Campo.aspx?fk_Entidad=" & fk_Entidad.SelectedValue & "&fk_Campo_Tipo=" & fk_Campo_Tipo.SelectedValue, "msgBusqueda", "Campo Búsqueda", 600, 350)
                Else
                    fk_Campo_Busqueda.Text = ""
                    fk_Campo_Busqueda.Visible = False
                End If
            Catch ex As Exception
                showErrorPage(ex)
            End Try
        End Sub

        Private Sub Campo_ModalClosed(ByVal parameters As String) Handles Me.ModalClosed
            Try
                Select Case ModalDialogNombre
                    Case "msgBusqueda"
                        If CDbl(MySesion.Pagina.Parameter("RESPUESTA")) > -1 Then
                            fk_Campo_Busqueda.Text = CInt(MySesion.Pagina.Parameter("RESPUESTA")).ToString()
                            fk_Campo_Busqueda.Visible = True
                            MyMasterPage.MasterDetailPanel.Update()
                        End If
                End Select
            Catch ex As Exception
                showErrorPage(ex)
            End Try
        End Sub

        Private Sub fk_Entidad_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles fk_Entidad.SelectedIndexChanged
            Load_Proyectos(CShort(fk_Entidad.SelectedValue), True)
            Load_Esquemas(CShort(fk_proyecto.SelectedValue), CShort(fk_Entidad.SelectedValue), True)
            Load_Documentos(CShort(fk_Entidad.SelectedValue), CShort(fk_proyecto.SelectedValue), CShort(fk_esquema.SelectedValue), True)
        End Sub

        Private Sub fk_Proyecto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles fk_proyecto.SelectedIndexChanged
            Load_Esquemas(CShort(fk_proyecto.SelectedValue), CShort(fk_Entidad.SelectedValue), True)
        End Sub

        Private Sub fk_Esquema_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles fk_esquema.SelectedIndexChanged
            Load_Documentos(CShort(fk_Entidad.SelectedValue), CShort(fk_proyecto.SelectedValue), CShort(fk_esquema.SelectedValue), True)
        End Sub

#End Region

#Region "Métodos"
        Private Sub Config_Page()
            Dim dmCore As New DBCoreDataBaseManager(MyBase.ConnectionString.Core)
            dmCore.Connection_Open(MySesion.Usuario.id)

            Try
                Dim imgB As ImageButton = CType(MyMasterPage.ToolControl.FindControl("imgSave"), ImageButton)
                imgB.ValidationGroup = "Guardar"
                Session("TableForm") = DataDictionary(Esquema, Tabla)

                'Almacenar en memoria los datos
                EntidadDateTable = New SchemaSecurity.CTA_EntidadDataTable
                Me.MySesion.Pagina.Parameter("EntidadDateTable") = EntidadDateTable

                ProyectoDateTable = New SchemaConfig.TBL_ProyectoDataTable
                Me.MySesion.Pagina.Parameter("ProyectoDateTable") = ProyectoDateTable

                EsquemaDateTable = New SchemaConfig.TBL_EsquemaDataTable
                Me.MySesion.Pagina.Parameter("EsquemaDateTable") = EsquemaDateTable

                DocumentoDateTable = New SchemaConfig.TBL_DocumentoDataTable
                Me.MySesion.Pagina.Parameter("DocumentoDateTable") = DocumentoDateTable

                Campo_TipoDataTable = New SchemaConfig.TBL_Campo_TipoDataTable
                Me.MySesion.Pagina.Parameter("Campo_TipoDataTable") = Campo_TipoDataTable

                Campo_ListaDataTable = New SchemaConfig.TBL_Campo_ListaDataTable
                Me.MySesion.Pagina.Parameter("Campo_ListaDataTable") = Campo_ListaDataTable

                Campo_BusquedaDataTable = New SchemaConfig.TBL_Campo_BusquedaDataTable
                Me.MySesion.Pagina.Parameter("Campo_BusquedaDataTable") = Campo_BusquedaDataTable

                'Cargar las Entidades
                dmCore.SchemaSecurity.CTA_Entidad.DBFill(EntidadDateTable, 0, New SchemaSecurity.CTA_EntidadEnumList(SchemaSecurity.CTA_EntidadEnum.Nombre_Entidad, True))

                'Cargar las Entidades
                ddlEntidad = LlenarDataddl(ddlEntidad, "id_Entidad", "Nombre_Entidad", EntidadDateTable)
                Load_Proyectos(CShort(ddlEntidad.SelectedValue), False)
                Load_Esquemas(CShort(ddlProyecto.SelectedValue), CShort(ddlEntidad.SelectedValue), False)
                Load_Documentos(CShort(ddlEntidad.SelectedValue), CShort(ddlProyecto.SelectedValue), CShort(ddlEsquema.SelectedValue), False)
            Catch ex As Exception
                showErrorPage(ex)
            End Try

            dmCore.Connection_Close()
        End Sub

        Private Sub Load_Data()
            EntidadDateTable = CType(Me.MySesion.Pagina.Parameter("EntidadDateTable"), SchemaSecurity.CTA_EntidadDataTable)
            ProyectoDateTable = CType(Me.MySesion.Pagina.Parameter("ProyectoDateTable"), SchemaConfig.TBL_ProyectoDataTable)
            EsquemaDateTable = CType(Me.MySesion.Pagina.Parameter("EsquemaDateTable"), SchemaConfig.TBL_EsquemaDataTable)
            DocumentoDateTable = CType(Me.MySesion.Pagina.Parameter("DocumentoDateTable"), SchemaConfig.TBL_DocumentoDataTable)
            Campo_TipoDataTable = CType(Me.MySesion.Pagina.Parameter("Campo_TipoDataTable"), SchemaConfig.TBL_Campo_TipoDataTable)
            Campo_ListaDataTable = CType(Me.MySesion.Pagina.Parameter("Campo_ListaDataTable"), SchemaConfig.TBL_Campo_ListaDataTable)
            Campo_BusquedaDataTable = CType(Me.MySesion.Pagina.Parameter("Campo_BusquedaDataTable"), SchemaConfig.TBL_Campo_BusquedaDataTable)
        End Sub

        Private Function adicionarOpSeleccionAddl(ByVal drp As DropDownList) As DropDownList
            Dim DropNuevo As DropDownList = drp
            Dim item As New ListItem("Seleccione ...", "-1")
            DropNuevo.Items.Insert(0, item)
            Return DropNuevo
        End Function

        Private Sub Load_Proyectos(ByVal id_Entidad As Short, ByVal Detalle As Boolean)
            Dim DBMMiharu As New DBCoreDataBaseManager(MyBase.ConnectionString.Core)

            Try
                DBMMiharu.Connection_Open(MySesion.Usuario.id)
                ProyectoDateTable.Clear()
                DBMMiharu.SchemaConfig.TBL_Proyecto.DBFillByfk_Entidad(ProyectoDateTable, id_Entidad)
            Catch ex As Exception
                showErrorPage(ex)
            Finally
                DBMMiharu.Connection_Close()
            End Try
            'Cargar los proyectos
            If Detalle Then
                fk_proyecto = LlenarDataddl(fk_proyecto, "id_Proyecto", "Nombre_Proyecto", ProyectoDateTable)
            Else
                ddlProyecto = LlenarDataddl(ddlProyecto, "id_Proyecto", "Nombre_Proyecto", ProyectoDateTable)
            End If
        End Sub

        Private Sub Load_Esquemas(ByVal id_Proyecto As Short, ByVal id_Entidad As Short, ByVal Detalle As Boolean)
            Dim DBMMiharu As New DBCoreDataBaseManager(MyBase.ConnectionString.Core)

            Try
                DBMMiharu.Connection_Open(MySesion.Usuario.id)
                EsquemaDateTable.Clear()
                DBMMiharu.SchemaConfig.TBL_Esquema.DBFillByfk_Entidadfk_Proyecto(EsquemaDateTable, id_Entidad, id_Proyecto)

            Catch ex As Exception
                showErrorPage(ex)
            Finally
                DBMMiharu.Connection_Close()
            End Try
            'Mostrar los esquemas
            If Detalle Then
                fk_esquema = LlenarDataddl(fk_esquema, "id_Esquema", "Nombre_Esquema", EsquemaDateTable)
            Else
                ddlEsquema = LlenarDataddl(ddlEsquema, "id_Esquema", "Nombre_Esquema", EsquemaDateTable)
            End If

        End Sub

        Private Sub Load_Documentos(ByVal id_Entidad As Short, ByVal id_Proyecto As Short, ByVal id_Esquema As Short, ByVal Detalle As Boolean)
            Dim DBMMiharu As New DBCoreDataBaseManager(MyBase.ConnectionString.Core)

            Try
                DBMMiharu.Connection_Open(MySesion.Usuario.id)
                DocumentoDateTable.Clear()
                DBMMiharu.SchemaConfig.TBL_Documento.DBFillByfk_Entidadfk_Proyectofk_Esquema(DocumentoDateTable, id_Entidad, id_Proyecto, id_Esquema)

            Catch ex As Exception
                showErrorPage(ex)
            Finally
                DBMMiharu.Connection_Close()
            End Try
            'Cargar los documentos
            If Detalle Then
                fk_Documento = LlenarDataddl(fk_Documento, "id_Documento", "Nombre_Documento", DocumentoDateTable)
            Else
                ddlDocumento = LlenarDataddl(ddlDocumento, "id_Documento", "Nombre_Documento", DocumentoDateTable)
            End If

        End Sub

        Private Sub LlenarGrillaCampos(ByVal CamposDataTable As DataTable)
            grdData.DataSource = CamposDataTable
            grdData.DataBind()
        End Sub

        Private Sub CargarDdlDetalle()

            If Campo_TipoDataTable.Rows.Count = 0 Then
                Dim DBMMiharu As New DBCoreDataBaseManager(MyBase.ConnectionString.Core)
                Try
                    DBMMiharu.Connection_Open(MySesion.Usuario.id)
                    Campo_TipoDataTable.Clear()

                    DBMMiharu.SchemaConfig.TBL_Campo_Tipo.DBFillByid_Campo_Tipo(Campo_TipoDataTable, Nothing)
                Catch ex As Exception
                    showErrorPage(ex)
                Finally
                    DBMMiharu.Connection_Close()
                End Try
            End If

            If Campo_ListaDataTable.Rows.Count = 0 Then
                Dim DBMMiharu As New DBCoreDataBaseManager(MyBase.ConnectionString.Core)
                Try
                    DBMMiharu.Connection_Open(MySesion.Usuario.id)
                    Campo_ListaDataTable.Clear()

                    DBMMiharu.SchemaConfig.TBL_Campo_Lista.DBFillByfk_Entidad(Campo_ListaDataTable, Nothing)
                Catch ex As Exception
                    showErrorPage(ex)
                Finally
                    DBMMiharu.Connection_Close()
                End Try
            End If

            'If Campo_BusquedaDataTable.Rows.Count = 0 Then
            '    Dim DBMMiharu As New DBCoreDataBaseManager(MyBase.ConnectionString.Core)
            '    Try
            '        DBMMiharu.Connection_Open(MySesion.Usuario.id)
            '        Campo_BusquedaDataTable.Clear()

            '        DBMMiharu.SchemaConfig.TBL_Campo_Busqueda_Entidad.DBFill(Campo_BusquedaDataTable, Nothing, Nothing, Nothing)
            '    Catch ex As Exception
            '        showErrorPage(ex)
            '    Finally
            '        DBMMiharu.Connection_Close()
            '    End Try
            'End If

            fk_Entidad = LlenarDataddl(fk_Entidad, "id_Entidad", "Nombre_Entidad", EntidadDateTable)
            fk_proyecto = LlenarDataddl(fk_proyecto, "id_Proyecto", "Nombre_Proyecto", ProyectoDateTable)
            fk_esquema = LlenarDataddl(fk_esquema, "id_Esquema", "Nombre_Esquema", EsquemaDateTable)
            fk_Documento = LlenarDataddl(fk_Documento, "id_Documento", "Nombre_Documento", DocumentoDateTable)
            fk_Campo_Tipo = LlenarDataddl(fk_Campo_Tipo, "id_Campo_Tipo", "Nombre_Campo_Tipo", Campo_TipoDataTable)
            fk_Campo_Lista = LlenarDataddl(fk_Campo_Lista, "id_Campo_Lista", "Nombre_Campo_Lista", Campo_ListaDataTable)
            'fk_Campo_Busqueda = LlenarDataddl(fk_Campo_Busqueda, "id_Campo_Busqueda", "Nombre_Campo_Busqueda", Campo_BusquedaDataTable)
        End Sub

#End Region

#Region "Funciones"

        Private Function LlenarDataddl(ByVal drp As DropDownList, ByVal value As String, ByVal text As String, ByVal Data As DataTable) As DropDownList
            drp.DataSource = Data
            drp.DataValueField = value
            drp.DataTextField = text
            drp.DataBind()
            Dim DropNew = adicionarOpSeleccionAddl(drp)
            Return DropNew
        End Function

#End Region

#Region "Automatic"

        Protected Sub FindAutomatic() Handles MyBase.CommandActionFind

            Dim query As String = "SELECT (convert(varchar(4), TE.id_Entidad) + '-' + TE.Nombre_Entidad) as Entidad, " & _
                                  "(convert(varchar(4), TP.id_Proyecto) + '-' + TP.Nombre_Proyecto) as Proyecto, " & _
                                  "(convert(varchar(4), ET.id_Esquema) + '-' + ET.Nombre_Esquema) as Esquema, " & _
                                  "(convert(varchar(4), D.id_Documento) + '-' + D.Nombre_Documento) as Documento, " & _
                                  "C.id_campo, C.Nombre_Campo, CT.Nombre_Campo_Tipo as Tipo_Campo, " & _
                                  "isNULL(TB.Nombre_Campo_Busqueda, '') as CampoBusqueda, D.id_Documento, " & _
                                  "C.fk_Entidad, C.fk_Documento, C.fk_Campo_Tipo,isNULL(C.fk_Campo_Lista, 0) as fk_Campo_Lista, " & _
                                  "C.Es_Campo_Busqueda, isNULL(C.fk_Campo_Busqueda,0) as fk_Campo_Busqueda, C.Es_Campo_Folder, " & _
                                  "C.Es_Obligatorio_Campo, C.Length_Campo, " & _
                                  "C.Es_Exportable, C.Eliminado_Campo " & _
                                  "FROM Config.TBL_Campo_Tipo CT, Config.TBL_Documento D,Config.TBL_Esquema ET, " & _
                                  "Config.TBL_Proyecto TP, security.CTA_Entidad TE,Config.TBL_Campo C " & _
                                  "LEFT OUTER JOIN Config.TBL_Campo_Busqueda TB " & _
                                  "ON TB.id_Campo_Busqueda = C.fk_Campo_Busqueda " & _
                                  "AND TB.fk_Campo_Tipo = C.fk_Campo_Tipo " & _
                                  "WHERE(C.fk_Campo_Tipo = CT.id_Campo_Tipo) " & _
                                  "AND C.fk_Documento = D.id_Documento " & _
                                  "AND D.fk_Esquema = ET.id_Esquema " & _
                                  "AND D.fk_Proyecto = ET.fk_Proyecto " & _
                                  "AND D.fk_Entidad = ET.fk_Entidad " & _
                                  "AND ET.fk_Proyecto = TP.id_Proyecto " & _
                                  "AND ET.fk_Entidad = TP.fk_Entidad " & _
                                  "AND TE.id_Entidad = TP.fk_Entidad "

            If ddlEntidad.SelectedValue <> "-1" Then
                query += " AND TE.id_Entidad = " & ddlEntidad.SelectedValue
            End If
            If ddlProyecto.SelectedValue <> "-1" Then
                query += " AND TP.id_Proyecto = " & ddlProyecto.SelectedValue
            End If
            If ddlEsquema.SelectedValue <> "-1" Then
                query += " AND ET.id_Esquema = " & ddlEsquema.SelectedValue
            End If
            If ddlDocumento.SelectedValue <> "-1" Then
                query += " AND D.id_Documento = " & ddlDocumento.SelectedValue
            End If


            Dim dbmCore As DBCoreDataBaseManager = Nothing

            Try
                dbmCore = New DBCoreDataBaseManager(ConnectionString.Core)

                Dim table As DataTable = dbmCore.DataBase.ExecuteQueryGet(query)
                LlenarGrillaCampos(table)
                GridData = table
                CurrentMasterTab = MasterTabType.Grid
            Catch ex As Exception
                Throw
            End Try

        End Sub

        Protected Sub btnGuardar_Click() Handles MyBase.CommandActionSave
            Try
                Dim dtTable As DataTable = CType(Session("TableForm"), DataTable)

                fk_Campo_Busqueda.Visible = True
                fk_Usuario_Log.Text = MySesion.Usuario.id
                Fecha_Log.Text = DateTime.Now.ToString(Program.getIdentifierDateFormat)
                If SaveType = SaveType.Insert Then CreateInsert(MySesion.Usuario.id, container, dtTable)
                If SaveType = SaveType.Update Then
                    'CreateUpdate(MySesion.Usuario.id, container, dtTable)

                    Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing

                    Try
                        dbmCore = New DBCoreDataBaseManager(ConnectionString.Core)
                        dbmCore.Connection_Open(MySesion.Usuario.id)
                        dbmCore.Transaction_Begin()
                        Dim updateCampo = New SchemaConfig.TBL_CampoType()

                        If Not IsDBNull(GetControlValue(container, "fk_Entidad")) Then updateCampo.fk_Entidad = CShort(GetControlValue(container, "fk_Entidad"))
                        If Not IsDBNull(GetControlValue(container, "fk_Documento")) Then updateCampo.fk_Documento = CInt(GetControlValue(container, "fk_Documento"))
                        If Not IsDBNull(GetControlValue(container, "id_Campo")) Then updateCampo.id_Campo = CShort(GetControlValue(container, "id_Campo"))
                        If Not IsDBNull(GetControlValue(container, "fk_Campo_Tipo")) Then updateCampo.fk_Campo_Tipo = CByte(GetControlValue(container, "fk_Campo_Tipo"))
                        If Not IsDBNull(GetControlValue(container, "fk_Campo_Lista")) Then updateCampo.fk_Campo_Lista = Short.Parse(IIf(GetControlValue(container, "fk_Campo_Lista") = "", "0", GetControlValue(container, "fk_Campo_Lista")))
                        If Not IsDBNull(GetControlValue(container, "Es_Campo_Busqueda")) Then updateCampo.Es_Campo_Busqueda = CBool(GetControlValue(container, "Es_Campo_Busqueda"))
                        If Not IsDBNull(GetControlValue(container, "fk_Campo_Busqueda")) Then updateCampo.fk_Campo_Busqueda = Short.Parse(IIf(GetControlValue(container, "fk_Campo_Busqueda") = "", "0", GetControlValue(container, "fk_Campo_Busqueda")))
                        If Not IsDBNull(GetControlValue(container, "Nombre_Campo")) Then updateCampo.Nombre_Campo = GetControlValue(container, "Nombre_Campo").ToString
                        If Not IsDBNull(GetControlValue(container, "Es_Campo_Folder")) Then updateCampo.Es_Campo_Folder = CBool(GetControlValue(container, "Es_Campo_Folder"))
                        If Not IsDBNull(GetControlValue(container, "Es_Obligatorio_Campo")) Then updateCampo.Es_Obligatorio_Campo = CBool(GetControlValue(container, "Es_Obligatorio_Campo"))
                        If Not IsDBNull(GetControlValue(container, "Length_Campo")) Then updateCampo.Length_Campo =Short.Parse(IIf(GetControlValue(container, "Length_Campo") = "", "0", GetControlValue(container, "Length_Campo")))
                        If Not IsDBNull(GetControlValue(container, "Es_Exportable")) Then updateCampo.Es_Exportable = CBool(GetControlValue(container, "Es_Exportable"))
                        If Not IsDBNull(GetControlValue(container, "Eliminado_Campo")) Then updateCampo.Eliminado_Campo = CBool(GetControlValue(container, "Eliminado_Campo"))
                        If Not IsDBNull(GetControlValue(container, "Length_Min_Campo")) Then updateCampo.Length_Min_Campo = Short.Parse(IIf(GetControlValue(container, "Length_Min_Campo") = "", "0", GetControlValue(container, "Length_Min_Campo")))
                        If Not IsDBNull(GetControlValue(container, "Usa_Decimales")) Then updateCampo.Usa_Decimales = CBool(GetControlValue(container, "Usa_Decimales"))
                        If Not IsDBNull(GetControlValue(container, "Caracter_Decimal")) Then updateCampo.Caracter_Decimal = CStr(GetControlValue(container, "Caracter_Decimal"))
                        If Not IsDBNull(GetControlValue(container, "Cantidad_Decimales")) Then updateCampo.Cantidad_Decimales = Short.Parse(IIf(GetControlValue(container, "Cantidad_Decimales") = "", "0", GetControlValue(container, "Cantidad_Decimales")))
                        If Not IsDBNull(GetControlValue(container, "Body_Query")) Then updateCampo.Body_Query = CStr(GetControlValue(container, "Body_Query"))
                        If Not IsDBNull(GetControlValue(container, "Validar_Registros")) Then updateCampo.Validar_Registros = CBool(GetControlValue(container, "Validar_Registros"))
                        If Not IsDBNull(GetControlValue(container, "Validar_Totales")) Then updateCampo.Validar_Totales = CBool(GetControlValue(container, "Validar_Totales"))
                        If Not IsDBNull(GetControlValue(container, "Valor_por_Defecto")) Then updateCampo.Valor_por_Defecto = CStr(GetControlValue(container, "Valor_por_Defecto"))
                        If Not IsDBNull(GetControlValue(container, "fk_Usuario_Log")) Then updateCampo.fk_Usuario_Log = MySesion.Usuario.id
                        If Not IsDBNull(GetControlValue(container, "Fecha_Log")) Then updateCampo.Fecha_Log = slygnullable.SysDate

                        dbmCore.SchemaConfig.TBL_Campo.DBUpdate(updateCampo, updateCampo.fk_Documento, updateCampo.id_Campo)

                        dbmCore.Transaction_Commit()
                    Catch ex As Exception
                        dbmCore.Transaction_Rollback()
                        Throw
                    Finally
                        If dbmCore IsNot Nothing Then dbmCore.Connection_Close()
                    End Try
                End If

                CurrentMasterTab = MasterTabType.Filter
                ' LlenaGrilla(grdData, CreateSelect(MySesion.Usuario.id, Esquema, Tabla))
            Catch ex As Exception
                showErrorPage(ex)
            End Try
        End Sub

        Protected Sub btnEliminar_Click() Handles MyBase.CommandActionDelete
            Try
                Dim table As DataTable = CType(Session("TableForm"), DataTable)

                CreateDelete(MySesion.Usuario.id, container, table)
                CurrentMasterTab = MasterTabType.Filter
            Catch ex As Exception
                showErrorPage(ex)
            End Try
        End Sub

        Protected Sub BtnNuevo_Click() Handles MyBase.CommandActionNew
            Try
                CargarDdlDetalle()

                Clear_Controls(CType(pnlDetalle, UI.Control))

                'Por defecto la entidad Actual
                fk_Entidad.SelectedValue = CStr(nEntidad)

                'Habilitar los ddl
                fk_Entidad.Enabled = True
                fk_proyecto.Enabled = True
                fk_esquema.Enabled = True
                fk_Documento.Enabled = True
                fk_Campo_Busqueda.Visible = False

                CurrentMasterTab = MasterTabType.Detail
                SaveType = SaveType.Insert
            Catch ex As Exception
                showErrorPage(ex)
            End Try
        End Sub

        Protected Sub CargarDetalleCampo()
            Try
                If grdData.SelectedIndex > -1 Then

                    '1. Cargar los ddl
                    CargarDdlDetalle()

                    Dim DBMCore As New DBCoreDataBaseManager(MyBase.ConnectionString.Core)
                    DBMCore.Connection_Open(MySesion.Usuario.id)

                    Dim nid_Campo, id_Documento As Integer ' = grdData.SelectedIndex + 1
                    nid_Campo = CShort(GridData.Rows(grdData.SelectedIndex)("id_campo"))
                    id_Documento = CShort(GridData.Rows(grdData.SelectedIndex)("id_Documento"))

                    Dim CampoDateTable = DBMCore.SchemaProcess.PA_Campos_Documento.DBExecute(CShort(id_Documento), CShort(nid_Campo))

                    If fk_Entidad.Items.Count = 0 Then
                        fk_Entidad = LlenarDataddl(fk_Entidad, "id_Entidad", "Nombre_Entidad", EntidadDateTable)
                    End If

                    fk_Entidad.SelectedValue = CampoDateTable.Rows(0)("id_Entidad").ToString
                    If fk_proyecto.Items.Count = 1 And fk_proyecto.SelectedValue = "-1" Then
                        Load_Proyectos(CShort(fk_Entidad.SelectedValue), True)
                    End If
                    fk_proyecto.SelectedValue = CampoDateTable.Rows(0)("id_Proyecto").ToString
                    If fk_esquema.Items.Count = 1 And fk_esquema.SelectedValue = "-1" Then
                        Load_Esquemas(CShort(fk_proyecto.SelectedValue), CShort(fk_Entidad.SelectedValue), True)
                    End If
                    fk_esquema.SelectedValue = CampoDateTable.Rows(0)("id_Esquema").ToString
                    If fk_Documento.Items.Count = 1 And fk_Documento.SelectedValue = "-1" Then
                        Load_Documentos(CShort(fk_Entidad.SelectedValue), CShort(fk_proyecto.SelectedValue), CShort(fk_esquema.SelectedValue), True)
                    End If
                    fk_Documento.SelectedValue = CampoDateTable.Rows(0)("id_Documento").ToString

                    'Cargar los atributos del Campo
                    id_Campo.Text = CampoDateTable.Rows(0)("id_Campo").ToString
                    fk_Campo_Tipo.SelectedValue = CampoDateTable.Rows(0)("fk_Campo_Tipo").ToString


                    If Not CampoDateTable.Rows(0)("fk_Campo_Lista") Is DBNull.Value Then
                        If CampoDateTable.Rows(0)("fk_Campo_Lista").ToString <> "0" Then
                            fk_Campo_Lista.SelectedValue = CampoDateTable.Rows(0)("fk_Campo_Lista").ToString
                        Else
                            fk_Campo_Lista.SelectedValue = Nothing
                        End If
                    Else
                        fk_Campo_Lista.SelectedValue = Nothing
                    End If
                    Es_Campo_Busqueda.Checked = CBool(CampoDateTable.Rows(0)("Es_Campo_Busqueda"))

                    If CBool(CampoDateTable.Rows(0)("Es_Campo_Busqueda")) Then
                        Dim idCampoBusqueda As Integer = CInt(CampoDateTable.Rows(0)("fk_Campo_Busqueda").ToString)
                        If idCampoBusqueda <> 0 Then
                            'Campo_BusquedaDataTable = DBMCore.SchemaConfig.TBL_Campo_Busqueda.DBFindByfk_Campo_Tipoid_Campo_Busqueda(CByte(fk_Campo_Tipo.SelectedValue), CShort(idCampoBusqueda))
                            fk_Campo_Busqueda.Text = CampoDateTable.Rows(0)("fk_Campo_Busqueda").ToString
                            fk_Campo_Busqueda.Visible = True
                        Else
                            fk_Campo_Busqueda.Text = ""
                            fk_Campo_Busqueda.Visible = True
                        End If
                    Else
                        fk_Campo_Busqueda.Text = ""
                        fk_Campo_Busqueda.Visible = False
                    End If

                    Nombre_Campo.Text = CampoDateTable.Rows(0)("Nombre_Campo").ToString
                    Es_Campo_Folder.Checked = CBool(CampoDateTable.Rows(0)("Es_Campo_Folder"))
                    Es_Obligatorio_Campo.Checked = CBool(CampoDateTable.Rows(0)("Es_Obligatorio_Campo"))
                    Length_Campo.Text = CampoDateTable.Rows(0)("Length_Campo").ToString
                    Es_Exportable.Checked = CBool(CampoDateTable.Rows(0)("Es_Exportable").ToString)
                    Eliminado_Campo.Checked = CBool(CampoDateTable.Rows(0)("Eliminado_Campo").ToString)

                    'Campos Nuevos

                    Length_Min_Campo.Text = CampoDateTable.Rows(0)("Length_Min_Campo").ToString
                    Usa_Decimales.Checked = CBool(CampoDateTable.Rows(0)("Usa_Decimales"))
                    Caracter_Decimal.Text = CampoDateTable.Rows(0)("Caracter_Decimal").ToString
                    Cantidad_Decimales.Text = CampoDateTable.Rows(0)("Cantidad_Decimales").ToString
                    Body_Query.Text = CampoDateTable.Rows(0)("Body_Query").ToString
                    Validar_Registros.Checked = CBool(CampoDateTable.Rows(0)("Validar_Registros"))
                    Validar_Totales.Checked = CBool(CampoDateTable.Rows(0)("Validar_Totales"))
                    Valor_por_Defecto.Text = CampoDateTable.Rows(0)("Valor_por_Defecto").ToString
                    fk_Usuario_Log.Text = CampoDateTable.Rows(0)("fk_Usuario_Log").ToString
                    Fecha_Log.Text = CampoDateTable.Rows(0)("Fecha_Log").ToString

                    'Inhabilitar los ddl de los datos genericos
                    fk_Entidad.Enabled = False
                    fk_proyecto.Enabled = False
                    fk_esquema.Enabled = False
                    fk_Documento.Enabled = False

                    SaveType = SaveType.Update
                    CurrentMasterTab = MasterTabType.Detail
                End If
            Catch ex As Exception
                showErrorPage(ex)
            End Try
        End Sub

        Public Sub IndexChanged()
            Try
                CargarDdlDetalle()
                If grdData.SelectedIndex > -1 Then
                    CoreGridViewLinkControls(GridData, grdData.SelectedIndex, container)
                    CurrentMasterTab = MasterTabType.Detail
                    SaveType = SaveType.Update
                End If
            Catch ex As Exception
                showErrorPage(ex)
            End Try
        End Sub

        Private Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
            NumRegistros.Text = "Número de registros: " & grdData.Rows.Count
        End Sub

#End Region

    End Class
End Namespace