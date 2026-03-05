using System;
using Miharu.Security.WebService.DMZ.Clases;
using System.Web.Configuration;

namespace  Miharu.Security.WebService.DMZ.tools
{
    public partial class RestorePassword : FormPage
    {
        #region Propiedades

        private Guid Token
        {
            get { return (Guid)Session["Token"]; }
            set { Session["Token"] = value; }
        }

        #endregion

        #region Eventos

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            restablecerLinkButton.Click += RestablecerLinkButton_Click;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            messageLabel.Text = "";
            formPanel.Visible = true;
            messageLabel.Text = "Ingrese la nueva contraseña.";
            messageLabel.CssClass = "";


            if (!this.IsPostBack)
                Config_Page();
        }

        void RestablecerLinkButton_Click(object sender, EventArgs e)
        {
            Restore();
        }

        #endregion

        #region Metodos

        public void Config_Page()
        {
            try
            {                
                var token = Request.QueryString["token"];

                if (token == "")
                    throw new Exception("Token no válido");

                this.Token = new Guid(token);

                string errMsg;

                string SecurityWebServiceURL = WebConfigurationManager.AppSettings["WebService.SecurityServiceLocal"];
                //var webService = new Library.WebService.SecurityDMZWebService(ResolveFullUrl("~/SecurityDMZService.asmx"), GetClientIpAddress());
                var webService = new Library.WebService.SecurityDMZWebService(SecurityWebServiceURL, GetClientIpAddress());

                var result = webService.ValidateRestoreToken(this.Token, out errMsg);

                if (!result)
                    throw new Exception(errMsg);
            }
            catch (Exception ex)
            {
                messageLabel.Text = ex.Message;
                messageLabel.CssClass = "error_message";
                formPanel.Visible = false;
            }
        }

        private void Restore()
        {
            try
            {
                string SecurityWebServiceURL = WebConfigurationManager.AppSettings["WebService.SecurityServiceLocal"];
                //var webService = new Library.WebService.SecurityDMZWebService(ResolveFullUrl("~/SecurityDMZService.asmx"), GetClientIpAddress());
                var webService = new Library.WebService.SecurityDMZWebService(SecurityWebServiceURL, GetClientIpAddress());

                webService.RestorePassword(this.Token, confirmPasswordTextBox.Text);

                formPanel.Visible = false;
                messageLabel.Text = "Su contraseña ha sido cambiada exitosamente";
                messageLabel.CssClass = "ok_message";
            }
            catch (Exception ex)
            {
                messageLabel.Text = ex.Message;
                messageLabel.CssClass = "error_message";
            }
        }

        #endregion
    }
}