Imports System.Windows.Forms
Imports Miharu.Desktop.Library.Config
Imports Slyg.Tools
Imports System.Drawing
Imports System.Diagnostics ' NOTE: Se debe eliminar junto a start y stop
Imports MyIndexerTable = Miharu.Imaging.Indexer.View.Indexacion.Table.IndexerTable

Namespace View.Indexacion

    Public Class TableInputControl
        Inherits InputControl
        Implements IInputControl

#Region " Declaraciones "

        Private _Tipo As DesktopConfig.CampoTipo = DesktopConfig.CampoTipo.TablaAsociada
        Private _DefinicionCaptura As List(Of DefinicionCaptura)

        Private _Value As DataTable
        Private _ValueOld1 As DataTable
        Private _ValueOld2 As DataTable
        Private _ValueOld3 As DataTable

        Private _uniqueColumnValues2 As New HashSet(Of ColumnValue)()
        Private _uniqueColumnValues As New HashSet(Of ColumnValue)()    ' Almacena los valores únicos de las columnas que el OCR ha leído
        Private _rowIndexUpdated As Boolean                        ' Variable de control para indicar si rowIndex ha sido actualizado
        Private _modifiedRowIndex As Integer

        ' Variables control paginación Tabla Asociada
        Private _currentPage As Integer = 1                    ' Página actual
        Private _pageSize As Integer = 50                      ' Tamaño de filas de la páginacion
        Private _ValuePage As New DataTable()                  ' Valores almacenados por pagina
        Private _levelConfidencieCell As New DataTable()       ' Valores con niveles de confianza del OCR por celda
        Private _isTableOCREnabled As Boolean = False          ' variable de control para el estado de drawtableOCR

        Private dictionaryOCRORder As New Dictionary(Of Integer, CleanedTableDataStructure)()

        ' Define un tipo de tupla explícitamente
        Private Structure ColumnValue
            Public Value As Object
            Public CampoTabla As Integer
        End Structure

        ' Clase para los datos de salida oara limpieza de campos
        Public Class CleanedTableDataStructure
            Public idCampoTable As Integer
            Public cellDataConfidence As List(Of List(Of String))
        End Class



#End Region

#Region " Propiedades "

        Public Property MinRegistros As Short
        Public Property MaxRegistros As Short
        Public Property ShowControlRegistros As Boolean
        Public Property ShowControlValor As Boolean
        Public Property ControlRegistros As Integer
        Public Property ControlValor As Double
        Public Property ColumnasTotales As List(Of Integer)

        Public Property DataCellOrderOCR As Dictionary(Of Integer, CleanedTableDataStructure)
            Get
                Return dictionaryOCRORder
            End Get
            Set(value As Dictionary(Of Integer, CleanedTableDataStructure))
                dictionaryOCRORder = value
            End Set
        End Property


        Public Property dataDictionaryOcr As Dictionary(Of Integer, List(Of String()))


        ' Propiedad para obtener y establecer el número de página actual.
        Public ReadOnly Property PaginationSize As Integer
            Get
                Return _pageSize
            End Get
        End Property

        ' Propiedad para obtener y establecer el número de página actual.
        Public Property PageNumber As Integer
            Get
                Return _currentPage
            End Get
            Set(value As Integer)
                _currentPage = value
            End Set
        End Property

        Public ReadOnly Property ViewRowsActually As Integer
            Get
                Return IndexerView.GridControl.Rows.Count
            End Get
        End Property

        ' Propiedad para almacenar los niveles de confianza obtenidos por el OCR
        Public Property LevelConfidenceCell As DataTable
            Get
                Return _levelConfidencieCell
            End Get
            Set(value As DataTable)
                _levelConfidencieCell = value
            End Set
        End Property


#End Region

#Region " Constructores "

        Public Sub New()
            ' Llamada necesaria para el diseñador.
            InitializeComponent()

            ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
            _DefinicionCaptura = New List(Of DefinicionCaptura)

            Me.UsaTrigger = False
            Me.TriggerValues = New List(Of KeyValueItem)
            Me.ColumnasTotales = New List(Of Integer)

            '_ValueOld1 = New DataTable()
            '_ValueOld2 = New DataTable()
            '_ValueOld3 = New DataTable()

        End Sub

#End Region

