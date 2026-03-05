Imports System.Drawing
Imports System.Windows.Forms
Imports Miharu.Desktop.Library
Imports Slyg.Tools

Namespace View.Indexacion.Table

    Public Class IndexerTable

#Region " Declaraciones "

        Private _Columns As IndexerTableColumnCollection
        Private _Rows As IndexerTableRowCollection

        Public Event AddColumnEvent(ByRef Column As IndexerTableColumn)
        Public Event ClearColumnsEvent()
        Public Event RemoveColumnEvent()

        Public Event NextControlEvent()

        Private _IndexerView As IIndexerView
        Private _View As IView

        Private _IndexerTableRow As IndexerTableRow
        Private tableInputControl As TableInputControl

#End Region

#Region " Constructores "

        Public Sub New()
            ' Llamada necesaria para el diseñador.
            InitializeComponent()


            ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
            _Columns = New IndexerTableColumnCollection()
            _Rows = New IndexerTableRowCollection()

            AddHandler _Columns.AddColumnEvent, AddressOf Columns_AddColumnEvent
            AddHandler _Columns.ClearColumnsEvent, AddressOf Columns_ClearColumnsEvent
            AddHandler _Columns.RemoveColumnEvent, AddressOf Columns_RemoveColumnEvent

            AddHandler _Rows.AddRowEvent, AddressOf Rows_AddRowEvent
            AddHandler _Rows.ClearRowsEvent, AddressOf Rows_ClearRowsEvent
            AddHandler _Rows.RemoveRowEvent, AddressOf Rows_RemoveRowEvent

            BodyFlowLayoutPanel.FlowDirection = FlowDirection.LeftToRight

            tableInputControl = New TableInputControl()

        End Sub

#End Region

#Region " Propieades "

        Public ReadOnly Property FocusedColumnIndex As Integer
            Get
                Dim focusedRow As IndexerTableRow = getRowFocus()

                If focusedRow IsNot Nothing Then
                    Return getFocusedCellIndex(focusedRow)
                End If

                Return -1
            End Get
        End Property

        Public ReadOnly Property FocusedRowIndex As Integer
            Get
                Dim focusedRow As IndexerTableRow = getRowFocus()
                If focusedRow IsNot Nothing Then
                    Return Rows.IndexOf(focusedRow)
                Else
                    Return -1
                End If
            End Get
        End Property

        Public ReadOnly Property IndexerView As IIndexerView
            Get
                Return _IndexerView
            End Get
        End Property

        Public ReadOnly Property View As IView
            Get
                Return _View
            End Get
        End Property

        Public ReadOnly Property Columns As IndexerTableColumnCollection
            Get
                Return _Columns
            End Get
        End Property

        Public ReadOnly Property Rows As IndexerTableRowCollection
            Get
                Return _Rows
            End Get
        End Property

        Public Property ColumHeaderHeight As Integer
            Get
                Return HeaderButton.Height
            End Get
            Set(ByVal value As Integer)
                HeaderButton.Height = value
                UpPanel.Height = value
            End Set
        End Property

        Public Property RowHeaderWidth As Integer
            Get
                Return HeaderButton.Width
            End Get
            Set(ByVal value As Integer)
                HeaderButton.Width = value
                RowHeaderFlowLayoutPanel.Width = value
            End Set
        End Property

        Public Property ShowSecondControls As Boolean

        Public Property AllowAddRow As Boolean

        Public Property PageNumber As Integer

        Public Property AllowAddItemRow As Boolean

        Public Property tableItemRowOCR As Boolean

        Public Property hasValuesRowsTable As Boolean

        Public Property tableColumnnOCR As New DataView()

        Public Property AllowDeleteRow As Boolean

        Public Property ShowControlRegistros As Boolean

        Public Property ShowControlValor As Boolean

        Public Property MinRegistros As Short

        Public Property MaxRegistros As Short

        Public Property ControlRegistros As Integer
            Get
                If (DataConvert.IsNumeric(controlRegistrosDesktopTextBox.Text)) Then
                    Return Integer.Parse(controlRegistrosDesktopTextBox.Text)
                Else
                    Return 0
                End If
            End Get
            Set(value As Integer)
                Me.controlRegistrosDesktopTextBox.Text = value.ToString()
            End Set
        End Property

        Public Property ControlValor As Double
            Get
                If (DataConvert.IsNumeric(controlValorDesktopTextBox.Text)) Then
                    Return DataConvert.ToNumericD(controlValorDesktopTextBox.Text, ".")
                Else
                    Return 0
                End If
            End Get
            Set(value As Double)
                Me.controlValorDesktopTextBox.Text = value.ToString("###.00").Replace(",", ".")
            End Set
        End Property

        Public Property IsCalidad() As Boolean

#End Region

