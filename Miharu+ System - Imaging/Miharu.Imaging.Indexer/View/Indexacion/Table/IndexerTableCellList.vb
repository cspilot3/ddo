Imports System.Windows.Forms
Imports Miharu.Desktop.Library.Config

Namespace View.Indexacion.Table

    Public Class IndexerTableCellList
        Inherits IndexerTableCell
        'Inherits System.Windows.Forms.UserControl

#Region " Declaraciones "

        Private _Tipo As DesktopConfig.CampoTipo = DesktopConfig.CampoTipo.Lista

#End Region

#Region " Constructores "

        Private Sub New()
            MyBase.New()

            ' Llamada necesaria para el diseñador.
            InitializeComponent()

            ' Agregue cualquier inicialización después de la llamada a InitializeComponent().

        End Sub

        Public Sub New(ByRef Parent As IndexerTableRow, nColumn As IndexerTableColumn, nTipo As DesktopConfig.CampoTipo)
            MyBase.New(Parent, nColumn)

            ' Llamada necesaria para el diseñador.
            InitializeComponent()

            ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
            Select Case nTipo
                Case DesktopConfig.CampoTipo.Lista
                    _Tipo = DesktopConfig.CampoTipo.Lista
                Case Else
                    _Tipo = DesktopConfig.CampoTipo.SiNo
            End Select

            If (_Tipo = DesktopConfig.CampoTipo.Lista) Then
                ValueDesktopComboBox.ValueMember = nColumn.ValueMember
                ValueDesktopComboBox.DisplayMember = nColumn.DisplayMember
                ValueDesktopComboBox.DataSource = nColumn.Items
                ValueDesktopComboBox.Refresh()

                ValueOld1DesktopComboBox.ValueMember = nColumn.ValueMember
                ValueOld1DesktopComboBox.DisplayMember = nColumn.DisplayMember
                ValueOld1DesktopComboBox.DataSource = New DataView(nColumn.Items.Table, "", nColumn.Items.Sort, nColumn.Items.RowStateFilter)

                ValueOld1DesktopComboBox.Refresh()

                ValueOld2DesktopComboBox.ValueMember = nColumn.ValueMember
                ValueOld2DesktopComboBox.DisplayMember = nColumn.DisplayMember
                ValueOld2DesktopComboBox.DataSource = New DataView(nColumn.Items.Table, "", nColumn.Items.Sort, nColumn.Items.RowStateFilter)
                ValueOld2DesktopComboBox.Refresh()
            Else
                ValueDesktopComboBox.DropDownStyle = ComboBoxStyle.DropDownList
                ValueDesktopComboBox.Items.Add("Si")
                ValueDesktopComboBox.Items.Add("No")
                ValueDesktopComboBox.SelectedIndex = 0

                ValueOld1DesktopComboBox.DropDownStyle = ComboBoxStyle.DropDownList
                ValueOld1DesktopComboBox.Items.Add("Si")
                ValueOld1DesktopComboBox.Items.Add("No")
                ValueOld1DesktopComboBox.SelectedIndex = 0

                ValueOld2DesktopComboBox.DropDownStyle = ComboBoxStyle.DropDownList
                ValueOld2DesktopComboBox.Items.Add("Si")
                ValueOld2DesktopComboBox.Items.Add("No")
                ValueOld2DesktopComboBox.SelectedIndex = 0
            End If
        End Sub

#End Region

