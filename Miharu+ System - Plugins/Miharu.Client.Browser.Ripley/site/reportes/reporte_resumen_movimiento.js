Frm = {
    CurrentPage: "reporte_resumen_movimiento.aspx",
    DocumentosList: [],
    PuntosList: [],
    ProyectosList: [],

    Init: function () {

        // Redimensionar el IFrame
        $(window).resize(function () { Frm.ResizeFrame(); });
        Frm.ResizeFrame();

        Global.ShowOptions('D');


        Options.report.click(Frm.CargarReporte);
        Frm.fechaInicialInput.datepicker().mask("9999/99/99");
        //        Frm.fechaInicialInput.val($.datepicker.formatDate('yy/mm/dd', new Date()));
        Frm.fechaFinalInput.datepicker().mask("9999/99/99");
        //        Frm.fechaFinalInput.val($.datepicker.formatDate('yy/mm/dd', new Date()));
        Frm.proyectoInput.autoList(Frm.ProyectosList, Frm.CargarDocumentosPorProyecto);
        Frm.puntoInput.autoList(Frm.PuntosList);
        Frm.documentoInput.autoList(Frm.DocumentosList, Frm.CargarCamposPorTipo);
    },

    
    CargarDocumentosPorProyecto: function () {
        try {
            var param = Frm.proyectoInput.ValidPar("Proyecto");
            AjaxRequest("CargarDocumentosPorProyecto", "", param, function (html) {
                TryExec(html);
                SetAutoListDataSource(Frm.documentoInput, Frm.DocumentosList);
            });

        } catch (e) {
            alert(e);
        }
    },

     ResizeFrame: function () {
                var Alto = ($(window).height());

                $("#IFrameReporte").css("height", (Alto - 95) + "px");
            },

    CargarReporte: function () {
        try {
            var param = Frm.fechaInicialInput.ValidPar("Fecha inicial", false);
            param += "&" + Frm.fechaFinalInput.ValidPar("Fecha final", false);
            param += "&" + Frm.proyectoInput.ValidPar("Proyecto");
            param += "&" + Frm.puntoInput.ValidPar("Punto");
            param += "&" + Frm.documentoInput.ValidPar("Tipo de documento");
            AjaxRequest("CargarReporte", "", param, function (html) {
                TryExec(html);
                var url = "../informes/web_report_viewer.aspx" + "?p=" + (Site.PagNum++);
                //window.open(url);
                $("#IFrameReporte").attr("src", url);
            });
        } catch (e) {
            alert(e);
        }
    }
    
}