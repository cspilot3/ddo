Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Risk.Library.Forms.CBarras
Imports Miharu.Desktop.Library.Config
Imports DBArchiving

Namespace Forms.Destape

    Public Class FormPrecintosCarpetas
        Inherits Miharu.Desktop.Library.FormBase

#Region " Declaraciones "

        Dim TablePrecintos As DataTable

#End Region

#Region " Funciones "

        Public Function CrearTabla(ByRef Tabledata As DataTable) As DataTable
            Dim Table As DataTable = Utilities.clonarDataTable(Tabledata)
            Const Columnas As String = "nombre_proyecto_llave"
            Const ColumnasValor As String = "valor_llave"


            Dim llaves As String() = {"fk_expediente", "CBarras_folder", "fk_ot", "id_folder", "fk_precinto", "fk_estado", "Estado"}

            'Captura de las columnas a crear
            Dim TableColumns As DataTable = Utilities.clonarDataTable(Table).DefaultView.ToTable(True, Columnas)

            'Creacion de columnas
            Dim Column As DataColumn
            For Each rowcolumns As DataRow In TableColumns.Rows
                Column = New DataColumn
                Column.ColumnName = rowcolumns("nombre_proyecto_llave").ToString
                Table.Columns.Add(Column)
            Next
            Column = New DataColumn
            Column.ColumnName = "IDVS"
            Table.Columns.Add(Column)
            Column = New DataColumn
            Column.ColumnName = "NumeracionVS"
            Table.Columns.Add(Column)

            'Ordenamiento datos por llaves
            Dim viewOrder As DataView = Table.DefaultView
            viewOrder.Sort = Join(llaves, ",")
            Table = viewOrder.ToTable

            'Asignacion de id por llaves
            Dim Order As Integer = 0
            Dim TableId As DataTable = Table.DefaultView.ToTable(False, llaves)
            If Table.Rows.Count > 1 Then
                Table.Rows(0)("IDVS") = 0
                Table.Rows(0)("NumeracionVS") = 0
            End If

            For i As Integer = 1 To Table.Rows.Count - 1 Step 1
                If (CompareRows(TableId.Rows(i), TableId.Rows(i - 1))) Then
                    Table.Rows(i)("IDVS") = Order
                Else
                    Order += 1
                    Table.Rows(i)("IDVS") = Order
                End If
                Table.Rows(i)("NumeracionVS") = i
            Next

            If Table.Rows.Count = 1 Then
                Table.Rows(0)("IDVS") = 0
                Table.Rows(0)("NumeracionVS") = 1
            End If

            Dim TableFiltro As New DataTable
            TableFiltro.Columns.Add("ID")
            TableFiltro.Columns.Add("Llave")
            TableFiltro.Columns.Add("Valor")
            TableFiltro.Columns.Add("Group")

            For j As Integer = 0 To Order Step 1
                For Each rowColumna As DataRow In TableColumns.Rows
                    Dim viewfiltro As DataView = Table.DefaultView
                    viewfiltro.RowFilter = Columnas & "='" & rowColumna(Columnas).ToString & "' and IDVS='" & CStr(j) & "'"
                    Dim table2 As DataTable = viewfiltro.ToTable(True, "NumeracionVS", Columnas, ColumnasValor)

                    If table2.Rows.Count > 0 Then
                        Dim row As DataRow = TableFiltro.NewRow
                        row("ID") = table2.Rows(0)("NumeracionVS").ToString
                        row("Llave") = table2.Rows(0)(Columnas).ToString
                        row("Valor") = table2.Rows(0)(ColumnasValor).ToString
                        row("Group") = CStr(j)
                        TableFiltro.Rows.Add(row)
                    End If
                Next
            Next

            Dim columnasFinales(llaves.Length + TableColumns.Rows.Count) As String
            Dim count As Integer = 0

            columnasFinales(count) = "IDVS"
            count += 1

            For Each llave As String In llaves
                columnasFinales(count) = llave
                count += 1
            Next

            For Each rowllave As DataRow In TableColumns.Rows
                columnasFinales(count) = rowllave(Columnas).ToString
                count += 1
            Next

            Table = Utilities.clonarDataTable(Table).DefaultView.ToTable(True, columnasFinales)

            For Each rowData As DataRow In TableFiltro.Rows
                Dim indice As Integer = CInt(rowData("Group").ToString)
                Dim Columna As String = rowData("Llave").ToString
                Dim Valor As String = rowData("Valor").ToString

                Table.Rows(indice)(Columna) = Valor
            Next

            Table.Columns.Remove("IDVS")
            Table = Table.DefaultView.ToTable(True)

            Return Table
        End Function

        Public Function CompareRows(ByVal row1 As DataRow, ByVal Row2 As DataRow) As Boolean
            Dim valida = Not row1.Table.Columns.Count <> Row2.Table.Columns.Count

            'Valida cantidad columnas

            For Each columna As DataColumn In row1.Table.Columns
                Try
                    If row1(columna.ColumnName).ToString <> Row2(columna.ColumnName).ToString Then
                        valida = False
                    End If
                Catch ex As Exception
                    valida = False
                End Try
            Next

            Return valida
        End Function

        Public Sub CargaDocumentos()
            Dim dmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dmArchiving.Connection_Open(Program.Sesion.Usuario.id)

            CajaProcesoLabel.Text = Program.RiskGlobal.CajaProceso
            PrecintoLabel.Text = Program.RiskGlobal.Precinto

            'Trae las carpetas que corresponden al precinto seleccionado
            'Dim TableCarpetas As DataTable = dmArchiving.Schemadbo.CTA_Precinto_carpeta_detalle.DBFindByfk_precintofk_entidadfk_proyecto(Program.RiskGlobal.Precinto, Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto)
            Dim TableCarpetas As DataTable = dmArchiving.SchemaProcess.PA_Precinto_carpeta_detalle.DBExecute(Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, Program.RiskGlobal.Precinto)

            CarpetasDesktopDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            'CarpetasDesktopDataGridView.DataSource = CrearTabla(Utilities.ClonarDataTable(TableCarpetas))
            CarpetasDesktopDataGridView.DataSource = TableCarpetas

            dmArchiving.Connection_Close()
        End Sub

        Public Sub CargaDocumentosDetalle()
            Dim dmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dmArchiving.Connection_Open(Program.Sesion.Usuario.id)

            Try
                'Trae los documentos de la carpeta que se esta seleccionando
                Dim folderSel As Short = CShort(CarpetasDesktopDataGridView.SelectedRows(0).Cells("id_folder").Value)
                Dim Expedientesel As Integer = CInt(CarpetasDesktopDataGridView.SelectedRows(0).Cells("fk_expediente").Value)

                Dim TableDocumentosxCarpeta As DataTable = dmArchiving.Schemadbo.CTA_Carpeta_documentos.DBFindByfk_Folderfk_OTfk_expedientefk_entidadfk_proyecto(folderSel, Nothing, Expedientesel, Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto)

                DocumentosDesktopDataGridView.AutoGenerateColumns = False
                DocumentosDesktopDataGridView.DataSource = TableDocumentosxCarpeta
            Catch : End Try

            dmArchiving.Connection_Close()
        End Sub

        Public Sub SeleccionarCaja()
            Dim formCaja As New FormSeleccionarCajaProceso()
            formCaja.ShowDialog()
            Program.RiskGlobal.CajaProceso = formCaja.CajaSeleccionada
            CajaProcesoLabel.Text = CStr(Program.RiskGlobal.CajaProceso)
        End Sub

