using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace  Miharu.Client.Browser
{
    public partial class errorform : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if( Session["Error_Title"] != null )
                Titulo.Text = (string)Session["Error_Title"];

            if( Session["Error_Message"] != null )
                Mensaje.Text = (string)Session["Error_Message"];
        }
    }
}