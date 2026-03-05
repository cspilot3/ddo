<%@ Page Title="" Language="C#" MasterPageFile="~/master/master_form.Master" AutoEventWireup="true" CodeBehind="dashboard.aspx.cs" Inherits="Miharu.Client.Browser.site.application.dashboard" %>

<%@ Register Src="../../controls/DashBoardItem.ascx" TagName="DashBoardItem" TagPrefix="slyg" %>
<%@ Register Src="../../controls/DashBoardItemLocal.ascx" TagName="DashBoardItemLocal" TagPrefix="slyg" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="dashboard.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <fieldset class="DashBoardGroup" style="text-align: left">
        <legend>Dashboard </legend>
        <table id="Dashboard-main">
            <tr>
                <td>
                    <slyg:DashBoardItem ID="ConsultaDashBoardItem" runat="server" Title="Consulta" Tooltip="Modulo de Consulta" ImageUrl="~/Images/dashboard/consultas.png" />
                </td>
                <td>
                    <slyg:DashBoardItem ID="ReportesResumenMovimientoDashBoardItem" runat="server" Title="Reportes" Tooltip="Reporte de Resumen de Movimiento" ImageUrl="~/Images/dashboard/informes.png" />
                </td>
                <td>
                    <slyg:DashBoardItemLocal ID="AdministracionDashBoardItem" runat="server" Title="Administración" Tooltip="Modulo Administracion" ImageUrl="~/Images/dashboard/parametrizacion.png" OnClientClick="Frm.ShowDashBoardMenu('#Dashboard-main', '#Dashboard-administracion')" />
                </td>
                <td class="DashBoard-separator">
                </td>
                <td>
                    <slyg:DashBoardItem ID="AcercaDeDashBoardItem" runat="server" Title="Acerca de" Tooltip="Acerca de MIHAU Client Browser..." ImageUrl="~/Images/dashboard/acercade.png" />
                </td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td></td>
            </tr>
        </table>
        <%--<table id="Dashboard_reportes" style="display: none">
            <tr>
                <td>
                    <slyg:DashBoardItemLocal ID="ResumenMovimientoDashBoardItemLocal" runat="server" Title="Resumen Movimiento" ImageUrl="~/Images/dashboard/digitalizacion.png" OnClientClick="ShowDashBoardMenu('#Dashboard-reportes', '#Dashboard-reportes-digitalizacion')" />
                </td>
                <td>
                    <slyg:DashBoardItemLocal ID="Report_CruceDashBoardItemLocal" runat="server" Title="Cruce" ImageUrl="~/Images/dashboard/cruce.png" OnClientClick="ShowDashBoardMenu('#Dashboard-reportes', '#Dashboard-reportes-cruce')" />
                </td>
                <td>
                </td>
                <td>
                </td>
                <td class="DashBoard-separator">
                </td>
                <td>
                    <slyg:DashBoardItemLocal ID="ReportesBackDashBoardItemLocal" runat="server" Title="Regresar" ImageUrl="~/Images/dashboard/back.png" OnClientClick="ShowDashBoardMenu('#Dashboard-reportes', '#Dashboard-main')" />
                </td>
            </tr>
        </table>--%>
        <table id="Dashboard-administracion" style="display: none">
            <tr>
                <td>
                    <slyg:DashBoardItem ID="UsuarioDashBoardItem" runat="server" Title="Usuarios" Tooltip="Usuarios" ImageUrl="~/Images/dashboard/usuario.png" />
                </td>
                <td>
                    <slyg:DashBoardItem ID="RolesDashBoardItem" runat="server" Title="Roles" ImageUrl="~/Images/dashboard/roles.png" />
                </td>
                <td class="DashBoard-separator">
                </td>
                <td>
                    <slyg:DashBoardItemLocal ID="AdministracionBackDashBoardItemLocal" runat="server" Title="Regresar" ImageUrl="~/Images/dashboard/back.png" OnClientClick="Frm.ShowDashBoardMenu('#Dashboard-administracion', '#Dashboard-main')" />
                </td>
            </tr>
        </table>
    </fieldset>
    <script type="text/javascript">
        Frm.DisplayTableMenu();            
    </script>
</asp:Content>
