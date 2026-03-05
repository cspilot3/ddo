<%@ Page  UICulture="es" Culture="es-MX" Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Main/PopupMasterPage.Master" CodeBehind="P_Parametros_Reportes.aspx.vb" Inherits="Miharu.Core.Sitio.Reporte.PParametrosReportes" %>
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
                                    <cc1:DNumber ID="id_Reporte_Parametro_" runat="server" Enabled="false" 
                                        WaterText="Auto" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align:left" >
                                    <asp:Label ID="Label27" runat="server" CssClass="Label" Text="Nombre Parametro" 
                                        Width="200px"></asp:Label>
                                    <cc1:DTexto ID="Nombre_Reporte_Parametro" runat="server" AutoPostBack="False" 
                                        BackColor_="" CssClass_="" EmptyValueMessage="  *" Heigth="" 
                                        InvalidValueMessage="  El dato no es valido" IsRequiered="True" MaxLength="0" 
                                        MensajeColor="Red" Multiline="SingleLine" Text="" TooltipMessage="" 
                                        ValidationGroup="Guardar" WaterText="" Width="300px" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align:left" >
                                    <asp:Label ID="Label1" runat="server" CssClass="Label" Text="Etiqueta Parametro" 
                                        Width="200px"></asp:Label>
                                    <cc1:DTexto ID="Etiqueta_Reporte_Parametro" runat="server" AutoPostBack="False" 
                                        BackColor_="" CssClass_="" EmptyValueMessage="  *" Heigth="" 
                                        InvalidValueMessage="  El dato no es valido" IsRequiered="True" MaxLength="0" 
                                        MensajeColor="Red" Multiline="SingleLine" Text="" TooltipMessage="" 
                                        ValidationGroup="Guardar" WaterText="" Width="300px" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align:left" >
                                    <asp:Label ID="Label28" runat="server" CssClass="Label" Text="Tipo" 
                                        Width="200px"></asp:Label>
                                    <asp:DropDownList ID="fk_tipo_Parametro" runat="server" 
                                        Width="200px" AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr id="ConsultaTR" runat="server">
                                <td style="text-align:left" >
                                    <asp:Label ID="Label2" runat="server" CssClass="Label" Text="Consulta" 
                                        Width="200px"></asp:Label>
                                    <cc1:DTexto ID="Consulta_Lista" runat="server" AutoPostBack="False" 
                                        BackColor_="" CssClass_="" EmptyValueMessage="  *" Heigth="" 
                                        InvalidValueMessage="  El dato no es valido" IsRequiered="True" MaxLength="0" 
                                        MensajeColor="Red" Multiline="MultiLine" Text="" TooltipMessage="" 
                                        ValidationGroup="Guardar" WaterText="" Width="300px" Height="100" />
                                </td>
                            </tr>
                            <tr id="EtiquetaTR" runat="server">
                                <td style="text-align:left" >
                                    <asp:Label ID="Label3" runat="server" CssClass="Label" Text="Columna Etiqueta" 
                                        Width="200px"></asp:Label>
                                    <cc1:DTexto ID="Columna_Etiqueta_Lista" runat="server" AutoPostBack="False" 
                                        BackColor_="" CssClass_="" EmptyValueMessage="  *" Heigth="" 
                                        InvalidValueMessage="  El dato no es valido" IsRequiered="True" MaxLength="0" 
                                        MensajeColor="Red" Multiline="SingleLine" Text="" TooltipMessage="" 
                                        ValidationGroup="Guardar" WaterText="" Width="300px" />
                                </td>
                            </tr>
                            <tr id="ValorTR" runat="server">
                                <td style="text-align:left" >
                                    <asp:Label ID="Label4" runat="server" CssClass="Label" Text="Columna Valor" 
                                        Width="200px"></asp:Label>
                                    <cc1:DTexto ID="Columna_Valor_Lista" runat="server" AutoPostBack="False" 
                                        BackColor_="" CssClass_="" EmptyValueMessage="  *" Heigth="" 
                                        InvalidValueMessage="  El dato no es valido" IsRequiered="True" MaxLength="0" 
                                        MensajeColor="Red" Multiline="SingleLine" Text="" TooltipMessage="" 
                                        ValidationGroup="Guardar" WaterText="" Width="300px" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align:left" >
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

                                            <asp:BoundField DataField="Id_Reporte_Parametro" HeaderText="ID" />
                                            <asp:BoundField DataField="Nombre_Parametro" HeaderText="Nombre" />
                                            <asp:BoundField DataField="Etiqueta_Parametro" HeaderText="Etiqueta" />
                                            <asp:BoundField DataField="Tipo" HeaderText="Tipo Campo" />
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
