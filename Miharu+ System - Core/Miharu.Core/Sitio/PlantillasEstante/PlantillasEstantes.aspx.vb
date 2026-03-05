Imports DBCore
Imports Miharu.Core.Clases

Namespace Sitio.PlantillasEstante

    Public Class PlantillasEstantes
        Inherits FormBase

#Region "Declaraciones"
        Private Const MyPathPermiso As String = "2.1"
        Public nEntidad As Short

        Public schema As String = "custody"
        Public table As String = "TBL_Plantilla_Estante"
        Dim container As Object

        'DataTables para Fila, Columna, Profundidad
        Private dtFila As DataTable
        Private dtColumna As DataTable
        Private dtProfundidad As DataTable

#End Region

#Region "Eventos"

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not IsPostBack Then

                container = pnlDetalle
                Config_Page()

                Dim imgB As ImageButton = CType(MyMasterPage.ToolControl.FindControl("imgSave"), ImageButton)
                imgB.ValidationGroup = "Guardar"
                Session("TableForm") = DataDictionary(schema, table)
            Else
                Load_Data()
            End If
        End Sub

        Private Sub PlantillasEstantes_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
            If Not IsPostBack Then
                MyMasterPage.MasterTabContainer.Tabs(0).Enabled = False
                CurrentMasterTab = MasterTabType.Grid
            End If

            NumRegistros.Text = "Número de registros: " & grdData.Rows.Count
        End Sub

        Private Sub grdData_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdData.SelectedIndexChanged
            EditaRegistro()
        End Sub

        Protected Sub lnkbAgregarFilas_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkbAgregarFilas.Click
            Agregar_Elemento(1)
        End Sub

        Protected Sub imgAddFila_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgAddFila.Click
            Agregar_Elemento(1)
        End Sub

        Protected Sub lnkbAgregarColumnas_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkbAgregarColumnas.Click
            Agregar_Elemento(2)
        End Sub

        Protected Sub imgAddColumna_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgAddColumna.Click
            Agregar_Elemento(2)
        End Sub

        Protected Sub lnkbAgregarProfundidades_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkbAgregarProfundidades.Click
            Agregar_Elemento(3)
        End Sub

        Private Sub imgAddProfundidad_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgAddProfundidad.Click
            Agregar_Elemento(3)
        End Sub

        Private Sub dtgFilas_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles dtgFilas.RowCommand
            Dim index As Integer = CType(sender, CoreGridView).PreSelectedIndex
            If (index > -1) Then
                Elimina_Elemento(1, index)
            End If
        End Sub

        Private Sub dtgColumnas_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles dtgColumnas.RowCommand
            Dim index As Integer = CType(sender, CoreGridView).PreSelectedIndex
            If (index > -1) Then
                Elimina_Elemento(2, index)
            End If
        End Sub

        Private Sub dtgProfundidades_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles dtgProfundidades.RowCommand
            Dim index As Integer = CType(sender, CoreGridView).PreSelectedIndex
            If (index > -1) Then
                Elimina_Elemento(3, index)
            End If
        End Sub

        Private Sub dtgFilas_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles dtgFilas.RowDataBound
            Try
                If e.Row.RowType = DataControlRowType.DataRow Then
                    Dim txtLong As DNumber = CType(e.Row.FindControl("txtLongitud"), DNumber)
                    Dim ckFlot As CheckBox = CType(e.Row.FindControl("chkEsFlotante"), CheckBox)

                    Dim dtData As DataTable = CType(CType(sender, CoreGridView).DataSource, DataTable)

                    If dtData.Rows.Count > 0 And dtData.Rows(0).Item(1).ToString <> "" Then
                        txtLong.Text = dtData.Rows(e.Row.RowIndex).Item(2).ToString()
                        txtLong.Enabled = False
                        ckFlot.Checked = CBool(dtData.Rows(e.Row.RowIndex).Item(3))
                        ckFlot.Enabled = False
                    End If
                End If
            Catch ex As Exception
                showErrorPage(ex)
            End Try
        End Sub

        Private Sub dtgColumnas_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles dtgColumnas.RowDataBound
            Try
                If e.Row.RowType = DataControlRowType.DataRow Then
                    Dim txtLong As DNumber = CType(e.Row.FindControl("txtLongitud"), DNumber)

                    Dim dtData As DataTable = CType(CType(sender, CoreGridView).DataSource, DataTable)

                    If dtData.Rows.Count > 0 And dtData.Rows(0).Item(1).ToString <> "" Then
                        txtLong.Text = dtData.Rows(e.Row.RowIndex).Item(2).ToString()
                        txtLong.Enabled = False
                    End If
                End If
            Catch ex As Exception
                showErrorPage(ex)
            End Try
        End Sub

        Private Sub dtgProfundidades_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles dtgProfundidades.RowDataBound
            Try
                If e.Row.RowType = DataControlRowType.DataRow Then
                    Dim txtLong As DNumber = CType(e.Row.FindControl("txtLongitud"), DNumber)

                    Dim dtData As DataTable = CType(CType(sender, CoreGridView).DataSource, DataTable)

                    If dtData.Rows.Count > 0 And dtData.Rows(0).Item(1).ToString <> "" Then
                        txtLong.Text = dtData.Rows(e.Row.RowIndex).Item(2).ToString()
                        txtLong.Enabled = False
                    End If
                End If
            Catch ex As Exception
                showErrorPage(ex)
            End Try
        End Sub
