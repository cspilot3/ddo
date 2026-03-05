using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebPunteoElectronico.Clases;
using DBAgrario;
using Microsoft.Reporting.WebForms;
using System.Data;
using System.Web.UI;

namespace WebPunteoElectronico.Site.R011_Soporte_Sobrantes_Medios_Pago
{
    public class R011_Soporte_Sobrantes_Medios_Pago : WebReport
    {
        #region Declaraciones

        private int IdUsuario;
        public string Path_Nodo = "3.2.13.2";

        #endregion

        #region Propiedades

        public override string ReportName
        {
            get { return "11-Soportes Sobrantes"; }
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

        #endregion

        #region Métodos

        public override void Launch(ref ReportViewer nReportViewer, Dictionary<string, object> nParameters)
        {
            var ConnectionString = (TypeConnectionString)nParameters["ConnectionString"];
            var IdRegional = (short)nParameters["IdRegional"];
            var NombreRegional = (string)nParameters["NombreRegional"];
            var idCOB = (short)nParameters["IdCOB"];
            var NombreCOB = (string)nParameters["NombreCOB"];
            var IdOficina = (int)nParameters["IdOficina"];
            var NombreOficina = (string)nParameters["NombreOficina"];
            var FechaMovimiento = (string)nParameters["FechaMovimiento"];
            var Login = (string)nParameters["Login"];
            IdUsuario = (int)nParameters["IdUsuario"];

            DBAgrario.DBAgrarioDataBaseManager dbmBanagrario = null;

            try
            {
                dbmBanagrario = new DBAgrario.DBAgrarioDataBaseManager(ConnectionString.BanAgrario);
                dbmBanagrario.Connection_Open(1);

                DataTable DatosDataTable = dbmBanagrario.SchemaReport.PA_11_Soporte_Sobrante_Medios_Pago.DBExecute(IdRegional,
                                                                                                idCOB,
                                                                                                IdOficina,
                                                                                                FechaMovimiento);

                //Registrar accion
                var query = dbmBanagrario.DataBase.LastQuery;
                Log.InsertLog(IdUsuario, Program.getIPName(), Tipo_Accion_Log.Consultar, Path_Nodo, query, "","");

                var reportParameters = new List<ReportParameter>();
                // Si se pasan los parámetros como null o "", tampoco aparece el reporte...
                string url =  Program.URLVisorImagen;
                var UrlRetorno = Program.LocalServerURL ;

                reportParameters.Add(new ReportParameter("UsuarioLogin", Login));
                reportParameters.Add(new ReportParameter("Url", url));
                reportParameters.Add(new ReportParameter("Regional", NombreRegional));
                reportParameters.Add(new ReportParameter("Cob", NombreCOB));
                reportParameters.Add(new ReportParameter("Oficina", NombreOficina));
                reportParameters.Add(new ReportParameter("FechaCuadre", FechaMovimiento));
                reportParameters.Add(new ReportParameter("UrlRetorno", UrlRetorno));
                reportParameters.Add(new ReportParameter("CodigoOficina", IdOficina.ToString()));

                var reportDataSource = new ReportDataSource("CTA_11_Soporte_Sobrante", DatosDataTable);

                nReportViewer.LocalReport.DataSources.Clear();
                nReportViewer.LocalReport.ReportPath = Reports.Site.Reportes.R011_Soporte_Sobrante_Medios_Pago.R011_Soporte_Sobrantes_Medios_Pago;
                nReportViewer.LocalReport.DataSources.Add(reportDataSource);
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

        private void Launch_Detalle(ref ReportViewer nReportViewer)
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
            nMessageError = "";
            return true;
        }

        #endregion
    }
}