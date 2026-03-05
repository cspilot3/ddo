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
        parent.close();
        return;
    }
    else if(arrAction[0] != "")
    {
        // Determinar los parametros
        var arrParametros = arrAction[1].split(";");
        
        if(arrAction[0] == "Alert")
        {            
            parent.ShowAlert(arrParametros[0], arrParametros[1], arrParametros[2])
        }
        else if (arrAction[0] == "ShowWindow")
        {
            parent.ShowWindow(arrParametros[0], arrParametros[1], arrParametros[2])            
        }
        else if (arrAction[0] == "ShowDialog")
        {
            parent.ShowDialog(arrParametros[0], arrParametros[1], arrParametros[2], arrParametros[3], arrParametros[4], arrParametros[5], arrParametros[6], arrParametros[7])
        }        
    }
    
    Proceso_no();
}

function Bloquear_yes(){ parent.Bloquear_yes();}
function Bloquear_no(){ parent.Bloquear_no();}
function Proceso_yes(){ parent.Proceso_yes();}
function Proceso_no(){ parent.Proceso_no(); parent.SubformCargado();}

function hideModalPopupViaClient(ModalControlName) 
{
    //ev.preventDefault();
    var modalPopupBehavior = $find(ModalControlName);
    modalPopupBehavior.hide();
}

function RegistarApuntadores()
{
    parent.RegistarApuntadores(this.window, this.document);
}

function HijaCloseEvent()
{
    document.getElementById(ctrId_lnkbHijaClose).click();
}