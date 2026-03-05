Imports Miharu.Desktop.Forms.Facturacion
Imports Miharu.Desktop.Clases
Imports Miharu.Desktop.Forms.Imaging
Imports Miharu.Desktop.Controls
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Forms.Risk
Imports Miharu.Desktop.Forms.Reportes

Namespace Forms

    Public Class FormMain
        Inherits Form

#Region " Declaraciones "

        Private _salir As Boolean = False

        'Private _SelectedProcess As IProcessLibrary

        Private _RiskProcess As Miharu.Risk.Library.ProcessLibrary
        Private _ImagingProcess As Miharu.Imaging.Library.ProcessLibrary
        Private _CustodyProcess As Custody.Library.ProccessLibrary

#End Region

#Region " Eventos "

        Private Sub FormMain_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            CargaPermisos()
            OpenImaging()
        End Sub

        Private Sub ProyectosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ProyectosToolStripMenuItem.Click
            OpenImagingProyectos()
        End Sub


        Private Sub GarantiasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles GarantiasToolStripMenuItem.Click
            OpenRisk()
        End Sub

        Private Sub AcercaDeMiharuDesktopToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AcercaDeMiharuDesktopToolStripMenuItem.Click
            ShowAbout()
        End Sub

        Private Sub RiskToolStripButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles RiskToolStripButton.Click
            OpenRisk()
        End Sub

        Private Sub ImagingToolStripButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ImagingToolStripButton.Click
            OpenImaging()
        End Sub

        Private Sub CustodyToolStripButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CustodyToolStripButton.Click
            OpenCustody()
        End Sub

        Private Sub CascadaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CascadaToolStripMenuItem.Click
            Me.LayoutMdi(MdiLayout.Cascade)
        End Sub

        Private Sub HorizontalToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles HorizontalToolStripMenuItem.Click
            Me.LayoutMdi(MdiLayout.TileHorizontal)
        End Sub

        Private Sub VerticalToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles VerticalToolStripMenuItem.Click
            Me.LayoutMdi(MdiLayout.TileVertical)
        End Sub

        Private Sub FormMain_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
            If Not _salir Then
                If Not Salir() Then
                    e.Cancel = True
                End If
            End If
        End Sub

        Private Sub SalirToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles SalirToolStripMenuItem.Click
            If Salir() Then
                _salir = True
                Me.Close()
            End If
        End Sub

        Private Sub GenerarToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles GenerarToolStripMenuItem.Click
            OpenBilling()
        End Sub

        Private Sub HomologaciónCodigosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles HomologaciónCodigosToolStripMenuItem.Click
            OpenHomologacionFacturacion()
        End Sub

        Private Sub EsquemasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles EsquemasToolStripMenuItem.Click
            OpenEsquemaFacturacion()
        End Sub

        Private Sub DesbloquearUsuarioToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles DesbloquearUsuarioToolStripMenuItem.Click
            Dim DesbloquearUser As New FormDesbloquear_Usuarios
            DesbloquearUser.ShowDialog()
        End Sub

        Private Sub ServiciosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ServiciosToolStripMenuItem.Click
            OpenServiciosFacturacion()
        End Sub

        Private Sub ProyectosToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ProyectosToolStripMenuItem1.Click
            OpenProyectoRisk()
        End Sub

        Private Sub AdmonPuntosToolStripMenuItem_Click(sender As System.Object, e As EventArgs) Handles AdmonPuntosToolStripMenuItem.Click
            OpenAdmonPuntosRisk()
        End Sub

        Private Sub CambioContraseñaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CambioContraseñaToolStripMenuItem.Click
            Program.ChangePassword("")
        End Sub

        Private Sub ReportesToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ReportesToolStripMenuItem1.Click
            Dim VisorReportes As New FormVisorReportes()
            VisorReportes.ShowDialog()
        End Sub

        Private Sub DocumentosToolStripMenuItem_Click(sender As System.Object, e As EventArgs) Handles DocumentosToolStripMenuItem.Click
            Dim fDocumento As New Parametrizacion.FormDocumento
            fDocumento.ShowDialog()
        End Sub


        Private Sub TipologiaToolStripMenuItem_Click(sender As System.Object, e As EventArgs) Handles TipologiaToolStripMenuItem.Click
            Dim fTipologia As New Parametrizacion.FormTipologia
            fTipologia.ShowDialog()
        End Sub

        Private Sub CamposToolStripMenuItem_Click(sender As System.Object, e As EventArgs) Handles CamposToolStripMenuItem.Click
            Dim fCampos As New Parametrizacion.FormParametrizacionCampos
            fCampos.ShowDialog()
        End Sub

        Private Sub CamposListaToolStripMenuItem_Click(sender As System.Object, e As EventArgs) Handles CamposListaToolStripMenuItem.Click
            Dim fCamposLista As New Parametrizacion.FormCampoLista
            fCamposLista.ShowDialog()
        End Sub

        Private Sub CamposCarpetaToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles CamposCarpetaToolStripMenuItem.Click
            Dim fCamposLlave As New Parametrizacion.FormCampoCarpeta
            fCamposLlave.ShowDialog()
        End Sub

        Private Sub ValidacionesToolStripMenuItem_Click(sender As System.Object, e As EventArgs) Handles ValidacionesToolStripMenuItem.Click
            Dim fValidaciones As New Parametrizacion.FormValidaciones
            fValidaciones.ShowDialog()
        End Sub

        Private Sub ValidacionesDinamicasToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ValidacionesDinamicasToolStripMenuItem.Click
            Dim fValidacionesDinamicas As New Parametrizacion.FormValidacionesDinamicas
            fValidacionesDinamicas.ShowDialog()
        End Sub

        Private Sub TablaAsociadaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles TablaAsociadaToolStripMenuItem.Click
            Dim fTablaAsociada As New Parametrizacion.FormTablaAsociada
            fTablaAsociada.ShowDialog()
        End Sub

        'Accesos directos
        Private Sub FormMain_KeyDown(ByVal sender As System.Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
            Select Case e.KeyCode
                Case Keys.F1
                    Dim Ayuda As New FormAyuda()
                    Ayuda.Show()
                    'Case Keys.F5
                    '    SeleccionarCambioProyecto()
                Case Keys.F6
                    'OpenRisk()
                Case Keys.F7

                Case Keys.F8

                Case Keys.F9
                    'If ImagingToolStripButton.Enabled Then
                    '    OpenImaging()
                    'End If
                Case Keys.F10
                    'OpenCustody()
            End Select
        End Sub

        Private Sub categoriasToolStripMenuItem_Click(sender As System.Object, e As EventArgs) Handles categoriasToolStripMenuItem.Click
            Dim f As New Parametrizacion.FormCategoria()
            f.ShowDialog()
        End Sub

        Private Sub permisosRolesToolStripMenuItem_Click(sender As System.Object, e As EventArgs) Handles permisosRolesToolStripMenuItem.Click
            Dim f As New Parametrizacion.FormRoles()
            f.ShowDialog()
        End Sub
        Private Sub ImagenesToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ImagenesToolStripMenuItem.Click
            Dim objSelecionarProyecto As New FormSelecionarProyecto()
            objSelecionarProyecto.ShowDialog()
        End Sub
#End Region

#Region " Metodos "

        Private Sub OpenProyectoRisk()
            Dim FormProyectorisk As New FormParProyecto
            FormProyectorisk.ShowDialog()
        End Sub

        Private Sub OpenAdmonPuntosRisk()
            Dim FormAdmonPuntos As New FormAdmonPuntos
            FormAdmonPuntos.ShowDialog()
        End Sub

        Private Sub OpenServiciosFacturacion()
            Dim FormServicio As New FormServiciosFacturacion
            FormServicio.ShowDialog()
        End Sub

        Private Sub OpenEsquemaFacturacion()
            Dim FormEsquema As New FormConsultarEsquemaFacturacion
            FormEsquema.ShowDialog()
        End Sub

        Private Sub OpenHomologacionFacturacion()
            Dim FomrHomologacion As New FormHomologacionFacturacion
            FomrHomologacion.ShowDialog()
        End Sub

        Private Sub OpenImagingProyectos()
            Dim ImagingProyecto As New FormParProyectoImaging
            ImagingProyecto.ShowDialog()
        End Sub

        Private Sub OpenRisk()
            Try
                If Me.MdiChildren.Length = 0 Then
                    If (_RiskProcess Is Nothing) Then
                        _RiskProcess = CType(ProcessLibraryFactory.getProcessLibrary(ProcessLibraryType.Risk), Miharu.Risk.Library.ProcessLibrary)
                    End If

                    '_RiskProcess = _RiskProcess

                    _RiskProcess.SetDesktopGlobal(Program.DesktopGlobal)
                    _RiskProcess.SetSesion(Program.Sesion)
                    '_SelectedProcess.SetPluginManager(Program.PluginManager)

                    'Program.PluginManager.ChangePluingsProcess(_SelectedProcess)

                    Program.DesktopGlobal = Program.DesktopGlobal
                    Miharu.Imaging.Library.Program.Sesion = Program.Sesion

                    If _RiskProcess.CallSelectProject() Then
                        _RiskProcess.CallShowWorkSpace(Me)
                    Else
                        _RiskProcess = Nothing
                        _RiskProcess = Nothing
                    End If
                Else
                    DesktopMessageBox.DesktopMessageBoxControl.DesktopMessageShow("Debe cerrar el módulo abierto actualmente antes de abrir uno nuevo.", "Alerta de Desktop", DesktopMessageBox.DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub OpenImaging()
            Try
                If Me.MdiChildren.Length = 0 Then
                    If (_ImagingProcess Is Nothing) Then
                        _ImagingProcess = CType(ProcessLibraryFactory.getProcessLibrary(ProcessLibraryType.Imaging), Miharu.Imaging.Library.ProcessLibrary)
                    End If

                    '_ImagingProcess = _ImagingProcess

                    _ImagingProcess.SetDesktopGlobal(Program.DesktopGlobal)
                    _ImagingProcess.SetSesion(Program.Sesion)
                    '_ImagingProcess.SetPluginManager(Program.PluginManager)

                    If _ImagingProcess.CallSelectProject() Then
                        _ImagingProcess.CallShowWorkSpace(Me)
                    Else
                        _ImagingProcess = Nothing
                        _ImagingProcess = Nothing
                    End If
                Else
                    DesktopMessageBox.DesktopMessageBoxControl.DesktopMessageShow("Debe cerrar el módulo abierto actualmente antes de abrir uno nuevo.", "Alerta de Desktop", DesktopMessageBox.DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub OpenCustody()
            Try
                If (_CustodyProcess Is Nothing) Then
                    _CustodyProcess = CType(ProcessLibraryFactory.getProcessLibrary(ProcessLibraryType.Custody), Custody.Library.ProccessLibrary)
                End If

                '_CustodyProcess = _CustodyProcess

                _CustodyProcess.SetDesktopGlobal(Program.DesktopGlobal)
                _CustodyProcess.SetSesion(Program.Sesion)
                '_CustodyProcess.SetPluginManager(Program.PluginManager)

                If _CustodyProcess.CallSelectProject() Then
                    _CustodyProcess.CallShowWorkSpace(Me)
                Else
                    _CustodyProcess = Nothing
                    _CustodyProcess = Nothing
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub OpenBilling()
            Try
                Dim f As New FormFacturacion()
                f.ShowDialog()
            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub ShowAbout()
            Dim f = New FormAbout()
            f.ShowDialog()
        End Sub

        Private Function Salir() As Boolean
            Dim bReturn As Boolean = False
            If DesktopMessageBox.DesktopMessageBoxControl.DesktopMessageShow("¿Desea salir de la aplicación?", "Salir de Miharu DDO", DesktopMessageBox.DesktopMessageBoxControl.IconEnum.AdvertencyIcon, False) = MsgBoxResult.Ok Then
                For i = 0 To Me.MdiChildren.Length - 1
                    Me.MdiChildren(0).Close()
                Next
                bReturn = True
            End If
            Return bReturn
        End Function

        Private Sub CargaPermisos()
            Try
                ''Se habilitan los botones de acuerdo al perfil del usuario
                ConfiguracionToolStripMenuItem.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Configuracion)
                FacturaciónToolStripMenuItem.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Facturacion)
                ReportesToolStripMenuItem1.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Informes)
                ExportarToolStripMenuItem.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Imaging.Proceso.Exportar_Log)
                RiskToolStripButton.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Risk.Path)
                PaperToolStripButton.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Paper)
                CreditFactoryToolStripButton.Enabled = False
                ImagingToolStripButton.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Imaging.Path)
                CustodyToolStripButton.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Custody)
                CamposCarpetaToolStripMenuItem.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.CampoCarpeta)

            Catch ex As Exception
                DesktopMessageBox.DesktopMessageBoxControl.DesktopMessageShow("CargaPermisos", ex)
            End Try
        End Sub

#End Region

    End Class

End Namespace