Imports System.Windows.Forms
Imports Miharu.Desktop.Library.Config

Namespace View.Indexacion.Table

    Public Class IndexerTableRow

#Region " Declaraciones"

        Private _cells As IndexerTableCellCollection
        Private _table As IndexerTable

#End Region

#Region " Constructores "

        Private Sub New()
            ' Llamada necesaria para el diseñador.
            InitializeComponent()

            ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
            _Cells = New IndexerTableCellCollection()
        End Sub

        Friend Sub New(ByRef nParent As IndexerTable)
            Me.New()

            _Table = nParent

            AddHandler _Table.AddColumnEvent, AddressOf Table_AddColumnEvent
            AddHandler _Table.ClearColumnsEvent, AddressOf Table_ClearColumnsEvent
            AddHandler _Table.RemoveColumnEvent, AddressOf Table_RemoveColumnEvent

            Me.Width = 20

            CreateCells()
        End Sub

#End Region

#Region " Propiedades "

        Public ReadOnly Property Cells As IndexerTableCellCollection
            Get
                Return _Cells
            End Get
        End Property

        Public ReadOnly Property Table As IndexerTable
            Get
                Return _Table
            End Get
        End Property

        Friend Property Header As Button

#End Region

#Region " Eventos "

        Public Sub Table_AddColumnEvent(ByRef nColumn As IndexerTableColumn)
            AddCell(nColumn)
        End Sub

        Public Sub Table_ClearColumnsEvent()
            ClearCells()
        End Sub

        Public Sub Table_RemoveColumnEvent()
            RemoveCell()
        End Sub

#End Region

