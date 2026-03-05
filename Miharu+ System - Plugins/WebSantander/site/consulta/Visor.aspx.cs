using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Miharu.Security.Library.Session;
using WebSantander.code;

namespace WebSantander.site.consulta
{
    public partial class Visor : System.Web.UI.Page
    {

        #region Propiedades

        public Sesion MiharuSession
        {
            get { return (Sesion)Session["Session"]; }
        }

        public TypeConnectionString ConnectionString
        {
            get
            {
                if (MiharuSession.Parameter["ConnectionStrings"] == null)
                    return null;
                return (TypeConnectionString)MiharuSession.Parameter["ConnectionStrings"];
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            var URLVisor = (this.MiharuSession.Entidad.id == Program.idProcesador) ? Program.URLVisorImagenInterno : Program.URLVisorImagenExterno;
            var Token = Request.Params["token"].ToString();
            string pagina = URLVisor + "?Token=" + Token;
            Response.Redirect(pagina);
        }
    }
}