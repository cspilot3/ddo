using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebPunteoElectronico.Clases;
using Slyg.Report;
using WebPunteoElectronico.Site.Estadisticos;

namespace WebPunteoElectronico.Controls
{
    public partial class GraphManager : UserWebControlBase
    {
        #region Declaraciones

        const int GraphHeight = 300;
        const int GraphWidth = 500;
        #endregion

        #region Propiedades

        public int idWebGraph
        {
            get { return (int)this.ViewState["idWebGraph"]; }
            set { this.ViewState["idWebGraph"] = value; }
        }

        public WebGraph WebGraph
        {
            get { return this.Page.MiharuSession.Pagina.Parameter[this.idWebGraph.ToString() + "_WebGraph"] as WebGraph; }
            set { this.Page.MiharuSession.Pagina.Parameter[this.idWebGraph.ToString() + "_WebGraph"] = value; }
        }

        public TipoReporteEnum Tipo
        {
            get { return (TipoReporteEnum)this.Page.MiharuSession.Pagina.Parameter[this.idWebGraph.ToString() + "_Tipo"]; }
            set { this.Page.MiharuSession.Pagina.Parameter[this.idWebGraph.ToString() + "_Tipo"] = value; }
        }

        public WebGraph.ModoParametrosEnum ModoParametros
        {
            get { return (WebGraph.ModoParametrosEnum)this.Page.MiharuSession.Pagina.Parameter["ModoParametros"]; }
            set { this.Page.MiharuSession.Pagina.Parameter["ModoParametros"] = value; }
        }

        public Dictionary<string, object> Parametros
        {
            get { return (Dictionary<string, object>)this.Page.MiharuSession.Pagina.Parameter["Parametros"]; }
            set { this.Page.MiharuSession.Pagina.Parameter["Parametros"] = value; }
        }

        public int idReport
        {
            get { return int.Parse(this.ReportsDropDownList.SelectedValue); }
        }

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
                Config_Page();
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.Area2DImageButton.Click += new System.Web.UI.ImageClickEventHandler(Area2DImageButton_Click);
            this.Bar2DImageButton.Click += new System.Web.UI.ImageClickEventHandler(Bar2DImageButton_Click);
            this.Column2DImageButton.Click += new System.Web.UI.ImageClickEventHandler(Column2DImageButton_Click);
            this.Column3DImageButton.Click += new System.Web.UI.ImageClickEventHandler(Column3DImageButton_Click);
            this.Doughnut2DImageButton.Click += new System.Web.UI.ImageClickEventHandler(Doughnut2DImageButton_Click);
            this.FunnelImageButton.Click += new System.Web.UI.ImageClickEventHandler(FunnelImageButton_Click);
            this.LineImageButton.Click += new System.Web.UI.ImageClickEventHandler(LineImageButton_Click);
            this.Pie2DImageButton.Click += new System.Web.UI.ImageClickEventHandler(Pie2DImageButton_Click);
            this.Pie3DImageButton.Click += new System.Web.UI.ImageClickEventHandler(Pie3DImageButton_Click);

            this.DowloadZipImageButton.Click += new System.Web.UI.ImageClickEventHandler(DowloadZipImageButton_Click);

            this.ReportsDropDownList.SelectedIndexChanged += new EventHandler(ReportsDropDownList_SelectedIndexChanged);
        }

        void ReportsDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.WebGraph = getReportControler(int.Parse(ReportsDropDownList.SelectedValue));

