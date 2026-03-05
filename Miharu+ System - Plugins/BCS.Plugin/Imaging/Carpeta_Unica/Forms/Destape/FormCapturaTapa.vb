Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports System.Windows.Forms
Imports Slyg.Tools
Imports Miharu.Desktop.Library.Config
Imports Slyg.Tools.Progress
Imports System.Text
Imports Microsoft.Reporting.WinForms
Imports DBIntegration

Namespace Imaging.Carpeta_Unica.Forms.Destape
    Public Class FormCapturaTapa

#Region " Declaraciones "

        Private _Plugin As CarpetaUnicaPlugin
        Private _ObservacionesDataTable As DBIntegration.SchemaBCSCarpetaUnica.CTA_ObservacionesDataTable
        Private _OficinasDataTable As DBIntegration.SchemaBCSCarpetaUnica.CTA_OficinasDataTable
        Private _nidOt As Integer
        Private _nidPrecinto As Integer
        Private _nidContenedor As Integer
        Private _Tipo_Proceso As Integer = 0

#End Region

#Region " Contructores "

        Public Sub New(ByVal nCarpetaUnicaDesktopPlugin As CarpetaUnicaPlugin, ByVal nidOt As Integer, ByVal nidPrecinto As Integer, ByVal nidContenedor As Integer)
            InitializeComponent()

            _Plugin = nCarpetaUnicaDesktopPlugin
            _nidOt = nidOt
            _nidPrecinto = nidPrecinto
            _nidContenedor = nidContenedor
            CargaTablas()
        End Sub

#End Region

#Region " Eventos "

        Private Sub FormCapturaTapa_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            CargaProceso()
            CargaOficinas(_nidOt, _nidPrecinto, _nidContenedor)
            CargaObservaciones()
            CargaDataSource()
        End Sub

        Private Sub BtnCancelar_Click(sender As System.Object, e As System.EventArgs) Handles BtnCancelar.Click
            Me.Close()
        End Sub

        Private Sub BtnGuardar_Click(sender As System.Object, e As System.EventArgs) Handles BtnGuardar.Click
            Guardar()
        End Sub

        Private Sub FechaProcesodateTimePicker_ValueChanged(sender As System.Object, e As System.EventArgs) Handles FechaProcesodateTimePicker.ValueChanged
            CargaDataSource()
        End Sub

#End Region

