Imports DBCore
Imports Miharu.Core.Clases

Namespace Sitio.Busqueda

    Public Class CampoBusquedaEntidad
        Inherits FormBase

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Config_Page()
        End Sub
        Public Sub Config_Page()
            Dim dbmCore As DBCoreDataBaseManager = Nothing

            Try
                dbmCore = New DBCoreDataBaseManager(ConnectionString.Core)
                dbmCore.Connection_Open(MySesion.Usuario.id)
                If Not IsPostBack Then
                    LlenaGrilla(grdData, dbmCore.Schemadbo.CTA_Entidad.DBGet())

                    Llenacombo(fk_entidad, dbmCore.Schemadbo.CTA_Entidad.DBGet(), "id_entidad", "nombre_entidad")
                    FillCheckBoxList(CheckBoxList1, dbmCore.Schemadbo.CampoBusqueda_x_Entidad.DBExecute(CShort(1)), "id", "nombre_campo_busqueda", "campo_busqueda_entidad", "deshabilitado", "fk_entidad=-1")
                End If

            Catch ex As Exception
                Throw
            Finally
                If dbmCore IsNot Nothing Then dbmCore.Connection_Close()
            End Try

        End Sub
        Private Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
            If Not IsPostBack Then
                MyMasterPage.MasterTabContainer.Tabs(0).Enabled = False
                CurrentMasterTab = MasterTabType.Grid
            End If

            NumRegistros.Text = "Número de registros: " & grdData.Rows.Count
        End Sub

#Region "Eventos controles"

        Protected Sub btnGuardar_Click() Handles MyBase.CommandActionSave
            Dim dm As New DBCoreDataBaseManager(MyBase.ConnectionString.Core)
            Try
                valida()
                dm.Connection_Open(MySesion.Usuario.id)
                dm.Transaction_Begin()

                For Each Item As ListItem In CheckBoxList1.Items
                    If Item.Enabled = True Then
                        Dim Registro As New SchemaConfig.TBL_Campo_Busqueda_EntidadType
                        Dim Value As String() = Split(Item.Value, "-")

                        Registro.fk_Campo_Tipo = DByte(Value(0))
                        Registro.fk_Campo_Busqueda = DShort(Value(1))
                        Registro.fk_Entidad = CShort(fk_entidad.SelectedValue)

                        dm.SchemaConfig.TBL_Campo_Busqueda_Entidad.DBDelete(Registro.fk_Entidad, Registro.fk_Campo_Tipo, Registro.fk_Campo_Busqueda)

                        If Item.Selected = True Then
                            dm.SchemaConfig.TBL_Campo_Busqueda_Entidad.DBInsert(Registro)
                        End If

                    End If
                Next
                dm.Transaction_Commit()

            Catch ex As Exception
                dm.Transaction_Rollback()
                'showErrorPage(ex)
                MyMasterPage.ShowMessage(ex.Message, MsgBoxIcon.IconWarning)
            Finally
                dm.Connection_Close()
            End Try
        End Sub
        Protected Sub fk_entidad_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles fk_entidad.SelectedIndexChanged
            Dim dbmCore As DBCoreDataBaseManager = Nothing

            Try
                dbmCore = New DBCoreDataBaseManager(ConnectionString.Core)
                dbmCore.Connection_Open(MySesion.Usuario.id)

                FillCheckBoxList(CheckBoxList1, dbmCore.Schemadbo.CampoBusqueda_x_Entidad.DBExecute(CShort(fk_entidad.SelectedValue)), "id", "nombre_campo_busqueda", "campo_busqueda_entidad", "deshabilitado", "fk_entidad=" & fk_entidad.SelectedValue & " or fk_entidad = 0")

            Catch ex As Exception
                Throw
            Finally
                If dbmCore IsNot Nothing Then dbmCore.Connection_Close()
            End Try
        End Sub
        Protected Sub BtnNuevo_Click() Handles MyBase.CommandActionNew
            Dim dbmCore As DBCoreDataBaseManager = Nothing

            Try
                dbmCore = New DBCoreDataBaseManager(ConnectionString.Core)
                dbmCore.Connection_Open(MySesion.Usuario.id)

                fk_entidad.SelectedValue = "-1"
                CurrentMasterTab = MasterTabType.Detail
                SaveType = SaveType.Insert
                FillCheckBoxList(CheckBoxList1, dbmCore.Schemadbo.CampoBusqueda_x_Entidad.DBExecute(CShort(fk_entidad.SelectedValue)), "id", "nombre_campo_busqueda", "campo_busqueda_entidad", "deshabilitado", "fk_entidad=" & fk_entidad.SelectedValue & " or fk_entidad = 0")
            Catch ex As Exception
                showErrorPage(ex)
            Finally
                If dbmCore IsNot Nothing Then dbmCore.Connection_Close()
            End Try

        End Sub
        Protected Sub grdData_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles grdData.SelectedIndexChanged
            IndexChanged()
        End Sub
        Protected Sub BtnEdit_Click() Handles MyBase.CommandActionEdit
            IndexChanged()
        End Sub

#End Region

#Region "Funciones"

        Public Sub valida()
            If fk_entidad.SelectedValue = "-1" Then Throw New Exception("Debe seleccionar una entidad.")
        End Sub
        Public Sub IndexChanged()
            Dim dbmCore As DBCoreDataBaseManager = Nothing

            Try
                dbmCore = New DBCoreDataBaseManager(ConnectionString.Core)
                dbmCore.Connection_Open(MySesion.Usuario.id)

                If grdData.SelectedIndex > -1 Then
                    CurrentMasterTab = MasterTabType.Detail
                    fk_entidad.SelectedValue = GridData.Rows(grdData.SelectedIndex)("id_entidad").ToString
                    FillCheckBoxList(CheckBoxList1, dbmCore.Schemadbo.CampoBusqueda_x_Entidad.DBExecute(CShort(fk_entidad.SelectedValue)), "id", "nombre_campo_busqueda", "campo_busqueda_entidad", "deshabilitado", "fk_entidad=" & fk_entidad.SelectedValue & " or fk_entidad = 0")
                End If
            Catch ex As Exception
                showErrorPage(ex)
            Finally
                If dbmCore IsNot Nothing Then dbmCore.Connection_Close()
            End Try

        End Sub

#End Region


    End Class
End Namespace