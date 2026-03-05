<%@ Page  UICulture="es" Culture="es-MX" Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Main/PopupMasterPage.Master" CodeBehind="P_Roles.aspx.vb" Inherits="Miharu.Core.Sitio.Administracion.P_Roles" %>
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
                        <table style="width:100%;">
                            <tr>
                                <td style="text-align:left">
                                    <cc1:CoreGridView ID="grdData" runat="server" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:BoundField DataField="Documento" HeaderText="Documento" />

                                            <asp:TemplateField HeaderText="Ver Registro" ItemStyle-HorizontalAlign="Center" 
                                                ItemStyle-Width="40">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="Ver_Registro" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Ver Data" ItemStyle-HorizontalAlign="Center" 
                                                ItemStyle-Width="40">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="Ver_Data" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Ver Imagen" ItemStyle-HorizontalAlign="Center" 
                                                ItemStyle-Width="40">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="Ver_Imagen" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Descargar" ItemStyle-HorizontalAlign="Center" 
                                                ItemStyle-Width="40">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="Descargar" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </cc1:CoreGridView>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align:left">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td style="text-align:left">
                                    <asp:ImageButton ID="ImageButton1" runat="server" 
                                        ImageUrl="~/_images/tool/save.png" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel> 
</asp:Content>
