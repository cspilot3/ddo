using System;
using System.Threading;
using WebSantander.code;
using WebSantander.code.Grid;

/*
 * Fecha modificacion: 2012-06-29
 * Autor: Eliseo Roa
 */

namespace  WebSantander.controls
{
    public partial class proxy_data : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            ConsultarData();
        }

        #endregion

        #region Metodos

        private void ConsultarData()
        {
            var slygid = Request["slygid"];

            var grid = SlygFlexigrid.CreateProxyInstance(slygid);

            var clearChecks = (Request["clearChecks"] != null);
            grid.UpdateMulticheckData(true, clearChecks);

            if (Request["sec"] != null) grid.Secuencia = Convert.ToInt32(Request["sec"].Split(',')[0]);
            if (Request["rp"] != null) grid.PageSize = Convert.ToInt32(Request["rp"].Split(',')[0]);
            if (Request["page"] != null) grid.PageNumber = Convert.ToInt32(Request["page"]);
            if (Request["sortname"] != null && Request["sortname"] != "undefined") grid.SortColumnName = Request["sortname"];
            if (Request["sortorder"] != null) grid.SortAscendant = Request["sortorder"].ToLower() != "desc";

            UpdateGridAdvancedFilters(grid);

            try
            {
                WriteData(grid, grid.PageSize, grid.PageNumber);
            }
            catch (ThreadAbortException ex)
            {
                TraceNothing(ex);
            }
            catch (Exception ex)
            {
                TraceError(ex);
                WriteError(ex.Message);
            }
        }

        private void UpdateGridAdvancedFilters(SlygFlexigrid nGrid)
        {
            foreach (var col in nGrid.ColModel)
            {
                var reqValue = Request.Form["filter_" + col.ColumnName];
                if (reqValue != null && reqValue.Trim() != "")
                {
                    col.AdvancedFilterOperator = Request["fope_" + col.ColumnName];
                    col.AdvancedFilterValue = reqValue;
                }
                else
                {
                    col.AdvancedFilterOperator = "";
                    col.AdvancedFilterValue = "";
                }
            }
        }

        public void WriteData(SlygFlexigrid nGrid, int nPageSize, int nPageNumber)
        {
            var js = "";
            if (nGrid != null)
            {
                js = JSonGridData.Instance.GenerateJsonGridData(nGrid);
            }
            else
            {
                js = JSonGridData.Instance.GenerateJsonGridData(0, nPageSize, nPageNumber);
            }

            Response.Clear();
            Response.Write(js);
            Response.End();
        }

        public void WriteError(string nErrorMessage)
        {
            var js = JSonGridData.Instance.GenerateJsonErroData(nErrorMessage);

            Response.Clear();
            Response.Write(js);
            Response.End();
        }

        private void TraceError(Exception ex)
        {
            Program.TraceError(ex);
        }

        // ReSharper disable UnusedParameter.Local
        private void TraceNothing(Exception ex)

        {
        }
        // ReSharper restore UnusedParameter.Local

        #endregion
    }
}