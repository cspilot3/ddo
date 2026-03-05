Imports DBCore

Namespace Sitio.Administracion

    Public Class P_Tabla_Asociada
        Inherits PopupBase

#Region "Declaraciones"

        Dim Entidad As Short
        Dim Documento As Integer
        Dim Campo As Short

#End Region

#Region "Eventos"

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            configPage()
        End Sub

        Protected Sub grdData_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles grdData.SelectedIndexChanged
            Try
                Dim CamposTablaAsociada = CType(Session("TablaAsociada"), DataTable)
                Dim row = CamposTablaAsociada.Rows(grdData.SelectedIndex)

                id_Campo_Tabla.Text = row("id_Campo_Tabla").ToString
                Nombre_Campo.Text = row("Nombre_Campo").ToString
                Length_Campo.Text = row("Length_Campo").ToString

                If Split(row("Campo_Tipo").ToString, "-")(0) = "" Then
                    fk_Campo_Tipo.SelectedValue = "-1"
                Else
                    fk_Campo_Tipo.SelectedValue = Split(row("Campo_Tipo").ToString, "-")(0)
                End If

                If Split(row("Campo_Lita").ToString, "-")(0) = "" Then
                    fk_Campo_Lista.SelectedValue = "-1"
                Else
                    fk_Campo_Lista.SelectedValue = Split(row("Campo_Lita").ToString, "-")(0)
                End If

                If Split(row("Campo_Busqueda").ToString, "-")(0) = "" Then
                    fk_Campo_Busqueda.SelectedValue = "-1"
                Else
                    fk_Campo_Busqueda.SelectedValue = Split(row("Campo_Busqueda").ToString, "-")(0)
                End If

                Es_Obligatorio_Campo.Checked = CBool(row("Es_Obligatorio_Campo"))
                Es_Exportable.Checked = CBool(row("Es_Exportable"))
                Eliminado_Campo.Checked = CBool(row("Eliminado_Campo"))

            Catch ex As Exception
                Throw
            End Try
        End Sub

        Protected Sub GuardarImageButton_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles GuardarImageButton.Click
            guardar()
        End Sub

        Protected Sub NuevoImageButton_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles NuevoImageButton.Click
            limpiaControles()
        End Sub

#End Region

