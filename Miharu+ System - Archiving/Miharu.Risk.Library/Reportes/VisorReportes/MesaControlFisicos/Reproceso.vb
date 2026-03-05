Imports DBArchiving
Imports Miharu.Desktop.Controls.DesktopReportViewer
Imports Miharu.Desktop.Library.Config

Namespace Reportes.VisorReportes.MesaControlFisicos

    Public Class Reproceso
        Inherits DesktopReport

#Region " Propiedades "

        Overrides ReadOnly Property ReportName As String
            Get
                Return "Reprocesos"
            End Get
        End Property
#End Region

#Region " Constructores "

        Public Sub New(ByRef nReportViewer As DesktopReportViewerControl)
            MyBase.New(nReportViewer)
        End Sub

#End Region

#Region " Metodos "

        Public Overrides Sub Launch()
            Dim Reproceso As New FormParametrosMesaControl()

            Dim dbmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)
            If Reproceso.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim TableFolderReprocesos = dbmArchiving.Schemadbo.PA_RPT_Folders_Reproceso.DBExecute(Utilities.DInt(Reproceso.ClienteDesktopComboBox.SelectedValue), Utilities.DInt(Reproceso.ProyectoDesktopComboBox.SelectedValue), Utilities.DInt(Reproceso.EsquemaDesktopComboBox.SelectedValue), Utilities.DInt(Reproceso.OTDesktopTextBox.Text))
                Dim TableFilesReprocesos = dbmArchiving.Schemadbo.PA_RPT_Files_Reproceso.DBExecute(Utilities.DInt(Reproceso.ClienteDesktopComboBox.SelectedValue), Utilities.DInt(Reproceso.ProyectoDesktopComboBox.SelectedValue), Utilities.DInt(Reproceso.EsquemaDesktopComboBox.SelectedValue), Utilities.DInt(Reproceso.OTDesktopTextBox.Text))

                If Me.ReportViewer.MainReportViewer.LocalReport.DataSources.Count > 0 Then Me.ReportViewer.MainReportViewer.LocalReport.DataSources.RemoveAt(0)

                'Reporte con imagen
                Dim dataReporte = New xsdFilesReproceso.FilesReprocesoDataTable
                For Each file In TableFilesReprocesos

                    Dim fila = dataReporte.NewFilesReprocesoRow()
                    Dim objCBarras = New Desktop.Controls.BarCode.BarCodeControl()
                    objCBarras.BarCode = file.CBarras_File
                    objCBarras.BarCodeHeight = 50
                    objCBarras.BarCodeType = Desktop.Controls.BarCode.BarCodeTypeType.EAN128
                    objCBarras.Align = Desktop.Controls.BarCode.AlignType.Center
                    objCBarras.ShowFooter = False
                    objCBarras.Width = 200
                    objCBarras.Height = 40
                    objCBarras.Weight = Desktop.Controls.BarCode.BarCodeWeight.Small
                    objCBarras.ShowHeader = False
                    objCBarras.FooterLinesClear()
                    objCBarras.Update()

                    fila.CBarrasImage = objCBarras.GetImage(Drawing.Imaging.ImageFormat.Jpeg)
                    fila.CBarras_File = file.CBarras_File
                    fila.Nombre_Tipologia = file.Nombre_Tipologia
                    fila.Nombre_Reproceso_Motivo = file.Nombre_Reproceso_Motivo
                    fila.Fecha_Reproceso = CStr(file.Fecha_Reproceso)
                    dataReporte.Rows.Add(fila)
                Next

                Utilities.NewDataSource(Me.ReportViewer.MainReportViewer, "FoldersReproceso", TableFolderReprocesos)
                Utilities.NewDataSource(Me.ReportViewer.MainReportViewer, "FilesReproceso", TableFilesReprocesos)
                Utilities.NewDataSource(Me.ReportViewer.MainReportViewer, "FilesReprocesoImg", dataReporte)
                Me.ReportViewer.MainReportViewer.LocalReport.ReportEmbeddedResource = "Miharu.Risk.Library.Reprocesos.rdlc"

                Me.ReportViewer.MainReportViewer.RefreshReport()

                dbmArchiving.Connection_Close()
            Else
                Reproceso.Close()
            End If

        End Sub

#End Region

    End Class

End Namespace