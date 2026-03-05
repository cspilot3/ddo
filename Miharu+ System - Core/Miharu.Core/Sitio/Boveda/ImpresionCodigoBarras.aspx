<%@ Page  UICulture="es" Culture="es-MX" Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Main/FormMasterPage.Master" CodeBehind="ImpresionCodigoBarras.aspx.vb" Inherits="Miharu.Core.Sitio.Boveda.ImpresionCodigoBarras" %>

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
    <style type="text/css">
        .style1
        {
            width: 180px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MasterBodyUnique" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="MasterFilter" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="MasterGrid" runat="server">
    <asp:Panel ID="pnlGrilla" runat="server" style="width: 100%;">
        <asp:Label ID="NumRegistros" runat="server" Text="Label" CssClass="Label"></asp:Label>
        <br/>
        
        <cc1:CoreGridView ID="grdData" runat="server" AutoGenerateColumns="true">
        </cc1:CoreGridView>
    </asp:Panel>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="MasterDetail" runat="server">
    <asp:Panel ID="pnlDetalle" runat="server" style="width: 100%;" Visible="true" ScrollBars="Auto" >
        <table style="width:95%">
            <tr>
                <td class="style1">
                    <asp:Label id="Label2" runat="server" CssClass="Label" Width="150px">Entidad:</asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="fk_Entidad" runat="server" AutoPostBack="True" 
                        Width="200px">
                    </asp:DropDownList>
                </td>
                <td rowspan="14" valign="top">
                    <asp:Panel ID="ImpresionPanel" runat="server">
                        <asp:Panel ID="OpcionesPanel" runat="server" CssClass="Label" 
                            GroupingText="Opciones Impresión" Width="300px">
                            <table style="width:100%;">
                                <tr>
                                    <td align="left">
                                        Separador</td>
                                    <td align="left">
                                        Tipo Impresión</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:RadioButton ID="ComaRadioButton" runat="server" Checked="True" 
                                            CssClass="Label" GroupName="Separador" Text="Coma (,)" />
                                    </td>
                                    <td>
                                        <asp:RadioButton ID="EstanteRadioButton" runat="server" Checked="True" 
                                            CssClass="Label" GroupName="Impresion" Text="Estante" 
                                            AutoPostBack="True" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:RadioButton ID="PuntoYComaRadioButton" runat="server" CssClass="Label" 
                                            GroupName="Separador" Text="Punto y Coma (;)" />
                                    </td>
                                    <td>
                                        <asp:RadioButton ID="FilaRadioButton" runat="server" CssClass="Label" 
                                            GroupName="Impresion" Text="Fila" AutoPostBack="True" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:RadioButton ID="TabuladorRadioButton" runat="server" CssClass="Label" 
                                            GroupName="Separador" 
                                            Text="Tabulador (&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;)" />
                                    </td>
                                    <td>
                                        <asp:RadioButton ID="ColumnaRadioButton" runat="server" CssClass="Label" 
                                            GroupName="Impresion" Text="Columna" AutoPostBack="True" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:RadioButton ID="SaltoLineaRadioButton" runat="server" CssClass="Label" 
                                            GroupName="Separador" Text="Salto de línea (ENTER)" />
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                            </table>
                            <br />
                            <asp:MultiView ID="ImpresionMultiView" runat="server">
                                <asp:View ID="EstanteView" runat="server">
                                </asp:View>
                                <asp:View ID="FilaView" runat="server">
                                    <div align="center">
                                        <asp:Label ID="Label3" runat="server" CssClass="Label" Text="Fila:"></asp:Label>
                                        <cc1:DTexto ID="FilaDTexto" runat="server" Width="50px" />
                                    </div>
                                </asp:View>
                                <asp:View ID="ColumnaView" runat="server">
                                    <div align="center">
                                        <asp:Label ID="Label4" runat="server" CssClass="Label" Text="Columna:"></asp:Label>
                                        <cc1:DTexto ID="ColumnaDTexto" runat="server" Width="50px" />
                                    </div>
                                </asp:View>
                            </asp:MultiView>
                            <div align="center">
                                <br />
                                <asp:ImageButton ID="ImprimirEstanteImageButton" runat="server" 
                                    ImageUrl="~/_images/btnPrinter.png" 
                                    ToolTip="Imprimir códigos de barras del estante seleccionado." />
                            </div>
                            <br />
                        </asp:Panel>
                    </asp:Panel>
                    <br />
                    <asp:TextBox ID="sw" runat="server" Width="15px">0</asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <asp:Label ID="LabelSede" runat="server" CssClass="Label" Width="150px">Sede:</asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="fk_Sede" runat="server" AutoPostBack="True" Width="200px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <asp:Label ID="LabelSede0" runat="server" CssClass="Label" Width="150px">Bóveda:</asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="fk_Boveda" runat="server" AutoPostBack="True" 
                        Width="200px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <asp:Label ID="LabelSede1" runat="server" CssClass="Label" Width="150px">Bóveda Sección:</asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="fk_Boveda_Seccion" runat="server" AutoPostBack="True" 
                        Width="200px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <asp:Label ID="LabelSede2" runat="server" CssClass="Label" Width="150px">Id Bóveda Estante:</asp:Label>
                </td>
                <td>
                    <cc1:DTexto ID="id_Boveda_Estante" runat="server" Enabled="False" 
                        IsRequiered="True" ValidationGroup="Guardar" Width="100px" />
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <asp:Label ID="LabelSede3" runat="server" CssClass="Label" Width="150px">Código Bóveda Estante:</asp:Label>
                </td>
                <td>
                    <cc1:DTexto ID="Codigo_Boveda_Estante" runat="server" IsRequiered="True" 
                        ValidationGroup="Guardar" Width="150px" />
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <asp:Label ID="LabelSede4" runat="server" CssClass="Label" Width="150px">Filas:</asp:Label>
                </td>
                <td>
                    <cc1:DTexto ID="Filas_Boveda_Estante" runat="server" IsRequiered="True" 
                        ValidationGroup="Guardar" Width="50px" />
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <asp:Label ID="LabelSede5" runat="server" CssClass="Label" Width="150px">Columnas:</asp:Label>
                </td>
                <td>
                    <cc1:DTexto ID="Columnas_Boveda_Estante" runat="server" IsRequiered="True" 
                        ValidationGroup="Guardar" Width="50px" />
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <asp:Label ID="LabelSede6" runat="server" CssClass="Label" Width="150px">Profundidades:</asp:Label>
                </td>
                <td>
                    <cc1:DTexto ID="Profundidades_Boveda_Estante" runat="server" IsRequiered="True" 
                        ValidationGroup="Guardar" Width="50px" />
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <asp:Label ID="LabelSede7" runat="server" CssClass="Label" Width="150px">Largo Estante:</asp:Label>
                </td>
                <td>
                    <cc1:DTexto ID="Largo_Boveda_Estante" runat="server" IsRequiered="True" 
                        ValidationGroup="Guardar" Width="50px" />
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <asp:Label ID="LabelSede8" runat="server" CssClass="Label" Width="150px">Ancho Estante:</asp:Label>
                </td>
                <td>
                    <cc1:DTexto ID="Ancho_Boveda_Estante" runat="server" IsRequiered="True" 
                        ValidationGroup="Guardar" Width="50px" />
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <asp:Label ID="LabelSede9" runat="server" CssClass="Label" Width="150px">Alto Estante:</asp:Label>
                </td>
                <td>
                    <cc1:DTexto ID="Alto_Boveda_Estante" runat="server" IsRequiered="True" 
                        ValidationGroup="Guardar" Width="50px" />
                </td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    </asp:Panel> 
</asp:Content>
