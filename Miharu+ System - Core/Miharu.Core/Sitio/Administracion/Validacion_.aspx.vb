Imports DBCore
Imports Miharu.Core.Clases
Imports Slyg.Tools

Namespace Sitio.Administracion

    Public Class Validacion
        Inherits FormBase

#Region "Declaraciones"

        Dim Esquema As String
        Dim Entidad As String
        Dim Tabla As String
        Dim container As Object
        Private grilla As CoreGridView
        Dim ValidacionesDataTable As SchemaConfig.CTA_ValidacionDataTable
        Dim DocumentoDataTable As DataTable
        Private EntidadDataTable As DBSecurity.SchemaConfig.TBL_EntidadDataTable
        Private ProyectoDataTable As SchemaConfig.TBL_ProyectoDataTable
        Private EsquemaDataTable As SchemaConfig.TBL_EsquemaDataTable
        Private DocumentosDataTable As SchemaConfig.TBL_DocumentoDataTable
        Private CampoListaDataTable As SchemaConfig.TBL_Campo_ListaDataTable
        Private ValidacionCategoriaDataTable As SchemaConfig.TBL_Validacion_CategoriaDataTable

        Public Property Grilla1 As CoreGridView
            Get
                Return grilla
            End Get
            Set(value As CoreGridView)
                grilla = value
            End Set
        End Property

#End Region

