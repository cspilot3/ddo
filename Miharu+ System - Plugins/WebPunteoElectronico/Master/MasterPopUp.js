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

function BeginRequestHandler() {
    if (vShowLoader == "SI") ShowProcess();
}

function pageLoadedHandler() {
    var CtlAction = document.getElementById(ctrId_EndRequestAction);

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

    HideProcess();
}

function ShowProcess() { try { document.getElementById(ctrId_LockDiv).style.display = 'inline'; } catch (ex) { } }
function HideProcess() { try { document.getElementById(ctrId_LockDiv).style.display = 'none'; } catch (ex) { } }

function ShowAlert(Mensaje, Titulo, Icono, Ancho) {
        $(function () {
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
    });
}

function MostrarNotificacion(tit, msg) {
    parent.MostrarNotificacion(tit, msg);
}

function DlgDefaultClose() {
    setTimeout(DlgDefaultCloseExec, 1000);
}

function DlgDefaultCloseExec() {
    DlgClose(false);
}

var IsClosed = false;
function DlgClose(valResult) {
    if (!IsClosed) {
        try {
            var result = (valResult.toString().toUpperCase() == "TRUE") ? true : false;

            if (window.opener == null || typeof window.opener == "undefined") {
                window.parent.WindowDialogClose(result);
            }
            else {
                if (result) {
                    window.opener.DaughterCloseEvent(result);
                }

                window.opener.Unlock();
                window.close();
            }
        }
        catch (ex) {
            try {
                window.close();
            }
            catch (ex) { }
        }
        IsClosed = true;
    }
}

function RowDblClick(value, row, cells, evt, grid) {
    var strCells = "";
    for (var i = 0; i < cells.length; i++) {
        if (strCells != "") strCells += ",";

        var cellVal = cells[i].textContent;
        if (cellVal == undefined) cellVal = cells[i].innerText;

        strCells += "" + cells[i].abbr + ":" + EncodeScript(cellVal) + "";
    }
    document.getElementById(ctrId_FlexiGridRowIndexInfo).value = "" + grid + ";" + value + ";" + strCells;
    document.getElementById(ctrId_FlexiGridRowIndexChanged).click();
}