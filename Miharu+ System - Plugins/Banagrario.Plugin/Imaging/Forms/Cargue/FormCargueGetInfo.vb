Imports System.IO
Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Config

Namespace Imaging.Forms.Cargue

    Public Class CargueGetInfoClass

#Region " Declaraciones "

        Private _plugin As New BanagrarioImagingPlugin
        Public RutaGlobal As String
        Public Shared FechaProceso As Integer
        'Private ProgressForm As New Tools.FormProgress

#End Region

#Region "Propiedades"

        Property SelectedPath As String
            Get
                Return RutaTextBox.Text.TrimEnd("\"c) & "\"
            End Get
            Set(ByVal value As String)
                RutaTextBox.Text = value
            End Set
        End Property

        Public Property Fecha As DateTime
            Get
                Return FechaProcesoPicker.Value
            End Get
            Set(ByVal value As DateTime)
                FechaProcesoPicker.Value = value
            End Set
        End Property

#End Region

#Region " Funciones "

#End Region

#Region " Constructores "

        Public Sub New(ByVal nBanagrarioDesktopPlugin As BanagrarioImagingPlugin)
            InitializeComponent()

            _plugin = nBanagrarioDesktopPlugin

        End Sub

#End Region

#Region " Metodos "

        Private Function Validar() As Boolean
            If Not MostrarAceptarCargue() Then
                'Dim Ruta_Cargue = RutaTextBox.Text.Split("\"c)
                'Dim Ruta_Cargue_Folder = Ruta_Cargue(Ruta_Cargue.Length - 1)

                'Dim Codigo_Oficina_Folder As String = ""
                'Dim Fecha_Movimiento_Folder As String = ""

                FechaProceso = Convert.ToInt32(FechaProcesoPicker.Value.ToString("yyyyMMdd"))


                If (RutaTextBox.Text = "") Then
                    DesktopMessageBoxControl.DesktopMessageShow("Debe seleccionar un directorio válido", "Directorio inválido", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                    RutaTextBox.Focus()
                ElseIf (Not Directory.Exists(Me.SelectedPath)) Then
                    DesktopMessageBoxControl.DesktopMessageShow("Debe seleccionar un directorio válido", "Directorio inválido", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                    RutaTextBox.Focus()
                    RutaTextBox.SelectAll()
                Else
                    Return True
                End If
            End If

            Return False
        End Function

        Private Function MostrarAceptarCargue() As Boolean
            Dim validacionFechCargue As New FormValidacionFechCargue()
            If FechaInvalida() Then
                Dim respuesta = DesktopMessageBoxControl.DesktopMessageShow("La fecha de proceso no puede tener mas de un día de diferencia hacia atrás con la fecha actual, Desea realizar el cargue de todas Formas? ", "Fecha no válida", DesktopMessageBoxControl.IconEnum.WarningIcon, False)
                If (respuesta = DialogResult.OK) Then
                    If (_plugin.Manager.Sesion.Usuario.PerfilManager.PuedeAcceder(Permisos.Imaging.Proceso.Control.Autorizaciones)) Then
                        Me.Hide()
                        validacionFechCargue.ShowDialog()
                        If FormValidacionFechCargue.MotivoCreado Then
                            Me.Show()
                            Return False
                        Else
                            Me.Show()
                        End If
                    Else
                        Me.Hide()
                        If (Utilities.ValidarPermiso(Permisos.Imaging.Proceso.Control.Autorizaciones, Program.AccesoDesktopAssembly, "El usuario no cuenta con permisos para realizar esta acción, Digite por favor usuario y clave de algún usuario que cuente con el perfil necesario.", _plugin.Manager.Sesion.Usuario, _plugin.Manager.DesktopGlobal.SecurityServiceURL, _plugin.Manager.DesktopGlobal.ClientIPAddress)) Then
                            validacionFechCargue.ShowDialog()
                            If FormValidacionFechCargue.MotivoCreado Then
                                Me.Show()
                                Return False
                            Else
                                Me.Show()
                            End If
                        Else
                            Return True
                        End If
                    End If
                End If
                Return True
            End If
            Return False
        End Function

        Private Function FechaInvalida() As Boolean
            Dim horas As TimeSpan = Date.Now.TimeOfDay
            Dim hreglamentaria As TimeSpan = TimeSpan.Parse(_plugin.HoraCambioFechaProceso & ":00:00")
            Dim fechahoy As DateTime = CDate(Date.Now.ToShortDateString)
            Dim fechaselec As DateTime = CDate(FechaProcesoPicker.Value.ToShortDateString)
            If fechaselec = fechahoy Then
                Return False
            Else
                If fechaselec = fechahoy.AddDays(-1) Then
                    If horas <= hreglamentaria Then
                        Return False
                    Else
                        Return True
                    End If
                Else
                    If fechaselec > fechahoy Then
                        Return False
                    End If
                End If
                Return True
            End If
        End Function

        Private Sub LoadConfig()
            Dim newDate = Now

            If (newDate.Hour < _plugin.HoraCambioFechaProceso) Then
                newDate = newDate.AddDays(-1)
            End If

            FechaProcesoPicker.Value = newDate

            RutaTextBox.Text = "D:\Scanner"

        End Sub

        Private Sub SelectFolderPath()

            Dim lectorFolderBrowserDialog = New FolderBrowserDialog()
            Dim respuesta As DialogResult
            lectorFolderBrowserDialog.SelectedPath = "D:\Scanner"

            lectorFolderBrowserDialog.ShowNewFolderButton = False
            lectorFolderBrowserDialog.Description = "Seleccione la carpeta"

            respuesta = lectorFolderBrowserDialog.ShowDialog()

            If (respuesta = DialogResult.OK) Then
                RutaTextBox.Text = lectorFolderBrowserDialog.SelectedPath

                ' Dim Directorios = LectorFolderBrowserDialog

                '**********************************************************
                'Try
                '    Dim _Paquetes = New StringCollection

                '    Dim FechaPaqueteForm As New FormFechaPaquete


                '    Respuesta = FechaPaqueteForm.ShowDialog()

                '    If (Respuesta = DialogResult.OK) Then

                '        Dim Fecha As Date = FechaPaqueteForm.Fecha
                '        Dim strFilter = Fecha.ToString(Program.ImagingGlobal.ProyectoImagingRow.Formato_Fecha_Paquete)
                '        Dim DirectoriosFolder As String()

                '        Try
                '            Directorios = Directory.GetDirectories(Program.ImagingGlobal.ProyectoImagingRow.Input_Folder, Program.ImagingGlobal.ProyectoImagingRow.Inicio_Nombre_Paquete & strFilter & ".*")
                '            Dim i As Integer = 0

                '            ProgressForm.Show()
                '            Application.DoEvents()

                '            ProgressForm.SetMaxValue(DirectoriosFolder.Length)

                '            For Each Directorio As String In DirectoriosFolder
                '                If ProgressForm.Cancelar Then Throw New Exception("La acción fue cancelada por el usuario")

                '                Try
                '                    Dim Paquete As Integer = Convert.ToInt32(Directorio.Split("."c)(Directorio.Split("."c).Length - 1))

                '                    If (Paquete >= FechaPaqueteForm.PaqueteIni And Paquete <= FechaPaqueteForm.PaqueteFin) Then
                '                        _Paquetes.Add(Directorio.TrimEnd("\"c) & "\")
                '                    End If
                '                Catch : End Try

                '                i += 1
                '                ProgressForm.SetProgreso(i)
                '            Next

                '    End If

                '    If _Paquetes.Count = 0 Then
                '        MessageBox.Show("No se encontraron paquetes", "Imagenes", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Else
                '        Return True
                '    End If
                'Catch ex As Exception
                '    MessageBox.Show("No se encontraron paquetes", "Imagenes", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                'End Try

                'Catch ex As Exception
                '    MessageBox.Show("ERROR función: [LoadImagesDirectories] " & ex.Message, " Validación ", MessageBoxButtons.OK, MessageBoxIcon.Error)
                'End Try




                '**********************************************************














            End If



        End Sub

#End Region

#Region " Eventos "

        Private Sub CargueGetInfo_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            LoadConfig()
        End Sub

        Private Sub AceptarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AceptarButton.Click
            If (Validar()) Then
                Me.DialogResult = DialogResult.OK
                Me.Close()
            Else
                Me.DialogResult = DialogResult.None
            End If
        End Sub

        Private Sub CancelarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CancelarButton.Click
            Me.DialogResult = DialogResult.Cancel
        End Sub

        Private Sub SelectFolderButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles SelectFolderButton.Click

            SelectFolderPath()

        End Sub

#End Region

    End Class

End Namespace