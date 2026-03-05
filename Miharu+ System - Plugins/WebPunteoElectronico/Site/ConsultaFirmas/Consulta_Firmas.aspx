<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterForm.Master" AutoEventWireup="true" 
    CodeBehind="Consulta_Firmas.aspx.cs" Inherits="WebPunteoElectronico.Site.ConsultaFirmas.Consulta_Firmas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentPlaceHolder" runat="server">
    <script src="../../Scripts/Jquery/jquery.maskedinput-1.3.min.js" type="text/javascript"></script>
    <script src="../../Scripts/Jquery/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/Jquery/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Scripts/Jquery/jquery.ui.position.js" type="text/javascript"></script>
    <script src="../../Scripts/Jquery/jquery.ui.autocomplete.js" type="text/javascript"></script>
    <script src="../../Scripts/Jquery/jquery.ui.datepicker.js" type="text/javascript"></script>
    <script src="../../Scripts/Jquery/jquery.ui.dialog.js" type="text/javascript"></script>
    <script src="Consulta_Firmas.js" type="text/javascript"></script>
    <script type="text/javascript">
    var grillaColModel = <%= GetGrillaColModel() %>;
    var COBSList = <%= GetCOBSJavascript() %>;
    var OficinasList = [];
    var TransaccionFirmasList = <%= GetTransaccionFirmas() %>; 

    function flexigridCellFormat(name, idx, val, o) {
    if (name == "Url" && val != "") 
    {
        if (isNaN(val))
            return "<span class='link' onclick='window.open(\"../../Site/VisorImagenes/VerImagen.aspx?token=" + val + "\", \"Detalle\", \"width=1000px,height=600px,resizable=yes\")'>Imagen</span>";
        else
            return "<span class='link' onclick='window.open(\"../../Site/ConsultaFirmas/VerTxLog.aspx?tx=" + val + "\", \"Detalle\", \"width=1000px,height=600px,resizable=yes\")'>Registro</span>";
    }
      else if (name == "Anexo" && val != "") 
    {
        return "<span class='link' onclick='window.open(\"../../Site/VisorImagenes/VerImagen.aspx?token=" + val + "\", \"Detalle\", \"width=1000px,height=600px,resizable=yes\")'>Anexo</span>";
    } 
    else 
        return val;
}

        $(document).ready(function () {
            $("#flex1").flexigrid({
                id: 'flex1',
                url: '../ConsultaFirmas/Consulta_Firmas_Data.aspx',
                dataType: 'json',
                colModel: grillaColModel,
                usepager: true,
                title: 'Transacciones',
                useRp: true,
                rp: 15,
                showTableToggleBtn: true,
                width: 'auto',
                //autoWidth: true,
                onSubmit: addFormData,
                height: 240,
                onChangeSort: false,
                isMultiCheck: false,
                onRowDblClick: function (e, r) {
                    var dt = $('#flex1')[0].rows[r].cells[0].innerText;
                    window.open("VerTxLog.aspx?dt=" + dt, "Detalle", "width=600px,height=600px,resizable=yes");
                }

            });

            try { parent.SubformCargado(); $("#headerlogo").remove(); }
            catch (e) { }

            $("#FechaInicialMov_Input").mask("9999/99/99");
            $("#FechaInicialMov_Input").datepicker();

            $("#FechaFinalMov_Input").mask("9999/99/99");
            $("#FechaFinalMov_Input").datepicker();

            $("#FechaInicialProc_Input").mask("9999/99/99");
            $("#FechaInicialProc_Input").datepicker();

            $("#FechaFinalProc_Input").mask("9999/99/99");
            $("#FechaFinalProc_Input").datepicker();

            CreateAutoComplete("COB_Input", COBSList);
            $("#COB_Input").autocomplete("option", "select", function () {
                setTimeout(function () {
                    var cob = document.getElementById("COB_Input").value;
                    MostrarOficinas(cob);
                }, 200);
            });

            CreateAutoComplete("Oficina_Input", OficinasList);
            CreateAutoComplete("TipoTransaccion_Input", TransaccionFirmasList);
            //CreateAutoComplete("TipoTransaccion_Input", TiposTransaccionList, function (event, ui) { MostrarCampos(ui.item.value) });

//            $('#TipoTransaccion_Input').keydown(function (event) {
//                if (event.keyCode == '13' || event.keyCode == '9') { MostrarCampos($('#TipoTransaccion_Input')[0].value); }
//            });
        });   
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContentPlaceHolder" runat="server">
        <script type="text/javascript">
        var grillaColModel = <%= GetGrillaColModel() %>;
        var COBSList = <%= GetCOBSJavascript() %>;
        var OficinasList = [];
//        var TiposTransaccionList = <%= GetTiposTransaccion() %>;  
//        var TransaccionFirmasList = <%= GetTransaccionFirmas() %>; 
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
                                Usuarios:
                            </td>
                            <td class="cs10">
                                &nbsp;
                            </td>
                            <td>
                                <input type="text" id="Usuarios_Input" name="Usuarios_Input" style="width: 370px;"
                                    class="textbox" />
                            </td>
                             <td class="cs20">
                                &nbsp;
                            </td>
                            <td class="label">
                                Fecha Movimiento:
                            </td>
                            <td class="cs10">
                                &nbsp;
                            </td>
                            <td align="left" style="width: 200px">
                                <input type="text" id="FechaInicialMov_Input" name="FechaInicialMov_Input" class="textbox"
                                    style="width: 80px" />
                                <input type="text" id="FechaFinalMov_Input" name="FechaInicialMov_Input" class="textbox"
                                    style="width: 80px" />
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
                                Fecha Proceso:
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
                                Nro Producto:
                            </td>
                            <td class="cs10">
                                &nbsp;
                            </td>
                            <td>
                                <input type="text" id="Nro_Producto_Input" name="Nro_Producto_Input" style="width: 370px;"
                                    class="textbox" />
                            </td>
                        </tr>
                         <tr>
                         <td class="cs100 label">
                                Nro Ente:
                            </td>
                            <td class="cs10">
                                &nbsp;
                            </td>
                            <td>
                                <input type="text" id="Nro_Ente_Input" name="Nro_Ente_Input" style="width: 370px;"
                                    class="textbox" />
                            </td>
                        </tr>
                        </table>
                </td>
            </tr>
            <tr>
                <td class="rs5">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="rs5">
                </td>
            </tr>
        </table>
        <input id="reload" name="reload" type="hidden" value="0" />
        <table id="flex1" style="display: none;">
        </table>
       <%-- <table border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="rs5">
                </td>
            </tr>
            <tr>
                <td align="right">
                    <input id="Generar_Button" type="button" value="Generar Reporte" class="button" onclick="window.open('ReporteConsulta.aspx?R=27&F=10');" />
                </td>
            </tr>
        </table>--%>
        <input id="Generar_Button" type="button" value="Generar Reporte" class="button" onclick="window.open('ReporteConsultas.aspx?R=10&F=10');" align="left" />
    </div>
</asp:Content>
