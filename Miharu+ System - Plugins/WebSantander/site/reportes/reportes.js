Frm = {
    CurrentPage: "reportes.aspx",

    Init: function () {
        Frm.fechaProcesoInicialInput.datepicker().mask("9999/99/99");
        Frm.fechaProcesoFinalInput.datepicker().mask("9999/99/99");
        Global.ShowOptions('E', 'D');
        Options.find.click(Frm.Buscar);
    },

    Buscar: function () {
        try {
            var request = Frm.leerGET();
            var report = request["rpt"];
            document.getElementById("reporteInput").value = report;
            var param = Frm.fechaProcesoInicialInput.ValidPar("Fecha proceso inicial", false);
            param += "&" + Frm.fechaProcesoFinalInput.ValidPar("Fecha proceso final", false);
            param += "&" + Frm.reporteInput.ValidPar("Reporte", false);
            param += "&" + ArrayParams($("input", "#CamposContainer"));
            AjaxRequest("Buscar", "", param, function (html) {
                TryExec(html);
                var url = "web_report_viewer.aspx" + "?p=" + (Site.PagNum++);
                $("#IFrameReporte").attr("src", url);
            });
        } catch (e) {
            alert(e);
        }
    },

    leerGET: function () {
        var cadGET = location.search.substr(1, location.search.length);
        var arrGET = cadGET.split("&");
        var asocGET = new Array();
        var variable = "";
        var valor = "";
        for (var i = 0; i < arrGET.length; i++) {
            var aux = arrGET[i].split("=");
            variable = aux[0];
            valor = aux[1];
            asocGET[variable] = valor;
        }

        return asocGET;
    }

};