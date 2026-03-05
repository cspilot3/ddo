Imports System.Windows.Forms
Imports DBArchiving
Imports DBCore
Imports DBCore.SchemaConfig
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Controls.DesktopTextBox
Imports Miharu.Desktop.Library.Config

Namespace Risk.Forms.Empaque

    Public Class ReempaqueDialog

#Region " Declaraciones "

        Private _plugin As BanagrarioRiskPlugin
        Public Shared NoContenedorNuevo As String = ""
        Public Shared TipoContenedorNuevo As String = ""
        Public Shared TipoContenedorIndexNuevo As Integer = 0
        Public Shared ContenedorCreado As Boolean = False
        Private Const OtActual As Integer = 0

#End Region

#Region " Constructores "

        Public Sub New(ByVal nBanagrarioDesktopPlugin As BanagrarioRiskPlugin, ByRef nOtActual As Integer)
            InitializeComponent()
            _plugin = nBanagrarioDesktopPlugin
            nOtActual = nOtActual
        End Sub

#End Region

#Region " Eventos "

        Private Sub TipoCont1DesktopComboBox_Leave_1(ByVal sender As System.Object, ByVal e As EventArgs) Handles TipoCont1DesktopComboBox.Leave
            TipoCont2ComboBox.Focus()
        End Sub

        Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles Aceptar_Button.Click
            If IgualdadContenedor() Then
                If IgualdadTipoContenedor() Then
                    If ValidarContenedor() Then
                        If BuscarContenedor(Contenedor1DesktopTextBox.Text) Then
                            NoContenedorNuevo = Contenedor1DesktopTextBox.Text
                            TipoContenedorNuevo = TipoCont1DesktopComboBox.Text
                            TipoContenedorIndexNuevo = CInt(TipoCont1DesktopComboBox.SelectedValue)
                            ContenedorCreado = True
                            Me.DialogResult = DialogResult.OK
                            Me.Close()
                        Else
                            LimpiarControles()
                            Me.DialogResult = DialogResult.Cancel
                            ContenedorOkControl.OK = TriState.False
                        End If
                    Else
                        LimpiarControles()
                    End If

                End If
            End If
        End Sub

        Private Sub TipoCont2ComboBox_KeyDown(ByVal sender As System.Object, ByVal e As KeyEventArgs) Handles TipoCont2ComboBox.KeyDown
            If (e.KeyCode = Keys.Enter) Then
                If IgualdadTipoContenedor() Then
                    Aceptar_Button.Focus()
                End If
            End If
        End Sub

        Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles Cancel_Button.Click
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        End Sub

        Private Sub ReempaqueDialog_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            LlenarCombos()
            lblTipoContenedor.Visible = False
            AddHandler Contenedor1DesktopTextBox.LostFocus, AddressOf ControlLostFocus
            AddHandler Contenedor1DesktopTextBox.GotFocus, AddressOf ControlGotFocus
            TipoCont1DesktopComboBox.SelectedIndex = -1
            TipoCont2ComboBox.SelectedIndex = -1
        End Sub

        Private Sub Contenedor1DesktopTextBox_KeyDown(ByVal sender As System.Object, ByVal e As KeyEventArgs) Handles Contenedor1DesktopTextBox.KeyDown
            If (e.KeyCode = Keys.Enter) Then
                Contenedor2DesktopTextBox.Focus()
            End If
        End Sub

        'Private Sub TipContenedorPanel_Click(ByVal sender As System.Object, ByVal e As EventArgs)
        '    TipoCont1DesktopComboBox.Focus()
        '    lblTipoContenedor.Visible = False
        'End Sub

        Private Sub TipoCont2ComboBox_Enter(ByVal sender As System.Object, ByVal e As EventArgs) Handles TipoCont2ComboBox.Enter
            lblTipoContenedor.Visible = True
        End Sub

        Private Sub Contenedor2DesktopTextBox_Leave(ByVal sender As System.Object, ByVal e As EventArgs) Handles Contenedor2DesktopTextBox.Leave
            If IgualdadContenedor() Then
                If BuscarContenedor(Contenedor1DesktopTextBox.Text) Then
                    TipoCont1DesktopComboBox.Focus()
                    ContenedorOkControl.Visible = True
                Else
                    ContenedorOkControl.OK = TriState.False
                    ContenedorOkControl.Visible = True
                End If
            End If
        End Sub

        'Private Sub TipoCont1DesktopComboBox_Leave(ByVal sender As System.Object, ByVal e As EventArgs)
        '    TipoCont2ComboBox.Focus()
        'End Sub

        Private Sub LimpiarControles()
            ContenedorCreado = False
            NoContenedorNuevo = ""
            TipoContenedorNuevo = ""
            TipoContenedorIndexNuevo = 0
        End Sub

        Private Sub TipoCont1DesktopComboBox_Enter(ByVal sender As System.Object, ByVal e As EventArgs) Handles TipoCont1DesktopComboBox.Enter
            lblTipoContenedor.Visible = False
        End Sub

        Private Sub TipoCont2ComboBox_Leave(ByVal sender As System.Object, ByVal e As EventArgs) Handles TipoCont2ComboBox.Leave
            IgualdadTipoContenedor()
        End Sub

