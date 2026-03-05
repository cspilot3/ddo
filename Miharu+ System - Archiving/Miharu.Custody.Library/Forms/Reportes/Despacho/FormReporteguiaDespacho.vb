Imports Miharu.Desktop.Library
Imports Microsoft.Reporting.WinForms
Imports Miharu.Desktop.Library.Config
Imports DBArchiving

Namespace Forms.Reportes.Despacho

    Public Class FormReporteguiaDespacho
        Inherits FormBase

#Region " Declaraciones "

        Private _TableGuias As DataTable
        Private _TableGuiasEncabezado As DataTable

        Private Entidad As Integer

#End Region

#Region " Constructor "

        Sub New(ByVal TableGuias As DataTable, ByVal TableGuiasEncabezado As DataTable, ByVal nEntidad As Integer)
            ' This call is required by the designer.
            InitializeComponent()

            Entidad = nEntidad

            ' Add any initialization after the InitializeComponent() call.
            _TableGuias = TableGuias
            _TableGuiasEncabezado = TableGuiasEncabezado
        End Sub

#End Region

#Region " Metodos "

        Private Sub FormReporteguiaDespacho_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load

            Dim dmArchiving As New DBArchiving.DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dmArchiving.Connection_Open(Program.Sesion.Usuario.id)

            If (CInt(dmArchiving.SchemaConfig.TBL_Parametro.DBGet("@ProximosVencerLetras").Rows(0).Item("Valor_Parametro")) = Entidad) Then
                CargaReportes("Miharu.Custody.Library.GuiaDespachoCampo.rdlc", _TableGuias)
            Else
                CargaReportes("Miharu.Custody.Library.GuiaDespacho.rdlc", _TableGuias)
            End If

            dmArchiving.Connection_Close()
        End Sub

        Private Sub CargaReportes(ByVal ReportPath As String, ByVal Table As DataTable)

            Dim dmArchiving As New DBArchiving.DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dmArchiving.Connection_Open(Program.Sesion.Usuario.id)

            Me.ReportViewer.LocalReport.ReportEmbeddedResource = ReportPath
            ReportViewer.LocalReport.DataSources.Clear()

            Dim Parametros As New List(Of ReportParameter)

            Dim Par1 As New ReportParameter
            Par1.Name = "Titulo1"
            Par1.Values.Add(_TableGuiasEncabezado.Rows(0)("TituloLlave1").ToString())

            Dim Par2 As New ReportParameter("Titulo2")
            Par2.Name = "Titulo2"
            Par2.Values.Add(_TableGuiasEncabezado.Rows(0)("TituloLlave2").ToString())

            Parametros.Add(Par1)
            Parametros.Add(Par2)

            If (CInt(dmArchiving.SchemaConfig.TBL_Parametro.DBGet("@ProximosVencerLetras").Rows(0).Item("Valor_Parametro")) = Entidad) Then
                Dim Par3 As New ReportParameter
                Par3.Name = "TituloCampo"
                Par3.Values.Add(_TableGuiasEncabezado.Rows(0)("TituloCampo").ToString())
                Parametros.Add(Par3)
            End If

            Utilities.NewDataSource(ReportViewer, "GuiaDespacho", Table, Parametros)
            Utilities.NewDataSource(ReportViewer, "GuiaDespachoEncabezado", _TableGuiasEncabezado)

            dmArchiving.Connection_Close()

            Me.ReportViewer.RefreshReport()
        End Sub

        Private Sub ImprimirButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ImprimirButton.Click
            Dim dmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dmArchiving.Connection_Open(Program.Sesion.Usuario.id)

            If Not OrdenRadioButton.Checked Then

                If (CInt(dmArchiving.SchemaConfig.TBL_Parametro.DBGet("@ProximosVencerLetras").Rows(0).Item("Valor_Parametro")) = Entidad) Then
                    CargaReportes("Miharu.Custody.Library.GuiaDespachoCampo.rdlc", _TableGuias)
                Else
                    CargaReportes("Miharu.Custody.Library.GuiaDespacho.rdlc", _TableGuias)
                End If

            Else
                If (CInt(dmArchiving.SchemaConfig.TBL_Parametro.DBGet("@ProximosVencerLetras").Rows(0).Item("Valor_Parametro")) = Entidad) Then
                    CargaReportes("Miharu.Custody.Library.GuiaDespachoOrdenCampo.rdlc", _TableGuias)
                Else
                    CargaReportes("Miharu.Custody.Library.GuiaDespachoOrden.rdlc", _TableGuias)
                End If

            End If
        End Sub

#End Region

    End Class

End Namespace