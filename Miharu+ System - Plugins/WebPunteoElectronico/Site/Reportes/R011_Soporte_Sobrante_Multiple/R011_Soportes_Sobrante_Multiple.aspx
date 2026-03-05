<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterConfig.Master" AutoEventWireup="true"
    CodeBehind="R011_Soportes_Sobrante_Multiple.aspx.cs" Inherits="WebPunteoElectronico.Site.Reportes.R011_Soporte_Sobrante_Multiple.R011_Soportes_Sobrante_Multiple" %>
<%@ Register TagPrefix="cc1" Namespace="WebPunteoElectronico.Clases.Slyg" Assembly="WebPunteoElectronico" %>
<%@ Register TagPrefix="cc1" Namespace="WebPunteoElectronico.Controls" Assembly="WebPunteoElectronico" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentPlaceHolder" runat="server">
    <script type="text/javascript">        
       var Esquemas_InputData = ""; 
       var Tipologia_InputData = "";
       var UrlVisorImg = "";
      <%=GetTipologiasJsonData()%>
    </script>
   <script type="text/javascript">
       function flexigridCellFormat(name, idx, cellHtml, cell) {
           if (name == "Url" || name == "UrlAnexo") {
               if (cellHtml != "") { return "<a href='" + UrlVisorImg + "?token=" + cellHtml + "'  target='_blank' style='color:Blue' >Imagen..</a>"; } else { return ""; }
           } else {
               if (name == "Valor" || name == "Valor_Original" || name == "Comision" || name == "Comision_Original") {
                   if (cellHtml != "") { return CurrencyFormat(cellHtml); } else { return "0.00"; }
               } else {
                   return cellHtml;
               }
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
                        <td class="cs100 label">
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
                        <td class="cs20">
                            &nbsp;
                        </td>
                        <td class="label" style="width: 200px">
                            Modo:
                        </td>
                        <td class="cs10">
                            &nbsp;
                        </td>
                        <td>
                            <asp:DropDownList ID="ModoFindDropDownList" runat="server" AutoPostBack="True">
                                <asp:ListItem Text="Fecha Movimiento Banco" Value="1" />
                                <asp:ListItem Text="Fecha Proceso P&amp;C" Value="2" />
                                <asp:ListItem Text="Fecha Movimiento Banco y Proceso P&amp;C" Value="3" Selected="True" />
                            </asp:DropDownList>
                        </td>
                        <td rowspan="3" align="right" style="width: 120px">
                            <asp:LinkButton ID="ConsultarLinkButton" runat="server" CssClass="button" Height="70px"><img src="../../../Images/basic/find.png" alt="" /><br />Consultar</asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td class="cs100 label">
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
                        <td class="cs20">
                            &nbsp;
                        </td>
                        <td class="label" style="width: 200px">
                            <asp:Label ID="FechaMovimientoLabel" runat="server" CssClass="label" Text="Fecha movimiento:"></asp:Label>
                        </td>
                        <td class="cs10">
                            &nbsp;
                        </td>
                        <td align="left">
                            <cc1:DFecha ID="FechaMovimientoInicialTextBox" runat="server" DateFormat="yyyy/MM/dd"
                                CssClass="textbox" />
                            <cc1:DFecha ID="FechaMovimientoFinalTextBox" runat="server" DateFormat="yyyy/MM/dd"
                                CssClass="textbox" />
                        </td>
                    </tr>
                    <tr>
                        <td class="cs100 label">
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
                            <asp:Label ID="FechaProcesoLabel" runat="server" CssClass="label" Text="Fecha proceso P&amp;C"></asp:Label>
                        </td>
                        <td class="cs10">
                            &nbsp;
                        </td>
                        <td class="cs200">
                            <cc1:DFecha ID="FechaProcesoInicialTextBox" runat="server" DateFormat="yyyy/MM/dd"
                                MaskFormat="YearMonthDay" CssClass="textbox" />
                            <cc1:DFecha ID="FechaProcesoFinalTextBox" runat="server" MaskFormat="YearMonthDay"
                                DateFormat="yyyy/MM/dd" CssClass="textbox" />
                        </td>
                    </tr>
                    <tr>
                        <%--Tipología Documental, se cambió a Transacción Banco porque así aparecía en el requerimiento--%>
                        <td class="cs100 label">
                            Transacción Banco:
                        </td>
                        <td class="cs10">
                            &nbsp;
                        </td>
                        <td>
                            <asp:DropDownList ID="TipologiaDropDownList" runat="server" Width="300px" AutoPostBack="False"
                                CssClass="textbox">
                            </asp:DropDownList>
                        </td>
                        <td class="cs20">
                            &nbsp;
                        </td>
                        <td class="label" style="width: 200px">
                            Valor:
                        </td>
                        <td class="cs10">
                            &nbsp;
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtValorUno" runat="server" CssClass="textbox" Width="120px" value=""></asp:TextBox>
                            <span class="style2">-</span>
                            <asp:TextBox ID="txtValorDos" runat="server" CssClass="textbox" Width="120px" value=""></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div style="bottom: 5px; top: 160px; position: absolute; right: 5px; left: 5px;">
        <cc1:SlygFlexigrid ID="MainGrid" runat="server" Title="Lista" UrlData="../../../Controls/ProxyData.aspx"
            Width="auto" Height="200" OnRowDblClick="RowDblClick" />
        <table>
            <tr>
                <td align="left">
                    <asp:LinkButton ID="EditarSeleccionadosButton" runat="server" OnClick="EditarSeleccionadosButton_Click"
                        CssClass="button" Text="Editar seleccionados" />
                </td>
                <td style="width: 800px">
                </td>
                <td>
                    <asp:ImageButton ID="ExportWordButton" runat="server" ImageUrl="../../../Images/basic/Word-32.gif"
                        ToolTip="Exportar A Word" />
                </td>
                <td>
                    <asp:ImageButton ID="ExportExcelButton0" runat="server" ImageUrl="../../../Images/basic/Excel-32.gif"
                        Style="text-align: right" ToolTip="Exportar A Excel" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="DetalleContent" runat="server" ContentPlaceHolderID="TabContentPlaceHolderDetalle">
    <table id="Table1" runat="server">
        <tr id="tr1" runat="server">
            <td id="tdControlGuardar" runat="server">
                <div id="btn_Guardar" style="width: 100px; height: 100px;" class="button" onclick="return SaveTipologia('¿Está seguro que desea guardar todos los registros seleccionados con esta tipología?')">
                    <img src="../../../Images/basic/Ajustar.png" alt="" /><br />
                    Guardar Cambios</div>
            </td>
            <td>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" Text="Esquema:" Font-Bold="True" ForeColor="#003300"></asp:Label>
            </td>
            <td>
                <input type="text" id="txtEsquemas" style="width: 520px" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="Tipología:" Font-Bold="True" ForeColor="#003300"></asp:Label>
            </td>
            <td>
                <input type="text" id="txtTipologias" style="width: 520px" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="FinalScriptContent" runat="server" ContentPlaceHolderID="FinalScriptContentPlaceHolder">
    <script src="R011_Soportes_Sobrante_Multiple.js" type="text/javascript"></script>
</asp:Content>
