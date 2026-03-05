<%@ Page UICulture="es" Culture="es-MX" Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Main/FormMasterPage.Master" CodeBehind="Proyecto.aspx.vb"
    Inherits="Miharu.Core.Sitio.Administracion.Proyecto" %>

<%@ Register Assembly="Miharu.Core" Namespace="Miharu.Core" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MasterHead" runat="server">
    <link href="../../_styles/Styles.css" rel="stylesheet" type="text/css" />
    <link href="../../_styles/ModalPopUp/StyleSheetModalPopUp.css" rel="stylesheet" type="text/css" />
    <link href="../../_styles/Marco/StyleSheetMaster.css" rel="stylesheet" type="text/css" />
    <link href="../../_styles/Tabpanel/TabpanelStyles.css" rel="stylesheet" type="text/css" />
    <link href="../../_styles/Gridview/GridviewStyles.css" rel="stylesheet" type="text/css" />
    <link href="../../_js/windows/themes/default.css" rel="stylesheet" type="text/css" />
    <link href="../../_js/windows/themes/alphacube.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MasterBodyUnique" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MasterFilter" runat="server">
    <asp:Panel ID="pnlFiltro" runat="server" Style="width: 100%;">
        <table style="width: 100%;">
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Entidad" CssClass="Label"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="find_fk_Entidad" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Proyecto" CssClass="Label"></asp:Label>
                </td>
                <td>
                    <cc1:DTexto ID="find_nombre_proyecto" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="Responsable proyecto" CssClass="Label"></asp:Label>
                </td>
                <td>
                    <cc1:DTexto ID="find_responsable_proyecto" runat="server" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MasterGrid" runat="server">
    <asp:Panel ID="pnlGrilla" runat="server" Style="width: 100%;">
        <asp:Label ID="NumRegistros" runat="server" Text="Label" CssClass="Label"></asp:Label>
        <br />
        <cc1:CoreGridView ID="grdData" runat="server" AutoGenerateColumns="true" OnEndPreSelect="OnPreselectMasterGrid">
        </cc1:CoreGridView>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="MasterDetail" runat="server">
    <asp:Panel ID="pnlDetalle" runat="server" Style="width: 100%;" Visible="true">
        <table style="width: 90%">
            <tr>
                <td style="width: 200px">
                    <asp:Label ID="lblIdCampo" runat="server" CssClass="Label" Text="Id Proyecto"></asp:Label>
                </td>
                <td>
                    <cc1:DNumber ID="id_proyecto" runat="server" Enabled="False" />
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    <asp:Label ID="lblEntidad" runat="server" CssClass="Label" Text="Entidad"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="fk_entidad" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    <asp:Label ID="lblDocumento" runat="server" CssClass="Label" Text="Nombre"></asp:Label>
                </td>
                <td>
                    <cc1:DTexto ID="nombre_proyecto" runat="server" Width="300px" IsRequiered="True" ValidationGroup="Guardar" />
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    <asp:Label ID="lblDocumento0" runat="server" CssClass="Label" Text="Vencimiento"></asp:Label>
                </td>
                <td>
                    <cc1:DFecha ID="vencimiento_proyecto" runat="server" IsRequiered="True" ValidationGroup="Guardar" />
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    <asp:Label ID="lblCampoLista" runat="server" CssClass="Label" Text="Responsable"></asp:Label>
                </td>
                <td>
                    <cc1:DTexto ID="Responsable_proyecto" runat="server" Width="300px" IsRequiered="True" ValidationGroup="Guardar" />
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    <asp:Label ID="lblCampoBusqueda" runat="server" CssClass="Label" Text="Telefono Responsable"></asp:Label>
                </td>
                <td>
                    <cc1:DTexto ID="telefono_responsable_proyecto" runat="server" IsRequiered="True" ValidationGroup="Guardar" />
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    <asp:Label ID="lblCampoBusqueda0" runat="server" CssClass="Label" Text="Email Responsable"></asp:Label>
                </td>
                <td>
                    <cc1:DTexto ID="email_responsable_proyecto" runat="server" IsRequiered="True" ValidationGroup="Guardar" />
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    <asp:Label ID="lblCampoBusqueda3" runat="server" CssClass="Label" Text="Tipo Folder"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="fk_Folder_Tipo" runat="server" Height="21px" Width="294px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    <asp:Label ID="lblCampoBusqueda4" runat="server" CssClass="Label" Text="Tipo Caja por defecto"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="fk_Caja_Defecto" runat="server" Height="21px" Width="294px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:CheckBox ID="Aplica_Fisico" runat="server" EnableViewState="true" Text="Aplica Fisico" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:CheckBox ID="Aplica_Imagen" runat="server" EnableViewState="true" Text="Aplica Imagen" />
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    <asp:Label ID="lblCampoBusqueda1" runat="server" CssClass="Label" Text="Nombre llave"></asp:Label>
                </td>
                <td>
                    <cc1:DTexto ID="nombre_proyecto_llave" runat="server" Width="300px" IsRequiered="True" ValidationGroup="llave" />
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    <asp:Label ID="lblCampoBusqueda2" runat="server" CssClass="Label" Text="Tipo"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="fk_campo_tipo" runat="server">
                    </asp:DropDownList>
                    <asp:ImageButton ID="grd_llaves_add" runat="server" ImageUrl="~/_images/basic/check.png" ValidationGroup="llave" />
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <cc1:CoreGridView ID="grd_llaves" runat="server" ClickAction="OnDblClickSelectedPostBack" CssClass="yui-datatable-theme" EnableSort="True" GridNum="0"
                        OnBeginPreSelect="" OnBeginSelect="" OnEndPreSelect="" OnEndSelect="" PreSelectedIndex="-1" PreSelectedStyleCssClass="row-PreSelect">
                        <AlternatingRowStyle CssClass="alt-data-row" />
                        <EditRowStyle CssClass="row-edit" />
                        <PagerStyle CssClass="pager-stl" />
                        <RowStyle CssClass="nor-data-row" />
                        <SelectedRowStyle CssClass="row-Select" />
                        <Columns>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Eliminar" ItemStyle-Width="40">
                                <ItemTemplate>
                                    <asp:ImageButton runat="server" ImageUrl="~/_images/basic/delete.png" ImageAlign="Middle" ID="imgEliminarItem" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </cc1:CoreGridView>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
