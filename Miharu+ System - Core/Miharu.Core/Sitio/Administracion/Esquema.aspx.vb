Imports DBCore
Imports Miharu.Core.Clases

Namespace Sitio.Administracion

    Public Class Esquema
        Inherits FormBase

#Region "Eventos"

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            grilla = grdData
            container = panelDetalle
            Esquema = "config"
            Entidad = CStr(MySesion.Entidad.id)
            Tabla = "tbl_Esquema"

            LoadAutomatic()

            If Not IsPostBack Then
                Me.TabPanelAsociacion.Visible = False
            Else
                Me.TabPanelAsociacion.Visible = usa_NombreArchivo_Asociado_Tipologia.Checked
            End If
        End Sub

        Protected Sub Load_Complete() Handles Me.LoadComplete
            Config_Page()
        End Sub

        Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
            Try
                Dim parameter As New ParameterCollection
                parameter.Add("fk_entidad", fk_entidad.SelectedValue)
                GlobalParameterCollection = parameter

                OpenModalDialog("Find_Servidor", "../sitio/Administracion/P_Esquema_Servidor.aspx", "Find_Servidor", "Servidores ", 600, 350)
            Catch ex As Exception
                showErrorPage(ex)
            End Try
        End Sub

        Private Sub Campo_ModalClosed(ByVal parameters As String) Handles Me.ModalClosed
            Try
                Select Case ModalDialogNombre
                    Case "Find_Servidor"
                        Try
                            If CInt(GlobalParameterCollection("id_servidor").DefaultValue) <> -1 Then
                                fk_entidad_servidor.Text = GlobalParameterCollection("fk_entidad_servidor").DefaultValue
                                fk_servidor.Text = GlobalParameterCollection("id_servidor").DefaultValue
                                'fk_servidor_volumen.Text = GlobalParameterCollection("id_Servidor_Volumen").DefaultValue
                            End If
                        Catch ex As Exception
                        End Try
                        MyMasterPage.MasterDetailPanel.Update()
                End Select

            Catch ex As Exception
                showErrorPage(ex)
            End Try
        End Sub

        Protected Sub Grd_documentos_add_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles grd_documentos_add.Click
            Try
                If fk_tipologia.SelectedValue = "-1" Then Throw New Exception("Debe seleccionar una tipologia.")
                Dim Table As DataTable = CType(Session("TableDocumentos"), DataTable)
                Table = clonarDataTable(Table).DefaultView.ToTable(True, "id_documento", "fk_tipologia", "nombre_documento")

                Dim view As DataView = Table.DefaultView
                Try
                    view.RowFilter = "fk_tipologia=" & fk_tipologia.SelectedValue
                Catch : End Try

                If view.ToTable.Rows.Count > 0 Then Throw New Exception("Solo puede crear un documento por tipologia")

                Dim row As DataRow = Table.NewRow
                row("id_documento") = -2
                row("fk_tipologia") = fk_tipologia.Text
                row("nombre_documento") = nombre_documento.Text

                Table.Rows.Add(row)

                Session("TableDocumentos") = Table
                LlenaGrilla(grd_Documentos, Table, False, True)

                nombre_documento.Clear()
                fk_tipologia.SelectedIndex = -1

            Catch ex As Exception
                MyMasterPage.ShowMessage(ex.Message, MsgBoxIcon.IconInformation, "")
            End Try
        End Sub

        Protected Sub Grd_llaves_add_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles grd_llaves_add.Click
            Try
                Dim Table As DataTable = clonarDataTable(CType(Session("TableLlaves"), DataTable))

                Dim viewKey As New DataView(Table)
                Dim viewFields As New DataView(Table)
                Try
                    viewKey.RowFilter = "fk_proyecto_llave=" & fk_proyecto_llave.SelectedValue &
                                        " and fk_esquema=" & id_esquema.Text &
                                        " and fk_entidad=" & fk_entidad.Text &
                                        " and fk_proyecto=" & fk_proyecto.Text

                    viewFields.RowFilter = "fk_documento=" & fk_documento.SelectedValue &
                                           " and fk_campo=" & fk_campo.SelectedValue &
                                           " and fk_esquema=" & id_esquema.Text &
                                           " and fk_entidad=" & fk_entidad.Text &
                                           " and fk_proyecto=" & fk_proyecto.Text
                Catch : End Try


                If viewKey.ToTable.Rows.Count > 0 Then Throw New Exception("No se puede agregar una llave que ya esta relacionada a una llave de proyecto.")
                If viewFields.ToTable.Rows.Count > 0 Then Throw New Exception("No se puede agregar un campo que ya esta relacionado a una llave de proyecto.")
                If fk_proyecto_llave.SelectedValue = "-1" Then Throw New Exception("Debe seleccionar una llave de proyecto.")
                If fk_documento.SelectedValue = "-1" Then Throw New Exception("Debe seleccionar un documento.")
                If fk_campo.SelectedValue = "-1" Or fk_campo.SelectedValue = "" Then Throw New Exception("Debe seleccionar un campo.")

                Dim row As DataRow = Table.NewRow
                row("fk_proyecto") = fk_proyecto.SelectedValue
                row("fk_esquema") = id_esquema.Text

                row("fk_entidad") = fk_entidad.SelectedValue
                row("fk_proyecto_llave") = fk_proyecto_llave.SelectedValue
                row("fk_documento") = fk_documento.SelectedValue
                row("fk_campo") = fk_campo.SelectedValue

                row("Llave_proyecto") = fk_proyecto_llave.SelectedValue & " - " & fk_proyecto_llave.SelectedItem.Text
                row("documento") = fk_documento.SelectedValue & " - " & fk_documento.SelectedItem.Text
                row("Campo") = fk_campo.SelectedValue & " - " & fk_campo.SelectedItem.Text

                Table.Rows.Add(row)

                Session("TableLlaves") = Table

                'LlenaGrilla(grd_llaves, Table, "fk_proyecto_llave", "fk_documento", "fk_campo")
                LlenaGrilla(grd_llaves, Table, "Llave_proyecto", "documento", "Campo")

                fk_proyecto_llave.SelectedIndex = -1
                fk_documento.SelectedIndex = -1
                fk_campo.SelectedIndex = -1

            Catch ex As Exception
                MyMasterPage.ShowMessage(ex.Message, MsgBoxIcon.IconInformation, "")
            End Try
        End Sub

        Private Sub Grd_documentos_delete(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grd_Documentos.RowCommand
            Try
                If e.CommandSource.GetType() Is GetType(Web.UI.WebControls.ImageButton) Then
                    Dim table As DataTable = CType(Session("TableDocumentos"), DataTable)
                    Dim Reg As Integer = CType(sender, CoreGridView).PreSelectedIndex

                    'Elimina el documento
                    Dim dbmCore As DBCoreDataBaseManager = Nothing

                    Try
                        dbmCore = New DBCoreDataBaseManager(ConnectionString.Core)
                        dbmCore.Connection_Open(MySesion.Usuario.id)
                        dbmCore.SchemaConfig.TBL_Documento.DBDelete(CInt(table.Rows(Reg).Item("id_Documento")))

                        table.Rows(Reg).Delete()
                        table.AcceptChanges()

                        Session("TableDocumentos") = table

                    Catch ex As Exception
                        Throw
                    Finally
                        If dbmCore IsNot Nothing Then dbmCore.Connection_Close()
                    End Try


                End If
            Catch ex As Exception
                showErrorPage(ex)
            Finally
                LlenaGrilla(grd_Documentos, CType(Session("TableDocumentos"), DataTable), False, True)
            End Try
        End Sub

        Private Sub Grd_llaves_delete(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grd_llaves.RowCommand
            Try
                If e.CommandSource.GetType() Is GetType(Web.UI.WebControls.ImageButton) Then
                    Dim table As DataTable = CType(Session("TableLlaves"), DataTable)
                    Dim Reg As Integer = CType(sender, CoreGridView).PreSelectedIndex

                    table.Rows(Reg).Delete()
                    table.AcceptChanges()

                    'LlenaGrilla(grd_llaves, table, "fk_proyecto_llave", "fk_documento", "fk_campo")
                    LlenaGrilla(grd_llaves, table, "Llave_proyecto", "documento", "Campo")

                    Session("TableLlaves") = table
                End If
            Catch ex As Exception
                showErrorPage(ex)
            End Try
        End Sub

#End Region

#Region "Métodos"

        Public Sub Config_Page()

            Dim dbmCore As DBCoreDataBaseManager = Nothing

            Try
                dbmCore = New DBCoreDataBaseManager(ConnectionString.Core)
                dbmCore.Connection_Open(MySesion.Usuario.id)
                If Not IsPostBack Then
                    Dim data As New DataSet

                    data.Tables.Add(dbmCore.SchemaConfig.TBL_Proyecto.DBGet(Nothing, Nothing))
                    data.Tables.Add(dbmCore.SchemaConfig.TBL_Campo.DBGet(Nothing, Nothing))
                    data.Tables.Add(dbmCore.SchemaConfig.TBL_Documento.DBGet(Nothing))
                    GlobalData = data

                    Llenacombo(fk_entidad, dbmCore.Schemadbo.CTA_Entidad.DBGet, "id_entidad", "nombre_entidad")
                    Llenacombo(fk_tipologia, dbmCore.SchemaConfig.TBL_Tipologia.DBGet(Nothing), "id_tipologia", "nombre_tipologia")
                    Llenacombo(fk_Restriccion_Monto, dbmCore.SchemaConfig.TBL_Restriccion_Monto.DBGet(Nothing), "id_Restriccion_Monto", "Nombre_Restriccion_Monto")
                    ' =========================
                    ' NUEVO: llenar lista de Notificaciones (ajusta nombre si difiere en tu DAL)
                    ' =========================
                    Llenacombo(fk_Notificacion, dbmCore.SchemaConfig.TBL_Notificacion.DBGet(Nothing), "id_notificacion", "nombre_notificacion")
                    ' =========================
                    ' NUEVO: tipología para la pestaña de Asociación
                    ' =========================
                    Llenacombo(fk_Tipologia0, dbmCore.SchemaConfig.TBL_Tipologia.DBGet(Nothing), "id_tipologia", "nombre_tipologia")
                    Llenacombo(find_fk_Entidad, dbmCore.Schemadbo.CTA_Entidad.DBGet, "id_entidad", "nombre_entidad")

                    Session("TableDocumentos") = DocumentosTable()
                    Session("TableLlaves") = LlavesTable()

                    ' =========================
                    ' NUEVO: iniciar tabla de asociaciones y bind inicial
                    ' =========================
                    Session("TableAsociaciones") = AsociacionesTable()
                    Try
                        LlenaGrilla(grd_Asociaciones, CType(Session("TableAsociaciones"), DataTable), "fk_tipologia", "inicia_por", "finaliza_por")
                    Catch : End Try
                End If

                DropDownListCascading(fk_proyecto, GlobalData.Tables(0), "id_proyecto", "nombre_proyecto", "fk_entidad=" & fk_entidad.SelectedValue)
                DropDownListCascading(fk_proyecto_llave, dbmCore.SchemaConfig.TBL_Proyecto_Llave.DBGet(Nothing, Nothing, Nothing), "id_proyecto_llave", "nombre_proyecto_llave", "fk_entidad=" & fk_entidad.SelectedValue, "Fk_proyecto=" & fk_proyecto.SelectedValue)
                DropDownListCascading(fk_campo, dbmCore.SchemaConfig.TBL_Campo.DBGet(Nothing, Nothing), "id_campo", "nombre_campo", "fk_entidad=" & fk_entidad.SelectedValue, "fk_documento=" & fk_documento.SelectedValue)

                DropDownListCascading(find_fk_proyecto, GlobalData.Tables(0), "id_proyecto", "nombre_proyecto", "fk_entidad=" & find_fk_Entidad.SelectedValue)


                If CInt(Session("GridChanged")) = 1 Then

                    TabDetail.Tabs.Item(0).Enabled = True

                    Try
                        Session("TableDocumentos") = DocumentosTable()
                        LlenaGrilla(grd_Documentos, CType(Session("TableDocumentos"), DataTable), "id_documento", "fk_tipologia", "nombre_documento")
                    Catch : End Try

                    Try
                        Session("TableLlaves") = LlavesTable()
                        LlenaGrilla(grd_llaves, CType(Session("TableLlaves"), DataTable), "Llave_proyecto", "documento", "Campo")
                    Catch : End Try

                    Session("GridChanged") = 0
                End If


                Try
                    DropDownListCascading(fk_documento, CType(Session("TableDocumentos"), DataTable), "id_documento", "nombre_documento", "fk_entidad='" & fk_entidad.SelectedValue & "'", "fk_proyecto='" & fk_proyecto.SelectedValue & "'", "fk_esquema='" & id_esquema.Text & "'")

                    'DropDownListCascading(fk_Documento_1, CType(Session("TableDocumentos"), DataTable), "id_documento", "nombre_documento", "fk_entidad='" & fk_entidad.SelectedValue & "'", "fk_proyecto='" & fk_proyecto.SelectedValue & "'", "fk_esquema='" & id_esquema.Text & "'")
                    'DropDownListCascading(fk_Documento_2, CType(Session("TableDocumentos"), DataTable), "id_documento", "nombre_documento", "fk_entidad='" & fk_entidad.SelectedValue & "'", "fk_proyecto='" & fk_proyecto.SelectedValue & "'", "fk_esquema='" & id_esquema.Text & "'")
                    'DropDownListCascading(fk_Documento_3, CType(Session("TableDocumentos"), DataTable), "id_documento", "nombre_documento", "fk_entidad='" & fk_entidad.SelectedValue & "'", "fk_proyecto='" & fk_proyecto.SelectedValue & "'", "fk_esquema='" & id_esquema.Text & "'")

                    'DropDownListCascading(fk_Campo_1, GlobalData.Tables(1), "id_Campo", "nombre_campo", "fk_documento=" & fk_Documento_1.SelectedValue)
                    'DropDownListCascading(fk_Campo_2, GlobalData.Tables(1), "id_Campo", "nombre_campo", "fk_documento=" & fk_Documento_2.SelectedValue)
                    'DropDownListCascading(fk_Campo_3, GlobalData.Tables(1), "id_Campo", "nombre_campo", "fk_documento=" & fk_Documento_3.SelectedValue)

                Catch : End Try

                If fk_servidor.Text <> "" Then
                    Servidor.Text = "OK"
                Else
                    Servidor.Text = ""
                End If

            Catch ex As Exception
                Throw
            Finally
                If dbmCore IsNot Nothing Then dbmCore.Connection_Close()
            End Try


        End Sub

        Public Sub CoreGridViewChange()
            Session("GridChanged") = 1
        End Sub

        Public Sub LimpiarEsquema()
            grd_Documentos.DataSource = Nothing
            grd_Documentos.DataBind()
            nombre_documento.Text = ""
            fk_tipologia.SelectedIndex = -1

            fk_proyecto_llave.SelectedIndex = -1
            fk_documento.SelectedIndex = -1
            fk_campo.SelectedIndex = -1
            grd_llaves.DataSource = Nothing
            grd_llaves.DataBind()

            Dim TableDocs As DataTable = CType(Session("TableDocumentos"), DataTable)
            TableDocs.Clear()
            Session("TableDocumentos") = TableDocs

            Dim TableLlaves As DataTable = CType(Session("TablaInicialLlaves"), DataTable)
            TableLlaves.Clear()
            Session("TablaInicialLlaves") = TableLlaves



            ' =========================
            ' NUEVO: limpiar pestaña "Identificación para Asociación"
            ' =========================
            fk_Tipologia0.SelectedIndex = -1
            inicia_Por.Text = ""
            contiene_and.Text = ""
            contiene_or.Text = ""
            no_Contiene.Text = ""
            finaliza_Por.Text = ""
            no_Finaliza_En.Text = ""

            Dim tableAsoc As DataTable = TryCast(Session("TableAsociaciones"), DataTable)
            If tableAsoc Is Nothing Then
                tableAsoc = AsociacionesTable()
            Else
                tableAsoc.Clear()
            End If
            Session("TableAsociaciones") = tableAsoc
            grd_Asociaciones.DataSource = tableAsoc
            grd_Asociaciones.DataBind()


        End Sub

#End Region

#Region "Funciones"

        Public Function DocumentosTable() As DataTable
            Dim dbmCore As DBCoreDataBaseManager = Nothing
            Dim Table As DataTable

            Try
                dbmCore = New DBCoreDataBaseManager(ConnectionString.Core)
                dbmCore.Connection_Open(MySesion.Usuario.id)

                Table = dbmCore.SchemaConfig.TBL_Documento.DBGet(Nothing)

            Catch ex As Exception
                Throw
            Finally
                If dbmCore IsNot Nothing Then dbmCore.Connection_Close()
            End Try


            Try
                Dim view As DataView = Table.DefaultView
                view.RowFilter = "fk_entidad='" & fk_entidad.SelectedValue & "' and fk_esquema='" & id_esquema.Text & "' and fk_proyecto='" & fk_proyecto.SelectedValue & "'"
            Catch
                Throw
            End Try

            Return Table.DefaultView.ToTable(True, "id_documento", "fk_tipologia", "nombre_documento")
        End Function

        Public Function LlavesTable() As DataTable
            Dim dbmCore As DBCoreDataBaseManager = Nothing
            Dim Table As DataTable
            Try
                dbmCore = New DBCoreDataBaseManager(ConnectionString.Core)
                dbmCore.Connection_Open(MySesion.Usuario.id)

                Table = dbmCore.Schemadbo.CTA_Esquema_Campos_Llave.DBFindByfk_Entidadfk_Proyectofk_Esquemafk_Proyecto_Llave(CShort(fk_entidad.SelectedValue), DShort(fk_proyecto.SelectedValue), DShort(id_esquema.Text), Nothing)

            Catch ex As Exception
                Throw
            Finally
                If dbmCore IsNot Nothing Then dbmCore.Connection_Close()
            End Try

            Session("TablaInicialLlaves") = Table
            Return Table
        End Function

#End Region

#Region "Declaraciones"

        Dim Esquema As String
        Dim Entidad As String
        Dim Tabla As String
        Dim container As Object
        Dim grilla As CoreGridView

#End Region

#Region "Automatic"

        Protected Sub FindAutomatic() Handles MyBase.CommandActionFind
            Dim Objetos As New ParameterCollection
            Dim PanelFind As Panel = pnlFiltro
            Dim query As New StringBuilder() ' = "select * from " & Esquema & "." & Tabla

            query.AppendLine("SELECT")
            query.AppendLine("CAST(ENT.id_Entidad AS VARCHAR(15)) + '-' + ENT.Nombre_Entidad AS fk_Entidad")
            query.AppendLine(",CAST(PRO.id_Proyecto AS VARCHAR(15))  + '-' + PRO.Nombre_Proyecto as fk_Proyecto")
            query.AppendLine(",ESQ.id_Esquema")
            query.AppendLine(",ESQ.Nombre_Esquema")
            query.AppendLine(",CAST(ENTS.id_Entidad AS VARCHAR(15)) + '-' + ENTS.Nombre_Entidad AS fk_Entidad_Servidor")
            query.AppendLine(",CAST(SER.id_Servidor AS VARCHAR(15)) + '-' + SER.Nombre_Servidor AS fk_Servidor")
            'query.AppendLine(",CAST(SERV.id_Servidor_Volumen AS VARCHAR(15)) + '-' + SERV.Nombre_Servidor_Volumen as fk_Servidor_Volumen")
            query.AppendLine(",CAST(RES.id_Restriccion_Monto AS VARCHAR(15)) + '-' + RES.Nombre_Restriccion_Monto as fk_Restriccion_Monto")
            query.AppendLine(",ESQ.Valor_Restriccion_Monto")
            query.AppendLine(",CAST(DOC1.id_Documento AS VARCHAR(15)) + '-' + DOC1.Nombre_Documento as fk_Documento_1")
            query.AppendLine(",CAST(CAM1.id_Campo AS VARCHAR(15)) + '-' + CAM1.Nombre_Campo as fk_Campo_1")
            query.AppendLine(",CAST(DOC2.id_Documento AS VARCHAR(15)) + '-' + DOC2.Nombre_Documento as fk_Documento_2")
            query.AppendLine(",CAST(CAM2.id_Campo AS VARCHAR(15)) + '-' + CAM2.Nombre_Campo as fk_Campo_2")
            query.AppendLine(",CAST(DOC3.id_Documento AS VARCHAR(15)) + '-' + DOC3.Nombre_Documento as fk_Documento_3")
            query.AppendLine(",CAST(CAM3.id_Campo AS VARCHAR(15)) + '-' + CAM3.Nombre_Campo as fk_Campo_3")
            query.AppendLine("")
            query.AppendLine("FROM [DB_Miharu.Core].[Config].TBL_Esquema ESQ")
            query.AppendLine("INNER JOIN [DB_Miharu.Core].[Security].CTA_Entidad ENT")
            query.AppendLine("ON ESQ.fk_Entidad = ENT.id_Entidad")
            query.AppendLine("INNER JOIN [DB_Miharu.Core].[Config].TBL_Proyecto PRO")
            query.AppendLine("ON PRO.fk_Entidad = ENT.id_Entidad")
            query.AppendLine("AND PRO.id_Proyecto = ESQ.fk_Proyecto")
            query.AppendLine("INNER JOIN [DB_Miharu.Core].[Security].CTA_Entidad ENTS")
            query.AppendLine("ON ENTS.id_Entidad = ESQ.fk_Entidad_Servidor")
            query.AppendLine("INNER JOIN [DB_Miharu.Core].[Imaging].TBL_Servidor SER")
            query.AppendLine("ON SER.fk_Entidad = ESQ.fk_Entidad_servidor")
            query.AppendLine("AND SER.id_Servidor = ESQ.fk_Servidor")
            'query.AppendLine("INNER JOIN Imaging.TBL_Servidor_Volumen SERV")
            'query.AppendLine("ON SER.fk_Entidad = SERV.fk_Entidad")
            'query.AppendLine("AND SER.id_Servidor = SERV.fk_Servidor")
            'query.AppendLine("AND SERV.id_Servidor_Volumen = ESQ.fk_Servidor_Volumen")
            query.AppendLine("INNER JOIN [DB_Miharu.Core].[Config].TBL_Restriccion_Monto RES")
            query.AppendLine("ON RES.id_Restriccion_Monto = ESQ.fk_Restriccion_Monto")
            query.AppendLine("LEFT JOIN [DB_Miharu.Core].[Config].TBL_Documento DOC1")
            query.AppendLine("ON DOC1.id_Documento = ESQ.fk_Documento_1")
            query.AppendLine("LEFT JOIN [DB_Miharu.Core].[Config].TBL_Campo CAM1")
            query.AppendLine("ON CAM1.fk_Documento = DOC1.id_Documento")
            query.AppendLine("AND CAM1.id_Campo = ESQ.fk_Campo_1")
            query.AppendLine("LEFT JOIN [DB_Miharu.Core].[Config].TBL_Documento DOC2")
            query.AppendLine("ON DOC2.id_Documento = ESQ.fk_Documento_2")
            query.AppendLine("LEFT JOIN [DB_Miharu.Core].[Config].TBL_Campo CAM2")
            query.AppendLine("ON CAM2.fk_Documento = DOC2.id_Documento")
            query.AppendLine("AND CAM2.id_Campo = ESQ.fk_Campo_2")
            query.AppendLine("LEFT JOIN [DB_Miharu.Core].[Config].TBL_Documento DOC3")
            query.AppendLine("ON DOC3.id_Documento = ESQ.fk_Documento_3")
            query.AppendLine("LEFT JOIN [DB_Miharu.Core].[Config].TBL_Campo CAM3")
            query.AppendLine("ON CAM3.fk_Documento = DOC3.id_Documento")
            query.AppendLine("AND CAM3.id_Campo = ESQ.fk_Campo_3")
            query.AppendLine(",ESQ.Indexacion_Automatica")
            query.AppendLine(",ESQ.UsaNotificacionCargue")
            query.AppendLine(",CAST(NOTI.id_Notificacion AS VARCHAR(15)) + '-' + NOTI.Nombre_Notificacion as fk_Notificacion")
            query.AppendLine(",ESQ.usa_Cargue_xDocumento")
            query.AppendLine(",ESQ.fk_Parametro_Escaner")
            query.AppendLine(",ESQ.formato_Archivo_Cargue")
            query.AppendLine(",ESQ.PesoImagenenBlanco")
            query.AppendLine(",ESQ.usa_NombreArchivo_Asociado_Tipologia")
            query.AppendLine("LEFT JOIN [DB_Miharu.Core].[Config].TBL_Notificacion NOTI")
            query.AppendLine("ON NOTI.id_Notificacion = ESQ.fk_Notificacion")



            For Each Controlfind As UI.Control In PanelFind.Controls
                Try
                    If Controlfind.ID.IndexOf("find_", System.StringComparison.Ordinal) >= 0 Then
                        If CStr(GetControlValue(PanelFind, Controlfind.ID)) <> "" Then
                            Objetos.Add(Controlfind.ID.Replace("find_", ""), CStr(GetControlValue(PanelFind, Controlfind.ID)))
                        End If
                    End If

                Catch : End Try
            Next

            If Objetos.Count > 0 Then query.AppendLine(" WHERE ")
            Dim where(Objetos.Count - 1) As String
            Dim i As Integer = 0

            For Each parametro As Parameter In Objetos
                where(i) += "ESQ." & parametro.Name & " = '" & parametro.DefaultValue & "'"
                i += 1
            Next

            query.AppendLine(Join(where, " AND "))

            Dim dbmCore As DBCoreDataBaseManager = Nothing

            Try
                dbmCore = New DBCoreDataBaseManager(ConnectionString.Core)
                dbmCore.Connection_Open(MySesion.Usuario.id)

                Dim table As DataTable = dbmCore.DataBase.ExecuteQueryGet(query.ToString)
                grdData.DataSource = table
                grdData.DataBind()
                GridData = table

            Catch ex As Exception
                Throw
            Finally
                If dbmCore IsNot Nothing Then dbmCore.Connection_Close()
            End Try

            CurrentMasterTab = MasterTabType.Grid
        End Sub

        Public Sub LoadAutomatic()
            If Not IsPostBack Then
                Dim imgB As ImageButton = CType(MyMasterPage.ToolControl.FindControl("imgSave"), ImageButton)
                imgB.ValidationGroup = "Guardar"
                Session("TableForm") = DataDictionary(Esquema, Tabla)
            End If
        End Sub

        Protected Sub BtnGuardar_Click() Handles MyBase.CommandActionSave

            Dim dm As New DBCoreDataBaseManager(MyBase.ConnectionString.Core)
            Dim GuardadoCorrecto As Boolean = True

            Try
                If fk_proyecto.SelectedValue = "-1" Then Throw New Exception("Debe seleccionar un Proyecto")

                Dim table As DataTable = CType(Session("TableForm"), DataTable)
                Dim NextId As Integer = -1

                If SaveType = SaveType.Insert Then NextId = CreateInsert(MySesion.Usuario.id, container, table)
                If SaveType = SaveType.Update Then : CreateUpdate(MySesion.Usuario.id, container, table) : NextId = CInt(id_esquema.Text) : End If

                dm.Connection_Open(MySesion.Usuario.id)
                dm.Transaction_Begin()

                'Guarda cada uno de los documentos
                Dim TableDoc As DataTable = CType(Session("TableDocumentos"), DataTable)
                Dim RegistroDoc As New SchemaConfig.TBL_DocumentoType
                RegistroDoc.fk_Entidad = CShort(fk_entidad.SelectedValue)
                RegistroDoc.fk_Esquema = CShort(NextId)
                RegistroDoc.fk_Proyecto = CShort(fk_proyecto.SelectedValue)

                For Each Row As DataRow In DocumentosTable.Rows
                    For Each rowVS As DataRow In TableDoc.Rows
                        Dim ViewDocs As New DataView(TableDoc)
                        ViewDocs.RowFilter = "id_Documento = " & rowVS("id_Documento").ToString()

                        If ViewDocs.ToTable(True).Rows.Count = 0 Then
                            dm.SchemaConfig.TBL_Documento.DBDelete(CShort(Row("id_Documento").ToString))
                            Exit For
                        End If
                    Next
                Next

                For Each row As DataRow In TableDoc.Rows
                    RegistroDoc.id_Documento = CShort(row("id_Documento").ToString)
                    RegistroDoc.fk_Tipologia = CShort(row("fk_Tipologia").ToString)
                    RegistroDoc.Nombre_Documento = row("Nombre_Documento").ToString

                    If RegistroDoc.id_Documento.Value = -1 Or RegistroDoc.id_Documento.Value = -2 Then
                        RegistroDoc.id_Documento = dm.SchemaConfig.TBL_Documento.DBNextId
                        dm.SchemaConfig.TBL_Documento.DBInsert(RegistroDoc)
                    End If
                Next

                'Guarda cada una de las llaves de los esquemas
                Dim RegistroLlaves As New SchemaConfig.TBL_Esquema_LlaveType
                RegistroLlaves.fk_Entidad = CShort(fk_entidad.SelectedValue)
                RegistroLlaves.fk_Esquema = CShort(NextId)
                RegistroLlaves.fk_Proyecto = CShort(fk_proyecto.SelectedValue)

                For Each row As DataRow In CType(Session("TablaInicialLlaves"), DataTable).Rows
                    Try
                        dm.SchemaConfig.TBL_Esquema_Llave.DBDelete(RegistroLlaves.fk_Entidad,
                                                                   RegistroLlaves.fk_Proyecto,
                                                                   CShort(NextId),
                                                                   CShort(row("fk_Proyecto_Llave").ToString))
                    Catch : End Try
                Next

                For Each row As DataRow In CType(Session("TableLlaves"), DataTable).Rows
                    RegistroLlaves.fk_Documento = CInt(row("fk_documento").ToString)
                    RegistroLlaves.fk_Campo = CShort(row("fk_Campo").ToString)
                    RegistroLlaves.fk_Proyecto_Llave = CShort(row("fk_Proyecto_Llave").ToString)

                    Try
                        dm.SchemaConfig.TBL_Esquema_Llave.DBDelete(RegistroLlaves.fk_Entidad,
                                                                   RegistroLlaves.fk_Proyecto,
                                                                   CShort(NextId),
                                                                   CShort(row("fk_Proyecto_Llave").ToString))
                    Catch : End Try

                    Try
                        dm.SchemaConfig.TBL_Esquema_Llave.DBInsert(RegistroLlaves)
                    Catch : End Try
                Next

                ' =========================
                ' NUEVO: guardar reglas de "Identificación para Asociación"
                ' Requiere confirmar nombre de tabla/objeto DAL y campos exactos.
                ' Ejemplo orientativo:
                ' =========================
                Dim RegistroAsoc As New SchemaConfig.TBL_Esquema_Asociacion_TipologiaType
                RegistroAsoc.fk_Entidad = CShort(fk_entidad.SelectedValue)
                RegistroAsoc.fk_Esquema = CShort(NextId)
                RegistroAsoc.fk_Proyecto = CShort(fk_proyecto.SelectedValue)
                RegistroAsoc.id_Asociacion_Tipologia = CShort(fk_proyecto.SelectedValue)
                RegistroAsoc.fk_Tipologia = CShort("fk_tipologia0")

                ' Primero eliminar registros existentes (si el esquema se actualiza)
                Try
                    dm.SchemaConfig.TBL_Esquema_Asociacion_Tipologia.DBDelete(RegistroAsoc.fk_Entidad, RegistroAsoc.fk_Proyecto, RegistroAsoc.fk_Esquema, RegistroAsoc.id_Asociacion_Tipologia, RegistroAsoc.id_Asociacion_Tipologia)
                Catch : End Try

                ' Insertar los de la tabla en Session
                For Each row As DataRow In CType(Session("TableAsociaciones"), DataTable).Rows
                    RegistroAsoc.fk_Entidad = CShort(fk_entidad.SelectedValue)
                    RegistroAsoc.fk_Proyecto = CShort(fk_proyecto.SelectedValue)
                    RegistroAsoc.fk_Esquema = CShort(NextId)
                    RegistroAsoc.fk_Tipologia = CShort(row("fk_tipologia"))
                    RegistroAsoc.inicia_por = row("inicia_por").ToString()
                    RegistroAsoc.contiene_and = row("contiene_and").ToString()
                    RegistroAsoc.contiene_or = row("contiene_or").ToString()
                    RegistroAsoc.no_Contiene = row("no_contiene").ToString()
                    RegistroAsoc.finaliza_Por = row("finaliza_por").ToString()
                    RegistroAsoc.no_Finaliza_En = row("no_finaliza_en").ToString()
                    dm.SchemaConfig.TBL_Esquema_Asociacion_Tipologia.DBInsert(RegistroAsoc)
                Next

                LimpiarControles(container, table, Entidad)

                dm.Transaction_Commit()
                CurrentMasterTab = MasterTabType.Filter
            Catch ex As Exception
                dm.Transaction_Rollback()
                GuardadoCorrecto = False
                MyMasterPage.ShowMessage(ex.Message, MsgBoxIcon.IconInformation)
            Finally
                dm.Connection_Close()
            End Try

            If GuardadoCorrecto Then
                MyMasterPage.MasterGridPanel.Update()
                grdData.DataSource = Nothing
                grdData.DataBind()

                LimpiarEsquema()
                Config_Page()
            End If
        End Sub

        Protected Sub BtnNuevo_Click() Handles MyBase.CommandActionNew
            Try
                Dim table As DataTable = CType(Session("TableForm"), DataTable)
                LimpiarControles(container, table, Entidad)
                CurrentMasterTab = MasterTabType.Detail
                SaveType = SaveType.Insert

                LimpiarEsquema()
            Catch ex As Exception
                showErrorPage(ex)
            End Try
        End Sub

        Protected Sub GrdData_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles grdData.SelectedIndexChanged
            IndexChanged()
        End Sub

        Protected Sub BtnEdit_Click() Handles MyBase.CommandActionEdit
            IndexChanged()
        End Sub

        Public Sub IndexChanged()
            'Llenacombo(fk_Campo_1, GlobalData.Tables(1), "id_Campo", "nombre_campo")
            'Llenacombo(fk_Campo_2, GlobalData.Tables(1), "id_Campo", "nombre_campo")
            'Llenacombo(fk_Campo_3, GlobalData.Tables(1), "id_Campo", "nombre_campo")

            'Llenacombo(fk_Documento_1, GlobalData.Tables(2), "id_Documento", "nombre_documento")
            'Llenacombo(fk_Documento_2, GlobalData.Tables(2), "id_Documento", "nombre_documento")
            'Llenacombo(fk_Documento_3, GlobalData.Tables(2), "id_Documento", "nombre_documento")

            Try
                If grdData.SelectedIndex > -1 Then
                    CoreGridViewChange()

                    CoreGridViewLinkControls(GridData, grilla.SelectedIndex, container)
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

        Protected Sub ChkAsociacionTipologia_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles usa_NombreArchivo_Asociado_Tipologia.CheckedChanged
            Me.TabPanelAsociacion.Visible = usa_NombreArchivo_Asociado_Tipologia.Checked
        End Sub


#End Region

        ' =========================
        ' NUEVO: Tabla en memoria para asociaciones (Session("TableAsociaciones"))
        ' =========================
        Private Function AsociacionesTable() As DataTable
            Dim t As New DataTable("Asociaciones")
            ' Sugeridos: ajusta el tipo de dato si lo requieres
            t.Columns.Add("id", GetType(Integer)) ' temporal (-1/-2 para nuevos)
            t.Columns.Add("fk_tipologia", GetType(Short))
            t.Columns.Add("inicia_por", GetType(String))
            t.Columns.Add("contiene_and", GetType(String))
            t.Columns.Add("contiene_or", GetType(String))
            t.Columns.Add("no_contiene", GetType(String))
            t.Columns.Add("finaliza_por", GetType(String))
            t.Columns.Add("no_finaliza_en", GetType(String))
            Return t
        End Function

        ' =========================
        ' NUEVO: Habilitar campos de reglas al seleccionar tipología en la pestaña de Asociación
        ' =========================
        Protected Sub Fk_Tipologia0_SelectedIndexChanged(sender As Object, e As EventArgs) Handles fk_Tipologia0.SelectedIndexChanged
            Try
                ' Habilita los campos de reglas cuando haya una tipología seleccionada
                Dim habilitar As Boolean = (fk_Tipologia0.SelectedValue IsNot Nothing AndAlso fk_Tipologia0.SelectedValue <> "-1" AndAlso fk_Tipologia0.SelectedValue <> "")
                inicia_Por.Enabled = habilitar
                contiene_and.Enabled = habilitar
                contiene_or.Enabled = habilitar
                no_Contiene.Enabled = habilitar
                finaliza_Por.Enabled = habilitar
                no_Finaliza_En.Enabled = habilitar

                ' Opcional: limpiar textos al cambiar tipología
                If habilitar Then
                    inicia_Por.Text = ""
                    contiene_and.Text = ""
                    contiene_or.Text = ""
                    no_Contiene.Text = ""
                    finaliza_Por.Text = ""
                    no_Finaliza_En.Text = ""
                End If

                MyMasterPage.MasterDetailPanel.Update()
            Catch ex As Exception
                showErrorPage(ex)
            End Try
        End Sub


        ' =========================
        ' NUEVO: Agregar asociación (botón check de la pestaña)
        ' ID del botón en ASPX: grd_llaves_add0
        ' =========================
        Protected Sub Grd_llaves_add0_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles grd_llaves_add0.Click
            Try
                If fk_Tipologia0.SelectedValue = "-1" OrElse String.IsNullOrWhiteSpace(fk_Tipologia0.SelectedValue) Then
                    Throw New Exception("Debe seleccionar una tipología.")
                End If

                Dim table As DataTable = TryCast(Session("TableAsociaciones"), DataTable)
                If table Is Nothing Then
                    table = AsociacionesTable()
                Else
                    table = clonarDataTable(table) ' respeta tu patrón
                End If

                ' Regla: 1 asociación por tipología (ajusta si quieres permitir varias)
                Dim view As New DataView(table)
                view.RowFilter = "fk_tipologia = " & CInt(fk_Tipologia0.SelectedValue).ToString()
                If view.ToTable(True).Rows.Count > 0 Then
                    Throw New Exception("Ya existe una regla de asociación para la tipología seleccionada.")
                End If

                ' Crear fila
                Dim row As DataRow = table.NewRow()
                row("id") = -2 ' temporal
                row("fk_tipologia") = CShort(fk_Tipologia0.SelectedValue)
                row("inicia_por") = inicia_Por.Text
                row("contiene_and") = contiene_and.Text
                row("contiene_or") = contiene_or.Text
                row("no_contiene") = no_Contiene.Text
                row("finaliza_por") = finaliza_Por.Text
                row("no_finaliza_en") = no_Finaliza_En.Text

                table.Rows.Add(row)
                Session("TableAsociaciones") = table

                ' Muestra columnas clave (ajusta las que quieras ver primero)
                LlenaGrilla(grd_Asociaciones, table, "fk_tipologia", "inicia_por", "finaliza_por")

                ' Limpia UI
                fk_Tipologia0.SelectedIndex = -1
                inicia_Por.Text = ""
                contiene_and.Text = ""
                contiene_or.Text = ""
                no_Contiene.Text = ""
                finaliza_Por.Text = ""
                no_Finaliza_En.Text = ""

            Catch ex As Exception
                MyMasterPage.ShowMessage(ex.Message, MsgBoxIcon.IconInformation, "")
            End Try
        End Sub

        ' =========================
        ' NUEVO: Eliminar asociación desde la grilla
        ' =========================
        Private Sub Grd_Asociaciones_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grd_Asociaciones.RowCommand
            Try
                If e.CommandSource.GetType() Is GetType(Web.UI.WebControls.ImageButton) Then
                    Dim table As DataTable = CType(Session("TableAsociaciones"), DataTable)
                    Dim Reg As Integer = CType(sender, CoreGridView).PreSelectedIndex

                    table.Rows(Reg).Delete()
                    table.AcceptChanges()

                    Session("TableAsociaciones") = table

                    ' Rebind
                    LlenaGrilla(grd_Asociaciones, table, "fk_tipologia", "inicia_por", "finaliza_por")
                End If
            Catch ex As Exception
                showErrorPage(ex)
            End Try
        End Sub




    End Class
End Namespace