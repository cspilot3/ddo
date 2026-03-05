<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MiharuMasterForm.master"
    CodeBehind="parametros.aspx.vb" Inherits="Miharu.Security._sitio.administracion.seguridad.parametros" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="Miharu.Web.Controls" Namespace="Miharu.Web.Controls" TagPrefix="Miharu" %>
<%@ Register Src="../../../_controls/wucFilter.ascx" TagName="wucFilter" TagPrefix="uc1" %>
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
                            <div id="divSave" runat="server" style="width: 25px; display: none">
                                <div class="BotonCambiante">
                                    <asp:ImageButton ID="ibSave" runat="server" ImageUrl="~/_images/opciones/save.png"
                                        ToolTip="Guardar los cambios" ValidationGroup="Guardar" />
                                </div>
                            </div>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTitulo" runat="server" Text="Parámetros" CssClass="Titulo_Principal"></asp:Label>
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
                            <asp:BoundField DataField="Nombre_Parametro_Sistema" HeaderText="Parámetro" ReadOnly="True">
                                <ItemStyle Width="1px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Valor">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtValor" runat="server" Width="150px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Descripcion_Parametro_Sistema" HeaderText="Parámetro"
                                ReadOnly="True" ItemStyle-Width="400px" ItemStyle-Wrap="false"></asp:BoundField>
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
