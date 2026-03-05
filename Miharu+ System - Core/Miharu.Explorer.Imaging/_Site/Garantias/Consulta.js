var Pagina = "Consulta.aspx/";
var content = "Application/json; charset=utf-8";
var CarpetasSolicitudTodos = 0;
var DocumentosSolicitudTodos = 0;
var Llaves;

$(function () {
    var valueCombo = $("#ComboCampoBusqueda").val();
    var valueComboDataAsociada = $("#ComboCampoBusquedaDetalle").val();
    $("#ddlParametro1").html(valueCombo);
//    $("#ddlParametroDataAsociada").html(valueComboDataAsociada);
    $("#tabs").tabs({ height: '550px' });
    $("#ddlEntidad").change(function () { GetProyecto(); });
    $("#ddlProyecto").change(function () { GetProyectoLlave(); });
    GetProyecto();
});

function GetHistorialSolicitud(codigo, TipoDB, tipo) {
    $.ajax({
        dataType: "json"
        , contentType: content
        , type: "POST"
        , url: Pagina + "GetHistorialSolicitud"
        , data: "{codigo:'" + codigo + "', TipoDB: '" + TipoDB + "', tipo: " + tipo + "}"
        , beforeSend: function () { window.parent.showCargar(1); }
        , complete: function () { window.parent.showCargar(0); }
        , error: function () { jAlert("Error obteniendo historial de solicitudes."); }
        , success: function (resp) { fillHistorialSolcitud(resp.d); $("#HistorialSolicitud").dialog({ width: 680, height: 400, modal: true }); }
    });
}

function fillHistorialSolcitud(data) {
    eval("data = " + data);

    $("#tblHistorialSolicitud").jqGrid("clearGridData", true);
    $("#tblHistorialSolicitud").jqGrid({
        datatype: "local"
        , colNames: ['Codigo', 'Numero Solicitud', 'Fecha Solicitud', 'Usuario Solicitante', 'Motivo Solicitud', 'Tipo solicitud', 'Prioridad Solicitud', 'Fecha Envió', 'Usuario Destino', 'Sello', 'Guia']
        , colModel: [{ name: 'CBarras_File', index: 'CBarras_File', width: '100px' }
            , { name: 'id_Solicitud', index: 'id_Solicitud', width: '60px' }
            , { name: 'Fecha_Solicitud', index: 'Fecha_Solicitud', width: '90px' }
            , { name: 'UsuarioSolicitud', index: 'UsuarioSolicitud', width: '200px' }
            , { name: 'Nombre_Solicitud_Motivo', index: 'Nombre_Solicitud_Motivo', width: '110px' }
            , { name: 'Nombre_Solicitud_Tipo', index: 'Nombre_Solicitud_Tipo', width: '110px' }
            , { name: 'Nombre_Solicitud_Prioridad', index: 'Nombre_Solicitud_Prioridad', width: '110px' }
            , { name: 'Fecha_Log', index: 'Fecha_Log', width: '90px' }
            , { name: 'UsuarioDestino', index: 'UsuarioDestino', width: '200px' }
            , { name: 'Sello', index: 'Sello', width: '60px' }
            , { name: 'Guia', index: 'Guia', width: '60px' }
        ]
        , autowidth: true
        , height: 270
        , rowNum: 10
        , rowList: [10, 20, 30]
        , pager: "#divHistorialSolicitud"
        , caption: "Historial solicitudes"
    });

    for (var i = 0; i <= data.length; i++) { $("#tblHistorialSolicitud").jqGrid('addRowData', i + 1, data[i]); }
}

function GetProyecto() {
    var idEntidad = $("#ddlEntidad").val();
    $("#txtllave").val("");

    $.ajax({
        dataType: "json"
        , contentType: content
        , type: "POST"
        , url: Pagina + "GetProyecto"
        , data: "{idEntidad: '" + idEntidad + "'}"
        , error: function () { jAlert("Error obteniendo proyectos"); }
        , success: function (resp) { $("#ddlProyecto").html(resp.d); GetProyectoLlave(); }
    });
}

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

function MostrarUsuario(value) {
    if (value == 1) {
        $("#divUsuarioEntidad").hide();
        $("#divSolicitante").hide();
    } else if (value == 2) {
        $("#divUsuarioEntidad").show();
        $("#divSolicitante").hide();
    } else {
        $("#divUsuarioEntidad").hide();
        $("#divSolicitante").show();
    }
}

