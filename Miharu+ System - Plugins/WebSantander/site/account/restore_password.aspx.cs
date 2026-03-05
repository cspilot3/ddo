using System;
using WebSantander.code;

namespace  WebSantander.site.account
{
    public partial class restore_password : page_site
    {
        #region Propiedades

        private Guid Token
        {
            get { return (Guid)this.MiharuSession.Parameter["Token"]; }
            set { this.MiharuSession.Parameter["Token"] = value; }
        }

        #endregion

        #region Eventos

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            RestablecerLinkButton.Click += RestablecerLinkButton_Click;
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        void RestablecerLinkButton_Click(object sender, EventArgs e)
        {
            Restore();
        }

        #endregion

        #region Metodos

        public override void Config_Page()
        {
            if (!this.IsPostBack)
            {
                this.RestablecerLinkButton.Enabled = false;

                var token = Request.QueryString["token"];

                if (token == "")
                {
                    ScriptHelper.Site.ShowAlert(this, "Token no válido", MsgBoxIcon.IconWarning);                    
                }
                else
                {
                    Guid guid;

                    if (Guid.TryParse(token, out guid))
                    {
                        this.Token = guid;
                        
                        try
                        {
                            //string ErrMsg;

                            //var WebService = new Miharu.Security.Library.WebService.SecurityWebService(Program.SecurityWebServiceURL, this.MiharuSession.ClientIPAddress);

                            //var result = WebService.ValidateRestoreToken(this.Token, out ErrMsg);

                            //if (!result)
                            //    ScriptHelper.Site.ShowAlert(this, ErrMsg, MsgBoxIcon.IconWarning);                                
                            //else
                            //    this.RestablecerLinkButton.Enabled = true;
                        }
                        catch (Exception ex)
                        {
                            ScriptHelper.Site.ShowAlert(this, "Se presentó un error al validar el Token: " + ex.Message, MsgBoxIcon.IconError);
                        }
                    }
                    else
                    {
                        ScriptHelper.Site.ShowAlert(this, "Token no válido", MsgBoxIcon.IconWarning);
                    }
                }
            }
        }

        private void Restore()
        {
            try
            {
                //var WebService = new Miharu.Security.Library.WebService.SecurityWebService(Program.SecurityWebServiceURL, this.MiharuSession.ClientIPAddress);
                //this.MiharuSession.WebService.RestorePassword(this.Token, this.NewPasswordTextBox.Text);
                ScriptHelper.Site.ShowAlert(this, "Su contraseña ha sido cambiada exitosamente", MsgBoxIcon.IconWarning);
                this.RestablecerLinkButton.Enabled = false;
            }
            catch (Exception ex)
            {
                ScriptHelper.Site.ShowAlert(this, ex.Message, MsgBoxIcon.IconError);
            }
        }

        #endregion
    }
}