#Region " Eventos "

        Private Sub IndexerTableCell_Enter(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Enter
            If (Not ValueDesktopComboBox.Enabled) Then
                Dim Index = Row.Cells.IndexOf(Me)

                If (Index + 1 < Row.Cells.Count) Then
                    NextCell()
                End If
            Else
                ValueDesktopComboBox.Focus()

                Row.Table.SetScroll()
            End If
        End Sub

        Private Sub ValueTextBox_KeyDown(ByVal sender As System.Object, ByVal e As KeyEventArgs) Handles ValueDesktopComboBox.KeyDown
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

        Private Sub ValueTextBox_Leave(sender As System.Object, e As EventArgs) Handles ValueDesktopComboBox.Leave
            If (Me.Row.Table.IsCalidad) Then ValidarCalidad()
        End Sub

#End Region

#Region " Propiedades "

        Public Overrides ReadOnly Property Type As DesktopConfig.CampoTipo
            Get
                Return _Tipo
            End Get
        End Property

        Property MaximumLength As Integer
            Get
                Return 0
            End Get
            Set(valor As Integer)

            End Set
        End Property

        Public Overrides Property Height As Integer
            Get
                Return ValueDesktopComboBox.Height
            End Get
            Set(ByVal valor As Integer)
                ValueDesktopComboBox.Height = valor
                ValueOld1DesktopComboBox.Height = valor
                ValueOld2DesktopComboBox.Height = valor
            End Set
        End Property

        Public Overrides Property Width As Integer
            Get
                Return ValueDesktopComboBox.Width
            End Get
            Set(ByVal valor As Integer)
                ValueDesktopComboBox.Width = valor
                ValueOld1DesktopComboBox.Width = valor
                ValueOld2DesktopComboBox.Width = valor
            End Set
        End Property

        Public Overrides Property ShowSecondControls As Boolean
            Get
                Return ValueOld1DesktopComboBox.Visible
            End Get
            Set(ByVal valor As Boolean)
                ValueOld1DesktopComboBox.Visible = valor
                ValueOld2DesktopComboBox.Visible = valor
                Row.UpdateSize()
            End Set
        End Property

        Public Overrides Property [ReadOnly] As Boolean
            Get
                Return Not ValueDesktopComboBox.Enabled
            End Get
            Set(ByVal valor As Boolean)
                ValueDesktopComboBox.Enabled = Not valor
            End Set
        End Property

        Public Overrides Property Value As Object
            Get
                If (ValueDesktopComboBox.SelectedIndex >= 0) Then
                    If (_Tipo = DesktopConfig.CampoTipo.SiNo) Then
                        Return (ValueDesktopComboBox.SelectedIndex = 0)
                    Else
                        Return ValueDesktopComboBox.SelectedValue
                    End If
                Else
                    Return ""
                End If
            End Get
            Set(ByVal valor As Object)
                If (_Tipo = DesktopConfig.CampoTipo.SiNo) Then
                    If (CBool(valor)) Then
                        ValueDesktopComboBox.SelectedIndex = 0
                    Else
                        ValueDesktopComboBox.SelectedIndex = 1
                    End If
                Else
                    ValueDesktopComboBox.SelectedValue = valor
                End If
            End Set
        End Property

        Public Overrides Property ValueOld1 As Object
            Get
                If (ValueOld1DesktopComboBox.SelectedIndex >= 0) Then
                    Return ValueOld1DesktopComboBox.SelectedValue
                Else
                    Return ""
                End If
            End Get
            Set(ByVal valor As Object)
                ValueOld1DesktopComboBox.SelectedValue = valor
            End Set
        End Property

        Public Overrides Property ValueOld2 As Object
            Get
                If (ValueOld2DesktopComboBox.SelectedIndex >= 0) Then
                    Return ValueOld2DesktopComboBox.SelectedValue
                Else
                    Return ""
                End If
            End Get
            Set(ByVal valor As Object)
                ValueOld2DesktopComboBox.SelectedValue = valor
            End Set
        End Property

        Public Overrides ReadOnly Property IsEmpty As Boolean
            Get
                Return Me.ValueDesktopComboBox.SelectedIndex = -1
            End Get
        End Property

        Public Property Mascara As String
            Get
                Return ""
            End Get
            Set(valor As String)

            End Set
        End Property

#End Region

#Region " Funciones "

        Protected Overrides Sub ValidarCalidad()
            If (Not Me.ReadOnly AndAlso Me.RequiereAutorizacion) Then
                Me.ValueDesktopComboBox.ForeColor = Drawing.Color.Red
            Else
                Me.ValueDesktopComboBox.ForeColor = Drawing.Color.Black
            End If
        End Sub

        Public Overrides Function Validar() As Boolean
            If ValueDesktopComboBox.Text.Trim() = "" And Me.Column.Es_Obligatorio Then
                MessageBox.Show("El campo " & Me.Column.HeaderText & " es obligatorio", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                ValueDesktopComboBox.Focus()

                Return False
            Else
                Return True
            End If
        End Function

#End Region

    End Class

End Namespace