using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Data;

namespace Slyg.Web.Controls
{
    public class ScriptBuilder
    {
        private StringBuilder Builder = new StringBuilder();

        public int Length { get { return Builder.Length; } }

        public void AppendAndCleanContinuous(string nScript)
        {
            Builder.Append(CleanScript(nScript));
        }

        public void Append(string nScript)
        {
            Builder.Append(nScript);
        }

        public void AppendContinuous(string nScript)
        {
            Builder.Append(nScript);
        }

        public string CleanScript(string nScript)
        {
            return nScript.Replace("\n", "").Replace("\r", "").Replace("\"", "\\\"");//.Replace("'", "\\'");
        }

        public string CleanListCharacters(String nValue)
        {
            return nValue.Replace(":", "").Replace(";", "").Replace(",", "").Replace("\"", "");
        }

        public void AppendLine(string nText)
        {
            if (nText.Trim() != "")
            {
                Builder.Append(nText);
                Builder.Append("´");
            }
        }

        public void AppendAndEncodeScript(string nScript)
        {
            AppendScript(EncodeScript(nScript));
        }

        public void AppendScript(string nScript)
        {
            if (nScript.Trim() != "")
            {
                Builder.Append(nScript);
                Builder.Append("");
                Builder.Append("´");
            }
        }

        public void Clear()
        {
            Builder.Clear();
        }

        public string GetScript()
        {
            return Builder.ToString();
        }

        public override string ToString()
        {
            return GetScript();
        }

        public string GetJSonData(DataRow[] nData, string nColumnName)
        {
            var sb = new ScriptBuilder();
            try
            {
                foreach (var row in nData)
                {
                    if (sb.Length > 0) sb.AppendContinuous(",");
                    //sb.AppendContinuous("\"" + sb.CleanListCharacters(row[nColumnName].ToString()) + "\"");
                    sb.AppendContinuous("'" + sb.CleanListCharacters(row[nColumnName].ToString()) + "'");
                }

                return "[" + sb.GetScript() + "]";
            }
            catch (Exception ex)
            {
                throw new Exception("No fue posible formatear los datos a json, " + ex.Message, ex);
            }
        }

        public string EncodeScript(string nScript)
        {
            if (nScript == null) return "";
            var encScript = nScript;

            encScript = encScript.Replace("*", "*_");
            encScript = encScript.Replace("\"", "*22_");
            encScript = encScript.Replace("'", "*27_");
            encScript = encScript.Replace("\\", "*5C_");
            encScript = encScript.Replace("`", "*60_");
            encScript = encScript.Replace(":", "*3A_");
            encScript = encScript.Replace(";", "*3B_");
            encScript = encScript.Replace(",", "*3C_");
            encScript = encScript.Replace("|", "*5X_");
            encScript = encScript.Replace("!", "*6X_");
            encScript = encScript.Replace("\n", "*7C_");
            encScript = encScript.Replace("\r", "*8C_");
            return encScript;
        }

        public string DecodeScript(string nScript)
        {
            if (nScript == null) return "";
            var decScript = nScript;

            decScript = decScript.Replace("*22_", "\"");
            decScript = decScript.Replace("*27_", "'");
            decScript = decScript.Replace("*5C_", "\\");
            decScript = decScript.Replace("*60_", "`");
            decScript = decScript.Replace("*3A_", ":");
            decScript = decScript.Replace("*3B_", ";");
            decScript = decScript.Replace("*3C_", ",");
            decScript = decScript.Replace("*5X_", "|");
            decScript = decScript.Replace("*6X_", "!");
            decScript = decScript.Replace("*7C_", "\n");
            decScript = decScript.Replace("*8C_", "\r");
            decScript = decScript.Replace("*_", "*");
            return decScript;
        }

    }
}
