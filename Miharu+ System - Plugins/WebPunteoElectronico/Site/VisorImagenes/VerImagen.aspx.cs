using System;
using WebPunteoElectronico.Clases;

namespace WebPunteoElectronico.Site.VisorImagenes
{
    public partial class VerImagen : FormBase       
    {
        protected void Page_Load(object sender, EventArgs e)
        {            
            var token = Request.QueryString["token"];
            if (token.Contains("!"))
            {
                token = token.Remove(token.IndexOf("!"));
            }

            var URLVisor = (this.MiharuSession.Entidad.id == 9) ? Program.URLVisorImagenInterno : Program.URLVisorImagenExterno;
            Response.Redirect(URLVisor + "?Token=" + token);
        }

        protected override void Config_Page()
        {
            
        }

        protected override void Load_Data()
        {
            
        }
    }
}