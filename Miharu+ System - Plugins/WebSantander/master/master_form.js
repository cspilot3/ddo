history.go(1);

$(document).ready(function () { Global.InitDocument(); });

Options = {};

var Site = parent.Site;
var AlertIcon = parent.AlertIcon;

/* 
Objeto que controla los elementos globales de la barra de herramientas y el filtro detalle
*/
Global = {
    IsLogged: false,
    BaseUrl: "",
    EditOptions: [],
    LastAction: "",
    FnOption: null,
    FnFilter: null,
    SessionTimeOut: 20,
    
    InitDocument: function() {
        Global.Redimencionar();

        $(window).resize(function() { Global.Redimencionar(); });

        $(".button").button();
        if (Global.IsLogged) Global.InitForm();
        Global.HideProcess();
    },

    InitForm: function() {
        try {
            Global.SetPointers();
            parent.Site.SubformLoaded();

            try {
                this.BaseUrl = document.location.toString().substr(0, document.location.toString().indexOf('/site/'));
            } catch(e) {
            }
            
            this.CreateOptionBar();

            if (typeof(window['Frm']) != "undefined") {
                $('[id]').each(function(i, l) { Global.CreateID(l.id); });
                Frm.Init();
            }

            if (Global.IsLogged)
                Global.SessionActivity();
            
            Global.InitMaxLength();
        } catch(e) {
            alert("Error al inicializar el formulario, " + e);
        }
    },

    InitMaxLength: function() {
        $(document).on('keyup change', 'textarea[maxlength]', function() {
            var str = $(this).val();
            var mx = parseInt($(this).attr('maxlength'));
            if (str.length > mx) {
                $(this).val(str.substr(0, mx));
                return false;
            }

            return true;
        });
    },

    CreateID: function(id) {
        try {
            if (!ReplaceAll(id, " ", "") == "")
                eval("Frm." + id.replace("-", "_").replace(" ", "_") + " = $('#" + id + "');");
        } catch(e) {
            throw "Control no valido " + id;
        }
    },

    InitConfigHandlers: function(tab, fnOption) {
        this.FnOption = fnOption;
        $('#' + tab).tabs({
            activate: function(event, ui) {
                Global.HideOptions();
                Global.ShowOptions("F");

                if (ui.index != 0 && (Global.LastAction == "New" || Global.LastAction == "Edit")) {
                    Global.ShowOptions("D");
                }
                return true;
            }
        });
        this.ShowOptions("F");
    },

    SessionActivity: function() {        
        setTimeout(Global.SessionActivityValidate, 60000); // 1 minute        
    },

    SessionActivityValidate: function() {
        Site.SessionActivityCounter++;

        if (Site.SessionActivityCounter >= Global.SessionTimeOut) {
            Site.SessionActivityCounter = 0;

            parent.Site.ShowSessionTimeOutDialog();
        } else {
            setTimeout(Global.SessionActivityValidate, 60000);
        }
    },
    
    OptionClick: function(e) {
        if (Global.FnOption != null)
            Global.FnOption(e.target.className.split(" ")[1]);
    },

    InitFilter: function(fn) {
        $(".filter-button").click(Global.FilterClick);
        Global.FnFilter = fn;
    },

    FilterClick: function(e) {
        if (Global.FnFilter != undefined && Global.FnFilter != null) {
            switch (e.target.innerHTML) {
            case "Ninguno":
                Global.FnFilter("");
                break;
            case "Todos":
                Global.FnFilter("*");
                break;
            case "A-D":
                Global.FnFilter("[A-D]*");
                break;
            case "E-H":
                Global.FnFilter("[E-H]*");
                break;
            case "I-L":
                Global.FnFilter("[I-L]*");
                break;
            case "M-P":
                Global.FnFilter("[M-P]*");
                break;
            case "R-T":
                Global.FnFilter("[Q-T]*");
                break;
            case "U-Z":
                Global.FnFilter("[U-Z]*");
                break;
            }
        }
    },

    CreateOptionBar: function() {
        for (var i = 0; i < this.EditOptions.length; i++) {
            var opt = this.EditOptions[i].split('-');

            this.AddOption(opt[0], opt[1], opt[2]);
        }

        $(".option", "#option_container").click(Global.OptionClick);
    },

    AddOption: function(o, t, v) {
        $("<style type='text/css'> ." + o + " { background-image: url('" + Global.BaseUrl + "/images/options/" + o + ".png'); } </style>").appendTo("head");
        $("#option_container").append("<div id='option_" + o + "' class='option " + o + " " + v + "' " + ((t == "") ? "" : " title='" + t + "'") + "></div>");
        eval("Options." + o + " = $('#option_" + o + "');");
    },

    HideOptions: function() {
        $(".option", "#option_container").hide();
    },

    ShowOptions: function(v1, v2, v3, v4) {

        if (v1 != undefined) $("." + v1, "#option_container").show();
        if (v2 != undefined) $("." + v2, "#option_container").show();
        if (v3 != undefined) $("." + v3, "#option_container").show();
        if (v4 != undefined) $("." + v4, "#option_container").show();
    },

    SelectTab: function(tab, index, action) {
        Global.LastAction = action;
        $('#' + tab).tabs('option', 'active', index);
    },

    Lock: function() { parent.Site.Lock(); },
    Unlock: function() { parent.Site.Unlock(); },
    ShowProcess: function() { parent.Site.ShowProcess(); },
    HideProcess: function() { parent.Site.HideProcess(); },

    SetPointers: function() {
        try {
            parent.Site.SetPointer(window, document);
        } catch(e) {
        }
    },

    //DaughterCloseEvent: function() {
    //    __doPostBack(ctrId_DaughterFormCloseLinkButton, '');
    //},

    MostrarNotificacion: function(tit, msg) {
        parent.Site.MostrarNotificacion(tit, msg);
    },

    ShowAlert: function(mensaje, titulo, icono, ancho) {
        parent.Site.ShowAlert(mensaje, titulo, icono, ancho);
    },

    ShowWindow: function(urlPage, titulo, parametros) {
        parent.Site.ShowWindow(urlPage, titulo, parametros);
    },

    ShowDialog: function(strUrl, valTitle, valWidth, valHeight, cancelEvent, closeCallBack) {
        parent.Site.ShowDialog(strUrl, valTitle, valWidth, valHeight, cancelEvent, closeCallBack);
    },

    Redimencionar: function() {
        //    var Alto = ($(window).height());
        //    var Ancho = ($(window).width());
    },

    ConfirmGuardar: function() {
        return confirm("¿Confirma que desea guardar los cambios realizados?");
    },

    ConfirmEliminacion: function() {
        return confirm("¿Confirma que desea eliminar el registro?");
    },

    ConfirmProcesar: function() {
        return confirm("¿Confirma que desea continuar el proceso?");
    }
};

