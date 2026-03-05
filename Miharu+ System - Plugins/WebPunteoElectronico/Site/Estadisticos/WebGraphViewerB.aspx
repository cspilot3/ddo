<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterForm.Master" AutoEventWireup="true"
    CodeBehind="WebGraphViewerB.aspx.cs" Inherits="WebPunteoElectronico.Site.Estadisticos.WebGraphViewerB" %>
<%@ Register TagPrefix="cc1" Namespace="WebPunteoElectronico.Controls" Assembly="WebPunteoElectronico" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContentPlaceHolder" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" style="margin-right: auto; margin-left: auto;">
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
                        <td class="cs20">
                            &nbsp;
                        </td>
                        <td class="label" style="width: 200px">
                        </td>
                        <td class="cs10">
                            &nbsp;
                        </td>
                        <td>
                        </td>
                        <td rowspan="3" align="right" style="width: 180px">
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
                        <td class="cs20">
                            &nbsp;
                        </td>
                        <td class="label">
                        </td>
                        <td class="cs10">
                            &nbsp;
                        </td>
                        <td align="left">
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
                        <td class="label">
                            <asp:Label ID="FechaMovimientoLabel" runat="server" CssClass="label" Text="Rango fecha movimiento"></asp:Label>
                        </td>
                        <td class="cs10">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 200px">
                            <cc1:DFecha ID="FechaMovimientoInicialTextBox" runat="server" DateFormat="yyyy/MM/dd"
                                MaskFormat="YearMonthDay" CssClass="textbox" />
                            <cc1:DFecha ID="FechaMovimientoFinalTextBox" runat="server" MaskFormat="YearMonthDay"
                                DateFormat="yyyy/MM/dd" CssClass="textbox" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="rs10">
            </td>
        </tr>
        <tr>
            <td align="center" style="border: 1px solid #808080">
                <asp:Panel ID="InicioPanel" runat="server">
                    <img src="../../Images/estadisticos/Estadisticos.png" alt="" />
                    <br />
                    <span>Seleccione los parámetros de consulta</span>
                </asp:Panel>
                <asp:Panel ID="NoDataPanel" runat="server">
                    <img src="../../Images/estadisticos/NoData.png" alt="" />
                    <br />
                    <span>No se encontraron datos para visualizar</span>
                </asp:Panel>
                <asp:Panel ID="DataPanel" runat="server">
                    <table border="0" cellpadding="2" cellspacing="0" class="page-table">
                        <tr>
                            <td class="cs50">
                                <asp:ImageButton ID="Area2DImageButton" runat="server" CssClass="changing-button"
                                    ImageUrl="~/Images/estadisticos/Area2D.png" ToolTip="Area 2D" />
                            </td>
                            <td rowspan="9" align="Center">
                                <asp:Literal ID="ChartLiteral" runat="server"></asp:Literal>
                            </td>
                            <td class="cs50">
                                <asp:ImageButton ID="DowloadZipImageButton" runat="server" CssClass="changing-button"
                                    ImageUrl="~/Images/estadisticos/DownloadZip.png" ToolTip="Descargar data en archivo Zip" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:ImageButton ID="Bar2DImageButton" runat="server" CssClass="changing-button"
                                    ImageUrl="~/Images/estadisticos/Bar2D.png" ToolTip="Barras 2D" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:ImageButton ID="Column2DImageButton" runat="server" CssClass="changing-button"
                                    ImageUrl="~/Images/estadisticos/Column2D.png" ToolTip="Columnas 2D" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:ImageButton ID="Column3DImageButton" runat="server" CssClass="changing-button"
                                    ImageUrl="~/Images/estadisticos/Column3D.png" ToolTip="Columnas 3D" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:ImageButton ID="Doughnut2DImageButton" runat="server" CssClass="changing-button"
                                    ImageUrl="~/Images/estadisticos/Doughnut2D.png" ToolTip="Anillo 2D" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:ImageButton ID="FunnelImageButton" runat="server" CssClass="changing-button"
                                    ImageUrl="~/Images/estadisticos/Funnel.png" ToolTip="Embudo" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:ImageButton ID="LineImageButton" runat="server" CssClass="changing-button" ImageUrl="~/Images/estadisticos/Line.png"
                                    ToolTip="Lineas" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:ImageButton ID="Pie2DImageButton" runat="server" CssClass="changing-button"
                                    ImageUrl="~/Images/estadisticos/Pie2d.png" ToolTip="Torta 2D" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:ImageButton ID="Pie3DImageButton" runat="server" CssClass="changing-button"
                                    ImageUrl="~/Images/estadisticos/Pie3d.png" ToolTip="Torta 3D" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
