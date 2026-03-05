Imports System.IO
Imports System.ServiceProcess
Imports DBSecurity
Imports Miharu.Reportes.Servicio
Imports Miharu.Security.Library.WebService

Namespace Forms
    Public Class MainForm
#Region " Declaraciones "

        Private _GuardadoServicio As Boolean = False
        Private _Servicio As ServiceController = Nothing
        Private _NotifiIcon_Activo As Icon
        Private _NotifiIcon_Inactivo As Icon
        Private _Cargando As Boolean = False

#End Region

#Region " Eventos "

        Private Sub MainForm_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            LoadConfig()
        End Sub

        Private Sub MainForm_FormClosed(ByVal sender As System.Object, ByVal e As FormClosedEventArgs) Handles MyBase.FormClosed
            Application.Exit()
        End Sub

        Private Sub MainForm_FormClosing(ByVal sender As System.Object, ByVal e As FormClosingEventArgs) Handles MyBase.FormClosing
            If e.CloseReason = CloseReason.UserClosing Then
                AbrirToolStripMenuItem.Enabled = True
                e.Cancel = True
                Me.Visible = False
            End If
        End Sub

        Private Sub mnuSalir_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles mnuSalir.Click
            Application.Exit()
        End Sub

        Private Sub AcercaDeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AcercaDeToolStripMenuItem.Click
            Dim f As New AboutForm

            f.ShowDialog()
        End Sub

        Private Sub AbrirToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AbrirToolStripMenuItem.Click

        End Sub

        Private Sub SalirContextualToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles SalirContextualToolStripMenuItem.Click
            Application.Exit()
        End Sub

        Private Sub IniciarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles IniciarButton.Click
            IniciarServicio()
        End Sub

        Private Sub DetenerButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles DetenerButton.Click
            DetenerServicio()
        End Sub

        Private Sub ReiniciarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ReiniciarButton.Click
            ReiniciarServicio()
        End Sub

        Private Sub GuardarServicioButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles GuardarServicioButton.Click
            SaveServico()
        End Sub

        Private Sub MonitorServicioTimer_Tick(ByVal sender As System.Object, ByVal e As EventArgs) Handles MonitorServicioTimer.Tick
            MonitorearServicio()
        End Sub

        Private Sub NotificacionNotifyIcon_MouseDoubleClick(ByVal sender As System.Object, ByVal e As MouseEventArgs) Handles NotificacionNotifyIcon.MouseDoubleClick
            Me.Show()
            Me.WindowState = FormWindowState.Normal
            Me.Focus()
        End Sub

        Private Sub ProbarButton_MouseDown(ByVal sender As System.Object, ByVal e As MouseEventArgs) Handles ProbarButton.MouseDown
            If (e.Button = MouseButtons.Left) Then
                ProbarServicioWeb()
            ElseIf (e.Button = MouseButtons.Right) Then
                Dim obj As New ReportesService
                obj.IniciarServicio()
            End If
        End Sub

        Private Sub IntervaloTextBox_TextChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles IntervaloTextBox.TextChanged
            ActivarGuardar()
        End Sub

        Private Sub ServicioWebTextBox_TextChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles ServicioWebTextBox.TextChanged
            ActivarGuardar()
        End Sub

        Private Sub PasswordTextBox_TextChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles PasswordTextBox.TextChanged
            ActivarGuardar()
        End Sub

#End Region

