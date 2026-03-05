using System;
using Microsoft.Reporting.WebForms;
using DBAgrario;
using Miharu.Security.Library.Session;
using System.Data;
using WebPunteoElectronico.Clases;

namespace WebPunteoElectronico.Site.ConsultaFirmas
{
    public partial class ReporteConsultas : System.Web.UI.Page
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

                    Load_Reports();
                    this.Fecha_Rep = null;
                }
            }
        }

        #endregion

        #region Metodos

        private void Load_Reports()
        {
            try
            {
                switch (Reporte)
                {
                    case "10":
                        RF10_Reporte_Consulta_Firmas_Proceso_Detalle();
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

        protected void RF10_Reporte_Consulta_Firmas_Proceso_Detalle()
        {
            DBAgrario.DBAgrarioDataBaseManager dbmBanagrario = null;

            try
            {
                //dbmBanagrario = new DBAgrarioDataBaseManager(((TypeConnectionString)Parametros.MiharuSession.Parameter["ConnectionStrings"]).BanAgrario);
                //dbmBanagrario.Connection_Open(Parametros.MiharuSession.Usuario.id);
                dbmBanagrario = new DBAgrario.DBAgrarioDataBaseManager(Parametros.cnx);
                dbmBanagrario.Connection_Open(Parametros.usrcnx);

                var data = dbmBanagrario.SchemaFirmas.PA_Consulta_Firmas_Proceso_Detalle.DBExecute(
                    Convert.ToInt32(Parametros.CodigoCOB),
                    Parametros.CodigoOficina,
                    int.Parse(Parametros.FechaInicio),
                    int.Parse(Parametros.FechaFin),
                    int.Parse(Parametros.FechaInicioP),
                    int.Parse(Parametros.FechaFinP),
                    Parametros.Usuario,
                    Parametros.TipoTransaccionNombre,
                    Parametros.Nro_Producto,
                    Parametros.Nro_Ente,
                    0,
                    Parametros.PageNumber);

                var query = dbmBanagrario.DataBase.LastQuery;

                GenerarReporteUrl(data, "RF10_Reporte_Consulta_Firmas_Proceso_Detalle", Reports.Site.Reportes.RF10_Reporte_Consulta_Firmas_Proceso_Detalle.RF10_Reporte_Consulta_Firmas_Proceso_Detalles);
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

        protected void GenerarReporteUrl(DataTable data, string NombreDs, string ReportPath)
        {
            try
            {
                this.rvUno.LocalReport.DataSources.Clear();

                var dsData = new ReportDataSource();

                string Oficina = Session["Oficina"] == null ? "--Todos--" : Session["Oficina"].ToString();
                string COB = Session["COB"] == null ? "--Todos--" : Session["COB"].ToString();

                //UsuarioLogin = Parametros.MiharuSession.Usuario.Login;
                UsuarioLogin = Parametros.usrlgn;
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