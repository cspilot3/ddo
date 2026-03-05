function BloquearMaster()
{
    try {        
        document.getElementById(ctrId_div_Bloquear).style.display = 'inline';
    }catch(ex){}
}

function DesbloquearMaster()
{
    try
    {
        document.getElementById(ctrId_div_Bloquear).style.display = 'none';
    }catch(ex){}
}

function BeginRequestHandler()
{
    BloquearMaster();
}

function pageLoadedHandler()
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
            window.close();
            return;
        }
        else if (arrAction[a] == "ModalPopup")
        {
            showPopup(arrMessage[a])
            return;
        }
        else if (arrAction[a] == "ShowDialog")
        {
            showDialog(arrMessage[a]);
            return;
        }
        else if(arrAction[a] == "Message")
        {
            alert(arrMessage[a]);
        }
        else if(arrAction[a] == "SetActiveTabIndex")
        {
            set_activeTabIndex(arrMessage[a]);
        }
    }

    DesbloquearMaster();
}

function showPopup(pars)
{
    arrVariables = pars.split("&amp;");
    arrVariableActual = arrVariables[0].split(";");

    BloquearMaster();
    window.open(arrVariableActual[0],arrVariableActual[1],arrVariableActual[2]);   
}

var wd = null;
var FormMasterPage = null;

function showDialog(pars,formPage)
{
    FormMasterPage = formPage;
    
    arrVariables = pars.split("&amp;");
    arrVariableActual = arrVariables[0].split(";");
    
    var strUrl = arrVariableActual[0];
    var strNombre = arrVariableActual[1];
    var strCadena = arrVariableActual[2];
    var strTitle = arrVariableActual[3];
    
    var options =  strCadena.split(",");
    var valWidth = parseInt(options[0].split("=")[1]);
    var valHeight = parseInt(options[1].split("=")[1]);
    var valLeft = parseInt(options[2].split("=")[1]);
    var valTop = parseInt(options[3].split("=")[1]);
	
    BloquearMaster();
	/*id: "WndDialog_1",*/
    wd = new Window({ url: strUrl, className: "alphacube", title:strTitle , width: valWidth, height: valHeight , top: valTop, left: valLeft });
    wd.setZIndex(101);
    //wd.setDestroyOnClosed();
    wd.setCloseCallback(WindowDialogOnClosed);
    wd.show();
}

function WindowDialogOnClosed()
{
    DesbloquearMaster();
    wd.destroy();
    wd = null;
    return true;
}

function WindowDialogClose(result)
{
    wd.close();
    if( result )
    {
        if( FormMasterPage != null )
        {
            FormMasterPage.doPostBack_ModalClosed();
        }
        //__doPostBack(ctrId_btnModalClosed,'');
    }
}

function modalClosed( params )
{
    DesbloquearMaster();
    if( FormMasterPage != null )
    {
        FormMasterPage.doPostBack_ModalClosed();
    }
    //__doPostBack(ctrId_btnModalClosed,'');
}

function set_activeTabIndex(value) 
{
    try
    {
        $find(ctrId_TabContainer).set_activeTabIndex(parseInt(value));
        ConfigureTool_TabSelected(ctrId_ToolControl, parseInt(value));
    }
    catch(ex){}
}

function OnPreselectMasterGrid(selectedIndex)
{
    var tabIndex = $find(ctrId_TabContainer).get_activeTabIndex();
    ConfigureTool_GridSelected(ctrId_ToolControl,selectedIndex,tabIndex);
}

function ConfigureTool_TabSelected(toolId, TabIndex)
{
    var tools = document.getElementById(toolId);
    var divArray = tools.getElementsByTagName("div");
    
    var index = 0;
    
    for( index=0; index<divArray.length; index++)
    {
        var div = divArray[index];
        if( div.attributes["ActionName"] )
        {
            var btn = div.parentNode.getElementsByTagName("input");
            if( div.attributes["ActionName"].nodeValue == "Find" )
            {
                if( TabIndex == 0 )
                {
                    div.style.display = 'none';
                    btn[0].disabled = null;
                }else{
                    div.style.display = 'inline-block';
                    btn[0].disabled = true;
                }
            }
            if( div.attributes["ActionName"].nodeValue == "Edit" )
            {
                if( TabIndex == 1 && IsGridSelected  )
                {
                    div.style.display = 'none';
                    btn[0].disabled = null;
                }else{
                    div.style.display = 'inline-block';
                    btn[0].disabled = true;
                }
            }
            if( div.attributes["ActionName"].nodeValue == "Delete" )
            {
                if( TabIndex == 2 )
                {
                    div.style.display = 'none';
                    btn[0].disabled = null;
                }else{
                    div.style.display = 'inline-block';
                    btn[0].disabled = true;
                }
            }
            if( div.attributes["ActionName"].nodeValue == "Save" )
            {
                if( TabIndex == 2 )
                {
                    div.style.display = 'none';
                    btn[0].disabled = null;
                }else{
                    div.style.display = 'inline-block';
                    btn[0].disabled = true;
                }
            }
        }
    }
}

function ConfigureTool_GridSelected(toolId, selectedIndex,tabIndex)
{
    var tools = document.getElementById(toolId);
    var divArray = tools.getElementsByTagName("div");
    
    var index = 0;
    
    for( index=0; index<divArray.length; index++)
    {
        var div = divArray[index];
        if( div.attributes["ActionName"] )
        {
            //alert(div.attributes["ActionName"]);
            var btn = div.parentNode.getElementsByTagName("input");
            if( div.attributes["ActionName"].nodeValue == "Edit" )
            {
                IsGridSelected = ( selectedIndex != -1);
                
                if( IsGridSelected && tabIndex == 1 )
                {
                    div.style.display = 'none';
                    btn[0].disabled = null;
                }
                else
                {
                    div.style.display = 'inline-block';
                    btn[0].disabled = true;
                }
            }
        }
    }
}


