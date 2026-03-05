<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MiharuMasterPopUp.master"
    CodeBehind="about.aspx.vb" Inherits="Miharu.Imaging.about" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../_styles/StyleSheet_Marco.css" rel="stylesheet" type="text/css" />
    <link href="../_styles/StyleSheet_Miharu.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Cuerpo" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" bgcolor="#ECE9D8">
        <tr>
            <td style="width: 256px">
                <img alt="" src="../_images/basic/MiharuImagingImage.png" height="256px" width="256px" />
            </td>
            <td style="width: 30px">
            </td>
            <td valign="top">
                <table border="0" cellpadding="2" cellspacing="5">
                   <%-- <tr>
                        <td>
                            <img alt="" src="../_images/basic/logoSLYG.png" />
                        </td>
                    </tr>--%>
                    <tr>
                        <td style="height: 10px">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblProductName" runat="server" CssClass="Titulo_Principal" Text="ProductName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblVersion" runat="server" CssClass="Titulo_Menu" Text="Version"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblCopyright" runat="server" CssClass="Titulo_Menu" Text="Copyright"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblCompanyName" runat="server" CssClass="Titulo_Menu" Text="CompanyName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtBoxDescription" runat="server" CssClass="Parrafo" Height="50px"
                                Width="250px" Text="BoxDescription" ReadOnly="True" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="3" style="height: 1px; background-color: #333333;">
            </td>
        </tr>
        <tr>
            <td colspan="3" style="height: 1px; background-color: #CCCCCC;">
            </td>
        </tr>
        <tr>
            <td colspan="3" align="right" style="padding: 10px">
                <asp:Button ID="btnCerrar" runat="server" Text="Cerrar" />
            </td>
        </tr>
    </table>
</asp:Content>
