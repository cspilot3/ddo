Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library
Imports System.Windows.Forms
Imports DBAgrario
Imports DBAgrario.SchemaProcess
Imports Miharu.Desktop.Library.Config
Imports Slyg.Tools.Progress

Namespace Imaging.Forms.Correciones

    Public Class FormCorreccion

#Region " Declaraciones"

        Private _Plugin As BanagrarioImagingPlugin
        Private Tipo_cambio As Integer = 0
        Private Oficina As String = "0"

#End Region

#Region " Constructores"

        Public Sub New(ByVal nBanagrarioDesktopPlugin As BanagrarioImagingPlugin)

            InitializeComponent()
            _Plugin = nBanagrarioDesktopPlugin
            'Ocultar la columa del fk_Proceso
            DatosDataGridView.Columns(0).Visible = False
        End Sub

#End Region

#Region " Eventos"

        Private Sub FormCorreccion_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

            'Accesos a opciones
            ActualizarButton.Enabled = _Plugin.Manager.Sesion.Usuario.PerfilManager.PuedeAcceder(Permisos.Imaging.Proceso.Captura.Reprocesos)
            BuscarButton.Enabled = _Plugin.Manager.Sesion.Usuario.PerfilManager.PuedeAcceder(Permisos.Imaging.Busqueda)
            ResultadoGroupBox.Enabled = _Plugin.Manager.Sesion.Usuario.PerfilManager.PuedeAcceder(Permisos.Imaging.Busqueda)
            CorreccionGroupBox.Enabled = _Plugin.Manager.Sesion.Usuario.PerfilManager.PuedeAcceder(Permisos.Imaging.Proceso.Captura.Reprocesos)
            CruzarButton.Enabled = _Plugin.Manager.Sesion.Usuario.PerfilManager.PuedeAcceder(Permisos.Imaging.Proceso.Especiales)

            LblValorCargue.Text = ""
            LblValorPaquete.Text = ""
            LblValorContenedor.Text = ""

            LoadData()
        End Sub

        Private Sub ActualizarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ActualizarButton.Click
            Insertardatos()
        End Sub

        Private Sub BuscarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BuscarButton.Click
            Buscar()
        End Sub

        Private Sub DatosDataGridView_CellMouseDoubleClick(ByVal sender As System.Object, ByVal e As DataGridViewCellMouseEventArgs) Handles DatosDataGridView.CellMouseDoubleClick

            CorreccionesDesktopTabControl.SelectedTab = CorreccionTab

            Lblfk_Proceso.Text = DatosDataGridView.Rows(e.RowIndex).Cells(0).Value.ToString()
            LblCodOfiAnterior.Text = DatosDataGridView.Rows(e.RowIndex).Cells(3).Value.ToString()
            LblFecMovAnterior.Text = DatosDataGridView.Rows(e.RowIndex).Cells(5).Value.ToString()
            LblValorCargue.Text = DatosDataGridView.Rows(e.RowIndex).Cells(6).Value.ToString()
            LblValorPaquete.Text = DatosDataGridView.Rows(e.RowIndex).Cells(7).Value.ToString()
            LblValorContenedor.Text = DatosDataGridView.Rows(e.RowIndex).Cells(2).Value.ToString()

            Limpiar_Controles()

        End Sub

        Private Sub BtnGuardar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BtnGuardar.Click
            Guardar()
        End Sub

        Private Sub CruzarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CruzarButton.Click
            Dim Respuesta = DesktopMessageBoxControl.DesktopMessageShow("¿Está seguro que desea generar el Cruce?", "Cruce", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, False)

            If (Respuesta = DialogResult.OK) Then
                Cruzar()
            End If
        End Sub

        Private Sub CBFechaMovimiento_CheckedChanged(sender As System.Object, e As EventArgs) Handles CBFechaMovimiento.CheckedChanged
            FechaMovimientoDateTimePicker.Enabled = CBFechaMovimiento.Checked
        End Sub

        Private Sub CBCodigoOficina_CheckedChanged(sender As System.Object, e As EventArgs) Handles CBCodigoOficina.CheckedChanged
            OficinaDesktopComboBox.Enabled = CBCodigoOficina.Checked
        End Sub

        Private Sub CBContenedor_CheckedChanged(sender As System.Object, e As EventArgs) Handles CBContenedor.CheckedChanged
            CodigoContenedorTextBox.Enabled = CBContenedor.Checked
        End Sub

