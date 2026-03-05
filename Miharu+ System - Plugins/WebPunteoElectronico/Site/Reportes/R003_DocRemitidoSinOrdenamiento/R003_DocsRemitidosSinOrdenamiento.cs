using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebPunteoElectronico.Clases;
using DBAgrario;
using Microsoft.Reporting.WebForms;
using System.Data;

namespace WebPunteoElectronico.Site.Reportes.R003_DocRemitidoSinOrdenamiento
{
    public class R003_DocsRemitidosSinOrdenamiento : WebReport
    {
        #region Declaraciones

        private int IdUsuario;
        public string Path_Nodo = "3.1.3";
        public string Query = "";

        #endregion

        #region Propiedades

        public override string ReportName
        {
            get { return "03-Documentos Remitidos Sin Ordenamiento Definido"; }
        }

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
            var FechaMovimientoInicial = (string)nParameters["FechaMovimientoInicial"];
            var FechaMovimientoFinal = (string)nParameters["FechaMovimientoFinal"];
            var Login = (string)nParameters["Login"];
            IdUsuario = (int)nParameters["IdUsuario"];

            DBAgrario.DBAgrarioDataBaseManager dbmBanagrario = null;

            try
            {
                dbmBanagrario = new DBAgrario.DBAgrarioDataBaseManager(ConnectionString.BanAgrario);
                dbmBanagrario.Connection_Open(1);

                DataTable DatosDataTable = dbmBanagrario.SchemaReport.PA_03_Documentos_Remitidos_Sin_Ordenamiento_Definido.DBExecute(IdRegional,
                                                                                                     idCOB,
                                                                                                     IdOficina,
                                                                                                     FechaMovimientoInicial,
                                                                                                     FechaMovimientoFinal
                                                                                                     );

                //Registrar accion
                Query = dbmBanagrario.DataBase.LastQuery;
                Log.InsertLog(IdUsuario, Program.getIPName(), Tipo_Accion_Log.Consultar, Path_Nodo, Query, "","");


                var reportParameters = new List<ReportParameter>();
                reportParameters.Add(new ReportParameter("UsuarioLogin", Login));
                reportParameters.Add(new ReportParameter("Regional", NombreRegional));
                reportParameters.Add(new ReportParameter("Cob", NombreCOB));
                reportParameters.Add(new ReportParameter("Oficina", NombreOficina));

                var reportDataSource = new ReportDataSource("CTA_03_Documento_Sin_Ordenamiento", DatosDataTable);

                nReportViewer.LocalReport.DataSources.Clear();
                nReportViewer.LocalReport.ReportPath = Reports.Site.Reportes.R003_DocRemitidoSinOrdenamiento.R003_DocsRemitidosSinOrdenamiento;
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

        #endregion

        #region Funciones

        public override string getParameter(string nParameterName)
        {
            return "-1";
        }

        public override bool Validate(Dictionary<string, object> nParameters, out string nMessageError)
        {
            // validar de Fecha Proceso

            var FechaInicialS = (string)nParameters["FechaMovimientoInicial"];
            var FechaFinalS = (string)nParameters["FechaMovimientoFinal"];

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