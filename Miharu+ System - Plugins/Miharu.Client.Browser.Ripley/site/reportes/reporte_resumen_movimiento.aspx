<%@ Page Title="" Language="C#" MasterPageFile="~/master/master_form.Master" AutoEventWireup="true" 
CodeBehind="reporte_resumen_movimiento.aspx.cs" Inherits="Miharu.Client.Browser.site.reportes.reporte_resumen_movimiento" %>
<%@ Import Namespace="Miharu.Client.Browser.code" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script src="reporte_resumen_movimiento.js" type="text/javascript"></script>
    <script src="<%=ResolveClientUrl("~/scripts/Utils.js")%>" type="text/javascript"></script>
    <script src="<%=ResolveClientUrl("~/scripts/jquery/js/jquery.maskedinput-1.3.min.js")%>" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    
    <script type="text/javascript">
        Global.EditOptions = <%= EditOptions.GetJson() %>;
        <%=GetScriptVariables() %>
    </script>
    <div class="form">
        <table width="100%">
            <tr>
                <td style="width: 60px">
                    Fecha Inicio:
                </td>
                <td>
                    <input id="fechaInicialInput" type="text" style="width: 180px" />
                </td>
                <td style="width: 60px">
                    Fecha Final:
                </td>
                <td>
                    <input id="fechaFinalInput" type="text" style="width: 180px" />
                </td>
            </tr>
              <tr>
                <td style="width: 60px">
                    Compañia:
                </td>
                <td>
                    <input id="proyectoInput" type="text" style="width: 180px" />
                </td>
                <td style="width: 60px"></td>
                <td></td>
            </tr>
            <tr>
                <td style="width: 60px">
                    Punto:
                </td>
                <td>
                    <input id="puntoInput" type="text" style="width: 180px" />
                </td>
                <td style="width: 60px">
                    Tipo Documental:
                </td>
                <td>
                    <input id="documentoInput" type="text" style="width: 180px" />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <iframe src="" width="100%" id="IFrameReporte" name="IFrameReporte" frameborder="0">
                    </iframe>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

