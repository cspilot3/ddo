

prm.add_pageLoaded(InitForm);

function InitForm() {
    var Data = [];
    $("#txtEsquemas").autoList(Esquemas_InputData, function () { BuildTipologias(); });
    $("#txtTipologias").autoList(Data, function () { });
    
}


function BuildTipologias() {

    var esquema = $("#txtEsquemas").val();
    $.ajax({
        type: "POST",
        url: "R011_Soportes_Sobrante_Multiple.aspx",
        data: "Metodo=BuildTipologias&Esquema=" + esquema + "&Cod=" + "",
        success: function (result) {
            var parts = result.split(':');
            if (parts[0] == "Error") {
                alert(parts[1]);
            }

            if (parts[0] == "Cod") {
                $("#txtTipologias").val("");
                parts[1] = parts[1].replace("['", "");
                parts[1] = parts[1].replace("']", "");
                parts[1] = parts[1].replace(/','/g, ",")
                Tipologia_InputData = parts[1].split(",");
                $("#txtTipologias").autoList(Tipologia_InputData, function () { });


            }


        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            try {

                alert("Error: " + XMLHttpRequest + ", " + textStatus + ", " + errorThrown);

            } catch (e) { }
        }
    });

}



function SaveTipologia(mensaje) {
    if (confirm(mensaje)) {
        var esquema = $("#txtEsquemas").val();
        var tipologia = $("#txtTipologias").val();

        $.ajax({
            type: "POST",
            url: "R011_Soportes_Sobrante_Multiple.aspx",
            data: "Metodo=GuardarTipologias&Esquema=" + esquema + "&Tipologias=" + tipologia,
            success: function (result) {
                var parts = result.split(':');
                if (parts[0] == "Message") {
                    alert(parts[1]);
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                try {

                    alert("Error: " + XMLHttpRequest + ", " + textStatus + ", " + errorThrown);

                } catch (e) { }
            }
        });

    }
}