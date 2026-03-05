Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports DBAgrario
Imports DBAgrario.SchemaProcess

Namespace Imaging.Forms.Correciones

    Public Class FormModuloCorreccionesCargue

#Region " Declaraciones"

        Private _plugin As BanagrarioImagingPlugin
        
#End Region

#Region " Constructores "

        Public Sub New(ByVal nBanagrarioDesktopPlugin As BanagrarioImagingPlugin)

            InitializeComponent()
            _Plugin = nBanagrarioDesktopPlugin

        End Sub

#End Region

#Region " Eventos "

        Private Sub BuscarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BuscarButton.Click
            Buscar()
        End Sub

        Private Sub CorreccionLlavesDataGridView_CellMouseDoubleClick(ByVal sender As System.Object, ByVal e As DataGridViewCellMouseEventArgs) Handles CorreccionLlavesDataGridView.CellMouseDoubleClick

            Dim f As FormCorrecciones
            f = New FormCorrecciones(Me._Plugin)
            f.Lblfk_Proceso.Text = CorreccionLlavesDataGridView.Rows(e.RowIndex).Cells(0).Value.ToString()
            f.LblCodOfiAnterior.Text = CorreccionLlavesDataGridView.Rows(e.RowIndex).Cells(2).Value.ToString()
            f.LblFecMovAnterior.Text = CorreccionLlavesDataGridView.Rows(e.RowIndex).Cells(4).Value.ToString()
            f.LblValorCargue.Text = CorreccionLlavesDataGridView.Rows(e.RowIndex).Cells(5).Value.ToString()
            f.LblValorPaquete.Text = CorreccionLlavesDataGridView.Rows(e.RowIndex).Cells(6).Value.ToString()
            f.LblValorContenedor.Text = CorreccionLlavesDataGridView.Rows(e.RowIndex).Cells(7).Value.ToString()

            f.ShowDialog()
            'Recargar DataGridView
            Buscar()

        End Sub

#End Region

#Region " Metodos "

        Private Sub Buscar()

            If (Validar()) Then

                Dim dbmAgrario As DBAgrarioDataBaseManager = Nothing

                Try
                    dbmAgrario = New DBAgrarioDataBaseManager(Me._Plugin.BancoAgrarioConnectionString)

                    dbmAgrario.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                    Dim fecha As String = FechaProcesodateTimePicker.Value.ToString("yyyy/MM/dd")

                    Dim correccionLlavesDataTable = New CTA_Get_Error_LlavesDataTable
                    correccionLlavesDataTable.fk_ProcesoColumn.DataType = GetType(String)
                    correccionLlavesDataTable.Nombre_COBColumn.DataType = GetType(String)
                    correccionLlavesDataTable.fk_OficinaColumn.DataType = GetType(String)
                    correccionLlavesDataTable.Nombre_OficinaColumn.DataType = GetType(String)
                    correccionLlavesDataTable.Fecha_MovimientoColumn.DataType = GetType(String)
                    'CorreccionLlavesDataTable.fk_CargueColumn.DataType = GetType(String)
                    'CorreccionLlavesDataTable.fk_Cargue_PaqueteColumn.DataType = GetType(String)
                    correccionLlavesDataTable.Codigo_ContenedorColumn.DataType = GetType(String)
                    correccionLlavesDataTable.Cant_Soporte_SobrantesColumn.DataType = GetType(String)
                    'CorreccionLlavesDataTable.CorregidoColumn.DataType = GetType(String)

                    Dim data = dbmAgrario.SchemaProcess.PA_Get_Error_Llaves.DBExecute(fecha)

                    If data.Count > 0 Then

                        For Each Fila In data

                            correccionLlavesDataTable.Rows.Add(Fila.ItemArray)

                        Next

                        CorreccionLlavesDataGridView.DataSource = correccionLlavesDataTable
                        CorreccionLlavesDataGridView.ClearSelection()
                        MessageBox.Show("Se encontraron " & correccionLlavesDataTable.Count & " registros para la fecha de proceso seleccionada", "Correcion llaves", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    Else

                        MessageBox.Show("No se encontraron registros para la fecha de proceso seleccionada", "Correccion de llaves", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    End If

                Catch ex As Exception
                    MessageBox.Show(ex.Message, " Modulo Correcciones Cargue", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    If (dbmAgrario IsNot Nothing) Then dbmAgrario.Connection_Close()
                End Try

            End If

        End Sub

#End Region

#Region " Funciones "

        Private Function Validar() As Boolean

            Dim fechaSeleccionada = New DateTime(FechaProcesodateTimePicker.Value.Year, FechaProcesodateTimePicker.Value.Month, FechaProcesodateTimePicker.Value.Day)
            Dim fechaHoy = New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)

            If fechaSeleccionada > fechaHoy Then

                DesktopMessageBoxControl.DesktopMessageShow("La Fecha de proceso no puede ser mayor al día de hoy", "Seleccione una fecha válida", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                Return False

            Else

                Return True

            End If


        End Function

#End Region
        
    End Class

End Namespace