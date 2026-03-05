function flexigridCellFormat(name, idx, val, o)
{
    if (name == "Url" && val != "") {
        return "<span class='link' onclick='window.open(\"../../Site/VisorImagenes/VerImagen.aspx?token=" + val + "\", \"Detalle\", \"width=1000px,height=600px,resizable=yes\")'>Imagen</span>";
    } else if (name == "Anexo" && val != "") {
        return "<span class='link' onclick='window.open(\"../../Site/VisorImagenes/VerImagen.aspx?token=" + val + "\", \"Detalle\", \"width=1000px,height=600px,resizable=yes\")'>Anexo</span>";
    }else return val;
}

$(document).ready(function () {
    $("#flex1").flexigrid({
        id: 'flex1',
        url: 'Consulta_Data.aspx',
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
        isMultiCheck :false,
        onRowDblClick: function (e, r) {
//            var dt = $('#flex1')[0].rows[r].cells[0].innerText;

//            window.open("VerDetalleData.aspx?dt=" + dt, "Detalle", "width=1000px,height=600px,resizable=yes");
        }

    });

    try { parent.SubformCargado(); $("#headerlogo").remove(); }
    catch (e) { }

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
    CreateAutoComplete("TipoTransaccion_Input", TiposTransaccionList, function (event, ui) { MostrarCampos(ui.item.value) });

    $('#TipoTransaccion_Input').keydown(function (event) {
        if (event.keyCode == '13' || event.keyCode == '9') { MostrarCampos($('#TipoTransaccion_Input')[0].value); }
    });
});

function addFormData() {
    var dt = [];
    dt[dt.length] = { name: 'FechaInicialProc_Input', value: Get('FechaInicialProc_Input').value };
    dt[dt.length] = { name: 'FechaFinalProc_Input', value: Get('FechaFinalProc_Input').value };
    dt[dt.length] = { name: 'Oficina_Input', value: Get('Oficina_Input').value };
    dt[dt.length] = { name: 'TipoTransaccion_Input', value: Get('TipoTransaccion_Input').value };
    dt[dt.length] = { name: 'COB_Input', value: Get('COB_Input').value };

    $("input", "#divCampos").each(function (i, v) {
        dt[dt.length] = { name: v.id, value: v.value };
    });

    $("#flex1").flexOptions({ params: dt });
    return true;
}

var UltimoTipoTransaccion = "";

function MostrarCampos(val) {
    if (UltimoTipoTransaccion != val) {
        if (ArrayContains(TiposTransaccionList, val)) {
            UltimoTipoTransaccion = val;

            $.ajax({
                type: "POST",
                url: "Consultas.aspx/MostrarCampos",
                data: "{'tipotransaccion':'" + val + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    // Replace the div's content with the page method's return.
                    $("#divCampos").html(msg.d);
                    ConfigureCalendars();
                    ConfigureLists();
                }
            });
        }
    }
}

function MostrarOficinas(val) {
    $.ajax({
        type: "POST",
        url: "Consultas.aspx/MostrarOficinas",
        data: "{'nStrCOB':'" + val + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            eval("OficinasList = " + msg.d);
            $("#Oficina_Input").autocomplete("option", "source", OficinasList);
        },
        error: function (a, b, err) {
            alert(err); 
        }
    });

}

function Consultar() {
    if (Validar()) {
        $("#reload").val("1");
        $("#flex1").flexReload();
        $("#reload").val("0");
    }
}

function Validar() {
    try {
        EsVacio("COB_Input", "COB");
        EsVacio("FechaInicialProc_Input", "Fecha inicial");
        EsVacio("FechaFinalProc_Input", "Fecha final");

        var difference = GetFecha("FechaFinalProc_Input", "Fecha final") - GetFecha("FechaInicialProc_Input", "Fecha inicial");
        var days = Math.round(difference / (1000 * 60 * 60 * 24));

        if (days < 0) { throw "La fecha final debe ser mayor a la fecha inicial" }
        if (days >= 15) { throw "El rango de fechas debe ser maximo de 15 dias" }
                
    }
    catch (ex) {
        alert(ex);
        return false;
    }
    return true;
}

