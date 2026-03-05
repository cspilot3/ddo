var validationlist = new Array();
validationlist[validationlist.length] = 'correo';
validationlist[validationlist.length] = 'numero';
validationlist[validationlist.length] = 'maxLentgh';
validationlist[validationlist.length] = 'minLentgh';
validationlist[validationlist.length] = 'required';
validationlist[validationlist.length] = 'date';

var validationmessage = new Array();
validationmessage[validationmessage.length] = 'El campo {0} no es un correo valido.';
validationmessage[validationmessage.length] = 'El campo {0} debe ser numerico.';
validationmessage[validationmessage.length] = 'El campo {0} debe tener una longitud maxima de {1}.';
validationmessage[validationmessage.length] = 'El campo {0} debe tener una longitud minima de {1}.';
validationmessage[validationmessage.length] = 'El campo {0} es obligatorio.';
validationmessage[validationmessage.length] = 'El campo {0} debe ser de tipo fecha [yyyy-mm-dd].';


jQuery.fn.validar = function () {
    var count = 0;
    var respuestas = new Array();

    while (count < $(this).length) {
        var control = $($(this)[count]);
        var clases = control.attr("class");
        var validaciones = clases.split(" ");

        var countval = 0;
        while (countval < validaciones.length) {
            var val = validaciones[countval].split('(')[0];
            var par = '';
            try { par = validaciones[countval].split('(')[1].split(')')[0]; } catch (a) { }

            if (validationlist.indexOf(val) >= 0) {
                eval('var resp = $("#' + control.attr("id") + '").' + val + '(' + par + ');');

                if (resp == false) {
                    respuestas[respuestas.length] = getValidation(val, control, par);
                }
            }
            countval = countval + 1;
        }
        count = count + 1;
    }

    if (respuestas.length > 0) {
        jAlert(respuestas.join("\n"), "Validaciones");
        return false;
    }
    else {
        return true;
    }
};

function getValidation(validation, control, longitud) {

    var mess = validationmessage[validationlist.indexOf(validation)];

    var nombrecontrol = control.attr("title");
    if (nombrecontrol == "" || nombrecontrol == undefined) {
        nombrecontrol = control.attr("id");
    }

    mess = mess.replace("{0}", nombrecontrol);

    if (longitud != undefined) {
        mess = mess.replace("{1}", longitud);
    }

    return mess;
}


jQuery.fn.correo = function () {
    if (/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test($(this).val())) {
        return true;
    }
    else {
        return false;
    }
};

jQuery.fn.numero = function () {
    if (isNaN($(this).val())) {
        return false;
    }
    else {
        return true;
    }
};

jQuery.fn.maxLentgh = function (value) {
    if ($(this).val().length <= value) {
        return true;
    }
    else {
        return false;
    }
};

jQuery.fn.minLentgh = function (value) {
    if ($(this).val().length >= value) {
        return true;
    }
    else {
        return false;
    }
};

jQuery.fn.required = function () {
    if ($(this).val().length == 0) {
        return false;
    }
    else {
        return true;
    }
};

jQuery.fn.date = function() {
    try {
        var Fecha = $(this).val();
        if (Fecha.length != 0) {
            var RealFecha = new Date();
            var Dia = new String(Fecha.substring(Fecha.lastIndexOf("-") + 1, Fecha.length));
            var Mes = new String(Fecha.substring(Fecha.indexOf("-") + 1, Fecha.lastIndexOf("-")));
            var Ano = new String(Fecha.substring(0, Fecha.indexOf("-")));

            if (isNaN(Ano) || Ano.length < 4 || parseFloat(Ano) < 1900) {
                return false;
            }

            if (isNaN(Mes) || parseFloat(Mes) < 1 || parseFloat(Mes) > 12) {
                return false;
            }

            if (isNaN(Dia) || parseInt(Dia, 10) < 1 || parseInt(Dia, 10) > 31) {
                return false;
            }

            if (Mes == 4 || Mes == 6 || Mes == 9 || Mes == 11 || Mes == 2) {
                if (Mes == 2 && Dia > 28 || Dia > 30) {
                    return false;
                }
            }
        }

        return true;
    }
    catch (a) {
        return false;
    }
}