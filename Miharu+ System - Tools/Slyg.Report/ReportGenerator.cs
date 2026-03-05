using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using Ionic.Zip;
using System.Linq;

namespace Slyg.Report
{
    public class ReportGenerator
    {
        private static string[] Colors = new string[15] { "FF0000", "6464C8", "D7A1FF", "004477", "FFFF00", "FF4B4B", "00C000", "A54BA5", "00AFFF", "FFFF64", "D7DB70", "FFB464", "966414", "A5A5A5", "E1E1E1"};
       
        public static string CreateGraph(DataReport nData, short nWidth, short nHeight)
        {
            string DataXML = string.Empty;

            //Genera Datos En XML
            nData.DatosXML = CreateXML(nData);

            //Genera Datos en TXT                
            DataXML = "";
            DataXML += "<graph caption='" + nData.Nombre_Reporte + "' xAxisName='" + nData.EjeX + "' yAxisName='" + nData.EjeY + "' decimalPrecision='" + nData.PrecisionDecimal + "' formatNumberScale='0' rotateNames='1' showNames='1' isSliced='1' slicingDistance='10'>";
            DataXML += nData.DatosXML;
            DataXML += "</graph>";
            nData.DatosXML = DataXML;

            return CreateHTML(nData.Tipo_Reporte, nData.DatosXML, nData.Id_Reporte, nWidth, nHeight);
        }

        public static string CreateGraphFiltered(DataReport nData, short nWidth, short nHeight)
        {
            string DataXML = string.Empty;

            //Genera Datos En XML
            nData.DatosXML = CreateXMLFiltered(nData);

            //Genera Datos en TXT                
            DataXML = "";
            DataXML += "<graph caption='" + nData.Nombre_Reporte + "' xAxisName='" + nData.EjeX + "' yAxisName='" + nData.EjeY + "' decimalPrecision='" + nData.PrecisionDecimal + "' formatNumberScale='0' rotateNames='1' showNames='1' isSliced='1' slicingDistance='10'>";
            DataXML += nData.DatosXML;
            DataXML += "</graph>";
            nData.DatosXML = DataXML;

            return CreateHTML(nData.Tipo_Reporte, nData.DatosXML, nData.Id_Reporte, nWidth, nHeight);
        }

        private static string CreateXML(DataReport nData)
        {
            try
            {
                string sXML = string.Empty;
                sXML = string.Empty;
                DataView desiredResult = GroupBy(nData.CampoMostrarX, "id_Oficina", nData.Datos);
                int Cantidad = 0;
                int Total = 0;

                foreach (DataRowView item in desiredResult)
                {
                    if (Cantidad < 15)
                        sXML += "<set name='" + item[0].ToString() + "' value='" + item[1].ToString() + "' color='" + Colors[Cantidad] + "' />";
                    else
                        Total += Convert.ToInt32(item[1].ToString());

                    Cantidad += 1;
                }
                if (Cantidad > 15)
                    sXML += "<set name='Otros' value='" + Total.ToString() + "' color='000000' />";

                return sXML;
            }
            catch (Exception ex)
            {
                throw new Exception("No fue posible generar el XML: " + ex.Message);
            }
        }

        private static string CreateXMLFiltered(DataReport nData)
        {
            try
            {
                string sXML = string.Empty;
                sXML = string.Empty;
                int Cantidad = 0;
                int Total = 0;

                if (nData.Datos != null)
                {
                    foreach (DataRow item in nData.Datos.Rows)
                    {
                        if (Cantidad < 15)
                            sXML += "<set name='" + item[1].ToString() + "' value='" + item[0].ToString() + "' color='" + Colors[Cantidad] + "' />";
                        else
                            Total += Convert.ToInt32(item[0].ToString());

                        Cantidad += 1;
                    }
                }

                if (Cantidad > 15)
                    sXML += "<set name='Otros' value='" + Total.ToString() + "' color='000000' />";

                return sXML;
            }
            catch (Exception ex)
            {
                throw new Exception("No fue posible generar el XML: " + ex.Message);
            }
        }

