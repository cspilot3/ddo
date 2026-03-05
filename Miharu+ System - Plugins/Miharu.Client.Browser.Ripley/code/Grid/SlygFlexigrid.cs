using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Web.UI;

/*
 * Fecha modificacion: 2012-06-29
 * Autor: Eliseo Roa
 */

namespace  Miharu.Client.Browser.code.Grid
{
    public sealed class SlygFlexigrid : WebControl
    {
        #region Declaraciones

        public Literal _ContentHtml = null;

        public const string CHECK_COLUMN_NAME = "Check_Multi";

        #endregion

        #region Propiedades

        public string Title { get; set; }
        public string UrlData { get; set; }
        public string OnRowDblClick { get; set; }
        public string OnRowSelected { get; set; }

        public string SessionCacheID
        {
            get { return "SlygFlexigrid_" + this.ID; }
        }

        public bool GenerateByColModel
        {
            get { return (bool)HttpContext.Current.Session[SessionCacheID + "GenerateByColModel"]; }
            set { HttpContext.Current.Session[SessionCacheID + "GenerateByColModel"] = value; }
        }

        public DataTable DataSource
        {
            get { return (DataTable)HttpContext.Current.Session[SessionCacheID + "DataSource"]; }
            set { SetDataSource(value); }
        }

        public SlygFlexColModel ColModel
        {
            get { return (SlygFlexColModel)HttpContext.Current.Session[SessionCacheID + "ColModel"]; }
            set { HttpContext.Current.Session[SessionCacheID + "ColModel"] = value; }
        }

        public int SecuenciaGlobal
        {
            get { return (int)HttpContext.Current.Session["SecuenciaGlobal"]; }
            set { HttpContext.Current.Session["SecuenciaGlobal"] = value; }
        }

        public int Secuencia
        {
            get { return (int)HttpContext.Current.Session[SessionCacheID + "Secuencia"]; }
            set { HttpContext.Current.Session[SessionCacheID + "Secuencia"] = value; }
        }

        public int PageSize
        {
            get { return (int)HttpContext.Current.Session[SessionCacheID + "PageSize"]; }
            set { HttpContext.Current.Session[SessionCacheID + "PageSize"] = value; }
        }

        public int PageNumber
        {
            get { return (int)HttpContext.Current.Session[SessionCacheID + "PageNumber"]; }
            set { HttpContext.Current.Session[SessionCacheID + "PageNumber"] = value; }
        }

        public string SortColumnName
        {
            get { return (string)HttpContext.Current.Session[SessionCacheID + "SortColumnName"]; }
            set { HttpContext.Current.Session[SessionCacheID + "SortColumnName"] = value; }
        }

        public bool SortAscendant
        {
            get { return (bool)HttpContext.Current.Session[SessionCacheID + "SortAscendant"]; }
            set { HttpContext.Current.Session[SessionCacheID + "SortAscendant"] = value; }
        }

        public string LastFilter
        {
            get { return (string)HttpContext.Current.Session[SessionCacheID + "LastFilter"]; }
            set { HttpContext.Current.Session[SessionCacheID + "LastFilter"] = value; }
        }

        public bool IsMultiCheck
        {
            get { return (bool)HttpContext.Current.Session[SessionCacheID + "IsMultiCheck"]; }
            set { HttpContext.Current.Session[SessionCacheID + "IsMultiCheck"] = value; }
        }

        public bool IgnoreUpdateMulticheckData
        {
            get { return (bool)HttpContext.Current.Session[SessionCacheID + "IgnoreUpdateMulticheckData"]; }
            set { HttpContext.Current.Session[SessionCacheID + "IgnoreUpdateMulticheckData"] = value; }
        }

        #endregion

        #region Constructores

        public SlygFlexigrid()
            : base("div")
        {
            _ContentHtml = new Literal();
            Controls.Add(_ContentHtml);
            Title = "";
            UrlData = "";
        }

        #endregion

        #region Eventos

