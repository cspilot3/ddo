Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Controls
Imports DBCore

Namespace Forms.Arqueo

    Public Class FormReporteArqueo

        Public Entidad As Short
        Public Arqueo As Short

        Private tblArqueo As New SchemaCustody.TBL_ArqueoDataTable
        Private rowArqueo As SchemaCustody.TBL_ArqueoRow

        Private tblArqueoCaja As New SchemaCustody.CTA_RPT_Arqueo_PosicionDataTable
        Private tblArqueoFolder As New SchemaCustody.CTA_RPT_Arqueo_FolderDataTable
        Private tblArqueoFile As New SchemaCustody.CTA_RPT_Arqueo_FileDataTable

        Private Sub FormReporteArqueo_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            Dim dbmCore As New DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
            Try
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                tblArqueo.Clear()
                dbmCore.SchemaCustody.TBL_Arqueo.DBFill(tblArqueo, Entidad, Arqueo)
                rowArqueo = tblArqueo.FindByfk_Entidadid_Arqueo(Entidad, Arqueo)

                tblArqueoCaja.Clear()
                dbmCore.SchemaCustody.CTA_RPT_Arqueo_Posicion.DBFillByid_Entidad_Custodiaid_Arqueo(tblArqueoCaja, Entidad, Arqueo)

                tblArqueoFolder.Clear()
                dbmCore.SchemaCustody.CTA_RPT_Arqueo_Folder.DBFillByid_Entidad_Custodiaid_Arqueo(tblArqueoFolder, Entidad, Arqueo)

                tblArqueoFile.Clear()
                dbmCore.SchemaCustody.CTA_RPT_Arqueo_File.DBFillByid_Entidad_Custodiaid_Arqueo(tblArqueoFile, Entidad, Arqueo)

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Cargar Reporte", ex)
            Finally
                dbmCore.Connection_Close()
            End Try

            InformacionStripStatusLabel.Text = "Arqueo: " & rowArqueo.Decripcion_Arqueo & " | Entidad: " & rowArqueo.fk_Entidad.ToString

            Llenar_Combo(rowArqueo.fk_Arqueo_Modo)

        End Sub

        Private Sub Llenar_Combo(ByVal Modo As Byte)
            Select Case Modo
                Case 1
                    ModoComboBox.Items.Add("Caja")
                Case 2
                    ModoComboBox.Items.Add("Caja")
                    ModoComboBox.Items.Add("Carpeta")
                Case 3
                    ModoComboBox.Items.Add("Caja")
                    ModoComboBox.Items.Add("Carpeta")
                    ModoComboBox.Items.Add("Documento")
                Case Else
                    ModoComboBox.Items.Add("-")
            End Select
        End Sub

        Private Sub ModoComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles ModoComboBox.SelectedIndexChanged
            Select Case ModoComboBox.SelectedItem.ToString()
                Case "Caja"
                    PrimarioSplitContainer.Panel2Collapsed = True
                    Mostrar_Resultados(1)
                Case "Carpeta"
                    PrimarioSplitContainer.Panel2Collapsed = False
                    SecundarioSplitContainer.Panel2Collapsed = True
                    PrimarioSplitContainer.SplitterDistance = CInt(PrimarioSplitContainer.Height / 2)
                    Mostrar_Resultados(2)
                Case "Documento"
                    PrimarioSplitContainer.Panel2Collapsed = False
                    SecundarioSplitContainer.Panel2Collapsed = False
                    PrimarioSplitContainer.SplitterDistance = CInt(PrimarioSplitContainer.Height / 3)
                    SecundarioSplitContainer.SplitterDistance = CInt(SecundarioSplitContainer.Height / 2)
                    Mostrar_Resultados(3)
            End Select
        End Sub

        Private Sub Mostrar_Resultados(ByVal Modo As Byte)
            Select Case Modo
                Case 1
                    CajaDataGridView.DataSource = tblArqueoCaja
                Case 2
                    CajaDataGridView.DataSource = tblArqueoCaja
                    CarpetaDataGridView.DataSource = tblArqueoFolder
                Case 3
                    CajaDataGridView.DataSource = tblArqueoCaja
                    CarpetaDataGridView.DataSource = tblArqueoFolder
                    DocumentoDataGridView.DataSource = tblArqueoFile
            End Select
        End Sub

        Private Sub CajaButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CajaButton.Click
            ModoComboBox.SelectedItem = "Caja"
        End Sub

        Private Sub CarpetaButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CarpetaButton.Click
            ModoComboBox.SelectedItem = "Carpeta"
        End Sub

        Private Sub Resize_Grids()
            Select Case ModoComboBox.SelectedItem.ToString
                Case "Caja"
                    PrimarioSplitContainer.Panel2Collapsed = True
                Case "Carpeta"
                    PrimarioSplitContainer.Panel2Collapsed = False
                    SecundarioSplitContainer.Panel2Collapsed = True
                    PrimarioSplitContainer.SplitterDistance = CInt(PrimarioSplitContainer.Height / 2)
                Case "Documento"
                    PrimarioSplitContainer.Panel2Collapsed = False
                    SecundarioSplitContainer.Panel2Collapsed = False
                    PrimarioSplitContainer.SplitterDistance = CInt(PrimarioSplitContainer.Height / 3)
                    SecundarioSplitContainer.SplitterDistance = CInt(SecundarioSplitContainer.Height / 2)
            End Select
        End Sub

        Private Sub FormReporteArqueo_ResizeEnd(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.ResizeEnd
            Resize_Grids()
        End Sub

        Private Sub ExportarToolStripButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ExportarToolStripButton.Click
            Select Case ModoComboBox.SelectedItem.ToString
                Case "Caja"
                    Exportar(1)
                Case "Carpeta"
                    Exportar(2)
                Case "Documento"
                    Exportar(3)
            End Select
        End Sub

        Public Sub Exportar(ByVal Modo As Byte)
            Dim Selector = New SaveFileDialog()

            Selector.Filter = "Texto separado por tabuladores (*.tsv)|*.tsv"

            Dim Respuesta = Selector.ShowDialog()

            If (Respuesta = DialogResult.OK) Then
                Dim Exportador As New Slyg.Tools.CSV.CSVData()
                Select Case Modo
                    Case 1
                        Exportador.DataTable = New Slyg.Tools.CSV.CSVTable(tblArqueoCaja)
                    Case 2
                        Exportador.DataTable = New Slyg.Tools.CSV.CSVTable(tblArqueoFolder)
                    Case 3
                        Exportador.DataTable = New Slyg.Tools.CSV.CSVTable(tblArqueoFile)
                End Select

                Exportador.SaveAsCSV(Selector.FileName, True, False, vbTab)

                MessageBox.Show("La data se exportó con exito", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        End Sub

    End Class

End Namespace