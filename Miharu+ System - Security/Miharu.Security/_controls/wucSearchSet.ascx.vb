Imports Miharu.Security._clases

Namespace _controls

    Partial Public Class wucSearchSet
        Inherits UserWebControlBase

#Region " Definiciones "

        Private vtbl_in As DataTable = New DataTable
        Private vtbl_out As DataTable = New DataTable
        Private vtbl_in_aux As DataTable = New DataTable  'tabla aux para realizar las busquedas

#End Region

#Region "Propiedades"

        Public Property tbl_in() As DataTable
            Get
                Return vtbl_in
            End Get
            Set(ByVal value As DataTable)
                vtbl_in = value
            End Set
        End Property

        Public Property tbl_in_aux() As DataTable
            Get
                Return vtbl_in_aux
            End Get
            Set(ByVal value As DataTable)
                vtbl_in_aux = value
            End Set
        End Property

        Public Property tbl_out() As DataTable
            Get
                Return vtbl_out
            End Get
            Set(ByVal value As DataTable)
                vtbl_out = value
            End Set
        End Property

#End Region

#Region " Eventos "

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            If Not Me.IsPostBack Then
                Load_Config()
            Else
                Load_Data()
            End If
            lblNotificacion.Text = ""

        End Sub

        Protected Sub btn_IzqDer_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_IzqDer.Click
            Enviar_IzqDer()
        End Sub

        Protected Sub btnDerIzq_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDerIzq.Click
            Enviar_DerIzq()
        End Sub

        Protected Sub btn_all_IzqDer_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_all_IzqDer.Click
            Enviar_AllIzqDer()
        End Sub

        Protected Sub btn_all_DerIzq_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_all_DerIzq.Click
            Enviar_AllDerIzq()
        End Sub

        Protected Sub txtSearch_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtSearch.TextChanged

        End Sub

        Protected Sub btn_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn.Click
            search_data()
        End Sub

        Private Sub gvSet_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles gvSet.RowDataBound
            If (gvSet.Rows.Count < gvSet.PageSize) And (gvSet.Rows.Count + gvSet.PageSize * gvSet.PageIndex < vtbl_in.Rows.Count) Then ' 
                e.Row.Cells(0).Visible = False
                'e.Row.Cells(1).Visible = False
            End If

        End Sub

        Private Sub gvGet_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles gvGet.RowDataBound
            If (gvGet.Rows.Count < gvGet.PageSize) And (gvGet.Rows.Count + gvGet.PageSize * gvGet.PageIndex < vtbl_out.Rows.Count) Then
                e.Row.Cells(0).Visible = False
                'e.Row.Cells(1).Visible = False
            End If
        End Sub

        Protected Sub lkbtnAll_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lkbtnAll.Click
            Load_All()
        End Sub

        Protected Sub gvSet_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles gvSet.PageIndexChanging
            gvSet.PageIndex = e.NewPageIndex
            Load_Grid_Set(vtbl_in)
        End Sub

        Protected Sub gvGet_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles gvGet.PageIndexChanging
            gvGet.PageIndex = e.NewPageIndex
            Load_Grid_Get()
        End Sub

#End Region

