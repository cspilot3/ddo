Imports System.IO
Imports System.ServiceProcess
Imports DBAgrario
Imports Miharu.Security.Library.WebService

Namespace Formularios

    Public Class MainForm

#Region " Declaraciones "

        Private _Servicio As ServiceController = Nothing
        Private _CargaIcon_Activo As Icon
        Private _CargaIcon_Inactivo As Icon

#End Region

#Region " Eventos "

        Private Sub MainForm_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            LoadConfig()
        End Sub

        Private Sub MainForm_FormClosing(ByVal sender As System.Object, ByVal e As FormClosingEventArgs) Handles MyBase.FormClosing
            If e.CloseReason = CloseReason.UserClosing Then
                mnuiAbrir.Enabled = True
                e.Cancel = True
                Me.Visible = False
            End If
        End Sub

        Private Sub mnuSalir_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles mnuSalir.Click
            Application.Exit()
        End Sub

        Private Sub mnuAcercaDe_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles mnuAcercaDe.Click
            Dim f As New AboutForm

            f.ShowDialog()
        End Sub

        Private Sub mnuiAbrir_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles mnuiAbrir.Click
            mnuiAbrir.Enabled = False
            Me.Show()
        End Sub

        Private Sub mnuiSalir2_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles mnuiSalir2.Click
            Application.Exit()
        End Sub

        Private Sub btnIniciar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnIniciar.Click
            IniciarServicio()
        End Sub
        Private Sub btnDetener_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnDetener.Click
            DetenerServicio()
        End Sub
        Private Sub btnReiniciar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnReiniciar.Click
            ReiniciarServicio()
        End Sub

        Private Sub btnGuardarServicio_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnGuardarServicio.Click
            GuardarDatosServico()
        End Sub

        Private Sub tmrMonitorServicio_Tick(ByVal sender As System.Object, ByVal e As EventArgs) Handles tmrMonitorServicio.Tick
            MonitorearServicio()
        End Sub

        Private Sub IconoNotificacion_MouseDoubleClick(ByVal sender As System.Object, ByVal e As MouseEventArgs) Handles IconoNotificacion.MouseDoubleClick
            Me.Show()
            Me.WindowState = FormWindowState.Normal
            Me.Focus()
        End Sub


        'Probar la conexión a la base de datos
        Private Sub btnProbar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnProbar.Click
            ProbarServicio()
        End Sub

        Private Sub txtIntervaloEscaneo_TextChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles txtIntervaloEscaneo.TextChanged
            Activar_GuardarCambios()
        End Sub

        Private Sub txtServicioWeb_TextChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles txtServicioWeb.TextChanged
            Activar_GuardarCambios()
        End Sub

        Private Sub txtContraseña_TextChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles txtContraseña.TextChanged
            Activar_GuardarCambios()
        End Sub

        Private Sub btnProbar_MouseDown(ByVal sender As System.Object, ByVal e As MouseEventArgs) Handles btnProbar.MouseDown
            If (e.Button = MouseButtons.Left) Then
                ProbarServicio()
            ElseIf (e.Button = MouseButtons.Right) Then
                Dim obj As New PunteoAgrarioFileUpService()
                obj.IniciarServicio()
            End If
        End Sub
#End Region

