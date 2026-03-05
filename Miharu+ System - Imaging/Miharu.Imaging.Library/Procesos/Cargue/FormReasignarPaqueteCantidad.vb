Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Controls

Namespace Procesos.Cargue

    Public Class FormReasignarPaqueteCantidad

#Region " Declaraciones "

        Private _SedeProcesamientoDataTable As DBSecurity.SchemaConfig.CTA_Sedes_CentrosProcesamientoDataTable
        Private _CentroProcesamientoDataTable As DBSecurity.SchemaConfig.TBL_Centro_ProcesamientoDataTable

        Private _seguimientoRow As DBImaging.SchemaProcess.CTA_SeguimientoRow

#End Region

#Region " Constructores "
        Sub New()

            ' This call is required by the designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            CargaTablas()
        End Sub

        Public Sub ConfigForm(nSeguimientoRow As DBImaging.SchemaProcess.CTA_SeguimientoRow)
            _seguimientoRow = nSeguimientoRow

            IndexacionPanel.Visible = Program.ImagingGlobal.ProyectoImagingRow.Seguimiento_Show_Col_Indexacion
            RetenidosPanel.Visible = Program.ImagingGlobal.ProyectoImagingRow.Seguimiento_Show_Col_Retenido
            PreCapturaPanel.Visible = Program.ImagingGlobal.ProyectoImagingRow.Seguimiento_Show_Col_PreCaptura
            CapturaPanel.Visible = Program.ImagingGlobal.ProyectoImagingRow.Seguimiento_Show_Col_PrimeraCaptura
            SegundaCapturaPanel.Visible = Program.ImagingGlobal.ProyectoImagingRow.Seguimiento_Show_Col_SegundaCaptura
            TerceraCapturaPanel.Visible = Program.ImagingGlobal.ProyectoImagingRow.Seguimiento_Show_Col_TerceraCaptura
            CalidadPanel.Visible = Program.ImagingGlobal.ProyectoImagingRow.Seguimiento_Show_Col_Calidad
            ReprocesoPanel.Visible = Program.ImagingGlobal.ProyectoImagingRow.Seguimiento_Show_Col_Reproceso
            ValidacionesPanel.Visible = Program.ImagingGlobal.ProyectoImagingRow.Seguimiento_Show_Col_Validaciones_Pendientes
            ValidacionListasPanel.Visible = Program.ImagingGlobal.ProyectoImagingRow.Seguimiento_Show_Col_Validacion_Listas


            If _seguimientoRow.Indexacion > 0 Then
                IndexacionNumericUpDown.Maximum = _seguimientoRow.Indexacion
                IndexacionNumericUpDown.Value = _seguimientoRow.Indexacion
            Else
                IndexacionPanel.Enabled = False
            End If

            If _seguimientoRow.Retenido > 0 Then
                RetenidosNumericUpDown.Maximum = _seguimientoRow.Retenido
                RetenidosNumericUpDown.Value = _seguimientoRow.Retenido
            Else
                RetenidosPanel.Enabled = False
            End If

            If _seguimientoRow.Pre_Captura > 0 Then
                PreCapturaNumericUpDown.Maximum = _seguimientoRow.Pre_Captura
                PreCapturaNumericUpDown.Value = _seguimientoRow.Pre_Captura
            Else
                PreCapturaPanel.Enabled = False
            End If

            If _seguimientoRow.Captura > 0 Then
                CapturaNumericUpDown.Maximum = _seguimientoRow.Captura
                CapturaNumericUpDown.Value = _seguimientoRow.Captura
            Else
                CapturaPanel.Enabled = False
            End If

            If _seguimientoRow.Segunda_Captura > 0 Then
                SegundaCapturaNumericUpDown.Maximum = _seguimientoRow.Segunda_Captura
                SegundaCapturaNumericUpDown.Value = _seguimientoRow.Segunda_Captura
            Else
                SegundaCapturaPanel.Enabled = False
            End If

            If _seguimientoRow.Tercera_Captura > 0 Then
                TerceraCapturaNumericUpDown.Maximum = _seguimientoRow.Tercera_Captura
                TerceraCapturaNumericUpDown.Value = _seguimientoRow.Tercera_Captura
            Else
                TerceraCapturaPanel.Enabled = False
            End If

            If _seguimientoRow.Calidad > 0 Then
                CalidadNumericUpDown.Maximum = _seguimientoRow.Calidad
                CalidadNumericUpDown.Value = _seguimientoRow.Calidad
            Else
                CalidadPanel.Enabled = False
            End If

            If _seguimientoRow.Reproceso > 0 Then
                ReprocesoNumericUpDown.Maximum = _seguimientoRow.Reproceso
                ReprocesoNumericUpDown.Value = _seguimientoRow.Reproceso
            Else
                ReprocesoPanel.Enabled = False
            End If

            If _seguimientoRow.Validaciones > 0 Then
                ValidacionesNumericUpDown.Maximum = _seguimientoRow.Validaciones
                ValidacionesNumericUpDown.Value = _seguimientoRow.Validaciones
            Else
                ValidacionesPanel.Enabled = False
            End If

            If _seguimientoRow.Validacion_Listas > 0 Then
                ValidacionListasNumericUpDown.Maximum = _seguimientoRow.Validacion_Listas
                ValidacionListasNumericUpDown.Value = _seguimientoRow.Validacion_Listas
            Else
                ValidacionListasPanel.Enabled = False
            End If

        End Sub

