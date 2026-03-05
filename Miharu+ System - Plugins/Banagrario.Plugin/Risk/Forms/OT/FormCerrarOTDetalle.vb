Imports System.Windows.Forms
Imports DBAgrario
Imports DBArchiving
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library.Config

Namespace Risk.Forms.OT

    Public Class FormCerrarOTDetalle

#Region " Declaraciones "

        Private _Plugin As BanagrarioRiskPlugin
        Private _OT_Actual As Integer
        Private _OT_FechaProceso As String
        Private _OT_fk_Estado As Integer
        Private _OT_Ciudad As String
#End Region

#Region " Constructores "

        Public Sub New(ByVal nBanagrarioDesktopPlugin As BanagrarioRiskPlugin, ByVal nOT_Actual As Integer, ByVal nFechaProceso As String, ByVal nOT_fk_Estado As Integer, ByVal nOT_Ciudad As String)
            InitializeComponent()

            'FormHelper.ControlarEventoCerrarVentanaTeclaEscape(Me)
            _Plugin = nBanagrarioDesktopPlugin
            _OT_Actual = nOT_Actual
            _OT_FechaProceso = nFechaProceso
            _OT_fk_Estado = nOT_fk_Estado
            _OT_Ciudad = nOT_Ciudad
        End Sub

#End Region

#Region " Eventos "
        Private Sub FormCerrarOTDetalle_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            MostrarDetalleOT()
        End Sub

        Private Sub CerrarOTButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CerrarOTButton.Click
            CerrarOT()
        End Sub

        Private Sub InformeButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles InformeButton.Click
            Exportar()
        End Sub

        Private Sub CancelarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CancelarButton.Click
            DialogResult = DialogResult.Cancel
            Close()
        End Sub

#End Region