#End Region

#Region " Eventos "

        Private Sub FormPrecintosCarpetas_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            CargaDocumentos()
        End Sub

        Private Sub DestaparButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DestaparButton.Click
            If CajaProcesoLabel.Text = "" Then
                DesktopMessageBoxControl.DesktopMessageShow("Debe Seleccionar una caja de proceso.", "Seleccione Caja de proceso.", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                SeleccionarCajaButton.Focus()
            Else
                Dim Form_ As New FormLlavesIndexacion()
                Form_.ShowDialog()
                CargaDocumentos()
                ImprimirButton.Focus()
            End If
        End Sub

        Private Sub CerrarPrecintoButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CerrarPrecintoButton.Click
            Dim dmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)

            If DesktopMessageBoxControl.DesktopMessageShow("¿Está seguro de que desea cerrar el precinto?. Después de que lo cierre no podra trabajar con este nuevamente", "Cierre precinto", DesktopMessageBoxControl.IconEnum.AdvertencyIcon) = Windows.Forms.DialogResult.OK Then
                dmArchiving.Connection_Open(Program.Sesion.Usuario.id)
                Dim TableCBarras = dmArchiving.Schemadbo.PA_CBarras_Impresion.DBExecute(Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, Program.RiskGlobal.Precinto, "")
                dmArchiving.Connection_Close()

                If TableCBarras.Rows.Count > 0 Then
                    If DesktopMessageBoxControl.DesktopMessageShow("Debe imprimir todos los codigos de barras antes de cerrar el precinto, Desea imprimir los Codigos de barras?", "Poblemas Cerrando precinto", DesktopMessageBoxControl.IconEnum.AdvertencyIcon) = Windows.Forms.DialogResult.OK Then
                        Dim impresion As New FormImprimirCBarras(TableCBarras)
                        If impresion.ShowDialog() = vbOK Then
                            'cierra el precinto
                            dmArchiving.Connection_Open(Program.Sesion.Usuario.id)
                            dmArchiving.Transaction_Begin()
                            Dim Table = dmArchiving.Schemadbo.PA_Cerrar_Precinto.DBExecute(Program.RiskGlobal.Precinto, Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, DesktopConfig.Modulo.Archiving, Program.RiskGlobal.OT, DesktopConfig.Servicio_Facturacion.Destape, Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede, Program.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento, DBCore.EstadoEnum.Mesa_de_Control)
                            dmArchiving.Transaction_Commit()
                            dmArchiving.Connection_Close()

                            If Table.Rows(0)(0).ToString = "OK" Then
                                DesktopMessageBoxControl.DesktopMessageShow("El cierre del precinto ha sucedido con éxito", "Cierre Precinto", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                                Me.Close()
                            Else
                                DesktopMessageBoxControl.DesktopMessageShow("Hubo errores al cerrar al precinto, por favor comuniquese con el administrador.", "Cierre Precinto", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                            End If
                        End If
                    End If
                Else
                    'cierra el precinto
                    dmArchiving.Connection_Open(Program.Sesion.Usuario.id)
                    dmArchiving.Transaction_Begin()
                    Dim Table = dmArchiving.Schemadbo.PA_Cerrar_Precinto.DBExecute(Program.RiskGlobal.Precinto, Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, DesktopConfig.Modulo.Archiving, Program.RiskGlobal.OT, DesktopConfig.Servicio_Facturacion.Destape, Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede, Program.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento, DBCore.EstadoEnum.Mesa_de_Control)
                    dmArchiving.Transaction_Commit()
                    dmArchiving.Connection_Close()

                    If Table.Rows(0)(0).ToString = "OK" Then
                        DesktopMessageBoxControl.DesktopMessageShow("El cierre del precinto ha sudedido con exito", "Cierre Precinto OK", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                        Me.Close()
                    Else
                        DesktopMessageBoxControl.DesktopMessageShow("Hubo errores al cerrar al precinto, por favor comuniquese con el administrador", "Cierre Precinto OK", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                    End If
                End If
            End If

        End Sub

        Private Sub CerrarButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CerrarButton.Click
            Me.Close()
        End Sub

        Private Sub SeleccionarCajaButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SeleccionarCajaButton.Click
            SeleccionarCaja()
            DestaparButton.Focus()
        End Sub

        Private Sub CarpetasDataGridView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CarpetasDesktopDataGridView.SelectionChanged
            CargaDocumentosDetalle()
        End Sub

        Private Sub ImprimirButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ImprimirButton.Click
            Dim dmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dmArchiving.Connection_Open(Program.Sesion.Usuario.id)
            Dim TableCBarras = dmArchiving.Schemadbo.PA_CBarras_Impresion.DBExecute(Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, Program.RiskGlobal.Precinto, "")
            dmArchiving.Connection_Close()

            Dim impresion As New FormImprimirCBarras(TableCBarras)
            impresion.ShowDialog()
            CerrarPrecintoButton.Focus()
        End Sub

        Private Sub CarpetasDesktopDataGridView_CellContentDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles CarpetasDesktopDataGridView.CellContentDoubleClick
            Try
                'Imprime el codigo de barras
                Dim CBarrasImpresion As New FormCBarrasFolderFile
                CBarrasImpresion.CBarras = CStr(CarpetasDesktopDataGridView.SelectedRows(0).Cells("FolderCBarras").Value)
                CBarrasImpresion.ShowDialog()
            Catch : End Try
        End Sub

        Private Sub DocumentosDesktopDataGridView_CellContentDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DocumentosDesktopDataGridView.CellContentDoubleClick
            Try
                'Imprime el codigo de barras
                Dim CBarrasImpresion As New FormCBarrasFolderFile
                CBarrasImpresion.CBarras = CStr(DocumentosDesktopDataGridView.SelectedRows(0).Cells("CBarras").Value)
                CBarrasImpresion.ShowDialog()
            Catch : End Try
        End Sub

#End Region

    End Class

End Namespace