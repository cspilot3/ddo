<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MiharuMasterPopUp.Master"
    CodeBehind="p_usuariojefe.aspx.vb" Inherits="Miharu.Security._sitio.administracion.seguridad.p_usuariojefe" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="Miharu.Web.Controls" Namespace="Miharu.Web.Controls" TagPrefix="Miharu" %>
<%@ Register Src="../../../_controls/wucFilter.ascx" TagName="wucFilter" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../../_styles/StyleSheet_Marco.css" rel="stylesheet" type="text/css" />
    <link href="../../../_styles/StyleSheet_Miharu.css" rel="stylesheet" type="text/css" />
    <link href="../../../_styles/gridview/GridviewStyles.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Cuerpo" runat="server">
    <asp:UpdatePanel ID="upCuerpo" runat="server">
        <ContentTemplate>
            <table style="padding: 5px" cellspacing="5" border="0">
                <tr>
                    <td>
                        <asp:Label ID="lblFiltro" runat="server" Text="Filtro: [Apellidos]" CssClass="Label"
                            Width="120px"></asp:Label>
                    </td>
                    <td>
                        <uc1:wucFilter ID="ucFiltro" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div style="border: 1px solid #CCCCCC; overflow: auto; height: 400px; width: 700px">
                            <Miharu:SlygGridView ID="gvBase" runat="server" AutoGenerateColumns="False" GridNum="0"
                                CssClass="yui-datatable-theme" EnableSort="True">
                                <AlternatingRowStyle CssClass="alt-data-row"></AlternatingRowStyle>
                                <Columns>
                                    <asp:BoundField DataField="id_Usuario" HeaderText="Cod." ItemStyle-Width="1" />
                                    <asp:BoundField DataField="Apellidos_Usuario" HeaderText="Apellidos" />
                                    <asp:BoundField DataField="Nombres_Usuario" HeaderText="Nombres" />
                                    <asp:BoundField DataField="Identificacion_Usuario" HeaderText="Cédula" />
                                    <asp:BoundField DataField="Login_Usuario" HeaderText="Login" />
                                    <asp:CheckBoxField DataField="Usuario_Activo" HeaderText="Activo" />
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