#Region " Metodos "

        ''' <summary>
        ''' Crea celdas para cada columna en la tabla y configura propiedades específicas si se cumple una condición.
        ''' </summary>
        ''' <remarks>
        ''' ''' Este método itera a través de las columnas de la tabla y realiza las siguientes acciones:
        ''' - Si la propiedad tableItemRowOCR es verdadera, inicializa la columna si está vacía y la configura para OCR.
        ''' - Agrega la columna a la celda.
        ''' </remarks>
        Private Sub CreateCells()

            For Each Column In _table.Columns

                If (_table.tableItemRowOCR) Then

                    Dim clonedColumn As New IndexerTableColumn  ' Variable para almacenar la columna clonada
                    CopyColumnProperties(Column, clonedColumn)  ' Clona la columna con sus propiedades

                    If Not (_table.hasValuesRowsTable) Then

                        InitializeTableColumnIfEmpty()  ' Inicializar la columna si está vacía y configurarla para OCR
                        clonedColumn = ConfigureColumnForOCR(clonedColumn) ' Configurar la columna para mostrar las columnas obtenidas del OCR

                        ' Bloquea la fila #1 al inicio del OCR si solo hay una columna marcada como OCR y su valor es vacío
                        Dim isFirstOCRColumn As Boolean = _table.tableColumnnOCR.Count = 1
                        Dim isFirstColumnValueEmpty As Boolean = _table.tableColumnnOCR(0)("Value_Field_List_Item_Default").ToString() = ""
                        clonedColumn.IsReadOnly = isFirstOCRColumn AndAlso isFirstColumnValueEmpty

                        ' Agrega la columna configurada a la celda
                        AddCell(clonedColumn)
                    Else
                        If (_table.tableColumnnOCR.Count > 1 AndAlso _table.tableColumnnOCR(1)("Value_Field_List_Item_Default").ToString() = "1") Then
                            clonedColumn = ConfigureColumnForOCR(clonedColumn) ' Configurar la columna para mostrar las columnas obtenidas del OCR
                            ' Agrega la columna configurada a la celda
                            AddCell(clonedColumn)
                        Else
                            AddCell(Column)                                             ' Agregar la columna a la celda
                        End If
                    End If
                Else
                    AddCell(Column)                                             ' Agregar la columna a la celda
                End If

            Next


            ''For Each Column In _table.Columns

            ''    If (_table.tableItemRowOCR) Then

            ''        If Not (_table.hasValuesRowsTable) Then
            ''            InitializeTableColumnIfEmpty()                          ' Inicializar la columna si está vacía y configurarla para OCR
            ''            Column = ConfigureColumnForOCR(Column)                  ' Configurar la columna para mostrar las columnas obtenidas del OCR

            ''            ' Bloquea la fila #1 al inicio del OCR si solo hay una columna marcada como OCR y su valor es vacío
            ''            Dim isFirstOCRColumn As Boolean = _table.tableColumnnOCR.Count = 1
            ''            Dim isFirstColumnValueEmpty As Boolean = _table.tableColumnnOCR(0)("Value_Field_List_Item_Default").ToString() = ""
            ''            Column.IsReadOnly = isFirstOCRColumn AndAlso isFirstColumnValueEmpty
            ''        Else
            ''            If (_table.tableColumnnOCR.Count > 1 AndAlso _table.tableColumnnOCR(1)("Value_Field_List_Item_Default").ToString() = "1") Then
            ''                Column = ConfigureColumnForOCR(Column)              ' Configurar la columna para mostrar las columnas obtenidas del OCR
            ''            End If
            ''        End If
            ''    End If

            ''    AddCell(Column)                                             ' Agregar la columna a la celda
            ''Next
        End Sub


        Sub CopyColumnProperties(originalColumn As IndexerTableColumn, clonedColumn As IndexerTableColumn)
            ' Copia todas las propiedades de la columna original a la columna clonada
            clonedColumn.Width = originalColumn.Width
            clonedColumn.Cantidad_Decimales = originalColumn.Cantidad_Decimales
            clonedColumn.DefaultValue = originalColumn.DefaultValue
            clonedColumn.DisplayMember = originalColumn.DisplayMember
            clonedColumn.Es_Obligatorio = originalColumn.Es_Obligatorio
            clonedColumn.FormatoFecha = originalColumn.FormatoFecha
            clonedColumn.Header = originalColumn.Header
            clonedColumn.HeaderText = originalColumn.HeaderText
            clonedColumn.IsReadOnly = originalColumn.IsReadOnly
            clonedColumn.Items = originalColumn.Items
            clonedColumn.Mascara = originalColumn.Mascara
            clonedColumn.MaximumLength = originalColumn.MaximumLength
            clonedColumn.Table = originalColumn.Table
            clonedColumn.Type = originalColumn.Type
            clonedColumn.Usa_Decimales = originalColumn.Usa_Decimales
            clonedColumn.ValueMember = originalColumn.ValueMember
        End Sub

        ''' <summary>
        ''' Inicializa la lista de columnas OCR con valores predeterminados si está vacía.
        ''' </summary>
        Private Sub InitializeTableColumnIfEmpty()
            If _table.tableColumnnOCR.Count = 0 Then                        ' Verificar si la lista de columnas OCR está vacía
                _table.tableColumnnOCR = InitializeTableColumnOCRDefault()  ' Si está vacía, inicializarla con valores predeterminados
            End If
        End Sub


        Private Sub ClearCells()
            _cells.Clear()
            BodyFlowLayoutPanel.Controls.Clear()

            UpdateSize()
        End Sub

        Private Sub AddCell(ByVal nColumn As IndexerTableColumn)
            Dim newCell = CreateCell(nColumn)

            BodyFlowLayoutPanel.Controls.Add(newCell)
            _cells.Add(newCell)

            UpdateSize()
        End Sub

        Private Sub RemoveCell()
            Dim cellToRemove = _cells.Remove()
            BodyFlowLayoutPanel.Controls.Remove(cellToRemove)

            UpdateSize()
        End Sub

        Sub UpdateSize()
            Me.Width = MaxWidth()
            Me.Height = MaxHeight()

            If (Me.Header IsNot Nothing) Then Me.Header.Height = Me.Height + 2
        End Sub

#End Region

