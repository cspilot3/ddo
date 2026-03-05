using System;
using WebSantander.code;

namespace  WebSantander
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