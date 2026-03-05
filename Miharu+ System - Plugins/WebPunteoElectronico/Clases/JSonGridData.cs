//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Data;

//namespace WebPunteoElectronico
//{
//    public class JSonGridData
//    {
//        public static JSonGridData Instance
//        {
//            get
//            {
//                return new JSonGridData();
//            }
//        }

//        public string GenerateJsonGridData(SlygFlexigrid nGrid)
//        {
//            var rows = new rowcollection() { page = nGrid.PageNumber };
//            if (nGrid.DataSource != null)
//            {
//                var sort = (nGrid.SortColumnName == "") ? "" : nGrid.SortColumnName + ((nGrid.SortAscendant) ? "" : " DESC");

//                DataRow[] gridRows = null;

//                var sel = "";
//                foreach (var k in nGrid.Filters.Keys)
//                {
//                    var val = GetFilterValue(nGrid, k);
//                    if (val != "")
//                    {
//                        if (sel != "") sel += " AND ";
//                        sel += k + val;
//                    }
//                }

//                if (sort == "" && sel == "")
//                {
//                    gridRows = new DataRow[nGrid.DataSource.Rows.Count];
//                    nGrid.DataSource.Rows.CopyTo(gridRows, 0);
//                }
//                else
//                {
//                    gridRows = nGrid.DataSource.Select(sel, sort);
//                }

//                //rows.total = nGrid.DataSource.Rows.Count;
//                //var start = nGrid.PageSize * (nGrid.PageNumber - 1);
//                //var end = (nGrid.DataSource.Rows.Count < nGrid.PageSize * (nGrid.PageNumber)) ? nGrid.DataSource.Rows.Count : nGrid.PageSize * (nGrid.PageNumber);

//                rows.total = gridRows.Length;
//                var start = nGrid.PageSize * (nGrid.PageNumber - 1);
//                var end = (gridRows.Length < nGrid.PageSize * (nGrid.PageNumber)) ? gridRows.Length : nGrid.PageSize * (nGrid.PageNumber);

//                for (int i = start; i < end; i++)
//                {
//                    var columnId = nGrid.ColModel.FindColumnId();
//                    var rowId = (columnId != "") ? gridRows[i][columnId].ToString() : "";

//                    var r = new rowelement() { id = rowId, cell = new List<string>() };
//                    r.cell.AddRange(GetRowData(gridRows[i], nGrid.ColModel));
//                    rows.rows.Add(r);
//                }
//            }

//            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
//            var jsonresult = serializer.Serialize(rows);

//            return jsonresult;
//        }

//        private string GetFilterValue(SlygFlexigrid nGrid, string k)
//        {
//            DataColumn col = nGrid.DataSource.Columns[k];
//            switch (col.DataType.Name)
//            {
//                case "Int16":
//                case "Int32":
//                case "Int64":
//                case "Decimal":
//                case "Float":
//                case "Double":
//                    try
//                    {
//                        Convert.ChangeType(nGrid.Filters[k], col.DataType);
//                        return " = " + nGrid.Filters[k].ToString();
//                    }
//                    catch { return ""; }
//                default:
//                    return " like '%" + nGrid.Filters[k].ToString() + "%'";
//            }
//        }

//        public string GenerateJsonGridData(DataTable nData, SlygFlexColModel nColModel, int nTotal, int nPageSize, int nPageNumber)
//        {
//            var rows = new rowcollection() { page = nPageNumber, total = nTotal };
//            if (nData != null)
//            {
//                var start = nPageSize * (nPageNumber - 1);
//                var end = (nData.Rows.Count < nPageSize * (nPageNumber)) ? nData.Rows.Count : nPageSize * (nPageNumber);

//                for (int i = start; i < end; i++)
//                {
//                    var columnId = nColModel.FindColumnId();
//                    var rowId = (columnId != "") ? nData.Rows[i][columnId].ToString() : "";
//                    var r = new rowelement() { id = rowId, cell = new List<string>() };
//                    r.cell.AddRange(GetRowData(nData.Rows[i], nColModel));
//                    rows.rows.Add(r);
//                }
//            }

//            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
//            var jsonresult = serializer.Serialize(rows);

//            return jsonresult;
//        }

//        private List<string> GetRowData(DataRow nRow, SlygFlexColModel nColModel)
//        {
//            var rowData = new List<string>();
//            var columns = nRow.Table.Columns;
//            for (int i = 0; i < columns.Count; i++)
//            {
//                var colMap = nColModel.FindColumnMap(columns[i].ColumnName);
//                if (!colMap.Ignore)
//                {
//                    rowData.Add(nRow[i].ToString());
//                }

//            }
//            return rowData;
//        }
//    }
//}


//public class rowcollection
//{
//    public int page { set; get; }
//    public int total { set; get; }

//    public List<rowelement> rows { set; get; }

//    public rowcollection()
//    {
//        page = 0;
//        total = 0;
//        rows = new List<rowelement>();
//    }

//}

//public class rowelement
//{
//    public string id { set; get; }
//    public List<string> cell { set; get; }

//    public rowelement()
//    {
//        id = "";
//        cell = new List<string>();
//    }
//}