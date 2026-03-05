Imports Miharu.Security._clases

Namespace _sitio.administracion.seguridad

    Partial Public Class p_usuariojefe
        Inherits PaginaBasePopUp

#Region " Declaraciones "

        Private tblUsuario As DBSecurity.SchemaSecurity.TBL_UsuarioDataTable

#End Region

#Region " Eventos "

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            If Not Me.IsPostBack Then
                Config_Page()
            Else
                Load_Data()
            End If
        End Sub

        Private Sub gvBase_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles gvBase.SelectedIndexChanged
            If gvBase.SelectedIndex >= 0 And gvBase.Rows.Count > 0 Then
                SelecionarRegistro()
            End If
        End Sub

        Private Sub ucFiltro_Click(ByVal nParametro As String) Handles ucFiltro.Click
            Buscar(nParametro)
        End Sub

#End Region

#Region " Metodos "

        Private Sub Config_Page()
            tblUsuario = New DBSecurity.SchemaSecurity.TBL_UsuarioDataTable
            Me.MySesion.Pagina.Parameter("tblUsuarioJefe") = tblUsuario
        End Sub

        Private Sub Load_Data()
            tblUsuario = CType(Me.MySesion.Pagina.Parameter("tblUsuarioJefe"), DBSecurity.SchemaSecurity.TBL_UsuarioDataTable)
        End Sub

        Private Sub Buscar(ByVal nParametro As String)
            gvBase.SelectedIndex = -1

            If nParametro <> "" Then
                Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)
                
                Try
                    dbmSecurity.Connection_Open(MySesion.Usuario.id)

                    dbmSecurity.SchemaSecurity.TBL_Usuario.DBFillByApellidos_Usuario(tblUsuario, nParametro)
                Catch ex As Exception
                    Master.ShowAlert(ex.Message, MiharuMasterPopUp.MsgBoxIcon.IconError)
                Finally
                    dbmSecurity.Connection_Close()
                End Try
            End If

            Visualizar_Resultados()

        End Sub

        Private Sub Visualizar_Resultados()
            gvBase.DataSource = tblUsuario
            gvBase.DataBind()
        End Sub

        Private Sub SelecionarRegistro()
            Dim RowUsuario As DBSecurity.SchemaSecurity.TBL_UsuarioRow

            RowUsuario = CType(tblUsuario.Rows(gvBase.SelectedRow.DataItemIndex), DBSecurity.SchemaSecurity.TBL_UsuarioRow)

            ' Data
            MySesion.Pagina.Parameter("JefeId") = RowUsuario.id_Usuario
            MySesion.Pagina.Parameter("JefeNombre") = RowUsuario.Apellidos_Usuario.Trim() & " " & RowUsuario.Nombres_Usuario.Trim()

            Master.Cerrar(True)

        End Sub
#End Region

    End Class

End Namespace