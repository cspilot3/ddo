Imports Miharu.Desktop.Library.Config

Namespace View.Indexacion.Table

    Public MustInherit Class IndexerTableCell

#Region " Constructores "

        Protected Sub New()
            ' Llamada necesaria para el diseñador.
            InitializeComponent()

            ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        End Sub

        Public Sub New(ByRef Parent As IndexerTableRow, nColumn As IndexerTableColumn)
            Me.New()

            _Row = Parent
            _Column = nColumn
        End Sub

#End Region

#Region " Propiedades "

        Public MustOverride ReadOnly Property Type As DesktopConfig.CampoTipo

        Private _Row As IndexerTableRow
        Public ReadOnly Property Row As IndexerTableRow
            Get
                Return _Row
            End Get
        End Property

        Private _Column As IndexerTableColumn
        Public ReadOnly Property Column As IndexerTableColumn
            Get
                Return _Column
            End Get
        End Property

        Public MustOverride Shadows Property Height As Integer

        Public MustOverride Shadows Property Width As Integer

        Public ReadOnly Property BaseHeight As Integer
            Get
                Return MyBase.Height
            End Get
        End Property

        Public ReadOnly Property BaseWidth As Integer
            Get
                Return MyBase.Width
            End Get
        End Property

        Public MustOverride Property ShowSecondControls As Boolean

        Public MustOverride Property [ReadOnly] As Boolean

        Public MustOverride Property Value As Object

        Public MustOverride Property ValueOld1 As Object

        Public MustOverride Property ValueOld2 As Object

        Public Property ValueOld3 As Object

        Public ReadOnly Property RequiereAutorizacion As Boolean
            Get
                Return (Me.Value.ToString() <> Me.ValueOld1.ToString() _
                    AndAlso Me.Value.ToString() <> Me.ValueOld2.ToString() _
                    AndAlso Me.Value.ToString() <> Me.ValueOld3.ToString())
            End Get
        End Property

        Public MustOverride ReadOnly Property IsEmpty As Boolean

#End Region

#Region " Metodos "

        Protected Sub NextCell()
            Dim Index = Row.Cells.IndexOf(Me)

            If (Index + 1 < Row.Cells.Count) Then
                Row.Cells(Index + 1).Focus()
            Else
                Index = Row.Table.Rows.IndexOf(Row)

                If (Index + 1 < Row.Table.Rows.Count) Then
                    Row.Table.Rows(Index + 1).Cells(0).Focus()
                Else
                    Row.Table.AddRow()
                End If
            End If
        End Sub

        Protected Sub NextControl()
            Row.Table.NextControl()
        End Sub

        ''' <summary>
        ''' Elimina la fila actual de la tabla si la eliminación de fila está permitida.
        ''' Si se elimina la fila, se enfoca en la celda de la fila anterior si existe, 
        ''' de lo contrario, se enfoca en la primera celda de la primera fila o agrega una nueva fila si la tabla está vacía.
        ''' </summary>
        Protected Sub DeleteRow()
            If (Row.Table.AllowDeleteRow) Then
                Dim focusRowIndex = Row.Table.Rows.IndexOf(Row)                 ' Obtiene el índice de la fila actual en la tabla
                Row.Table.Rows.RemoveIndex(focusRowIndex)                       ' Elimina la fila actual de la tabla

                Dim Index = focusRowIndex - 1                                   ' Calcula el índice de la fila a enfocar después de la eliminación

                If Index >= 0 Then                                              ' Verifica si hay una fila anterior a la que enfocar
                    If (Index < Row.Table.Rows.Count) Then
                        Row.Table.Rows(focusRowIndex - 1).Cells(0).Focus()      ' Enfoca en la primera celda de la fila anterior
                    End If
                Else
                    If (Row.Table.Rows.Count > 0) Then                          ' Si no hay fila anterior, verifica si la tabla aún tiene filas
                        Row.Table.Rows(0).Cells(0).Focus()
                    Else
                        Row.Table.AddRow()
                    End If
                End If

            End If
        End Sub

#End Region

#Region " Funciones "

        Protected MustOverride Sub ValidarCalidad()

        Public MustOverride Function Validar() As Boolean

#End Region

    End Class

End Namespace