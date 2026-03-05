//------------------------------------------------------------------------------
// <copyright author="Eliseo Roa" >
//	Copyright (c) Eliseo.
//  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

function HmtlRowIndex() {
    this.Server = -1;
    this.Client = -1;
}

function ERGridView(varGridTableId, varGridTableServerId, varGridNum, varPreSelectedIndexControlId, varSelectedIndexControlId, varIsConfigureRequiredClientControlId, varIsInitializeRequiredClientControlId, varRowStyleCssClass, varAlternatingStyleCssClass, varPreSelectedStyleCssClass, varSelectedStyleCssClass, varIsPreSelectedDoPostBack, varIsSelectedDoPostBack) {
    this.GridTableId = varGridTableId;
    this.GridTableServerId = varGridTableServerId;
    this.GridNum = varGridNum;

    this.HmtlRowIndexArray = new Array();

    this.LastPreSelectedRow = null;
    this.LastSelectedRow = null;

    this.PreSelectedIndexControlId = varPreSelectedIndexControlId;
    this.SelectedIndexControlId = varSelectedIndexControlId;
    this.IsConfigureRequiredClientControlId = varIsConfigureRequiredClientControlId;
    this.IsInitializeRequiredClientControlId = varIsInitializeRequiredClientControlId;

    this.RowStyleCssClass = varRowStyleCssClass;
    this.AlternatingStyleCssClass = varAlternatingStyleCssClass;
    this.PreSelectedStyleCssClass = varPreSelectedStyleCssClass;
    this.SelectedStyleCssClass = varSelectedStyleCssClass;

    this.isOmitPostBack = false;
    this.isPreSelectedDoPostBack = varIsPreSelectedDoPostBack;
    this.isSelectedDoPostBack = varIsSelectedDoPostBack;

    this.OnBeginPreSelect = null;
    this.OnBeginSelect = null;
    this.OnEndPreSelect = null;
    this.OnEndSelect = null;


    // variables de ordenamiento QuickSort
    this.QuickSort_col = 0;
    this.QuickSort_parent = null;
    this.QuickSort_items = new Array();
    this.QuickSort_N = 0;
}

ERGridView.prototype.PreSelectedIndexChanged = ERGridView_PreSelectedIndexChanged;
ERGridView.prototype.SelectedIndexChanged = ERGridView_SelectedIndexChanged;
ERGridView.prototype.GetRowStyle = ERGridView_GetRowStyle;
ERGridView.prototype.SortColumn = ERGridView_SortColumn;
ERGridView.prototype.Initialize = ERGridView_Initialize;
ERGridView.prototype.Configure = ERGridView_Configure;
ERGridView.prototype.ResetStyles = ERGridView_ResetStyles;
ERGridView.prototype.PreSelectedIndexGet = ERGridView_PreSelectedIndexGet;
ERGridView.prototype.PreSelectedIndexSet = ERGridView_PreSelectedIndexSet;
ERGridView.prototype.SelectedIndexGet = ERGridView_SelectedIndexGet;
ERGridView.prototype.SelectedIndexSet = ERGridView_SelectedIndexSet;
ERGridView.prototype.IsConfigureRequiredGet = ERGridView_IsConfigureRequiredGet;
ERGridView.prototype.IsConfigureRequiredSet = ERGridView_IsConfigureRequiredSet;
ERGridView.prototype.IsInitializeRequiredGet = ERGridView_IsInitializeRequiredGet;
ERGridView.prototype.IsInitializeRequiredSet = ERGridView_IsInitializeRequiredSet;
ERGridView.CreateNew = ERGridView_CreateNew;

ERGridView.prototype.QuickSort_get = ERGridView_QuickSort_get;
ERGridView.prototype.QuickSort_compare = ERGridView_QuickSort_compare;
ERGridView.prototype.QuickSort_SwapRows = ERGridView_QuickSort_SwapRows;
ERGridView.prototype.QuickSort_SwapHtmlRows = ERGridView_QuickSort_SwapHtmlRows;
ERGridView.prototype.QuickSort_exchange = ERGridView_QuickSort_exchange;
ERGridView.prototype.QuickSort_quicksort = ERGridView_QuickSort_quicksort;
ERGridView.prototype.QuickSort_sortTable = ERGridView_QuickSort_sortTable;