function changePrioridad() {
    var data = $("#tblGestion").jqGrid('getGridParam', 'data');
    var value = $("#ddlPrioridad").val();

    for (var i = 0; i < data.length; i++) {
        $('#tblGestion').setCell(data[i].id, 'Prioridad', value);
    }
}

function changeMotivo() {
    var data = $("#tblGestion").jqGrid('getGridParam', 'data');
    var value = $("#ddlMotivo").val();

    for (var i = 0; i < data.length; i++) {
        $('#tblGestion').setCell(data[i].id, 'Motivo', value);
    }
}

function changeTipo() {
    var data = $("#tblGestion").jqGrid('getGridParam', 'data');
    var value = $("#ddlTipo").val();

    for (var i = 0; i < data.length; i++) {
        $('#tblGestion').setCell(data[i].id, 'TipoSolicitud', value);
    }
}

function GuardarSolicitud() {
    var data = $("#tblGestion").jqGrid('getGridParam', 'data');
    var info = new Array();

    /*Busca el tipo de solicitante*/
    var usuarioEntidad = $("#rdUsuarioEntidad").attr("checked") == "checked";
    var solicitante = $("#rdSolicitante").attr("checked") == "checked";
    var usuarioSoliciud = "1";

    if (usuarioEntidad) {
        usuarioSoliciud = "2;" + $("#ddlUsuarioEntidad").val();
    } else if (solicitante) {
        var nombre = $("#txtNombre").val();
        var apellido = $("#txtApellido").val();
        var identificacion = $("#txtIdentificacion").val();
        var entidad = $("#txtEntidad").val();
        var direccion = $("#txtDireccion").val();
        var departamento = $("#txtDepartamento").val();
        var ciudad = $("#txtCiudad").val();
        var datosSolicitante = nombre + "," + apellido + "," + identificacion + "," + entidad + "," + direccion + "," + departamento + "," + ciudad;
        usuarioSoliciud = "3;" + datosSolicitante;
    }

    /*Busca las solicitudes y las envia*/
    for (var i = 0; i <= data.length - 1; i++) {
        var tipoSolicitud = data[i]['TipoSolicitud'];
        var motivo = data[i]['Motivo'];
        var prioridad = data[i]['Prioridad'];
        var codigo = data[i]['Codigo'];
        info[i] = codigo + '-' + motivo + '-' + tipoSolicitud + '-' + prioridad;
    }

    $.ajax({
        dataType: "json"
        , contentType: content
        , type: "POST"
        , url: Pagina + "GestionSolicitud"
        , data: "{gestiones: '" + info.join(";") + "', usuario: '" + usuarioSoliciud + "'}"
        , beforeSend: function () { window.parent.showCargar(1); }
        , complete: function () { window.parent.showCargar(0); }
        , error: function (err) { alert(err); }
        , success: function (resp) {
            $("#GestionSolicitud").dialog("close");
            clearTables();
            jAlert(resp.d, "Gestión Solicitud");
        }
    });
}

