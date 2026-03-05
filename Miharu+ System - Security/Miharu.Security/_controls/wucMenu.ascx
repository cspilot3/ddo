<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="wucMenu.ascx.vb" Inherits="Miharu.Security._controls.wucMenu" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
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
        <td valign="top" style="border-style: none solid solid none; border-width: 1px; border-color: #666666; background-color: #EDEDED; height: 500px;">
            <ajaxToolkit:Accordion ID="AccordionMenu" runat="server" SelectedIndex="0" HeaderCssClass="menu-header" HeaderSelectedCssClass="menu-header-selected" ContentCssClass="menu-body" FadeTransitions="false" FramesPerSecond="40" TransitionDuration="250" AutoSize="None"
                RequireOpenedPane="True" SuppressHeaderPostbacks="true" BorderStyle="None" Width="200px" EnableViewState="False">
                <Panes>
                    <ajaxToolkit:AccordionPane ID="apAdministracion" runat="server" EnableViewState="False" ContentCssClass="menu-item-cuerpo">
                        <Header>
                            <div class="div-header">
                                <asp:Image ID="imgAdministracion" runat="server" ImageUrl="~/_images/menu/Administracion.png" Style="margin-right: 10px" />Administración</div>
                        </Header>
                        <Content>
                            <div class="div-cuerpo">
                                <asp:TreeView ID="tvAdministracion" runat="server" ImageSet="Arrows" NodeStyle-CssClass="menu-item" HoverNodeStyle-CssClass="menu-item-hover" SelectedNodeStyle-CssClass="menu-item-selected" NodeStyle-HorizontalPadding="5px">
                                    <Nodes>
                                        <asp:TreeNode ImageUrl="~/_images/menu/Estructura.png" Text="Estructura" Value="1.1" ToolTip="Configuración base" Expanded="False">
                                            <asp:TreeNode ImageUrl="~/_images/menu/GrupoEmpresarial.png" Text="Grp. empresarial" Value="1.1.1" ToolTip="Administra los grupos empresariales" />
                                            <asp:TreeNode ImageUrl="~/_images/menu/Entidad.png" Text="Entidades" Value="1.1.2" ToolTip="Administra las entidades"></asp:TreeNode>
                                            <asp:TreeNode ImageUrl="~/_images/menu/Dependencia.png" Text="Dependencias" Value="1.1.3" ToolTip="Administra las dependencias" />
                                            <%--  Adiconal menu---Lady--%>
                                            <asp:TreeNode ImageUrl="~/_images/menu/Ciudad.png" Text="Regionales" Value="1.1.4" ToolTip="Administra las regionales" />
                                            <asp:TreeNode ImageUrl="~/_images/menu/Ciudad.png" Text="Pais" Value="1.1.5" ToolTip="Administra los paises" />
                                            <asp:TreeNode ImageUrl="~/_images/menu/Ciudad.png" Text="Regiones" Value="1.1.6" ToolTip="Administra las regiones" />
                                            <%--  -------------------------------%>
                                            <asp:TreeNode ImageUrl="~/_images/menu/Ciudad.png" Text="Ciudades" Value="1.1.7" ToolTip="Administra las ciudades" />
                                            <asp:TreeNode ImageUrl="~/_images/menu/Sede.png" Text="Sedes" Value="1.1.8" ToolTip="Administra las sedes de las entidades" />
                                            <asp:TreeNode ImageUrl="~/_images/menu/puestotrabajo.png" Text="P. de Trabajo" Value="1.1.9" ToolTip="Administra los puestos de trabajo" />
                                            <asp:TreeNode ImageUrl="~/_images/menu/centroprocesamiento.png" Text="C. Procesamiento" Value="1.1.10" ToolTip="Administra los centros de procesamiento" />
                                            <asp:TreeNode ImageUrl="~/_images/menu/calendar.png" Text="Calendario" Value="1.1.11" ToolTip="Administra el calendario" />
                                        </asp:TreeNode>
                                        <asp:TreeNode ImageUrl="~/_images/menu/Seguridad.png" Text="Seguridad" Value="1.2" ToolTip="Configuración de seguridad" Expanded="False">
                                            <asp:TreeNode ImageUrl="~/_images/menu/Modulos.png" Text="Módulos" Value="1.2.1" ToolTip="Administra los módulos del sistema" />
                                            <asp:TreeNode ImageUrl="~/_images/menu/Perfiles.png" Text="Perfiles" Value="1.2.2" ToolTip="Administra los perfiles" />
                                            <asp:TreeNode ImageUrl="~/_images/menu/Roles.png" Text="Roles" Value="1.2.7" ToolTip="Administra los roles" />
                                            <asp:TreeNode ImageUrl="~/_images/menu/EsquemasSeguridad.png" Text="Esquemas" Value="1.2.3" ToolTip="Administra los esquemas de seguridad" />
                                            <asp:TreeNode ImageUrl="~/_images/menu/Usuarios.png" Text="Usuarios" Value="1.2.4" ToolTip="Administra los usuarios" />
                                            <asp:TreeNode ImageUrl="~/_images/menu/Parametros.png" Text="Parámetros" Value="1.2.5" ToolTip="Administra los parámetros generales" />
                                            <asp:TreeNode ImageUrl="~/_images/menu/IPBloqueada.png" Text="IPs bloqueadas" Value="1.2.6" ToolTip="Administra las direcciones IP bloqueadas" />
                                            <asp:TreeNode ImageUrl="~/_images/menu/LDAP.png" Text="LDAP" Value="1.2.8" ToolTip="Administra la sincronización con LDAP" />
                                        </asp:TreeNode>
                                    </Nodes>
                                </asp:TreeView>
                            </div>
                        </Content>
                    </ajaxToolkit:AccordionPane>
                    <ajaxToolkit:AccordionPane ID="apInformes" runat="server" EnableViewState="False" ContentCssClass="menu-item-cuerpo">
                        <Header>
                            <div class="div-header">
                                <asp:Image ID="imgInformes" runat="server" ImageUrl="~/_images/menu/Informes.png" Style="margin-right: 10px" />Informes</div>
                        </Header>
                        <Content>
                            <div class="div-cuerpo">
                                <asp:TreeView ID="tvInformes" runat="server" ImageSet="Arrows" NodeStyle-CssClass="menu-item" HoverNodeStyle-CssClass="menu-item-hover" SelectedNodeStyle-CssClass="menu-item-selected" NodeStyle-HorizontalPadding="5px">
                                    <Nodes>
                                    </Nodes>
                                </asp:TreeView>
                            </div>
                        </Content>
                    </ajaxToolkit:AccordionPane>
                    <ajaxToolkit:AccordionPane ID="apConexion" runat="server" EnableViewState="False" ContentCssClass="menu-item-cuerpo">
                        <Header>
                            <div class="div-header">
                                <asp:Image ID="imgConexion" runat="server" ImageUrl="~/_images/menu/conexion.png" Style="margin-right: 10px" />Conexión</div>
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
