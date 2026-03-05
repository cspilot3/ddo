using System;
using System.Collections.Generic;
using System.Web;
using WebSantander.code;
using Microsoft.Reporting.WebForms;

namespace WebSantander.site.reportes
{
    public class ReportesWeb: WebReport
    {
        public override string ReportName
        {
            get { return "Reporte Embargos y Desembargos"; }
        }

        public int FechaProcesoInicial { get; set; }
        public int FechaProcesoFinal { get; set; }
        public int Reporte { get; set; }
        public string CadenaConexion { get; set; }
        public int IdUsuario { get; set; }
        public short Entidad { get; set; }
        public short Proyecto { get; set; }


        public override void Launch(ref ReportViewer nReportViewer)
        {
            DBIntegration.DBIntegrationDataBaseManager dbmIntegration = null;

            try
            {
                dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(this.CadenaConexion);
                dbmIntegration.Connection_Open(IdUsuario);

                var Datos = dbmIntegration.SchemaSantander.SP_Report_DataClient.DBExecute(Reporte,FechaProcesoInicial,FechaProcesoFinal,Entidad, Proyecto);
                var TBL_Process_DataDataSet = new ReportDataSource("TBL_Process_DataDataSet", Datos);

                switch (Reporte)
                {
                    case 1:
                        nReportViewer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath(Reports.site.Reportes.Reporte_Embargos_Desembargos.Reporte_Embargo_Desembargo);
                        break;
                    case 2:
                        nReportViewer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath(Reports.site.Reportes.Reporte_Validacion_Listas.Reporte_Validacion_Lista);
                        break;
                    case 3:
                        nReportViewer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath(Reports.site.Reportes.Reporte_Facturaciones.Reporte_Facturacion);
                        var mostrar = (Proyecto == 1);
                        var parametro = new ReportParameter("OcultarEnte",mostrar.ToString());
                        nReportViewer.LocalReport.SetParameters(parametro);
                        break;
                    case 4:
                        nReportViewer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath(Reports.site.Reportes.Reporte_Facturacion_Detallado.Reporte_Facturacion_Detallada);
                        break;
                    case 5:
                        nReportViewer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath(Reports.site.Reportes.Reporte_Cruces.Reporte_Cruce);
                        break;
                    case 6:
                        nReportViewer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath(Reports.site.Reportes.Reporte_Cargues.Reporte_Cargue);
                        break;
                }
                nReportViewer.LocalReport.DataSources.Clear();
                
                nReportViewer.LocalReport.DataSources.Add(TBL_Process_DataDataSet);
                nReportViewer.ShowExportControls = true;
                nReportViewer.ShowPrintButton = true;
                nReportViewer.LocalReport.Refresh();
            }
            finally
            {
                if (dbmIntegration != null) dbmIntegration.Connection_Close();
            }
        }

        public override void Launch(ref ReportViewer nReportViewer, Dictionary<string, object> nParameters)
        {
            this.CadenaConexion = (string)nParameters["ConnectionString"];
            this.Reporte = (int)nParameters["Reporte"];
            this.FechaProcesoInicial = (int)nParameters["FechaProcesoInicial"];
            this.FechaProcesoFinal = (int)nParameters["FechaProcesoFinal"];
        }

        public override void Launch(ref ReportViewer nReportViewer, System.Collections.Specialized.NameValueCollection nameValueCollection)
        {
            throw new NotImplementedException();
        }

        public override string getParameter(string nParameterName, string nDefaultValue)
        {
            throw new NotImplementedException();
        }
        

    }
}