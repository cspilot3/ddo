<%@ Page  UICulture="es" Culture="es-MX" Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Main/FormMasterPage.Master" CodeBehind="SubSerie.aspx.vb" Inherits="Miharu.Core.Sitio.TRD.SubSerie" %>

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
                    <asp:Label ID="lblTRD13" runat="server" CssClass="Label" Text="Id SubSerie"></asp:Label>
                </td>
                <td>
                    
                    <cc1:DNumber ID="id_TRD_Subserie" runat="server" Enabled="False" 
                        Width="100px" />
                    
                </td>
            </tr>
            <tr>
                <td style="width:200px">
                    <asp:Label ID="lblTRD" runat="server" CssClass="Label" Text="TRD"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="fk_TRD" runat="server" AutoPostBack="True" Width="200px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width:200px">
                    <asp:Label ID="lblTRD0" runat="server" CssClass="Label" Text="Serie"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="fk_TRD_Serie" runat="server" Width="200px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width:200px">
                    <asp:Label ID="lblTRD2" runat="server" CssClass="Label" Text="Dependencia"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="fk_Dependencia" runat="server" Width="200px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width:200px">
                    <asp:Label ID="lblTRD3" runat="server" CssClass="Label" Text="Nombre SubSerie"></asp:Label>
                </td>
                <td>
                    <cc1:DTexto ID="Nombre_TRD_Subserie" runat="server" Width="200px" 
                        IsRequiered="True" ValidationGroup="Guardar" />
                </td>
            </tr>
            <tr>
                <td style="width:200px">
                    <asp:Label ID="lblTRD4" runat="server" CssClass="Label" Text="Código Subserie"></asp:Label>
                </td>
                <td>
                    <cc1:DTexto ID="Codigo_TRD_Subserie" runat="server" Width="100px" 
                        IsRequiered="True" ValidationGroup="Guardar" />
                </td>
            </tr>
            <tr>
                <td style="width:200px">
                    <asp:Label ID="lblTRD5" runat="server" CssClass="Label" 
                        Text="Días Archivo Gestión"></asp:Label>
                </td>
                <td>
                    <cc1:DNumber ID="Dias_Archivo_Gestion" runat="server" Width="50px" 
                        IsRequiered="True" IsRange="False" MaximumValue="0" MinimumValue="0" 
                        TypeDB="Custom" MaxLength="5" ValidationGroup="Guardar" />
                </td>
            </tr>
            <tr>
                <td style="width:200px">
                    <asp:Label ID="lblTRD6" runat="server" CssClass="Label" 
                        Text="Días Archivo Central"></asp:Label>
                </td>
                <td>
                    <cc1:DNumber ID="Dias_Archivo_Central" runat="server" Width="50px" 
                        IsRequiered="True" IsRange="False" MaximumValue="0" MinimumValue="0" 
                        TypeDB="Custom" MaxLength="5" ValidationGroup="Guardar" />
                </td>
            </tr>
            <tr>
                <td style="width:200px">
                    <asp:Label ID="lblTRD7" runat="server" CssClass="Label" Text="Eliminación"></asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="Eliminacion" runat="server" CssClass="Label" />
                </td>
            </tr>
            <tr>
                <td style="width:200px">
                    <asp:Label ID="lblTRD8" runat="server" CssClass="Label" 
                        Text="Conservación Total"></asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="Conservacion_Total" runat="server" CssClass="Label" />
                </td>
            </tr>
            <tr>
                <td style="width:200px">
                    <asp:Label ID="lblTRD9" runat="server" CssClass="Label" Text="Microfilmación"></asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="Microfilmacion" runat="server" CssClass="Label" />
                </td>
            </tr>
            <tr>
                <td style="width:200px">
                    <asp:Label ID="lblTRD10" runat="server" CssClass="Label" Text="Digitalización"></asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="Digitalizacion" runat="server" CssClass="Label" />
                </td>
            </tr>
            <tr>
                <td style="width:200px">
                    <asp:Label ID="lblTRD11" runat="server" CssClass="Label" Text="Selección"></asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="Seleccion" runat="server" CssClass="Label" />
                </td>
            </tr>
            <tr>
                <td style="width:200px">
                    <asp:Label ID="lblTRD12" runat="server" CssClass="Label" Text="Observaciones"></asp:Label>
                </td>
                <td>
                    <cc1:DTexto ID="Observaciones" runat="server" Width="200px" 
                        IsRequiered="True" ValidationGroup="Guardar" />
                </td>
            </tr>
        </table>
    </asp:Panel> 
</asp:Content>
