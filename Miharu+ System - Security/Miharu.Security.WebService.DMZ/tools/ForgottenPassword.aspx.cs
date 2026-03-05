using System;
using Miharu.Security.WebService.DMZ.Clases;
using System.Web.Configuration;

namespace Miharu.Security.WebService.DMZ.tools
{
    public partial class ForgottenPassword : FormPage
    {
        #region Eventos

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.restablecerLinkButton.Click += RestablecerLinkButton_Click;
        }        

        protected void Page_Load(object sender, EventArgs e)
        {
            messageLabel.Text = "";
            okMessageLabel.Text = "Especifique su nombre de usuario.";
            okMessageLabel.CssClass = "";
        }

        void RestablecerLinkButton_Click(object sender, EventArgs e)
        {
            Restore();
        }

        #endregion

        #region Metodos
        
        private void Restore()
        {            
            try
            {    
                string SecurityWebServiceURL = WebConfigurationManager.AppSettings["WebService.SecurityServiceLocal"];

                var webService = new Library.WebService.SecurityDMZWebService(SecurityWebServiceURL, GetClientIpAddress());

                var LocalUrl = ResolveFullUrl("~/SecurityDMZService.asmx").Replace("/SecurityDMZService.asmx", "");
                webService.ForgottenPasswordLocalUrl(LocalUrl, UserNameTextBox.Text);

                formPanel.Visible = false;
                messageLabel.Text = "Su solicitud ha sido enviada al correo del usuario";
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