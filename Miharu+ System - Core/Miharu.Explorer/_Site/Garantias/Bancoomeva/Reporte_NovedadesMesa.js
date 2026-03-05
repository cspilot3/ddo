var PaginaNovedadesMesa = "Reporte_NovedadesMesa.aspx/";
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


function fillRepNovedadesMesa(data) {
    
    eval('data = ' + data);
    if (data.length > 0) {
        clearTable();
        $("#tblReporte").jqGrid({
            datatype: "local",
            colNames: ['Fecha de recolección', 'Id_deudor', 'Crédito', 'Nombre_estado', 'Nombre_documento', 'Pregunta validación'],
            colModel: [{ name: 'Fecha_Recoleccion', index: 'Fecha_Recoleccion', width: "100px" },
                        { name: 'Id_Deudor', index: 'Id_Deudor', width: '150px' },
                        { name: 'No_Credito', index: 'No_Credito', width: '100px' },
                        { name: 'Nombre_Estado', index: 'Nombre_Estado', width: '100px' },
                        { name: 'Nombre_Documento', index: 'Nombre_Documento', width: '100px' },
                        { name: 'Novedad', index: 'Novedad', width: '100px' }],
            rowNum: 10,
            autowidth: true,
            height: '300px',
            rowList: [10, 20, 30],
            pager: '#divReporte',
            caption: "Reporte Novedades en Mesa"
        });

        for (var i = 0; i < data.length; i++) {
            if (data[i] != undefined)
                $("#tblReporte").jqGrid('addRowData', i + 1, data[i]);
        }

        deshabilitarBtnExportar(false);     
    } 
    else {
        jAlert("No se encontraron datos", "Reporte Novedades en Mesa");
        clearTable();
        deshabilitarBtnExportar(true);      
    }

    jQuery("#tblReporte").setGridParam({ rowNum: 10 }).trigger("reloadGrid");

}

function clearTable() {
    $("#tblReporte").jqGrid("clearGridData", true);
}


function buscarRepNovedadesMesa() {
    if (Validar()) {
    

        $("#tblReporte").jqGrid("clearGridData", true);

        var parametros = new Array();

        parametros[0] = "fecharecoleccionini:'" + $('#DFecha1_DFecha1DFechaText').val() + "'";
        parametros[1] = "fecharecoleccionfin:'" + $('#DFecha2_DFecha2DFechaText').val() + "'";

        $.ajax({
            dataType: "json"
            , contentType: content
            , type: "POST"
            , url: PaginaNovedadesMesa + "BuscarReporte"
            , data: "{" + parametros.join(",") + "}"
            , beforeSend: function () { window.parent.showCargar(1); }
            , complete: function () { window.parent.showCargar(0); }
            , success: function (jsondata) { fillRepNovedadesMesa(jsondata.d); }
            , error: function () { jAlert("Error buscando data."); }
            , error: function (err) { jAlert(err);}
        });
    }
}

function Validar() {
    try {

        EsVacio("DFecha1_DFecha1DFechaText", "Fecha Recoleccion");
        EsVacio("DFecha2_DFecha2DFechaText", "Fecha Recoleccion");


	var difference = GetFecha("DFecha2_DFecha2DFechaText", "Fecha final") - GetFecha("DFecha1_DFecha1DFechaText", "Fecha inicial");
	var days = Math.round(difference / (1000 * 60 * 60 * 24));


	if (days < 0) {jAlert("La fecha final debe ser mayor a la fecha inicial"); return false; }
	if (days >= 15) { jAlert("El rango de fechas debe ser maximo de 15 dias"); return false; }
    }
    catch (err) {
        jAlert(err);
        return false;
    }
    return true;
}

function EsVacio(id, n) {
    if ($("#" + id)[0].value == "") {
        throw new Error("El campo " + n + " es requerido");
    }
}

function deshabilitarBtnExportar(habilitar) {
    $("#BtnExportar").prop("disabled", habilitar);
}

function Exportar() {
    try {
        var fecha = $.format.date(new Date($('#DFecha1_DFecha1DFechaText').val()), 'yyyyMM');

        ExportJQGridDataToExcel("#tblReporte", "Novedades En Mesa" + fecha + ".xlsx");
    }
    catch (ex) {
        alert(ex);
        return false;
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

