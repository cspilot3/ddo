Imports System.Windows.Forms
Imports DBAgrario

Namespace Imaging.Forms.Parametrización

    Public Class FormNovedades

#Region " Declaraciones "

        Private _plugin As BanagrarioImagingPlugin

#End Region

#Region "Constructor"

        Public Sub New(ByVal nBanagrarioImaginPlugin As BanagrarioImagingPlugin)
            InitializeComponent()
            _Plugin = nBanagrarioImaginPlugin
        End Sub

#End Region

#Region " Eventos "

        Private Sub NovedadDataGridView_CellMouseDoubleClick(ByVal sender As Object, ByVal e As DataGridViewCellMouseEventArgs) Handles NovedadDataGridView.CellMouseDoubleClick
            Dim fNovedad As New FormNuevaNovedad(_plugin)
            fNovedad._idNovedad = CInt(NovedadDataGridView.Rows(e.RowIndex).Cells(2).Value.ToString())
            fNovedad.NovedadTextBox.Text = NovedadDataGridView.Rows(e.RowIndex).Cells(0).Value.ToString()
            fNovedad.CheckBoxActivo.Checked = CBool(NovedadDataGridView.Rows(e.RowIndex).Cells(1).Value)
            If fNovedad.ShowDialog() = DialogResult.OK Then
                BuscarNovedades()
            End If
        End Sub

        Private Sub FormNovedades_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            BuscarNovedades()
        End Sub

        Private Sub NuevoDocumentoButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles NuevoDocumentoButton.Click
            Dim fNovedad As New FormNuevaNovedad(_plugin)
            If fNovedad.ShowDialog() = DialogResult.OK Then
                BuscarNovedades()
            End If
        End Sub

        Private Sub CerrarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CerrarButton.Click
            Me.Close()
        End Sub

#End Region

#Region "Métodos"

        Private Sub BuscarNovedades()
            Dim dbmAgrario As DBAgrarioDataBaseManager = Nothing

            Try
                dbmAgrario = New DBAgrarioDataBaseManager(Me._Plugin.BancoAgrarioConnectionString)

                dbmAgrario.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                Dim novedadDataTable As DataTable = dbmAgrario.SchemaProcess.PA_Get_Novedades.DBExecute()
                NovedadDataGridView.DataSource = novedadDataTable

            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                If (dbmAgrario IsNot Nothing) Then dbmAgrario.Connection_Close()
            End Try
        End Sub

#End Region

    End Class

End Namespace