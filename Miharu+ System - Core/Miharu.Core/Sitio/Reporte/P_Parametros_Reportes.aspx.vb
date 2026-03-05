Imports DBCore

Namespace Sitio.Reporte

    Public Class PParametrosReportes
        Inherits PopupBase

#Region " Declaraciones "

        Private _idReporte As Integer

#End Region

#Region " Eventos "

        Private Sub grd_Data_delete(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles grdData.RowCommand
            Try
                If e.CommandSource.GetType() Is GetType(ImageButton) Then

                    Dim dmcore As New DBCoreDataBaseManager(ConnectionString.Core)

                    Try
                        dmcore.Connection_Open(MySesion.Usuario.id)

                        Dim data = CType(Session("GridPopup"), DataTable)
                        Dim reg As Integer = CType(sender, CoreGridView).PreSelectedIndex
                        Dim idReporteParametro = CShort(data(reg)("Id_Reporte_Parametro"))
                        _idReporte = CInt(GlobalParameterCollection("id_Reporte").DefaultValue)

                        dmcore.Transaction_Begin()
                        dmcore.SchemaConfig.TBL_Reporte_Parametro.DBDelete(idReporteParametro, _idReporte)
                        dmcore.Transaction_Commit()

                    Catch ex As Exception
                        dmcore.Transaction_Rollback()
                    Finally
                        dmcore.Connection_Close()
                    End Try

                    Config_Page()
                End If

            Catch ex As Exception
                showErrorPage(ex)
            End Try
        End Sub

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            If Not IsPostBack Then
                Config_Page()
            End If
        End Sub

        Protected Sub grdData_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles grdData.SelectedIndexChanged
            Try
                If grdData.SelectedIndex > -1 Then
                    Dim data = CType(Session("GridPopup"), DataTable)

                    id_Reporte_Parametro_.Text = CStr(data(grdData.SelectedIndex)("Id_Reporte_Parametro"))
                    fk_tipo_Parametro.SelectedValue = Split(CStr(data(grdData.SelectedIndex)("Tipo")), "-")(0).Trim
                    Nombre_Reporte_Parametro.Text = CStr(data(grdData.SelectedIndex)("Nombre_Parametro"))
                    Etiqueta_Reporte_Parametro.Text = CStr(data(grdData.SelectedIndex)("Etiqueta_Parametro"))
                    If (data(grdData.SelectedIndex).IsNull("Consulta_Lista")) Then
                        Consulta_Lista.Text = ""
                    Else
                        Consulta_Lista.Text = data(grdData.SelectedIndex)("Consulta_Lista")
                    End If
                    If (data(grdData.SelectedIndex).IsNull("Columna_Etiqueta_Lista")) Then
                        Columna_Etiqueta_Lista.Text = ""
                    Else
                        Columna_Etiqueta_Lista.Text = data(grdData.SelectedIndex)("Columna_Etiqueta_Lista")
                    End If
                    If (data(grdData.SelectedIndex).IsNull("Columna_Valor_Lista")) Then
                        Columna_Valor_Lista.Text = ""
                    Else
                        Columna_Valor_Lista.Text = data(grdData.SelectedIndex)("Columna_Valor_Lista")
                    End If
                End If

            Catch ex As Exception
                showErrorPage(ex)
            End Try
        End Sub

        Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs) Handles ImageButton1.Click
            GuardarCambios()
        End Sub

#End Region

