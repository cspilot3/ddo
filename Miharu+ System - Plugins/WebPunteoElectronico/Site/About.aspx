<%@ Page Title="Acerca de nosotros" Language="C#" MasterPageFile="~/Master/MasterForm.master"
    AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="WebPunteoElectronico.Site.About" %>

<%@ Register Assembly="WebPunteoElectronico" Namespace="WebPunteoElectronico" TagPrefix="cc1" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContentPlaceHolder">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="BodyContentPlaceHolder">
    <h2>
        Acerca de Punteo Electrónico
    </h2>
    <div style="text-align: left; width: 100%">
        <table border="0" cellpadding="0" cellspacing="0" style="margin-left: auto; margin-right: auto">
            <tr>
                <td style="width: 256px">
                    <asp:Image ID="LogoImage" runat="server" ImageUrl="~/Images/basic/WebPunteoElectronicoImage.png" />
                </td>
                <td style="width: 30px">
                </td>
                <td valign="top">
                    <table border="0" cellpadding="2" cellspacing="5">
                        <tr>
                            <td style="height: 10px">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="ProductNameLabel" runat="server" CssClass="Label_Titulo" Text="ProductName"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="VersionLabel" runat="server" CssClass="Label" Text="Version"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="CopyrightLabel" runat="server" CssClass="Label" Text="Copyright"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="CompanyNameLabel" runat="server" CssClass="Label" Text="CompanyName"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Image ID="PyCImage" runat="server" ImageUrl="~/Images/basic/LogoPyC.png" />
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top">
                                <asp:TextBox ID="DescriptionTextBox" runat="server" Height="50px" Width="450px" Text=""
                                    ReadOnly="True">Description</asp:TextBox>
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
        </table>
    </div>
</asp:Content>
