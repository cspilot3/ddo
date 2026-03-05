<%@ Page  UICulture="es" Culture="es-MX" Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Main/FormMasterPage.Master" CodeBehind="PlantillasEstantes.aspx.vb" Inherits="Miharu.Core.Sitio.PlantillasEstante.PlantillasEstantes" %>

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
            width: 164px;
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
        
        <cc1:CoreGridView id="grdData" runat="server" AutoGenerateColumns="False">
            <Columns>
				<asp:BoundField DataField="id_Plantilla_Estante" SortExpression="id_Plantilla_Estante" HeaderText="C&#243;digo"></asp:BoundField>
				<asp:BoundField DataField="Nombre_Plantilla_Estante" SortExpression="Nombre_Plantilla_Estante" HeaderText="Nombre"></asp:BoundField>
				<asp:BoundField DataField="Filas_Plantilla_Estante" SortExpression="Filas_Plantilla_Estante" HeaderText="Filas"></asp:BoundField>
				<asp:BoundField DataField="Columnas_Plantilla_Estante" SortExpression="Columnas_Plantilla_Estante" HeaderText="Cols."></asp:BoundField>
				<asp:BoundField DataField="Profundidades_Plantilla_Estante" SortExpression="Profundidades_Plantilla_Estante" HeaderText="Prof."></asp:BoundField>
				<asp:BoundField DataField="Largo_Plantilla_Estante" SortExpression="Largo_Plantilla_Estante" HeaderText="Largo (cms)"></asp:BoundField>
				<asp:BoundField DataField="Ancho_Plantilla_Estante" SortExpression="Ancho_Plantilla_Estante" HeaderText="Ancho (cms)"></asp:BoundField>
				<asp:BoundField DataField="Alto_Plantilla_Estante" SortExpression="Alto_Plantilla_Estante" HeaderText="Alto (cms)"></asp:BoundField>
			</Columns>
        </cc1:CoreGridView>
    </asp:Panel>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="MasterDetail" runat="server">
    <asp:Panel ID="pnlDetalle" runat="server" style="width: 100%;" Visible="true" ScrollBars="Auto" >
        <table style="width:90%">
            <tr>
                <td>
                    <table width="100%">
                        <tr>
                            <td class="style1">
                                <asp:Label id="Label2" runat="server" CssClass="Label" Width="150px">Nombre:</asp:Label>
                                <cc1:DNumber ID="txtId" runat="server" Width="10px" Visible="False" />
                            </td>
                            <td>
                                <cc1:DTexto ID="txtNombre" runat="server" Width="300px" IsRequiered="True" 
                                    ValidationGroup="Guardar" />
                            </td>
                        </tr>
                    </table>
                    
                </td>
            </tr>

            <tr>
                <td>
                    <table border="0" cellpadding="1" cellspacing="1" width="100%">
                        <tr>
                            <td width="33%" align="center">
                                <asp:Label id="lblAlto" runat="server" CssClass="Label">Alto</asp:Label>
                                <cc1:DNumber ID="txtAlto" runat="server" IsRequiered="True" Width="80px" 
                                    MaxLength="5" TooltipMessage="" ValidationGroup="Guardar" 
                                    AceptaPuntoFlotante="True" />
                                <asp:Label ID="lblCms4" runat="server" CssClass="Label">cms.</asp:Label>
                            </td>
                            
                            <td width="34%" align="center">
                                <asp:Label id="lblAncho" runat="server" CssClass="Label">Ancho</asp:Label>
                                <cc1:DNumber ID="txtAncho" runat="server" IsRequiered="True" Width="80px" 
                                    MaxLength="5" TooltipMessage="" ValidationGroup="Guardar" 
                                    AceptaPuntoFlotante="True" />
                                <asp:Label id="lblCms3" runat="server" CssClass="Label">cms.</asp:Label>
                            </td>

                            <td width="33%" align="center">
                                <asp:Label id="lblLargo" runat="server" CssClass="Label">Profundidad</asp:Label>
                                <cc1:DNumber ID="txtProfundidad" runat="server" Width="80px" IsRequiered="True" 
                                    MaxLength="5" TooltipMessage="" ValidationGroup="Guardar" />
                                <asp:Label id="lblCms2" runat="server" CssClass="Label">cms.</asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>

            <tr>
                <td>
                    &nbsp;</td>
            </tr>

            <tr>
                <td class="Titulo_Terceario">
                    Elementos</td>
            </tr>

            <tr>
                <td>
                    <table border="0" cellpadding="2" cellspacing="2" width="100%">
                            <tr>
                                <td width="33%" align="center"><asp:LinkButton id="lnkbAgregarFilas" runat="server" 
                                        CssClass="Label">Agregar fila</asp:LinkButton>&nbsp;<asp:ImageButton 
                                        ID="imgAddFila" runat="server" ImageUrl="~/_images/basic/check.png" />
                                </td>
                                <td width="34%" align="center"><asp:LinkButton id="lnkbAgregarColumnas" 
                                        runat="server" CssClass="Label">Agregar columna</asp:LinkButton>&nbsp;<asp:ImageButton 
                                        ID="imgAddColumna" runat="server" ImageUrl="~/_images/basic/check.png" />
                                </td>
                                <td width="33%" align="center"><asp:LinkButton id="lnkbAgregarProfundidades" 
                                        runat="server" CssClass="Label">Agregar profundidad</asp:LinkButton>&nbsp;<asp:ImageButton 
                                        ID="imgAddProfundidad" runat="server" ImageUrl="~/_images/basic/check.png" />
                                </td>
                            </tr>

                            <tr>
                                <td align="center" valign="top">
                                    <cc1:CoreGridView id="dtgFilas" AutoGenerateColumns="False" Width="95px" 
                                        runat="server" GridNum="0">
                                        <AlternatingRowStyle CssClass="alt-data-row" />
                                        <Columns>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="" ItemStyle-Width="15px">
                                                <ItemTemplate>
                                                    <asp:ImageButton runat="server" ImageUrl="~/_images/basic/delete.png" ImageAlign="Middle" ID="imgEliminarItem" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="id_Plantilla_Estante_Fila" HeaderText="id" SortExpression="id_Plantilla_Estante_Fila" ItemStyle-Width="15px" />
                                            <asp:TemplateField HeaderText="Longitud" ItemStyle-Width="50px">
                                                <ItemTemplate>
                                                    <cc1:DNumber ID="txtLongitud" runat="server" Width="50px" MaxLength="5" IsRequiered="true" ValidationGroup="Guardar" TooltipMessage=" "></cc1:DNumber>
                                                    <asp:Label ID="lblLongitud" runat="server">cms.</asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Es Flo." ItemStyle-Width="15px">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkEsFlotante" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </cc1:CoreGridView>
                                </td>
                                
                                <td align="center" valign="top">
                                    <cc1:CoreGridView id="dtgColumnas" runat="server" Width="80px" 
                                        AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="" ItemStyle-Width="15">
                                                <ItemTemplate>
                                                    <asp:ImageButton runat="server" ImageUrl="~/_images/basic/delete.png" ImageAlign="Middle" ID="imgEliminarItem" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
								            <asp:BoundField DataField="id_Plantilla_Estante_Columna" SortExpression="id_Plantilla_Estante_Columna" HeaderText="id" ItemStyle-Width="15px"></asp:BoundField>
								            <asp:TemplateField HeaderText="Longitud" ItemStyle-Width="50px">
									            <ItemTemplate>
										            <cc1:DNumber id="txtLongitud" runat="server" Width="50px" MaxLength="5" IsRequiered="true" ValidationGroup="Guardar" TooltipMessage=" "></cc1:DNumber>
										            <asp:Label id="Label1" runat="server">cms.</asp:Label>
									            </ItemTemplate>
								            </asp:TemplateField>
							            </Columns>
                                    </cc1:CoreGridView>
                                </td>
                                
                                <td align="center" valign="top">
                                    <cc1:CoreGridView id="dtgProfundidades" runat="server" Width="80px" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="" ItemStyle-Width="15">
                                                    <ItemTemplate>
                                                        <asp:ImageButton runat="server" ImageUrl="~/_images/basic/delete.png" ImageAlign="Middle" ID="imgEliminarItem" />
                                                    </ItemTemplate>
                                            </asp:TemplateField>
								            <asp:BoundField DataField="id_Plantilla_Estante_Profundidad" SortExpression="id_Plantilla_Estante_Profundidad" HeaderText="id" ItemStyle-Width="15"></asp:BoundField>
								            <asp:TemplateField HeaderText="Longitud">
									            <ItemTemplate>
										            <cc1:DNumber id="txtLongitud" runat="server" Width="50px" MaxLength="5" IsRequiered="true" ValidationGroup="Guardar" TooltipMessage=" "></cc1:DNumber>
										            <asp:Label id="lblLongitud" runat="server">cms.</asp:Label>
									            </ItemTemplate>
								            </asp:TemplateField>
							            </Columns>
                                    </cc1:CoreGridView>
                                </td>
                            </tr>
                        </table>
                </td>
            </tr>
            

            <tr>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
