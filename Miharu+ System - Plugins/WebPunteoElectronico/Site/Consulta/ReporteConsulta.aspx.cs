using System;
using Microsoft.Reporting.WebForms;
using DBAgrario;
using Miharu.Security.Library.Session;
using System.Data;
using WebPunteoElectronico.Clases;

namespace WebPunteoElectronico.Site.Consulta
{
    public partial class ReporteConsulta : System.Web.UI.Page
    {
        #region Declaraciones

        public Sesion _MiharuSession;
        private string Reporte;
        private string Fecha_Rep;

        private string UsuarioLogin;        
        private ParametrosConsulta Parametros;
        
        #endregion

        #region Propiedades

        public Sesion MiharuSession
        {
            get { return (Sesion)Session["Session"]; }
        }

        public TypeConnectionString ConnectionString
        {
            get
            {
                if (MiharuSession.Parameter["ConnectionStrings"] == null)
                    return null;
                return (TypeConnectionString)MiharuSession.Parameter["ConnectionStrings"];
            }
        }

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Reporte = Request.QueryString["R"];
            this.Parametros = (ParametrosConsulta)this.MiharuSession.Parameter["ParametrosConsulta"];

            if (!this.IsPostBack)
            {
                this.Fecha_Rep = Request.QueryString["F"];
                if (Fecha_Rep != null)
                {
                    this.Fecha_Rep = Fecha_Rep.Replace("'", "");
                    this.Parametros = (ParametrosConsulta)this.MiharuSession.Parameter["ParametrosConsulta"];
             
                    Load_Reports2();
                    this.Fecha_Rep = null;
                }
            }
        }

        #endregion

        #region Metodos

        private void Load_Reports2()
        {
            try
            {
                switch (Reporte)
                {
                    case "27":
                        R027_Consulta_transacciones();
                        break;

                    default:
                        throw new Exception("Reporte no configurado.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        protected void R027_Consulta_transacciones()
        {
            DBAgrario.DBAgrarioDataBaseManager dbmBanagrario = null;

            try
            {
                dbmBanagrario = new DBAgrarioDataBaseManager(((TypeConnectionString)Parametros.MiharuSession.Parameter["ConnectionStrings"]).BanAgrario);
                dbmBanagrario.Connection_Open(Parametros.MiharuSession.Usuario.id);

                var data = dbmBanagrario.SchemaProcess.PA_Consulta_Proceso_Detalle.DBExecute(
                    Parametros.FechaInicio,
                    Parametros.FechaFin,
                    Parametros.Oficina,
                    Parametros.Documento,
                    Parametros.Producto,
                    Parametros.Codigo_Causal,
                    Parametros.CampoUno,
                    Parametros.CampoDos,
                    Parametros.CampoTres,
                    Parametros.CampoCuatro,
                    Parametros.CampoCinco,
                    Parametros.CampoSeis,
                    Parametros.CampoSiete,
                    Parametros.CampoOcho,
                    Parametros.CampoNueve,
                    Parametros.CampoDiez,
                    Parametros.ValorIni,
                    Parametros.ValorFin,
                    Parametros.Efectivo_Ini,
                    Parametros.Efectivo_Fin,
                    Parametros.Chq_Local_Ini,
                    Parametros.Chq_Local_Fin,
                    Parametros.Chq_Propio_Ini,
                    Parametros.Chq_Propio_Fin,
                    Parametros.Chq_gerencia_Ini,
                    Parametros.Chq_gerencia_Fin,
                    Parametros.Nota_Debito_Ini,
                    Parametros.Nota_Debito_Fin,
                    Parametros.Nota_Credito_Ini,
                    Parametros.Nota_Credito_Fin,
                    Parametros.Remesa_Negociada_Ini,
                    Parametros.Remesa_Negociada_Fin,
                    Parametros.Remesa_al_Cobro_Ini,
                    Parametros.Remesa_al_Cobro_Fin,
                    Parametros.No_Chq_Gerencia,
                    Parametros.No_Cta_Afectada,
                    Parametros.Comision,
                    Parametros.Key_04,
                    Parametros.Key_05,
                    0,
                    0,
                    Parametros.CodigoCOB);

                GenerarReporteUrl(data, "CTA_27_Reporte_Consulta_Transacciones", Reports.Site.Reportes.R027_Consulta_Tx.R027_Reporte_Consulta_Transacciones);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (dbmBanagrario != null) dbmBanagrario.Connection_Close();
            }
        }

        protected void R028_Consulta_SMS()
        {
            
        }

        protected void GenerarReporte(DataTable data, string NombreDs, string ReportPath)
        {
            try
            {
                var dsData = new ReportDataSource();
                string Oficina = Session["Oficina"] == null ? "--Todos--" : Session["Oficina"].ToString();
                string COB = Session["COB"] == null ? "--Todos--" : Session["COB"].ToString();

                UsuarioLogin = Parametros.MiharuSession.Usuario.Login;
                dsData = new ReportDataSource(NombreDs, data);

                var p1 = new ReportParameter("UsuarioLogin", this.UsuarioLogin);
                var p2 = new ReportParameter("Cob", COB);
                var p3 = new ReportParameter("Oficina", Oficina);

                this.rvUno.LocalReport.DataSources.Clear();
                this.rvUno.LocalReport.ReportPath = ReportPath;
                this.rvUno.LocalReport.DataSources.Add(dsData);
                this.rvUno.LocalReport.SetParameters(new[] { p1, p2, p3 });
                this.rvUno.LocalReport.Refresh();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        protected void GenerarReporteUrl(DataTable data, string NombreDs, string ReportPath)
        {
            try
            {
                this.rvUno.LocalReport.DataSources.Clear();

                var dsData = new ReportDataSource();

                string Oficina = Session["Oficina"] == null ? "--Todos--" : Session["Oficina"].ToString();
                string COB = Session["COB"] == null ? "--Todos--" : Session["COB"].ToString();

                UsuarioLogin = Parametros.MiharuSession.Usuario.Login;
                dsData = new ReportDataSource(NombreDs, data);

                var LoginReportParameter = new ReportParameter("UsuarioLogin", UsuarioLogin);
                var COBReportParameter = new ReportParameter("Cob", COB);
                var OficinaReportParameter = new ReportParameter("Oficina", Oficina);
                var URLVisor = (this.MiharuSession.Entidad.id == 9) ? Program.URLVisorImagenInterno : Program.URLVisorImagenExterno;
                var URLReportParameter = new ReportParameter("Url", URLVisor);

                this.rvUno.LocalReport.DataSources.Clear();
                this.rvUno.LocalReport.ReportPath = ReportPath;
                this.rvUno.LocalReport.DataSources.Add(dsData);
                this.rvUno.LocalReport.SetParameters(new[] { LoginReportParameter, COBReportParameter, OficinaReportParameter, URLReportParameter });
                this.rvUno.LocalReport.Refresh();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion
    }
}