function fillFolder(data) {
    if (data != "La sesion ha caducado") {
        eval('data = ' + data);
        $("#tblCarpetas").jqGrid("clearGridData", true);
        $("#tblCarpetas").jqGrid({
            datatype: "local",
            colNames: ['S', 'H', 'Expediente', 'Entidad', 'Proyecto', 'Esquema', 'Codigo', 'Estado', 'Estado', 'Data 1', 'Data 2', 'Data 3', 'id_Entidad', 'FilesCustodia', 'llaves', 'Tipo','TipoDB', 'Fecha Proceso P&C'],
            colModel: [{ name: 'Solicitado', index: 'Solicitado', width: '25px', formatter: solicitudFormat },
                    { name: 'historia', index: 'historia', width: '25px', formatter: historiaSFormat },
                    { name: 'id_Expediente', index: 'id_Expediente', hidden: true },
                    { name: 'Nombre_Entidad', index: 'Nombre_Entidad' },
                    { name: 'Nombre_Proyecto', index: 'Nombre_Proyecto' },
                    { name: 'Nombre_Esquema', index: 'Nombre_Esquema' },
                    { name: 'CBarras_Folder', index: 'CBarras_Folder' },
                    { name: 'id_Estado', index: 'id_Estado', hidden: true },
                    { name: 'Nombre_Estado', index: 'Nombre_Estado' },
                    { name: 'Data_1', index: 'Data_1' },
                    { name: 'Data_2', index: 'Data_2' },
                    { name: 'Data_3', index: 'Data_3' },
                    { name: 'id_Entidad', index: 'id_Entidad', hidden: true },
                    { name: 'FilesCustodia', index: 'FilesCustodia', hidden: true },
                    { name: 'llaves', index: 'llaves', hidden: true},
                    { name: 'Tipo', index: 'Tipo', hidden: true },
                    { name: 'TipoDB', index: 'TipoDB', hidden: true},
                    { name: 'FechaProceso', index: 'FechaProceso'}],
            autowidth: true,
            rowNum: 10,
            rowList: [10, 20, 30],
            onCellSelect: function (rowid, index) {
                var codigo = $('#tblCarpetas').getCell(rowid, 'CBarras_Folder');
                var llaves = $('#tblCarpetas').getCell(rowid, 'llaves');
                var tipo = $('#tblCarpetas').getCell(rowid, 'Tipo');
                var tipoDB = $('#tblCarpetas').getCell(rowid, 'TipoDB');
                $('#hdnllavesFolder').val(llaves);

                switch (index) {
                    case 0:
                        if ($('#tblCarpetas').getCell(rowid, 'id_Estado') == '1100') {
                            if ($('#tblCarpetas').getCell(rowid, 'id_Entidad') == $("#EntidadUsuario").val()) {
                                if ($('#tblCarpetas').getCell(rowid, 'FilesCustodia') > 0) {
                                    if ($("#PermisoSolicitudes").val() == '1')
                                        AddCodigo(codigo, rowid, true);
                                    else
                                        jAlert('No tiene los permisos requeridos para efectuar solicitudes', 'Solicitud');
                                }
                                else {
                                    jAlert('Las carpeta no se encuentra con ningún documento en custodia', 'Solicitud');
                                }
                            }
                            else
                                jAlert('Las carpetas solo pueden ser solicitadas por un usuario de la misma entidad', 'Solicitud');
                        }
                        else
                            jAlert('El estado de la carpeta no es valido para solicitud', 'Solicitud');
                        break;
                    case 1:
                        GetHistorialSolicitud(codigo, tipoDB, index);
                        break;
                    default:
                        getFile(codigo, llaves, tipo,tipoDB);

                }

            },
            pager: '#divCarpetas',
            caption: "Carpetas"
        });

        if (CarpetasSolicitudTodos == 0) {
            $("#tblCarpetas").navGrid('#divCarpetas', { edit: false, add: false, del: false, search: false }).navButtonAdd('#divCarpetas', {
                caption: "Solicitar todos"
            , buttonicon: "ui-icon-add"
            , onClickButton: function () { SolicitarCarpetas(); }
            , position: "last"
            });
            CarpetasSolicitudTodos = 1;
        }

        for (var i = 0; i <= data.length; i++) { jQuery("#tblCarpetas").jqGrid('addRowData', i + 1, data[i]); }
    }
    else {
        //window.location.href = "../../_Main/Login.aspx";
        parent.window.location = "../../_Main/Login.aspx";
        alert(data);
    }
}

function SolicitarCarpetas() {
    var data = $("#tblCarpetas").jqGrid('getGridParam', 'data');
    var noAgregadas = new Array();

    for (var i = 0; i < data.length; i++) {
        if (data[0].id_Estado == '1100' && data[0].FilesCustodia > 0 && data[0].id_Entidad == $("#EntidadUsuario").val()) {
            AddCodigo(data[0].CBarras_Folder, null, true);
            $('#tblCarpetas').setCell(data[i].id, 'Solicitado', '1');
        } else {
            noAgregadas[i] = data[0].CBarras_Folder;
        }
    }

    if (noAgregadas.length > 0) { jAlert('Hubo problemas al solicitar las siguientes carpetas [' + noAgregadas.join(',') + '], por favor valide que se encuentren en custodia y que tenga permisos para solictar el prestamo', 'Solicitudes'); }
}

function SolicitarDocumentos() {
    var data = $("#tblDocumentos").jqGrid('getGridParam', 'data');
    var noAgregadas = new Array();

    for (var i = 0; i < data.length; i++) {
        if (data[0].id_Estado == '1100' && data[0].id_Entidad == $("#EntidadUsuario").val()) {
            AddCodigo(data[0].CBarras_File, null, true);
            $('#tblDocumentos').setCell(data[i].id, 'Solicitado', '1');
        } else {
            noAgregadas[i] = data[0].CBarras_File;
        }
    }

    if (noAgregadas.length > 0) { jAlert('Hubo problemas al solicitar los siguientes documentos [' + noAgregadas.join(',') + '], por favor valide que se encuentren en custodia y que tenga permisos para solictar el prestamo', 'Solicitudes'); }
}

