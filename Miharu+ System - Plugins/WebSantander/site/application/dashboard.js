
Frm = {
    CurrentPage: "dashboard.aspx",
    ServerMode: "",

    Init: function () {

    },

    DisplayTableMenu: function () {
        var request = Frm.leerGET();
        var tableMenu = request["TableMenu"];

        switch (tableMenu) 
        {
            case "Reportes":
                Frm.ShowDashBoardMenu('#Dashboard-main', '#Dashboard-reportes');
                break;
            case "VL":
                Frm.ShowDashBoardMenu('#Dashboard-main', '#Dashboard-reportes');
                Frm.ShowDashBoardMenu('#Dashboard-reportes', '#Dashboard-validacionlistas');
                break;
            case "E":
                Frm.ShowDashBoardMenu('#Dashboard-main', '#Dashboard-reportes');
                Frm.ShowDashBoardMenu('#Dashboard-reportes', '#Dashboard-embargos');
                break;
            case "D":
                Frm.ShowDashBoardMenu('#Dashboard-main', '#Dashboard-reportes');
                Frm.ShowDashBoardMenu('#Dashboard-reportes', '#Dashboard-desembargos');
                break;
            default:
                break;
        }
    },

    ShowDashBoardMenu: function (aOcultar, aMostrar) {
        $(aOcultar).css("display", "none");
        $(aMostrar).css("display", "");
    },

    leerGET: function () 
    {
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
}