var NullString = "__NULL";
Array.prototype.find = function (value) { for (var i = 0, loopCnt = this.length; i < loopCnt; i++) { if (this[i].toLowerCase() === value.toLowerCase()) { return this[i]; } } return NullString; };

function Get(i) { return document.getElementById(i); }
function GetAtt(c, a) { for (var i = 0; i < c.attributes.length; i++) if (c.attributes[i].name == a) return c.attributes[i].value; return ""; }
function GetNode(c, t) {
    if (c == null)
        return null;
    if (c.tagName.toUpperCase() == t.toUpperCase()) { return c; } else { var p = GetParent(c); if (p != null) return GetNode(p, t); }

    return null;
}
function TryExec(s) {
    try { if (s != "") { eval(s); } } catch (e) { alert("ERROR: " + e + ", SCRIPT: " + s); }
}
// ReSharper disable ExpressionIsAlwaysConst
// ReSharper disable HeuristicallyUnreachableCode
function SetClassName(c, css) { if (c != null && c != undefined) c.className = css; }
function SetVisible(c, v) { c.style.display = (v) ? "" : "none"; }
function GetVisible(c) { return (c.style.display == ""); }
function GetParent(ctr) { try { var parent = ctr.parentElement; if (parent == null || parent == undefined) parent = ctr.offsetParent; if (parent == null || parent == undefined) parent = ctr.parentNode; return parent; } catch (e) { return null; } }
function IsVisible(ctr) { if (ctr == null || ctr == undefined) return false; if (ctr.style != undefined && ctr.style.display == "none") { return false; } var parent = GetParent(ctr); if (parent == null || parent == undefined) return true; return IsVisible(parent); }
function IsEditable(ctr) { if (ctr == null || ctr == undefined) return false; return !ctr.disabled; }
function IsNull(v) { return (v == null || v == undefined); }
// ReSharper restore HeuristicallyUnreachableCode
// ReSharper restore ExpressionIsAlwaysConst

function ReplaceAll(t, b, r) { while (t.indexOf(b) != -1) t = t.replace(b, r); return t; }
function EncodeScript(ns) { var e = ns; e = ReplaceAll(e, '*', "*_"); e = ReplaceAll(e, '"', "*22_"); e = ReplaceAll(e, "'", "*27_"); e = ReplaceAll(e, "\\", "*5C_"); e = ReplaceAll(e, "`", "*60_"); e = ReplaceAll(e, ":", "*3A_"); e = ReplaceAll(e, ";", "*3B_"); e = ReplaceAll(e, ",", "*3C_"); e = ReplaceAll(e, "|", "*5X_"); e = ReplaceAll(e, "!", "*6X_"); e = ReplaceAll(e, "\n", "*7C_"); e = ReplaceAll(e, "\r", "*8C_"); return e; }
function DecodeScript(ns) { var d = ns; d = ReplaceAll(d, "*22_", '"'); d = ReplaceAll(d, "*27_", "'"); d = ReplaceAll(d, "*5C_", "\\"); d = ReplaceAll(d, "*60_", "`"); d = ReplaceAll(d, "*3A_", ":"); d = ReplaceAll(d, "*3B_", ";"); d = ReplaceAll(d, "*3C_", ","); d = ReplaceAll(d, "*5X_", "|"); d = ReplaceAll(d, "*6X_", "!"); d = ReplaceAll(d, "*7C_", "\n"); d = ReplaceAll(d, "*8C_", "\r"); d = ReplaceAll(d, "*_", '*'); return d; }
function SubStringCount(t, b) { var index = t.indexOf(b); var count = 0; while (index != -1 && index < t.length) { count++; index = t.indexOf(b, index + b.length); } return count; }

function IndexOfArray(a, it) { for (var i = 0; i < a.length; i++) if (a[i] == it) return i; return -1; }
function IndexOfStringArray(a, it) { for (var i = 0; i < a.length; i++) if (a[i].toLowerCase() == it.toLowerCase()) return i; return -1; }
function IsEmail(id) { var jq = $("#" + id); var val = jq.val(); var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/; if (val.length > 0 && reg.test(val) == false) { throw "El correo electronico no es valido"; } }

