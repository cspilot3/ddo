Imports DBCore.SchemaConfig
Imports Miharu.Imaging.Indexer.Controller
Imports System.Windows.Forms

Namespace View.Indexacion

    Public Class FormDocumentSelector

#Region " Declaraciones "

        Private _Lista As New List(Of Slyg.Tools.GenericItem(Of Integer))

        Public Property SelectedValue As Slyg.Tools.SlygNullable(Of Integer)

        Public Property Controller As IController

        Private loading As Boolean = False

#End Region

#Region " Eventos "

        Private Sub FindTextBox_TextChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles FindTextBox.TextChanged
            CargarCombo(FindTextBox.Text.ToUpper())
        End Sub

        Private Sub ComboBoxForm_KeyDown(ByVal sender As System.Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
            If (e.KeyCode = Keys.Escape) Then
                Cerrar()
            ElseIf (e.KeyCode = Keys.Enter) Then
                Seleccionar()
            End If
        End Sub

        Private Sub FindListBox_DoubleClick(ByVal sender As System.Object, ByVal e As EventArgs) Handles FindListBox.DoubleClick
            Seleccionar()
        End Sub

        Private Sub FindTextBox_KeyDown(ByVal sender As System.Object, ByVal e As KeyEventArgs) Handles FindTextBox.KeyDown
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

        Private Sub FindListBox_KeyDown(ByVal sender As System.Object, ByVal e As KeyEventArgs) Handles FindListBox.KeyDown
            Select Case e.KeyCode
                Case Keys.Escape
                    Cerrar()

                Case Keys.Enter
                    Seleccionar()

            End Select
        End Sub

        Private Sub EsquemaComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles EsquemaComboBox.SelectedIndexChanged
            If (Not loading) Then CargarDocumentos()
        End Sub

        Private Sub GuardarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles GuardarButton.Click
            Seleccionar()
        End Sub

        Private Sub CerrarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CerrarButton.Click
            Cerrar()
        End Sub

        Private Sub TodosButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles TodosButton.Click
            Me.SelectedValue = Nothing

            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        End Sub

#End Region

#Region " Metodos "

        Public Sub LoadData(ByVal nController As IController)
            Me.Controller = nController

            loading = True

            EsquemaComboBox.Items.Clear()
            EsquemaComboBox.SelectedIndex = -1

            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing

            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(Me.Controller.IndexerDesktopGlobal.ConnectionStrings.Core)
                dbmCore.Connection_Open(Me.Controller.IndexerSesion.Usuario.id)

                Dim EsquemasDataTable = dbmCore.SchemaConfig.TBL_Esquema.DBGet(Me.Controller.IndexerImagingGlobal.Entidad, Me.Controller.IndexerImagingGlobal.Proyecto, Nothing, 0, New TBL_EsquemaEnumList(TBL_EsquemaEnum.Nombre_Esquema, True))

                For Each EsquemaRow In EsquemasDataTable
                    EsquemaComboBox.Items.Add(New Slyg.Tools.GenericItem(Of Short)(EsquemaRow.id_Esquema, EsquemaRow.Nombre_Esquema))
                Next

                If (EsquemaComboBox.Items.Count > 0) Then EsquemaComboBox.SelectedIndex = 0

            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                loading = False

                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            End Try

            CargarDocumentos()
        End Sub

        Private Sub CargarDocumentos()
            _Lista = New List(Of Slyg.Tools.GenericItem(Of Integer))

            If (EsquemaComboBox.SelectedIndex >= 0) Then
                Dim Esquema As Slyg.Tools.GenericItem(Of Short) = CType(EsquemaComboBox.SelectedItem, Slyg.Tools.GenericItem(Of Short))

                Dim DocumentosDataTable = Me.Controller.DocumentosCaptura(Esquema.Value)

                For Each DocumentoRow In DocumentosDataTable
                    _Lista.Add(New Slyg.Tools.GenericItem(Of Integer)(DocumentoRow.id_Documento, DocumentoRow.Nombre_Documento))
                Next
            End If

            FindTextBox.Focus()

            InicializarCombo()

            FindTextBox.SelectAll()

            CargarCombo("")
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
                Me.SelectedValue = CType(FindListBox.SelectedItem, Slyg.Tools.GenericItem(Of Integer)).Value

                Me.DialogResult = Windows.Forms.DialogResult.OK
                Me.Close()
            End If
        End Sub

        Private Sub Cerrar()
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
            Me.Close()
        End Sub

#End Region

    End Class

End Namespace