history.go(1);

//begin desabilitar teclas
document.onkeydown = function () {
    //116->f5 
    //122->f11 
    if (window.event && (window.event.keyCode == 122 || window.event.keyCode == 116))
        window.event.keyCode = 505;

    if (window.event.keyCode == 505)
        return false;

    if (window.event && (window.event.keyCode == 8)) {
        valor = document.activeElement.value;

        if (valor == undefined) //Evita Back en página.
        {
            return false;
        }
        else {
            if (document.activeElement.getAttribute('type') == 'select-one') { return false; } //Evita Back en select. 
            if (document.activeElement.getAttribute('type') == 'button') { return false; } //Evita Back en button. 
            if (document.activeElement.getAttribute('type') == 'radio') { return false; } //Evita Back en radio. 
            if (document.activeElement.getAttribute('type') == 'checkbox') { return false; } //Evita Back en checkbox. 
            if (document.activeElement.getAttribute('type') == 'file') { return false; } //Evita Back en file. 
            if (document.activeElement.getAttribute('type') == 'reset') { return false; } //Evita Back en reset. 

            if (document.activeElement.getAttribute('type') == 'submit') //Evita Back en submit.
            {
                return false;
            }
            else //Text, textarea o password
            {
                if (document.activeElement.value.length == 0) //No realiza el backspace(largo igual a 0). 
                    return false;
                else //Realiza el backspace.
                    document.activeElement.value.keyCode = 8;
            }
        }
    }
}
//end desabilitar teclas 

function BeginRequestHandler() { ShowProcess(); }

function pageLoadedHandler() {
    var CtlAction = Get(ctrId_EndRequestAction);

    if (CtlAction != null) {
        var strScripts = CtlAction.value;
        if (strScripts != "") {
            var scripts = strScripts.split("´");
            for (var i = 0; i < scripts.length; i++) {
                if (scripts[i] != "") {
                    var decScr = DecodeScript(scripts[i]);
                    TryExec(decScr);
                }
            }
        }
    }

    ResizeBody();
    HideProcess();
}

function Lock() { try { document.getElementById(ctrId_LockDiv).style.display = 'inline'; } catch (ex) { } }
function Unlock() { try { document.getElementById(ctrId_LockDiv).style.display = 'none'; } catch (ex) { } }
function ShowProcess() { try { document.getElementById(ctrId_ProcessDiv).style.display = 'inline'; } catch (ex) { } }
function HideProcess() { try { document.getElementById(ctrId_ProcessDiv).style.display = 'none'; } catch (ex) { } }

var CloseDialogCallBack = null;

function MostrarNotificacion(tit, msg) {
    $.gritter.add({ title: tit, text: msg });
}

function ShowAlert(Mensaje, Titulo, Icono, Ancho) {
    $("#dialog:ui-dialog").dialog("destroy");

    document.getElementById("dialog-message").title = Titulo;
    document.getElementById("dialog-message-text").innerHTML = Mensaje;
    document.getElementById("dialog-message-icon").className = Icono;

    $("#dialog-message").dialog({
        modal: true,
        resizable: false,
        width: Ancho,
        buttons: {
            Ok: function () {
                $(this).dialog("close");
            }
        }
    });
}

function ShowWindow(UrlPage, Titulo, Parametros) {
    //Lock();
    window.open(UrlPage, Titulo, Parametros);
}

function ShowDialog(strUrl, valName, valTitle, valWidth, valHeight, CancelEvent, closeCallBack) {
    var jDoc = $(window);
    var jPosy = (jDoc.height() - valHeight) / 2;
    var jPosx = (jDoc.width() - valWidth) / 2;

    jPosy = (jPosy < 30) ? 10 : jPosy - 20;

    UseDaughterCancelEvent = CancelEvent;
    CloseDialogCallBack = closeCallBack;

    $.newWindow({ id: "iframewindow", posx: jPosx, posy: jPosy, width: valWidth, height: valHeight, title: valTitle, type: "iframe", modal: true });
    //    $.newWindow({ id: "iframewindow", posx: 100, posy: 10, width: valWidth, height: valHeight, title: valTitle, type: "iframe", modal: true });
    $.updateWindowContent("iframewindow", "<iframe src='" + strUrl + "' width='" + valWidth + "' height='" + valHeight + "' />");
}

function CloseDialog() {
    $.closeWindow("iframewindow");
    if (CloseDialogCallBack != undefined && CloseDialogCallBack != null) {
        CloseDialogCallBack();
    }
}

function WindowDialogClose(result) {
    CloseDialog();
    DaughterCloseEvent(result)
}

function DaughterCloseEvent(result) {
    Unlock();

    if (result) {
        if (Pwindow != null)
            Pwindow.DaughterCloseEvent();
    }
}

function SetPointer(nwindow, ndocument) {
    Pwindow = nwindow;
    Pdocument = ndocument;
}

function SetFormTitle(nTitle) {
    var c = document.getElementById("FormTitle");
    c.innerText = nTitle;
}

function FireParentPostback() {
//    var ctr = document.getElementById(ctrId_RefreshLinkButton);
//    ctr.click();
    __doPostBack(ctrId_RefreshLinkButton, '');
    //ctl00$RefreshLinkButton
}



var inFireResizeBody = false;

function ResizeBody() {
    if (!inFireResizeBody) {
        inFireResizeBody = true;
        setTimeout(FireResizeBody, 100);
    }
}

function FireResizeBody() {
    ResizeChild();

    inFireResizeBody = false;
}

function NotificarCargue() {
    if (window.opener != null) {
        try { window.opener.OpenedChield(); } catch (ex) { }
    }
}

function NotificarCierre() {
    if (window.opener != null) {
        try { window.opener.ClosedChield(); } catch (ex) { }
    }
}

function ShowWindowNoBloqueo(UrlPage, Titulo, Parametros) {
    window.open(UrlPage, Titulo, Parametros);
}