Imports System.Windows.Forms
Imports Slyg.Tools.CSV
Imports System.IO

Namespace Help

    Public Class ModuloAyuda

#Region " Declaraciones "

        Dim Lista As New List(Of Slyg.Tools.GenericItem(Of String))
        Dim SourcePath As String

#End Region

#Region " Constructores "

        Public Sub New()
            ' Llamada necesaria para el diseñador.
            InitializeComponent()
        End Sub

        Public Sub New(ByVal nSourcePath As String)
            Me.New()
            SourcePath = nSourcePath
            InicializarCombo()
        End Sub

#End Region

#Region " Eventos "

        Private Sub FindListBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles FindListBox.SelectedIndexChanged
            If (FindListBox.SelectedIndex >= 0) Then
                Dim Item As Slyg.Tools.GenericItem(Of String) = CType(FindListBox.SelectedItem, Slyg.Tools.GenericItem(Of String))

                If (File.Exists(SourcePath & Item.Id)) Then
                    pbImagen.ImageLocation = SourcePath & Item.Id
                    pbImagen.Show()
                End If

                txtDescripcion.Text = Item.Value
            End If
        End Sub

        Private Sub FindTextBox_TextChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles FindTextBox.TextChanged
            CargarCombo(FindTextBox.Text.ToUpper())
        End Sub

#End Region

#Region " Metodos "

        Public Sub InicializarCombo()
            If (CargarArchivo()) Then
                Me.FindListBox.ValueMember = "Value"
                Me.FindListBox.DisplayMember = "Display"
                CargarCombo("")
            Else
                Me.FindListBox.Enabled = False
            End If
        End Sub

        Private Function CargarArchivo() As Boolean
            Try
                'Se obtiene el separador               
                Dim objCSV As New CSVData()

                objCSV.Separator = CChar(vbTab)
                objCSV.LoadCSV(SourcePath & "Index.txt", True)

                Lista.Clear()

                For Each Fila As DataRow In objCSV.DataTable.Rows
                    Lista.Add(New Slyg.Tools.GenericItem(Of String)(Fila("Observacion").ToString(), Fila("Documento").ToString().ToUpper(), Fila("Imagen").ToString()))
                Next

                Return True
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Ayuda", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End Try
        End Function

        Private Sub CargarCombo(ByVal nFiltro As String)
            SyncLock FindListBox.Items
                FindListBox.Items.Clear()

                For Each Item In Lista
                    If (nFiltro = "" OrElse Item.Display.Contains(nFiltro)) Then
                        FindListBox.Items.Add(Item)
                    End If
                Next
            End SyncLock
        End Sub

#End Region

    End Class

End Namespace