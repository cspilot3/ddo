<%@ Page  UICulture="es" Culture="es-MX" Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Main/FormMasterPage.Master" CodeBehind="CampoBusquedaEntidad.aspx.vb" Inherits="Miharu.Core.Sitio.Busqueda.CampoBusquedaEntidad" %>

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

<asp:Content ID="Content3" ContentPlaceHolderID="MasterBodyUnique" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="MasterFilter" runat="server">
    <asp:Panel ID="pnlFiltro" runat="server" style="width: 100%;" Visible="true" >
    </asp:Panel>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="MasterGrid" runat="server">
    <asp:Panel ID="pnlGrilla" runat="server" style="width: 100%;">
        <asp:Label ID="NumRegistros" runat="server" Text="Label" CssClass="Label"></asp:Label>
        <br /><br />
        <cc1:CoreGridView ID="grdData" runat="server" AutoGenerateColumns="false" OnEndPreSelect="OnPreselectMasterGrid">
            <Columns>
                <asp:BoundField DataField="id_entidad" HeaderText="Tipo Equipo" Visible="false" />    
                <asp:BoundField DataField="nombre_entidad" HeaderText="Entidad" />    
                <asp:BoundField DataField="contacto_entidad" HeaderText="Contacto Entidad" />    
            </Columns>
        </cc1:CoreGridView>
    </asp:Panel>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="MasterDetail" runat="server">
<asp:Panel ID="pnlDetalle" runat="server" style="width: 100%;" Visible="true" >
    <table style="width:100%;">
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" CssClass="Label" Text="Entidad" 
                    Width="200px"></asp:Label>
                <asp:DropDownList ID="fk_entidad" runat="server" Height="20px" Width="184px" 
                    AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <br />
                <asp:Label ID="Label4" runat="server" CssClass="Label" 
                    Text="Busquedas permitidas por entidad" Width="200px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:CheckBoxList ID="CheckBoxList1" runat="server" BorderColor="#006699" 
                    BorderStyle="Solid" BorderWidth="1px" CssClass="Label" Height="28px" 
                    RepeatColumns="3" Width="511px" BackColor="#FFFFF4">
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</asp:Panel>
</asp:Content>
