Frm = {
    CurrentPage: "consultas.aspx",
    ProyectosList: [],
    EntidadSolicitanteList: [],
    EstadoExpedienteList: ["POSITIVO", "NEGATIVO", "MULTIPLE"],
    Validaciones: [],
    UrlVisorImagen: "",

    Init: function () {
        var InitMainGrid = function () {
            if (Grids.MainGrid_flex) Frm.MainGrid = Grids.MainGrid_flex[0].grid;
            else setTimeout(InitMainGrid, 100);
        };

        setTimeout(InitMainGrid, 100);

        Frm.fechaRecibidoInicialInput.datepicker().mask("9999/99/99");
        Frm.fechaRecibidoFinalInput.datepicker().mask("9999/99/99");
        //        Frm.fechaInicialInput.val($.datepicker.formatDate('yy/mm/dd', new Date()));
        Frm.fechaProcesoInicialInput.datepicker().mask("9999/99/99");
        Frm.fechaProcesoFinalInput.datepicker().mask("9999/99/99");
        //        Frm.fechaFinalInput.val($.datepicker.formatDate('yy/mm/dd', new Date()));
        //        Frm.proyectoInput.autoList(Frm.ProyectosList, Frm.CargarDocumentosPorProyecto);
        //        Frm.puntoInput.autoList(Frm.PuntosList);
        //        Frm.documentoInput.autoList(Frm.DocumentosList, Frm.CargarCamposPorTipo);
        document.getElementById("EstadoClienteInput").disabled = true;
        document.getElementById("EntidadInput").disabled = true;
        Frm.ProyectoInput.autoList(Frm.ProyectosList, Frm.HabilitarCampos);
        Frm.EntidadInput.autoList(Frm.EntidadSolicitanteList);
        Frm.EstadoClienteInput.autoList(Frm.EstadoExpedienteList);

        Global.ShowOptions('E', 'D');
        Options.find.click(Frm.Buscar);
    },

    HabilitarCampos: function () {
        try {
            var param = Frm.ProyectoInput.ValidPar("Proyecto");
            //AjaxRequest("HabilitarCampos", "", param, function (html) {
              //  TryExec(html);
                if (param == "ProyectoInput=Validacion de Listas") {
                    document.getElementById("EstadoClienteInput").disabled = false;
                    document.getElementById("EntidadInput").disabled = true;
                }
                else {
                    document.getElementById("EstadoClienteInput").disabled = true;
                    document.getElementById("EntidadInput").disabled = false;
                }
            //});
        }
        catch (e) {
            alert(e);
        }
    },

    MainGridFormat: function flexigridCellFormat(colName, idx, content, row) {
        /*if (colName == "URLImagen") {
        return "<img class='click' alt='imagen' src='../../images/basic/visualizar.png' onclick='window.open(\"" + Frm.UrlVisorImagen + "?Token=" + content + "\", \"Detalle\", \"width=500px,height=500px,resizable=no\");' />";
        //return "<span class='link' onclick='window.open(\"" + Frm.UrlVisorImagen + "?Token=" + content + "\", \"Detalle\", \"width=1000px,height=600px,resizable=yes\")'>Imagen</span>";
        }
        else */
        if (colName == "LinkData") {
            return "<img class='click' alt='imagen' src='../../images/basic/visualizar.png' onclick='window.open(\"VerTxLog.aspx?prc=1&tx=" + content + " \", \"Detalle\", \"width=900px,height=300px,resizable=no\");' />";
            //return "<span class='link' onclick='window.open(\"VerTxLog.aspx?tx=" + content + "\", \"Detalle\", \"width=1000px,height=600px,resizable=yes\")'>Datos</span>";
        } /*
        else if (colName == "Historial") {
            return "<img class='click' alt='imagen' src='../../images/basic/visualizar.png' onclick='window.open(\"VerTxLog.aspx?prc=2&tx=" + content + "\", \"Detalle\", \"width=500px,height=500px,resizable=no\");' />";
        }*/
        else {

            return content;
        }

    },

    Buscar: function () {
        try {

            var param = Frm.fechaRecibidoInicialInput.ValidPar("Fecha recibo inicial", false);
            param += "&" + Frm.fechaRecibidoFinalInput.ValidPar("Fecha recibo final", false);
            param += "&" + Frm.fechaProcesoInicialInput.ValidPar("Fecha proceso inicial", false);
            param += "&" + Frm.fechaProcesoFinalInput.ValidPar("Fecha proceso final", false);
            param += "&" + Frm.ProyectoInput.ValidPar("Proyecto", false);
            param += "&" + Frm.EstadoClienteInput.ValidPar("Estado", false);
            param += "&" + Frm.IdentificacionOficioInput.ValidPar("Id Expediente", false);
            param += "&" + Frm.EntidadInput.ValidPar("Entidad", false);
            param += "&" + Frm.CedulaNitInput.ValidPar("Id", false);
            param += "&" + ArrayParams($("input", "#CamposContainer"));
            AjaxRequest("Buscar", "", param, function (html) {
                TryExec(html);
                $('#MainGrid_flex').flexReload();
            });
        } catch (e) {
            alert(e);
        }
    }

};

function flexigridCellFormat(colName, idx, content, row, grid) {
    return Frm.MainGridFormat(colName, idx, content, row);
}