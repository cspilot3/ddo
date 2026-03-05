using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebPunteoElectronico.Clases;
using DBAgrario;
using Microsoft.Reporting.WebForms;
using System.Data;

namespace WebPunteoElectronico.Site.Reportes.R005_Consolidado_Medio_Pago_Detalle
{
    public class R005_Consolidado_Medios_Pago_Detalle : WebReport
    {
        #region Declaraciones

        private int IdUsuario;
        public string Path_Nodo = "3.2.13.1";

        #endregion

        #region Propiedades

        public override string ReportName
        {
            get { return "05-Consolidado Medios de Pago"; }
        }

        public TypeConnectionString ConnectionString { get; set; }        

        public short idRegional { get; set; }
        public string NombreRegional { get; set; }

        public short idCOB { get; set; }
        public string NombreCOB { get; set; }

        public int IdOficina { get; set; }
        public string NombreOficina { get; set; }

        public string FechaMovimiento { get; set; }
        public string Login { get; set; }

        public int IdOficinaDetalle { get; set; }

        DataTable datos_Otros_Medios { get; set; }
        DataTable datos_Efectivo { get; set; }
        DataTable datos_Sin_Identificar { get; set; }
        DataTable datos_Totales { get; set; }

        #endregion
                     
        #region Métodos

        public override void Reload(ref ReportViewer nReportViewer)
        {

            var UrlRetorno = Program.LocalServerURL;
            var reportParameters = new List<ReportParameter>();
            reportParameters.Add(new ReportParameter("UsuarioLogin", Login));
            reportParameters.Add(new ReportParameter("Regional", NombreRegional));
            reportParameters.Add(new ReportParameter("Cob", NombreCOB));
            reportParameters.Add(new ReportParameter("Oficina", NombreOficina));
            reportParameters.Add(new ReportParameter("FechaCuadre", FechaMovimiento));
            reportParameters.Add(new ReportParameter("CodigoOficina", IdOficina.ToString()));

            reportParameters.Add(new ReportParameter("Url", Program.LocalServerURL));
            reportParameters.Add(new ReportParameter("UrlRetorno",UrlRetorno));
            

            var reportDataSource1 = new ReportDataSource("CTA_05_Cuadre_Medios_de_Pago", datos_Otros_Medios);
            var reportDataSource2 = new ReportDataSource("CTA_05_Cuadre_Medios_de_Pago_Efectivo", datos_Efectivo);
            var reportDataSource3 = new ReportDataSource("CTA_05_Cuadre_Medios_de_Pago_Sin_Identificar", datos_Sin_Identificar);
            var reportDataSource4 = new ReportDataSource("CTA_05_Cuadre_Medios_de_Pago_Totales", datos_Totales);

            nReportViewer.LocalReport.DataSources.Clear();
            nReportViewer.LocalReport.ReportPath = Reports.Site.Reportes.R005_Consolidado_Medio_Pago_Detalle.R005_Consolidado_Medios_Pago_Detalle;
            nReportViewer.LocalReport.DataSources.Add(reportDataSource1);
            nReportViewer.LocalReport.DataSources.Add(reportDataSource2);
            nReportViewer.LocalReport.DataSources.Add(reportDataSource3);
            nReportViewer.LocalReport.DataSources.Add(reportDataSource4);

            nReportViewer.LocalReport.SetParameters(reportParameters);
            nReportViewer.LocalReport.Refresh();
        }