#End Region

#Region "Métodos"
        Private Sub Config_Page()
            Dim dmCore As New DBCoreDataBaseManager(MyBase.ConnectionString.Core)
            dmCore.Connection_Open(MySesion.Usuario.id)

            Try
                Dim dtBase As DataTable = dmCore.SchemaCustody.TBL_Plantilla_Estante.DBGet(Nothing)
                grdData.DataSource = dtBase
                grdData.DataBind()

                'Configura los Datatables dtFila , dtColumna, dtProfundidad
                ''dtFila 
                dtFila = New DataTable()
                dtFila.Columns.Add("id_Plantilla_Estante_Fila")
                dtFila.Columns.Add("txtLongitud")
                dtFila.Columns.Add("chkEsFlotante")
                Session("dtFila") = dtFila
                Carga_Grilla(dtgFilas, dtFila)
                Agregar_Elemento(1)

                ''dtColumna
                dtColumna = New DataTable()
                dtColumna.Columns.Add("id_Plantilla_Estante_Columna")
                dtColumna.Columns.Add("txtLongitud")
                Session("dtColumna") = dtColumna
                Carga_Grilla(dtgColumnas, dtColumna)
                Agregar_Elemento(2)

                ''dtProfundidad
                dtProfundidad = New DataTable()
                dtProfundidad.Columns.Add("id_Plantilla_Estante_Profundidad")
                dtProfundidad.Columns.Add("txtLongitud")
                Session("dtProfundidad") = dtProfundidad
                Carga_Grilla(dtgProfundidades, dtProfundidad)
                Agregar_Elemento(3)

            Catch ex As Exception
                showErrorPage(ex)
            End Try

            dmCore.Connection_Close()
        End Sub

        Private Sub Load_Data()
            Try
                dtFila = CType(Session("dtFila"), DataTable)
                dtColumna = CType(Session("dtColumna"), DataTable)
                dtProfundidad = CType(Session("dtProfundidad"), DataTable)
            Catch ex As Exception
                showErrorPage(ex)
            End Try
        End Sub

        Private Sub EditaRegistro()
            Try
                If grdData.SelectedIndex > -1 Then
                    Dim row As GridViewRow = grdData.Rows(grdData.SelectedIndex)

                    txtNombre.Text = CStr(row.Cells(1).Text)
                    txtAlto.Text = CStr(row.Cells(7).Text)
                    txtAncho.Text = CStr(row.Cells(6).Text)
                    txtProfundidad.Text = CStr(row.Cells(5).Text)

                    Carga_Grilla_Elementos(CInt(row.Cells(0).Text))

                    CurrentMasterTab = MasterTabType.Detail
                    SaveType = SaveType.Update
                    MyMasterPage.MasterDetailPanel.Update()
                End If
            Catch ex As Exception
                showErrorPage(ex)
            End Try
        End Sub


        Private Sub GuardarRegistro()
            Dim dmCore As New DBCoreDataBaseManager(MyBase.ConnectionString.Core)

            Try
                Select Case SaveType
                    Case SaveType.Insert
                        'Inserta Planilla_Instante
                        dmCore.Connection_Open(MySesion.Usuario.id)
                        dmCore.Transaction_Begin()

                        'NextId
                        Dim idPlantillaEstante As Integer = dmCore.SchemaCustody.TBL_Plantilla_Estante.DBNextId()
                        Dim typePlantillaEstante As New SchemaCustody.TBL_Plantilla_EstanteType

                        typePlantillaEstante.id_Plantilla_Estante = CShort(idPlantillaEstante)
                        typePlantillaEstante.Nombre_Plantilla_Estante = txtNombre.Text
                        typePlantillaEstante.Filas_Plantilla_Estante = dtgFilas.Rows.Count()
                        typePlantillaEstante.Columnas_Plantilla_Estante = dtgColumnas.Rows.Count()
                        typePlantillaEstante.Profundidades_Plantilla_Estante = dtgProfundidades.Rows.Count()
                        typePlantillaEstante.Alto_Plantilla_Estante = CShort(txtAlto.Text)
                        typePlantillaEstante.Ancho_Plantilla_Estante = CShort(txtAncho.Text)
                        typePlantillaEstante.Largo_Plantilla_Estante = CShort(txtProfundidad.Text)

                        dmCore.SchemaCustody.TBL_Plantilla_Estante.DBInsert(typePlantillaEstante)

                        'Estante_Fila
                        For Each Fila As GridViewRow In dtgFilas.Rows
                            Dim nId As Integer = CInt(Fila.Cells(1).Text)
                            Dim Longitud As Integer = CInt(CType(Fila.FindControl("txtLongitud"), DNumber).Text)
                            Dim esFlotante As Boolean = CType(Fila.FindControl("chkEsFlotante"), CheckBox).Checked
                            dmCore.SchemaCustody.TBL_Plantilla_Estante_Fila.DBInsert(CShort(idPlantillaEstante), CShort(nId), Longitud, esFlotante)
                        Next

                        'Estante_Columna
                        For Each Columna As GridViewRow In dtgColumnas.Rows
                            Dim nId As Integer = CInt(Columna.Cells(1).Text)
                            Dim Longitud As Integer = CInt(CType(Columna.FindControl("txtLongitud"), DNumber).Text)
                            dmCore.SchemaCustody.TBL_Plantilla_Estante_Columna.DBInsert(CShort(idPlantillaEstante), CShort(nId), Longitud)
                        Next

                        'Estante_Profundidad
                        For Each Profundidad As GridViewRow In dtgProfundidades.Rows
                            Dim nId As Integer = CInt(Profundidad.Cells(1).Text)
                            Dim Longitud As Integer = CInt(CType(Profundidad.FindControl("txtLongitud"), DNumber).Text)
                            dmCore.SchemaCustody.TBL_Plantilla_Estante_Profundidad.DBInsert(CShort(idPlantillaEstante), CShort(nId), Longitud)
                        Next

                        dmCore.Transaction_Commit()
                        dmCore.Connection_Close()

                        Config_Page()
                        Clear_Controls(CType(pnlDetalle, UI.Control))
                        CurrentMasterTab = MasterTabType.Grid

                    Case SaveType.Update
                        MyMasterPage.ShowMessage("Las plantillas de estantes son de solo lectura.", MsgBoxIcon.IconInformation, "Estante")
                End Select
            Catch ex As Exception
                dmCore.Connection_Close()
                showErrorPage(ex)
            End Try

        End Sub

        Private Sub Carga_Grilla(ByRef coreGridView As CoreGridView, ByRef dt As DataTable)
            Try
                coreGridView.DataSource = dt
                coreGridView.DataBind()
            Catch ex As Exception
                showErrorPage(ex)
            End Try
        End Sub

        Private Sub Agregar_Elemento(ByVal tipo As Short)
            '1: Fila
            '2: Columna
            '3: Profundidad
            Try
                Select Case tipo
                    Case 1
                        Dim dr As DataRow = dtFila.NewRow()
                        dr.Item("id_Plantilla_Estante_Fila") = dtFila.Rows.Count + 1
                        dtFila.Rows.Add(dr)
                        Session("dtFila") = dtFila
                        Carga_Grilla(dtgFilas, dtFila)

                    Case 2
                        Dim dr As DataRow = dtColumna.NewRow()
                        dr.Item("id_Plantilla_Estante_Columna") = dtColumna.Rows.Count + 1
                        dtColumna.Rows.Add(dr)
                        Session("dtColumna") = dtColumna
                        Carga_Grilla(dtgColumnas, dtColumna)

                    Case 3
                        Dim dr As DataRow = dtProfundidad.NewRow()
                        dr.Item("id_Plantilla_Estante_Profundidad") = dtProfundidad.Rows.Count + 1
                        dtProfundidad.Rows.Add(dr)
                        Session("dtProfundidad") = dtProfundidad
                        Carga_Grilla(dtgProfundidades, dtProfundidad)
                End Select
            Catch ex As Exception
                showErrorPage(ex)
            End Try
        End Sub

        Private Sub Elimina_Elemento(ByVal tipo As Short, ByVal index As Integer)
            '1: Fila
            '2: Columna
            '3: Profundidad
            Try
                Select Case tipo
                    Case 1
                        dtFila.Rows(index).Delete()
                        dtFila.AcceptChanges()
                        Asigna_ConsecutivoDT(dtFila, 0)
                        Session("dtFila") = dtFila
                        Carga_Grilla(dtgFilas, dtFila)

                    Case 2
                        dtColumna.Rows(index).Delete()
                        dtColumna.AcceptChanges()
                        Asigna_ConsecutivoDT(dtColumna, 0)
                        Session("dtColumna") = dtColumna
                        Carga_Grilla(dtgColumnas, dtColumna)

                    Case 3
                        dtProfundidad.Rows(index).Delete()
                        dtProfundidad.AcceptChanges()
                        Asigna_ConsecutivoDT(dtProfundidad, 0)
                        Session("dtProfundidad") = dtProfundidad
                        Carga_Grilla(dtgProfundidades, dtProfundidad)
                End Select
            Catch ex As Exception
                showErrorPage(ex)
            End Try
        End Sub

        Private Sub Asigna_ConsecutivoDT(ByRef data As DataTable, ByVal idColumna As Integer)
            Try
                Dim nId As Integer = 0
                For Each fila As DataRow In data.Rows
                    nId += 1
                    fila.Item(idColumna) = nId
                Next
            Catch ex As Exception
                showErrorPage(ex)
            End Try
        End Sub

        Private Function Validar_Estante() As Boolean
            Dim bReturn As Boolean = False

            Dim nAlto As Integer = 0
            Dim nAncho As Integer = 0
            Dim nProfundidad As Integer = 0

            Dim nSumFilas As Integer = 0
            Dim nSumColumnas As Integer = 0
            Dim nSumProfundidades As Integer = 0

            Try
                nAlto = CInt(txtAlto.Text)
                nAncho = CInt(txtAncho.Text)
                nProfundidad = CInt(txtProfundidad.Text)

                nSumFilas = Suma_Grilla(dtgFilas)
                nSumColumnas = Suma_Grilla(dtgColumnas)
                nSumProfundidades = Suma_Grilla(dtgProfundidades)

                If (nAlto >= nSumFilas And nAncho >= nSumColumnas And nProfundidad >= nSumProfundidades) Then
                    bReturn = True
                End If
            Catch ex As Exception
                showErrorPage(ex)
            End Try
            Return bReturn
        End Function

        Private Function Suma_Grilla(ByRef grilla As CoreGridView) As Integer
            Dim nReturn As Integer = 0
            Dim caja As DNumber
            Try
                For Each fila As GridViewRow In grilla.Rows
                    caja = CType(fila.FindControl("txtLongitud"), DNumber)
                    nReturn += CInt(caja.Text)
                Next
            Catch ex As Exception
                showErrorPage(ex)
            End Try
            Return nReturn
        End Function

        Private Sub Carga_Grilla_Elementos(ByVal idPlantillaEstante As Integer)
            Dim dmCore As New DBCoreDataBaseManager(MyBase.ConnectionString.Core)
            dmCore.Connection_Open(MySesion.Usuario.id)

            Try
                dtFila = dmCore.SchemaCustody.TBL_Plantilla_Estante_Fila.DBFindByfk_Plantilla_Estante(CShort(idPlantillaEstante))
                dtColumna = dmCore.SchemaCustody.TBL_Plantilla_Estante_Columna.DBFindByfk_Plantilla_Estante(CShort(idPlantillaEstante))
                dtProfundidad = dmCore.SchemaCustody.TBL_Plantilla_Estante_Profundidad.DBFindByfk_Plantilla_Estante(CShort(idPlantillaEstante))

                dtgFilas.DataSource = dtFila
                dtgFilas.DataBind()

                dtgColumnas.DataSource = dtColumna
                dtgColumnas.DataBind()

                dtgProfundidades.DataSource = dtProfundidad
                dtgProfundidades.DataBind()

                dtgFilas.Columns(0).Visible = False
                dtgColumnas.Columns(0).Visible = False
                dtgProfundidades.Columns(0).Visible = False
            Catch ex As Exception
                showErrorPage(ex)
            End Try

            dmCore.Connection_Close()
        End Sub