function fillFile(data) {
    eval('data = ' + data);
    $("#tblDocumentos").jqGrid("clearGridData", true);
    $("#tblDocumentos").jqGrid({
        datatype: "local",
        colNames: ['S', 'HE', 'HS', 'D', 'I', 'P', 'Codigo', 'Folios', 'Tipologia', 'Documento', 'id_Estado', 'Estado', 'id_Entidad', 'File_Unique_Identifier', 'Fk_Log_Cargue', 'fk_Documento', 'fk_Expediente', 'fk_Folder', 'id_File', 'TipoDB'],
        colModel: [{ name: 'Solicitado', index: 'Solicitado', width: '25px', formatter: solicitudFormat },                                                                                   
                    { name: 'historia_estados', index: 'historiaE', width: '25px', formatter: historiaEFormat },                                                                             
                    { name: 'historia_solicitudes', index: 'historiaS', width: '25px', formatter: historiaSFormat },
                    { name: 'data_file', index: 'dataFile', width: '25px', formatter: dataFileFormat },
                    { name: 'Imaging', index: 'Imaging', width: '25px', formatter: imageFormat },
                    { name: 'Pdf', index: 'Pdf', width: '25px', formatter: PdfFormat },
                    { name: 'CBarras_File', index: 'CBarras_File' },
                    { name: 'Folios_File', index: 'Folios_File' },
                    { name: 'Nombre_Documento', index: 'Nombre_Documento'},
//                    { name: 'Monto_File', index: 'Monto_File' },
                    { name: 'Nombre_Tipologia', index: 'Nombre_Tipologia' },
                    { name: 'id_Estado', index: 'id_Estado', hidden: true },
                    { name: 'Nombre_Estado', index: 'Nombre_Estado' },
                    { name: 'id_Entidad', index: 'id_Entidad', hidden: true },
                    { name: 'File_Unique_Identifier', index: 'File_Unique_Identifier', hidden: true },
                    { name: 'Fk_Log_Cargue', index: 'Fk_Log_Cargue', hidden: true },
                    { name: 'fk_Documento', index: 'fk_Documento', hidden: true },
                    { name: 'fk_Expediente', index: 'fk_Expediente', hidden: true},
                    { name: 'fk_Folder', index: 'fk_Folder', hidden: true},
                    { name: 'id_File', index: 'id_File', hidden: true },
                    { name: 'TipoDB', index: 'TipoDB', hidden: true}],
        autowidth: true,
        rowNum: 10,
        rowList: [10, 20, 30],
        onCellSelect: function (rowid, index) {
            var codigo = $('#tblDocumentos').getCell(rowid, 'CBarras_File');
            var fk_log_cargue = $('#tblDocumentos').getCell(rowid, 'Fk_Log_Cargue');
            var fk_Documento = $('#tblDocumentos').getCell(rowid, 'fk_Documento');
            var fk_Expediente = $('#tblDocumentos').getCell(rowid, 'fk_Expediente');
            var fk_Folder = $('#tblDocumentos').getCell(rowid, 'fk_Folder');
            var id_File = $('#tblDocumentos').getCell(rowid, 'id_File');
            var TtipoDB = $('#tblDocumentos').getCell(rowid, 'TipoDB');

            switch (index) {
                case 0:
                    if ($('#tblDocumentos').getCell(rowid, 'id_Estado') == '1100') {
                        if ($('#tblDocumentos').getCell(rowid, 'id_Entidad') == $("#EntidadUsuario").val()) {
                            if ($("#PermisoSolicitudes").val() == '1')
                                AddCodigo(codigo, rowid, false);
                            else
                                jAlert('No tiene los permisos requeridos para efectuar solicitudes', 'Solicitud');
                        }
                        else {
                            jAlert('Los documentos solo pueden ser solicitados por un usuario de la misma entidad', 'Solicitud');
                        }
                    }
                    else
                        jAlert('El estado del documento no es valido para solicitud', 'Solicitud');
                    break;
                case 1:
                    getHistorialEstados(codigo, fk_log_cargue, fk_Documento, fk_Documento, fk_Expediente, fk_Folder, id_File, TtipoDB);
                    break;
                case 2:
                    GetHistorialSolicitud(codigo, TtipoDB, index);
                    break;
                case 3:
                    GetFileData(codigo, fk_log_cargue, fk_Documento, fk_Expediente, fk_Folder, id_File, TtipoDB);
                    break;
                case 4:
                    var token = $('#tblDocumentos').getCell(rowid, 'File_Unique_Identifier');
                    var imaging = $('#tblDocumentos').getCell(rowid, 'Imaging');
                    if (imaging != '') { verImagen(token, TtipoDB); }
                    break;
                case 5:
                    var token = $('#tblDocumentos').getCell(rowid, 'File_Unique_Identifier');
                    var imaging = $('#tblDocumentos').getCell(rowid, 'Imaging');
                    if (imaging != '') { verPdf(token, TtipoDB); }
                    break;
            }

        },
        pager: '#divDocumentos',
        caption: "Documentos"
    });

    if (DocumentosSolicitudTodos == 0) {
        $("#tblDocumentos").navGrid('#divDocumentos', { edit: false, add: false, del: false, search: false }).navButtonAdd('#divDocumentos', {
            caption: "Solicitar todos"
            , buttonicon: "ui-icon-add"
            , onClickButton: function () { SolicitarDocumentos(); }
            , position: "last"
        });
        DocumentosSolicitudTodos = 1;
    }

    for (var i = 0; i <= data.length; i++) { jQuery("#tblDocumentos").jqGrid('addRowData', i + 1, data[i]); }
}