function EsVacio(id, n) {
    if ($("#" + id)[0].value == "") {
        throw "El campo " + n + " es requerido";
    }
}

function GetFecha(id, n) {
    try {
        var f = $("#" + id)[0].value;

        return new Date(f);
    }
    catch (ex) {
        throw "El campo " + n + " no contiene una fecha valida";
    }
}

function ConfigureCalendars() {
    var calendarios = document.getElementById('fecha_campos').value.split(',');
    for (var i = 0; i < calendarios.length; i++) {
        if (calendarios[i] != "") {
            $("#" + calendarios[i]).mask("9999/99/99");
            $("#" + calendarios[i]).datepicker();
        }
    }
}

function ListaInfo(nFk_Campo_Lista, nListaDetalle) {
    this.Fk_Campo_Lista = nFk_Campo_Lista;
    this.ListaDetalle = nListaDetalle;
}
var ListasData = new Array();

function ConfigureLists() {
    try {
        ListasData = new Array();

        var valoresDeListas = document.getElementById('lista_data').value.split(';');
        for (var i = 0; i < valoresDeListas.length; i++) {
            var valores = valoresDeListas[i];
            if (valores != "") {
                var l = valores.split('=');
                var detalle = new Array();
                var elementos = l[1].split(',');
                for (var j = 0; j < elementos.length; j++) {
                    var item = elementos[j].split(':');
                    detalle[j] = item[1];
                }

                var info = new ListaInfo(l[0], detalle);
                ListasData[ListasData.length] = info;
            }
        }

        var listas = document.getElementById('lista_campos').value.split(',');
        for (var i = 0; i < listas.length; i++) {
            if (listas[i] != "") {
                var lista = listas[i].split(':');

                for (var k = 0; k < ListasData.length; k++) {
                    if (ListasData[k].Fk_Campo_Lista == lista[1]) {
                        CreateAutoComplete(lista[0], ListasData[k].ListaDetalle);
                        break;
                    }
                }
            }
        }
    } catch (e) { }
}

function ArrayContains(a, obj) {
    for (var i = 0; i < a.length; i++) {
        if (a[i] === obj) {
            return true;
        }
    }
    return false;
}

function CreateAutoComplete(id, src, sel) {
    var inCtr = document.getElementById(id);
    var parentid = id + "_parent";
    var buttonid = id + "_div";

    var parent = inCtr.offsetParent;
    if (parent == null || parent == undefined) parent = inCtr.parentElement;

    var ctrWidth = $(inCtr).width();

    parent.id = parentid;

    $('#' + id).remove();
    var html = "<table cellpadding='0' cellspacing='0'> <tr> <td> <input type='text' id='" + id + "' name='" + id + "' class='cinp' /> </td> <td> <div id='" + buttonid + "' class='blist'></div></td></tr></table>";

    $("#" + parentid).append(html);
    $("#" + id).autocomplete({ source: src, select: sel });

    if (ctrWidth > 0) $("#" + id).width(ctrWidth);

    $("#" + buttonid).click(function (e) {
        var inputId = e.currentTarget.id.replace('_div', '');
        $("#" + inputId).autocomplete("search", "");
        $("#" + inputId).trigger("focus");
    });
}

function formatNumber(control, prefix) {
    var num = control.value;
    if (num != '') {
        num = Math.round(parseFloat(num) * Math.pow(10, 2)) / Math.pow(10, 2);

        prefix = prefix || '';
        num += '';
        var splitStr = num.split('.');
        var splitLeft = splitStr[0];
        var splitRight = splitStr.length > 1 ? '.' + splitStr[1] : '.00';
        splitRight = splitRight + '00';
        splitRight = splitRight.substr(0, 3);
        var regx = /(\d+)(\d{3})/;
        while (regx.test(splitLeft)) {
            splitLeft = splitLeft.replace(regx, '$1' + ',' + '$2');
        }
        if (splitLeft == 'NaN') {
            control.value = '';
        }
        else
            control.value = prefix + splitLeft + splitRight;
    }
    else
        control.value = '';
}
function LimpiaControl(control) {
    control.value = '';
}

function Reporte_Button_onclick() {
}