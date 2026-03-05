; (function($) {
    /**
    * jqGrid English Translation
    * Tony Tomov tony@trirand.com
    * http://trirand.com/blog/ 
    * Dual licensed under the MIT and GPL licenses:
    * http://www.opensource.org/licenses/mit-license.php
    * http://www.gnu.org/licenses/gpl.html
    **/
    $.jgrid = {
        defaults: {
            recordtext: "Total {0} - {1} de {2}",
            emptyrecords: "No se encuentran registros",
            loadtext: "Cargando...",
            pgtext: "Pagina {0} de {1}"
        },
        search: {
            caption: "Search...",
            Find: "Find",
            Reset: "Reset",
            odata: ['equal', 'not equal', 'less', 'less or equal', 'greater', 'greater or equal', 'begins with', 'does not begin with', 'is in', 'is not in', 'ends with', 'does not end with', 'contains', 'does not contain'],
            groupOps: [{ op: "AND", text: "all" }, { op: "OR", text: "any"}],
            matchText: " match",
            rulesText: " rules"
        },
        edit: {
            addCaption: "Add Record",
            editCaption: "Edit Record",
            bSubmit: "Submit",
            bCancel: "Cancel",
            bClose: "Close",
            saveData: "Data has been changed! Save changes?",
            bYes: "Si",
            bNo: "No",
            bExit: "Cancel",
            msg: {
                required: "Field is required",
                number: "Please, enter valid number",
                minValue: "value must be greater than or equal to ",
                maxValue: "value must be less than or equal to",
                email: "is not a valid e-mail",
                integer: "Please, enter valid integer value",
                date: "Please, enter valid date value",
                url: "is not a valid URL. Prefix required ('http://' or 'https://')",
                nodefined: " is not defined!",
                novalue: " return value is required!",
                customarray: "Custom function should return array!",
                customfcheck: "Custom function should be present in case of custom checking!"

            }
        },
        view: {
            caption: "View Record",
            bClose: "Close"
        },
        del: {
            caption: "Delete",
            msg: "Delete selected record(s)?",
            bSubmit: "Delete",
            bCancel: "Cancel"
        },
        nav: {
            edittext: "",
            edittitle: "Edit selected row",
            addtext: "",
            addtitle: "Add new row",
            deltext: "",
            deltitle: "Delete selected row",
            searchtext: "",
            searchtitle: "Find records",
            refreshtext: "",
            refreshtitle: "Reload Grid",
            alertcap: "Warning",
            alerttext: "Please, select row",
            viewtext: "",
            viewtitle: "View selected row"
        },
        col: {
            caption: "Select columns",
            bSubmit: "Ok",
            bCancel: "Cancel"
        },
        errors: {
            errcap: "Error",
            nourl: "No url is set",
            norecords: "No records to process",
            model: "Length of colNames <> colModel!"
        },
        formatter: {
            integer: { thousandsSeparator: " ", defaultValue: '0' },
            number: { decimalSeparator: ".", thousandsSeparator: " ", decimalPlaces: 2, defaultValue: '0.00' },
            currency: { decimalSeparator: ".", thousandsSeparator: " ", decimalPlaces: 2, prefix: "", suffix: "", defaultValue: '0.00' },
            date: {
                dayNames: [
				"Sun", "Mon", "Tue", "Wed", "Thr", "Fri", "Sat",
				"Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"
			],
                monthNames: [
				"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec",
				"January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"
			],
                AmPm: ["am", "pm", "AM", "PM"],
                S: function(j) { return j < 11 || j > 13 ? ['st', 'nd', 'rd', 'th'][Math.min((j - 1) % 10, 3)] : 'th' },
                srcformat: 'Y-m-d',
                newformat: 'd/m/Y',
                masks: {
                    ISO8601Long: "Y-m-d H:i:s",
                    ISO8601Short: "Y-m-d",
                    ShortDate: "n/j/Y",
                    LongDate: "l, F d, Y",
                    FullDateTime: "l, F d, Y g:i:s A",
                    MonthDay: "F d",
                    ShortTime: "g:i A",
                    LongTime: "g:i:s A",
                    SortableDateTime: "Y-m-d\\TH:i:s",
                    UniversalSortableDateTime: "Y-m-d H:i:sO",
                    YearMonth: "F, Y"
                },
                reformatAfterEdit: false
            },
            baseLinkUrl: '',
            showAction: '',
            target: '',
            checkbox: { disabled: true },
            idName: 'id'
        }
    };
})(jQuery);


var page = document.URL.replace('#', '').split("/")[document.URL.replace('#', '').split("/").length - 1];



function getStrinArray(value) {
    if (value != undefined && value != null) {
        var count = 0;
        var cadena = new Array();

        while (count < value.length) {
            cadena[count] = '{"' + value[count].join('","') + '"}';
            count = count + 1;
        }

        return cadena.join(",");
    }
    else {
        return '';
    }
}

