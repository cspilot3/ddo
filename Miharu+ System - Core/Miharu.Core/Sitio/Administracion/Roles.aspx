<%@ Page  UICulture="es" Culture="es-MX" Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Main/FormMasterPage.Master" CodeBehind="Roles.aspx.vb" Inherits="Miharu.Core.Sitio.Administracion.Roles" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="Miharu.Core" Namespace="Miharu.Core" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MasterHead" runat="server">
    <link href="../../_styles/Styles.css" rel="stylesheet" type="text/css" />
    <link href="../../_styles/ModalPopUp/StyleSheetModalPopUp.css" rel="stylesheet" type="text/css" />
    <link href="../../_styles/Marco/StyleSheetMaster.css" rel="stylesheet" type="text/css" />
    <link href="../../_styles/Tabpanel/TabpanelStyles.css" rel="stylesheet" type="text/css" />
    <link href="../../_styles/Gridview/GridviewStyles.css" rel="stylesheet" type="text/css" />
    <link href="../../_js/windows/themes/default.css" rel="stylesheet" type="text/css" />
    <link href="../../_js/windows/themes/alphacube.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            height: 28px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MasterBodyUnique" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="MasterFilter" runat="server">
    <asp:Panel ID="pnlFiltro" runat="server" style="width: 100%;" Visible="true" >
    </asp:Panel>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="MasterGrid" runat="server">
    <asp:Panel ID="pnlGrilla" runat="server" style="width: 100%;">

        <table style="width:100%;">
            
        </table>
        
        <asp:Label ID="NumRegistros" runat="server" Text="Label" CssClass="Label"></asp:Label>

        <cc1:CoreGridView ID="grdData" runat="server" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField DataField="id_Rol" HeaderText="Id Rol" />
            <asp:BoundField DataField="nombre_rol" HeaderText="Nombre Rol" />
        </Columns>
        </cc1:CoreGridView>
    </asp:Panel>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="MasterDetail" runat="server">
    <asp:Panel ID="pnlDetalle" runat="server" style="width: 95%;" Visible="true" >
    <table style="width:100%;">
        <tr>
            <td>
                    <asp:Label ID="EntidadLabel" runat="server" CssClass="Label" Text="Entidad" Width="200px"></asp:Label>
                    <asp:DropDownList ID="EntidadDropDownList" runat="server" Width="200px" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="ProyectoLabel" runat="server" CssClass="Label" Text="Proyecto" Width="200px"></asp:Label>
                    <asp:DropDownList ID="ProyectoDropDownList" runat="server" Width="200px" AutoPostBack="true">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
        <tr>
            <td>
                <asp:Label ID="Label8" runat="server" CssClass="Label" Text="Esquemas" 
                    Width="200px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <cc1:CoreGridView ID="grdEsquemas" runat="server" AutoGenerateColumns="false" Enabled="true">
                    <Columns>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Docs." ItemStyle-Width="30">
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ImageUrl="~/_images/basic/Explorar.png" ImageAlign="Middle" ID="imgEliminarItem" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="nombre_entidad" HeaderText="Entidad" />
                        <asp:BoundField DataField="nombre_proyecto" HeaderText="Proyecto" />
                        <asp:BoundField DataField="nombre_esquema" HeaderText="Esquema" />

                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Rol" ItemStyle-Width="30">
                            <ItemTemplate>
                                <asp:Label ID="Entidad" runat="server" Text='<%# Eval("nombre_entidad") %>' Visible="false" />
                                <asp:Label ID="Proyecto" runat="server" Text='<%# Eval("nombre_proyecto") %>' Visible="false" />
                                <asp:Label ID="Esquema" runat="server" Text='<%# Eval("nombre_esquema") %>' Visible="false" />
                                <asp:CheckBox ID="Aplica" runat="server" EnableViewState="true" />
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                </cc1:CoreGridView>
            </td>

        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    </asp:Panel>
</asp:Content>
