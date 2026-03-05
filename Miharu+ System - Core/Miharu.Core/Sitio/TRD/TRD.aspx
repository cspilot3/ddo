<%@ Page  UICulture="es" Culture="es-MX" Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Main/FormMasterPage.Master" CodeBehind="TRD.aspx.vb" Inherits="Miharu.Core.Sitio.TRD.TRD" %>

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
    </asp:Content>
    
<asp:Content ID="Content4" ContentPlaceHolderID="MasterFilter" runat="server">
    <asp:Panel ID="pnlFiltro" runat="server" style="width: 100%;" Visible="true" >

<table style="width:100%;">
        <tr><td><asp:Label ID="Label8" runat="server" CssClass="Label" Text="Entidad" Width="200px"></asp:Label>
            <asp:DropDownList ID="find_fk_entidad" runat="server" AutoPostBack="True" 
                Height="20px" Width="184px">
            </asp:DropDownList>
        </td></tr>
        
        <tr>
        <td><asp:Label ID="Label11" runat="server" CssClass="Label" Text="Versión" Width="200px"></asp:Label>
            <cc1:DTexto ID="find_version_trd" runat="server" MaxLength="20" IsRequiered="true"/>
        </td></tr>

        <tr><td><asp:Label ID="Label12" runat="server" CssClass="Label" Text="Activo" Width="200px"></asp:Label>
            <asp:CheckBox ID="find_activa_trd" runat="server" />
        </td></tr>

        </table>

    </asp:Panel>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="MasterGrid" runat="server">
    <asp:Panel ID="pnlGrilla" runat="server" style="width: 100%;">
        <asp:Label ID="NumRegistros" runat="server" Text="Label" CssClass="Label"></asp:Label>
        <br /><br />
        <cc1:CoreGridView ID="grdData" runat="server" AutoGenerateColumns="true" OnEndPreSelect="OnPreselectMasterGrid">
        </cc1:CoreGridView>
    </asp:Panel>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="MasterDetail" runat="server">
    <asp:Panel ID="panelDetalle" runat="server" style="width: 100%;" Visible="true" ScrollBars="Auto">

        <table style="width:100%;">

        <tr><td><asp:Label ID="Label5" runat="server" CssClass="Label" Text="Entidad" Width="200px"></asp:Label>
            <asp:DropDownList ID="fk_entidad" runat="server" AutoPostBack="True" 
                Height="20px" Width="184px">
            </asp:DropDownList>
        </td></tr>

        <tr><td><asp:Label ID="Label2" runat="server" CssClass="Label" Text="Id TRD" Width="200px"></asp:Label>
            <cc1:DNumber ID="id_TRD" runat="server" WaterText="Auto" Enabled="false" 
                ValidationGroup="Guardar" />
        </td></tr>

        <tr><td>
            <asp:Label ID="Label13" runat="server" CssClass="Label" Text="Nombre TRD" 
                Width="200px"></asp:Label>
            <cc1:DTexto ID="Nombre_TRD" runat="server" IsRequiered="true" MaxLength="100" 
                ValidationGroup="Guardar" />
        </td></tr>

            <tr>
                <td>
                    <asp:Label ID="Label4" runat="server" CssClass="Label" Text="Fecha TRD" 
                        Width="200px"></asp:Label>
                    <cc1:DFecha ID="Fecha_trd" runat="server" IsRequiered="true" />
                </td>
            </tr>

        <tr><td><asp:Label ID="Label6" runat="server" CssClass="Label" Text="Versión" Width="200px"></asp:Label>
            <cc1:DTexto ID="version_trd" runat="server" MaxLength="20" IsRequiered="true" 
                ValidationGroup="Guardar"/>
        </td></tr>

        <tr><td><asp:Label ID="Label7" runat="server" CssClass="Label" Text="Activo" Width="200px"></asp:Label>
            <asp:CheckBox ID="Activa_TRD" runat="server" />
        </td></tr>

        </table>
</asp:Panel>
</asp:Content>
