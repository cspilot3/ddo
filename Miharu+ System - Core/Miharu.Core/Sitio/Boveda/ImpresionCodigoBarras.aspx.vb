Imports DBCore
Imports Miharu.Core.Clases

Namespace Sitio.Boveda

    Public Class ImpresionCodigoBarras
        Inherits FormBase

#Region "Declaraciones"
        Private Const MyPathPermiso As String = "3.2"
        Private nEntidad As Short
        Private container As Object
        Private Const schema As String = "custody"
        Private Const table As String = "TBL_Boveda_Estante"

#End Region

#Region "Eventos"
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            container = pnlDetalle
            nEntidad = MySesion.Entidad.id
            If Not IsPostBack Then
                container = pnlDetalle
                Config_Page()
            Else

            End If
        End Sub

        Private Sub grdData_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdData.SelectedIndexChanged
            EditaRegistro()
        End Sub

        Protected Sub EstanteRadioButton_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles EstanteRadioButton.CheckedChanged
            If EstanteRadioButton.Checked Then
                ImpresionMultiView.SetActiveView(EstanteView)
            End If
        End Sub

        Protected Sub FilaRadioButton_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles FilaRadioButton.CheckedChanged
            If FilaRadioButton.Checked Then
                ImpresionMultiView.SetActiveView(FilaView)
            End If
        End Sub

        Protected Sub ColumnaRadioButton_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ColumnaRadioButton.CheckedChanged
            If ColumnaRadioButton.Checked Then
                ImpresionMultiView.SetActiveView(ColumnaView)
            End If
        End Sub

        Protected Sub ImprimirEstanteImageButton_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImprimirEstanteImageButton.Click
            If ValidaImpresion() Then
                sw.Text = "1"
                Imprimir()
            Else
                sw.Text = "0"
            End If
        End Sub
#End Region

#Region "Métodos"
        Private Sub Config_Page()
            Dim dbmCore As DBCoreDataBaseManager = Nothing
            Try
                dbmCore = New DBCoreDataBaseManager(MyBase.ConnectionString.Core)
                dbmCore.Connection_Open(MySesion.Usuario.id)

                Dim dtBase As DataTable = dbmCore.SchemaCustody.TBL_Boveda_Estante.DBFindByfk_Entidadfk_Sedefk_Bovedafk_Boveda_Seccion(nEntidad, Nothing, Nothing, Nothing)
                grdData.DataSource = dtBase
                grdData.DataBind()

                'Carga grilla
                LlenaGrilla(grdData, CreateSelect(MySesion.Usuario.id, schema, table))

                'Carga Combos
                Dim dtEntidades = dbmCore.Schemadbo.CTA_Entidad.DBFindByid_Entidad(Nothing)
                Llenacombo(fk_Entidad, dtEntidades, dtEntidades.id_EntidadColumn.ColumnName, dtEntidades.Nombre_EntidadColumn.ColumnName)
                fk_Entidad.SelectedValue = CStr(nEntidad)
                fk_Entidad.Enabled = AccesoEntidad

                Dim dtSede = dbmCore.Schemadbo.CTA_Sede.DBFindByfk_Entidad(Nothing)
                Llenacombo(fk_Sede, dtSede, dtSede.id_SedeColumn.ColumnName, dtSede.Nombre_SedeColumn.ColumnName)

                Dim dtBoveda = dbmCore.SchemaCustody.TBL_Boveda.DBFindByfk_Entidadfk_Sede(Nothing, Nothing)
                Llenacombo(fk_Boveda, dtBoveda, dtBoveda.id_BovedaColumn.ColumnName, dtBoveda.Nombre_BovedaColumn.ColumnName)

                Dim dtBovedaSeccion = dbmCore.SchemaCustody.TBL_Boveda_Seccion.DBFindByfk_Entidadfk_Sedefk_Boveda(Nothing, Nothing, Nothing)
                Llenacombo(fk_Boveda_Seccion, dtBovedaSeccion, dtBovedaSeccion.id_Boveda_SeccionColumn.ColumnName, dtBovedaSeccion.Nombre_Boveda_SeccionColumn.ColumnName)

                'Carga elementos de impresión
                ImpresionMultiView.SetActiveView(EstanteView)
            Catch ex As Exception
                showErrorPage(ex)
            Finally
                If dbmCore IsNot Nothing Then dbmCore.Connection_Close()
            End Try
        End Sub

        Private Sub EditaRegistro()
            Try
                If grdData.SelectedIndex > -1 Then
                    CoreGridViewLinkControls(GridData, grdData.SelectedIndex, container)
                    CurrentMasterTab = MasterTabType.Detail
                    SaveType = SaveType.Update
                End If
            Catch ex As Exception
                showErrorPage(ex)
            End Try
        End Sub

        Private Sub Imprimir()
            Dim dmCore As New DBCoreDataBaseManager(MyBase.ConnectionString.Core)
            Dim separador As String = vbCrLf

            Try
                dmCore.Connection_Open(MySesion.Usuario.id)
                Dim Estante = dmCore.SchemaCustody.TBL_Boveda_Posicion.DBFindByfk_Entidadfk_Sedefk_Bovedafk_Boveda_Seccionfk_Boveda_Estante(CShort(fk_Entidad.SelectedValue), CShort(fk_Sede.SelectedValue), CShort(fk_Boveda.SelectedValue), CShort(fk_Boveda_Seccion.SelectedValue), CShort(id_Boveda_Estante.Text)).DefaultView

                'Separador
                If ComaRadioButton.Checked Then
                    separador = ","
                ElseIf PuntoYComaRadioButton.Checked Then
                    separador = ";"
                ElseIf TabuladorRadioButton.Checked Then
                    separador = vbTab
                ElseIf SaltoLineaRadioButton.Checked Then
                    separador = vbCrLf
                End If

                'Estante
                If EstanteRadioButton.Checked Then
                    Estante.RowFilter = ""
                ElseIf FilaRadioButton.Checked Then
                    Estante.RowFilter = "Fila_Boveda_Posicion=" & FilaDTexto.Text
                ElseIf ColumnaRadioButton.Checked Then
                    Estante.RowFilter = "Columna_Boveda_Posicion=" & ColumnaDTexto.Text
                End If

                Session("DataExportar") = Estante.ToTable()
                Session("NombreArchivo") = "Estante-" & id_Boveda_Estante.Text
                Session("Separador") = separador
            Catch ex As Exception
                showErrorPage(ex)
            Finally
                dmCore.Connection_Close()
            End Try
        End Sub
