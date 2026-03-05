<%@ Page Title="" Language="C#" MasterPageFile="~/master/master_site.Master" AutoEventWireup="true" CodeBehind="connect.aspx.cs" Inherits="Miharu.Client.Browser.site.account.connect" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        Frm = {
            CurrentPage: "connect.aspx",

            Init: function () {
            },

        };

        function ValidateSubmit() {
            parent.Site.ShowProcess();
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="a-panel" style="top: 82px; left: 0; right: 0">
    <table style="margin-right: auto; margin-left: auto;">
        <tr>
            <td class="rs50"></td>
        </tr>
        <tr>
            <td>Se encontró un sesión activa en otro equipo, si continua se cerarrá</td>
        </tr>
        <tr>
            <td class="rs20"></td>
        </tr>
        <tr>
            <td>IP</td>
            <td class="cs20"></td>
            <td>
                <asp:Label ID="IpLabel" runat="server" Text="Label"></asp:Label></td>
        </tr>
        <tr>
            <td>Fecha de conexión</td>
            <td class="cs20"></td>
            <td>
                <asp:Label ID="DateLabel" runat="server" Text="Label"></asp:Label></td>
        </tr>
        <tr>
            <td>Fecha actualización</td>
            <td class="cs20"></td>
            <td>
                <asp:Label ID="LastUpdateLabel" runat="server" Text="Label"></asp:Label></td>
        </tr>
        <tr>
            <td class="rs20"></td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:LinkButton ID="OkLinkButton" runat="server" CssClass="button">Continuar</asp:LinkButton></td>
            <td class="cs20"></td>
            <td>
                <asp:LinkButton ID="CancelLinkButton" runat="server" CssClass="button">Salir</asp:LinkButton></td>
        </tr>
    </table>
    </div>
</asp:Content>
