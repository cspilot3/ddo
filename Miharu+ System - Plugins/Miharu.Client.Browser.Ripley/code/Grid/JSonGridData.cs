using System;
using System.Data;
using System.Text;
using System.Collections.Generic;

/*
 * Fecha modificacion: 2012-06-29
 * Autor: Eliseo Roa
 */
namespace  Miharu.Client.Browser.code.Grid
{
    public class JSonGridData
    {
        #region Propiedades

        public static JSonGridData Instance
        {
            get { return new JSonGridData(); }
        }

        #endregion

        #region Funciones

        public string GenerateJsonGridData(int nTotal, int nPageSize, int nPageNumber)
        {
            var rows = new rowcollection() { page = nPageNumber, total = nTotal };

            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            return "{\"gridData\":" + serializer.Serialize(rows) + "}";
        }

        public string GenerateJsonGridData(SlygFlexigrid nGrid)
        {
            var strData = "{\"gridData\":" + GetJsonDataSource(nGrid) + ",";
            strData += "\"filterLists\":" + GetFilterLists(nGrid) + "}";

            return strData;
        }

        public string GenerateJsonErroData(string nErrorMessage)
        {
            return "{\"error\":\"" + new ScriptBuilder().CleanScript(nErrorMessage) + "\"}";
        }

        private string GetJsonDataSource(SlygFlexigrid nGrid)
        {
            var rows = new rowcollection() { page = nGrid.PageNumber };
            if (nGrid.DataSource != null)
            {
                DataRow[] gridRows = nGrid.GetRows();

                rows.total = gridRows.Length;
                var start = nGrid.PageSize * (nGrid.PageNumber - 1);
                var end = (gridRows.Length < nGrid.PageSize * (nGrid.PageNumber)) ? gridRows.Length : nGrid.PageSize * (nGrid.PageNumber);

                for (int i = start; i < end; i++)
                {
                    var columnId = nGrid.ColModel.FindColumnId();
                    var rowId = (columnId != "") ? gridRows[i][columnId].ToString() : "";

                    var r = new rowelement() { id = rowId, cell = new List<string>() };
                    r.cell.AddRange(GetRowData(gridRows[i], nGrid));
                    rows.rows.Add(r);
                }
            }

            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            return serializer.Serialize(rows);
        }

        private string GetFilterLists(SlygFlexigrid nGrid)
        {
            var sb = new StringBuilder();

            if (nGrid.DataSource != null)
            {
                foreach (DataColumn col in nGrid.DataSource.Columns)
                {
                    var colMap = nGrid.ColModel.FindColumnMap(col.ColumnName);
                    if (colMap.IsFilterList)
                    {
                        var list = GetColumnList(nGrid, col);
                        if (list != "")
                        {
                            if (sb.Length > 0) sb.Append(",");
                            sb.Append("{\"list\" : \"f_In" + nGrid.Secuencia + col.ColumnName + "\" , \"listData\": [" + list + "]}");
                        }
                    }
                }
            }

            return "[" + sb + "]";
        }

        private string GetColumnList(SlygFlexigrid nGrid, DataColumn nColumn)
        {
            var list = new List<string>();
            var rows = nGrid.DataSource.Select("", nColumn.ColumnName);
            foreach (DataRow row in rows)
            {
                try
                {
                    var val = "\"" + CleanValue(row[nColumn].ToString()) + "\"";
                    if (val != "\"\"" && !list.Contains(val)) list.Add(val);
                }
                catch (Exception ex)
                {
                    throw new Exception("Se presentó un error en el método GetColumnList, " + ex.Message);
                }
            }

            var sb = new StringBuilder();
            foreach (var item in list)
            {
                if (sb.Length > 0) sb.Append(",");
                sb.Append(item);
            }

            return sb.ToString();
        }

        private IEnumerable<string> GetRowData(DataRow nRow, SlygFlexigrid nGrid)
        {
            var rowData = new List<string>();
            var columns = nRow.Table.Columns;
            if (nGrid.IsMultiCheck)
            {
                rowData.Add(nRow[SlygFlexigrid.CHECK_COLUMN_NAME].ToString().ToLower());
            }
            for (int i = 0; i < columns.Count; i++)
            {
                var colMap = nGrid.ColModel.FindColumnMap(columns[i].ColumnName);
                if (!colMap.Ignore && colMap.ColumnName != SlygFlexigrid.CHECK_COLUMN_NAME)
                {
                    var val = "";
                    var type = nRow.Table.Columns[i].DataType;
                    if (nRow[i] != null)
                    {
                        if (type == typeof(DateTime))
                            val = string.Format(Program.DateFormat, nRow[i]);
                        else if (type == typeof(decimal))
                            val = nRow[i].ToString().Replace(",", ".");
                        else if (type == typeof(bool))
                            val = nRow[i].ToString().ToLower();
                        else
                            val = nRow[i].ToString();
                    }

                    rowData.Add(CleanValue(val));
                }

            }
            return rowData;
        }

        private string CleanValue(string nValue)
        {
            return nValue.Replace("\"", "").Replace("'", "").Replace("\r", "").Replace("\n", "").Replace("\\", "").Trim();
        }

        #endregion
    }
    public class rowcollection
    {
        #region Propiedades

        public int page { set; get; }
        public int total { set; get; }

        public List<rowelement> rows { set; get; }

        #endregion

        #region Constructores

        public rowcollection()
        {
            page = 0;
            total = 0;
            rows = new List<rowelement>();
        }

        #endregion
    }

    public class rowelement
    {
        #region Propiedades

        public string id { set; get; }
        public List<string> cell { set; get; }

        #endregion

        #region Constructores

        public rowelement()
        {
            id = "";
            cell = new List<string>();
        }

        #endregion
    }
}