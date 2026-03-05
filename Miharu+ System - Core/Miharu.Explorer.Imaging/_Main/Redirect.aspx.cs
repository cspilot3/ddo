using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Miharu.Explorer.Imaging._Main
{
    public partial class Redirect : System.Web.UI.Page
    {
        

        protected void Page_Load(object sender, EventArgs e)
        {
            Miharu.Explorer.Imaging._Clases.Utils.urlValida url = Miharu.Explorer.Imaging._Clases.Utils.getUrl();
            
            Response.Redirect(url.URLBusqueda);

        }
    }
}