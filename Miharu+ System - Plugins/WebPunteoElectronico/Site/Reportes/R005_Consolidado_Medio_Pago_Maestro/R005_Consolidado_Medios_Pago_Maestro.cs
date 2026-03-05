using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebPunteoElectronico.Clases;
using DBAgrario;
using Microsoft.Reporting.WebForms;
using System.Data;

namespace WebPunteoElectronico.Site.Reportes.R005_Consolidado_Medio_Pago_Maestro
{
    public class R005_Consolidado_Medios_Pago_Maestro : WebReport
    {
        #region Declaraciones

        private int IdUsuario;
        public string Path_Nodo = "3.2.13";

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

        public DataTable DatosDataTable { get; set; }

        public R005_Consolidado_Medio_Pago_Detalle.R005_Consolidado_Medios_Pago_Detalle SubReport { get; set; }
        //{ get { return (short)HttpContext.Current.Session["R005_idRegional"]; } set { HttpContext.Current.Session["R005_idRegional"] = value; } }

        #endregion

        #region Constructores

        public R005_Consolidado_Medios_Pago_Maestro()
        {
            this.idRegional = -1;
            this.idCOB = -1;
            this.IdOficina = -1;

            this.SubReport = new R005_Consolidado_Medio_Pago_Detalle.R005_Consolidado_Medios_Pago_Detalle();
        }

        #endregion

        #region Metodos

        public override void Reload(ref ReportViewer nReportViewer)
        {
            var reportParameters = new List<ReportParameter>();
            reportParameters.Add(new ReportParameter("UsuarioLogin", this.Login));
            reportParameters.Add(new ReportParameter("Regional", this.NombreRegional));
            reportParameters.Add(new ReportParameter("Cob", this.NombreCOB));
            reportParameters.Add(new ReportParameter("Oficina", this.NombreOficina));

            reportParameters.Add(new ReportParameter("Url", Program.LocalServerURL));

            var reportDataSource = new ReportDataSource("CTA_Oficina_Diferencia_Valores", this.DatosDataTable);

            nReportViewer.LocalReport.DataSources.Clear();
            nReportViewer.LocalReport.ReportPath = Reports.Site.Reportes.R005_Consolidado_Medio_Pago_Maestro.R005_Consolidado_Medios_Pago_Maestro;
            nReportViewer.LocalReport.DataSources.Add(reportDataSource);
            nReportViewer.LocalReport.SetParameters(reportParameters);
            nReportViewer.LocalReport.Refresh();
        }

        public override void Launch(ref ReportViewer nReportViewer)
        {
            //Launch_Maestro(ref nReportViewer);
        }

        public override void Launch(ref ReportViewer nReportViewer, Dictionary<string, object> nParameters)
        {
         
                this.ConnectionString = (TypeConnectionString)nParameters["ConnectionString"];
                this.idRegional = (short)nParameters["IdRegional"];
                this.NombreRegional = (string)nParameters["NombreRegional"];
                this.idCOB = (short)nParameters["IdCOB"];
                this.NombreCOB = (string)nParameters["NombreCOB"];
                this.IdOficina = (int)nParameters["IdOficina"];
                this.NombreOficina = (string)nParameters["NombreOficina"];
                this.FechaMovimiento = (string)nParameters["FechaMovimiento"];
                this.Login = (string)nParameters["Login"];
                IdUsuario = (int)nParameters["IdUsuario"];
  
            Launch_Maestro(ref nReportViewer);
        }

        public override void Launch(ref ReportViewer nReportViewer, System.Collections.Specialized.NameValueCollection nQueryString)
        {
            var ReportName = nQueryString["ReportName"];

            switch (ReportName)
            {
                case "Detalle":
                    bool IsReload = nQueryString["Reload"] == "1";
                    if (!IsReload) this.IdOficinaDetalle = int.Parse(nQueryString["IdOficinaDetalle"]);
                    Launch_Detalle(ref nReportViewer, IsReload);
                    break;
                case "Soporte":
                    Launch_Soporte(ref nReportViewer);
                    break;
                case "Registro":
                    Launch_Registro(ref nReportViewer);
                    break;
                case "Valor":
                    Launch_Valor(ref nReportViewer);
                    break;
                case "Medio":
                    Launch_Medio(ref nReportViewer);
                    break;
                case "NoIdentificado":
                    Launch_NoIdentificado(ref nReportViewer);
                    break;
            }
        }