        protected override void OnInit(EventArgs e)
        {
            if (HttpContext.Current.Session["SecuenciaGlobal"] == null) SecuenciaGlobal = 1;
            else SecuenciaGlobal++;

            if (HttpContext.Current.Session[SessionCacheID + "Secuencia"] == null) Secuencia = SecuenciaGlobal;
            if (HttpContext.Current.Session[SessionCacheID + "PageSize"] == null) PageSize = 15;
            if (HttpContext.Current.Session[SessionCacheID + "PageNumber"] == null) PageNumber = 1;
            if (HttpContext.Current.Session[SessionCacheID + "SortColumnName"] == null) SortColumnName = "";
            if (HttpContext.Current.Session[SessionCacheID + "SortAscendant"] == null) SortAscendant = true;
            if (HttpContext.Current.Session[SessionCacheID + "IsMultiCheck"] == null) IsMultiCheck = false;
            if (HttpContext.Current.Session[SessionCacheID + "IgnoreUpdateMulticheckData"] == null) IgnoreUpdateMulticheckData = true;
            if (HttpContext.Current.Session[SessionCacheID + "GenerateByColModel"] == null) GenerateByColModel = false;

            UpdateMulticheckData(false, false);

            base.OnInit(e);

            if (ColModel == null)
                ColModel = new SlygFlexColModel();
        }

        #endregion

        #region Metodos

        public void UpdateMulticheckData(bool nIsAjaxRequest, bool nClearChecks)
        {
            if (IsMultiCheck)
            {
                if (nClearChecks)
                {
                    foreach (DataRow row in DataSource.Rows)
                        row[CHECK_COLUMN_NAME] = false;
                }
                else
                {
                    if (DataSource != null && !IgnoreUpdateMulticheckData)
                    {
                        var rows = GetCurrentPageRows();
                        for (var i = 0; i < rows.Length; i++)
                        {
                            var checkId = this.ID + "_flex_rowcheck_" + i;
                            var checkValue = HttpContext.Current.Request[checkId];
                            rows[i][CHECK_COLUMN_NAME] = (checkValue != null);
                        }
                    }
                }
                IgnoreUpdateMulticheckData = !nIsAjaxRequest;                
            }
        }

        public void SetDataSource(DataTable nData)
        {
            var innerData = nData;
            if (IsMultiCheck)
            {
                innerData.Columns.Add(CHECK_COLUMN_NAME, typeof(bool));
                foreach (DataRow row in nData.Rows)
                {
                    row[CHECK_COLUMN_NAME] = false;
                }
            }
            HttpContext.Current.Session[SessionCacheID + "DataSource"] = innerData;
        }

        public void SetDataSource(object[] nDataSource)
        {
            var table = new DataTable();
            if (nDataSource != null && nDataSource.Length > 0)
            {
                var dType = nDataSource[0].GetType();
                var fields = dType.GetProperties();
                foreach (var field in fields)
                {
                    table.Columns.Add(field.Name);
                }

                foreach (object data in nDataSource)
                {
                    var row = table.NewRow();

                    foreach (var field in fields)
                    {
                        row[field.Name] = field.GetValue(data, null).ToString();
                    }

                    table.Rows.Add(row);
                }
            }

            DataSource = table;
        }

        public void SetSelectedRows(DataRow[] nRows)
        {
            CLearSelectedRows();

            foreach (DataRow row in nRows)
            {
                row[SlygFlexigrid.CHECK_COLUMN_NAME] = true;
            }
        }

        public void SetSelectedRows(DataRow nRow)
        {
            CLearSelectedRows();

            nRow[SlygFlexigrid.CHECK_COLUMN_NAME] = true;
        }

        public void CLearSelectedRows()
        {
            if (DataSource == null) return;
            foreach (DataRow row in DataSource.Rows)
            {
                row[SlygFlexigrid.CHECK_COLUMN_NAME] = false;
            }
        }

        public void Initialize()
        {
            ColModel = new SlygFlexColModel();
            PageNumber = 1;
            SortColumnName = "";
            SortAscendant = true;
            LastFilter = "";
        }

