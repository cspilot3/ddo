var Pagina = "Consulta.aspx/";
var PaginaFaltanteLogico = "FaltanteLogico.aspx/";
var content = "Application/json; charset=utf-8";
var CarpetasSolicitudTodos = 0;
var DocumentosSolicitudTodos = 0;


function GetProyectoLlave() {
    var idEntidad = $("#ddlEntidad").val();
    var idProyecto = $("#ddlProyecto").val();

    if (idEntidad == null || idProyecto == null) {
        jAlert("No se encuntra ningún Proyecto configurado para su rol.", "Busqueda");
    } else {

    $.ajax({
        dataType: "json"
            , contentType: content
            , type: "POST"
            , url: Pagina + "GetProyectoLlave"
            , data: "{idEntidad: '" + idEntidad + "', idProyecto: '" + idProyecto + "'}"
            , error: function () { jAlert("Error obteniendo proyectos-llaves"); }
            , success: function (resp) {
                $("#ddlProyectoLlave").html(resp.d);
            }
    });
    }
}


function fillFaltantesLogicos(data) {


    if ((data.substring(0,5)) != 'error') {

        eval('data = ' + data);
        if (data.length > 0) {
            $("#tblFaltantesLogicos").jqGrid("clearGridData", true);
            $("#tblFaltantesLogicos").jqGrid({
                datatype: "local",
                colNames: ['Tipología', 'Crédito', 'Cédula', 'Agencia Prestamo', 'Producto Prestamo', 'Estado Documento', 'Precinto'],
                colModel: [{ name: 'Tipologia', index: 'Tipologia', width: '60px' },
                            { name: 'Credito', index: 'Credito', width: '200px' },
                            { name: 'Cedula', index: 'Cedula', width: '200px' },
                            { name: 'Agencia_Prestamo', index: 'Agencia_Prestamo', width: '200px' },
                            { name: 'Producto_Prestamo', index: 'Producto_Prestamo', width: '110px' },
                            { name: 'Estado_Documento', index: 'Estado_Documento', width: '200px' },
                            { name: 'Precinto', index: 'Precinto', width: '200px'}],
                rowNum: 10,
                autowidth: true,
                height: '300px',
                rowList: [10, 20, 30],
                pager: '#divFaltantesLogicos',
                caption: "Faltantes Logicos"
            });

            for (var i = 0; i < data.length; i++) {
                if (data[i] != undefined)
                    $("#tblFaltantesLogicos").jqGrid('addRowData', i + 1, data[i]);
            }
            deshabilitarBtnExportar(false);
        }
        else {
            jAlert("No se encontraron Faltantes", "Faltantes");
            clearTable();
            deshabilitarBtnExportar(true);
        }

        jQuery("#tblFaltantesLogicos").setGridParam({ rowNum: 10 }).trigger("reloadGrid");

    }

    else {

        alert(data);

    }
    
}

function clearTable() {
    $("#tblFaltantesLogicos").jqGrid("clearGridData", true);
}

function buscarFaltantesLogicos() {
   // if (Validar()) {

        clearTable();
        var parametros = new Array();

//        parametros[0] = "val1:'" + $('#DFecha1_DFecha1DFechaText').val() + "'";
//        parametros[1] = "val2:'" + $('#DFecha2_DFecha2DFechaText').val() + "'";
        //        parametros[2] = "idEntidad:'" + $('#ddlEntidad').val() + "'";

        parametros[0] = "idEntidad:'" + $('#ddlEntidad').val() + "'";

        $.ajax({
            dataType: "json"
            , contentType: content
            , type: "POST"
            , url: PaginaFaltanteLogico + "BuscarFaltanteLogico"
            , data: "{" + parametros.join(",") + "}"
            , beforeSend: function () { window.parent.showCargar(1); }
            , complete: function () { window.parent.showCargar(0); }
            , success: function (jsondata) { fillFaltantesLogicos(jsondata.d); }
            //, error: function () { jAlert("Error buscando data."); }
            , error: function (xhr, status, error) { jAlert("Error buscando data: " + xhr.responseText); }
        });
   // }
}

//function Validar() {
//    try {

//        EsVacio("DFecha1_DFecha1DFechaText", "Fecha inicial");
//        EsVacio("DFecha2_DFecha2DFechaText", "Fecha final");

//        var difference = GetFecha("DFecha2_DFecha2DFechaText", "Fecha final") - GetFecha("DFecha1_DFecha1DFechaText", "Fecha inicial");
//        var days = Math.round(difference / (1000 * 60 * 60 * 24));

//        if (days < 0) {jAlert("La fecha final debe ser mayor a la fecha inicial"); return false; }
//        if (days >= 15) { jAlert("El rango de fechas debe ser maximo de 15 dias"); return false; }

//    }
//    catch (ex) {
//        alert(ex);
//        return false;
//    }
//    return true;
//}

//function GetFecha(id, n) {
//    try {
//        var f = $("#" + id)[0].value;

//        return new Date(f);
//    }
//    catch (ex) {
//        throw "El campo " + n + " no contiene una fecha valida";
//    }
//}

function EsVacio(id, n) {
    if ($("#" + id)[0].value == "") {
        throw "El campo " + n + " es requerido";
    }
}

function deshabilitarBtnExportar(habilitar) {
    $("#BtnExportar").prop("disabled", habilitar);
}

function Exportar() {
    try {
//        var fecha1 = $.format.date(new Date($('#DFecha1_DFecha1DFechaText').val()), 'yyyyMMdd');
//        var fecha2 = $.format.date(new Date($('#DFecha2_DFecha2DFechaText').val()), 'yyyyMMdd');
       
        //ExportJQGridDataToExcel("#tblFaltantesLogicos", "Faltantes_" + fecha1 + "-" + fecha2 + ".xlsx");
        ExportJQGridDataToExcel("#tblFaltantesLogicos", "Faltantes.xlsx");
    }
    catch (ex) {
        alert(ex);
        return false;
    }
}

function GetAnnoMes(tbname) { 
    

}