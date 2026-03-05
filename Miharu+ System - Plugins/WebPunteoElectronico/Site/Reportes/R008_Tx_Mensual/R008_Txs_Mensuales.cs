using System;
using System.Collections.Generic;
using WebPunteoElectronico.Clases;
using DBAgrario;
using Microsoft.Reporting.WebForms;
using System.Data;

namespace WebPunteoElectronico.Site.Reportes.R008_Tx_Mensual
{
    public class R008_Txs_Mensuales : WebReport
    {
        #region Declaraciones

        private int IdUsuario;
        public string Path_Nodo = "3.2.10";

        #endregion

        #region Propiedades

        public override string ReportName
        {
            get { return "08-Transacciones Mensuales"; }
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
            var FechaMovimiento = (string)nParameters["FechaMovimiento"];
            var Modo = (int)nParameters["Modo"];
            var Login = (string)nParameters["Login"];
            var esDetallado = (Boolean)nParameters["Detallado"];
            IdUsuario = (int)nParameters["IdUsuario"];
            

            DBAgrario.DBAgrarioDataBaseManager dbmBanagrario = null;

            try
            {
                dbmBanagrario = new DBAgrario.DBAgrarioDataBaseManager(ConnectionString.BanAgrario);
                dbmBanagrario.Connection_Open(1);
                var DatosDataTable = new DataTable();

                if (!esDetallado)
                {
                    DatosDataTable = dbmBanagrario.SchemaReport.PA_Reporte_Transacciones_Procesadas_Mensualmente_Tipos.DBExecute(IdRegional,
                                                                                                                        idCOB,
                                                                                                                        IdOficina,
                                                                                                                        FechaMovimiento,
                                                                                                                        Modo
                                                                                                                        );}
                else
                {
                    DatosDataTable = dbmBanagrario.SchemaReport.PA_Reporte_Transacciones_Procesadas_Mensualmente_Detallado.DBExecute(IdRegional,
                                                                                                                        idCOB,
                                                                                                                        IdOficina,
                                                                                                                        FechaMovimiento,
                                                                                                                        Modo
                                                                                                                        );
                }
                

                //Registrar accion
                var query = dbmBanagrario.DataBase.LastQuery;
                Log.InsertLog(IdUsuario, Program.getIPName(), Tipo_Accion_Log.Consultar, Path_Nodo, query, "","");

                var reportParameters = new List<ReportParameter>();
                reportParameters.Add(new ReportParameter("UsuarioLogin", Login));
                reportParameters.Add(new ReportParameter("Regional", NombreRegional));
                reportParameters.Add(new ReportParameter("Cob", NombreCOB));
                reportParameters.Add(new ReportParameter("Oficina", NombreOficina));

                var reportDataSource = new ReportDataSource("CTA_Reporte_Transacciones_Procesadas_Mensualmente_Tipos", DatosDataTable);

                nReportViewer.LocalReport.DataSources.Clear();
                nReportViewer.LocalReport.ReportPath = (!esDetallado) ?  Reports.Site.Reportes.R008_Tx_Mensual.R008_Txs_Mensuales 
                                                                       : Reports.Site.Reportes.R008_Tx_Mensual.R008_Txs_Mensuales_Detallado;
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
        }

        public override void Launch(ref ReportViewer nReportViewer, System.Collections.Specialized.NameValueCollection nQueryString)
        {
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