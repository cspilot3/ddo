using System;
using System.Collections.Generic;
using WebPunteoElectronico.Clases;
using Microsoft.Reporting.WebForms;

namespace WebPunteoElectronico.Site.Reportes.ConsultaCasosSMS
{
    public class Reporte_ConsultaCasosSMS : WebReport
    {
        #region Propiedades

        public override string ReportName
        {
            get { return "Consulta Casos SMS"; }
        }

        #endregion

        #region Metodos

        public override void Launch(ref ReportViewer nReportViewer, Dictionary<string, object> nParameters)
        {
            var ConnectionString = (TypeConnectionString)nParameters["ConnectionString"];
            var Usuario = (string)nParameters["Usuario"];

            int IdRadicacion = ((int) nParameters["IdRadicacion"] == 0) ? -1 : (int) nParameters["IdRadicacion"];

            var IdRegional = (short)nParameters["IdRegional"];
            var NombreRegional = (string)nParameters["NombreRegional"];
            var IdCOB = (short)nParameters["IdCOB"];
            var NombreCOB = (string)nParameters["NombreCOB"];
            var IdOficina = (int)nParameters["IdOficina"];
            var NombreOficina = (string)nParameters["NombreOficina"];
            var FechaInicial = (string)nParameters["FechaInicial"];
            var FechaFinal = (string)nParameters["FechaFinal"];
            var Login = (string)nParameters["Login"];

            DBWorkFlow.DBWorkFlowDataBaseManager DBMWorkflow = null;
               
            try
            {
                DBMWorkflow = new DBWorkFlow.DBWorkFlowDataBaseManager(ConnectionString.Workflow);
                DBMWorkflow.Connection_Open();

                System.Data.DataTable DataR = DBMWorkflow.Schemadbo.PA_DatosBanagrario.DBExecute(IdRadicacion, Usuario == "" ? null : Usuario, FechaInicial, FechaFinal, IdRegional, IdCOB, IdOficina);

                var reportParameters = new List<ReportParameter>
                    {
                        new ReportParameter("UsuarioLogin", Login),
                        new ReportParameter("Regional", NombreRegional), 
                        new ReportParameter("Cob", NombreCOB), 
                        new ReportParameter("Oficina", NombreOficina)
                    };

                var reportDataSource = new ReportDataSource("CTA_Reporte_Consulta_Casos_SMS", DataR);

                nReportViewer.LocalReport.DataSources.Clear();
                nReportViewer.LocalReport.ReportPath = Reports.Site.Reportes.ConsultaCasosSMS.Reporte_ConsultaCasosSMS;
                nReportViewer.LocalReport.DataSources.Add(reportDataSource);
                nReportViewer.LocalReport.SetParameters(reportParameters);
                nReportViewer.LocalReport.Refresh();
            }
            finally
            {
                if (DBMWorkflow != null) DBMWorkflow.Connection_Close();
            }
        }

        public override void Launch(ref ReportViewer nReportViewer)
        {
            throw new NotImplementedException();
        }

        public override void Launch(ref ReportViewer nReportViewer, System.Collections.Specialized.NameValueCollection nQueryString)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Funciones

        public override string getParameter(string nParameterName)
        {
            return "-1";
        }

        public override bool Validate(Dictionary<string, object> nParameters, out string nMessageError)
        {

            var FechaInicialS = (string)nParameters["FechaInicial"];
            var FechaFinalS = (string)nParameters["FechaFinal"];

            var FechaInicialD = Slyg.Tools.DataConvert.ToDate(FechaInicialS, Slyg.Tools.DataConvert.EnumDateFormat.yyyyMMdd, '/');
            var FechaFinalD = Slyg.Tools.DataConvert.ToDate(FechaFinalS, Slyg.Tools.DataConvert.EnumDateFormat.yyyyMMdd, '/');

            if ((FechaFinalD.Value - FechaInicialD.Value).Days > 30)
            {
                nMessageError = "El rango de Fechas de movimiento debe ser menor o igual a 30 días";
                return false;
            }

            nMessageError = "";
            return true;
        }

        #endregion
    }
}