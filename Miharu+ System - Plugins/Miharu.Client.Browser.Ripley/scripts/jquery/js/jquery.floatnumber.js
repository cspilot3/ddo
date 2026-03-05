/*
* 2012-07-05
* Eliseo Roa
*/
var separator = '.';

function roundnumber(number, decimals) {
    var original = parseFloat(number);
    var fixval = original.toFixed(decimals);
    var result = parseFloat(fixval);
    return result;
}

$.fn.numeric = function () {
    return this.each(function () {
        $(this).keydown(function (e) {
            if ((e.shiftKey && e.keyCode != 9) || e.altKey || e.ctrlKey) return false;
            var k = e.charCode || e.keyCode || 0;
            if (k == 110 || k == 190) return ($(e.target).val().indexOf('.') < 0);
            return (
                    k == 8 || k == 9 || k == 46 ||
                    (k >= 37 && k <= 40) ||
                    (k >= 48 && k <= 57) ||
                    (k >= 96 && k <= 105));
        });
    });
};

$.fn.formatToFloat = function (precision) {
    return this.each(function () {
        var input = $(this);

        var re = new RegExp(",", "g");

        var s = null;
        if (input[0].type != undefined) s = input.val();
        else s = input.html();

        s = s.replace(re, ".");
        if (s == "") s = "0";

        if (!isNaN(s)) {
            n = parseFloat(s);
            s = n.toFixed(precision);
            re2 = new RegExp("\\.", "g");
            s = s.replace(re2, separator);


            if (input[0].type != undefined) input.val(s);
            else input.html(s);
        }
    });
};

//TODO: Reemplazar por  formatToMiles
$.fn.convertToCurrencyDisplayFormat = function (sim) {
    return this.each(function (i, ctr) {
        var input = $(ctr);
        var str = null;
        if (input[0].type != undefined) str = input.val();
        else str = input.html();

        str = ReplaceAll(str, "$", "")
        var c = ',', p = '.';
        var s = str.toString().split(p);
        var n = s[0];
        var d = s.length > 1 ? p + (s[1] == '' ? '00' : s[1]) : '.00';
        var r = /(\d+)(\d{3})/;
        while (r.test(n)) n = n.replace(r, '$1' + c + '$2');

        str = sim + (n.length == 0 ? '0' : n) + d;
        if (input[0].type != undefined) input.val(str);
        else input.html(str);

        ctr.IsCurrencyDisplayFormat = true;
    });
};

$.fn.formatToCurrency = function (p, s) {
    return this.each(function () {
        $(this).formatToFloat(p).convertToCurrencyDisplayFormat(s);
        var a = 0;
        //event.preventDefault
    });
};

$.fn.dec = function (decimals) {
    var s = 0;
    this.each(function () {
        var input = $(this);

        var v = null;
        if (input[0].type != undefined) v = input.val();
        else v = input.html();

        if (v.replace(" ", "") == "") v = "0";
        s = v.replace(/[^0-9.-]/g, '')
        s = parseFloat(s);

        if (s.toString() == "NaN") throw "El valor del campo " + input[0].id + " no es correcto";

        if (decimals != undefined && decimals != null)
            s = roundnumber(s, decimals);
    });
    return s;
};

$.fn.removezeros = function () {
    return this.each(function () {
        var input = $(this);

        var s = null;
        if (input[0].type != undefined) s = input.val();
        else s = input.html();

        if (r__a(s.replace(".", "").replace(",", ""), "0", "") == "") s = "";

        if (input[0].type != undefined) input.val(s);
        else input.html(s);
    });
};

$.fn.floatnumber = function (precision, fnChange, isempty) {
    return this.each(function () {
        var input = $(this);
        var valid = false;
        input.bind("blur", function (e) {
            input.formatToFloat(precision);
            if (isempty == true) if (input.dec() == 0) input.val('');
            if (fnChange != null && fnChange != undefined) fnChange(e);
        });
    });
};

$.fn.number = function (n1, fnChange,isempty) {
    return this.each(function () {
        $(this).numeric('.').floatnumber(n1, fnChange,isempty);
    });
};

$.fn.stripNonNumeric = function () {
    return this.each(function (i, c) {
        var str = c.value;
        str += '';
        str = str.replace(/[^0-9.-]/g, '');
        c.value = str;
    });
};

$.fn.numbercurrency = function (p, s, fnChange) {
    if (s == undefined) s = '';

    this.blur(function (e) {
        $(this).formatToCurrency(p, s);
        if (fnChange != null && fnChange != undefined) fnChange(e);
    });

    this.focus(function () {
        var j = $(this);
        if (j[0].type != undefined) {
            j.stripNonNumeric();
            var l = this.value.length;
            var r = j.get(0).createTextRange();
            r.moveStart("character", l);
            r.moveEnd("character", l);
            r.select();
        }
    });

    this.keyup(function () {
        this.value.replace(/[^0-9.-]/g, '');
    });

    this.keydown(function (e) {
        if ((e.shiftKey && e.keyCode != 9) || e.altKey || e.ctrlKey) return false;
        var k = e.charCode || e.keyCode || 0;
        if (k == 110 || k == 190) return ($(e.target).val().indexOf('.') < 0);
        return (
                    k == 8 || k == 9 || k == 46 ||
                    (k >= 37 && k <= 40) ||
                    (k >= 48 && k <= 57) ||
                    (k >= 96 && k <= 105));
    });

    return this.each(function () {
        $(this).formatToCurrency(p, s);
    });
};

function r__a(t, b, r) { while (t.indexOf(b) != -1) t = t.replace(b, r); return t; };


//function valCurreny(str, p, sim) {

//    var c = ',', p = '.';
//    var s = str.toString().split(p);
//    var n = s[0];
//    var d = s.length > 1 ? p + (s[1] == '' ? '00' : s[1]) : '.00';
//    var r = /(\d+)(\d{3})/;
//    while (r.test(n)) n = n.replace(r, '$1' + c + '$2');

//    str = sim + (n.length == 0 ? '0' : n) + d;

//    return str;
//}