function getArrayString(value) {
    if (value != "") {
        var count = 0;
        var total = value.split("},{");
        var Par = new Array();

        while (count < total.length) {
            var reg = ('{' + value.split("},{")[count] + '}').replace('{{', '{').replace('}}', '}').replace('{"', '').replace('"}', '').split('","');
            Par[Par.length] = ParByValue(reg[0], reg[1]);
            count = count + 1;
        }
        return Par;
    }

    return null;
}

function refactorizeColumns(columns) {

    var count = 0;
    var colNames = new Array();

    while (count < columns.length) {
        if (columns[count].colName == undefined) {
            colNames[count] = columns[count].name;
        }
        else {
            if (columns[count].colName == "")
                colNames[count] = columns[count].name;
            else
                colNames[count] = columns[count].colName;
        }
        count = count + 1;
    }

    return colNames;
}

jQuery.fn.fillgrid = function (method, parameters, columns, _multiselect, _menu) {

    //Crea la tabla y panel de la grilla
    var _divGrid = $(this);
    _divGrid.html('');
    var _tableGrid = _divGrid.attr("id") + '_packages';
    var _panelGrid = _divGrid.attr("id") + '_packagePager';
    var _methodGrid = _divGrid.attr("id") + '_method';
    var ondblClick = _divGrid.attr("ondblClick");
    var bMenu = false;

    if (parameters == undefined | parameters == null) { parameters = ''; }
    _divGrid.attr("ondblClick", "");

    var widthParent = _divGrid.css("width");
    var modificarWidth = (widthParent.indexOf("%") >= 0);
    if (modificarWidth) {
        widthParent = _divGrid.parent().css("width").replace("px", "") * widthParent.replace("%", "") / 100;
        _divGrid.append('<table id="' + _tableGrid + '" style="width: ' + widthParent + '"></table>');
    } else {
        _divGrid.append('<table id="' + _tableGrid + '" ></table>');
        widthParent = _divGrid.width();
    }

    _divGrid.append('<div id="' + _panelGrid + '"></div>');
    _divGrid.append('<spam style="display:none;" id="' + _methodGrid + '" class="' + method + '">' + getStrinArray(parameters) + '</spam>');

    //Formatear las columnas
    var countc = 0;
    while (countc < columns.length) {
        try {
            if (columns[countc].formatter.toLowerCase() == 'date') {
                columns[countc].formatoptions = { "srcformat": "d-m-Y", "newformat": "Y-m-d" };
            }
        } catch (a) { }
        columns[countc].jsonmap = 'cell.' + columns[countc].name;
        countc = countc + 1;
    }

    //Validar si es MultiSelect o tiene menu contextual
    if (_multiselect == null) { _multiselect = false; }
    if (_multiselect != false & _multiselect != true) { _multiselect = false; }
    bMenu = (_menu != null);


    $("#" + _tableGrid).jqGrid(
    {
        colNames: refactorizeColumns(columns),
        colModel: columns,
        pager: '#' + _panelGrid,
        ignoreCase: true,
        caption: _divGrid.attr("title"),
        rowNum: 10,
        rowList: [10, 20, 30],
        jsonReader: { repeatitems: false },
        datatype: function () { servergrid(method, parameters, _tableGrid); },
        multiselect: _multiselect,
        ondblClickRow: function (rowId) { if (ondblClick != undefined) { eval(ondblClick + "($('#' + _tableGrid).getRowData(rowId));"); } },
        afterInsertRow: function (rowid) { $('#' + rowid).contextMenu(_menu.Name, _menu.eventsMenu); }        
    }).setGridWidth(widthParent, true).setGridHeight(_divGrid.height(), true);

    _divGrid.attr("ondblClick", ondblClick);

    $(window).bind('resize', function () {
        $('#' + _tableGrid).setGridWidth(widthParent, true);
    }).trigger('resize');

    return $(this);
};

function servergrid(Metodo, parameters, grid) {
    var _table = $("#" + grid);

    $.ajax({
        url: page + "/" + Metodo,
        data: formatParameters(parameters),
        dataType: "json",
        type: "post",
        contentType: "application/json; charset=utf-8",
        jsonReader: {
            root: "rows",
            page: "page",
            records: "records"
        },

        complete: function(jsondata, stat) {
            if (stat == "success") {
                _table.setGridParam({ datatype: 'jsonstring' });
                _table.setGridParam({ datastr: JSON.parse(jsondata.responseText).d });
                _table.trigger("reloadGrid");
            }
            else {
                alert(JSON.parse(jsondata.responseText).Message);
            }
        },
        error: errores
    });
}

jQuery.fn.gridfilter = function () {
    var _divGrid = $(this);
    var _tableGrid = _divGrid.attr("id") + '_packages';
    $('#' + _tableGrid).jqGrid('filterToolbar', { defaultSearch: 'cn', stringResult: true });
};

jQuery.fn.gridreload = function () {
    var _divGrid = $(this);
    var _tableGrid = _divGrid.attr("id") + '_packages';
    var _metodpar = $('#' + _divGrid.attr("id") + '_method');
    var metodo = $.trim(_metodpar.attr("class"));
    var par = getArrayString($.trim(_metodpar.html()));
    servergrid(metodo, par, _tableGrid);
};

