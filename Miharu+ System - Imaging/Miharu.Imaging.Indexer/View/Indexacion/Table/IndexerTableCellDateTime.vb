Imports System.Windows.Forms
Imports Miharu.Desktop.Library.Config

Namespace View.Indexacion.Table

    Public Class IndexerTableCellDateTime
        Inherits IndexerTableCell

#Region " Constructores "

        Private Sub New()
            MyBase.New()

            ' Llamada necesaria para el diseñador.
            InitializeComponent()

            ' Agregue cualquier inicialización después de la llamada a InitializeComponent().

        End Sub

        Public Sub New(ByRef Parent As IndexerTableRow, nColumn As IndexerTableColumn)
            MyBase.New(Parent, nColumn)

            ' Llamada necesaria para el diseñador.
            InitializeComponent()

            ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        End Sub

#End Region

#Region " Eventos "

        Private Sub IndexerTableCell_Enter(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Enter
            If (Me.ValueDesktopTextBox.ReadOnly) Then
                Dim Index = Me.Row.Cells.IndexOf(Me)

                If (Index + 1 < Me.Row.Cells.Count) Then
                    NextCell()
                End If
            Else
                Me.ValueDesktopTextBox.Focus()

                Me.Row.Table.SetScroll()
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
                    Me.Row.Table.AddRow()

            End Select
        End Sub

        Private Sub ValueTextBox_Leave(sender As System.Object, e As EventArgs) Handles ValueDesktopTextBox.Leave
            If (Me.Row.Table.IsCalidad) Then ValidarCalidad()
        End Sub

#End Region

#Region " Propiedades "

        Public Overrides ReadOnly Property Type As DesktopConfig.CampoTipo
            Get
                Return DesktopConfig.CampoTipo.Fecha
            End Get
        End Property

        Public Overrides Property Height As Integer
            Get
                Return ValueDesktopTextBox.Height
            End Get
            Set(ByVal value As Integer)
                ValueDesktopTextBox.Height = value
                ValueOld1DesktopTextBox.Height = value
                Valueold2DesktopTextBox.Height = value
            End Set
        End Property

        Public Overrides Property Width As Integer
            Get
                Return ValueDesktopTextBox.Width
            End Get
            Set(ByVal value As Integer)
                ValueDesktopTextBox.Width = value
                ValueOld1DesktopTextBox.Width = value
                Valueold2DesktopTextBox.Width = value
            End Set
        End Property

        Public Overrides Property ShowSecondControls As Boolean
            Get
                Return ValueOld1DesktopTextBox.Visible
            End Get
            Set(ByVal value As Boolean)
                ValueOld1DesktopTextBox.Visible = value
                Valueold2DesktopTextBox.Visible = value
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
                Return Me.ValueDesktopTextBox.Text
            End Get
            Set(value As Object)
                Me.ValueDesktopTextBox.Text = value.ToString()
            End Set
        End Property

        Public Overrides Property ValueOld1 As Object
            Get
                Return Me.ValueOld1DesktopTextBox.Text
            End Get
            Set(value As Object)
                Me.ValueOld1DesktopTextBox.Text = value.ToString()
            End Set
        End Property

        Public Overrides Property ValueOld2 As Object
            Get
                Return Me.Valueold2DesktopTextBox.Text
            End Get
            Set(value As Object)
                Me.Valueold2DesktopTextBox.Text = value.ToString()
            End Set
        End Property

        Public Overrides ReadOnly Property IsEmpty As Boolean
            Get
                Return Me.ValueDesktopTextBox.IsEmpty
            End Get
        End Property

        Public Property Mascara As String
            Get
                Return Me.ValueDesktopTextBox.Mask
            End Get
            Set(value As String)
                Me.ValueDesktopTextBox.Mask = value
            End Set
        End Property

        Public Property FormatoFecha As String
            Get
                Return Me.ValueDesktopTextBox.DateFormat
            End Get
            Set(value As String)
                Me.ValueDesktopTextBox.DateFormat = value
            End Set
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
            If (Me.Visible AndAlso Me.Column.Es_Obligatorio AndAlso ValueDesktopTextBox.Text = "") Then
                MessageBox.Show("El campo " & Me.Column.HeaderText & " es obligatorio", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Warning)

                ValueDesktopTextBox.SelectAll()
                ValueDesktopTextBox.Focus()
                Return False
            Else
                ValueDesktopTextBox.NombreCampo = Me.Column.HeaderText
                Return ValueDesktopTextBox.Validar()
            End If
        End Function

#End Region

    End Class

End Namespace