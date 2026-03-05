Imports System.IO
Imports System.ServiceProcess
Imports Miharu.FileSending.Library.Clases
Imports DBImaging
Imports Miharu.Security.Library.WebService

Namespace Formularios

    Public Class MainForm

#Region " Declaraciones "

        Private _GuardadoServicio As Boolean = False


        Private _Servicio As ServiceController = Nothing
        Private _NotifiIcon_Activo As Icon
        Private _NotifiIcon_Inactivo As Icon

#End Region

#Region " Eventos "

        Private Sub MainForm_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            LoadConfig()
        End Sub

        Private Sub MainForm_FormClosing(ByVal sender As System.Object, ByVal e As FormClosingEventArgs) Handles MyBase.FormClosing
            If e.CloseReason = CloseReason.UserClosing Then
                AbrirToolStripMenuItem.Enabled = True
                e.Cancel = True
                Me.Visible = False
            End If
        End Sub

        Private Sub MainForm_FormClosed(ByVal sender As Object, ByVal e As FormClosedEventArgs) Handles Me.FormClosed
            Application.Exit()
        End Sub

        Private Sub SalirToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles SalirToolStripMenuItem.Click
            Application.Exit()
        End Sub

        Private Sub AcercaDeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AcercaDeToolStripMenuItem.Click
            Dim f As New AboutForm()

            f.ShowDialog()
        End Sub

        Private Sub AbrirToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AbrirToolStripMenuItem.Click
            AbrirToolStripMenuItem.Enabled = False
            Me.Show()
        End Sub

        Private Sub SalirContextualToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles SalirContextualToolStripMenuItem.Click
            Application.Exit()
        End Sub

        Private Sub TextBox_TextChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles AppNameTextBox.TextChanged, WorkingFolderTextBox.TextChanged, PuertoTextBox.TextChanged, UserTextBox.TextChanged, ServicioWebTextBox.TextChanged, PasswordTextBox.TextChanged, ServidorRemotingTextBox.TextChanged, PuertoRemotingTextBox.TextChanged, PassWordRemotingTextBox.TextChanged, AppNameRemotingTextBox.TextChanged, LogFolderTextBox.TextChanged, LogActivoCheckBox.CheckedChanged
            ActivarGuardar()
        End Sub

        Private Sub CheckBox_CheckedChanged(sender As Object, e As System.EventArgs) Handles DataRemotingCheckBox.CheckedChanged, CifradoCheckBox.CheckedChanged
            ActivarGuardar()

            If (DataRemotingCheckBox.Checked) Then

                RemotingPanel.Enabled = True
            Else
                RemotingPanel.Enabled = False
            End If
        End Sub

        Private Sub GuardarServicioButton_Click(sender As System.Object, e As System.EventArgs) Handles GuardarServicioButton.Click
            SaveServico()
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

        Private Sub tmrMonitorServicio_Tick(ByVal sender As System.Object, ByVal e As EventArgs) Handles MonitorServicioTimer.Tick
            MonitorearServicio()
        End Sub

        Private Sub NotificacionNotifyIcon_MouseDoubleClick(ByVal sender As System.Object, ByVal e As MouseEventArgs) Handles NotificacionNotifyIcon.MouseDoubleClick
            Me.Show()
            Me.WindowState = FormWindowState.Normal
            Me.Focus()
        End Sub

        Private Sub CargarPathButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CargarPathButton.Click
            WorkingFolderTextBox.Text = GetFolder(WorkingFolderTextBox.Text)
        End Sub

        Private Sub ProbarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ProbarButton.Click
            'Dim Servicio = New FileSendingService()
            'Servicio.IniciarServicio()
            ProbarServicio()
        End Sub

        Private Sub ProbarRemotingButton_Click(sender As System.Object, e As System.EventArgs) Handles ProbarRemotingButton.Click
            ProbarRemoting()
        End Sub

#End Region