function fillFileData(data) {
    eval('data = ' + data);
    $("#tblDocumentosData").jqGrid("clearGridData", true);
    $("#tblDocumentosData").jqGrid({
        datatype: "local",
        colNames: ['Campo', 'Valor', 'Tipo', 'id_Documento', 'id_Campo', 'fk_Expediente', 'fk_Folder', 'id_File'],
        colModel: [{ name: 'Nombre_Campo', index: 'Nombre_Campo', width: '300px' },
                    { name: 'Valor_File_Data', index: 'Valor_File_Data', width: '200px' },
                    { name: 'TIPO', index: 'TIPO', hidden: true },
                    { name: 'id_Documento', index: 'id_Documento', hidden: true },
                    { name: 'id_Campo', index: 'id_Campo', hidden: true },
                    { name: 'fk_Expediente', index: 'fk_Expediente', hidden: true },
                    { name: 'fk_Folder', index: 'fk_Folder', hidden: true },
                    { name: 'id_File', index: 'id_File', hidden: true }],
        height: 300,
        rowNum: 10,
        rowList: [10, 20, 30],
        caption: "Datos",
        onCellSelect: function (rowid, index) {
            var TipoDB = $('#tblDocumentosData').getCell(rowid, 'TIPO');
            var fk_Documento = $('#tblDocumentosData').getCell(rowid, 'id_Documento');
            var fk_Campo = $('#tblDocumentosData').getCell(rowid, 'id_Campo');
            var fk_Expediente = $('#tblDocumentosData').getCell(rowid, 'fk_Expediente');
            var fk_Folder = $('#tblDocumentosData').getCell(rowid, 'fk_Folder');
            var id_File = $('#tblDocumentosData').getCell(rowid, 'id_File');
            var Valor_File_Data = $('#tblDocumentosData').getCell(rowid, 'Valor_File_Data');

            if (Valor_File_Data == '>> Tabla asociada') {
                GetFileDataAsociada(fk_Documento, fk_Campo, fk_Expediente, fk_Folder, id_File, TipoDB);
            }            
        }
    });

    for (var i = 0; i < data.length; i++) {
        if (data[i] != undefined)
            $("#tblDocumentosData").jqGrid('addRowData', i + 1, data[i]);
    }
    if (data.length >= 0) { $("#DataAsociada").dialog({ width: 530, modal: true }); }
}

function fillFileDataAsociada(data) {
    eval('data = ' + data);
    $("#tblDocumentosDataAsociada").jqGrid("GridUnload");

    var lModels = [];
    var lNames = [];

    if (data.length > 0) {
        for (var prop in data[0]) {
            if (data[0].hasOwnProperty(prop)) {
                lNames.push(prop);
                var nwidth = ''
                if (prop == 'Registro') {
                    nwidth = '80px';
                }
                else {
                    nwidth = '200px';
                }
                lModels.push({
                    name: prop,
                    width: nwidth,
                    sortable: true,
                    hidden: false
                });
            }
        }    
    }
   
    $("#tblDocumentosDataAsociada").jqGrid({
        datatype: "local",
        colNames: lNames,
        colModel: lModels,
        height: 300,
        rowNum: 10,
        rowList: [10, 20, 30],
        caption: "Datos"
    });

    for (var i = 0; i < data.length; i++) {
        if (data[i] != undefined)
            $("#tblDocumentosDataAsociada").jqGrid('addRowData', i + 1, data[i]);
    }
    if (data.length >= 0) { $("#DataTablaAsociada").dialog({ width: 630, modal: true }); }
}