#End Region

#Region "Funciones"
        Private Function ValidaImpresion() As Boolean
            Dim bReturn As Boolean = False
            Dim msgError As String = ""
            Try
                If EstanteRadioButton.Checked Then
                    bReturn = True
                ElseIf FilaRadioButton.Checked Then
                    If IsNumeric(FilaDTexto.Text) AndAlso _
                       (CInt(FilaDTexto.Text) >= 1 And CInt(FilaDTexto.Text) <= CInt(Filas_Boveda_Estante.Text)) Then
                        Return True
                    Else
                        msgError = "Valor Inválido: El número de la fila debe estar dentro del número de filas del estante."
                    End If
                ElseIf ColumnaRadioButton.Checked Then
                    If IsNumeric(ColumnaDTexto.Text) AndAlso _
                       (CInt(ColumnaDTexto.Text) >= 1 And CInt(ColumnaDTexto.Text) <= CInt(Columnas_Boveda_Estante.Text)) Then
                        Return True
                    Else
                        msgError = "Valor Inválido: El número de la columna debe estar dentro del número de columnas del estante."
                    End If
                End If

                If Not bReturn Then
                    MyMasterPage.ShowMessage(msgError, MsgBoxIcon.IconInformation, "Impresión Estante")
                End If
            Catch ex As Exception
                showErrorPage(ex)
            End Try
            Return bReturn
        End Function
#End Region

#Region "Automatic"
        Private Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
            If Not IsPostBack Then
                MyMasterPage.MasterTabContainer.Tabs(0).Enabled = False
                CurrentMasterTab = MasterTabType.Grid
            End If

            NumRegistros.Text = "Número de registros: " & grdData.Rows.Count

            'JavaScript
            ImprimirEstanteImageButton.Attributes.Add("onclick", "if(document.getElementById('" & sw.ClientID & "').value=='1')" & _
                                                                 "{ window.open('p_Exportar.aspx'); " & _
                                                                 "return true;}")
        End Sub
#End Region

    End Class
End Namespace