#Region " Metodos "

        Private Sub LoadConfig()
            Try
                ' Cargar datos servicio
                If (File.Exists(Program.AppDataPath + FileSendingConfig.ConfigFileName)) Then
                    Program.Config = FileSendingConfig.Deserialize(Program.AppDataPath)
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

            ServicioWebTextBox.Text = Program.Config.SecurityWebServiceURL
            UserTextBox.Text = Program.Config.SecurityWebServiceUser
            PasswordTextBox.Text = FileSendingConfig.Decrypt(Program.Config.SecurityWebServicePassword)
            AppNameTextBox.Text = Program.Config.AppName
            IdentifierDateFormatTextBox.Text = Program.Config.IdentifierDateFormat
            WorkingFolderTextBox.Text = Program.Config.WorkingFolder
            PuertoTextBox.Text = CStr(Program.Config.Puerto)
            DataRemotingCheckBox.Checked = Program.Config.UsaRemoting

            ServidorRemotingTextBox.Text = Program.Config.RemotingIPName
            PuertoRemotingTextBox.Text = CStr(Program.Config.RemotingPuerto)
            AppNameRemotingTextBox.Text = Program.Config.RemotingAppName
            PassWordRemotingTextBox.Text = FileSendingConfig.Decrypt(Program.Config.RemotingPassword)
            CifradoCheckBox.Checked = Program.Config.RemotingCifrado
            LogFolderTextBox.Text = Program.Config.LogFolder
            LogActivoCheckBox.Checked = CBool(Program.Config.LogActivo)

            ActivarGuardar()

            MonitorearServicio()
        End Sub

        Private Sub SaveServico()
            If Validar() Then
                Try
                    Program.Config.SecurityWebServiceURL = ServicioWebTextBox.Text
                    Program.Config.SecurityWebServiceUser = UserTextBox.Text
                    Program.Config.SecurityWebServicePassword = FileSendingConfig.Encrypt(PasswordTextBox.Text)
                    Program.Config.AppName = AppNameTextBox.Text
                    Program.Config.IdentifierDateFormat = IdentifierDateFormatTextBox.Text
                    Program.Config.WorkingFolder = WorkingFolderTextBox.Text
                    Program.Config.Puerto = CInt(PuertoTextBox.Text)
                    Program.Config.UsaRemoting = DataRemotingCheckBox.Checked
                    Program.Config.RemotingIPName = ServidorRemotingTextBox.Text
                    Program.Config.RemotingPuerto = CInt(PuertoRemotingTextBox.Text)
                    Program.Config.RemotingPassword = FileSendingConfig.Encrypt(PassWordRemotingTextBox.Text)
                    Program.Config.RemotingAppName = AppNameRemotingTextBox.Text
                    Program.Config.RemotingCifrado = CifradoCheckBox.Checked
                    Program.Config.LogFolder = LogFolderTextBox.Text
                    Program.Config.LogActivo = CBool(LogActivoCheckBox.Checked)

                    FileSendingConfig.Serialize(Program.Config, Program.AppDataPath)



                    _GuardadoServicio = True

                    MessageBox.Show("Los datos se almacenaron correctamente, para que los cambios tengan efecto debe reiniciar el servicio", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If

            ActivarGuardar()
        End Sub

        Private Sub ActivarGuardar()
            ' Servicio
            Dim HayCambios As Boolean

            HayCambios = (Program.Config.SecurityWebServiceURL <> ServicioWebTextBox.Text) Or
                         (FileSendingConfig.Decrypt(Program.Config.SecurityWebServicePassword) <> PasswordTextBox.Text) Or
                         (Program.Config.AppName <> AppNameTextBox.Text) Or
                         (Program.Config.IdentifierDateFormat <> IdentifierDateFormatTextBox.Text) Or
                         (Program.Config.WorkingFolder <> WorkingFolderTextBox.Text) Or
                         (Program.Config.Puerto.ToString() <> PuertoTextBox.Text) Or
                         (Program.Config.UsaRemoting <> DataRemotingCheckBox.Checked) Or
                         (Program.Config.RemotingIPName <> ServidorRemotingTextBox.Text) Or
                         (FileSendingConfig.Decrypt(Program.Config.RemotingPassword) <> PassWordRemotingTextBox.Text) Or
                         (Program.Config.RemotingAppName <> AppNameRemotingTextBox.Text) Or
                         (Program.Config.RemotingPuerto.ToString() <> PuertoRemotingTextBox.Text) Or
                         (Program.Config.RemotingCifrado <> CifradoCheckBox.Checked) Or (Program.Config.LogFolder <> LogFolderTextBox.Text) Or (Program.Config.LogActivo <> CBool(LogActivoCheckBox.Checked))

            GuardarServicioButton.Enabled = Not _GuardadoServicio Or HayCambios
        End Sub

        Private Sub MonitorearServicio()
            MonitorServicioTimer.Enabled = False

            Try

                Dim Servicios() As ServiceController

                Servicios = ServiceController.GetServices()
                _Servicio = Nothing

                For Each ServicioEncontrado As ServiceController In Servicios
                    If ServicioEncontrado.ServiceName = "MiharuFileSendingDDOService" Then
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
                            NotificacionNotifyIcon.Text = "SLYG FileSending [Activo]"

                        Case Else
                            EstadoStatusStripLabel.Text = "Servicio detenido"
                            NotificacionNotifyIcon.Icon = _NotifiIcon_Inactivo
                            NotificacionNotifyIcon.Text = "SLYG FileSending [Inactivo]"

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
                NotificacionNotifyIcon.BalloonTipTitle = "FileSending"
                NotificacionNotifyIcon.BalloonTipText = "El servicio FileSending ha sido activado"
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
                    NotificacionNotifyIcon.BalloonTipTitle = "FileSending"
                    NotificacionNotifyIcon.BalloonTipText = "El servicio FileSending ha sido desactivado"
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
                    NotificacionNotifyIcon.BalloonTipTitle = "FileSending"
                    NotificacionNotifyIcon.BalloonTipText = "El servicio FileSending ha sido desactivado"
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
                    NotificacionNotifyIcon.BalloonTipTitle = "FileSending"
                    NotificacionNotifyIcon.BalloonTipText = "El servicio FileSending ha sido activado"
                    NotificacionNotifyIcon.ShowBalloonTip(5000)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Error al reiniciar el servicio", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If

            MonitorearServicio()

        End Sub

        Private Sub ProbarServicio()
            If GuardarServicioButton.Enabled Then
                MessageBox.Show("Recuerde almacenar los cambios para poder realizar la prueba", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Else
                Me.Enabled = False
                Me.Cursor = Cursors.WaitCursor

                If (DataRemotingCheckBox.Checked) Then
                    ValidarConexionRemoting()
                Else
                    ValidarServicio()
                End If

                Me.Cursor = Cursors.Default
                Me.Enabled = True
            End If
        End Sub

        Public Sub ValidarServicio()
            Dim WebService As SecurityWebService

            Try
                WebService = New SecurityWebService(Program.Config.SecurityWebServiceURL, "")
                WebService.CrearCanalSeguro()
                WebService.setUser(Program.Config.SecurityWebServiceUser, FileSendingConfig.Decrypt(Program.Config.SecurityWebServicePassword))
                Dim ConnectionStrings = FileSendingConfig.getCadenasConexion(WebService)

                Dim dbmImaging As New DBImagingDataBaseManager(ConnectionStrings.Imaging)

                Try
                    DBImagingDataBaseManager.IdentifierDateFormat = IdentifierDateFormatTextBox.Text
                    dbmImaging.Connection_Open(1)
                Catch ex As Exception
                    Throw New Exception("No se pudo realizar la conexión a la base de datos Imaging", ex)
                Finally
                    dbmImaging.Connection_Close()
                End Try

                Dim dbmCore As New DBCore.DBCoreDataBaseManager(ConnectionStrings.Core)

                Try
                    dbmCore.Connection_Open(1)
                Catch ex As Exception
                    Throw New Exception("No se pudo realizar la conexión a la base de datos Core", ex)
                Finally
                    dbmCore.Connection_Close()
                End Try

                MessageBox.Show("Prueba exitosa", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)

            Catch ex As Exception
                MessageBox.Show("Prueba fallida: " & ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)

            End Try
        End Sub

        Public Sub ValidarConexionRemoting()
            Dim WebService As SecurityWebService

            Try
                WebService = New SecurityWebService(Program.Config.SecurityWebServiceURL, "")
                WebService.CrearCanalSeguro()
                WebService.setUser(Program.Config.SecurityWebServiceUser, FileSendingConfig.Decrypt(Program.Config.SecurityWebServicePassword))
                Dim ConnectionStrings = FileSendingConfig.getCadenasConexion(WebService)

                Dim DataRemoting = ";RemotingUrl=tcp://" & Program.Config.RemotingIPName & ":" & Program.Config.RemotingPuerto & "/" & Program.Config.RemotingAppName & ";RemotingPwd=" & FileSendingConfig.Decrypt(Program.Config.RemotingPassword) & ";RemotingTrusted=" & Program.Config.RemotingCifrado
                Dim Imaging = ConnectionStrings.Imaging & DataRemoting
                Dim dbmImaging As New DBImagingDataBaseManager(ConnectionStrings.Imaging & DataRemoting)

                Try
                    dbmImaging.Connection_Open(1)
                Catch ex As Exception
                    Throw New Exception("No se pudo realizar la conexión a la base de datos Imaging", ex)
                Finally
                    dbmImaging.Connection_Close()
                End Try

                Dim dbmCore As New DBCore.DBCoreDataBaseManager(ConnectionStrings.Core & DataRemoting)

                Try
                    dbmCore.Connection_Open(1)
                Catch ex As Exception
                    Throw New Exception("No se pudo realizar la conexión a la base de datos Core", ex)
                Finally
                    dbmCore.Connection_Close()
                End Try

                Dim dbmTools As New DBTools.DBToolsDataBaseManager(ConnectionStrings.Tools & DataRemoting)

                Try
                    dbmTools.Connection_Open()
                Catch ex As Exception
                    Throw New Exception("No se pudo realizar la conexión a la base de datos Tools", ex)
                Finally
                    dbmTools.Connection_Close()
                End Try

                Dim dbmIntegration As New DBIntegration.DBIntegrationDataBaseManager(ConnectionStrings.Integration & DataRemoting)
                Try
                    dbmIntegration.Connection_Open(1)
                Catch ex As Exception
                    Throw New Exception("No se pudo realizar la conexión a la base de datos Integration", ex)
                Finally
                    dbmIntegration.Connection_Close()
                End Try

                MessageBox.Show("Prueba Data Remoting exitosa", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)

            Catch ex As Exception
                MessageBox.Show("Prueba Data Remoting fallida: " & ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)

            End Try
        End Sub

        Private Sub ProbarRemoting()

            If GuardarServicioButton.Enabled Then
                MessageBox.Show("Recuerde almacenar los cambios para poder realizar la prueba", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Else
                Me.Enabled = False
                Me.Cursor = Cursors.WaitCursor

                ValidarConexionRemoting()

                Me.Cursor = Cursors.Default
                Me.Enabled = True
            End If
        End Sub

#End Region

#Region " Funciones "

        Private Function Validar() As Boolean
            If ServicioWebTextBox.Text = "" Then
                MessageBox.Show("Debe ingresar la URL del servicio Web de seguridad", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                ServicioWebTextBox.Focus()

            ElseIf AppNameTextBox.Text = "" Then
                MessageBox.Show("Debe ingresar el AppName", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                AppNameTextBox.Focus()

            ElseIf IdentifierDateFormatTextBox.Text = "" Then
                MessageBox.Show("Debe ingresar el Formato de fecha a usar", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                IdentifierDateFormatTextBox.Focus()

            ElseIf WorkingFolderTextBox.Text = "" Then
                MessageBox.Show("Debe ingresar el directorio de trabajo", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                WorkingFolderTextBox.Focus()

            ElseIf Not Directory.Exists(WorkingFolderTextBox.Text) Then
                MessageBox.Show("El directorio de trabajo debe ser una ruta válida", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                WorkingFolderTextBox.Focus()

            ElseIf PuertoTextBox.Text = "" Then
                MessageBox.Show("Debe ingresar el Puerto", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                PuertoTextBox.Focus()

            ElseIf Not IsNumeric(PuertoTextBox.Text) Then
                MessageBox.Show("El Puerto debe ser un dato numérico entre 100 y 64.000", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                PuertoTextBox.Focus()
                PuertoTextBox.SelectAll()

            ElseIf CInt(PuertoTextBox.Text) < 100 Or CInt(PuertoTextBox.Text) > 64000 Then
                MessageBox.Show("El Puerto debe ser un dato numérico entre 100 y 64.000", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                PuertoTextBox.Focus()
                PuertoTextBox.SelectAll()

            ElseIf (DataRemotingCheckBox.Checked) Then
                If ServidorRemotingTextBox.Text = "" Then
                    MessageBox.Show("Debe ingresar la IP o nombre del servidor", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    ServidorRemotingTextBox.Focus()

                ElseIf AppNameRemotingTextBox.Text = "" Then
                    MessageBox.Show("Debe ingresar el AppName de DataRemoting", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    AppNameRemotingTextBox.Focus()

                ElseIf PuertoRemotingTextBox.Text = "" Then
                    MessageBox.Show("Debe ingresar el Puerto", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    PuertoRemotingTextBox.Focus()

                ElseIf Not IsNumeric(PuertoRemotingTextBox.Text) Then
                    MessageBox.Show("El Puerto debe ser un dato numérico entre 100 y 64.000", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    PuertoRemotingTextBox.Focus()
                    PuertoRemotingTextBox.SelectAll()

                ElseIf CInt(PuertoRemotingTextBox.Text) < 100 Or CInt(PuertoRemotingTextBox.Text) > 64000 Then
                    MessageBox.Show("El Puerto debe ser un dato numérico entre 100 y 64.000", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    PuertoRemotingTextBox.Focus()
                    PuertoRemotingTextBox.SelectAll()

                ElseIf (LogActivoCheckBox.Checked) Then
                    If Me.LogFolderTextBox.Text = "" Then
                        MessageBox.Show("Debe ingresar la carpeta de logs", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Me.LogFolderTextBox.Focus()
                    ElseIf Not Directory.Exists(Me.LogFolderTextBox.Text) Then
                        MessageBox.Show("La carpeta de logs debe ser una ruta válida", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Me.LogFolderTextBox.Focus()
                    Else
                        Return True
                    End If
                Else
                    Return True
                End If
            Else
                Return True
            End If

            Return False

        End Function

        Private Function GetFolder(ByVal nPath As String) As String
            Dim Selector As New FolderBrowserDialog

            Selector.SelectedPath = nPath
            Selector.ShowNewFolderButton = False
            Selector.Description = "Seleccione la carpeta"

            Dim Respuesta = Selector.ShowDialog

            If Respuesta = DialogResult.OK Then
                Return Selector.SelectedPath
            Else
                Return nPath
            End If
        End Function

#End Region

      
        
    End Class

End Namespace