Imports System.Windows.Forms
Imports DBAgrario
Imports DBAgrario.SchemaProcess
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library.Config

Namespace Imaging.Forms.Correciones

    Public Class FormCorrecciones

#Region " Declaraciones "

        Private _plugin As BanagrarioImagingPlugin
        Dim _tipoCambio As Integer = 0

#End Region

#Region " Constructores "

        Public Sub New(ByVal nBanagrarioDesktopPlugin As BanagrarioImagingPlugin)

            InitializeComponent()
            _plugin = nBanagrarioDesktopPlugin

        End Sub

#End Region

#Region " Eventos "

        Private Sub BtnGuardar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BtnGuardar.Click
            Guardar()
        End Sub

        Private Sub FormCorrecciones_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

            Dim dbmAgrario As DBAgrarioDataBaseManager = Nothing

            Try

                dbmAgrario = New DBAgrarioDataBaseManager(Me._plugin.BancoAgrarioConnectionString)

                dbmAgrario.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                Dim entidadProcesamiento As Short = _plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad
                Dim sedeProcesamiento As Short = _plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Sede

                Dim oficinaData = dbmAgrario.SchemaProcess.CTA_Oficina_Concatenacion.DBFindByfk_Entidadfk_Sede(entidadProcesamiento, sedeProcesamiento, 0, New CTA_Oficina_ConcatenacionEnumList(CTA_Oficina_ConcatenacionEnum.id_Oficina, True))
                Dim ofiView = oficinaData.DefaultView

                Utilities.LlenarCombo(OficinaDesktopComboBox, ofiView.ToTable(), CTA_Oficina_ConcatenacionEnum.id_Oficina.ColumnName, CTA_Oficina_ConcatenacionEnum.Nombre_Oficina.ColumnName)
                OficinaDesktopComboBox.SelectedIndex = -1

            Catch ex As Exception
                MessageBox.Show(ex.Message, " Correcciones Llaves", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally

                If (dbmAgrario IsNot Nothing) Then dbmAgrario.Connection_Close()

            End Try



        End Sub

#End Region

#Region " Metodos "

        Private Sub Guardar()

            If (Validar()) Then

                Dim dbmAgrario As DBAgrarioDataBaseManager = Nothing

                Try
                    dbmAgrario = New DBAgrarioDataBaseManager(Me._plugin.BancoAgrarioConnectionString)

                    dbmAgrario.Connection_Open(_plugin.Manager.Sesion.Usuario.id)
                    ''dbmAgrario.DataBase.Identifier_Date_Format = _plugin.Manager.DesktopGlobal.IdentifierDateFormat


                    'Tipo de cambio: Oficina (1), Fecha de Movimiento(2) o ambas(3)
                    If CBFechaMovimiento.Checked = True And CBCodigoOficina.Checked = True Then
                        _tipoCambio = 3
                    ElseIf CBFechaMovimiento.Checked = True Then
                        _tipoCambio = 2
                    ElseIf CBCodigoOficina.Checked = True Then
                        _tipoCambio = 1
                    End If



                    dbmAgrario.SchemaProcess.PA_LLaves_00_Correccion_Error.DBExecute(Integer.Parse(Lblfk_Proceso.Text), _
                                                                                     Integer.Parse(LblValorCargue.Text), _
                                                                                     Short.Parse(LblValorPaquete.Text), _
                                                                                     LblValorContenedor.Text, _
                                                                                     FechaProcesodateTimePicker.Value.ToString("yyyy/MM/dd"), _
                                                                                     LblFecMovAnterior.Text, _
                                                                                     Integer.Parse(OficinaDesktopComboBox.SelectedValue.ToString()), _
                                                                                     Integer.Parse(LblCodOfiAnterior.Text), _
                                                                                     _tipoCambio)


                    If _tipoCambio = 3 Then

                        dbmAgrario.SchemaAudit.PA_Inserccion_Log_Cambio_Error_Llaves.DBExecute(Long.Parse(Lblfk_Proceso.Text), _
                                                                                               Integer.Parse(LblValorCargue.Text), _
                                                                                               Short.Parse(LblValorPaquete.Text), _
                                                                                               1,
                                                                                               LblValorContenedor.Text, _
                                                                                               RdbFuncionariopyc.Checked, _
                                                                                               _plugin.Manager.Sesion.Usuario.id, _
                                                                                               LblCodOfiAnterior.Text, _
                                                                                               OficinaDesktopComboBox.SelectedValue.ToString)

                        dbmAgrario.SchemaAudit.PA_Inserccion_Log_Cambio_Error_Llaves.DBExecute(Long.Parse(Lblfk_Proceso.Text), _
                                                                                               Integer.Parse(LblValorCargue.Text), _
                                                                                               Short.Parse(LblValorPaquete.Text), _
                                                                                               2,
                                                                                               LblValorContenedor.Text, _
                                                                                               RdbFuncionariopyc.Checked, _
                                                                                               _plugin.Manager.Sesion.Usuario.id, _
                                                                                               LblFecMovAnterior.Text, _
                                                                                               FechaProcesodateTimePicker.Value.ToString("yyyy/MM/dd"))
                    ElseIf _tipoCambio = 2 Then

                        dbmAgrario.SchemaAudit.PA_Inserccion_Log_Cambio_Error_Llaves.DBExecute(Long.Parse(Lblfk_Proceso.Text), _
                                                                                               Integer.Parse(LblValorCargue.Text), _
                                                                                               Short.Parse(LblValorPaquete.Text), _
                                                                                               2,
                                                                                               LblValorContenedor.Text, _
                                                                                               RdbFuncionariopyc.Checked, _
                                                                                               _plugin.Manager.Sesion.Usuario.id, _
                                                                                               LblFecMovAnterior.Text, _
                                                                                               FechaProcesodateTimePicker.Value.ToString("yyyy/MM/dd"))

                    ElseIf _tipoCambio = 1 Then

                        dbmAgrario.SchemaAudit.PA_Inserccion_Log_Cambio_Error_Llaves.DBExecute(Long.Parse(Lblfk_Proceso.Text), _
                                                                                               Integer.Parse(LblValorCargue.Text), _
                                                                                               Short.Parse(LblValorPaquete.Text), _
                                                                                               1,
                                                                                               LblValorContenedor.Text, _
                                                                                               RdbFuncionariopyc.Checked, _
                                                                                               _plugin.Manager.Sesion.Usuario.id, _
                                                                                               LblCodOfiAnterior.Text, _
                                                                                               OficinaDesktopComboBox.SelectedValue.ToString)

                    End If






                    Dim updateErrorLlaves = New TBL_Error_LlavesType()
                    'UpdateErrorLlaves.Corregido = CType(1, Global.Slyg.Tools.SlygNullable(Of Boolean))
                    dbmAgrario.SchemaProcess.TBL_Error_Llaves.DBUpdate(updateErrorLlaves, Integer.Parse(LblValorCargue.Text), Short.Parse(LblValorPaquete.Text))

                    MessageBox.Show("Se guardó la correccion exitosamente", "Correcion llaves", MessageBoxButtons.OK, MessageBoxIcon.Information)


                Catch ex As Exception
                    MessageBox.Show(ex.Message, " Correcciones Llaves", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    If (dbmAgrario IsNot Nothing) Then dbmAgrario.Connection_Close()
                    Me.Close()
                End Try



            End If


        End Sub

#End Region

#Region " Funciones "

        Private Function Validar() As Boolean

            Dim fechaSeleccionada = New DateTime(FechaProcesodateTimePicker.Value.Year, FechaProcesodateTimePicker.Value.Month, FechaProcesodateTimePicker.Value.Day)
            Dim fechaHoy = New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)


            If CBFechaMovimiento.Checked = True Then
                If fechaSeleccionada > fechaHoy Then

                    DesktopMessageBoxControl.DesktopMessageShow("La Fecha de proceso no puede ser mayor al día de hoy", "Seleccione una fecha válida", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                    Return False

                ElseIf fechaSeleccionada.ToString("yyyyMMdd") < "20110811" Then

                    DesktopMessageBoxControl.DesktopMessageShow("La Fecha de proceso no puede ser menor al inicio de operación del Banco (2011/08/11)", "Seleccione una fecha válida", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                    Return False

                End If
            End If

            If CBCodigoOficina.Checked = True Then

                If OficinaDesktopComboBox.SelectedValue Is "-1" Then

                    DesktopMessageBoxControl.DesktopMessageShow("El Código de oficina debe ser diferente a 'Seleccionar' ", "Seleccione una oficina válida", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                    Return False

                End If

            End If


            Return True


        End Function


#End Region

    End Class

End Namespace