<%@ Page  UICulture="es" Culture="es-MX" Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Main/PopupMasterPage.Master" CodeBehind="p_Campo.aspx.vb" Inherits="Miharu.Core.Sitio.Administracion.p_Campo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
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
            <table border="0" cellpadding="0" cellspacing="0" style="margin: 10px 10px 10px 10px" class="formTable">
                <tr>
                    <td>
                        <cc1:CoreGridView ID="grdData" runat="server" AutoGenerateColumns="false" ClickAction="OnClickSelectedPostBack">
                            <Columns>
                                <asp:BoundField DataField="id_Campo_Busqueda" HeaderText="Id" />
                                <asp:BoundField DataField="Nombre_Campo_Busqueda" HeaderText="Nombre" />
                                <asp:BoundField DataField="Nombre_Campo_Tipo" HeaderText="Tipo" />
                            </Columns>
                        </cc1:CoreGridView>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel> 
</asp:Content>
