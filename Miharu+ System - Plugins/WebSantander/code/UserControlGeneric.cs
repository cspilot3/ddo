using System;

namespace  WebSantander.code
{
    public class userwebcontrol_generic : System.Web.UI.UserControl
    {
        #region Propiedades

        public new page_generic Page
        {
            get { return (page_generic)base.Page; }
        }

        #endregion

        #region Eventos

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        public string ResolveUrlPage(string nUrl)
        {
            return Page.ResolveUrlPage(nUrl);
        }

        public string FormatUrl(string nUrl)
        {
            return Page.FormatUrl(nUrl);
        }

        #endregion
    }
}