#Region " Implementación IIndexerControl "

        Public Overloads Property Etiqueta As String Implements IInputControl.Etiqueta
            Get
                Return MyBase.Etiqueta
            End Get
            Set(ByVal value As String)
                MyBase.Etiqueta = value
            End Set
        End Property

        Public Property Value As Object Implements IInputControl.Value
            Get
                Return _Value
            End Get
            Set(ByVal value As Object)
                _Value = CType(value, DataTable)
                CloneAndModifyDataTable(_Value)        ' Clona la informacion para variable visualización
                SetupColumnsLevelConfidencieCell()     ' crea las columnas de la tabla donde se almacenan los valores de confianza obtenidos a través del OCR.
            End Set
        End Property

        Public Property ValorValidacion As Object Implements IInputControl.ValorValidacion
            Get
                Return _Value
            End Get
            Set(ByVal value As Object)
                _Value = CType(value, DataTable)
                CloneAndModifyDataTable(_Value)      ' Clona la informacion para variable visualización
                SetupColumnsLevelConfidencieCell()   ' crea las columnas de la tabla donde se almacenan los valores de confianza obtenidos a través del OCR.
            End Set
        End Property

        Public Property ValueControl As Boolean Implements IInputControl.ValueControl


        Public Property ÏsOCRCapture As Boolean Implements IInputControl.ÏsOCRCapture

        Public Property EnableTableOCR As Boolean Implements IInputControl.EnableTableOCR
            Get
                Return _isTableOCREnabled
            End Get
            Set(value As Boolean)
                _isTableOCREnabled = value
            End Set
        End Property


        Public Property ValueValidacionListas As Object Implements IInputControl.ValueValidacionListas

        Public Property ValueOld1 As Object Implements IInputControl.ValueOld1
            Get
                Return _ValueOld1
            End Get
            Set(ByVal value As Object)
                _ValueOld1 = CType(value, DataTable)
            End Set
        End Property

        Public Property ValueOld2 As Object Implements IInputControl.ValueOld2
            Get
                Return _ValueOld2
            End Get
            Set(ByVal value As Object)
                _ValueOld2 = CType(value, DataTable)
            End Set
        End Property

        Public Property ValueOld3 As Object Implements IInputControl.ValueOld3
            Get
                Return _ValueOld3
            End Get
            Set(ByVal value As Object)
                _ValueOld3 = CType(value, DataTable)
            End Set
        End Property

        Public Property NextControl As Windows.Forms.Control Implements IInputControl.NextControl

        Public Property ShowSecondControls As Boolean Implements IInputControl.ShowSecondControls
            Get
                Return False
            End Get
            Set(ByVal value As Boolean)

            End Set
        End Property

        Public Property CampoCaptura As CampoCaptura Implements IInputControl.CampoCaptura

        Public Property CampoLlaveCaptura As CampoLlaveCaptura Implements IInputControl.CampoLlaveCaptura

        Public ReadOnly Property DefinicionCaptura As List(Of DefinicionCaptura) Implements IInputControl.DefinicionCaptura
            Get
                Return _DefinicionCaptura
            End Get
        End Property

        Public Property IndexerView As IIndexerView Implements IInputControl.IndexerView

        Public Property Tipo As DesktopConfig.CampoTipo Implements IInputControl.Tipo
            Get
                Return _Tipo
            End Get
            Set(ByVal value As DesktopConfig.CampoTipo)
                Select Case value
                    Case DesktopConfig.CampoTipo.TablaAsociada

                    Case Else
                        Throw New Exception("Tipo de dato no soportado por el control " + value.ToString())

                End Select

                _Tipo = value

            End Set
        End Property

        Public Property UsaTrigger As Boolean Implements IInputControl.UsaTrigger

        Public Property TriggerValues As List(Of KeyValueItem) Implements IInputControl.TriggerValues

        Public Property TriggerValidaciones As List(Of TriggersValidations_Items) Implements IInputControl.TriggerValidaciones

        Public Property IsVisible As Boolean Implements IInputControl.IsVisible
            Get
                Return Me.Visible
            End Get
            Set(ByVal value As Boolean)
                Me.Visible = value
            End Set
        End Property


        Public Function Validar() As Boolean Implements IInputControl.Validar
            If (IndexerView.GridControl.Rows.Count > 0) Then
                If (IndexerView.GridControl.Rows(IndexerView.GridControl.Rows.Count - 1).IsEmpty()) Then
                    IndexerView.GridControl.DeleteRow()
                End If
            End If

            'Luego de que se eliminan las posibles celdas vacías, se valida si la tabla tiene valores.
            If ((IndexerView.GridControl.Rows.Count = 0) And (DefinicionCaptura.Item(0).Es_Obligatorio_Campo) And (Me.Visible)) Then
                MessageBox.Show("El campo [" & Me.Etiqueta & "] es obligatorio.", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.Focus()
                Return False
            End If

            Try
                'Dim Fil As Integer = 0

                For Each Fila In IndexerView.GridControl.Rows
                    'Dim Col As Integer = 0

                    For Each Cell In Fila.Cells
                        'Dim Definicion = _DefinicionCaptura.Item(Col)

                        If (Not Cell.ReadOnly) Then
                            If Not Cell.Validar() Then Return False

                            'Select Case Definicion.Type
                            '    Case DesktopConfig.CampoTipo.Texto
                            '        If (Cell.Value.ToString().Trim() = "" And Definicion.Es_Obligatorio_Campo) Then
                            '            MessageBox.Show("El campo " & Etiqueta & ", Fila: [" & (Fil + 1) & "] Columna: [" & Definicion.Caption & "] es obligatorio", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            '            Cell.Focus()

                            '            Return False
                            '        End If

                            '    Case DesktopConfig.CampoTipo.Numerico
                            '        If (CDbl(Cell.Value) = 0 And Definicion.Es_Obligatorio_Campo) Then
                            '            MessageBox.Show("El campo " & Etiqueta & ", Fila: [" & (Fil + 1) & "] Columna: [" & Definicion.Caption & "] debe ser un valor numérico", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            '            Cell.Focus()

                            '            Return False
                            '        End If

                            'End Select
                        End If

                        'Col += 1
                    Next

                    'Fil += 1
                Next

                ' Validar registros mínimos
                If (Me.MinRegistros > 0 AndAlso IndexerView.GridControl.Rows.Count < Me.MinRegistros) Then
                    MessageBox.Show("Se deben capturar mínimo " & Me.MinRegistros.ToString("#,###") & " registros", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return False
                End If

                ' Validar registros máximos
                If (Me.MaxRegistros > 0 AndAlso IndexerView.GridControl.Rows.Count > Me.MaxRegistros) Then
                    MessageBox.Show("Se deben capturar máximo " & Me.MaxRegistros.ToString("#,###") & " registros", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return False
                End If

                ' Validar totales
                If (Me.ShowControlRegistros) Then
                    If (IndexerView.GridControl.Rows.Count <> IndexerView.GridControl.ControlRegistros) Then
                        MessageBox.Show("El total de registros capturados no coincide con la validación, se capturaron " & IndexerView.GridControl.Rows.Count & " registros y se esperaban " & IndexerView.GridControl.ControlRegistros & " registros", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Return False
                    End If
                End If

                If (Me.ShowControlValor) Then
                    Dim TotalValor As Double = 0

                    For Each Fila In IndexerView.GridControl.Rows
                        For Each Col In Me.ColumnasTotales
                            Dim idCelda = 0

                            For Each Definicion In _DefinicionCaptura
                                If (Definicion.id = Col) Then
                                    Dim Cell = Fila.Cells(idCelda)

                                    Select Case Definicion.Type
                                        Case DesktopConfig.CampoTipo.Numerico
                                            TotalValor += DataConvert.ToNumericD(Cell.Value.ToString().Trim(), ".")

                                        Case Else
                                            MessageBox.Show("El campo " & Definicion.Caption & " no se puede validar como total por no ser un valor numérico, por favor valide la configuración del sistema", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                            Return False

                                    End Select

                                    Exit For
                                End If

                                idCelda += 1
                            Next
                        Next

                        'Fil += 1
                    Next
                    If (CDec(TotalValor).ToString("f2") <> CDec(IndexerView.GridControl.ControlValor).ToString("f2")) Then
                        MessageBox.Show("El valor total de los registros capturados no coincide con la validación, se capturó un valor total de " & TotalValor & " y se esperaba " & IndexerView.GridControl.ControlValor, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Return False
                    End If
                End If

            Catch ex As Exception
                Throw
            End Try

            Return True
        End Function

        Public Sub LoadDefinition(ByVal nDefinicionCaptura As List(Of DefinicionCaptura)) Implements IInputControl.LoadDefinition
            _DefinicionCaptura = nDefinicionCaptura
        End Sub

        Public Property IsCalidad As Boolean Implements IInputControl.IsCalidad

        Public ReadOnly Property RequiereAutorizacion As Boolean Implements IInputControl.RequiereAutorizacion
            Get
                If (_ValueOld3 IsNot Nothing) Then

                    For Fil As Integer = 0 To _Value.Rows.Count - 1
                        For Col As Integer = 0 To _Value.Columns.Count - 1
                            If (_Value.Rows(Fil).Item(Col).ToString() <> _ValueOld1.Rows(Fil).Item(Col).ToString() _
                               AndAlso _Value.Rows(Fil).Item(Col).ToString() <> _ValueOld2.Rows(Fil).Item(Col).ToString() _
                               AndAlso _Value.Rows(Fil).Item(Col).ToString() <> _ValueOld3.Rows(Fil).Item(Col).ToString()) Then
                                Return True
                            End If
                        Next
                    Next
                End If

                Return False
            End Get
        End Property

        Public Property ShowValidacionListasControls As Boolean Implements IInputControl.ShowValidacionListasControls
            Get
                Return False
            End Get
            Set(ByVal value As Boolean)

            End Set
        End Property

        Public Property ShowPrimaryControls As Boolean Implements IInputControl.ShowPrimaryControls
            Get
                Return True
            End Get
            Set(ByVal value As Boolean)

            End Set
        End Property
#End Region

#Region " Eventos "

        Private Sub SelectButton_Enter(ByVal sender As Object, ByVal e As EventArgs) Handles SelectButton.Enter
            Me.IndexerView.SelectedInputControl = Me
        End Sub

        Private Sub TableInputControl_Enter(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Enter
            SelectButton.Focus()
        End Sub

        Private Sub SelectButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles SelectButton.Click
            ShowGrid()
        End Sub

#End Region


#Region " Metodos "

        Public Sub DisposeUnusedControls()
            If (IndexerView.GridControl IsNot Nothing) Then
                ClearRowsAndColumns(IndexerView.GridControl)
            End If
        End Sub

        Public Sub ProcessRecibiedDataOCRTable()

        End Sub

        ''' <summary>
        ''' Clona el DataTable proporcionado y modifica la longitud máxima de sus columnas.
        ''' </summary>
        ''' <param name="originalDataTable">El DataTable original que se clonará.</param>
        Public Sub CloneAndModifyDataTable(ByVal originalDataTable As DataTable)

            Dim clonedDataTable As DataTable = originalDataTable.Clone() ' Clonar el DataTable

            For Each column As DataColumn In clonedDataTable.Columns     ' Modificar la longitud máxima de las columnas clonadas
                column.MaxLength = -1                                    ' Sin límite de longitud 
            Next
            _ValuePage = clonedDataTable                                 ' Asignar el DataTable clonado a la variable _ValuePage
        End Sub

        ''' <summary>
        ''' Configura las columnas del DataTable _levelConfidencieCell basándose en las columnas de _ValuePage.
        ''' </summary>
        Public Sub SetupColumnsLevelConfidencieCell()

            If _levelConfidencieCell.Columns.Count = 0 Then

                For Each valuePageColumn As DataColumn In _ValuePage.Columns
                    ' Crea una nueva columna en _levelConfidencieCell con el mismo nombre y tipo (Integer).
                    Dim newColum As New DataColumn(valuePageColumn.ColumnName, GetType(Integer))
                    _levelConfidencieCell.Columns.Add(newColum)
                Next
            End If
        End Sub

        ''' <summary>
        ''' Carga datos de _ValuePage en el DataTable _levelConfidencieCell.
        ''' </summary>
        Public Sub LoadDataIntoLevelConfidenceCell()

            If _ValuePage IsNot Nothing AndAlso _ValuePage.Rows.Count > 0 Then
                For Row As Integer = 0 To _ValuePage.Rows.Count - 1
                    ' Crea una nueva fila en _levelConfidencieCell.
                    Dim NewRowPage = _levelConfidencieCell.NewRow()
                    _levelConfidencieCell.Rows.Add(NewRowPage)
                Next
            End If

        End Sub

        ''' <summary>
        ''' Limpia las filas del nivel de confianza del OCR si la captura OCR está habilitada y hay filas presentes.
        ''' </summary>
        Private Sub ClearOcrConfidenceRows()
            If Me.ÏsOCRCapture AndAlso _levelConfidencieCell.Rows.Count > 0 Then
                _levelConfidencieCell.Rows.Clear() ' Limpia las filas del nivel de confianza del OCR
            End If
        End Sub


        '' Muestra los datos almacenados previamente en la tabla
        Public Sub ShowData()

            If ClearRows() Then

                If _ValuePage.Rows.Count = 0 Then                           ' Contiene datos almacenados de varias ejecuciones OCR Tabla

                    For Fil As Integer = 0 To _Value.Rows.Count - 1
                        IndexerView.GridControl.AddRow()

                        For Col As Integer = 0 To _Value.Columns.Count - 1
                            If (DefinicionCaptura(Col).IsReadOnly) Then
                                IndexerView.GridControl.Rows(Fil).Cells(Col).Value = DefinicionCaptura(Col).DefaultValue
                            Else
                                IndexerView.GridControl.Rows(Fil).Cells(Col).Value = _Value.Rows(Fil).Item(Col).ToString()
                            End If
                        Next
                    Next

                Else

                    For Fil As Integer = 0 To _ValuePage.Rows.Count - 1
                        IndexerView.GridControl.AddRow()

                        For Col As Integer = 0 To _ValuePage.Columns.Count - 1
                            If (DefinicionCaptura(Col).IsReadOnly) Then
                                IndexerView.GridControl.Rows(Fil).Cells(Col).Value = DefinicionCaptura(Col).DefaultValue
                            Else
                                IndexerView.GridControl.Rows(Fil).Cells(Col).Value = _ValuePage.Rows(Fil).Item(Col).ToString()
                            End If
                        Next
                    Next

                End If
            End If
        End Sub

        ''' <summary>
        ''' Procesa la confianza de una celda y colorea la celda en función del valor de confianza OCR.
        ''' </summary>
        ''' <param name="fil">El índice de fila actual.</param>
        ''' <param name="col">El índice de columna actual.</param>
        ''' <param name="rowOffset">El desplazamiento de fila con respecto al índice de inicio.</param>
        Private Sub ProcessCellConfidence(fil As Integer, col As Integer, rowOffset As Integer)

            Dim cellValueConfidencie As Object = _levelConfidencieCell.Rows(fil)(col)   ' Obtener el valor de confianza de la celda actual en la tabla de nivel de confianza.

            If Not IsDBNull(cellValueConfidencie) Then
                Dim ocrConfidence As Integer = CInt(cellValueConfidencie)
                If ocrConfidence > 0 Then
                    ColorCellBasedOnValue(ocrConfidence, rowOffset, col)                ' Aplicar color a la celda basado en el valor de confianza OCR.
                End If
            End If

        End Sub

        ''' <summary>
        ''' Reinicia y establece los datos relacionados con la funcionalidad OCR.
        ''' </summary>
        Public Sub SetDataOcr()
            Me.dataDictionaryOcr = Nothing
            Me._uniqueColumnValues = Nothing
            _ValuePage.Rows.Clear()
            IndexerView.GridControl.tableColumnnOCR = New DataView()
        End Sub

        ''' <summary>
        ''' Colorea una celda en función del valor proporcionado dentro de un rango específico.
        ''' </summary>
        ''' <param name="value">El valor para determinar el color de la celda.</param>
        ''' <param name="rowIndex">El índice de fila de la celda que se va a colorear.</param>
        ''' <param name="columnIndex">El índice de columna de la celda que se va a colorear.</param>
        Private Sub ColorCellBasedOnValue(ByVal value As Integer, ByVal rowIndex As Integer, ByVal columnIndex As Integer)

            Const TopLimitThreshold As Integer = 75                     ' Definir umbral de rango específicos nivel de confianza OCR alto 
            Const IntermediateLimitThreshold As Integer = 45            ' Definir umbral de rango específicos nivel de confianza OCR medio

            Dim cellColor As Color                                      ' Declarar una variable para el color de la celda

            Select Case value                                           ' Determinar el color de la celda en función del valor proporcionado
                Case Is > TopLimitThreshold
                    cellColor = Color.Green
                Case Is > IntermediateLimitThreshold
                    cellColor = Color.Orange
                Case Else
                    cellColor = Color.Red
            End Select

            Dim gridControl As MyIndexerTable = IndexerView.GridControl  ' Almacenar una referencia local al objeto GridControl para mejorar la legibilidad y el rendimiento

            ' Configurar el color de fondo de la celda
            gridControl.Rows(rowIndex).Cells(columnIndex).BackgroundImage = Nothing
            gridControl.Rows(rowIndex).Cells(columnIndex).BackColor = cellColor

            gridControl.Invalidate()                                     ' Configurar el color de fondo de la celda

        End Sub

        ''' <summary>
        ''' Agrega una nueva fila a la tabla y visualiza los datos OCR en las celdas correspondientes.
        ''' </summary>
        ''' <param name="RowActually">El índice de la fila actual.</param>
        Public Sub VisualizateDataRowOCR(ByVal RowActually As Integer)
            Try
                If Me.dataDictionaryOcr IsNot Nothing Then

                    ' itera sobre las columnas actuales de la tabla
                    For ColumnActually As Integer = 0 To IndexerView.GridControl.Columns.Count - 1

                        ' verifica la existencia de la columna en el diccionario OCR
                        If dataDictionaryOcr.ContainsKey(ColumnActually) Then

                            Dim dataList As List(Of String()) = dataDictionaryOcr(ColumnActually)                        ' Extrae los datos leidos y su nivel de confianza de esa columnas

                            If RowActually >= 0 AndAlso RowActually < dataList.Count Then                               ' Verifica que el índice de fila sea válido

                                Dim currentElement As String() = dataList(RowActually)                                   ' Extrae los datos de la fila 

                                If currentElement.Length > 0 Then                                                        ' Verifica si el elemento contiene datos

                                    Dim textReadOCR As String = currentElement(0)
                                    Dim confidenceReadOCR As String = currentElement(1)
                                    Dim ocrText As String = If(textReadOCR Is Nothing, "", textReadOCR)                                         ' Almacena el texto que fue leido de esa celda en especifico

                                    If _DefinicionCaptura.Item(ColumnActually).Type = DesktopConfig.CampoTipo.Texto Then   ' Verifica si el tipo de campo es texto antes de procesar

                                        Dim currentRowCount As Integer = IndexerView.GridControl.Rows.Count - 1
                                        IndexerView.GridControl.Rows(currentRowCount).Cells(ColumnActually).Value = ocrText       ' Establece el valor del texto reconocido en la celda
                                        'ColorCellBasedOnValue(ocrConfidence, currentRowCount, columnIndex)                     ' Establece el color del texto segun su confidencialidad
                                        '_levelConfidencieCell.Rows(rowLevelConfidencieCell)(columnIndex) = ocrConfidence
                                    End If
                                End If
                            End If
                        End If
                    Next
                End If
            Catch ex As Exception
                Throw
            End Try
        End Sub

        ''' <summary>
        ''' Procesa y agrega datos OCR para la columna seleccionada.
        ''' </summary>
        ''' <param name="ColumnActuallyDictionary">El índice de la columna en el diccionario de datos actual.</param>
        Public Sub ProcessAndAddOcrDataForSelectedColumn(ByVal ColumnActuallyDictionary As Integer)

            Try
                If Me.dataDictionaryOcr IsNot Nothing Then

                    Dim valueOrderCells As New List(Of List(Of String))()

                    For ColumnActually As Integer = 0 To IndexerView.GridControl.Columns.Count - 1    ' Recorre cada celda (columnas) en la cuadrícula.

                        If Me.ÏsOCRCapture Then

                            If Me._uniqueColumnValues.Count > 0 AndAlso ColumnActually >= 0 AndAlso ColumnActually < Me._uniqueColumnValues.Count Then

                                Dim datastring = _uniqueColumnValues.ElementAt(ColumnActually).Value.ToString()
                                If Me._uniqueColumnValues.Count > 0 AndAlso _uniqueColumnValues.ElementAt(ColumnActually).Value.ToString().Contains(CStr(ColumnActuallyDictionary + 1)) Then   ' Verifica si existen valores únicos y si la columna actual contiene la clave

                                    For Each value As String() In dataDictionaryOcr(ColumnActuallyDictionary)
                                        Dim result As New List(Of String)()
                                        Dim textReadOCR As String = value(0)
                                        Dim confidenceReadOCR As String = value(1)
                                        result.Add(textReadOCR)
                                        result.Add(confidenceReadOCR)

                                        valueOrderCells.Add(result)
                                    Next
                                    If valueOrderCells.Count > 0 Then
                                        Dim cleanedData As New CleanedTableDataStructure()
                                        cleanedData.cellDataConfidence = valueOrderCells
                                        cleanedData.idCampoTable = _uniqueColumnValues.ElementAt(ColumnActually).CampoTabla
                                        dictionaryOCRORder.Add(ColumnActually, cleanedData)
                                        Exit For
                                    End If
                                End If
                            End If
                        End If
                    Next
                End If
            Catch ex As Exception
                Throw
            End Try
        End Sub

        ''' <summary>
        ''' Procesa una celda en la cuadrícula según los índices de fila y columna proporcionados.
        ''' </summary>
        ''' <param name="rowIndex">El índice de fila de la celda que se va a procesar.</param>
        ''' <param name="columnIndex">El índice de columna de la celda que se va a procesar.</param>
        ''' <remarks></remarks>
        Private Function ProcessGridCell(ByVal rowIndex As Integer, ByVal columnIndex As Integer) As List(Of List(Of String))

            Dim resultList As New List(Of List(Of String))

            Try
                If Me.ÏsOCRCapture Then

                    If Me._uniqueColumnValues.Count > 0 AndAlso columnIndex >= 0 AndAlso columnIndex < Me._uniqueColumnValues.Count Then

                        For Each kvp As KeyValuePair(Of Integer, List(Of String())) In dataDictionaryOcr                                        ' Itera a través de las entradas del diccionario de datos
                            Dim key As String = (kvp.Key + 1).ToString()

                            If Me._uniqueColumnValues.Count > 0 AndAlso _uniqueColumnValues.ElementAt(columnIndex).Value.ToString().Contains(key) Then   ' Verifica si existen valores únicos y si la columna actual contiene la clave
                                ProcessDataDictionaryEntry(kvp.Key, rowIndex, columnIndex)                                                         ' Procesa la entrada del diccionario de datos para la clave especificada
                            End If
                        Next
                    Else
                        ' Manejar el caso donde columnIndex está fuera de los límites
                        'MessageBox.Show("La columna especificada está fuera de los límites.", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    End If
                End If
            Catch ex As Exception
                Throw
            End Try

            Return resultList  ' Devolver la lista de resultados
        End Function

        ''' <summary>
        ''' Asigna un valor OCR a la celda enfocada en la tabla.
        ''' </summary>
        ''' <param name="dataOCR">El valor OCR a asignar a la celda.</param>
        Public Sub AssignOCRCellValue(ByVal dataOCR As String)

            If IndexerView.GridControl IsNot Nothing Then
                Dim selectedRow As Integer = IndexerView.GridControl.FocusedRowIndex
                Dim selectedColumn As Integer = IndexerView.GridControl.FocusedColumnIndex

                If selectedRow >= 0 AndAlso selectedColumn >= 0 Then
                    Dim row As Table.IndexerTableRow = IndexerView.GridControl.Rows(selectedRow)

                    ' Verifica si la celda existe en la posición indicada.
                    If selectedColumn < row.Cells.Count Then
                        Dim cell As Table.IndexerTableCell = row.Cells(selectedColumn)

                        ' Verifica el tipo de la celda y asigna el valor solo si es numérico o texto.
                        If cell.Type = DesktopConfig.CampoTipo.Numerico OrElse cell.Type = DesktopConfig.CampoTipo.Texto Then
                            cell.Value = dataOCR
                        End If
                    End If
                End If
            End If
        End Sub


        ''' <summary>
        ''' Procesa una entrada del diccionario de datos para una clave dada.
        ''' </summary>
        ''' <param name="dictionaryKey">La clave del diccionario de datos a procesar.</param>
        ''' <param name="rowIndex">El índice de fila para la celda a procesar.</param>
        ''' <param name="columnIndex">El índice de columna para la celda a procesar.</param>
        ''' <remarks></remarks>
        Private Sub ProcessDataDictionaryEntry(ByVal dictionaryKey As Integer, ByVal rowIndex As Integer, ByVal columnIndex As Integer)
            Try
                Dim defaultOcrConfidence As Integer = 70
                Dim defaultOcrDowConfidence As Integer = 20

                If dataDictionaryOcr.ContainsKey(dictionaryKey) Then                                            ' Verifica si la clave existe en el diccionario de datos

                    Dim dataList As List(Of String()) = dataDictionaryOcr(dictionaryKey)                        ' Extrae los datos leidos y su nivel de confianza de esa columnas

                    If rowIndex >= 0 AndAlso rowIndex < dataList.Count Then                                     ' Verifica que el índice de fila sea válido

                        Dim currentElement As String() = dataList(rowIndex)                                     ' Extrae los datos de la fila 
                        Dim textReadOCR As String = currentElement(0)
                        Dim confidenceReadOCR As String = currentElement(1)

                        If currentElement.Length > 0 Then                                                       ' Verifica si el elemento contiene datos

                            Dim ocrText As String = If(textReadOCR Is Nothing, "", textReadOCR)                                         ' Almacena el texto que fue leido de esa celda en especifico
                            Dim rowLevelConfidencieCell As Integer = _levelConfidencieCell.Rows.Count - 1

                            Dim ocrConfidence As Integer '= defaultOcrConfidence                                 ' establece un valor por defecto si no puede reconocer el dato enviado por el OCR 
                            Dim confidenceNumber As Integer
                            If Not Integer.TryParse(confidenceReadOCR, confidenceNumber) Then
                                confidenceNumber = 0
                            End If

                            If confidenceNumber > 0 Then
                                Dim numberConfidence As Long
                                numberConfidence = CLng(confidenceNumber)
                                If numberConfidence <= 100 Then
                                    ocrConfidence = CInt(numberConfidence)
                                Else
                                    ocrConfidence = defaultOcrConfidence 'TODO: Establecer un valor estandar para ello
                                End If
                            Else
                                ocrConfidence = defaultOcrDowConfidence 'TODO: Establecer un valor estandar para ello
                            End If

                            If _DefinicionCaptura.Item(columnIndex).Type = DesktopConfig.CampoTipo.Texto Then   ' Verifica si el tipo de campo es texto antes de procesar

                                Dim currentRowCount As Integer = IndexerView.GridControl.Rows.Count - 1

                                IndexerView.GridControl.Rows(currentRowCount).Cells(columnIndex).Value = ocrText       ' Establece el valor del texto reconocido en la celda
                                ColorCellBasedOnValue(ocrConfidence, currentRowCount, columnIndex)                     ' Establece el color del texto segun su confidencialidad
                                _levelConfidencieCell.Rows(rowLevelConfidencieCell)(columnIndex) = ocrConfidence
                            End If
                        End If
                    End If
                End If
            Catch ex As Exception
                Throw
            End Try
        End Sub

        ''' <summary>
        ''' Elimina todas las filas de la vista de la tabla del indexador y desactiva la marca de ítem de la fila de la tabla.
        ''' </summary>
        Public Sub DeleteRowsItems()
            Dim gridControl As MyIndexerTable = IndexerView.GridControl         ' Almacenar una referencia local al objeto GridControl para mejorar la legibilidad y el rendimiento

            If ClearRows() Then
                gridControl.tableItemRowOCR = False
            End If
        End Sub

        ''' <summary>
        ''' Agrega una nueva fila a la vista de la tabla del indexador y establece el enfoque en ella.
        ''' </summary>
        Public Sub IndexerTableAddRow()

            Dim gridControl As MyIndexerTable = IndexerView.GridControl                             ' Almacenar una referencia local al objeto GridControl para mejorar la legibilidad y el rendimiento

            Dim rowCount As Integer = _Value.Rows.Count                                             ' Numero de filas almacenadas
            gridControl.tableItemRowOCR = Me.ÏsOCRCapture AndAlso (Me.dataDictionaryOcr Is Nothing) ' Variable control para agregar Items de cada fila por el
            gridControl.hasValuesRowsTable = Me.ÏsOCRCapture AndAlso (rowCount > 0)                 ' Variable control para establecer items por defecto OCR

            'gridControl.PageNumber = _currentPage                                                   ' Actualiza el valor de la pagina actual para mostrar el numero de la fila
            gridControl.AddRow()                                                                    ' Agrega una nueva fila
            gridControl.setFocus()
        End Sub

        ''' <summary>
        ''' Inicializa la columna de tabla OCR en el control de tabla.
        ''' </summary>
        Public Sub InitializeOCRTableColumn()
            IndexerView.GridControl.tableColumnnOCR = New DataView()
        End Sub

        Public Sub InitiliazeOCRDictionaryOCRORder()
            Me.dictionaryOCRORder = New Dictionary(Of Integer, CleanedTableDataStructure)()
        End Sub

        ''' <summary>
        ''' Obtiene datos OCR y actualiza la vista.
        ''' </summary>
        ''' <param name="ocrDataDictionary">Diccionario que contiene los datos OCR.</param>
        ''' <remarks></remarks>
        Public Sub getDataOCR(ocrDataDictionary As Dictionary(Of Integer, List(Of String())))

            Dim gridControl As MyIndexerTable = IndexerView.GridControl ' Almacenar una referencia local al objeto GridControl para mejorar la legibilidad y el rendimiento

            If ocrDataDictionary.Count > 0 Then

                Me.dataDictionaryOcr = ocrDataDictionary
                If Not gridControl.tableItemRowOCR Then gridControl.tableItemRowOCR = True

                If ValidateCell(gridControl) Then
                    SavePageDataOCR()
                End If

                Dim numberColumnsOCR As Integer = ocrDataDictionary.Count - 1
                gridControl.tableColumnnOCR = CreateTableFromOCRColumns(numberColumnsOCR)

                If ClearRows() Then
                    gridControl.AddRow()                               ' Agrega una nueva fila a la vista.
                    gridControl.setFocus()                             ' Establece el enfoque en la vista.
                End If
            End If
        End Sub


        ''' <summary>
        ''' Valida si las celdas de la primera fila de un control de tabla contienen datos significativos.
        ''' </summary>
        ''' <param name="gridControl">El control de tabla que se va a validar.</param>
        ''' <returns>Devuelve True si las celdas contienen datos significativos; de lo contrario, False.</returns>
        Private Function ValidateCell(ByVal gridControl As MyIndexerTable) As Boolean

            If gridControl.Rows.Count = 1 Then

                For Each cell As Indexacion.Table.IndexerTableCell In gridControl.Rows(0).Cells
                    If Not (cell.Value Is Nothing OrElse cell.Value.ToString().Trim() = "" OrElse cell.Value.ToString().Trim() = "0") Then
                        Return True
                    End If
                Next

                Return False
            Else
                Return True
            End If
        End Function


        ''' <summary>
        ''' Genera y agrega columnas al control de tabla especificado a partir de una lista de definiciones de captura.
        ''' </summary>
        ''' <param name="gridControl">El control de tabla al que se agregarán las columnas.</param>
        ''' <param name="definicionCaptura">La lista de definiciones de captura utilizadas para generar las columnas.</param>
        Private Sub GenerateColumns(ByVal gridControl As MyIndexerTable, ByVal definicionCaptura As List(Of DefinicionCaptura))

            If (definicionCaptura IsNot Nothing AndAlso definicionCaptura.Count > 0) Then

                ' Generar columnas
                For i As Integer = 0 To definicionCaptura.Count - 1

                    Dim definicion As DefinicionCaptura = definicionCaptura.Item(i)

                    ' Cambia el ancho de las columnas
                    Dim WidthColumn As Integer = 200
                    If definicionCaptura.Item(i).Type = DesktopConfig.CampoTipo.Lista Then
                        WidthColumn = 150
                    End If

                    Dim NewColumn As New Table.IndexerTableColumn() With {
                        .IdCampoTabla = definicion.id,
                        .HeaderText = definicion.Caption,
                        .Width = WidthColumn,
                        .Type = definicion.Type,
                        .IsReadOnly = definicion.IsReadOnly,
                        .DefaultValue = definicion.DefaultValue,
                        .Es_Obligatorio = definicion.Es_Obligatorio_Campo,
                        .MaximumLength = definicion.MaximumLength,
                        .Usa_Decimales = definicion.Usa_Decimales,
                        .Cantidad_Decimales = CShort(If(definicion.Usa_Decimales, definicion.Cantidad_Decimales, 0)),
                        .Items = definicion.Items,
                        .DisplayMember = definicion.DisplayMember,
                        .ValueMember = definicion.ValueMember,
                        .Mascara = definicion.Mascara,
                        .FormatoFecha = definicion.FormatoFecha
                    }

                    'NewColumn.Width = 200

                    gridControl.Columns.Add(NewColumn)

                Next
            End If

        End Sub

        ''' <summary>
        ''' Copia filas de una tabla de origen a una tabla de destino, realizando una conversión a cadenas de texto.
        ''' </summary>
        ''' <param name="sourceTable">La tabla de origen de la que se copiarán las filas.</param>
        ''' <param name="destinationTable">La tabla de destino donde se agregarán las filas copiadas.</param>
        Private Sub CopyRowsToPage(ByVal sourceTable As DataTable, ByVal destinationTable As DataTable)
            Dim rowCount As Integer = sourceTable.Rows.Count

            For Row As Integer = 0 To rowCount - 1
                Dim NewRow = destinationTable.NewRow()

                For Col As Integer = 0 To sourceTable.Columns.Count - 1
                    NewRow.Item(Col) = sourceTable.Rows(Row).Item(Col).ToString()
                Next

                destinationTable.Rows.Add(NewRow)
            Next
        End Sub

        Public Sub ShowGrid()

            ' Almacenar una referencia local al objeto GridControl para mejorar la legibilidad y el rendimiento
            Dim gridControl As MyIndexerTable = IndexerView.GridControl

            If ClearRowsAndColumns(gridControl) Then

                'ResetPaging()                                                                           ' Reinicia la paginación
                GenerateColumns(gridControl, _DefinicionCaptura)                                        ' Genera las columnas que tiene la tabla asociada

                gridControl.AllowAddRow = True
                gridControl.AllowDeleteRow = True
                gridControl.IsCalidad = Me.IsCalidad

                Dim rowCount As Integer = _Value.Rows.Count                                                                         ' Numero de filas almacenadas
                gridControl.tableItemRowOCR = Me.ÏsOCRCapture AndAlso (Me.dataDictionaryOcr Is Nothing) AndAlso _isTableOCREnabled  ' Variable control para agregar Items de cada fila por el
                gridControl.hasValuesRowsTable = Me.ÏsOCRCapture AndAlso (rowCount > 0) AndAlso _isTableOCREnabled                  ' Variable control para establecer items por defecto OCR

                ' Generar filas
                If (_ValueOld1 Is Nothing) Then

                    For Fil As Integer = 0 To _Value.Rows.Count - 1
                        IndexerView.GridControl.AddRow()

                        For Col As Integer = 0 To _Value.Columns.Count - 1
                            If (DefinicionCaptura(Col).IsReadOnly) Then
                                IndexerView.GridControl.Rows(Fil).Cells(Col).Value = DefinicionCaptura(Col).DefaultValue
                            Else
                                IndexerView.GridControl.Rows(Fil).Cells(Col).Value = _Value.Rows(Fil).Item(Col).ToString()
                            End If
                        Next
                    Next

                Else ' NOTE: Se debe trabajar la paginación (No esta implementada)
                    For Fil As Integer = 0 To _ValueOld1.Rows.Count - 1
                        IndexerView.GridControl.AddRow()
                        Dim Val1 As String
                        Dim Val2 As String
                        Dim Val3 As String = ""

                        For Col As Integer = 0 To _Value.Columns.Count - 1
                            Val1 = _ValueOld1.Rows(Fil).Item(Col).ToString()
                            Val2 = _ValueOld2.Rows(Fil).Item(Col).ToString()

                            If (Me.IsCalidad) Then
                                Val3 = _ValueOld3.Rows(Fil).Item(Col).ToString()
                            End If

                            If (Not Me.IsCalidad AndAlso Val1 <> Val2) Then
                                IndexerView.GridControl.Rows(Fil).Cells(Col).ReadOnly = False
                                IndexerView.GridControl.Rows(Fil).Cells(Col).ShowSecondControls = True
                            ElseIf (Me.IsCalidad AndAlso Val1 <> Val2 And Val1 <> Val3 And Val2 <> Val3) Then
                                IndexerView.GridControl.Rows(Fil).Cells(Col).ReadOnly = False
                                IndexerView.GridControl.Rows(Fil).Cells(Col).ShowSecondControls = False
                            Else
                                IndexerView.GridControl.Rows(Fil).Cells(Col).ReadOnly = True
                                IndexerView.GridControl.Rows(Fil).Cells(Col).ShowSecondControls = False
                            End If

                            If (IndexerView.GridControl.Rows(Fil).Cells(Col).ReadOnly) Then
                                IndexerView.GridControl.Rows(Fil).Cells(Col).Value = CStr(IIf(Me.IsCalidad, Val3, Val1))
                            ElseIf (Fil < _Value.Rows.Count) Then
                                IndexerView.GridControl.Rows(Fil).Cells(Col).Value = _Value.Rows(Fil).Item(Col).ToString()
                            End If

                            IndexerView.GridControl.Rows(Fil).Cells(Col).ValueOld1 = Val1
                            IndexerView.GridControl.Rows(Fil).Cells(Col).ValueOld2 = Val2
                            IndexerView.GridControl.Rows(Fil).Cells(Col).ValueOld3 = Val3
                        Next
                    Next
                End If

                ' Control de totales
                gridControl.MinRegistros = Me.MinRegistros
                gridControl.MaxRegistros = Me.MaxRegistros
                gridControl.ShowControlRegistros = Me.ShowControlRegistros
                gridControl.ShowControlValor = Me.ShowControlValor
                gridControl.ControlRegistros = Me.ControlRegistros
                gridControl.ControlValor = Me.ControlValor

                gridControl.AllowAddRow = (_ValueOld1 Is Nothing)
                gridControl.AllowDeleteRow = (_ValueOld1 Is Nothing)

                IndexerView.SelectedInputControl = Me
                IndexerView.ShowGridControl(Me)
                gridControl.PageNumber = _currentPage       ' Actualiza el valor de la pagina actual para mostrar el numero de la fila

                If (gridControl.Rows.Count = 0) Then
                    gridControl.AddRow()
                End If

                gridControl.setFocus()

            End If
        End Sub

#End Region


#Region " Funciones "

        ''' <summary>
        ''' Borra las filas de un control de tabla.
        ''' </summary>
        ''' <returns>Devuelve True si las filas se han eliminado con éxito, de lo contrario, False.</returns>
        Function ClearRows() As Boolean
            ' Almacenar una referencia local al objeto GridControl para mejorar la legibilidad y el rendimiento
            Dim gridControl As MyIndexerTable = IndexerView.GridControl

            gridControl.Rows.Clear()            ' Borrar configuración de filas

            ' Verificar si las filas se han eliminado correctamente
            If gridControl.RowHeaderFlowLayoutPanel.Controls.Count = 0 AndAlso
               gridControl.BodyFlowLayoutPanel.Controls.Count = 0 Then
                Return True
            End If

            Return False
        End Function

        ''' <summary>
        ''' Borra las filas y columnas de un control de tabla.
        ''' </summary>
        ''' <param name="gridControl">El control de tabla del que se eliminarán las filas y columnas.</param>
        ''' <returns>Devuelve True si las filas y columnas se han eliminado con éxito, de lo contrario, False.</returns>
        Function ClearRowsAndColumns(ByVal gridControl As MyIndexerTable) As Boolean

            If ClearRows() Then

                gridControl.Columns.Clear()     ' Borrar configuración de columnas

                ' Verificar si las columnas se han eliminado correctamente
                If gridControl.ColumnHeaderFlowLayoutPanel.Controls.Count = 0 Then
                    Return True                 ' Se han limpiado con éxito
                End If
            End If

            Return False
        End Function

        Public Function getValuePage() As DataTable
            Return _ValuePage
        End Function

        ''' <summary>
        ''' Inicializa la columna de tabla OCR predeterminada con valores predefinidos.
        ''' </summary>
        ''' <returns>Un DataView que representa la columna de tabla OCR predeterminada.</returns>
        ''' <remarks></remarks>
        Private Function CreateTableFromOCRColumns(numberColumnsOCR As Integer) As DataView

            Using TableColumnsOCROutput As New DataTable()

                ' Agregar las columnas al DataTable
                TableColumnsOCROutput.Columns.Add("Label_Field_List_Item_Default", GetType(String)).MaxLength = _pageSize
                TableColumnsOCROutput.Columns.Add("Value_Field_List_Item_Default", GetType(String)).MaxLength = _pageSize

                TableColumnsOCROutput.Rows.Add("N/A", 0)                      ' Agregar una fila para "N/A"
                For columnNumber As Integer = 0 To numberColumnsOCR           ' Agregar columnas según el número de columnas recibidas por el OCR
                    Dim valueColumn As String = (columnNumber + 1).ToString   ' .
                    Dim columnName As String = "Column_" & valueColumn
                    TableColumnsOCROutput.Rows.Add(columnName, valueColumn)
                Next

                Return New DataView(TableColumnsOCROutput)
            End Using
        End Function

        ''' <summary>
        ''' Verifica y almacena los valores únicos de las celdas de la tabla en GridControl en _valoresUnicos.
        ''' </summary>
        ''' <returns>
        ''' True si todos los valores son únicos, False si hay duplicados o si algún objeto es nulo.
        ''' </returns>
        ''' <remarks></remarks>
        Public Function SaveColumnsOCR() As Boolean?

            If Me.IndexerView.GridControl.tableItemRowOCR Then

                For Each ItemRow In IndexerView.GridControl.Rows

                    Me._uniqueColumnValues = New HashSet(Of ColumnValue)()  ' Crear un HashSet para almacenar los pares de valor y campoTabla
                    Dim foundNonZeroValue As Boolean = False

                    For i As Integer = 0 To ItemRow.Cells.Count - 1

                        Dim valueCellColumn As Object = ItemRow.Cells(i).Value
                        Dim campoTabla = ItemRow.Table.Columns(i).IdCampoTabla

                        If String.IsNullOrEmpty(CStr(valueCellColumn).ToString()) Then
                            ' El valor de valueCellColumn es una cadena vacía o es Nothing
                            Dim alertMessage As String = "Por favor, asegúrese de que los valores en las columnas OCR seleccionadas sean válidos. " &
                                                        "Seleccione la tabla y aplique OCR nuevamente."
                            MessageBox.Show(alertMessage, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Return False
                        End If

                        ' Verificar si el valor ya está en el HashSet (es decir, es un duplicado)
                        If Not valueCellColumn.Equals("0") AndAlso Me._uniqueColumnValues.Any(Function(x) x.Value.Equals(valueCellColumn)) Then
                            MessageBox.Show("Asegúrese de seleccionar un valor válido y no repetido para las columnas de OCR.", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Return False
                        Else
                            Dim valueCell As New ColumnValue()
                            valueCell.Value = valueCellColumn
                            valueCell.CampoTabla = campoTabla
                            Me._uniqueColumnValues.Add(valueCell)  ' Agregar el valor al HashSet si no es un duplicado

                            ' Verificar si el valor es diferente de "0"
                            If Not valueCellColumn.Equals("0") Then
                                foundNonZeroValue = True
                            End If

                        End If

                    Next

                    ' Verificar que al menos un valor sea diferente de "0"
                    If Not foundNonZeroValue Then
                        MessageBox.Show("Al menos un valor debe ser diferente de 'N/A' en las columnas de OCR.", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Return False
                    End If
                Next

            Else
                Return Nothing
            End If

            Return True
        End Function

        Public Sub SavePageDataOCR()

            Dim rowIndexActually As Integer = 0 ' Variable para llevar un seguimiento del índice de fila

            Try

                For Each ItemRow In IndexerView.GridControl.Rows
                    Dim NewRow = _ValuePage.NewRow()

                    For i As Integer = 0 To ItemRow.Cells.Count - 1

                        Dim cellValue As String = ItemRow.Cells(i).Value.ToString()
                        Dim maxLength As Integer = _ValuePage.Columns(i).MaxLength

                        ' Valida que cumpla con la longitud maxima
                        If maxLength > 0 AndAlso cellValue.Length > maxLength Then
                            MessageBox.Show("Advertencia: El valor en la fila " & rowIndexActually & ", columna " & i & " excede el límite de longitud y será truncado.", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Me.Focus()
                            cellValue = cellValue.Substring(0, maxLength)
                        End If

                        NewRow.Item(i) = ItemRow.Cells(i).Value
                    Next

                    If rowIndexActually >= 0 AndAlso rowIndexActually <= _ValuePage.Rows.Count - 1 Then
                        _ValuePage.Rows(rowIndexActually).ItemArray = NewRow.ItemArray    ' Actualiza el valor de esa columna
                    Else
                        _ValuePage.Rows.Add(NewRow)                                       ' Agrega un nuevo valor
                    End If

                    rowIndexActually += 1
                Next

            Catch ex As Exception
                Throw
            End Try
        End Sub

        Public Function Save() As Boolean

            If (Validar()) Then
                ' Validar totales
                Me.ControlRegistros = IndexerView.GridControl.ControlRegistros
                Me.ControlValor = IndexerView.GridControl.ControlValor

                _Value.Rows.Clear()

                Dim rowIndexActually As Integer = 0 ' Variable para llevar un seguimiento del índice de fila

                For Each ItemRow In IndexerView.GridControl.Rows
                    Dim NewRow = _Value.NewRow()

                    For i As Integer = 0 To ItemRow.Cells.Count - 1

                        Dim cellValue As String = ItemRow.Cells(i).Value.ToString()
                        Dim maxLength As Integer = _Value.Columns(i).MaxLength

                        ' Verificar si el valor de la celda excede el maxLength permitido
                        If maxLength > 0 AndAlso cellValue.Length > maxLength Then
                            MessageBox.Show("Advertencia: El valor en la fila " & (rowIndexActually + 1) & ", columna " & (i + 1) & " excede el límite de longitud.", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            ItemRow.Cells(i).Focus()
                            Return False
                        End If

                        NewRow.Item(i) = ItemRow.Cells(i).Value
                    Next

                    _Value.Rows.Add(NewRow)

                    rowIndexActually += 1 ' Incrementar el índice de fila actual
                Next

                Return True
            End If

            Return False
        End Function

#End Region

    End Class

End Namespace
