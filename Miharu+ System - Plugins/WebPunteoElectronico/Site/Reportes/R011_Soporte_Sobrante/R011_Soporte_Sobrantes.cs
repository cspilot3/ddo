using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebPunteoElectronico.Clases;
using DBAgrario;
using Microsoft.Reporting.WebForms;
using System.Data;
using System.Web.UI;

namespace WebPunteoElectronico.Site.Reportes.R011_Soporte_Sobrante
{
    public class R011_Soporte_Sobrantes : WebReport
    {
        #region Declaraciones

        private int IdUsuario;
        public string Path_Nodo = "3.2.1";

        #endregion

        #region Propiedades

        public override string ReportName
        {
            get { return "11-Soportes Sobrantes"; }
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
            var FechaProcesoInicial = (string)nParameters["FechaProcesoInicial"];
            var FechaProcesoFinal = (string)nParameters["FechaProcesoFinal"];
            int Modo = (int)nParameters["Modo"];
            string Tipologia = (string)nParameters["Tipologia"];
            string ValorUno = (string)nParameters["Valor_Uno"];
            string ValorDos = (string)nParameters["Valor_Dos"];
            var Login = (string)nParameters["Login"];
            IdUsuario = (int)nParameters["IdUsuario"];
            
            DBAgrario.DBAgrarioDataBaseManager dbmBanagrario = null;

            try
            {
                dbmBanagrario = new DBAgrario.DBAgrarioDataBaseManager(ConnectionString.BanAgrario);
                dbmBanagrario.Connection_Open(1);

                DataTable DatosDataTable = dbmBanagrario.SchemaReport.PA_11_Soporte_Sobrante.DBExecute(IdRegional,
                                                                                                    idCOB,
                                                                                                    IdOficina,
                                                                                                    FechaMovimientoInicial,
                                                                                                    FechaMovimientoFinal,
                                                                                                    FechaProcesoInicial,
                                                                                                    FechaProcesoFinal,
                                                                                                    Modo,
                                                                                                    Tipologia,
                                                                                                    ValorUno,
                                                                                                    ValorDos
                                                                                                );

                //Registrar accion
                var query = dbmBanagrario.DataBase.LastQuery;
                Log.InsertLog(IdUsuario, Program.getIPName(), Tipo_Accion_Log.Consultar, Path_Nodo, query, "","");

                var reportParameters = new List<ReportParameter>();
                reportParameters.Add(new ReportParameter("UsuarioLogin", Login));
                reportParameters.Add(new ReportParameter("Regional", NombreRegional));
                reportParameters.Add(new ReportParameter("Cob", NombreCOB));
                reportParameters.Add(new ReportParameter("Oficina", NombreOficina));

                // Si se pasan los parámetros como null o "", tampoco aparece el reporte...
                string url = Program.LocalServerURL  + Program.URLVisorImagen;
                string url2 = Program.LocalServerURL + "Site/Ajustes/CambioTipologia.aspx";

                reportParameters.Add(new ReportParameter("Url", url));
                reportParameters.Add(new ReportParameter("Url2", url2));

                var reportDataSource = new ReportDataSource("CTA_11_Soporte_Sobrante", DatosDataTable);

                nReportViewer.LocalReport.DataSources.Clear();
                nReportViewer.LocalReport.ReportPath = Reports.Site.Reportes.R011_Soporte_Sobrante.R011_Soporte_Sobrantes;
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
            int Modo = (int)nParameters["Modo"];

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
                string FechaInicialS = (string)nParameters["FechaProcesoInicial"];
                string FechaFinalS = (string)nParameters["FechaProcesoFinal"];

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