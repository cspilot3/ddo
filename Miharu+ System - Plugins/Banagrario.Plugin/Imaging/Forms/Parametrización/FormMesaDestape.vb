Imports System.Windows.Forms
Imports DBAgrario
Imports DBSecurity
Imports Miharu.Desktop.Library.Config

Namespace Imaging.Forms.Parametrización

    Public Class FormMesaDestape

#Region " Declaraciones "

        Private _plugin As BanagrarioImagingPlugin
        Friend Shared DesktopGlobal As New DesktopGlobal()
#End Region


#Region "Constructor"
        
        Public Sub New(ByVal nBanagrarioImaginPlugin As BanagrarioImagingPlugin)
            InitializeComponent()
            _Plugin = nBanagrarioImaginPlugin
        End Sub

#End Region

#Region " Eventos "

        Private Sub FormMesaDestape_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            CargarSedes()
        End Sub

        Private Sub AgregarMesaDestapeButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AgregarMesaDestapeButton.Click
            Dim agregarMesaForm As New FormNuevaMesaDestape(_plugin)
            If agregarMesaForm.ShowDialog() = DialogResult.OK Then
                BuscarMesasDestape()
            End If
        End Sub

        Private Sub BuscarButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BuscarButton.Click
            BuscarMesasDestape()
        End Sub

        Private Sub SedeDesktopComboBox_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles SedeDesktopComboBox.SelectedIndexChanged
            CargarCentroProcesamiento()
        End Sub
#End Region


        Private Sub BuscarMesasDestape()
            Dim dbmAgrario As DBAgrarioDataBaseManager = Nothing

            Try
                dbmAgrario = New DBAgrarioDataBaseManager(Me._Plugin.BancoAgrarioConnectionString)

                dbmAgrario.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                Dim mesaDestapeDataTable As DataTable = dbmAgrario.SchemaConfig.PA_Get_MesasDestape.DBExecute(CShort(SedeDesktopComboBox.SelectedValue), _plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, CShort(CentroProcesamientoDesktopComboBox.SelectedValue))
                MesaDestapeDataGridView.DataSource = mesaDestapeDataTable

            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                If (dbmAgrario IsNot Nothing) Then dbmAgrario.Connection_Close()
            End Try
        End Sub

        Private Sub CerrarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CerrarButton.Click
            Me.Close()
        End Sub

        Private Sub CargarSedes()
            Dim dbmSecurity As DBSecurityDataBaseManager = Nothing


            Try
                dbmSecurity = New DBSecurityDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Security)
                dbmSecurity.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                Dim sedesDataTable = dbmSecurity.SchemaConfig.PA_Get_Sedes.DBExecute(_plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad)
                Utilities.LlenarCombo(SedeDesktopComboBox, sedesDataTable, sedesDataTable.id_SedeColumn.ColumnName, sedesDataTable.Nombre_SedeColumn.ColumnName, True, "-1", "- Seleccione... -")

            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                If (dbmSecurity IsNot Nothing) Then dbmSecurity.Connection_Close()
            End Try
        End Sub

        Private Sub CargarCentroProcesamiento()
            Dim dbmSecurity As DBSecurityDataBaseManager = Nothing

            Try
                dbmSecurity = New DBSecurityDataBaseManager(_plugin.Manager.DesktopGlobal.ConnectionStrings.Security)

                dbmSecurity.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                'If SedeDesktopComboBox.SelectedIndex > 0 Then
                Dim centroProcesamientoDataTable = dbmSecurity.SchemaConfig.PA_Get_CentroProcesamineto_Sedes.DBExecute(CShort(SedeDesktopComboBox.SelectedValue), _plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad)
                Utilities.LlenarCombo(CentroProcesamientoDesktopComboBox, centroProcesamientoDataTable, centroProcesamientoDataTable.id_Centro_ProcesamientoColumn.ColumnName, centroProcesamientoDataTable.Nombre_Centro_ProcesamientoColumn.ColumnName, True, "-1", "- Seleccione... -")
                ' End If


            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                If (dbmSecurity IsNot Nothing) Then dbmSecurity.Connection_Close()
            End Try
        End Sub

        Private Sub MesaDestapeDataGridView_CellMouseDoubleClick(ByVal sender As Object, ByVal e As DataGridViewCellMouseEventArgs) Handles MesaDestapeDataGridView.CellMouseDoubleClick
            Dim agregarMesaForm As New FormNuevaMesaDestape(_plugin)
            agregarMesaForm._idMesaDestape = CInt(MesaDestapeDataGridView.Rows(e.RowIndex).Cells(0).Value.ToString())
            agregarMesaForm._idSede = CInt(MesaDestapeDataGridView.Rows(e.RowIndex).Cells(4).Value.ToString())
            agregarMesaForm._idCentroProcesamiento = CInt(MesaDestapeDataGridView.Rows(e.RowIndex).Cells(5).Value.ToString())
            agregarMesaForm.PCNameTextBox.Text = MesaDestapeDataGridView.Rows(e.RowIndex).Cells(1).Value.ToString()
            If agregarMesaForm.ShowDialog() = DialogResult.OK Then
                BuscarMesasDestape()
            End If
        End Sub

    End Class

End Namespace