Imports System.IO
Imports System
Imports DBCore
Imports Miharu.Core.Clases
Imports Slyg.Tools
Imports System.Security.Cryptography
Imports Slyg.Tools.Cryptographic
Imports Miharu.Security.Library.WebService
Imports System.Xml.Serialization
Imports System.ComponentModel
Imports Miharu.Security.Library


Namespace Sitio.Reporte

    Public Class Reportes
        Inherits FormBase

#Region "Declaraciones"

        Private _esquema As String
        Private _tabla As String
        Private _container As Object

#End Region

#Region " Eventos"

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            _container = panelDetalle
            _esquema = "config"
            _tabla = "TBL_Reporte"

            LoadAutomatic()
        End Sub

        Protected Sub Load_Complete() Handles Me.LoadComplete
            Config_Page()
        End Sub

        Protected Sub ParametrosImageButtonButton_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs) Handles ParametrosImageButton.Click
            If id_Reporte.Text <> "" Then
                Dim parameter As New ParameterCollection
                parameter.Add("id_Reporte", id_Reporte.Text)
                GlobalParameterCollection = parameter

                OpenModalDialog("Save", "../sitio/Reporte/P_Parametros_Reportes.aspx", "msgParametros", "Parametros Reporte", 650, 400)
            Else
                MyMasterPage.ShowMessage("Debe guardar el reporte antes de configurar los parametros.", MsgBoxIcon.IconWarning)
            End If
        End Sub

        Protected Sub SalidasImageButtonButton_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs) Handles SalidasImageButton.Click
            If id_Reporte.Text <> "" Then
                Dim parameter As New ParameterCollection
                parameter.Add("id_Reporte", id_Reporte.Text)
                GlobalParameterCollection = parameter

                OpenModalDialog("Save", "../sitio/Reporte/P_Salida_Reportes.aspx", "msgParametros", "Títulos Salida Reporte", 650, 400)
            Else
                MyMasterPage.ShowMessage("Debe guardar el reporte antes de configurar los Títulos de Salida.", MsgBoxIcon.IconWarning)
            End If
        End Sub

        Protected Sub btnGuardar_Click() Handles MyBase.CommandActionSave
            If Valida() Then
                Try
                    Dim dtTable As DataTable = CType(Session("TableForm"), DataTable)

                    If SaveType = SaveType.Insert Then CreateInsert(MySesion.Usuario.id, _container, dtTable)
                    If SaveType = SaveType.Update Then CreateUpdate(MySesion.Usuario.id, _container, dtTable)

                    CurrentMasterTab = MasterTabType.Grid
                    LlenaGrilla(grdData, CreateSelect(MySesion.Usuario.id, _esquema, _tabla))
                Catch ex As Exception
                    showErrorPage(ex)
                End Try
            End If
        End Sub

        Protected Sub btnEliminar_Click() Handles MyBase.CommandActionDelete
            Try
                Dim table As DataTable = CType(Session("TableForm"), DataTable)

                CreateDelete(MySesion.Usuario.id, _container, table)
                CurrentMasterTab = MasterTabType.Grid
            Catch ex As Exception
                showErrorPage(ex)
            End Try
        End Sub

        Protected Sub BtnNuevo_Click() Handles MyBase.CommandActionNew
            Try
                'LimpiarControles(container, table, CStr(nEntidad))
                Clear_Controls(CType(panelDetalle, Control))
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

        Private Sub Page_LoadComplete(ByVal sender As Object, ByVal e As EventArgs) Handles Me.LoadComplete
            If Not IsPostBack Then
                MyMasterPage.MasterTabContainer.Tabs(0).Enabled = False
                CurrentMasterTab = MasterTabType.Grid
            End If

            NumRegistros.Text = "Número de registros: " & grdData.Rows.Count
        End Sub

        Private Sub Usa_Columnas_Ancho_Fijo_CheckedChanged(sender As Object, e As System.EventArgs) Handles Usa_Columnas_Ancho_Fijo.CheckedChanged
            Dim dbmCore As DBCoreDataBaseManager = Nothing

            Try

                If id_Reporte.Text <> "" Then
                    If Usa_Columnas_Ancho_Fijo.Checked = True Then

                        Dim parameter As New ParameterCollection
                        parameter.Add("id_Reporte", id_Reporte.Text)
                        GlobalParameterCollection = parameter

                        OpenModalDialog("Save", "../sitio/Reporte/P_Parametros_Columnas.aspx", "msgParametrosColumnas", "Parametros Columna Reporte", 650, 400)
                    Else

                        dbmCore = New DBCoreDataBaseManager(ConnectionString.Core)
                        dbmCore.Connection_Open(MySesion.Usuario.id)

                        Dim TablaReporteCol = dbmCore.SchemaConfig.TBL_Reporte_Columna.DBFindByfk_Reporte(CInt(id_Reporte.Text))

                        If TablaReporteCol.Rows.Count > 0 Then

                            MySesion.Pagina.Parameter("mensaje") = "Este reporte tiene configurado parametros de columnas. ¿Desea eliminar los parametros de columnas?. Recuerde que los datos serán elminados permanentemente."
                            OpenModalDialog("CampoLista", "../Controles/Confirmacion.aspx", "msgDelete", "Eliminar Registro", 550, 150)

                        End If

                    End If

                Else
                    MyMasterPage.ShowMessage("Debe guardar el reporte antes de configurar los parametros de columnas del reporte.", MsgBoxIcon.IconWarning)
                End If

            Catch ex As Exception
                Throw
            Finally
                If dbmCore IsNot Nothing Then dbmCore.Connection_Close()
            End Try

        End Sub

        Private Sub ImgParametrosColumna_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImgParametrosColumna.Click
            If id_Reporte.Text <> "" And Usa_Columnas_Ancho_Fijo.Checked = True Then
                Dim parameter As New ParameterCollection
                parameter.Add("id_Reporte", id_Reporte.Text)
                GlobalParameterCollection = parameter

                OpenModalDialog("Save", "../sitio/Reporte/P_Parametros_Columnas.aspx", "msgParametrosColumnas", "Parametros Columna Reporte", 650, 400)
            Else
                MyMasterPage.ShowMessage("Debe guardar el reporte antes de configurar los parametros de columnas del reporte.", MsgBoxIcon.IconWarning)
            End If
        End Sub

        Private Sub AsignarClaveReporte_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles AsignarClaveReporte.Click
            If id_Reporte.Text <> "" Then
                Dim parameter As New ParameterCollection
                parameter.Add("id_Reporte", id_Reporte.Text)
                GlobalParameterCollection = parameter

                OpenModalDialog("Save", "../sitio/Reporte/P_Parametros_ClaveReporte.aspx", "msgParametros", "Asignacion Clave Reporte", 650, 400)
            Else
                MyMasterPage.ShowMessage("Debe guardar antes de asignar la clave de reporte.", MsgBoxIcon.IconWarning)
            End If
        End Sub


