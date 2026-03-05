<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MiharuMasterForm.master"
    CodeBehind="puestotrabajo.aspx.vb" Inherits="Miharu.Security._sitio.administracion.estructura.puestotrabajo" Title="" %>

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

    <style type="text/css">
        .style1
        {
            height: 63px;
        }
        .style3
        {
            width: 10px;
        }
    </style>
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
                                    <asp:ImageButton ID="ibAdd" runat="server" ImageUrl="~/_images/opciones/nuevo.png"
                                        ToolTip="Agregar un nuevo registro" />
                                </div>
                            </div>
                        </td>
                        <td style="width: 30px">
                            <div id="divDelete" runat="server" style="width: 25px; display: none">
                                <div class="BotonCambiante">
                                    <asp:ImageButton ID="ibDelete" runat="server" ImageUrl="~/_images/opciones/delete.png"
                                        ToolTip="Eliminar el registro actual" />
                                </div>
                                <ajaxToolkit:ConfirmButtonExtender ID="ibDelete_ConfirmButtonExtender" runat="server"
                                    ConfirmText="¿Confirma que desea eliminar el registro?" TargetControlID="ibDelete">
                                </ajaxToolkit:ConfirmButtonExtender>
                            </div>
                        </td>
                        <td style="width: 10px">
                            &#160;
                        </td>
                        <td style="width: 30px">
                            <div id="divSave" runat="server" style="width: 25px; display: none">
                                <div class="BotonCambiante">
                                    <asp:ImageButton ID="ibSave" runat="server" ImageUrl="~/_images/opciones/save.png"
                                        ToolTip="Guardar los cambios" ValidationGroup="Guardar" />
                                </div>
                            </div>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTitulo" runat="server" Text="Puestos de Trabajo" CssClass="Titulo_Principal"></asp:Label>
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
                <ajaxToolkit:TabContainer ID="tcBase" runat="server" CssClass="ajax__tab_technorati-theme"
                    ActiveTabIndex="0" Width="770">
                    <ajaxToolkit:TabPanel runat="server" ID="tpConsulta">
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
                                    <td style="height: 20px">
                                        <asp:Label ID="lblEntidad" runat="server" Text="Entidad" CssClass="Label" Width="120px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlEntidad" runat="server" Width="300px" AutoPostBack="True"
                                            CssClass="Textbox">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 20px">
                                        <asp:Label ID="lblSede" runat="server" Text="Sede" CssClass="Label" Width="120px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlSede" runat="server" Width="300px" AutoPostBack="True" CssClass="Textbox">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblFiltro" runat="server" Text="Filtro: [Nombre]" CssClass="Label"
                                            Width="120px"></asp:Label>
                                    </td>
                                    <td>
                                        <uc1:wucFilter ID="ucFiltro" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <div style="border: 1px solid #CCCCCC; overflow: auto; height: 375px; width: 735px">
                                            <Miharu:SlygGridView ID="gvBase" runat="server" AutoGenerateColumns="False" GridNum="0"
                                                CssClass="yui-datatable-theme" EnableSort="True" ClickAction="OnDblClickSelectedPostBack" OnBeginPreSelect="" OnBeginSelect="" OnEndPreSelect="" 
                                                OnEndSelect="" PreSelectedIndex="-1" PreSelectedStyleCssClass="row-PreSelect">
                                                <AlternatingRowStyle CssClass="alt-data-row"></AlternatingRowStyle>
                                                <Columns>
                                                    <asp:BoundField DataField="id_Puesto_Trabajo" HeaderText="Cod." >
                                                    <ItemStyle Width="1px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="fk_Centro_Procesamiento" HeaderText="Centro procesamiento" />
                                                    <asp:BoundField DataField="PC_Name" HeaderText="Nombre" />
                                                    <asp:BoundField DataField="IP_Address" HeaderText="Direccion IP" />
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
                    <ajaxToolkit:TabPanel ID="tpDetalle" runat="server">
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
                            <div style="margin: 5px; height: 475px; width: 737px; overflow: auto;">
                                <asp:Panel ID="pnlDetalle" runat="server" Visible="False">
                                    <table id="tblDetalle" cellspacing="0" cellpadding="0" border="0" style="margin-left: 20px">
                                        <tr>
                                            <td colspan="3" style="height: 10px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblEntidadEdit" runat="server" CssClass="Label">Entidad</asp:Label>
                                            </td>
                                            <td style="width: 10px">
                                            </td>
                                            <td>
                                                <asp:Label ID="lblNombreEntidad" runat="server" CssClass="Textbox" Width="500px">Entidad</asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" style="height: 5px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblSedeEdit" runat="server" CssClass="Label">Sede</asp:Label>
                                            </td>
                                            <td style="width: 10px">
                                            </td>
                                            <td>
                                                <asp:Label ID="lblNombreSede" runat="server" CssClass="Textbox" Width="500px">Sede</asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" style="height: 10px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblCentroProcesamiento" runat="server" CssClass="Label">Centro de Procesamiento</asp:Label>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlCentro" runat="server" Width="400px" AutoPostBack="True"
                                                    CssClass="Textbox">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" style="height: 8px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblPCName" runat="server" CssClass="Label">Nombre Equipo</asp:Label>
                                            </td>
                                            <td class="style3">
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCodPuestoTrabajo" runat="server" CssClass="Oculto"></asp:Label>
                                                <asp:TextBox ID="txtPCName" TabIndex="1" runat="server" CssClass="Textbox" Width="400px"
                                                    MaxLength="100" />
                                                <asp:RequiredFieldValidator ID="rfvPCName" runat="server" ErrorMessage="&lt;b&gt;Falta campo requerido&lt;/b&gt;&lt;br /&gt;Se requiere el Nombre del Puesto de Trabajo."
                                                    Display="None" ValidationGroup="Guardar" SetFocusOnError="True" ControlToValidate="txtPCName"></asp:RequiredFieldValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="rfvPCName_ValidatorCalloutExtender" runat="server"
                                                    TargetControlID="rfvPCName" Enabled="True">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" style="height: 5px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblIPName" runat="server" CssClass="Label">Direccion IP</asp:Label>
                                            </td>
                                            <td class="style3">
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtIPName" TabIndex="2" runat="server" CssClass="Textbox" Width="400px"
                                                    MaxLength="100" />
                                                <asp:RequiredFieldValidator ID="rfvIPName" runat="server" ErrorMessage="&lt;b&gt;Falta campo requerido&lt;/b&gt;&lt;br /&gt;Se requiere una Dirección IP para el Puesto de Trabajo."
                                                    Display="None" ValidationGroup="Guardar" SetFocusOnError="True" ControlToValidate="txtIPName"></asp:RequiredFieldValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="rfvIPName_ValidatorCalloutExtender" runat="server"
                                                    TargetControlID="rfvIPName" Enabled="True">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" style="height: 5px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblDescripcion" runat="server" CssClass="Label">Descripción</asp:Label>
                                            </td>
                                            <td class="style3">
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDescripción" TabIndex="3" runat="server" CssClass="Textbox" Width="400px"
                                                    MaxLength="100" />
                                                <asp:RequiredFieldValidator ID="rfvDescripcion" runat="server" ErrorMessage="&lt;b&gt;Falta campo requerido&lt;/b&gt;&lt;br /&gt;Se requiere una Descripcion para el Puesto de Trabajo."
                                                    Display="None" ValidationGroup="Guardar" SetFocusOnError="True" ControlToValidate="txtIPName"></asp:RequiredFieldValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="rfvDescripcion_ValidatorCalloutExtender"
                                                    runat="server" TargetControlID="rfvDescripcion" Enabled="True">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblCamara" runat="server" CssClass="Label">Tiene Camara</asp:Label>
                                            </td>
                                            <td style="width: 10px">
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="cbxTieneCamara" runat="server" CssClass="Label" Width="500px"></asp:CheckBox>
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblCodigoCamara" runat="server" CssClass="Label">Código Cámara</asp:Label>
                                            </td>
                                            <td class="style3">
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCodigoCamara" TabIndex="3" runat="server" CssClass="Textbox" Width="400px"
                                                    MaxLength="100" />
                                               <%-- <asp:RequiredFieldValidator ID="rfvCodigoCamara" runat="server" ErrorMessage="&lt;b&gt;Falta campo requerido&lt;/b&gt;&lt;br /&gt;Se requiere un código de Cámara."
                                                    Display="None" ValidationGroup="Guardar" SetFocusOnError="True" ControlToValidate="txtCodigoCamara"></asp:RequiredFieldValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="rfvCodigoCamara_ValidatorCalloutExtender"
                                                    runat="server" TargetControlID="rfvCodigoCamara" Enabled="True">
                                                </ajaxToolkit:ValidatorCalloutExtender>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" style="height: 8px">
                                            </td>
                                        </tr>
                                </asp:Panel>
                            </div>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                </ajaxToolkit:TabContainer>
            </td>
        </tr>
    </table>
</asp:Content>
