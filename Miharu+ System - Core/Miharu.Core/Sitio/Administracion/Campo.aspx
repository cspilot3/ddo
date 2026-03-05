<%@ Page UICulture="es" Culture="es-MX" Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Main/FormMasterPage.Master" CodeBehind="Campo.aspx.vb"
    Inherits="Miharu.Core.Sitio.Administracion.Campo" %>

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
                    <asp:DropDownList ID="ddlEntidad" runat="server" AutoPostBack="True" Width="200px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Proyecto" CssClass="Label"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlProyecto" runat="server" AutoPostBack="True" Width="200px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="Esquema" CssClass="Label"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlEsquema" runat="server" AutoPostBack="True" Width="200px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="Documento" CssClass="Label"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlDocumento" runat="server" Width="300px">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MasterGrid" runat="server">
    <asp:Panel ID="pnlGrilla" runat="server" Style="width: 100%;">
        <asp:Label ID="NumRegistros" runat="server" Text="Label" CssClass="Label"></asp:Label>
        <br />
        <cc1:CoreGridView ID="grdData" runat="server" AutoGenerateColumns="false" OnSelectedIndexChanged="CargarDetalleCampo">
            <Columns>
                <asp:BoundField DataField="Entidad" HeaderText="Entidad" />
                <asp:BoundField DataField="Proyecto" HeaderText="Proyecto" />
                <asp:BoundField DataField="Esquema" HeaderText="Esquema" />
                <asp:BoundField DataField="Documento" HeaderText="Documento" />
                <asp:BoundField DataField="id_campo" HeaderText="id_campo" />
                <asp:BoundField DataField="Nombre_Campo" HeaderText="Nombre_Campo" />
                <asp:BoundField DataField="Tipo_Campo" HeaderText="Tipo_Campo" />
                <asp:BoundField DataField="CampoBusqueda" HeaderText="CampoBusqueda" />
                <asp:BoundField DataField="id_Documento" HeaderText="id_Documento" Visible="false" />
                <asp:BoundField DataField="fk_Entidad" HeaderText="fk_Entidad" Visible="false" />
                <asp:BoundField DataField="fk_Campo_Tipo" HeaderText="fk_Campo_Tipo" Visible="false" />
                <asp:BoundField DataField="fk_Campo_Busqueda" HeaderText="fk_Campo_Busqueda" Visible="false" />
                <asp:BoundField DataField="Es_Campo_Folder" HeaderText="Es_Campo_Folder" Visible="false" />
                <asp:BoundField DataField="Es_Obligatorio_Campo" HeaderText="Es_Obligatorio_Campo" Visible="false" />
                <asp:BoundField DataField="Length_Campo" HeaderText="Length_Campo" Visible="false" />
                <asp:BoundField DataField="Es_Exportable" HeaderText="Es_Exportable" Visible="false" />
                <asp:BoundField DataField="Eliminado_Campo" HeaderText="Eliminado_Campo" Visible="false" />
                <asp:BoundField DataField="Es_Campo_Busqueda" HeaderText="Es_Campo_Busqueda" Visible="false" />
                <asp:BoundField DataField="Length_Min_Campo" HeaderText="Length_Min_Campo" Visible="false" />
                <asp:BoundField DataField="Usa_Decimales" HeaderText="Usa_Decimales" Visible="false" />
                <asp:BoundField DataField="Caracter_Decimal" HeaderText="Caracter_Decimal" Visible="false" />
                <asp:BoundField DataField="Cantidad_Decimales" HeaderText="Cantidad_Decimales" Visible="false" />
                <asp:BoundField DataField="Body_Query" HeaderText="Body_Query" Visible="false" />
                <asp:BoundField DataField="Validar_Registros" HeaderText="Validar_Registros" Visible="false" />
                <asp:BoundField DataField="Validar_Totales" HeaderText="Validar_Totales" Visible="false" />
                <asp:BoundField DataField="Valor_por_Defecto" HeaderText="Valor_por_Defecto" Visible="false" />
                <asp:BoundField DataField="fk_Usuario_Log" HeaderText="fk_Usuario_Log" Visible="false" />
                <asp:BoundField DataField="Fecha_Log" HeaderText="Fecha_Log" Visible="false" />
            </Columns>
        </cc1:CoreGridView>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="MasterDetail" runat="server">
    <asp:Panel ID="pnlDetalle" runat="server" Style="width: 100%;" Visible="true">
        <table style="width: 90%">
            <tr>
                <td style="width: 200px">
                    <asp:Label ID="lblIdCampo" runat="server" CssClass="Label" Text="Id Campo"></asp:Label>
                </td>
                <td>
                    <cc1:DNumber ID="id_Campo" runat="server" Width="100px" Enabled="False" />
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    <asp:Label ID="lblEntidad" runat="server" CssClass="Label" Text="Entidad"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="fk_Entidad" runat="server" Width="200px" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    <asp:Label ID="lblDocumento2" runat="server" CssClass="Label" Text="Proyecto"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="fk_proyecto" runat="server" AutoPostBack="True" Width="200px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    <asp:Label ID="lblDocumento1" runat="server" CssClass="Label" Text="Esquema"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="fk_esquema" runat="server" AutoPostBack="True" Width="200px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    <asp:Label ID="lblDocumento" runat="server" CssClass="Label" Text="Documento"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="fk_Documento" runat="server" Width="300px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    <asp:Label ID="lblDocumento0" runat="server" CssClass="Label" Text="Tipo Campo"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="fk_Campo_Tipo" runat="server" Width="200px">
                    </asp:DropDownList>
                    <asp:ImageButton ID="TablaAsodiadaImageButton" runat="server" ImageUrl="~/_images/find.png" />
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    <asp:Label ID="lblCampoLista" runat="server" CssClass="Label" Text="Campo Lista"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="fk_Campo_Lista" runat="server" Width="200px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    <asp:Label ID="lblCampoBusqueda" runat="server" CssClass="Label" Text="Es Campo Búsqueda"></asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="Es_Campo_Busqueda" runat="server" CssClass="Label" AutoPostBack="True" />
                    <cc1:DNumber ID="fk_Campo_Busqueda" runat="server" Width="0" Enabled="False" />
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    <asp:Label ID="lblNombreCampo" runat="server" CssClass="Label" Text="Nombre Campo"></asp:Label>
                </td>
                <td>
                    <cc1:DTexto ID="Nombre_Campo" runat="server" Width="200px" IsRequiered="True" ValidationGroup="Guardar" />
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    <asp:Label ID="lblCampoFolder" runat="server" CssClass="Label" Text="Es Campo Folder"></asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="Es_Campo_Folder" runat="server" CssClass="Label" />
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    <asp:Label ID="lblCampoObligatorio" runat="server" CssClass="Label" Text="Es Campo Obligatorio"></asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="Es_Obligatorio_Campo" runat="server" CssClass="Label" />
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    <asp:Label ID="lblTamanoCampo" runat="server" CssClass="Label" Text="Tamaño Campo"></asp:Label>
                </td>
                <td>
                    <cc1:DNumber ID="Length_Campo" runat="server" Width="100px" IsRequiered="True" MaxLength="3" ValidationGroup="Guardar" />
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    <asp:Label ID="lblEsExportable" runat="server" CssClass="Label" Text="Es Exportable"></asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="Es_Exportable" runat="server" CssClass="Label" />
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    <asp:Label ID="lblEliminado" runat="server" CssClass="Label" Text="Eliminado"></asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="Eliminado_Campo" runat="server" CssClass="Label" />
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    <asp:Label CssClass="Label" ID="lblTamanoMInCampo" runat="server" Text="Tamaño Minimo Campo"></asp:Label>
                </td>
                <td>
                    <cc1:DNumber ID="Length_Min_Campo" runat="server" Width="100px" IsRequiered="False" MaxLength="3" ValidationGroup="Guardar" />
                </td>
            </tr>
            <tr>
                <td style="width: 100px">
                    <asp:Label ID="lblUsaDecimales" runat="server" CssClass="Label" Text="Usa Decimales"></asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="Usa_Decimales" runat="server" CssClass="Label" />
                </td>
            </tr>
            <tr>
                <td style="width: 100px">
                    <asp:Label ID="lblCaracterDecimal" runat="server" CssClass="Label" Text="Caracter Decimal"></asp:Label>
                </td>
                <td>
                    <cc1:DTexto ID="Caracter_Decimal" runat="server" Width="50px" IsRequiered="False" ValidationGroup="Guardar" />
                </td>
            </tr>
            <tr>
                <td style="width: 100px">
                    <asp:Label ID="lblCantidadDecimales" runat="server" CssClass="Label" Text="Cantidad Decimales"></asp:Label>
                </td>
                <td>
                    <cc1:DNumber ID="Cantidad_Decimales" runat="server" Width="50px" IsRequiered="False" MaxLength="3" ValidationGroup="Guardar" />
                </td>
            </tr>
            <tr>
                <td style="width: 100px">
                    <asp:Label ID="lblBody_Query" runat="server" CssClass="Label" Text="Body Query"></asp:Label>
                </td>
                <td>
                    <cc1:DTexto ID="Body_Query" runat="server" Width="100px" IsRequiered="False" ValidationGroup="Guardar" />
                </td>
            </tr>
            <tr>
                <td style="width: 100px">
                    <asp:Label ID="lblValidar_Registros" runat="server" CssClass="Label" Text="Validar Registros"></asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="Validar_Registros" runat="server" CssClass="Label" />
                </td>
            </tr>
            <tr>
                <td style="width: 100px">
                    <asp:Label ID="lblValidar_Totales" runat="server" CssClass="Label" Text="Validar Totales"></asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="Validar_Totales" runat="server" CssClass="Label" />
                </td>
            </tr>
            <tr>
                <td style="width: 100px">
                    <asp:Label ID="lblValor_por_Defecto" runat="server" CssClass="Label" Text="Valor por Defecto"></asp:Label>
                </td>
                <td>
                    <cc1:DTexto ID="Valor_por_Defecto" runat="server" Width="50px" IsRequiered="False" ValidationGroup="Guardar" />
                </td>
            </tr>
            <tr>
                <td>
                    <cc1:DTexto ID="fk_Usuario_Log" runat="server" Width="50px" IsRequiered="False" ValidationGroup="Guardar" Visible="false" />
                </td>
                <td>
                    <cc1:DFecha ID="Fecha_Log" runat="server" IsRequiered="False" ValidationGroup="Guardar" Visible="False" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
