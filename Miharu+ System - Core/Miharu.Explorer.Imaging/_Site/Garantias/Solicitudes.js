var Pagina = "Solicitudes.aspx/";
var content = "Application/json; charset=utf-8";
var CarpetasSolicitudTodos = 0;
var DocumentosSolicitudTodos = 0;
var Llaves;

/*$(function () {
    $("#tabs").tabs({ height: '550px' });
    $("#ddlEntidad").change(function () { GetProyecto(); });
    GetProyecto();
});

function GetProyecto() {
    var idEntidad = $("#ddlEntidad").val();
   
    $.ajax({
        dataType: "json"
        , contentType: content
        , type: "POST"
        , url: Pagina + "GetProyecto"
        , data: "{idEntidad: '" + idEntidad + "'}"
        , error: function () { jAlert("Error obteniendo proyectos"); }
        , success: function (resp) { $("#ddlProyecto").html(resp.d); }
    });
}*/

function fillFolder(data) {
    if (data != "La sesion ha caducado") {
        eval('data = ' + data);
        $("#tblCarpetas").jqGrid("clearGridData", true);
        $("#tblCarpetas").jqGrid({
            datatype: "local",
            colNames: ['S', 'H', 'Expediente', 'Entidad', 'Proyecto', 'Esquema', 'Codigo', 'Estado', 'Estado', 'Data 1', 'Data 2', 'Data 3', 'id_Entidad', 'FilesCustodia', 'llaves', 'Tipo'],
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
                    { name: 'llaves', index: 'llaves', hidden: true },
                    { name: 'Tipo', index: 'Tipo'}],
            autowidth: true,
            rowNum: 10,
            rowList: [10, 20, 30],
            onCellSelect: function (rowid, index) {
                var codigo = $('#tblCarpetas').getCell(rowid, 'CBarras_Folder');
                var llaves = $('#tblCarpetas').getCell(rowid, 'llaves');
                var tipo = $('#tblCarpetas').getCell(rowid, 'Tipo');
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
                        GetHistorialSolicitud(codigo);
                        break;
                    default:
                        getFile(codigo, llaves, tipo);

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

function Upload() {
    var parametros = new Array();
    parametros[0] = "idEntidad:'" + $('#ddlEntidad').val() + "'";
    parametros[1] = "idProyecto:'" + $('#ddlProyecto').val() + "'";
    //parametros[2] = "idFile:'" + $('#FileUpload1').val() + "'";
    
    $.ajax({
        dataType: "json"
        , contentType: content
        , type: "POST"
        , url: Pagina + "Upload"
        , data: "{" + parametros.join(",") + "}"
        , beforeSend: function () { window.parent.showCargar(1); }
        , complete: function () { window.parent.showCargar(0); }
        , success: function (jsondata) { fillFolder(jsondata.d); }
        //, error: function () { jAlert("Error buscando data."); }
        , error: function (err) { jAlert(err); }
    });
}