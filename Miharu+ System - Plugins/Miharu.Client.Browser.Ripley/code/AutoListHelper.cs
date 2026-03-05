using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using Slyg.Data;

namespace  Miharu.Client.Browser.code
{
    public class AutoListHelper<T, E, R> : IAutoListHelper
        where T : DataTable
        where E : ColumnEnum
        where R : DataRow
    {
        #region Propiedades

        public T Data { get; set; }
        public E ColumnText { get; set; }
        public E ColumnValue { get; set; }
        public bool IsAjaxData { get; set; }

        #endregion

        #region Constructores

        #endregion

        #region Metodos

        //public void Init(T nData, E nColumnText)
        //{
        //    IsAjaxData = false;
        //    Data = nData;
        //    ColumnText = nColumnText;
        //    //ColumnValue = nColumnValue;
        //    IsAjaxData = false;
        //    Initialize();
        //}

        public void Init(T nData, E nColumnText, bool nIsAjaxData = false)
        {
            Data = nData;
            ColumnText = nColumnText;
            //ColumnValue = nColumnValue;
            IsAjaxData = nIsAjaxData;
            Initialize();
        }

        public void Init(T nData, E nColumnText, E nColumnValue, bool nIsAjaxData = false)
        {
            Data = nData;
            ColumnText = nColumnText;
            ColumnValue = nColumnValue;
            IsAjaxData = nIsAjaxData;
            Initialize();
        }

        #endregion

        #region Funciones

        public override string GetAjaxData(string nQuery)
        {
            if (Data == null) return "{query:'" + nQuery + "',suggestions:[],data:[]}";

            var sb = new ScriptBuilder();
            var like = nQuery.Replace("*", "%").TrimEnd('%') + "%";

            var jSonData = sb.GetJSonData(Data.Select(ColumnText.ColumnName + " LIKE '" + like + "'", ColumnText.ColumnName), ColumnText.ColumnName, 100);

            return "{query:'" + nQuery + "',suggestions:" + jSonData + ",data:[]}";
        }

        public string GetJson()
        {
            if (!IsAjaxData)
            {
                if (Data == null) return "[]";

                var sb = new ScriptBuilder();
                return sb.GetJSonData(Data.Select("", ColumnText.ColumnName), ColumnText.ColumnName);
            }

            return "'" + AutoListManager.GetRelativeUrlToProxyData() + "?nID=" + ID + "'";
        }

        public string GetJson(string nFilter)
        {
            if (!IsAjaxData)
            {
                if (Data == null) return "[]";

                var sb = new ScriptBuilder();
                return sb.GetJSonData(Data.Select(nFilter, ColumnText.ColumnName), ColumnText.ColumnName);
            }
            
            return "'" + AutoListManager.GetRelativeUrlToProxyData() + "?nID=" + ID + "'";
        }

        public string GetJson(string nFilter, string nSort)
        {
            if (!IsAjaxData)
            {
                if (Data == null) return "[]";

                var sb = new ScriptBuilder();
                return sb.GetJSonData(Data.Select(nFilter, nSort), ColumnText.ColumnName);
            }

            return "'" + AutoListManager.GetRelativeUrlToProxyData() + "?nID=" + ID + "'";
        }

        public R GetRowByValue(string nValue)
        {
            var rows = Data.Select(ColumnValue.ColumnName + "='" + nValue + "'");
            if (rows.Length > 0)
            {
                return (R)rows[0];
            }
            return null;
        }

        public R GetRowByText(string nText)
        {
            if (Data == null) return null;

            var rows = Data.Select(ColumnText.ColumnName + "='" + nText + "'");
            if (rows.Length > 0)
            {
                return (R)rows[0];
            }
            return null;
        }

        public R GetRowByFilter(ColumnEnum nColumn, object nValue, string nText)
        {
            if (Data == null) return null;

            var rows = Data.Select(nColumn.ColumnName + " = " + nValue + " AND " + ColumnText.ColumnName + "='" + nText + "'");
            if (rows.Length > 0)
            {
                return (R)rows[0];
            }
            return null;
        }

        public R GetRowByFilter(ColumnEnum nColumn1, object nValue1, ColumnEnum nColumn2, object nValue2, string nText)
        {
            if (Data == null) return null;

            var rows = Data.Select(nColumn1.ColumnName + " = " + nValue1 + " AND " + nColumn2.ColumnName + " = " + nValue2 + " AND " + ColumnText.ColumnName + "='" + nText + "'");
            if (rows.Length > 0)
            {
                return (R)rows[0];
            }
            return null;
        }

        public R GetRowByFilter(ColumnEnum nColumn1, object nValue1, ColumnEnum nColumn2, object nValue2, ColumnEnum nColumn3, object nValue3, string nText)
        {
            if (Data == null) return null;

            var rows = Data.Select(nColumn1.ColumnName + " = " + nValue1 + " AND " + nColumn2.ColumnName + " = " + nValue2 + " AND " + nColumn3.ColumnName + " = " + nValue3 + " AND " + ColumnText.ColumnName + "='" + nText + "'");
            if (rows.Length > 0)
            {
                return (R)rows[0];
            }
            return null;
        }

        public R GetRowByFilter(ColumnEnum nColumn1, object nValue1, ColumnEnum nColumn2, object nValue2, ColumnEnum nColumn3, object nValue3, ColumnEnum nColumn4, object nValue4, string nText)
        {
            if (Data == null) return null;

            var rows = Data.Select(nColumn1.ColumnName + " = " + nValue1 + " AND " + nColumn2.ColumnName + " = " + nValue2 + " AND " + nColumn3.ColumnName + " = " + nValue3 + " AND " + nColumn4.ColumnName + " = " + nValue4 + " AND " + ColumnText.ColumnName + "='" + nText + "'");
            if (rows.Length > 0)
            {
                return (R)rows[0];
            }
            return null;
        }

        public string GetText(E nColumnValue, string nValue)
        {
            if (Data == null) return null;

            var rows = Data.Select(nColumnValue.ColumnName + "='" + nValue + "'");
            if (rows.Length > 0)
            {
                return rows[0][ColumnText.ColumnName].ToString();
            }
            return "";
        }

        public string GetText(E nColumnValue, int nValue)
        {
            if (Data == null) return null;

            var rows = Data.Select(nColumnValue.ColumnName + "=" + nValue + "");
            if (rows.Length > 0)
            {
                return rows[0][ColumnText.ColumnName].ToString();
            }
            return "";
        }

        #endregion
    }