function AjaxRequest(m, c, p, fn, errfn, controller, ito) { AjaxRequestUrl(m, c, p, fn, errfn, Frm.CurrentPage, controller, ito); }
function AjaxRequestUrl(m, c, p, fn, errfn, u, controller, ito) {

    AjaxLock.Lock();
    
    if (controller == undefined) controller = "Page";
    if (ito == undefined) ito = false;
    if (!ito) Site.SessionActivityCounter = 0;
    
    var dt = "RequestType=Ajax&AjaxMethod=" + m + "&ControlCache=" + c + ((p != "") ? "&" + p : "") + "&Controller=" + controller;
    $.ajax({
        type: "POST", url: u, data: dt,
        success: function (tx, est, res) {
            AjaxLock.UnLock();
            var html = res.responseText;

            if (html.indexOf("ERROR") == 0) { alert(html); if (errfn != undefined) errfn(html); return; }
            if (fn != undefined) fn(html);
        },
        error: function (a, b, err) {
            AjaxLock.UnLock();

            alert(err); if (errfn != undefined) errfn(err);
        }
    });
}

function GetQueryStringParameter(name) {
    name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regexS = "[\\?&]" + name + "=([^&#]*)";
    var regex = new RegExp(regexS);
    var results = regex.exec(window.location.search);
    if (results == null)
        return "";
    else
        return decodeURIComponent(results[1].replace(/\+/g, " "));
}

function Par(name, id) {
    if (id == undefined) id = name;
    try {
        var jq = $("#" + id);
        if (jq.length == 0) throw "campo no encontrado " + id;

        if (jq[0].type == "checkbox" || jq[0].type == "radio")
            return name + "=" + jq[0].checked.toString();

        var val = (jq.val() == "" ? (jq.html() == "" ? jq.text() : jq.html()) : jq.val());
        return name + "=" + ((val == null) ? "" : val.toString());
    } catch (e) { throw "Parametro invalido " + name + ", " + e; }
}

function ValidPar(id, label, req, name, nullvalue) {
    if (label == undefined) label = id;
    if (name == undefined) name = id;
    if (req == undefined) req = true;
    if (nullvalue == undefined) nullvalue = "";
    try {
        var jq = $("#" + id);
        if (jq[0].type == "checkbox" || jq[0].type == "radio")
            return name + "=" + jq[0].checked.toString();

        var val = (jq.val() == "" ? (jq.html() == "" ? jq.text() : jq.html()) : jq.val());
        if ((req) && (val == null || val == nullvalue))
            throw "El campo " + (label == "" ? id : label) + " es requerido";
        return name + "=" + ((val == null) ? "" : val.toString());
    } catch (e) { throw "Parametro invalido " + label + ", " + e; }
}

function ArrayParams(a) {
    var params = "";
    if (a.length != undefined && a.length > 0) {
        for (var i = 0; i < a.length; i++) {
            var ctr = $(a[i]);

            if (params != "") params += "&";

            if (ctr[0].type == "checkbox" || ctr[0].type == "radio")
                params += ctr[0].id + "=" + ctr[0].checked.toString();
            else
                params += ctr[0].id + "=" + (ctr.val() == "" ? (ctr.html() == "" ? ctr.text() : ctr.html()) : ctr.val());
        }
    }
    return params;
}


$.fn.ValidPar = function (label, req, name, nullvalue) {
    var s = "";
    this.each(function () {
        if (this.id != undefined && this.id != "") {
            if (s != "") s += "&";
            s += ValidPar(this.id, label, req, name, nullvalue);
        }
    });
    return s;
};

$.fn.disabled = function (v) {
    return this.each(function (i, t) {
        $(t).prop('disabled', v);
        $(t).fadeTo('slow', (v ? .1 : 1));
    });
};


