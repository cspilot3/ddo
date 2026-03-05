function BeginRequestHandler2()
{
    BloquearMasterPopup();
}

function pageLoadedHandler2()
{
    var Action = document.getElementById(ctrId_EndRequestAction).value;
    var Message = document.getElementById(ctrId_EndRequestMessage).value;
    document.getElementById(ctrId_EndRequestAction).value = "";
    document.getElementById(ctrId_EndRequestMessage).value = "";
    
    var arrAction = Action.split("|");
    var arrMessage = Message.split("|");
    
    for( var a = 0; a<arrAction.length; a++)
    {
        if(arrAction[a] == "Close")
        {
            DlgClose(arrMessage[a]);
            return;
        }
        else if(arrAction[a] == "Message")
        {
            alert(arrMessage[a]);
        }
        else if(arrAction[a] == "ParentRedirect")
        {
            ParentRedirect( arrMessage[a].split("$")[0] , arrMessage[a].split("$")[1] )
        }
        else if(arrAction[a] == "BloquearMasterPopup")
        {
            BloquearMasterPopup();
        }
    }

    DesbloquearMasterPopup();
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

function PopupClosed()
{
    try
    {
        window.opener.modalClosed('');
    }
    catch(ex)
    {
    }
}

function InitPage()
{
    try
    {
        document.getElementById(ctrId_tbl_Bloquear).style.height = GetHeight() - 1;
    }
    catch(ex){}
}    

function GetHeight()
{
    try
    {
        var y = 0;
        if (self.innerHeight) { 
            y = self.innerHeight; 
        }else if (document.documentElement && document.documentElement.clientHeight) {
            y = document.documentElement.clientHeight; 
        }else if (document.body){
            y = document.body.clientHeight;
        }
        return y;
    }
    catch(ex)
    {
        return 1;
    }
}

function BloquearMasterPopup()
{
    try
    {
        document.getElementById(ctrId_tbl_Bloquear).style.display = 'inline';
    }catch(ex){}
}

function DesbloquearMasterPopup()
{
    try
    {
        document.getElementById(ctrId_tbl_Bloquear).style.display = 'none';
    }catch(ex){}
}

function DisableAndGo(ctr)
{
    ctr.disabled = true;
    __doPostBack(ctr.name,'');
    return true;
}

function ParentRedirect( relativeUrl , paramsUrl )
{
    try
    {
        window.parent.Redirect(relativeUrl , paramsUrl);
    }
    catch(ex)
    {
        try
        {
            window.opener.Redirect(relativeUrl , paramsUrl);
        }
        catch(ex){}
    }
}