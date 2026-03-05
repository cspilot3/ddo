<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MiharuMasterPopUp.master"
    CodeBehind="p_endtidades_logo.aspx.vb" Inherits="Miharu.Security._sitio.administracion.estructura.p_endtidades_logo"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="Miharu.Web.Controls" Namespace="Miharu.Web.Controls" TagPrefix="Miharu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../../_styles/StyleSheet_Marco.css" rel="stylesheet" type="text/css" />
    <link href="../../../_styles/StyleSheet_Miharu.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Cuerpo" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" style="margin: 10px 10px 10px 10px">
        <tr>
            <td style="height: 50px">
                <asp:Label ID="lblMensaje" runat="server" CssClass="Label" Text="Seleccione la imagen a usar como logo de la entidad"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 500px;" valign="top">
                <input type="file" id="ifCargar" runat="server" size="70" />
            </td>
        </tr>
        <tr>
            <td style="height: 30px">
            </td>
        </tr>
        <tr>
            <td align="center">
                <table>
                    <tr>
                        <td style="width: 90px" align="right">
                            <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" />
                        </td>
                        <td style="width: 20px">
                        </td>
                        <td style="width: 90px">
                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
