<%@ Page  UICulture="es" Culture="es-MX" Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Main/PopupMasterPage.Master" CodeBehind="P_Salida_Reportes.aspx.vb" Inherits="Miharu.Core.Sitio.Reporte.P_Salida_Reportes" %>
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
                                    <asp:Label ID="Label29" runat="server" CssClass="Label" Text="ID" Width="200px"></asp:Label>
                                    <cc1:DNumber ID="id_Reporte_Salida" runat="server" Enabled="false" 
                                        WaterText="Auto" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align:left" >
                                    <asp:Label ID="Label27" runat="server" CssClass="Label" Text="Nombre Parametro" 
                                        Width="200px"></asp:Label>
                                    <cc1:DTexto ID="Titulo_Salida" runat="server" AutoPostBack="False" 
                                        BackColor_="" CssClass_="" EmptyValueMessage="  *" Heigth="" 
                                        InvalidValueMessage="  El dato no es valido" IsRequiered="True" MaxLength="0" 
                                        MensajeColor="Red" Multiline="SingleLine" Text="" TooltipMessage="" 
                                        ValidationGroup="Guardar" WaterText="" Width="300px" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align:right" >
                                    <asp:ImageButton ID="ImageButton1" runat="server" 
                                        ImageUrl="~/_images/tool/save.png" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align:left" >
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td style="text-align:left" >
                                    <cc1:CoreGridView ID="grdData" runat="server" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Eliminar" ItemStyle-Width="40">
                                                <ItemTemplate>
                                                    <asp:ImageButton runat="server" ImageUrl="~/_images/basic/delete.png" ImageAlign="Middle" ID="imgEliminarItem" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Id_Reporte_Salida" HeaderText="ID" />
                                            <asp:BoundField DataField="Titulo_Salida" HeaderText="Titulo Salida" />
                                        </Columns>
                                    </cc1:CoreGridView>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel> 
</asp:Content>
