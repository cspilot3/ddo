Partial Public Class MiharuMasterPopUp
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

            ' Funciones generales [Debe ser la ultima en cargarse]
            ScriptCreator.Append(vbTab & "<script src='" & ResolveClientUrl("~/_scripts/MiharuMasterPopUpScript.js") & "' type='text/javascript'></script>" & vbCrLf)

            ltrHeadScripts.Text = ScriptCreator.ToString
        End If
    End Sub

#End Region

#Region " Metodos "

    Public Sub Cerrar(ByVal result As Boolean)
        EndRequestAction.Value = "Close|" & result
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

#End Region

End Class