#Region " Metodos "

        Private Sub Load_Config()
            If Not IsNothing(vtbl_in) And vtbl_in.Columns.Count = 4 Then
                Load_Table()
                'Asigno a las variables de session-*---------------------------
                Me.MySesion.Pagina.Parameter("vtbl_out") = vtbl_out
                Me.MySesion.Pagina.Parameter("vtbl_in") = vtbl_in
                Me.MySesion.Pagina.Parameter("vtbl_in_aux") = vtbl_in_aux
            End If

        End Sub

        Private Sub Load_Data()
            'Asigno a las tablas las variables de session*-------------------
            vtbl_out = CType(Me.MySesion.Pagina.Parameter("vtbl_out"), DataTable)
            vtbl_in = CType(Me.MySesion.Pagina.Parameter("vtbl_in"), DataTable)
            vtbl_in_aux = CType(Me.MySesion.Pagina.Parameter("vtbl_in_aux"), DataTable)

        End Sub

        Private Sub Load_Grid_Set(ByVal table As DataTable)
            gvSet.DataSource = table
            gvSet.DataBind()
        End Sub

        Private Sub Load_Grid_Get()
            gvGet.DataSource = vtbl_out
            gvGet.DataBind()
        End Sub

        Private Sub Load_Table()
            ' structura_table(vtbl_in_aux)
            If Not IsNothing(vtbl_out) Then                            'si envian datos asignados
                If vtbl_out.Columns.Count = 4 Then                     'pendiente esta validacion rectificar!!!
                    Load_Grid_Get()
                End If
            Else
                vtbl_out = New DataTable
                crea_tbl()                                             'si no envian datos asignados crea una tabla vacia  
            End If
            Load_Grid_Set(vtbl_in)
        End Sub

        Private Sub Load_All()
            'ver todos
            vtbl_in.Rows.Clear()
            vtbl_in.AcceptChanges()
            If vtbl_in_aux.Rows.Count <> 0 Then
                For i As Integer = 0 To vtbl_in_aux.Rows.Count - 1
                    row_add(vtbl_in, vtbl_in_aux, i)
                Next
                Load_Grid_Set(vtbl_in)
            Else
                row_add_null(vtbl_in)
            End If

        End Sub

        Private Sub Enviar_IzqDer()
            If gvSet.PreSelectedIndex >= 0 And vtbl_out.Rows.Count > 0 _
               And vtbl_in.Rows(0).Item("Codigo").ToString <> "0" Then
                If vtbl_out.Rows(0).Item("Codigo").ToString = "0" Then
                    vtbl_out.Rows(0).Delete()
                    Load_Grid_Get()
                End If
                Dim rowSet As Integer = gvSet.PreSelectedIndex
                Dim nid As Integer = CInt(gvSet.Rows(rowSet).Cells(1).Text)
                delete_row_seletc(vtbl_in_aux, nid)
                row_add(vtbl_out, vtbl_in, rowSet)
                Load_Grid_Get()
                'elimina de la tabla set
                vtbl_in.Rows(rowSet).Delete()
                vtbl_in.AcceptChanges()
                If vtbl_in.Rows.Count = 0 Then
                    row_add_null(vtbl_in)
                End If
                Load_Grid_Set(vtbl_in)
            End If
        End Sub

        Private Sub Enviar_DerIzq()
            If gvGet.PreSelectedIndex >= 0 And vtbl_in.Rows.Count > 0 _
               And vtbl_out.Rows(0).Item("Codigo").ToString <> "0" Then
                If vtbl_in.Rows(0).Item("Codigo").ToString = "0" Then
                    vtbl_in.Rows(0).Delete()
                    Load_Grid_Set(vtbl_in)
                End If
                Dim rowGet As Integer = gvGet.PreSelectedIndex
                row_add(vtbl_in, vtbl_out, rowGet)
                add_row_seletc(rowGet)
                Load_Grid_Set(vtbl_in)
                'elimina de la tabla set
                vtbl_out.Rows(rowGet).Delete()
                vtbl_out.AcceptChanges()
                If vtbl_out.Rows.Count = 0 Then
                    row_add_null(vtbl_out)
                End If
                Load_Grid_Get()
            End If
        End Sub

        Private Sub Enviar_AllIzqDer()
            If vtbl_in.Rows.Count > 0 Then
                If vtbl_in.Rows(0).Item("Codigo").ToString <> "0" Then
                    If vtbl_out.Rows(0).Item("Codigo").ToString = "0" Then
                        vtbl_out.Rows(0).Delete()
                        Load_Grid_Get()
                    End If
                    For i As Integer = 0 To tbl_in.Rows.Count - 1
                        row_add(vtbl_out, vtbl_in, i)
                        Dim nid As Integer = CInt(vtbl_in.Rows(i).Item(1).ToString)
                        delete_row_seletc(vtbl_in_aux, nid)
                    Next
                    Load_Grid_Get()
                    '-------------
                    tbl_in.Rows.Clear()
                    row_add_null(vtbl_in)
                    Load_Grid_Set(vtbl_in)
                End If
            End If

        End Sub

        Private Sub Enviar_AllDerIzq()
            If vtbl_out.Rows.Count > 0 Then
                If vtbl_out.Rows(0).Item("Codigo").ToString <> "0" Then
                    If vtbl_in.Rows(0).Item("Codigo").ToString = "0" Then
                        vtbl_in.Rows(0).Delete()
                        Load_Grid_Set(vtbl_in)
                    End If
                    For i As Integer = 0 To tbl_out.Rows.Count - 1
                        row_add(vtbl_in, vtbl_out, i)
                        add_row_seletc(i)
                    Next i
                    Load_Grid_Set(vtbl_in)
                    '-------------
                    tbl_out.Rows.Clear()
                    row_add_null(vtbl_out)
                    Load_Grid_Get()
                End If
            End If
        End Sub

        Private Sub crea_tbl()
            'agrega una fila vacia para mostrar el grid
            structura_table(vtbl_out)
            row_add_null(vtbl_out)
            Load_Grid_Get()
        End Sub

        Private Sub structura_table(ByVal table As DataTable)
            'crea la estrutura de la tabla
            If vtbl_in.Columns.Count > 0 Then
                Dim strcolumn1 As String = vtbl_in.Columns(0).ColumnName
                Dim strcolumn2 As String = vtbl_in.Columns(1).ColumnName
                Dim strcolumn3 As String = vtbl_in.Columns(2).ColumnName
                Dim strcolumn4 As String = vtbl_in.Columns(3).ColumnName
                Dim tipo1 As String = vtbl_in.Columns("fk").DataType.ToString
                Dim tipo2 As String = vtbl_in.Columns("id").DataType.ToString
                Dim tipo3 As String = vtbl_in.Columns("Codigo").DataType.ToString
                Dim tipo4 As String = vtbl_in.Columns("Nombre").DataType.ToString
                Dim column As DataColumn
                column = New DataColumn()
                column.DataType = Type.GetType(tipo1)
                column.ColumnName = strcolumn1
                table.Columns.Add(column)
                column = New DataColumn()
                column.DataType = Type.GetType(tipo2)
                column.ColumnName = strcolumn2
                table.Columns.Add(column)
                column = New DataColumn()
                column.DataType = Type.GetType(tipo3)
                column.ColumnName = strcolumn3
                table.Columns.Add(column)
                column = New DataColumn()
                column.DataType = Type.GetType(tipo4)
                column.ColumnName = strcolumn4
                table.Columns.Add(column)
            End If
        End Sub

        Private Sub row_add_null(ByVal tbl As DataTable)
            Dim row As DataRow
            row = tbl.NewRow()
            row("fk") = 0
            row("id") = 0
            row("Codigo") = 0
            row("Nombre") = "----"
            tbl.Rows.Add(row)
        End Sub

        Private Sub row_add(ByVal tbladd As DataTable, ByVal tbl2 As DataTable, ByVal fila As Integer)
            Dim row As DataRow
            row = tbladd.NewRow()
            row("fk") = tbl2.Rows(fila).Item("fk")
            row("id") = tbl2.Rows(fila).Item("id")
            row("Codigo") = tbl2.Rows(fila).Item("Codigo")
            row("Nombre") = tbl2.Rows(fila).Item("Nombre")
            tbladd.Rows.Add(row)
        End Sub

        Private Sub search_data()
            Dim word As String = Trim(txtSearch.Text)
            Dim table_aux As New DataTable()
            structura_table(table_aux)
            If gvSet.Rows.Count > 0 And vtbl_in.Rows.Count > 0 Then
                Dim foundRows() As DataRow
                Dim strColumn As String
                If IsNumeric(word) Then
                    strColumn = vtbl_in.Columns("Codigo").ColumnName
                Else
                    strColumn = vtbl_in.Columns("Nombre").ColumnName
                End If
                foundRows = vtbl_in_aux.Select(strColumn & " LIKE '" & word & "%'")
                For i As Integer = 0 To foundRows.GetUpperBound(0)
                    Dim row As DataRow
                    row = table_aux.NewRow()
                    row("fk") = foundRows(i)("fk")
                    row("id") = foundRows(i)("id")
                    row("Codigo") = foundRows(i)("Codigo")
                    row("Nombre") = foundRows(i)("Nombre")
                    table_aux.Rows.Add(row)
                Next i
                If foundRows.Length = 0 Then
                    txtSearch.Text = ""
                    txtSearch.Focus()
                    lblNotificacion.Text = "No se encontraron registros coincidentes."
                Else
                    Load_Grid_Set(table_aux)
                    vtbl_in.Rows.Clear()
                    '-Actualizo la tabla vtbl_in
                    For i As Integer = 0 To table_aux.Rows.Count - 1
                        row_add(vtbl_in, table_aux, i)
                    Next i
                    Load_Grid_Set(vtbl_in)
                End If
            End If
        End Sub

        Private Sub delete_row_seletc(ByVal tbl As DataTable, ByVal id As Integer)
            'permite a la tabla de busqueda quitarle las filas que se seleccionan en el grid SET
            Dim index As Integer = -1
            For i As Integer = 0 To tbl.Rows.Count - 1
                If CInt(tbl.Rows(i).Item("id").ToString) = id Then
                    index = i
                    Exit For
                End If
            Next
            If index <> -1 Then
                tbl.Rows(index).Delete()
                tbl.AcceptChanges()

            End If
        End Sub

        Private Sub add_row_seletc(ByVal fila As Integer)
            'permite a la tabla de busqueda agregarle las filas que se seleccionan en el grid GET
            row_add(vtbl_in_aux, vtbl_out, fila)
        End Sub

        Public Sub Set_Data(ByVal tbl_in As DataTable, ByVal tbl_out As DataTable)
            vtbl_in = New DataTable
            vtbl_out = New DataTable
            vtbl_in_aux = New DataTable
            vtbl_in = tbl_in
            vtbl_out = tbl_out
            structura_table(vtbl_in_aux)
            txtSearch.Text = ""
            If tbl_in.Rows.Count <> 0 Then
                For i As Integer = 0 To tbl_in.Rows.Count - 1
                    row_add(vtbl_in_aux, tbl_in, i)
                Next
            Else
                row_add_null(vtbl_in)
            End If

            If Not IsNothing(vtbl_in) And vtbl_in.Columns.Count = 4 Then
                Load_Config()
            End If
        End Sub

#End Region

    End Class
End Namespace