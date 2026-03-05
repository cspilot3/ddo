Imports DBArchiving
Imports DBImaging
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Controls.DesktopTextBox
Imports Miharu.Desktop.Library
Imports DBCore
Imports DBCore.SchemaConfig
Imports DBAgrario
Imports System.Windows.Forms
Imports DBCore.SchemaProcess
Imports DBAgrario.SchemaProcess
Imports Banagrario.Plugin.Risk.Forms.OT
Imports SLYG.Tools
Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Controls.Utils

Namespace Risk.Forms.Destape

    Public Class FormDestape

#Region " Declaraciones "

        Private _Plugin As BanagrarioRiskPlugin

        Private _OT_actual As Integer = 0
        Private _OT_Fecha As String = ""
        Private _UltimoPrecinto As String = ""
        Private Id_MesaDestape As Short

        Private _PermiteIgualarValor As Boolean = True

        Private _ContenedorEdicion As CTA_Destape_DetalleType = Nothing

        Private _PrecintoTextBox_LeaveEjecutado As Boolean = False


        Private oficina As String ''Controla el Codigo de la oficina al activar el foco
        Private anexocombo As String  ''Controla el Codigo de la anexo al activar el foco 
        Private EsCob As Boolean = False

        Private loading As Boolean = False

#End Region

#Region " Constructores "

        Public Sub New(ByVal nBanagrarioDesktopPlugin As BanagrarioRiskPlugin)
            InitializeComponent()

            _Plugin = nBanagrarioDesktopPlugin

            '_SedeProcesamiento = nSedeProcesamiento
            '_CentroProcesamiento = nCentroProcesamiento
        End Sub

#End Region

