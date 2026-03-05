Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports DBArchiving
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Config

Namespace Forms.Reportes.MesaControlFisicos

    Public Class FormReporteReprocesos
        Inherits FormBase

#Region " Eventos "

        Private Sub FormReporteReprocesos_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            CargarCombos()
        End Sub

        Private Sub ClienteDesktopComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles ClienteDesktopComboBox.SelectedIndexChanged
            Dim Archiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            Archiving.Connection_Open(Program.Sesion.Usuario.id)

            Try
                Dim Proyecto = Archiving.Schemadbo.CTA_Proyecto.DBFindByfk_Entidad(CShort(ClienteDesktopComboBox.SelectedValue))
                ProyectoDesktopComboBox.DataSource = Proyecto
                ProyectoDesktopComboBox.ValueMember = Proyecto.id_ProyectoColumn.ColumnName
                ProyectoDesktopComboBox.DisplayMember = Proyecto.Nombre_ProyectoColumn.ColumnName

                Dim Esquema = Archiving.Schemadbo.CTA_Esquema.DBFindByfk_entidadfk_proyecto(CShort(ClienteDesktopComboBox.SelectedValue), CShort(ProyectoDesktopComboBox.SelectedValue))
                EsquemaDesktopComboBox.DataSource = Esquema
                EsquemaDesktopComboBox.ValueMember = Esquema.fk_esquemaColumn.ColumnName
                EsquemaDesktopComboBox.DisplayMember = Esquema.Nombre_esquemaColumn.ColumnName
            Catch : End Try

            Archiving.Connection_Close()
        End Sub

        Private Sub ProyectoDesktopComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles ProyectoDesktopComboBox.SelectedIndexChanged
            Dim Archiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            Archiving.Connection_Open(Program.Sesion.Usuario.id)

            Try
                Dim Esquema = Archiving.Schemadbo.CTA_Esquema.DBFindByfk_entidadfk_proyecto(CShort(ClienteDesktopComboBox.SelectedValue), CShort(ProyectoDesktopComboBox.SelectedValue))
                EsquemaDesktopComboBox.DataSource = Esquema
                EsquemaDesktopComboBox.ValueMember = Esquema.fk_esquemaColumn.ColumnName
                EsquemaDesktopComboBox.DisplayMember = Esquema.Nombre_esquemaColumn.ColumnName
            Catch : End Try

            Archiving.Connection_Close()
        End Sub

        Private Sub BuscarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BuscarButton.Click
            BuscarReporte()
        End Sub
#End Region

#Region " Metodos "

        Public Sub CargarCombos()
            Try
                Dim Archiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
                Archiving.Connection_Open(Program.Sesion.Usuario.id)

                Dim Entidad = Archiving.SchemaSecurity.CTA_Entidad.DBGet()
                ClienteDesktopComboBox.DataSource = Entidad
                ClienteDesktopComboBox.ValueMember = Entidad.id_EntidadColumn.ColumnName
                ClienteDesktopComboBox.DisplayMember = Entidad.Nombre_EntidadColumn.ColumnName

                Dim Proyecto = Archiving.Schemadbo.CTA_Proyecto.DBFindByfk_Entidad(CShort(ClienteDesktopComboBox.SelectedValue))
                ProyectoDesktopComboBox.DataSource = Proyecto
                ProyectoDesktopComboBox.ValueMember = Proyecto.id_ProyectoColumn.ColumnName
                ProyectoDesktopComboBox.DisplayMember = Proyecto.Nombre_ProyectoColumn.ColumnName

                Dim Esquema = Archiving.Schemadbo.CTA_Esquema.DBFindByfk_entidadfk_proyecto(CShort(ClienteDesktopComboBox.SelectedValue), CShort(ProyectoDesktopComboBox.SelectedValue))
                EsquemaDesktopComboBox.DataSource = Esquema
                EsquemaDesktopComboBox.ValueMember = Esquema.fk_esquemaColumn.ColumnName
                EsquemaDesktopComboBox.DisplayMember = Esquema.Nombre_esquemaColumn.ColumnName

                Archiving.Connection_Close()
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargarCombos", ex)
            End Try
        End Sub

        Private Sub BuscarReporte()
            Dim dbmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

            Try
                Dim TableFolderReprocesos = dbmArchiving.Schemadbo.PA_RPT_Folders_Reproceso.DBExecute(Utilities.DInt(ClienteDesktopComboBox.SelectedValue), Utilities.DInt(ProyectoDesktopComboBox.SelectedValue), Utilities.DInt(EsquemaDesktopComboBox.SelectedValue), Utilities.DInt(OTDesktopTextBox.Text))
                Dim TableFilesReprocesos = dbmArchiving.Schemadbo.PA_RPT_Files_Reproceso.DBExecute(Utilities.DInt(ClienteDesktopComboBox.SelectedValue), Utilities.DInt(ProyectoDesktopComboBox.SelectedValue), Utilities.DInt(EsquemaDesktopComboBox.SelectedValue), Utilities.DInt(OTDesktopTextBox.Text))

                If ReportViewer.LocalReport.DataSources.Count > 0 Then ReportViewer.LocalReport.DataSources.RemoveAt(0)

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

                Utilities.NewDataSource(ReportViewer, "FoldersReproceso", TableFolderReprocesos)
                Utilities.NewDataSource(ReportViewer, "FilesReproceso", TableFilesReprocesos)
                Utilities.NewDataSource(ReportViewer, "FilesReprocesoImg", dataReporte)


                Me.ReportViewer.RefreshReport()
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("BuscarReporte", ex)
            End Try

            dbmArchiving.Connection_Close()
        End Sub

#End Region

    End Class

End Namespace