var AjaxLock = {
    MouseX: 0, MouseY: 0, IsLocking: false, ZIndex: 0,
    Move: function (e) { AjaxLock.MouseX = e.pageX; AjaxLock.MouseY = e.pageY; if (AjaxLock.IsLocking) { AjaxLock.Paint(); } },
    Lock: function () { AjaxLock.ZIndex = AjaxLock.GetZIndex() + 10; AjaxLock.IsLocking = true; AjaxLock.Paint(); $('#MouseLock').show(); },
    UnLock: function () { $('#MouseLock').fadeOut(); AjaxLock.IsLocking = false; },
    Paint: function () { $('#MouseLock').css({ left: AjaxLock.MouseX - 25, top: AjaxLock.MouseY - 25, zIndex: AjaxLock.ZIndex }); },

    GetZIndex: function () {
        // ReSharper disable UnusedParameter
        return Math.max.apply(null, $.map($('body > *'), function (e, n) {
            // ReSharper restore UnusedParameter
            if ($(e).css('position') == 'absolute')
                return parseInt($(e).css('z-index')) || 1;

            return 0;
        }));
    }
};
$(document).mousemove(AjaxLock.Move);


// ReSharper disable UnusedParameter
$.fn.columnsIter = function (fn, v1, v2, v3, v4, v5, v6, v7, v8) {
    // ReSharper restore UnusedParameter
    return this.each(function (i, tab) {
        var trs = $("tr", tab);
        trs.each(function (j, tr) {
            var tds = $("td", tr);
            for (var ii = 0; ii < 8; ii++) {
                var v = eval("v" + (ii + 1));
                if (v != undefined && tds.length > ii) fn(tds, v, ii);
            }
        });
    });
};
$.fn.columnsWidth = function (w1, w2, w3, w4, w5, w6, w7, w8) { return this.columnsIter(function (t, w, i) { $(t[i]).width(w); }, w1, w2, w3, w4, w5, w6, w7, w8); };
$.fn.columnsClass = function (c1, c2, c3, c4, c5, c6, c7, c8) { return this.columnsIter(function (t, c, i) { if (c != "") $(t[i]).addClass(c); }, c1, c2, c3, c4, c5, c6, c7, c8); };

$.fn.validateDate = function (l, r) {
    return this.each(function () {
        try {
            var b = function (a) { return ((a % 100 != 0) && ((a % 4 == 0) || (a % 400 == 0))); };
            var toInt = function (v) {
                var l = 0;
                while (l < v.length && v[l] == '0') l++;
                return parseInt(v.substring(l, v.length));
            };
            var input = $(this); if (input.length == 0) throw "Fecha '" + l + "' no encontrado";
            if (l == undefined || l == "") l = input[0].id;

            var f = null; if (input[0].type != undefined) f = input.val().split("/");
            else f = input.html().split("/");

            var a = toInt(f[0]); var m = toInt(f[1]); var d = toInt(f[2]); var nd;
            if (a < 1900 || a > 2100) throw "Fecha '" + l + "' año debe estar entre 1900 y 2100";
            switch (m) {
                case 1: case 3: case 5: case 7: case 8: case 10: case 12: nd = 31; break;
                case 4: case 6: case 9: case 11: nd = 30; break;
                case 2: nd = b(a) ? 29 : 28; break;
                default: throw "Fecha " + l + " mes error";
            }
            if (d > nd || d == 0) throw "Fecha '" + l + "' dia error";
        } catch (e) { if (r) { alert(e); return false; } else throw e; }
    });
};

$.fn.getDate = function () {
    var d = null;
    this.each(function () {
        try {
            var input = $(this); if (input.length == 0) throw "Fecha '" + l + "' no encontrado";

            var f = null; if (input[0].type != undefined) f = input.val().split("/");
            else f = input.html().split("/");

            var a = parseInt(f[0]); var m = parseInt(f[1]); var d = parseInt(f[2]);
            d = new Date(a, m, d);

        } catch (e) { throw "Fecha no válida, " + e; }
    });
    return d;
};
