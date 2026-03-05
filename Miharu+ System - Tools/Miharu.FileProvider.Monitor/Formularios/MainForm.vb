Imports System.IO
Imports System.ServiceProcess
Imports Microsoft.Win32

Public Class MainForm

#Region " Declaraciones "

    Private _GuardadoServicio As Boolean = False
    Private _GuardadoPath As Boolean = False
    Private _Servicio As ServiceController = Nothing
    Private _NotifiIcon_Activo As Icon
    Private _NotifiIcon_Inactivo As Icon
    Private _ListaAccesos As New List(Of String)
    Private _HayCambiosPath As Boolean = False

    Private Const CLAVE As String = "Miharu.FileProvider"

#End Region

#Region " Eventos "

    Private Sub MainForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadConfig()
    End Sub

    Private Sub MainForm_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If e.CloseReason = CloseReason.UserClosing Then
            AbrirToolStripMenuItem.Enabled = True
            e.Cancel = True
            Me.Visible = False
        End If
    End Sub

    Private Sub SalirToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SalirToolStripMenuItem.Click
        Application.Exit()
    End Sub

    Private Sub AcercaDeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AcercaDeToolStripMenuItem.Click
        Dim f As New AboutForm

        f.ShowDialog()
    End Sub

    Private Sub AbrirToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AbrirToolStripMenuItem.Click
        AbrirToolStripMenuItem.Enabled = False
        Me.Show()
    End Sub

    Private Sub SalirContextualToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SalirContextualToolStripMenuItem.Click
        Application.Exit()
    End Sub

    Private Sub WorkingFolderTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WorkingFolderTextBox.TextChanged
        ActivarGuardar()
    End Sub

    Private Sub IntervaloTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IntervaloTextBox.TextChanged
        ActivarGuardar()
    End Sub

    Private Sub HistoricoTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HistoricoTextBox.TextChanged
        ActivarGuardar()
    End Sub
    Private Sub AppNameTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AppNameTextBox.TextChanged
        ActivarGuardar()
    End Sub

    Private Sub PuertoTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PuertoTextBox.TextChanged
        ActivarGuardar()
    End Sub

    Private Sub IniciarButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IniciarButton.Click
        IniciarServicio()
    End Sub

    Private Sub DetenerButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DetenerButton.Click
        DetenerServicio()
    End Sub

    Private Sub ReiniciarButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReiniciarButton.Click
        ReiniciarServicio()
    End Sub

    Private Sub GuardarServicioButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GuardarServicioButton.Click
        SaveServico()
    End Sub

    Private Sub MonitorServicioTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MonitorServicioTimer.Tick
        MonitorearServicio()
    End Sub

    Private Sub NotificacionNotifyIcon_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles NotificacionNotifyIcon.MouseDoubleClick
        Me.Show()
        Me.WindowState = FormWindowState.Normal
        Me.Focus()
    End Sub

    Private Sub CargarPathButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CargarPathButton.Click
        WorkingFolderTextBox.Text = GetFolder(WorkingFolderTextBox.Text)
    End Sub

    Private Sub ProbarButton_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        If (e.Button = MouseButtons.Right) Then
            Dim f = New Miharu.FileProvider.FileProviderDDOService()

            f.IniciarServicio()
        End If
    End Sub

#End Region

