using System;
using Miharu.Client.Browser.code;
using Miharu.Client.Browser.controls;
using Miharu.Security.Library.Session;

namespace Miharu.Client.Browser.site.application
{
    public enum ServerMode
    {
        Ninguno,
        Administracion
    }

    public partial class dashboard : page_form
    {
        #region Propiedades

        public ServerMode ServerMode
        {
            get { return (ServerMode) this.SessionManager.Parameter["ServerMode"]; }
            set { this.SessionManager.Parameter["ServerMode"] = value; }
        }

        public string ServerModeString
        {
            get
            {
                var Value = this.SessionManager.Parameter["ServerMode"];
                return Enum.GetName(typeof (ServerMode), Value ?? ServerMode.Ninguno);
            }
        }

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Master.Title = "Escritorio";

            if (!this.IsPostBack)
            {

            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            Master.Title = "";

            #region Consultas

            this.ConsultaDashBoardItem.OnItemClick += ConsultasDahBoardItem_OnItemClick;

            #endregion

            #region Reportes

            this.ReportesResumenMovimientoDashBoardItem.OnItemClick += new DashBoardItem.ItemClick(ReportesResumenMovimientoDashBoardItem_OnItemClick);

            #endregion

            this.AcercaDeDashBoardItem.OnItemClick += AcercaDe_Item_OnItemClick;

            #region Administracion

            this.UsuarioDashBoardItem.OnItemClick += UsuarioDashBoardItem_OnItemClick;

            this.RolesDashBoardItem.OnItemClick += RolesDashBoardItem_OnItemClick;

            #endregion
        }

        private void ConsultasDahBoardItem_OnItemClick(controls.DashBoardItem nItem)
        {
            this.SessionManager.Pagina = new Pagina(typeof (Miharu.Client.Browser.site.consulta.consultas).FullName,
                                                    "About", Navigation.site.consulta.consultas, "1");
            this.Response.Redirect(this.SessionManager.Pagina.PageDir);
        }

        private void AcercaDe_Item_OnItemClick(controls.DashBoardItem nItem)
        {
            this.SessionManager.Pagina = new Pagina(typeof (Miharu.Client.Browser.site.application.about).FullName,
                                                    "About", Navigation.site.application.about, "0");
            this.Response.Redirect(this.SessionManager.Pagina.PageDir);
        }

        private void ReportesResumenMovimientoDashBoardItem_OnItemClick(controls.DashBoardItem nItem)
        {
            this.SessionManager.Pagina =
                new Pagina(typeof(Miharu.Client.Browser.site.reportes.reporte_resumen_movimiento).FullName, "Reportes",
                           Navigation.site.reportes.reporte_resumen_movimiento, "0");
            this.Response.Redirect(this.SessionManager.Pagina.PageDir);
        }

        #region Administracion_Eventos

        private void UsuarioDashBoardItem_OnItemClick(controls.DashBoardItem nItem)
        {
            this.SessionManager.Pagina =
                new Pagina(typeof (Miharu.Client.Browser.site.administracion.seguridad.usuarios).FullName, "Usuarios",
                           Navigation.site.administracion.seguridad.usuarios, "0");
            this.Response.Redirect(this.SessionManager.Pagina.PageDir);
        }

        private void RolesDashBoardItem_OnItemClick(DashBoardItem nItem)
        {

            this.SessionManager.Pagina =
                new Pagina(typeof (Miharu.Client.Browser.site.administracion.seguridad.roles).FullName, "Roles",
                           Navigation.site.administracion.seguridad.roles, "0");
            this.Response.Redirect(this.SessionManager.Pagina.PageDir);
        }

        #endregion


        #endregion

        #region Metodos

        public override void Config_Page()
        {
            #region Consultas

            ConfigItem(this.ConsultaDashBoardItem, Navigation.site.consulta.consultas, Auth.Consultas.Consulta);

            #endregion

            #region Administracion

            ConfigItem(this.UsuarioDashBoardItem, Navigation.site.administracion.seguridad.usuarios,
                       Auth.Administracion.Seguridad.Usuarios);
            ConfigItem(this.UsuarioDashBoardItem, Navigation.site.administracion.seguridad.roles,
                       Auth.Administracion.Seguridad.Roles);
            
            #endregion

            ConfigItem(this.ReportesResumenMovimientoDashBoardItem, Navigation.site.reportes.reporte_resumen_movimiento,
                       Auth.Informes.Resumen_Movimiento);


        }

        private void ConfigItem(DashBoardItem nMenuItem, string nNavigationUrl, string nPermiso)
        {
            if (this.SessionManager.Usuario.PerfilManager.PuedeAcceder(nPermiso))
                this.UserPagesWithAccess.Add(FormatUrl(ResolveUrl(nNavigationUrl)));
            else
                nMenuItem.Visible = false;
        }

        private void LaunchReportA(WebReport nWebReport)
        {
            //this.SessionManager.Pagina = new Pagina(typeof(WebPunteoElectronico.Site.Reportes.WebReportViewerA).FullName, "Reportes", Navigation.Site.Reportes.WebReportViewerA, "0");
            this.SessionManager.Pagina.Parameter["WebReport"] = nWebReport;
            this.Response.Redirect(this.SessionManager.Pagina.PageDir);
        }

        #endregion
    }
}