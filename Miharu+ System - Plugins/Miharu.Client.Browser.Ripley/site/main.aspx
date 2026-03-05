<%@ Page Title="" Language="C#" MasterPageFile="~/master/master_site.Master" AutoEventWireup="true" CodeBehind="main.aspx.cs" Inherits="Miharu.Client.Browser.site.main" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="main.js" type="text/javascript"></script>
    <link href="<%=ResolveClientUrl("~/styles/detail_tooltip.css")%>" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <iframe id="FormIFrame" class="framechild" scrolling="auto" marginheight="0" marginwidth="0" style="border-width: 0; border-style: none; display: none;" src="application/dashboard.aspx"></iframe>
    <div class="menu-options">
        <div class="imagebutton help a-panel" style="top: 5px; right: 20px; width: 30px; height: 30px;" title="Ayuda" onclick="$('#OptionTooltip').show();">
        </div>
        <div class="option-dashboard" title="Ir a la pantalla principal" onclick="Site.ShowDashBoard();">
        </div>
    </div>
    <div id="OptionTooltip" class="uiEnvolvente hidden">
        <div class="uiEnvolvente" onclick="$('#OptionTooltip').hide();">
        </div>
        <div class="uiContextualDialogPositioner uiContextualDialogLeft" style="right: 55px; top: 95px;">
            <div class="uiOverlay uiContextualDialog uiOverlayArrowRight" style="top: 0; width: 400px;">
                <div class="uiOverlayContent">
                    <div class="uiOverlayContentHolder">
                        <div class="user">
                            <asp:Image ID="UserImage" CssClass="user" runat="server" ImageUrl="~/images/basic/user.png" />
                            <table>
                                <tr>
                                    <td colspan="3">
                                        <asp:Label ID="UserLabel" runat="server" CssClass="enfasis" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Empresa:
                                    </td>
                                    <td class="cs10">
                                    </td>
                                    <td>
                                        <asp:Label ID="EntidadLabel" runat="server" CssClass="text" Text="" EnableViewState="False"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Usuario:
                                    </td>
                                    <td class="cs10">
                                    </td>
                                    <td>
                                        <asp:Label ID="LoginLabel" runat="server" CssClass="text" Text="" EnableViewState="False"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Perfil:
                                    </td>
                                    <td class="cs10">
                                    </td>
                                    <td>
                                        <asp:Label ID="PerfilLabel" runat="server" CssClass="text" Text="" ToolTip="" EnableViewState="False"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Rol:
                                    </td>
                                    <td class="cs10">
                                    </td>
                                    <td>
                                        <asp:Label ID="RolLabel" runat="server" CssClass="text" Text="" ToolTip="" EnableViewState="False"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <img alt="" class="imagebutton password" src="<%= ResolveClientUrl("~/images/basic/change-password.png") %>" title="Cambiar la contraseña del ususario" onclick="AppInfo.ChangePassword();" />
                        </div>
                        <div class="uiLine">
                        </div>
                        <div class="connection">
                            <table>
                                <tr>
                                    <td>
                                        Dirección IP:
                                    </td>
                                    <td class="cs10">
                                    </td>
                                    <td>
                                        <asp:Label ID="IPNameLabel" runat="server" CssClass="text" Text="" EnableViewState="False"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Última conexión
                                    </td>
                                    <td class="cs10">
                                    </td>
                                    <td>
                                        <asp:Label ID="LastConnectionLabel" runat="server" CssClass="text" Text="" EnableViewState="False"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="uiLine">
                        </div>
                        <div class="bottom">
                            <img alt="" class="imagebutton slyg" src="<%= ResolveClientUrl("~/images/basic/logoPyC.png") %>" onclick="$('#OptionTooltip').hide(); Site.About();" />
                            <img alt="" class="imagebutton to-exit" src="<%= ResolveClientUrl("~/images/basic/exit.png") %>" title="Salir del sistema" onclick="$('#OptionTooltip').hide(); Site.Salir();" />
                        </div>
                    </div>
                    <div class="statusbar">
                        <span id="clock">Hora</span>
                    </div>
                </div>
                <div class="uiOverlayArrow" style="top: 15px; margin-top: 0;">
                </div>
                <div class="uiClose" onclick="$('#OptionTooltip').hide();">
                    X
                </div>
            </div>
        </div>
    </div>
    <div id="ChangePassword_Ventana" class="hidden">
        <table style="width: 100%; border: 0; border-collapse: separate; border-spacing: 5px;">
            <tr>
                <td colspan="2">
                    <span id="ChangePassword_Message" class="error_message"></span>
                </td>
            </tr>
            <tr>
                <td>
                    Contraseña actual
                </td>
                <td>
                    <input id="OldPassword" type="password" style="width: 250px" />
                </td>
            </tr>
            <tr>
                <td>
                    Nueva contraseña
                </td>
                <td>
                    <input id="NewPassword" type="password" style="width: 250px" />
                </td>
            </tr>
            <tr>
                <td>
                    Confirmar contraseña
                </td>
                <td>
                    <input id="ConfirmPassword" type="password" style="width: 250px" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div style="height: 20px; width: 10px;">
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table style="width: 100%">
                        <tr>
                            <td style="text-align: right">
                                <div id="AceptarPassword" class="button" style="height: 25px; width: 100px;">
                                    Aceptar</div>
                            </td>
                            <td class="cs20">
                            </td>
                            <td style="text-align: left">
                                <div id="CancelarPassword" class="button" style="height: 25px; width: 100px;">
                                    Cancelar</div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div id='LoadingDiv' class="Loading_div">
        <img class="ProcessImg" src="<%= ResolveClientUrl("~/images/basic/ajax-loader.gif") %>" alt="" />
    </div>
</asp:Content>
