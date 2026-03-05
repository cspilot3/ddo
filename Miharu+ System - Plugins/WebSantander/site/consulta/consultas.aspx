<%@ Page Title="" Language="C#" MasterPageFile="~/master/master_form.Master" AutoEventWireup="true"
    CodeBehind="consultas.aspx.cs" Inherits="WebSantander.site.consulta.consultas" %>

<%@ Register TagPrefix="slygblock" Namespace="WebSantander.code.Grid" Assembly="WebSantander" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    </script>
    <script src="consultas.js" type="text/javascript"></script>
    <script src="<%=ResolveClientUrl("~/scripts/jquery/js/flexigrid.custom.js")%>" type="text/javascript"></script>
    <link href="<%=ResolveClientUrl("~/styles/flexigrid-1.1/flexigrid.css")%>" rel="stylesheet"
        type="text/css" />
    <script src="<%=ResolveClientUrl("~/scripts/jquery/js/jquery.alphanumeric.js")%>"
        type="text/javascript"></script>
    <script src="<%=ResolveClientUrl("~/scripts/jquery/js/jquery.maskedinput-1.3.min.js")%>"
        type="text/javascript"></script>
    <style type="text/css">
        .gridclass1
        {
            position: absolute;
            left: 10px;
            right: 320px;
            bottom: 10px;
            top: 138px;
            text-align: left;
        }
        .gridclass2
        {
            position: absolute;
            width: 300px;
            right: 10px;
            bottom: 10px;
            top: 90px;
            text-align: left;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <script type="text/javascript">
        Global.EditOptions = <%= EditOptions.GetJson() %>;
        <%= GetScriptVariables() %>
    </script>
    <table class="aleft borde" style="position: absolute; top: 10px; left: 10px; width: 895px;
        height: 75px;">
        <tr>
            <td>
                Proyecto
            </td>
            <td class="cs20">
            </td>
            <td colspan="2">
                <input id="ProyectoInput" type="text" style="width: 220px;" />
            </td>
            <td class="cs20">
            </td>
            <td>
                Estado
            </td>
            <td class="cs20">
            </td>
            <td colspan="2">
                <input id="EstadoClienteInput" type="text" style="width: 220px;" />
            </td>
        </tr>
        <tr>
            <td class="cs100">
                Fecha de Proceso
            </td>
            <td class="cs20">
            </td>
            <td>
                <input id="fechaProcesoInicialInput" type="text" style="width: 90px;" maxlength="10" />
            </td>
            <td class="aleft">
                <input id="fechaProcesoFinalInput" type="text" style="width: 90px;" maxlength="10" />
            </td>
            <td class="cs20">
            </td>
            <td class="cs100">
                Fecha de Recibido
            </td>
            <td class="cs20">
            </td>
            <td>
                <input id="fechaRecibidoInicialInput" type="text" style="width: 90px;" maxlength="10" />
            </td>
            <td class="aleft">
                <input id="fechaRecibidoFinalInput" type="text" style="width: 90px;" maxlength="10" />
            </td>
        </tr>
        <tr>
            <td>
                Identificación Expediente
            </td>
            <td class="cs20">
            </td>
            <td colspan="2">
                <input id="IdentificacionOficioInput" type="text" style="width: 240px;" />
            </td>
            <td class="cs20">
            </td>
            <td>
                Entidad Solicitante
            </td>
            <td class="cs20">
            </td>
            <td colspan="2">
                <input id="EntidadInput" type="text" style="width: 220px;" />
            </td>
        </tr>
        <tr>
            <td>
                No. identificación
            </td>
            <td class="cs20">
            </td>
            <td colspan="2">
                <input id="CedulaNitInput" type="text" style="width: 240px;" />
            </td>
        </tr>
    </table>
    <slygblock:SlygFlexigrid ID="MainGrid" runat="server" Title="Resultados" UrlData="../../controls/proxy_data.aspx"
        OnRowDblClick="Frm.MainGridClick" CssClass="gridclass1" />
</asp:Content>
