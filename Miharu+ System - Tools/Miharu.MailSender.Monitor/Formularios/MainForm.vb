Imports System.IO
Imports System.ServiceProcess
Imports Microsoft.Win32
Imports System.Data
Imports System.Text.RegularExpressions
Imports Miharu.MailSender
Imports Miharu.Security.Library
Imports Miharu.Security.Library.WebService
Imports DBTools

Public Class MainForm

#Region " Declaraciones "

    Private _GuardadoServicio As Boolean = False
    Private _Servicio As ServiceController = Nothing
    Private _NotifiIcon_Activo As Icon
    Private _NotifiIcon_Inactivo As Icon
    Private _Cargando As Boolean = False

#End Region

#Region " Eventos "

    Private Sub MainForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadConfig()
    End Sub
    Private Sub MainForm_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If e.CloseReason = CloseReason.UserClosing Then
            mnuiAbrir.Enabled = True
            e.Cancel = True
            Me.Visible = False
        End If
    End Sub

    Private Sub mnuSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSalir.Click
        Application.Exit()
    End Sub
    Private Sub mnuAcercaDe_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAcercaDe.Click
        Dim f As New AboutForm

        f.ShowDialog()
    End Sub
    Private Sub mnuiAbrir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuiAbrir.Click
        mnuiAbrir.Enabled = False
        Me.Show()
    End Sub
    Private Sub mnuiSalir2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuiSalir2.Click
        Application.Exit()
    End Sub

    Private Sub txtFromMailAddress_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFromMailAddress.TextChanged
        ActivarGuardar()
    End Sub
    Private Sub txtFromMailDisplay_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFromMailDisplay.TextChanged
        ActivarGuardar()
    End Sub
    Private Sub txtSMTPServerAddress_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSMTPServerAddress.TextChanged
        ActivarGuardar()
    End Sub
    Private Sub txtSMTPServerPort_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSMTPServerPort.TextChanged
        ActivarGuardar()
    End Sub
    Private Sub txtMailUser_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMailUser.TextChanged
        ActivarGuardar()
    End Sub
    Private Sub txtMailUserPassword_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMailUserPassword.TextChanged
        ActivarGuardar()
    End Sub

    Private Sub txtIntervalo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtIntervalo.TextChanged
        ActivarGuardar()
    End Sub
    Private Sub txttxtServicioWeb_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtServicioWeb.TextChanged
        ActivarGuardar()
    End Sub
    Private Sub txtUsuario_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtUsuario.TextChanged
        ActivarGuardar()
    End Sub
    Private Sub txtContraseña_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtContraseña.TextChanged
        ActivarGuardar()
    End Sub

    Private Sub chkEnabledSSL_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEnabledSSL.CheckedChanged
        ActivarGuardar()
    End Sub
    Private Sub chkWinNTSecurity_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ActivarGuardar()
    End Sub

    Private Sub btnIniciar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnIniciar.Click
        IniciarServicio()
    End Sub
    Private Sub btnDetener_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDetener.Click
        DetenerServicio()
    End Sub
    Private Sub btnReiniciar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReiniciar.Click
        ReiniciarServicio()
    End Sub

    Private Sub btnGuardarServicio_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardarServicio.Click
        SaveServico()
    End Sub

    Private Sub tmrMonitorServicio_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrMonitorServicio.Tick
        MonitorearServicio()
    End Sub

    Private Sub IconoNotificacion_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles IconoNotificacion.MouseDoubleClick
        Me.Show()
        Me.WindowState = FormWindowState.Normal
        Me.Focus()
    End Sub

    Private Sub chkAutoIniciar_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAutoIniciar.CheckedChanged
        If chkAutoIniciar.Checked Then
            ponerEnInicio(Program.AssemblyName, Program.AppName)
        Else
            quitarDeInicio(Program.AssemblyName)
        End If
    End Sub

    Private Sub btnEmailPrueba_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEmailPrueba.Click
        EmailPrueba()
    End Sub

    Private Sub btnProbar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProbar.Click
        ProbarServicioWeb()
    End Sub