#Region "Eventos"

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Grilla1 = grdData
            container = pnlDetalle
            Esquema = "config"
            Entidad = CStr(MySesion.Entidad.id)
            Tabla = "TBL_Validacion"

            'fk_Entidad.Enabled = AccesoEntidad
            'fk_Proyecto.Enabled = AccesoEntidad
            'fk_Esquema.Enabled = AccesoEntidad
            'fk_Documento.Enabled = AccesoEntidad
            LoadAutomatic()

            If Not Me.IsPostBack Then
                Config_Page()
            Else
                Load_Data()
            End If
        End Sub

        Private Sub fk_EntidadFiltro_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles fk_EntidadFiltro.SelectedIndexChanged
            Load_Proyectos(CShort(fk_EntidadFiltro.SelectedValue), False)
            Load_Esquemas(CShort(fk_ProyectoFiltro.SelectedValue), CShort(fk_EntidadFiltro.SelectedValue), False)
            Load_Documentos(CShort(fk_EntidadFiltro.SelectedValue), CShort(fk_ProyectoFiltro.SelectedValue), CShort(fk_EsquemaFiltro.SelectedValue), False)
        End Sub

        Private Sub fk_ProyectoFiltro_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles fk_ProyectoFiltro.SelectedIndexChanged
            Load_Esquemas(CShort(fk_ProyectoFiltro.SelectedValue), CShort(fk_EntidadFiltro.SelectedValue), False)
        End Sub

        Private Sub fk_EsquemaFiltro_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles fk_EsquemaFiltro.SelectedIndexChanged
            Load_Documentos(CShort(fk_EntidadFiltro.SelectedValue), CShort(fk_ProyectoFiltro.SelectedValue), CShort(fk_EsquemaFiltro.SelectedValue), False)
        End Sub

        Private Sub fk_Entidad_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles fk_Entidad.SelectedIndexChanged
            Load_Proyectos(CShort(fk_Entidad.SelectedValue), True)
            Load_Motivos(CShort(fk_Entidad.SelectedValue))
            Load_Categorias()
            Load_Esquemas(CShort(fk_Proyecto.SelectedValue), CShort(fk_Entidad.SelectedValue), True)
            Load_Documentos(CShort(fk_Entidad.SelectedValue), CShort(fk_Proyecto.SelectedValue), CShort(fk_Esquema.SelectedValue), True)
        End Sub

        Private Sub fk_Proyecto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles fk_Proyecto.SelectedIndexChanged
            Load_Esquemas(CShort(fk_Proyecto.SelectedValue), CShort(fk_Entidad.SelectedValue), True)
        End Sub

        Private Sub fk_Esquema_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles fk_Esquema.SelectedIndexChanged
            Load_Documentos(CShort(fk_Entidad.SelectedValue), CShort(fk_Proyecto.SelectedValue), CShort(fk_Esquema.SelectedValue), True)
        End Sub

        Private Sub GrdData_changed(ByVal nDocumentoDataTable As DataTable)
            CargarDdlDetalle()

            If fk_Entidad.Items.Count = 0 Then
                fk_Entidad = LlenarDataddl(fk_Entidad, "id_Entidad", "Nombre_Entidad", EntidadDataTable)
            End If

            id_Validacion.Text = nDocumentoDataTable.Rows(0)("id_Validacion").ToString
            fk_Entidad.SelectedValue = nDocumentoDataTable.Rows(0)("fk_Entidad").ToString

            If fk_Proyecto.Items.Count = 1 And fk_Proyecto.SelectedValue = "-1" Then
                Load_Proyectos(CShort(fk_Entidad.SelectedValue), True)
            End If
            fk_Proyecto.SelectedValue = nDocumentoDataTable.Rows(0)("fk_Proyecto").ToString

            If fk_Campo_Lista.Items.Count = 1 And fk_Campo_Lista.SelectedValue = "-1" Then
                Load_Motivos(CShort(fk_Entidad.SelectedValue))
            End If
            fk_Campo_Lista.SelectedValue = nDocumentoDataTable.Rows(0)("fk_Campo_Lista").ToString

            If fk_Esquema.Items.Count = 1 And fk_Esquema.SelectedValue = "-1" Then
                Load_Esquemas(CShort(fk_Proyecto.SelectedValue), CShort(fk_Entidad.SelectedValue), True)
            End If
            fk_Esquema.SelectedValue = nDocumentoDataTable.Rows(0)("fk_Esquema").ToString

            If fk_Documento.Items.Count = 1 And fk_Documento.SelectedValue = "-1" Then
                Load_Documentos(CShort(fk_Entidad.SelectedValue), CShort(fk_Proyecto.SelectedValue), CShort(fk_Esquema.SelectedValue), True)
            End If
            fk_Documento.SelectedValue = nDocumentoDataTable.Rows(0)("id_Documento").ToString

            If fk_Validacion_Categoria.Items.Count = 1 And fk_Validacion_Categoria.SelectedValue = "-1" Then
                Load_Categorias()
            End If
            fk_Validacion_Categoria.SelectedValue = nDocumentoDataTable.Rows(0)("fk_Validacion_Categoria").ToString

            Pregunta_Validacion.Text = nDocumentoDataTable.Rows(0)("Pregunta_Validacion").ToString
            Pregunta_Validacion_Reporte.Text = nDocumentoDataTable.Rows(0)("Pregunta_Validacion_Reporte").ToString
            Obligatorio.Checked = Boolean.Parse(nDocumentoDataTable.Rows(0)("Obligatorio").ToString)
            Es_Subsanable.Checked = Boolean.Parse(nDocumentoDataTable.Rows(0)("Es_Subsanable").ToString)
            Usa_Motivo.Checked = Boolean.Parse(nDocumentoDataTable.Rows(0)("Usa_Motivo").ToString)
            Eliminado.Checked = Boolean.Parse(nDocumentoDataTable.Rows(0)("Eliminado").ToString)

            fk_Entidad.Enabled = False
            fk_Proyecto.Enabled = False
            fk_Esquema.Enabled = False
            fk_Documento.Enabled = False
        End Sub

        Private Sub CargarDdlDetalle()
            fk_Entidad = LlenarDataddl(fk_Entidad, "id_Entidad", "Nombre_Entidad", EntidadDataTable)
            fk_Proyecto = LlenarDataddl(fk_Proyecto, "id_Proyecto", "Nombre_Proyecto", ProyectoDataTable)
            fk_Esquema = LlenarDataddl(fk_Esquema, "id_Esquema", "Nombre_Esquema", EsquemaDataTable)
            fk_Documento = LlenarDataddl(fk_Documento, "id_Documento", "Nombre_Documento", DocumentosDataTable)
        End Sub

        Protected Sub Usa_Motivo_CheckedChanged(sender As Object, e As EventArgs) Handles Usa_Motivo.CheckedChanged
            fk_Campo_Lista.Enabled = Usa_Motivo.Checked
        End Sub


