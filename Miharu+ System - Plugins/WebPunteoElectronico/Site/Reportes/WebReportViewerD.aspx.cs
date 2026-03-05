using System;
using System.Collections.Generic;
using WebPunteoElectronico.Clases;

namespace WebPunteoElectronico.Site.Reportes
{
    public partial class WebReportViewerD : FormBase
    {
        #region Propiedades

        public WebReport WebReport
        {
            get { return this.MiharuSession.Pagina.Parameter["WebReport"] as WebReport; }
        }
        
        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack) return;

            Config_Page();

            Consultar();
        }
        
        #endregion

        #region Metodos

        protected override void Config_Page()
        {
            if (this.WebReport == null)
            {
                this.Master.ShowAlert("No se definió la clase manejadora del reporte", MsgBoxIcon.IconError);                
                return;
            }

            this.Master.Title = this.WebReport.ReportName;

        }

        protected override void Load_Data() { }
        
        private void Consultar()
        {
            if (this.Validar())
            {
                try
                {
                    this.WebReport.Launch(ref this.PageReportViewer, this.Request.QueryString);
                }
                catch (Exception ex)
                {
                    this.Master.ShowAlert(ex.Message, MsgBoxIcon.IconError);
                }
            }
        }

        #endregion

        #region Funciones

        private DateTime getDate(string nFecha, bool nFinal)
        {
            var Partes = nFecha.Split('/');

            return nFinal ? new DateTime(int.Parse(Partes[0]), int.Parse(Partes[1]), int.Parse(Partes[2]), 23, 59, 59) : new DateTime(int.Parse(Partes[0]), int.Parse(Partes[1]), int.Parse(Partes[2]));
        }

        private bool Validar()
        {
            try
            {
                var Parametros = new Dictionary<string, object>();
                
                string ErrorMessage;
                var Result = this.WebReport.Validate(Parametros, out ErrorMessage);

                if (!Result)
                    this.Master.ShowAlert(ErrorMessage, MsgBoxIcon.IconWarning);
                else
                    return true;
            }
            catch (Exception ex)
            {
                this.Master.ShowAlert(ex.Message, MsgBoxIcon.IconError);
            }


            return false;
        }

        #endregion
    }
}