function ERGridView_CreateNew(varGridTableId, varGridTableServerId, varGridNum, varPreSelectedIndexControlId, varSelectedIndexControlId, varIsConfigureRequiredClientControlId, varIsInitializeRequiredClientControlId, varRowStyleCssClass, varAlternatingStyleCssClass, varPreSelectedStyleCssClass, varSelectedStyleCssClass, varIsPreSelectedDoPostBack, varIsSelectedDoPostBack) {
    var grd = new ERGridView(varGridTableId, varGridTableServerId, varGridNum, varPreSelectedIndexControlId, varSelectedIndexControlId, varIsConfigureRequiredClientControlId, varIsInitializeRequiredClientControlId, varRowStyleCssClass, varAlternatingStyleCssClass, varPreSelectedStyleCssClass, varSelectedStyleCssClass, varIsPreSelectedDoPostBack, varIsSelectedDoPostBack);
    return grd;
}

function ERGridView_Initialize(sender, args) {
    if (this.IsInitializeRequiredGet()) {
        var index = 0;

        this.HmtlRowIndexArray.length = 0;

        var prtTable = document.getElementById(this.GridTableId);
        if (prtTable != null) {
            var rows = prtTable.getElementsByTagName('tr');
            for (index = 0; index < rows.length; index++) {
                //rows[index].setAttribute("ServerIndex",index-1); 
                this.HmtlRowIndexArray[index] = new HmtlRowIndex();
                this.HmtlRowIndexArray[index].Server = index - 1;
                this.HmtlRowIndexArray[index].Client = index - 1;
            }

            this.IsInitializeRequiredSet(false);
        }
    }
}

function ERGridView_Configure(sender, args) {
    if (this.IsInitializeRequiredGet())
        this.Initialize(sender, args);

    if (this.IsConfigureRequiredGet()) {
        if (!document.getElementsByTagName || !document.createTextNode) return;
        var prtTable = document.getElementById(this.GridTableId);
        if (prtTable != null) {
            var rows = prtTable.getElementsByTagName('tr');

            var preIndex = this.PreSelectedIndexGet();
            var selIndex = this.SelectedIndexGet();
            var index = 0;

            this.isOmitPostBack = true;
            if (preIndex == -1)
                this.PreSelectedIndexChanged(null, preIndex)
            else
                this.PreSelectedIndexChanged(rows[preIndex + 1], preIndex)

            if (selIndex == -1)
                this.SelectedIndexChanged(null, selIndex)
            else
                this.SelectedIndexChanged(rows[selIndex + 1], selIndex)
            this.isOmitPostBack = false;

            var rows = document.getElementById(this.GridTableId).getElementsByTagName('tr');
            for (index = 1; index < this.HmtlRowIndexArray.length; index++) {
                try {
                    if (parseInt(rows[index].attributes["ServerIndex"].nodeValue) != this.HmtlRowIndexArray[index].Server) {
                        this.QuickSort_SwapHtmlRows(rows[index], rows[this.HmtlRowIndexArray[index].Server + 1]);
                    }
                } catch (ex) { }
            }
            this.ResetStyles();

            this.IsConfigureRequiredSet(false);
        }
    }
}

function ERGridView_PreSelectedIndexChanged(ptrRow, serverIndex) {
    if (this.OnBeginPreSelect) if (!this.OnBeginPreSelect(serverIndex)) return;

    try {
        if (this.LastPreSelectedRow != null) {
            if (this.LastSelectedRow == null || this.LastPreSelectedRow.rowIndex != this.LastSelectedRow.rowIndex) {
                this.LastPreSelectedRow.className = this.GetRowStyle(this.LastPreSelectedRow.rowIndex);
            } else {
                this.LastPreSelectedRow.className = this.SelectedStyleCssClass;
            }
        }

        if (ptrRow != null)
            ptrRow.className = this.PreSelectedStyleCssClass;

        this.LastPreSelectedRow = ptrRow;
        this.PreSelectedIndexSet(serverIndex);

        if (this.isPreSelectedDoPostBack && !this.isOmitPostBack) {
            __doPostBack(this.GridTableServerId, 'Select$' + serverIndex);
        }

        if (this.OnEndPreSelect) this.OnEndPreSelect(serverIndex);
    }
    catch (ex) {
        alert(ex);
    }
}

