<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MiharuMasterPage.master"
    CodeBehind="errorform.aspx.vb" Inherits="Miharu.Imaging.errorform" Title="Página sin título" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="_styles/StyleSheet_Miharu.css" rel="stylesheet" type="text/css" />
    <link href="_styles/StyleSheet_Marco.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Opciones" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Cuerpo" runat="server">
    <table cellpadding="0" cellspacing="0">
        <tr>
            <td style="height: 585px; width: 100px">
                &nbsp;
            </td>
            <td align="center" valign="middle">
                <img alt="" src="_images/basic/MiharuImagingImage.png" />
            </td>
            <td style="height: 585px; width: 100px">
                &nbsp;
            </td>
            <td align="left" valign="middle" style="width: 350px">
                <asp:UpdatePanel ID="upCuadros" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="pnlBase" runat="server" DefaultButton="btnSalir">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="height: 20px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="lblTitulo" runat="server" CssClass="Titulo_Principal">Error</asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="height: 30px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <asp:Label ID="lblMensaje" runat="server" CssClass="Error" Width="350" Height="200">Mensaje</asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 30px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="btnSalir" runat="server" Text="Salir" Width="80px" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
