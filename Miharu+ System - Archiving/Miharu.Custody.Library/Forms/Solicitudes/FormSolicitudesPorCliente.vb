Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Controls
Imports DBArchiving
Imports DBCore
Imports Miharu.Custody.Library.Forms.Reportes.Solicitudes
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Config
Imports Slyg.Tools
Imports Miharu.Custody.Library.Forms.Solicitudes

Public Class FormSolicitudesPorCliente
    Inherits FormBase

    Dim _idEntidadLocal As Short
    Dim DoubleClick As Boolean = False


#Region "Eventos"
    Private Sub FormSolicitudesPorCliente_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        CargarSolicitudes()
    End Sub

    Private Sub btnMostrarFormDescuelgue_Click(sender As System.Object, e As System.EventArgs) Handles btnMostrarFormDescuelgue.Click
        AbrirFormDescuelgue()
    End Sub

    Private Sub dgvEntidadSolicitudes_CellClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvEntidadSolicitudes.CellClick
        ObtenerEntidad(e)
    End Sub

    Private Sub dgvEntidadSolicitudes_CellDoubleClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvEntidadSolicitudes.CellDoubleClick
        Me.DoubleClick = True
        ObtenerEntidad(e)
    End Sub

    Private Sub FormSolicitudesPorCliente_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If (e.KeyCode = System.Windows.Forms.Keys.Escape) Then
            Me.Close()
        End If
    End Sub
#End Region

#Region "Metodos"

    Private Sub ObtenerEntidad(ByVal eventArg As DataGridViewCellEventArgs)
        Try
            Dim idEntidadvalue = Short.Parse(Me.dgvEntidadSolicitudes.Rows(eventArg.RowIndex).Cells(0).Value.ToString())

            If (idEntidadvalue <> 0) Then
                Me._idEntidadLocal = idEntidadvalue

                If (Me.DoubleClick) Then
                    AbrirFormDescuelgue()
                End If
            End If
        Catch ex As Exception
            DesktopMessageBoxControl.DesktopMessageShow("Item Seleccionado", ex)
        End Try
    End Sub

    Private Sub AbrirFormDescuelgue()
        Try
            If (Me._idEntidadLocal <> 0) Then
                Dim objDesculgeDocs As New FormDescuelgueDocumentos(Me._idEntidadLocal)
                objDesculgeDocs.ShowDialog()
                Me.Close()
            Else
                DesktopMessageBoxControl.DesktopMessageShow("Error debe seleccionar una entidad!!", "Error entidad Selecionada", DesktopMessageBoxControl.IconEnum.ErrorIcon, True, False)
            End If
        Catch ex As Exception
            DesktopMessageBoxControl.DesktopMessageShow("Error entidad Selecionada", ex)
        End Try
    End Sub

    Private Sub CargarSolicitudes()
        Dim dmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)

        Try
            dmArchiving.Connection_Open(Program.Sesion.Usuario.id)
            Me.dgvEntidadSolicitudes.AutoGenerateColumns = False
            Dim DtSolicitudesPorCliente = dmArchiving.SchemaCustody.PA_Busca_CantidadSolicitudes_VSEntidades().DBExecute()
            If (DtSolicitudesPorCliente.Rows.Count = 0) Then
                DesktopMessageBoxControl.DesktopMessageShow("No hay solicitudes pendientes", "Solicitudes", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                Return
            Else
                Me.dgvEntidadSolicitudes.DataSource = DtSolicitudesPorCliente
                dgvEntidadSolicitudes_CellClick(Me, New DataGridViewCellEventArgs(0, 0))
            End If
        Catch ex As Exception
            DesktopMessageBoxControl.DesktopMessageShow("CargaFiltros", ex)
        Finally
            dmArchiving.Connection_Close()
        End Try
    End Sub
#End Region
    
End Class