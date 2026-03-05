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

    $(".button").button();
    setTimeout(Redimencionar, 300);
    HideProcess();
}

function Lock() { parent.Lock(); }
function Unlock() { parent.Unlock(); }
function ShowProcess() { parent.ShowProcess(); }
function HideProcess() { parent.HideProcess(); parent.SubformLoaded(); }

function SetPointers() {
    parent.SetPointer(this.window, this.document);
}

function DaughterCloseEvent() {
    __doPostBack(ctrId_DaughterFormCloseLinkButton, '');
}

function MostrarNotificacion(tit, msg) {
    parent.MostrarNotificacion(tit, msg);
}

function ShowAlert(Mensaje, Titulo, Icono, Ancho) {
    parent.ShowAlert(Mensaje, Titulo, Icono, Ancho);
}

function ShowWindow(UrlPage, Titulo, Parametros) {
    parent.ShowWindow(UrlPage, Titulo, Parametros);
}

function ShowDialog(strUrl, valName, valTitle, valWidth, valHeight, CancelEvent, closeCallBack) {
    parent.ShowDialog(strUrl, valName, valTitle, valWidth, valHeight, CancelEvent, closeCallBack);
}


function EvalScriptBag(script) {
    try {
        if (script != "") {
            eval(script);
        }
    } catch (e) { alert("Error al ejecutar el script , " + script); }
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

function Redimencionar() {
//    var Alto = ($(window).height());
//    var Ancho = ($(window).width());

    //$("#baseForm").css("height", (Alto - 2) + "px");
//    $("#baseForm").css("height", (Alto - 7) + "px");

//    $("#baseForm").css("width", (Ancho - 1) + "px");
    
//    $(".panel-content").css("height", (Alto - 45) + "px");
}

function ConfirmGuardar() {
    return confirm("¿Confirma que desea guardar los cambios realizados?");
}

function ConfirmEliminacion() {
    return confirm("¿Confirma que desea eliminar el registro?");
}

function ConfirmProcesar() {
    return confirm("¿Confirma que desea continuar el proceso?");
}