#Region " Metodos "

        Private Sub LoadConfig()
            _Cargando = True

            Try
                ' Cargar datos servicio
                If (File.Exists(Program.AppDataPath + ReportesConfig.ConfigFileName)) Then
                    Program.Config = ReportesConfig.Deserialize(Program.AppDataPath)
                    _GuardadoServicio = True
                Else
                    _GuardadoServicio = False
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

            ' Iconos de notificación
            _NotifiIcon_Activo = Icon.FromHandle((CType(IconosImageList.Images(0), Bitmap)).GetHicon())
            _NotifiIcon_Inactivo = Icon.FromHandle((CType(IconosImageList.Images(1), Bitmap)).GetHicon())

            ' Servicio
            IntervaloTextBox.Text = CStr(Program.Config.Intervalo)
            ServicioWebTextBox.Text = Program.Config.SecurityWebServiceURL
            UserTextBox.Text = Program.Config.User
            PasswordTextBox.Text = ReportesConfig.Decrypt(Program.Config.Password)

            _Cargando = False
            ActivarGuardar()
            MonitorearServicio()
        End Sub

        Private Sub SaveServico()
            If Validar() Then
                ' Servicio
                Program.Config.Intervalo = CInt(IntervaloTextBox.Text)
                Program.Config.SecurityWebServiceURL = ServicioWebTextBox.Text
                Program.Config.User = UserTextBox.Text
                Program.Config.Password = ReportesConfig.Encrypt(PasswordTextBox.Text)

                ReportesConfig.Serialize(Program.Config, Program.AppDataPath)

                _GuardadoServicio = True

                If _GuardadoServicio Then
                    MessageBox.Show("Los datos se almacenaron correctamente, para que los cambios tengan efecto debe reiniciar el servicio", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    MessageBox.Show("Se presento un error al almacenar los datos", "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End If

            ActivarGuardar()
        End Sub

        Private Sub ActivarGuardar()
            If Not _Cargando Then
                ' Servicio
                Dim HayCambios As Boolean

                HayCambios = (Program.Config.User <> UserTextBox.Text) Or
                             (Program.Config.SecurityWebServiceURL <> ServicioWebTextBox.Text) Or
                             (ReportesConfig.Decrypt(Program.Config.Password) <> PasswordTextBox.Text) Or
                             (Program.Config.Intervalo <> CInt(IIf(IntervaloTextBox.Text <> "", IntervaloTextBox.Text, 0)))

                GuardarServicioButton.Enabled = Not _GuardadoServicio Or HayCambios
            End If
        End Sub

        Private Sub MonitorearServicio()
            MonitorServicioTimer.Enabled = False

            Try

                Dim Servicios() As ServiceController

                Servicios = ServiceController.GetServices()
                _Servicio = Nothing

                For Each ServicioEncontrado As ServiceController In Servicios
                    If ServicioEncontrado.ServiceName = "MiharuReportesDDOService" Then
                        _Servicio = ServicioEncontrado
                    End If
                Next

                If _Servicio Is Nothing Then
                    EstadoStatusStripLabel.Text = "El servicio no se encuentra instalado"

                    IniciarButton.Enabled = False
                    DetenerButton.Enabled = False
                    ReiniciarButton.Enabled = False
                Else
                    _Servicio.Refresh()

                    If _Servicio.CanStop Then
                        DetenerButton.Enabled = (_Servicio.Status = ServiceControllerStatus.Running)
                    Else
                        DetenerButton.Enabled = False
                    End If

                    IniciarButton.Enabled = (_Servicio.Status = ServiceControllerStatus.Stopped)
                    ReiniciarButton.Enabled = DetenerButton.Enabled

                    Select Case _Servicio.Status
                        Case ServiceControllerStatus.Running
                            EstadoStatusStripLabel.Text = "Servicio activo"
                            NotificacionNotifyIcon.Icon = _NotifiIcon_Activo
                            NotificacionNotifyIcon.Text = "MiharuReportesDDOService [Activo]"

                        Case Else
                            EstadoStatusStripLabel.Text = "Servicio detenido"
                            NotificacionNotifyIcon.Icon = _NotifiIcon_Inactivo
                            NotificacionNotifyIcon.Text = "MiharuReportesDDOService [Inactivo]"

                    End Select

                End If

            Catch
                IniciarButton.Enabled = False
                DetenerButton.Enabled = False
                ReiniciarButton.Enabled = False

                EstadoStatusStripLabel.Text = "Error al validar el servicio"
            End Try

            MonitorServicioTimer.Enabled = True
        End Sub

        Private Sub IniciarServicio()
            Try
                _Servicio.Start()
                MonitorearServicio()

                NotificacionNotifyIcon.BalloonTipIcon = ToolTipIcon.Info
                NotificacionNotifyIcon.BalloonTipTitle = "MiharuReportesDDOService"
                NotificacionNotifyIcon.BalloonTipText = "El servicio MiharuReportesDDOService ha sido activado"
                NotificacionNotifyIcon.ShowBalloonTip(5000)

            Catch ex As Exception
                MessageBox.Show(ex.Message, "Error al iniciar el servicio", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub DetenerServicio()
            Try
                If _Servicio.CanStop Then
                    _Servicio.Stop()
                    MonitorearServicio()

                    NotificacionNotifyIcon.BalloonTipIcon = ToolTipIcon.Warning
                    NotificacionNotifyIcon.BalloonTipTitle = "MiharuReportesDDOService"
                    NotificacionNotifyIcon.BalloonTipText = "El servicio MiharuReportesDDOService ha sido desactivado"
                    NotificacionNotifyIcon.ShowBalloonTip(5000)
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Error al detener el servicio", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub ReiniciarServicio()
            EstadoStatusStripLabel.Text = "Reiniciando el servicio..."

            If _Servicio.Status = ServiceControllerStatus.Running Then
                Try
                    _Servicio.Stop()

                    NotificacionNotifyIcon.BalloonTipIcon = ToolTipIcon.Warning
                    NotificacionNotifyIcon.BalloonTipTitle = "MiharuReportesDDOService"
                    NotificacionNotifyIcon.BalloonTipText = "El servicio MiharuReportesDDOService ha sido desactivado"
                    NotificacionNotifyIcon.ShowBalloonTip(5000)

                    _Servicio.Refresh()
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Error al reiniciar el servicio", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If

            Dim Segundos As Byte = 0

            While _Servicio.Status = ServiceControllerStatus.StopPending And Segundos < 60
                Threading.Thread.Sleep(1000)

                Segundos = CByte(Segundos + 1)

                _Servicio.Refresh()
            End While

            If _Servicio.Status = ServiceControllerStatus.Stopped Then
                Try
                    _Servicio.Start()

                    NotificacionNotifyIcon.BalloonTipIcon = ToolTipIcon.Info
                    NotificacionNotifyIcon.BalloonTipTitle = "MiharuReportesDDOService"
                    NotificacionNotifyIcon.BalloonTipText = "El servicio MiharuReportesDDOService ha sido activado"
                    NotificacionNotifyIcon.ShowBalloonTip(5000)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Error al reiniciar el servicio", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If

            MonitorearServicio()

        End Sub

        Private Sub ProbarServicioWeb()
            If GuardarServicioButton.Enabled Then
                MessageBox.Show("Debe almacenar los cambios para poder realizar la prueba", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Else
                Me.Enabled = False
                Me.Cursor = Cursors.WaitCursor

                ValidarServicio()

                Me.Cursor = Cursors.Default
                Me.Enabled = True
            End If
        End Sub

        Public Sub ValidarServicio()
            Dim WebService As SecurityWebService

            Try
                Dim ConnectionStrings As ReportesConfig.TypeConnectionString

                WebService = New SecurityWebService(Program.Config.SecurityWebServiceURL, "")
                WebService.CrearCanalSeguro()
                WebService.setUser(Program.Config.User, ReportesConfig.Decrypt(Program.Config.Password))
                ConnectionStrings = ReportesConfig.getCadenasConexion(WebService)

                ' Security
                Dim dbmSecurity As DBSecurityDataBaseManager = Nothing
                Try
                    dbmSecurity = New DBSecurityDataBaseManager(ConnectionStrings.Security)
                    dbmSecurity.Connection_Open(2) ' Service
                Catch ex As Exception
                    Throw New Exception("No se pudo realizar la conexión a la base de datos Security", ex)
                Finally
                    If (dbmSecurity IsNot Nothing) Then dbmSecurity.Connection_Close()
                End Try

                Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
                Try
                    dbmCore = New DBCore.DBCoreDataBaseManager(ConnectionStrings.Core)
                    dbmCore.Connection_Open(2) ' Service
                Catch ex As Exception
                    Throw New Exception("No se pudo realizar la conexión a la base de datos Core", ex)
                Finally
                    If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                End Try

                Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
                Try
                    dbmImaging = New DBImaging.DBImagingDataBaseManager(ConnectionStrings.Imaging)
                    dbmImaging.Connection_Open(2) ' Service
                Catch ex As Exception
                    Throw New Exception("No se pudo realizar la conexión a la base de datos Core", ex)
                Finally
                    If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                End Try

                MessageBox.Show("Prueba exitosa", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Information)

            Catch ex As Exception
                MessageBox.Show("Prueba fallida: " & ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)

            End Try
        End Sub

#End Region

#Region " Funciones "

        Private Function Validar() As Boolean
            If IntervaloTextBox.Text = "" Then
                MessageBox.Show("Debe ingresar el intervalo de proceso", "Datos inválidos", MessageBoxButtons.OK, MessageBoxIcon.Error)
                tcBase.SelectedIndex = 2
                IntervaloTextBox.Focus()
            ElseIf Not IsNumeric(IntervaloTextBox.Text) Then
                MessageBox.Show("El intervalo debe ser un dato numérico", "Datos inválidos", MessageBoxButtons.OK, MessageBoxIcon.Error)
                tcBase.SelectedIndex = 2
                IntervaloTextBox.Focus()
                IntervaloTextBox.SelectAll()
            Else
                Return True
            End If

            Return False

        End Function

#End Region
    End Class
End Namespace

