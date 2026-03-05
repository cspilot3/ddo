<%@ Page Title="" Language="C#" MasterPageFile="~/master/master_form.Master" AutoEventWireup="true"
    CodeBehind="dashboard.aspx.cs" Inherits="WebSantander.site.application.dashboard" %>

<%@ Register Src="../../controls/DashBoardItem.ascx" TagName="DashBoardItem" TagPrefix="slyg" %>
<%@ Register Src="../../controls/DashBoardItemLocal.ascx" TagName="DashBoardItemLocal"
    TagPrefix="slyg" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="dashboard.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <fieldset class="DashBoardGroup" style="text-align: left">
        <legend>Dashboard </legend>
        <table id="Dashboard-main">
            <tr>
                <td>
                    <slyg:DashBoardItem ID="ConsultaDashBoardItem" runat="server" Title="Consulta" Tooltip="Modulo de Consulta"
                        ImageUrl="~/Images/dashboard/consultas.png" />
                </td>
                <td>
                    <slyg:DashBoardItemLocal ID="ReportesDashBoardItemLocal" runat="server" Title="Reportes"
                        Tooltip="Reportes" ImageUrl="~/Images/dashboard/informes.png" OnClientClick="Frm.ShowDashBoardMenu('#Dashboard-main', '#Dashboard-reportes')" />
                </td>
                <td class="DashBoard-separator">
                </td>
                <td>
                    <slyg:DashBoardItem ID="AcercaDeDashBoardItem" runat="server" Title="Acerca de" Tooltip="Acerca de MIHAU Client Browser..."
                        ImageUrl="~/Images/dashboard/acercade.png" />
                </td>
            </tr>
        </table>
        <table id="Dashboard-reportes" style="display:none">
            <tr>
                <td>
                    <slyg:DashBoardItemLocal ID="ValidacionListasDashBoardItemLocal" runat="server" Title="Validacion de Listas"
                        Tooltip="Reportes" ImageUrl="~/Images/dashboard/informes.png" OnClientClick="Frm.ShowDashBoardMenu('#Dashboard-reportes', '#Dashboard-validacionlistas')" />
                </td>
                <td>
                    <slyg:DashBoardItemLocal ID="EmbargosDashBoardItemLocal" runat="server" Title="Embargos"
                        Tooltip="Reportes" ImageUrl="~/Images/dashboard/informes.png" OnClientClick="Frm.ShowDashBoardMenu('#Dashboard-reportes', '#Dashboard-embargos')" />
                </td>
                <td>
                    <slyg:DashBoardItemLocal ID="DesembargosDashBoardItemLocal" runat="server" Title="Desembargos"
                        Tooltip="Reportes" ImageUrl="~/Images/dashboard/informes.png" OnClientClick="Frm.ShowDashBoardMenu('#Dashboard-reportes', '#Dashboard-desembargos')" />
                </td>
                <td class="DashBoard-separator">
                </td>
                <td>
                    <slyg:DashBoardItemLocal ID="RegresarReporteMainDashBoardItemLocal" runat="server" Title="Regresar"
                        ImageUrl="~/Images/dashboard/back.png" OnClientClick="Frm.ShowDashBoardMenu('#Dashboard-reportes', '#Dashboard-main')" />
                </td>
            </tr>
        </table>
        <table id="Dashboard-validacionlistas" style="display:none">
            <tr>
                <td>
                    <slyg:DashBoardItem ID="ReporteValidacionListasDashBoardItem" runat="server" Title="Reporte Validacion de Listas" ImageUrl="~/Images/dashboard/informes.png" />
                </td>
                <td>
                    <slyg:DashBoardItem ID="ReporteFacturacionValidacionListasDashBoardItem" runat="server" Title="Facturación" ImageUrl="~/Images/dashboard/informes.png" />
                </td>
                <td>
                    <slyg:DashBoardItem ID="ReporteFacturacionDetalladaValidacionListasDashBoardItem" runat="server" Title="Facturación Detallada" ImageUrl="~/Images/dashboard/informes.png" />
                </td>
                <td class="DashBoard-separator">
                </td>
                <td>
                    <slyg:DashBoardItemLocal ID="RegresarValidacionListasReporteDashBoardItemLocal" runat="server" Title="Regresar"
                        ImageUrl="~/Images/dashboard/back.png" OnClientClick="Frm.ShowDashBoardMenu('#Dashboard-validacionlistas', '#Dashboard-reportes')" />
                </td>
            </tr>
            <tr>
                <td>
                    <slyg:DashBoardItem ID="ReporteCargueValidacionListasDashBoardItem" runat="server" Title="Cargues" ImageUrl="~/Images/dashboard/informes.png" />
                </td>
            </tr>
        </table>
        <table id="Dashboard-embargos" style="display:none">
            <tr>
                <td>
                    <slyg:DashBoardItem ID="ReporteEmbargosDashBoardItem" runat="server" Title="Reporte Embargos" ImageUrl="~/Images/dashboard/informes.png" />
                </td>
                <td>
                    <slyg:DashBoardItem ID="ReporteFacturacionEmbargosDashBoardItem" runat="server" Title="Facturación" ImageUrl="~/Images/dashboard/informes.png" />
                </td>
                <td>
                    <slyg:DashBoardItem ID="ReporteFacturacionDetalladaEmbargosDashBoardItem" runat="server" Title="Facturación Detallada" ImageUrl="~/Images/dashboard/informes.png" />
                </td>
                <td class="DashBoard-separator">
                </td>
                <td>
                    <slyg:DashBoardItemLocal ID="RegresarEmbargosReporteDashBoardItemLocal" runat="server" Title="Regresar"
                        ImageUrl="~/Images/dashboard/back.png" OnClientClick="Frm.ShowDashBoardMenu('#Dashboard-embargos', '#Dashboard-reportes')" />
                </td>
            </tr>
            <tr>
                <td>
                    <slyg:DashBoardItem ID="ReporteCruceEmbargosDashBoardItem" runat="server" Title="Cruce" ImageUrl="~/Images/dashboard/informes.png" />
                </td>
                <td>
                    <slyg:DashBoardItem ID="ReporteCargueEmbargosDashBoardItem" runat="server" Title="Cargues" ImageUrl="~/Images/dashboard/informes.png" />
                </td>
            </tr>
        </table>
        <table id="Dashboard-desembargos" style="display:none">
            <tr>
                <td>
                    <slyg:DashBoardItem ID="ReporteDesembargosDashBoardItem" runat="server" Title="Reporte Desembargos" ImageUrl="~/Images/dashboard/informes.png" />
                </td>
                <td>
                    <slyg:DashBoardItem ID="ReporteFacturacionDesembargosDashBoardItem" runat="server" Title="Facturación" ImageUrl="~/Images/dashboard/informes.png" />
                </td>
                <td>
                    <slyg:DashBoardItem ID="ReporteFacturacionDetalladaDesembargosDashBoardItem" runat="server" Title="Facturación Detallada" ImageUrl="~/Images/dashboard/informes.png" />
                </td>
                <td class="DashBoard-separator">
                </td>
                <td>
                    <slyg:DashBoardItemLocal ID="RegresarDesembargosReporteDashBoardItemLocal" runat="server" Title="Regresar"
                        ImageUrl="~/Images/dashboard/back.png" OnClientClick="Frm.ShowDashBoardMenu('#Dashboard-desembargos', '#Dashboard-reportes')" />
                </td>
            </tr>
            <tr>
                <td>
                    <slyg:DashBoardItem ID="ReporteCruceDesembargosDashBoardItem" runat="server" Title="Cruce" ImageUrl="~/Images/dashboard/informes.png" />
                </td>
                <td>
                    <slyg:DashBoardItem ID="ReporteCargueDesembargosDashBoardItem" runat="server" Title="Cargues" ImageUrl="~/Images/dashboard/informes.png" />
                </td>
            </tr>
        </table>
        <%--<table id="Dashboard-reportes" style="display: none">
            <tr>
                <td>
                    <slyg:DashBoardItem ID="ReporteEmbargosDesembargosDashBoardItem" runat="server" Title="Embargos y Desembargos" ImageUrl="~/Images/dashboard/informes.png" />
                </td>
                <td>
                    <slyg:DashBoardItem ID="ReporteValidacionListasDashBoardItem" runat="server" Title="Validación de Listas" ImageUrl="~/Images/dashboard/informes.png" />
                </td>
                <td>
                    <slyg:DashBoardItem ID="ReporteFacturacionDashBoardItem" runat="server" Title="Facturación" ImageUrl="~/Images/dashboard/informes.png" />
                </td>
                <td class="DashBoard-separator">
                </td>
                <td>
                    <slyg:DashBoardItemLocal ID="ReportesBackDashBoardItemLocal" runat="server" Title="Regresar"
                        ImageUrl="~/Images/dashboard/back.png" OnClientClick="Frm.ShowDashBoardMenu('#Dashboard-reportes1', '#Dashboard-reportes')" />
                </td>
            </tr>
            <tr>
                <td>
                    <slyg:DashBoardItem ID="ReporteFacturacionDetalladoDashBoardItem" runat="server" Title="Facturación Detallado" ImageUrl="~/Images/dashboard/informes.png" />
                </td>
                <td>
                    <slyg:DashBoardItem ID="ReporteCruceDashBoardItem" runat="server" Title="Cruce Embargos y Desembargos" ImageUrl="~/Images/dashboard/informes.png" />
                </td>
                <td>
                    <slyg:DashBoardItem ID="ReporteCargueDashboardItem" runat="server" Title="Cargues" ImageUrl="~/Images/dashboard/informes.png" />
                </td>
            </tr>
        </table>--%>
    </fieldset>
    <script type="text/javascript">
        Frm.DisplayTableMenu();            
    </script>
</asp:Content>
