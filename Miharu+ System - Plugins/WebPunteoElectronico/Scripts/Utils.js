var NullString = "__NULL";
function Get(i) { return document.getElementById(i); }
function TryExec(s) {
    try { if (s != "") { eval(s); } } catch (e) {
        alert("ERROR: " + e + ", SCRIPT: " + s);
    } 
}
function SetClassName(c, css) { if (c != null && c != undefined) c.className = css; }
function SetVisible(c, v) { c.style.display = (v) ? "" : "none"; }
function GetVisible(c) { return (c.style.display == ""); }
function GetParent(ctr) { try { var parent = ctr.parentElement; if (parent == null || parent == undefined) parent = ctr.offsetParent; if (parent == null || parent == undefined) parent = ctr.parentNode; return parent; } catch (e) { return null; } }
function IsVisible(ctr) { if (ctr == null || ctr == undefined) return false; if (ctr.style != undefined && ctr.style.display == "none") { return false; } var parent = GetParent(ctr); if (parent == null || parent == undefined) return true; return IsVisible(parent); }
function IsEditable(ctr) { if (ctr == null || ctr == undefined) return false; return !ctr.disabled; }
function ReplaceAll(t, b, r) { while (t.indexOf(b) != -1) t = t.replace(b, r); return t; }
function EncodeScript(ns) { var e = ns; e = ReplaceAll(e, '*', "*_"); e = ReplaceAll(e, '"', "*22_"); e = ReplaceAll(e, "'", "*27_"); e = ReplaceAll(e, "\\", "*5C_"); e = ReplaceAll(e, "`", "*60_"); e = ReplaceAll(e, ":", "*3A_"); e = ReplaceAll(e, ";", "*3B_"); e = ReplaceAll(e, ",", "*3C_"); e = ReplaceAll(e, "|", "*5X_"); e = ReplaceAll(e, "!", "*6X_"); e = ReplaceAll(e, "\n", "*7C_"); e = ReplaceAll(e, "\r", "*8C_"); return e; }
function DecodeScript(ns) { var d = ns; d = ReplaceAll(d, "*22_", '"'); d = ReplaceAll(d, "*27_", "'"); d = ReplaceAll(d, "*5C_", "\\"); d = ReplaceAll(d, "*60_", "`"); d = ReplaceAll(d, "*3A_", ":"); d = ReplaceAll(d, "*3B_", ";"); d = ReplaceAll(d, "*3C_", ","); d = ReplaceAll(d, "*5X_", "|"); d = ReplaceAll(d, "*6X_", "!"); d = ReplaceAll(d, "*7C_", "\n"); d = ReplaceAll(d, "*8C_", "\r"); d = ReplaceAll(d, "*_", '*'); return d; }
function SubStringCount(t, b, r) { var index = t.indexOf(b); var count = 0; while (index != -1 && index < t.length) { count++; index = t.indexOf(b, index + b.length); } return count; }

function IndexOfArray(a, it) { for (var i = 0; i < a.length; i++) if (a[i] == it) return i; return -1; }
function IndexOfStringArray(a, it) { for (var i = 0; i < a.length; i++) if (a[i].toLowerCase() == it.toLowerCase()) return i; return -1; }
function isNull(v) { return (v == null || v == undefined); }

Array.prototype.find = function (value) { for (var i = 0, loopCnt = this.length; i < loopCnt; i++) { if (this[i].toLowerCase() === value.toLowerCase()) { return this[i]; } } return NullString; };

function CurrencyFormat(v) {
    var c = ',', p = '.';
    var s = v.toString().split(p);
    var n = s[0];
    var d = s.length > 1 ? p + s[1] : '';
    var r = /(\d+)(\d{3})/;
    while (r.test(n)) n = n.replace(r, '$1' + c + '$2');
    return n + d;
}
