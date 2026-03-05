Imports DBCore
Imports Miharu.Core.Clases

Namespace Sitio.Administracion

    Public Class CajaProceso
        Inherits FormBase

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            grilla = grdData
            container = panelDetalle
            Esquema = "Custody"
            Entidad = CStr(MySesion.Entidad.id)
            Tabla = "TBL_Caja"

            LoadAutomatic()
        End Sub
        Protected Sub Load_Complete() Handles Me.LoadComplete
            Config_Page()
        End Sub
        Public Sub Config_Page()
            Es_Proceso.Checked = True
            fk_Estado.Text = "10"

            If Not IsPostBack Then
                Dim dmCore As New DBCoreDataBaseManager(ConnectionString.Core)
                dmCore.Connection_Open(MySesion.Usuario.id)

                Dim data As New DataSet
                Dim Entidad = dmCore.SchemaSecurity.CTA_Entidad.DBGet()
                Dim Sede = dmCore.SchemaSecurity.CTA_Sede.DBGet()
                Dim TipoCaja = dmCore.SchemaCustody.TBL_Caja_Tipo.DBGet(Nothing)

                data.Tables.Add(Sede)
                GlobalData = data

                Llenacombo(find_fk_Entidad, Entidad, Entidad.id_EntidadColumn.ColumnName, Entidad.Nombre_EntidadColumn.ColumnName)
                Llenacombo(fk_Entidad, Entidad, Entidad.id_EntidadColumn.ColumnName, Entidad.Nombre_EntidadColumn.ColumnName)
                Llenacombo(fk_Caja_Tipo, TipoCaja, TipoCaja.id_Caja_TipoColumn.ColumnName, TipoCaja.Nombre_Caja_TipoColumn.ColumnName)

                'Disabled_Menu_Form(Me.GetType().BaseType.FullName, "1.4.5")
                dmCore.Connection_Close()
            End If

            DropDownListCascading(find_fk_Sede, GlobalData.Tables(0), "id_Sede", "Nombre_Sede", "fk_Entidad = " & find_fk_Entidad.SelectedValue)
            DropDownListCascading(fk_Sede, GlobalData.Tables(0), "id_Sede", "Nombre_Sede", "fk_Entidad = " & fk_Entidad.SelectedValue)
        End Sub

        Public Sub CoreGridViewChange()
        End Sub

        Public Function Valida() As Boolean
            Dim validacion As Boolean = True

            If fk_Entidad.SelectedValue = "-1" Or fk_Entidad.SelectedValue = "" Then
                validacion = False
                MyMasterPage.ShowMessage("Debe seleccionar una entidad para continuar", Core.MsgBoxIcon.IconInformation, "Error Guardando")
            ElseIf fk_Sede.SelectedValue = "-1" Or fk_Sede.SelectedValue = "" Then
                validacion = False
                MyMasterPage.ShowMessage("Debe seleccionar una sede para continuar", MsgBoxIcon.IconWarning, "Error Guardando")
            ElseIf fk_Caja_Tipo.SelectedValue = "-1" Or fk_Caja_Tipo.SelectedValue = "" Then
                validacion = False
                MyMasterPage.ShowMessage("Debe seleccionar un tipo de caja para continuar", MsgBoxIcon.IconWarning, "Error Guardando")
            End If

            Return validacion
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

                LlenaGrilla(grdData, table)
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
            If Valida() Then
                Try
                    Dim table As DataTable = CType(Session("TableForm"), DataTable)

                    If SaveType = SaveType.Insert Then CreateInsert(MySesion.Usuario.id, container, table)
                    If SaveType = SaveType.Update Then CreateUpdate(MySesion.Usuario.id, container, table)

                    CurrentMasterTab = MasterTabType.Filter
                    'LlenaGrilla(grdData, CreateSelect(MySesion.Usuario.id, Esquema, Tabla))
                    grdData.DataSource = Nothing
                    grdData.DataBind()
                    LimpiarControles(container, table, Entidad)
                Catch ex As Exception
                    MyMasterPage.ShowMessage(ex.Message, MsgBoxIcon.IconWarning, "")
                End Try
            End If
        End Sub
        Protected Sub btnEliminar_Click() Handles MyBase.CommandActionDelete
            Try
                Dim table As DataTable = CType(Session("TableForm"), DataTable)

                CreateDelete(MySesion.Usuario.id, container, table)
                CurrentMasterTab = MasterTabType.Filter
                LlenaGrilla(grdData, CreateSelect(MySesion.Usuario.id, Esquema, Tabla))
                LimpiarControles(container, table, Entidad)
            Catch ex As Exception
                showErrorPage(ex)
            End Try
        End Sub
        Protected Sub BtnNuevo_Click() Handles MyBase.CommandActionNew
            Try
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

    End Class
End Namespace