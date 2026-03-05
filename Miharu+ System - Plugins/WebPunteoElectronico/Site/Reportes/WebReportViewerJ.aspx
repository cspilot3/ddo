<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterForm.Master" AutoEventWireup="true"
    CodeBehind="WebReportViewerJ.aspx.cs" Inherits="WebPunteoElectronico.Site.Reportes.WebReportViewerJ" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register TagPrefix="cc1" Namespace="WebPunteoElectronico.Controls" Assembly="WebPunteoElectronico" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentPlaceHolder" runat="server">
<script type="text/javascript">
    function Navegar(nUrl) {
        ShowProcess();
        window.location.href(nUrl);
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContentPlaceHolder" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
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
                        <td style="text-align:left;">
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
                            <asp:Label ID="FechaProcesoLabel" runat="server" CssClass="label" Text="Fecha:"></asp:Label>
                        </td>
                        <td class="cs10">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 200px">
                            <cc1:DFecha ID="FechaProcesoTextBox" runat="server" DateFormat="yyyy/MM/dd" MaskFormat="YearMonthDay"
                                CssClass="textbox" />
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
                            <asp:CheckBox ID="DetalladoCheck" runat="server" text="Ver Detallado"/></td>
                        <td class="cs10">
                            &nbsp;
                        </td>
                        <td align="left">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div style="position: absolute; top: 140px; right: 10px; left: 10px; bottom: 5px">
        <rsweb:ReportViewer ID="PageReportViewer" runat="server" Width="100%" Height="100%"
            HyperlinkTarget="" BorderWidth="1" BorderStyle="Solid" BorderColor="#999999">
            <LocalReport EnableHyperlinks="True">
            </LocalReport>
        </rsweb:ReportViewer>
    </div>
</asp:Content>