        public void Initialize(DataTable nDataSource)
        {
            DataSource = nDataSource;
            ColModel = new SlygFlexColModel();
            PageNumber = 1;
            SortColumnName = "";
            SortAscendant = true;
            LastFilter = "";
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            _ContentHtml.Text = GetHtmlFlexigrid();
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), SessionCacheID, GetScriptFlexigrid(), true);
            if (!this.Height.IsEmpty || !this.Width.IsEmpty)
                this.Style.Add("position", "relative");
        }

        #endregion

        #region Funciones

        public DataRow[] GetRows()
        {
            var sort = (SortColumnName == "") ? "" : SortColumnName + ((SortAscendant) ? "" : " DESC");

            DataRow[] gridRows = null;

            var sel = "";
            foreach (var col in ColModel)
            {
                if (col.AdvancedFilterValue != "")
                {
                    var opeVal = GetFilterOperatorAndValue(col);
                    if (opeVal != "")
                    {
                        if (sel != "") sel += " AND ";
                        sel += Convert.ToString(col.ColumnName) + opeVal;
                    }
                }
            }

            if (sort == "" && sel == "")
            {
                gridRows = new DataRow[DataSource.Rows.Count];
                DataSource.Rows.CopyTo(gridRows, 0);
            }
            else
            {
                gridRows = DataSource.Select(sel, sort);
            }

            return gridRows;
        }

        public DataRow[] GetCurrentPageRows()
        {
            DataRow[] gridRows = GetRows();
            var returnRows = new List<DataRow>();

            var start = PageSize * (PageNumber - 1);
            var end = (gridRows.Length < PageSize * (PageNumber)) ? gridRows.Length : PageSize * (PageNumber);

            for (int i = start; i < end; i++)
            {
                returnRows.Add(gridRows[i]);
            }
            return returnRows.ToArray();
        }

        public DataRow[] GetSelectedRows()
        {
            if (DataSource == null) return null;
            return DataSource.Select(SlygFlexigrid.CHECK_COLUMN_NAME + "= true");
        }

        private string GetFilterOperatorAndValue(FlexColumnMap nFilter)
        {
            DataColumn col = DataSource.Columns[nFilter.ColumnName];
            switch (col.DataType.Name)
            {
                case "Int16":
                case "Int32":
                case "Int64":
                case "Decimal":
                case "Float":
                case "Double":
                    try
                    {
// ReSharper disable ReturnValueOfPureMethodIsNotUsed
                        Convert.ChangeType(nFilter.AdvancedFilterValue, col.DataType);
// ReSharper restore ReturnValueOfPureMethodIsNotUsed
                        switch (nFilter.AdvancedFilterOperator)
                        {
                            case "1": return " < " + nFilter.AdvancedFilterValue;
                            case "2": return " = " + nFilter.AdvancedFilterValue;
                            default: return " > " + nFilter.AdvancedFilterValue;
                        }
                    }
                    catch { return ""; }
                case "Boolean":
                    try
                    {
                        return " = '" + ((nFilter.AdvancedFilterValue.ToUpper() == "TRUE") ? "true" : "false") + "'";
                    }
                    catch { return ""; }
                case "DateTime":
                    try
                    {
                        return " = '" + nFilter.AdvancedFilterValue + "'";
                    }
                    catch { return ""; }
                default:
                    try
                    {
                        if (nFilter.AdvancedFilterOperator == "4")                        
                            return " like '%" + nFilter.AdvancedFilterValue + "%'";
                        
                        return " = '" + nFilter.AdvancedFilterValue + "'";
                    }
                    catch { return ""; }
            }
        }

        private string GetHtmlFlexigrid()
        {
            var sb = new StringBuilder();
            var flexId = this.ID + "_flex";

            sb.AppendLine("<table id='" + flexId + "' style='display: none'></table>");

            return sb.ToString();
        }

        private string GetScriptFlexigrid()
        {
            var sb = new StringBuilder();
            var flexId = this.ID + "_flex";

            sb.Append("Grids." + flexId + "=$('#" + flexId + "').flexigrid({");
            sb.Append("id: '" + flexId + "',");
            sb.Append("sec: '" + Secuencia + "',");
            sb.Append("url: '" + UrlData + "',");
            sb.Append("dataType: 'json',");
            sb.Append("colModel: " + GetGrillaColModel() + ",");
            sb.Append("usepager: true,");
            sb.Append("title: '" + Title + "',");
            sb.Append("useRp: true,");
            sb.Append("rp: " + PageSize.ToString() + ",");
            sb.Append("page: " + PageNumber.ToString() + ",");
            sb.Append("newp: " + PageNumber.ToString() + ",");
            sb.Append("showTableToggleBtn: true,");
            sb.Append("onSubmit: function () { return AddFormData('#" + Page.Form.ID + "', '#" + flexId + "','" + this.ID + "'); },");
            sb.Append("isMultiCheck: " + IsMultiCheck.ToString().ToLower() + ",");
            sb.Append("onChangeSort: false,");
            if (OnRowDblClick != "") sb.Append("onRowDblClick: function (e,r,t) { ParseOnRowDblClick(e,r,t, '" + ColModel.FindColumnId() + "', " + OnRowDblClick + ",'" + this.ID + "');},");
            if (OnRowSelected != "") sb.Append("onRowSelected: function (r,p,g) { " + OnRowSelected + "(r,p,g,'" + this.ID + "');},");
            sb.Append("autoload: true");
            sb.Append("});");

            return "InitGrid('" + flexId + "', function () { " + sb + " });";
        }

        public string GetGrillaColModel()
        {
            var script = "";
            if (IsMultiCheck)
            {
                script += "{ display: '', name: '" + CHECK_COLUMN_NAME + "', width: 20, sortable: false, type: 'Check'}";
            }

            if (!GenerateByColModel && DataSource != null)
            {
                foreach (DataColumn col in DataSource.Columns)
                {
                    var map = ColModel.FindColumnMap(col.ColumnName);
                    if (map.ColumnName != CHECK_COLUMN_NAME && !map.Ignore)
                    {
                        if (script != "") script += ",";
                        var hideStr = (map.Hidden) ? ", hide: true" : "";

                        script += "{ display: '" + Convert.ToString(map.Header) + "', " +
                          "name: '" + Convert.ToString(map.ColumnName) + "', " +
                          "width: " + Convert.ToString(map.Width) + ", " +
                          "sortable: " + map.Sortable.ToString().ToLower() + " " + hideStr + ", " +
                          "type: '" + GetColModelType(col) + "', " +
                          "filterValue: '" + map.AdvancedFilterValue + "', " +
                          "filterOperator: '" + map.AdvancedFilterOperator + "' " +
                          "}";
                    }
                }
            }
            else
            {
                foreach (var map in ColModel)
                {
                    if (!map.Ignore)
                    {
                        if (script != "") script += ",";
                        var hideStr = (map.Hidden) ? ", hide: true" : "";

                        script += "{ display: '" + Convert.ToString(map.Header) + "', " +
                          "name: '" + Convert.ToString(map.ColumnName) + "', " +
                          "width: " + Convert.ToString(map.Width) + ", " +
                          "sortable: " + map.Sortable.ToString().ToLower() + " " + hideStr + ", " +
                          "type: '" + "S" + "', " +
                          "filterValue: '" + map.AdvancedFilterValue + "', " +
                          "filterOperator: '" + map.AdvancedFilterOperator + "' " +
                          "}";
                    }
                }
            }

            return "[" + script + "]";
        }

        private string GetColModelType(DataColumn nColumn)
        {
            switch (nColumn.DataType.Name)
            {
                case "Int16":
                case "Int32":
                case "Int64":
                case "Decimal":
                case "Float":
                case "Double":
                    return "N";
                case "DateTime":
                    return "D";
                default:
                    return "S";
            }
        }

        public static SlygFlexigrid CreateProxyInstance(string nID)
        {
            var grid = new SlygFlexigrid();
            grid.ID = nID;
            return grid;
        }

        #endregion
    }

    public class SlygFlexColModel : List<FlexColumnMap>
    {
        #region Funciones

        public string FindColumnId()
        {
            foreach (var map in this)
            {
                if (map.IsColumnID)
                    return map.ColumnName;
            }
            return "";
        }

        public FlexColumnMap FindColumnMap(string nColumnName)
        {
            foreach (var map in this)
            {
                if (map.ColumnName == nColumnName)
                {
                    if (map.Header == "") map.Header = map.ColumnName;
                    return map;
                }
            }
            return new FlexColumnMap() { ColumnName = nColumnName, Header = nColumnName };
        }

        #endregion
    }

    public class FlexColumnMap
    {
        #region Propiedades

        public string ColumnName { get; set; }
        public string Header { get; set; }
        public int Width { get; set; }
        public bool IsColumnID { get; set; }
        public bool Sortable { get; set; }
        public bool Hidden { get; set; }
        public bool Ignore { get; set; }
        public bool IsFilterList { get; set; }
        public string AdvancedFilterOperator { get; set; }
        public string AdvancedFilterValue { get; set; }

        #endregion

        #region Constructores

        public FlexColumnMap()
        {
            this.ColumnName = "";
            this.Header = "";
            this.Width = 130;
            this.IsColumnID = false;
            this.Sortable = true;
            this.Hidden = false;
            this.Ignore = false;
            this.IsFilterList = false;
            this.AdvancedFilterOperator = "";
            this.AdvancedFilterValue = "";
        }

        #endregion
    }

}