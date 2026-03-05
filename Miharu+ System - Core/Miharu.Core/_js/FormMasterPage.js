function BloquearMaster() {
    try {
        document.getElementById(window.window.ctrId_div_Bloquear).style.display = 'inline';
    } catch (ex) { }
}

function DesbloquearMaster() {
    try {
        document.getElementById(window.ctrId_div_Bloquear).style.display = 'none';
    } catch (ex) { }
}

function BeginRequestHandler() {
    //    var Action = document.getElementById(window.ctrId_EndRequestAction).value;
    //    var arrAction = Action.split("|");
    //    var unlock = false;
    //    for( var a = 0; a<arrAction.length; a++)
    //    {
    //        if(arrAction[a] == "Unlock")
    //            unlock = true;
    //    }
    //    
    //    if (unlock == false )
    //        BloquearMaster();
    parent.BloquearMaster();
}

function pageLoadedHandler() {
    var Action = document.getElementById(window.ctrId_EndRequestAction).value;
    var Message = document.getElementById(window.ctrId_EndRequestMessage).value;
    document.getElementById(window.ctrId_EndRequestAction).value = "";
    document.getElementById(window.ctrId_EndRequestMessage).value = "";

    var arrAction = Action.split("|");
    var arrMessage = Message.split("|");

    for (var a = 0; a < arrAction.length; a++) {
        if (arrAction[a] == "Close") {
            window.close();
            return;
        }
        else if (arrAction[a] == "ModalPopup") {
        showPopup(arrMessage[a]);
            parent.DesbloquearMaster();
            return;
        }
        else if (arrAction[a] == "ShowDialog") {
            parent.showDialog(arrMessage[a], window);
            return;
        }
        else if (arrAction[a] == "Message") {
            alert(arrMessage[a]);
        }
        else if (arrAction[a] == "SetActiveTabIndex") {
            set_activeTabIndex(arrMessage[a]);
        }
    }

    parent.DesbloquearMaster();
}

function showPopup(pars) {
    window.arrVariables = pars.split("&amp;");
    window.arrVariableActual = window.arrVariables[0].split(";");

    //BloquearMaster();
    window.open(window.arrVariableActual[0], window.arrVariableActual[1], window.arrVariableActual[2]);
}

var wd = null;

//Esta funcion se ejecuta en main master page
//function showDialogHijo(pars)
//{
//    arrVariables = pars.split("&amp;");
//    arrVariableActual = arrVariables[0].split(";");
//    
//    var strUrl = arrVariableActual[0];
//    var strNombre = arrVariableActual[1];
//    var strCadena = arrVariableActual[2];
//    var strTitle = arrVariableActual[3];
//    
//    var options =  strCadena.split(",");
//    var valWidth = parseInt(options[0].split("=")[1]);
//    var valHeight = parseInt(options[1].split("=")[1]);
//    var valLeft = parseInt(options[2].split("=")[1]);
//    var valTop = parseInt(options[3].split("=")[1]);
//	
//    BloquearMaster();
//	/*id: "WndDialog_1",*/
//    wd = new Window({ url: strUrl, className: "alphacube", title:strTitle , width: valWidth, height: valHeight , top: valTop, left: valLeft });
//    wd.setZIndex(101);
//    //wd.setDestroyOnClosed();
//    wd.setCloseCallback(WindowDialogOnClosed);
//    wd.show();
//}

//function WindowDialogOnClosed()
//{
//    DesbloquearMaster();
//    wd.destroy();
//    wd = null;
//    return true;
//}

//function WindowDialogClose(result)
//{
//    wd.close();
//    if( result )
//    {
//        __doPostBack(window.ctrId_btnModalClosed,'');
//    }
//}

//function modalClosed( params )
//{
//    DesbloquearMaster();
//    __doPostBack(window.ctrId_btnModalClosed,'');
//}

function doPostBack_ModalClosed() {
    window.__doPostBack(window.ctrId_btnModalClosed, '');
}

function set_activeTabIndex(value) {
    try {
        window.$find(window.ctrId_TabContainer).set_activeTabIndex(parseInt(value));
        /* cambio realizado por simon ariza 2010-02-26 (se deshabilita la configuracion del toolbox en el evento de cambio de pestañas)
        ConfigureTool_TabSelected(window.ctrId_ToolControl, parseInt(value));*/
    }
    catch (ex) { }
}

function OnPreselectMasterGrid(selectedIndex) {
    var tabIndex = window.$find(window.ctrId_TabContainer).get_activeTabIndex();
    ConfigureTool_GridSelected(window.ctrId_ToolControl, selectedIndex, tabIndex);
}

function IsCommandNameInArray(CommandName, Array) {
    for (var j = 0; j < Array.length; j++) {
        if (CommandName == Array[j]) {
            return true;
        }
    }
    return false;
}

