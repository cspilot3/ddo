ServerEvents = { ControlValueChanged: "ControlValueChanged" }
ServerTypes = { DynCheckBox: "DynCheckBox", DynNumberBox: "DynNumberBox", DynTextBox: "DynTextBox", DynDatePickerBox: "DynDatePickerBox", DynAutoCompleteBox: "DynAutoCompleteBox" }

function DynSection(id, serverID) {
    this.ClientID = id;
    this.ServerID = serverID;

    this.JQ = function () { return $("#" + this.ClientID); };
    this.Control = function () { return Get(this.ClientID); };
    this.SetVisible = function (v) { SetVisible(this.Control(), v); };
    this.GetVisible = function (v) { return GetVisible(this.Control()); };
}

function DynControl(parent, id, serverID, serverType, val, scrChanged, srcEnter, srcLeave, isServerChange, desc) {
    try {
        this.ParentSection = parent;
        this.ClientID = id;
        this.ServerID = serverID;
        this.ClientScriptEnter = srcEnter;
        this.ClientScriptLeave = srcLeave;
        this.ClientScriptChange = scrChanged;
        this.IsServerChange = isServerChange;
        this.ServerType = serverType;
        this.Description = desc;
        this.LastValue = null;
        this.DataSource = [];

        this.JQ = function () { return $("#" + this.ClientID); };
        this.Control = function () { return Get(this.ClientID); };
        this.SetValue = function (v) { Dyn.SetControlValue(this, v); };
        this.GetValue = function () { return Dyn.GetControlValue(this); };
        this.SetVisible = function (v) { SetVisible(this.Control(), v); };
        this.GetVisible = function (v) { return GetVisible(this.Control()); };

        this.SetValue(val);
        this.Control().Tag = this;

        Dyn.AddControlEventHandlers(this);

        Dyn.Controls.push(this);

        this.JQ().addClass("Control-Blur");

    } catch (e) { alert("ERROR: No fue posible crear el control, " + id + e); }
}

Dyn = {
    DynFormClientID: "",
    DynFormID: "",
    DynamicScriptID: "",
    ControlEventArgsID: "",
    ControlEventLinkID: "",
    ControlEventLinkUnique: "",
    DynAutoCompleteBinderPage: "",
    Controls: null,
    CssClass: "",
    LastFocusedControl: null,
    ToolTipItem: { Control: null, Message: "" }
}

Dyn.Initialize = function (nDynFormId) {
    try {
        this.DynFormClientID = nDynFormId;
        this.DynamicScriptID = nDynFormId + "DynScript";


        var strScripts = Get(this.DynamicScriptID).value;
        if (strScripts != "") {
            var scripts = strScripts.split("´");
            for (var i = 0; i < scripts.length; i++) {
                if (scripts[i] != "") {
                    TryExec(scripts[i]);
                }
            }
        }

        this.SetFormCssClass(this.CssClass);

        this.ManageEnterKey();
        this.ManageHightLigths();

        if (this.LastFocusedControl != null) {
            this.LastFocusedControl.JQ().focus();
            this.LastFocusedControl = null;
        }

    } catch (e) { alert("ERROR: No fue posible inicializar el formulario," + e); }
}

Dyn.SetFormCssClass = function (c) {
    try {
        this.CssClass = c;

        var form = Get(this.DynFormClientID);
        SetClassName(form, this.CssClass);

        var sections = Get(this.DynFormClientID + "Sections");
        SetClassName(sections, this.CssClass + "-Sections");

        for (var i = 0; i < sections.children.length; i++) {
            var child = sections.children[i]

            SetClassName(child, this.CssClass + "-Section");

            var head = child.children[0];
            SetClassName(head, this.CssClass + "-Section-Head");

            var content = child.children[1];
            SetClassName(content, this.CssClass + "-Section-Content");

            var tbody = content.children[0].children[0];
            for (var j = 0; j < tbody.children.length; j++) {
                var tr = tbody.children[j];

                SetClassName(tr.children[0], this.CssClass + "-Section-CntLabel");
                SetClassName(tr.children[1], this.CssClass + "-Section-CntControl");
            }
        }
    } catch (e) { alert("ADVERTENCIA: No fue posible establecer el estilo del formulario, " + e); }
}

Dyn.ReconfigureControl = function (d, val) {
    try {
        d.Control().Tag = d;
        d.SetValue(val);
        Dyn.AddControlEventHandlers(d);
        d.JQ().addClass("Control-Blur");
    } catch (e) { alert("ERROR: No fue posible reconfigurar el control, " + Dyn.GetID(d) + ", " + e); }
}

