using System;
using Miharu.Security.Library.Session;
using Miharu.Security.Library.SecurityServiceReference;
using Miharu.Security.Library.WebService;
using WebPunteoElectronico.Clases;

namespace WebPunteoElectronico.Site.Account
{
    public partial class ChangePassword : FormBase
    {
        #region Eventos

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            Master.ShowTitle = false;
            Master.Title = "";

            ChangePasswordButton.Click += new EventHandler(ChangePasswordButton_Click);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            OldPasswordTextBox.Focus();
        }

        void ChangePasswordButton_Click(object sender, EventArgs e)
        {
            AsignarPassword(OldPasswordTextBox.Text, NewPasswordTextBox.Text);
        }

        #endregion

        #region Metodos

        protected override void Config_Page() { }

        protected override void Load_Data() { }

        private void AsignarPassword(string nOldPassword, string nNewPassword)
        {
            if (!Validar()) return;

            var WebService = new SecurityWebService(Program.SecurityWebServiceURL, MiharuSession.ClientIPAddress);

            try
            {
                WebService.CrearCanalSeguro();
                WebService.setUser(this.MiharuSession.Usuario.Login, nOldPassword);
                string nMsgError = "";

                var Respuesta = WebService.ChangePassword(this.MiharuSession.Usuario.Login, nNewPassword, out nMsgError);
                    
                switch (Respuesta)
                {
                    case EnumValidateUser.INVALIDO_PASSWORD:
                        Master.ShowAlert("Contraseña no válida", MsgBoxIcon.IconWarning);
                        break;

                    case EnumValidateUser.ERROR_PASSWORD:
                        Master.ShowAlert(nMsgError, MsgBoxIcon.IconWarning);
                        break;

                    case EnumValidateUser.VALIDO:
                        Master.ShowAlert("La contraseña se cambió exitosamente", MsgBoxIcon.IconInformation);
                        Master.FireParentPostback();
                        MiharuSession.Pagina = new Pagina(typeof(WebPunteoElectronico.Site.Blankform).FullName, "Blankform", "~/Site/DashBoard.aspx", "0");
                        //Response.Redirect(MiharuSession.Pagina.PageDir);
                        break;
                }
            }
            catch (Exception ex)
            {
                Master.ShowAlert(ex.Message, MsgBoxIcon.IconError);
            }
        }

        #endregion

        #region Funciones

        private bool Validar()
        {
            if (NewPasswordTextBox.Text != ConfirmPasswordTextBox.Text)
            {
                Master.ShowAlert("Las contraseñas ingresadas no coinciden", MsgBoxIcon.IconWarning);
                NewPasswordTextBox.Focus();
            }
            else
            {
                return true;
            }

            return false;
        }

        #endregion
    }
}