#Region " Metodos "

        Private Sub LoadConfig()
            Try
                ' Cargar datos servicio
                If (File.Exists(Program.AppDataPath + PunteoAgrarioFileUpConfig.ConfigFileName)) Then
                    Program.Config = PunteoAgrarioFileUpConfig.Deserialize(Program.AppDataPath)            
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

            ' Iconos de notificación
            _CargaIcon_Activo = Icon.FromHandle((CType(imgIconoList.Images(0), Bitmap)).GetHicon())
            _CargaIcon_Inactivo = Icon.FromHandle((CType(imgIconoList.Images(1), Bitmap)).GetHicon())

            ' Servicio
            txtIntervaloEscaneo.Text = CStr(Program.Config.IntervaloEscaneo)
            txtServicioWeb.Text = Program.Config.SecurityWebServiceURL
            txtUsuario.Text = Program.Config.User
            txtContraseña.Text = PunteoAgrarioFileUpConfig.Decrypt(Program.Config.Password)
            txtCarpetaFileUp.Text = Program.Config.CarpetaFileUp
            txtCarpetaProcesado.Text = Program.Config.CarpetaProcesado
            txtCarpetaNoProcesado.Text = Program.Config.CarpetaNoProcesado
            txtCarpetaPDF.Text = Program.Config.CarpetaGeneracionPDF
            txtNombreLlaveCifrado.Text = Program.Config.NombreLlaveCifrado

            'Cargar la hora de la generación de los PDF
            Dim _hourDef, _horas, _min, _minAux As Integer
            _hourDef = Program.Config.HoraGeneracionPDF
            If _hourDef > 59 Then
                _horas = Math.Truncate(_hourDef / 60)
                _minAux = _horas * 60
                If _minAux <> _hourDef Then _min = _hourDef - _minAux Else _min = 0
            End If
            Dim hour As Date
            Dim extra As String
            '  If _min <> 0 Then
            extra = _horas & ":" & _min
            '  Else
            '  extra = _horas & ":" & _hourDef
            '  End If
            hour = Convert.ToDateTime(extra)
            dtHoraGeneracion.Value = hour

            Activar_GuardarCambios()
            MonitorearServicio()
            'btnProbar.Enabled = False
        End Sub

        Private Sub IniciarServicio()
            Try
                _Servicio.Start()
                MonitorearServicio()

                IconoNotificacion.BalloonTipIcon = ToolTipIcon.Info
                IconoNotificacion.BalloonTipTitle = "BanAgrarioFileUp"
                IconoNotificacion.BalloonTipText = "El servicio BanAgrarioFileUp ha sido activado"
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
                    IconoNotificacion.BalloonTipTitle = "BanAgrarioFileUp"
                    IconoNotificacion.BalloonTipText = "El servicio BanAgrarioFileUp ha sido desactivado"
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
                    IconoNotificacion.BalloonTipTitle = "BanAgrarioFileUp"
                    IconoNotificacion.BalloonTipText = "El servicio BanAgrarioFileUp ha sido desactivado"
                    IconoNotificacion.ShowBalloonTip(5000)

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

                    IconoNotificacion.BalloonTipIcon = ToolTipIcon.Info
                    IconoNotificacion.BalloonTipTitle = "BanAgrarioFileUp"
                    IconoNotificacion.BalloonTipText = "El servicio BanAgrarioFileUp ha sido activado"
                    IconoNotificacion.ShowBalloonTip(5000)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Error al reiniciar el servicio", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If

            MonitorearServicio()

        End Sub

        Private Sub Activar_GuardarCambios()
            'If Not _Cargando Then
            '    ' Servicio
            '    Dim HayCambios As Boolean

            '    HayCambios = (Program.Config.User <> txtUsuario.Text) Or _
            '                (Program.Config.SecurityWebServiceURL <> txtServicioWeb.Text) Or _
            '                (PunteoAgrarioFileUpConfig.Decrypt(Program.Config.Password) <> txtContraseña.Text) Or _
            '                (Program.Config.IntervaloEscaneo <> CInt(IIf(txtIntervaloEscaneo.Text <> "", txtIntervaloEscaneo.Text, 0))) Or _
            '                (Program.Config.CarpetaFileUp <> txtCarpetaFileUp.Text) Or _
            '                (Program.Config.CarpetaProcesado <> txtCarpetaProcesado.Text) Or _
            '                (Program.Config.CarpetaNoProcesado <> txtCarpetaNoProcesado.Text) Or _
            '                (Program.Config.CarpetaGeneracionPDF <> txtCarpetaPDF.Text)

            '    btnGuardarServicio.Enabled = Not _GuardadoServicio Or HayCambios
            'End If
        End Sub

        Private Sub MonitorearServicio()
            tmrMonitorServicio.Enabled = False

            Try
                Dim Servicios() As ServiceController

                Servicios = ServiceController.GetServices()
                _Servicio = Nothing

                For Each ServicioEncontrado As ServiceController In Servicios
                    If ServicioEncontrado.ServiceName = "PunteoAgrarioFileUpService" Then
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
                            IconoNotificacion.Icon = _CargaIcon_Activo
                            IconoNotificacion.Text = "Punteo BanAgrario FileUp [Activo]"
                        Case Else
                            tsslEstado.Text = "Servicio detenido"
                            IconoNotificacion.Icon = _CargaIcon_Inactivo
                            IconoNotificacion.Text = "Punteo BanAgrario FileUp [Inactivo]"
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

        Private Sub GuardarDatosServico()
            If ValidarCampos() Then
                Try
                    ' Servicio
                    Program.Config.IntervaloEscaneo = CInt(txtIntervaloEscaneo.Text)
                    Program.Config.SecurityWebServiceURL = txtServicioWeb.Text
                    Program.Config.User = txtUsuario.Text
                    Program.Config.Password = PunteoAgrarioFileUpConfig.Encrypt(txtContraseña.Text)
                    Program.Config.CarpetaFileUp = txtCarpetaFileUp.Text
                    Program.Config.CarpetaProcesado = txtCarpetaProcesado.Text
                    Program.Config.CarpetaNoProcesado = txtCarpetaNoProcesado.Text
                    Program.Config.CarpetaGeneracionPDF = txtCarpetaPDF.Text
                    Program.Config.NombreLlaveCifrado = txtNombreLlaveCifrado.Text

                    'Actualizar el tiempo para la generación de los PDF
                    Dim numHoras, numMin, minTotal As Integer
                    numHoras = dtHoraGeneracion.Value.Hour
                    numMin = dtHoraGeneracion.Value.Minute
                    minTotal = (numHoras * 60) + numMin
                    Program.Config.HoraGeneracionPDF = minTotal

                    PunteoAgrarioFileUpConfig.Serialize(Program.Config, Program.AppDataPath)

                    MessageBox.Show("Los datos se almacenaron correctamente, para que los cambios tengan efecto debe reiniciar el servicio", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If

            Activar_GuardarCambios()
            ' btnGuardarServicio.Enabled = False
            ' btnProbar.Enabled = True
        End Sub

        'Probar la conexión a la base de datos
        Private Sub ProbarServicio()
            'If btnGuardarServicio.Enabled Then
            MessageBox.Show("Recuerde almacenar los cambios para poder realizar la prueba", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            '  Else
            Me.Enabled = False
            Me.Cursor = Cursors.WaitCursor

            ValidarServicio()

            Me.Cursor = Cursors.Default
            Me.Enabled = True
            ' End If
        End Sub

        Public Sub ValidarServicio()
            Dim WebService As SecurityWebService

            Try
                Dim ConnectionStrings As PunteoAgrarioFileUpConfig.TypeConnectionString


                WebService = New SecurityWebService(Program.Config.SecurityWebServiceURL, "")
                WebService.CrearCanalSeguro()
                WebService.setUser(Program.Config.User, PunteoAgrarioFileUpConfig.Decrypt(Program.Config.Password))
                ConnectionStrings = PunteoAgrarioFileUpConfig.getCadenasConexion(WebService)

                Dim dbmBanagrario As New DBAgrarioDataBaseManager(ConnectionStrings.PunteoAgrario)

                Try
                    ' Abrir la conexion sin usuario
                    dbmBanagrario.Connection_Open(1)
                Catch ex As Exception
                    Throw New Exception("No se pudo realizar la conexión a la base de datos Punteo BanAgrario", ex)
                Finally
                    dbmBanagrario.Connection_Close()
                End Try

                MessageBox.Show("Prueba exitosa", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Information)

            Catch ex As Exception
                MessageBox.Show("Prueba fallida: " & ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)

            End Try
        End Sub

#End Region

#Region " Funciones "

        Private Function ValidarCampos() As Boolean
            If txtIntervaloEscaneo.Text = "" Then
                MessageBox.Show("Debe ingresar el intervalo de escaneo", "Datos inválidos", MessageBoxButtons.OK, MessageBoxIcon.Error)
                tcBase.SelectedIndex = 2
                txtIntervaloEscaneo.Focus()
            ElseIf Not IsNumeric(txtIntervaloEscaneo.Text) Then
                MessageBox.Show("El intervalo de escaneo debe ser un dato numérico", "Datos inválidos", MessageBoxButtons.OK, MessageBoxIcon.Error)
                tcBase.SelectedIndex = 2
                txtIntervaloEscaneo.Focus()
                txtIntervaloEscaneo.SelectAll()
            ElseIf txtCarpetaFileUp.Text = "" Then
                MessageBox.Show("Debe ingresar la carpeta donde se encuentran ubicados los archivos a cargar", "Datos inválidos", MessageBoxButtons.OK, MessageBoxIcon.Error)
                tcBase.SelectedIndex = 2
                txtCarpetaFileUp.Focus()
            ElseIf txtCarpetaProcesado.Text = "" Then
                MessageBox.Show("Debe crear la carpeta donde se alamcenarán los archivos que son cargados exitosamente", "Datos inválidos", MessageBoxButtons.OK, MessageBoxIcon.Error)
                tcBase.SelectedIndex = 2
                txtCarpetaProcesado.Focus()
            ElseIf txtCarpetaNoProcesado.Text = "" Then
                MessageBox.Show("Debe crear la carpeta donde se alamcenarán los archivos cuya carga no fue exitosa", "Datos inválidos", MessageBoxButtons.OK, MessageBoxIcon.Error)
                tcBase.SelectedIndex = 2
                txtCarpetaNoProcesado.Focus()
            Else
                Return True
            End If

            Return False

        End Function

#End Region

    End Class

End Namespace