#End Region

#Region " Eventos "

        Private Sub FormReasignarPaqueteCantidad_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            CargaSede()
        End Sub

        Private Sub SedeProcesamientoDesktopComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles SedeProcesamientoDesktopComboBox.SelectedIndexChanged
            CargaCentroPorcesamiento()
            AsignarCargueButton.Enabled = (SedeProcesamientoDesktopComboBox.SelectedIndex <> 0 And CentroProcesamientoDesktopComboBox.SelectedIndex <> 0)
        End Sub

        Private Sub CentroProcesamientoDesktopComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles CentroProcesamientoDesktopComboBox.SelectedIndexChanged
            AsignarCargueButton.Enabled = (SedeProcesamientoDesktopComboBox.SelectedIndex <> 0 And CentroProcesamientoDesktopComboBox.SelectedIndex <> 0)
        End Sub

        Private Sub AsignarCargueButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AsignarCargueButton.Click
            AsignarCargue()
        End Sub

#End Region

#Region " Metodos "

        Private Sub CargaSede()
            Try
                Utilities.LlenarCombo(SedeProcesamientoDesktopComboBox, _SedeProcesamientoDataTable, DBSecurity.SchemaConfig.TBL_SedeEnum.id_Sede.ColumnName, DBSecurity.SchemaConfig.TBL_SedeEnum.Nombre_Sede.ColumnName, True, "-1", "--Seleccione...--")
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaSede", ex)
            End Try
        End Sub

        Private Sub CargaCentroPorcesamiento()
            Try
                Dim CentroProcesamientoView = _CentroProcesamientoDataTable.DefaultView
                CentroProcesamientoView.RowFilter = "fk_Sede = " + CShort(SedeProcesamientoDesktopComboBox.SelectedValue).ToString()

                Utilities.LlenarCombo(CentroProcesamientoDesktopComboBox, CentroProcesamientoView.ToTable(), DBSecurity.SchemaConfig.TBL_Centro_ProcesamientoEnum.id_Centro_Procesamiento.ColumnName, DBSecurity.SchemaConfig.TBL_Centro_ProcesamientoEnum.Nombre_Centro_Procesamiento.ColumnName, True, "-1", "Seleccione...")
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaCentroPorcesamiento", ex)
            End Try
        End Sub

        Private Sub CargaTablas()
            Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Security)
            Try

                dbmSecurity.Connection_Open(Program.Sesion.Usuario.id)

                _SedeProcesamientoDataTable = dbmSecurity.SchemaConfig.CTA_Sedes_CentrosProcesamiento.DBFindByfk_Entidad(Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad)
                _CentroProcesamientoDataTable = dbmSecurity.SchemaConfig.TBL_Centro_Procesamiento.DBGet(Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, Nothing, Nothing)

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaTablas", ex)
            Finally
                dbmSecurity.Connection_Close()
            End Try
        End Sub

        Private Sub AsignarCargue()
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)

                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                If (SedeProcesamientoDesktopComboBox.SelectedIndex <> -1 And CentroProcesamientoDesktopComboBox.SelectedIndex <> -1) Then

                    If IndexacionNumericUpDown.Value > 0 Then
                        dbmImaging.SchemaProcess.PA_Dashboard_Reasignar_Paquete_Top.DBExecute(_seguimientoRow.id_OT, _seguimientoRow.idSedeAsignado, _seguimientoRow.idCentroAsignado, CShort(SedeProcesamientoDesktopComboBox.SelectedValue), CShort(CentroProcesamientoDesktopComboBox.SelectedValue), CInt(IndexacionNumericUpDown.Value))
                    End If

                    If RetenidosNumericUpDown.Value > 0 Then
                        dbmImaging.SchemaProcess.PA_Dashboard_Reasignar_Captura_Top.DBExecute(_seguimientoRow.id_OT, _seguimientoRow.idSedeAsignado, _seguimientoRow.idCentroAsignado, CShort(SedeProcesamientoDesktopComboBox.SelectedValue), CShort(CentroProcesamientoDesktopComboBox.SelectedValue), CShort(29), CInt(RetenidosNumericUpDown.Value))
                    End If

                    If PreCapturaNumericUpDown.Value > 0 Then
                        dbmImaging.SchemaProcess.PA_Dashboard_Reasignar_Captura_Top.DBExecute(_seguimientoRow.id_OT, _seguimientoRow.idSedeAsignado, _seguimientoRow.idCentroAsignado, CShort(SedeProcesamientoDesktopComboBox.SelectedValue), CShort(CentroProcesamientoDesktopComboBox.SelectedValue), CShort(32), CInt(PreCapturaNumericUpDown.Value))
                    End If

                    If CapturaNumericUpDown.Value > 0 Then
                        dbmImaging.SchemaProcess.PA_Dashboard_Reasignar_Captura_Top.DBExecute(_seguimientoRow.id_OT, _seguimientoRow.idSedeAsignado, _seguimientoRow.idCentroAsignado, CShort(SedeProcesamientoDesktopComboBox.SelectedValue), CShort(CentroProcesamientoDesktopComboBox.SelectedValue), CShort(33), CInt(CapturaNumericUpDown.Value))
                    End If

                    If SegundaCapturaNumericUpDown.Value > 0 Then
                        dbmImaging.SchemaProcess.PA_Dashboard_Reasignar_Captura_Top.DBExecute(_seguimientoRow.id_OT, _seguimientoRow.idSedeAsignado, _seguimientoRow.idCentroAsignado, CShort(SedeProcesamientoDesktopComboBox.SelectedValue), CShort(CentroProcesamientoDesktopComboBox.SelectedValue), CShort(34), CInt(SegundaCapturaNumericUpDown.Value))
                    End If

                    If TerceraCapturaNumericUpDown.Value > 0 Then
                        dbmImaging.SchemaProcess.PA_Dashboard_Reasignar_Captura_Top.DBExecute(_seguimientoRow.id_OT, _seguimientoRow.idSedeAsignado, _seguimientoRow.idCentroAsignado, CShort(SedeProcesamientoDesktopComboBox.SelectedValue), CShort(CentroProcesamientoDesktopComboBox.SelectedValue), CShort(35), CInt(TerceraCapturaNumericUpDown.Value))
                    End If

                    If CalidadNumericUpDown.Value > 0 Then
                        dbmImaging.SchemaProcess.PA_Dashboard_Reasignar_Captura_Top.DBExecute(_seguimientoRow.id_OT, _seguimientoRow.idSedeAsignado, _seguimientoRow.idCentroAsignado, CShort(SedeProcesamientoDesktopComboBox.SelectedValue), CShort(CentroProcesamientoDesktopComboBox.SelectedValue), CShort(36), CInt(CalidadNumericUpDown.Value))
                    End If

                    If ReprocesoNumericUpDown.Value > 0 Then
                        dbmImaging.SchemaProcess.PA_Dashboard_Reasignar_Captura_Top.DBExecute(_seguimientoRow.id_OT, _seguimientoRow.idSedeAsignado, _seguimientoRow.idCentroAsignado, CShort(SedeProcesamientoDesktopComboBox.SelectedValue), CShort(CentroProcesamientoDesktopComboBox.SelectedValue), CShort(2), CInt(ReprocesoNumericUpDown.Value))
                    End If

                    If ValidacionesNumericUpDown.Value > 0 Then
                        dbmImaging.SchemaProcess.PA_Dashboard_Reasignar_Validacion_Top.DBExecute(_seguimientoRow.id_OT, _seguimientoRow.idSedeAsignado, _seguimientoRow.idCentroAsignado, CShort(SedeProcesamientoDesktopComboBox.SelectedValue), CShort(CentroProcesamientoDesktopComboBox.SelectedValue), CInt(ValidacionesNumericUpDown.Value))
                    End If

                    If ValidacionListasNumericUpDown.Value > 0 Then
                        dbmImaging.SchemaProcess.PA_Dashboard_Reasignar_Validacion_Listas_Top.DBExecute(_seguimientoRow.id_OT, _seguimientoRow.idSedeAsignado, _seguimientoRow.idCentroAsignado, CShort(SedeProcesamientoDesktopComboBox.SelectedValue), CShort(CentroProcesamientoDesktopComboBox.SelectedValue), CInt(ValidacionListasNumericUpDown.Value))
                    End If

                    If CorreccionCapturaNumericUpDown.Value > 0 Then
                        dbmImaging.SchemaProcess.PA_Dashboard_Reasignar_Captura_Top.DBExecute(_seguimientoRow.id_OT, _seguimientoRow.idSedeAsignado, _seguimientoRow.idCentroAsignado, CShort(SedeProcesamientoDesktopComboBox.SelectedValue), CShort(CentroProcesamientoDesktopComboBox.SelectedValue), CShort(46), CInt(CorreccionCapturaNumericUpDown.Value))
                    End If


                    DesktopMessageBoxControl.DesktopMessageShow("Se realizó la asignación de cargues satisfactoriamente", "Asignación de Cargue", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                    DialogResult = DialogResult.OK
                Else
                    DesktopMessageBoxControl.DesktopMessageShow("Debe seleccionar la Sede y el Centro de Procesamiento", "Asignación de Cargue", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("AsignarCargue", ex)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

#End Region

    End Class

End Namespace