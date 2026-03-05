Imports System.Windows.Forms
Imports Banagrario.Plugin.Risk.Forms.OT
Imports Banagrario.Plugin.Risk.Reportes.EmpaqueContenedoresTulaEnlace
Imports DBAgrario
Imports DBAgrario.SchemaConfig
Imports DBAgrario.SchemaProcess
Imports DBArchiving
Imports DBArchiving.SchemaRisk
Imports DBCore
Imports DBCore.SchemaConfig
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Config
Imports SLYG.Tools
Imports Miharu.Desktop.Controls.Utils

Namespace Risk.Forms.Empaque

    Public Class FormEmpaque

#Region " Declaraciones "

        Private _plugin As BanagrarioRiskPlugin

        Private _otActual As Integer = 0
        Private _ultimoPrecinto As String = ""
        Private _ultimoContenedor As String = ""
        Private _ultimaCantidad As String = ""

        Private _contenedorActual As ContenedorInfo = Nothing
        Private _cajaActual As TBL_CajaType = Nothing

#End Region

#Region " Constructores "

        Public Sub New(ByVal nBanagrarioDesktopPlugin As BanagrarioRiskPlugin)
            InitializeComponent()

            _plugin = nBanagrarioDesktopPlugin

        End Sub

#End Region

#Region " Eventos "

        Private Sub FormEmpaque_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            Dim seleccionarOt As New FormSeleccionarOT(_plugin)
            If (seleccionarOt.ShowDialog() = DialogResult.OK) Then
                _otActual = seleccionarOt.OT_Seleccionada
                LlenarCombos()

                LimpiarControlesContenedor(True, True, True)
                HabilitarDestapePrecinto(False)
                TipoContenedorComboBox.SelectedIndex = -1
                Anexo1ComboBox.SelectedIndex = -1
                Anexo2ComboBox.SelectedIndex = -1
            Else
                Close()
            End If
        End Sub

        Private Sub Anexo2ComboBox_KeyDown(ByVal sender As System.Object, ByVal e As KeyEventArgs) Handles Anexo2ComboBox.KeyDown
            If (e.KeyCode = Keys.Enter) Then
                CantidadDocumentosTextBox.Focus()
            End If
        End Sub

        Private Sub TipoContenedorComboBox_KeyDown(ByVal sender As System.Object, ByVal e As KeyEventArgs) Handles TipoContenedorComboBox.KeyDown
            If (e.KeyCode = Keys.Enter) Then
                Anexo1ComboBox.Focus()
            End If
        End Sub

        Private Sub TipoContenedorComboBox_Leave(ByVal sender As System.Object, ByVal e As EventArgs) Handles TipoContenedorComboBox.Leave
            OkControlTipoContendor.Visible = True
        End Sub

        Private Sub Anexo1ComboBox_KeyDown(ByVal sender As System.Object, ByVal e As KeyEventArgs) Handles Anexo1ComboBox.KeyDown
            If (e.KeyCode = Keys.Enter) Then
                Anexo2ComboBox.Focus()
            End If

        End Sub

        Private Sub Imprimir_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ImprimirButton.Click
            ImprimirReporte()
        End Sub

        Private Sub Anexo1ComboBox_Enter(ByVal sender As System.Object, ByVal e As EventArgs) Handles Anexo1ComboBox.Enter
            AnexoLabel.Visible = False
        End Sub

        Private Sub AnexoLabel_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AnexoLabel.Click
            AnexoLabel.Visible = False
            Anexo1ComboBox.Focus()
        End Sub

        Private Sub Anexo2ComboBox_Enter(ByVal sender As System.Object, ByVal e As EventArgs) Handles Anexo2ComboBox.Enter
            AnexoLabel.Visible = True
        End Sub

        Private Sub FormEmpaque_KeyDown(ByVal sender As System.Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
            If (Me.Enabled) Then
                If (((e.Control And e.KeyCode = Keys.I) Or e.KeyCode = Keys.F9) And InsertarContenedorButton.Visible And InsertarContenedorButton.Enabled) Then
                    InsertarContenedor()
                End If
                If (((e.Control And e.KeyCode = Keys.F) Or e.KeyCode = Keys.F5) And FinalizarButton.Visible And FinalizarButton.Enabled) Then
                    FinalizarUCS()
                End If
            End If
        End Sub

        Private Sub PrecintoTextBox_Enter(ByVal sender As System.Object, ByVal e As EventArgs) Handles PrecintoCajaTextBox.Enter
            _UltimoPrecinto = PrecintoCajaTextBox.Text
        End Sub

        Private Sub PrecintoTextBox_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles PrecintoCajaTextBox.KeyDown
            If (e.KeyCode = Keys.Enter) Then
                PuntoDestinoComboBox.Focus()
            End If
        End Sub

        Private Sub PrecintoTextBox_Leave(ByVal sender As System.Object, ByVal e As EventArgs) Handles PrecintoCajaTextBox.Leave
            If (_UltimoPrecinto <> PrecintoCajaTextBox.Text) Then
                CambiarPrecintoSeleccionado()
            End If
        End Sub

        Private Sub InsertarContenedorButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles InsertarContenedorButton.Click
            InsertarContenedor()
        End Sub

        Private Sub FinalizarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles FinalizarButton.Click
            FinalizarUCS()
        End Sub

        Private Sub CerrarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CerrarButton.Click
            Close()
        End Sub

        Private Sub PuntoDestinoComboBox_KeyDown(ByVal sender As System.Object, ByVal e As KeyEventArgs) Handles PuntoDestinoComboBox.KeyDown
            If (e.KeyCode = Keys.Enter) Then
                ContenedorTextBox.Focus()
            End If
        End Sub

        Private Sub ContenedorTextBox_Enter(ByVal sender As Object, ByVal e As EventArgs) Handles ContenedorTextBox.Enter
            _UltimoContenedor = ContenedorTextBox.Text
        End Sub

        Private Sub ContenedorTextBox_KeyDown(ByVal sender As System.Object, ByVal e As KeyEventArgs) Handles ContenedorTextBox.KeyDown
            If (e.KeyCode = Keys.Enter) Then
                If (_UltimoContenedor <> ContenedorTextBox.Text) Then
                    Dim mensaje As String = ""
                    If BuscarContenedor(ContenedorTextBox.Text, mensaje) Then
                        If (Not _ContenedorActual Is Nothing) Then
                            ContenedorOkControl.OK = TriState.True
                            TipoContenedorComboBox.Focus()
                        Else
                            MessageBox.Show(mensaje, "Empaque", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            ContenedorTextBox.Focus()
                            ContenedorOkControl.OK = TriState.False
                        End If
                    Else
                        ContenedorOkControl.OK = TriState.False
                    End If
                End If
            End If
        End Sub

        Private Sub ContenedorTextBox_Leave(ByVal sender As System.Object, ByVal e As EventArgs) Handles ContenedorTextBox.Leave
            If (_UltimoContenedor <> ContenedorTextBox.Text) Then
                Dim mensaje As String = ""
                If (ContenedorTextBox.Text <> "0") Then
                    If BuscarContenedor(ContenedorTextBox.Text, mensaje) Then
                        If (Not _ContenedorActual Is Nothing) Then
                            ContenedorOkControl.Visible = True
                            ContenedorOkControl.OK = TriState.True
                        Else
                            MessageBox.Show(mensaje, "Empaque", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            ContenedorOkControl.Visible = True
                            ContenedorOkControl.OK = TriState.False
                        End If
                    Else
                        ContenedorOkControl.OK = TriState.False
                    End If
                Else
                    ContenedorTextBox.Text = ""
                End If

            End If
        End Sub

        Private Sub CantidadDocumentosTextBox_Enter(ByVal sender As System.Object, ByVal e As EventArgs) Handles CantidadDocumentosTextBox.Enter
            _UltimaCantidad = CantidadDocumentosTextBox.Text
        End Sub

        Private Sub CantidadDocumentosTextBox_Leave(ByVal sender As System.Object, ByVal e As EventArgs) Handles CantidadDocumentosTextBox.Leave
            If (_UltimaCantidad <> CantidadDocumentosTextBox.Text) Then
                CantidadOkControl.Visible = True
                ValidarCantidad()
            End If
        End Sub

        Private Sub CantidadDocumentosTextBox_KeyDown(ByVal sender As System.Object, ByVal e As KeyEventArgs) Handles CantidadDocumentosTextBox.KeyDown
            If (e.KeyCode = Keys.Enter) Then
                InsertarContenedorButton.Focus()
            End If
        End Sub

        'Private Sub ContenedorReempaqueTextBox_KeyDown(ByVal sender As System.Object, ByVal e As KeyEventArgs)
        '    If (e.KeyCode = Keys.Enter) Then
        '        TipoContenedorComboBox.Focus()
        '    End If
        'End Sub

        Private Sub PuntoDestinoComboBox_Leave(ByVal sender As System.Object, ByVal e As EventArgs) Handles PuntoDestinoComboBox.Leave
            ValidarPuntoDestino(False)
        End Sub

        Private Sub Anexo2ComboBox_Leave(ByVal sender As System.Object, ByVal e As EventArgs) Handles Anexo2ComboBox.Leave
            ValidarAnexos(True)
            AnexoOkControl.Visible = True
        End Sub

        Private Sub CambioStickerDesktopCheckBox_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles CambioStickerDesktopCheckBox.CheckedChanged
            If CambioStickerDesktopCheckBox.Checked Then
                If ContenedorOkControl.OK = TriState.True Then
                    Dim showDialogCambioSticker As New ReempaqueDialog(_plugin, _otActual)
                    If showDialogCambioSticker.ShowDialog = DialogResult.OK Then
                        CodigoContenedorNuevoLabel.Text = ReempaqueDialog.NoContenedorNuevo
                        TipoContenedorNuevoLabel.Text = ReempaqueDialog.TipoContenedorNuevo
                    Else
                        CambioStickerDesktopCheckBox.Checked = False
                    End If
                Else
                    MessageBox.Show("Verifique el Número de Contenedor", "Empaque", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    CambioStickerDesktopCheckBox.Checked = False
                    ContenedorTextBox.Focus()
                End If
            Else
                CodigoContenedorNuevoLabel.Text = ""
                TipoContenedorNuevoLabel.Text = ""
                ReempaqueDialog.NoContenedorNuevo = ""
                ReempaqueDialog.TipoContenedorNuevo = ""
                ReempaqueDialog.TipoContenedorIndexNuevo = 0
                ReempaqueDialog.ContenedorCreado = False
            End If
        End Sub

        Private Sub LlenarCombos()
            Dim dbmAgrario As DBAgrarioDataBaseManager = Nothing
            Dim dbmCore As DBCoreDataBaseManager = Nothing
            Try
                dbmAgrario = New DBAgrarioDataBaseManager(Me._plugin.BancoAgrarioConnectionString)
                'dbmAgrario.DataBase.Identifier_Date_Format = _plugin.Manager.DesktopGlobal.IdentifierDateFormat
                dbmAgrario.Connection_Open(_plugin.Manager.Sesion.Usuario.id)


                dbmCore = New DBCoreDataBaseManager(_plugin.Manager.DesktopGlobal.ConnectionStrings.Core)
                'dbmCore.DataBase.Identifier_Date_Format = Configuration.ConfigurationManager.AppSettings("IdentifierDateFormat")
                dbmCore.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                Dim puntosData = dbmAgrario.SchemaConfig.TBL_Punto_Destino.DBGet(Nothing, 0, New TBL_Punto_DestinoEnumList(TBL_Punto_DestinoEnum.Nombre_Punto_Destino, True))
                Utilities.LlenarCombo(PuntoDestinoComboBox, puntosData, TBL_Punto_DestinoEnum.id_Punto_Destino.ColumnName, TBL_Punto_DestinoEnum.Nombre_Punto_Destino.ColumnName)

                Dim tipoContenedorData = dbmCore.SchemaConfig.TBL_Campo_Lista_Item.DBFindByfk_Entidadfk_Campo_Lista(_plugin.Manager.RiskGlobal.Entidad, Program.Banagrario_ListaTipoContenedorId, 0, New TBL_Campo_Lista_ItemEnumList(TBL_Campo_Lista_ItemEnum.Etiqueta_Campo_Lista_Item, True))
                Utilities.LlenarCombo(TipoContenedorComboBox, tipoContenedorData, TBL_Campo_Lista_ItemEnum.Valor_Campo_Lista_Item.ColumnName, TBL_Campo_Lista_ItemEnum.Etiqueta_Campo_Lista_Item.ColumnName)

                Dim anexoData = dbmCore.SchemaConfig.TBL_Anexos.DBFindByfk_Documento(Program.BanagrarioDocumentoTapaId)
                Utilities.LlenarCombo(Anexo1ComboBox, anexoData, TBL_AnexosEnum.id_Anexo.ColumnName, TBL_AnexosEnum.Nombre_Anexo.ColumnName)
                Utilities.LlenarCombo(Anexo2ComboBox, anexoData, TBL_AnexosEnum.id_Anexo.ColumnName, TBL_AnexosEnum.Nombre_Anexo.ColumnName)

                PuntoDestinoComboBox.SelectedIndex = -1
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Empaque", ex)
            Finally
                If (dbmAgrario IsNot Nothing) Then dbmAgrario.Connection_Close()
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            End Try
        End Sub

        Private Sub FinalizarUcs()
            If (_cajaActual Is Nothing) Then
                DesktopMessageBoxControl.DesktopMessageShow("No se diligenciado una caja valida", "FinalizarUCS", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                Return
            End If

            Dim dbmAgrario As DBAgrarioDataBaseManager = Nothing
            Try
                dbmAgrario = New DBAgrarioDataBaseManager(Me._plugin.BancoAgrarioConnectionString)
                'dbmAgrario.DataBase.Identifier_Date_Format = _plugin.Manager.DesktopGlobal.IdentifierDateFormat
                dbmAgrario.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                Dim contenedores = dbmAgrario.SchemaProcess.TBL_Caja_Contenedor.DBGet(_cajaActual.id_Caja, Nothing)
                If (contenedores.Count = 0) Then
                    Throw New Exception("No se ha ingresado ningún contenedor para la caja diligenciada")
                End If

                dbmAgrario.Transaction_Begin()

                _cajaActual.Cerrada = True
                _cajaActual.fk_Usuario_log = _plugin.Manager.Sesion.Usuario.id
                _cajaActual.Fecha_log = SlygNullable.SysDate 'Now
                dbmAgrario.SchemaProcess.TBL_Caja.DBUpdate(_cajaActual, _cajaActual.id_Caja)
                dbmAgrario.Transaction_Commit()
                DesktopMessageBoxControl.DesktopMessageShow("La caja se ha marcado como cerrada", "FinalizarUCS", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                Dim cajaDataTable = dbmAgrario.SchemaProcess.TBL_Caja.DBGet(_cajaActual.id_Caja)
                Dim formTulaEnlace As New FormContenedoresTulaEnlace(_plugin)
                formTulaEnlace.Show()
                formTulaEnlace.BuscarReporte(cajaDataTable(0).Codigo_Precinto)
                formTulaEnlace.PrecintoDesktopTextBox.Text = cajaDataTable(0).Codigo_Precinto
                LimpiarControlesContenedor(True, True, True)
            Catch ex As Exception
                Try : dbmAgrario.Transaction_Rollback() : Catch : End Try
                DesktopMessageBoxControl.DesktopMessageShow("Destape", ex)
            Finally
                If (dbmAgrario IsNot Nothing) Then dbmAgrario.Connection_Close()
            End Try

        End Sub

        Private Sub CambiarPrecintoSeleccionado()
            LimpiarControlesContenedor(False, True, True)
            HabilitarDestapePrecinto(True)

            If (ValidarCajaPrecinto(True)) Then
                If (Not _CajaActual Is Nothing AndAlso _CajaActual.Cerrada.Value) Then
                    DesktopMessageBoxControl.DesktopMessageShow("La caja ya se encuentra cerrada, necesitará autorización especial para realizar cambios", "Empaque", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                End If
            End If
            BloquearCaja()

            CargarFoldersPrecinto()
            'PuntoDestinoComboBox.Focus()

        End Sub

        Private Sub ImprimirReporte()
            Dim formContenedorTulaEnlace As New FormContenedoresTulaEnlace(_plugin)
            formContenedorTulaEnlace.PrecintoDesktopTextBox.Text = PrecintoCajaTextBox.Text
            formContenedorTulaEnlace.Show()
        End Sub

        'Private Sub CambiarContenedorSeleccionado()
        '    Try
        '        ContenedorOkControl.OK = TriState.False
        '        Dim mensaje As String = ""
        '        If (BuscarContenedor(ContenedorTextBox.Text, mensaje)) Then
        '            If (Not _ContenedorActual Is Nothing) Then
        '                ContenedorOkControl.OK = TriState.True
        '            Else
        '                If (BanAgrarioData.TBL_Caja_Contenedor.Select(TBL_Caja_ContenedorEnum.Codigo_Contenedor_Empaque.ColumnName & "=" & ContenedorTextBox.Text).Length = 0) Then
        '                    MessageBox.Show(mensaje & ", por favor verificar. ¿Desea especificar bolsas a empacar en este contenedor?", "Empaque", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '                    ContenedorOkControl.OK = TriState.False
        '                Else
        '                    ContenedorOkControl.OK = TriState.True
        '                End If
        '            End If
        '        Else
        '            ContenedorOkControl.OK = TriState.False
        '        End If
        '    Catch ex As Exception
        '        DesktopMessageBox.DesktopMessageShow("Destape", ex)
        '    Finally
        '    End Try
        'End Sub

        Function ValidarCantidad() As Boolean
            Try
                If (Not _ContenedorActual Is Nothing) Then
                    If (_ContenedorActual.CantidadDocumentos <> CInt(CantidadDocumentosTextBox.Text)) Then
                        CantidadOkControl.OK = TriState.UseDefault
                        DesktopMessageBoxControl.DesktopMessageShow("La cantidad de documentos diligenciados en el empaque no corresponde con la cantidad diligenciada en el destape, por favor verificar", "Empaque", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                        Return False
                    Else
                        CantidadOkControl.OK = TriState.True
                        Return True
                    End If
                End If

            Catch ex As Exception
                CantidadOkControl.OK = TriState.False
                CantidadOkControl.Visible = True
                DesktopMessageBoxControl.DesktopMessageShow("Destape", ex)
            End Try
            Return True
        End Function

#End Region

#Region " Metodos "

        Private Function ValidarCajaPrecinto(ByVal nMostrarMensaje As Boolean) As Boolean
            _CajaActual = Nothing

            Dim dbmAgrario As DBAgrarioDataBaseManager = Nothing

            Try
                If (PrecintoCajaTextBox.Text.Trim() = "") Then
                    Throw New Exception("El numero de caja es requerido")
                End If

                If (PrecintoCajaTextBox.Text.Length < Program.CajaMinCaracteres) Then
                    Throw New Exception("El numero de caja no puede ser menor a " + Program.CajaMinCaracteres.ToString() + " digitos")
                End If

                If (PrecintoCajaTextBox.Text.Length > Program.CajaMaxCaracteres) Then
                    Throw New Exception("El numero de caja no puede ser mayor a " + Program.CajaMaxCaracteres.ToString() + " digitos")
                End If

                dbmAgrario = New DBAgrarioDataBaseManager(Me._plugin.BancoAgrarioConnectionString)
                'dbmAgrario.DataBase.Identifier_Date_Format = _plugin.Manager.DesktopGlobal.IdentifierDateFormat
                dbmAgrario.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                Dim cajaExistenteData = dbmAgrario.SchemaProcess.TBL_Caja.DBFindByCodigo_Precinto(PrecintoCajaTextBox.Text)

                If (cajaExistenteData.Count > 0) Then
                    _CajaActual = cajaExistenteData(0).ToTBL_CajaType()
                End If

            Catch ex As Exception
                PrecintoOkControl.OK = TriState.False
                If (nMostrarMensaje) Then
                    DesktopMessageBoxControl.DesktopMessageShow(ex.Message, "Empaque", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                End If
                Return False
            Finally
                If (dbmAgrario IsNot Nothing) Then dbmAgrario.Connection_Close()
            End Try

            PrecintoOkControl.OK = TriState.True
            Return True
        End Function

        Private Sub BloquearCaja()
            PuntoDestinoComboBox.SelectedIndex = -1
            PuntoDestinoComboBox.Enabled = True

            If (Not _CajaActual Is Nothing) Then
                For Each item In PuntoDestinoComboBox.Items
                    Dim row = DirectCast(item, DataRowView).Row
                    If (row(TBL_Punto_DestinoEnum.id_Punto_Destino.ColumnName).ToString() = _CajaActual.fk_Punto_Destino.ToString()) Then
                        PuntoDestinoComboBox.SelectedItem = item
                        PuntoDestinoComboBox.Enabled = False
                        Exit For
                    End If
                Next
            End If

            If (PuntoDestinoComboBox.Enabled) Then
                PuntoDestinoComboBox.Focus()
            Else
                ContenedorTextBox.Focus()
            End If
        End Sub

        Private Sub CargarFoldersPrecinto()
            If (Not _CajaActual Is Nothing) Then
                Dim dbmAgrario As DBAgrarioDataBaseManager = Nothing
                Try
                    dbmAgrario = New DBAgrarioDataBaseManager(Me._plugin.BancoAgrarioConnectionString)
                    'dbmAgrario.DataBase.Identifier_Date_Format = _plugin.Manager.DesktopGlobal.IdentifierDateFormat
                    dbmAgrario.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                    Dim contenedoresData = dbmAgrario.SchemaProcess.TBL_Caja_Contenedor.DBGet(_CajaActual.id_Caja, Nothing)
                    BanAgrarioData.TBL_Caja_Contenedor.Rows.Clear()
                    BanAgrarioData.TBL_Caja_Contenedor.Merge(contenedoresData)
                    'ContenedoresDataGridView.DataSource = contenedoresData

                    PuntoDestinoComboBox.Enabled = Not (contenedoresData.Count > 0)

                Catch ex As Exception
                    DesktopMessageBoxControl.DesktopMessageShow("Destape", ex)
                Finally
                    If (dbmAgrario IsNot Nothing) Then dbmAgrario.Connection_Close()
                End Try
            End If
        End Sub

        Private Function ValidarPuntoDestino(ByVal nMostrarMensaje As Boolean) As Boolean
            If (PuntoDestinoComboBox.SelectedIndex = -1) Then
                Return MarcarValidacion("No se ha seleccionado el punto de destino", PuntoDestinoComboBox, nMostrarMensaje, PuntoOkControl)
            End If

            PuntoOkControl.OK = TriState.True
            Return True
        End Function

        Private Function ValidarContenedor(ByVal nMostrarMensaje As Boolean) As Boolean
            If (ContenedorTextBox.Text.Trim() = "") Then
                Return MarcarValidacion("No se ha ingresado el codigo del contenedor", ContenedorTextBox, nMostrarMensaje, ContenedorOkControl)
            End If

            ContenedorOkControl.OK = TriState.True
            Return True
        End Function

        Private Function ValidarCantidadDocumentos(ByVal nMostrarMensaje As Boolean) As Boolean
            If (CantidadDocumentosTextBox.Text.Trim() = "") Then
                Return MarcarValidacion("No se ha ingresado la cantidad de documentos del contenedor", CantidadDocumentosTextBox, nMostrarMensaje, CantidadOkControl)
            End If

            CantidadOkControl.OK = TriState.True
            Return True
        End Function

        Private Function ValidarAnexos(ByVal nMostrarMensaje As Boolean) As Boolean
            If (Anexo1ComboBox.SelectedIndex < 0) Then
                Return MarcarValidacion("El anexo  es requerido", Anexo1ComboBox, nMostrarMensaje, AnexoOkControl)
            End If
            If (CStr(Anexo1ComboBox.SelectedValue) <> CStr(Anexo2ComboBox.SelectedValue)) Then
                Return MarcarValidacion("El Anexo seleccionado no corresponde con la confirmación", Anexo1ComboBox, nMostrarMensaje, AnexoOkControl)
            End If
            Try
                ' ReSharper disable once UnusedVariable
                Dim val = CDec(Anexo1ComboBox.SelectedValue)
            Catch
                Return MarcarValidacion("El anexo seleccionado no tiene un codigo válido", Anexo1ComboBox, nMostrarMensaje, AnexoOkControl)
            End Try

            AnexoOkControl.OK = TriState.True
            AnexoOkControl.Visible = True
            Return True

        End Function

        Private Function ValidarContenedor() As Boolean
            Try
                If (Not ValidarCajaPrecinto(True)) Then
                    Return False
                End If

                If (Not ValidarPuntoDestino(True)) Then Return False
                If (Not ValidarContenedor(True)) Then Return False
                If (Not ValidarCantidadDocumentos(True)) Then Return False
                If (Not ValidarCantidad()) Then Return False
                If (Not ValidarAnexos(True)) Then Return False
                If (Not ValidarIgualarAnexo()) Then Return False
                If Not ReempaqueDialog.ContenedorCreado Then
                    If Not ValidarIgualdadTipoContenedor(True) Then Return False
                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow(ex.Message, "Empaque", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                Return False
            End Try

            Return True
        End Function

        'Private Function ValidarContenedorSoloDestape(ByVal nContenedorDestape As String) As Boolean
        '    Dim dbmCore As DBCoreDataBaseManager = Nothing
        '    Dim dmArchiving As DBArchivingDataBaseManager = Nothing

        '    Try
        '        dbmCore = New DBCoreDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Core)
        '        'dbmCore.DataBase.Identifier_Date_Format = _Plugin.Manager.DesktopGlobal.IdentifierDateFormat
        '        dbmCore.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

        '        dmArchiving = New DBArchivingDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Archiving)
        '        'dmArchiving.DataBase.Identifier_Date_Format = _Plugin.Manager.DesktopGlobal.IdentifierDateFormat
        '        dmArchiving.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

        '        Dim EntidadId As Short = _Plugin.Manager.RiskGlobal.Entidad
        '        Dim ProyectoId As Short = _Plugin.Manager.RiskGlobal.Proyecto

        '        Dim expCoreExistenteData = dbmCore.SchemaProcess.PA_Expediente_getByKeysEsquema.DBExecute(EntidadId, ProyectoId, Program.BanagrarioEsquemaRiskId, Nothing, Nothing, nContenedorDestape, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        '        If (expCoreExistenteData.Count = 0) Then
        '            Return False
        '        End If

        '        Const FolderId As Short = 1
        '        Dim contenedorEncontrado As Boolean = False

        '        For Each expCor In expCoreExistenteData
        '            Dim expArchivingData = dmArchiving.SchemaRisk.TBL_Folder.DBGet(expCor.id_Expediente, FolderId, Nothing)
        '            For Each expArc In expArchivingData
        '                If (expArc.fk_OT = _OT_actual) Then
        '                    contenedorEncontrado = True
        '                    Exit For
        '                End If
        '            Next
        '        Next

        '        If (Not contenedorEncontrado) Then
        '            Return False
        '        End If

        '        Return True
        '    Catch ex As Exception
        '        Return False
        '    Finally
        '        If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
        '        If (dmArchiving IsNot Nothing) Then dmArchiving.Connection_Close()
        '    End Try
        'End Function

        Private Function BuscarContenedor(ByVal nContenedorDestape As String, ByRef nMensaje As String) As Boolean
            _ContenedorActual = Nothing

            Dim dbmCore As DBCoreDataBaseManager = Nothing
            Dim dmArchiving As DBArchivingDataBaseManager = Nothing
            Dim dbmAgrario As DBAgrarioDataBaseManager = Nothing

            Try
                dbmCore = New DBCoreDataBaseManager(_plugin.Manager.DesktopGlobal.ConnectionStrings.Core)
                'dbmCore.DataBase.Identifier_Date_Format = _plugin.Manager.DesktopGlobal.IdentifierDateFormat
                dbmCore.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                dmArchiving = New DBArchivingDataBaseManager(_plugin.Manager.DesktopGlobal.ConnectionStrings.Archiving)
                'dmArchiving.DataBase.Identifier_Date_Format = _plugin.Manager.DesktopGlobal.IdentifierDateFormat
                dmArchiving.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                dbmAgrario = New DBAgrarioDataBaseManager(_plugin.BancoAgrarioConnectionString)
                dbmAgrario.Connection_Open(_plugin.Manager.Sesion.Usuario.id)


                Dim entidadId As Short = _plugin.Manager.RiskGlobal.Entidad
                Dim proyectoId As Short = _plugin.Manager.RiskGlobal.Proyecto

                Dim expCoreExistenteData = dbmCore.SchemaProcess.PA_Expediente_getByKeysEsquema.DBExecute(entidadId, proyectoId, Program.BanagrarioEsquemaRiskId, Nothing, Nothing, nContenedorDestape, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                If (expCoreExistenteData.Count = 0) Then
                    nMensaje = "El contenedor " & nContenedorDestape & " no ha sido destapado"
                    Return True
                End If

                Const folderId As Short = 1
                Dim contenedorEncontrado As Boolean = False
                Dim otEncontrada As Integer = -1

                Dim expCore As DBCore.SchemaProcess.TBL_ExpedienteType = Nothing

                For Each expCor In expCoreExistenteData
                    Dim expArchivingData = dmArchiving.SchemaRisk.TBL_Folder.DBGet(expCor.id_Expediente, folderId, Nothing)

                    For Each expArc In expArchivingData
                        If (expArc.fk_OT = _otActual) Then
                            If (expArc.fk_Estado < EstadoEnum.Empaque) Then
                                nMensaje = "El contenedor " & nContenedorDestape & " aun no tiene finalizado el destape"
                                Return True
                            Else
                                If (expArc.fk_Estado = EstadoEnum.Empacado) Then
                                    Dim contenedorDataEmpaque = dbmAgrario.SchemaProcess.TBL_Caja_Contenedor.DBFindByCodigo_Contenedor_Empaque(nContenedorDestape)
                                    Dim contenedorDataDestape = dbmAgrario.SchemaProcess.TBL_Caja_Contenedor.DBFindByCodigo_Contenedor_Destape(nContenedorDestape)
                                    If (contenedorDataEmpaque.Count > 0) Then
                                        Dim cajaExistente = _cajaActual
                                        If (cajaExistente Is Nothing OrElse cajaExistente.id_Caja.Value <> contenedorDataEmpaque(0).fk_Caja) Then
                                            cajaExistente = dbmAgrario.SchemaProcess.TBL_Caja.DBGet(contenedorDataEmpaque(0).fk_Caja)(0).ToTBL_CajaType
                                        End If
                                        ContenedorOkControl.OK = TriState.False
                                        nMensaje = "El contenedor " & nContenedorDestape & " ya fue Empacado" & "con el número de precinto " & cajaExistente.Codigo_Precinto.ToString()
                                    End If

                                    If (contenedorDataDestape.Count > 0) Then
                                        Dim cajaExistente = _cajaActual
                                        If (cajaExistente Is Nothing OrElse cajaExistente.id_Caja.Value <> contenedorDataDestape(0).fk_Caja) Then
                                            cajaExistente = dbmAgrario.SchemaProcess.TBL_Caja.DBGet(contenedorDataDestape(0).fk_Caja)(0).ToTBL_CajaType
                                        End If
                                        ContenedorOkControl.OK = TriState.False
                                        nMensaje = "El contenedor " & nContenedorDestape & " ya fue Empacado" & "con el número de precinto " & cajaExistente.Codigo_Precinto.ToString()
                                    End If

                                    Return True
                                End If
                            End If
                            contenedorEncontrado = True

                            _contenedorActual = New ContenedorInfo
                            expCore = expCor.ToTBL_ExpedienteType
                            _contenedorActual.Folder = expArc.ToTBL_FolderType()
                            Exit For
                        Else
                            otEncontrada = expArc.fk_OT
                        End If
                    Next
                Next

                If (Not contenedorEncontrado) Then
                    If (otEncontrada <> -1) Then
                        Dim otEncontradaData = dmArchiving.SchemaRisk.TBL_OT.DBGet(otEncontrada)
                        nMensaje = "El contenedor " & nContenedorDestape & " fue destapado en la fecha de proceso [" & otEncontradaData(0).Fecha_Proceso.ToString("yyyy-MM-dd") & " - OT(" & otEncontrada & ")], la cual es diferente a la seleccionada"
                    Else
                        nMensaje = "El contenedor " & nContenedorDestape & " no ha sido destapado"
                    End If
                    Return True
                End If

                _contenedorActual.FkExpediente = expCore.id_Expediente
                _contenedorActual.FkFolder = folderId
                _contenedorActual.FkOt = _contenedorActual.Folder.fk_OT
                _contenedorActual.PrecintoDestape = _contenedorActual.Folder.fk_Precinto

                Dim fileData = dbmCore.SchemaProcess.TBL_File.DBFindByfk_Expedientefk_Folderfk_Documento(expCore.id_Expediente, folderId, Program.BanagrarioDocumentoTapaId)
                If (fileData.Count = 0) Then Throw New Exception("Tapa no encontrada, expediente: " & expCore.id_Expediente.ToString() & " , folder: " & folderId.ToString() & " , tapa:" & Program.BanagrarioDocumentoTapaId.ToString())

                _contenedorActual.FkFile = fileData(0).id_File

                Dim cantidadData = dbmCore.SchemaProcess.TBL_File_Data.DBGet(_contenedorActual.FkExpediente, _contenedorActual.FkFolder, _contenedorActual.FkFile, Program.BanagrarioDocumentoTapaId, Program.Tapa_CampoId_CantidadFisicos)
                If (cantidadData.Count = 0) Then
                    Throw New Exception("No fue posible consultar la cantidad de fisicos capturados en el destape")
                End If

                _contenedorActual.CantidadDocumentos = CInt(cantidadData(0).Valor_File_Data)

                Dim tipoContenedorData = dbmCore.SchemaProcess.TBL_File_Data.DBGet(_contenedorActual.FkExpediente, _contenedorActual.FkFolder, _contenedorActual.FkFile, Program.BanagrarioDocumentoTapaId, Program.Tapa_CampoId_TipoContenedor)

                If (tipoContenedorData.Count = 0) Then
                    Throw New Exception("No fue posible consultar el tipo de contenedor capturado en el destape")
                End If


                _contenedorActual.TipoContenedor = CInt(tipoContenedorData(0).Valor_File_Data)


                Dim anexoData = dbmCore.SchemaProcess.TBL_File_Data.DBGet(_contenedorActual.FkExpediente, _contenedorActual.FkFolder, _contenedorActual.FkFile, Program.BanagrarioDocumentoTapaId, Program.Tapa_CampoId_Anexo)

                If (anexoData.Count = 0) Then
                    Throw New Exception("No fue posible consultar el Anexo capturado en el destape")
                End If

                _contenedorActual.Anexo = CInt(anexoData(0).Valor_File_Data)



                Dim tipoMovimiento = dbmCore.SchemaProcess.TBL_File_Data.DBGet(_contenedorActual.FkExpediente, _contenedorActual.FkFolder, _contenedorActual.FkFile, Program.BanagrarioDocumentoTapaId, Program.Tapa_CampoId_TipoMovimiento)

                If (tipoMovimiento.Count = 0) Then
                    Throw New Exception("No fue posible consultar el Tipo de Movimiento capturado en el destape")
                End If

                _contenedorActual.TipoMovimiento = CInt(tipoMovimiento(0).Valor_File_Data)



                Dim fechaMovimientoBac = dbmCore.SchemaProcess.TBL_File_Data.DBGet(_contenedorActual.FkExpediente, _contenedorActual.FkFolder, _contenedorActual.FkFile, Program.BanagrarioDocumentoTapaId, Program.Tapa_CampoId_Fecha)

                If (fechaMovimientoBac.Count = 0) Then
                    Throw New Exception("No fue posible consultar el Tipo de Movimiento capturado en el destape")
                End If

                _contenedorActual.FechaMovimiento = CStr(fechaMovimientoBac(0).Valor_File_Data)


                Dim documentosOrdenados = dbmCore.SchemaProcess.TBL_File_Data.DBGet(_contenedorActual.FkExpediente, _contenedorActual.FkFolder, _contenedorActual.FkFile, Program.BanagrarioDocumentoTapaId, Program.Tapa_CampoId_DocumentosOrdenados)

                If (documentosOrdenados.Count = 0) Then
                    Throw New Exception("No fue posible consultar el Tipo de Movimiento capturado en el destape")
                End If

                _contenedorActual.DocumentosOrdenados = CShort(documentosOrdenados(0).Valor_File_Data)





                Return True
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Destape", ex)
                Return False
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                If (dmArchiving IsNot Nothing) Then dmArchiving.Connection_Close()
                If (dbmAgrario IsNot Nothing) Then dbmAgrario.Connection_Close()
            End Try
        End Function

        Private Sub InsertarContenedor()
            If (Not ValidarContenedor()) Then
                Return
            End If

            Dim dbmAgrario As DBAgrarioDataBaseManager = Nothing
            Dim dbmCore As DBCoreDataBaseManager = Nothing
            Dim dmArchiving As DBArchivingDataBaseManager = Nothing

            Try
                Dim cajaPrecinto = PrecintoCajaTextBox.Text
                Dim contenedorDestape = ContenedorTextBox.Text
                Dim contenedorEmpaque = ContenedorTextBox.Text
                Dim contenedorDestapeControlOk = ContenedorOkControl

                Dim mensaje As String = ""
                BuscarContenedor(contenedorDestape, mensaje)
                If (_contenedorActual Is Nothing) Then
                    contenedorDestapeControlOk.OK = TriState.False
                    Throw New Exception(mensaje)
                End If

                dbmAgrario = New DBAgrarioDataBaseManager(Me._plugin.BancoAgrarioConnectionString)
                'dbmAgrario.DataBase.Identifier_Date_Format = _plugin.Manager.DesktopGlobal.IdentifierDateFormat
                dbmAgrario.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                dbmCore = New DBCoreDataBaseManager(_plugin.Manager.DesktopGlobal.ConnectionStrings.Core)
                'dbmCore.DataBase.Identifier_Date_Format = _plugin.Manager.DesktopGlobal.IdentifierDateFormat
                dbmCore.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                dmArchiving = New DBArchivingDataBaseManager(_plugin.Manager.DesktopGlobal.ConnectionStrings.Archiving)
                'dmArchiving.DataBase.Identifier_Date_Format = _plugin.Manager.DesktopGlobal.IdentifierDateFormat
                dmArchiving.Connection_Open(_plugin.Manager.Sesion.Usuario.id)



                'Dim contenedorData = dbmAgrario.SchemaProcess.TBL_Caja_Contenedor.DBFindByCodigo_Contenedor_Empaque(contenedorEmpaque)
                'If (contenedorData.Count > 0) Then
                '    Dim cajaExistente = _CajaActual
                '    If (cajaExistente Is Nothing OrElse cajaExistente.id_Caja.Value <> contenedorData(0).fk_Caja) Then
                '        cajaExistente = dbmAgrario.SchemaProcess.TBL_Caja.DBGet(contenedorData(0).fk_Caja)(0).ToTBL_CajaType
                '    End If
                '    contenedorDestapeControlOK.OK = TriState.False
                '    Throw New Exception("Contenedor " & contenedorDestape & " ya fue empacado en la caja con precinto " & cajaExistente.Codigo_Precinto.ToString())
                'End If

                Dim contenedorDataEmpaque = dbmAgrario.SchemaProcess.TBL_Caja_Contenedor.DBFindByCodigo_Contenedor_Empaque(contenedorEmpaque)
                Dim contenedorDataDestape = dbmAgrario.SchemaProcess.TBL_Caja_Contenedor.DBFindByCodigo_Contenedor_Destape(contenedorEmpaque)
                If (contenedorDataEmpaque.Count > 0) Then
                    Dim cajaExistente = _cajaActual
                    If (cajaExistente Is Nothing OrElse cajaExistente.id_Caja.Value <> contenedorDataEmpaque(0).fk_Caja) Then
                        cajaExistente = dbmAgrario.SchemaProcess.TBL_Caja.DBGet(contenedorDataEmpaque(0).fk_Caja)(0).ToTBL_CajaType
                    End If
                    ContenedorOkControl.OK = TriState.False
                    Throw New Exception("Contenedor " & contenedorDestape & " ya fue empacado en la caja con precinto " & cajaExistente.Codigo_Precinto.ToString())
                End If

                If (contenedorDataDestape.Count > 0) Then
                    Dim cajaExistente = _cajaActual
                    If (cajaExistente Is Nothing OrElse cajaExistente.id_Caja.Value <> contenedorDataDestape(0).fk_Caja) Then
                        cajaExistente = dbmAgrario.SchemaProcess.TBL_Caja.DBGet(contenedorDataDestape(0).fk_Caja)(0).ToTBL_CajaType
                    End If
                    ContenedorOkControl.OK = TriState.False
                    Throw New Exception("Contenedor " & contenedorDestape & " ya fue empacado en la caja con precinto " & cajaExistente.Codigo_Precinto.ToString())

                End If

                If (_cajaActual Is Nothing) Then
                    Dim cajaData = dbmAgrario.SchemaProcess.TBL_Caja.DBFindByCodigo_Precinto(cajaPrecinto)
                    If (cajaData.Count > 0) Then
                        _cajaActual = cajaData(0).ToTBL_CajaType()

                        If (_cajaActual.Cerrada) Then
                            If (Not _plugin.Manager.Sesion.Usuario.PerfilManager.PuedeEditar(Permisos.Imaging.Proceso.Captura.Reprocesos)) Then
                                If (Not Utilities.ValidarPermiso(Permisos.Imaging.Proceso.Control.Autorizaciones, Program.AccesoDesktopAssembly, "Autorizar el empaque del contenedor " & contenedorEmpaque & ", en la caja con precinto " & cajaPrecinto & " que se encuentra cerrada", _plugin.Manager.Sesion.Usuario, _plugin.Manager.DesktopGlobal.SecurityServiceURL, _plugin.Manager.DesktopGlobal.ClientIPAddress)) Then
                                    PrecintoOkControl.OK = TriState.False
                                    Throw New Exception("No se permitio el empaque del contenedor en la caja cerrada")
                                End If
                            End If
                        End If
                    Else
                        dbmAgrario.Transaction_Begin()

                        _cajaActual = New TBL_CajaType()
                        _cajaActual.id_Caja = dbmAgrario.SchemaProcess.TBL_Caja.DBNextId()
                        _cajaActual.Codigo_Precinto = PrecintoCajaTextBox.Text
                        _cajaActual.fk_Punto_Destino = CInt(PuntoDestinoComboBox.SelectedValue)
                        _cajaActual.Cerrada = False
                        _cajaActual.Fecha_log = SlygNullable.SysDate 'Now
                        _cajaActual.fk_Usuario_log = _plugin.Manager.Sesion.Usuario.id
                        dbmAgrario.SchemaProcess.TBL_Caja.DBInsert(_cajaActual)
                    End If
                End If

                If (dbmAgrario.DataBase.Transaction Is Nothing) Then dbmAgrario.Transaction_Begin()

                'Validar la integridad de las llaves del folder, para verificar si hay un error al empacar
                If (_contenedorActual Is Nothing OrElse _contenedorActual.FkExpediente = -1 OrElse _contenedorActual.FkFolder = -1) Then
                    Throw New Exception("El contenedor no tiene los datos requeridos para ejecutar el cambio de estado a Empacado")
                End If

                dbmCore.Transaction_Begin()
                dmArchiving.Transaction_Begin()

                Dim codigoContenedorFinal As String

                If ReempaqueDialog.ContenedorCreado Then
                    InsertarNuevoContedor(ReempaqueDialog.NoContenedorNuevo, dbmCore, dmArchiving)
                    codigoContenedorFinal = ReempaqueDialog.NoContenedorNuevo
                Else
                    codigoContenedorFinal = contenedorEmpaque
                End If

                dbmCore.SchemaProcess.TBL_Folder_estado.DBUpdate(_contenedorActual.FkExpediente, _contenedorActual.FkFolder, DesktopConfig.Modulo.Archiving, EstadoEnum.Empacado, _
                                                                 _plugin.Manager.Sesion.Usuario.id, SlygNullable.SysDate, _contenedorActual.FkExpediente, _contenedorActual.FkFolder, DesktopConfig.Modulo.Archiving)

                dbmCore.SchemaProcess.TBL_File_Estado.DBUpdate(_contenedorActual.FkExpediente, _contenedorActual.FkFolder, _contenedorActual.FkFile, DesktopConfig.Modulo.Archiving, _
                                                               EstadoEnum.Empacado, _plugin.Manager.Sesion.Usuario.id, SlygNullable.SysDate, _contenedorActual.FkExpediente, _contenedorActual.FkFolder, _contenedorActual.FkFile, DesktopConfig.Modulo.Archiving)

                _contenedorActual.Folder.fk_Estado = EstadoEnum.Empacado
                dmArchiving.SchemaRisk.TBL_Folder.DBUpdate(_contenedorActual.Folder, _contenedorActual.FkExpediente, _contenedorActual.FkFolder, _contenedorActual.Folder.fk_OT)

                Dim riskFileData = dmArchiving.SchemaRisk.TBL_File.DBGet(_contenedorActual.FkOt, _contenedorActual.FkFolder, _contenedorActual.FkFile, _contenedorActual.FkExpediente)
                If (riskFileData.Count = 0) Then Throw New Exception("File no encontrado en archiving , expediente: " & _contenedorActual.FkExpediente.ToString() & ", folder: " _
                                                                     & _contenedorActual.FkFolder.ToString() & ", OT:" & _contenedorActual.FkOt.ToString() & " , tapa:" & Program.BanagrarioDocumentoTapaId.ToString())

                riskFileData(0).fk_Estado = EstadoEnum.Empacado
                dmArchiving.SchemaRisk.TBL_File.DBUpdate(riskFileData(0).ToTBL_FileType, riskFileData(0).fk_OT, riskFileData(0).fk_Folder, riskFileData(0).id_File, riskFileData(0).fk_expediente)

                Dim contenedor As New TBL_Caja_ContenedorType()
                contenedor.fk_Caja = _cajaActual.id_Caja
                contenedor.id_Caja_Contenedor = dbmAgrario.SchemaProcess.TBL_Caja_Contenedor.DBNextId(contenedor.fk_Caja)
                contenedor.Codigo_Contenedor_Destape = contenedorDestape
                contenedor.Codigo_Contenedor_Empaque = codigoContenedorFinal
                contenedor.Cantidad_Documentos = CInt(CantidadDocumentosTextBox.Text)
                contenedor.Fecha_log = SlygNullable.SysDate ' Now
                contenedor.fk_Usuario_log = _plugin.Manager.Sesion.Usuario.id

                dbmAgrario.SchemaProcess.TBL_Caja_Contenedor.DBInsert(contenedor)

                Dim contenedoresData = dbmAgrario.SchemaProcess.TBL_Caja_Contenedor.DBGet(_cajaActual.id_Caja, Nothing)
                'ContenedoresDataGridView.DataSource = contenedoresData
                BanAgrarioData.TBL_Caja_Contenedor.Rows.Clear()
                BanAgrarioData.TBL_Caja_Contenedor.Merge(contenedoresData)

                dbmAgrario.Transaction_Commit()
                dbmCore.Transaction_Commit()
                dmArchiving.Transaction_Commit()

                LimpiarControlesContenedor(False, False, False)
                CargarFoldersPrecinto()
                lblContenedor.Focus()

            Catch ex As Exception
                Try : dbmAgrario.Transaction_Rollback() : Catch : End Try
                Try : dbmCore.Transaction_Rollback() : Catch : End Try
                Try : dmArchiving.Transaction_Rollback() : Catch : End Try
                DesktopMessageBoxControl.DesktopMessageShow("Empaque", ex)
            Finally
                If (dbmAgrario IsNot Nothing) Then dbmAgrario.Connection_Close()
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                If (dmArchiving IsNot Nothing) Then dmArchiving.Connection_Close()
            End Try

            CargarFoldersPrecinto()
        End Sub

        Private Function ValidarIgualdadTipoContenedor(ByVal nMostrarMensaje As Boolean) As Boolean
            If _contenedorActual.TipoContenedor <> CInt(TipoContenedorComboBox.SelectedValue) Then
                If nMostrarMensaje Then
                    DesktopMessageBoxControl.DesktopMessageShow("El Tipo de Contenedor seleccionado no corresponde al elegido en la etapa de Destape", "Empaque", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                End If
                OkControlTipoContendor.Visible = True
                OkControlTipoContendor.OK = TriState.False
                Return False
            End If
            OkControlTipoContendor.Visible = True
            OkControlTipoContendor.OK = TriState.True
            Return True
        End Function

        Private Function ValidarIgualarAnexo() As Boolean
            If _contenedorActual.Anexo <> CInt(Anexo1ComboBox.SelectedValue) Then
                DesktopMessageBoxControl.DesktopMessageShow("El Anexo seleccionado no corresponde al elegido en la etapa de Destape", "Empaque", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                AnexoOkControl.OK = TriState.False
                Return False
            End If
            AnexoOkControl.OK = TriState.True
            Return True
        End Function

        Private Sub HabilitarDestapePrecinto(ByVal nEnabled As Boolean)
            ContenedorEncabezadoGroupBox.Enabled = nEnabled
            BotonesPanel.Enabled = nEnabled
            ContenedoresGroupBox.Enabled = nEnabled
        End Sub

        Private Sub LimpiarControlesContenedor(ByVal nLimpiarPrecinto As Boolean, ByVal nLimpiarPuntoDestino As Boolean, ByVal nLimpiarGrillaContenedores As Boolean)
            ContenedorTextBox.Text = ""
            ContenedorOkControl.OK = TriState.UseDefault


            CantidadDocumentosTextBox.Text = ""
            CantidadOkControl.OK = TriState.UseDefault

            _ultimoContenedor = ""
            _ultimaCantidad = ""
            _contenedorActual = Nothing

            If (nLimpiarPrecinto) Then
                PrecintoCajaTextBox.Text = ""
                _ultimoPrecinto = ""
                PrecintoOkControl.OK = TriState.UseDefault
            End If

            If (nLimpiarPuntoDestino) Then
                PuntoDestinoComboBox.SelectedIndex = -1
                PuntoOkControl.OK = TriState.UseDefault
            End If

            AnexoLabel.Visible = False

            CodigoContenedorNuevoLabel.Text = ""
            TipoContenedorNuevoLabel.Text = ""
            ReempaqueDialog.NoContenedorNuevo = ""
            ReempaqueDialog.TipoContenedorIndexNuevo = 0
            ReempaqueDialog.TipoContenedorNuevo = ""
            ReempaqueDialog.ContenedorCreado = False

            Anexo1ComboBox.SelectedIndex = -1
            Anexo2ComboBox.SelectedIndex = -1

            ContenedorOkControl.Visible = False
            CantidadOkControl.Visible = False
            OkControlTipoContendor.Visible = False
            AnexoOkControl.Visible = False

            TipoContenedorComboBox.SelectedIndex = -1
            CambioStickerDesktopCheckBox.Checked = False

            If (nLimpiarGrillaContenedores) Then
                'ContenedoresDataGridView.DataSource = Nothing
                BanAgrarioData.TBL_Caja_Contenedor.Rows.Clear()
            End If
        End Sub

        'Private Function MarcarValidacion(ByVal nMensaje As String, ByVal nControl As Control) As Boolean
        '    'MessageBox.Show(nMensaje)
        '    DesktopMessageBox.DesktopMessageShow(nMensaje, "Destape", DesktopMessageBox.IconEnum.WarningIcon, True)

        '    nControl.Focus()
        '    Return False
        'End Function

        Private Sub InsertarNuevoContedor(ByVal nContenedorNuevo As String, ByRef dbmCore As DBCoreDataBaseManager, ByRef dmArchiving As DBArchivingDataBaseManager)
            Dim objFolderCore As DesktopConfig.FolderCORE
            Try
                Dim precinto As String = _contenedorActual.PrecintoDestape

                Dim key1Oficina = _contenedorActual.Oficina
                Dim key2FechaString = _contenedorActual.FechaMovimiento
                Dim key3Contenedor = nContenedorNuevo



                'Lista de llaves Expediente
                Dim listLlavesExpediente As New List(Of DesktopConfig.StrLlaves)
                listLlavesExpediente.Add(New DesktopConfig.StrLlaves() With {.id_Llave = Program.Tapa_CampoId_Oficina, .Tipo = DesktopConfig.CampoTipo.Texto, .Valor_Llave = CStr(key1Oficina), .Nombre_Llave = "Oficina"})
                listLlavesExpediente.Add(New DesktopConfig.StrLlaves() With {.id_Llave = Program.Tapa_CampoId_Fecha, .Tipo = DesktopConfig.CampoTipo.Fecha, .Valor_Llave = key2FechaString, .Nombre_Llave = "Fecha"})
                listLlavesExpediente.Add(New DesktopConfig.StrLlaves() With {.id_Llave = Program.Tapa_CampoId_Contenedor, .Tipo = DesktopConfig.CampoTipo.Texto, .Valor_Llave = key3Contenedor, .Nombre_Llave = "Codigo Contenedor"})

                objFolderCore = Utilities.InsertaFolderCore(dbmCore, dmArchiving, _plugin.Manager.RiskGlobal, Program.BanagrarioEsquemaRiskId, listLlavesExpediente, _plugin.Manager.DesktopGlobal.ConnectionStrings, _plugin.Manager.Sesion.Usuario.id, EstadoEnum.Empacado)
                Utilities.InsertaFolderArchiving(dmArchiving, dbmCore, objFolderCore.Expediente, objFolderCore.Folder, _otActual, precinto, EstadoEnum.Empacado)

                Dim idFile = Utilities.InsertaFileCore(dbmCore, dmArchiving, objFolderCore.Expediente, objFolderCore.Folder, Program.BanagrarioDocumentoTapaId, _plugin.Manager.RiskGlobal)
                Utilities.InsertaFileArchiving(dmArchiving, dbmCore, idFile, objFolderCore.Folder, objFolderCore.Expediente, Nothing, False, DesktopConfig.RegistroTipo.Nuevo, _otActual, Nothing, EstadoEnum.Empacado, True, _plugin.Manager.Sesion.Usuario.id)

                Dim objFileData As New DBCore.SchemaProcess.TBL_File_DataType() With {.fk_Expediente = objFolderCore.Expediente, .fk_Folder = objFolderCore.Folder, .fk_File = idFile, .fk_Documento = Program.BanagrarioDocumentoTapaId}

                'Oficina
                objFileData.fk_Campo = Program.Tapa_CampoId_Oficina
                objFileData.Valor_File_Data = key1Oficina
                objFileData.Conteo_File_Data = objFileData.Valor_File_Data.ToString().Length
                dbmCore.SchemaProcess.TBL_File_Data.DBInsert(objFileData)

                'Fecha Movimiento
                objFileData.fk_Campo = Program.Tapa_CampoId_Fecha
                objFileData.Valor_File_Data = key2FechaString
                objFileData.Conteo_File_Data = objFileData.Valor_File_Data.ToString().Length
                dbmCore.SchemaProcess.TBL_File_Data.DBInsert(objFileData)

                'Código de Barras Contenedor Nuevo
                objFileData.fk_Campo = Program.Tapa_CampoId_Contenedor
                objFileData.Valor_File_Data = key3Contenedor
                objFileData.Conteo_File_Data = objFileData.Valor_File_Data.ToString().Length
                dbmCore.SchemaProcess.TBL_File_Data.DBInsert(objFileData)

                'Cantidad Según Contenedor
                objFileData.fk_Campo = Program.Tapa_CampoId_CantidadSegunContenedor
                objFileData.Valor_File_Data = _contenedorActual.CantidadDocumentos
                objFileData.Conteo_File_Data = objFileData.Valor_File_Data.ToString().Length
                dbmCore.SchemaProcess.TBL_File_Data.DBInsert(objFileData)

                'Cantidad Real
                objFileData.fk_Campo = Program.Tapa_CampoId_CantidadFisicos
                objFileData.Valor_File_Data = _contenedorActual.CantidadDocumentos
                objFileData.Conteo_File_Data = objFileData.Valor_File_Data.ToString().Length
                dbmCore.SchemaProcess.TBL_File_Data.DBInsert(objFileData)

                'Documentos Ordenados
                objFileData.fk_Campo = Program.Tapa_CampoId_DocumentosOrdenados
                objFileData.Valor_File_Data = _contenedorActual.DocumentosOrdenados
                objFileData.Conteo_File_Data = objFileData.Valor_File_Data.ToString().Length
                dbmCore.SchemaProcess.TBL_File_Data.DBInsert(objFileData)

                'Tipo Contenedor
                objFileData.fk_Campo = Program.Tapa_CampoId_TipoContenedor
                objFileData.Valor_File_Data = ReempaqueDialog.TipoContenedorIndexNuevo
                objFileData.Conteo_File_Data = objFileData.Valor_File_Data.ToString().Length
                dbmCore.SchemaProcess.TBL_File_Data.DBInsert(objFileData)

                'Tipo Movimiento
                objFileData.fk_Campo = Program.Tapa_CampoId_TipoMovimiento
                objFileData.Valor_File_Data = _contenedorActual.TipoMovimiento
                objFileData.Conteo_File_Data = objFileData.Valor_File_Data.ToString().Length
                dbmCore.SchemaProcess.TBL_File_Data.DBInsert(objFileData)

                'Anexos
                objFileData.fk_Campo = Program.Tapa_CampoId_Anexo
                objFileData.Valor_File_Data = _contenedorActual.Anexo
                objFileData.Conteo_File_Data = objFileData.Valor_File_Data.ToString().Length
                dbmCore.SchemaProcess.TBL_File_Data.DBInsert(objFileData)

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("A ocurrido un error al intentar insertar el nuevo Contenedor", ex)
            End Try


        End Sub

        Private Function MarcarValidacion(ByVal nMensaje As String, ByVal nControl As Control, ByVal nMostrarMensaje As Boolean, ByVal nOkControl As OkControl) As Boolean
            If (Not nOkControl Is Nothing) Then
                nOkControl.OK = TriState.False
            End If

            If (nMostrarMensaje) Then
                DesktopMessageBoxControl.DesktopMessageShow(nMensaje, "Empaque", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                nControl.Focus()
            End If

            Return False
        End Function

#End Region

    End Class

    Public Class ContenedorInfo
        Public ContenedorDestape As String = ""
        Public CantidadDocumentos As Integer = 0
        Public TipoContenedor As Integer = 0
        Public TipoMovimiento As Integer = 0
        Public Anexo As Integer = 0
        Public Oficina As Integer = 0
        Public FechaMovimiento As String = ""
        Public DocumentosOrdenados As Short = 0


        Public FkExpediente As Long = -1
        Public FkFolder As Short = -1
        Public FkFile As Short = -1
        Public FkOt As Integer = -1
        Public Folder As TBL_FolderType = Nothing
        Public PrecintoDestape As String = ""
    End Class

End Namespace