#End Region

#Region " Metodos"

        Private Sub LoadData()
            Dim dbmAgrario As DBAgrarioDataBaseManager = Nothing

            Try
                dbmAgrario = New DBAgrarioDataBaseManager(Me._Plugin.BancoAgrarioConnectionString)
                dbmAgrario.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                Dim EntidadProcesamiento As Short = _Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad

                Dim OficinaData = dbmAgrario.SchemaProcess.CTA_Oficina_Concatenacion.DBFindByfk_Entidad(EntidadProcesamiento, 0, New CTA_Oficina_ConcatenacionEnumList(CTA_Oficina_ConcatenacionEnum.id_Oficina, True))
                Dim OfiView = OficinaData.DefaultView

                Utilities.LlenarCombo(OficinaDesktopComboBox, OfiView.ToTable(), CTA_Oficina_ConcatenacionEnum.id_Oficina.ColumnName, CTA_Oficina_ConcatenacionEnum.Nombre_Oficina.ColumnName)
                OficinaDesktopComboBox.SelectedIndex = -1
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow(ex.Message, "Módulo de Correcciones", DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
            Finally

                If (dbmAgrario IsNot Nothing) Then dbmAgrario.Connection_Close()

            End Try

        End Sub

        Private Sub Insertardatos()
            If (Validar()) Then

                Dim dbmAgrario As DBAgrarioDataBaseManager = Nothing

                Try
                    dbmAgrario = New DBAgrarioDataBaseManager(Me._Plugin.BancoAgrarioConnectionString)
                    dbmAgrario.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                    Dim Fecha As String = FechaProcesoDateTimePicker.Value.ToString("yyyy/MM/dd")
                    Dim Cant_Registros = dbmAgrario.SchemaKey.PA_Calcular_Registros.DBExecute(Fecha)

                    DesktopMessageBoxControl.DesktopMessageShow("Proceso finalizado, se han encontrado " & Cant_Registros & " registros para la fecha de proceso seleccionada", "Módulo de Correcciones", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                Catch ex As Exception
                    DesktopMessageBoxControl.DesktopMessageShow(ex.Message, "Módulo de Correcciones", DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
                Finally
                    If (dbmAgrario IsNot Nothing) Then dbmAgrario.Connection_Close()
                End Try

            End If
        End Sub

        Private Sub Buscar()

            If (Validar()) Then

                Dim dbmAgrario As DBAgrarioDataBaseManager = Nothing

                Try
                    dbmAgrario = New DBAgrarioDataBaseManager(Me._Plugin.BancoAgrarioConnectionString)
                    dbmAgrario.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                    Dim Fecha As String = FechaProcesoDateTimePicker.Value.ToString("yyyy/MM/dd")

                    Dim CorreccionLlavesDataTable = dbmAgrario.SchemaKey.PA_Get_Registros_Error_Llaves.DBExecute(Fecha)

                    If CorreccionLlavesDataTable.Count > 0 Then

                        DatosDataGridView.DataSource = CorreccionLlavesDataTable
                        DatosDataGridView.ClearSelection()

                        MessageBox.Show("Se encontraron " & CorreccionLlavesDataTable.Count & " registros para la fecha de proceso seleccionada", "Módulo de Correcciones", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        MessageBox.Show("No se encontraron registros para la fecha de proceso seleccionada", "Módulo de Correcciones", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    End If

                Catch ex As Exception
                    DesktopMessageBoxControl.DesktopMessageShow(ex.Message, "Módulo de Correcciones", DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
                Finally
                    If (dbmAgrario IsNot Nothing) Then dbmAgrario.Connection_Close()
                End Try

            End If
        End Sub

        Private Sub Guardar()

            If (ValidarSeleccionCorreccion()) Then

                Dim dbmAgrario As DBAgrarioDataBaseManager = Nothing
                Dim ProgressForm As New FormProgress()


                Try
                    dbmAgrario = New DBAgrarioDataBaseManager(Me._Plugin.BancoAgrarioConnectionString)
                    dbmAgrario.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
                    ''dbmAgrario.DataBase.Identifier_Date_Format = _Plugin.Manager.DesktopGlobal.IdentifierDateFormat

                    'Tipo de cambio: Oficina (1), Fecha de Movimiento(2) o ambas(3)
                    If CBFechaMovimiento.Checked = True And CBCodigoOficina.Checked = True Then
                        Tipo_cambio = 3
                    ElseIf CBFechaMovimiento.Checked = True Then
                        Tipo_cambio = 2
                    ElseIf CBCodigoOficina.Checked = True Then
                        Tipo_cambio = 1
                    End If

                    'dbmAgrario.SchemaProcess.PA_LLaves_00_Correccion_Error.DBExecute(CType(Lblfk_Proceso.Text, Global.Slyg.Tools.SlygNullable(Of Integer)), _
                    '                                                                 Integer.Parse(LblValorCargue.Text), _
                    '                                                                 Short.Parse(LblValorPaquete.Text), _
                    '                                                                 LblValorContenedor.Text, _
                    '                                                                 FechaProcesoDateTimePicker.Value.ToString("yyyy/MM/dd"), _
                    '                                                                 LblFecMovAnterior.Text, _
                    '                                                                 CType(OficinaDesktopComboBox.SelectedValue.ToString, Global.Slyg.Tools.SlygNullable(Of Integer)), _
                    '                                                                 CType(LblCodOfiAnterior.Text, Global.Slyg.Tools.SlygNullable(Of Integer)), _
                    '                                                                 Tipo_cambio)

                    ProgressForm.Action = "Corrección"
                    ProgressForm.ValueAction = 0
                    ProgressForm.MaxValueAction = 4

                    ProgressForm.Show()

                    ProgressForm.Action = "Corrigiendo Destape"
                    ProgressForm.ValueAction = 1
                    Application.DoEvents()

                    Dim codigoContenedor As Slyg.Tools.SlygNullable(Of String)

                    If CodigoContenedorTextBox.Text = "" Then
                        codigoContenedor = DBNull.Value
                    Else
                        codigoContenedor = CodigoContenedorTextBox.Text
                    End If

                    dbmAgrario.SchemaKey.PA_01_Correcion_Llaves_Destape.DBExecute(LblValorContenedor.Text, _
                                                                                  Integer.Parse(LblValorCargue.Text), _
                                                                                  Short.Parse(LblValorPaquete.Text), _
                                                                                  CShort(Tipo_cambio), _
                                                                                  Integer.Parse(Oficina), _
                                                                                  FechaMovimientoDateTimePicker.Value.ToString("yyyy/MM/dd"), _
                                                                                  codigoContenedor)


                    ProgressForm.Action = "Corrigiendo datos de Cargue - Paquete"
                    ProgressForm.ValueAction = 2
                    Application.DoEvents()
                    dbmAgrario.SchemaKey.PA_02_Correcion_Cargue_Paquete.DBExecute(Integer.Parse(LblValorCargue.Text), _
                                                                                  Short.Parse(LblValorPaquete.Text), _
                                                                                  Tipo_cambio, _
                                                                                  FechaMovimientoDateTimePicker.Value.ToString("yyyy/MM/dd"), _
                                                                                  Integer.Parse(Oficina))

                    ProgressForm.Action = "Corrigiendo Llaves"
                    ProgressForm.ValueAction = 3
                    Application.DoEvents()

                    If CBCodigoOficina.Checked Then

                        dbmAgrario.SchemaKey.PA_03_Correcion_Llaves_Oficina.DBExecute(Integer.Parse(LblValorCargue.Text), _
                                                                                      Short.Parse(LblValorPaquete.Text), _
                                                                                      Integer.Parse(Oficina))

                        dbmAgrario.SchemaAudit.PA_Inserccion_Log_Cambio_Error_Llaves.DBExecute(Long.Parse(Lblfk_Proceso.Text), _
                                                                        Integer.Parse(LblValorCargue.Text), _
                                                                        Short.Parse(LblValorPaquete.Text), _
                                                                        1,
                                                                        LblValorContenedor.Text, _
                                                                        RdbFuncionariopyc.Checked, _
                                                                        _Plugin.Manager.Sesion.Usuario.id, _
                                                                        LblCodOfiAnterior.Text, _
                                                                        Oficina)
                    End If

                    If CBFechaMovimiento.Checked Then

                        dbmAgrario.SchemaKey.PA_04_Correcion_Llaves_Fecha_Movimiento.DBExecute(Integer.Parse(LblValorCargue.Text), _
                                                                                               Short.Parse(LblValorPaquete.Text), _
                                                                                               FechaMovimientoDateTimePicker.Value.ToString("yyyy/MM/dd"))

                        dbmAgrario.SchemaAudit.PA_Inserccion_Log_Cambio_Error_Llaves.DBExecute(Long.Parse(Lblfk_Proceso.Text), _
                                                                        Integer.Parse(LblValorCargue.Text), _
                                                                        Short.Parse(LblValorPaquete.Text), _
                                                                        2,
                                                                        LblValorContenedor.Text, _
                                                                        RdbFuncionariopyc.Checked, _
                                                                        _Plugin.Manager.Sesion.Usuario.id, _
                                                                        LblFecMovAnterior.Text, _
                                                                        FechaMovimientoDateTimePicker.Value.ToString("yyyy/MM/dd"))
                    End If

                    If CBContenedor.Checked Then

                        'Dim DestapeDataTable = dbmAgrario.SchemaProcess.TBL_Destape.DBFindByfk_Carguefk_Cargue_Paquete(Integer.Parse(LblValorCargue.Text), Short.Parse(LblValorPaquete.Text))

                        'Dim DestapeContenedorDataTable = dbmAgrario.SchemaProcess.TBL_Destape.DBFindBycodigo_Contenedorfecha_Proceso(CodigoContenedorTextBox.Text, Nothing)

                        'Dim contador As Integer = 0

                        'For Each tblDestapeRow As TBL_DestapeRow In DestapeContenedorDataTable
                        '    If (tblDestapeRow.fecha_Movimiento = DestapeDataTable(0).fecha_Movimiento) And (tblDestapeRow.fk_Oficina = DestapeDataTable(0).fk_Oficina) Then
                        '        contador = contador + 1
                        '    End If
                        'Next

                        'If contador = 0 Then
                        '    Throw New Exception("El contenedor seleccionado corresponde a otra Fecha de Movimiento u Oficina, Debe seleccionar otro")
                        'End If

                        dbmAgrario.SchemaKey.PA_05_Correcion_Llaves_Contenedor.DBExecute(Integer.Parse(LblValorCargue.Text), _
                                                                                               Short.Parse(LblValorPaquete.Text), _
                                                                                               CodigoContenedorTextBox.Text)

                        dbmAgrario.SchemaAudit.PA_Inserccion_Log_Cambio_Error_Llaves.DBExecute(Long.Parse(Lblfk_Proceso.Text), _
                                                                        Integer.Parse(LblValorCargue.Text), _
                                                                        Short.Parse(LblValorPaquete.Text), _
                                                                        3,
                                                                        LblValorContenedor.Text, _
                                                                        RdbFuncionariopyc.Checked, _
                                                                        _Plugin.Manager.Sesion.Usuario.id, _
                                                                        LblValorContenedor.Text, _
                                                                        codigoContenedor)
                    End If

                    ProgressForm.Close()

                    DesktopMessageBoxControl.DesktopMessageShow("Se guardó la correccion exitosamente", "Módulo de Correcciones", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                    Limpiar_Controles()
                Catch ex As Exception
                    DesktopMessageBoxControl.DesktopMessageShow(ex.Message, "Módulo de Correcciones", DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
                    ProgressForm.Close()
                Finally
                    If (dbmAgrario IsNot Nothing) Then dbmAgrario.Connection_Close()
                End Try
            End If
        End Sub

        Private Sub Cruzar()
            Dim dmBanAgrario As New DBAgrarioDataBaseManager(Me._Plugin.BancoAgrarioConnectionString)

            Dim ProgressForm As New FormProgress()

            Try
                Me.Enabled = False
                Me.Cursor = Cursors.WaitCursor

                dmBanAgrario.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
                'dmBanAgrario.DataBase.Identifier_Date_Format = _Plugin.Manager.DesktopGlobal.IdentifierDateFormat


                Dim ExpedientesCruce As SchemaCore.CTA_Imaging_FolderDataTable
                Dim ExpedientesDictionary As New Dictionary(Of Int64, String)

                ExpedientesCruce = dmBanAgrario.SchemaCore.CTA_Imaging_Folder.DBFindByfk_Carguefk_Cargue_Paquete(Integer.Parse(LblValorCargue.Text), Short.Parse(LblValorPaquete.Text))

                'Se adiciona los expedientes que cumplen los filtros
                For Each expediente In ExpedientesCruce
                    ExpedientesDictionary.Add(expediente.fk_Expediente, "")
                Next

                ProgressForm.Process = ""
                ProgressForm.Action = ""
                ProgressForm.ValueProcess = 0
                ProgressForm.ValueAction = 0
                ProgressForm.MaxValueProcess = ExpedientesDictionary.Count
                ProgressForm.MaxValueAction = 2

                ProgressForm.Show()

                Dim i As Integer = 0
                For Each Item In ExpedientesDictionary

                    ProgressForm.Process = Item.Value
                    'ProgressForm.Process = Item.Value

                    ProgressForm.Action = "Reversar Cruce"
                    ProgressForm.ValueAction = 0
                    Application.DoEvents()
                    dmBanAgrario.SchemaCrossing.PA_New_00_Reversar_Cruce_File.DBExecute(Item.Key, CShort(1), CShort(1))
                    If (ProgressForm.Cancel) Then Throw New Exception("Operación cancelada por el usuario")

                    ProgressForm.Action = "Realizando Cruce"
                    ProgressForm.ValueAction = 1
                    Application.DoEvents()
                    'dmBanAgrario.SchemaCrossing.PA_New_00_Finalizar_Primera_Captura.DBExecute(Item.Key, CShort(1), CShort(1))
                    dmBanAgrario.SchemaCrossing.PA_New_00_Cruce_En_Linea_Cola.DBExecute(Item.Key, CShort(1), CShort(1), "1")
                    If (ProgressForm.Cancel) Then Throw New Exception("Operación cancelada por el usuario")

                    i += 1
                    ProgressForm.ValueProcess = i
                Next

                'Validar si realmente cruzaron los registros
                ProgressForm.Action = "Actualizar Cálculo de Soportes Sobrantes"
                ProgressForm.ValueAction = 1
                Application.DoEvents()
                dmBanAgrario.SchemaKey.PA_Calcular_Registros_Cargue_Paquete.DBExecute(Integer.Parse(LblValorCargue.Text), Short.Parse(LblValorPaquete.Text))

                ProgressForm.Close()

                MessageBox.Show("Se ha realizado el proceso de cruce exitosamente", "Módulo de Correcciones", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("GeneraCruce", ex)
                ProgressForm.Close()
            Finally
                dmBanAgrario.Connection_Close()
                Me.Enabled = True
                Me.Cursor = Cursors.Default
            End Try
        End Sub

        Private Sub Limpiar_Controles()

            CBFechaMovimiento.Checked = False
            FechaMovimientoDateTimePicker.Value = Date.Now
            CBCodigoOficina.Checked = False
            OficinaDesktopComboBox.SelectedIndex = -1
            CBContenedor.Checked = False
            CodigoContenedorTextBox.Text = ""

        End Sub


#End Region

#Region " Funciones  "

        Private Function Validar() As Boolean

            Dim FechaSeleccionada = New DateTime(FechaProcesoDateTimePicker.Value.Year, FechaProcesoDateTimePicker.Value.Month, FechaProcesoDateTimePicker.Value.Day)
            Dim FechaHoy = New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)

            If FechaSeleccionada > FechaHoy Then
                DesktopMessageBoxControl.DesktopMessageShow("La Fecha de proceso no puede ser mayor al día de hoy", "Seleccione una fecha válida", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                Return False
            End If

            Return True

        End Function

        Private Function ValidarSeleccionCorreccion() As Boolean

            'Validación de la selección de un tipo de cambio a realizar
            If CBFechaMovimiento.Checked = False And CBCodigoOficina.Checked = False And CBContenedor.Checked = False Then
                DesktopMessageBoxControl.DesktopMessageShow("No se ha seleccionado el tipo de cambio a realizar, por favor seleccione por lo menos uno", "Módulo de Correcciones", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                Return False
            End If

            Dim FechaSeleccionada = New DateTime(FechaMovimientoDateTimePicker.Value.Year, FechaMovimientoDateTimePicker.Value.Month, FechaMovimientoDateTimePicker.Value.Day)
            Dim FechaHoy = New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)

            'Validación de la fecha de movimiento seleccionada
            If CBFechaMovimiento.Checked = True Then
                If FechaSeleccionada > FechaHoy Then
                    DesktopMessageBoxControl.DesktopMessageShow("La Fecha de Movimiento no puede ser mayor al día de hoy", "Módulo de Correcciones", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                    Return False

                ElseIf FechaSeleccionada.ToString("yyyyMMdd") < "20110811" Then
                    DesktopMessageBoxControl.DesktopMessageShow("La Fecha de Movimiento no puede ser menor a la fecha de inicio de operación del Banco (2011/08/11)", "Módulo de Correcciones", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                    Return False

                End If

                'Validación de datos ingresados
                If FechaSeleccionada.ToString("yyyy/MM/dd") = LblFecMovAnterior.Text Then
                    DesktopMessageBoxControl.DesktopMessageShow("La fecha de movimiento seleccionada es igual a la fecha de movimiento asociada actualmente al cargue. Por favor verifique ", "Módulo de Correcciones", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                    Return False
                End If
            End If

            'Validación de la seleccón de una oficina
            If CBCodigoOficina.Checked = True Then
                If OficinaDesktopComboBox.SelectedValue Is "-1" Then
                    DesktopMessageBoxControl.DesktopMessageShow("La selección de Oficina no es válida ", "Seleccione una oficina válida", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                    Return False

                End If
                If OficinaDesktopComboBox.SelectedValue.ToString = LblCodOfiAnterior.Text Then
                    DesktopMessageBoxControl.DesktopMessageShow("La oficina seleccionada es igual a la oficina asociada actualmente al cargue. Por favor verifique", "Módulo de Correcciones", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                    Return False
                End If

                Oficina = OficinaDesktopComboBox.SelectedValue.ToString
            End If

            If CBContenedor.Checked AndAlso CodigoContenedorTextBox.Text = "" Then
                DesktopMessageBoxControl.DesktopMessageShow("El código de contenedor se encuentra vacio. Por favor verifique", "Módulo de Correcciones", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                Return False
            End If
            If CBContenedor.Checked AndAlso CodigoContenedorTextBox.Text = LblValorContenedor.Text Then
                DesktopMessageBoxControl.DesktopMessageShow("El código de contenedor es igual al valor anterior. Por favor verifique", "Módulo de Correcciones", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                Return False
            End If

            Return True

        End Function

#End Region

    End Class
End Namespace