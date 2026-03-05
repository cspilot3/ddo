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

function BeginRequestHandler()
{
    if (vShowLoader == "SI") Proceso_yes();
}

function pageLoadedHandler()
{
    var CtlAction = document.getElementById(ctrId_EndRequestAction);
    var arrAction = CtlAction.value.split("|");
    
    CtlAction.value = "";
            
    if(arrAction[0] == "Close")
    {
        DlgClose(arrAction[1]);
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
    }
    
    Proceso_no();
}

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

function DlgClose(valResult)
{
    try
    {
        var result = (valResult.toString().toUpperCase() == "TRUE") ? true : false;
        
        if( window.opener == null || typeof window.opener == "undefined" )
        {
            window.parent.WindowDialogClose(result);
        }
        else
        {
            if( result )
            {
                try { window.opener.HijaCloseEvent();} catch(ex){}
            }
            window.close();
        }
    }
    catch(ex)
    {
        try
        {
            window.close();
        }
        catch(ex){}
    }
}