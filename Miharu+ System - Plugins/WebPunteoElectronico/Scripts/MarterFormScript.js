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

function BeginRequestHandler() { Proceso_yes(); }

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

    Proceso_no();
}

//function Lock() { parent.Lock(); }
//function Unlock() { parent.Unlock(); }
//function ShowProcess() { parent.ShowProcess(); }
//function HideProcess() { parent.HideProcess(); parent.SubformLoaded(); }

function Bloquear_yes() { parent.Bloquear_yes(); }
function Bloquear_no() { parent.Bloquear_no(); }
function Proceso_yes() { parent.Proceso_yes(); }

function Proceso_no() {
    try { parent.Proceso_no(); parent.SubformCargado(); } catch (ex) { }
}

function RegistarApuntadores() {
    parent.RegistarApuntadores(this.window, this.document);
}

function HijaCloseEvent() {
    document.getElementById(ctrId_DaughterCloseLinkButton).click();
}

function DaughterCloseEvent() {
    document.getElementById(ctrId_DaughterCloseLinkButton).click();
}

function MostrarNotificacion(tit, msg) {
    parent.MostrarNotificacion(tit, msg);
}

/*
Scripts de Master Config
----------------------------------*/
var SelectedTab = 0;

// Responder a la seleccion de un tab
function CreateTabsHandlers() {
    $('#tabs').tabs({
        selected : SelectedTab ,
        select: function (event, ui) {
            ShowOpciones(ui.index == 0 ? 'none' : 'inline');
            return true;
        }
    });
}

function ShowOpciones(Modo) {
//    document.getElementById(ctrId_SaveDiv).style.display = Modo;
//    document.getElementById(ctrId_DeleteDiv).style.display = Modo;
}

function SelectTab(tab) {
    SelectedTab = (tab == "Filtro") ? 0 : 1;
    ShowOpciones(SelectedTab == 0 ? 'none' : 'inline');
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


