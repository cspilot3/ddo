Imports DBCore

Imports Slyg.Tools
Imports System.Security.Cryptography
Imports Slyg.Tools.Cryptographic
Imports Miharu.Security.Library.WebService
Imports System.Xml.Serialization
Imports System.ComponentModel
Imports Miharu.Security.Library

Namespace Sitio.Reporte

    Public Class P_Parametros_Columnas
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
                        _idReporte = CInt(GlobalParameterCollection("id_Reporte").DefaultValue)
                        Dim Id_Columna = CShort(data(reg)("id_Columna"))

                        'verifica que la columna a eliminar sea la ultima
                        If Id_Columna = UltimaColumnaReporte(data) Then
                            dmcore.Transaction_Begin()
                            dmcore.SchemaConfig.TBL_Reporte_Columna.DBDelete(_idReporte, Id_Columna)
                            dmcore.Transaction_Commit()
                            ShowMessage("Eliminado exitoso!")
                            ClearCampos()
                        Else
                            ShowMessage("No es posible eliminar esta columna! Debe eliminar antes la columna de la ultima posicion del reporte.")
                        End If

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

                    fk_Reporte.Text = CStr(data(grdData.SelectedIndex)("fk_Reporte"))
                    id_Columna.Text = CStr(data(grdData.SelectedIndex)("id_Columna"))
                    Nombre_Columna.Text = CStr(data(grdData.SelectedIndex)("Nombre_Columna"))
                    Inicio_Columna.Text = CStr(data(grdData.SelectedIndex)("Inicio_Columna"))
                    Longitud_Columna.Text = CStr(data(grdData.SelectedIndex)("Longitud_Columna"))

                End If

            Catch ex As Exception
                showErrorPage(ex)
            End Try
        End Sub

        Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs) Handles ImageButton1.Click
            GuardarCambios()
        End Sub

        Private Sub imgNuevo_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgNuevo.Click
            clearCampos()
            Me.Nombre_Columna.Focus()
        End Sub



#End Region

#Region " Metodos "

        Private Sub Config_Page()
            Dim dbmCore As DBCoreDataBaseManager = Nothing

            Try
                dbmCore = New DBCoreDataBaseManager(ConnectionString.Core)
                dbmCore.Connection_Open(MySesion.Usuario.id)

                _idReporte = CInt(GlobalParameterCollection("id_Reporte").DefaultValue)

                'If Not IsPostBack Then

                'End If

                Dim parametros = dbmCore.SchemaConfig.TBL_Reporte_Columna.DBFindByfk_Reporte(_idReporte)
                Session("GridPopup") = parametros
                grdData.DataSource = Session("GridPopup")
                grdData.DataBind()

                fk_Reporte.Text = CInt(GlobalParameterCollection("id_Reporte").DefaultValue)
                Nombre_Columna.Text = ""
            Catch ex As Exception
                Throw
            Finally
                If dbmCore IsNot Nothing Then dbmCore.Connection_Close()
            End Try

        End Sub

        Private Sub ClearCampos()
            Me.id_Columna.Text = Nothing
            Me.Nombre_Columna.Text = Nothing
            Me.Longitud_Columna.Text = Nothing
            Me.Inicio_Columna.Text = Nothing
        End Sub

        Private Function UltimaColumnaReporte(ByVal DtColumnas As DataTable) As Integer
            'obtiene la ultima posicion de columnas del reporte
            Dim rows As Integer = grdData.Rows.Count + 1
            Dim Cols As Integer = grdData.Columns.Count
            Dim linea(rows, Cols) As String

            Dim idCol As Integer = 0

            For Each row As DataRow In DtColumnas.Rows
                If idCol < CInt(row.Item("id_Columna").ToString) Then
                    idCol = CInt(row.Item("id_Columna").ToString)
                End If
            Next

            Return idCol

        End Function


        Public Function Valida() As Boolean
            Dim validacion As Boolean = True
            Dim cadenaError As New StringBuilder()

            If Nombre_Columna.Text = "" Then
                cadenaError.AppendLine("El Nombre de la columna es obligatorio")
                validacion = False
            End If

            If Inicio_Columna.Text = "" Then
                cadenaError.AppendLine("El campo Inicio Columna es obligatorio")
                validacion = False
            End If

            If Longitud_Columna.Text = "" Then
                cadenaError.AppendLine("El campo longitud es obligatorio")
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
                If Valida() Then
                    dbmCore.Transaction_Begin()

                    Dim parametro As New SchemaConfig.TBL_Reporte_ColumnaType
                    parametro.fk_Reporte = _idReporte
                    If id_Columna.Text <> "" Then
                        parametro.id_Columna = id_Columna.Text
                    End If
                    parametro.Nombre_Columna = Nombre_Columna.Text
                    parametro.Inicio_Columna = Inicio_Columna.Text
                    parametro.Longitud_Columna = Longitud_Columna.Text

                    'Inserta un nuevo parametro
                    If fk_Reporte.Text <> "" Then
                        If id_Columna.Text = "" Then
                            dbmCore.SchemaConfig.PA_Reporte_Columna.DBExecute(CInt(fk_Reporte.Text), Nothing, parametro.Nombre_Columna, CInt(parametro.Inicio_Columna), CInt(parametro.Longitud_Columna))
                        Else
                            'Actualiza informacion de Reporte Columna
                            dbmCore.SchemaConfig.TBL_Reporte_Columna.DBUpdate(parametro, CInt(fk_Reporte.Text), CInt(id_Columna.Text))
                        End If

                        'Actualiza reporte
                        Dim ParametroReporte As New SchemaConfig.TBL_ReporteType
                        ParametroReporte.Usa_Columnas_Ancho_Fijo = True

                        dbmCore.SchemaConfig.TBL_Reporte.DBUpdate(ParametroReporte, CInt(fk_Reporte.Text))

                    End If

                    dbmCore.Transaction_Commit()
                    Config_Page()
                    ClearCampos()
                End If
            Catch ex As Exception
                If dbmCore IsNot Nothing Then dbmCore.Transaction_Rollback()
                MyMasterPage.ShowMessage("Ha ocurrido un problema al guardar los cambios, por favor comuniquese con el administrador.", MsgBoxIcon.IconWarning, "Error guardando cambios")
            Finally
                If dbmCore IsNot Nothing Then dbmCore.Connection_Close()
            End Try
        End Sub

#End Region

    End Class

End Namespace