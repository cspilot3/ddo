using System;
using Miharu.Security.Library.Session;
using WebPunteoElectronico.Clases;

namespace WebPunteoElectronico.Controls
{
    public partial class LoginWebUserControl : UserWebControlBase
    {
        #region Eventos

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            LoginLinkButton.Click += new EventHandler(LoginLinkButton_Click);
            CloseLinkButton.Click += new EventHandler(CloseLinkButton_Click);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LoginLinkButton.Visible = !this.Page.MiharuSession.UserLogged || !(this.Page.MiharuSession.Pagina != null && (this.Page.MiharuSession.Pagina.PageTitle != "Login" || this.Page.MiharuSession.Pagina.PageTitle != "ChangePassword"));
            CloseLinkButton.Visible = this.Page.MiharuSession.UserLogged;
        }

        private void LoginLinkButton_Click(object sender, EventArgs e)
        {
            LoginLinkButton.Visible = false;
            this.Page.MiharuSession.Pagina = new Pagina(typeof (WebPunteoElectronico.Site.Account.Login).FullName, "Login", "~/Site/Account/Login.aspx", "0");
        }

        private void CloseLinkButton_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("~/Main.aspx");
            //this.Page.MiharuSession.Pagina = new Security.Library.Session.Pagina(typeof(WebPunteoElectronico.Site.Account.Login).FullName, "Login", "~/Site/Account/Login.aspx", "0");
        }

        #endregion
    }
}