        private void Launch_Maestro(ref ReportViewer nReportViewer)
        {
            DBAgrario.DBAgrarioDataBaseManager dbmBanagrario = null;

            try
            {
                dbmBanagrario = new DBAgrario.DBAgrarioDataBaseManager(ConnectionString.BanAgrario);
                dbmBanagrario.Connection_Open(1);

                this.DatosDataTable = dbmBanagrario.SchemaReport.PA_05_Cuadre_Medios_Pago_Maestro.DBExecute(this.idRegional,
                                                                                                    this.idCOB,
                                                                                                    this.IdOficina,
                                                                                                    this.FechaMovimiento
                                                                                                    );

                //Registrar accion
                var query = dbmBanagrario.DataBase.LastQuery;
                Log.InsertLog(IdUsuario, Program.getIPName(), Tipo_Accion_Log.Consultar, Path_Nodo, query, "","");

                var reportParameters = new List<ReportParameter>();
                reportParameters.Add(new ReportParameter("UsuarioLogin", this.Login));
                reportParameters.Add(new ReportParameter("Regional", this.NombreRegional));
                reportParameters.Add(new ReportParameter("Cob", this.NombreCOB));
                reportParameters.Add(new ReportParameter("Oficina", this.NombreOficina));

                reportParameters.Add(new ReportParameter("Url", Program.LocalServerURL));

                var reportDataSource = new ReportDataSource("CTA_Oficina_Diferencia_Valores", this.DatosDataTable);

                nReportViewer.LocalReport.DataSources.Clear();
                nReportViewer.LocalReport.ReportPath = Reports.Site.Reportes.R005_Consolidado_Medio_Pago_Maestro.R005_Consolidado_Medios_Pago_Maestro;
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

        private void Launch_Detalle(ref ReportViewer nReportViewer, bool nIsReload)
        {
            if (nIsReload)
            {
                SubReport.Reload(ref nReportViewer);
            }
            else
            {
                var Parametros = new Dictionary<string, object>();

                Parametros.Add("ConnectionString", this.ConnectionString);
                Parametros.Add("IdRegional", this.idRegional);
                Parametros.Add("NombreRegional", this.NombreRegional);
                Parametros.Add("IdCOB", this.idCOB);
                Parametros.Add("NombreCOB", this.NombreCOB);
                Parametros.Add("IdOficina", this.IdOficinaDetalle);
                Parametros.Add("NombreOficina", this.NombreOficina);
                Parametros.Add("FechaMovimiento", this.FechaMovimiento);
                Parametros.Add("Login", this.Login);
                Parametros.Add("IdUsuario", IdUsuario);

                SubReport.Launch(ref nReportViewer, Parametros);
            }
        }

        private void Launch_Soporte(ref ReportViewer nReportViewer)
        {
            var SubReport = new R011_Soporte_Sobrantes_Medios_Pago.R011_Soporte_Sobrantes_Medios_Pago();

            var Parametros = new Dictionary<string, object>();

            Parametros.Add("ConnectionString", this.ConnectionString);
            Parametros.Add("IdRegional", this.idRegional);
            Parametros.Add("NombreRegional", this.NombreRegional);
            Parametros.Add("IdCOB", this.idCOB);
            Parametros.Add("NombreCOB", this.NombreCOB);
            Parametros.Add("IdOficina", this.IdOficinaDetalle);
            Parametros.Add("NombreOficina", this.NombreOficina);
            Parametros.Add("FechaMovimiento", this.FechaMovimiento);
            Parametros.Add("Login", this.Login);
            Parametros.Add("IdUsuario", IdUsuario);

            SubReport.Launch(ref nReportViewer, Parametros);
        }

        private void Launch_Registro(ref ReportViewer nReportViewer)
        {
            var SubReport = new R012_Registro_Sobrantes_Medios_Pago.R012_Registro_Sobrantes_Medios_Pago();

            var Parametros = new Dictionary<string, object>();

            Parametros.Add("ConnectionString", this.ConnectionString);
            Parametros.Add("IdRegional", this.idRegional);
            Parametros.Add("NombreRegional", this.NombreRegional);
            Parametros.Add("IdCOB", this.idCOB);
            Parametros.Add("NombreCOB", this.NombreCOB);
            Parametros.Add("IdOficina", this.IdOficinaDetalle);
            Parametros.Add("NombreOficina", this.NombreOficina);
            Parametros.Add("FechaMovimiento", this.FechaMovimiento);
            Parametros.Add("Login", this.Login);
            Parametros.Add("IdUsuario", IdUsuario);

            SubReport.Launch(ref nReportViewer, Parametros);
        }

        private void Launch_Valor(ref ReportViewer nReportViewer)
        {
            var SubReport = new R013_ResultadoCruceAutomatico_Valor.R013_ResultadosCruceAutomatico_Valor();

            var Parametros = new Dictionary<string, object>();

            Parametros.Add("ConnectionString", this.ConnectionString);
            Parametros.Add("IdRegional", this.idRegional);
            Parametros.Add("NombreRegional", this.NombreRegional);
            Parametros.Add("IdCOB", this.idCOB);
            Parametros.Add("NombreCOB", this.NombreCOB);
            Parametros.Add("IdOficina", this.IdOficinaDetalle);
            Parametros.Add("NombreOficina", this.NombreOficina);
            Parametros.Add("FechaMovimiento", this.FechaMovimiento);
            Parametros.Add("Login", this.Login);
            Parametros.Add("IdUsuario", IdUsuario);

            SubReport.Launch(ref nReportViewer, Parametros);
        }

        private void Launch_Medio(ref ReportViewer nReportViewer)
        {
            var SubReport = new R013_ResultadoCruceAutomatico_Medio.R013_ResultadosCruceAutomatico_Medio();

            var Parametros = new Dictionary<string, object>();

            Parametros.Add("ConnectionString", this.ConnectionString);
            Parametros.Add("IdRegional", this.idRegional);
            Parametros.Add("NombreRegional", this.NombreRegional);
            Parametros.Add("IdCOB", this.idCOB);
            Parametros.Add("NombreCOB", this.NombreCOB);
            Parametros.Add("IdOficina", this.IdOficinaDetalle);
            Parametros.Add("NombreOficina", this.NombreOficina);
            Parametros.Add("FechaMovimiento", this.FechaMovimiento);
            Parametros.Add("Login", this.Login);
            Parametros.Add("IdUsuario", IdUsuario);

            SubReport.Launch(ref nReportViewer, Parametros);
        }

        private void Launch_NoIdentificado(ref ReportViewer nReportViewer)
        {
            var SubReport = new R026_Tx_sin_Identificar_Medio_de_Pago.R026_Txs_sin_Identificar_Medio_de_Pago();

            var Parametros = new Dictionary<string, object>();

            Parametros.Add("ConnectionString", this.ConnectionString);
            Parametros.Add("IdRegional", this.idRegional);
            Parametros.Add("NombreRegional", this.NombreRegional);
            Parametros.Add("IdCOB", this.idCOB);
            Parametros.Add("NombreCOB", this.NombreCOB);
            Parametros.Add("IdOficina", this.IdOficinaDetalle);
            Parametros.Add("NombreOficina", this.NombreOficina);
            Parametros.Add("FechaMovimiento", this.FechaMovimiento);
            Parametros.Add("Login", this.Login);
            Parametros.Add("IdUsuario", IdUsuario);

            SubReport.Launch(ref nReportViewer, Parametros);
        }

        #endregion

        #region Funciones

        public override string getParameter(string nParameterName)
        {
            switch (nParameterName)
            {
                case "idRegional":
                    return this.idRegional.ToString();

                case "idCOB":
                    return this.idCOB.ToString();

                case "idOficina":
                    return this.IdOficina.ToString();

                case "FechaMovimiento":
                    return this.FechaMovimiento.ToString();
            }

            return "-1";
        }

        public override bool Validate(Dictionary<string, object> nParameters, out string nMessageError)
        {
            if (nParameters.Count > 0)
            {
                string FechaInicialS = (string)nParameters["FechaMovimiento"];
                string FechaFinalS = (string)nParameters["FechaMovimiento"];

                var FechaInicialD = Slyg.Tools.DataConvert.ToDate(FechaInicialS, Slyg.Tools.DataConvert.EnumDateFormat.yyyyMMdd, '/');
                var FechaFinalD = Slyg.Tools.DataConvert.ToDate(FechaFinalS, Slyg.Tools.DataConvert.EnumDateFormat.yyyyMMdd, '/');

                if ((FechaFinalD.Value - FechaInicialD.Value).Days > 30)
                {
                    nMessageError = "El rango de Fechas de movimiento debe ser menor o igual a 30 días";
                    return false;
                }
            }

            nMessageError = "";
            return true;
        }

        #endregion
    }
}