function fillHistorialEstados(data) {
    eval('data = ' + data);
    $("#tblHistorialEstados").jqGrid("clearGridData", true);
    $("#tblHistorialEstados").jqGrid({
        datatype: "local",
        colNames: ['Campo', 'Valor', 'Tipo'],
        colModel: [{ name: 'Nombre_Campo', index: 'Nombre_Campo', width: '300px' },
                    { name: 'Valor_File_Data', index: 'Valor_File_Data', width: '200px' },
                    { name: 'TIPO', index: 'TIPO', hidden: true}],
        height: 300,
        rowNum: 10,
        rowList: [10, 20, 30],
        caption: "Historial Estados"
    });

    for (var i = 0; i < data.length; i++) {
        if (data[i] != undefined)
            $("#tblHistorialEstados").jqGrid('addRowData', i + 1, data[i]);
    }
    if (data.length >= 0) { $("#HistorialEstados").dialog({ width: 530, modal: true }); }
}

function fillSolicitudes(data) {
    var lastRow = -1;
    eval("data = " + data);
    var Llave1Name = $("#ddlProyectoLlave option[value='1']").text();
    var Llave2Name = "__";
    var ExisteLlave2 = false;

    if ($("#ddlProyectoLlave option[value='2']").text() != undefined && $("#ddlProyectoLlave option[value='2']").text() != "") {
        Llave2Name = $("#ddlProyectoLlave option[value='2']").text();
    } else {
        ExisteLlave2 = true;
    }

    if (data.length > 0) {
        $("#tblGestion").jqGrid("clearGridData", true);
        $("#tblGestion").jqGrid({
            datatype: "local"
                    , colNames: ["", Llave1Name, Llave2Name, "Tipo", "Motivo", "Tipo Solicitud", "Prioridad", "Codigo"]
                    , colModel: [{ name: "Accion", index: "Accion", formatter: eliminarsolicitud, width: '20px' }
                        , { name: "Llave1", index: "Llave1", width: '200px' }
                        , { name: "Llave2", index: "Llave2", width: '200px', hidden: ExisteLlave2 }
                        , { name: "Tipo", index: "Tipo", width: '160px' }
                        , { name: "Motivo", index: "Motivo", editable: true, edittype: "select", formatter: 'select', editoptions: { value: $("#ComboMotivo").val()} }
                        , { name: "TipoSolicitud", index: "TipoSolicitud", editable: true, edittype: "select", formatter: 'select', editoptions: { value: $("#ComboTipo").val()} }
                        , { name: "Prioridad", index: "Prioridad", editable: true, edittype: "select", formatter: 'select', editoptions: { value: $("#ComboPrioridad").val()} }
                        , { name: "Codigo", index: "Codigo", hidden: true }
                    ]
                    , onCellSelect: function (id, index) {
                        var codigo = $('#tblGestion').getCell(id, 'Codigo');
                        if (index == 0) {
                            AddCodigo(codigo, id, false);
                            $('#tblGestion').jqGrid('delRowData', id);
                        } else {
                            if (lastRow == id) {
                                $('#tblGestion').jqGrid('saveRow', id);
                                lastRow = -1;
                            } else {
                                if (lastRow != -1) { $('#tblGestion').jqGrid('saveRow', lastRow); }
                                $('#tblGestion').jqGrid('editRow', id, true);
                                lastRow = id;
                            }
                        }
                    }
                    , height: "330px"
                    , editurl: Pagina + "Editar"
                    , caption: "Solicitud"
        });

        $("#tblGestion").jqGrid('setGridWidth', "600", true);

        for (var i = 0; i <= data.length; i++) { $("#tblGestion").jqGrid('addRowData', i + 1, data[i]); }
        changePrioridad();
        changeMotivo();
        changeTipo();

        $("#GestionSolicitud").dialog({ width: 650, modal: true });
    } else {
        jAlert("No hay solicitudes pendientes!!", "Solicitudes");
    }
}

function clearTables() {
    $("#tblGestion").jqGrid("clearGridData", true);
    $("#tblDocumentosData").jqGrid("clearGridData", true);
    $("#tblDocumentos").jqGrid("clearGridData", true);
    $("#tblCarpetas").jqGrid("clearGridData", true);
    $("#tblHistorialSolicitud").jqGrid("clearGridData", true);
    $("#tblDocumentosDataAsociada").jqGrid("clearGridData", true);
}

