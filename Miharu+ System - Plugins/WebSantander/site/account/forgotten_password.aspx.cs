using System;
using WebSantander.code;

namespace  WebSantander.site.account
{
    public partial class forgotten_password : page_site
    {
        #region Eventos

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.RestablecerLinkButton.Click += RestablecerLinkButton_Click;
        }        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
                Config_Page();
        }

        void RestablecerLinkButton_Click(object sender, EventArgs e)
        {
            Restore();
        }

        #endregion

        #region Metodos

        public override void Config_Page()
        {                                                
        }

        private void Restore()
        {
            try
            {
                //var WebService = new Miharu.Security.Library.WebService.SecurityWebService(Program.SecurityWebServiceURL, this.SessionManager.ClientIPAddress);

                //WebService.ForgottenPassword(UserNameTextBox.Text);
                ScriptHelper.Site.ShowAlert(this, "Su solicitud a sido enviada al correo del usuario", MsgBoxIcon.IconWarning);
            }
            catch (Exception ex)
            {
                ScriptHelper.Site.ShowAlert(this, ex.Message, MsgBoxIcon.IconError);
            }
        }

        #endregion
    }
}