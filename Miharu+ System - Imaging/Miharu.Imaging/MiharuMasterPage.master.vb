Imports System.Text
Imports Miharu.Security.Library.SecurityServiceReference
Imports Miharu.Security.Library.WebService

Partial Public Class MiharuMasterPage
    Inherits System.Web.UI.MasterPage

#Region " Declaraciones "

    Private Const IconInformation As String = "MB_information"
    Private Const IconWarning As String = "MB_warning"
    Private Const IconError As String = "MB_error"

    Public Enum MsgBoxIcon As Byte
        IconInformation = 1
        IconWarning = 2
        IconError = 3
    End Enum

    Public Event HijaClose()

#End Region

#Region " Eventos "

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            Dim ScriptCreator As StringBuilder

            ' Scripts del encabezado
            ScriptCreator = New StringBuilder("")

            ' TapPanel
            ScriptCreator.Append(vbTab & "<link href='" & ResolveClientUrl("~/_styles/Tabpanel/TabpanelStyles.css") & "' rel='stylesheet' type='text/css' />" & vbCrLf)

            ' Grilla
            ScriptCreator.Append(vbTab & "<script src='" & ResolveClientUrl("~/_styles/Gridview/SlygGridView.js") & "' type='text/javascript'></script>" & vbCrLf)

            ' Alerta
            ScriptCreator.Append(vbTab & "<link href='" & ResolveClientUrl("~/_scripts/ModalBox/modalbox.css") & "' rel='stylesheet' type='text/css' />" & vbCrLf)
            ScriptCreator.Append(vbTab & "<script src='" & ResolveClientUrl("~/_scripts/ModalBox/lib/prototype.js") & "' type='text/javascript'></script>" & vbCrLf)
            ScriptCreator.Append(vbTab & "<script src='" & ResolveClientUrl("~/_scripts/ModalBox/lib/scriptaculous.js?load=effects") & "' type='text/javascript'></script>" & vbCrLf)
            ScriptCreator.Append(vbTab & "<script src='" & ResolveClientUrl("~/_scripts/ModalBox/modalbox.js") & "' type='text/javascript'></script>" & vbCrLf)

            ' Ventana modal
            'ScriptCreator.Append(vbTab & "<script src='" & ResolveClientUrl("~/_scripts/windows/javascripts/prototype.js") & "' type='text/javascript'></script>" & vbCrLf)
            ScriptCreator.Append(vbTab & "<script src='" & ResolveClientUrl("~/_scripts/windows/javascripts/window.js") & "' type='text/javascript'></script>" & vbCrLf)
            ScriptCreator.Append(vbTab & "<script src='" & ResolveClientUrl("~/_scripts/windows/javascripts/window_ext.js") & "' type='text/javascript'></script>" & vbCrLf)
            'ScriptCreator.Append(vbTab & "<script src='" & ResolveClientUrl("~/_scripts/windows/javascripts/effects.js") & "' type='text/javascript'></script>" & vbCrLf)
            ScriptCreator.Append(vbTab & "<script src='" & ResolveClientUrl("~/_scripts/windows/javascripts/debug.js") & "' type='text/javascript'></script>" & vbCrLf)
            ScriptCreator.Append(vbTab & "<link href='" & ResolveClientUrl("~/_scripts/windows/themes/default.css") & "' rel='stylesheet' type='text/css' />" & vbCrLf)
            ScriptCreator.Append(vbTab & "<link href='" & ResolveClientUrl("~/_scripts/windows/themes/alphacube.css") & "' rel='stylesheet' type='text/css' />" & vbCrLf)

            ' DialogBox
            ScriptCreator.Append(vbTab & "<link href='" & ResolveClientUrl("~/_styles/StyleSheet_DialogBox.css") & "' rel='stylesheet' type='text/css' />" & vbCrLf)

            ' Funciones generales [Debe ser la ultima en cargarse]
            ScriptCreator.Append(vbTab & "<script src='" & ResolveClientUrl("~/_scripts/MiharuMasterPageScript.js") & "' type='text/javascript'></script>" & vbCrLf)

            ltrHeadScripts.Text = ScriptCreator.ToString
        Else
            EndRequestAction.Value = ""
        End If
    End Sub

    Private Sub lnkbHijaClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbHijaClose.Click
        RaiseEvent HijaClose()
    End Sub

    Protected Sub mnuOpciones_MenuItemClick(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.MenuEventArgs) Handles mnuOpciones.MenuItemClick
        e.Item.Selected = False

        Select Case e.Item.Value
            Case "Password"
                ModalPopupPassword.Show()

            Case "About"
                ShowDialog("about.aspx", "About", "Acerca de " & Program.AssemblyTitle, "555", "325", "100", "100", False)

            Case "Cerrar"
                ModalPopupCerrarSesion.Show()

        End Select
    End Sub

    Protected Sub btnCerrarSesionAceptar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCerrarSesionAceptar.Click
        CerrarSesion()
    End Sub

    Private Sub PasswordAceptarButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PasswordAceptarButton.Click
        AsignarPassword(Me.OldPasswordTextBox.Text, Me.NewPasswordTextBox.Text, Me.ConfirmPasswordTextBox.Text)
    End Sub

