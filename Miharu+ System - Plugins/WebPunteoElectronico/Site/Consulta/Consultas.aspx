<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterForm.Master" AutoEventWireup="true"
    CodeBehind="Consultas.aspx.cs" Inherits="WebPunteoElectronico.Site.Consulta.Consultas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentPlaceHolder" runat="server">
    <script src="../../Scripts/Jquery/jquery.maskedinput-1.3.min.js" type="text/javascript"></script>
    <script src="../../Scripts/Jquery/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/Jquery/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Scripts/Jquery/jquery.ui.position.js" type="text/javascript"></script>
    <script src="../../Scripts/Jquery/jquery.ui.autocomplete.js" type="text/javascript"></script>
    <script src="../../Scripts/Jquery/jquery.ui.datepicker.js" type="text/javascript"></script>
    <script src="../../Scripts/Jquery/jquery.ui.dialog.js" type="text/javascript"></script>
    <script src="Consultas.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContentPlaceHolder" runat="server">
    <script type="text/javascript">
        var grillaColModel = <%= GetGrillaColModel() %>;
        var COBSList = <%= GetCOBSJavascript() %>;
        var OficinasList = [];
        var TiposTransaccionList = <%= GetTiposTransaccion() %>;               
    </script>
    <div style="position: absolute; top: 20px; right: 10px; left: 10px; bottom: 5px">
        <table border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" class="page-table">
                        <tr>
                            <td class="cs100 label">
                                COB:
                            </td>
                            <td class="cs10">
                                &nbsp;
                            </td>
                            <td>
                                <input type="text" id="COB_Input" name="COB_Input" style="width: 350px;" />
                                <input id="COBHidden" name="COBHidden" type="hidden" />
                            </td>
                            <td class="cs20">
                                &nbsp;
                            </td>
                            <td class="cs100 label">
                            </td>
                            <td class="cs10">
                                &nbsp;
                            </td>
                            <td>
                            </td>
                            <td rowspan="3" align="right" style="width: 180px">
                                <%--<input id="Buscar_Button" type="button" value="Consultar"
                                    class="button" style="height: 50px" />--%>
                                <span class="button" height="70px" onclick="Consultar();">
                                    <img src="../../Images/basic/find.png" alt="" />
                                    <br />
                                    Consultar</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="cs100 label">
                                Oficina:
                            </td>
                            <td class="cs10">
                                &nbsp;
                            </td>
                            <td>
                                <input type="text" id="Oficina_Input" name="Oficina_Input" style="width: 350px;"
                                    class="textbox" />
                            </td>
                            <td class="cs20">
                                &nbsp;
                            </td>
                            <td class="label">
                                <asp:Label ID="FechaProcesoLabel" runat="server" CssClass="label" Text=""></asp:Label>
                            </td>
                            <td class="cs10">
                                &nbsp;
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="cs100 label">
                                Tipo transacción:
                            </td>
                            <td class="cs10">
                                &nbsp;
                            </td>
                            <td>
                                <input type="text" id="TipoTransaccion_Input" name="TipoTransaccion_Input" style="width: 350px;"
                                    class="textbox" />
                            </td>
                            <td class="cs20">
                                &nbsp;
                            </td>
                            <td class="label">
                                Rango fecha:
                            </td>
                            <td class="cs10">
                                &nbsp;
                            </td>
                            <td align="left" style="width: 200px">
                                <input type="text" id="FechaInicialProc_Input" name="FechaInicialProc_Input" class="textbox"
                                    style="width: 80px" />
                                <input type="text" id="FechaFinalProc_Input" name="FechaFinalProc_Input" class="textbox"
                                    style="width: 80px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="cs100 label">
                                Campos:
                            </td>
                            <td class="cs10">
                                &nbsp;
                            </td>
                            <td colspan="5">
                                <div id="divCampos" class="divCampos" style="border: 1px solid #CCCCCC">
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="rs5">
                </td>
            </tr>
        </table>
        <input id="reload" name="reload" type="hidden" value="0" />
        <table id="flex1" style="display: none;">
        </table>
        <table border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="rs5">
                </td>
            </tr>
            <tr>
                <td align="right">
                    <input id="Generar_Button" type="button" value="Generar Reporte" class="button" onclick="window.open('ReporteConsulta.aspx?R=27&F=10');" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
