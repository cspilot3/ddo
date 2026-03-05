Imports System.Windows.Forms

Namespace Imaging.Beps.Rechazo_Bizagi

    Public Class FormCausalRechazo

#Region " Declaraciones "

        Private _plugin As Plugin

#End Region

#Region "Constructor"

        Public Sub New(ByVal nPlugin As Plugin)
            InitializeComponent()
            _plugin = nPlugin
        End Sub

#End Region

#Region " Eventos "

        Private Sub FormCausalRechazo_Load(sender As System.Object, e As EventArgs) Handles MyBase.Load
            BuscarCausalesRechazo()
        End Sub

        Private Sub CausalRechazoDataGridView_CellDoubleClick(sender As System.Object, e As DataGridViewCellEventArgs) Handles CausalRechazoDataGridView.CellDoubleClick
            Dim fCausalR As New FormNuevaCausalRechazo(_plugin)
            fCausalR.IdCausalRechazo = CInt(CausalRechazoDataGridView.Rows(e.RowIndex).Cells(0).Value.ToString())
            fCausalR.CausalRechazoTextBox.Text = CausalRechazoDataGridView.Rows(e.RowIndex).Cells(1).Value.ToString()
            fCausalR.CheckBoxActivo.Checked = CBool(CausalRechazoDataGridView.Rows(e.RowIndex).Cells(2).Value)
            fCausalR.fk_Tipo_Novedad = CInt(CausalRechazoDataGridView.Rows(e.RowIndex).Cells(5).Value.ToString())
            If fCausalR.ShowDialog() = DialogResult.OK Then
                BuscarCausalesRechazo()
            End If
        End Sub

        Private Sub NuevoCausalRechazoButton_Click(sender As System.Object, e As EventArgs) Handles NuevoCausalRechazoButton.Click
            Dim fCausalR As New FormNuevaCausalRechazo(_plugin)
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
            Dim dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = Nothing
            Dim causalRechazoDt As DBIntegration.SchemaColpensionesBEPS.TBL_Rechazos_BizagiDataTable
            Try
                dbmIntegration = New DBIntegration.DBIntegrationDataBaseManager(Me._plugin.ColpensionesConnectionString)
                dbmIntegration.Connection_Open(_plugin.Manager.Sesion.Usuario.id)
                causalRechazoDt = dbmIntegration.SchemaColpensionesBEPS.TBL_Rechazos_Bizagi.DBGet(Nothing)

            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()

            End Try

            CausalRechazoDataGridView.DataSource = causalRechazoDt
            ArmarCausalRechazoDataGridView()

        End Sub

        Private Sub ArmarCausalRechazoDataGridView()

            CausalRechazoDataGridView.Columns("id_Rechazo").HeaderText = "Id. Rechazo"
            CausalRechazoDataGridView.Columns("Descripcion_Rechazo").HeaderText = "Descripción"
            CausalRechazoDataGridView.Columns("Descripcion_Rechazo").Width = 300

        End Sub


#End Region

    End Class
End Namespace





