<%@ Page Title="" Language="C#" MasterPageFile="~/master/master_form.Master" AutoEventWireup="true"
    CodeBehind="consultas.aspx.cs" Inherits="Miharu.Client.Browser.site.consulta.consultas" %>

<%@ Register TagPrefix="slygblock" Namespace="Miharu.Client.Browser.code.Grid" Assembly="Miharu.Client.Browser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
            top: 110px;
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
    <table class="aleft borde" style="position: absolute; top: 10px; left: 10px; width: 395px;
        height: 75px;">
        <tr>
            <td class="cs100">
                Fecha
            </td>
            <td class="cs20">
            </td>
            <td>
                <input id="fechaInicialInput" type="text" style="width: 100px;" maxlength="30" />
            </td>
            <td class="aright">
                <input id="fechaFinalInput" type="text" style="width: 100px;" maxlength="30" />
            </td>
        </tr>
        <tr>
            <td>
                Compañia
            </td>
            <td class="cs20">
            </td>
            <td colspan="2">
                <input id="proyectoInput" type="text" style="width: 240px;" />
            </td>
        </tr>
        <tr>
            <td>
                Punto
            </td>
            <td class="cs20">
            </td>
            <td colspan="2">
                <input id="puntoInput" type="text" style="width: 240px;" />
            </td>
        </tr>
        <tr>
            <td>
                Tipo de Documento
            </td>
            <td class="cs20">
            </td>
            <td colspan="2">
                <input id="documentoInput" type="text" style="width: 240px;" />
            </td>
        </tr>
    </table>
    <div class="borde auto" style="position: absolute; top: 10px; left: 420px; width: 500px;
        height: 75px;">
        <table class="aleft" style="width: 100%;">
            <tbody id="CamposContainer">
            </tbody>
        </table>
    </div>
    <slygblock:SlygFlexigrid ID="MainGrid" runat="server" Title="Resultados" UrlData="../../controls/proxy_data.aspx"
        OnRowDblClick="Frm.MainGridClick" CssClass="gridclass1" />
    <div class="gridclass2 borde auto">
        <table width="100%">
            <thead class="titulo">
                <tr>
                    <td>
                        Validaciones
                    </td>
                    <td>
                    </td>
                </tr>
            </thead>
            <tbody id="ValidacionesContainer" class="table">
            </tbody>
        </table>
    </div>
</asp:Content>
