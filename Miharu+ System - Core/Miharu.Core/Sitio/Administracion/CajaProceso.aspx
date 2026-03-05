<%@ Page  UICulture="es" Culture="es-MX" Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Main/FormMasterPage.Master" CodeBehind="CajaProceso.aspx.vb" Inherits="Miharu.Core.Sitio.Administracion.CajaProceso" %>
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

    <table style="width: 100%;">
    
    <tr><td><asp:Label ID="Label3" runat="server" Text="Entidad" CssClass="Label"></asp:Label></td>
    <td><asp:DropDownList ID="find_fk_Entidad" runat="server" AutoPostBack="True" Height="20px" Width="184px" /></td></tr>

    <tr><td><asp:Label ID="Label2" runat="server" Text="Sede" CssClass="Label"></asp:Label></td>
    <td><asp:DropDownList ID="find_fk_Sede" runat="server" AutoPostBack="False" Height="20px" Width="184px" /></td></tr>

    <tr><td><asp:Label ID="Label4" runat="server" Text="Cajas Proceso" CssClass="Label"></asp:Label></td>
    <td><asp:CheckBox id="find_Es_Proceso" runat="server" Enabled="false" Checked="true" /></td></tr>

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
                    <asp:Label ID="Label1" runat="server" CssClass="Label" Text="Entidad" Width="200px"></asp:Label>
                    <asp:DropDownList ID="fk_Entidad" runat="server" AutoPostBack="True" Height="20px" Width="184px"></asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="Label21" runat="server" CssClass="Label" Text="Sede" Width="200px"></asp:Label>
                    <asp:DropDownList ID="fk_Sede" runat="server" Height="20px" Width="184px"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label22" runat="server" CssClass="Label" Text="Id Caja" Width="200px"></asp:Label>
                    <cc1:DNumber ID="id_Caja" runat="server" Enabled="false" WaterText="Auto" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label23" runat="server" CssClass="Label" Text="Codigo Caja" Width="200px"></asp:Label>
                    <cc1:DTexto ID="Codigo_Caja" runat="server" AutoPostBack="False" BackColor_="" CssClass_="" EmptyValueMessage="*" Heigth="" InvalidValueMessage="   *" IsRequiered="True" MaxLength="100" MensajeColor="Red" Multiline="SingleLine" Text="" TooltipMessage="" ValidationGroup="Guardar" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label24" runat="server" CssClass="Label" Text="Tipo de caja" Width="200px"></asp:Label>
                    <asp:DropDownList ID="fk_Caja_Tipo" runat="server" Height="20px" Width="184px"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label25" runat="server" CssClass="Label" Text="Es Proceso" Width="200px" Visible="False"></asp:Label>
                    <asp:CheckBox ID="Es_Proceso" runat="server" Enabled="False" Visible="False" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label26" runat="server" CssClass="Label" Text="Entidad Cliente" Width="200px" Visible="False"></asp:Label>
                    <cc1:DNumber ID="fk_Entidad_Cliente" runat="server" Enabled="false" WaterText="" Visible="False" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label27" runat="server" CssClass="Label" Text="Proyecto Cliente" Width="200px" Visible="False"></asp:Label>
                    <cc1:DNumber ID="fk_Proyecto_Cliente" runat="server" Enabled="false" WaterText="" Visible="False" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label28" runat="server" CssClass="Label" Text="Estado" Width="200px" Visible="False"></asp:Label>
                    <cc1:DNumber ID="fk_Estado" runat="server" Enabled="false" WaterText="Auto" Visible="False" />
                </td>
            </tr>

            <tr>
                <td>
                    <cc1:DFecha ID="Fecha_Creacion" runat="server" Enabled="false" Visible="false" />
                    <cc1:DFecha ID="Fecha_Cierre" runat="server" Enabled="false" Visible="false" />
                    <cc1:DNumber ID="fk_Usuario_Cierre" runat="server" Enabled="false" Visible="False" />
                </td>
            </tr>

        </table>
</asp:Panel>
</asp:Content>
