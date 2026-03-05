Imports System.Windows.Forms
Imports DMB = Miharu.Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl

Namespace Imaging.Forms.Parametrización
    Public Class Tipologia_CodigoTx

        Private _plugin As BanagrarioImagingPlugin

        Public Sub New(nBanagrarioImagingPlugin As BanagrarioImagingPlugin)

            InitializeComponent()
            _plugin = nBanagrarioImagingPlugin

        End Sub

        Private Sub Tipologia_CodigoTx_Load(sender As System.Object, e As EventArgs) Handles MyBase.Load
            CargarTipologiasTx()

        End Sub

        Private Sub CargarTipologiasTx()

            Dim dbmAgrario As DBAgrario.DBAgrarioDataBaseManager = Nothing

            Try
                dbmAgrario = New DBAgrario.DBAgrarioDataBaseManager(_plugin.BancoAgrarioConnectionString)
                dbmAgrario.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                Dim TipologiaTxDataTable = dbmAgrario.SchemaConfig.CTA_Tipologia_Codigo_Tx_Parametrizacion.DBGet()
                If TipologiaTxDataTable.Rows.Count > 0 Then
                    TipologiaTXDataGridView.AutoGenerateColumns = False
                    TipologiaTXDataGridView.DataSource = TipologiaTxDataTable
                    TipologiaTXDataGridView.Refresh()
                End If

            Catch ex As Exception
                DMB.DesktopMessageShow("Cargar Tipologias TX", ex)
            Finally
                If dbmAgrario IsNot Nothing Then dbmAgrario.Connection_Close()
            End Try

        End Sub

        Private Sub AñadirButton_Click(sender As System.Object, e As EventArgs) Handles AñadirButton.Click
            Dim fNuevaTipologiaTx As New FormNuevoTipologiaTx(_plugin)
            fNuevaTipologiaTx.IdTipologia = 0
            If fNuevaTipologiaTx.ShowDialog() = DialogResult.OK Then
                CargarTipologiasTx()
            End If
        End Sub

        Private Sub TipologiaTXDataGridView_DoubleClick(sender As System.Object, e As EventArgs) Handles TipologiaTXDataGridView.DoubleClick
            Dim fNuevaTipologiaTx As New FormNuevoTipologiaTx(_plugin)
            fNuevaTipologiaTx.IdTipologia = CShort(TipologiaTXDataGridView.SelectedRows(0).Cells("fk_Tipologia").Value)
            If fNuevaTipologiaTx.ShowDialog() = DialogResult.OK Then
                CargarTipologiasTx()
            End If
        End Sub
    End Class
End Namespace