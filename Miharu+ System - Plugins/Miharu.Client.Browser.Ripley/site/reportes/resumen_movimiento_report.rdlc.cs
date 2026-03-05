using System;
using System.Collections.Generic;
using System.Web;
using Miharu.Client.Browser.code;
using Microsoft.Reporting.WebForms;

namespace Miharu.Client.Browser.site.reportes
{
    public class resumen_movimiento_report : WebReport
    {
        public override string ReportName
        {
            get { return "Reporte de Resumen de Movimientos"; }
        }

        public DateTime FechaInicio {get; set;}
        public DateTime FechaFinal { get; set; }
        public int IdProyecto { get; set; }
        public int IdPunto { get; set; }
        public int IdDocumento { get; set; }
        public string CadenaConexion { get; set; }
        public int IdUsuario { get; set; }
        public bool TieneExportacion { get; set; }


        public override void Launch(ref ReportViewer nReportViewer)
        {
            DBIntegration.DBIntegrationDataBaseManager dbmIntegration = null;

            try
            {
                dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(this.CadenaConexion);
                dbmIntegration.Connection_Open(IdUsuario);

                var ResumenData = dbmIntegration.SchemaRipley.PA_Resumen_Movimiento_Report.DBExecute(IdProyecto,IdPunto, IdDocumento, FechaInicio.ToString("yyyy/MM/dd"), FechaFinal.ToString("yyyy/MM/dd"));
                var CTA_ResumenCTA_Resumen_Movimiento_ReportDataSet = new ReportDataSource("CTA_Resumen_Movimiento_ReportDataSet", ResumenData);

                nReportViewer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/" + Reports.site.reportes.resumen_movimiento_report);
                nReportViewer.LocalReport.DataSources.Clear();
                nReportViewer.LocalReport.DataSources.Add(CTA_ResumenCTA_Resumen_Movimiento_ReportDataSet);
                nReportViewer.ShowExportControls = TieneExportacion;
                nReportViewer.ShowPrintButton = TieneExportacion;
                nReportViewer.LocalReport.Refresh();
            }            
            finally
            {
                if (dbmIntegration != null) dbmIntegration.Connection_Close();
            }
        }

        public override void Launch(ref ReportViewer nReportViewer, Dictionary<string, object> nParameters)
        {
            this.FechaInicio = (DateTime)nParameters["FechaInicio"];
            this.FechaFinal = (DateTime)nParameters["FechaFinal"];
            this.IdPunto = (int)nParameters["IdPunto"];
            this.IdDocumento = (int)nParameters["IdDocumento"];
            this.CadenaConexion = (string)nParameters["CadenaConexion"];
        }

        public override void Launch(ref ReportViewer reportViewer, System.Collections.Specialized.NameValueCollection nameValueCollection)
        {
            throw new NotImplementedException();
        }

        public override string getParameter(string nParameterName, string nDefaultValue)
        {
            throw new NotImplementedException();
        }
    }
}