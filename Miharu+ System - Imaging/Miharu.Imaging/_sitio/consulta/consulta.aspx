<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MiharuMasterForm.master"
    CodeBehind="consulta.aspx.vb" Inherits="Miharu.Imaging.consulta" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="Miharu.Web.Controls" Namespace="Miharu.Web.Controls" TagPrefix="Miharu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../_styles/StyleSheet_Marco.css" rel="stylesheet" type="text/css" />
    <link href="../../_styles/StyleSheet_Miharu.css" rel="stylesheet" type="text/css" />
    <link href="../../_styles/StyleSheet_Menu.css" rel="stylesheet" type="text/css" />
    <link href="../../_styles/gridview/GridviewStyles.css" rel="stylesheet" type="text/css" />
    <link href="../../_styles/StyleSheet_DialogBox.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Cuerpo" runat="server">
    <table style="width: 780px;" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td style="height: 10px">
            </td>
        </tr>
        <tr>
            <td align="left">
                <table cellpadding="0" cellspacing="0" border="0" style="width: 780px; margin-left: 4px;">
                    <tr>
                        <td>
                            <asp:Panel ID="pnlParametros" runat="server" Style="border: 1px solid #333333; background-color: #F0F0F0"
                                DefaultButton="ibBuscar">
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td style="height: 5px" colspan="11">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 10px">
                                            &#160;
                                        </td>
                                        <td class="Label" style="width: 80px">
                                            Campo
                                        </td>
                                        <td style="width: 10px">
                                            &#160;
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlCampo_1" runat="server" CssClass="Textbox" Width="230px"
                                                TabIndex="2" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 10px">
                                            &#160;
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlOperador_1" runat="server" CssClass="Textbox" Width="50px"
                                                TabIndex="3">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 10px">
                                            &#160;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtParametro_1" runat="server" CssClass="Textbox" Width="240px"
                                                TabIndex="4" MaxLength="900"></asp:TextBox>
                                        </td>
                                        <td style="width: 10px">
                                            &#160;
                                        </td>
                                        <td rowspan="2">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <div class="BotonCambiante">
                                                        <asp:ImageButton ID="ibBuscar" runat="server" ImageUrl="~/_images/opciones/document_find.png"
                                                            ToolTip="Iniciar la búsqueda" ValidationGroup="Guardar" />
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td rowspan="2" style="width: 10px">
                                            &#160;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 5px" colspan="11">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 10px">
                                            &#160;
                                        </td>
                                        <td class="Label" style="width: 80px">
                                            Campo
                                        </td>
                                        <td style="width: 10px">
                                            &#160;
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlCampo_2" runat="server" CssClass="Textbox" Width="230px"
                                                TabIndex="6" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 10px">
                                            &#160;
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlOperador_2" runat="server" CssClass="Textbox" Width="50px"
                                                TabIndex="7">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 10px">
                                            &#160;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtParametro_2" runat="server" CssClass="Textbox" Width="240px"
                                                TabIndex="8" MaxLength="900"></asp:TextBox>
                                        </td>
                                        <td style="width: 10px">
                                            &#160;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 5px" colspan="11">
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                        <td style="width: 180px" align="center" valign="top">
                            <asp:Label ID="lblTitulo" runat="server" Text="Búsqueda" CssClass="Titulo_Principal"></asp:Label><br />
                            <br />
                            <asp:RadioButton ID="rbAnd" runat="server" Text="Y" CssClass="Label" Checked="True"
                                GroupName="OperadorLogico" />&#160;&#160;
                            <asp:RadioButton ID="rbOr" runat="server" Text="O" CssClass="Label" GroupName="OperadorLogico" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="left" valign="top">
                <table border="0" cellspacing="2">
                    <tr>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Panel ID="pnlResultadosMarco" runat="server">
                                <table border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="center">
                                            <div class="EncabezadoVertical" style="height: 175px">
                                                Resultados</div>
                                        </td>
                                        <td>
                                            <asp:Panel ID="pnlResultados" runat="server" Style="border: 1px solid #333333" Width="745px"
                                                Height="175px" ScrollBars="Auto">
                                                <Miharu:SlygGridView ID="ResultadosDataGridView" runat="server" ClickAction="OnClickSelectedPostBack"
                                                    AutoGenerateColumns="False" CssClass="yui-datatable-theme" EnableSort="True"
                                                    GridNum="0">
                                                    <PagerStyle CssClass="pager-stl"></PagerStyle>
                                                    <EditRowStyle CssClass="row-edit"></EditRowStyle>
                                                    <SelectedRowStyle CssClass="row-edit"></SelectedRowStyle>
                                                    <AlternatingRowStyle CssClass="alt-data-row"></AlternatingRowStyle>
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Tipo">
                                                            <ItemTemplate>
                                                                <asp:Image ID="imgTipo" runat="server" ImageUrl="~/_images/basic/folder.png" />
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="Flujo_Estado" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="Entidad" DataField="Nombre_Entidad" />
                                                        <asp:BoundField HeaderText="Proyecto" DataField="Nombre_Proyecto" />
                                                        <asp:BoundField HeaderText="Unidad" DataField="Nombre_Esquema" />
                                                        <asp:BoundField HeaderText="Tipología" DataField="Nombre_Documento" />
                                                        <asp:BoundField HeaderText="" DataField="Data_1" />
                                                        <asp:BoundField HeaderText="" DataField="Data_2" />
                                                        <asp:BoundField HeaderText="" DataField="Data_3" />
                                                    </Columns>
                                                    <RowStyle CssClass="nor-data-row"></RowStyle>
                                                </Miharu:SlygGridView>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Panel ID="pnlTipologiasMarco" runat="server">
                                <table border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="center">
                                            <div class="EncabezadoVertical" style="height: 175px">
                                                Tipologías</div>
                                        </td>
                                        <td>
                                            <asp:Panel ID="pnlTipologias" runat="server" Style="border: 1px solid #333333;" Width="745px"
                                                Height="175px" ScrollBars="Auto">
                                                <Miharu:SlygGridView ID="TipologiasDataGridView" runat="server" ClickAction="OnClickSelectedPostBack"
                                                    AutoGenerateColumns="False" CssClass="yui-datatable-theme" EnableSort="True"
                                                    GridNum="1">
                                                    <PagerStyle CssClass="pager-stl"></PagerStyle>
                                                    <EditRowStyle CssClass="row-edit"></EditRowStyle>
                                                    <SelectedRowStyle CssClass="row-edit"></SelectedRowStyle>
                                                    <AlternatingRowStyle CssClass="alt-data-row"></AlternatingRowStyle>
                                                    <Columns>
                                                        <asp:BoundField HeaderText="Folder" DataField="fk_Folder" ItemStyle-Width="80px" />
                                                        <asp:BoundField HeaderText="" DataField="id_Documento" ItemStyle-Width="10px" />
                                                        <asp:TemplateField HeaderText="" ItemStyle-Width="20px">
                                                            <ItemTemplate>
                                                                <asp:Image ID="imgLock" runat="server" ImageUrl="~/_images/basic/lock.png" />
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="Flujo_Estado" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="Tipología" DataField="Nombre_Documento" ItemStyle-Width="400px" />
                                                        <asp:BoundField HeaderText="Archivos" DataField="Files" ItemStyle-Width="10px" />
                                                    </Columns>
                                                    <RowStyle CssClass="nor-data-row"></RowStyle>
                                                </Miharu:SlygGridView>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Panel ID="pnlDataMarco" runat="server">
                                <table border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="center">
                                            <div class="EncabezadoVertical" style="height: 125px;">
                                                Data</div>
                                        </td>
                                        <td>
                                            <asp:Panel ID="pnlData" runat="server" Style="border: 1px solid #333333;" Width="745px"
                                                Height="125px" ScrollBars="Auto">
                                                <Miharu:SlygGridView ID="DataDataGridView" runat="server" ClickAction="OnDblClickSelectedPostBack"
                                                    AutoGenerateColumns="False" CssClass="yui-datatable-theme" EnableSort="True"
                                                    GridNum="2">
                                                    <PagerStyle CssClass="pager-stl"></PagerStyle>
                                                    <EditRowStyle CssClass="row-edit"></EditRowStyle>
                                                    <SelectedRowStyle CssClass="row-edit"></SelectedRowStyle>
                                                    <AlternatingRowStyle CssClass="alt-data-row"></AlternatingRowStyle>
                                                    <Columns>
                                                    </Columns>
                                                    <RowStyle CssClass="nor-data-row"></RowStyle>
                                                </Miharu:SlygGridView>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
