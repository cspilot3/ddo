using System;
using Miharu.Client.Browser.code;

namespace  Miharu.Client.Browser.site.account
{
    public partial class connect : page_site
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
                Config_Page();
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.OkLinkButton.Click += OkLinkButton_Click;
            this.CancelLinkButton.Click += CancelLinkButton_Click;
        }

        void CancelLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(Navigation.site.account.login);
        }

        void OkLinkButton_Click(object sender, EventArgs e)
        {
            GoToMain();
        }

        #endregion

        #region Metodos

        public override void Config_Page()
        {
            var WebService = new Miharu.Security.Library.WebService.SecurityWebService(Program.SecurityWebServiceURL, this.SessionManager.ClientIPAddress);
            var Sesion = WebService.GetAppSession(Program.idModulo, this.SessionManager.Usuario.id);

            if (Sesion != null && (Sesion.Client_IP != this.SessionManager.ClientIPAddress || Sesion.Activo) &&
                Sesion.Fecha_Validacion.AddMinutes(Program.SessionScan) > DateTime.Now)
            {
                this.IpLabel.Text = Sesion.Client_IP;
                this.DateLabel.Text = Sesion.Fecha_Conexion.ToString();
                this.LastUpdateLabel.Text = Sesion.Fecha_Validacion.ToString();
            }
            else
            {
                GoToMain();
            }
        }

        private void GoToMain()
        {
            this.UserPagesWithAccess.Clear();
            this.UserPagesWithAccess.Add(ResolveUrlPage(Navigation.site.application.dashboard));

            var WebService = new Miharu.Security.Library.WebService.SecurityWebService(Program.SecurityWebServiceURL, this.SessionManager.ClientIPAddress);
            this.SessionToken = WebService.RegisterAppSession(Program.idModulo, this.SessionManager.Usuario.id);
            Response.Redirect(Navigation.site.main);
        }

        #endregion

        #region Funciones

        #endregion
    }
}