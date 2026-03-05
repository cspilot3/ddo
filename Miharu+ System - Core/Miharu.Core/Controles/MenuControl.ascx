<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="MenuControl.ascx.vb" Inherits="Miharu.Core.Controles.MenuControl" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<table style="width: 195px;" border="0" cellpadding="0" cellspacing="0">
    <tr>
        <td style="width: 200px; height: 60px;" valign="middle">
            <asp:Image ID="imgLogoCliente" runat="server" Height="60px" Width="170px" />
        </td>
    </tr>
    <tr>
        <td style="width: 170px; height: 20px" class="Titulo_Menu">Menú</td>
    </tr>
    <%--<tr style="height: 5px;">
        <td>
        </td>
    </tr>--%>

    <tr>
        <td valign="top" style="border-style: none solid solid none; border-width: 1px; border-color: #666666; background-color: #EDEDED;">
            
            <ajaxToolkit:Accordion ID="AccordionMenu" runat="server" SelectedIndex="0" HeaderCssClass="menu-header"
                HeaderSelectedCssClass="menu-header-selected" ContentCssClass="menu-body" FadeTransitions="false"
                FramesPerSecond="40" TransitionDuration="250" RequireOpenedPane="True" SuppressHeaderPostbacks="true"
                BorderStyle="None" Width="220px" Height="412px" EnableViewState="False" AutoSize="Limit">
                
                <Panes>
                    <%--Administración--%>
                    <ajaxToolkit:AccordionPane ID="apAdministarcion" runat="server" EnableViewState="true" Height="150px" ContentCssClass="menu-item-cuerpo">
                        <Header>
                            <div id="hAdministracion" runat="server" class="div-header">
                                <asp:Image ID="imgAdministracion" runat="server" ImageUrl="~/_images/menu/Administracion.png" Style="margin-right: 10px"/>&nbsp;Administración
                            </div>
                        </Header>
                        
                        <Content>
                            <asp:UpdatePanel ID="upAdministracion" runat="server">
                                <ContentTemplate>
                                    <div class="div-cuerpo">
                                        <asp:TreeView ID="tvAdministracion" runat="server" ImageSet="Simple2"  NodeStyle-HorizontalPadding="5px" Font-Size="X-Small">
                                            <Nodes>
                                                <%--Proyecto--%>
                                                <asp:TreeNode ImageUrl="~/_images/menu/Proyecto.png" Text="Proyecto" Value="1.1" ToolTip="Administrar Proyectos" Expanded="false" />
                                                <%--Esquema--%>
                                                <asp:TreeNode ImageUrl="~/_images/menu/Esquema.png" Text="Esquema" Value="1.2" ToolTip="Administrar Esquemas" Expanded="false" />
                                                <%--Documento
                                                <asp:TreeNode ImageUrl="~/_images/menu/Documento.png" Text="Documento" Value="1.3" ToolTip="Administrar Documentos" Expanded="false" />--%>
                                                
                                                <%--Parámetros--%>
                                                <asp:TreeNode ImageUrl="~/_images/menu/Parametros.png" Text="Parámetros" Value="1.4" ToolTip="Parámetros" Expanded="false">
                                                    <asp:TreeNode ImageUrl="~/_images/menu/FolderTipo.png" Text="Tipo Folder (Custodia)" Value="1.4.9" ToolTip="Administrar Folders" Expanded="false" />
                                                    <asp:TreeNode ImageUrl="~/_images/menu/Caja.png" Text="Tipo Caja" Value="1.4.2" ToolTip="Administrar Tipo Cajas" Expanded="false" />
                                                    <asp:TreeNode ImageUrl="~/_images/menu/Tipologia.png" Text="Tipología" Value="1.4.5" ToolTip="Administrar Tipologías" Expanded="false" />
                                                    <asp:TreeNode ImageUrl="~/_images/menu/CampoLista.png" Text="Campo Lista" Value="1.4.6" ToolTip="Administrar Campo Listas" Expanded="false" />
                                                    <%--<asp:TreeNode ImageUrl="~/_images/menu/TablaAsociada.png" Text="Tabla Asociada" Value="1.4.8" ToolTip="Administrar Tabla Asociada" Expanded="false" />--%>
                                                    <asp:TreeNode ImageUrl="~/_images/menu/Caja.png" Text="Caja proceso" Value="1.4.10" ToolTip="Administrar Cajas de Proceso" Expanded="false" />
                                                </asp:TreeNode>

                                                <%--Servidor Imágenes--%>
                                                <asp:TreeNode ImageUrl="~/_images/menu/ServidorImagenes.png" Text="Servidor Imágenes" Value="1.5" ToolTip="Servidor Imágenes" Expanded="false">
                                                    <asp:TreeNode ImageUrl="~/_images/menu/Servidor.png" Text="Servidor" Value="1.5.2" ToolTip="Administrar Servidores" Expanded="false" />
                                                    <asp:TreeNode ImageUrl="~/_images/menu/VolumenServidor.png" Text="Volumen Servidor" Value="1.5.3" ToolTip="Administrar Volumen Servidor" Expanded="false" />
                                                </asp:TreeNode>
                                                
                                                <%--campos--%>
                                                <asp:TreeNode ImageUrl="~/_images/menu/campo.png" Text="Campos" Value="1.6" ToolTip="Administrar Campos" Expanded="false" />

                                                <%--Validacion de documentos--%>
                                                <asp:TreeNode ImageUrl="~/_images/menu/Validar.png" Text="Validaciones Documentos" Value="1.7" ToolTip="Administrar Validaciones de Documentos" Expanded="false" />

                                                <%--Roles + Security--%>
                                                <asp:TreeNode ImageUrl="~/_images/menu/rol.png" Text="Roles" Value="1.8" ToolTip="Administrar roles usuarios" Expanded="false" />

                                            </Nodes>
                                        </asp:TreeView>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </Content>
                    </ajaxToolkit:AccordionPane>


                    <%--Plantillas Estante--%>
                    <ajaxToolkit:AccordionPane ID="apPlantillasEstante" runat="server" EnableViewState="true" Height="150px" ContentCssClass="menu-item-cuerpo">
                        <Header>
                            <div id="hPlantillasEstante" runat="server" class="div-header">
                                <asp:Image ID="imgPlantillasEstante" runat="server" ImageUrl="~/_images/menu/PlantillaEstante.png" Style="margin-right: 10px"/>&nbsp;Plantillas Estante
                            </div>
                        </Header>

                        <Content>
                            <asp:UpdatePanel ID="upPlantillasEstante" runat="server">
                                <ContentTemplate>
                                    <div class="div-cuerpo">
                                        <%--Plantillas Estante--%>
                                        <asp:TreeView ID="tvPlantillasEstante" runat="server" ImageSet="Simple2"  NodeStyle-HorizontalPadding="5px" Font-Size="X-Small">
                                            <Nodes>
                                                <asp:TreeNode ImageUrl="~/_images/menu/Profundidad.png" Text="Plantilla Estante" Value="2.1" ToolTip="Administrar Plantillas Estantes" Expanded="false" />
                                                <%--<asp:TreeNode ImageUrl="~/_images/menu/Profundidad.png" Text="Profundidad" Value="2.2" ToolTip="Administrar Profundidades" Expanded="false" />
                                                <asp:TreeNode ImageUrl="~/_images/menu/Fila.png" Text="Fila" Value="2.3" ToolTip="Administrar Filas" Expanded="false" />
                                                <asp:TreeNode ImageUrl="~/_images/menu/Columna.png" Text="Columna" Value="2.4" ToolTip="Administrar Columnas" Expanded="false" />--%>
                                            </Nodes>
                                        </asp:TreeView>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </Content>
                    </ajaxToolkit:AccordionPane>

                    <%--Bóveda--%>
                    <ajaxToolkit:AccordionPane ID="apBoveda" runat="server" EnableViewState="true" Height="150px" ContentCssClass="menu-item-cuerpo">
                        <Header>
                            <div id="hBoveda" runat="server" class="div-header">
                                <asp:Image ID="imgBoveda" runat="server" ImageUrl="~/_images/menu/Boveda.png" Style="margin-right: 10px"/>&nbsp;Bóveda
                            </div>
                        </Header>

                        <Content>
                            <asp:UpdatePanel ID="upBoveda" runat="server">
                                <ContentTemplate>
                                    <div class="div-cuerpo">
                                        <%--Bóveda--%>
                                        <asp:TreeView ID="tvBoveda" runat="server" ImageSet="Simple2"  NodeStyle-HorizontalPadding="5px" Font-Size="X-Small">
                                            <Nodes>
                                                <asp:TreeNode ImageUrl="~/_images/menu/Boveda.png" Text="Bóveda" Value="3.1" ToolTip="Administrar Bóvedas" Expanded="false" />
                                                <asp:TreeNode ImageUrl="~/_images/menu/Barcode.png" Text="Impresión C. Barras" Value="3.2" ToolTip="Impresión de códigos de barras" Expanded="false" />
                                            </Nodes>
                                        </asp:TreeView>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </Content>
                    </ajaxToolkit:AccordionPane>

                    <%--TRD--%>
                    <ajaxToolkit:AccordionPane ID="apTRD" runat="server" EnableViewState="true" Height="150px" ContentCssClass="menu-item-cuerpo">
                    <Header>
                        <div id="hTRD" runat="server" class="div-header">
                                <asp:Image ID="imgTRD" runat="server" ImageUrl="~/_images/menu/TRD.png"  Style="margin-right: 10px"/>&nbsp;TRD
                            </div>
                    </Header>
                    <Content>
                        <asp:UpdatePanel ID="upTRD" runat="server">
                            <ContentTemplate>
                                <div class="div-cuerpo">
                                    <asp:TreeView ID="tvTRD" runat="server" ImageSet="Simple2"  NodeStyle-HorizontalPadding="5px" Font-Size="X-Small">
                                            <Nodes>
                                                <asp:TreeNode ImageUrl="~/_images/menu/TRD.png" Text="TRD" Value="5.1" ToolTip="Administrar TRD" Expanded="false" />
                                                <asp:TreeNode ImageUrl="~/_images/menu/Serie.png" Text="Serie" Value="5.2" ToolTip="Administrar Series" Expanded="false" />
                                                <asp:TreeNode ImageUrl="~/_images/menu/SubSerie.png" Text="Subserie" Value="5.3" ToolTip="Administrar Sub Series" Expanded="false" />
                                            </Nodes>
                                    </asp:TreeView>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </Content>
                    </ajaxToolkit:AccordionPane>

                    <%--Búsqueda--%>
                    <ajaxToolkit:AccordionPane ID="apBusqueda" runat="server" EnableViewState="true" Height="150px" ContentCssClass="menu-item-cuerpo">
                    <Header>
                        <div id="hBusqueda" runat="server" class="div-header">
                                <asp:Image ID="imgBusqueda" runat="server" ImageUrl="~/_images/menu/Busqueda.png"  Style="margin-right: 10px"/>&nbsp;Búsqueda
                            </div>
                    </Header>
                    <Content>
                        <asp:UpdatePanel ID="upBusqueda" runat="server">
                            <ContentTemplate>
                                <div class="div-cuerpo">
                                    <asp:TreeView ID="tvBusqueda" runat="server" ImageSet="Simple2"  NodeStyle-HorizontalPadding="5px" Font-Size="X-Small">
                                            <Nodes>
                                                <asp:TreeNode ImageUrl="~/_images/menu/CampoBusqueda.png" Text="Campo Búsqueda" Value="6.1" ToolTip="Administrar Campo Búsqueda" Expanded="false" />
                                                <asp:TreeNode ImageUrl="~/_images/menu/CampoBusquedaEntidad.png" Text="Campo Búsqueda Entidad" Value="6.2" ToolTip="Administrar Campo Búsqueda Entidad" Expanded="false" />
                                            </Nodes>
                                    </asp:TreeView>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </Content>
                    </ajaxToolkit:AccordionPane>

                    <%--Reportes--%>
                    <ajaxToolkit:AccordionPane ID="apReporte" runat="server" EnableViewState="true" Height="150px" ContentCssClass="menu-item-cuerpo">
                    <Header>
                        <div id="hReporte" runat="server" class="div-header">
                                <asp:Image ID="imgReporte" runat="server" ImageUrl="~/_images/menu/Report.png"  Style="margin-right: 10px"/>&nbsp;Reportes
                            </div>
                    </Header>
                    <Content>
                        <asp:UpdatePanel ID="upReporte" runat="server">
                            <ContentTemplate>
                                <div class="div-cuerpo">
                                    <asp:TreeView ID="tvReporte" runat="server" ImageSet="Simple2"  NodeStyle-HorizontalPadding="5px" Font-Size="X-Small">
                                            <Nodes>
                                                <asp:TreeNode ImageUrl="~/_images/menu/Database.png" Text="Conexiones" Value="7.1" ToolTip="Administrar Conexiones a Servidores" Expanded="false" />
                                                <asp:TreeNode ImageUrl="~/_images/menu/Category.png" Text="Categorias" Value="7.2" ToolTip="Administrar Categorias Reportes" Expanded="false" />
                                                <asp:TreeNode ImageUrl="~/_images/menu/Query.png" Text="Reportes" Value="7.3" ToolTip="Administrar Reportes" Expanded="false" />
                                            </Nodes>
                                    </asp:TreeView>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </Content>
                    </ajaxToolkit:AccordionPane>

                </Panes>
            </ajaxToolkit:Accordion>
        </td>
    </tr>
</table> 