#End Region

#Region " Metodos "

        Public Sub Config_Page()
            Dim dbmCore As DBCoreDataBaseManager = Nothing

            Try
                dbmCore = New DBCoreDataBaseManager(ConnectionString.Core)
                dbmCore.Connection_Open(MySesion.Usuario.id)

                If Not IsPostBack Then
                    Dim data As New DataSet
                    data.Tables.Add(dbmCore.SchemaConfig.TBL_Conexion.DBGet(Nothing, Nothing))
                    GlobalData = data

                    Llenacombo(fk_Entidad, dbmCore.SchemaSecurity.CTA_Entidad.DBGet(), "id_Entidad", "Nombre_Entidad")
                    Llenacombo(fk_Categoria_Reporte, dbmCore.SchemaConfig.TBL_Categoria_Reporte.DBGet(Nothing), "Id_Categoria_Reporte", "Nombre_Categoria_Reporte", True, "ACTIVO", "", False)
                    Llenacombo(fk_Formato, dbmCore.SchemaConfig.TBL_Formato_Archivo.DBGet(Nothing), "id_Formato", "Formato_Archivo_Salida", True, "ACTIVO", "", True, 0)
                    Llenacombo(fk_notificacion, dbmCore.SchemaConfig.TBL_Notificacion.DBGet(Nothing), "id_Notificacion", "Nombre_Notificacion", True, "ACTIVO", "", True, 0)
                End If

                DropDownListCascading(fk_Conexion, GlobalData.Tables(0), "id_Conexion", "Nombre_Conexion", "fk_entidad=" & fk_Entidad.SelectedValue)
            Catch ex As Exception
                Throw
            Finally
                If dbmCore IsNot Nothing Then dbmCore.Connection_Close()
            End Try

        End Sub

        Public Sub CoreGridViewChange()
        End Sub

        Public Sub LoadAutomatic()
            If Not IsPostBack Then
                Dim imgB As ImageButton = CType(MyMasterPage.ToolControl.FindControl("imgSave"), ImageButton)
                imgB.ValidationGroup = "Guardar"
                Session("TableForm") = DataDictionary(_esquema, _tabla)

                CurrentMasterTab = MasterTabType.Grid
                LlenaGrilla(grdData, CreateSelect(MySesion.Usuario.id, _esquema, _tabla))
            End If
        End Sub

        Public Sub IndexChanged()
            Try
                If grdData.SelectedIndex > -1 Then
                    CoreGridViewChange()
                    CoreGridViewLinkControls(GridData, grdData.SelectedIndex, _container)
                    CurrentMasterTab = MasterTabType.Detail
                    SaveType = SaveType.Update
                End If
            Catch ex As Exception
                showErrorPage(ex)
            End Try
        End Sub

        Private Sub Reportes_ModalClosed(ByVal parameters As String) Handles Me.ModalClosed
            Try
                Select Case ModalDialogNombre

                    Case "msgDelete"
                        If CStr(MySesion.Pagina.Parameter("RESPUESTA")).ToUpper = "SI" Then
                            Dim dbmCore As DBCoreDataBaseManager = Nothing
                            Try

                                dbmCore = New DBCoreDataBaseManager(ConnectionString.Core)
                                dbmCore.Connection_Open(MySesion.Usuario.id)

                                dbmCore.SchemaConfig.TBL_Reporte_Columna.DBDelete(CInt(id_Reporte.Text), Nothing)

                                'Actualiza reporte
                                Dim ParametroReporte As New SchemaConfig.TBL_ReporteType
                                ParametroReporte.Usa_Columnas_Ancho_Fijo = False

                                dbmCore.SchemaConfig.TBL_Reporte.DBUpdate(ParametroReporte, CInt(id_Reporte.Text))

                            Catch ex As Exception
                                Throw
                            Finally
                                If dbmCore IsNot Nothing Then dbmCore.Connection_Close()
                            End Try
                        Else
                            'Recarga data
                            IndexChanged()
                        End If
                End Select
            Catch ex As Exception
                Dim msg As String = valida_Excepcion(ex)
                If msg <> "" Then
                    MyMasterPage.ShowMessage(msg, Miharu.Core.MsgBoxIcon.IconWarning, "Error al eliminar Registro")
                Else
                    showErrorPage(ex)
                End If
            End Try

        End Sub

