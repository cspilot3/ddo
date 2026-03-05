Imports DBCore

Namespace Sitio.Administracion

    Public Class P_Esquema_Servidor
        Inherits PopupBase

#Region "EVENTOS"

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not IsPostBack Then
                Config_Page()
            End If
        End Sub

        Private Sub grdData_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles grdData.SelectedIndexChanging
            Dim Table As DataTable = CType(Session("GridPopup"), DataTable)
            Try
                If grdData.PreSelectedIndex > -1 Then
                    GlobalParameterCollection.Add("id_servidor", Table.Rows(grdData.PreSelectedIndex)("id_servidor").ToString)
                    'GlobalParameterCollection.Add("id_Servidor_Volumen", Table.Rows(grdData.PreSelectedIndex)("id_Servidor_Volumen").ToString)
                    GlobalParameterCollection.Add("fk_entidad_servidor", Table.Rows(grdData.PreSelectedIndex)("fk_entidad").ToString)
                    GlobalParameterCollection.Add("Nombre_servidor", Table.Rows(grdData.PreSelectedIndex)("Nombre_servidor").ToString)
                    CloseWindow(True)
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

#End Region

#Region "METODOS"

        Private Sub Config_Page()

            Dim dbmCore As DBCoreDataBaseManager = Nothing

            Try
                dbmCore = New DBCoreDataBaseManager(ConnectionString.Core)
                dbmCore.Connection_Open(MySesion.Usuario.id)

                Session("GridPopup") = dbmCore.Schemadbo.CTA_Servidores_Core.DBGet()
                grdData.DataSource = Session("GridPopup")
                grdData.DataBind()
            Catch ex As Exception
                Throw
            Finally
                If dbmCore IsNot Nothing Then dbmCore.Connection_Close()
            End Try


        End Sub

#End Region

    End Class
End Namespace