Dyn.GetID = function (d) {
    try {
        return (d != null && d != undefined && d.ServerID != undefined && d.ServerID != null) ? d.ServerID : "";
    } catch (e) { return ""; }
}

Dyn.InitNumeric = function (d, charDec, dec) {
    try {
        var jq = d.JQ();
        jq.numeric(charDec);
        jq.floatnumber(charDec, dec);
    } catch (e) { alert("ERROR: No fue posible crear el controlador numerico, " + Dyn.GetID(d) + ", " + e); }
}

Dyn.InitDate = function (d) {
    try {
        var jq = d.JQ();
        jq.mask("9999/99/99");
        jq.datepicker();
    } catch (e) { alert("ERROR: No fue posible crear el controlador de fecha, " + Dyn.GetID(d) + ", " + e); }
}

Dyn.InitList = function (d, autoLoad, childs) {
    try {
        d.AutoLoad = autoLoad;
        d.Childs = childs;

        var ctr = d.Control();
        var ctrVal = ctr.value;
        var ctrName = ctr.name;

        var parentid = d.ClientID + "_parent";
        var buttonid = d.ClientID + "_div";

        parent = ctr.parentElement;
        if (parent == null || parent == undefined) var parent = ctr.offsetParent;
        if (parent == null || parent == undefined) var parent = ctr.parentNode;

        parent.id = parentid;

        d.JQ().remove();

        var html = "<table cellpadding='0' cellspacing='0'> <tr> <td> <input type='text' id='" + d.ClientID + "' name='" + ctrName + "' value='" + ctrVal + "' /> </td> <td> <div id='" + buttonid + "' class='blist'></div></td></tr></table>";
        $("#" + parentid).append(html);

        d.Control().Tag = d;

        var jq = d.JQ();

        jq.addClass("Control-Blur");
        jq.focus(function () { Dyn.ShowToolTip(this, this.Tag.Description); });
        jq.blur(function () { Dyn.HideToolTip(); });

        jq.autocomplete({
            source: d.DataSource,
            select: function (event, ui) {
                event.target.value = ui.item.label;
                Dyn.EvalControlChange(event.target, event.target.Tag);
            },
            change: function (event, ui) {
                Dyn.EvalControlChange(event.target, event.target.Tag);
            }
        });

        Dyn.AddControlEventHandlers(d);

        jq.click(function (e) {
            var inputId = e.currentTarget.id.replace('_div', '');
            $("#" + inputId).autocomplete("search", "");
            $("#" + inputId).trigger("focus");
        });

        $("#" + buttonid).click(function (e) {
            var inputId = e.currentTarget.id.replace('_div', '');
            $("#" + inputId).autocomplete("search", "");
            $("#" + inputId).trigger("focus");
        });

        if (d.AutoLoad) {
            Dyn.CallDataSource(d);
        }

    } catch (e) { alert("ERROR: No fue posible crear el controlador de lista, " + Dyn.GetID(d) + ", " + e); }
}

Dyn.AddControlEventHandlers = function (d) {
    try {
        var jq = d.JQ();

        jq.focus(function () { Dyn.ShowToolTip(this, this.Tag.Description); });
        jq.blur(function () { Dyn.HideToolTip(); });

        if (d.ClientScriptEnter != null) {
            jq.focus(function () { Dyn.EvalControlEnter(this, this.Tag); });
        }

        if (d.ClientScriptLeave != null || d.ClientScriptChange != null || d.IsServerChange) {
            if (d.ServerType == ServerTypes.DynCheckBox) {
                jq.change(function () { Dyn.EvalControlChange(this, this.Tag); });
            } else if (d.ServerType == ServerTypes.DynAutoCompleteBox) {
                //No aplica, el control realiza las validaciones correspondientes
            } else {
                jq.blur(function () { Dyn.EvalControlChange(this, this.Tag); });
            }
        }
    } catch (e) { alert("ERROR: No fue posible agregar los manejadores de eventos, " + Dyn.GetID(d) + ", " + e); }
}

Dyn.FireServerEvent = function (d, e) {
    try {
        this.LastFocusedControl = d;
        Get(this.ControlEventArgsID).value = d.ServerID + "," + e;
        __doPostBack(this.ControlEventLinkUnique, "");
    } catch (e) { alert("ERROR: No fue posible llamar al evento de servidor, " + Dyn.GetID(d) + ", " + e); }
}

Dyn.SetControlValue = function (d, v) {
    try {
        if (v == undefined)
            v = Dyn.GetDefaultValue(d);

        d.LastValue = v;
        var control = d.Control();

        if (control != null) {
            if (d.ServerType == ServerTypes.DynCheckBox)
                control.checked = v;
            else
                control.value = v;
        }
    } catch (e) { alert("ERROR: No fue posible establecer el valor para el control, " + Dyn.GetID(d) + ", " + e); }
}

