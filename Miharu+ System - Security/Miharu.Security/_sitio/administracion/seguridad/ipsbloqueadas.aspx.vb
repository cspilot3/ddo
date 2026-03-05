Imports Miharu.Security._clases

Namespace _sitio.administracion.seguridad

    Partial Public Class ipsbloqueadas
        Inherits FormBase

#Region " Declaraciones "

        Private Const MyPathPermiso As String = "1.2.6"

        Private tblBase As DBSecurity.SchemaSecurity.CTA_Conexiones_IPDataTable

#End Region

#Region " Eventos "

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            If Not MyBase.ValidarNavegacion(Me.GetType().BaseType.FullName, MyPathPermiso) Then Return

            If Not Me.IsPostBack Then
                Config_Page()
            Else
                Load_Data()
            End If
        End Sub

        Private Sub Page_HijaClose() Handles Me.HijaClose

        End Sub

        Private Sub gvBase_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles gvBase.RowDataBound
            Select Case e.Row.RowType
                Case DataControlRowType.DataRow

                    Dim ctrlEliminar As LinkButton = CType(e.Row.Cells(1).Controls(0), LinkButton)
                    ctrlEliminar.OnClientClick = "return confirm('¿Esta seguro de desbloquear la dirección IP seleccionada?');"

            End Select
        End Sub

        Private Sub gvBase_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles gvBase.RowCommand
            Eliminar(CInt(e.CommandArgument))
        End Sub

#End Region

#Region " Metodos "

        Private Sub Config_Page()
            tblBase = New DBSecurity.SchemaSecurity.CTA_Conexiones_IPDataTable
            Me.MySesion.Pagina.Parameter("tblBase") = tblBase

            Visualizar_Datos()
        End Sub

        Private Sub Load_Data()
            tblBase = CType(Me.MySesion.Pagina.Parameter("tblBase"), DBSecurity.SchemaSecurity.CTA_Conexiones_IPDataTable)
        End Sub

        Private Sub Visualizar_Datos()
            ' cargar datos
            Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)

            Try
                dbmSecurity.Connection_Open(MySesion.Usuario.id)
                tblBase.Clear()
                dbmSecurity.SchemaSecurity.CTA_Conexiones_IP.DBFill(tblBase)

            Catch ex As Exception
                Master.ShowAlert(ex.Message, MiharuMasterForm.MsgBoxIcon.IconError)
            Finally
                dbmSecurity.Connection_Close()
            End Try

            gvBase.DataSource = tblBase
            gvBase.DataBind()
        End Sub

        Private Sub Eliminar(ByVal nIndex As Integer)
            Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)
            Dim IP As String

            IP = CStr(tblBase.Rows(nIndex).Item("IP_Address"))

            Try
                dbmSecurity.Connection_Open(MySesion.Usuario.id)
                dbmSecurity.Transaction_Begin()

                dbmSecurity.SchemaSecurity.TBL_Conexiones_IP.DBDelete(IP, Nothing)

                dbmSecurity.Transaction_Commit()

                Me.Master.ShowAlert("La dirección IP " & IP & " fue desbloqueada", MiharuMasterForm.MsgBoxIcon.IconInformation)

            Catch ex As Exception
                dbmSecurity.Transaction_Rollback()
                Master.ShowAlert(ex.Message, MiharuMasterForm.MsgBoxIcon.IconError)

            Finally
                dbmSecurity.Connection_Close()
            End Try

            Visualizar_Datos()
        End Sub

#End Region

    End Class

End Namespace