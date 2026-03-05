using System;

namespace Miharu.Explorer.Imaging._Main
{
    public partial class Sites : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            //Registrar JavaScripts
            JavaScripts.Text = getScript("_Scripts/jquery.js");
            JavaScripts.Text += getScript("_Scripts/jquery-ui/jquery-ui.js");
            JavaScripts.Text += getScript("_Scripts/jqGrid/jqGrid.js");
            JavaScripts.Text += getScript("_Scripts/jqGrid/jquery.jqGrid.min.js");
            JavaScripts.Text += getScript("_Scripts/alert/jquery.alerts.js");
        }

        private string getScript(string PathScript)
        {
            return "<script src='" + ResolveClientUrl("~/" + PathScript) + "' type='text/javascript'></script>";
        }
    }
}