Dyn.GetDefaultValue = function (d) {
    if (d.ServerType == ServerTypes.DynCheckBox) return false;
    return "";
}

Dyn.GetControlValue = function (d) {
    var c = d.Control();
    if (c != null) {
        if (d.ServerType == ServerTypes.DynCheckBox)
            return c.checked;
        return c.value;
    }
    return Dyn.GetDefaultValue(d);
}

Dyn.EvalControlEnter = function (control, dynControl) {
    TryExec(dynControl.ClientScriptEnter);
}

Dyn.EvalControlChange = function (control, dynControl) {
    try {
        if (control != null) {
            if (dynControl.ClientScriptLeave != null) {
                TryExec(dynControl.ClientScriptLeave);
            }

            if (dynControl.ServerType == ServerTypes.DynAutoCompleteBox && dynControl.DataSource != null) {
                var listValue = dynControl.DataSource.find(dynControl.GetValue());

                if (listValue == NullString) {
                    dynControl.SetValue(dynControl.LastValue);
                }
                else {
                    control.value = listValue;
                }
            }

            if (dynControl.GetValue() != dynControl.LastValue) {
                dynControl.LastValue = dynControl.GetValue();

                if (dynControl.ClientScriptChange != null) {
                    TryExec(dynControl.ClientScriptChange);
                }

                Dyn.ClearAutocompleteChilds(dynControl, true);

                if (dynControl.IsServerChange) {
                    //                    dynControl.JQ().qtip("destroy");
                    this.FireServerEvent(dynControl, ServerEvents.ControlValueChanged);
                }
            }
        }
    } catch (e) { alert("ERROR: No fue posible ejecutar correctamente al menos uno de los controladores de evento de cambio, " + Dyn.GetID(dynControl) + ", " + e); }
}

Dyn.ClearAutocompleteChilds = function (dynControl, fillDataSource) {
    if (dynControl.ServerType == ServerTypes.DynAutoCompleteBox && dynControl.Childs.length > 0) {
        for (var j = 0; j < dynControl.Childs.length; j++) {
            var child = Dyn.FindControl(dynControl.Childs[j]);
            if (child != null) {
                child.SetValue('');
                if (fillDataSource) {
                    Dyn.CallDataSource(child);
                } else {
                    child.DataSource = [];
                    Dyn.BindDataSource(child);
                }

                Dyn.ClearAutocompleteChilds(child, false);
            }
        }
    }
}

Dyn.BindDataSource = function (d) {
    try {
        d.JQ().autocomplete('option', 'source', d.DataSource);
    } catch (e) { alert("ERROR: No fue posible enlazar los datos de la lista, " + Dyn.GetID(d) + ", " + e); }
}

Dyn.CallDataSource = function (d) {
    try {
        var request = "";
        for (var i = 0; i < this.Controls.length; i++) {
            request += "&" + this.Controls[i].ServerID + "=" + this.Controls[i].GetValue();
        }

        $.ajax({
            type: "POST",
            url: Dyn.DynAutoCompleteBinderPage,
            data: "FormID=" + Dyn.DynFormID + "&ClientID=" + d.ClientID + "&ServerID=" + d.ServerID + request,
            success: function (html) {
                try {
                    var parts = html.split("|");
                    var control = Dyn.FindControl(parts[0]);
                    if (control != null) {
                        eval("Dyn." + control.ServerID + ".DataSource = " + parts[1] + ";");
                        Dyn.BindDataSource(control);
                    }
                }
                catch (e) {
                    alert("ERROR: " + e + ", SCRIPT: " + html);
                }
            }
        });
    } catch (e) { alert("ERROR: No fue posible obtener los datos de la lista, " + Dyn.GetID(d) + ", " + e); }
}

Dyn.FindControl = function (id) {
    for (var i = 0; i < this.Controls.length; i++) {
        if (this.Controls[i].ServerID == id) {
            return this.Controls[i];
        }
    }
    return null;
}

