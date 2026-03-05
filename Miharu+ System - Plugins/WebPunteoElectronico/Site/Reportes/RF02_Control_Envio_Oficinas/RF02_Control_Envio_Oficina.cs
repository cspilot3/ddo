using System;
using System.Collections.Generic;
using WebPunteoElectronico.Clases;
using Microsoft.Reporting.WebForms;
using System.Data;

namespace WebPunteoElectronico.Site.Reportes.RF02_Control_Envio_Oficinas
{
    public class RF02_Control_Envio_Oficina : WebReport
    {
        #region Declaraciones

        private int IdUsuario;
        public string Path_Nodo = "3.2.2";

        #endregion

        #region Propiedades

        public override string ReportName
        {
            get { return "RF02 - Control de Envio por Oficina"; }
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
            var FechaMovimientoInicial = int.Parse(nParameters["FechaMovimientoInicial"].ToString().Replace("/", ""));
            var FechaMovimientoFinal = int.Parse(nParameters["FechaMovimientoFinal"].ToString().Replace("/", ""));
            var FechaProcesoInicial = int.Parse(nParameters["FechaProcesoInicial"].ToString().Replace("/", ""));
            var FechaProcesoFinal = int.Parse(nParameters["FechaProcesoFinal"].ToString().Replace("/", ""));
            var Modo = (int)nParameters["Modo"];
            var Login = (string)nParameters["Login"];
            IdUsuario = (int)nParameters["IdUsuario"];


            DBAgrario.DBAgrarioDataBaseManager dbmBanagrario = null;

            try
            {
                dbmBanagrario = new DBAgrario.DBAgrarioDataBaseManager(ConnectionString.BanAgrario);
                dbmBanagrario.Connection_Open(1);

                DataTable DatosDataTable = dbmBanagrario.SchemaFirmasReport.PA_Control_Envio_Oficina.DBExecute(IdRegional,
                                                                                                              idCOB,
                                                                                                              IdOficina,
                                                                                                              FechaMovimientoInicial,
                                                                                                              FechaMovimientoFinal,
                                                                                                              FechaProcesoInicial,
                                                                                                              FechaProcesoFinal,
                                                                                                              Modo);

                //Registrar accion
                var query = dbmBanagrario.DataBase.LastQuery;
                Log.InsertLog(IdUsuario, Program.getIPName(), Tipo_Accion_Log.Consultar, Path_Nodo, query, "", "");

                var reportParameters = new List<ReportParameter> {new ReportParameter("UsuarioLogin", Login), new ReportParameter("Regional", NombreRegional), new ReportParameter("Cob", NombreCOB), new ReportParameter("Oficina", NombreOficina), new ReportParameter("Url", Program.LocalServerURL + Program.URLVisorImagen)};

                // Si se pasan los parámetros como null o "", tampoco aparece el reporte...

                var reportDataSource = new ReportDataSource("DataSet1", DatosDataTable);

                nReportViewer.LocalReport.DataSources.Clear();
                nReportViewer.LocalReport.DisplayName = "RF02_Control_Envio_Oficina_" + FechaProcesoInicial;
                nReportViewer.LocalReport.ReportPath = Reports.Site.Reportes.RF02_Control_Envio_Oficinas.RF02_Control_Envio_Oficina;
                nReportViewer.LocalReport.DataSources.Add(reportDataSource);
                nReportViewer.LocalReport.SetParameters(reportParameters);
                nReportViewer.LocalReport.Refresh();

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
            var Modo = (int)nParameters["Modo"];

            if (Modo != 2) // validar de Fecha Proceso
            {
                var FechaInicialS = (string)nParameters["FechaMovimientoInicial"];
                var FechaFinalS = (string)nParameters["FechaMovimientoFinal"];

                var FechaInicialD = Slyg.Tools.DataConvert.ToDate(FechaInicialS, Slyg.Tools.DataConvert.EnumDateFormat.yyyyMMdd, '/');
                var FechaFinalD = Slyg.Tools.DataConvert.ToDate(FechaFinalS, Slyg.Tools.DataConvert.EnumDateFormat.yyyyMMdd, '/');

                if ((FechaFinalD.Value - FechaInicialD.Value).Days > 15)
                {
                    nMessageError = "El rango de Fechas de movimiento debe ser menor o igual a 15 días";
                    return false;
                }
            }

            if (Modo != 1) // Validar Fecha de Movimiento
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
            }

            nMessageError = "";
            return true;
        }

        #endregion
    }
}
