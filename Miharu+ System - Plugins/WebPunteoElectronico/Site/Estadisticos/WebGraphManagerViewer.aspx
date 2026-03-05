<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterForm.Master" AutoEventWireup="true"
    CodeBehind="WebGraphManagerViewer.aspx.cs" Inherits="WebPunteoElectronico.Site.Estadisticos.WebGraphManagerViewer" %>
<%@ Register Src="../../Controls/GraphManager.ascx" TagName="GraphManager" TagPrefix="uc1" %>
<%@ Register TagPrefix="cc1" Namespace="WebPunteoElectronico.Controls" Assembly="WebPunteoElectronico" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentPlaceHolder" runat="server">
    <script src="WebGraphManagerViewer.js" type="text/javascript"></script>
    <script src="../../Scripts/Jquery/jquery.corner.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContentPlaceHolder" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" style="margin-right: auto; margin-left: auto;">
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" width="1000px">
                    <tr>
                        <td class="cs50 label">
                            Regional:
                        </td>
                        <td class="cs10">
                            &nbsp;
                        </td>
                        <td>
                            <asp:DropDownList ID="RegionalFindDropDownList" runat="server" Width="400px" AutoPostBack="True"
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
                        <td rowspan="3" align="right" style="width: 180px">
                            <asp:LinkButton ID="ConsultarLinkButton" runat="server" CssClass="button" Height="70px"><img src="../../Images/basic/find.png" alt="" /><br />Consultar</asp:LinkButton>
                        </td>
                        <td rowspan="3" class="cs20">
                        </td>
                        <td rowspan="3" align="right" style="width: 180px">
                            <asp:LinkButton ID="SaveLinkButton" runat="server" CssClass="button" Height="70px"
                                ToolTip="Almacenar configuración"><img src="../../Images/basic/Save.png" alt="" /><br />Guardar</asp:LinkButton>
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
                            <asp:DropDownList ID="COBFindDropDownList" runat="server" Width="400px" AutoPostBack="True"
                                CssClass="textbox">
                            </asp:DropDownList>
                        </td>
                        <td class="cs20">
                            &nbsp;
                        </td>
                        <td class="label" style="width: 200px">
                            <asp:Label ID="FechaMovimientoLabel" runat="server" CssClass="label" Text="Rango fecha movimiento"></asp:Label>
                        </td>
                        <td class="cs10">
                            &nbsp;
                        </td>
                        <td align="left">
                            <cc1:DFecha ID="FechaMovimientoInicialTextBox" runat="server" DateFormat="yyyy/MM/dd"
                                MaskFormat="YearMonthDay" CssClass="textbox" />
                            <cc1:DFecha ID="FechaMovimientoFinalTextBox" runat="server" MaskFormat="YearMonthDay"
                                DateFormat="yyyy/MM/dd" CssClass="textbox" />
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
                            <asp:DropDownList ID="OficinaFindDropDownList" runat="server" Width="400px" CssClass="textbox">
                            </asp:DropDownList>
                        </td>
                        <td class="cs20">
                            &nbsp;
                        </td>
                        <td class="label" style="width: 200px">
                            <asp:Label ID="FechaProcesoLabel" runat="server" CssClass="label" Text="Rango fecha proceso P&amp;C"></asp:Label>
                        </td>
                        <td class="cs10">
                            &nbsp;
                        </td>
                        <td align="left">
                            <cc1:DFecha ID="FechaProcesoInicialTextBox" runat="server" DateFormat="yyyy/MM/dd"
                                MaskFormat="YearMonthDay" CssClass="textbox" />
                            <cc1:DFecha ID="FechaProcesoFinalTextBox" runat="server" MaskFormat="YearMonthDay"
                                DateFormat="yyyy/MM/dd" CssClass="textbox" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="10" width="1000px">
                    <tr>
                        <td align="center" style="border: 1px solid #808080;">
                            <uc1:GraphManager ID="GraphManager1" runat="server" idWebGraph="1" />
                        </td>
                        <td align="center" style="border: 1px solid #808080;">
                            <uc1:GraphManager ID="GraphManager2" runat="server" idWebGraph="2" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="border: 1px solid #808080;">
                            <uc1:GraphManager ID="GraphManager3" runat="server" idWebGraph="3" />
                        </td>
                        <td align="center" style="border: 1px solid #808080;">
                            <uc1:GraphManager ID="GraphManager4" runat="server" idWebGraph="4" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="border: 1px solid #808080;">
                            <uc1:GraphManager ID="GraphManager5" runat="server" idWebGraph="5" />
                        </td>
                        <td align="center" style="border: 1px solid #808080;">
                            <uc1:GraphManager ID="GraphManager6" runat="server" idWebGraph="6" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