Dyn.ManageEnterKey = function () {
    try {
        var focusables = $(":input").not("[type='hidden']").not("[id='" + this.ControlEventLinkID + "']"); //.not('[type="image"]').not('[type="submit"]');

        focusables.keydown(function (e) {
            if (e.keyCode == 13) {
                var node = (e.target) ? e.target : ((e.srcElement) ? e.srcElement : null);

                //                if ((node.type != "submit") && (node.type != "button")) {
                e.preventDefault();

                var current = focusables.index(this);

                var okNext = false;
                var currentCycle = 30;
                while (!okNext) {
                    var next = focusables.eq(current + 1).length ? focusables.eq(current + 1) : focusables.eq(0);

                    if (next.length && next[0].Tag != undefined && next[0].Tag != null) {
                        if (next[0].Tag.GetVisible() && next[0].Tag.ParentSection.GetVisible()) {
                            okNext = true;
                        }
                    } else {
                        if (next[0].style.display == "") {
                            okNext = true;
                        }
                    }

                    if (okNext) { next.focus(); }
                    else { current++; }

                    currentCycle--;
                    if (currentCycle < 0) okNext = true;
                }
                //                }
            }
        });
    } catch (e) { alert("ERROR: No fue posible establer la utilizacion de la tecla ENTER, " + Dyn.GetID(d) + ", " + e); }
}

Dyn.ManageHightLigths = function () {
    var focusables = $(":input").not("[type='hidden']").not("[id='" + this.ControlEventLinkID + "']").not('[type="button"]').not('[type="submit"]');
    //    toggleClass
    focusables.focus(function () { var jq = $(this); jq.removeClass("Control-Blur"); jq.addClass("Control-Focus"); });
    focusables.blur(function () { var jq = $(this); jq.removeClass("Control-Focus"); jq.addClass("Control-Blur"); });
}

Dyn.ShowToolTip = function (c, msg) {
    this.ToolTipItem.Control = c;
    this.ToolTipItem.Message = msg;

    setTimeout(Dyn.FadeInToolTip, 100);
}

Dyn.FadeInToolTip = function () {
    Dyn.HideToolTip();

    var jq = $(Dyn.ToolTipItem.Control);
    var offSet = jq.offset();
    offSet.left = offSet.left + jq.width() + 30;

    var qHtml = $("<div id='slyg_tooltip' class='slyg-tooltip'><div class='slyg-tooltip-content'>" + Dyn.ToolTipItem.Message + "</div></div>");
    qHtml.offset(offSet);
    $('body').append(qHtml)
    qHtml.fadeIn("slow");
}

Dyn.HideToolTip = function () {
    $('#slyg_tooltip').remove();
}


/*FUNCIONES AUXILIARES*/
var NullString = "__NULL";

function Get(i) {
    return document.getElementById(i);
}

function TryExec(s) {
    try {
        if (s != "") {
            eval(s);
        }
    } catch (e) {
        alert("ERROR: " + e + ", SCRIPT: " + s);
    }
}

function SetClassName(c, css) {
    //$("#" + c.id).toggleClass(css);
    if (c != null && c != undefined) c.className = css;
}

function SetVisible(c, v) {
    c.style.display = (v) ? "" : "none";
}

function GetVisible(c) {
    return (c.style.display == "");
}

Array.prototype.find = function (value) {
    for (var i = 0, loopCnt = this.length; i < loopCnt; i++) {
        if (this[i].toLowerCase() === value.toLowerCase()) {
            return this[i];
        }
    }

    return NullString;
};





//function AddEvent(obj, type, fn) {
//    if (obj.attachEvent) {
//        obj["e" + type + fn] = fn;
//        obj[type + fn] = function () { obj["e" + type + fn](window.event); }
//        obj.attachEvent("on" + type, obj[type + fn]);
//    } else
//        obj.addEventListener(type, fn, false);
//}

//function RemoveEvent(obj, type, fn) {
//    if (obj.detachEvent) {
//        obj.detachEvent("on" + type, obj[type + fn]);
//        obj[type + fn] = null;
//    } else
//        obj.removeEventListener(type, fn, false);
//}


/* CACHE DE VALORES */
//CacheValues = {
//    Items: new Array()
//};

//function CacheValueItem(nControlID, nServerID, nValue) {
//    this.ControlID = nControlID;
//    this.ServerID = nServerID;
//    this.Value = nValue;
//}

//CacheValues.Find = function (nControlID) {
//    for (var i = 0; i < this.Items.length; i++) {
//        if (this.Items[i].ControlID == nControlID) 
//            return this.Items[i];
//    }
//    return null;
//}

//CacheValues.Set = function (nControlID, nServerID, nValue) {
//    var item = this.Find(nControlID);
//    if (item == null) {
//        this.Items.push(new CacheValueItem(nControlID,nServerID, nValue));
//    }else {
//        item.Value = nValue;
//    }
//}

//CacheValues.FindValue = function (nControlID) {
//    var item = this.Find(nControlID);
//    if (item != null) return item.Value;
//    else return "";
//}