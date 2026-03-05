/**
* Ajax Autocomplete for jQuery, version 1.1.5
* (c) 2010 Tomas Kirda, Vytautas Pranskunas
*
* Ajax Autocomplete for jQuery is freely distributable under the terms of an MIT-style license.
* For details, see the web site: http://www.devbridge.com/projects/autocomplete/jquery/
* Last Review: 07/24/2012
*
* Patch To: AutoList
* Autor: Eliseo Roa
* Last Review: 10/08/2012
*/

var ui_autocomplete_appendTo = "body";

var reEscape = new RegExp('(\\' + ['/', '.', '*', '+', '?', '|', '(', ')', '[', ']', '{', '}', '\\'].join('|\\') + ')', 'g');

function fnFormatResult(value, data, currentValue) {
    var pattern = '(' + currentValue.replace(reEscape, '\\$1') + ')';
    return value.replace(new RegExp(pattern, 'gi'), '<strong>$1<\/strong>');
}

function AutoComplete(el, options) {
    this.el = $(el);
    this.el.attr('autocomplete', 'off');
    this.suggestions = [];
    this.data = [];
    this.badQueries = [];
    this.selectedIndex = -1;
    this.currentValue = this.el.val();
    this.lastValue = "";
    this.intervalId = 0;
    this.cachedResponse = [];
    this.onChangeInterval = null;
    this.onChange = null;
    this.ignoreValueChange = false;
    this.serviceUrl = options.serviceUrl;
    this.isLocal = false;
    this.options = {
        autoSubmit: false,
        minChars: 0,
        maxHeight: 300,
        deferRequestBy: 50,
        width: 0,
        highlight: true,
        params: {},
        fnFormatResult: fnFormatResult,
        appendTo: ui_autocomplete_appendTo,
        delimiter: null,
        zIndex: 9999,
        validatedata : true
    };
    this.setOptions(options);
    this.initialize();
    this.initCss();
    this.el.data('autocomplete', this);
}

$.fn.autocomplete = function (options, optionName) {

    var autocompleteControl;

    if (typeof options == 'string') {
        autocompleteControl = this.data('autocomplete');
        if (autocompleteControl == null) return null;

        if (typeof autocompleteControl[options] == 'function') {
            autocompleteControl[options](optionName);
        }
    } else {
        autocompleteControl = new AutoComplete(this.get(0) || $('<input />'), options);
    }
    return autocompleteControl;
};

