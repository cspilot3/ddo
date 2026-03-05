Imports DBCore

Namespace Sitio.Administracion

    Public Class p_Campo
        Inherits PopupBase

#Region "DECLARACIONES"

        Dim nEntidad As Slyg.Tools.SlygNullable(Of Integer)
        Dim nCampoTipo As Slyg.Tools.SlygNullable(Of Integer)

#End Region

#Region "EVENTOS"
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            nEntidad = CInt(Request.QueryString("fk_Entidad"))
            nCampoTipo = CInt(Request.QueryString("fk_Campo_Tipo"))
            If Not IsPostBack Then
                Config_Page()
            End If
        End Sub

        Private Sub grdData_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles grdData.SelectedIndexChanging
            Try
                If grdData.PreSelectedIndex > -1 Then
                    MySesion.Pagina.Parameter("RESPUESTA") = CInt(grdData.Rows(grdData.PreSelectedIndex).Cells(0).Text)
                    CloseWindow(True)
                End If
            Catch ex As Exception
                Throw New Exception()
            End Try
        End Sub
#End Region

#Region "METODOS"
        Private Sub Config_Page()
            Dim dbmCore As DBCoreDataBaseManager = Nothing

            Try
                dbmCore = New DBCoreDataBaseManager(ConnectionString.Core)
                dbmCore.Connection_Open(MySesion.Usuario.id)

                Dim dtCampoBusqueda As DataTable = dbmCore.Schemadbo.PA_Especial_TBL_Campo_Busqueda_get.DBExecute(nEntidad, nCampoTipo)

                grdData.DataSource = dtCampoBusqueda
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