#Region " Metodos "

        Private Sub Config_Page()
            Dim dbmCore As DBCoreDataBaseManager = Nothing

            Try
                dbmCore = New DBCoreDataBaseManager(ConnectionString.Core)
                dbmCore.Connection_Open(MySesion.Usuario.id)

                _idReporte = CInt(GlobalParameterCollection("id_Reporte").DefaultValue)

                If Not IsPostBack Then
                    Dim tipos = dbmCore.SchemaConfig.TBL_Campo_Tipo.DBFindByEs_Primario(True)
                    Llenacombo(fk_tipo_Parametro, tipos, tipos.id_Campo_TipoColumn.ColumnName, tipos.Nombre_Campo_TipoColumn.ColumnName)
                End If

                Dim parametros = dbmCore.Schemadbo.CTA_Parametros_Reporte.DBFindByfk_Reporte(_idReporte)
                Session("GridPopup") = parametros
                grdData.DataSource = Session("GridPopup")
                grdData.DataBind()

                id_Reporte_Parametro_.Text = ""
                Nombre_Reporte_Parametro.Text = ""
                fk_tipo_Parametro.SelectedValue = "-1"
            Catch ex As Exception
                Throw
            Finally
                If dbmCore IsNot Nothing Then dbmCore.Connection_Close()
            End Try

        End Sub

        Public Function Valida() As Boolean
            Dim validacion As Boolean = True
            Dim cadenaError As New StringBuilder()

            If Nombre_Reporte_Parametro.Text = "" Then
                cadenaError.AppendLine("El Nombre del Parametro es obligatorio")
                validacion = False
            ElseIf Nombre_Reporte_Parametro.Text.Substring(0, 1) <> "@" Then
                cadenaError.AppendLine("El Nombre debe empezar con  '@' ")
                validacion = False
            End If

            If Etiqueta_Reporte_Parametro.Text = "" Then
                cadenaError.AppendLine("La Etiqueta del Parametro es obligatoria")
                validacion = False
            End If

            If fk_tipo_Parametro.SelectedValue = "" Or fk_tipo_Parametro.SelectedValue = "-1" Then
                cadenaError.AppendLine("El tipo de dato del parametro es obligatorio")
                validacion = False
            End If

            If validacion = False Then
                ShowMessage(cadenaError.ToString)
            End If

            Return validacion
        End Function

        Public Sub GuardarCambios()
            Dim dbmCore As DBCoreDataBaseManager = Nothing

            _idReporte = CInt(GlobalParameterCollection("id_Reporte").DefaultValue)

            Try
                dbmCore = New DBCoreDataBaseManager(ConnectionString.Core)
                dbmCore.Connection_Open(MySesion.Usuario.id)
                If valida() Then
                    dbmCore.Transaction_Begin()

                    Dim parametro As New SchemaConfig.TBL_Reporte_ParametroType
                    parametro.fk_Reporte = _idReporte
                    parametro.fk_Tipo_Parametro = CByte(fk_tipo_Parametro.SelectedValue)
                    parametro.Nombre_Parametro = Nombre_Reporte_Parametro.Text
                    parametro.Etiqueta_Parametro = Etiqueta_Reporte_Parametro.Text

                    If parametro.fk_Tipo_Parametro.ToString = "5" Then
                        parametro.Consulta_Lista = Consulta_Lista.Text
                        parametro.Columna_Etiqueta_Lista = Columna_Etiqueta_Lista.Text
                        parametro.Columna_Valor_Lista = Columna_Valor_Lista.Text
                    Else
                        parametro.Consulta_Lista = DBNull.Value
                        parametro.Columna_Etiqueta_Lista = DBNull.Value
                        parametro.Columna_Valor_Lista = DBNull.Value
                    End If

                    'Inserta un nuevo parametro
                    If id_Reporte_Parametro_.Text = "" Then
                        parametro.Id_Reporte_Parametro = dbmCore.SchemaConfig.TBL_Reporte_Parametro.DBNextId(_idReporte)
                        dbmCore.SchemaConfig.TBL_Reporte_Parametro.DBInsert(parametro)
                    Else 'Actualiza el parametro
                        parametro.Id_Reporte_Parametro = CShort(id_Reporte_Parametro_.Text)
                        dbmCore.SchemaConfig.TBL_Reporte_Parametro.DBUpdate(parametro, parametro.Id_Reporte_Parametro, _idReporte)
                    End If

                    dbmCore.Transaction_Commit()
                    Config_Page()
                End If
            Catch ex As Exception
                If dbmCore IsNot Nothing Then dbmCore.Transaction_Rollback()
                MyMasterPage.ShowMessage("Ha ocurrido un problema al guardar los cambios, por favor comuniquese con el administrador.", MsgBoxIcon.IconWarning, "Error guardando cambios")
            Finally
                If dbmCore IsNot Nothing Then dbmCore.Connection_Close()
            End Try
        End Sub

#End Region

        Protected Sub fk_tipo_Parametro_SelectedIndexChanged(sender As Object, e As EventArgs) Handles fk_tipo_Parametro.SelectedIndexChanged
            ConsultaTR.Visible = (fk_tipo_Parametro.SelectedValue = 5)
            EtiquetaTR.Visible = (fk_tipo_Parametro.SelectedValue = 5)
            ValorTR.Visible = (fk_tipo_Parametro.SelectedValue = 5)
        End Sub
    End Class

End Namespace