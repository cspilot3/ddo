<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MiharuMasterForm.master"
    CodeBehind="usuarios.aspx.vb" Inherits="Miharu.Security._sitio.administracion.seguridad.usuarios" Title="" Culture="Auto"
    UICulture="Auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="Miharu.Web.Controls" Namespace="Miharu.Web.Controls" TagPrefix="Miharu" %>
<%@ Register Src="../../../_controls/wucFilter.ascx" TagName="wucFilter" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../../_styles/StyleSheet_Marco.css" rel="stylesheet" type="text/css" />
    <link href="../../../_styles/StyleSheet_Miharu.css" rel="stylesheet" type="text/css" />
    <link href="../../../_styles/StyleSheet_Menu.css" rel="stylesheet" type="text/css" />
    <link href="../../../_styles/gridview/GridviewStyles.css" rel="stylesheet" type="text/css" />
    <link href="../../../_styles/StyleSheet_DialogBox.css" rel="stylesheet" type="text/css" />
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
                <table cellpadding="0" cellspacing="0" border="0" style="width: 760px;">
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
                            <asp:Label ID="lblTitulo" runat="server" Text="Usuarios" CssClass="Titulo_Principal"></asp:Label>
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
                                        <asp:Label ID="lblFiltro" runat="server" Text="Filtro: [Apellidos]" CssClass="Label"
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
                                                CssClass="yui-datatable-theme" EnableSort="True" ClickAction="OnDblClickSelectedPostBack"
                                                OnBeginPreSelect="" OnBeginSelect="" OnEndPreSelect="" OnEndSelect="" PreSelectedIndex="-1"
                                                PreSelectedStyleCssClass="row-PreSelect">
                                                <AlternatingRowStyle CssClass="alt-data-row"></AlternatingRowStyle>
                                                <Columns>
                                                    <asp:BoundField DataField="id_Usuario" HeaderText="Cod.">
                                                        <ItemStyle Width="1px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Apellidos_Usuario" HeaderText="Apellidos" />
                                                    <asp:BoundField DataField="Nombres_Usuario" HeaderText="Nombres" />
                                                    <asp:BoundField DataField="Identificacion_Usuario" HeaderText="Cédula" />
                                                    <asp:BoundField DataField="Login_Usuario" HeaderText="Login" />
                                                    <asp:CheckBoxField DataField="Usuario_Activo" HeaderText="Activo" />                                                    
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
                                            <td colspan="5" style="height: 10px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblLogin" runat="server" CssClass="Label">Login</asp:Label>
                                            </td>
                                            <td style="width: 10px">
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtLogin" TabIndex="1" runat="server" CssClass="Textbox" Width="200px"
                                                    MaxLength="20" />
                                                <asp:RequiredFieldValidator ID="rfvLogin" runat="server" ErrorMessage="&lt;b&gt;Falta campo requerido&lt;/b&gt;&lt;br /&gt;Se requiere un Login de usuario."
                                                    Display="None" ValidationGroup="Guardar" SetFocusOnError="True" ControlToValidate="txtLogin"></asp:RequiredFieldValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="rfvLogin_ValidatorCalloutExtender" runat="server"
                                                    TargetControlID="rfvLogin" Enabled="True">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>
                                            <td style="width: 10px">
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chkActivo" TabIndex="9" runat="server" CssClass="Label" Text="Activo">
                                                </asp:CheckBox>
                                            </td>
                                            <td style="width: 10px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="5" style="height: 10px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="5" style="height: 10px">
                                                <ajaxToolkit:TabContainer ID="tcDetalle" runat="server" CssClass="ajax__tab_technorati-theme"
                                                    ActiveTabIndex="1" Width="700px">
                                                    <ajaxToolkit:TabPanel runat="server" HeaderText="Datos" ID="tpDatos">
                                                        <ContentTemplate>
                                                            <div style="border: 1px solid #FFFFFF; overflow: auto; width: 680px; height: 360px">
                                                                <table border="0" cellpadding="0" cellspacing="0" width="650px" style="margin-left: 10px">
                                                                    <tr>
                                                                        <td colspan="5" style="height: 8px">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="height: 14px">
                                                                            <asp:Label ID="lblNombres" runat="server" CssClass="Label">Nombres</asp:Label>
                                                                        </td>
                                                                        <td style="width: 10px">
                                                                        </td>
                                                                        <td style="height: 14px">
                                                                            <asp:Label ID="lblCodUsuario" runat="server" Text="-1" CssClass="Oculto"></asp:Label>
                                                                            <asp:TextBox ID="txtNombres" TabIndex="2" runat="server" CssClass="Textbox" Width="300px"
                                                                                MaxLength="30"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="rfvNombres" runat="server" ErrorMessage="&lt;b&gt;Falta campo requerido&lt;/b&gt;&lt;br /&gt;Se requiere un Nombre de usuario."
                                                                                Display="None" ValidationGroup="Guardar" SetFocusOnError="True" ControlToValidate="txtNombres"></asp:RequiredFieldValidator>
                                                                            <ajaxToolkit:ValidatorCalloutExtender ID="rfvNombres_ValidatorCalloutExtender" runat="server"
                                                                                TargetControlID="rfvNombres" Enabled="True">
                                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                                        </td>
                                                                        <td style="height: 14px">
                                                                        </td>
                                                                        <td style="height: 14px">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="5" style="height: 5px">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lblApellidos" runat="server" CssClass="Label">Apellidos</asp:Label>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtApellidos" TabIndex="3" runat="server" CssClass="Textbox" Width="300px"
                                                                                MaxLength="30"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="rfvApellidos" runat="server" ErrorMessage="&lt;b&gt;Falta campo requerido&lt;/b&gt;&lt;br /&gt;Se requiere un Apellido de usuario."
                                                                                Display="None" ValidationGroup="Guardar" SetFocusOnError="True" ControlToValidate="txtApellidos"></asp:RequiredFieldValidator>
                                                                            <ajaxToolkit:ValidatorCalloutExtender ID="rfvApellidos_ValidatorCalloutExtender"
                                                                                runat="server" TargetControlID="rfvApellidos" Enabled="True">
                                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="5" style="height: 8px">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lblIdentificacion" runat="server" CssClass="Label">Identificación</asp:Label>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtIdentificacion" TabIndex="4" runat="server" CssClass="Textbox"
                                                                                Width="200px" MaxLength="15"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="rfvIdentificacion" runat="server" ErrorMessage="&lt;b&gt;Falta campo requerido&lt;/b&gt;&lt;br /&gt;Se requiere el documento de identificación del usuario."
                                                                                Display="None" ValidationGroup="Guardar" SetFocusOnError="True" ControlToValidate="txtIdentificacion"></asp:RequiredFieldValidator>
                                                                            <ajaxToolkit:ValidatorCalloutExtender ID="rfvIdentificacion_ValidatorCalloutExtender"
                                                                                runat="server" TargetControlID="rfvIdentificacion" Enabled="True">
                                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="5" style="height: 8px">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lblEmail" runat="server" CssClass="Label">Email</asp:Label>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtEmail" TabIndex="5" runat="server" CssClass="Textbox" Width="300px"
                                                                                MaxLength="100"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ErrorMessage="&lt;b&gt;Falta campo requerido&lt;/b&gt;&lt;br /&gt;Debe ingresar una dirección de e-mail valida."
                                                                                Display="None" ValidationGroup="Guardar" SetFocusOnError="True" ControlToValidate="txtEmail"></asp:RequiredFieldValidator>
                                                                            <ajaxToolkit:ValidatorCalloutExtender ID="rfvEmail_ValidatorCalloutExtender" runat="server"
                                                                                TargetControlID="rfvEmail" Enabled="True">
                                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                                            <asp:RegularExpressionValidator ID="revEmail" runat="server" ErrorMessage="&lt;b&gt;Dato invalido&lt;/b&gt;&lt;br /&gt;Debe ingresar una dirección de e-mail valida."
                                                                                ValidationExpression='^(([^<;>;()[\]\\.,;:\s@\""]+(\.[^<;>;()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$'
                                                                                Display="None" ValidationGroup="Guardar" SetFocusOnError="True" ControlToValidate="txtEmail"></asp:RegularExpressionValidator>
                                                                            <ajaxToolkit:ValidatorCalloutExtender ID="revEmail_ValidatorCalloutExtender" runat="server"
                                                                                TargetControlID="revEmail" Enabled="True">
                                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="5" style="height: 8px">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lblDireccion" runat="server" CssClass="Label">Dirección</asp:Label>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtDireccion" TabIndex="6" runat="server" CssClass="Textbox" Width="300px"
                                                                                MaxLength="100"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="5" style="height: 8px">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lblTelefono" runat="server" CssClass="Label">Teléfono</asp:Label>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtTelefono" TabIndex="7" runat="server" CssClass="Textbox" MaxLength="15"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="5" style="height: 8px">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lblDependencia" runat="server" CssClass="Label">Dependencia</asp:Label>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlDependencia" runat="server" Width="300px" CssClass="Textbox">
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="rfvDependencia" runat="server" ErrorMessage="&lt;b&gt;Falta campo requerido&lt;/b&gt;&lt;br /&gt;Debe seleccionar una Dependencia."
                                                                                Display="None" ValidationGroup="Guardar" SetFocusOnError="True" ControlToValidate="ddlDependencia"></asp:RequiredFieldValidator>
                                                                            <ajaxToolkit:ValidatorCalloutExtender ID="rfvDependencia_ValidatorCalloutExtender"
                                                                                runat="server" TargetControlID="rfvDependencia" Enabled="True">
                                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="5" style="height: 8px">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lblJefe" runat="server" CssClass="Label">Jefe</asp:Label>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td colspan="3">
                                                                            <table border="0" cellpadding="0" cellspacing="0">
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtJefe" TabIndex="9" runat="server" CssClass="TextboxDisable" Enabled="false"
                                                                                            Width="480"></asp:TextBox><asp:Label ID="lblCodJefe" runat="server" Text="-1" CssClass="Oculto"></asp:Label>
                                                                                    </td>
                                                                                    <td style="width: 10px">
                                                                                    </td>
                                                                                    <td>
                                                                                        <Miharu:ImageChangingButton ID="icbJefe" runat="server" ImageUrl="~/_images/basic/jefe.png" />
                                                                                    </td>
                                                                                    <td style="width: 10px">
                                                                                    </td>
                                                                                    <td>
                                                                                        <div id="divDeleteJefe" class="BotonCambiante" style="width: 16px" runat="server">
                                                                                            <asp:ImageButton ID="ibDeleteJefe" runat="server" ImageUrl="~/_images/flujo/delete.png"
                                                                                                ToolTip="Quitar jefe" />
                                                                                        </div>
                                                                                        <ajaxToolkit:ConfirmButtonExtender ID="ibDeleteJefe_ConfirmButtonExtender" runat="server"
                                                                                            ConfirmText="¿Confirma que desea quitar el jefe?" TargetControlID="ibDeleteJefe">
                                                                                        </ajaxToolkit:ConfirmButtonExtender>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="5" style="height: 8px">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td colspan="3">
                                                                            <asp:CheckBox ID="chkCambio" runat="server" CssClass="Label" TabIndex="8" Text="Solicitar cambio de password">
                                                                            </asp:CheckBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="5" style="height: 8px">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="height: 14px">
                                                                            <asp:Label ID="lblObservaciones" runat="server" CssClass="Label">Observaciones</asp:Label>
                                                                        </td>
                                                                        <td style="width: 10px">
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtObservaciones" TabIndex="10" runat="server" CssClass="Textbox"
                                                                                Width="500px" Height="60" MaxLength="200" TextMode="MultiLine"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="5" style="height: 8px">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:Button ID="btnAsignarPassword" runat="server" CssClass="Button" Text="Asignar contraseña"
                                                                                Width="120px" TabIndex="11" />
                                                                            <ajaxToolkit:ModalPopupExtender ID="ModalPopupPassword" runat="server" TargetControlID="btnAsignarPassword"
                                                                                PopupControlID="pnlPassword" PopupDragHandleControlID="pnlPasswordHead" CancelControlID="PasswordCancelarButton"
                                                                                DropShadow="True" BackgroundCssClass="modalBackground" DynamicServicePath=""
                                                                                Enabled="True">
                                                                            </ajaxToolkit:ModalPopupExtender>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </ContentTemplate>
                                                    </ajaxToolkit:TabPanel>
                                                    <ajaxToolkit:TabPanel runat="server" HeaderText="Seguridad" ID="tpPerfiles">
                                                        <ContentTemplate>
                                                            <div style="border: 1px solid #FFFFFF; overflow: auto; width: 680px; height: 360px">
                                                                <table border="0" cellpadding="0" cellspacing="0" width="650px" style="margin-left: 10px">
                                                                    <tr>
                                                                        <td colspan="3" style="height: 10px">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lblEsquemaSeguridad" runat="server" CssClass="Label">Esquema de seguridad</asp:Label>
                                                                        </td>
                                                                        <td style="width: 10px">
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlEsquemaSeguridad" CssClass="Textbox" runat="server" Width="300px">
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="rfvEsquemaSeguridad" runat="server" ErrorMessage="&lt;b&gt;Falta campo requerido&lt;/b&gt;&lt;br /&gt;Debe seleccionar un Esquema de seguridad."
                                                                                Display="None" ValidationGroup="Guardar" SetFocusOnError="True" ControlToValidate="ddlEsquemaSeguridad"></asp:RequiredFieldValidator>
                                                                            <ajaxToolkit:ValidatorCalloutExtender ID="rfvEsquemaSeguridad_ValidatorCalloutExtender"
                                                                                runat="server" TargetControlID="rfvEsquemaSeguridad" Enabled="True">
                                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="3" style="height: 8px">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="3">
                                                                            <div style="border: 1px solid #999999; overflow: auto; width: 660px; height: 310px">
                                                                                <asp:GridView ID="gvPerfiles" runat="server" AutoGenerateColumns="False" CssClass="yui-datatable-theme"
                                                                                    Width="630px">
                                                                                    <RowStyle CssClass="nor-data-row" />
                                                                                    <Columns>
                                                                                        <asp:BoundField DataField="id_Perfil" HeaderText="Cod." ItemStyle-Width="1" />
                                                                                        <asp:BoundField DataField="Nombre_Perfil" HeaderText="Perfil" />
                                                                                        <asp:TemplateField ItemStyle-Width="1px">
                                                                                            <ItemTemplate>
                                                                                                <asp:CheckBox ID="chkPerfil" runat="server" />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                    <PagerStyle CssClass="pager-stl" />
                                                                                    <SelectedRowStyle CssClass="row-edit" />
                                                                                    <EditRowStyle CssClass="row-edit" />
                                                                                    <AlternatingRowStyle CssClass="alt-data-row" />
                                                                                </asp:GridView>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </ContentTemplate>
                                                    </ajaxToolkit:TabPanel>
                                                    <ajaxToolkit:TabPanel runat="server" HeaderText="Roles" ID="tpRoles">
                                                        <ContentTemplate>
                                                            <div style="border: 1px solid #FFFFFF; overflow: auto; width: 680px; height: 360px">
                                                                <table border="0" cellpadding="0" cellspacing="0" width="650px" style="margin-left: 10px">
                                                                    <tr>
                                                                        <td colspan="3" style="height: 10px">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="3">
                                                                            <div style="border: 1px solid #999999; overflow: auto; width: 660px; height: 310px">
                                                                                <asp:GridView ID="gvRoles" runat="server" AutoGenerateColumns="False" CssClass="yui-datatable-theme"
                                                                                    Width="630px">
                                                                                    <RowStyle CssClass="nor-data-row" />
                                                                                    <Columns>
                                                                                        <asp:BoundField DataField="id_Rol" HeaderText="Cod." ItemStyle-Width="1" />
                                                                                        <asp:BoundField DataField="Nombre_Rol" HeaderText="Rol" />
                                                                                        <asp:TemplateField ItemStyle-Width="1px">
                                                                                            <ItemTemplate>
                                                                                                <asp:CheckBox ID="chkRol" runat="server" />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                    <PagerStyle CssClass="pager-stl" />
                                                                                    <SelectedRowStyle CssClass="row-edit" />
                                                                                    <EditRowStyle CssClass="row-edit" />
                                                                                    <AlternatingRowStyle CssClass="alt-data-row" />
                                                                                </asp:GridView>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </ContentTemplate>
                                                    </ajaxToolkit:TabPanel>
                                                </ajaxToolkit:TabContainer>
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
    <asp:Panel ID="pnlPassword" runat="server" Style="display: none" Width="220px" DefaultButton="PasswordAceptarButton">
        <table class="table_window" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="DialogBox_nw">
                </td>
                <td class="DialogBox_n">
                    <asp:Panel ID="pnlPasswordHead" runat="server">
                        <div class="title_window">
                            Asignar contraseña</div>
                    </asp:Panel>
                </td>
                <td class="DialogBox_close" onclick="hideModalPopupViaClient('<%= ModalPopupPassword.ClientID %>')">
                </td>
                <td class="DialogBox_ne">
                </td>
            </tr>
        </table>
        <table class="table_window" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="DialogBox_w">
                </td>
                <td valign="top" class="Content">
                    <table class="table_window" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td colspan="3" style="height: 10px">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="padding: 0px 0px 0px 10px">
                                <asp:Label ID="lblPassword1" runat="server" Text="Contraseña" CssClass="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="padding: 0px 0px 0px 10px">
                                <asp:TextBox ID="txtPassword1" runat="server" Width="180px" TextMode="Password"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="height: 10px">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="padding: 0px 0px 0px 10px">
                                <asp:Label ID="lblPassword2" runat="server" Text="Confirmar contraseña" CssClass="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="padding: 0px 0px 0px 10px">
                                <asp:TextBox ID="txtPassword2" runat="server" Width="180px" TextMode="Password"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="height: 10px">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 90px" align="right">
                                <asp:Button ID="PasswordAceptarButton" runat="server" Text="Aceptar" />
                            </td>
                            <td style="width: 20px">
                            </td>
                            <td style="width: 90px">
                                <asp:Button ID="PasswordCancelarButton" runat="server" Text="Cancelar" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="height: 10px">
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="DialogBox_e">
                </td>
            </tr>
        </table>
        <table class="table_window" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="DialogBox_sw">
                </td>
                <td class="DialogBox_s">
                    <div style="width: 10px; height: 7px">
                    </div>
                </td>
                <td class="DialogBox_se">
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
