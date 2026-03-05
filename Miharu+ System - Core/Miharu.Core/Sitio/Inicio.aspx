<%@ Page  UICulture="es" Culture="es-MX" Language="vb" AutoEventWireup="false" MasterPageFile="~/Main/FormMasterPage.Master" CodeBehind="Inicio.aspx.vb" Inherits="Miharu.Core.Sitio.Inicio" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="Miharu.Core" Namespace="Miharu.Core" TagPrefix="cc1" %>

<%--<%@ Register assembly="Miharu.MailCenter" namespace="Miharu.MailCenter.Thycotic.Web.UI.WebControls" tagprefix="cc3" %>--%>

<asp:Content ID="cntHead" ContentPlaceHolderID="MasterHead" runat="Server">
    <link href="../../_styles/Styles.css" rel="stylesheet" type="text/css" />
    <link href="../../_styles/ModalPopUp/StyleSheetModalPopUp.css" rel="stylesheet" type="text/css" />
    <link href="../../_styles/Marco/StyleSheetMaster.css" rel="stylesheet" type="text/css" />
    <link href="../../_styles/Tabpanel/TabpanelStyles.css" rel="stylesheet" type="text/css" />
    <link href="../../_styles/Gridview/GridviewStyles.css" rel="stylesheet" type="text/css" />
    <link href="../../_js/windows/themes/default.css" rel="stylesheet" type="text/css" />
    <link href="../../_js/windows/themes/alphacube.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MasterBodyUnique" runat="server">

    <div>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
    
        <table id="table1" style="width:100%;">
            <tr>
                <td align="center">
                    <asp:Image ID="Image1" runat="server" 
                        ImageUrl="~/_images/MiharuCore.png" />
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Verdana" 
                        Text="Bienvenido(a), seleccione una opción del menú."></asp:Label>
                </td>
            </tr>
        </table>
    
    </div>
</asp:Content>
