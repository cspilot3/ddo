<%@ Page  UICulture="es" Culture="es-MX" Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Main/PopupMasterPage.Master" CodeBehind="p_Secciones.aspx.vb" Inherits="Miharu.Core.Sitio.Boveda.p_Secciones" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="Miharu.Core" Namespace="Miharu.Core" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MasterHead" runat="server">
    <link href="../../_styles/Gridview/GridviewStyles.css" rel="stylesheet" type="text/css" />
    <link href="../../_styles/Styles.css" rel="stylesheet" type="text/css" />
    <link href="../../_styles/ModalPopUp/StyleSheetModalPopUp.css" rel="stylesheet" type="text/css" />
    <link href="../../_styles/Default.css" rel="stylesheet" type="text/css" />
    <script src="../../_js/CmiGridView.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MasterContent" runat="server">
    <asp:UpdatePanel ID="upCuerpo" runat="server">
        <ContentTemplate>
            <table border="0" cellpadding="2" cellspacing="2" width="90%">
                <tr>
                    <td align="left">
                        <asp:Label ID="Label3" runat="server" CssClass="Label" 
                            Text="Id Sección"></asp:Label>
                    </td>
                    <td align="left">
                        <cc1:DNumber ID="Id_Seccion" runat="server" Enabled="False" Width="100px" />
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="Label4" runat="server" CssClass="Label" 
                            Text="Nombre Sección"></asp:Label>
                    </td>
                    <td align="left">
                        <cc1:DTexto ID="Nombre_Boveda_Seccion" runat="server" Width="200px" 
                            IsRequiered="True" ValidationGroup="Guardar" />
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="Label5" runat="server" CssClass="Label" Text="Ambiente Sección"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="Ambiente_Boveda_Seccion" runat="server" Width="50px">
                            <asp:ListItem>0</asp:ListItem>
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                            <asp:ListItem>3</asp:ListItem>
                            <asp:ListItem>4</asp:ListItem>
                            <asp:ListItem>5</asp:ListItem>
                            <asp:ListItem>6</asp:ListItem>
                            <asp:ListItem>7</asp:ListItem>
                            <asp:ListItem>8</asp:ListItem>
                            <asp:ListItem>9</asp:ListItem>
                            <asp:ListItem>10</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Label ID="Label7" runat="server" CssClass="Label" 
                            Text="0 - Minimo, 10 - Maximo"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="Label6" runat="server" CssClass="Label" Text="Seguridad Sección"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="Seguridad_Boveda_Seccion" runat="server" Width="50px">
                            <asp:ListItem>0</asp:ListItem>
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                            <asp:ListItem>3</asp:ListItem>
                            <asp:ListItem>4</asp:ListItem>
                            <asp:ListItem>5</asp:ListItem>
                            <asp:ListItem>6</asp:ListItem>
                            <asp:ListItem>7</asp:ListItem>
                            <asp:ListItem>8</asp:ListItem>
                            <asp:ListItem>9</asp:ListItem>
                            <asp:ListItem>10</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Label ID="Label8" runat="server" CssClass="Label" 
                            Text="0 - Minimo, 10 - Maximo"></asp:Label>
                        &nbsp;&nbsp;
                        <asp:ImageButton ID="imgGuardarSeccion" runat="server" 
                            ImageUrl="~/_images/tool/save.png" ValidationGroup="Guardar" />
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        &nbsp;</td>
                    <td align="left">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="left" class="Titulo_Terceario" colspan="2">
                        <asp:Panel ID="pnlAddEstante" runat="server" Visible="false">
                            <table border="0" width="100%">
                                <tr>
                                    <td align="left">Estantes </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlPlantillaEstante" runat="server" Width="200px">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right"><asp:ImageButton ID="imgAgregarEstante" runat="server" ImageUrl="~/_images/basic/check.png" ToolTip="Agregar Estante"/></td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="2">
                        <cc1:CoreGridView ID="grdEstante" runat="server" AutoGenerateColumns="false">
                            <Columns>
                                <asp:BoundField DataField="id_Boveda_Estante" HeaderText="Id" />
                                <asp:BoundField DataField="Codigo_Boveda_Estante" HeaderText="Código" />
                                <asp:BoundField DataField="Filas_Boveda_Estante" HeaderText="Filas" />
                                <asp:BoundField DataField="Columnas_Boveda_Estante" HeaderText="Columnas" />
                                <asp:BoundField DataField="Profundidades_Boveda_Estante" HeaderText="Profundidades" />
                                <asp:BoundField DataField="Largo_Boveda_Estante" HeaderText="Largo" />
                                <asp:BoundField DataField="Ancho_Boveda_Estante" HeaderText="Ancho" />
                                <asp:BoundField DataField="Alto_Boveda_Estante" HeaderText="Alto" />
                            </Columns>
                        </cc1:CoreGridView>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
