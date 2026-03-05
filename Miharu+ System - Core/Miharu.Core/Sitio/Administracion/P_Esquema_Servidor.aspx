<%@ Page AutoEventWireup="false" CodeBehind="P_Esquema_Servidor.aspx.vb" Culture="es-MX" Inherits="Miharu.Core.Sitio.Administracion.P_Esquema_Servidor" Language="vb" MasterPageFile="~/Main/PopupMasterPage.Master" Title="" UICulture="es" %>
<%@ Register Assembly="Miharu.Core" Namespace="Miharu.Core" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MasterHead" runat="server">
    <link href="../../_styles/Gridview/GridviewStyles.css" rel="stylesheet" type="text/css" />
    <link href="../../_styles/Styles.css" rel="stylesheet" type="text/css" />
    <link href="../../_styles/ModalPopUp/StyleSheetModalPopUp.css" rel="stylesheet" type="text/css" />
    <link href="../../_styles/Default.css" rel="stylesheet" type="text/css" />
    <script src="../../_js/CmiGridView.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MasterContent" runat="server">
    <asp:UpdatePanel ID="upCuerpo" runat="server">
        <ContentTemplate>
            <table style="margin: 10px 10px 10px 10px" class="formTable">
                <tr>
                    <td>
                        <cc1:CoreGridView ID="grdData" runat="server" AutoGenerateColumns="False" 
                            ClickAction="OnClickSelectedPostBack" CssClass="yui-datatable-theme" 
                            EnableSort="True" GridNum="0" PreSelectedIndex="-1" 
                            PreSelectedStyleCssClass="row-PreSelect">
                            <AlternatingRowStyle CssClass="alt-data-row" />
                            <Columns>
                                <asp:BoundField DataField="id_servidor" HeaderText="id_servidor" Visible="false"/>
                                <asp:BoundField DataField="id_Servidor_Tipo" HeaderText="id_Servidor_Tipo" 
                                    Visible="false" />
                                <asp:BoundField DataField="fk_entidad" HeaderText="Id" Visible="false" />
                                <asp:BoundField DataField="Nombre_servidor" HeaderText="Nombre Servidor"/>
                                <asp:BoundField DataField="nombre_servidor_tipo" HeaderText="Tipo" />
                                <asp:BoundField DataField="ipname_servidor" HeaderText="Ip" />
                                <asp:BoundField DataField="ConnectionString_servidor" 
                                    HeaderText="Cadena Conexión" />
                                <asp:BoundField DataField="fk_Calendario" HeaderText="Calendario" />
                            </Columns>
                            <EditRowStyle CssClass="row-edit" />
                            <PagerStyle CssClass="pager-stl" />
                            <RowStyle CssClass="nor-data-row" />
                            <SelectedRowStyle CssClass="row-Select" />
                        </cc1:CoreGridView>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel> 
</asp:Content>