#End Region

#Region "Métodos"

        Public Sub Config_Page()
            Dim dmCore As DBCoreDataBaseManager = Nothing

            Try
                dmCore = New DBCoreDataBaseManager(MyBase.ConnectionString.Core)
                dmCore.Connection_Open(MySesion.Usuario.id)
                Dim imgB As ImageButton = CType(MyMasterPage.ToolControl.FindControl("imgSave"), ImageButton)
                imgB.ValidationGroup = "Guardar"
                Session("TableForm") = DataDictionary(Esquema, Tabla)

                'Almacenar en memoria los datos
                EntidadDataTable = New DBSecurity.SchemaConfig.TBL_EntidadDataTable
                Me.MySesion.Pagina.Parameter("EntidadDataTable") = EntidadDataTable

                ProyectoDataTable = New SchemaConfig.TBL_ProyectoDataTable
                Me.MySesion.Pagina.Parameter("ProyectoDataTable") = ProyectoDataTable

                EsquemaDataTable = New SchemaConfig.TBL_EsquemaDataTable
                Me.MySesion.Pagina.Parameter("EsquemaDataTable") = EsquemaDataTable

                DocumentosDataTable = New SchemaConfig.TBL_DocumentoDataTable
                Me.MySesion.Pagina.Parameter("DocumentosDataTable") = DocumentosDataTable

                CampoListaDataTable = New SchemaConfig.TBL_Campo_ListaDataTable
                Me.MySesion.Pagina.Parameter("CampoListaDataTable") = CampoListaDataTable

                ValidacionCategoriaDataTable = New SchemaConfig.TBL_Validacion_CategoriaDataTable
                Me.MySesion.Pagina.Parameter("ValidacionCategoriaDataTable") = ValidacionCategoriaDataTable

                'Cargar las Entidades

                Dim DBMSecurity As DBSecurity.DBSecurityDataBaseManager = Nothing
                Try
                    DBMSecurity = New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security)
                    DBMSecurity.Connection_Open(MySesion.Usuario.id)
                    DBMSecurity.SchemaConfig.TBL_Entidad.DBFill(EntidadDataTable, Nothing)
                Catch ex As Exception
                    Throw
                Finally
                    If DBMSecurity IsNot Nothing Then DBMSecurity.Connection_Close()
                End Try

                'Cargar las Entidades
                fk_EntidadFiltro = LlenarDataddl(fk_EntidadFiltro, "id_Entidad", "Nombre_Entidad", EntidadDataTable)
                Load_Proyectos(CShort(fk_EntidadFiltro.SelectedValue), False)
                Load_Motivos(CShort(fk_EntidadFiltro.SelectedValue))
                Load_Categorias()
                Load_Esquemas(CShort(fk_ProyectoFiltro.SelectedValue), CShort(fk_EntidadFiltro.SelectedValue), False)
                Load_Documentos(CShort(fk_EntidadFiltro.SelectedValue), CShort(fk_ProyectoFiltro.SelectedValue), CShort(fk_EsquemaFiltro.SelectedValue), False)
            Catch ex As Exception
                showErrorPage(ex)
            Finally
                If dmCore IsNot Nothing Then dmCore.Connection_Close()
            End Try

        End Sub

        Private Sub Load_Proyectos(ByVal id_Entidad As Short, ByVal Detalle As Boolean)
            Dim DBMMiharu As New DBCoreDataBaseManager(MyBase.ConnectionString.Core)

            Try
                DBMMiharu.Connection_Open(MySesion.Usuario.id)
                ProyectoDataTable.Clear()
                DBMMiharu.SchemaConfig.TBL_Proyecto.DBFillByfk_Entidad(ProyectoDataTable, id_Entidad)
            Catch ex As Exception
                showErrorPage(ex)
            Finally
                DBMMiharu.Connection_Close()
            End Try
            'Cargar los proyectos
            If Detalle Then
                fk_Proyecto = LlenarDataddl(fk_Proyecto, "id_Proyecto", "Nombre_Proyecto", ProyectoDataTable)
            Else
                fk_ProyectoFiltro = LlenarDataddl(fk_ProyectoFiltro, "id_Proyecto", "Nombre_Proyecto", ProyectoDataTable)
            End If
        End Sub

        Private Sub Load_Motivos(ByVal id_Entidad As Short)
            Dim dbmCore As DBCoreDataBaseManager = Nothing

            Try
                dbmCore = New DBCoreDataBaseManager(MyBase.ConnectionString.Core)
                dbmCore.Connection_Open(MySesion.Usuario.id)
                CampoListaDataTable.Clear()
                CampoListaDataTable = dbmCore.SchemaConfig.TBL_Campo_Lista.DBFindByfk_Entidad(id_Entidad)
            Catch ex As Exception
                showErrorPage(ex)
            Finally
                dbmCore.Connection_Close()
            End Try
            'Cargar los Motivos
            fk_Campo_Lista = LlenarDataddl(fk_Campo_Lista, "id_Campo_Lista", "Nombre_Campo_Lista", CampoListaDataTable)
        End Sub

        Private Sub Load_Categorias()
            Dim dbmCore As DBCoreDataBaseManager = Nothing

            Try
                dbmCore = New DBCoreDataBaseManager(MyBase.ConnectionString.Core)
                dbmCore.Connection_Open(MySesion.Usuario.id)
                ValidacionCategoriaDataTable.Clear()
                ValidacionCategoriaDataTable = dbmCore.SchemaConfig.TBL_Validacion_Categoria.DBGet(Nothing)
            Catch ex As Exception
                showErrorPage(ex)
            Finally
                dbmCore.Connection_Close()
            End Try
            'Cargar las Categorias
            fk_Validacion_Categoria = LlenarDataddl(fk_Validacion_Categoria, "id_Validacion_Categoria", "Nombre_Validacion_Categoria", ValidacionCategoriaDataTable)
        End Sub

        Private Sub Load_Esquemas(ByVal id_Proyecto As Short, ByVal id_Entidad As Short, ByVal Detalle As Boolean)
            Dim DBMMiharu As New DBCoreDataBaseManager(MyBase.ConnectionString.Core)

            Try
                DBMMiharu.Connection_Open(MySesion.Usuario.id)
                EsquemaDataTable.Clear()
                DBMMiharu.SchemaConfig.TBL_Esquema.DBFillByfk_Entidadfk_Proyecto(EsquemaDataTable, id_Entidad, id_Proyecto)

            Catch ex As Exception
                showErrorPage(ex)
            Finally
                DBMMiharu.Connection_Close()
            End Try
            'Mostrar los esquemas
            If Detalle Then
                fk_Esquema = LlenarDataddl(fk_Esquema, "id_Esquema", "Nombre_Esquema", EsquemaDataTable)
            Else
                fk_EsquemaFiltro = LlenarDataddl(fk_EsquemaFiltro, "id_Esquema", "Nombre_Esquema", EsquemaDataTable)
            End If

        End Sub

        Private Sub Load_Documentos(ByVal id_Entidad As Short, ByVal id_Proyecto As Short, ByVal id_Esquema As Short, ByVal Detalle As Boolean)
            Dim DBMMiharu As New DBCoreDataBaseManager(MyBase.ConnectionString.Core)

            Try
                DBMMiharu.Connection_Open(MySesion.Usuario.id)
                DocumentosDataTable.Clear()
                DBMMiharu.SchemaConfig.TBL_Documento.DBFillByfk_Entidadfk_Proyectofk_Esquema(DocumentosDataTable, id_Entidad, id_Proyecto, id_Esquema)

            Catch ex As Exception
                showErrorPage(ex)
            Finally
                DBMMiharu.Connection_Close()
            End Try
            'Cargar los documentos
            If Detalle Then
                fk_Documento = LlenarDataddl(fk_Documento, "id_Documento", "Nombre_Documento", DocumentosDataTable)
            Else
                fk_DocumentoFiltro = LlenarDataddl(fk_DocumentoFiltro, "id_Documento", "Nombre_Documento", DocumentosDataTable)
            End If

        End Sub

        Private Function LlenarDataddl(ByVal drp As DropDownList, ByVal value As String, ByVal text As String, ByVal Data As DataTable) As DropDownList
            drp.DataSource = Data
            drp.DataValueField = value
            drp.DataTextField = text
            drp.DataBind()
            Dim drop = adicionarOpSeleccionAddl(drp)
            Return drop
        End Function

        Private Function adicionarOpSeleccionAddl(ByVal drp As DropDownList) As DropDownList
            Dim DropNew As DropDownList = drp
            Dim item As New ListItem("Seleccione ...", "-1")
            DropNew.Items.Insert(0, item)
            Return DropNew
        End Function

        Private Sub Load_Data()
            EntidadDataTable = CType(Me.MySesion.Pagina.Parameter("EntidadDataTable"), DBSecurity.SchemaConfig.TBL_EntidadDataTable)
            ProyectoDataTable = CType(Me.MySesion.Pagina.Parameter("ProyectoDataTable"), SchemaConfig.TBL_ProyectoDataTable)
            EsquemaDataTable = CType(Me.MySesion.Pagina.Parameter("EsquemaDataTable"), SchemaConfig.TBL_EsquemaDataTable)
            DocumentosDataTable = CType(Me.MySesion.Pagina.Parameter("DocumentosDataTable"), SchemaConfig.TBL_DocumentoDataTable)
            CampoListaDataTable = CType(Me.MySesion.Pagina.Parameter("CampoListaDataTable"), SchemaConfig.TBL_Campo_ListaDataTable)
            ValidacionCategoriaDataTable = CType(Me.MySesion.Pagina.Parameter("ValidacionCategoriaDataTable"), SchemaConfig.TBL_Validacion_CategoriaDataTable)
        End Sub

        Public Sub CargarCombos()
            Dim dbmCore As DBCoreDataBaseManager = Nothing

            Try
                dbmCore = New DBCoreDataBaseManager(ConnectionString.Core)
                dbmCore.Connection_Open(MySesion.Usuario.id)
                Dim Data As New DataSet
                Data.Tables.Add(dbmCore.SchemaConfig.TBL_Proyecto.DBGet(Nothing, Nothing))
                Data.Tables.Add(dbmCore.SchemaConfig.TBL_Esquema.DBGet(Nothing, Nothing, Nothing))
                Data.Tables.Add(dbmCore.SchemaConfig.TBL_Documento.DBGet(Nothing))
                GlobalData = Data
                Llenacombo(fk_Entidad, dbmCore.Schemadbo.CTA_Entidad.DBGet(), "id_entidad", "nombre_entidad")

                DropDownListCascading(fk_Proyecto, GlobalData.Tables(0), "id_proyecto", "nombre_proyecto")
                DropDownListCascading(fk_Esquema, GlobalData.Tables(1), "id_esquema", "nombre_esquema")

                If fk_Esquema.SelectedValue = "" Or fk_Esquema.SelectedValue = "-1" Then
                    DropDownListCascading(fk_Documento, GlobalData.Tables(2), "id_documento", "nombre_documento")
                Else
                    DropDownListCascading(fk_Documento, GlobalData.Tables(2), "id_documento", "nombre_documento", "fk_entidad='" & fk_Entidad.SelectedValue & "'", "fk_proyecto='" & fk_Proyecto.SelectedValue & "'", "fk_esquema='" & fk_Esquema.SelectedValue & "'")
                End If

            Catch ex As Exception
                Throw
            Finally
                If dbmCore IsNot Nothing Then dbmCore.Connection_Close()
            End Try


        End Sub

        Public Sub CargarCombosBusqueda()
            Dim dbmCore As DBCoreDataBaseManager = Nothing

            Try
                dbmCore = New DBCoreDataBaseManager(ConnectionString.Core)
                dbmCore.Connection_Open(MySesion.Usuario.id)

                Dim Data As New DataSet

                Llenacombo(fk_EntidadFiltro, dbmCore.Schemadbo.CTA_Entidad.DBGet(), "id_entidad", "nombre_entidad")
                Data.Tables.Add(dbmCore.SchemaConfig.TBL_Proyecto.DBGet(Nothing, Nothing))
                Data.Tables.Add(dbmCore.SchemaConfig.TBL_Esquema.DBGet(Nothing, Nothing, Nothing))
                Data.Tables.Add(dbmCore.SchemaConfig.TBL_Documento.DBGet(Nothing))

                DropDownListCascading(fk_ProyectoFiltro, Data.Tables(0), "id_proyecto", "nombre_proyecto", "fk_entidad='" & fk_EntidadFiltro.SelectedValue & "'")

                GlobalData = Data

                DropDownListCascading(fk_EsquemaFiltro, Data.Tables(1), "id_esquema", "nombre_esquema", "fk_entidad='" & fk_EntidadFiltro.SelectedValue & "'", "fk_proyecto='" & fk_ProyectoFiltro.SelectedValue & "'")

                If fk_EsquemaFiltro.SelectedValue = "" Or fk_EsquemaFiltro.SelectedValue = "-1" Then
                    DropDownListCascading(fk_DocumentoFiltro, Data.Tables(2), "id_documento", "nombre_documento")
                Else
                    DropDownListCascading(fk_DocumentoFiltro, Data.Tables(2), "id_documento", "nombre_documento", "fk_entidad='" & fk_EntidadFiltro.SelectedValue & "'", "fk_proyecto='" & fk_ProyectoFiltro.SelectedValue & "'", "fk_esquema='" & fk_EsquemaFiltro.SelectedValue & "'")
                End If

            Catch ex As Exception
                Throw
            Finally
                If dbmCore IsNot Nothing Then dbmCore.Connection_Close()
            End Try

        End Sub

        Private Sub BuscarValidaciones()
            Dim DBMCore As DBCoreDataBaseManager
            Try

                DBMCore = New DBCoreDataBaseManager(MyBase.ConnectionString.Core)
                ValidacionesDataTable = New SchemaConfig.CTA_ValidacionDataTable
                Me.MySesion.Pagina.Parameter("ValidacionesDataTable") = ValidacionesDataTable

                Dim nEntidad As New Slyg.Tools.SlygNullable(Of Short)
                Dim Proyecto As New Slyg.Tools.SlygNullable(Of Short)
                Dim nEsquema = New Slyg.Tools.SlygNullable(Of Short)
                Dim Documento As New Slyg.Tools.SlygNullable(Of Integer)

                If fk_EntidadFiltro.SelectedValue <> "-1" Then nEntidad = CShort(fk_EntidadFiltro.SelectedValue)
                If fk_ProyectoFiltro.SelectedValue <> "-1" Then Proyecto = CShort(fk_ProyectoFiltro.SelectedValue)
                If fk_EsquemaFiltro.SelectedValue <> "-1" Then nEsquema = CShort(fk_EsquemaFiltro.SelectedValue)
                If fk_DocumentoFiltro.SelectedValue <> "-1" Then Documento = CInt(fk_DocumentoFiltro.SelectedValue)

                Try
                    DBMCore.Connection_Open(MySesion.Usuario.id)
                    ValidacionesDataTable = DBMCore.SchemaConfig.CTA_Validacion.DBFindByfk_Entidadfk_Proyectofk_Esquemaid_Documento(nEntidad, Proyecto, nEsquema, Documento)
                    GridData = ValidacionesDataTable
                Catch ex As Exception
                    showErrorPage(ex)
                Finally
                    DBMCore.Connection_Close()
                End Try
                'LlenaGrilla(grdData, CreateSelect(MySesion.Usuario.id, Esquema, Tabla))
                grdData.DataSource = ValidacionesDataTable
                grdData.DataBind()
                CurrentMasterTab = MasterTabType.Grid

            Catch ex As Exception
                showErrorPage(ex)
            End Try
        End Sub

