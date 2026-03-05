using System;

namespace Miharu.Explorer
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Redirect("_Main/Login.aspx");
        }
    }
}