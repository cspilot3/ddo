Imports System.Windows.Forms
Imports DBAgrario
Imports DBAgrario.SchemaProcess
Imports DBArchiving
Imports Miharu.Desktop.Controls.DesktopMessageBox

Namespace Risk.Forms.OT

    Public Class FormCerrarOT
        Inherits Form

#Region " Declaraciones "

        Private _Plugin As BanagrarioRiskPlugin

#End Region

#Region " Constructores "

        Public Sub New(ByVal nBanagrarioDesktopPlugin As BanagrarioRiskPlugin)
            InitializeComponent()

            'FormHelper.ControlarEventoCerrarVentanaTeclaEscape(Me)
            _Plugin = nBanagrarioDesktopPlugin
        End Sub

#End Region

#Region " Eventos "

        Private Sub CerrarOTButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CerrarOTButton.Click
            MostrarDetalleCerrarOT()
        End Sub

        Private Sub CancelarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CancelarButton.Click
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        End Sub

        Private Sub BuscarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BuscarButton.Click
            Buscar()
        End Sub

        Private Sub OTDataGridView_SelectionChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles OTDataGridView.SelectionChanged
            'If (OTProcessDataSet.CTA_Cierre_OT.Count > 0) Then
            '    Dim Fila As SchemaProcess.CTA_Cierre_OTRow = CType(CType(OTDataGridView.CurrentRow.DataBoundItem, DataRowView).Row, SchemaProcess.CTA_Cierre_OTRow)

            '    CerrarOTButton.Enabled = (Fila.id_Estado <> EstadoEnum.Cerrado)
            'Else
            '    CerrarOTButton.Enabled = False
            'End If
        End Sub

        Private Sub InformeButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles InformeButton.Click
            Exportar()
        End Sub

#End Region

#Region " Funciones "

        Private Sub Buscar()
            Dim dmArchiving As DBArchivingDataBaseManager = Nothing

            Try
                dmArchiving = New DBArchivingDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Archiving)
                dmArchiving.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
                dmArchiving.Transaction_Begin()
                dmArchiving.Schemadbo.PA_Actualizar_OTs.DBExecute()
                dmArchiving.Transaction_Commit()

            Catch ex As Exception
                dmArchiving.Transaction_Rollback()
            Finally
                If (Not dmArchiving Is Nothing) Then dmArchiving.Connection_Close()
            End Try


            Dim dbmAgrario As DBAgrarioDataBaseManager = Nothing

            Try
                dbmAgrario = New DBAgrarioDataBaseManager(Me._Plugin.BancoAgrarioConnectionString)
                'dbmAgrario.DataBase.Identifier_Date_Format = _Plugin.Manager.DesktopGlobal.IdentifierDateFormat
                dbmAgrario.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                Dim FechaInicial As DateTime = New DateTime(FechaInicialDateTimePicker.Value.Year, FechaInicialDateTimePicker.Value.Month, FechaInicialDateTimePicker.Value.Day)
                Dim FechaFinal As DateTime = New DateTime(FechaFinalDateTimePicker.Value.Year, FechaFinalDateTimePicker.Value.Month, FechaFinalDateTimePicker.Value.Day, 23, 59, 59)

                Dim Datos = dbmAgrario.SchemaProcess.PA_Cierre_OT.DBExecute(FechaInicial, FechaFinal)

                OTProcessDataSet.CTA_Cierre_OT.Clear()
                OTProcessDataSet.CTA_Cierre_OT.Merge(Datos)

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("No fue posible cargar las OTS, " + ex.Message, "GenerarOT", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
            Finally
                If (Not dbmAgrario Is Nothing) Then dbmAgrario.Connection_Close()
            End Try
        End Sub

        Private Sub MostrarDetalleCerrarOT()
            If (OTProcessDataSet.CTA_Cierre_OT.Count > 0) Then
                Try
                    Dim Fila As CTA_Cierre_OTRow = CType(CType(OTDataGridView.CurrentRow.DataBoundItem, DataRowView).Row, CTA_Cierre_OTRow)

                    Dim dlgDetalleOT As New FormCerrarOTDetalle(_Plugin, Fila.id_OT, Fila.Fecha_Proceso.ToString("yyyy-MM-dd"), Fila.id_Estado, Fila.Nombre_Ciudad)
                    If (dlgDetalleOT.ShowDialog() = DialogResult.OK) Then
                        Buscar()
                    End If
                Catch ex As Exception
                    DesktopMessageBoxControl.DesktopMessageShow("CerrarOT", ex)
                End Try
            End If
        End Sub

        Private Sub Exportar()
            Dim Cuadro As New SaveFileDialog()

            Cuadro.Filter = "Archivo de Excel (*.xls)|*.xls"

            Dim Respuesta = Cuadro.ShowDialog()

            If (Respuesta = DialogResult.OK) Then
                Try
                    Dim Exportador As New Slyg.Tools.CSV.CSVData(vbTab, """", True)

                    Dim Datos = New DataTable()
                    Datos.Merge(OTProcessDataSet.CTA_Cierre_OT)

                    Exportador.DataTable = New Slyg.Tools.CSV.CSVTable(Datos)
                    Exportador.SaveAsCSV(Cuadro.FileName, False)

                    MessageBox.Show("La información se exportó exitosamente", "Exportar", MessageBoxButtons.OK, MessageBoxIcon.Information)

                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Exportar", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        End Sub

#End Region

    End Class

End Namespace