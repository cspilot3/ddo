using System;
using Miharu.Client.Browser.code;

namespace  Miharu.Client.Browser
{
    public partial class _default : page_site
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public override void Config_Page()
        {
            Response.Redirect(Navigation.site.account.login);
        }
    }
}