<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterForm.Master" AutoEventWireup="true"
    CodeBehind="WebReportViewerI.aspx.cs" Inherits="WebPunteoElectronico.Site.Reportes.WebReportViewerI" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register TagPrefix="cc1" Namespace="WebPunteoElectronico.Controls" Assembly="WebPunteoElectronico" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContentPlaceHolder" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0"  class="page-table" style="text-align:left;">
                    <tr>
                        <td class="cs50 label">
                            Regional:
                        </td>
                        <td class="cs10">
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
                                <asp:ListItem Text="Fecha Movimiento Banco" Value="1"  />
                                <asp:ListItem Text="Fecha Proceso P&amp;C" Value="2" />
                                <asp:ListItem Text="Fecha Movimiento Banco y Proceso P&amp;C" Value="3" Selected="True" />
                            </asp:DropDownList>
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
                    <tr >
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
                        <td class="label" style="width: 200px; text-align:left;">
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
                    <tr >
                        <td class="cs50 label">
                            Estado:
                        </td>
                        <td class="cs10">
                            &nbsp;
                        </td>
                        <td>
                            <asp:DropDownList ID="EstadoDropDownList" runat="server" Width="350px" CssClass="textbox">
                            </asp:DropDownList>
                        </td>
                        <td class="cs20">
                            &nbsp;
                        </td>
                        <td class="label" style="width: 200px; text-align:left;">
                        </td>
                        <td class="cs10">
                        </td>
                        <td align="left">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div style="position: absolute; z-index: 0; top: 160px; right: 10px; left: 10px;
        bottom: 5px">
        <rsweb:ReportViewer ID="PageReportViewer" runat="server" Width="100%" Height="100%"
            HyperlinkTarget="" BorderWidth="1" BorderStyle="Solid" BorderColor="#999999">
            <LocalReport EnableHyperlinks="True">
            </LocalReport>
        </rsweb:ReportViewer>
    </div>
</asp:Content>