        private static string CreateHTML(string nTipoReporte, string nXML, string nIdReporte, short nWith, short nHeigth)
        {
            try
            {
                StringBuilder HTML = new StringBuilder();
                HTML.AppendLine(string.Format("<!-- START Bloque de codigo, grafico {0} -->", nIdReporte));
                HTML.AppendLine(string.Format("<object classid=\"clsid:d27cdb6e-ae6d-11cf-96b8-444553540000\" codebase=\"http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=8,0,0,0\" width=\"{0}\" height=\"{1}\" name=\"{2}\" style=\"z-index: 1\">", nWith, nHeigth, nIdReporte));
                HTML.AppendLine("<param name=\"allowScriptAccess\" value=\"always\" />");
                HTML.AppendLine(string.Format("<param name=\"movie\" value=\"../../Charts/FCF_{0}.swf\"/>", nTipoReporte));
                HTML.AppendLine(string.Format("<param name=\"FlashVars\" value=\"&chartWidth={0}&chartHeight={1}&debugMode=0&dataXML={2}\"/>", nWith, nHeigth, nXML));
                HTML.AppendLine("<param name=\"quality\" value=\"high\" />");
                HTML.AppendLine("<param name=\"wmode\" value=\"transparent\" />");
                HTML.AppendLine(string.Format("<embed src=\"../../Charts/FCF_{3}.swf\"  FlashVars=\"&chartWidth={0}&chartHeight={1}&debugMode=0&dataXML={2}\"", nWith, nHeigth, nXML, nTipoReporte));
                HTML.AppendLine(string.Format("quality=\"high\" width=\"{0}\" height=\"{1}\" name=\"{2}\"  allowScriptAccess=\"always\" wmode=\"transparent\"></", nWith, nHeigth, nIdReporte));
                HTML.AppendLine("type=\"application/x-shockwave-flash\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\" />");
                HTML.AppendLine("</object>");
                HTML.AppendLine(string.Format("<!-- END Bloque de codigo, grafico {0}  -->", nIdReporte));

                return HTML.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("No fue posible generar el grafico: " + ex.Message);
            }
        }

        public static byte[] BuildZipData(DataTable nDataTable, string nZipFilename)
        {
            var Exportador = new Slyg.Tools.CSV.CSVData(";", "", true);
            var Flujo = new MemoryStream();

            Exportador.SaveEncoding = System.Text.Encoding.UTF32;

            var t = new Slyg.Tools.CSV.CSVTable(nDataTable);

            if (nDataTable.Columns.Contains("Valor"))
                t.Columns["Valor"].Format = "$ #,###0.00";

            if (nDataTable.Columns.Contains("Comision"))
                t.Columns["Comision"].Format = "$ #,###0.00";

            Exportador.SaveAsCSV(t, Flujo, true);

            byte[] nData;
            ZipFile zip = new ZipFile();
            zip.AddEntry(Path.GetFileNameWithoutExtension(nZipFilename) + ".csv",  Flujo.ToArray());

            MemoryStream ZipData = new MemoryStream();
            zip.Save(ZipData);
            nData = ZipData.GetBuffer();

            return nData;
        }

        public static byte[] BuildZipData_Consolidado(DataTable nDataTable, string nZipFilename)
        {
            StringBuilder Output = new StringBuilder();
            byte[] nData;
            using (var sw = new StringWriter())
            {
                foreach (DataColumn col in nDataTable.Columns)
                {
                    sw.Write(col.ColumnName + "; ");
                }

                Output.AppendLine(sw.ToString().Substring(0, sw.ToString().Length - 2));
            }

            foreach (DataRow row in nDataTable.Rows)
            {
                using (var sw = new StringWriter())
                {
                    foreach (DataColumn col in nDataTable.Columns)
                    {
                        sw.Write(row[col].ToString() + "; ");
                    }

                    Output.AppendLine(sw.ToString().Substring(0, sw.ToString().Length - 2));
                }
            }


            ZipFile zip = new ZipFile();

            zip.AddEntry(Path.GetFileNameWithoutExtension(nZipFilename) + ".csv", Output.ToString());

            MemoryStream ZipData = new MemoryStream();
            zip.Save(ZipData);

            nData = ZipData.GetBuffer();

            return nData;
        }


        private static DataView GroupBy(string i_sGroupByColumn, string i_sAggregateColumn, DataTable i_dSourceTable)
        {
            DataView dv = new DataView(i_dSourceTable);

            DataTable dtGroup = dv.ToTable(true, new string[] { i_sGroupByColumn });

            dtGroup.Columns.Add("Count", typeof(int));

            foreach (DataRow dr in dtGroup.Rows)
            {
                dr["Count"] = i_dSourceTable.Compute("Count(" + i_sAggregateColumn + ")", i_sGroupByColumn + " = '" + dr[i_sGroupByColumn] + "'");
            }
            dtGroup.DefaultView.Sort = "Count DESC";
            return dtGroup.DefaultView;
        }

        public static string getTipoReporteEnumString(TipoReporteEnum nTipo)
        {
            switch (nTipo)
            {
                case TipoReporteEnum.Area2D: return "Area2D";
                case TipoReporteEnum.Bar2D: return "Bar2D";
                case TipoReporteEnum.Column2D: return "Column2D";
                case TipoReporteEnum.Column3D: return "Column3D";
                case TipoReporteEnum.Doughnut2D: return "Doughnut2D";
                case TipoReporteEnum.Funnel: return "Funnel";
                case TipoReporteEnum.Line: return "Line";
                case TipoReporteEnum.Pie2D: return "Pie2D";
                case TipoReporteEnum.Pie3D: return "Pie3D";
                default: throw new Exception("Tipo no definido");
            }
        }
    }
}
