using System;
using System.Collections.Generic;
using WebPunteoElectronico.Clases;
using Microsoft.Reporting.WebForms;

namespace WebPunteoElectronico.Site.Reportes.R026_Tx_sin_Identificar_Medio_de_Pago
{
    public class R026_Txs_sin_Identificar_Medio_de_Pago : WebReport
    {
        #region Declaraciones

        private int IdUsuario;
        public string Path_Nodo = "3.2.13.6";

        #endregion

        #region Propiedades

        public override string ReportName
        {
            get { return "28-Txs sin Identificar Medio de Pago"; }
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
            var Login = (string)nParameters["Login"];
            var Url =  Program.URLVisorImagen;
            var UrlRetorno = Program.LocalServerURL;
            IdUsuario = (int)nParameters["IdUsuario"];

            DBAgrario.DBAgrarioDataBaseManager dbmBanagrario = null;

            try
            {
                dbmBanagrario = new DBAgrario.DBAgrarioDataBaseManager(ConnectionString.BanAgrario);
                dbmBanagrario.Connection_Open(1);

                var DatosDataTable = dbmBanagrario.SchemaReport.PA_26_Transacciones_Medio_Pago_Sin_Identificar.DBExecute(IdRegional,
                                                                                                    idCOB,
                                                                                                    IdOficina,
                                                                                                    FechaMovimiento
                                                                                                    );

                //Registrar accion
                var query = dbmBanagrario.DataBase.LastQuery;
                Log.InsertLog(IdUsuario, Program.getIPName(), Tipo_Accion_Log.Consultar, "", query, "","");


                var reportParameters = new List<ReportParameter> {new ReportParameter("UsuarioLogin", Login), new ReportParameter("Regional", NombreRegional), new ReportParameter("Cob", NombreCOB), new ReportParameter("Oficina", NombreOficina), new ReportParameter("CodigoOficina", "1"), new ReportParameter("Url", Url), new ReportParameter("UrlRetorno", UrlRetorno), new ReportParameter("FechaCuadre", "1")};

                // Si se pasan los parámetros como null o "", tampoco aparece el reporte...

                var reportDataSource = new ReportDataSource("CTA_26_Transacciones_Medio_Pago_Sin_Identificar", DatosDataTable);

                nReportViewer.LocalReport.DataSources.Clear();
                nReportViewer.LocalReport.ReportPath = Reports.Site.Reportes.R026_Tx_sin_Identificar_Medio_de_Pago.R026_Txs_sin_Identificar_Medio_de_Pago;
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
            nMessageError = "";
            return true;
        }

        #endregion
    }
}