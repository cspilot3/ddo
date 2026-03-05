Imports DBCore

Namespace Sitio.Reporte

    Public Class P_Salida_Reportes
        Inherits PopupBase

#Region "Declaraciones"

        Dim id_Reporte As Integer

#End Region

#Region "Eventos"

        Private Sub grd_Data_delete(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdData.RowCommand
            Try
                If e.CommandSource.GetType() Is GetType(Web.UI.WebControls.ImageButton) Then

                    Dim dbmCore As New DBCoreDataBaseManager(ConnectionString.Core)

                    Try
                        dbmCore.Connection_Open(MySesion.Usuario.id)

                        Dim Data = CType(Session("GridPopup"), DataTable)
                        Dim Reg As Integer = CType(sender, CoreGridView).PreSelectedIndex
                        Dim nId_Reporte_Salida = CShort(Data(Reg)("id_Reporte_Salida"))
                        id_Reporte = CInt(GlobalParameterCollection("id_Reporte").DefaultValue)

                        dbmCore.Transaction_Begin()
                        dbmCore.SchemaConfig.TBL_Reporte_Salida.DBDelete(id_Reporte, nId_Reporte_Salida)
                        dbmCore.Transaction_Commit()

                    Catch ex As Exception
                        dbmCore.Transaction_Rollback()
                    Finally
                        dbmCore.Connection_Close()
                    End Try

                    Config_Page()
                End If

            Catch ex As Exception
                showErrorPage(ex)
            End Try
        End Sub

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not IsPostBack Then
                Config_Page()
            End If
        End Sub

        Protected Sub grdData_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles grdData.SelectedIndexChanged
            Try
                If grdData.SelectedIndex > -1 Then
                    Dim Data = CType(Session("GridPopup"), DataTable)

                    id_Reporte_Salida.Text = CStr(Data(grdData.SelectedIndex)("Id_Reporte_Salida"))
                    Titulo_Salida.Text = CStr(Data(grdData.SelectedIndex)("Titulo_Salida"))
                End If

            Catch ex As Exception
                showErrorPage(ex)
            End Try
        End Sub

        Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
            GuardarCambios()
        End Sub

#End Region

#Region "METODOS"

        Private Sub Config_Page()
            Dim dbmCore As DBCoreDataBaseManager = Nothing

            Try
                dbmCore = New DBCoreDataBaseManager(ConnectionString.Core)
                dbmCore.Connection_Open(MySesion.Usuario.id)
                
                id_Reporte = CInt(GlobalParameterCollection("id_Reporte").DefaultValue)

                Dim Parametros = dbmCore.SchemaConfig.TBL_Reporte_Salida.DBGet(id_Reporte, Nothing)
                Session("GridPopup") = Parametros
                grdData.DataSource = Session("GridPopup")
                grdData.DataBind()

                id_Reporte_Salida.Text = ""
                Titulo_Salida.Text = ""
            Catch ex As Exception
                Throw
            Finally
                If dbmCore IsNot Nothing Then dbmCore.Connection_Close()
            End Try

        End Sub

        Public Function valida() As Boolean
            Dim validacion As Boolean = True
            Dim cadenaError As New StringBuilder()
            
            If Titulo_Salida.Text = "" Then
                cadenaError.AppendLine("El Titulo de la Salida es obligatorio")
                validacion = False
            End If

            If validacion = False Then
                ShowMessage(cadenaError.ToString)
            End If

            Return validacion
        End Function

        Public Sub GuardarCambios()
            Dim dbmCore As DBCoreDataBaseManager = Nothing

            id_Reporte = CInt(GlobalParameterCollection("id_Reporte").DefaultValue)

            Try
                dbmCore = New DBCoreDataBaseManager(ConnectionString.Core)
                dbmCore.Connection_Open(MySesion.Usuario.id)
                If valida() Then
                    dbmCore.Transaction_Begin()

                    Dim TituloSalida As New SchemaConfig.TBL_Reporte_SalidaType
                    TituloSalida.fk_Reporte = id_Reporte
                    TituloSalida.Titulo_Salida = Titulo_Salida.Text

                    'Inserta un nuevo parametro
                    If id_Reporte_Salida.Text = "" Then
                        TituloSalida.Id_Reporte_Salida = dbmCore.SchemaConfig.TBL_Reporte_Salida.DBNextId_for_Id_Reporte_Salida(id_Reporte)
                        dbmCore.SchemaConfig.TBL_Reporte_Salida.DBInsert(TituloSalida)
                    Else 'Actualiza el parametro
                        TituloSalida.Id_Reporte_Salida = CShort(id_Reporte_Salida.Text)
                        dbmCore.SchemaConfig.TBL_Reporte_Salida.DBUpdate(TituloSalida, TituloSalida.fk_Reporte, TituloSalida.Id_Reporte_Salida)
                    End If

                    dbmCore.Transaction_Commit()
                    Config_Page()
                End If
            Catch ex As Exception
                If dbmCore IsNot Nothing Then dbmCore.Transaction_Rollback()
                MyMasterPage.ShowMessage("Ha ocurrido un problema al guardar los cambios, por favor comuniquese con el administrador.", Miharu.Core.MsgBoxIcon.IconWarning, "Error guardando cambios")
            Finally
                If dbmCore IsNot Nothing Then dbmCore.Connection_Close()
            End Try
        End Sub

#End Region

    End Class
End Namespace