using System;
using System.Collections.Generic;
using WebPunteoElectronico.Clases;
using Microsoft.Reporting.WebForms;
using System.Data;

namespace WebPunteoElectronico.Site.Reportes.RF06_Transacciones_Mensuales
{
    public class RF06_Transacciones_Mensual : WebReport
    {
        #region Declaraciones

        private int IdUsuario;
        public string Path_Nodo = "3.2.2";

        #endregion

        #region Propiedades

        public override string ReportName
        {
            get { return "RF06 - Transacciones Mensuales"; }
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
            var FechaProceso = int.Parse(nParameters["FechaProceso"].ToString().Replace("/", ""));
            var Login = (string)nParameters["Login"];
            IdUsuario = (int)nParameters["IdUsuario"];
            var esDetallado = (Boolean)nParameters["Detallado"];

            DBAgrario.DBAgrarioDataBaseManager dbmBanagrario = null;

            try
            {
                dbmBanagrario = new DBAgrario.DBAgrarioDataBaseManager(ConnectionString.BanAgrario);
                dbmBanagrario.Connection_Open(1);

                DataTable DatosDataTable;

                if (esDetallado)
                {
                    DatosDataTable = dbmBanagrario.SchemaFirmasReport.PA_Transacciones_Mensuales_Detallado.DBExecute(IdRegional,
                                                                                                              idCOB,
                                                                                                              IdOficina,
                                                                                                              FechaProceso);
                }
                else
                {
                    DatosDataTable = dbmBanagrario.SchemaFirmasReport.PA_Transacciones_Mensuales.DBExecute(IdRegional,
                                                                                                              idCOB,
                                                                                                              IdOficina,
                                                                                                              FechaProceso);
                }
                

                //Registrar accion
                var query = dbmBanagrario.DataBase.LastQuery;
                Log.InsertLog(IdUsuario, Program.getIPName(), Tipo_Accion_Log.Consultar, Path_Nodo, query, "", "");

                var reportParameters = new List<ReportParameter> {new ReportParameter("UsuarioLogin", Login), new ReportParameter("Regional", NombreRegional), new ReportParameter("Cob", NombreCOB), new ReportParameter("Oficina", NombreOficina), new ReportParameter("Url", Program.LocalServerURL + Program.URLVisorImagen)};

                // Si se pasan los parámetros como null o "", tampoco aparece el reporte...


                var Nombre_DataSet = esDetallado ? "TBL_Transacciones_Mensuales_DetalladoDataSet" : "TBL_Transacciones_MensualesDataSet";
                var reportDataSource = new ReportDataSource(Nombre_DataSet, DatosDataTable);

                nReportViewer.LocalReport.DataSources.Clear();
                nReportViewer.LocalReport.DisplayName = "RF06_Transacciones_Mensual " + FechaProceso;
                nReportViewer.LocalReport.ReportPath = esDetallado ? Reports.Site.Reportes.RF06_Transacciones_Mensuales.RF06_Transacciones_Mensual_Detallado
                                                                    : Reports.Site.Reportes.RF06_Transacciones_Mensuales.RF06_Transacciones_Mensual;
                nReportViewer.LocalReport.DataSources.Add(reportDataSource);
                nReportViewer.LocalReport.SetParameters(reportParameters);
                nReportViewer.LocalReport.Refresh();

            }
            catch (Exception ex)
            {
                throw new Exception("Error en Reporte de Transacciones Mensuales: " + ex.Message);
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


            var FechaInicialS = (string)nParameters["FechaProcesoInicial"];
            var FechaFinalS = (string)nParameters["FechaProcesoFinal"];

            var FechaInicialD = Slyg.Tools.DataConvert.ToDate(FechaInicialS, Slyg.Tools.DataConvert.EnumDateFormat.yyyyMMdd, '/');
            var FechaFinalD = Slyg.Tools.DataConvert.ToDate(FechaFinalS, Slyg.Tools.DataConvert.EnumDateFormat.yyyyMMdd, '/');

            if ((FechaFinalD.Value - FechaInicialD.Value).Days > 15)
            {
                nMessageError = "El rango de Fechas de proceso debe ser menor o igual a 15 días";
                return false;
            }


            nMessageError = "";
            return true;
        }

        #endregion
    }
}

