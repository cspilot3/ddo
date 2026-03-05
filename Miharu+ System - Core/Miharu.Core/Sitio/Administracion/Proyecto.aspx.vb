
Imports DBCore
Imports Miharu.Core.Clases

Namespace Sitio.Administracion


    Public Class Proyecto
        Inherits FormBase

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            grilla = grdData
            container = pnlDetalle
            Esquema = "config"
            Entidad = CStr(MySesion.Entidad.id)
            Tabla = "TBL_proyecto"

            fk_entidad.Enabled = AccesoEntidad
            Config_Page()
            LoadAutomatic()

            Dim imgB As ImageButton = CType(MyMasterPage.ToolControl.FindControl("imgEdit"), ImageButton)
            If CurrentMasterTab = MasterTabType.Grid Then
                imgB.Enabled = False
            Else
                imgB.Enabled = True
            End If

        End Sub

        Protected Sub Page_LoadComplete() Handles Me.LoadComplete
            Config_Page()
        End Sub

        Public Sub Config_Page()

            Dim dbmCore As DBCoreDataBaseManager = Nothing

            Try
                dbmCore = New DBCoreDataBaseManager(ConnectionString.Core)
                dbmCore.Connection_Open(MySesion.Usuario.id)

                If Not IsPostBack Then
                    'LlenaGrilla(grdData, CreateSelect(MySesion.Usuario.id, Esquema, Tabla))
                    Llenacombo(fk_entidad, dbmCore.Schemadbo.CTA_Entidad.DBGet(), "id_entidad", "nombre_entidad")
                    Llenacombo(fk_campo_tipo, dbmCore.SchemaConfig.TBL_Campo_Tipo.DBGet(Nothing), "id_campo_tipo", "nombre_campo_tipo")

                    Llenacombo(find_fk_Entidad, dbmCore.Schemadbo.CTA_Entidad.DBGet(), "id_entidad", "nombre_entidad")

                    Llenacombo(fk_Folder_Tipo, dbmCore.SchemaCustody.TBL_Folder_Tipo.DBGet(Nothing), "id_folder_tipo", "nombre_folder_tipo")
                    Llenacombo(fk_Caja_Defecto, dbmCore.SchemaCustody.TBL_Caja_Tipo.DBGet(Nothing), "id_caja_tipo", "nombre_caja_tipo")
                    
                    pTableLlaves = TableLlaves_()
                End If

                If CInt(Session("GridChange")) = 1 Then
                    pTableLlaves = TableLlaves_()

                    LlenaGrilla(grd_llaves, pTableLlaves, False, False)
                    Session("GridChange") = 0
                End If

            Catch ex As Exception
                Throw
            Finally
                If dbmCore IsNot Nothing Then dbmCore.Connection_Close()
            End Try
            

        End Sub

        Private Sub grd_llaves_delete(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grd_llaves.RowCommand
            Try
                If e.CommandSource.GetType() Is GetType(Web.UI.WebControls.ImageButton) Then
                    Dim Reg As Integer = CType(sender, CoreGridView).PreSelectedIndex

                    pTableLlaves.Rows(Reg).Delete()
                    pTableLlaves.AcceptChanges()

                    grd_llaves.DataSource = pTableLlaves
                    grd_llaves.DataBind()
                End If
            Catch ex As Exception
                showErrorPage(ex)
            End Try
        End Sub

        Public Sub CoreGridViewChange()
            Session("GridChange") = 1
        End Sub

        Protected Sub grd_llaves_add_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles grd_llaves_add.Click
            Try
                Dim tablea As DataTable = clonarDataTable(pTableLlaves)
                tablea = tablea.DefaultView.ToTable(True, "nombre_proyecto_llave", "fk_campo_tipo", "id_proyecto_llave")

                If fk_campo_tipo.SelectedValue = "-1" Then Throw New Exception("Debe seleccionar una tipologia.")
                Dim row As DataRow = tablea.NewRow
                row("nombre_proyecto_llave") = nombre_proyecto_llave.Text
                row("fk_campo_tipo") = fk_campo_tipo.SelectedValue
                row("id_proyecto_llave") = -1
                tablea.Rows.Add(row)

                pTableLlaves = tablea

                LlenaGrilla(grd_llaves, pTableLlaves, False, False)
            Catch ex As Exception
                MyMasterPage.ShowMessage(ex.Message, MsgBoxIcon.IconInformation, "")
            End Try

        End Sub

        Public Function TableLlaves_() As DataTable
            Dim dbmCore As DBCoreDataBaseManager = Nothing
            Dim TableLlaves As DataTable = Nothing
            Try
                dbmCore = New DBCoreDataBaseManager(ConnectionString.Core)
                dbmCore.Connection_Open(MySesion.Usuario.id)

                Dim table As DataTable = dbmCore.SchemaConfig.TBL_Proyecto_Llave.DBGet(CShort(IIf(fk_entidad.SelectedValue = "", 0, fk_entidad.SelectedValue)), CShort(IIf(id_proyecto.Text = "", 0, id_proyecto.Text)), Nothing)

                Dim SourceData As DataTable = table
                Dim Filtro As String = "fk_entidad='" & fk_entidad.SelectedValue & "' and " & "fk_proyecto='" & id_proyecto.Text & "'"
                Dim View As DataView = SourceData.DefaultView
                View.RowFilter = Filtro
                SourceData = View.ToTable
                TableLlaves = SourceData.DefaultView.ToTable(True, "nombre_proyecto_llave", "fk_campo_tipo", "id_proyecto_llave")

            Catch ex As Exception
                Throw
            Finally
                If dbmCore IsNot Nothing Then dbmCore.Connection_Close()
            End Try

            Return TableLlaves
        End Function

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
            Dim query As String = "select * from " & Esquema & "." & Tabla

            For Each Controlfind As UI.Control In PanelFind.Controls
                Try
                    If Controlfind.ID.IndexOf("find_", System.StringComparison.Ordinal) >= 0 Then
                        If CStr(GetControlValue(PanelFind, Controlfind.ID)) <> "" Then
                            Objetos.Add(Controlfind.ID.Replace("find_", ""), CStr(GetControlValue(PanelFind, Controlfind.ID)))
                        End If
                    End If

                Catch : End Try
            Next

            If Objetos.Count > 0 Then query += " where "
            Dim where(Objetos.Count - 1) As String
            Dim i As Integer = 0

            For Each parametro As Parameter In Objetos
                where(i) += parametro.Name & " = '" & parametro.DefaultValue & "'"
                i += 1
            Next

            query += " " & Join(where, " and ")

            Dim dbmCore As DBCoreDataBaseManager = Nothing

            Try
                dbmCore = New DBCoreDataBaseManager(ConnectionString.Core)
                dbmCore.Connection_Open(MySesion.Usuario.id)
                
                Dim table As DataTable = dbmCore.DataBase.ExecuteQueryGet(query)

                LlenaGrilla(grdData, table, True, True)
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

        Protected Sub btnGuardar_Click() Handles MyBase.CommandActionSave
            Dim EliminarTable As DataTable = TableLlaves_()

            Dim dbmCore As DBCoreDataBaseManager = Nothing
            Try
                dbmCore = New DBCoreDataBaseManager(ConnectionString.Core)
                dbmCore.Connection_Open(MySesion.Usuario.id)
                dbmCore.Transaction_Begin()

                Dim table As DataTable = CType(Session("TableForm"), DataTable)
                Dim nextid As Integer

                If SaveType = SaveType.Insert Then nextid = CreateInsert(MySesion.Usuario.id, container, table, MyBase.ConnectionString.Core)
                If SaveType = SaveType.Update Then : CreateUpdate(MySesion.Usuario.id, container, table) : nextid = CInt(id_proyecto.Text) : End If

                Dim Registro As New SchemaConfig.TBL_Proyecto_LlaveType
                Registro.fk_Entidad = CShort(fk_entidad.SelectedValue)
                Registro.fk_Proyecto = CShort(nextid)

                'Elimina las llaves eliminadas de la grilla
                For Each row As DataRow In EliminarTable.Rows
                    Dim View As New DataView(pTableLlaves)
                    View.RowFilter = "id_Proyecto_Llave = " & row("id_Proyecto_Llave").ToString

                    If View.ToTable().Rows.Count = 0 Then
                        Try
                            dbmCore.SchemaConfig.TBL_Proyecto_Llave.DBDelete(Registro.fk_Entidad, Registro.fk_Proyecto, CShort(row("id_Proyecto_Llave").ToString))
                        Catch ex As Exception
                            Throw New Exception("Imposible eliminar la llave del proyecto ya que esta es utilizada por un esquema")
                        End Try
                    End If
                Next

                'Crea las llaves de la grilla
                Dim tableLlaves1 As DataTable = pTableLlaves
                For Each row As DataRow In tableLlaves1.Rows
                    Try
                        dbmCore.SchemaConfig.TBL_Proyecto_Llave.DBDelete(Registro.fk_Entidad, Registro.fk_Proyecto, CShort(row("id_Proyecto_Llave").ToString))

                        Registro.id_Proyecto_Llave = dbmCore.SchemaConfig.TBL_Proyecto_Llave.DBNextId(Registro.fk_Entidad, Registro.fk_Proyecto)
                        Registro.fk_Campo_Tipo = CByte(row("fk_campo_tipo"))
                        Registro.Nombre_Proyecto_Llave = row("nombre_proyecto_llave").ToString
                        dbmCore.SchemaConfig.TBL_Proyecto_Llave.DBInsert(Registro)
                    Catch : End Try
                Next

                CurrentMasterTab = MasterTabType.Filter
                MyMasterPage.MasterGridPanel.Update()
                grdData.DataSource = Nothing
                grdData.DataBind()
                LimpiarControles(container, table, Entidad)
                dbmCore.Transaction_Commit()

            Catch ex As Exception
                If dbmCore IsNot Nothing Then dbmCore.Transaction_Rollback()
                MyMasterPage.ShowMessage(ex.Message, MsgBoxIcon.IconError, "Proyectos")
            Finally
                If dbmCore IsNot Nothing Then dbmCore.Connection_Close()
            End Try
        End Sub

        Protected Sub BtnNuevo_Click() Handles MyBase.CommandActionNew
            Try
                pTableLlaves.Rows.Clear()
                grd_llaves.DataSource = Nothing
                grd_llaves.DataBind()

                Dim table As DataTable = CType(Session("TableForm"), DataTable)

                LimpiarControles(container, table, Entidad)
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

#End Region

#Region "Propiedades"

        Public Property pTableLlaves As DataTable
            Get
                Return CType(Session("TableLlaves"), DataTable)
            End Get
            Set(ByVal value As DataTable)
                Session("TableLlaves") = value
            End Set
        End Property

#End Region

    End Class
End Namespace