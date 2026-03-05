
Frm = {
    CurrentPage: "dashboard.aspx",
    ServerMode: "",

    Init: function () {

    },

    DisplayTableMenu: function () {
        var request = Frm.leerGET();
        var tableMenu = request["TableMenu"];

        switch (tableMenu) {
            case "ServerMode":
                switch (Frm.ServerMode) {
                    case "Estadisticos_Digitalizacion":
                        ShowDashBoardMenu('#Dashboard-main', '#Dashboard-estadisticos-digital');
                        break;

                    default:
                        break;
                }

                break;
            case "Administracion":
                Frm.ShowDashBoardMenu('#Dashboard-main', '#Dashboard-administracion');
                break;
            case "Servicio":
                ShowDashBoardMenu('#Dashboard-main', '#Dashboard-sms');
                break;
            default:
                break;
        }
    },

    ShowDashBoardMenu: function (aOcultar, aMostrar) {
        $(aOcultar).css("display", "none");
        $(aMostrar).css("display", "");
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
}