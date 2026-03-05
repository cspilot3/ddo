Partial Public Class MiharuMasterForm
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

            ' Ventana modal
            ScriptCreator.Append(vbTab & "<script src='" & ResolveClientUrl("~/_scripts/windows/javascripts/prototype.js") & "' type='text/javascript'></script>" & vbCrLf)
            ScriptCreator.Append(vbTab & "<script src='" & ResolveClientUrl("~/_scripts/windows/javascripts/window.js") & "' type='text/javascript'></script>" & vbCrLf)
            ScriptCreator.Append(vbTab & "<script src='" & ResolveClientUrl("~/_scripts/windows/javascripts/window_ext.js") & "' type='text/javascript'></script>" & vbCrLf)
            ScriptCreator.Append(vbTab & "<script src='" & ResolveClientUrl("~/_scripts/windows/javascripts/effects.js") & "' type='text/javascript'></script>" & vbCrLf)
            ScriptCreator.Append(vbTab & "<script src='" & ResolveClientUrl("~/_scripts/windows/javascripts/debug.js") & "' type='text/javascript'></script>" & vbCrLf)
            ScriptCreator.Append(vbTab & "<link href='" & ResolveClientUrl("~/_scripts/windows/themes/default.css") & "' rel='stylesheet' type='text/css' />" & vbCrLf)
            ScriptCreator.Append(vbTab & "<link href='" & ResolveClientUrl("~/_scripts/windows/themes/alphacube.css") & "' rel='stylesheet' type='text/css' />" & vbCrLf)

            ' Funciones generales [Debe ser la ultima en cargarse]
            ScriptCreator.Append(vbTab & "<script src='" & ResolveClientUrl("~/_scripts/MiharuMasterFormScript.js") & "' type='text/javascript'></script>" & vbCrLf)

            ltrHeadScripts.Text = ScriptCreator.ToString
        End If

    End Sub

    Private Sub lnkbHijaClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbHijaClose.Click
        RaiseEvent HijaClose()
    End Sub

#End Region

#Region " Metodos "

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

    Public Sub ShowWindow(ByVal URLPage As String, ByVal Titulo As String, ByVal PageWidth As String, ByVal PageHeight As String, _
                                Optional ByVal PageTop As String = "0", Optional ByVal PageLeft As String = "0", _
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