<%@ Page  UICulture="es" Culture="es-MX" Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Main/FormMasterPage.Master" CodeBehind="Boveda.aspx.vb" Inherits="Miharu.Core.Sitio.Boveda.Boveda" %>

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
                    <asp:Label ID="Label3" runat="server" CssClass="Label" Text="Id Bóveda"></asp:Label>
                </td>
                <td>
                    <cc1:DNumber ID="id_Boveda" runat="server" Enabled="False" Width="100px" />
                </td>
            </tr>
            <tr>
                <td style="width:200px">
                    <asp:Label ID="lblEntidad" runat="server" CssClass="Label" Text="Entidad"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="fk_Entidad" runat="server" AutoPostBack="True" 
                        Width="200px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width:200px">
                    <asp:Label ID="Label4" runat="server" CssClass="Label" Text="Sede"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="fk_Sede" runat="server" Width="200px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width:200px">
                    <asp:Label ID="Label5" runat="server" CssClass="Label" Text="Nombre Bóveda"></asp:Label>
                </td>
                <td>
                    <cc1:DTexto ID="Nombre_Boveda" runat="server" IsRequiered="True" 
                        ValidationGroup="Guardar" Width="200px" />
                </td>
            </tr>
            <tr>
                <td style="width:200px">
                    <asp:Label ID="Label6" runat="server" CssClass="Label" 
                        Text="Responsable Bóveda"></asp:Label>
                </td>
                <td>
                    <cc1:DTexto ID="Responsable_Boveda" runat="server" IsRequiered="True" ValidationGroup="Guardar" Width="200px" />
                </td>
            </tr>
            <tr>
                <td style="width:200px">
                    <asp:Label ID="Label7" runat="server" CssClass="Label" Text="Cargo responsable bóveda"></asp:Label>
                </td>
                <td>
                    <cc1:DTexto ID="Cargo_Responsable_Boveda" runat="server" IsRequiered="True" ValidationGroup="Guardar" Width="200px" />
                </td>
            </tr>
            <tr>
                <td style="width:200px">
                    <asp:Label ID="Label1" runat="server" CssClass="Label" Text="Area responsable bóveda"></asp:Label>
                </td>
                <td>
                    <cc1:DTexto ID="Area_Responsable_Boveda" runat="server" IsRequiered="False"  Width="200px" />
                </td>
            </tr>
            <tr>
                <td style="width:200px">
                    <asp:Label ID="Label8" runat="server" CssClass="Label" 
                        Text="Centros de Procesamiento"></asp:Label>
                </td>
                <td>
                    <asp:ImageButton ID="ImageButton1" runat="server" 
                        ImageUrl="~/_images/menu/Busqueda.png" />
                </td>
            </tr>
            <tr>
                <td class="Titulo_Terceario" colspan="2">
                    <table border="0" width="100%">
                        <tr>
                            <td align="left">Secciones</td>
                            <td align="right"><asp:ImageButton ID="imgAgregarSeccion" runat="server" ImageUrl="~/_images/basic/check.png" ToolTip="Agregar Sección" /></td>
                        </tr>
                    </table>    
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <cc1:CoreGridView ID="grdSecciones" runat="server" AutoGenerateColumns="false">
                        <Columns>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Eliminar" ItemStyle-Width="40">
                                <ItemTemplate>
                                    <asp:ImageButton runat="server" ImageUrl="~/_images/basic/delete.png" ImageAlign="Middle" ID="imgEliminarItem" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Id_Boveda_Seccion" HeaderText="Id" />
                            <asp:BoundField DataField="Nombre_Boveda_Seccion" HeaderText="Nombre" />
                            <asp:BoundField DataField="Ambiente_Boveda_Seccion" HeaderText="Ambiente" />
                            <asp:BoundField DataField="Seguridad_Boveda_Seccion" HeaderText="Sección" />
                        </Columns>
                    </cc1:CoreGridView>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;</td>
            </tr>


        </table>
    </asp:Panel>
</asp:Content>
