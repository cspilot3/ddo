using System;
using Miharu.Security.Library.Session;
using WebPunteoElectronico.Clases;

namespace WebPunteoElectronico
{
    public partial class Main : PageBase
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Session["SesionError"] != null)
                {
                    if ((bool)Session["SesionError"])
                    {
                        Master.ShowAlert("La sesión ha caducado, por favor vuelva a ingresar al sistema", MsgBoxIcon.IconWarning);
                        // Inicializar el objeto sesion
                        this.CreateSession();
                        //this.MiharuSession.Pagina = new Pagina(typeof(WebPunteoElectronico.Site.Account.Login).FullName, "Login", "~/Site/Account/Login.aspx", "0");
                    }
                }
                else
                {
                    if (MiharuSession == null || this.MiharuSession.Pagina == null)
                    {
                        this.CreateSession();
                        var miharuSession = this.MiharuSession;
                        if (miharuSession != null) miharuSession.Pagina = new Pagina(typeof(WebPunteoElectronico.Wellcome).FullName, "Wellcome", "~/Wellcome.aspx", "0");
                    }
                }
            }
        }

        #endregion

        #region Metodos

        protected override void Config_Page() { }

        protected override void Load_Data() { }

        #endregion
    }
}
