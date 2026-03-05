<%@ Page  UICulture="es" Culture="es-MX" Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Main/PopupMasterPage.Master" CodeBehind="Confirmacion.aspx.vb" Inherits="Miharu.Core.Confirmacion" %>

<%@ Register Assembly="Miharu.Core" Namespace="Miharu.Core" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>

<asp:Content ID="cntHead" ContentPlaceHolderID="MasterHead" runat="server">
    <link href="../../_styles/Gridview/GridviewStyles.css" rel="stylesheet" type="text/css" />
    <link href="../../_styles/Default.css" rel="stylesheet" type="text/css" />
    <link href="../../_styles/ModalPopUp/StyleSheetModalPopUp.css" rel="stylesheet" type="text/css" />
    <link href="../../_styles/StyleSheet_DialogBox.css" rel="stylesheet" type="text/css" />   
    <script src="../../_js/CmiGridView.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="cntContent" ContentPlaceHolderID="MasterContent" runat="server">
    <table border="0" >
        <tr>
            <td colspan='3'>
            
              <div style="border: 1px solid #CCCCCC; overflow: auto; height: 100px; width: 520px">
               
                        <table width="500px"  style="text-align: left" border="0">
                            <tr>
                                <td style="width: 350px;">
                                    <asp:Label ID="lblMensaje" runat="server" ForeColor="#006699" Text=""></asp:Label>
                                </td>
                                <td align="center"   style="width: 30px;">
                                    <asp:Image ID="MsgBoxIcono" runat="server" ImageUrl="~/_images/basic/icon-warning.png" />
                                </td>
                            </tr>
                       </table>
              </div>     
               
            </td>
        </tr>
        <tr align="left">
            <td align='center' colspan='3'>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:Button ID="btnAceptar" runat="server" Height="24px" Width="50px" Text="Si" />
                        &nbsp;&nbsp;
                        <asp:Button ID="btnCancelar" runat="server" Height="24px" Width="50px" Text="No" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <%--<tr>
            <td style='height: 10px' colspan='5'>
            </td>
        </tr>--%>
    </table>
</asp:Content>
