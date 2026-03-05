Imports DBCore

Namespace Sitio.Administracion

    Public Class P_Roles
        Inherits PopupBase

#Region "Declaraciones"
        Dim Entidad As Short
        Dim Proyecto As Short
        Dim Esquema As Short
        Dim Rol As Short
#End Region

#Region "Eventos"

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not IsPostBack Then
                Config_Page()
            End If
        End Sub

        Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
            GuardarCambios()
        End Sub
#End Region

#Region "Métodos"
        Private Sub Config_Page()
            Entidad = CShort(GlobalParameterCollection("id_Entidad").DefaultValue)
            Proyecto = CShort(GlobalParameterCollection("id_Proyecto").DefaultValue)
            Esquema = CShort(GlobalParameterCollection("id_Esquema").DefaultValue)
            Rol = CShort(GlobalParameterCollection("id_Rol").DefaultValue)
            Dim dbmCore As DBCoreDataBaseManager = Nothing

            Try
                dbmCore = New DBCoreDataBaseManager(ConnectionString.Core)
                dbmCore.Connection_Open(MySesion.Usuario.id)

                Dim DocumentosRoles = dbmCore.Schemadbo.PA_Documentos_Rol.DBExecute(Rol, Entidad, Proyecto, Esquema)
                DocumentosRoles = DocumentosRoles.DefaultView.ToTable(True, "Documento", "Ver_Registro", "Ver_Data", "Ver_Imagen", "Descargar")

                Session("GridPopup") = DocumentosRoles
                grdData.DataSource = Session("GridPopup")
                grdData.DataBind()
                For Each row As GridViewRow In grdData.Rows
                    CType(row.FindControl("Ver_Registro"), CheckBox).Checked = CBool(DocumentosRoles(row.RowIndex)("Ver_Registro"))
                    CType(row.FindControl("Ver_Data"), CheckBox).Checked = CBool(DocumentosRoles(row.RowIndex)("Ver_Data"))
                    CType(row.FindControl("Ver_Imagen"), CheckBox).Checked = CBool(DocumentosRoles(row.RowIndex)("Ver_Imagen"))
                    CType(row.FindControl("Descargar"), CheckBox).Checked = CBool(DocumentosRoles(row.RowIndex)("Descargar"))
                Next

            Catch ex As Exception
                Throw
            Finally
                If dbmCore IsNot Nothing Then dbmCore.Connection_Close()
            End Try

        End Sub

        Public Sub GuardarCambios()
            Dim dmcore As New DBCoreDataBaseManager(ConnectionString.Core)
            dmcore.Connection_Open(MySesion.Usuario.id)

            Try
                dmcore.Transaction_Begin()
                Dim Documento As Short
                Dim Registro As Boolean
                Dim Data As Boolean
                Dim Imagen As Boolean

                Entidad = CShort(GlobalParameterCollection("id_Entidad").DefaultValue)
                Proyecto = CShort(GlobalParameterCollection("id_Proyecto").DefaultValue)
                Esquema = CShort(GlobalParameterCollection("id_Esquema").DefaultValue)
                Rol = CShort(GlobalParameterCollection("id_Rol").DefaultValue)
                dmcore.SchemaSecurity.TBL_Rol_Documento.DBDelete(Rol, Entidad, Proyecto, Esquema, Nothing)

                For Each Row As GridViewRow In grdData.Rows
                    Documento = CShort(Split(Row.Cells(0).Text, "-")(0))

                    Registro = CType(Row.FindControl("Ver_Registro"), CheckBox).Checked
                    Data = CType(Row.FindControl("Ver_Data"), CheckBox).Checked
                    Imagen = CType(Row.FindControl("Ver_Imagen"), CheckBox).Checked
                    Dim Descargar As Boolean = CType(Row.FindControl("Descargar"), CheckBox).Checked

                    If Not (Registro = False And Data = False And Imagen = False And Descargar = False) Then
                        dmcore.SchemaSecurity.TBL_Rol_Documento.DBInsert(Rol, Entidad, Proyecto, Esquema, Documento, Registro, Data, Imagen, Descargar)
                    End If
                Next

                dmcore.Transaction_Commit()
                CloseWindow(True)
            Catch ex As Exception
                dmcore.Transaction_Rollback()
                MyMasterPage.ShowMessage("Ha ocurrido un problema al guardar los cambios, por favor comuniquese con el administrador.", Miharu.Core.MsgBoxIcon.IconWarning, "Error guardando cambios")
            Finally
                dmcore.Connection_Close()
            End Try

        End Sub
#End Region

    End Class
End Namespace