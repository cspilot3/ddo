$.fn.fillCombo = function (data) {
    eval("data = " + data);
    var cadena = new Array();
    for (var i = 0; i < data.length; i++) {
        cadena[i] = '<option value="' + data[i].v + '">' + data[i].t + '</option>';
    }
    $(this).html(cadena.join(''));
}