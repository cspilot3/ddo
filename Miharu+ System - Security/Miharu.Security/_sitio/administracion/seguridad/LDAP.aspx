<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MiharuMasterForm.master" CodeBehind="LDAP.aspx.vb" Inherits="Miharu.Security._sitio.administracion.seguridad.LDAP" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="Miharu.Web.Controls" Namespace="Miharu.Web.Controls" TagPrefix="Miharu" %>
<%@ Register Src="../../../_controls/wucFilter.ascx" TagName="wucFilter" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../../_styles/StyleSheet_Marco.css" rel="stylesheet" type="text/css" />
    <link href="../../../_styles/StyleSheet_Miharu.css" rel="stylesheet" type="text/css" />
    <link href="../../../_styles/StyleSheet_Menu.css" rel="stylesheet" type="text/css" />
    <link href="../../../_styles/gridview/GridviewStyles.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function ShowOpciones(Modo) {
            document.getElementById("<%= divSave.ClientID %>").style.display = Modo;
            document.getElementById("<%= divDelete.ClientID %>").style.display = Modo;
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Cuerpo" runat="server">
    <table style="width: 780px; height: 585px;" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td style="height: 10px">
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" border="0" style="width: 760px">
                    <tr>
                        <td style="width: 10px">
                            &#160;
                        </td>
                        <td style="width: 30px">
                            <div id="divAdd" runat="server" style="width: 25px">
                                <div class="BotonCambiante">
                                    <asp:ImageButton ID="ibAdd" runat="server" ImageUrl="~/_images/opciones/nuevo.png" ToolTip="Agregar un nuevo registro" />
                                </div>
                            </div>
                        </td>
                        <td style="width: 30px">
                            <div id="divDelete" runat="server" style="width: 25px; display: none">
                                <div class="BotonCambiante">
                                    <asp:ImageButton ID="ibDelete" runat="server" ImageUrl="~/_images/opciones/delete.png" ToolTip="Eliminar el registro actual" />
                                </div>
                                <ajaxToolkit:ConfirmButtonExtender ID="ibDelete_ConfirmButtonExtender" runat="server" ConfirmText="¿Confirma que desea eliminar el registro?" TargetControlID="ibDelete">
                                </ajaxToolkit:ConfirmButtonExtender>
                            </div>
                        </td>
                        <td style="width: 10px">
                            &#160;
                        </td>
                        <td style="width: 30px">
                            <div id="divSave" runat="server" style="width: 25px; display: none">
                                <div class="BotonCambiante">
                                    <asp:ImageButton ID="ibSave" runat="server" ImageUrl="~/_images/opciones/save.png" ToolTip="Guardar los cambios" ValidationGroup="Guardar" />
                                </div>
                            </div>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTitulo" runat="server" Text="LDAP" CssClass="Titulo_Principal"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 10px">
            </td>
        </tr>
        <tr>
            <td valign="top" style="padding: 0px 5px 5px 5px;" align="left">
                <ajaxToolkit:TabContainer ID="BaseTabContainer" runat="server" CssClass="ajax__tab_technorati-theme" ActiveTabIndex="0" Width="770">
                    <ajaxToolkit:TabPanel runat="server" ID="ConsultaTabPanel">
                        <HeaderTemplate>
                            <table cellpadding="0" cellspacing="0" border="0" style="height: 20px" onclick="ShowOpciones('none')">
                                <tr>
                                    <td>
                                        <asp:Image ID="imgIcoConsulta" runat="server" ImageUrl="~/_images/opciones/consulta.png" />
                                    </td>
                                    <td style="width: 5px" />
                                    <td>
                                        <asp:Label ID="lblConsulta" runat="server" Text="Consulta"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <ContentTemplate>
                            <table style="padding: 5px" cellspacing="5" border="0">
                                <tr>
                                    <td style="height: 30px">
                                        <asp:Label ID="EntidadLabel" runat="server" Text="Entidad" CssClass="Label" Width="120px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="EntidadDropDownList" runat="server" Width="300px" AutoPostBack="True" CssClass="Textbox">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="FiltroLabel" runat="server" Text="Filtro: [Grupo]" CssClass="Label" Width="120px"></asp:Label>
                                    </td>
                                    <td>
                                        <uc1:wucFilter ID="ucFiltro" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <div style="border: 1px solid #CCCCCC; overflow: auto; height: 395px; width: 735px">
                                            <Miharu:SlygGridView ID="BaseGridView" runat="server" AutoGenerateColumns="False" GridNum="0" CssClass="yui-datatable-theme" EnableSort="True" ClickAction="OnDblClickSelectedPostBack">
                                                <AlternatingRowStyle CssClass="alt-data-row"></AlternatingRowStyle>
                                                <Columns>
                                                    <asp:BoundField DataField="id_LDAP" HeaderText="Cod." ItemStyle-Width="1" />
                                                    <asp:BoundField DataField="Grupo_LDAP" HeaderText="Grupo" />
                                                    <asp:BoundField DataField="Nombre_Perfil" HeaderText="Perfil" />
                                                </Columns>
                                                <EditRowStyle CssClass="row-edit"></EditRowStyle>
                                                <PagerStyle CssClass="pager-stl"></PagerStyle>
                                                <RowStyle CssClass="nor-data-row"></RowStyle>
                                                <SelectedRowStyle CssClass="row-edit"></SelectedRowStyle>
                                            </Miharu:SlygGridView>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel ID="DetalleTabPanel" runat="server">
                        <HeaderTemplate>
                            <table cellpadding="0" cellspacing="0" border="0" style="height: 20px" onclick="ShowOpciones('inline')">
                                <tr>
                                    <td>
                                        <asp:Image ID="imgIcoDetalle" runat="server" ImageUrl="~/_images/opciones/detalle.png" />
                                    </td>
                                    <td style="width: 5px" />
                                    <td>
                                        <asp:Label ID="lblDetalle" runat="server" Text="Detalle"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <ContentTemplate>
                            <div style="margin: 5px; height: 475px; width: 737px;">
                                <asp:Panel ID="DetallePanel" runat="server" Visible="False">
                                    <table id="DetalleTable" cellspacing="0" cellpadding="0" border="0" style="margin-left: 20px">
                                        <tr>
                                            <td colspan="3" style="height: 10px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="EntidadEditLabel" runat="server" CssClass="Label">Entidad</asp:Label>
                                            </td>
                                            <td style="width: 10px">
                                            </td>
                                            <td>
                                                <asp:Label ID="NombreEntidadLabel" runat="server" CssClass="Textbox" Width="500px">Entidad</asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" style="height: 10px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="GrupoLabel" runat="server" CssClass="Label">Grupo</asp:Label>
                                            </td>
                                            <td style="width: 10px">
                                            </td>
                                            <td>
                                                <asp:Label ID="CodLDAPLabel" runat="server" CssClass="Oculto"></asp:Label>
                                                <asp:TextBox ID="GrupoTextBox" TabIndex="1" runat="server" CssClass="Textbox" Width="400px" MaxLength="500" />
                                                <asp:RequiredFieldValidator ID="GrupoRequiredFieldValidator" runat="server" ErrorMessage="&lt;b&gt;Falta campo requerido&lt;/b&gt;&lt;br /&gt;Se requiere el Grupo del LDAP." Display="None" ValidationGroup="Guardar" SetFocusOnError="True" ControlToValidate="GrupoTextBox"></asp:RequiredFieldValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="GrupoRequiredFieldValidator_ValidatorCalloutExtender" runat="server" TargetControlID="GrupoRequiredFieldValidator" Enabled="True">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" style="height: 8px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 14px">
                                                <asp:Label ID="PerfilLabel" runat="server" CssClass="Label">Perfil</asp:Label>
                                            </td>
                                            <td style="height: 14px">
                                            </td>
                                            <td style="height: 14px">
                                                <asp:DropDownList ID="PerfilDropDownList" runat="server" CssClass="Textbox" Width="400px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </div>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                </ajaxToolkit:TabContainer>
            </td>
        </tr>
    </table>
</asp:Content>
