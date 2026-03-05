Imports DBSecurity
Imports System.Windows.Forms
Imports Miharu
Imports Miharu.Security.Library
Imports Miharu.Security.Library.SecurityServiceReference
Imports Miharu.Security.Library.WebService
Imports BCS.Plugin.Imaging.Carpeta_Unica
Imports Miharu.Desktop.Controls.DesktopMessageBox


Public Class FormAutorizacion

#Region " Declaraciones "

    Private _Plugin As CarpetaUnicaPlugin

    Private Permiso As String

    Private SecurityWebServiceURL As String

    Private ClientIPAddress As String

    Private FechaProceso As String

    Private TipoProceso As Integer

    Public Shared UsuarioAutorizador As String

    Dim LstProcesosAutoriza As New List(Of String)

#End Region

#Region " Constructores "

    Public Sub New(ByVal nCarpetaUnicaDesktopPlugin As CarpetaUnicaPlugin, ByVal nTextoAutorizacion As String, ByVal nAsseblyName As String, ByVal nPermiso As String, ByVal nSecurityWebServiceURL As String, ByVal nClientIPAddress As String, ByVal nFechaProceso As String, ByVal nTipoProceso As Integer)
        ' Llamada necesaria para el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        Me.TextoAutorizacionTextBox.Text = nTextoAutorizacion
        Me.Text = nAsseblyName
        Me.Permiso = nPermiso
        Me.SecurityWebServiceURL = nSecurityWebServiceURL
        Me.ClientIPAddress = nClientIPAddress
        Me.FechaProceso = nFechaProceso
        Me.TipoProceso = nTipoProceso
        Me._Plugin = nCarpetaUnicaDesktopPlugin
    End Sub

#End Region

#Region " Eventos "

    Private Sub AceptarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AceptarButton.Click
        If (Validar()) Then
            If (AutorizarMarcados()) Then
                Me.DialogResult = DialogResult.OK
                Me.Close()
            End If
        End If
    End Sub

    Private Sub CancelarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CancelarButton.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub FormAutorizacion_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim dbmIntegration As New DBIntegration.DBIntegrationDataBaseManager(Me._Plugin.CajaSocialConnectionString)
        Dim ProcesosAutorizar As New DataTable
        Try
            dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

            'obtiene todos los procesos sin cargue completo a autorizar
            ProcesosAutorizar = dbmIntegration.SchemaBCSCarpetaUnica.PA_Obtiene_Procesos_Autorizar.DBExecute(FechaProceso, _Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, TipoProceso)
            
            If ProcesosAutorizar.Rows.Count > 0 Then

                DataGridProcesosAutoriza.DataSource = ProcesosAutorizar

                Dim checkBoxColumn As New DataGridViewCheckBoxColumn()
                checkBoxColumn.HeaderText = ""
                checkBoxColumn.Width = 20
                checkBoxColumn.Name = "checkBoxAutorizar"
                checkBoxColumn.HeaderText = "Autorizar"
                DataGridProcesosAutoriza.Columns.Insert(0, checkBoxColumn)

            End If

        Catch ex As Exception
            DesktopMessageBoxControl.DesktopMessageShow("Autorizar procesos", ex)
        Finally
            dbmIntegration.Connection_Close()
        End Try
    End Sub

#End Region

#Region " Funciones "

    Private Function Validar() As Boolean
        Dim WebService = New SecurityWebService(Me.SecurityWebServiceURL, Me.ClientIPAddress)
        Dim idEntidad As Short = -1
        Dim idUsuario As Integer = -1
        Dim LogonResult = EnumValidateUser.LOGIN_ERROR

        WebService.CrearCanalSeguro()
        WebService.setUser(Me.LoginTextBox.Text, Me.PasswordTextBox.Text)

        If Not (WebService.ValidateUser(idEntidad, idUsuario, LogonResult)) Then

            Select Case LogonResult
                Case EnumValidateUser.FALTA_LOGIN
                    MessageBox.Show("Debe ingresar el usuario", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)

                Case EnumValidateUser.INVALIDO_LOGIN, EnumValidateUser.INVALIDO_PASSWORD
                    MessageBox.Show("Usuario o contraseña invalida", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)

                Case EnumValidateUser.INACTIVO
                    MessageBox.Show("El usuario no se encuentra activo", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)

                Case EnumValidateUser.CAMBIAR_PASSWORD
                    MessageBox.Show("Su clave ha expirado.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)

            End Select

        Else
            Select Case LogonResult
                Case EnumValidateUser.VALIDO, EnumValidateUser.VALIDO

                    Dim LocalSession As New Security.Library.Session.Sesion()

                    WebService.FillSession(LocalSession, Me.Text)

                    If (LocalSession.Usuario.PerfilManager.PuedeEditar(Permiso)) Then
                        UsuarioAutorizador = Me.LoginTextBox.Text
                        Return True
                    Else
                        MessageBox.Show("El usuario no cuenta con permisos suficientes para realizar la acción", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    End If
            End Select

            PasswordTextBox.Focus()
            PasswordTextBox.SelectAll()

        End If


        Return False
    End Function

    Private Function AutorizarMarcados() As Boolean

        For Each row As DataGridViewRow In DataGridProcesosAutoriza.Rows
            Dim isSelected As Boolean = Convert.ToBoolean(row.Cells("checkBoxAutorizar").Value)
            If isSelected Then
                LstProcesosAutoriza.Add(row.Cells("id_Control_Proceso").Value.ToString())
            End If
        Next

        Dim dbmIntegration As New DBIntegration.DBIntegrationDataBaseManager(Me._Plugin.CajaSocialConnectionString)
        dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

        If LstProcesosAutoriza.Count > 0 Then
            For Each itemIdControlProceso In LstProcesosAutoriza
                dbmIntegration.SchemaBCSCarpetaUnica.PA_Autorizar_Procesos_Cruce.DBExecute(CType(itemIdControlProceso, Long), FechaProceso, _Plugin.Manager.Sesion.Usuario.id, ObservacionTexBox.Text)
            Next
        End If

        Return True

    End Function

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

#End Region

End Class