#Region " Eventos "

        Private Sub IndexerTable_Enter(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Enter
            setFocus()
        End Sub

        Private Sub ControlRegistrosDesktopTextBox_KeyDown(sender As Object, e As KeyEventArgs) Handles controlRegistrosDesktopTextBox.KeyDown
            If (e.KeyCode = Keys.Enter) Then
                If (Me.ShowControlValor) Then
                    Me.controlValorDesktopTextBox.Focus()
                    Me.controlValorDesktopTextBox.SelectAll()
                ElseIf (_Rows.Count > 0 And _Columns.Count > 0) Then
                    _Rows(0).Cells(0).Focus()
                Else
                    HeaderButton.Focus()
                End If
            End If
        End Sub

        Private Sub ControlValorDesktopTextBox_KeyDown(sender As Object, e As KeyEventArgs) Handles controlValorDesktopTextBox.KeyDown
            If (e.KeyCode = Keys.Enter) Then
                If (_Rows.Count > 0 And _Columns.Count > 0) Then
                    _Rows(0).Cells(0).Focus()
                Else
                    HeaderButton.Focus()
                End If
            End If
        End Sub

        Private Sub BodyFlowLayoutPanel_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BodyFlowLayoutPanel.Click
            If (_Columns.Count = 0 Or _Rows.Count = 0) Then
                HeaderButton.Focus()
            End If
        End Sub

        Private Sub HeaderButton_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles HeaderButton.KeyDown
            Select Case e.KeyCode
                Case Keys.Enter
                    If (e.Control) Then
                        NextControl()
                    End If

                Case Keys.F8
                    DeleteRow()

                Case Keys.F10
                    AddRow()

                Case Keys.Delete
                    If (e.Control) Then
                        DeleteRow()
                    End If

            End Select
        End Sub

        Private Sub Columns_AddColumnEvent(ByRef Column As IndexerTableColumn)

            'Using ColumnHeaderButton As New Button()
            Dim ColumnHeaderButton As New Button()
            ColumnHeaderButton.Text = Column.HeaderText
            ColumnHeaderButton.TextAlign = ContentAlignment.MiddleCenter
            ColumnHeaderButton.Width = Column.Width + 4
            ColumnHeaderButton.Height = ColumnHeaderFlowLayoutPanel.Height
            ColumnHeaderButton.Margin = New Padding(0, 0, 0, 0)
            ColumnHeaderButton.FlatStyle = FlatStyle.Popup
            ColumnHeaderButton.TabStop = False

            Column.Header = ColumnHeaderButton

            ColumnHeaderFlowLayoutPanel.Width = MaxWidth()
            ColumnHeaderFlowLayoutPanel.Controls.Add(ColumnHeaderButton)
            'End Using

            RaiseEvent AddColumnEvent(Column)

            SetScroll()
        End Sub

        Private Sub Columns_ClearColumnsEvent()

            ' Suspender las actualizaciones del diseño para mejorar el rendimiento
            ColumnHeaderFlowLayoutPanel.SuspendLayout()

            Try
                ' Crear una lista separada de los controles para evitar modificar la colección mientras se itera
                Dim allControls As New List(Of Control)
                allControls.AddRange(ColumnHeaderFlowLayoutPanel.Controls.Cast(Of Control)())

                ' Disponer de todos los controles en la lista
                For Each control As Control In allControls
                    If TypeOf control Is Button Then
                        control.Dispose() ' Libera los recursos del botón
                    End If
                Next

                ' Limpiar los controles del panel
                ColumnHeaderFlowLayoutPanel.Controls.Clear()

                ColumnHeaderFlowLayoutPanel.Width = MaxWidth()
                RaiseEvent ClearColumnsEvent()

                SetScroll()
            Finally
                ' Reanudar las actualizaciones del diseño
                ColumnHeaderFlowLayoutPanel.ResumeLayout()
            End Try
        End Sub

        Private Sub Columns_RemoveColumnEvent(ByRef Column As IndexerTableColumn)

            RowHeaderFlowLayoutPanel.Controls.Remove(Column.Header)
            ColumnHeaderFlowLayoutPanel.Width = MaxWidth()
            RaiseEvent RemoveColumnEvent()

            SetScroll()
        End Sub

        Private Sub Rows_AddRowEvent(ByRef Row As IndexerTableRow)
            Dim RowHeaderButton As New Button()
            PageNumber = If(PageNumber = 0, 1, PageNumber)

            Dim pageSize As Integer = tableInputControl.PaginationSize
            RowHeaderButton.Text = CStr(_Rows.Count + ((PageNumber - 1) * pageSize))
            RowHeaderButton.Width = RowHeaderFlowLayoutPanel.Width
            RowHeaderButton.Height = Row.Height + 2
            RowHeaderButton.Margin = New Padding(0, 0, 0, 0)
            RowHeaderButton.FlatStyle = FlatStyle.Popup
            RowHeaderButton.TabStop = False

            Row.Header = RowHeaderButton

            RowHeaderFlowLayoutPanel.Controls.Add(RowHeaderButton)
            BodyFlowLayoutPanel.Controls.Add(Row)

            If (Columns.Count > 0) Then Row.Cells(0).Focus()

            RowHeaderFlowLayoutPanel.Height = MaxHeight()

            SetScroll()
        End Sub

        Private Sub Rows_ClearRowsEvent()

            ' Suspender las actualizaciones del diseño para mejorar el rendimiento
            RowHeaderFlowLayoutPanel.SuspendLayout()
            BodyFlowLayoutPanel.SuspendLayout()

            Try
                ' Crear una lista separada de los controles para evitar modificar la colección mientras se itera
                Dim allControls As New List(Of Control)
                allControls.AddRange(RowHeaderFlowLayoutPanel.Controls.Cast(Of Control)())
                allControls.AddRange(BodyFlowLayoutPanel.Controls.Cast(Of Control)())

                ' Disponer de todos los controles en RowHeaderFlowLayoutPanel y BodyFlowLayoutPanel
                For Each control As Control In allControls
                    control.Dispose()
                Next

                ' Limpiar los controles del panel
                RowHeaderFlowLayoutPanel.Controls.Clear()
                BodyFlowLayoutPanel.Controls.Clear()

                RowHeaderFlowLayoutPanel.Height = MaxHeight()

                SetScroll()

            Finally
                ' Reanudar las actualizaciones del diseño
                RowHeaderFlowLayoutPanel.ResumeLayout()
                BodyFlowLayoutPanel.ResumeLayout()
            End Try

        End Sub

        Private Sub Rows_RemoveRowEvent(ByRef Row As IndexerTableRow)

            RowHeaderFlowLayoutPanel.Controls.Remove(Row.Header)
            Row.Header.Dispose()                                    ' Liberar los recursos del botón del encabezado
            BodyFlowLayoutPanel.Controls.Remove(Row)
            Row.Dispose()                                           ' Liberar los recursos del control de la fila

            RowHeaderFlowLayoutPanel.Height = MaxHeight()

            SetScroll()
        End Sub

        Private Sub BodyFlowLayoutPanel_Scroll(ByVal sender As System.Object, ByVal e As ScrollEventArgs) Handles BodyFlowLayoutPanel.Scroll
            SetScroll()
        End Sub

        Private Sub ControlRegistrosDesktopTextBox_Leave(sender As System.Object, e As EventArgs) Handles controlRegistrosDesktopTextBox.Leave
            Me.controlRegistrosDesktopTextBox.ReadOnly = (DataConvert.ToNumeric(Me.controlRegistrosDesktopTextBox.Text) > 0)
            Me.unLockButton.Visible = (Me.controlRegistrosDesktopTextBox.ReadOnly Or Me.controlValorDesktopTextBox.ReadOnly)
        End Sub

        Private Sub ControlValorDesktopTextBox_Leave(sender As System.Object, e As EventArgs) Handles controlValorDesktopTextBox.Leave
            Me.controlValorDesktopTextBox.ReadOnly = (DataConvert.ToNumeric(Me.controlValorDesktopTextBox.Text) > 0)
            Me.unLockButton.Visible = (Me.controlRegistrosDesktopTextBox.ReadOnly Or Me.controlValorDesktopTextBox.ReadOnly)
        End Sub

        Private Sub unLockButton_Click(sender As System.Object, e As EventArgs) Handles unLockButton.Click
            UnLockControls()
        End Sub

#End Region

#Region " Metodos "

        Sub SetView(nView As Object)
            _View = CType(nView, IView)
            _IndexerView = CType(nView, IIndexerView)
        End Sub

        Friend Sub NextControl()
            RaiseEvent NextControlEvent()
        End Sub

        Public Sub AddRow()
            If (Me.AllowAddRow) Then

                Dim NewItemRow = NewRow()
                _Rows.Add(NewItemRow)

                If (_Columns.Count > 0) Then NewItemRow.Cells(0).Focus()
            Else
                NextControl()
            End If
        End Sub

        Public Sub AddRowItem()

            If (Me.tableItemRowOCR) Then
                Dim NewItemRow = NewRow()
                _Rows.Add(NewItemRow)

                If (_Columns.Count > 0) Then NewItemRow.Cells(0).Focus()
            Else
                NextControl()
            End If
        End Sub


        Public Sub DeleteRow()
            If (Me.AllowDeleteRow And _Rows.Count > 0) Then
                _Rows.Remove()
            End If
        End Sub

        Friend Sub SetScroll()
            ColumnHeaderFlowLayoutPanel.Location = New Point(RowHeaderWidth + BodyFlowLayoutPanel.AutoScrollPosition.X, 0)
            RowHeaderFlowLayoutPanel.Location = New Point(0, BodyFlowLayoutPanel.AutoScrollPosition.Y)
        End Sub

        Public Sub ShowControls()
            Me.PanelControlRegistros.Visible = Me.ShowControlRegistros
            Me.PanelControlValor.Visible = Me.ShowControlValor
            Me.ControlPanel.Visible = (Me.ShowControlRegistros Or Me.ShowControlValor)
        End Sub

        Public Sub setFocus()
            If (Me.ShowControlRegistros) Then
                Me.controlRegistrosDesktopTextBox.Focus()
                Me.controlRegistrosDesktopTextBox.SelectAll()
            ElseIf (Me.ShowControlValor) Then
                Me.controlValorDesktopTextBox.Focus()
                Me.controlValorDesktopTextBox.SelectAll()
            ElseIf (_Rows.Count > 0 And _Columns.Count > 0) Then
                _Rows(0).Cells(0).Focus()
            Else
                HeaderButton.Focus()
            End If

            Me.controlValorDesktopTextBox.ReadOnly = (DataConvert.ToNumeric(Me.controlValorDesktopTextBox.Text) > 0)
            Me.controlRegistrosDesktopTextBox.ReadOnly = (DataConvert.ToNumeric(Me.controlRegistrosDesktopTextBox.Text) > 0)
            Me.unLockButton.Visible = (Me.controlRegistrosDesktopTextBox.ReadOnly Or Me.controlValorDesktopTextBox.ReadOnly)
        End Sub

        Private Sub UnLockControls()
            Dim Valido As Boolean

            Me.View.Controller.EventManager.ValidarPermiso(Permisos.Imaging.Proceso.Control.Autorizaciones, Program.AccesoDesktopAssembly, "Se requieren permisos de administrador para cambiar los valores de control de totales", Valido)

            If (Valido) Then
                Me.controlRegistrosDesktopTextBox.ReadOnly = False
                Me.controlValorDesktopTextBox.ReadOnly = False

                If (Me.ShowControlRegistros) Then
                    Me.controlRegistrosDesktopTextBox.Focus()
                    Me.controlRegistrosDesktopTextBox.SelectAll()
                ElseIf (Me.ShowControlValor) Then
                    Me.controlValorDesktopTextBox.Focus()
                    Me.controlValorDesktopTextBox.SelectAll()
                End If
            Else
                MessageBox.Show("No se permitió el cambio de los valores de control de totales", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End Sub

#End Region

#Region " Funciones "

        ''' <summary>
        ''' Obtiene el índice de la celda enfocada en una fila específica.
        ''' </summary>
        ''' <param name="row">La fila en la que se busca la celda enfocada.</param>
        ''' <returns>El índice de la celda enfocada o -1 si no se encuentra ninguna celda con foco.</returns>
        Private Function getFocusedCellIndex(row As IndexerTableRow) As Integer

            If row IsNot Nothing Then                               ' Verifica que la fila no sea nula.                

                For Each cell As IndexerTableCell In row.Cells      ' Recorre todas las celdas de la fila.
                    If cell.ContainsFocus Then                      ' Verifica si la celda tiene el foco.
                        Return row.Cells.IndexOf(cell)              ' Devuelve el valor de la celda.
                    End If
                Next
            End If

            Return -1                                               ' Devuelve una cadena vacía si no se encontró ninguna celda con foco.
        End Function

        ''' <summary>
        ''' Obtiene la fila que actualmente tiene el foco en la tabla.
        ''' </summary>
        ''' <returns>La fila enfocada o Nothing si no hay ninguna fila enfocada.</returns>
        Private Function getRowFocus() As IndexerTableRow

            For Each row As IndexerTableRow In Rows
                If row.ContainsFocus Then
                    Return row
                End If
            Next

            Return Nothing                                         ' Indicar que no hay una fila enfocada.
        End Function

        Public Function NewRow() As IndexerTableRow

            Return (New IndexerTableRow(Me))
        End Function

        Private Function MaxHeight() As Integer
            Dim Max As Integer = 20

            For Each Row In _Rows
                Max += Row.Height
            Next

            If ((Me.BodyFlowLayoutPanel.Height) > Max) Then
                Return Me.BodyFlowLayoutPanel.Height
            Else
                Return Max
            End If
        End Function

        Private Function MaxWidth() As Integer
            Dim Max As Integer = 20

            For Each Column In _Columns
                Max += Column.Width + 8
            Next

            If ((Me.BodyFlowLayoutPanel.Width - 20) > Max) Then
                Return Me.BodyFlowLayoutPanel.Width - 20
            Else
                Return Max
            End If
        End Function

#End Region

    End Class

End Namespace