AutoComplete.prototype = {

    killerFn: null,

    lastSuggetions: "",
    autocompleteElId: null,

    initialize: function () {

        var me, uid;
        me = this;
        uid = Math.floor(Math.random() * 0x100000).toString(16);
        this.autocompleteElId = 'Autocomplete_' + uid;

        this.killerFn = function (e) {
            if ($(e.target).parents('.autocomplete').size() === 0) {
                try {
                    var m = $("#" + me.autocompleteElId);
                    var ending_right = m.offset().left + m.outerWidth() + 10;
                    var ending_bottom = m.offset().top + m.outerHeight();

                    if ((e.target.id != me.el[0].id) && (e.target.id != me.el[0].id + "_div")
                    && (!((ending_right > e.pageX && m.offset().left < e.pageX) && (ending_bottom > e.pageY && m.offset().top < e.pageY)))
                    ) {
                        me.killSuggestions();
                        me.disableKillerFn();
                    }
                } catch (e) { }
            }
        };

        if (!this.options.width) {
            this.options.width = this.el.width();
        }
        this.mainContainerId = 'AutocompleteContainter_' + uid;

        this.options.appendTo = this.parseAppendTo();

        $(this.options.appendTo).append('<div id="' + this.mainContainerId + '" style="position:absolute;z-index:9999;"><div class="autocomplete-w1"><div class="autocomplete" id="' + this.autocompleteElId + '" style="display:none; width:300px;"></div></div></div>');

        if (this.options.appendTo != 'body') {
            $(this.options.appendTo).css({ position: 'relative' });
        }

        this.container = $('#' + this.autocompleteElId);

        this.fixPosition(me);
        if (window.opera) {
            this.el.keypress(function (e) { me.onKeyPress(e); });
        } else {
            this.el.keydown(function (e) { me.onKeyPress(e); });
        }
        this.el.keyup(function (e) { me.onKeyUp(e); });
        //this.el.blur(function () { me.enableKillerFn(); });

        this.el.focus(function () { me.fixPosition(me); });
        this.el.change(function () { me.onValueChanged(); });
    },

    parseAppendTo: function () {
        //        if (this.options.appendTo == "body") {
        //            if (this.parsedAppendTo == undefined) {
        //                var parent = this.findParent(this.el[0]);
        //                this.parsedAppendTo = parent;
        //            }
        //            return this.parsedAppendTo;
        //        }
        return this.options.appendTo;
    },

    //    findParent: function (c) {
    //        var p = $(c).parent();
    //        if (p.length == 0) return c;
    //        if (p.is("form") || p.is("body") || (p.is("div") && p.width() > this.el.width() && p.height() > this.options.maxHeight)) {
    //            return p;
    //        }
    //        return this.findParent(p);
    //    },

    extendOptions: function (options) {
        $.extend(this.options, options);
    },

    setOptions: function (options) {
        var o = this.options;
        this.extendOptions(options);
        if (o.lookup || o.isLocal) {
            this.isLocal = true;
            if ($.isArray(o.lookup)) { o.lookup = { suggestions: o.lookup, data: [] }; }
        }
    },

    initCss: function () {
        var o = this.options;
        $('#' + this.mainContainerId).css({ zIndex: o.zIndex });
        this.container.css({ maxHeight: o.maxHeight + 'px', width: (o.width + 19) });
    },

    clearCache: function () {
        this.cachedResponse = [];
        this.badQueries = [];
    },

    disable: function () {
        this.disabled = true;
    },

    enable: function () {
        this.disabled = false;
    },

    fixPosition: function (me) {
        var offsetBody = $('body').offset();
        //if( me.options.appendTo == 'body'){ 
        offsetBody.left += 10; offsetBody.top += 10
        //}; // solucionado con  $(this.options.appendTo).css({position:'relative'});

        var offset = this.el.offset();
        var offSetParent = $(this.options.appendTo).offset();
        offset.left = offset.left + offsetBody.left - offSetParent.left;
        offset.top = offset.top + offsetBody.top - offSetParent.top;

        $('#' + this.mainContainerId).css({ top: (offset.top + this.el.innerHeight()) + 'px', left: offset.left + 'px' });

        if (!this.options.width) {
            me.options.width = me.el.width();
            me.initCss();
        }
    },

    enableKillerFn: function () {
        this.disableKillerFn();
        var me = this;
        //$(document).bind('click', me.killerFn);
        $(document).bind('mousedown', me.killerFn);
    },

    disableKillerFn: function () {
        var me = this;
        //$(document).unbind('click', me.killerFn);
        $(document).unbind('mousedown', me.killerFn);
    },

    killSuggestions: function () {
        var me = this;
        me.hide();
        this.stopKillSuggestions();
        //        this.intervalId = window.setInterval(function () {
        //            me.hide();
        //            me.stopKillSuggestions();
        //        }, 300);
    },

    stopKillSuggestions: function () {
        window.clearInterval(this.intervalId);
    },

    onValueChanged: function () {
        this.change(this.selectedIndex);
    },

    onKeyPress: function (e) {
        if (this.disabled || !this.enabled) {
            if ((e.keyCode == 9 || e.keyCode == 13) && this.currentValue == "") {
                this.unSelect();
            }
            return;
        }
        // return will exit the function
        // and event will not be prevented
        switch (e.keyCode) {
            case 27: //KEY_ESC:
                //this.el.val(this.currentValue);
                this.el.val(this.lastValue);
                this.hide();
                break;
            case 9: //KEY_TAB:
            case 13: //KEY_RETURN:
                if (this.selectedIndex === -1) {
                    var index = IndexOfArrayAU(this.suggestions, this.currentValue);
                    if (index >= 0) this.selectedIndex = index;
                }

                if (this.selectedIndex === -1) {
                    this.hide();

                    if (e.keyCode === 13)
                        this.unSelect();
                    else
                        this.el.val(this.lastValue);

                    return;
                }
                this.select(this.selectedIndex);
                if (e.keyCode === 9) return;
                break;
            case 38: //KEY_UP:
                this.moveUp();
                break;
            case 40: //KEY_DOWN:
                this.moveDown();
                break;
            default:
                return;
        }
        e.stopImmediatePropagation();
        e.preventDefault();
    },

    onKeyUp: function (e) {
        if (this.disabled) { return; }
        switch (e.keyCode) {
            case 38: //KEY_UP:
            case 40: //KEY_DOWN:
                return;
        }
        clearInterval(this.onChangeInterval);
        if (this.currentValue !== this.el.val()) {
            if (this.options.deferRequestBy > 0) {
                // Defer lookup in case when value changes very quickly:
                var me = this;
                this.onChangeInterval = setInterval(function () { me.onValueChange(); }, this.options.deferRequestBy);
            } else {
                this.onValueChange();
            }
        }
    },

    onValueChange: function () {
        clearInterval(this.onChangeInterval);
        this.currentValue = this.el.val();
        var q = this.getQuery(this.currentValue);
        this.selectedIndex = -1;
        if (this.ignoreValueChange) {
            this.ignoreValueChange = false;
            return;
        }
        if (q === '' || q.length < this.options.minChars) {
            this.hide();
        } else {
            this.getSuggestions(q);
        }
    },

    getQuery: function (val) {
        var d, arr;
        d = this.options.delimiter;
        if (!d) { return $.trim(val); }
        arr = val.split(d);
        return $.trim(arr[arr.length - 1]);
    },

    getSuggestionsLocal: function (q) {
        q = q.replace("*", "");
        var ret, arr, len, val, i;
        arr = this.options.lookup;
        len = arr.suggestions.length;
        ret = { suggestions: [], data: [] };
        q = q.toLowerCase();
        for (i = 0; i < len; i++) {
            val = arr.suggestions[i];
            if (val.toLowerCase().indexOf(q) === 0) {
                ret.suggestions.push(val);
                ret.data.push(arr.data[i]);
            }
        }
        return ret;
    },

    getSuggestions: function (q) {
        this.lastSuggetions = q;
        var cr, me;
        cr = this.isLocal ? this.getSuggestionsLocal(q) : this.cachedResponse[q]; //dadeta this.options.isLocal ||
        if (cr && $.isArray(cr.suggestions)) {
            this.suggestions = cr.suggestions;
            this.data = cr.data;
            this.suggest();
        } else if (!this.isBadQuery(q)) {
            me = this;
            if (me.options.fnTrace != undefined) me.options.fnTrace("Ajax query " + q);
            me.options.params.query = q;
            $.get(this.serviceUrl, me.options.params, function (txt) { me.processResponse(txt); }, 'text');
        }
    },

    isBadQuery: function (q) {
        var i = this.badQueries.length;
        while (i--) {
            if (q.indexOf(this.badQueries[i]) === 0) { return true; }
        }
        return false;
    },

    hide: function () {
        this.enabled = false;
        this.selectedIndex = -1;
        this.container.hide();
    },

    suggest: function () {

        if (this.suggestions.length === 0) {
            this.hide();
            return;
        }

        var me, len, div, f, v, i, s, mOver, mClick;
        me = this;
        me.fixPosition(me);
        len = this.suggestions.length;
        f = this.options.fnFormatResult;
        v = this.getQuery(this.currentValue);
        mOver = function (xi) { return function () { me.activate(xi); }; };
        mClick = function (xi) { return function () { me.select(xi); }; };
        this.container.hide().empty();
        for (i = 0; i < len; i++) {
            s = this.suggestions[i];
            div = $((me.selectedIndex === i ? '<div class="selected"' : '<div') + ' title="' + s + '">' + f(s, this.data[i], v) + '</div>');
            div.mouseover(mOver(i));
            div.click(mClick(i));
            this.container.append(div);
        }
        this.enabled = true;
        this.container.show();
        me.enableKillerFn();

    },

    processResponse: function (text) {
        var response;
        try {
            response = eval('(' + text + ')');
        } catch (err) { return; }
        if (!$.isArray(response.data)) { response.data = []; }
        if (!this.options.noCache) {
            this.cachedResponse[response.query] = response;
            if (response.suggestions.length === 0) { this.badQueries.push(response.query); }
        }
        if (response.query === this.getQuery(this.currentValue)) {
            this.suggestions = response.suggestions;
            this.data = response.data;
            this.suggest();
        }
    },

    activate: function (index) {
        var divs, activeItem;
        divs = this.container.children();
        // Clear previous selection:
        if (this.selectedIndex !== -1 && divs.length > this.selectedIndex) {
            $(divs.get(this.selectedIndex)).removeClass();
        }
        this.selectedIndex = index;
        if (this.selectedIndex !== -1 && divs.length > this.selectedIndex) {
            activeItem = divs.get(this.selectedIndex);
            $(activeItem).addClass('selected');
        }
        return activeItem;
    },

    deactivate: function (div, index) {
        div.className = '';
        if (this.selectedIndex === index) { this.selectedIndex = -1; }
    },

    select: function (i) {
        var selectedValue, f;
        selectedValue = this.suggestions[i];
        if (selectedValue) {
            this.el.val(selectedValue);
            if (this.options.autoSubmit) {
                f = this.el.parents('form');
                if (f.length > 0) { f.get(0).submit(); }
            }
            this.ignoreValueChange = true;
            this.hide();
            this.onSelect(i);
        }
        this.lastValue = this.el.val();
    },

    unSelect: function () {
        this.el.val("");
        this.ignoreValueChange = true;
        this.hide();
        this.onUnSelect();
        this.lastValue = "";
    },

    change: function (i) {
        var selectedValue, fn, me;
        me = this;
        selectedValue = this.suggestions[i];
        if (selectedValue) {
            var s, d;
            s = me.suggestions[i];
            d = me.data[i];
            me.el.val(me.getValue(s));
        }
        else {
            s = '';
            d = -1;
            if (me.options.validatedata) {
                if (me.el.val() != '') {
                    me.el.val(me.lastValue);
                    return;
                }
            }
        }

        fn = me.options.onChange;
        if ($.isFunction(fn)) { fn(s, d, me.el); }
    },

    moveUp: function () {
        if (this.selectedIndex === -1) { return; }
        if (this.selectedIndex === 0) {
            this.container.children().get(0).className = '';
            this.selectedIndex = -1;
            this.el.val(this.currentValue);
            return;
        }
        this.adjustScroll(this.selectedIndex - 1);
    },

    moveDown: function () {
        if (this.selectedIndex === (this.suggestions.length - 1)) { return; }
        this.adjustScroll(this.selectedIndex + 1);
    },

    adjustScroll: function (i) {
        var activeItem, offsetTop, upperBound, lowerBound;
        activeItem = this.activate(i);
        offsetTop = activeItem.offsetTop;
        upperBound = this.container.scrollTop();
        lowerBound = upperBound + this.options.maxHeight - 25;
        if (offsetTop < upperBound) {
            this.container.scrollTop(offsetTop);
        } else if (offsetTop > lowerBound) {
            this.container.scrollTop(offsetTop - this.options.maxHeight + 25);
        }
        this.el.val(this.getValue(this.suggestions[i]));
    },

    onSelect: function (i) {
        var me, fn, s, d;
        me = this;
        fn = me.options.onSelect;
        s = me.suggestions[i];
        d = me.data[i];
        me.el.val(me.getValue(s));
        if ($.isFunction(fn)) { fn(s, d, me.el); }
    },

    onUnSelect: function () {
        fn = this.options.onSelect;
        if ($.isFunction(fn)) { fn("", null, this.el); }
    },

    getValue: function (value) {
        var del, currVal, arr, me;
        me = this;
        del = me.options.delimiter;
        if (!del) { return value; }
        currVal = me.currentValue;
        arr = currVal.split(del);
        if (arr.length === 1) { return value; }
        return currVal.substr(0, currVal.length - arr[arr.length - 1].length) + value;
    }

};

