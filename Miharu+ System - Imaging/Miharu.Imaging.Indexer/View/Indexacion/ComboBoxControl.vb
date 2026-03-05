Imports System.Windows.Forms

Namespace View.Indexacion

    Public Class ComboBoxControl

        Private _ComboBox As ComboBox
        Private _Lista As New List(Of Slyg.Tools.GenericItem(Of String))

        Public Event SeleccionarSiguienteControl()

        Public Sub New()
            ' Llamada necesaria para el diseñador.
            InitializeComponent()

            ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        End Sub

#Region " Eventos "

        Private Sub CloseButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles Table_CloseButton.Click
            Cerrar()
        End Sub

        Private Sub FindTextBox_TextChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles FindTextBox.TextChanged
            CargarCombo(FindTextBox.Text.ToUpper())
        End Sub

        Private Sub ComboBoxForm_KeyUp(ByVal sender As System.Object, ByVal e As KeyEventArgs) Handles MyBase.KeyUp
            If (e.KeyCode = Keys.Escape) Then
                Cerrar()
            ElseIf (e.KeyCode = Keys.Enter) Then
                Seleccionar()
            End If
        End Sub

        Private Sub FindListBox_DoubleClick(ByVal sender As System.Object, ByVal e As EventArgs) Handles FindListBox.DoubleClick
            Seleccionar()
        End Sub

        Private Sub FindTextBox_KeyUp(ByVal sender As System.Object, ByVal e As KeyEventArgs) Handles FindTextBox.KeyUp
            Select Case e.KeyCode
                Case Keys.Escape
                    Cerrar()

                Case Keys.Enter
                    Seleccionar()

                Case Keys.Up
                    If (FindListBox.SelectedIndex > 0) Then FindListBox.SelectedIndex -= 1
                    FindTextBox.SelectionStart = FindTextBox.Text.Length

                Case Keys.Down
                    If (FindListBox.SelectedIndex < FindListBox.Items.Count - 1) Then FindListBox.SelectedIndex += 1
                    FindTextBox.SelectionStart = FindTextBox.Text.Length

            End Select
        End Sub

        Private Sub FindListBox_KeyUp(ByVal sender As System.Object, ByVal e As KeyEventArgs) Handles FindListBox.KeyUp
            Select Case e.KeyCode
                Case Keys.Escape
                    Cerrar()

                Case Keys.Enter
                    Seleccionar()

            End Select
        End Sub

#End Region

        Public Sub Mostrar(ByRef nComboBox As ComboBox, ByVal Lista As List(Of Slyg.Tools.GenericItem(Of String)))
            Me.Visible = True
            FindTextBox.Focus()
            _Lista = Lista
            _ComboBox = nComboBox

            InicializarCombo()

            FindTextBox.Text = nComboBox.Text
            FindTextBox.SelectAll()

            CargarCombo("")

            If (_ComboBox.SelectedValue IsNot Nothing) Then
                For i As Integer = 0 To FindListBox.Items.Count
                    If (CType(FindListBox.Items(i), Slyg.Tools.GenericItem(Of String)).Value = _ComboBox.SelectedValue.ToString()) Then
                        FindListBox.SelectedIndex = i
                        Exit For
                    End If
                Next
            End If
        End Sub

        Public Sub InicializarCombo()
            Me.FindListBox.ValueMember = "Value"
            Me.FindListBox.DisplayMember = "Display"
        End Sub

        Private Sub CargarCombo(ByVal nFiltro As String)
            SyncLock FindListBox.Items
                FindListBox.Items.Clear()

                For Each Item In _Lista
                    If (nFiltro = "" OrElse Item.Display.ToUpper().Contains(nFiltro)) Then
                        FindListBox.Items.Add(Item)
                    End If
                Next

                If (FindListBox.Items.Count > 0) Then FindListBox.SelectedIndex = 0
            End SyncLock
        End Sub

        Private Sub Seleccionar()
            If (FindListBox.SelectedIndex >= 0) Then
                _ComboBox.SelectedValue = CType(FindListBox.SelectedItem, Slyg.Tools.GenericItem(Of String)).Value
                Cerrar()
            End If
        End Sub

        Private Sub Cerrar()
            RaiseEvent SeleccionarSiguienteControl()
            Me.Visible = False
        End Sub

    End Class

End Namespace