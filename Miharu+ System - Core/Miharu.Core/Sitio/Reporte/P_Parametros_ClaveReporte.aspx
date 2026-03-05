<%@ Page  UICulture="es" Culture="es-MX" Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Main/PopupMasterPage.Master" CodeBehind="P_Parametros_ClaveReporte.aspx.vb" Inherits="Miharu.Core.Sitio.Reporte.P_Parametros_ClaveReporte" %>
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
                        <table style="width:117%;">
                            <tr>
                                <td style="text-align:left" >
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td style="text-align:left" >
                                    <asp:Label ID="Label29" runat="server" CssClass="Label" Text="Id Reporte" Width="200px"></asp:Label>
                                    <cc1:DNumber ID="fk_Reporte" runat="server" Enabled="false" 
                                        WaterText="Auto" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align:left" >
                                    <asp:Label ID="Label1" runat="server" CssClass="Label" Text="Clave" Width="200px"></asp:Label>
                                    <cc1:DTexto ID="Clave_Reporte" runat="server"/>
                                </td>
                            </tr>

                            <tr>
                                <td style="text-align:left" >
                                    <asp:Label ID="Label2" runat="server" CssClass="Label" Text="Confirmar Clave" Width="200px"></asp:Label>
                                    <cc1:DTexto ID="ConfirmarClave" runat="server" />
                                </td>
                            </tr>
                            
                           
                            <tr>
                                <td style="text-align:left" width="30px" >
                                    <asp:ImageButton ID="Clave" runat="server" 
                                        ImageUrl="~/_images/tool/save.png" />
                                </td>

                            </tr>

                            <tr>
                                <td style="text-align:left" >
                                    &nbsp;</td>
                            </tr>
                            
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel> 
</asp:Content>
