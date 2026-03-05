using System;

namespace Miharu.Client.Browser.controls
{
    public partial class DashBoardItemLocal : System.Web.UI.UserControl
    {
        #region Propiedades

        public string Title
        {
            get { return (string)ViewState["Text"]; }
            set { ViewState["Text"] = value; }
        }

        public string Tooltip
        {
            get { return (string)ViewState["Tooltip"]; }
            set { ViewState["Tooltip"] = value; }
        }

        public string OnClientClick
        {
            get { return (string)ViewState["OnClientClick"]; }
            set { ViewState["OnClientClick"] = value; }
        }

        public string ImageUrl
        {
            get { return (string)ViewState["ImageUrl"]; }
            set { ViewState["ImageUrl"] = ResolveClientUrl(value); }
        }

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #endregion
    }
}