#Region " Metodos "

    Private Sub LoadConfig()
        ' Iconos de notificación
        _NotifiIcon_Activo = Icon.FromHandle((CType(IconosImageList.Images(0), Bitmap)).GetHicon())
        _NotifiIcon_Inactivo = Icon.FromHandle((CType(IconosImageList.Images(1), Bitmap)).GetHicon())

        ' Cargar datos servicio
        If (File.Exists(Program.AppDataPath + Library.FileProviderConfig.ConfigFileName)) Then
            Program.Config = Library.FileProviderConfig.Deserialize(Program.AppDataPath)
        Else
            _GuardadoServicio = False
        End If

        AppNameTextBox.Text = Program.Config.AppName
        PuertoTextBox.Text = Program.Config.Puerto.ToString()

        AppNameTextBox.Text = Program.Config.AppName
        WorkingFolderTextBox.Text = Program.Config.WorkingFolder
        PuertoTextBox.Text = Program.Config.Puerto.ToString()
        IntervaloTextBox.Text = Program.Config.IntervaloDepuracion.ToString()
        HistoricoTextBox.Text = Program.Config.TiempoHistoricoEliminacion.ToString()

        ActivarGuardar()

        MonitorearServicio()

    End Sub

    Private Sub SaveServico()
        If Validar() Then
            Program.Config.AppName = AppNameTextBox.Text
            Program.Config.Puerto = CInt(PuertoTextBox.Text)            
            Program.Config.WorkingFolder = WorkingFolderTextBox.Text
            Program.Config.IntervaloDepuracion = CInt(IntervaloTextBox.Text)
            Program.Config.TiempoHistoricoEliminacion = CInt(HistoricoTextBox.Text)

            Try
                Library.FileProviderConfig.Serialize(Program.Config, Program.AppDataPath)

                MessageBox.Show("Los datos se almacenaron correctamente, para que los cambios tengan efecto debe reiniciar el servicio", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)

                _GuardadoServicio = True
            Catch ex As Exception
                MessageBox.Show("Se presento un error al almacenar los datos", "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Error)

                _GuardadoServicio = False
            End Try
        End If

        ActivarGuardar()
    End Sub

    Private Sub ActivarGuardar()
        ' Servicio
        Dim HayCambios As Boolean

        HayCambios = (Program.Config.AppName <> AppNameTextBox.Text) Or _
                     (Program.Config.WorkingFolder <> WorkingFolderTextBox.Text) Or _
                     (Program.Config.Puerto.ToString() <> PuertoTextBox.Text) Or _
                     (Program.Config.IntervaloDepuracion.ToString() <> IntervaloTextBox.Text) Or _
                     (Program.Config.TiempoHistoricoEliminacion.ToString() <> HistoricoTextBox.Text)

        GuardarServicioButton.Enabled = Not _GuardadoServicio Or HayCambios
    End Sub

    Private Sub MonitorearServicio()
        MonitorServicioTimer.Enabled = False

        Try

            Dim Servicios() As ServiceController

            Servicios = ServiceController.GetServices()
            _Servicio = Nothing

            For Each ServicioEncontrado As ServiceController In Servicios
                If ServicioEncontrado.ServiceName = "MiharuFileProviderDDOService" Then
                    _Servicio = ServicioEncontrado
                End If
            Next

            If _Servicio Is Nothing Then
                EstadoToolStripStatusLabel.Text = "El servicio no se encuentra instalado"

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
                        EstadoToolStripStatusLabel.Text = "Servicio activo"
                        NotificacionNotifyIcon.Icon = _NotifiIcon_Activo
                        NotificacionNotifyIcon.Text = "SLYG FileProvider [Activo]"

                    Case Else
                        EstadoToolStripStatusLabel.Text = "Servicio detenido"
                        NotificacionNotifyIcon.Icon = _NotifiIcon_Inactivo
                        NotificacionNotifyIcon.Text = "SLYG FileProvider [Inactivo]"

                End Select

            End If

        Catch
            IniciarButton.Enabled = False
            DetenerButton.Enabled = False
            ReiniciarButton.Enabled = False

            EstadoToolStripStatusLabel.Text = "Error al validar el servicio"
        End Try

        MonitorServicioTimer.Enabled = True
    End Sub

    Private Sub IniciarServicio()
        Try
            _Servicio.Start()
            MonitorearServicio()

            NotificacionNotifyIcon.BalloonTipIcon = ToolTipIcon.Info
            NotificacionNotifyIcon.BalloonTipTitle = "FileProvider"
            NotificacionNotifyIcon.BalloonTipText = "El servicio FileProvider ha sido activado"
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
                NotificacionNotifyIcon.BalloonTipTitle = "FileProvider"
                NotificacionNotifyIcon.BalloonTipText = "El servicio FileProvider ha sido desactivado"
                NotificacionNotifyIcon.ShowBalloonTip(5000)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error al detener el servicio", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ReiniciarServicio()
        EstadoToolStripStatusLabel.Text = "Reiniciando el servicio..."

        If _Servicio.Status = ServiceControllerStatus.Running Then
            Try
                _Servicio.Stop()

                NotificacionNotifyIcon.BalloonTipIcon = ToolTipIcon.Warning
                NotificacionNotifyIcon.BalloonTipTitle = "FileProvider"
                NotificacionNotifyIcon.BalloonTipText = "El servicio FileProvider ha sido desactivado"
                NotificacionNotifyIcon.ShowBalloonTip(5000)

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

                NotificacionNotifyIcon.BalloonTipIcon = ToolTipIcon.Info
                NotificacionNotifyIcon.BalloonTipTitle = "FileProvider"
                NotificacionNotifyIcon.BalloonTipText = "El servicio FileProvider ha sido activado"
                NotificacionNotifyIcon.ShowBalloonTip(5000)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Error al reiniciar el servicio", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If

        MonitorearServicio()

    End Sub

#End Region

#Region " Funciones "

    Private Function Validar() As Boolean
        If AppNameTextBox.Text = "" Then
            MessageBox.Show("Debe ingresar el AppName", "Datos inválidos", MessageBoxButtons.OK, MessageBoxIcon.Error)
            AppNameTextBox.Focus()

        ElseIf PuertoTextBox.Text = "" Then
            MessageBox.Show("Debe ingresar el Puerto", "Datos inválidos", MessageBoxButtons.OK, MessageBoxIcon.Error)
            PuertoTextBox.Focus()
        ElseIf Not IsNumeric(PuertoTextBox.Text) Then
            MessageBox.Show("El Puerto debe ser un dato numérico entre 100 y 64.000", "Datos inválidos", MessageBoxButtons.OK, MessageBoxIcon.Error)
            PuertoTextBox.Focus()
            PuertoTextBox.SelectAll()
        ElseIf CInt(PuertoTextBox.Text) < 100 Or CInt(PuertoTextBox.Text) > 64000 Then
            MessageBox.Show("El Puerto debe ser un dato numérico entre 100 y 64.000", "Datos inválidos", MessageBoxButtons.OK, MessageBoxIcon.Error)
            PuertoTextBox.Focus()
            PuertoTextBox.SelectAll()


        ElseIf IntervaloTextBox.Text = "" Then
            MessageBox.Show("Debe ingresar el intervalo de depuración", "Datos inválidos", MessageBoxButtons.OK, MessageBoxIcon.Error)
            IntervaloTextBox.Focus()
        ElseIf Not IsNumeric(IntervaloTextBox.Text) Then
            MessageBox.Show("El intervalo de depuración debe ser un valor numérico", "Datos inválidos", MessageBoxButtons.OK, MessageBoxIcon.Error)
            IntervaloTextBox.Focus()
            IntervaloTextBox.SelectAll()
        ElseIf CInt(IntervaloTextBox.Text) < 10000 Then
            MessageBox.Show("El intervalo de depuración debe ser un valor mayor o igual a 10.000", "Datos inválidos", MessageBoxButtons.OK, MessageBoxIcon.Error)
            IntervaloTextBox.Focus()
            IntervaloTextBox.SelectAll()

        ElseIf HistoricoTextBox.Text = "" Then
            MessageBox.Show("Debe ingresar el tiempo de almacenamiento de hitóricos", "Datos inválidos", MessageBoxButtons.OK, MessageBoxIcon.Error)
            HistoricoTextBox.Focus()
        ElseIf Not IsNumeric(HistoricoTextBox.Text) Then
            MessageBox.Show("El tiempo de almacenamiento de hitóricos debe ser un valor numérico", "Datos inválidos", MessageBoxButtons.OK, MessageBoxIcon.Error)
            HistoricoTextBox.Focus()
            HistoricoTextBox.SelectAll()
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