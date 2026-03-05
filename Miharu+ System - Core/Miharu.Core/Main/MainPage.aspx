<%@ Page  UICulture="es" Culture="es-MX" Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Main/MainMasterPage.Master" CodeBehind="MainPage.aspx.vb" Inherits="Miharu.Core.Main.MainPage" %>
<%@ Register Src="../Controles/MenuControl.ascx" TagName="MenuControl" TagPrefix="uc8" %>

<asp:Content ID="MainMasterBody" ContentPlaceHolderID="MasterBody" runat="server">
    <table style="width: 1000px; height: 500px;">
        <tr>
            <td style="width: 200px; height: 500px; vertical-align:top" >
                <uc8:MenuControl ID="MenuControl1" runat="server" />
            </td>
            <td style="vertical-align:top; text-align:left" >
                <asp:UpdatePanel ID="UpdatePanel1000" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <iframe id="ifPagina" scrolling="no" marginheight="0" marginwidth="0" runat="server" height="580" width="795" style="background-color: #FFFFFF;" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>