#Region " Eventos "

        Public Sub ControlLostFocus(ByVal sender As System.Object, ByVal e As EventArgs)
            Dim TextBoxLostFocus As DesktopTextBoxControl = CType(sender, DesktopTextBoxControl)
            TextBoxLostFocus.PasswordChar = CChar("*")
        End Sub

        Public Sub ControlGotFocus(ByVal sender As System.Object, ByVal e As EventArgs)
            Dim TextBoxLostFocus As DesktopTextBoxControl = CType(sender, DesktopTextBoxControl)
            TextBoxLostFocus.PasswordChar = CChar("")
        End Sub

        Private Sub FormDestape_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            Dim seleccionarOt As New FormSeleccionarOT(_Plugin)
            If (seleccionarOt.ShowDialog() = DialogResult.OK) Then
                _OT_actual = seleccionarOt.OT_Seleccionada
                ' _OT_Fecha = seleccionarOt.OT_Fecha
                _OT_Fecha = GetFechaOT(_OT_actual)
                '_CentroProcesamiento = seleccionarOt.CentroProcesamiento
                '_SedeProcesamiento = seleccionarOt.SedeProcesamiento
                LlenarCombos()
                LimpiarControlesContenedor(True, True, True)
                HabilitarDestapePrecinto(False)

                AddHandler CodigoContenedor1TextBox.LostFocus, AddressOf ControlLostFocus
                AddHandler CodigoContenedor1TextBox.GotFocus, AddressOf ControlGotFocus

            Else
                Close()
            End If

            CargarValidacionOrdenamiento()
            CargarNovedades()
        End Sub

        Private Function GetFechaOT(ByVal id_Ot As Integer) As String
            Dim dmArchiving As DBArchivingDataBaseManager = Nothing

            Try
                ' Dim _EntidadProcesamientoId = _Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad
                ' Dim _SedeProcesamientoId = CShort(SedeComboBox.SelectedValue)

                dmArchiving = New DBArchivingDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Archiving)
                dmArchiving.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
                dmArchiving.Transaction_Begin()
                Dim OT_DataTable = dmArchiving.SchemaRisk.TBL_OT.DBFindByid_OT(id_Ot)
                Dim fecha As String = OT_DataTable(0).Fecha_OT.ToString("dd-MM-yyyy")
                Return fecha
            Catch ex As Exception
                Return "Fecha no valida" + ex.Message
            Finally
                If (dmArchiving IsNot Nothing) Then dmArchiving.Connection_Close()
            End Try
        End Function

        Private Sub FormDestape_KeyUp(ByVal sender As System.Object, ByVal e As KeyEventArgs) Handles MyBase.KeyUp
            If (Me.Enabled) Then
                If (((e.Control And e.KeyCode = Keys.I) Or e.KeyCode = Keys.F9) And InsertarContenedorButton.Visible And InsertarContenedorButton.Enabled) Then
                    InsertarContenedor()
                End If
                If (((e.Control And e.KeyCode = Keys.G) Or e.KeyCode = Keys.F10) And GuardarContenedorButton.Visible And GuardarContenedorButton.Enabled) Then
                    ActualizarContenedor()
                End If
                If (((e.Control And e.KeyCode = Keys.F) Or e.KeyCode = Keys.F5) And FinalizarButton.Visible And FinalizarButton.Enabled) Then
                    FinalizarUCS()
                End If
            End If
        End Sub

        Private Sub PrecintoTextBox_Enter(ByVal sender As Object, ByVal e As EventArgs) Handles PrecintoTextBox.Enter
            _UltimoPrecinto = PrecintoTextBox.Text
        End Sub

        Private Sub PrecintoTextBox_KeyUp(ByVal sender As Object, ByVal e As KeyEventArgs) Handles PrecintoTextBox.KeyUp
            If (e.KeyCode = Keys.Enter) Then
                If (_UltimoPrecinto <> PrecintoTextBox.Text) Then
                    CambiarPrecintoSeleccionado()
                    _PrecintoTextBox_LeaveEjecutado = True
                End If
            End If
        End Sub

        Private Sub PrecintoTextBox_Leave(ByVal sender As System.Object, ByVal e As EventArgs) Handles PrecintoTextBox.Leave
            If (Not _PrecintoTextBox_LeaveEjecutado) Then
                If (_UltimoPrecinto <> PrecintoTextBox.Text) Then
                    CambiarPrecintoSeleccionado()
                End If
            End If
            lblCodigoOficina.Visible = False
            _PrecintoTextBox_LeaveEjecutado = False
        End Sub

        Private Sub CodOficina1ComboBox_KeyUp(ByVal sender As Object, ByVal e As KeyEventArgs) Handles CodOficina1ComboBox.KeyUp
            If (CodOficina1ComboBox.SelectedIndex <> -1) Then
                If EsCob = False Then
                    oficina = CodOficina1ComboBox.SelectedValue.ToString()
                    If (e.KeyCode = Keys.Enter) Then
                        CodigoContenedor1TextBox.Focus()
                    End If
                Else
                    If (e.KeyCode = Keys.Enter) Then
                        CodOficinaCobComboBox.Focus()
                    End If
                End If

            End If
        End Sub

        Private Sub CodOficina1ComboBox_SelectedValueChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles CodOficina1ComboBox.SelectedValueChanged
            IgualarOficinaValor(CodOficina1ComboBox, NombreOficina1ComboBox)
        End Sub

        Private Sub CodOficina1ComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles CodOficina1ComboBox.SelectedIndexChanged
            If CodOficina1ComboBox.SelectedIndex <> -1 Then
                ValidarTipoOficina()
            End If
        End Sub

        Private Sub CodOficinaCobComboBox_KeyUp(ByVal sender As System.Object, ByVal e As KeyEventArgs) Handles CodOficinaCobComboBox.KeyUp
            If (CodOficinaCobComboBox.SelectedIndex <> -1) Then
                oficina = CodOficinaCobComboBox.SelectedValue.ToString()
                If (e.KeyCode = Keys.Enter) Then
                    CodigoContenedor1TextBox.Focus()
                End If
            End If
        End Sub

        Private Sub CodOficinaCobComboBox_SelectedValueChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles CodOficinaCobComboBox.SelectedValueChanged
            If (Not loading) Then IgualarOficinaCobValor(CodOficinaCobComboBox, NombreOficinaCobComboBox)
        End Sub

        Private Sub CodOficina2ComboBox_SelectedValueChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles CodOficina2ComboBox.SelectedValueChanged
            If (Not loading) Then
                If (Not EsCob) Then
                    IgualarOficinaValor(CodOficina2ComboBox, NombreOficina2ComboBox)
                Else
                    IgualarOficinaCobValor(CodOficina2ComboBox, NombreOficina2ComboBox)
                End If
            End If
        End Sub

        Private Sub CodOficina2ComboBox_KeyUp(ByVal sender As Object, ByVal e As KeyEventArgs) Handles CodOficina2ComboBox.KeyUp
            If (e.KeyCode = Keys.Enter) Then
                oficina = CodOficina2ComboBox.SelectedValue.ToString()
                InsertarContenedorButton.Focus()
                'FechaMovimiento2MaskedTextBox.Focus()
                CodOficina2ComboBox.SelectedValue = oficina
            End If
        End Sub

        Private Sub CodOficina2ComboBox_Leave(ByVal sender As System.Object, ByVal e As EventArgs) Handles CodOficina2ComboBox.Leave
            ValidarOficina(False)
        End Sub

        Private Sub CodOficina2ComboBox_Enter(ByVal sender As System.Object, ByVal e As EventArgs) Handles CodOficina2ComboBox.Enter
            lblCodigoOficina.Visible = True
            lblNombreOficina.Visible = True
        End Sub

        Private Sub NombreOficina1ComboBox_SelectedValueChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles NombreOficina1ComboBox.SelectedValueChanged
            IgualarOficinaValor(NombreOficina1ComboBox, CodOficina1ComboBox)
        End Sub

        Private Sub NombreOficina1ComboBox_KeyUp(ByVal sender As Object, ByVal e As KeyEventArgs) Handles NombreOficina1ComboBox.KeyUp
            If (e.KeyCode = Keys.Enter) Then
                If EsCob = False Then
                    If (NombreOficina1ComboBox.SelectedIndex <> -1) Then
                        OficinaConfirmarPanel.Visible = True
                        OficinaCobPanel.Visible = False
                    End If
                    CodigoContenedor1TextBox.Focus()
                Else
                    If (NombreOficina1ComboBox.SelectedIndex <> -1) Then
                        OficinaCobPanel.Visible = True
                        OficinaConfirmarPanel.Visible = False
                    End If
                    CodOficinaCobComboBox.Focus()
                End If

            End If
        End Sub

        Private Sub NombreOficina1ComboBox_Leave(ByVal sender As System.Object, ByVal e As EventArgs) Handles NombreOficina1ComboBox.Leave
            If (CodOficina1ComboBox.SelectedIndex <> -1) Then
                If EsCob = False Then
                    OficinaConfirmarPanel.Visible = True
                    OficinaCobPanel.Visible = False
                    CodigoContenedor1TextBox.Focus()
                Else
                    OficinaConfirmarPanel.Visible = False
                    OficinaCobPanel.Visible = True
                    CodOficinaCobComboBox.Focus()
                End If
            End If

        End Sub

        Private Sub NombreOficinaCobComboBox_SelectedValueChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles NombreOficinaCobComboBox.SelectedValueChanged
            IgualarOficinaCobValor(NombreOficinaCobComboBox, CodOficinaCobComboBox)
        End Sub

        Private Sub NombreOficinaCobComboBox_KeyUp(ByVal sender As System.Object, ByVal e As KeyEventArgs) Handles NombreOficinaCobComboBox.KeyUp
            If (e.KeyCode = Keys.Enter) Then
                If (NombreOficinaCobComboBox.SelectedIndex <> -1) Then
                    OficinaConfirmarPanel.Visible = True
                End If
                CodigoContenedor1TextBox.Focus()
            End If
        End Sub

        Private Sub NombreOficinaCobComboBox_Leave(ByVal sender As System.Object, ByVal e As EventArgs) Handles NombreOficinaCobComboBox.Leave
            If (CodOficinaCobComboBox.SelectedIndex <> -1) Then
                OficinaConfirmarPanel.Visible = True
            End If
            CodigoContenedor1TextBox.Focus()
        End Sub

        Private Sub NombreOficina2ComboBox_SelectedValueChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles NombreOficina2ComboBox.SelectedValueChanged
            If EsCob = False Then
                IgualarOficinaValor(NombreOficina2ComboBox, CodOficina2ComboBox)
            Else
                IgualarOficinaCobValor(NombreOficina2ComboBox, CodOficina2ComboBox)
            End If

        End Sub

        Private Sub NombreOficina2ComboBox_KeyUp(ByVal sender As Object, ByVal e As KeyEventArgs) Handles NombreOficina2ComboBox.KeyUp
            If (e.KeyCode = Keys.Enter) Then
                CodigoContenedor1TextBox.Focus()
            End If
        End Sub

        Private Sub NombreOficina2ComboBox_Leave(ByVal sender As System.Object, ByVal e As EventArgs) Handles NombreOficina2ComboBox.Leave
            ValidarOficina(False)
        End Sub

        Private Sub NombreOficina2ComboBox_Enter(ByVal sender As System.Object, ByVal e As EventArgs) Handles NombreOficina2ComboBox.Enter
            lblCodigoOficina.Visible = True
            lblNombreOficina.Visible = True
        End Sub

        Private Sub CodigoContenedor1TextBox_KeyUp(ByVal sender As System.Object, ByVal e As KeyEventArgs) Handles CodigoContenedor1TextBox.KeyUp
            If (e.KeyCode = Keys.Enter And CodigoContenedor1TextBox.Text <> "" And CodigoContenedor1TextBox.Text <> "0") Then
                FechaMovimiento1MaskedTextBox.Focus()
            End If
        End Sub

        Private Sub CodigoContenedor2TextBox_KeyUp(ByVal sender As System.Object, ByVal e As KeyEventArgs) Handles CodigoContenedor2TextBox.KeyUp
            If (e.KeyCode = Keys.Enter) Then
                Anexo2ComboBox.Focus()
            End If
        End Sub

        Private Sub TipoMovimientoComboBox_KeyUp(ByVal sender As System.Object, ByVal e As KeyEventArgs) Handles TipoMovimientoComboBox.KeyUp
            If (e.KeyCode = Keys.Enter) Then
                Anexo1ComboBox.Focus()
            End If
        End Sub

        Private Sub FechaMovimiento1MaskedTextBox_KeyUp(ByVal sender As System.Object, ByVal e As KeyEventArgs) Handles FechaMovimiento1MaskedTextBox.KeyUp
            If (e.KeyCode = Keys.Enter) Then
                TipoMovimientoComboBox.Focus()
            End If
        End Sub

        Private Sub FechaMovimiento2MaskedTextBox_KeyUp(ByVal sender As System.Object, ByVal e As KeyEventArgs) Handles FechaMovimiento2MaskedTextBox.KeyUp
            If (e.KeyCode = Keys.Enter) Then
                CodigoContenedor2TextBox.Focus()
                ' Anexo2ComboBox.Focus()
            End If
        End Sub

        Private Sub Anexo1ComboBox_KeyUp(ByVal sender As System.Object, ByVal e As KeyEventArgs) Handles Anexo1ComboBox.KeyUp
            If (Anexo1ComboBox.SelectedIndex <> -1) Then
                anexocombo = Anexo1ComboBox.SelectedValue.ToString()
                If (e.KeyCode = Keys.Enter) Then
                    CantidadDocDiceContenerTextBox.Focus()
                End If
            End If
        End Sub

        Private Sub Anexo2ComboBox_KeyUp(ByVal sender As System.Object, ByVal e As KeyEventArgs) Handles Anexo2ComboBox.KeyUp
            If (e.KeyCode = Keys.Enter) Then
                If (Anexo2ComboBox.SelectedValue Is Nothing) Then
                    Return
                End If

                anexocombo = Anexo2ComboBox.SelectedValue.ToString()
                'Anexo2ComboBox.Focus()
                If OficinaConfirmarPanel.Enabled = False Then
                    InsertarContenedorButton.Focus()
                Else
                    CodOficina2ComboBox.Focus()
                End If

                Anexo2ComboBox.SelectedValue = anexocombo
                'InsertarContenedorButton.Focus()
            End If
        End Sub

        Private Sub CantidadDocDiceContenerTextBox_KeyUp(ByVal sender As System.Object, ByVal e As KeyEventArgs) Handles CantidadDocDiceContenerTextBox.KeyUp
            If (e.KeyCode = Keys.Enter) Then
                CantidadDocFisicosTextBox.Focus()
            End If
        End Sub

        Private Sub CantidadDocFisicosTextBox_KeyUp(ByVal sender As System.Object, ByVal e As KeyEventArgs) Handles CantidadDocFisicosTextBox.KeyUp
            If (e.KeyCode = Keys.Enter) Then
                TipoContenedorComboBox.Focus()
            End If
        End Sub

        Private Sub TipoContenedorComboBox_KeyUp(ByVal sender As Object, ByVal e As KeyEventArgs) Handles TipoContenedorComboBox.KeyUp
            If (e.KeyCode = Keys.Enter) Then
                DocumentosOrdenadosComboBox.Focus()
            End If
        End Sub

        Private Sub DocumentosOrdenadosComboBox_KeyUp(ByVal sender As System.Object, ByVal e As KeyEventArgs) Handles DocumentosOrdenadosComboBox.KeyUp
            If (e.KeyCode = Keys.Enter) Then
                If (DocumentosOrdenadosComboBox.Text.ToUpper() = "NO") Then
                    OrdenDocumentosCheckedListBox.Focus()
                    'End If

                    'If CodOficina2ComboBox.Enabled Then
                    '    CodOficina2ComboBox.Focus()
                Else
                    PresentaNovedadesComboBox.Focus()
                    'FechaMovimiento2MaskedTextBox.Focus()
                End If


            End If
        End Sub

        Private Sub OrdenDocumentosCheckedListBox_KeyUp(ByVal sender As System.Object, ByVal e As KeyEventArgs) Handles OrdenDocumentosCheckedListBox.KeyUp
            If (e.KeyCode = Keys.Enter) Then
                InsertarContenedorButton.Focus()
            End If
        End Sub

        'Private Sub DocumentosOrdenadosCheckBox_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs)
        '    OrdenDocumentosCheckedListBox.Visible = (DocumentosOrdenadosComboBox.SelectedIndex = 0)
        'End Sub

        Private Sub PresentaNovedadesComboBox_KeyUp(ByVal sender As System.Object, ByVal e As KeyEventArgs) Handles PresentaNovedadesComboBox.KeyUp
            If (e.KeyCode = Keys.Enter) Then
                If (PresentaNovedadesComboBox.Text.ToUpper() = "SI") Then
                    NovedadesCheckedListBox.Focus()
                Else
                    FechaCoincideAnexo23ComboBox.Focus()
                End If
            End If
        End Sub

        Private Sub NovedadesCheckedListBox_KeyUp(ByVal sender As Object, ByVal e As KeyEventArgs) Handles NovedadesCheckedListBox.KeyUp
            If (e.KeyCode = Keys.Enter) Then
                'InsertarContenedorButton.Focus()
                FechaCoincideAnexo23ComboBox.Focus()
            End If
        End Sub

        Private Sub FechaCoincideAnexo23ComboBox_KeyUp(ByVal sender As System.Object, ByVal e As KeyEventArgs) Handles FechaCoincideAnexo23ComboBox.KeyUp
            If (e.KeyCode = Keys.Enter) Then
                'If (FechaCoincideAnexo23ComboBox.Text.ToUpper() = "NO") Then
                '    TxFechaAnexo23Panel.Focus()
                'Else
                FechaMovimiento2MaskedTextBox.Focus()
                'End If
            End If
        End Sub

        Private Sub CantCoincidenDesktopTextBox_KeyUp(ByVal sender As System.Object, ByVal e As KeyEventArgs) Handles CantCoincidenDesktopTextBox.KeyUp
            If (e.KeyCode = Keys.Enter) Then
                CantNoCoincidenDesktopTextBox.Focus()
            End If
        End Sub

        Private Sub CantNoCoincidenDesktopTextBox_KeyUp(ByVal sender As System.Object, ByVal e As KeyEventArgs) Handles CantNoCoincidenDesktopTextBox.KeyUp
            If (e.KeyCode = Keys.Enter) Then
                FechaMovimiento2MaskedTextBox.Focus()
            End If
        End Sub

        Private Sub InsertarContenedorButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles InsertarContenedorButton.Click
            InsertarContenedor()
        End Sub

        Private Sub FinalizarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles FinalizarButton.Click
            FinalizarUCS()
        End Sub

        Private Sub ContenedoresDataGridView_CellDoubleClick(ByVal sender As System.Object, ByVal e As DataGridViewCellEventArgs) Handles ContenedoresDataGridView.CellDoubleClick
            IniciarEdicionContenedor(e.RowIndex)
        End Sub

        Private Sub GuardarContenedorButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles GuardarContenedorButton.Click
            ActualizarContenedor()
        End Sub

        Private Sub CancelarEdicionContenedorButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CancelarEdicionContenedorButton.Click
            CerrarEdicionContenedor()
        End Sub

        Private Sub EliminarContenedorButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles EliminarContenedorButton.Click
            EliminarContenedor()
        End Sub

        Private Sub DocumentosOrdenadosComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles DocumentosOrdenadosComboBox.SelectedIndexChanged
            OrdenDocumentosCheckedListBox.Visible = (DocumentosOrdenadosComboBox.SelectedIndex = 1)
        End Sub

        Private Sub PresentaNovedadesComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles PresentaNovedadesComboBox.SelectedIndexChanged
            NovedadesCheckedListBox.Visible = (PresentaNovedadesComboBox.SelectedIndex = 0)
        End Sub

        'Private Sub FechaCoincideAnexo23ComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles FechaCoincideAnexo23ComboBox.SelectedIndexChanged
        '    TxFechaAnexo23Panel.Visible = (FechaCoincideAnexo23ComboBox.SelectedIndex = 1)
        'End Sub

        Private Sub BuscarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BuscarButton.Click
            Dim dlg As New FormBuscarPrecinto(_Plugin, _OT_actual)
            If (dlg.ShowDialog() = DialogResult.OK) Then
                PrecintoTextBox.Text = dlg.PrecintoSeleccionado

                CambiarPrecintoSeleccionado()
            End If
        End Sub

        Private Sub Anexo2ComboBox_Leave(ByVal sender As System.Object, ByVal e As EventArgs) Handles Anexo2ComboBox.Leave
            ValidarAnexos(False)
        End Sub

        Private Sub CodigoContenedor2TextBox_Leave(ByVal sender As System.Object, ByVal e As EventArgs) Handles CodigoContenedor2TextBox.Leave
            If ValidarContenedor(False) Then
                ValidarStickerOficina()
            End If


        End Sub

        Private Function ValidarStickerOficina() As Boolean
            If Not CheckBoxSticker.Checked Then
                Dim Cod_Oficina_Contenedor As Integer = CInt(CodigoContenedor1TextBox.Text.Substring(2, 4))
                If EsCob = False Then
                    If Cod_Oficina_Contenedor <> CInt(CodOficina1ComboBox.SelectedValue) Then
                        ContenedorOkControl.OK = TriState.False
                        DesktopMessageBoxControl.DesktopMessageShow("La Oficina del Código de Barras, no corresponde a la seleccionada inicialmente", "Destape", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                        CodigoContenedor1TextBox.Focus()
                        Return False
                    End If
                    ContenedorOkControl.OK = TriState.True
                    Return True
                Else
                    If Cod_Oficina_Contenedor <> CInt(CodOficinaCobComboBox.SelectedValue) Then
                        ContenedorOkControl.OK = TriState.False
                        DesktopMessageBoxControl.DesktopMessageShow("La Oficina del Código de Barras, no corresponde a la seleccionada inicialmente", "Destape", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                        CodigoContenedor1TextBox.Focus()
                        Return False
                    End If
                    ContenedorOkControl.OK = TriState.True
                    Return True
                End If

            End If
            Return True
        End Function

        Private Sub TipoMovimientoComboBox_Leave(ByVal sender As System.Object, ByVal e As EventArgs) Handles TipoMovimientoComboBox.Leave
            ValidarTipoMovimiento(False)
        End Sub

        Private Sub FechaMovimiento2MaskedTextBox_Leave(ByVal sender As System.Object, ByVal e As EventArgs) Handles FechaMovimiento2MaskedTextBox.Leave
            If ValidarFechaMovimiento(False) Then
                CodigoContenedor2TextBox.Focus()

            End If
        End Sub

        Private Sub CantidadDocDiceContenerTextBox_Leave(ByVal sender As System.Object, ByVal e As EventArgs) Handles CantidadDocDiceContenerTextBox.Leave
            ValidarCantidadDocDiceContener(False)
        End Sub

        Private Sub CantidadDocFisicosTextBox_Leave(ByVal sender As System.Object, ByVal e As EventArgs) Handles CantidadDocFisicosTextBox.Leave
            ValidarCantidadDocFisicos(False)
        End Sub

        Private Sub TipoContenedorComboBox_Leave(ByVal sender As System.Object, ByVal e As EventArgs) Handles TipoContenedorComboBox.Leave
            ValidarTipoContenedor(False)
        End Sub

        Private Sub DocumentosOrdenadosComboBox_Leave(ByVal sender As System.Object, ByVal e As EventArgs) Handles DocumentosOrdenadosComboBox.Leave
            ValidarDocumentoOrdenados(False)
        End Sub

        Private Sub FechaCoincideAnexo23ComboBox_Leave(ByVal sender As System.Object, ByVal e As EventArgs) Handles FechaCoincideAnexo23ComboBox.Leave
            ValidarCoincideFechaAnexo23(False)
        End Sub

        Private Sub PresentaNovedadesComboBox_Leave(ByVal sender As System.Object, ByVal e As EventArgs) Handles PresentaNovedadesComboBox.Leave
            ValidarNovedades(False)
        End Sub

        Private Sub OrdenDocumentosCheckedListBox_Leave(ByVal sender As System.Object, ByVal e As EventArgs) Handles OrdenDocumentosCheckedListBox.Leave
            ValidarDocumentosSinOrden(False)
        End Sub

        Private Sub NovedadesCheckedListBox_Leave(ByVal sender As System.Object, ByVal e As EventArgs) Handles NovedadesCheckedListBox.Leave
            ValidarPresentaNovedades(False)
        End Sub

        'Private Sub CantCoincidenDesktopTextBox_Leave(ByVal sender As System.Object, ByVal e As EventArgs) Handles CantCoincidenDesktopTextBox.Leave
        '    ValidarCantidadCoincidenAnexo23(False)
        'End Sub

        'Private Sub CantNoCoincidenDesktopTextBox_Leave(ByVal sender As System.Object, ByVal e As EventArgs) Handles CantNoCoincidenDesktopTextBox.Leave
        '    ValidarCantidadNoCoincidenAnexo23(False)
        'End Sub

        Private Sub CodigoContenedor1TextBox_TextChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles CodigoContenedor1TextBox.TextChanged

            If (CodigoContenedor1TextBox.TextLength = 15) Then
                DesktopMessageBoxControl.DesktopMessageShow("El contenedor no puede tener más de 15 caracteres", "Destape", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
            End If
        End Sub

        Private Sub CodigoContenedor2TextBox_TextChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles CodigoContenedor2TextBox.TextChanged
            If (CodigoContenedor2TextBox.TextLength = 15) Then
                DesktopMessageBoxControl.DesktopMessageShow("El contenedor no puede tener más de 15 caracteres", "Destape", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                CodigoContenedor2TextBox.Focus()
            End If
        End Sub


        ''Eventos Click 



        ''Eventos Enter


        'Private Sub FechaMovimiento2MaskedTextBox_Enter(ByVal sender As System.Object, ByVal e As EventArgs) Handles FechaMovimiento2MaskedTextBox.Enter
        '    lblFechaMovimiento.Visible = True
        'End Sub

        Private Sub lblCodigoOficina_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles lblCodigoOficina.Click
            lblCodigoOficina.Visible = False
            CodOficina1ComboBox.Focus()
        End Sub

        Private Sub lblCodigoOficinaCob_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles lblCodigoOficinaCob.Click
            lblCodigoOficinaCob.Visible = False
            CodOficinaCobComboBox.Focus()
        End Sub

        Private Sub CodOficina1ComboBox_Enter(ByVal sender As System.Object, ByVal e As EventArgs) Handles CodOficina1ComboBox.Enter
            lblCodigoOficina.Visible = False
            lblNombreOficina.Visible = False
        End Sub

        Private Sub CodOficinaCobComboBox_Enter(ByVal sender As System.Object, ByVal e As EventArgs) Handles CodOficinaCobComboBox.Enter
            lblCodigoOficinaCob.Visible = False
            lblNombreOficinaCob.Visible = False
        End Sub

        Private Sub NombreOficina1ComboBox_Enter(ByVal sender As System.Object, ByVal e As EventArgs) Handles NombreOficina1ComboBox.Enter
            lblNombreOficina.Visible = False
            If EsCob = False Then
                CodigoContenedor1TextBox.Focus()
            Else
                CodOficinaCobComboBox.Focus()
            End If

        End Sub

        Private Sub NombreOficinaCobComboBox_Enter(ByVal sender As System.Object, ByVal e As EventArgs) Handles NombreOficinaCobComboBox.Enter
            lblNombreOficinaCob.Visible = False
            CodigoContenedor1TextBox.Focus()
        End Sub

        Private Sub TipoMovimientoComboBox_Enter(ByVal sender As System.Object, ByVal e As EventArgs) Handles TipoMovimientoComboBox.Enter
            lblFechaMovimiento.Visible = True
        End Sub

        Private Sub CantidadDocDiceContenerTextBox_Enter(ByVal sender As System.Object, ByVal e As EventArgs) Handles CantidadDocDiceContenerTextBox.Enter
            If (Not anexocombo Is Nothing) Then
                Anexo1ComboBox.SelectedValue = anexocombo
            End If

            lblAnexos.Visible = True
        End Sub

        Private Sub CodigoContenedor1TextBox_Enter(ByVal sender As System.Object, ByVal e As EventArgs) Handles CodigoContenedor1TextBox.Enter
            If (Not oficina Is Nothing) Then
                If EsCob = False Then
                    CodOficina1ComboBox.SelectedValue = oficina
                Else
                    CodOficinaCobComboBox.SelectedValue = oficina
                End If


            End If

            lblCodigoOficina.Visible = True
            lblNombreOficina.Visible = True
            lblCodigoOficinaCob.Visible = True
            lblNombreOficinaCob.Visible = True
        End Sub

        Private Sub lblFechaMovimiento_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles lblFechaMovimiento.Click
            FechaMovimiento1MaskedTextBox.Focus()
            lblFechaMovimiento.Visible = False
        End Sub

        Private Sub lblAnexos_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles lblAnexos.Click
            Anexo1ComboBox.SelectedValue = anexocombo
            lblAnexos.Visible = False
            Anexo1ComboBox.Focus()
        End Sub

#End Region

#Region " Metodos "

        Private Sub LlenarCombos()
            Dim dbmCore As DBCoreDataBaseManager = Nothing
            Dim dbmAgrario As DBAgrarioDataBaseManager = Nothing

            Try
                dbmCore = New DBCoreDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Core)
                ''dbmCore.DataBase.Identifier_Date_Format = Configuration.ConfigurationManager.AppSettings("IdentifierDateFormat")
                dbmCore.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                dbmAgrario = New DBAgrarioDataBaseManager(Me._Plugin.BancoAgrarioConnectionString)
                ''dbmAgrario.DataBase.Identifier_Date_Format = _Plugin.Manager.DesktopGlobal.IdentifierDateFormat
                dbmAgrario.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                Dim EntidadId As Short = _Plugin.Manager.RiskGlobal.Entidad
                Dim EntidadProcesamiento As Short = _Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad
                Dim SedeProcesamiento = _Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Sede

                If EsCob = False Then

                    Dim oficinaData = dbmAgrario.SchemaProcess.CTA_Oficina.DBFindByfk_Entidadfk_Sede(EntidadProcesamiento, SedeProcesamiento)
                    Dim ofiView = oficinaData.DefaultView
                    'ofiView.Sort = TBL_Campo_Lista_ItemEnum.Etiqueta_Campo_Lista_Item.ColumnName

                    _PermiteIgualarValor = False
                    Utilities.LlenarCombo(CodOficina1ComboBox, ofiView.ToTable(), CTA_OficinaEnum.id_Oficina.ColumnName, CTA_OficinaEnum.id_Oficina.ColumnName)
                    Utilities.LlenarCombo(CodOficina2ComboBox, ofiView.ToTable(), CTA_OficinaEnum.id_Oficina.ColumnName, CTA_OficinaEnum.id_Oficina.ColumnName)

                    'For Each ofi In oficinaData
                    '    Dim posGuion = ofi.Nombre_Oficina.IndexOf("-"c)
                    '    ofi.Nombre_Oficina = ofi.Nombre_Oficina.Substring(posGuion + 1, ofi.Nombre_Oficina.Length - posGuion - 1).Trim()
                    'Next

                    ofiView = oficinaData.DefaultView
                    ofiView.Sort = CTA_OficinaEnum.Nombre_Oficina.ColumnName

                    Utilities.LlenarCombo(NombreOficina1ComboBox, ofiView.ToTable(), CTA_OficinaEnum.id_Oficina.ColumnName, CTA_OficinaEnum.Nombre_Oficina.ColumnName)
                    Utilities.LlenarCombo(NombreOficina2ComboBox, ofiView.ToTable(), CTA_OficinaEnum.id_Oficina.ColumnName, CTA_OficinaEnum.Nombre_Oficina.ColumnName)

                Else


                End If



                Dim tipoMovimientoData = dbmCore.SchemaConfig.TBL_Campo_Lista_Item.DBFindByfk_Entidadfk_Campo_Lista(EntidadId, Program.Banagrario_ListaTipoMovimientoId, 0, New TBL_Campo_Lista_ItemEnumList(TBL_Campo_Lista_ItemEnum.Etiqueta_Campo_Lista_Item, True))
                Utilities.LlenarCombo(TipoMovimientoComboBox, tipoMovimientoData, TBL_Campo_Lista_ItemEnum.Valor_Campo_Lista_Item.ColumnName, TBL_Campo_Lista_ItemEnum.Etiqueta_Campo_Lista_Item.ColumnName)

                Dim tipoContenedorData = dbmCore.SchemaConfig.TBL_Campo_Lista_Item.DBFindByfk_Entidadfk_Campo_Lista(EntidadId, Program.Banagrario_ListaTipoContenedorId, 0, New TBL_Campo_Lista_ItemEnumList(TBL_Campo_Lista_ItemEnum.Etiqueta_Campo_Lista_Item, True))
                Utilities.LlenarCombo(TipoContenedorComboBox, tipoContenedorData, TBL_Campo_Lista_ItemEnum.Valor_Campo_Lista_Item.ColumnName, TBL_Campo_Lista_ItemEnum.Etiqueta_Campo_Lista_Item.ColumnName)

                Dim AnexoData = dbmCore.SchemaConfig.TBL_Anexos.DBFindByfk_Documento(Program.BanagrarioDocumentoTapaId)
                Utilities.LlenarCombo(Anexo1ComboBox, AnexoData, TBL_AnexosEnum.id_Anexo.ColumnName, TBL_AnexosEnum.Nombre_Anexo.ColumnName)
                Utilities.LlenarCombo(Anexo2ComboBox, AnexoData, TBL_AnexosEnum.id_Anexo.ColumnName, TBL_AnexosEnum.Nombre_Anexo.ColumnName)

                CodOficina1ComboBox.SelectedIndex = -1
                'CodOficina2ComboBox.SelectedIndex = -1
                NombreOficina1ComboBox.SelectedIndex = -1
                'NombreOficina2ComboBox.SelectedIndex = -1
                TipoMovimientoComboBox.SelectedIndex = -1
                TipoContenedorComboBox.SelectedIndex = -1
                Anexo1ComboBox.SelectedIndex = -1
                Anexo2ComboBox.SelectedIndex = -1

                _PermiteIgualarValor = True

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Destape", ex)
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                If (dbmAgrario IsNot Nothing) Then dbmAgrario.Connection_Close()
            End Try
        End Sub

        Private Sub CambiarPrecintoSeleccionado()
            If (ValidarPrecinto()) Then
                HabilitarDestapePrecinto(True)
                LimpiarControlesContenedor(True, False, False)
                CargarFoldersPrecinto()
            End If
        End Sub

        Private Function ValidarPrecinto() As Boolean
            Dim dbmArchiving As DBArchivingDataBaseManager = Nothing
            Dim dbmImaging As DBImagingDataBaseManager = Nothing
            Dim dbmCore As DBCoreDataBaseManager = Nothing

            Try
                If (PrecintoTextBox.Text.Trim() = "") Then
                    Return False
                End If

                If (PrecintoTextBox.Text.Length < Program.PrecintoMinCaracteres) Then
                    Throw New Exception("El numero de precinto no puede ser menor a " + Program.PrecintoMinCaracteres.ToString() + " digitos")
                End If

                If (PrecintoTextBox.Text.Length > Program.PrecintoMaxCaracteres) Then
                    Throw New Exception("El numero de precinto no puede ser mayor a " + Program.PrecintoMaxCaracteres.ToString() + " digitos")
                End If

                dbmArchiving = New DBArchivingDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Archiving)
                dbmImaging = New DBImagingDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)
                dbmCore = New DBCoreDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Core)

                ''dbmArchiving.DataBase.Identifier_Date_Format = _Plugin.Manager.DesktopGlobal.IdentifierDateFormat

                dbmArchiving.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
                dbmImaging.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
                dbmCore.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                Dim precintosExistentes = dbmArchiving.SchemaRisk.TBL_Precinto.DBGet(_Plugin.Manager.RiskGlobal.Entidad, _Plugin.Manager.RiskGlobal.Proyecto, PrecintoTextBox.Text)

                If (precintosExistentes.Count = 0) Then
                    Throw New Exception("El precinto digitado no ha sido destapado " + PrecintoTextBox.Text)
                End If

                If (precintosExistentes(0).fk_OT <> _OT_actual) Then
                    Throw New Exception("El precinto digitado no pertenece a la OT en Proceso de destape " + PrecintoTextBox.Text)
                End If

            Catch ex As Exception
                PrecintoOkControl.OK = TriState.False
                HabilitarDestapePrecinto(False)
                DesktopMessageBoxControl.DesktopMessageShow(ex.Message, "Destape", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                Return False
            Finally
                If (dbmArchiving IsNot Nothing) Then dbmArchiving.Connection_Close()
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            End Try

            PrecintoOkControl.OK = TriState.True
            Return True
        End Function

        Private Sub CargarFoldersPrecinto()
            Dim dbmAgrario As DBAgrarioDataBaseManager = Nothing
            Try
                dbmAgrario = New DBAgrarioDataBaseManager(Me._Plugin.BancoAgrarioConnectionString)
                ''dbmAgrario.DataBase.Identifier_Date_Format = _Plugin.Manager.DesktopGlobal.IdentifierDateFormat
                dbmAgrario.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                Dim foldersData = dbmAgrario.SchemaProcess.CTA_Destape_Detalle.DBFindByfk_OTfk_Precinto(_OT_actual, PrecintoTextBox.Text)

                ContenedoresDataGridView.DataSource = foldersData


                OficinaPanel.Enabled = True
                OficinaCobPanel.Enabled = True
                OficinaConfirmarPanel.Enabled = True


                If (foldersData.Count > 0) Then
                    If foldersData(0).OficinaCob = "" Then
                        For i = 0 To CodOficina1ComboBox.Items.Count - 1
                            If (CStr(DirectCast(CodOficina1ComboBox.Items(i), DataRowView).Row(CTA_OficinaEnum.id_Oficina.ColumnName)) = foldersData(0).Oficina) Then

                                CodOficina1ComboBox.SelectedIndex = i
                                CodOficina2ComboBox.SelectedIndex = i

                                OficinaPanel.Enabled = False
                                OficinaConfirmarPanel.Enabled = False
                                Exit For
                            End If
                        Next
                    Else
                        For i = 0 To CodOficina1ComboBox.Items.Count - 1
                            If (CStr(DirectCast(CodOficina1ComboBox.Items(i), DataRowView).Row(CTA_OficinaEnum.id_Oficina.ColumnName)) = foldersData(0).OficinaCob) Then

                                CodOficina1ComboBox.SelectedIndex = i

                                Exit For
                            End If
                        Next
                        'For i = 0 To CodOficinaCobComboBox.Items.Count - 1
                        '    If (CStr(DirectCast(CodOficinaCobComboBox.Items(i), DataRowView).Row(CTA_OficinaEnum.id_Oficina.ColumnName)) = foldersData(0).Oficina) Then

                        '        CodOficinaCobComboBox.SelectedIndex = i
                        '        CodOficina2ComboBox.SelectedIndex = i
                        '        Exit For
                        '    End If
                        'Next
                        CodOficinaCobComboBox.SelectedIndex = -1
                        NombreOficinaCobComboBox.SelectedIndex = -1
                        CodOficina2ComboBox.SelectedIndex = -1
                        NombreOficina2ComboBox.SelectedIndex = -1
                        OficinaPanel.Enabled = False
                        OficinaCobPanel.Enabled = True
                        OficinaConfirmarPanel.Enabled = True
                    End If


                End If

                If (OficinaPanel.Enabled) Then
                    CodOficina1ComboBox.Focus()
                ElseIf OficinaCobPanel.Visible = True Then '(OficinaCobPanel.Enabled) Or
                    CodOficinaCobComboBox.Focus()
                Else
                    CodigoContenedor1TextBox.Focus()
                End If

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Destape", ex)
            Finally
                If (dbmAgrario IsNot Nothing) Then dbmAgrario.Connection_Close()
            End Try
        End Sub

        Private Sub InsertarContenedor()
            Dim contenedorGuardado As Boolean = False
            BotonesGroupBox.Enabled = False


            If (ValidarCamposContenedor()) Then
                Dim objFolderCore As DesktopConfig.FolderCORE

                Dim dbmCore As DBCoreDataBaseManager = Nothing
                Dim dmArchiving As DBArchivingDataBaseManager = Nothing
                Dim dmAgrario As DBAgrarioDataBaseManager = Nothing

                Try
                    dbmCore = New DBCoreDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Core)
                    DBCoreDataBaseManager.IdentifierDateFormat = _Plugin.Manager.DesktopGlobal.IdentifierDateFormat
                    dbmCore.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                    dmArchiving = New DBArchivingDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Archiving)
                    DBArchivingDataBaseManager.IdentifierDateFormat = _Plugin.Manager.DesktopGlobal.IdentifierDateFormat
                    dmArchiving.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                    dmAgrario = New DBAgrarioDataBaseManager(_Plugin.BancoAgrarioConnectionString)
                    DBAgrarioDataBaseManager.IdentifierDateFormat = _Plugin.Manager.DesktopGlobal.IdentifierDateFormat
                    dmAgrario.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                    Dim entidadId As Short = _Plugin.Manager.RiskGlobal.Entidad
                    Dim proyectoId As Short = _Plugin.Manager.RiskGlobal.Proyecto
                    Dim precinto As String = PrecintoTextBox.Text

                    Dim Key1_Oficina As String
                    Dim OficinaCob As String
                    If EsCob = False Then
                        OficinaCob = Nothing
                        Key1_Oficina = CStr(CodOficina1ComboBox.SelectedValue)
                    Else
                        OficinaCob = CStr(CodOficina1ComboBox.SelectedValue)
                        Key1_Oficina = CStr(CodOficinaCobComboBox.SelectedValue)
                    End If

                    Dim Key2_FechaString = TextoAFecha(FechaMovimiento1MaskedTextBox.Text).ToString("yyyy/MM/dd")
                    Dim Key3_Contenedor = CStr(CodigoContenedor1TextBox.Text)

                    ''Validar Oficina Correcto 
                    If EsCob = False Then
                        If IgualarCodOficinayNomOficina(CodOficina1ComboBox, NombreOficina1ComboBox) Then
                            If IgualarCodOficinayNomOficina(CodOficina2ComboBox, NombreOficina2ComboBox) Then
                            Else
                                Throw New Exception("Los codigos de las oficinas no corresponden con el nombre")
                            End If
                        Else
                            Throw New Exception("Los codigos de las oficinas no corresponden con el nombre")
                        End If

                    Else
                        If IgualarCodOficinayNomOficina(CodOficinaCobComboBox, NombreOficinaCobComboBox) Then
                            If IgualarCodOficinayNomOficina(CodOficina2ComboBox, NombreOficina2ComboBox) Then
                            Else
                                Throw New Exception("Los codigos de las oficinas no corresponden con el nombre")
                            End If
                        Else
                            Throw New Exception("Los codigos de las oficinas no corresponden con el nombre")
                        End If

                    End If

                    Dim expExistenteData = dbmCore.SchemaProcess.PA_Expediente_getByKeysEsquema.DBExecute(entidadId, proyectoId, Program.BanagrarioEsquemaRiskId, Key1_Oficina, Key2_FechaString, Key3_Contenedor, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                    If (expExistenteData.Count > 0) Then
                        Throw New Exception("El contenedor ya se encuentra destapado")
                    End If

                    Dim Key2_FechaDateTime = TextoAFecha(FechaMovimiento1MaskedTextBox.Text).ToString("yyyy/MM/dd")

                    Dim esquemaData = dmArchiving.SchemaConfig.TBL_Esquema.DBGet(entidadId, proyectoId, Program.BanagrarioEsquemaRiskId)
                    If (esquemaData.Count = 0) Then
                        Throw New Exception("Esquema no se encuentra")
                    End If

                    Dim precintoData = dmArchiving.SchemaRisk.TBL_Precinto.DBGet(_Plugin.Manager.RiskGlobal.Entidad, _Plugin.Manager.RiskGlobal.Proyecto, precinto)
                    If (precintoData.Count = 0) Then Throw New Exception("Precinto no encontrado " & precinto)
                    If (precintoData(0).Destapado) Then
                        If (Not Utilities.ValidarPermiso(Permisos.Imaging.Proceso.Captura.Reprocesos, Program.AccesoDesktopAssembly, "Autorizar el destape del contenedor " & Key3_Contenedor & " para el precinto " & precinto & " el cual se ya encuentra finalizado", _Plugin.Manager.Sesion.Usuario, _Plugin.Manager.DesktopGlobal.SecurityServiceURL, _Plugin.Manager.DesktopGlobal.ClientIPAddress)) Then
                            Throw New Exception("El destape precinto " & precinto & " ya se encuentra finalizado ")
                        End If
                    End If

                    Dim OTRow = dmArchiving.SchemaRisk.TBL_OT.DBGet(precintoData(0).fk_OT)(0)

                    dbmCore.Transaction_Begin()
                    dmArchiving.Transaction_Begin()
                    dmAgrario.Transaction_Begin()

                    'Lista de llaves Expediente
                    Dim listLlavesExpediente As New List(Of DesktopConfig.StrLlaves)
                    listLlavesExpediente.Add(New DesktopConfig.StrLlaves() With {.id_Llave = Program.Tapa_CampoId_Oficina, .Tipo = DesktopConfig.CampoTipo.Texto, .Valor_Llave = Key1_Oficina, .Nombre_Llave = "Oficina"})
                    listLlavesExpediente.Add(New DesktopConfig.StrLlaves() With {.id_Llave = Program.Tapa_CampoId_Fecha, .Tipo = DesktopConfig.CampoTipo.Fecha, .Valor_Llave = Key2_FechaDateTime, .Nombre_Llave = "Fecha"})
                    listLlavesExpediente.Add(New DesktopConfig.StrLlaves() With {.id_Llave = Program.Tapa_CampoId_Contenedor, .Tipo = DesktopConfig.CampoTipo.Texto, .Valor_Llave = Key3_Contenedor, .Nombre_Llave = "Codigo Contenedor"})

                    objFolderCore = Utilities.InsertaFolderCore(dbmCore, dmArchiving, _Plugin.Manager.RiskGlobal, Program.BanagrarioEsquemaRiskId, listLlavesExpediente, _Plugin.Manager.DesktopGlobal.ConnectionStrings, _Plugin.Manager.Sesion.Usuario.id, EstadoEnum.Destapado)
                    Utilities.InsertaFolderArchiving(dmArchiving, dbmCore, objFolderCore.Expediente, objFolderCore.Folder, _OT_actual, precinto, EstadoEnum.Destapado)

                    Dim idFile = Utilities.InsertaFileCore(dbmCore, dmArchiving, objFolderCore.Expediente, objFolderCore.Folder, Program.BanagrarioDocumentoTapaId, _Plugin.Manager.RiskGlobal)
                    Utilities.InsertaFileArchiving(dmArchiving, dbmCore, idFile, objFolderCore.Folder, objFolderCore.Expediente, Nothing, False, DesktopConfig.RegistroTipo.Nuevo, _OT_actual, Nothing, EstadoEnum.Destapado, True, _Plugin.Manager.Sesion.Usuario.id)

                    Dim objFileData As New TBL_File_DataType() With {.fk_Expediente = objFolderCore.Expediente, .fk_Folder = objFolderCore.Folder, .fk_File = idFile, .fk_Documento = Program.BanagrarioDocumentoTapaId}

                    'Oficina
                    objFileData.fk_Campo = Program.Tapa_CampoId_Oficina
                    objFileData.Valor_File_Data = Key1_Oficina
                    objFileData.Conteo_File_Data = objFileData.Valor_File_Data.ToString().Length
                    dbmCore.SchemaProcess.TBL_File_Data.DBInsert(objFileData)

                    'Fecha
                    objFileData.fk_Campo = Program.Tapa_CampoId_Fecha
                    objFileData.Valor_File_Data = Key2_FechaDateTime
                    objFileData.Conteo_File_Data = objFileData.Valor_File_Data.ToString().Length
                    dbmCore.SchemaProcess.TBL_File_Data.DBInsert(objFileData)

                    'Código de Barras Contenedor
                    objFileData.fk_Campo = Program.Tapa_CampoId_Contenedor
                    objFileData.Valor_File_Data = Key3_Contenedor
                    objFileData.Conteo_File_Data = objFileData.Valor_File_Data.ToString().Length
                    dbmCore.SchemaProcess.TBL_File_Data.DBInsert(objFileData)

                    'Cantidad Según Contenedor
                    objFileData.fk_Campo = Program.Tapa_CampoId_CantidadSegunContenedor
                    objFileData.Valor_File_Data = CantidadDocDiceContenerTextBox.Text
                    objFileData.Conteo_File_Data = objFileData.Valor_File_Data.ToString().Length
                    dbmCore.SchemaProcess.TBL_File_Data.DBInsert(objFileData)

                    'Cantidad Real
                    objFileData.fk_Campo = Program.Tapa_CampoId_CantidadFisicos
                    objFileData.Valor_File_Data = CantidadDocFisicosTextBox.Text
                    objFileData.Conteo_File_Data = objFileData.Valor_File_Data.ToString().Length
                    dbmCore.SchemaProcess.TBL_File_Data.DBInsert(objFileData)

                    'Documentos Ordenados
                    objFileData.fk_Campo = Program.Tapa_CampoId_DocumentosOrdenados
                    objFileData.Valor_File_Data = (DocumentosOrdenadosComboBox.SelectedIndex = 0)
                    objFileData.Conteo_File_Data = objFileData.Valor_File_Data.ToString().Length
                    dbmCore.SchemaProcess.TBL_File_Data.DBInsert(objFileData)

                    'Presenta Novedades
                    objFileData.fk_Campo = Program.Tapa_CampoId_PresentaNovedades
                    objFileData.Valor_File_Data = (PresentaNovedadesComboBox.SelectedIndex = 0)
                    objFileData.Conteo_File_Data = objFileData.Valor_File_Data.ToString().Length
                    dbmCore.SchemaProcess.TBL_File_Data.DBInsert(objFileData)

                    'Conicide Fecha Anexo23
                    objFileData.fk_Campo = Program.Tapa_CampoId_CoincideFechaAnexo23
                    objFileData.Valor_File_Data = (FechaCoincideAnexo23ComboBox.SelectedIndex = 0)
                    objFileData.Conteo_File_Data = objFileData.Valor_File_Data.ToString().Length
                    dbmCore.SchemaProcess.TBL_File_Data.DBInsert(objFileData)

                    'Tipo Contenedor
                    objFileData.fk_Campo = Program.Tapa_CampoId_TipoContenedor
                    objFileData.Valor_File_Data = TipoContenedorComboBox.SelectedValue
                    objFileData.Conteo_File_Data = objFileData.Valor_File_Data.ToString().Length
                    dbmCore.SchemaProcess.TBL_File_Data.DBInsert(objFileData)

                    'Tipo Movimiento
                    objFileData.fk_Campo = Program.Tapa_CampoId_TipoMovimiento
                    objFileData.Valor_File_Data = TipoMovimientoComboBox.SelectedValue
                    objFileData.Conteo_File_Data = objFileData.Valor_File_Data.ToString().Length
                    dbmCore.SchemaProcess.TBL_File_Data.DBInsert(objFileData)

                    'Anexos
                    objFileData.fk_Campo = Program.Tapa_CampoId_Anexo
                    objFileData.Valor_File_Data = Anexo1ComboBox.SelectedValue
                    objFileData.Conteo_File_Data = objFileData.Valor_File_Data.ToString().Length
                    dbmCore.SchemaProcess.TBL_File_Data.DBInsert(objFileData)


                    ' Validaciones
                    Dim objValidacion As New TBL_File_ValidacionType() With {.fk_Expediente = objFolderCore.Expediente, .fk_Folder = objFolderCore.Folder, .fk_File = idFile}

                    For Index = 0 To OrdenDocumentosCheckedListBox.Items.Count - 1
                        objValidacion.fk_Validacion = CShort(Index + 1)
                        objValidacion.Respuesta = (OrdenDocumentosCheckedListBox.GetItemChecked(Index) And DocumentosOrdenadosComboBox.Text.ToUpper() = "NO")
                        objValidacion.Motivo = Nothing
                        dbmCore.SchemaProcess.TBL_File_Validacion.DBInsert(objValidacion)
                    Next

                    ' Novedades
                    Dim objNovedad As New TBL_NovedadesType() With {.fk_Expediente = objFolderCore.Expediente, .fk_Folder = objFolderCore.Folder, .fk_File = idFile, .fk_Documento = Program.BanagrarioDocumentoTapaId}

                    For Index = 0 To NovedadesCheckedListBox.Items.Count - 1
                        objNovedad.fk_Novedad = CShort(Index + 1)
                        objNovedad.Respuesta = (NovedadesCheckedListBox.GetItemChecked(Index) And PresentaNovedadesComboBox.Text.ToUpper() = "SI")
                        dmAgrario.SchemaProcess.TBL_Novedades.DBInsert(objNovedad)
                    Next

                    Dim MesaDestapeDataTable = dmAgrario.SchemaConfig.TBL_Mesa_Destape.DBFindByPC_Namefk_Entidadfk_Sedefk_Centro_ProcesamientoActiva(Environment.MachineName, _Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, _Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Sede, _Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento, True)
                    If MesaDestapeDataTable.Count > 0 Then
                        Id_MesaDestape = MesaDestapeDataTable(0).id_Mesa_Destape
                    End If

                    Dim DestapeType = New DBAgrario.SchemaProcess.TBL_DestapeType()

                    Dim oficinaRow = dmAgrario.SchemaConfig.CTA_Oficina_Cob_Regional.DBFindByid_Oficina(CInt(Key1_Oficina))(0)

                    With DestapeType
                        '.id_Destape = dmAgrario.SchemaProcess.TBL_Destape.DBNex    tId()
                        .codigo_Contenedor = Key3_Contenedor
                        .fecha_Movimiento = Key2_FechaDateTime
                        .fecha_Proceso = OTRow.Fecha_Proceso.ToString("yyyy/MM/dd")
                        .fk_Entidad = entidadId
                        .fk_Sede = _Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Sede
                        .fk_Centro_Procesamiento = _Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento
                        .fk_Expediente = objFileData.fk_Expediente
                        .fk_Folder = CShort(objFileData.fk_Folder)
                        .fk_file = CShort(objFileData.fk_File)
                        .fk_Regional = oficinaRow.id_Regional
                        .fk_Cob = oficinaRow.id_Cob
                        .fk_Oficina = oficinaRow.id_Oficina
                        .fk_Usuario_Log = _Plugin.Manager.Sesion.Usuario.id
                        .fecha_Log = SlygNullable.SysDate
                        .fk_Oficina_Destape = CInt(Key1_Oficina)
                        .fk_Usuario_Destape = _Plugin.Manager.Sesion.Usuario.id
                        .fecha_Destape_Log = SlygNullable.SysDate
                        .Cantidad_Real = CInt(CantidadDocFisicosTextBox.Text)
                        .Cantidad_Reportada = CInt(CantidadDocDiceContenerTextBox.Text)
                        .fk_Tipo_Movimiento = CInt(TipoMovimientoComboBox.SelectedValue)
                        .fk_Anexo = CInt(Anexo1ComboBox.SelectedValue)
                        .Digitalizado = False
                        .fecha_Empaque = SlygNullable.SysDate
                        .Tipo_Contenedor = CShort(TipoContenedorComboBox.SelectedValue)
                        .File_Unique_Identifier = Nothing
                        .Nombre_Regional = oficinaRow.Nombre_Regional
                        .Nombre_Cob = oficinaRow.Nombre_COB
                        .Nombre_Oficina = oficinaRow.nombre_oficina
                        .Nombre_Oficina_Tipo = oficinaRow.Nombre_Oficina_Tipo
                        .fk_Oficina_Tipo = oficinaRow.id_Oficina_Tipo
                        .fk_Mesa_Destape = Id_MesaDestape
                        .Coincide_Fecha_Anexo23 = CBool(FechaCoincideAnexo23ComboBox.SelectedValue)
                        If CantCoincidenDesktopTextBox.Visible = True Then
                            .Cantidad_Coincide_Fecha_Anexo23 = CInt(CantCoincidenDesktopTextBox.Text)
                        Else
                            .Cantidad_Coincide_Fecha_Anexo23 = Nothing
                        End If
                        If CantNoCoincidenDesktopTextBox.Visible = True Then
                            .Cantidad_No_Coincide_Fecha_Anexo23 = CInt(CantNoCoincidenDesktopTextBox.Text)
                        Else
                            .Cantidad_No_Coincide_Fecha_Anexo23 = Nothing
                        End If
                        If (EsCob) Then
                            .Oficina_Cob = CInt(OficinaCob)
                        Else
                            .Oficina_Cob = Nothing
                        End If
                    End With

                    dmAgrario.SchemaProcess.TBL_Destape.DBInsert(DestapeType)

                    dmAgrario.Transaction_Commit()
                    dbmCore.Transaction_Commit()
                    dmArchiving.Transaction_Commit()


                    contenedorGuardado = True

                Catch ex As Exception
                    DesktopMessageBoxControl.DesktopMessageShow("Destape", ex)
                    dmAgrario.Transaction_Rollback()
                    dbmCore.Transaction_Rollback()
                    dmArchiving.Transaction_Rollback()

                Finally
                    If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                    If (dmArchiving IsNot Nothing) Then dmArchiving.Connection_Close()
                    If (dmAgrario IsNot Nothing) Then dmAgrario.Connection_Close()
                End Try

                CargarFoldersPrecinto()
                If (contenedorGuardado) Then
                    LimpiarControlesContenedor(False, False, False)
                End If
            End If

            BotonesGroupBox.Enabled = True
            If (contenedorGuardado) Then
                If (EsCob) Then
                    CodOficinaCobComboBox.Focus()
                Else
                    CodigoContenedor1TextBox.Text = ""
                    CodigoContenedor1TextBox.Focus()
                End If

            End If

        End Sub

        Private Sub IniciarEdicionContenedor(ByVal nIndex As Integer)
            ' Deshabilitar los campos si no han sido cargados
            'CodigoContenedor1TextBox.Enabled = nEnabled And Not cargado
            'CodigoContenedor2TextBox.Enabled = nEnabled And Not cargado
            'FechaMovimiento1MaskedTextBox.Enabled = nEnabled And Not cargado
            'FechaMovimiento2MaskedTextBox.Enabled = nEnabled And Not cargado

            If (nIndex > -1 And ContenedoresDataGridView.SelectedCells.Count > 0) Then
                _ContenedorEdicion = DirectCast(DirectCast(DirectCast(ContenedoresDataGridView.SelectedCells(0), DataGridViewTextBoxCell).OwningRow.DataBoundItem, DataRowView).Row, DBAgrario.SchemaProcess.CTA_Destape_DetalleRow).ToCTA_Destape_DetalleType

                If (_ContenedorEdicion.fk_Cargue IsNot Nothing) Then
                    MessageBox.Show("El contenedor ya fue cargado y no se permite su modificación", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    _ContenedorEdicion = Nothing
                    Return
                End If

                If _ContenedorEdicion.OficinaCob <> "" Then
                    CodOficinaCobComboBox.SelectedValue = _ContenedorEdicion.Oficina
                    CodOficina2ComboBox.SelectedValue = _ContenedorEdicion.Oficina
                    OficinaCobPanel.Enabled = False
                End If
                OficinaConfirmarPanel.Enabled = False

                CodigoContenedor1TextBox.Text = _ContenedorEdicion.Codigo_Contenedor
                CodigoContenedor2TextBox.Text = _ContenedorEdicion.Codigo_Contenedor

                For i As Integer = 0 To TipoMovimientoComboBox.Items.Count - 1
                    Dim row = DirectCast(TipoMovimientoComboBox.Items(i), DataRowView).Row
                    If (row(TBL_Campo_Lista_ItemEnum.Etiqueta_Campo_Lista_Item.ColumnName).ToString().ToUpper() = _ContenedorEdicion.Tipo_Movimiento.ToString().ToUpper()) Then
                        TipoMovimientoComboBox.SelectedIndex = i
                        Exit For
                    End If
                Next

                FechaMovimiento1MaskedTextBox.Text = DateTime.Parse(_ContenedorEdicion.Fecha).ToString("dd/MM/yyyy")
                FechaMovimiento2MaskedTextBox.Text = DateTime.Parse(_ContenedorEdicion.Fecha).ToString("dd/MM/yyyy")

                CantidadDocDiceContenerTextBox.Text = _ContenedorEdicion.Cantidad_Segun_Contenedor
                CantidadDocFisicosTextBox.Text = _ContenedorEdicion.Cantidad_Real

                For i As Integer = 0 To TipoContenedorComboBox.Items.Count - 1
                    Dim row = DirectCast(TipoContenedorComboBox.Items(i), DataRowView).Row
                    If (row(TBL_Campo_Lista_ItemEnum.Etiqueta_Campo_Lista_Item.ColumnName).ToString().ToUpper() = _ContenedorEdicion.Tipo_Contenedor.ToString().ToUpper()) Then
                        TipoContenedorComboBox.SelectedIndex = i
                        Exit For
                    End If
                Next

                For i As Integer = 0 To DocumentosOrdenadosComboBox.Items.Count - 1
                    If (DocumentosOrdenadosComboBox.Items(i).ToString().ToUpper() = _ContenedorEdicion.Documentos_Ordenados.ToString().ToUpper()) Then
                        DocumentosOrdenadosComboBox.SelectedIndex = i
                        Exit For
                    End If
                Next

                For i As Integer = 0 To PresentaNovedadesComboBox.Items.Count - 1
                    If (PresentaNovedadesComboBox.Items(i).ToString().ToUpper() = _ContenedorEdicion.Presenta_Novedades.ToString().ToUpper()) Then
                        PresentaNovedadesComboBox.SelectedIndex = i
                        Exit For
                    End If
                Next

                For i As Integer = 0 To FechaCoincideAnexo23ComboBox.Items.Count - 1
                    If (FechaCoincideAnexo23ComboBox.Items(i).ToString().ToUpper() = _ContenedorEdicion.Coincide_FechaAnexo23.ToString().ToUpper()) Then
                        FechaCoincideAnexo23ComboBox.SelectedIndex = i
                        If FechaCoincideAnexo23ComboBox.SelectedItem Is "NO" Then
                            CantCoincidenDesktopTextBox.Text = _ContenedorEdicion.Cant_Coincide_Anexo23
                            CantNoCoincidenDesktopTextBox.Text = _ContenedorEdicion.Cant_No_Coincide_Anexo23
                        End If

                    End If
                Next


                Dim dbmCore As DBCoreDataBaseManager = Nothing
                Dim dmAgrario As DBAgrarioDataBaseManager = Nothing

                Try
                    dbmCore = New DBCoreDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Core)
                    ''dbmCore.DataBase.Identifier_Date_Format = _Plugin.Manager.DesktopGlobal.IdentifierDateFormat
                    dbmCore.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                    dmAgrario = New DBAgrarioDataBaseManager(_Plugin.BancoAgrarioConnectionString)
                    ''dmAgrario.DataBase.Identifier_Date_Format = _Plugin.Manager.DesktopGlobal.IdentifierDateFormat
                    dmAgrario.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                    Dim ValidacionesDataTable = dbmCore.SchemaProcess.TBL_File_Validacion.DBGet(_ContenedorEdicion.fk_Expediente, _ContenedorEdicion.fk_Folder, _ContenedorEdicion.id_File, Nothing, _ContenedorEdicion.fk_Documento)

                    'OrdenDocumentosCheckedListBox.ClearSelected()
                    For i As Integer = 0 To OrdenDocumentosCheckedListBox.Items.Count - 1
                        Dim validacion = DirectCast(ValidacionesDataTable.Select(TBL_File_ValidacionEnum.fk_Validacion.ColumnName & "=" & (i + 1).ToString()), TBL_File_ValidacionRow())
                        If (validacion.Length = 0 OrElse validacion(0).Respuesta = False) Then
                            OrdenDocumentosCheckedListBox.SetItemChecked(i, False)
                        Else
                            OrdenDocumentosCheckedListBox.SetItemChecked(i, True)
                        End If
                    Next

                    Dim NovedadesDataTable = dmAgrario.SchemaProcess.TBL_Novedades.DBGet(_ContenedorEdicion.fk_Documento, _ContenedorEdicion.fk_Expediente, _ContenedorEdicion.fk_Folder, _ContenedorEdicion.id_File, Nothing)
                    'NovedadesCheckedListBox.ClearSelected()
                    For i As Integer = 0 To NovedadesCheckedListBox.Items.Count - 1
                        Dim Novedad = DirectCast(NovedadesDataTable.Select(TBL_NovedadesEnum.fk_Novedad.ColumnName & "=" & (i + 1).ToString()), TBL_NovedadesRow())
                        If (Novedad.Length = 0 OrElse Novedad(0).Respuesta = False) Then
                            NovedadesCheckedListBox.SetItemChecked(i, False)
                        Else
                            NovedadesCheckedListBox.SetItemChecked(i, True)
                        End If
                    Next

                Catch ex As Exception
                    DesktopMessageBoxControl.DesktopMessageShow("Destape", ex)
                Finally
                    If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                    If (dmAgrario IsNot Nothing) Then dmAgrario.Connection_Close()
                End Try

                InsertarContenedorButton.Visible = False
                GuardarContenedorButton.Visible = True
                CancelarEdicionContenedorButton.Visible = True
                EliminarContenedorButton.Visible = True
                GuardarContenedorButton.Left = 6
            End If
        End Sub

        Private Sub CerrarEdicionContenedor()
            _ContenedorEdicion = Nothing
            InsertarContenedorButton.Visible = True
            GuardarContenedorButton.Visible = False
            CancelarEdicionContenedorButton.Visible = False
            EliminarContenedorButton.Visible = False
            LimpiarControlesContenedor(False, False, False)
        End Sub

        Private Sub ActualizarContenedor()
            Dim contenedorGuardado As Boolean = False
            Me.Enabled = False

            If (ValidarCamposContenedor()) Then
                Dim dbmCore As DBCoreDataBaseManager = Nothing
                Dim dmAgrario As DBAgrarioDataBaseManager = Nothing

                Try
                    Dim EntidadId As Integer = _Plugin.Manager.RiskGlobal.Entidad
                    Dim Key1_Oficina As String
                    Dim OficinaCob As String
                    If EsCob = False Then
                        OficinaCob = Nothing
                        Key1_Oficina = CStr(CodOficina1ComboBox.SelectedValue)
                    Else
                        OficinaCob = CStr(CodOficina1ComboBox.SelectedValue)
                        Key1_Oficina = CStr(CodOficinaCobComboBox.SelectedValue)
                    End If
                    Dim Key2_FechaString = TextoAFecha(FechaMovimiento1MaskedTextBox.Text).ToString("yyyy/MM/dd")
                    Dim Key2_FechaDateTime = TextoAFecha(FechaMovimiento1MaskedTextBox.Text).ToString("yyyy/MM/dd")
                    Dim Key3_Contenedor = CStr(CodigoContenedor1TextBox.Text)

                    If (Not Utilities.ValidarPermiso(Permisos.Imaging.Proceso.Control.Autorizaciones, "Plugin", "Autorizar la modificación del contenedor " & Key3_Contenedor & " , oficina " & Key1_Oficina & " , fecha " & Key2_FechaString, _Plugin.Manager.Sesion.Usuario, _Plugin.Manager.DesktopGlobal.SecurityServiceURL, _Plugin.Manager.DesktopGlobal.ClientIPAddress)) Then
                        Throw New Exception("No se permitio la modificación del contenedor")
                    End If

                    dmAgrario = New DBAgrarioDataBaseManager(_Plugin.BancoAgrarioConnectionString)
                    ''dmAgrario.DataBase.Identifier_Date_Format = _Plugin.Manager.DesktopGlobal.IdentifierDateFormat
                    dmAgrario.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                    dbmCore = New DBCoreDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Core)
                    ''dbmCore.DataBase.Identifier_Date_Format = _Plugin.Manager.DesktopGlobal.IdentifierDateFormat
                    dbmCore.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
                    dbmCore.Transaction_Begin()

                    'Llaves Expediente
                    dbmCore.SchemaProcess.TBL_Expediente_Llave.DBUpdate(_ContenedorEdicion.fk_Expediente, Program.Proyecto_LlaveId_Oficina, CByte(1), Key1_Oficina, _ContenedorEdicion.fk_Expediente, Program.Proyecto_LlaveId_Oficina)
                    dbmCore.SchemaProcess.TBL_Expediente_Llave.DBUpdate(_ContenedorEdicion.fk_Expediente, Program.Proyecto_LlaveId_Fecha, CByte(3), Key2_FechaDateTime, _ContenedorEdicion.fk_Expediente, Program.Proyecto_LlaveId_Fecha)
                    dbmCore.SchemaProcess.TBL_Expediente_Llave.DBUpdate(_ContenedorEdicion.fk_Expediente, Program.Proyecto_LlaveId_Contenedor, CByte(1), Key3_Contenedor, _ContenedorEdicion.fk_Expediente, Program.Proyecto_LlaveId_Contenedor)

                    Dim objFileData As New TBL_File_DataType() With {.fk_Expediente = _ContenedorEdicion.fk_Expediente, .fk_Folder = _ContenedorEdicion.fk_Folder, .fk_File = _ContenedorEdicion.id_File, .fk_Documento = _ContenedorEdicion.fk_Documento}

                    'Oficina
                    objFileData.fk_Campo = Program.Tapa_CampoId_Oficina
                    objFileData.Valor_File_Data = Key1_Oficina
                    objFileData.Conteo_File_Data = objFileData.Valor_File_Data.ToString().Length
                    dbmCore.SchemaProcess.TBL_File_Data.DBUpdate(objFileData, objFileData.fk_Expediente, objFileData.fk_Folder, objFileData.fk_File, objFileData.fk_Documento, objFileData.fk_Campo)

                    'Fecha
                    objFileData.fk_Campo = Program.Tapa_CampoId_Fecha
                    objFileData.Valor_File_Data = Key2_FechaDateTime
                    objFileData.Conteo_File_Data = objFileData.Valor_File_Data.ToString().Length
                    dbmCore.SchemaProcess.TBL_File_Data.DBUpdate(objFileData, objFileData.fk_Expediente, objFileData.fk_Folder, objFileData.fk_File, objFileData.fk_Documento, objFileData.fk_Campo)

                    'Código de Barras Contenedor
                    objFileData.fk_Campo = Program.Tapa_CampoId_Contenedor
                    objFileData.Valor_File_Data = Key3_Contenedor
                    objFileData.Conteo_File_Data = objFileData.Valor_File_Data.ToString().Length
                    dbmCore.SchemaProcess.TBL_File_Data.DBUpdate(objFileData, objFileData.fk_Expediente, objFileData.fk_Folder, objFileData.fk_File, objFileData.fk_Documento, objFileData.fk_Campo)

                    'Destape
                    'dmAgrario.SchemaProcess.PA_Inserccion_Destape.DBExecute(Key3_Contenedor, Key2_FechaDateTime.ToString("yyyy/MM/dd"), CType(Key1_Oficina, Global.Slyg.Tools.SlygNullable(Of Integer)), _Plugin.Manager.Sesion.Usuario.id)

                    'Cantidad Según Contenedor
                    objFileData.fk_Campo = Program.Tapa_CampoId_CantidadSegunContenedor
                    objFileData.Valor_File_Data = CantidadDocDiceContenerTextBox.Text
                    objFileData.Conteo_File_Data = objFileData.Valor_File_Data.ToString().Length
                    dbmCore.SchemaProcess.TBL_File_Data.DBUpdate(objFileData, objFileData.fk_Expediente, objFileData.fk_Folder, objFileData.fk_File, objFileData.fk_Documento, objFileData.fk_Campo)

                    'Cantidad Real
                    objFileData.fk_Campo = Program.Tapa_CampoId_CantidadFisicos
                    objFileData.Valor_File_Data = CantidadDocFisicosTextBox.Text
                    objFileData.Conteo_File_Data = objFileData.Valor_File_Data.ToString().Length
                    dbmCore.SchemaProcess.TBL_File_Data.DBUpdate(objFileData, objFileData.fk_Expediente, objFileData.fk_Folder, objFileData.fk_File, objFileData.fk_Documento, objFileData.fk_Campo)

                    'Documentos Ordenados
                    objFileData.fk_Campo = Program.Tapa_CampoId_DocumentosOrdenados
                    objFileData.Valor_File_Data = (DocumentosOrdenadosComboBox.SelectedIndex = 0)
                    objFileData.Conteo_File_Data = objFileData.Valor_File_Data.ToString().Length
                    dbmCore.SchemaProcess.TBL_File_Data.DBUpdate(objFileData, objFileData.fk_Expediente, objFileData.fk_Folder, objFileData.fk_File, objFileData.fk_Documento, objFileData.fk_Campo)

                    'Presenta Novedad
                    objFileData.fk_Campo = Program.Tapa_CampoId_PresentaNovedades
                    objFileData.Valor_File_Data = (PresentaNovedadesComboBox.SelectedIndex = 0)
                    objFileData.Conteo_File_Data = objFileData.Valor_File_Data.ToString().Length
                    dbmCore.SchemaProcess.TBL_File_Data.DBUpdate(objFileData, objFileData.fk_Expediente, objFileData.fk_Folder, objFileData.fk_File, objFileData.fk_Documento, objFileData.fk_Campo)

                    'Tipo Contenedor
                    objFileData.fk_Campo = Program.Tapa_CampoId_TipoContenedor
                    objFileData.Valor_File_Data = TipoContenedorComboBox.SelectedValue
                    objFileData.Conteo_File_Data = objFileData.Valor_File_Data.ToString().Length
                    dbmCore.SchemaProcess.TBL_File_Data.DBUpdate(objFileData, objFileData.fk_Expediente, objFileData.fk_Folder, objFileData.fk_File, objFileData.fk_Documento, objFileData.fk_Campo)

                    'Tipo Movimiento
                    objFileData.fk_Campo = Program.Tapa_CampoId_TipoMovimiento
                    objFileData.Valor_File_Data = TipoMovimientoComboBox.SelectedValue
                    objFileData.Conteo_File_Data = objFileData.Valor_File_Data.ToString().Length
                    dbmCore.SchemaProcess.TBL_File_Data.DBUpdate(objFileData, objFileData.fk_Expediente, objFileData.fk_Folder, objFileData.fk_File, objFileData.fk_Documento, objFileData.fk_Campo)

                    'Anexos
                    objFileData.fk_Campo = Program.Tapa_CampoId_Anexo
                    objFileData.Valor_File_Data = Anexo1ComboBox.SelectedValue
                    objFileData.Conteo_File_Data = objFileData.Valor_File_Data.ToString().Length
                    dbmCore.SchemaProcess.TBL_File_Data.DBUpdate(objFileData, objFileData.fk_Expediente, objFileData.fk_Folder, objFileData.fk_File, objFileData.fk_Documento, objFileData.fk_Campo)

                    ' Validaciones
                    Dim objValidacion As New TBL_File_ValidacionType() With {.fk_Expediente = _ContenedorEdicion.fk_Expediente, .fk_Folder = _ContenedorEdicion.fk_Folder, .fk_File = _ContenedorEdicion.id_File}

                    For Index = 0 To OrdenDocumentosCheckedListBox.Items.Count - 1
                        objValidacion.fk_Validacion = CShort(Index + 1)

                        objValidacion.Respuesta = (OrdenDocumentosCheckedListBox.GetItemChecked(Index) And DocumentosOrdenadosComboBox.Text.ToUpper() = "NO")
                        objValidacion.Motivo = Nothing
                        dbmCore.SchemaProcess.TBL_File_Validacion.DBUpdate(objValidacion, objFileData.fk_Expediente, objFileData.fk_Folder, objFileData.fk_File, objValidacion.fk_Validacion, objValidacion.fk_Documento)
                    Next


                    ' Novedades
                    Dim objNovedad As New TBL_NovedadesType() With {.fk_Expediente = _ContenedorEdicion.fk_Expediente, .fk_Folder = _ContenedorEdicion.fk_Folder, .fk_File = _ContenedorEdicion.id_File, .fk_Documento = _ContenedorEdicion.fk_Documento}

                    For Index = 0 To NovedadesCheckedListBox.Items.Count - 1
                        objNovedad.fk_Novedad = CShort(Index + 1)
                        objNovedad.Respuesta = (NovedadesCheckedListBox.GetItemChecked(Index) And PresentaNovedadesComboBox.Text.ToUpper() = "SI")
                        dmAgrario.SchemaProcess.TBL_Novedades.DBUpdate(objNovedad, objNovedad.fk_Documento, objFileData.fk_Expediente, objFileData.fk_Folder, objFileData.fk_File, objNovedad.fk_Novedad)
                    Next


                    Dim MesaDestapeDataTable = dmAgrario.SchemaConfig.TBL_Mesa_Destape.DBFindByPC_Namefk_Entidadfk_Sedefk_Centro_ProcesamientoActiva(Environment.MachineName, _Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, _Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Sede, _Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento, True)
                    If MesaDestapeDataTable.Count > 0 Then
                        Id_MesaDestape = MesaDestapeDataTable(0).id_Mesa_Destape
                    End If

                    Dim DestapeRow = dmAgrario.SchemaProcess.TBL_Destape.DBFindByfk_Expedientefk_Folderfk_file(objFileData.fk_Expediente, CShort(objFileData.fk_Folder), CShort(objFileData.fk_File))(0)

                    Dim DestapeType = New DBAgrario.SchemaProcess.TBL_DestapeType()

                    Dim oficinaRow = dmAgrario.SchemaConfig.CTA_Oficina_Cob_Regional.DBFindByid_Oficina(CInt(Key1_Oficina))(0)

                    With DestapeType
                        .codigo_Contenedor = Key3_Contenedor
                        .fecha_Movimiento = Key2_FechaDateTime
                        .fk_Entidad = CShort(EntidadId)
                        .fk_Sede = _Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Sede
                        .fk_Centro_Procesamiento = _Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento
                        .fk_Regional = oficinaRow.id_Regional
                        .fk_Cob = oficinaRow.id_Cob
                        .fk_Oficina = oficinaRow.id_Oficina
                        .fk_Usuario_Log = _Plugin.Manager.Sesion.Usuario.id
                        .fecha_Log = SlygNullable.SysDate
                        .fk_Oficina_Destape = CInt(Key1_Oficina)
                        .Cantidad_Real = CInt(CantidadDocFisicosTextBox.Text)
                        .Cantidad_Reportada = CInt(CantidadDocDiceContenerTextBox.Text)
                        .fk_Tipo_Movimiento = CInt(TipoMovimientoComboBox.SelectedValue)
                        .fk_Anexo = CInt(Anexo1ComboBox.SelectedValue)
                        .Tipo_Contenedor = CShort(TipoContenedorComboBox.SelectedValue)
                        .Nombre_Regional = oficinaRow.Nombre_Regional
                        .Nombre_Cob = oficinaRow.Nombre_COB
                        .Nombre_Oficina = oficinaRow.nombre_oficina
                        .Nombre_Oficina_Tipo = oficinaRow.Nombre_Oficina_Tipo
                        .fk_Oficina_Tipo = oficinaRow.id_Oficina_Tipo
                        .fk_Mesa_Destape_Mod = Id_MesaDestape
                        .Coincide_Fecha_Anexo23 = CBool(FechaCoincideAnexo23ComboBox.SelectedValue)
                        If CantCoincidenDesktopTextBox.Visible = True Then
                            .Cantidad_Coincide_Fecha_Anexo23 = CInt(CantCoincidenDesktopTextBox.Text)
                        Else
                            .Cantidad_Coincide_Fecha_Anexo23 = Nothing
                        End If
                        If CantNoCoincidenDesktopTextBox.Visible = True Then
                            .Cantidad_No_Coincide_Fecha_Anexo23 = CInt(CantNoCoincidenDesktopTextBox.Text)
                        Else
                            .Cantidad_No_Coincide_Fecha_Anexo23 = Nothing
                        End If
                        If (EsCob) Then
                            .Oficina_Cob = CInt(OficinaCob)
                        Else
                            .Oficina_Cob = Nothing
                        End If
                    End With

                    dmAgrario.SchemaProcess.TBL_Destape.DBUpdate(DestapeType, DestapeRow.id_Destape)

                    dbmCore.Transaction_Commit()

                    contenedorGuardado = True

                Catch ex As Exception
                    DesktopMessageBoxControl.DesktopMessageShow("Destape", ex)
                Finally
                    If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                    If (dmAgrario IsNot Nothing) Then dmAgrario.Connection_Close()
                End Try

                CargarFoldersPrecinto()
                If (contenedorGuardado) Then
                    CerrarEdicionContenedor()
                End If
            End If

            Me.Enabled = True
            If (contenedorGuardado) Then
                CodigoContenedor1TextBox.Focus()
            End If
        End Sub

        Private Sub EliminarContenedor()
            Dim contenedorEliminado As Boolean = False
            Me.Enabled = False

            Dim dbmCore As DBCoreDataBaseManager = Nothing
            Dim dmArchiving As DBArchivingDataBaseManager = Nothing
            Dim dmAgrario As DBAgrarioDataBaseManager = Nothing

            Try
                Dim Key1_Oficina = ""
                Dim Key2_FechaString = ""
                Dim Key3_Contenedor = ""
                Try
                    Key1_Oficina = CodOficina1ComboBox.SelectedValue.ToString()
                    Key2_FechaString = TextoAFecha(FechaMovimiento1MaskedTextBox.Text).ToString("yyyy/MM/dd")
                    Key3_Contenedor = CodigoContenedor1TextBox.Text
                Catch : End Try

                If (Not Utilities.ValidarPermiso(Permisos.Imaging.Proceso.Control.Autorizaciones, "Plugin", "Autorizar la eliminación del contenedor " & Key3_Contenedor & " , oficina " & Key1_Oficina & " , fecha " & Key2_FechaString, _Plugin.Manager.Sesion.Usuario, _Plugin.Manager.DesktopGlobal.SecurityServiceURL, _Plugin.Manager.DesktopGlobal.ClientIPAddress)) Then
                    Throw New Exception("No se permitio la eliminación del contenedor")
                End If

                dbmCore = New DBCoreDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Core)
                'dbmCore.DataBase.Identifier_Date_Format = _Plugin.Manager.DesktopGlobal.IdentifierDateFormat
                dbmCore.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
                dbmCore.Transaction_Begin()

                dmArchiving = New DBArchivingDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Archiving)
                'dmArchiving.DataBase.Identifier_Date_Format = _Plugin.Manager.DesktopGlobal.IdentifierDateFormat
                dmArchiving.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
                dmArchiving.Transaction_Begin()

                dmAgrario = New DBAgrarioDataBaseManager(_Plugin.BancoAgrarioConnectionString)
                'dmAgrario.DataBase.Identifier_Date_Format = _Plugin.Manager.DesktopGlobal.IdentifierDateFormat
                dmAgrario.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
                dmAgrario.Transaction_Begin()

                Dim DestapeDataTable = dmAgrario.SchemaProcess.TBL_Destape.DBFindByfk_Expedientefk_Folderfk_file(_ContenedorEdicion.fk_Expediente, Nothing, Nothing)

                For Each DestapeRow In DestapeDataTable
                    dmAgrario.SchemaProcess.TBL_Destape.DBDelete(DestapeRow.id_Destape)
                Next

                dbmCore.SchemaProcess.TBL_Expediente.DBDelete(_ContenedorEdicion.fk_Expediente)
                dmArchiving.SchemaRisk.TBL_Folder.DBDelete(_ContenedorEdicion.fk_Expediente, Nothing, Nothing)

                dbmCore.Transaction_Commit()
                dmArchiving.Transaction_Commit()
                dmAgrario.Transaction_Commit()

                contenedorEliminado = True

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Destape", ex)
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                If (dmArchiving IsNot Nothing) Then dmArchiving.Connection_Close()
                If (dmAgrario IsNot Nothing) Then dmAgrario.Connection_Close()
            End Try

            Me.Enabled = True

            CargarFoldersPrecinto()
            If (contenedorEliminado) Then
                CerrarEdicionContenedor()
                CodigoContenedor1TextBox.Focus()
            End If
        End Sub

        Private Sub FinalizarUCS()
            Me.Enabled = False
            Dim UCSFinalizada As Boolean = False

            Dim dmArchiving As DBArchivingDataBaseManager = Nothing
            Dim dbmCore As DBCoreDataBaseManager = Nothing
            Try
                dmArchiving = New DBArchivingDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Archiving)
                dbmCore = New DBCoreDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Core)

                'Cierra el precinto
                Dim EntidadId As Short = _Plugin.Manager.RiskGlobal.Entidad
                Dim ProyectoId As Short = _Plugin.Manager.RiskGlobal.Proyecto
                Const Modulo As DesktopConfig.Modulo = DesktopConfig.Modulo.Archiving
                Dim precinto As String = PrecintoTextBox.Text
                Const Servicio_Facturacion As Short = 1
                Dim SedeProcesamiento As Short = _Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Sede
                Dim CentroProcesamiento As Short = _Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento

                dmArchiving.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
                dbmCore.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                Dim foldersData = dmArchiving.SchemaRisk.TBL_Folder.DBFindByfk_Precinto(precinto)
                If (foldersData.Count = 0) Then
                    DesktopMessageBoxControl.DesktopMessageShow("No se permite finalizar la recepción, debido a que no se ha ingresado ningún contenedor", "Destape", DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
                    Return
                End If

                dmArchiving.Transaction_Begin()
                Dim Table = dmArchiving.Schemadbo.PA_Cerrar_Precinto.DBExecute(precinto, EntidadId, ProyectoId, CByte(Modulo), _OT_actual, Servicio_Facturacion, SedeProcesamiento, CentroProcesamiento, EstadoEnum.Empaque)
                dmArchiving.Transaction_Commit()

                If Table.Rows(0)(0).ToString = "OK" Then
                    DesktopMessageBoxControl.DesktopMessageShow("El cierre del precinto ha sucedido con éxito", "Cierre Precinto", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                    UCSFinalizada = True
                Else
                    DesktopMessageBoxControl.DesktopMessageShow("Se generaron problemas al cerrar al precinto, por favor comuniquese con el administrador.", "Cierre Precinto", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                End If

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CerrarPrecinto", ex)
            Finally
                If (dmArchiving IsNot Nothing) Then dmArchiving.Connection_Close()
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()

                Me.Enabled = True

                If (UCSFinalizada) Then
                    LimpiarControlesContenedor(True, True, True)
                    PrecintoTextBox.Focus()
                End If
            End Try
        End Sub

        Private Sub IgualarOficinaValor(ByVal nComboBoxOrigen As ComboBox, ByVal nComboBoxDestino As ComboBox)
            If (_PermiteIgualarValor) Then
                _PermiteIgualarValor = False

                If (Not nComboBoxOrigen.SelectedValue Is Nothing) Then
                    Dim valor = nComboBoxOrigen.SelectedValue.ToString()
                    Dim destinoSelectedIndex = -1
                    For i As Integer = 0 To nComboBoxDestino.Items.Count - 1
                        Dim itemRow = DirectCast(nComboBoxDestino.Items(i), DataRowView).Row
                        Dim valorItem = CStr(itemRow(CTA_OficinaEnum.id_Oficina.ColumnName))
                        If (valor = valorItem) Then
                            destinoSelectedIndex = i
                            Exit For
                        End If
                    Next

                    If (destinoSelectedIndex = -1) Then
                        DesktopMessageBoxControl.DesktopMessageShow("La lista de destino no contiene el elemento seleccionado " + valor, "Destape", DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
                    End If

                    nComboBoxDestino.SelectedIndex = destinoSelectedIndex
                End If

                _PermiteIgualarValor = True
            End If
        End Sub

        Private Sub IgualarOficinaCobValor(ByVal nComboBoxOrigen As ComboBox, ByVal nComboBoxDestino As ComboBox)
            If (_PermiteIgualarValor AndAlso nComboBoxOrigen.SelectedIndex >= 0) Then
                _PermiteIgualarValor = False

                If (Not nComboBoxOrigen.SelectedValue Is Nothing) Then
                    Dim valor = nComboBoxOrigen.SelectedValue.ToString()
                    Dim destinoSelectedIndex = -1
                    For i As Integer = 0 To nComboBoxDestino.Items.Count - 1
                        Dim itemRow = DirectCast(nComboBoxDestino.Items(i), DataRowView).Row
                        Dim valorItem = CStr(itemRow(CTA_Cob_OficinaEnum.id_Oficina.ColumnName))
                        If (valor = valorItem) Then
                            destinoSelectedIndex = i
                            Exit For
                        End If
                    Next

                    If (destinoSelectedIndex = -1) Then
                        DesktopMessageBoxControl.DesktopMessageShow("La lista de destino no contiene el elemento seleccionado " + valor, "Destape", DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
                    End If

                    nComboBoxDestino.SelectedIndex = destinoSelectedIndex
                End If

                _PermiteIgualarValor = True
            End If
        End Sub

        Private Sub HabilitarDestapePrecinto(ByVal nEnabled As Boolean)
            'OficinaPanel.Enabled = nEnabled
            ContenedorGroupBox.Enabled = nEnabled
            BotonesPanel.Enabled = nEnabled
            ContenedoresGroupBox.Enabled = nEnabled
        End Sub

        Private Sub LimpiarControlesContenedor(ByVal nLimpiarOficina As Boolean, ByVal nLimpiarPrecinto As Boolean, ByVal nLimpiarGrillaContenedores As Boolean)
            If (nLimpiarPrecinto) Then
                PrecintoTextBox.Text = ""
                PrecintoOkControl.OK = TriState.UseDefault
            End If
            If (nLimpiarOficina) Then
                CodOficina1ComboBox.SelectedIndex = -1
                CodOficinaCobComboBox.SelectedIndex = -1
                CodOficina2ComboBox.SelectedIndex = -1
                NombreOficina1ComboBox.SelectedIndex = -1
                NombreOficinaCobComboBox.SelectedIndex = -1
                NombreOficina2ComboBox.SelectedIndex = -1
                OficinaOkControl.OK = TriState.UseDefault
                OficinaCobOkControl.OK = TriState.UseDefault
            End If

            CodigoContenedor1TextBox.Text = ""
            CodigoContenedor2TextBox.Text = ""
            ContenedorOkControl.OK = TriState.UseDefault

            TipoMovimientoComboBox.SelectedIndex = -1
            TipoMovimientoOkControl.OK = TriState.UseDefault

            FechaMovimiento1MaskedTextBox.Text = ""
            FechaMovimiento2MaskedTextBox.Text = ""
            FechaMovimientoOkControl.OK = TriState.UseDefault

            CantidadDocDiceContenerTextBox.Text = ""
            CantidadDocDiceContenerOkControl.OK = TriState.UseDefault

            CantidadDocFisicosTextBox.Text = ""
            CantidadDocFisicosOkControl.OK = TriState.UseDefault

            TipoContenedorComboBox.SelectedIndex = -1
            TipoContenedorOkControl.OK = TriState.UseDefault

            DocumentosOrdenadosComboBox.SelectedIndex = -1
            DocumentosOrdenadosOkControl.OK = TriState.UseDefault

            PresentaNovedadesComboBox.SelectedIndex = -1
            NovedadesOkControl.OK = TriState.UseDefault

            FechaCoincideAnexo23ComboBox.SelectedIndex = -1
            CoincideFechaAnexo23OkControl.OK = TriState.UseDefault

            Anexo1ComboBox.SelectedIndex = -1
            Anexo2ComboBox.SelectedIndex = -1
            AnexoOkControl.OK = TriState.UseDefault

            lblAnexos.Visible = False
            lblCodigoOficina.Visible = False
            lblNombreOficina.Visible = False
            lblCodigoOficinaCob.Visible = False
            lblNombreOficinaCob.Visible = False
            lblFechaMovimiento.Visible = False

            'OrdenDocumentosCheckedListBox.SelectedItems.Clear()
            For i As Integer = 0 To OrdenDocumentosCheckedListBox.Items.Count - 1
                OrdenDocumentosCheckedListBox.SetItemChecked(i, False)
            Next
            DocumentosSinOrdenOkControl.OK = TriState.UseDefault

            'NovedadesCheckedListBox.SelectedItems.Clear()
            For i As Integer = 0 To NovedadesCheckedListBox.Items.Count - 1
                NovedadesCheckedListBox.SetItemChecked(i, False)
            Next
            PresentaNovedadesOkControl.OK = TriState.UseDefault

            If (nLimpiarGrillaContenedores) Then
                ContenedoresDataGridView.DataSource = Nothing
            End If
        End Sub

        Private Function ValidarOficina(ByVal nMostrarMensaje As Boolean) As Boolean
            'If oficina <> Nothing Then
            '    CodOficina2ComboBox.SelectedValue = oficina
            '    CodOficina2ComboBox.Refresh()
            'End If
            If EsCob = False Then
                If (CodOficina1ComboBox.SelectedIndex < 0) Then
                    Return MarcarValidacion("La oficina es requerida", CodOficina1ComboBox, nMostrarMensaje, OficinaOkControl)
                End If
                If IgualarCodOficinayNomOficina(CodOficina1ComboBox, NombreOficina1ComboBox) Then
                    If IgualarCodOficinayNomOficina(CodOficina2ComboBox, NombreOficina2ComboBox) Then

                        If (CStr(CodOficina1ComboBox.SelectedValue) <> CStr(CodOficina2ComboBox.SelectedValue)) Then
                            CodOficina1ComboBox.Focus()
                            Return MarcarValidacion("La oficina seleccionada no corresponde con la confirmación", CodOficina1ComboBox, nMostrarMensaje, OficinaOkControl)
                        Else
                            If oficina <> Nothing Then
                                CodOficina2ComboBox.SelectedValue = oficina
                                CodOficina2ComboBox.Refresh()
                            End If
                        End If
                    Else
                        Return MarcarValidacion("La oficina seleccionada no corresponde con la confirmación", CodOficina1ComboBox, nMostrarMensaje, OficinaOkControl)
                    End If
                Else
                    Return MarcarValidacion("La oficina seleccionada no corresponde con la confirmación", CodOficina1ComboBox, nMostrarMensaje, OficinaOkControl)
                End If

                If (Not Slyg.Tools.DataConvert.IsNumeric(CodOficina1ComboBox.SelectedValue.ToString())) Then
                    Return MarcarValidacion("La oficina seleccionada no tiene un codigo válido", CodOficina1ComboBox, nMostrarMensaje, OficinaOkControl)
                End If
            Else
                If (CodOficinaCobComboBox.SelectedIndex < 0) Then
                    Return MarcarValidacion("La oficina es requerida", CodOficinaCobComboBox, nMostrarMensaje, OficinaOkControl)
                End If
                If IgualarCodOficinayNomOficina(CodOficinaCobComboBox, NombreOficinaCobComboBox) Then
                    If IgualarCodOficinayNomOficina(CodOficina2ComboBox, NombreOficina2ComboBox) Then

                        If (CStr(CodOficinaCobComboBox.SelectedValue) <> CStr(CodOficina2ComboBox.SelectedValue)) Then
                            Return MarcarValidacion("La oficina seleccionada no corresponde con la confirmación", CodOficinaCobComboBox, nMostrarMensaje, OficinaCobOkControl)
                        Else
                            If oficina <> Nothing Then
                                CodOficina2ComboBox.SelectedValue = oficina
                                CodOficina2ComboBox.Refresh()
                            End If
                        End If
                    Else
                        Return MarcarValidacion("La oficina seleccionada no corresponde con la confirmación", CodOficinaCobComboBox, nMostrarMensaje, OficinaCobOkControl)
                    End If
                Else
                    Return MarcarValidacion("La oficina seleccionada no corresponde con la confirmación", CodOficinaCobComboBox, nMostrarMensaje, OficinaCobOkControl)
                End If

                If (Not Slyg.Tools.DataConvert.IsNumeric(CodOficinaCobComboBox.SelectedValue.ToString())) Then
                    Return MarcarValidacion("La oficina selecciona no tiene un codigo válido", CodOficinaCobComboBox, nMostrarMensaje, OficinaCobOkControl)
                End If
            End If

            OficinaOkControl.OK = TriState.True
            OficinaCobOkControl.OK = TriState.True
            Return True
        End Function

        Private Function ValidarAnexos(ByVal nMostrarMensaje As Boolean) As Boolean

            If (Anexo1ComboBox.SelectedIndex < 0) Then
                Return MarcarValidacion("El anexo  es requerido", Anexo1ComboBox, nMostrarMensaje, AnexoOkControl)
            End If

            If (CStr(Anexo1ComboBox.SelectedValue) <> CStr(Anexo2ComboBox.SelectedValue)) Then
                Return MarcarValidacion("El Anexo seleccionado no corresponde con la confirmación", Anexo1ComboBox, nMostrarMensaje, AnexoOkControl)
            Else
                If anexocombo <> Nothing Then
                    Anexo2ComboBox.SelectedValue = anexocombo
                    Anexo2ComboBox.Refresh()
                End If
            End If

            If (Not Slyg.Tools.DataConvert.IsNumeric(Anexo1ComboBox.SelectedValue.ToString())) Then
                Return MarcarValidacion("El anexo seleccionado no tiene un codigo válido", Anexo1ComboBox, nMostrarMensaje, AnexoOkControl)
            End If

            AnexoOkControl.OK = TriState.True
            Return True

        End Function

        Private Function ValidarContenedor(ByVal nMostrarMensaje As Boolean) As Boolean
            If (CodigoContenedor1TextBox.Text.Length <= 9) Then
                Return MarcarValidacion("El minimo de caracteres para el código del contenedor son 9", CodigoContenedor1TextBox, nMostrarMensaje, ContenedorOkControl)
            End If

            If (CodigoContenedor1TextBox.Text.Trim() = "") Then
                Return MarcarValidacion("El código del contenedor es obligatorio", CodigoContenedor1TextBox, nMostrarMensaje, ContenedorOkControl)
            End If

            If (CodigoContenedor1TextBox.Text <> CodigoContenedor2TextBox.Text) Then
                Return MarcarValidacion("El código del contenedor digitado en la confirmación no corresponde con la primera captura", CodigoContenedor1TextBox, nMostrarMensaje, ContenedorOkControl)
            End If

            If (Not Slyg.Tools.DataConvert.IsNumeric(CodigoContenedor1TextBox.Text)) Then
                Return MarcarValidacion("El código del contenedor no contiene un valor valido", CodigoContenedor1TextBox, nMostrarMensaje, ContenedorOkControl)
            End If

            ContenedorOkControl.OK = TriState.True
            Return True
        End Function

        Private Function ValidarTipoMovimiento(ByVal nMostrarMensaje As Boolean) As Boolean
            If (TipoMovimientoComboBox.SelectedIndex = -1) Then
                Return MarcarValidacion("Se debe seleccionar el tipo de movimiento", TipoMovimientoComboBox, nMostrarMensaje, TipoMovimientoOkControl)
            End If

            TipoMovimientoOkControl.OK = TriState.True
            Return True
        End Function

        Private Function ValidarFechaMovimiento(ByVal nMostrarMensaje As Boolean) As Boolean
            Dim mensaje = ValidarFecha(FechaMovimiento1MaskedTextBox.Text)
            If (mensaje <> "") Then
                Return MarcarValidacion(mensaje, FechaMovimiento1MaskedTextBox, nMostrarMensaje, FechaMovimientoOkControl)
            End If
            If (FechaMovimiento1MaskedTextBox.Text <> FechaMovimiento2MaskedTextBox.Text) Then
                FechaMovimiento1MaskedTextBox.Focus()
                Return MarcarValidacion("La fecha de movimiento digitada en la confirmación no corresponde con la primera captura", FechaMovimiento1MaskedTextBox, nMostrarMensaje, FechaMovimientoOkControl)
            End If

            FechaMovimientoOkControl.OK = TriState.True
            Return True
        End Function

        Private Function ValidarCantidadDocDiceContener(ByVal nMostrarMensaje As Boolean) As Boolean
            If (CantidadDocDiceContenerTextBox.Text.Trim() = "") Then
                Return MarcarValidacion("El campo [Cantidad Doc dice contener] es obligatorio", CantidadDocDiceContenerTextBox, nMostrarMensaje, CantidadDocDiceContenerOkControl)
            End If

            CantidadDocDiceContenerOkControl.OK = TriState.True
            Return True
        End Function

        Private Function ValidarCantidadDocFisicos(ByVal nMostrarMensaje As Boolean) As Boolean
            If (CantidadDocFisicosTextBox.Text.Trim() = "") Then
                Return MarcarValidacion("El campo [Cantidad Doc físicos] es obligatorio", CantidadDocFisicosTextBox, nMostrarMensaje, CantidadDocFisicosOkControl)
            End If

            CantidadDocFisicosOkControl.OK = TriState.True
            Return True
        End Function

        'Private Function ValidarCantidadCoincidenAnexo23(ByVal nMostrarMensaje As Boolean) As Boolean
        '    If FechaCoincideAnexo23ComboBox.SelectedItem Is "NO" Then
        '        If (CantCoincidenDesktopTextBox.Text.Trim() = "") Then
        '            Return MarcarValidacion("El campo [Cantidad coinciden] es obligatorio", CantCoincidenDesktopTextBox, nMostrarMensaje, CantCoincidenFechaAnexo23OkControl)
        '        End If
        '    End If

        '    CantCoincidenFechaAnexo23OkControl.OK = TriState.True
        '    Return True
        'End Function

        'Private Function ValidarCantidadNoCoincidenAnexo23(ByVal nMostrarMensaje As Boolean) As Boolean
        '    If FechaCoincideAnexo23ComboBox.SelectedItem Is "NO" Then
        '        If (CantNoCoincidenDesktopTextBox.Text.Trim() = "") Then
        '            Return MarcarValidacion("El campo [Cantidad no coinciden] es obligatorio", CantNoCoincidenDesktopTextBox, nMostrarMensaje, CantNoCoincidenFechaAnexo23OkControl)
        '        End If
        '    End If


        '    CantNoCoincidenFechaAnexo23OkControl.OK = TriState.True
        '    Return True
        'End Function

        Private Function ValidarTipoContenedor(ByVal nMostrarMensaje As Boolean) As Boolean
            If (TipoContenedorComboBox.SelectedIndex = -1) Then
                Return MarcarValidacion("Se debe seleccionar el tipo de contenedor", TipoContenedorComboBox, nMostrarMensaje, TipoContenedorOkControl)
            End If

            TipoContenedorOkControl.OK = TriState.True
            Return True
        End Function

        Private Function ValidarDocumentoOrdenados(ByVal nMostrarMensaje As Boolean) As Boolean
            If (DocumentosOrdenadosComboBox.SelectedIndex = -1) Then
                Return MarcarValidacion("Se debe establecer si los documentos se encuentran ordenados", DocumentosOrdenadosComboBox, nMostrarMensaje, DocumentosOrdenadosOkControl)
            End If

            DocumentosOrdenadosOkControl.OK = TriState.True
            Return True
        End Function

        Private Function ValidarNovedades(ByVal nMostrarMensaje As Boolean) As Boolean
            If (PresentaNovedadesComboBox.SelectedIndex = -1) Then
                Return MarcarValidacion("Se debe establecer si el destape presenta novedades", PresentaNovedadesComboBox, nMostrarMensaje, NovedadesOkControl)
            End If

            NovedadesOkControl.OK = TriState.True
            Return True
        End Function

        Private Function ValidarCoincideFechaAnexo23(ByVal nMostrarMensaje As Boolean) As Boolean
            If (FechaCoincideAnexo23ComboBox.SelectedIndex = -1) Then
                Return MarcarValidacion("Se debe establecer si la fecha del Anexo23 coincide con la de los documentos", FechaCoincideAnexo23ComboBox, nMostrarMensaje, CoincideFechaAnexo23OkControl)
            End If

            CoincideFechaAnexo23OkControl.OK = TriState.True
            Return True
        End Function

        Private Function ValidarDocumentosSinOrden(ByVal nMostrarMensaje As Boolean) As Boolean
            If (DocumentosOrdenadosComboBox.Text.ToUpper() = "NO") Then
                Dim chequeados As Integer = 0
                For i As Integer = 0 To OrdenDocumentosCheckedListBox.Items.Count - 1
                    If (OrdenDocumentosCheckedListBox.GetItemChecked(i)) Then
                        chequeados += 1
                    End If
                Next
                If (chequeados = 0) Then
                    Return MarcarValidacion("Se debe marcar por los menos un elemento no ordenado", OrdenDocumentosCheckedListBox, nMostrarMensaje, DocumentosSinOrdenOkControl)
                End If
            End If

            DocumentosSinOrdenOkControl.OK = TriState.True
            Return True
        End Function

        Private Function ValidarPresentaNovedades(ByVal nMostrarMensaje As Boolean) As Boolean
            If (PresentaNovedadesComboBox.Text.ToUpper() = "SI") Then
                Dim chequeados As Integer = 0
                For i As Integer = 0 To NovedadesCheckedListBox.Items.Count - 1
                    If (NovedadesCheckedListBox.GetItemChecked(i)) Then
                        chequeados += 1
                    End If
                Next
                If (chequeados = 0) Then
                    Return MarcarValidacion("Se debe marcar por los menos una Novedad", NovedadesCheckedListBox, nMostrarMensaje, PresentaNovedadesOkControl)
                End If
            End If

            PresentaNovedadesOkControl.OK = TriState.True
            Return True
        End Function

        Private Function ValidarCamposContenedor() As Boolean
            If (Not ValidarOficina(True)) Then Return False
            If (Not ValidarContenedor(True)) Then Return False
            If (Not ValidarTipoMovimiento(True)) Then Return False
            If (Not ValidarFechaMovimiento(True)) Then Return False
            If (Not ValidarCantidadDocDiceContener(True)) Then Return False
            If (Not ValidarCantidadDocFisicos(True)) Then Return False
            If (Not ValidarTipoContenedor(True)) Then Return False
            If (Not ValidarDocumentoOrdenados(True)) Then Return False
            If (Not ValidarDocumentosSinOrden(True)) Then Return False
            If (Not ValidarAnexos(True)) Then Return False
            If (Not ValidarStickerOficina()) Then Return False
            If (Not ValidarPresentaNovedades(True)) Then Return False
            If (Not ValidarCoincideFechaAnexo23(True)) Then Return False
            If (Not ValidarNovedades(True)) Then Return False
            'If (Not ValidarCantidadCoincidenAnexo23(True)) Then Return False
            'If (Not ValidarCantidadNoCoincidenAnexo23(True)) Then Return False
            Return True
        End Function

        Public Function ValidarFecha(ByVal nTextoFecha As String) As String
            Try
                TextoAFecha(nTextoFecha)
            Catch ex As Exception
                Return "Fecha no valida : " + ex.Message
            End Try

            Return ""
        End Function

        Public Function TextoAFecha(ByVal nTextoFecha As String) As DateTime
            Dim partes = nTextoFecha.Split("/-".ToCharArray())
            Dim partes2 = _OT_Fecha.Split("/-".ToCharArray())
            Dim fecha_proceso As DateTime
            Dim fecha_movimiento As DateTime

            Try
                fecha_proceso = New DateTime(CInt(partes2(2)), CInt(partes2(1)), CInt(partes2(0)))
                fecha_movimiento = New DateTime(CInt(partes(2)), CInt(partes(1)), CInt(partes(0)))
            Catch ex As Exception
                Throw New Exception("Recuerde que el formato para la fecha es DD/MM/AAAA")
            End Try


            Dim valido As Boolean

            If (CInt(partes(2)) >= 2011) Then
                If (CInt(partes(1)) > 0) And (CInt(partes(1)) <= 12) Then
                    If (CInt(partes(0)) > 0) And (CInt(partes(0)) <= DateTime.DaysInMonth(CInt(partes(2)), CInt(partes(1)))) Then
                        If fecha_movimiento <= fecha_proceso Then
                            valido = True
                        Else
                            Throw New Exception("Recuerde que la Fecha de Movimiento debe ser menor o igual a la Fecha de Proceso")
                        End If
                    Else
                        Throw New Exception("Dia Invalido")
                    End If

                Else
                    Throw New Exception("Mes Invalido")
                End If
            Else
                Throw New Exception("Año Invalido, Recuerde que el año debe ser mayor o igual a 2011")
            End If

            If valido Then
                Return fecha_movimiento
            Else
                Return fecha_movimiento
            End If
        End Function

        Private Function MarcarValidacion(ByVal nMensaje As String, ByVal nControl As Control, ByVal nMostrarMensaje As Boolean, ByVal nOkControl As OkControl) As Boolean
            If (Not nOkControl Is Nothing) Then
                nOkControl.OK = TriState.False
                DesktopMessageBoxControl.DesktopMessageShow(nMensaje, "Destape", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
            End If

            If (nMostrarMensaje) Then
                DesktopMessageBoxControl.DesktopMessageShow(nMensaje, "Destape", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                nControl.Focus()
            End If

            Return False
        End Function

        Private Function IgualarCodOficinayNomOficina(ByVal nComboBoxCodOfic As ComboBox, ByVal nComboBoxNomOfic As ComboBox) As Boolean
            Dim NomOficSelectedIndex = -1
            If (Not nComboBoxCodOfic.SelectedValue Is Nothing) Then
                Dim CodOficina = nComboBoxCodOfic.SelectedValue.ToString()

                If EsCob = False Then

                    For i As Integer = 0 To nComboBoxNomOfic.Items.Count - 1
                        Dim itemRow = DirectCast(nComboBoxNomOfic.Items(i), DataRowView).Row
                        Dim valorItem = CStr(itemRow(CTA_OficinaEnum.id_Oficina.ColumnName))
                        If (CodOficina = valorItem) Then
                            NomOficSelectedIndex = i
                            Exit For
                        End If
                    Next
                Else
                    For i As Integer = 0 To nComboBoxNomOfic.Items.Count - 1
                        Dim itemRow = DirectCast(nComboBoxNomOfic.Items(i), DataRowView).Row
                        Dim valorItem = CStr(itemRow(CTA_Cob_OficinaEnum.id_Oficina.ColumnName))
                        If (CodOficina = valorItem) Then
                            NomOficSelectedIndex = i
                            Exit For
                        End If
                    Next

                End If


            End If

            Return (nComboBoxNomOfic.SelectedIndex = NomOficSelectedIndex)
        End Function

        Private Sub CargarValidacionOrdenamiento()
            Dim dbmCore As DBCoreDataBaseManager = Nothing

            Try
                dbmCore = New DBCoreDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Core)
                'dbmCore.DataBase.Identifier_Date_Format = _Plugin.Manager.DesktopGlobal.IdentifierDateFormat
                dbmCore.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                Dim ValidacionesDataTable = dbmCore.SchemaConfig.TBL_Validacion.DBGet(Program.BanagrarioDocumentoTapaId, Nothing)

                OrdenDocumentosCheckedListBox.Items.Clear()

                For Each ValidacionRow In ValidacionesDataTable
                    OrdenDocumentosCheckedListBox.Items.Add(ValidacionRow.Pregunta_Validacion)
                Next

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Destape", ex)
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            End Try
        End Sub

        Private Sub CargarNovedades()
            Dim dmAgrario As DBAgrarioDataBaseManager = Nothing

            Try
                dmAgrario = New DBAgrarioDataBaseManager(_Plugin.BancoAgrarioConnectionString)
                'dmAgrario.DataBase.Identifier_Date_Format = _Plugin.Manager.DesktopGlobal.IdentifierDateFormat
                dmAgrario.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                'Dim NovedadesDataTable = dmAgrario.SchemaConfig.TBL_Novedades.DBGet(Nothing, Program.BanagrarioDocumentoTapaId)

                Dim NovedadesDataTable = dmAgrario.SchemaConfig.TBL_Novedades.DBFindByfk_DocumentoEliminado(Program.BanagrarioDocumentoTapaId, False)

                NovedadesCheckedListBox.Items.Clear()

                For Each NovedadRow In NovedadesDataTable
                    NovedadesCheckedListBox.Items.Add(NovedadRow.Novedad)
                Next

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Destape ", ex)
            Finally
                If (dmAgrario IsNot Nothing) Then dmAgrario.Connection_Close()
            End Try
        End Sub

        Private Sub ValidarTipoOficina()
            Dim dmAgrario As DBAgrarioDataBaseManager = Nothing

            loading = True

            Try
                dmAgrario = New DBAgrarioDataBaseManager(_Plugin.BancoAgrarioConnectionString)
                'dmAgrario.DataBase.Identifier_Date_Format = _Plugin.Manager.DesktopGlobal.IdentifierDateFormat
                dmAgrario.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)



                Dim TipoOficinaDataTable = dmAgrario.SchemaConfig.PA_Get_Tipo_Oficina.DBExecute(Integer.Parse(CodOficina1ComboBox.SelectedValue.ToString()))

                If TipoOficinaDataTable.Count > 0 Then
                    'Dim Tipo_Oficina = TipoOficinaDataTable.Rows(0)("fk_Oficina_Tipo").ToString()
                    Dim Tipo_Oficina = TipoOficinaDataTable(0).fk_Oficina_Tipo
                    If (Tipo_Oficina = 5) Then
                        'Dim OficinasDataTable = dmAgrario.SchemaProcess.CTA_Cob_Oficina.DBFindByid_COB(CType(TipoOficinaDataTable.Rows(0)("fk_Cob").ToString(), Global.Slyg.Tools.SlygNullable(Of Short)))
                        Dim OficinasDataTable = dmAgrario.SchemaProcess.CTA_Cob_Oficina.DBFindByid_COB(TipoOficinaDataTable(0).fk_COB)
                        Dim ofiView = OficinasDataTable.DefaultView
                        EsCob = True
                        OficinaCobPanel.Visible = True

                        Utilities.LlenarCombo(CodOficinaCobComboBox, ofiView.ToTable(), CTA_Cob_OficinaEnum.id_Oficina.ColumnName, CTA_Cob_OficinaEnum.id_Oficina.ColumnName)
                        'Utilities.LlenarCombo(CodOficina2ComboBox, ofiView.ToTable(), CTA_Cob_OficinaEnum.id_Oficina.ColumnName, CTA_Cob_OficinaEnum.id_Oficina.ColumnName)

                        ofiView = OficinasDataTable.DefaultView
                        ofiView.Sort = CTA_Cob_OficinaEnum.Nombre_Oficina.ColumnName
                        Utilities.LlenarCombo(NombreOficinaCobComboBox, ofiView.ToTable(), CTA_Cob_OficinaEnum.id_Oficina.ColumnName, CTA_Cob_OficinaEnum.Nombre_Oficina.ColumnName)
                        'Utilities.LlenarCombo(NombreOficina2ComboBox, ofiView.ToTable(), CTA_Cob_OficinaEnum.id_Oficina.ColumnName, CTA_Cob_OficinaEnum.Nombre_Oficina.ColumnName)

                    Else
                        OficinaCobPanel.Visible = False
                        EsCob = False
                        Dim EntidadProcesamiento As Short = _Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad
                        Dim SedeProcesamiento = _Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Sede
                        Dim oficinaData = dmAgrario.SchemaProcess.CTA_Oficina.DBFindByfk_Entidadfk_Sede(EntidadProcesamiento, SedeProcesamiento)
                        Dim ofiView = oficinaData.DefaultView

                        Utilities.LlenarCombo(CodOficina2ComboBox, ofiView.ToTable(), CTA_OficinaEnum.id_Oficina.ColumnName, CTA_OficinaEnum.id_Oficina.ColumnName)

                        ofiView = oficinaData.DefaultView
                        ofiView.Sort = CTA_OficinaEnum.Nombre_Oficina.ColumnName
                        Utilities.LlenarCombo(NombreOficina2ComboBox, ofiView.ToTable(), CTA_OficinaEnum.id_Oficina.ColumnName, CTA_OficinaEnum.Nombre_Oficina.ColumnName)

                    End If
                End If

                CodOficinaCobComboBox.SelectedIndex = -1
                CodOficina2ComboBox.SelectedIndex = -1
                NombreOficinaCobComboBox.SelectedIndex = -1
                NombreOficina2ComboBox.SelectedIndex = -1

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Destape ", ex)
            Finally
                loading = False
                If (dmAgrario IsNot Nothing) Then dmAgrario.Connection_Close()
            End Try
        End Sub

#End Region

    End Class
End Namespace