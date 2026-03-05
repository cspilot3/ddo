<%@ Page  UICulture="es" Culture="es-MX" Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Main/FormMasterPage.Master" CodeBehind="Servidor_Volumen_Imaging.aspx.vb" Inherits="Miharu.Core.Sitio.Administracion.Servidor_Volumen_Imaging" %>

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
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="MasterGrid" runat="server">
    <asp:Panel ID="pnlGrilla" runat="server" style="width: 100%;">
        <asp:Label ID="NumRegistros" runat="server" Text="Label" CssClass="Label"></asp:Label>
        <br/>
        
        <cc1:CoreGridView ID="grdData" runat="server" AutoGenerateColumns="true" OnEndPreSelect="OnPreselectMasterGrid">
        </cc1:CoreGridView>
    </asp:Panel>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="MasterDetail" runat="server">
    <asp:Panel ID="pnlDetalle" runat="server" style="width: 100%;" Visible="true" ScrollBars="Auto" >
        <table style="width:90%">
            <tr>
                <td style="width:200px">
                    <asp:Label ID="Label7" runat="server" CssClass="Label" 
                        Text="id Servidor Volumen"></asp:Label>
                </td>
                <td>
                    <cc1:DNumber ID="id_Servidor_Volumen" runat="server" Width="100px" 
                        Enabled="False" />
                </td>
            </tr>
            <tr>
                <td style="width:200px">
                    <asp:Label ID="Label5" runat="server" CssClass="Label" Text="Entidad"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="fk_Entidad" runat="server" Width="200px" 
                        AutoPostBack="True">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width:200px">
                    <asp:Label ID="Label6" runat="server" CssClass="Label" Text="Servidor"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="fk_Servidor" runat="server" Width="200px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width:200px">
                    <asp:Label ID="Label8" runat="server" CssClass="Label" 
                        Text="Nombre Servidor Volumen"></asp:Label>
                </td>
                <td>
                    <cc1:DTexto ID="Nombre_Servidor_Volumen" runat="server" Width="200px" 
                        IsRequiered="True" />
                </td>
            </tr>
            <tr>
                <td style="width:200px">
                    <asp:Label ID="Label9" runat="server" CssClass="Label" 
                        Text="Path Servidor Volumen"></asp:Label>
                </td>
                <td>
                    <cc1:DTexto ID="Path_Servidor_Volumen" runat="server" Width="200px" 
                        IsRequiered="True" />
                </td>
            </tr>
            <tr>
                <td style="width:200px">
                    <asp:Label ID="Label10" runat="server" CssClass="Label" 
                        Text="Capacidad Servidor Volumen"></asp:Label>
                </td>
                <td>
                    <cc1:DNumber ID="Capacidad_Servidor_Volumen" runat="server" Width="150px" 
                        IsRequiered="True" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
