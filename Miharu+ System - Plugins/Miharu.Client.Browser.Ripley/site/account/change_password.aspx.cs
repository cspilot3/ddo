using System;
using Miharu.Client.Browser.code;
using Miharu.Security.Library.SecurityServiceReference;

namespace Miharu.Client.Browser.site.account
{
    public partial class change_password : page_site
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            this.OldPasswordTextBox.Focus();

            if (!this.IsPostBack)
                Config_Page();

            if (this.ChangePasswordLinkButton.Enabled)
                Validate_Locked_IP();
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ChangePasswordLinkButton.Click += ChangePasswordLinkButton_Click;
            CancelLinkButton.Click += CancelLinkButton_Click;
        }

        private void CancelLinkButton_Click(object sender, EventArgs e)
        {
            Back_Page();
        }

        private void ChangePasswordLinkButton_Click(object sender, EventArgs e)
        {
            if (this.ChangePasswordLinkButton.Enabled)
                Change_Password(this.OldPasswordTextBox.Text, this.NewPasswordTextBox.Text,
                                this.ConfirmPasswordTextBox.Text);
        }

        #endregion

        #region Metodos

        public override void Config_Page()
        {
        }

        private void Validate_Locked_IP()
        {
            try
            {
                var WebService = new Miharu.Security.Library.WebService.SecurityWebService(
                    Program.SecurityWebServiceURL, this.SessionManager.ClientIPAddress);

                if (WebService.IsIPBloqueada())
                {
                    Session["Error_Title"] = "IP Bloqueada";
                    Session["Error_Message"] = "La dirección IP: " + this.SessionManager.ClientIPAddress +
                                               "Se encuentra bloqueada por exceder el número de intentos de conexión fallidos, por favor comuniquese con el administrador del sistema";

                    Response.Redirect("~/errorform.aspx");
                }
            }
            catch (Exception ex)
            {
                OldPasswordTextBox.Enabled = false;
                NewPasswordTextBox.Enabled = false;
                ConfirmPasswordTextBox.Enabled = false;
                ChangePasswordLinkButton.Enabled = false;

                ScriptHelper.Site.ShowAlert(this, ex.Message, MsgBoxIcon.IconError);
            }
        }

        private void Back_Page()
        {
            if (this.SessionManager.Parameter[consts.BackPage] != null)
                Response.Redirect(this.SessionManager.Parameter[consts.BackPage].ToString());
        }

        private void Change_Password(string nOldPassword, string nNewPassword, string nConfirmPassword)
        {
            if (Validar(nNewPassword, nConfirmPassword))
            {
                var WebService = new Miharu.Security.Library.WebService.SecurityWebService(
                    Program.SecurityWebServiceURL, this.SessionManager.ClientIPAddress);

                try
                {
                    WebService.CrearCanalSeguro();
                    WebService.setUser(this.SessionManager.Usuario.Login, nOldPassword);
                    var nMsgError = "";

                    var Respuesta = WebService.ChangePassword(this.SessionManager.Usuario.Login, nNewPassword,
                                                              out nMsgError);

                    switch (Respuesta)
                    {
                        case EnumValidateUser.INVALIDO_PASSWORD:
                            ScriptHelper.Site.ShowAlert(this, "Contraseña no válida", MsgBoxIcon.IconWarning);
                            break;

                        case EnumValidateUser.ERROR_PASSWORD:
                            ScriptHelper.Site.ShowAlert(this, nMsgError, MsgBoxIcon.IconWarning);
                            break;

                        case EnumValidateUser.VALIDO:
                            ScriptHelper.Site.ShowAlert(this, "La contraseña se cambió exitosamente",
                                                        MsgBoxIcon.IconInformation);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    ScriptHelper.Site.ShowAlert(this, ex.Message, MsgBoxIcon.IconError);
                }
            }
        }

        #endregion

        #region Funciones

        private bool Validar(string nNewPassword, string nConfirmPassword)
        {
            if (nNewPassword == "")
                ScriptHelper.Site.ShowAlert(this, "El nuevo password no puede ser vacío", MsgBoxIcon.IconError);
            else if (nNewPassword != nConfirmPassword)
                ScriptHelper.Site.ShowAlert(this, "La confirmación del password no coincide", MsgBoxIcon.IconError);
            else
                return true;

            return false;
        }

        #endregion
    }
}