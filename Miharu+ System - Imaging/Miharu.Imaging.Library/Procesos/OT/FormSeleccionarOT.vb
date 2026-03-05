Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Controls

Namespace Procesos.OT

    Public MustInherit Class FormSeleccionarOT
        Inherits Form

#Region " Declaraciones "

        Private _pv As Boolean

#End Region

#Region " Propiedades "

        Public Property FechaProceso As Date

        Public Property OT As Integer

#End Region

#Region " Constructores "

        Protected Sub New()
            ' Llamada necesaria para el diseñador.
            InitializeComponent()

            ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        End Sub

#End Region

#Region " Eventos "

        Private Sub FormOT_Load(sender As System.Object, e As EventArgs) Handles MyBase.Load
            CargarFechas()
            CargarOTs()

            'Si solo existe una fecha de proceso y una OT la selecciona automaticamente
            If (FechaProcesoComboBox.Items.Count = 1 AndAlso FechaProcesoDataGridView.Rows.Count = 1) Then
                Dim Row = FechaProcesoDataGridView.Rows(0)
                Me.OT = Row.Value(Of Integer)("id_OT")
                Me.FechaProceso = CDate(CType(FechaProcesoComboBox.Items(0), DataRowView)("Fecha_Proceso"))
                Me.DialogResult = Windows.Forms.DialogResult.OK
                Me.Close()
            End If

            'Si no contiene data
            If FechaProcesoDataGridView.Rows.Count = 0 Then
                DesktopMessageBoxControl.DesktopMessageShow("No se encuentran Elementos disponibles para procesar", "OT", DesktopMessageBoxControl.IconEnum.AdvertencyIcon)
                Me.Close()
            ElseIf FechaProcesoComboBox.Items.Count = 0 Then
                DesktopMessageBoxControl.DesktopMessageShow("No se encuentran Elementos disponibles para procesar", "OT", DesktopMessageBoxControl.IconEnum.AdvertencyIcon)
                Me.Close()
            End If

        End Sub

        Private Sub FechaProcesoComboBox_SelectedIndexChanged(sender As System.Object, e As EventArgs) Handles FechaProcesoComboBox.SelectedIndexChanged
            CargarOTs()
        End Sub

        Private Sub FechaProcesoDataGridView_CellDoubleClick(sender As System.Object, e As Windows.Forms.DataGridViewCellEventArgs) Handles FechaProcesoDataGridView.CellDoubleClick
            If e.RowIndex > -1 Then
                SeleccionarOT(e.RowIndex)
            End If
        End Sub

        Private Sub SeleccionarButton_Click(sender As System.Object, e As EventArgs) Handles SeleccionarButton.Click
            If (FechaProcesoDataGridView.SelectedRows.Count > 0) Then
                SeleccionarOT(FechaProcesoDataGridView.SelectedCells(0).RowIndex)
            End If
        End Sub

#End Region

#Region " Metodos "

        Private Sub CargarFechas()
            _pv = True

            Dim fechaProcesoData = getFechas()

            FechaProcesoComboBox.Fill(fechaProcesoData, fechaProcesoData.id_fecha_procesoColumn, fechaProcesoData.Fecha_ProcesoColumn)
            FechaProcesoComboBox.SelectedIndex = FechaProcesoComboBox.Items.Count - 1

            _pv = False
        End Sub

        Private Sub CargarOTs()
            If (Not _pv) Then                
                FechaProcesoDataGridView.AutoGenerateColumns = False
                FechaProcesoDataGridView.DataSource = getOTs()
                FechaProcesoDataGridView.Refresh()
            End If
        End Sub
        
        Private Sub SeleccionarOT(index As Integer)
            Dim Row = FechaProcesoDataGridView.Rows(index)
            Me.OT = CInt(Row.Cells("id_OT").Value)
            Me.FechaProceso = CDate(CType(FechaProcesoComboBox.SelectedItem, DataRowView)("Fecha_Proceso"))
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        End Sub

#End Region

#Region " Funciones "

        Protected MustOverride Function getFechas() As DBImaging.SchemaProcess.TBL_Fecha_ProcesoDataTable

        Protected MustOverride Function getOTs() As DBImaging.SchemaProcess.CTA_OTDataTable

#End Region

    End Class

End Namespace