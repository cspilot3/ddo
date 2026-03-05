<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MiharuMasterForm.master"
    CodeBehind="ipsbloqueadas.aspx.vb" Inherits="Miharu.Security._sitio.administracion.seguridad.ipsbloqueadas" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="Miharu.Web.Controls" Namespace="Miharu.Web.Controls" TagPrefix="Miharu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../../_styles/StyleSheet_Marco.css" rel="stylesheet" type="text/css" />
    <link href="../../../_styles/StyleSheet_Miharu.css" rel="stylesheet" type="text/css" />
    <link href="../../../_styles/StyleSheet_Menu.css" rel="stylesheet" type="text/css" />
    <link href="../../../_styles/gridview/GridviewStyles.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Cuerpo" runat="server">
    <table style="width: 780px; height: 585px;" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td style="height: 10px">
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" border="0" style="width: 760px;">
                    <tr>
                        <td style="width: 10px">
                            &#160;
                        </td>
                        <td style="width: 30px">
                            &#160;
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTitulo" runat="server" Text="IPs bloqueadas" CssClass="Titulo_Principal"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 10px">
            </td>
        </tr>
        <tr>
            <td valign="top" style="padding: 0px 5px 5px 5px;" align="left">
                <div style="border: 1px solid #CCCCCC; overflow: auto; height: 515px; width: 765px">
                    <Miharu:SlygGridView ID="gvBase" runat="server" AutoGenerateColumns="False" GridNum="1"
                        CssClass="yui-datatable-theme" EnableSort="True" ClickAction="OnClickNoEvents">
                        <AlternatingRowStyle CssClass="alt-data-row"></AlternatingRowStyle>
                        <Columns>
                            <asp:BoundField DataField="IP_Address" HeaderText="Dirección IP" ReadOnly="True">
                            </asp:BoundField>
                            <asp:ButtonField CommandName="Desbloquear" Text="Desbloquear" ItemStyle-Width="1px" />
                        </Columns>
                        <EditRowStyle CssClass="row-edit"></EditRowStyle>
                        <PagerStyle CssClass="pager-stl"></PagerStyle>
                        <RowStyle CssClass="nor-data-row"></RowStyle>
                        <SelectedRowStyle CssClass="row-edit"></SelectedRowStyle>
                    </Miharu:SlygGridView>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