function ERGridView_SelectedIndexChanged(ptrRow, serverIndex) {
    if (this.OnBeginSelect) if (!this.OnBeginSelect(serverIndex)) return;

    try {
        this.PreSelectedIndexChanged(ptrRow, serverIndex);

        if (ptrRow != null)
            ptrRow.className = this.SelectedStyleCssClass;
        this.LastSelectedRow = ptrRow;
        this.SelectedIndexSet(serverIndex);

        if (this.isSelectedDoPostBack && !this.isOmitPostBack) {
            __doPostBack(this.GridTableServerId, 'Select$' + serverIndex);
        }

        if (this.OnEndSelect) this.OnEndSelect(serverIndex);
    }
    catch (ex) {
        alert(ex);
    }
}

function ERGridView_SortColumn(colNum) {
    if (!document.getElementsByTagName || !document.createTextNode) return;
    var rows = document.getElementById(this.GridTableId).getElementsByTagName('tr');

    try { document.getElementById('div_Bloquear').style.display = 'inline'; } catch (ex) { }

    if (rows.length < 20 || confirm("¿Ordenar ascedentemente por la columna " + rows[0].cells[colNum].innerHTML + "? ")) {
        this.QuickSort_sortTable(this.GridTableId, colNum, false);
        this.ResetStyles();
    }
    try { document.getElementById('div_Bloquear').style.display = 'none'; } catch (ex) { }
}

//function ERGridView_CompareText(text1,text2)
//{
//    var str1 = new String();
//    var str2 = new String();
//    str1 = text1.toString() ;
//    str2 = text2.toString();
//    var max = (str1.length < str2.length ) ?  str1.length : str2.length;
//    for( i = 0; i<max; i++ )
//    {
//        if(str1.charAt(i) > str2.charAt(i))
//        {
//            return true;
//        }
//        else if(str1.charAt(i) < str2.charAt(i))
//        {
//            return false;
//        }
//    }
//    
//    if( str1.length > str2.length )
//        return true;
//    else
//        return false;
//}

function ERGridView_GetRowStyle(tableIndex) {
    if ((tableIndex % 2) == 0)
        return this.AlternatingStyleCssClass;
    else
        return this.RowStyleCssClass;
}

function ERGridView_ResetStyles() {
    var index = 1;
    var rows = document.getElementById(this.GridTableId).getElementsByTagName('tr');
    for (index = 1; index < rows.length; index++)
        rows[index].className = this.GetRowStyle(index);

    var preIndex = this.PreSelectedIndexGet();
    if (preIndex != -1) {
        for (index = 1; index < rows.length; index++) {
            if (rows[index].attributes["ServerIndex"].nodeValue == preIndex) {
                this.LastPreSelectedRow = rows[index];
                this.LastPreSelectedRow.className = this.PreSelectedStyleCssClass;
                break;
            }
        }
    }

    var selIndex = this.SelectedIndexGet();
    if (selIndex != -1) {
        for (index = 1; index < rows.length; index++) {
            if (rows[index].attributes["ServerIndex"].nodeValue == selIndex) {
                this.LastSelectedRow = rows[index];
                this.LastSelectedRow.className = this.SelectedStyleCssClass;
                break;
            }
        }
    }
}

function ERGridView_PreSelectedIndexGet() {
    var value = -1;
    try {
        value = parseInt(document.getElementById(this.PreSelectedIndexControlId).value);
    }
    catch (ex) {
        value = -1;
    }
    return value;
}

function ERGridView_PreSelectedIndexSet(value) {
    try {
        document.getElementById(this.PreSelectedIndexControlId).value = value;
    }
    catch (ex) { }
}

function ERGridView_SelectedIndexGet() {
    var value = -1;
    try {
        value = parseInt(document.getElementById(this.SelectedIndexControlId).value);
    }
    catch (ex) {
        value = -1;
    }
    return value;
}

function ERGridView_SelectedIndexSet(value) {
    try {
        document.getElementById(this.SelectedIndexControlId).value = value;
    }
    catch (ex) { }
}

function ERGridView_IsConfigureRequiredGet() {
    var value = false;
    try {
        var ctr = document.getElementById(this.IsConfigureRequiredClientControlId);
        if (ctr.value.toUpperCase() == "TRUE")
            value = true;
    }
    catch (ex) {
        value = false;
    }
    return value;
}