#End Region

#Region " Metodos "

    Private Sub LoadConfig()
        _Cargando = True

        ' Iconos de notificación
        _NotifiIcon_Activo = Icon.FromHandle((CType(imglIconos.Images(0), Bitmap)).GetHicon())
        _NotifiIcon_Inactivo = Icon.FromHandle((CType(imglIconos.Images(1), Bitmap)).GetHicon())

        ' Cargar datos servicio
        If (File.Exists(Program.AppPath + Program.ConfigFileName)) Then
            _GuardadoServicio = Library.MailSenderConfig.Deserialize(Program.Config, Program.AppPath + Program.ConfigFileName)
        Else
            _GuardadoServicio = False
        End If

        ' Mail
        txtFromMailAddress.Text = Program.Config.FromMailAddress
        txtFromMailDisplay.Text = Program.Config.FromMailDisplay
        txtSMTPServerAddress.Text = Program.Config.SMTPServerAddress
        txtSMTPServerPort.Text = CStr(Program.Config.SMTPServerPort)
        txtMailUser.Text = Program.Config.MailUser
        txtMailUserPassword.Text = Program.Config.MailUserPassword
        chkEnabledSSL.Checked = Program.Config.EnabledSSL

        ' Servicio
        txtIntervalo.Text = CStr(Program.Config.Intervalo)
        txtServicioWeb.Text = Program.Config.SecurityWebServiceURL
        txtUsuario.Text = Program.Config.User
        txtContraseña.Text = Library.MailSenderConfig.Decrypt(Program.Config.Password)

        ' Remoting
        ServidorRemotingTextBox.Text = Program.Config.RemotingIPName
        PuertoRemotingTextBox.Text = CStr(Program.Config.RemotingPuerto)
        AppNameRemotingTextBox.Text = Program.Config.RemotingAppName
        PassWordRemotingTextBox.Text = Library.MailSenderConfig.Decrypt(Program.Config.RemotingPassword)
        CifradoCheckBox.Checked = Program.Config.RemotingCifrado

        _Cargando = False

        ActivarGuardar()
        MonitorearServicio()
    End Sub
    Private Sub SaveServico()
        If Validar() Then
            ' Mail
            Program.Config.FromMailAddress = txtFromMailAddress.Text
            Program.Config.FromMailDisplay = txtFromMailDisplay.Text
            Program.Config.SMTPServerAddress = txtSMTPServerAddress.Text
            Program.Config.SMTPServerPort = CInt(txtSMTPServerPort.Text)
            Program.Config.MailUser = txtMailUser.Text
            Program.Config.MailUserPassword = txtMailUserPassword.Text
            Program.Config.EnabledSSL = chkEnabledSSL.Checked

            ' Servicio
            Program.Config.Intervalo = CInt(txtIntervalo.Text)
            Program.Config.SecurityWebServiceURL = txtServicioWeb.Text
            Program.Config.User = txtUsuario.Text
            Program.Config.Password = Library.MailSenderConfig.Encrypt(txtContraseña.Text)

            ' Remoting
            Program.Config.UsaRemoting = DataRemotingCheckBox.Checked
            Program.Config.RemotingIPName = ServidorRemotingTextBox.Text
            Program.Config.RemotingPuerto = CInt(PuertoRemotingTextBox.Text)
            Program.Config.RemotingPassword = Library.MailSenderConfig.Encrypt(PassWordRemotingTextBox.Text)
            Program.Config.RemotingAppName = AppNameRemotingTextBox.Text
            Program.Config.RemotingCifrado = CifradoCheckBox.Checked

            _GuardadoServicio = Library.MailSenderConfig.Serialize(Program.Config, Program.AppPath + Program.ConfigFileName)

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

            HayCambios = (Program.Config.FromMailAddress <> txtFromMailAddress.Text) Or _
                        (Program.Config.FromMailDisplay <> txtFromMailDisplay.Text) Or _
                        (Program.Config.SMTPServerAddress <> txtSMTPServerAddress.Text) Or _
                        (Program.Config.SMTPServerPort <> CInt(IIf(txtSMTPServerPort.Text <> "", txtSMTPServerPort.Text, 0))) Or _
                        (Program.Config.MailUser <> txtMailUser.Text) Or _
                        (Program.Config.MailUserPassword <> txtMailUserPassword.Text) Or _
                        (Program.Config.EnabledSSL <> chkEnabledSSL.Checked) Or _
                        (Program.Config.SecurityWebServiceURL <> txtServicioWeb.Text) Or _
                        (Library.MailSenderConfig.Decrypt(Program.Config.Password) <> txtContraseña.Text) Or _
                        (Program.Config.Intervalo <> CInt(IIf(txtIntervalo.Text <> "", txtIntervalo.Text, 0))) Or _
                        (Program.Config.UsaRemoting <> DataRemotingCheckBox.Checked) Or _
                        (Program.Config.RemotingIPName <> ServidorRemotingTextBox.Text) Or _
                        (Library.MailSenderConfig.Decrypt(Program.Config.RemotingPassword) <> PassWordRemotingTextBox.Text) Or _
                        (Program.Config.RemotingAppName <> AppNameRemotingTextBox.Text) Or _
                        (Program.Config.RemotingPuerto.ToString() <> PuertoRemotingTextBox.Text) Or _
                        (Program.Config.RemotingCifrado <> CifradoCheckBox.Checked)

            btnGuardarServicio.Enabled = Not _GuardadoServicio Or HayCambios
        End If
    End Sub

    Private Sub MonitorearServicio()
        tmrMonitorServicio.Enabled = False

        Try

            Dim Servicios() As ServiceController

            Servicios = ServiceController.GetServices()
            _Servicio = Nothing

            For Each ServicioEncontrado As ServiceController In Servicios
                If ServicioEncontrado.ServiceName = "MiharuMailSenderDDOService" Then
                    _Servicio = ServicioEncontrado
                End If
            Next

            If _Servicio Is Nothing Then
                tsslEstado.Text = "El servicio no se encuentra instalado"

                btnIniciar.Enabled = False
                btnDetener.Enabled = False
                btnReiniciar.Enabled = False
            Else
                _Servicio.Refresh()

                If _Servicio.CanStop Then
                    btnDetener.Enabled = (_Servicio.Status = ServiceControllerStatus.Running)
                Else
                    btnDetener.Enabled = False
                End If

                btnIniciar.Enabled = (_Servicio.Status = ServiceControllerStatus.Stopped)
                btnReiniciar.Enabled = btnDetener.Enabled

                Select Case _Servicio.Status
                    Case ServiceControllerStatus.Running
                        tsslEstado.Text = "Servicio activo"
                        IconoNotificacion.Icon = _NotifiIcon_Activo
                        IconoNotificacion.Text = "SLYG MailSender [Activo]"

                    Case Else
                        tsslEstado.Text = "Servicio detenido"
                        IconoNotificacion.Icon = _NotifiIcon_Inactivo
                        IconoNotificacion.Text = "SLYG MailSender [Inactivo]"

                End Select

            End If

        Catch
            btnIniciar.Enabled = False
            btnDetener.Enabled = False
            btnReiniciar.Enabled = False

            tsslEstado.Text = "Error al validar el servicio"
        End Try

        tmrMonitorServicio.Enabled = True
    End Sub

    Private Sub IniciarServicio()
        Try
            _Servicio.Start()
            MonitorearServicio()

            IconoNotificacion.BalloonTipIcon = ToolTipIcon.Info
            IconoNotificacion.BalloonTipTitle = "MailSender"
            IconoNotificacion.BalloonTipText = "El servicio MailSender ha sido activado"
            IconoNotificacion.ShowBalloonTip(5000)

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error al iniciar el servicio", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub DetenerServicio()
        Try
            If _Servicio.CanStop Then
                _Servicio.Stop()
                MonitorearServicio()

                IconoNotificacion.BalloonTipIcon = ToolTipIcon.Warning
                IconoNotificacion.BalloonTipTitle = "MailSender"
                IconoNotificacion.BalloonTipText = "El servicio MailSender ha sido desactivado"
                IconoNotificacion.ShowBalloonTip(5000)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error al detener el servicio", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub ReiniciarServicio()
        tsslEstado.Text = "Reiniciando el servicio..."

        If _Servicio.Status = ServiceControllerStatus.Running Then
            Try
                _Servicio.Stop()

                IconoNotificacion.BalloonTipIcon = ToolTipIcon.Warning
                IconoNotificacion.BalloonTipTitle = "MailSender"
                IconoNotificacion.BalloonTipText = "El servicio MailSender ha sido desactivado"
                IconoNotificacion.ShowBalloonTip(5000)

                _Servicio.Refresh()
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Error al reiniciar el servicio", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If

        Dim Segundos As Byte = 0

        While _Servicio.Status = ServiceControllerStatus.StopPending And Segundos < 60
            System.Threading.Thread.Sleep(1000)

            Segundos = CByte(Segundos + 1)

            _Servicio.Refresh()
        End While

        If _Servicio.Status = ServiceControllerStatus.Stopped Then
            Try
                _Servicio.Start()

                IconoNotificacion.BalloonTipIcon = ToolTipIcon.Info
                IconoNotificacion.BalloonTipTitle = "MailSender"
                IconoNotificacion.BalloonTipText = "El servicio MailSender ha sido activado"
                IconoNotificacion.ShowBalloonTip(5000)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Error al reiniciar el servicio", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If

        MonitorearServicio()

    End Sub

    Private Sub EmailPrueba()
        If Validar() Then
            Dim EmailAddress As String

            EmailAddress = InputBox("Ingrese la dirección de correo a la que se enviará el e-mail de prueba", "Prueba", txtFromMailAddress.Text, , )

            If IsMail(EmailAddress) Then
                Me.Enabled = False

                Try
                    Dim MailManager As New MailSender.Library.MailSenderManager()

                    MailManager.SMTPServerAddress = txtSMTPServerAddress.Text
                    MailManager.Port = CInt(txtSMTPServerPort.Text)
                    MailManager.FromMailAddress = txtFromMailAddress.Text
                    MailManager.FromMailDisplay = txtFromMailDisplay.Text
                    MailManager.User = txtMailUser.Text
                    MailManager.Password = txtMailUserPassword.Text
                    MailManager.EnabledSSL = chkEnabledSSL.Checked

                    Application.DoEvents()

                    MailManager.SendMail("", "", EmailAddress, "", "", "Miharu.MailSender: e-mail de prueba", "Este es un correo de prueba", Nothing)

                    MessageBox.Show("El correo se envió exitosamente, por favor valide que halla llegado al destinatario", "Prueba", MessageBoxButtons.OK, MessageBoxIcon.Information)

                Catch ex As Exception
                    MessageBox.Show("Error a intentar enviar el correo: " & ex.Message, "Prueba", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try

                Me.Enabled = True
            Else
                MessageBox.Show("La cuenta de correo ingresada no es una dirección de e-mail válida", "Datos inválidos", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
    End Sub

    Private Sub ProbarServicioWeb()
        If btnGuardarServicio.Enabled Then
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
            Dim ConnectionStrings As Library.MailSenderConfig.TypeConnectionString

            WebService = New SecurityWebService(Program.Config.SecurityWebServiceURL, "")
            WebService.CrearCanalSeguro()
            WebService.setUser(Program.Config.User, Library.MailSenderConfig.Decrypt(Program.Config.Password))
            ConnectionStrings = Library.MailSenderConfig.getCadenasConexion(WebService)

            Dim SqlCnn As DBToolsDataBaseManager = Nothing

            Try
                SqlCnn = New DBToolsDataBaseManager(ConnectionStrings.Tools)

                SqlCnn.Connection_Open()
            Catch ex As Exception
                Throw New Exception("No se pudo realizar la conexión a la base de datos Tools. " & ex.Message)
            Finally
                If (SqlCnn IsNot Nothing) Then SqlCnn.Connection_Close()
            End Try

            MessageBox.Show("Prueba exitosa", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show("Prueba fallida: " & ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Sub ProbarRemoting()

        If btnGuardarServicio.Enabled Then
            MessageBox.Show("Debe almacenar los cambios para poder realizar la prueba", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            Me.Enabled = False
            Me.Cursor = Cursors.WaitCursor

            ValidarConexionRemoting()

            Me.Cursor = Cursors.Default
            Me.Enabled = True
        End If
    End Sub

    Public Sub ValidarConexionRemoting()
        Dim WebService As SecurityWebService

        Try
            Dim ConnectionStrings As Library.MailSenderConfig.TypeConnectionString

            WebService = New SecurityWebService(Program.Config.SecurityWebServiceURL, "")
            WebService.CrearCanalSeguro()
            WebService.setUser(Program.Config.User, Library.MailSenderConfig.Decrypt(Program.Config.Password))
            ConnectionStrings = Library.MailSenderConfig.getCadenasConexion(WebService)

            Dim DataRemoting = ";RemotingUrl=tcp://" & Program.Config.RemotingIPName & ":" & Program.Config.RemotingPuerto & "/" & Program.Config.RemotingAppName & ";RemotingPwd=" & Library.MailSenderConfig.Decrypt(Program.Config.RemotingPassword) & ";RemotingTrusted=" & Program.Config.RemotingCifrado
            Dim Tools = ConnectionStrings.Tools & DataRemoting
            Dim dbmTools As New DBTools.DBToolsDataBaseManager(Tools)

            Try
                dbmTools.Connection_Open()
            Catch ex As Exception
                Throw New Exception("No se pudo realizar la conexión a la base de datos Tools", ex)
            Finally
                dbmTools.Connection_Close()
            End Try

            Dim dbmCore As New DBCore.DBCoreDataBaseManager(ConnectionStrings.Core & DataRemoting)

            Try
                dbmCore.Connection_Open(1)
            Catch ex As Exception
                Throw New Exception("No se pudo realizar la conexión a la base de datos Core", ex)
            Finally
                dbmCore.Connection_Close()
            End Try

            MessageBox.Show("Prueba Data Remoting exitosa", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show("Prueba Data Remoting fallida: " & ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

#End Region

#Region " Funciones "

    Private Function Validar() As Boolean
        If Not IsMail(txtFromMailAddress.Text) Then
            MessageBox.Show("La cuenta de correo no es una dirección de e-mail válida", "Datos inválidos", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtFromMailAddress.Focus()
        ElseIf txtSMTPServerPort.Text = "" Then
            MessageBox.Show("Debe ingresar el Puerto", "Datos inválidos", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtSMTPServerPort.Focus()
        ElseIf Not IsNumeric(txtSMTPServerPort.Text) Then
            MessageBox.Show("El Puerto debe ser un dato numérico entre 0 y 64.000", "Datos inválidos", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtSMTPServerPort.Focus()
            txtSMTPServerPort.SelectAll()
        ElseIf CInt(txtSMTPServerPort.Text) < 0 Or CInt(txtSMTPServerPort.Text) > 64000 Then
            MessageBox.Show("El Puerto debe ser un dato numérico entre 0 y 64.000", "Datos inválidos", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtSMTPServerPort.Focus()
            txtSMTPServerPort.SelectAll()
        ElseIf txtIntervalo.Text = "" Then
            MessageBox.Show("Debe ingresar el intervalo de proceso", "Datos inválidos", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tcBase.SelectedIndex = 2
            txtIntervalo.Focus()
        ElseIf Not IsNumeric(txtIntervalo.Text) Then
            MessageBox.Show("El intervalo debe ser un dato numérico", "Datos inválidos", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tcBase.SelectedIndex = 2
            txtIntervalo.Focus()
            txtIntervalo.SelectAll()

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
            Else
                Return True
            End If
        Else
            Return True
        End If

        Return False

    End Function

    Public Function IsMail(ByVal nEmail As String) As Boolean
        If nEmail = "" Then Return False

        Dim l_reg As Regex = New Regex("^(([^<;>;()[\]\\.,;:\s@\""]+(\.[^<;>;()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$")

        Return l_reg.IsMatch(nEmail)
    End Function

    Private Function ponerEnInicio(ByVal nombreClave As String, ByVal nombreApp As String) As Boolean
        Try
            Dim runK As RegistryKey = Registry.LocalMachine.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Run", True)
            ' añadirlo al registro
            ' Si el path contiene espacios se debería incluir entre comillas dobles
            If Not nombreApp.StartsWith(ChrW(34)) AndAlso nombreApp.IndexOf(" ") > -1 Then
                nombreApp = ChrW(34) & nombreApp & ChrW(34)
            End If
            runK.SetValue(nombreClave, nombreApp)
            Return True
        Catch ex As Exception
            MessageBox.Show("ERROR al guardar en el registro. Seguramente no tienes privilegios suficientes. " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function
    Private Function quitarDeInicio(ByVal nombreClave As String) As Boolean
        Try
            Dim runK As RegistryKey = Registry.LocalMachine.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Run", True)
            ' quitar la clave indicada del registo
            runK.DeleteValue(nombreClave, False)
            Return True
        Catch ex As Exception
            MessageBox.Show("ERROR al eliminar la clave del registro. Seguramente no tienes privilegios suficientes. " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
        ''
    End Function
    Private Function comprobarEnInicio(ByVal nombreClave As String) As Boolean
        Try
            Dim runK As RegistryKey = Registry.LocalMachine.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Run", False)
            Dim Valor As String

            ' comprobar si está
            Valor = runK.GetValue(nombreClave, "").ToString

            Return (Valor <> "")
        Catch ex As Exception
            MessageBox.Show("ERROR al leer el valor de la clave del registro.. Seguramente no tienes privilegios suficientes. " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

            Return False
        End Try
    End Function

#End Region

    Private Sub ProbarRemotingButton_Click(sender As System.Object, e As System.EventArgs)
        ProbarRemoting()
    End Sub

    Private Sub CargarPathButton_Click(sender As System.Object, e As System.EventArgs)

    End Sub
    Private Sub TextBox_TextChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles ServidorRemotingTextBox.TextChanged, PuertoRemotingTextBox.TextChanged, PassWordRemotingTextBox.TextChanged, AppNameRemotingTextBox.TextChanged
        ActivarGuardar()
    End Sub
    Private Sub ProbarButton_Click(sender As System.Object, e As System.EventArgs)

    End Sub
    Private Sub CheckBox_CheckedChanged(sender As System.Object, e As System.EventArgs)

    End Sub


    Private Sub DataRemotingCheckBox_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles DataRemotingCheckBox.CheckedChanged
        ActivarGuardar()

        If (DataRemotingCheckBox.Checked) Then

            RemotingPanel.Enabled = True
        Else
            RemotingPanel.Enabled = False
        End If
    End Sub

    Private Sub ProbarRemotingButton_Click_1(sender As System.Object, e As System.EventArgs) Handles ProbarRemotingButton.Click
        ProbarRemoting()
    End Sub
End Class