            if (!this.InicioPanel.Visible)
                Consultar(this.Parametros, this.ModoParametros);
        }

        protected void Area2DImageButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Draw(TipoReporteEnum.Area2D);
        }

        protected void Bar2DImageButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Draw(TipoReporteEnum.Bar2D);
        }

        protected void Column2DImageButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Draw(TipoReporteEnum.Column2D);
        }

        protected void Column3DImageButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Draw(TipoReporteEnum.Column3D);
        }

        protected void Doughnut2DImageButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Draw(TipoReporteEnum.Doughnut2D);
        }

        protected void FunnelImageButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Draw(TipoReporteEnum.Funnel);
        }

        protected void LineImageButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Draw(TipoReporteEnum.Line);
        }

        protected void Pie2DImageButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Draw(TipoReporteEnum.Pie2D);
        }

        protected void Pie3DImageButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Draw(TipoReporteEnum.Pie3D);
        }

        protected void DowloadZipImageButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Dowload();
        }

        #endregion

        #region Metodos

        protected void Config_Page()
        {
            this.InicioPanel.Visible = true;
            this.NoDataPanel.Visible = false;
            this.DataPanel.Visible = false;
            this.ErrorPanel.Visible = false;
        }

        private void Draw(TipoReporteEnum nTipo)
        {
            this.Tipo = nTipo;

            if (!this.InicioPanel.Visible)
                this.WebGraph.Draw(ref this.ChartLiteral, this.Tipo, GraphWidth, GraphHeight);
        }

        public void Consultar(Dictionary<string, object> nParametros, WebGraph.ModoParametrosEnum nModoParametros)
        {
            this.InicioPanel.Visible = false;
            this.NoDataPanel.Visible = false;
            this.DataPanel.Visible = false;
            this.ErrorPanel.Visible = false;
            this.NoDiponiblePanel.Visible = false;

            this.Parametros = nParametros;
            this.ModoParametros = nModoParametros;

            if (this.WebGraph != null)
            {
                try
                {
                    string MessageError;

                    if (this.WebGraph.Validate(nParametros, out MessageError))
                    {
                        if (this.WebGraph.ModoParametros.Contains(this.ModoParametros))
                        {
                            bool Result = this.WebGraph.Load(nParametros);
                            this.WebGraph.Draw(ref this.ChartLiteral, this.Tipo, GraphWidth, GraphHeight);

                            this.NoDataPanel.Visible = !Result;
                            this.DataPanel.Visible = Result;
                        }
                        else
                        {
                            this.NoDiponiblePanel.Visible = true;
                        }
                    }
                    else
                    {
                        this.ErrorPanel.Visible = true;
                        this.ErrorLabel.Text = MessageError;
                    }
                }
                catch (Exception ex)
                {
                    this.ErrorPanel.Visible = true;
                    this.ErrorLabel.Text = ex.Message;
                }
            }
            else
            {
                this.ErrorPanel.Visible = true;
                this.ErrorLabel.Text = "No se ha definido un controlador para el informe";
            }
        }

        public void LoadReportList(List<ListItem> nRepors, int nReport, short nTipo)
        {
            this.Tipo = (TipoReporteEnum)nTipo;

            this.ReportsDropDownList.Items.Clear();
            foreach (var Report in nRepors)
            {
                this.ReportsDropDownList.Items.Add(new ListItem(Report.Text, Report.Value));
            }

            this.ReportsDropDownList.SelectedValue = nReport.ToString();

            this.WebGraph = getReportControler(nReport);
        }

        protected void Dowload()
        {
            var data = this.WebGraph.BuildZipData();

            this.Page.Master.Download(this.Page, this.WebGraph.ZipFileName, data, "application/zip");
        }

        #endregion

        #region Funciones

        private Clases.WebGraph getReportControler(int nReport)
        {
            switch (nReport)
            {
                case 0:// Consolidado Total                
                    return new WebPunteoElectronico.Site.Estadisticos.R000_Consolidado_Total.R000_Consolidados_Totales();

                case 1:// Contenedores Oficina
                    return new WebPunteoElectronico.Site.Estadisticos.R001_Contenedor_Oficina.R001_Contenedores_Oficinas();

                case 2:// Diferencias entre Doc. físicos en tula y contenedores
                    return new WebPunteoElectronico.Site.Estadisticos.R002_Diferencia_entre_Doc_Fisicos_en_Tula_y_en_Contenedor.R002_Diferencias_entre_Doc_Fisicos_en_Tula_y_en_Contenedores();

                case 3:// Documentos remitidos sin ordenamiento definido
                    return new WebPunteoElectronico.Site.Estadisticos.R003_Documento_Remitido_Sin_Ordenamiento_Definido.R003_Documentos_Remitidos_Sin_Ordenamiento_Definido();

                case 4:// Soportes de Transacciones con inconsistencias y/o anexos faltantes
                    return new WebPunteoElectronico.Site.Estadisticos.R004_Soporte_Tx_Con_Inconsistencia_y_o_Anexo_Faltante.R004_Soportes_Tx_Con_Inconsistencias_y_o_Anexos_Faltantes();

                case 5:// Oficinas pendientes de transmisión y/o envío de movimiento
                    return new WebPunteoElectronico.Site.Estadisticos.R006_Oficina_Sin_Transmision_No_Envio_Movimiento.R006_Oficinas_Sin_Transmision_No_Envio_Movimiento();

                case 6:// Oficias pendientes de proceso
                    return new WebPunteoElectronico.Site.Estadisticos.R007_Oficina_Pendiente_Proceso.R007_Oficinas_Pendiente_Proceso();

                case 7:// Transacciones mensuales
                    return new WebPunteoElectronico.Site.Estadisticos.R000_Transaccion_Procesada_Mensualmente_Tipo.R000_Transacciones_Procesadas_Mensualmente_Tipos();

                case 8:// Transacciones desmaterializadas con soporte físico
                    return new WebPunteoElectronico.Site.Estadisticos.R009_Transaccion_Desmaterializadas_Con_Soporte_Fisico.R009_Transacciones_Desmaterializadas_Con_Soporte_Fisico();

                case 9:// Soportes Sobrantes
                    return new WebPunteoElectronico.Site.Estadisticos.R011_Soporte_Sobrante.R011_Soportes_Sobrantes();

                case 10:// Registros Sobrantes
                    return new WebPunteoElectronico.Site.Estadisticos.R012_Registros_Sobrantes.R012_Registro_Sobrante();

                case 11:// Resultado proceso cruce automático
                    return new WebPunteoElectronico.Site.Estadisticos.R013_Resultado_Proceso_Cruce_Automatico.R013_Resultados_Proceso_Cruce_Automatico();

                case 12:// Novedades en validaciones de forma
                    return new WebPunteoElectronico.Site.Estadisticos.R014_Novedad_en_validacion_de_Forma.R014_Novedades_en_validaciones_de_Forma();

                case 13:// Consolidado de novedades en validaciones de forma
                    return new WebPunteoElectronico.Site.Estadisticos.R016_Consolidado_Novedad_De_Forma.R016_Consolidado_Novedades_De_Forma();

                case 14:// Consolidado del cruce automático
                    return new WebPunteoElectronico.Site.Estadisticos.R018_Consolidado_Cruce_Automatico.R018_Consolidado_Cruces_Automaticos();

                case 15:// Transacciones no identificadas
                    return new WebPunteoElectronico.Site.Estadisticos.R019_Tx_No_Identificada.R019_Tx_No_Identificadas();

                case 16:// Transacciones con cruce exitoso
                    return new WebPunteoElectronico.Site.Estadisticos.R020_Tx_Con_Cruce_Exitoso.R020_Tx_Con_Cruces_Exitosos();

                case 17:// Transacciones excluidas del cruce
                    return new WebPunteoElectronico.Site.Estadisticos.R022_Transaccion_Excluida_Archivo_Detalle.R022_Transacciones_Excluidas_Archivo_Detalle();

                case 18:// Transacciones reversadas en log
                    return new WebPunteoElectronico.Site.Estadisticos.R023_Transaccion_Reversada_Archivo_Detalle.R023_Transacciones_Reversadas_Archivo_Detalle();

                case 19:// Documentos no identificados
                    return new WebPunteoElectronico.Site.Estadisticos.R024_Doc_No_Identificado.R024_Doc_No_Identificados();

                case 20:// Oficinas pendientes de cruce
                    return new WebPunteoElectronico.Site.Estadisticos.R025_Oficina_Pendiente_Cierre.R025_Oficinas_Pendientes_Cierres();

                case 21:// Transacciones 357
                    return new WebPunteoElectronico.Site.Estadisticos.R028_Transaccion_357.R028_Transacciones_357();

                default:
                    throw new Exception("Grafico no definido");
            }
        }

        #endregion
    }
}