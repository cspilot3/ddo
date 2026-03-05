using System;
using WebSantander.code;
using WebSantander.controls;
using Miharu.Security.Library.Session;

namespace WebSantander.site.application
{
    public enum ServerMode
    {
        Ninguno,
        Reportes
    }

    public partial class dashboard : page_form
    {
        #region Propiedades

        public ServerMode ServerMode
        {
            get { return (ServerMode) this.MiharuSession.Parameter["ServerMode"]; }
            set { this.MiharuSession.Parameter["ServerMode"] = value; }
        }

        public string ServerModeString
        {
            get
            {
                var Value = this.MiharuSession.Parameter["ServerMode"];
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

            this.ReporteValidacionListasDashBoardItem.OnItemClick += ReportesDashBoardItem_OnItemClick;
            this.ReporteFacturacionValidacionListasDashBoardItem.OnItemClick += ReportesDashBoardItem_OnItemClick;
            this.ReporteFacturacionDetalladaValidacionListasDashBoardItem.OnItemClick += ReportesDashBoardItem_OnItemClick;
            this.ReporteCargueValidacionListasDashBoardItem.OnItemClick += ReportesDashBoardItem_OnItemClick;

            this.ReporteEmbargosDashBoardItem.OnItemClick += ReportesDashBoardItem_OnItemClick;
            this.ReporteFacturacionEmbargosDashBoardItem.OnItemClick += ReportesDashBoardItem_OnItemClick;
            this.ReporteFacturacionDetalladaEmbargosDashBoardItem.OnItemClick += ReportesDashBoardItem_OnItemClick;
            this.ReporteCruceEmbargosDashBoardItem.OnItemClick += ReportesDashBoardItem_OnItemClick;
            this.ReporteCargueEmbargosDashBoardItem.OnItemClick += ReportesDashBoardItem_OnItemClick;

            this.ReporteDesembargosDashBoardItem.OnItemClick += ReportesDashBoardItem_OnItemClick;
            this.ReporteFacturacionDesembargosDashBoardItem.OnItemClick += ReportesDashBoardItem_OnItemClick;
            this.ReporteFacturacionDetalladaDesembargosDashBoardItem.OnItemClick += ReportesDashBoardItem_OnItemClick;
            this.ReporteCruceDesembargosDashBoardItem.OnItemClick += ReportesDashBoardItem_OnItemClick;
            this.ReporteCargueDesembargosDashBoardItem.OnItemClick += ReportesDashBoardItem_OnItemClick;

            #endregion

            this.AcercaDeDashBoardItem.OnItemClick += AcercaDe_Item_OnItemClick;

        }

        private void ConsultasDahBoardItem_OnItemClick(controls.DashBoardItem nItem)
        {
            this.MiharuSession.Pagina = new Pagina(typeof(WebSantander.site.consulta.consultas).FullName,
                                                    "Consultas", Navigation.site.consulta.consultas, "0");
            this.Response.Redirect(this.MiharuSession.Pagina.PageDir);
        }

        private void AcercaDe_Item_OnItemClick(controls.DashBoardItem nItem)
        {
            this.MiharuSession.Pagina = new Pagina(typeof(WebSantander.site.application.about).FullName,
                                                    "About", Navigation.site.application.about, "0");
            this.Response.Redirect(this.MiharuSession.Pagina.PageDir);
        }

        private void ReportesDashBoardItem_OnItemClick(controls.DashBoardItem nItem)
        {
            switch (nItem.ID)
            {

                #region ValidacionListas

                case "ReporteValidacionListasDashBoardItem":
                    this.MiharuSession.Pagina = new Pagina(typeof(WebSantander.site.reportes.reportes).FullName,
                                                            "Reporte Validación de Listas", Navigation.site.reporte.ReporteValidacionListas, "0");
                    break;
                case "ReporteFacturacionValidacionListasDashBoardItem":
                    this.MiharuSession.Pagina = new Pagina(typeof(WebSantander.site.reportes.reportes).FullName,
                                                            "Reporte Facturación - Validación de Listas", Navigation.site.reporte.ReporteFacturacionValidacionListas, "0");
                    break;
                case "ReporteFacturacionDetalladaValidacionListasDashBoardItem":
                    this.MiharuSession.Pagina = new Pagina(typeof(WebSantander.site.reportes.reportes).FullName,
                                                            "Reporte Facturación Detallada - Validación de Listas", Navigation.site.reporte.ReporteFacturacionDetalladoValidacionListas, "0");
                    break;
                case "ReporteCargueValidacionListasDashBoardItem":
                    this.MiharuSession.Pagina = new Pagina(typeof(WebSantander.site.reportes.reportes).FullName,
                                                            "Reporte Cargue - Validación de Listas", Navigation.site.reporte.ReporteCargueValidacionListas, "0");
                    break;

                #endregion

                #region Embargos

                case "ReporteEmbargosDashBoardItem":
                    this.MiharuSession.Pagina = new Pagina(typeof(WebSantander.site.reportes.reportes).FullName,
                                                            "Reporte Embargos", Navigation.site.reporte.ReporteEmbargos, "0");
                    break;
                case "ReporteFacturacionEmbargosDashBoardItem":
                    this.MiharuSession.Pagina = new Pagina(typeof(WebSantander.site.reportes.reportes).FullName,
                                                            "Reporte Facturación - Embargos", Navigation.site.reporte.ReporteFacturacionEmbargos, "0");
                    break;
                case "ReporteFacturacionDetalladaEmbargosDashBoardItem":
                    this.MiharuSession.Pagina = new Pagina(typeof(WebSantander.site.reportes.reportes).FullName,
                                                            "Reporte Facturación Detallada - Embargos", Navigation.site.reporte.ReporteFacturacionDetalladoEmbargos, "0");
                    break;
                case "ReporteCruceEmbargosDashBoardItem":
                    this.MiharuSession.Pagina = new Pagina(typeof(WebSantander.site.reportes.reportes).FullName,
                                                            "Reporte Cruce - Embargos", Navigation.site.reporte.ReporteCruceEmbargos, "0");
                    break;
                case "ReporteCargueEmbargosDashBoardItem":
                    this.MiharuSession.Pagina = new Pagina(typeof(WebSantander.site.reportes.reportes).FullName,
                                                            "Reporte Cargue - Embargos", Navigation.site.reporte.ReporteCargueEmbargos, "0");
                    break;

                #endregion

                #region Desembargos

                case "ReporteDesembargosDashBoardItem":
                    this.MiharuSession.Pagina = new Pagina(typeof(WebSantander.site.reportes.reportes).FullName,
                                                            "Reporte Desembargos", Navigation.site.reporte.ReporteDesembargos, "0");
                    break;
                case "ReporteFacturacionDesembargosDashBoardItem":
                    this.MiharuSession.Pagina = new Pagina(typeof(WebSantander.site.reportes.reportes).FullName,
                                                            "Reporte Facturación - Desembargos", Navigation.site.reporte.ReporteFacturacionDesembargos, "0");
                    break;
                case "ReporteFacturacionDetalladaDesembargosDashBoardItem":
                    this.MiharuSession.Pagina = new Pagina(typeof(WebSantander.site.reportes.reportes).FullName,
                                                            "Reporte Facturación Detallada - Desembargos", Navigation.site.reporte.ReporteFacturacionDetalladoDesembargos, "0");
                    break;
                case "ReporteCruceDesembargosDashBoardItem":
                    this.MiharuSession.Pagina = new Pagina(typeof(WebSantander.site.reportes.reportes).FullName,
                                                            "Reporte Cruce - Desembargos", Navigation.site.reporte.ReporteCruceDesembargos, "0");
                    break;
                case "ReporteCargueDesembargosDashBoardItem":
                    this.MiharuSession.Pagina = new Pagina(typeof(WebSantander.site.reportes.reportes).FullName,
                                                            "Reporte Cargue - Desembargos", Navigation.site.reporte.ReporteCargueDesembargos, "0");
                    break;

                #endregion
                
            }
            this.Response.Redirect(this.MiharuSession.Pagina.PageDir);
        }

        #endregion

        #region Metodos

        public override void Config_Page()
        {
            #region Consultas

            ConfigItem(this.ConsultaDashBoardItem, Navigation.site.consulta.consultas, Auth.Consultas.Consulta);

            #endregion

            #region Reportes

            DashBoardItem Item = new DashBoardItem();
            ConfigItem(Item, Navigation.site.reporte.general, Auth.Reporte.Reportes.Path);
           
            #endregion
        }

        private void ConfigItem(DashBoardItem nMenuItem, string nNavigationUrl, string nPermiso)
        {
            if (this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(nPermiso))
                this.UserPagesWithAccess.Add(FormatUrl(ResolveUrl(nNavigationUrl)));
            else
                nMenuItem.Visible = false;
        }

        private void LaunchReport(string ID)
        {
            //WebReport nWebReport;
            //switch (ID)
            //{
            //    case "ReporteEmbargosDesembargosDashBoardItem":
            //        nWebReport = new reportes.Embargos_y_Desembargos.ReporteEmbargosDesembargos();
            //        this.MiharuSession.Pagina = new Pagina(typeof(WebSantander.site.reportes.reportes).FullName,
            //                                                "Reporte Embargos y Desembargos", Navigation.site.reporte.ReporteEmbargoDesembargo, "0");
            //        break;
            //    case "ReporteValidacionListasDashBoardItem":
            //        nWebReport = null;
            //        this.MiharuSession.Pagina = new Pagina(typeof(WebSantander.site.reportes.reportes).FullName,
            //                                                "Reporte Validación de Listas", Navigation.site.reporte.ReporteValidacionListas, "0");
            //        break;
            //    case "ReporteFacturacionDashBoardItem":
            //        nWebReport = null;
            //        this.MiharuSession.Pagina = new Pagina(typeof(WebSantander.site.reportes.reportes).FullName,
            //                                                "Reporte Facturación", Navigation.site.reporte.ReporteFacturacion, "0");
            //        break;
            //    case "ReporteFacturacionDetalladoDashBoardItem":
            //        nWebReport = null;
            //        this.MiharuSession.Pagina = new Pagina(typeof(WebSantander.site.reportes.reportes).FullName,
            //                                                "Reporte Facturación Detallado", Navigation.site.reporte.ReporteFacturacionDetallado, "0");
            //        break;
            //    case "ReporteCruceDashBoardItem":
            //        nWebReport = null;
            //        this.MiharuSession.Pagina = new Pagina(typeof(WebSantander.site.reportes.reportes).FullName,
            //                                                "Reporte Cruce Embargos y Desembargos", Navigation.site.reporte.ReporteCruce, "0");
            //        break;
            //    default:
            //        nWebReport = null;
            //        break;
            //}

            //if (nWebReport != null)
            //{
                //this.MiharuSession.Pagina.Parameter["WebReport"] = nWebReport;
                //this.Response.Redirect(this.MiharuSession.Pagina.PageDir);
            //}
            //else
            //{
            //    ScriptHelper.Site.ShowAlert(this, "El servicio no esta disponible", MsgBoxIcon.IconError);
            //}
        }
        
        #endregion
    }
}