<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MiharuMasterForm.master" CodeBehind="calendario.aspx.vb" Inherits="Miharu.Security._sitio.administracion.estructura.calendario" %>
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
<asp:Content ID="Content2" ContentPlaceHolderID="Cuerpo" runat="server">
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
                            <asp:Label ID="lblTitulo" runat="server" Text="Calendario" CssClass="Titulo_Principal"></asp:Label>
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
                                                    <asp:BoundField DataField="id_Calendario" HeaderText="Cod." ItemStyle-Width="1" />
                                                    <asp:BoundField DataField="Nombre_Calendario" HeaderText="Nombre" />
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
                                    <table id="tblDetalle" cellspacing="0" cellpadding="0" border="0" style="margin-left: 10px">
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
                                                <asp:Label ID="lblCodCalendario" runat="server" CssClass="Oculto"></asp:Label>
                                                <asp:TextBox ID="txtNombre" TabIndex="1" runat="server" CssClass="Textbox" Width="400px"
                                                    MaxLength="100" />
                                                <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ErrorMessage="&lt;b&gt;Falta campo requerido&lt;/b&gt;&lt;br /&gt;Se requiere un Nombre de Calendario."
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
                                            <td colspan="3" style="height: 8px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <div style="border: 1px solid #CCCCCC; overflow: auto; height: 390px; width: 725px">
                                                    <Miharu:SlygGridView ID="gvHorario" runat="server" AutoGenerateColumns="False" GridNum="1"
                                                        CssClass="yui-datatable-theme" EnableSort="False" ClickAction="OnClickNoEvents"
                                                        Width="680px">
                                                        <AlternatingRowStyle CssClass="alt-data-row"></AlternatingRowStyle>
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Hora" ItemStyle-Font-Bold="True">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblHora" runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Domingo">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkDomingo" runat="server" />
                                                                </ItemTemplate>
                                                                <ItemStyle BackColor="#FFCC66" HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Lunes">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkLunes" runat="server" />
                                                                </ItemTemplate>
                                                                <ItemStyle BackColor="#FFFFCC" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Martes">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkMartes" runat="server" />
                                                                </ItemTemplate>
                                                                <ItemStyle BackColor="#FFFFCC" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Miercoles">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkMiercoles" runat="server" />
                                                                </ItemTemplate>
                                                                <ItemStyle BackColor="#FFFFCC" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Jueves">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkJueves" runat="server" />
                                                                </ItemTemplate>
                                                                <ItemStyle BackColor="#FFFFCC" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Viernes">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkViernes" runat="server" />
                                                                </ItemTemplate>
                                                                <ItemStyle BackColor="#FFFFCC" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Sabado">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkSabado" runat="server" />
                                                                </ItemTemplate>
                                                                <ItemStyle BackColor="#99CCFF" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Festivo">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkFestivo" runat="server" />
                                                                </ItemTemplate>
                                                                <ItemStyle BackColor="#FFCC66" HorizontalAlign="Center" />
                                                            </asp:TemplateField>
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
                                </asp:Panel>
                            </div>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel runat="server" HeaderText="Festivos" ID="tpFestivo">
                        <HeaderTemplate>
                            <table cellpadding="0" cellspacing="0" border="0" style="height: 20px" onclick="ShowOpciones('none')">
                                <tr>
                                    <td>
                                        <asp:Image ID="imgFestivos" runat="server" ImageUrl="~/_images/opciones/festivos.png" />
                                    </td>
                                    <td style="width: 5px" />
                                    <td>
                                        <asp:Label ID="lblFestivos" runat="server" Text="Festivos"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <ContentTemplate>
                            <table style="padding: 5px" cellspacing="5" border="0">
                                <tr>
                                    <td style="height: 30px">
                                        <asp:Label ID="lblYear" runat="server" Text="Filtro: [Año]" CssClass="Label" Width="80px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlYear" runat="server" Width="100px" AutoPostBack="True" CssClass="Textbox">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblFecha" runat="server" Text="Fecha" CssClass="Label" Width="100px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFecha" CssClass="Textbox" runat="server" Width="200px"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="txtFecha_CalendarExtender" runat="server" TargetControlID="txtFecha"
                                            Enabled="True" Format="dd/MM/yyyy">
                                        </ajaxToolkit:CalendarExtender>
                                        <asp:RequiredFieldValidator ID="rfvFecha" runat="server" ErrorMessage="&lt;b&gt;Falta campo requerido&lt;/b&gt;&lt;br /&gt;Debe ingresar una fecha valida."
                                            Display="None" ValidationGroup="Fecha" SetFocusOnError="True" ControlToValidate="txtFecha"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="rfvFecha_ValidatorCalloutExtender" runat="server"
                                            TargetControlID="rfvFecha" Enabled="True">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                        <asp:RegularExpressionValidator ID="revFecha" runat="server" ErrorMessage="&lt;b&gt;Dato invalido&lt;/b&gt;&lt;br /&gt;Debe ingresar una fecha valida."
                                            ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                                            Display="None" ValidationGroup="Fecha" SetFocusOnError="True" ControlToValidate="txtFecha"></asp:RegularExpressionValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="revFecha_ValidatorCalloutExtender" runat="server"
                                            TargetControlID="revFecha" Enabled="True">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                    </td>
                                    <td style="width: 10px;">
                                    </td>
                                    <td align="left" style="width: 405px;">
                                        <div class="BotonCambiante" style="width: 16px;">
                                            <asp:ImageButton ID="btnAddFecha" runat="server" ImageUrl="~/_images/basic/add.png"
                                                ValidationGroup="Fecha" />
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <div style="border: 1px solid #CCCCCC; overflow: auto; height: 395px; width: 735px">
                                            <Miharu:SlygGridView ID="gvFestivo" runat="server" AutoGenerateColumns="False" GridNum="2"
                                                CssClass="yui-datatable-theme" EnableSort="True" ClickAction="OnDblClickSelectedPostBack">
                                                <AlternatingRowStyle CssClass="alt-data-row"></AlternatingRowStyle>
                                                <Columns>
                                                    <asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:yyyy-MMM-dd}" />
                                                    <asp:BoundField DataField="Fecha" DataFormatString="{0:dddd}" HeaderText="Día" />
                                                    <asp:TemplateField ItemStyle-Width="1px">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="delete" runat="server" OnClientClick="return confirm('¿Desea eliminar este item?');"
                                                                CommandName="Delete">Eliminar</asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
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
                </ajaxToolkit:TabContainer>
            </td>
        </tr>
    </table>
</asp:Content>
