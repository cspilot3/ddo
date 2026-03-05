<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GraphManager.ascx.cs"
    Inherits="WebPunteoElectronico.Controls.GraphManager" %>
<div style="position: relative; top: 0px; left: 0px; height: 300px;">
    <div id="OptionDiv_<%=idWebGraph %>" style="padding: 5px; border: 1px solid #808080;
        display: none; position: absolute; top: 0px; left: 0px; background-color: #666666;">
        <table border="0" cellpadding="0" cellspacing="0" width="505px">
            <tr>
                <td colspan="11" align="right">
                    <div onclick="HideOptions('<%=idWebGraph %>');" style="cursor: pointer">
                        <asp:Image ID="CloseImage" runat="server" ImageUrl="~/Images/basic/cross.png" ToolTip="Ocultar opciones de configuración" />
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="11">
                    <asp:DropDownList ID="ReportsDropDownList" runat="server" Width="100%" CssClass="textbox"
                        AutoPostBack="true">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="11" class="rs5">
                </td>
            </tr>
            <tr>
                <td>
                    <asp:ImageButton ID="Area2DImageButton" runat="server" CssClass="changing-button"
                        ImageUrl="~/Images/estadisticos/Area2D_32.png" ToolTip="Area 2D" />
                </td>
                <td>
                    <asp:ImageButton ID="Bar2DImageButton" runat="server" CssClass="changing-button"
                        ImageUrl="~/Images/estadisticos/Bar2D_32.png" ToolTip="Barras 2D" />
                </td>
                <td>
                    <asp:ImageButton ID="Column2DImageButton" runat="server" CssClass="changing-button"
                        ImageUrl="~/Images/estadisticos/Column2D_32.png" ToolTip="Columnas 2D" />
                </td>
                <td>
                    <asp:ImageButton ID="Column3DImageButton" runat="server" CssClass="changing-button"
                        ImageUrl="~/Images/estadisticos/Column3D_32.png" ToolTip="Columnas 3D" />
                </td>
                <td>
                    <asp:ImageButton ID="Doughnut2DImageButton" runat="server" CssClass="changing-button"
                        ImageUrl="~/Images/estadisticos/Doughnut2D_32.png" ToolTip="Anillo 2D" />
                </td>
                <td>
                    <asp:ImageButton ID="FunnelImageButton" runat="server" CssClass="changing-button"
                        ImageUrl="~/Images/estadisticos/Funnel_32.png" ToolTip="Embudo" />
                </td>
                <td>
                    <asp:ImageButton ID="LineImageButton" runat="server" CssClass="changing-button" ImageUrl="~/Images/estadisticos/Line_32.png"
                        ToolTip="Lineas" />
                </td>
                <td>
                    <asp:ImageButton ID="Pie2DImageButton" runat="server" CssClass="changing-button"
                        ImageUrl="~/Images/estadisticos/Pie2d_32.png" ToolTip="Torta 2D" />
                </td>
                <td>
                    <asp:ImageButton ID="Pie3DImageButton" runat="server" CssClass="changing-button"
                        ImageUrl="~/Images/estadisticos/Pie3d_32.png" ToolTip="Torta 3D" />
                </td>
                <td class="cs50">
                </td>
                <td>
                    <asp:ImageButton ID="DowloadZipImageButton" runat="server" CssClass="changing-button"
                        ImageUrl="~/Images/estadisticos/DownloadZip_32.png" ToolTip="Descargar data en archivo Zip" />
                </td>
            </tr>
        </table>
    </div>
    <div id="ShowDiv_<%=idWebGraph %>" onclick="ShowOptions('<%=idWebGraph %>');" style="cursor: pointer;
        position: absolute; top: 0px; right: 0px;">
        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/basic/cog_edit.png" ToolTip="Configurar" />
    </div>
    <table border="0" cellpadding="0" cellspacing="0" width="505px">
        <tr>
            <td>
                <asp:Panel ID="InicioPanel" runat="server">
                    <img src="../../Images/estadisticos/Estadisticos_128.png" alt="" />
                    <br />
                    <span>Seleccione los parámetros de consulta</span>
                </asp:Panel>
                <asp:Panel ID="NoDataPanel" runat="server" Visible="false">
                    <img src="../../Images/estadisticos/NoData_128.png" alt="" />
                    <br />
                    <span>No se encontraron datos para visualizar</span>
                </asp:Panel>
                <asp:Panel ID="NoDiponiblePanel" runat="server" Visible="false">
                    <img src="../../Images/estadisticos/NoDisponible_128.png" alt="" />
                    <br />
                    <span>Los parámetros seleccionados no aplican para este reporte</span>
                </asp:Panel>
                <asp:Panel ID="DataPanel" runat="server" Visible="false">
                    <asp:Literal ID="ChartLiteral" runat="server"></asp:Literal>
                </asp:Panel>
                <asp:Panel ID="ErrorPanel" runat="server" Visible="false">
                    <img src="../../Images/estadisticos/Error_128.png" alt="" />
                    <br />
                    <asp:Label ID="ErrorLabel" runat="server" CssClass="error_label" Text=""></asp:Label>
                </asp:Panel>
            </td>
        </tr>
    </table>
</div>