#Region "Funciones"

        Public Sub guardar()
            If valida() Then
                Dim dmCore As New DBCoreDataBaseManager(ConnectionString.Core)
                Entidad = CShort(GlobalParameterCollection("fk_Entidad").DefaultValue)
                Documento = CInt(GlobalParameterCollection("fk_Documento").DefaultValue)
                Campo = CShort(GlobalParameterCollection("fk_Campo").DefaultValue)

                Try
                    dmCore.Connection_Open(MySesion.Usuario.id)
                    dmCore.Transaction_Begin()

                    Dim CampoTablaType As New SchemaConfig.TBL_Tabla_AsociadaType
                    CampoTablaType.Eliminado_Campo = Eliminado_Campo.Checked
                    CampoTablaType.Es_Campo_Busqueda = CBool(IIf(fk_Campo_Busqueda.SelectedValue = "-1", False, True))
                    CampoTablaType.Es_Exportable = Es_Exportable.Checked
                    CampoTablaType.Es_Obligatorio_Campo = Es_Obligatorio_Campo.Checked
                    CampoTablaType.fk_Campo = Campo
                    CampoTablaType.fk_Campo_Busqueda = DShort(fk_Campo_Busqueda.SelectedValue)
                    CampoTablaType.fk_Campo_Lista = DShort(fk_Campo_Lista.SelectedValue)
                    CampoTablaType.fk_Campo_Tipo = DByte(fk_Campo_Tipo.SelectedValue)
                    CampoTablaType.fk_Documento = Documento
                    CampoTablaType.fk_Entidad = Entidad
                    CampoTablaType.Length_Campo = CShort(Length_Campo.Text)
                    CampoTablaType.Nombre_Campo = Nombre_Campo.Text

                    If id_Campo_Tabla.Text = "" Then
                        CampoTablaType.id_Campo_Tabla = dmCore.SchemaConfig.TBL_Tabla_Asociada.DBNextId(Documento, Campo)
                        id_Campo_Tabla.Text = CStr(CampoTablaType.id_Campo_Tabla.Value)
                        dmCore.SchemaConfig.TBL_Tabla_Asociada.DBInsert(CampoTablaType)
                    Else
                        dmCore.SchemaConfig.TBL_Tabla_Asociada.DBUpdate(CampoTablaType, Documento, Campo, CInt(id_Campo_Tabla.Text))
                    End If

                    dmCore.Transaction_Commit()

                    Dim CamposTablaAsociada = dmCore.Schemadbo.CTA_Campos_Tabla_Asociada.DBFindByfk_Entidadfk_Documentofk_Campo(Entidad, Documento, Campo)
                    Session("TablaAsociada") = CamposTablaAsociada
                    grdData.DataSource = CamposTablaAsociada
                    grdData.DataBind()

                Catch ex As Exception
                    dmCore.Transaction_Rollback()
                Finally
                    dmCore.Connection_Close()
                End Try

            End If
        End Sub

        Public Sub configPage()
            If Not IsPostBack Then
                Entidad = CShort(GlobalParameterCollection("fk_Entidad").DefaultValue)
                Documento = CInt(GlobalParameterCollection("fk_Documento").DefaultValue)
                Campo = CShort(GlobalParameterCollection("fk_Campo").DefaultValue)
                Dim dbmCore As DBCoreDataBaseManager = Nothing

                Try
                    dbmCore = New DBCoreDataBaseManager(ConnectionString.Core)
                    dbmCore.Connection_Open(MySesion.Usuario.id)

                    Dim CamposTablaAsociada = dbmCore.Schemadbo.CTA_Campos_Tabla_Asociada.DBFindByfk_Entidadfk_Documentofk_Campo(Entidad, Documento, Campo)
                    Session("CampoBusqueda") = dbmCore.Schemadbo.CTA_CampoBusqueda_Entidad.DBFindByfk_entidad(Entidad)
                    Llenacombo(fk_Campo_Tipo, dbmCore.SchemaConfig.TBL_Campo_Tipo.DBFindByEs_Primario(True), "id_campo_tipo", "nombre_campo_tipo")
                    Llenacombo(fk_Campo_Lista, dbmCore.SchemaConfig.TBL_Campo_Lista.DBGet(Nothing, Nothing), "id_campo_lista", "nombre_campo_lista")

                    Session("TablaAsociada") = CamposTablaAsociada

                    grdData.DataSource = CamposTablaAsociada
                    grdData.DataBind()

                Catch ex As Exception
                    Throw
                Finally
                    If dbmCore IsNot Nothing Then dbmCore.Connection_Close()
                End Try

            End If

            DropDownListCascading(fk_Campo_Busqueda, CType(Session("CampoBusqueda"), DataTable), "fk_campo_busqueda", "nombre_campo_busqueda", "fk_campo_tipo='" & fk_Campo_Tipo.SelectedValue & "'")
        End Sub

        Public Function valida() As Boolean
            Dim validacion As Boolean = True
            Dim CadenaError As New StringBuilder

            If Nombre_Campo.Text = "" Then
                CadenaError.AppendLine("Debe Escribir un nombre para el campo")
                validacion = False
            End If

            If Length_Campo.Text = "" Then
                CadenaError.AppendLine("Debe Escribir una longitud para el campo")
                validacion = False
            End If

            If fk_Campo_Tipo.SelectedValue = "-1" Then
                CadenaError.AppendLine("Debe seleccionar un tipo de campo")
                validacion = False
            End If

            If validacion = False Then
                MyMasterPage.ShowMessage(CadenaError.ToString(), MsgBoxIcon.IconWarning)
            End If

            Return validacion
        End Function

        Public Sub limpiaControles()
            id_Campo_Tabla.Text = ""
            Nombre_Campo.Text = ""
            Length_Campo.Text = ""
            fk_Campo_Tipo.SelectedValue = "-1"
            fk_Campo_Lista.SelectedValue = "-1"
            fk_Campo_Busqueda.SelectedValue = "-1"
            Es_Obligatorio_Campo.Checked = False
            Es_Exportable.Checked = False
            Eliminado_Campo.Checked = False
        End Sub

#End Region

    End Class
End Namespace