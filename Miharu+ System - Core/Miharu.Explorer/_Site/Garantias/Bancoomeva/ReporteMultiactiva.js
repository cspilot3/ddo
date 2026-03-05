var Pagina = "Consulta.aspx/";
var PaginaMultiactiva = "Reporte_Multiactiva.aspx/";
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


function fillRepMultiActiva(data) {
    
    eval('data = ' + data);
    if (data.length > 0) {
        $("#tblReporte").jqGrid("clearGridData", true);
        $("#tblReporte").jqGrid({
            datatype: "local",
            colNames: ['Tipologia', 'Estado Documento', 'No Obligacion', 'Id Deudor', 'Codigo Garantia', 'Codigo Oficina', 'Codigo Linea', 'Nombre Documento'],
            colModel: [{ name: 'Tipologia', index: 'Tipologia', width: "100" },
                        { name: 'Estado_Documento', index: 'Estado_Documento', width: '150px' },
                        { name: 'No_Obligacion', index: 'No_Obligacion', width: '100px' },
                        { name: 'Id_Deudor', index: 'Id_Deudor', width: '100px' },         
                        { name: 'Codigo_Garantia', index: 'Codigo_Garantia', width: '100px' },
                        { name: 'Codigo_Oficina', index: 'Codigo_Oficina', width: '100px' },
                        { name: 'Codigo_Linea', index: 'Codigo_Linea', width: '110px' },
                        { name: 'Nombre_Documento', index: 'Nombre_Documento', width: '150px' }],
            rowNum: 10,
            autowidth: true,
            height: '300px',
            rowList: [10, 20, 30],
            pager: '#divReporte',
            caption: "Reporte Multiactiva"
        });

        for (var i = 0; i < data.length; i++) {
            if (data[i] != undefined)
                $("#tblReporte").jqGrid('addRowData', i + 1, data[i]);
        }
        deshabilitarBtnExportar(false);     
    } 
    else {
        jAlert("No se encontraron datos", "Reporte Multiactiva");
        clearTable();
        deshabilitarBtnExportar(true);
    }

    jQuery("#tblReporte").setGridParam({ rowNum: 10 }).trigger("reloadGrid");
    
}

function clearTable() {
    $("#tblReporte").jqGrid("clearGridData", true);
}


function buscarRepMultiActiva() {
    if (Validar()) {
        clearTable();

        var parametros = new Array();

        parametros[0] = "fecharecoleccion:'" + $('#DFecha1_DFecha1DFechaText').val() + "'";

        $.ajax({
            dataType: "json"
            , contentType: content
            , type: "POST"
            , url: PaginaMultiactiva + "BuscarReporte"
            , data: "{" + parametros.join(",") + "}"
            , beforeSend: function () { window.parent.showCargar(1); }
            , complete: function () { window.parent.showCargar(0); }
            , success: function (jsondata) { fillRepMultiActiva(jsondata.d); }
            , error: function () { jAlert("Error buscando data."); }
            , error: function (err) { jAlert(err); }
        });
    }
}

function Validar() {
    try {

        EsVacio("DFecha1_DFecha1DFechaText", "Fecha Recoleccion");

    }
    catch (err) {
        jAlert(err);
        return false;
    }
    return true;
}

function EsVacio(id, n) {
    if ($("#" + id)[0].value == "") {
        throw new Error ("El campo " + n + " es requerido");
    }
}

function deshabilitarBtnExportar(habilitar) {
    $("#BtnExportar").prop("disabled", habilitar);
}

function Exportar() {
    try {
        var fecha = $.format.date(new Date($('#DFecha1_DFecha1DFechaText').val()), 'yyyyMM');
        ExportJQGridDataToExcel("#tblReporte", "MultiActiva" + fecha + ".xlsx");
    }
    catch (ex) {
        alert(ex);
        return false;
    }
}