function ExportJQGridDataToExcel(tableCtrl, excelFilename) {
    //  Export the data from jqGrid into Excel .xlsx file.


    var allJQGridData = $(tableCtrl).jqGrid('getGridParam', 'data');

    var jqgridRowIDs = $(tableCtrl).getDataIDs();                
    var headerData = $(tableCtrl).getRowData(jqgridRowIDs[0]);   


    var columnNames = new Array();       //   "name" 
    var columnHeaders = new Array();     //   Header-Text
    var inx = 0;
    var allColumnNames = $(tableCtrl).jqGrid('getGridParam', 'colNames');


    var bIsMultiSelect = $(tableCtrl).jqGrid('getGridParam', 'multiselect');
    if (bIsMultiSelect) {
        allColumnNames.splice(0, 1);
    }

    for (var headerValue in headerData) {

        var isColumnHidden = $(tableCtrl).jqGrid("getColProp", headerValue).hidden;
        if (!isColumnHidden && headerValue != null) {
            columnNames.push(headerValue);
            columnHeaders.push(allColumnNames[inx]);
        }
        inx++;
    }


    var excelData = '';
    for (var k = 0; k < columnNames.length; k++) {
        excelData += columnHeaders[k] + "\t";
    }
    excelData = removeLastChar(excelData) + "\r\n";

    var cellValue = '';
    for (i = 0; i < allJQGridData.length; i++) {

        var data = allJQGridData[i];

        for (var j = 0; j < columnNames.length; j++) {

            cellValue = '' + data[columnNames[j]];

            if (cellValue == null || cellValue == "undefined" )
                excelData += "\t";
            else {
                if (cellValue.indexOf("a href") > -1) {

                    cellValue = $(cellValue).text();
                }
                cellValue = cellValue.replace(/'/g, "&apos;");

                excelData += cellValue + "\t";
            }
        }
        excelData = removeLastChar(excelData) + "\r\n";
    }

    postAndRedirect("../../../Handlers/ExportGridToExcel.ashx?filename=" + excelFilename, { excelData: excelData });
}

function removeLastChar(str) {

    return str.substring(0, str.length - 1);
}

function postAndRedirect(url, postData) {

    var postFormStr = "<form method='POST' action='" + url + "'>\n";

    for (var key in postData) {
        if (postData.hasOwnProperty(key)) {
            postFormStr += "<input type='hidden' name='" + key + "' value='" + postData[key] + "'></input>";
        }
    }

    postFormStr += "</form>";

    var formElement = $(postFormStr);

    $('body').append(formElement);
    $(formElement).submit();
}