#End Region

#Region "Semi - Automatic"
        Private Sub PlantillasEstantes_CommandActionNew() Handles Me.CommandActionNew
            Try
                Clear_Controls(CType(pnlDetalle, UI.Control))

                CurrentMasterTab = MasterTabType.Detail
                SaveType = SaveType.Insert
            Catch ex As Exception
                showErrorPage(ex)
            End Try
        End Sub

        Private Sub PlantillasEstantes_CommandActionEdit() Handles Me.CommandActionEdit
            EditaRegistro()
        End Sub

        Private Sub PlantillasEstantes_CommandActionSave() Handles Me.CommandActionSave
            Try
                If Validar_Estante() Then
                    GuardarRegistro()
                Else
                    MyMasterPage.ShowMessage("Los valores ingresados no son válidos. Por favor revise los siguientes datos: <br><br>" & _
                                             "1. La suma de la longitud de las filas, debe ser menor o igual al alto del estante.<br>" & _
                                             "2. La suma de la longitud de las columnas, debe ser menor o igual al ancho del estante.<br>" & _
                                             "3. La suma de la longitud de las profundidades, debe ser menor o igual a la profundidad del estante.<br>" & _
                                             "", MsgBoxIcon.IconWarning, "Error validando los Datos")
                End If
            Catch ex As Exception
                showErrorPage(ex)
            End Try
        End Sub
#End Region

    End Class
End Namespace