#Region " Metodos "

        Private Sub MostrarDetalleOT()
            OTLabel.Text = "Orden de trabajo : " & _OT_Actual & "     Fecha proceso:  " & _OT_FechaProceso

            Dim dbmAgrario As DBAgrarioDataBaseManager = Nothing

            Try
                dbmAgrario = New DBAgrarioDataBaseManager(Me._Plugin.BancoAgrarioConnectionString)
                'dbmAgrario.DataBase.Identifier_Date_Format = _Plugin.Manager.DesktopGlobal.IdentifierDateFormat
                dbmAgrario.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                Dim OTData = dbmAgrario.SchemaProcess.CTA_OT_Cierre_Detalle.DBFindByid_OT(_OT_Actual)

                OTDataGridView.DataSource = OTData

                Dim precintosSinDestape As String = ""
                Dim cajasSinCierre As String = ""
                Dim contenedoresSinEmpaque As String = ""
                Dim contenedoresCantidad As New List(Of ContenedorDestapeDetalle)

                For Each item In OTData
                    If (Not item.Destapado) Then
                        If (precintosSinDestape.IndexOf(item.id_Precinto) < 0) Then
                            If (precintosSinDestape <> "") Then precintosSinDestape &= " , "
                            precintosSinDestape &= item.id_Precinto
                        End If
                    End If

                    If (Not item.IsCajaNull() AndAlso Not item.Cerrada) Then
                        If (cajasSinCierre.IndexOf(item.Caja) < 0) Then
                            If (cajasSinCierre <> "") Then cajasSinCierre &= " , "
                            cajasSinCierre &= item.Caja
                        End If
                    End If

                    If (Not item.Isfk_EstadoNull()) Then
                        If (item.fk_Estado < DBCore.EstadoEnum.Empacado) Then
                            If (contenedoresSinEmpaque.IndexOf(item.Codigo_Contenedor_Destape) < 0) Then
                                If (contenedoresSinEmpaque <> "") Then contenedoresSinEmpaque &= " , "
                                contenedoresSinEmpaque &= item.Codigo_Contenedor_Destape
                            End If
                        End If
                    End If

                    If (Not item.IsCodigo_Contenedor_DestapeNull() AndAlso item.Codigo_Contenedor_Destape.ToString().Trim() <> "") Then
                        Dim contenedor = ContenedorDestapeDetalle.Buscar(contenedoresCantidad, item.Codigo_Contenedor_Destape.ToString().Trim())
                        If (contenedor Is Nothing) Then
                            contenedor = New ContenedorDestapeDetalle() With {.CodigoDestape = item.Codigo_Contenedor_Destape.ToString().Trim(), .CantidadDestape = item.Destape_Cantidad_Doc_Real}
                            contenedoresCantidad.Add(contenedor)
                        End If

                        If (Not item.IsCodigo_Contenedor_EmpaqueNull() AndAlso item.Codigo_Contenedor_Empaque.ToString().Trim() <> "") Then
                            If (contenedor.CodigosEmpaque <> "") Then contenedor.CodigosEmpaque &= " , "
                            contenedor.CodigosEmpaque &= item.Codigo_Contenedor_Empaque.ToString().Trim()
                            contenedor.CantidadEmpaque += item.Cantidad_Documentos
                        End If
                    End If
                Next

                Dim contenedoresError As String = ""
                For Each contenedor In contenedoresCantidad
                    If (contenedor.CantidadDestape <> contenedor.CantidadEmpaque) Then
                        If (contenedoresError <> "") Then contenedoresError &= ","
                        contenedoresError &= "CodigoDestape: " & contenedor.CodigoDestape & " CantidadDestape: " & contenedor.CantidadDestape.ToString() & " CodigosEmpaque: [ " & contenedor.CodigosEmpaque & " ] CantidadEmpaque: " & contenedor.CantidadEmpaque.ToString() & Environment.NewLine
                    End If
                Next

                ResumenOTTextBox.Text = ""

                If (precintosSinDestape <> "") Then
                    ResumenOTTextBox.Text &= "Los siguientes precintos aún no se les ha finalizado el destape [ " & precintosSinDestape & " ]" & Environment.NewLine
                    ResumenOTTextBox.Text &= "Por lo que no se permite el cierre de la OT" & Environment.NewLine
                    CerrarOTButton.Enabled = False
                End If

                If (contenedoresError <> "") Then
                    ResumenOTTextBox.Text &= Environment.NewLine
                    ResumenOTTextBox.Text &= "Los siguientes contenedores presentan diferencias entre las cantidades en destape y empaque: " & Environment.NewLine
                    ResumenOTTextBox.Text &= contenedoresError
                End If

                If (cajasSinCierre <> "") Then
                    ResumenOTTextBox.Text &= Environment.NewLine
                    ResumenOTTextBox.Text &= "Las siguientes cajas aún no ha sido cerradas [ " & cajasSinCierre & " ]" & Environment.NewLine
                    ResumenOTTextBox.Text &= "Por lo que no se permite el cierre de la OT" & Environment.NewLine
                    CerrarOTButton.Enabled = False
                End If

                If (contenedoresSinEmpaque <> "") Then
                    ResumenOTTextBox.Text &= Environment.NewLine
                    ResumenOTTextBox.Text &= "Los siguientes contenedores aún no ha sido empacados [ " & contenedoresSinEmpaque & " ]" & Environment.NewLine
                    ResumenOTTextBox.Text &= "Por lo que no se permite el cierre de la OT" & Environment.NewLine
                    CerrarOTButton.Enabled = False
                End If


                If (ResumenOTTextBox.Text = "") Then
                    ResumenOTTextBox.Text = "Los registros de la OT se encuentran correctamente para realizar el cierre"
                Else
                    ResumenOTTextBox.Text = "Los registros de la OT no cruzan correctamente" & Environment.NewLine & ResumenOTTextBox.Text
                End If


            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("No fue posible cargar el detalle de la OT, " + ex.Message, "GenerarOT", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
            Finally
                Try : dbmAgrario.Connection_Close() : Catch : End Try
            End Try
        End Sub

        Private Sub CerrarOT()
            'If (OTProcessDataSet.CTA_Cierre_OT.Count > 0) Then
            '    'Dim Fila As SchemaProcess.CTA_Cierre_OTRow = CType(CType(OTDataGridView.CurrentRow.DataBoundItem, DataRowView).Row, SchemaProcess.CTA_Cierre_OTRow)

            If (_OT_fk_Estado <> DBCore.EstadoEnum.Cerrado) Then

                If DesktopMessageBoxControl.DesktopMessageShow("Esta seguro de cerrar la OT?, los documentos que no hayan sido destapados quedaran como faltantes.", "Cerrar OT", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, False) = DialogResult.OK Then
                    Dim dmArchiving As DBArchivingDataBaseManager = Nothing

                    Try
                        dmArchiving = New DBArchivingDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Archiving)
                        dmArchiving.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                        dmArchiving.Transaction_Begin()
                        dmArchiving.Schemadbo.PA_Cerrar_OT.DBExecute(_OT_Actual, _Plugin.Manager.Sesion.Usuario.id, DesktopConfig.Modulo.Archiving, DBCore.EstadoEnum.Cerrado)
                        dmArchiving.Transaction_Commit()

                        DesktopMessageBoxControl.DesktopMessageShow("La OT [" & _OT_FechaProceso & "] - [" & _OT_Ciudad & "] se ha cerrado con exito", "Cerrar OT", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)

                        DialogResult = DialogResult.OK
                        Close()

                    Catch ex As Exception
                        Try : dmArchiving.Transaction_Rollback() : Catch : End Try
                    Finally
                        Try : dmArchiving.Connection_Close() : Catch : End Try
                    End Try
                End If
            Else
                DesktopMessageBoxControl.DesktopMessageShow("La OT ya se encuentra cerrada", "Cerrar OT", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
            End If

            'End If
        End Sub

        Private Sub Exportar()
            Dim Cuadro As New SaveFileDialog()

            Cuadro.Filter = "Archivo de Excel (*.xls)|*.xls"

            Dim Respuesta = Cuadro.ShowDialog()

            If (Respuesta = DialogResult.OK) Then
                Try
                    Dim Exportador As New Slyg.Tools.CSV.CSVData(vbTab, """", True)

                    Dim Datos = New DataTable()
                    Datos.Merge(CType(OTDataGridView.DataSource, DataTable))

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

    Public Class ContenedorDestapeDetalle
        Public CodigoDestape As String = ""
        Public CantidadDestape As Integer = 0
        Public CantidadEmpaque As Integer = 0
        Public CodigosEmpaque As String = ""

        Public Shared Function Buscar(ByVal nLista As List(Of ContenedorDestapeDetalle), ByVal nCodigoDestape As String) As ContenedorDestapeDetalle
            For Each item In nLista
                If (item.CodigoDestape = nCodigoDestape) Then
                    Return item
                End If
            Next
            Return Nothing
        End Function

    End Class

End Namespace