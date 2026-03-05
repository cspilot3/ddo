<%@ Page Title="" Language="C#" MasterPageFile="~/master/master_form.Master" AutoEventWireup="true"
    CodeBehind="reportes.aspx.cs" Inherits="WebSantander.site.reportes.reportes" %>

<%@ Register TagPrefix="cc1" Namespace="WebSantander.controls" Assembly="WebSantander" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    </script>
    <script src="reportes.js" type="text/javascript"></script>
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
    </script>
    <table class="aleft borde" style="position: absolute; top: 10px; left: 10px; width:1200px;
        height:auto;">
        <tr>
            <td>
                Fecha Proceso 
<input id="fechaProcesoInicialInput" type="text" style="width: 90px;" maxlength="10" />
<input id="fechaProcesoFinalInput" type="text" style="width: 90px;" maxlength="10" />
<input id="reporteInput" type="text" style="width: 90px; display:none;" maxlength="10"; />  
            </td>
        </tr>
        <tr>
                <td colspan="3">
                    <iframe src="" width="100%" height="450px" id="IFrameReporte" name="IFrameReporte" frameborder="0">
                    </iframe>
                </td>
            </tr>
    </table>
</asp:Content>
