using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Slyg.Data;
using Slyg.Web.Controls;

namespace WebPunteoElectronico
{
    public class AutoListHelper<T,E,R> where T : DataTable where E : ColumnEnum where R : DataRow
    {
        public T Data { get; set; }
        public E ColumnText { get; set; }
        public E ColumnValue { get; set; }

        public void Init(T nData, E nColumnText)
        {
            Data = nData;
            ColumnText = nColumnText;
            //ColumnValue = nColumnValue;
        }
        public void Init(T nData, E nColumnText, E nColumnValue)
        {
            Data = nData;
            ColumnText = nColumnText;
            ColumnValue = nColumnValue;
        }

        public string GetJson()
        {
            if (Data == null) return "[]"; 
            var sb = new ScriptBuilder(); 

            return sb.GetJSonData(Data.Select("", ColumnText.ColumnName), ColumnText.ColumnName);
        }

        public string GetJson(string nFilter)
        {
            if (Data == null) return "[]"; 
            var sb = new ScriptBuilder();
            return sb.GetJSonData(Data.Select(nFilter, ColumnText.ColumnName), ColumnText.ColumnName);
        }

        public string GetJson(string nFilter,string nSort)
        {
            if (Data == null) return "[]"; 
            var sb = new ScriptBuilder();
            return sb.GetJSonData(Data.Select(nFilter,nSort), ColumnText.ColumnName);
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
            var rows = Data.Select(ColumnText.ColumnName + "='" + nText + "'");
            if (rows.Length > 0)
            {
                return (R)rows[0];
            }
            return null;
        }

        public R GetRowByFilter(ColumnEnum nColumn, object nValue, string nText)
        {
            var rows = Data.Select(nColumn.ColumnName + " = " + nValue.ToString() + " AND " + ColumnText.ColumnName + "='" + nText + "'");
            if (rows.Length > 0)
            {
                return (R)rows[0];
            }
            return null;
        }

        public R GetRowByFilter(ColumnEnum nColumn1, object nValue1, ColumnEnum nColumn2, object nValue2, string nText)
        {
            var rows = Data.Select(nColumn1.ColumnName + " = " + nValue1.ToString() + " AND " + nColumn2.ColumnName + " = " + nValue2.ToString() + " AND " + ColumnText.ColumnName + "='" + nText + "'");
            if (rows.Length > 0)
            {
                return (R)rows[0];
            }
            return null;
        }

        public R GetRowByFilter(ColumnEnum nColumn1, object nValue1, ColumnEnum nColumn2, object nValue2, ColumnEnum nColumn3, object nValue3, string nText)
        {
            var rows = Data.Select(nColumn1.ColumnName + " = " + nValue1.ToString() + " AND " + nColumn2.ColumnName + " = " + nValue2.ToString() + " AND " + nColumn3.ColumnName + " = " + nValue3.ToString() + " AND " + ColumnText.ColumnName + "='" + nText + "'");
            if (rows.Length > 0)
            {
                return (R)rows[0];
            }
            return null;
        }

        public R GetRowByFilter(ColumnEnum nColumn1, object nValue1, ColumnEnum nColumn2, object nValue2, ColumnEnum nColumn3, object nValue3, ColumnEnum nColumn4, object nValue4, string nText)
        {
            var rows = Data.Select(nColumn1.ColumnName + " = " + nValue1.ToString() + " AND " + nColumn2.ColumnName + " = " + nValue2.ToString() + " AND " + nColumn3.ColumnName + " = " + nValue3.ToString() + " AND " + nColumn4.ColumnName + " = " + nValue4.ToString() + " AND " + ColumnText.ColumnName + "='" + nText + "'");
            if (rows.Length > 0)
            {
                return (R)rows[0];
            }
            return null;
        }

        public string GetText(E nColumnValue, string nValue)
        {
            var rows = Data.Select(nColumnValue.ColumnName + "='" + nValue + "'");
            if (rows.Length > 0)
            {
                return rows[0][ColumnText.ColumnName].ToString();
            }
            return "";
        }

        public string GetText(E nColumnValue, int nValue)
        {
            var rows = Data.Select(nColumnValue.ColumnName + "=" + nValue + "");
            if (rows.Length > 0)
            {
                return rows[0][ColumnText.ColumnName].ToString();
            }
            return "";
        }
    }
}