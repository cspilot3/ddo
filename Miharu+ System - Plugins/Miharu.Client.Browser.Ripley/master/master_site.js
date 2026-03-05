history.go(1);

$(function () { Site.Init(); });

AlertIcon = {
    Information: "MB_information",
    Warning: "MB_warning",
    Error: "MB_error"
};

Site = {
    CurrentPage: null,
    Loaded: false,
    
    ProcessDiv: 'ProcessDiv',
    LockDiv: 'LockDiv',
    Pwindow: null,
    Pdocument: null,
    UseDaughterCancelEvent: 0,
    PagNum: 0,
    TimeOutCounter: 0,
    TimeOutCancel: true,
    ObraActual: null,

    CloseDialogCallBack: null,

    Constructoras: [],
    Proyectos: [],
    Obras: [],

    Valido: false,
    Mensaje: "",

    SessionActivityCounter: 0,
    SessionScan: 2,
    TokenIP: '',
    TokenValid: true,

    Init: function() {
        var p = document.location.toString().split("/");
        Site.CurrentPage = p[p.length - 1];

        Site.ResizeFrame();
        Site.ResizeBody();

        $(window).resize(function() {
            Site.ResizeBody();
            Site.ResizeFrame();
        });

        $(".ProcessImg").click(Site.HideProcess);

        Site.HideProcess();

        $(".button").button();
        Site.Loaded = true;
        Site.IniciarMenu();
        if (typeof(window['AppInfo']) != "undefined") AppInfo.Init();

        Site.InitSessionTimeOutDialog();
    },

    IniciarMenu: function() {
        $(".TMI").each(function(i, m) {
            var jq = $(m);
            jq.addClass("ToolMenuItem");
            var html = "<table width='100%' border='0' cellpadding='0'cellspacing='0'><tr><td align='center'><img alt='' src='{img}' /></td></tr><tr><td class='rs3'></td></tr><tr><td align='center'><a onclick=''>{tl}</a></td></tr></table>";
            html = html.replace("{img}", jq.attr('iurl'));
            html = html.replace("{tl}", jq.html());
            jq.html(html);
            var page = jq.attr('page');
            if (page != undefined && page != "") {
                jq.click(function(e) { Site.Navigate(page); });
            }
        });
    },

    MostrarObraActual: function(obr) {
        Site.ObraActual = obr;
        $(function() {
            if (Site.ObraActual != null) {
                $("#ConstructoraLabel").html(Site.ObraActual.nombre_entidad);
                $("#ProyectoLabel").html(Site.ObraActual.nombre_proyecto);
                $("#ObraLabel").html(Site.ObraActual.nombre_obra);
            } else {
                $("#ConstructoraLabel").html("");
                $("#ProyectoLabel").html("");
                $("#ObraLabel").html("");
            }
        });
    },

    CargarProyectos: function() {
        try {
            Site.Proyectos = [];
            Site.Obras = [];
            SetAutoListDataSource("Proyecto", Site.Proyectos);
            SetAutoListValue("Proyecto", "");
            SetAutoListDataSource("Obra", Site.Obras);
            SetAutoListValue("Obra", "");

            var param = ValidPar("Constructora", "", true);
            AjaxRequestUrl("CargarProyectos", "", param, Site.MostrarProyectos, undefined, Site.CurrentPage);
        } catch(e) {
            alert(e);
        }
    },

    CargarObras: function() {
        try {
            Site.Obras = [];
            SetAutoListDataSource("Obra", Site.Obras);
            SetAutoListValue("Obra", "");

            var param = ValidPar("Constructora", "", true);
            param += "&" + ValidPar("Proyecto", "", true);
            AjaxRequestUrl("CargarObras", "", param, Site.MostrarObras, undefined, Site.CurrentPage);
        } catch(e) {
            alert(e);
        }
    },

    MostrarProyectos: function(html) {
        TryExec(html);
        SetAutoListDataSource("Proyecto", Site.Proyectos);
    },

    MostrarObras: function(html) {
        TryExec(html);
        SetAutoListDataSource("Obra", Site.Obras);
    },

    AceptarObraClick: function() {
        try {
            var param = ValidPar("Constructora", "", true);
            param += "&" + ValidPar("Proyecto", "", true);
            param += "&" + ValidPar("Obra", "", true);
            AjaxRequestUrl("CambiarObra", "", param, Site.ActualizarPagina, undefined, Site.CurrentPage);
        } catch(e) {
            alert(e);
        }
    },

    ActualizarPagina: function(html) {
        TryExec(html);

        $("#SeleccionarProyecto_Ventana").remove();
        Site.RefreshUrl();
    },

    ChangePasswordClick: function() {
        try {
            $("#ChangePassword_Message").html("");
            var newPassword = $("#NewPassword").val();
            var confirmPassword = $("#ConfirmPassword").val();

            if (newPassword == confirmPassword) {
                var param = ValidPar("OldPassword", "", false);
                param += "&" + ValidPar("NewPassword", "", false);
                AjaxRequestUrl("ChangePassword", "", param, Site.ChangePassword, undefined, Site.CurrentPage);
            } else {
                $("#ChangePassword_Message").html("Las contraseñas ingresadas no son iguales");
            }
        } catch(e) {
            alert(e);
        }
    },

    ChangePassword: function(html) {
        Site.Valido = false;
        TryExec(html);

        if (Site.Valido) {
            $("#ChangePassword_Ventana").remove();
            alert("La contraseña se actualizó correctamente");
        } else {
            $("#ChangePassword_Message").html(Site.Mensaje);
        }
    },

    SubformLoaded: function() {
        try {
            document.getElementById('FormIFrame').style.display = 'inline';
        } catch(ex) {
        }
    },

    ResizeFrame: function() {
        var alto = ($(window).height());
        var ancho = ($(window).width());

        $("#FormIFrame").css("height", (alto - 85) + "px");
        $("#FormIFrame").css("width", ancho + "px");
    },

    ResizeBody: function() {
        Site.ResizeFrame();
    },

    Lock: function() { document.getElementById(Site.LockDiv).style.display = 'inline'; },
    Unlock: function() { document.getElementById(Site.LockDiv).style.display = 'none'; },
    ShowProcess: function() { document.getElementById(Site.ProcessDiv).style.display = 'inline'; },
    HideProcess: function() {
        document.getElementById(Site.ProcessDiv).style.display = 'none';
        try {
            document.getElementById('LoadingDiv').style.display = 'none';
        } catch(e) {
        }
    },

    MostrarNotificacion: function(tit, msg) {
        $.gritter.add({ title: tit, text: msg });
    },

    InitSessionTimeOutDialog: function() {
        var continuar = function() {
            Site.TimeOutCancel = true;
            $("#SessionTimeOutDialog").dialog("close");
            setTimeout(Site.SessionActivityValidate, 60000);
        };

        var salir = function() {
            Site.TimeOutCancel = true;
            
            AjaxRequestUrl("CloseSession", "", undefined, function () {
                window.location = "account/login.aspx";
            }, undefined, Site.CurrentPage);
        };

        $("#SessionTimeOutDialog").dialog({
            autoOpen: false,
            width: 400,
            height: 200,
            modal: true,
            zIndex: 3999,
            buttons: {
                "Continuar": continuar,
                "Salir": salir
            }
        });
    },

    ShowSessionTimeOutDialog: function() {
        $("#SessionTimeOutDialog").dialog("open");

        Site.TimeOutCounter = 20;
        Site.TimeOutCancel = false;
        Site.ShowSessionTimeOutCounter();
    },

    ShowSessionTimeOutCounter: function() {
        if (!Site.TimeOutCancel) {
            if (Site.TimeOutCounter == 0) {
                $("#SessionTimeOutDialog").dialog("close");
                Site.ShowProcess();
                window.location = "account/login.aspx";
            } else {
                $("#SessionTimeOutSeconds").html(Site.TimeOutCounter);
                Site.TimeOutCounter--;

                setTimeout(Site.ShowSessionTimeOutCounter, 1000);
            }
        }
    },

    ShowAlert: function(mensaje, titulo, icono, ancho) {
        $("#dialog:ui-dialog").dialog("destroy");

        document.getElementById("dialog-message").title = titulo;
        document.getElementById("dialog-message-text").innerHTML = mensaje;
        document.getElementById("dialog-message-icon").className = icono;

        $("#dialog-message").dialog({
            modal: true,
            resizable: false,
            width: ancho,
            buttons: {
                Ok: function() {
                    $(this).dialog("close");
                }
            }
        });
    },

    ShowConfirm: function(mensaje, titulo, icono, ancho, fn) {
        $("#dialog:ui-dialog").dialog("destroy");

        document.getElementById("dialog-message").title = titulo;
        document.getElementById("dialog-message-text").innerHTML = mensaje;
        document.getElementById("dialog-message-icon").className = icono;

        $("#dialog-message").dialog({
            modal: true,
            resizable: false,
            width: ancho,
            buttons: {
                Ok: function() {
                    $(this).dialog("close");
                    if (fn != undefined) fn();
                }
            }
        });
    },

    ShowWindow: function(urlPage, titulo, parametros) {
        //Lock();
        window.open(urlPage, titulo, parametros);
    },

    ShowDialog: function(strUrl, valTitle, valWidth, valHeight, cancelEvent, closeCallBack) {
        var jDoc = $(window);
        var jPosy = (jDoc.height() - valHeight) / 2;
        var jPosx = (jDoc.width() - valWidth) / 2;

        jPosy = (jPosy < 30) ? 10 : jPosy - 20;

        Site.UseDaughterCancelEvent = cancelEvent;
        Site.CloseDialogCallBack = closeCallBack;

        $.newWindow({ id: "iframewindow", posx: jPosx, posy: jPosy, width: valWidth, height: valHeight, title: valTitle, type: "iframe", modal: true });
        $.updateWindowContent("iframewindow", "<iframe src='" + strUrl + "' width='" + valWidth + "' height='" + valHeight + "' />");
    },

    CloseDialog: function() {
        $.closeWindow("iframewindow");
        if (Site.CloseDialogCallBack != undefined && Site.CloseDialogCallBack != null) {
            Site.CloseDialogCallBack();
        }
    },

    SessionTokenStart: function() {
        setTimeout(Site.SessionToken, Site.SessionScan * 60000);
    },

    SessionToken: function() {
        if (Site.CurrentPage == "main.aspx") {
            Site.TokenValid = true;
            
            AjaxRequestUrl("ValidateToken", "", "",
                function(html) {
                    TryExec(html);

                    if (!Site.TokenValid) {
                        alert("El usuario ha iniciado otra sesión desde la IP: " + Site.TokenIP + ". El sistema cerrará la sesión actual.");

                        AjaxRequestUrl("CloseSession", "", undefined, function () {
                            window.location = "account/login.aspx";
                        }, undefined, Site.CurrentPage);
                    } else {
                        setTimeout(Site.SessionToken, Site.SessionScan * 60000);
                    }
                }, undefined, Site.CurrentPage,undefined, true);
        }
    },

    //WindowDialogClose: function (result) {
    //    Site.CloseDialog();
    //    Site.DaughterCloseEvent(result);
    //},

    //DaughterCloseEvent: function (result) {
    //    Site.Unlock();

    //    if (result) {
    //        if (Site.Pwindow != null && Site.Pwindow.Global.DaughterCloseEvent != undefined && Site.Pwindow.Global.DaughterCloseEvent != null)
    //            Site.Pwindow.Global.DaughterCloseEvent();
    //    }
    //},

    SetPointer: function (nwindow, ndocument) {
        Site.Pwindow = nwindow;
        Site.Pdocument = ndocument;
    },

    SetFormTitle: function (nTitle) {
        document.getElementById("FormTitle").innerText = nTitle;
    },

    Navigate: function (nUrl) {
        Site.ShowProcess();
        Site.LastNavigateUrl = nUrl;
        Site.RefreshUrl();
    },

    RefreshUrl: function () {
        if (Site.LastNavigateUrl != null) {
            var op = (Site.LastNavigateUrl.indexOf("?") > -1) ? "&" : "?";
            $("#FormIFrame").attr("src", Site.LastNavigateUrl + op + "p=" + (Site.PagNum++));
        }
    },

    About: function () {
        Site.Navigate("application/about.aspx");
    },

    ShowDashBoard: function () {
        Site.Navigate("application/dashboard.aspx");
    },

    Salir: function () {
        Site.ShowProcess();
        if (confirm("¿Desea cerrar la sesión?"))            
            AjaxRequestUrl("CloseSession", "", undefined, function () {                
                window.location = "account/login.aspx";
            }, undefined, Site.CurrentPage);
        else
            Site.HideProcess();
    }
};

//begin desabilitar teclas
document.onkeydown = function () {
    if (window.event && (window.event.keyCode == 122 || window.event.keyCode == 116)) window.event.keyCode = 505; /*116->f5 */ /*122->f11 */
    if (window.event.keyCode == 505) return false;
    if (window.event && (window.event.keyCode == 8)) {
        var valor = document.activeElement.value;
        if (valor == undefined) { return false; } /*Evita Back en página.*/
        else {
            if (IsElemAct('select-one')) { return false; } /*Evita Back en select. */
            if (IsElemAct('button')) { return false; } /*Evita Back en button. */
            if (IsElemAct('radio')) { return false; } /*Evita Back en radio. */
            if (IsElemAct('checkbox')) { return false; } /*Evita Back en checkbox. */
            if (IsElemAct('file')) { return false; } /*Evita Back en file. */
            if (IsElemAct('reset')) { return false; } /*Evita Back en reset. */
            if (IsElemAct('submit')) { return false; } /*Evita Back en submit.*/
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