        public override void Launch(ref ReportViewer nReportViewer, Dictionary<string, object> nParameters)
        {
            var ConnectionString = (TypeConnectionString)nParameters["ConnectionString"];

            NombreRegional = (string)nParameters["NombreRegional"];
            NombreCOB = (string)nParameters["NombreCOB"];
            IdOficina = (int)nParameters["IdOficina"];
            NombreOficina = (string)nParameters["NombreOficina"];
            FechaMovimiento = (string)nParameters["FechaMovimiento"];
            Login = (string)nParameters["Login"];
            IdUsuario = (int)nParameters["IdUsuario"];

            DBAgrario.DBAgrarioDataBaseManager dbmBanagrario = null;

            try
            {
                dbmBanagrario = new DBAgrario.DBAgrarioDataBaseManager(ConnectionString.BanAgrario);
                dbmBanagrario.Connection_Open(1);

                datos_Otros_Medios = dbmBanagrario.SchemaReport.PA_05_Cuadre_Medios_Pago_Detalle.DBExecute(IdOficina, FechaMovimiento, 1);
                //Registrar accion datos_Otros_Medios
                var query1 = dbmBanagrario.DataBase.LastQuery;
                Log.InsertLog(IdUsuario, Program.getIPName(), Tipo_Accion_Log.Consultar, Path_Nodo, query1, "","");

                datos_Efectivo = dbmBanagrario.SchemaReport.PA_05_Cuadre_Medios_Pago_Detalle.DBExecute(IdOficina, FechaMovimiento, 2);
                //Registrar accion datos_Efectivo
                var query2 = dbmBanagrario.DataBase.LastQuery;
                Log.InsertLog(IdUsuario, Program.getIPName(), Tipo_Accion_Log.Consultar, Path_Nodo, query2, "","");

                datos_Sin_Identificar = dbmBanagrario.SchemaReport.PA_05_Cuadre_Medios_Pago_Detalle.DBExecute(IdOficina, FechaMovimiento, 3);
                //Registrar accion datos_Sin_Identificar
                var query3 = dbmBanagrario.DataBase.LastQuery;
                Log.InsertLog(IdUsuario, Program.getIPName(), Tipo_Accion_Log.Consultar, Path_Nodo, query3, "","");

                datos_Totales = dbmBanagrario.SchemaReport.PA_05_Cuadre_Medios_Pago_Detalle.DBExecute(IdOficina, FechaMovimiento, 4);
                //Registrar accion datos_Totales
                var query4 = dbmBanagrario.DataBase.LastQuery;
                Log.InsertLog(IdUsuario, Program.getIPName(), Tipo_Accion_Log.Consultar, Path_Nodo, query4, "","");

                var reportParameters = new List<ReportParameter>();
                reportParameters.Add(new ReportParameter("UsuarioLogin", Login));
                reportParameters.Add(new ReportParameter("Regional", NombreRegional));
                reportParameters.Add(new ReportParameter("Cob", NombreCOB));
                reportParameters.Add(new ReportParameter("Oficina", NombreOficina));
                reportParameters.Add(new ReportParameter("FechaCuadre", FechaMovimiento));
                reportParameters.Add(new ReportParameter("CodigoOficina", IdOficina.ToString()));

                reportParameters.Add(new ReportParameter("Url", Program.LocalServerURL));
                reportParameters.Add(new ReportParameter("UrlRetorno", Program.LocalServerURL));

                var reportDataSource1 = new ReportDataSource("CTA_05_Cuadre_Medios_de_Pago", datos_Otros_Medios);
                var reportDataSource2 = new ReportDataSource("CTA_05_Cuadre_Medios_de_Pago_Efectivo", datos_Efectivo);
                var reportDataSource3 = new ReportDataSource("CTA_05_Cuadre_Medios_de_Pago_Sin_Identificar", datos_Sin_Identificar);
                var reportDataSource4 = new ReportDataSource("CTA_05_Cuadre_Medios_de_Pago_Totales", datos_Totales);

                nReportViewer.LocalReport.DataSources.Clear();
                nReportViewer.LocalReport.ReportPath = Reports.Site.Reportes.R005_Consolidado_Medio_Pago_Detalle.R005_Consolidado_Medios_Pago_Detalle;
                nReportViewer.LocalReport.DataSources.Add(reportDataSource1);
                nReportViewer.LocalReport.DataSources.Add(reportDataSource2);
                nReportViewer.LocalReport.DataSources.Add(reportDataSource3);
                nReportViewer.LocalReport.DataSources.Add(reportDataSource4);

                nReportViewer.LocalReport.SetParameters(reportParameters);
                nReportViewer.LocalReport.Refresh();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbmBanagrario != null) dbmBanagrario.Connection_Close();
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
            // validar de Fecha Proceso

            nMessageError = "";
            return true;
        }

        #endregion
    }
}