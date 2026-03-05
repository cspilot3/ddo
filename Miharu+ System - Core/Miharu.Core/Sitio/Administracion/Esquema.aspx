<%@ Page UICulture="es" Culture="es-MX" Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Main/FormMasterPage.Master" CodeBehind="Esquema.aspx.vb"
    Inherits="Miharu.Core.Sitio.Administracion.Esquema" %>

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
    <style type="text/css">
        .style1
        {
            height: 20px;
        }
        .style2
        {
            width: 714px;
        }
        .style3
        {
            width: 708px;
        }
        .style4
        {
            width: 162px;
        }
        .auto-style5 {
            width: 185px;
        }
        .auto-style7 {
            width: 173px;
        }
        .auto-style9 {
            width: 93px;
        }
        .auto-style10 {
            width: 185px;
            height: 24px;
        }
        .auto-style11 {
            width: 93px;
            height: 24px;
        }
        .auto-style12 {
            width: 173px;
            height: 24px;
        }
        .auto-style13 {
            height: 24px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MasterBodyUnique" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MasterFilter" runat="server">
    <asp:Panel ID="pnlFiltro" runat="server" Style="width: 100%;" Visible="true">
        <table style="width: 100%;">
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Entidad" CssClass="Label"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="find_fk_Entidad" runat="server" AutoPostBack="true">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Proyecto" CssClass="Label"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="find_fk_proyecto" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="Esquema" CssClass="Label"></asp:Label>
                </td>
                <td>
                    <cc1:DTexto ID="find_nombre_esquema" runat="server" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MasterGrid" runat="server">
    <asp:Panel ID="pnlGrilla" runat="server" Style="width: 100%;">
        <asp:Label ID="NumRegistros" runat="server" Text="Label" CssClass="Label"></asp:Label>
        <asp:HiddenField ID="PreSelectedIndex" runat="server" />
        <br />
        <br />
        <cc1:CoreGridView ID="grdData" runat="server" AutoGenerateColumns="false" OnEndPreSelect="OnPreselectMasterGrid">
            <Columns>
                <asp:BoundField HeaderText="Entidad" DataField="fk_Entidad" />
                <asp:BoundField HeaderText="Proyecto" DataField="fk_Proyecto" />
                <asp:BoundField HeaderText="Esquema" DataField="id_Esquema" />
                <asp:BoundField HeaderText="Nombre Esquema" DataField="Nombre_Esquema" />
                <asp:BoundField HeaderText="Entidad Servidor" DataField="fk_Entidad_Servidor" />
                <asp:BoundField HeaderText="Servidor" DataField="fk_Servidor" />
                <%--<asp:BoundField HeaderText="Servidor Volumen" DataField="fk_Servidor_Volumen" />--%>
                <asp:BoundField HeaderText="Restriccion Monto" DataField="fk_Restriccion_Monto" />
                <asp:BoundField HeaderText="Valor Restriccion Monto" DataField="Valor_Restriccion_Monto" />
                <asp:BoundField HeaderText="Documento 1" DataField="fk_Documento_1" />
                <asp:BoundField HeaderText="Campo 1" DataField="fk_Campo_1" />
                <asp:BoundField HeaderText="Documento 2" DataField="fk_Documento_2" />
                <asp:BoundField HeaderText="Campo 2" DataField="fk_Campo_2" />
                <asp:BoundField HeaderText="Documento 3" DataField="fk_Documento_3" />
                <asp:BoundField HeaderText="Campo 3" DataField="fk_Campo_3" />
                <asp:BoundField HeaderText="Indexación Automática" DataField="Indexacion_Automatica" />
                <asp:BoundField HeaderText="Usa Notificación Cargue" DataField="UsaNotificacionCargue" />
                <asp:BoundField HeaderText="Notificación" DataField="fk_Notificacion" />
                <asp:BoundField HeaderText="Cargue x Documento" DataField="usa_Cargue_xDocumento" />
                <asp:BoundField HeaderText="Parámetro Escáner" DataField="fk_Parametro_Escaner" />
                <asp:BoundField HeaderText="Formato Archivo Cargue" DataField="formato_Archivo_Cargue" />
                <asp:BoundField HeaderText="Peso Imagen Blanca bits" DataField="PesoImagenenBlanco" />
                <asp:BoundField HeaderText="Asociación arch. tipología" DataField="usa_NombreArchivo_Asociado_Tipologia" />
            </Columns>
        </cc1:CoreGridView>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="MasterDetail" runat="server">
    <asp:Panel ID="pnlDetalle" runat="server" Style="width: 100%;" Visible="true" ScrollBars="Auto">
        <table style="width: 100%; height: 373px;">
            <tr>
                <td>
                    <table class="style2">
                        <tr>
                            <td class="style3">
                                <cc2:TabContainer ID="TabDetail" runat="server" ActiveTabIndex="0" CssClass="yui-datatable-theme" Height="380px" Width="689px" ScrollBars="Both">
                                    <cc2:TabPanel ID="panelDetalle" runat="server" HeaderText="Esquema">
                                        <ContentTemplate>
                                            <asp:Panel ID="Panel4" runat="server" Height="310px">
                                                <table style="width: 100%; margin-bottom: 0;">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label5" runat="server" CssClass="Label" Text="Entidad" Width="200px" />
                                                            <asp:DropDownList ID="fk_entidad" runat="server" AutoPostBack="True" Height="20px" Width="184px" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label18" runat="server" CssClass="Label" Text="Proyecto" Width="200px" />
                                                            <asp:DropDownList ID="fk_proyecto" runat="server" Height="20px" Width="184px" AutoPostBack="True" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label19" runat="server" CssClass="Label" Text="Id Esquema" Width="200px" />
                                                            <cc1:DNumber ID="id_esquema" runat="server" AceptaPuntoFlotante="False" AutoPostBack="False" BackColor_="'#EAE8E3'" CssClass_="" EmptyValueMessage="  *" Enabled="False" Heigth="" InvalidValueMessage="" IsRange="False" IsRequiered="False" MaximumValue="0" MaxLength="0" MensajeColor="Red" MinimumValue="0" Multiline="SingleLine" Text="" TooltipMessage="" TypeDB="Custom" ValidationGroup="Guardar" WaterText="" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label20" runat="server" CssClass="Label" Text="Nombre" Width="200px"></asp:Label>
                                                            <cc1:DTexto ID="Nombre_esquema" runat="server" AutoPostBack="False" BackColor_="" CssClass_="" EmptyValueMessage="  *" Heigth="" InvalidValueMessage="  El dato no es valido"
                                                                IsRequiered="True" MaxLength="0" MensajeColor="Red" Multiline="SingleLine" Text="" TooltipMessage="" ValidationGroup="Guardar" WaterText="" Width="300px" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label21" runat="server" CssClass="Label" Text="Servidor" Width="200px" />
                                                            <cc1:DTexto ID="Servidor" runat="server" AutoPostBack="False" BackColor_="" CssClass_="" EmptyValueMessage="Debe seleccionar un servidor." Enabled="False"
                                                                Heigth="" InvalidValueMessage="  El dato no es valido" IsRequiered="True" MaxLength="0" MensajeColor="Red" Multiline="SingleLine" Text="" TooltipMessage=""
                                                                ValidationGroup="Guardar" WaterText="Sin seleccionar" />
                                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/_images/find.png" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label26" runat="server" CssClass="Label" Text="Restriccion Monto" Width="200px" />
                                                            <asp:DropDownList ID="fk_Restriccion_Monto" runat="server" AutoPostBack="True" Height="20px" Width="184px" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label27" runat="server" CssClass="Label" Text="Valor Restriccion Monto" Width="200px" />
                                                            <cc1:DNumber ID="Valor_Restriccion_Monto" runat="server" AceptaPuntoFlotante="True" MaxLength="18" AutoPostBack="False" BackColor_="" CssClass_="" EmptyValueMessage="  *"
                                                                Heigth="" InvalidValueMessage="  El dato no es válido" IsRange="False" IsRequiered="False" MensajeColor="" Multiline="SingleLine" Text="" TooltipMessage="  Ingrese un valor numérico"
                                                                TypeDB="Custom" ValidationGroup="" WaterText="" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table style="border-style: none; border-color: inherit; border-width: 1px; width: 94%; ">
                                                                <tr>
                                                                    <td class="auto-style5">
                                                                        Parametro Escaner<td class="auto-style9">
                                                                            <cc1:DTexto ID="fk_Parametro_Escaner" runat="server" AutoPostBack="False" BackColor_="" CssClass_="" EmptyValueMessage="" Enabled="False" Heigth="" InvalidValueMessage="  El dato no es valido" IsRequiered="True" MaxLength="0" MensajeColor="Red" Multiline="SingleLine" Text="" TooltipMessage="" ValidationGroup="Guardar" WaterText="Sin seleccionar" />
                                                                            <td class="auto-style7">Formatos archivo Cargue</td>
                                                                            <td>
                                                                                <cc1:DTexto ID="formato_Archivo_Cargue" runat="server" AutoPostBack="False" BackColor_="" CssClass_="" EmptyValueMessage="" Enabled="False" Heigth="" InvalidValueMessage="  El dato no es valido" IsRequiered="True" MaxLength="0" MensajeColor="Red" Multiline="SingleLine" Text="" TooltipMessage="" ValidationGroup="Guardar" WaterText="Sin seleccionar" />
                                                                            </td>
                                                                        </td>
                                                                        </tr>
                                                                <tr>
                                                                    <td class="auto-style5">
                                                                        Indexación Automática</td>
                                                                    <td class="auto-style9">
                                                                        <asp:CheckBox ID="Indexacion_Automatica" runat="server" Height="20px" Text=" " ToolTip="Indexación Automatica" Width="20px" />
                                                                    </td>
                                                                    <td class="auto-style7">
                                                                        Usa notificación Cargue</td>
                                                                    <td>
                                                                        <asp:CheckBox ID="UsaNotificacionCargue" runat="server" Height="20px" Text=" " ToolTip="Usa notificación cargue" Width="20px" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="auto-style5">
                                                                        Peso imagen Blanca bits</td>
                                                                    <td class="auto-style9">
                                                                        <cc1:DTexto ID="PesoImagenenBlanco" runat="server" AutoPostBack="False" BackColor_="" CssClass_="" EmptyValueMessage="" Enabled="False" Heigth="" InvalidValueMessage="  El dato no es valido" IsRequiered="True" MaxLength="0" MensajeColor="Red" Multiline="SingleLine" Text="" TooltipMessage="" ValidationGroup="Guardar" WaterText="Sin seleccionar" />
                                                                    </td>
                                                                    <td class="auto-style7">
                                                                        Notificación Cargue</td>
                                                                    <td>
                                                                        <asp:DropDownList ID="fk_Notificacion" runat="server" AutoPostBack="True" Height="16px" Width="134px">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="auto-style10">
                                                                        Usa cargue x Documento</td>
                                                                    <td class="auto-style11">
                                                                        <asp:CheckBox ID="usa_Cargue_xDocumento" runat="server" Height="20px" Text=" " ToolTip="Usa cargue por documento" Width="20px" />
                                                                    </td>
                                                                    <td class="auto-style12">
                                                                        Asociación arch. tipologia</td>
                                                                    <td class="auto-style13">
                                                                        <asp:CheckBox ID="usa_NombreArchivo_Asociado_Tipologia" runat="server" AutoPostBack="True" Height="20px" Text=" " ToolTip="Usa asociación nombre archivo con tipologia." Width="20px" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="display: none">
                                                            <cc1:DNumber ID="fk_entidad_servidor" runat="server" Enabled="False" Width="0px" AceptaPuntoFlotante="False" AutoPostBack="False" BackColor_="'#EAE8E3'"
                                                                CssClass_="" EmptyValueMessage="  *" Heigth="" InvalidValueMessage="  El dato no es válido" IsRange="False" IsRequiered="False" MaxLength="0" MensajeColor=""
                                                                Multiline="SingleLine" Text="" TooltipMessage="  Ingrese un valor numérico" TypeDB="Custom" ValidationGroup="" WaterText="" />
                                                            <cc1:DNumber ID="fk_servidor" runat="server" Enabled="False" Width="0px" AceptaPuntoFlotante="False" AutoPostBack="False" BackColor_="'#EAE8E3'" CssClass_=""
                                                                EmptyValueMessage="  *" Heigth="" InvalidValueMessage="  El dato no es válido" IsRange="False" IsRequiered="False" MaxLength="0" MensajeColor="" Multiline="SingleLine"
                                                                Text="" TooltipMessage="  Ingrese un valor numérico" TypeDB="Custom" ValidationGroup="" WaterText="" />
                                                            <cc1:DNumber ID="fk_servidor_volumen" runat="server" Enabled="False" Width="0px" AceptaPuntoFlotante="False" AutoPostBack="False" BackColor_="'#EAE8E3'"
                                                                CssClass_="" EmptyValueMessage="  *" Heigth="" InvalidValueMessage="  El dato no es válido" IsRange="False" IsRequiered="False" MaxLength="0" MensajeColor=""
                                                                Multiline="SingleLine" Text="" TooltipMessage="  Ingrese un valor numérico" TypeDB="Custom" ValidationGroup="" WaterText="" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <br />
                                            </asp:Panel>
                                        </ContentTemplate>
                                    </cc2:TabPanel>
                                    <cc2:TabPanel ID="TabPanel2" runat="server" HeaderText="Documentos">
                                        <ContentTemplate>
                                            <asp:Panel ID="Panel2" runat="server" Height="256px">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label24" runat="server" CssClass="Label" Text="Tipo documento" Width="150px"></asp:Label>
                                                            <asp:DropDownList ID="fk_tipologia" runat="server" Width="170px">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label25" runat="server" CssClass="Label" Text="Nombre documento" Width="150px"></asp:Label>
                                                            <cc1:DTexto ID="nombre_documento" runat="server" AutoPostBack="False" BackColor_="" CssClass_="" EmptyValueMessage="  *" Heigth="" InvalidValueMessage="  El dato no es valido"
                                                                IsRequiered="True" MaxLength="0" MensajeColor="Red" Multiline="SingleLine" Text="" TooltipMessage="" ValidationGroup="Documentos" WaterText="" Width="150px" />
                                                            <asp:ImageButton ID="grd_documentos_add" runat="server" ImageUrl="~/_images/basic/check.png" ValidationGroup="Documentos" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <cc1:CoreGridView ID="grd_Documentos" runat="server" ClickAction="OnDblClickSelectedPostBack" CssClass="yui-datatable-theme" EnableSort="True" GridNum="0"
                                                                OnBeginPreSelect="" OnBeginSelect="" OnEndPreSelect="" OnEndSelect="" PreSelectedIndex="-1" PreSelectedStyleCssClass="row-PreSelect">
                                                                <AlternatingRowStyle CssClass="alt-data-row" />
                                                                <EditRowStyle CssClass="row-edit" />
                                                                <PagerStyle CssClass="pager-stl" />
                                                                <RowStyle CssClass="nor-data-row" />
                                                                <SelectedRowStyle CssClass="row-Select" />
                                                                <Columns>
                                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Eliminar" ItemStyle-Width="40">
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton runat="server" ImageUrl="~/_images/basic/delete.png" ImageAlign="Middle" ID="imgEliminarItem1" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </cc1:CoreGridView>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </ContentTemplate>
                                    </cc2:TabPanel>
                                    <cc2:TabPanel ID="TabPanel3" runat="server" HeaderText="Llaves Esquema">
                                        <ContentTemplate>
                                            <asp:Panel runat="server" Height="204px">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label15" runat="server" CssClass="Label" Text="Proyecto llave" Width="150px"></asp:Label>
                                                            <asp:DropDownList ID="fk_proyecto_llave" runat="server" Width="182px">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label16" runat="server" CssClass="Label" Text="Documento" Width="150px"></asp:Label>
                                                            <asp:DropDownList ID="fk_documento" runat="server" AutoPostBack="True" Width="182px">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label17" runat="server" CssClass="Label" Text="Campo" Width="150px"></asp:Label>
                                                            <asp:DropDownList ID="fk_campo" runat="server" Width="182px">
                                                            </asp:DropDownList>
                                                            <asp:ImageButton ID="grd_llaves_add" runat="server" ImageUrl="~/_images/basic/check.png" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style1">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <cc1:CoreGridView ID="grd_llaves" runat="server" ClickAction="OnDblClickSelectedPostBack" CssClass="yui-datatable-theme" EnableSort="True" GridNum="0"
                                                                OnBeginPreSelect="" OnBeginSelect="" OnEndPreSelect="" OnEndSelect="" PreSelectedIndex="-1" PreSelectedStyleCssClass="row-PreSelect">
                                                                <AlternatingRowStyle CssClass="alt-data-row" />
                                                                <EditRowStyle CssClass="row-edit" />
                                                                <PagerStyle CssClass="pager-stl" />
                                                                <RowStyle CssClass="nor-data-row" />
                                                                <SelectedRowStyle CssClass="row-Select" />
                                                                <Columns>
                                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Eliminar" ItemStyle-Width="40">
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton runat="server" ImageUrl="~/_images/basic/delete.png" ImageAlign="Middle" ID="imgEliminarItem" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </cc1:CoreGridView>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </ContentTemplate>
                                    </cc2:TabPanel>
                                    <cc2:TabPanel ID="TabPanelAsociacion" runat="server" HeaderText="Identificación para Asociación">
                                        <ContentTemplate>
                                            <br />
                                            <table style="width: 100%; border: 1px;">
                                                <tr>
                                                    <td><strong>Seleccione tipologia</strong><td colspan="2">
                                                        <asp:DropDownList ID="fk_Tipologia0" runat="server" AutoPostBack="True" Height="16px" Width="377px">
                                                        </asp:DropDownList>
                                                        <asp:ImageButton ID="grd_llaves_add0" runat="server" ImageUrl="~/_images/basic/check.png" Width="18px" />
                                                        </td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td><strong>Inicia por (=)</strong></td>
                                                    <td><b>Contiene (and)</b></td>
                                                    <td><b>Contiene (or)</b></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <cc1:DTexto ID="inicia_Por" runat="server" AutoPostBack="False" BackColor_="" CssClass_="" EmptyValueMessage="" Enabled="False" Heigth="" InvalidValueMessage="  El dato no es valido" IsRequiered="True" MaxLength="0" MensajeColor="Red" Multiline="SingleLine" Text="" TooltipMessage="" ValidationGroup="Guardar" WaterText="Sin seleccionar" Width="200px" />
                                                    </td>
                                                    <td>
                                                        <cc1:DTexto ID="contiene_and" runat="server" AutoPostBack="False" BackColor_="" CssClass_="" EmptyValueMessage="" Enabled="False" Heigth="" InvalidValueMessage="  El dato no es valido" IsRequiered="True" MaxLength="0" MensajeColor="Red" Multiline="SingleLine" Text="" TooltipMessage="" ValidationGroup="Guardar" WaterText="Sin seleccionar" Width="200px" />
                                                    </td>
                                                    <td>
                                                        <cc1:DTexto ID="contiene_or" runat="server" AutoPostBack="False" BackColor_="" CssClass_="" EmptyValueMessage="" Enabled="False" Heigth="" InvalidValueMessage="  El dato no es valido" IsRequiered="False" MaxLength="0" MensajeColor="Red" Multiline="SingleLine" Text="" TooltipMessage="" ValidationGroup="Guardar" WaterText="Sin seleccionar" Width="200px" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td><b>No contiene (&lt;&gt;)</b></td>
                                                    <td><b>Finaliza en (=)</b></td>
                                                    <td><b>No finaliza en (&lt;&gt;)</b></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <cc1:DTexto ID="no_Contiene" runat="server" AutoPostBack="False" BackColor_="" CssClass_="" EmptyValueMessage="" Enabled="False" Heigth="" InvalidValueMessage="  El dato no es valido" IsRequiered="True" MaxLength="0" MensajeColor="Red" Multiline="SingleLine" Text="" TooltipMessage="" ValidationGroup="Guardar" WaterText="Sin seleccionar" Width="200px" />
                                                    </td>
                                                    <td>
                                                        <cc1:DTexto ID="finaliza_Por" runat="server" AutoPostBack="False" BackColor_="" CssClass_="" EmptyValueMessage="" Enabled="False" Heigth="" InvalidValueMessage="  El dato no es valido" IsRequiered="True" MaxLength="0" MensajeColor="Red" Multiline="SingleLine" Text="" TooltipMessage="" ValidationGroup="Guardar" WaterText="Sin seleccionar" Width="200px" />
                                                    </td>
                                                    <td>
                                                        <cc1:DTexto ID="no_Finaliza_En" runat="server" AutoPostBack="False" BackColor_="'#EAE8E3'" CssClass_="" EmptyValueMessage="" Enabled="False" Heigth="" InvalidValueMessage="  El dato no es valido" IsRequiered="False" MaxLength="0" MensajeColor="Red" Multiline="SingleLine" Text="" TooltipMessage="" ValidationGroup="Guardar" WaterText="Sin seleccionar" Width="200px" />
                                                    </td>
                                                </tr>
                                            </table>
                                            <br />
                                            <br />
                                            <cc1:CoreGridView ID="grd_Asociaciones" runat="server" ClickAction="OnDblClickSelectedPostBack" CssClass="yui-datatable-theme" EnableSort="True" GridNum="0" Height="48px" OnBeginPreSelect="" OnBeginSelect="" OnEndPreSelect="" OnEndSelect="" PreSelectedIndex="-1" PreSelectedStyleCssClass="row-PreSelect" Width="294px">
                                                <AlternatingRowStyle CssClass="alt-data-row" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Eliminar">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="imgEliminarItem0" runat="server" ImageAlign="Middle" ImageUrl="~/_images/basic/delete.png" />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="40px" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EditRowStyle CssClass="row-edit" />
                                                <PagerStyle CssClass="pager-stl" />
                                                <RowStyle CssClass="nor-data-row" />
                                                <SelectedRowStyle CssClass="row-Select" />
                                            </cc1:CoreGridView>
                                            <br />
                                            <br />
                                        </ContentTemplate>
                                    </cc2:TabPanel>
                                </cc2:TabContainer>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
