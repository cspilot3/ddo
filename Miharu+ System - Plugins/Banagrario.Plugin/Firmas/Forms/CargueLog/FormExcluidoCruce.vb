Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library.Config

Namespace Firmas.Forms.CargueLog


    Public Class FormExcluidoCruce

#Region " Declaraciones "

        Private _Plugin As FirmasImagingPlugin
        Private _RegionalDataTable As DBAgrario.SchemaConfig.TBL_RegionalDataTable
        Private _COBDataTable As DBAgrario.SchemaConfig.TBL_COBDataTable
        Private _OficinaDataTable As DBAgrario.SchemaConfig.TBL_OficinaDataTable
        Private _TransaccionesDataTable As DBAgrario.SchemaFirmas.TBL_TransaccionDataTable

#End Region

#Region " Propiedades "

#End Region

#Region " Constructores "
        Public Sub New(ByVal nBanagrarioImaginPlugin As FirmasImagingPlugin)
            InitializeComponent()
            _Plugin = nBanagrarioImaginPlugin
            CargaTablas()
        End Sub


#End Region

#Region " Eventos "

        Private Sub ExcluirButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ExcluirButton.Click
            ExcluirTransaccionesCruce()
        End Sub

        Private Sub FormExcluidoLog_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            CargaRegional()
            CargaTransacciones()
        End Sub

        Private Sub RegionalDesktopComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles RegionalDesktopComboBox.SelectedIndexChanged
            CargaCOB()
        End Sub

        Private Sub COBDesktopComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles COBDesktopComboBox.SelectedIndexChanged
            CargaOficina()
        End Sub

        Private Sub CancelarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CancelarButton.Click
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
        End Sub

#End Region

#Region " Metodos "

        Private Sub CargaTransacciones()
            Try
                Utilities.LlenarCombo(TransaccionesDesktopComboBox, _TransaccionesDataTable, DBAgrario.SchemaFirmas.TBL_TransaccionEnum.Codigo_Transaccion.ColumnName, DBAgrario.SchemaFirmas.TBL_TransaccionEnum.Nombre_Transaccion.ColumnName, True, "-1", "--TODOS--")
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaFiltros", ex)
            End Try
        End Sub

        Private Sub CargaRegional()
            Try
                Utilities.LlenarCombo(RegionalDesktopComboBox, _RegionalDataTable, DBAgrario.SchemaConfig.TBL_RegionalEnum.id_Regional.ColumnName, DBAgrario.SchemaConfig.TBL_RegionalEnum.Nombre_Regional.ColumnName, True, "-1", "--TODOS--")
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaFiltros", ex)
            End Try
        End Sub

        Private Sub CargaTablas()
            Dim dmBanAgrario As New DBAgrario.DBAgrarioDataBaseManager(Me._Plugin.BancoAgrarioConnectionString)
            Try

                dmBanAgrario.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                _RegionalDataTable = dmBanAgrario.SchemaConfig.TBL_Regional.DBGet(Nothing)
                _COBDataTable = dmBanAgrario.SchemaConfig.TBL_COB.DBGet(Nothing)
                _OficinaDataTable = dmBanAgrario.SchemaConfig.TBL_Oficina.DBGet(Nothing)
                _TransaccionesDataTable = dmBanAgrario.SchemaFirmas.TBL_Transaccion.DBGet(Nothing)

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaTablas", ex)
            Finally
                dmBanAgrario.Connection_Close()
            End Try
        End Sub

        Private Sub CargaCOB()
            Try
                Dim COBView = _COBDataTable.DefaultView
                COBView.RowFilter = "fk_Regional = " + RegionalDesktopComboBox.SelectedValue.ToString()
                Utilities.LlenarCombo(COBDesktopComboBox, COBView.ToTable(), DBAgrario.SchemaConfig.TBL_COBEnum.id_COB.ColumnName, DBAgrario.SchemaConfig.TBL_COBEnum.Nombre_COB.ColumnName, True, "-1", "--TODOS--")

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaCOB", ex)
            End Try
        End Sub

        Private Sub CargaOficina()
            Try
                Dim OficinaView = _OficinaDataTable.DefaultView
                OficinaView.RowFilter = "fk_COB = " + COBDesktopComboBox.SelectedValue.ToString()

                Utilities.LlenarCombo(OficinaDesktopComboBox, OficinaView.ToTable(), DBAgrario.SchemaConfig.TBL_OficinaEnum.id_Oficina.ColumnName, DBAgrario.SchemaConfig.TBL_OficinaEnum.Nombre_Oficina.ColumnName, True, "-1", "--TODOS--")

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaOficina", ex)
            End Try
        End Sub

        Private Sub ExcluirTransaccionesCruce()
            Dim FechaProceso = FechaProcesoPicker.Value.ToString("yyyyMMdd")
            If Validar() Then
                If MsgBox("Está seguro de excluir la información del cruce contra el log ?", MsgBoxStyle.YesNo, "Excluidos Cruce") = MsgBoxResult.Yes Then
                    Dim dbmAgrario As DBAgrario.DBAgrarioDataBaseManager = Nothing
                    ' Carga Log correspondiente a la fecha de proceso seleccionada
                    Try
                        dbmAgrario = New DBAgrario.DBAgrarioDataBaseManager(_Plugin.BancoAgrarioConnectionString)
                        dbmAgrario.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
                        Dim data = dbmAgrario.SchemaFirmas.PA_AdministrarProcesoFirmas.DBExecute(FechaProceso, OficinaDesktopComboBox.SelectedValue.ToString, TransaccionesDesktopComboBox.SelectedValue.ToString, NumeroCuentaTextbox.Text.Trim)
                        If data.Rows.Count > 0 Then
                            Dim Excluidos As String
                            Excluidos = Trim(data.Rows(0).Item("RegistrosActualizados").ToString())
                            If Not Excluidos = "0" Then
                                DesktopMessageBoxControl.DesktopMessageShow("Se realizó el proceso correctamente, se excluyeron " & Excluidos & " transacciones.", "Excluidos del Cruce", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                            Else
                                DesktopMessageBoxControl.DesktopMessageShow("No existen transacciones a excluir según los datos ingresados ", "Excluidos del Cruce", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                            End If
                        End If
                    Catch ex As Exception
                        If dbmAgrario IsNot Nothing Then dbmAgrario.Transaction_Rollback()
                        DesktopMessageBoxControl.DesktopMessageShow("Validar Cargue Archivo Log", ex)
                    Finally
                        If dbmAgrario IsNot Nothing Then dbmAgrario.Connection_Close()
                    End Try
                    Me.DialogResult = Windows.Forms.DialogResult.OK
                    Me.Hide()
                End If
            End If

        End Sub

#End Region

#Region " Funciones "

        Private Function Validar() As Boolean

            If NumeroCuentaTextbox.Text.Trim = "" Then
                DesktopMessageBoxControl.DesktopMessageShow("Debe seleccionar un Numero de Cuenta", "Excuidos Cruce", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                FechaProcesoPicker.Focus()
                Return False
            End If

            If FechaProcesoPicker.Value.ToString = "" Then
                DesktopMessageBoxControl.DesktopMessageShow("Debe seleccionar una fecha de Movimiento", "Excuidos Cruce", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                FechaProcesoPicker.Focus()
                Return False
            End If

            Return True
        End Function

#End Region

    End Class
End Namespace