#End Region

#Region " Metodos "

        Private Sub LlenarCombos()
            Dim dbmCore As DBCoreDataBaseManager = Nothing

            Try
                dbmCore = New DBCoreDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Core)
                dbmCore.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                Dim tipoContenedorData = dbmCore.SchemaConfig.TBL_Campo_Lista_Item.DBFindByfk_Entidadfk_Campo_Lista(_Plugin.Manager.RiskGlobal.Entidad, Program.Banagrario_ListaTipoContenedorId, 0, New TBL_Campo_Lista_ItemEnumList(TBL_Campo_Lista_ItemEnum.Etiqueta_Campo_Lista_Item, True))
                Utilities.LlenarCombo(TipoCont1DesktopComboBox, tipoContenedorData, TBL_Campo_Lista_ItemEnum.Valor_Campo_Lista_Item.ColumnName, TBL_Campo_Lista_ItemEnum.Etiqueta_Campo_Lista_Item.ColumnName)
                Utilities.LlenarCombo(TipoCont2ComboBox, tipoContenedorData, TBL_Campo_Lista_ItemEnum.Valor_Campo_Lista_Item.ColumnName, TBL_Campo_Lista_ItemEnum.Etiqueta_Campo_Lista_Item.ColumnName)
                TipoCont1DesktopComboBox.SelectedIndex = -1
            Catch
                Throw
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            End Try
        End Sub

        Public Sub ControlLostFocus(ByVal sender As System.Object, ByVal e As EventArgs)
            Dim textBoxLostFocus As DesktopTextBoxControl = CType(sender, DesktopTextBoxControl)
            textBoxLostFocus.PasswordChar = CChar("*")
        End Sub

        Public Sub ControlGotFocus(ByVal sender As System.Object, ByVal e As EventArgs)
            Dim textBoxLostFocus As DesktopTextBoxControl = CType(sender, DesktopTextBoxControl)
            textBoxLostFocus.PasswordChar = CChar("")
        End Sub


#End Region

