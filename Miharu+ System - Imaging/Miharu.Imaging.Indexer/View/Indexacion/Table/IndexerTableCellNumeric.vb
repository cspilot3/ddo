Imports System.Windows.Forms
Imports Miharu.Desktop.Library.Config

Namespace View.Indexacion.Table

    Public Class IndexerTableCellNumeric
        Inherits IndexerTableCell

#Region " Constructores "

        Private Sub New()
            MyBase.New()

            ' Llamada necesaria para el diseñador.
            InitializeComponent()

            ' Agregue cualquier inicialización después de la llamada a InitializeComponent().

        End Sub

        Public Sub New(ByRef nParent As IndexerTableRow, nColumn As IndexerTableColumn)
            MyBase.New(nParent, nColumn)

            ' Llamada necesaria para el diseñador.
            InitializeComponent()

            ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        End Sub

#End Region

#Region " Eventos "

        Private Sub IndexerTableCell_Enter(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Enter
            If (ValueDesktopTextBox.ReadOnly) Then
                Dim index = Row.Cells.IndexOf(Me)

                If (index + 1 < Row.Cells.Count) Then
                    NextCell()
                End If
            Else
                ValueDesktopTextBox.Focus()

                Row.Table.SetScroll()
            End If
        End Sub

        Private Sub ValueTextBox_KeyDown(ByVal sender As System.Object, ByVal e As KeyEventArgs) Handles ValueDesktopTextBox.KeyDown
            Select Case e.KeyCode
                Case Keys.Enter
                    If (e.Control) Then
                        NextControl()
                    Else
                        NextCell()
                    End If

                Case Keys.F8
                    DeleteRow()

                Case Keys.F10
                    Row.Table.AddRow()

            End Select
        End Sub

        Private Sub ValueTextBox_Leave(sender As System.Object, e As EventArgs) Handles ValueDesktopTextBox.Leave
            If (Me.Row.Table.IsCalidad) Then ValidarCalidad()
        End Sub

#End Region

#Region " Propiedades "

        Public Overrides ReadOnly Property Type As DesktopConfig.CampoTipo
            Get
                Return DesktopConfig.CampoTipo.Numerico
            End Get
        End Property

        Property MaximumLength As Integer
            Get
                Return Me.ValueDesktopTextBox.MaxLength
            End Get
            Set(nValue As Integer)
                Me.ValueDesktopTextBox.MaxLength = nValue
                Me.ValueOld1DesktopTextBox.MaxLength = nValue
                Me.ValueOld2DesktopTextBox.MaxLength = nValue
            End Set
        End Property

        Property UsaDecimales As Boolean
            Get
                Return Me.ValueDesktopTextBox.UsaDecimales
            End Get
            Set(nValue As Boolean)
                Me.ValueDesktopTextBox.UsaDecimales = nValue
                Me.ValueOld1DesktopTextBox.UsaDecimales = nValue
                Me.ValueOld2DesktopTextBox.UsaDecimales = nValue
            End Set
        End Property

        Property CantidadDecimales As Short
            Get
                Return Me.ValueDesktopTextBox.CantidadDecimales
            End Get
            Set(nValue As Short)
                Me.ValueDesktopTextBox.CantidadDecimales = nValue
                Me.ValueOld1DesktopTextBox.CantidadDecimales = nValue
                Me.ValueOld2DesktopTextBox.CantidadDecimales = nValue
            End Set
        End Property

        Public Overrides Property Height As Integer
            Get
                Return ValueDesktopTextBox.Height
            End Get
            Set(ByVal value As Integer)
                ValueDesktopTextBox.Height = value
                ValueOld1DesktopTextBox.Height = value
                ValueOld2DesktopTextBox.Height = value
            End Set
        End Property

        Public Overrides Property Width As Integer
            Get
                Return ValueDesktopTextBox.Width
            End Get
            Set(ByVal value As Integer)
                ValueDesktopTextBox.Width = value
                ValueOld1DesktopTextBox.Width = value
                ValueOld2DesktopTextBox.Width = value
            End Set
        End Property

        Public Overrides Property ShowSecondControls As Boolean
            Get
                Return ValueOld1DesktopTextBox.Visible
            End Get
            Set(ByVal value As Boolean)
                ValueOld1DesktopTextBox.Visible = value
                ValueOld2DesktopTextBox.Visible = value
                Row.UpdateSize()
            End Set
        End Property

        Public Overrides Property [ReadOnly] As Boolean
            Get
                Return ValueDesktopTextBox.ReadOnly
            End Get
            Set(ByVal value As Boolean)
                ValueDesktopTextBox.ReadOnly = value
            End Set
        End Property

        Public Overrides Property Value As Object
            Get
                Return Me.ValueDesktopTextBox.getString()
            End Get
            Set(nValue As Object)
                Me.ValueDesktopTextBox.Text = nValue.ToString()
            End Set
        End Property

        Public Overrides Property ValueOld1 As Object
            Get
                Return Me.ValueOld1DesktopTextBox.Text
            End Get
            Set(nValue As Object)
                Me.ValueOld1DesktopTextBox.Text = nValue.ToString()
            End Set
        End Property

        Public Overrides Property ValueOld2 As Object
            Get
                Return Me.ValueOld2DesktopTextBox.Text
            End Get
            Set(nValue As Object)
                Me.ValueOld2DesktopTextBox.Text = nValue.ToString()
            End Set
        End Property

        Public Overrides ReadOnly Property IsEmpty As Boolean
            Get
                Return Me.ValueDesktopTextBox.IsEmpty
            End Get
        End Property

#End Region

#Region " Funciones "

        Protected Overrides Sub ValidarCalidad()
            If (Not Me.ReadOnly AndAlso Me.RequiereAutorizacion) Then
                Me.ValueDesktopTextBox.ForeColor = Drawing.Color.Red
            Else
                Me.ValueDesktopTextBox.ForeColor = Drawing.Color.Black
            End If
        End Sub

        Public Overrides Function Validar() As Boolean
            ' ReSharper disable once CompareOfFloatsByEqualityOperator
            If (Me.Visible AndAlso Me.Column.Es_Obligatorio AndAlso CDbl(ValueDesktopTextBox.GetValue()) = 0) Then
                MessageBox.Show("El campo " & Me.Column.HeaderText & " es obligatorio", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Warning)

                ValueDesktopTextBox.SelectAll()
                ValueDesktopTextBox.Focus()
                Return False
            Else
                Return ValueDesktopTextBox.Validar()
            End If
        End Function

#End Region

    End Class

End Namespace