function buscar() {
    clearTables();

    var parametros = new Array();
    parametros[0] = "par1:'" + $('#ddlParametro1').val() + "'";    
    parametros[1] = "val1:'" + $('#txtvalue1').val() + "'";
    parametros[2] = "idEntidad:'" + $('#ddlEntidad').val() + "'";
    parametros[3] = "idProyecto:'" + $('#ddlProyecto').val() + "'";
    parametros[4] = "idProyectoLlave:'" + $('#ddlProyectoLlave').val() + "'";
    parametros[5] = "nLlave:'" + $('#txtllave').val() + "'";
    parametros[6] = "ntipoBusqueda:'" + $('#chktipoBusqueda').prop('checked') + "'";
//    parametros[6] = "par2:'" + $('#ddlParametroDataAsociada').val() + "'";
//    parametros[7] = "val2:'" + $('#textDataAsociadaValor').val() + "'";

    $.ajax({
        dataType: "json"
        , contentType: content
        , type: "POST"
        , url: Pagina + "Buscar"
        , data: "{" + parametros.join(",") + "}"
        , beforeSend: function () { window.parent.showCargar(1); }
        , complete: function () { window.parent.showCargar(0); }
        , success: function (jsondata) { fillFolder(jsondata.d); }
        //, error: function () { jAlert("Error buscando data."); }
        , error: function (err) { jAlert(err); }
    });
}

function getFile(codigo, llaves, tipo, tipoDB) {
    $.ajax({
        dataType: "json"
        , contentType: content
        , type: "POST"
        , url: Pagina + "GetFiles"
        , data: "{codigo:'" + codigo + "' , llaves: '" + llaves + "' , tipo: '" + tipo +  "' , tipoDB: '" + tipoDB + "'}"
        , beforeSend: function () { window.parent.showCargar(1); }
        , complete: function () { window.parent.showCargar(0); }
        , success: function (jsondata) { fillFile(jsondata.d); }
        , error: function (err) { alert(err); }
    });
}

function GetFileData(codigo, fk_log_cargue, fk_Documento, fk_Expediente, fk_Folder, id_File, TtipoDB) {
    var hdnllaves = $('#hdnllavesFolder').val();
    if (fk_Expediente == "")
        fk_Expediente = 0;
    if (fk_Folder == "")
        fk_Folder = 0;
    if (id_File == "")
        id_File = 0;

    $.ajax({
        dataType: "json"
        , contentType: content
        , type: "POST"
        , url: Pagina + "GetFileData"
        , data: "{codigo:'" + codigo + "', tipo:1, fk_log_Cargue: '" + fk_log_cargue + "', fk_Documento: '" + fk_Documento + "', llaves: '" + hdnllaves + "', fk_Expediente: '" + fk_Expediente + "', fk_Folder: '" + fk_Folder + "' , id_File: '" + id_File + "', TipoDB: '" + TtipoDB + "'}"
        , beforeSend: function () { window.parent.showCargar(1); }
        , complete: function () { window.parent.showCargar(0); }
        , success: function (jsondata) { fillFileData(jsondata.d); }
        , error: function (err) { alert(err); }
    });
}

function GetFileDataAsociada(fk_Documento, fk_Campo, fk_Expediente, fk_Folder, id_File, tipoDB) {
    var hdnllaves = $('#hdnllavesFolder').val();
    if (fk_Expediente == "")
        fk_Expediente = 0;
    if (fk_Folder == "")
        fk_Folder = 0;
    if (id_File == "")
        id_File = 0;

    $.ajax({
        dataType: "json"
        , contentType: content
        , type: "POST"
        , url: Pagina + "GetFileDataAsociada"
        , data: "{fk_Documento: '" + fk_Documento + "', fk_Campo: '" + fk_Campo + "', fk_Expediente: '" + fk_Expediente + "', fk_Folder: '" + fk_Folder + "' , id_File: '" + id_File + "', TipoDB: '" + tipoDB + "'}"
        , beforeSend: function () { window.parent.showCargar(1); }
        , complete: function () { window.parent.showCargar(0); }
        , success: function (jsondata) { fillFileDataAsociada(jsondata.d); }
        , error: function (err) { alert(err); }
    });
}

