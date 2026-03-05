<%@ Page  UICulture="es" Culture="es-MX" Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Main/FormMasterPage.Master" CodeBehind="Tipologia.aspx.vb" Inherits="Miharu.Core.Sitio.Administracion.Tipologia" %>
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
            height: 20px;
        }
    </style>
    </asp:Content>
    
<asp:Content ID="Content4" ContentPlaceHolderID="MasterFilter" runat="server">
    <asp:Panel ID="pnlFiltro" runat="server" style="width: 100%;" Visible="true" >

    <table style="width: 100%;">
    
    <tr><td><asp:Label ID="Label3" runat="server" Text="Tipologia" CssClass="Label"></asp:Label></td>
    <td><cc1:DTexto ID="find_nombre_tipologia" runat="server" /></td></tr>

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
        <tr>
        <td>
            <asp:Label ID="Label1" runat="server" CssClass="Label" Text="Id tipologia" 
                Width="200px"></asp:Label>
            <cc1:DNumber ID="id_tipologia" runat="server" WaterText="Auto" Enabled="false" />
        </td>
        </tr>

        <tr><td>
            <asp:Label ID="Label21" runat="server" CssClass="Label" Text="Nombre" 
                Width="200px"></asp:Label>
            <cc1:DTexto ID="nombre_tipologia" runat="server" AutoPostBack="False" BackColor_="" 
                CssClass_="" EmptyValueMessage="*" 
                Heigth="" InvalidValueMessage="   *" IsRequiered="True" 
                MaxLength="100" MensajeColor="Red" Multiline="SingleLine" Text="" 
                TooltipMessage="" ValidationGroup="Guardar"/>
        </td></tr>
        <tr>
           <cc1:DTexto ID="Tipologia_Eliminado" runat="server" WaterText="Auto" Enabled="false" Visible="False" />
           <cc1:DNumber ID="fk_Usuario_Log" runat="server" WaterText="Auto" Enabled="false" Visible="False" />
           <cc1:DFecha ID="Fecha_Log" runat="server" WaterText="Auto" Enabled="false" Visible="False" /> 
        </tr>

        </table>
</asp:Panel>
</asp:Content>