$.fn.autoList = function (dataSource, fnChanged, options) {
    return this.each(function () {
        try {
            var ctr = this;
            var me = $(ctr);

            var ctrDisabled = ctr.disabled;
            var buttonid = ctr.id + "_div";

            var parent = $(me.parent()[0]);
            var ctrWidth = me.width();
            me.remove();

            var html = "<table cellpadding='0' cellspacing='0' align='left'> <tr> <td> <input type='text' id='" + ctr.id + "' name='" + ctr.name + "' value='" + ctr.value + "' class='" + ctr.className + "' /> </td> <td> <div id='" + buttonid + "' class='blist'></div></td></tr></table>";
            parent.append(html);

            var ctr = GetAU(ctr.id);
            var me = $(ctr);

            try { Global.CreateID(ctr.id); } catch (e) { } /*Parche para MIHARU*/

            ctr.disabled = ctrDisabled;

            ctr.LastVarValue = ctr.value;
            ctr.fnChanged = fnChanged;
            ctr.auoptions = options;

            if (ctrWidth > 0) me.width(ctrWidth);

            ctr.evaluateClick = function () {
                if (!this.disabled) {
                    var inputId = this.id;
                    var jq = $("#" + inputId).autocomplete("");
                    var q = GetAU(inputId).value;
                    if (q == "") q = "*";
                    else q = (jq.lastSuggetions == "") ? "*" : jq.lastSuggetions;

                    jq.currentValue = q;

                    jq.getSuggestions(q);
                    jq.stopKillSuggestions();
                }
            }

            $(ctr).click(function (e) {
                e.currentTarget.evaluateClick();
            });

            $("#" + buttonid).click(function (e) {
                GetAU(e.currentTarget.id.replace('_div', '')).evaluateClick();
            });

            ctr.ClearValue = function (enable) {
                this.LastVarValue = "";
                this.value = "";
                if (enable != undefined) {
                    this.disabled = !enable;
                    GetAU(this.id + "_div").disabled = this.disabled;
                }
            }

            ctr.evaluateValue = function (value, data, ui) {
                var ctr = ui[0];
                if (value == "... (mas)")
                    ctr.value = ctr.LastVarValue;
                value = ctr.value;

                if (ctr.LastVarValue != value) {
                    if (ctr.fnChanged != undefined && ctr.fnChanged != null) {
                        ctr.fnChanged(ctr, value, data);
                        if (document.activeElement == ctr) $(ctr).select();
                    }
                }
                ctr.LastVarValue = value;
            }

            var _options = (ctr.auoptions != undefined) ? ctr.auoptions : {};
            if (typeof (dataSource) == "string") {
                _options = $.extend(_options, {
                    maxHeight: 180,
                    serviceUrl: dataSource,
                    onSelect: ctr.evaluateValue,
                    onChange: ctr.evaluateValue,
                    deferRequestBy: 50
                });
                me.autocomplete(_options);
            } else {
                _options = $.extend(_options, {
                    maxHeight: 180,
                    delimiter: /(,|;)\s*/,
                    lookup: dataSource,
                    onSelect: ctr.evaluateValue,
                    onChange: ctr.evaluateValue
                });
                me.autocomplete(_options);
            }
        } catch (e) { alert("ERROR: No fue posible crear el controlador de lista, " + id + ", " + e); }
    });
}

function SetAutoListDataSource(id, d, p) {
    var ctr = null;
    if (typeof (id) == "string") ctr = $("#" + id);
    else ctr = $(id);

    if (ctr.length) {
        var v = (p) ? ctr.val() : "";

        ctr.autocomplete("setOptions", { lookup: d });
        ctr[0].DataSource = d;
        ctr[0].disabled = (d.length == 0);

        GetAU(ctr[0].id + "_div").disabled = ctr[0].disabled;

        ctr.val(v);
        ctr[0].LastVarValue = v;
    }
}

function EnableAutoList(id, e) {
    var ctr = null;
    if (typeof (id) == "string") ctr = $("#" + id);
    else ctr = $(id);

    ctr[0].disabled = !e;
    GetAU(ctr[0].id + "_div").disabled = !e;
}

function SetAutoListValue(id, val) {
    if (typeof (id) == "string")
        $("#" + id).val(val)[0].LastVarValue = val;
    else
        $(id).val(val)[0].LastVarValue = val;
}

function GetAU(i) { return document.getElementById(i); }
function IndexOfArrayAU(a, it) { for (var i = 0; i < a.length; i++) if (a[i].toLowerCase() == it.toLowerCase()) return i; return -1; }
