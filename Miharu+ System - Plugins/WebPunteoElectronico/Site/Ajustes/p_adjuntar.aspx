<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPopUp.Master" AutoEventWireup="true"
    CodeBehind="p_adjuntar.aspx.cs" Inherits="WebPunteoElectronico.Site.Ajustes.p_adjuntar" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="Miharu.Web.Controls" Namespace="Miharu.Web.Controls" TagPrefix="Miharu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ShowLoader" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyContentPlaceHolder" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" style="margin: 10px 10px 10px 10px">
        <tr>
            <td>
                <asp:Label ID="lblMensaje" runat="server" CssClass="Label" Text="Archivo a adjuntar"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="height: 10px">
                <asp:FileUpload ID="ifCargar" runat="server" Width="540px" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="ErrorLabel" runat="server" Font-Bold="True" ForeColor="Red" Style="font-size: x-small"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="height: 10px">
                <table align="center">
                    <tr>
                        <td>
                            <asp:Button CssClass="button" ID="btnAceptar" runat="server" Text="Aceptar" />
                        </td>
                        <td style="width: 40px">
                        </td>
                        <td style="width: 90px">
                            <asp:Button  CssClass="button" ID="btnCancelar" runat="server" Text="Cancelar" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 10px">
                <asp:Label ID="lblMensaje0" runat="server" CssClass="Label" Text="Archivos Cargados"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblItems" runat="server" CssClass="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Eventos" runat="server">
</asp:Content>
