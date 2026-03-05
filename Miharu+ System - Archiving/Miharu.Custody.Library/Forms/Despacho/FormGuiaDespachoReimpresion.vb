Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Controls
Imports Microsoft.Reporting.WinForms
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Config
Imports Slyg.Tools
Imports DBArchiving

Namespace Forms.Despacho

    Public Class FormGuiaDespachoReimpresion
        Inherits FormBase

        Private Sub FormGuiaDespachoReimpresion_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            Fecha_FinalDateTimePicker.Text = Date.Now.ToString
        End Sub

        Private Sub BuscarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BuscarButton.Click
            BuscarGuiasDespacho()
        End Sub

        Private Sub CerrarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CerrarButton.Click
            Me.Close()
        End Sub

        Private Sub ImprimirButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ImprimirButton.Click
            ImprimirGuia()
        End Sub

        Private Sub GuiasDesktopDataGridView_CellDoubleClick(ByVal sender As System.Object, ByVal e As DataGridViewCellEventArgs) Handles GuiasDesktopDataGridView.CellDoubleClick
            ImprimirGuia()
        End Sub

        Private Sub CorrecionDatosButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CorrecionDatosButton.Click
            CorregirData()
        End Sub

#Region " Metodos "

        Public Sub BuscarGuiasDespacho()
            GuiasDesktopDataGridView.AutoGenerateColumns = False
            GuiasDesktopDataGridView.DataSource = Nothing
            Dim dmArchiving As New DBArchiving.DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dmArchiving.Connection_Open(Program.Sesion.Usuario.id)
            Dim Guias = dmArchiving.Schemadbo.PA_Buscar_Guia_Despacho_Solicitudes.DBExecute(CDate(Fecha_InicialDateTimePicker.Text), CDate(Fecha_FinalDateTimePicker.Text), Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede)
            dmArchiving.Connection_Close()

            If Guias.Rows.Count = 0 Then
                DesktopMessageBoxControl.DesktopMessageShow("No se han encontrado guias para estas fecha y para la posicion del centro de procesamiento actual", "Datos no encontrados", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
            Else
                GuiasDesktopDataGridView.DataSource = Guias
            End If
        End Sub

        Public Sub ImprimirGuia()
            If GuiasDesktopDataGridView.SelectedRows.Count <> 0 Then

                Dim Table As DataTable
                Dim TableE As DataTable

                Dim idGuiaDespacho = CInt(GuiasDesktopDataGridView.SelectedRows(0).Cells("id_Guia_Despacho").Value)
                Dim dmArchiving As New DBArchiving.DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
                dmArchiving.Connection_Open(Program.Sesion.Usuario.id)

                Dim EntidadConsulta = dmArchiving.SchemaReport.PA_Obtener_Entidad_Reporte_Guia_Despacho.DBExecute(idGuiaDespacho)
                Dim Entidad As String = EntidadConsulta.Rows(0).Item(0).ToString()

                If (CInt(dmArchiving.SchemaConfig.TBL_Parametro.DBGet("@ProximosVencerLetras").Rows(0).Item("Valor_Parametro")) = CInt(Entidad)) Then
                    Table = dmArchiving.SchemaReport.PA_Guia_Despacho_Solicitudes.DBExecute(idGuiaDespacho)
                    TableE = dmArchiving.SchemaReport.PA_Guia_Despacho_Solicitudes_Encabezado.DBExecute(idGuiaDespacho)
                Else
                    Table = dmArchiving.Schemadbo.RPT_Guia_Despacho_Solicitudes.DBFindByid_Guia_Despacho(idGuiaDespacho)
                    TableE = dmArchiving.Schemadbo.RPT_Guia_Despacho_Solicitudes_Encabezado.DBFindByid_Guia_Despacho(idGuiaDespacho)
                End If

                If GrupoRadioButton.Checked Then
                    If (CInt(dmArchiving.SchemaConfig.TBL_Parametro.DBGet("@ProximosVencerLetras").Rows(0).Item("Valor_Parametro")) = CInt(Entidad)) Then
                        CargaReportes("Miharu.Custody.Library.GuiaDespachoCampo.rdlc", Table, TableE, CInt(Entidad))
                    Else
                        CargaReportes("Miharu.Custody.Library.GuiaDespacho.rdlc", Table, TableE, CInt(Entidad))
                    End If
                Else
                    If (CInt(dmArchiving.SchemaConfig.TBL_Parametro.DBGet("@ProximosVencerLetras").Rows(0).Item("Valor_Parametro")) = CInt(Entidad)) Then
                        CargaReportes("Miharu.Custody.Library.GuiaDespachoOrdenCampo.rdlc", Table, TableE, CInt(Entidad))
                    Else
                        CargaReportes("Miharu.Custody.Library.GuiaDespachoOrden.rdlc", Table, TableE, CInt(Entidad))
                    End If

                End If

                dmArchiving.Connection_Close()

            Else
                DesktopMessageBoxControl.DesktopMessageShow("No ha selecionado ninguna guia para imprimir, por favor selecciona una y pruebe nuevamente", "Error imprimiendo guia", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
            End If
        End Sub

        Public Sub CorregirData()
            If GuiasDesktopDataGridView.SelectedRows.Count <> 0 Then
                Dim idGuiaDespacho = CShort(GuiasDesktopDataGridView.SelectedRows(0).Cells("id_Guia_Despacho").Value)
                Dim nGuia = CStr(GuiasDesktopDataGridView.SelectedRows(0).Cells("Guia").Value)
                Dim nSello = CStr(GuiasDesktopDataGridView.SelectedRows(0).Cells("Sello").Value)

                If DesktopMessageBoxControl.DesktopMessageShow("Esta seguro que desea cambiar La guia y el sello del item Seleccionado?", "Correccion Guias", DesktopMessageBoxControl.IconEnum.AdvertencyIcon) = DialogResult.OK Then
                    Dim GuiSello As New FormGuiaDespacho
                    GuiSello.Guia = nGuia
                    GuiSello.Sello = nSello
                    GuiSello.ShowDialog()

                    If GuiSello.Guia = "" Or GuiSello.Sello = "" Then
                        DesktopMessageBoxControl.DesktopMessageShow("No puede dejar la Guia ni el sello en blanco", "Error en Guia y sello", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                    Else
                        Dim dmArchiving As New DBArchiving.DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)

                        Try
                            dmArchiving.Connection_Open(Program.Sesion.Usuario.id)
                            dmArchiving.Transaction_Begin()

                            Dim GuiaDespacho As New DBArchiving.SchemaCustody.TBL_Guia_DespachoType
                            GuiaDespacho.Fecha_Log = SlygNullable.SysDate
                            GuiaDespacho.Guia = GuiSello.Guia
                            GuiaDespacho.Sello = GuiSello.Sello
                            GuiaDespacho.Usuario_Log = Program.Sesion.Usuario.id

                            dmArchiving.SchemaCustody.TBL_Guia_Despacho.DBUpdate(GuiaDespacho, idGuiaDespacho)
                            dmArchiving.Transaction_Commit()
                            DesktopMessageBoxControl.DesktopMessageShow("Datos actualizados con exito", "Correccion Guia y Sello OK", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)

                        Catch ex As Exception
                            dmArchiving.Transaction_Commit()
                            DesktopMessageBoxControl.DesktopMessageShow("CorregirData", ex)
                        Finally
                            dmArchiving.Connection_Close()
                            BuscarGuiasDespacho()
                        End Try
                    End If
                End If
            End If
        End Sub

        Private Sub CargaReportes(ByVal ReportPath As String, ByVal TableDetalle As DataTable, ByVal TableEncabezado As DataTable, ByVal Entidad As Integer)
            Me.ReportViewer.LocalReport.ReportEmbeddedResource = ReportPath
            ReportViewer.LocalReport.DataSources.Clear()

            Dim dmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dmArchiving.Connection_Open(Program.Sesion.Usuario.id)

            Dim Parametros As New List(Of ReportParameter)

            Dim Par1 As New ReportParameter
            Par1.Name = "Titulo1"
            Par1.Values.Add(TableEncabezado.Rows(0)("TituloLlave1").ToString())

            Dim Par2 As New ReportParameter
            Par2.Name = "Titulo2"
            Par2.Values.Add(TableEncabezado.Rows(0)("TituloLlave2").ToString())

            Parametros.Add(Par1)
            Parametros.Add(Par2)

            If (CInt(dmArchiving.SchemaConfig.TBL_Parametro.DBGet("@ProximosVencerLetras").Rows(0).Item("Valor_Parametro")) = Entidad) Then
                Dim Par3 As New ReportParameter
                Par3.Name = "TituloCampo"
                Par3.Values.Add(TableEncabezado.Rows(0)("TituloCampo").ToString())
                Parametros.Add(Par3)
            End If

            Utilities.NewDataSource(ReportViewer, "GuiaDespacho", TableDetalle, Parametros)
            Utilities.NewDataSource(ReportViewer, "GuiaDespachoEncabezado", TableEncabezado)

            Me.ReportViewer.RefreshReport()
        End Sub

#End Region

    End Class

End Namespace