#End Region

#Region "Automatic"

        Public Sub LoadAutomatic()
            If Not IsPostBack Then
                Dim imgB As ImageButton = CType(MyMasterPage.ToolControl.FindControl("imgSave"), ImageButton)
                imgB.ValidationGroup = "Guardar"
                Session("TableForm") = DataDictionary(Esquema, Tabla)
            End If
        End Sub

        Private Sub Page_CommandActionFind() Handles Me.CommandActionFind
            BuscarValidaciones()
        End Sub

        Protected Sub btnGuardar_Click() Handles MyBase.CommandActionSave
            Try
                Dim table As DataTable = CType(Session("TableForm"), DataTable)

                fk_Usuario_Log.Text = MySesion.Usuario.id
                Fecha_Log.Text = DateTime.Now.ToString(Program.getIdentifierDateFormat)

                If SaveType = SaveType.Insert Then CreateInsert(MySesion.Usuario.id, container, table)
                If SaveType = SaveType.Update Then
                    'CreateUpdate(MySesion.Usuario.id, container, table)

                    Dim dbmCore As DBCoreDataBaseManager = Nothing

                    Try
                        dbmCore = New DBCoreDataBaseManager(ConnectionString.Core)
                        dbmCore.Connection_Open(MySesion.Usuario.id)

                        Dim validacionType As SchemaConfig.TBL_ValidacionType = Nothing

                        validacionType = New SchemaConfig.TBL_ValidacionType

                        With (validacionType)
                            If Not IsDBNull(GetControlValue(container, "fk_Documento")) Then .fk_Documento = Short.Parse(IIf(GetControlValue(container, "fk_Documento") = "", "0", GetControlValue(container, "fk_Documento")))
                            If Not IsDBNull(GetControlValue(container, "id_Validacion")) Then .id_Validacion = Short.Parse(IIf(GetControlValue(container, "id_Validacion") = "", "0", GetControlValue(container, "id_Validacion")))
                            If Not IsDBNull(GetControlValue(container, "Pregunta_Validacion")) Then .Pregunta_Validacion = GetControlValue(container, "Pregunta_Validacion").ToString()
                            If Not IsDBNull(GetControlValue(container, "Obligatorio")) Then .Obligatorio = CBool(GetControlValue(container, "Obligatorio"))
                            If Not IsDBNull(GetControlValue(container, "Es_Subsanable")) Then .Es_Subsanable = CBool(GetControlValue(container, "Es_Subsanable"))
                            If Not IsDBNull(GetControlValue(container, "Usa_Motivo")) Then .Usa_Motivo = CBool(GetControlValue(container, "Usa_Motivo"))
                            If Not IsDBNull(GetControlValue(container, "fk_Entidad")) Then .fk_Entidad = Short.Parse(IIf(GetControlValue(container, "fk_Entidad") = "", "0", GetControlValue(container, "fk_Entidad")))
                            If Not IsDBNull(GetControlValue(container, "fk_Campo_Lista")) Then .fk_Campo_Lista = Short.Parse(IIf(GetControlValue(container, "fk_Campo_Lista") = "", "0", GetControlValue(container, "fk_Campo_Lista")))
                            If Not IsDBNull(GetControlValue(container, "fk_Validacion_Categoria")) Then .fk_Validacion_Categoria = Short.Parse(IIf(GetControlValue(container, "fk_Validacion_Categoria") = "", "0", GetControlValue(container, "fk_Validacion_Categoria")))
                            If Not IsDBNull(GetControlValue(container, "Pregunta_Validacion_Reporte")) Then .Pregunta_Validacion_Reporte = GetControlValue(container, "Pregunta_Validacion_Reporte").ToString()
                            If Not IsDBNull(GetControlValue(container, "Eliminado")) Then .Eliminado = CBool(GetControlValue(container, "Eliminado"))
                            .fk_Usuario_Log = MySesion.Usuario.id
                            .Fecha_Log = SlygNullable.SysDate
                        End With

                        dbmCore.SchemaConfig.TBL_Validacion.DBUpdate(validacionType, validacionType.fk_Documento, validacionType.id_Validacion)

                    Catch ex As Exception
                        Throw
                    Finally
                        If dbmCore IsNot Nothing Then dbmCore.Connection_Close()
                    End Try

                End If

                CurrentMasterTab = MasterTabType.Filter
                MyMasterPage.MasterGridPanel.Update()
                grdData.DataSource = Nothing
                grdData.DataBind()

                LimpiarControles(container, table, Entidad)
            Catch ex As Exception
                showErrorPage(ex)
            End Try
        End Sub

        Protected Sub btnEliminar_Click() Handles MyBase.CommandActionDelete
            Try
                Dim table As DataTable = CType(Session("TableForm"), DataTable)

                CreateDelete(MySesion.Usuario.id, container, table)
                CurrentMasterTab = MasterTabType.Grid
                BuscarValidaciones()
                LimpiarControles(container, table, Entidad)
            Catch ex As Exception
                showErrorPage(ex)
            End Try
        End Sub

        Protected Sub BtnNuevo_Click() Handles MyBase.CommandActionNew
            Try

                ' LimpiarControles(container, table, Entidad)
                CargarDdlDetalle()
                id_Validacion.Text = ""
                Pregunta_Validacion.Text = ""
                Obligatorio.Checked = False
                'Es_Validacion_Destape.Checked = False
                fk_Entidad.SelectedValue = "-1"
                fk_Proyecto.SelectedValue = "-1"
                fk_Esquema.SelectedValue = "-1"
                fk_Documento.SelectedValue = "-1"
                CurrentMasterTab = MasterTabType.Detail
                SaveType = SaveType.Insert
            Catch ex As Exception
                showErrorPage(ex)
            End Try
        End Sub

        Protected Sub grdData_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles grdData.SelectedIndexChanged
            IndexChanged()
        End Sub

        Protected Sub BtnEdit_Click() Handles MyBase.CommandActionEdit
            IndexChanged()
        End Sub

        Public Sub IndexChanged()
            Dim idValidacion As Integer
            Dim Documento As String
            Dim idDocumento As Integer
            Dim dbmCore As New DBCoreDataBaseManager(MyBase.ConnectionString.Core)
            Documento = CStr(GridData.Rows(grdData.SelectedIndex)("id_Documento"))
            idValidacion = CShort(GridData.Rows(grdData.SelectedIndex)("Id_validacion"))
            idDocumento = CInt(Split(Documento, "-")(0))
            DocumentoDataTable = New DataTable
            Try
                If grdData.SelectedIndex > -1 Then

                    CargarDdlDetalle()
                    'CoreGridViewLinkControls(GridData, grilla.SelectedIndex, container)
                    CurrentMasterTab = MasterTabType.Detail
                    SaveType = SaveType.Update
                    dbmCore.Connection_Open(MySesion.Usuario.id)
                    DocumentoDataTable = dbmCore.SchemaProcess.PA_Documento_Data.DBExecute(DShort(idValidacion), DShort(idDocumento))
                    GrdData_changed(DocumentoDataTable)
                End If
            Catch ex As Exception
                showErrorPage(ex)
            Finally
                dbmCore.Connection_Close()
            End Try
        End Sub

        Private Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
            If Not IsPostBack Then
                'MyMasterPage.MasterTabContainer.Tabs(0).Enabled = False
                CurrentMasterTab = MasterTabType.Filter
            End If

            NumRegistros.Text = "Número de registros: " & grdData.Rows.Count
        End Sub

#End Region

    End Class
End Namespace