#Region " Funciones "

        Private Function IgualdadTipoContenedor() As Boolean
            If CStr(TipoCont1DesktopComboBox.SelectedValue) <> CStr(TipoCont2ComboBox.SelectedValue) Then
                If TipoCont1DesktopComboBox.SelectedIndex < 0 Then
                    DesktopMessageBoxControl.DesktopMessageShow("Seleccione Porfavor un Tipo de Contenedor", "Empaque", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                    TipoContOKControl.OK = TriState.False
                    TipoContOKControl.Visible = True
                    Return False
                Else
                    DesktopMessageBoxControl.DesktopMessageShow("El Tipo de Contenedor seleccionado no corresponde al elegido inicialmente", "Empaque", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                    TipoContOKControl.Visible = True
                    Return False
                End If
            End If
            TipoContOKControl.OK = TriState.True
            Return True

        End Function

        Private Function IgualdadContenedor() As Boolean

            If (Contenedor1DesktopTextBox.Text <> Contenedor2DesktopTextBox.Text) Then

                If Contenedor1DesktopTextBox.Text = "" Then
                    DesktopMessageBoxControl.DesktopMessageShow("El Número de Contenedor seleccionado no es Valido", "Empaque", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                    ContenedorOkControl.OK = TriState.False
                    Return False
                Else
                    DesktopMessageBoxControl.DesktopMessageShow("El Número de Contenedor seleccionado no corresponde al elegido inicialmente", "Empaque", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                    ContenedorOkControl.OK = TriState.False
                    Return False
                End If


            End If
            ContenedorOkControl.OK = TriState.True
            Return True
        End Function

        Private Function BuscarContenedor(ByVal nContenedorDestape As String) As Boolean

            Dim dbmCore As DBCoreDataBaseManager = Nothing
            Dim dmArchiving As DBArchivingDataBaseManager = Nothing
            Dim mensaje As String


            Try
                dbmCore = New DBCoreDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Core)
                'dbmCore.DataBase.Identifier_Date_Format = _Plugin.Manager.DesktopGlobal.IdentifierDateFormat
                dbmCore.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                dmArchiving = New DBArchivingDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Archiving)
                'dmArchiving.DataBase.Identifier_Date_Format = _Plugin.Manager.DesktopGlobal.IdentifierDateFormat
                dmArchiving.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                Dim entidadId As Short = _plugin.Manager.RiskGlobal.Entidad
                Dim proyectoId As Short = _plugin.Manager.RiskGlobal.Proyecto

                Dim expCoreExistenteData = dbmCore.SchemaProcess.PA_Expediente_getByKeysEsquema.DBExecute(entidadId, proyectoId, Program.BanagrarioEsquemaRiskId, Nothing, Nothing, nContenedorDestape, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                If (expCoreExistenteData.Count = 0) Then
                    Return True
                End If

                Const folderId As Short = 1
                Dim contenedorEncontrado As Boolean = False
                Dim otEncontrada As Integer = -1

                'Dim expCore As DBCore.SchemaProcess.TBL_ExpedienteType = Nothing

                For Each expCor In expCoreExistenteData
                    Dim expArchivingData = dmArchiving.SchemaRisk.TBL_Folder.DBGet(expCor.id_Expediente, folderId, Nothing)

                    For Each expArc In expArchivingData
                        If (expArc.fk_OT = OtActual) Then
                            If (expArc.fk_Estado < EstadoEnum.Empaque) Then
                                mensaje = "El No de contenedor  " & nContenedorDestape & " se está utilizando, y aun no ha el destape, Utilice por favor otro Número de Contenedor"
                                DesktopMessageBoxControl.DesktopMessageShow(mensaje, "Empaque", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                                Return False
                            End If
                            contenedorEncontrado = True
                            Exit For
                        Else
                            otEncontrada = expArc.fk_OT
                        End If
                    Next
                Next

                If (Not contenedorEncontrado) Then
                    If (otEncontrada <> -1) Then
                        'Dim OTEncontradaData = dmArchiving.SchemaRisk.TBL_OT.DBGet(OTEncontrada)
                        mensaje = "El contenedor " & nContenedorDestape & " se esta utilizando,Utilice por favor otro Número de Contenedor"
                        DesktopMessageBoxControl.DesktopMessageShow(mensaje, "Empaque", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                        Return False
                    Else
                        Return True
                    End If
                End If
                Return False
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Empaque", ex)
                Return False
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                If (dmArchiving IsNot Nothing) Then dmArchiving.Connection_Close()
            End Try
        End Function

        Private Function ValidarContenedor() As Boolean

            If (Contenedor1DesktopTextBox.Text = "") Then
                DesktopMessageBoxControl.DesktopMessageShow("Digite por favor un Número de Contenedor valido", "Empaque", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                Contenedor1DesktopTextBox.Focus()
                Return False
            End If

            If (TipoCont1DesktopComboBox.SelectedIndex = -1) Then
                TipoCont1DesktopComboBox.Focus()
                DesktopMessageBoxControl.DesktopMessageShow("Seleccione algún Tipo de Contenedor", "Empaque", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                Return False
            End If

            Return True


        End Function

#End Region

    End Class

End Namespace