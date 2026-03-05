using System;
using Miharu.Security.WebService.Clases;

namespace Miharu.Security.WebService.tools
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
                var webService = new Library.WebService.SecurityWebService(ResolveFullUrl("~/SecurityService.asmx"), GetClientIpAddress());

                webService.ForgottenPassword(UserNameTextBox.Text);

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