#Region " Funciones "

        ''' <summary>
        ''' Inicializa la columna de tabla OCR predeterminada con valores predefinidos.
        ''' </summary>
        ''' <returns>Un DataView que representa la columna de tabla OCR predeterminada.</returns>
        ''' <remarks></remarks>
        Private Function InitializeTableColumnOCRDefault() As DataView

            Using dataTableDefault As New DataTable()

                dataTableDefault.Columns.Add("Label_Field_List_Item_Default", GetType(String)).MaxLength = 10
                dataTableDefault.Columns.Add("Value_Field_List_Item_Default", GetType(String)).MaxLength = 10
                dataTableDefault.Rows.Add("", "")
                Return New DataView(dataTableDefault)
            End Using
        End Function

        ''' <summary>
        ''' Configura una columna para que sea de tipo Lista y asigna propiedades relacionadas.
        ''' </summary>
        ''' <param name="_datacolumn">La columna que se va a configurar.</param>
        ''' <returns>La columna configurada.</returns>
        Private Function ConfigureColumnForOCR(_datacolumn As IndexerTableColumn) As IndexerTableColumn
            _datacolumn.Type = DesktopConfig.CampoTipo.Lista
            _datacolumn.Items = _table.tableColumnnOCR
            _datacolumn.ValueMember = "Value_Field_List_Item_Default"
            _datacolumn.DisplayMember = "Label_Field_List_Item_Default"

            ' Devolver la columna configurada
            Return _datacolumn
        End Function

        Private Function CreateCell(ByVal nColumn As IndexerTableColumn) As IndexerTableCell
            Dim newCell As IndexerTableCell

            Select Case nColumn.Type
                Case DesktopConfig.CampoTipo.Numerico
                    Dim tempCell = New IndexerTableCellNumeric(Me, nColumn)

                    tempCell.MaximumLength = nColumn.MaximumLength
                    tempCell.UsaDecimales = nColumn.Usa_Decimales

                    If (tempCell.UsaDecimales) Then
                        tempCell.CantidadDecimales = nColumn.Cantidad_Decimales
                    End If

                    newCell = tempCell

                Case DesktopConfig.CampoTipo.Fecha
                    Dim tempCell = New IndexerTableCellDateTime(Me, nColumn)

                    tempCell.Mascara = nColumn.Mascara
                    tempCell.FormatoFecha = nColumn.FormatoFecha

                    newCell = tempCell

                Case DesktopConfig.CampoTipo.Lista
                    Dim tempCell = New IndexerTableCellList(Me, nColumn, DesktopConfig.CampoTipo.Lista)

                    newCell = tempCell

                Case DesktopConfig.CampoTipo.SiNo
                    Dim tempCell = New IndexerTableCellList(Me, nColumn, DesktopConfig.CampoTipo.SiNo)

                    newCell = tempCell

                Case Else
                    Dim tempCell = New IndexerTableCellText(Me, nColumn)

                    tempCell.MaximumLength = nColumn.MaximumLength
                    tempCell.Mascara = nColumn.Mascara

                    newCell = tempCell

            End Select

            newCell.Width = nColumn.Width
            newCell.ShowSecondControls = _table.ShowSecondControls

            newCell.ReadOnly = nColumn.IsReadOnly

            If (nColumn.DefaultValue <> "") Then
                newCell.Value = nColumn.DefaultValue
            End If

            newCell.Anchor = AnchorStyles.Bottom

            Return newCell
        End Function

        Private Function MaxHeight() As Integer
            Dim max As Integer = 20

            For Each Cell In _cells
                If (max < Cell.BaseHeight + 1) Then max = Cell.BaseHeight + 1
            Next

            Return max
        End Function

        Private Function MaxWidth() As Integer
            Dim max As Integer = 20

            For Each Cell In _cells
                max += Cell.BaseWidth
            Next

            If ((_table.BodyFlowLayoutPanel.Width - 20) > max) Then
                Return _table.BodyFlowLayoutPanel.Width - 20
            Else
                Return max
            End If
        End Function

        Public Function IsEmpty() As Boolean
            For Each Cell In _cells
                If (Not Cell.IsEmpty() And Not Cell.ShowSecondControls) Then
                    Return False
                End If
            Next

            Return True
        End Function

#End Region

    End Class

End Namespace