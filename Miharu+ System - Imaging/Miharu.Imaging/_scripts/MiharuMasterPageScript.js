history.go(1);

//begin desabilitar teclas 
document.onkeydown = function()
{  
    //116->f5 
    //122->f11 
    if (window.event && (window.event.keyCode == 122 || window.event.keyCode == 116))
    {
        window.event.keyCode = 505;
    }

    if (window.event.keyCode == 505)
    {
        return false;
    }

    if (window.event && (window.event.keyCode == 8))
    {
        valor = document.activeElement.value;
        
        if (valor==undefined) //Evita Back en página.
        {
            return false;
        }
        else
        {
            if (document.activeElement.getAttribute('type')=='select-one')  { return false; } //Evita Back en select. 
            if (document.activeElement.getAttribute('type')=='button')      { return false; } //Evita Back en button. 
            if (document.activeElement.getAttribute('type')=='radio')       { return false; } //Evita Back en radio. 
            if (document.activeElement.getAttribute('type')=='checkbox')    { return false; } //Evita Back en checkbox. 
            if (document.activeElement.getAttribute('type')=='file')        { return false; } //Evita Back en file. 
            if (document.activeElement.getAttribute('type')=='reset')       { return false; } //Evita Back en reset. 
            
            if (document.activeElement.getAttribute('type')=='submit') //Evita Back en submit.
            {
                return false;
            }
            else //Text, textarea o password
            {
                if (document.activeElement.value.length==0) //No realiza el backspace(largo igual a 0). 
                {
                    return false;
                }
                else //Realiza el backspace.
                {
                    document.activeElement.value.keyCode = 8;
                }
            }
        }
    }
}
//end desabilitar teclas 


function BeginRequestHandler(){ Proceso_yes(); }

function pageLoadedHandler()
{
    var CtlAction = document.getElementById(ctrId_EndRequestAction);
    var arrAction = CtlAction.value.split("|");
    
    CtlAction.value = "";
            
    if(arrAction[0] == "Close")
    {
        window.close();
        return;
    }
    else if(arrAction[0] != "")
    {
        // Determinar los parametros
        var arrParametros = arrAction[1].split(";");
        
        if(arrAction[0] == "Alert")
        {            
            ShowAlert(arrParametros[0], arrParametros[1], arrParametros[2])
        }
        else if (arrAction[0] == "ShowWindow")
        {
            ShowWindow(arrParametros[0], arrParametros[1], arrParametros[2])            
        }
        else if (arrAction[0] == "ShowDialog")
        {
            ShowDialog(arrParametros[0], arrParametros[1], arrParametros[2], arrParametros[3], arrParametros[4], arrParametros[5], arrParametros[6], arrParametros[7])
        }        
    }
    
    Proceso_no();
}

function Bloquear_yes(){ try { document.getElementById(ctrId_div_Bloquear).style.display = 'inline'; }catch(ex){}}
function Bloquear_no(){ try { document.getElementById(ctrId_div_Bloquear).style.display = 'none'; }catch(ex){}}
function Proceso_yes(){ try { document.getElementById(ctrId_div_Proceso).style.display = 'inline'; }catch(ex){}}
function Proceso_no(){ try { document.getElementById(ctrId_div_Proceso).style.display = 'none'; }catch(ex){}}

function ShowAlert(Mensaje, Icono, Ancho)
{
    Modalbox.show("<div class=\'" + Icono + "\'>" +
                    "<table>" +
                         "<tr>" +
                            "<td style='width:40px'></td>" +
                            "<td><p>" + Mensaje + "</p></td>" +
                         "</tr>" +
                         "<tr>" +
                            "<td style='height:20px'></td>" +
                            "<td></td>" +
                         "</tr>" +
                         "<tr>" +
                            "<td></td>" +
                            "<td><input type=\'button\' value=\'Aceptar\' onclick=\'Modalbox.hide()\' /></td>" +
                         "</tr>" +
                    "</table>" +
                  "</div>", { title: ':: MIHARU ::', width: Ancho });
}

function ShowWindow(UrlPage, Titulo, Parametros)
{
    Bloquear_yes();
    window.open(UrlPage, Titulo, Parametros);
}

function ShowDialog(strUrl, strNombre, strTitle, valWidth, valHeight, valLeft, valTop, CancelEvent)
{
    Bloquear_yes();
    
    UseHijaCancelEvent = CancelEvent;
    
    wd = new Window({ url: strUrl, className: "alphacube", title: strTitle , width: valWidth, height: valHeight , top: valTop, left: valLeft });
    wd.setZIndex(101);

    wd.setCloseCallback(WindowDialogOnClosed);
    wd.showCenter();
    
    myObserver = 
    { 
        onEndResize: function(eventName, win) 
        { 
            if (win == wd) 
            { 
                var objSizePopUp = wd.getSize(); 
                
                try
                {
                    var obj = window.frames;
                    obj[0].PopupResized(objSizePopUp.width, objSizePopUp.height);
                }catch(ex){}
                
            } 
         } 
    } 
    Windows.addObserver(myObserver);

    myObserver2 = 
    { 
        onMaximize: function(eventName, win)
        { 
            if (win == wd) 
            { 
                var objSizePopUp = wd.getSize();
                
                try
                {
                    var obj = window.frames;
                    obj[0].PopupResized(objSizePopUp.width, objSizePopUp.height);
                }catch(ex){}
                
            } 
         } 
    } 
    Windows.addObserver(myObserver2);
}

function ShowContentDialog(strNombre, strTitle, valWidth, valHeight, valLeft, valTop, strContent, CancelEvent)
{
    Bloquear_yes();
    
    UseHijaCancelEvent = CancelEvent;
    
    wd = new Window(strNombre, {title: strTitle, className: "alphacube", top:valTop, left:valLeft, width: valWidth, height: valHeight})
    wd.getContent().innerHTML = strContent;
    //wd.setDestroyOnClose();
    wd.setZIndex(101);

    wd.setCloseCallback(WindowDialogOnClosed);
    wd.showCenter();
}

function WindowDialogClose(result)
{
    wd.close();
    HijaCloseEvent(result)
}

function HijaCloseEvent(result)
{
    Bloquear_no();
    
    if(result)
    {        
        if(Pwindow != null)
        {
            Pwindow.HijaCloseEvent();
        }
    }
}

function WindowDialogOnClosed()
{
    if (UseHijaCancelEvent==1)
    {
        if(Pwindow != null)
        {
            Pwindow.HijaCloseEvent();
        }
    }
    
    Bloquear_no();
    wd.destroy();
    wd = null;
    
    return true;
}

function hideModalPopupViaClient(ModalControlName) {
    //ev.preventDefault();
    var modalPopupBehavior = $find(ModalControlName);
    modalPopupBehavior.hide();
}

function RegistarApuntadores(nwindow, ndocument)
{
    Pwindow = nwindow;
    Pdocument = ndocument;
}

function NotificarCargue()
{
    if(window.opener != null)
    {
        try { window.opener.OpenedChield();}catch(ex){}
    }
}

function NotificarCierre()
{
    if(window.opener != null)
    {
        try { window.opener.ClosedChield();}catch(ex){}
    }
}