//begin desabilitar teclas
document.onkeydown = function() {
    if (window.event && (window.event.keyCode == 122 || window.event.keyCode == 116)) window.event.keyCode = 505; /*116->f5 */ /*122->f11 */
    if (window.event.keyCode == 505) return false;
    if (window.event && (window.event.keyCode == 8)) {
        var valor = document.activeElement.value;
        if (valor == undefined) {
            return false;
        } /*Evita Back en página.*/
        else {
            if (IsElemAct('select-one')) {
                return false;
            } /*Evita Back en select. */
            if (IsElemAct('button')) {
                return false;
            } /*Evita Back en button. */
            if (IsElemAct('radio')) {
                return false;
            } /*Evita Back en radio. */
            if (IsElemAct('checkbox')) {
                return false;
            } /*Evita Back en checkbox. */
            if (IsElemAct('file')) {
                return false;
            } /*Evita Back en file. */
            if (IsElemAct('reset')) {
                return false;
            } /*Evita Back en reset. */
            if (IsElemAct('submit')) {
                return false;
            } /*Evita Back en submit.*/
            else { /*Text, textarea o password*/
                if (document.activeElement.value.length == 0) return false; /*No realiza el backspace(largo igual a 0).*/
                else document.activeElement.value.keyCode = 8; /*Realiza el backspace.*/
            }
        }
    }

    return true;
};

function IsElemAct(t) { return (document.activeElement.getAttribute('type') == t); }
//end desabilitar teclas
