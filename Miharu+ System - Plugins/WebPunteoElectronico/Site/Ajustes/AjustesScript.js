var accion = "";
var anterior = "";
var obsGuardar = "";

prm.add_pageLoaded(InitForm);

function InitForm() {
    if (ShowObservacion == "1")
        $("#" + Observacion_TextBoxId).autoList(Observacion_InputData, function () { });
}

function Nueva_Click(e) {
    accion = "Nueva";
    $("#VentanaObservacionCodigo").val('');
    $("#VentanaObservacionDescripcion").val('');
    $("#VentanaObservacion_Div").show();
    setTimeout(function () { $("#CancelarObservacion").focus(); }, 100);
}

function Editar_Click(e) {
    accion = "Editar";
    anterior = $("#" + Observacion_TextBoxId).val();
    var obsparts = anterior.split('|');

    $("#VentanaObservacionCodigo").val(obsparts[0]);
    $("#VentanaObservacionDescripcion").val(anterior.replace(obsparts[0] + '|', ''));
    $("#VentanaObservacion_Div").show();
    setTimeout(function () { $("#CancelarObservacion").focus(); }, 100);
}

function Inactivar_Click(e) {
    accion = "Inactivar";
    anterior = $("#" + Observacion_TextBoxId).val();
    var obsparts = anterior.split('|');

    $("#VentanaObservacionCodigo").val(obsparts[0]);
    $("#VentanaObservacionDescripcion").val(anterior.replace(obsparts[0] + '|', ''));
    $("#VentanaObservacion_Div").show();
    setTimeout(function () { $("#CancelarObservacion").focus(); }, 100);
}

function AceptarObservacion_Click() {
    var cod = $("#VentanaObservacionCodigo").val();
    var obs = $("#VentanaObservacionDescripcion").val();

    if (accion == "Editar") {
        if (!confirm('Presione <Aceptar> si desea guardar los cambios permanentemente, o <cancelar> para guardar los cambios unicamente para el ajuste actual')) {
            //            var indice = BuscarIndice(anterior);
            //            if (indice > -1) {
            var nuevaObs = cod + "|" + obs;
            //                Observacion_InputData[indice] = nuevaObs;
            //                $("#" + Observacion_TextBoxId).autocomplete("option", "source", Observacion_InputData);
            $("#" + Observacion_TextBoxId).val(nuevaObs);
            //            } else {
            //                alert("Error: No se encontro el elemento");
            //            }
            $("#VentanaObservacion_Div").hide();
            return;
        }
    }
    GuardarObservacion(cod, obs);
}

function CancelarObservacion_Click() {
    $("#VentanaObservacion_Div").hide();
}

function BuscarIndice(obs) {
    for (var i = 0; i < Observacion_InputData.length; i++) {
        if (Observacion_InputData[i] == obs) {
            return i;
        }
    }
    return -1;
}

function GuardarObservacion(cod, obs) {
    obsGuardar = obs;
    $.ajax({
        type: "POST",
        url: "AjustesAjaxController.aspx",
        data: "Metodo=GuardarObservacion&Accion=" + accion + "&Cod=" + cod + "&Obs=" + EncodeScript(obs),
        success: function (result) {
            $("#VentanaObservacion_Div").hide();
            var parts = result.split(':');
            if (parts[0] == "Error") {
                alert(parts[1]);
            }
            if (parts[0] == "Cod") {
                var nuevaObs = parts[1] + "|" + obsGuardar;
                if (accion == "Nueva") {
                    Observacion_InputData[Observacion_InputData.length] = nuevaObs;
                    $("#" + Observacion_TextBoxId).autocomplete("option", "source", Observacion_InputData);
                    $("#" + Observacion_TextBoxId).val(nuevaObs);
                }
                else {
                    var indice = BuscarIndice(anterior);
                    if (indice > -1) {
                        if (accion == "Editar") {
                            Observacion_InputData[indice] = nuevaObs;
                            $("#" + Observacion_TextBoxId).autocomplete("option", "source", Observacion_InputData);
                            $("#" + Observacion_TextBoxId).val(nuevaObs);
                        } else {
                            Observacion_InputData.splice(indice, 1);
                            $("#" + Observacion_TextBoxId).autocomplete("option", "source", Observacion_InputData);
                            $("#" + Observacion_TextBoxId).val("");
                        }
                    }
                }
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            try {
                $("#VentanaObservacion_Div").hide();
                alert("Error: " + XMLHttpRequest + ", " + textStatus + ", " + errorThrown);

            } catch (e) { }
        }
    });
}