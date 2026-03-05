using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;

namespace Miharu.Explorer._Clases
{
    public static class Extensions
    {

        public static void Fill(this DropDownList combo, object dataSource, string value, string text)
        {
            combo.DataSource = dataSource;
            combo.DataValueField = value;
            combo.DataTextField = text;
            combo.DataBind();
        }

        public static string Serialize(this DataTable Data)
        {
            var cadena = (from DataRow Row in Data.Rows select (from DataColumn col in Data.Columns select col.ColumnName + ":'" + Row[col].val() + "'").ToList() into fila select "{" + string.Join(",", fila) + "}").ToList();
            return "[" + string.Join(",", cadena) + "]";
        }

        public static string val(this object data)
        {
            try
            {
                return data.ToString();
            }
            catch
            {
                return "";
            }
        }

        public static DataTable ToDataTable<T>(this IList<T> data)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            var table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }

            var values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }

        public static string getOption(this DataTable data, string value, string text)
        {
            var Resultado = (from DataRow Data in data.Rows
                             select "<Option value='" + Data[value].val() + "'>" + Data[text].val() + "</option>").ToArray();

            return string.Join("", Resultado);
        }

        public static string getOptionGrid(this DataTable data, string value, string text)
        {
            var Resultado = (from DataRow Data in data.Rows
                             select Data[value].val() + ":" + Data[text].val());

            return string.Join(";", Resultado);
        }

    }
}