<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterConfig.Master" AutoEventWireup="true"
    CodeBehind="AjusteSoportesSobrantes.aspx.cs" Inherits="WebPunteoElectronico.Site.Ajustes.AjusteSoportesSobrantes" %>
<%@ Register TagPrefix="cc1" Namespace="WebPunteoElectronico.Clases.Slyg" Assembly="WebPunteoElectronico" %>
<%@ Register TagPrefix="cc1" Namespace="WebPunteoElectronico.Controls" Assembly="WebPunteoElectronico" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentPlaceHolder" runat="server">
    <script type="text/javascript">
        var Observacion_TextBoxId = "<%=txtObservacion.ClientID %>";
        var Observacion_InputData = <%= GetObservacionesJsonData() %>;
        var ShowObservacion = "1";

        function flexigridCellFormat(name, idx, cellHtml, cell) {
           if (name == "Valor_Transaccion" || name == "Comision_Original") {
                   if (cellHtml != "") { return CurrencyFormat(cellHtml); } else { return "0.00"; }
               } else {
                   return cellHtml;
               }
       }
    </script>
</asp:Content>
<asp:Content ID="FiltroContent" runat="server" ContentPlaceHolderID="TabContentPlaceHolderFiltro">
    <table border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" class="page-table">
                    <tr>
                        <td class="cs50 label">
                            Regional:
                        </td>
                        <td class="cs10">
                            &nbsp;
                        </td>
                        <td>
                            <asp:DropDownList ID="RegionalFindDropDownList" runat="server" Width="350px" AutoPostBack="True"
                                CssClass="textbox">
                            </asp:DropDownList>
                        </td>
                        <td colspan="4">
                        </td>
                        <td rowspan="3" align="right" style="width: 120px">
                            <asp:LinkButton ID="ConsultarLinkButton" runat="server" CssClass="button" Height="70px"><img src="../../Images/basic/find.png" alt="" /><br />Consultar</asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td class="cs50 label">
                            COB:
                        </td>
                        <td class="cs10">
                            &nbsp;
                        </td>
                        <td>
                            <asp:DropDownList ID="COBFindDropDownList" runat="server" Width="350px" AutoPostBack="True"
                                CssClass="textbox">
                            </asp:DropDownList>
                        </td>
                        <td colspan="4">
                        </td>
                    </tr>
                    <tr>
                        <td class="cs50 label">
                            Oficina:
                        </td>
                        <td class="cs10">
                            &nbsp;
                        </td>
                        <td>
                            <asp:DropDownList ID="OficinaFindDropDownList" runat="server" Width="350px" CssClass="textbox">
                            </asp:DropDownList>
                        </td>
                        <td class="cs20">
                            &nbsp;
                        </td>
                        <td class="label cs150">
                            Rango Fecha de Proceso P&C:
                        </td>
                        <td class="cs10">
                            &nbsp;
                        </td>
                        <td class="cs200">
                            <cc1:DFecha ID="FechaInicialTextBox" runat="server" DateFormat="yyyy/MM/dd" MaskFormat="YearMonthDay"
                                CssClass="textbox" />
                            <cc1:DFecha ID="FechaFinalTextBox" runat="server" MaskFormat="YearMonthDay" DateFormat="yyyy/MM/dd"
                                CssClass="textbox" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="rs10">
            </td>
        </tr>
    </table>
    <div style="bottom: 5px; top: 140px; position: absolute; right: 5px; left: 5px;">
        <cc1:SlygFlexigrid ID="MainGrid" runat="server" Title="Lista" UrlData="../../Controls/ProxyData.aspx"
            Width="auto" Height="210" OnRowDblClick="RowDblClick" />
        <table>
            <tr>
                <td>
                    <asp:LinkButton ID="EditarSeleccionadosButton" runat="server" OnClick="EditarSeleccionadosButton_Click"
                        CssClass="button" Text="Editar seleccionados" />
                </td>
                <td style="width: 700px">
                </td>
                <td>
                    <asp:ImageButton ID="ExportWordButton" runat="server" ImageUrl="~/Images/basic/Word-32.gif"
                        ToolTip="Exportar A Word" />
                </td>
                <td>
                    <asp:ImageButton ID="ExportExcelButton0" runat="server" ImageUrl="~/Images/basic/Excel-32.gif"
                        Style="text-align: right" ToolTip="Exportar A Excel" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="DetalleContent" runat="server" ContentPlaceHolderID="TabContentPlaceHolderDetalle">
    <table id="Table1" runat="server">
        <tr id="tr1" runat="server">
            <td id="tdControlAjustar" runat="server">
                <asp:LinkButton ID="lnkAjustar" OnClick="lnkAjustar_Click" runat="server" OnClientClick="return confirm('¿Está seguro que desea guardar el ajuste realizado?')"
                    CssClass="button"><img src="../../Images/basic/Ajustar.png" alt="" /><br />Guardar ajuste</asp:LinkButton>
            </td>
            <td>
            </td>
            <td id="tdControlAprobar" runat="server">
                <asp:LinkButton ID="lnkAprobar" OnClick="lnkAprobar_Click" runat="server" CssClass="button"><img src="../../Images/basic/Aprobar.png" alt=""/><br />Aprobar ajuste</asp:LinkButton>
            </td>
            <td>
            </td>
            <td id="tdControlRechazar" runat="server">
                <asp:LinkButton ID="lnkRechazar" OnClick="lnkRechazar_Click" runat="server" CssClass="button"><img src="../../Images/basic/Rechazar.png" alt=""/><br />Rechazar ajuste</asp:LinkButton>
            </td>
            <td>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td align="right">
                <asp:LinkButton ID="VerImagenButton" runat="server" CssClass="button">Ver imagen
                </asp:LinkButton>
                 <asp:LinkButton ID="VerAnexoButton" runat="server" CssClass="button">Ver anexo
                </asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td colspan="3" style="color: #003300; font-weight: bold">
                Datos del Soporte Sobrante:
            </td>
        </tr>
        <tr>
            <td>
                Fecha Movimiento:
            </td>
            <td>
            </td>
            <td>
                <asp:Label ID="lblDataFechaP" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Oficina:
            </td>
            <td>
            </td>
            <td>
                <asp:Label ID="lblDataOficina" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Transacción:
            </td>
            <td>
            </td>
            <td>
                <asp:Label ID="lblDataTx" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Valor:
            </td>
            <td>
            </td>
            <td>
                <asp:Label ID="lblDataValor" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Label ID="Label9" runat="server" Text="Ajuste:" Font-Bold="True" ForeColor="#003300"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Observación:
            </td>
            <td>
            </td>
            <td>
                <asp:TextBox ID="txtObservacion" runat="server" Style="width: 520px" />
            </td>
        </tr>
        <tr>
            <td colspan="3" align="right">
                <table id="tblEdicionAjuste" runat="server">
                    <tr>
                        <td>
                            <input id="btnNuevo" runat="server" class="button" type="button" value="Nueva" onclick="Nueva_Click(this);" />
                        </td>
                        <td>
                            <input id="btnEditar" runat="server" class="button" type="button" value="Editar"
                                onclick="Editar_Click(this);" />
                        </td>
                        <td>
                            <input id="btnInactivar" runat="server" class="button" type="button" value="Inactivar"
                                onclick="Inactivar_Click(this);" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <span>
                    <asp:LinkButton ID="AdjuntarButton" runat="server" CssClass="button" Text="">
                    Adjuntar</asp:LinkButton>Tamaño máximo 2MB</span>
            </td>
            <td colspan="2">
                <div style="border: 1px solid #CCCCCC; width: 200px; height: 80px; overflow: auto;">
                    <asp:DataGrid ID="AdjuntosDataGrid" runat="server" AutoGenerateColumns="False" BackColor="LightGoldenrodYellow"
                        BorderColor="Tan" BorderWidth="1px" CellPadding="2" Width="192px" ForeColor="Black"
                        GridLines="None" ShowHeader="False">
                        <AlternatingItemStyle Font-Size="X-Small" BackColor="PaleGoldenrod" />
                        <Columns>
                            <asp:ButtonColumn DataTextField="Nombre_Archivo" HeaderText="Nombre Archivo" Text="Nombre Archivo"
                                CommandName="Select"></asp:ButtonColumn>
                            <asp:BoundColumn DataField="fk_Ajuste_Adjunto" HeaderText="fk_Ajuste_Adjunto" Visible="False">
                            </asp:BoundColumn>
                        </Columns>
                        <EditItemStyle Font-Size="X-Small" />
                        <FooterStyle BackColor="Tan" Font-Size="X-Small" />
                        <HeaderStyle BackColor="Tan" Font-Bold="True" Font-Size="X-Small" />
                        <ItemStyle Font-Size="X-Small" />
                        <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                        <SelectedItemStyle BackColor="DarkSlateBlue" Font-Size="X-Small" ForeColor="GhostWhite" />
                    </asp:DataGrid>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                ¿Ajusta?:
            </td>
            <td>
            </td>
            <td>
                <asp:RadioButtonList ID="rbtAjuste" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="1" Text="Si"></asp:ListItem>
                    <asp:ListItem Value="0" Text="No"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
    </table>
    <div id="VentanaObservacion_Div" style="border-style: solid; border-width: 1px; border-color: #808080 #000000 #000000 #808080;
        display: none; background-color: #CCCCCC; width: 650px; height: 120px; position: absolute !important;
        left: 10px; top: 160px; text-align: left;">
        <table width="100%">
            <tr style="background-color: #C0C0C0">
                <td colspan="2">
                    Observacion
                </td>
            </tr>
            <tr>
                <td>
                    Codigo
                </td>
                <td>
                    <input id="VentanaObservacionCodigo" readonly="readonly" type="text" style="width: 100px" />
                </td>
            </tr>
            <tr>
                <td>
                    Descripcion
                </td>
                <td>
                    <input type="text" id="VentanaObservacionDescripcion" style="width: 550px;" />
                </td>
            </tr>
            <tr>
                <td>
                    <input class="button" type="button" value="Aceptar" onclick="AceptarObservacion_Click();" />
                </td>
                <td align="right">
                    <input class="button" type="button" id="CancelarObservacion" value="Cancelar" onclick="CancelarObservacion_Click();" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="FinalScriptContent" runat="server" ContentPlaceHolderID="FinalScriptContentPlaceHolder">
    <script src="AjustesScript.js" type="text/javascript"></script>
</asp:Content>
