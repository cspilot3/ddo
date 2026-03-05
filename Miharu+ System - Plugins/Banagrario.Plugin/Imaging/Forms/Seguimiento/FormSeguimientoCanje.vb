Imports System.Windows.Forms
Imports DBAgrario
Imports DBAgrario.SchemaProcess

Namespace Imaging.Forms.Seguimiento

    Public Class FormSeguimientoCanje

#Region " Declaraciones "

        Private _Plugin As BanagrarioImagingPlugin

#End Region

#Region " Contructores "

        Public Sub New(ByVal nBanagrarioDesktopPlugin As BanagrarioImagingPlugin)
            InitializeComponent()

            _Plugin = nBanagrarioDesktopPlugin

        End Sub

#End Region

#Region " Eventos "

        Private Sub FormSeguimientoCanje_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            LoadConfig()
        End Sub

        Private Sub BuscarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BuscarButton.Click
            Buscar()
        End Sub

#End Region

#Region " Metodos "

        Private Sub LoadConfig()
            Dim NewDate = Now

            FechaInicialDateTimePicker.Value = NewDate
            Me.ColumnFecha_Movimiento.Visible = True
            Me.ColumnCiudad.Visible = True
            Me.ColumnCantidad_a_Transmitir.Visible = True
            Me.ColumnCantidad_Transmitida.Visible = True

        End Sub


        Private Sub Buscar()
            If (Validar()) Then
                Dim dbmAgrario As DBAgrarioDataBaseManager = Nothing

                Try
                    dbmAgrario = New DBAgrarioDataBaseManager(Me._Plugin.BancoAgrarioConnectionString)

                    dbmAgrario.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                    Dim FechaInicial As New DateTime(FechaInicialDateTimePicker.Value.Year, FechaInicialDateTimePicker.Value.Month, FechaInicialDateTimePicker.Value.Day)
                    Dim FechaFinal As New DateTime(FechaFinalDateTimePicker.Value.Year, FechaFinalDateTimePicker.Value.Month, FechaFinalDateTimePicker.Value.Day, 23, 59, 59)

                    Dim CanjeSeguimientoDataTable = New CTA_Get_Consolidado_Registros_CanjeDataTable()
                    CanjeSeguimientoDataTable.Fecha_ProcesoColumn.DataType = GetType(String)
                    CanjeSeguimientoDataTable.Nombre_ConexionColumn.DataType = GetType(String)
                    CanjeSeguimientoDataTable.Cantidad_TransmitirColumn.DataType = GetType(String)
                    CanjeSeguimientoDataTable.Cantidad_TransmitidaColumn.DataType = GetType(String)

                    Dim Data As CTA_Get_Consolidado_Registros_CanjeDataTable
                    Data = dbmAgrario.SchemaProcess.PA_Canje_Seguimiento_get.DBExecute(FechaInicial, FechaFinal)

                    If Data.Count > 0 Then
                        For Each fila In Data

                            CanjeSeguimientoDataTable.Rows.Add(fila.ItemArray)

                        Next

                        CanjeDatosDataGridView.DataSource = CanjeSeguimientoDataTable
                        CanjeDatosDataGridView.ClearSelection()

                        MessageBox.Show("Se encontraron " & CanjeSeguimientoDataTable.Count & " registros para el rango de fechas seleccionado", "Seguimiento Canje", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    Else

                        MessageBox.Show("No Se encontraron registros para el rango de fechas seleccionado", "Seguimiento Canje", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    End If


                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Consolidado Canje", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    If (dbmAgrario IsNot Nothing) Then dbmAgrario.Connection_Close()
                End Try
            End If

        End Sub
#End Region

#Region " Funciones "

        Private Function Validar() As Boolean
            Dim FechaInicial = New DateTime(FechaInicialDateTimePicker.Value.Year, FechaInicialDateTimePicker.Value.Month, FechaInicialDateTimePicker.Value.Day)
            Dim FechaFinal = New DateTime(FechaFinalDateTimePicker.Value.Year, FechaFinalDateTimePicker.Value.Month, FechaFinalDateTimePicker.Value.Day)

            If (FechaInicial > FechaFinal) Then
                MessageBox.Show("La fecha final no puede ser inferior a la fecha inicial", "Consolidado Canje", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                Return True
            End If

            Return False
        End Function

#End Region

    End Class

End Namespace