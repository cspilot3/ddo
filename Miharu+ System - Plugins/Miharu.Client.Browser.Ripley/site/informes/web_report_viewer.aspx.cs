using System;
using Miharu.Client.Browser.code;

namespace Miharu.Client.Browser.site.informes
{
    public partial class web_report_viewer : System.Web.UI.Page
    {
        #region Propiedades

        public WebReport WebReport
        {
            get { return Session[consts.SessionWebReport] as WebReport; }
        }

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                Config_Page();
        }

        #endregion

        #region Metodos

        public void Config_Page()
        {
            if (WebReport != null)
            {
                Page.Title = WebReport.ReportName;
                Consultar();
            }
            else Alert("Reporte no definido");
        }

        private void Consultar()
        {
            try
            {
                WebReport.Launch(ref PageReportViewer);
            }
            catch (Exception ex)
            {
                Alert(ex.Message);
            }
        }

        public void Alert(string nMessage)
        {
            var sb = new ScriptBuilder();
            Response.Write("<script type='text/javascript'>alert(\"" + sb.CleanScript(sb.CleanListCharacters(nMessage)) + "\");</script>");
        }

        #endregion
    }
}