function ERGridView_IsConfigureRequiredSet(value) {
    try {
        if (value == true)
            document.getElementById(this.IsConfigureRequiredClientControlId).value = "True";
        else
            document.getElementById(this.IsConfigureRequiredClientControlId).value = "False";
    }
    catch (ex) { }
}

function ERGridView_IsInitializeRequiredGet() {
    var value = false;
    try {
        var ctr = document.getElementById(this.IsInitializeRequiredClientControlId);
        if (ctr.value.toUpperCase() == "TRUE")
            value = true;
    }
    catch (ex) {
        value = false;
    }
    return value;
}

function ERGridView_IsInitializeRequiredSet(value) {
    try {
        if (value == true)
            document.getElementById(this.IsInitializeRequiredClientControlId).value = "True";
        else
            document.getElementById(this.IsInitializeRequiredClientControlId).value = "False";
    }
    catch (ex) { }
}


//modificacion de http://www.the-art-of-web.com/javascript/quicksort/

function ERGridView_QuickSort_get(i) {
    var node = this.QuickSort_items[i].getElementsByTagName("TD")[this.QuickSort_col];
    if (node.childNodes.length == 0) return "";
    var retval = node.firstChild.nodeValue;
    if (parseInt(retval) == retval) return parseInt(retval);
    return retval;
}

function ERGridView_QuickSort_compare(val1, val2, desc) {
    return (desc) ? val1 > val2 : val1 < val2;
}

function ERGridView_QuickSort_SwapRows(r1, r2, index1, index2) {
    this.QuickSort_SwapHtmlRows(r1, r2);

    var srv = this.HmtlRowIndexArray[index1].Server;
    this.HmtlRowIndexArray[index1].Server = this.HmtlRowIndexArray[index2].Server;
    this.HmtlRowIndexArray[index2].Server = srv;
}

function ERGridView_QuickSort_SwapHtmlRows(r1, r2) {
    var bP = r2.parentNode;
    var bS = r2.nextSibling;
    r1.parentNode.insertBefore(r2, r1.nextSibling);
    bP.insertBefore(r1, bS);
}

function ERGridView_QuickSort_exchange(i, j) {
    if (i != j)
        this.QuickSort_SwapRows(this.QuickSort_items[i], this.QuickSort_items[j], i, j);
}

function ERGridView_QuickSort_quicksort(m, n, desc) {
    if (n <= m + 1) return;

    if ((n - m) == 2) {
        if (this.QuickSort_compare(this.QuickSort_get(n - 1), this.QuickSort_get(m), desc)) this.QuickSort_exchange(n - 1, m);
        return;
    }

    i = m + 1;
    j = n - 1;

    if (this.QuickSort_compare(this.QuickSort_get(m), this.QuickSort_get(i), desc)) this.QuickSort_exchange(i, m);
    if (this.QuickSort_compare(this.QuickSort_get(j), this.QuickSort_get(m), desc)) this.QuickSort_exchange(m, j);
    if (this.QuickSort_compare(this.QuickSort_get(m), this.QuickSort_get(i), desc)) this.QuickSort_exchange(i, m);

    pivot = this.QuickSort_get(m);

    while (true) {
        j--;
        while (this.QuickSort_compare(pivot, this.QuickSort_get(j), desc)) j--;
        i++;
        while (this.QuickSort_compare(this.QuickSort_get(i), pivot, desc)) i++;
        if (j <= i) break;
        this.QuickSort_exchange(i, j);
    }

    this.QuickSort_exchange(m, j);

    if ((j - m) < (n - j)) {
        this.QuickSort_quicksort(m, j, desc);
        this.QuickSort_quicksort(j + 1, n, desc);
    } else {
        this.QuickSort_quicksort(j + 1, n, desc);
        this.QuickSort_quicksort(m, j, desc);
    }
}

function ERGridView_QuickSort_sortTable(tableid, n, desc) {
    this.QuickSort_parent = document.getElementById(tableid);
    this.QuickSort_col = n;

    this.QuickSort_items = document.getElementById(tableid).getElementsByTagName('tr');
    this.QuickSort_N = this.QuickSort_items.length;

    // quick sort
    this.QuickSort_quicksort(1, this.QuickSort_N, desc);
}
