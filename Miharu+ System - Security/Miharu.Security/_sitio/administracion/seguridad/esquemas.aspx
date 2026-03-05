<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MiharuMasterForm.master"
    CodeBehind="esquemas.aspx.vb" Inherits="Miharu.Security._sitio.administracion.seguridad.esquemas" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="Miharu.Web.Controls" Namespace="Miharu.Web.Controls" TagPrefix="Miharu" %>
<%@ Register Src="../../../_controls/wucFilter.ascx" TagName="wucFilter" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../../_styles/StyleSheet_Marco.css" rel="stylesheet" type="text/css" />
    <link href="../../../_styles/StyleSheet_Miharu.css" rel="stylesheet" type="text/css" />
    <link href="../../../_styles/StyleSheet_Menu.css" rel="stylesheet" type="text/css" />
    <link href="../../../_styles/gridview/GridviewStyles.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function ShowOpciones(Modo)
        {
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
                            <asp:Label ID="lblTitulo" runat="server" Text="Esquemas" CssClass="Titulo_Principal"></asp:Label>
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
                                    <td style="height: 30px">
                                        <asp:Label ID="lblEntidad" runat="server" Text="Entidad" CssClass="Label" Width="120px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlEntidad" runat="server" Width="300px" AutoPostBack="True"
                                            CssClass="Textbox">
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
                                        <div style="border: 1px solid #CCCCCC; overflow: auto; height: 395px; width: 735px">
                                            <Miharu:SlygGridView ID="gvBase" runat="server" AutoGenerateColumns="False" GridNum="0"
                                                CssClass="yui-datatable-theme" EnableSort="True" ClickAction="OnDblClickSelectedPostBack">
                                                <AlternatingRowStyle CssClass="alt-data-row"></AlternatingRowStyle>
                                                <Columns>
                                                    <asp:BoundField DataField="id_Esquema_Seguridad" HeaderText="Cod." ItemStyle-Width="1" />
                                                    <asp:BoundField DataField="Nombre_Esquema_Seguridad" HeaderText="Nombre" />
                                                    <asp:BoundField DataField="Min_Longitud" HeaderText="Longitud" />
                                                    <asp:BoundField DataField="Min_Especiales" HeaderText="Especiales" />
                                                    <asp:BoundField DataField="Min_Mayusculas" HeaderText="Mayúsculas" />
                                                    <asp:BoundField DataField="Min_Minusculas" HeaderText="Minúsculas" />
                                                    <asp:BoundField DataField="Min_Numeros" HeaderText="Números" />
                                                    <asp:BoundField DataField="Num_Historial" HeaderText="Historial" />
                                                    <asp:CheckBoxField DataField="Cambiar_Password" HeaderText="Cambiar" />
                                                    <asp:BoundField DataField="Dias_Cambio_Password" HeaderText="Días" />
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
                            <div style="margin: 5px; height: 475px; width: 737px;">
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
                                            <td colspan="3" style="height: 10px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblNombre" runat="server" CssClass="Label">Nombre</asp:Label>
                                            </td>
                                            <td style="width: 10px">
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCodEsquema" runat="server" CssClass="Oculto"></asp:Label>
                                                <asp:TextBox ID="txtNombre" TabIndex="1" runat="server" CssClass="Textbox" Width="400px"
                                                    MaxLength="100" />
                                                <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ErrorMessage="&lt;b&gt;Falta campo requerido&lt;/b&gt;&lt;br /&gt;Se requiere un Nombre de Esquema."
                                                    Display="None" ValidationGroup="Guardar" SetFocusOnError="True" ControlToValidate="txtNombre"></asp:RequiredFieldValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="rfvNombre_ValidatorCalloutExtender" runat="server"
                                                    TargetControlID="rfvNombre" Enabled="True">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" style="height: 8px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 14px">
                                                <asp:Label ID="lblLongitud" runat="server" CssClass="Label">Longitud</asp:Label>
                                            </td>
                                            <td style="height: 14px">
                                            </td>
                                            <td style="height: 14px">
                                                <asp:TextBox ID="txtLongitud" TabIndex="2" runat="server" CssClass="Textbox" Width="30px"
                                                    MaxLength="2" ToolTip="Logitud mínima de la contraseña"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvLongitud" runat="server" ErrorMessage="&lt;b&gt;Falta campo requerido&lt;/b&gt;&lt;br /&gt;Se requiere la longitud mínima."
                                                    Display="None" ValidationGroup="Guardar" SetFocusOnError="True" ControlToValidate="txtLongitud"></asp:RequiredFieldValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="rfvLongitud_ValidatorCalloutExtender" runat="server"
                                                    TargetControlID="rfvLongitud" Enabled="True">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                <asp:RegularExpressionValidator ID="revLongitud" runat="server" ErrorMessage="&lt;b&gt;Dato invalido&lt;/b&gt;&lt;br /&gt;El valor debe ser un número."
                                                    ValidationExpression='\d*' Display="None" ValidationGroup="Guardar" SetFocusOnError="True"
                                                    ControlToValidate="txtLongitud"></asp:RegularExpressionValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="revLongitud_ValidatorCalloutExtender" runat="server"
                                                    TargetControlID="revLongitud" Enabled="True">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" style="height: 8px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 14px">
                                                <asp:Label ID="lblEspeciales" runat="server" CssClass="Label">Especiales</asp:Label>
                                            </td>
                                            <td style="height: 14px">
                                            </td>
                                            <td style="height: 14px">
                                                <asp:TextBox ID="txtEspeciales" TabIndex="3" runat="server" CssClass="Textbox" Width="30px"
                                                    MaxLength="2" ToolTip="Número mínimo de caracteres especiales a exigir en la contraseña"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvEspeciales" runat="server" ErrorMessage="&lt;b&gt;Falta campo requerido&lt;/b&gt;&lt;br /&gt;Se requiere el número de caracteres especiales."
                                                    Display="None" ValidationGroup="Guardar" SetFocusOnError="True" ControlToValidate="txtEspeciales"></asp:RequiredFieldValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="rfvEspeciales_ValidatorCalloutExtender"
                                                    runat="server" TargetControlID="rfvEspeciales" Enabled="True">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                <asp:RegularExpressionValidator ID="revEspeciales" runat="server" ErrorMessage="&lt;b&gt;Dato invalido&lt;/b&gt;&lt;br /&gt;El valor debe ser un número."
                                                    ValidationExpression='\d*' Display="None" ValidationGroup="Guardar" SetFocusOnError="True"
                                                    ControlToValidate="txtEspeciales"></asp:RegularExpressionValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="revEspeciales_ValidatorCalloutExtender"
                                                    runat="server" TargetControlID="revEspeciales" Enabled="True">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" style="height: 8px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 14px">
                                                <asp:Label ID="lblMayusculas" runat="server" CssClass="Label">Mayúsculas</asp:Label>
                                            </td>
                                            <td style="height: 14px">
                                            </td>
                                            <td style="height: 14px">
                                                <asp:TextBox ID="txtMayusculas" TabIndex="4" runat="server" CssClass="Textbox" Width="30px"
                                                    MaxLength="2" ToolTip="Número mínimo de letras mayúsculas a exigir en la contraseña"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvMayusculas" runat="server" ErrorMessage="&lt;b&gt;Falta campo requerido&lt;/b&gt;&lt;br /&gt;Se requiere el número de caracteres mayúscula."
                                                    Display="None" ValidationGroup="Guardar" SetFocusOnError="True" ControlToValidate="txtMayusculas"></asp:RequiredFieldValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="rfvMayusculas_ValidatorCalloutExtender"
                                                    runat="server" TargetControlID="rfvMayusculas" Enabled="True">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                <asp:RegularExpressionValidator ID="revMayusculas" runat="server" ErrorMessage="&lt;b&gt;Dato invalido&lt;/b&gt;&lt;br /&gt;El valor debe ser un número."
                                                    ValidationExpression='\d*' Display="None" ValidationGroup="Guardar" SetFocusOnError="True"
                                                    ControlToValidate="txtMayusculas"></asp:RegularExpressionValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="revMayusculas_ValidatorCalloutExtender"
                                                    runat="server" TargetControlID="revMayusculas" Enabled="True">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" style="height: 8px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 14px">
                                                <asp:Label ID="lblMinusculas" runat="server" CssClass="Label">Minúsculas</asp:Label>
                                            </td>
                                            <td style="height: 14px">
                                            </td>
                                            <td style="height: 14px">
                                                <asp:TextBox ID="txtMinusculas" TabIndex="5" runat="server" CssClass="Textbox" Width="30px"
                                                    MaxLength="2" ToolTip="Número mínimo de letras minúsculas a exigir en la contraseña"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvMinusculas" runat="server" ErrorMessage="&lt;b&gt;Falta campo requerido&lt;/b&gt;&lt;br /&gt;Se requiere el número de caracteres minúscula."
                                                    Display="None" ValidationGroup="Guardar" SetFocusOnError="True" ControlToValidate="txtMinusculas"></asp:RequiredFieldValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="rfvMinusculas_ValidatorCalloutExtender"
                                                    runat="server" TargetControlID="rfvMinusculas" Enabled="True">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                <asp:RegularExpressionValidator ID="revMinusculas" runat="server" ErrorMessage="&lt;b&gt;Dato invalido&lt;/b&gt;&lt;br /&gt;El valor debe ser un número."
                                                    ValidationExpression='\d*' Display="None" ValidationGroup="Guardar" SetFocusOnError="True"
                                                    ControlToValidate="txtMinusculas"></asp:RegularExpressionValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="revMinusculas_ValidatorCalloutExtender"
                                                    runat="server" TargetControlID="revMinusculas" Enabled="True">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" style="height: 8px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 14px">
                                                <asp:Label ID="lblNumeros" runat="server" CssClass="Label">Números</asp:Label>
                                            </td>
                                            <td style="height: 14px">
                                            </td>
                                            <td style="height: 14px">
                                                <asp:TextBox ID="txtNumeros" TabIndex="6" runat="server" CssClass="Textbox" Width="30px"
                                                    MaxLength="2" ToolTip="Número mínimo de caracteres número a exigir en la contraseña"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvNumeros" runat="server" ErrorMessage="&lt;b&gt;Falta campo requerido&lt;/b&gt;&lt;br /&gt;Se requiere el número de caracteres número."
                                                    Display="None" ValidationGroup="Guardar" SetFocusOnError="True" ControlToValidate="txtNumeros"></asp:RequiredFieldValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="rfvNumeros_ValidatorCalloutExtender" runat="server"
                                                    TargetControlID="rfvNumeros" Enabled="True">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                <asp:RegularExpressionValidator ID="revNumeros" runat="server" ErrorMessage="&lt;b&gt;Dato invalido&lt;/b&gt;&lt;br /&gt;El valor debe ser un número."
                                                    ValidationExpression='\d*' Display="None" ValidationGroup="Guardar" SetFocusOnError="True"
                                                    ControlToValidate="txtNumeros"></asp:RegularExpressionValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="revNumeros_ValidatorCalloutExtender" runat="server"
                                                    TargetControlID="revNumeros" Enabled="True">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" style="height: 8px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 14px">
                                                <asp:Label ID="lblHistorial" runat="server" CssClass="Label">Historial</asp:Label>
                                            </td>
                                            <td style="height: 14px">
                                            </td>
                                            <td style="height: 14px">
                                                <asp:TextBox ID="txtHistorial" TabIndex="7" runat="server" CssClass="Textbox" Width="30px"
                                                    MaxLength="3" ToolTip="Número de últimas contraseñas en el historial para validar que no se repita la contraseña"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvHistorial" runat="server" ErrorMessage="&lt;b&gt;Falta campo requerido&lt;/b&gt;&lt;br /&gt;Se requiere el número de contraseñas en el historial."
                                                    Display="None" ValidationGroup="Guardar" SetFocusOnError="True" ControlToValidate="txtHistorial"></asp:RequiredFieldValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="rfvHistorial_ValidatorCalloutExtender"
                                                    runat="server" TargetControlID="rfvHistorial" Enabled="True">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                <asp:RegularExpressionValidator ID="revHistorial" runat="server" ErrorMessage="&lt;b&gt;Dato invalido&lt;/b&gt;&lt;br /&gt;El valor debe ser un número."
                                                    ValidationExpression='\d*' Display="None" ValidationGroup="Guardar" SetFocusOnError="True"
                                                    ControlToValidate="txtHistorial"></asp:RegularExpressionValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="revHistorial_ValidatorCalloutExtender"
                                                    runat="server" TargetControlID="revHistorial" Enabled="True">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" style="height: 8px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 14px">
                                            </td>
                                            <td style="height: 14px">
                                            </td>
                                            <td style="height: 14px">
                                                <asp:CheckBox ID="chkCambioContraseña" TabIndex="8" runat="server" Text="Exigir cambio periodico de contraseña?"
                                                    ToolTip="Exigir cambio periodico de contraseña" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" style="height: 8px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 14px">
                                                <asp:Label ID="lblDias" runat="server" CssClass="Label">Días cambio</asp:Label>
                                            </td>
                                            <td style="height: 14px">
                                            </td>
                                            <td style="height: 14px">
                                                <asp:TextBox ID="txtDias" TabIndex="9" runat="server" CssClass="Textbox" Width="50px"
                                                    MaxLength="5" ToolTip="Número máximo de días antes del cambio de contraseña"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvDias" runat="server" ErrorMessage="&lt;b&gt;Falta campo requerido&lt;/b&gt;&lt;br /&gt;Se requiere el número de días antes del cambio de obligatorio de contraseña."
                                                    Display="None" ValidationGroup="Guardar" SetFocusOnError="True" ControlToValidate="txtDias"></asp:RequiredFieldValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="rfvDias_ValidatorCalloutExtender" runat="server"
                                                    TargetControlID="rfvDias" Enabled="True">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                <asp:RegularExpressionValidator ID="revDias" runat="server" ErrorMessage="&lt;b&gt;Dato invalido&lt;/b&gt;&lt;br /&gt;El valor debe ser un número."
                                                    ValidationExpression='\d*' Display="None" ValidationGroup="Guardar" SetFocusOnError="True"
                                                    ControlToValidate="txtDias"></asp:RegularExpressionValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="revDias_ValidatorCalloutExtender" runat="server"
                                                    TargetControlID="revDias" Enabled="True">
                                                </ajaxToolkit:ValidatorCalloutExtender>
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
