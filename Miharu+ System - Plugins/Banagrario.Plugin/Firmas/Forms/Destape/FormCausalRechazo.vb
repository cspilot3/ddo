Imports System.Windows.Forms
Imports DBAgrario

Namespace Firmas.Forms.Destape

    Public Class FormCausalRechazo

#Region " Declaraciones "

        Private _plugin As FirmasImagingPlugin

#End Region

#Region "Constructor"

        Public Sub New(ByVal nFirmasImagingPlugin As FirmasImagingPlugin)
            InitializeComponent()
            _Plugin = nFirmasImagingPlugin
        End Sub

#End Region

#Region " Eventos "

        Private Sub FormCausalRechazo_Load(sender As System.Object, e As EventArgs) Handles MyBase.Load
            BuscarCausalesRechazo()
        End Sub

        Private Sub CausalRechazoDataGridView_CellDoubleClick(sender As System.Object, e As DataGridViewCellEventArgs) Handles CausalRechazoDataGridView.CellDoubleClick
            Dim fCausalR As New FormNuevaCausalRechazo(_Plugin)
            fCausalR.IdCausalRechazo = CInt(CausalRechazoDataGridView.Rows(e.RowIndex).Cells(0).Value.ToString())
            fCausalR.CausalRechazoTextBox.Text = CausalRechazoDataGridView.Rows(e.RowIndex).Cells(1).Value.ToString()
            fCausalR.CheckBoxActivo.Checked = CBool(CausalRechazoDataGridView.Rows(e.RowIndex).Cells(2).Value)
            If fCausalR.ShowDialog() = DialogResult.OK Then
                BuscarCausalesRechazo()
            End If
        End Sub

        Private Sub NuevoCausalRechazoButton_Click(sender As System.Object, e As EventArgs) Handles NuevoCausalRechazoButton.Click
            Dim fCausalR As New FormNuevaCausalRechazo(_Plugin)
            fCausalR.IdCausalRechazo = 32767
            If fCausalR.ShowDialog() = DialogResult.OK Then
                BuscarCausalesRechazo()
            End If
        End Sub

        Private Sub CerrarButton_Click(sender As System.Object, e As EventArgs) Handles CerrarButton.Click
            Me.Close()
        End Sub

#End Region

#Region "Métodos"

        Private Sub BuscarCausalesRechazo()
            Dim dbmAgrario As DBAgrarioDataBaseManager = Nothing
            Dim causalRechazoDt As DataTable
            Try
                dbmAgrario = New DBAgrarioDataBaseManager(Me._Plugin.BancoAgrarioConnectionString)
                dbmAgrario.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
                causalRechazoDt = dbmAgrario.SchemaFirmas.PA_Get_CausalRechazo.DBExecute()

            Catch ex As Exception
                Throw New Exception(ex.Message)

            Finally
                If (dbmAgrario IsNot Nothing) Then dbmAgrario.Connection_Close()

            End Try

            CausalRechazoDataGridView.DataSource = causalRechazoDt
            ArmarCausalRechazoDataGridView()

        End Sub

        Private Sub ArmarCausalRechazoDataGridView()

            CausalRechazoDataGridView.Columns("id_Rechazo").HeaderText = "Id. Rechazo"
            CausalRechazoDataGridView.Columns("Descripcion").HeaderText = "Descripción"
            CausalRechazoDataGridView.Columns("Descripcion").Width = 300

        End Sub


#End Region

    End Class
End Namespace