#End Region

#Region " Metodos "

    Public Sub CerrarSesion()
        Session.Abandon()
        Response.Redirect("~/_sitio/login.aspx")
    End Sub

    Public Sub Cerrar()
        Session.Abandon()
        EndRequestAction.Value = "Close|Cerrar"
    End Sub

    Public Sub ShowAlert(ByVal nMensaje As String, ByVal nIcon As MsgBoxIcon, Optional ByVal Ancho As Short = 420)
        Dim Icono As String = ""

        Select Case nIcon
            Case MsgBoxIcon.IconError : Icono = IconError
            Case MsgBoxIcon.IconInformation : Icono = IconInformation
            Case MsgBoxIcon.IconWarning : Icono = IconWarning
        End Select

        EndRequestAction.Value = "Alert|" & nMensaje & ";" & Icono & ";" & Ancho
    End Sub

    Public Sub ShowWindow(ByVal URLPage As String, ByVal Titulo As String, ByVal PageHeight As Short, ByVal PageWidth As Short, _
                                Optional ByVal PageTop As Short = 0, Optional ByVal PageLeft As Short = 0, _
                                Optional ByVal BarraEstado As Boolean = True, Optional ByVal BarraHerramientas As Boolean = False, _
                                Optional ByVal BarraMenus As Boolean = False, Optional ByVal Bloquear As Boolean = False, _
                                Optional ByVal CambiarTamaño As Boolean = True, Optional ByVal BarrasDesplazamiento As Boolean = False)

        Dim Parametros, Status, Toolbar, Menubar, Location, Resizable, Scrollbars As String

        Status = SetYesNoValue(BarraEstado)
        Toolbar = SetYesNoValue(BarraHerramientas)
        Menubar = SetYesNoValue(BarraMenus)
        Location = SetYesNoValue(Bloquear)
        Resizable = SetYesNoValue(CambiarTamaño)
        Scrollbars = SetYesNoValue(BarrasDesplazamiento)

        Parametros = "height=" & PageHeight & "px, width=" & PageWidth & "px, top=" & PageTop & "px, " & _
                                "left=" & PageLeft & "px, status=" & Status & ", toolbar=" & Toolbar & ", menubar=" & Menubar & _
                                ", location=" & Location & ", resizable=" & Resizable & ", scrollbars=" & Scrollbars

        EndRequestAction.Value = "ShowWindow|" & URLPage & ";" & Titulo & ";" & Parametros

    End Sub

    Public Sub ShowDialog(ByVal strUrl As String, ByVal strNombre As String, ByVal strTitle As String, ByVal valWidth As String, ByVal valHeight As String, ByVal valLeft As String, ByVal valTop As String, ByVal EventoCancel As Boolean)
        Dim Cancel As String = CStr(IIf(EventoCancel, "1", "0"))

        EndRequestAction.Value = "ShowDialog|" & strUrl & ";" & strNombre & ";" & strTitle & ";" & valWidth & ";" & valHeight & ";" & valLeft & ";" & valTop & ";" & Cancel
    End Sub

    Private Sub AsignarPassword(nOldPassword As String, nNewPassword As String, nConfirmPassword As String)
        ModalPopupPassword.Hide()

        If (nNewPassword = nConfirmPassword) Then
            Dim MySesion As Security.Library.Session.Sesion = CType(Session("Sesion"), Security.Library.Session.Sesion)
            Dim WebService = New SecurityWebService(Program.SecurityWebServiceURL, MySesion.ClientIPAddress)

            Try
                Dim nMsgError As String = String.Empty
                WebService.CrearCanalSeguro()
                WebService.setUser(MySesion.Usuario.Login, nOldPassword)
                Dim Respuesta = WebService.ChangePassword(MySesion.Usuario.Login, nNewPassword, nMsgError)

                Select Case Respuesta
                    Case EnumValidateUser.INVALIDO_PASSWORD
                        ShowAlert("Contraseña no inválida", MsgBoxIcon.IconWarning)

                    Case EnumValidateUser.ERROR_PASSWORD
                        ShowAlert("La contraseña no tiene la complejidad requerida por las políticas de seguridad del sistema", MsgBoxIcon.IconWarning)

                End Select
            Catch ex As Exception
                ShowAlert(ex.Message, MsgBoxIcon.IconError)
            End Try
        Else
            ShowAlert("Las contraseñas ingresadas no coinciden", MiharuMasterPage.MsgBoxIcon.IconWarning)
        End If
    End Sub

#End Region

#Region " Funciones "

    Private Function SetYesNoValue(ByVal Valor As Boolean) As String
        If Valor Then
            Return "yes"
        Else
            Return "no"
        End If
    End Function

#End Region

End Class