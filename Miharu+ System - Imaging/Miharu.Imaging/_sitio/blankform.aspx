<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MiharuMasterForm.master"
    CodeBehind="blankform.aspx.vb" Inherits="Miharu.Imaging.blankform" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../_styles/StyleSheet_Marco.css" rel="stylesheet" type="text/css" />
    <link href="../_styles/StyleSheet_Miharu.css" rel="stylesheet" type="text/css" />
    <link href="../_styles/StyleSheet_Menu.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Cuerpo" runat="server">
    <table style="width: 780px" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td style="height: 100px">
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Label ID="lblTitulo" runat="server" Text="MIHARU Imaging" Style="font-size: xx-large;
                    color: #003399; font-weight: bold"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="height: 30px">
            </td>
        </tr>
        <tr>
            <td align="center">
                <img alt="" src="../_images/basic/MiharuImagingImage.png" />
            </td>
        </tr>
    </table>
</asp:Content>
