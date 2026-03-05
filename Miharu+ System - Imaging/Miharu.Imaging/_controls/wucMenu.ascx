<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="wucMenu.ascx.vb" Inherits="Miharu.Imaging.wucMenu" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<link href="../_styles/StyleSheet_Marco.css" rel="stylesheet" type="text/css" />
<link href="../_styles/StyleSheet_Miharu.css" rel="stylesheet" type="text/css" />
<link href="../_styles/StyleSheet_Menu.css" rel="stylesheet" type="text/css" />
<table style="width: 200px;" border="0" cellpadding="0" cellspacing="0">
    <tr>
        <td style="width: 200px; height: 50px;" valign="middle">
            <asp:Image ID="imgLogoCliente" runat="server" Height="40px" Width="200px" />
        </td>
    </tr>
    <tr>
        <td style="width: 200px; height: 30px" class="Titulo_Menu">
            Menú
        </td>
    </tr>
    <tr style="height: 5px;">
        <td>
        </td>
    </tr>
    <tr>
        <td valign="top" style="border-style: none solid solid none; border-width: 1px; border-color: #666666;
            background-color: #EDEDED; height: 500px;">
            <ajaxToolkit:Accordion ID="AccordionMenu" runat="server" SelectedIndex="0" HeaderCssClass="menu-header"
                HeaderSelectedCssClass="menu-header-selected" ContentCssClass="menu-body" FadeTransitions="false"
                FramesPerSecond="40" TransitionDuration="250" AutoSize="None" RequireOpenedPane="True"
                SuppressHeaderPostbacks="true" BorderStyle="None" Width="200px" EnableViewState="False">
                <Panes>
                    <ajaxToolkit:AccordionPane ID="apConsulta" runat="server" EnableViewState="False"
                        ContentCssClass="menu-item-cuerpo">
                        <Header>
                            <div class="div-header">
                                <asp:Image ID="imgConsulta" runat="server" ImageUrl="~/_images/menu/consulta.png"
                                    Style="margin-right: 10px" />Consulta</div>
                        </Header>
                        <Content>
                            <div class="div-cuerpo">
                                <asp:TreeView ID="tvConsulta" runat="server" ImageSet="Arrows" NodeStyle-CssClass="menu-item"
                                    HoverNodeStyle-CssClass="menu-item-hover" SelectedNodeStyle-CssClass="menu-item-selected"
                                    NodeStyle-HorizontalPadding="5px">
                                    <Nodes>
                                    <asp:TreeNode ImageUrl="~/_images/menu/busqueda.png" Text="Búsqueda" Value="1" ToolTip="Búsqueda de archivos digitales" />
                                    </Nodes>
                                </asp:TreeView>
                            </div>
                        </Content>
                    </ajaxToolkit:AccordionPane>
                    <ajaxToolkit:AccordionPane ID="apInformes" runat="server" EnableViewState="False"
                        ContentCssClass="menu-item-cuerpo">
                        <Header>
                            <div class="div-header">
                                <asp:Image ID="imgInformes" runat="server" ImageUrl="~/_images/menu/informes.png"
                                    Style="margin-right: 10px" />Informes</div>
                        </Header>
                        <Content>
                            <div class="div-cuerpo">
                                <asp:TreeView ID="tvInformes" runat="server" ImageSet="Arrows" NodeStyle-CssClass="menu-item"
                                    HoverNodeStyle-CssClass="menu-item-hover" SelectedNodeStyle-CssClass="menu-item-selected"
                                    NodeStyle-HorizontalPadding="5px">
                                    <Nodes>
                                    
                                    </Nodes>
                                </asp:TreeView>
                            </div>
                        </Content>
                    </ajaxToolkit:AccordionPane>
                    <ajaxToolkit:AccordionPane ID="apAdministracion" runat="server" EnableViewState="False"
                        ContentCssClass="menu-item-cuerpo">
                        <Header>
                            <div class="div-header">
                                <asp:Image ID="imgAdministracion" runat="server" ImageUrl="~/_images/menu/administracion.png"
                                    Style="margin-right: 10px" />Administración</div>
                        </Header>
                        <Content>
                            <div class="div-cuerpo">
                                <asp:TreeView ID="tvAdministracion" runat="server" ImageSet="Arrows" NodeStyle-CssClass="menu-item"
                                    HoverNodeStyle-CssClass="menu-item-hover" SelectedNodeStyle-CssClass="menu-item-selected"
                                    NodeStyle-HorizontalPadding="5px">
                                    <Nodes>
                                        
                                    </Nodes>
                                </asp:TreeView>
                            </div>
                        </Content>
                    </ajaxToolkit:AccordionPane>
                    <ajaxToolkit:AccordionPane ID="apConexion" runat="server" EnableViewState="False"
                        ContentCssClass="menu-item-cuerpo">
                        <Header>
                            <div class="div-header">
                                <asp:Image ID="imgConexion" runat="server" ImageUrl="~/_images/menu/conexion.png"
                                    Style="margin-right: 10px" />Conexión</div>
                        </Header>
                        <Content>
                            <div class="div-cuerpo">
                                <table>
                                    <tr>
                                        <td class="Label">
                                            Entidad
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblConexion_Entidad" runat="server" CssClass="menu-item"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 10px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Label">
                                            Usuario
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblConexion_Nombres" runat="server" CssClass="menu-item"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblConexion_Apellidos" runat="server" CssClass="menu-item"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 10px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Label">
                                            Login
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblConexion_Login" runat="server" CssClass="menu-item"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 10px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Label">
                                            Dirección IP
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblConexion_IP" runat="server" CssClass="menu-item"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 10px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Label">
                                            Versión del sistema
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblConexion_Version" runat="server" CssClass="menu-item"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </Content>
                    </ajaxToolkit:AccordionPane>
                </Panes>
            </ajaxToolkit:Accordion>
        </td>
    </tr>
</table>