    public abstract class IAutoListHelper
    {
        public int ID { get; set; }

        #region Constructores

        public IAutoListHelper()
        {
            this.ID = -1;
        }

        #endregion

        #region Metodos

        protected void Initialize()
        {
            if (ID == -1)
            {
                ID = AutoListManager.Instance.Consecutivo++;
                AutoListManager.Instance.Add(this);
            }
        }

        #endregion

        #region Funciones

        public abstract string GetAjaxData(string nQuery);

        #endregion
    }

    public class AutoListManager
    {
        #region Propiedades

        public int Consecutivo { get; set; }
        public static string ProxyDataUrl { get; set; }
        public List<IAutoListHelper> Items { get; set; }

        public static AutoListManager Instance
        {
            get
            {
                if (HttpContext.Current.Session["__AutoListManagerCache_"] == null)
                    HttpContext.Current.Session["__AutoListManagerCache_"] = new AutoListManager();
                return (AutoListManager)HttpContext.Current.Session["__AutoListManagerCache_"];
            }
        }

        #endregion

        #region Constructores

        public AutoListManager()
        {
            this.Consecutivo = 0;
            this.Items = new List<IAutoListHelper>();
        }

        #endregion

        #region Metodos

        public void Add(IAutoListHelper nAutoList)
        {
            if (!Items.Contains(nAutoList))
                Items.Add(nAutoList);
        }

        #endregion

        #region Funciones

        public static string GetRelativeUrlToProxyData()
        {
            if (string.IsNullOrEmpty(ProxyDataUrl))
                throw new Exception("No se ha establecido la ruta del cache de datos ProxyDataUrl");

            return System.Web.VirtualPathUtility.ToAbsolute(ProxyDataUrl);
        }

        public IAutoListHelper FindByID(int nID)
        {
            foreach (var i in Items)
            {
                if (i.ID == nID)
                    return i;
            }
            return null;
        }

        public string GetCurrentAutoListAjaxData()
        {
            var context = HttpContext.Current;
            if (context.Request["nID"] == null) return "[]";

            var autoList = FindByID(Convert.ToInt32(context.Request["nID"]));
            if (autoList == null) return "[]";

            return autoList.GetAjaxData(context.Request["query"]);
        }

        #endregion
    }
}