#Region " Metodos "

        Private Sub CargaTablas()
            Dim dbmIntegration As New DBIntegration.DBIntegrationDataBaseManager(Me._Plugin.CajaSocialConnectionString)

            Try
                dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                _ObservacionesDataTable = dbmIntegration.SchemaBCSCarpetaUnica.CTA_Observaciones.DBGet()
                _OficinasDataTable = dbmIntegration.SchemaBCSCarpetaUnica.CTA_Oficinas.DBGet()

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaTablas", ex)
            Finally
                If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
            End Try
        End Sub

        Private Sub CargaObservaciones()
            Try
                Utilities.LlenarCombo(ObservacionesDesktopComboBoxControl, _ObservacionesDataTable, DBIntegration.SchemaBCSCarpetaUnica.TBL_ObservacionesEnum.id_Codigo_Observacion.ColumnName, DBIntegration.SchemaBCSCarpetaUnica.TBL_ObservacionesEnum.Descripcion.ColumnName, True)
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaObservaciones", ex)
            End Try
        End Sub

        Private Sub CargaOficinas(ByVal nidOt As Integer, ByVal nidPrecinto As Integer, ByVal nidContenedor As Integer)
            Dim dbmIntegration As New DBIntegration.DBIntegrationDataBaseManager(Me._Plugin.CajaSocialConnectionString)
            Dim dbmImaging As New DBImaging.DBImagingDataBaseManager(Me._Plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)
            Try
                dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
                dbmImaging.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                Utilities.LlenarCombo(OficinasDesktopComboBoxControl, _OficinasDataTable, DBIntegration.SchemaBCSCarpetaUnica.TBL_OficinasEnum.Codigo_Oficina.ColumnName, DBIntegration.SchemaBCSCarpetaUnica.TBL_OficinasEnum.Nombre_Oficina.ColumnName, True)

                Dim CTA_ContenedorDataTable = dbmImaging.SchemaConfig.CTA_Data_Contenedor.DBFindByfk_OTfk_Precintofk_Contenedorfk_Entidadfk_ProyectoNombre_Campo(nidOt, nidPrecinto, nidContenedor, _Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, "OFICINA")
                If CTA_ContenedorDataTable.Count > 0 Then
                    OficinasDesktopComboBoxControl.SelectedValue = CTA_ContenedorDataTable(0).Data_Campo.ToString()
                    OficinasDesktopComboBoxControl.Enabled = False
                Else
                    MessageBox.Show("La oficina del contenedor no existe, por favor verifique.", "Captura Tapa", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaOficinas", ex)
            Finally
                If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

        Private Sub Guardar()
            If Validar() Then
                Dim dbmIntegration As New DBIntegration.DBIntegrationDataBaseManager(Me._Plugin.CajaSocialConnectionString)
                Dim CapturaTapa = New DBIntegration.SchemaBCSCarpetaUnica.TBL_Captura_Tapa_ObservacionesType()


                Try
                    dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                    Dim CapturaTapaDataTable = dbmIntegration.SchemaBCSCarpetaUnica.TBL_Captura_Tapa_Observaciones.DBFindByFecha_Procesofk_Tipo_Procesofk_OficinaCampo_1Campo_3(FechaProcesodateTimePicker.Value.ToString("yyyyMMdd"), _Tipo_Proceso, CInt(OficinasDesktopComboBoxControl.SelectedValue.ToString()), NumProductoTextBox.Text.ToString(), CInt(ObservacionesDesktopComboBoxControl.SelectedValue.ToString()))

                    If CapturaTapaDataTable.Count = 0 Then
                        With CapturaTapa
                            CapturaTapa.Fecha_Proceso = FechaProcesodateTimePicker.Value.ToString("yyyyMMdd")
                            CapturaTapa.fk_Tipo_Proceso = _Tipo_Proceso
                            CapturaTapa.fk_Oficina = CInt(OficinasDesktopComboBoxControl.SelectedValue.ToString())
                            CapturaTapa.Campo_1 = NumProductoTextBox.Text.ToString()
                            CapturaTapa.Campo_3 = CInt(ObservacionesDesktopComboBoxControl.SelectedValue.ToString())
                            CapturaTapa.Fecha_Proceso_Ejecucion = FechaProcesodateTimePicker.Value.ToString("yyyyMMdd")
                        End With
                    Else
                        MessageBox.Show("La observación para el número de producto, fecha, oficina y tipo de proceso ya existe, Por favor verifique.", "Captura Tapa", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                   

                    dbmIntegration.SchemaBCSCarpetaUnica.TBL_Captura_Tapa_Observaciones.DBInsert(CapturaTapa)

                    CargaDataSource()

                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Captura Tapa", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
                End Try

            End If
        End Sub

        Private Sub CargaDataSource()
            Dim dbmIntegration As New DBIntegration.DBIntegrationDataBaseManager(Me._Plugin.CajaSocialConnectionString)
            Try
                dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                ContenedoresDataGridView.AutoGenerateColumns = False
                ContenedoresDataGridView.DataSource = dbmIntegration.SchemaBCSCarpetaUnica.CTA_Observaciones_Tapa.DBFindByFecha_Procesofk_Tipo_Procesofk_Oficina(FechaProcesodateTimePicker.Value.ToString("yyyyMMdd"), _Tipo_Proceso, CInt(OficinasDesktopComboBoxControl.SelectedValue.ToString()))
                ObservacionesDesktopComboBoxControl.SelectedValue = "-1"
                NumProductoTextBox.Text = ""
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Captura Tapa", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
            End Try

        End Sub

        Private Sub CargaProceso()
            Dim dbmIntegration As New DBIntegration.DBIntegrationDataBaseManager(Me._Plugin.CajaSocialConnectionString)
            Dim dbmImaging As New DBImaging.DBImagingDataBaseManager(Me._Plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)
            Dim Proceso As String

            Try
                dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
                dbmImaging.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                Dim CTA_Data_ContenedorDataTable = dbmImaging.SchemaConfig.CTA_Data_Contenedor.DBFindByfk_OTfk_Precintofk_Contenedorfk_Entidadfk_ProyectoNombre_Campo(_nidOt, _nidPrecinto, _nidContenedor, _Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, "PROCESO")
                If CTA_Data_ContenedorDataTable.Count > 0 Then
                    Proceso = CTA_Data_ContenedorDataTable(0).Data_Campo

                    Dim ProcesoDataTable = dbmIntegration.SchemaBCSCarpetaUnica.TBL_Tipo_Proceso.DBFindByNombre_Tipo_Proceso(Proceso)
                    If ProcesoDataTable.Count > 0 Then
                        _Tipo_Proceso = ProcesoDataTable(0).id_Tipo_Proceso
                    Else
                        MessageBox.Show("El tipo de proceso no existe o no esta configurado, Por favor verifique.", "Captura Tapa", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                Else
                    MessageBox.Show("La data del contenedor para el campo PROCESO no existe, por favor verifique.", "Captura Tapa", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If

            Catch ex As Exception
                MessageBox.Show(ex.Message, "Captura Tapa", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try

        End Sub

#End Region

#Region " Funciones "

        Private Function Validar() As Boolean
            If NumProductoTextBox.Text = "" Then
                MessageBox.Show("Debe digitar un número de producto", "Captura Tapa", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ElseIf ObservacionesDesktopComboBoxControl.SelectedValue.ToString() = "-1" Then
                MessageBox.Show("Debe seleccionar una observación", "Captura Tapa", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ElseIf OficinasDesktopComboBoxControl.SelectedValue.ToString() = "-1" Then
                MessageBox.Show("Debe seleccionar una oficina", "Captura Tapa", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                Return True
            End If

            Return False
        End Function

#End Region

    End Class
End Namespace