function getHistorialEstados(codigo, fk_log_cargue, fk_Documento, fk_Documento, fk_Expediente, fk_Folder, id_File, TtipoDB) {
    var hdnllaves = $('#hdnllavesFolder').val();
    if (fk_Expediente == "")
        fk_Expediente = 0;
    if (fk_Folder == "")
        fk_Folder = 0;
    if (id_File == "")
        id_File = 0;


    $.ajax({
        contentType: content
        , dataType: "json"
        , type: "POST"
        , url: Pagina + "GetFileData"
        , data: "{codigo:'" + codigo + "', tipo:3, fk_log_Cargue: '" + fk_log_cargue + "', fk_Documento: '" + fk_Documento + "', llaves: '" + hdnllaves + "', fk_Expediente: '" + fk_Expediente + "', fk_Folder: '" + fk_Folder + "' , id_File: '" + id_File + "' , TipoDB: '" + TtipoDB + "'}"
        , beforeSend: function () { window.parent.showCargar(1); }
        , complete: function () { window.parent.showCargar(0); }
        , success: function (jsondata) { fillHistorialEstados(jsondata.d); }
        , error: function (err) { alert(err); }
    });
}

function getGestionSolicitud() {
    $.ajax({
        dataType: "json",
        contentType: content,
        type: "POST",
        url: Pagina + "GetGestionSolicitud",
        data: "{}",
        beforeSend: function () { window.parent.showCargar(1); },
        complete: function () { window.parent.showCargar(0); },
        success: function (resp) { fillSolicitudes(resp.d); },
        error: function (err) { alert(err); }
    });
}

function historiaEFormat() {
    return '<img src="../../_Images/historiaE.png" style="width:20px; heigth:20px;  cursor:help;" />';
}

function historiaSFormat() {
    return '<img src="../../_Images/historiaS.png" style="width:20px; heigth:20px;  cursor:help;"/>';
}

function dataFileFormat() {
    return '<img src="../../_Images/data.png" style="width:20px; heigth:20px;  cursor:help;"/>';
}

function solicitudFormat(cellvalue) {
    if (cellvalue == 1 || cellvalue == "True")
        return '<img src="../../_Images/Buy.png" style="cursor:help;"/>';
    else
        return '';
}

function imageFormat(cellvalue) {
    if (cellvalue == 1 || cellvalue == "True")
        return '<img src="../../_Images/ver_imagen.png" style="width:20px; heigth:20px;  cursor:help;"/>';
    else
        return '';
}

function PdfFormat(cellvalue) {
    if (cellvalue == 1 || cellvalue == "True")
        return '<img src="../../_Images/pdf-file.png" style="width:20px; heigth:20px;  cursor:help;"/>';
    else
        return '';
}

function eliminarsolicitud() {
    return '<img src="../../_Images/Delete.png"  style="cursor:help;"/>';
}

function verImagen(token, tipoDB) {
    var url = $("#ImagingURL").val() + "?token=" + token + "&tipoDB=" + tipoDB;
    var params = ['height=' + screen.height, 'width=' + screen.width, 'fullscreen=yes'].join(',');
    //window.open(url, "visor", "height=650px,width=980px,top=10px");
    var popup = window.open(url, "visor", params);
    popup.moveTo(0, 0);
}

function verPdf(token, tipoDB) {

    var url = $("#PDFURL").val() + "?token=" + token + "&tipoDB=" + tipoDB;
    var params = ['height=' + screen.height, 'width=' + screen.width, 'fullscreen=yes'].join(',');
    //window.open(url, "visor", "height=650px,width=980px,top=10px");
    //    var popup = window.open(url, "VisorPdf", params);
    //    var popup = 
    window.open(url, "_blank");
    //    popup.moveTo(0, 0);

    //    $.ajax({
    //        contentType: content,
    //        dataType: "json",
    //        type: "POST",
    //        url: Pagina + "VerPDF",
    //        data: "{token:'" + token + "'}",
    //        success: function (resp) {
    //            { alert(resp);
    //        },
    //        error: function (err) { alert(err); }
    //    });
}

function AddCodigo(codigo, row, carpeta) {
    $.ajax({
        contentType: content,
        dataType: "json",
        type: "POST",
        url: Pagina + "AddCodigo",
        data: "{codigo:'" + codigo + "'}",
        success: function (resp) {
            if (carpeta)
                $('#tblCarpetas').setCell(row, 'Solicitado', resp.d);
            else if (carpeta == false)
                $('#tblDocumentos').setCell(row, 'Solicitado', resp.d);
        },
        error: function (err) { alert(err); }
    });
}
