<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MiharuMasterForm.master"
    CodeBehind="centrosprocesamiento.aspx.vb" Inherits="Miharu.Security._sitio.administracion.estructura.centrosprocesamiento" %>

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
    <script runat="server"> 

        Sub Check_Clicked(ByVal sender As Object, ByVal e As EventArgs)
            If checkboxDefaultCentro.Checked Then
                ddlSedeAsig.Visible = False
                ddlCentroProcAsig.Visible = False
                lblSedeAsignada.Visible = False
                lblCentroAsignado.Visible = False
            Else
                ddlSedeAsig.Visible = True
                ddlCentroProcAsig.Visible = True
                lblSedeAsignada.Visible = True
                lblCentroAsignado.Visible = True
            End If

        End Sub
          
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Cuerpo" runat="server">
    <table style="width: 780px; height: 619px;" border="0" cellpadding="0" cellspacing="0">
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
                            <asp:Label ID="lblTitulo" runat="server" Text="Centros de Procesamiento" CssClass="Titulo_Principal"></asp:Label>
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
                                                CssClass="yui-datatable-theme" EnableSort="True" ClickAction="OnDblClickSelectedPostBack"
                                                OnBeginPreSelect="" OnBeginSelect="" OnEndPreSelect="" OnEndSelect="" PreSelectedIndex="-1"
                                                PreSelectedStyleCssClass="row-PreSelect">
                                                <AlternatingRowStyle CssClass="alt-data-row"></AlternatingRowStyle>
                                                <Columns>
                                                    <asp:BoundField DataField="id_Centro_Procesamiento" HeaderText="Cod.">
                                                        <ItemStyle Width="1px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Nombre_Centro_Procesamiento" HeaderText="Nombre" />
                                                    <asp:BoundField DataField="PC_Name" HeaderText="PCName" />
                                                    <asp:BoundField DataField="IP_Address" HeaderText="IPAddress" />
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
                            <asp:Panel ID="pnlDetalle" runat="server" Height="472px" Width="766px">
                                <table id="tblDetalle" cellspacing="0" cellpadding="0" border="0" style="margin-left: 20px">
                                    <tr>
                                        <td colspan="3" style="height: 10px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblEntidadEdit" runat="server" CssClass="Label">Entidad</asp:Label>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="lblNombreEntidad" runat="server" CssClass="Textbox" Width="349px"
                                                Height="17px">Entidad</asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="height: 5px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style10">
                                            <asp:Label ID="lblSedeEdit" runat="server" CssClass="Label">Sede</asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblNombreSede" runat="server" CssClass="Textbox" Width="352px">Sede</asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="height: 10px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style10">
                                            <asp:Label ID="lblNombre" runat="server" CssClass="Label">Nombre</asp:Label>
                                            <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="txtNombre"
                                                Display="None" ErrorMessage="&lt;b&gt;Falta campo requerido&lt;/b&gt;&lt;br /&gt;Se requiere un Nombre de CentroProcesamiento."
                                                SetFocusOnError="True" ValidationGroup="Guardar"></asp:RequiredFieldValidator>
                                            <ajaxToolkit:ValidatorCalloutExtender ID="rfvNombre_ValidatorCalloutExtender" runat="server"
                                                Enabled="True" TargetControlID="rfvNombre">
                                            </ajaxToolkit:ValidatorCalloutExtender>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblCodCentroProcesamiento" runat="server" CssClass="Oculto"></asp:Label>
                                            <asp:TextBox ID="txtNombre" TabIndex="1" runat="server" CssClass="Textbox" Width="354px"
                                                MaxLength="100" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="height: 8px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style10">
                                            <asp:Label ID="lblPCName" runat="server" CssClass="Label">Nombre Equipo</asp:Label>
                                            <asp:RequiredFieldValidator ID="rfvPCName" runat="server" ControlToValidate="txtPCName"
                                                Display="None" ErrorMessage="&lt;b&gt;Falta campo requerido&lt;/b&gt;&lt;br /&gt;Se requiere el Nombre de Equipo del  CentroProcesamiento."
                                                SetFocusOnError="True" ValidationGroup="Guardar"></asp:RequiredFieldValidator>
                                            <ajaxToolkit:ValidatorCalloutExtender ID="rfvPCName_ValidatorCalloutExtender" runat="server"
                                                Enabled="True" TargetControlID="rfvPCName">
                                            </ajaxToolkit:ValidatorCalloutExtender>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPCName" TabIndex="1" runat="server" CssClass="Textbox" Width="352px"
                                                MaxLength="100" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="height: 5px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style10">
                                            <asp:Label ID="lblIPName" runat="server" CssClass="Label">Direccion IP</asp:Label>
                                            <asp:RequiredFieldValidator ID="rfvIPName" runat="server" ControlToValidate="txtIPName"
                                                Display="None" ErrorMessage="&lt;b&gt;Falta campo requerido&lt;/b&gt;&lt;br /&gt;Se requiere una Dirección IP para el Centro de Procesamiento."
                                                SetFocusOnError="True" ValidationGroup="Guardar"></asp:RequiredFieldValidator>
                                            <ajaxToolkit:ValidatorCalloutExtender ID="rfvIPName_ValidatorCalloutExtender" runat="server"
                                                Enabled="True" TargetControlID="rfvIPName">
                                            </ajaxToolkit:ValidatorCalloutExtender>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtIPName" TabIndex="1" runat="server" CssClass="Textbox" Width="352px"
                                                MaxLength="100" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="height: 5px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style10">
                                            <asp:Label ID="lblServerLocalPath" runat="server" CssClass="Label">Direccion local</asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtServerLocalPath" TabIndex="1" runat="server" CssClass="Textbox"
                                                Width="352px" MaxLength="100" />
                                            <asp:RequiredFieldValidator ID="rfvServerLocalPath" runat="server" ControlToValidate="txtServerLocalPath"
                                                Display="None" ErrorMessage="&lt;b&gt;Falta campo requerido&lt;/b&gt;&lt;br /&gt;Se requiere la dirección de la carpeta local del servidor."
                                                SetFocusOnError="True" ValidationGroup="Guardar"></asp:RequiredFieldValidator>
                                            <ajaxToolkit:ValidatorCalloutExtender ID="rfvServerLocalPath_ValidatorCalloutExtender"
                                                runat="server" Enabled="True" TargetControlID="rfvServerLocalPath">
                                            </ajaxToolkit:ValidatorCalloutExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style10">
                                            <asp:Label ID="Label7" runat="server" CssClass="Label">Usa Cargue Local</asp:Label>
                                        </td>
                                        <td>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:RadioButton ID="rbtnsi_Cargue" runat="server" GroupName="Cargue_Local" Text="Si"
                                                CssClass="Label" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:RadioButton ID="rbtnNoCargue" runat="server" GroupName="Cargue_Local" Text="No"
                                                CssClass="Label" />
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="height: 5px">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style10">
                                            <asp:Label ID="lblCalendario" runat="server" CssClass="Label">Calendario transferencias</asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlCalendario" runat="server" CssClass="Textbox" Width="352px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="height: 15px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style10">
                                            <asp:Label ID="lblUnidadMapeo" runat="server" Text="Unidades de Mapeo" CssClass="Label"
                                                Width="191px"></asp:Label>
                                        </td>
                                        <td rowspan="2" style="width: 200px" valign="middle">
                                            <div id="divAddUnidadMapeo" runat="server" class="BotonCambiante" style="width: 20px">
                                                <asp:ImageButton ID="btnAddUnidadMapeo" TabIndex="4" ToolTip="Agregar nueva Unidad de mapeo"
                                                    runat="server" ImageUrl="~/_images/basic/add.png" ValidationGroup="Elemento" />
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="height: 10px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style10">
                                            <asp:Label ID="Label6" runat="server" CssClass="Label">Nombre Aplicación Servidor</asp:Label>
                                            <asp:RequiredFieldValidator ID="rfvAppNameServidor" runat="server" ControlToValidate="txtNameAppServer"
                                                Display="None" ErrorMessage="&lt;b&gt;Falta campo requerido&lt;/b&gt;&lt;br /&gt;Se requiere el nombre de la aplicación de servidor"
                                                Font-Size="Smaller" SetFocusOnError="True" ValidationGroup="Guardar"></asp:RequiredFieldValidator>
                                            <ajaxToolkit:ValidatorCalloutExtender ID="rfvAppNameServidor_ValidatorCalloutExtender"
                                                runat="server" Enabled="True" TargetControlID="rfvAppNameServidor">
                                            </ajaxToolkit:ValidatorCalloutExtender>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNameAppServer" runat="server" CssClass="Textbox" MaxLength="100"
                                                TabIndex="1" Width="352px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="height: 15px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style10">
                                            <asp:Label ID="Label1" runat="server" CssClass="Label">Dirección IP Servidor </asp:Label>
                                            <asp:RequiredFieldValidator ID="rfvServerIP" runat="server" ControlToValidate="txtPathIpServer"
                                                Display="None" ErrorMessage="&lt;b&gt;Falta campo requerido&lt;/b&gt;&lt;br /&gt;Se requiere la dirección IP del servidor."
                                                SetFocusOnError="True" ValidationGroup="Guardar"></asp:RequiredFieldValidator>
                                            <ajaxToolkit:ValidatorCalloutExtender ID="rfvServerIP_ValidatorCalloutExtender" runat="server"
                                                Enabled="True" TargetControlID="rfvServerIP">
                                            </ajaxToolkit:ValidatorCalloutExtender>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPathIpServer" runat="server" CssClass="Textbox" MaxLength="100"
                                                TabIndex="1" Width="352px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="height: 15px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style10">
                                            <asp:Label ID="Label2" runat="server" CssClass="Label">Usa cache local</asp:Label>
                                        </td>
                                        <td>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:RadioButton ID="rbtnsi_Cache" runat="server" Text="Si" GroupName="Cache_Local"
                                                CssClass="Label" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:RadioButton ID="rbtnno_Cache" Text="No" runat="server" Style="text-align: right"
                                                GroupName="Cache_Local" CssClass="Label" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="height: 15px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style10">
                                            <asp:Label ID="Label8" runat="server" CssClass="Label">Usa cache local recursiva</asp:Label>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:RadioButton ID="rbtnUsaCacheRecurSI" runat="server" Text="Si" GroupName="Cache_Recursiva"
                                                CssClass="Label" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:RadioButton ID="rbtnUsaCacheRecurNO" Text="No" runat="server" Style="text-align: right"
                                                GroupName="Cache_Recursiva" CssClass="Label" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="height: 15px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style10">
                                            <asp:Label ID="Label3" runat="server" CssClass="Label">Puerto Servidor</asp:Label>
                                            <asp:RequiredFieldValidator ID="rfvServerPort" runat="server" ControlToValidate="txtPortServer"
                                                Display="None" ErrorMessage="&lt;b&gt;Falta campo requerido&lt;/b&gt;&lt;br /&gt;Se requiere el puerto del servidor."
                                                SetFocusOnError="True" ValidationGroup="Guardar"></asp:RequiredFieldValidator>
                                            <ajaxToolkit:ValidatorCalloutExtender ID="rfvServerPort_ValidatorCalloutExtender"
                                                runat="server" Enabled="True" TargetControlID="rfvServerPort">
                                            </ajaxToolkit:ValidatorCalloutExtender>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPortServer" runat="server" CssClass="Textbox" MaxLength="100"
                                                TabIndex="1" Width="352px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="height: 15px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style10">
                                            <asp:Label ID="lblDefault" runat="server" CssClass="Label">Sede y Centro de Procesamiento por defecto:</asp:Label>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="checkboxDefaultCentro" runat="server" AutoPostBack="True" OnCheckedChanged="Check_Clicked"
                                                Checked="True" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="height: 15px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style10">
                                            <asp:Label ID="lblSedeAsignada" runat="server" CssClass="Label">Sede Asignada</asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlSedeAsig" runat="server" CssClass="Textbox" Width="352px"
                                                AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="height: 15px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style10">
                                            <asp:Label ID="lblCentroAsignado" runat="server" CssClass="Label">Centro de Procesamiento Asignado</asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlCentroProcAsig" runat="server" CssClass="Textbox" Width="352px"
                                                AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="height: 15px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <div style="border: 1px solid #CCCCCC; overflow: auto; height: 200px; width: 715px">
                                                <Miharu:SlygGridView ID="gvUnidadMapeo" runat="server" AutoGenerateColumns="False"
                                                    GridNum="0" CssClass="yui-datatable-theme" EnableSort="True" ClickAction="OnClickNoEvents"
                                                    OnBeginPreSelect="" OnBeginSelect="" OnEndPreSelect="" OnEndSelect="" PreSelectedIndex="-1"
                                                    PreSelectedStyleCssClass="row-PreSelect">
                                                    <AlternatingRowStyle CssClass="alt-data-row"></AlternatingRowStyle>
                                                    <Columns>
                                                        <asp:BoundField DataField="id_Unidad_Mapeo" HeaderText="Cod." ReadOnly="True">
                                                            <ItemStyle Width="1px" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Nombre">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtNombreUnidadMapeo" runat="server" Width="120px" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Carpeta">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtCarpetaUnidadMapeo" runat="server" Width="200px" MaxLength="100" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Unidad">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtUnidadMapeo" runat="server" Width="30px" MaxLength="10" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Usuario">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtUsuarioUnidadMapeo" runat="server" Width="80px" MaxLength="100" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Password">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtPasswordUnidadMapeo" runat="server" Width="80px" MaxLength="100" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Activa">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkActivaUnidadMapeo" runat="server" Width="10px" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Usar Usuario C">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkUsarUsuarioContexto" runat="server" Width="10px" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <itemstyle width="1px" />
                                                                <div class="BotonCambiante" style="width: 20px;">
                                                                    <asp:ImageButton ID="imgbDelete" runat="server" CommandName="Delete" OnClientClick="return confirm('¿Desea eliminar este item?');"
                                                                        ImageUrl="~/_images/basic/delete.png" ToolTip="Eliminar el registro" />
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EditRowStyle CssClass="row-edit"></EditRowStyle>
                                                    <PagerStyle CssClass="pager-stl"></PagerStyle>
                                                    <RowStyle CssClass="nor-data-row"></RowStyle>
                                                    <SelectedRowStyle CssClass="row-edit"></SelectedRowStyle>
                                                </Miharu:SlygGridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="height: 8px">
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                </ajaxToolkit:TabContainer>
            </td>
        </tr>
    </table>
</asp:Content>