#End Region

#Region " Funciones "

        Public Function Valida() As Boolean
            Dim query As String = Consulta.Text.ToUpper
            Dim validacion As Boolean = query.Contains("INSERT") _
                                        Or query.Contains("DELETE") _
                                        Or query.Contains("DROP") _
                                        Or query.Contains("TRUNCATE") _
                                        Or query.Contains("UPDATE") _
                                        Or query.Contains("SET") _
                                        Or query.Contains("INTO") _
                                        Or query.Contains("DATABASE") _
                                        Or query.Contains("TABLE")
            
            If validacion Then
                Dim cadenaError = New StringBuilder
                cadenaError.AppendLine("La consulta no puede tener ninguna de las palabra reservadas")
                cadenaError.AppendLine("INSERT")
                cadenaError.AppendLine("DELETE")
                cadenaError.AppendLine("DROP")
                cadenaError.AppendLine("TRUNCATE")
                cadenaError.AppendLine("UPDATE")
                cadenaError.AppendLine("SET")
                cadenaError.AppendLine("INTO")
                cadenaError.AppendLine("DATABASE")
                cadenaError.AppendLine("TABLE")
                MyMasterPage.ShowMessage(cadenaError.ToString, MsgBoxIcon.IconWarning)
            End If

            Return Not validacion
        End Function

#End Region

    End Class

End Namespace