function ConfigureTool_TabSelected(toolId, TabIndex) {
    //var EnabledCommandsControl = document.getElementById(window.ctrId_EnabledCommands);
    var ListCommandsControl = document.getElementById(window.ctrId_ListCommands);
    var DetailCommandsControl = document.getElementById(window.ctrId_DetailCommands);
    //var UniqueCommandsControl = document.getElementById(window.ctrId_UniqueCommands);

    var ToolContainerControl = document.getElementById(window.ctrId_ToolContainer);
    var tdArray;
    var index;
    var divAction;
    var img;
    var divDisabled;
    if (TabIndex == 0) //Filter
    {
        var FilterCommandsControl = document.getElementById(window.ctrId_FilterCommands);
        var FilterCommands = FilterCommandsControl.value.split(",");
        tdArray = ToolContainerControl.getElementsByTagName("TD");
        index = 0;
        for (index = 0; index < tdArray.length; index++) {
            divAction = tdArray[index].childNodes[1];
            if (divAction.attributes != null)
                if (divAction.attributes["ActionName"]) {
                    img = divAction.getElementsByTagName("input")(0);
                    divDisabled = divAction.getElementsByTagName("div")(0);
                    if (IsCommandNameInArray(divAction.attributes["ActionName"].nodeValue, FilterCommands)) {
                        divDisabled.style.display = 'none';
                        img.disabled = null;
                    } else {
                        divDisabled.style.display = 'inline-block';
                        img.disabled = true;

                    }
                }
        }
    }

    if (TabIndex == 1) //List
    {
        ListCommandsControl = document.getElementById(window.ctrId_ListCommands);
        var ListCommands = ListCommandsControl.value.split(",");
        tdArray = ToolContainerControl.getElementsByTagName("TD");
        index = 0;
        for (index = 0; index < tdArray.length; index++) {
            divAction = tdArray[index].childNodes[1];
            if (divAction.attributes != null)
            if (divAction.attributes["ActionName"]) {
                img = divAction.getElementsByTagName("input")(0);
                divDisabled = divAction.getElementsByTagName("div")(0);
                var enabledTool = false;

                if (IsCommandNameInArray(divAction.attributes["ActionName"].nodeValue, ListCommands)) {
                    enabledTool = true;
                    //                    if( divAction.attributes["ActionName"].nodeValue == "Edit")
                    //                    {
                    //                        if(IsGridSelected == false)
                    //                        {
                    //                            enabledTool = false;
                    //                        }
                    //                    }
                }

                if (enabledTool) {
                    divDisabled.style.display = 'none';
                    img.disabled = null;
                }
                else {
                    divDisabled.style.display = 'inline-block';
                    img.disabled = true;
                }
            }
        }
    }

    if (TabIndex == 2) //Detail
    {
        DetailCommandsControl = document.getElementById(window.ctrId_DetailCommands);
        var DetailCommands = DetailCommandsControl.value.split(",");
        tdArray = ToolContainerControl.getElementsByTagName("TD");
        index = 0;
        for (index = 0; index < tdArray.length; index++) {
            divAction = tdArray[index].childNodes[1];
            if (divAction.attributes != null)
            if (divAction.attributes["ActionName"]) {
                img = divAction.getElementsByTagName("input")(0);
                divDisabled = divAction.getElementsByTagName("div")(0);
                if (IsCommandNameInArray(divAction.attributes["ActionName"].nodeValue, DetailCommands)) {
                    divDisabled.style.display = 'none';
                    img.disabled = null;
                } else {
                    divDisabled.style.display = 'inline-block';
                    img.disabled = true;
                }
            }
        }
    }
}

function ConfigureTool_GridSelected(toolId, selectedIndex, tabIndex) {
    //    var tools = document.getElementById(toolId);
    //    var divArray = tools.getElementsByTagName("div");
    //    
    //    var index = 0;
    //    
    //    for( index=0; index<divArray.length; index++)
    //    {
    //        var div = divArray[index];
    //        if( div.attributes["ActionName"] )
    //        {
    //            //alert(div.attributes["ActionName"]);
    //            var btn = div.parentNode.getElementsByTagName("input");
    //            if( div.attributes["ActionName"].nodeValue == "Edit" )
    //            {
    //                IsGridSelected = ( selectedIndex != -1);
    //                
    //                if( IsGridSelected && tabIndex == 1 )
    //                {
    //                    div.style.display = 'none';
    //                    btn[0].disabled = null;
    //                }
    //                else
    //                {
    //                    div.style.display = 'inline-block';
    //                    btn[0].disabled = true;
    //                }
    //            }
    //        }
    //    }
}


