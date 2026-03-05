<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StatusBarWebUserControl.ascx.cs"
    Inherits="WebPunteoElectronico.Controls.StatusBarWebUserControl" %>
<asp:Panel ID="DefaultPanel" runat="server">
    <div style="width: 100%; text-align: center;">
        Punteo Electrónico - Procesos y Canje S.A.
    </div>
</asp:Panel>
<asp:Panel ID="LoggedPanel" runat="server">
    <table border="2" cellpadding="1" cellspacing="1" width="100%">
        <tr>
            <td class="footer-label">
                Entidad:
            </td>
            <td class="footer-text">
                <asp:Label ID="EntidadLabel" runat="server" Text=""></asp:Label>
            </td>
            <td class="footer-label">
                Usuario:
            </td>
            <td class="footer-text">
                <asp:Label ID="UsuarioLabel" runat="server" Text=""></asp:Label>
            </td>
            <td class="footer-label">
                Fecha:
            </td>
            <td class="footer-text">
                <asp:Label ID="FechaLabel" runat="server" Text=""></asp:Label>
            </td>
            <td class="footer-label">
                Fecha Último Ingreso:
            </td>
            <td class="footer-text">
                <asp:Label ID="UltimoIngresoLabel" runat="server" Text=""></asp:Label>
            </td>
            <td class="footer-label">
                IP:
            </td>
            <td class="footer-text">
                <asp:Label ID="IPLabel" runat="server" Text=""></asp:Label>
            </td>
            <td class="footer-label">
                Perfil:
            </td>
            <td class="footer-text">
                <asp:Label ID="PerfilLabel" runat="server" Text=""></asp:Label>
            </td>
        </tr>
    </table>
</asp:Panel>
